Imports System.Web.Services

<WebService(Namespace:="www.prosetech.com")> _
Public Class DiscoveryService
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    Private DB As New P2PDatabase()

    <WebMethod()> _
    Public Function Register(ByVal peer As Peer) As Boolean

        Try
            DB.AddPeer(peer)
            Return True
        Catch err As Exception
            Trace.Write(err.ToString)
            Return False
        End Try

    End Function

    <WebMethod()> _
    Public Function RefreshRegistration(ByVal peer As Peer) As Boolean

        Try
            DB.RefreshPeer(peer)
            Return True
        Catch err As Exception
            Trace.Write(err.ToString)
            Return False
        End Try

    End Function

    <WebMethod()> _
    Public Sub Unregister(ByVal peer As Peer)

        Try
            DB.DeletePeerAndFiles(peer)
        Catch err As Exception
            Trace.Write(err.ToString)
        End Try

    End Sub

    <WebMethod()> _
    Public Function PublishFiles(ByVal files() As SharedFile, ByVal peer As Peer) As Boolean

        Try
            DB.AddFileInfo(files, peer)
            Return True
        Catch err As Exception
            Trace.Write(err.ToString)
            Return False
        End Try

    End Function

    <WebMethod()> _
    Public Function SearchForFile(ByVal keywords() As String) As SharedFile()

        Try
            Return DB.GetFileInfo(keywords)
        Catch err As Exception
            Trace.Write(err.ToString)
            Dim EmptyArray() As SharedFile = {}
            Return EmptyArray
        End Try

    End Function


End Class
