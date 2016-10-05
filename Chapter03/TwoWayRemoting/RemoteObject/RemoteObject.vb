Public Class RemoteObject
    Inherits MarshalByRefObject

    Private WithEvents tmrCallback As New System.Timers.Timer()
    Private Callback As ConfirmationCallback
    Private Message As String

    Public Sub ReceiveMessage(ByVal message As String, ByVal callback As ConfirmationCallback)
        Console.WriteLine()
        Console.WriteLine("Received message: " & message)
        Me.Message = "Received message: " & message
        Me.Callback = callback
        tmrCallback.Interval = 5000
        tmrCallback.Start()
    End Sub

    Private Sub tmrCallback_Elapsed(ByVal sender As System.Object, _
     ByVal e As System.Timers.ElapsedEventArgs) _
     Handles tmrCallback.Elapsed

        Console.WriteLine("Sending Callback")
        Console.WriteLine()
        tmrCallback.Stop()
        Callback.Invoke(Message)

    End Sub

End Class

Public Delegate Sub ConfirmationCallback(ByVal message As String)

Public Class Listener
    Inherits MarshalByRefObject

    Public Event CallbackReceived(ByVal sender As Object, ByVal e As MessageEventArgs)

    Public Sub ConfirmationCallback(ByVal message As String)
        RaiseEvent CallbackReceived(Me, New MessageEventArgs(message))
    End Sub

    ' Ensures that this object will not be prematurely released.
    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

End Class

Public Class MessageEventArgs
    Inherits EventArgs

    Public Message As String

    Public Sub New(ByVal message As String)
        Me.Message = message
    End Sub

End Class
