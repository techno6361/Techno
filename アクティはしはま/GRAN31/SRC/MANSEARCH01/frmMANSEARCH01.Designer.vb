<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMANSEARCH01
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
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.dgvMANMST = New BaseControl.CustomGridView()
        Me.SCLMANNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SCLKBN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANNM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANKN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANSEX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANZIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANADDRESS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANTELNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MANKTELNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtMANKTELNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtMANTELNO = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMANADDRESS = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbMANSEX = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMANKN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMANNM = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEndSCLMANNO = New System.Windows.Forms.TextBox()
        Me.txtStaSCLMANNO = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkSCLKBN0 = New System.Windows.Forms.CheckBox()
        Me.chkSCLKBN1 = New System.Windows.Forms.CheckBox()
        Me.chkSCLKBN8 = New System.Windows.Forms.CheckBox()
        Me.chkSCLKBN9 = New System.Windows.Forms.CheckBox()
        CType(Me.dgvMANMST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMsg
        '
        Me.lblMsg.BackColor = System.Drawing.Color.Transparent
        Me.lblMsg.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(12, 113)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(830, 46)
        Me.lblMsg.TabIndex = 163
        Me.lblMsg.Text = "※スクール生データが見つかりません。※"
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMsg.Visible = False
        '
        'dgvMANMST
        '
        Me.dgvMANMST.AllowUserToAddRows = False
        Me.dgvMANMST.AllowUserToDeleteRows = False
        Me.dgvMANMST.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvMANMST.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMANMST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMANMST.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvMANMST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvMANMST.ColumnHeadersHeight = 50
        Me.dgvMANMST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvMANMST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SCLMANNO, Me.SCLKBN, Me.MANNM, Me.MANKN, Me.MANSEX, Me.MANZIP, Me.MANADDRESS, Me.MANTELNO, Me.MANKTELNO})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvMANMST.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvMANMST.EnableHeadersVisualStyles = False
        Me.dgvMANMST.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvMANMST.Location = New System.Drawing.Point(12, 161)
        Me.dgvMANMST.MultiSelect = False
        Me.dgvMANMST.Name = "dgvMANMST"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvMANMST.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvMANMST.RowHeadersVisible = False
        Me.dgvMANMST.RowHeadersWidth = 61
        Me.dgvMANMST.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvMANMST.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvMANMST.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvMANMST.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvMANMST.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvMANMST.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvMANMST.RowTemplate.Height = 40
        Me.dgvMANMST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMANMST.Size = New System.Drawing.Size(1082, 389)
        Me.dgvMANMST.TabIndex = 0
        '
        'SCLMANNO
        '
        Me.SCLMANNO.DataPropertyName = "SCLMANNO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.SCLMANNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.SCLMANNO.Frozen = True
        Me.SCLMANNO.HeaderText = "スクール生番号"
        Me.SCLMANNO.Name = "SCLMANNO"
        Me.SCLMANNO.ReadOnly = True
        Me.SCLMANNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SCLMANNO.Width = 180
        '
        'SCLKBN
        '
        Me.SCLKBN.DataPropertyName = "SCLKBN"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.SCLKBN.DefaultCellStyle = DataGridViewCellStyle4
        Me.SCLKBN.HeaderText = "種別"
        Me.SCLKBN.Name = "SCLKBN"
        Me.SCLKBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SCLKBN.Width = 120
        '
        'MANNM
        '
        Me.MANNM.DataPropertyName = "MANNM"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.MANNM.DefaultCellStyle = DataGridViewCellStyle5
        Me.MANNM.HeaderText = "氏名"
        Me.MANNM.MaxInputLength = 10
        Me.MANNM.Name = "MANNM"
        Me.MANNM.ReadOnly = True
        Me.MANNM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MANNM.Width = 250
        '
        'MANKN
        '
        Me.MANKN.DataPropertyName = "MANKN"
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.MANKN.DefaultCellStyle = DataGridViewCellStyle6
        Me.MANKN.HeaderText = "氏名ｶﾅ"
        Me.MANKN.Name = "MANKN"
        Me.MANKN.ReadOnly = True
        Me.MANKN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MANKN.Width = 200
        '
        'MANSEX
        '
        Me.MANSEX.DataPropertyName = "MANSEX"
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.MANSEX.DefaultCellStyle = DataGridViewCellStyle7
        Me.MANSEX.HeaderText = "性別"
        Me.MANSEX.Name = "MANSEX"
        Me.MANSEX.ReadOnly = True
        Me.MANSEX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MANZIP
        '
        Me.MANZIP.DataPropertyName = "MANZIP"
        DataGridViewCellStyle8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.MANZIP.DefaultCellStyle = DataGridViewCellStyle8
        Me.MANZIP.HeaderText = "郵便番号"
        Me.MANZIP.Name = "MANZIP"
        Me.MANZIP.ReadOnly = True
        Me.MANZIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MANZIP.Width = 120
        '
        'MANADDRESS
        '
        Me.MANADDRESS.DataPropertyName = "MANADDRESS"
        DataGridViewCellStyle9.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.MANADDRESS.DefaultCellStyle = DataGridViewCellStyle9
        Me.MANADDRESS.HeaderText = "住所"
        Me.MANADDRESS.Name = "MANADDRESS"
        Me.MANADDRESS.ReadOnly = True
        Me.MANADDRESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MANADDRESS.Width = 400
        '
        'MANTELNO
        '
        Me.MANTELNO.DataPropertyName = "MANTELNO"
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle10.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.MANTELNO.DefaultCellStyle = DataGridViewCellStyle10
        Me.MANTELNO.HeaderText = "電話番号"
        Me.MANTELNO.Name = "MANTELNO"
        Me.MANTELNO.ReadOnly = True
        Me.MANTELNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MANTELNO.Width = 200
        '
        'MANKTELNO
        '
        Me.MANKTELNO.DataPropertyName = "MANKTELNO"
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        Me.MANKTELNO.DefaultCellStyle = DataGridViewCellStyle11
        Me.MANKTELNO.HeaderText = "携帯電話番号"
        Me.MANKTELNO.Name = "MANKTELNO"
        Me.MANKTELNO.ReadOnly = True
        Me.MANKTELNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MANKTELNO.Width = 200
        '
        'txtMANKTELNO
        '
        Me.txtMANKTELNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMANKTELNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtMANKTELNO.Location = New System.Drawing.Point(648, 792)
        Me.txtMANKTELNO.MaxLength = 15
        Me.txtMANKTELNO.Name = "txtMANKTELNO"
        Me.txtMANKTELNO.Size = New System.Drawing.Size(227, 34)
        Me.txtMANKTELNO.TabIndex = 8
        Me.txtMANKTELNO.Text = "999999999999999"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(454, 792)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(192, 34)
        Me.Label12.TabIndex = 277
        Me.Label12.Text = "携帯電話番号"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMANTELNO
        '
        Me.txtMANTELNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMANTELNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtMANTELNO.Location = New System.Drawing.Point(211, 792)
        Me.txtMANTELNO.MaxLength = 15
        Me.txtMANTELNO.Name = "txtMANTELNO"
        Me.txtMANTELNO.Size = New System.Drawing.Size(227, 34)
        Me.txtMANTELNO.TabIndex = 7
        Me.txtMANTELNO.Text = "999999999999999"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkGreen
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(17, 792)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(192, 34)
        Me.Label10.TabIndex = 276
        Me.Label10.Text = "電話番号"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMANADDRESS
        '
        Me.txtMANADDRESS.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMANADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtMANADDRESS.Location = New System.Drawing.Point(211, 748)
        Me.txtMANADDRESS.MaxLength = 25
        Me.txtMANADDRESS.Name = "txtMANADDRESS"
        Me.txtMANADDRESS.Size = New System.Drawing.Size(564, 34)
        Me.txtMANADDRESS.TabIndex = 6
        Me.txtMANADDRESS.Text = "あああああああああああああああああああああああああ"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.DarkGreen
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(17, 748)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(192, 34)
        Me.Label8.TabIndex = 275
        Me.Label8.Text = "住所"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMANSEX
        '
        Me.cmbMANSEX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMANSEX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMANSEX.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMANSEX.FormattingEnabled = True
        Me.cmbMANSEX.Items.AddRange(New Object() {"", "男", "女", "不明"})
        Me.cmbMANSEX.Location = New System.Drawing.Point(211, 704)
        Me.cmbMANSEX.Name = "cmbMANSEX"
        Me.cmbMANSEX.Size = New System.Drawing.Size(96, 35)
        Me.cmbMANSEX.TabIndex = 5
        Me.cmbMANSEX.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(17, 704)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(192, 34)
        Me.Label5.TabIndex = 274
        Me.Label5.Text = "性別"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMANKN
        '
        Me.txtMANKN.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMANKN.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtMANKN.Location = New System.Drawing.Point(673, 660)
        Me.txtMANKN.MaxLength = 10
        Me.txtMANKN.Name = "txtMANKN"
        Me.txtMANKN.Size = New System.Drawing.Size(184, 34)
        Me.txtMANKN.TabIndex = 4
        Me.txtMANKN.Text = "ｱｱｱｱｱｱｱｱｱｱ"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(479, 660)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 34)
        Me.Label3.TabIndex = 273
        Me.Label3.Text = "氏名ｶﾅ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMANNM
        '
        Me.txtMANNM.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMANNM.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtMANNM.Location = New System.Drawing.Point(211, 660)
        Me.txtMANNM.MaxLength = 10
        Me.txtMANNM.Name = "txtMANNM"
        Me.txtMANNM.Size = New System.Drawing.Size(246, 34)
        Me.txtMANNM.TabIndex = 3
        Me.txtMANNM.Text = "ああああああああああ"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkGreen
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(17, 660)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(192, 34)
        Me.Label4.TabIndex = 272
        Me.Label4.Text = "氏名"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(312, 571)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 34)
        Me.Label1.TabIndex = 271
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEndSCLMANNO
        '
        Me.txtEndSCLMANNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndSCLMANNO.Location = New System.Drawing.Point(354, 571)
        Me.txtEndSCLMANNO.MaxLength = 6
        Me.txtEndSCLMANNO.Name = "txtEndSCLMANNO"
        Me.txtEndSCLMANNO.Size = New System.Drawing.Size(98, 34)
        Me.txtEndSCLMANNO.TabIndex = 2
        Me.txtEndSCLMANNO.Text = "999999"
        '
        'txtStaSCLMANNO
        '
        Me.txtStaSCLMANNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaSCLMANNO.Location = New System.Drawing.Point(211, 572)
        Me.txtStaSCLMANNO.MaxLength = 6
        Me.txtStaSCLMANNO.Name = "txtStaSCLMANNO"
        Me.txtStaSCLMANNO.Size = New System.Drawing.Size(98, 34)
        Me.txtStaSCLMANNO.TabIndex = 1
        Me.txtStaSCLMANNO.Text = "999999"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(17, 572)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(192, 34)
        Me.Label22.TabIndex = 270
        Me.Label22.Text = "スクール生番号"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(17, 616)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 34)
        Me.Label2.TabIndex = 278
        Me.Label2.Text = "種別"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkSCLKBN0
        '
        Me.chkSCLKBN0.AutoSize = True
        Me.chkSCLKBN0.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSCLKBN0.Location = New System.Drawing.Point(215, 619)
        Me.chkSCLKBN0.Name = "chkSCLKBN0"
        Me.chkSCLKBN0.Size = New System.Drawing.Size(101, 28)
        Me.chkSCLKBN0.TabIndex = 279
        Me.chkSCLKBN0.TabStop = False
        Me.chkSCLKBN0.Text = "本科生"
        Me.chkSCLKBN0.UseVisualStyleBackColor = True
        '
        'chkSCLKBN1
        '
        Me.chkSCLKBN1.AutoSize = True
        Me.chkSCLKBN1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSCLKBN1.Location = New System.Drawing.Point(316, 619)
        Me.chkSCLKBN1.Name = "chkSCLKBN1"
        Me.chkSCLKBN1.Size = New System.Drawing.Size(77, 28)
        Me.chkSCLKBN1.TabIndex = 280
        Me.chkSCLKBN1.TabStop = False
        Me.chkSCLKBN1.Text = "体験"
        Me.chkSCLKBN1.UseVisualStyleBackColor = True
        '
        'chkSCLKBN8
        '
        Me.chkSCLKBN8.AutoSize = True
        Me.chkSCLKBN8.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSCLKBN8.Location = New System.Drawing.Point(395, 619)
        Me.chkSCLKBN8.Name = "chkSCLKBN8"
        Me.chkSCLKBN8.Size = New System.Drawing.Size(77, 28)
        Me.chkSCLKBN8.TabIndex = 281
        Me.chkSCLKBN8.TabStop = False
        Me.chkSCLKBN8.Text = "休会"
        Me.chkSCLKBN8.UseVisualStyleBackColor = True
        '
        'chkSCLKBN9
        '
        Me.chkSCLKBN9.AutoSize = True
        Me.chkSCLKBN9.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSCLKBN9.Location = New System.Drawing.Point(478, 619)
        Me.chkSCLKBN9.Name = "chkSCLKBN9"
        Me.chkSCLKBN9.Size = New System.Drawing.Size(77, 28)
        Me.chkSCLKBN9.TabIndex = 282
        Me.chkSCLKBN9.TabStop = False
        Me.chkSCLKBN9.Text = "退会"
        Me.chkSCLKBN9.UseVisualStyleBackColor = True
        '
        'frmMANSEARCH01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1106, 962)
        Me.Controls.Add(Me.chkSCLKBN9)
        Me.Controls.Add(Me.chkSCLKBN8)
        Me.Controls.Add(Me.chkSCLKBN1)
        Me.Controls.Add(Me.chkSCLKBN0)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMANKTELNO)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtMANTELNO)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtMANADDRESS)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbMANSEX)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtMANKN)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMANNM)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtEndSCLMANNO)
        Me.Controls.Add(Me.txtStaSCLMANNO)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.dgvMANMST)
        Me.MaximumSize = New System.Drawing.Size(1122, 1000)
        Me.MinimumSize = New System.Drawing.Size(1122, 1000)
        Me.Name = "frmMANSEARCH01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvMANMST, 0)
        Me.Controls.SetChildIndex(Me.lblMsg, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.txtStaSCLMANNO, 0)
        Me.Controls.SetChildIndex(Me.txtEndSCLMANNO, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtMANNM, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtMANKN, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.cmbMANSEX, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtMANADDRESS, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.txtMANTELNO, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.txtMANKTELNO, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.chkSCLKBN0, 0)
        Me.Controls.SetChildIndex(Me.chkSCLKBN1, 0)
        Me.Controls.SetChildIndex(Me.chkSCLKBN8, 0)
        Me.Controls.SetChildIndex(Me.chkSCLKBN9, 0)
        CType(Me.dgvMANMST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents dgvMANMST As BaseControl.CustomGridView
    Friend WithEvents txtMANKTELNO As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMANTELNO As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMANADDRESS As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbMANSEX As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMANKN As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMANNM As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEndSCLMANNO As System.Windows.Forms.TextBox
    Friend WithEvents txtStaSCLMANNO As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkSCLKBN0 As System.Windows.Forms.CheckBox
    Friend WithEvents chkSCLKBN1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkSCLKBN8 As System.Windows.Forms.CheckBox
    Friend WithEvents chkSCLKBN9 As System.Windows.Forms.CheckBox
    Friend WithEvents SCLMANNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SCLKBN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANNM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANKN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANSEX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANZIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANADDRESS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANTELNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MANKTELNO As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
