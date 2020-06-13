Imports System.Reflection

Public Class frmCHECKRESULT

#Region "▼ 宣言部"

    Public Enum eType
        CHECKIN
        CHECKOUT
    End Enum

    Private _checkoutType As eType

    Private _frm As frmMSGBOXEx03

    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New TECHNO.DeviceControls.ICR700Control
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New TECHNO.DeviceControls.MCH3000Control

#End Region

#Region "▼ プロパティ"

    Public Property Result As CheckResult = New CheckResult ' チェックイン/チェックアウト用の結果格納クラス

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
    ''' ﾁｪｯｸｱｳﾄ時にスロットをﾌﾟﾚｲ済みならばTrue
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SlotAfter As Boolean = False

    ' 入金画面から呼ばれたときに排出停止モードを強制的にOFFにする
    Private _blnEjectFlg As Boolean = False

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

    Public Sub New(ByVal _result As CheckResult, ByVal type As eType, Optional ByVal blnEjectFlg As Boolean = False)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        ' すべてのコントロールにダブルバッファリングを有効化
        For Each c As Control In GetAllControls(Me)
            EnableDoubleBuffering(c)
        Next

        Me.Result = _result

        Select Case type
            Case eType.CHECKIN
                Me.lblMSG1.Text = "チェックインが完了しました。"
                Me.lblMSG2.Text = "ごゆっくりお楽しみください。"
                _checkoutType = eType.CHECKIN

            Case eType.CHECKOUT
                Me.lblMSG1.Text = "チェックアウトが完了しました。"
                Me.lblMSG2.Text = "またのご来店をお待ちしています。"
                _checkoutType = eType.CHECKOUT

        End Select

        _blnEjectFlg = blnEjectFlg

        ' カード排出停止モードの表示
        If IsResultEjectStopMode() And _checkoutType = eType.CHECKIN And Not _blnEjectFlg Then
            Me.btnCharge.Location = New Point(259, 468)
            Me.btnBack.Location = New Point(559, 468)
            Me.lblMSG2.ForeColor = Color.Yellow
            'frmBase.ChangePictureBoxEnabled(Me.btnCharge, True)
            Me.lblMSG2.Text = "ご入金を利用の方はボタンにタッチしてください。"
            'Me.btnBack.Image = Images.BtnEjectCard
            Sound.PlayTouchAnyButton()
        Else

        End If

        If UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
            frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
        End If

    End Sub

#End Region

#Region "▼ イベント定義"

    ''' <summary>
    ''' フォーム_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCHECKRESULT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Disposed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCHECKRESULT_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            If Not _frm Is Nothing Then
                _frm.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCHECKIN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblBonus1.Visible = False
            Me.lblBonus2.Visible = False
            Me.lblBonus3.Visible = False
            Me.lblBonus4.Visible = False
            Me.lblBonus5.Visible = False
            Me.pnlENTKIN.Visible = False
            Me.pnlInfo.Visible = False

            ' カード残高/ポイント/状態
            Me.lblZANKN.Text = String.Format("{0}円", Me.Result.USER_INFO.ZANKN.ToString("N0"))
            Me.lblSRTPO.Text = String.Format("{0}Ｐ", Me.Result.USER_INFO.SRTPO.ToString("N0"))
            'If CType(Me.Result.USER_INFO.SRTPO, Integer).Equals(0) Then
            '    Me.Label5.Visible = False
            '    Me.lblSRTPO.Visible = False
            'End If

            ' 入場料
            If Me.Result.ENTKIN > 0 Then
                Me.pnlENTKIN.Visible = True
                Me.lblENTKINMSG.Text = String.Format("{0}円", Me.Result.ENTKIN.ToString("N0"))
            End If

            If Me.Result.PLAYKBN.Equals(2) Then
                Me.Label1.Text = "打ち放題"
            End If

            ' ボーナス
            Dim bonus = New List(Of String)
            If Me.Result.ENTPO > 0 Then
                bonus.Add(String.Format("{0}ポイント : 入場ボーナスを獲得しました。", Me.Result.ENTPO.ToString("N0")))
            End If
            If Me.Result.OUTPO > 0 Then
                bonus.Add(String.Format("{0}ポイント : 退場ボーナスを獲得しました。", Me.Result.OUTPO.ToString("N0")))
            End If
            If Me.Result.BIRTHDPO > 0 Then
                bonus.Add(String.Format("{0}ポイント : 誕生日ボーナスを獲得しました。", Me.Result.BIRTHDPO.ToString("N0")))
            End If
            If Me.Result.BIRTHMPO > 0 Then
                bonus.Add(String.Format("{0}ポイント : 誕生月ボーナスを獲得しました。", Me.Result.BIRTHMPO.ToString("N0")))
            End If
            If Me.Result.M_ENTCNT > 0 And Me.Result.M_ENTCNTPO > 0 Then
                bonus.Add(String.Format("{1}ポイント : 月間{0}回目の来場ボーナスを獲得しました。", Me.Result.M_ENTCNT.ToString("N0"), Me.Result.M_ENTCNTPO.ToString("N0")))
            End If
            If Me.Result.MINIGAMEPO > 0 Then
                bonus.Add(String.Format("{0}ポイント : ミニゲームボーナスを獲得しました。", Me.Result.MINIGAMEPO.ToString("N0")))
            End If
            If bonus.Any Then Me.pnlInfo.Visible = True
            Dim i = 0
            For Each b In bonus
                Select Case i
                    Case 0
                        Me.lblBonus1.Text = b
                        Me.lblBonus1.Visible = True
                    Case 1
                        Me.lblBonus2.Text = b
                        Me.lblBonus2.Visible = True
                    Case 2
                        Me.lblBonus3.Text = b
                        Me.lblBonus3.Visible = True
                    Case 3
                        Me.lblBonus4.Text = b
                        Me.lblBonus4.Visible = True
                    Case 4
                        Me.lblBonus5.Text = b
                        Me.lblBonus5.Visible = True
                End Select
                i += 1
            Next

            ' フォームを表示
            'If bonus.Any Then
            '    _frm = New frmMSGBOXEx03(bonus)
            '    _frm.TopMost = True
            '    _frm.Show(Me)
            'End If

            ' 位置調整
            If Not Me.pnlENTKIN.Visible And Not Me.pnlInfo.Visible Then
                Me.lblMSG1.Location = New Point(Me.lblMSG1.Location.X, Me.lblMSG1.Location.Y)
                Me.lblMSG2.Location = New Point(Me.lblMSG2.Location.X, Me.lblMSG2.Location.Y)
            End If

            If Me.pnlInfo.Visible Then
                Me.btnCharge.Location = New Point(Me.btnCharge.Location.X, Me.btnCharge.Location.Y)
                Me.btnBack.Location = New Point(Me.btnBack.Location.X, Me.btnBack.Location.Y)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCHECKIN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If IsResultEjectStopMode() And _checkoutType = eType.CHECKIN And Not _blnEjectFlg Then

                ' 【カード排出停止モード & チェックイン】

                Me.tmClose.Interval = CommonSettings.CheckResultCloseIntervalLong
                Me.tmClose.Start()

                ' 点滅タイマー
                Me.timBlinkText.Interval = 1000
                Me.timBlinkText.Start()

            Else

                ' 【カード排出しないモード & チェックイン または チェックアウト】

                If _checkoutType = eType.CHECKIN Then
                    'If Not _blnEjectFlg Then Sound.PlayReceiveCard()
                    If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                        '【通常】
                        Me.tmClose.Interval = CommonSettings.CheckResultCloseInterval
                        If _blnEjectFlg Then Me.tmClose.Interval = 5000
                    Else
                        '【自動受付】
                        Me.tmClose.Interval = CommonSettings.CheckResultCloseInterval
                    End If
                    Me.btnBack.Visible = False
                    Me.btnCharge.Visible = False
                    Me.tmClose.Start()
                Else
                    'Sound.PlayReceiveCard()
                    Me.tmSound.Interval = 3000
                    Me.tmSound.Start()

                    Me.btnBack.Visible = False
                    Me.Refresh()

                    '' カードが完全に排出されたのを待つ
                    'Threading.Thread.Sleep(5000)
                    'Me.btnBack.Visible = True
                End If

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ボタン_入金画面へ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCharge.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            Me.vbDialogResult = eResult.CHARGE
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
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_音声
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmSound_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmSound.Tick
        Try
            Me.tmSound.Stop()
            Sound.PlayThank()

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                '【通常】
                Me.tmClose.Interval = CommonSettings.CheckResultCloseIntervalShort
            Else
                '【自動受付】
                Me.tmClose.Interval = CommonSettings.CheckResultCloseIntervalShort
            End If

            Me.tmClose.Start()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_文字の点滅
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timBlinkText_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBlinkText.Tick
        Try
            'Me.lblMSG2.Visible = Not Me.lblMSG2.Visible
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 自動で閉じるタイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmClose_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmClose.Tick
        Try
            Me.tmClose.Stop()
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼ 関数定義"

    ''' <summary>
    ''' 排出停止モード判定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsResultEjectStopMode() As Boolean
        Return CommonSettings.IsCheckResultEjectStopMode
    End Function

#End Region


End Class