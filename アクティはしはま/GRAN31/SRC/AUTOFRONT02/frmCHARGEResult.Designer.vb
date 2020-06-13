<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCHARGEResult
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCHARGEResult))
        Me.timSound = New System.Windows.Forms.Timer(Me.components)
        Me.timClose = New System.Windows.Forms.Timer(Me.components)
        Me.mCharge = New System.ComponentModel.BackgroundWorker()
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
        Me.timBlinkText = New System.Windows.Forms.Timer(Me.components)
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.timReceipt = New System.Windows.Forms.Timer(Me.components)
        Me.timSound2 = New System.Windows.Forms.Timer(Me.components)
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.btnCheckInOut = New System.Windows.Forms.PictureBox()
        Me.timBlinkText2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckInOut, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(1443, 19)
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
        Me.lblMSG2.Location = New System.Drawing.Point(0, 296)
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
        Me.lblBlinkText.Location = New System.Drawing.Point(0, 220)
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
        Me.lblChange.Location = New System.Drawing.Point(1251, 599)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(242, 57)
        Me.lblChange.TabIndex = 74
        Me.lblChange.Text = "99,999"
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.Transparent
        Me.lblSRTPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.White
        Me.lblSRTPO.Location = New System.Drawing.Point(758, 35)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(248, 76)
        Me.lblSRTPO.TabIndex = 55
        Me.lblSRTPO.Text = "99,999P"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblZANKN.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.White
        Me.lblZANKN.Location = New System.Drawing.Point(347, 35)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(255, 76)
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
        'timBlinkText
        '
        '
        'picImage
        '
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.Image = CType(resources.GetObject("picImage.Image"), System.Drawing.Image)
        Me.picImage.Location = New System.Drawing.Point(450, 720)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(1088, 205)
        Me.picImage.TabIndex = 177
        Me.picImage.TabStop = False
        '
        'timReceipt
        '
        '
        'timSound2
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
        Me.picStatus.TabIndex = 178
        Me.picStatus.TabStop = False
        '
        'btnCheckInOut
        '
        Me.btnCheckInOut.BackColor = System.Drawing.Color.Transparent
        Me.btnCheckInOut.Location = New System.Drawing.Point(1077, 20)
        Me.btnCheckInOut.Name = "btnCheckInOut"
        Me.btnCheckInOut.Size = New System.Drawing.Size(335, 100)
        Me.btnCheckInOut.TabIndex = 179
        Me.btnCheckInOut.TabStop = False
        '
        'timBlinkText2
        '
        '
        'frmCHARGEResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.btnCheckInOut)
        Me.Controls.Add(Me.picStatus)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.btnCancel)
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
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCHARGEResult"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmCHARGE"
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckInOut, System.ComponentModel.ISupportInitialize).EndInit()
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
End Class
