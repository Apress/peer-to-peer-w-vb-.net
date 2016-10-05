Imports System.IO
Imports System.Reflection
Imports TaskComponent
Imports System.Security.Policy
Imports System.Security
Imports System.Security.Permissions

Module Module1

    Sub Main()

        ' Create a permission set with the permissions the
        ' dynamically loaded assembly should have. 
        Dim SandBoxPerms As New NamedPermissionSet("Sandbox", PermissionState.None)
        SandBoxPerms.AddPermission(New SecurityPermission(SecurityPermissionFlag.Execution))

        ' Create a policy level that uses this permission set.
        Dim Policy As PolicyLevel = PolicyLevel.CreateAppDomainLevel()
        Policy.AddNamedPermissionSet(SandBoxPerms)

        ' The policy collection automatically includes an "everything" and
        ' a "nothing" permission set. We need to use these.
        Dim None As NamedPermissionSet = Policy.GetNamedPermissionSet("Nothing")
        Dim All As NamedPermissionSet = Policy.GetNamedPermissionSet("Everything")

        ' We need to distinguish between ordinary code (that will be allowed
        ' to do everything), and sandboxed code, that will be given
        ' the restricted permission set.
        Dim SandboxCondition As New SandboxMembershipCondition()
        Dim AllCondition As New AllMembershipCondition()

        ' The default group grants nothing.
        Dim RootCodeGroup As New FirstMatchCodeGroup(AllCondition, New PolicyStatement(None))

        ' Code with the Sandbox evidence is given execute permission only.
        Dim SandboxCodeGroup As New UnionCodeGroup(SandboxCondition, New PolicyStatement(SandBoxPerms))

        ' All other code will be given full permission.
        Dim AllCodeGroup As New UnionCodeGroup(AllCondition, New PolicyStatement(All))

        ' Add these membership conditions.
        RootCodeGroup.AddChild(SandboxCodeGroup)
        RootCodeGroup.AddChild(AllCodeGroup)
        Policy.RootCodeGroup = RootCodeGroup

        ' Set this policy into action for the current application.
        AppDomain.CurrentDomain.SetAppDomainPolicy(Policy)

        ' Create the evidence that identifies assemblies that should be sandboxed.
        Dim Evidence As New Evidence()
        Evidence.AddHost(New SandboxEvidence())

        ' Load an assembly from a file.
        ' We specify the evidence to use as an extra parameter.
        Dim TaskAssembly As System.Reflection.Assembly
        TaskAssembly = System.Reflection.Assembly.LoadFrom("PrimeNumberTest.dll", Evidence)

        ' Instantiate a class from the assembly.
        Dim TaskProcess As IGenericTask
        TaskProcess = CType(TaskAssembly.CreateInstance("TaskProcessor"), IGenericTask)


        Dim FromValue As Integer = 1
        Dim ToValue As Integer = 1000

        Dim ms As New MemoryStream()
        Dim w As New BinaryWriter(ms)
        
        w.Write(FromValue)
        w.Write(ToValue)

        Dim InputData() As Byte = ms.ToArray()
        Dim OutputData() As Byte = TaskProcess.DoTask(InputData)

        ms = New MemoryStream(OutputData)

        Dim r As New BinaryReader(ms)
        Do Until ms.Position = ms.Length
            Console.WriteLine(r.ReadInt32())
        Loop

        Console.ReadLine()
    End Sub

End Module


<Serializable()> _
Public NotInheritable Class SandboxEvidence
End Class

<Serializable()> _
Public NotInheritable Class SandboxMembershipCondition
    Implements IMembershipCondition

    Public Function Check(ByVal ev As Evidence) As Boolean _
      Implements IMembershipCondition.Check

        Dim Evidence As Object
        For Each Evidence In ev
            If TypeOf Evidence Is SandboxEvidence Then
                Return True
            End If
        Next
        Return False

    End Function

    Public Function Copy() As IMembershipCondition Implements IMembershipCondition.Copy
        Return New SandboxMembershipCondition()
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean Implements IMembershipCondition.Equals
        Return (obj Is Me)
    End Function

    Public Overloads Overrides Function ToString() As String _
     Implements IMembershipCondition.ToString
        Return "SandboxMembershipCondition"
    End Function

    Public Sub FromXml(ByVal e As SecurityElement) Implements ISecurityEncodable.FromXml
        Throw New NotImplementedException()
    End Sub

    Public Function ToXml() As SecurityElement Implements ISecurityEncodable.ToXml
        Throw New NotImplementedException()
    End Function

    Public Sub FromXml(ByVal e As SecurityElement, ByVal Level As PolicyLevel) _
     Implements ISecurityPolicyEncodable.FromXml
        Throw New NotImplementedException()
    End Sub

    Public Function ToXml(ByVal Level As PolicyLevel) As SecurityElement _
     Implements ISecurityPolicyEncodable.ToXml
        Throw New NotImplementedException()
    End Function

End Class
