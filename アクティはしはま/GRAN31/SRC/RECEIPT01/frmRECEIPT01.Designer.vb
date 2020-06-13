<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRECEIPT01
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtNAME = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDay = New BaseControl.CustomTextBoxNum()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMonth = New BaseControl.CustomTextBoxNum()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtYear = New BaseControl.CustomTextBoxNum()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblDENNO = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTADASHI = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtKINGAKU = New BaseControl.CustomTextBoxNum()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.dgvRCTTRN = New BaseControl.CustomGridView()
        Me.RCTDT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RCTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ATENA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KINGAKU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TADASHI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STFNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvRCTTRN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtNAME
        '
        Me.txtNAME.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtNAME.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNAME.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtNAME.Location = New System.Drawing.Point(33, 51)
        Me.txtNAME.MaxLength = 17
        Me.txtNAME.Name = "txtNAME"
        Me.txtNAME.Size = New System.Drawing.Size(486, 34)
        Me.txtNAME.TabIndex = 8
        Me.txtNAME.Text = "株式会社テクノ・アシスト"
        Me.txtNAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txtDay)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtMonth)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtYear)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.lblDENNO)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtTADASHI)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtKINGAKU)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtNAME)
        Me.Panel1.Location = New System.Drawing.Point(35, 156)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(912, 240)
        Me.Panel1.TabIndex = 9
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Location = New System.Drawing.Point(774, 103)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(111, 103)
        Me.Panel5.TabIndex = 29
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(24, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 57)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "収　入" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "印　紙"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(863, 47)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(30, 20)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "日"
        '
        'txtDay
        '
        Me.txtDay.BackColor = System.Drawing.Color.LightGray
        Me.txtDay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDay.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDay.Format = "#,##0"
        Me.txtDay.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtDay.Location = New System.Drawing.Point(824, 47)
        Me.txtDay.MaxLength = 2
        Me.txtDay.Name = "txtDay"
        Me.txtDay.Size = New System.Drawing.Size(34, 20)
        Me.txtDay.TabIndex = 27
        Me.txtDay.Tag = ""
        Me.txtDay.Text = "99"
        Me.txtDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(792, 47)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(30, 20)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "月"
        '
        'txtMonth
        '
        Me.txtMonth.BackColor = System.Drawing.Color.LightGray
        Me.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMonth.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMonth.Format = "#,##0"
        Me.txtMonth.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtMonth.Location = New System.Drawing.Point(753, 47)
        Me.txtMonth.MaxLength = 2
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.Size = New System.Drawing.Size(34, 20)
        Me.txtMonth.TabIndex = 25
        Me.txtMonth.Tag = ""
        Me.txtMonth.Text = "99"
        Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(722, 47)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(30, 20)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "年"
        '
        'txtYear
        '
        Me.txtYear.BackColor = System.Drawing.Color.LightGray
        Me.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtYear.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear.Format = ""
        Me.txtYear.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtYear.Location = New System.Drawing.Point(673, 47)
        Me.txtYear.MaxLength = 4
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(45, 20)
        Me.txtYear.TabIndex = 23
        Me.txtYear.Tag = ""
        Me.txtYear.Text = "9999"
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(602, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 20)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "発行日"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Black
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Location = New System.Drawing.Point(759, 34)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(144, 1)
        Me.Panel4.TabIndex = 11
        '
        'lblDENNO
        '
        Me.lblDENNO.AutoSize = True
        Me.lblDENNO.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDENNO.Location = New System.Drawing.Point(820, 9)
        Me.lblDENNO.Name = "lblDENNO"
        Me.lblDENNO.Size = New System.Drawing.Size(58, 24)
        Me.lblDENNO.TabIndex = 21
        Me.lblDENNO.Text = "0000"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(763, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(34, 24)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "№"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(527, 209)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(141, 19)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "0898-41-9006"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(450, 188)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(293, 19)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "愛媛県今治市内堀2丁目1番15号"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(534, 165)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(130, 19)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "アクティはしはま"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(46, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(220, 19)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "上記正に領収致しました。"
        '
        'txtTADASHI
        '
        Me.txtTADASHI.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTADASHI.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTADASHI.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTADASHI.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtTADASHI.Location = New System.Drawing.Point(83, 165)
        Me.txtTADASHI.MaxLength = 20
        Me.txtTADASHI.Name = "txtTADASHI"
        Me.txtTADASHI.Size = New System.Drawing.Size(347, 19)
        Me.txtTADASHI.TabIndex = 16
        Me.txtTADASHI.Text = "ボール代金として"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(46, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 19)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "但"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(87, 135)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(395, 4)
        Me.Panel3.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(389, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 24)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "-"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(257, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 24)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "\"
        '
        'txtKINGAKU
        '
        Me.txtKINGAKU.BackColor = System.Drawing.Color.LightGray
        Me.txtKINGAKU.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtKINGAKU.Font = New System.Drawing.Font("MS UI Gothic", 22.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKINGAKU.Format = "#,##0"
        Me.txtKINGAKU.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtKINGAKU.Location = New System.Drawing.Point(283, 105)
        Me.txtKINGAKU.MaxLength = 6
        Me.txtKINGAKU.Name = "txtKINGAKU"
        Me.txtKINGAKU.Size = New System.Drawing.Size(102, 30)
        Me.txtKINGAKU.TabIndex = 12
        Me.txtKINGAKU.Tag = ""
        Me.txtKINGAKU.Text = "999,999"
        Me.txtKINGAKU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 23.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(108, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 31)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "金額"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Black
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(33, 86)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(527, 2)
        Me.Panel2.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(522, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 24)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "様"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(307, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 24)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "領収書"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.SeaGreen
        Me.btnPrint.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(789, 412)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(158, 76)
        Me.btnPrint.TabIndex = 609
        Me.btnPrint.TabStop = False
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'dgvRCTTRN
        '
        Me.dgvRCTTRN.AllowUserToAddRows = False
        Me.dgvRCTTRN.AllowUserToDeleteRows = False
        Me.dgvRCTTRN.AllowUserToResizeColumns = False
        Me.dgvRCTTRN.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvRCTTRN.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRCTTRN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvRCTTRN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRCTTRN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvRCTTRN.ColumnHeadersHeight = 30
        Me.dgvRCTTRN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvRCTTRN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RCTDT, Me.RCTNO, Me.ATENA, Me.KINGAKU, Me.TADASHI, Me.STFNAME})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRCTTRN.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvRCTTRN.EnableHeadersVisualStyles = False
        Me.dgvRCTTRN.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvRCTTRN.Location = New System.Drawing.Point(12, 558)
        Me.dgvRCTTRN.MultiSelect = False
        Me.dgvRCTTRN.Name = "dgvRCTTRN"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRCTTRN.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvRCTTRN.RowHeadersVisible = False
        Me.dgvRCTTRN.RowHeadersWidth = 30
        Me.dgvRCTTRN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvRCTTRN.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvRCTTRN.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvRCTTRN.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvRCTTRN.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvRCTTRN.RowTemplate.Height = 40
        Me.dgvRCTTRN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRCTTRN.Size = New System.Drawing.Size(960, 352)
        Me.dgvRCTTRN.TabIndex = 1026
        '
        'RCTDT
        '
        Me.RCTDT.DataPropertyName = "RCTDT"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.RCTDT.DefaultCellStyle = DataGridViewCellStyle3
        Me.RCTDT.HeaderText = "日付"
        Me.RCTDT.MaxInputLength = 10
        Me.RCTDT.Name = "RCTDT"
        Me.RCTDT.ReadOnly = True
        Me.RCTDT.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RCTDT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RCTDT.Visible = False
        Me.RCTDT.Width = 130
        '
        'RCTNO
        '
        Me.RCTNO.DataPropertyName = "RCTNO"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.RCTNO.DefaultCellStyle = DataGridViewCellStyle4
        Me.RCTNO.HeaderText = "伝票番号"
        Me.RCTNO.MaxInputLength = 5
        Me.RCTNO.Name = "RCTNO"
        Me.RCTNO.ReadOnly = True
        Me.RCTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ATENA
        '
        Me.ATENA.DataPropertyName = "ATENA"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.ATENA.DefaultCellStyle = DataGridViewCellStyle5
        Me.ATENA.HeaderText = "宛名"
        Me.ATENA.Name = "ATENA"
        Me.ATENA.ReadOnly = True
        Me.ATENA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ATENA.Width = 240
        '
        'KINGAKU
        '
        Me.KINGAKU.DataPropertyName = "KINGAKU"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.KINGAKU.DefaultCellStyle = DataGridViewCellStyle6
        Me.KINGAKU.HeaderText = "金額"
        Me.KINGAKU.MaxInputLength = 2
        Me.KINGAKU.Name = "KINGAKU"
        Me.KINGAKU.ReadOnly = True
        Me.KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.KINGAKU.Width = 110
        '
        'TADASHI
        '
        Me.TADASHI.DataPropertyName = "TADASHI"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.TADASHI.DefaultCellStyle = DataGridViewCellStyle7
        Me.TADASHI.HeaderText = "但"
        Me.TADASHI.MaxInputLength = 2
        Me.TADASHI.Name = "TADASHI"
        Me.TADASHI.ReadOnly = True
        Me.TADASHI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TADASHI.Width = 300
        '
        'STFNAME
        '
        Me.STFNAME.DataPropertyName = "STFNAME"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.STFNAME.DefaultCellStyle = DataGridViewCellStyle8
        Me.STFNAME.HeaderText = "スタッフ名"
        Me.STFNAME.Name = "STFNAME"
        Me.STFNAME.ReadOnly = True
        Me.STFNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.STFNAME.Width = 190
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.Gray
        Me.btnClear.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(592, 412)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(158, 76)
        Me.btnClear.TabIndex = 1027
        Me.btnClear.TabStop = False
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Red
        Me.Label17.Location = New System.Drawing.Point(554, 535)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(418, 19)
        Me.Label17.TabIndex = 1028
        Me.Label17.Text = "※再出力の場合ダブルクリックで選択して下さい。※"
        '
        'frmRECEIPT01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 962)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.dgvRCTTRN)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Panel1)
        Me.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.MinimumSize = New System.Drawing.Size(1000, 1000)
        Me.Name = "frmRECEIPT01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.btnPrint, 0)
        Me.Controls.SetChildIndex(Me.dgvRCTTRN, 0)
        Me.Controls.SetChildIndex(Me.btnClear, 0)
        Me.Controls.SetChildIndex(Me.Label17, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgvRCTTRN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNAME As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblDENNO As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTADASHI As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtKINGAKU As BaseControl.CustomTextBoxNum
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtDay As BaseControl.CustomTextBoxNum
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMonth As BaseControl.CustomTextBoxNum
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtYear As BaseControl.CustomTextBoxNum
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents dgvRCTTRN As BaseControl.CustomGridView
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents RCTDT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RCTNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ATENA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KINGAKU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TADASHI As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STFNAME As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
