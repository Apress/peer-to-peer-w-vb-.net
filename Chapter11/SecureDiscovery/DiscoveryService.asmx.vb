Imports System.Web.Services
Imports System.Security.Cryptography
Imports System.Runtime.Serialization.Formatters.Binary
Imports CryptoComponent

<WebService(Namespace:="www.prosetech.com")> _
Public Class DiscoveryService
    Inherits System.Web.Services.WebService

    Private DB As New P2PDatabase()

    Public Sub New()
        Application.Lock()
        If Application("DiscoveryServiceKey") Is Nothing Then
            Application("DiscoveryServiceKey") = New RSACryptoServiceProvider()
        End If
        Application.UnLock()
    End Sub

    <WebMethod()> _
    Public Function GetServerDateTime() As DateTime
        Return DateTime.Now
    End Function

    <WebMethod()> _
    Public Sub RegisterNewUser(ByVal emailAddress As String, ByVal publicKeyXml As String)

        Try
            DB.AddPeer(emailAddress, publicKeyXml)
        Catch err As Exception
            Trace.Write(err.ToString)
            Throw New ApplicationException("Could not register new user.")
        End Try

    End Sub

    <WebMethod()> _
    Public Function StartSession(ByVal signedLoginInfo As Byte()) As Guid

        Try
            Dim Package As SignedObject = SignedObject.Deserialize(signedLoginInfo)
            Dim Login As LoginInfo = CType(Package.GetObjectWithoutSignature, LoginInfo)

            ' Check date.
            If DateTime.Now.Subtract(Login.TimeStamp).TotalMinutes > 2 Or DateTime.Now.Subtract(Login.TimeStamp).TotalMinutes < 0 Then
                Throw New ApplicationException("Invalid request message.")
            End If

            ' Verify the signature.
            Dim Peer As PeerInfo = DB.GetPeerInfo(Login.EmailAddress)
            If Not Package.ValidateSignature(Peer.PublicKeyXml) Then
                Throw New ApplicationException("Invalid request message.")
            End If

            Return DB.CreateSession(Peer.EmailAddress, Login.ObjRef)
        Catch err As Exception
            Trace.Write(err.ToString)
            Throw New ApplicationException("Could not create session.")
        End Try

    End Function

    <WebMethod()> _
    Public Sub RefreshSession(ByVal sessionID As Guid)

        Try
            DB.RefreshSession(sessionID)
        Catch err As Exception
            Trace.Write(err.ToString)
            Throw New ApplicationException("Could not refresh session.")
        End Try

    End Sub


    <WebMethod()> _
    Public Sub EndSession(ByVal sessionID As Guid)

        Try
            DB.DeleteSession(sessionID)
        Catch err As Exception
            Trace.Write(err.ToString)
            Throw New ApplicationException("Could not end session.")
        End Try

    End Sub

    <WebMethod()> _
    Public Function GetPeerInfo(ByVal emailAddress As String) As PeerInfo

        Try
            Return DB.GetPeerInfo(emailAddress)
        Catch err As Exception
            Trace.Write(err.ToString)
            Throw New ApplicationException("Could not find peer.")
        End Try

    End Function

    <WebMethod()> _
    Public Function GetPeers(ByVal sessionID As Guid) As String()

        Try
            RefreshSession(sessionID)
            Return DB.GetPeers()
        Catch err As Exception
            Trace.Write(err.ToString)
            Throw New ApplicationException("Could not find peers.")
        End Try

    End Function

End Class

