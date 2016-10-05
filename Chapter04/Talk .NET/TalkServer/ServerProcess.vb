Imports TalkComponent
Imports System.Threading


' The ServerProcess receives calls from all clients. It tracks
' users in a collection, and reroutes messages to them as needed.
Public Class ServerProcess
    Inherits MarshalByRefObject
    Implements ITalkServer

    ' Tracks all the user aliases, and the "network pointer" needed
    ' to communicate with them.
    Private _ActiveUsers As New Hashtable()

    Public Function GetUsers() As System.Collections.ICollection Implements TalkComponent.ITalkServer.GetUsers
        Return _ActiveUsers.Keys
    End Function

    Public Sub AddUser(ByVal [alias] As String, ByVal client As ITalkClient) Implements TalkComponent.ITalkServer.AddUser
        Trace.Write("Added user '" & [alias] & "'")
        Dim SynchronizedCollection As Hashtable = Hashtable.Synchronized(_ActiveUsers)
        SynchronizedCollection([alias]) = client
    End Sub

    Public Sub RemoveUser(ByVal [alias] As String) Implements TalkComponent.ITalkServer.RemoveUser
        Trace.Write("Removed user '" & [alias] & "'")
        Dim SynchronizedCollection As Hashtable = Hashtable.Synchronized(_ActiveUsers)
        SynchronizedCollection.Remove([alias])
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Public Sub SendMessage(ByVal senderAlias As String, ByVal recipientAlias As String, ByVal message As String) Implements TalkComponent.ITalkServer.SendMessage

        ' Deliver the message.
        Dim Recipient As ITalkClient
        If _ActiveUsers.ContainsKey(recipientAlias) Then
            Trace.Write("Recipient '" & recipientAlias & "' found")
            Recipient = CType(_ActiveUsers(recipientAlias), ITalkClient)
        Else
            ' User was not found. Try to find the sender.
            If _ActiveUsers.ContainsKey(senderAlias) Then
                Trace.Write("Recipient '" & recipientAlias & "' not found")
                Recipient = CType(_ActiveUsers(senderAlias), ITalkClient)
                message = "'" & message & "' could not be delivered."
                senderAlias = "Talk .NET"
            Else
                Trace.Write("Recipient '" & recipientAlias & "' and sender '" & senderAlias & "' not found")
                ' Both sender and recipient were not found.
                ' Ignore this message.
            End If
        End If

        Trace.Write("Delivering message to '" & recipientAlias & "' from '" & senderAlias & "'")
        If Not Recipient Is Nothing Then
            'Recipient.ReceiveMessage(message, senderAlias)
            Dim callback As New ReceiveMessageCallback(AddressOf Recipient.ReceiveMessage)
            callback.BeginInvoke(message, senderAlias, Nothing, Nothing)
        End If

    End Sub

End Class
