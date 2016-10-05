Imports System.IO
Imports System.Runtime.Remoting
Imports TalkComponent

' The ClientProcess class does double duty. Any message the TalkClient
' wants to send to the TalkServer is sent through ClientProcess.
' Similarly, any callback sent from the TalkServer to the TalkClient
' is received by ClientProcess, which then raises a local event.
' ClientProcess inherits MarshalByRefObject because the
' server must be able to call its ReceiveMessage() method.
Public Class ClientProcess
    Inherits MarshalByRefObject
    Implements ITalkClient

    ' This event occurs when a message is received.
    ' It's used to transfer the message from the remotable
    ' ClientProcess object to the Talk form.
    Event MessageReceived(ByVal sender As Object, ByVal e As MessageReceivedEventArgs)

    ' The reference to the server object.
    ' (Technically, this really holds a proxy class.)
    Private Server As ITalkServer

    ' The user ID for this instance.
    Private _Alias As String
    Public Property [Alias]() As String
        Get
            Return _Alias
        End Get
        Set(ByVal Value As String)
            _Alias = Value
        End Set
    End Property

    Public Sub New(ByVal [alias] As String)
        _Alias = [alias]
    End Sub

    ' This override ensures that the if the object is idle for an extended 
    ' period, waiting for messages, it won't lose its lease and
    ' be garbage collected.
    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

    Public Sub Login()

        ' Configure the client channel for sending messages and receiving
        ' the server callback.
        RemotingConfiguration.Configure("TalkClient.exe.config")

        ' You could accomplish the same thing in code by uncommenting
        ' the following two lines:
        ' Dim Channel As New System.Runtime.Remoting.Channels.Tcp.TcpChannel(0)
        ' ChannelServices.RegisterChannel(Channel)

        ' Create the proxy that references the server object.
        Server = CType(Activator.GetObject(GetType(ITalkServer), "tcp://localhost:8000/TalkNET/TalkServer"), ITalkServer)

        ' Register the current user with the server.
        ' If the server is not running, or the URL or class information is
        ' incorrect, this is the most likely place for an error to occur.
        Server.AddUser(_Alias, Me)

    End Sub

    Public Sub LogOut()
        Server.RemoveUser(_Alias)
    End Sub

    Public Sub SendMessage(ByVal recipientAlias As String, ByVal message As String)
        Server.SendMessage(Me.Alias, recipientAlias, message)
    End Sub

    <System.Runtime.Remoting.Messaging.OneWay()> _
    Private Sub ReceiveMessage(ByVal message As String, ByVal senderAlias As String) Implements ITalkClient.ReceiveMessage
        RaiseEvent MessageReceived(Me, New MessageReceivedEventArgs(message, senderAlias))
    End Sub

    Public Function GetUsers() As ICollection
        Return Server.GetUsers
    End Function



    Private OfferedFiles As New Hashtable()

    Event FileOfferReceived(ByVal sender As Object, ByVal e As FileOfferReceivedEventArgs)

    Public Function SendFileOffer(ByVal recipientAlias As String, ByVal sourcePath As String)

        ' Retrive the reference to the other user.
        Dim peer As ITalkClient = Server.GetUser(recipientAlias)

        ' Create a GUID to identify the file, and add it to the collection.
        Dim fileIdentifier As Guid = Guid.NewGuid()
        OfferedFiles(fileIdentifier) = sourcePath

        ' Offer the file.
        peer.ReceiveFileOffer(Path.GetFileName(sourcePath), fileIdentifier, Me.Alias)

    End Function

    Private Sub ReceiveFileOffer(ByVal filename As String, ByVal fileIdentifier As System.Guid, ByVal senderAlias As String) Implements TalkComponent.ITalkClient.ReceiveFileOffer
        RaiseEvent FileOfferReceived(Me, New FileOfferReceivedEventArgs(filename, fileIdentifier, senderAlias))
    End Sub

    Public Sub AcceptFile(ByVal recipientAlias As String, ByVal fileIdentifier As Guid, ByVal destinationPath As String)

        ' Retrive the reference to the other user.
        Dim peer As ITalkClient = Server.GetUser(recipientAlias)

        ' Create an array to store the data.
        Dim FileData As Byte()

        ' Request the file.
        FileData = peer.TransferFile(fileIdentifier, Me.Alias)
        Dim fs As FileStream

        ' Create the local copy of the file in the desired location.
        ' This method does not bother to check if it is overwriting a file with the same name.
        fs = File.Create(destinationPath)
        fs.Write(FileData, 0, FileData.Length)

        ' Clean up.
        fs.Close()

    End Sub

    Private Function TransferFile(ByVal fileIdentifier As System.Guid, ByVal senderAlias As String) As Byte() Implements TalkComponent.ITalkClient.TransferFile

        ' Ensure that the GUID corresponds to a valid file offer.
        If Not OfferedFiles.Contains(fileIdentifier) Then
            Throw New ApplicationException("This file is no longer available from the client.")
        End If

        ' Lookup the file path from the OfferedFiles collection, and open it.
        Dim fs As FileStream
        fs = File.Open(OfferedFiles(fileIdentifier), FileMode.Open)

        ' Fill the FileData byte array with the data from the file.
        Dim FileData As Byte()
        ReDim FileData(fs.Length)
        fs.Read(FileData, 0, FileData.Length)

        ' Remove the offered file from the collection.
        OfferedFiles.Remove(fileIdentifier)

        ' Clean up.
        fs.Close()

        ' Transmit the file data.
        Return FileData

    End Function
End Class

' Used for the MessageReceived event.
' This event transfers the message from the remotable
' ClientProcess object to the Talk form.
Public Class MessageReceivedEventArgs
    Inherits EventArgs

    Public Message As String
    Public SenderAlias As String

    Public Sub New(ByVal message As String, ByVal senderAlias As String)
        Me.Message = message
        Me.SenderAlias = senderAlias
    End Sub

End Class

Public Class FileOfferReceivedEventArgs
    Inherits EventArgs

    Public Filename As String
    Public FileIdentifier As Guid
    Public SenderAlias As String

    Public Sub New(ByVal filename As String, ByVal fileIdentifier As Guid, ByVal senderAlias As String)
        Me.Filename = filename
        Me.FileIdentifier = fileIdentifier
        Me.SenderAlias = senderAlias
    End Sub

End Class
