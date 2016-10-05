Public Class Startup
    Inherits System.ComponentModel.Component
    Friend WithEvents mnuContext As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuShowStatus As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSeparator As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem

    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Private components As System.ComponentModel.IContainer

    Public Sub New()
        frm.Client = Client
        InitializeComponent()

        TrayIcon.Visible = True

        ' Create the new remotable client object.
        Client.Login()

        System.Windows.Forms.Application.Run(frm)
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Startup))
        Me.mnuContext = New System.Windows.Forms.ContextMenu()
        Me.mnuShowStatus = New System.Windows.Forms.MenuItem()
        Me.mnuSeparator = New System.Windows.Forms.MenuItem()
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        '
        'mnuContext
        '
        Me.mnuContext.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuShowStatus, Me.mnuSeparator, Me.mnuExit})
        '
        'mnuShowStatus
        '
        Me.mnuShowStatus.Index = 0
        Me.mnuShowStatus.Text = "Show Status"
        '
        'mnuSeparator
        '
        Me.mnuSeparator.Index = 1
        Me.mnuSeparator.Text = "-"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 2
        Me.mnuExit.Text = "Exit"
        '
        'TrayIcon
        '
        Me.TrayIcon.ContextMenu = Me.mnuContext
        Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
        Me.TrayIcon.Text = "Task Worker"
        Me.TrayIcon.Visible = True

    End Sub



    Private Client As New ClientProcess()

    Public Shared Sub Main()

        ' (Create a system tray menu.)
        Dim Startup As New Startup()


    End Sub

    ' Create the client form.
    Private frm As New MainForm()
    Private Sub mnuShowStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuShowStatus.Click
        frm.Show()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click

        If Client.Status = BackgroundStatus.Processing Then
            MessageBox.Show("A background task is still in progress.", "Cannot Exit")
        Else
            Try
                Client.LogOut()
            Catch
            End Try

            ' Clear it manually. Otherwise, it may linger until the user moves the mouse over it.
            TrayIcon.Visible = False
            System.Windows.Forms.Application.Exit()
        End If
    End Sub
End Class
