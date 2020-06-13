<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCHARGEResult02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCHARGEResult02))
        Me.timSound = New System.Windows.Forms.Timer(Me.components)
        Me.timClose = New System.Windows.Forms.Timer(Me.components)
        Me.mCharge = New System.ComponentModel.BackgroundWorker()
        Me.timBlinkText = New System.Windows.Forms.Timer(Me.components)
        Me.timReceipt = New System.Windows.Forms.Timer(Me.components)
        Me.timSound2 = New System.Windows.Forms.Timer(Me.components)
        Me.timBlinkText2 = New System.Windows.Forms.Timer(Me.components)
        Me.lblCCSNAME = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNCSNO = New System.Windows.Forms.Label()
        Me.btnReceipt = New System.Windows.Forms.PictureBox()
        Me.btnCheckInOut = New System.Windows.Forms.PictureBox()
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.lblDesipot = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMSG2 = New System.Windows.Forms.Label()
        Me.lblBlinkText = New System.Windows.Forms.Label()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.picCARDFEE = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.PictureBox()
        CType(Me.btnReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckInOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCARDFEE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timSound
        '
        Me.timSound.Interval = 5000
        '
        'timClose
        '
        Me.timClose.Interval = 5000
        '
        'timBlinkText
        '
        '
        'timReceipt
        '
        '
        'timSound2
        '
        '
        'timBlinkText2
        '
        '
        'lblCCSNAME
        '
        Me.lblCCSNAME.BackColor = System.Drawing.Color.Transparent
        Me.lblCCSNAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCCSNAME.ForeColor = System.Drawing.Color.White
        Me.lblCCSNAME.Location = New System.Drawing.Point(396, 339)
        Me.lblCCSNAME.Name = "lblCCSNAME"
        Me.lblCCSNAME.Size = New System.Drawing.Size(585, 61)
        Me.lblCCSNAME.TabIndex = 183
        Me.lblCCSNAME.Text = "ああああああああああ"
        Me.lblCCSNAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(12, 248)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(320, 61)
        Me.Label3.TabIndex = 182
        Me.Label3.Text = "【顧客番号】"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'lblNCSNO
        '
        Me.lblNCSNO.BackColor = System.Drawing.Color.Transparent
        Me.lblNCSNO.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNCSNO.ForeColor = System.Drawing.Color.Yellow
        Me.lblNCSNO.Location = New System.Drawing.Point(26, 339)
        Me.lblNCSNO.Name = "lblNCSNO"
        Me.lblNCSNO.Size = New System.Drawing.Size(248, 61)
        Me.lblNCSNO.TabIndex = 181
        Me.lblNCSNO.Text = "00000000"
        Me.lblNCSNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNCSNO.Visible = False
        '
        'btnReceipt
        '
        Me.btnReceipt.BackColor = System.Drawing.Color.Transparent
        Me.btnReceipt.Image = CType(resources.GetObject("btnReceipt.Image"), System.Drawing.Image)
        Me.btnReceipt.Location = New System.Drawing.Point(1278, 742)
        Me.btnReceipt.Name = "btnReceipt"
        Me.btnReceipt.Size = New System.Drawing.Size(335, 100)
        Me.btnReceipt.TabIndex = 180
        Me.btnReceipt.TabStop = False
        '
        'btnCheckInOut
        '
        Me.btnCheckInOut.BackColor = System.Drawing.Color.Transparent
        Me.btnCheckInOut.Image = CType(resources.GetObject("btnCheckInOut.Image"), System.Drawing.Image)
        Me.btnCheckInOut.Location = New System.Drawing.Point(361, 742)
        Me.btnCheckInOut.Name = "btnCheckInOut"
        Me.btnCheckInOut.Size = New System.Drawing.Size(335, 100)
        Me.btnCheckInOut.TabIndex = 179
        Me.btnCheckInOut.TabStop = False
        '
        'picStatus
        '
        Me.picStatus.BackColor = System.Drawing.Color.Transparent
        Me.picStatus.Image = CType(resources.GetObject("picStatus.Image"), System.Drawing.Image)
        Me.picStatus.Location = New System.Drawing.Point(36, 34)
        Me.picStatus.Name = "picStatus"
        Me.picStatus.Size = New System.Drawing.Size(362, 90)
        Me.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picStatus.TabIndex = 178
        Me.picStatus.TabStop = False
        Me.picStatus.Visible = False
        '
        'picImage
        '
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.Location = New System.Drawing.Point(426, 886)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(1088, 108)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picImage.TabIndex = 177
        Me.picImage.TabStop = False
        Me.picImage.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(828, 742)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(335, 100)
        Me.btnCancel.TabIndex = 176
        Me.btnCancel.TabStop = False
        '
        'lblDesipot
        '
        Me.lblDesipot.BackColor = System.Drawing.Color.White
        Me.lblDesipot.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDesipot.ForeColor = System.Drawing.Color.Black
        Me.lblDesipot.Location = New System.Drawing.Point(1155, 516)
        Me.lblDesipot.Name = "lblDesipot"
        Me.lblDesipot.Size = New System.Drawing.Size(338, 59)
        Me.lblDesipot.TabIndex = 80
        Me.lblDesipot.Text = "99,999"
        Me.lblDesipot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(1486, 597)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 57)
        Me.Label4.TabIndex = 79
        Me.Label4.Text = "円"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(1486, 517)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 57)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "円"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMSG2
        '
        Me.lblMSG2.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG2.ForeColor = System.Drawing.Color.White
        Me.lblMSG2.Location = New System.Drawing.Point(0, 263)
        Me.lblMSG2.Name = "lblMSG2"
        Me.lblMSG2.Size = New System.Drawing.Size(1920, 61)
        Me.lblMSG2.TabIndex = 77
        Me.lblMSG2.Text = "ご利用ありがとうございました。"
        Me.lblMSG2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBlinkText
        '
        Me.lblBlinkText.BackColor = System.Drawing.Color.Transparent
        Me.lblBlinkText.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBlinkText.ForeColor = System.Drawing.Color.White
        Me.lblBlinkText.Location = New System.Drawing.Point(0, 187)
        Me.lblBlinkText.Name = "lblBlinkText"
        Me.lblBlinkText.Size = New System.Drawing.Size(1920, 61)
        Me.lblBlinkText.TabIndex = 76
        Me.lblBlinkText.Text = "お取り忘れのないようご注意ください。"
        Me.lblBlinkText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblChange
        '
        Me.lblChange.BackColor = System.Drawing.Color.White
        Me.lblChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChange.Location = New System.Drawing.Point(1251, 597)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(242, 57)
        Me.lblChange.TabIndex = 74
        Me.lblChange.Text = "99,999"
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.Transparent
        Me.lblSRTPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.White
        Me.lblSRTPO.Location = New System.Drawing.Point(1152, 43)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(287, 76)
        Me.lblSRTPO.TabIndex = 55
        Me.lblSRTPO.Text = "99,999P"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblZANKN.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.White
        Me.lblZANKN.Location = New System.Drawing.Point(619, 43)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(307, 76)
        Me.lblZANKN.TabIndex = 52
        Me.lblZANKN.Text = "99,999円"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(1486, 434)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 57)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "円"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPrice
        '
        Me.lblPrice.BackColor = System.Drawing.Color.White
        Me.lblPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPrice.ForeColor = System.Drawing.Color.Black
        Me.lblPrice.Location = New System.Drawing.Point(1155, 433)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(338, 59)
        Me.lblPrice.TabIndex = 48
        Me.lblPrice.Text = "99,999"
        Me.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(949, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(198, 55)
        Me.Label5.TabIndex = 211
        Me.Label5.Text = "残ポイント"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(418, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(198, 55)
        Me.Label6.TabIndex = 210
        Me.Label6.Text = "カード残高"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picCARDFEE
        '
        Me.picCARDFEE.BackColor = System.Drawing.Color.Transparent
        Me.picCARDFEE.BackgroundImage = CType(resources.GetObject("picCARDFEE.BackgroundImage"), System.Drawing.Image)
        Me.picCARDFEE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCARDFEE.Location = New System.Drawing.Point(393, 426)
        Me.picCARDFEE.Name = "picCARDFEE"
        Me.picCARDFEE.Size = New System.Drawing.Size(1191, 76)
        Me.picCARDFEE.TabIndex = 212
        Me.picCARDFEE.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(393, 508)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1191, 76)
        Me.PictureBox1.TabIndex = 213
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(393, 590)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(1191, 76)
        Me.PictureBox2.TabIndex = 214
        Me.PictureBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(455, 433)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(297, 63)
        Me.Label7.TabIndex = 215
        Me.Label7.Text = "購入金額"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(455, 515)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(297, 63)
        Me.Label8.TabIndex = 216
        Me.Label8.Text = "投入金額"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(455, 597)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(297, 63)
        Me.Label9.TabIndex = 217
        Me.Label9.Text = "お つ り"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(828, 742)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(335, 100)
        Me.btnOK.TabIndex = 218
        Me.btnOK.TabStop = False
        Me.btnOK.Visible = False
        '
        'frmCHARGEResult02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblCCSNAME)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblNCSNO)
        Me.Controls.Add(Me.btnReceipt)
        Me.Controls.Add(Me.btnCheckInOut)
        Me.Controls.Add(Me.picStatus)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.lblDesipot)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMSG2)
        Me.Controls.Add(Me.lblBlinkText)
        Me.Controls.Add(Me.lblChange)
        Me.Controls.Add(Me.lblSRTPO)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblPrice)
        Me.Controls.Add(Me.picCARDFEE)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCHARGEResult02"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "n"
        CType(Me.btnReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckInOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCARDFEE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents timSound As System.Windows.Forms.Timer
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents lblBlinkText As System.Windows.Forms.Label
    Friend WithEvents lblMSG2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblDesipot As System.Windows.Forms.Label
    Friend WithEvents timClose As System.Windows.Forms.Timer
    Friend WithEvents mCharge As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents timBlinkText As System.Windows.Forms.Timer
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents timReceipt As System.Windows.Forms.Timer
    Friend WithEvents timSound2 As System.Windows.Forms.Timer
    Friend WithEvents picStatus As System.Windows.Forms.PictureBox
    Friend WithEvents btnCheckInOut As System.Windows.Forms.PictureBox
    Friend WithEvents timBlinkText2 As System.Windows.Forms.Timer
    Friend WithEvents btnReceipt As System.Windows.Forms.PictureBox
    Friend WithEvents lblNCSNO As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCCSNAME As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents picCARDFEE As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.PictureBox
End Class
