<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINT01
    Inherits Techno.FORM.BaseForm01

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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.rdoPrint2 = New System.Windows.Forms.RadioButton()
        Me.rdoPrint1 = New System.Windows.Forms.RadioButton()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtStaNCSNO = New BaseControl.CustomTextBoxNum()
        Me.txtEndNCSNO = New BaseControl.CustomTextBoxNum()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.txtEndENTCNT2 = New BaseControl.CustomTextBoxNum()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtStaENTCNT2 = New BaseControl.CustomTextBoxNum()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpEndZENENTDATE = New System.Windows.Forms.DateTimePicker()
        Me.dtpStaZENENTDATE = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpEndDMEMBER = New System.Windows.Forms.DateTimePicker()
        Me.dtpStaDMEMBER = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpEndDENTRY = New System.Windows.Forms.DateTimePicker()
        Me.dtpStaDENTRY = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCADDRESS = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBIRTHMM = New BaseControl.CustomTextBoxNum()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbSEXKB = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbKBMAST = New System.Windows.Forms.ComboBox()
        Me.dgvCSMAST = New BaseControl.CustomGridView()
        Me.colCHKBOX = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMANNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSCRANK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMANNM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCCSKANA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNSEX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNZIP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCADDRESS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDBIRTH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCTELEPHONE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPOTABLENUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDMEMBER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblMaxCount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvCSMAST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdoPrint2
        '
        Me.rdoPrint2.AutoSize = True
        Me.rdoPrint2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoPrint2.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoPrint2.Location = New System.Drawing.Point(317, 52)
        Me.rdoPrint2.Name = "rdoPrint2"
        Me.rdoPrint2.Size = New System.Drawing.Size(180, 38)
        Me.rdoPrint2.TabIndex = 1075
        Me.rdoPrint2.Text = "宛名ラベル"
        Me.rdoPrint2.UseVisualStyleBackColor = True
        '
        'rdoPrint1
        '
        Me.rdoPrint1.AutoSize = True
        Me.rdoPrint1.Checked = True
        Me.rdoPrint1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoPrint1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoPrint1.Location = New System.Drawing.Point(76, 52)
        Me.rdoPrint1.Name = "rdoPrint1"
        Me.rdoPrint1.Size = New System.Drawing.Size(168, 38)
        Me.rdoPrint1.TabIndex = 1074
        Me.rdoPrint1.TabStop = True
        Me.rdoPrint1.Text = "顧客一覧"
        Me.rdoPrint1.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(78, 172)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(212, 35)
        Me.Label22.TabIndex = 1076
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
        Me.txtStaNCSNO.Location = New System.Drawing.Point(291, 173)
        Me.txtStaNCSNO.MaxLength = 8
        Me.txtStaNCSNO.Name = "txtStaNCSNO"
        Me.txtStaNCSNO.Size = New System.Drawing.Size(177, 34)
        Me.txtStaNCSNO.TabIndex = 1077
        Me.txtStaNCSNO.Tag = ""
        Me.txtStaNCSNO.Text = "99999999"
        Me.txtStaNCSNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEndNCSNO
        '
        Me.txtEndNCSNO.BackColor = System.Drawing.Color.White
        Me.txtEndNCSNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEndNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndNCSNO.Format = ""
        Me.txtEndNCSNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtEndNCSNO.Location = New System.Drawing.Point(513, 173)
        Me.txtEndNCSNO.MaxLength = 8
        Me.txtEndNCSNO.Name = "txtEndNCSNO"
        Me.txtEndNCSNO.Size = New System.Drawing.Size(177, 34)
        Me.txtEndNCSNO.TabIndex = 1079
        Me.txtEndNCSNO.Tag = ""
        Me.txtEndNCSNO.Text = "99999999"
        Me.txtEndNCSNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(472, 177)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 27)
        Me.Label2.TabIndex = 1080
        Me.Label2.Text = "～"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnFind)
        Me.Panel1.Controls.Add(Me.txtEndENTCNT2)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtStaENTCNT2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.dtpEndZENENTDATE)
        Me.Panel1.Controls.Add(Me.dtpStaZENENTDATE)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.dtpEndDMEMBER)
        Me.Panel1.Controls.Add(Me.dtpStaDMEMBER)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.dtpEndDENTRY)
        Me.Panel1.Controls.Add(Me.dtpStaDENTRY)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtCADDRESS)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtBIRTHMM)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cmbSEXKB)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbKBMAST)
        Me.Panel1.Controls.Add(Me.rdoPrint1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.rdoPrint2)
        Me.Panel1.Controls.Add(Me.txtEndNCSNO)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.txtStaNCSNO)
        Me.Panel1.Location = New System.Drawing.Point(12, 130)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(902, 793)
        Me.Panel1.TabIndex = 1081
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.Color.Orange
        Me.btnFind.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFind.ForeColor = System.Drawing.Color.Black
        Me.btnFind.Location = New System.Drawing.Point(654, 38)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(207, 71)
        Me.btnFind.TabIndex = 1106
        Me.btnFind.Text = "検索"
        Me.btnFind.UseVisualStyleBackColor = False
        '
        'txtEndENTCNT2
        '
        Me.txtEndENTCNT2.BackColor = System.Drawing.Color.White
        Me.txtEndENTCNT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEndENTCNT2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndENTCNT2.Format = "#,##0"
        Me.txtEndENTCNT2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtEndENTCNT2.Location = New System.Drawing.Point(422, 608)
        Me.txtEndENTCNT2.MaxLength = 5
        Me.txtEndENTCNT2.Name = "txtEndENTCNT2"
        Me.txtEndENTCNT2.Size = New System.Drawing.Size(84, 34)
        Me.txtEndENTCNT2.TabIndex = 1105
        Me.txtEndENTCNT2.Tag = ""
        Me.txtEndENTCNT2.Text = "99999"
        Me.txtEndENTCNT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(380, 612)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(39, 27)
        Me.Label13.TabIndex = 1104
        Me.Label13.Text = "～"
        '
        'txtStaENTCNT2
        '
        Me.txtStaENTCNT2.BackColor = System.Drawing.Color.White
        Me.txtStaENTCNT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStaENTCNT2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaENTCNT2.Format = "#,##0"
        Me.txtStaENTCNT2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtStaENTCNT2.Location = New System.Drawing.Point(291, 608)
        Me.txtStaENTCNT2.MaxLength = 5
        Me.txtStaENTCNT2.Name = "txtStaENTCNT2"
        Me.txtStaENTCNT2.Size = New System.Drawing.Size(84, 34)
        Me.txtStaENTCNT2.TabIndex = 1103
        Me.txtStaENTCNT2.Tag = ""
        Me.txtStaENTCNT2.Text = "99999"
        Me.txtStaENTCNT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.DarkGreen
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(78, 607)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 35)
        Me.Label1.TabIndex = 1102
        Me.Label1.Text = "月間来場回数"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(518, 565)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 27)
        Me.Label11.TabIndex = 1101
        Me.Label11.Text = "～"
        '
        'dtpEndZENENTDATE
        '
        Me.dtpEndZENENTDATE.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpEndZENENTDATE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndZENENTDATE.Location = New System.Drawing.Point(559, 559)
        Me.dtpEndZENENTDATE.Name = "dtpEndZENENTDATE"
        Me.dtpEndZENENTDATE.ShowCheckBox = True
        Me.dtpEndZENENTDATE.Size = New System.Drawing.Size(210, 34)
        Me.dtpEndZENENTDATE.TabIndex = 1100
        '
        'dtpStaZENENTDATE
        '
        Me.dtpStaZENENTDATE.Checked = False
        Me.dtpStaZENENTDATE.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpStaZENENTDATE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStaZENENTDATE.Location = New System.Drawing.Point(292, 559)
        Me.dtpStaZENENTDATE.Name = "dtpStaZENENTDATE"
        Me.dtpStaZENENTDATE.ShowCheckBox = True
        Me.dtpStaZENENTDATE.Size = New System.Drawing.Size(220, 34)
        Me.dtpStaZENENTDATE.TabIndex = 1099
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(78, 559)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(212, 34)
        Me.Label12.TabIndex = 1098
        Me.Label12.Text = "前回来場日"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(516, 519)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 27)
        Me.Label9.TabIndex = 1097
        Me.Label9.Text = "～"
        '
        'dtpEndDMEMBER
        '
        Me.dtpEndDMEMBER.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpEndDMEMBER.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDMEMBER.Location = New System.Drawing.Point(557, 513)
        Me.dtpEndDMEMBER.Name = "dtpEndDMEMBER"
        Me.dtpEndDMEMBER.ShowCheckBox = True
        Me.dtpEndDMEMBER.Size = New System.Drawing.Size(210, 34)
        Me.dtpEndDMEMBER.TabIndex = 1096
        '
        'dtpStaDMEMBER
        '
        Me.dtpStaDMEMBER.Checked = False
        Me.dtpStaDMEMBER.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpStaDMEMBER.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStaDMEMBER.Location = New System.Drawing.Point(290, 513)
        Me.dtpStaDMEMBER.Name = "dtpStaDMEMBER"
        Me.dtpStaDMEMBER.ShowCheckBox = True
        Me.dtpStaDMEMBER.Size = New System.Drawing.Size(220, 34)
        Me.dtpStaDMEMBER.TabIndex = 1095
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkGreen
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(76, 513)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(212, 34)
        Me.Label10.TabIndex = 1094
        Me.Label10.Text = "会員期限"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(516, 476)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 27)
        Me.Label8.TabIndex = 1093
        Me.Label8.Text = "～"
        '
        'dtpEndDENTRY
        '
        Me.dtpEndDENTRY.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpEndDENTRY.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDENTRY.Location = New System.Drawing.Point(557, 470)
        Me.dtpEndDENTRY.Name = "dtpEndDENTRY"
        Me.dtpEndDENTRY.ShowCheckBox = True
        Me.dtpEndDENTRY.Size = New System.Drawing.Size(210, 34)
        Me.dtpEndDENTRY.TabIndex = 1092
        '
        'dtpStaDENTRY
        '
        Me.dtpStaDENTRY.Checked = False
        Me.dtpStaDENTRY.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpStaDENTRY.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStaDENTRY.Location = New System.Drawing.Point(290, 470)
        Me.dtpStaDENTRY.Name = "dtpStaDENTRY"
        Me.dtpStaDENTRY.ShowCheckBox = True
        Me.dtpStaDENTRY.Size = New System.Drawing.Size(220, 34)
        Me.dtpStaDENTRY.TabIndex = 1091
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.DarkGreen
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(76, 470)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(212, 34)
        Me.Label7.TabIndex = 1089
        Me.Label7.Text = "会員登録日"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCADDRESS
        '
        Me.txtCADDRESS.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCADDRESS.Location = New System.Drawing.Point(291, 427)
        Me.txtCADDRESS.MaxLength = 25
        Me.txtCADDRESS.Name = "txtCADDRESS"
        Me.txtCADDRESS.Size = New System.Drawing.Size(570, 34)
        Me.txtCADDRESS.TabIndex = 1088
        Me.txtCADDRESS.Text = "あああああああああああああああああああああああああ"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.DarkGreen
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(76, 427)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(212, 34)
        Me.Label6.TabIndex = 1087
        Me.Label6.Text = "住所"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBIRTHMM
        '
        Me.txtBIRTHMM.BackColor = System.Drawing.Color.White
        Me.txtBIRTHMM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBIRTHMM.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBIRTHMM.Format = ""
        Me.txtBIRTHMM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtBIRTHMM.Location = New System.Drawing.Point(291, 368)
        Me.txtBIRTHMM.MaxLength = 2
        Me.txtBIRTHMM.Name = "txtBIRTHMM"
        Me.txtBIRTHMM.Size = New System.Drawing.Size(66, 34)
        Me.txtBIRTHMM.TabIndex = 1086
        Me.txtBIRTHMM.Tag = ""
        Me.txtBIRTHMM.Text = "99"
        Me.txtBIRTHMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(78, 367)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(212, 35)
        Me.Label5.TabIndex = 1085
        Me.Label5.Text = "誕生月"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkGreen
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(78, 306)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(212, 35)
        Me.Label4.TabIndex = 1084
        Me.Label4.Text = "性別"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbSEXKB
        '
        Me.cmbSEXKB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSEXKB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSEXKB.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSEXKB.FormattingEnabled = True
        Me.cmbSEXKB.Items.AddRange(New Object() {"", "男性", "女性"})
        Me.cmbSEXKB.Location = New System.Drawing.Point(291, 306)
        Me.cmbSEXKB.Name = "cmbSEXKB"
        Me.cmbSEXKB.Size = New System.Drawing.Size(108, 35)
        Me.cmbSEXKB.TabIndex = 1083
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(76, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(212, 35)
        Me.Label3.TabIndex = 1082
        Me.Label3.Text = "顧客種別"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbKBMAST
        '
        Me.cmbKBMAST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKBMAST.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbKBMAST.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKBMAST.FormattingEnabled = True
        Me.cmbKBMAST.Location = New System.Drawing.Point(291, 243)
        Me.cmbKBMAST.Name = "cmbKBMAST"
        Me.cmbKBMAST.Size = New System.Drawing.Size(419, 35)
        Me.cmbKBMAST.TabIndex = 1081
        Me.cmbKBMAST.TabStop = False
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
        Me.dgvCSMAST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCHKBOX, Me.colMANNO, Me.colNSCRANK, Me.colMANNM, Me.colCCSKANA, Me.colNSEX, Me.colNZIP, Me.colCADDRESS, Me.colDBIRTH, Me.colCTELEPHONE, Me.colCPOTABLENUM, Me.colDMEMBER})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCSMAST.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvCSMAST.EnableHeadersVisualStyles = False
        Me.dgvCSMAST.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvCSMAST.Location = New System.Drawing.Point(954, 168)
        Me.dgvCSMAST.MultiSelect = False
        Me.dgvCSMAST.Name = "dgvCSMAST"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCSMAST.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
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
        Me.dgvCSMAST.Size = New System.Drawing.Size(865, 629)
        Me.dgvCSMAST.TabIndex = 1082
        '
        'colCHKBOX
        '
        Me.colCHKBOX.DataPropertyName = "CHKBOX"
        Me.colCHKBOX.Frozen = True
        Me.colCHKBOX.HeaderText = ""
        Me.colCHKBOX.Name = "colCHKBOX"
        Me.colCHKBOX.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCHKBOX.Width = 50
        '
        'colMANNO
        '
        Me.colMANNO.DataPropertyName = "NCSNO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colMANNO.DefaultCellStyle = DataGridViewCellStyle3
        Me.colMANNO.Frozen = True
        Me.colMANNO.HeaderText = "顧客番号"
        Me.colMANNO.Name = "colMANNO"
        Me.colMANNO.ReadOnly = True
        Me.colMANNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colMANNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMANNO.Width = 150
        '
        'colNSCRANK
        '
        Me.colNSCRANK.DataPropertyName = "NCSRANK"
        Me.colNSCRANK.Frozen = True
        Me.colNSCRANK.HeaderText = "顧客種別"
        Me.colNSCRANK.Name = "colNSCRANK"
        Me.colNSCRANK.ReadOnly = True
        Me.colNSCRANK.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colNSCRANK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNSCRANK.Width = 200
        '
        'colMANNM
        '
        Me.colMANNM.DataPropertyName = "CCSNAME"
        Me.colMANNM.Frozen = True
        Me.colMANNM.HeaderText = "氏名"
        Me.colMANNM.Name = "colMANNM"
        Me.colMANNM.ReadOnly = True
        Me.colMANNM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colMANNM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMANNM.Width = 250
        '
        'colCCSKANA
        '
        Me.colCCSKANA.DataPropertyName = "CCSKANA"
        Me.colCCSKANA.HeaderText = "氏名ｶﾅ"
        Me.colCCSKANA.Name = "colCCSKANA"
        Me.colCCSKANA.ReadOnly = True
        Me.colCCSKANA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCCSKANA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCCSKANA.Width = 200
        '
        'colNSEX
        '
        Me.colNSEX.DataPropertyName = "NSEX"
        Me.colNSEX.HeaderText = "性別"
        Me.colNSEX.Name = "colNSEX"
        Me.colNSEX.ReadOnly = True
        Me.colNSEX.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colNSEX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colNZIP
        '
        Me.colNZIP.DataPropertyName = "NZIP"
        Me.colNZIP.HeaderText = "〒番号"
        Me.colNZIP.Name = "colNZIP"
        Me.colNZIP.ReadOnly = True
        Me.colNZIP.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colNZIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNZIP.Width = 120
        '
        'colCADDRESS
        '
        Me.colCADDRESS.DataPropertyName = "CADDRESS"
        Me.colCADDRESS.HeaderText = "住所"
        Me.colCADDRESS.Name = "colCADDRESS"
        Me.colCADDRESS.ReadOnly = True
        Me.colCADDRESS.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCADDRESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCADDRESS.Width = 400
        '
        'colDBIRTH
        '
        Me.colDBIRTH.DataPropertyName = "DBIRTH"
        Me.colDBIRTH.HeaderText = "誕生日"
        Me.colDBIRTH.Name = "colDBIRTH"
        Me.colDBIRTH.ReadOnly = True
        Me.colDBIRTH.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colDBIRTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDBIRTH.Width = 150
        '
        'colCTELEPHONE
        '
        Me.colCTELEPHONE.DataPropertyName = "CTELEPHONE"
        Me.colCTELEPHONE.HeaderText = "電話番号"
        Me.colCTELEPHONE.Name = "colCTELEPHONE"
        Me.colCTELEPHONE.ReadOnly = True
        Me.colCTELEPHONE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCTELEPHONE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCTELEPHONE.Width = 200
        '
        'colCPOTABLENUM
        '
        Me.colCPOTABLENUM.DataPropertyName = "CPOTABLENUM"
        Me.colCPOTABLENUM.HeaderText = "携帯電話番号"
        Me.colCPOTABLENUM.Name = "colCPOTABLENUM"
        Me.colCPOTABLENUM.ReadOnly = True
        Me.colCPOTABLENUM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCPOTABLENUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colCPOTABLENUM.Width = 200
        '
        'colDMEMBER
        '
        Me.colDMEMBER.DataPropertyName = "DMEMBER"
        Me.colDMEMBER.HeaderText = "会員期限"
        Me.colDMEMBER.Name = "colDMEMBER"
        Me.colDMEMBER.ReadOnly = True
        Me.colDMEMBER.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colDMEMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDMEMBER.Width = 150
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkAll.Location = New System.Drawing.Point(954, 801)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(143, 25)
        Me.chkAll.TabIndex = 1083
        Me.chkAll.Text = "すべてチェック"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(1580, 800)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 21)
        Me.Label14.TabIndex = 1086
        Me.Label14.Text = "件中"
        '
        'lblMaxCount
        '
        Me.lblMaxCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxCount.Location = New System.Drawing.Point(1627, 800)
        Me.lblMaxCount.Name = "lblMaxCount"
        Me.lblMaxCount.Size = New System.Drawing.Size(56, 21)
        Me.lblMaxCount.TabIndex = 1087
        Me.lblMaxCount.Text = "0"
        Me.lblMaxCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(1680, 800)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 21)
        Me.Label16.TabIndex = 1088
        Me.Label16.Text = "件表示"
        '
        'lblCount
        '
        Me.lblCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCount.Location = New System.Drawing.Point(1520, 800)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(58, 21)
        Me.lblCount.TabIndex = 1089
        Me.lblCount.Text = "0"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMode.Location = New System.Drawing.Point(1769, 800)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(54, 21)
        Me.lblMode.TabIndex = 1090
        Me.lblMode.Text = "全件"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(1752, 800)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(22, 21)
        Me.Label15.TabIndex = 1091
        Me.Label15.Text = "/"
        '
        'frmPRINT01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.lblMode)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblMaxCount)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.dgvCSMAST)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmPRINT01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.dgvCSMAST, 0)
        Me.Controls.SetChildIndex(Me.chkAll, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.lblMaxCount, 0)
        Me.Controls.SetChildIndex(Me.Label16, 0)
        Me.Controls.SetChildIndex(Me.lblCount, 0)
        Me.Controls.SetChildIndex(Me.lblMode, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvCSMAST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoPrint2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPrint1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtStaNCSNO As BaseControl.CustomTextBoxNum
    Friend WithEvents txtEndNCSNO As BaseControl.CustomTextBoxNum
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbKBMAST As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbSEXKB As System.Windows.Forms.ComboBox
    Friend WithEvents txtBIRTHMM As BaseControl.CustomTextBoxNum
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCADDRESS As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDENTRY As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStaDENTRY As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDMEMBER As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStaDMEMBER As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpEndZENENTDATE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStaZENENTDATE As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEndENTCNT2 As BaseControl.CustomTextBoxNum
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtStaENTCNT2 As BaseControl.CustomTextBoxNum
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvCSMAST As BaseControl.CustomGridView
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblMaxCount As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents colCHKBOX As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colMANNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSCRANK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMANNM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCCSKANA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNSEX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNZIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCADDRESS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDBIRTH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCTELEPHONE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCPOTABLENUM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDMEMBER As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
