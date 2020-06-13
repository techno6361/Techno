<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmREQUESTCARD
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
        Me.components = New System.ComponentModel.Container()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblMSG = New System.Windows.Forms.Label()
        Me.timCardInit = New System.Windows.Forms.Timer(Me.components)
        Me.timRead = New System.Windows.Forms.Timer(Me.components)
        Me.timPreRead = New System.Windows.Forms.Timer(Me.components)
        Me.timRePair = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Gray
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(238, 147)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(235, 74)
        Me.btnCancel.TabIndex = 171
        Me.btnCancel.Text = "取消"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblMSG
        '
        Me.lblMSG.BackColor = System.Drawing.Color.White
        Me.lblMSG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMSG.Font = New System.Drawing.Font("MS UI Gothic", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG.ForeColor = System.Drawing.Color.Blue
        Me.lblMSG.Location = New System.Drawing.Point(24, 21)
        Me.lblMSG.Name = "lblMSG"
        Me.lblMSG.Size = New System.Drawing.Size(664, 76)
        Me.lblMSG.TabIndex = 172
        Me.lblMSG.Text = "カードを置いてください。"
        Me.lblMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'timRead
        '
        Me.timRead.Interval = 50
        '
        'timPreRead
        '
        '
        'timRePair
        '
        '
        'frmREQUESTCARD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.ClientSize = New System.Drawing.Size(719, 236)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblMSG)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmREQUESTCARD"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "カード要求メッセージ"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblMSG As System.Windows.Forms.Label
    Friend WithEvents timCardInit As System.Windows.Forms.Timer
    Friend WithEvents timRead As System.Windows.Forms.Timer
    Friend WithEvents timPreRead As System.Windows.Forms.Timer
    Friend WithEvents timRePair As System.Windows.Forms.Timer

End Class
