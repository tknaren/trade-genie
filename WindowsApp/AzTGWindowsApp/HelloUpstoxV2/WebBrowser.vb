Imports System.Threading

Public Class WebBrowser
    Public Url As String = ""

    Private Sub AutoCloseForm()
        Threading.Thread.Sleep(5 * 60 * 1000)
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

    Private Sub WebBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Thread As New Thread(AddressOf Me.AutoCloseForm)
        Thread.IsBackground = True
        Thread.Start()
        WebBrowser1.Navigate(Url)
    End Sub

    Private Sub WebBrowser1_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        Try
            'https://howutrade.in/?code=sasas55sas5a5as55ass5ass5as5aas&status=success
            Dim CurrentUrl As String = WebBrowser1.Url.ToString
            If CurrentUrl.Contains("code=") Then
                Dim Str1 As String = CurrentUrl.Substring(CurrentUrl.IndexOf("code=") + 5)
                Dim StrArray() As String = Str1.Split(New String() {"&"}, StringSplitOptions.None)
                Dim Code As String = StrArray(0)
                AccessCodeLocal = Code
                Me.Close()
            End If
        Catch Ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, Ex.Message, BoxStyle.Critical)
        End Try
    End Sub
End Class