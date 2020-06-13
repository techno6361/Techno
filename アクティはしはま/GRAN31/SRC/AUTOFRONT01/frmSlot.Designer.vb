<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSlot
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSlot))
        Me.timSlot = New System.Windows.Forms.Timer(Me.components)
        Me.timStart = New System.Windows.Forms.Timer(Me.components)
        Me.timBlinkText = New System.Windows.Forms.Timer(Me.components)
        Me.timClose = New System.Windows.Forms.Timer(Me.components)
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblTotalPoint = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBonus2 = New System.Windows.Forms.Label()
        Me.lblBonus1 = New System.Windows.Forms.Label()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPoint4 = New System.Windows.Forms.Label()
        Me.picReelM4 = New System.Windows.Forms.PictureBox()
        Me.lblPoint3 = New System.Windows.Forms.Label()
        Me.picReelM3 = New System.Windows.Forms.PictureBox()
        Me.lblPoint2 = New System.Windows.Forms.Label()
        Me.picReelM2 = New System.Windows.Forms.PictureBox()
        Me.lblPoint1 = New System.Windows.Forms.Label()
        Me.picReelM1 = New System.Windows.Forms.PictureBox()
        Me.lblResult = New System.Windows.Forms.Label()
        Me.btnCharge = New System.Windows.Forms.PictureBox()
        Me.btnRetry = New System.Windows.Forms.PictureBox()
        Me.lblCoin = New System.Windows.Forms.Label()
        Me.lblDebug = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.PictureBox()
        Me.picReel3 = New System.Windows.Forms.PictureBox()
        Me.picReel2 = New System.Windows.Forms.PictureBox()
        Me.picReel1 = New System.Windows.Forms.PictureBox()
        Me.pnlInfo.SuspendLayout()
        CType(Me.picReelM4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picReelM3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picReelM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picReelM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRetry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picReel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picReel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picReel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timSlot
        '
        '
        'timStart
        '
        '
        'timBlinkText
        '
        '
        'timClose
        '
        '
        'pnlInfo
        '
        Me.pnlInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnlInfo.BackgroundImage = CType(resources.GetObject("pnlInfo.BackgroundImage"), System.Drawing.Image)
        Me.pnlInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlInfo.Controls.Add(Me.lblTotalPoint)
        Me.pnlInfo.Controls.Add(Me.Label2)
        Me.pnlInfo.Controls.Add(Me.lblBonus2)
        Me.pnlInfo.Controls.Add(Me.lblBonus1)
        Me.pnlInfo.Location = New System.Drawing.Point(206, 677)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(1667, 258)
        Me.pnlInfo.TabIndex = 201
        Me.pnlInfo.Visible = False
        '
        'lblTotalPoint
        '
        Me.lblTotalPoint.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPoint.ForeColor = System.Drawing.Color.Yellow
        Me.lblTotalPoint.Location = New System.Drawing.Point(702, 198)
        Me.lblTotalPoint.Name = "lblTotalPoint"
        Me.lblTotalPoint.Size = New System.Drawing.Size(277, 62)
        Me.lblTotalPoint.TabIndex = 203
        Me.lblTotalPoint.Text = "100ポイント"
        Me.lblTotalPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Yellow
        Me.Label2.Location = New System.Drawing.Point(493, 198)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(277, 62)
        Me.Label2.TabIndex = 202
        Me.Label2.Text = "合　  　　計 :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus2
        '
        Me.lblBonus2.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBonus2.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus2.Location = New System.Drawing.Point(488, 109)
        Me.lblBonus2.Name = "lblBonus2"
        Me.lblBonus2.Size = New System.Drawing.Size(1040, 62)
        Me.lblBonus2.TabIndex = 26
        Me.lblBonus2.Text = "100ポイント : ミニゲームボーナス"
        Me.lblBonus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus1
        '
        Me.lblBonus1.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBonus1.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus1.Location = New System.Drawing.Point(488, 50)
        Me.lblBonus1.Name = "lblBonus1"
        Me.lblBonus1.Size = New System.Drawing.Size(1040, 59)
        Me.lblBonus1.TabIndex = 24
        Me.lblBonus1.Text = "100ポイント : チェックアウトボーナス"
        Me.lblBonus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComment
        '
        Me.lblComment.BackColor = System.Drawing.Color.Transparent
        Me.lblComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComment.ForeColor = System.Drawing.Color.White
        Me.lblComment.Location = New System.Drawing.Point(298, 291)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(1361, 91)
        Me.lblComment.TabIndex = 200
        Me.lblComment.Text = "ハズレても退場ボーナスプレゼント！"
        Me.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(189, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 105)
        Me.Label1.TabIndex = 199
        Me.Label1.Text = "SLOT MACHINE"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPoint4
        '
        Me.lblPoint4.AutoSize = True
        Me.lblPoint4.BackColor = System.Drawing.Color.Transparent
        Me.lblPoint4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPoint4.ForeColor = System.Drawing.Color.White
        Me.lblPoint4.Location = New System.Drawing.Point(1253, 69)
        Me.lblPoint4.Name = "lblPoint4"
        Me.lblPoint4.Size = New System.Drawing.Size(97, 24)
        Me.lblPoint4.TabIndex = 198
        Me.lblPoint4.Text = "999ポイント"
        '
        'picReelM4
        '
        Me.picReelM4.BackColor = System.Drawing.Color.Transparent
        Me.picReelM4.Location = New System.Drawing.Point(1176, 44)
        Me.picReelM4.Name = "picReelM4"
        Me.picReelM4.Size = New System.Drawing.Size(71, 64)
        Me.picReelM4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReelM4.TabIndex = 197
        Me.picReelM4.TabStop = False
        '
        'lblPoint3
        '
        Me.lblPoint3.AutoSize = True
        Me.lblPoint3.BackColor = System.Drawing.Color.Transparent
        Me.lblPoint3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPoint3.ForeColor = System.Drawing.Color.White
        Me.lblPoint3.Location = New System.Drawing.Point(1051, 67)
        Me.lblPoint3.Name = "lblPoint3"
        Me.lblPoint3.Size = New System.Drawing.Size(97, 24)
        Me.lblPoint3.TabIndex = 196
        Me.lblPoint3.Text = "999ポイント"
        '
        'picReelM3
        '
        Me.picReelM3.BackColor = System.Drawing.Color.Transparent
        Me.picReelM3.Location = New System.Drawing.Point(974, 42)
        Me.picReelM3.Name = "picReelM3"
        Me.picReelM3.Size = New System.Drawing.Size(71, 64)
        Me.picReelM3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReelM3.TabIndex = 195
        Me.picReelM3.TabStop = False
        '
        'lblPoint2
        '
        Me.lblPoint2.AutoSize = True
        Me.lblPoint2.BackColor = System.Drawing.Color.Transparent
        Me.lblPoint2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPoint2.ForeColor = System.Drawing.Color.White
        Me.lblPoint2.Location = New System.Drawing.Point(849, 67)
        Me.lblPoint2.Name = "lblPoint2"
        Me.lblPoint2.Size = New System.Drawing.Size(97, 24)
        Me.lblPoint2.TabIndex = 194
        Me.lblPoint2.Text = "999ポイント"
        '
        'picReelM2
        '
        Me.picReelM2.BackColor = System.Drawing.Color.Transparent
        Me.picReelM2.Location = New System.Drawing.Point(772, 42)
        Me.picReelM2.Name = "picReelM2"
        Me.picReelM2.Size = New System.Drawing.Size(71, 64)
        Me.picReelM2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReelM2.TabIndex = 193
        Me.picReelM2.TabStop = False
        '
        'lblPoint1
        '
        Me.lblPoint1.AutoSize = True
        Me.lblPoint1.BackColor = System.Drawing.Color.Transparent
        Me.lblPoint1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPoint1.ForeColor = System.Drawing.Color.White
        Me.lblPoint1.Location = New System.Drawing.Point(640, 67)
        Me.lblPoint1.Name = "lblPoint1"
        Me.lblPoint1.Size = New System.Drawing.Size(97, 24)
        Me.lblPoint1.TabIndex = 192
        Me.lblPoint1.Text = "999ポイント"
        '
        'picReelM1
        '
        Me.picReelM1.BackColor = System.Drawing.Color.Transparent
        Me.picReelM1.Location = New System.Drawing.Point(563, 42)
        Me.picReelM1.Name = "picReelM1"
        Me.picReelM1.Size = New System.Drawing.Size(71, 64)
        Me.picReelM1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReelM1.TabIndex = 188
        Me.picReelM1.TabStop = False
        '
        'lblResult
        '
        Me.lblResult.BackColor = System.Drawing.Color.Transparent
        Me.lblResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResult.ForeColor = System.Drawing.Color.White
        Me.lblResult.Location = New System.Drawing.Point(300, 217)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(1361, 91)
        Me.lblResult.TabIndex = 185
        Me.lblResult.Text = "やったね。大当たり！！ +9999ptゲット！！"
        Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCharge
        '
        Me.btnCharge.BackColor = System.Drawing.Color.Transparent
        Me.btnCharge.Image = CType(resources.GetObject("btnCharge.Image"), System.Drawing.Image)
        Me.btnCharge.Location = New System.Drawing.Point(1496, 364)
        Me.btnCharge.Name = "btnCharge"
        Me.btnCharge.Size = New System.Drawing.Size(335, 100)
        Me.btnCharge.TabIndex = 181
        Me.btnCharge.TabStop = False
        Me.btnCharge.Visible = False
        '
        'btnRetry
        '
        Me.btnRetry.BackColor = System.Drawing.Color.Transparent
        Me.btnRetry.Image = CType(resources.GetObject("btnRetry.Image"), System.Drawing.Image)
        Me.btnRetry.Location = New System.Drawing.Point(1456, 134)
        Me.btnRetry.Name = "btnRetry"
        Me.btnRetry.Size = New System.Drawing.Size(335, 100)
        Me.btnRetry.TabIndex = 180
        Me.btnRetry.TabStop = False
        Me.btnRetry.Visible = False
        '
        'lblCoin
        '
        Me.lblCoin.BackColor = System.Drawing.Color.Transparent
        Me.lblCoin.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCoin.ForeColor = System.Drawing.Color.White
        Me.lblCoin.Location = New System.Drawing.Point(12, 353)
        Me.lblCoin.Name = "lblCoin"
        Me.lblCoin.Size = New System.Drawing.Size(91, 46)
        Me.lblCoin.TabIndex = 179
        Me.lblCoin.Text = "99"
        Me.lblCoin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCoin.Visible = False
        '
        'lblDebug
        '
        Me.lblDebug.AutoSize = True
        Me.lblDebug.BackColor = System.Drawing.Color.Transparent
        Me.lblDebug.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDebug.ForeColor = System.Drawing.Color.White
        Me.lblDebug.Location = New System.Drawing.Point(191, 1011)
        Me.lblDebug.Name = "lblDebug"
        Me.lblDebug.Size = New System.Drawing.Size(216, 35)
        Me.lblDebug.TabIndex = 177
        Me.lblDebug.Text = "当選:0 乱数:0"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.Location = New System.Drawing.Point(1443, 18)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(335, 100)
        Me.btnExit.TabIndex = 176
        Me.btnExit.TabStop = False
        '
        'picReel3
        '
        Me.picReel3.BackColor = System.Drawing.Color.Transparent
        Me.picReel3.Image = CType(resources.GetObject("picReel3.Image"), System.Drawing.Image)
        Me.picReel3.Location = New System.Drawing.Point(1176, 401)
        Me.picReel3.Name = "picReel3"
        Me.picReel3.Size = New System.Drawing.Size(256, 256)
        Me.picReel3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReel3.TabIndex = 3
        Me.picReel3.TabStop = False
        '
        'picReel2
        '
        Me.picReel2.BackColor = System.Drawing.Color.Transparent
        Me.picReel2.Image = CType(resources.GetObject("picReel2.Image"), System.Drawing.Image)
        Me.picReel2.InitialImage = Nothing
        Me.picReel2.Location = New System.Drawing.Point(871, 401)
        Me.picReel2.Name = "picReel2"
        Me.picReel2.Size = New System.Drawing.Size(256, 256)
        Me.picReel2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReel2.TabIndex = 2
        Me.picReel2.TabStop = False
        '
        'picReel1
        '
        Me.picReel1.BackColor = System.Drawing.Color.Transparent
        Me.picReel1.Image = CType(resources.GetObject("picReel1.Image"), System.Drawing.Image)
        Me.picReel1.Location = New System.Drawing.Point(563, 401)
        Me.picReel1.Name = "picReel1"
        Me.picReel1.Size = New System.Drawing.Size(256, 256)
        Me.picReel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picReel1.TabIndex = 1
        Me.picReel1.TabStop = False
        '
        'frmSlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblPoint4)
        Me.Controls.Add(Me.picReelM4)
        Me.Controls.Add(Me.lblPoint3)
        Me.Controls.Add(Me.picReelM3)
        Me.Controls.Add(Me.lblPoint2)
        Me.Controls.Add(Me.picReelM2)
        Me.Controls.Add(Me.lblPoint1)
        Me.Controls.Add(Me.picReelM1)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.btnCharge)
        Me.Controls.Add(Me.btnRetry)
        Me.Controls.Add(Me.lblCoin)
        Me.Controls.Add(Me.lblDebug)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.picReel3)
        Me.Controls.Add(Me.picReel2)
        Me.Controls.Add(Me.picReel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSlot"
        Me.Text = "frmDummy"
        Me.pnlInfo.ResumeLayout(False)
        CType(Me.picReelM4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picReelM3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picReelM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picReelM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRetry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picReel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picReel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picReel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.PictureBox
    Friend WithEvents timSlot As System.Windows.Forms.Timer
    Friend WithEvents lblDebug As System.Windows.Forms.Label
    Public WithEvents picReel1 As System.Windows.Forms.PictureBox
    Public WithEvents picReel2 As System.Windows.Forms.PictureBox
    Public WithEvents picReel3 As System.Windows.Forms.PictureBox
    Friend WithEvents timStart As System.Windows.Forms.Timer
    Friend WithEvents lblCoin As System.Windows.Forms.Label
    Friend WithEvents btnRetry As System.Windows.Forms.PictureBox
    Friend WithEvents btnCharge As System.Windows.Forms.PictureBox
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents timBlinkText As System.Windows.Forms.Timer
    Friend WithEvents timClose As System.Windows.Forms.Timer
    Friend WithEvents picReelM1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPoint1 As System.Windows.Forms.Label
    Friend WithEvents lblPoint2 As System.Windows.Forms.Label
    Friend WithEvents picReelM2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPoint3 As System.Windows.Forms.Label
    Friend WithEvents picReelM3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPoint4 As System.Windows.Forms.Label
    Friend WithEvents picReelM4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents lblBonus2 As System.Windows.Forms.Label
    Friend WithEvents lblBonus1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPoint As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
