Imports System.Runtime.Remoting

Public Module Startup

    Public Sub Main()
        ' Create the server-side form (which displays diagnostic information).
        ' This form is implemented as a diagnostic logger.
        ' That means that server trace messages are displayed automatically,
        ' without requiring any specialized threading code.
        Dim frmLog As New TraceComponent.FormTraceListener()
        Trace.Listeners.Add(frmLog)

        ' Configure the connection, and register the well-known object
        ' (ServerProcess) that will accept client requests.
        RemotingConfiguration.Configure("TalkServer.exe.config")

        ' From this point on, messages can be received by the ServerProcess
        ' object. The object will be created for the first request,
        ' although you could create it explicitly if desired.

        ' Show the trace listener form. By using ShowDialog, we set up a 
        ' message loop on this thread. The application will automatically end
        ' when the form is closed.
        Dim frm As Form = frmLog.TraceForm
        frm.Text = "Talk .NET Server (Trace Display)"
        frm.ShowDialog()

    End Sub

End Module
