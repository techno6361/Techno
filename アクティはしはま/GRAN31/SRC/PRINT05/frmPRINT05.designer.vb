<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINT05
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
        Me.plDate1 = New System.Windows.Forms.Panel()
        Me.dtpEndSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.cmbTerm = New System.Windows.Forms.ComboBox()
        Me.dtpStaSEATDT = New System.Windows.Forms.DateTimePicker()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbSheet4 = New System.Windows.Forms.RadioButton()
        Me.rbSheet3 = New System.Windows.Forms.RadioButton()
        Me.rbSheet2 = New System.Windows.Forms.RadioButton()
        Me.rbSheet1 = New System.Windows.Forms.RadioButton()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblMANNO = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAGE = New System.Windows.Forms.Label()
        Me.lblADDRESS = New System.Windows.Forms.Label()
        Me.lblSEX = New System.Windows.Forms.Label()
        Me.cmbKSBKB = New System.Windows.Forms.ComboBox()
        Me.gbSEX = New System.Windows.Forms.GroupBox()
        Me.rbSEXOther = New System.Windows.Forms.RadioButton()
        Me.rbSEXFemale = New System.Windows.Forms.RadioButton()
        Me.rbSEXMale = New System.Windows.Forms.RadioButton()
        Me.rbSEXAll = New System.Windows.Forms.RadioButton()
        Me.txtStaNCSNO = New System.Windows.Forms.TextBox()
        Me.txtStaAGE = New System.Windows.Forms.TextBox()
        Me.txtCADDRESS = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEndAGE = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEndNCSNO = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.plDate2 = New System.Windows.Forms.Panel()
        Me.dtpEndSEATDT2 = New System.Windows.Forms.DateTimePicker()
        Me.cmbTerm2 = New System.Windows.Forms.ComboBox()
        Me.dtpStaSEATDT2 = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtEndENTNum = New System.Windows.Forms.TextBox()
        Me.txtStaENTNum = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtEndENTNum2 = New System.Windows.Forms.TextBox()
        Me.txtStaENTNum2 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtEndZougenNum = New System.Windows.Forms.TextBox()
        Me.txtStaZougenNum = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.plNum = New System.Windows.Forms.Panel()
        Me.btnEndZougen = New System.Windows.Forms.Button()
        Me.btnStaZougen = New System.Windows.Forms.Button()
        Me.pnlStatus = New BaseControl.StatusPanel
        Me.plDate1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbSEX.SuspendLayout()
        Me.plDate2.SuspendLayout()
        Me.plNum.SuspendLayout()
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
        Me.plDate1.Location = New System.Drawing.Point(23, 220)
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
        Me.GroupBox1.Controls.Add(Me.rbSheet4)
        Me.GroupBox1.Controls.Add(Me.rbSheet3)
        Me.GroupBox1.Controls.Add(Me.rbSheet2)
        Me.GroupBox1.Controls.Add(Me.rbSheet1)
        Me.GroupBox1.Location = New System.Drawing.Point(253, 123)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1222, 76)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'rbSheet4
        '
        Me.rbSheet4.AutoSize = True
        Me.rbSheet4.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSheet4.Location = New System.Drawing.Point(880, 27)
        Me.rbSheet4.Name = "rbSheet4"
        Me.rbSheet4.Size = New System.Drawing.Size(327, 31)
        Me.rbSheet4.TabIndex = 3
        Me.rbSheet4.Text = "期間来場者個人比較一覧"
        Me.rbSheet4.UseVisualStyleBackColor = True
        '
        'rbSheet3
        '
        Me.rbSheet3.AutoSize = True
        Me.rbSheet3.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSheet3.Location = New System.Drawing.Point(581, 27)
        Me.rbSheet3.Name = "rbSheet3"
        Me.rbSheet3.Size = New System.Drawing.Size(273, 31)
        Me.rbSheet3.TabIndex = 2
        Me.rbSheet3.Text = "期間来場者個人一覧"
        Me.rbSheet3.UseVisualStyleBackColor = True
        '
        'rbSheet2
        '
        Me.rbSheet2.AutoSize = True
        Me.rbSheet2.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSheet2.Location = New System.Drawing.Point(263, 27)
        Me.rbSheet2.Name = "rbSheet2"
        Me.rbSheet2.Size = New System.Drawing.Size(273, 31)
        Me.rbSheet2.TabIndex = 1
        Me.rbSheet2.Text = "期間来場者比較集計"
        Me.rbSheet2.UseVisualStyleBackColor = True
        '
        'rbSheet1
        '
        Me.rbSheet1.AutoSize = True
        Me.rbSheet1.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSheet1.Location = New System.Drawing.Point(15, 27)
        Me.rbSheet1.Name = "rbSheet1"
        Me.rbSheet1.Size = New System.Drawing.Size(219, 31)
        Me.rbSheet1.TabIndex = 0
        Me.rbSheet1.Text = "期間来場者集計"
        Me.rbSheet1.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(23, 129)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(212, 70)
        Me.Label22.TabIndex = 1077
        Me.Label22.Text = "帳票選択"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMANNO
        '
        Me.lblMANNO.BackColor = System.Drawing.Color.DarkGreen
        Me.lblMANNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMANNO.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMANNO.ForeColor = System.Drawing.Color.White
        Me.lblMANNO.Location = New System.Drawing.Point(23, 330)
        Me.lblMANNO.Name = "lblMANNO"
        Me.lblMANNO.Size = New System.Drawing.Size(212, 35)
        Me.lblMANNO.TabIndex = 1078
        Me.lblMANNO.Text = "顧客番号"
        Me.lblMANNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(23, 386)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(212, 35)
        Me.Label2.TabIndex = 1079
        Me.Label2.Text = "顧客種別"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAGE
        '
        Me.lblAGE.BackColor = System.Drawing.Color.DarkGreen
        Me.lblAGE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAGE.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAGE.ForeColor = System.Drawing.Color.White
        Me.lblAGE.Location = New System.Drawing.Point(22, 441)
        Me.lblAGE.Name = "lblAGE"
        Me.lblAGE.Size = New System.Drawing.Size(212, 35)
        Me.lblAGE.TabIndex = 1080
        Me.lblAGE.Text = "年齢"
        Me.lblAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblADDRESS
        '
        Me.lblADDRESS.BackColor = System.Drawing.Color.DarkGreen
        Me.lblADDRESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblADDRESS.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblADDRESS.ForeColor = System.Drawing.Color.White
        Me.lblADDRESS.Location = New System.Drawing.Point(22, 497)
        Me.lblADDRESS.Name = "lblADDRESS"
        Me.lblADDRESS.Size = New System.Drawing.Size(212, 35)
        Me.lblADDRESS.TabIndex = 1081
        Me.lblADDRESS.Text = "住所"
        Me.lblADDRESS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSEX
        '
        Me.lblSEX.BackColor = System.Drawing.Color.DarkGreen
        Me.lblSEX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSEX.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSEX.ForeColor = System.Drawing.Color.White
        Me.lblSEX.Location = New System.Drawing.Point(491, 386)
        Me.lblSEX.Name = "lblSEX"
        Me.lblSEX.Size = New System.Drawing.Size(212, 35)
        Me.lblSEX.TabIndex = 1082
        Me.lblSEX.Text = "性別"
        Me.lblSEX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbKSBKB
        '
        Me.cmbKSBKB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKSBKB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbKSBKB.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKSBKB.FormattingEnabled = True
        Me.cmbKSBKB.Items.AddRange(New Object() {""})
        Me.cmbKSBKB.Location = New System.Drawing.Point(241, 386)
        Me.cmbKSBKB.Name = "cmbKSBKB"
        Me.cmbKSBKB.Size = New System.Drawing.Size(243, 35)
        Me.cmbKSBKB.TabIndex = 20
        '
        'gbSEX
        '
        Me.gbSEX.Controls.Add(Me.rbSEXOther)
        Me.gbSEX.Controls.Add(Me.rbSEXFemale)
        Me.gbSEX.Controls.Add(Me.rbSEXMale)
        Me.gbSEX.Controls.Add(Me.rbSEXAll)
        Me.gbSEX.Location = New System.Drawing.Point(709, 378)
        Me.gbSEX.Name = "gbSEX"
        Me.gbSEX.Size = New System.Drawing.Size(374, 45)
        Me.gbSEX.TabIndex = 21
        Me.gbSEX.TabStop = False
        '
        'rbSEXOther
        '
        Me.rbSEXOther.AutoSize = True
        Me.rbSEXOther.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSEXOther.Location = New System.Drawing.Point(261, 13)
        Me.rbSEXOther.Name = "rbSEXOther"
        Me.rbSEXOther.Size = New System.Drawing.Size(70, 25)
        Me.rbSEXOther.TabIndex = 3
        Me.rbSEXOther.Text = "不明"
        Me.rbSEXOther.UseVisualStyleBackColor = True
        '
        'rbSEXFemale
        '
        Me.rbSEXFemale.AutoSize = True
        Me.rbSEXFemale.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSEXFemale.Location = New System.Drawing.Point(178, 13)
        Me.rbSEXFemale.Name = "rbSEXFemale"
        Me.rbSEXFemale.Size = New System.Drawing.Size(49, 25)
        Me.rbSEXFemale.TabIndex = 2
        Me.rbSEXFemale.Text = "女"
        Me.rbSEXFemale.UseVisualStyleBackColor = True
        '
        'rbSEXMale
        '
        Me.rbSEXMale.AutoSize = True
        Me.rbSEXMale.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSEXMale.Location = New System.Drawing.Point(106, 13)
        Me.rbSEXMale.Name = "rbSEXMale"
        Me.rbSEXMale.Size = New System.Drawing.Size(49, 25)
        Me.rbSEXMale.TabIndex = 1
        Me.rbSEXMale.Text = "男"
        Me.rbSEXMale.UseVisualStyleBackColor = True
        '
        'rbSEXAll
        '
        Me.rbSEXAll.AutoSize = True
        Me.rbSEXAll.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSEXAll.Location = New System.Drawing.Point(22, 13)
        Me.rbSEXAll.Name = "rbSEXAll"
        Me.rbSEXAll.Size = New System.Drawing.Size(65, 25)
        Me.rbSEXAll.TabIndex = 0
        Me.rbSEXAll.Text = "全て"
        Me.rbSEXAll.UseVisualStyleBackColor = True
        '
        'txtStaNCSNO
        '
        Me.txtStaNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaNCSNO.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaNCSNO.Location = New System.Drawing.Point(241, 331)
        Me.txtStaNCSNO.MaxLength = 8
        Me.txtStaNCSNO.Name = "txtStaNCSNO"
        Me.txtStaNCSNO.Size = New System.Drawing.Size(177, 34)
        Me.txtStaNCSNO.TabIndex = 10
        Me.txtStaNCSNO.Tag = ""
        Me.txtStaNCSNO.Text = "99999999"
        Me.txtStaNCSNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStaAGE
        '
        Me.txtStaAGE.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaAGE.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaAGE.Location = New System.Drawing.Point(240, 442)
        Me.txtStaAGE.MaxLength = 2
        Me.txtStaAGE.Name = "txtStaAGE"
        Me.txtStaAGE.Size = New System.Drawing.Size(68, 34)
        Me.txtStaAGE.TabIndex = 30
        Me.txtStaAGE.Tag = ""
        Me.txtStaAGE.Text = "99"
        Me.txtStaAGE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCADDRESS
        '
        Me.txtCADDRESS.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCADDRESS.Location = New System.Drawing.Point(240, 498)
        Me.txtCADDRESS.MaxLength = 25
        Me.txtCADDRESS.Name = "txtCADDRESS"
        Me.txtCADDRESS.Size = New System.Drawing.Size(570, 34)
        Me.txtCADDRESS.TabIndex = 40
        Me.txtCADDRESS.Text = "あああああああああああああああああああああああああ"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(428, 448)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 27)
        Me.Label8.TabIndex = 1090
        Me.Label8.Text = "歳"
        '
        'txtEndAGE
        '
        Me.txtEndAGE.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndAGE.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtEndAGE.Location = New System.Drawing.Point(359, 441)
        Me.txtEndAGE.MaxLength = 2
        Me.txtEndAGE.Name = "txtEndAGE"
        Me.txtEndAGE.Size = New System.Drawing.Size(63, 34)
        Me.txtEndAGE.TabIndex = 31
        Me.txtEndAGE.Tag = ""
        Me.txtEndAGE.Text = "99"
        Me.txtEndAGE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(314, 449)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 27)
        Me.Label9.TabIndex = 1092
        Me.Label9.Text = "～"
        '
        'txtEndNCSNO
        '
        Me.txtEndNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndNCSNO.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtEndNCSNO.Location = New System.Drawing.Point(469, 330)
        Me.txtEndNCSNO.MaxLength = 8
        Me.txtEndNCSNO.Name = "txtEndNCSNO"
        Me.txtEndNCSNO.Size = New System.Drawing.Size(177, 34)
        Me.txtEndNCSNO.TabIndex = 11
        Me.txtEndNCSNO.Tag = ""
        Me.txtEndNCSNO.Text = "99999999"
        Me.txtEndNCSNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(424, 334)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 27)
        Me.Label10.TabIndex = 1094
        Me.Label10.Text = "～"
        '
        'plDate2
        '
        Me.plDate2.BackColor = System.Drawing.Color.Moccasin
        Me.plDate2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plDate2.Controls.Add(Me.dtpEndSEATDT2)
        Me.plDate2.Controls.Add(Me.cmbTerm2)
        Me.plDate2.Controls.Add(Me.dtpStaSEATDT2)
        Me.plDate2.Controls.Add(Me.Label11)
        Me.plDate2.Controls.Add(Me.Label12)
        Me.plDate2.Controls.Add(Me.Label13)
        Me.plDate2.Enabled = False
        Me.plDate2.Location = New System.Drawing.Point(558, 220)
        Me.plDate2.Name = "plDate2"
        Me.plDate2.Size = New System.Drawing.Size(516, 83)
        Me.plDate2.TabIndex = 1
        '
        'dtpEndSEATDT2
        '
        Me.dtpEndSEATDT2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpEndSEATDT2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndSEATDT2.Location = New System.Drawing.Point(324, 5)
        Me.dtpEndSEATDT2.Name = "dtpEndSEATDT2"
        Me.dtpEndSEATDT2.Size = New System.Drawing.Size(178, 34)
        Me.dtpEndSEATDT2.TabIndex = 2
        '
        'cmbTerm2
        '
        Me.cmbTerm2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTerm2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbTerm2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTerm2.FormattingEnabled = True
        Me.cmbTerm2.Items.AddRange(New Object() {"任意入力", "今月", "前月", "今年", "前年"})
        Me.cmbTerm2.Location = New System.Drawing.Point(107, 42)
        Me.cmbTerm2.Name = "cmbTerm2"
        Me.cmbTerm2.Size = New System.Drawing.Size(395, 35)
        Me.cmbTerm2.TabIndex = 3
        Me.cmbTerm2.TabStop = False
        '
        'dtpStaSEATDT2
        '
        Me.dtpStaSEATDT2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpStaSEATDT2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStaSEATDT2.Location = New System.Drawing.Point(107, 5)
        Me.dtpStaSEATDT2.Name = "dtpStaSEATDT2"
        Me.dtpStaSEATDT2.Size = New System.Drawing.Size(178, 34)
        Me.dtpStaSEATDT2.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(4, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(102, 35)
        Me.Label11.TabIndex = 1013
        Me.Label11.Text = "期間"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(289, 13)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 20)
        Me.Label12.TabIndex = 1012
        Me.Label12.Text = "～"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(4, 5)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(102, 32)
        Me.Label13.TabIndex = 1010
        Me.Label13.Text = "来場日"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.DarkGreen
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(13, 17)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(212, 35)
        Me.Label14.TabIndex = 1096
        Me.Label14.Text = "来場回数"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(333, 25)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 27)
        Me.Label15.TabIndex = 1099
        Me.Label15.Text = "～"
        '
        'txtEndENTNum
        '
        Me.txtEndENTNum.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndENTNum.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtEndENTNum.Location = New System.Drawing.Point(378, 18)
        Me.txtEndENTNum.MaxLength = 5
        Me.txtEndENTNum.Name = "txtEndENTNum"
        Me.txtEndENTNum.Size = New System.Drawing.Size(96, 34)
        Me.txtEndENTNum.TabIndex = 51
        Me.txtEndENTNum.Tag = ""
        Me.txtEndENTNum.Text = "99999"
        Me.txtEndENTNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStaENTNum
        '
        Me.txtStaENTNum.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaENTNum.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaENTNum.Location = New System.Drawing.Point(231, 18)
        Me.txtStaENTNum.MaxLength = 5
        Me.txtStaENTNum.Name = "txtStaENTNum"
        Me.txtStaENTNum.Size = New System.Drawing.Size(96, 34)
        Me.txtStaENTNum.TabIndex = 50
        Me.txtStaENTNum.Tag = ""
        Me.txtStaENTNum.Text = "99999"
        Me.txtStaENTNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(480, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 27)
        Me.Label16.TabIndex = 1100
        Me.Label16.Text = "回"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(480, 76)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(39, 27)
        Me.Label17.TabIndex = 1105
        Me.Label17.Text = "回"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(333, 80)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(39, 27)
        Me.Label18.TabIndex = 1104
        Me.Label18.Text = "～"
        '
        'txtEndENTNum2
        '
        Me.txtEndENTNum2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndENTNum2.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtEndENTNum2.Location = New System.Drawing.Point(378, 73)
        Me.txtEndENTNum2.MaxLength = 5
        Me.txtEndENTNum2.Name = "txtEndENTNum2"
        Me.txtEndENTNum2.Size = New System.Drawing.Size(96, 34)
        Me.txtEndENTNum2.TabIndex = 61
        Me.txtEndENTNum2.Tag = ""
        Me.txtEndENTNum2.Text = "99"
        Me.txtEndENTNum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStaENTNum2
        '
        Me.txtStaENTNum2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaENTNum2.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaENTNum2.Location = New System.Drawing.Point(231, 73)
        Me.txtStaENTNum2.MaxLength = 5
        Me.txtStaENTNum2.Name = "txtStaENTNum2"
        Me.txtStaENTNum2.Size = New System.Drawing.Size(96, 34)
        Me.txtStaENTNum2.TabIndex = 60
        Me.txtStaENTNum2.Tag = ""
        Me.txtStaENTNum2.Text = "99"
        Me.txtStaENTNum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.DarkGreen
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(13, 72)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(212, 35)
        Me.Label19.TabIndex = 1101
        Me.Label19.Text = "比較来場回数"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(554, 129)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(39, 27)
        Me.Label20.TabIndex = 1110
        Me.Label20.Text = "回"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(369, 133)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(39, 27)
        Me.Label21.TabIndex = 1109
        Me.Label21.Text = "～"
        '
        'txtEndZougenNum
        '
        Me.txtEndZougenNum.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEndZougenNum.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtEndZougenNum.Location = New System.Drawing.Point(452, 126)
        Me.txtEndZougenNum.MaxLength = 5
        Me.txtEndZougenNum.Name = "txtEndZougenNum"
        Me.txtEndZougenNum.Size = New System.Drawing.Size(96, 34)
        Me.txtEndZougenNum.TabIndex = 71
        Me.txtEndZougenNum.Tag = ""
        Me.txtEndZougenNum.Text = "99"
        Me.txtEndZougenNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStaZougenNum
        '
        Me.txtStaZougenNum.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStaZougenNum.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtStaZougenNum.Location = New System.Drawing.Point(268, 126)
        Me.txtStaZougenNum.MaxLength = 5
        Me.txtStaZougenNum.Name = "txtStaZougenNum"
        Me.txtStaZougenNum.Size = New System.Drawing.Size(96, 34)
        Me.txtStaZougenNum.TabIndex = 70
        Me.txtStaZougenNum.Tag = ""
        Me.txtStaZougenNum.Text = "99"
        Me.txtStaZougenNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.DarkGreen
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.White
        Me.Label23.Location = New System.Drawing.Point(12, 125)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(212, 35)
        Me.Label23.TabIndex = 1106
        Me.Label23.Text = "増減回数"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plNum
        '
        Me.plNum.Controls.Add(Me.btnEndZougen)
        Me.plNum.Controls.Add(Me.btnStaZougen)
        Me.plNum.Controls.Add(Me.Label14)
        Me.plNum.Controls.Add(Me.Label20)
        Me.plNum.Controls.Add(Me.txtStaENTNum)
        Me.plNum.Controls.Add(Me.Label21)
        Me.plNum.Controls.Add(Me.txtEndENTNum)
        Me.plNum.Controls.Add(Me.txtEndZougenNum)
        Me.plNum.Controls.Add(Me.Label15)
        Me.plNum.Controls.Add(Me.txtStaZougenNum)
        Me.plNum.Controls.Add(Me.Label16)
        Me.plNum.Controls.Add(Me.Label23)
        Me.plNum.Controls.Add(Me.Label19)
        Me.plNum.Controls.Add(Me.Label17)
        Me.plNum.Controls.Add(Me.txtStaENTNum2)
        Me.plNum.Controls.Add(Me.Label18)
        Me.plNum.Controls.Add(Me.txtEndENTNum2)
        Me.plNum.Enabled = False
        Me.plNum.Location = New System.Drawing.Point(11, 538)
        Me.plNum.Name = "plNum"
        Me.plNum.Size = New System.Drawing.Size(692, 182)
        Me.plNum.TabIndex = 1111
        '
        'btnEndZougen
        '
        Me.btnEndZougen.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEndZougen.Location = New System.Drawing.Point(413, 127)
        Me.btnEndZougen.Name = "btnEndZougen"
        Me.btnEndZougen.Size = New System.Drawing.Size(33, 34)
        Me.btnEndZougen.TabIndex = 1114
        Me.btnEndZougen.TabStop = False
        Me.btnEndZougen.Text = "+"
        Me.btnEndZougen.UseVisualStyleBackColor = True
        '
        'btnStaZougen
        '
        Me.btnStaZougen.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnStaZougen.Location = New System.Drawing.Point(228, 126)
        Me.btnStaZougen.Name = "btnStaZougen"
        Me.btnStaZougen.Size = New System.Drawing.Size(34, 34)
        Me.btnStaZougen.TabIndex = 1113
        Me.btnStaZougen.TabStop = False
        Me.btnStaZougen.Text = "+"
        Me.btnStaZougen.UseVisualStyleBackColor = True
        '
        'pnlStatus
        '
        Me.pnlStatus.Count = 0
        Me.pnlStatus.CountVisible = True
        Me.pnlStatus.Location = New System.Drawing.Point(28, 773)
        Me.pnlStatus.MaxCount = 0
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(949, 195)
        Me.pnlStatus.TabIndex = 1112
        Me.pnlStatus.Title = "処理中…しばらくお待ちください。"
        '
        'frmPRINT05
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.plNum)
        Me.Controls.Add(Me.plDate2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtEndNCSNO)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtEndAGE)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCADDRESS)
        Me.Controls.Add(Me.txtStaAGE)
        Me.Controls.Add(Me.txtStaNCSNO)
        Me.Controls.Add(Me.gbSEX)
        Me.Controls.Add(Me.cmbKSBKB)
        Me.Controls.Add(Me.lblSEX)
        Me.Controls.Add(Me.lblADDRESS)
        Me.Controls.Add(Me.lblAGE)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblMANNO)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.plDate1)
        Me.Name = "frmPRINT05"
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.plDate1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.lblMANNO, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.lblAGE, 0)
        Me.Controls.SetChildIndex(Me.lblADDRESS, 0)
        Me.Controls.SetChildIndex(Me.lblSEX, 0)
        Me.Controls.SetChildIndex(Me.cmbKSBKB, 0)
        Me.Controls.SetChildIndex(Me.gbSEX, 0)
        Me.Controls.SetChildIndex(Me.txtStaNCSNO, 0)
        Me.Controls.SetChildIndex(Me.txtStaAGE, 0)
        Me.Controls.SetChildIndex(Me.txtCADDRESS, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtEndAGE, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.txtEndNCSNO, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.plDate2, 0)
        Me.Controls.SetChildIndex(Me.plNum, 0)
        Me.Controls.SetChildIndex(Me.pnlStatus, 0)
        Me.plDate1.ResumeLayout(False)
        Me.plDate1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbSEX.ResumeLayout(False)
        Me.gbSEX.PerformLayout()
        Me.plDate2.ResumeLayout(False)
        Me.plDate2.PerformLayout()
        Me.plNum.ResumeLayout(False)
        Me.plNum.PerformLayout()
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
    Friend WithEvents rbSheet3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSheet2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSheet1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblMANNO As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblAGE As System.Windows.Forms.Label
    Friend WithEvents lblADDRESS As System.Windows.Forms.Label
    Friend WithEvents lblSEX As System.Windows.Forms.Label
    Friend WithEvents rbSheet4 As System.Windows.Forms.RadioButton
    Friend WithEvents cmbKSBKB As System.Windows.Forms.ComboBox
    Friend WithEvents gbSEX As System.Windows.Forms.GroupBox
    Friend WithEvents rbSEXOther As System.Windows.Forms.RadioButton
    Friend WithEvents rbSEXFemale As System.Windows.Forms.RadioButton
    Friend WithEvents rbSEXMale As System.Windows.Forms.RadioButton
    Friend WithEvents rbSEXAll As System.Windows.Forms.RadioButton
    Friend WithEvents txtStaNCSNO As System.Windows.Forms.TextBox
    Friend WithEvents txtStaAGE As System.Windows.Forms.TextBox
    Friend WithEvents txtCADDRESS As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtEndAGE As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEndNCSNO As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents plDate2 As System.Windows.Forms.Panel
    Friend WithEvents dtpEndSEATDT2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTerm2 As System.Windows.Forms.ComboBox
    Friend WithEvents dtpStaSEATDT2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtEndENTNum As System.Windows.Forms.TextBox
    Friend WithEvents txtStaENTNum As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtEndENTNum2 As System.Windows.Forms.TextBox
    Friend WithEvents txtStaENTNum2 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtEndZougenNum As System.Windows.Forms.TextBox
    Friend WithEvents txtStaZougenNum As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents plNum As System.Windows.Forms.Panel
    Friend WithEvents btnEndZougen As System.Windows.Forms.Button
    Friend WithEvents btnStaZougen As System.Windows.Forms.Button
    Friend WithEvents pnlStatus As BaseControl.StatusPanel

End Class
