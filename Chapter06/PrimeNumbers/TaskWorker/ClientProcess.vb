Imports System.IO
Imports System.Runtime.Remoting
Imports TaskComponent

' The ClientProcess class works both as a task worker (by implementing
' ITaskWorker) and as a TaskRequester (by implementing ITaskRequester).
' The client can use the ITaskRequester functionality to start a task by
' calling a method. Events are fired to notify the client of changing
' status, and received messages.
Public Class ClientProcess
    Inherits MarshalByRefObject
    Implements ITaskWorker, ITaskRequester

    ' This event occurs when work begins or ends on the background thread.
    Public Event BackgroundStatusChanged(ByVal sender As Object, ByVal e As BackgroundStatusChanged)

    ' This event occurs when the prime number series is received (answer to a query).
    Public Event ResultsReceived(ByVal sender As Object, ByVal e As ResultsReceivedEventArgs)

    ' The reference to the server object.
    ' (Technically, this really holds a proxy class.)
    Private Server As ITaskServer

    Private _ID As Guid
    Public ReadOnly Property ID() As Guid
        Get
            Return _ID
        End Get
    End Property

    Private _Status As BackgroundStatus = BackgroundStatus.Idle
    Public ReadOnly Property Status() As BackgroundStatus
        Get
            Return _Status
        End Get
    End Property

    Public Sub New()

        ' Configure the client channel for sending messages and receiving
        ' the server callback.
        RemotingConfiguration.Configure("TaskWorker.exe.config")

        ' Create the proxy that references the server object.
        Server = CType(Activator.GetObject(GetType(ITaskServer), "tcp://localhost:8000/WorkManager/TaskServer"), ITaskServer)

    End Sub

    Public Sub Login()
        ' Register the current worker with the server.
        _ID = Server.AddWorker(Me)
    End Sub

    Public Sub LogOut()
        Server.RemoveWorker(ID)
    End Sub

    ' This override ensures that the if the object is idle for an extended 
    ' period, waiting for messages, it won't lose its lease and
    ' be garbage collected.
    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

    Public Sub FindPrimes(ByVal fromNumber As Integer, ByVal toNumber As Integer)
        Server.SubmitTask(New TaskRequest(Me, fromNumber, toNumber))
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Public Sub ReceiveTask(ByVal task As TaskComponent.TaskSegment) Implements TaskComponent.ITaskWorker.ReceiveTask
        _Status = BackgroundStatus.Processing
        ' Raise an event to altert the form that the background thread is processing.
        RaiseEvent BackgroundStatusChanged(Me, New BackgroundStatusChanged(BackgroundStatus.Processing))

        task.Primes = Erastothenes.FindPrimes(task.FromNumber, task.ToNumber)
        Server.ReceiveTaskComplete(task, ID)

        _Status = BackgroundStatus.Idle
        ' Raise an event to altert the form that the background thread is finished.
        RaiseEvent BackgroundStatusChanged(Me, New BackgroundStatusChanged(BackgroundStatus.Idle))

    End Sub

    Public Sub ReceiveResults(ByVal results As TaskComponent.TaskResults) Implements TaskComponent.ITaskRequester.ReceiveResults
        ' Raise an event to notify the form.
        RaiseEvent ResultsReceived(Me, New ResultsReceivedEventArgs(results.Primes))
    End Sub
End Class

Public Class ResultsReceivedEventArgs
    Inherits EventArgs

    Private _Primes() As Integer
    Public Property Primes() As Integer()
        Get
            Return _Primes
        End Get
        Set(ByVal Value As Integer())
            _Primes = Value
        End Set
    End Property

    Public Sub New(ByVal primes() As Integer)
        _Primes = primes
    End Sub

End Class

Public Enum BackgroundStatus
    Processing
    Idle
End Enum

Public Class BackgroundStatusChanged
    Inherits EventArgs

    Private _Status As BackgroundStatus
    Public Property Status() As BackgroundStatus
        Get
            Return _Status
        End Get
        Set(ByVal Value As BackgroundStatus)
            _Status = Value
        End Set
    End Property

    Public Sub New(ByVal status As BackgroundStatus)
        Me.Status = status
    End Sub
End Class

