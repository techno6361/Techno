<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmREQUESTCARDEx
    Inherits GRAN31.UI.frmBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmREQUESTCARDEx))
        Me.timCardInit = New System.Windows.Forms.Timer(Me.components)
        Me.timRead = New System.Windows.Forms.Timer(Me.components)
        Me.timPreRead = New System.Windows.Forms.Timer(Me.components)
        Me.timCLEANING = New System.Windows.Forms.Timer(Me.components)
        Me.timRePair = New System.Windows.Forms.Timer(Me.components)
        Me.mTimRead = New System.ComponentModel.BackgroundWorker()
        Me.mEjectCard = New System.ComponentModel.BackgroundWorker()
        Me.mWriteCard = New System.ComponentModel.BackgroundWorker()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.lblMSG = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.PictureBox()
        Me.lblMSG2 = New System.Windows.Forms.Label()
        Me.timClose = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timRead
        '
        Me.timRead.Interval = 50
        '
        'mTimRead
        '
        '
        'mEjectCard
        '
        '
        'mWriteCard
        '
        '
        'picImage
        '
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.Location = New System.Drawing.Point(627, 396)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(512, 384)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picImage.TabIndex = 177
        Me.picImage.TabStop = False
        '
        'lblMSG
        '
        Me.lblMSG.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG.Font = New System.Drawing.Font("Microsoft Sans Serif", 39.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG.ForeColor = System.Drawing.Color.White
        Me.lblMSG.Location = New System.Drawing.Point(0, 250)
        Me.lblMSG.Name = "lblMSG"
        Me.lblMSG.Size = New System.Drawing.Size(1920, 68)
        Me.lblMSG.TabIndex = 176
        Me.lblMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(1443, 18)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(335, 100)
        Me.btnCancel.TabIndex = 175
        Me.btnCancel.TabStop = False
        '
        'lblMSG2
        '
        Me.lblMSG2.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG2.Font = New System.Drawing.Font("Microsoft Sans Serif", 39.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG2.ForeColor = System.Drawing.Color.PaleTurquoise
        Me.lblMSG2.Location = New System.Drawing.Point(0, 783)
        Me.lblMSG2.Name = "lblMSG2"
        Me.lblMSG2.Size = New System.Drawing.Size(1920, 68)
        Me.lblMSG2.TabIndex = 178
        Me.lblMSG2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'timClose
        '
        Me.timClose.Interval = 30000
        '
        'frmREQUESTCARDEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.lblMSG2)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.lblMSG)
        Me.Controls.Add(Me.btnCancel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmREQUESTCARDEx"
        Me.Text = "frmREQUESTCARDBase"
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timCardInit As System.Windows.Forms.Timer
    Friend WithEvents timRead As System.Windows.Forms.Timer
    Friend WithEvents timPreRead As System.Windows.Forms.Timer
    Friend WithEvents timCLEANING As System.Windows.Forms.Timer
    Friend WithEvents timRePair As System.Windows.Forms.Timer
    Friend WithEvents btnCancel As System.Windows.Forms.PictureBox
    Friend WithEvents lblMSG As System.Windows.Forms.Label
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents mTimRead As System.ComponentModel.BackgroundWorker
    Friend WithEvents mEjectCard As System.ComponentModel.BackgroundWorker
    Friend WithEvents mWriteCard As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblMSG2 As System.Windows.Forms.Label
    Friend WithEvents timClose As System.Windows.Forms.Timer
End Class
