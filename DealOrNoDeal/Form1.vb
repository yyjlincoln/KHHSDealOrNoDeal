Public Class Form1
    Public Cases As New List(Of Double)
    Public Values As New List(Of Double) From {0.5, 1, 2, 5, 10, 20, 50, 100, 150, 200, 250, 500, 750, 1000, 2000, 3000, 4000, 5000, 10000, 15000, 20000, 30000, 50000, 75000, 100000, 200000}
    Public Selected As New Double
    Public nl = Environment.NewLine
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        init()
        updateLabel()
    End Sub
    Public Sub updateLabel()
        Label1.Text = ""
        ' = 0: selected
        ' < 0: opened
        ' > 0: not opened
        For x As Integer = 0 To Cases.Count() - 1
            Label1.Text = Label1.Text + "Case #" + CType(x, String) + ": "
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
    End Sub

    Private Sub log(message)
        Console.WriteLine(message)
    End Sub


End Class
