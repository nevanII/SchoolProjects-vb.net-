Imports System.Console


Module Module1

    Sub Main()
        PellNumbers()


        ReadKey()

    End Sub
    Sub PellNumbers()

        Dim n1, n2, n3, ninput As Integer
        n1 = 0
        n2 = 1
        n3 = (n2 * 2) + n1




        WriteLine("What is the number that you would like to loop to?")
        ninput = ReadLine()

        WriteLine("The Pell numbers up to {0} are :", ninput)
        Write(n1)
        Write(",")
        For i = 1 To ninput

            n1 = n2
            Write(n1)
            Write(",")

            n2 = n3


            n3 = (n2 * 2) + n1
        Next

        Write(n2)
        Write(",")
        Write(n3)

        ReadKey()
    End Sub

End Module
