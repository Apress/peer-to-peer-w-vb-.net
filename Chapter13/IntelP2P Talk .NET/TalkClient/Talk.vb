Imports TalkComponent

Public Class Talk
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSend As System.Windows.Forms.Button
    Friend WithEvents lstUsers As System.Windows.Forms.ComboBox
    Friend WithEvents txtReceived As System.Windows.Forms.RichTextBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents tmrRefreshUsers As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtReceived = New System.Windows.Forms.RichTextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdSend = New System.Windows.Forms.Button()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstUsers = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tmrRefreshUsers = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtReceived})
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(8, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(364, 196)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'txtReceived
        '
        Me.txtReceived.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtReceived.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtReceived.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReceived.Location = New System.Drawing.Point(8, 24)
        Me.txtReceived.Name = "txtReceived"
        Me.txtReceived.ReadOnly = True
        Me.txtReceived.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.txtReceived.Size = New System.Drawing.Size(354, 168)
        Me.txtReceived.TabIndex = 0
        Me.txtReceived.TabStop = False
        Me.txtReceived.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Window
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdSend, Me.txtMessage})
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(8, 272)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(364, 60)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'cmdSend
        '
        Me.cmdSend.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdSend.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdSend.Location = New System.Drawing.Point(320, 22)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(40, 40)
        Me.cmdSend.TabIndex = 5
        Me.cmdSend.Text = "Send"
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMessage.Location = New System.Drawing.Point(4, 24)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(312, 44)
        Me.txtMessage.TabIndex = 4
        Me.txtMessage.Text = ""
        '
        'Label2
        '
        Me.Label2.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(364, 18)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Recent Messages:"
        '
        'lstUsers
        '
        Me.lstUsers.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.lstUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lstUsers.Location = New System.Drawing.Point(60, 252)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.Size = New System.Drawing.Size(172, 21)
        Me.lstUsers.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Label1.Location = New System.Drawing.Point(8, 256)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 22)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Send To:"
        '
        'tmrRefreshUsers
        '
        Me.tmrRefreshUsers.Interval = 10000
        '
        'Talk
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(380, 346)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lstUsers, Me.Label1, Me.Label2, Me.GroupBox2, Me.GroupBox1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(224, 268)
        Me.Name = "Talk"
        Me.Text = "Talk .NET"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' The remotable intermediary for all client-to-server communication.
    'Public TalkClient As ClientProcess

    Private Sub Talk_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'AddHandler ClientProcess.MessageReceived, AddressOf Me.TalkClient_MessageReceived
        ClientProcess.MessageReceivedCallback = AddressOf Me.ReceiveMessage
        Me.Text &= " - " & ClientProcess.Alias

        ' Attempt to register with the server.
        ClientProcess.Login()


        ' Ordinaly, a user list is periodically fetched from the
        ' server. In this case, we enable the timer, and call it
        ' once (immediately) to initially populate the list box.
        tmrRefreshUsers_Tick(Me, EventArgs.Empty)
        tmrRefreshUsers.Enabled = True
        lstUsers.SelectedIndex = 0

    End Sub


    Private Sub ReceiveMessage(ByVal message As String, ByVal senderAlias As String)

        ' Define the text.
        Dim NewText As String
        NewText = "Message From: " & senderAlias
        NewText &= " delivered at " & DateTime.Now.ToShortTimeString()
        NewText &= Environment.NewLine & message
        NewText &= Environment.NewLine & Environment.NewLine

        ' Create the object.
        Dim ThreadsafeUpdate As New UpdateControlText(NewText, txtReceived)

        ' Invoke the update on the user interface thread.
        Me.Invoke(New MethodInvoker(AddressOf ThreadsafeUpdate.Update))

    End Sub


    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click

        ClientProcess.MessageReceivedCallback = AddressOf Me.ReceiveMessage

        txtReceived.Text &= "Sent Message To: " & lstUsers.Text
        txtReceived.Text &= Environment.NewLine & txtMessage.Text
        txtReceived.Text &= Environment.NewLine & Environment.NewLine

        ' Send the message through the ClientProcess object.
        ClientProcess.SendMessage(lstUsers.Text, txtMessage.Text)
        txtMessage.Text = ""

    End Sub

    ' Checks every 30 seconds.
    ' For very large systems with low user turnover,
    ' it might be better to broadcast user added and user removed messages.
    Private Sub tmrRefreshUsers_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefreshUsers.Tick

        ' Prepare list of logged in users.
        ' The code must copy the ICollection entries into
        ' an ordinary array before they can be added.
        Dim UserArray() As String
        Dim UserCollection As ICollection = ClientProcess.GetUsers
        ReDim UserArray(UserCollection.Count - 1)
        UserCollection.CopyTo(UserArray, 0)

        ' Replace the list entries. At the same time,
        ' the code will track the previous selection and try
        ' to restore it, so the update won't be noticeable.
        Dim CurrentSelection As String = lstUsers.Text
        lstUsers.Items.Clear()
        lstUsers.Items.AddRange(UserArray)
        lstUsers.Text = CurrentSelection

    End Sub

    Private Sub Talk_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ClientProcess.LogOut()
    End Sub

    'Private FileOffers As New Hashtable()

    'Private Sub FileOfferReceived(ByVal filename As String, ByVal fileIdentifier As Guid, ByVal senderAlias As String)

    '    ' Create the user message describing the file offer.
    '    Dim NewText As String
    '    NewText = senderAlias & " has offered to transmit the file named: "
    '    NewText &= filename
    '    NewText &= Environment.NewLine & Environment.NewLine

    '    ' Create the object.
    '    Dim ThreadsafeUpdate As New UpdateControlText(NewText, txtReceived)

    '    ' Invoke the update on the user interface thread.
    '    Me.Invoke(New MethodInvoker(AddressOf ThreadsafeUpdate.Update))

    'End Sub

    'Private Sub cmdOffer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOffer.Click

    '    ' Prompt the user for a file to offer.
    '    Dim dlgOpen As New OpenFileDialog()
    '    dlgOpen.Title = "Choose a File to Transmit"

    '    If dlgOpen.ShowDialog() = DialogResult.OK Then
    '        ' Send the offer.
    '        ClientProcess.SendFileOffer(lstUsers.Text, dlgOpen.FileName)
    '    End If

    'End Sub
End Class
Public Class UpdateControlText

    Private NewText As String

    ' The reference is retained as a generic control, 
    ' allowing this helper class to be reused in other scenarios.
    Private ControlToUpdate As Control

    Public Sub New(ByVal newText As String, ByVal controlToUpdate As Control)
        Me.NewText = newText
        Me.ControlToUpdate = controlToUpdate
    End Sub

    ' This method must execute on the user interface thread.
    Public Sub Update()
        Me.ControlToUpdate.Text &= NewText
    End Sub

End Class
