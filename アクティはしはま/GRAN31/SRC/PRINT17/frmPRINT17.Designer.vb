﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINT17
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
        Me.dgvRECARDTRN = New BaseControl.CustomGridView()
        Me.plDate1 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtStaNCSNO = New BaseControl.CustomTextBoxNum()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoDATKB19 = New System.Windows.Forms.RadioButton()
        Me.rdoDATKB9 = New System.Windows.Forms.RadioButton()
        Me.rdoDATKB1 = New System.Windows.Forms.RadioButton()
        Me.dtpEndSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.cmbTerm = New System.Windows.Forms.ComboBox()
        Me.dtpStaSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnFIND = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblEndCount = New System.Windows.Forms.Label()
        Me.lblMaxCount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblStaCount = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMaxPage = New System.Windows.Forms.Label()
        Me.lblPage = New System.Windows.Forms.Label()
        Me.pnlPrintStatus = New BaseControl.StatusPanel()
        Me.DATKB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ETPKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STFNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvRECARDTRN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plDate1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvRECARDTRN
        '
        Me.dgvRECARDTRN.AllowUserToAddRows = False
        Me.dgvRECARDTRN.AllowUserToDeleteRows = False
        Me.dgvRECARDTRN.AllowUserToResizeColumns = False
        Me.dgvRECARDTRN.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.NullValue = Nothing
        Me.dgvRECARDTRN.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRECARDTRN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvRECARDTRN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRECARDTRN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvRECARDTRN.ColumnHeadersHeight = 50
        Me.dgvRECARDTRN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvRECARDTRN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DATKB, Me.ETPKBN, Me.Column1, Me.Column7, Me.Column3, Me.CKBNAME, Me.CCSNAME, Me.Column8, Me.Column2, Me.STFNAME})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRECARDTRN.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvRECARDTRN.EnableHeadersVisualStyles = False
        Me.dgvRECARDTRN.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dgvRECARDTRN.Location = New System.Drawing.Point(118, 279)
        Me.dgvRECARDTRN.MultiSelect = False
        Me.dgvRECARDTRN.Name = "dgvRECARDTRN"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRECARDTRN.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvRECARDTRN.RowHeadersVisible = False
        Me.dgvRECARDTRN.RowHeadersWidth = 61
        Me.dgvRECARDTRN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvRECARDTRN.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvRECARDTRN.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvRECARDTRN.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvRECARDTRN.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvRECARDTRN.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvRECARDTRN.RowTemplate.Height = 40
        Me.dgvRECARDTRN.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvRECARDTRN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRECARDTRN.Size = New System.Drawing.Size(1718, 691)
        Me.dgvRECARDTRN.TabIndex = 1103
        '
        'plDate1
        '
        Me.plDate1.BackColor = System.Drawing.Color.Moccasin
        Me.plDate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plDate1.Controls.Add(Me.Label22)
        Me.plDate1.Controls.Add(Me.txtStaNCSNO)
        Me.plDate1.Controls.Add(Me.Panel1)
        Me.plDate1.Controls.Add(Me.dtpEndSEATDT)
        Me.plDate1.Controls.Add(Me.cmbTerm)
        Me.plDate1.Controls.Add(Me.dtpStaSEATDT)
        Me.plDate1.Controls.Add(Me.Label49)
        Me.plDate1.Controls.Add(Me.Label4)
        Me.plDate1.Controls.Add(Me.Label3)
        Me.plDate1.Location = New System.Drawing.Point(25, 136)
        Me.plDate1.Name = "plDate1"
        Me.plDate1.Size = New System.Drawing.Size(1212, 137)
        Me.plDate1.TabIndex = 1101
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(531, 78)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(212, 35)
        Me.Label22.TabIndex = 1128
        Me.Label22.Text = "顧客番号"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStaNCSNO
        '
        Me.txtStaNCSNO.BackColor = System.Drawing.Color.White
        Me.txtStaNCSNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStaNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaNCSNO.Format = ""
        Me.txtStaNCSNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtStaNCSNO.Location = New System.Drawing.Point(744, 79)
        Me.txtStaNCSNO.MaxLength = 8
        Me.txtStaNCSNO.Name = "txtStaNCSNO"
        Me.txtStaNCSNO.Size = New System.Drawing.Size(177, 34)
        Me.txtStaNCSNO.TabIndex = 1129
        Me.txtStaNCSNO.Tag = ""
        Me.txtStaNCSNO.Text = "99999999"
        Me.txtStaNCSNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdoDATKB19)
        Me.Panel1.Controls.Add(Me.rdoDATKB9)
        Me.Panel1.Controls.Add(Me.rdoDATKB1)
        Me.Panel1.Location = New System.Drawing.Point(531, 21)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(427, 38)
        Me.Panel1.TabIndex = 1127
        '
        'rdoDATKB19
        '
        Me.rdoDATKB19.AutoSize = True
        Me.rdoDATKB19.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoDATKB19.Location = New System.Drawing.Point(265, 6)
        Me.rdoDATKB19.Name = "rdoDATKB19"
        Me.rdoDATKB19.Size = New System.Drawing.Size(156, 28)
        Me.rdoDATKB19.TabIndex = 1016
        Me.rdoDATKB19.Text = "取消を含める"
        Me.rdoDATKB19.UseVisualStyleBackColor = True
        '
        'rdoDATKB9
        '
        Me.rdoDATKB9.AutoSize = True
        Me.rdoDATKB9.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoDATKB9.Location = New System.Drawing.Point(144, 6)
        Me.rdoDATKB9.Name = "rdoDATKB9"
        Me.rdoDATKB9.Size = New System.Drawing.Size(117, 28)
        Me.rdoDATKB9.TabIndex = 1015
        Me.rdoDATKB9.Text = "取消のみ"
        Me.rdoDATKB9.UseVisualStyleBackColor = True
        '
        'rdoDATKB1
        '
        Me.rdoDATKB1.AutoSize = True
        Me.rdoDATKB1.Checked = True
        Me.rdoDATKB1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoDATKB1.Location = New System.Drawing.Point(8, 6)
        Me.rdoDATKB1.Name = "rdoDATKB1"
        Me.rdoDATKB1.Size = New System.Drawing.Size(130, 28)
        Me.rdoDATKB1.TabIndex = 1014
        Me.rdoDATKB1.TabStop = True
        Me.rdoDATKB1.Text = "取消を省く"
        Me.rdoDATKB1.UseVisualStyleBackColor = True
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
        Me.Label3.Text = "営業日"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Orange
        Me.btnNext.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNext.Location = New System.Drawing.Point(1602, 135)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(116, 84)
        Me.btnNext.TabIndex = 1106
        Me.btnNext.TabStop = False
        Me.btnNext.Text = "次へ"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnPrev
        '
        Me.btnPrev.BackColor = System.Drawing.Color.Orange
        Me.btnPrev.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrev.Location = New System.Drawing.Point(1480, 135)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(116, 84)
        Me.btnPrev.TabIndex = 1105
        Me.btnPrev.TabStop = False
        Me.btnPrev.Text = "前へ"
        Me.btnPrev.UseVisualStyleBackColor = False
        '
        'btnFIND
        '
        Me.btnFIND.BackColor = System.Drawing.Color.Orange
        Me.btnFIND.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFIND.Location = New System.Drawing.Point(1259, 136)
        Me.btnFIND.Name = "btnFIND"
        Me.btnFIND.Size = New System.Drawing.Size(215, 83)
        Me.btnFIND.TabIndex = 1104
        Me.btnFIND.TabStop = False
        Me.btnFIND.Text = "検索"
        Me.btnFIND.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(1672, 973)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 21)
        Me.Label5.TabIndex = 1115
        Me.Label5.Text = "から"
        '
        'lblEndCount
        '
        Me.lblEndCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEndCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEndCount.Location = New System.Drawing.Point(1711, 973)
        Me.lblEndCount.Name = "lblEndCount"
        Me.lblEndCount.Size = New System.Drawing.Size(56, 21)
        Me.lblEndCount.TabIndex = 1114
        Me.lblEndCount.Text = "0"
        Me.lblEndCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaxCount
        '
        Me.lblMaxCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxCount.Location = New System.Drawing.Point(1295, 973)
        Me.lblMaxCount.Name = "lblMaxCount"
        Me.lblMaxCount.Size = New System.Drawing.Size(266, 21)
        Me.lblMaxCount.TabIndex = 1113
        Me.lblMaxCount.Text = "0"
        Me.lblMaxCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(1760, 973)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 21)
        Me.Label16.TabIndex = 1112
        Me.Label16.Text = "件表示"
        '
        'lblStaCount
        '
        Me.lblStaCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStaCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStaCount.Location = New System.Drawing.Point(1610, 973)
        Me.lblStaCount.Name = "lblStaCount"
        Me.lblStaCount.Size = New System.Drawing.Size(56, 21)
        Me.lblStaCount.TabIndex = 1111
        Me.lblStaCount.Text = "0"
        Me.lblStaCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(1563, 973)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 21)
        Me.Label14.TabIndex = 1110
        Me.Label14.Text = "件中"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(339, 973)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 21)
        Me.Label2.TabIndex = 1119
        Me.Label2.Text = "ページ"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(252, 973)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 21)
        Me.Label1.TabIndex = 1118
        Me.Label1.Text = "/"
        '
        'lblMaxPage
        '
        Me.lblMaxPage.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxPage.Location = New System.Drawing.Point(274, 973)
        Me.lblMaxPage.Name = "lblMaxPage"
        Me.lblMaxPage.Size = New System.Drawing.Size(59, 21)
        Me.lblMaxPage.TabIndex = 1117
        Me.lblMaxPage.Text = "0001"
        Me.lblMaxPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPage
        '
        Me.lblPage.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPage.Location = New System.Drawing.Point(189, 973)
        Me.lblPage.Name = "lblPage"
        Me.lblPage.Size = New System.Drawing.Size(67, 21)
        Me.lblPage.TabIndex = 1116
        Me.lblPage.Text = "0001"
        Me.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPrintStatus
        '
        Me.pnlPrintStatus.Count = 0
        Me.pnlPrintStatus.CountVisible = True
        Me.pnlPrintStatus.Location = New System.Drawing.Point(0, 820)
        Me.pnlPrintStatus.MaxCount = 0
        Me.pnlPrintStatus.Name = "pnlPrintStatus"
        Me.pnlPrintStatus.Size = New System.Drawing.Size(132, 195)
        Me.pnlPrintStatus.TabIndex = 1120
        Me.pnlPrintStatus.Title = "処理中…しばらくお待ちください。"
        '
        'DATKB
        '
        Me.DATKB.DataPropertyName = "DATKB"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DATKB.DefaultCellStyle = DataGridViewCellStyle3
        Me.DATKB.HeaderText = ""
        Me.DATKB.Name = "DATKB"
        Me.DATKB.ReadOnly = True
        Me.DATKB.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DATKB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DATKB.Width = 80
        '
        'ETPKBN
        '
        Me.ETPKBN.DataPropertyName = "INSDTM1"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ETPKBN.DefaultCellStyle = DataGridViewCellStyle4
        Me.ETPKBN.HeaderText = "営業日"
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
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column1.HeaderText = "時間"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Width = 125
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "HINNMA"
        Me.Column7.HeaderText = "コース精算内容"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column7.Width = 350
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "MANNO"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column3.HeaderText = "顧客番号"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 150
        '
        'CKBNAME
        '
        Me.CKBNAME.DataPropertyName = "CKBNAME"
        Me.CKBNAME.HeaderText = "顧客種別"
        Me.CKBNAME.Name = "CKBNAME"
        Me.CKBNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CKBNAME.Width = 150
        '
        'CCSNAME
        '
        Me.CCSNAME.DataPropertyName = "CCSNAME"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.Format = "N0"
        Me.CCSNAME.DefaultCellStyle = DataGridViewCellStyle7
        Me.CCSNAME.HeaderText = "氏名"
        Me.CCSNAME.Name = "CCSNAME"
        Me.CCSNAME.ReadOnly = True
        Me.CCSNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.CCSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CCSNAME.Width = 230
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "KOSUKN"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column8.HeaderText = "料金"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column8.Width = 125
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "POINT"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column2.HeaderText = "ポイント"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 125
        '
        'STFNAME
        '
        Me.STFNAME.DataPropertyName = "STFNAME"
        Me.STFNAME.HeaderText = "担当者"
        Me.STFNAME.Name = "STFNAME"
        Me.STFNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.STFNAME.Width = 230
        '
        'frmPRINT17
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.pnlPrintStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMaxPage)
        Me.Controls.Add(Me.lblPage)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblEndCount)
        Me.Controls.Add(Me.lblMaxCount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblStaCount)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnFIND)
        Me.Controls.Add(Me.dgvRECARDTRN)
        Me.Controls.Add(Me.plDate1)
        Me.Name = "frmPRINT17"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.plDate1, 0)
        Me.Controls.SetChildIndex(Me.dgvRECARDTRN, 0)
        Me.Controls.SetChildIndex(Me.btnFIND, 0)
        Me.Controls.SetChildIndex(Me.btnPrev, 0)
        Me.Controls.SetChildIndex(Me.btnNext, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.lblStaCount, 0)
        Me.Controls.SetChildIndex(Me.Label16, 0)
        Me.Controls.SetChildIndex(Me.lblMaxCount, 0)
        Me.Controls.SetChildIndex(Me.lblEndCount, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.lblPage, 0)
        Me.Controls.SetChildIndex(Me.lblMaxPage, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.pnlPrintStatus, 0)
        CType(Me.dgvRECARDTRN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plDate1.ResumeLayout(False)
        Me.plDate1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvRECARDTRN As BaseControl.CustomGridView
    Friend WithEvents plDate1 As System.Windows.Forms.Panel
    Friend WithEvents dtpEndSEATDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTerm As System.Windows.Forms.ComboBox
    Friend WithEvents dtpStaSEATDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnFIND As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblEndCount As System.Windows.Forms.Label
    Friend WithEvents lblMaxCount As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblStaCount As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMaxPage As System.Windows.Forms.Label
    Friend WithEvents lblPage As System.Windows.Forms.Label
    Friend WithEvents pnlPrintStatus As BaseControl.StatusPanel
    Friend WithEvents rdoDATKB19 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDATKB9 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDATKB1 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtStaNCSNO As BaseControl.CustomTextBoxNum
    Friend WithEvents DATKB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ETPKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STFNAME As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
