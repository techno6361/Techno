<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmINFOEx
    Inherits GRAN31.UI.frmBase

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmINFOEx))
        Me.tmClose = New System.Windows.Forms.Timer(Me.components)
        Me.lblNCSNO = New System.Windows.Forms.Label()
        Me.lblCCSNAME = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblDMEMBER = New System.Windows.Forms.Label()
        Me.lblZanTama2F = New System.Windows.Forms.Label()
        Me.lblTanka2F = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblZanTama1F = New System.Windows.Forms.Label()
        Me.lblTanka1F = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblPREZANKN = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblCOUNT = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.lblKINGAKU = New System.Windows.Forms.Label()
        Me.btnCharge = New System.Windows.Forms.PictureBox()
        Me.btnExit = New System.Windows.Forms.PictureBox()
        Me.btnCheckIn = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmClose
        '
        '
        'lblNCSNO
        '
        Me.lblNCSNO.BackColor = System.Drawing.Color.Transparent
        Me.lblNCSNO.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNCSNO.ForeColor = System.Drawing.Color.White
        Me.lblNCSNO.Location = New System.Drawing.Point(160, 9)
        Me.lblNCSNO.Name = "lblNCSNO"
        Me.lblNCSNO.Size = New System.Drawing.Size(416, 76)
        Me.lblNCSNO.TabIndex = 222
        Me.lblNCSNO.Text = "【顧客番号】00000000"
        Me.lblNCSNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCCSNAME
        '
        Me.lblCCSNAME.BackColor = System.Drawing.Color.Transparent
        Me.lblCCSNAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCCSNAME.ForeColor = System.Drawing.Color.White
        Me.lblCCSNAME.Location = New System.Drawing.Point(171, 115)
        Me.lblCCSNAME.Name = "lblCCSNAME"
        Me.lblCCSNAME.Size = New System.Drawing.Size(449, 76)
        Me.lblCCSNAME.TabIndex = 220
        Me.lblCCSNAME.Text = "テクノ・アシストああ 様"
        Me.lblCCSNAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(656, 114)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(179, 76)
        Me.Label8.TabIndex = 218
        Me.Label8.Text = "会員期限"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDMEMBER
        '
        Me.lblDMEMBER.BackColor = System.Drawing.Color.Transparent
        Me.lblDMEMBER.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDMEMBER.ForeColor = System.Drawing.Color.White
        Me.lblDMEMBER.Location = New System.Drawing.Point(831, 114)
        Me.lblDMEMBER.Name = "lblDMEMBER"
        Me.lblDMEMBER.Size = New System.Drawing.Size(279, 76)
        Me.lblDMEMBER.TabIndex = 216
        Me.lblDMEMBER.Text = "2019/01/01"
        Me.lblDMEMBER.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZanTama2F
        '
        Me.lblZanTama2F.BackColor = System.Drawing.Color.White
        Me.lblZanTama2F.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZanTama2F.ForeColor = System.Drawing.Color.Black
        Me.lblZanTama2F.Location = New System.Drawing.Point(835, 547)
        Me.lblZanTama2F.Name = "lblZanTama2F"
        Me.lblZanTama2F.Size = New System.Drawing.Size(138, 76)
        Me.lblZanTama2F.TabIndex = 215
        Me.lblZanTama2F.Text = "99,999"
        Me.lblZanTama2F.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTanka2F
        '
        Me.lblTanka2F.BackColor = System.Drawing.Color.White
        Me.lblTanka2F.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTanka2F.ForeColor = System.Drawing.Color.Black
        Me.lblTanka2F.Location = New System.Drawing.Point(677, 547)
        Me.lblTanka2F.Name = "lblTanka2F"
        Me.lblTanka2F.Size = New System.Drawing.Size(157, 76)
        Me.lblTanka2F.TabIndex = 214
        Me.lblTanka2F.Text = "1F 99.99円"
        Me.lblTanka2F.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(971, 547)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 76)
        Me.Label13.TabIndex = 213
        Me.Label13.Text = "球"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblZanTama1F
        '
        Me.lblZanTama1F.BackColor = System.Drawing.Color.White
        Me.lblZanTama1F.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZanTama1F.ForeColor = System.Drawing.Color.Black
        Me.lblZanTama1F.Location = New System.Drawing.Point(468, 547)
        Me.lblZanTama1F.Name = "lblZanTama1F"
        Me.lblZanTama1F.Size = New System.Drawing.Size(152, 76)
        Me.lblZanTama1F.TabIndex = 212
        Me.lblZanTama1F.Text = "99,999"
        Me.lblZanTama1F.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTanka1F
        '
        Me.lblTanka1F.BackColor = System.Drawing.Color.White
        Me.lblTanka1F.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTanka1F.ForeColor = System.Drawing.Color.Black
        Me.lblTanka1F.Location = New System.Drawing.Point(308, 547)
        Me.lblTanka1F.Name = "lblTanka1F"
        Me.lblTanka1F.Size = New System.Drawing.Size(162, 76)
        Me.lblTanka1F.TabIndex = 211
        Me.lblTanka1F.Text = "1F 99.99円"
        Me.lblTanka1F.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 29.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(991, 383)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 45)
        Me.Label10.TabIndex = 208
        Me.Label10.Text = "円"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPREZANKN
        '
        Me.lblPREZANKN.BackColor = System.Drawing.Color.White
        Me.lblPREZANKN.Font = New System.Drawing.Font("Microsoft Sans Serif", 29.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPREZANKN.ForeColor = System.Drawing.Color.Black
        Me.lblPREZANKN.Location = New System.Drawing.Point(772, 383)
        Me.lblPREZANKN.Name = "lblPREZANKN"
        Me.lblPREZANKN.Size = New System.Drawing.Size(219, 45)
        Me.lblPREZANKN.TabIndex = 207
        Me.lblPREZANKN.Text = "99,999"
        Me.lblPREZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 29.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(991, 312)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 45)
        Me.Label7.TabIndex = 206
        Me.Label7.Text = "円"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.White
        Me.lblZANKN.Font = New System.Drawing.Font("Microsoft Sans Serif", 29.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.Black
        Me.lblZANKN.Location = New System.Drawing.Point(772, 312)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(219, 45)
        Me.lblZANKN.TabIndex = 205
        Me.lblZANKN.Text = "99,999"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(404, 383)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(309, 45)
        Me.Label6.TabIndex = 204
        Me.Label6.Text = "プレミアムポイント残金額"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(592, 312)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(211, 45)
        Me.Label1.TabIndex = 202
        Me.Label1.Text = "残金額"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(623, 547)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 76)
        Me.Label5.TabIndex = 197
        Me.Label5.Text = "球"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCOUNT
        '
        Me.lblCOUNT.BackColor = System.Drawing.Color.White
        Me.lblCOUNT.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCOUNT.ForeColor = System.Drawing.Color.Black
        Me.lblCOUNT.Location = New System.Drawing.Point(884, 9)
        Me.lblCOUNT.Name = "lblCOUNT"
        Me.lblCOUNT.Size = New System.Drawing.Size(307, 76)
        Me.lblCOUNT.TabIndex = 196
        Me.lblCOUNT.Text = "99,999"
        Me.lblCOUNT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCOUNT.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(185, 547)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(152, 76)
        Me.Label4.TabIndex = 195
        Me.Label4.Text = "残球数"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(185, 449)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(248, 76)
        Me.Label3.TabIndex = 194
        Me.Label3.Text = "ポイント残高"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(185, 214)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(211, 76)
        Me.Label2.TabIndex = 193
        Me.Label2.Text = "カード残高"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(991, 449)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 76)
        Me.Label12.TabIndex = 192
        Me.Label12.Text = "Ｐ"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(991, 214)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 76)
        Me.Label9.TabIndex = 189
        Me.Label9.Text = "円"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.White
        Me.lblSRTPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.Black
        Me.lblSRTPO.Location = New System.Drawing.Point(684, 449)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(307, 76)
        Me.lblSRTPO.TabIndex = 183
        Me.lblSRTPO.Text = "99,999"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKINGAKU
        '
        Me.lblKINGAKU.BackColor = System.Drawing.Color.White
        Me.lblKINGAKU.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKINGAKU.ForeColor = System.Drawing.Color.Black
        Me.lblKINGAKU.Location = New System.Drawing.Point(684, 214)
        Me.lblKINGAKU.Name = "lblKINGAKU"
        Me.lblKINGAKU.Size = New System.Drawing.Size(307, 76)
        Me.lblKINGAKU.TabIndex = 179
        Me.lblKINGAKU.Text = "99,999"
        Me.lblKINGAKU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCharge
        '
        Me.btnCharge.BackColor = System.Drawing.Color.Transparent
        Me.btnCharge.BackgroundImage = CType(resources.GetObject("btnCharge.BackgroundImage"), System.Drawing.Image)
        Me.btnCharge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCharge.Location = New System.Drawing.Point(500, 655)
        Me.btnCharge.Name = "btnCharge"
        Me.btnCharge.Size = New System.Drawing.Size(245, 103)
        Me.btnCharge.TabIndex = 18
        Me.btnCharge.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.BackgroundImage = CType(resources.GetObject("btnExit.BackgroundImage"), System.Drawing.Image)
        Me.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExit.Location = New System.Drawing.Point(787, 655)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(245, 103)
        Me.btnExit.TabIndex = 17
        Me.btnExit.TabStop = False
        '
        'btnCheckIn
        '
        Me.btnCheckIn.BackColor = System.Drawing.Color.Transparent
        Me.btnCheckIn.BackgroundImage = CType(resources.GetObject("btnCheckIn.BackgroundImage"), System.Drawing.Image)
        Me.btnCheckIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCheckIn.Location = New System.Drawing.Point(212, 655)
        Me.btnCheckIn.Name = "btnCheckIn"
        Me.btnCheckIn.Size = New System.Drawing.Size(245, 103)
        Me.btnCheckIn.TabIndex = 16
        Me.btnCheckIn.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(168, 206)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(917, 92)
        Me.PictureBox1.TabIndex = 198
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(168, 441)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(917, 92)
        Me.PictureBox2.TabIndex = 199
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(169, 539)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(917, 92)
        Me.PictureBox3.TabIndex = 200
        Me.PictureBox3.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox6.BackgroundImage = CType(resources.GetObject("PictureBox6.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox6.Location = New System.Drawing.Point(167, 304)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(918, 63)
        Me.PictureBox6.TabIndex = 209
        Me.PictureBox6.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(168, 372)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(918, 63)
        Me.PictureBox4.TabIndex = 210
        Me.PictureBox4.TabStop = False
        '
        'frmINFOEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.lblNCSNO)
        Me.Controls.Add(Me.lblCCSNAME)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblDMEMBER)
        Me.Controls.Add(Me.lblZanTama2F)
        Me.Controls.Add(Me.lblTanka2F)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblZanTama1F)
        Me.Controls.Add(Me.lblTanka1F)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblPREZANKN)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblCOUNT)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblSRTPO)
        Me.Controls.Add(Me.lblKINGAKU)
        Me.Controls.Add(Me.btnCharge)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCheckIn)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox4)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmINFOEx"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMSGBOXEx"
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmClose As System.Windows.Forms.Timer
    Friend WithEvents btnCheckIn As System.Windows.Forms.PictureBox
    Friend WithEvents btnExit As System.Windows.Forms.PictureBox
    Friend WithEvents btnCharge As System.Windows.Forms.PictureBox
    Friend WithEvents lblKINGAKU As System.Windows.Forms.Label
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCOUNT As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblPREZANKN As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents lblTanka1F As System.Windows.Forms.Label
    Friend WithEvents lblZanTama1F As System.Windows.Forms.Label
    Friend WithEvents lblZanTama2F As System.Windows.Forms.Label
    Friend WithEvents lblTanka2F As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblDMEMBER As System.Windows.Forms.Label
    Friend WithEvents lblCCSNAME As System.Windows.Forms.Label
    Friend WithEvents lblNCSNO As System.Windows.Forms.Label
End Class
