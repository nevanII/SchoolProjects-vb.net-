Imports System.Console


Public Class Player
    Private PlayerName As String
    Private PlayerSymbol As Char

    Public Property Name
        Get
            Return PlayerName
        End Get
        Set(ByVal value)
            Dim valid As Boolean = False
            'Do Until valid = True
            '    Try
            '        PlayerName = value
            '        valid = True
            '    Catch ex As Exception
            '        WriteLine(ex.Message)
            '    End Try
            'Loop
            PlayerName = value
        End Set
    End Property

    Public Property Symbol
        Get
            Return PlayerSymbol
        End Get


        Set(ByVal value)
            Dim valid As Boolean = False

            PlayerSymbol = value
        End Set


    End Property

    Public Overridable Function InputACoordinate()
        Dim uinput As Integer
        Dim valid As Boolean = False
        Do Until valid = True
            WriteLine("{0}, what is the Co Ordinate you would like to use?", PlayerName)
            Try
                uinput = ReadLine()
            Catch ex As Exception
                Console.ForegroundColor = ConsoleColor.Red
                WriteLine(ex.Message)
                Console.ResetColor()

            End Try
            If uinput > 0 And uinput < 8 Then


                valid = True
            Else
                Console.ForegroundColor = ConsoleColor.Red
                WriteLine("Invalid input, please use a number from 1-7")
                Console.ResetColor()

            End If
        Loop

        Return uinput

    End Function


End Class

Class Game
    Private Players(1) As Player
    Private Board(7, 6) As Char
    Private Complete As Boolean = False




    Private Sub boardPop()
        For x = 1 To 7
            For y = 1 To 6
                Board(x, y) = "-"
            Next
        Next

        For y = 1 To 6
            Board(y, 0) = "|"

        Next
    End Sub

    Public Sub New(ByVal aPlayer1 As Player, ByVal aPlayer2 As Player)

        Players(0) = aPlayer1

        Players(1) = aPlayer2



        boardPop()
        PickSymbols()


        Dim count As Integer = 0


        Do Until Complete = True Or count = 42
            Clear()

            PlaceSymbol(Players(0))
            checkForWins(Players(0))
            DisplayBoard(Players(0))

            PlaceSymbol(Players(1))
            checkForWins(Players(1))
            DisplayBoard(Players(1))

            ReadKey()
        Loop

    End Sub

    Public Property isComplete()
        Get
            Return Complete
        End Get
        Set(ByVal value)
        End Set
    End Property

    Private Sub PickSymbols()
        WriteLine("We will flip a coin to see who picks their symbol")

        Dim randNum As Integer
        Randomize()
        randNum = Math.Ceiling(Int(2) * Rnd())

        If randNum = 1 Then
            WriteLine("Player one will pick first, X or O?")
            Dim uinput As String = ReadLine()
            If uinput.ToUpper = "X" Then
                Players(0).Symbol = Convert.ToChar(uinput)
                Players(1).Symbol = "O"
            ElseIf uinput.ToUpper = "O" Then
                Players(0).Symbol = Convert.ToChar(uinput)
                Players(1).Symbol = "X"
            Else
                Console.ForegroundColor = ConsoleColor.Red
                WriteLine("You have inputted an invalid character.")
                Console.ResetColor()
                ReadKey()
                End
            End If


        Else


            WriteLine("Player two will pick first, X or O?")
            Dim uinput As String = ReadLine()
            If uinput.ToUpper = "X" Then
                Players(1).Symbol = Convert.ToChar(uinput)
                Players(0).Symbol = "O"
            ElseIf uinput.ToUpper = "O" Then
                Players(1).Symbol = Convert.ToChar(uinput)
                Players(0).Symbol = "X"
            Else
                Console.ForegroundColor = ConsoleColor.Red
                WriteLine("You have inputted an invalid character")
                Console.ResetColor()
                ReadKey()
                End
            End If
        End If
    End Sub

    Private Sub DisplayBoard(ByRef player As Player)
        If player.Symbol = "X" Then
            Console.ForegroundColor = ConsoleColor.Cyan
            For x = 0 To 7
                For y = 0 To 6
                    Write(Board(x, y))
                Next
                WriteLine()
            Next
            Console.ResetColor()
        Else
            Console.ForegroundColor = ConsoleColor.DarkMagenta
            For x = 0 To 7
                For y = 0 To 6
                    Write(Board(x, y))
                Next
                WriteLine()
            Next
            Console.ResetColor()
        End If

    End Sub

    Private Sub PlaceSymbol(ByVal player As Player)
        Dim CoOrdinate, counter As Integer
        Dim valid As Boolean
        Dim NotFull As Boolean = False


        Do Until NotFull = True
            CoOrdinate = player.InputACoordinate()
            valid = CheckColumnForNull(CoOrdinate, counter)


            If valid = True Then
                WriteLine("Column is not full")
                counter += 1
                Board(CoOrdinate, counter) = player.Symbol
                NotFull = True
            Else
                Console.ForegroundColor = ConsoleColor.Red
                WriteLine("That column is full.")
                Console.ResetColor()

            End If
        Loop


    End Sub

    Private Function CheckColumnForNull(ByVal ColumnNo, ByRef counter)
        Dim Empty As Boolean = True
        counter = -1


        For y = 0 To 5
            If Board(ColumnNo, y) = "-" Then
            Else
                counter += 1

            End If
        Next


        If counter >= 5 Then
            Empty = False
        Else
            Empty = True
        End If
        Return Empty


    End Function

    Private Sub checkForWins(ByVal currentPlayer As Player)

        checkwithValues(4, 6, 1, 1, 0, currentPlayer)

        checkwithValues(7, 3, 1, 0, 1, currentPlayer)

        checkwithValues(4, 3, 1, 1, 1, currentPlayer)

        checkwithValues(4, 6, 4, 1, -1, currentPlayer)

    End Sub

    Private Sub checkwithValues(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal constantX As Integer, ByVal constantY As Integer, ByVal currentPlayer As Player)

        Dim symbol As Char = currentPlayer.Symbol

        For i As Integer = 1 To x

            For j As Integer = z To y

                If board(i, j) = symbol And board(i + constantX, j + constantY) = symbol And board(i + 2 * constantX, j + 2 * constantY) = symbol And board(i + 3 * constantX, j + 3 * constantY) = symbol Then 

                    CompleteGame()
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine(currentPlayer.Name & " has won the game!!!!!")
                    Console.ResetColor()


                End If

            Next

        Next

    End Sub

    Private Function CompleteGame()
        Complete = True
        Return Complete
    End Function

