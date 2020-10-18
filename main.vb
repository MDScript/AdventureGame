Imports System
Module Module1

    Dim makingChoice As Boolean = True
    Dim CR As String = "mainroom"
    Dim px As Integer = 3
    Dim py As Integer = 3

    Dim ROOMS As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

    ' room format: room as 7x7 array, bottom row as connecting room ids

    Dim MAINROOM As Array = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"0", "0", "0", "2", "0", "0", "0"},
                             {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"riddleroom", "placeholder1", "", "nothing", "", "", ""}
        }

    Dim RIDDLEROOM As Array = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "0"},
                               {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "s", "0", "0", "1"}, {"1", "1", "1", "0", "1", "1", "1"}, {"nothing", "mainroom", "", "riddlerslair", "", "", ""}
        }

    Dim PLACEHOLDER1 As Array = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"0", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "0", "1", "1", "1"}, {"mainroom", "nothing", "", "", "", "", ""}
        }

    Dim RIDDLERSLAIR As Array = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "p"},
                                 {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "0", "1", "1", "1"}, {"nothing", "keycode", "riddleroom", "victory", "", "", ""}
        }

    Dim VICTORY As Array = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"},
                         {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"nothing", "nothing", "riddlerslair", "nothing", "", "", ""}
        }

    Dim KEYCODE As Array = {{"1", "1", "1", "1", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"0", "0", "0", "0", "0", "0", "1"},
                            {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"riddlerslair", "nothing", "nothing", "nothing", "", "", ""}
        }

    Function printRoom(ByVal Room As Array)

        Dim r As Array = Room

        For y = 0 To 6
            For x = 0 To 6
                Select Case r(y, x)
                    Case "0"
                        Console.BackgroundColor = ConsoleColor.Green
                    Case "1"
                        Console.BackgroundColor = ConsoleColor.DarkGreen
                    Case "2"
                        Console.BackgroundColor = ConsoleColor.Red
                    Case "o"
                        Console.BackgroundColor = ConsoleColor.Yellow
                    Case "p"
                        Console.BackgroundColor = ConsoleColor.Gray
                End Select
                Console.Write("  ")
            Next
            Console.Write(vbCrLf)
        Next
    End Function

    Function handlePlayerMovement(ByVal currentRoom As Array, ByVal keyPress As Char)

        currentRoom(py, px) = "0"

        Select Case keyPress
            Case "a"
                If px = 0 And ROOMS.Item(CR)(3, 0) = "0" Then
                    px = 7
                    CR = currentRoom(7, 0)
                    currentRoom = ROOMS.Item(CR)
                End If
                If currentRoom(py, px - 1) <> "1" Then
                    px -= 1
                End If
            Case "d"
                If px = 6 And ROOMS.Item(CR)(3, 6) = "0" Then
                    px = -1
                    CR = currentRoom(7, 1)
                    currentRoom = ROOMS.Item(CR)
                End If
                If currentRoom(py, px + 1) <> "1" Then
                    px += 1
                End If
            Case "w"
                If py = 0 And ROOMS.Item(CR)(0, 3) = "0" Then
                    py = 7
                    CR = currentRoom(7, 2)
                    currentRoom = ROOMS.Item(CR)
                End If
                If currentRoom(py - 1, px) <> "1" Then
                    py -= 1
                End If
            Case "s"
                If py = 6 And ROOMS.Item(CR)(6, 3) = "0" Then
                    py = -1
                    CR = currentRoom(7, 3)
                    currentRoom = ROOMS.Item(CR)
                End If
                If currentRoom(py + 1, px) <> "1" Then
                    py += 1
                End If
        End Select

        If currentRoom(py, px) = "p" Then
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine("You have come across a puzzle! On completion, you will gain access to the room ahead!")
            Console.ReadKey()
            Minigame.startGame()
        End If

        currentRoom(py, px) = "2"

    End Function

    Sub Main()
        ROOMS.Add("mainroom", MAINROOM)
        ROOMS.Add("riddleroom", RIDDLEROOM)
        ROOMS.Add("placeholder1", PLACEHOLDER1)
        ROOMS.Add("riddlerslair", RIDDLERSLAIR)
        ROOMS.Add("keycode", KEYCODE)
        ROOMS.Add("victory", VICTORY)

        Dim toExit As Boolean = False

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("Welcome to the adventure game!" & vbCrLf & vbCrLf & " To play, type 'play'" & vbCrLf & " To Exit, type 'exit' or tap 'e' at any point during the game" & vbCrLf)

        While makingChoice
            Console.Write("> ")
            Dim chosenOption As String = Console.ReadLine()

            Select Case chosenOption.ToLower()
                Case "play"
                    Console.WriteLine("You Have Chosen To Play The Game")
                    makingChoice = False
                Case "exit"
                    Console.WriteLine(vbCrLf & "[Exiting game]")
                    toExit = True
                    makingChoice = False
            End Select
        End While

        Dim moveTo As Char = Nothing

        While Not toExit
            moveTo = Nothing

            Console.BackgroundColor = ConsoleColor.Black
            Console.ForegroundColor = ConsoleColor.Green
            Console.Clear()

            Console.WriteLine(vbCrLf & "How To Play" & vbCrLf & "    - Press 'w', 'a', 's', 'd' to Move" & vbCrLf & "    - Press 'e' to exit at any point in the game" & vbCrLf & "    - Press 'i' to access your inventory" & vbCrLf & "    - Press 'o' to open an object" & vbCrLf & "    - Press 'y' or 'n' for options Yes or No respectively" & vbCrLf)
            printRoom(ROOMS.Item(CR))

            Console.ForegroundColor = ConsoleColor.Black
            Console.BackgroundColor = ConsoleColor.Black

            moveTo = (Console.ReadKey()).KeyChar
            handlePlayerMovement(ROOMS.Item(CR), moveTo)

            If LCase(moveTo) = "e" Then
                Console.BackgroundColor = ConsoleColor.Black
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(vbCrLf & "[Exiting game]")
                toExit = True
            End If
        End While

        Console.Write(vbCrLf & "Press Any Key to Close" & vbCrLf & "> ")
        Console.ReadKey()

    End Sub
