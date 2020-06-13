<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINTER01
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
        Me.lblConnect = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lblOffLine = New System.Windows.Forms.Label()
        Me.lblNone0 = New System.Windows.Forms.Label()
        Me.lblAutoCutterError = New System.Windows.Forms.Label()
        Me.lblAcError = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblNone1 = New System.Windows.Forms.Label()
        Me.lblAutoReturnDisable = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblPaperSensorError = New System.Windows.Forms.Label()
        Me.lblNone2 = New System.Windows.Forms.Label()
        Me.lblNearEnd = New System.Windows.Forms.Label()
        Me.lblPaperLess = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblNone3 = New System.Windows.Forms.Label()
        Me.lblPaperRemaining = New System.Windows.Forms.Label()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
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
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkGray
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1623, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(285, 116)
        Me.btnBack.TabIndex = 31
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.ForestGreen
        Me.btnConnect.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(1319, 12)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(285, 116)
        Me.btnConnect.TabIndex = 33
        Me.btnConnect.Text = "接続"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'lblOffLine
        '
        Me.lblOffLine.AutoSize = True
        Me.lblOffLine.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOffLine.ForeColor = System.Drawing.Color.Silver
        Me.lblOffLine.Location = New System.Drawing.Point(11, 26)
        Me.lblOffLine.Name = "lblOffLine"
        Me.lblOffLine.Size = New System.Drawing.Size(216, 54)
        Me.lblOffLine.TabIndex = 34
        Me.lblOffLine.Text = "オフライン"
        '
        'lblNone0
        '
        Me.lblNone0.AutoSize = True
        Me.lblNone0.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNone0.ForeColor = System.Drawing.Color.Silver
        Me.lblNone0.Location = New System.Drawing.Point(1245, 26)
        Me.lblNone0.Name = "lblNone0"
        Me.lblNone0.Size = New System.Drawing.Size(238, 54)
        Me.lblNone0.TabIndex = 35
        Me.lblNone0.Text = "エラー無し"
        '
        'lblAutoCutterError
        '
        Me.lblAutoCutterError.AutoSize = True
        Me.lblAutoCutterError.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAutoCutterError.ForeColor = System.Drawing.Color.Silver
        Me.lblAutoCutterError.Location = New System.Drawing.Point(11, 25)
        Me.lblAutoCutterError.Name = "lblAutoCutterError"
        Me.lblAutoCutterError.Size = New System.Drawing.Size(419, 54)
        Me.lblAutoCutterError.TabIndex = 36
        Me.lblAutoCutterError.Text = "オートカッターエラー"
        '
        'lblAcError
        '
        Me.lblAcError.AutoSize = True
        Me.lblAcError.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAcError.ForeColor = System.Drawing.Color.Silver
        Me.lblAcError.Location = New System.Drawing.Point(480, 25)
        Me.lblAcError.Name = "lblAcError"
        Me.lblAcError.Size = New System.Drawing.Size(253, 54)
        Me.lblAcError.TabIndex = 37
        Me.lblAcError.Text = "電圧エラー"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblConnect)
        Me.Panel1.Location = New System.Drawing.Point(95, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(261, 110)
        Me.Panel1.TabIndex = 38
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lblOffLine)
        Me.Panel2.Controls.Add(Me.lblNone0)
        Me.Panel2.Location = New System.Drawing.Point(95, 209)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1509, 110)
        Me.Panel2.TabIndex = 39
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.lblNone1)
        Me.Panel3.Controls.Add(Me.lblAutoReturnDisable)
        Me.Panel3.Controls.Add(Me.lblAutoCutterError)
        Me.Panel3.Controls.Add(Me.lblAcError)
        Me.Panel3.Location = New System.Drawing.Point(95, 368)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1509, 110)
        Me.Panel3.TabIndex = 40
        '
        'lblNone1
        '
        Me.lblNone1.AutoSize = True
        Me.lblNone1.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNone1.ForeColor = System.Drawing.Color.Silver
        Me.lblNone1.Location = New System.Drawing.Point(1245, 25)
        Me.lblNone1.Name = "lblNone1"
        Me.lblNone1.Size = New System.Drawing.Size(238, 54)
        Me.lblNone1.TabIndex = 43
        Me.lblNone1.Text = "エラー無し"
        '
        'lblAutoReturnDisable
        '
        Me.lblAutoReturnDisable.AutoSize = True
        Me.lblAutoReturnDisable.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAutoReturnDisable.ForeColor = System.Drawing.Color.Silver
        Me.lblAutoReturnDisable.Location = New System.Drawing.Point(802, 25)
        Me.lblAutoReturnDisable.Name = "lblAutoReturnDisable"
        Me.lblAutoReturnDisable.Size = New System.Drawing.Size(361, 54)
        Me.lblAutoReturnDisable.TabIndex = 42
        Me.lblAutoReturnDisable.Text = "自動復帰エラー"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.lblPaperSensorError)
        Me.Panel4.Controls.Add(Me.lblNone2)
        Me.Panel4.Controls.Add(Me.lblNearEnd)
        Me.Panel4.Controls.Add(Me.lblPaperLess)
        Me.Panel4.Location = New System.Drawing.Point(95, 527)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1509, 110)
        Me.Panel4.TabIndex = 41
        '
        'lblPaperSensorError
        '
        Me.lblPaperSensorError.AutoSize = True
        Me.lblPaperSensorError.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPaperSensorError.ForeColor = System.Drawing.Color.Silver
        Me.lblPaperSensorError.Location = New System.Drawing.Point(802, 25)
        Me.lblPaperSensorError.Name = "lblPaperSensorError"
        Me.lblPaperSensorError.Size = New System.Drawing.Size(131, 54)
        Me.lblPaperSensorError.TabIndex = 45
        Me.lblPaperSensorError.Text = "両方"
        '
        'lblNone2
        '
        Me.lblNone2.AutoSize = True
        Me.lblNone2.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNone2.ForeColor = System.Drawing.Color.Silver
        Me.lblNone2.Location = New System.Drawing.Point(1245, 25)
        Me.lblNone2.Name = "lblNone2"
        Me.lblNone2.Size = New System.Drawing.Size(238, 54)
        Me.lblNone2.TabIndex = 44
        Me.lblNone2.Text = "エラー無し"
        '
        'lblNearEnd
        '
        Me.lblNearEnd.AutoSize = True
        Me.lblNearEnd.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNearEnd.ForeColor = System.Drawing.Color.Silver
        Me.lblNearEnd.Location = New System.Drawing.Point(480, 25)
        Me.lblNearEnd.Name = "lblNearEnd"
        Me.lblNearEnd.Size = New System.Drawing.Size(224, 54)
        Me.lblNearEnd.TabIndex = 38
        Me.lblNearEnd.Text = "ニアエンド"
        '
        'lblPaperLess
        '
        Me.lblPaperLess.AutoSize = True
        Me.lblPaperLess.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPaperLess.ForeColor = System.Drawing.Color.Silver
        Me.lblPaperLess.Location = New System.Drawing.Point(11, 25)
        Me.lblPaperLess.Name = "lblPaperLess"
        Me.lblPaperLess.Size = New System.Drawing.Size(179, 54)
        Me.lblPaperLess.TabIndex = 37
        Me.lblPaperLess.Text = "紙切れ"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.lblNone3)
        Me.Panel5.Controls.Add(Me.lblPaperRemaining)
        Me.Panel5.Location = New System.Drawing.Point(95, 686)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1509, 110)
        Me.Panel5.TabIndex = 42
        '
        'lblNone3
        '
        Me.lblNone3.AutoSize = True
        Me.lblNone3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNone3.ForeColor = System.Drawing.Color.Silver
        Me.lblNone3.Location = New System.Drawing.Point(1245, 25)
        Me.lblNone3.Name = "lblNone3"
        Me.lblNone3.Size = New System.Drawing.Size(238, 54)
        Me.lblNone3.TabIndex = 44
        Me.lblNone3.Text = "エラー無し"
        '
        'lblPaperRemaining
        '
        Me.lblPaperRemaining.AutoSize = True
        Me.lblPaperRemaining.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPaperRemaining.ForeColor = System.Drawing.Color.Silver
        Me.lblPaperRemaining.Location = New System.Drawing.Point(11, 25)
        Me.lblPaperRemaining.Name = "lblPaperRemaining"
        Me.lblPaperRemaining.Size = New System.Drawing.Size(455, 54)
        Me.lblPaperRemaining.TabIndex = 37
        Me.lblPaperRemaining.Text = "プレゼンタに用紙あり"
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
        Me.btnTest.TabIndex = 43
        Me.btnTest.Text = "テスト印刷"
        Me.btnTest.UseVisualStyleBackColor = False
        '
        'frmPRINTER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPRINTER"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPRINTER"
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
    Friend WithEvents lblConnect As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents lblOffLine As System.Windows.Forms.Label
    Friend WithEvents lblNone0 As System.Windows.Forms.Label
    Friend WithEvents lblAutoCutterError As System.Windows.Forms.Label
    Friend WithEvents lblAcError As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblNone1 As System.Windows.Forms.Label
    Friend WithEvents lblAutoReturnDisable As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblNearEnd As System.Windows.Forms.Label
    Friend WithEvents lblPaperLess As System.Windows.Forms.Label
    Friend WithEvents lblNone2 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblNone3 As System.Windows.Forms.Label
    Friend WithEvents lblPaperRemaining As System.Windows.Forms.Label
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents lblPaperSensorError As System.Windows.Forms.Label
End Class
