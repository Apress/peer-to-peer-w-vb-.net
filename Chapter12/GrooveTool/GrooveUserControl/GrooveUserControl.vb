Imports System.Runtime.InteropServices
Imports Groove.CollectionComponents
Imports Groove.Components
Imports Groove.MiscServices
Imports Groove.Record
Imports Groove.Helper

Public Class GrooveUserControl
    Inherits System.Windows.Forms.UserControl
    Implements Groove.Interop.Components.IGrooveComponent

#Region " Groove member variables"

    Private WithEvents propertyList As GroovePropertyList
    Private WithEvents recordSetEngine As GrooveRecordSetEngine

#End Region

#Region " IGrooveComponent default implementation "

	'Common Groove property names.
    Private Const CommonPropertyName = "Name"
    Private Const CommonPropertyBindableURL = "_BindableURL"
    Private Const CommonPropertyCanonicalURL = "_CanonicalURL"
    Private Const RecordSetEngineConnection = 0

	'Cached Groove property values.
    Private componentName As String
    Private componentBindableURL As String
    Private componentCanonicalURL As String

    Sub Initialize(ByVal propertyListInterop As Groove.Interop.Components.IGroovePropertyList) Implements Groove.Interop.Components.IGrooveComponent.Initialize
		'Create the property list wrapper object.
        propertyList = new GroovePropertyList(propertyListInterop)
        
		'Cache frequently used property values.
		componentName = propertyList.OpenPropertyAsString(CommonPropertyName)
        componentBindableURL = propertyList.OpenPropertyAsString(CommonPropertyBindableURL)
        componentCanonicalURL = propertyList.OpenPropertyAsString(CommonPropertyCanonicalURL)
    End Sub

    Sub BeginConnectToComponents() Implements Groove.Interop.Components.IGrooveComponent.BeginComponentConnections

    End Sub

    Sub ConnectToComponent(ByVal componentInterop As Groove.Interop.Components.IGrooveComponent, ByVal connectionID As Integer) Implements Groove.Interop.Components.IGrooveComponent.ConnectToComponent
        Select Case connectionID
            Case RecordSetEngineConnection
                'Create the recordset engine wrapper object.
                Dim recordSetEngineInterop As Groove.Interop.CollectionComponents.IGrooveRecordSetEngine
                recordSetEngineInterop = componentInterop
                recordSetEngine = New GrooveRecordSetEngine(recordSetEngineInterop)
        End Select
    End Sub

    Sub EndConnectToComponents() Implements Groove.Interop.Components.IGrooveComponent.EndComponentConnections

    End Sub

    Sub PreUnconnectFromComponents() Implements Groove.Interop.Components.IGrooveComponent.PreUnconnectFromComponents

    End Sub

    Sub UnconnectFromComponents() Implements Groove.Interop.Components.IGrooveComponent.UnconnectFromComponents
		'Dispose of the recordset engine.
        recordSetEngine.Dispose()
        recordSetEngine = Nothing
    End Sub

    Sub Terminate() Implements Groove.Interop.Components.IGrooveComponent.Terminate
		'Dispose of the property list.
        propertyList.Dispose()
        propertyList = Nothing
    End Sub

    ReadOnly Property BindableURL() As String Implements Groove.Interop.Components.IGrooveComponent.BindableURL
        Get
            BindableURL = componentBindableURL
        End Get
    End Property

    ReadOnly Property CanonicalURL() As String Implements Groove.Interop.Components.IGrooveComponent.CanonicalURL
        Get
            CanonicalURL = componentCanonicalURL
        End Get
    End Property

    Function OpenName() As String Implements Groove.Interop.Components.IGrooveComponent.OpenName
        OpenName = componentName
    End Function

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl1 overrides dispose to clean up the component list.
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
    Friend WithEvents lstItems As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents txtBroughtBy As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents lblCreator As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstItems = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.txtBroughtBy = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.lblCreator = New System.Windows.Forms.Label()
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader()
        Me.SuspendLayout()
        '
        'lstItems
        '
        Me.lstItems.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lstItems.FullRowSelect = True
        Me.lstItems.HideSelection = False
        Me.lstItems.Location = New System.Drawing.Point(12, 68)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(448, 264)
        Me.lstItems.TabIndex = 2
        Me.lstItems.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Item"
        Me.ColumnHeader1.Width = 160
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Brought By"
        Me.ColumnHeader2.Width = 160
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdAdd.Location = New System.Drawing.Point(384, 36)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(72, 24)
        Me.cmdAdd.TabIndex = 3
        Me.cmdAdd.Text = "Add Item"
        '
        'txtItem
        '
        Me.txtItem.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtItem.Location = New System.Drawing.Point(104, 12)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(220, 21)
        Me.txtItem.TabIndex = 4
        Me.txtItem.Text = ""
        '
        'txtBroughtBy
        '
        Me.txtBroughtBy.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtBroughtBy.Location = New System.Drawing.Point(104, 36)
        Me.txtBroughtBy.Name = "txtBroughtBy"
        Me.txtBroughtBy.ReadOnly = True
        Me.txtBroughtBy.Size = New System.Drawing.Size(220, 21)
        Me.txtBroughtBy.TabIndex = 5
        Me.txtBroughtBy.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 20)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Item:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Brought By:"
        '
        'cmdRemove
        '
        Me.cmdRemove.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdRemove.Location = New System.Drawing.Point(380, 344)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdRemove.TabIndex = 8
        Me.cmdRemove.Text = "Remove Item"
        '
        'lblCreator
        '
        Me.lblCreator.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblCreator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCreator.Location = New System.Drawing.Point(12, 344)
        Me.lblCreator.Name = "lblCreator"
        Me.lblCreator.Size = New System.Drawing.Size(360, 24)
        Me.lblCreator.TabIndex = 9
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "User URL"
        Me.ColumnHeader3.Width = 200
        '
        'GrooveUserControl
        '
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCreator, Me.cmdRemove, Me.Label2, Me.Label1, Me.txtBroughtBy, Me.txtItem, Me.cmdAdd, Me.lstItems})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "GrooveUserControl"
        Me.Size = New System.Drawing.Size(472, 380)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Groove event handlers"

    Private Sub OnRecordSetChanged(ByVal sender As GrooveRecordSetEngine, ByVal e As GrooveRecordSetListenerEventArgs) Handles recordSetEngine.RecordSetChangedEvent

        Dim RecordID As Double
        Dim Record As IGrooveRecord

        ' The ToolHelper is used to start a new transaction.
        ' This prevents the data from changing while the display is being updated.
        Dim ToolHelper As New GrooveToolHelper(Me.propertyList)
        ToolHelper.StartTelespaceTransaction(True)

        Try
            ' Determine the type of change.
            Select Case e.RecordSetChangeType

                Case GrooveRecordSetChangeType.GrooveRecordSetChangeType_Added

                    ' The record set contains one or more items to be added.
                    Do While e.RecordIDEnum.HasMore()
                        RecordID = e.RecordIDEnum.OpenNext()
                        If recordSetEngine.HasRecord(RecordID) Then

                            Record = recordSetEngine.OpenRecord(RecordID)
                            Dim Item As New ListViewItem(Record.OpenFieldAsString("Item"))
                            Item.SubItems.Add(Record.OpenFieldAsString("BroughtBy"))
                            Item.SubItems.Add(Record.OpenFieldAsString("UserURL"))
                            lstItems.Items.Add(Item)

                            ' Store the unique record ID.
                            item.Tag = RecordID

                            ' Explicitly release record.
                            Record.Dispose()
                        End If
                    Loop

                Case GrooveRecordSetChangeType.GrooveRecordSetChangeType_Replaced
                    ' Not implemented in this tool.

                Case GrooveRecordSetChangeType.GrooveRecordSetChangeType_Removed

                    ' Recordset contains one or more items to be removed.
                    Do While e.RecordIDEnum.HasMore()
                        RecordID = e.RecordIDEnum.OpenNext()

                        ' Check the ListView for this item.
                        Dim Item As ListViewItem
                        For Each Item In lstItems.Items
                            If CType(Item.Tag, Double) = RecordID Then
                                lstItems.Items.Remove(item)
                            End If
                        Next

                    Loop

                Case GrooveRecordSetChangeType.GrooveRecordSetChangeType_AllRemoved
                    lstItems.Items.Clear()

            End Select

            ToolHelper.CommitTelespaceTransaction()
        Catch Err As Exception

            ' Abort transaction
            ToolHelper.AbortTelespaceTransaction()
            MessageBox.Show(Err.Message)
        Finally

            ' Explicity release unmanaged resources.
            Marshal.ReleaseComObject(Record)
            Marshal.ReleaseComObject(sender)

        End Try
		
    End Sub

