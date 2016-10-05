Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports TalkComponent

' The ClientProcess class does double duty. Any message the TalkClient
' wants to send to the TalkServer is sent through ClientProcess.
' Similarly, any callback sent from the TalkServer to the TalkClient
' is received by ClientProcess, which then raises a local event.
' ClientProcess inherits MarshalByRefObject because the
' server must be able to call its ReceiveMessage() method.
Public Class ClientProcess
    Inherits MarshalByRefObject
    Implements ITalkClient

    ' This event occurs when a message is received.
    ' It's used to transfer the message from the remotable
    ' ClientProcess object to the Talk form.
    Event MessageReceived(ByVal sender As Object, ByVal e As MessageReceivedEventArgs)

    ' The reference to the server object.
    ' (Technically, this really holds a proxy class.)
    Private Server As ITalkServer

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

    Public Sub New(ByVal [alias] As String)
        _Alias = [alias]
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

        ' Create the proxy that references the server object.
        Server = CType(Activator.GetObject(GetType(ITalkServer), "tcp://localhost:8000/TalkNET/TalkServer"), ITalkServer)

        ' Register the current user with the server.
        ' If the server is not running, or the URL or class information is
        ' incorrect, this is the most likely place for an error to occur.
        Server.AddUser(_Alias, Me)

    End Sub

    Public Sub LogOut()
        Server.RemoveUser(_Alias)
    End Sub

    Public Sub SendMessage(ByVal recipientAlias As String, ByVal message As String)
        Server.SendMessage(_Alias, recipientAlias, message)
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Private Sub ReceiveMessage(ByVal message As String, ByVal senderAlias As String) Implements ITalkClient.ReceiveMessage
        RaiseEvent MessageReceived(Me, New MessageReceivedEventArgs(message, senderAlias))
    End Sub

    Public Function GetUsers() As ICollection
        Return Server.GetUsers
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
