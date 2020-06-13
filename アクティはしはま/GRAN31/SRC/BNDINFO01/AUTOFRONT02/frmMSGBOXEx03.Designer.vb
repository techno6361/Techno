<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMSGBOXEx03
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSGBOXEx03))
        Me.lblMSG1 = New System.Windows.Forms.Label()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnExit = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.btnOK = New System.Windows.Forms.PictureBox()
        Me.lblMSG2 = New System.Windows.Forms.Label()
        Me.lblMSG3 = New System.Windows.Forms.Label()
        Me.lblMSG4 = New System.Windows.Forms.Label()
        Me.lblMSG5 = New System.Windows.Forms.Label()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMSG1
        '
        Me.lblMSG1.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG1.Enabled = False
        Me.lblMSG1.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.lblMSG1.Location = New System.Drawing.Point(174, 257)
        Me.lblMSG1.Name = "lblMSG1"
        Me.lblMSG1.Size = New System.Drawing.Size(917, 38)
        Me.lblMSG1.TabIndex = 8
        Me.lblMSG1.Text = "月間来場3回目の為、ポイントを100P獲得しました。"
        Me.lblMSG1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMSG1.Visible = False
        '
        'picImage
        '
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.Location = New System.Drawing.Point(507, 51)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(128, 128)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 13
        Me.picImage.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'btnExit
        '
        Me.btnExit.AutoSize = True
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.Font = New System.Drawing.Font("MS UI Gothic", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExit.Location = New System.Drawing.Point(1036, 38)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(55, 37)
        Me.btnExit.TabIndex = 22
        Me.btnExit.Text = "×"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblTitle.Location = New System.Drawing.Point(317, 190)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(555, 61)
        Me.lblTitle.TabIndex = 23
        Me.lblTitle.Text = "おめでとうございます！"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(662, 38)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(342, 103)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.TabStop = False
        Me.btnCancel.Visible = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(136, 51)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(342, 103)
        Me.btnOK.TabIndex = 20
        Me.btnOK.TabStop = False
        '
        'lblMSG2
        '
        Me.lblMSG2.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG2.Enabled = False
        Me.lblMSG2.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.lblMSG2.Location = New System.Drawing.Point(174, 300)
        Me.lblMSG2.Name = "lblMSG2"
        Me.lblMSG2.Size = New System.Drawing.Size(917, 38)
        Me.lblMSG2.TabIndex = 24
        Me.lblMSG2.Text = "月間来場ポイントを100P獲得しました。"
        Me.lblMSG2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMSG2.Visible = False
        '
        'lblMSG3
        '
        Me.lblMSG3.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG3.Enabled = False
        Me.lblMSG3.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.lblMSG3.Location = New System.Drawing.Point(174, 342)
        Me.lblMSG3.Name = "lblMSG3"
        Me.lblMSG3.Size = New System.Drawing.Size(917, 38)
        Me.lblMSG3.TabIndex = 25
        Me.lblMSG3.Text = "月間来場ポイントを100P獲得しました。"
        Me.lblMSG3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMSG3.Visible = False
        '
        'lblMSG4
        '
        Me.lblMSG4.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG4.Enabled = False
        Me.lblMSG4.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.lblMSG4.Location = New System.Drawing.Point(174, 384)
        Me.lblMSG4.Name = "lblMSG4"
        Me.lblMSG4.Size = New System.Drawing.Size(917, 38)
        Me.lblMSG4.TabIndex = 26
        Me.lblMSG4.Text = "月間来場ポイントを100P獲得しました。"
        Me.lblMSG4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMSG4.Visible = False
        '
        'lblMSG5
        '
        Me.lblMSG5.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG5.Enabled = False
        Me.lblMSG5.Font = New System.Drawing.Font("游ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.lblMSG5.Location = New System.Drawing.Point(174, 425)
        Me.lblMSG5.Name = "lblMSG5"
        Me.lblMSG5.Size = New System.Drawing.Size(917, 38)
        Me.lblMSG5.TabIndex = 27
        Me.lblMSG5.Text = "月間来場ポイントを100P獲得しました。"
        Me.lblMSG5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMSG5.Visible = False
        '
        'frmMSGBOXEx03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1124, 497)
        Me.Controls.Add(Me.lblMSG5)
        Me.Controls.Add(Me.lblMSG4)
        Me.Controls.Add(Me.lblMSG3)
        Me.Controls.Add(Me.lblMSG2)
        Me.Controls.Add(Me.lblMSG1)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmMSGBOXEx03"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "おめでとうございます！"
        Me.TransparencyKey = System.Drawing.Color.Green
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents lblMSG1 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnExit As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents btnOK As System.Windows.Forms.PictureBox
    Friend WithEvents lblMSG2 As System.Windows.Forms.Label
    Friend WithEvents lblMSG3 As System.Windows.Forms.Label
    Friend WithEvents lblMSG4 As System.Windows.Forms.Label
    Friend WithEvents lblMSG5 As System.Windows.Forms.Label
End Class
