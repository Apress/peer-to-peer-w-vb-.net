Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading
Imports System.Text

Module UdpClientConsole

    ' The port used to listen for incoming messages.
    Private LocalPort As Integer

    Public Sub Main()

        ' Set up ports.
        Console.Write("Remote IP: ")
        Dim IP As String = Console.ReadLine()

        Console.Write("Remote port: ")
        Dim Port As String = Console.ReadLine()

        ' Define the IP and port where messages are sent.
        Dim RemoteEndPoint As New IPEndPoint(IPAddress.Parse(IP), Int32.Parse(Port))

        Console.Write("Local port: ")
        LocalPort = Int32.Parse(Console.ReadLine())
        Console.WriteLine(New String("-", 40))
        Console.WriteLine()

        ' Create a new thread for receiving incoming messages.
        Dim ReceiveThread As New Thread(AddressOf ReceiveData)
        ReceiveThread.IsBackground = True
        ReceiveThread.Start()

        Dim Client As New UdpClient()

        Try

            ' Loop until the word QUIT is entered.
            Dim Text As String
            Dim Data() As Byte
            Do
                Text = Console.ReadLine()

                ' Send the text to the remote client.
                If Text <> "QUIT" Then
                    ' Encode the data to binary manually using UTF8 encoding.
                    Data = Encoding.UTF8.GetBytes(Text)

                    ' Send the text to the remote client.
                    Client.Send(Data, Data.Length, RemoteEndPoint)
                End If

            Loop Until Text = "QUIT"

        Catch Err As Exception
            Console.WriteLine(Err.ToString())
        End Try

    End Sub


    Private Sub ReceiveData()

        Dim Client As New UdpClient(LocalPort)

        Dim Data() As Byte
        Dim Text As String

        Do
            Try
                ' Receive bytes.
                Data = Client.Receive(Nothing)

                ' Try to convert bytes into a message using UTF8 encoding.
                Text = Encoding.UTF8.GetString(Data)

                ' Display the retreived text.
                Console.WriteLine("*** RECEIVED: " & Text)

            Catch Err As Exception
                Console.WriteLine(Err.ToString())
            End Try
        Loop

    End Sub

End Module
