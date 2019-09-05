Imports System.IO
Imports System.Text

Public Class DataTable

    Public FormTitle As String = ""
    Public CsvData As String = ""

    Private Sub KryptonContextMenuItem1_Click(sender As Object, e As EventArgs) Handles KryptonContextMenuItem1.Click
        ExportDataGridView()
    End Sub

    Private Sub ExportDataGridView()
        Try
            If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then Exit Sub

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

    Private Sub KryptonContextMenu1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles KryptonContextMenu1.Opening
        If (KryptonDataGridView1.SelectedRows.Count <= 0 AndAlso KryptonDataGridView1.SelectedCells.Count <= 0) Then
            'e.Cancel = True
            KryptonContextMenuItem1.Enabled = False
            Exit Sub
        End If
        KryptonContextMenuItem1.Enabled = True
    End Sub

    Private Sub DataTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Not String.IsNullOrEmpty(FormTitle) Then
                Me.Text = FormTitle
            End If
            If String.IsNullOrEmpty(CsvData) Then Exit Sub

            'DataGridview Settings
            KryptonDataGridView1.AllowUserToAddRows = False
            KryptonDataGridView1.AllowUserToDeleteRows = False
            KryptonDataGridView1.AllowUserToOrderColumns = False

            KryptonDataGridView1.MultiSelect = False
            KryptonDataGridView1.ReadOnly = True
            KryptonDataGridView1.RowHeadersWidth = 20

            KryptonDataGridView1.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView1.StateCommon.HeaderColumn.Content.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
            KryptonDataGridView1.RowTemplate.DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}

            KryptonDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            KryptonDataGridView1.ColumnHeadersHeight = 20

            KryptonDataGridView1.Columns.Clear()

            Dim Records() As String = CsvData.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            Dim HeaderArray() As String = Records(0).Split(New String() {","}, StringSplitOptions.None)
            For i = 0 To HeaderArray.Count - 1
                KryptonDataGridView1.Columns.Add(New DataGridViewTextBoxColumn With {.SortMode = DataGridViewColumnSortMode.Automatic, .HeaderText = HeaderArray(i), .Name = HeaderArray(i)})
            Next

            For i = 1 To Records.Count - 1
                KryptonDataGridView1.Rows.Add(Records(i).Split(New String() {","}, StringSplitOptions.None))
            Next
        Catch Ex As Exception
            ShowMsgBoxOk(MsgBoxTitle, Ex.Message, BoxStyle.Critical)
        End Try
    End Sub
End Class