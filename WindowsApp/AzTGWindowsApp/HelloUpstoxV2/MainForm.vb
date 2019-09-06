Imports System.Threading
Imports System.Text
Imports System.IO
Imports ComponentFactory.Krypton.Toolkit
Imports System.Net.Sockets
Imports System.Net


Public Class MainForm
    Delegate Sub SetRichBoxTextCallback(ByVal Message As String, ByVal LogType As LogType)
    Private Sub SetRichBoxText(ByVal Message As String, ByVal LogType As LogType)
        Try
            If KryptonRichTextBox1.InvokeRequired Then
                Dim d As New SetRichBoxTextCallback(AddressOf SetRichBoxText)
                Me.KryptonRichTextBox1.Invoke(d, New Object() {Message, LogType})
                Exit Sub
            End If

            Dim StrLogType As String
            Select Case LogType
                Case HelloUpstoxV2.LogType.AppEvent
                    StrLogType = "APPEVENT"
                Case HelloUpstoxV2.LogType.Debug
                    StrLogType = "DEBUG"
                Case HelloUpstoxV2.LogType.OrderUpdate
                    StrLogType = "ORDERUPDATE"
                Case HelloUpstoxV2.LogType.PositionUpdate
                    StrLogType = "POSITIONUPDATE"
                Case Else
                    StrLogType = "OTHERS"
            End Select
            KryptonRichTextBox1.Text &= Environment.NewLine & Now.ToString("HH:mm:ss.fff") & " " & StrLogType & " " & Message
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetControlCallback(ByVal Control As Control, ByVal Status As Boolean)
    Private Sub SetControl(ByVal Control As Control, ByVal Status As Boolean)
        Try
            If Control.InvokeRequired Then
                Dim d As New SetControlCallback(AddressOf SetControl)
                Control.Invoke(d, New Object() {Control, Status})
                Exit Sub
            End If
            Control.Enabled = Status
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetControlColorCallback(ByVal Control As Control, ByVal Color As Color)
    Private Sub SetControlColor(ByVal Control As Control, ByVal Color As Color)
        Try
            If Control.InvokeRequired Then
                Dim d As New SetControlColorCallback(AddressOf SetControlColor)
                Control.Invoke(d, New Object() {Control, Color})
                Exit Sub
            End If
            Control.BackColor = Color
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetKryptonLabelColorCallback(ByVal Control As ComponentFactory.Krypton.Toolkit.KryptonLabel, ByVal Color As Color)
    Private Sub SetKryptonLabelColor(ByVal Control As ComponentFactory.Krypton.Toolkit.KryptonLabel, ByVal Color As Color)
        Try
            If Control.InvokeRequired Then
                Dim d As New SetKryptonLabelColorCallback(AddressOf SetKryptonLabelColor)
                Control.Invoke(d, New Object() {Control, Color})
                Exit Sub
            End If
            Control.StateNormal.ShortText.Color1 = Color
            Control.StateNormal.ShortText.Color2 = Color
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetKryptonComboBoxItemsCallback(ByVal Control As ComponentFactory.Krypton.Toolkit.KryptonComboBox, ByVal Items As String())
    Private Sub SetKryptonComboBoxItems(ByVal Control As ComponentFactory.Krypton.Toolkit.KryptonComboBox, ByVal Items As String())
        Try
            If Control.InvokeRequired Then
                Dim d As New SetKryptonComboBoxItemsCallback(AddressOf SetKryptonComboBoxItems)
                Control.Invoke(d, New Object() {Control, Items})
                Exit Sub
            End If
            Control.Items.Clear()
            Control.Items.AddRange(Items)
            Control.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub SetControlTextCallback(ByVal Control As Control, ByVal Text As String)
    Private Sub SetControlText(ByVal Control As Control, ByVal Text As String)
        Try
            If Control.InvokeRequired Then
                Dim d As New SetControlTextCallback(AddressOf SetControlText)
                Control.Invoke(d, New Object() {Control, Text})
                Exit Sub
            End If
            Control.Text = Text
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Quotes_Received(sender As Object, e As UpstoxNet.QuotesReceivedEventArgs)
        'Console.WriteLine("SYMBOL: " & e.TrdSym & " LTP: " & e.LTP & " VOLUME: " & e.Volume & " LTT: " & e.LTT.ToString("dd-MMM-yy HH:mm:ss"))
    End Sub

    Private Sub UpdateMarketWatchFile()
        SyncLock (LockUpdateMarketWatchFile)
            Try
                File.WriteAllText(MarketWatchFileName, String.Join(vbNewLine, ListMarketWatchSymbol.ToArray))
            Catch Ex As Exception
            End Try
        End SyncLock
    End Sub

    Private Sub Order_Update(sender As Object, e As UpstoxNet.OrderUpdateEventArgs)
        SetRichBoxText("OrderId: " & e.OrderId & " Symbol: " & e.TrdSym & " Trans: " & e.TransType & " Qty: " & e.Quantity & " Status: " & e.Status, LogType.OrderUpdate)
        SyncLock (LockAddSymbol)
            Try
                Dim SymbolExch As String = e.TrdSym & "." & e.Exch
                If Not ListMarketWatchSymbol.Contains(SymbolExch) Then
                    Dim InstToken As String = Upstox.GetInstToken(e.Exch, e.TrdSym)
                    AddSymbolToMarketWatch(e.Exch, e.TrdSym, InstToken)
                End If
            Catch ex As Exception
            End Try
        End SyncLock
    End Sub

    Private Sub Position_Update(sender As Object, e As UpstoxNet.PositionUpdateEventArgs)
        SetRichBoxText("Symbol: " & e.TrdSym & " BuyQty: " & e.BoughtQty & " SellQty: " & e.SoldQty & " Mtm: " & e.MTM, LogType.PositionUpdate)
    End Sub

    Private Sub App_Update(sender As Object, e As UpstoxNet.AppUpdateEventArgs)
        Try
            SetRichBoxText("Code: " & e.EventCode & " Message: " & e.EventMessage, LogType.AppEvent)

            'Symbol downloaded - One time happens
            If e.EventCode = 4 Then
                SetControlText(KryptonLabel4, "Connected")
                SetKryptonLabelColor(KryptonLabel4, Color.DarkGreen)

                'Add Symbol to Market watch
                GetSymbolListFromMarketWatchFile()
                ProcessSymbolListMarketWatch()

                'Enable Symbol add
                SetControl(KryptonPanel6, True)
                SetKryptonComboBoxItems(KryptonComboBox1, Upstox.ListExch)

                'Set client Id
                SetControlText(KryptonLabel3, Upstox.Client_Id)

                'Enable Logout button
                SetControl(KryptonButton2, True)
            End If

            'User logged-out
            If e.EventCode = 2 Then
                'Disable Symbol add
                SetControl(KryptonPanel6, False)

                SetControlText(KryptonLabel4, "Logout")
                SetKryptonLabelColor(KryptonLabel4, Color.Crimson)
            End If

            'Network disconnected
            If e.EventCode = 5 Then
                If Upstox.Symbol_Download_Status Then
                    SetControlText(KryptonLabel4, "Disconnected")
                    SetKryptonLabelColor(KryptonLabel4, Color.Crimson)
                End If
            End If

            'Network connected
            If e.EventCode = 6 Then
                If Upstox.Symbol_Download_Status Then
                    SetControlText(KryptonLabel4, "Connected")
                    SetKryptonLabelColor(KryptonLabel4, Color.DarkGreen)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateMarketWatch()
        Do
            Threading.Thread.Sleep(1000)

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            If DisableMarketWatch Then Continue Do
            UpdateMarketWatch_Background()
        Loop
    End Sub

    Private Sub UpdateMarketWatch_Background()
        Try
            If KryptonDataGridView1.InvokeRequired Then
                KryptonDataGridView1.Invoke(New MethodInvoker(AddressOf UpdateMarketWatch_Background))
                Exit Sub
            End If

            With KryptonDataGridView1
                For RowIndex = 0 To .RowCount - 1
                    With .Rows(RowIndex)
                        Dim InstToken As String = .Cells(16).Value 'Last Column
                        Dim Exch As String = .Cells(0).Value
                        Dim TrdSym As String = .Cells(1).Value

                        Dim Ltp As Double = Upstox.GetLtp(Exch, TrdSym)
                        If Ltp = 0 Then Continue For

                        Dim BestBid As Double = Upstox.GetBestBid(Exch, TrdSym)
                        Dim BestAsk As Double = Upstox.GetBestAsk(Exch, TrdSym)
                        Dim Open As Double = Upstox.GetOpen(Exch, TrdSym)
                        Dim High As Double = Upstox.GetHigh(Exch, TrdSym)
                        Dim Low As Double = Upstox.GetLow(Exch, TrdSym)
                        Dim Close As Double = Upstox.GetClose(Exch, TrdSym)

                        Dim ATP As Double = Upstox.GetAtp(Exch, TrdSym)
                        Dim Volume As Double = Upstox.GetVolume(Exch, TrdSym)
                        Dim OpnInt As Double = Upstox.GetOpenInt(Exch, TrdSym)

                        Dim LastUpdateTime As String = Upstox.GetLUT(Exch, TrdSym).ToString("dd-MMM-yy HH:mm:ss")
                        Dim LastTradeTime As String = Upstox.GetLTT(Exch, TrdSym).ToString("dd-MMM-yy HH:mm:ss")
                        Dim NetQty As Double = Upstox.GetNetQty(Exch, TrdSym)
                        Dim MTM As Double = Upstox.GetMtm(Exch, TrdSym)

                        .Cells(2).Value = Ltp
                        .Cells(3).Value = BestBid
                        .Cells(4).Value = BestAsk
                        .Cells(5).Value = Open
                        .Cells(6).Value = High
                        .Cells(7).Value = Low
                        .Cells(8).Value = Close
                        .Cells(9).Value = ATP
                        .Cells(10).Value = Volume
                        .Cells(11).Value = OpnInt
                        .Cells(12).Value = LastUpdateTime
                        .Cells(13).Value = LastTradeTime
                        .Cells(14).Value = NetQty
                        .Cells(15).Value = MTM
                    End With
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ShowMarketDepth()
        Try
            If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            If KryptonDataGridView1.SelectedRows.Count > 0 Then
                With KryptonDataGridView1
                    Dim Exch As String = .SelectedRows(0).Cells(0).Value
                    Dim TrdSym As String = .SelectedRows(0).Cells(1).Value
                    Upstox.ShowMarketDepth(Exch, TrdSym)
                End With
            Else
                With KryptonDataGridView1
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Dim Exch As String = .Rows(RowIndex).Cells(0).Value
                    Dim TrdSym As String = .Rows(RowIndex).Cells(1).Value
                    Upstox.ShowMarketDepth(Exch, TrdSym)
                End With
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub DelSymbolFromMarketWatch()
        Try
            If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            If KryptonDataGridView1.SelectedRows.Count > 0 Then
                With KryptonDataGridView1
                    Dim InstToken As String = .SelectedRows(0).Cells(16).Value 'Last Column
                    Dim Exch As String = .SelectedRows(0).Cells(0).Value
                    Dim TrdSym As String = .SelectedRows(0).Cells(1).Value

                    Dim SymbolExch As String = TrdSym & "." & Exch
                    ListMarketWatchSymbol.Remove(SymbolExch)
                    UpdateMarketWatchFile()

                    'Unsubscribe Quotes
                    'Upstox.UnSubscribeQuotes(Exch, TrdSym)
                    .Rows.Remove(.SelectedRows(0))
                End With
            Else
                With KryptonDataGridView1
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Dim InstToken As String = .Rows(RowIndex).Cells(16).Value 'Last Column
                    Dim Exch As String = .Rows(RowIndex).Cells(0).Value
                    Dim TrdSym As String = .Rows(RowIndex).Cells(1).Value

                    Dim SymbolExch As String = TrdSym & "." & Exch
                    ListMarketWatchSymbol.Remove(SymbolExch)
                    UpdateMarketWatchFile()

                    'Unsubscribe Quotes
                    'Upstox.UnSubscribeQuotes(Exch, TrdSym)
                    .Rows.Remove(.Rows(RowIndex))
                End With
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub AddSymbolToMarketWatch()
        Try
            If KryptonComboBox1.SelectedIndex < 0 Then
                ShowMsgBoxOk(MsgBoxTitle, "Select Exchange")
                KryptonComboBox1.Focus()
                Exit Sub
            End If

            If KryptonComboBox3.SelectedIndex < 0 Then
                ShowMsgBoxOk(MsgBoxTitle, "Select Symbol")
                KryptonComboBox3.Focus()
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            Dim TrdSym As String = KryptonComboBox3.SelectedItem
            Dim Exch As String = KryptonComboBox1.SelectedItem
            Dim InstToken As String = Upstox.GetInstToken(Exch, TrdSym).ToUpper

            If KryptonDataGridView1.RowCount > 0 Then
                For RowIndex = 0 To KryptonDataGridView1.RowCount - 1
                    Dim Token As String = KryptonDataGridView1.Rows(RowIndex).Cells(16).Value 'Last Column
                    If Token = InstToken Then
                        ShowMsgBoxOk(MsgBoxTitle, "Symbol already added to Market Watch")
                        Exit Sub
                    End If
                Next
            End If

            Dim Items As New List(Of String)

            Items.Add(Exch) '0
            Items.Add(TrdSym) '1

            Items.Add(0) '2 LTP
            Items.Add(0) '3 BestBid
            Items.Add(0) '4 Best Ask
            Items.Add(0) '5 Open
            Items.Add(0) '6 High
            Items.Add(0) '7 Low
            Items.Add(0) '8 Close
            Items.Add(0) '9 ATP
            Items.Add(0) '10 Volume
            Items.Add(0) '11 OpenInt
            Items.Add(Now.ToString("dd-MMM-yy HH:mm:ss")) '12 Last Update Time
            Items.Add(Now.ToString("dd-MMM-yy HH:mm:ss")) '13 Last Trade Time
            Items.Add(0) '14 NetQty
            Items.Add(0) '15 Mtm
            Items.Add(InstToken) '16
            Dim Index As Integer = KryptonDataGridView1.Rows.Add(Items.ToArray)

            'Set Data Type
            With KryptonDataGridView1.Rows(Index)
                .Cells(0).ValueType = GetType(System.String)
                .Cells(1).ValueType = GetType(System.String)
                .Cells(2).ValueType = GetType(System.Double)
                .Cells(3).ValueType = GetType(System.Double)
                .Cells(4).ValueType = GetType(System.Double)
                .Cells(5).ValueType = GetType(System.Double)
                .Cells(6).ValueType = GetType(System.Double)
                .Cells(7).ValueType = GetType(System.Double)
                .Cells(8).ValueType = GetType(System.Double)
                .Cells(9).ValueType = GetType(System.Double)
                .Cells(10).ValueType = GetType(System.Double)
                .Cells(11).ValueType = GetType(System.Double)
                .Cells(12).ValueType = GetType(System.String)
                .Cells(13).ValueType = GetType(System.String)
                .Cells(14).ValueType = GetType(System.Double)
                .Cells(15).ValueType = GetType(System.Double)
                .Cells(16).ValueType = GetType(System.String)
            End With

            Dim SymbolExch As String = TrdSym & "." & Exch
            ListMarketWatchSymbol.Add(SymbolExch)
            UpdateMarketWatchFile()

            'Subscribe Quotes
            Upstox.SubscribeQuotes(Exch, TrdSym)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub AddSymbolToMarketWatch(ByVal Exch As String, ByVal TrdSym As String, ByVal InstToken As String)
        Try
            If KryptonDataGridView1.InvokeRequired Then
                KryptonDataGridView1.Invoke(New MethodInvoker(Sub() AddSymbolToMarketWatch(Exch, TrdSym, InstToken)))
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            If KryptonDataGridView1.RowCount > 0 Then
                For RowIndex = 0 To KryptonDataGridView1.RowCount - 1
                    Dim Token As String = KryptonDataGridView1.Rows(RowIndex).Cells(16).Value 'Last Column
                    If Token = InstToken Then Exit Sub
                Next
            End If

            Dim Items As New List(Of String)

            Items.Add(Exch) '0
            Items.Add(TrdSym) '1

            Items.Add(0) '2 LTP
            Items.Add(0) '3 BestBid
            Items.Add(0) '4 Best Ask
            Items.Add(0) '5 Open
            Items.Add(0) '6 High
            Items.Add(0) '7 Low
            Items.Add(0) '8 Close
            Items.Add(0) '9 ATP
            Items.Add(0) '10 Volume
            Items.Add(0) '11 OpenInt
            Items.Add(Now.ToString("dd-MMM-yy HH:mm:ss")) '12 Last Update Time
            Items.Add(Now.ToString("dd-MMM-yy HH:mm:ss")) '13 Last Trade Time
            Items.Add(0) '14 NetQty
            Items.Add(0) '15 Mtm
            Items.Add(InstToken) '16

            Dim Index As Integer = KryptonDataGridView1.Rows.Add(Items.ToArray)
            'Set Data Type
            With KryptonDataGridView1.Rows(Index)
                .Cells(0).ValueType = GetType(System.String)
                .Cells(1).ValueType = GetType(System.String)
                .Cells(2).ValueType = GetType(System.Double)
                .Cells(3).ValueType = GetType(System.Double)
                .Cells(4).ValueType = GetType(System.Double)
                .Cells(5).ValueType = GetType(System.Double)
                .Cells(6).ValueType = GetType(System.Double)
                .Cells(7).ValueType = GetType(System.Double)
                .Cells(8).ValueType = GetType(System.Double)
                .Cells(9).ValueType = GetType(System.Double)
                .Cells(10).ValueType = GetType(System.Double)
                .Cells(11).ValueType = GetType(System.Double)
                .Cells(12).ValueType = GetType(System.String)
                .Cells(13).ValueType = GetType(System.String)
                .Cells(14).ValueType = GetType(System.Double)
                .Cells(15).ValueType = GetType(System.Double)
                .Cells(16).ValueType = GetType(System.String)
            End With

            Dim SymbolExch As String = TrdSym & "." & Exch
            ListMarketWatchSymbol.Add(SymbolExch)
            UpdateMarketWatchFile()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetSymbolListFromMarketWatchFile()
        Try
            If Not File.Exists(MarketWatchFileName) Then File.Create(MarketWatchFileName).Dispose()

            Using sr As StreamReader = New StreamReader(MarketWatchFileName)
                Do While sr.Peek() >= 0
                    Try
                        Dim Line As String = Trim(sr.ReadLine)
                        If String.IsNullOrEmpty(Line) Then Continue Do

                        Dim Values() As String = Line.ToUpper.Split(New String() {"."}, StringSplitOptions.None)

                        Dim TrdSym As String = Values(0)
                        Dim Exch As String = Values(1)

                        Dim LotSize As Integer = Upstox.GetLotSize(Exch, TrdSym)
                        Dim SymbolExch As String = TrdSym & "." & Exch
                        If ListMarketWatchSymbol.Contains(SymbolExch) Then Continue Do
                        ListMarketWatchSymbol.Add(SymbolExch)
                    Catch ex As Exception
                    End Try
                Loop
            End Using
            File.WriteAllText(MarketWatchFileName, String.Join(vbNewLine, ListMarketWatchSymbol.ToArray))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ProcessSymbolListMarketWatch()
        Try
            If KryptonDataGridView1.InvokeRequired Then
                KryptonDataGridView1.Invoke(New MethodInvoker(AddressOf ProcessSymbolListMarketWatch))
                Exit Sub
            End If

            For i = 0 To ListMarketWatchSymbol.Count - 1
                Try
                    Dim StrArr() As String = ListMarketWatchSymbol(i).Split(New String() {"."}, StringSplitOptions.RemoveEmptyEntries)
                    Dim Exch As String = StrArr(1)
                    Dim TrdSym As String = StrArr(0)
                    Dim InstToken As String = Upstox.GetInstToken(Exch, TrdSym)

                    Dim Items As New List(Of String)

                    Items.Add(Exch) '0
                    Items.Add(TrdSym) '1

                    Items.Add(0) '2 LTP
                    Items.Add(0) '3 BestBid
                    Items.Add(0) '4 Best Ask
                    Items.Add(0) '5 Open
                    Items.Add(0) '6 High
                    Items.Add(0) '7 Low
                    Items.Add(0) '8 Close
                    Items.Add(0) '9 ATP
                    Items.Add(0) '10 Volume
                    Items.Add(0) '11 OpenInt
                    Items.Add(Now.ToString("dd-MMM-yy HH:mm:ss")) '12 Last Update Time
                    Items.Add(Now.ToString("dd-MMM-yy HH:mm:ss")) '13 Last Trade Time
                    Items.Add(0) '14 NetQty
                    Items.Add(0) '15 Mtm
                    Items.Add(InstToken) '0

                    KryptonDataGridView1.Rows.Add(Items.ToArray)
                Catch ex As Exception
                End Try
            Next

            'Set Data Type
            With KryptonDataGridView1
                For RowIndex = 0 To .RowCount - 1
                    With .Rows(RowIndex)
                        .Cells(0).ValueType = GetType(System.String)
                        .Cells(1).ValueType = GetType(System.String)
                        .Cells(2).ValueType = GetType(System.Double)
                        .Cells(3).ValueType = GetType(System.Double)
                        .Cells(4).ValueType = GetType(System.Double)
                        .Cells(5).ValueType = GetType(System.Double)
                        .Cells(6).ValueType = GetType(System.Double)
                        .Cells(7).ValueType = GetType(System.Double)
                        .Cells(8).ValueType = GetType(System.Double)
                        .Cells(9).ValueType = GetType(System.Double)
                        .Cells(10).ValueType = GetType(System.Double)
                        .Cells(11).ValueType = GetType(System.Double)
                        .Cells(12).ValueType = GetType(System.String)
                        .Cells(13).ValueType = GetType(System.String)
                        .Cells(14).ValueType = GetType(System.Double)
                        .Cells(15).ValueType = GetType(System.Double)
                        .Cells(16).ValueType = GetType(System.String)
                    End With
                Next
            End With

            Dim Thread As New Thread(AddressOf UpdateMarketWatch)
            Thread.IsBackground = True
            Thread.Start()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If ShowMsgBoxOkCancel(MsgBoxTitle, "Are you sure exit application?") <> DialogResult.OK Then
            e.Cancel = True
        End If
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                KryptonContextMenuItem10_Click(Nothing, New System.EventArgs)
            End If

            If e.KeyCode = Keys.F2 Then
                KryptonContextMenuItem11_Click(Nothing, New System.EventArgs)
            End If

            If e.KeyCode = Keys.F3 Then
                KryptonContextMenuItem13_Click(Nothing, New System.EventArgs)
            End If

            If e.KeyCode = Keys.F4 Then
                KryptonContextMenuItem12_Click(Nothing, New System.EventArgs)
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            KryptonRichTextBox1.Text = Now.ToString("HH:mm:ss.fff") & " " & "Hello Upstox v2.0"
            KryptonRichTextBox1.Text &= vbNewLine & Now.ToString("HH:mm:ss.fff") & " " & "Demo Trading Platform built with UpstoxNet for Upstox API"

            KryptonPanel6.Enabled = False
            If DisableMarketWatch Then
                KryptonCheckButton1.Checked = True
                KryptonCheckButton1.Text = "Enable Market Watch"

                KryptonDataGridView1.Enabled = False

                KryptonComboBox1.Enabled = False
                KryptonComboBox2.Enabled = False
                KryptonComboBox3.Enabled = False

                KryptonButton4.Enabled = False
                KryptonButton5.Enabled = False
            Else
                KryptonCheckButton1.Checked = False
                KryptonCheckButton1.Text = "Disable Market Watch"

                KryptonDataGridView1.Enabled = True

                KryptonComboBox1.Enabled = True
                KryptonComboBox2.Enabled = True
                KryptonComboBox3.Enabled = True

                KryptonButton4.Enabled = True
                KryptonButton5.Enabled = True
            End If


            KryptonButton1.Enabled = True
            KryptonButton2.Enabled = False

            AddHandler Upstox.QuotesReceivedEvent, AddressOf Quotes_Received
            AddHandler Upstox.AppUpdateEvent, AddressOf App_Update
            AddHandler Upstox.OrderUpdateEvent, AddressOf Order_Update
            AddHandler Upstox.PositionUpdateEvent, AddressOf Position_Update

            SetControlText(KryptonLabel4, "Waiting")
            SetKryptonLabelColor(KryptonLabel4, Color.Crimson)

            DataGridInitialSettings()

            KryptonDockableNavigator1.SelectedPage = KryptonDockableNavigator1.Pages.First
            KryptonDockableNavigator2.SelectedPage = KryptonDockableNavigator2.Pages.First

            Dim Thread As New Thread(AddressOf UpdateClock)
            Thread.IsBackground = True
            Thread.Start()
        Catch Ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, Ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub DataGridInitialSettings()
        Try
            'Order Book Page
            KryptonDataGridView2.AllowUserToAddRows = False
            KryptonDataGridView2.AllowUserToDeleteRows = False
            KryptonDataGridView2.AllowUserToOrderColumns = False

            KryptonDataGridView2.MultiSelect = False
            KryptonDataGridView2.ReadOnly = True
            KryptonDataGridView2.RowHeadersWidth = 20

            KryptonDataGridView2.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView2.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView2.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView2.ColumnHeadersHeight = 20

            Dim OrderBookHeaderArray() As String = OrderBookHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To OrderBookHeaderArray.Count - 1
                KryptonDataGridView2.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = OrderBookHeaderArray(i), .Name = OrderBookHeaderArray(i)})
            Next

            'Positions
            KryptonDataGridView3.AllowUserToAddRows = False
            KryptonDataGridView3.AllowUserToDeleteRows = False
            KryptonDataGridView3.AllowUserToOrderColumns = False

            KryptonDataGridView3.MultiSelect = False
            KryptonDataGridView3.ReadOnly = True
            KryptonDataGridView3.RowHeadersWidth = 20

            KryptonDataGridView3.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView3.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView3.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView3.ColumnHeadersHeight = 20

            Dim PositionsHeaderArray() As String = PositionsHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To PositionsHeaderArray.Count - 1
                KryptonDataGridView3.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = PositionsHeaderArray(i), .Name = PositionsHeaderArray(i)})
            Next

            'Holdings
            KryptonDataGridView4.AllowUserToAddRows = False
            KryptonDataGridView4.AllowUserToDeleteRows = False
            KryptonDataGridView4.AllowUserToOrderColumns = False

            KryptonDataGridView4.MultiSelect = False
            KryptonDataGridView4.ReadOnly = True
            KryptonDataGridView4.RowHeadersWidth = 20

            KryptonDataGridView4.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView4.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView4.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView4.ColumnHeadersHeight = 20

            Dim HoldingsHeaderArray() As String = HoldingsHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HoldingsHeaderArray.Count - 1
                KryptonDataGridView4.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HoldingsHeaderArray(i), .Name = HoldingsHeaderArray(i)})
            Next

            'Funds
            KryptonDataGridView5.AllowUserToAddRows = False
            KryptonDataGridView5.AllowUserToDeleteRows = False
            KryptonDataGridView5.AllowUserToOrderColumns = False

            KryptonDataGridView5.MultiSelect = False
            KryptonDataGridView5.ReadOnly = True
            KryptonDataGridView5.RowHeadersWidth = 20

            KryptonDataGridView5.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView5.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView5.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView5.ColumnHeadersHeight = 20

            Dim FundsHeaderArray() As String = FundsHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To FundsHeaderArray.Count - 1
                KryptonDataGridView5.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = FundsHeaderArray(i), .Name = FundsHeaderArray(i)})
            Next

            'TradeBook
            KryptonDataGridView6.AllowUserToAddRows = False
            KryptonDataGridView6.AllowUserToDeleteRows = False
            KryptonDataGridView6.AllowUserToOrderColumns = False

            KryptonDataGridView6.MultiSelect = False
            KryptonDataGridView6.ReadOnly = True
            KryptonDataGridView6.RowHeadersWidth = 20

            KryptonDataGridView6.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView6.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView6.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView6.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView6.ColumnHeadersHeight = 20

            Dim TradeBookHeaderArray() As String = TradeBookHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To TradeBookHeaderArray.Count - 1
                KryptonDataGridView6.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = TradeBookHeaderArray(i), .Name = TradeBookHeaderArray(i)})
            Next

            'BridgeLogs
            KryptonDataGridView7.AllowUserToAddRows = False
            KryptonDataGridView7.AllowUserToDeleteRows = False
            KryptonDataGridView7.AllowUserToOrderColumns = False

            KryptonDataGridView7.MultiSelect = False
            KryptonDataGridView7.ReadOnly = True
            KryptonDataGridView7.RowHeadersWidth = 20

            KryptonDataGridView7.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView7.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView7.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView7.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView7.ColumnHeadersHeight = 20

            Dim BridgeLogsHeaderArray() As String = BridgeLogsHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To BridgeLogsHeaderArray.Count - 1
                KryptonDataGridView7.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = BridgeLogsHeaderArray(i), .Name = BridgeLogsHeaderArray(i)})
            Next

            'BridgePositions
            KryptonDataGridView8.AllowUserToAddRows = False
            KryptonDataGridView8.AllowUserToDeleteRows = False
            KryptonDataGridView8.AllowUserToOrderColumns = False

            KryptonDataGridView8.MultiSelect = False
            KryptonDataGridView8.ReadOnly = True
            KryptonDataGridView8.RowHeadersWidth = 20

            KryptonDataGridView8.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView8.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView8.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView8.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView8.ColumnHeadersHeight = 20

            Dim BridgePositionsHeaderArray() As String = BridgePositionsHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To BridgePositionsHeaderArray.Count - 1
                KryptonDataGridView8.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = BridgePositionsHeaderArray(i), .Name = BridgePositionsHeaderArray(i)})
            Next

            'BridgePositionsAll
            KryptonDataGridView9.AllowUserToAddRows = False
            KryptonDataGridView9.AllowUserToDeleteRows = False
            KryptonDataGridView9.AllowUserToOrderColumns = False

            KryptonDataGridView9.MultiSelect = False
            KryptonDataGridView9.ReadOnly = True
            KryptonDataGridView9.RowHeadersWidth = 20

            KryptonDataGridView9.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView9.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView9.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView9.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView9.ColumnHeadersHeight = 20

            Dim BridgePositionsAllHeaderArray() As String = BridgePositionsAllHeader.Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To BridgePositionsAllHeaderArray.Count - 1
                KryptonDataGridView9.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = BridgePositionsAllHeaderArray(i), .Name = BridgePositionsAllHeaderArray(i)})
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub UpdateClock()
        Do
            Threading.Thread.Sleep(1000)
            SetControlText(KryptonLabel1, Now.ToString("HH:mm:ss"))
        Loop
    End Sub

    Private Sub KryptonLinkLabel2_LinkClicked(sender As Object, e As EventArgs) Handles KryptonLinkLabel2.LinkClicked
        Try
            Process.Start("https://howutrade.in")
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub KryptonRichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles KryptonRichTextBox1.TextChanged
        Try
            KryptonRichTextBox1.SelectionStart = KryptonRichTextBox1.Text.Length
            KryptonRichTextBox1.ScrollToCaret()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        LoginCustom()
    End Sub

    Private Sub LogoutCustom()
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User already Logged-out", BoxStyle.Information)
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Information)
                Exit Sub
            End If

            AzDbMethods.InsertUserLoggedOutInfo(Upstox.Access_Token)

            Upstox.Logout()

            KryptonButton2.Enabled = False
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub LoginCustom()
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Login_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User already Logged-in", BoxStyle.Information)
                Exit Sub
            End If

            AccessCodeLocal = ""
            Dim Url As String = LoginUrl & "/index/dialog/authorize?apiKey=" & Upstox.Api_Key & "&redirect_uri=" & Upstox.Redirect_Url & "&response_type=code"

            Dim WebForm As New WebBrowser
            WebForm.Url = Url
            WebForm.WebBrowser1.ScriptErrorsSuppressed = True
            WebForm.ShowDialog()

            If String.IsNullOrEmpty(AccessCodeLocal) Then
                ShowMsgBoxOk(MsgBoxTitle, "Access code is null or invalid", BoxStyle.Warning)
                Exit Sub
            End If

            Upstox.GetAccessToken(AccessCodeLocal)
            Upstox.GetMasterContract()

            AzDbMethods.InsertUserLoginInfo(Upstox.Client_Id, AccessCodeLocal, Upstox.Access_Token)

            KryptonButton1.Enabled = False
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub UpdateDatabase()
        Throw New NotImplementedException()
    End Sub

    Private Sub KryptonCheckButton1_CheckedChanged(sender As Object, e As EventArgs) Handles KryptonCheckButton1.CheckedChanged
        Try
            If KryptonCheckButton1.Checked = True Then
                KryptonCheckButton1.Text = "Enable Market Watch"
                DisableMarketWatch = True
                My.Settings.DisableMarketWatch = True
                My.Settings.Save()

                KryptonDataGridView1.Enabled = False

                KryptonComboBox1.Enabled = False
                KryptonComboBox2.Enabled = False
                KryptonComboBox3.Enabled = False

                KryptonButton4.Enabled = False
                KryptonButton5.Enabled = False
            Else
                KryptonCheckButton1.Text = "Disable Market Watch"
                DisableMarketWatch = False
                My.Settings.DisableMarketWatch = False
                My.Settings.Save()

                KryptonDataGridView1.Enabled = True

                KryptonComboBox1.Enabled = True
                KryptonComboBox2.Enabled = True
                KryptonComboBox3.Enabled = True

                KryptonButton4.Enabled = True
                KryptonButton5.Enabled = True
            End If
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub KryptonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles KryptonComboBox1.SelectedIndexChanged
        Try
            Dim SelectedExch As String = KryptonComboBox1.SelectedItem
            KryptonComboBox2.Items.Clear()
            If (SelectedExch = "NSE_EQ" OrElse SelectedExch = "BSE_EQ") Then
                KryptonComboBox2.Items.AddRange(Upstox.ListInstTypeEq)
            ElseIf (SelectedExch = "NSE_INDEX" OrElse SelectedExch = "BSE_INDEX") Then
                KryptonComboBox2.Items.AddRange(Upstox.ListInstTypeIdx)
            Else
                KryptonComboBox2.Items.AddRange(Upstox.ListInstTypeFut)
            End If
            KryptonComboBox2.SelectedIndex = 0
        Catch Ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, Ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles KryptonComboBox2.SelectedIndexChanged
        Try
            Dim SelectedInst As String = KryptonComboBox2.SelectedItem
            Dim SelectedExch As String = KryptonComboBox1.SelectedItem

            KryptonComboBox3.Items.Clear()
            If SelectedExch = "NSE_EQ" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolNSEEq)
            ElseIf SelectedExch = "BSE_EQ" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolBSEEq)
            ElseIf SelectedExch = "NSE_INDEX" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolNSEIdx)
            ElseIf SelectedExch = "BSE_INDEX" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolBSEIdx)
            ElseIf SelectedExch = "MCX_FO" AndAlso SelectedInst = "FUT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolMCXFut)
            ElseIf SelectedExch = "MCX_FO" AndAlso SelectedInst = "OPT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolMCXOpt)
            ElseIf SelectedExch = "NSE_FO" AndAlso SelectedInst = "FUT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolNSEFut)
            ElseIf SelectedExch = "NSE_FO" AndAlso SelectedInst = "OPT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolNSEOpt)
            ElseIf SelectedExch = "BCD_FO" AndAlso SelectedInst = "FUT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolBCDFut)
            ElseIf SelectedExch = "BCD_FO" AndAlso SelectedInst = "OPT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolBCDOpt)
            ElseIf SelectedExch = "NCD_FO" AndAlso SelectedInst = "FUT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolNCDFut)
            ElseIf SelectedExch = "NCD_FO" AndAlso SelectedInst = "OPT" Then
                KryptonComboBox3.Items.AddRange(Upstox.ListSymbolNCDOpt)
            End If

            KryptonComboBox3.SelectedIndex = -1
            KryptonComboBox3.ResetText()
        Catch Ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, Ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton4_Click(sender As Object, e As EventArgs) Handles KryptonButton4.Click
        AddSymbolToMarketWatch()
    End Sub

    Private Sub KryptonButton5_Click(sender As Object, e As EventArgs) Handles KryptonButton5.Click
        DelSymbolFromMarketWatch()
    End Sub

    Private Sub KryptonComboBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles KryptonComboBox3.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not DisableMarketWatch Then
            AddSymbolToMarketWatch()
        End If
    End Sub

    Private Sub KryptonDataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles KryptonDataGridView1.DoubleClick
        ShowMarketDepth()
    End Sub

    Private Sub KryptonDataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles KryptonDataGridView1.KeyDown
        If e.KeyCode = Keys.Delete AndAlso Not DisableMarketWatch Then
            DelSymbolFromMarketWatch()
        End If
    End Sub

    Private Sub KryptonContextMenuMarketWatch_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles KryptonContextMenuMarketWatch.Opening
        If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then
            'e.Cancel = True
            KryptonContextMenuItem2.Enabled = False
            KryptonContextMenuItem3.Enabled = False
            KryptonContextMenuItem4.Enabled = False
            Exit Sub
        End If

        If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
            'e.Cancel = True
            KryptonContextMenuItem2.Enabled = False
            KryptonContextMenuItem3.Enabled = False
            KryptonContextMenuItem4.Enabled = False
            Exit Sub
        End If

        KryptonContextMenuItem4.Enabled = True
        Dim Exch As String = ""

        If KryptonDataGridView1.SelectedRows.Count > 0 Then
            Exch = KryptonDataGridView1.SelectedRows(0).Cells(0).Value
        Else
            Dim RowIndex As Integer = KryptonDataGridView1.SelectedCells(0).RowIndex
            Exch = KryptonDataGridView1.Rows(RowIndex).Cells(0).Value
        End If

        If (Exch = "NSE_INDEX" OrElse Exch = "BSE_INDEX" OrElse Exch = "MCX_INDEX") Then
            KryptonContextMenuItem2.Enabled = False
            KryptonContextMenuItem3.Enabled = False
        Else
            KryptonContextMenuItem2.Enabled = True
            KryptonContextMenuItem3.Enabled = True
        End If
    End Sub

    Private Sub BuyFromMarketWatch()
        Try
            If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            If KryptonDataGridView1.SelectedRows.Count > 0 Then
                With KryptonDataGridView1
                    Dim Exch As String = .SelectedRows(0).Cells(0).Value
                    Dim TrdSym As String = .SelectedRows(0).Cells(1).Value
                    Upstox.ShowOrderWindow("B", Exch, TrdSym, "I")
                End With
            Else
                With KryptonDataGridView1
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Dim Exch As String = .Rows(RowIndex).Cells(0).Value
                    Dim TrdSym As String = .Rows(RowIndex).Cells(1).Value
                    Upstox.ShowOrderWindow("B", Exch, TrdSym, "I")
                End With
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub SellFromMarketWatch()
        Try
            If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            If KryptonDataGridView1.SelectedRows.Count > 0 Then
                With KryptonDataGridView1
                    Dim Exch As String = .SelectedRows(0).Cells(0).Value
                    Dim TrdSym As String = .SelectedRows(0).Cells(1).Value
                    Upstox.ShowOrderWindow("S", Exch, TrdSym, "I")
                End With
            Else
                With KryptonDataGridView1
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Dim Exch As String = .Rows(RowIndex).Cells(0).Value
                    Dim TrdSym As String = .Rows(RowIndex).Cells(1).Value
                    Upstox.ShowOrderWindow("S", Exch, TrdSym, "I")
                End With
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExportDataGridView()
        Try
            If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            SaveFileDialog1.OverwritePrompt = False
            SaveFileDialog1.Title = MsgBoxTitle
            SaveFileDialog1.Filter = "Csv Files (*.csv)|*.csv"

            If SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
            If SaveFileDialog1.FileName = String.Empty Then Exit Sub
            Dim FileName As String = SaveFileDialog1.FileName

            If File.Exists(FileName) Then File.Delete(FileName)

            Dim StrBuilder As New StringBuilder
            For ColIndex As Integer = 0 To KryptonDataGridView1.Columns.Count - 1
                StrBuilder.Append(KryptonDataGridView1.Columns(ColIndex).HeaderText).Append(",")
            Next
            StrBuilder.Remove(StrBuilder.Length - 1, 1)
            StrBuilder.AppendLine()

            For rowIndex = 0 To KryptonDataGridView1.RowCount - 1
                For colIndex = 0 To KryptonDataGridView1.ColumnCount - 1
                    StrBuilder.Append(KryptonDataGridView1.Rows(rowIndex).Cells(colIndex).Value.ToString).Append(",")
                Next
                StrBuilder.Remove(StrBuilder.Length - 1, 1)
                StrBuilder.AppendLine()
            Next

            StrBuilder.Remove(StrBuilder.Length - 1, 1)

            Using Sw As New StreamWriter(FileName, False)
                Sw.Write(StrBuilder.ToString)
            End Using
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem3_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem3.Click
        SellFromMarketWatch()
    End Sub

    Private Sub KryptonContextMenuItem4_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem4.Click
        ExportDataGridView()
    End Sub

    Private Sub KryptonContextMenuItem2_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem2.Click
        BuyFromMarketWatch()
    End Sub

    Private Sub KryptonButton2_Click(sender As Object, e As EventArgs) Handles KryptonButton2.Click
        LogoutCustom()
    End Sub

    Private Sub KryptonButton3_Click(sender As Object, e As EventArgs) Handles KryptonButton3.Click
        Try
            Upstox.ShowSettingsWindow()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonDockableNavigator1_Selected(sender As Object, e As ComponentFactory.Krypton.Navigator.KryptonPageEventArgs) Handles KryptonDockableNavigator1.Selected
        Dim PageTitle As String = e.Item.Text
        If PageTitle = "Order Book" Then
            GetOrderBooK()
        ElseIf PageTitle = "Positions" Then
            GetPositions()
        ElseIf PageTitle = "Holdings" Then
            LastSelectedPage = "Holdings"
            GetHoldings()
        ElseIf PageTitle = "Funds" Then
            LastSelectedPage = "Funds"
            GetFunds()
        ElseIf PageTitle = "Trade Book" Then
            LastSelectedPage = "Trade Book"
            GetTradeBook()
        ElseIf PageTitle = "Bridge" Then
            KryptonDockableNavigator2.SelectedPage = KryptonDockableNavigator2.Pages.First
            LastSelectedPage = "Logs"
            GetBridgeLogs()
        End If
    End Sub

    Private Sub KryptonDockableNavigator2_Selected(sender As Object, e As ComponentFactory.Krypton.Navigator.KryptonPageEventArgs) Handles KryptonDockableNavigator2.Selected
        Dim PageTitle As String = e.Item.Text
        If PageTitle = "Logs" Then
            LastSelectedPage = "Logs"
            GetBridgeLogs()
        ElseIf PageTitle = "Positions" Then
            LastSelectedPage = "Positions"
            GetBridgePositions()
        ElseIf PageTitle = "Strategies" Then
            LastSelectedPage = "Strategies"
            GetBridgePositionsAll()
        End If
    End Sub

    Private Sub KryptonContextMenuItem1_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem1.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Login_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User already Logged-in", BoxStyle.Information)
                Exit Sub
            End If

            Upstox.Login()
            Upstox.GetAccessToken()
            Upstox.GetMasterContract()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem5_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem5.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Login_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User already Logged-in", BoxStyle.Information)
                Exit Sub
            End If

            Upstox.Login("chrome")
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem6_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem6.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Authorization_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "Access Token already received", BoxStyle.Information)
                Exit Sub
            End If

            Dim Code As String = ShowMsgBoxInput(MsgBoxTitle, "Enter your Access code copied from browser Url")

            Upstox.GetAccessToken(Code)
            Upstox.GetMasterContract()

        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem7_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem7.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Authorization_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "Access Token already set", BoxStyle.Information)
                Exit Sub
            End If

            Dim Token As String = ShowMsgBoxInput(MsgBoxTitle, "Enter your Access Token generated from your earlier login")

            Upstox.SetAccessToken(Token)
            Upstox.GetMasterContract()

        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem8_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem8.Click
        Try
            If String.IsNullOrEmpty(Upstox.Api_Key) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Api_Secret) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If String.IsNullOrEmpty(Upstox.Redirect_Url) Then
                Upstox.ShowSettingsWindow()
                Exit Sub
            End If

            If Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User already Logged-out", BoxStyle.Information)
                Exit Sub
            End If

            If Not Upstox.Login_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Information)
                Exit Sub
            End If

            Upstox.Logout()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem9_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem9.Click
        Me.Close()
    End Sub

    Private Sub KryptonContextMenuItem10_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem10.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Dim Exch As String = ""
            Dim TrdSym As String = ""

            If KryptonDataGridView1.SelectedRows.Count > 0 AndAlso Not DisableMarketWatch Then
                With KryptonDataGridView1
                    Exch = .SelectedRows(0).Cells(0).Value
                    TrdSym = .SelectedRows(0).Cells(1).Value
                End With
            ElseIf KryptonDataGridView1.SelectedCells.Count > 0 AndAlso Not DisableMarketWatch Then
                With KryptonDataGridView1
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Exch = .Rows(RowIndex).Cells(0).Value
                    TrdSym = .Rows(RowIndex).Cells(1).Value
                End With
            End If

            If (Exch = "NSE_INDEX" OrElse Exch = "BSE_INDEX" OrElse Exch = "MCX_INDEX") Then
                Upstox.ShowOrderWindow("B")
            Else
                Upstox.ShowOrderWindow("B", Exch, TrdSym, "I")
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem11_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem11.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Dim Exch As String = ""
            Dim TrdSym As String = ""

            If KryptonDataGridView1.SelectedRows.Count > 0 AndAlso Not DisableMarketWatch Then
                With KryptonDataGridView1
                    Exch = .SelectedRows(0).Cells(0).Value
                    TrdSym = .SelectedRows(0).Cells(1).Value
                End With
            ElseIf KryptonDataGridView1.SelectedCells.Count > 0 AndAlso Not DisableMarketWatch Then
                With KryptonDataGridView1
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Exch = .Rows(RowIndex).Cells(0).Value
                    TrdSym = .Rows(RowIndex).Cells(1).Value
                End With
            End If

            If (Exch = "NSE_INDEX" OrElse Exch = "BSE_INDEX" OrElse Exch = "MCX_INDEX") Then
                Upstox.ShowOrderWindow("S")
            Else
                Upstox.ShowOrderWindow("S", Exch, TrdSym, "I")
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem12_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem12.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Upstox.ShowOrderWindowBridge()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem13_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem13.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                With KryptonDataGridView2
                    Dim OrderId As String = .SelectedRows(0).Cells(Index_OrderId_OrderBook).Value 'Index Check
                    Upstox.ShowModifyWindow(OrderId)
                    Exit Sub
                End With
            ElseIf KryptonDataGridView2.SelectedCells.Count > 0 Then
                With KryptonDataGridView2
                    Dim RowIndex As Integer = .SelectedCells(0).RowIndex
                    Dim OrderId As String = .Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value 'Index Check
                    Upstox.ShowModifyWindow(OrderId)
                    Exit Sub
                End With
            End If
            Upstox.ShowModifyWindow()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem14_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem14.Click
        Help.ShowDialog()
    End Sub

    Private Sub KryptonContextMenuItem15_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem15.Click
        About.ShowDialog()
    End Sub

    Private Sub KryptonContextMenuItem22_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem22.Click
        Disclaimer.ShowDialog()
    End Sub

    Private Sub KryptonContextMenuItem23_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem23.Click
        License.ShowDialog()
    End Sub

    Private Sub KryptonContextMenuItem16_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem16.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Dim Obj As Object() = Upstox.ShowHistDataInputBox()
            Dim Exch As String = Obj(0)
            Dim TrdSym As String = Obj(1)
            Dim Interval As String = Obj(2)
            Dim NoOfDays As Integer = Obj(3)

            If (String.IsNullOrEmpty(Exch) AndAlso String.IsNullOrEmpty(TrdSym)) Then Exit Sub

            Dim ToDate As Date = Today
            Dim FromDate As Date = ToDate.AddDays(-NoOfDays)

            Dim StrInput As String = String.Join(",", Upstox.GetHistData(Exch, TrdSym, Interval, FromDate, ToDate))
            'Dim StrInput As String = String.Join(vbNewLine, Upstox.GetHistData(Exch, TrdSym, Interval, FromDate, ToDate))
            Dim Frm As New DataTable()
            Frm.FormTitle = TrdSym
            Frm.CsvData = StrInput
            Frm.ShowDialog()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem17_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem17.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Upstox.BatchOrderPlacement()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem18_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem18.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Dim d As Double = Upstox.CheckOrderExecutionLatency
            ShowMsgBoxOk(MsgBoxTitle, "Order executed in " & d & " milliseconds")
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem19_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem19.Click
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
                Exit Sub
            End If

            Upstox.GetHistDataBatch()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub GetOrderBooK()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetOrderBook()
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView2.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView2.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView2.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton13_Click(sender As Object, e As EventArgs) Handles KryptonButton13.Click
        GetOrderBooK()
    End Sub

    Private Sub GetPositions()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetPositions
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView3.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView3.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView3.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton8_Click(sender As Object, e As EventArgs) Handles KryptonButton8.Click
        GetPositions()
    End Sub

    Private Sub GetHoldings()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetHoldings
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView4.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView4.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView4.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton6_Click(sender As Object, e As EventArgs) Handles KryptonButton6.Click
        GetHoldings()
    End Sub

    Private Sub GetFunds()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetFunds
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView5.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView5.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView5.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton10_Click(sender As Object, e As EventArgs) Handles KryptonButton10.Click
        GetFunds()
    End Sub

    Private Sub GetTradeBook()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetTradeBook
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView6.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView6.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView6.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton14_Click(sender As Object, e As EventArgs) Handles KryptonButton14.Click
        GetTradeBook()
    End Sub

    Private Sub GetBridgeLogs()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetBridgeLogs
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView7.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView7.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView7.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton16_Click(sender As Object, e As EventArgs) Handles KryptonButton16.Click
        GetBridgeLogs()
    End Sub

    Private Sub GetBridgePositions()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetBridgePositions
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView8.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView8.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView8.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton18_Click(sender As Object, e As EventArgs) Handles KryptonButton18.Click
        GetBridgePositions()
    End Sub

    Private Sub GetBridgePositionsAll()
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then Exit Sub
            Dim CsvData As String = Upstox.GetBridgePositionsAll
            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)

            KryptonDataGridView9.Columns.Clear()

            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView9.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView9.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton20_Click(sender As Object, e As EventArgs) Handles KryptonButton20.Click
        GetBridgePositionsAll()
    End Sub

    Private Sub ExportDataGridViewCommon()
        Try
            Dim Ctrl As KryptonDataGridView = Nothing
            Select Case LastSelectedPage
                Case "Holdings"
                    Ctrl = KryptonDataGridView4
                Case "Funds"
                    Ctrl = KryptonDataGridView5
                Case "Trade Book"
                    Ctrl = KryptonDataGridView6
                Case "Logs"
                    Ctrl = KryptonDataGridView7
                Case "Positions"
                    Ctrl = KryptonDataGridView8
                Case "Strategies"
                    Ctrl = KryptonDataGridView9
                Case Else
                    Exit Sub
            End Select

            If (Ctrl.SelectedRows.Count <= 0 AndAlso Ctrl.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            SaveFileDialog1.OverwritePrompt = False
            SaveFileDialog1.Title = MsgBoxTitle
            SaveFileDialog1.Filter = "Csv Files (*.csv)|*.csv"

            If SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
            If SaveFileDialog1.FileName = String.Empty Then Exit Sub
            Dim FileName As String = SaveFileDialog1.FileName

            If File.Exists(FileName) Then File.Delete(FileName)

            Dim StrBuilder As New StringBuilder
            For ColIndex As Integer = 0 To Ctrl.Columns.Count - 1
                StrBuilder.Append(Ctrl.Columns(ColIndex).HeaderText).Append(",")
            Next
            StrBuilder.Remove(StrBuilder.Length - 1, 1)
            StrBuilder.AppendLine()

            For rowIndex = 0 To Ctrl.RowCount - 1
                For colIndex = 0 To Ctrl.ColumnCount - 1
                    StrBuilder.Append(Ctrl.Rows(rowIndex).Cells(colIndex).Value.ToString).Append(",")
                Next
                StrBuilder.Remove(StrBuilder.Length - 1, 1)
                StrBuilder.AppendLine()
            Next

            StrBuilder.Remove(StrBuilder.Length - 1, 1)

            Using Sw As New StreamWriter(FileName, False)
                Sw.Write(StrBuilder.ToString)
            End Using
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuCommon_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles KryptonContextMenuCommon.Opening
        Try
            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                'e.Cancel = True
                KryptonContextMenuItem20.Enabled = False
                Exit Sub
            End If

            Dim Ctrl As KryptonDataGridView = Nothing
            Select Case LastSelectedPage
                Case "Holdings"
                    Ctrl = KryptonDataGridView4
                Case "Funds"
                    Ctrl = KryptonDataGridView5
                Case "Trade Book"
                    Ctrl = KryptonDataGridView6
                Case "Logs"
                    Ctrl = KryptonDataGridView7
                Case "Positions"
                    Ctrl = KryptonDataGridView8
                Case "Strategies"
                    Ctrl = KryptonDataGridView9
                Case Else
                    Exit Sub
            End Select

            If (Ctrl.SelectedRows.Count <= 0 AndAlso Ctrl.SelectedCells.Count <= 0) Then
                'e.Cancel = True
                KryptonContextMenuItem20.Enabled = False
                Exit Sub
            End If
            KryptonContextMenuItem20.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'KryptonDockableNavigator2.Pages(0).Select()
            'KryptonDockableNavigator1.Pages(0).Select()
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonContextMenuItem20_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem20.Click
        ExportDataGridViewCommon()
    End Sub

    Private Sub KryptonContextMenuItem21_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem21.Click
        If ShowMsgBoxOkCancel(MsgBoxTitle, "This will delete all Cookies, Browse History from Internet Explorer. Are you sure?") = System.Windows.Forms.DialogResult.OK Then
            Upstox.DeleteIECookies()
        End If
    End Sub

    Private Sub KryptonButton12_Click(sender As Object, e As EventArgs) Handles KryptonButton12.Click
        CancelAllSimple()
    End Sub

    Private Sub CancelAndExitAllCO()
        If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
            ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
            Exit Sub
        End If

        If ShowMsgBoxOkCancel(MsgBoxTitle, "This will cancel all CO orders and exit all CO positions. Are you sure?", BoxStyle.Warning) <> System.Windows.Forms.DialogResult.OK Then Exit Sub

        Try
            Dim OrderBook() As String = Upstox.GetOrderBook.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each Order In OrderBook
                Try
                    'Skip if empty
                    If String.IsNullOrEmpty(Order) Then Continue For

                    Dim OrderDetails() As String = Order.Split(New String() {","}, StringSplitOptions.None)
                    If OrderDetails.Length < 23 Then Continue For 'Check for elements
                    If Not Integer.TryParse(OrderDetails(Index_Qty_OrderBook), Nothing) Then Continue For 'Skip header row

                    Dim Status As String = OrderDetails(Index_Status_OrderBook)
                    Dim OrderId As String = OrderDetails(Index_OrderId_OrderBook)
                    Dim ProdType As String = OrderDetails(Index_ProdType_OrderBook)
                    Dim ParentId As String = OrderDetails(Index_ParentId_OrderBook)
                    Dim OrdType As String = OrderDetails(Index_OrdType_OrderBook)

                    Dim LmtPrice As Double
                    Dim TrgPrice As Double
                    Double.TryParse(OrderDetails(Index_LmtPrice_OrderBook), LmtPrice)
                    Double.TryParse(OrderDetails(Index_TrgPrice_OrderBook), TrgPrice)

                    If Status = "complete" OrElse Status = "rejected" OrElse Status = "cancelled" OrElse Status = "cancelled after market order" Then Continue For

                    Select Case ProdType
                        Case "OCO"
                            'If (ParentId = "NA" OrElse String.IsNullOrEmpty(ParentId)) Then 'Identify OCO Main Order
                            '    Upstox.CancelOCOMain(OrderId)
                            'ElseIf (OrdType = "L" AndAlso TrgPrice <= 0) Then 'Identify OCO Target Order
                            '    Upstox.ExitOCO(OrderId)
                            'ElseIf (OrdType = "SL" OrElse OrdType = "SL-M" OrElse (OrdType = "L" AndAlso TrgPrice > 0)) Then 'Identify OCO Stoploss Order
                            '    Upstox.ExitOCO(OrderId)
                            'End If
                        Case "CO"
                            If (ParentId = "NA" OrElse String.IsNullOrEmpty(ParentId)) Then 'Identify CO Main Order
                                Upstox.CancelCOMain(OrderId)
                            ElseIf (OrdType = "SL" OrElse OrdType = "SL-M" OrElse (OrdType = "L" AndAlso TrgPrice > 0)) Then 'Identify CO Stoploss Order
                                Upstox.ExitCO(OrderId)
                            End If
                        Case Else 'This is SimpleOrder
                            'Upstox.CancelSimpleOrder(OrderId)
                    End Select
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub CancelAndExitAllOCO()
        If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
            ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
            Exit Sub
        End If

        If ShowMsgBoxOkCancel(MsgBoxTitle, "This will cancel all OCO orders and exit all OCO positions. Are you sure?", BoxStyle.Warning) <> System.Windows.Forms.DialogResult.OK Then Exit Sub

        Try
            Dim OrderBook() As String = Upstox.GetOrderBook.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each Order In OrderBook
                Try
                    'Skip if empty
                    If String.IsNullOrEmpty(Order) Then Continue For

                    Dim OrderDetails() As String = Order.Split(New String() {","}, StringSplitOptions.None)
                    If OrderDetails.Length < 23 Then Continue For 'Check for elements
                    If Not Integer.TryParse(OrderDetails(Index_Qty_OrderBook), Nothing) Then Continue For 'Skip header row

                    Dim Status As String = OrderDetails(Index_Status_OrderBook)
                    Dim OrderId As String = OrderDetails(Index_OrderId_OrderBook)
                    Dim ProdType As String = OrderDetails(Index_ProdType_OrderBook)
                    Dim ParentId As String = OrderDetails(Index_ParentId_OrderBook)
                    Dim OrdType As String = OrderDetails(Index_OrdType_OrderBook)

                    Dim LmtPrice As Double
                    Dim TrgPrice As Double
                    Double.TryParse(OrderDetails(Index_LmtPrice_OrderBook), LmtPrice)
                    Double.TryParse(OrderDetails(Index_TrgPrice_OrderBook), TrgPrice)

                    If Status = "complete" OrElse Status = "rejected" OrElse Status = "cancelled" OrElse Status = "cancelled after market order" Then Continue For

                    Select Case ProdType
                        Case "OCO"
                            If (ParentId = "NA" OrElse String.IsNullOrEmpty(ParentId)) Then 'Identify OCO Main Order
                                Upstox.CancelOCOMain(OrderId)
                            ElseIf (OrdType = "L" AndAlso TrgPrice <= 0) Then 'Identify OCO Target Order
                                Upstox.ExitOCO(OrderId)
                            ElseIf (OrdType = "SL" OrElse OrdType = "SL-M" OrElse (OrdType = "L" AndAlso TrgPrice > 0)) Then 'Identify OCO Stoploss Order
                                Upstox.ExitOCO(OrderId)
                            End If
                        Case "CO"
                            'If (ParentId = "NA" OrElse String.IsNullOrEmpty(ParentId)) Then 'Identify CO Main Order
                            '    Upstox.CancelCOMain(OrderId)
                            'ElseIf (OrdType = "SL" OrElse OrdType = "SL-M" OrElse (OrdType = "L" AndAlso TrgPrice > 0)) Then 'Identify CO Stoploss Order
                            '    Upstox.ExitCO(OrderId)
                            'End If
                        Case Else 'This is SimpleOrder
                            'Upstox.CancelSimpleOrder(OrderId)
                    End Select
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub CancelAllSimple()
        If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
            ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
            Exit Sub
        End If

        If ShowMsgBoxOkCancel(MsgBoxTitle, "This will cancel all simple orders. Are you sure?", BoxStyle.Warning) <> System.Windows.Forms.DialogResult.OK Then Exit Sub

        Try
            Dim OrderBook() As String = Upstox.GetOrderBook.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each Order In OrderBook
                Try
                    If String.IsNullOrEmpty(Order) Then Continue For

                    Dim OrderDetails() As String = Order.Split(New String() {","}, StringSplitOptions.None)
                    If OrderDetails.Length < 23 Then Continue For 'Check for elements
                    If Not Integer.TryParse(OrderDetails(Index_Qty_OrderBook), Nothing) Then Continue For 'Skip header row

                    Dim Status As String = OrderDetails(Index_Status_OrderBook)
                    Dim OrderId As String = OrderDetails(Index_OrderId_OrderBook)
                    Dim ProdType As String = OrderDetails(Index_ProdType_OrderBook)
                    Dim ParentId As String = OrderDetails(Index_ParentId_OrderBook)
                    Dim OrdType As String = OrderDetails(Index_OrdType_OrderBook)

                    Dim LmtPrice As Double
                    Dim TrgPrice As Double
                    Double.TryParse(OrderDetails(Index_LmtPrice_OrderBook), LmtPrice)
                    Double.TryParse(OrderDetails(Index_TrgPrice_OrderBook), TrgPrice)

                    If Status = "complete" OrElse Status = "rejected" OrElse Status = "cancelled" OrElse Status = "cancelled after market order" Then Continue For

                    Select Case ProdType
                        Case "OCO"
                            'If (ParentId = "NA" OrElse String.IsNullOrEmpty(ParentId)) Then 'Identify OCO Main Order
                            '    Upstox.CancelOCOMain(OrderId)
                            'ElseIf (OrdType = "L" AndAlso TrgPrice <= 0) Then 'Identify OCO Target Order
                            '    Upstox.ExitOCO(OrderId)
                            'ElseIf (OrdType = "SL" OrElse OrdType = "SL-M" OrElse (OrdType = "L" AndAlso TrgPrice > 0)) Then 'Identify OCO Stoploss Order
                            '    Upstox.ExitOCO(OrderId)
                            'End If
                        Case "CO"
                            'If (ParentId = "NA" OrElse String.IsNullOrEmpty(ParentId)) Then 'Identify CO Main Order
                            '    Upstox.CancelCOMain(OrderId)
                            'ElseIf (OrdType = "SL" OrElse OrdType = "SL-M" OrElse (OrdType = "L" AndAlso TrgPrice > 0)) Then 'Identify CO Stoploss Order
                            '    Upstox.ExitCO(OrderId)
                            'End If
                        Case Else 'This is SimpleOrder
                            Upstox.CancelSimpleOrder(OrderId)
                    End Select
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton22_Click(sender As Object, e As EventArgs) Handles KryptonButton22.Click
        CancelAndExitAllOCO()
    End Sub

    Private Sub KryptonButton23_Click(sender As Object, e As EventArgs) Handles KryptonButton23.Click
        CancelAndExitAllCO()
    End Sub

    Private Sub ExitAllSimple()
        If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
            ShowMsgBoxOk(MsgBoxTitle, "User not Logged-in", BoxStyle.Warning)
            Exit Sub
        End If

        If ShowMsgBoxOkCancel(MsgBoxTitle, "This will close all Simple positions by placing opposite order. Are you sure?", BoxStyle.Warning) <> System.Windows.Forms.DialogResult.OK Then Exit Sub

        Try
            Dim Positions() As String = Upstox.GetPositions.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each Position In Positions
                Try
                    'Skip if empty
                    If String.IsNullOrEmpty(Position) Then Continue For

                    Dim PositionDetails() As String = Position.Split(New String() {","}, StringSplitOptions.None)
                    If PositionDetails.Length < 20 Then Continue For 'Check for elements

                    Dim NetQty As Integer
                    If Not Integer.TryParse(PositionDetails(Index_NetQty_Positions), NetQty) Then Continue For 'Skip header row

                    Dim ProdType As String = PositionDetails(Index_ProdType_Positions)
                    If (ProdType = "CO" OrElse ProdType = "OCO" OrElse NetQty = 0) Then Continue For

                    Dim Exch As String = PositionDetails(Index_Exch_Positions)
                    Dim TrdSym As String = PositionDetails(Index_TrdSym_Positions)
                    Dim LotSize As Integer = Upstox.GetLotSize(Exch, TrdSym)
                    Dim Trans As String = If(NetQty > 0, "S", "B")
                    Dim Qty As Integer = Math.Abs(NetQty) / LotSize

                    Upstox.PlaceSimpleOrder(Exch, TrdSym, Trans, "M", Qty, ProdType)
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonButton9_Click(sender As Object, e As EventArgs) Handles KryptonButton9.Click
        ExitAllSimple()
    End Sub

    Private Sub ExportOrderBooK()
        Try
            If (KryptonDataGridView2.SelectedRows.Count <= 0 AndAlso KryptonDataGridView2.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            SaveFileDialog1.OverwritePrompt = False
            SaveFileDialog1.Title = MsgBoxTitle
            SaveFileDialog1.Filter = "Csv Files (*.csv)|*.csv"

            If SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
            If SaveFileDialog1.FileName = String.Empty Then Exit Sub
            Dim FileName As String = SaveFileDialog1.FileName

            If File.Exists(FileName) Then File.Delete(FileName)

            Dim StrBuilder As New StringBuilder
            For ColIndex As Integer = 0 To KryptonDataGridView2.Columns.Count - 1
                StrBuilder.Append(KryptonDataGridView2.Columns(ColIndex).HeaderText).Append(",")
            Next
            StrBuilder.Remove(StrBuilder.Length - 1, 1)
            StrBuilder.AppendLine()

            For rowIndex = 0 To KryptonDataGridView2.RowCount - 1
                For colIndex = 0 To KryptonDataGridView2.ColumnCount - 1
                    StrBuilder.Append(KryptonDataGridView2.Rows(rowIndex).Cells(colIndex).Value.ToString).Append(",")
                Next
                StrBuilder.Remove(StrBuilder.Length - 1, 1)
                StrBuilder.AppendLine()
            Next

            StrBuilder.Remove(StrBuilder.Length - 1, 1)

            Using Sw As New StreamWriter(FileName, False)
                Sw.Write(StrBuilder.ToString)
            End Using
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExportPositions()
        Try
            If (KryptonDataGridView3.SelectedRows.Count <= 0 AndAlso KryptonDataGridView3.SelectedCells.Count <= 0) Then Exit Sub

            If Not Upstox.Symbol_Download_Status Then Exit Sub
            If Upstox.Logout_Status Then Exit Sub

            SaveFileDialog1.OverwritePrompt = False
            SaveFileDialog1.Title = MsgBoxTitle
            SaveFileDialog1.Filter = "Csv Files (*.csv)|*.csv"

            If SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then Exit Sub
            If SaveFileDialog1.FileName = String.Empty Then Exit Sub
            Dim FileName As String = SaveFileDialog1.FileName

            If File.Exists(FileName) Then File.Delete(FileName)

            Dim StrBuilder As New StringBuilder
            For ColIndex As Integer = 0 To KryptonDataGridView3.Columns.Count - 1
                StrBuilder.Append(KryptonDataGridView3.Columns(ColIndex).HeaderText).Append(",")
            Next
            StrBuilder.Remove(StrBuilder.Length - 1, 1)
            StrBuilder.AppendLine()

            For rowIndex = 0 To KryptonDataGridView3.RowCount - 1
                For colIndex = 0 To KryptonDataGridView3.ColumnCount - 1
                    StrBuilder.Append(KryptonDataGridView3.Rows(rowIndex).Cells(colIndex).Value.ToString).Append(",")
                Next
                StrBuilder.Remove(StrBuilder.Length - 1, 1)
                StrBuilder.AppendLine()
            Next

            StrBuilder.Remove(StrBuilder.Length - 1, 1)

            Using Sw As New StreamWriter(FileName, False)
                Sw.Write(StrBuilder.ToString)
            End Using
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExportOrderBookMenuItem_Click(sender As Object, e As EventArgs) Handles ExportOrderBookMenuItem.Click
        ExportOrderBooK()
    End Sub

    Private Sub ExportPositionsMenuItem_Click(sender As Object, e As EventArgs) Handles ExportPositionsMenuItem.Click
        ExportPositions()
    End Sub

    Private Sub KryptonContextMenuPositions_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles KryptonContextMenuPositions.Opening
        Try
            If (KryptonDataGridView3.SelectedRows.Count <= 0 AndAlso KryptonDataGridView3.SelectedCells.Count <= 0) Then
                'e.Cancel = True
                ExitPositionToolStripMenuItem.Enabled = False
                ExportPositionsMenuItem.Enabled = False
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                'e.Cancel = True
                ExitPositionToolStripMenuItem.Enabled = False
                ExportPositionsMenuItem.Enabled = False
                Exit Sub
            End If

            ExportPositionsMenuItem.Enabled = True

            Dim ProdType As String = ""
            Dim NetQty As Integer = 0

            If KryptonDataGridView3.SelectedRows.Count > 0 Then
                ProdType = KryptonDataGridView3.SelectedRows(0).Cells(Index_ProdType_Positions).Value
                Integer.TryParse(KryptonDataGridView3.SelectedRows(0).Cells(Index_NetQty_Positions).Value, NetQty)
            Else
                Dim RowIndex As Integer = KryptonDataGridView3.SelectedCells(0).RowIndex
                ProdType = KryptonDataGridView3.Rows(RowIndex).Cells(Index_ProdType_Positions).Value
                Integer.TryParse(KryptonDataGridView3.Rows(RowIndex).Cells(Index_NetQty_Positions).Value, NetQty)
            End If

            If ProdType = "CO" OrElse ProdType = "OCO" OrElse NetQty = 0 Then
                ExitPositionToolStripMenuItem.Enabled = False
            Else
                ExitPositionToolStripMenuItem.Enabled = True
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExitPosition()
        Try
            Dim Exch As String = ""
            Dim TrdSym As String = ""
            Dim ProdType As String = ""
            Dim NetQty As Integer = 0

            If KryptonDataGridView3.SelectedRows.Count > 0 Then
                Exch = KryptonDataGridView3.SelectedRows(0).Cells(Index_Exch_Positions).Value
                TrdSym = KryptonDataGridView3.SelectedRows(0).Cells(Index_TrdSym_Positions).Value
                ProdType = KryptonDataGridView3.SelectedRows(0).Cells(Index_ProdType_Positions).Value
                Integer.TryParse(KryptonDataGridView3.SelectedRows(0).Cells(Index_NetQty_Positions).Value, NetQty)
            Else
                Dim RowIndex As Integer = KryptonDataGridView3.SelectedCells(0).RowIndex
                Exch = KryptonDataGridView3.Rows(RowIndex).Cells(Index_Exch_Positions).Value
                TrdSym = KryptonDataGridView3.Rows(RowIndex).Cells(Index_TrdSym_Positions).Value
                ProdType = KryptonDataGridView3.Rows(RowIndex).Cells(Index_ProdType_Positions).Value
                Integer.TryParse(KryptonDataGridView3.Rows(RowIndex).Cells(Index_NetQty_Positions).Value, NetQty)
            End If

            Dim LotSize As Integer = Upstox.GetLotSize(Exch, TrdSym)
            Dim Trans As String = If(NetQty > 0, "S", "B")
            Dim Qty As Integer = Math.Abs(NetQty) / LotSize
            Upstox.PlaceSimpleOrder(Exch, TrdSym, Trans, "M", Qty, ProdType)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExitPositionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitPositionToolStripMenuItem.Click
        ExitPosition()
    End Sub

    Private Sub KryptonContextMenuOrderBook_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles KryptonContextMenuOrderBook.Opening
        Try
            If (KryptonDataGridView2.SelectedRows.Count <= 0 AndAlso KryptonDataGridView2.SelectedCells.Count <= 0) Then
                'e.Cancel = True
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                ExportOrderBookMenuItem.Enabled = False
                Exit Sub
            End If

            If Not Upstox.Symbol_Download_Status OrElse Upstox.Logout_Status Then
                'e.Cancel = True
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                ExportOrderBookMenuItem.Enabled = False
                Exit Sub
            End If

            ExportOrderBookMenuItem.Enabled = True

            Dim Status As String = ""
            Dim ProdType As String = ""
            Dim ParentId As String = ""
            Dim OrdType As String = ""

            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                Status = KryptonDataGridView2.SelectedRows(0).Cells(Index_Status_OrderBook).Value
                ParentId = KryptonDataGridView2.SelectedRows(0).Cells(Index_ParentId_OrderBook).Value
                OrdType = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrdType_OrderBook).Value
                ProdType = KryptonDataGridView2.SelectedRows(0).Cells(Index_ProdType_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                Status = KryptonDataGridView2.Rows(RowIndex).Cells(Index_Status_OrderBook).Value
                ParentId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_ParentId_OrderBook).Value
                OrdType = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrdType_OrderBook).Value
                ProdType = KryptonDataGridView2.Rows(RowIndex).Cells(Index_ProdType_OrderBook).Value
            End If

            If Status = "complete" OrElse Status = "rejected" OrElse Status = "cancelled" OrElse Status = "cancelled after market order" Then
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                Exit Sub
            End If

            If ProdType = "OCO" Then
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = True
                COToolStripMenuItem.Enabled = False
                If ParentId = "NA" OrElse ParentId = "" Then
                    ModifyEntryToolStripMenuItem.Enabled = True
                    CancelEntryOrderToolStripMenuItem.Enabled = True
                    ModifyTargetToolStripMenuItem.Enabled = False
                    ModifyStoplossToolStripMenuItem.Enabled = False
                    ExitToolStripMenuItem.Enabled = False
                ElseIf OrdType = "L" Then
                    ModifyEntryToolStripMenuItem.Enabled = False
                    CancelEntryOrderToolStripMenuItem.Enabled = False
                    ModifyTargetToolStripMenuItem.Enabled = True
                    ModifyStoplossToolStripMenuItem.Enabled = False
                    ExitToolStripMenuItem.Enabled = True
                Else
                    ModifyEntryToolStripMenuItem.Enabled = False
                    CancelEntryOrderToolStripMenuItem.Enabled = False
                    ModifyTargetToolStripMenuItem.Enabled = False
                    ModifyStoplossToolStripMenuItem.Enabled = True
                    ExitToolStripMenuItem.Enabled = True
                End If
            ElseIf ProdType = "CO" Then
                SimpleToolStripMenuItem.Enabled = False
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = True
                If ParentId = "NA" OrElse ParentId = "" Then
                    CancelEntryToolStripMenuItem.Enabled = True
                    ModifyStoplossToolStripMenuItem1.Enabled = False
                    ExitToolStripMenuItem1.Enabled = False
                Else
                    CancelEntryToolStripMenuItem.Enabled = False
                    ModifyStoplossToolStripMenuItem1.Enabled = True
                    ExitToolStripMenuItem1.Enabled = True
                End If
            Else
                SimpleToolStripMenuItem.Enabled = True
                OCOToolStripMenuItem.Enabled = False
                COToolStripMenuItem.Enabled = False
                If Status = "after market order req received" Then
                    ModifyToolStripMenuItem.Enabled = False
                Else
                    ModifyToolStripMenuItem.Enabled = True
                End If
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            Dim Status As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
                Status = KryptonDataGridView2.SelectedRows(0).Cells(Index_Status_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
                Status = KryptonDataGridView2.Rows(RowIndex).Cells(Index_Status_OrderBook).Value
            End If
            If Status = "after market order req received" Then
                Upstox.CancelAmo(OrderId)
            Else
                Upstox.CancelSimpleOrder(OrderId)
            End If
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ModifyEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyEntryToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ModifyTargetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyTargetToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ModifyStoplossToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyStoplossToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub CancelEntryOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelEntryOrderToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.CancelOCOMain(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ExitOCO(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub CancelEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelEntryToolStripMenuItem.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.CancelCOMain(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ModifyStoplossToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ModifyStoplossToolStripMenuItem1.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ShowModifyWindow(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Try
            Dim OrderId As String = ""
            If KryptonDataGridView2.SelectedRows.Count > 0 Then
                OrderId = KryptonDataGridView2.SelectedRows(0).Cells(Index_OrderId_OrderBook).Value
            Else
                Dim RowIndex As Integer = KryptonDataGridView2.SelectedCells(0).RowIndex
                OrderId = KryptonDataGridView2.Rows(RowIndex).Cells(Index_OrderId_OrderBook).Value
            End If
            Upstox.ExitCO(OrderId)
        Catch ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, ex.Message, BoxStyle.Critical)
        End Try
    End Sub

    Private Sub KryptonCheckButton1_Click(sender As Object, e As EventArgs) Handles KryptonCheckButton1.Click

    End Sub

    Private Sub KryptonDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles KryptonDataGridView1.CellContentClick

    End Sub

End Class
