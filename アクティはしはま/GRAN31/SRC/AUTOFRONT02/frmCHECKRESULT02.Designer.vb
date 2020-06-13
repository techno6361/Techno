<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCHECKRESULT02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCHECKRESULT02))
        Me.pnlENTKIN = New System.Windows.Forms.Panel()
        Me.lblENTKINMSG = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblMSG2 = New System.Windows.Forms.Label()
        Me.lblMSG1 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.PictureBox()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBonus1 = New System.Windows.Forms.Label()
        Me.lblBonus2 = New System.Windows.Forms.Label()
        Me.lblBonus3 = New System.Windows.Forms.Label()
        Me.lblBonus4 = New System.Windows.Forms.Label()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.tmClose = New System.Windows.Forms.Timer(Me.components)
        Me.tmSound = New System.Windows.Forms.Timer(Me.components)
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.btnCharge = New System.Windows.Forms.PictureBox()
        Me.timBlinkText = New System.Windows.Forms.Timer(Me.components)
        Me.pnlENTKIN.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfo.SuspendLayout()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlENTKIN
        '
        Me.pnlENTKIN.BackColor = System.Drawing.Color.Transparent
        Me.pnlENTKIN.Controls.Add(Me.lblENTKINMSG)
        Me.pnlENTKIN.Controls.Add(Me.Label1)
        Me.pnlENTKIN.Controls.Add(Me.PictureBox1)
        Me.pnlENTKIN.Location = New System.Drawing.Point(301, 452)
        Me.pnlENTKIN.Name = "pnlENTKIN"
        Me.pnlENTKIN.Size = New System.Drawing.Size(1244, 100)
        Me.pnlENTKIN.TabIndex = 37
        '
        'lblENTKINMSG
        '
        Me.lblENTKINMSG.BackColor = System.Drawing.Color.White
        Me.lblENTKINMSG.Font = New System.Drawing.Font("游ゴシック", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblENTKINMSG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblENTKINMSG.Location = New System.Drawing.Point(689, 34)
        Me.lblENTKINMSG.Name = "lblENTKINMSG"
        Me.lblENTKINMSG.Size = New System.Drawing.Size(505, 44)
        Me.lblENTKINMSG.TabIndex = 35
        Me.lblENTKINMSG.Text = "￥99,999円"
        Me.lblENTKINMSG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("游ゴシック", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(63, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 48)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "入場料"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(26, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1188, 78)
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'lblMSG2
        '
        Me.lblMSG2.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG2.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG2.ForeColor = System.Drawing.Color.White
        Me.lblMSG2.Location = New System.Drawing.Point(0, 332)
        Me.lblMSG2.Name = "lblMSG2"
        Me.lblMSG2.Size = New System.Drawing.Size(1920, 61)
        Me.lblMSG2.TabIndex = 36
        Me.lblMSG2.Text = "ごゆっくりお楽しみください。"
        Me.lblMSG2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMSG1
        '
        Me.lblMSG1.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG1.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG1.ForeColor = System.Drawing.Color.White
        Me.lblMSG1.Location = New System.Drawing.Point(0, 254)
        Me.lblMSG1.Name = "lblMSG1"
        Me.lblMSG1.Size = New System.Drawing.Size(1920, 61)
        Me.lblMSG1.TabIndex = 35
        Me.lblMSG1.Text = "チェックインが完了しました。"
        Me.lblMSG1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.Location = New System.Drawing.Point(1443, 19)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(344, 103)
        Me.btnBack.TabIndex = 34
        Me.btnBack.TabStop = False
        '
        'pnlInfo
        '
        Me.pnlInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnlInfo.BackgroundImage = CType(resources.GetObject("pnlInfo.BackgroundImage"), System.Drawing.Image)
        Me.pnlInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlInfo.Controls.Add(Me.Label2)
        Me.pnlInfo.Controls.Add(Me.lblBonus1)
        Me.pnlInfo.Controls.Add(Me.lblBonus2)
        Me.pnlInfo.Controls.Add(Me.lblBonus3)
        Me.pnlInfo.Controls.Add(Me.lblBonus4)
        Me.pnlInfo.Location = New System.Drawing.Point(118, 617)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(1641, 386)
        Me.pnlInfo.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("游ゴシック", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.PaleGreen
        Me.Label2.Location = New System.Drawing.Point(679, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(780, 67)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "おめでとうございます！"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus1
        '
        Me.lblBonus1.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus1.ForeColor = System.Drawing.Color.White
        Me.lblBonus1.Location = New System.Drawing.Point(689, 134)
        Me.lblBonus1.Name = "lblBonus1"
        Me.lblBonus1.Size = New System.Drawing.Size(725, 44)
        Me.lblBonus1.TabIndex = 24
        Me.lblBonus1.Text = "チェックインで00ポイント付きます！"
        Me.lblBonus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus2
        '
        Me.lblBonus2.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus2.ForeColor = System.Drawing.Color.White
        Me.lblBonus2.Location = New System.Drawing.Point(689, 178)
        Me.lblBonus2.Name = "lblBonus2"
        Me.lblBonus2.Size = New System.Drawing.Size(725, 44)
        Me.lblBonus2.TabIndex = 23
        Me.lblBonus2.Text = "お誕生月です！00ポイント付きます！"
        Me.lblBonus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus3
        '
        Me.lblBonus3.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus3.ForeColor = System.Drawing.Color.White
        Me.lblBonus3.Location = New System.Drawing.Point(689, 225)
        Me.lblBonus3.Name = "lblBonus3"
        Me.lblBonus3.Size = New System.Drawing.Size(725, 44)
        Me.lblBonus3.TabIndex = 22
        Me.lblBonus3.Text = "お誕生日です！00ポイント付きます！"
        Me.lblBonus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBonus4
        '
        Me.lblBonus4.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBonus4.ForeColor = System.Drawing.Color.White
        Me.lblBonus4.Location = New System.Drawing.Point(689, 269)
        Me.lblBonus4.Name = "lblBonus4"
        Me.lblBonus4.Size = New System.Drawing.Size(941, 44)
        Me.lblBonus4.TabIndex = 21
        Me.lblBonus4.Text = "月間00回目の来場です。00ポイント付きます！"
        Me.lblBonus4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblZANKN.Font = New System.Drawing.Font("游ゴシック", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.White
        Me.lblZANKN.Location = New System.Drawing.Point(341, 36)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(256, 76)
        Me.lblZANKN.TabIndex = 17
        Me.lblZANKN.Text = "99,999円"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.Transparent
        Me.lblSRTPO.Font = New System.Drawing.Font("游ゴシック", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.White
        Me.lblSRTPO.Location = New System.Drawing.Point(754, 36)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(256, 76)
        Me.lblSRTPO.TabIndex = 20
        Me.lblSRTPO.Text = "99,999P"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmClose
        '
        '
        'tmSound
        '
        '
        'picStatus
        '
        Me.picStatus.BackColor = System.Drawing.Color.Transparent
        Me.picStatus.Image = CType(resources.GetObject("picStatus.Image"), System.Drawing.Image)
        Me.picStatus.Location = New System.Drawing.Point(24, 43)
        Me.picStatus.Name = "picStatus"
        Me.picStatus.Size = New System.Drawing.Size(171, 62)
        Me.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picStatus.TabIndex = 38
        Me.picStatus.TabStop = False
        '
        'btnCharge
        '
        Me.btnCharge.BackColor = System.Drawing.Color.Transparent
        Me.btnCharge.Image = CType(resources.GetObject("btnCharge.Image"), System.Drawing.Image)
        Me.btnCharge.Location = New System.Drawing.Point(1077, 19)
        Me.btnCharge.Name = "btnCharge"
        Me.btnCharge.Size = New System.Drawing.Size(344, 103)
        Me.btnCharge.TabIndex = 39
        Me.btnCharge.TabStop = False
        '
        'timBlinkText
        '
        '
        'frmCHECKRESULT02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.btnCharge)
        Me.Controls.Add(Me.picStatus)
        Me.Controls.Add(Me.pnlENTKIN)
        Me.Controls.Add(Me.lblMSG2)
        Me.Controls.Add(Me.lblMSG1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.lblSRTPO)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCHECKRESULT02"
        Me.Text = "frmCHECKIN"
        Me.pnlENTKIN.ResumeLayout(False)
        Me.pnlENTKIN.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfo.ResumeLayout(False)
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents lblBonus1 As System.Windows.Forms.Label
    Friend WithEvents lblBonus2 As System.Windows.Forms.Label
    Friend WithEvents lblBonus3 As System.Windows.Forms.Label
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents lblBonus4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnBack As System.Windows.Forms.PictureBox
    Friend WithEvents lblMSG1 As System.Windows.Forms.Label
    Friend WithEvents lblMSG2 As System.Windows.Forms.Label
    Friend WithEvents pnlENTKIN As System.Windows.Forms.Panel
    Friend WithEvents lblENTKINMSG As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmClose As System.Windows.Forms.Timer
    Friend WithEvents tmSound As System.Windows.Forms.Timer
    Friend WithEvents picStatus As System.Windows.Forms.PictureBox
    Friend WithEvents btnCharge As System.Windows.Forms.PictureBox
    Friend WithEvents timBlinkText As System.Windows.Forms.Timer
End Class
