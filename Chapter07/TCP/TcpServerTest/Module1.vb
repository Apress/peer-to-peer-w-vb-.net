Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading

Module TcpServerConsole

    Private Stream As NetworkStream

    Public Sub Main()

        ' Create a new listener on port 11000.
        Dim Listener As New TcpListener(11000)

        ' Initialize the port and start listening.
        Listener.Start()

        Console.WriteLine("* TCP Server *")
        Console.WriteLine("Waiting for a connection...")

        Try
            ' Wait for a connection request, 
            ' and return a TcpClient initialized for communication. 
            Dim Client As TcpClient = Listener.AcceptTcpClient()
            Console.WriteLine("Connection accepted.")
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

            ' Terminate the receiveing thread.
            ReceiveThread.Abort()

            ' Close the connection socket.
            Client.Close()

            ' Close the underlying socket (stop listening for new requests).
            Listener.Stop()

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
