<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRETURNSHOPNO
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
        Me.pnlTama = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtUSEKINGAKU = New BaseControl.CustomTextBoxNum()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtUSEBALL = New BaseControl.CustomTextBoxNum()
        Me.rdoBALL2F = New System.Windows.Forms.RadioButton()
        Me.rdoBALL1F = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBALL2F = New System.Windows.Forms.Label()
        Me.lblBALL1F = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label246 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pnlTime = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblZANTIME = New System.Windows.Forms.Label()
        Me.lblSelectTime = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPlayTime = New BaseControl.CustomTextBoxNum()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlTama.SuspendLayout()
        Me.pnlTime.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTama
        '
        Me.pnlTama.BackColor = System.Drawing.Color.FloralWhite
        Me.pnlTama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTama.Controls.Add(Me.Label9)
        Me.pnlTama.Controls.Add(Me.Label8)
        Me.pnlTama.Controls.Add(Me.txtUSEKINGAKU)
        Me.pnlTama.Controls.Add(Me.Label7)
        Me.pnlTama.Controls.Add(Me.txtUSEBALL)
        Me.pnlTama.Controls.Add(Me.rdoBALL2F)
        Me.pnlTama.Controls.Add(Me.rdoBALL1F)
        Me.pnlTama.Controls.Add(Me.Label6)
        Me.pnlTama.Controls.Add(Me.lblBALL2F)
        Me.pnlTama.Controls.Add(Me.lblBALL1F)
        Me.pnlTama.Controls.Add(Me.Label5)
        Me.pnlTama.Controls.Add(Me.Label4)
        Me.pnlTama.Controls.Add(Me.Label1)
        Me.pnlTama.Controls.Add(Me.Label246)
        Me.pnlTama.Location = New System.Drawing.Point(12, 9)
        Me.pnlTama.Name = "pnlTama"
        Me.pnlTama.Size = New System.Drawing.Size(1058, 163)
        Me.pnlTama.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(294, 110)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 20)
        Me.Label9.TabIndex = 250
        Me.Label9.Text = "円"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(294, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 20)
        Me.Label8.TabIndex = 249
        Me.Label8.Text = "円"
        '
        'txtUSEKINGAKU
        '
        Me.txtUSEKINGAKU.BackColor = System.Drawing.Color.White
        Me.txtUSEKINGAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSEKINGAKU.Font = New System.Drawing.Font("MS UI Gothic", 22.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUSEKINGAKU.Format = "#,##0"
        Me.txtUSEKINGAKU.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtUSEKINGAKU.Location = New System.Drawing.Point(806, 99)
        Me.txtUSEKINGAKU.MaxLength = 5
        Me.txtUSEKINGAKU.Name = "txtUSEKINGAKU"
        Me.txtUSEKINGAKU.Size = New System.Drawing.Size(112, 37)
        Me.txtUSEKINGAKU.TabIndex = 248
        Me.txtUSEKINGAKU.Text = "99,999"
        Me.txtUSEKINGAKU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.DarkGreen
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(674, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 37)
        Me.Label7.TabIndex = 247
        Me.Label7.Text = "使用金額"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUSEBALL
        '
        Me.txtUSEBALL.BackColor = System.Drawing.Color.White
        Me.txtUSEBALL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSEBALL.Font = New System.Drawing.Font("MS UI Gothic", 22.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUSEBALL.Format = "#,##0"
        Me.txtUSEBALL.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtUSEBALL.Location = New System.Drawing.Point(535, 99)
        Me.txtUSEBALL.MaxLength = 5
        Me.txtUSEBALL.Name = "txtUSEBALL"
        Me.txtUSEBALL.Size = New System.Drawing.Size(112, 37)
        Me.txtUSEBALL.TabIndex = 246
        Me.txtUSEBALL.Text = "99,999"
        Me.txtUSEBALL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdoBALL2F
        '
        Me.rdoBALL2F.AutoSize = True
        Me.rdoBALL2F.Location = New System.Drawing.Point(330, 113)
        Me.rdoBALL2F.Name = "rdoBALL2F"
        Me.rdoBALL2F.Size = New System.Drawing.Size(14, 13)
        Me.rdoBALL2F.TabIndex = 245
        Me.rdoBALL2F.UseVisualStyleBackColor = True
        '
        'rdoBALL1F
        '
        Me.rdoBALL1F.AutoSize = True
        Me.rdoBALL1F.Checked = True
        Me.rdoBALL1F.Location = New System.Drawing.Point(330, 78)
        Me.rdoBALL1F.Name = "rdoBALL1F"
        Me.rdoBALL1F.Size = New System.Drawing.Size(14, 13)
        Me.rdoBALL1F.TabIndex = 244
        Me.rdoBALL1F.TabStop = True
        Me.rdoBALL1F.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.DarkGreen
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(403, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 37)
        Me.Label6.TabIndex = 243
        Me.Label6.Text = "使用球数"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBALL2F
        '
        Me.lblBALL2F.BackColor = System.Drawing.Color.White
        Me.lblBALL2F.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBALL2F.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBALL2F.ForeColor = System.Drawing.Color.Black
        Me.lblBALL2F.Location = New System.Drawing.Point(197, 101)
        Me.lblBALL2F.Name = "lblBALL2F"
        Me.lblBALL2F.Size = New System.Drawing.Size(91, 35)
        Me.lblBALL2F.TabIndex = 242
        Me.lblBALL2F.Text = "99.00"
        Me.lblBALL2F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBALL1F
        '
        Me.lblBALL1F.BackColor = System.Drawing.Color.White
        Me.lblBALL1F.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBALL1F.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBALL1F.ForeColor = System.Drawing.Color.Black
        Me.lblBALL1F.Location = New System.Drawing.Point(197, 65)
        Me.lblBALL1F.Name = "lblBALL1F"
        Me.lblBALL1F.Size = New System.Drawing.Size(91, 35)
        Me.lblBALL1F.TabIndex = 241
        Me.lblBALL1F.Text = "99.00"
        Me.lblBALL1F.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(140, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 35)
        Me.Label5.TabIndex = 240
        Me.Label5.Text = "2Ｆ"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkGreen
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(140, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 35)
        Me.Label4.TabIndex = 239
        Me.Label4.Text = "1Ｆ"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.DarkGreen
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Yellow
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1035, 49)
        Me.Label1.TabIndex = 238
        Me.Label1.Text = "店番復帰を行います。障害発生時までの打席での使用球数を入力して下さい。"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label246
        '
        Me.Label246.BackColor = System.Drawing.Color.DarkGreen
        Me.Label246.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label246.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label246.ForeColor = System.Drawing.Color.White
        Me.Label246.Location = New System.Drawing.Point(8, 65)
        Me.Label246.Name = "Label246"
        Me.Label246.Size = New System.Drawing.Size(131, 71)
        Me.Label246.TabIndex = 237
        Me.Label246.Text = "ボール単価"
        Me.Label246.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Salmon
        Me.btnOK.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(819, 178)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(143, 83)
        Me.btnOK.TabIndex = 239
        Me.btnOK.Tag = "0"
        Me.btnOK.Text = "店番復帰"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'pnlTime
        '
        Me.pnlTime.BackColor = System.Drawing.Color.FloralWhite
        Me.pnlTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTime.Controls.Add(Me.Label12)
        Me.pnlTime.Controls.Add(Me.lblZANTIME)
        Me.pnlTime.Controls.Add(Me.lblSelectTime)
        Me.pnlTime.Controls.Add(Me.Label10)
        Me.pnlTime.Controls.Add(Me.Label3)
        Me.pnlTime.Controls.Add(Me.txtPlayTime)
        Me.pnlTime.Controls.Add(Me.Label2)
        Me.pnlTime.Location = New System.Drawing.Point(12, 9)
        Me.pnlTime.Name = "pnlTime"
        Me.pnlTime.Size = New System.Drawing.Size(1058, 163)
        Me.pnlTime.TabIndex = 240
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(787, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(131, 37)
        Me.Label12.TabIndex = 253
        Me.Label12.Text = "残時間"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZANTIME
        '
        Me.lblZANTIME.BackColor = System.Drawing.Color.White
        Me.lblZANTIME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZANTIME.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZANTIME.ForeColor = System.Drawing.Color.Black
        Me.lblZANTIME.Location = New System.Drawing.Point(919, 82)
        Me.lblZANTIME.Name = "lblZANTIME"
        Me.lblZANTIME.Size = New System.Drawing.Size(111, 37)
        Me.lblZANTIME.TabIndex = 252
        Me.lblZANTIME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSelectTime
        '
        Me.lblSelectTime.BackColor = System.Drawing.Color.White
        Me.lblSelectTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSelectTime.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSelectTime.ForeColor = System.Drawing.Color.Black
        Me.lblSelectTime.Location = New System.Drawing.Point(213, 82)
        Me.lblSelectTime.Name = "lblSelectTime"
        Me.lblSelectTime.Size = New System.Drawing.Size(111, 37)
        Me.lblSelectTime.TabIndex = 251
        Me.lblSelectTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkGreen
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(81, 82)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(131, 37)
        Me.Label10.TabIndex = 250
        Me.Label10.Text = "打ち放題"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkGreen
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(350, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 37)
        Me.Label3.TabIndex = 248
        Me.Label3.Text = "利用時間"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPlayTime
        '
        Me.txtPlayTime.BackColor = System.Drawing.Color.White
        Me.txtPlayTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlayTime.Font = New System.Drawing.Font("MS UI Gothic", 22.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPlayTime.Format = "#,##0"
        Me.txtPlayTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtPlayTime.Location = New System.Drawing.Point(482, 82)
        Me.txtPlayTime.MaxLength = 5
        Me.txtPlayTime.Name = "txtPlayTime"
        Me.txtPlayTime.Size = New System.Drawing.Size(112, 37)
        Me.txtPlayTime.TabIndex = 247
        Me.txtPlayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.DarkGreen
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Yellow
        Me.Label2.Location = New System.Drawing.Point(8, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1035, 49)
        Me.Label2.TabIndex = 238
        Me.Label2.Text = "店番復帰を行います。障害発生時までの打席での利用時間を入力して下さい。"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmRETURNSHOPNO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.ClientSize = New System.Drawing.Size(1082, 277)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.pnlTama)
        Me.Controls.Add(Me.pnlTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmRETURNSHOPNO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRETURNSHOPNO"
        Me.TopMost = True
        Me.pnlTama.ResumeLayout(False)
        Me.pnlTama.PerformLayout()
        Me.pnlTime.ResumeLayout(False)
        Me.pnlTime.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTama As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label246 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlTime As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdoBALL2F As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBALL1F As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblBALL2F As System.Windows.Forms.Label
    Friend WithEvents lblBALL1F As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUSEKINGAKU As BaseControl.CustomTextBoxNum
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUSEBALL As BaseControl.CustomTextBoxNum
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPlayTime As BaseControl.CustomTextBoxNum
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblZANTIME As System.Windows.Forms.Label
    Friend WithEvents lblSelectTime As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
