<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFEEHIST01
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbKBMAST = New System.Windows.Forms.ComboBox()
        Me.txtStaNCSNO = New BaseControl.CustomTextBoxNum()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpEndSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.cmbTerm = New System.Windows.Forms.ComboBox()
        Me.dtpStaSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dgvFEEHIST = New BaseControl.CustomGridView()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.colNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUDNDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUDNTIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMANNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCCSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCKBNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPAYKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSTFNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFEE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCANCEL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.colUDNNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDRAKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPREMKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvFEEHIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Moccasin
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cmbKBMAST)
        Me.Panel2.Controls.Add(Me.txtStaNCSNO)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.dtpEndSEATDT)
        Me.Panel2.Controls.Add(Me.cmbTerm)
        Me.Panel2.Controls.Add(Me.dtpStaSEATDT)
        Me.Panel2.Controls.Add(Me.Label49)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.btnSearch)
        Me.Panel2.Location = New System.Drawing.Point(22, 117)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(639, 175)
        Me.Panel2.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 35)
        Me.Label2.TabIndex = 1083
        Me.Label2.Text = "顧客種別"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbKBMAST
        '
        Me.cmbKBMAST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKBMAST.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbKBMAST.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKBMAST.FormattingEnabled = True
        Me.cmbKBMAST.Location = New System.Drawing.Point(107, 125)
        Me.cmbKBMAST.Name = "cmbKBMAST"
        Me.cmbKBMAST.Size = New System.Drawing.Size(395, 35)
        Me.cmbKBMAST.TabIndex = 1082
        Me.cmbKBMAST.TabStop = False
        '
        'txtStaNCSNO
        '
        Me.txtStaNCSNO.BackColor = System.Drawing.Color.White
        Me.txtStaNCSNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStaNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaNCSNO.Format = ""
        Me.txtStaNCSNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtStaNCSNO.Location = New System.Drawing.Point(107, 84)
        Me.txtStaNCSNO.MaxLength = 8
        Me.txtStaNCSNO.Name = "txtStaNCSNO"
        Me.txtStaNCSNO.Size = New System.Drawing.Size(177, 34)
        Me.txtStaNCSNO.TabIndex = 1078
        Me.txtStaNCSNO.Tag = ""
        Me.txtStaNCSNO.Text = "99999999"
        Me.txtStaNCSNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 35)
        Me.Label1.TabIndex = 1014
        Me.Label1.Text = "顧客番号"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.btnSearch.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(508, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(126, 158)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Tag = "　"
        Me.btnSearch.Text = "検索"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'dgvFEEHIST
        '
        Me.dgvFEEHIST.AllowUserToAddRows = False
        Me.dgvFEEHIST.AllowUserToDeleteRows = False
        Me.dgvFEEHIST.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvFEEHIST.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvFEEHIST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvFEEHIST.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFEEHIST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvFEEHIST.ColumnHeadersHeight = 50
        Me.dgvFEEHIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvFEEHIST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNO, Me.colUDNDT, Me.colUDNTIME, Me.colMANNO, Me.colCCSNAME, Me.colCKBNAME, Me.colCPAYKBN, Me.colSTFNAME, Me.colFEE, Me.colCANCEL, Me.colUDNNO, Me.colDRAKBN, Me.colPREMKN})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFEEHIST.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvFEEHIST.EnableHeadersVisualStyles = False
        Me.dgvFEEHIST.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvFEEHIST.Location = New System.Drawing.Point(118, 372)
        Me.dgvFEEHIST.MultiSelect = False
        Me.dgvFEEHIST.Name = "dgvFEEHIST"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvFEEHIST.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvFEEHIST.RowHeadersVisible = False
        Me.dgvFEEHIST.RowHeadersWidth = 61
        Me.dgvFEEHIST.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvFEEHIST.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvFEEHIST.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvFEEHIST.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvFEEHIST.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvFEEHIST.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvFEEHIST.RowTemplate.Height = 40
        Me.dgvFEEHIST.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvFEEHIST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFEEHIST.Size = New System.Drawing.Size(1653, 572)
        Me.dgvFEEHIST.TabIndex = 1084
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnNext.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(1616, 302)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(155, 64)
        Me.btnNext.TabIndex = 1110
        Me.btnNext.Text = "次ページ"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1455, 302)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(155, 64)
        Me.btnBack.TabIndex = 1109
        Me.btnBack.Text = "前ページ"
        Me.btnBack.UseVisualStyleBackColor = False
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
        Me.colNO.Width = 80
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
        Me.colUDNDT.Width = 150
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
        Me.colUDNTIME.Width = 150
        '
        'colMANNO
        '
        Me.colMANNO.DataPropertyName = "MANNO"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colMANNO.DefaultCellStyle = DataGridViewCellStyle6
        Me.colMANNO.Frozen = True
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
        Me.colCCSNAME.Frozen = True
        Me.colCCSNAME.HeaderText = "氏名"
        Me.colCCSNAME.Name = "colCCSNAME"
        Me.colCCSNAME.ReadOnly = True
        Me.colCCSNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCCSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCCSNAME.Width = 300
        '
        'colCKBNAME
        '
        Me.colCKBNAME.DataPropertyName = "CKBNAME"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.Format = "#,0"
        Me.colCKBNAME.DefaultCellStyle = DataGridViewCellStyle7
        Me.colCKBNAME.Frozen = True
        Me.colCKBNAME.HeaderText = "顧客種別"
        Me.colCKBNAME.Name = "colCKBNAME"
        Me.colCKBNAME.ReadOnly = True
        Me.colCKBNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCKBNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCKBNAME.Width = 200
        '
        'colCPAYKBN
        '
        Me.colCPAYKBN.DataPropertyName = "CPAYKBN"
        Me.colCPAYKBN.Frozen = True
        Me.colCPAYKBN.HeaderText = "精算区分"
        Me.colCPAYKBN.Name = "colCPAYKBN"
        Me.colCPAYKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCPAYKBN.Width = 120
        '
        'colSTFNAME
        '
        Me.colSTFNAME.Frozen = True
        Me.colSTFNAME.HeaderText = "担当者"
        Me.colSTFNAME.Name = "colSTFNAME"
        Me.colSTFNAME.ReadOnly = True
        Me.colSTFNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colSTFNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSTFNAME.Width = 300
        '
        'colFEE
        '
        Me.colFEE.DataPropertyName = "FEE"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colFEE.DefaultCellStyle = DataGridViewCellStyle8
        Me.colFEE.HeaderText = "年会費"
        Me.colFEE.Name = "colFEE"
        Me.colFEE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colCANCEL
        '
        Me.colCANCEL.DataPropertyName = "CANCEL"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.Format = "#,0"
        Me.colCANCEL.DefaultCellStyle = DataGridViewCellStyle9
        Me.colCANCEL.HeaderText = ""
        Me.colCANCEL.Name = "colCANCEL"
        Me.colCANCEL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colUDNNO
        '
        Me.colUDNNO.DataPropertyName = "UDNNO"
        Me.colUDNNO.HeaderText = "伝票番号"
        Me.colUDNNO.Name = "colUDNNO"
        Me.colUDNNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUDNNO.Visible = False
        '
        'colDRAKBN
        '
        Me.colDRAKBN.HeaderText = "ドロア区分"
        Me.colDRAKBN.Name = "colDRAKBN"
        Me.colDRAKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDRAKBN.Visible = False
        '
        'colPREMKN
        '
        Me.colPREMKN.DataPropertyName = "PREMKN"
        Me.colPREMKN.HeaderText = "ﾌﾟﾚﾐｱﾑ"
        Me.colPREMKN.Name = "colPREMKN"
        Me.colPREMKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colPREMKN.Visible = False
        '
        'frmFEEHIST01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.dgvFEEHIST)
        Me.Name = "frmFEEHIST01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvFEEHIST, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.btnBack, 0)
        Me.Controls.SetChildIndex(Me.btnNext, 0)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvFEEHIST, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtStaNCSNO As BaseControl.CustomTextBoxNum
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbKBMAST As System.Windows.Forms.ComboBox
    Friend WithEvents dgvFEEHIST As BaseControl.CustomGridView
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents colNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUDNDT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUDNTIME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMANNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCCSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCKBNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPAYKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSTFNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFEE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCANCEL As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents colUDNNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDRAKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPREMKN As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
