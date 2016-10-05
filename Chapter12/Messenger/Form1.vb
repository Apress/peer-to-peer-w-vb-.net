Public Class Form1
    Inherits System.Windows.Forms.Form
    Implements MSNP.ISessionHandler

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstContacts As System.Windows.Forms.ListView
    Friend WithEvents cmdSend As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdStartSession As System.Windows.Forms.Button
    Friend WithEvents lblSession As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMessages As System.Windows.Forms.TextBox
    Friend WithEvents txtSend As System.Windows.Forms.TextBox
    Friend WithEvents cmdEndSession As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdEndSession = New System.Windows.Forms.Button()
        Me.lblSession = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstContacts = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader()
        Me.cmdStartSession = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSend = New System.Windows.Forms.TextBox()
        Me.txtMessages = New System.Windows.Forms.TextBox()
        Me.cmdSend = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdEndSession, Me.lblSession, Me.Label1, Me.lstContacts, Me.cmdStartSession})
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(568, 176)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Contacts"
        '
        'cmdEndSession
        '
        Me.cmdEndSession.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdEndSession.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdEndSession.Location = New System.Drawing.Point(464, 144)
        Me.cmdEndSession.Name = "cmdEndSession"
        Me.cmdEndSession.Size = New System.Drawing.Size(86, 24)
        Me.cmdEndSession.TabIndex = 8
        Me.cmdEndSession.Text = "End Session"
        '
        'lblSession
        '
        Me.lblSession.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblSession.Location = New System.Drawing.Point(112, 148)
        Me.lblSession.Name = "lblSession"
        Me.lblSession.Size = New System.Drawing.Size(164, 16)
        Me.lblSession.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.Label1.Location = New System.Drawing.Point(16, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Current Session:"
        '
        'lstContacts
        '
        Me.lstContacts.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstContacts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstContacts.HideSelection = False
        Me.lstContacts.Location = New System.Drawing.Point(12, 28)
        Me.lstContacts.MultiSelect = False
        Me.lstContacts.Name = "lstContacts"
        Me.lstContacts.Size = New System.Drawing.Size(540, 108)
        Me.lstContacts.TabIndex = 0
        Me.lstContacts.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Friendly Name"
        Me.ColumnHeader1.Width = 120
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "State"
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Substate"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "User Name"
        Me.ColumnHeader4.Width = 120
        '
        'cmdStartSession
        '
        Me.cmdStartSession.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdStartSession.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdStartSession.Location = New System.Drawing.Point(284, 144)
        Me.cmdStartSession.Name = "cmdStartSession"
        Me.cmdStartSession.Size = New System.Drawing.Size(172, 24)
        Me.cmdStartSession.TabIndex = 5
        Me.cmdStartSession.Text = "Start Session With Selected User"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label2, Me.txtSend, Me.txtMessages, Me.cmdSend})
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(8, 196)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(568, 188)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Messages"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Message:"
        '
        'txtSend
        '
        Me.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtSend.Location = New System.Drawing.Point(72, 20)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(416, 21)
        Me.txtSend.TabIndex = 3
        Me.txtSend.Text = ""
        '
        'txtMessages
        '
        Me.txtMessages.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtMessages.Location = New System.Drawing.Point(12, 60)
        Me.txtMessages.Multiline = True
        Me.txtMessages.Name = "txtMessages"
        Me.txtMessages.ReadOnly = True
        Me.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessages.Size = New System.Drawing.Size(540, 112)
        Me.txtMessages.TabIndex = 0
        Me.txtMessages.Text = ""
        '
        'cmdSend
        '
        Me.cmdSend.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdSend.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdSend.Location = New System.Drawing.Point(500, 18)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(56, 24)
        Me.cmdSend.TabIndex = 2
        Me.cmdSend.Text = "Send"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdClose.Location = New System.Drawing.Point(504, 400)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(76, 32)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "Close"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(592, 442)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdClose, Me.GroupBox2, Me.GroupBox1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Form1"
        Me.Text = "Messenger Test"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' The helper used to sign in and out, and retrieve contacts.
    Private Helper As MSNP.MSNPHelper

    ' These variables track the current session, and the related user.
    Private CurrentSessionUser As String
    Private CurrentSession As MSNP.Session

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Retrieve the IP address for the messenger server.
        Dim IP As String = System.Net.Dns.GetHostByName("messenger.hotmail.com").AddressList(0).ToString()
        Debug.Write(IP)

        ' Sign in. For simplicity's sake, a test user is hard-coded.
        ' Note that that communication is always performed on port 1863.
        ' The ISessionHandler class is the current form, so we pass that as a reference.
        Helper = New MSNP.MSNPHelper(IP, 1863, "mymsgtest@hotmail.com", "letmein", Me)
        Helper.Signin()

        RefreshContactList()
    End Sub

    Public Sub SessionEnded(ByVal session As MSNP.Session) Implements MSNP.ISessionHandler.SessionEnded
        If Not IsClosing Then
            Dim Updater As New UpdateControlText(lblSession)
            Updater.ReplaceText("")
        End If
        Me.CurrentSession = Nothing
    End Sub

    Public Sub SessionStarted(ByVal session As MSNP.Session) Implements MSNP.ISessionHandler.SessionStarted
        Dim Updater As New UpdateControlText(lblSession)
        Updater.ReplaceText(session.SessionIdentifier.ToString())
        Me.CurrentSession = session
    End Sub

    Public Sub ErrorReceived(ByVal session As MSNP.Session, ByVal errorDescription As String) Implements MSNP.ISessionHandler.ErrorReceived
        MessageBox.Show(errorDescription, "Error")
    End Sub

    Public Sub MessageReceived(ByVal session As MSNP.Session, ByVal message As MSNP.MimeMessage) Implements MSNP.ISessionHandler.MessageReceived
        ' Add text.
        If message.Body <> "" Then
            Dim Updater As New UpdateControlText(txtMessages)
            Dim NewText As String
            NewText = "FROM: " & message.SenderFriendlyName
            NewText &= Environment.NewLine
            NewText &= "RECEIVED: " & message.Body
            NewText &= Environment.NewLine & Environment.NewLine
            Updater.AddText(NewText)
        End If
    End Sub

    Public Sub UserDeparted(ByVal session As MSNP.Session, ByVal userHandle As String) Implements MSNP.ISessionHandler.UserDeparted
        ' Refresh the contact list.
        Dim Invoker As New MethodInvoker(AddressOf Me.RefreshContactList)
        Me.Invoke(Invoker)
    End Sub

    Public Sub UserJoined(ByVal session As MSNP.Session, ByVal userHandle As String, ByVal userFriendlyName As String) Implements MSNP.ISessionHandler.UserJoined
        ' Refresh the contact list.
        Dim Invoker As New MethodInvoker(AddressOf Me.RefreshContactList)
        Me.Invoke(Invoker)
    End Sub

    Private Sub RefreshContactList()
        ' Fill the contact list.
        ' If the user's friendly name has been changed from his or her email address,
        ' multiple entries may appear for the user. You can use additional code
        ' to ignore entries with duplicate UserName values.
        Dim Item As ListViewItem
        Dim Peer As MSNP.Contact
        For Each Peer In Me.Helper.FLContacts
            Item = lstContacts.Items.Add(Peer.FriendlyName)
            Item.SubItems.Add(Peer.State.ToString())
            Item.SubItems.Add(Peer.Substate.ToString())
            Item.SubItems.Add(Peer.UserName)
        Next
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not Me.CurrentSession Is Nothing Then
            'Me.CurrentSession.EndSession()
        End If
        Helper.Signout()
    End Sub

    
    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        If Me.CurrentSession Is Nothing Then
            MessageBox.Show("There is no current session.")
            Return
        Else
            Me.CurrentSession.SendMessage(txtSend.Text)
            Dim NewText As String
            NewText = "SENT: " & txtSend.Text
            NewText &= Environment.NewLine & Environment.NewLine
            txtMessages.Text &= NewText
        End If
    End Sub

    Private IsClosing As Boolean
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        IsClosing = True
        Me.Close()
    End Sub

    Private Sub cmdStartSession_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartSession.Click
        If Not Me.CurrentSession Is Nothing Then
            MessageBox.Show("There is already a current session.")
            Return
        Else
            If lstContacts.SelectedIndices.Count = 0 Then
                MessageBox.Show("No user is selected.")
                Return
            Else
                Dim Contact As String = lstContacts.Items(lstContacts.SelectedIndices(0)).SubItems(3).Text
                Helper.RequestSession(Contact, Guid.NewGuid())
            End If
        End If
    End Sub

    Private Sub cmdEndSession_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEndSession.Click
        If Me.CurrentSession Is Nothing Then
            MessageBox.Show("There is no current session.")
            Return
        Else
            Me.CurrentSession.EndSession()
        End If
    End Sub
End Class




Public Class UpdateControlText

    Private NewText As String

    ' The reference is retained as a generic control, 
    ' allowing this helper class to be reused in other scenarios.
    Private ControlToUpdate As Control

    Public Sub New(ByVal controlToUpdate As Control)
        Me.ControlToUpdate = controlToUpdate
    End Sub

    Public Sub AddText(ByVal newText As String)
        SyncLock Me
            Me.NewText = newText
            Dim Invoker As New MethodInvoker(AddressOf AddText)
            Me.ControlToUpdate.Invoke(Invoker)
        End SyncLock
    End Sub

    ' This method executes on the user interface thread.
    Private Sub AddText()
        Me.ControlToUpdate.Text &= NewText
    End Sub

    Public Sub ReplaceText(ByVal newText As String)
        SyncLock Me
            Me.NewText = newText
            Dim Invoker As New MethodInvoker(AddressOf ReplaceText)
            Me.ControlToUpdate.Invoke(Invoker)
        End SyncLock
    End Sub

    ' This method executes on the user interface thread.
    Private Sub ReplaceText()
        Me.ControlToUpdate.Text = NewText
    End Sub
End Class

