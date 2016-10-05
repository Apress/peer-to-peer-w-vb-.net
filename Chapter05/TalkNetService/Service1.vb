Imports System.ServiceProcess
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels

Public Class TalkNetService
    Inherits System.ServiceProcess.ServiceBase

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        ' (The code for all design-time property configuration appears here.)
        Me.ServiceName = "Talk .NET Service"
    End Sub

    <MTAThread()> _
    Shared Sub Main()
        ServiceBase.Run(New TalkNetService())
    End Sub

    ' Register the listening channels.
    Protected Overrides Sub OnStart(ByVal args() As String)
        RemotingConfiguration.Configure("C:\Code\P2P\Chapter05\TalkNetService\Bin\TalkNetService.exe.config")
    End Sub

    ' Remove all the listening channels.
    Protected Overrides Sub OnStop()
        Dim Channel As IChannel
        For Each Channel In ChannelServices.RegisteredChannels()
            Try
                ChannelServices.UnregisterChannel(Channel)
            Catch
            End Try
        Next
    End Sub

End Class
