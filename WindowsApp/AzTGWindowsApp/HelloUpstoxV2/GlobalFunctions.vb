Module GlobalFunctions
    Public Function ShowMsgBoxOk(ByVal Title As String, ByVal Message As String, Optional ByVal BoxStyle As BoxStyle = BoxStyle.Information, Optional ByVal EnableAutoClose As Boolean = False, Optional ByVal AutoCloseTimeInSeconds As Integer = 10) As DialogResult
        Try
            If String.IsNullOrEmpty(Title) Then Return DialogResult.Cancel
            If String.IsNullOrEmpty(Message) Then Return DialogResult.Cancel

            AutoCloseTimeInSeconds = Math.Max(1, AutoCloseTimeInSeconds)
            AutoCloseTimeInSeconds = Math.Min(3600, AutoCloseTimeInSeconds)

            Dim Frm As New MessageBoxOk
            Select Case BoxStyle
                Case HelloUpstoxV2.BoxStyle.Critical
                    Frm.Icon = SystemIcons.Error
                Case HelloUpstoxV2.BoxStyle.Exclamation
                    Frm.Icon = SystemIcons.Exclamation
                Case HelloUpstoxV2.BoxStyle.Information
                    Frm.Icon = SystemIcons.Information
                Case Else
                    Frm.Icon = SystemIcons.Warning
            End Select
            Frm.Text = Title
            Frm.EnableAutoClose = EnableAutoClose
            Frm.AutoCloseTimeInSeconds = AutoCloseTimeInSeconds
            Frm.LabelMessage.Text = Message
            Return Frm.ShowDialog()
        Catch Ex As Exception
        End Try
        Return DialogResult.Cancel
    End Function

    Public Function ShowMsgBoxOkCancel(ByVal Title As String, ByVal Message As String, Optional ByVal BoxStyle As BoxStyle = BoxStyle.Information) As DialogResult
        Try
            If String.IsNullOrEmpty(Title) Then Return DialogResult.Cancel
            If String.IsNullOrEmpty(Message) Then Return DialogResult.Cancel

            Dim Frm As New MessageBoxOkCancel
            Select Case BoxStyle
                Case HelloUpstoxV2.BoxStyle.Critical
                    Frm.Icon = SystemIcons.Error
                Case HelloUpstoxV2.BoxStyle.Exclamation
                    Frm.Icon = SystemIcons.Exclamation
                Case HelloUpstoxV2.BoxStyle.Information
                    Frm.Icon = SystemIcons.Information
                Case Else
                    Frm.Icon = SystemIcons.Warning
            End Select
            Frm.Text = Title
            Frm.LabelMessage.Text = Message
            Return Frm.ShowDialog()
        Catch Ex As Exception
        End Try
        Return DialogResult.Cancel
    End Function

    Public Function ShowMsgBoxInput(ByVal Title As String, ByVal Message As String) As String
        Try
            If String.IsNullOrEmpty(Title) Then Return ""
            If String.IsNullOrEmpty(Message) Then Return ""

            Dim Frm As New MessageBoxInput
            Frm.Text = Title
            Frm.LabelMessage.Text = Message
            Frm.ShowDialog()
            Return Frm.InputString
        Catch Ex As Exception
        End Try
        Return ""
    End Function
End Module
