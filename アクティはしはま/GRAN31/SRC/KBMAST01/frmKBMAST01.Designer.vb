<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKBMAST01
    Inherits TECHNO.FORM.BaseForm01

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvKBMAST = New BaseControl.CustomGridView()
        Me.NKBNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBRN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBLIMIT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBFEE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TAGNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INSDTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UPDDTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvKBMAST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvKBMAST
        '
        Me.dgvKBMAST.AllowUserToAddRows = False
        Me.dgvKBMAST.AllowUserToDeleteRows = False
        Me.dgvKBMAST.AllowUserToResizeColumns = False
        Me.dgvKBMAST.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvKBMAST.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvKBMAST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvKBMAST.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvKBMAST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvKBMAST.ColumnHeadersHeight = 50
        Me.dgvKBMAST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvKBMAST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NKBNO, Me.CKBNAME, Me.CKBRN, Me.CKBLIMIT, Me.CKBFEE, Me.TAGNO, Me.INSDTM, Me.UPDDTM})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvKBMAST.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvKBMAST.EnableHeadersVisualStyles = False
        Me.dgvKBMAST.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvKBMAST.Location = New System.Drawing.Point(604, 301)
        Me.dgvKBMAST.MultiSelect = False
        Me.dgvKBMAST.Name = "dgvKBMAST"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvKBMAST.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvKBMAST.RowHeadersVisible = False
        Me.dgvKBMAST.RowHeadersWidth = 61
        Me.dgvKBMAST.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvKBMAST.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvKBMAST.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvKBMAST.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvKBMAST.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvKBMAST.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvKBMAST.RowTemplate.Height = 40
        Me.dgvKBMAST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvKBMAST.Size = New System.Drawing.Size(622, 452)
        Me.dgvKBMAST.TabIndex = 8
        '
        'NKBNO
        '
        Me.NKBNO.DataPropertyName = "NKBNO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.NKBNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.NKBNO.HeaderText = "顧客種別"
        Me.NKBNO.Name = "NKBNO"
        Me.NKBNO.ReadOnly = True
        Me.NKBNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CKBNAME
        '
        Me.CKBNAME.DataPropertyName = "CKBNAME"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.CKBNAME.DefaultCellStyle = DataGridViewCellStyle4
        Me.CKBNAME.HeaderText = "種別名称"
        Me.CKBNAME.MaxInputLength = 10
        Me.CKBNAME.Name = "CKBNAME"
        Me.CKBNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.CKBNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CKBNAME.Width = 300
        '
        'CKBRN
        '
        Me.CKBRN.DataPropertyName = "CKBRN"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.CKBRN.DefaultCellStyle = DataGridViewCellStyle5
        Me.CKBRN.HeaderText = "種別略称"
        Me.CKBRN.MaxInputLength = 5
        Me.CKBRN.Name = "CKBRN"
        Me.CKBRN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CKBRN.Visible = False
        Me.CKBRN.Width = 150
        '
        'CKBLIMIT
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.CKBLIMIT.DefaultCellStyle = DataGridViewCellStyle6
        Me.CKBLIMIT.HeaderText = "期限(月)"
        Me.CKBLIMIT.MaxInputLength = 2
        Me.CKBLIMIT.Name = "CKBLIMIT"
        Me.CKBLIMIT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CKBFEE
        '
        Me.CKBFEE.DataPropertyName = "CKBFEE"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = Nothing
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.CKBFEE.DefaultCellStyle = DataGridViewCellStyle7
        Me.CKBFEE.HeaderText = "年会費"
        Me.CKBFEE.MaxInputLength = 6
        Me.CKBFEE.Name = "CKBFEE"
        Me.CKBFEE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CKBFEE.Width = 120
        '
        'TAGNO
        '
        Me.TAGNO.DataPropertyName = "TAGNO"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.TAGNO.DefaultCellStyle = DataGridViewCellStyle8
        Me.TAGNO.HeaderText = "タグ番号"
        Me.TAGNO.MaxInputLength = 2
        Me.TAGNO.Name = "TAGNO"
        Me.TAGNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TAGNO.Visible = False
        '
        'INSDTM
        '
        Me.INSDTM.DataPropertyName = "INSDTM"
        Me.INSDTM.HeaderText = "作成日時"
        Me.INSDTM.Name = "INSDTM"
        Me.INSDTM.ReadOnly = True
        Me.INSDTM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.INSDTM.Visible = False
        '
        'UPDDTM
        '
        Me.UPDDTM.DataPropertyName = "UPDDTM"
        Me.UPDDTM.HeaderText = "更新日時"
        Me.UPDDTM.Name = "UPDDTM"
        Me.UPDDTM.ReadOnly = True
        Me.UPDDTM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UPDDTM.Visible = False
        '
        'frmKBMAST01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.dgvKBMAST)
        Me.Name = "frmKBMAST01"
        Me.ShowInTaskbar = False
        Me.Text = ""
        Me.Controls.SetChildIndex(Me.dgvKBMAST, 0)
        CType(Me.dgvKBMAST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvKBMAST As BaseControl.CustomGridView
    Friend WithEvents NKBNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBRN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBLIMIT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBFEE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TAGNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INSDTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UPDDTM As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
