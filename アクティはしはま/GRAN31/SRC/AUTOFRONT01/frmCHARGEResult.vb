Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmCHARGEResult

#Region "▼宣言部"

    ''' <summary>
    ''' レシートプリンタコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSK121 As New Techno.DeviceControls.SK121Control
    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New Techno.DeviceControls.ICR700Control
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New Techno.DeviceControls.MCH3000Control

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' レシートプリンター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property SK121 As Techno.DeviceControls.SK121Control
        Get
            Return _dcSK121
        End Get
        Set(ByVal value As Techno.DeviceControls.SK121Control)
            _dcSK121 = value
        End Set
    End Property
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
                Me.vbDialogResult = eResult.CHECKOUT
            Else
                Me.vbDialogResult = eResult.CHECKIN
            End If
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            If CommonSettings.IsChargeResultEjectStopMode Then
                EjectPreCard()
            End If
            Me.vbDialogResult = eResult.NONE
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
            If CommonSettings.IsChargeResultEjectStopMode Then
                EjectPreCard()
            End If
            Me.vbDialogResult = eResult.NONE
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

    ''' <summary>
    ''' タイマー_点滅テキスト２
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timBlinkText2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBlinkText2.Tick
        Try
            'Me.lblMSG2.Visible = Not Me.lblMSG2.Visible
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_領収書発行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timReceipt_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timReceipt.Tick

        Me.timReceipt.Stop()

        ' 発行日
        _dcSK121.PrintDate = DateTime.Now

        ' 料金
        _dcSK121.ReceiptPrice = Me.NKNKN

        ' 発行者
        Dim publisher = New Techno.DeviceControls.Publisher
        publisher.Name = "アクティはしはま"
        publisher.Address = "愛媛県今治市内堀2丁目1番15号"
        publisher.PhoneNumber = "0898-41-9006"
        _dcSK121.Publisher = publisher

        ' 領収書番号
        _dcSK121.ReceiptNumber = "01-" & Me.DENNO.ToString.PadLeft(5, "0"c)

        If Not _dcSK121.PrintReceipt() Then
            Using frm As New frmMSGBOXEx("領収書の発行に失敗しました。", 2)
                frm.ShowDialog()
            End Using
        End If

    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            ' カード残金とポイントの表示
            Me.lblZANKN.Text = Me.USER_INFO.ZANKN.ToString("N0") & "円"
            Me.lblSRTPO.Text = Me.USER_INFO.SRTPO.ToString("N0") & "Ｐ"
           
            Me.lblPrice.Text = Me.NKNKN.ToString("N0") ' 購入金額
            Me.lblDesipot.Text = Me.DEPOSIT.ToString("N0") '投入金額
            Me.lblChange.Text = (Me.DEPOSIT - Me.NKNKN).ToString("N0") 'お釣り

            If Me.USER_INFO.IsCheckIn Then
                Me.picStatus.Image = Images.IconCheckIn
                Me.btnCheckInOut.Image = Images.BtnCheckOut
            Else
                Me.picStatus.Image = Images.IconCheckOut
                Me.btnCheckInOut.Image = Images.BtnCheckIn
            End If

            If CommonSettings.IsChargeResultEjectStopMode Then

                Me.btnCheckInOut.Visible = True
                Me.btnCancel.Image = Images.BtnClose

                If Me.RECEIPT Then
                    Me.lblBlinkText.Text = "領収書をお受け取りください。"
                    If Me.USER_INFO.IsCheckIn Then
                        Me.lblMSG2.Text = "チェックアウトする方はボタンにタッチしてください。"
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
                    Me.lblBlinkText.Text = "ご利用ありがとうございました。"
                    If Me.USER_INFO.IsCheckIn Then
                        Me.lblMSG2.Text = "チェックアウトする方はボタンにタッチしてください。"
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
                    Me.timBlinkText2.Interval = 1000
                    Me.timBlinkText2.Start()

                End If

            Else
                Me.btnCheckInOut.Visible = False

                If Me.RECEIPT Then
                    Me.lblBlinkText.Text = "カードと領収書をお受け取りください。"
                    Me.picImage.Image = Images.IconChargeResultCardAndReceipt
                    Me.timReceipt.Interval = 2000
                    Me.timReceipt.Start() ' 領収書をお取りください。

                    ' ありがとうございました
                    Me.timSound2.Interval = 4000
                    Me.timSound2.Start()

                Else
                    Me.lblBlinkText.Text = "カードをお受け取りください。"
                    Me.picImage.Image = Images.IconChargeResultCardOnly

                    ' ありがとうございました
                    Me.timSound2.Interval = 2500
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

    ''' <summary>
    ''' 排出停止中のカードを排出
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EjectPreCard()
        Try

            Sound.PlayReceiveCard()

            If CommonSettings.IsChargeResultEjectStopMode Then
                Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                    frm.COMMAND = frmREQUESTCARDEx.Command_Type.EJECT
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
