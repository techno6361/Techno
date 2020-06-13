<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSTAFFMT01
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvSTAFFMT = New BaseControl.CustomGridView()
        Me.colSTFCODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSTFNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colINSDTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUPDDTM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvSTAFFMT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvSTAFFMT
        '
        Me.dgvSTAFFMT.AllowUserToAddRows = False
        Me.dgvSTAFFMT.AllowUserToDeleteRows = False
        Me.dgvSTAFFMT.AllowUserToResizeColumns = False
        Me.dgvSTAFFMT.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvSTAFFMT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSTAFFMT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvSTAFFMT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSTAFFMT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSTAFFMT.ColumnHeadersHeight = 50
        Me.dgvSTAFFMT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSTAFFMT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSTFCODE, Me.colSTFNAME, Me.colINSDTM, Me.colUPDDTM})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSTAFFMT.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvSTAFFMT.EnableHeadersVisualStyles = False
        Me.dgvSTAFFMT.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvSTAFFMT.Location = New System.Drawing.Point(696, 134)
        Me.dgvSTAFFMT.MultiSelect = False
        Me.dgvSTAFFMT.Name = "dgvSTAFFMT"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSTAFFMT.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvSTAFFMT.RowHeadersVisible = False
        Me.dgvSTAFFMT.RowHeadersWidth = 61
        Me.dgvSTAFFMT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvSTAFFMT.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvSTAFFMT.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvSTAFFMT.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvSTAFFMT.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvSTAFFMT.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSTAFFMT.RowTemplate.Height = 40
        Me.dgvSTAFFMT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvSTAFFMT.Size = New System.Drawing.Size(404, 852)
        Me.dgvSTAFFMT.TabIndex = 9
        '
        'colSTFCODE
        '
        Me.colSTFCODE.DataPropertyName = "STFCODE"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.colSTFCODE.DefaultCellStyle = DataGridViewCellStyle3
        Me.colSTFCODE.HeaderText = "コード"
        Me.colSTFCODE.Name = "colSTFCODE"
        Me.colSTFCODE.ReadOnly = True
        Me.colSTFCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colSTFNAME
        '
        Me.colSTFNAME.DataPropertyName = "STFNAME"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.colSTFNAME.DefaultCellStyle = DataGridViewCellStyle4
        Me.colSTFNAME.HeaderText = "氏名"
        Me.colSTFNAME.MaxInputLength = 10
        Me.colSTFNAME.Name = "colSTFNAME"
        Me.colSTFNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSTFNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSTFNAME.Width = 300
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
        'frmSTAFFMT01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.dgvSTAFFMT)
        Me.Name = "frmSTAFFMT01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvSTAFFMT, 0)
        CType(Me.dgvSTAFFMT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvSTAFFMT As BaseControl.CustomGridView
    Friend WithEvents colSTFCODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSTFNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colINSDTM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUPDDTM As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
