Imports System.Threading
Imports System.ComponentModel
Imports Techno.DeviceControls

Public Class frmREQUESTCARD

#Region "▼宣言部"

    ''' <summary>
    ''' ICR700リーダライターICR700制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New Techno.DeviceControls.ICR700Control
    ''' <summary>
    ''' KU-A201リーダライターIA240制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcIA240 As New Techno.DeviceControls.IA240Control

    ''' <summary>
    ''' コマンド選択プロパティ列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Command_Type As Integer
        ''' <summary>
        ''' カード初期化
        ''' </summary>
        ''' <remarks></remarks>
        CARDINIT = 1
        ''' <summary>
        ''' カードリード
        ''' </summary>
        ''' <remarks></remarks>
        READ = 2
        ''' <summary>
        ''' カードライト
        ''' </summary>
        ''' <remarks></remarks>
        WRITE = 3
        ''' <summary>
        ''' カード排出
        ''' </summary>
        ''' <remarks></remarks>
        EJECT = 4
        ''' <summary>
        ''' プリペイドカードリード
        ''' </summary>
        ''' <remarks></remarks>
        PREREAD = 5
        ''' <summary>
        ''' プリペイドカードライト
        ''' </summary>
        ''' <remarks></remarks>
        PREWRITE = 6
        ''' <summary>
        ''' ｶｰﾄﾞ修復
        ''' </summary>
        ''' <remarks></remarks>
        REPAIR = 8
    End Enum

    ''' <summary>
    ''' バックアップ用
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700Bak As New Techno.DeviceControls.ICR700Control

    ''' <summary>
    ''' DB更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private _iDatabase As Techno.DataBase.IDatabase.IMethod

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' コマンド指定
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property COMMAND() As Command_Type
        Set(ByVal value As Command_Type)
            _intCommand = value
        End Set
    End Property
    Private _intCommand As Integer = 0

    ''' <summary>
    ''' プリカRW内からカードを排出しない【True】しない【False】する
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EJECTSTOP As Boolean
        Set(ByVal value As Boolean)
            _blnEJECTSTOP = value
        End Set
    End Property
    Private _blnEJECTSTOP As Boolean = False

    ''' <summary>
    ''' エラーフラグ【True】エラー有り
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ERRFLG As Boolean
        Get
            Return _blnERRFLG
        End Get
    End Property
    Private _blnERRFLG As Boolean = False

    ''' <summary>
    ''' PreRwエラーフラグ
    ''' 【1】PreRwでエラー【2】V31Rwでエラー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ERRRWKBN As Integer
        Get
            Return _intERRRWKBN
        End Get
    End Property
    Private _intERRRWKBN As Integer = 0

    ''' <summary>
    ''' リライトＲＷﾘｰﾄﾞｴﾗｰ無視
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ERRSKIPV31 As Boolean
        Set(ByVal value As Boolean)
            _blnERRSKIPV31 = value
        End Set
    End Property
    Private _blnERRSKIPV31 As Boolean = False

    ''' <summary>
    ''' 店番エラーフラグ【True】エラー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ERRSHOPFLG As Boolean
        Get
            Return _blnERRSHOPFLG
        End Get
    End Property
    Private _blnERRSHOPFLG As Boolean = False

    ''' <summary>
    ''' 取消確認【True】取消 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CANCEL As Boolean
        Get
            Return _blnCANCEL
        End Get
    End Property
    Private _blnCANCEL As Boolean = False

    ''' <summary>
    ''' 読み取り、書き込み時のカード要求をスキップ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property REQUEST_SKIP As Boolean
        Get
            Return _blnREQUEST_SKIP
        End Get
        Set(ByVal value As Boolean)
            _blnREQUEST_SKIP = value
        End Set
    End Property
    Private _blnREQUEST_SKIP As Boolean = False

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal ICR700 As Techno.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()



            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            '
            _dcICR700 = ICR700
            _dcICR700.ResetSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal ICR700 As Techno.DeviceControls.ICR700Control, ByVal iDatabase As Techno.DataBase.IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()



            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            '
            _dcICR700 = ICR700
            _dcICR700.ResetSettings()

            _iDatabase = iDatabase

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal IA240 As Techno.DeviceControls.IA240Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()



            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            '
            _dcIA240 = IA240

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmREQUESTCARD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblMSG.ForeColor = Color.Blue
            Me.lblMSG.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カードユニットの初期化/起動テスト
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InitRWUnit() As Boolean
        Dim blnErrFlg = False
        Try
            ' ICRW
            If _dcICR700.GetStatusAny Then
                blnErrFlg = False
            End If

            If blnErrFlg Then
                Me.lblMSG.Text = "リーダーライターの接続に失敗しました。"
                Me.lblMSG.ForeColor = Color.Red
                Me.Refresh()
                Thread.Sleep(2000)
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmREQUESTCARD_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.Refresh()

            Select Case _intCommand
                Case Command_Type.CARDINIT
                    '【カード初期化】

                    If Not REQUEST_SKIP Then
                        Me.lblMSG.Text = UIMessage.InsertCard()
                    End If
                    Me.lblMSG.Refresh()

                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    ' 初期化
                    If Not CardInit() Then
                        _blnCANCEL = True
                        Throw New Exception
                    End If

                Case Command_Type.READ
                    '【カードリード】

                    Me.lblMSG.Text = "初期化中…しばらくお待ちください。"
                    Me.lblMSG.Refresh()

                    If Not REQUEST_SKIP Then
                        Me.lblMSG.Text = UIMessage.InsertCard()
                    End If
                    Me.lblMSG.Refresh()

                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    ' カード読み込み
                    If Not CardRead() Then
                        Throw New Exception
                    End If

                Case Command_Type.WRITE
                    '【カードライト】

                    Me.lblMSG.Text = "カードに情報を書き込んでいます。"
                    Me.lblMSG.Refresh()

                    Me.btnCancel.Visible = False
                    Me.Refresh()

                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    ' あらかじめバックアップ
                    _dcICR700Bak = _dcICR700.Clone

                    ' カード書込み
                    If Not CardWrite() Then
                        Throw New Exception
                    End If

                    ' カード排出
                    If Not _blnEJECTSTOP Then
                        Me.lblMSG.Text = UIMessage.EjectCard()
                        Me.lblMSG.Refresh()
                        If Not CardEject_Wait() Then
                            Throw New Exception
                        End If
                    End If
                    
                Case Command_Type.EJECT

                    '【カード排出】
                    Me.lblMSG.Text = UIMessage.EjectCard()
                    Me.lblMSG.Refresh()
                    Me.btnCancel.Visible = False

                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    ' カード排出
                    If Not CardEject_Wait() Then
                        Throw New Exception
                    End If

                Case Command_Type.PREREAD
                    '【プリペイドカードリード】
                    Me.lblMSG.Text = "ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞを挿入してください。"
                    Me.lblMSG.Refresh()
                    Me.timPreRead.Start()
                Case Command_Type.PREWRITE
                    '【プリペイドカードライト】
                    Me.lblMSG.Text = "カード情報を書き込んでいます。"
                    Me.btnCancel.Visible = False
                    Me.Refresh()
                    PreCardWrite()

                Case Command_Type.REPAIR
                    '【ｶｰﾄﾞ修復リード】
                    
                Case Else
                    Me.lblMSG.Text = "コマンドを指定してください。"
                    Me.lblMSG.ForeColor = Color.Red
                    Me.Refresh()
                    Thread.Sleep(2000)
                    Throw New Exception

            End Select

        Catch ex As Exception
            'MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            _blnERRFLG = True
        Finally
            If Not _intCommand.Equals(Command_Type.PREREAD) And Not _intCommand.Equals(Command_Type.PREWRITE) Then
                Me.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 取消ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.btnCancel.BackColor = Color.Orange
            Me.btnCancel.Refresh()

            _blnCANCEL = True

            If _intCommand.Equals(Command_Type.PREREAD) Or _intCommand.Equals(Command_Type.PREWRITE) Then
                'コマンド取消
                If Not _dcIA240.ST_Command Then
                    ErrDisp("コマンドの取消に失敗しました。(PRERW)")
                End If
            Else
                'コマンド取消
                If Not _dcICR700.Cancel Then
                    ErrDisp("コマンドの取消に失敗しました。")
                End If
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            Me.btnCancel.BackColor = Color.Gray
        End Try
    End Sub

    ''' <summary>
    ''' プリペイドカードリードタイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timPreRead_Tick(sender As System.Object, e As System.EventArgs) Handles timPreRead.Tick
        Dim intCnt As Integer
        Dim strErrNo As String = String.Empty
        Dim blnRead As Boolean = False
        Try
            'カード読み込み
            If _dcIA240.RD_Command(strErrNo) Then
                '【読み込み完了】
                If (Not _dcIA240.SHOPNO.Substring(2, 2).Equals(UIUtility.SYSTEM.SHOPNO.Substring(2, 2))) And Not (_dcIA240.SHOPNO.Equals("0000")) Then
                    '【店番不一致】
                    Me.timRead.Stop()
                    _blnERRSHOPFLG = True
                    'カード排出
                    Do
                        If _dcIA240.CO_Command Then
                            '【成功】
                            Me.Close()
                            Exit Sub
                        End If
                        If intCnt.Equals(10) Then
                            ErrDisp("カード排出に失敗しました。")
                            Exit Sub
                        End If
                        Thread.Sleep(100)
                        intCnt += 1
                    Loop
                    Me.Close()
                    Exit Sub
                End If
                blnRead = True
            Else
                If strErrNo.IndexOf("E 8") >= 0 Then
                    'If strErrNo.Equals("E 8") Then
                    '【失敗】
                    ErrDisp("カードの読み取りに失敗しました。")
                    'カード排出
                    Do
                        If _dcIA240.CO_Command Then
                            Exit Do
                        End If
                        If intCnt.Equals(10) Then
                            ErrDisp("カード排出に失敗しました。")
                            Exit Do
                        End If
                        Thread.Sleep(100)
                        intCnt += 1
                    Loop
                    Exit Sub
                Else
                    _dcIA240.TI_Command()
                    Thread.Sleep(100)
                End If
            End If

            If Not blnRead Then Exit Sub
            Thread.Sleep(300)
            Me.timPreRead.Stop()

            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

    ''' <summary>
    ''' 各ユニットのリセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetAllUnit()
        Try
            If Not _intCommand.Equals(Command_Type.PREREAD) And Not _intCommand.Equals(Command_Type.PREWRITE) Then
                ' ICRWのリセット
                _dcICR700.Cancel()
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' エラー時のメッセージ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ErrDisp(ByVal strMsg As String)
        Try
            Me.lblMSG.Text = strMsg
            Me.lblMSG.ForeColor = Color.Red
            Me.lblMSG.Refresh()

            ResetAllUnit()

            Thread.Sleep(2000)
            _blnERRFLG = True
            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 書き込みエラー時のメッセージ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ErrWriteDisp(ByVal strMsg As String)
        Dim blnRet As Boolean = False
        Try
            Me.lblMSG.Text = strMsg
            Me.lblMSG.ForeColor = Color.Red
            Me.lblMSG.Refresh()

            ResetAllUnit()

            Thread.Sleep(2000)
            _blnERRFLG = True

            ' 読込
            Me.lblMSG.Text = "復旧するカードを置いてください。"
            blnRet = Not CardRead(-1)

            ' 取り込みエラー
            If blnRet Then
                Me.Close()
                Exit Sub
            End If

            '【取引前の情報に戻す】
            _dcICR700.Repair(_dcICR700Bak)

            ' 書込み
            CardWrite(-1)
            Me.lblMSG.Text = "カードの復旧に成功しました。"

            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CardInit() As Boolean
        Try
            ' カード読み込み
            If Not CardRead() Then
                Return False
            End If

            '【顧客番号チェック】
            Dim num As Integer
            If Integer.TryParse(_dcICR700.NCSNO, num) Then
                If Not (num.Equals(0)) And num < 50000000 Then
                    Using frm As New frmMSGBOX01("既に顧客情報が書き込まれています。" & vbCr & "初期化してよろしいですか？", 1)
                        frm.ShowDialog()
                        If Not frm.Reply Then
                            '【キャンセル】
                            Return False
                        End If
                    End Using
                End If
            End If

            ' カード書き込み
            If Not CardWrite() Then
                Return False
            End If

            ' カード排出
            'If Not CardEject_Wait() Then
            '    Return False
            'End If

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    ''' <summary>
    ''' カード読込処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CardRead(Optional ByVal intTimeoutCount As Integer = 20) As Boolean
        Dim blnRet = False
        Try
            _dcICR700.Init()
            Do
                Dim ret = _dcICR700.Read(intTimeoutCount)

                Select Case ret
                    Case ICR700Control.eResult.PROCESSING
                        Me.lblMSG.Text = "カード読み取り中…"
                        Me.btnCancel.Visible = False

                    Case ICR700Control.eResult.CANCEL
                        Me.lblMSG.Text = "操作がキャンセルされました。"
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.TIMEOUT
                        ErrDisp("タイムアウトしました。")
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.vbERROR
                        ErrDisp("読込中にエラーが起こりました。")
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.SUCCESS
                        blnRet = True
                        Exit Do

                End Select

                Application.DoEvents()
            Loop

            Thread.Sleep(500)

            ' 取り込みエラー
            If Not blnRet Then Return False

            '【読み込み完了】
            If (Not _dcICR700.SHOPNO.Equals(UIUtility.SYSTEM.SHOPNO)) And Not (_dcICR700.SHOPNO.Equals("0000")) Then
                '【店番不一致】
                ErrDisp("店番が一致しません。")
                _blnERRSHOPFLG = True
                Return False
            End If

            ' カード区分チェック
            If _dcICR700.CARDKBN.Equals("2") Then
                ErrDisp("スクールカードは使用できません。")
                Return False
            End If

            'メンテナンスカードはここで抜ける
            If _dcICR700.SYUBETU.Equals("B") Or _dcICR700.SYUBETU.Equals("0") Then
                Return True
            End If

            '貸出カードはここで抜ける
            If _dcICR700.NCSNO >= "50000000" Then
                Return True
            End If

            ' *** STA ADD 2018/12/20 TERAYAMA DBの金額情報とカードの金額情報の矛盾チェック
            If Not _iDatabase Is Nothing Then
                Dim result = UIFunction.ChkCardKingaku(_iDatabase, _dcICR700)
                If result = UIFunction.eChkCardKingakuType.vbERROR Then
                    Dim bSuccess = UIFunction.UpdateKINSMAFromCard(_iDatabase, _dcICR700)
                    If UIUtility.SYSTEM.FUITCHIMSG Then
                        Using frm As New frmMSGBOX01("金額情報とカード金額が一致しませんでした。" + vbCrLf + "金額情報を更新します。", 3)
                            frm.ShowDialog()
                        End Using
                        If bSuccess Then
                            Using frm As New frmMSGBOX01("金額情報の更新に成功しました。", 0)
                                frm.ShowDialog()
                            End Using
                        Else
                            Using frm As New frmMSGBOX01("金額情報の更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                        End If
                    End If
                ElseIf result = UIFunction.eChkCardKingakuType.vbDBNOTHING Then
                    'Using frm As New frmMSGBOX01("金額情報が存在しません。", 2)
                    '    frm.ShowDialog()
                    'End Using
                End If
            End If
            ' *** END ADD

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' カード書き込み処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CardWrite(Optional ByVal intTimeoutCount As Integer = 20) As Boolean
        Me.lblMSG.Text = "カードに情報を書き込んでいます。"
        Me.lblMSG.Refresh()
        Dim blnRet As Boolean = False
        Try
            _dcICR700.Init()
            Do
                Dim ret = _dcICR700.Write(intTimeoutCount)

                Select Case ret
                    Case ICR700Control.eResult.PROCESSING
                        Me.lblMSG.Text = "カード書込み中…"
                        Me.btnCancel.Visible = False

                    Case ICR700Control.eResult.CANCEL
                        Me.lblMSG.Text = "操作がキャンセルされました。"
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.TIMEOUT
                        ErrDisp("タイムアウトしました。")
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.vbERROR
                        ErrWriteDisp("書込み中にエラーが起こりました。")
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.SUCCESS
                        blnRet = True
                        Exit Do

                End Select

                Application.DoEvents()
            Loop

            Thread.Sleep(500)

            Return blnRet

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' プリペイドカード書き込み処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreCardWrite()
        Dim strErrNo As String = String.Empty
        Dim intCnt As Integer = 0
        Try

            '【書き込みデータセット(PRERW)】
            Dim blnErr As Boolean = True
            intCnt = 0
            Do
                If _dcIA240.WD_Command() Then
                    blnErr = False
                    Exit Do
                End If
                If intCnt.Equals(3) Then Exit Do
                Thread.Sleep(500)
                intCnt += 1
            Loop
            If blnErr Then
                ErrDisp("書き込みデータセットに失敗しました。")
            End If


            If Not blnErr Then
                '【書き込み】
                blnErr = True
                intCnt = 0
                Do
                    If _dcIA240.WR_Command Then
                        blnErr = False
                        Exit Do
                    End If
                    If intCnt.Equals(3) Then Exit Do
                    Thread.Sleep(500)
                    intCnt += 1
                Loop
                If blnErr Then
                    ErrDisp("データの書き込みに失敗しました。")
                End If

            End If

            If Not _blnEJECTSTOP Then
                'カード排出コマンド
                Do
                    For i As Integer = 1 To 5
                        If _dcIA240.CO_Command Then
                            Exit Do
                        End If
                        If i.Equals(5) Then
                            Me.lblMSG.Text = "カード排出に失敗しました。"
                            Me.lblMSG.ForeColor = Color.Red
                            Me.lblMSG.Refresh()
                        End If
                    Next
                Loop
            End If

            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード排出処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CardEject_Wait() As Boolean

        ' ICカード排出はカードをリーダーから離すまで検出
        Dim blnRet As Boolean = False
        Try
            _dcICR700.Init()
            Do
                Dim ret = _dcICR700.EjectWait(-1)

                Select Case ret
                    Case ICR700Control.eResult.PROCESSING
                        Me.lblMSG.ForeColor = Color.Red
                        Me.lblMSG.Text = "カードをお取りください。"
                        Me.btnCancel.Visible = False

                    Case ICR700Control.eResult.CANCEL
                        Me.lblMSG.Text = "操作がキャンセルされました。"
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.TIMEOUT
                        ErrDisp("タイムアウトしました。")
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.vbERROR
                        ErrDisp("エラーが起こりました。")
                        blnRet = False
                        Exit Do

                    Case ICR700Control.eResult.SUCCESS
                        blnRet = True
                        Exit Do

                End Select

                Application.DoEvents()
            Loop

            'Thread.Sleep(500)
            Return blnRet

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
















End Class
