Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmCHARGEEx

#Region "▼宣言部"
    Private _threadAD1 As New Thread(New ThreadStart(AddressOf threadAD1))
    Private _blnRead As Boolean = False
    Private _blnTdEnd As Boolean = False
    Private _blnEnd As Boolean = False

    Private _blnPay_Command As Boolean = False

    Private _blnBillStop As Boolean = False

    ''' <summary>
    ''' データベース制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _iDatabase As IDatabase.IMethod
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
    ''' <summary>
    ''' AD1コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.SC1708Control
    ''' <summary>
    ''' レシートプリンタコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSK121 As New TECHNO.DeviceControls.SK121Control
    ''' <summary>
    ''' 顧客情報保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtCSMAST As DataTable
    ''' <summary>
    ''' 紙幣投入枚数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intInBill As Integer
    ''' <summary>
    ''' 領収書フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnReceipt As Boolean = False
    ''' <summary>
    ''' 入金設定
    ''' 【0】入金禁止【1】千円札のみ【3】５千円・1万円禁止【7】1万円禁止【15】全紙幣可
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPayCommand As Integer = 0
    ''' <summary>
    ''' エラーメッセージ
    ''' </summary>
    ''' <remarks></remarks>
    Private _strErrMessage As String = String.Empty
    ''' <summary>
    ''' 画像ファイルの定義
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DESIPOT_BUTTON_W_IMAGE As String = "D:\GRAN31\IMAGE\AUTOFRONT\btn_charge_deposit_white.png"
    Private Const DESIPOT_BUTTON_B_IMAGE As String = "D:\GRAN31\IMAGE\AUTOFRONT\btn_charge_deposit_black.png"

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' データベース制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property iDatabase As IDatabase.IMethod
        Set(ByVal value As IDatabase.IMethod)
            _iDatabase = value
        End Set
    End Property
    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ICR700 As TECHNO.DeviceControls.ICR700Control
        Set(ByVal value As TECHNO.DeviceControls.ICR700Control)
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
    ''' AD1制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AD1 As Techno.DeviceControls.SC1708Control
        Set(ByVal value As Techno.DeviceControls.SC1708Control)
            _dcAD1 = value
        End Set
    End Property
    ''' <summary>
    ''' 顧客情報ﾃｰﾌﾞﾙ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CSMAST As DataTable
        Set(ByVal value As DataTable)
            _dtCSMAST = value
        End Set
    End Property

    ''' <summary>
    ''' カード排出フラグ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EjectFlg As Boolean
        Set(ByVal value As Boolean)
            _blnEjectFlg = value
        End Set
    End Property
    Private _blnEjectFlg As Boolean = False

    ' カード発券で呼び出された場合True
    Public Property EjectCardMode As Boolean = False

    ' カード発券モードでカードが挿入されているフラグ
    Public Property BlnCardIssued As Boolean = False

    '' バックアップ用
    'Public Property SEQTRN As SEQTRNModel = New SEQTRNModel
    'Public Property DUDNTRN As DUDNTRNModel = New DUDNTRNModel
    'Public Property DENTRA As DENTRAModel = New DENTRAModel
    'Public Property REPOCHARGE_M As REPOCHARGE_MModel = New REPOCHARGE_MModel

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

    ' ユーザー情報
    Public Property USER_INFO As UserInfo = New UserInfo

    ' カード情報
    Public Property ICR700Data As New TECHNO.DeviceControls.ICR700Model

#End Region

