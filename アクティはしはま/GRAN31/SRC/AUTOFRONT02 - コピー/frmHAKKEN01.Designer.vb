<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHAKKEN01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHAKKEN01))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblConnect = New System.Windows.Forms.Label()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblC0 = New System.Windows.Forms.Label()
        Me.lblNONE2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblB3 = New System.Windows.Forms.Label()
        Me.lblB2 = New System.Windows.Forms.Label()
        Me.lblB1 = New System.Windows.Forms.Label()
        Me.lblB0 = New System.Windows.Forms.Label()
        Me.lblNONE1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblA2 = New System.Windows.Forms.Label()
        Me.lblA3 = New System.Windows.Forms.Label()
        Me.lblA1 = New System.Windows.Forms.Label()
        Me.lblA0 = New System.Windows.Forms.Label()
        Me.lblNONE0 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblD3 = New System.Windows.Forms.Label()
        Me.lblD2 = New System.Windows.Forms.Label()
        Me.lblD1 = New System.Windows.Forms.Label()
        Me.lblD0 = New System.Windows.Forms.Label()
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
        Me.Panel1.Location = New System.Drawing.Point(36, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(193, 110)
        Me.Panel1.TabIndex = 39
        '
        'lblConnect
        '
        Me.lblConnect.AutoSize = True
        Me.lblConnect.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblConnect.ForeColor = System.Drawing.Color.Silver
        Me.lblConnect.Location = New System.Drawing.Point(28, 33)
        Me.lblConnect.Name = "lblConnect"
        Me.lblConnect.Size = New System.Drawing.Size(137, 40)
        Me.lblConnect.TabIndex = 0
        Me.lblConnect.Text = "接続中"
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.ForestGreen
        Me.btnConnect.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(939, 18)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(147, 116)
        Me.btnConnect.TabIndex = 41
        Me.btnConnect.Text = "接続"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkGray
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1107, 18)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(147, 116)
        Me.btnBack.TabIndex = 40
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnTest
        '
        Me.btnTest.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnTest.Enabled = False
        Me.btnTest.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTest.ForeColor = System.Drawing.Color.White
        Me.btnTest.Location = New System.Drawing.Point(569, 12)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(147, 116)
        Me.btnTest.TabIndex = 44
        Me.btnTest.Text = "テスト発券"
        Me.btnTest.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lblC0)
        Me.Panel2.Controls.Add(Me.lblNONE2)
        Me.Panel2.Location = New System.Drawing.Point(98, 445)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1018, 110)
        Me.Panel2.TabIndex = 45
        '
        'lblC0
        '
        Me.lblC0.AutoSize = True
        Me.lblC0.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblC0.ForeColor = System.Drawing.Color.Silver
        Me.lblC0.Location = New System.Drawing.Point(11, 34)
        Me.lblC0.Name = "lblC0"
        Me.lblC0.Size = New System.Drawing.Size(182, 34)
        Me.lblC0.TabIndex = 34
        Me.lblC0.Text = "CDエンプティ"
        '
        'lblNONE2
        '
        Me.lblNONE2.AutoSize = True
        Me.lblNONE2.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE2.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE2.Location = New System.Drawing.Point(813, 34)
        Me.lblNONE2.Name = "lblNONE2"
        Me.lblNONE2.Size = New System.Drawing.Size(152, 34)
        Me.lblNONE2.TabIndex = 35
        Me.lblNONE2.Text = "エラー無し"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.lblB3)
        Me.Panel3.Controls.Add(Me.lblB2)
        Me.Panel3.Controls.Add(Me.lblB1)
        Me.Panel3.Controls.Add(Me.lblB0)
        Me.Panel3.Controls.Add(Me.lblNONE1)
        Me.Panel3.Location = New System.Drawing.Point(98, 328)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1018, 110)
        Me.Panel3.TabIndex = 46
        '
        'lblB3
        '
        Me.lblB3.AutoSize = True
        Me.lblB3.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblB3.ForeColor = System.Drawing.Color.Silver
        Me.lblB3.Location = New System.Drawing.Point(601, 34)
        Me.lblB3.Name = "lblB3"
        Me.lblB3.Size = New System.Drawing.Size(151, 34)
        Me.lblB3.TabIndex = 49
        Me.lblB3.Text = "後方排出"
        '
        'lblB2
        '
        Me.lblB2.AutoSize = True
        Me.lblB2.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblB2.ForeColor = System.Drawing.Color.Silver
        Me.lblB2.Location = New System.Drawing.Point(408, 34)
        Me.lblB2.Name = "lblB2"
        Me.lblB2.Size = New System.Drawing.Size(146, 34)
        Me.lblB2.TabIndex = 37
        Me.lblB2.Text = "ユニット内"
        '
        'lblB1
        '
        Me.lblB1.AutoSize = True
        Me.lblB1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblB1.ForeColor = System.Drawing.Color.Silver
        Me.lblB1.Location = New System.Drawing.Point(213, 34)
        Me.lblB1.Name = "lblB1"
        Me.lblB1.Size = New System.Drawing.Size(138, 34)
        Me.lblB1.TabIndex = 36
        Me.lblB1.Text = "出入り口"
        '
        'lblB0
        '
        Me.lblB0.AutoSize = True
        Me.lblB0.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblB0.ForeColor = System.Drawing.Color.Silver
        Me.lblB0.Location = New System.Drawing.Point(11, 34)
        Me.lblB0.Name = "lblB0"
        Me.lblB0.Size = New System.Drawing.Size(136, 34)
        Me.lblB0.TabIndex = 34
        Me.lblB0.Text = "コンタクト"
        '
        'lblNONE1
        '
        Me.lblNONE1.AutoSize = True
        Me.lblNONE1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE1.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE1.Location = New System.Drawing.Point(813, 34)
        Me.lblNONE1.Name = "lblNONE1"
        Me.lblNONE1.Size = New System.Drawing.Size(152, 34)
        Me.lblNONE1.TabIndex = 35
        Me.lblNONE1.Text = "エラー無し"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.lblA2)
        Me.Panel4.Controls.Add(Me.lblA3)
        Me.Panel4.Controls.Add(Me.lblA1)
        Me.Panel4.Controls.Add(Me.lblA0)
        Me.Panel4.Controls.Add(Me.lblNONE0)
        Me.Panel4.Location = New System.Drawing.Point(98, 209)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1018, 110)
        Me.Panel4.TabIndex = 47
        '
        'lblA2
        '
        Me.lblA2.AutoSize = True
        Me.lblA2.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblA2.ForeColor = System.Drawing.Color.Silver
        Me.lblA2.Location = New System.Drawing.Point(408, 34)
        Me.lblA2.Name = "lblA2"
        Me.lblA2.Size = New System.Drawing.Size(137, 34)
        Me.lblA2.TabIndex = 50
        Me.lblA2.Text = "シャッター"
        '
        'lblA3
        '
        Me.lblA3.AutoSize = True
        Me.lblA3.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblA3.ForeColor = System.Drawing.Color.Silver
        Me.lblA3.Location = New System.Drawing.Point(601, 34)
        Me.lblA3.Name = "lblA3"
        Me.lblA3.Size = New System.Drawing.Size(170, 34)
        Me.lblA3.TabIndex = 50
        Me.lblA3.Text = "その他異常"
        '
        'lblA1
        '
        Me.lblA1.AutoSize = True
        Me.lblA1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblA1.ForeColor = System.Drawing.Color.Silver
        Me.lblA1.Location = New System.Drawing.Point(213, 34)
        Me.lblA1.Name = "lblA1"
        Me.lblA1.Size = New System.Drawing.Size(120, 34)
        Me.lblA1.TabIndex = 49
        Me.lblA1.Text = "タンパー"
        '
        'lblA0
        '
        Me.lblA0.AutoSize = True
        Me.lblA0.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblA0.ForeColor = System.Drawing.Color.Silver
        Me.lblA0.Location = New System.Drawing.Point(11, 34)
        Me.lblA0.Name = "lblA0"
        Me.lblA0.Size = New System.Drawing.Size(147, 34)
        Me.lblA0.TabIndex = 48
        Me.lblA0.Text = "カード詰り"
        '
        'lblNONE0
        '
        Me.lblNONE0.AutoSize = True
        Me.lblNONE0.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE0.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE0.Location = New System.Drawing.Point(813, 34)
        Me.lblNONE0.Name = "lblNONE0"
        Me.lblNONE0.Size = New System.Drawing.Size(152, 34)
        Me.lblNONE0.TabIndex = 35
        Me.lblNONE0.Text = "エラー無し"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.lblD3)
        Me.Panel5.Controls.Add(Me.lblD2)
        Me.Panel5.Controls.Add(Me.lblD1)
        Me.Panel5.Controls.Add(Me.lblD0)
        Me.Panel5.Controls.Add(Me.lblNONE3)
        Me.Panel5.Location = New System.Drawing.Point(98, 563)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1018, 110)
        Me.Panel5.TabIndex = 48
        '
        'lblD3
        '
        Me.lblD3.AutoSize = True
        Me.lblD3.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblD3.ForeColor = System.Drawing.Color.Silver
        Me.lblD3.Location = New System.Drawing.Point(601, 34)
        Me.lblD3.Name = "lblD3"
        Me.lblD3.Size = New System.Drawing.Size(147, 34)
        Me.lblD3.TabIndex = 48
        Me.lblD3.Text = "センサー４"
        '
        'lblD2
        '
        Me.lblD2.AutoSize = True
        Me.lblD2.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblD2.ForeColor = System.Drawing.Color.Silver
        Me.lblD2.Location = New System.Drawing.Point(408, 34)
        Me.lblD2.Name = "lblD2"
        Me.lblD2.Size = New System.Drawing.Size(147, 34)
        Me.lblD2.TabIndex = 48
        Me.lblD2.Text = "センサー３"
        '
        'lblD1
        '
        Me.lblD1.AutoSize = True
        Me.lblD1.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblD1.ForeColor = System.Drawing.Color.Silver
        Me.lblD1.Location = New System.Drawing.Point(213, 34)
        Me.lblD1.Name = "lblD1"
        Me.lblD1.Size = New System.Drawing.Size(147, 34)
        Me.lblD1.TabIndex = 36
        Me.lblD1.Text = "センサー２"
        '
        'lblD0
        '
        Me.lblD0.AutoSize = True
        Me.lblD0.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblD0.ForeColor = System.Drawing.Color.Silver
        Me.lblD0.Location = New System.Drawing.Point(11, 34)
        Me.lblD0.Name = "lblD0"
        Me.lblD0.Size = New System.Drawing.Size(147, 34)
        Me.lblD0.TabIndex = 34
        Me.lblD0.Text = "センサー１"
        '
        'lblNONE3
        '
        Me.lblNONE3.AutoSize = True
        Me.lblNONE3.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNONE3.ForeColor = System.Drawing.Color.Silver
        Me.lblNONE3.Location = New System.Drawing.Point(813, 34)
        Me.lblNONE3.Name = "lblNONE3"
        Me.lblNONE3.Size = New System.Drawing.Size(152, 34)
        Me.lblNONE3.TabIndex = 35
        Me.lblNONE3.Text = "エラー無し"
        '
        'frmHAKKEN01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1280, 800)
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
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
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
    Friend WithEvents lblC0 As System.Windows.Forms.Label
    Friend WithEvents lblNONE2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblB0 As System.Windows.Forms.Label
    Friend WithEvents lblNONE1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblA1 As System.Windows.Forms.Label
    Friend WithEvents lblA0 As System.Windows.Forms.Label
    Friend WithEvents lblNONE0 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblD3 As System.Windows.Forms.Label
    Friend WithEvents lblD2 As System.Windows.Forms.Label
    Friend WithEvents lblD1 As System.Windows.Forms.Label
    Friend WithEvents lblD0 As System.Windows.Forms.Label
    Friend WithEvents lblNONE3 As System.Windows.Forms.Label
    Friend WithEvents lblB3 As System.Windows.Forms.Label
    Friend WithEvents lblB2 As System.Windows.Forms.Label
    Friend WithEvents lblB1 As System.Windows.Forms.Label
    Friend WithEvents lblA2 As System.Windows.Forms.Label
    Friend WithEvents lblA3 As System.Windows.Forms.Label
End Class
