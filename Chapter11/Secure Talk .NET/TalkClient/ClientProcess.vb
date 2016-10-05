Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports CryptoComponent

Imports System.Security.Cryptography
Imports System.Runtime.Remoting
Imports TalkComponent

Public Class ClientProcess
    Inherits MarshalByRefObject
    Implements ITalkClient

    ' This event occurs when a message is received.
    ' It's used to transfer the message from the remotable
    ' ClientProcess object to the Talk form.
    Event MessageReceived(ByVal sender As Object, ByVal e As MessageReceivedEventArgs)

    ' The web server proxy.
    Private DiscoveryService As New localhost.DiscoveryService()

    Private Rsa As RSACryptoServiceProvider
    Private SessionID As Guid
    'Private CreateUser As Boolean

    ' Contains all recently contacted clients.
    Private RecentClients As New Hashtable()

    ' The user ID for this instance.
    Private _Alias As String
    Public Property [Alias]() As String
        Get
            Return _Alias
        End Get
        Set(ByVal Value As String)
            _Alias = Value
        End Set
    End Property

    Public Sub New(ByVal [alias] As String, ByVal createUser As Boolean, ByVal rsa As RSACryptoServiceProvider)
        Me.Rsa = rsa
        Me.[Alias] = [alias]

        If createUser Then
            DiscoveryService.RegisterNewUser([alias], Me.Rsa.ToXmlString(False))
        End If
    End Sub

    ' This override ensures that the if the object is idle for an extended 
    ' period, waiting for messages, it won't lose its lease and
    ' be garbage collected.
    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

    Public Sub Login()

        ' Configure the client channel for sending messages and receiving
        ' the server callback.
        RemotingConfiguration.Configure("TalkClient.exe.config")

        ' You could accomplish the same thing in code by uncommenting
        ' the following two lines:
        ' Dim Channel As New System.Runtime.Remoting.Channels.Tcp.TcpChannel(0)
        ' ChannelServices.RegisterChannel(Channel)

        Dim Obj As ObjRef = RemotingServices.Marshal(Me)
        Dim ObjStream As New MemoryStream()
        Dim f As New BinaryFormatter()
        f.Serialize(ObjStream, Obj)
        ' Register the current user with the server.
        Dim Login As New LoginInfo()

        Login.EmailAddress = [Alias]
        Login.ObjRef = ObjStream.ToArray()
        Login.TimeStamp = DiscoveryService.GetServerDateTime()
        Dim Package As New SignedObject(Login, Me.Rsa.ToXmlString(True))
        Me.SessionID = DiscoveryService.StartSession(Package.Serialize())
    End Sub

    Public Sub LogOut()
        DiscoveryService.EndSession(Me.SessionID)
    End Sub

    Public Sub SendMessage(ByVal emailAddress As String, ByVal messageBody As String)

        Dim PeerInfo As localhost.PeerInfo

        ' Check if the peer connectivity information is cached.
        If RecentClients.Contains(emailAddress) Then
            PeerInfo = CType(RecentClients(emailAddress), localhost.PeerInfo)
        Else
            PeerInfo = DiscoveryService.GetPeerInfo(emailAddress)
            RecentClients.Add(PeerInfo.EmailAddress, PeerInfo)
        End If

        Dim ObjStream As New MemoryStream(PeerInfo.ObjRef)

        Dim f As New BinaryFormatter()
        Dim Obj As Object = f.Deserialize(ObjStream)

        Dim Message As New Message(Me.Alias, messageBody)
        Dim Package As New EncryptedObject(Message, PeerInfo.PublicKeyXml)

        Dim Peer As ITalkClient = CType(Obj, ITalkClient)

        Try
            'Peer.ReceiveMessage(Package.Serialize())
            Dim callback As New ReceiveMessageCallback(AddressOf Peer.ReceiveMessage)
            callback.BeginInvoke(Package, Nothing, Nothing)
        Catch
            ' Ignore connectivity errors.
        End Try
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Private Sub ReceiveMessage(ByVal encryptedMessage As EncryptedObject) Implements ITalkClient.ReceiveMessage

        Dim Message As Message = CType(encryptedMessage.DecryptContainedObject(Me.Rsa.ToXmlString(True)), Message)
        RaiseEvent MessageReceived(Me, New MessageReceivedEventArgs(Message.MessageBody, Message.SenderAlias))
    End Sub

    Public Function GetUsers() As ICollection
        Dim Peers() As String
        Peers = DiscoveryService.GetPeers(Me.SessionID)

        ' Identify any peers in the local cache that aren't online.
        Dim PeerSearch As New ArrayList()
        PeerSearch.AddRange(Peers)
        Dim PeersToDelete As New ArrayList()
        Dim Item As DictionaryEntry
        Dim Peer As localhost.PeerInfo
        For Each Item In Me.RecentClients
            Peer = CType(Item.Value, localhost.PeerInfo)
            ' Check if this email address is in the server list.
            If Not PeerSearch.Contains(Peer.EmailAddress) Then
                ' The email address was not found. Mark this peer for deletion.
                PeersToDelete.Add(Peer)
            End If
        Next

        ' Remove the peers that weren't found.
        For Each Peer In PeersToDelete
            Me.RecentClients.Remove(Peer.EmailAddress)
        Next

        Return Peers
    End Function

End Class

' Used for the MessageReceived event.
' This event transfers the message from the remotable
' ClientProcess object to the Talk form.
Public Class MessageReceivedEventArgs
    Inherits EventArgs

    Public Message As String
    Public SenderAlias As String

    Public Sub New(ByVal message As String, ByVal senderAlias As String)
        Me.Message = message
        Me.SenderAlias = senderAlias
    End Sub

End Class
