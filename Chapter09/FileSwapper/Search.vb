Imports FileSwapper.localhost
Imports System.Threading.Thread


Public Class Search

    Private SearchThread As System.Threading.Thread
    Private ListView As ListView

    Private SearchResults() As SharedFile
    Private PingTimes As New Hashtable()

    Private _Searching As Boolean = False
    Private Keywords() As String

    Public Sub New(ByVal linkedControl As ListView)
        ListView = linkedControl
    End Sub

    Private Sub Search()
        SearchResults = App.SearchForFile(Me.Keywords)
        _Searching = False

        PingRecipients()

        Try
            ListView.Invoke(New MethodInvoker(AddressOf UpdateInterface))
        Catch
            ' An error could occur here if the search is cancelled and the
            ' class is destroyed before the invoke finishes.
        End Try
    End Sub

    Private Sub PingRecipients()

        PingTimes.Clear()
        Dim File As SharedFile
        For Each File In SearchResults
            Dim PingTime As Integer = PingUtility.Pinger.GetPingTime(File.Peer.IP)
            If PingTime = -1 Then
                PingTimes.Add(File.Guid, "Error")
            Else
                PingTimes.Add(File.Guid, PingTime.ToString() & " ms")
            End If
        Next

    End Sub

    Private Sub UpdateInterface()
        ListView.Items.Clear()
        If SearchResults.Length = 0 Then
            MessageBox.Show("No matches found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim File As SharedFile
            For Each File In SearchResults
                Dim Item As ListViewItem = ListView.Items.Add(File.FileName)
                Item.SubItems.Add(PingTimes(File.Guid).ToString())
                Item.SubItems.Add(File.FileCreated)
                Item.SubItems.Add(File.Peer.IP)
                Item.SubItems.Add(File.Peer.Port)
                Item.SubItems.Add(File.Guid.ToString())
                Item.SubItems.Add(File.Peer.Guid.ToString())

                ' Store the SharedFile object for easy access later.
                Item.Tag = File
            Next
        End If
    End Sub

    Public Sub StartSearch(ByVal keywordString As String)
        If _Searching Then
            Throw New ApplicationException("Cancel current search first.")
        Else
            _Searching = True
            SearchResults = Nothing
            Keywords = KeywordUtil.ParseKeywords(keywordString)
            SearchThread = New Threading.Thread(AddressOf Search)
            SearchThread.Start()
        End If
    End Sub

    Public Sub Abort()
        If _Searching Then

            SearchThread.Abort()
            SearchThread.Join()

            _Searching = False
        End If
    End Sub

    Public Function GetSearchResults() As SharedFile()
        If _Searching = False Then
            Return SearchResults
        Else
            Return Nothing
        End If
    End Function

    Public ReadOnly Property Searching() As Boolean
        Get
            Return _Searching
        End Get
    End Property

End Class
