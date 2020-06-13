<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CntSeat01
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CntSeat01))
        Me.btnLamp = New System.Windows.Forms.PictureBox()
        Me.btnNo = New System.Windows.Forms.Label()
        Me.btnLR = New System.Windows.Forms.Label()
        Me.tmLamp = New System.Windows.Forms.Timer(Me.components)
        CType(Me.btnLamp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLamp
        '
        Me.btnLamp.BackColor = System.Drawing.Color.Transparent
        Me.btnLamp.Location = New System.Drawing.Point(0, 115)
        Me.btnLamp.Name = "btnLamp"
        Me.btnLamp.Size = New System.Drawing.Size(103, 95)
        Me.btnLamp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnLamp.TabIndex = 1
        Me.btnLamp.TabStop = False
        '
        'btnNo
        '
        Me.btnNo.Font = New System.Drawing.Font("MS UI Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnNo.ForeColor = System.Drawing.Color.DimGray
        Me.btnNo.Location = New System.Drawing.Point(3, 38)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(100, 74)
        Me.btnNo.TabIndex = 2
        Me.btnNo.Text = "01"
        Me.btnNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnLR
        '
        Me.btnLR.BackColor = System.Drawing.Color.Transparent
        Me.btnLR.Font = New System.Drawing.Font("FFF Planeta", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLR.ForeColor = System.Drawing.Color.DarkOrange
        Me.btnLR.Image = CType(resources.GetObject("btnLR.Image"), System.Drawing.Image)
        Me.btnLR.Location = New System.Drawing.Point(3, 0)
        Me.btnLR.Name = "btnLR"
        Me.btnLR.Size = New System.Drawing.Size(97, 48)
        Me.btnLR.TabIndex = 3
        Me.btnLR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tmLamp
        '
        Me.tmLamp.Interval = 1000
        '
        'cntSeat04
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.btnLR)
        Me.Controls.Add(Me.btnNo)
        Me.Controls.Add(Me.btnLamp)
        Me.Name = "cntSeat04"
        Me.Size = New System.Drawing.Size(103, 213)
        CType(Me.btnLamp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnLamp As System.Windows.Forms.PictureBox
    Friend WithEvents btnNo As System.Windows.Forms.Label
    Friend WithEvents btnLR As System.Windows.Forms.Label
    Friend WithEvents tmLamp As System.Windows.Forms.Timer

End Class
