Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase
Imports System.ComponentModel


Public Class frmSEATSERVER01

#Region "▼宣言部"
    ' IPCクライアント
    Public _ipc As IpcClient

    Private iDatabase As IDatabase.IMethod
    ''' <summary>
    ''' 打席通信用シリアルポート
    ''' </summary>
    ''' <remarks></remarks>
    Private _spSEATLINK As New SerialPort
    ''' <summary>
    ''' '業務終了ボタン押したときTrue←スレッド内で使用
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnEndFlg As Boolean = False
    ''' <summary>
    ''' 通信変換機またはCOMポートの異常時True
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnSeatCom_Err As Boolean = False
    ''' <summary>
    ''' 呼び出し制御関連
    ''' </summary>
    ''' <param name="lpstrCommand"></param>
    ''' <param name="lpstrRetrunString"></param>
    ''' <param name="uReturnLength"></param>
    ''' <param name="hwndCallback"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrRetrunString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer
    Private _strSoundBuffer As String = New String(Chr(0), 255)
    Private _strSoundMode As String
    Private _blnYOBIDASHI As Boolean = False

    ''' <summary>
    ''' 顧客種別
    ''' </summary>
    ''' <remarks></remarks>
    Private _intNCSRANK As Integer = 1

    ''' <summary>
    ''' 送信後受信待ち時間
    ''' </summary>
    ''' <remarks></remarks>
    Private _intSleep As Integer = 100

    ''' <summary>
    ''' ストップウォッチ
    ''' </summary>
    ''' <remarks></remarks>
    Private _sw As New System.Diagnostics.Stopwatch()
    Private _blnsw As Boolean = False

    Private ReadOnly worker As New BackgroundWorker()

    Private _intErrorCnt As Integer = 0

#End Region

#Region "▼コンストラクタ"
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        ' 先に非同期処理の本体や完了時処理を登録しておく
        AddHandler Me.worker.DoWork, AddressOf Me.OnDoWork
        AddHandler Me.worker.RunWorkerCompleted, AddressOf Me.OnCompleted

    End Sub
#End Region



#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATSERVER01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Location = New Point(0, 0)

            ' IPCに接続
            _ipc = New IpcClient

            '店番
            Try
                UIUtility.SYSTEM.SHOPNO = _ipc.SYSTEM.SHOPNO
            Catch ex As Exception
                UIUtility.SYSTEM.SHOPNO = "7706"
            End Try


            'データベース接続情報取得
            Dim DbInfo As String = "D:\GRAN31\DLL\DbInfo.xml"
            Try
                DbInfo = _ipc.SYSTEM.DbInfo_PATH
            Catch ex As Exception

            End Try


            '*** ＤＢ接続 ***'
            Dim dbControl As DatabaseControl = New DatabaseControl

            If dbControl.CreateDatabaseInstance(DatabaseControl.DatabaseKind.PostgreSQL) Then
                iDatabase = dbControl.IDB
                iDatabase.Connect(DbInfo)
                iDatabase.Open()
            End If

            'システム情報取得
            If Not GetSYSMTA() Then
                MessageBox.Show("エラー")
                Me.Close()
                Exit Sub
            End If

            '本日料金体系情報取得
            If Not GetRknInfo() Then
                MessageBox.Show("エラー")
                Me.Close()
                Exit Sub
            End If

            '営業情報マスタ取得
            If Not GetEIGMTA() Then
                MessageBox.Show("エラー")
                Me.Close()
                Exit Sub
            End If

            Try
                UIUtility.TABLE.SEATINFO = _ipc.SYSTEM.SEATINFO
            Catch ex As Exception
                '【打席情報保持用テーブル作成
                UIUtility.TABLE.SEATINFO = New DataTable
                '打席番号
                UIUtility.TABLE.SEATINFO.Columns.Add("SEATNO", GetType(Integer))
                '左区分
                UIUtility.TABLE.SEATINFO.Columns.Add("LEFTKB", GetType(Integer))
                '時間貸し区分
                UIUtility.TABLE.SEATINFO.Columns.Add("JKNKB", GetType(Integer))
                'フロア区分
                UIUtility.TABLE.SEATINFO.Columns.Add("FLRKB", GetType(Integer))
                '時間帯区分
                UIUtility.TABLE.SEATINFO.Columns.Add("TIMEKB", GetType(Integer))
                '打席状況
                UIUtility.TABLE.SEATINFO.Columns.Add("SEATSTATE", GetType(Integer))
                '使用総球数
                UIUtility.TABLE.SEATINFO.Columns.Add("ALLBALLCNT", GetType(Integer))
                '使用球数
                UIUtility.TABLE.SEATINFO.Columns.Add("USEBALLCNT", GetType(Integer))
                '使用開始時間
                UIUtility.TABLE.SEATINFO.Columns.Add("STARTTIME", GetType(Integer))
                '使用時間
                UIUtility.TABLE.SEATINFO.Columns.Add("USETIME", GetType(Integer))
                '顧客種別
                UIUtility.TABLE.SEATINFO.Columns.Add("NKBNO", GetType(Integer))
                'ボール単価
                UIUtility.TABLE.SEATINFO.Columns.Add("BALLKIN", GetType(Integer))
                '球数更新フラグ
                UIUtility.TABLE.SEATINFO.Columns.Add("UPDFLG", GetType(Integer))
                'レスポンスカウント
                UIUtility.TABLE.SEATINFO.Columns.Add("RSPCNT", GetType(Integer))
                '予約区分
                UIUtility.TABLE.SEATINFO.Columns.Add("RSVKBN", GetType(Integer))
                '顧客番号
                UIUtility.TABLE.SEATINFO.Columns.Add("NCSNO", GetType(String))
                '氏名
                UIUtility.TABLE.SEATINFO.Columns.Add("CCSNAME", GetType(String))
                '精算金額
                UIUtility.TABLE.SEATINFO.Columns.Add("SEISANKIN", GetType(Integer))
                '使用前残金額
                UIUtility.TABLE.SEATINFO.Columns.Add("ZANKN", GetType(Integer))
                '使用前P)残金額
                UIUtility.TABLE.SEATINFO.Columns.Add("PREZANKN", GetType(Integer))
                '使用前残ポイント
                UIUtility.TABLE.SEATINFO.Columns.Add("SRTPO", GetType(Integer))
                '顧客番号(DB取得時)
                UIUtility.TABLE.SEATINFO.Columns.Add("DBNCSNO", GetType(String))
                '前回来場日
                UIUtility.TABLE.SEATINFO.Columns.Add("ENTDT", GetType(String))

                Dim intLEFTKB As Integer
                Dim intFLRKB As Integer
                For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                    intLEFTKB = 0
                    intFLRKB = 0
                    If i <= UIUtility.SYSTEM.LSTNO1F Then
                        '【1F】
                        intFLRKB = 1
                    ElseIf i <= UIUtility.SYSTEM.LSTNO2F Then
                        '【2F】
                        intFLRKB = 2
                    Else
                        '【3F】
                        intFLRKB = 3
                    End If
                    UIUtility.TABLE.SEATINFO.Rows.Add(i, 0, 0, intFLRKB, 0, UIUtility.SEATSTATE.INIT_DATA, 0, 0, 0, 0, 0, 0, 0, 0, 0, String.Empty, String.Empty, 0, 0, 0, 0, String.Empty, String.Empty)
                    If UIUtility.SYSTEM.LRMULTI01.Equals(i) Or UIUtility.SYSTEM.LRMULTI02.Equals(i) Or UIUtility.SYSTEM.LRMULTI03.Equals(i) _
                                                                    Or UIUtility.SYSTEM.LRMULTI04.Equals(i) Or UIUtility.SYSTEM.LRMULTI05.Equals(i) Then
                        '【左右兼用打席】
                        UIUtility.TABLE.SEATINFO.Rows.Add(i, 1, 0, intFLRKB, 0, UIUtility.SEATSTATE.INIT_DATA, 0, 0, 0, 0, 0, 0, 0, 0, 0, String.Empty, String.Empty, 0, 0, 0, 0, String.Empty, String.Empty)
                    End If

                Next
            End Try

            '*** 打席通信開始 ***'
            'シリアルポートの名前
            Try
                _spSEATLINK.PortName = _ipc.SYSTEM.SEAT_COM
                _intSleep = _ipc.SYSTEM.SLEEP_TIME
            Catch ex As Exception
                _spSEATLINK.PortName = "COM3"
            End Try
            'シリアルポートの通信速度指定
            _spSEATLINK.BaudRate = 9600
            'シリアルポートのパリティ指定
            _spSEATLINK.Parity = IO.Ports.Parity.Even
            'シリアルポートのビット数指定
            _spSEATLINK.DataBits = 8
            'シリアルポートのストップビット指定
            _spSEATLINK.StopBits = IO.Ports.StopBits.Two
            'シリアルポートのタイムアウト指定
            _spSEATLINK.ReadTimeout = 500
            _spSEATLINK.WriteTimeout = 500
            _spSEATLINK.DtrEnable = True  'DTRをON
            _spSEATLINK.RtsEnable = True  'RSTをON

            'シリアルポートのオープン
            If Not _spSEATLINK.IsOpen Then
                _spSEATLINK.Open()

                '打席情報スレッド開始
                Me.worker.WorkerSupportsCancellation = True
                Me.worker.RunWorkerAsync()  ' 非同期処理開始

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' フォーム_FormClosing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATSERVER01_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    ''' <summary>
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATSERVER01_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            _spSEATLINK.Close()
            iDatabase.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region


