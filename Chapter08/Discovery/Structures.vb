Public Class Peer

    Public Guid As Guid
    Public IP As String
    Public Port As Integer

End Class

Public Class SharedFile

    Public Guid As Guid
    Public FileName As String
    Public FileCreated As Date
    Public Peer As New Peer()
    Public Keywords() As String

End Class
