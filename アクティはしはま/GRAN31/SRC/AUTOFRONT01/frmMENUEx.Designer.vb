<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenuEx
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenuEx))
        Me.timClearInfo = New System.Windows.Forms.Timer(Me.components)
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.timSYSMENU01 = New System.Windows.Forms.Timer(Me.components)
        Me.picSYSMENU01 = New System.Windows.Forms.PictureBox()
        Me.lblPrinterError = New System.Windows.Forms.Label()
        Me.lblCardDispenserError = New System.Windows.Forms.Label()
        Me.lblAd1Error = New System.Windows.Forms.Label()
        Me.lblChangeError = New System.Windows.Forms.Label()
        Me.timRead = New System.Windows.Forms.Timer(Me.components)
        Me.btnCheckIn = New System.Windows.Forms.PictureBox()
        Me.btnInfo = New System.Windows.Forms.PictureBox()
        Me.btnCharge = New System.Windows.Forms.PictureBox()
        Me.btnCardDispenser = New System.Windows.Forms.PictureBox()
        Me.lblTimeCoron = New System.Windows.Forms.Label()
        Me.lblTime2 = New System.Windows.Forms.Label()
        Me.lblTime1 = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.mClock = New System.ComponentModel.BackgroundWorker()
        Me.btnSlotTest = New System.Windows.Forms.Button()
        Me.lblRWError = New System.Windows.Forms.Label()
        Me.lblCMError = New System.Windows.Forms.Label()
        CType(Me.picSYSMENU01, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCardDispenser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timClearInfo
        '
        Me.timClearInfo.Interval = 2000
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.White
        Me.lblMessage.Location = New System.Drawing.Point(108, 238)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(1405, 76)
        Me.lblMessage.TabIndex = 30
        Me.lblMessage.Text = "ご希望のボタンをタッチしてください。"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(1720, 1055)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(199, 23)
        Me.ProgressBar1.TabIndex = 31
        Me.ProgressBar1.Visible = False
        '
        'timSYSMENU01
        '
        Me.timSYSMENU01.Interval = 500
        '
        'picSYSMENU01
        '
        Me.picSYSMENU01.BackColor = System.Drawing.Color.Transparent
        Me.picSYSMENU01.Location = New System.Drawing.Point(1600, 914)
        Me.picSYSMENU01.Name = "picSYSMENU01"
        Me.picSYSMENU01.Size = New System.Drawing.Size(319, 164)
        Me.picSYSMENU01.TabIndex = 32
        Me.picSYSMENU01.TabStop = False
        '
        'lblPrinterError
        '
        Me.lblPrinterError.BackColor = System.Drawing.Color.Transparent
        Me.lblPrinterError.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPrinterError.ForeColor = System.Drawing.Color.White
        Me.lblPrinterError.Image = CType(resources.GetObject("lblPrinterError.Image"), System.Drawing.Image)
        Me.lblPrinterError.Location = New System.Drawing.Point(1022, 953)
        Me.lblPrinterError.Name = "lblPrinterError"
        Me.lblPrinterError.Size = New System.Drawing.Size(295, 95)
        Me.lblPrinterError.TabIndex = 33
        Me.lblPrinterError.Text = "レシート切れ"
        Me.lblPrinterError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPrinterError.Visible = False
        '
        'lblCardDispenserError
        '
        Me.lblCardDispenserError.BackColor = System.Drawing.Color.Transparent
        Me.lblCardDispenserError.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCardDispenserError.ForeColor = System.Drawing.Color.White
        Me.lblCardDispenserError.Image = CType(resources.GetObject("lblCardDispenserError.Image"), System.Drawing.Image)
        Me.lblCardDispenserError.Location = New System.Drawing.Point(721, 953)
        Me.lblCardDispenserError.Name = "lblCardDispenserError"
        Me.lblCardDispenserError.Size = New System.Drawing.Size(295, 95)
        Me.lblCardDispenserError.TabIndex = 34
        Me.lblCardDispenserError.Text = "カード切れ"
        Me.lblCardDispenserError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCardDispenserError.Visible = False
        '
        'lblAd1Error
        '
        Me.lblAd1Error.BackColor = System.Drawing.Color.Transparent
        Me.lblAd1Error.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAd1Error.ForeColor = System.Drawing.Color.White
        Me.lblAd1Error.Image = CType(resources.GetObject("lblAd1Error.Image"), System.Drawing.Image)
        Me.lblAd1Error.Location = New System.Drawing.Point(420, 953)
        Me.lblAd1Error.Name = "lblAd1Error"
        Me.lblAd1Error.Size = New System.Drawing.Size(295, 95)
        Me.lblAd1Error.TabIndex = 35
        Me.lblAd1Error.Text = "ビルバリエラー"
        Me.lblAd1Error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAd1Error.Visible = False
        '
        'lblChangeError
        '
        Me.lblChangeError.BackColor = System.Drawing.Color.Transparent
        Me.lblChangeError.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblChangeError.ForeColor = System.Drawing.Color.White
        Me.lblChangeError.Image = CType(resources.GetObject("lblChangeError.Image"), System.Drawing.Image)
        Me.lblChangeError.Location = New System.Drawing.Point(119, 953)
        Me.lblChangeError.Name = "lblChangeError"
        Me.lblChangeError.Size = New System.Drawing.Size(295, 95)
        Me.lblChangeError.TabIndex = 36
        Me.lblChangeError.Text = "つり銭切れ"
        Me.lblChangeError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblChangeError.Visible = False
        '
        'timRead
        '
        Me.timRead.Interval = 50
        '
        'btnCheckIn
        '
        Me.btnCheckIn.BackColor = System.Drawing.Color.Transparent
        Me.btnCheckIn.Image = CType(resources.GetObject("btnCheckIn.Image"), System.Drawing.Image)
        Me.btnCheckIn.Location = New System.Drawing.Point(171, 426)
        Me.btnCheckIn.Name = "btnCheckIn"
        Me.btnCheckIn.Size = New System.Drawing.Size(771, 228)
        Me.btnCheckIn.TabIndex = 37
        Me.btnCheckIn.TabStop = False
        '
        'btnInfo
        '
        Me.btnInfo.BackColor = System.Drawing.Color.Transparent
        Me.btnInfo.Image = CType(resources.GetObject("btnInfo.Image"), System.Drawing.Image)
        Me.btnInfo.Location = New System.Drawing.Point(997, 426)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(771, 228)
        Me.btnInfo.TabIndex = 38
        Me.btnInfo.TabStop = False
        '
        'btnCharge
        '
        Me.btnCharge.BackColor = System.Drawing.Color.Transparent
        Me.btnCharge.Image = CType(resources.GetObject("btnCharge.Image"), System.Drawing.Image)
        Me.btnCharge.Location = New System.Drawing.Point(171, 676)
        Me.btnCharge.Name = "btnCharge"
        Me.btnCharge.Size = New System.Drawing.Size(771, 228)
        Me.btnCharge.TabIndex = 39
        Me.btnCharge.TabStop = False
        '
        'btnCardDispenser
        '
        Me.btnCardDispenser.BackColor = System.Drawing.Color.Transparent
        Me.btnCardDispenser.Image = CType(resources.GetObject("btnCardDispenser.Image"), System.Drawing.Image)
        Me.btnCardDispenser.Location = New System.Drawing.Point(997, 676)
        Me.btnCardDispenser.Name = "btnCardDispenser"
        Me.btnCardDispenser.Size = New System.Drawing.Size(771, 228)
        Me.btnCardDispenser.TabIndex = 40
        Me.btnCardDispenser.TabStop = False
        '
        'lblTimeCoron
        '
        Me.lblTimeCoron.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeCoron.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTimeCoron.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblTimeCoron.Location = New System.Drawing.Point(1704, 57)
        Me.lblTimeCoron.Name = "lblTimeCoron"
        Me.lblTimeCoron.Size = New System.Drawing.Size(30, 64)
        Me.lblTimeCoron.TabIndex = 44
        Me.lblTimeCoron.Text = ":"
        '
        'lblTime2
        '
        Me.lblTime2.AutoSize = True
        Me.lblTime2.BackColor = System.Drawing.Color.Transparent
        Me.lblTime2.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTime2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblTime2.Location = New System.Drawing.Point(1724, 47)
        Me.lblTime2.Name = "lblTime2"
        Me.lblTime2.Size = New System.Drawing.Size(118, 73)
        Me.lblTime2.TabIndex = 43
        Me.lblTime2.Text = "XX"
        '
        'lblTime1
        '
        Me.lblTime1.AutoSize = True
        Me.lblTime1.BackColor = System.Drawing.Color.Transparent
        Me.lblTime1.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTime1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblTime1.Location = New System.Drawing.Point(1614, 47)
        Me.lblTime1.Name = "lblTime1"
        Me.lblTime1.Size = New System.Drawing.Size(118, 73)
        Me.lblTime1.TabIndex = 42
        Me.lblTime1.Text = "XX"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblDate.Location = New System.Drawing.Point(1594, 138)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(275, 31)
        Me.lblDate.TabIndex = 41
        Me.lblDate.Text = "XXXX年XX月XX日(X)"
        '
        'mClock
        '
        '
        'btnSlotTest
        '
        Me.btnSlotTest.Location = New System.Drawing.Point(39, 851)
        Me.btnSlotTest.Name = "btnSlotTest"
        Me.btnSlotTest.Size = New System.Drawing.Size(100, 45)
        Me.btnSlotTest.TabIndex = 45
        Me.btnSlotTest.Text = "プリンタチェック"
        Me.btnSlotTest.UseVisualStyleBackColor = True
        Me.btnSlotTest.Visible = False
        '
        'lblRWError
        '
        Me.lblRWError.BackColor = System.Drawing.Color.Transparent
        Me.lblRWError.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRWError.ForeColor = System.Drawing.Color.White
        Me.lblRWError.Image = CType(resources.GetObject("lblRWError.Image"), System.Drawing.Image)
        Me.lblRWError.Location = New System.Drawing.Point(1323, 953)
        Me.lblRWError.Name = "lblRWError"
        Me.lblRWError.Size = New System.Drawing.Size(295, 95)
        Me.lblRWError.TabIndex = 46
        Me.lblRWError.Text = "RWエラー"
        Me.lblRWError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblRWError.Visible = False
        '
        'lblCMError
        '
        Me.lblCMError.BackColor = System.Drawing.Color.Transparent
        Me.lblCMError.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCMError.ForeColor = System.Drawing.Color.White
        Me.lblCMError.Image = CType(resources.GetObject("lblCMError.Image"), System.Drawing.Image)
        Me.lblCMError.Location = New System.Drawing.Point(721, 953)
        Me.lblCMError.Name = "lblCMError"
        Me.lblCMError.Size = New System.Drawing.Size(295, 95)
        Me.lblCMError.TabIndex = 47
        Me.lblCMError.Text = "コインメックエラー"
        Me.lblCMError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCMError.Visible = False
        '
        'frmMenuEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.lblRWError)
        Me.Controls.Add(Me.btnSlotTest)
        Me.Controls.Add(Me.lblTimeCoron)
        Me.Controls.Add(Me.lblTime2)
        Me.Controls.Add(Me.lblTime1)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.btnCardDispenser)
        Me.Controls.Add(Me.btnCharge)
        Me.Controls.Add(Me.btnCheckIn)
        Me.Controls.Add(Me.lblChangeError)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblAd1Error)
        Me.Controls.Add(Me.lblCardDispenserError)
        Me.Controls.Add(Me.lblPrinterError)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.picSYSMENU01)
        Me.Controls.Add(Me.lblCMError)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmMenuEx"
        Me.ShowInTaskbar = False
        Me.Text = ""
        CType(Me.picSYSMENU01, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCardDispenser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents timClearInfo As System.Windows.Forms.Timer
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents timSYSMENU01 As System.Windows.Forms.Timer
    Friend WithEvents picSYSMENU01 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPrinterError As System.Windows.Forms.Label
    Friend WithEvents lblCardDispenserError As System.Windows.Forms.Label
    Friend WithEvents lblAd1Error As System.Windows.Forms.Label
    Friend WithEvents lblChangeError As System.Windows.Forms.Label
    Friend WithEvents timRead As System.Windows.Forms.Timer
    Friend WithEvents btnCheckIn As System.Windows.Forms.PictureBox
    Friend WithEvents btnInfo As System.Windows.Forms.PictureBox
    Friend WithEvents btnCharge As System.Windows.Forms.PictureBox
    Friend WithEvents btnCardDispenser As System.Windows.Forms.PictureBox
    Friend WithEvents lblTimeCoron As System.Windows.Forms.Label
    Friend WithEvents lblTime2 As System.Windows.Forms.Label
    Friend WithEvents lblTime1 As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents mClock As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnSlotTest As System.Windows.Forms.Button
    Friend WithEvents lblRWError As System.Windows.Forms.Label
    Friend WithEvents lblCMError As System.Windows.Forms.Label
End Class
