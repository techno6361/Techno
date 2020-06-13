Imports System.Threading
Imports System.ComponentModel
Imports TECHNO.DeviceControls

Public Class frmREQUESTCARDEx

#Region "▼宣言部"

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
    ''' バックアップ用
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700Bak As New TECHNO.DeviceControls.ICR700Control
    Private _backupMode As Boolean = False
    ''' <summary>
    ''' DB更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private _iDatabase As TECHNO.DataBase.IDatabase.IMethod
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
    End Enum

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
    ''' ﾁｪｯｸｲﾝ処理かどうか【True】ﾁｪｯｸｲﾝ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CHECKIN As Boolean
        Set(ByVal value As Boolean)
            _blnCHECKIN = value
        End Set
    End Property
    Private _blnCHECKIN As Boolean = False

    ''' <summary>
    ''' ﾁｪｯｸｱｳﾄ処理かどうか【True】ﾁｪｯｸｱｳﾄ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CHECKOUT As Boolean
        Set(ByVal value As Boolean)
            _blnCHECKOUT = value
        End Set
    End Property
    Private _blnCHECKOUT As Boolean = False

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
    ''' 後方吸引時エラーフラグ【True】エラー有り
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ERREIFLG As Boolean
        Get
            Return _blnERREIFLG
        End Get
    End Property
    Private _blnERREIFLG As Boolean = False

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

            ' すべてのコントロールにダブルバッファリングを有効化
            For Each c As Control In GetAllControls(Me)
                EnableDoubleBuffering(c)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(
                  ByVal ICR700 As Techno.DeviceControls.ICR700Control,
                  ByVal MCH3000 As Techno.DeviceControls.MCH3000Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()



            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            ' すべてのコントロールにダブルバッファリングを有効化
            For Each c As Control In GetAllControls(Me)
                EnableDoubleBuffering(c)
            Next

            '
            _dcICR700 = ICR700
            _dcMCH3000 = MCH3000

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(
                  ByVal ICR700 As Techno.DeviceControls.ICR700Control,
                  ByVal MCH3000 As Techno.DeviceControls.MCH3000Control,
                  ByVal iDatabase As Techno.DataBase.IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()



            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            ' すべてのコントロールにダブルバッファリングを有効化
            For Each c As Control In GetAllControls(Me)
                EnableDoubleBuffering(c)
            Next

            '
            _dcICR700 = ICR700
            _dcMCH3000 = MCH3000
            _iDatabase = iDatabase

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
            ' バックグランドワーカーの設定
            Me.mTimRead.WorkerSupportsCancellation = True
            Me.mTimRead.WorkerReportsProgress = True
            Me.mEjectCard.WorkerReportsProgress = True
            Me.mWriteCard.WorkerReportsProgress = True

            ' 位置調整
            Dim x = CInt((MyBase.ScreenSize.Width / 2) - (Me.picImage.Width / 2))
            Me.picImage.Location = New Point(x, Me.picImage.Location.Y)

            Me.btnCancel.Visible = False

            Me.timClose.Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 戻るタイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timClose_Tick(sender As System.Object, e As System.EventArgs) Handles timClose.Tick
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.mTimRead.CancelAsync()

            _blnCANCEL = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
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
            ' 搬送ユニット
            If UIUtility.SYSTEM.RWUnitKB = 1 Then
                If Not _dcMCH3000.Init Then
                    blnErrFlg = True
                End If
                If _dcMCH3000.CR_Command() Is Nothing Then
                    blnErrFlg = False
                End If
            End If

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
                Case Command_Type.READ
                    '【カードリード】
                    Me.lblMSG.Text = "初期化中…しばらくお待ちください。"
                    Me.lblMSG.Refresh()
                    Me.btnCancel.Visible = False
                    
                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    Me.mTimRead.RunWorkerAsync()

                Case Command_Type.WRITE
                    '【カードライト】
                    Me.btnCancel.Visible = False
                    If UIUtility.SYSTEM.RWUnitKB = 1 Then
                        Me.lblMSG.Text = "書込中…しばらくお待ちください。"
                    Else
                        Me.lblMSG.Text = "カードを指定の位置にセットしてください。"
                    End If
                    Me.picImage.Image = Images.GIFLoading
                    Me.lblMSG.Refresh()
                    
                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    ' あらかじめバックアップ
                    '_dcICR700Bak = _dcICR700.Clone

                    Me.mWriteCard.RunWorkerAsync()

                Case Command_Type.EJECT
                    '【カード排出】
                    Me.btnCancel.Visible = False
                    If UIUtility.SYSTEM.RWUnitKB = 1 Then
                        Me.lblMSG.Text = "カードを排出しています。"
                    Else
                        Me.lblMSG.Text = "カードをお取りください。"
                    End If
                    Me.picImage.Image = Images.GIFLoading
                    Me.lblMSG.Refresh()

                    ' ユニット初期化
                    If Not InitRWUnit() Then
                        Throw New Exception
                    End If

                    Me.mEjectCard.RunWorkerAsync()

                Case Else
                    Me.lblMSG.Text = "コマンドを指定してください。"
                    Me.lblMSG.ForeColor = Color.Red
                    Me.Refresh()
                    Thread.Sleep(2000)
                    _blnERRFLG = True
                    Me.Close()

            End Select

        Catch ex As Exception
            'MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            _dcMCH3000.Reset()
            _blnERRFLG = True

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

            PushAnimation(CType(sender, Control))

            Me.mTimRead.CancelAsync()

            _blnCANCEL = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' エラー時のメッセージ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ErrDisp(ByVal strMsg As String)
        Try
            Me.lblMSG.Text = strMsg
            Me.lblMSG.ForeColor = Color.Yellow
            Me.lblMSG.Refresh()
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
        Dim intCnt As Integer = 0
        Try
            Me.lblMSG.Text = strMsg
            Me.lblMSG.ForeColor = Color.Red
            Me.lblMSG.Refresh()
            Thread.Sleep(2000)
            _blnERRFLG = True

            '【取引前の情報に戻す】
            _dcICR700.Repair(_dcICR700Bak)

            '*** Sta Del 2019/06/22 Kitahara
            '_backupMode = True
            'Me.mWriteCard.RunWorkerAsync()
            '*** End Del 2019/06/22 Kitahara

            '_blnERRFLG = True
            'blnRet = Not CardRead(-1)

            '' 取り込みエラー
            'If blnRet Then
            '    Me.Close()
            '    Exit Sub
            'End If

            ''【取引前の情報に戻す】
            '_dcICR700.Repair(_dcICR700Bak)

            '' 書込み
            'CardWrite(-1)
            'Me.lblMSG.Text = "カードの復旧に成功しました。"

            'Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード排出処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CardEject()

        Me.picImage.Image = Images.GIFEjectCard
        Me.lblMSG.Text = "カード排出を行っています。"
        Me.lblMSG.Refresh()

        Me.mEjectCard.RunWorkerAsync()
        Exit Sub

        'Dim intCnt As Integer = 0
        'Try
        '    '【V31カード排出】
        '    intCnt = 0
        '    Do
        '        If _dcICR700.Eject(String.Empty) Then
        '            '【成功】
        '            Exit Do
        '        End If
        '        If intCnt.Equals(5) Then
        '            '【失敗】
        '            ErrDisp("カード排出に失敗しました。(RW②)")
        '            Exit Sub
        '        End If
        '        Thread.Sleep(200)
        '        intCnt += 1
        '    Loop

        '    '【後方吸引(PRERW)】
        '    intCnt = 0
        '    Do
        '        Me.lblMSG.Text = "カード排出を行っています。"
        '        Me.lblMSG.Refresh()
        '        If _dcICR700.EI_Command Then
        '            '【成功】
        '            Exit Do
        '        End If
        '        If intCnt.Equals(10) Then
        '            '【失敗】
        '            ErrDisp("後方吸引に失敗しました。(RW①)")
        '            Exit Sub
        '        End If
        '        Thread.Sleep(200)
        '        intCnt += 1
        '    Loop

        '    'カード排出コマンド
        '    If Not _blnEJECTSTOP Then
        '        If Not _dcICR700.CO_Command Then
        '            '【失敗】
        '            ErrDisp("カード排出に失敗しました。(RW①)")
        '            Exit Sub
        '        End If
        '    End If

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

#End Region

#Region "▼ スレッド"

    Enum eERROR
        INSERT
        WRITE
        EJECT
    End Enum

    ''' <summary>
    ''' 読み込み_ループ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mTimRead_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mTimRead.DoWork

        Dim result = New ResultModel()

        Try
            Dim states = _dcMCH3000.SR_Command
            If states(1) = 1 Then
                ' カードが既に入っている状態
                Me.mTimRead.ReportProgress(1)
            Else
                ' カード未挿入
                Me.mTimRead.ReportProgress(2)
                _dcMCH3000.Reset()

                ' カード挿入コマンド実行
                If Not _dcMCH3000.CA_Command(1) Then
                    result = New ResultModel(False, eERROR.INSERT)
                    Return
                End If
                Me.mTimRead.ReportProgress(0)
            End If

            ' ICRWリセット
            _dcICR700.Init()


            Dim intCnt As Integer = 1
            Try
                ' キャンセル通知があればスレッドをキャンセルする
                Do
                    If Me.mTimRead.CancellationPending Then
                        e.Cancel = True
                        Return
                    Else
                        states = _dcMCH3000.SR_Command()

                        ' 取り込み完了
                        If states(1) = 1 Then

                            Dim ret = _dcICR700.Read(100)

                            Select Case ret
                                Case ICR700Control.eResult.PROCESSING
                                    Me.mTimRead.ReportProgress(1)

                                Case ICR700Control.eResult.CANCEL
                                    result = New ResultModel(False, ICR700Control.eResult.CANCEL)
                                    Exit Do

                                Case ICR700Control.eResult.TIMEOUT
                                    result = New ResultModel(False, ICR700Control.eResult.TIMEOUT)
                                    Exit Do

                                Case ICR700Control.eResult.vbERROR
                                    result = New ResultModel(False, ICR700Control.eResult.vbERROR)
                                    Exit Do

                                Case ICR700Control.eResult.SUCCESS
                                    result = New ResultModel(True, ICR700Control.eResult.SUCCESS)
                                    Exit Do

                            End Select
                            intCnt += 1
                            If intCnt > 15 Then
                                Me.mTimRead.CancelAsync()
                                _blnCANCEL = True
                            End If
                        End If

                        'Application.DoEvents()
                    End If
                Loop

                e.Result = result
                Return

            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' 読み込み_スレッド終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mTimRead_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles mTimRead.RunWorkerCompleted
        Try
            ' スレッド終了
            If e.Cancelled Then
                'コマンド取消
                If Not _dcICR700.Cancel Then
                    ErrDisp("コマンドの取消に失敗しました。")
                    _blnERRFLG = True
                End If
                _dcMCH3000.Reset()
                Me.Close()
                Exit Sub
            End If

            Thread.Sleep(100)

            Dim result = CType(e.Result, ResultModel)

            Select Case result.ErrCode
                Case ICR700Control.eResult.CANCEL
                    ErrDisp("キャンセルされました。")
                    _blnERRFLG = True
                Case ICR700Control.eResult.TIMEOUT
                    ErrDisp("タイムアウトしました。")
                    _blnERRFLG = True
                Case ICR700Control.eResult.vbERROR
                    ErrDisp("カードの読み取りに失敗しました。")
                    _blnERRFLG = True
                Case ICR700Control.eResult.SUCCESS
                    _blnERRFLG = False
                Case eERROR.INSERT
                    ErrDisp("カードの取り込みに失敗しました。")
                    _blnERRFLG = True
            End Select

            If _blnERRFLG Then
                Me.Close()
                Exit Sub
            End If

            '【読み込み完了】
            If (Not _dcICR700.SHOPNO.Equals(UIUtility.SYSTEM.SHOPNO)) And Not (_dcICR700.SHOPNO.Equals("0000")) Then
                '【店番不一致】
                ErrDisp("店番が一致しません。")
                _blnERRSHOPFLG = True
                Me.Close()
                Exit Sub
            End If

            ' カード区分チェック
            If _dcICR700.CARDKBN.Equals("2") Then
                ErrDisp("スクールカードは使用できません。")
                _blnERRFLG = True
                Me.Close()
                Exit Sub
            End If

            ' *** STA ADD 2018/12/20 TERAYAMA DBの金額情報とカードの金額情報の矛盾チェック
            If Not _iDatabase Is Nothing Then
                Dim ret = UIFunction.ChkCardKingaku(_iDatabase, _dcICR700)
                If ret = UIFunction.eChkCardKingakuType.vbERROR Then
                    UIFunction.UpdateKINSMAFromCard(_iDatabase, _dcICR700)
                End If
            End If
            ' *** END ADD

            Me.Close()
            Exit Sub

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' 読み込み_出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mTimRead_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mTimRead.ProgressChanged
        Select Case e.ProgressPercentage
            Case 0
                If UIUtility.SYSTEM.RWUnitKB = 1 Then
                    Me.picImage.Image = Images.GIFInsertCard
                    Me.lblMSG.Text = "カードを入れてください。"
                Else
                    Me.picImage.Image = Images.GIFSetICCard
                    Me.lblMSG.Text = "カードを指定の位置にセットしてください。"
                End If
                Me.lblMSG.Refresh()
                Me.btnCancel.Visible = True
            Case 1
                Me.picImage.Image = Images.GIFLoading
                If UIUtility.SYSTEM.RWUnitKB = 1 Then
                    Me.lblMSG.Text = "読込中…しばらくお待ちください。"
                Else
                    Me.lblMSG.Text = "読込中…カードを動かさないでください。"
                End If
                Me.lblMSG.Refresh()
                Me.btnCancel.Visible = False
            Case 2
                Me.lblMSG.Text = "初期化中…しばらくお待ちください。"
                Me.picImage.Image = Images.GIFLoading
                Me.lblMSG.Refresh()
                Me.btnCancel.Visible = False
        End Select
    End Sub

    ''' <summary>
    ''' 書込み_ループ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mWriteCard_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mWriteCard.DoWork

        Dim result = New ResultModel()

        Try
            _dcICR700.Init()

            If _backupMode Then
                ' 復旧中
                Me.mWriteCard.ReportProgress(-1)
            Else
                ' 通常
                If Not Me.REQUEST_SKIP Then
                    'Me.mWriteCard.ReportProgress(0)
                End If
            End If

            Try
                ' キャンセル通知があればスレッドをキャンセルする
                Dim intCnt As Integer = 1
                Do
                    If Me.mTimRead.CancellationPending Then
                        e.Cancel = True
                        Return
                    Else
                        Dim ret = _dcICR700.Write(-1)

                        Select Case ret
                            Case ICR700Control.eResult.PROCESSING
                                Me.mWriteCard.ReportProgress(1)

                            Case ICR700Control.eResult.CANCEL
                                result = New ResultModel(False, ICR700Control.eResult.CANCEL)
                                Exit Do

                            Case ICR700Control.eResult.TIMEOUT
                                result = New ResultModel(False, ICR700Control.eResult.TIMEOUT)
                                Exit Do

                            Case ICR700Control.eResult.vbERROR
                                result = New ResultModel(False, ICR700Control.eResult.vbERROR)
                                Exit Do

                            Case ICR700Control.eResult.SUCCESS
                                result = New ResultModel(True, ICR700Control.eResult.SUCCESS)
                                Exit Do

                        End Select

                        'Application.DoEvents()
                    End If
                    intCnt += 1
                    If intCnt > 15 Then
                        result = New ResultModel(False, ICR700Control.eResult.vbERROR)
                        Exit Do
                    End If
                Loop

                e.Result = result
                Return

            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' 書込み_スレッド終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mWriteCard_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles mWriteCard.RunWorkerCompleted
        Try
            ' スレッド終了
            If e.Cancelled Then
                'コマンド取消
                If Not _dcICR700.Cancel Then
                    ErrDisp("コマンドの取消に失敗しました。")
                    _blnERRFLG = True
                End If
                Me.Close()
                Exit Sub
            End If

            Thread.Sleep(100)

            Dim result = CType(e.Result, ResultModel)

            Select Case result.ErrCode
                Case ICR700Control.eResult.CANCEL
                    ErrDisp("キャンセルされました。")
                    _blnERRFLG = True
                Case ICR700Control.eResult.TIMEOUT
                    ErrDisp("操作が取り消されました。")
                    _blnERRFLG = True
                Case ICR700Control.eResult.vbERROR
                    ErrWriteDisp("カードの書き込みに失敗しました。")
                    _blnERRFLG = True
                    '*** Sta Del 2019/06/22 Kitahara
                    'Exit Sub
                    '*** End De 2019/06/22 Kitahara
                Case ICR700Control.eResult.SUCCESS
                    If _backupMode Then
                        ' 復旧時は強制的にエラーを返す
                        _backupMode = False
                        _blnERRFLG = True
                    Else
                        ' 通常
                        _blnERRFLG = False
                    End If

            End Select

            If _blnERRFLG Then
                Me.Close()
                Exit Sub
            End If

            '【読み込み完了】
            If (Not _dcICR700.SHOPNO.Equals(UIUtility.SYSTEM.SHOPNO)) And Not (_dcICR700.SHOPNO.Equals("0000")) Then
                '【店番不一致】
                ErrDisp("店番が一致しません。")
                _blnERRSHOPFLG = True
                Me.Close()
                Exit Sub
            End If

            ' カード区分チェック
            If _dcICR700.CARDKBN.Equals("2") Then
                ErrDisp("スクールカードは使用できません。")
                _blnERRFLG = True
                Me.Close()
                Exit Sub
            End If

            'Sound.PlayReceiveCard()

            If _blnEJECTSTOP Then
                ' 排出停止モード
                Me.Close()
                Exit Sub
            Else
                ' 排出実行
                CardEject()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' 書込み_出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mWriteCard_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mWriteCard.ProgressChanged
        _blnERREIFLG = False
        Select Case e.ProgressPercentage
            Case 0 ' ICRWのみ
                Me.picImage.Image = Images.GIFSetICCard
                Me.lblMSG.Text = "カードを指定の位置にセットしてください。"
                Me.lblMSG.Refresh()
            Case 1
                Me.picImage.Image = Images.GIFLoading
                If UIUtility.SYSTEM.RWUnitKB = 1 Then
                    Me.lblMSG.Text = "書込中…しばらくお待ちください。"
                Else
                    Me.lblMSG.Text = "書込中…カードを動かさないでください。"
                End If
                Me.lblMSG.Refresh()
            Case -1
                If UIUtility.SYSTEM.RWUnitKB = 1 Then
                    Me.lblMSG.Text = "復帰中…しばらくお待ちください。"
                Else
                    Me.picImage.Image = Images.GIFSetICCard
                    Me.lblMSG.Text = "復旧中…カードを指定の位置にセットしてください。"
                End If
                Me.lblMSG.Refresh()
        End Select
    End Sub

    ''' <summary>
    ''' 排出_ループ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mEjectCard_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mEjectCard.DoWork
        Dim intCnt As Integer = 0
        Try
            If _blnEJECTSTOP Then
                e.Result = True
                Return
            End If

            'Me.mEjectCard.ReportProgress(0)

            If UIUtility.SYSTEM.RWUnitKB = 1 Then
                '【ユニット＆ICRW排出命令】
                _dcICR700.Cancel()
                
                ' 排出開始
                If Not _dcMCH3000.CE_Command() Then
                    Me.mEjectCard.ReportProgress(-1)
                    e.Result = False
                    Return
                End If

                Me.mEjectCard.ReportProgress(1)

                ' カードが抜かれるまで待機
                Dim blnErr = True
                For i = 0 To 1000
                    Dim status = _dcMCH3000.SR_Command
                    If status(0) = 0 Then
                        blnErr = False
                        Exit For
                    End If
                Next

                ' カード詰まり
                If blnErr Then
                    Me.mEjectCard.ReportProgress(-3)
                    Thread.Sleep(1000)
                    e.Result = False
                    Return
                End If

            Else
                '【ICRWのみ】
                intCnt = 0
                Do
                    If _dcICR700.EjectWait() = TECHNO.DeviceControls.ICR700Control.eResult.SUCCESS Then
                        '【成功】
                        Exit Do
                    End If
                    If intCnt.Equals(5) Then
                        '【失敗】
                        Me.mEjectCard.ReportProgress(-2)
                        e.Result = False
                        Return
                    End If
                    Thread.Sleep(200)
                    intCnt += 1
                Loop
            End If

            e.Result = True
            Return

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 排出_スレッド終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mEjectCard_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles mEjectCard.RunWorkerCompleted
        If CType(e.Result, Boolean) Then
            If UIUtility.SYSTEM.RWUnitKB = 1 Then
                _dcMCH3000.Reset()
            End If
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' 排出_出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mEjectCard_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mEjectCard.ProgressChanged
        Select Case e.ProgressPercentage
            Case 0
                Me.picImage.Image = Images.GIFLoading
                Me.lblMSG.Text = "カードを排出しています。"
                Me.lblMSG.Refresh()
            Case 1
                Me.picImage.Image = Images.GIFEjectCard
                Me.lblMSG.Text = "カードをお受け取りください。"
                Me.lblMSG.Refresh()
            Case -1
                _blnERREIFLG = True
                ErrDisp("カード排出コマンドに失敗しました。")
            Case -2
                _blnERREIFLG = True
                ErrDisp("カード排出に失敗しました。")
            Case -3
                _blnERREIFLG = True
                ErrDisp("カードが詰まっています。")
        End Select
    End Sub

#End Region

End Class
