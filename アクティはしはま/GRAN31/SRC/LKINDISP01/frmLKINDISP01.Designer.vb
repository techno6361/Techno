<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLKINDISP01
    Inherits TECHNO.FORM.BaseForm01

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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCARDLIMIT = New System.Windows.Forms.Label()
        Me.Label281 = New System.Windows.Forms.Label()
        Me.lblZANKN = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPREZANKN = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSRTPO = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblSRTPO2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPREZANKN2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblZANKN2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblSita = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPREMLIMIT = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Red
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(12, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(860, 50)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "カードの有効期限が切れています。残高情報をクリアしてよろしいですか？"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCARDLIMIT
        '
        Me.lblCARDLIMIT.BackColor = System.Drawing.Color.White
        Me.lblCARDLIMIT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCARDLIMIT.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCARDLIMIT.ForeColor = System.Drawing.Color.Red
        Me.lblCARDLIMIT.Location = New System.Drawing.Point(177, 193)
        Me.lblCARDLIMIT.Name = "lblCARDLIMIT"
        Me.lblCARDLIMIT.Size = New System.Drawing.Size(188, 52)
        Me.lblCARDLIMIT.TabIndex = 247
        Me.lblCARDLIMIT.Text = "1985/12/03"
        Me.lblCARDLIMIT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label281
        '
        Me.Label281.BackColor = System.Drawing.Color.LightCoral
        Me.Label281.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label281.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label281.ForeColor = System.Drawing.Color.White
        Me.Label281.Location = New System.Drawing.Point(12, 193)
        Me.Label281.Name = "Label281"
        Me.Label281.Size = New System.Drawing.Size(166, 52)
        Me.Label281.TabIndex = 246
        Me.Label281.Text = "ｶｰﾄﾞ有効期限"
        Me.Label281.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZANKN
        '
        Me.lblZANKN.BackColor = System.Drawing.Color.White
        Me.lblZANKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZANKN.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN.ForeColor = System.Drawing.Color.Black
        Me.lblZANKN.Location = New System.Drawing.Point(177, 260)
        Me.lblZANKN.Name = "lblZANKN"
        Me.lblZANKN.Size = New System.Drawing.Size(120, 52)
        Me.lblZANKN.TabIndex = 249
        Me.lblZANKN.Text = "99,999"
        Me.lblZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(12, 260)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(166, 52)
        Me.Label3.TabIndex = 248
        Me.Label3.Text = "残金額"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPREZANKN
        '
        Me.lblPREZANKN.BackColor = System.Drawing.Color.White
        Me.lblPREZANKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPREZANKN.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPREZANKN.ForeColor = System.Drawing.Color.Black
        Me.lblPREZANKN.Location = New System.Drawing.Point(468, 260)
        Me.lblPREZANKN.Name = "lblPREZANKN"
        Me.lblPREZANKN.Size = New System.Drawing.Size(120, 52)
        Me.lblPREZANKN.TabIndex = 251
        Me.lblPREZANKN.Text = "99,999"
        Me.lblPREZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(303, 260)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(166, 52)
        Me.Label5.TabIndex = 250
        Me.Label5.Text = "P)残金額"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSRTPO
        '
        Me.lblSRTPO.BackColor = System.Drawing.Color.White
        Me.lblSRTPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSRTPO.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO.ForeColor = System.Drawing.Color.Black
        Me.lblSRTPO.Location = New System.Drawing.Point(759, 260)
        Me.lblSRTPO.Name = "lblSRTPO"
        Me.lblSRTPO.Size = New System.Drawing.Size(120, 52)
        Me.lblSRTPO.TabIndex = 253
        Me.lblSRTPO.Text = "99,999"
        Me.lblSRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.DarkGreen
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(594, 260)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(166, 52)
        Me.Label7.TabIndex = 252
        Me.Label7.Text = "残ポイント"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Red
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(303, 454)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(264, 66)
        Me.btnOK.TabIndex = 254
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Gray
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(615, 454)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(264, 66)
        Me.btnCancel.TabIndex = 255
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblSRTPO2
        '
        Me.lblSRTPO2.BackColor = System.Drawing.Color.White
        Me.lblSRTPO2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSRTPO2.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSRTPO2.ForeColor = System.Drawing.Color.Black
        Me.lblSRTPO2.Location = New System.Drawing.Point(756, 55)
        Me.lblSRTPO2.Name = "lblSRTPO2"
        Me.lblSRTPO2.Size = New System.Drawing.Size(120, 52)
        Me.lblSRTPO2.TabIndex = 261
        Me.lblSRTPO2.Text = "99,999"
        Me.lblSRTPO2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkGreen
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(591, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(166, 52)
        Me.Label4.TabIndex = 260
        Me.Label4.Text = "残ポイント"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPREZANKN2
        '
        Me.lblPREZANKN2.BackColor = System.Drawing.Color.White
        Me.lblPREZANKN2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPREZANKN2.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPREZANKN2.ForeColor = System.Drawing.Color.Black
        Me.lblPREZANKN2.Location = New System.Drawing.Point(465, 55)
        Me.lblPREZANKN2.Name = "lblPREZANKN2"
        Me.lblPREZANKN2.Size = New System.Drawing.Size(120, 52)
        Me.lblPREZANKN2.TabIndex = 259
        Me.lblPREZANKN2.Text = "99,999"
        Me.lblPREZANKN2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.DarkGreen
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(300, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(166, 52)
        Me.Label8.TabIndex = 258
        Me.Label8.Text = "P)残金額"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZANKN2
        '
        Me.lblZANKN2.BackColor = System.Drawing.Color.White
        Me.lblZANKN2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZANKN2.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANKN2.ForeColor = System.Drawing.Color.Black
        Me.lblZANKN2.Location = New System.Drawing.Point(174, 55)
        Me.lblZANKN2.Name = "lblZANKN2"
        Me.lblZANKN2.Size = New System.Drawing.Size(120, 52)
        Me.lblZANKN2.TabIndex = 257
        Me.lblZANKN2.Text = "99,999"
        Me.lblZANKN2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkGreen
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(9, 55)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(166, 52)
        Me.Label10.TabIndex = 256
        Me.Label10.Text = "残金額"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSita
        '
        Me.lblSita.AutoSize = True
        Me.lblSita.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSita.Location = New System.Drawing.Point(447, 7)
        Me.lblSita.Name = "lblSita"
        Me.lblSita.Size = New System.Drawing.Size(57, 40)
        Me.lblSita.TabIndex = 262
        Me.lblSita.Text = "↓"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblSita)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lblSRTPO2)
        Me.Panel1.Controls.Add(Me.lblZANKN2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.lblPREZANKN2)
        Me.Panel1.Location = New System.Drawing.Point(4, 329)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(880, 112)
        Me.Panel1.TabIndex = 263
        '
        'lblPREMLIMIT
        '
        Me.lblPREMLIMIT.BackColor = System.Drawing.Color.White
        Me.lblPREMLIMIT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPREMLIMIT.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPREMLIMIT.ForeColor = System.Drawing.Color.Red
        Me.lblPREMLIMIT.Location = New System.Drawing.Point(623, 193)
        Me.lblPREMLIMIT.Name = "lblPREMLIMIT"
        Me.lblPREMLIMIT.Size = New System.Drawing.Size(188, 52)
        Me.lblPREMLIMIT.TabIndex = 265
        Me.lblPREMLIMIT.Text = "1985/12/03"
        Me.lblPREMLIMIT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.LightCoral
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(458, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(166, 52)
        Me.Label6.TabIndex = 264
        Me.Label6.Text = "入金残高有効期限"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmLKINDISP01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 562)
        Me.Controls.Add(Me.lblPREMLIMIT)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblSRTPO)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblPREZANKN)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblZANKN)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblCARDLIMIT)
        Me.Controls.Add(Me.Label281)
        Me.Controls.Add(Me.Label1)
        Me.MaximumSize = New System.Drawing.Size(900, 600)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "frmLKINDISP01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.TopMost = True
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label281, 0)
        Me.Controls.SetChildIndex(Me.lblCARDLIMIT, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.lblZANKN, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.lblPREZANKN, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.lblSRTPO, 0)
        Me.Controls.SetChildIndex(Me.btnOK, 0)
        Me.Controls.SetChildIndex(Me.btnCancel, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.lblPREMLIMIT, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCARDLIMIT As System.Windows.Forms.Label
    Friend WithEvents Label281 As System.Windows.Forms.Label
    Friend WithEvents lblZANKN As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblPREZANKN As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSRTPO As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblSRTPO2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblPREZANKN2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblZANKN2 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblSita As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblPREMLIMIT As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
