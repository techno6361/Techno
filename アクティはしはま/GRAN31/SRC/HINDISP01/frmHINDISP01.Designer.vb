<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHINDISP01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHINDISP01))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnReceipt = New System.Windows.Forms.Button()
        Me.btnSlipCancel = New System.Windows.Forms.Button()
        Me.tabHINDISP = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.rdoHINKBN2 = New System.Windows.Forms.RadioButton()
        Me.rdoHINKBN1 = New System.Windows.Forms.RadioButton()
        Me.txtHINNMA = New System.Windows.Forms.TextBox()
        Me.lblPOINT = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPrice = New BaseControl.CustomTextBoxNum()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnBACK = New System.Windows.Forms.Button()
        Me.btnCLEAR = New System.Windows.Forms.Button()
        Me.btn00 = New System.Windows.Forms.Button()
        Me.btn0 = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btn9 = New System.Windows.Forms.Button()
        Me.btn8 = New System.Windows.Forms.Button()
        Me.btn7 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItemNum = New BaseControl.CustomTextBoxNum()
        Me.btnItemNumUp = New System.Windows.Forms.Button()
        Me.btnItemNumDown = New System.Windows.Forms.Button()
        Me.btnCart = New System.Windows.Forms.Button()
        Me.btnTrash = New System.Windows.Forms.Button()
        Me.btnClearDisp = New System.Windows.Forms.Button()
        Me.dgvHINMTA = New BaseControl.CustomGridView()
        Me.colHINNMA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHINNUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colURIBTK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCashAdjust = New System.Windows.Forms.Button()
        Me.btnCardAdjust = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotalTax = New System.Windows.Forms.TextBox()
        Me.txtTotalPoint = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblNCSNO = New System.Windows.Forms.Label()
        Me.btnChkCard = New System.Windows.Forms.Button()
        Me.lblCCSNAME = New System.Windows.Forms.Label()
        Me.tabHINDISP.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvHINMTA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnReceipt
        '
        Me.btnReceipt.BackColor = System.Drawing.Color.GreenYellow
        Me.btnReceipt.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnReceipt.ForeColor = System.Drawing.Color.Black
        Me.btnReceipt.Location = New System.Drawing.Point(310, 0)
        Me.btnReceipt.Name = "btnReceipt"
        Me.btnReceipt.Size = New System.Drawing.Size(315, 103)
        Me.btnReceipt.TabIndex = 8
        Me.btnReceipt.TabStop = False
        Me.btnReceipt.Text = "レシート再発行"
        Me.btnReceipt.UseVisualStyleBackColor = False
        '
        'btnSlipCancel
        '
        Me.btnSlipCancel.BackColor = System.Drawing.Color.PaleTurquoise
        Me.btnSlipCancel.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSlipCancel.ForeColor = System.Drawing.Color.Black
        Me.btnSlipCancel.Location = New System.Drawing.Point(626, 0)
        Me.btnSlipCancel.Name = "btnSlipCancel"
        Me.btnSlipCancel.Size = New System.Drawing.Size(315, 103)
        Me.btnSlipCancel.TabIndex = 9
        Me.btnSlipCancel.TabStop = False
        Me.btnSlipCancel.Text = "伝票取消"
        Me.btnSlipCancel.UseVisualStyleBackColor = False
        '
        'tabHINDISP
        '
        Me.tabHINDISP.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.tabHINDISP.Controls.Add(Me.TabPage1)
        Me.tabHINDISP.Controls.Add(Me.TabPage2)
        Me.tabHINDISP.Controls.Add(Me.TabPage3)
        Me.tabHINDISP.Controls.Add(Me.TabPage4)
        Me.tabHINDISP.Controls.Add(Me.TabPage5)
        Me.tabHINDISP.Controls.Add(Me.TabPage6)
        Me.tabHINDISP.Controls.Add(Me.TabPage7)
        Me.tabHINDISP.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tabHINDISP.ItemSize = New System.Drawing.Size(208, 90)
        Me.tabHINDISP.Location = New System.Drawing.Point(9, 141)
        Me.tabHINDISP.Multiline = True
        Me.tabHINDISP.Name = "tabHINDISP"
        Me.tabHINDISP.SelectedIndex = 0
        Me.tabHINDISP.Size = New System.Drawing.Size(858, 839)
        Me.tabHINDISP.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabHINDISP.TabIndex = 11
        Me.tabHINDISP.TabStop = False
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(670, 831)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "商品１"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(670, 831)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "商品２"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage3.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(670, 831)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "商品３"
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage4.Location = New System.Drawing.Point(4, 4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(670, 831)
        Me.TabPage4.TabIndex = 4
        Me.TabPage4.Text = "商品4"
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage5.Location = New System.Drawing.Point(4, 4)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(670, 831)
        Me.TabPage5.TabIndex = 5
        Me.TabPage5.Text = "商品5"
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage6.Location = New System.Drawing.Point(4, 4)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(670, 831)
        Me.TabPage6.TabIndex = 6
        Me.TabPage6.Text = "商品6"
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.FloralWhite
        Me.TabPage7.Controls.Add(Me.rdoHINKBN2)
        Me.TabPage7.Controls.Add(Me.rdoHINKBN1)
        Me.TabPage7.Controls.Add(Me.txtHINNMA)
        Me.TabPage7.Controls.Add(Me.lblPOINT)
        Me.TabPage7.Controls.Add(Me.Label7)
        Me.TabPage7.Controls.Add(Me.Label6)
        Me.TabPage7.Controls.Add(Me.txtPrice)
        Me.TabPage7.Controls.Add(Me.Label5)
        Me.TabPage7.Controls.Add(Me.GroupBox2)
        Me.TabPage7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabPage7.Location = New System.Drawing.Point(4, 4)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(670, 831)
        Me.TabPage7.TabIndex = 3
        Me.TabPage7.Text = "手入力"
        '
        'rdoHINKBN2
        '
        Me.rdoHINKBN2.AutoSize = True
        Me.rdoHINKBN2.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoHINKBN2.Location = New System.Drawing.Point(250, 209)
        Me.rdoHINKBN2.Name = "rdoHINKBN2"
        Me.rdoHINKBN2.Size = New System.Drawing.Size(158, 44)
        Me.rdoHINKBN2.TabIndex = 303
        Me.rdoHINKBN2.Text = "雑収入"
        Me.rdoHINKBN2.UseVisualStyleBackColor = True
        '
        'rdoHINKBN1
        '
        Me.rdoHINKBN1.AutoSize = True
        Me.rdoHINKBN1.Checked = True
        Me.rdoHINKBN1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdoHINKBN1.Location = New System.Drawing.Point(48, 209)
        Me.rdoHINKBN1.Name = "rdoHINKBN1"
        Me.rdoHINKBN1.Size = New System.Drawing.Size(117, 44)
        Me.rdoHINKBN1.TabIndex = 302
        Me.rdoHINKBN1.TabStop = True
        Me.rdoHINKBN1.Text = "商品"
        Me.rdoHINKBN1.UseVisualStyleBackColor = True
        '
        'txtHINNMA
        '
        Me.txtHINNMA.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHINNMA.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtHINNMA.Location = New System.Drawing.Point(51, 334)
        Me.txtHINNMA.MaxLength = 10
        Me.txtHINNMA.Name = "txtHINNMA"
        Me.txtHINNMA.Size = New System.Drawing.Size(553, 39)
        Me.txtHINNMA.TabIndex = 300
        Me.txtHINNMA.Visible = False
        '
        'lblPOINT
        '
        Me.lblPOINT.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPOINT.ForeColor = System.Drawing.Color.Maroon
        Me.lblPOINT.Location = New System.Drawing.Point(51, 157)
        Me.lblPOINT.Name = "lblPOINT"
        Me.lblPOINT.Size = New System.Drawing.Size(328, 20)
        Me.lblPOINT.TabIndex = 214
        Me.lblPOINT.Text = "(Point: 0)"
        Me.lblPOINT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.FloralWhite
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(379, 116)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 33)
        Me.Label7.TabIndex = 213
        Me.Label7.Text = "円（税込）"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(51, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(328, 44)
        Me.Label6.TabIndex = 212
        Me.Label6.Text = "金額"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPrice
        '
        Me.txtPrice.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrice.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPrice.Format = "#,##0"
        Me.txtPrice.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPrice.Location = New System.Drawing.Point(51, 115)
        Me.txtPrice.MaxLength = 5
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(328, 39)
        Me.txtPrice.TabIndex = 301
        Me.txtPrice.Tag = ""
        Me.txtPrice.Text = "0"
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(51, 291)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(553, 44)
        Me.Label5.TabIndex = 210
        Me.Label5.Text = "商品名"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnBACK)
        Me.GroupBox2.Controls.Add(Me.btnCLEAR)
        Me.GroupBox2.Controls.Add(Me.btn00)
        Me.GroupBox2.Controls.Add(Me.btn0)
        Me.GroupBox2.Controls.Add(Me.btn3)
        Me.GroupBox2.Controls.Add(Me.btn2)
        Me.GroupBox2.Controls.Add(Me.btn1)
        Me.GroupBox2.Controls.Add(Me.btn6)
        Me.GroupBox2.Controls.Add(Me.btn5)
        Me.GroupBox2.Controls.Add(Me.btn4)
        Me.GroupBox2.Controls.Add(Me.btn9)
        Me.GroupBox2.Controls.Add(Me.btn8)
        Me.GroupBox2.Controls.Add(Me.btn7)
        Me.GroupBox2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(48, 397)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(550, 388)
        Me.GroupBox2.TabIndex = 175
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "引き落とし金額入力画面"
        '
        'btnBACK
        '
        Me.btnBACK.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnBACK.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBACK.ForeColor = System.Drawing.Color.Black
        Me.btnBACK.Location = New System.Drawing.Point(333, 130)
        Me.btnBACK.Name = "btnBACK"
        Me.btnBACK.Size = New System.Drawing.Size(185, 68)
        Me.btnBACK.TabIndex = 194
        Me.btnBACK.TabStop = False
        Me.btnBACK.Text = "BACK"
        Me.btnBACK.UseVisualStyleBackColor = False
        '
        'btnCLEAR
        '
        Me.btnCLEAR.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnCLEAR.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCLEAR.ForeColor = System.Drawing.Color.Black
        Me.btnCLEAR.Location = New System.Drawing.Point(333, 41)
        Me.btnCLEAR.Name = "btnCLEAR"
        Me.btnCLEAR.Size = New System.Drawing.Size(185, 68)
        Me.btnCLEAR.TabIndex = 193
        Me.btnCLEAR.TabStop = False
        Me.btnCLEAR.Text = "クリア"
        Me.btnCLEAR.UseVisualStyleBackColor = False
        '
        'btn00
        '
        Me.btn00.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn00.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn00.ForeColor = System.Drawing.Color.Black
        Me.btn00.Location = New System.Drawing.Point(123, 302)
        Me.btn00.Name = "btn00"
        Me.btn00.Size = New System.Drawing.Size(175, 68)
        Me.btn00.TabIndex = 192
        Me.btn00.TabStop = False
        Me.btn00.Text = "00"
        Me.btn00.UseVisualStyleBackColor = False
        '
        'btn0
        '
        Me.btn0.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn0.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn0.ForeColor = System.Drawing.Color.Black
        Me.btn0.Location = New System.Drawing.Point(26, 302)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(79, 68)
        Me.btn0.TabIndex = 191
        Me.btn0.TabStop = False
        Me.btn0.Text = "0"
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn3.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn3.ForeColor = System.Drawing.Color.Black
        Me.btn3.Location = New System.Drawing.Point(219, 217)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(79, 68)
        Me.btn3.TabIndex = 190
        Me.btn3.TabStop = False
        Me.btn3.Text = "3"
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn2.ForeColor = System.Drawing.Color.Black
        Me.btn2.Location = New System.Drawing.Point(123, 217)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(79, 68)
        Me.btn2.TabIndex = 189
        Me.btn2.TabStop = False
        Me.btn2.Text = "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn1.ForeColor = System.Drawing.Color.Black
        Me.btn1.Location = New System.Drawing.Point(26, 217)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(79, 68)
        Me.btn1.TabIndex = 188
        Me.btn1.TabStop = False
        Me.btn1.Text = "1"
        Me.btn1.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn6.ForeColor = System.Drawing.Color.Black
        Me.btn6.Location = New System.Drawing.Point(219, 130)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(79, 68)
        Me.btn6.TabIndex = 187
        Me.btn6.TabStop = False
        Me.btn6.Text = "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn5.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn5.ForeColor = System.Drawing.Color.Black
        Me.btn5.Location = New System.Drawing.Point(123, 130)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(79, 68)
        Me.btn5.TabIndex = 186
        Me.btn5.TabStop = False
        Me.btn5.Text = "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn4.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn4.ForeColor = System.Drawing.Color.Black
        Me.btn4.Location = New System.Drawing.Point(26, 130)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(79, 68)
        Me.btn4.TabIndex = 185
        Me.btn4.TabStop = False
        Me.btn4.Text = "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn9.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn9.ForeColor = System.Drawing.Color.Black
        Me.btn9.Location = New System.Drawing.Point(219, 41)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(79, 68)
        Me.btn9.TabIndex = 184
        Me.btn9.TabStop = False
        Me.btn9.Text = "9"
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn8.ForeColor = System.Drawing.Color.Black
        Me.btn8.Location = New System.Drawing.Point(123, 41)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(79, 68)
        Me.btn8.TabIndex = 183
        Me.btn8.TabStop = False
        Me.btn8.Text = "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btn7.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn7.ForeColor = System.Drawing.Color.Black
        Me.btn7.Location = New System.Drawing.Point(26, 41)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(79, 68)
        Me.btn7.TabIndex = 182
        Me.btn7.TabStop = False
        Me.btn7.Text = "7"
        Me.btn7.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(868, 217)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 71)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "×"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtItemNum
        '
        Me.txtItemNum.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemNum.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtItemNum.Format = "#,##0"
        Me.txtItemNum.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtItemNum.Location = New System.Drawing.Point(924, 214)
        Me.txtItemNum.MaxLength = 2
        Me.txtItemNum.Name = "txtItemNum"
        Me.txtItemNum.Size = New System.Drawing.Size(73, 71)
        Me.txtItemNum.TabIndex = 13
        Me.txtItemNum.Tag = ""
        Me.txtItemNum.Text = "99"
        Me.txtItemNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnItemNumUp
        '
        Me.btnItemNumUp.BackColor = System.Drawing.Color.SkyBlue
        Me.btnItemNumUp.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnItemNumUp.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnItemNumUp.Location = New System.Drawing.Point(1015, 167)
        Me.btnItemNumUp.Name = "btnItemNumUp"
        Me.btnItemNumUp.Size = New System.Drawing.Size(79, 69)
        Me.btnItemNumUp.TabIndex = 14
        Me.btnItemNumUp.TabStop = False
        Me.btnItemNumUp.Text = "▲"
        Me.btnItemNumUp.UseVisualStyleBackColor = False
        '
        'btnItemNumDown
        '
        Me.btnItemNumDown.BackColor = System.Drawing.Color.Pink
        Me.btnItemNumDown.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnItemNumDown.ForeColor = System.Drawing.Color.Red
        Me.btnItemNumDown.Location = New System.Drawing.Point(1015, 242)
        Me.btnItemNumDown.Name = "btnItemNumDown"
        Me.btnItemNumDown.Size = New System.Drawing.Size(79, 69)
        Me.btnItemNumDown.TabIndex = 15
        Me.btnItemNumDown.TabStop = False
        Me.btnItemNumDown.Text = "▼"
        Me.btnItemNumDown.UseVisualStyleBackColor = False
        '
        'btnCart
        '
        Me.btnCart.BackColor = System.Drawing.Color.White
        Me.btnCart.Image = CType(resources.GetObject("btnCart.Image"), System.Drawing.Image)
        Me.btnCart.Location = New System.Drawing.Point(891, 354)
        Me.btnCart.Name = "btnCart"
        Me.btnCart.Size = New System.Drawing.Size(222, 199)
        Me.btnCart.TabIndex = 16
        Me.btnCart.TabStop = False
        Me.btnCart.UseVisualStyleBackColor = False
        '
        'btnTrash
        '
        Me.btnTrash.BackColor = System.Drawing.Color.White
        Me.btnTrash.Image = CType(resources.GetObject("btnTrash.Image"), System.Drawing.Image)
        Me.btnTrash.Location = New System.Drawing.Point(891, 559)
        Me.btnTrash.Name = "btnTrash"
        Me.btnTrash.Size = New System.Drawing.Size(222, 199)
        Me.btnTrash.TabIndex = 17
        Me.btnTrash.TabStop = False
        Me.btnTrash.UseVisualStyleBackColor = False
        '
        'btnClearDisp
        '
        Me.btnClearDisp.BackColor = System.Drawing.Color.Cyan
        Me.btnClearDisp.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnClearDisp.ForeColor = System.Drawing.Color.Black
        Me.btnClearDisp.Location = New System.Drawing.Point(891, 816)
        Me.btnClearDisp.Name = "btnClearDisp"
        Me.btnClearDisp.Size = New System.Drawing.Size(222, 160)
        Me.btnClearDisp.TabIndex = 18
        Me.btnClearDisp.TabStop = False
        Me.btnClearDisp.Text = "クリア" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(F11)"
        Me.btnClearDisp.UseVisualStyleBackColor = False
        '
        'dgvHINMTA
        '
        Me.dgvHINMTA.AllowUserToAddRows = False
        Me.dgvHINMTA.AllowUserToDeleteRows = False
        Me.dgvHINMTA.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvHINMTA.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvHINMTA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvHINMTA.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvHINMTA.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvHINMTA.ColumnHeadersHeight = 50
        Me.dgvHINMTA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvHINMTA.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colHINNMA, Me.colHINNUM, Me.colURIBTK, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvHINMTA.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvHINMTA.EnableHeadersVisualStyles = False
        Me.dgvHINMTA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvHINMTA.Location = New System.Drawing.Point(1122, 141)
        Me.dgvHINMTA.MultiSelect = False
        Me.dgvHINMTA.Name = "dgvHINMTA"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvHINMTA.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvHINMTA.RowHeadersVisible = False
        Me.dgvHINMTA.RowHeadersWidth = 61
        Me.dgvHINMTA.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvHINMTA.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvHINMTA.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvHINMTA.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvHINMTA.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow
        Me.dgvHINMTA.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvHINMTA.RowTemplate.Height = 40
        Me.dgvHINMTA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvHINMTA.Size = New System.Drawing.Size(764, 412)
        Me.dgvHINMTA.TabIndex = 102
        Me.dgvHINMTA.TabStop = False
        '
        'colHINNMA
        '
        Me.colHINNMA.DataPropertyName = "HINNMA"
        Me.colHINNMA.HeaderText = "商品名"
        Me.colHINNMA.MaxInputLength = 15
        Me.colHINNMA.Name = "colHINNMA"
        Me.colHINNMA.ReadOnly = True
        Me.colHINNMA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colHINNMA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colHINNMA.Width = 375
        '
        'colHINNUM
        '
        Me.colHINNUM.DataPropertyName = "HINNUM"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "#,0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colHINNUM.DefaultCellStyle = DataGridViewCellStyle3
        Me.colHINNUM.HeaderText = "数量"
        Me.colHINNUM.MaxInputLength = 2
        Me.colHINNUM.Name = "colHINNUM"
        Me.colHINNUM.ReadOnly = True
        Me.colHINNUM.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colHINNUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colHINNUM.Width = 150
        '
        'colURIBTK
        '
        Me.colURIBTK.DataPropertyName = "URIBTK"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "#,0"
        Me.colURIBTK.DefaultCellStyle = DataGridViewCellStyle4
        Me.colURIBTK.HeaderText = "金額"
        Me.colURIBTK.MaxInputLength = 6
        Me.colURIBTK.Name = "colURIBTK"
        Me.colURIBTK.ReadOnly = True
        Me.colURIBTK.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colURIBTK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colURIBTK.Width = 215
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "BMNCD"
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Visible = False
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "BUNCDA"
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Visible = False
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "BUNCDB"
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Visible = False
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "BUNCDC"
        Me.Column4.HeaderText = "Column4"
        Me.Column4.Name = "Column4"
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Visible = False
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "HINCD"
        Me.Column5.HeaderText = "Column5"
        Me.Column5.Name = "Column5"
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column5.Visible = False
        '
        'Column6
        '
        Me.Column6.DataPropertyName = "ZEITK"
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column6.Visible = False
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "POINT"
        Me.Column7.HeaderText = "Column7"
        Me.Column7.Name = "Column7"
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column7.Visible = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.OrangeRed
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(1239, 575)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(249, 61)
        Me.Label14.TabIndex = 205
        Me.Label14.Text = "合　計"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FloralWhite
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Location = New System.Drawing.Point(1801, 584)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 48)
        Me.Label10.TabIndex = 204
        Me.Label10.Text = "円"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(1239, 688)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(249, 42)
        Me.Label2.TabIndex = 208
        Me.Label2.Text = "取得ポイント"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCashAdjust
        '
        Me.btnCashAdjust.BackColor = System.Drawing.Color.Yellow
        Me.btnCashAdjust.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCashAdjust.ForeColor = System.Drawing.Color.Black
        Me.btnCashAdjust.Location = New System.Drawing.Point(1159, 847)
        Me.btnCashAdjust.Name = "btnCashAdjust"
        Me.btnCashAdjust.Size = New System.Drawing.Size(358, 133)
        Me.btnCashAdjust.TabIndex = 209
        Me.btnCashAdjust.Text = "現金精算"
        Me.btnCashAdjust.UseVisualStyleBackColor = False
        Me.btnCashAdjust.Visible = False
        '
        'btnCardAdjust
        '
        Me.btnCardAdjust.BackColor = System.Drawing.Color.Yellow
        Me.btnCardAdjust.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCardAdjust.ForeColor = System.Drawing.Color.Black
        Me.btnCardAdjust.Location = New System.Drawing.Point(1528, 847)
        Me.btnCardAdjust.Name = "btnCardAdjust"
        Me.btnCardAdjust.Size = New System.Drawing.Size(358, 133)
        Me.btnCardAdjust.TabIndex = 210
        Me.btnCardAdjust.Text = "ICカード精算"
        Me.btnCardAdjust.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Label3.Location = New System.Drawing.Point(171, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 83)
        Me.Label3.TabIndex = 211
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Label4.Location = New System.Drawing.Point(1736, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 83)
        Me.Label4.TabIndex = 212
        '
        'txtTotalTax
        '
        Me.txtTotalTax.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalTax.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTotalTax.Location = New System.Drawing.Point(1494, 635)
        Me.txtTotalTax.Name = "txtTotalTax"
        Me.txtTotalTax.ReadOnly = True
        Me.txtTotalTax.Size = New System.Drawing.Size(304, 39)
        Me.txtTotalTax.TabIndex = 101
        Me.txtTotalTax.TabStop = False
        Me.txtTotalTax.Text = "(内) 100"
        Me.txtTotalTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalPoint
        '
        Me.txtTotalPoint.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPoint.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTotalPoint.Location = New System.Drawing.Point(1494, 688)
        Me.txtTotalPoint.Name = "txtTotalPoint"
        Me.txtTotalPoint.ReadOnly = True
        Me.txtTotalPoint.Size = New System.Drawing.Size(304, 42)
        Me.txtTotalPoint.TabIndex = 102
        Me.txtTotalPoint.TabStop = False
        Me.txtTotalPoint.Text = "0"
        Me.txtTotalPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotal.Location = New System.Drawing.Point(1494, 575)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(304, 61)
        Me.txtTotal.TabIndex = 100
        Me.txtTotal.TabStop = False
        Me.txtTotal.Text = "1,230"
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(1159, 751)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(194, 45)
        Me.Label8.TabIndex = 213
        Me.Label8.Text = "顧客番号"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label8.Visible = False
        '
        'lblNCSNO
        '
        Me.lblNCSNO.BackColor = System.Drawing.Color.White
        Me.lblNCSNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNCSNO.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNCSNO.ForeColor = System.Drawing.Color.Black
        Me.lblNCSNO.Location = New System.Drawing.Point(1352, 751)
        Me.lblNCSNO.Name = "lblNCSNO"
        Me.lblNCSNO.Size = New System.Drawing.Size(181, 45)
        Me.lblNCSNO.TabIndex = 214
        Me.lblNCSNO.Text = "00000000"
        Me.lblNCSNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNCSNO.Visible = False
        '
        'btnChkCard
        '
        Me.btnChkCard.BackColor = System.Drawing.Color.ForestGreen
        Me.btnChkCard.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnChkCard.ForeColor = System.Drawing.Color.White
        Me.btnChkCard.Location = New System.Drawing.Point(1682, 751)
        Me.btnChkCard.Name = "btnChkCard"
        Me.btnChkCard.Size = New System.Drawing.Size(204, 90)
        Me.btnChkCard.TabIndex = 216
        Me.btnChkCard.Text = "カード確認"
        Me.btnChkCard.UseVisualStyleBackColor = False
        Me.btnChkCard.Visible = False
        '
        'lblCCSNAME
        '
        Me.lblCCSNAME.BackColor = System.Drawing.Color.White
        Me.lblCCSNAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCCSNAME.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCCSNAME.ForeColor = System.Drawing.Color.Black
        Me.lblCCSNAME.Location = New System.Drawing.Point(1352, 795)
        Me.lblCCSNAME.Name = "lblCCSNAME"
        Me.lblCCSNAME.Size = New System.Drawing.Size(322, 45)
        Me.lblCCSNAME.TabIndex = 217
        Me.lblCCSNAME.Text = "ああああああああああ"
        Me.lblCCSNAME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCCSNAME.Visible = False
        '
        'frmHINDISP01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1041)
        Me.Controls.Add(Me.lblCCSNAME)
        Me.Controls.Add(Me.btnChkCard)
        Me.Controls.Add(Me.lblNCSNO)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtTotalPoint)
        Me.Controls.Add(Me.txtTotalTax)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCardAdjust)
        Me.Controls.Add(Me.btnCashAdjust)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.dgvHINMTA)
        Me.Controls.Add(Me.btnClearDisp)
        Me.Controls.Add(Me.btnTrash)
        Me.Controls.Add(Me.btnCart)
        Me.Controls.Add(Me.btnItemNumDown)
        Me.Controls.Add(Me.btnItemNumUp)
        Me.Controls.Add(Me.txtItemNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tabHINDISP)
        Me.Controls.Add(Me.btnSlipCancel)
        Me.Controls.Add(Me.btnReceipt)
        Me.Name = "frmHINDISP01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.btnReceipt, 0)
        Me.Controls.SetChildIndex(Me.btnSlipCancel, 0)
        Me.Controls.SetChildIndex(Me.tabHINDISP, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.txtItemNum, 0)
        Me.Controls.SetChildIndex(Me.btnItemNumUp, 0)
        Me.Controls.SetChildIndex(Me.btnItemNumDown, 0)
        Me.Controls.SetChildIndex(Me.btnCart, 0)
        Me.Controls.SetChildIndex(Me.btnTrash, 0)
        Me.Controls.SetChildIndex(Me.btnClearDisp, 0)
        Me.Controls.SetChildIndex(Me.dgvHINMTA, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.Label14, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.btnCashAdjust, 0)
        Me.Controls.SetChildIndex(Me.btnCardAdjust, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.txtTotalTax, 0)
        Me.Controls.SetChildIndex(Me.txtTotalPoint, 0)
        Me.Controls.SetChildIndex(Me.txtTotal, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.lblNCSNO, 0)
        Me.Controls.SetChildIndex(Me.btnChkCard, 0)
        Me.Controls.SetChildIndex(Me.lblCCSNAME, 0)
        Me.tabHINDISP.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvHINMTA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReceipt As System.Windows.Forms.Button
    Friend WithEvents btnSlipCancel As System.Windows.Forms.Button
    Friend WithEvents tabHINDISP As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemNum As BaseControl.CustomTextBoxNum
    Friend WithEvents btnItemNumUp As System.Windows.Forms.Button
    Friend WithEvents btnItemNumDown As System.Windows.Forms.Button
    Friend WithEvents btnCart As System.Windows.Forms.Button
    Friend WithEvents btnTrash As System.Windows.Forms.Button
    Friend WithEvents btnClearDisp As System.Windows.Forms.Button
    Friend WithEvents dgvHINMTA As BaseControl.CustomGridView
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCashAdjust As System.Windows.Forms.Button
    Friend WithEvents btnCardAdjust As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As BaseControl.CustomTextBoxNum
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBACK As System.Windows.Forms.Button
    Friend WithEvents btnCLEAR As System.Windows.Forms.Button
    Friend WithEvents btn00 As System.Windows.Forms.Button
    Friend WithEvents btn0 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents lblPOINT As System.Windows.Forms.Label
    Friend WithEvents txtHINNMA As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTax As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPoint As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents colHINNMA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHINNUM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colURIBTK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblNCSNO As System.Windows.Forms.Label
    Friend WithEvents btnChkCard As System.Windows.Forms.Button
    Friend WithEvents lblCCSNAME As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents rdoHINKBN2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHINKBN1 As System.Windows.Forms.RadioButton

End Class
