<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCARDREPAIR01
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRW1ZANKN = New BaseControl.CustomTextBoxNum()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRW2ZANKN = New BaseControl.CustomTextBoxNum()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRW2PREMKN = New BaseControl.CustomTextBoxNum()
        Me.CustomTextBoxNum3 = New BaseControl.CustomTextBoxNum()
        Me.CustomTextBoxNum4 = New BaseControl.CustomTextBoxNum()
        Me.txtRW2SRTPO = New BaseControl.CustomTextBoxNum()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnCard = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblRW1ZANKN = New System.Windows.Forms.Label()
        Me.lblRW2ZANKN = New System.Windows.Forms.Label()
        Me.lblRW2PREMKN = New System.Windows.Forms.Label()
        Me.lblRW2SRTPO = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblErrMsg1 = New System.Windows.Forms.Label()
        Me.lblErrMsg2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.LightCoral
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(58, 283)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(209, 54)
        Me.Label3.TabIndex = 172
        Me.Label3.Text = "磁気情報①"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightCoral
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(267, 229)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(209, 54)
        Me.Label1.TabIndex = 173
        Me.Label1.Text = "残金額"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRW1ZANKN
        '
        Me.txtRW1ZANKN.BackColor = System.Drawing.Color.White
        Me.txtRW1ZANKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRW1ZANKN.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRW1ZANKN.Format = "#,##0"
        Me.txtRW1ZANKN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtRW1ZANKN.Location = New System.Drawing.Point(267, 283)
        Me.txtRW1ZANKN.MaxLength = 5
        Me.txtRW1ZANKN.Name = "txtRW1ZANKN"
        Me.txtRW1ZANKN.ReadOnly = True
        Me.txtRW1ZANKN.Size = New System.Drawing.Size(209, 54)
        Me.txtRW1ZANKN.TabIndex = 174
        Me.txtRW1ZANKN.Tag = ""
        Me.txtRW1ZANKN.Text = "99,999"
        Me.txtRW1ZANKN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.LightCoral
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(477, 229)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(209, 54)
        Me.Label2.TabIndex = 175
        Me.Label2.Text = "P)残金額"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRW2ZANKN
        '
        Me.txtRW2ZANKN.BackColor = System.Drawing.Color.White
        Me.txtRW2ZANKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRW2ZANKN.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRW2ZANKN.Format = "#,##0"
        Me.txtRW2ZANKN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtRW2ZANKN.Location = New System.Drawing.Point(267, 338)
        Me.txtRW2ZANKN.MaxLength = 5
        Me.txtRW2ZANKN.Name = "txtRW2ZANKN"
        Me.txtRW2ZANKN.ReadOnly = True
        Me.txtRW2ZANKN.Size = New System.Drawing.Size(209, 54)
        Me.txtRW2ZANKN.TabIndex = 176
        Me.txtRW2ZANKN.Tag = ""
        Me.txtRW2ZANKN.Text = "99,999"
        Me.txtRW2ZANKN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.LightCoral
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(58, 338)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(209, 54)
        Me.Label4.TabIndex = 177
        Me.Label4.Text = "磁気情報②"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRW2PREMKN
        '
        Me.txtRW2PREMKN.BackColor = System.Drawing.Color.White
        Me.txtRW2PREMKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRW2PREMKN.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRW2PREMKN.Format = "#,##0"
        Me.txtRW2PREMKN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtRW2PREMKN.Location = New System.Drawing.Point(477, 338)
        Me.txtRW2PREMKN.MaxLength = 5
        Me.txtRW2PREMKN.Name = "txtRW2PREMKN"
        Me.txtRW2PREMKN.ReadOnly = True
        Me.txtRW2PREMKN.Size = New System.Drawing.Size(209, 54)
        Me.txtRW2PREMKN.TabIndex = 178
        Me.txtRW2PREMKN.Tag = ""
        Me.txtRW2PREMKN.Text = "99,999"
        Me.txtRW2PREMKN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CustomTextBoxNum3
        '
        Me.CustomTextBoxNum3.BackColor = System.Drawing.Color.DarkGray
        Me.CustomTextBoxNum3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CustomTextBoxNum3.Enabled = False
        Me.CustomTextBoxNum3.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CustomTextBoxNum3.Format = "#,##0"
        Me.CustomTextBoxNum3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CustomTextBoxNum3.Location = New System.Drawing.Point(477, 283)
        Me.CustomTextBoxNum3.MaxLength = 5
        Me.CustomTextBoxNum3.Name = "CustomTextBoxNum3"
        Me.CustomTextBoxNum3.Size = New System.Drawing.Size(209, 54)
        Me.CustomTextBoxNum3.TabIndex = 179
        Me.CustomTextBoxNum3.Tag = ""
        Me.CustomTextBoxNum3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CustomTextBoxNum4
        '
        Me.CustomTextBoxNum4.BackColor = System.Drawing.Color.DarkGray
        Me.CustomTextBoxNum4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CustomTextBoxNum4.Enabled = False
        Me.CustomTextBoxNum4.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CustomTextBoxNum4.Format = "#,##0"
        Me.CustomTextBoxNum4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CustomTextBoxNum4.Location = New System.Drawing.Point(687, 283)
        Me.CustomTextBoxNum4.MaxLength = 5
        Me.CustomTextBoxNum4.Name = "CustomTextBoxNum4"
        Me.CustomTextBoxNum4.Size = New System.Drawing.Size(209, 54)
        Me.CustomTextBoxNum4.TabIndex = 182
        Me.CustomTextBoxNum4.Tag = ""
        Me.CustomTextBoxNum4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRW2SRTPO
        '
        Me.txtRW2SRTPO.BackColor = System.Drawing.Color.White
        Me.txtRW2SRTPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRW2SRTPO.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRW2SRTPO.Format = "#,##0"
        Me.txtRW2SRTPO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtRW2SRTPO.Location = New System.Drawing.Point(687, 338)
        Me.txtRW2SRTPO.MaxLength = 5
        Me.txtRW2SRTPO.Name = "txtRW2SRTPO"
        Me.txtRW2SRTPO.ReadOnly = True
        Me.txtRW2SRTPO.Size = New System.Drawing.Size(209, 54)
        Me.txtRW2SRTPO.TabIndex = 181
        Me.txtRW2SRTPO.Tag = ""
        Me.txtRW2SRTPO.Text = "99,999"
        Me.txtRW2SRTPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.LightCoral
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(687, 229)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(209, 54)
        Me.Label5.TabIndex = 180
        Me.Label5.Text = "ポイント"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCard
        '
        Me.btnCard.BackColor = System.Drawing.Color.Salmon
        Me.btnCard.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCard.ForeColor = System.Drawing.Color.White
        Me.btnCard.Location = New System.Drawing.Point(731, 413)
        Me.btnCard.Name = "btnCard"
        Me.btnCard.Size = New System.Drawing.Size(165, 74)
        Me.btnCard.TabIndex = 183
        Me.btnCard.Text = "カード挿入"
        Me.btnCard.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Red
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(58, 229)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(209, 54)
        Me.Label6.TabIndex = 184
        Me.Label6.Text = "修復内容"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(58, 513)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(209, 54)
        Me.Label7.TabIndex = 196
        Me.Label7.Text = "修復前"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.DarkGreen
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(687, 513)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(209, 54)
        Me.Label8.TabIndex = 193
        Me.Label8.Text = "ポイント"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.DarkGreen
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(58, 624)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(209, 55)
        Me.Label9.TabIndex = 190
        Me.Label9.Text = "磁気情報②"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DarkGreen
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(477, 513)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(209, 54)
        Me.Label10.TabIndex = 188
        Me.Label10.Text = "P)残金額"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.DarkGreen
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(267, 513)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(209, 54)
        Me.Label11.TabIndex = 186
        Me.Label11.Text = "残金額"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkGreen
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(58, 568)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(209, 55)
        Me.Label12.TabIndex = 185
        Me.Label12.Text = "磁気情報①"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRW1ZANKN
        '
        Me.lblRW1ZANKN.BackColor = System.Drawing.Color.Gainsboro
        Me.lblRW1ZANKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRW1ZANKN.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRW1ZANKN.ForeColor = System.Drawing.Color.Black
        Me.lblRW1ZANKN.Location = New System.Drawing.Point(267, 568)
        Me.lblRW1ZANKN.Name = "lblRW1ZANKN"
        Me.lblRW1ZANKN.Size = New System.Drawing.Size(209, 55)
        Me.lblRW1ZANKN.TabIndex = 197
        Me.lblRW1ZANKN.Text = "99,999"
        Me.lblRW1ZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRW2ZANKN
        '
        Me.lblRW2ZANKN.BackColor = System.Drawing.Color.Gainsboro
        Me.lblRW2ZANKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRW2ZANKN.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRW2ZANKN.ForeColor = System.Drawing.Color.Black
        Me.lblRW2ZANKN.Location = New System.Drawing.Point(267, 624)
        Me.lblRW2ZANKN.Name = "lblRW2ZANKN"
        Me.lblRW2ZANKN.Size = New System.Drawing.Size(209, 55)
        Me.lblRW2ZANKN.TabIndex = 198
        Me.lblRW2ZANKN.Text = "99,999"
        Me.lblRW2ZANKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRW2PREMKN
        '
        Me.lblRW2PREMKN.BackColor = System.Drawing.Color.Gainsboro
        Me.lblRW2PREMKN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRW2PREMKN.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRW2PREMKN.ForeColor = System.Drawing.Color.Black
        Me.lblRW2PREMKN.Location = New System.Drawing.Point(477, 624)
        Me.lblRW2PREMKN.Name = "lblRW2PREMKN"
        Me.lblRW2PREMKN.Size = New System.Drawing.Size(209, 55)
        Me.lblRW2PREMKN.TabIndex = 199
        Me.lblRW2PREMKN.Text = "99,999"
        Me.lblRW2PREMKN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRW2SRTPO
        '
        Me.lblRW2SRTPO.BackColor = System.Drawing.Color.Gainsboro
        Me.lblRW2SRTPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRW2SRTPO.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRW2SRTPO.ForeColor = System.Drawing.Color.Black
        Me.lblRW2SRTPO.Location = New System.Drawing.Point(687, 624)
        Me.lblRW2SRTPO.Name = "lblRW2SRTPO"
        Me.lblRW2SRTPO.Size = New System.Drawing.Size(209, 55)
        Me.lblRW2SRTPO.TabIndex = 200
        Me.lblRW2SRTPO.Text = "99,999"
        Me.lblRW2SRTPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.DarkGray
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(687, 568)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(209, 55)
        Me.Label17.TabIndex = 202
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.DarkGray
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(477, 568)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(209, 55)
        Me.Label18.TabIndex = 201
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.DarkGreen
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Yellow
        Me.Label13.Location = New System.Drawing.Point(21, 116)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(940, 54)
        Me.Label13.TabIndex = 203
        Me.Label13.Text = "サーバーの情報でカードを修復します。"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblErrMsg1
        '
        Me.lblErrMsg1.BackColor = System.Drawing.Color.Transparent
        Me.lblErrMsg1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblErrMsg1.ForeColor = System.Drawing.Color.Red
        Me.lblErrMsg1.Location = New System.Drawing.Point(54, 694)
        Me.lblErrMsg1.Name = "lblErrMsg1"
        Me.lblErrMsg1.Size = New System.Drawing.Size(592, 33)
        Me.lblErrMsg1.TabIndex = 204
        Me.lblErrMsg1.Text = "※磁気情報①のデータが破損しています。"
        Me.lblErrMsg1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblErrMsg1.Visible = False
        '
        'lblErrMsg2
        '
        Me.lblErrMsg2.BackColor = System.Drawing.Color.Transparent
        Me.lblErrMsg2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblErrMsg2.ForeColor = System.Drawing.Color.Red
        Me.lblErrMsg2.Location = New System.Drawing.Point(54, 727)
        Me.lblErrMsg2.Name = "lblErrMsg2"
        Me.lblErrMsg2.Size = New System.Drawing.Size(592, 28)
        Me.lblErrMsg2.TabIndex = 205
        Me.lblErrMsg2.Text = "※磁気情報②のデータが破損しています。"
        Me.lblErrMsg2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblErrMsg2.Visible = False
        '
        'frmCARDREPAIR01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 962)
        Me.Controls.Add(Me.lblErrMsg1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.lblRW2SRTPO)
        Me.Controls.Add(Me.lblRW2PREMKN)
        Me.Controls.Add(Me.lblRW2ZANKN)
        Me.Controls.Add(Me.lblRW1ZANKN)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnCard)
        Me.Controls.Add(Me.CustomTextBoxNum4)
        Me.Controls.Add(Me.txtRW2SRTPO)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CustomTextBoxNum3)
        Me.Controls.Add(Me.txtRW2PREMKN)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRW2ZANKN)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRW1ZANKN)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblErrMsg2)
        Me.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.MinimumSize = New System.Drawing.Size(800, 500)
        Me.Name = "frmCARDREPAIR01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.lblErrMsg2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtRW1ZANKN, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtRW2ZANKN, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtRW2PREMKN, 0)
        Me.Controls.SetChildIndex(Me.CustomTextBoxNum3, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.txtRW2SRTPO, 0)
        Me.Controls.SetChildIndex(Me.CustomTextBoxNum4, 0)
        Me.Controls.SetChildIndex(Me.btnCard, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.lblRW1ZANKN, 0)
        Me.Controls.SetChildIndex(Me.lblRW2ZANKN, 0)
        Me.Controls.SetChildIndex(Me.lblRW2PREMKN, 0)
        Me.Controls.SetChildIndex(Me.lblRW2SRTPO, 0)
        Me.Controls.SetChildIndex(Me.Label18, 0)
        Me.Controls.SetChildIndex(Me.Label17, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.lblErrMsg1, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRW1ZANKN As BaseControl.CustomTextBoxNum
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRW2ZANKN As BaseControl.CustomTextBoxNum
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRW2PREMKN As BaseControl.CustomTextBoxNum
    Friend WithEvents CustomTextBoxNum3 As BaseControl.CustomTextBoxNum
    Friend WithEvents CustomTextBoxNum4 As BaseControl.CustomTextBoxNum
    Friend WithEvents txtRW2SRTPO As BaseControl.CustomTextBoxNum
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnCard As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblRW1ZANKN As System.Windows.Forms.Label
    Friend WithEvents lblRW2ZANKN As System.Windows.Forms.Label
    Friend WithEvents lblRW2PREMKN As System.Windows.Forms.Label
    Friend WithEvents lblRW2SRTPO As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblErrMsg1 As System.Windows.Forms.Label
    Friend WithEvents lblErrMsg2 As System.Windows.Forms.Label

End Class