End Class

Class Computer
    Inherits Player
    Public Overrides Function InputACoordinate()
        Dim randNum As Integer
        Randomize()
        randNum = Math.Ceiling(Int(7) * Rnd())

        Return randNum

    End Function
End Class

Class MediumComputer
    Inherits Computer

End Class

Class HardComputer
    Inherits Computer
    'Public Overloads Function InputACoordinate(ByVal board(,)) As Object


    'End Function


End Class

Module Module1

    Sub Main()

        Dim players(2) As Player

        players(1) = New Player

        players(2) = New Player

        players(1).Name = "Bob"

        players(2).Name = "Alice"

        Dim userOption As Integer = 0

        Do Until userOption = 4

            Console.WriteLine("Would you like to:")

            Console.WriteLine("1. New Game (player vs player)")

            Console.WriteLine("2. New Game (player vs computer)")

            Console.WriteLine("3. New Game (computer vs player)")

            Console.WriteLine("Please type the number of the option you would like")

            userOption = Console.ReadLine

            Select Case userOption

                Case 1

                    'New game of PvP
                    Dim Connect4Game As New Game(players(1), players(2))

                Case 2

                    'New game of PvC
                    Dim computerPlayer As New Computer
                    computerPlayer.Name = "Computer"
                    Dim Connect4Game As New Game(players(1), computerPlayer)

                Case 3

                    'New game of CvC
                    Dim computer1 As New Computer
                    Dim computer2 As New Computer
                    computer1.Name = "AlphaZero"
                    computer2.Name = "Stockfish"

                    Dim Connect4Game As New Game(computer1, computer2)


                Case 4

                    'Exit

                Case Else

                    Console.WriteLine("That was not a valid option please try again")

                    Console.Clear()

            End Select

        Loop





    End Sub

End Module
