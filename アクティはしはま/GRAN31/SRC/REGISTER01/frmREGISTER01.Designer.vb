<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmREGISTER01
    Inherits System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtPOINT = New BaseControl.CustomTextBoxNum()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPREZANKN = New BaseControl.CustomTextBoxNum()
        Me.txtZANKN = New BaseControl.CustomTextBoxNum()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDBIRTH = New System.Windows.Forms.TextBox()
        Me.txtDMEMBER = New System.Windows.Forms.TextBox()
        Me.txtSCLMANNO = New System.Windows.Forms.TextBox()
        Me.txtCKBNAME = New System.Windows.Forms.TextBox()
        Me.txtCCSNAME = New System.Windows.Forms.TextBox()
        Me.txtCCSKANA = New System.Windows.Forms.TextBox()
        Me.txtNCSNO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnBACK = New System.Windows.Forms.Button()
        Me.btnCLEAR = New System.Windows.Forms.Button()
        Me.btn00 = New System.Windows.Forms.Button()
        Me.btn0 = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btn9 = New System.Windows.Forms.Button()
        Me.btn8 = New System.Windows.Forms.Button()
        Me.btn7 = New System.Windows.Forms.Button()
        Me.txtDENNO = New System.Windows.Forms.TextBox()
        Me.chbRECEIPT = New System.Windows.Forms.CheckBox()
        Me.dgvGOODS = New BaseControl.CustomGridView()
        Me.GDSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDSCOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDSTAX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDSKIN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSEISAN = New System.Windows.Forms.Button()
        Me.btnRETURN = New System.Windows.Forms.Button()
        Me.chbCPAYKBN1 = New System.Windows.Forms.CheckBox()
        Me.chbCPAYKBN2 = New System.Windows.Forms.CheckBox()
        Me.chbCPAYKBN3 = New System.Windows.Forms.CheckBox()
        Me.lblGETPREMKN = New System.Windows.Forms.Label()
        Me.lblGETPOINT = New System.Windows.Forms.Label()
        Me.btnGETPREMKN_CLEAR = New System.Windows.Forms.Button()
        Me.btnGETPOINT_CLEAR = New System.Windows.Forms.Button()
        Me.txtPAYMENT = New BaseControl.CustomTextBoxNum()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtDEPOSIT = New BaseControl.CustomTextBoxNum()
        Me.txtCHANGE = New BaseControl.CustomTextBoxNum()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtGETPREMKN = New BaseControl.CustomTextBoxNum()
        Me.txtGETPOINT = New BaseControl.CustomTextBoxNum()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvGOODS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(26, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(136, 34)
        Me.Label22.TabIndex = 171
        Me.Label22.Text = "現金"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(179, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 34)
        Me.Label1.TabIndex = 172
        Me.Label1.Text = "伝票番号"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.txtDBIRTH)
        Me.GroupBox1.Controls.Add(Me.txtDMEMBER)
        Me.GroupBox1.Controls.Add(Me.txtSCLMANNO)
        Me.GroupBox1.Controls.Add(Me.txtCKBNAME)
        Me.GroupBox1.Controls.Add(Me.txtCCSNAME)
        Me.GroupBox1.Controls.Add(Me.txtCCSKANA)
        Me.GroupBox1.Controls.Add(Me.txtNCSNO)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(36, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1522, 245)
        Me.GroupBox1.TabIndex = 173
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Bisque
        Me.Panel1.Controls.Add(Me.txtPOINT)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtPREZANKN)
        Me.Panel1.Controls.Add(Me.txtZANKN)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(1167, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(313, 214)
        Me.Panel1.TabIndex = 183
        '
        'txtPOINT
        '
        Me.txtPOINT.BackColor = System.Drawing.SystemColors.Control
        Me.txtPOINT.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPOINT.Format = "#,##0"
        Me.txtPOINT.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPOINT.Location = New System.Drawing.Point(31, 163)
        Me.txtPOINT.MaxLength = 6
        Me.txtPOINT.Name = "txtPOINT"
        Me.txtPOINT.ReadOnly = True
        Me.txtPOINT.Size = New System.Drawing.Size(246, 34)
        Me.txtPOINT.TabIndex = 208
        Me.txtPOINT.TabStop = False
        Me.txtPOINT.Tag = ""
        Me.txtPOINT.Text = "999,999"
        Me.txtPOINT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(136, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(50, 21)
        Me.Label17.TabIndex = 207
        Me.Label17.Text = "（Ｐ）"
        '
        'txtPREZANKN
        '
        Me.txtPREZANKN.BackColor = System.Drawing.SystemColors.Control
        Me.txtPREZANKN.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPREZANKN.Format = "#,##0"
        Me.txtPREZANKN.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPREZANKN.Location = New System.Drawing.Point(31, 86)
        Me.txtPREZANKN.MaxLength = 6
        Me.txtPREZANKN.Name = "txtPREZANKN"
        Me.txtPREZANKN.ReadOnly = True
        Me.txtPREZANKN.Size = New System.Drawing.Size(246, 34)
        Me.txtPREZANKN.TabIndex = 206
        Me.txtPREZANKN.TabStop = False
        Me.txtPREZANKN.Tag = ""
        Me.txtPREZANKN.Text = "999,999"
        Me.txtPREZANKN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtZANKN
        '
        Me.txtZANKN.BackColor = System.Drawing.SystemColors.Control
        Me.txtZANKN.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZANKN.Format = "#,##0"
        Me.txtZANKN.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtZANKN.Location = New System.Drawing.Point(31, 53)
        Me.txtZANKN.MaxLength = 6
        Me.txtZANKN.Name = "txtZANKN"
        Me.txtZANKN.ReadOnly = True
        Me.txtZANKN.Size = New System.Drawing.Size(246, 34)
        Me.txtZANKN.TabIndex = 205
        Me.txtZANKN.TabStop = False
        Me.txtZANKN.Tag = ""
        Me.txtZANKN.Text = "999,999"
        Me.txtZANKN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(31, 130)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(246, 34)
        Me.Label7.TabIndex = 184
        Me.Label7.Text = "現在ポイント"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(31, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(246, 34)
        Me.Label6.TabIndex = 175
        Me.Label6.Text = "カード残金"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDBIRTH
        '
        Me.txtDBIRTH.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDBIRTH.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtDBIRTH.Location = New System.Drawing.Point(695, 101)
        Me.txtDBIRTH.MaxLength = 10
        Me.txtDBIRTH.Name = "txtDBIRTH"
        Me.txtDBIRTH.ReadOnly = True
        Me.txtDBIRTH.Size = New System.Drawing.Size(246, 34)
        Me.txtDBIRTH.TabIndex = 182
        Me.txtDBIRTH.TabStop = False
        Me.txtDBIRTH.Text = "00000000"
        '
        'txtDMEMBER
        '
        Me.txtDMEMBER.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDMEMBER.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtDMEMBER.Location = New System.Drawing.Point(695, 61)
        Me.txtDMEMBER.MaxLength = 10
        Me.txtDMEMBER.Name = "txtDMEMBER"
        Me.txtDMEMBER.ReadOnly = True
        Me.txtDMEMBER.Size = New System.Drawing.Size(246, 34)
        Me.txtDMEMBER.TabIndex = 181
        Me.txtDMEMBER.TabStop = False
        Me.txtDMEMBER.Text = "00000000"
        '
        'txtSCLMANNO
        '
        Me.txtSCLMANNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSCLMANNO.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtSCLMANNO.Location = New System.Drawing.Point(224, 198)
        Me.txtSCLMANNO.MaxLength = 10
        Me.txtSCLMANNO.Name = "txtSCLMANNO"
        Me.txtSCLMANNO.ReadOnly = True
        Me.txtSCLMANNO.Size = New System.Drawing.Size(246, 34)
        Me.txtSCLMANNO.TabIndex = 180
        Me.txtSCLMANNO.TabStop = False
        Me.txtSCLMANNO.Text = "00000000"
        Me.txtSCLMANNO.Visible = False
        '
        'txtCKBNAME
        '
        Me.txtCKBNAME.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCKBNAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCKBNAME.Location = New System.Drawing.Point(26, 148)
        Me.txtCKBNAME.MaxLength = 10
        Me.txtCKBNAME.Name = "txtCKBNAME"
        Me.txtCKBNAME.ReadOnly = True
        Me.txtCKBNAME.Size = New System.Drawing.Size(444, 34)
        Me.txtCKBNAME.TabIndex = 179
        Me.txtCKBNAME.TabStop = False
        Me.txtCKBNAME.Text = "顧客種別"
        '
        'txtCCSNAME
        '
        Me.txtCCSNAME.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCCSNAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCCSNAME.Location = New System.Drawing.Point(26, 94)
        Me.txtCCSNAME.MaxLength = 10
        Me.txtCCSNAME.Name = "txtCCSNAME"
        Me.txtCCSNAME.ReadOnly = True
        Me.txtCCSNAME.Size = New System.Drawing.Size(444, 34)
        Me.txtCCSNAME.TabIndex = 178
        Me.txtCCSNAME.TabStop = False
        Me.txtCCSNAME.Text = "読み仮名"
        '
        'txtCCSKANA
        '
        Me.txtCCSKANA.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCCSKANA.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCCSKANA.Location = New System.Drawing.Point(26, 61)
        Me.txtCCSKANA.MaxLength = 10
        Me.txtCCSKANA.Name = "txtCCSKANA"
        Me.txtCCSKANA.ReadOnly = True
        Me.txtCCSKANA.Size = New System.Drawing.Size(444, 34)
        Me.txtCCSKANA.TabIndex = 177
        Me.txtCCSKANA.TabStop = False
        Me.txtCCSKANA.Text = "ﾖﾐｶﾞﾅ"
        '
        'txtNCSNO
        '
        Me.txtNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNCSNO.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtNCSNO.Location = New System.Drawing.Point(224, 15)
        Me.txtNCSNO.MaxLength = 10
        Me.txtNCSNO.Name = "txtNCSNO"
        Me.txtNCSNO.ReadOnly = True
        Me.txtNCSNO.Size = New System.Drawing.Size(246, 34)
        Me.txtNCSNO.TabIndex = 176
        Me.txtNCSNO.TabStop = False
        Me.txtNCSNO.Text = "00000000"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(497, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(192, 34)
        Me.Label5.TabIndex = 175
        Me.Label5.Text = "誕生日"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(497, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(192, 34)
        Me.Label4.TabIndex = 174
        Me.Label4.Text = "会員期限"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(26, 198)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 34)
        Me.Label3.TabIndex = 173
        Me.Label3.Text = "スクール生番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(26, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 34)
        Me.Label2.TabIndex = 172
        Me.Label2.Text = "顧客番号"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnBACK)
        Me.GroupBox2.Controls.Add(Me.btnCLEAR)
        Me.GroupBox2.Controls.Add(Me.btn00)
        Me.GroupBox2.Controls.Add(Me.btn0)
        Me.GroupBox2.Controls.Add(Me.btn3)
        Me.GroupBox2.Controls.Add(Me.btn2)
        Me.GroupBox2.Controls.Add(Me.btn1)
        Me.GroupBox2.Controls.Add(Me.btn6)
        Me.GroupBox2.Controls.Add(Me.btn5)
        Me.GroupBox2.Controls.Add(Me.btn4)
        Me.GroupBox2.Controls.Add(Me.btn9)
        Me.GroupBox2.Controls.Add(Me.btn8)
        Me.GroupBox2.Controls.Add(Me.btn7)
        Me.GroupBox2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(754, 614)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(550, 388)
        Me.GroupBox2.TabIndex = 174
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "預かり金入力ボタン"
        '
        'btnBACK
        '
        Me.btnBACK.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ForeColor = System.Drawing.Color.Black
        Me.btnBACK.Location = New System.Drawing.Point(333, 130)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(185, 68)
        Me.btnBACK.TabIndex = 194
        Me.btnBACK.TabStop = False
        Me.btnBACK.Text = "BACK"
        Me.btnBACK.UseVisualStyleBackColor = False
        '
        'btnCLEAR
        '
        Me.btnCLEAR.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnCLEAR.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCLEAR.ForeColor = System.Drawing.Color.Black
        Me.btnCLEAR.Location = New System.Drawing.Point(333, 41)
        Me.btnCLEAR.Name = "btnCLEAR"
        Me.btnCLEAR.Size = New System.Drawing.Size(185, 68)
        Me.btnCLEAR.TabIndex = 193
        Me.btnCLEAR.TabStop = False
        Me.btnCLEAR.Text = "クリア"
        Me.btnCLEAR.UseVisualStyleBackColor = False
        '
        'btn00
        '
        Me.btn00.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn00.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn00.ForeColor = System.Drawing.Color.Black
        Me.btn00.Location = New System.Drawing.Point(123, 302)
        Me.btn00.Name = "btn00"
        Me.btn00.Size = New System.Drawing.Size(175, 68)
        Me.btn00.TabIndex = 192
        Me.btn00.TabStop = False
        Me.btn00.Text = "00"
        Me.btn00.UseVisualStyleBackColor = False
        '
        'btn0
        '
        Me.btn0.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn0.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn0.ForeColor = System.Drawing.Color.Black
        Me.btn0.Location = New System.Drawing.Point(26, 302)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(79, 68)
        Me.btn0.TabIndex = 191
        Me.btn0.TabStop = False
        Me.btn0.Text = "0"
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn3.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn3.ForeColor = System.Drawing.Color.Black
        Me.btn3.Location = New System.Drawing.Point(219, 217)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(79, 68)
        Me.btn3.TabIndex = 190
        Me.btn3.TabStop = False
        Me.btn3.Text = "3"
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn2.ForeColor = System.Drawing.Color.Black
        Me.btn2.Location = New System.Drawing.Point(123, 217)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(79, 68)
        Me.btn2.TabIndex = 189
        Me.btn2.TabStop = False
        Me.btn2.Text = "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn1.ForeColor = System.Drawing.Color.Black
        Me.btn1.Location = New System.Drawing.Point(26, 217)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(79, 68)
        Me.btn1.TabIndex = 188
        Me.btn1.TabStop = False
        Me.btn1.Text = "1"
        Me.btn1.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn6.ForeColor = System.Drawing.Color.Black
        Me.btn6.Location = New System.Drawing.Point(219, 130)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(79, 68)
        Me.btn6.TabIndex = 187
        Me.btn6.TabStop = False
        Me.btn6.Text = "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn5.ForeColor = System.Drawing.Color.Black
        Me.btn5.Location = New System.Drawing.Point(123, 130)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(79, 68)
        Me.btn5.TabIndex = 186
        Me.btn5.TabStop = False
        Me.btn5.Text = "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn4.ForeColor = System.Drawing.Color.Black
        Me.btn4.Location = New System.Drawing.Point(26, 130)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(79, 68)
        Me.btn4.TabIndex = 185
        Me.btn4.TabStop = False
        Me.btn4.Text = "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn9.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn9.ForeColor = System.Drawing.Color.Black
        Me.btn9.Location = New System.Drawing.Point(219, 41)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(79, 68)
        Me.btn9.TabIndex = 184
        Me.btn9.TabStop = False
        Me.btn9.Text = "9"
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn8.ForeColor = System.Drawing.Color.Black
        Me.btn8.Location = New System.Drawing.Point(123, 41)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(79, 68)
        Me.btn8.TabIndex = 183
        Me.btn8.TabStop = False
        Me.btn8.Text = "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn7.ForeColor = System.Drawing.Color.Black
        Me.btn7.Location = New System.Drawing.Point(26, 41)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(79, 68)
        Me.btn7.TabIndex = 182
        Me.btn7.TabStop = False
        Me.btn7.Text = "7"
        Me.btn7.UseVisualStyleBackColor = False
        '
        'txtDENNO
        '
        Me.txtDENNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDENNO.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtDENNO.Location = New System.Drawing.Point(377, 15)
        Me.txtDENNO.MaxLength = 10
        Me.txtDENNO.Name = "txtDENNO"
        Me.txtDENNO.ReadOnly = True
        Me.txtDENNO.Size = New System.Drawing.Size(148, 34)
        Me.txtDENNO.TabIndex = 177
        Me.txtDENNO.TabStop = False
        Me.txtDENNO.Text = "0000"
        '
        'chbRECEIPT
        '
        Me.chbRECEIPT.AutoSize = True
        Me.chbRECEIPT.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chbRECEIPT.ForeColor = System.Drawing.Color.DarkOrange
        Me.chbRECEIPT.Location = New System.Drawing.Point(552, 22)
        Me.chbRECEIPT.Name = "chbRECEIPT"
        Me.chbRECEIPT.Size = New System.Drawing.Size(195, 25)
        Me.chbRECEIPT.TabIndex = 178
        Me.chbRECEIPT.Text = "レシートを出力する"
        Me.chbRECEIPT.UseVisualStyleBackColor = True
        '
        'dgvGOODS
        '
        Me.dgvGOODS.AllowUserToAddRows = False
        Me.dgvGOODS.AllowUserToDeleteRows = False
        Me.dgvGOODS.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        Me.dgvGOODS.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvGOODS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvGOODS.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGOODS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvGOODS.ColumnHeadersHeight = 50
        Me.dgvGOODS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvGOODS.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GDSNAME, Me.GDSCOUNT, Me.GDSTAX, Me.GDSKIN})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGOODS.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvGOODS.EnableHeadersVisualStyles = False
        Me.dgvGOODS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvGOODS.Location = New System.Drawing.Point(36, 330)
        Me.dgvGOODS.MultiSelect = False
        Me.dgvGOODS.Name = "dgvGOODS"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGOODS.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvGOODS.RowHeadersVisible = False
        Me.dgvGOODS.RowHeadersWidth = 61
        Me.dgvGOODS.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvGOODS.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvGOODS.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvGOODS.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvGOODS.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvGOODS.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvGOODS.RowTemplate.Height = 40
        Me.dgvGOODS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvGOODS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGOODS.Size = New System.Drawing.Size(1268, 252)
        Me.dgvGOODS.TabIndex = 179
        '
        'GDSNAME
        '
        Me.GDSNAME.DataPropertyName = "GDSNAME"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GDSNAME.DefaultCellStyle = DataGridViewCellStyle3
        Me.GDSNAME.HeaderText = "商品名"
        Me.GDSNAME.MaxInputLength = 999
        Me.GDSNAME.Name = "GDSNAME"
        Me.GDSNAME.ReadOnly = True
        Me.GDSNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDSNAME.Width = 459
        '
        'GDSCOUNT
        '
        Me.GDSCOUNT.DataPropertyName = "GDSCOUNT"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "#,0"
        Me.GDSCOUNT.DefaultCellStyle = DataGridViewCellStyle4
        Me.GDSCOUNT.HeaderText = "数量"
        Me.GDSCOUNT.MaxInputLength = 99
        Me.GDSCOUNT.Name = "GDSCOUNT"
        Me.GDSCOUNT.ReadOnly = True
        Me.GDSCOUNT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDSCOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDSCOUNT.Width = 150
        '
        'GDSTAX
        '
        Me.GDSTAX.DataPropertyName = "GDSTAX"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GDSTAX.DefaultCellStyle = DataGridViewCellStyle5
        Me.GDSTAX.HeaderText = "消費税"
        Me.GDSTAX.MaxInputLength = 9999
        Me.GDSTAX.Name = "GDSTAX"
        Me.GDSTAX.ReadOnly = True
        Me.GDSTAX.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDSTAX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDSTAX.Width = 240
        '
        'GDSKIN
        '
        Me.GDSKIN.DataPropertyName = "GDSKIN"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "#,0"
        Me.GDSKIN.DefaultCellStyle = DataGridViewCellStyle6
        Me.GDSKIN.HeaderText = "合計金額"
        Me.GDSKIN.MaxInputLength = 99999
        Me.GDSKIN.Name = "GDSKIN"
        Me.GDSKIN.ReadOnly = True
        Me.GDSKIN.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDSKIN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDSKIN.Width = 400
        '
        'btnSEISAN
        '
        Me.btnSEISAN.BackColor = System.Drawing.Color.OrangeRed
        Me.btnSEISAN.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSEISAN.ForeColor = System.Drawing.Color.White
        Me.btnSEISAN.Location = New System.Drawing.Point(1331, 623)
        Me.btnSEISAN.Name = "btnSEISAN"
        Me.btnSEISAN.Size = New System.Drawing.Size(224, 189)
        Me.btnSEISAN.TabIndex = 5
        Me.btnSEISAN.Text = "精　算（F12）"
        Me.btnSEISAN.UseVisualStyleBackColor = False
        '
        'btnRETURN
        '
        Me.btnRETURN.BackColor = System.Drawing.Color.Orange
        Me.btnRETURN.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRETURN.ForeColor = System.Drawing.Color.Black
        Me.btnRETURN.Location = New System.Drawing.Point(1331, 946)
        Me.btnRETURN.Name = "btnRETURN"
        Me.btnRETURN.Size = New System.Drawing.Size(224, 56)
        Me.btnRETURN.TabIndex = 181
        Me.btnRETURN.Text = "戻る（F1）"
        Me.btnRETURN.UseVisualStyleBackColor = False
        '
        'chbCPAYKBN1
        '
        Me.chbCPAYKBN1.AutoSize = True
        Me.chbCPAYKBN1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chbCPAYKBN1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chbCPAYKBN1.Location = New System.Drawing.Point(1397, 330)
        Me.chbCPAYKBN1.Name = "chbCPAYKBN1"
        Me.chbCPAYKBN1.Size = New System.Drawing.Size(119, 25)
        Me.chbCPAYKBN1.TabIndex = 182
        Me.chbCPAYKBN1.TabStop = False
        Me.chbCPAYKBN1.Text = "カード払い"
        Me.chbCPAYKBN1.UseVisualStyleBackColor = True
        '
        'chbCPAYKBN2
        '
        Me.chbCPAYKBN2.AutoSize = True
        Me.chbCPAYKBN2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chbCPAYKBN2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chbCPAYKBN2.Location = New System.Drawing.Point(1397, 371)
        Me.chbCPAYKBN2.Name = "chbCPAYKBN2"
        Me.chbCPAYKBN2.Size = New System.Drawing.Size(95, 25)
        Me.chbCPAYKBN2.TabIndex = 183
        Me.chbCPAYKBN2.TabStop = False
        Me.chbCPAYKBN2.Text = "商品券"
        Me.chbCPAYKBN2.UseVisualStyleBackColor = True
        '
        'chbCPAYKBN3
        '
        Me.chbCPAYKBN3.AutoSize = True
        Me.chbCPAYKBN3.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chbCPAYKBN3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chbCPAYKBN3.Location = New System.Drawing.Point(1397, 412)
        Me.chbCPAYKBN3.Name = "chbCPAYKBN3"
        Me.chbCPAYKBN3.Size = New System.Drawing.Size(117, 25)
        Me.chbCPAYKBN3.TabIndex = 184
        Me.chbCPAYKBN3.TabStop = False
        Me.chbCPAYKBN3.Text = "銀行振込"
        Me.chbCPAYKBN3.UseVisualStyleBackColor = True
        '
        'lblGETPREMKN
        '
        Me.lblGETPREMKN.BackColor = System.Drawing.SystemColors.Highlight
        Me.lblGETPREMKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGETPREMKN.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGETPREMKN.ForeColor = System.Drawing.Color.White
        Me.lblGETPREMKN.Location = New System.Drawing.Point(46, 903)
        Me.lblGETPREMKN.Name = "lblGETPREMKN"
        Me.lblGETPREMKN.Size = New System.Drawing.Size(192, 34)
        Me.lblGETPREMKN.TabIndex = 185
        Me.lblGETPREMKN.Text = "取得プレミアム"
        Me.lblGETPREMKN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGETPOINT
        '
        Me.lblGETPOINT.BackColor = System.Drawing.SystemColors.Highlight
        Me.lblGETPOINT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGETPOINT.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGETPOINT.ForeColor = System.Drawing.Color.White
        Me.lblGETPOINT.Location = New System.Drawing.Point(46, 962)
        Me.lblGETPOINT.Name = "lblGETPOINT"
        Me.lblGETPOINT.Size = New System.Drawing.Size(192, 34)
        Me.lblGETPOINT.TabIndex = 187
        Me.lblGETPOINT.Text = "取得ポイント"
        Me.lblGETPOINT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnGETPREMKN_CLEAR
        '
        Me.btnGETPREMKN_CLEAR.BackColor = System.Drawing.Color.Gold
        Me.btnGETPREMKN_CLEAR.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnGETPREMKN_CLEAR.ForeColor = System.Drawing.Color.Black
        Me.btnGETPREMKN_CLEAR.Location = New System.Drawing.Point(510, 899)
        Me.btnGETPREMKN_CLEAR.Name = "btnGETPREMKN_CLEAR"
        Me.btnGETPREMKN_CLEAR.Size = New System.Drawing.Size(55, 45)
        Me.btnGETPREMKN_CLEAR.TabIndex = 189
        Me.btnGETPREMKN_CLEAR.TabStop = False
        Me.btnGETPREMKN_CLEAR.Text = "C"
        Me.btnGETPREMKN_CLEAR.UseVisualStyleBackColor = False
        '
        'btnGETPOINT_CLEAR
        '
        Me.btnGETPOINT_CLEAR.BackColor = System.Drawing.Color.Gold
        Me.btnGETPOINT_CLEAR.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnGETPOINT_CLEAR.ForeColor = System.Drawing.Color.Black
        Me.btnGETPOINT_CLEAR.Location = New System.Drawing.Point(510, 957)
        Me.btnGETPOINT_CLEAR.Name = "btnGETPOINT_CLEAR"
        Me.btnGETPOINT_CLEAR.Size = New System.Drawing.Size(55, 45)
        Me.btnGETPOINT_CLEAR.TabIndex = 190
        Me.btnGETPOINT_CLEAR.TabStop = False
        Me.btnGETPOINT_CLEAR.Text = "C"
        Me.btnGETPOINT_CLEAR.UseVisualStyleBackColor = False
        '
        'txtPAYMENT
        '
        Me.txtPAYMENT.BackColor = System.Drawing.SystemColors.Control
        Me.txtPAYMENT.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPAYMENT.Format = "#,##0"
        Me.txtPAYMENT.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPAYMENT.Location = New System.Drawing.Point(244, 658)
        Me.txtPAYMENT.MaxLength = 6
        Me.txtPAYMENT.Name = "txtPAYMENT"
        Me.txtPAYMENT.ReadOnly = True
        Me.txtPAYMENT.Size = New System.Drawing.Size(246, 55)
        Me.txtPAYMENT.TabIndex = 0
        Me.txtPAYMENT.TabStop = False
        Me.txtPAYMENT.Tag = ""
        Me.txtPAYMENT.Text = "99,999"
        Me.txtPAYMENT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.FloralWhite
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(502, 661)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 48)
        Me.Label12.TabIndex = 196
        Me.Label12.Text = "円"
        '
        'txtDEPOSIT
        '
        Me.txtDEPOSIT.BackColor = System.Drawing.SystemColors.Window
        Me.txtDEPOSIT.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDEPOSIT.Format = "#,##0"
        Me.txtDEPOSIT.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDEPOSIT.Location = New System.Drawing.Point(244, 722)
        Me.txtDEPOSIT.MaxLength = 6
        Me.txtDEPOSIT.Name = "txtDEPOSIT"
        Me.txtDEPOSIT.Size = New System.Drawing.Size(246, 55)
        Me.txtDEPOSIT.TabIndex = 1
        Me.txtDEPOSIT.Tag = ""
        Me.txtDEPOSIT.Text = "0"
        Me.txtDEPOSIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCHANGE
        '
        Me.txtCHANGE.BackColor = System.Drawing.SystemColors.Control
        Me.txtCHANGE.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCHANGE.Format = "#,##0"
        Me.txtCHANGE.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCHANGE.Location = New System.Drawing.Point(244, 787)
        Me.txtCHANGE.MaxLength = 6
        Me.txtCHANGE.Name = "txtCHANGE"
        Me.txtCHANGE.ReadOnly = True
        Me.txtCHANGE.Size = New System.Drawing.Size(246, 55)
        Me.txtCHANGE.TabIndex = 2
        Me.txtCHANGE.TabStop = False
        Me.txtCHANGE.Tag = ""
        Me.txtCHANGE.Text = "99,999"
        Me.txtCHANGE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FloralWhite
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(502, 729)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 48)
        Me.Label10.TabIndex = 199
        Me.Label10.Text = "円"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.FloralWhite
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(502, 794)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 48)
        Me.Label11.TabIndex = 200
        Me.Label11.Text = "円"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.OrangeRed
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(29, 656)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(209, 53)
        Me.Label13.TabIndex = 201
        Me.Label13.Text = "現　金"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.OrangeRed
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(29, 722)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(209, 53)
        Me.Label14.TabIndex = 202
        Me.Label14.Text = "預かり金"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.OrangeRed
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(29, 789)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(209, 53)
        Me.Label15.TabIndex = 203
        Me.Label15.Text = "釣　銭"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtGETPREMKN
        '
        Me.txtGETPREMKN.BackColor = System.Drawing.SystemColors.Window
        Me.txtGETPREMKN.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGETPREMKN.Format = "#,##0"
        Me.txtGETPREMKN.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtGETPREMKN.Location = New System.Drawing.Point(244, 902)
        Me.txtGETPREMKN.MaxLength = 5
        Me.txtGETPREMKN.Name = "txtGETPREMKN"
        Me.txtGETPREMKN.Size = New System.Drawing.Size(246, 34)
        Me.txtGETPREMKN.TabIndex = 3
        Me.txtGETPREMKN.Tag = ""
        Me.txtGETPREMKN.Text = "99,999"
        Me.txtGETPREMKN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGETPOINT
        '
        Me.txtGETPOINT.BackColor = System.Drawing.SystemColors.Window
        Me.txtGETPOINT.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGETPOINT.Format = "#,##0"
        Me.txtGETPOINT.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtGETPOINT.Location = New System.Drawing.Point(244, 961)
        Me.txtGETPOINT.MaxLength = 5
        Me.txtGETPOINT.Name = "txtGETPOINT"
        Me.txtGETPOINT.Size = New System.Drawing.Size(246, 34)
        Me.txtGETPOINT.TabIndex = 4
        Me.txtGETPOINT.Tag = ""
        Me.txtGETPOINT.Text = "99,999"
        Me.txtGETPOINT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmREGISTER01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FloralWhite
        Me.ClientSize = New System.Drawing.Size(1584, 1041)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtGETPOINT)
        Me.Controls.Add(Me.txtGETPREMKN)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtCHANGE)
        Me.Controls.Add(Me.txtDEPOSIT)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtPAYMENT)
        Me.Controls.Add(Me.btnGETPOINT_CLEAR)
        Me.Controls.Add(Me.btnGETPREMKN_CLEAR)
        Me.Controls.Add(Me.lblGETPOINT)
        Me.Controls.Add(Me.lblGETPREMKN)
        Me.Controls.Add(Me.chbCPAYKBN3)
        Me.Controls.Add(Me.chbCPAYKBN2)
        Me.Controls.Add(Me.chbCPAYKBN1)
        Me.Controls.Add(Me.btnRETURN)
        Me.Controls.Add(Me.btnSEISAN)
        Me.Controls.Add(Me.dgvGOODS)
        Me.Controls.Add(Me.chbRECEIPT)
        Me.Controls.Add(Me.txtDENNO)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label22)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmREGISTER01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "商品引き落とし情報表示"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvGOODS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDBIRTH As System.Windows.Forms.TextBox
    Friend WithEvents txtDMEMBER As System.Windows.Forms.TextBox
    Friend WithEvents txtSCLMANNO As System.Windows.Forms.TextBox
    Friend WithEvents txtCKBNAME As System.Windows.Forms.TextBox
    Friend WithEvents txtCCSNAME As System.Windows.Forms.TextBox
    Friend WithEvents txtCCSKANA As System.Windows.Forms.TextBox
    Friend WithEvents txtNCSNO As System.Windows.Forms.TextBox
    Friend WithEvents txtDENNO As System.Windows.Forms.TextBox
    Friend WithEvents chbRECEIPT As System.Windows.Forms.CheckBox
    Friend WithEvents dgvGOODS As BaseControl.CustomGridView
    Friend WithEvents btnSEISAN As System.Windows.Forms.Button
    Friend WithEvents btnRETURN As System.Windows.Forms.Button
    Friend WithEvents chbCPAYKBN1 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCPAYKBN2 As System.Windows.Forms.CheckBox
    Friend WithEvents chbCPAYKBN3 As System.Windows.Forms.CheckBox
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents btnCLEAR As System.Windows.Forms.Button
    Friend WithEvents btn00 As System.Windows.Forms.Button
    Friend WithEvents btn0 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents lblGETPREMKN As System.Windows.Forms.Label
    Friend WithEvents lblGETPOINT As System.Windows.Forms.Label
    Friend WithEvents btnGETPREMKN_CLEAR As System.Windows.Forms.Button
    Friend WithEvents btnGETPOINT_CLEAR As System.Windows.Forms.Button
    Friend WithEvents txtPAYMENT As BaseControl.CustomTextBoxNum
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtDEPOSIT As BaseControl.CustomTextBoxNum
    Friend WithEvents txtCHANGE As BaseControl.CustomTextBoxNum
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtGETPREMKN As BaseControl.CustomTextBoxNum
    Friend WithEvents txtGETPOINT As BaseControl.CustomTextBoxNum
    Friend WithEvents txtPOINT As BaseControl.CustomTextBoxNum
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPREZANKN As BaseControl.CustomTextBoxNum
    Friend WithEvents txtZANKN As BaseControl.CustomTextBoxNum
    Friend WithEvents GDSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GDSCOUNT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GDSTAX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GDSKIN As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
