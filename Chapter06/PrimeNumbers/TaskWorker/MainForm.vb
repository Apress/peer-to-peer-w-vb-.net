Imports TaskComponent

Public Class MainForm
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
    Friend WithEvents cmdHide As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTimeTaken As System.Windows.Forms.Label
    Friend WithEvents lblBackgroundInfo As System.Windows.Forms.Label
    Friend WithEvents txtResults As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTimeTaken = New System.Windows.Forms.Label()
        Me.txtResults = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblBackgroundInfo = New System.Windows.Forms.Label()
        Me.cmdHide = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblTimeTaken, Me.txtResults, Me.Label4, Me.cmdFind, Me.Label3, Me.txtTo, Me.Label2, Me.Label1, Me.txtFrom})
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(500, 248)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Request a Task"
        '
        'lblTimeTaken
        '
        Me.lblTimeTaken.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTimeTaken.Location = New System.Drawing.Point(80, 212)
        Me.lblTimeTaken.Name = "lblTimeTaken"
        Me.lblTimeTaken.Size = New System.Drawing.Size(408, 20)
        Me.lblTimeTaken.TabIndex = 8
        '
        'txtResults
        '
        Me.txtResults.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtResults.Location = New System.Drawing.Point(80, 104)
        Me.txtResults.Multiline = True
        Me.txtResults.Name = "txtResults"
        Me.txtResults.ReadOnly = True
        Me.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResults.Size = New System.Drawing.Size(408, 92)
        Me.txtResults.TabIndex = 7
        Me.txtResults.Text = ""
        '
        'Label4
        '
        Me.Label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.Label4.Location = New System.Drawing.Point(16, 212)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Time Taken:"
        '
        'cmdFind
        '
        Me.cmdFind.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdFind.Location = New System.Drawing.Point(216, 52)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(112, 24)
        Me.cmdFind.TabIndex = 5
        Me.cmdFind.Text = "Find Primes"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "To:"
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(80, 56)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.TabIndex = 3
        Me.txtTo.Text = "500000"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "From:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Results:"
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(80, 28)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.TabIndex = 0
        Me.txtFrom.Text = "1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblBackgroundInfo})
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(8, 268)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(500, 88)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Background Task"
        '
        'lblBackgroundInfo
        '
        Me.lblBackgroundInfo.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblBackgroundInfo.Location = New System.Drawing.Point(16, 36)
        Me.lblBackgroundInfo.Name = "lblBackgroundInfo"
        Me.lblBackgroundInfo.Size = New System.Drawing.Size(468, 40)
        Me.lblBackgroundInfo.TabIndex = 0
        '
        'cmdHide
        '
        Me.cmdHide.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdHide.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdHide.Location = New System.Drawing.Point(436, 380)
        Me.cmdHide.Name = "cmdHide"
        Me.cmdHide.Size = New System.Drawing.Size(68, 28)
        Me.cmdHide.TabIndex = 2
        Me.cmdHide.Text = "Hide"
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(520, 418)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdHide, Me.GroupBox2, Me.GroupBox1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Worker Status"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WithEvents Client As ClientProcess
    
    Private Sub cmdHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHide.Click
        Me.Hide()
    End Sub

    Private StartTime As DateTime

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        txtResults.Text = ""
        lblTimeTaken.Text = ""
        Try
            StartTime = DateTime.Now
            Client.FindPrimes(txtFrom.Text, txtTo.Text)
        Catch Err As Exception
            MessageBox.Show(Err.ToString())
        End Try
    End Sub

    
    Private Sub Client_BackgroundStatusChanged(ByVal sender As Object, ByVal e As TaskWorker.BackgroundStatusChanged) Handles Client.BackgroundStatusChanged
        ' Marshal changes to UI thread.
        ' Create the object.
        Dim NewText As String
        If e.Status = BackgroundStatus.Idle Then
            NewText = "The background thread has finished processing its prime number query, and is now idle."
        ElseIf e.Status = BackgroundStatus.Processing Then
            NewText = "The background thread has received a new prime number query, and is now processing it."
        End If

        Dim ThreadsafeUpdate As New UpdateControlText(NewText, lblBackgroundInfo)

        ' Invoke the update on the user interface thread.
        Me.Invoke(New MethodInvoker(AddressOf ThreadsafeUpdate.Update))
    End Sub

    Private Sub Client_ResultsReceived(ByVal sender As Object, ByVal e As TaskWorker.ResultsReceivedEventArgs) Handles Client.ResultsReceived
        ' Marshal changes to UI thread.
        Dim NewText As String
        NewText = DateTime.Now.Subtract(StartTime).ToString()
        Dim ThreadsafeUpdate As New UpdateControlText(NewText, lblTimeTaken)
        ' Invoke the update on the user interface thread.
        Me.Invoke(New MethodInvoker(AddressOf ThreadsafeUpdate.Update))

        Dim Builder As New System.Text.StringBuilder()
        Dim Prime As Integer
        For Each Prime In e.Primes
            Builder.Append(Prime.ToString() & " ")
        Next
        NewText = Builder.ToString()

        ThreadsafeUpdate = New UpdateControlText(NewText, txtResults)
        ' Invoke the update on the user interface thread.
        Me.Invoke(New MethodInvoker(AddressOf ThreadsafeUpdate.Update))
    End Sub

    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTo.KeyPress, txtFrom.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then e.Handled = True
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Client.Status = BackgroundStatus.Idle Then
            lblBackgroundInfo.Text = "The background thread is idle."
        ElseIf Client.Status = BackgroundStatus.Processing Then
            lblBackgroundInfo.Text = "The background thread has received a new prime number query, and is now processing it."
        End If
    End Sub

    Private Sub MainForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
        Me.Hide()
    End Sub
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
        Me.ControlToUpdate.Text = NewText
    End Sub

End Class
