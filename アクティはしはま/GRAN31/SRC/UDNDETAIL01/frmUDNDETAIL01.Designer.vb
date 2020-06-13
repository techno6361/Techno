<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUDNDETAIL01
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvUDNDETAIL = New BaseControl.CustomGridView()
        Me.btnRETURN = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.colUDNDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDATKB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUDNNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUDNKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDRWNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDRWCHK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPAYKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMANNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMANNM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHINNMA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.hide_UDNDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvUDNDETAIL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvUDNDETAIL
        '
        Me.dgvUDNDETAIL.AllowUserToAddRows = False
        Me.dgvUDNDETAIL.AllowUserToDeleteRows = False
        Me.dgvUDNDETAIL.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvUDNDETAIL.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUDNDETAIL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvUDNDETAIL.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUDNDETAIL.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvUDNDETAIL.ColumnHeadersHeight = 50
        Me.dgvUDNDETAIL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvUDNDETAIL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUDNDT, Me.colDATKB, Me.colUDNNO, Me.colUDNKN, Me.colDRWNO, Me.colDRWCHK, Me.colCPAYKBN, Me.colMANNO, Me.colMANNM, Me.colHINNMA, Me.hide_UDNDT})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUDNDETAIL.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvUDNDETAIL.EnableHeadersVisualStyles = False
        Me.dgvUDNDETAIL.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvUDNDETAIL.Location = New System.Drawing.Point(90, 196)
        Me.dgvUDNDETAIL.MultiSelect = False
        Me.dgvUDNDETAIL.Name = "dgvUDNDETAIL"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUDNDETAIL.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvUDNDETAIL.RowHeadersVisible = False
        Me.dgvUDNDETAIL.RowHeadersWidth = 61
        Me.dgvUDNDETAIL.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvUDNDETAIL.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvUDNDETAIL.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvUDNDETAIL.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvUDNDETAIL.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvUDNDETAIL.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvUDNDETAIL.RowTemplate.Height = 40
        Me.dgvUDNDETAIL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUDNDETAIL.Size = New System.Drawing.Size(1709, 330)
        Me.dgvUDNDETAIL.TabIndex = 180
        '
        'btnRETURN
        '
        Me.btnRETURN.BackColor = System.Drawing.Color.Orange
        Me.btnRETURN.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRETURN.ForeColor = System.Drawing.Color.Black
        Me.btnRETURN.Location = New System.Drawing.Point(465, 698)
        Me.btnRETURN.Name = "btnRETURN"
        Me.btnRETURN.Size = New System.Drawing.Size(381, 86)
        Me.btnRETURN.TabIndex = 182
        Me.btnRETURN.Text = "戻る"
        Me.btnRETURN.UseVisualStyleBackColor = False
        '
        'btnSelect
        '
        Me.btnSelect.BackColor = System.Drawing.Color.Orange
        Me.btnSelect.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.ForeColor = System.Drawing.Color.Black
        Me.btnSelect.Location = New System.Drawing.Point(1043, 698)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(381, 86)
        Me.btnSelect.TabIndex = 183
        Me.btnSelect.Text = "伝票番号「0001」の伝票を選択"
        Me.btnSelect.UseVisualStyleBackColor = False
        '
        'colUDNDT
        '
        Me.colUDNDT.DataPropertyName = "UDNDT"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colUDNDT.DefaultCellStyle = DataGridViewCellStyle3
        Me.colUDNDT.HeaderText = "伝票日付(時刻)"
        Me.colUDNDT.Name = "colUDNDT"
        Me.colUDNDT.ReadOnly = True
        Me.colUDNDT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colUDNDT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUDNDT.Width = 200
        '
        'colDATKB
        '
        Me.colDATKB.DataPropertyName = "DATKB"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Format = "#,0"
        Me.colDATKB.DefaultCellStyle = DataGridViewCellStyle4
        Me.colDATKB.HeaderText = ""
        Me.colDATKB.Name = "colDATKB"
        Me.colDATKB.ReadOnly = True
        Me.colDATKB.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colDATKB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDATKB.Width = 50
        '
        'colUDNNO
        '
        Me.colUDNNO.DataPropertyName = "UDNNO"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colUDNNO.DefaultCellStyle = DataGridViewCellStyle5
        Me.colUDNNO.HeaderText = "伝票番号"
        Me.colUDNNO.Name = "colUDNNO"
        Me.colUDNNO.ReadOnly = True
        Me.colUDNNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colUDNNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUDNNO.Width = 150
        '
        'colUDNKN
        '
        Me.colUDNKN.DataPropertyName = "UDNBKN"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "#,0"
        Me.colUDNKN.DefaultCellStyle = DataGridViewCellStyle6
        Me.colUDNKN.HeaderText = "合計金額"
        Me.colUDNKN.Name = "colUDNKN"
        Me.colUDNKN.ReadOnly = True
        Me.colUDNKN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colUDNKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUDNKN.Width = 150
        '
        'colDRWNO
        '
        Me.colDRWNO.DataPropertyName = "HOSTNAME"
        Me.colDRWNO.HeaderText = "ドロワNo."
        Me.colDRWNO.Name = "colDRWNO"
        Me.colDRWNO.ReadOnly = True
        Me.colDRWNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colDRWNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDRWNO.Width = 200
        '
        'colDRWCHK
        '
        Me.colDRWCHK.DataPropertyName = "DRAKBN"
        Me.colDRWCHK.HeaderText = "ﾄﾞﾛﾜﾁｪｯｸ"
        Me.colDRWCHK.Name = "colDRWCHK"
        Me.colDRWCHK.ReadOnly = True
        Me.colDRWCHK.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colDRWCHK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDRWCHK.Width = 150
        '
        'colCPAYKBN
        '
        Me.colCPAYKBN.DataPropertyName = "CPAYKBN"
        Me.colCPAYKBN.HeaderText = "支払い区分"
        Me.colCPAYKBN.Name = "colCPAYKBN"
        Me.colCPAYKBN.ReadOnly = True
        Me.colCPAYKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCPAYKBN.Width = 140
        '
        'colMANNO
        '
        Me.colMANNO.DataPropertyName = "MANNO"
        Me.colMANNO.HeaderText = "顧客番号"
        Me.colMANNO.Name = "colMANNO"
        Me.colMANNO.ReadOnly = True
        Me.colMANNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colMANNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMANNO.Width = 150
        '
        'colMANNM
        '
        Me.colMANNM.DataPropertyName = "CCSNAME"
        Me.colMANNM.HeaderText = "氏名"
        Me.colMANNM.Name = "colMANNM"
        Me.colMANNM.ReadOnly = True
        Me.colMANNM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colMANNM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMANNM.Width = 200
        '
        'colHINNMA
        '
        Me.colHINNMA.DataPropertyName = "HINNMA"
        Me.colHINNMA.HeaderText = "商品名"
        Me.colHINNMA.Name = "colHINNMA"
        Me.colHINNMA.ReadOnly = True
        Me.colHINNMA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colHINNMA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colHINNMA.Width = 300
        '
        'hide_UDNDT
        '
        Me.hide_UDNDT.DataPropertyName = "hide_UDNDT"
        Me.hide_UDNDT.HeaderText = "hide_伝票日付"
        Me.hide_UDNDT.Name = "hide_UDNDT"
        Me.hide_UDNDT.ReadOnly = True
        Me.hide_UDNDT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.hide_UDNDT.Visible = False
        '
        'frmUDNDETAIL01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnRETURN)
        Me.Controls.Add(Me.dgvUDNDETAIL)
        Me.Name = "frmUDNDETAIL01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvUDNDETAIL, 0)
        Me.Controls.SetChildIndex(Me.btnRETURN, 0)
        Me.Controls.SetChildIndex(Me.btnSelect, 0)
        CType(Me.dgvUDNDETAIL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvUDNDETAIL As BaseControl.CustomGridView
    Friend WithEvents btnRETURN As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents colUDNDT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDATKB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUDNNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUDNKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDRWNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDRWCHK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPAYKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMANNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMANNM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHINNMA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents hide_UDNDT As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
