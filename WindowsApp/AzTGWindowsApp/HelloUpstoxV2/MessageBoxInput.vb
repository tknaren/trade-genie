Imports System.Threading

Public Class MessageBoxInput

    Public InputString As String = ""

    Private Sub MessageBoxInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = SystemIcons.Information
    End Sub

    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        InputString = TextBoxInput.Text
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub MessageBoxInput_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        TextBoxInput.Focus()
    End Sub
End Class