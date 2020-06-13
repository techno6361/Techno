<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPRINT07
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoRepo07 = New System.Windows.Forms.RadioButton()
        Me.rdoRepo06 = New System.Windows.Forms.RadioButton()
        Me.rdoRepo05 = New System.Windows.Forms.RadioButton()
        Me.rdoRepo03 = New System.Windows.Forms.RadioButton()
        Me.rdoRepo02 = New System.Windows.Forms.RadioButton()
        Me.rdoRepo01 = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtYear4 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtYear3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtYear2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtYear1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtYear5 = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Wheat
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.rdoRepo07)
        Me.Panel1.Controls.Add(Me.rdoRepo06)
        Me.Panel1.Controls.Add(Me.rdoRepo05)
        Me.Panel1.Controls.Add(Me.rdoRepo03)
        Me.Panel1.Controls.Add(Me.rdoRepo02)
        Me.Panel1.Controls.Add(Me.rdoRepo01)
        Me.Panel1.Location = New System.Drawing.Point(203, 515)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1562, 236)
        Me.Panel1.TabIndex = 6
        '
        'rdoRepo07
        '
        Me.rdoRepo07.AutoSize = True
        Me.rdoRepo07.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoRepo07.Location = New System.Drawing.Point(654, 81)
        Me.rdoRepo07.Name = "rdoRepo07"
        Me.rdoRepo07.Size = New System.Drawing.Size(345, 44)
        Me.rdoRepo07.TabIndex = 13
        Me.rdoRepo07.Text = "打ち放題利用者数"
        Me.rdoRepo07.UseVisualStyleBackColor = True
        '
        'rdoRepo06
        '
        Me.rdoRepo06.AutoSize = True
        Me.rdoRepo06.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoRepo06.Location = New System.Drawing.Point(314, 81)
        Me.rdoRepo06.Name = "rdoRepo06"
        Me.rdoRepo06.Size = New System.Drawing.Size(324, 44)
        Me.rdoRepo06.TabIndex = 12
        Me.rdoRepo06.Text = "1球貸し利用者数"
        Me.rdoRepo06.UseVisualStyleBackColor = True
        '
        'rdoRepo05
        '
        Me.rdoRepo05.AutoSize = True
        Me.rdoRepo05.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoRepo05.Location = New System.Drawing.Point(20, 81)
        Me.rdoRepo05.Name = "rdoRepo05"
        Me.rdoRepo05.Size = New System.Drawing.Size(195, 44)
        Me.rdoRepo05.TabIndex = 11
        Me.rdoRepo05.Text = "入場者数"
        Me.rdoRepo05.UseVisualStyleBackColor = True
        '
        'rdoRepo03
        '
        Me.rdoRepo03.AutoSize = True
        Me.rdoRepo03.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoRepo03.Location = New System.Drawing.Point(654, 19)
        Me.rdoRepo03.Name = "rdoRepo03"
        Me.rdoRepo03.Size = New System.Drawing.Size(265, 44)
        Me.rdoRepo03.TabIndex = 9
        Me.rdoRepo03.Text = "打ち放題売上"
        Me.rdoRepo03.UseVisualStyleBackColor = True
        '
        'rdoRepo02
        '
        Me.rdoRepo02.AutoSize = True
        Me.rdoRepo02.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoRepo02.Location = New System.Drawing.Point(314, 19)
        Me.rdoRepo02.Name = "rdoRepo02"
        Me.rdoRepo02.Size = New System.Drawing.Size(324, 44)
        Me.rdoRepo02.TabIndex = 8
        Me.rdoRepo02.Text = "1球貸し球数売上"
        Me.rdoRepo02.UseVisualStyleBackColor = True
        '
        'rdoRepo01
        '
        Me.rdoRepo01.AutoSize = True
        Me.rdoRepo01.Checked = True
        Me.rdoRepo01.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoRepo01.Location = New System.Drawing.Point(20, 19)
        Me.rdoRepo01.Name = "rdoRepo01"
        Me.rdoRepo01.Size = New System.Drawing.Size(284, 44)
        Me.rdoRepo01.TabIndex = 7
        Me.rdoRepo01.TabStop = True
        Me.rdoRepo01.Text = "1球貸し打球数"
        Me.rdoRepo01.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Purple
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(874, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 61)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "4"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(1078, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 54)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "年"
        '
        'txtYear4
        '
        Me.txtYear4.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear4.Location = New System.Drawing.Point(952, 43)
        Me.txtYear4.MaxLength = 4
        Me.txtYear4.Name = "txtYear4"
        Me.txtYear4.Size = New System.Drawing.Size(121, 61)
        Me.txtYear4.TabIndex = 4
        Me.txtYear4.Text = "2000"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(597, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 61)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "3"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(801, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 54)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "年"
        '
        'txtYear3
        '
        Me.txtYear3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear3.Location = New System.Drawing.Point(675, 43)
        Me.txtYear3.MaxLength = 4
        Me.txtYear3.Name = "txtYear3"
        Me.txtYear3.Size = New System.Drawing.Size(121, 61)
        Me.txtYear3.TabIndex = 3
        Me.txtYear3.Text = "2000"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Brown
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(310, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 61)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "2"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(514, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 54)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "年"
        '
        'txtYear2
        '
        Me.txtYear2.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear2.Location = New System.Drawing.Point(388, 43)
        Me.txtYear2.MaxLength = 4
        Me.txtYear2.Name = "txtYear2"
        Me.txtYear2.Size = New System.Drawing.Size(121, 61)
        Me.txtYear2.TabIndex = 2
        Me.txtYear2.Text = "2000"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(23, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 61)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "1"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(227, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 54)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "年"
        '
        'txtYear1
        '
        Me.txtYear1.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear1.Location = New System.Drawing.Point(101, 43)
        Me.txtYear1.MaxLength = 4
        Me.txtYear1.Name = "txtYear1"
        Me.txtYear1.Size = New System.Drawing.Size(121, 61)
        Me.txtYear1.TabIndex = 1
        Me.txtYear1.Text = "2000"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.SkyBlue
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(1161, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 61)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "5"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(1365, 46)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 54)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "年"
        '
        'txtYear5
        '
        Me.txtYear5.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear5.Location = New System.Drawing.Point(1239, 42)
        Me.txtYear5.MaxLength = 4
        Me.txtYear5.Name = "txtYear5"
        Me.txtYear5.Size = New System.Drawing.Size(121, 61)
        Me.txtYear5.TabIndex = 5
        Me.txtYear5.Text = "2000"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Wheat
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txtYear1)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtYear5)
        Me.Panel2.Controls.Add(Me.txtYear2)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtYear4)
        Me.Panel2.Controls.Add(Me.txtYear3)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Location = New System.Drawing.Point(203, 257)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1562, 134)
        Me.Panel2.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.ForestGreen
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(203, 197)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1562, 61)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "対象年"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.ForestGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(203, 455)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1562, 61)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "出力区分"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmPRINT07
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmPRINT07"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoRepo01 As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtYear4 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtYear3 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtYear2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtYear1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtYear5 As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rdoRepo02 As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rdoRepo03 As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents rdoRepo05 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRepo06 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRepo07 As System.Windows.Forms.RadioButton

End Class
