Imports TalkComponent
Imports System.Threading


' The ServerProcess receives calls from all clients. It tracks
' users in a collection, and reroutes messages to them as needed.
Public Class ServerProcess
    Inherits MarshalByRefObject
    Implements ITalkServer

    ' Tracks all the user aliases, and the "network pointer" needed
    ' to communicate with them.
    Private _ActiveUsers As New Hashtable()

    Public Function GetUsers() As System.Collections.ICollection Implements TalkComponent.ITalkServer.GetUsers
        Return _ActiveUsers.Keys
    End Function

    Public Sub AddUser(ByVal [alias] As String, ByVal client As ITalkClient) Implements TalkComponent.ITalkServer.AddUser
        Trace.Write("Added user '" & [alias] & "'")
        Dim SynchronizedCollection As Hashtable = Hashtable.Synchronized(_ActiveUsers)
        SynchronizedCollection([alias]) = client
    End Sub

    Public Sub RemoveUser(ByVal [alias] As String) Implements TalkComponent.ITalkServer.RemoveUser
        Trace.Write("Removed user '" & [alias] & "'")
        Dim SynchronizedCollection As Hashtable = Hashtable.Synchronized(_ActiveUsers)
        SynchronizedCollection.Remove([alias])
    End Sub

    Public Function GetUser(ByVal [alias] As String) As TalkComponent.ITalkClient Implements TalkComponent.ITalkServer.GetUser
        Return _ActiveUsers([alias])
    End Function
End Class