#Region "▼関数定義"

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

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

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
            '指定打席区分
            UIUtility.SYSTEM.SITEIKBN = CType(resultDt.Rows(0).Item("SITEIKBN").ToString(), Integer)
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
            '優先単価
            UIUtility.SYSTEM.YUSENTANKA = CType(resultDt.Rows(0).Item("YUSENTANKA").ToString(), Integer)
            '時間貸し上限打球数
            UIUtility.SYSTEM.MAXJDCNT = CType(resultDt.Rows(0).Item("MAXJDCNT").ToString(), Integer)
            'カード発行手数料
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
            Sql.Append(" A.*")
            Sql.Append(",B.RKNNM")  '料金体系名称
            Sql.Append(",B.CLRR")   'カラーR
            Sql.Append(",B.CLRG")   'カラーG
            Sql.Append(",B.CLRB")   'カラーB
            Sql.Append(" FROM CALMTA AS A")
            Sql.Append(" LEFT JOIN RKNMTA AS B ON")
            Sql.Append(" B.RKNKB = A.RKNKB")
            Sql.Append(" WHERE")
            '----------検索条件----------'
            Sql.Append(" A.CALDT = '" & Now.Date.Year.ToString & Now.Date.Month.ToString.PadLeft(2, "0"c) & Now.Date.Day.ToString.PadLeft(2, "0"c) & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(Sql.ToString())

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
    Private Function GetEIGMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If Not UIUtility.TABLE.EIGMT Is Nothing Then
                UIUtility.TABLE.EIGMT.Clear()
            End If

            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM EIGMTA")
            sql.Append(" ORDER BY")
            sql.Append(" TIMEKB,NKBNO")

            UIUtility.TABLE.EIGMT = iDatabase.ExecuteRead(sql.ToString())

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
    ''' 打席情報ワーク更新
    ''' </summary>
    ''' <param name="intSEATNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdSEATWORK(ByVal intSEATNO As Integer, ByVal intLEFTKB As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim drow() As DataRow
        Try
            drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSEATNO & " AND LEFTKB = " & intLEFTKB)

            sql.Clear()
            sql.Append("UPDATE SEATWORK SET ")

            '左区分
            sql.Append("LEFTKB =" & CType(drow(0).Item("LEFTKB").ToString, Integer) & ",")
            '時間貸し区分
            sql.Append("JKNKB =" & CType(drow(0).Item("JKNKB").ToString, Integer) & ",")
            'フロア区分
            sql.Append("FLRKB =" & CType(drow(0).Item("FLRKB").ToString, Integer) & ",")
            '時間帯
            sql.Append("TIMEKB =" & CType(drow(0).Item("TIMEKB").ToString, Integer) & ",")
            '打席状況
            sql.Append("SEATSTATE =" & CType(drow(0).Item("SEATSTATE").ToString, Integer) & ",")
            '使用総球数
            sql.Append("ALLBALLCNT =" & CType(drow(0).Item("ALLBALLCNT").ToString, Integer) & ",")
            '使用球数
            sql.Append("USEBALLCNT =" & CType(drow(0).Item("USEBALLCNT").ToString, Integer) & ",")
            '使用開始時間
            sql.Append("STARTTIME =" & CType(drow(0).Item("STARTTIME").ToString, Integer) & ",")
            '使用時間
            sql.Append("USETIME =" & CType(drow(0).Item("USETIME").ToString, Integer) & ",")
            '顧客種別
            sql.Append("NKBNO =" & CType(drow(0).Item("NKBNO").ToString, Integer) & ",")
            '単価
            sql.Append("BALLKIN =" & CType(drow(0).Item("BALLKIN").ToString, Integer) & ",")
            '球数更新フラグ
            sql.Append("UPDFLG =" & CType(drow(0).Item("UPDFLG").ToString, Integer) & ",")
            '予約区分
            sql.Append("RSVKBN =" & CType(drow(0).Item("RSVKBN").ToString, Integer) & ",")
            '顧客番号
            sql.Append("NCSNO = '" & drow(0).Item("NCSNO").ToString & "',")
            '氏名
            sql.Append("CCSNAME = '" & drow(0).Item("CCSNAME").ToString & "',")
            '精算金額
            sql.Append("SEISANKIN =" & CType(drow(0).Item("SEISANKIN").ToString, Integer) & ",")
            '使用前残金額
            sql.Append("ZANKN =" & CType(drow(0).Item("ZANKN").ToString, Integer) & ",")
            '使用前P)残金額
            sql.Append("PREZANKN =" & CType(drow(0).Item("PREZANKN").ToString, Integer) & ",")
            '使用前残ポイント
            sql.Append("SRTPO =" & CType(drow(0).Item("SRTPO").ToString, Integer) & ",")
            '顧客番号(DB取得時)
            sql.Append("DBNCSNO ='" & drow(0).Item("DBNCSNO").ToString & "',")
            '前回来場日
            sql.Append("ENTDT ='" & drow(0).Item("ENTDT").ToString & "',")


            sql.Append("UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" SEATDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND LEFTKB = " & intLEFTKB)
            sql.Append(" AND SEATNO = " & intSEATNO)


            If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                Dim textFile As System.IO.StreamWriter
                textFile = New System.IO.StreamWriter("D:\GRAN31\Log\" & Now.ToString("yyyyMMdd") & "log.txt", True, System.Text.Encoding.Default)
                textFile.WriteLine(Now.ToString("HH:mm") & sql.ToString)
                textFile.Close()
                Return False
            End If


            Return True

        Catch ex As Exception
            Dim textFile As System.IO.StreamWriter
            textFile = New System.IO.StreamWriter("D:\GRAN31\Log\" & Now.ToString("yyyyMMdd") & "log.txt", True, System.Text.Encoding.Default)
            textFile.WriteLine(Now.ToString("HH:mm") & sql.ToString)
            textFile.Close()
            'MessageBox.Show("打席情報ワーク更新エラー")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 打席情報サマリC更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdSEATSMC() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intJIKAN As Integer = Now.Hour + 1
        Try
            Try
                UIUtility.ENTRY.NKBNO11_CNT = _ipc.SYSTEM.NKBNO11_CNT
                UIUtility.ENTRY.NKBNO12_CNT = _ipc.SYSTEM.NKBNO12_CNT
                UIUtility.ENTRY.NKBNO13_CNT = _ipc.SYSTEM.NKBNO13_CNT
                UIUtility.ENTRY.ENT_CNT = _ipc.SYSTEM.ENT_CNT
                UIUtility.SYSTEM.SUMBALL = _ipc.SYSTEM.SUMBALL
            Catch ex As Exception
            End Try

            sql.Clear()
            sql.Append("UPDATE SEATSMC SET ")

            'サービス人数
            sql.Append("SERVICECNT =" & UIUtility.ENTRY.NKBNO11_CNT + UIUtility.ENTRY.NKBNO12_CNT + UIUtility.ENTRY.NKBNO13_CNT & ",")
            '入場者数
            sql.Append("ENTCNT =" & UIUtility.ENTRY.ENT_CNT & ",")
            '打球数
            sql.Append("BALLSU =" & UIUtility.SYSTEM.SUMBALL & ",")
            sql.Append("UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" SEATDT = '" & Now.ToString("yyyyMMdd") & "'")
            If Now.Hour.Equals(24) Then intJIKAN = 0
            sql.Append(" AND JIKAN = '" & intJIKAN.ToString.PadLeft(2, "0"c) & "'")

            If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then Return False

            Return True

        Catch ex As Exception
            MessageBox.Show("打席情報サマリC更新エラー")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 打席情報サマリ更新
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdSEATINFO(ByVal drow() As DataRow) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strNKBBALL As String = String.Empty
        Dim strNKBKIN As String = String.Empty
        Dim strNKBTAXKIN As String = String.Empty
        Dim strNKBTAX As String = String.Empty
        Dim strNKBDOSU As String = String.Empty


        Dim intKIN As Integer = 0
        Dim intUSEBALLCNT As Integer = 0
        Dim intBALLKIN As Integer = 0

        Dim strUpdNCSNO As String = "00000000"
        Dim strENTDT As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(drow(0).Item("DBNCSNO").ToString) Then
                For i As Integer = 0 To drow(0).Item("CCSNAME").ToString.Length - 1
                    If drow(0).Item("CCSNAME").ToString.Substring(i, 1).Equals("【") Then
                        strUpdNCSNO = drow(0).Item("CCSNAME").ToString.Substring(i + 1, 8)
                        Exit For
                    End If
                Next
                'strUpdNCSNO = drow(0).Item("DBNCSNO").ToString
            End If
            If Not String.IsNullOrEmpty(drow(0).Item("ENTDT").ToString) Then
                strENTDT = drow(0).Item("ENTDT").ToString
            End If


            'ICカード残金
            Dim intIcZANKN As Integer = CType(drow(0).Item("ZANKN").ToString, Integer)
            'ICカード残プレミアム
            Dim intIcPREZANKN As Integer = CType(drow(0).Item("PREZANKN").ToString, Integer)


            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0
            '精算金額
            Dim intSEISANKIN As Integer = CType(drow(0).Item("SEISANKIN").ToString, Integer)

            If intIcPREZANKN >= intSEISANKIN Then
                '【残プレミアム >= 入場料金】
                intIcPREZANKN -= intSEISANKIN
                'プレミアムから支払った金額
                intPayPREMKN = intSEISANKIN
            Else
                intIcZANKN = intIcZANKN - (intSEISANKIN - intIcPREZANKN)
                '残金から支払った金額
                intPayKINGAKU = (intSEISANKIN - intIcPREZANKN)
                'プレミアムから支払った金額
                intPayPREMKN = intIcPREZANKN
                intIcPREZANKN = 0
            End If

            'トランザクション開始
            iDatabase.BeginTransaction()

            If intSEISANKIN > 0 Then
                '金額情報更新
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & intIcZANKN)
                sql.Append(",PREZANKN = " & intIcPREZANKN)
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & strUpdNCSNO & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    MessageBox.Show("金額サマリの更新に失敗しました。")
                    iDatabase.RollBack()
                    Return False
                End If

                'ボール料金更新
                UIFunction.UpdBALLTRN(CType(strUpdNCSNO, Integer) _
                                      , drow(0).Item("NKBNO").ToString _
                                      , Now.ToString("yyyyMMdd") _
                                      , CType(drow(0).Item("SEISANKIN").ToString, Integer) _
                                      , intPayKINGAKU _
                                      , intPayPREMKN _
                                      , CType(drow(0).Item("USEBALLCNT").ToString, Integer) _
                                      , CType(drow(0).Item("ZANKN").ToString, Integer) _
                                      , intIcZANKN _
                                      , CType(drow(0).Item("PREZANKN").ToString, Integer) _
                                      , intIcPREZANKN _
                                      , CType(drow(0).Item("SRTPO").ToString, Integer) _
                                      , CType(drow(0).Item("SRTPO").ToString, Integer) _
                                      , iDatabase)
            ElseIf CType(drow(0).Item("NKBNO").ToString, Integer).Equals(12) Then
                'ボール料金更新
                UIFunction.UpdBALLTRN(CType(strUpdNCSNO, Integer) _
                                      , drow(0).Item("NKBNO").ToString _
                                      , Now.ToString("yyyyMMdd") _
                                      , CType(drow(0).Item("SEISANKIN").ToString, Integer) _
                                      , intPayKINGAKU _
                                      , intPayPREMKN _
                                      , CType(drow(0).Item("USEBALLCNT").ToString, Integer) _
                                      , CType(drow(0).Item("ZANKN").ToString, Integer) _
                                      , intIcZANKN _
                                      , CType(drow(0).Item("PREZANKN").ToString, Integer) _
                                      , intIcPREZANKN _
                                      , CType(drow(0).Item("SRTPO").ToString, Integer) _
                                      , CType(drow(0).Item("SRTPO").ToString, Integer) _
                                      , iDatabase)
            End If

            If CType(drow(0).Item("NKBNO").ToString, Integer).Equals(0) Then
                strNKBBALL = "NKB01BALL"
                strNKBKIN = "NKB01KIN"
                strNKBTAXKIN = "NKB01TAXKIN"
                strNKBTAX = "NKB01TAX"
                strNKBDOSU = "NKB01DOSU"
            Else
                strNKBBALL = "NKB" & CType(drow(0).Item("NKBNO").ToString, Integer).ToString.PadLeft(2, "0"c) & "BALL"
                strNKBKIN = "NKB" & CType(drow(0).Item("NKBNO").ToString, Integer).ToString.PadLeft(2, "0"c) & "KIN"
                strNKBTAXKIN = "NKB" & CType(drow(0).Item("NKBNO").ToString, Integer).ToString.PadLeft(2, "0"c) & "TAXKIN"
                strNKBTAX = "NKB" & CType(drow(0).Item("NKBNO").ToString, Integer).ToString.PadLeft(2, "0"c) & "TAX"
                strNKBDOSU = "NKB" & CType(drow(0).Item("NKBNO").ToString, Integer).ToString.PadLeft(2, "0"c) & "DOSU"
            End If

            '*** SEATSMA更新 ***'
            sql.Clear()
            sql.Append("UPDATE SEATSMA SET ")
            '打球数
            sql.Append(strNKBBALL & " = " & strNKBBALL & " + " & CType(drow(0).Item("USEBALLCNT").ToString, Integer))

            intUSEBALLCNT = CType(drow(0).Item("USEBALLCNT").ToString, Integer)
            intBALLKIN = CType(drow(0).Item("BALLKIN").ToString, Integer)
            '金額
            intKIN = CType(intUSEBALLCNT * (intBALLKIN + UIFunction.GetTaxTanka(intBALLKIN)), Integer)
            sql.Append("," & strNKBKIN & " = " & strNKBKIN & " + " & intKIN)
            '消費税金額
            sql.Append("," & strNKBTAXKIN & " = " & strNKBTAXKIN & " + " & UIFunction.CalcExcludedTax(intKIN))
            '消費税率
            sql.Append("," & strNKBTAX & " = " & UIUtility.SYSTEM.TAX)
            '度数
            sql.Append("," & strNKBDOSU & " = " & strNKBDOSU & " + 1")
            '更新日時
            sql.Append(",UPDDTM = NOW() ")

            sql.Append("WHERE ")
            sql.Append("SEATDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND FLRKB = " & CType(drow(0).Item("FLRKB").ToString, Integer))
            sql.Append(" AND TIMEKB = " & CType(drow(0).Item("TIMEKB").ToString, Integer))

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                MessageBox.Show("打席情報サマリA更新失敗")
                iDatabase.RollBack()
                Return False
            End If
            '*** SEATSMB更新 ***'
            sql.Clear()
            sql.Append("UPDATE SEATSMB SET ")

            '打球数
            sql.Append("BALL" & drow(0).Item("SEATNO").ToString.PadLeft(3, "0"c) & " = (BALL" & drow(0).Item("SEATNO").ToString.PadLeft(3, "0"c) & " + " & CType(drow(0).Item("USEBALLCNT").ToString, Integer) & ")")
            '度数
            sql.Append(",DOSU" & drow(0).Item("SEATNO").ToString.PadLeft(3, "0"c) & " = (DOSU" & drow(0).Item("SEATNO").ToString.PadLeft(3, "0"c) & " + 1) ")
            '利用時間
            sql.Append(",TIME" & drow(0).Item("SEATNO").ToString.PadLeft(3, "0"c) & " = (TIME" & drow(0).Item("SEATNO").ToString.PadLeft(3, "0"c) & " + " & CType(drow(0).Item("USETIME").ToString, Integer) & ")")

            '更新日時
            sql.Append(",UPDDTM = NOW() ")

            sql.Append("WHERE ")
            sql.Append("SEATDT = '" & Now.ToString("yyyyMMdd") & "'")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                MessageBox.Show("打席情報サマリB更新失敗")
                iDatabase.RollBack()
                Return False
            End If

            'コミット
            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show("打席情報サマリB更新エラー")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 打席予約状況取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSEATRSV() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim dr As DataRow()
        Dim intSEATNO As Integer = 0
        Try


            For i As Integer = 0 To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                intSEATNO = CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString, Integer)
                dr = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSEATNO)
                '通常打席
                dr(0).Item("RSVKBN") = 0
                dr(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSEATNO) _
                                Or UIUtility.SYSTEM.LRMULTI04.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSEATNO) Then
                    '予約打席
                    dr(1).Item("RSVKBN") = 0
                    dr(1).EndEdit()
                End If
            Next

            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEATRSV")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" SEATDT = '" & Now.ToString("yyyyMMdd") & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt.Rows.Count - 1
                intSEATNO = CType(resultDt.Rows(i).Item("SEATNO").ToString, Integer)

                dr = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSEATNO)

                '予約打席
                dr(0).Item("RSVKBN") = resultDt.Rows(i).Item("RSVKBN").ToString
                dr(0).Item("CCSNAME") = resultDt.Rows(i).Item("CCSNAME").ToString
                dr(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSEATNO) _
                                Or UIUtility.SYSTEM.LRMULTI04.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSEATNO) Then
                    '予約打席
                    dr(1).Item("RSVKBN") = resultDt.Rows(i).Item("RSVKBN").ToString
                    dr(1).Item("CCSNAME") = resultDt.Rows(i).Item("CCSNAME").ToString
                    dr(1).EndEdit()
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCsInfo(ByRef drow() As DataRow, ByRef strDMEMBER As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intSEATNO As Integer = 0
        Dim intENTCNT2 As Integer = 0
        Dim intGetPOINT As Integer = 0
        Try
            strDMEMBER = String.Empty
            _intNCSRANK = 1
            If drow(0).Item("NCSNO").Equals("00000000") Then
                _intNCSRANK = 11
            End If

            drow(0).Item("CCSNAME") = String.Empty
            drow(0).Item("ZANKN") = 0
            drow(0).Item("PREZANKN") = 0
            drow(0).Item("SRTPO") = 0
            drow(0).Item("DBNCSNO") = String.Empty
            drow(0).Item("ENTDT") = String.Empty

            sql.Append(" SELECT")
            sql.Append(" A.NCSNO")
            sql.Append(",A.NCARDID")
            sql.Append(",A.NCSRANK")
            sql.Append(",A.CCSNAME")
            sql.Append(",A.CARDLIMIT")
            sql.Append(",A.ENTDT")
            sql.Append(",A.ENTCNT2")
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYYMMDD') AS TO_DMEMBER") '会員期限
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYYMMDD') AS TO_DBIRTH")  '誕生日
            sql.Append(",B.ZANKN")
            sql.Append(",B.PREZANKN")
            sql.Append(",C.SRTPO")
            sql.Append(",D.NCARDID AS STPFLG")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DCSTPTRN AS D ON D.NCSNO = A.NCSNO AND D.NCARDID = A.NCARDID")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" A.NCARDID = " & CType(drow(0).Item("NCSNO"), Integer))

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) And (Not CType(drow(0).Item("NCSNO"), Integer).Equals(0) And Not CType(drow(0).Item("NCSNO"), Integer) >= 50000000) Then
                '再発行の為使用不可
                Return True
            End If

            If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("CARDLIMIT").ToString) Then
                'カード残高有効期限切れ
                If Now.ToString("yyyyMMdd") > resultDt.Rows(0).Item("CARDLIMIT").ToString Then
                    Return True
                End If
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                '顧客種別保持
                If drow(0).Item("NCSNO").Equals("00000000") Then
                    _intNCSRANK = 11
                Else
                    _intNCSRANK = CType(resultDt.Rows(i).Item("NCSRANK").ToString, Integer)

                    drow(0).Item("CCSNAME") = resultDt.Rows(i).Item("CCSNAME").ToString & "【" & resultDt.Rows(i).Item("NCSNO").ToString.PadLeft(8, "0"c) & "】"
                    drow(0).Item("ZANKN") = CType(resultDt.Rows(i).Item("ZANKN").ToString, Integer)
                    drow(0).Item("PREZANKN") = CType(resultDt.Rows(i).Item("PREZANKN").ToString, Integer)
                    drow(0).Item("SRTPO") = CType(resultDt.Rows(i).Item("SRTPO").ToString, Integer)
                    drow(0).Item("DBNCSNO") = resultDt.Rows(i).Item("NCARDID").ToString.PadLeft(8, "0"c)
                    drow(0).Item("ENTDT") = resultDt.Rows(i).Item("ENTDT").ToString
                    If resultDt.Rows(i).Item("NCSRANK").ToString.Equals("3") Then
                        '期間限定メンバーの時のみ
                        strDMEMBER = resultDt.Rows(i).Item("TO_DMEMBER").ToString
                    End If
                    intENTCNT2 = CType(resultDt.Rows(i).Item("ENTCNT2").ToString, Integer)

                    '*** Sta Add 2019/12/04 Kitahara
                    '【顧客情報更新】
                    sql.Clear()
                    sql.Append("UPDATE CSMAST SET")
                    sql.Append(" ZENENTDATE = '" & Now.ToString("yyyyMMdd") & "'")
                    sql.Append(",ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
                    If Not resultDt.Rows(i).Item("ENTDT").ToString.Equals(Now.ToString("yyyyMMdd")) Then
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

                    'sql.Append(",DUPDATE = NOW()")
                    sql.Append(" WHERE")
                    sql.Append(" NCSNO = " & CType(resultDt.Rows(i).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        MessageBox.Show("顧客情報の更新に失敗しました。")
                        iDatabase.RollBack()
                        Return False
                    End If
                    '無人営業時入場処理
                    If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                        Dim strSQL1 As String = String.Empty
                        Dim strSQL2 As String = String.Empty

                        If Not resultDt.Rows(i).Item("ENTDT").ToString.Equals(Now.ToString("yyyyMMdd")) Then
                            '【本日初来場の場合 月間来場ポイント確認】

                            '月間来場ポイント情報取得
                            sql.Clear()
                            sql.Append("SELECT")
                            sql.Append(" *")
                            sql.Append(" FROM")
                            sql.Append(" ETPMTA")
                            sql.Append(" WHERE")
                            sql.Append(" NKBNO = " & _intNCSRANK.ToString)
                            sql.Append(" AND ENTCNT =" & (intENTCNT2 + 1))
                            sql.Append(" ORDER BY ENTCNT")

                            Dim dtETPMTA As DataTable = iDatabase.ExecuteRead(sql.ToString)
                            intGetPOINT = 0
                            If Not dtETPMTA.Rows.Count.Equals(0) Then
                                intGetPOINT = CType(dtETPMTA.Rows(0).Item("POINT").ToString, Integer)
                            End If

                            'ポイントマスタ情報取得
                            Dim intBIRTHMPO As Integer = 0
                            Dim intBIRTHDPO As Integer = 0
                            If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("TO_DBIRTH").ToString) Then

                                Try
                                    UIFunction.GetPOINTMST(intBIRTHMPO, intBIRTHDPO, iDatabase)
                                Catch ex As Exception
                                    'エラー無視
                                End Try

                                If resultDt.Rows(0).Item("TO_DBIRTH").ToString.Substring(4, 4).Equals(Now.ToString("MMdd")) Then
                                    '【本日誕生日】

                                ElseIf resultDt.Rows(0).Item("TO_DBIRTH").ToString.Substring(4, 2).Equals(Now.ToString("MM")) Then
                                    '【誕生月】

                                    intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0

                                Else
                                    intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
                                    intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
                                End If
                                intGetPOINT = intGetPOINT + intBIRTHMPO + intBIRTHDPO
                            End If

                        End If

                        iDatabase.BeginTransaction()

                        If intGetPOINT > 0 Then
                            '【ポイントサマリ】
                            sql.Clear()
                            sql.Append("UPDATE DPOINTSMA SET")
                            sql.Append(" SRTPO = SRTPO +" & +intGetPOINT)
                            sql.Append(",UPDDTM = NOW()")
                            sql.Append(" WHERE")
                            sql.Append(" MANNO = '" & resultDt.Rows(i).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                                MessageBox.Show("ポイントサマリの更新に失敗しました。")
                                iDatabase.RollBack()
                                Return False
                            End If
                        End If

                        strSQL1 &= "INSERT INTO ENTTRB("
                        strSQL2 &= " VALUES("
                        '伝票日付
                        strSQL1 &= "ENTDT,"
                        strSQL2 &= "'" & Now.ToString("yyyyMMdd") & "',"
                        '顧客番号
                        strSQL1 &= "MANNO,"
                        strSQL2 &= "'" & resultDt.Rows(i).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
                        '顧客種別区分
                        strSQL1 &= "KSBKB,"
                        strSQL2 &= "'" & _intNCSRANK & "',"
                        '料金体系
                        strSQL1 &= "RKNKB,"
                        strSQL2 &= "'" & UIUtility.SYSTEM.RKNKB & "',"
                        '時間帯コード
                        strSQL1 &= "TIMCD,"
                        strSQL2 &= "'" & UIUtility.SYSTEM.NOWTIMEKB & "',"
                        '時間帯
                        strSQL1 &= "TIMTM,"
                        strSQL2 &= "NULL,"
                        'ポイント
                        strSQL1 &= "POINT,"
                        strSQL2 &= intGetPOINT & ","
                        '処理フラグ(ポイント加算)
                        strSQL1 &= "POINTFLG,"
                        strSQL2 &= "0,"
                        '作成日時
                        strSQL1 &= "INSDTM)"
                        strSQL2 &= "NOW())"

                        If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                            MessageBox.Show("入場トラン(無人用)の更新に失敗しました。")
                            iDatabase.RollBack()
                            Return False
                        End If

                        iDatabase.Commit()
                    End If
                    '*** End Add 2019/12/04 Kitahara
                End If
            Next

            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                drow(0).Item("NKBNO") = _intNCSRANK
            End If

            If CType(drow(0).Item("NCSNO"), Integer) >= 50000000 Then
                drow(0).Item("CCSNAME") = "貸出カード"
            End If

            drow(0).EndEdit()

            'If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("CARDLIMIT").ToString) Then
            '    'カード残高有効期限切れ
            '    If Now.ToString("yyyyMMdd") > resultDt.Rows(0).Item("CARDLIMIT").ToString Then
            '        Return True
            '    End If
            'End If
            If String.IsNullOrEmpty(resultDt.Rows(0).Item("STPFLG").ToString) Then
                Return False
            Else
                'カード停止中
                Return True
            End If

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' シートワークを再作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResetSeatWork(ByVal intKbn As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Try
            iDatabase.BeginTransaction()

            If Not (iDatabase.ExecuteUpdate("DELETE FROM SEATWORK")) Then
                iDatabase.RollBack()
                Return False
            End If

            If intKbn.Equals(1) Then
                '右打席
                For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                    strSQL01 = "INSERT INTO SEATWORK ("
                    strSQL02 = "VALUES ("

                    '営業日付
                    strSQL01 &= "SEATDT, "
                    strSQL02 &= "'" & Now.ToString("yyyyMMdd") & "',"

                    '打席№
                    strSQL01 &= "SEATNO, "
                    strSQL02 &= i & ","
                    '左区分
                    strSQL01 &= "LEFTKB, "
                    strSQL02 &= "0,"
                    '時間貸し区分
                    strSQL01 &= "JKNKB, "
                    strSQL02 &= "0,"
                    'フロア区分
                    strSQL01 &= "FLRKB, "
                    strSQL02 &= "0,"
                    '時間帯
                    strSQL01 &= "TIMEKB, "
                    strSQL02 &= "0,"
                    '打席状況
                    strSQL01 &= "SEATSTATE, "
                    strSQL02 &= "0,"
                    '使用総球数
                    strSQL01 &= "ALLBALLCNT, "
                    strSQL02 &= "0,"
                    '使用球数
                    strSQL01 &= "USEBALLCNT, "
                    strSQL02 &= "0,"
                    '使用開始時間
                    strSQL01 &= "STARTTIME, "
                    strSQL02 &= "0,"
                    '使用時間
                    strSQL01 &= "USETIME, "
                    strSQL02 &= "0,"
                    '顧客種別
                    strSQL01 &= "NKBNO, "
                    strSQL02 &= "0,"
                    'ボール単価
                    strSQL01 &= "BALLKIN, "
                    strSQL02 &= "0,"
                    '球数更新フラグ
                    strSQL01 &= "UPDFLG, "
                    strSQL02 &= "0,"
                    '予約区分
                    strSQL01 &= "RSVKBN, "
                    strSQL02 &= "0,"
                    '顧客番号
                    strSQL01 &= "NCSNO, "
                    strSQL02 &= "NULL,"
                    '氏名
                    strSQL01 &= "CCSNAME, "
                    strSQL02 &= "NULL,"
                    '精算金額
                    strSQL01 &= "SEISANKIN, "
                    strSQL02 &= "0,"
                    '使用前残金額
                    strSQL01 &= "ZANKN, "
                    strSQL02 &= "0,"
                    '使用前P)残金額
                    strSQL01 &= "PREZANKN, "
                    strSQL02 &= "0,"
                    '使用前残ポイント
                    strSQL01 &= "SRTPO, "
                    strSQL02 &= "0,"
                    '顧客番号(DB取得時)
                    strSQL01 &= "DBNCSNO, "
                    strSQL02 &= "NULL,"
                    '前回来場日
                    strSQL01 &= "ENTDT, "
                    strSQL02 &= "NULL,"
                    '作成日時
                    strSQL01 &= "INSDTM,"
                    strSQL02 &= "NOW(),"
                    '更新日時
                    strSQL01 &= "UPDDTM"
                    strSQL02 &= "NOW()"

                    strSQL01 &= ") "
                    strSQL02 &= ")"

                    If Not (iDatabase.ExecuteUpdate(strSQL01 & strSQL02)) Then
                        iDatabase.RollBack()
                        Return False
                    End If

                Next
                '左打席
                For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                    If UIUtility.SYSTEM.LRMULTI01.Equals(i) Or UIUtility.SYSTEM.LRMULTI02.Equals(i) Or UIUtility.SYSTEM.LRMULTI03.Equals(i) _
                                    Or UIUtility.SYSTEM.LRMULTI04.Equals(i) Or UIUtility.SYSTEM.LRMULTI05.Equals(i) Then

                        strSQL01 = "INSERT INTO SEATWORK ("
                        strSQL02 = "VALUES ("

                        '営業日付
                        strSQL01 &= "SEATDT, "
                        strSQL02 &= "'" & Now.ToString("yyyyMMdd") & "',"

                        '打席№
                        strSQL01 &= "SEATNO, "
                        strSQL02 &= i & ","
                        '左区分
                        strSQL01 &= "LEFTKB, "
                        strSQL02 &= "1,"
                        '時間貸し区分
                        strSQL01 &= "JKNKB, "
                        strSQL02 &= "0,"
                        'フロア区分
                        strSQL01 &= "FLRKB, "
                        strSQL02 &= "0,"
                        '時間帯
                        strSQL01 &= "TIMEKB, "
                        strSQL02 &= "0,"
                        '打席状況
                        strSQL01 &= "SEATSTATE, "
                        strSQL02 &= "0,"
                        '使用総球数
                        strSQL01 &= "ALLBALLCNT, "
                        strSQL02 &= "0,"
                        '使用球数
                        strSQL01 &= "USEBALLCNT, "
                        strSQL02 &= "0,"
                        '使用開始時間
                        strSQL01 &= "STARTTIME, "
                        strSQL02 &= "0,"
                        '使用時間
                        strSQL01 &= "USETIME, "
                        strSQL02 &= "0,"
                        '顧客種別
                        strSQL01 &= "NKBNO, "
                        strSQL02 &= "0,"
                        'ボール単価
                        strSQL01 &= "BALLKIN, "
                        strSQL02 &= "0,"
                        '球数更新フラグ
                        strSQL01 &= "UPDFLG, "
                        strSQL02 &= "0,"
                        '予約区分
                        strSQL01 &= "RSVKBN, "
                        strSQL02 &= "0,"
                        '顧客番号
                        strSQL01 &= "NCSNO, "
                        strSQL02 &= "NULL,"
                        '氏名
                        strSQL01 &= "CCSNAME, "
                        strSQL02 &= "NULL,"
                        '精算金額
                        strSQL01 &= "SEISANKIN, "
                        strSQL02 &= "0,"
                        '使用前残金額
                        strSQL01 &= "ZANKN, "
                        strSQL02 &= "0,"
                        '使用前P)残金額
                        strSQL01 &= "PREZANKN, "
                        strSQL02 &= "0,"
                        '使用前残ポイント
                        strSQL01 &= "SRTPO, "
                        strSQL02 &= "0,"
                        '顧客番号(DB取得時)
                        strSQL01 &= "DBNCSNO, "
                        strSQL02 &= "NULL,"
                        '前回来場日
                        strSQL01 &= "ENTDT, "
                        strSQL02 &= "NULL,"
                        '作成日時
                        strSQL01 &= "INSDTM,"
                        strSQL02 &= "NOW(),"
                        '更新日時
                        strSQL01 &= "UPDDTM"
                        strSQL02 &= "NOW()"

                        strSQL01 &= ") "
                        strSQL02 &= ")"

                        If Not (iDatabase.ExecuteUpdate(strSQL01 & strSQL02)) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next

                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM SEATWORK")
                sql.Append(" WHERE SEATDT = '" & Now.ToString("yyyyMMdd") & "'")
                sql.Append(" ORDER BY")
                sql.Append(" SEATNO,LEFTKB")


                Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

                Dim drow() As DataRow
                For i As Integer = 0 To resultDt.Rows.Count - 1
                    drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & CType(resultDt.Rows(i).Item("SEATNO").ToString, Integer) _
                                                           & " AND LEFTKB = " & CType(resultDt.Rows(i).Item("LEFTKB").ToString, Integer))
                    '時間貸し区分
                    drow(0).Item("JKNKB") = CType(resultDt.Rows(i).Item("JKNKB").ToString, Integer)
                    '時間帯
                    drow(0).Item("TIMEKB") = CType(resultDt.Rows(i).Item("TIMEKB").ToString, Integer)
                    '打席状況
                    drow(0).Item("SEATSTATE") = CType(resultDt.Rows(i).Item("SEATSTATE").ToString, Integer)
                    '使用総球数
                    drow(0).Item("ALLBALLCNT") = CType(resultDt.Rows(i).Item("ALLBALLCNT").ToString, Integer)
                    '使用球数
                    drow(0).Item("USEBALLCNT") = CType(resultDt.Rows(i).Item("USEBALLCNT").ToString, Integer)
                    '使用開始時間
                    drow(0).Item("STARTTIME") = CType(resultDt.Rows(i).Item("STARTTIME").ToString, Integer)
                    '使用時間
                    drow(0).Item("USETIME") = CType(resultDt.Rows(i).Item("USETIME").ToString, Integer)
                    '顧客種別
                    drow(0).Item("NKBNO") = CType(resultDt.Rows(i).Item("NKBNO").ToString, Integer)
                    'ボール単価
                    drow(0).Item("BALLKIN") = CType(resultDt.Rows(i).Item("BALLKIN").ToString, Integer)
                    '球数更新フラグ
                    drow(0).Item("UPDFLG") = CType(resultDt.Rows(i).Item("UPDFLG").ToString, Integer)
                    '予約打席
                    drow(0).Item("RSVKBN") = CType(resultDt.Rows(i).Item("RSVKBN").ToString, Integer)
                    '顧客番号
                    drow(0).Item("NCSNO") = resultDt.Rows(i).Item("NCSNO").ToString
                    '氏名
                    drow(0).Item("CCSNAME") = resultDt.Rows(i).Item("CCSNAME").ToString
                    '精算金額
                    drow(0).Item("SEISANKIN") = CType(resultDt.Rows(i).Item("SEISANKIN").ToString, Integer)
                    '使用前残金額
                    drow(0).Item("ZANKN") = CType(resultDt.Rows(i).Item("ZANKN").ToString, Integer)
                    '使用前P)残金額
                    drow(0).Item("PREZANKN") = CType(resultDt.Rows(i).Item("PREZANKN").ToString, Integer)
                    '使用前残ポイント
                    drow(0).Item("SRTPO") = CType(resultDt.Rows(i).Item("SRTPO").ToString, Integer)
                    '顧客番号(DB取得時)
                    drow(0).Item("DBNCSNO") = resultDt.Rows(i).Item("DBNCSNO").ToString
                    '前回来場日
                    drow(0).Item("ENTDT") = resultDt.Rows(i).Item("ENTDT").ToString

                    drow(0).EndEdit()
                Next
            End If

 

            iDatabase.Commit()

            Return True

        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

#End Region


#Region "▼打席通信関連"

    ' 非同期処理本体
    Private Sub OnDoWork(senser As Object, e As DoWorkEventArgs)
        Dim dr As DataRow()
        Dim strSeatNo As String = String.Empty
        Dim strBALLKIN As String = String.Empty
        Dim intLEFTKB As Integer = 0                '左打席区分
        Dim strPASSCD As String = String.Empty      'パスワード
        Dim strBALLKIN01 As String = String.Empty   '種別01ボール単価
        Dim strBALLKIN02 As String = String.Empty   '種別02ボール単価
        Dim strBALLKIN03 As String = String.Empty   '種別03ボール単価
        Dim strBALLKIN04 As String = String.Empty   '種別04ボール単価
        Dim strBALLKIN05 As String = String.Empty   '種別05ボール単価
        Dim strBALLKIN06 As String = String.Empty   '種別06ボール単価
        Dim strBALLKIN07 As String = String.Empty   '種別07ボール単価
        Dim strBALLKIN08 As String = String.Empty   '種別08ボール単価
        Dim strBALLKIN09 As String = String.Empty   '種別09ボール単価
        Dim strBALLKIN10 As String = String.Empty   '種別10ボール単価
        Dim strBALLKIN11 As String = String.Empty   '種別11ボール単価
        Dim strBALLKIN12 As String = String.Empty   '種別12ボール単価
        Dim strYUSENKIN As String = String.Empty    '優先単価
        Dim strMAXBALL As String = String.Empty     '上限打球数
        Dim strTAX As String = String.Empty         '消費税率
        Dim drow() As DataRow
        Dim blnUpdDB As Boolean = False

        Dim ipcUpdateCount As Integer = 0
        Try
            Do
                Try
                    UIUtility.SYSTEM.NOWTIMEKB = _ipc.SYSTEM.NOWTIMEKB
                Catch ex As Exception
                    UIUtility.SYSTEM.NOWTIMEKB = 1
                End Try


                '単価切り替え時間
                dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                If dr.Length > 0 Then UIUtility.SYSTEM.NEXTTIMENM = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)

                '呼び出しフラグ
                _blnYOBIDASHI = False
                For i As Integer = 1 To UIUtility.SYSTEM.SEATSU

                    'ボール単価
                    If i <= UIUtility.SYSTEM.LSTNO1F Then
                        '【1F】
                        strBALLKIN = "BALLKIN1F"
                    ElseIf i <= UIUtility.SYSTEM.LSTNO2F Then
                        '【2F】
                        strBALLKIN = "BALLKIN2F"
                    Else
                        '【3F】
                        strBALLKIN = "BALLKIN3F"
                    End If
                    '種別1
                    strBALLKIN01 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN01 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別2
                    strBALLKIN02 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 2 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN02 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別3
                    strBALLKIN03 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 3 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN03 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別4
                    strBALLKIN04 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 4 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN04 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別5
                    strBALLKIN05 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 5 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN05 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別6
                    strBALLKIN06 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 6 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN06 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別7
                    strBALLKIN07 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 7 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN07 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別8
                    strBALLKIN08 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 8 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN08 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別9
                    strBALLKIN09 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 9 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN09 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別10
                    strBALLKIN10 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 10 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN10 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別11
                    strBALLKIN11 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 11 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN11 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)
                    '種別12
                    strBALLKIN12 = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 12 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strBALLKIN12 = "&H" & dr(0).Item(strBALLKIN).ToString.PadLeft(2, "0"c)

                    '優先単価
                    strYUSENKIN = "&H00"

                    '上限打球数
                    strMAXBALL = "&H" & UIUtility.SYSTEM.MAXJDCNT.ToString.PadLeft(2, "0"c)


                    'パスワード
                    strPASSCD = "&H00"
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then strPASSCD = "&H" & dr(0).Item("PASSCD").ToString.PadLeft(2, "0"c)
                    UIUtility.SYSTEM.NOWPASSCD = strPASSCD.Replace("&H", String.Empty)

                    '消費税率
                    strTAX = "&H" & UIUtility.SYSTEM.TAMATAX.ToString.PadLeft(2, "0"c)

                    '打席指定区分
                    dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
                    If dr.Length > 0 Then UIUtility.SYSTEM.SITEIKBN = CType(dr(0).Item("SITEIKBN").ToString, Integer)

                    strSeatNo = i.ToString.PadLeft(4, "0"c)

                    intLEFTKB = 0
                    blnUpdDB = False
                    GetSeatInfo("&H" & strSeatNo.Substring(0, 2), "&H" & strSeatNo.Substring(2, 2), intLEFTKB, strPASSCD _
                                , strBALLKIN01, strBALLKIN02, strBALLKIN03, strBALLKIN04, strBALLKIN05 _
                                , strBALLKIN06, strBALLKIN07, strBALLKIN08, strBALLKIN09, strBALLKIN10 _
                                , strBALLKIN11, strBALLKIN12, strYUSENKIN, strMAXBALL, strTAX, blnUpdDB)

                    '打席サマリ更新
                    If blnUpdDB Then
                        drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & i & " AND LEFTKB = " & intLEFTKB)
                        Try
                            UpdSEATINFO(drow)
                        Catch ex As Exception
                            MessageBox.Show("UpdSEATINFOエラー")
                        End Try

                        '球数更新フラグ初期値
                        drow(0).Item("UPDFLG") = 0
                        drow(0).Item("DBNCSNO") = String.Empty
                        drow(0).EndEdit()
                    End If

                    If UIUtility.SYSTEM.LRMULTI01.Equals(i) Or UIUtility.SYSTEM.LRMULTI02.Equals(i) Or UIUtility.SYSTEM.LRMULTI03.Equals(i) _
                                                Or UIUtility.SYSTEM.LRMULTI04.Equals(i) Or UIUtility.SYSTEM.LRMULTI05.Equals(i) Then
                        '【左打席】
                        intLEFTKB = 1
                        blnUpdDB = False
                        GetSeatInfo("&H" & strSeatNo.Substring(0, 2), "&H" & strSeatNo.Substring(2, 2), intLEFTKB, strPASSCD _
                                    , strBALLKIN01, strBALLKIN02, strBALLKIN03, strBALLKIN04, strBALLKIN05 _
                                    , strBALLKIN06, strBALLKIN07, strBALLKIN08, strBALLKIN09, strBALLKIN10 _
                                    , strBALLKIN11, strBALLKIN12, strYUSENKIN, strMAXBALL, strTAX, blnUpdDB)

                        '打席サマリ更新
                        If blnUpdDB Then
                            drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & i & " AND LEFTKB = " & intLEFTKB)
                            UpdSEATINFO(drow)
                            '球数更新フラグ初期値
                            drow(0).Item("UPDFLG") = 0
                            drow(0).Item("DBNCSNO") = String.Empty
                            drow(0).EndEdit()
                        End If
                    End If

                    '*** トランザクション開始 ***'
                    iDatabase.BeginTransaction()

                    ''打席情報ワーク更新()
                    ''【右】
                    'If Not UpdSEATWORK(i, 0) Then
                    '    'MessageBox.Show("aaaaa")
                    '    iDatabase.RollBack()
                    'End If
                    ''【左】
                    'If intLEFTKB.Equals(1) Then
                    '    If Not UpdSEATWORK(i, 1) Then
                    '        'MessageBox.Show("bbbb")
                    '        iDatabase.RollBack()
                    '    End If
                    'End If
                    Try
                        '【右】
                        If Not UpdSEATWORK(i, 0) Then
                            'MessageBox.Show("aaaaa")
                            iDatabase.RollBack()
                            iDatabase.BeginTransaction()
                        End If
                        '【左】
                        If intLEFTKB.Equals(1) Then
                            If Not UpdSEATWORK(i, 1) Then
                                'MessageBox.Show("bbbb")
                                iDatabase.RollBack()
                                iDatabase.BeginTransaction()
                            End If
                        End If
                    Catch ex As Exception
   
                    End Try


                    '打席情報サマリC更新
                    If Not UpdSEATSMC() Then
                        iDatabase.RollBack()
                        iDatabase.BeginTransaction()
                    End If

                    '*** コミット ***'
                    iDatabase.Commit()

                    If (i Mod 5).Equals(0) Then

                        '営業情報マスタ取得
                        GetEIGMTA()


                        Try
                            '打席予約状況取得
                            GetSEATRSV()
                        Catch ex As Exception
                            'エラーでも無視
                        End Try
                    End If


                    Try
                        _ipc.SYSTEM.SEATINFO = UIUtility.TABLE.SEATINFO
                    Catch ex As Exception

                    End Try

                    'Thread.Sleep(10)
                Next
                '呼び出し音
                Try
                    _ipc.SYSTEM.YOBIDASHI = _blnYOBIDASHI
                Catch ex As Exception

                End Try

                '***
                If _blnsw Then
                    _sw.Stop()
                    MessageBox.Show(_sw.Elapsed.ToString)
                    Stop
                End If
                '***



            Loop
        Catch ex As Exception
            If _blnSeatCom_Err Then
                'MessageBox.Show("ComPortエラー")
            Else
                'If Not _blnEndFlg Then MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            e.Cancel = True
        End Try
    End Sub

    ' UIスレッド上で動作する完了時コールバック
    Private Sub OnCompleted(senser As Object, e As RunWorkerCompletedEventArgs)
        Try
            '打席通信でｴﾗｰ発生
            If e.Cancelled Then
                _intErrorCnt += 1
                If _intErrorCnt > 1000 Then
                    Me.Size = New Size(420, 95)
                    Me.Location = New Point(1500, 0)
                    Me.BackColor = Color.Red
                    Me.TopMost = True
                    Me.WindowState = FormWindowState.Normal
                    'シートワーククリア
                    Try
                        ResetSeatWork(0)
                    Catch ex As Exception
                        'MessageBox.Show("シートワーククリア失敗")
                    End Try
                ElseIf _intErrorCnt > 10 Then
                    'シートワーククリア
                    Try
                        ResetSeatWork(1)
                    Catch ex As Exception
                        'MessageBox.Show("シートワーククリア失敗")
                    End Try
                    Me.worker.RunWorkerAsync()  ' 非同期処理開始
                Else
                    Me.worker.RunWorkerAsync()  ' 非同期処理開始
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("エラー")
        End Try
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strSEAT1"></param>
    ''' <param name="strSEAT2"></param>
    ''' <param name="intLEFTKB"></param>
    ''' <param name="strPASSCD"></param>
    ''' <param name="strBALLKIN01"></param>
    ''' <param name="strBALLKIN02"></param>
    ''' <param name="strBALLKIN03"></param>
    ''' <param name="strBALLKIN04"></param>
    ''' <param name="strBALLKIN05"></param>
    ''' <param name="strBALLKIN06"></param>
    ''' <param name="strBALLKIN07"></param>
    ''' <param name="strBALLKIN08"></param>
    ''' <param name="strBALLKIN09"></param>
    ''' <param name="strBALLKIN10"></param>
    ''' <param name="strBALLKIN11"></param>
    ''' <param name="strBALLKIN12"></param>
    ''' <param name="strYUSENKIN"></param>
    ''' <param name="strMAXBALL"></param>
    ''' <param name="strTAX"></param>
    ''' <param name="blnUpdDB"></param>
    ''' <remarks></remarks>
    Private Sub GetSeatInfo(ByVal strSEAT1 As String, ByVal strSEAT2 As String, ByVal intLEFTKB As Integer, ByVal strPASSCD As String _
                        , ByVal strBALLKIN01 As String, ByVal strBALLKIN02 As String, ByVal strBALLKIN03 As String, ByVal strBALLKIN04 As String, ByVal strBALLKIN05 As String _
                        , ByVal strBALLKIN06 As String, ByVal strBALLKIN07 As String, ByVal strBALLKIN08 As String, ByVal strBALLKIN09 As String, ByVal strBALLKIN10 As String _
                        , ByVal strBALLKIN11 As String, ByVal strBALLKIN12 As String, ByVal strYUSENKIN As String, ByVal strMAXBALL As String _
                        , ByVal strTAX As String, ByRef blnUpdDB As Boolean)

        Dim bytSend(21) As Byte
        Dim intChkSum As Integer
        Dim strSTRMARK As String
        Dim drow() As DataRow
        'test
        Dim intNo As Integer = 0
        Dim bytSeisan2(4) As Byte
        Dim strNCSNO2 As String
        Dim strDMEMBER As String = String.Empty
        Try

            '打席番号
            Dim intSeatNo As Integer = CType(strSEAT1.Replace("&H", "") & strSEAT2.Replace("&H", ""), Integer)

            Dim strYOYAKU As String = "&H00"
            drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSeatNo)
            If CType(drow(0).Item("RSVKBN").ToString, Integer).Equals(1) Then
                strYOYAKU = "&H01"
            End If

            _spSEATLINK.DiscardInBuffer()

            If intLEFTKB.Equals(0) Then
                '【右打席】
                strSTRMARK = "&H4E"
            Else
                '【左打席】
                strSTRMARK = "&H4F"
            End If

            bytSend(0) = CByte(Val(strSTRMARK))     'スタートマーク
            intChkSum = intChkSum + CByte(Val(strSTRMARK))

            bytSend(1) = CByte(Val(strSEAT1))     '打席番号 上桁
            intChkSum = intChkSum + CByte(Val(strSEAT1))

            bytSend(2) = CByte(Val(strSEAT2))     '打席番号 下桁
            intChkSum = intChkSum + CByte(Val(strSEAT2))

            bytSend(3) = CByte(Val(strBALLKIN01))     '種別１　単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN01))

            bytSend(4) = CByte(Val(strBALLKIN02))     '種別２　単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN02))

            bytSend(5) = CByte(Val(strBALLKIN03))     '種別３　単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN03))

            bytSend(6) = CByte(Val(strBALLKIN04))     '種別４  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN04))

            bytSend(7) = CByte(Val(strBALLKIN05))     '種別５  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN05))

            bytSend(8) = CByte(Val(strBALLKIN06))     '種別６  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN06))

            bytSend(9) = CByte(Val(strBALLKIN07))     '種別７  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN07))

            bytSend(10) = CByte(Val(strBALLKIN08))    '種別８  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN08))

            bytSend(11) = CByte(Val(strBALLKIN09))     '種別９  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN09))

            bytSend(12) = CByte(Val(strBALLKIN10))     '種別10  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN10))

            bytSend(13) = CByte(Val(strBALLKIN11))     '種別11  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN11))

            bytSend(14) = CByte(Val(strBALLKIN12))     '種別12  単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN12))

            bytSend(15) = CByte(Val(strYUSENKIN))      '優先単価
            intChkSum = intChkSum + CByte(Val(strYUSENKIN))

            bytSend(16) = CByte(Val(strMAXBALL))       '上限打球数
            intChkSum = intChkSum + CByte(Val(strMAXBALL))

            bytSend(17) = CByte(Val(strYOYAKU))        '予約
            intChkSum = intChkSum + CByte(Val(strYOYAKU))

            bytSend(18) = CByte(Val(strPASSCD))        'パスワード
            intChkSum = intChkSum + CByte(Val(strPASSCD))

            bytSend(19) = CByte(Val(strTAX))           '消費税率
            intChkSum = intChkSum + CByte(Val(strTAX))

            bytSend(20) = CByte(Val("&H45"))           'エンドマーク('E')
            intChkSum = intChkSum + CByte(Val("&H45"))

            Dim str2shinsu As String = Convert.ToString(intChkSum, 2)    '２進数変換
            Dim str1hosu As String = String.Empty                   '１の補数保持

            '１の補数計算
            For i As Integer = 0 To str2shinsu.Length - 1
                If str2shinsu.Substring(i, 1).Equals("0") Then
                    str1hosu = str1hosu & "1"
                Else
                    str1hosu = str1hosu & "0"
                End If
            Next

            Dim int2bekijo As Integer = 1   '２のべき乗値保持
            Dim int10shinsu As Integer = 0  '１の補数の１０進数保持
            For i As Integer = str1hosu.Length - 1 To 0 Step -1
                If str1hosu.Substring(i, 1).Equals("1") Then
                    int10shinsu += int2bekijo
                End If
                int2bekijo += int2bekijo
            Next

            If (int10shinsu + 1) > 511 Then
                bytSend(21) = CByte((int10shinsu + 1) - 512)               'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            ElseIf (int10shinsu + 1) > 255 Then
                bytSend(21) = CByte((int10shinsu + 1) - 256)               'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            Else
                bytSend(21) = CByte(int10shinsu + 1) 'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            End If

            Try
                _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))
            Catch ex As Exception
                '通信変換機またはComPort異常
                _blnSeatCom_Err = True
                Throw ex
            End Try

            'Thread.Sleep(_intSleep)
            Thread.Sleep(50)
            'Thread.Sleep(60)

            Dim dat As Byte() = New Byte(_spSEATLINK.BytesToRead - 1) {}

            _spSEATLINK.Read(dat, 0, dat.GetLength(0))

            'dat(0)  = スタートマーク 4Fh
            'dat(1)  = 打席番号上位
            'dat(2)  = 打席番号下位
            'dat(3)  = 打球数上位
            'dat(4)  = 打球数下位
            'dat(5)  = 打席状況１
            'dat(6)  = 打席状況２
            'dat(7)  = 顧客番号(8桁目)
            'dat(8)  = 顧客番号(7桁目)
            'dat(9)  = 顧客番号(6桁目)
            'dat(10) = 顧客番号(5桁目)
            'dat(11) = 顧客番号(4桁目)
            'dat(12) = 顧客番号(3桁目)
            'dat(13) = 顧客番号(2桁目)
            'dat(14) = 顧客番号(1桁目)
            'dat(15) = 精算金額(5桁目)
            'dat(16) = 精算金額(4桁目)
            'dat(17) = 精算金額(3桁目)
            'dat(18) = 精算金額(2桁目)
            'dat(19) = 精算金額(1桁目)
            'dat(20) = エンドマーク 45h
            'dat(21) = SUM



            drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSeatNo & " AND LEFTKB = " & intLEFTKB)

            '*******
            intNo = 1
            '*******

            Dim strSeatNo As String = String.Empty
            If dat.Length.Equals(22) Then
                '【打席から応答あり】
                If dat(0).Equals(CByte(Val("&H4F"))) Then
                    strSeatNo = Convert.ToString(dat(1), 16) & Convert.ToString(dat(2), 16)
                    If intSeatNo.Equals(CType(strSeatNo, Integer)) Then
                        '【打席からの応答と送信先の打席があっているかどうか】

                        '***
                        If _blnsw Then _sw.Start()
                        '***

                        '*******
                        intNo = 2
                        '*******

                        '*** 球数計算 なぜか10球毎カウントで16の倍数でカウントされる為 ***'
                        Dim intUseBall As Integer = 0   '使用球数保持
                        Dim intCal1 As Integer = 0      '球数計算用
                        Dim intCal2 As Integer = 0
                        '10の位
                        intCal1 = CType(Math.Floor(CType(dat(4).ToString, Decimal) / 16), Integer)
                        intCal2 = CType(CType(dat(4).ToString, Decimal) - (intCal1 * 16), Integer)
                        intUseBall = (intCal1 * 10) + intCal2
                        '100の位
                        intCal1 = CType(Math.Floor(CType(dat(3).ToString, Decimal) / 16), Integer)
                        intCal2 = CType(CType(dat(3).ToString, Decimal) - (intCal1 * 16), Integer)
                        '累計打球数
                        intUseBall = intUseBall + (intCal1 * 1000) + (intCal2 * 100)
                        '*****************************************************************'

                        '*******
                        intNo = 3
                        '*******

                        '打席エラー【001】正常【010】単価切り替え完了【011】未使用【100】リーダライタ故障
                        Dim strSEAT_ERR As String = Convert.ToString(dat(5), 2).PadLeft(8, "0"c).Substring(0, 3)

                        '*******
                        intNo = 4
                        '*******

                        '打席使用状況【00】空き打席【01】使用中【10】呼び出し中
                        Dim strSEAT_STATE As String = Convert.ToString(dat(5), 2).PadLeft(8, "0"c).Substring(5, 3)

                        '*******
                        intNo = 5
                        '*******

                        '時間貸し【0】1球貸し中【1】時間貸し中
                        Dim strJKNKB As String = Convert.ToString(dat(6), 2).PadLeft(8, "0"c).Substring(0, 1)

                        '*******
                        intNo = 6
                        '*******

                        '左右兼用打席【0】右打席からの返信【1】左打席からの返信
                        Dim strLRKB As String = Convert.ToString(dat(6), 2).PadLeft(8, "0"c).Substring(2, 1)

                        '*******
                        intNo = 7
                        '*******

                        '打席使用種別
                        Dim strNKBNO As String = Convert.ToString(dat(6), 2).PadLeft(8, "0"c).Substring(4, 4)

                        '*******
                        intNo = 8
                        '*******

                        '顧客番号
                        Dim bytNCSNO(7) As Byte
                        For i As Integer = 0 To 7
                            bytNCSNO(i) = dat(i + 7)
                        Next
                        Dim strNCSNO As String = System.Text.Encoding.GetEncoding(932).GetString(bytNCSNO)
                        strNCSNO2 = strNCSNO
                        drow(0).Item("NCSNO") = strNCSNO

                        '*******
                        intNo = 9
                        '*******

                        '精算金額
                        Dim bytSeisan(4) As Byte
                        Dim intSEISANKIN As Integer = 0
                        If strNCSNO.Equals(drow(0).Item("DBNCSNO").ToString) Then
                            For i As Integer = 0 To 4
                                bytSeisan(i) = dat(i + 15)
                                '***
                                bytSeisan2(i) = dat(i + 15)
                                '***
                            Next
                            '*******
                            intNo = 10
                            '*******
                            intSEISANKIN = CType(System.Text.Encoding.GetEncoding(932).GetString(bytSeisan), Integer)
                            '*******
                            intNo = 11
                            '*******
                            drow(0).Item("SEISANKIN") = intSEISANKIN.ToString
                            '*******
                            intNo = 12
                            '*******

                        End If

                        '*******
                        intNo = 13
                        '*******

                        Select Case strSEAT_ERR
                            Case "001", "010", "000"
                                '【正常】【単価切り替え完了】

                                Select Case strSEAT_STATE
                                    Case "000"
                                        '【空き打席】

                                        Try
                                            If drow(0).Item("SEATSTATE").Equals(UIUtility.SEATSTATE.USE_SPACE) Or drow(0).Item("SEATSTATE").Equals(UIUtility.SEATSTATE.CALL_SPACE) Then
                                                For i As Integer = 1 To 6
                                                    _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))

                                                    Thread.Sleep(50)
                                                    'Thread.Sleep(60)
                                                    'Thread.Sleep(_intSleep)

                                                    Dim dat2 As Byte() = New Byte(_spSEATLINK.BytesToRead - 1) {}

                                                    _spSEATLINK.Read(dat2, 0, dat2.GetLength(0))

                                                    For j As Integer = 0 To 7
                                                        bytNCSNO(j) = dat2(j + 7)
                                                    Next
                                                    strNCSNO = System.Text.Encoding.GetEncoding(932).GetString(bytNCSNO)
                                                    If strNCSNO.Equals(drow(0).Item("DBNCSNO").ToString) Then
                                                        '精算金額
                                                        For j As Integer = 0 To 4
                                                            bytSeisan(j) = dat2(j + 15)
                                                        Next
                                                        intSEISANKIN = CType(System.Text.Encoding.GetEncoding(932).GetString(bytSeisan), Integer)
                                                        drow(0).Item("SEISANKIN") = intSEISANKIN.ToString("#,##0")
                                                    End If
                                                Next
                                            End If
                                        Catch ex As Exception
                                        End Try

                                        If drow(0).Item("UPDFLG").Equals(1) Then
                                            '【球数更新フラグが1の場合DB更新】

                                            If String.IsNullOrEmpty(drow(0).Item("DBNCSNO").ToString) Then
                                                '待機中に取得できなかったので顧客情報取得
                                                Try
                                                    GetCsInfo(drow, strDMEMBER)
                                                Catch ex As Exception
                                                End Try
                                            End If

                                            '使用ボール球数再チェック(ｶｰﾄﾞ残金0円時の球数拾えない為)
                                            drow(0).Item("USEBALLCNT") = intUseBall

                                            '打席サマリ更新
                                            blnUpdDB = True

                                        End If

                                        '空き打席の時に累計打球数を保持
                                        drow(0).Item("ALLBALLCNT") = intUseBall

                                        drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.FREE_SPACE

                                    Case "001", "101"
                                        '【使用中】
                                        Dim blnErr As Boolean = False

                                        If String.IsNullOrEmpty(drow(0).Item("DBNCSNO").ToString) Then
                                            '待機中に取得できなかったので顧客情報取得
                                            Try
                                                blnErr = GetCsInfo(drow, strDMEMBER)
                                            Catch ex As Exception
                                            End Try
                                            'If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                                            '    Try
                                            '        For i As Integer = 1 To 2
                                            '            SendTanka(bytSend)
                                            '            Thread.Sleep(50)
                                            '        Next

                                            '    Catch ex As Exception
                                            '    End Try
                                            'End If
                                        End If

                                        If drow(0).Item("SEATSTATE").Equals(UIUtility.SEATSTATE.FREE_SPACE) Or drow(0).Item("SEATSTATE").Equals(UIUtility.SEATSTATE.WAIT_SPACE) Or drow(0).Item("SEATSTATE").Equals(7) Then
                                            '【空き打席から使用中に】または【待機中から使用中に】

                                            '種別・単価セット

                                            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") And drow(0).Item("SEATSTATE").Equals(7) Then
                                                drow(0).Item("NKBNO") = 2
                                                drow(0).Item("BALLKIN") = CType(strBALLKIN02.Replace("&H", String.Empty), Integer)
                                            Else
                                                If strNKBNO.Equals("0001") Or strNKBNO.Equals("0000") Then
                                                    '【種別１】
                                                    drow(0).Item("NKBNO") = 1
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN01.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("0010") Then
                                                    '【種別２】
                                                    drow(0).Item("NKBNO") = 2
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN02.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("0011") Then
                                                    '【種別３】
                                                    drow(0).Item("NKBNO") = 3
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN03.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("0100") Then
                                                    '【種別４】
                                                    drow(0).Item("NKBNO") = 4
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN04.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("0101") Then
                                                    '【種別５】
                                                    drow(0).Item("NKBNO") = 5
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN05.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("0110") Then
                                                    '【種別６】
                                                    drow(0).Item("NKBNO") = 6
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN06.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("0111") Then
                                                    '【種別７】
                                                    drow(0).Item("NKBNO") = 7
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN07.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("1000") Then
                                                    '【種別８】
                                                    drow(0).Item("NKBNO") = 8
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN08.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("1001") Then
                                                    '【種別９】
                                                    drow(0).Item("NKBNO") = 9
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN09.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("1010") Then
                                                    '【種別１０】
                                                    drow(0).Item("NKBNO") = 10
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN10.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("1011") Then
                                                    '【種別１１】
                                                    drow(0).Item("NKBNO") = 11
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN11.Replace("&H", String.Empty), Integer)
                                                ElseIf strNKBNO.Equals("1100") Then
                                                    '【種別１２】
                                                    drow(0).Item("NKBNO") = 12
                                                    drow(0).Item("BALLKIN") = CType(strBALLKIN12.Replace("&H", String.Empty), Integer)
                                                End If
                                            End If
                                            ''種別・単価セット

                                            'If strNKBNO.Equals("0001") Or strNKBNO.Equals("0000") Then
                                            '    '【種別１】
                                            '    drow(0).Item("NKBNO") = 1
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN01.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("0010") Then
                                            '    '【種別２】
                                            '    drow(0).Item("NKBNO") = 2
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN02.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("0011") Then
                                            '    '【種別３】
                                            '    drow(0).Item("NKBNO") = 3
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN03.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("0100") Then
                                            '    '【種別４】
                                            '    drow(0).Item("NKBNO") = 4
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN04.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("0101") Then
                                            '    '【種別５】
                                            '    drow(0).Item("NKBNO") = 5
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN05.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("0110") Then
                                            '    '【種別６】
                                            '    drow(0).Item("NKBNO") = 6
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN06.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("0111") Then
                                            '    '【種別７】
                                            '    drow(0).Item("NKBNO") = 7
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN07.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("1000") Then
                                            '    '【種別８】
                                            '    drow(0).Item("NKBNO") = 8
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN08.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("1001") Then
                                            '    '【種別９】
                                            '    drow(0).Item("NKBNO") = 9
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN09.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("1010") Then
                                            '    '【種別１０】
                                            '    drow(0).Item("NKBNO") = 10
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN10.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("1011") Then
                                            '    '【種別１１】
                                            '    drow(0).Item("NKBNO") = 11
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN11.Replace("&H", String.Empty), Integer)
                                            'ElseIf strNKBNO.Equals("1100") Then
                                            '    '【種別１２】
                                            '    drow(0).Item("NKBNO") = 12
                                            '    drow(0).Item("BALLKIN") = CType(strBALLKIN12.Replace("&H", String.Empty), Integer)
                                            'End If


                                            '使用開始時間
                                            drow(0).Item("STARTTIME") = (CType(CType(DateTime.Now.ToString("HHmm"), Integer).ToString.PadLeft(4, "0"c).Substring(0, 2), Integer) * 60) _
                                                                        + (CType(DateTime.Now.ToString("HHmm").PadLeft(4, "0"c).ToString.Substring(2, 2), Integer))

                                            '利用開始時の時間帯
                                            drow(0).Item("TIMEKB") = UIUtility.SYSTEM.NOWTIMEKB


                                            If strJKNKB.Equals("0") Then
                                                '【1球貸し中】
                                                drow(0).Item("JKNKB") = 0
                                            Else
                                                '【時間貸し中】
                                                drow(0).Item("JKNKB") = 1
                                            End If

                                        ElseIf drow(0).Item("SEATSTATE").Equals(UIUtility.SEATSTATE.INIT_DATA) Then
                                            '【システム起動時に使用中の場合】
                                            '使用開始時間
                                            drow(0).Item("STARTTIME") = (CType(CType(DateTime.Now.ToString("HHmm"), Integer).ToString.PadLeft(4, "0"c).Substring(0, 2), Integer) * 60) _
                                                                        + (CType(DateTime.Now.ToString("HHmm").PadLeft(4, "0"c).ToString.Substring(2, 2), Integer))
                                            '総使用球数
                                            drow(0).Item("ALLBALLCNT") = intUseBall
                                        End If
                                        '使用時間計算
                                        drow(0).Item("USETIME") = (CType(CType(DateTime.Now.ToString("HHmm"), Integer).ToString.PadLeft(4, "0"c).Substring(0, 2), Integer) * 60) _
                                                                + (CType(DateTime.Now.ToString("HHmm").PadLeft(4, "0"c).ToString.Substring(2, 2), Integer)) - CType(drow(0).Item("STARTTIME"), Integer)
                                        '球数更新フラグ
                                        drow(0).Item("UPDFLG") = 1
                                        drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.USE_SPACE

                                        If blnErr Then
                                            'カード停止中 OR カード有効期限切れ
                                            CardEject(bytSend)
                                        End If
                                    Case "010"
                                        '【呼び出し中】
                                        _blnYOBIDASHI = True
                                        drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.CALL_SPACE
                                    Case "011"
                                        '【待機中】
                                        Dim blnErr As Boolean = False
                                        Try
                                            blnErr = GetCsInfo(drow, strDMEMBER)
                                        Catch ex As Exception
                                        End Try
                                        If blnErr Then
                                            'カード停止中 OR カード有効期限切れ
                                            CardEject(bytSend)
                                        Else
                                            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then

                                                If String.IsNullOrEmpty(strDMEMBER) Or strDMEMBER >= Now.ToString("yyyyMMdd") Then
                                                    Try
                                                        For i As Integer = 1 To 2
                                                            _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))
                                                            'Thread.Sleep(_intSleep)
                                                            Thread.Sleep(50)
                                                        Next

                                                    Catch ex As Exception
                                                    End Try
                                                    drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.WAIT_SPACE
                                                Else
                                                    '期限切れ
                                                    Try
                                                        For i As Integer = 1 To 5
                                                            SendTanka(bytSend, strBALLKIN02)
                                                            Thread.Sleep(70)
                                                            'Thread.Sleep(50)
                                                        Next

                                                    Catch ex As Exception
                                                    End Try
                                                    drow(0).Item("SEATSTATE") = 7
                                                End If
                                            Else
                                                Try
                                                    For i As Integer = 1 To 2
                                                        _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))
                                                        'Thread.Sleep(_intSleep)
                                                        Thread.Sleep(50)
                                                    Next

                                                Catch ex As Exception
                                                End Try
                                                drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.WAIT_SPACE
                                            End If



                                        End If
                                        ''【待機中】
                                        'Dim blnErr As Boolean = False
                                        'Try
                                        '    blnErr = GetCsInfo(drow, strDMEMBER)
                                        'Catch ex As Exception
                                        'End Try
                                        'If blnErr Then
                                        '    'カード停止中 OR カード有効期限切れ
                                        '    CardEject(bytSend)
                                        'Else
                                        '    Try
                                        '        For i As Integer = 1 To 2
                                        '            _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))
                                        '            'Thread.Sleep(_intSleep)
                                        '            Thread.Sleep(50)
                                        '        Next

                                        '    Catch ex As Exception
                                        '    End Try
                                        '    drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.WAIT_SPACE
                                        'End If
                                End Select



                                If Not strSEAT_STATE.Equals("00") Then

                                    '【空き打席以外の場合】
                                    drow(0).Item("USEBALLCNT") = intUseBall
                                End If
                            Case "110", "100"
                                '【リーダライタ故障または何かしわトラブル】
                                'drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.ERR_RW
                                _blnYOBIDASHI = True
                                drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.CALL_SPACE
                            Case Else
                                '【通信不能】
                                drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.ERR_SPACE
                        End Select


                        'レスポンスカウント初期化
                        drow(0).Item("RSPCNT") = 0
                    End If
                End If

            Else
                '【応答なし】

                If CType(drow(0).Item("RSPCNT"), Integer) >= 4 Then
                    '通信不能
                    drow(0).Item("SEATSTATE") = UIUtility.SEATSTATE.ERR_SPACE
                Else
                    drow(0).Item("RSPCNT") = CType(drow(0).Item("RSPCNT").ToString, Integer) + 1
                End If
            End If
            drow(0).EndEdit()

        Catch ex As Exception
            Dim textFile As System.IO.StreamWriter
            textFile = New System.IO.StreamWriter("D:\GRAN31\Log\" & Now.ToString("yyyyMMdd") & "log.txt", True, System.Text.Encoding.Default)
            textFile.WriteLine(Now.ToString("HH:mm") & " GetSeatInfoで発生 " & intNo & " 打席№" & strSEAT1 & strSEAT2 & " 顧客№" & strNCSNO2)
            textFile.WriteLine(bytSeisan2(0).ToString & bytSeisan2(1).ToString & bytSeisan2(2).ToString & bytSeisan2(3).ToString & bytSeisan2(4).ToString)
            textFile.WriteLine(ex.Message.ToString)
            textFile.Close()
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 会員期限切れの単価送信
    ''' </summary>
    ''' <param name="pbytSend"></param>
    ''' <remarks></remarks>
    Private Sub SendTanka(ByVal pbytSend() As Byte, ByVal strBALLKIN02 As String, Optional strPW As String = "")
        Try

            Dim bytSend(21) As Byte
            Dim intChkSum As Integer = 0

            _spSEATLINK.DiscardInBuffer()

            bytSend(0) = pbytSend(0)                    'スタートマーク
            intChkSum = intChkSum + pbytSend(0)

            bytSend(1) = pbytSend(1)                    '打席番号 上桁
            intChkSum = intChkSum + pbytSend(1)

            bytSend(2) = pbytSend(2)                    '打席番号 下桁
            intChkSum = intChkSum + pbytSend(2)

            bytSend(3) = pbytSend(3)                    '種別１　単価
            intChkSum = intChkSum + pbytSend(3)

            bytSend(4) = pbytSend(4)                    '種別２　単価
            intChkSum = intChkSum + pbytSend(4)

            bytSend(5) = pbytSend(5)                    '種別３　単価
            intChkSum = intChkSum + pbytSend(5)

            bytSend(6) = pbytSend(6)                    '種別４  単価
            intChkSum = intChkSum + pbytSend(6)

            bytSend(7) = pbytSend(7)                    '種別５  単価
            intChkSum = intChkSum + pbytSend(7)

            bytSend(8) = pbytSend(8)                    '種別６  単価
            intChkSum = intChkSum + pbytSend(8)

            bytSend(9) = pbytSend(9)                    '種別７  単価
            intChkSum = intChkSum + pbytSend(9)

            bytSend(10) = pbytSend(10)                  '種別８  単価
            intChkSum = intChkSum + pbytSend(10)

            bytSend(11) = pbytSend(11)                  '種別９  単価
            intChkSum = intChkSum + pbytSend(11)

            bytSend(12) = pbytSend(12)                  '種別10  単価
            intChkSum = intChkSum + pbytSend(12)

            bytSend(13) = pbytSend(13)                  '種別11  単価
            intChkSum = intChkSum + pbytSend(13)

            bytSend(14) = pbytSend(14)                  '種別12  単価
            intChkSum = intChkSum + pbytSend(14)

            bytSend(15) = CByte(Val(strBALLKIN02))     '優先単価
            intChkSum = intChkSum + CByte(Val(strBALLKIN02))

            bytSend(16) = pbytSend(16)                  '上限打球数
            intChkSum = intChkSum + pbytSend(16)

            bytSend(17) = pbytSend(17)                  '予約
            intChkSum = intChkSum + pbytSend(17)

            If strPW.Equals("") Then
                bytSend(18) = pbytSend(18)            'パスワード
                intChkSum = intChkSum + pbytSend(18)
            Else
                bytSend(18) = CByte(Val(strPW))            'パスワード
                intChkSum = intChkSum + CByte(Val(strPW))
            End If


            bytSend(19) = pbytSend(19)                  '消費税率
            intChkSum = intChkSum + pbytSend(19)

            bytSend(20) = CByte(Val("&H45"))           'エンドマーク('E')
            intChkSum = intChkSum + CByte(Val("&H45"))

            Dim str2shinsu As String = Convert.ToString(intChkSum, 2)    '２進数変換
            Dim str1hosu As String = String.Empty                   '１の補数保持

            '１の補数計算
            For i As Integer = 0 To str2shinsu.Length - 1
                If str2shinsu.Substring(i, 1).Equals("0") Then
                    str1hosu = str1hosu & "1"
                Else
                    str1hosu = str1hosu & "0"
                End If
            Next

            Dim int2bekijo As Integer = 1   '２のべき乗値保持
            Dim int10shinsu As Integer = 0  '１の補数の１０進数保持
            For i As Integer = str1hosu.Length - 1 To 0 Step -1
                If str1hosu.Substring(i, 1).Equals("1") Then
                    int10shinsu += int2bekijo
                End If
                int2bekijo += int2bekijo
            Next

            If (int10shinsu + 1) > 511 Then
                bytSend(21) = CByte((int10shinsu + 1) - 512)               'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            ElseIf (int10shinsu + 1) > 255 Then
                bytSend(21) = CByte((int10shinsu + 1) - 256)               'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            Else
                bytSend(21) = CByte(int10shinsu + 1) 'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            End If

            Try
                _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))
            Catch ex As Exception
                '通信変換機またはComPort異常
                _blnSeatCom_Err = True
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード停止中の場合強制排出
    ''' </summary>
    ''' <param name="pbytSend"></param>
    ''' <remarks></remarks>
    Private Sub CardEject(ByVal pbytSend() As Byte)
        Try
            Dim bytSend(21) As Byte
            Dim intChkSum As Integer = 0

            _spSEATLINK.DiscardInBuffer()

            bytSend(0) = pbytSend(0)                    'スタートマーク
            intChkSum = intChkSum + pbytSend(0)

            bytSend(1) = pbytSend(1)                    '打席番号 上桁
            intChkSum = intChkSum + pbytSend(1)

            bytSend(2) = pbytSend(2)                    '打席番号 下桁
            intChkSum = intChkSum + pbytSend(2)

            bytSend(3) = pbytSend(3)                    '種別１　単価
            intChkSum = intChkSum + pbytSend(3)

            bytSend(4) = pbytSend(4)                    '種別２　単価
            intChkSum = intChkSum + pbytSend(4)

            bytSend(5) = pbytSend(5)                    '種別３　単価
            intChkSum = intChkSum + pbytSend(5)

            bytSend(6) = pbytSend(6)                    '種別４  単価
            intChkSum = intChkSum + pbytSend(6)

            bytSend(7) = pbytSend(7)                    '種別５  単価
            intChkSum = intChkSum + pbytSend(7)

            bytSend(8) = pbytSend(8)                    '種別６  単価
            intChkSum = intChkSum + pbytSend(8)

            bytSend(9) = pbytSend(9)                    '種別７  単価
            intChkSum = intChkSum + pbytSend(9)

            bytSend(10) = pbytSend(10)                  '種別８  単価
            intChkSum = intChkSum + pbytSend(10)

            bytSend(11) = pbytSend(11)                  '種別９  単価
            intChkSum = intChkSum + pbytSend(11)

            bytSend(12) = pbytSend(12)                  '種別10  単価
            intChkSum = intChkSum + pbytSend(12)

            bytSend(13) = pbytSend(13)                  '種別11  単価
            intChkSum = intChkSum + pbytSend(13)

            bytSend(14) = pbytSend(14)                  '種別12  単価
            intChkSum = intChkSum + pbytSend(14)

            bytSend(15) = pbytSend(15)                  '優先単価
            intChkSum = intChkSum + pbytSend(15)

            bytSend(16) = pbytSend(16)                  '上限打球数
            intChkSum = intChkSum + pbytSend(16)

            bytSend(17) = pbytSend(17)                  '予約
            intChkSum = intChkSum + pbytSend(17)

            bytSend(18) = CByte(Val("&H99"))            'パスワード
            intChkSum = intChkSum + CByte(Val("&H99"))

            bytSend(19) = pbytSend(19)                  '消費税率
            intChkSum = intChkSum + pbytSend(19)

            bytSend(20) = CByte(Val("&H45"))           'エンドマーク('E')
            intChkSum = intChkSum + CByte(Val("&H45"))

            Dim str2shinsu As String = Convert.ToString(intChkSum, 2)    '２進数変換
            Dim str1hosu As String = String.Empty                   '１の補数保持

            '１の補数計算
            For i As Integer = 0 To str2shinsu.Length - 1
                If str2shinsu.Substring(i, 1).Equals("0") Then
                    str1hosu = str1hosu & "1"
                Else
                    str1hosu = str1hosu & "0"
                End If
            Next

            Dim int2bekijo As Integer = 1   '２のべき乗値保持
            Dim int10shinsu As Integer = 0  '１の補数の１０進数保持
            For i As Integer = str1hosu.Length - 1 To 0 Step -1
                If str1hosu.Substring(i, 1).Equals("1") Then
                    int10shinsu += int2bekijo
                End If
                int2bekijo += int2bekijo
            Next

            If (int10shinsu + 1) > 511 Then
                bytSend(21) = CByte((int10shinsu + 1) - 512)               'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            ElseIf (int10shinsu + 1) > 255 Then
                bytSend(21) = CByte((int10shinsu + 1) - 256)               'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            Else
                bytSend(21) = CByte(int10shinsu + 1) 'バイナリ １バイト (１をプラスして２の補数の１０進数値をセット)
            End If

            Try
                _spSEATLINK.Write(bytSend, 0, bytSend.GetLength(0))
            Catch ex As Exception
                '通信変換機またはComPort異常
                _blnSeatCom_Err = True
                Throw ex
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region



End Class
