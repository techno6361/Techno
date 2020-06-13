<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMULTIDISP01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMULTIDISP01))
        Me.tmTelop = New System.Windows.Forms.Timer(Me.components)
        Me.tmScroll = New System.Windows.Forms.Timer(Me.components)
        Me.lblTelop = New System.Windows.Forms.Label()
        Me.tmSeats = New System.Windows.Forms.Timer(Me.components)
        Me.pnlTELOP = New System.Windows.Forms.Panel()
        Me.bwTelop = New System.ComponentModel.BackgroundWorker()
        Me.bwSeats = New System.ComponentModel.BackgroundWorker()
        Me.bwMouse = New System.ComponentModel.BackgroundWorker()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.pnlTELOP.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmTelop
        '
        Me.tmTelop.Interval = 1000
        '
        'tmScroll
        '
        Me.tmScroll.Interval = 5
        '
        'lblTelop
        '
        Me.lblTelop.BackColor = System.Drawing.Color.Transparent
        Me.lblTelop.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTelop.ForeColor = System.Drawing.Color.White
        Me.lblTelop.Location = New System.Drawing.Point(0, 26)
        Me.lblTelop.Name = "lblTelop"
        Me.lblTelop.Size = New System.Drawing.Size(1920, 70)
        Me.lblTelop.TabIndex = 7
        Me.lblTelop.Text = "Label1"
        '
        'tmSeats
        '
        Me.tmSeats.Interval = 1000
        '
        'pnlTELOP
        '
        Me.pnlTELOP.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.pnlTELOP.Controls.Add(Me.lblTelop)
        Me.pnlTELOP.Location = New System.Drawing.Point(0, 0)
        Me.pnlTELOP.Name = "pnlTELOP"
        Me.pnlTELOP.Size = New System.Drawing.Size(1920, 120)
        Me.pnlTELOP.TabIndex = 8
        '
        'bwTelop
        '
        Me.bwTelop.WorkerReportsProgress = True
        '
        'bwSeats
        '
        Me.bwSeats.WorkerReportsProgress = True
        '
        'bwMouse
        '
        Me.bwMouse.WorkerReportsProgress = True
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1778, 155)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(42, 42)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 12
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(1778, 534)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(42, 42)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 11
        Me.PictureBox4.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(1599, 155)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(42, 42)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox7.TabIndex = 16
        Me.PictureBox7.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(1599, 534)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(42, 42)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox8.TabIndex = 15
        Me.PictureBox8.TabStop = False
        '
        'frmMULTIDISP01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1919, 1079)
        Me.Controls.Add(Me.PictureBox7)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.pnlTELOP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMULTIDISP01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Form1"
        Me.pnlTELOP.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmTelop As System.Windows.Forms.Timer
    Friend WithEvents tmScroll As System.Windows.Forms.Timer
    Friend WithEvents lblTelop As System.Windows.Forms.Label
    Friend WithEvents tmSeats As System.Windows.Forms.Timer
    Friend WithEvents pnlTELOP As System.Windows.Forms.Panel
    Friend WithEvents bwTelop As System.ComponentModel.BackgroundWorker
    Friend WithEvents bwSeats As System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMouse As System.ComponentModel.BackgroundWorker
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox

End Class
