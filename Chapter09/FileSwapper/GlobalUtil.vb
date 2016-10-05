Imports System.Net
Imports System.IO
Imports System.Text
Imports FileSwapper.localhost


Public Class Global

    ' Contains information about the current peer.
    Public Shared LoggedIn As Boolean = False
    Public Shared Identity As New Peer()

    ' Lists files that are available for other peers.
    Public Shared SharedFiles() As SharedFile

    ' Provides access to configuration settings that are stored in the registry.
    Public Shared Settings As New RegistrySettings()

End Class


Public Class App

    ' Holds a reference to the web service proxy.
    Private Shared Discovery As New DiscoveryService()

    Public Shared SearchThread As Search
    Public Shared DownwnloadThread As FileDownloadQueue
    Public Shared UploadThread As FileServer

    Public Shared Sub Login()

        Global.Identity.Guid = Guid.NewGuid
        Global.Identity.IP = Dns.GetHostByName(Dns.GetHostName).AddressList(0).ToString()

        Global.LoggedIn = Discovery.Register(Global.Identity)

    End Sub

    Public Shared Sub Logout()
        If Global.LoggedIn Then Discovery.Unregister(Global.Identity)
    End Sub

    Public Shared Sub RefreshLogin()
        If Global.LoggedIn Then Discovery.RefreshRegistration(Global.Identity)
    End Sub

    Public Shared Function PublishFiles() As Boolean

        Try
            ' Perform a failsafe check in case old registry settings
            ' that point to a directory that no longer exists.
            If Not Directory.Exists(Global.Settings.SharePath) Then
                Global.Settings.SharePath = Application.StartupPath
            End If

            Dim Dir As New DirectoryInfo(Global.Settings.SharePath)

            Dim Files() As FileInfo = Dir.GetFiles()
            Dim FileList As New ArrayList()
            Dim File As FileInfo
            Dim IsMP3 As Boolean

            For Each File In Files

                IsMP3 = Path.GetExtension(File.Name).ToLower() = ".mp3"

                If Path.GetExtension(File.Name).ToLower() = ".tmp" Then
                    ' Ignore all temporary files.
                ElseIf (Not IsMP3) And Global.Settings.ShareMP3Only Then
                    ' Ignore non-MP3 file depending on setting.
                Else
                    Dim SharedFile As New SharedFile()
                    SharedFile.Guid = Guid.NewGuid()
                    SharedFile.FileName = File.Name
                    SharedFile.FileCreated = File.CreationTime

                    If IsMP3 Then
                        SharedFile.Keywords = MP3Util.GetMP3Keywords(File.FullName)
                    Else
                        ' Determine some other way to set keywords,
                        ' perhaps by filename or depending on the file
                        ' type.
                        ' The default (no keywords), will prevent the
                        ' file from appearing in a search.
                    End If

                    FileList.Add(SharedFile)
                End If
            Next

            Global.SharedFiles = CType(FileList.ToArray(GetType(SharedFile)), SharedFile())

            Return Discovery.PublishFiles(Global.SharedFiles, Global.Identity)

        Catch Err As Exception
            MessageBox.Show(Err.ToString())
        End Try

    End Function

    Public Shared Function SearchForFile(ByVal keywords() As String) As SharedFile()
        Return Discovery.SearchForFile(keywords)
    End Function

End Class

Public Class KeywordUtil

    Private Shared NoiseWords() As String = {"the", "for", "and", "or"}
    Public Shared Function ParseKeywords(ByVal keywordString As String) As String()

        ' Split the list of words into an array.
        Dim Keywords() As String
        Dim Delimeters() As Char = {" ", ",", "."}
        Keywords = keywordString.Split(Delimeters)

        ' Add each valid word into an ArrayList.
        Dim FilteredWords As New ArrayList()
        Dim Word As String
        For Each Word In Keywords
            If Word.Trim() <> "" And Word.Length > 1 Then
                If Array.IndexOf(NoiseWords, Word.ToLower()) = -1 Then
                    FilteredWords.Add(Word)
                End If
            End If
        Next

        ' Convert the ArrayList into a normal string array.
        Return FilteredWords.ToArray(GetType(String))

    End Function

End Class

Public Class MP3Util

    Public Shared Function GetMP3Keywords(ByVal filename As String) As String()

        Dim fs As New FileStream(filename, FileMode.Open)

        ' Read the MP3 tag.
        fs.Seek(0 - 128, SeekOrigin.End)
        Dim Tag(2) As Byte
        fs.Read(Tag, 0, 3)

        If Encoding.ASCII.GetString(Tag).Trim() = "TAG" Then

            Dim KeywordString As New StringBuilder()
            ' Title.
            KeywordString.Append(GetTagData(fs, 30))
            ' Artist.
            KeywordString.Append(" ")
            KeywordString.Append(GetTagData(fs, 30))
            ' Album.
            KeywordString.Append(" ")
            KeywordString.Append(GetTagData(fs, 30))
            ' Year. 
            'KeywordString.Append(" ")
            'KeywordString.Append(GetTagData(fs, 4))
            ' Comment.
            'KeywordString.Append(" ")
            'KeywordString.Append(GetTagData(fs, 30))

            fs.Close()
            Dim Keywords() As String = KeywordUtil.ParseKeywords(KeywordString.ToString())
            Return Keywords
        Else
            fs.Close()
            Dim EmptyArray() As String = {}
            Return EmptyArray
        End If

    End Function

    Public Shared Function GetTagData(ByVal stream As Stream, ByVal length As Integer) As String

        Dim Bytes(length - 1) As Byte
        stream.Read(Bytes, 0, length)

        Dim TagData As String = Encoding.ASCII.GetString(Bytes)

        ' Trim nulls.
        Dim TrimChars() As Char = {" ", vbNullChar}
        TagData = TagData.Trim(TrimChars)
        Return TagData

    End Function

End Class

