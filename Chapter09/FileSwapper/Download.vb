Imports FileSwapper.localhost
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.IO


Public Class FileDownloadQueue

    Private AllocateWorkThread As System.Threading.Thread
    Private _Working As Boolean
    Private ListView As ListView

    Private QueuedFiles As New ArrayList()
    Private DownloadThreads As New ArrayList()

    Public Sub New(ByVal linkedControl As ListView)
        ListView = linkedControl
    End Sub

    Public Sub AddFile(ByVal file As SharedFile)
        ' Add shared file.
        SyncLock QueuedFiles
            QueuedFiles.Add(New DisplayFile(file, ListView))
        End SyncLock
    End Sub

    Public Function CheckForFile(ByVal file As SharedFile) As Boolean

        Dim Item As DisplayFile
        For Each Item In QueuedFiles
            If Item.File.Guid.ToString() = file.Guid.ToString() Then Return True
        Next

        Dim DownloadThread As FileDownloadClient
        For Each DownloadThread In DownloadThreads
            If DownloadThread.File.Guid.ToString() = file.Guid.ToString() Then Return True
        Next

        Return False
    End Function

    Private Sub AllocateWork()
        Do
            ' Remove completed.
            Dim i As Integer
            For i = DownloadThreads.Count - 1 To 0 Step -1
                Dim DownloadThread As FileDownloadClient
                DownloadThread = CType(DownloadThreads(i), FileDownloadClient)
                If Not DownloadThread.Working Then
                    SyncLock DownloadThreads
                        DownloadThreads.Remove(DownloadThread)
                    End SyncLock
                End If
            Next


            ' Allocate new while threads are available.
            Do While QueuedFiles.Count > 0 And DownloadThreads.Count < Global.Settings.MaxDownloadThreads
                Dim DownloadThread As New FileDownloadClient(QueuedFiles(0))
                SyncLock DownloadThreads
                    DownloadThreads.Add(DownloadThread)
                End SyncLock
                SyncLock QueuedFiles
                    QueuedFiles.RemoveAt(0)
                End SyncLock
                DownloadThread.StartDownload()
            Loop

            Thread.Sleep(TimeSpan.FromSeconds(10))
        Loop
    End Sub

    Public Sub StartAllocateWork()
        If _Working Then
            Throw New ApplicationException("Already in progress.")
        Else
            _Working = True

            AllocateWorkThread = New Threading.Thread(AddressOf AllocateWork)
            AllocateWorkThread.Start()
        End If
    End Sub

    Public Sub Abort()
        If _Working Then
            AllocateWorkThread.Abort()
            'AllocateWorkThread.Join()

            ' Abort all download threads.
            Dim DownloadThread As FileDownloadClient
            For Each DownloadThread In DownloadThreads
                DownloadThread.Abort()
            Next

            _Working = False
        End If
    End Sub

    Public ReadOnly Property Working() As Boolean
        Get
            Return _Working
        End Get
    End Property

End Class

