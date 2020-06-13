<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StatusPanel
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
        Me.pnlPrintStatus = New System.Windows.Forms.Panel()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlPrintStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPrintStatus
        '
        Me.pnlPrintStatus.BackColor = System.Drawing.Color.White
        Me.pnlPrintStatus.Controls.Add(Me.lblCount)
        Me.pnlPrintStatus.Controls.Add(Me.lblTitle)
        Me.pnlPrintStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPrintStatus.Location = New System.Drawing.Point(0, 0)
        Me.pnlPrintStatus.Name = "pnlPrintStatus"
        Me.pnlPrintStatus.Padding = New System.Windows.Forms.Padding(0, 20, 0, 20)
        Me.pnlPrintStatus.Size = New System.Drawing.Size(949, 195)
        Me.pnlPrintStatus.TabIndex = 1100
        '
        'lblCount
        '
        Me.lblCount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblCount.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCount.ForeColor = System.Drawing.Color.Red
        Me.lblCount.Location = New System.Drawing.Point(0, 117)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(949, 58)
        Me.lblCount.TabIndex = 1100
        Me.lblCount.Text = "(0/0)"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("MS UI Gothic", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Red
        Me.lblTitle.Location = New System.Drawing.Point(0, 20)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(949, 65)
        Me.lblTitle.TabIndex = 1099
        Me.lblTitle.Text = "処理中…しばらくお待ちください。"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StatusPanel
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.pnlPrintStatus)
        Me.Name = "StatusPanel"
        Me.Size = New System.Drawing.Size(949, 195)
        Me.pnlPrintStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlPrintStatus As System.Windows.Forms.Panel
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label

End Class
