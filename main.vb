Imports System
Module Program

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
                                 {"riddleroom", "", "", "nothing", "", "", ""}
        }

    Dim RIDDLEROOM As Array = {
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "0", "0", "0", "0"},
                                 {"1", "0", "0", "0", "0", "0", "1"},
                                 {"1", "0", "0", "s", "0", "0", "1"},
                                 {"1", "1", "1", "0", "1", "1", "1"},
                                 {"nothing", "mainroom", "", "", "", "", ""}
        }

    Function printRoom(ByVal Room As Array)

        Dim r As Array = Room

        For y = 0 To 6

            For x = 0 To 6

                If r(y, x) = "0" Then
                    Console.BackgroundColor = ConsoleColor.Green
                End If

                If r(y, x) = "1" Then
                    Console.BackgroundColor = ConsoleColor.Blue
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
                If currentRoom(py - 1, px) <> "1" Then
                    py -= 1
                End If
            Case "s"
                If currentRoom(py + 1, px) <> "1" Then
                    py += 1
                End If
        End Select

        currentRoom(py, px) = "2"

    End Function

    Sub Main()

        ROOMS.Add("mainroom", MAINROOM)
        ROOMS.Add("riddleroom", RIDDLEROOM)

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
                    makingChoice = False
            End Select

        End While

        Dim moveTo As Char = Nothing

        While Not toExit

            moveTo = Nothing

            Console.BackgroundColor = ConsoleColor.Black
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

            moveTo = (Console.ReadKey()).KeyChar

            handlePlayerMovement(ROOMS.Item(CR), moveTo)

        End While

        ' enter to close
        Console.Write(vbCrLf & "Press Any Key to Close" & vbCrLf & "> ")
        Console.ReadKey()

    End Sub
End Module
