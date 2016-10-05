Public Interface ITalkServer

    ' These methods allow users to be registered and unregistered with the server.
    Sub AddUser(ByVal [alias] As String, ByVal callback As ITalkClient)
    Sub RemoveUser(ByVal [alias] As String)

    ' This returns a collection of currently logged in user names.
    Function GetUsers() As ICollection

    ' The client calls this to send a message to the server.
    Sub SendMessage(ByVal senderAlias As String, ByVal recipientAlias As String, ByVal message As String)

End Interface

Public Interface ITalkClient

    ' The server calls this to forward a message to the appropriate client.
    Sub ReceiveMessage(ByVal message As String, ByVal senderAlias As String)

End Interface

' This delegate is primarily for convenience on some server-side code.
Public Delegate Sub ReceiveMessageCallback(ByVal message As String, ByVal senderAlias As String)