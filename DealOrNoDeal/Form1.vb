' Finite state machine
' load -> updateState() to selection -> <userInput> -> handleSelection() -> updateState()

Public Class Form1
    Public Cases As New List(Of Double)
    Public Labels As New List(Of Label)
    Public Buttons As New List(Of Button)
    Public LabelAccending As New List(Of Label)

    Public Selected As New Double

    Public LabelsInit As New List(Of Label)
    Public ValuesInit As New List(Of Double) From {0.5, 1, 2, 5, 10, 20, 50, 100, 150, 200, 250, 500, 750, 1000, 2000, 3000, 4000, 5000, 10000, 15000, 20000, 30000, 50000, 75000, 100000, 200000}
    Public BankTime As New List(Of Integer) From {2, 3, 4, 5, 7, 10, 14, 19} ' 26 in total, choose 1 to keep, choose 6
    Public state = "load"

    Const button_width = 70
    Const button_height = 50
    Const button_up = 10 ' top margin
    Const button_left = 10 ' left margin

    Const label_width = 50
    Const label_height = 20
    Const label_up = 10
    Const label_left = 10

    Const reservedWidthLeft = 70
    Const reservedWidthRight = 70



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
        update_form()
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


    Public Sub update_form()
        Dim adjustedWidth = Me.Width - reservedWidthLeft - reservedWidthRight
        ' Calculate Delta_left, which is to get the ( Form width - (max number of buttons that can be placed * space used
        ' each) ) / 2 + reserved space at left
        Dim delta_left As Double = (adjustedWidth - (Int(adjustedWidth / (button_left + button_width)) - 1) * (button_left + button_width) - button_left) / 2
        If delta_left < 0 Then
            delta_left = 0
        End If

        Dim delta_top = 100
        Dim heightAccumulation = button_up + delta_top
        Dim leftAccumulation = button_left + delta_left
        Dim left_start = reservedWidthLeft
        Dim top_start = 0

        'For buttons, we don't need to adjust height since we don't even have it and it does not care about height

        For x As Integer = 0 To Buttons.Count() - 1
            ' Calculate if next button is still in the window                   Otherwise in case the window is too small, draw at least 1
            If leftAccumulation < adjustedWidth - button_width - button_left Or leftAccumulation = button_left + delta_left Then
                Dim btn = Buttons(x)
                btn.Height = button_height
                btn.Width = button_width
                btn.Top = heightAccumulation + top_start
                btn.Left = leftAccumulation + left_start
                leftAccumulation = leftAccumulation + button_width + button_left
            Else
                leftAccumulation = button_left + delta_left ' Reset leftAccumulation since entering a new line
                heightAccumulation = heightAccumulation + button_up + button_height ' Accumulate height
                x = x - 1 ' Since the button relocation failed due to insufficient width, do it again.
            End If
        Next

        delta_top = 100
        delta_left = 5 ' For area 1, at the left, still leave 5px at left. Inner-padding
        heightAccumulation = label_up + delta_top
        leftAccumulation = label_left + delta_left
        adjustedWidth = reservedWidthLeft - delta_left
        Dim adjustedHeight = Me.Height
        Dim drawArea = 0
        left_start = 0
        top_start = 0


        For x As Integer = 0 To LabelAccending.Count() - 1

            If leftAccumulation < adjustedWidth - label_width - label_left Or leftAccumulation = label_left + delta_left Then

                If heightAccumulation < adjustedHeight - 2 * label_height - label_up Then
                    Dim lbl = LabelAccending(x)
                    lbl.Height = label_height
                    lbl.Width = label_width
                    lbl.Top = heightAccumulation + top_start
                    lbl.Left = leftAccumulation + left_start
                    Console.WriteLine("Set label #" & x & "at Top=" & lbl.Top & "And Left=" & lbl.Left)
                    heightAccumulation = heightAccumulation + label_up + label_height
                Else
                    ' Test if there is enough space to create another column before switching area
                    If adjustedWidth - leftAccumulation - label_width - label_left >= 0 Then
                        '                        MsgBox("NextLine")
                        leftAccumulation = leftAccumulation + label_width + label_left ' Update LeftAccumulation
                        heightAccumulation = label_up + delta_top ' Reset heightaccumulation
                        x = x - 1
                    Else
                        If drawArea = 0 Then
                            drawArea = 1
                            adjustedWidth = reservedWidthRight
                            adjustedHeight = Me.Height
                            leftAccumulation = label_left + delta_left
                            heightAccumulation = label_up + delta_top
                            left_start = Me.Width - reservedWidthRight - 20
                            x = x - 1
                        End If
                    End If
                End If
            End If

        Next

        ' Draw labels


        ' Draw the rest




    End Sub
    Public Sub init()
        ' Initialize Labels and Buttons
        For x As Integer = 0 To ValuesInit.Count() - 1
            ' Add button
            Dim btn As Button = New Button()
            btn.Text = "Case " & x + 1
            Me.Controls.Add(btn)
            Buttons.Add(btn)
            Dim ckn As Integer = x
            AddHandler btn.Click, Function() handleClick(ckn)
            Dim lbl As Label = New Label()
            lbl.Text = "$" & ValuesInit(x)
            LabelsInit.Add(lbl)
            LabelAccending.Add(lbl)
            Me.Controls.Add(lbl)
        Next



        Dim ran = New Random()
        For x As Integer = 0 To ValuesInit.Count() - 1
            Dim i As Integer = ran.Next(0, ValuesInit.Count())
            Cases.Add(ValuesInit(i))
            Labels.Add(LabelsInit(i))
            ValuesInit.RemoveAt(i)
            LabelsInit.RemoveAt(i)
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
            'Invalid selection
        Else
            If Cases(selection - 1) = 0 Then
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
    Public Sub handleSubmission(caseNo)
        Try
            ' MsgBox("Hand" & caseNo)
            Select Case state
                Case "selection"
                    handleSelection(caseNo)
                Case "open"
                    handleOpen(caseNo)
                Case "finish"
                    MsgBox("Game finished")
                Case "final"
                    handleFinal(caseNo)
                Case Else
                    log("Exception: unmapped state." + state)
                    MsgBox("Exception: unmapped state. Details in the log.")
            End Select
        Catch ex As Exception
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
        If MsgBox("Bank has given you an offer of $" + CType(offervalue, String) + "! Do you want to accept it?", MsgBoxStyle.YesNo, "Bank Offer") = MsgBoxResult.Yes Then
            MsgBox("You've got $" + CType(offervalue, String))
            state = "finish"
        Else
            state = "bankfinish"
        End If
        updateState()
    End Sub

    Public Function handleClick(num)
        Buttons(num).Enabled = False
        handleSubmission(num + 1)
        Return 0
    End Function
End Class
