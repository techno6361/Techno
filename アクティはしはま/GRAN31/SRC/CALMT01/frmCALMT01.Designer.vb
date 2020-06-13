<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCALMT01
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
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblNenGetu = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Font = New System.Drawing.Font("MS UI Gothic", 18!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(1167, 172)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(170, 55)
        Me.btnNext.TabIndex = 12
        Me.btnNext.Text = "次月"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(605, 172)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(170, 55)
        Me.btnBack.TabIndex = 13
        Me.btnBack.Text = "前月"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'lblNenGetu
        '
        Me.lblNenGetu.BackColor = System.Drawing.Color.DarkGreen
        Me.lblNenGetu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNenGetu.Font = New System.Drawing.Font("ＭＳ ゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNenGetu.ForeColor = System.Drawing.Color.White
        Me.lblNenGetu.Location = New System.Drawing.Point(783, 172)
        Me.lblNenGetu.Name = "lblNenGetu"
        Me.lblNenGetu.Size = New System.Drawing.Size(378, 55)
        Me.lblNenGetu.TabIndex = 11
        Me.lblNenGetu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmCALMT01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1904, 1042)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblNenGetu)
        Me.Name = "frmCALMT01"
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.lblNenGetu, 0)
        Me.Controls.SetChildIndex(Me.btnBack, 0)
        Me.Controls.SetChildIndex(Me.btnNext, 0)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblNenGetu As System.Windows.Forms.Label

End Class
