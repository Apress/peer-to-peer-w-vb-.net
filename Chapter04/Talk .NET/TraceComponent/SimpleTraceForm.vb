Public Class SimpleTraceForm
    Inherits System.Windows.Forms.Form
    Implements ITraceForm

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
    Friend WithEvents lstMessages As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstMessages = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lstMessages
        '
        Me.lstMessages.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstMessages.IntegralHeight = False
        Me.lstMessages.Location = New System.Drawing.Point(4, 4)
        Me.lstMessages.Name = "lstMessages"
        Me.lstMessages.Size = New System.Drawing.Size(304, 224)
        Me.lstMessages.TabIndex = 0
        '
        'SimpleTraceForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(316, 238)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lstMessages})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "SimpleTraceForm"
        Me.Text = "Trace Display"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub LogToForm(ByVal message As String) Implements ITraceForm.LogToForm
        ' Add the log message.
        lstMessages.Items.Add(message)

        ' Scroll to the bottom of the list.
        lstMessages.SelectedIndex = lstMessages.Items.Count - 1
    End Sub

End Class