Public Class FileDownloadClient

    Private DisplayFile As DisplayFile
    Private DownloadThread As System.Threading.Thread
    Private _Working As Boolean

    Public Sub New(ByVal file As DisplayFile)
        Me.DisplayFile = file
    End Sub

    Private Client As TcpClient

    Private Sub Download()

        DisplayFile.ListViewItem.ChangeStatus("Connecting...")
        ' Connect.
        Dim Completed As Boolean = False
        Do
            ' (Add error handling.)
            Client = New TcpClient()
            Client.Connect(Dns.GetHostByAddress(DisplayFile.File.Peer.IP).AddressList(0), Val(DisplayFile.File.Peer.Port))

            Dim r As New BinaryReader(Client.GetStream())

            Dim Response As String = r.ReadString()
            If Response = Messages.Ok Then
                DisplayFile.ListViewItem.ChangeStatus("Connected")

                Dim w As New BinaryWriter(Client.GetStream())
                ' Request file.
                w.Write(DisplayFile.File.Guid.ToString())


                ' Write file.
                Dim TotalBytes As Integer = r.ReadInt32()
                If TotalBytes = Messages.FileNotFound Then
                    DisplayFile.ListViewItem.ChangeStatus("File Not Found")

                Else
                    ' Write temporary file.
                    Dim FullPath As String = Path.Combine(Global.Settings.SharePath, File.Guid.ToString() & ".tmp")
                    Dim Download As New FileInfo(FullPath)

                    Dim TotalBytesRead, BytesRead As Integer

                    Dim fs As FileStream = Download.Create()
                    Dim Buffer(1024) As Byte
                    Dim Percent As Single
                    Dim LastWrite As DateTime = DateTime.Now
                    Do
                        BytesRead = r.Read(Buffer, 0, Buffer.Length)
                        fs.Write(Buffer, 0, BytesRead)
                        TotalBytesRead += BytesRead

                        If DateTime.Now.Subtract(LastWrite).TotalSeconds > 2 Then
                            LastWrite = DateTime.Now
                            Percent = Math.Round((TotalBytesRead / TotalBytes) * 100, 0)
                            DisplayFile.ListViewItem.ChangeStatus(Percent.ToString() & "% transferred")
                        End If
                    Loop While BytesRead > 0

                    fs.Close()


                    ' Ensure a unique name is chosen.
                    Dim FileNames() As String = Directory.GetFiles(Global.Settings.SharePath)
                    Dim FinalPath As String = Path.Combine(Global.Settings.SharePath, File.FileName)
                    Dim i As Integer
                    Do While Array.IndexOf(FileNames, FinalPath) <> -1
                        i += 1
                        FinalPath = Path.Combine(Global.Settings.SharePath, Path.GetFileNameWithoutExtension(File.FileName) & i.ToString() & Path.GetExtension(File.FileName))
                    Loop

                    ' Rename file.
                    System.IO.File.Move(FullPath, FinalPath)


                    ' We could also contact the server here to update the file
                    ' subsription information.

                    DisplayFile.ListViewItem.ChangeStatus("Completed")
                End If

                Client.Close()
                Completed = True

            ElseIf Response = Messages.Busy Then
                DisplayFile.ListViewItem.ChangeStatus("Busy - Will Retry")
                Client.Close()
            Else
                DisplayFile.ListViewItem.ChangeStatus("Error - Will Retry")
                Client.Close()
            End If
            If Not Completed Then Thread.Sleep(TimeSpan.FromSeconds(10))

        Loop Until Completed

        _Working = False

    End Sub

    Public Sub StartDownload()
        If _Working Then
            Throw New ApplicationException("Already in progress.")
        Else
            _Working = True
            DownloadThread = New Threading.Thread(AddressOf Download)
            DownloadThread.Start()
        End If
    End Sub

    Public Sub Abort()
        If _Working Then
            'Client.Close()
            DownloadThread.Abort()
            'DownloadThread.Join()
            _Working = False
        End If
    End Sub


    Public ReadOnly Property Working() As Boolean
        Get
            Return _Working
        End Get
    End Property

    Public ReadOnly Property File() As SharedFile
        Get
            Return DisplayFile.File
        End Get
    End Property

End Class

Public Class DisplayFile

    Private _ListViewItem As ListViewItemWrapper
    Private _File As SharedFile

    Public ReadOnly Property File() As SharedFile
        Get
            Return _File
        End Get
    End Property

    Public ReadOnly Property ListViewItem() As ListViewItemWrapper
        Get
            Return _ListViewItem
        End Get
    End Property

    Public Sub New(ByVal file As SharedFile, ByVal linkedControl As ListView)

        _ListViewItem = New ListViewItemWrapper(linkedControl, file.FileName, "Queued")
        _File = file

    End Sub

End Class


Public Class ListViewItemWrapper
    Private ListView As ListView
    Private ListViewItem As ListViewItem
    Private RowName As String
    Private RowStatus As String

    Public Sub New(ByVal listView As ListView, ByVal rowName As String, ByVal rowStatus As String)
        Me.ListView = listView
        Me.RowName = rowName
        Me.RowStatus = rowStatus

        ' Marshal the operation to the user interface thread.
        listView.Invoke(New MethodInvoker(AddressOf AddListViewItem))
    End Sub

    ' This code executes on the user interface thread.
    Private Sub AddListViewItem()
        ' Create new ListView item.
        ListViewItem = New ListViewItem(RowName)
        ListViewItem.SubItems.Add(RowStatus)
        ListView.Items.Add(ListViewItem)
    End Sub

    Public Sub ChangeStatus(ByVal rowStatus As String)
        Me.RowStatus = rowStatus

        ' Marshal the operation to the user interface thread.
        ListView.Invoke(New MethodInvoker(AddressOf RefreshListViewItem))
    End Sub

    ' This code executes on the user interface thread.
    Private Sub RefreshListViewItem()
        ListViewItem.SubItems(1).Text = RowStatus
    End Sub

End Class