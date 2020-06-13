<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHAKKEN01_STR610
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHAKKEN01_STR610))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblConnect = New System.Windows.Forms.Label()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblDIP4 = New System.Windows.Forms.Label()
        Me.lblDIP3 = New System.Windows.Forms.Label()
        Me.lblDIP2 = New System.Windows.Forms.Label()
        Me.lblDIP1 = New System.Windows.Forms.Label()
        Me.lblNONE2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblNSNS = New System.Windows.Forms.Label()
        Me.lblHSNS = New System.Windows.Forms.Label()
        Me.lblESNS = New System.Windows.Forms.Label()
        Me.lblTSNS = New System.Windows.Forms.Label()
        Me.lblNONE1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lbl44H = New System.Windows.Forms.Label()
        Me.lbl43H = New System.Windows.Forms.Label()
        Me.lbl42H = New System.Windows.Forms.Label()
        Me.lbl41H = New System.Windows.Forms.Label()
        Me.lblNONE0 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblTIN3 = New System.Windows.Forms.Label()
        Me.lblTIN2 = New System.Windows.Forms.Label()
        Me.lblTIN1 = New System.Windows.Forms.Label()
        Me.lblTOUT3 = New System.Windows.Forms.Label()
        Me.lblNONE3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblConnect)
        Me.Panel1.Location = New System.Drawing.Point(95, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(261, 110)
        Me.Panel1.TabIndex = 39
        '
        'lblConnect
        '
        Me.lblConnect.AutoSize = True
        Me.lblConnect.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblConnect.ForeColor = System.Drawing.Color.Silver
        Me.lblConnect.Location = New System.Drawing.Point(33, 22)
        Me.lblConnect.Name = "lblConnect"
        Me.lblConnect.Size = New System.Drawing.Size(185, 54)
        Me.lblConnect.TabIndex = 0
        Me.lblConnect.Text = "接続中"
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.ForestGreen
        Me.btnConnect.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(1319, 12)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(285, 116)
        Me.btnConnect.TabIndex = 41
        Me.btnConnect.Text = "接続"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkGray
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1623, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(285, 116)
        Me.btnBack.TabIndex = 40
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnTest
        '
        Me.btnTest.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnTest.Enabled = False
        Me.btnTest.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTest.ForeColor = System.Drawing.Color.White
        Me.btnTest.Location = New System.Drawing.Point(458, 12)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(285, 116)
        Me.btnTest.TabIndex = 44
        Me.btnTest.Text = "テスト発券"
        Me.btnTest.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lblDIP4)
        Me.Panel2.Controls.Add(Me.lblDIP3)
        Me.Panel2.Controls.Add(Me.lblDIP2)
        Me.Panel2.Controls.Add(Me.lblDIP1)
        Me.Panel2.Controls.Add(Me.lblNONE2)
        Me.Panel2.Location = New System.Drawing.Point(95, 527)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1509, 110)
        Me.Panel2.TabIndex = 45
        '
        'lblDIP4
        '
        Me.lblDIP4.AutoSize = True
        Me.lblDIP4.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDIP4.ForeColor = System.Drawing.Color.Silver
        Me.lblDIP4.Location = New System.Drawing.Point(913, 26)
        Me.lblDIP4.Name = "lblDIP4"
        Me.lblDIP4.Size = New System.Drawing.Size(154, 54)
        Me.lblDIP4.TabIndex = 48
        Me.lblDIP4.Text = "ＤＩＰ４"
        '
        'lblDIP3
        '
        Me.lblDIP3.AutoSize = True
        Me.lblDIP3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDIP3.ForeColor = System.Drawing.Color.Silver
        Me.lblDIP3.Location = New System.Drawing.Point(647, 26)
        Me.lblDIP3.Name = "lblDIP3"
        Me.lblDIP3.Size = New System.Drawing.Size(154, 54)
        Me.lblDIP3.TabIndex = 48
        Me.lblDIP3.Text = "ＤＩＰ３"
        '
        'lblDIP2
        '
        Me.lblDIP2.AutoSize = True
        Me.lblDIP2.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDIP2.ForeColor = System.Drawing.Color.Silver
        Me.lblDIP2.Location = New System.Drawing.Point(350, 26)
        Me.lblDIP2.Name = "lblDIP2"
        Me.lblDIP2.Size = New System.Drawing.Size(154, 54)
        Me.lblDIP2.TabIndex = 36
        Me.lblDIP2.Text = "ＤＩＰ２"
        '
        'lblDIP1
        '
        Me.lblDIP1.AutoSize = True
        Me.lblDIP1.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDIP1.ForeColor = System.Drawing.Color.Silver
        Me.lblDIP1.Location = New System.Drawing.Point(11, 26)
        Me.lblDIP1.Name = "lblDIP1"
        Me.lblDIP1.Size = New System.Drawing.Size(154, 54)
        Me.lblDIP1.TabIndex = 34
        Me.lblDIP1.Text = "ＤＩＰ１"
        '
        'lblNONE2
        '
        Me.lblNONE2.AutoSize = True
        Me.lblNONE2.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE2.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE2.Location = New System.Drawing.Point(1245, 26)
        Me.lblNONE2.Name = "lblNONE2"
        Me.lblNONE2.Size = New System.Drawing.Size(238, 54)
        Me.lblNONE2.TabIndex = 35
        Me.lblNONE2.Text = "エラー無し"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.lblNSNS)
        Me.Panel3.Controls.Add(Me.lblHSNS)
        Me.Panel3.Controls.Add(Me.lblESNS)
        Me.Panel3.Controls.Add(Me.lblTSNS)
        Me.Panel3.Controls.Add(Me.lblNONE1)
        Me.Panel3.Location = New System.Drawing.Point(95, 368)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1509, 110)
        Me.Panel3.TabIndex = 46
        '
        'lblNSNS
        '
        Me.lblNSNS.AutoSize = True
        Me.lblNSNS.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNSNS.ForeColor = System.Drawing.Color.Silver
        Me.lblNSNS.Location = New System.Drawing.Point(350, 26)
        Me.lblNSNS.Name = "lblNSNS"
        Me.lblNSNS.Size = New System.Drawing.Size(224, 54)
        Me.lblNSNS.TabIndex = 48
        Me.lblNSNS.Text = "ニアエンド"
        '
        'lblHSNS
        '
        Me.lblHSNS.AutoSize = True
        Me.lblHSNS.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHSNS.ForeColor = System.Drawing.Color.Silver
        Me.lblHSNS.Location = New System.Drawing.Point(913, 26)
        Me.lblHSNS.Name = "lblHSNS"
        Me.lblHSNS.Size = New System.Drawing.Size(187, 54)
        Me.lblHSNS.TabIndex = 49
        Me.lblHSNS.Text = "ＨＳＮＳ"
        '
        'lblESNS
        '
        Me.lblESNS.AutoSize = True
        Me.lblESNS.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblESNS.ForeColor = System.Drawing.Color.Silver
        Me.lblESNS.Location = New System.Drawing.Point(647, 26)
        Me.lblESNS.Name = "lblESNS"
        Me.lblESNS.Size = New System.Drawing.Size(183, 54)
        Me.lblESNS.TabIndex = 48
        Me.lblESNS.Text = "ＥＳＮＳ"
        '
        'lblTSNS
        '
        Me.lblTSNS.AutoSize = True
        Me.lblTSNS.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTSNS.ForeColor = System.Drawing.Color.Silver
        Me.lblTSNS.Location = New System.Drawing.Point(11, 26)
        Me.lblTSNS.Name = "lblTSNS"
        Me.lblTSNS.Size = New System.Drawing.Size(238, 54)
        Me.lblTSNS.TabIndex = 34
        Me.lblTSNS.Text = "カード無し"
        '
        'lblNONE1
        '
        Me.lblNONE1.AutoSize = True
        Me.lblNONE1.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE1.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE1.Location = New System.Drawing.Point(1245, 26)
        Me.lblNONE1.Name = "lblNONE1"
        Me.lblNONE1.Size = New System.Drawing.Size(238, 54)
        Me.lblNONE1.TabIndex = 35
        Me.lblNONE1.Text = "エラー無し"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.lbl44H)
        Me.Panel4.Controls.Add(Me.lbl43H)
        Me.Panel4.Controls.Add(Me.lbl42H)
        Me.Panel4.Controls.Add(Me.lbl41H)
        Me.Panel4.Controls.Add(Me.lblNONE0)
        Me.Panel4.Location = New System.Drawing.Point(95, 209)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1509, 110)
        Me.Panel4.TabIndex = 47
        '
        'lbl44H
        '
        Me.lbl44H.AutoSize = True
        Me.lbl44H.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl44H.ForeColor = System.Drawing.Color.Silver
        Me.lbl44H.Location = New System.Drawing.Point(913, 26)
        Me.lbl44H.Name = "lbl44H"
        Me.lbl44H.Size = New System.Drawing.Size(307, 54)
        Me.lbl44H.TabIndex = 50
        Me.lbl44H.Text = "エラー発生中"
        '
        'lbl43H
        '
        Me.lbl43H.AutoSize = True
        Me.lbl43H.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl43H.ForeColor = System.Drawing.Color.Silver
        Me.lbl43H.Location = New System.Drawing.Point(647, 26)
        Me.lbl43H.Name = "lbl43H"
        Me.lbl43H.Size = New System.Drawing.Size(239, 54)
        Me.lbl43H.TabIndex = 49
        Me.lbl43H.Text = "初期化中"
        '
        'lbl42H
        '
        Me.lbl42H.AutoSize = True
        Me.lbl42H.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl42H.ForeColor = System.Drawing.Color.Silver
        Me.lbl42H.Location = New System.Drawing.Point(350, 26)
        Me.lbl42H.Name = "lbl42H"
        Me.lbl42H.Size = New System.Drawing.Size(259, 54)
        Me.lbl42H.TabIndex = 48
        Me.lbl42H.Text = "センサ異常"
        '
        'lbl41H
        '
        Me.lbl41H.AutoSize = True
        Me.lbl41H.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl41H.ForeColor = System.Drawing.Color.Silver
        Me.lbl41H.Location = New System.Drawing.Point(11, 26)
        Me.lbl41H.Name = "lbl41H"
        Me.lbl41H.Size = New System.Drawing.Size(307, 54)
        Me.lbl41H.TabIndex = 34
        Me.lbl41H.Text = "カード払出中"
        '
        'lblNONE0
        '
        Me.lblNONE0.AutoSize = True
        Me.lblNONE0.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE0.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE0.Location = New System.Drawing.Point(1245, 26)
        Me.lblNONE0.Name = "lblNONE0"
        Me.lblNONE0.Size = New System.Drawing.Size(238, 54)
        Me.lblNONE0.TabIndex = 35
        Me.lblNONE0.Text = "エラー無し"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.lblTIN3)
        Me.Panel5.Controls.Add(Me.lblTIN2)
        Me.Panel5.Controls.Add(Me.lblTIN1)
        Me.Panel5.Controls.Add(Me.lblTOUT3)
        Me.Panel5.Controls.Add(Me.lblNONE3)
        Me.Panel5.Location = New System.Drawing.Point(95, 686)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1509, 110)
        Me.Panel5.TabIndex = 48
        '
        'lblTIN3
        '
        Me.lblTIN3.AutoSize = True
        Me.lblTIN3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTIN3.ForeColor = System.Drawing.Color.Silver
        Me.lblTIN3.Location = New System.Drawing.Point(913, 26)
        Me.lblTIN3.Name = "lblTIN3"
        Me.lblTIN3.Size = New System.Drawing.Size(150, 54)
        Me.lblTIN3.TabIndex = 48
        Me.lblTIN3.Text = "ＴＩＮ３"
        '
        'lblTIN2
        '
        Me.lblTIN2.AutoSize = True
        Me.lblTIN2.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTIN2.ForeColor = System.Drawing.Color.Silver
        Me.lblTIN2.Location = New System.Drawing.Point(647, 26)
        Me.lblTIN2.Name = "lblTIN2"
        Me.lblTIN2.Size = New System.Drawing.Size(150, 54)
        Me.lblTIN2.TabIndex = 48
        Me.lblTIN2.Text = "ＴＩＮ２"
        '
        'lblTIN1
        '
        Me.lblTIN1.AutoSize = True
        Me.lblTIN1.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTIN1.ForeColor = System.Drawing.Color.Silver
        Me.lblTIN1.Location = New System.Drawing.Point(350, 26)
        Me.lblTIN1.Name = "lblTIN1"
        Me.lblTIN1.Size = New System.Drawing.Size(150, 54)
        Me.lblTIN1.TabIndex = 36
        Me.lblTIN1.Text = "ＴＩＮ１"
        '
        'lblTOUT3
        '
        Me.lblTOUT3.AutoSize = True
        Me.lblTOUT3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTOUT3.ForeColor = System.Drawing.Color.Silver
        Me.lblTOUT3.Location = New System.Drawing.Point(11, 26)
        Me.lblTOUT3.Name = "lblTOUT3"
        Me.lblTOUT3.Size = New System.Drawing.Size(211, 54)
        Me.lblTOUT3.TabIndex = 34
        Me.lblTOUT3.Text = "ＴＯＵＴ３"
        '
        'lblNONE3
        '
        Me.lblNONE3.AutoSize = True
        Me.lblNONE3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE3.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE3.Location = New System.Drawing.Point(1245, 26)
        Me.lblNONE3.Name = "lblNONE3"
        Me.lblNONE3.Size = New System.Drawing.Size(238, 54)
        Me.lblNONE3.TabIndex = 35
        Me.lblNONE3.Text = "エラー無し"
        '
        'frmHAKKEN01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHAKKEN01"
        Me.Text = "frmHAKKEN01"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblConnect As System.Windows.Forms.Label
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblDIP1 As System.Windows.Forms.Label
    Friend WithEvents lblNONE2 As System.Windows.Forms.Label
    Friend WithEvents lblDIP4 As System.Windows.Forms.Label
    Friend WithEvents lblDIP3 As System.Windows.Forms.Label
    Friend WithEvents lblDIP2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblNSNS As System.Windows.Forms.Label
    Friend WithEvents lblHSNS As System.Windows.Forms.Label
    Friend WithEvents lblESNS As System.Windows.Forms.Label
    Friend WithEvents lblTSNS As System.Windows.Forms.Label
    Friend WithEvents lblNONE1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lbl44H As System.Windows.Forms.Label
    Friend WithEvents lbl43H As System.Windows.Forms.Label
    Friend WithEvents lbl42H As System.Windows.Forms.Label
    Friend WithEvents lbl41H As System.Windows.Forms.Label
    Friend WithEvents lblNONE0 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblTIN3 As System.Windows.Forms.Label
    Friend WithEvents lblTIN2 As System.Windows.Forms.Label
    Friend WithEvents lblTIN1 As System.Windows.Forms.Label
    Friend WithEvents lblTOUT3 As System.Windows.Forms.Label
    Friend WithEvents lblNONE3 As System.Windows.Forms.Label
End Class
