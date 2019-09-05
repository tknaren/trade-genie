Imports System.Threading

Public Class MessageBoxOkCancel
    Private Sub MessageBoxOk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub KryptonButton2_Click(sender As Object, e As EventArgs) Handles KryptonButton2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub
End Class