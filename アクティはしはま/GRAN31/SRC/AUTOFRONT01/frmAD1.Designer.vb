<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAD1
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
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblConnect = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblDeviceStatus1_128 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_64 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_32 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_16 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_8 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_0 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_4 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_1 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus1_2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblDeviceStatus2_128 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_64 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_32 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_16 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_8 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_0 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_4 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_1 = New System.Windows.Forms.Label()
        Me.lblDeviceStatus2_2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblBillState1_0 = New System.Windows.Forms.Label()
        Me.lblBillState1_16 = New System.Windows.Forms.Label()
        Me.lblBillState1_1 = New System.Windows.Forms.Label()
        Me.lblBillState1_8 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblBillState2_128 = New System.Windows.Forms.Label()
        Me.lblBillState2_0 = New System.Windows.Forms.Label()
        Me.lblBillState2_4 = New System.Windows.Forms.Label()
        Me.lblBillState2_1 = New System.Windows.Forms.Label()
        Me.lblBillState2_2 = New System.Windows.Forms.Label()
        Me.btnUpdInfo = New System.Windows.Forms.Button()
        Me.btnCleaning = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblSensorState2_8 = New System.Windows.Forms.Label()
        Me.lblSensorState2_4 = New System.Windows.Forms.Label()
        Me.lblSensorState2_7 = New System.Windows.Forms.Label()
        Me.lblSensorState2_3 = New System.Windows.Forms.Label()
        Me.lblSensorState2_6 = New System.Windows.Forms.Label()
        Me.lblSensorState2_0 = New System.Windows.Forms.Label()
        Me.lblSensorState2_2 = New System.Windows.Forms.Label()
        Me.lblSensorState2_1 = New System.Windows.Forms.Label()
        Me.lblSensorState2_5 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblSensorState1_8 = New System.Windows.Forms.Label()
        Me.lblSensorState1_4 = New System.Windows.Forms.Label()
        Me.lblSensorState1_7 = New System.Windows.Forms.Label()
        Me.lblSensorState1_3 = New System.Windows.Forms.Label()
        Me.lblSensorState1_6 = New System.Windows.Forms.Label()
        Me.lblSensorState1_0 = New System.Windows.Forms.Label()
        Me.lblSensorState1_2 = New System.Windows.Forms.Label()
        Me.lblSensorState1_1 = New System.Windows.Forms.Label()
        Me.lblSensorState1_5 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkGray
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1605, 30)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(285, 116)
        Me.btnBack.TabIndex = 32
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'lblConnect
        '
        Me.lblConnect.AutoSize = True
        Me.lblConnect.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblConnect.ForeColor = System.Drawing.Color.Silver
        Me.lblConnect.Location = New System.Drawing.Point(33, 22)
        Me.lblConnect.Name = "lblConnect"
        Me.lblConnect.Size = New System.Drawing.Size(185, 54)
        Me.lblConnect.TabIndex = 0
        Me.lblConnect.Text = "接続中"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblConnect)
        Me.Panel1.Location = New System.Drawing.Point(95, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(261, 110)
        Me.Panel1.TabIndex = 39
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.ForestGreen
        Me.btnConnect.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(1244, 30)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(285, 116)
        Me.btnConnect.TabIndex = 40
        Me.btnConnect.Text = "接続"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_128)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_64)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_32)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_16)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_8)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_0)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_4)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_1)
        Me.Panel3.Controls.Add(Me.lblDeviceStatus1_2)
        Me.Panel3.Location = New System.Drawing.Point(51, 297)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1839, 110)
        Me.Panel3.TabIndex = 41
        '
        'lblDeviceStatus1_128
        '
        Me.lblDeviceStatus1_128.AutoSize = True
        Me.lblDeviceStatus1_128.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_128.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_128.Location = New System.Drawing.Point(865, 54)
        Me.lblDeviceStatus1_128.Name = "lblDeviceStatus1_128"
        Me.lblDeviceStatus1_128.Size = New System.Drawing.Size(365, 47)
        Me.lblDeviceStatus1_128.TabIndex = 48
        Me.lblDeviceStatus1_128.Text = "クリーニング動作中"
        '
        'lblDeviceStatus1_64
        '
        Me.lblDeviceStatus1_64.AutoSize = True
        Me.lblDeviceStatus1_64.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_64.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_64.Location = New System.Drawing.Point(865, 7)
        Me.lblDeviceStatus1_64.Name = "lblDeviceStatus1_64"
        Me.lblDeviceStatus1_64.Size = New System.Drawing.Size(479, 47)
        Me.lblDeviceStatus1_64.TabIndex = 47
        Me.lblDeviceStatus1_64.Text = "入出金口紙幣受取待ち"
        '
        'lblDeviceStatus1_32
        '
        Me.lblDeviceStatus1_32.AutoSize = True
        Me.lblDeviceStatus1_32.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_32.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_32.Location = New System.Drawing.Point(604, 54)
        Me.lblDeviceStatus1_32.Name = "lblDeviceStatus1_32"
        Me.lblDeviceStatus1_32.Size = New System.Drawing.Size(255, 47)
        Me.lblDeviceStatus1_32.TabIndex = 46
        Me.lblDeviceStatus1_32.Text = "出金動作中"
        '
        'lblDeviceStatus1_16
        '
        Me.lblDeviceStatus1_16.AutoSize = True
        Me.lblDeviceStatus1_16.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_16.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_16.Location = New System.Drawing.Point(604, 7)
        Me.lblDeviceStatus1_16.Name = "lblDeviceStatus1_16"
        Me.lblDeviceStatus1_16.Size = New System.Drawing.Size(255, 47)
        Me.lblDeviceStatus1_16.TabIndex = 45
        Me.lblDeviceStatus1_16.Text = "入金動作中"
        '
        'lblDeviceStatus1_8
        '
        Me.lblDeviceStatus1_8.AutoSize = True
        Me.lblDeviceStatus1_8.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_8.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_8.Location = New System.Drawing.Point(304, 54)
        Me.lblDeviceStatus1_8.Name = "lblDeviceStatus1_8"
        Me.lblDeviceStatus1_8.Size = New System.Drawing.Size(291, 47)
        Me.lblDeviceStatus1_8.TabIndex = 44
        Me.lblDeviceStatus1_8.Text = "紙幣挿入待ち"
        '
        'lblDeviceStatus1_0
        '
        Me.lblDeviceStatus1_0.AutoSize = True
        Me.lblDeviceStatus1_0.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_0.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_0.Location = New System.Drawing.Point(1577, 7)
        Me.lblDeviceStatus1_0.Name = "lblDeviceStatus1_0"
        Me.lblDeviceStatus1_0.Size = New System.Drawing.Size(206, 47)
        Me.lblDeviceStatus1_0.TabIndex = 43
        Me.lblDeviceStatus1_0.Text = "エラー無し"
        '
        'lblDeviceStatus1_4
        '
        Me.lblDeviceStatus1_4.AutoSize = True
        Me.lblDeviceStatus1_4.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_4.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_4.Location = New System.Drawing.Point(304, 7)
        Me.lblDeviceStatus1_4.Name = "lblDeviceStatus1_4"
        Me.lblDeviceStatus1_4.Size = New System.Drawing.Size(161, 47)
        Me.lblDeviceStatus1_4.TabIndex = 42
        Me.lblDeviceStatus1_4.Text = "待機中"
        '
        'lblDeviceStatus1_1
        '
        Me.lblDeviceStatus1_1.AutoSize = True
        Me.lblDeviceStatus1_1.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_1.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_1.Location = New System.Drawing.Point(11, 7)
        Me.lblDeviceStatus1_1.Name = "lblDeviceStatus1_1"
        Me.lblDeviceStatus1_1.Size = New System.Drawing.Size(203, 47)
        Me.lblDeviceStatus1_1.TabIndex = 36
        Me.lblDeviceStatus1_1.Text = "パワーオン"
        '
        'lblDeviceStatus1_2
        '
        Me.lblDeviceStatus1_2.AutoSize = True
        Me.lblDeviceStatus1_2.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus1_2.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus1_2.Location = New System.Drawing.Point(11, 54)
        Me.lblDeviceStatus1_2.Name = "lblDeviceStatus1_2"
        Me.lblDeviceStatus1_2.Size = New System.Drawing.Size(287, 47)
        Me.lblDeviceStatus1_2.TabIndex = 37
        Me.lblDeviceStatus1_2.Text = "リセット動作中"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_128)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_64)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_32)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_16)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_8)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_0)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_4)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_1)
        Me.Panel2.Controls.Add(Me.lblDeviceStatus2_2)
        Me.Panel2.Location = New System.Drawing.Point(51, 412)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1839, 110)
        Me.Panel2.TabIndex = 42
        '
        'lblDeviceStatus2_128
        '
        Me.lblDeviceStatus2_128.AutoSize = True
        Me.lblDeviceStatus2_128.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_128.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_128.Location = New System.Drawing.Point(1037, 54)
        Me.lblDeviceStatus2_128.Name = "lblDeviceStatus2_128"
        Me.lblDeviceStatus2_128.Size = New System.Drawing.Size(456, 47)
        Me.lblDeviceStatus2_128.TabIndex = 48
        Me.lblDeviceStatus2_128.Text = "自己診断モード動作中"
        '
        'lblDeviceStatus2_64
        '
        Me.lblDeviceStatus2_64.AutoSize = True
        Me.lblDeviceStatus2_64.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_64.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_64.Location = New System.Drawing.Point(1037, 7)
        Me.lblDeviceStatus2_64.Name = "lblDeviceStatus2_64"
        Me.lblDeviceStatus2_64.Size = New System.Drawing.Size(456, 47)
        Me.lblDeviceStatus2_64.TabIndex = 47
        Me.lblDeviceStatus2_64.Text = "自己診断モード待機中"
        '
        'lblDeviceStatus2_32
        '
        Me.lblDeviceStatus2_32.AutoSize = True
        Me.lblDeviceStatus2_32.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_32.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_32.Location = New System.Drawing.Point(624, 54)
        Me.lblDeviceStatus2_32.Name = "lblDeviceStatus2_32"
        Me.lblDeviceStatus2_32.Size = New System.Drawing.Size(415, 47)
        Me.lblDeviceStatus2_32.TabIndex = 46
        Me.lblDeviceStatus2_32.Text = "計数リジェクト動作中"
        '
        'lblDeviceStatus2_16
        '
        Me.lblDeviceStatus2_16.AutoSize = True
        Me.lblDeviceStatus2_16.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_16.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_16.Location = New System.Drawing.Point(624, 7)
        Me.lblDeviceStatus2_16.Name = "lblDeviceStatus2_16"
        Me.lblDeviceStatus2_16.Size = New System.Drawing.Size(349, 47)
        Me.lblDeviceStatus2_16.TabIndex = 45
        Me.lblDeviceStatus2_16.Text = "計数払出動作中"
        '
        'lblDeviceStatus2_8
        '
        Me.lblDeviceStatus2_8.AutoSize = True
        Me.lblDeviceStatus2_8.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_8.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_8.Location = New System.Drawing.Point(359, 54)
        Me.lblDeviceStatus2_8.Name = "lblDeviceStatus2_8"
        Me.lblDeviceStatus2_8.Size = New System.Drawing.Size(255, 47)
        Me.lblDeviceStatus2_8.TabIndex = 44
        Me.lblDeviceStatus2_8.Text = "計数待機中"
        '
        'lblDeviceStatus2_0
        '
        Me.lblDeviceStatus2_0.AutoSize = True
        Me.lblDeviceStatus2_0.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_0.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_0.Location = New System.Drawing.Point(1577, 7)
        Me.lblDeviceStatus2_0.Name = "lblDeviceStatus2_0"
        Me.lblDeviceStatus2_0.Size = New System.Drawing.Size(206, 47)
        Me.lblDeviceStatus2_0.TabIndex = 43
        Me.lblDeviceStatus2_0.Text = "エラー無し"
        '
        'lblDeviceStatus2_4
        '
        Me.lblDeviceStatus2_4.AutoSize = True
        Me.lblDeviceStatus2_4.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_4.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_4.Location = New System.Drawing.Point(359, 7)
        Me.lblDeviceStatus2_4.Name = "lblDeviceStatus2_4"
        Me.lblDeviceStatus2_4.Size = New System.Drawing.Size(255, 47)
        Me.lblDeviceStatus2_4.TabIndex = 42
        Me.lblDeviceStatus2_4.Text = "計数動作中"
        '
        'lblDeviceStatus2_1
        '
        Me.lblDeviceStatus2_1.AutoSize = True
        Me.lblDeviceStatus2_1.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_1.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_1.Location = New System.Drawing.Point(11, 7)
        Me.lblDeviceStatus2_1.Name = "lblDeviceStatus2_1"
        Me.lblDeviceStatus2_1.Size = New System.Drawing.Size(304, 47)
        Me.lblDeviceStatus2_1.TabIndex = 36
        Me.lblDeviceStatus2_1.Text = "アラーム発生中"
        '
        'lblDeviceStatus2_2
        '
        Me.lblDeviceStatus2_2.AutoSize = True
        Me.lblDeviceStatus2_2.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDeviceStatus2_2.ForeColor = System.Drawing.Color.Silver
        Me.lblDeviceStatus2_2.Location = New System.Drawing.Point(11, 54)
        Me.lblDeviceStatus2_2.Name = "lblDeviceStatus2_2"
        Me.lblDeviceStatus2_2.Size = New System.Drawing.Size(349, 47)
        Me.lblDeviceStatus2_2.TabIndex = 37
        Me.lblDeviceStatus2_2.Text = "払出停止動作中"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.lblBillState1_0)
        Me.Panel4.Controls.Add(Me.lblBillState1_16)
        Me.Panel4.Controls.Add(Me.lblBillState1_1)
        Me.Panel4.Controls.Add(Me.lblBillState1_8)
        Me.Panel4.Location = New System.Drawing.Point(51, 527)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1839, 71)
        Me.Panel4.TabIndex = 43
        '
        'lblBillState1_0
        '
        Me.lblBillState1_0.AutoSize = True
        Me.lblBillState1_0.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState1_0.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState1_0.Location = New System.Drawing.Point(1577, 9)
        Me.lblBillState1_0.Name = "lblBillState1_0"
        Me.lblBillState1_0.Size = New System.Drawing.Size(206, 47)
        Me.lblBillState1_0.TabIndex = 43
        Me.lblBillState1_0.Text = "エラー無し"
        '
        'lblBillState1_16
        '
        Me.lblBillState1_16.AutoSize = True
        Me.lblBillState1_16.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState1_16.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState1_16.Location = New System.Drawing.Point(564, 9)
        Me.lblBillState1_16.Name = "lblBillState1_16"
        Me.lblBillState1_16.Size = New System.Drawing.Size(224, 47)
        Me.lblBillState1_16.TabIndex = 42
        Me.lblBillState1_16.Text = "お釣り切れ"
        '
        'lblBillState1_1
        '
        Me.lblBillState1_1.AutoSize = True
        Me.lblBillState1_1.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState1_1.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState1_1.Location = New System.Drawing.Point(11, 9)
        Me.lblBillState1_1.Name = "lblBillState1_1"
        Me.lblBillState1_1.Size = New System.Drawing.Size(255, 47)
        Me.lblBillState1_1.TabIndex = 36
        Me.lblBillState1_1.Text = "千円札満杯"
        '
        'lblBillState1_8
        '
        Me.lblBillState1_8.AutoSize = True
        Me.lblBillState1_8.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState1_8.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState1_8.Location = New System.Drawing.Point(290, 9)
        Me.lblBillState1_8.Name = "lblBillState1_8"
        Me.lblBillState1_8.Size = New System.Drawing.Size(255, 47)
        Me.lblBillState1_8.TabIndex = 37
        Me.lblBillState1_8.Text = "混合札満杯"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.lblBillState2_128)
        Me.Panel5.Controls.Add(Me.lblBillState2_0)
        Me.Panel5.Controls.Add(Me.lblBillState2_4)
        Me.Panel5.Controls.Add(Me.lblBillState2_1)
        Me.Panel5.Controls.Add(Me.lblBillState2_2)
        Me.Panel5.Location = New System.Drawing.Point(51, 604)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1839, 71)
        Me.Panel5.TabIndex = 44
        '
        'lblBillState2_128
        '
        Me.lblBillState2_128.AutoSize = True
        Me.lblBillState2_128.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState2_128.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState2_128.Location = New System.Drawing.Point(1134, 9)
        Me.lblBillState2_128.Name = "lblBillState2_128"
        Me.lblBillState2_128.Size = New System.Drawing.Size(268, 47)
        Me.lblBillState2_128.TabIndex = 44
        Me.lblBillState2_128.Text = "模擬券モード"
        '
        'lblBillState2_0
        '
        Me.lblBillState2_0.AutoSize = True
        Me.lblBillState2_0.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState2_0.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState2_0.Location = New System.Drawing.Point(1577, 9)
        Me.lblBillState2_0.Name = "lblBillState2_0"
        Me.lblBillState2_0.Size = New System.Drawing.Size(206, 47)
        Me.lblBillState2_0.TabIndex = 43
        Me.lblBillState2_0.Text = "エラー無し"
        '
        'lblBillState2_4
        '
        Me.lblBillState2_4.AutoSize = True
        Me.lblBillState2_4.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState2_4.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState2_4.Location = New System.Drawing.Point(718, 9)
        Me.lblBillState2_4.Name = "lblBillState2_4"
        Me.lblBillState2_4.Size = New System.Drawing.Size(410, 47)
        Me.lblBillState2_4.TabIndex = 42
        Me.lblBillState2_4.Text = "ユニット引き出し検出"
        '
        'lblBillState2_1
        '
        Me.lblBillState2_1.AutoSize = True
        Me.lblBillState2_1.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState2_1.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState2_1.Location = New System.Drawing.Point(11, 9)
        Me.lblBillState2_1.Name = "lblBillState2_1"
        Me.lblBillState2_1.Size = New System.Drawing.Size(502, 47)
        Me.lblBillState2_1.TabIndex = 36
        Me.lblBillState2_1.Text = "リジェクトボックス満杯検出"
        '
        'lblBillState2_2
        '
        Me.lblBillState2_2.AutoSize = True
        Me.lblBillState2_2.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBillState2_2.ForeColor = System.Drawing.Color.Silver
        Me.lblBillState2_2.Location = New System.Drawing.Point(516, 9)
        Me.lblBillState2_2.Name = "lblBillState2_2"
        Me.lblBillState2_2.Size = New System.Drawing.Size(208, 47)
        Me.lblBillState2_2.TabIndex = 37
        Me.lblBillState2_2.Text = "扉開検出"
        '
        'btnUpdInfo
        '
        Me.btnUpdInfo.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnUpdInfo.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnUpdInfo.ForeColor = System.Drawing.Color.White
        Me.btnUpdInfo.Location = New System.Drawing.Point(906, 30)
        Me.btnUpdInfo.Name = "btnUpdInfo"
        Me.btnUpdInfo.Size = New System.Drawing.Size(285, 116)
        Me.btnUpdInfo.TabIndex = 45
        Me.btnUpdInfo.Text = "情報更新"
        Me.btnUpdInfo.UseVisualStyleBackColor = False
        '
        'btnCleaning
        '
        Me.btnCleaning.BackColor = System.Drawing.Color.Salmon
        Me.btnCleaning.Font = New System.Drawing.Font("MS UI Gothic", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCleaning.ForeColor = System.Drawing.Color.White
        Me.btnCleaning.Location = New System.Drawing.Point(906, 152)
        Me.btnCleaning.Name = "btnCleaning"
        Me.btnCleaning.Size = New System.Drawing.Size(285, 116)
        Me.btnCleaning.TabIndex = 46
        Me.btnCleaning.Text = "クリーニング"
        Me.btnCleaning.UseVisualStyleBackColor = False
        Me.btnCleaning.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel7.Controls.Add(Me.lblSensorState2_8)
        Me.Panel7.Controls.Add(Me.lblSensorState2_4)
        Me.Panel7.Controls.Add(Me.lblSensorState2_7)
        Me.Panel7.Controls.Add(Me.lblSensorState2_3)
        Me.Panel7.Controls.Add(Me.lblSensorState2_6)
        Me.Panel7.Controls.Add(Me.lblSensorState2_0)
        Me.Panel7.Controls.Add(Me.lblSensorState2_2)
        Me.Panel7.Controls.Add(Me.lblSensorState2_1)
        Me.Panel7.Controls.Add(Me.lblSensorState2_5)
        Me.Panel7.Location = New System.Drawing.Point(51, 797)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1839, 110)
        Me.Panel7.TabIndex = 50
        '
        'lblSensorState2_8
        '
        Me.lblSensorState2_8.AutoSize = True
        Me.lblSensorState2_8.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_8.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_8.Location = New System.Drawing.Point(1037, 54)
        Me.lblSensorState2_8.Name = "lblSensorState2_8"
        Me.lblSensorState2_8.Size = New System.Drawing.Size(288, 47)
        Me.lblSensorState2_8.TabIndex = 48
        Me.lblSensorState2_8.Text = "一括ポジション"
        '
        'lblSensorState2_4
        '
        Me.lblSensorState2_4.AutoSize = True
        Me.lblSensorState2_4.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_4.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_4.Location = New System.Drawing.Point(1037, 7)
        Me.lblSensorState2_4.Name = "lblSensorState2_4"
        Me.lblSensorState2_4.Size = New System.Drawing.Size(408, 47)
        Me.lblSensorState2_4.TabIndex = 47
        Me.lblSensorState2_4.Text = "ﾘｼﾞｪｸﾄBOX・ｴﾝﾌﾟﾃｨ"
        '
        'lblSensorState2_7
        '
        Me.lblSensorState2_7.AutoSize = True
        Me.lblSensorState2_7.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_7.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_7.Location = New System.Drawing.Point(624, 54)
        Me.lblSensorState2_7.Name = "lblSensorState2_7"
        Me.lblSensorState2_7.Size = New System.Drawing.Size(255, 47)
        Me.lblSensorState2_7.TabIndex = 46
        Me.lblSensorState2_7.Text = "識別部内部"
        '
        'lblSensorState2_3
        '
        Me.lblSensorState2_3.AutoSize = True
        Me.lblSensorState2_3.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_3.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_3.Location = New System.Drawing.Point(624, 7)
        Me.lblSensorState2_3.Name = "lblSensorState2_3"
        Me.lblSensorState2_3.Size = New System.Drawing.Size(161, 47)
        Me.lblSensorState2_3.TabIndex = 45
        Me.lblSensorState2_3.Text = "払出口"
        '
        'lblSensorState2_6
        '
        Me.lblSensorState2_6.AutoSize = True
        Me.lblSensorState2_6.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_6.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_6.Location = New System.Drawing.Point(359, 54)
        Me.lblSensorState2_6.Name = "lblSensorState2_6"
        Me.lblSensorState2_6.Size = New System.Drawing.Size(159, 47)
        Me.lblSensorState2_6.TabIndex = 44
        Me.lblSensorState2_6.Text = "リフタ下"
        '
        'lblSensorState2_0
        '
        Me.lblSensorState2_0.AutoSize = True
        Me.lblSensorState2_0.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_0.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_0.Location = New System.Drawing.Point(1577, 7)
        Me.lblSensorState2_0.Name = "lblSensorState2_0"
        Me.lblSensorState2_0.Size = New System.Drawing.Size(206, 47)
        Me.lblSensorState2_0.TabIndex = 43
        Me.lblSensorState2_0.Text = "エラー無し"
        '
        'lblSensorState2_2
        '
        Me.lblSensorState2_2.AutoSize = True
        Me.lblSensorState2_2.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_2.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_2.Location = New System.Drawing.Point(359, 7)
        Me.lblSensorState2_2.Name = "lblSensorState2_2"
        Me.lblSensorState2_2.Size = New System.Drawing.Size(243, 47)
        Me.lblSensorState2_2.TabIndex = 42
        Me.lblSensorState2_2.Text = "スタッカ残留"
        '
        'lblSensorState2_1
        '
        Me.lblSensorState2_1.AutoSize = True
        Me.lblSensorState2_1.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_1.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_1.Location = New System.Drawing.Point(11, 7)
        Me.lblSensorState2_1.Name = "lblSensorState2_1"
        Me.lblSensorState2_1.Size = New System.Drawing.Size(216, 47)
        Me.lblSensorState2_1.TabIndex = 36
        Me.lblSensorState2_1.Text = "スタックイン"
        '
        'lblSensorState2_5
        '
        Me.lblSensorState2_5.AutoSize = True
        Me.lblSensorState2_5.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState2_5.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState2_5.Location = New System.Drawing.Point(11, 54)
        Me.lblSensorState2_5.Name = "lblSensorState2_5"
        Me.lblSensorState2_5.Size = New System.Drawing.Size(159, 47)
        Me.lblSensorState2_5.TabIndex = 37
        Me.lblSensorState2_5.Text = "リフタ上"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel6.Controls.Add(Me.lblSensorState1_8)
        Me.Panel6.Controls.Add(Me.lblSensorState1_4)
        Me.Panel6.Controls.Add(Me.lblSensorState1_7)
        Me.Panel6.Controls.Add(Me.lblSensorState1_3)
        Me.Panel6.Controls.Add(Me.lblSensorState1_6)
        Me.Panel6.Controls.Add(Me.lblSensorState1_0)
        Me.Panel6.Controls.Add(Me.lblSensorState1_2)
        Me.Panel6.Controls.Add(Me.lblSensorState1_1)
        Me.Panel6.Controls.Add(Me.lblSensorState1_5)
        Me.Panel6.Location = New System.Drawing.Point(51, 681)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1839, 110)
        Me.Panel6.TabIndex = 49
        '
        'lblSensorState1_8
        '
        Me.lblSensorState1_8.AutoSize = True
        Me.lblSensorState1_8.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_8.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_8.Location = New System.Drawing.Point(1037, 54)
        Me.lblSensorState1_8.Name = "lblSensorState1_8"
        Me.lblSensorState1_8.Size = New System.Drawing.Size(198, 47)
        Me.lblSensorState1_8.TabIndex = 48
        Me.lblSensorState1_8.Text = "プッシャ下"
        '
        'lblSensorState1_4
        '
        Me.lblSensorState1_4.AutoSize = True
        Me.lblSensorState1_4.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_4.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_4.Location = New System.Drawing.Point(1037, 7)
        Me.lblSensorState1_4.Name = "lblSensorState1_4"
        Me.lblSensorState1_4.Size = New System.Drawing.Size(302, 47)
        Me.lblSensorState1_4.TabIndex = 47
        Me.lblSensorState1_4.Text = "識別部入口左"
        '
        'lblSensorState1_7
        '
        Me.lblSensorState1_7.AutoSize = True
        Me.lblSensorState1_7.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_7.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_7.Location = New System.Drawing.Point(624, 54)
        Me.lblSensorState1_7.Name = "lblSensorState1_7"
        Me.lblSensorState1_7.Size = New System.Drawing.Size(198, 47)
        Me.lblSensorState1_7.TabIndex = 46
        Me.lblSensorState1_7.Text = "プッシャ上"
        '
        'lblSensorState1_3
        '
        Me.lblSensorState1_3.AutoSize = True
        Me.lblSensorState1_3.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_3.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_3.Location = New System.Drawing.Point(624, 7)
        Me.lblSensorState1_3.Name = "lblSensorState1_3"
        Me.lblSensorState1_3.Size = New System.Drawing.Size(302, 47)
        Me.lblSensorState1_3.TabIndex = 45
        Me.lblSensorState1_3.Text = "識別部入口右"
        '
        'lblSensorState1_6
        '
        Me.lblSensorState1_6.AutoSize = True
        Me.lblSensorState1_6.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_6.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_6.Location = New System.Drawing.Point(359, 54)
        Me.lblSensorState1_6.Name = "lblSensorState1_6"
        Me.lblSensorState1_6.Size = New System.Drawing.Size(161, 47)
        Me.lblSensorState1_6.TabIndex = 44
        Me.lblSensorState1_6.Text = "通過下"
        '
        'lblSensorState1_0
        '
        Me.lblSensorState1_0.AutoSize = True
        Me.lblSensorState1_0.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_0.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_0.Location = New System.Drawing.Point(1577, 7)
        Me.lblSensorState1_0.Name = "lblSensorState1_0"
        Me.lblSensorState1_0.Size = New System.Drawing.Size(206, 47)
        Me.lblSensorState1_0.TabIndex = 43
        Me.lblSensorState1_0.Text = "エラー無し"
        '
        'lblSensorState1_2
        '
        Me.lblSensorState1_2.AutoSize = True
        Me.lblSensorState1_2.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_2.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_2.Location = New System.Drawing.Point(359, 7)
        Me.lblSensorState1_2.Name = "lblSensorState1_2"
        Me.lblSensorState1_2.Size = New System.Drawing.Size(255, 47)
        Me.lblSensorState1_2.TabIndex = 42
        Me.lblSensorState1_2.Text = "一括入口左"
        '
        'lblSensorState1_1
        '
        Me.lblSensorState1_1.AutoSize = True
        Me.lblSensorState1_1.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_1.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_1.Location = New System.Drawing.Point(11, 7)
        Me.lblSensorState1_1.Name = "lblSensorState1_1"
        Me.lblSensorState1_1.Size = New System.Drawing.Size(255, 47)
        Me.lblSensorState1_1.TabIndex = 36
        Me.lblSensorState1_1.Text = "一括入口右"
        '
        'lblSensorState1_5
        '
        Me.lblSensorState1_5.AutoSize = True
        Me.lblSensorState1_5.Font = New System.Drawing.Font("MS UI Gothic", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSensorState1_5.ForeColor = System.Drawing.Color.Silver
        Me.lblSensorState1_5.Location = New System.Drawing.Point(11, 54)
        Me.lblSensorState1_5.Name = "lblSensorState1_5"
        Me.lblSensorState1_5.Size = New System.Drawing.Size(161, 47)
        Me.lblSensorState1_5.TabIndex = 37
        Me.lblSensorState1_5.Text = "通過上"
        '
        'frmAD1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.btnCleaning)
        Me.Controls.Add(Me.btnUpdInfo)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmAD1"
        Me.Text = "frmAD1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblConnect As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblDeviceStatus1_0 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_4 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_1 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_2 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_8 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_16 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_32 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_64 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus1_128 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblDeviceStatus2_128 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_64 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_32 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_16 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_8 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_0 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_4 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_1 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceStatus2_2 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblBillState1_0 As System.Windows.Forms.Label
    Friend WithEvents lblBillState1_16 As System.Windows.Forms.Label
    Friend WithEvents lblBillState1_1 As System.Windows.Forms.Label
    Friend WithEvents lblBillState1_8 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblBillState2_0 As System.Windows.Forms.Label
    Friend WithEvents lblBillState2_4 As System.Windows.Forms.Label
    Friend WithEvents lblBillState2_1 As System.Windows.Forms.Label
    Friend WithEvents lblBillState2_2 As System.Windows.Forms.Label
    Friend WithEvents lblBillState2_128 As System.Windows.Forms.Label
    Friend WithEvents btnUpdInfo As System.Windows.Forms.Button
    Friend WithEvents btnCleaning As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblSensorState2_8 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_4 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_7 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_3 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_6 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_0 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_2 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_1 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState2_5 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblSensorState1_8 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_4 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_7 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_3 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_6 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_0 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_2 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_1 As System.Windows.Forms.Label
    Friend WithEvents lblSensorState1_5 As System.Windows.Forms.Label
End Class
