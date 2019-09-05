Imports System.Threading

Public Class MessageBoxOk
    Public EnableAutoClose As Boolean = False
    Public AutoCloseTimeInSeconds As Integer = 10

    Private Sub MessageBoxOk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If EnableAutoClose Then
            Dim Thread As New Thread(AddressOf Me.AutoCloseForm)
            Thread.IsBackground = True
            Thread.Start()
        End If
    End Sub

    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub AutoCloseForm()
        Threading.Thread.Sleep(AutoCloseTimeInSeconds * 1000)
        CloseForm()
    End Sub

    Private Sub CloseForm()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf CloseForm))
                Exit Sub
            End If
            Me.Close()
        Catch Ex As Exception
        End Try
    End Sub
End Class