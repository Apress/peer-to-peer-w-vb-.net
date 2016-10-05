Imports TalkComponent
Imports System.Threading
Imports Intel.Peer.Messaging

' The ServerProcess receives calls from all clients. It tracks
' users in a collection, and reroutes messages to them as needed.
Public Class ServerProcess
    Inherits MarshalByRefObject
    Implements ITalkServer

    ' Tracks all the user aliases, and the "network pointer" needed
    ' to communicate with them.
    Private _ActiveUsers As New Hashtable()

    ' The object used for delivering messages.
    Private MessageDelivery As New MessageDelivery()

    ' The thread where the message delivery takes palce.
    Private DeliveryThread As New Thread(AddressOf MessageDelivery.Deliver)

    Public Sub New()
        MyBase.New()
        DeliveryThread.IsBackground = True
    End Sub

    Public Function GetUsers() As System.Collections.ICollection Implements TalkComponent.ITalkServer.GetUsers
        Return _ActiveUsers.Keys
    End Function

    Public Sub AddUser(ByVal [alias] As String, ByVal peerUrl As String) Implements TalkComponent.ITalkServer.AddUser
        Trace.Write("Added user '" & [alias] & "'")
        Dim SynchronizedCollection As Hashtable = Hashtable.Synchronized(_ActiveUsers)
        SynchronizedCollection([alias]) = peerUrl

        MessageDelivery.UpdateUsers(_ActiveUsers.Clone())
    End Sub

    Public Sub RemoveUser(ByVal [alias] As String) Implements TalkComponent.ITalkServer.RemoveUser
        Trace.Write("Removed user '" & [alias] & "'")
        Dim SynchronizedCollection As Hashtable = Hashtable.Synchronized(_ActiveUsers)
        SynchronizedCollection.Remove([alias])
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Public Sub SendMessage(ByVal senderAlias As String, ByVal recipientAlias As String, ByVal message As String) Implements TalkComponent.ITalkServer.SendMessage

        '' Deliver the message.
        'Dim Recipient As ITalkClient
        'Dim PeerUrl As String
        'If _ActiveUsers.ContainsKey(recipientAlias) Then
        '    Trace.Write("Recipient '" & recipientAlias & "' found")
        '    PeerUrl = _ActiveUsers(recipientAlias)
        'Else
        '    ' User was not found. Try to find the sender.
        '    If _ActiveUsers.ContainsKey(senderAlias) Then
        '        Trace.Write("Recipient '" & recipientAlias & "' not found")

        '        PeerUrl = _ActiveUsers(senderAlias)
        '        message = "'" & message & "' could not be delivered."
        '        senderAlias = "Talk .NET"
        '    Else
        '        Trace.Write("Recipient '" & recipientAlias & "' and sender '" & senderAlias & "' not found")
        '        ' Both sender and recipient were not found.
        '        ' Ignore this message.
        '        Return
        '    End If
        'End If

        'Recipient = CType(Activator.GetObject(GetType(ITalkServer), PeerUrl), ITalkServer)

        'If Not Recipient Is Nothing Then
        '    Trace.Write("Delivering message to '" & recipientAlias & "' from '" & senderAlias & "'")
        '    'Recipient.ReceiveMessage(message, senderAlias)
        '    Dim callback As New ReceiveMessageCallback(AddressOf Recipient.ReceiveMessage)
        '    callback.BeginInvoke(message, senderAlias, Nothing, Nothing)
        'End If

        Dim NewMessage As New Message(senderAlias, recipientAlias, message)
        MessageDelivery.RegisterMessage(NewMessage)

        Trace.Write("Queuing message to '" & recipientAlias & "' from '" & senderAlias & "'")

        If (DeliveryThread.ThreadState And ThreadState.Unstarted) = ThreadState.Unstarted Then
            Trace.Write("Start delivery thread")
            DeliveryThread.Start()
        ElseIf (DeliveryThread.ThreadState And ThreadState.Suspended) = ThreadState.Suspended Then
            Trace.Write("Resuming delivery thread")
            DeliveryThread.Resume()
        End If
    End Sub

End Class





Public Class MessageDelivery

    Private RegisteredUsers As New Hashtable()
    Private Messages As New Queue()

    Public Sub RegisterMessage(ByVal message As Message)
        SyncLock Messages
            Messages.Enqueue(message)
        End SyncLock
    End Sub

    Public Sub UpdateUsers(ByVal users As Hashtable)

        SyncLock (RegisteredUsers)
            RegisteredUsers = users
        End SyncLock

    End Sub

    Public Sub Deliver()

        Do
            Trace.Write("Starting message delivery")
            DeliverMessages()

            Trace.Write("Suspending thread")

            ' Processing is complete. The thread can be put on hold.
            Thread.CurrentThread.Suspend()

        Loop

    End Sub

    Private Sub DeliverMessages()

        Do While Messages.Count > 0

            Dim NextMessage As Message

            Trace.Write("Retrieving next message")

            SyncLock Messages
                NextMessage = CType(Messages.Dequeue(), Message)
            End SyncLock

            ' Deliver the message.
            Dim Recipient As ITalkClient
            Dim MessageBody As String
            Dim Sender As String
            Dim PeerUrl As String

            SyncLock RegisteredUsers
                If RegisteredUsers.ContainsKey(NextMessage.RecipientAlias) Then
                    PeerUrl = RegisteredUsers(NextMessage.RecipientAlias)
                    'Recipient = CType(RegisteredUsers(NextMessage.RecipientAlias), ITalkClient)
                    MessageBody = NextMessage.MessageBody
                    Sender = NextMessage.SenderAlias
                Else
                    ' User was not found. Try to find the sender.
                    If RegisteredUsers.ContainsKey(NextMessage.SenderAlias) Then
                        PeerUrl = RegisteredUsers(NextMessage.SenderAlias)
                        'Recipient = CType(RegisteredUsers(NextMessage.SenderAlias), ITalkClient)
                        MessageBody = "'" & NextMessage.MessageBody & "' could not be delivered."
                        Sender = "Talk .NET"
                    Else
                        ' Both sender and recipient were not found.
                        ' Ignore this message.
                    End If
                End If
            End SyncLock

            If PeerUrl <> "" Then
                ''''
                'PeerUrl = PeerChannel.MakeSecure(PeerUrl)

                Recipient = CType(Activator.GetObject(GetType(ITalkClient), PeerUrl), ITalkClient)
                'Recipient = CType(Activator.GetObject(GetType(ClientProcess), PeerUrl), ClientProcess)
                Trace.Write("Performing message delivery callback")
                Dim callback As New ReceiveMessageCallback(AddressOf Recipient.ReceiveMessage)
                callback.BeginInvoke(MessageBody, Sender, Nothing, Nothing)
                'Recipient.ReceiveMessage(MessageBody, Sender)
            End If

        Loop
    End Sub

End Class

Public Class Message
    Public SenderAlias As String
    Public RecipientAlias As String
    Public MessageBody As String

    Public Sub New(ByVal sender As String, ByVal recipient As String, ByVal body As String)
        Me.SenderAlias = sender
        Me.RecipientAlias = recipient
        Me.MessageBody = body
    End Sub
End Class