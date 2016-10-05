Public Class Startup

    Public Shared Sub Main()
        ' Create the login window (which retrieves the user identifier).
        Dim frmLogin As New Login()

        ' Only continue if the user successfully exits by clicking OK
        ' (not the Cancel or Exit button).
        If Not frmLogin.ShowDialog Then
            ' Create the new remotable client object.
            Dim Client As New ClientProcess(frmLogin.UserName)

            ' Create the client form.
            Dim frm As New Talk()
            frm.TalkClient = Client

            ' Show the form.
            frm.ShowDialog()
        End If

    End Sub

End Class
