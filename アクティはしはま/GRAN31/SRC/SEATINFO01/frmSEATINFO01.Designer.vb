<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSEATINFO01
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvSEAT = New BaseControl.CustomGridView()
        Me.FLRKB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SEATNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CKBNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCSNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALLKIN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USEBALL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STARTTIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USETIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.dgvErrSEAT = New BaseControl.CustomGridView()
        Me.ERRFLRKB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ERRSEATNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ERRSTATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvCallSEAT = New BaseControl.CustomGridView()
        Me.CALLFLRKB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CALLSEATNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CALLSTATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgvSEAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvErrSEAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCallSEAT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvSEAT
        '
        Me.dgvSEAT.AllowUserToAddRows = False
        Me.dgvSEAT.AllowUserToDeleteRows = False
        Me.dgvSEAT.AllowUserToResizeColumns = False
        Me.dgvSEAT.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvSEAT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSEAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvSEAT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSEAT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSEAT.ColumnHeadersHeight = 30
        Me.dgvSEAT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSEAT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FLRKB, Me.SEATNO, Me.STATE, Me.CKBNAME, Me.CCSNAME, Me.BALLKIN, Me.USEBALL, Me.STARTTIME, Me.USETIME})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSEAT.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvSEAT.EnableHeadersVisualStyles = False
        Me.dgvSEAT.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvSEAT.Location = New System.Drawing.Point(60, 150)
        Me.dgvSEAT.MultiSelect = False
        Me.dgvSEAT.Name = "dgvSEAT"
        Me.dgvSEAT.RowHeadersVisible = False
        Me.dgvSEAT.RowHeadersWidth = 30
        Me.dgvSEAT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvSEAT.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvSEAT.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvSEAT.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvSEAT.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSEAT.RowTemplate.Height = 40
        Me.dgvSEAT.Size = New System.Drawing.Size(1089, 832)
        Me.dgvSEAT.TabIndex = 195
        '
        'FLRKB
        '
        Me.FLRKB.DataPropertyName = "FLRKB"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.FLRKB.DefaultCellStyle = DataGridViewCellStyle3
        Me.FLRKB.HeaderText = "階"
        Me.FLRKB.Name = "FLRKB"
        Me.FLRKB.ReadOnly = True
        Me.FLRKB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FLRKB.Width = 50
        '
        'SEATNO
        '
        Me.SEATNO.DataPropertyName = "SEATNO"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.SEATNO.DefaultCellStyle = DataGridViewCellStyle4
        Me.SEATNO.HeaderText = "№"
        Me.SEATNO.MaxInputLength = 10
        Me.SEATNO.Name = "SEATNO"
        Me.SEATNO.ReadOnly = True
        Me.SEATNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SEATNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SEATNO.Width = 70
        '
        'STATE
        '
        Me.STATE.DataPropertyName = "STATE"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.STATE.DefaultCellStyle = DataGridViewCellStyle5
        Me.STATE.HeaderText = "打席状態"
        Me.STATE.MaxInputLength = 5
        Me.STATE.Name = "STATE"
        Me.STATE.ReadOnly = True
        Me.STATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.STATE.Width = 150
        '
        'CKBNAME
        '
        Me.CKBNAME.DataPropertyName = "CKBNAME"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.CKBNAME.DefaultCellStyle = DataGridViewCellStyle6
        Me.CKBNAME.HeaderText = "顧客種別"
        Me.CKBNAME.Name = "CKBNAME"
        Me.CKBNAME.ReadOnly = True
        Me.CKBNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CKBNAME.Width = 150
        '
        'CCSNAME
        '
        Me.CCSNAME.DataPropertyName = "CCSNAME"
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CCSNAME.DefaultCellStyle = DataGridViewCellStyle7
        Me.CCSNAME.HeaderText = "氏名"
        Me.CCSNAME.Name = "CCSNAME"
        Me.CCSNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CCSNAME.Width = 250
        '
        'BALLKIN
        '
        Me.BALLKIN.DataPropertyName = "BALLKIN"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.BALLKIN.DefaultCellStyle = DataGridViewCellStyle8
        Me.BALLKIN.HeaderText = "単価"
        Me.BALLKIN.MaxInputLength = 2
        Me.BALLKIN.Name = "BALLKIN"
        Me.BALLKIN.ReadOnly = True
        Me.BALLKIN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'USEBALL
        '
        Me.USEBALL.DataPropertyName = "USEBALL"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.USEBALL.DefaultCellStyle = DataGridViewCellStyle9
        Me.USEBALL.HeaderText = "使用球数"
        Me.USEBALL.MaxInputLength = 2
        Me.USEBALL.Name = "USEBALL"
        Me.USEBALL.ReadOnly = True
        Me.USEBALL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'STARTTIME
        '
        Me.STARTTIME.DataPropertyName = "STARTTIME"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.STARTTIME.DefaultCellStyle = DataGridViewCellStyle10
        Me.STARTTIME.HeaderText = "開始時間"
        Me.STARTTIME.Name = "STARTTIME"
        Me.STARTTIME.ReadOnly = True
        Me.STARTTIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'USETIME
        '
        Me.USETIME.DataPropertyName = "USETIME"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        Me.USETIME.DefaultCellStyle = DataGridViewCellStyle11
        Me.USETIME.HeaderText = "使用時間"
        Me.USETIME.Name = "USETIME"
        Me.USETIME.ReadOnly = True
        Me.USETIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Timer1
        '
        '
        'dgvErrSEAT
        '
        Me.dgvErrSEAT.AllowUserToAddRows = False
        Me.dgvErrSEAT.AllowUserToDeleteRows = False
        Me.dgvErrSEAT.AllowUserToResizeColumns = False
        Me.dgvErrSEAT.AllowUserToResizeRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.White
        Me.dgvErrSEAT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvErrSEAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvErrSEAT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle14.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvErrSEAT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.dgvErrSEAT.ColumnHeadersHeight = 30
        Me.dgvErrSEAT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvErrSEAT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ERRFLRKB, Me.ERRSEATNO, Me.ERRSTATE})
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvErrSEAT.DefaultCellStyle = DataGridViewCellStyle18
        Me.dgvErrSEAT.EnableHeadersVisualStyles = False
        Me.dgvErrSEAT.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvErrSEAT.Location = New System.Drawing.Point(1239, 150)
        Me.dgvErrSEAT.MultiSelect = False
        Me.dgvErrSEAT.Name = "dgvErrSEAT"
        Me.dgvErrSEAT.RowHeadersVisible = False
        Me.dgvErrSEAT.RowHeadersWidth = 30
        Me.dgvErrSEAT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvErrSEAT.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvErrSEAT.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvErrSEAT.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvErrSEAT.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvErrSEAT.RowTemplate.Height = 40
        Me.dgvErrSEAT.Size = New System.Drawing.Size(289, 832)
        Me.dgvErrSEAT.TabIndex = 393
        '
        'ERRFLRKB
        '
        Me.ERRFLRKB.DataPropertyName = "ERRFLRKB"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle15.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black
        Me.ERRFLRKB.DefaultCellStyle = DataGridViewCellStyle15
        Me.ERRFLRKB.HeaderText = "階"
        Me.ERRFLRKB.Name = "ERRFLRKB"
        Me.ERRFLRKB.ReadOnly = True
        Me.ERRFLRKB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ERRFLRKB.Width = 50
        '
        'ERRSEATNO
        '
        Me.ERRSEATNO.DataPropertyName = "ERRSEATNO"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black
        Me.ERRSEATNO.DefaultCellStyle = DataGridViewCellStyle16
        Me.ERRSEATNO.HeaderText = "№"
        Me.ERRSEATNO.MaxInputLength = 10
        Me.ERRSEATNO.Name = "ERRSEATNO"
        Me.ERRSEATNO.ReadOnly = True
        Me.ERRSEATNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ERRSEATNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ERRSEATNO.Width = 70
        '
        'ERRSTATE
        '
        Me.ERRSTATE.DataPropertyName = "ERRSTATE"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle17.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black
        Me.ERRSTATE.DefaultCellStyle = DataGridViewCellStyle17
        Me.ERRSTATE.HeaderText = "打席状態"
        Me.ERRSTATE.MaxInputLength = 5
        Me.ERRSTATE.Name = "ERRSTATE"
        Me.ERRSTATE.ReadOnly = True
        Me.ERRSTATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ERRSTATE.Width = 150
        '
        'dgvCallSEAT
        '
        Me.dgvCallSEAT.AllowUserToAddRows = False
        Me.dgvCallSEAT.AllowUserToDeleteRows = False
        Me.dgvCallSEAT.AllowUserToResizeColumns = False
        Me.dgvCallSEAT.AllowUserToResizeRows = False
        DataGridViewCellStyle19.BackColor = System.Drawing.Color.White
        Me.dgvCallSEAT.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle19
        Me.dgvCallSEAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCallSEAT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle20.BackColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle20.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCallSEAT.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.dgvCallSEAT.ColumnHeadersHeight = 30
        Me.dgvCallSEAT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvCallSEAT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CALLFLRKB, Me.CALLSEATNO, Me.CALLSTATE})
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle24.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.Yellow
        DataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCallSEAT.DefaultCellStyle = DataGridViewCellStyle24
        Me.dgvCallSEAT.EnableHeadersVisualStyles = False
        Me.dgvCallSEAT.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.dgvCallSEAT.Location = New System.Drawing.Point(1534, 150)
        Me.dgvCallSEAT.MultiSelect = False
        Me.dgvCallSEAT.Name = "dgvCallSEAT"
        Me.dgvCallSEAT.RowHeadersVisible = False
        Me.dgvCallSEAT.RowHeadersWidth = 30
        Me.dgvCallSEAT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCallSEAT.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgvCallSEAT.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("ＭＳ ゴシック", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dgvCallSEAT.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgvCallSEAT.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvCallSEAT.RowTemplate.Height = 40
        Me.dgvCallSEAT.Size = New System.Drawing.Size(289, 832)
        Me.dgvCallSEAT.TabIndex = 394
        '
        'CALLFLRKB
        '
        Me.CALLFLRKB.DataPropertyName = "CALLFLRKB"
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle21.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black
        Me.CALLFLRKB.DefaultCellStyle = DataGridViewCellStyle21
        Me.CALLFLRKB.HeaderText = "階"
        Me.CALLFLRKB.Name = "CALLFLRKB"
        Me.CALLFLRKB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CALLFLRKB.Width = 50
        '
        'CALLSEATNO
        '
        Me.CALLSEATNO.DataPropertyName = "CALLSEATNO"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle22.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black
        Me.CALLSEATNO.DefaultCellStyle = DataGridViewCellStyle22
        Me.CALLSEATNO.HeaderText = "№"
        Me.CALLSEATNO.MaxInputLength = 10
        Me.CALLSEATNO.Name = "CALLSEATNO"
        Me.CALLSEATNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.CALLSEATNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CALLSEATNO.Width = 70
        '
        'CALLSTATE
        '
        Me.CALLSTATE.DataPropertyName = "CALLSTATE"
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle23.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle23.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.Black
        Me.CALLSTATE.DefaultCellStyle = DataGridViewCellStyle23
        Me.CALLSTATE.HeaderText = "打席状態"
        Me.CALLSTATE.MaxInputLength = 5
        Me.CALLSTATE.Name = "CALLSTATE"
        Me.CALLSTATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CALLSTATE.Width = 150
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Red
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(1239, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(289, 30)
        Me.Label2.TabIndex = 395
        Me.Label2.Text = "通信不能打席"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(1534, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(289, 30)
        Me.Label3.TabIndex = 396
        Me.Label3.Text = "呼び出し打席"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmSEATINFO01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvCallSEAT)
        Me.Controls.Add(Me.dgvErrSEAT)
        Me.Controls.Add(Me.dgvSEAT)
        Me.Name = "frmSEATINFO01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.dgvSEAT, 0)
        Me.Controls.SetChildIndex(Me.dgvErrSEAT, 0)
        Me.Controls.SetChildIndex(Me.dgvCallSEAT, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        CType(Me.dgvSEAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvErrSEAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCallSEAT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvSEAT As BaseControl.CustomGridView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents dgvErrSEAT As BaseControl.CustomGridView
    Friend WithEvents ERRFLRKB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ERRSEATNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ERRSTATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvCallSEAT As BaseControl.CustomGridView
    Friend WithEvents CALLFLRKB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CALLSEATNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CALLSTATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FLRKB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SEATNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CKBNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCSNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BALLKIN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USEBALL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents STARTTIME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USETIME As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
