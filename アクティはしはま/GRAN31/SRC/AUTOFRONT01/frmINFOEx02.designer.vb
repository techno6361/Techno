<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmINFOEx02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmINFOEx02))
        Me.tmClose = New System.Windows.Forms.Timer(Me.components)
        Me.btnExit = New System.Windows.Forms.PictureBox()
        Me.btnCheckIn = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCharge = New System.Windows.Forms.PictureBox()
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.lblKINGAKU = New System.Windows.Forms.Label()
        Me.lblPREZANKN = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.lblZENENTDATE = New System.Windows.Forms.Label()
        Me.lblZENNYUKIN = New System.Windows.Forms.Label()
        Me.lblCCSNAME = New System.Windows.Forms.Label()
        Me.lblNCSRANK = New System.Windows.Forms.Label()
        Me.lblNCARDID = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        CType(Me.btnExit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmClose
        '
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.Location = New System.Drawing.Point(1269, 938)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(342, 103)
        Me.btnExit.TabIndex = 17
        Me.btnExit.TabStop = False
        Me.btnExit.Visible = False
        '
        'btnCheckIn
        '
        Me.btnCheckIn.BackColor = System.Drawing.Color.Transparent
        Me.btnCheckIn.Image = CType(resources.GetObject("btnCheckIn.Image"), System.Drawing.Image)
        Me.btnCheckIn.Location = New System.Drawing.Point(346, 938)
        Me.btnCheckIn.Name = "btnCheckIn"
        Me.btnCheckIn.Size = New System.Drawing.Size(342, 103)
        Me.btnCheckIn.TabIndex = 16
        Me.btnCheckIn.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(323, 272)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 142)
        Me.Label1.TabIndex = 10
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCharge
        '
        Me.btnCharge.BackColor = System.Drawing.Color.Transparent
        Me.btnCharge.Image = CType(resources.GetObject("btnCharge.Image"), System.Drawing.Image)
        Me.btnCharge.Location = New System.Drawing.Point(816, 938)
        Me.btnCharge.Name = "btnCharge"
        Me.btnCharge.Size = New System.Drawing.Size(342, 103)
        Me.btnCharge.TabIndex = 18
        Me.btnCharge.TabStop = False
        Me.btnCharge.Visible = False
        '
        'picStatus
        '
        Me.picStatus.BackColor = System.Drawing.Color.Transparent
        Me.picStatus.Image = CType(resources.GetObject("picStatus.Image"), System.Drawing.Image)
        Me.picStatus.Location = New System.Drawing.Point(66, 29)
        Me.picStatus.Name = "picStatus"
        Me.picStatus.Size = New System.Drawing.Size(362, 90)
        Me.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picStatus.TabIndex = 181
        Me.picStatus.TabStop = False
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblZANKN.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.LightBlue
        Me.lblZANKN.Location = New System.Drawing.Point(1226, 526)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(287, 76)
        Me.lblZANKN.TabIndex = 180
        Me.lblZANKN.Text = "99,999"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKINGAKU
        '
        Me.lblKINGAKU.BackColor = System.Drawing.Color.Transparent
        Me.lblKINGAKU.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKINGAKU.ForeColor = System.Drawing.Color.Black
        Me.lblKINGAKU.Location = New System.Drawing.Point(1206, 441)
        Me.lblKINGAKU.Name = "lblKINGAKU"
        Me.lblKINGAKU.Size = New System.Drawing.Size(307, 76)
        Me.lblKINGAKU.TabIndex = 179
        Me.lblKINGAKU.Text = "99,999"
        Me.lblKINGAKU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPREZANKN
        '
        Me.lblPREZANKN.BackColor = System.Drawing.Color.Transparent
        Me.lblPREZANKN.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPREZANKN.ForeColor = System.Drawing.Color.LightBlue
        Me.lblPREZANKN.Location = New System.Drawing.Point(1226, 607)
        Me.lblPREZANKN.Name = "lblPREZANKN"
        Me.lblPREZANKN.Size = New System.Drawing.Size(287, 76)
        Me.lblPREZANKN.TabIndex = 182
        Me.lblPREZANKN.Text = "99,999"
        Me.lblPREZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.Transparent
        Me.lblSRTPO.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.Black
        Me.lblSRTPO.Location = New System.Drawing.Point(1226, 689)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(287, 76)
        Me.lblSRTPO.TabIndex = 183
        Me.lblSRTPO.Text = "99,999"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZENENTDATE
        '
        Me.lblZENENTDATE.BackColor = System.Drawing.Color.Transparent
        Me.lblZENENTDATE.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZENENTDATE.ForeColor = System.Drawing.Color.Black
        Me.lblZENENTDATE.Location = New System.Drawing.Point(1162, 771)
        Me.lblZENENTDATE.Name = "lblZENENTDATE"
        Me.lblZENENTDATE.Size = New System.Drawing.Size(369, 76)
        Me.lblZENENTDATE.TabIndex = 184
        Me.lblZENENTDATE.Text = "19/03/30(土)"
        Me.lblZENENTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblZENNYUKIN
        '
        Me.lblZENNYUKIN.BackColor = System.Drawing.Color.Transparent
        Me.lblZENNYUKIN.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZENNYUKIN.ForeColor = System.Drawing.Color.Black
        Me.lblZENNYUKIN.Location = New System.Drawing.Point(1226, 852)
        Me.lblZENNYUKIN.Name = "lblZENNYUKIN"
        Me.lblZENNYUKIN.Size = New System.Drawing.Size(287, 76)
        Me.lblZENNYUKIN.TabIndex = 185
        Me.lblZENNYUKIN.Text = "99,999"
        Me.lblZENNYUKIN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCCSNAME
        '
        Me.lblCCSNAME.BackColor = System.Drawing.Color.Transparent
        Me.lblCCSNAME.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCCSNAME.ForeColor = System.Drawing.Color.Black
        Me.lblCCSNAME.Location = New System.Drawing.Point(800, 362)
        Me.lblCCSNAME.Name = "lblCCSNAME"
        Me.lblCCSNAME.Size = New System.Drawing.Size(713, 76)
        Me.lblCCSNAME.TabIndex = 186
        Me.lblCCSNAME.Text = "テクノ太郎"
        Me.lblCCSNAME.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNCSRANK
        '
        Me.lblNCSRANK.BackColor = System.Drawing.Color.Transparent
        Me.lblNCSRANK.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNCSRANK.ForeColor = System.Drawing.Color.Black
        Me.lblNCSRANK.Location = New System.Drawing.Point(800, 278)
        Me.lblNCSRANK.Name = "lblNCSRANK"
        Me.lblNCSRANK.Size = New System.Drawing.Size(713, 76)
        Me.lblNCSRANK.TabIndex = 187
        Me.lblNCSRANK.Text = "ビジター"
        Me.lblNCSRANK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNCARDID
        '
        Me.lblNCARDID.BackColor = System.Drawing.Color.Transparent
        Me.lblNCARDID.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNCARDID.ForeColor = System.Drawing.Color.Black
        Me.lblNCARDID.Location = New System.Drawing.Point(640, 199)
        Me.lblNCARDID.Name = "lblNCARDID"
        Me.lblNCARDID.Size = New System.Drawing.Size(862, 76)
        Me.lblNCARDID.TabIndex = 188
        Me.lblNCARDID.Text = "00003372 (SN0.        )"
        Me.lblNCARDID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(1500, 441)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 76)
        Me.Label9.TabIndex = 189
        Me.Label9.Text = "円"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.LightBlue
        Me.Label10.Location = New System.Drawing.Point(1500, 526)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 76)
        Me.Label10.TabIndex = 190
        Me.Label10.Text = "円"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.LightBlue
        Me.Label11.Location = New System.Drawing.Point(1500, 607)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 76)
        Me.Label11.TabIndex = 191
        Me.Label11.Text = "円"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(1500, 689)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 76)
        Me.Label12.TabIndex = 192
        Me.Label12.Text = "Ｐ"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("游ゴシック", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(1500, 852)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 76)
        Me.Label13.TabIndex = 193
        Me.Label13.Text = "円"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmINFOEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblNCARDID)
        Me.Controls.Add(Me.lblNCSRANK)
        Me.Controls.Add(Me.lblCCSNAME)
        Me.Controls.Add(Me.lblZENNYUKIN)
        Me.Controls.Add(Me.lblZENENTDATE)
        Me.Controls.Add(Me.lblSRTPO)
        Me.Controls.Add(Me.lblPREZANKN)
        Me.Controls.Add(Me.picStatus)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.lblKINGAKU)
        Me.Controls.Add(Me.btnCharge)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCheckIn)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmINFOEx"
        Me.Text = "frmMSGBOXEx"
        CType(Me.btnExit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmClose As System.Windows.Forms.Timer
    Friend WithEvents btnCheckIn As System.Windows.Forms.PictureBox
    Friend WithEvents btnExit As System.Windows.Forms.PictureBox
    Friend WithEvents btnCharge As System.Windows.Forms.PictureBox
    Friend WithEvents picStatus As System.Windows.Forms.PictureBox
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents lblKINGAKU As System.Windows.Forms.Label
    Friend WithEvents lblPREZANKN As System.Windows.Forms.Label
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents lblZENENTDATE As System.Windows.Forms.Label
    Friend WithEvents lblZENNYUKIN As System.Windows.Forms.Label
    Friend WithEvents lblCCSNAME As System.Windows.Forms.Label
    Friend WithEvents lblNCSRANK As System.Windows.Forms.Label
    Friend WithEvents lblNCARDID As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
