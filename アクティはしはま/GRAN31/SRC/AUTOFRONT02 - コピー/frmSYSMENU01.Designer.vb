<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSYSMENU01
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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnRwReset = New System.Windows.Forms.Button()
        Me.btnMCH3000 = New System.Windows.Forms.Button()
        Me.btnAD1 = New System.Windows.Forms.Button()
        Me.cmbICR700Port = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbAD1Port = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkZANCLEARE = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbKBMAST = New System.Windows.Forms.ComboBox()
        Me.cmbMCH3000Port = New System.Windows.Forms.ComboBox()
        Me.btnUpdConfig = New System.Windows.Forms.Button()
        Me.chkFunction4 = New System.Windows.Forms.CheckBox()
        Me.chkFunction3 = New System.Windows.Forms.CheckBox()
        Me.chkFunction2 = New System.Windows.Forms.CheckBox()
        Me.chkFunction1 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rdoSYSTEMKBN99 = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkSLOT_CORRECTION_ENABLED = New System.Windows.Forms.CheckBox()
        Me.picSlot4 = New System.Windows.Forms.PictureBox()
        Me.picSlot3 = New System.Windows.Forms.PictureBox()
        Me.picSlot2 = New System.Windows.Forms.PictureBox()
        Me.picSlot1 = New System.Windows.Forms.PictureBox()
        Me.cmbSlotPoint4 = New System.Windows.Forms.ComboBox()
        Me.cmbSlotPer4 = New System.Windows.Forms.ComboBox()
        Me.chkMinigame = New System.Windows.Forms.CheckBox()
        Me.cmbSlotPoint3 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbSlotPer3 = New System.Windows.Forms.ComboBox()
        Me.chkEjectStopMode = New System.Windows.Forms.CheckBox()
        Me.cmbSlotPoint2 = New System.Windows.Forms.ComboBox()
        Me.cmbSlotPer2 = New System.Windows.Forms.ComboBox()
        Me.cmbSlotPoint1 = New System.Windows.Forms.ComboBox()
        Me.cmbSlotPer1 = New System.Windows.Forms.ComboBox()
        Me.rdoSYSTEMKBN1 = New System.Windows.Forms.RadioButton()
        Me.rdoSYSTEMKBN0 = New System.Windows.Forms.RadioButton()
        Me.btnNKNTRN = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        CType(Me.picSlot4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSlot3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSlot2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Brown
        Me.btnExit.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(71, 617)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(206, 111)
        Me.btnExit.TabIndex = 29
        Me.btnExit.Text = "電源断"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkGray
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1003, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(206, 111)
        Me.btnBack.TabIndex = 30
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnRwReset
        '
        Me.btnRwReset.BackColor = System.Drawing.Color.ForestGreen
        Me.btnRwReset.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRwReset.ForeColor = System.Drawing.Color.White
        Me.btnRwReset.Location = New System.Drawing.Point(71, 47)
        Me.btnRwReset.Name = "btnRwReset"
        Me.btnRwReset.Size = New System.Drawing.Size(206, 111)
        Me.btnRwReset.TabIndex = 32
        Me.btnRwReset.Text = "ICRWリセット"
        Me.btnRwReset.UseVisualStyleBackColor = False
        '
        'btnMCH3000
        '
        Me.btnMCH3000.BackColor = System.Drawing.Color.ForestGreen
        Me.btnMCH3000.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMCH3000.ForeColor = System.Drawing.Color.White
        Me.btnMCH3000.Location = New System.Drawing.Point(71, 167)
        Me.btnMCH3000.Name = "btnMCH3000"
        Me.btnMCH3000.Size = New System.Drawing.Size(206, 111)
        Me.btnMCH3000.TabIndex = 34
        Me.btnMCH3000.Text = "搬送ユニット情報"
        Me.btnMCH3000.UseVisualStyleBackColor = False
        '
        'btnAD1
        '
        Me.btnAD1.BackColor = System.Drawing.Color.ForestGreen
        Me.btnAD1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAD1.ForeColor = System.Drawing.Color.White
        Me.btnAD1.Location = New System.Drawing.Point(71, 287)
        Me.btnAD1.Name = "btnAD1"
        Me.btnAD1.Size = New System.Drawing.Size(206, 111)
        Me.btnAD1.TabIndex = 35
        Me.btnAD1.Text = "ビルバリ情報"
        Me.btnAD1.UseVisualStyleBackColor = False
        '
        'cmbICR700Port
        '
        Me.cmbICR700Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbICR700Port.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbICR700Port.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbICR700Port.FormattingEnabled = True
        Me.cmbICR700Port.Location = New System.Drawing.Point(296, 182)
        Me.cmbICR700Port.MaxDropDownItems = 99
        Me.cmbICR700Port.Name = "cmbICR700Port"
        Me.cmbICR700Port.Size = New System.Drawing.Size(217, 48)
        Me.cmbICR700Port.TabIndex = 1120
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.ForestGreen
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label26.ForeColor = System.Drawing.Color.White
        Me.Label26.Location = New System.Drawing.Point(18, 181)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(276, 49)
        Me.Label26.TabIndex = 1119
        Me.Label26.Text = "ICﾘｰﾀﾞｰﾗｲﾀｰ"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.ForestGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(18, 283)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(276, 49)
        Me.Label3.TabIndex = 1129
        Me.Label3.Text = "ビルバリ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbAD1Port
        '
        Me.cmbAD1Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAD1Port.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbAD1Port.Font = New System.Drawing.Font("MS UI Gothic", 30.0!)
        Me.cmbAD1Port.FormattingEnabled = True
        Me.cmbAD1Port.Location = New System.Drawing.Point(296, 284)
        Me.cmbAD1Port.MaxDropDownItems = 99
        Me.cmbAD1Port.Name = "cmbAD1Port"
        Me.cmbAD1Port.Size = New System.Drawing.Size(217, 48)
        Me.cmbAD1Port.TabIndex = 1132
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Tan
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkZANCLEARE)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmbKBMAST)
        Me.Panel1.Controls.Add(Me.cmbMCH3000Port)
        Me.Panel1.Controls.Add(Me.btnUpdConfig)
        Me.Panel1.Controls.Add(Me.chkFunction4)
        Me.Panel1.Controls.Add(Me.chkFunction3)
        Me.Panel1.Controls.Add(Me.chkFunction2)
        Me.Panel1.Controls.Add(Me.chkFunction1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.cmbICR700Port)
        Me.Panel1.Controls.Add(Me.cmbAD1Port)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(351, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(539, 740)
        Me.Panel1.TabIndex = 1136
        '
        'chkZANCLEARE
        '
        Me.chkZANCLEARE.AutoSize = True
        Me.chkZANCLEARE.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkZANCLEARE.Location = New System.Drawing.Point(223, 412)
        Me.chkZANCLEARE.Name = "chkZANCLEARE"
        Me.chkZANCLEARE.Size = New System.Drawing.Size(304, 38)
        Me.chkZANCLEARE.TabIndex = 1162
        Me.chkZANCLEARE.Text = "期限切れ残高クリア"
        Me.chkZANCLEARE.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(18, 619)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(509, 48)
        Me.Label12.TabIndex = 1137
        Me.Label12.Text = "発券設定"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.ForestGreen
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(18, 668)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(220, 40)
        Me.Label13.TabIndex = 1119
        Me.Label13.Text = "顧客種別"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.ForestGreen
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(18, 232)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 49)
        Me.Label1.TabIndex = 1160
        Me.Label1.Text = "ｶｰﾄﾞ搬送ﾕﾆｯﾄ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbKBMAST
        '
        Me.cmbKBMAST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKBMAST.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbKBMAST.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKBMAST.FormattingEnabled = True
        Me.cmbKBMAST.Location = New System.Drawing.Point(240, 669)
        Me.cmbKBMAST.MaxDropDownItems = 99
        Me.cmbKBMAST.Name = "cmbKBMAST"
        Me.cmbKBMAST.Size = New System.Drawing.Size(285, 39)
        Me.cmbKBMAST.TabIndex = 1120
        '
        'cmbMCH3000Port
        '
        Me.cmbMCH3000Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMCH3000Port.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMCH3000Port.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMCH3000Port.FormattingEnabled = True
        Me.cmbMCH3000Port.Location = New System.Drawing.Point(296, 233)
        Me.cmbMCH3000Port.MaxDropDownItems = 99
        Me.cmbMCH3000Port.Name = "cmbMCH3000Port"
        Me.cmbMCH3000Port.Size = New System.Drawing.Size(217, 48)
        Me.cmbMCH3000Port.TabIndex = 1161
        '
        'btnUpdConfig
        '
        Me.btnUpdConfig.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.btnUpdConfig.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnUpdConfig.ForeColor = System.Drawing.Color.White
        Me.btnUpdConfig.Location = New System.Drawing.Point(18, 14)
        Me.btnUpdConfig.Name = "btnUpdConfig"
        Me.btnUpdConfig.Size = New System.Drawing.Size(495, 76)
        Me.btnUpdConfig.TabIndex = 1138
        Me.btnUpdConfig.Text = "保存"
        Me.btnUpdConfig.UseVisualStyleBackColor = False
        '
        'chkFunction4
        '
        Me.chkFunction4.AutoSize = True
        Me.chkFunction4.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFunction4.Location = New System.Drawing.Point(23, 562)
        Me.chkFunction4.Name = "chkFunction4"
        Me.chkFunction4.Size = New System.Drawing.Size(168, 38)
        Me.chkFunction4.TabIndex = 1142
        Me.chkFunction4.Text = "ｶｰﾄﾞ発券"
        Me.chkFunction4.UseVisualStyleBackColor = True
        '
        'chkFunction3
        '
        Me.chkFunction3.AutoSize = True
        Me.chkFunction3.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFunction3.Location = New System.Drawing.Point(23, 512)
        Me.chkFunction3.Name = "chkFunction3"
        Me.chkFunction3.Size = New System.Drawing.Size(102, 38)
        Me.chkFunction3.TabIndex = 1141
        Me.chkFunction3.Text = "入金"
        Me.chkFunction3.UseVisualStyleBackColor = True
        '
        'chkFunction2
        '
        Me.chkFunction2.AutoSize = True
        Me.chkFunction2.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFunction2.Location = New System.Drawing.Point(23, 462)
        Me.chkFunction2.Name = "chkFunction2"
        Me.chkFunction2.Size = New System.Drawing.Size(179, 38)
        Me.chkFunction2.TabIndex = 1140
        Me.chkFunction2.Text = "カード確認"
        Me.chkFunction2.UseVisualStyleBackColor = True
        '
        'chkFunction1
        '
        Me.chkFunction1.AutoSize = True
        Me.chkFunction1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFunction1.Location = New System.Drawing.Point(23, 412)
        Me.chkFunction1.Name = "chkFunction1"
        Me.chkFunction1.Size = New System.Drawing.Size(143, 38)
        Me.chkFunction1.TabIndex = 1139
        Me.chkFunction1.Text = "ﾁｪｯｸｲﾝ"
        Me.chkFunction1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(18, 353)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(507, 51)
        Me.Label6.TabIndex = 1138
        Me.Label6.Text = "システム区分"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 25.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(18, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(495, 52)
        Me.Label5.TabIndex = 1137
        Me.Label5.Text = "通信ポート設定"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdoSYSTEMKBN99
        '
        Me.rdoSYSTEMKBN99.AutoSize = True
        Me.rdoSYSTEMKBN99.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoSYSTEMKBN99.Location = New System.Drawing.Point(185, 82)
        Me.rdoSYSTEMKBN99.Name = "rdoSYSTEMKBN99"
        Me.rdoSYSTEMKBN99.Size = New System.Drawing.Size(195, 44)
        Me.rdoSYSTEMKBN99.TabIndex = 1159
        Me.rdoSYSTEMKBN99.TabStop = True
        Me.rdoSYSTEMKBN99.Text = "営業中止"
        Me.rdoSYSTEMKBN99.UseVisualStyleBackColor = True
        Me.rdoSYSTEMKBN99.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(189, 361)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(220, 32)
        Me.Label8.TabIndex = 1158
        Me.Label8.Text = "※ミニゲームの当選確率を利用" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "　 料金に応じて変動させます。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label8.Visible = False
        '
        'chkSLOT_CORRECTION_ENABLED
        '
        Me.chkSLOT_CORRECTION_ENABLED.AutoSize = True
        Me.chkSLOT_CORRECTION_ENABLED.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSLOT_CORRECTION_ENABLED.Location = New System.Drawing.Point(186, 314)
        Me.chkSLOT_CORRECTION_ENABLED.Name = "chkSLOT_CORRECTION_ENABLED"
        Me.chkSLOT_CORRECTION_ENABLED.Size = New System.Drawing.Size(116, 44)
        Me.chkSLOT_CORRECTION_ENABLED.TabIndex = 1157
        Me.chkSLOT_CORRECTION_ENABLED.Text = "補正"
        Me.chkSLOT_CORRECTION_ENABLED.UseVisualStyleBackColor = True
        Me.chkSLOT_CORRECTION_ENABLED.Visible = False
        '
        'picSlot4
        '
        Me.picSlot4.BackColor = System.Drawing.Color.ForestGreen
        Me.picSlot4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picSlot4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSlot4.Location = New System.Drawing.Point(13, 661)
        Me.picSlot4.Name = "picSlot4"
        Me.picSlot4.Size = New System.Drawing.Size(276, 70)
        Me.picSlot4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picSlot4.TabIndex = 1155
        Me.picSlot4.TabStop = False
        Me.picSlot4.Visible = False
        '
        'picSlot3
        '
        Me.picSlot3.BackColor = System.Drawing.Color.ForestGreen
        Me.picSlot3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picSlot3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSlot3.Location = New System.Drawing.Point(13, 470)
        Me.picSlot3.Name = "picSlot3"
        Me.picSlot3.Size = New System.Drawing.Size(276, 70)
        Me.picSlot3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picSlot3.TabIndex = 1155
        Me.picSlot3.TabStop = False
        Me.picSlot3.Visible = False
        '
        'picSlot2
        '
        Me.picSlot2.BackColor = System.Drawing.Color.ForestGreen
        Me.picSlot2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picSlot2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSlot2.Location = New System.Drawing.Point(13, 278)
        Me.picSlot2.Name = "picSlot2"
        Me.picSlot2.Size = New System.Drawing.Size(276, 70)
        Me.picSlot2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picSlot2.TabIndex = 1155
        Me.picSlot2.TabStop = False
        Me.picSlot2.Visible = False
        '
        'picSlot1
        '
        Me.picSlot1.BackColor = System.Drawing.Color.ForestGreen
        Me.picSlot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picSlot1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSlot1.Location = New System.Drawing.Point(13, 88)
        Me.picSlot1.Name = "picSlot1"
        Me.picSlot1.Size = New System.Drawing.Size(276, 70)
        Me.picSlot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picSlot1.TabIndex = 1154
        Me.picSlot1.TabStop = False
        Me.picSlot1.Visible = False
        '
        'cmbSlotPoint4
        '
        Me.cmbSlotPoint4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPoint4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPoint4.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPoint4.FormattingEnabled = True
        Me.cmbSlotPoint4.Location = New System.Drawing.Point(13, 793)
        Me.cmbSlotPoint4.MaxDropDownItems = 99
        Me.cmbSlotPoint4.Name = "cmbSlotPoint4"
        Me.cmbSlotPoint4.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPoint4.TabIndex = 1156
        Me.cmbSlotPoint4.Visible = False
        '
        'cmbSlotPer4
        '
        Me.cmbSlotPer4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPer4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPer4.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPer4.FormattingEnabled = True
        Me.cmbSlotPer4.Location = New System.Drawing.Point(13, 734)
        Me.cmbSlotPer4.MaxDropDownItems = 99
        Me.cmbSlotPer4.Name = "cmbSlotPer4"
        Me.cmbSlotPer4.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPer4.TabIndex = 1155
        Me.cmbSlotPer4.Visible = False
        '
        'chkMinigame
        '
        Me.chkMinigame.AutoSize = True
        Me.chkMinigame.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMinigame.Location = New System.Drawing.Point(186, 264)
        Me.chkMinigame.Name = "chkMinigame"
        Me.chkMinigame.Size = New System.Drawing.Size(165, 44)
        Me.chkMinigame.TabIndex = 1153
        Me.chkMinigame.Text = "ﾐﾆｹﾞｰﾑ"
        Me.chkMinigame.UseVisualStyleBackColor = True
        Me.chkMinigame.Visible = False
        '
        'cmbSlotPoint3
        '
        Me.cmbSlotPoint3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPoint3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPoint3.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPoint3.FormattingEnabled = True
        Me.cmbSlotPoint3.Location = New System.Drawing.Point(13, 602)
        Me.cmbSlotPoint3.MaxDropDownItems = 99
        Me.cmbSlotPoint3.Name = "cmbSlotPoint3"
        Me.cmbSlotPoint3.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPoint3.TabIndex = 1152
        Me.cmbSlotPoint3.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(189, 185)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(225, 64)
        Me.Label7.TabIndex = 1143
        Me.Label7.Text = "※カードを排出せずに各処理を" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "　 続行するモードです。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " 　ただし対応する機能が有効に" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " 　なっていないと機能しません。"
        '
        'cmbSlotPer3
        '
        Me.cmbSlotPer3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPer3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPer3.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPer3.FormattingEnabled = True
        Me.cmbSlotPer3.Location = New System.Drawing.Point(13, 543)
        Me.cmbSlotPer3.MaxDropDownItems = 99
        Me.cmbSlotPer3.Name = "cmbSlotPer3"
        Me.cmbSlotPer3.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPer3.TabIndex = 1151
        Me.cmbSlotPer3.Visible = False
        '
        'chkEjectStopMode
        '
        Me.chkEjectStopMode.AutoSize = True
        Me.chkEjectStopMode.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkEjectStopMode.Location = New System.Drawing.Point(186, 141)
        Me.chkEjectStopMode.Name = "chkEjectStopMode"
        Me.chkEjectStopMode.Size = New System.Drawing.Size(234, 37)
        Me.chkEjectStopMode.TabIndex = 1140
        Me.chkEjectStopMode.Text = "カード排出停止"
        Me.chkEjectStopMode.UseVisualStyleBackColor = True
        Me.chkEjectStopMode.Visible = False
        '
        'cmbSlotPoint2
        '
        Me.cmbSlotPoint2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPoint2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPoint2.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPoint2.FormattingEnabled = True
        Me.cmbSlotPoint2.Location = New System.Drawing.Point(13, 411)
        Me.cmbSlotPoint2.MaxDropDownItems = 99
        Me.cmbSlotPoint2.Name = "cmbSlotPoint2"
        Me.cmbSlotPoint2.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPoint2.TabIndex = 1150
        Me.cmbSlotPoint2.Visible = False
        '
        'cmbSlotPer2
        '
        Me.cmbSlotPer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPer2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPer2.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPer2.FormattingEnabled = True
        Me.cmbSlotPer2.Location = New System.Drawing.Point(13, 351)
        Me.cmbSlotPer2.MaxDropDownItems = 99
        Me.cmbSlotPer2.Name = "cmbSlotPer2"
        Me.cmbSlotPer2.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPer2.TabIndex = 1149
        Me.cmbSlotPer2.Visible = False
        '
        'cmbSlotPoint1
        '
        Me.cmbSlotPoint1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPoint1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPoint1.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPoint1.FormattingEnabled = True
        Me.cmbSlotPoint1.Location = New System.Drawing.Point(13, 219)
        Me.cmbSlotPoint1.MaxDropDownItems = 99
        Me.cmbSlotPoint1.Name = "cmbSlotPoint1"
        Me.cmbSlotPoint1.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPoint1.TabIndex = 1148
        Me.cmbSlotPoint1.Visible = False
        '
        'cmbSlotPer1
        '
        Me.cmbSlotPer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlotPer1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSlotPer1.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSlotPer1.FormattingEnabled = True
        Me.cmbSlotPer1.Location = New System.Drawing.Point(13, 160)
        Me.cmbSlotPer1.MaxDropDownItems = 99
        Me.cmbSlotPer1.Name = "cmbSlotPer1"
        Me.cmbSlotPer1.Size = New System.Drawing.Size(276, 56)
        Me.cmbSlotPer1.TabIndex = 1147
        Me.cmbSlotPer1.Visible = False
        '
        'rdoSYSTEMKBN1
        '
        Me.rdoSYSTEMKBN1.AutoSize = True
        Me.rdoSYSTEMKBN1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoSYSTEMKBN1.Location = New System.Drawing.Point(185, 29)
        Me.rdoSYSTEMKBN1.Name = "rdoSYSTEMKBN1"
        Me.rdoSYSTEMKBN1.Size = New System.Drawing.Size(235, 44)
        Me.rdoSYSTEMKBN1.TabIndex = 1135
        Me.rdoSYSTEMKBN1.TabStop = True
        Me.rdoSYSTEMKBN1.Text = "自動受付機"
        Me.rdoSYSTEMKBN1.UseVisualStyleBackColor = True
        Me.rdoSYSTEMKBN1.Visible = False
        '
        'rdoSYSTEMKBN0
        '
        Me.rdoSYSTEMKBN0.AutoSize = True
        Me.rdoSYSTEMKBN0.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoSYSTEMKBN0.ForeColor = System.Drawing.Color.Yellow
        Me.rdoSYSTEMKBN0.Location = New System.Drawing.Point(29, 29)
        Me.rdoSYSTEMKBN0.Name = "rdoSYSTEMKBN0"
        Me.rdoSYSTEMKBN0.Size = New System.Drawing.Size(114, 44)
        Me.rdoSYSTEMKBN0.TabIndex = 1134
        Me.rdoSYSTEMKBN0.TabStop = True
        Me.rdoSYSTEMKBN0.Text = "ATM"
        Me.rdoSYSTEMKBN0.UseVisualStyleBackColor = True
        Me.rdoSYSTEMKBN0.Visible = False
        '
        'btnNKNTRN
        '
        Me.btnNKNTRN.BackColor = System.Drawing.Color.ForestGreen
        Me.btnNKNTRN.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNKNTRN.ForeColor = System.Drawing.Color.White
        Me.btnNKNTRN.Location = New System.Drawing.Point(71, 407)
        Me.btnNKNTRN.Name = "btnNKNTRN"
        Me.btnNKNTRN.Size = New System.Drawing.Size(206, 111)
        Me.btnNKNTRN.TabIndex = 1137
        Me.btnNKNTRN.Text = "入金履歴"
        Me.btnNKNTRN.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 30.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(13, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(276, 76)
        Me.Label11.TabIndex = 1153
        Me.Label11.Text = "ﾐﾆｹﾞｰﾑ設定"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label11.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.cmbSlotPer1)
        Me.Panel3.Controls.Add(Me.cmbSlotPoint1)
        Me.Panel3.Controls.Add(Me.cmbSlotPer2)
        Me.Panel3.Controls.Add(Me.cmbSlotPoint2)
        Me.Panel3.Controls.Add(Me.cmbSlotPer3)
        Me.Panel3.Controls.Add(Me.picSlot4)
        Me.Panel3.Controls.Add(Me.cmbSlotPoint3)
        Me.Panel3.Controls.Add(Me.picSlot3)
        Me.Panel3.Controls.Add(Me.cmbSlotPer4)
        Me.Panel3.Controls.Add(Me.picSlot2)
        Me.Panel3.Controls.Add(Me.cmbSlotPoint4)
        Me.Panel3.Controls.Add(Me.picSlot1)
        Me.Panel3.Location = New System.Drawing.Point(186, 530)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(77, 63)
        Me.Panel3.TabIndex = 1156
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rdoSYSTEMKBN1)
        Me.Panel4.Controls.Add(Me.rdoSYSTEMKBN0)
        Me.Panel4.Controls.Add(Me.chkEjectStopMode)
        Me.Panel4.Controls.Add(Me.rdoSYSTEMKBN99)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.chkMinigame)
        Me.Panel4.Controls.Add(Me.chkSLOT_CORRECTION_ENABLED)
        Me.Panel4.Location = New System.Drawing.Point(37, 528)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(104, 83)
        Me.Panel4.TabIndex = 1162
        '
        'frmSYSMENU01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.btnNKNTRN)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnAD1)
        Me.Controls.Add(Me.btnMCH3000)
        Me.Controls.Add(Me.btnRwReset)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmSYSMENU01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.picSlot4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSlot3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSlot2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSlot1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnRwReset As System.Windows.Forms.Button
    Friend WithEvents btnMCH3000 As System.Windows.Forms.Button
    Friend WithEvents btnAD1 As System.Windows.Forms.Button
    Friend WithEvents cmbICR700Port As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbAD1Port As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnUpdConfig As System.Windows.Forms.Button
    Friend WithEvents rdoSYSTEMKBN0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSYSTEMKBN1 As System.Windows.Forms.RadioButton
    Friend WithEvents chkFunction4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFunction3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFunction2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkFunction1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkEjectStopMode As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSlotPoint3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSlotPer3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSlotPoint2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSlotPer2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSlotPoint1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSlotPer1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnNKNTRN As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkMinigame As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSlotPoint4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSlotPer4 As System.Windows.Forms.ComboBox
    Friend WithEvents picSlot4 As System.Windows.Forms.PictureBox
    Friend WithEvents picSlot3 As System.Windows.Forms.PictureBox
    Friend WithEvents picSlot2 As System.Windows.Forms.PictureBox
    Friend WithEvents picSlot1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkSLOT_CORRECTION_ENABLED As System.Windows.Forms.CheckBox
    Friend WithEvents rdoSYSTEMKBN99 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMCH3000Port As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbKBMAST As System.Windows.Forms.ComboBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkZANCLEARE As System.Windows.Forms.CheckBox

End Class
