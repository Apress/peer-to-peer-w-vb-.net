Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports TalkComponent


Imports Intel.Peer.Messaging

' The ClientProcess class does double duty. Any message the TalkClient
' wants to send to the TalkServer is sent through ClientProcess.
' Similarly, any callback sent from the TalkServer to the TalkClient
' is received by ClientProcess, which then raises a local event.
' ClientProcess inherits MarshalByRefObject because the
' server must be able to call its ReceiveMessage() method.
Public Class ClientProcess
    Inherits MarshalByRefObject
    Implements ITalkClient

    Public Shared MessageReceivedCallback As ReceiveMessageCallback
    'Public Shared FileOfferReceivedCallback As ReceiveFileOfferCallback

    ' This event occurs when a message is received.
    ' It's used to transfer the message from the remotable
    ' ClientProcess object to the Talk form.
    'Public Shared Event MessageReceived(ByVal sender As Object, ByVal e As MessageReceivedEventArgs)

    ' The reference to the server object.
    ' (Technically, this really holds a proxy class.)
    Private Shared Server As ITalkServer

    ' The user ID for this instance.
    Private Shared _Alias As String
    Public Shared Property [Alias]() As String
        Get
            Return _Alias
        End Get
        Set(ByVal Value As String)
            _Alias = Value
        End Set
    End Property

    'Public Shared Sub New(ByVal [alias] As String)
    '    _Alias = [alias]
    'End Sub

    ' This override ensures that the if the object is idle for an extended 
    ' period, waiting for messages, it won't lose its lease and
    ' be garbage collected.
    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

    Public Shared Sub Login()

        ' Configure the client channel for sending messages and receiving
        ' the server callback.
        'RemotingConfiguration.Configure("TalkClient.exe.config")

        ' Create the proxy that references the server object.
        'Server = CType(Activator.GetObject(GetType(ITalkServer), "tcp://localhost:8000/TalkNET/TalkServer"), ITalkServer)

        RemotingConfiguration.ApplicationName = [Alias]
        Dim Channel As New PeerChannel()
        ChannelServices.RegisterChannel(Channel)

        Dim Uri As String = "TalkClient"
        Dim ServiceEntry As New WellKnownServiceTypeEntry(GetType(ClientProcess), Uri, WellKnownObjectMode.Singleton)

        ''''
        'PeerChannel.SecureWellKnownServiceType(ServiceEntry)

        RemotingConfiguration.RegisterWellKnownServiceType(ServiceEntry)
        Dim PeerUrl As String = PeerChannel.GetUrl(Uri)

        Dim Peer As String = "pCAC4B01B908344AF9784515B13521E15.peer"
        Dim App As String = "TalkServer"
        Dim Obj As String = "ServerObject"
        Dim Url As String = "peer://" & Peer & "/" & App & "/" & Obj

        'Url = PeerChannel.MakeSecure(Url)
        Server = CType(Activator.GetObject(GetType(ITalkServer), Url), ITalkServer)

        ' Register the current user with the server.
        ' If the server is not running, or the URL or class information is
        ' incorrect, this is the most likely place for an error to occur.
        Server.AddUser(_Alias, PeerUrl)

    End Sub

    Public Shared Sub LogOut()
        Server.RemoveUser(_Alias)
    End Sub

    Public Shared Sub SendMessage(ByVal recipientAlias As String, ByVal message As String)
        Server.SendMessage(_Alias, recipientAlias, message)
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Private Sub ReceiveMessage(ByVal message As String, ByVal senderAlias As String) Implements ITalkClient.ReceiveMessage
        'RaiseEvent MessageReceived(Nothing, New MessageReceivedEventArgs(message, senderAlias))
        If Not ClientProcess.MessageReceivedCallback Is Nothing Then
            MessageReceivedCallback(message, senderAlias)
        End If

    End Sub

    Public Shared Function GetUsers() As ICollection
        Return Server.GetUsers
    End Function

    'Public Sub ReceiveFileOffer(ByVal filename As String, ByVal fileIdentifier As System.Guid, ByVal senderAlias As String) Implements TalkComponent.ITalkClient.ReceiveFileOffer
    '    If Not ClientProcess.FileOfferReceivedCallback Is Nothing Then
    '        FileOfferReceivedCallback(filename, fileIdentifier, senderAlias)
    '    End If
    'End Sub

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
