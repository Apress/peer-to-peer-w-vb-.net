Imports System.Security.Cryptography
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

<Serializable()> _
Public Class SignedObject

    Private SerializedObject As New MemoryStream()
    Private Signature() As Byte

    Public Sub New(ByVal objectToSign As Object, ByVal keyPairXml As String)
        ' Serialize a copy of objectToSign in memory.
        Dim f As New BinaryFormatter()
        f.Serialize(Me.SerializedObject, objectToSign)

        ' Add the signature.
        Me.SerializedObject.Position = 0
        Dim Rsa As New RSACryptoServiceProvider()
        Rsa.FromXmlString(keyPairXml)
        Me.Signature = Rsa.SignData(Me.SerializedObject, HashAlgorithm.Create())
    End Sub

    Public Shared Function Deserialize(ByVal signedObjectBytes() As Byte) As SignedObject
        ' Deserialize the SignedObject.
        Dim ObjectStream As New MemoryStream()
        ObjectStream.Write(signedObjectBytes, 0, signedObjectBytes.Length)
        ObjectStream.Position = 0
        Dim f As New BinaryFormatter()
        Return CType(f.Deserialize(ObjectStream), SignedObject)
    End Function

    Public Function Serialize() As Byte()
        Dim f As New BinaryFormatter()
        Dim ObjectStream As New MemoryStream()
        f.Serialize(ObjectStream, Me)
        Return ObjectStream.ToArray()
    End Function

    Public Function ValidateSignature(ByVal publicKeyXml) As Boolean
        Dim Rsa As New RSACryptoServiceProvider()
        Rsa.FromXmlString(publicKeyXml)
        Return Rsa.VerifyData(Me.SerializedObject.ToArray(), HashAlgorithm.Create(), Me.Signature)
    End Function

    Public Function GetObjectWithoutSignature() As Object
        Dim f As New BinaryFormatter()

        Me.SerializedObject.Position = 0
        Return f.Deserialize(Me.SerializedObject)
    End Function


End Class

<Serializable()> _
Public Class EncryptedObject

    Private SerializedObject As New MemoryStream()

    Public Sub New(ByVal objectToEncrypt As Object, ByVal publicKeyXml As String)
        ' Serialize a copy of objectToEncrypt in memory.
        Dim f As New BinaryFormatter()
        Dim ObjectStream As New MemoryStream()
        f.Serialize(ObjectStream, objectToEncrypt)
        ObjectStream.Position = 0
        'Dim ObjectBytes() As Byte = ObjectStream.ToArray()

        Dim BlockSize As Integer
        Dim Rsa As New RSACryptoServiceProvider()
        Rsa.FromXmlString(publicKeyXml)

        If Rsa.KeySize = 1024 Then
            BlockSize = 16
        Else
            BlockSize = 5
        End If

        ' Move through the data one block at a time.
        Dim RawBlock(), EncryptedBlock() As Byte
        Dim i As Integer
        Dim Bytes As Integer = ObjectStream.Length
        For i = 0 To Bytes Step BlockSize

            If Bytes - i > BlockSize Then
                ReDim RawBlock(BlockSize - 1)
            Else
                ReDim RawBlock(Bytes - i - 1)
            End If

            ' Copy a block of data.
            'Buffer.BlockCopy(ObjectBytes, i, RawBlock, 0, RawBlock.Length)
            ObjectStream.Read(RawBlock, 0, RawBlock.Length)

            ' Encrypt the block of data.
            EncryptedBlock = Rsa.Encrypt(RawBlock, False)

            ' Write the block of data.
            Me.SerializedObject.Write(EncryptedBlock, 0, EncryptedBlock.Length)
        Next

    End Sub

    Public Shared Function Deserialize(ByVal encryptedObjectBytes() As Byte) As EncryptedObject
        ' Deserialize the SignedObject.
        Dim ObjectStream As New MemoryStream()
        ObjectStream.Write(encryptedObjectBytes, 0, encryptedObjectBytes.Length)
        ObjectStream.Position = 0
        Dim f As New BinaryFormatter()
        Return CType(f.Deserialize(ObjectStream), EncryptedObject)
    End Function

    Public Function Serialize() As Byte()
        Dim f As New BinaryFormatter()
        Dim ObjectStream As New MemoryStream()
        f.Serialize(ObjectStream, Me)
        Return ObjectStream.ToArray()
    End Function


    Public Function DecryptContainedObject(ByVal keyPairXml As String) As Object

        Dim Rsa As New RSACryptoServiceProvider()
        Rsa.FromXmlString(keyPairXml)

        ' Create the memory stream where the decrypted data
        ' will be stored.
        Dim ObjectStream As New MemoryStream()

        'Dim ObjectBytes() As Byte = Me.SerializedObject.ToArray()
        Me.SerializedObject.Position = 0
        ' Determine the block size for decrypting.
        Dim keySize As Integer = Rsa.KeySize / 8

        ' Move through the data one block at a time.
        Dim DecryptedBlock(), RawBlock() As Byte
        Dim i As Integer
        Dim Bytes As Integer = Me.SerializedObject.Length
        For i = 0 To bytes - 1 Step keySize

            If ((Bytes - i) > keySize) Then
                ReDim RawBlock(keySize - 1)
            Else
                ReDim RawBlock(Bytes - i - 1)
            End If

            ' Copy a block of data.
            'Buffer.BlockCopy(ObjectBytes, i, RawBlock, 0, RawBlock.Length)
            Me.SerializedObject.Read(RawBlock, 0, RawBlock.Length)

            ' Decrypt a block of data.
            DecryptedBlock = Rsa.Decrypt(RawBlock, False)

            ' Write the decrypted data to the in-memory stream.
            ObjectStream.Write(DecryptedBlock, 0, DecryptedBlock.Length)
        Next

        ObjectStream.Position = 0
        Dim f As New BinaryFormatter()
        Return f.Deserialize(ObjectStream)

    End Function

