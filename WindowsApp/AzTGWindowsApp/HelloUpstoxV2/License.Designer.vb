<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class License
    Inherits ComponentFactory.Krypton.Toolkit.KryptonForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(License))
        Me.KryptonRichTextBox1 = New ComponentFactory.Krypton.Toolkit.KryptonRichTextBox()
        Me.SuspendLayout()
        '
        'KryptonRichTextBox1
        '
        Me.KryptonRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonRichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.KryptonRichTextBox1.Name = "KryptonRichTextBox1"
        Me.KryptonRichTextBox1.ReadOnly = True
        Me.KryptonRichTextBox1.Size = New System.Drawing.Size(570, 338)
        Me.KryptonRichTextBox1.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.KryptonRichTextBox1.StateCommon.Border.Color1 = System.Drawing.Color.White
        Me.KryptonRichTextBox1.StateCommon.Border.Color2 = System.Drawing.Color.White
        Me.KryptonRichTextBox1.StateCommon.Border.DrawBorders = CType((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top Or ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) _
            Or ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) _
            Or ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right), ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)
        Me.KryptonRichTextBox1.StateCommon.Border.Width = 2
        Me.KryptonRichTextBox1.StateCommon.Content.Padding = New System.Windows.Forms.Padding(15)
        Me.KryptonRichTextBox1.TabIndex = 0
        Me.KryptonRichTextBox1.Text = resources.GetString("KryptonRichTextBox1.Text")
        '
        'License
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 338)
        Me.Controls.Add(Me.KryptonRichTextBox1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "License"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "License"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents KryptonRichTextBox1 As ComponentFactory.Krypton.Toolkit.KryptonRichTextBox
End Class
