<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINT02
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
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dtpEndSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.cmbTerm = New System.Windows.Forms.ComboBox()
        Me.dtpStaSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dgvSEATSMB = New BaseControl.CustomGridView()
        Me.SEATNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOSU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALLDOSU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOSURITU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALLRITU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvSEATSMB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Moccasin
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.dtpEndSEATDT)
        Me.Panel2.Controls.Add(Me.cmbTerm)
        Me.Panel2.Controls.Add(Me.dtpStaSEATDT)
        Me.Panel2.Controls.Add(Me.Label49)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Location = New System.Drawing.Point(25, 130)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(639, 83)
        Me.Panel2.TabIndex = 1024
        '
        'dtpEndSEATDT
        '
        Me.dtpEndSEATDT.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpEndSEATDT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndSEATDT.Location = New System.Drawing.Point(324, 5)
        Me.dtpEndSEATDT.Name = "dtpEndSEATDT"
        Me.dtpEndSEATDT.Size = New System.Drawing.Size(178, 34)
        Me.dtpEndSEATDT.TabIndex = 2
        '
        'cmbTerm
        '
        Me.cmbTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTerm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbTerm.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTerm.FormattingEnabled = True
        Me.cmbTerm.Items.AddRange(New Object() {"任意入力", "今月", "前月", "今年", "前年"})
        Me.cmbTerm.Location = New System.Drawing.Point(107, 42)
        Me.cmbTerm.Name = "cmbTerm"
        Me.cmbTerm.Size = New System.Drawing.Size(395, 35)
        Me.cmbTerm.TabIndex = 1013
        Me.cmbTerm.TabStop = False
        '
        'dtpStaSEATDT
        '
        Me.dtpStaSEATDT.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpStaSEATDT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStaSEATDT.Location = New System.Drawing.Point(107, 5)
        Me.dtpStaSEATDT.Name = "dtpStaSEATDT"
        Me.dtpStaSEATDT.Size = New System.Drawing.Size(178, 34)
        Me.dtpStaSEATDT.TabIndex = 1
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label49.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.White
        Me.Label49.Location = New System.Drawing.Point(4, 42)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(102, 35)
        Me.Label49.TabIndex = 1013
        Me.Label49.Text = "期間"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(289, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 20)
        Me.Label4.TabIndex = 1012
        Me.Label4.Text = "～"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(4, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 32)
        Me.Label3.TabIndex = 1010
        Me.Label3.Text = "営業日"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.btnSearch.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(508, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(126, 74)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Tag = "　"
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'dgvSEATSMB
        '
        Me.dgvSEATSMB.AllowUserToAddRows = False
        Me.dgvSEATSMB.AllowUserToDeleteRows = False
        Me.dgvSEATSMB.AllowUserToResizeColumns = False
        Me.dgvSEATSMB.AllowUserToResizeRows = False
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvSEATSMB.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvSEATSMB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvSEATSMB.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle13.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSEATSMB.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvSEATSMB.ColumnHeadersHeight = 30
        Me.dgvSEATSMB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSEATSMB.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SEATNO, Me.BALL, Me.DOSU, Me.TIME, Me.BALLDOSU, Me.DOSURITU, Me.BALLRITU})
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle21.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSEATSMB.DefaultCellStyle = DataGridViewCellStyle21
        Me.dgvSEATSMB.EnableHeadersVisualStyles = False
        Me.dgvSEATSMB.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvSEATSMB.Location = New System.Drawing.Point(425, 233)
        Me.dgvSEATSMB.MultiSelect = False
        Me.dgvSEATSMB.Name = "dgvSEATSMB"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSEATSMB.RowHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.dgvSEATSMB.RowHeadersVisible = False
        Me.dgvSEATSMB.RowHeadersWidth = 30
        Me.dgvSEATSMB.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvSEATSMB.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvSEATSMB.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvSEATSMB.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvSEATSMB.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSEATSMB.RowTemplate.Height = 40
        Me.dgvSEATSMB.Size = New System.Drawing.Size(1020, 752)
        Me.dgvSEATSMB.TabIndex = 1025
        '
        'SEATNO
        '
        Me.SEATNO.DataPropertyName = "SEATNO"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        Me.SEATNO.DefaultCellStyle = DataGridViewCellStyle14
        Me.SEATNO.HeaderText = "打席番号"
        Me.SEATNO.MaxInputLength = 10
        Me.SEATNO.Name = "SEATNO"
        Me.SEATNO.ReadOnly = True
        Me.SEATNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SEATNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BALL
        '
        Me.BALL.DataPropertyName = "BALL"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black
        Me.BALL.DefaultCellStyle = DataGridViewCellStyle15
        Me.BALL.HeaderText = "使用球数"
        Me.BALL.MaxInputLength = 5
        Me.BALL.Name = "BALL"
        Me.BALL.ReadOnly = True
        Me.BALL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BALL.Width = 150
        '
        'DOSU
        '
        Me.DOSU.DataPropertyName = "DOSU"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle16.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black
        Me.DOSU.DefaultCellStyle = DataGridViewCellStyle16
        Me.DOSU.HeaderText = "使用回数"
        Me.DOSU.Name = "DOSU"
        Me.DOSU.ReadOnly = True
        Me.DOSU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DOSU.Width = 150
        '
        'TIME
        '
        Me.TIME.DataPropertyName = "TIME"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black
        Me.TIME.DefaultCellStyle = DataGridViewCellStyle17
        Me.TIME.HeaderText = "使用時間"
        Me.TIME.MaxInputLength = 2
        Me.TIME.Name = "TIME"
        Me.TIME.ReadOnly = True
        Me.TIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TIME.Width = 150
        '
        'BALLDOSU
        '
        Me.BALLDOSU.DataPropertyName = "BALLDOSU"
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black
        Me.BALLDOSU.DefaultCellStyle = DataGridViewCellStyle18
        Me.BALLDOSU.HeaderText = "球数/回数"
        Me.BALLDOSU.MaxInputLength = 2
        Me.BALLDOSU.Name = "BALLDOSU"
        Me.BALLDOSU.ReadOnly = True
        Me.BALLDOSU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BALLDOSU.Width = 150
        '
        'DOSURITU
        '
        Me.DOSURITU.DataPropertyName = "DOSURITU"
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle19.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black
        Me.DOSURITU.DefaultCellStyle = DataGridViewCellStyle19
        Me.DOSURITU.HeaderText = "回数率"
        Me.DOSURITU.Name = "DOSURITU"
        Me.DOSURITU.ReadOnly = True
        Me.DOSURITU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DOSURITU.Width = 150
        '
        'BALLRITU
        '
        Me.BALLRITU.DataPropertyName = "BALLRITU"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle20.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle20.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black
        Me.BALLRITU.DefaultCellStyle = DataGridViewCellStyle20
        Me.BALLRITU.HeaderText = "球数率"
        Me.BALLRITU.Name = "BALLRITU"
        Me.BALLRITU.ReadOnly = True
        Me.BALLRITU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.BALLRITU.Width = 150
        '
        'frmPRINT02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.dgvSEATSMB)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmPRINT02"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.dgvSEATSMB, 0)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvSEATSMB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dtpEndSEATDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTerm As System.Windows.Forms.ComboBox
    Friend WithEvents dtpStaSEATDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents dgvSEATSMB As BaseControl.CustomGridView
    Friend WithEvents SEATNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BALL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOSU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TIME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BALLDOSU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOSURITU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BALLRITU As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
