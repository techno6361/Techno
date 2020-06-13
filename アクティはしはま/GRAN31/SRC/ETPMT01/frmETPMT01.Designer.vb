<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmETPMT01
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvETPMTA = New BaseControl.CustomGridView()
        Me.ETPKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ENTCNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCOPY = New System.Windows.Forms.Button()
        Me.cmbNKBNO_COPY = New System.Windows.Forms.ComboBox()
        Me.cmbNKBNO = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvETPMTA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvETPMTA)
        Me.GroupBox1.Controls.Add(Me.btnCOPY)
        Me.GroupBox1.Controls.Add(Me.cmbNKBNO_COPY)
        Me.GroupBox1.Controls.Add(Me.cmbNKBNO)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Location = New System.Drawing.Point(30, 125)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1845, 870)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'dgvETPMTA
        '
        Me.dgvETPMTA.AllowUserToAddRows = False
        Me.dgvETPMTA.AllowUserToDeleteRows = False
        Me.dgvETPMTA.AllowUserToResizeColumns = False
        Me.dgvETPMTA.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgvETPMTA.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvETPMTA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvETPMTA.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvETPMTA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvETPMTA.ColumnHeadersHeight = 50
        Me.dgvETPMTA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvETPMTA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ETPKBN, Me.ENTCNT, Me.POINT})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvETPMTA.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvETPMTA.EnableHeadersVisualStyles = False
        Me.dgvETPMTA.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dgvETPMTA.Location = New System.Drawing.Point(569, 140)
        Me.dgvETPMTA.MultiSelect = False
        Me.dgvETPMTA.Name = "dgvETPMTA"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvETPMTA.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvETPMTA.RowHeadersVisible = False
        Me.dgvETPMTA.RowHeadersWidth = 61
        Me.dgvETPMTA.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvETPMTA.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvETPMTA.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvETPMTA.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvETPMTA.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvETPMTA.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvETPMTA.RowTemplate.Height = 40
        Me.dgvETPMTA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvETPMTA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvETPMTA.Size = New System.Drawing.Size(723, 690)
        Me.dgvETPMTA.TabIndex = 175
        '
        'ETPKBN
        '
        Me.ETPKBN.DataPropertyName = "ETPKBN"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ETPKBN.DefaultCellStyle = DataGridViewCellStyle3
        Me.ETPKBN.HeaderText = "No."
        Me.ETPKBN.MaxInputLength = 2
        Me.ETPKBN.Name = "ETPKBN"
        Me.ETPKBN.ReadOnly = True
        Me.ETPKBN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ETPKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ETPKBN.Width = 104
        '
        'ENTCNT
        '
        Me.ENTCNT.DataPropertyName = "ENTCNT"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ENTCNT.DefaultCellStyle = DataGridViewCellStyle4
        Me.ENTCNT.HeaderText = "来場回数"
        Me.ENTCNT.MaxInputLength = 2
        Me.ENTCNT.Name = "ENTCNT"
        Me.ENTCNT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ENTCNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ENTCNT.Width = 300
        '
        'POINT
        '
        Me.POINT.DataPropertyName = "POINT"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.NullValue = Nothing
        Me.POINT.DefaultCellStyle = DataGridViewCellStyle5
        Me.POINT.HeaderText = "付加ポイント"
        Me.POINT.MaxInputLength = 4
        Me.POINT.Name = "POINT"
        Me.POINT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.POINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.POINT.Width = 300
        '
        'btnCOPY
        '
        Me.btnCOPY.BackColor = System.Drawing.Color.Orange
        Me.btnCOPY.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCOPY.Location = New System.Drawing.Point(487, 76)
        Me.btnCOPY.Name = "btnCOPY"
        Me.btnCOPY.Size = New System.Drawing.Size(170, 35)
        Me.btnCOPY.TabIndex = 174
        Me.btnCOPY.TabStop = False
        Me.btnCOPY.Text = "コピー表示"
        Me.btnCOPY.UseVisualStyleBackColor = False
        '
        'cmbNKBNO_COPY
        '
        Me.cmbNKBNO_COPY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNKBNO_COPY.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNKBNO_COPY.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNKBNO_COPY.FormattingEnabled = True
        Me.cmbNKBNO_COPY.Location = New System.Drawing.Point(235, 76)
        Me.cmbNKBNO_COPY.Name = "cmbNKBNO_COPY"
        Me.cmbNKBNO_COPY.Size = New System.Drawing.Size(246, 35)
        Me.cmbNKBNO_COPY.TabIndex = 173
        Me.cmbNKBNO_COPY.TabStop = False
        '
        'cmbNKBNO
        '
        Me.cmbNKBNO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNKBNO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNKBNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNKBNO.FormattingEnabled = True
        Me.cmbNKBNO.Location = New System.Drawing.Point(235, 29)
        Me.cmbNKBNO.Name = "cmbNKBNO"
        Me.cmbNKBNO.Size = New System.Drawing.Size(246, 35)
        Me.cmbNKBNO.TabIndex = 172
        Me.cmbNKBNO.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(37, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 34)
        Me.Label1.TabIndex = 171
        Me.Label1.Text = "コピー元顧客種別"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(37, 29)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(192, 34)
        Me.Label22.TabIndex = 170
        Me.Label22.Text = "顧客種別"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmETPMT01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmETPMT01"
        Me.ShowInTaskbar = False
        Me.Text = "月間来場ポイントマスタ登録"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvETPMTA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbNKBNO_COPY As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNKBNO As System.Windows.Forms.ComboBox
    Friend WithEvents btnCOPY As System.Windows.Forms.Button
    Friend WithEvents dgvETPMTA As BaseControl.CustomGridView
    Friend WithEvents ETPKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ENTCNT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents POINT As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
