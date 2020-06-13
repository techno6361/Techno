<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmColorSelect
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
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnColor4 = New System.Windows.Forms.Button()
        Me.btnColor3 = New System.Windows.Forms.Button()
        Me.btnColor2 = New System.Windows.Forms.Button()
        Me.btnColor1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkAll)
        Me.Panel1.Controls.Add(Me.btnEnd)
        Me.Panel1.Controls.Add(Me.btnColor4)
        Me.Panel1.Controls.Add(Me.btnColor3)
        Me.Panel1.Controls.Add(Me.btnColor2)
        Me.Panel1.Controls.Add(Me.btnColor1)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(822, 426)
        Me.Panel1.TabIndex = 0
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Font = New System.Drawing.Font("MS UI Gothic", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkAll.Location = New System.Drawing.Point(386, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(354, 38)
        Me.chkAll.TabIndex = 617
        Me.chkAll.Text = "全ての顧客情報に反映"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.DarkGray
        Me.btnEnd.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.ForeColor = System.Drawing.Color.White
        Me.btnEnd.Location = New System.Drawing.Point(518, 323)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(222, 75)
        Me.btnEnd.TabIndex = 616
        Me.btnEnd.TabStop = False
        Me.btnEnd.Text = "閉じる"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'btnColor4
        '
        Me.btnColor4.BackColor = System.Drawing.Color.White
        Me.btnColor4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnColor4.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColor4.ForeColor = System.Drawing.Color.White
        Me.btnColor4.Location = New System.Drawing.Point(590, 83)
        Me.btnColor4.Name = "btnColor4"
        Me.btnColor4.Size = New System.Drawing.Size(150, 214)
        Me.btnColor4.TabIndex = 615
        Me.btnColor4.TabStop = False
        Me.btnColor4.Tag = "4"
        Me.btnColor4.UseVisualStyleBackColor = False
        '
        'btnColor3
        '
        Me.btnColor3.BackColor = System.Drawing.Color.Red
        Me.btnColor3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnColor3.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColor3.ForeColor = System.Drawing.Color.White
        Me.btnColor3.Location = New System.Drawing.Point(419, 83)
        Me.btnColor3.Name = "btnColor3"
        Me.btnColor3.Size = New System.Drawing.Size(150, 214)
        Me.btnColor3.TabIndex = 614
        Me.btnColor3.TabStop = False
        Me.btnColor3.Tag = "3"
        Me.btnColor3.UseVisualStyleBackColor = False
        '
        'btnColor2
        '
        Me.btnColor2.BackColor = System.Drawing.Color.Yellow
        Me.btnColor2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnColor2.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColor2.ForeColor = System.Drawing.Color.White
        Me.btnColor2.Location = New System.Drawing.Point(248, 83)
        Me.btnColor2.Name = "btnColor2"
        Me.btnColor2.Size = New System.Drawing.Size(150, 214)
        Me.btnColor2.TabIndex = 613
        Me.btnColor2.TabStop = False
        Me.btnColor2.Tag = "2"
        Me.btnColor2.UseVisualStyleBackColor = False
        '
        'btnColor1
        '
        Me.btnColor1.BackColor = System.Drawing.Color.Green
        Me.btnColor1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnColor1.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnColor1.ForeColor = System.Drawing.Color.White
        Me.btnColor1.Location = New System.Drawing.Point(77, 83)
        Me.btnColor1.Name = "btnColor1"
        Me.btnColor1.Size = New System.Drawing.Size(150, 214)
        Me.btnColor1.TabIndex = 612
        Me.btnColor1.TabStop = False
        Me.btnColor1.Tag = "1"
        Me.btnColor1.UseVisualStyleBackColor = False
        '
        'frmColorSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkGreen
        Me.ClientSize = New System.Drawing.Size(846, 450)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmColorSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmColorSelect"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnColor3 As System.Windows.Forms.Button
    Friend WithEvents btnColor2 As System.Windows.Forms.Button
    Friend WithEvents btnColor1 As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnColor4 As System.Windows.Forms.Button
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
End Class
