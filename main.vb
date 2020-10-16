Imports System
Module Module1

    Dim makingChoice As Boolean = True
    Dim CR As String = "mainroom"
    Dim px As Integer = 3
    Dim py As Integer = 3

    Dim ROOMS As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

    ' room format: room as 7x7 array, bottom row as connecting room ids

    Dim MAINROOM As Array = {
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"0", "0", "0", "2", "0", "0", "0"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "1", "1", "1", "1", "1", "1"},
                                 {"riddleroom", "placeholder1", "", "nothing", "", "", ""}
        }

    Dim RIDDLEROOM As Array = {
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "0"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "s", "0", "0", "1"},
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"nothing", "mainroom", "", "riddlerslair", "", "", ""}
        }

    Dim PLACEHOLDER1 As Array = {
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"0", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"mainroom", "nothing", "", "", "", "", ""}
        }

    Dim RIDDLERSLAIR As Array = {
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "0"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"nothing", "victory", "riddleroom", "maze", "", "", ""}
        }

    Dim MAZE As Array = {
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "1", "1", "1", "1", "1", "1"},
                                 {"nothing", "nothing", "riddlerslair", "nothing", "", "", ""}
        }

    Dim VICTORY As Array = {
                                 {"1", "1", "1", "1", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"0", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "1", "1", "1", "1", "1", "1"},
                                 {"riddlerslair", "nothing", "nothing", "nothing", "", "", ""}
        }

    Function printRoom(ByVal Room As Array)

        Dim r As Array = Room

        For y = 0 To 6

            For x = 0 To 6

                If r(y, x) = "0" Then
                    Console.BackgroundColor = ConsoleColor.Green
                End If

                If r(y, x) = "1" Then
                    Console.BackgroundColor = ConsoleColor.DarkGreen
                End If

                If r(y, x) = "2" Then
                    Console.BackgroundColor = ConsoleColor.Red
                End If

                If r(y, x) = "o" Then
                    Console.BackgroundColor = ConsoleColor.Yellow
                End If

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

        currentRoom(py, px) = "2"

    End Function

    Sub Main()

        Minigame.startGame()

        ROOMS.Add("mainroom", MAINROOM)
        ROOMS.Add("riddleroom", RIDDLEROOM)
        ROOMS.Add("placeholder1", PLACEHOLDER1)
        ROOMS.Add("riddlerslair", RIDDLERSLAIR)
        ROOMS.Add("victory", VICTORY)
        ROOMS.Add("maze", MAZE)

        Dim toExit As Boolean = False

        ' Start The Story

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

            ' Write instructions on how to play the game
            Console.WriteLine("
How To Play
    - Press 'w', 'a', 's', 'd' to Move
    - Press 'e' to exit at any point in the game
    - Press 'i' to access your inventory
    - Press 'o' to open an object
    - Press 'y' or 'n' for options Yes or No respectively
")

            ' display the room the palyer is in

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

        ' enter to close
        Console.Write(vbCrLf & "Press Any Key to Close" & vbCrLf & "> ")
        Console.ReadKey()

    End Sub
End Module

Module Minigame

    Public complete As Boolean = False

    Sub startGame()

        Randomize()

        Dim incorrectBolt() As String = {" ", " ", "1", "1", "2", "1", "1", " ", " "}

        Dim bars As Array = {{" ", " ", " ", " ", " ", " ", " ", " ", " "},
                             {" ", " ", " ", " ", " ", " ", " ", " ", " "},
                             {" ", " ", " ", " ", " ", " ", " ", " ", " "},
                             {" ", " ", " ", " ", " ", " ", " ", " ", " "},
                             {" ", " ", " ", " ", " ", " ", " ", " ", " "}}

        For BOLT = 0 To 4

            While True

                For segment = 0 To 8
                    bars(BOLT, segment) = " "
                Next

                Dim SegementStart As Integer = Int(Rnd() * 5)

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

            For i = 0 To 8
                Console.Write(bars(BOLT, i))
            Next
            Console.WriteLine()
        Next

        Dim selected_bolt As Integer = 2

        While True
            Dim input As ConsoleKeyInfo = Console.ReadKey()

            Select Case input.Key
                Case 38
                    ' up
                    Console.WriteLine("up")
                    Exit Select
                Case 40
                    ' down
                    Console.WriteLine("down")
                    Exit Select
                Case 39
                    ' right
                    If selected_bolt < 4 Then
                        selected_bolt += 1
                    End If
                    Exit Select
                Case 37
                    ' left
                    If selected_bolt > 0 Then
                        selected_bolt -= 1
                    End If
                    Exit Select
            End Select

            Console.WriteLine(selected_bolt)
        End While

    End Sub

End Module
