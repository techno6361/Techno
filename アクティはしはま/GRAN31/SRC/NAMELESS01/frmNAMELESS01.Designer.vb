<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNAMELESS01
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
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbNCSRANK = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnCard = New System.Windows.Forms.Button()
        Me.lblCARDNO = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCCSKANA = New System.Windows.Forms.TextBox()
        Me.txtCCSNAME = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDMEMBER = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.chkHakko = New System.Windows.Forms.CheckBox()
        Me.txtNCSNO = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.DarkGreen
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(22, 207)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(192, 35)
        Me.Label17.TabIndex = 238
        Me.Label17.Text = "顧客種別"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbNCSRANK
        '
        Me.cmbNCSRANK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNCSRANK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNCSRANK.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNCSRANK.FormattingEnabled = True
        Me.cmbNCSRANK.Location = New System.Drawing.Point(216, 208)
        Me.cmbNCSRANK.Name = "cmbNCSRANK"
        Me.cmbNCSRANK.Size = New System.Drawing.Size(246, 35)
        Me.cmbNCSRANK.TabIndex = 237
        Me.cmbNCSRANK.TabStop = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.DarkGreen
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(22, 128)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(192, 34)
        Me.Label22.TabIndex = 239
        Me.Label22.Text = "顧客番号"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCard
        '
        Me.btnCard.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.btnCard.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCard.ForeColor = System.Drawing.Color.White
        Me.btnCard.Location = New System.Drawing.Point(159, 451)
        Me.btnCard.Name = "btnCard"
        Me.btnCard.Size = New System.Drawing.Size(165, 74)
        Me.btnCard.TabIndex = 248
        Me.btnCard.Text = "作成"
        Me.btnCard.UseVisualStyleBackColor = False
        '
        'lblCARDNO
        '
        Me.lblCARDNO.BackColor = System.Drawing.Color.White
        Me.lblCARDNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCARDNO.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCARDNO.ForeColor = System.Drawing.Color.Black
        Me.lblCARDNO.Location = New System.Drawing.Point(215, 167)
        Me.lblCARDNO.Name = "lblCARDNO"
        Me.lblCARDNO.Size = New System.Drawing.Size(142, 34)
        Me.lblCARDNO.TabIndex = 243
        Me.lblCARDNO.Text = "99999999"
        Me.lblCARDNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCARDNO.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(22, 167)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 34)
        Me.Label2.TabIndex = 242
        Me.Label2.Text = "カード番号"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.DarkGreen
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(22, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 34)
        Me.Label1.TabIndex = 244
        Me.Label1.Text = "氏名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCCSKANA
        '
        Me.txtCCSKANA.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCCSKANA.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtCCSKANA.Location = New System.Drawing.Point(216, 289)
        Me.txtCCSKANA.MaxLength = 20
        Me.txtCCSKANA.Name = "txtCCSKANA"
        Me.txtCCSKANA.Size = New System.Drawing.Size(349, 34)
        Me.txtCCSKANA.TabIndex = 246
        Me.txtCCSKANA.Text = "ｱｱｱｱｱｱｱｱｱｱｱｱｱｱｱｱｱｱｱｱ"
        '
        'txtCCSNAME
        '
        Me.txtCCSNAME.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCCSNAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtCCSNAME.Location = New System.Drawing.Point(215, 248)
        Me.txtCCSNAME.MaxLength = 10
        Me.txtCCSNAME.Name = "txtCCSNAME"
        Me.txtCCSNAME.Size = New System.Drawing.Size(246, 34)
        Me.txtCCSNAME.TabIndex = 245
        Me.txtCCSNAME.Text = "ああああああああああ"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(22, 288)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 34)
        Me.Label3.TabIndex = 247
        Me.Label3.Text = "氏名カナ"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDMEMBER
        '
        Me.txtDMEMBER.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDMEMBER.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtDMEMBER.Location = New System.Drawing.Point(216, 329)
        Me.txtDMEMBER.MaxLength = 8
        Me.txtDMEMBER.Name = "txtDMEMBER"
        Me.txtDMEMBER.Size = New System.Drawing.Size(165, 34)
        Me.txtDMEMBER.TabIndex = 247
        Me.txtDMEMBER.Text = "1985/12/03"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.DarkGreen
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("MS UI Gothic", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(22, 329)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(192, 34)
        Me.Label19.TabIndex = 267
        Me.Label19.Text = "会員期限"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkHakko
        '
        Me.chkHakko.AutoSize = True
        Me.chkHakko.Checked = True
        Me.chkHakko.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHakko.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHakko.Location = New System.Drawing.Point(318, 384)
        Me.chkHakko.Name = "chkHakko"
        Me.chkHakko.Size = New System.Drawing.Size(247, 31)
        Me.chkHakko.TabIndex = 620
        Me.chkHakko.Text = "発行料入金機精算"
        Me.chkHakko.UseVisualStyleBackColor = True
        '
        'txtNCSNO
        '
        Me.txtNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNCSNO.Location = New System.Drawing.Point(215, 127)
        Me.txtNCSNO.MaxLength = 8
        Me.txtNCSNO.Name = "txtNCSNO"
        Me.txtNCSNO.Size = New System.Drawing.Size(121, 34)
        Me.txtNCSNO.TabIndex = 621
        Me.txtNCSNO.Text = "99999999"
        '
        'frmNAMELESS01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 762)
        Me.Controls.Add(Me.txtNCSNO)
        Me.Controls.Add(Me.chkHakko)
        Me.Controls.Add(Me.txtDMEMBER)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCCSKANA)
        Me.Controls.Add(Me.txtCCSNAME)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCARDNO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnCard)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.cmbNCSRANK)
        Me.MaximumSize = New System.Drawing.Size(600, 800)
        Me.MinimumSize = New System.Drawing.Size(600, 800)
        Me.Name = "frmNAMELESS01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.cmbNCSRANK, 0)
        Me.Controls.SetChildIndex(Me.Label17, 0)
        Me.Controls.SetChildIndex(Me.Label22, 0)
        Me.Controls.SetChildIndex(Me.btnCard, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.lblCARDNO, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtCCSNAME, 0)
        Me.Controls.SetChildIndex(Me.txtCCSKANA, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label19, 0)
        Me.Controls.SetChildIndex(Me.txtDMEMBER, 0)
        Me.Controls.SetChildIndex(Me.chkHakko, 0)
        Me.Controls.SetChildIndex(Me.txtNCSNO, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbNCSRANK As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnCard As System.Windows.Forms.Button
    Friend WithEvents lblCARDNO As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCCSKANA As System.Windows.Forms.TextBox
    Friend WithEvents txtCCSNAME As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDMEMBER As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkHakko As System.Windows.Forms.CheckBox
    Friend WithEvents txtNCSNO As System.Windows.Forms.TextBox

End Class
