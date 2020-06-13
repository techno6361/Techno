Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmCHARGEResult02

#Region "▼宣言部"

    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New TECHNO.DeviceControls.ICR700Control
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New Techno.DeviceControls.MCH3000Control
    ''' <summary>
    ''' 画像ファイルの定義
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RECEIPT_BUTTON_A_IMAGE As String = "D:\GRAN31\IMAGE\AUTOFRONT\btn_receipt_01.png"
    Private Const RECEIPT_BUTTON_B_IMAGE As String = "D:\GRAN31\IMAGE\AUTOFRONT\btn_receipt_02.png"

#End Region

#Region "▼プロパティ"


    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ICR700 As Techno.DeviceControls.ICR700Control
        Set(ByVal value As Techno.DeviceControls.ICR700Control)
            _dcICR700 = value
        End Set
    End Property
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MCH3000 As TECHNO.DeviceControls.MCH3000Control
        Set(ByVal value As TECHNO.DeviceControls.MCH3000Control)
            _dcMCH3000 = value
        End Set
    End Property

    ''' <summary>
    ''' エラー発生
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Err As Boolean
        Get
            Return _blnErr
        End Get
    End Property
    Private _blnErr As Boolean = False

    Public Property NKNKN As Integer = 0        ' 入金額
    Public Property DEPOSIT As Integer = 0      ' 投入額
    Public Property PREMKN As Integer = 0       ' プレミアム
    Public Property POINT As Integer = 0        ' ポイント
    Public Property RECEIPT As Boolean = False  ' 領収書フラグ
    Public Property DENNO As Integer = 0        ' 伝票番号
    Public Property USER_INFO As UserInfo = New UserInfo ' ユーザー情報

#End Region

#Region "▼ コンストラクタ"

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        ' すべてのコントロールにダブルバッファリングを有効化
        For Each c As Control In GetAllControls(Me)
            EnableDoubleBuffering(c)
        Next

    End Sub

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCHARGE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCHARGE01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.Refresh()

            ' 終了タイマー
            Me.timClose.Interval = CommonSettings.ChargeResultCloseInterval
            Me.timClose.Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' チェックイン/チェックアウト
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheckInOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckInOut.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            If Me.USER_INFO.IsCheckIn Then
                Me.vbDialogResult = eResult.CHECKIN
            Else
                Me.vbDialogResult = eResult.CHECKIN
            End If
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード排出_OKボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btnOK.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_音声１
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timSound_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timSound.Tick
        Try
            ' 音声_お取り忘れないようご注意ください。
            Sound.PlayForgetAttention()

            Me.timSound2.Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.timSound.Stop()
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_音声２
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timSound2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timSound2.Tick
        Try
            Sound.PlayThank()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.timSound2.Stop()
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timClose_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timClose.Tick
        Try
            Me.timClose.Stop()
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_点滅テキスト
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timBlinkText_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBlinkText.Tick
        Try
            Me.lblBlinkText.Visible = Not Me.lblBlinkText.Visible
        Catch ex As Exception
            Throw ex
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
            '顧客番号
            Me.lblNCSNO.Text = USER_INFO.NCSNO
            '氏名
            Me.lblCCSNAME.Text = USER_INFO.CCSNAME

            ' カード残金とポイントの表示
            Me.lblZANKN.Text = Me.USER_INFO.ZANKN.ToString("N0") & "円"
            Me.lblSRTPO.Text = Me.USER_INFO.SRTPO.ToString("N0") & "Ｐ"
            'If CType(Me.USER_INFO.SRTPO, Integer).Equals(0) Then
            '    Me.Label5.Visible = False
            '    Me.lblSRTPO.Visible = False
            'End If

            Me.lblPrice.Text = Me.NKNKN.ToString("N0") ' 購入金額
            Me.lblDesipot.Text = Me.DEPOSIT.ToString("N0") '投入金額

            If Me.USER_INFO.IsCheckIn Then
                Me.btnCheckInOut.Image = Images.BtnCheckOut
            Else
                Me.btnCheckInOut.Image = Images.BtnCheckIn
            End If

            If CommonSettings.IsChargeResultEjectStopMode Then

                ' ****【排出停止モードON】****

                If Me.USER_INFO.IsCheckIn Then
                    ' GRAN31ではチェックアウトボタンを無効にする
                    Me.btnCheckInOut.Image = Images.BtnCheckIn
                    frmBase.ChangePictureBoxEnabled(Me.btnCheckInOut, True)
                Else
                    frmBase.ChangePictureBoxEnabled(Me.btnCheckInOut, True)
                End If

                Me.btnCancel.Image = Images.BtnEjectCard

                Me.lblMSG2.ForeColor = Color.Yellow

                If Me.RECEIPT Then
                    '【前の画面で領収書発行ON】
                    Me.lblBlinkText.Text = "領収書をお受け取りください。"
                    If Me.USER_INFO.IsCheckIn Then
                        Me.lblMSG2.Text = "またのご利用をお待ちしております。"
                    Else
                        Me.lblMSG2.Text = "チェックインする方はボタンにタッチしてください。"
                    End If
                    Me.picImage.Image = Images.IconChargeResultCardAndReceipt

                    ' 領収書をお取りください。
                    Me.timReceipt.Interval = 2000
                    Me.timReceipt.Start()

                    ' ありがとうございました
                    Me.timSound2.Interval = 4000
                    Me.timSound2.Start()


                Else
                    '【前の画面で領収書発行OFF】
                    Me.lblBlinkText.Text = "ご利用ありがとうございました。"
                    If Me.USER_INFO.IsCheckIn Then
                        Me.lblMSG2.Text = "またのご利用をお待ちしております。"
                    Else
                        Me.lblMSG2.Text = "チェックインする方はボタンにタッチしてください。"
                    End If
                    Me.picImage.Image = Nothing

                    ' ご希望のボタンにタッチしてください。
                    Sound.PlayTouchAnyButton()

                    ' ありがとうございました
                    Me.timSound2.Interval = 4000
                    Me.timSound2.Start()

                    ' 点滅テキスト２
                    Me.timBlinkText2.Interval = 2000
                    Me.timBlinkText2.Start()


                End If

            Else

                ' ****【排出停止モードOFF】****

                Me.btnCheckInOut.Visible = False
                Me.btnCancel.Visible = False
                frmBase.ChangePictureBoxEnabled(Me.btnCheckInOut, False)
                frmBase.ChangePictureBoxEnabled(Me.btnCancel, False)
                Me.btnOK.Visible = True

                If Me.RECEIPT Then
                    Me.lblBlinkText.Text = "領収書をお受け取りください。"
                    Me.lblBlinkText.ForeColor = Color.Yellow
                    Me.picImage.Image = Images.IconChargeResultCardAndReceipt
                    Me.timReceipt.Interval = 2000
                    Me.timReceipt.Start() ' 領収書をお取りください。

                    ' ありがとうございました
                    Me.timSound2.Interval = 4000
                    Me.timSound2.Start()

                Else
                    Me.lblBlinkText.Text = "領収書が必要な方は領収書ボタンをタッチしてください。"
                    Me.lblBlinkText.ForeColor = Color.Yellow
                    Me.picImage.Image = Images.IconChargeResultCardOnly

                    ' ありがとうございました
                    Me.timSound2.Interval = 1000
                    'Me.timSound2.Interval = 2500
                    Me.timSound2.Start()
                End If

                ' 点滅テキスト
                Me.timBlinkText.Interval = 1000
                Me.timBlinkText.Start()

            End If

            Me.mCharge.WorkerReportsProgress = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

End Class
