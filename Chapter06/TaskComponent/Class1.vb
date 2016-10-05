Public Interface IGenericTask
    Function DoTask(ByVal inputData() As Byte) As Byte()
End Interface

<Serializable()> _
Public Class TaskRequest
    'Public Client As ITaskRequester
    Public InputData() As Byte
    Public OutputData() As Byte
End Class

