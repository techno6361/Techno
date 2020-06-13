<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmENTHIST01
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvENTHIST = New BaseControl.CustomGridView()
        Me.colENTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colENTTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCKBNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMANNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCCSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colENTBKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        CType(Me.dgvENTHIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvENTHIST
        '
        Me.dgvENTHIST.AllowUserToAddRows = False
        Me.dgvENTHIST.AllowUserToDeleteRows = False
        Me.dgvENTHIST.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvENTHIST.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvENTHIST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvENTHIST.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvENTHIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvENTHIST.ColumnHeadersHeight = 50
        Me.dgvENTHIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvENTHIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colENTNO, Me.colENTTM, Me.colCKBNAME, Me.colMANNO, Me.colCCSNAME, Me.colENTBKN, Me.POINT})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvENTHIST.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvENTHIST.EnableHeadersVisualStyles = False
        Me.dgvENTHIST.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvENTHIST.Location = New System.Drawing.Point(308, 208)
        Me.dgvENTHIST.MultiSelect = False
        Me.dgvENTHIST.Name = "dgvENTHIST"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvENTHIST.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvENTHIST.RowHeadersVisible = False
        Me.dgvENTHIST.RowHeadersWidth = 61
        Me.dgvENTHIST.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvENTHIST.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvENTHIST.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvENTHIST.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvENTHIST.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvENTHIST.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvENTHIST.RowTemplate.Height = 40
        Me.dgvENTHIST.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvENTHIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvENTHIST.Size = New System.Drawing.Size(1351, 690)
        Me.dgvENTHIST.TabIndex = 1083
        '
        'colENTNO
        '
        Me.colENTNO.DataPropertyName = "ENTNO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colENTNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.colENTNO.Frozen = True
        Me.colENTNO.HeaderText = "受付順"
        Me.colENTNO.Name = "colENTNO"
        Me.colENTNO.ReadOnly = True
        Me.colENTNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colENTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colENTNO.Width = 150
        '
        'colENTTM
        '
        Me.colENTTM.DataPropertyName = "ENTTM"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.NullValue = Nothing
        Me.colENTTM.DefaultCellStyle = DataGridViewCellStyle4
        Me.colENTTM.Frozen = True
        Me.colENTTM.HeaderText = "時間"
        Me.colENTTM.Name = "colENTTM"
        Me.colENTTM.ReadOnly = True
        Me.colENTTM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colENTTM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colENTTM.Width = 150
        '
        'colCKBNAME
        '
        Me.colCKBNAME.DataPropertyName = "CKBNAME"
        Me.colCKBNAME.Frozen = True
        Me.colCKBNAME.HeaderText = "受付内容"
        Me.colCKBNAME.Name = "colCKBNAME"
        Me.colCKBNAME.ReadOnly = True
        Me.colCKBNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCKBNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCKBNAME.Width = 300
        '
        'colMANNO
        '
        Me.colMANNO.DataPropertyName = "MANNO"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colMANNO.DefaultCellStyle = DataGridViewCellStyle5
        Me.colMANNO.HeaderText = "顧客番号"
        Me.colMANNO.Name = "colMANNO"
        Me.colMANNO.ReadOnly = True
        Me.colMANNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colMANNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMANNO.Width = 150
        '
        'colCCSNAME
        '
        Me.colCCSNAME.DataPropertyName = "CCSNAME"
        Me.colCCSNAME.HeaderText = "氏名"
        Me.colCCSNAME.Name = "colCCSNAME"
        Me.colCCSNAME.ReadOnly = True
        Me.colCCSNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCCSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCCSNAME.Width = 300
        '
        'colENTBKN
        '
        Me.colENTBKN.DataPropertyName = "ENTBKN"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "#,0"
        Me.colENTBKN.DefaultCellStyle = DataGridViewCellStyle6
        Me.colENTBKN.HeaderText = "入場料"
        Me.colENTBKN.Name = "colENTBKN"
        Me.colENTBKN.ReadOnly = True
        Me.colENTBKN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colENTBKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colENTBKN.Width = 200
        '
        'POINT
        '
        Me.POINT.DataPropertyName = "POINT"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "#,0"
        Me.POINT.DefaultCellStyle = DataGridViewCellStyle7
        Me.POINT.HeaderText = "ポイント"
        Me.POINT.Name = "POINT"
        Me.POINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.Location = New System.Drawing.Point(315, 181)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(66, 21)
        Me.lblCount.TabIndex = 1084
        Me.lblCount.Text = "全0件"
        '
        'btnPrev
        '
        Me.btnPrev.BackColor = System.Drawing.Color.Orange
        Me.btnPrev.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrev.ForeColor = System.Drawing.Color.Black
        Me.btnPrev.Location = New System.Drawing.Point(1343, 160)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(155, 42)
        Me.btnPrev.TabIndex = 1107
        Me.btnPrev.Text = "前ページ"
        Me.btnPrev.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Orange
        Me.btnNext.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNext.ForeColor = System.Drawing.Color.Black
        Me.btnNext.Location = New System.Drawing.Point(1504, 160)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(155, 42)
        Me.btnNext.TabIndex = 1108
        Me.btnNext.Text = "次ページ"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'frmENTHIST01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.dgvENTHIST)
        Me.Name = "frmENTHIST01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvENTHIST, 0)
        Me.Controls.SetChildIndex(Me.lblCount, 0)
        Me.Controls.SetChildIndex(Me.btnPrev, 0)
        Me.Controls.SetChildIndex(Me.btnNext, 0)
        CType(Me.dgvENTHIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvENTHIST As BaseControl.CustomGridView
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents colENTNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colENTTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCKBNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMANNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCCSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colENTBKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents POINT As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