#End Region


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        ' Verify the item information is present.
        If txtItem.Text = "" Or txtBroughtBy.Text = "" Then
            MessageBox.Show("Enter your name and the item name.")
            Return
        End If

        ' Create a new record to add to the Groove record set.
        Dim Record As New GrooveRecord()

        Try
            ' Set the new field values.
            Record.SetField("Item", txtItem.Text)
            Record.SetField("BroughtBy", txtBroughtBy.Text)
            Record.SetField("UserURL", Me.UserUrl)

            ' Add the record
            Me.recordSetEngine.AddRecord(Record)

        Finally
            ' Explicitly release the unmanaged resources held by the record.
            Record.Dispose()
        End Try

    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click

        If lstItems.FocusedItem Is Nothing Then
            MessageBox.Show("No item selected.")
        ElseIf lstItems.FocusedItem.SubItems(2).Text <> Me.UserUrl Then
            MessageBox.Show("You did not add this item.")
        Else
            Dim RecordID As Double = CType(lstItems.FocusedItem.Tag, Double)
            Dim ToolHelper As New GrooveToolHelper(Me.propertyList)

            ' Open a transaction on the telespace to prevent data from
            ' changing out from under us.
            ToolHelper.StartTelespaceTransaction(False)

            Try
                Me.recordSetEngine.RemoveRecord(RecordID)
                ToolHelper.CommitTelespaceTransaction()

            Catch Err As Exception
                ToolHelper.AbortTelespaceTransaction()
                MessageBox.Show(Err.Message)

            End Try

        End If
    End Sub

    ' Track unique identifiers that indicate who created the 
    ' shared space, and who is currently using it.
    Private UserUrl As String
    Private CreatorUrl As String

    Private Sub GrooveUserControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Define some basic Groove identity interfaces.
        Dim Account As Groove.Interop.AccountServices.IGrooveAccount
        Dim Identity As Groove.Interop.IdentityServices.IGrooveIdentity
        Dim Contact As Groove.Interop.ContactServices.IGrooveContact
        Dim VCard As Groove.Interop.ContactServices.IGrooveVCard
        Dim Identification As Groove.Interop.ContactServices.IGrooveIdentification

        ' Retrieve the identity information for the shared space creator.
        Account = CType(Me.propertyList.OpenProperty("_Account"), Groove.Interop.AccountServices.IGrooveAccount)
        Identity = Account.DefaultIdentity
        Me.CreatorUrl = Identity.URL
        Contact = Identity.Contact
        VCard = Contact.OpenVCard()
        Identification = VCard.OpenIdentification()

        ' Display this identity in the window.
        lblCreator.Text = "Space hosted by: " & Identification.OpenFullName()


        ' Retrieve the identify information for the current user.
        Identity = CType(Me.propertyList.OpenProperty("_CurrentIdentity"), Groove.Interop.IdentityServices.IGrooveIdentity)
        Me.UserUrl = Identity.URL
        Contact = Identity.Contact
        VCard = Contact.OpenVCard()
        Identification = VCard.OpenIdentification()

        ' Pre-fill in the txtBroughtBy textbox.
        txtBroughtBy.Text = Identification.OpenFullName()

    End Sub
End Class
