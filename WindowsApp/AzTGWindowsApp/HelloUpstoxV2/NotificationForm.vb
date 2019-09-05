Imports System.Threading

Public Class NotificationForm

    '<Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    'Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean
    'End Function

    'Private Const SWP_NOSIZE As Integer = &H1
    'Private Const SWP_NOMOVE As Integer = &H2

    'Private Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)
    'Private Shared ReadOnly HWND_NOTOPMOST As New IntPtr(-2)

    'Public Function MakeTopMost()
    '    Return SetWindowPos(Me.Handle(), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    'End Function

    'Public Function MakeNormal()
    '    Return SetWindowPos(Me.Handle(), HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    'End Function

    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        Try
            Me.Close()
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub FadeoutForm()
        For i = 1 To 100
            Threading.Thread.Sleep(10)
            SetOpacity(i / 100)
        Next
        Threading.Thread.Sleep(2000)
        For i = 1 To 100
            Threading.Thread.Sleep(10)
            SetOpacity((100 - i) / 100)
        Next
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

    Private Sub SetOpacity(ByVal Opacity As Double)
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(Sub() SetOpacity(Opacity)))
                Exit Sub
            End If
            Me.Opacity = Opacity
            Me.Refresh()
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub NotificationForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Location = New Point(ScreenWidth - Me.Width - 3, ScreenHeight - Me.Height - 3)
        Dim Thread As New Thread(AddressOf Me.FadeoutForm)
        Thread.IsBackground = True
        Thread.Start()
    End Sub
End Class