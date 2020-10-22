Imports System
Module Module1

    Dim makingChoice As Boolean
    Dim CR As String
    Dim px As Integer
    Dim py As Integer
    Dim PrevX As Integer
    Dim PrevY As Integer

    Dim ROOMS As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

    ' room format: room as 7x7 array, bottom row as connecting room ids

    Public MAINROOM As Array
    Public RIDDLEROOM As Array
    Public TOLOCKER As Array
    Public LOCKER As Array
    Public RIDDLERSLAIR As Array
    Public VICTORY As Array
    Public KEYCODE As Array
    Public PLINTH As Array
    Public toExit As Boolean
    Sub resetGame()

        toExit = False

        makingChoice = True
        CR = "mainroom"
        px = 3
        py = 3
        PrevX = 3
        PrevY = 3

        MAINROOM = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"0", "0", "0", "2", "0", "0", "0"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"riddleroom", "tolocker", "plinth", "nothing", "", "", ""}}
        RIDDLEROOM = {{"1", "1", "1", "1", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "0"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "s", "0", "0", "1"}, {"1", "1", "1", "k", "1", "1", "1"}, {"nothing", "mainroom", "nothing", "riddlerslair", "", "", ""}}
        TOLOCKER = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"0", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"mainroom", "nothing", "locker", "nothing", "", "", ""}}
        LOCKER = {{"1", "1", "1", "1", "1", "1", "1"}, {"1", "0", "0", "o", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "T", "0", "0", "1"}, {"1", "1", "1", "0", "1", "1", "1"}, {"riddlerslair", "nothing", "nothing", "tolocker", "", "", ""}}
        RIDDLERSLAIR = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "p"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "k", "1", "1", "1"}, {"nothing", "keycode", "riddleroom", "victory", "", "", ""}}
        VICTORY = {{"1", "1", "1", "0", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"nothing", "nothing", "riddlerslair", "nothing", "", "", ""}}
        KEYCODE = {{"1", "1", "1", "1", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"0", "0", "0", "o", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "1", "1", "1", "1"}, {"riddlerslair", "nothing", "nothing", "nothing", "", "", ""}}
        PLINTH = {{"1", "1", "1", "1", "1", "1", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "B", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "0", "0", "0", "0", "0", "1"}, {"1", "1", "1", "0", "1", "1", "1"}, {"nothing", "nothing", "nothing", "mainroom", "", "", ""}}

    End Sub

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
                    Case "A"
                        Console.BackgroundColor = ConsoleColor.Yellow
                    Case "p"
                        Console.BackgroundColor = ConsoleColor.Gray
                    Case "k"
                        Console.BackgroundColor = ConsoleColor.Cyan
                    Case "B"
                        Console.BackgroundColor = ConsoleColor.Cyan
                    Case "N"
                        Console.BackgroundColor = ConsoleColor.DarkGray
                End Select
                Console.Write("  ")
            Next
            Console.Write(vbCrLf)
        Next
    End Function

    Function handlePlayerMovement(ByVal currentRoom As Array, ByVal keyPress As Char)

        currentRoom(py, px) = "0"

        PrevX = px
        PrevY = py

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

        Console.ForegroundColor = ConsoleColor.Green
        If currentRoom(py, px) = "p" Then
            Console.WriteLine("You have come across a puzzle! On completion, you will gain access to the room ahead! (any key to move on)")
            Console.ReadKey()
            Minigame.startGame()
        ElseIf currentRoom(py, px) = "k" Then
            Console.WriteLine("You have happened upon a keycode lock! complete this to gain access to the next room (any key to move on)")
            Console.ReadKey()
            Select Case CR
                Case "riddleroom"
                    UserCode.getKeyCode("1912")
                Case "riddlerslair"
                    UserCode.getKeyCode("5834")
            End Select
            If Not UserCode.correct Then
                px = PrevX
                py = PrevY
            End If
        ElseIf currentRoom(py, px) = "T" Then
            Console.Write("You have fallen into a pit!" & vbCrLf & "Roll a Dice to escape! if the number rolled is above 2, you will escape, else you will die!" & vbCrLf & "Press any key to roll " & vbCrLf & ">")
            Randomize()
            Console.ReadKey()
            If Int(Rnd() * 7) > 2 Then
                Console.Clear()
                Console.Write("The number you rolled was greater than 3! you managed to pull yourself free of the pit" & vbCrLf & "Press Any key to continue" & vbCrLf & "> ")
                Console.ReadKey()
            Else
                Console.Clear()
                Console.Write("The number you rolled was lower than 3!" & vbCrLf & "You were unable to pull yourself free of the pit and so starved to death!" & vbCrLf & "Press Any key to continue" & vbCrLf & "> ")
                Console.ReadKey()
                CR = "mainroom"
                MAINROOM(3, 3) = "2"
                px = 3
                py = 3
                PrevX = 3
                PrevY = 3
                Exit Function
            End If
        ElseIf currentRoom(py, px) = "o" Then
            Console.WriteLine("you found a chest")
            Select Case CR
                Case "locker"
                    Inventory.addItem("1912", "keycode")
                    Inventory.addItem("sword", "weapon")
                Case "keycode"
                    Inventory.addItem("dagger", "weapon")
            End Select
            Console.ReadKey()
        End If

        If CR = "victory" Then
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.Cyan
            Console.WriteLine("Congratulations! you were able to escape the dungeon!")
            Console.Write("Would you like to restart(r) or exit(e) the game?" & vbCrLf & "> ")
            Dim userIn As Char
            ' logic for replaying the game
            While userIn <> "r" And userIn <> "e"
                Console.ForegroundColor = ConsoleColor.Black
                userIn = LCase(Console.ReadKey().KeyChar)
            End While

            Console.ForegroundColor = ConsoleColor.Cyan


            Select Case userIn
                Case "r"
                    Console.WriteLine("Restarting Game")
                    Threading.Thread.Sleep(1000)
                    resetGame()
                    Console.Clear()
                    printRoom(MAINROOM)
                    currentRoom = MAINROOM
                    CR = "mainroom"
                    MAINROOM(3, 3) = "2"
                Case "e"
                    toExit = True
            End Select
        End If

        currentRoom(py, px) = "2"

        If CR = "plinth" And currentRoom(3, 3) = "B" Then
            Console.Clear()
            printRoom(currentRoom)
            Console.BackgroundColor = ConsoleColor.Black
            Console.WriteLine("You have entered the Plinth room!
There is a troll sleeping in the corner of the room, guarding the plinth and the final door keycode!
You can choose one of two options:
    1 - Kill the troll in its sleep (30% success rate)
    2 - Sneak to the plinth and take the keycode (70% chance to wake the troll and engage in combat)
")
            Dim userIn As String
            While LCase(userIn) <> "1" And LCase(userIn) <> "2"
                Console.Write("> ")
                userIn = Console.ReadKey().KeyChar.ToString
            End While

            Randomize()

            Select Case userIn
                Case "1"
                    If Int(Rnd() * 11) - 7 Then
                        Console.WriteLine(vbCrLf & "You were able to kill the troll in its sleep!
This allows you to retrieve the keycode from the plinth!
You head back to the starting room and the plinth room seals behind you!
")
                        Inventory.addItem("5834", "keycode")

                        Console.ReadKey()

                        px = 3
                        py = 1
                        PrevX = 3
                        PrevY = 1
                        CR = "mainroom"
                        currentRoom = MAINROOM

                        currentRoom(py, px) = "2"
                        MAINROOM(0, px) = "1"

                    Else
                        Console.WriteLine(vbCrLf & "As you sneak towards the troll, it senses your approach, quickly wakes and kills you.")
                        px = 3
                        py = 3
                        PrevX = 3
                        PrevY = 3
                        CR = "mainroom"
                        currentRoom = MAINROOM

                        Console.ReadKey()

                    End If
                Case "2"
                    ' 30% chance to wake troll and engage in combat (further 40% chance of victory)
                    Console.WriteLine(vbCrLf & "You edge towards the plinth slowly as no to wake the troll..")
                    If Int(Rnd() * 11) - 7 Then
                        Console.WriteLine(vbCrLf & "It was successful, you take the keycode from the plinth and hastily retreat! The room seals behind you!
")
                        Inventory.addItem("5834", "keycode")

                        px = 3
                        py = 1
                        PrevX = 3
                        PrevY = 1
                        CR = "mainroom"
                        currentRoom = MAINROOM

                        currentRoom(py, px) = "2"
                        MAINROOM(0, px) = "1"

                        Console.Write(vbCrLf & "Any key to continue" & vbCrLf & "> ")
                        Console.ReadKey()

                    Else
                        Console.WriteLine(vbCrLf & "It senses you and wakes, you must now engage in combat! Press any key to test your luck!")
                        Console.Write(vbCrLf & "Any key to continue" & vbCrLf & "> ")
                        Console.ReadKey()

                        If Int(Rnd() * 6 < 3) Then
                            Console.WriteLine(vbCrLf & "You were able to subdue the troll and now grab the keycode from the plinth!
The plinth room closes behind you")

                            Inventory.addItem("5834", "keycode")

                            px = 3
                            py = 1
                            PrevX = 3
                            PrevY = 1
                            CR = "mainroom"
                            currentRoom = MAINROOM

                            currentRoom(py, px) = "2"
                            MAINROOM(0, px) = "1"

                            Console.Write(vbCrLf & "Any key to continue" & vbCrLf & "> ")
                            Console.ReadKey()

                        Else
                            Console.WriteLine(vbCrLf & "The troll overpowered you! You die")
                            Console.Write(vbCrLf & "Any key to continue" & vbCrLf & "> ")
                            Console.ReadKey()
                            px = 3
                            py = 3
                            PrevX = 3
                            PrevY = 3
                            CR = "mainroom"
                            currentRoom = MAINROOM

                        End If
                    End If
            End Select
        End If


    End Function

    Sub Main()

        resetGame()

        ROOMS.Add("mainroom", MAINROOM)
        ROOMS.Add("plinth", PLINTH)
        ROOMS.Add("riddleroom", RIDDLEROOM)
        ROOMS.Add("tolocker", TOLOCKER)
        ROOMS.Add("locker", LOCKER)
        ROOMS.Add("riddlerslair", RIDDLERSLAIR)
        ROOMS.Add("keycode", KEYCODE)
        ROOMS.Add("victory", VICTORY)

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

            Console.WriteLine(vbCrLf & "How To Play" & vbCrLf & "    - Press 'w', 'a', 's', 'd' to Move" & vbCrLf & "    - Press 'e' to exit at any point in the game" & vbCrLf & "    - Press 'i' to access your inventory" & vbCrLf & vbCrLf)
            printRoom(ROOMS.Item(CR))

            Console.ForegroundColor = ConsoleColor.Black
            Console.BackgroundColor = ConsoleColor.Black

            moveTo = (Console.ReadKey()).KeyChar
            handlePlayerMovement(ROOMS.Item(CR), moveTo)

            If LCase(moveTo) = "i" Then
                Inventory.openInv()
            End If

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
        Console.WriteLine("The Cyan dot hovers under the bolt selected, to scroll across the bolts, press the left and right arrow keys!" & vbCrLf & "To complete the puzzle, all the red parts must be in line with the green dot!" & vbCrLf & "In order to move a bolt up or down, use the up and down arrow keys!" & vbCrLf)
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
                Case 40 ' down
                    If barHeights(selected_bolt) < 4 Then
                        shiftBolt(selected_bolt, 1)
                    End If
                Case 39 ' right
                    If selected_bolt < 4 Then
                        selected_bolt += 1.0
                    End If
                Case 37 ' left
                    If selected_bolt > 0 Then
                        selected_bolt -= 1
                    End If
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

Module UserCode

    Public correct As Boolean = False
    Sub getKeyCode(ByRef password As String)
        correct = False
        Dim current_input As String = ""
        Dim entering As Boolean = True
        Dim input As String

        While entering
            Console.ForegroundColor = ConsoleColor.Green
            Console.Clear()
            Console.WriteLine("Enter KeyCode To Pass" & vbCrLf & "You may type 'exit' to leave without completing")
            Console.Write("> ")

            input = Console.ReadLine()

            If input = password Then
                correct = True
                entering = False
                Console.Write(vbCrLf & "Password Correct" & vbCrLf & "Press any key to exit" & vbCrLf & "> ")
                Console.ReadKey()

            ElseIf input.ToLower() = "exit" Then
                entering = False
            End If
            Console.Clear()
        End While
    End Sub
End Module

Module Inventory

    Dim codes As Array = {" ", " "}
    Dim weapons As Array = {" ", " "}

    Sub openInv()

        Console.Clear()
        Console.ForegroundColor = ConsoleColor.White

        Console.WriteLine(" - [Inventory] -

Key Codes:")
        For i = 0 To 1
            Console.WriteLine("    " & codes(i))
        Next
        Console.WriteLine(vbCrLf & "Weapons:")

        For i = 0 To 1
            Console.WriteLine("    " & weapons(i))
        Next
        Console.WriteLine()

        Console.Write("Press Any key To Exit Inventory" & vbCrLf & "> ")

        Console.ReadKey()
        Console.Clear()

    End Sub

    Sub addItem(ByVal value As String, ByVal type As String)

        Select Case type
            Case "keycode"
                If codes(0) = " " Then
                    codes(0) = value
                Else
                    codes(1) = value
                End If
            Case "weapon"
                If weapons(0) = " " Then
                    weapons(0) = value
                Else
                    weapons(1) = value
                End If
        End Select

        Console.WriteLine(type & " [" & value & "] added to inventory!")

    End Sub

End Module
