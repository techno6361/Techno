﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEJECTCARD_Restore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEJECTCARD_Restore))
        Me.lblMSG = New System.Windows.Forms.Label()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.mCreateCustmer = New System.ComponentModel.BackgroundWorker()
        Me.mRestoreMode = New System.ComponentModel.BackgroundWorker()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMSG
        '
        Me.lblMSG.BackColor = System.Drawing.Color.Transparent
        Me.lblMSG.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMSG.ForeColor = System.Drawing.Color.White
        Me.lblMSG.Location = New System.Drawing.Point(0, 184)
        Me.lblMSG.Name = "lblMSG"
        Me.lblMSG.Size = New System.Drawing.Size(1274, 57)
        Me.lblMSG.TabIndex = 0
        Me.lblMSG.Text = "処理中…しばらくお待ちください。"
        Me.lblMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picImage
        '
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.Location = New System.Drawing.Point(444, 321)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(449, 320)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picImage.TabIndex = 1
        Me.picImage.TabStop = False
        '
        'mCreateCustmer
        '
        '
        'frmEJECTCARD_Restore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1280, 800)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.lblMSG)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmEJECTCARD_Restore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMSG As System.Windows.Forms.Label
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents mCreateCustmer As System.ComponentModel.BackgroundWorker
    Friend WithEvents mRestoreMode As System.ComponentModel.BackgroundWorker
End Class
