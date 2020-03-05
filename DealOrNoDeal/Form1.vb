' Finite state machine
' load -> updateState() to selection -> <userInput> -> handleSelection() -> updateState()

Public Class Form1
    Public Cases As New List(Of Double)
    Public Values As New List(Of Double) From {0.5, 1, 2, 5, 10, 20, 50, 100, 150, 200, 250, 500, 750, 1000, 2000, 3000, 4000, 5000, 10000, 15000, 20000, 30000, 50000, 75000, 100000, 200000}
    Public Selected As New Double
    Public BankTime As New List(Of Integer) From {2, 3, 4, 5, 7, 10, 14, 19} ' 26 in total, choose 1 to keep, choose 6
    Public state = "load"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
        updateState()
    End Sub
    Public Sub updateLabel()
        Label1.Text = ""
        ' = 0: selected
        ' < 0: opened
        ' > 0: not opened
        Dim textToBeUpdated As String = ""
        For x As Integer = 0 To Cases.Count() - 1
            textToBeUpdated = textToBeUpdated + "Case #" + CType(x + 1, String) + ": "
            If Cases(x) = 0 Then
                textToBeUpdated = textToBeUpdated + "Selected"
            ElseIf Cases(x) < 0 Then
                textToBeUpdated = textToBeUpdated + "$" + CType(Cases(x) * -1, String)
            Else
                textToBeUpdated = textToBeUpdated + "Available"
            End If
            textToBeUpdated = textToBeUpdated + Environment.NewLine
        Next
        Label1.Text = textToBeUpdated

    End Sub

    Public Sub init()
        Dim ran = New Random()
        For x As Integer = 0 To Values.Count() - 1
            Dim i As Integer = ran.Next(0, Values.Count())
            Cases.Add(Values(i))
            Values.RemoveAt(i)
        Next
        updateLabel()
    End Sub

    Public Sub handleSelection(selection As Integer)
        If selection > Cases.Count() Or selection < 1 Then
            '            Label2.Text = "Invalid selection."
            'MsgBox("Invalid Selection")
        Else
            Selected = Cases(selection - 1)
            Cases(selection - 1) = 0
            Label2.Text = "You've selected #" + CType(selection, String)
            updateState()
        End If

    End Sub

    Public Sub handleOpen(selection As Integer)
        If selection > Cases.Count() Or selection < 1 Then
            'Label2.Text = "Invalid selection."
            'MsgBox("Invalid Selection")
        Else
            If Cases(selection - 1) = 0 Then
                '                Label2.Text = "You can not choose the one you've already selected."
                '                MsgBox("You can not choose the one you've already selected.")
                MsgBox("You MUST NOT open your briefcase. You are eliminated.")
                state = "finish"
            ElseIf Cases(selection - 1) < 0 Then
                '                Label2.Text = "This one is already open."
                MsgBox("This one is already open!")
                Return
            End If
            Cases(selection - 1) = Cases(selection - 1) * -1
        End If
        updateState()
    End Sub

    Public Sub updateState()
        updateLabel()

        Select Case state
            Case "load" 'Select the one that the player wanna keep
                Label2.Text = "Please chose one of these, and press confirm"
                state = "selection"
                Return
            Case "selection"
                Label2.Text = "Please select one to open."
                state = "open"
            Case "open"
                Dim no = 0
                For x As Integer = 0 To Cases.Count() - 1
                    If Cases(x) <= 0 Then
                        no = no + 1
                    End If
                Next
                log(BankTime.Contains(no))
                log(Cases.Count() - no)
                If BankTime.Contains(Cases.Count() - no) Then
                    bank()
                End If
                If Cases.Count() - no = 1 Then
                    Label2.Text = "Choose the last one that you want to keep"
                    state = "final"
                End If
            Case "bankfinish"
                state = "open"
            Case "finish"
                MsgBox("Game finished.")
            Case "final"
                state = "finish"
        End Select


    End Sub

    Private Sub log(message)
        Console.WriteLine(message)
    End Sub
    Private Sub ConfirmBtn_Click(sender As Object, e As EventArgs) Handles confirmBtn.Click
        handleSubmission()
    End Sub
    Public Sub handleFinal(choice As Integer)
        If Cases(choice - 1) < 0 Then
            MsgBox("That case is already open, and you ca not keep it.")
        ElseIf Cases(choice - 1) = 0 Then
            MsgBox("Case #" + CType(choice, String) + " has $" + CType(Selected, String) + ", and that's what you get.")
            Cases(choice - 1) = Selected * -1
            state = "finish"
        Else
            MsgBox("Case #" + CType(choice, String) + " has $" + CType(Cases(choice - 1), String) + ", and that's what you get.")
            Cases(choice - 1) = Cases(choice - 1) * -1
            state = "finish"
        End If
        updateState()


    End Sub
    Public Sub handleSubmission()
        Try
            Select Case state
                Case "selection"
                    handleSelection(SelectionTxt.Text)
                Case "open"
                    handleOpen(SelectionTxt.Text)
                Case "finish"
                    MsgBox("Game finished")
                Case "final"
                    handleFinal(SelectionTxt.Text)
                Case Else
                    log("Exception: unmapped state." + state)
                    MsgBox("Exception: unmapped state. Details in the log.")
            End Select
            SelectionTxt.Clear()
        Catch ex As Exception
            SelectionTxt.Clear()
            log(ex)
        End Try
    End Sub
    Function calcOffer() As Double
        Dim numberOfCases As Integer
        Dim valueOfCases As Double
        For x As Integer = 0 To Cases.Count() - 1
            If (Cases(x) <= 0) Then
                numberOfCases = numberOfCases + 1
                valueOfCases = valueOfCases + (Cases(x) * -1)
            End If
        Next
        Return 0.9 * (valueOfCases / numberOfCases)
    End Function
    Public Sub bank()
        Dim offervalue = calcOffer()
        If MsgBox("Bank has given you an offer of $" + CType(offervalue, String) + "! Do you want to accept it?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            MsgBox("You've got $" + CType(offervalue, String))
            state = "finish"
        Else
            state = "bankfinish"
        End If
        updateState()
    End Sub

    Private Sub SelectionTxt_TextChanged(sender As Object, e As KeyEventArgs) Handles SelectionTxt.KeyDown
        If e.KeyCode = Keys.Enter Then
            handleSubmission()
        End If
    End Sub
End Class
