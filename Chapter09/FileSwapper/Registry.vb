Imports Microsoft.Win32

Public Class RegistrySettings

    Public SharePath As String
    Public ShareMP3Only As Boolean
    Public MaxUploadThreads As Integer
    Public MaxDownloadThreads As Integer
    Public Port As Integer

    Public Sub Load()

        Dim Key As RegistryKey
        Key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey( _
             "Software\FilesSwapper\Settings")

        SharePath = Key.GetValue("SharePath", Application.StartupPath)
        Port = CType(Key.GetValue("LocalPort", "8000"), Integer)
        ShareMP3Only = CType(Key.GetValue("OnlyShareMP3", "True"), Boolean)
        MaxUploadThreads = CType(Key.GetValue("MaxUploadThreads", "2"), Integer)
        MaxDownloadThreads = CType(Key.GetValue("MaxDownloadThreads", "2"), Integer)

    End Sub

    Public Sub Save()

        Dim Key As RegistryKey
        Key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey( _
             "Software\FilesSwapper\Settings")

        Key.SetValue("SharePath", SharePath)
        Key.SetValue("LocalPort", Port.ToString())
        Key.SetValue("OnlyShareMP3", ShareMP3Only.ToString())
        Key.SetValue("MaxUploadThreads", MaxUploadThreads.ToString())
        Key.SetValue("MaxDownloadThreads", MaxDownloadThreads.ToString())

    End Sub

End Class