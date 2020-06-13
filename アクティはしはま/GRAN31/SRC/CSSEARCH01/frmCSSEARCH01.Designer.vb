<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSSEARCH01
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
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.dgvCSMAST = New BaseControl.CustomGridView()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.txtStaNCSNO = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtEndNCSNO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNCSRANK = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCCSKANA = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCCSNAME = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbNSEX = New System.Windows.Forms.ComboBox()
        Me.txtCADDRESS = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDBIRTH = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkCard = New System.Windows.Forms.CheckBox()
        Me.txtStaDMEMBER = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEndDMEMBER = New System.Windows.Forms.TextBox()
        Me.txtCPOTABLENUM = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCTELEPHONE = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtEndMEMBERNO = New System.Windows.Forms.TextBox()
        Me.txtStaMEMBERNO = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkCard2 = New System.Windows.Forms.CheckBox()
        Me.NCSNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCSKANA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DBIRTH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NSEX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NZIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CADDRESS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CTELEPHONE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CPOTABLENUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DMEMBER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MEMBERNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvCSMAST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvCSMAST
        '
        Me.dgvCSMAST.AllowUserToAddRows = False
        Me.dgvCSMAST.AllowUserToDeleteRows = False
        Me.dgvCSMAST.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvCSMAST.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCSMAST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCSMAST.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCSMAST.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCSMAST.ColumnHeadersHeight = 50
        Me.dgvCSMAST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvCSMAST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NCSNO, Me.CKBNAME, Me.CCSNAME, Me.CCSKANA, Me.DBIRTH, Me.NSEX, Me.NZIP, Me.CADDRESS, Me.CTELEPHONE, Me.CPOTABLENUM, Me.DMEMBER, Me.MEMBERNO})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCSMAST.DefaultCellStyle = DataGridViewCellStyle15
        Me.dgvCSMAST.EnableHeadersVisualStyles = False
        Me.dgvCSMAST.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvCSMAST.Location = New System.Drawing.Point(12, 153)
        Me.dgvCSMAST.MultiSelect = False
        Me.dgvCSMAST.Name = "dgvCSMAST"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCSMAST.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvCSMAST.RowHeadersVisible = False
        Me.dgvCSMAST.RowHeadersWidth = 61
        Me.dgvCSMAST.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCSMAST.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvCSMAST.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvCSMAST.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvCSMAST.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvCSMAST.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvCSMAST.RowTemplate.Height = 40
        Me.dgvCSMAST.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCSMAST.Size = New System.Drawing.Size(1082, 389)
        Me.dgvCSMAST.TabIndex = 9
        '
        'lblMsg
        '
        Me.lblMsg.BackColor = System.Drawing.Color.Transparent
        Me.lblMsg.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(12, 105)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(830, 46)
        Me.lblMsg.TabIndex = 161
        Me.lblMsg.Text = "※顧客データが見つかりません。※"
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMsg.Visible = False
        '
        'txtStaNCSNO
        '
        Me.txtStaNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaNCSNO.Location = New System.Drawing.Point(206, 571)
        Me.txtStaNCSNO.MaxLength = 8
        Me.txtStaNCSNO.Name = "txtStaNCSNO"
        Me.txtStaNCSNO.Size = New System.Drawing.Size(121, 34)
        Me.txtStaNCSNO.TabIndex = 0
        Me.txtStaNCSNO.Text = "99999999"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(12, 571)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(192, 34)
        Me.Label22.TabIndex = 171
        Me.Label22.Text = "顧客番号"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEndNCSNO
        '
        Me.txtEndNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndNCSNO.Location = New System.Drawing.Point(373, 570)
        Me.txtEndNCSNO.MaxLength = 8
        Me.txtEndNCSNO.Name = "txtEndNCSNO"
        Me.txtEndNCSNO.Size = New System.Drawing.Size(121, 34)
        Me.txtEndNCSNO.TabIndex = 1
        Me.txtEndNCSNO.Text = "99999999"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(331, 570)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 34)
        Me.Label1.TabIndex = 174
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbNCSRANK
        '
        Me.cmbNCSRANK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNCSRANK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNCSRANK.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNCSRANK.FormattingEnabled = True
        Me.cmbNCSRANK.Location = New System.Drawing.Point(206, 622)
        Me.cmbNCSRANK.Name = "cmbNCSRANK"
        Me.cmbNCSRANK.Size = New System.Drawing.Size(246, 35)
        Me.cmbNCSRANK.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 622)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 34)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "顧客種別"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCCSKANA
        '
        Me.txtCCSKANA.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCCSKANA.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtCCSKANA.Location = New System.Drawing.Point(668, 676)
        Me.txtCCSKANA.MaxLength = 10
        Me.txtCCSKANA.Name = "txtCCSKANA"
        Me.txtCCSKANA.Size = New System.Drawing.Size(184, 34)
        Me.txtCCSKANA.TabIndex = 5
        Me.txtCCSKANA.Text = "ｱｱｱｱｱｱｱｱｱｱ"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(474, 676)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 34)
        Me.Label3.TabIndex = 236
        Me.Label3.Text = "氏名ｶﾅ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCCSNAME
        '
        Me.txtCCSNAME.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCCSNAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCCSNAME.Location = New System.Drawing.Point(206, 676)
        Me.txtCCSNAME.MaxLength = 10
        Me.txtCCSNAME.Name = "txtCCSNAME"
        Me.txtCCSNAME.Size = New System.Drawing.Size(246, 34)
        Me.txtCCSNAME.TabIndex = 4
        Me.txtCCSNAME.Text = "ああああああああああ"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkGreen
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(12, 676)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(192, 34)
        Me.Label4.TabIndex = 234
        Me.Label4.Text = "氏名"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(12, 728)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(192, 34)
        Me.Label5.TabIndex = 238
        Me.Label5.Text = "性別"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbNSEX
        '
        Me.cmbNSEX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNSEX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNSEX.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNSEX.FormattingEnabled = True
        Me.cmbNSEX.Items.AddRange(New Object() {"", "男", "女", "不明"})
        Me.cmbNSEX.Location = New System.Drawing.Point(206, 728)
        Me.cmbNSEX.Name = "cmbNSEX"
        Me.cmbNSEX.Size = New System.Drawing.Size(96, 35)
        Me.cmbNSEX.TabIndex = 6
        Me.cmbNSEX.TabStop = False
        '
        'txtCADDRESS
        '
        Me.txtCADDRESS.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCADDRESS.Location = New System.Drawing.Point(206, 779)
        Me.txtCADDRESS.MaxLength = 25
        Me.txtCADDRESS.Name = "txtCADDRESS"
        Me.txtCADDRESS.Size = New System.Drawing.Size(564, 34)
        Me.txtCADDRESS.TabIndex = 7
        Me.txtCADDRESS.Text = "あああああああああああああああああああああああああ"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.DarkGreen
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(12, 779)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(192, 34)
        Me.Label8.TabIndex = 244
        Me.Label8.Text = "住所"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDBIRTH
        '
        Me.txtDBIRTH.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDBIRTH.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtDBIRTH.Location = New System.Drawing.Point(206, 828)
        Me.txtDBIRTH.MaxLength = 8
        Me.txtDBIRTH.Name = "txtDBIRTH"
        Me.txtDBIRTH.Size = New System.Drawing.Size(165, 34)
        Me.txtDBIRTH.TabIndex = 8
        Me.txtDBIRTH.Text = "1985/12/03"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.DarkGreen
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(12, 828)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(192, 34)
        Me.Label7.TabIndex = 246
        Me.Label7.Text = "誕生日"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkCard
        '
        Me.chkCard.AutoSize = True
        Me.chkCard.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkCard.Location = New System.Drawing.Point(568, 573)
        Me.chkCard.Name = "chkCard"
        Me.chkCard.Size = New System.Drawing.Size(173, 31)
        Me.chkCard.TabIndex = 255
        Me.chkCard.TabStop = False
        Me.chkCard.Text = "カード未発行"
        Me.chkCard.UseVisualStyleBackColor = True
        '
        'txtStaDMEMBER
        '
        Me.txtStaDMEMBER.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaDMEMBER.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtStaDMEMBER.Location = New System.Drawing.Point(598, 828)
        Me.txtStaDMEMBER.MaxLength = 8
        Me.txtStaDMEMBER.Name = "txtStaDMEMBER"
        Me.txtStaDMEMBER.Size = New System.Drawing.Size(165, 34)
        Me.txtStaDMEMBER.TabIndex = 9
        Me.txtStaDMEMBER.Text = "1985/12/03"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.DarkGreen
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(404, 828)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(192, 34)
        Me.Label6.TabIndex = 256
        Me.Label6.Text = "会員期限"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(766, 828)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 34)
        Me.Label9.TabIndex = 258
        Me.Label9.Text = "～"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEndDMEMBER
        '
        Me.txtEndDMEMBER.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndDMEMBER.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtEndDMEMBER.Location = New System.Drawing.Point(803, 828)
        Me.txtEndDMEMBER.MaxLength = 8
        Me.txtEndDMEMBER.Name = "txtEndDMEMBER"
        Me.txtEndDMEMBER.Size = New System.Drawing.Size(165, 34)
        Me.txtEndDMEMBER.TabIndex = 10
        Me.txtEndDMEMBER.Text = "1985/12/03"
        '
        'txtCPOTABLENUM
        '
        Me.txtCPOTABLENUM.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCPOTABLENUM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtCPOTABLENUM.Location = New System.Drawing.Point(643, 879)
        Me.txtCPOTABLENUM.MaxLength = 15
        Me.txtCPOTABLENUM.Name = "txtCPOTABLENUM"
        Me.txtCPOTABLENUM.Size = New System.Drawing.Size(227, 34)
        Me.txtCPOTABLENUM.TabIndex = 12
        Me.txtCPOTABLENUM.Text = "999999999999999"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(449, 879)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(192, 34)
        Me.Label12.TabIndex = 261
        Me.Label12.Text = "携帯電話番号"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCTELEPHONE
        '
        Me.txtCTELEPHONE.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCTELEPHONE.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtCTELEPHONE.Location = New System.Drawing.Point(206, 879)
        Me.txtCTELEPHONE.MaxLength = 15
        Me.txtCTELEPHONE.Name = "txtCTELEPHONE"
        Me.txtCTELEPHONE.Size = New System.Drawing.Size(227, 34)
        Me.txtCTELEPHONE.TabIndex = 11
        Me.txtCTELEPHONE.Text = "999999999999999"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkGreen
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(12, 879)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(192, 34)
        Me.Label10.TabIndex = 259
        Me.Label10.Text = "電話番号"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(331, 927)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 34)
        Me.Label11.TabIndex = 265
        Me.Label11.Text = "～"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label11.Visible = False
        '
        'txtEndMEMBERNO
        '
        Me.txtEndMEMBERNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndMEMBERNO.Location = New System.Drawing.Point(373, 927)
        Me.txtEndMEMBERNO.MaxLength = 8
        Me.txtEndMEMBERNO.Name = "txtEndMEMBERNO"
        Me.txtEndMEMBERNO.Size = New System.Drawing.Size(121, 34)
        Me.txtEndMEMBERNO.TabIndex = 14
        Me.txtEndMEMBERNO.Text = "99999999"
        Me.txtEndMEMBERNO.Visible = False
        '
        'txtStaMEMBERNO
        '
        Me.txtStaMEMBERNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaMEMBERNO.Location = New System.Drawing.Point(206, 928)
        Me.txtStaMEMBERNO.MaxLength = 8
        Me.txtStaMEMBERNO.Name = "txtStaMEMBERNO"
        Me.txtStaMEMBERNO.Size = New System.Drawing.Size(121, 34)
        Me.txtStaMEMBERNO.TabIndex = 13
        Me.txtStaMEMBERNO.Text = "99999999"
        Me.txtStaMEMBERNO.Visible = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.DarkGreen
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(12, 928)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(192, 34)
        Me.Label13.TabIndex = 264
        Me.Label13.Text = "会員番号"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label13.Visible = False
        '
        'chkCard2
        '
        Me.chkCard2.AutoSize = True
        Me.chkCard2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkCard2.Location = New System.Drawing.Point(760, 572)
        Me.chkCard2.Name = "chkCard2"
        Me.chkCard2.Size = New System.Drawing.Size(197, 31)
        Me.chkCard2.TabIndex = 266
        Me.chkCard2.TabStop = False
        Me.chkCard2.Text = "カード発行済み"
        Me.chkCard2.UseVisualStyleBackColor = True
        '
        'NCSNO
        '
        Me.NCSNO.DataPropertyName = "NCSNO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.NCSNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.NCSNO.Frozen = True
        Me.NCSNO.HeaderText = "顧客番号"
        Me.NCSNO.Name = "NCSNO"
        Me.NCSNO.ReadOnly = True
        Me.NCSNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NCSNO.Width = 150
        '
        'CKBNAME
        '
        Me.CKBNAME.DataPropertyName = "CKBNAME"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.CKBNAME.DefaultCellStyle = DataGridViewCellStyle4
        Me.CKBNAME.Frozen = True
        Me.CKBNAME.HeaderText = "顧客種別"
        Me.CKBNAME.Name = "CKBNAME"
        Me.CKBNAME.ReadOnly = True
        Me.CKBNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CKBNAME.Width = 200
        '
        'CCSNAME
        '
        Me.CCSNAME.DataPropertyName = "CCSNAME"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.CCSNAME.DefaultCellStyle = DataGridViewCellStyle5
        Me.CCSNAME.Frozen = True
        Me.CCSNAME.HeaderText = "氏名"
        Me.CCSNAME.MaxInputLength = 10
        Me.CCSNAME.Name = "CCSNAME"
        Me.CCSNAME.ReadOnly = True
        Me.CCSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CCSNAME.Width = 250
        '
        'CCSKANA
        '
        Me.CCSKANA.DataPropertyName = "CCSKANA"
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.CCSKANA.DefaultCellStyle = DataGridViewCellStyle6
        Me.CCSKANA.HeaderText = "氏名ｶﾅ"
        Me.CCSKANA.Name = "CCSKANA"
        Me.CCSKANA.ReadOnly = True
        Me.CCSKANA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CCSKANA.Width = 200
        '
        'DBIRTH
        '
        Me.DBIRTH.DataPropertyName = "DBIRTH"
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.DBIRTH.DefaultCellStyle = DataGridViewCellStyle7
        Me.DBIRTH.HeaderText = "誕生日"
        Me.DBIRTH.Name = "DBIRTH"
        Me.DBIRTH.ReadOnly = True
        Me.DBIRTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DBIRTH.Width = 150
        '
        'NSEX
        '
        Me.NSEX.DataPropertyName = "NSEX"
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.NSEX.DefaultCellStyle = DataGridViewCellStyle8
        Me.NSEX.HeaderText = "性別"
        Me.NSEX.Name = "NSEX"
        Me.NSEX.ReadOnly = True
        Me.NSEX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'NZIP
        '
        Me.NZIP.DataPropertyName = "NZIP"
        DataGridViewCellStyle9.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.NZIP.DefaultCellStyle = DataGridViewCellStyle9
        Me.NZIP.HeaderText = "郵便番号"
        Me.NZIP.Name = "NZIP"
        Me.NZIP.ReadOnly = True
        Me.NZIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NZIP.Visible = False
        Me.NZIP.Width = 120
        '
        'CADDRESS
        '
        Me.CADDRESS.DataPropertyName = "CADDRESS"
        DataGridViewCellStyle10.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.CADDRESS.DefaultCellStyle = DataGridViewCellStyle10
        Me.CADDRESS.HeaderText = "住所"
        Me.CADDRESS.Name = "CADDRESS"
        Me.CADDRESS.ReadOnly = True
        Me.CADDRESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CADDRESS.Visible = False
        Me.CADDRESS.Width = 400
        '
        'CTELEPHONE
        '
        Me.CTELEPHONE.DataPropertyName = "CTELEPHONE"
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        Me.CTELEPHONE.DefaultCellStyle = DataGridViewCellStyle11
        Me.CTELEPHONE.HeaderText = "電話番号"
        Me.CTELEPHONE.Name = "CTELEPHONE"
        Me.CTELEPHONE.ReadOnly = True
        Me.CTELEPHONE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CTELEPHONE.Visible = False
        Me.CTELEPHONE.Width = 200
        '
        'CPOTABLENUM
        '
        Me.CPOTABLENUM.DataPropertyName = "CPOTABLENUM"
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle12.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black
        Me.CPOTABLENUM.DefaultCellStyle = DataGridViewCellStyle12
        Me.CPOTABLENUM.HeaderText = "携帯電話番号"
        Me.CPOTABLENUM.Name = "CPOTABLENUM"
        Me.CPOTABLENUM.ReadOnly = True
        Me.CPOTABLENUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CPOTABLENUM.Visible = False
        Me.CPOTABLENUM.Width = 200
        '
        'DMEMBER
        '
        Me.DMEMBER.DataPropertyName = "DMEMBER"
        DataGridViewCellStyle13.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black
        Me.DMEMBER.DefaultCellStyle = DataGridViewCellStyle13
        Me.DMEMBER.HeaderText = "会員期限"
        Me.DMEMBER.Name = "DMEMBER"
        Me.DMEMBER.ReadOnly = True
        Me.DMEMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DMEMBER.Width = 150
        '
        'MEMBERNO
        '
        Me.MEMBERNO.DataPropertyName = "MEMBERNO"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle14.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        Me.MEMBERNO.DefaultCellStyle = DataGridViewCellStyle14
        Me.MEMBERNO.HeaderText = "会員番号"
        Me.MEMBERNO.Name = "MEMBERNO"
        Me.MEMBERNO.ReadOnly = True
        Me.MEMBERNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MEMBERNO.Visible = False
        Me.MEMBERNO.Width = 150
        '
        'frmCSSEARCH01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1106, 1000)
        Me.Controls.Add(Me.chkCard2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtEndMEMBERNO)
        Me.Controls.Add(Me.txtStaMEMBERNO)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtCPOTABLENUM)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtCTELEPHONE)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtEndDMEMBER)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtStaDMEMBER)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.chkCard)
        Me.Controls.Add(Me.txtDBIRTH)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCADDRESS)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbNSEX)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtCCSKANA)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCCSNAME)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbNCSRANK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtEndNCSNO)
        Me.Controls.Add(Me.txtStaNCSNO)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.dgvCSMAST)
        Me.MaximumSize = New System.Drawing.Size(1122, 1100)
        Me.MinimumSize = New System.Drawing.Size(1122, 1038)
        Me.Name = "frmCSSEARCH01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvCSMAST, 0)
        Me.Controls.SetChildIndex(Me.lblMsg, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.txtStaNCSNO, 0)
        Me.Controls.SetChildIndex(Me.txtEndNCSNO, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.cmbNCSRANK, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtCCSNAME, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtCCSKANA, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.cmbNSEX, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtCADDRESS, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.txtDBIRTH, 0)
        Me.Controls.SetChildIndex(Me.chkCard, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.txtStaDMEMBER, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtEndDMEMBER, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.txtCTELEPHONE, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.txtCPOTABLENUM, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.txtStaMEMBERNO, 0)
        Me.Controls.SetChildIndex(Me.txtEndMEMBERNO, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.chkCard2, 0)
        CType(Me.dgvCSMAST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvCSMAST As BaseControl.CustomGridView
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents txtStaNCSNO As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtEndNCSNO As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNCSRANK As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCCSKANA As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCCSNAME As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbNSEX As System.Windows.Forms.ComboBox
    Friend WithEvents txtCADDRESS As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDBIRTH As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkCard As System.Windows.Forms.CheckBox
    Friend WithEvents txtStaDMEMBER As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEndDMEMBER As System.Windows.Forms.TextBox
    Friend WithEvents txtCPOTABLENUM As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCTELEPHONE As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtEndMEMBERNO As System.Windows.Forms.TextBox
    Friend WithEvents txtStaMEMBERNO As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkCard2 As System.Windows.Forms.CheckBox
    Friend WithEvents NCSNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCSKANA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DBIRTH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NSEX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NZIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CADDRESS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CTELEPHONE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPOTABLENUM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DMEMBER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MEMBERNO As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
