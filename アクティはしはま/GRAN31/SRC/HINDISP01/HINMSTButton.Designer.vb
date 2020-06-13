<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HINMSTButton
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.lblURIATK = New System.Windows.Forms.Label()
        Me.lblHINNMA = New System.Windows.Forms.Label()
        Me.btnHINNMA = New System.Windows.Forms.Button()
        Me.lblPOINT = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblURIATK
        '
        Me.lblURIATK.BackColor = System.Drawing.Color.Orange
        Me.lblURIATK.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblURIATK.Location = New System.Drawing.Point(18, 34)
        Me.lblURIATK.Name = "lblURIATK"
        Me.lblURIATK.Size = New System.Drawing.Size(285, 23)
        Me.lblURIATK.TabIndex = 17
        Me.lblURIATK.Text = "1,000円"
        Me.lblURIATK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHINNMA
        '
        Me.lblHINNMA.BackColor = System.Drawing.Color.Orange
        Me.lblHINNMA.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHINNMA.Location = New System.Drawing.Point(18, 11)
        Me.lblHINNMA.Name = "lblHINNMA"
        Me.lblHINNMA.Size = New System.Drawing.Size(285, 23)
        Me.lblHINNMA.TabIndex = 15
        Me.lblHINNMA.Text = "あああああああああああああああ"
        Me.lblHINNMA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnHINNMA
        '
        Me.btnHINNMA.BackColor = System.Drawing.Color.Orange
        Me.btnHINNMA.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnHINNMA.ForeColor = System.Drawing.Color.Black
        Me.btnHINNMA.Location = New System.Drawing.Point(7, 3)
        Me.btnHINNMA.Name = "btnHINNMA"
        Me.btnHINNMA.Size = New System.Drawing.Size(308, 62)
        Me.btnHINNMA.TabIndex = 14
        Me.btnHINNMA.TabStop = False
        Me.btnHINNMA.UseVisualStyleBackColor = False
        '
        'lblPOINT
        '
        Me.lblPOINT.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPOINT.ForeColor = System.Drawing.Color.Maroon
        Me.lblPOINT.Location = New System.Drawing.Point(7, 65)
        Me.lblPOINT.Name = "lblPOINT"
        Me.lblPOINT.Size = New System.Drawing.Size(308, 20)
        Me.lblPOINT.TabIndex = 16
        Me.lblPOINT.Text = "(Point: 100)"
        Me.lblPOINT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HINMSTButton
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.FloralWhite
        Me.Controls.Add(Me.lblURIATK)
        Me.Controls.Add(Me.lblHINNMA)
        Me.Controls.Add(Me.btnHINNMA)
        Me.Controls.Add(Me.lblPOINT)
        Me.Name = "HINMSTButton"
        Me.Size = New System.Drawing.Size(320, 89)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblURIATK As System.Windows.Forms.Label
    Friend WithEvents lblHINNMA As System.Windows.Forms.Label
    Friend WithEvents btnHINNMA As System.Windows.Forms.Button
    Friend WithEvents lblPOINT As System.Windows.Forms.Label

End Class
