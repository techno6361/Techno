<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMSGBOX01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSGBOX01))
        Me.lblMSG = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.picQuestion = New System.Windows.Forms.PictureBox()
        Me.picError = New System.Windows.Forms.PictureBox()
        Me.picExclamation = New System.Windows.Forms.PictureBox()
        Me.picInfomation = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.picQuestion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picError, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picExclamation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picInfomation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMSG
        '
        Me.lblMSG.BackColor = System.Drawing.Color.White
        Me.lblMSG.Font = New System.Drawing.Font("MS UI Gothic", 27.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG.Location = New System.Drawing.Point(147, 25)
        Me.lblMSG.Name = "lblMSG"
        Me.lblMSG.Size = New System.Drawing.Size(778, 142)
        Me.lblMSG.TabIndex = 0
        Me.lblMSG.Text = "他端末からの更新があったため、更新に失敗しました。"
        Me.lblMSG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.SeaGreen
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(453, 259)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(224, 90)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.picQuestion)
        Me.Panel1.Controls.Add(Me.picError)
        Me.Panel1.Controls.Add(Me.picExclamation)
        Me.Panel1.Controls.Add(Me.picInfomation)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Controls.Add(Me.lblMSG)
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(957, 402)
        Me.Panel1.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Silver
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(701, 259)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(224, 90)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = False
        Me.btnCancel.Visible = False
        '
        'picQuestion
        '
        Me.picQuestion.BackColor = System.Drawing.Color.White
        Me.picQuestion.Image = CType(resources.GetObject("picQuestion.Image"), System.Drawing.Image)
        Me.picQuestion.Location = New System.Drawing.Point(50, 53)
        Me.picQuestion.Name = "picQuestion"
        Me.picQuestion.Size = New System.Drawing.Size(91, 80)
        Me.picQuestion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picQuestion.TabIndex = 6
        Me.picQuestion.TabStop = False
        Me.picQuestion.Visible = False
        '
        'picError
        '
        Me.picError.BackColor = System.Drawing.Color.White
        Me.picError.Image = CType(resources.GetObject("picError.Image"), System.Drawing.Image)
        Me.picError.Location = New System.Drawing.Point(50, 53)
        Me.picError.Name = "picError"
        Me.picError.Size = New System.Drawing.Size(91, 80)
        Me.picError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picError.TabIndex = 5
        Me.picError.TabStop = False
        Me.picError.Visible = False
        '
        'picExclamation
        '
        Me.picExclamation.BackColor = System.Drawing.Color.White
        Me.picExclamation.Image = CType(resources.GetObject("picExclamation.Image"), System.Drawing.Image)
        Me.picExclamation.Location = New System.Drawing.Point(50, 53)
        Me.picExclamation.Name = "picExclamation"
        Me.picExclamation.Size = New System.Drawing.Size(91, 80)
        Me.picExclamation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picExclamation.TabIndex = 4
        Me.picExclamation.TabStop = False
        Me.picExclamation.Visible = False
        '
        'picInfomation
        '
        Me.picInfomation.BackColor = System.Drawing.Color.White
        Me.picInfomation.Image = CType(resources.GetObject("picInfomation.Image"), System.Drawing.Image)
        Me.picInfomation.Location = New System.Drawing.Point(50, 53)
        Me.picInfomation.Name = "picInfomation"
        Me.picInfomation.Size = New System.Drawing.Size(91, 80)
        Me.picInfomation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picInfomation.TabIndex = 3
        Me.picInfomation.TabStop = False
        Me.picInfomation.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 142)
        Me.Label1.TabIndex = 2
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'frmMSGBOX01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Tan
        Me.ClientSize = New System.Drawing.Size(966, 411)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSGBOX01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        CType(Me.picQuestion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picError, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picExclamation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picInfomation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMSG As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents picInfomation As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picQuestion As System.Windows.Forms.PictureBox
    Friend WithEvents picError As System.Windows.Forms.PictureBox
    Friend WithEvents picExclamation As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
