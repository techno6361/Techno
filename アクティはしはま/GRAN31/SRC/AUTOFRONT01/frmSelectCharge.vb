Public Class frmSelectCharge

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"


    ''' <summary>
    ''' 指定金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SITEI As Integer
        Get
            Return _intSITEI
        End Get
    End Property
    Private _intSITEI As Integer

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSelectCharge_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 決定ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Try
            If String.IsNullOrEmpty(Me.lblShitei.Text) Then Exit Sub

            _intSITEI = CType(Me.lblShitei.Text, Integer)

            If _intSITEI > 20000 Then
                Using frm As New frmMSGBOX01("20,000円を超える入金はできません。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If _intSITEI < UIUtility.SYSTEM.CARDFEE Then
                Using frm As New frmMSGBOX01("カード発行料を超える金額を指定してください。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 閉じるボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSelectSitei_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectSitei.Click
        Try
            _intSITEI = 0

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 数字ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Click(sender As System.Object, e As System.EventArgs) Handles btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn0.Click
        Try
            Dim btn As Control
            btn = CType(sender, Control)

            If Me.lblShitei.Text.Length > 1 And Me.lblShitei.Text.Length < 5 Then
                Me.lblShitei.Text = Me.lblShitei.Text.Substring(0, Me.lblShitei.Text.Length - 1)
            End If

            If Me.lblShitei.Text.Length < 5 Then
                Me.lblShitei.Text &= btn.Text & "0"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnC_Click(sender As System.Object, e As System.EventArgs) Handles btnC.Click
        Try
            Me.lblShitei.Text = String.Empty
            _intSITEI = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            Me.lblShitei.Text = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region





End Class