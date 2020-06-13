<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCHARGEEx
    Inherits frmBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCHARGEEx))
        Me.timCharge = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.timBlinkText = New System.Windows.Forms.Timer(Me.components)
        Me.timAnimation = New System.Windows.Forms.Timer(Me.components)
        Me.btnPayBack = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnNKNKN01 = New System.Windows.Forms.Button()
        Me.lblCARDFEE2 = New System.Windows.Forms.Label()
        Me.lblCARDFEE = New System.Windows.Forms.Label()
        Me.lblBlinkText = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.pnlNKNKN01 = New System.Windows.Forms.Panel()
        Me.lbl1_2 = New System.Windows.Forms.Label()
        Me.lbl1_1 = New System.Windows.Forms.Label()
        Me.lblNKNKN01 = New System.Windows.Forms.Label()
        Me.lblPREMKN01 = New System.Windows.Forms.Label()
        Me.lblPOINT01 = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblInMoney = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblCARDFEE1 = New System.Windows.Forms.Label()
        Me.picCARDFEE = New System.Windows.Forms.PictureBox()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNKNKN01.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCARDFEE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timCharge
        '
        '
        'Timer2
        '
        Me.Timer2.Interval = 7000
        '
        'timBlinkText
        '
        Me.timBlinkText.Interval = 1000
        '
        'btnPayBack
        '
        Me.btnPayBack.Location = New System.Drawing.Point(125, 576)
        Me.btnPayBack.Name = "btnPayBack"
        Me.btnPayBack.Size = New System.Drawing.Size(75, 23)
        Me.btnPayBack.TabIndex = 208
        Me.btnPayBack.Text = "Button6"
        Me.btnPayBack.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.White
        Me.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 39.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.lblInfo.Location = New System.Drawing.Point(0, 295)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(1279, 167)
        Me.lblInfo.TabIndex = 90
        Me.lblInfo.Text = "紙 幣 確 認 中"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblInfo.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(431, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(146, 55)
        Me.Label5.TabIndex = 207
        Me.Label5.Text = "残ポイント"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(70, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(146, 55)
        Me.Label4.TabIndex = 206
        Me.Label4.Text = "カード残高"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNKNKN01
        '
        Me.btnNKNKN01.BackColor = System.Drawing.Color.Black
        Me.btnNKNKN01.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnNKNKN01.Location = New System.Drawing.Point(13, 570)
        Me.btnNKNKN01.Name = "btnNKNKN01"
        Me.btnNKNKN01.Size = New System.Drawing.Size(75, 23)
        Me.btnNKNKN01.TabIndex = 78
        Me.btnNKNKN01.Text = "Button1"
        Me.btnNKNKN01.UseVisualStyleBackColor = False
        '
        'lblCARDFEE2
        '
        Me.lblCARDFEE2.BackColor = System.Drawing.Color.White
        Me.lblCARDFEE2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCARDFEE2.ForeColor = System.Drawing.Color.Black
        Me.lblCARDFEE2.Location = New System.Drawing.Point(884, 399)
        Me.lblCARDFEE2.Name = "lblCARDFEE2"
        Me.lblCARDFEE2.Size = New System.Drawing.Size(74, 57)
        Me.lblCARDFEE2.TabIndex = 202
        Me.lblCARDFEE2.Text = "円"
        Me.lblCARDFEE2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCARDFEE2.Visible = False
        '
        'lblCARDFEE
        '
        Me.lblCARDFEE.BackColor = System.Drawing.Color.White
        Me.lblCARDFEE.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCARDFEE.ForeColor = System.Drawing.Color.Red
        Me.lblCARDFEE.Location = New System.Drawing.Point(553, 397)
        Me.lblCARDFEE.Name = "lblCARDFEE"
        Me.lblCARDFEE.Size = New System.Drawing.Size(338, 59)
        Me.lblCARDFEE.TabIndex = 201
        Me.lblCARDFEE.Text = "99,999"
        Me.lblCARDFEE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCARDFEE.Visible = False
        '
        'lblBlinkText
        '
        Me.lblBlinkText.BackColor = System.Drawing.Color.Transparent
        Me.lblBlinkText.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBlinkText.ForeColor = System.Drawing.Color.White
        Me.lblBlinkText.Location = New System.Drawing.Point(12, 172)
        Me.lblBlinkText.Name = "lblBlinkText"
        Me.lblBlinkText.Size = New System.Drawing.Size(1267, 55)
        Me.lblBlinkText.TabIndex = 76
        Me.lblBlinkText.Text = "お金を入れてください。"
        Me.lblBlinkText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnBack.Enabled = False
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.Location = New System.Drawing.Point(1016, 16)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(237, 76)
        Me.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnBack.TabIndex = 75
        Me.btnBack.TabStop = False
        '
        'lblChange
        '
        Me.lblChange.Location = New System.Drawing.Point(11, 490)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(242, 47)
        Me.lblChange.TabIndex = 74
        Me.lblChange.Text = "lblChange"
        Me.lblChange.Visible = False
        '
        'pnlNKNKN01
        '
        Me.pnlNKNKN01.BackColor = System.Drawing.Color.Transparent
        Me.pnlNKNKN01.BackgroundImage = CType(resources.GetObject("pnlNKNKN01.BackgroundImage"), System.Drawing.Image)
        Me.pnlNKNKN01.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlNKNKN01.Controls.Add(Me.lbl1_2)
        Me.pnlNKNKN01.Controls.Add(Me.lbl1_1)
        Me.pnlNKNKN01.Controls.Add(Me.lblNKNKN01)
        Me.pnlNKNKN01.Controls.Add(Me.lblPREMKN01)
        Me.pnlNKNKN01.Controls.Add(Me.lblPOINT01)
        Me.pnlNKNKN01.Location = New System.Drawing.Point(465, 506)
        Me.pnlNKNKN01.Name = "pnlNKNKN01"
        Me.pnlNKNKN01.Size = New System.Drawing.Size(340, 168)
        Me.pnlNKNKN01.TabIndex = 68
        Me.pnlNKNKN01.Visible = False
        '
        'lbl1_2
        '
        Me.lbl1_2.BackColor = System.Drawing.Color.Transparent
        Me.lbl1_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl1_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbl1_2.Location = New System.Drawing.Point(14, 111)
        Me.lbl1_2.Name = "lbl1_2"
        Me.lbl1_2.Size = New System.Drawing.Size(141, 31)
        Me.lbl1_2.TabIndex = 39
        Me.lbl1_2.Text = "ポイント"
        Me.lbl1_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl1_1
        '
        Me.lbl1_1.BackColor = System.Drawing.Color.Transparent
        Me.lbl1_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl1_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbl1_1.Location = New System.Drawing.Point(14, 80)
        Me.lbl1_1.Name = "lbl1_1"
        Me.lbl1_1.Size = New System.Drawing.Size(165, 31)
        Me.lbl1_1.TabIndex = 38
        Me.lbl1_1.Text = "プレミアムサービス"
        Me.lbl1_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNKNKN01
        '
        Me.lblNKNKN01.BackColor = System.Drawing.Color.Transparent
        Me.lblNKNKN01.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNKNKN01.ForeColor = System.Drawing.Color.White
        Me.lblNKNKN01.Location = New System.Drawing.Point(16, 6)
        Me.lblNKNKN01.Name = "lblNKNKN01"
        Me.lblNKNKN01.Size = New System.Drawing.Size(300, 68)
        Me.lblNKNKN01.TabIndex = 37
        Me.lblNKNKN01.Text = "￥99,999円"
        Me.lblNKNKN01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPREMKN01
        '
        Me.lblPREMKN01.BackColor = System.Drawing.Color.Transparent
        Me.lblPREMKN01.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPREMKN01.ForeColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblPREMKN01.Location = New System.Drawing.Point(162, 78)
        Me.lblPREMKN01.Name = "lblPREMKN01"
        Me.lblPREMKN01.Size = New System.Drawing.Size(141, 31)
        Me.lblPREMKN01.TabIndex = 33
        Me.lblPREMKN01.Text = "99,999円"
        Me.lblPREMKN01.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPOINT01
        '
        Me.lblPOINT01.BackColor = System.Drawing.Color.Transparent
        Me.lblPOINT01.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPOINT01.ForeColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblPOINT01.Location = New System.Drawing.Point(155, 111)
        Me.lblPOINT01.Name = "lblPOINT01"
        Me.lblPOINT01.Size = New System.Drawing.Size(141, 31)
        Me.lblPOINT01.TabIndex = 36
        Me.lblPOINT01.Text = "99,999Ｐ"
        Me.lblPOINT01.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPOINT01.Visible = False
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.Transparent
        Me.lblSRTPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.White
        Me.lblSRTPO.Location = New System.Drawing.Point(580, 16)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(188, 76)
        Me.lblSRTPO.TabIndex = 55
        Me.lblSRTPO.Text = "99,999P"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblZANKN.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.White
        Me.lblZANKN.Location = New System.Drawing.Point(216, 16)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(208, 76)
        Me.lblZANKN.TabIndex = 52
        Me.lblZANKN.Text = "99,999円"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(883, 307)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 57)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "円"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInMoney
        '
        Me.lblInMoney.BackColor = System.Drawing.Color.White
        Me.lblInMoney.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInMoney.ForeColor = System.Drawing.Color.Black
        Me.lblInMoney.Location = New System.Drawing.Point(552, 305)
        Me.lblInMoney.Name = "lblInMoney"
        Me.lblInMoney.Size = New System.Drawing.Size(338, 59)
        Me.lblInMoney.TabIndex = 48
        Me.lblInMoney.Text = "99,999"
        Me.lblInMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(310, 302)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(297, 63)
        Me.Label1.TabIndex = 205
        Me.Label1.Text = "投入金額"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(278, 295)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(725, 76)
        Me.PictureBox1.TabIndex = 204
        Me.PictureBox1.TabStop = False
        '
        'lblCARDFEE1
        '
        Me.lblCARDFEE1.BackColor = System.Drawing.Color.White
        Me.lblCARDFEE1.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCARDFEE1.ForeColor = System.Drawing.Color.Black
        Me.lblCARDFEE1.Location = New System.Drawing.Point(310, 395)
        Me.lblCARDFEE1.Name = "lblCARDFEE1"
        Me.lblCARDFEE1.Size = New System.Drawing.Size(297, 63)
        Me.lblCARDFEE1.TabIndex = 203
        Me.lblCARDFEE1.Text = "カード発行料"
        Me.lblCARDFEE1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCARDFEE1.Visible = False
        '
        'picCARDFEE
        '
        Me.picCARDFEE.BackColor = System.Drawing.Color.Transparent
        Me.picCARDFEE.BackgroundImage = CType(resources.GetObject("picCARDFEE.BackgroundImage"), System.Drawing.Image)
        Me.picCARDFEE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCARDFEE.Location = New System.Drawing.Point(278, 388)
        Me.picCARDFEE.Name = "picCARDFEE"
        Me.picCARDFEE.Size = New System.Drawing.Size(725, 76)
        Me.picCARDFEE.TabIndex = 200
        Me.picCARDFEE.TabStop = False
        Me.picCARDFEE.Visible = False
        '
        'frmCHARGEEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.btnPayBack)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnNKNKN01)
        Me.Controls.Add(Me.lblCARDFEE2)
        Me.Controls.Add(Me.lblCARDFEE)
        Me.Controls.Add(Me.lblBlinkText)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblChange)
        Me.Controls.Add(Me.pnlNKNKN01)
        Me.Controls.Add(Me.lblSRTPO)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblInMoney)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblCARDFEE1)
        Me.Controls.Add(Me.picCARDFEE)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCHARGEEx"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCHARGE"
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNKNKN01.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCARDFEE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents lblPREMKN01 As System.Windows.Forms.Label
    Friend WithEvents lblPOINT01 As System.Windows.Forms.Label
    Friend WithEvents timCharge As System.Windows.Forms.Timer
    Friend WithEvents lblInMoney As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents pnlNKNKN01 As System.Windows.Forms.Panel
    Friend WithEvents lblNKNKN01 As System.Windows.Forms.Label
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.PictureBox
    Friend WithEvents lblBlinkText As System.Windows.Forms.Label
    Friend WithEvents lbl1_2 As System.Windows.Forms.Label
    Friend WithEvents lbl1_1 As System.Windows.Forms.Label
    Friend WithEvents btnNKNKN01 As System.Windows.Forms.Button
    Friend WithEvents timBlinkText As System.Windows.Forms.Timer
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents timAnimation As System.Windows.Forms.Timer
    Friend WithEvents picCARDFEE As System.Windows.Forms.PictureBox
    Friend WithEvents lblCARDFEE2 As System.Windows.Forms.Label
    Friend WithEvents lblCARDFEE As System.Windows.Forms.Label
    Friend WithEvents lblCARDFEE1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnPayBack As System.Windows.Forms.Button
End Class
