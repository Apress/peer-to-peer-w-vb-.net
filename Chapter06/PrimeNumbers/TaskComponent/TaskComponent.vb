Public Interface ITaskServer

    ' These methods allow users to be registered and unregistered with the server.
    Function AddWorker(ByVal callback As ITaskWorker) As Guid
    Sub RemoveWorker(ByVal workerID As Guid)

    ' This method is called to send a task complete notification.
    ' In this example, no data is returned directly. Instead, the Task 
    ' object contians the information about where to store the result.
    Sub ReceiveTaskComplete(ByVal taskSegment As TaskSegment, ByVal workerID As Guid)

    ' This method can be called if the task worker needs to shutdown.
    'Sub ReceiveTaskCancel(ByVal taskID As Guid, ByVal workerID As Guid)

    ' This method is used to register a task.
    Function SubmitTask(ByVal taskRequest As TaskRequest) As Guid

End Interface

Public Delegate Sub ReceiveTaskDelegate(ByVal taskSegment As TaskSegment)
Public Delegate Sub ReceiveResultsDelegate(ByVal results As TaskResults)

Public Interface ITaskWorker

    ' The server calls this to submit a task to a client.
    Sub ReceiveTask(ByVal task As TaskSegment)

    ' The server calls this to verify a task is still being processed.
    'Function CheckTaskRunning(ByVal taskID As Guid) As Boolean

    ' The server could call this method to cancel task processing.
    'Sub CancelTask(ByVal taskID As Guid)

End Interface

Public Interface ITaskRequester

    Sub ReceiveResults(ByVal results As TaskResults)

End Interface


<Serializable()> _
Public Class TaskRequest

    Public Client As ITaskRequester
    Public FromNumber As Integer
    Public ToNumber As Integer

    Public Sub New(ByVal client As ITaskRequester, ByVal fromNumber As Integer, ByVal toNumber As Integer)
        Me.Client = client
        Me.FromNumber = fromNumber
        Me.ToNumber = toNumber
    End Sub

    Public Sub New()
        ' Default constructor.
    End Sub

End Class


<Serializable()> _
Public Class TaskSegment

    Public TaskID As Guid
    Public SequenceNumber As Integer
    Public FromNumber As Integer
    Public ToNumber As Integer
    Public WorkerID As Guid

    ' This holds the task results.
    Public Primes() As Integer

    Public Sub New(ByVal taskID As Guid, ByVal fromNumber As Integer, ByVal toNumber As Integer, ByVal sequenceNumber As Integer)
        Me.TaskID = taskID
        Me.FromNumber = fromNumber
        Me.ToNumber = toNumber
        Me.SequenceNumber = sequenceNumber
        Me.WorkerID = WorkerID
    End Sub

    Public Sub New()
        ' Default constructor.
    End Sub

End Class

<Serializable()> _
Public Class TaskResults

    Public Primes() As Integer
    Public FromNumber As Integer
    Public ToNumber As Integer

    Public Sub New(ByVal fromNumber As Integer, ByVal toNumber As Integer, ByVal results() As Integer)
        Me.Primes = results
        Me.FromNumber = fromNumber
        Me.ToNumber = toNumber
    End Sub

    Public Sub New()
        ' Default constructor.
    End Sub

End Class


