Imports System.Runtime.Remoting

Public Class Form1
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
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents cmdSend As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.cmdSend = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(48, 16)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(168, 20)
        Me.txtMessage.TabIndex = 0
        Me.txtMessage.Text = "Sample Message"
        '
        'cmdSend
        '
        Me.cmdSend.Location = New System.Drawing.Point(76, 56)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.Size = New System.Drawing.Size(120, 32)
        Me.cmdSend.TabIndex = 1
        Me.cmdSend.Text = "Send Message"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(272, 102)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdSend, Me.txtMessage})
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

#End Region


    ' Create the local remotable object that can receive the callback.
    Private ListenerObject As New RemoteLibrary.Listener()

    ' Create the remote object.
    Private TestObject As New RemoteLibrary.RemoteObject()

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Console.WriteLine("Configuring remote objects....")
        RemotingConfiguration.Configure("Client.exe.config")
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click

        ' Create the delegate that points to the client object.
        Dim Callback As New RemoteLibrary.ConfirmationCallback(AddressOf ListenerObject.ConfirmationCallback)

        ' Connect the event handler to the local listener class.
        AddHandler ListenerObject.CallbackReceived, AddressOf ListenerObject_CallbackReceived

        ' Send the message to the remote object.
        TestObject.ReceiveMessage(txtMessage.Text, Callback)

    End Sub

    Private Sub ListenerObject_CallbackReceived(ByVal sender As Object, ByVal e As RemoteLibrary.MessageEventArgs)
        MessageBox.Show("Callback received: " & e.Message)
    End Sub
End Class
