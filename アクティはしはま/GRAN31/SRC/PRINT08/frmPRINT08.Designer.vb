﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINT08
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.plDate1 = New System.Windows.Forms.Panel()
        Me.dtpEndSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.cmbTerm = New System.Windows.Forms.ComboBox()
        Me.dtpStaSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbKB2 = New System.Windows.Forms.RadioButton()
        Me.rbKB4 = New System.Windows.Forms.RadioButton()
        Me.rbKB1 = New System.Windows.Forms.RadioButton()
        Me.rbKB_ALL = New System.Windows.Forms.RadioButton()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dgvNKNTRN = New BaseControl.CustomGridView()
        Me.ETPKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ENTCNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POINT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnFIND = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblPage = New System.Windows.Forms.Label()
        Me.lblMaxPage = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMaxCount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblStaCount = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblEndCount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlPrintStatus = New BaseControl.StatusPanel()
        Me.btnCSMAST01 = New System.Windows.Forms.Button()
        Me.rbKB3 = New System.Windows.Forms.RadioButton()
        Me.plDate1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvNKNTRN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'plDate1
        '
        Me.plDate1.BackColor = System.Drawing.Color.Moccasin
        Me.plDate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plDate1.Controls.Add(Me.dtpEndSEATDT)
        Me.plDate1.Controls.Add(Me.cmbTerm)
        Me.plDate1.Controls.Add(Me.dtpStaSEATDT)
        Me.plDate1.Controls.Add(Me.Label49)
        Me.plDate1.Controls.Add(Me.Label4)
        Me.plDate1.Controls.Add(Me.Label3)
        Me.plDate1.Location = New System.Drawing.Point(25, 136)
        Me.plDate1.Name = "plDate1"
        Me.plDate1.Size = New System.Drawing.Size(516, 83)
        Me.plDate1.TabIndex = 0
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
        Me.cmbTerm.TabIndex = 3
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
        Me.Label3.Text = "来場日"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbKB3)
        Me.GroupBox1.Controls.Add(Me.rbKB2)
        Me.GroupBox1.Controls.Add(Me.rbKB4)
        Me.GroupBox1.Controls.Add(Me.rbKB1)
        Me.GroupBox1.Controls.Add(Me.rbKB_ALL)
        Me.GroupBox1.Location = New System.Drawing.Point(711, 132)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(671, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'rbKB2
        '
        Me.rbKB2.AutoSize = True
        Me.rbKB2.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbKB2.Location = New System.Drawing.Point(186, 34)
        Me.rbKB2.Name = "rbKB2"
        Me.rbKB2.Size = New System.Drawing.Size(111, 31)
        Me.rbKB2.TabIndex = 3
        Me.rbKB2.Text = "入金機"
        Me.rbKB2.UseVisualStyleBackColor = True
        '
        'rbKB4
        '
        Me.rbKB4.AutoSize = True
        Me.rbKB4.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbKB4.Location = New System.Drawing.Point(525, 34)
        Me.rbKB4.Name = "rbKB4"
        Me.rbKB4.Size = New System.Drawing.Size(114, 31)
        Me.rbKB4.TabIndex = 2
        Me.rbKB4.Text = "サービス"
        Me.rbKB4.UseVisualStyleBackColor = True
        '
        'rbKB1
        '
        Me.rbKB1.AutoSize = True
        Me.rbKB1.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbKB1.Location = New System.Drawing.Point(96, 34)
        Me.rbKB1.Name = "rbKB1"
        Me.rbKB1.Size = New System.Drawing.Size(84, 31)
        Me.rbKB1.TabIndex = 1
        Me.rbKB1.Text = "入金"
        Me.rbKB1.UseVisualStyleBackColor = True
        '
        'rbKB_ALL
        '
        Me.rbKB_ALL.AutoSize = True
        Me.rbKB_ALL.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbKB_ALL.Location = New System.Drawing.Point(12, 34)
        Me.rbKB_ALL.Name = "rbKB_ALL"
        Me.rbKB_ALL.Size = New System.Drawing.Size(78, 31)
        Me.rbKB_ALL.TabIndex = 0
        Me.rbKB_ALL.Text = "全て"
        Me.rbKB_ALL.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(569, 138)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(136, 82)
        Me.Label22.TabIndex = 1077
        Me.Label22.Text = "入金区分"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvNKNTRN
        '
        Me.dgvNKNTRN.AllowUserToAddRows = False
        Me.dgvNKNTRN.AllowUserToDeleteRows = False
        Me.dgvNKNTRN.AllowUserToResizeColumns = False
        Me.dgvNKNTRN.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgvNKNTRN.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvNKNTRN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvNKNTRN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNKNTRN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvNKNTRN.ColumnHeadersHeight = 50
        Me.dgvNKNTRN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvNKNTRN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ETPKBN, Me.Column1, Me.Column2, Me.MANNO, Me.CCSNAME, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.ENTCNT, Me.POINT})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNKNTRN.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvNKNTRN.EnableHeadersVisualStyles = False
        Me.dgvNKNTRN.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dgvNKNTRN.Location = New System.Drawing.Point(132, 255)
        Me.dgvNKNTRN.MultiSelect = False
        Me.dgvNKNTRN.Name = "dgvNKNTRN"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvNKNTRN.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvNKNTRN.RowHeadersVisible = False
        Me.dgvNKNTRN.RowHeadersWidth = 61
        Me.dgvNKNTRN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvNKNTRN.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvNKNTRN.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvNKNTRN.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvNKNTRN.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvNKNTRN.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvNKNTRN.RowTemplate.Height = 40
        Me.dgvNKNTRN.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvNKNTRN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNKNTRN.Size = New System.Drawing.Size(1653, 691)
        Me.dgvNKNTRN.TabIndex = 1079
        '
        'ETPKBN
        '
        Me.ETPKBN.DataPropertyName = "INSDTM1"
        Me.ETPKBN.HeaderText = "取引日"
        Me.ETPKBN.MaxInputLength = 2
        Me.ETPKBN.Name = "ETPKBN"
        Me.ETPKBN.ReadOnly = True
        Me.ETPKBN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ETPKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ETPKBN.Width = 150
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "INSDTM2"
        Me.Column1.HeaderText = "時間"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "STSFLG"
        Me.Column2.HeaderText = "入金区分"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MANNO
        '
        Me.MANNO.DataPropertyName = "MANNO"
        Me.MANNO.HeaderText = "顧客番号"
        Me.MANNO.Name = "MANNO"
        Me.MANNO.ReadOnly = True
        Me.MANNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MANNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CCSNAME
        '
        Me.CCSNAME.DataPropertyName = "CCSNAME"
        Me.CCSNAME.HeaderText = "氏名"
        Me.CCSNAME.Name = "CCSNAME"
        Me.CCSNAME.ReadOnly = True
        Me.CCSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CCSNAME.Width = 200
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "NKNKN"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column4.HeaderText = "入金額"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "PREMKN"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column5.HeaderText = "ﾌﾟﾚﾐｱﾑ"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column6
        '
        Me.Column6.DataPropertyName = "POINT"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column6.HeaderText = "ポイント"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "STFNAME"
        Me.Column7.HeaderText = "担当者"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column7.Width = 200
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "NYUKN"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column8.HeaderText = "投入金額"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column9
        '
        Me.Column9.DataPropertyName = "TURIKN"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Me.Column9.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column9.HeaderText = "おつり"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column10
        '
        Me.Column10.DataPropertyName = "CPAYKBN"
        Me.Column10.HeaderText = "支払区分"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ENTCNT
        '
        Me.ENTCNT.DataPropertyName = "ZANAKN"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Me.ENTCNT.DefaultCellStyle = DataGridViewCellStyle8
        Me.ENTCNT.HeaderText = "入金前"
        Me.ENTCNT.Name = "ENTCNT"
        Me.ENTCNT.ReadOnly = True
        Me.ENTCNT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ENTCNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'POINT
        '
        Me.POINT.DataPropertyName = "ZANBKN"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.POINT.DefaultCellStyle = DataGridViewCellStyle9
        Me.POINT.HeaderText = "入金後"
        Me.POINT.Name = "POINT"
        Me.POINT.ReadOnly = True
        Me.POINT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.POINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'btnFIND
        '
        Me.btnFIND.BackColor = System.Drawing.Color.Orange
        Me.btnFIND.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFIND.Location = New System.Drawing.Point(1388, 136)
        Me.btnFIND.Name = "btnFIND"
        Me.btnFIND.Size = New System.Drawing.Size(152, 83)
        Me.btnFIND.TabIndex = 1080
        Me.btnFIND.TabStop = False
        Me.btnFIND.Text = "検索"
        Me.btnFIND.UseVisualStyleBackColor = False
        '
        'btnPrev
        '
        Me.btnPrev.BackColor = System.Drawing.Color.Orange
        Me.btnPrev.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrev.Location = New System.Drawing.Point(1546, 135)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(116, 84)
        Me.btnPrev.TabIndex = 1081
        Me.btnPrev.TabStop = False
        Me.btnPrev.Text = "前へ"
        Me.btnPrev.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Orange
        Me.btnNext.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNext.Location = New System.Drawing.Point(1668, 135)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(116, 84)
        Me.btnNext.TabIndex = 1082
        Me.btnNext.TabStop = False
        Me.btnNext.Text = "次へ"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'lblPage
        '
        Me.lblPage.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPage.Location = New System.Drawing.Point(121, 951)
        Me.lblPage.Name = "lblPage"
        Me.lblPage.Size = New System.Drawing.Size(67, 21)
        Me.lblPage.TabIndex = 1083
        Me.lblPage.Text = "0001"
        Me.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMaxPage
        '
        Me.lblMaxPage.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxPage.Location = New System.Drawing.Point(206, 951)
        Me.lblMaxPage.Name = "lblMaxPage"
        Me.lblMaxPage.Size = New System.Drawing.Size(59, 21)
        Me.lblMaxPage.TabIndex = 1084
        Me.lblMaxPage.Text = "0001"
        Me.lblMaxPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(184, 951)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 21)
        Me.Label1.TabIndex = 1085
        Me.Label1.Text = "/"
        '
        'lblMaxCount
        '
        Me.lblMaxCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxCount.Location = New System.Drawing.Point(1245, 951)
        Me.lblMaxCount.Name = "lblMaxCount"
        Me.lblMaxCount.Size = New System.Drawing.Size(266, 21)
        Me.lblMaxCount.TabIndex = 1095
        Me.lblMaxCount.Text = "0"
        Me.lblMaxCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(1710, 951)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 21)
        Me.Label16.TabIndex = 1094
        Me.Label16.Text = "件表示"
        '
        'lblStaCount
        '
        Me.lblStaCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStaCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStaCount.Location = New System.Drawing.Point(1560, 951)
        Me.lblStaCount.Name = "lblStaCount"
        Me.lblStaCount.Size = New System.Drawing.Size(56, 21)
        Me.lblStaCount.TabIndex = 1093
        Me.lblStaCount.Text = "0"
        Me.lblStaCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(1513, 951)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 21)
        Me.Label14.TabIndex = 1092
        Me.Label14.Text = "件中"
        '
        'lblEndCount
        '
        Me.lblEndCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEndCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEndCount.Location = New System.Drawing.Point(1661, 951)
        Me.lblEndCount.Name = "lblEndCount"
        Me.lblEndCount.Size = New System.Drawing.Size(56, 21)
        Me.lblEndCount.TabIndex = 1096
        Me.lblEndCount.Text = "0"
        Me.lblEndCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(1622, 951)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 21)
        Me.Label5.TabIndex = 1097
        Me.Label5.Text = "から"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(271, 951)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 21)
        Me.Label2.TabIndex = 1098
        Me.Label2.Text = "ページ"
        '
        'pnlPrintStatus
        '
        Me.pnlPrintStatus.Count = 0
        Me.pnlPrintStatus.CountVisible = True
        Me.pnlPrintStatus.Location = New System.Drawing.Point(30, 777)
        Me.pnlPrintStatus.MaxCount = 0
        Me.pnlPrintStatus.Name = "pnlPrintStatus"
        Me.pnlPrintStatus.Size = New System.Drawing.Size(154, 195)
        Me.pnlPrintStatus.TabIndex = 1099
        Me.pnlPrintStatus.Title = "処理中…しばらくお待ちください。"
        '
        'btnCSMAST01
        '
        Me.btnCSMAST01.BackColor = System.Drawing.Color.SeaGreen
        Me.btnCSMAST01.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCSMAST01.ForeColor = System.Drawing.Color.White
        Me.btnCSMAST01.Location = New System.Drawing.Point(778, 0)
        Me.btnCSMAST01.Name = "btnCSMAST01"
        Me.btnCSMAST01.Size = New System.Drawing.Size(159, 102)
        Me.btnCSMAST01.TabIndex = 1100
        Me.btnCSMAST01.TabStop = False
        Me.btnCSMAST01.Text = "顧客情報"
        Me.btnCSMAST01.UseVisualStyleBackColor = False
        '
        'rbKB3
        '
        Me.rbKB3.AutoSize = True
        Me.rbKB3.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbKB3.Location = New System.Drawing.Point(312, 34)
        Me.rbKB3.Name = "rbKB3"
        Me.rbKB3.Size = New System.Drawing.Size(192, 31)
        Me.rbKB3.TabIndex = 4
        Me.rbKB3.Text = "簡易型入金機"
        Me.rbKB3.UseVisualStyleBackColor = True
        '
        'frmPRINT08
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.btnCSMAST01)
        Me.Controls.Add(Me.pnlPrintStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblEndCount)
        Me.Controls.Add(Me.lblMaxCount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblStaCount)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMaxPage)
        Me.Controls.Add(Me.lblPage)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnFIND)
        Me.Controls.Add(Me.dgvNKNTRN)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.plDate1)
        Me.Name = "frmPRINT08"
        Me.ShowInTaskbar = False
        Me.Text = "入金履歴"
        Me.Controls.SetChildIndex(Me.plDate1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.dgvNKNTRN, 0)
        Me.Controls.SetChildIndex(Me.btnFIND, 0)
        Me.Controls.SetChildIndex(Me.btnPrev, 0)
        Me.Controls.SetChildIndex(Me.btnNext, 0)
        Me.Controls.SetChildIndex(Me.lblPage, 0)
        Me.Controls.SetChildIndex(Me.lblMaxPage, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.lblStaCount, 0)
        Me.Controls.SetChildIndex(Me.Label16, 0)
        Me.Controls.SetChildIndex(Me.lblMaxCount, 0)
        Me.Controls.SetChildIndex(Me.lblEndCount, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.pnlPrintStatus, 0)
        Me.Controls.SetChildIndex(Me.btnCSMAST01, 0)
        Me.plDate1.ResumeLayout(False)
        Me.plDate1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvNKNTRN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents plDate1 As System.Windows.Forms.Panel
    Friend WithEvents dtpEndSEATDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTerm As System.Windows.Forms.ComboBox
    Friend WithEvents dtpStaSEATDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbKB4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbKB1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbKB_ALL As System.Windows.Forms.RadioButton
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents rbKB2 As System.Windows.Forms.RadioButton
    Friend WithEvents dgvNKNTRN As BaseControl.CustomGridView
    Friend WithEvents btnFIND As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblPage As System.Windows.Forms.Label
    Friend WithEvents lblMaxPage As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMaxCount As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblStaCount As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblEndCount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlPrintStatus As BaseControl.StatusPanel
    Friend WithEvents btnCSMAST01 As System.Windows.Forms.Button
    Friend WithEvents ETPKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ENTCNT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents POINT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents rbKB3 As System.Windows.Forms.RadioButton

End Class
