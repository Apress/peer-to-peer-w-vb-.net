Imports TaskComponent

Public Class Task

    Private _TaskID As Guid = Guid.NewGuid()

    ' The original task information.
    Private _Request As TaskRequest

    ' Holds WorkerRecord objects.
    Private _WorkersInProgress As New ArrayList()

    ' Holds partial prime lists, indexed by sequence number.
    Private _TaskResults As New Hashtable()

    Public ReadOnly Property TaskID() As Guid
        Get
            Return _TaskID
        End Get
    End Property

    Public ReadOnly Property Request() As TaskRequest
        Get
            Return _Request
        End Get
    End Property

    Public Property Workers() As ArrayList
        Get
            Return _WorkersInProgress
        End Get
        Set(ByVal Value As ArrayList)
            _WorkersInProgress = Value
        End Set
    End Property

    Public Property Results() As Hashtable
        Get
            Return _TaskResults
        End Get
        Set(ByVal Value As Hashtable)
            _TaskResults = Value
        End Set
    End Property

    Public Function GetJoinedResults() As Integer()

        ' Count the number of primes.
        Dim NumberOfPrimes As Integer
        Dim SegmentResults() As Integer
        Dim i As Integer
        For i = 0 To _TaskResults.Count - 1
            SegmentResults = CType(_TaskResults(i), Integer())
            NumberOfPrimes += SegmentResults.Length
        Next
        ' Create the whole array.
        Dim Results(NumberOfPrimes - 1) As Integer

        ' Combine the partial results, in order.
        Dim Pos As Integer

        For i = 0 To _TaskResults.Count - 1
            SegmentResults = CType(_TaskResults(i), Integer())
            SegmentResults.CopyTo(Results, Pos)
            Pos += SegmentResults.Length
        Next

        Return Results

    End Function

    Public Sub New(ByVal taskRequest As TaskRequest)
        _Request = taskRequest
    End Sub

End Class


Public Class WorkerRecord

    Private _WorkerID As Guid = Guid.NewGuid()
    Private _WorkerReference As ITaskWorker
    Private _TaskAssigned As Boolean = False

    Public ReadOnly Property WorkerID() As Guid
        Get
            Return _WorkerID
        End Get
    End Property

    Public ReadOnly Property ITaskWorker() As ITaskWorker
        Get
            Return _WorkerReference
        End Get
    End Property

    Public Property TaskAssigned() As Boolean
        Get
            Return _TaskAssigned
        End Get
        Set(ByVal Value As Boolean)
            _TaskAssigned = Value
        End Set
    End Property

    Public Sub New(ByVal worker As ITaskWorker)
        _WorkerReference = worker
    End Sub

End Class


