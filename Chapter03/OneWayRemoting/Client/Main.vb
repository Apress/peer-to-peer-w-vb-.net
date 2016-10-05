Imports System.Runtime.Remoting

Module ClientApplication

    Sub Main()

        Console.WriteLine("Configuring remote objects....")
        RemotingConfiguration.Configure("Client.exe.config")

        Do
            Console.WriteLine()
            Console.WriteLine("Enter the message you would like to send.")
            Console.WriteLine("Or type 'exit' to exit the application.")
            Console.Write(">")
            Dim Message As String = Console.ReadLine()
            If Message.ToUpper = "EXIT" Then Exit Do

            ' Create the remote object.
            Dim TestObject As New RemoteLibrary.RemoteObject()

            ' Send the message to the remote object.
            TestObject.ReceiveMessage(New RemoteLibrary.Message(Message, "client"))

            Console.WriteLine()
            Console.WriteLine("Message sent.")

            TestObject = Nothing
        Loop

    End Sub

End Module