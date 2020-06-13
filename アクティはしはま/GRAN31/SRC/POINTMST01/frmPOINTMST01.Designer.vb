<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOINTMST01
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
        Me.dgvPOINTMST = New BaseControl.CustomGridView()
        Me.colNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPOINTNM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPOINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colINSDTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUPDDTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPOINTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvPOINTMST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPOINTMST
        '
        Me.dgvPOINTMST.AllowUserToAddRows = False
        Me.dgvPOINTMST.AllowUserToDeleteRows = False
        Me.dgvPOINTMST.AllowUserToResizeColumns = False
        Me.dgvPOINTMST.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvPOINTMST.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPOINTMST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvPOINTMST.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPOINTMST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPOINTMST.ColumnHeadersHeight = 50
        Me.dgvPOINTMST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvPOINTMST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNO, Me.colPOINTNM, Me.colPOINT, Me.colINSDTM, Me.colUPDDTM, Me.colPOINTNO})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPOINTMST.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPOINTMST.EnableHeadersVisualStyles = False
        Me.dgvPOINTMST.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvPOINTMST.Location = New System.Drawing.Point(656, 251)
        Me.dgvPOINTMST.MultiSelect = False
        Me.dgvPOINTMST.Name = "dgvPOINTMST"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPOINTMST.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvPOINTMST.RowHeadersVisible = False
        Me.dgvPOINTMST.RowHeadersWidth = 61
        Me.dgvPOINTMST.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvPOINTMST.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvPOINTMST.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvPOINTMST.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvPOINTMST.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvPOINTMST.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvPOINTMST.RowTemplate.Height = 40
        Me.dgvPOINTMST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvPOINTMST.Size = New System.Drawing.Size(504, 652)
        Me.dgvPOINTMST.TabIndex = 9
        '
        'colNO
        '
        Me.colNO.DataPropertyName = "NO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.colNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.colNO.HeaderText = "№"
        Me.colNO.Name = "colNO"
        Me.colNO.ReadOnly = True
        Me.colNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNO.Width = 50
        '
        'colPOINTNM
        '
        Me.colPOINTNM.DataPropertyName = "POINTNM"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.colPOINTNM.DefaultCellStyle = DataGridViewCellStyle4
        Me.colPOINTNM.HeaderText = "ポイント名称"
        Me.colPOINTNM.MaxInputLength = 10
        Me.colPOINTNM.Name = "colPOINTNM"
        Me.colPOINTNM.ReadOnly = True
        Me.colPOINTNM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colPOINTNM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPOINTNM.Width = 300
        '
        'colPOINT
        '
        Me.colPOINT.DataPropertyName = "POINT"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.Format = "C0"
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.colPOINT.DefaultCellStyle = DataGridViewCellStyle5
        Me.colPOINT.HeaderText = "ポイント数"
        Me.colPOINT.MaxInputLength = 5
        Me.colPOINT.Name = "colPOINT"
        Me.colPOINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPOINT.Width = 150
        '
        'colINSDTM
        '
        Me.colINSDTM.DataPropertyName = "INSDTM"
        Me.colINSDTM.HeaderText = "作成日時"
        Me.colINSDTM.Name = "colINSDTM"
        Me.colINSDTM.ReadOnly = True
        Me.colINSDTM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colINSDTM.Visible = False
        '
        'colUPDDTM
        '
        Me.colUPDDTM.DataPropertyName = "UPDDTM"
        Me.colUPDDTM.HeaderText = "更新日時"
        Me.colUPDDTM.Name = "colUPDDTM"
        Me.colUPDDTM.ReadOnly = True
        Me.colUPDDTM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUPDDTM.Visible = False
        '
        'colPOINTNO
        '
        Me.colPOINTNO.DataPropertyName = "POINTNO"
        Me.colPOINTNO.HeaderText = "ポイント№"
        Me.colPOINTNO.Name = "colPOINTNO"
        Me.colPOINTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPOINTNO.Visible = False
        '
        'frmPOINTMST01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.dgvPOINTMST)
        Me.Name = "frmPOINTMST01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvPOINTMST, 0)
        CType(Me.dgvPOINTMST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvPOINTMST As BaseControl.CustomGridView
    Friend WithEvents colNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPOINTNM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPOINT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colINSDTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUPDDTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPOINTNO As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
