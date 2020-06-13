<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectCharge
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblShitei = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnC = New System.Windows.Forms.Button()
        Me.btn0 = New System.Windows.Forms.Button()
        Me.btn9 = New System.Windows.Forms.Button()
        Me.btn8 = New System.Windows.Forms.Button()
        Me.btn7 = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.btnSelectSitei = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblShitei)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnC)
        Me.Panel1.Controls.Add(Me.btn0)
        Me.Panel1.Controls.Add(Me.btn9)
        Me.Panel1.Controls.Add(Me.btn8)
        Me.Panel1.Controls.Add(Me.btn7)
        Me.Panel1.Controls.Add(Me.btn6)
        Me.Panel1.Controls.Add(Me.btn5)
        Me.Panel1.Controls.Add(Me.btn4)
        Me.Panel1.Controls.Add(Me.btn3)
        Me.Panel1.Controls.Add(Me.btn2)
        Me.Panel1.Controls.Add(Me.btn1)
        Me.Panel1.Controls.Add(Me.btnSelectSitei)
        Me.Panel1.Location = New System.Drawing.Point(25, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 729)
        Me.Panel1.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Gray
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnOK.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(500, 253)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(212, 106)
        Me.btnOK.TabIndex = 233
        Me.btnOK.Tag = "0"
        Me.btnOK.Text = "決定"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(25, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(687, 44)
        Me.Label3.TabIndex = 232
        Me.Label3.Text = "※20,000円を超える指定はできません※"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(25, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(687, 44)
        Me.Label2.TabIndex = 231
        Me.Label2.Text = "※金額指定での入金はﾌﾟﾚﾐｱﾑ・ﾎﾟｲﾝﾄは付与されません※"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(148, 97)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 44)
        Me.Label7.TabIndex = 230
        Me.Label7.Text = "入金額"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblShitei
        '
        Me.lblShitei.BackColor = System.Drawing.Color.White
        Me.lblShitei.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShitei.Location = New System.Drawing.Point(142, 143)
        Me.lblShitei.Name = "lblShitei"
        Me.lblShitei.Size = New System.Drawing.Size(225, 93)
        Me.lblShitei.TabIndex = 225
        Me.lblShitei.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(373, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 93)
        Me.Label1.TabIndex = 224
        Me.Label1.Text = "円"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnC
        '
        Me.btnC.BackColor = System.Drawing.Color.Gray
        Me.btnC.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnC.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnC.ForeColor = System.Drawing.Color.White
        Me.btnC.Location = New System.Drawing.Point(207, 595)
        Me.btnC.Name = "btnC"
        Me.btnC.Size = New System.Drawing.Size(260, 106)
        Me.btnC.TabIndex = 222
        Me.btnC.Tag = "0"
        Me.btnC.Text = "クリア"
        Me.btnC.UseVisualStyleBackColor = False
        '
        'btn0
        '
        Me.btn0.BackColor = System.Drawing.Color.Gray
        Me.btn0.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn0.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn0.ForeColor = System.Drawing.Color.White
        Me.btn0.Location = New System.Drawing.Point(73, 595)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(126, 106)
        Me.btn0.TabIndex = 221
        Me.btn0.Tag = "0"
        Me.btn0.Text = "0"
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.BackColor = System.Drawing.Color.Gray
        Me.btn9.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn9.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn9.ForeColor = System.Drawing.Color.White
        Me.btn9.Location = New System.Drawing.Point(341, 481)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(126, 106)
        Me.btn9.TabIndex = 220
        Me.btn9.Tag = "0"
        Me.btn9.Text = "9"
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.Gray
        Me.btn8.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn8.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn8.ForeColor = System.Drawing.Color.White
        Me.btn8.Location = New System.Drawing.Point(207, 481)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(126, 106)
        Me.btn8.TabIndex = 219
        Me.btn8.Tag = "0"
        Me.btn8.Text = "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.Gray
        Me.btn7.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn7.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn7.ForeColor = System.Drawing.Color.White
        Me.btn7.Location = New System.Drawing.Point(73, 481)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(126, 106)
        Me.btn7.TabIndex = 218
        Me.btn7.Tag = "0"
        Me.btn7.Text = "7"
        Me.btn7.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.Gray
        Me.btn6.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn6.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn6.ForeColor = System.Drawing.Color.White
        Me.btn6.Location = New System.Drawing.Point(341, 367)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(126, 106)
        Me.btn6.TabIndex = 217
        Me.btn6.Tag = "0"
        Me.btn6.Text = "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.Gray
        Me.btn5.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn5.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn5.ForeColor = System.Drawing.Color.White
        Me.btn5.Location = New System.Drawing.Point(207, 367)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(126, 106)
        Me.btn5.TabIndex = 216
        Me.btn5.Tag = "0"
        Me.btn5.Text = "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.Gray
        Me.btn4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn4.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn4.ForeColor = System.Drawing.Color.White
        Me.btn4.Location = New System.Drawing.Point(73, 367)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(126, 106)
        Me.btn4.TabIndex = 215
        Me.btn4.Tag = "0"
        Me.btn4.Text = "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.Gray
        Me.btn3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn3.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn3.ForeColor = System.Drawing.Color.White
        Me.btn3.Location = New System.Drawing.Point(341, 253)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(126, 106)
        Me.btn3.TabIndex = 214
        Me.btn3.Tag = "0"
        Me.btn3.Text = "3"
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.Gray
        Me.btn2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn2.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn2.ForeColor = System.Drawing.Color.White
        Me.btn2.Location = New System.Drawing.Point(207, 253)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(126, 106)
        Me.btn2.TabIndex = 213
        Me.btn2.Tag = "0"
        Me.btn2.Text = "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.BackColor = System.Drawing.Color.Gray
        Me.btn1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn1.ForeColor = System.Drawing.Color.White
        Me.btn1.Location = New System.Drawing.Point(73, 253)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(126, 106)
        Me.btn1.TabIndex = 212
        Me.btn1.Tag = "0"
        Me.btn1.Text = "1"
        Me.btn1.UseVisualStyleBackColor = False
        '
        'btnSelectSitei
        '
        Me.btnSelectSitei.BackColor = System.Drawing.Color.Gray
        Me.btnSelectSitei.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSelectSitei.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelectSitei.ForeColor = System.Drawing.Color.White
        Me.btnSelectSitei.Location = New System.Drawing.Point(500, 595)
        Me.btnSelectSitei.Name = "btnSelectSitei"
        Me.btnSelectSitei.Size = New System.Drawing.Size(212, 106)
        Me.btnSelectSitei.TabIndex = 211
        Me.btnSelectSitei.Tag = "0"
        Me.btnSelectSitei.Text = "閉じる"
        Me.btnSelectSitei.UseVisualStyleBackColor = False
        '
        'frmSelectCharge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.RoyalBlue
        Me.ClientSize = New System.Drawing.Size(780, 781)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSelectCharge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSelectCharge"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnC As System.Windows.Forms.Button
    Friend WithEvents btn0 As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents btnSelectSitei As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblShitei As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
