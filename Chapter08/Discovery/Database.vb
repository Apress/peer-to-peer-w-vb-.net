Imports System.Data.SqlClient
Imports System.Configuration

Public Class P2PDatabase

    Private ConnectionString As String

    Public Sub New()
        ConnectionString = ConfigurationSettings.AppSettings("DBConnection")
    End Sub

    Public Sub AddPeer(ByVal peer As Peer)

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("AddPeer", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
        param.Value = peer.Guid
        param = cmd.Parameters.Add("@IP", SqlDbType.NVarChar, 15)
        param.Value = peer.IP
        param = cmd.Parameters.Add("@Port", SqlDbType.SmallInt)
        param.Value = peer.Port

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

    End Sub

    Public Sub RefreshPeer(ByVal peer As Peer)

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("RefreshPeer", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
        param.Value = peer.Guid

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

    End Sub

    Public Sub AddFileInfo(ByVal files() As SharedFile, ByVal peer As Peer)

        ' Define commands and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmdDelete As New SqlCommand("DeleteFiles", con)
        cmdDelete.CommandType = CommandType.StoredProcedure

        Dim cmdFile As New SqlCommand("AddFile", con)
        cmdFile.CommandType = CommandType.StoredProcedure

        Dim cmdKeyword As New SqlCommand("AddKeyword", con)
        cmdKeyword.CommandType = CommandType.StoredProcedure

        Dim param As SqlParameter

        Try
            con.Open()

            ' Delete current registration information.
            param = cmdDelete.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
            param.Value = peer.Guid
            cmdDelete.ExecuteNonQuery()

            Dim File As SharedFile
            For Each File In files

                ' Add parameters.
                cmdFile.Parameters.Clear()
                param = cmdFile.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
                param.Value = File.Guid
                param = cmdFile.Parameters.Add("@PeerID", SqlDbType.UniqueIdentifier)
                param.Value = peer.Guid
                param = cmdFile.Parameters.Add("@FileName", SqlDbType.NVarChar, 50)
                param.Value = File.FileName
                param = cmdFile.Parameters.Add("@FileCreated", SqlDbType.DateTime)
                param.Value = File.FileCreated

                cmdFile.ExecuteNonQuery()

                ' Add keywords for this file.
                ' Note that the lack of any keywords is not considered
                ' to be an error condition (although it could be).
                Dim Keyword As String
                For Each Keyword In File.Keywords
                    cmdKeyword.Parameters.Clear()
                    param = cmdKeyword.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier)
                    param.Value = File.Guid
                    param = cmdKeyword.Parameters.Add("@PeerID", SqlDbType.UniqueIdentifier)
                    param.Value = peer.Guid
                    param = cmdKeyword.Parameters.Add("@Keyword", SqlDbType.NVarChar, 50)
                    param.Value = Keyword
                    cmdKeyword.ExecuteNonQuery()
                Next
            Next
        Finally
            con.Close()
        End Try

    End Sub

    Public Function GetFileInfo(ByVal keywords() As String) As SharedFile()

        ' Build dynamic query string.
        Dim DynamicSQL As New System.Text.StringBuilder( _
         "SELECT DISTINCT Files.ID AS FileID, Peers.ID AS PeerID, FileName, FileCreated, IP, Port " + _
         "FROM Files, Keywords, Peers " + _
         "WHERE Files.ID = keywords.FileID AND Files.PeerID = Peers.ID AND ")

        Dim i As Integer
        For i = 1 To keywords.Length
            DynamicSQL.Append("Keyword LIKE '%" + keywords(i - 1) + "%' ")
            If Not (i = keywords.Length) Then DynamicSQL.Append("OR ")
        Next

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand(DynamicSQL.ToString(), con)
        Dim r As SqlDataReader
        Dim Files As New ArrayList()

        Try
            con.Open()
            r = cmd.ExecuteReader()
            Do While (r.Read())
                Dim File As New SharedFile()
                File.Guid = r("FileID")
                File.FileName = r("FileName")
                File.FileCreated = r("FileCreated")
                File.Peer.IP = r("IP")
                File.Peer.Port = r("Port")
                File.Peer.Guid = r("PeerID")
                Files.Add(File)
            Loop
        Finally
            con.Close()
        End Try

        ' Convert the generic ArrayList to an array of SharedFile objects.
        Return CType(Files.ToArray(GetType(SharedFile)), SharedFile())

    End Function

    Public Sub DeletePeerAndFiles(ByVal peer As Peer)

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("DeletePeerAndFiles", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
        param.Value = peer.Guid

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

    End Sub

End Class
