Imports System.Threading

Module GlobalSubs
    Public Sub ShowTrayNotification(ByVal Title As String, ByVal Message As String)
        If String.IsNullOrEmpty(Title) Then Exit Sub
        If String.IsNullOrEmpty(Message) Then Exit Sub
        ThreadPool.QueueUserWorkItem(New WaitCallback(Sub() ShowTrayNotification_Background(Title, Message)), Nothing)
    End Sub

    Private Sub ShowTrayNotification_Background(ByVal Title As String, ByVal Message As String)
        Try
            Using Frm As New NotificationForm()
                Frm.LabelTitle.Text = Title
                Frm.LabelMessage.Text = Message
                Frm.TopMost = True
                Frm.Opacity = 0
                Application.Run(Frm)
            End Using
        Catch Ex As Exception
        End Try
    End Sub
End Module
