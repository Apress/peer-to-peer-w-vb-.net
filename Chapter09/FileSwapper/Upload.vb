Imports FileSwapper.localhost
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.IO

Public Class FileServer

    Private WaitForRequestThread As System.Threading.Thread
    Private _Working As Boolean
    Private ListView As ListView

    Private UploadThreads As New ArrayList()

    Public Sub New(ByVal linkedControl As ListView)
        ListView = linkedControl
    End Sub

    Private Listener As TcpListener

    Public Sub WaitForRequest()
        Listener = New TcpListener(Global.Settings.Port)
        Listener.Start()
        Do
            ' (Error handling code).

            ' Block until connection received.
            Dim Client As TcpClient = Listener.AcceptTcpClient()

            ' Check for completed requests.
            ' This will free up space for new requests.
            Dim UploadThread As FileUpload
            Dim i As Integer
            For i = (UploadThreads.Count - 1) To 0 Step -1
                UploadThread = CType(UploadThreads(i), FileUpload)
                If UploadThread.Working = False Then
                    UploadThreads.Remove(UploadThread)
                End If
            Next

            Dim s As NetworkStream = Client.GetStream()
            Dim w As New BinaryWriter(s)
            If UploadThreads.Count > Global.Settings.MaxUploadThreads Then
                w.Write(Messages.Busy)
                s.Close()
            Else
                w.Write(Messages.Ok)
                Dim Upload As New FileUpload(s, ListView)
                UploadThreads.Add(Upload)
                Upload.StartUpload()
            End If
        Loop
    End Sub

    Public Sub StartWaitForRequest()
        If _Working Then
            Throw New ApplicationException("Already in progress.")
        Else
            _Working = True

            WaitForRequestThread = New Threading.Thread(AddressOf WaitForRequest)
            WaitForRequestThread.Start()
        End If
    End Sub

    Public Sub Abort()
        If _Working Then
            Listener.Stop()

            WaitForRequestThread.Abort()
            'WaitForRequestThread.Join()

            ' Abort all upload threads.
            Dim UploadThread As FileUpload
            For Each UploadThread In UploadThreads
                UploadThread.Abort()
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

Public Class FileUpload

    Private Stream As NetworkStream
    Private UploadThread As System.Threading.Thread
    Private _Working As Boolean

    Private ListView As ListView

    Public Sub New(ByVal stream As NetworkStream, ByVal listView As ListView)
        Me.Stream = stream
        Me.ListView = listView
    End Sub

    Private Sub Upload()

        ' Connect.
        Dim w As New BinaryWriter(Stream)
        Dim r As New BinaryReader(Stream)

        ' Read file request.
        Dim FileRequest As String = r.ReadString()

        Dim File As SharedFile
        Dim Filename
        For Each File In Global.SharedFiles
            If File.Guid.ToString() = FileRequest Then
                Filename = File.FileName
                Exit For
            End If
        Next

        ' Check file is shared.
        If Filename = "" Then
            w.Write(Messages.FileNotFound)
        Else

            ' Create ListView.
            Dim ListViewItem As New ListViewItemWrapper(ListView, Filename, "Initializing")

            ' (Add error handling.)
            ' Open file.
            Dim Upload As New FileInfo(Path.Combine(Global.Settings.SharePath, Filename))

            ' Read file.
            Dim TotalBytes As Integer = Upload.Length
            w.Write(TotalBytes)

            Dim TotalBytesRead, BytesRead As Integer

            Dim fs As FileStream = Upload.OpenRead()
            Dim Buffer(1024) As Byte
            Dim Percent As Single
            Dim LastWrite As DateTime = DateTime.MinValue
            Do
                BytesRead = fs.Read(Buffer, 0, Buffer.Length)
                w.Write(Buffer, 0, BytesRead)
                TotalBytesRead += BytesRead
                Percent = Math.Round((TotalBytesRead / TotalBytes) * 100, 0)
                If DateTime.Now.Subtract(LastWrite).TotalSeconds > 2 Then
                    LastWrite = DateTime.Now
                    ListViewItem.ChangeStatus(Percent.ToString() & "% transferred")
                End If
                Thread.Sleep(TimeSpan.FromSeconds(1))
            Loop While BytesRead > 0

            fs.Close()

            ListViewItem.ChangeStatus("Completed")

        End If

        Stream.Close()

        _Working = False
    End Sub

    Public Sub StartUpload()
        If _Working Then
            Throw New ApplicationException("Already in progress.")
        Else
            _Working = True
            UploadThread = New Threading.Thread(AddressOf Upload)
            UploadThread.Start()
        End If
    End Sub

    Public Sub Abort()
        If _Working Then
            UploadThread.Abort()
            'UploadThread.Join()
            _Working = False
        End If
    End Sub

    Public ReadOnly Property Working() As Boolean
        Get
            Return _Working
        End Get
    End Property

End Class