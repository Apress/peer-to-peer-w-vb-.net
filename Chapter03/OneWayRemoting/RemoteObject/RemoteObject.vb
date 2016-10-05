Public Class RemoteObject
    Inherits MarshalByRefObject

    Public Sub ReceiveMessage(ByVal message As Message)
        Console.WriteLine()
        Console.WriteLine("Received message: " & message.Text)
    End Sub

End Class

<Serializable()> _
Public Class Message

    Public Text As String
    Public Sender As String

    Public Sub New(ByVal text As String, ByVal sender As String)
        Me.Text = text
        Me.Sender = sender
    End Sub

End Class