End Class

<Serializable()> _
Public Class LoginInfo
    Public EmailAddress As String
    Public TimeStamp As DateTime
    Public ObjRef As Byte()
End Class



<Serializable()> _
Public Class LargeEncryptedObject

    Private SerializedObject As New MemoryStream()
    Private EncryptedDynamicKey() As Byte

    Public Sub New(ByVal objectToEncrypt As Object, ByVal publicKeyXml As String)

        ' Generate the new symmetric key.
        ' In this example, we'll use the Rijndael algorithm.
        Dim Rijn As New RijndaelManaged()

        ' Encrypt the RijndaelManaged.Key and RijndaleManaged.IV properties.
        ' Store the data in the EncryptedDynamicKey member variable.
        ' (Asymmetric encryption code omitted.)

        ' Wrap the object in a stream that encrypts automatically.
        Dim cs As New CryptoStream(Me.SerializedObject, Rijn.CreateEncryptor(), CryptoStreamMode.Write)

        Dim f As New BinaryFormatter()
        f.Serialize(cs, objectToEncrypt)
        cs.FlushFinalBlock()

    End Sub

    Public Shared Function Deserialize(ByVal encryptedObjectBytes() As Byte) As EncryptedObject
        ' Deserialize the SignedObject.
        Dim ObjectStream As New MemoryStream()
        ObjectStream.Write(encryptedObjectBytes, 0, encryptedObjectBytes.Length)
        ObjectStream.Position = 0
        Dim f As New BinaryFormatter()
        Return CType(f.Deserialize(ObjectStream), EncryptedObject)
    End Function

    Public Function Serialize() As Byte()
        Dim f As New BinaryFormatter()
        Dim ObjectStream As New MemoryStream()
        f.Serialize(ObjectStream, Me)
        Return ObjectStream.ToArray()
    End Function


    Public Function DecryptContainedObject(ByVal keyPairXml As String) As Object

        ' Generate the new symmetric key.
        Dim Rijn As New RijndaelManaged()

        ' Decrypt the EncryptedDynamic key member variable, and use it to set
        ' the RijndaelManaged.Key and RijndaleManaged.IV properties.
        ' (Asymmetric decryption code omitted.)

        Dim ms As New MemoryStream()
        Dim cs As New CryptoStream(ms, Rijn.CreateDecryptor(), CryptoStreamMode.Write)

        Dim i, BytesRead As Integer
        Dim Bytes(1023) As Byte
        For i = 0 To Me.SerializedObject.Length
            BytesRead = Me.SerializedObject.Read(Bytes, 0, Bytes.Length)
            cs.Write(Bytes, 0, BytesRead)
        Next
        cs.FlushFinalBlock()

        ' Now deserialize the decrypted memory stream.
        ms.Position = 0
        Dim f As New BinaryFormatter()
        Return f.Deserialize(ms)

    End Function

End Class
