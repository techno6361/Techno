<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCHECKRESULT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCHECKRESULT))
        Me.tmClose = New System.Windows.Forms.Timer(Me.components)
        Me.tmSound = New System.Windows.Forms.Timer(Me.components)
        Me.timBlinkText = New System.Windows.Forms.Timer(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCharge = New System.Windows.Forms.PictureBox()
        Me.pnlENTKIN = New System.Windows.Forms.Panel()
        Me.lblENTKINMSG = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblMSG2 = New System.Windows.Forms.Label()
        Me.lblMSG1 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblBonus1 = New System.Windows.Forms.Label()
        Me.lblBonus2 = New System.Windows.Forms.Label()
        Me.lblBonus3 = New System.Windows.Forms.Label()
        Me.lblBonus4 = New System.Windows.Forms.Label()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.lblBonus5 = New System.Windows.Forms.Label()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlENTKIN.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmClose
        '
        '
        'tmSound
        '
        '
        'timBlinkText
        '
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(396, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(137, 55)
        Me.Label5.TabIndex = 209
        Me.Label5.Text = "残ポイント"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(28, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(137, 55)
        Me.Label4.TabIndex = 208
        Me.Label4.Text = "カード残高"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCharge
        '
        Me.btnCharge.BackColor = System.Drawing.Color.Transparent
        Me.btnCharge.BackgroundImage = CType(resources.GetObject("btnCharge.BackgroundImage"), System.Drawing.Image)
        Me.btnCharge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCharge.Location = New System.Drawing.Point(1006, 148)
        Me.btnCharge.Name = "btnCharge"
        Me.btnCharge.Size = New System.Drawing.Size(249, 78)
        Me.btnCharge.TabIndex = 39
        Me.btnCharge.TabStop = False
        '
        'pnlENTKIN
        '
        Me.pnlENTKIN.BackColor = System.Drawing.Color.Transparent
        Me.pnlENTKIN.Controls.Add(Me.lblENTKINMSG)
        Me.pnlENTKIN.Controls.Add(Me.Label1)
        Me.pnlENTKIN.Controls.Add(Me.PictureBox1)
        Me.pnlENTKIN.Location = New System.Drawing.Point(240, 365)
        Me.pnlENTKIN.Name = "pnlENTKIN"
        Me.pnlENTKIN.Size = New System.Drawing.Size(758, 100)
        Me.pnlENTKIN.TabIndex = 37
        '
        'lblENTKINMSG
        '
        Me.lblENTKINMSG.BackColor = System.Drawing.Color.White
        Me.lblENTKINMSG.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblENTKINMSG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblENTKINMSG.Location = New System.Drawing.Point(399, 29)
        Me.lblENTKINMSG.Name = "lblENTKINMSG"
        Me.lblENTKINMSG.Size = New System.Drawing.Size(289, 44)
        Me.lblENTKINMSG.TabIndex = 35
        Me.lblENTKINMSG.Text = "￥99,999円"
        Me.lblENTKINMSG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(47, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 42)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "入場料"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(10, 14)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(701, 78)
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'lblMSG2
        '
        Me.lblMSG2.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG2.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG2.ForeColor = System.Drawing.Color.White
        Me.lblMSG2.Location = New System.Drawing.Point(117, 305)
        Me.lblMSG2.Name = "lblMSG2"
        Me.lblMSG2.Size = New System.Drawing.Size(1024, 61)
        Me.lblMSG2.TabIndex = 36
        Me.lblMSG2.Text = "ごゆっくりお楽しみください。"
        Me.lblMSG2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMSG1
        '
        Me.lblMSG1.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG1.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG1.ForeColor = System.Drawing.Color.White
        Me.lblMSG1.Location = New System.Drawing.Point(117, 241)
        Me.lblMSG1.Name = "lblMSG1"
        Me.lblMSG1.Size = New System.Drawing.Size(1024, 61)
        Me.lblMSG1.TabIndex = 35
        Me.lblMSG1.Text = "チェックインが完了しました。"
        Me.lblMSG1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.BackgroundImage = CType(resources.GetObject("btnBack.BackgroundImage"), System.Drawing.Image)
        Me.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBack.Location = New System.Drawing.Point(1019, 39)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(249, 78)
        Me.btnBack.TabIndex = 34
        Me.btnBack.TabStop = False
        '
        'pnlInfo
        '
        Me.pnlInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnlInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlInfo.Controls.Add(Me.lblBonus5)
        Me.pnlInfo.Controls.Add(Me.lblBonus1)
        Me.pnlInfo.Controls.Add(Me.lblBonus2)
        Me.pnlInfo.Controls.Add(Me.lblBonus3)
        Me.pnlInfo.Controls.Add(Me.lblBonus4)
        Me.pnlInfo.Location = New System.Drawing.Point(151, 483)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(930, 305)
        Me.pnlInfo.TabIndex = 30
        '
        'lblBonus1
        '
        Me.lblBonus1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus1.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus1.Location = New System.Drawing.Point(92, 79)
        Me.lblBonus1.Name = "lblBonus1"
        Me.lblBonus1.Size = New System.Drawing.Size(824, 59)
        Me.lblBonus1.TabIndex = 24
        Me.lblBonus1.Text = "100ポイント : チェックインボーナス"
        Me.lblBonus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus2
        '
        Me.lblBonus2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus2.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus2.Location = New System.Drawing.Point(92, 118)
        Me.lblBonus2.Name = "lblBonus2"
        Me.lblBonus2.Size = New System.Drawing.Size(824, 61)
        Me.lblBonus2.TabIndex = 23
        Me.lblBonus2.Text = "100ポイント : お誕生月ポイント"
        Me.lblBonus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus3
        '
        Me.lblBonus3.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus3.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus3.Location = New System.Drawing.Point(92, 157)
        Me.lblBonus3.Name = "lblBonus3"
        Me.lblBonus3.Size = New System.Drawing.Size(824, 62)
        Me.lblBonus3.TabIndex = 22
        Me.lblBonus3.Text = "100ポイント : お誕生日ボーナス"
        Me.lblBonus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus4
        '
        Me.lblBonus4.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus4.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus4.Location = New System.Drawing.Point(92, 200)
        Me.lblBonus4.Name = "lblBonus4"
        Me.lblBonus4.Size = New System.Drawing.Size(824, 62)
        Me.lblBonus4.TabIndex = 21
        Me.lblBonus4.Text = "100ポイント : 月間99回目来場ボーナスを獲得しました。"
        Me.lblBonus4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblZANKN.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.White
        Me.lblZANKN.Location = New System.Drawing.Point(171, 41)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(210, 76)
        Me.lblZANKN.TabIndex = 17
        Me.lblZANKN.Text = "99,999円"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.Transparent
        Me.lblSRTPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.White
        Me.lblSRTPO.Location = New System.Drawing.Point(538, 41)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(184, 76)
        Me.lblSRTPO.TabIndex = 20
        Me.lblSRTPO.Text = "99,999P"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBonus5
        '
        Me.lblBonus5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus5.ForeColor = System.Drawing.Color.Yellow
        Me.lblBonus5.Location = New System.Drawing.Point(92, 244)
        Me.lblBonus5.Name = "lblBonus5"
        Me.lblBonus5.Size = New System.Drawing.Size(824, 62)
        Me.lblBonus5.TabIndex = 25
        Me.lblBonus5.Text = "100ポイント : 月間99回目来場ボーナスを獲得しました。"
        Me.lblBonus5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmCHECKRESULT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnCharge)
        Me.Controls.Add(Me.pnlENTKIN)
        Me.Controls.Add(Me.lblMSG2)
        Me.Controls.Add(Me.lblMSG1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.lblSRTPO)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCHECKRESULT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCHECKIN"
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlENTKIN.ResumeLayout(False)
        Me.pnlENTKIN.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents lblBonus1 As System.Windows.Forms.Label
    Friend WithEvents lblBonus2 As System.Windows.Forms.Label
    Friend WithEvents lblBonus3 As System.Windows.Forms.Label
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents lblBonus4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnBack As System.Windows.Forms.PictureBox
    Friend WithEvents lblMSG1 As System.Windows.Forms.Label
    Friend WithEvents lblMSG2 As System.Windows.Forms.Label
    Friend WithEvents pnlENTKIN As System.Windows.Forms.Panel
    Friend WithEvents lblENTKINMSG As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmClose As System.Windows.Forms.Timer
    Friend WithEvents tmSound As System.Windows.Forms.Timer
    Friend WithEvents btnCharge As System.Windows.Forms.PictureBox
    Friend WithEvents timBlinkText As System.Windows.Forms.Timer
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblBonus5 As System.Windows.Forms.Label
End Class
