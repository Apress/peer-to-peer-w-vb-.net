Public Class Erastothenes

    Public Shared Function FindPrimes(ByVal fromNumber As Integer, ByVal toNumber As Integer) As Integer()

        Dim List(toNumber - fromNumber) As Integer

        'create an array containing all integers between the two specified numbers
        Dim i As Integer
        For i = 0 To List.Length - 1
            List(i) = fromNumber
            fromNumber += 1
        Next


        'find out the module for each item in list, divided by each d, where
        'd is < or == to sqrt(to)
        'if the remainder is 0, the nubmer is a composite, and thus
        'we mark its position with 0 in the marks array,
        'otherwise the number is a prime, and thus mark it with 1
        Dim MaxDiv As Integer = CType(Math.Floor(Math.Sqrt(toNumber)), Integer)

        Dim Mark(List.Length - 1) As Integer

        Dim j As Integer
        For i = 0 To List.Length - 1
            For j = 2 To MaxDiv
                If (Not List(i) = j) And (List(i) Mod j = 0) Then
                    Mark(i) = 1
                End If
            Next
        Next


        'create new array that contains only the primes, and return that array
        Dim Primes As Integer
        For i = 0 To Mark.Length - 1
            If Mark(i) = 0 Then
                Primes += 1
            End If
        Next

        Dim Ret(Primes - 1) As Integer
        Dim Curs As Integer
        For i = 0 To Mark.Length - 1
            If Mark(i) = 0 Then
                Ret(Curs) = List(i)
                Curs += 1
            End If
        Next

        Return Ret

    End Function


End Class
