<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmError01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmError01))
        Me.pnlErrorInfo = New System.Windows.Forms.Panel()
        Me.btnPowerOff = New System.Windows.Forms.Button()
        Me.lblAD1 = New System.Windows.Forms.Label()
        Me.lblMCH3000 = New System.Windows.Forms.Label()
        Me.lblIcRw = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblErrMessage = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.picError = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlErrorInfo.SuspendLayout()
        CType(Me.picError, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlErrorInfo
        '
        Me.pnlErrorInfo.BackColor = System.Drawing.Color.LightGray
        Me.pnlErrorInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlErrorInfo.Controls.Add(Me.btnPowerOff)
        Me.pnlErrorInfo.Controls.Add(Me.lblAD1)
        Me.pnlErrorInfo.Controls.Add(Me.lblMCH3000)
        Me.pnlErrorInfo.Controls.Add(Me.lblIcRw)
        Me.pnlErrorInfo.Controls.Add(Me.btnReset)
        Me.pnlErrorInfo.Controls.Add(Me.btnBack)
        Me.pnlErrorInfo.Controls.Add(Me.lblErrMessage)
        Me.pnlErrorInfo.Location = New System.Drawing.Point(31, 26)
        Me.pnlErrorInfo.Name = "pnlErrorInfo"
        Me.pnlErrorInfo.Size = New System.Drawing.Size(1222, 752)
        Me.pnlErrorInfo.TabIndex = 0
        Me.pnlErrorInfo.Visible = False
        '
        'btnPowerOff
        '
        Me.btnPowerOff.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPowerOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPowerOff.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPowerOff.ForeColor = System.Drawing.Color.White
        Me.btnPowerOff.Location = New System.Drawing.Point(26, 614)
        Me.btnPowerOff.Name = "btnPowerOff"
        Me.btnPowerOff.Size = New System.Drawing.Size(285, 116)
        Me.btnPowerOff.TabIndex = 78
        Me.btnPowerOff.Text = "電源断"
        Me.btnPowerOff.UseVisualStyleBackColor = False
        '
        'lblAD1
        '
        Me.lblAD1.BackColor = System.Drawing.Color.Silver
        Me.lblAD1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAD1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAD1.ForeColor = System.Drawing.Color.Gray
        Me.lblAD1.Location = New System.Drawing.Point(905, 520)
        Me.lblAD1.Name = "lblAD1"
        Me.lblAD1.Size = New System.Drawing.Size(285, 63)
        Me.lblAD1.TabIndex = 72
        Me.lblAD1.Text = "ビルバリ"
        Me.lblAD1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMCH3000
        '
        Me.lblMCH3000.BackColor = System.Drawing.Color.Silver
        Me.lblMCH3000.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMCH3000.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMCH3000.ForeColor = System.Drawing.Color.Gray
        Me.lblMCH3000.Location = New System.Drawing.Point(905, 457)
        Me.lblMCH3000.Name = "lblMCH3000"
        Me.lblMCH3000.Size = New System.Drawing.Size(285, 63)
        Me.lblMCH3000.TabIndex = 71
        Me.lblMCH3000.Text = "カードユニット"
        Me.lblMCH3000.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIcRw
        '
        Me.lblIcRw.BackColor = System.Drawing.Color.Silver
        Me.lblIcRw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIcRw.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIcRw.ForeColor = System.Drawing.Color.Gray
        Me.lblIcRw.Location = New System.Drawing.Point(905, 393)
        Me.lblIcRw.Name = "lblIcRw"
        Me.lblIcRw.Size = New System.Drawing.Size(285, 63)
        Me.lblIcRw.TabIndex = 70
        Me.lblIcRw.Text = "ICRW"
        Me.lblIcRw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.Orange
        Me.btnReset.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReset.ForeColor = System.Drawing.Color.White
        Me.btnReset.Location = New System.Drawing.Point(905, 256)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(285, 116)
        Me.btnReset.TabIndex = 69
        Me.btnReset.Text = "リセット"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Gray
        Me.btnBack.Enabled = False
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(905, 102)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(285, 116)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "閉じる"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'lblErrMessage
        '
        Me.lblErrMessage.BackColor = System.Drawing.Color.White
        Me.lblErrMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblErrMessage.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblErrMessage.ForeColor = System.Drawing.Color.Red
        Me.lblErrMessage.Location = New System.Drawing.Point(13, 13)
        Me.lblErrMessage.Name = "lblErrMessage"
        Me.lblErrMessage.Size = New System.Drawing.Size(1177, 63)
        Me.lblErrMessage.TabIndex = 0
        Me.lblErrMessage.Text = "エラーが発生しました。"
        Me.lblErrMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 60.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Yellow
        Me.Label8.Location = New System.Drawing.Point(200, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(860, 290)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "エラーが発生しました。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "係員をお呼び下さい。"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picError
        '
        Me.picError.BackColor = System.Drawing.Color.Transparent
        Me.picError.Location = New System.Drawing.Point(840, 532)
        Me.picError.Name = "picError"
        Me.picError.Size = New System.Drawing.Size(439, 267)
        Me.picError.TabIndex = 2
        Me.picError.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(542, 420)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(156, 350)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'frmError01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.pnlErrorInfo)
        Me.Controls.Add(Me.picError)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmError01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmError01"
        Me.pnlErrorInfo.ResumeLayout(False)
        CType(Me.picError, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlErrorInfo As System.Windows.Forms.Panel
    Friend WithEvents lblErrMessage As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents picError As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents lblAD1 As System.Windows.Forms.Label
    Friend WithEvents lblMCH3000 As System.Windows.Forms.Label
    Friend WithEvents lblIcRw As System.Windows.Forms.Label
    Friend WithEvents btnPowerOff As System.Windows.Forms.Button
End Class
