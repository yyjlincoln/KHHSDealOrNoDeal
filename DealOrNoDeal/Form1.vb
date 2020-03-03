' Finite state machine
' load -> updateState() to selection -> <userInput> -> handleSelection() -> updateState()

Public Class Form1
    Public Cases As New List(Of Double)
    Public Values As New List(Of Double) From {0.5, 1, 2, 5, 10, 20, 50, 100, 150, 200, 250, 500, 750, 1000, 2000, 3000, 4000, 5000, 10000, 15000, 20000, 30000, 50000, 75000, 100000, 200000}
    Public Selected As New Double
    Public nl = Environment.NewLine
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
        For x As Integer = 0 To Cases.Count() - 1
            Label1.Text = Label1.Text + "Case #" + CType(x + 1, String) + ": "
            If Cases(x) = 0 Then
                Label1.Text = Label1.Text + "Selected"
            ElseIf Cases(x) < 0 Then
                Label1.Text = Label1.Text + "$" + CType(Cases(x) * -1, String)
            Else
                Label1.Text = Label1.Text + "Available"
            End If
            Label1.Text = Label1.Text + nl
        Next

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
            Label2.Text = "Invalid selection."
        Else
            Selected = Cases(selection - 1)
            Cases(selection - 1) = 0
            Label2.Text = "You've selected #" + CType(selection, String)
            updateState()
        End If

    End Sub

    Public Sub handleOpen(selection As Integer)
        If selection > Cases.Count() Or selection < 1 Then
            Label2.Text = "Invalid selection."
        Else
            If Cases(selection - 1) = 0 Then
                Label2.Text = "You can not choose the one you've already selected."
            ElseIf Cases(selection - 1) < 0 Then
                Label2.Text = "This one is already open."
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
                Return

        End Select


    End Sub

    Private Sub log(message)
        Console.WriteLine(message)
    End Sub
    Private Sub ConfirmBtn_Click(sender As Object, e As EventArgs) Handles confirmBtn.Click
        Try
            Select Case state
                Case "selection"
                    handleSelection(SelectionTxt.Text)
                Case "open"
                    handleOpen(SelectionTxt.Text)
                Case Else
                    log("Exception: unmapped state." + state)
            End Select
            SelectionTxt.Text = ""
        Catch ex As Exception
            SelectionTxt.Text = ""
            log(ex)
        End Try

    End Sub
End Class
