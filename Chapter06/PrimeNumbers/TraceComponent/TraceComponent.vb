' Any custom form can be a "trace form" as long as it
' implements this interface.
Public Interface ITraceForm

    ' Determines how trace messages will be displayed.
    Sub LogToForm(ByVal message As String)

End Interface

' The form listener is a TraceListener object that
' maps trace messages to an ITraceForm instance, which
' will then display them in a window.
Public Class FormTraceListener
    Inherits TraceListener

    Public TraceForm As ITraceForm

    ' Use the default trace form.
    Public Sub New()
        MyBase.New()
        Me.TraceForm = New SimpleTraceForm()
    End Sub

    ' Use a custom trace form.
    Public Sub New(ByVal traceForm As ITraceForm)
        MyBase.New()

        If Not TypeOf traceForm Is Form Then
            Throw New InvalidCastException("ITraceForm must be used on a Form instance.")
        End If

        Me.TraceForm = traceForm
    End Sub

    Public Overloads Overrides Sub Write(ByVal value As String)
        TraceForm.LogToForm(value)
    End Sub

    Public Overloads Overrides Sub WriteLine(ByVal message As String)
        ' WriteLine() and Write() are equivalent in this simple example.
        Me.Write(message)
    End Sub

End Class
