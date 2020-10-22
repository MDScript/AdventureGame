Module Module1

    Function f(ByVal x As Double)

        Dim xVal As Double = x

        Dim format As String = "x^2"

        Dim y As Double = x ^ 2

        Return y / 2

    End Function

    Sub drawLine(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double)

        Dim sx As Double = x1
        Dim sy As Double = y1
        Dim ex As Double = x2
        Dim ey As Double = y2

        Dim xDif As Double = ex - sx
        Dim yDif As Double = ey - sy
        Dim hypot As Double = ((xDif ^ 2) + (yDif ^ 2)) ^ 0.5

        Dim multiplier As Double = 1 / hypot

        Dim gradient As Double = (ey - sy) / (ex - sx)

        Dim xStep As Double = xDif * multiplier
        Dim yStep As Double = yDif * multiplier

        For x = 0 To Int(xDif)
            Console.SetCursorPosition()
            Console.Write("x")

        Next

    End Sub

    Sub Main()

        ' drawing graphs
        ' [PLAN]
        ' get user input
        ' decode user input into it's seperate parts
        ' get value for gradient
        ' 
        ' 
        ' 
        ' 
        ' 

        'Console.WriteLine("Enter expression in form : y = mx + c <e.g: y = sin(x) + 3 | y = 3x | y = x^2 + 2x + 5>")
        'Dim userIn As String
        'userIn = Console.ReadLine()

        Dim exp As String = "y = x^2"

        Dim yMax As Integer = 25
        Dim xMax As Integer = 50

        Dim xValues(xMax) As Double

        For x = 0 To xMax - 1
            xValues(x) = f(x / 2)
        Next

        For i = 0 To xMax - 1

            If xValues(i) < 36 Then
                Console.SetCursorPosition(i, 36 - xValues(i))
                Console.Write("x")
            End If
        Next

        For i = 0 To xMax - 2
            Try
                drawLine(i / 2, xValues(i), (i + 1) / 2, xValues(i + 1))
            Catch
                'Do nothing
            End Try
        Next

        Console.ReadKey()

    End Sub

End Module
