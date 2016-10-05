Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading

Module TcpClientConsole

    Private Stream As NetworkStream

    Public Sub Main()
        ' Give the server a chance to load.
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1))

        Dim Client As New TcpClient()

        Try
            ' Try to connect to the server on port 11000.
            Client.Connect(IPAddress.Parse("127.0.0.1"), 11000)
            Console.WriteLine("* TCP Client *")
            Console.WriteLine("Connection established.")
            Console.WriteLine(New String("-", 40))
            Console.WriteLine()

            ' Retrieve the network stream.
            Stream = Client.GetStream()

            ' Create a new thread for receiving incoming messages.
            Dim ReceiveThread As New Thread(AddressOf ReceiveData)
            ReceiveThread.IsBackground = True
            ReceiveThread.Start()

            ' Create a BinaryWriter for writing to the stream.
            Dim w As New BinaryWriter(Stream)

            ' Loop until the word QUIT is entered.
            Dim Text As String
            Do
                Text = Console.ReadLine()

                ' Send the text to the remote client.
                If Text <> "QUIT" Then w.Write(Text)

            Loop Until Text.ToUpper() = "QUIT"

            ' Close the connection socket.
            Client.Close()

        Catch Err As Exception
            Console.WriteLine(Err.ToString())
        End Try

    End Sub


    Private Sub ReceiveData()

        ' Create a BinaryReader for the stream.
        Dim r As New BinaryReader(Stream)

        Do
            ' Display any received text.
            If Stream.DataAvailable Then
                Console.WriteLine(("*** RECEIVED: " + r.ReadString()))
            End If
        Loop

    End Sub

End Module
