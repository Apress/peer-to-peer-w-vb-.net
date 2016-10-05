Imports CryptoComponent

Public Interface ITalkClient

    ' The server calls this to forward a message to the appropriate client.
    Sub ReceiveMessage(ByVal encryptedMessage As EncryptedObject)

End Interface

Public Delegate Sub ReceiveMessageCallback(ByVal encryptedMessage As EncryptedObject)
<Serializable()> _
Public Class Message
    Public SenderAlias As String
    Public MessageBody As String

    Public Sub New(ByVal sender As String, ByVal body As String)
        Me.SenderAlias = sender
        Me.MessageBody = body
    End Sub
End Class