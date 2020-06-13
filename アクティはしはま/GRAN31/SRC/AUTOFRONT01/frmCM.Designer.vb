<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCM
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
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblConnect = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnUpdInfo = New System.Windows.Forms.Button()
        Me.lblAbNormal11 = New System.Windows.Forms.Label()
        Me.lblAbNormal10 = New System.Windows.Forms.Label()
        Me.lblAbNormal9 = New System.Windows.Forms.Label()
        Me.lblAbNormal8 = New System.Windows.Forms.Label()
        Me.lblAbNormal7 = New System.Windows.Forms.Label()
        Me.lblAbNormal6 = New System.Windows.Forms.Label()
        Me.lblAbNormal5 = New System.Windows.Forms.Label()
        Me.lblAbNormal4 = New System.Windows.Forms.Label()
        Me.lblAbNormal3 = New System.Windows.Forms.Label()
        Me.lblAbNormal2 = New System.Windows.Forms.Label()
        Me.lblAbNormal1 = New System.Windows.Forms.Label()
        Me.lblState8 = New System.Windows.Forms.Label()
        Me.lblState7 = New System.Windows.Forms.Label()
        Me.lblState6 = New System.Windows.Forms.Label()
        Me.lblState5 = New System.Windows.Forms.Label()
        Me.lblState4 = New System.Windows.Forms.Label()
        Me.lblState3 = New System.Windows.Forms.Label()
        Me.lblState2 = New System.Windows.Forms.Label()
        Me.lblState1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl10yen = New System.Windows.Forms.Label()
        Me.lbl100yen = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkGray
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1605, 30)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(285, 116)
        Me.btnBack.TabIndex = 32
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
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
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.ForestGreen
        Me.btnConnect.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(1244, 30)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(285, 116)
        Me.btnConnect.TabIndex = 40
        Me.btnConnect.Text = "接続"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.lblAbNormal11)
        Me.Panel3.Controls.Add(Me.lblAbNormal1)
        Me.Panel3.Controls.Add(Me.lblAbNormal10)
        Me.Panel3.Controls.Add(Me.lblAbNormal2)
        Me.Panel3.Controls.Add(Me.lblAbNormal9)
        Me.Panel3.Controls.Add(Me.lblAbNormal3)
        Me.Panel3.Controls.Add(Me.lblAbNormal8)
        Me.Panel3.Controls.Add(Me.lblAbNormal4)
        Me.Panel3.Controls.Add(Me.lblAbNormal7)
        Me.Panel3.Controls.Add(Me.lblAbNormal5)
        Me.Panel3.Controls.Add(Me.lblAbNormal6)
        Me.Panel3.Location = New System.Drawing.Point(51, 350)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1839, 163)
        Me.Panel3.TabIndex = 41
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lblState8)
        Me.Panel2.Controls.Add(Me.lblState7)
        Me.Panel2.Controls.Add(Me.lblState6)
        Me.Panel2.Controls.Add(Me.lblState5)
        Me.Panel2.Controls.Add(Me.lblState4)
        Me.Panel2.Controls.Add(Me.lblState3)
        Me.Panel2.Controls.Add(Me.lblState2)
        Me.Panel2.Controls.Add(Me.lblState1)
        Me.Panel2.Location = New System.Drawing.Point(51, 534)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1839, 76)
        Me.Panel2.TabIndex = 42
        '
        'btnUpdInfo
        '
        Me.btnUpdInfo.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnUpdInfo.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnUpdInfo.ForeColor = System.Drawing.Color.White
        Me.btnUpdInfo.Location = New System.Drawing.Point(906, 30)
        Me.btnUpdInfo.Name = "btnUpdInfo"
        Me.btnUpdInfo.Size = New System.Drawing.Size(285, 116)
        Me.btnUpdInfo.TabIndex = 45
        Me.btnUpdInfo.Text = "情報更新"
        Me.btnUpdInfo.UseVisualStyleBackColor = False
        '
        'lblAbNormal11
        '
        Me.lblAbNormal11.AutoSize = True
        Me.lblAbNormal11.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal11.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal11.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal11.Location = New System.Drawing.Point(912, 109)
        Me.lblAbNormal11.Name = "lblAbNormal11"
        Me.lblAbNormal11.Size = New System.Drawing.Size(269, 35)
        Me.lblAbNormal11.TabIndex = 58
        Me.lblAbNormal11.Text = "パルススイッチ異常"
        Me.lblAbNormal11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal10
        '
        Me.lblAbNormal10.AutoSize = True
        Me.lblAbNormal10.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal10.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal10.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal10.Location = New System.Drawing.Point(578, 109)
        Me.lblAbNormal10.Name = "lblAbNormal10"
        Me.lblAbNormal10.Size = New System.Drawing.Size(313, 35)
        Me.lblAbNormal10.TabIndex = 57
        Me.lblAbNormal10.Text = "セーフティスイッチ異常"
        Me.lblAbNormal10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal9
        '
        Me.lblAbNormal9.AutoSize = True
        Me.lblAbNormal9.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal9.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal9.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal9.Location = New System.Drawing.Point(295, 109)
        Me.lblAbNormal9.Name = "lblAbNormal9"
        Me.lblAbNormal9.Size = New System.Drawing.Size(230, 35)
        Me.lblAbNormal9.TabIndex = 56
        Me.lblAbNormal9.Text = "コイン払出不良"
        Me.lblAbNormal9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal8
        '
        Me.lblAbNormal8.AutoSize = True
        Me.lblAbNormal8.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal8.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal8.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal8.Location = New System.Drawing.Point(20, 109)
        Me.lblAbNormal8.Name = "lblAbNormal8"
        Me.lblAbNormal8.Size = New System.Drawing.Size(254, 35)
        Me.lblAbNormal8.TabIndex = 55
        Me.lblAbNormal8.Text = "返却スイッチ異常"
        Me.lblAbNormal8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal7
        '
        Me.lblAbNormal7.AutoSize = True
        Me.lblAbNormal7.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal7.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal7.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal7.Location = New System.Drawing.Point(912, 60)
        Me.lblAbNormal7.Name = "lblAbNormal7"
        Me.lblAbNormal7.Size = New System.Drawing.Size(279, 35)
        Me.lblAbNormal7.TabIndex = 54
        Me.lblAbNormal7.Text = "500円ｴﾝﾌﾟﾃｨ異常"
        Me.lblAbNormal7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal6
        '
        Me.lblAbNormal6.AutoSize = True
        Me.lblAbNormal6.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal6.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal6.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal6.Location = New System.Drawing.Point(578, 60)
        Me.lblAbNormal6.Name = "lblAbNormal6"
        Me.lblAbNormal6.Size = New System.Drawing.Size(279, 35)
        Me.lblAbNormal6.TabIndex = 53
        Me.lblAbNormal6.Text = "100円ｴﾝﾌﾟﾃｨ異常"
        Me.lblAbNormal6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal5
        '
        Me.lblAbNormal5.AutoSize = True
        Me.lblAbNormal5.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal5.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal5.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal5.Location = New System.Drawing.Point(295, 60)
        Me.lblAbNormal5.Name = "lblAbNormal5"
        Me.lblAbNormal5.Size = New System.Drawing.Size(261, 35)
        Me.lblAbNormal5.TabIndex = 52
        Me.lblAbNormal5.Text = "50円ｴﾝﾌﾟﾃｨ異常"
        Me.lblAbNormal5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal4
        '
        Me.lblAbNormal4.AutoSize = True
        Me.lblAbNormal4.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal4.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal4.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal4.Location = New System.Drawing.Point(13, 60)
        Me.lblAbNormal4.Name = "lblAbNormal4"
        Me.lblAbNormal4.Size = New System.Drawing.Size(261, 35)
        Me.lblAbNormal4.TabIndex = 51
        Me.lblAbNormal4.Text = "10円ｴﾝﾌﾟﾃｨ異常"
        Me.lblAbNormal4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal3
        '
        Me.lblAbNormal3.AutoSize = True
        Me.lblAbNormal3.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal3.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal3.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal3.Location = New System.Drawing.Point(330, 9)
        Me.lblAbNormal3.Name = "lblAbNormal3"
        Me.lblAbNormal3.Size = New System.Drawing.Size(243, 35)
        Me.lblAbNormal3.TabIndex = 50
        Me.lblAbNormal3.Text = "アクセプター異常"
        Me.lblAbNormal3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal2
        '
        Me.lblAbNormal2.AutoSize = True
        Me.lblAbNormal2.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal2.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal2.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal2.Location = New System.Drawing.Point(195, 9)
        Me.lblAbNormal2.Name = "lblAbNormal2"
        Me.lblAbNormal2.Size = New System.Drawing.Size(120, 35)
        Me.lblAbNormal2.TabIndex = 49
        Me.lblAbNormal2.Text = "動作可"
        Me.lblAbNormal2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbNormal1
        '
        Me.lblAbNormal1.AutoSize = True
        Me.lblAbNormal1.BackColor = System.Drawing.Color.Transparent
        Me.lblAbNormal1.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAbNormal1.ForeColor = System.Drawing.Color.Silver
        Me.lblAbNormal1.Location = New System.Drawing.Point(13, 9)
        Me.lblAbNormal1.Name = "lblAbNormal1"
        Me.lblAbNormal1.Size = New System.Drawing.Size(155, 35)
        Me.lblAbNormal1.TabIndex = 48
        Me.lblAbNormal1.Text = "代表異常"
        Me.lblAbNormal1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState8
        '
        Me.lblState8.AutoSize = True
        Me.lblState8.BackColor = System.Drawing.Color.Transparent
        Me.lblState8.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState8.ForeColor = System.Drawing.Color.Silver
        Me.lblState8.Location = New System.Drawing.Point(1556, 16)
        Me.lblState8.Name = "lblState8"
        Me.lblState8.Size = New System.Drawing.Size(246, 35)
        Me.lblState8.TabIndex = 43
        Me.lblState8.Text = "つり銭あわせ SW"
        Me.lblState8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState7
        '
        Me.lblState7.AutoSize = True
        Me.lblState7.BackColor = System.Drawing.Color.Transparent
        Me.lblState7.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState7.ForeColor = System.Drawing.Color.Silver
        Me.lblState7.Location = New System.Drawing.Point(1301, 16)
        Me.lblState7.Name = "lblState7"
        Me.lblState7.Size = New System.Drawing.Size(237, 35)
        Me.lblState7.TabIndex = 42
        Me.lblState7.Text = "インベントリ状態"
        Me.lblState7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState6
        '
        Me.lblState6.AutoSize = True
        Me.lblState6.BackColor = System.Drawing.Color.Transparent
        Me.lblState6.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState6.ForeColor = System.Drawing.Color.Silver
        Me.lblState6.Location = New System.Drawing.Point(1130, 16)
        Me.lblState6.Name = "lblState6"
        Me.lblState6.Size = New System.Drawing.Size(152, 35)
        Me.lblState6.TabIndex = 41
        Me.lblState6.Text = "クリア済み"
        Me.lblState6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState5
        '
        Me.lblState5.AutoSize = True
        Me.lblState5.BackColor = System.Drawing.Color.Transparent
        Me.lblState5.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState5.ForeColor = System.Drawing.Color.Silver
        Me.lblState5.Location = New System.Drawing.Point(953, 16)
        Me.lblState5.Name = "lblState5"
        Me.lblState5.Size = New System.Drawing.Size(155, 35)
        Me.lblState5.TabIndex = 40
        Me.lblState5.Text = "払出終了"
        Me.lblState5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState4
        '
        Me.lblState4.AutoSize = True
        Me.lblState4.BackColor = System.Drawing.Color.Transparent
        Me.lblState4.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState4.ForeColor = System.Drawing.Color.Silver
        Me.lblState4.Location = New System.Drawing.Point(689, 16)
        Me.lblState4.Name = "lblState4"
        Me.lblState4.Size = New System.Drawing.Size(242, 35)
        Me.lblState4.TabIndex = 39
        Me.lblState4.Text = "返却スイッチ ON"
        Me.lblState4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState3
        '
        Me.lblState3.AutoSize = True
        Me.lblState3.BackColor = System.Drawing.Color.Transparent
        Me.lblState3.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState3.ForeColor = System.Drawing.Color.Silver
        Me.lblState3.Location = New System.Drawing.Point(427, 16)
        Me.lblState3.Name = "lblState3"
        Me.lblState3.Size = New System.Drawing.Size(239, 35)
        Me.lblState3.TabIndex = 38
        Me.lblState3.Text = "つり銭払出可能"
        Me.lblState3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState2
        '
        Me.lblState2.AutoSize = True
        Me.lblState2.BackColor = System.Drawing.Color.Transparent
        Me.lblState2.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState2.ForeColor = System.Drawing.Color.Silver
        Me.lblState2.Location = New System.Drawing.Point(206, 16)
        Me.lblState2.Name = "lblState2"
        Me.lblState2.Size = New System.Drawing.Size(202, 35)
        Me.lblState2.TabIndex = 37
        Me.lblState2.Text = "インベントリ中"
        Me.lblState2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblState1
        '
        Me.lblState1.AutoSize = True
        Me.lblState1.BackColor = System.Drawing.Color.Transparent
        Me.lblState1.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblState1.ForeColor = System.Drawing.Color.Silver
        Me.lblState1.Location = New System.Drawing.Point(20, 16)
        Me.lblState1.Name = "lblState1"
        Me.lblState1.Size = New System.Drawing.Size(164, 35)
        Me.lblState1.TabIndex = 36
        Me.lblState1.Text = "CREM ON"
        Me.lblState1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.lbl100yen)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.lbl10yen)
        Me.Panel4.Location = New System.Drawing.Point(51, 250)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1839, 76)
        Me.Panel4.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Silver
        Me.Label1.Location = New System.Drawing.Point(1556, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 35)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "つり銭あわせ SW"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl10yen
        '
        Me.lbl10yen.AutoSize = True
        Me.lbl10yen.BackColor = System.Drawing.Color.Transparent
        Me.lbl10yen.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl10yen.ForeColor = System.Drawing.Color.Silver
        Me.lbl10yen.Location = New System.Drawing.Point(20, 16)
        Me.lbl10yen.Name = "lbl10yen"
        Me.lbl10yen.Size = New System.Drawing.Size(152, 35)
        Me.lbl10yen.TabIndex = 36
        Me.lbl10yen.Text = "10円切れ"
        Me.lbl10yen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl100yen
        '
        Me.lbl100yen.AutoSize = True
        Me.lbl100yen.BackColor = System.Drawing.Color.Transparent
        Me.lbl100yen.Font = New System.Drawing.Font("MS UI Gothic", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbl100yen.ForeColor = System.Drawing.Color.Silver
        Me.lbl100yen.Location = New System.Drawing.Point(195, 16)
        Me.lbl100yen.Name = "lbl100yen"
        Me.lbl100yen.Size = New System.Drawing.Size(170, 35)
        Me.lbl100yen.TabIndex = 44
        Me.lbl100yen.Text = "100円切れ"
        Me.lbl100yen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmCM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnUpdInfo)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCM"
        Me.Text = "frmCM"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblConnect As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnUpdInfo As System.Windows.Forms.Button
    Friend WithEvents lblAbNormal11 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal10 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal9 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal8 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal7 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal6 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal5 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal4 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal3 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal2 As System.Windows.Forms.Label
    Friend WithEvents lblAbNormal1 As System.Windows.Forms.Label
    Friend WithEvents lblState8 As System.Windows.Forms.Label
    Friend WithEvents lblState7 As System.Windows.Forms.Label
    Friend WithEvents lblState6 As System.Windows.Forms.Label
    Friend WithEvents lblState5 As System.Windows.Forms.Label
    Friend WithEvents lblState4 As System.Windows.Forms.Label
    Friend WithEvents lblState3 As System.Windows.Forms.Label
    Friend WithEvents lblState2 As System.Windows.Forms.Label
    Friend WithEvents lblState1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lbl100yen As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl10yen As System.Windows.Forms.Label
End Class
