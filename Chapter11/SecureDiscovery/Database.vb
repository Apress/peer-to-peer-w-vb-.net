Imports System.Runtime.Remoting
Imports System.Data.SqlClient
Imports System.Configuration
Imports CryptoComponent

Public Class P2PDatabase

    Private ConnectionString As String

    Public Sub New()
        ConnectionString = ConfigurationSettings.AppSettings("DBConnection")
    End Sub

    Public Sub AddPeer(ByVal emailAddress As String, ByVal publicKeyXml As String)

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("AddPeer", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50)
        param.Value = emailAddress
        param = cmd.Parameters.Add("@PublicKeyXml", SqlDbType.NVarChar, 300)
        param.Value = publicKeyXml

        Debug.Assert(publicKeyXml.Length < 300)

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

    End Sub

    Public Function CreateSession(ByVal emailAddress As String, ByVal objRef() As Byte) As Guid

        ' Define command and connection.
        Dim SessionID As Guid = Guid.NewGuid()

        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("CreateSession", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
        param.Value = SessionID
        param = cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 300)
        param.Value = emailAddress

        '
        Debug.Assert(objRef.Length < 1500)

        param = cmd.Parameters.Add("@ObjRef", SqlDbType.VarBinary, 1500)
        param.Value = objRef

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

        Return SessionID

    End Function

    Public Function GetPeerInfo(ByVal email As String) As PeerInfo

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("GetPeerAndSessionInfo", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50)
        param.Value = email

        Dim Peer As New PeerInfo()
        Try
            con.Open()
            Dim r As SqlDataReader = cmd.ExecuteReader()
            r.Read()
            Peer.EmailAddress = r("EmailAddress")
            Peer.ID = r("ID")
            If Not (r("ObjRef") Is DBNull.Value) Then
                Peer.ObjRef = r("ObjRef")
            End If
            Peer.PublicKeyXml = r("PublicKeyXML")
        Finally
            con.Close()
        End Try

        Return Peer
    End Function

    Public Function GetPeers() As String()

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("GetPeers", con)
        cmd.CommandType = CommandType.StoredProcedure

        Dim Peers As New ArrayList()
        Try
            con.Open()
            Dim r As SqlDataReader = cmd.ExecuteReader()
            Do While r.Read()
                Peers.Add(r("EmailAddress"))
            Loop
        Finally
            con.Close()
        End Try

        Return CType(Peers.ToArray(GetType(String)), String())
    End Function


    Public Sub RefreshSession(ByVal sessionID As Guid)

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("RefreshSession", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
        param.Value = sessionID

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

    End Sub

    Public Sub DeleteSession(ByVal sessionID As Guid)

        ' Define command and connection.
        Dim con As New SqlConnection(ConnectionString)
        Dim cmd As New SqlCommand("DeleteSession", con)
        cmd.CommandType = CommandType.StoredProcedure

        ' Add parameters.
        Dim param As SqlParameter
        param = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier)
        param.Value = sessionID

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Finally
            con.Close()
        End Try

    End Sub

End Class


Public Class PeerInfo

    Public ID As Integer
    Public EmailAddress As String
    Public PublicKeyXml As String
    Public ObjRef() As Byte

End Class