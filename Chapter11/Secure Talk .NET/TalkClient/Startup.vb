Imports System.Security.Cryptography
Imports System.IO

Public Class Startup

    Public Shared Sub Main()
        ' Create the login window (which retrieves the user identifier).
        Dim frmLogin As New Login()

        Dim Rsa As New RSACryptoServiceProvider()

        ' Create the new remotable client object.
        Dim Client As ClientProcess

        ' Only continue if the user successfully exits by clicking OK
        ' (not the Cancel or Exit button).
        Do
            If Not frmLogin.ShowDialog() = DialogResult.OK Then End

            Try
                If frmLogin.CreateNew Then
                    If File.Exists(frmLogin.UserName) Then
                        MessageBox.Show("Cannot create new account. Key file already exists for this user.")
                    Else
                        Rsa = New RSACryptoServiceProvider()
                        Client = New ClientProcess(frmLogin.UserName, frmLogin.CreateNew, Rsa)

                        Dim fs As New FileStream(frmLogin.UserName, FileMode.Create)
                        Dim w As New BinaryWriter(fs)
                        w.Write(Rsa.ToXmlString(True))
                        w.Flush()
                        fs.Close()
                        Exit Do
                    End If
                Else
                    If File.Exists(frmLogin.UserName) Then
                        Dim fs As New FileStream(frmLogin.UserName, FileMode.Open)
                        Dim r As New BinaryReader(fs)
                        Rsa.FromXmlString(r.ReadString())
                        fs.Close()

                        '''
                        Debug.Write(Rsa.ToXmlString(False))

                        Client = New ClientProcess(frmLogin.UserName, frmLogin.CreateNew, Rsa)
                        Exit Do
                    Else
                        MessageBox.Show("No key file exists for this user.")
                    End If
                End If

            Catch Err As Exception
                MessageBox.Show(Err.Message)
            End Try

        Loop

        ' Create the client form.
        Dim frm As New Talk()
        frm.TalkClient = Client

        ' Show the form.
        frm.ShowDialog()

    End Sub

End Class
