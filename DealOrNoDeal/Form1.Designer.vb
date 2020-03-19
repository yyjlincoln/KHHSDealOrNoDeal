<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SelectionTxt = New System.Windows.Forms.TextBox()
        Me.confirmBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'SelectionTxt
        '
        Me.SelectionTxt.Location = New System.Drawing.Point(1424, 468)
        Me.SelectionTxt.Margin = New System.Windows.Forms.Padding(6)
        Me.SelectionTxt.Name = "SelectionTxt"
        Me.SelectionTxt.Size = New System.Drawing.Size(196, 35)
        Me.SelectionTxt.TabIndex = 0
        '
        'confirmBtn
        '
        Me.confirmBtn.Location = New System.Drawing.Point(1397, 342)
        Me.confirmBtn.Margin = New System.Windows.Forms.Padding(6)
        Me.confirmBtn.Name = "confirmBtn"
        Me.confirmBtn.Size = New System.Drawing.Size(150, 42)
        Me.confirmBtn.TabIndex = 1
        Me.confirmBtn.Text = "Select"
        Me.confirmBtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AllowDrop = True
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1362, 184)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(202, 24)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Starting Game..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(317, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 24)
        Me.Label2.TabIndex = 3
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1850, 1157)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.confirmBtn)
        Me.Controls.Add(Me.SelectionTxt)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SelectionTxt As TextBox
    Friend WithEvents confirmBtn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