#Region "▼ コンストラクタ"

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        '' すべてのコントロールにダブルバッファリングを有効化
        'For Each c As Control In GetAllControls(Me)
        '    EnableDoubleBuffering(c)
        'Next

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

            If CommonSettings.DEBUG Then
                ' 背景画像と表示色の変更
                Me.pnlNKNKN01.BackgroundImage = Image.FromFile(DESIPOT_BUTTON_W_IMAGE)
                Me.lblNKNKN01.ForeColor = Color.Black
                Me.pnlNKNKN01.Enabled = True
            End If

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
            Me.lblInfo.Text = "しばらくお待ちください。"
            Me.lblInfo.Visible = True
            Me.Refresh()

            Me.Refresh()

            ' 発券モードでない場合:取り込んだカードからプロパティを取得して ICR700Data に設定
            ' 発券モードの場合：ICR700Data を呼び出し側から設定
            ' 以後 ICR700Data を元に処理を続行する
            If Not Me.EjectCardMode Then
                SetDataFromCard()
            End If

            '入金マスタ情報取得
            If Not GetNKNMST() Then
                _strErrMessage = "入金マスタ情報の取得に失敗しました。"
                _blnErr = True
                Exit Sub
            End If

            Me.Refresh()

            '取引開始
            For i As Integer = 1 To 5
                If _dcAD1.BvControlData(1, 1, 0, 1, 0, 0) Then
                    Exit For
                End If
                If i.Equals(2) Then
                    _strErrMessage = "取引開始に失敗しました。"
                    _blnErr = True
                    Exit Sub
                End If
                Thread.Sleep(200)
            Next

            'ビルバリ情報確認
            If Not CheckAD1() Then
                _blnErr = True
                Exit Sub
            End If

            ''入金上限設定
            'If Not _dcAD1.Limit_Command(10) Then
            '    _strErrMessage = "入金限度額設定に失敗しました。"
            '    _blnErr = True
            '    Exit Sub
            'End If

            ''入金許可設定に失敗しました。
            'If Not _dcAD1.Pay_Command(_intPayCommand) Then
            '    _strErrMessage = "入金許可設定に失敗しました。"
            '    _blnErr = True
            '    Exit Sub
            'End If


            'Sound.PlayPaymentable20()
            'Sound.PlayTouchReceipt()

            'Me.Timer2.Start()
            Me.timCharge.Start()

            ' 点滅タイマースタート
            Me.timBlinkText.Start()

            _threadAD1.Priority = ThreadPriority.Highest
            _threadAD1.IsBackground = True
            _threadAD1.Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.btnBack.Enabled = True
            Me.lblInfo.Text = "紙 幣 確 認 中"
            Me.lblInfo.Visible = False
            If _blnErr Then
                Me.Timer2.Stop()
                Me.timCharge.Stop()
                Me.timBlinkText.Stop()
                'エラー発生
                Using frm As New frmError01
                    frm.ErrMessage = _strErrMessage
                    frm.AD1 = _dcAD1
                    frm.ICR700 = _dcICR700
                    frm.MCH3000 = _dcMCH3000
                    frm.ShowDialog()
                End Using
                Me.Close()
            End If

        End Try
    End Sub

    ''' <summary>
    ''' 戻るボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click, btnPayBack.Click
        Dim blnERRFLG As Boolean = False
        Dim sql As New System.Text.StringBuilder
        Try

            frmBase.PushAnimation(CType(sender, Control))

            ' ※エラー検出
            If _blnErr Then
                ' エラーで中断
                _blnErr = False
                Me.vbDialogResult = eResult.vbERROR
                Me.Close()
                Exit Sub
            End If

            Me.lblInfo.Text = "しばらくお待ちください。"
            Me.lblInfo.Visible = True
            Me.btnBack.Enabled = False
            'Me.btnBack.BackColor = Color.Orange
            Me.Refresh()

            'Sound.PlayLoading()

            _blnTdEnd = True
            Do
                If _blnEnd Then
                    Exit Do
                End If
            Loop

            Me.timCharge.Stop()

            Do
                If CType(Me.timCharge.Tag, Integer).Equals(0) Then
                    Exit Do
                End If
            Loop

            Do
                _dcAD1.BvDetailData()
                '識別動作中
                If Not _dcAD1.BvState1.Substring(2, 1).Equals("1") Then
                    '紙幣受け入れ/後続金なし
                    If _dcAD1.BvState1.Substring(7, 1).Equals("1") Or _dcAD1.BvState1.Substring(5, 1).Equals("1") Or _dcAD1.BvState1.Equals("00000000") Then
                        Exit Do
                    End If
                End If
            Loop

            For i As Integer = 1 To 5
                If _dcAD1.BvControlData(0, 0, 1, 1, 1, 1) Then
                    Exit For
                End If
                If i.Equals(2) Then
                    _strErrMessage = "入金停止コマンドに失敗しました。"
                    _blnErr = True
                    Me.btnBack.Enabled = True
                    frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                    Exit Sub
                End If
                Thread.Sleep(200)
            Next


            Do
                _dcAD1.BvDetailData()
                '後続金なし
                If _dcAD1.BvState1.Substring(5, 1).Equals("1") Then
                    Exit Do
                End If
            Loop

            If _dcAD1.OutPossibleBill1000 > 0 Then
                For i As Integer = 1 To 5
                    If _dcAD1.Repayment(1) Then
                        Exit For
                    End If
                    If i.Equals(2) Then
                        _strErrMessage = "返金処理に失敗しました。"
                        _blnErr = True
                        Me.btnBack.Enabled = True
                        frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                        Exit Sub
                    End If
                    Thread.Sleep(200)
                Next

                Do
                    _dcAD1.BvDetailData()
                    '払出終了
                    If _dcAD1.BvState1.Substring(4, 1).Equals("1") Then
                        Exit Do
                    End If
                    '代表異常
                    If _dcAD1.BvAbnormal1.Substring(7, 1).Equals("1") Then
                        _strErrMessage = "払出時異常が発生しました。"
                        _blnErr = True
                        Me.btnBack.Enabled = True
                        frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                        Exit Sub
                    End If
                Loop
            End If



            ' 中断成功
            Me.vbDialogResult = eResult.CANCEL

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If _blnErr Then
                'エラー発生
                Using frm As New frmError01
                    frm.ErrMessage = _strErrMessage
                    frm.AD1 = _dcAD1
                    frm.ICR700 = _dcICR700
                    frm.MCH3000 = _dcMCH3000
                    frm.ShowDialog()
                End Using
                Me.Close()
            End If
            'Me.btnBack.BackColor = Color.DarkGray
            Me.btnBack.Refresh()

        End Try
    End Sub

    Dim _blnSelectedButton As Boolean = False ' ボタン選択時

    ''' <summary>
    ''' 入金ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNKN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnNKNKN01.Click

        ' ボタン選択後は二度と選択できない
        If _blnSelectedButton Then Return

        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim strMsg As String = String.Empty
        Dim intChange As Integer = 0         'お釣り
        Dim intButton As Integer = 0
        Dim blnERREIFLG As Boolean = False
        Dim intStep = 0 ' 1…無記名の顧客作成後 2…カード排出後
        Dim backup = New EjectCardRestoreModel ' バックアップ
        Dim blnCreateCustmerError = False

        Try
            'If _dcAD1.DeviceStatus1.Equals(16) Then Exit Sub

            Dim btnNKNKN As Control
            btnNKNKN = CType(sender, Control)

            '入金額
            Dim intNKNKN As Integer = 0
            'プレミアム
            Dim intPREMKN As Integer = 0
            'ポイント
            Dim intPOINT As Integer = 0

            intNKNKN = CType(btnNKNKN.Tag, Integer)
            Select Case btnNKNKN.Name.ToString
                Case "btnNKNKN01" '【入金タグ１】
                    intPREMKN = CType(Me.lblPREMKN01.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT01.Tag, Integer)
                    intButton = 1
            End Select

            '*** 金額計算 ***'

            'プリカ金額
            Dim intKINGAKU As Integer = CType(ICR700Data.KINGAKU, Integer)
            'V31残金
            Dim intV31ZANKN As Integer = CType(ICR700Data.ZANKN, Integer)
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = CType(ICR700Data.PREZANKN, Integer)


            '********************'



            '入金限度額チェック
            If (intKINGAKU + (intNKNKN + intPREMKN)) > UIUtility.SYSTEM.ZANMAX Then
                Sound.PlayPaymentLimit()
                strMsg = "入金限度額を超えています。"
            End If
            If (CType(Me.USER_INFO.SRTPO, Integer) + intPOINT) > 99999 Then
                strMsg = "ﾎﾟｲﾝﾄの上限数を超えています。"
            End If
            If intKINGAKU + intNKNKN < 0 Then
                strMsg = "残金がマイナスになります。"
            End If
            If intV31PREZANKN + intPREMKN < 0 Then
                strMsg = "P)残金がマイナスになります。"
            End If
            If CType(Me.USER_INFO.SRTPO, Integer) + intPOINT < 0 Then
                strMsg = "ポイントがマイナスになります。"
            End If
            If Not String.IsNullOrEmpty(strMsg) Then
                Using frm As New frmMSGBOXEx(strMsg, 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            ' 各種ボタンを無効に
            frmBase.ChangePictureBoxEnabled(Me.btnBack, False)

            ' 選択ボタン以外をデフォルト色に
            Dim i = 1
            For Each pnl As System.Windows.Forms.Panel In GetNKNKNPanels()
                If i <> intButton Then
                    pnl.BackgroundImage = Image.FromFile(DESIPOT_BUTTON_B_IMAGE)
                    pnl.Enabled = False
                End If
                i += 1
            Next

            ' 選択フラグ
            _blnSelectedButton = True

            Me.lblInfo.Text = "しばらくお待ちください。"
            Me.lblInfo.Visible = True
            btnNKNKN.BackColor = Color.Orange
            Me.Refresh()

            Sound.PlayLoading()

            'スレッドストップ
            _blnTdEnd = True
            'スレッド処理完了待ち
            Do
                If _blnEnd Then
                    Exit Do
                End If
            Loop

            Me.timCharge.Stop()

            'Do
            '    If CType(Me.timCharge.Tag, Integer).Equals(0) Then
            '        Exit Do
            '    End If
            'Loop

            'ビルバリ情報再取得
            Do
                If Not CheckAD1() Then
                    _blnErr = True
                    Me.btnBack.Enabled = True
                    frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                    Exit Sub
                End If
                '後続金無しになったら抜ける
                If _dcAD1.BvState1.Equals("00000000") Then
                    Exit Do
                End If
            Loop

            '*** Sta Add 2019/03/19 Kitahara
            '【カード新規発券】 
            If Me.EjectCardMode Then
                Dim custmer = New CustmerModel
                custmer.Enabled = False

                '①無記名の顧客作成
                Using frm As New frmNAMELESS
                    frm.iDatabase = _iDatabase
                    frm.ShowDialog()
                    ' 戻り値
                    blnCreateCustmerError = frm.Err
                    custmer = frm.Custmer
                    ' 復元用
                    backup.CUSTMER = custmer.Clone
                    backup.CUSTMER.Enabled = True ' 復元許可
                End Using

                If blnCreateCustmerError Then
                    Throw New Exception
                End If

                Me.Refresh()

                ' 無記名の顧客作成完了
                intStep = 1

                '②カード発券画面(カードをコンタクト部で停止→書込み)
                Using frm As New frmEJECTCARD
                    frm.Custmer = custmer
                    frm.dcICR700 = _dcICR700
                    frm.dcMCH3000 = _dcMCH3000
                    frm.EjectType = frmEJECTCARD.eEjectType.WithCreateCustmer
                    frm.ShowDialog()
                    ' 戻り値
                    Me.ICR700Data = frm.ICR700Data
                    blnCreateCustmerError = frm.Err
                End Using

                If blnCreateCustmerError Then
                    _strErrMessage = "新規発券に失敗しました。"
                    _blnErr = True
                    Exit Sub
                End If

                ' カード発券完了
                intStep = 2
                Me.BlnCardIssued = True

                '③読込プロパティに値を設定
                _dcICR700.SetPropaties(Me.ICR700Data)

                '⑥顧客情報取得
                If Not GetCSMAST(ICR700Data.NCSNO, ICR700Data.CARDNO) Then
                    Using frm As New frmMSGBOXEx("顧客情報がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    Throw New Exception
                End If

            End If
            '*** End Add 2019/03/19 Kitahara

            For j As Integer = 1 To 5
                If _dcAD1.BvControlData(0, 1, 1, 1, 1, 1) Then
                    Exit For
                End If
                If j.Equals(2) Then
                    _blnErr = True
                    Me.btnBack.Enabled = True
                    frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                    Exit Sub
                End If
                Thread.Sleep(200)
            Next

            intChange = CType(Me.lblInMoney.Text, Integer) - intNKNKN
            Me.lblChange.Text = intChange.ToString("#,##0")
            Me.lblChange.Refresh()


            Me.lblInfo.Visible = False
            Me.Enabled = False
            Me.Refresh()

            '【プリカRW書き込み情報セット】
            '店番号
            _dcICR700.SHOPNO_WR = ICR700Data.SHOPNO
            'パスワード
            _dcICR700.PASSCD_WR = ICR700Data.PASSCD
            'シリアルナンバー
            _dcICR700.SERIALNO_WR = ICR700Data.SERIALNO
            '種別
            _dcICR700.SYUBETU_WR = ICR700Data.SYUBETU
            '金額
            If Me.EjectCardMode Then
                _dcICR700.KINGAKU_WR = (intKINGAKU + (intNKNKN + intPREMKN) - UIUtility.SYSTEM.CARDFEE).ToString.PadLeft(5, "0"c)
            Else
                _dcICR700.KINGAKU_WR = (intKINGAKU + (intNKNKN + intPREMKN)).ToString.PadLeft(5, "0"c)
            End If
            '予備
            _dcICR700.YOBI_WR = ICR700Data.YOBI

            '【V31RW書き込み情報セット】
            '店番
            _dcICR700.SHOPNO_WR = ICR700Data.SHOPNO
            'カード区分
            _dcICR700.CARDKBN_WR = ICR700Data.CARDKBN
            'カード番号
            _dcICR700.CARDNO_WR = ICR700Data.CARDNO
            '顧客番号
            _dcICR700.NCSNO_WR = ICR700Data.NCSNO
            'スクール生番号
            _dcICR700.SCLMANNO_WR = ICR700Data.SCLMANNO
            '顧客種別
            _dcICR700.NKBNO_WR = ICR700Data.NKBNO
            '会員期限
            _dcICR700.DMEMBER_WR = ICR700Data.DMEMBER
            'パスワード
            _dcICR700.PASSCD_WR = ICR700Data.PASSCD
            '前回来場日
            _dcICR700.ZENENTDATE_WR = ICR700Data.ZENENTDATE
            '残金額
            _dcICR700.ZANKN_WR = (intV31ZANKN + intNKNKN - UIUtility.SYSTEM.CARDFEE).ToString.PadLeft(5, "0"c)
            'P残金額
            If Me.EjectCardMode Then
                _dcICR700.PREZANKN_WR = (intV31PREZANKN + intPREMKN).ToString.PadLeft(5, "0"c)
            Else
                _dcICR700.PREZANKN_WR = (intV31PREZANKN + intPREMKN + UIUtility.SYSTEM.CARDFEE).ToString.PadLeft(5, "0"c)
            End If
            '残ポイント
            _dcICR700.POINT_WR = (CType(Me.USER_INFO.SRTPO, Integer) + intPOINT).ToString.PadLeft(5, "0"c)
            '入場区分
            _dcICR700.ENTKBN_WR = "0"
            'ボール単価
            _dcICR700.BALLKIN_WR = ICR700Data.BALLKIN
            '打席番号
            _dcICR700.SEATNO_WR = ICR700Data.SEATNO
            If ICR700Data.SEATNO.Equals("000") Then
                _dcICR700.SEATNO_WR = "FFF"
            End If


            '処理日時
            Dim dtmInsDt As DateTime = Now
            _iDatabase.BeginTransaction()

            ' トランザクション開始
            intStep = 3

            '【伝票番号】
            sql.Clear()
            sql.Append("UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                _strErrMessage = "伝票番号の更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                Exit Sub
            End If

            '伝票番号取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" DENNOSEQ AS DENNO")
            sql.Append(" FROM SEQTRN")

            Dim dtSEQTRN As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If dtSEQTRN.Rows.Count.Equals(0) Then
                _iDatabase.RollBack()
                _strErrMessage = "伝票番号の取得に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                Exit Sub
            End If

            Dim key1 = "1"
            Dim key2 = UIUtility.SYSTEM.UPDDAY
            Dim key3 = dtSEQTRN.Rows(0).Item("DENNO").ToString
            Dim key4 = dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss")

            ' *** STA ADD 2019/03/15 TERAYAMA
            ' 発券機から呼ばれた場合
            Dim buncda = "003"
            Dim buncdc = "002"
            Dim hinnma = "入金"
            If Me.EjectCardMode Or _dcICR700.ENTKBN.Equals("1") Then
                buncda = "010"
                buncdc = "001"
                hinnma = "カード発券"
            End If
            'If Me.EjectCardMode Then
            '    buncda = "010"
            '    buncdc = "001"
            '    hinnma = "カード発券"
            'End If
            ' *** END ADD 2019/03/15 TERAYAMA

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
            '分類コード１【003】入金【010】カード発券
            strSQL1 &= "BUNCDA,"
            strSQL2 &= String.Format("'{0}',", buncda)
            '分類コード２ 入金区分(入金マスタの区分)
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= String.Format("'{0}',", buncdc)
            '売上個数区分 入金区分(入金マスタの区分)
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'001',"
            '売上金額(入金額)
            strSQL1 &= "UDNKN,"
            strSQL2 &= intNKNKN & ","
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
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSRANK").ToString & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= intNKNKN & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= (intNKNKN) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= "0,"
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intPOINT & ","
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= (_intInBill * 1000) & ","
            'おつり
            strSQL1 &= "TURIKN,"
            strSQL2 &= (intChange * 1000) & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'1'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'0',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & hinnma & " " & intNKNKN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPREMKN & ","
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
                _iDatabase.RollBack()
                _strErrMessage = "売上トランの更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                Exit Sub
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
            '分類コード１【003】入金【010】カード発券
            strSQL1 &= "BUNCDA,"
            strSQL2 &= String.Format("'{0}',", buncda)
            '分類コード２ 入金区分(入金マスタの区分)
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= String.Format("'{0}',", buncdc)
            '売上個数区分
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'001',"
            '売上名
            strSQL1 &= "TKTNMA,"
            strSQL2 &= "'" & hinnma & " " & intNKNKN.ToString("#,##0") & "円" & "',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= intNKNKN & ","
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
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSRANK").ToString & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= intNKNKN & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= (intNKNKN) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= "0,"
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intPOINT & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'1'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "0,"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & hinnma & " " & intNKNKN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= intPREMKN & ")"
            If Not _iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                _iDatabase.RollBack()
                _strErrMessage = "伝票トランの更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                Exit Sub
            End If
            '【入金トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO NKNTRN("
            strSQL2 &= " VALUES("
            '削除区分【1】使用中【9】削除
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '伝票日付
            strSQL1 &= "DENDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '伝票番号
            strSQL1 &= "DENNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '入金名
            strSQL1 &= "NKNNM,"
            strSQL2 &= "'" & "入金 " & intNKNKN.ToString("#,##0") & "円" & "',"
            '入金額
            strSQL1 &= "NKNKN,"
            strSQL2 &= intNKNKN & ","
            '税抜売上金額
            strSQL1 &= "NKNAKN,"
            strSQL2 &= intNKNKN & ","
            '税込売上金額
            strSQL1 &= "NKNBKN,"
            strSQL2 &= (intNKNKN) & ","
            '消費税
            strSQL1 &= "NZEIKN,"
            strSQL2 &= "0,"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intPOINT & ","
            '用途不明
            strSQL1 &= "PRERT,"
            strSQL2 &= UIUtility.SYSTEM.CARDFEE & ","
            'strSQL2 &= "NULL,"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPREMKN & ","
            '残金入金前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intV31ZANKN & ","
            '残金入金後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intV31ZANKN + intNKNKN - UIUtility.SYSTEM.CARDFEE & ","
            'P)残金入金前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intV31PREZANKN & ","
            'P)残金入金後
            strSQL1 &= "PREZANBKN,"
            If Me.EjectCardMode Then
                strSQL2 &= intV31PREZANKN + intPREMKN & ","
            Else
                strSQL2 &= intV31PREZANKN + intPREMKN + UIUtility.SYSTEM.CARDFEE & ","
            End If
            '残ポイント入金前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= CType(Me.USER_INFO.SRTPO, Integer) & ","
            '残ポイント入金後
            strSQL1 &= "ZANBPO,"
            strSQL2 &= CType(Me.USER_INFO.SRTPO, Integer) + intPOINT & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            '種別フラグ
            strSQL1 &= "STSFLG,"
            strSQL2 &= "'2',"
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"
            If Not _iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                _iDatabase.RollBack()
                _strErrMessage = "入金トランの更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                Exit Sub
            End If

            '帳票用入金機履歴
            sql.Clear()
            sql.Append("UPDATE REPOCHARGE_M SET")
            Select Case intButton
                Case 1
                    sql.Append(" CHARGE1KIN = " & intNKNKN)
                    sql.Append(",CHARGE1KAISU = CHARGE1KAISU + 1")
                    sql.Append(",CHARGE1GOKEIKIN = CHARGE1GOKEIKIN + " & intNKNKN)
                Case 2
                    sql.Append(" CHARGE2KIN = " & intNKNKN)
                    sql.Append(",CHARGE2KAISU = CHARGE2KAISU + 1")
                    sql.Append(",CHARGE2GOKEIKIN = CHARGE2GOKEIKIN + " & intNKNKN)
                Case 3
                    sql.Append(" CHARGE3KIN = " & intNKNKN)
                    sql.Append(",CHARGE3KAISU = CHARGE3KAISU + 1")
                    sql.Append(",CHARGE3GOKEIKIN = CHARGE3GOKEIKIN + " & intNKNKN)
                Case 4
                    sql.Append(" CHARGE4KIN = " & intNKNKN)
                    sql.Append(",CHARGE4KAISU = CHARGE4KAISU + 1")
                    sql.Append(",CHARGE4GOKEIKIN = CHARGE4GOKEIKIN + " & intNKNKN)
                Case 5
                    sql.Append(" CHARGE5KIN = " & intNKNKN)
                    sql.Append(",CHARGE5KAISU = CHARGE5KAISU + 1")
                    sql.Append(",CHARGE5GOKEIKIN = CHARGE5GOKEIKIN + " & intNKNKN)
                Case 6
                    sql.Append(" CHARGE6KIN = " & intNKNKN)
                    sql.Append(",CHARGE6KAISU = CHARGE6KAISU + 1")
                    sql.Append(",CHARGE6GOKEIKIN = CHARGE6GOKEIKIN + " & intNKNKN)
            End Select
            '千円札投入回数
            sql.Append(",SENENINKAISU = SENENINKAISU + " & _dcAD1.InBill1000)
            '二千円札投入回数
            sql.Append(",NISENENINKAISU = NISENENINKAISU + " & _dcAD1.InBill2000)
            '五千円札投入回数
            sql.Append(",GOSENENINKAISU = GOSENENINKAISU + " & _dcAD1.InBill5000)
            '一万円札投入回数
            sql.Append(",ICHIMANENINKAISU = ICHIMANENINKAISU + " & _dcAD1.InBill10000)
            '千円札払出回数
            sql.Append(",SENENOUTKAISU = SENENOUTKAISU + " & _dcAD1.OutBill1000)
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE CHARGEDAY = '" & UIUtility.SYSTEM.UPDDAY & "' AND HOSTNAME = '" & My.Computer.Name & "'")
            sql.Append(" AND NKNKBN = 1")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                _strErrMessage = "帳票用入金機履歴の更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Exit Sub
            End If

            ' *** STA ADD 2019/02/12 TERAYAMA カード発行回数の更新
            '【帳票用入金機履歴(カード発行回数)】
            ' * カード発券モード時のみ
            If Me.EjectCardMode Or _dcICR700.ENTKBN.Equals("1") Then
                sql.Clear()
                sql.Append("UPDATE REPOCHARGE_M SET HAKKOKAISU = HAKKOKAISU + 1,HAKKOGOKEIKIN = HAKKOGOKEIKIN + " & UIUtility.SYSTEM.CARDFEE & "  WHERE CHARGEDAY = '" & UIUtility.SYSTEM.UPDDAY & "' AND HOSTNAME = '" & My.Computer.Name & "' AND NKNKBN = 1")

                If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                    _iDatabase.RollBack()
                    _strErrMessage = "帳票用入金機履歴(ｶｰﾄﾞ発行回数)の更新に失敗しました。"
                    _blnErr = True
                    Me.btnBack.Enabled = True
                    frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                    Exit Sub
                End If
            End If
            'If Me.EjectCardMode Then
            '    sql.Clear()
            '    sql.Append("UPDATE REPOCHARGE_M SET HAKKOKAISU = HAKKOKAISU + 1,HAKKOGOKEIKIN = HAKKOGOKEIKIN + " & UIUtility.SYSTEM.CARDFEE & "  WHERE CHARGEDAY = '" & UIUtility.SYSTEM.UPDDAY & "' AND HOSTNAME = '" & My.Computer.Name & "'")

            '    If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
            '        _iDatabase.RollBack()
            '        _strErrMessage = "帳票用入金機履歴(ｶｰﾄﾞ発行回数)の更新に失敗しました。"
            '        _blnErr = True
            '        Me.btnBack.Enabled = True
            '        frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
            '        Exit Sub
            '    End If
            'End If
            ' *** END ADD 2019/02/12 TERAYAMA カード発行回数の更新

            '【金額サマリ】
            sql.Clear()
            sql.Append("UPDATE KINSMA SET")
            sql.Append(" ZANKN = " & (intV31ZANKN + intNKNKN) - UIUtility.SYSTEM.CARDFEE)
            If Me.EjectCardMode Then
                sql.Append(",PREZANKN = " & (intV31PREZANKN + intPREMKN))
            Else
                sql.Append(",PREZANKN = " & (intV31PREZANKN + intPREMKN) + UIUtility.SYSTEM.CARDFEE)
            End If
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                _strErrMessage = "金額サマリの更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Exit Sub
            End If
            '【ポイントサマリ】
            sql.Clear()
            sql.Append("UPDATE DPOINTSMA SET")
            sql.Append(" SRTPO = " & (CType(Me.USER_INFO.SRTPO, Integer) + intPOINT))
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                _strErrMessage = "ポイントサマリの更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Exit Sub
            End If
            '【カード有効期限】
            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                Dim dtCARDLIMIT As DateTime = Now
                sql.Append(" CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
            Else
                sql.Append(" CARDLIMIT = NULL")
            End If
            sql.Append(",DUPDATE = NOW()")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                _strErrMessage = "有効期限の更新に失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Exit Sub
            End If

            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                'カードライト
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.WRITE
                frm.EJECTSTOP = CommonSettings.IsChargeResultEjectStopMode
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
                blnERREIFLG = frm.ERREIFLG
            End Using

            Me.Refresh()

            If blnERRFLG Then
                If blnERREIFLG Then
                    _strErrMessage = "ｶｰﾄﾞの排出に失敗しました。"
                    _iDatabase.Commit()
                    Exit Sub
                End If
                _iDatabase.RollBack()
                _strErrMessage = "ｶｰﾄﾞの書き込みに失敗しました。"
                _blnErr = True
                Me.btnBack.Enabled = True
                frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
                DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                'If blnERREIFLG Then
                '    '// 後方吸引失敗 //
                '    '【V31RW書き込み情報セット】
                '    '店番
                '    _dcICR700.SHOPNO_WR = ICR700Data.SHOPNO
                '    'カード区分
                '    _dcICR700.CARDKBN_WR = ICR700Data.CARDKBN
                '    'カード番号
                '    _dcICR700.CARDNO_WR = ICR700Data.CARDNO
                '    '顧客番号
                '    _dcICR700.NCSNO_WR = ICR700Data.NCSNO
                '    'スクール生番号
                '    _dcICR700.SCLMANNO_WR = ICR700Data.SCLMANNO
                '    '顧客種別
                '    _dcICR700.NKBNO_WR = ICR700Data.NKBNO
                '    '会員期限
                '    _dcICR700.DMEMBER_WR = ICR700Data.DMEMBER
                '    'パスワード
                '    _dcICR700.PASSCD_WR = ICR700Data.PASSCD
                '    '前回来場日
                '    _dcICR700.ZENENTDATE_WR = ICR700Data.ZENENTDATE
                '    '残金額
                '    _dcICR700.ZANKN_WR = ICR700Data.ZANKN
                '    'P残金額
                '    _dcICR700.PREZANKN_WR = ICR700Data.PREZANKN
                '    '残ポイント
                '    _dcICR700.POINT_WR = ICR700Data.POINT
                '    '入場区分
                '    _dcICR700.ENTKBN_WR = ICR700Data.ENTKBN
                '    'ボール単価
                '    _dcICR700.BALLKIN_WR = ICR700Data.BALLKIN
                '    '打席番号
                '    _dcICR700.SEATNO_WR = ICR700Data.SEATNO

                'End If
                Exit Sub
            End If

            _iDatabase.Commit()

            If Not CommonSettings.IsChargeResultEjectStopMode Then
                'Sound.PlayReceiveCard()
            End If

            Dim intDenno = CInt(dtSEQTRN.Rows(0).Item("DENNO"))

            If Not _blnErr Then

                Dim intDeposit = CInt(Me.lblInMoney.Text.Replace(",", ""))
                Dim user = New UserInfo
                user.NCSNO = String.Empty
                user.CCSNAME = String.Empty
                If _dtCSMAST.Rows.Count > 0 Then
                    user.NCSNO = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                    If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("CCSNAME").ToString) Then
                        user.CCSNAME = _dtCSMAST.Rows(0).Item("CCSNAME").ToString & " 様"
                    End If
                End If
                If Me.EjectCardMode Then
                    user.ZANKN = CInt(ICR700Data.KINGAKU) + intNKNKN + intPREMKN - UIUtility.SYSTEM.CARDFEE
                Else
                    user.ZANKN = CInt(ICR700Data.KINGAKU) + intNKNKN + intPREMKN
                End If
                user.SRTPO = CInt(Me.USER_INFO.SRTPO) + intPOINT
                user.ENTKBN = ICR700Data.ENTKBN

                Select Case CommonSettings.LayoutType
                    Case 0
                        Using frm As New frmCHARGEResult
                            frm.NKNKN = intNKNKN
                            frm.DEPOSIT = intDeposit
                            frm.PREMKN = intPREMKN
                            frm.POINT = intPOINT
                            frm.RECEIPT = _blnReceipt
                            frm.USER_INFO = user
                            frm.SK121 = _dcSK121
                            frm.ICR700 = _dcICR700
                            frm.MCH3000 = _dcMCH3000
                            frm.DENNO = intDenno
                            frm.ShowDialog(Me)
                            _blnErr = frm.Err
                            Me.vbDialogResult = frm.vbDialogResult
                        End Using
                    Case 1
                        Using frm As New frmCHARGEResult02
                            frm.NKNKN = intNKNKN
                            frm.DEPOSIT = intDeposit
                            frm.PREMKN = intPREMKN
                            frm.POINT = intPOINT
                            frm.RECEIPT = _blnReceipt
                            frm.USER_INFO = user
                            frm.ICR700 = _dcICR700
                            frm.MCH3000 = _dcMCH3000
                            frm.DENNO = intDenno
                            frm.ShowDialog(Me)
                            _blnErr = frm.Err
                            Me.vbDialogResult = frm.vbDialogResult
                        End Using
                End Select

            End If

            Me.Close()

        Catch ex As Exception
            If intStep >= 3 Then
                _iDatabase.RollBack()
            End If
            Me.timCharge.Tag = 0
            Me.btnBack.Enabled = True
            frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
            Me.btnPayBack.PerformClick()
            'MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.btnBack.Enabled = True
            frmBase.ChangePictureBoxEnabled(Me.btnBack, True)

            ' *** ADD STA 2019/04/04 TERAYAMA 新規発券操作の取消とカード排出
            If _blnErr Or blnCreateCustmerError Then
                ' 入金エラーまたは無記名レコード中のエラー、発券時のエラー
                RestoreCreateCustmer(intStep, backup)
            End If
            ' *** ADD END 2019/04/04 TERAYAMA 新規発券操作の取消とカード排出

            If _blnErr Or blnERREIFLG Then
                'エラー発生
                '_dcAD1.Pay_Command(0)
                Using frm As New frmError01
                    frm.ErrMessage = _strErrMessage
                    frm.ICR700 = _dcICR700
                    frm.AD1 = _dcAD1
                    frm.MCH3000 = _dcMCH3000
                    frm.ShowDialog()
                End Using
                Me.Close()
            End If


        End Try
    End Sub


    Dim _blnSoundPlayed As Boolean = False    ' 音声再生済みフラグ
    Dim _blnBillStopShowed As Boolean = False ' ビルストップ表示済フラグ

    ''' <summary>
    ''' タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timCharge_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timCharge.Tick
        Try
            Me.timCharge.Tag = 1

            If _blnErr Then
                Me.timCharge.Stop()
                Exit Sub
            End If
            'ビルバリデータ詳細データ取得
            If Not CheckAD1() Then
                Me.timCharge.Stop()
                _blnErr = True
                Exit Sub
            End If

            Me.lblInMoney.Text = (_intInBill * 1000).ToString("#,##0")
            Me.lblInMoney.Refresh()


            ' 投入額に応じて入金ボタン有効化
            If (CType(Me.lblInMoney.Text, Integer) >= CType(Me.btnNKNKN01.Tag, Integer)) And Not CType(Me.btnNKNKN01.Tag, Integer).Equals(0) Then
                ' 背景画像と表示色の変更
                Me.pnlNKNKN01.BackgroundImage = Image.FromFile(DESIPOT_BUTTON_W_IMAGE)
                Me.lblNKNKN01.ForeColor = Color.Black
                Me.pnlNKNKN01.Enabled = True
                Me.btnNKNKN01.PerformClick()
            End If
     

            ' 入金可能なら音声再生
            If pnlNKNKN01.Enabled And Not _blnSoundPlayed Then
                Sound.PlayTouchAmount()
                _blnSoundPlayed = True
            End If


            'ビルバリデータ詳細データ取得
            If Not CheckAD1() Then
                Me.timCharge.Stop()
                _blnErr = True
                Exit Sub
            End If

            'If _dcAD1.DeviceStatus1.Equals(4) Then
            '    '【待機中】

            '    ''ビルバリデータ詳細データ取得
            '    'If Not CheckAD1() Then
            '    '    Me.timCharge.Stop()
            '    '    _blnErr = True
            '    '    Exit Sub
            '    'End If
            '    _blnPay_Command = True

            '    '紙幣識別中
            '    Me.lblInfo.Visible = False
            '    Me.lblInfo.Refresh()
            '    'Me.Refresh()

            '    Me.btnBack.Enabled = True
            '    frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
            '    Me.btnBack.Refresh()

            '    'If Not _dcAD1.Pay_Command(_intPayCommand) Then
            '    '    _strErrMessage = "入金許可設定に失敗しました。"
            '    '    _blnErr = True
            '    '    timCharge.Stop()
            '    '    Exit Sub
            '    'End If

            'ElseIf _dcAD1.DeviceStatus1.Equals(8) Then
            '    '【紙幣挿入待ち】

            '    '紙幣識別中
            '    Me.lblInfo.Visible = False
            '    Me.lblInfo.Refresh()
            '    'Me.Refresh()

            '    Me.btnBack.Enabled = True
            '    frmBase.ChangePictureBoxEnabled(Me.btnBack, True)
            '    Me.btnBack.Refresh()

            'ElseIf _dcAD1.DeviceStatus1.Equals(16) Then
            '    '【入金動作中】

            '    '紙幣識別中
            '    Me.lblInfo.Visible = True
            '    Me.lblInfo.Refresh()

            '    Me.btnBack.Enabled = False
            '    Me.btnBack.Refresh()

            '    ''ビルバリデータ詳細データ取得
            '    'If Not CheckAD1() Then
            '    '    Me.timCharge.Stop()
            '    '    _blnErr = True
            '    '    Exit Sub
            '    'End If

            '    'If Not _dcAD1.Pay_Command(_intPayCommand) Then
            '    '    _strErrMessage = "入金許可設定に失敗しました。"
            '    '    _blnErr = True
            '    '    timCharge.Stop()
            '    '    Exit Sub
            '    'End If
            'Else
            '    _blnPay_Command = True
            '    ''ビルバリデータ詳細データ取得
            '    'If Not CheckAD1() Then
            '    '    Me.timCharge.Stop()
            '    '    _blnErr = True
            '    '    Exit Sub
            '    'End If
            'End If

        Catch ex As Exception
            Me.timCharge.Stop()
            _strErrMessage = ex.Message.ToString
        Finally

            Me.timCharge.Tag = 0
            If _blnErr Then
                Me.timCharge.Stop()
                'エラー発生
                Using frm As New frmError01
                    frm.ErrMessage = _strErrMessage
                    frm.AD1 = _dcAD1
                    frm.ICR700 = _dcICR700
                    frm.MCH3000 = _dcMCH3000
                    frm.ShowDialog()
                End Using
                Me.pnlNKNKN01.Enabled = False
                Me.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 領収書音声タイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            If Not _blnSoundPlayed Then
                Sound.PlayPaymentable20()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Timer2.Stop()
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
            '入金
            Me.lblNKNKN01.Text = String.Empty
            'ﾌﾟﾚﾐｱﾑ
            Me.lblPREMKN01.Text = String.Empty
            'ポイント
            Me.lblPOINT01.Text = String.Empty
            '投入金額
            Me.lblInMoney.Text = "0"
            'お釣り
            Me.lblChange.Text = "0"


            ' 料金ボタンの初期化
            Me.pnlNKNKN01.Enabled = False

            ' ラベル系の非表示
            Me.lbl1_1.Visible = False
            Me.lbl1_2.Visible = False

            ' 見えない位置に移動
            Me.btnNKNKN01.Location = New Point(-100, -100)

            Me.btnPayBack.Location = New Point(-100, -100)

            ' 

            Me.lblCARDFEE.Text = UIUtility.SYSTEM.CARDFEE.ToString("#,##0")
            If Me.EjectCardMode Then
                Me.Label4.Visible = False
                Me.lblZANKN.Visible = False
                Me.Label5.Visible = False
                Me.lblSRTPO.Visible = False
                If UIUtility.SYSTEM.CARDFEE > 0 Then
                    Me.picCARDFEE.Visible = True
                    Me.lblCARDFEE1.Visible = True
                    Me.lblCARDFEE2.Visible = True
                    Me.lblCARDFEE.Visible = True
                End If
            Else
                If _dcICR700.ENTKBN.Equals("0") Then
                    UIUtility.SYSTEM.CARDFEE = 0
                End If
                If UIUtility.SYSTEM.CARDFEE > 0 Then
                    Me.picCARDFEE.Visible = True
                    Me.lblCARDFEE1.Visible = True
                    Me.lblCARDFEE2.Visible = True
                    Me.lblCARDFEE.Visible = True
                End If
                'UIUtility.SYSTEM.CARDFEE = 0
            End If

            ' ユーザー情報
            Me.lblZANKN.Text = String.Format("{0}円", Me.USER_INFO.ZANKN.ToString("N0"))
            Me.lblSRTPO.Text = String.Format("{0}Ｐ", Me.USER_INFO.SRTPO.ToString("N0"))



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 読み込んだカードから各値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataFromCard()
        ICR700Data.SHOPNO = _dcICR700.SHOPNO
        ICR700Data.PASSCD = _dcICR700.PASSCD
        ICR700Data.SERIALNO = _dcICR700.SERIALNO
        ICR700Data.SYUBETU = _dcICR700.SYUBETU
        ICR700Data.KINGAKU = _dcICR700.KINGAKU
        ICR700Data.YOBI = _dcICR700.YOBI
        ICR700Data.SHOPNO = _dcICR700.SHOPNO
        ICR700Data.CARDKBN = _dcICR700.CARDKBN
        ICR700Data.CARDNO = _dcICR700.CARDNO
        ICR700Data.NCSNO = _dcICR700.NCSNO
        ICR700Data.SCLMANNO = _dcICR700.SCLMANNO
        ICR700Data.NKBNO = _dcICR700.NKBNO
        ICR700Data.DMEMBER = _dcICR700.DMEMBER
        ICR700Data.PASSCD = _dcICR700.PASSCD
        ICR700Data.ZANKN = _dcICR700.ZANKN
        ICR700Data.PREZANKN = _dcICR700.PREZANKN
        ICR700Data.POINT = _dcICR700.POINT
        ICR700Data.ZENENTDATE = _dcICR700.ZENENTDATE
        ICR700Data.ENTKBN = _dcICR700.ENTKBN
        ICR700Data.BALLKIN = _dcICR700.BALLKIN
        ICR700Data.SEATNO = _dcICR700.SEATNO
    End Sub

    ''' <summary>
    ''' ビルバリ情報確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckAD1(Optional ByVal blnThread As Boolean = False) As Boolean
        Try
            _strErrMessage = String.Empty

            If blnThread Then
                _dcAD1.BvDetailData()
            Else
                _dcAD1.BvDetailData()
            End If

            '紙幣投入枚数データ取得
            _intInBill = _dcAD1.InBill1000

            If Not blnThread Then
                Me.lblInMoney.Text = (_intInBill * 1000).ToString("#,##0")
                Me.lblInMoney.Refresh()
            End If

            '代表異常
            If _dcAD1.BvAbnormal1.Substring(7, 1).Equals("1") Then
                _strErrMessage = "【代表異常】"
            End If
            '識別部異常
            If _dcAD1.BvAbnormal1.Substring(5, 1).Equals("1") Then
                _strErrMessage &= "【識別部異常】"
                Return False
            End If
            'スタッカー異常
            If _dcAD1.BvAbnormal1.Substring(4, 1).Equals("1") Then
                _strErrMessage &= "【スタッカー異常】"
                Return False
            End If
            '札詰まり
            If _dcAD1.BvAbnormal1.Substring(3, 1).Equals("1") Then
                _strErrMessage &= "【札詰まり】"
                Return False
            End If
            '紙幣払出異常
            If _dcAD1.BvAbnormal1.Substring(2, 1).Equals("1") Then
                _strErrMessage &= "【紙幣払出異常】"
                Return False
            End If
            '金庫満杯
            If _dcAD1.BvAbnormal1.Substring(1, 1).Equals("1") Then
                _strErrMessage &= "【金庫満杯】"
                Return False
            End If

            ''紙幣受け入れ
            'If _dcAD1.BvState1.Substring(7, 1).Equals("1") Then
            'End If
            '紙幣回収中
            If _dcAD1.BvState1.Substring(6, 1).Equals("1") Then
                _strErrMessage &= "【紙幣回収中】"
                Return False
            End If

            ''後続金無し
            'If _dcAD1.BvState1.Substring(5, 1).Equals("1") Then
            'End If
            ''払出終了
            'If _dcAD1.BvState1.Substring(4, 1).Equals("1") Then
            'End If
            ''クリア済み
            'If _dcAD1.BvState1.Substring(3, 1).Equals("1") Then
            'End If
            ''識別動作中
            'If _dcAD1.BvState1.Substring(2, 1).Equals("1") Then
            'End If

            ''集金動作中
            'If _dcAD1.BvState1.Substring(1, 1).Equals("1") Then
            'End If
            ''返金動作中
            'If _dcAD1.BvState1.Substring(0, 1).Equals("1") Then
            'End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' 入金マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNKNMST() As Boolean
        Try
            Me.pnlNKNKN01.Visible = True
            '入金額01
            Me.lblNKNKN01.Text = String.Format("￥{0}円", "1,000")
            Me.btnNKNKN01.Tag = 1000
            'プレミアム01
            Me.lblPREMKN01.Text = "0円"
            Me.lblPREMKN01.Tag = 0
            Me.lblPREMKN01.Visible = True
            'ポイント01
            Me.lblPOINT01.Text = "0Ｐ"
            Me.lblPOINT01.Tag = 0
            ' ラベルの表示
            Me.lbl1_1.Visible = True

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' カード書き込み失敗による入場情報削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DelNknInfo(ByVal intDENNO As Integer, ByVal intNCSNO As Integer)
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
            sql.Append("UPDATE NKNTRN SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  DENDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND DENNO = " & intDENNO)

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

#Region "▼ｽﾚｯﾄﾞ"
    ''' <summary>
    ''' 紙幣識別スレッド
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ThreadAD1()

        Dim strErrNo As String = String.Empty
        Dim blnRead As Boolean = False
        Try
            Do
                If _blnTdEnd Then Exit Do

                ''ビルバリデータ詳細データ取得
                'If Not CheckAD1(True) Then
                '    'Me.timCharge.Stop()
                '    _blnErr = True
                '    Exit Sub
                'End If

                'If _blnPay_Command Then
                '    If Not _dcAD1.Pay_Command(_intPayCommand) Then
                '        _strErrMessage = "入金許可設定に失敗しました。"
                '        _blnErr = True
                '        'timCharge.Stop()
                '        Exit Sub
                '    End If
                '    _blnPay_Command = False
                'End If
            Loop

        Catch ex As Exception
            MessageBox.Show("スレッドエラー")
        Finally
            _blnEnd = True
        End Try

    End Sub
#End Region

    ''' <summary>
    ''' 関連するパネルをクリックした場合、実態のボタンを起動させる
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lblNKNKN01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        pnlNKNKN01.Click, lblNKNKN01.Click, lblPREMKN01.Click, lblPOINT01.Click, lbl1_1.Click, lbl1_2.Click
        btnNKNKN01.PerformClick()
    End Sub

    ''' <summary>
    ''' 文字の点滅
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timBlinkText_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBlinkText.Tick
        If Me.lblBlinkText.Visible Then
            Me.lblBlinkText.Visible = False
        Else
            Me.lblBlinkText.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' 入金ボタンを配列で取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNKNKNPanels() As System.Windows.Forms.Panel()
        Return {Me.pnlNKNKN01}
    End Function

#Region "▼ アニメーション"

    Private _timBlinkTextEnabled As Boolean = False
    Private _timChargeEnabled As Boolean = False
    Private _Timer2Enabled As Boolean = False

    Private Sub PlayBillStopAnimation()
        ' アニメーションスタート
        ' 他のタイマーの一時停止
        _timBlinkTextEnabled = timBlinkText.Enabled
        _timChargeEnabled = timCharge.Enabled
        _Timer2Enabled = Timer2.Enabled
        timBlinkText.Stop()
        timCharge.Stop()
        Timer2.Stop()

        timAnimation.Interval = 1
        timAnimation.Start()
    End Sub


#End Region

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST(ByVal NCSNO As String, ByVal CARDNO As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

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
            sql.Append(" A.NCARDID = " & CType(NCSNO, Integer))

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
    ''' 新規発行操作の取消とカード排出
    ''' </summary>
    ''' <param name="intStep"></param>
    ''' <param name="backup"></param>
    ''' <remarks></remarks>
    Private Sub RestoreCreateCustmer(ByVal intStep As Integer, ByRef backup As EjectCardRestoreModel)
        Dim blnErr = False
        Try
            If intStep >= 1 Then
                ' 無記名の顧客を削除
                Using frm As New frmEJECTCARD_Restore
                    frm.Custmer = backup.CUSTMER
                    frm.iDatabase = _iDatabase
                    frm.ShowDialog()
                    blnErr = frm.Err
                End Using
                If blnErr Then
                    Using frm As New frmMSGBOXEx("データベースの復元に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If
            If intStep = 2 Then
                ' コンタクト部のカードを強制排出
                ' ただしトランザクションが開始されたら中止ボタンから排出する
                Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                    frm.COMMAND = frmREQUESTCARDEx.Command_Type.EJECT
                    frm.ShowDialog()
                    blnErr = frm.ERRFLG
                End Using
                If blnErr Then
                    Using frm As New frmMSGBOXEx("カード排出に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class