#Const DEBUG_CHECKIN = False ' 永遠にチェックインしない

Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmMenuEx

#Region "▼宣言部"

    ''' <summary>
    ''' データベース制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _iDatabase As IDatabase.IMethod
    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New TECHNO.DeviceControls.ICR700Control
    ''' <summary>
    ''' MCH3000制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New TECHNO.DeviceControls.MCH3000Control
    ''' <summary>
    ''' レシートプリンタコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSK121 As New TECHNO.DeviceControls.SK121Control
    ''' カード発券機コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSTR610 As New TECHNO.DeviceControls.STR610Control
    ''' <summary>
    ''' ビルバリコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.SC1708Control
    ''' <summary>
    ''' 顧客情報保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtCSMAST As DataTable
    ''' <summary>
    ''' 選択打席番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _intSeatNo As Integer

    ''' <summary>
    ''' 顧客種別名
    ''' </summary>
    ''' <remarks></remarks>
    Private _strCKBNAME As String
    ''' <summary>
    ''' 一球打ちか打ち放題か【1】一球打ち【2】打ち放題
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPlaySelect As Integer
    ''' <summary>
    ''' 入場料金
    ''' </summary>
    ''' <remarks></remarks>
    Private _intENTKIN As Integer
    ''' <summary>
    ''' 入場料金割引額(打ち放題の金額から割り引かれる)
    ''' </summary>
    ''' <remarks></remarks>
    Private _intWARIBIKI As Integer
    ''' <summary>
    ''' 時間貸し区分
    ''' </summary>
    ''' <remarks></remarks>
    Private _intJKNKB As Integer
    ''' <summary>
    ''' 貸し時間
    ''' </summary>
    ''' <remarks></remarks>
    Private _intJKNMM As Integer
    ''' <summary>
    ''' 指定フロア
    ''' </summary>
    ''' <remarks></remarks>
    Private _intFLRKB As Integer
    ''' <summary>
    ''' ボール単価1F
    ''' </summary>
    ''' <remarks></remarks>
    Private _intBALLKIN1F As Integer
    ''' <summary>
    ''' ボール単価2F
    ''' </summary>
    ''' <remarks></remarks>
    Private _intBALLKIN2F As Integer
    ''' <summary>
    ''' 取得ﾎﾟｲﾝﾄ
    ''' </summary>
    ''' <remarks></remarks>
    Private _intGetPOINT As Integer
    ''' <summary>
    ''' 月間来場ポイント
    ''' </summary>
    ''' <remarks></remarks>
    Private _intETPPO As Integer
    ''' <summary>
    ''' 誕生月ポイント
    ''' </summary>
    ''' <remarks></remarks>
    Private _intBIRTHMPO As Integer
    ''' <summary>
    ''' 誕生日ﾎﾟｲﾝﾄ
    ''' </summary>
    ''' <remarks></remarks>
    Private _intBIRTHDPO As Integer

    Private _intSYSMENUCount As Integer = 0

    ' カード発券機のエラー
    Private _cardDispenserUnitMessage As Techno.DeviceControls.STR610Control.UNIT_STATUS

    ' なんらかのシステムエラー
    Private _CheckInEnabled As Boolean = True
    Private _ChargeEnabled As Boolean = True

    ''' <summary>
    ''' 入金機機能
    ''' </summary>
    ''' <remarks></remarks>
    Private _strFunction As String = String.Empty
    ''' <summary>
    ''' 有効期限切れ時カード残金クリア区分【0】クリアせずｶｰﾄﾞ受け付けない【1】残金クリアする
    ''' </summary>
    ''' <remarks></remarks>
    Private _intZANKIN_CLEARE As Integer = 0

    ' チェックイン/チェックアウト用の結果格納クラス
    Private _CheckResult As CheckResult = New CheckResult

    ''' <summary>
    ''' 有効期限切れ確認変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intCLRKBN As Integer = 0

    ''' <summary>
    ''' カード排出フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnEjectFlg As Boolean = False

    Private _drwPoint As System.Drawing.Point
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
    Private Sub frmMenu01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '時刻調整
            Process.Start(Configuration.ConfigurationManager.AppSettings("JIKOKU_PATH"))

            ' エラーリセット
            _CheckInEnabled = True
            _ChargeEnabled = True

            '画面初期設定
            Init()

            ' 時計開始
            Me.mClock.WorkerReportsProgress = True
            Me.mClock.RunWorkerAsync()


            'マウスカーソル非表示
            'Cursor.Hide()

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
    Private Sub frmMenu01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.lblMessage.ForeColor = Color.Yellow
            Me.Refresh()

            'DB登録用本日営業日セット
            UIUtility.SYSTEM.UPDDAY = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            'システムモード【0】オートセッター【1】ボール貸出機
            UIUtility.SYSTEM.SYSTEMMODE = CType(Configuration.ConfigurationManager.AppSettings("SYSTEMMODE"), Integer)
            'システム区分【0】入金機モード【1】自動受付ﾄ専用端末
            UIUtility.SYSTEM.SYSTEMKBN = CType(Configuration.ConfigurationManager.AppSettings("SYSTEMKBN"), Integer)
            '機能
            _strFunction = Configuration.ConfigurationManager.AppSettings("FUNCTION")
            '店番
            UIUtility.SYSTEM.SHOPNO = Configuration.ConfigurationManager.AppSettings("SHOPNO")
            'ICR700リーダライターCOMポート名
            UIUtility.SYSTEM.ICR700COM = Configuration.ConfigurationManager.AppSettings("ICR700RW_COM")
            'MCH3000カード搬送ユニットCOMポート名
            UIUtility.SYSTEM.MCH3000COM = Configuration.ConfigurationManager.AppSettings("MCH3000_COM")
            'カードユニット区分
            UIUtility.SYSTEM.RWUnitKB = 1
            'AD1COMポート名
            UIUtility.SYSTEM.AD1COM = Configuration.ConfigurationManager.AppSettings("AD1_COM")
            'レシートプリンターポート名
            UIUtility.SYSTEM.SK121COM = Configuration.ConfigurationManager.AppSettings("SK121_COM")
            'カード発券機ポート名
            UIUtility.SYSTEM.STR610COM = Configuration.ConfigurationManager.AppSettings("STR610_COM")
            '音声ファイルパス取得
            UIUtility.SYSTEM.SOUND_PAHT = Configuration.ConfigurationManager.AppSettings("SOUND_PATH")
            'カードリードインターバル
            UIUtility.SYSTEM.ReadInterval = CType(Configuration.ConfigurationManager.AppSettings("ReadInterval"), Integer)
            '有効期限切れｶｰﾄﾞ残金クリア区分
            _intZANKIN_CLEARE = CType(Configuration.ConfigurationManager.AppSettings("ZANKIN_CLEARE"), Integer)


            Try
                'データベース接続情報取得
                Dim DbInfo As String = Configuration.ConfigurationManager.AppSettings("DbInfo_PATH")

                '*** ＤＢ接続 ***'
                Dim dbControl As DatabaseControl = New DatabaseControl

                If dbControl.CreateDatabaseInstance(DatabaseControl.DatabaseKind.PostgreSQL) Then
                    _iDatabase = dbControl.IDB
                    _iDatabase.Connect(DbInfo)
                    _iDatabase.Open()
                End If
            Catch ex As Exception
                'Me.lblMessage.Text = "データベースの接続に失敗しました。"
                'Me.lblMessage.Refresh()
                'Thread.Sleep(5000)
                Using frm As New frmMSGBOXEx("データベースの接続に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Me.Close()
                Exit Sub
            End Try

            '伝票番号クリア処理
            If Not ClrDENNO() Then
                Using frm As New frmMSGBOXEx("伝票番号の0クリア処理に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Me.Close()
                Exit Sub
            End If


            '帳票用入金機履歴テーブル作成
            If Not InsREPOCHARGE_M() Then
                Using frm As New frmMSGBOXEx("帳票用入金機履歴テーブルの作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Me.Close()
                Exit Sub
            End If

            '***【各ユニットの起動チェック】***

            Dim blnMCH3000 = True   ' カード搬送ユニット

            '【MCH3000カード搬送ユニット接続】'
            If _dcMCH3000.IsOpen Then
                _dcMCH3000.Close()
            End If
            Do
                If Not _dcMCH3000.Open(UIUtility.SYSTEM.MCH3000COM) Then
                    blnMCH3000 = False
                    Exit Do
                End If
                If Not _dcMCH3000.Init() Then
                    ' 何故か電源投下時のみInitしたら初回動作＆エラーが返ってくるのでスルー
                    'blnErr = True
                End If
                If Not _dcMCH3000.Reset() Then
                    blnMCH3000 = False
                    Exit Do
                End If
                Thread.Sleep(300)
                ' カード詰まり強制リセット
                If Not _dcMCH3000.RS_Command(2) Then
                    blnMCH3000 = False
                    Exit Do
                End If
                Exit Do
            Loop
            If Not blnMCH3000 Then
                Me.lblCardDispenserError.Visible = True
                Me.lblCardDispenserError.Text = "発券エラー"
            Else
                Me.lblCardDispenserError.Visible = False
            End If

            '【ICR700リーダライター接続】'
            Dim blnICR700 = True    ' ICRW
            Do
                If _dcICR700.IsOpen Then
                    _dcICR700.Close()
                End If
                If Not _dcICR700.Open(UIUtility.SYSTEM.ICR700COM) Then
                    blnICR700 = False
                    Exit Do
                End If
                Dim keys = New Byte() {&H36, &H37, &H37, &H37, &H37, &H32}
                If Not _dcICR700.AuthKeySet(keys, False, 2, 4) Then
                    blnICR700 = False
                    Exit Do
                End If
                If Not _dcICR700.Cancel Then
                    blnICR700 = False
                End If

                Exit Do
            Loop

            If Not blnICR700 Then
                Me.lblRWError.Visible = True
            Else
                Me.lblRWError.Visible = False
            End If

            Thread.Sleep(1000)

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                '【入金機モード】

                If _strFunction.Substring(0, 1).Equals("0") Then
                    '【ﾁｪｯｸｲﾝ】
                    Me.btnCheckIn.Visible = False
                    'frmBase.ChangePictureBoxEnabled(Me.btnCheckIn, False)
                End If
                If _strFunction.Substring(1, 1).Equals("0") Then
                    '【カード確認】
                    Me.btnInfo.Visible = False
                    'frmBase.ChangePictureBoxEnabled(Me.btnInfo, False)
                End If
                If _strFunction.Substring(2, 1).Equals("0") Then
                    '【入金】
                    Me.btnCharge.Visible = False
                    'frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
                End If
                If _strFunction.Substring(3, 1).Equals("0") Then
                    '【ｶｰﾄﾞ発券】
                    Me.btnCardDispenser.Visible = False
                    'frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
                End If

                ' 【カード発券機接続】
                If Me.btnCardDispenser.Visible Then
                    If blnMCH3000 Then
                        If CheckCardDispenser() Then
                            frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, True)
                            Me.lblCardDispenserError.Visible = False
                        Else
                            frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
                            Me.lblCardDispenserError.Visible = True
                        End If
                    End If
                End If

                If Me.btnCharge.Visible Then

                    '【ビルバリ】
                    Dim blnAD1 As Boolean = False
                    _dcAD1.Open(UIUtility.SYSTEM.AD1COM)
                    If _dcAD1.Reset Then
                        '接続があるか確認
                        If Not _dcAD1.ConnectionCheck.Equals("2") Then
                            frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
                            Me.lblAd1Error.Visible = True
                        End If
                        For i As Integer = 1 To 7
                            CheckAD1()
                            If Not Me.lblAd1Error.Visible Then
                                Exit For
                            Else
                                Thread.Sleep(200)
                            End If
                        Next
                    Else
                        frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
                        Me.lblAd1Error.Visible = True
                    End If
                End If

                Me.lblMessage.Text = "ご希望のボタンをタッチしてください。"
                Me.lblMessage.ForeColor = Color.White

                'frmBase.ChangePictureBoxEnabled(Me.btnCheckIn, True)
                'frmBase.ChangePictureBoxEnabled(Me.btnInfo, True)

            ElseIf UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
                '【自動受付専用端末】

                frmBase.ChangePictureBoxEnabled(Me.btnCheckIn, True)
                Me.btnCheckIn.Visible = False

                Me.btnCheckIn.Width = 1
                Me.btnCheckIn.Height = 1

                frmBase.ChangePictureBoxEnabled(Me.btnInfo, True)
                Me.btnInfo.Visible = False

                Me.btnInfo.Width = 1
                Me.btnInfo.Height = 1

                Me.btnCharge.Visible = False
                Me.btnCardDispenser.Visible = False

                Me.BackgroundImage = Images.FrameMenuBlank
                Me.lblMessage.Height += 25
                Dim y = CInt((frmBase.ScreenSize.Height / 2) - (Me.lblMessage.Height / 2))
                Me.lblMessage.Location = New Point(0, y)
                Me.lblMessage.Size = New Size(frmBase.ScreenSize.Width, Me.lblMessage.Height)
                Me.lblMessage.TextAlign = ContentAlignment.MiddleCenter
                Me.lblMessage.Font = New Font("游ゴシック", 50, FontStyle.Bold)

                Me.lblMessage.Text = "【ﾁｪｯｸｲﾝ】／【ﾁｪｯｸｱｳﾄ】の方はカードをセットしてください。"
                Me.lblMessage.ForeColor = Color.White

                'カード要求開始
                ReadStart()
            Else
                '【営業中止】

                frmBase.ChangePictureBoxEnabled(Me.btnCheckIn, True)
                Me.btnCheckIn.Visible = False

                Me.btnCheckIn.Width = 1
                Me.btnCheckIn.Height = 1

                frmBase.ChangePictureBoxEnabled(Me.btnInfo, True)
                Me.btnInfo.Visible = False

                Me.btnInfo.Width = 1
                Me.btnInfo.Height = 1

                frmBase.ChangePictureBoxEnabled(Me.btnCharge, True)
                Me.btnCharge.Visible = False
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, True)
                Me.btnCardDispenser.Visible = False

                Me.BackgroundImage = Images.FrameMenuBlank
                Me.lblMessage.Height += 25
                Dim y = CInt((frmBase.ScreenSize.Height / 2) - (Me.lblMessage.Height / 2))
                Me.lblMessage.Location = New Point(0, y)
                Me.lblMessage.Size = New Size(frmBase.ScreenSize.Width, Me.lblMessage.Height)
                Me.lblMessage.TextAlign = ContentAlignment.MiddleCenter
                Me.lblMessage.Font = New Font("游ゴシック", 55, FontStyle.Bold)

                Me.lblMessage.Text = "フ ロ ン ト に お 越 し く だ さ い。"
                Me.lblMessage.ForeColor = Color.White

            End If

            If CommonSettings.DEBUG Then
                Me.btnCharge.Enabled = True
            End If

            ' 入金不可
            If Me.lblAd1Error.Visible Or Me.lblRWError.Visible Then
                _ChargeEnabled = False
            End If
            ' チェックイン不可
            If Me.lblRWError.Visible Or (Me.lblCardDispenserError.Visible And Me.lblCardDispenserError.Text.Equals("発券エラー")) Then
                _CheckInEnabled = False

                Me.btnCheckIn.Enabled = False
                Me.btnInfo.Enabled = False
                Me.btnCharge.Enabled = False
                Me.btnCardDispenser.Enabled = False
                frmBase.ChangePictureBoxEnabled(Me.btnCheckIn, False)
                frmBase.ChangePictureBoxEnabled(Me.btnInfo, False)
                frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
            ElseIf Me.lblAd1Error.Visible Then
                frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMenu01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            '顧客情報ﾃｰﾌﾞﾙ削除
            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Dispose()
            End If
            '顧客種別マスタ
            If Not UIUtility.TABLE.KBMAST Is Nothing Then
                UIUtility.TABLE.KBMAST.Dispose()
            End If

            '営業情報マスタ
            If Not UIUtility.TABLE.EIGMT Is Nothing Then
                UIUtility.TABLE.EIGMT.Dispose()
            End If

            'ICR700リーダライターポートクローズ
            '_dcICR700.Cancel(False)
            _dcICR700.Close()

            'MCH3000ユニットクローズ
            '_dcMCH3000.Reset()
            _dcMCH3000.Close()

            'AD1ポートクローズ
            _dcAD1.Close()
            'レシートプリンターポートクローズ
            _dcSK121.Close()
            ' カード発券機ポートクローズ
            _dcSTR610.Close()

            If Not _iDatabase Is Nothing Then
                _iDatabase.Dispose()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' フォーム_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMENU01_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            '各キー入力時のイベント
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' システムメニュー起動カウントダウン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub picSYSMENU01_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSYSMENU01.MouseDown, ProgressBar1.MouseDown
        Try
            Me.Cursor = Cursors.WaitCursor

            'コントロールを初期化する
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = 4
            ProgressBar1.Value = 0
            _intSYSMENUCount = 0

            Me.timSYSMENU01.Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' システムメニュー起動カウントダウン解除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub picSYSMENU01_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSYSMENU01.MouseUp, ProgressBar1.MouseUp
        Try
            Me.timSYSMENU01.Stop()
            _intSYSMENUCount = 0
            Me.ProgressBar1.Value = 0
            Me.ProgressBar1.Visible = False

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
                '【ﾁｪｯｸｱｳﾄ専用端末】  
                ReadStart()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' システムメニュー起動タイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timSYSMENU01_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timSYSMENU01.Tick
        Dim blnPowerOff As Boolean = False
        Dim blnReStart As Boolean = False
        Try
            _intSYSMENUCount += 1

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
                '【ﾁｪｯｸｱｳﾄ専用端末】  
                If _intSYSMENUCount > 0 Then
                    ReadStop()
                End If
            End If

            Me.ProgressBar1.Value = _intSYSMENUCount
            Me.ProgressBar1.Refresh()

            If _intSYSMENUCount.Equals(4) Then

                Me.timSYSMENU01.Stop()

                Using frm As New frmSYSMENU01
                    frm.iDatabase = _iDatabase
                    frm.ICR700 = _dcICR700
                    frm.MCH3000 = _dcMCH3000
                    frm.AD1 = _dcAD1
                    frm.ShowDialog()
                    _dcAD1 = frm.AD1
                    blnPowerOff = frm.PowerOff
                    blnReStart = frm.ReStart
                End Using
                Me.ProgressBar1.Visible = False
                Me.ProgressBar1.Refresh()
                If Not blnPowerOff Then
                    If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                        '【入金機モード】

                        If _strFunction.Substring(3, 1).Equals("1") Then
                            ' カード発券機状態確認
                            Try
                                CheckCardDispenser()
                            Catch ex As Exception
                                Me.lblCardDispenserError.Visible = True
                            End Try

                        End If

                        If _strFunction.Substring(2, 1).Equals("1") Then
                            'ビルバリ情報確認
                            CheckAD1()
                        End If

                    ElseIf UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
                        '【自動受付専用端末】
                        ReadStart()
                    Else
                        '【営業中止】
                    End If
                End If
                Me.Cursor = Cursors.Default
            ElseIf _intSYSMENUCount.Equals(2) Then
                Me.ProgressBar1.Visible = True
            End If

            If blnPowerOff Then
                '【電源断(シャットダウン)】
                '強制的にシャットダウン()
                UIFunction.AdjustToken()
                UIUtility.ExitWindowsEx(UIUtility.ExitWindows.EWX_POWEROFF Or UIUtility.ExitWindows.EWX_POWEROFF, 0)

                Exit Sub
            ElseIf blnReStart Then
                '【システム再起動】
                Application.Restart()
                Exit Sub
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' チェックインボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheckIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIn.Click
        Dim blnERRFLG As Boolean = False        'エラーが発生したかどうか
        Dim blnCANCEL As Boolean = False        '取消が押されたか
        Dim blnERRSHOPFLG As Boolean = False    '店番エラーが発生したかどうか
        Dim strMsg As String = String.Empty

        Dim dummy = New frmDummy
        dummy.Show(Me)

        Try

            ' 結果リセット
            _CheckResult = New CheckResult

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                If Not Me.vbDialogResult = eResult.CHECKIN Then
                    'PushAnimation(CType(sender, Control))
                    Sound.PlayInsertCard()
                End If

                Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000, _iDatabase)
                    frm.COMMAND = frmREQUESTCARDEx.Command_Type.READ
                    frm.ShowDialog()
                    blnCANCEL = frm.CANCEL
                    blnERRSHOPFLG = frm.ERRSHOPFLG
                    blnERRFLG = frm.ERRFLG
                End Using
            End If

            '取消押された
            If blnCANCEL Then
                '命令取り消し
                _dcICR700.Cancel()
                If Not String.IsNullOrEmpty(_dcICR700.SHOPNO) Or blnERRFLG Then
                    CardEject(False)
                End If
                Exit Sub
            End If

            'エラー発生 / '店番エラー発生
            If blnERRFLG Or blnERRSHOPFLG Then
                'Using frm As New frmMSGBOXEx("店番が一致しません。", 2)
                '    frm.ShowDialog()
                'End Using
                CardEject()
                Exit Sub
            End If

            If Not GetTable() Then

                If _intCLRKBN.Equals(99) Then
                    CardEject()
                Else
                    If _intZANKIN_CLEARE.Equals(0) And _intCLRKBN.Equals(1) Then
                        '残金のクリアしない
                        CardEject()
                        Exit Sub
                    End If
                    If Not ClearZANKN(_intCLRKBN) Then
                        Using frm As New frmMSGBOXEx("有効期限処理に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        CardEject()
                        Exit Sub
                    End If
                End If

                Exit Sub
            End If

            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                Using frm As New frmMSGBOXEx("只今の時間ﾁｪｯｸｲﾝなしでご利用頂けます。", 0)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '発行料未精算
            If _dcICR700.ENTKBN.Equals("1") Then
                Using frm As New frmMSGBOXEx("入金からカード発行料の精算をしてください", 0)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            _intJKNMM = 0
            _intFLRKB = 0
            _intPlaySelect = 1
            Using frm As New frmPlaySelect01()
                frm.iDatabase = _iDatabase
                frm.ICR700 = _dcICR700
                frm.ENTKIN = _intENTKIN
                frm.WARIBIKI = _intWARIBIKI
                frm.SRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
                frm.NKBNO = CType(_dtCSMAST.Rows(0).Item("NCSRANK").ToString, Integer)

                frm.ShowDialog()
                'blnERRFLG = frm.Err
                Me.vbDialogResult = frm.vbDialogResult
                _intPlaySelect = frm.PlaySelect
                _CheckResult.PLAYKBN = frm.PlaySelect
                '打ち放題選択
                If _intPlaySelect.Equals(2) Then
                    _intJKNKB = frm.JKNKB
                    _intENTKIN = frm.ENTKIN
                    _intJKNMM = frm.JKNMM
                    _intFLRKB = frm.FLRKB
                    _intGetPOINT += frm.POINT
                    If _intENTKIN < 0 Then
                        _intENTKIN = 0
                    End If
                End If
            End Using

            _CheckResult.ENTKIN = _intENTKIN

            Select Case Me.vbDialogResult
                Case frmBase.eResult.CANCEL
                    CardEject()
                    Exit Sub
                Case frmBase.eResult.vbERROR
                    CardEject()
                    Exit Sub
            End Select
            Me.vbDialogResult = eResult.NONE



            '【チェックイン】
            If Not CheckIn(strMsg) Then
                ' NG
                If Not String.IsNullOrEmpty(strMsg) Then
                    Using frm As New frmMSGBOXEx(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                Else
                    Exit Sub
                End If
            Else

                ' OK
                Using frm As New frmCHECKRESULT(_CheckResult, frmCHECKRESULT.eType.CHECKIN, _blnEjectFlg)
                    frm.ICR700 = _dcICR700
                    frm.MCH3000 = _dcMCH3000
                    frm.ShowDialog()
                    Me.vbDialogResult = frm.vbDialogResult
                End Using
                _blnEjectFlg = False

                ' 入金処理

                Select Case Me.vbDialogResult
                    Case frmBase.eResult.CHARGE
                        _blnEjectFlg = True
                        btnCharge_Click(sender, e)
                    Case frmBase.eResult.CANCEL
                        CardEject()
                End Select
                Me.vbDialogResult = eResult.NONE

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dummy.Close()

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                '【入金機モード】
                'Me.btnCheckOut.BackColor = Color.ForestGreen
                Me.lblMessage.Text = "ご希望のボタンをタッチしてください。"
                Me.lblMessage.ForeColor = Color.White
            End If

            Me.ActiveControl = Nothing
            Me.lblMessage.Focus()

        End Try
    End Sub


    ''' <summary>
    ''' 入金ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCharge.Click
        Dim blnERRFLG As Boolean = False        'エラーが発生したかどうか
        Dim blnCANCEL As Boolean = False        '取消が押されたか
        Dim blnERRSHOPFLG As Boolean = False    '店番エラーが発生したかどうか
        Dim strMsg As String = String.Empty

        Dim dummy = New frmDummy
        dummy.Show()

        Try
            If Not Me.vbDialogResult = eResult.CHARGE Then
                'frmBase.PushAnimation(CType(sender, Control))
            End If

            CheckAD1()

            If Me.lblAd1Error.Visible Then
                Exit Sub
            End If


            If CommonSettings.DEBUG Then
                If Not Me.btnCharge.Enabled Then
                    Exit Sub
                End If
            End If



            If Not Me.vbDialogResult = eResult.CHARGE Then
                Sound.PlayInsertCard()
            End If

            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000, _iDatabase)
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.READ
                frm.ShowDialog()
                blnCANCEL = frm.CANCEL
                blnERRSHOPFLG = frm.ERRSHOPFLG
                blnERRFLG = frm.ERRFLG
            End Using

            '取消押された
            If blnCANCEL Then
                '命令取り消し
                _dcICR700.Cancel()
                If Not String.IsNullOrEmpty(_dcICR700.SHOPNO) Or blnERRFLG Then
                    CardEject(False)
                End If
                Exit Sub
            End If

            'エラー発生 / '店番エラー発生
            If blnERRFLG Or blnERRSHOPFLG Then
                'Using frm As New frmMSGBOXEx("店番が一致しません。", 2)
                '    frm.ShowDialog()
                'End Using
                CardEject()
                Exit Sub
            End If

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOXEx("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            ' テロップ・コメントの取得
            UIFunction.GetDTELOP(_iDatabase)


            '営業情報マスタ取得
            If Not GetEIGMT() Then
                Using frm As New frmMSGBOXEx("営業情報マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '顧客情報取得
            If Not GetCSMAST() Then
                Using frm As New frmMSGBOXEx("顧客情報がありません。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '*** カード有効期限チェック ***'
            _intCLRKBN = 99
            If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                Dim blnReply As Boolean = False
                If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString) Then
                    Dim dtCARDLIMIT As DateTime = DateTime.Parse(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(6, 2))
                    ' 入金残高有効期限
                    Dim intPREMLIMIT As Integer = 0
                    Dim strCARDLIMIT As String = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                        intPREMLIMIT = CType("-" & UIUtility.SYSTEM.PREMLIMIT, Integer)
                        dtCARDLIMIT = dtCARDLIMIT.AddMonths(intPREMLIMIT)
                        strCARDLIMIT = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    End If
                    If Now.ToString("yyyyMMdd") > _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString Then
                        _intCLRKBN = 1
                        If _intZANKIN_CLEARE.Equals(0) Then
                            Using frm As New frmMSGBOXEx("カードの有効期限切れです。" & vbCrLf & "フロントへお越しください。", 2)
                                frm.ShowDialog()
                            End Using
                        Else
                            Using frm As New frmMSGBOXEx("カードの有効期限切れです。" & vbCrLf & "残金額をクリアします。", 2)
                                frm.ShowDialog()
                            End Using
                        End If
                    ElseIf Now.ToString("yyyy/MM/dd") > strCARDLIMIT And Not String.IsNullOrEmpty(strCARDLIMIT) Then
                        _intCLRKBN = 0
                        Using frm As New frmMSGBOXEx("残金額の有効期限切れです。" & vbCrLf & "残金額をﾌﾟﾚﾐｱﾑに移行します。", 2)
                            frm.ShowDialog()
                        End Using
                    End If
                    If Not _intCLRKBN.Equals(99) Then
                        If _intZANKIN_CLEARE.Equals(0) And _intCLRKBN.Equals(1) Then
                            '残金のクリアしない
                            CardEject()
                            Exit Sub
                        End If
                        If Not ClearZANKN(_intCLRKBN) Then
                            Using frm As New frmMSGBOXEx("有効期限処理に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            CardEject()
                            Exit Sub
                        End If
                        Exit Sub
                    End If
                End If
            End If

            'カード停止中
            If UIFunction.ChkDCSTPTRN(_dcICR700.CARDNO, _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), _iDatabase) Then
                Using frm As New frmMSGBOXEx("カード停止中です。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            ' テロップ・コメントの取得
            UIFunction.GetDTELOP(_iDatabase)

            Using frm As New frmCHARGEEx()
                frm.iDatabase = _iDatabase
                frm.ICR700 = _dcICR700
                frm.MCH3000 = _dcMCH3000
                frm.AD1 = _dcAD1
                frm.CSMAST = _dtCSMAST
                frm.EjectFlg = _blnEjectFlg

                Dim user = New UserInfo
                user.NCSNO = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                user.ZANKN = CInt(_dcICR700.KINGAKU)
                user.SRTPO = CInt(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c))
                user.ENTKBN = _dcICR700.ENTKBN
                frm.USER_INFO = user

                frm.ShowDialog()
                blnERRFLG = frm.Err
                Me.vbDialogResult = frm.vbDialogResult

            End Using

            _blnEjectFlg = False

            'ビルバリ状態取得
            CheckAD1()

            'If blnERRFLG Then
            '    'エラー発生
            '    Me.btnCharge.Enabled = False
            '    Me.lblAd1Error.Visible = True
            'End If

            Select Case Me.vbDialogResult
                Case frmBase.eResult.CHECKIN
                    _blnEjectFlg = True
                    btnCheckIn_Click(sender, e)
                Case frmBase.eResult.CANCEL
                    CardEject()
                Case frmBase.eResult.vbERROR
                    CardEject()
            End Select
            Me.vbDialogResult = eResult.NONE

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            dummy.Close()

            'Me.btnCharge.BackColor = Color.ForestGreen
            Me.lblMessage.Text = "ご希望のボタンをタッチしてください。"
            Me.lblMessage.ForeColor = Color.White

            Me.ActiveControl = Nothing
            Me.lblMessage.Focus()



        End Try
    End Sub

    ''' <summary>
    ''' ｶｰﾄﾞ発行ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCardDispenser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCardDispenser.Click

        CheckAD1()

        If Me.lblAd1Error.Visible Then
            Exit Sub
        End If

        If Not CheckCardDispenser() Then
            Using frm As New frmMSGBOXEx("発券ユニットが異常です。", 3)
                frm.ShowDialog()
            End Using
            Exit Sub
        End If

        'Now Loading の表示
        'frmBase.PushAnimation(CType(sender, Control))
        Me.Refresh()
        Cursor = Cursors.WaitCursor
        Dim dummy = New frmDummy
        dummy.Show(Me)
        dummy.Refresh()
        dummy.Update()
        Thread.Sleep(1000)

        Dim custmer = New CustmerModel
        custmer.Enabled = False
        Dim ICR700Data = New TECHNO.DeviceControls.ICR700Model ' カード情報の保持
        Dim backup = New EjectCardRestoreModel ' バックアップ
        Dim blnErr = False
        Dim errMsg = ""

        Try
            '④システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOXEx("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
                Throw New Exception
            End If

            '⑤営業情報マスタ取得
            If Not GetEIGMT() Then
                Using frm As New frmMSGBOXEx("営業情報マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Throw New Exception
            End If

            Dim blnCardIssued = False

            '⑦入金画面
            Using frm As New frmCHARGEEx()
                frm.iDatabase = _iDatabase
                frm.ICR700 = _dcICR700
                frm.MCH3000 = _dcMCH3000
                frm.AD1 = _dcAD1
                frm.CSMAST = _dtCSMAST
                frm.EjectFlg = _blnEjectFlg
                frm.EjectCardMode = True    ' 発券モードで入金画面を呼び出したフラグ
                frm.ICR700Data = ICR700Data ' カード情報(ICR700)

                Dim user = New UserInfo
                user.NCSNO = String.Empty
                user.ZANKN = CInt(ICR700Data.KINGAKU)
                user.SRTPO = CInt(ICR700Data.POINT)
                user.ENTKBN = ICR700Data.ENTKBN
                frm.USER_INFO = user

                frm.ShowDialog()

                blnErr = frm.Err
                blnCardIssued = frm.BlnCardIssued ' カードがすでに発券済みか
                Me.vbDialogResult = frm.vbDialogResult ' 中止を押すと eResult.CANCEL が返る
            End Using

            Select Case Me.vbDialogResult
                Case eResult.CHECKIN
                    _blnEjectFlg = True
                    btnCheckIn_Click(sender, e)
                Case eResult.CANCEL
                    If blnCardIssued Then
                        CardEject()
                    End If
                Case eResult.vbERROR
                    If blnCardIssued Then
                        CardEject()
                    End If
            End Select

            '⑦-②入金エラー
            If blnErr Then
                Throw New Exception
            End If

        Catch ex As Exception
            'Using frm As New frmMSGBOXEx("発券に失敗しました", 2)
            '    frm.ShowDialog()
            'End Using
        Finally
            dummy.Close()
            Cursor = Cursors.Default
            CheckCardDispenser()
            Me.ActiveControl = Nothing
            Me.lblMessage.Focus()

        End Try

    End Sub

    ''' <summary>
    ''' 画面情報クリアタイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timClearInfo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timClearInfo.Tick
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.timClearInfo.Stop()
        End Try
    End Sub

    ''' <summary>
    ''' カードリードタイマー(自動受付用)_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timRead_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timRead.Tick
        Dim intCnt As Integer
        Dim strErrNo As String = String.Empty
        Dim blnRead As Boolean = False
        Try
            Dim states() As Integer = {0, 1}
            If UIUtility.SYSTEM.RWUnitKB = 1 Then
                states = _dcMCH3000.SR_Command
            End If

            ' カードがコンタクト部に到着するまで待機
            If states(1) <> 1 Then
                Return
            End If

            blnRead = True

            'Dim ret = _dcICR700.Read(-1)
            'If ret = TECHNO.DeviceControls.ICR700Control.eResult.SUCCESS Then

            '    If (Not _dcICR700.SHOPNO.Equals(UIUtility.SYSTEM.SHOPNO)) And Not (_dcICR700.SHOPNO.Equals("0000")) Then
            '        '【店番不一致】
            '        Me.timRead.Stop()
            '        Using frm As New frmMSGBOXEx("店番が一致しません。", 2)
            '            frm.ShowDialog()
            '        End Using
            '        Me.Close()
            '        Exit Sub
            '    End If
            '    blnRead = True
            'Else
            '    If ret = (TECHNO.DeviceControls.ICR700Control.eResult.vbERROR Or TECHNO.DeviceControls.ICR700Control.eResult.TIMEOUT) Then
            '        '【失敗】
            '        Me.timRead.Stop()
            '        Using frm As New frmMSGBOX01("カードの読み取りに失敗しました。(RW①)", 2)
            '            frm.ShowDialog()
            '        End Using
            '        'カード要求開始
            '        ReadStart()
            '        Exit Sub
            '    End If
            'End If

            'If Not blnRead Then Exit Sub
            'Thread.Sleep(300)
            Me.timRead.Stop()

            Dim blnERRFLG As Boolean = False        'エラーが発生したかどうか
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000, _iDatabase)
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.READ
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using

            'エラー発生
            If blnERRFLG Then
                Me.btnCheckIn.Visible = False
                Me.btnInfo.Visible = False
                ReadStart()
                Exit Sub
            End If

            '【ﾁｪｯｸｲﾝ】
            Me.btnCheckIn.Visible = True
            Me.btnCheckIn_Click(sender, e)

            Me.btnCheckIn.Visible = False
            Me.btnInfo.Visible = False
            ReadStart()

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
            Me.lblMessage.Text = "システム起動中です・・・・・しばらくお待ちください。"
            Me.lblMessage.ForeColor = Color.Yellow

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード要求開始(自動ﾁｪｯｸｱｳﾄ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadStart()
        Try
            Thread.Sleep(2000)

            ' カード取り込み
            If UIUtility.SYSTEM.RWUnitKB = 1 Then
                Dim states = _dcMCH3000.SR_Command
                If states(0) = 0 Then
                    If Not _dcMCH3000.CA_Command(1) Then
                        Using frm As New frmMSGBOX01("カードの取り込みに失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        _dcMCH3000.Reset()
                        Exit Sub
                    End If
                End If
            End If

            ' ICRW初期化
            _dcICR700.Init()

            Me.timRead.Start()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード要求ストップ(自動ﾁｪｯｸｱｳﾄ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadStop()
        Try
            Me.timRead.Stop()

            ' カード搬入機のシャッターを閉める
            If UIUtility.SYSTEM.RWUnitKB = 1 Then
                _dcMCH3000.CA_Command(0)
            End If

            'コマンド取消
            _dcICR700.Cancel(False)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ｶｰﾄﾞ排出
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CardEject(Optional ByVal blnSound As Boolean = True)
        Dim blnERRFLG As Boolean = False
        Try
            ' カードがすでに入ってるかチェック
            Dim states = _dcMCH3000.SR_Command
            If states(1) = 0 Then
                Exit Sub
            End If

            ' カードをお受け取りください
            If blnSound Then
                Sound.PlayReceiveCard()
            End If

            'カード排出コマンド
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.EJECT
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Using frm As New frmMSGBOXEx("カード排出に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                _dcMCH3000.Reset()
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' システム情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSYSMTA() As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SYSMTA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            'センター名
            UIUtility.SYSTEM.SHOPNM = resultDt.Rows(0).Item("SHOPNM").ToString()
            '管理者パスワード
            UIUtility.SYSTEM.ADMNPW = resultDt.Rows(0).Item("ADMNPW").ToString()
            'SEパスワード
            UIUtility.SYSTEM.SEPW = resultDt.Rows(0).Item("SEPW").ToString()
            '球数税率
            UIUtility.SYSTEM.TAMATAX = CType(resultDt.Rows(0).Item("TAMATAX").ToString(), Integer)
            '税率
            UIUtility.SYSTEM.TAX = CType(resultDt.Rows(0).Item("TAX").ToString(), Integer)
            '税区分
            UIUtility.SYSTEM.TAXKBN = CType(resultDt.Rows(0).Item("TAXKBN").ToString(), Integer)
            '税端数区分
            UIUtility.SYSTEM.TAXHASUKBN = CType(resultDt.Rows(0).Item("TAXHASUKBN").ToString(), Integer)
            'フロア数
            UIUtility.SYSTEM.FLRSU = CType(resultDt.Rows(0).Item("FLRSU").ToString(), Integer)
            '打席数
            UIUtility.SYSTEM.SEATSU = CType(resultDt.Rows(0).Item("SEATSU").ToString(), Integer)
            '1F最終打席№
            UIUtility.SYSTEM.LSTNO1F = CType(resultDt.Rows(0).Item("LSTNO1F").ToString(), Integer)
            '2F最終打席№
            UIUtility.SYSTEM.LSTNO2F = CType(resultDt.Rows(0).Item("LSTNO2F").ToString(), Integer)
            '3F最終打席№
            UIUtility.SYSTEM.LSTNO3F = CType(resultDt.Rows(0).Item("LSTNO3F").ToString(), Integer)
            'カレンダー作成最終日付
            UIUtility.SYSTEM.CALLSTDT = resultDt.Rows(0).Item("CALLSTDT").ToString()
            'パスワード区分
            UIUtility.SYSTEM.PASSKBN = CType(resultDt.Rows(0).Item("PASSKBN").ToString(), Integer)
            '担当者確認フラグ
            UIUtility.SYSTEM.TANCHKFLG = CType(resultDt.Rows(0).Item("TANCHKFLG").ToString(), Integer)
            '会員期限調整日数
            UIUtility.SYSTEM.CALLMT = CType(resultDt.Rows(0).Item("CALLMT").ToString(), Integer)
            '左右兼用打席１
            UIUtility.SYSTEM.LRMULTI01 = CType(resultDt.Rows(0).Item("LRMULTI01").ToString(), Integer)
            '左右兼用打席２
            UIUtility.SYSTEM.LRMULTI02 = CType(resultDt.Rows(0).Item("LRMULTI02").ToString(), Integer)
            '左右兼用打席３
            UIUtility.SYSTEM.LRMULTI03 = CType(resultDt.Rows(0).Item("LRMULTI03").ToString(), Integer)
            '左右兼用打席４
            UIUtility.SYSTEM.LRMULTI04 = CType(resultDt.Rows(0).Item("LRMULTI04").ToString(), Integer)
            '左右兼用打席５
            UIUtility.SYSTEM.LRMULTI05 = CType(resultDt.Rows(0).Item("LRMULTI05").ToString(), Integer)
            '打席情報画面フラグ
            UIUtility.SYSTEM.DISPMULTI = CType(resultDt.Rows(0).Item("DISPMULTI").ToString(), Integer)
            '月間来場回数クリア月
            UIUtility.SYSTEM.CLRENTMCNT = resultDt.Rows(0).Item("CLRENTMCNT").ToString()
            'OSシャットダウン
            UIUtility.SYSTEM.OSDOWNFLG = CType(resultDt.Rows(0).Item("OSDOWNFLG").ToString(), Integer)
            'レシート印刷フラグ
            UIUtility.SYSTEM.RECEIPTFLG = CType(resultDt.Rows(0).Item("RECEIPTFLG").ToString(), Integer)
            'ﾁｪｯｸｱｳﾄﾎﾟｲﾝﾄ
            UIUtility.SYSTEM.CHKPOINT = CType(resultDt.Rows(0).Item("CHKPOINT").ToString(), Integer)
            'カード入金限度額
            UIUtility.SYSTEM.ZANMAX = CType(resultDt.Rows(0).Item("ZANMAX").ToString(), Integer)
            'カード残金有効期限
            UIUtility.SYSTEM.CARDLIMIT = CType(resultDt.Rows(0).Item("CARDLIMIT").ToString(), Integer)
            '商品引落しﾌﾟﾚﾐｱﾑ精算区分
            UIUtility.SYSTEM.HINPREMPAYKBN = CType(resultDt.Rows(0).Item("HINPREMPAYKBN").ToString(), Integer)
            'ｶｰﾄﾞ支出ﾌﾟﾚﾐｱﾑ精算区分
            UIUtility.SYSTEM.SHTPREMPAYKBN = CType(resultDt.Rows(0).Item("SHTPREMPAYKBN").ToString(), Integer)
            '入金残高有効期限
            UIUtility.SYSTEM.PREMLIMIT = CType(resultDt.Rows(0).Item("PREMLIMIT").ToString(), Integer)
            'カード発行料
            UIUtility.SYSTEM.CARDFEE = CType(resultDt.Rows(0).Item("CARDFEE").ToString(), Integer)
            'システム更新日時
            UIUtility.SYSTEM.UPDDTM = resultDt.Rows(0).Item("UPDDTM").ToString()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 本日料金体系情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRknInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" A.*")
            sql.Append(",B.RKNNM")  '料金体系名称
            sql.Append(",B.CLRR")   'カラーR
            sql.Append(",B.CLRG")   'カラーG
            sql.Append(",B.CLRB")   'カラーB
            sql.Append(" FROM CALMTA AS A")
            sql.Append(" LEFT JOIN RKNMTA AS B ON")
            sql.Append(" B.RKNKB = A.RKNKB")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" A.CALDT = '" & Now.Date.Year.ToString & Now.Date.Month.ToString.PadLeft(2, "0"c) & Now.Date.Day.ToString.PadLeft(2, "0"c) & "'")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For Each arr As DataRow In resultDt.Rows
                '料金区分取得
                UIUtility.SYSTEM.RKNKB = CType(arr("RKNKB").ToString(), Integer)
                '料金体系名称
                UIUtility.SYSTEM.RKNNM = arr("RKNNM").ToString()
                'RGB値
                UIUtility.COLOR_INFO.RKN_R = CType(arr("CLRR").ToString(), Integer)
                UIUtility.COLOR_INFO.RKN_G = CType(arr("CLRG").ToString(), Integer)
                UIUtility.COLOR_INFO.RKN_B = CType(arr("CLRB").ToString(), Integer)
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 営業情報マスタ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEIGMT() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //

                sql.Append(" SELECT")
                sql.Append(" *")
                sql.Append(" FROM EIGMTA")
                sql.Append(" ORDER BY")
                sql.Append(" TIMEKB,NKBNO")
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                sql.Append(" SELECT")
                sql.Append(" *")
                sql.Append(" FROM EIGMTC")
                sql.Append(" ORDER BY")
                sql.Append(" TIMEKB,NKBNO")
            End If

            UIUtility.TABLE.EIGMT = _iDatabase.ExecuteRead(sql.ToString())

            If UIUtility.TABLE.EIGMT.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY KBMAST ")

            UIUtility.TABLE.KBMAST = _iDatabase.ExecuteRead(sql.ToString())

            If UIUtility.TABLE.KBMAST.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If _dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
                Return False
            End If

            '顧客情報取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS TO_DBIRTH")  '誕生日
            sql.Append(",TO_CHAR(A.DENTRY,'YYYY/MM/DD') AS TO_DENTRY") '会員登録日
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYY/MM/DD') AS TO_DMEMBER") '会員期限
            sql.Append(",B.ZANKN")
            sql.Append(",B.PREZANKN")
            sql.Append(",C.SRTPO")
            sql.Append(",D.CKBNAME")
            sql.Append(",E.SCLKBN")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS D ON D.NKBNO = A.NCSRANK")
            sql.Append(" LEFT JOIN MANMST AS E ON E.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" WHERE")
            sql.Append(" NCARDID = " & CType(_dcICR700.NCSNO, Integer))

            _dtCSMAST = _iDatabase.ExecuteRead(sql.ToString())

            If _dtCSMAST.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 月間来場ポイント
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetETPMTA() As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then Return 0

            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" ETPMTA")
            sql.Append(" WHERE")
            sql.Append(" NKBNO = " & _dtCSMAST.Rows(0).Item("NCSRANK").ToString)
            sql.Append(" AND ENTCNT >=" & (CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer) + 1))
            sql.Append(" ORDER BY ENTCNT")

            Dim dtETPMTA As DataTable = _iDatabase.ExecuteRead(sql.ToString)

            If dtETPMTA.Rows.Count.Equals(0) Then
                Return 0
            Else
                If CType(dtETPMTA.Rows(0).Item("ENTCNT").ToString, Integer).Equals(CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer) + 1) Then
                    If _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals(Now.ToString("yyyyMMdd")) Then
                        'If _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) Then
                        Return 0
                    End If

                    _CheckResult.M_ENTCNT = CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer) + 1
                    _CheckResult.M_ENTCNTPO = CType(dtETPMTA.Rows(0).Item("POINT").ToString, Integer)

                    'Using frm As New frmMSGBOXEx03(msg, 4)
                    '    frm.ShowDialog()
                    'End Using

                    'Me.lblETPMSG.Text = "月間" & (CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer) + 1).ToString("#,##0") & "回目の来場です。" _
                    '                & CType(dtETPMTA.Rows(0).Item("POINT").ToString, Integer).ToString("#,##0") & "ポイント付きます！"
                    'Me.lblETPMSG.Visible = True
                    Return CType(dtETPMTA.Rows(0).Item("POINT").ToString, Integer)
                Else
                    Return 0
                End If


            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' チェックイン
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckIn(ByRef strMsg As String) As Boolean
        Dim intAftKINGAKU As Integer = 0
        Dim intTAX As Integer = 0
        Dim strSERIALNO As String = "FFF"       'シリアルナンバー
        Dim strSltNKBNO As String = String.Empty
        Try

            '残高チェック
            If CType(_dcICR700.KINGAKU, Integer) < _intENTKIN Then
                'strMsg = "カード残金が不足しています。"
                strMsg = String.Empty
                CardEject()
                Return False
            End If

            _intSeatNo = 0
            If UIUtility.SYSTEM.SITEIKBN.Equals(1) And _intFLRKB.Equals(0) Then
                '打席指定あり
                Dim blnCancel As Boolean = False
                Using frm As New frmSEATINFOEx()
                    frm.iDatabase = _iDatabase
                    frm.ShowDialog()
                    blnCancel = frm.CANCEL
                    _intSeatNo = frm.SELECTSEAT
                End Using

                If blnCancel Then
                    strMsg = String.Empty
                    CardEject()
                    Me.timClearInfo.Start()
                    Return False
                End If
                If _intSeatNo.Equals(100) Then
                    '【100番打席】
                    strSERIALNO = "00F"
                Else
                    strSERIALNO = _intSeatNo.ToString.PadLeft(2, "0"c) & "F"
                End If
            End If
            'フロア指定
            Select Case _intFLRKB
                Case 1
                    strSERIALNO = "0A"
                Case 2
                    strSERIALNO = "0B"
                Case 3
                    strSERIALNO = "0C"
                Case Else
                    If _intPlaySelect.Equals(1) Then
                        strSERIALNO = "FFF"
                    Else
                        strSERIALNO = "FF"
                    End If
            End Select

            intAftKINGAKU = CType(_dcICR700.KINGAKU, Integer) - _intENTKIN

            If _intENTKIN > 0 Then
                intTAX = UIFunction.CalcExcludedTax(_intENTKIN)
            End If

            '【残金整合処理】
            'ICカード残金
            Dim intIcZANKN As Integer = CType(_dcICR700.ZANKN, Integer)
            'ICカード残プレミアム
            Dim intIcPREZANKN As Integer = CType(_dcICR700.PREZANKN, Integer)
            Dim intUseKINGAKU As Integer = 0

            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0

            If intIcPREZANKN >= _intENTKIN Then
                '【残プレミアム >= 入場料金】
                intIcPREZANKN -= _intENTKIN
                'プレミアムから支払った金額
                intPayPREMKN = _intENTKIN
            Else
                intIcZANKN = intIcZANKN - (_intENTKIN - intIcPREZANKN)
                '残金から支払った金額
                intPayKINGAKU = (_intENTKIN - intIcPREZANKN)
                'プレミアムから支払った金額
                intPayPREMKN = intIcPREZANKN
                intIcPREZANKN = 0
            End If

            '書き込む前の前回来場セット(ボールトラン更新用)
            Dim strZENENTDATE As String = _dcICR700.ZENENTDATE
            If _dcICR700.ZENENTDATE.Equals("00000000") Then
                strZENENTDATE = UIUtility.SYSTEM.UPDDAY
            End If

            '【ICカードセクタ２書き込み情報セット】
            '店番号
            _dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            _dcICR700.PASSCD_WR = UIUtility.SYSTEM.NOWPASSCD
            If _intPlaySelect.Equals(1) Then
                'シリアルナンバー
                _dcICR700.SERIALNO_WR = strSERIALNO
                '種別
                If _dtCSMAST.Rows(0).Item("NCSRANK").ToString.Equals("10") Then
                    _dcICR700.SYUBETU_WR = "A"
                    strSltNKBNO = "A"
                Else
                    _dcICR700.SYUBETU_WR = _dtCSMAST.Rows(0).Item("NCSRANK").ToString
                    strSltNKBNO = _dtCSMAST.Rows(0).Item("NCSRANK").ToString
                End If
                '予備
                _dcICR700.YOBI_WR = "F"
            Else
                'シリアルナンバー
                _dcICR700.SERIALNO_WR = strSERIALNO & Convert.ToString(_intJKNMM, 16).ToString.PadLeft(2, "0"c).Substring(0, 1).ToUpper
                '種別
                _dcICR700.SYUBETU_WR = "C"
                '予備
                _dcICR700.YOBI_WR = Convert.ToString(_intJKNMM, 16).ToString.PadLeft(2, "0"c).Substring(1, 1).ToUpper
                strSltNKBNO = _intJKNKB.ToString
            End If

            '金額
            _dcICR700.KINGAKU_WR = intAftKINGAKU.ToString.PadLeft(5, "0"c)


            '【ICカードセクタ３、４書き込み情報セット】
            '店番
            _dcICR700.SHOPNO_WR = _dcICR700.SHOPNO
            'カード区分
            _dcICR700.CARDKBN_WR = _dcICR700.CARDKBN
            'カード番号
            _dcICR700.CARDNO_WR = _dcICR700.CARDNO
            '顧客番号
            _dcICR700.NCSNO_WR = _dcICR700.NCSNO
            'スクール生番号
            _dcICR700.SCLMANNO_WR = _dcICR700.SCLMANNO
            '顧客種別
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then
                _dcICR700.NKBNO_WR = _dcICR700.NKBNO
            Else
                If _dtCSMAST.Rows(0).Item("NCSRANK").ToString.Equals("10") Then
                    _dcICR700.NKBNO_WR = "A"
                Else
                    _dcICR700.NKBNO_WR = _dtCSMAST.Rows(0).Item("NCSRANK").ToString
                End If
            End If
            '会員期限
            _dcICR700.DMEMBER_WR = _dcICR700.DMEMBER
            'パスワード
            _dcICR700.PASSCD_WR = UIUtility.SYSTEM.NOWPASSCD
            '残金額
            _dcICR700.ZANKN_WR = intIcZANKN.ToString.PadLeft(5, "0"c)
            'P残金額
            _dcICR700.PREZANKN_WR = intIcPREZANKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            _dcICR700.POINT_WR = (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + _intGetPOINT).ToString.PadLeft(5, "0"c)
            '前回来場日
            _dcICR700.ZENENTDATE_WR = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            '入場区分
#If DEBUG_CHECKIN Then
            _dcICR700.ENTKBN_WR = "0"
#Else
            _dcICR700.ENTKBN_WR = "0"
#End If
            'ボール単価
            _dcICR700.BALLKIN_WR = "00"
            ' 打席番号
            _dcICR700.SEATNO_WR = "FFF"
            If UIUtility.SYSTEM.SITEIKBN.Equals(1) Then
                _dcICR700.SEATNO_WR = _intSeatNo.ToString.PadLeft(3, "0"c)
            End If

            'トランザクション開始
            _iDatabase.BeginTransaction()

            Dim intDelDENNO As Integer = 0

            '*** 【データベース更新処理】***'
            If Not UpdDatabase(strSltNKBNO, intIcZANKN, intIcPREZANKN, _intGetPOINT, _intENTKIN, intTAX, intPayKINGAKU, intPayPREMKN, strMsg, intDelDENNO) Then
                Return False
            End If

            'コミット
            _iDatabase.Commit()

            _iDatabase.BeginTransaction()

            Dim sql As New System.Text.StringBuilder
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) < 50000000 Then
                '【顧客情報更新】
                sql.Clear()
                sql.Append("UPDATE CSMAST SET")
                sql.Append(" ZENENTDATE = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c) & "'")
                sql.Append(",ENTDT = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c) & "'")
                If Not _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString().Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                    'If Not _dcICR700.ZENENTDATE.Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                    '【本日初来場の場合】
                    sql.Append(",ENTCNT = ENTCNT + 1")
                    sql.Append(",ENTCNT2 = ENTCNT2 + 1")
                End If
                'カード有効期限
                If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                    Dim dtCARDLIMIT As DateTime = Now
                    sql.Append(",CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
                Else
                    sql.Append(",CARDLIMIT = NULL")
                End If

                sql.Append(",DUPDATE = NOW()")
                sql.Append(" WHERE")
                sql.Append(" NCSNO = " & CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "顧客情報の更新に失敗しました。"
                    _iDatabase.RollBack()
                    DelEntInfo(intDelDENNO, strZENENTDATE, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Return False
                End If

                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & intIcZANKN)
                sql.Append(",PREZANKN = " & intIcPREZANKN)
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    _iDatabase.RollBack()
                    DelEntInfo(intDelDENNO, strZENENTDATE, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Return False
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + _intGetPOINT))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "ポイントサマリの更新に失敗しました。"
                    _iDatabase.RollBack()
                    DelEntInfo(intDelDENNO, strZENENTDATE, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Return False
                End If
            End If


            '【カード書き込み】
            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                frm.CHECKIN = True  'ﾁｪｯｸｲﾝ
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.WRITE
                frm.EJECTSTOP = CommonSettings.IsCheckResultEjectStopMode
                frm.REQUEST_SKIP = True
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                _iDatabase.RollBack()
                strMsg = "カードの書き込みに失敗しました。"
                DelEntInfo(intDelDENNO, strZENENTDATE, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Return False
            End If

            'コミット
            _iDatabase.Commit()

            ' 結果画面用の設定
            Dim user = New UserInfo
            user.NCSNO = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
            user.ZANKN = intAftKINGAKU
            user.SRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + _intGetPOINT
            user.ENTKBN = "1"
            _CheckResult.USER_INFO = user

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' データベース更新処理
    ''' </summary>
    ''' <param name="strSltNKBNO">選択された種別</param>
    ''' <param name="intZANKN">残金額</param>
    ''' <param name="intPREZANKN">残プレミアム</param>
    ''' <param name="intGetPOINT">取得ﾎﾟｲﾝﾄ</param>
    ''' <param name="intENTKIN">入場料金(打ち放題料金)</param>
    ''' <param name="intTAX">消費税</param>
    ''' <param name="intPayKINGAKU">支払った残金額</param>
    ''' <param name="intPayPREMKN">支払ったプレミアム額</param>
    ''' <param name="strMsg">エラーメッセージ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdDatabase(ByVal strSltNKBNO As String, ByVal intZANKN As Integer, ByVal intPREZANKN As Integer, ByVal intGetPOINT As Integer _
                                 , ByVal intENTKIN As Integer, ByVal intTAX As Integer, ByVal intPayKINGAKU As Integer, ByVal intPayPREMKN As Integer, ByRef strMsg As String, ByRef intDENNO As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try

            '【伝票番号】
            sql.Clear()
            sql.Append("UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                strMsg = "伝票番号の更新に失敗しました。"
                _iDatabase.RollBack()
                Return False
            End If
            '伝票番号取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" DENNOSEQ AS DENNO")
            sql.Append(" FROM SEQTRN")

            Dim dtSEQTRN As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If dtSEQTRN.Rows.Count.Equals(0) Then
                strMsg = "伝票番号の取得に失敗しました。"
                _iDatabase.RollBack()
                Return False
            End If

            Dim dtmInsDt As DateTime = Now
            '【売上トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO DUDNTRN("
            strSQL2 &= " VALUES("
            '削除区分
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '売上日付
            strSQL1 &= "UDNDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '売上番号(伝票番号)
            strSQL1 &= "UDNNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '部門コード【001】スクール【002】ベンダー
            strSQL1 &= "BMNCD,"
            strSQL2 &= "'002',"
            '分類コード１【005】入場料
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'005',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分 入金区分(入金マスタの区分)
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "1,"
            '売上金額(入場料)
            strSQL1 &= "UDNKN,"
            strSQL2 &= intENTKIN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '顧客種別
            strSQL1 &= "KSBKB,"
            strSQL2 &= "'" & strSltNKBNO & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            End If
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            End If
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= (intENTKIN - intTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intENTKIN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intGetPOINT & ","
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= (intENTKIN + intTAX) & ","
            'おつり
            strSQL1 &= "TURIKN,"
            strSQL2 &= "0,"
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'2'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込【打席カード】
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'4',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "入場料 " & intENTKIN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPayPREMKN & ","
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= "NULL,"
            'スタッフ名
            strSQL1 &= "STFNAME,"
            strSQL2 &= "NULL,"
            ''カード期限
            strSQL1 &= "CARDLIMIT)"
            strSQL2 &= "NULL)"
            If Not _iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "売上トランの更新に失敗しました。"
                _iDatabase.RollBack()
                Return False
            End If
            '【伝票トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO DENTRA("
            strSQL2 &= " VALUES("
            '削除区分【1】使用中【9】削除
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '伝票日付
            strSQL1 &= "UDNDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '伝票番号
            strSQL1 &= "UDNNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '伝票行番号
            strSQL1 &= "LINNO,"
            strSQL2 &= "1,"
            '部門コード【001】スクール【002】ベンダー
            strSQL1 &= "BMNCD,"
            strSQL2 &= "'002',"
            '分類コード１【005】入場料
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'005',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'1',"
            '売上名
            strSQL1 &= "TKTNMA,"
            strSQL2 &= "'" & "入場料 " & intENTKIN.ToString("#,##0") & "円" & "',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= intENTKIN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '顧客種別
            strSQL1 &= "KSBKB,"
            strSQL2 &= "'" & strSltNKBNO & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            End If
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            End If
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= (intENTKIN - intTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intENTKIN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intGetPOINT & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'2'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込【4】打席カード
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'4',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "入場料 " & intENTKIN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= intPayPREMKN & ")"
            If Not _iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "伝票トランの更新に失敗しました。"
                _iDatabase.RollBack()
                Return False
            End If
            '【入場トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO ENTTRA("
            strSQL2 &= " VALUES("
            '削除区分【1】使用中【9】削除
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '伝票日付
            strSQL1 &= "ENTDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '伝票番号
            strSQL1 &= "ENTNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '顧客種別区分
            strSQL1 &= "KSBKB,"
            Select Case strSltNKBNO
                Case "A" : strSQL2 &= "'10',"
                Case "B" : strSQL2 &= "'11',"
                Case "C" : strSQL2 &= "'12',"
                Case "D" : strSQL2 &= "'13',"
                Case "E" : strSQL2 &= "'14',"
                Case "F" : strSQL2 &= "'15',"
                Case Else : strSQL2 &= "'" & strSltNKBNO & "',"
            End Select

            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '営業種別区分【1】1球貸し【2】打ち放題
            strSQL1 &= "EIGKB,"
            strSQL2 &= "'" & _intPlaySelect & "',"
            '料金体系
            strSQL1 &= "RKNKB,"
            strSQL2 &= "'" & UIUtility.SYSTEM.RKNKB & "',"
            '時間帯コード
            strSQL1 &= "TIMCD,"
            strSQL2 &= "'" & UIUtility.SYSTEM.NOWTIMEKB & "',"
            '時間帯
            strSQL1 &= "TIMTM,"
            strSQL2 &= "NULL,"
            'パスワード
            strSQL1 &= "PASSNO,"
            strSQL2 &= "'" & UIUtility.SYSTEM.NOWPASSCD & "',"
            '入場料
            strSQL1 &= "ENTKN,"
            strSQL2 &= intENTKIN & ","
            '入場料(税抜)
            strSQL1 &= "ENTAKN,"
            strSQL2 &= (intENTKIN - intTAX) & ","
            '入場料(税込)
            strSQL1 &= "ENTBKN,"
            strSQL2 &= intENTKIN & ","
            '消費税区分
            strSQL1 &= "ENTZEIKB,"
            strSQL2 &= "2,"
            '消費税
            strSQL1 &= "ENTZEI,"
            strSQL2 &= intTAX & ","
            '入場料備考１
            strSQL1 &= "ENTCMA,"
            strSQL2 &= "NULL,"
            '入場料備考２
            strSQL1 &= "ENTCMB,"
            strSQL2 &= "NULL,"
            'ポイント
            strSQL1 &= "SRTPO,"
            strSQL2 &= intGetPOINT & ","
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '前回来場日
            strSQL1 &= "ZENENTDT,"
            If CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) >= 50000000 Then
                If _dcICR700.ZENENTDATE.Equals("00000000") Then
                    strSQL2 &= "NULL,"
                Else
                    strSQL2 &= "'" & _dcICR700.ZENENTDATE & "',"
                End If
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString & "',"
            End If
            '前回顧客種別
            strSQL1 &= "ZENKSBKB,"
            strSQL2 &= "NULL,"
            '残金から支払った金額
            strSQL1 &= "ZENKIN,"
            strSQL2 &= intPayKINGAKU & ","
            '残プレミアムから支払った金額
            strSQL1 &= "ZENPKIN,"
            strSQL2 &= intPayPREMKN & ","
            '月間来場ポイント
            strSQL1 &= "ETPPO,"
            strSQL2 &= _intETPPO & ","
            '誕生日月ポイント
            strSQL1 &= "BIRTHMPO,"
            strSQL2 &= _intBIRTHMPO & ","
            '誕生日ﾎﾟｲﾝﾄ
            strSQL1 &= "BIRTHDPO,"
            strSQL2 &= _intBIRTHDPO & ","
            '退場ポイント
            strSQL1 &= "OUTPO,"
            strSQL2 &= "0,"
            '退場フラグ
            strSQL1 &= "OUTFLG,"
            strSQL2 &= "0,"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '更新日時
            strSQL1 &= "UPDDTM,"
            strSQL2 &= "NOW(),"
            '残金額処理前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= (intZANKN + intPayKINGAKU) & ","
            '残金額処理後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intZANKN & ","
            'P)残金額処理前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= (intPREZANKN + intPayPREMKN) & ","
            'P)残金額処理後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= intPREZANKN & ","
            '残ポイント処理前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) & ","
            '残ポイント処理後
            strSQL1 &= "ZANBPO)"
            strSQL2 &= CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intGetPOINT & ")"
            If Not _iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "入場トランの更新に失敗しました。"
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            _iDatabase.RollBack()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 伝票番号クリア処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClrDENNO() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(",TO_CHAR(UPDDTM,'YYYYMMDD') AS UPDDAY")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            If resultDt.Rows(0).Item("UPDDAY").Equals(UIUtility.SYSTEM.UPDDAY) Then
                '伝票番号クリア済み
                Return True
            End If

            sql.Clear()
            sql.Append("UPDATE SEQTRN SET")
            sql.Append(" DENNOSEQ = 0")
            sql.Append(",UPDDTM = NOW()")
            If Not (_iDatabase.ExecuteUpdate(sql.ToString)) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 帳票用入金機履歴テーブル作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsREPOCHARGE_M() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Dim dr As DataRow()
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM REPOCHARGE_M")
            sql.Append(" WHERE CHARGEDAY = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND HOSTNAME = '" & Net.Dns.GetHostName & "'")
            sql.Append(" AND NKNKBN = 1")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                _iDatabase.BeginTransaction()

                strSQL01 = "INSERT INTO REPOCHARGE_M ("
                strSQL02 = "VALUES ("

                '日付
                strSQL01 &= "CHARGEDAY, "
                strSQL02 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
                'ホスト名
                strSQL01 &= "HOSTNAME, "
                strSQL02 &= "'" & Net.Dns.GetHostName & "',"
                '入金機区分
                strSQL01 &= "NKNKBN, "
                strSQL02 &= "1,"
                'ｶｰﾄﾞ発行回数
                strSQL01 &= "HAKKOKAISU,"
                strSQL02 &= "0,"
                'ｶｰﾄﾞ発行合計金額
                strSQL01 &= "HAKKOGOKEIKIN,"
                strSQL02 &= "0,"
                '入金ボタン１金額
                strSQL01 &= "CHARGE1KIN,"
                strSQL02 &= "1000,"
                '入金ボタン１回数
                strSQL01 &= "CHARGE1KAISU,"
                strSQL02 &= "0,"
                '入金ボタン１合計金額
                strSQL01 &= "CHARGE1GOKEIKIN,"
                strSQL02 &= "0,"
                '入金ボタン２金額
                strSQL01 &= "CHARGE2KIN,"
                strSQL02 &= "0,"
                '入金ボタン２回数
                strSQL01 &= "CHARGE2KAISU,"
                strSQL02 &= "0,"
                '入金ボタン２合計金額
                strSQL01 &= "CHARGE2GOKEIKIN,"
                strSQL02 &= "0,"
                '入金ボタン３金額
                strSQL01 &= "CHARGE3KIN,"
                strSQL02 &= "0,"
                '入金ボタン３回数
                strSQL01 &= "CHARGE3KAISU,"
                strSQL02 &= "0,"
                '入金ボタン３合計金額
                strSQL01 &= "CHARGE3GOKEIKIN,"
                strSQL02 &= "0,"
                '入金ボタン４金額
                strSQL01 &= "CHARGE4KIN,"
                strSQL02 &= "0,"
                '入金ボタン４回数
                strSQL01 &= "CHARGE4KAISU,"
                strSQL02 &= "0,"
                '入金ボタン４合計金額
                strSQL01 &= "CHARGE4GOKEIKIN,"
                strSQL02 &= "0,"
                '入金ボタン５金額
                strSQL01 &= "CHARGE5KIN,"
                strSQL02 &= "0,"
                '入金ボタン５回数
                strSQL01 &= "CHARGE5KAISU,"
                strSQL02 &= "0,"
                '入金ボタン５合計金額
                strSQL01 &= "CHARGE5GOKEIKIN,"
                strSQL02 &= "0,"
                '入金ボタン６金額
                strSQL01 &= "CHARGE6KIN,"
                strSQL02 &= "0,"
                '入金ボタン６回数
                strSQL01 &= "CHARGE6KAISU,"
                strSQL02 &= "0,"
                '入金ボタン６合計金額
                strSQL01 &= "CHARGE6GOKEIKIN,"
                strSQL02 &= "0,"
                '千円投入回数
                strSQL01 &= "SENENINKAISU,"
                strSQL02 &= "0,"
                '二千円投入回数
                strSQL01 &= "NISENENINKAISU,"
                strSQL02 &= "0,"
                '五千円投入回数
                strSQL01 &= "GOSENENINKAISU,"
                strSQL02 &= "0,"
                '一万円投入回数
                strSQL01 &= "ICHIMANENINKAISU,"
                strSQL02 &= "0,"
                '千円払出回数
                strSQL01 &= "SENENOUTKAISU,"
                strSQL02 &= "0,"
                '五千円払出回数
                strSQL01 &= "GOSENENOUTKAISU,"
                strSQL02 &= "0,"
                '十円投入回数
                strSQL01 &= "JYUENINKAISU,"
                strSQL02 &= "0,"
                '五十円投入回数
                strSQL01 &= "GOJYUENINKAISU,"
                strSQL02 &= "0,"
                '百円投入回数
                strSQL01 &= "HYAKUENINKAISU,"
                strSQL02 &= "0,"
                '五百円投入回数
                strSQL01 &= "GOHYAKUENINKAISU,"
                strSQL02 &= "0,"
                '十円払出回数
                strSQL01 &= "JYUENOUTKAISU,"
                strSQL02 &= "0,"
                '五十円払出回数
                strSQL01 &= "GOJYUENOUTKAISU,"
                strSQL02 &= "0,"
                '百円払出回数
                strSQL01 &= "HYAKUENOUTKAISU,"
                strSQL02 &= "0,"
                '五百円払出回数
                strSQL01 &= "GOHYAKUENOUTKAISU,"
                strSQL02 &= "0,"
                '作成日時
                strSQL01 &= "INSDTM,"
                strSQL02 &= "NOW(),"
                '更新日時
                strSQL01 &= "UPDDTM"
                strSQL02 &= "NOW()"

                strSQL01 &= ") "
                strSQL02 &= ")"

                Try
                    If Not (_iDatabase.ExecuteUpdate(strSQL01 & strSQL02)) Then
                        _iDatabase.RollBack()
                        Return False
                    End If
                Catch ex As Exception
                    _iDatabase.RollBack()
                    Return False
                End Try


                _iDatabase.Commit()

                Return True
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function





    Private Function GetTable(Optional ByVal intENTKBN As Integer = 0) As Boolean
        Dim dr As DataRow()
        Dim intNKBNO As Integer = 0                 '顧客種別コード
        Dim intENTPO As Integer = 0                 '入場ポイント

        _intCLRKBN = 99

        'システム情報取得
        If Not GetSYSMTA() Then
            Using frm As New frmMSGBOXEx("システム情報が見つかりませんでした。", 3)
                frm.ShowDialog()
            End Using
            Return False
        End If

        '本日料金体系情報取得
        If Not GetRknInfo() Then
            Using frm As New frmMSGBOXEx("料金体系情報の取得に失敗しました。", 2)
                frm.ShowDialog()
            End Using
            Return False
        End If

        '営業情報マスタ取得
        If Not GetEIGMT() Then
            Using frm As New frmMSGBOXEx("営業情報マスタの取得に失敗しました。", 2)
                frm.ShowDialog()
            End Using
            Return False
        End If

        ' テロップ・コメントの取得
        UIFunction.GetDTELOP(_iDatabase)

        '顧客種別情報マスタ取得
        If Not GetKBMAST() Then
            Using frm As New frmMSGBOXEx("顧客種別マスタの取得に失敗しました。", 2)
                frm.ShowDialog()
            End Using
            Return False
        End If

        '顧客情報取得
        If Not GetCSMAST() Then
            Using frm As New frmMSGBOXEx("顧客情報がありません。", 2)
                frm.ShowDialog()
            End Using
            Return False
        End If

        '現在の時間帯確認
        dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB)

        UIUtility.SYSTEM.NOWTIMEKB = 0
        For i As Integer = 0 To dr.Length - 1
            If dr(i).Item("TIMENM").ToString >= Now.ToString("HHmm") Then
                UIUtility.SYSTEM.NOWTIMEKB = CType(dr(i).Item("TIMEKB").ToString, Integer)
                Exit For
            End If
        Next
        If UIUtility.SYSTEM.NOWTIMEKB.Equals(0) Then
            UIUtility.SYSTEM.NOWTIMEKB = 1
        End If

        '単価情報取得
        intNKBNO = CType(_dtCSMAST.Rows(0).Item("NCSRANK").ToString, Integer)

        dr = UIUtility.TABLE.EIGMT.Select("NKBNO = " & intNKBNO & " AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
        If dr.Length > 0 Then
            '入場料
            '入場ポイント
            intENTPO = CType(dr(0).Item("POINT"), Integer)
            'ﾚﾃﾞｨｰｽ
            If _dtCSMAST.Rows(0).Item("NSEX").ToString.Equals("2") Then
                intENTPO += CType(dr(0).Item("POINTW").ToString, Integer)
            End If
            'ｼﾆｱ
            Dim intBirthMM2 As Integer = 0
            If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString) Then
                intBirthMM2 = CType(_dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString.Replace("/", String.Empty).Substring(4, 2), Integer)
                Dim intAge As Integer = 0
                intAge = UIFunction.GetAge(DateTime.Parse(_dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString), Now)
                If intAge >= 70 Then
                    intENTPO += CType(dr(0).Item("POINTS").ToString, Integer)
                End If
            End If
            '本日コース来場回数確認
            If UIFunction.GetKosuEnt(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), _iDatabase) > 0 Then
                intENTPO = 0
            End If
            If _dcICR700.ZENENTDATE.Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                '【再入場】
                _intENTKIN = 0
                intENTPO = 0
                _intWARIBIKI = CType(dr(0).Item("ENTKIN").ToString, Integer)
            Else
                _intENTKIN = CType(dr(0).Item("ENTKIN").ToString, Integer)
                _intWARIBIKI = 0
            End If

            'パスワード
            UIUtility.SYSTEM.NOWPASSCD = dr(0).Item("PASSCD").ToString

            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //

                'ボール単価
                _intBALLKIN1F = CType(dr(0).Item("BALLKIN1F").ToString, Integer)
                _intBALLKIN2F = CType(dr(0).Item("BALLKIN2F").ToString, Integer)
                '打席指定区分
                UIUtility.SYSTEM.SITEIKBN = CType(dr(0).Item("SITEIKBN").ToString, Integer)
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                '打席指定区分
                UIUtility.SYSTEM.SITEIKBN = 0
            End If
            '顧客種別
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & intNKBNO)
            If dr.Length > 0 Then _strCKBNAME = dr(0).Item("CKBNAME").ToString
        End If

        _intGetPOINT = 0
        If UIFunction.ChkDCSTPTRN(_dcICR700.CARDNO, _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), _iDatabase) Then
            Using frm As New frmMSGBOXEx("カード停止中です。", 2)
                frm.ShowDialog()
            End Using
            Return False
        End If

        '会員期限チェック
        Dim strDMEMBER As String = String.Empty
        Dim strToday As String = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
        If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString) Then
            Dim dtDMEMBER As DateTime = DateTime.Parse(_dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString)
            ' 1ヶ月減算する
            dtDMEMBER = dtDMEMBER.AddMonths(-1)
            strDMEMBER = _dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString.Replace("/", String.Empty)
            If strDMEMBER < strToday Then
                Using frm As New frmMSGBOXEx("会員期限が切れています。", 2)
                    frm.ShowDialog()
                End Using
                Return False
            End If
        End If
        '*** カード有効期限チェック ***'
        If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
            Dim blnReply As Boolean = False
            If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString) Then
                Dim dtCARDLIMIT As DateTime = DateTime.Parse(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(6, 2))
                ' 入金残高有効期限
                Dim intPREMLIMIT As Integer = 0
                Dim strCARDLIMIT As String = dtCARDLIMIT.ToString("yyyy/MM/dd")
                If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                    intPREMLIMIT = CType("-" & UIUtility.SYSTEM.PREMLIMIT, Integer)
                    dtCARDLIMIT = dtCARDLIMIT.AddMonths(intPREMLIMIT)
                    strCARDLIMIT = dtCARDLIMIT.ToString("yyyy/MM/dd")
                End If
                If Now.ToString("yyyyMMdd") > _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString Then
                    _intCLRKBN = 1
                    If _intZANKIN_CLEARE.Equals(0) Then
                        Using frm As New frmMSGBOXEx("カードの有効期限切れです。" & vbCrLf & "フロントへお越しください。", 2)
                            frm.ShowDialog()
                        End Using
                    Else
                        Using frm As New frmMSGBOXEx("カードの有効期限切れです。" & vbCrLf & "残金額をクリアします。", 2)
                            frm.ShowDialog()
                        End Using
                    End If
                    Return False
                ElseIf Now.ToString("yyyy/MM/dd") > strCARDLIMIT And Not String.IsNullOrEmpty(strCARDLIMIT) Then
                    _intCLRKBN = 0
                    Using frm As New frmMSGBOXEx("残金額の有効期限切れです。" & vbCrLf & "残金額をﾌﾟﾚﾐｱﾑに移行します。", 2)
                        frm.ShowDialog()
                    End Using
                    Return False
                End If
            End If
        End If
        'ポイント
        '月間来場ポイント取得
        _intETPPO = GetETPMTA()
        'ポイントマスタ情報取得
        If Not Not UIFunction.GetPOINTMST(_intBIRTHMPO, _intBIRTHDPO, _iDatabase) Then
        End If
        '誕生日確認
        Dim strBirthday As String = _dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString.Replace("/", String.Empty)
        If String.IsNullOrEmpty(strBirthday) Then
            _intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
            _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
        Else
            If strBirthday.Substring(4, 4).Equals(Now.ToString("MMdd")) Then
                '【本日誕生日】

            ElseIf strBirthday.Substring(4, 2).Equals(Now.ToString("MM")) Then
                '【誕生月】

                _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
            Else
                _intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
                _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
            End If
            If _intBIRTHDPO > 0 Then
                'Dim msg = String.Format("{0}ポイントのお誕生日ボーナスを獲得しました。", _intBIRTHDPO.ToString("N0"))
                'Using frm As New frmMSGBOXEx03(msg, 4)
                '    frm.ShowDialog()
                'End Using

                'Me.lblBIRTHDMSG.Visible = True
                'Me.lblBIRTHDMSG.Text = "お誕生日です！" & _intBIRTHDPO.ToString("#,##0") & "ポイント付きます！"
            End If

            If _intBIRTHMPO > 0 Then
                'Dim msg = String.Format("{0}ポイントのお誕生月ボーナスを獲得しました。", _intBIRTHMPO.ToString("N0"))
                'Using frm As New frmMSGBOXEx03(msg, 4)
                '    frm.ShowDialog()
                'End Using

                'Me.lblBIRTHMMSG.Visible = True
                'Me.lblBIRTHMMSG.Text = "お誕生月です！" & _intBIRTHMPO.ToString("#,##0") & "ポイント付きます！"
            End If
            If _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals(Now.ToString("yyyyMMdd")) Then
                '来場済みなので誕生日ポイントなし

                _intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
                _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
            End If
        End If
        '入場ﾎﾟｲﾝﾄ + 月間来場ﾎﾟｲﾝﾄ + 誕生月ﾎﾟｲﾝﾄ + 誕生日ﾎﾟｲﾝﾄ
        _intGetPOINT = intENTPO + _intETPPO + _intBIRTHMPO + _intBIRTHDPO
        If Not _dcICR700.ZENENTDATE.Equals("00000000") Then
            If _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) Then
                '本日来場済みなので0ポイント
                _intGetPOINT = 0
                _intETPPO = 0
                _intBIRTHMPO = 0
                _intBIRTHDPO = 0
            End If
        End If


        ' チェックイン情報の登録
        SetCheckInResult(_intENTKIN, _intETPPO, _intGetPOINT, intENTPO, _intBIRTHDPO, _intBIRTHMPO)

        Return True

    End Function

    ''' <summary>
    ''' チェックイン情報の登録
    ''' </summary>
    ''' <param name="intENTKIN"></param>
    ''' <param name="intETPPO"></param>
    ''' <param name="intGetPOINT"></param>
    ''' <param name="intENTPO"></param>
    ''' <param name="intBIRTHDPO"></param>
    ''' <param name="intBIRTHMPO"></param>
    ''' <remarks></remarks>
    Public Sub SetCheckInResult(ByVal intENTKIN As Integer, ByVal intETPPO As Integer, _
                                ByVal intGetPOINT As Integer, ByVal intENTPO As Integer, _
                                ByVal intBIRTHDPO As Integer, ByVal intBIRTHMPO As Integer)
        ' 入場金額
        _CheckResult.ENTKIN = intENTKIN

        If intETPPO <= 0 Then
            _CheckResult.M_ENTCNT = 0
            _CheckResult.M_ENTCNTPO = 0
        End If
        If intGetPOINT > 0 Then
            _CheckResult.ENTPO = intENTPO
            _CheckResult.BIRTHDPO = intBIRTHDPO
            _CheckResult.BIRTHMPO = intBIRTHMPO
        Else
            _CheckResult.ENTPO = 0
            _CheckResult.OUTPO = 0
            _CheckResult.BIRTHMPO = 0
            _CheckResult.BIRTHDPO = 0
            _CheckResult.M_ENTCNT = 0
            _CheckResult.M_ENTCNTPO = 0
        End If
    End Sub

    ''' <summary>
    ''' ビルバリ状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckAD1()
        Dim blnError As Boolean = False
        Try
            _dcAD1.BvDetailData()

            '代表異常
            If _dcAD1.BvAbnormal1.Substring(7, 1).Equals("1") Then
                blnError = True
            End If
            '識別部異常
            If _dcAD1.BvAbnormal1.Substring(5, 1).Equals("1") Then
                blnError = True
            End If
            'スタッカー異常
            If _dcAD1.BvAbnormal1.Substring(4, 1).Equals("1") Then
                blnError = True
            End If
            '札詰まり
            If _dcAD1.BvAbnormal1.Substring(3, 1).Equals("1") Then
                blnError = True
            End If
            '紙幣払出異常
            If _dcAD1.BvAbnormal1.Substring(2, 1).Equals("1") Then
                blnError = True
            End If
            '金庫満杯
            If _dcAD1.BvAbnormal1.Substring(1, 1).Equals("1") Then
                blnError = True
            End If

            ''紙幣受け入れ
            'If _dcAD1.BvState1.Substring(7, 1).Equals("1") Then
            '    blnError = True
            'End If
            ''紙幣回収中
            'If _dcAD1.BvState1.Substring(6, 1).Equals("1") Then
            '    blnError = True
            'End If

            ''後続金無し
            'If _dcAD1.BvState1.Substring(5, 1).Equals("1") Then
            'End If
            ''払出終了
            'If _dcAD1.BvState1.Substring(4, 1).Equals("1") Then
            '    blnError = True
            'End If
            ''クリア済み
            'If _dcAD1.BvState1.Substring(3, 1).Equals("1") Then
            '    blnError = True
            'End If
            ''識別動作中
            'If _dcAD1.BvState1.Substring(2, 1).Equals("1") Then
            '    blnError = True
            'End If

            ''集金動作中
            'If _dcAD1.BvState1.Substring(1, 1).Equals("1") Then
            '    blnError = True
            'End If
            ''返金動作中
            'If _dcAD1.BvState1.Substring(0, 1).Equals("1") Then
            '    blnError = True
            'End If

            If blnError Then
                frmBase.ChangePictureBoxEnabled(Me.btnCharge, False)
                Me.lblAd1Error.Visible = True
            End If




        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード発券機状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckCardDispenser() As Boolean
        Try
            Me.lblCardDispenserError.Visible = False

            Dim result = True

            'Thread.Sleep(1000)

            ' 接続エラー
            If Not _dcMCH3000.IsOpen Then
                ' エラーあり
                Me.lblCardDispenserError.Text = "発券エラー"
                Me.lblCardDispenserError.Visible = True
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
                Return False
            End If

            ' ユニットステータスの取得
            Dim states = _dcMCH3000.SR_Command

            ' アイドル状態
            If states(0) <> 0 Then
                Me.lblCardDispenserError.Text = "発券エラー"
                result = False
            End If

            ' ユニット内にカードが無い
            If states(1) <> 0 Then
                Me.lblCardDispenserError.Text = "カード詰まり"
                result = False
            End If

            ' カセット内にカードがある
            If states(2) <> 1 Then
                Me.lblCardDispenserError.Text = "カード切れ"
                result = False
            End If

            If result Then
                Me.lblCardDispenserError.Visible = False
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, True)
            Else
                Me.lblCardDispenserError.Visible = True
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
            End If

            If Me.lblRWError.Visible Or Me.lblAd1Error.Visible Then
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
            End If

            Return result

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' カード発券機状態確認_STR610
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckCardDispenser_STR610() As Boolean
        Try
            Me.lblCardDispenserError.Visible = False

            Dim result = True
            Dim near_end = False

            ' 接続エラー
            If Not _dcSTR610.IsOpen Then
                ' エラーあり
                Me.lblCardDispenserError.Text = "発券エラー"
                Me.lblCardDispenserError.Visible = True
                'Me.btnCardDispenser.Enabled = False
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
                _dcSTR610.Close()
                Return False
            End If

            ' ユニットステータスの取得
            If Not _dcSTR610.GetUnitStatus() Then
                ' エラーあり
                result = False
            End If

            If result Then
                ' センサーステータスの取得
                If Not _dcSTR610.GetSensorStatus() Then
                    For Each msg In _dcSTR610.SensorMessages
                        If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.NSNS_ON Then
                            ' ニアエンド
                            near_end = True

                        ElseIf Not msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.NONE Then
                            ' それ以外は操作不可
                            result = False

                        End If
                    Next
                End If
            End If

            If result Then
                If near_end Then
                    ' ニアエンドはカード無しの通知のみを行う
                    Me.lblCardDispenserError.Text = "要カード補充"
                    Me.lblCardDispenserError.Visible = True
                Else
                    Me.lblCardDispenserError.Visible = False
                End If
                'Me.btnCardDispenser.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, True)
            Else
                Me.lblCardDispenserError.Text = "発券エラー"
                Me.lblCardDispenserError.Visible = True
                'Me.btnCardDispenser.Enabled = False
                frmBase.ChangePictureBoxEnabled(Me.btnCardDispenser, False)
                _dcSTR610.Close()
            End If

            Return result

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 売上トランに新規作成(カード発券機用)
    ''' </summary>
    ''' <param name="key_udndt"></param>
    ''' <param name="key_udnno"></param>
    ''' <param name="key_insdtm"></param>
    ''' <param name="key_insdtmstr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsDUDNTRN_ForCardDispenser(ByVal key_udndt As String, ByVal key_udnno As Integer, ByVal key_insdtm As Date, ByVal key_insdtmstr As String) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim datkb = "1"
            Dim bmncd = "002"
            Dim buncda = "010"
            Dim buncdb = "001"
            Dim buncdc = "001"
            Dim tktkbn = "001"
            Dim udnkn = 0
            Dim tktsu = 1
            Dim udnakn = 0
            Dim udnbkn = 0
            Dim udnzkn = 0
            Dim hinzeikb = "2"
            Dim point = 0
            Dim nyukn = 0
            Dim turikn = 0
            Dim koteikbn = "0"
            Dim udnkbn = "0"
            Dim cpaykbn = "0"
            Dim hostname = My.Computer.Name
            Dim drakbn = "0"
            Dim hinnma = "カード発券"
            Dim premkn = 0

            sql.Clear()
            sql.Append(" INSERT INTO DUDNTRN (")
            sql.Append(" DATKB, UDNDT, UDNNO, BMNCD, BUNCDA, BUNCDB, BUNCDC, TKTKBN, UDNKN, TKTSU,")
            sql.Append(" INSDTMSTR, INSDTM, UDNAKN, UDNBKN, UDNZKN, HINZEIKB, POINT, NYUKN, TURIKN,")
            sql.Append(" KOTEIKBN, UDNKBN, CPAYKBN, HOSTNAME, DRAKBN, HINNMA, PREMKN")
            sql.Append(" )")
            sql.Append(" VALUES(")
            sql.Append(" '" & datkb & "',")
            sql.Append(" '" & key_udndt & "',")
            sql.Append(" " & key_udnno & ",")
            sql.Append(" '" & bmncd & "',")
            sql.Append(" '" & buncda & "',")
            sql.Append(" '" & buncdb & "',")
            sql.Append(" '" & buncdc & "',")
            sql.Append(" '" & tktkbn & "',")
            sql.Append(" " & udnkn & ",")
            sql.Append(" " & tktsu & ",")
            sql.Append(" '" & key_insdtmstr & "',")
            sql.Append(" '" & key_insdtm & "',")
            sql.Append(" " & udnakn & ",")
            sql.Append(" " & udnbkn & ",")
            sql.Append(" " & udnzkn & ",")
            sql.Append(" '" & hinzeikb & "',")
            sql.Append(" " & point & ",")
            sql.Append(" " & nyukn & ",")
            sql.Append(" " & turikn & ",")
            sql.Append(" '" & koteikbn & "',")
            sql.Append(" '" & udnkbn & "',")
            sql.Append(" '" & cpaykbn & "',")
            sql.Append(" '" & hostname & "',")
            sql.Append(" '" & drakbn & "',")
            sql.Append(" '" & hinnma & "',")
            sql.Append(" " & premkn & " ")
            sql.Append(" )")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 伝票トランに新規作成(カード発券機用)
    ''' </summary>
    ''' <param name="key_udndt"></param>
    ''' <param name="key_udnno"></param>
    ''' <param name="key_insdtm"></param>
    ''' <param name="key_insdtmstr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsDENTRA_ForCardDispenser(ByVal key_udndt As String, ByVal key_udnno As Integer, ByVal key_insdtm As Date, ByVal key_insdtmstr As String) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim datkb = "1"
            Dim linno = 1
            Dim bmncd = "002"
            Dim buncda = "010"
            Dim buncdb = "001"
            Dim buncdc = "001"
            Dim tktkbn = "001"
            Dim tktnma = "カード発券"
            Dim udnkn = 0
            Dim tktsu = 0
            Dim udnakn = 0
            Dim udnbkn = 0
            Dim udnzkn = 0
            Dim hinzeikb = "2"
            Dim point = 0
            Dim koteikbn = "0"
            Dim udnkbn = "3"
            Dim cpaykbn = "0"
            Dim hostname = My.Computer.Name
            Dim drakbn = "0"
            Dim hinnma = "カード発券"
            Dim premkn = 0

            sql.Clear()
            sql.Append(" INSERT INTO DENTRA (")
            sql.Append(" DATKB, UDNDT, UDNNO, LINNO, BMNCD, BUNCDA, BUNCDB, BUNCDC, TKTKBN, TKTNMA, UDNKN, TKTSU,")
            sql.Append(" INSDTMSTR, INSDTM, UDNAKN, UDNBKN, UDNZKN, HINZEIKB, POINT,")
            sql.Append(" KOTEIKBN, UDNKBN, CPAYKBN, HOSTNAME, DRAKBN, HINNMA, PREMKN")
            sql.Append(" )")
            sql.Append(" VALUES(")
            sql.Append(" '" & datkb & "',")
            sql.Append(" '" & key_udndt & "',")
            sql.Append(" " & key_udnno & ",")
            sql.Append(" " & linno & ",")
            sql.Append(" '" & bmncd & "',")
            sql.Append(" '" & buncda & "',")
            sql.Append(" '" & buncdb & "',")
            sql.Append(" '" & buncdc & "',")
            sql.Append(" '" & tktkbn & "',")
            sql.Append(" '" & tktnma & "',")
            sql.Append(" " & udnkn & ",")
            sql.Append(" " & tktsu & ",")
            sql.Append(" '" & key_insdtmstr & "',")
            sql.Append(" '" & key_insdtm & "',")
            sql.Append(" " & udnakn & ",")
            sql.Append(" " & udnbkn & ",")
            sql.Append(" " & udnzkn & ",")
            sql.Append(" '" & hinzeikb & "',")
            sql.Append(" " & point & ",")
            sql.Append(" '" & koteikbn & "',")
            sql.Append(" '" & udnkbn & "',")
            sql.Append(" '" & cpaykbn & "',")
            sql.Append(" '" & hostname & "',")
            sql.Append(" '" & drakbn & "',")
            sql.Append(" '" & hinnma & "',")
            sql.Append(" " & premkn & " ")
            sql.Append(" )")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 帳票用入金機履歴更新(ｶｰﾄﾞ発行回数)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateREPOCHARGE_M(ByVal key_udndt As String) As Boolean
        Try
            Dim sql = New System.Text.StringBuilder

            sql.Clear()
            sql.Append("UPDATE REPOCHARGE_M SET HAKKOKAISU = HAKKOKAISU + 1 WHERE CHARGEDAY = '" & key_udndt & "' AND HOSTNAME = '" & My.Computer.Name & "'")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 伝票番号の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUDNNO() As Integer
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEQTRN")
            Dim dt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If Not dt.Rows.Count > 0 Then
                Return Nothing
            Else
                Return CInt(dt.AsEnumerable.Max(Function(x As DataRow) x("DENNOSEQ")))
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function



    ''' <summary>
    ''' SEQTRNの伝票番号の更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSEQTRN() As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function UpdateDUDNTRN_ForCardDispenser(ByVal udndt As String, ByVal udnno As Integer, ByVal insdtm As Date) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE DUDNTRN SET DATKB = '9'")
            sql.Append(" WHERE")
            sql.Append(" UDNDT = '" & udndt & "' AND")
            sql.Append(" UDNNO = " & udnno & " AND")
            sql.Append(" INSDTM = '" & insdtm.ToString & "' ")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function UpdateDENTRA_ForCardDispenser(ByVal udndt As String, ByVal udnno As Integer, ByVal insdtm As Date) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE DENTRA SET DATKB = '9'")
            sql.Append(" WHERE")
            sql.Append(" UDNDT = '" & udndt & "' AND")
            sql.Append(" UDNNO = " & udnno & " AND")
            sql.Append(" LINNO = " & 1 & " AND")
            sql.Append(" INSDTM = '" & insdtm.ToString & "' ")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 残高0円クリア
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClearZANKN(ByVal intCLRKBN As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intZANKN As Integer = 0
        Dim intPREMKN As Integer = 0
        Dim intSRTPO As Integer = 0
        Dim intClrZANKN As Integer = 0
        Dim intClrPREMKN As Integer = 0
        Dim intClrSRTPO As Integer = 0
        Try
            '*** 金額計算 ***'

            '使用球数金額
            Dim intUseKINGAKU As Integer = 0
            'プリカ金額
            Dim intKINGAKU As Integer = CType(_dcICR700.KINGAKU, Integer)
            'V31残金
            Dim intV31ZANKN As Integer = CType(_dcICR700.ZANKN, Integer)
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = CType(_dcICR700.PREZANKN, Integer)
            Dim intPayBallZANKN As Integer = 0
            Dim intPayBallPREMKN As Integer = 0
            Select Case _dcICR700.SYUBETU
                Case "B", "C", "D"
                    intUseKINGAKU = 0
                    intKINGAKU = (intV31ZANKN + intV31PREZANKN)
                Case Else
                    '【残金整合処理】
                    If Not UIFunction.ZANSEIGO(intV31ZANKN, intV31PREZANKN, CType(_dcICR700.KINGAKU, Integer), intUseKINGAKU, intPayBallZANKN, intPayBallPREMKN) Then
                        Return False
                    End If
            End Select

            '書き込む前の前回来場セット(ボールトラン更新用)
            Dim strZENENTDATE As String = _dcICR700.ZENENTDATE
            If _dcICR700.ZENENTDATE.Equals("00000000") Then
                strZENENTDATE = UIUtility.SYSTEM.UPDDAY
            End If
            '********************'
            If intCLRKBN.Equals(0) Then
                intClrZANKN = intV31ZANKN
                intClrPREMKN = intClrZANKN

                intZANKN = 0
                intPREMKN = intV31ZANKN + intV31PREZANKN
                intSRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            Else
                intClrZANKN = intV31ZANKN
                intClrPREMKN = intV31PREZANKN
                intClrSRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            End If

            _iDatabase.BeginTransaction()

            '【カード有効期限】
            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            If UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                sql.Append(" CARDLIMIT = NULL")
            Else
                Dim dtCARDLIMIT As DateTime = Now
                sql.Append(" CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
            End If

            sql.Append(",DUPDATE = NOW()")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            '【金額サマリ】
            sql.Clear()
            sql.Append("UPDATE KINSMA SET")
            sql.Append(" ZANKN =" & intZANKN)
            sql.Append(",PREZANKN = " & intPREMKN)
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If
            '【ポイントサマリ】
            sql.Clear()
            sql.Append("UPDATE DPOINTSMA SET")
            sql.Append(" SRTPO = " & intSRTPO)
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If
            Dim dtmInsDt As DateTime = Now
            '【金額クリアトラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO LKINTRA("
            strSQL2 &= " VALUES("
            '金額クリア番号
            strSQL1 &= "LKINNO,"
            strSQL2 &= "(SELECT CASE WHEN MAX(LKINNO) + 1 IS NULL THEN 1 ELSE (MAX(LKINNO) + 1) END FROM LKINTRA),"
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '顧客名
            strSQL1 &= "MANNM,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("CCSNAME").ToString & "',"
            '残金額
            strSQL1 &= "ZANKN,"
            strSQL2 &= intClrZANKN & ","
            'P)残金額
            strSQL1 &= "PREZANKN,"
            strSQL2 &= intClrPREMKN & ","
            '残ポイント
            strSQL1 &= "SRTPO,"
            strSQL2 &= intClrSRTPO & ","
            'カード期限
            strSQL1 &= "CARDLIMIT,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString & "',"
            'クリア区分
            strSQL1 &= "CLRKBN,"
            strSQL2 &= intCLRKBN & ","
            '残金額処理前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intV31ZANKN & ","
            'P)残金額処理前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intV31PREZANKN & ","
            '残ポイント処理前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= CType(_dcICR700.POINT, Integer) & ","
            If intCLRKBN.Equals(0) Then
                '残金額処理後
                strSQL1 &= "ZANBKN,"
                strSQL2 &= "0,"
                'P)残金額処理後
                strSQL1 &= "PREZANBKN,"
                strSQL2 &= intPREMKN & ","
                '残ポイント処理後
                strSQL1 &= "ZANBPO,"
                strSQL2 &= intSRTPO & ","
            Else
                '残金額処理後
                strSQL1 &= "ZANBKN,"
                strSQL2 &= "0,"

                'P)残金額処理後
                strSQL1 &= "PREZANBKN,"
                strSQL2 &= "0,"
                '残ポイント処理後
                strSQL1 &= "ZANBPO,"
                strSQL2 &= "0,"
            End If
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= "NULL,"
            'スタッフ名  
            strSQL1 &= "STFNAME,"
            strSQL2 &= "NULL,"
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"

            If Not _iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                _iDatabase.RollBack()
                Return False
            End If

            'ボールトラン更新
            Dim intZANAKN As Integer = 0
            Dim intZANBKN As Integer = 0
            Dim intPREZANAKN As Integer = 0
            Dim intPREZANBKN As Integer = 0
            Dim intZANAPO As Integer = 0
            Dim intZANBPO As Integer = 0

            intZANAKN = CType(_dcICR700.ZANKN, Integer)
            intZANBKN = CType(_dcICR700.ZANKN, Integer) - intPayBallZANKN
            intPREZANAKN = CType(_dcICR700.PREZANKN, Integer)
            intPREZANBKN = CType(_dcICR700.PREZANKN, Integer) - intPayBallPREMKN
            intZANAPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            intZANBPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)

            If Not intUseKINGAKU.Equals(0) Then
                If Not UIFunction.UpdBALLTRN(CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer), _dcICR700.NKBNO, strZENENTDATE, intUseKINGAKU, intPayBallZANKN, intPayBallPREMKN, CType(_dcICR700.BALLKIN, Integer) _
                            , intZANAKN, intZANBKN, intPREZANAKN, intPREZANBKN, intZANAPO, intZANBPO, _iDatabase) Then
                    _iDatabase.RollBack()
                    Return False
                End If
            End If

            '【プリカRW書き込み情報セット】
            '店番号
            _dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            _dcICR700.PASSCD_WR = _dcICR700.PASSCD
            'シリアルナンバー
            _dcICR700.SERIALNO_WR = _dcICR700.SERIALNO
            '種別
            _dcICR700.SYUBETU_WR = _dcICR700.SYUBETU
            '金額
            _dcICR700.KINGAKU_WR = (intZANKN + intPREMKN).ToString.PadLeft(5, "0"c)
            '予備
            _dcICR700.YOBI_WR = _dcICR700.YOBI

            '【V31RW書き込み情報セット】
            '店番
            _dcICR700.SHOPNO_WR = _dcICR700.SHOPNO
            'カード区分
            _dcICR700.CARDKBN_WR = _dcICR700.CARDKBN
            'カード番号
            _dcICR700.CARDNO_WR = _dcICR700.CARDNO
            '顧客番号
            _dcICR700.NCSNO_WR = _dcICR700.NCSNO
            'スクール生番号
            _dcICR700.SCLMANNO_WR = _dcICR700.SCLMANNO
            '顧客種別
            _dcICR700.NKBNO_WR = _dcICR700.NKBNO
            '会員期限
            _dcICR700.DMEMBER_WR = _dcICR700.DMEMBER
            'パスワード
            _dcICR700.PASSCD_WR = _dcICR700.PASSCD
            '残金額
            _dcICR700.ZANKN_WR = intZANKN.ToString.PadLeft(5, "0"c)
            'P残金額
            _dcICR700.PREZANKN_WR = intPREMKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            _dcICR700.POINT_WR = intSRTPO.ToString.PadLeft(5, "0"c)
            '前回来場日
            _dcICR700.ZENENTDATE_WR = _dcICR700.ZENENTDATE
            '入場区分
            _dcICR700.ENTKBN_WR = _dcICR700.ENTKBN
            'ボール単価
            _dcICR700.BALLKIN_WR = _dcICR700.BALLKIN

            '【カード書き込み】
            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.WRITE
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Using frm As New frmMSGBOXEx("カードの書き込みに失敗しました。", 2)
                    frm.ShowDialog()
                    _iDatabase.RollBack()
                    Return False
                End Using
            End If

            _iDatabase.Commit()

            Return True

        Catch ex As Exception
            _iDatabase.RollBack()
            Throw ex
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' カード書き込み失敗による入場情報削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DelEntInfo(ByVal intDENNO As Integer, ByVal strUDNDT As String, ByVal intNCSNO As Integer)
        Dim sql As New System.Text.StringBuilder
        Try
            _iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE DUDNTRN SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND UDNNO = " & intDENNO)

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
            End If

            sql.Clear()
            sql.Append("UPDATE DENTRA SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND UDNNO = " & intDENNO)

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
            End If

            sql.Clear()
            sql.Append("UPDATE ENTTRA SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND ENTNO = " & intDENNO)

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
            End If

            _iDatabase.Commit()

        Catch ex As Exception
            '失敗してもほっとく
            _iDatabase.RollBack()
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' マルチスレッド_時計処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mClock_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mClock.DoWork
        Do
            Me.mClock.ReportProgress(0)
            Thread.Sleep(1000)
        Loop
    End Sub

    ''' <summary>
    ''' マルチスレッド_時計表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mClock_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mClock.ProgressChanged
        'Dim culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ja-JP", True)
        'culture.DateTimeFormat.Calendar = New System.Globalization.JapaneseCalendar
        'lblDate.Text = Now.ToString("ggyy年M月d日(ddd)", culture)
        lblDate.Text = Now.ToString("yyyy年MM月dd日(ddd)")

        lblTime1.Text = Now.ToString("HH")
        lblTime2.Text = Now.ToString("mm")
        'If lblTimeCoron.Visible Then
        '    lblTimeCoron.Visible = False
        'Else
        '    lblTimeCoron.Visible = True
        'End If

    End Sub



    ''' <summary>
    ''' カード確認画面_クリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInfo.Click
        Dim blnERRFLG As Boolean = False        'エラーが発生したかどうか
        Dim blnCANCEL As Boolean = False        '取消が押されたか
        Dim blnERRSHOPFLG As Boolean = False    '店番エラーが発生したかどうか
        Dim strMsg As String = String.Empty


        Dim dummy = New frmDummy
        dummy.Show()

        Try
            'カード読込
            Sound.PlayInsertCard()
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000, _iDatabase)
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.READ
                frm.ShowDialog()
                blnCANCEL = frm.CANCEL
                blnERRSHOPFLG = frm.ERRSHOPFLG
                blnERRFLG = frm.ERRFLG
            End Using

            '取消押された
            If blnCANCEL Then
                _dcICR700.Cancel()
                If Not String.IsNullOrEmpty(_dcICR700.SHOPNO) Or blnERRFLG Then
                    CardEject(False)
                End If
                Exit Sub
            End If

            'エラー発生 / '店番エラー発生
            If blnERRFLG Or blnERRSHOPFLG Then
                'Using frm As New frmMSGBOXEx("店番が一致しません。", 2)
                '    frm.ShowDialog()
                'End Using
                CardEject()
                Exit Sub
            End If

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOXEx("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '本日料金体系情報取得
            If Not GetRknInfo() Then
                Using frm As New frmMSGBOXEx("料金体系情報の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '営業情報マスタ取得
            If Not GetEIGMT() Then
                Using frm As New frmMSGBOXEx("営業情報マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '顧客情報取得
            If Not GetCSMAST() Then
                Using frm As New frmMSGBOXEx("顧客情報がありません。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '*** カード有効期限チェック ***'
            _intCLRKBN = 99
            If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                Dim blnReply As Boolean = False
                If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString) Then
                    Dim dtCARDLIMIT As DateTime = DateTime.Parse(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" & _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(6, 2))
                    ' 入金残高有効期限
                    Dim intPREMLIMIT As Integer = 0
                    Dim strCARDLIMIT As String = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                        intPREMLIMIT = CType("-" & UIUtility.SYSTEM.PREMLIMIT, Integer)
                        dtCARDLIMIT = dtCARDLIMIT.AddMonths(intPREMLIMIT)
                        strCARDLIMIT = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    End If
                    If Now.ToString("yyyyMMdd") > _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString Then
                        _intCLRKBN = 1
                        If _intZANKIN_CLEARE.Equals(0) Then
                            Using frm As New frmMSGBOXEx("カードの有効期限切れです。" & vbCrLf & "フロントへお越しください。", 2)
                                frm.ShowDialog()
                            End Using
                        Else
                            Using frm As New frmMSGBOXEx("カードの有効期限切れです。" & vbCrLf & "残金額をクリアします。", 2)
                                frm.ShowDialog()
                            End Using
                        End If
                    ElseIf Now.ToString("yyyy/MM/dd") > strCARDLIMIT And Not String.IsNullOrEmpty(strCARDLIMIT) Then
                        _intCLRKBN = 0
                        Using frm As New frmMSGBOXEx("残金額の有効期限切れです。" & vbCrLf & "残金額をﾌﾟﾚﾐｱﾑに移行します。", 2)
                            frm.ShowDialog()
                        End Using
                    End If
                    If Not _intCLRKBN.Equals(99) Then
                        If _intZANKIN_CLEARE.Equals(0) And _intCLRKBN.Equals(1) Then
                            '残金のクリアしない
                            CardEject()
                            Exit Sub
                        End If
                        If Not ClearZANKN(_intCLRKBN) Then
                            Using frm As New frmMSGBOXEx("有効期限処理に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            CardEject()
                            Exit Sub
                        End If
                        Exit Sub
                    End If
                End If
            End If

            'カード停止中
            If UIFunction.ChkDCSTPTRN(_dcICR700.CARDNO, _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), _iDatabase) Then
                Using frm As New frmMSGBOXEx("カード停止中です。", 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            '現在の時間帯確認
            Dim dr As DataRow()
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB)

            UIUtility.SYSTEM.NOWTIMEKB = 0
            For i As Integer = 0 To dr.Length - 1
                If dr(i).Item("TIMENM").ToString >= Now.ToString("HHmm") Then
                    UIUtility.SYSTEM.NOWTIMEKB = CType(dr(i).Item("TIMEKB").ToString, Integer)
                    Exit For
                End If
            Next
            If UIUtility.SYSTEM.NOWTIMEKB.Equals(0) Then
                UIUtility.SYSTEM.NOWTIMEKB = 1
            End If
            '単価情報取得
            Dim intNKBNO As Integer = 1
            intNKBNO = CType(_dtCSMAST.Rows(0).Item("NCSRANK").ToString, Integer)

            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = " & intNKBNO & " AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)

            ' カード詳細画面表示
            Using frm As New frmINFOEx()
                frm.iDataBase = _iDatabase
                frm.ICR700 = _dcICR700
                frm.CheckInEnabled = _CheckInEnabled
                frm.ChargeEnabled = _ChargeEnabled
                frm.BallKin1F = CType(dr(0).Item("BALLKIN1F"), Integer)
                frm.BallKin2F = CType(dr(0).Item("BALLKIN2F"), Integer)
                frm.DMEMBER = _dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString
                frm.NCSNO = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                frm.CCSNAME = _dtCSMAST.Rows(0).Item("CCSNAME").ToString
                frm.SRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString, Integer)
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
                Me.vbDialogResult = frm.vbDialogResult
            End Using

            ' 戻り値でボタン押下
            Select Case Me.vbDialogResult
                Case frmBase.eResult.CHECKIN
                    _blnEjectFlg = False
                    btnCheckIn_Click(sender, e)
                Case frmBase.eResult.CHARGE
                    btnCharge_Click(sender, e)
                Case frmBase.eResult.CANCEL
                    CardEject()
            End Select
            Me.vbDialogResult = eResult.NONE

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dummy.Close()
            Me.lblMessage.Text = "ご希望のボタンをタッチしてください。"
            Me.lblMessage.ForeColor = Color.White
            Me.ActiveControl = Nothing
            Me.lblMessage.Focus()

        End Try
    End Sub



End Class