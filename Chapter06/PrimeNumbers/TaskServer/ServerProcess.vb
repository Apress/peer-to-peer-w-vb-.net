Imports TaskComponent
Imports System.Threading
Imports System.Configuration

' The ServerProcess tracks all workers and tasks.
Public Class ServerProcess
    Inherits MarshalByRefObject
    Implements ITaskServer

    ' Tracks a collection of WorkerRecord objects.
    ' This contains information about workers, their status,
    ' and the "network pointer" needed to communicate with them.
    ' The collection is indexed by WorkerID.
    Private Workers As New Hashtable()

    ' Tracks Task objects, and their status.
    ' The collection is indexed by TaskID.
    ' You could use custom collections for more fail-safe code.
    Private Tasks As New Hashtable()

    ' The object used for monitoring tasks (old tasks are cancelled).
    'Private TaskMonitor As New TaskMonitor(Me)

    ' The thread where the task monitoring takes place.
    'Private MonitorThread As New Thread(AddressOf TaskMonitor.Monitor)

    ' The maximum number of workers that will be assigned to a task.
    ' This helps to ensure that other workers will be free to serve new
    ' requests
    Private MaxWorkers As Integer

    Public Sub New()

        MyBase.New()

        ' Retrieve configuration settings.
        MaxWorkers = Int32.Parse(ConfigurationSettings.AppSettings("MaxWorkers"))

        'MonitorThread.IsBackground = True
        'MonitorThread.Start()

    End Sub

    Public Function AddWorker(ByVal callback As TaskComponent.ITaskWorker) As System.Guid Implements TaskComponent.ITaskServer.AddWorker

        Dim Worker As New WorkerRecord(callback)

        SyncLock Workers
            Workers(Worker.WorkerID) = Worker
        End SyncLock
        Trace.Write("Added worker " & Worker.WorkerID.ToString())

        Return Worker.WorkerID

    End Function

    Public Sub RemoveWorker(ByVal workerID As System.Guid) Implements TaskComponent.ITaskServer.RemoveWorker

        SyncLock Workers
            Workers.Remove(workerID)
        End SyncLock
        Trace.Write("Removed worker " & workerID.ToString())

        ' We assume that the worker has properly cancelled any outstanding
        ' tasks before exiting. Otherwise, you could add cleanup code
        ' here to check that the TaskSegment is assigned.

    End Sub

    '! receive error
    '<System.Runtime.Remoting.Messaging.OneWay()> _
    Public Function SubmitTask(ByVal taskRequest As TaskComponent.TaskRequest) As Guid Implements TaskComponent.ITaskServer.SubmitTask

        ' Validate task request.
        If taskRequest.FromNumber > taskRequest.ToNumber Then
            Throw New ArgumentException("First number must be smaller than the second.")
        End If

        ' Calcualte if the task can benefit from parallelism.
        Dim TotalRange As Integer = taskRequest.ToNumber - taskRequest.FromNumber
        Dim MaxWorkersForTask As Integer
        If TotalRange < 10000 Then
            MaxWorkersForTask = 1
        Else
            MaxWorkersForTask = MaxWorkers
        End If

        ' Create the task.
        Dim Task As New Task(taskRequest)
        Dim Worker As WorkerRecord

        ' This ensures that the server won't try to allocate two different tasks
        ' to the same worker if the requests arrive simultaneously.
        SyncLock Workers

            ' Try to find workers for this task.
            Dim Item As DictionaryEntry
            For Each Item In Workers
                Worker = CType(Item.Value, WorkerRecord)
                If Not Worker.TaskAssigned Then
                    Worker.TaskAssigned = True
                    Task.Workers.Add(Worker)
                End If
                If Task.Workers.Count >= MaxWorkersForTask Then Exit For
            Next

        End SyncLock

        ' If there are no available workers, an exception is thrown.
        ' An alternative might be to queue the task, and use a continuously
        ' running thread to try to assign it.
        If Task.Workers.Count = 0 Then
            Throw New ApplicationException("No free workers. Try again later.")
        End If

        Trace.Write("Trying to assign " & Task.Workers.Count.ToString() & " worker(s) for task " & Task.TaskID.ToString() & ".")

        ' Calculate segment sizes.
        Dim Segment As TaskSegment
        Dim LowerBound As Integer = taskRequest.FromNumber
        Dim AverageRange As Integer = Math.Floor(TotalRange / Task.Workers.Count)
        Dim i As Integer

        ' Divide the task into segments, and dispatch each segment.
        ' This code will be skipped if there is only segment, because
        ' (WorkersToUse.Count - 2) will equal 0.
        Dim ReceiveTask As ReceiveTaskDelegate
        For i = 0 To Task.Workers.Count - 2
            Segment = New TaskSegment(Task.TaskID, LowerBound, LowerBound + AverageRange, i)
            LowerBound += AverageRange + 1
            Worker = CType(Task.Workers(i), WorkerRecord)
            Segment.WorkerID = Worker.WorkerID
            'Worker.ITaskWorker.ReceiveTask(Segment)
            ReceiveTask = New ReceiveTaskDelegate(AddressOf Worker.ITaskWorker.ReceiveTask)
            ReceiveTask.BeginInvoke(Segment, Nothing, Nothing)
        Next

        ' Create the last segment to get the remaining numbers.
        Segment = New TaskSegment(Task.TaskID, LowerBound, taskRequest.ToNumber, i)
        Worker = CType(Task.Workers(Task.Workers.Count - 1), WorkerRecord)
        Segment.WorkerID = Worker.WorkerID

        ReceiveTask = New ReceiveTaskDelegate(AddressOf Worker.ITaskWorker.ReceiveTask)
        ReceiveTask.BeginInvoke(Segment, Nothing, Nothing)
        'Worker.ITaskWorker.ReceiveTask(Segment)

        ' Store the Task object.
        SyncLock Tasks
            Tasks.Add(Task.TaskID, Task)
        End SyncLock

        Trace.Write("Created and assigned task " & Task.TaskID.ToString() & ".")

    End Function

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Public Sub ReceiveTaskComplete(ByVal taskSegment As TaskSegment, ByVal workerID As System.Guid) Implements TaskComponent.ITaskServer.ReceiveTaskComplete
        Trace.Write("Received result sequence #" & taskSegment.SequenceNumber.ToString() & " for task " & taskSegment.TaskID.ToString() & ".")

        Dim Task As Task = CType(Tasks(taskSegment.TaskID), Task)
        Task.Results.Add(taskSegment.SequenceNumber, taskSegment.Primes)

        ' Free up worker.
        Dim Worker As WorkerRecord = CType(Workers(taskSegment.WorkerID), WorkerRecord)
        Worker.TaskAssigned = False

        ' Check if this is the final submission.
        If Task.Results.Count = Task.Workers.Count Then

            SyncLock Tasks
                Trace.Write("Task " & Task.TaskID.ToString() & " completed.")
                ' (Add error handling.)
                Dim Primes() As Integer = Task.GetJoinedResults()
                Dim Results As New TaskResults(Task.Request.FromNumber, Task.Request.ToNumber, Primes)
                Dim ReceiveResults As New ReceiveResultsDelegate(AddressOf Task.Request.Client.ReceiveResults)
                ReceiveResults.BeginInvoke(Results, Nothing, Nothing)
                'Task.Request.Client.ReceiveResults(Results)

                ' Remove task.
                Tasks.Remove(Task.TaskID)
            End SyncLock
        End If

    End Sub


End Class
