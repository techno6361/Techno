<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWEATHER
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
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.btnAme = New System.Windows.Forms.Button()
        Me.btnKumori = New System.Windows.Forms.Button()
        Me.btnHare = New System.Windows.Forms.Button()
        Me.btnYuki = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Moccasin
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnYuki)
        Me.Panel1.Controls.Add(Me.btnEnd)
        Me.Panel1.Controls.Add(Me.btnAme)
        Me.Panel1.Controls.Add(Me.btnKumori)
        Me.Panel1.Controls.Add(Me.btnHare)
        Me.Panel1.Location = New System.Drawing.Point(12, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(355, 331)
        Me.Panel1.TabIndex = 0
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.Gray
        Me.btnEnd.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.ForeColor = System.Drawing.Color.White
        Me.btnEnd.Location = New System.Drawing.Point(201, 265)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(117, 51)
        Me.btnEnd.TabIndex = 3
        Me.btnEnd.Text = "閉じる"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'btnAme
        '
        Me.btnAme.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnAme.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAme.ForeColor = System.Drawing.Color.White
        Me.btnAme.Location = New System.Drawing.Point(34, 141)
        Me.btnAme.Name = "btnAme"
        Me.btnAme.Size = New System.Drawing.Size(284, 51)
        Me.btnAme.TabIndex = 2
        Me.btnAme.Text = "雨"
        Me.btnAme.UseVisualStyleBackColor = False
        '
        'btnKumori
        '
        Me.btnKumori.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnKumori.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKumori.ForeColor = System.Drawing.Color.White
        Me.btnKumori.Location = New System.Drawing.Point(34, 79)
        Me.btnKumori.Name = "btnKumori"
        Me.btnKumori.Size = New System.Drawing.Size(284, 51)
        Me.btnKumori.TabIndex = 1
        Me.btnKumori.Text = "曇り"
        Me.btnKumori.UseVisualStyleBackColor = False
        '
        'btnHare
        '
        Me.btnHare.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnHare.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnHare.ForeColor = System.Drawing.Color.White
        Me.btnHare.Location = New System.Drawing.Point(34, 18)
        Me.btnHare.Name = "btnHare"
        Me.btnHare.Size = New System.Drawing.Size(284, 51)
        Me.btnHare.TabIndex = 0
        Me.btnHare.Text = "晴れ"
        Me.btnHare.UseVisualStyleBackColor = False
        '
        'btnYuki
        '
        Me.btnYuki.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnYuki.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnYuki.ForeColor = System.Drawing.Color.White
        Me.btnYuki.Location = New System.Drawing.Point(34, 204)
        Me.btnYuki.Name = "btnYuki"
        Me.btnYuki.Size = New System.Drawing.Size(284, 51)
        Me.btnYuki.TabIndex = 4
        Me.btnYuki.Text = "雪"
        Me.btnYuki.UseVisualStyleBackColor = False
        '
        'frmWEATHER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(379, 362)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmWEATHER"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmWEATHER"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents btnAme As System.Windows.Forms.Button
    Friend WithEvents btnKumori As System.Windows.Forms.Button
    Friend WithEvents btnHare As System.Windows.Forms.Button
    Friend WithEvents btnYuki As System.Windows.Forms.Button
End Class
