Imports FileSwapper.localhost

Public Class SwapperClient
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents pnlState As System.Windows.Forms.StatusBarPanel
    Friend WithEvents txtSharePath As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtKeywords As System.Windows.Forms.TextBox
    Friend WithEvents lstSearchResults As System.Windows.Forms.ListView
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents lstUploads As System.Windows.Forms.ListView
    Friend WithEvents chkMP3Only As System.Windows.Forms.CheckBox
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents Filename As System.Windows.Forms.ColumnHeader
    Friend WithEvents IP As System.Windows.Forms.ColumnHeader
    Friend WithEvents Ping As System.Windows.Forms.ColumnHeader
    Friend WithEvents CreatedDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents Port As System.Windows.Forms.ColumnHeader
    Friend WithEvents tmrRefreshRegistration As System.Windows.Forms.Timer
    Friend WithEvents FileGUID As System.Windows.Forms.ColumnHeader
    Friend WithEvents PeerGUID As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDownloads As System.Windows.Forms.TextBox
    Friend WithEvents txtUploads As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents File As System.Windows.Forms.ColumnHeader
    Friend WithEvents Progress As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstDownloads As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbPages As System.Windows.Forms.TabControl
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tbPages = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtKeywords = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.lstSearchResults = New System.Windows.Forms.ListView()
        Me.Filename = New System.Windows.Forms.ColumnHeader()
        Me.Ping = New System.Windows.Forms.ColumnHeader()
        Me.CreatedDate = New System.Windows.Forms.ColumnHeader()
        Me.IP = New System.Windows.Forms.ColumnHeader()
        Me.Port = New System.Windows.Forms.ColumnHeader()
        Me.FileGUID = New System.Windows.Forms.ColumnHeader()
        Me.PeerGUID = New System.Windows.Forms.ColumnHeader()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lstDownloads = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.lstUploads = New System.Windows.Forms.ListView()
        Me.File = New System.Windows.Forms.ColumnHeader()
        Me.Progress = New System.Windows.Forms.ColumnHeader()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtUploads = New System.Windows.Forms.TextBox()
        Me.txtDownloads = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkMP3Only = New System.Windows.Forms.CheckBox()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtSharePath = New System.Windows.Forms.TextBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.pnlState = New System.Windows.Forms.StatusBarPanel()
        Me.tmrRefreshRegistration = New System.Windows.Forms.Timer(Me.components)
        Me.tbPages.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.pnlState, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbPages
        '
        Me.tbPages.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.tbPages.Controls.AddRange(New System.Windows.Forms.Control() {Me.TabPage1, Me.TabPage2, Me.TabPage4, Me.TabPage3})
        Me.tbPages.Location = New System.Drawing.Point(12, 12)
        Me.tbPages.Name = "tbPages"
        Me.tbPages.SelectedIndex = 0
        Me.tbPages.Size = New System.Drawing.Size(644, 328)
        Me.tbPages.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtKeywords, Me.Label3, Me.cmdSearch, Me.lstSearchResults})
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(636, 302)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Search"
        '
        'txtKeywords
        '
        Me.txtKeywords.Location = New System.Drawing.Point(84, 16)
        Me.txtKeywords.Name = "txtKeywords"
        Me.txtKeywords.Size = New System.Drawing.Size(328, 21)
        Me.txtKeywords.TabIndex = 7
        Me.txtKeywords.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Search For:"
        '
        'cmdSearch
        '
        Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdSearch.Location = New System.Drawing.Point(420, 15)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(60, 24)
        Me.cmdSearch.TabIndex = 5
        Me.cmdSearch.Text = "Search"
        '
        'lstSearchResults
        '
        Me.lstSearchResults.Activation = System.Windows.Forms.ItemActivation.TwoClick
        Me.lstSearchResults.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Filename, Me.Ping, Me.CreatedDate, Me.IP, Me.Port, Me.FileGUID, Me.PeerGUID})
        Me.lstSearchResults.FullRowSelect = True
        Me.lstSearchResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstSearchResults.Location = New System.Drawing.Point(16, 52)
        Me.lstSearchResults.MultiSelect = False
        Me.lstSearchResults.Name = "lstSearchResults"
        Me.lstSearchResults.Size = New System.Drawing.Size(612, 240)
        Me.lstSearchResults.TabIndex = 0
        Me.lstSearchResults.View = System.Windows.Forms.View.Details
        '
        'Filename
        '
        Me.Filename.Text = "Filename"
        Me.Filename.Width = 150
        '
        'Ping
        '
        Me.Ping.Text = "Ping"
        Me.Ping.Width = 100
        '
        'CreatedDate
        '
        Me.CreatedDate.Text = "File Created"
        Me.CreatedDate.Width = 100
        '
        'IP
        '
        Me.IP.Text = "IP"
        Me.IP.Width = 100
        '
        'Port
        '
        Me.Port.Text = "Port"
        '
        'FileGUID
        '
        Me.FileGUID.Text = "File GUID"
        Me.FileGUID.Width = 100
        '
        'PeerGUID
        '
        Me.PeerGUID.Text = "Peer GUID"
        Me.PeerGUID.Width = 100
        '
        'TabPage2
        '
        Me.TabPage2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lstDownloads})
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(636, 302)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Downloads"
        '
        'lstDownloads
        '
        Me.lstDownloads.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstDownloads.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lstDownloads.FullRowSelect = True
        Me.lstDownloads.Location = New System.Drawing.Point(12, 13)
        Me.lstDownloads.Name = "lstDownloads"
        Me.lstDownloads.Size = New System.Drawing.Size(612, 276)
        Me.lstDownloads.TabIndex = 3
        Me.lstDownloads.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "File"
        Me.ColumnHeader1.Width = 200
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Progress"
        Me.ColumnHeader2.Width = 200
        '
        'TabPage4
        '
        Me.TabPage4.Controls.AddRange(New System.Windows.Forms.Control() {Me.lstUploads})
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(636, 302)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Uploads"
        '
        'lstUploads
        '
        Me.lstUploads.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstUploads.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.File, Me.Progress})
        Me.lstUploads.FullRowSelect = True
        Me.lstUploads.Location = New System.Drawing.Point(12, 13)
        Me.lstUploads.Name = "lstUploads"
        Me.lstUploads.Size = New System.Drawing.Size(612, 276)
        Me.lstUploads.TabIndex = 2
        Me.lstUploads.View = System.Windows.Forms.View.Details
        '
        'File
        '
        Me.File.Text = "File"
        Me.File.Width = 200
        '
        'Progress
        '
        Me.Progress.Text = "Progress"
        Me.Progress.Width = 200
        '
        'TabPage3
        '
        Me.TabPage3.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label6, Me.Label5, Me.txtUploads, Me.txtDownloads, Me.Label4, Me.chkMP3Only, Me.cmdUpdate, Me.Label2, Me.Label1, Me.txtPort, Me.txtSharePath})
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(636, 302)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Options"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(300, 132)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(268, 108)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Changes to the Port settings will not take effect until you restart the applicati" & _
        "on."
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 116)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Max Uploads:"
        '
        'txtUploads
        '
        Me.txtUploads.Location = New System.Drawing.Point(100, 112)
        Me.txtUploads.Name = "txtUploads"
        Me.txtUploads.Size = New System.Drawing.Size(88, 21)
        Me.txtUploads.TabIndex = 8
        Me.txtUploads.Text = ""
        '
        'txtDownloads
        '
        Me.txtDownloads.Location = New System.Drawing.Point(100, 80)
        Me.txtDownloads.Name = "txtDownloads"
        Me.txtDownloads.Size = New System.Drawing.Size(88, 21)
        Me.txtDownloads.TabIndex = 7
        Me.txtDownloads.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Max Downloads:"
        '
        'chkMP3Only
        '
        Me.chkMP3Only.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkMP3Only.Location = New System.Drawing.Point(12, 148)
        Me.chkMP3Only.Name = "chkMP3Only"
        Me.chkMP3Only.Size = New System.Drawing.Size(164, 24)
        Me.chkMP3Only.TabIndex = 5
        Me.chkMP3Only.Text = "Only Share MP3 Files"
        '
        'cmdUpdate
        '
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdUpdate.Location = New System.Drawing.Point(12, 188)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(60, 24)
        Me.cmdUpdate.TabIndex = 4
        Me.cmdUpdate.Text = "Update"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Use Port:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Share Files In:"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(100, 48)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(88, 21)
        Me.txtPort.TabIndex = 1
        Me.txtPort.Text = ""
        '
        'txtSharePath
        '
        Me.txtSharePath.Location = New System.Drawing.Point(100, 16)
        Me.txtSharePath.Name = "txtSharePath"
        Me.txtSharePath.Size = New System.Drawing.Size(388, 21)
        Me.txtSharePath.TabIndex = 0
        Me.txtSharePath.Text = ""
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdExit.Location = New System.Drawing.Point(580, 348)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 28)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 392)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.pnlState})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(668, 22)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 2
        Me.StatusBar1.Text = "StatusBar1"
        '
        'pnlState
        '
        Me.pnlState.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.pnlState.Text = "Not logged in."
        Me.pnlState.Width = 668
        '
        'tmrRefreshRegistration
        '
        Me.tmrRefreshRegistration.Interval = 300000
        '
        'SwapperClient
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(668, 414)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.StatusBar1, Me.cmdExit, Me.tbPages})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "SwapperClient"
        Me.Text = "FileSwapper Peer"
        Me.tbPages.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.pnlState, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub SwapperClient_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Show()
        Me.Refresh()

        ' Read registry.
        Global.Settings.Load()
        txtSharePath.Text = Global.Settings.SharePath
        txtPort.Text = Global.Settings.Port
        chkMP3Only.Checked = Global.Settings.ShareMP3Only
        txtUploads.Text = Global.Settings.MaxUploadThreads
        txtDownloads.Text = Global.Settings.MaxDownloadThreads

        ' Create the search, download, and upload objects.
        ' They will create their own threads.
        App.SearchThread = New Search(lstSearchResults)
        App.DownwnloadThread = New FileDownloadQueue(lstDownloads)
        App.UploadThread = New FileServer(lstUploads)
        App.UploadThread.StartWaitForRequest()


        Global.Identity.Port = Global.Settings.Port
        DoLogin()
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledException

    End Sub

    Private Sub DoLogin()

        Me.Cursor = Cursors.WaitCursor

        ' Log in.
        pnlState.Text = "Trying to log in."
        App.Login()
        If Not Global.LoggedIn Then
            pnlState.Text = "Not logged in."
            Me.Cursor = Cursors.Default
            Return
        End If

        ' Submit list of files.
        pnlState.Text = "Sending file information..."
        If App.PublishFiles() Then
            pnlState.Text = "File list published to server."
        Else
            pnlState.Text = "Could not publish file list."
        End If

        ' Refresh login information every five minutes.
        tmrRefreshRegistration.Start()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub SwapperClient_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        App.Logout()
        App.DownwnloadThread.Abort()
        App.SearchThread.Abort()
        App.UploadThread.Abort()

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        If App.SearchThread.Searching Then
            App.SearchThread.Abort()
        End If

        App.SearchThread.StartSearch(txtKeywords.Text)

    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

        Global.Settings.Port = Val(txtPort.Text)
        Global.Settings.SharePath = txtSharePath.Text
        Global.Settings.ShareMP3Only = chkMP3Only.Checked
        Global.Settings.MaxDownloadThreads = Val(txtDownloads.Text)
        Global.Settings.MaxUploadThreads = Val(txtUploads.Text)

        Global.Settings.Save()

        ' Log back in.
        App.Logout()
        Global.Identity.Port = Global.Settings.Port
        DoLogin()

    End Sub

    Private Sub tmRefreshRegistration_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefreshRegistration.Tick

        ' Fires every five minutes to update registration.
        App.RefreshLogin()
        ' Currently no steps are taken to refresh subscribed file list.

    End Sub


    Private Sub lstSearchResults_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSearchResults.ItemActivate
        Dim File As SharedFile
        File = CType(CType(sender, ListView).SelectedItems(0).Tag, SharedFile)

        If App.DownwnloadThread.CheckForFile(File) Then
            MessageBox.Show("You are already downloading this file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'ElseIf File.Peer.Guid.ToString() = Global.Identity.Guid.ToString() Then
            'MessageBox.Show("This is a local file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            App.DownwnloadThread.AddFile(File)
            If Not App.DownwnloadThread.Working Then
                App.DownwnloadThread.StartAllocateWork()
            End If
            tbPages.SelectedTab = tbPages.TabPages(1)
        End If

    End Sub


    Public Sub UnhandledException(ByVal sender As Object, _
      ByVal e As UnhandledExceptionEventArgs)

        ' Log the error.
        Trace.Write(e.ExceptionObject.ToString())

        ' Log out of the discovery service.
        App.Logout()

    End Sub

End Class