End Module

Module Minigame
    Public complete As Boolean = False
    Dim selected_bolt As Integer = 0
    Dim incorrectBolt() As String = {" ", " ", "1", "1", "2", "1", "1", " ", " "}
    Dim barHeights() As Integer = {0, 0, 0, 0, 0}
    Dim bars As Array = {{" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}}

    Sub setup()
        complete = False
        selected_bolt = 0
        incorrectBolt = {" ", " ", "1", "1", "2", "1", "1", " ", " "}
        barHeights = {0, 0, 0, 0, 0}
        bars = {{" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}, {" ", " ", " ", " ", " ", " ", " ", " ", " "}}
    End Sub

    Function shiftBolt(ByVal bolt_index As Integer, ByVal shift As Integer)
        Dim temporaryBolt() As String = {" ", " ", " ", " ", " ", " ", " ", " ", " "}
        Try ' apply shifting logic to the bar
            For i = 0 To 8
                If bars(bolt_index, i) <> " " Then
                    temporaryBolt(i + shift) = bars(bolt_index, i)
                End If
            Next
            For i = 0 To 8
                bars(bolt_index, i) = temporaryBolt(i)
            Next
            barHeights(selected_bolt) += shift
        Catch
        End Try
    End Function

    Sub printBolts()
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("The Cyan dot hovers under the bolt selected, to scroll across the bolts, press the left adn right arrow keys!" & vbCrLf & "To complete the puzzle, all the red parts must be in line with the green dot!" & vbCrLf & "In order to move a bolt up or down, use the up and down arrow keys!" & vbCrLf)
        For row = 0 To 8
            Console.Write("  ")
            For bolt = 0 To 4
                If bars(bolt, row) = "1" Then
                    Console.BackgroundColor = ConsoleColor.White
                ElseIf bars(bolt, row) = "2" Then
                    If complete Or row = 4 Then
                        Console.BackgroundColor = ConsoleColor.Green
                    Else
                        Console.BackgroundColor = ConsoleColor.Red
                    End If
                End If
                Console.Write("  ")
                Console.BackgroundColor = ConsoleColor.Black
                Console.Write("  ")
            Next
            If row = 4 Then
                Console.BackgroundColor = ConsoleColor.Green
                Console.Write("  ")
            End If
            Console.BackgroundColor = ConsoleColor.Black
            Console.Write(vbCrLf)
        Next

        For i = 0 To 4
            Console.Write("  ")
            If i = selected_bolt Then
                Console.BackgroundColor = ConsoleColor.Cyan
                Console.Write("  ")
            Else
                Console.Write("  ")
            End If
            Console.BackgroundColor = ConsoleColor.Black
        Next
    End Sub

    Sub startGame()
        setup() ' make sure all variables are reset
        Randomize() ' set random seed for random number generation

        For BOLT = 0 To 4
            While True ' this loop will execute until the bolt generated is not the same as the correctly positioned bolt
                For segment = 0 To 8
                    bars(BOLT, segment) = " "
                Next

                Dim SegementStart As Integer = Int(Rnd() * 5)
                barHeights(BOLT) = SegementStart

                For segment = SegementStart To SegementStart + 4
                    bars(BOLT, segment) = "1"
                    If segment - SegementStart = 2 Then
                        bars(BOLT, segment) = "2"
                    End If
                Next

                For segment = 0 To 8
                    If bars(BOLT, segment) <> incorrectBolt(segment) Then
                        Exit While
                    End If
                Next
            End While
        Next

        While Not complete
            Console.Clear()
            printBolts()
            Dim input As ConsoleKeyInfo = Console.ReadKey()

            Select Case input.Key
                Case 38 ' up
                    If barHeights(selected_bolt) > 0 Then
                        shiftBolt(selected_bolt, -1)
                    End If
                    Exit Select
                Case 40 ' down
                    If barHeights(selected_bolt) < 4 Then
                        shiftBolt(selected_bolt, 1)
                    End If
                    Exit Select
                Case 39 ' right
                    If selected_bolt < 4 Then
                        selected_bolt += 1.0
                    End If
                    Exit Select
                Case 37 ' left
                    If selected_bolt > 0 Then
                        selected_bolt -= 1
                    End If
                    Exit Select
            End Select

            complete = True

            For i = 0 To 4
                If bars(i, 4) <> "2" Then
                    complete = False
                End If
            Next

            If complete Then
                Console.Clear()
                printBolts()

                Console.Write(vbCrLf & "Well done! you have completed the puzzle!" & vbCrLf & "Press any key to continue" & vbCrLf & "> ")
                Console.ReadKey()

            End If
        End While
    End Sub
End Module
