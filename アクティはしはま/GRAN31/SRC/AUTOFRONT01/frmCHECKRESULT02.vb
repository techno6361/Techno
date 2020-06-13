Imports System.Reflection

Public Class frmCHECKRESULT02

#Region "▼ 宣言部"

    Public Enum eType
        CHECKIN
        CHECKOUT
    End Enum

    Private _checkoutType As eType

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

    Public Sub New(ByVal _result As CheckResult, ByVal type As eType)

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

        If CommonSettings.IsCheckResultEjectStopMode Then
            Me.btnCharge.Visible = True
            Me.lblMSG2.Text = "ご入金を利用の方はボタンにタッチしてください。"
            Me.btnBack.Image = Images.BtnClose
            Sound.PlayTouchAnyButton()
        End If

    End Sub

#End Region

#Region "▼ イベント定義"

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
            Me.pnlENTKIN.Visible = False
            Me.pnlInfo.Visible = False

            ' カード残高/ポイント/状態
            Me.lblZANKN.Text = String.Format("{0}円", Me.Result.USER_INFO.ZANKN.ToString("N0"))
            Me.lblSRTPO.Text = String.Format("{0}Ｐ", Me.Result.USER_INFO.SRTPO.ToString("N0"))
            If Me.Result.USER_INFO.IsCheckIn Then
                Me.picStatus.Image = Images.IconCheckIn
            Else
                Me.picStatus.Image = Images.IconCheckOut
            End If

            ' 入場料
            If Me.Result.ENTKIN > 0 Then
                Me.pnlENTKIN.Visible = True
                Me.lblENTKINMSG.Text = String.Format("{0}円", Me.Result.ENTKIN.ToString("N0"))
            End If

            ' ボーナス
            Dim bonus = New List(Of String)
            If Me.Result.ENTPO > 0 Then
                bonus.Add(String.Format("{0}ポイントの入場ボーナスを獲得しました。", Me.Result.ENTPO.ToString("N0")))
            End If
            If Me.Result.OUTPO > 0 Then
                bonus.Add(String.Format("{0}ポイントの退場ボーナスを獲得しました。", Me.Result.OUTPO.ToString("N0")))
            End If
            If Me.Result.BIRTHDPO > 0 Then
                bonus.Add(String.Format("{0}ポイントの誕生日ボーナスを獲得しました。", Me.Result.BIRTHDPO.ToString("N0")))
            End If
            If Me.Result.BIRTHMPO > 0 Then
                bonus.Add(String.Format("{0}ポイントの誕生日ボーナスを獲得しました。", Me.Result.BIRTHMPO.ToString("N0")))
            End If
            If Me.Result.M_ENTCNT > 0 And Me.Result.M_ENTCNTPO > 0 Then
                bonus.Add(String.Format("月間{0}回目の来場ボーナスで{1}ポイントを獲得しました。", Me.Result.M_ENTCNT.ToString("N0"), Me.Result.M_ENTCNTPO.ToString("N0")))
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
                End Select
                i += 1
            Next

            ' 位置調整
            If Not Me.pnlENTKIN.Visible And Not Me.pnlInfo.Visible Then
                Me.lblMSG1.Location = New Point(Me.lblMSG1.Location.X, Me.lblMSG1.Location.Y + 225)
                Me.lblMSG2.Location = New Point(Me.lblMSG2.Location.X, Me.lblMSG2.Location.Y + 225)
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
            If CommonSettings.IsCheckResultEjectStopMode Then

                ' カード排出停止時
                If _checkoutType = eType.CHECKIN Then
                    Me.tmClose.Interval = CommonSettings.CheckResultCloseInterval
                    Me.tmClose.Start()
                Else
                    Me.tmSound.Interval = 4000
                    Me.tmSound.Start()
                End If

                ' 点滅タイマー
                Me.timBlinkText.Interval = 1000
                Me.timBlinkText.Start()

            Else

                ' 通常
                If _checkoutType = eType.CHECKIN Then
                    Sound.PlayReceiveCard()
                    Me.tmClose.Interval = CommonSettings.CheckResultCloseInterval
                    Me.tmClose.Start()
                Else
                    Sound.PlayReceiveCard()
                    Me.tmSound.Interval = 3000
                    Me.tmSound.Start()
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
            If CommonSettings.IsCheckResultEjectStopMode Then
                EjectPreCard()
            End If
            Me.vbDialogResult = eResult.NONE
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

            Me.tmClose.Interval = 8000
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
            Me.lblMSG2.Visible = Not Me.lblMSG2.Visible
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
            If CommonSettings.IsCheckResultEjectStopMode Then
                EjectPreCard()
            End If
            Me.vbDialogResult = eResult.NONE
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼ 関数定義"

    ''' <summary>
    ''' 排出停止中のカードを排出
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EjectPreCard()
        Try

            Sound.PlayReceiveCard()

            If CommonSettings.IsCheckResultEjectStopMode Then
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