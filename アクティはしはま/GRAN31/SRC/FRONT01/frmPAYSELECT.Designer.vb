<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPAYSELECT
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPaySelect0 = New System.Windows.Forms.Button()
        Me.btnPaySelect2 = New System.Windows.Forms.Button()
        Me.btnPaySelect1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Moccasin
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnPaySelect0)
        Me.Panel1.Controls.Add(Me.btnPaySelect2)
        Me.Panel1.Controls.Add(Me.btnPaySelect1)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(789, 344)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(738, 83)
        Me.Label1.TabIndex = 174
        Me.Label1.Text = "精算方法を選択してください。"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnPaySelect0
        '
        Me.btnPaySelect0.BackColor = System.Drawing.Color.Silver
        Me.btnPaySelect0.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPaySelect0.ForeColor = System.Drawing.Color.White
        Me.btnPaySelect0.Location = New System.Drawing.Point(528, 161)
        Me.btnPaySelect0.Name = "btnPaySelect0"
        Me.btnPaySelect0.Size = New System.Drawing.Size(233, 150)
        Me.btnPaySelect0.TabIndex = 173
        Me.btnPaySelect0.Tag = "0"
        Me.btnPaySelect0.Text = "キャンセル"
        Me.btnPaySelect0.UseVisualStyleBackColor = False
        '
        'btnPaySelect2
        '
        Me.btnPaySelect2.BackColor = System.Drawing.Color.Green
        Me.btnPaySelect2.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPaySelect2.ForeColor = System.Drawing.Color.White
        Me.btnPaySelect2.Location = New System.Drawing.Point(277, 161)
        Me.btnPaySelect2.Name = "btnPaySelect2"
        Me.btnPaySelect2.Size = New System.Drawing.Size(233, 150)
        Me.btnPaySelect2.TabIndex = 172
        Me.btnPaySelect2.Tag = "2"
        Me.btnPaySelect2.Text = "打席カード"
        Me.btnPaySelect2.UseVisualStyleBackColor = False
        '
        'btnPaySelect1
        '
        Me.btnPaySelect1.BackColor = System.Drawing.Color.Blue
        Me.btnPaySelect1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPaySelect1.ForeColor = System.Drawing.Color.White
        Me.btnPaySelect1.Location = New System.Drawing.Point(23, 161)
        Me.btnPaySelect1.Name = "btnPaySelect1"
        Me.btnPaySelect1.Size = New System.Drawing.Size(233, 150)
        Me.btnPaySelect1.TabIndex = 171
        Me.btnPaySelect1.Tag = "1"
        Me.btnPaySelect1.Text = "現金"
        Me.btnPaySelect1.UseVisualStyleBackColor = False
        Me.btnPaySelect1.Visible = False
        '
        'frmPAYSELECT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.ClientSize = New System.Drawing.Size(813, 368)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPAYSELECT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPAYSELECT"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPaySelect0 As System.Windows.Forms.Button
    Friend WithEvents btnPaySelect2 As System.Windows.Forms.Button
    Friend WithEvents btnPaySelect1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
