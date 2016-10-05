Imports System.Runtime.Remoting

Module ServerApplication

    Sub Main()

        Console.WriteLine("Configuring remotable objects....")
        RemotingConfiguration.Configure("Server.exe.config")

        Console.WriteLine("Waiting for a request.")
        Console.WriteLine("Press any key to exit the application.")

        ' The CLR will monitor for requests as long as this application
        ' is running. When the user presses enter, it will end.
        Console.ReadLine()

    End Sub

End Module
