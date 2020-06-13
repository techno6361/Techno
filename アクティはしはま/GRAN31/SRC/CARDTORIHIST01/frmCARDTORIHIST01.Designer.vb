<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCARDTORIHIST01
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
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.dgvTORIHIKI = New BaseControl.CustomGridView()
        Me.colNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUDNDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUDNTIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBZANKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBPREMKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBPOINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colKINGAKU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPREMKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPOINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAZANKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAPREMKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAPOINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblNCSNO = New System.Windows.Forms.Label()
        Me.lblCCSNAME = New System.Windows.Forms.Label()
        CType(Me.dgvTORIHIKI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(29, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 35)
        Me.Label1.TabIndex = 1014
        Me.Label1.Text = "顧客番号"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnNext.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(1724, 173)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(155, 42)
        Me.btnNext.TabIndex = 1113
        Me.btnNext.Text = "次ページ"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1563, 173)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(155, 42)
        Me.btnBack.TabIndex = 1112
        Me.btnBack.Text = "前ページ"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'dgvTORIHIKI
        '
        Me.dgvTORIHIKI.AllowUserToAddRows = False
        Me.dgvTORIHIKI.AllowUserToDeleteRows = False
        Me.dgvTORIHIKI.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvTORIHIKI.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTORIHIKI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTORIHIKI.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTORIHIKI.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTORIHIKI.ColumnHeadersHeight = 50
        Me.dgvTORIHIKI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvTORIHIKI.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNO, Me.colUDNDT, Me.colUDNTIME, Me.colKBN, Me.colBZANKN, Me.colBPREMKN, Me.colBPOINT, Me.colKINGAKU, Me.colPREMKN, Me.colPOINT, Me.colAZANKN, Me.colAPREMKN, Me.colAPOINT})
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle16.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTORIHIKI.DefaultCellStyle = DataGridViewCellStyle16
        Me.dgvTORIHIKI.EnableHeadersVisualStyles = False
        Me.dgvTORIHIKI.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvTORIHIKI.Location = New System.Drawing.Point(29, 226)
        Me.dgvTORIHIKI.MultiSelect = False
        Me.dgvTORIHIKI.Name = "dgvTORIHIKI"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle17.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTORIHIKI.RowHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.dgvTORIHIKI.RowHeadersVisible = False
        Me.dgvTORIHIKI.RowHeadersWidth = 61
        Me.dgvTORIHIKI.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvTORIHIKI.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvTORIHIKI.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvTORIHIKI.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvTORIHIKI.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvTORIHIKI.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvTORIHIKI.RowTemplate.Height = 40
        Me.dgvTORIHIKI.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvTORIHIKI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvTORIHIKI.Size = New System.Drawing.Size(1853, 732)
        Me.dgvTORIHIKI.TabIndex = 1111
        '
        'colNO
        '
        Me.colNO.DataPropertyName = "NO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.colNO.Frozen = True
        Me.colNO.HeaderText = "№"
        Me.colNO.Name = "colNO"
        Me.colNO.ReadOnly = True
        Me.colNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNO.Width = 65
        '
        'colUDNDT
        '
        Me.colUDNDT.DataPropertyName = "UDNDT"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.NullValue = Nothing
        Me.colUDNDT.DefaultCellStyle = DataGridViewCellStyle4
        Me.colUDNDT.Frozen = True
        Me.colUDNDT.HeaderText = "日付"
        Me.colUDNDT.Name = "colUDNDT"
        Me.colUDNDT.ReadOnly = True
        Me.colUDNDT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colUDNDT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUDNDT.Width = 135
        '
        'colUDNTIME
        '
        Me.colUDNTIME.DataPropertyName = "UDNTIME"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colUDNTIME.DefaultCellStyle = DataGridViewCellStyle5
        Me.colUDNTIME.Frozen = True
        Me.colUDNTIME.HeaderText = "時間"
        Me.colUDNTIME.Name = "colUDNTIME"
        Me.colUDNTIME.ReadOnly = True
        Me.colUDNTIME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colUDNTIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUDNTIME.Width = 115
        '
        'colKBN
        '
        Me.colKBN.DataPropertyName = "KBN"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colKBN.DefaultCellStyle = DataGridViewCellStyle6
        Me.colKBN.Frozen = True
        Me.colKBN.HeaderText = "区分"
        Me.colKBN.Name = "colKBN"
        Me.colKBN.ReadOnly = True
        Me.colKBN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colKBN.Width = 245
        '
        'colBZANKN
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colBZANKN.DefaultCellStyle = DataGridViewCellStyle7
        Me.colBZANKN.HeaderText = "(前)残金"
        Me.colBZANKN.Name = "colBZANKN"
        Me.colBZANKN.ReadOnly = True
        Me.colBZANKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colBZANKN.Width = 150
        '
        'colBPREMKN
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colBPREMKN.DefaultCellStyle = DataGridViewCellStyle8
        Me.colBPREMKN.HeaderText = "(前)残ﾌﾟﾚﾐｱﾑ"
        Me.colBPREMKN.Name = "colBPREMKN"
        Me.colBPREMKN.ReadOnly = True
        Me.colBPREMKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colBPREMKN.Width = 150
        '
        'colBPOINT
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colBPOINT.DefaultCellStyle = DataGridViewCellStyle9
        Me.colBPOINT.HeaderText = "(前)残ﾎﾟｲﾝﾄ"
        Me.colBPOINT.Name = "colBPOINT"
        Me.colBPOINT.ReadOnly = True
        Me.colBPOINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colBPOINT.Width = 150
        '
        'colKINGAKU
        '
        Me.colKINGAKU.DataPropertyName = "KINGAKU"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colKINGAKU.DefaultCellStyle = DataGridViewCellStyle10
        Me.colKINGAKU.HeaderText = "金額"
        Me.colKINGAKU.Name = "colKINGAKU"
        Me.colKINGAKU.ReadOnly = True
        Me.colKINGAKU.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colKINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colKINGAKU.Width = 130
        '
        'colPREMKN
        '
        Me.colPREMKN.DataPropertyName = "PREMKN"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "#,0"
        Me.colPREMKN.DefaultCellStyle = DataGridViewCellStyle11
        Me.colPREMKN.HeaderText = "ﾌﾟﾚﾐｱﾑ"
        Me.colPREMKN.Name = "colPREMKN"
        Me.colPREMKN.ReadOnly = True
        Me.colPREMKN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colPREMKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPREMKN.Width = 130
        '
        'colPOINT
        '
        Me.colPOINT.DataPropertyName = "POINT"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colPOINT.DefaultCellStyle = DataGridViewCellStyle12
        Me.colPOINT.HeaderText = "ﾎﾟｲﾝﾄ"
        Me.colPOINT.Name = "colPOINT"
        Me.colPOINT.ReadOnly = True
        Me.colPOINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPOINT.Width = 130
        '
        'colAZANKN
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colAZANKN.DefaultCellStyle = DataGridViewCellStyle13
        Me.colAZANKN.HeaderText = "(後)残金"
        Me.colAZANKN.Name = "colAZANKN"
        Me.colAZANKN.ReadOnly = True
        Me.colAZANKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colAZANKN.Width = 150
        '
        'colAPREMKN
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colAPREMKN.DefaultCellStyle = DataGridViewCellStyle14
        Me.colAPREMKN.HeaderText = "(後)残ﾌﾟﾚﾐｱﾑ"
        Me.colAPREMKN.Name = "colAPREMKN"
        Me.colAPREMKN.ReadOnly = True
        Me.colAPREMKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colAPREMKN.Width = 150
        '
        'colAPOINT
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colAPOINT.DefaultCellStyle = DataGridViewCellStyle15
        Me.colAPOINT.HeaderText = "(後)残ﾎﾟｲﾝﾄ"
        Me.colAPOINT.Name = "colAPOINT"
        Me.colAPOINT.ReadOnly = True
        Me.colAPOINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colAPOINT.Width = 150
        '
        'lblNCSNO
        '
        Me.lblNCSNO.BackColor = System.Drawing.Color.White
        Me.lblNCSNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNCSNO.ForeColor = System.Drawing.Color.Black
        Me.lblNCSNO.Location = New System.Drawing.Point(132, 138)
        Me.lblNCSNO.Name = "lblNCSNO"
        Me.lblNCSNO.Size = New System.Drawing.Size(107, 35)
        Me.lblNCSNO.TabIndex = 1114
        Me.lblNCSNO.Text = "99999999"
        Me.lblNCSNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCCSNAME
        '
        Me.lblCCSNAME.BackColor = System.Drawing.Color.White
        Me.lblCCSNAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCCSNAME.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCCSNAME.ForeColor = System.Drawing.Color.Black
        Me.lblCCSNAME.Location = New System.Drawing.Point(238, 138)
        Me.lblCCSNAME.Name = "lblCCSNAME"
        Me.lblCCSNAME.Size = New System.Drawing.Size(204, 35)
        Me.lblCCSNAME.TabIndex = 1115
        Me.lblCCSNAME.Text = "ああああああああああ"
        Me.lblCCSNAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmCARDTORIHIST01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.lblCCSNAME)
        Me.Controls.Add(Me.lblNCSNO)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.dgvTORIHIKI)
        Me.Name = "frmCARDTORIHIST01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvTORIHIKI, 0)
        Me.Controls.SetChildIndex(Me.btnBack, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnNext, 0)
        Me.Controls.SetChildIndex(Me.lblNCSNO, 0)
        Me.Controls.SetChildIndex(Me.lblCCSNAME, 0)
        CType(Me.dgvTORIHIKI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents dgvTORIHIKI As BaseControl.CustomGridView
    Friend WithEvents lblNCSNO As System.Windows.Forms.Label
    Friend WithEvents lblCCSNAME As System.Windows.Forms.Label
    Friend WithEvents colNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUDNDT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUDNTIME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBZANKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBPREMKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBPOINT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colKINGAKU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPREMKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPOINT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAZANKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAPREMKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAPOINT As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
