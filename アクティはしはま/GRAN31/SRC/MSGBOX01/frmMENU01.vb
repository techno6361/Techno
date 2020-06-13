Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase
Imports TMT90
Imports System.Security.Permissions

Public Class frmMENU01

#Region "▼宣言部"

    Private iDatabase As IDatabase.IMethod

    ' HACK ADD 2018/03/06 TERAYAMA
    ' *** STA
    Dim _ipc As IpcServer
    Dim _process As Process
    ' *** END

    Private _prcSEATSERVER As Process

    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New Techno.DeviceControls.ICR700Control
    ''' <summary>
    ''' 打席通信用シリアルポート
    ''' </summary>
    ''' <remarks></remarks>
    Private _spSEATLINK As New SerialPort
    ''' <summary>
    ''' 時間帯区分
    ''' </summary>
    ''' <remarks></remarks>
    Private _intTIMEKB As Integer
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
    ''' 打席情報画面用フォーム
    ''' </summary>
    ''' <remarks></remarks>
    Private _frmMULTIDISP As New Form


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
#End Region

#Region "▼閉じるボタン無効化関連"
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Shared Function GetSystemMenu(ByVal hWnd As IntPtr, _
ByVal bRevert As Boolean) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Shared Function GetMenuItemCount(ByVal hMenu As IntPtr) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Shared Function DrawMenuBar(ByVal hWnd As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Shared Function RemoveMenu(ByVal hMenu As IntPtr, _
        ByVal uPosition As Integer, _
        ByVal uFlags As Integer) As Boolean
    End Function

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)

        Const MF_BYPOSITION As Int32 = &H400
        Const MF_REMOVE As Int32 = &H1000

        Dim menu As IntPtr = GetSystemMenu(Me.Handle, False)
        Dim menuCount As Integer = GetMenuItemCount(menu)
        If menuCount > 1 Then
            'メニューの「閉じる」とセパレータを削除
            RemoveMenu(menu, menuCount - 1, MF_BYPOSITION Or MF_REMOVE)
            RemoveMenu(menu, menuCount - 2, MF_BYPOSITION Or MF_REMOVE)
            DrawMenuBar(Me.Handle)
        End If
    End Sub
#End Region


#Region "▼フォーム移動不可"
    'Imports System.Security.Permissions

    <SecurityPermission(SecurityAction.Demand, _
        Flags:=SecurityPermissionFlag.UnmanagedCode)> _
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_SYSCOMMAND As Integer = &H112
        Const SC_MOVE As Long = &HF010L

        If m.Msg = WM_SYSCOMMAND AndAlso _
            (m.WParam.ToInt64() And &HFFF0L) = SC_MOVE Then
            m.Result = IntPtr.Zero
            Return
        End If

        MyBase.WndProc(m)
    End Sub
#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMENU01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '時刻調整
            Process.Start(Configuration.ConfigurationManager.AppSettings("JIKOKU_PATH"))

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
    Private Sub frmMENU01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.Refresh()

            Me.Cursor = Cursors.WaitCursor

            ' マウスの移動領域を制限
            Cursor.Clip = Me.Bounds

            'DB登録用本日営業日セット
            UIUtility.SYSTEM.UPDDAY = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            'システムモード【0】オートセッター【1】ボール貸出機
            UIUtility.SYSTEM.SYSTEMMODE = CType(Configuration.ConfigurationManager.AppSettings("SYSTEMMODE"), Integer)
            'システム区分【0】打席管理【0以外】打席と通信しない端末
            UIUtility.SYSTEM.SYSTEMKBN = CType(Configuration.ConfigurationManager.AppSettings("SYSTEMKBN"), Integer)
            '店番
            UIUtility.SYSTEM.SHOPNO = Configuration.ConfigurationManager.AppSettings("SHOPNO")
            'ICR700リーダライターCOMポート名
            UIUtility.SYSTEM.ICR700COM = Configuration.ConfigurationManager.AppSettings("ICR700RW_COM")
            'KU-A201リーダライターCOMポート名
            UIUtility.SYSTEM.IA240COM = Configuration.ConfigurationManager.AppSettings("IA240RW_COM")
            ' ﾘｰﾀﾞｰﾗｲﾀｰ区分【0】ICR700のみ【1】MCH3000 + ICR700
            UIUtility.SYSTEM.RWUnitKB = 0
            'カードリードインターバル
            UIUtility.SYSTEM.ReadInterval = CType(Configuration.ConfigurationManager.AppSettings("ReadInterval"), Integer)
            '呼び出し音ファイルパス
            UIUtility.FILE_PATH.YOBIDASHI = Configuration.ConfigurationManager.AppSettings("YOBIDASHI_PATH")
            '帳票用エクセルテンプレートファイルパス
            UIUtility.FILE_PATH.TEMPLATE = Configuration.ConfigurationManager.AppSettings("TEMPLATE_PATH")
            '帳票用エクセル保存先ファイルパス
            UIUtility.FILE_PATH.REPORT = Configuration.ConfigurationManager.AppSettings("REPORT_PATH")
            'スクールメニューファイルパス
            UIUtility.FILE_PATH.SCLMENU = Configuration.ConfigurationManager.AppSettings("SCLMENU_PATH")
            '予約受講画面ファイルパス
            UIUtility.FILE_PATH.SCL010 = Configuration.ConfigurationManager.AppSettings("SCL010_PATH")
            '画像ファイルパス
            UIUtility.FILE_PATH.IMAGE = Configuration.ConfigurationManager.AppSettings("IMAGE_PATH")
            'メニュー区分
            UIUtility.SYSTEM.MENUKBN = Configuration.ConfigurationManager.AppSettings("MENU_KBN")
            'ﾊﾟｽﾜｰﾄﾞ区分
            UIUtility.SYSTEM.PWKBN = Configuration.ConfigurationManager.AppSettings("PW_KBN")

            '帳票用エクセルファイル削除後フォルダ再作成
            If IO.Directory.Exists(UIUtility.FILE_PATH.REPORT) Then
                Try
                    IO.Directory.Delete(UIUtility.FILE_PATH.REPORT, True)
                Catch ex As Exception
                    Using frm As New frmMSGBOX01("帳票のエクセルファイルを終了して下さい。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.Close()
                    Exit Sub
                End Try
                Thread.Sleep(200)
            End If
            IO.Directory.CreateDirectory(UIUtility.FILE_PATH.REPORT)


            'データベース接続情報取得
            Dim DbInfo As String = Configuration.ConfigurationManager.AppSettings("DbInfo_PATH")

            '*** ＤＢ接続 ***'
            Dim dbControl As DatabaseControl = New DatabaseControl

            If dbControl.CreateDatabaseInstance(DatabaseControl.DatabaseKind.PostgreSQL) Then
                iDatabase = dbControl.IDB
                iDatabase.Connect(DbInfo)
                Try
                    iDatabase.Open()
                Catch ex As Exception
                    Using frm As New frmMSGBOX01("データベースと接続できませんでした。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.Close()
                    Exit Sub
                End Try
            End If

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

            '営業情報マスタ取得
            If Not GetEIGMT() Then
                Using frm As New frmMSGBOX01("営業情報マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '顧客種別情報マスタ取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            ' テロップ・コメントの取得
            UIFunction.GetDTELOP(iDatabase)

            'トランザクション開始
            iDatabase.BeginTransaction()

            'カレンダー情報作成
            If Not InsCALMTA() Then
                Using frm As New frmMSGBOX01("カレンダー情報の作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '本日料金体系情報取得
            If Not GetRknInfo() Then
                Using frm As New frmMSGBOX01("料金体系情報の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If UIUtility.SYSTEM.PASSKBN.Equals(0) Then
                'パスワード区分ランダムの場合
                If Not UpdPASSCD() Then
                    Using frm As New frmMSGBOX01("パスワードの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

            '伝票番号クリア処理
            If Not ClrDENNO() Then
                Using frm As New frmMSGBOX01("伝票番号の0クリア処理に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '月間来場回数クリア処理
            If Not UIUtility.SYSTEM.CLRENTMCNT.Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c)) Then
                If Not ClrENTCNT2() Then
                    Using frm As New frmMSGBOX01("月間来場回数の0クリア処理に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

            If UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                '時間貸しトラン削除処理
                If Not ClrTIMTRA() Then
                    Using frm As New frmMSGBOX01("時間貸しトラン削除処理に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

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
            '打席情報ワーク作成 ※作成済みの場合データ取得※
            If Not InsSEATWORK() Then
                Using frm As New frmMSGBOX01("打席情報ワークの作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '打席情報サマリA作成
            If Not InsSEATSMA() Then
                Using frm As New frmMSGBOX01("打席情報サマリAの作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '打席情報サマリB作成
            If Not InsSEATSMB() Then
                Using frm As New frmMSGBOX01("打席情報サマリBの作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '打席情報サマリC作成
            If Not InsSEATSMC() Then
                Using frm As New frmMSGBOX01("打席情報サマリCの作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'コミット
            iDatabase.Commit()

            '*** リーダライター接続 ***'
            Me.btnReRW.PerformClick()

            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                ' // オートセッター //

                '打席情報画面あり・なし
                If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                    _ipc = New IpcServer
                    _ipc.SYSTEM.TELOP = UIUtility.SYSTEM.TELOP
                    _ipc.SYSTEM.LRMULTI01 = UIUtility.SYSTEM.LRMULTI01
                    _ipc.SYSTEM.LRMULTI02 = UIUtility.SYSTEM.LRMULTI02
                    _ipc.SYSTEM.LRMULTI03 = UIUtility.SYSTEM.LRMULTI03
                    _ipc.SYSTEM.LRMULTI04 = UIUtility.SYSTEM.LRMULTI04
                    _ipc.SYSTEM.LRMULTI05 = UIUtility.SYSTEM.LRMULTI05
                    _ipc.SYSTEM.LSTNO1F = UIUtility.SYSTEM.LSTNO1F
                    _ipc.SYSTEM.LSTNO2F = UIUtility.SYSTEM.LSTNO2F
                    _ipc.SYSTEM.LSTNO3F = UIUtility.SYSTEM.LSTNO3F
                    _ipc.SYSTEM.FLRSU = UIUtility.SYSTEM.FLRSU
                    _ipc.SYSTEM.SEATINFO = UIUtility.TABLE.SEATINFO
                    _ipc.SYSTEM.SHOPNO = UIUtility.SYSTEM.SHOPNO
                    _ipc.SYSTEM.DbInfo_PATH = Configuration.ConfigurationManager.AppSettings("DbInfo_PATH")
                    _ipc.SYSTEM.SEAT_COM = Configuration.ConfigurationManager.AppSettings("SEAT_COM")
                    _ipc.SYSTEM.SLEEP_TIME = CType(Configuration.ConfigurationManager.AppSettings("SLEEP_TIME"), Integer)
                    _ipc.SYSTEM.MULTIDISP_TYPE = CInt(Configuration.ConfigurationManager.AppSettings("MULTIDISP_TYPE"))

                    If Screen.AllScreens.Length > 1 Then
                        If UIUtility.SYSTEM.DISPMULTI.Equals(1) Then
                            Dim scr As Screen
                            For Each i In Screen.AllScreens
                                If Not i.Primary Then
                                    scr = i
                                End If
                            Next
                            ''フォームの開始位置をディスプレイの左上座標に設定する
                            '_frmMULTIDISP = New frmMULTIDISP01
                            '_frmMULTIDISP.StartPosition = FormStartPosition.Manual
                            '_frmMULTIDISP.Location = scr.Bounds.Location
                            '_frmMULTIDISP.Show()

                            ' HACK ADD 2018/03/06 TERAYAMA
                            ' *** STA
                            _ipc.SYSTEM.LOCATION_X = scr.Bounds.Location.X
                            _process = System.Diagnostics.Process.Start(".\MULTIDISP01.exe")
                            _process.WaitForInputIdle()
                            ' *** END

                        End If
                    End If

                    _prcSEATSERVER = System.Diagnostics.Process.Start(".\SEATSERVER01.exe")
                    _prcSEATSERVER.WaitForInputIdle()
                End If

                'タイマー開始
                Me.Timer1.Start()
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                ' // ボール貸出機 //

                Me.Timer2.Start()
            End If

            'メニュー区分
            Me.btnBusiness.Enabled = False
            Me.pnlBusiness.Enabled = False
            If UIUtility.SYSTEM.MENUKBN.Substring(0, 1).Equals("1") Then
                Me.btnBusiness.Enabled = True
                Me.pnlBusiness.Enabled = True
            End If
            'Me.btnMaster.Enabled = False
            'Me.pnlMaster.Enabled = False
            'If UIUtility.SYSTEM.MENUKBN.Substring(1, 1).Equals("1") Then
            '    Me.btnMaster.Enabled = True
            '    Me.pnlMaster.Enabled = True
            'End If
            Me.btnPrint.Enabled = False
            Me.pnlPrint.Enabled = False
            If UIUtility.SYSTEM.MENUKBN.Substring(2, 1).Equals("1") Then
                Me.btnPrint.Enabled = True
                Me.pnlPrint.Enabled = True
            End If
            Me.btnSchool.Enabled = False
            Me.pnlSchool.Enabled = False
            If UIUtility.SYSTEM.MENUKBN.Substring(3, 1).Equals("1") Then
                Me.btnSchool.Enabled = True
                Me.pnlSchool.Enabled = True
            End If

            'ﾊﾟｽﾜｰﾄﾞ要求確認
            If UIUtility.SYSTEM.PWKBN.Substring(0, 1).Equals("1") Then
                Using frm As New frmPWDISP01
                    frm.PWKBN = 1
                    frm.ShowDialog()
                End Using
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMENU01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            '営業情報マスタ
            If Not UIUtility.TABLE.EIGMT Is Nothing Then UIUtility.TABLE.EIGMT.Dispose()
            '打席情報ﾃｰﾌﾞﾙ
            If Not UIUtility.TABLE.SEATINFO Is Nothing Then UIUtility.TABLE.SEATINFO.Dispose()
            '顧客種別情報
            If Not UIUtility.TABLE.KBMAST Is Nothing Then UIUtility.TABLE.KBMAST.Dispose()

            'ICR700リーダライターポートクローズ
            _dcICR700.Close()

            '打席通信ポート解放
            _spSEATLINK.Dispose()
            '打席情報画面解放
            _frmMULTIDISP.Dispose()

            'データベース切断
            If Not iDatabase Is Nothing Then iDatabase.Dispose()


            '【打席管理の端末のみ】
            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                Try
                    _prcSEATSERVER.Kill()
                    If Screen.AllScreens.Length > 1 Then
                        If UIUtility.SYSTEM.DISPMULTI.Equals(1) Then
                            _process.Kill()
                        End If
                    End If
                Catch ex As Exception
                    'エラー無視
                End Try
                Process.Start(Configuration.ConfigurationManager.AppSettings("BACKUP_PATH"))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim inte As Integer = 0
        Dim intTIMEKB As Integer = 0
        Dim dr As DataRow()
        Try
            '本日の料金体系
            Me.lblRKNNM.ForeColor = Color.FromArgb(UIUtility.COLOR_INFO.RKN_R, UIUtility.COLOR_INFO.RKN_G, UIUtility.COLOR_INFO.RKN_B)
            Me.lblRKNNM.Text = "【" & UIUtility.SYSTEM.RKNNM & "】"

            '本日日付時間(yyyy/MM/dd (曜日))
            UIUtility.SYSTEM.DAYTIME = Now.Year.ToString & "年" _
                                    & Now.Month.ToString.PadLeft(2, "0"c) & "月" _
                                    & Now.Day.ToString.PadLeft(2, "0"c) & "日" & "(" & WeekdayName(Weekday(CType(Now.ToString("yyyy/MM/dd"), Date))).Substring(0, 1) & ")" _
                                    & Now.Hour.ToString.PadLeft(2, "0"c) & "時" _
                                    & Now.Minute.ToString.PadLeft(2, "0"c) & "分"
            Me.lblDAYTIME.Text = UIUtility.SYSTEM.DAYTIME

            inte = 1

            '営業情報取得
            GetEIGMT()

            '総打球数取得
            GetSUMBALL()
            '総来場者数
            GetSUMENTCNT()

            inte = 2

            '*** 現在の時間帯確認 ***************************************************************
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB)

            intTIMEKB = 0
            For i As Integer = 0 To dr.Length - 1
                If dr(i).Item("TIMENM").ToString >= Now.ToString("HHmm") Then
                    intTIMEKB = CType(dr(i).Item("TIMEKB").ToString, Integer)
                    Exit For
                End If
            Next
            If intTIMEKB.Equals(0) Then
                intTIMEKB = 1
            End If
            If Not _intTIMEKB.Equals(intTIMEKB) Then
                _intTIMEKB = intTIMEKB
            End If
            UIUtility.SYSTEM.NOWTIMEKB = _intTIMEKB
            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                _ipc.SYSTEM.NOWTIMEKB = _intTIMEKB
                _ipc.SYSTEM.NKBNO11_CNT = UIUtility.ENTRY.NKBNO11_CNT
                _ipc.SYSTEM.NKBNO12_CNT = UIUtility.ENTRY.NKBNO12_CNT
                _ipc.SYSTEM.NKBNO13_CNT = UIUtility.ENTRY.NKBNO13_CNT
                _ipc.SYSTEM.ENT_CNT = UIUtility.ENTRY.ENT_CNT
                _ipc.SYSTEM.SUMBALL = UIUtility.SYSTEM.SUMBALL
            End If

            '***************************************************************************************

            '【単価切り替え時間】
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & intTIMEKB)
            If dr.Length > 0 Then UIUtility.SYSTEM.NEXTTIMENM = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)


            '【パスワード】
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & intTIMEKB)
            If dr.Length > 0 Then UIUtility.SYSTEM.NOWPASSCD = dr(0).Item("PASSCD").ToString.PadLeft(2, "0"c)

            '【打席指定区分】
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & intTIMEKB)
            If dr.Length > 0 Then UIUtility.SYSTEM.SITEIKBN = CType(dr(0).Item("SITEIKBN").ToString, Integer)

            inte = 3

            '【打席サーバーより打席情報取得】
            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then

                UIUtility.TABLE.SEATINFO = _ipc.SYSTEM.SEATINFO
                '呼び出し音
                _blnYOBIDASHI = _ipc.SYSTEM.YOBIDASHI
                If _blnYOBIDASHI Then
                    StopCallSound()
                    PlayCallSound()
                Else
                    StopCallSound()
                End If
            End If


            inte = 4



            inte = 5

            '【打席管理の端末のみ】
            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                Try
                    ' HACK ADD 2018/03/06 TERAYAMA
                    ' *** STA                
                    If Not _ipc.SYSTEM.UPDATED Then
                        _ipc.SYSTEM.SEATINFO = UIUtility.TABLE.SEATINFO
                        _ipc.SYSTEM.UPDATED = True
                    End If
                    ' *** END
                Catch ex As Exception
                    MessageBox.Show("でたわ")
                End Try

            End If

            inte = 6

        Catch ex As Exception
            Me.Timer1.Stop()
            MessageBox.Show(inte.ToString() & ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボール貸出機 タイマー2_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Dim intTIMEKB As Integer = 0
        Dim dr As DataRow()
        Try
            '本日の料金体系
            Me.lblRKNNM.ForeColor = Color.FromArgb(UIUtility.COLOR_INFO.RKN_R, UIUtility.COLOR_INFO.RKN_G, UIUtility.COLOR_INFO.RKN_B)
            Me.lblRKNNM.Text = "【" & UIUtility.SYSTEM.RKNNM & "】"

            '本日日付時間(yyyy/MM/dd (曜日))
            UIUtility.SYSTEM.DAYTIME = Now.Year.ToString & "年" _
                                    & Now.Month.ToString.PadLeft(2, "0"c) & "月" _
                                    & Now.Day.ToString.PadLeft(2, "0"c) & "日" & "(" & WeekdayName(Weekday(CType(Now.ToString("yyyy/MM/dd"), Date))).Substring(0, 1) & ")" _
                                    & Now.Hour.ToString.PadLeft(2, "0"c) & "時" _
                                    & Now.Minute.ToString.PadLeft(2, "0"c) & "分"
            Me.lblDAYTIME.Text = UIUtility.SYSTEM.DAYTIME

            '営業情報取得
            GetEIGMT()

            '総打球数取得
            GetSUMBALL()
            '総来場者数
            GetSUMENTCNT()

            '*** 現在の時間帯確認 ***************************************************************
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB)

            intTIMEKB = 0
            For i As Integer = 0 To dr.Length - 1
                If dr(i).Item("TIMENM").ToString >= Now.ToString("HHmm") Then
                    intTIMEKB = CType(dr(i).Item("TIMEKB").ToString, Integer)
                    Exit For
                End If
            Next
            If intTIMEKB.Equals(0) Then
                intTIMEKB = 1
            End If
            If Not _intTIMEKB.Equals(intTIMEKB) Then
                _intTIMEKB = intTIMEKB
            End If
            UIUtility.SYSTEM.NOWTIMEKB = _intTIMEKB
            '***************************************************************************************

            '【単価切り替え時間】
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & intTIMEKB)
            If dr.Length > 0 Then UIUtility.SYSTEM.NEXTTIMENM = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)


            '【パスワード】
            dr = UIUtility.TABLE.EIGMT.Select("NKBNO = 1 AND RKNKB = " & UIUtility.SYSTEM.RKNKB & " AND TIMEKB = " & intTIMEKB)
            If dr.Length > 0 Then UIUtility.SYSTEM.NOWPASSCD = dr(0).Item("PASSCD").ToString.PadLeft(2, "0"c)

            '【ﾍﾞﾝﾀﾞｰ呼び出し監視】
            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                '呼び出し音
                UIUtility.SYSTEM.BNDCALL = GetBndCall()
                If UIUtility.SYSTEM.BNDCALL Then
                    StopCallSound()
                    PlayCallSound()
                Else
                    StopCallSound()
                End If
            End If


        Catch ex As Exception
            Me.Timer2.Stop()
        End Try
    End Sub


    ''' <summary>
    ''' メニューボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBusiness.Click, btnMaster.Click, btnPrint.Click, btnSchool.Click
        Try

            Dim btn As Button
            btn = CType(sender, Button)

            If CType(btn.Tag, Integer).Equals(2) Then
                If UIUtility.SYSTEM.MENUKBN.Substring(1, 1).Equals("0") Then
                    Using frm As New frmPWDISP01
                        frm.PWKBN = 2
                        frm.ShowDialog()
                        If Not frm.CLEAR Then Exit Sub
                    End Using
                End If
            End If

            Me.pnlBusiness.Visible = False
            Me.btnBusiness.ForeColor = Color.White
            Me.pnlMaster.Visible = False
            Me.btnMaster.ForeColor = Color.White
            Me.pnlPrint.Visible = False
            Me.btnPrint.ForeColor = Color.White
            Me.pnlSchool.Visible = False
            Me.btnSchool.ForeColor = Color.White

            Select Case CType(btn.Tag, Integer)
                Case 1
                    '【業務】
                    Me.pnlBusiness.Visible = True
                    Me.btnBusiness.ForeColor = Color.Yellow
                Case 2
                    '【マスタ登録】
                    Me.pnlMaster.Visible = True
                    Me.btnMaster.ForeColor = Color.Yellow

                Case 3
                    '【帳票】
                    Me.pnlPrint.Visible = True
                    Me.btnPrint.ForeColor = Color.Yellow
                Case 4
                    '【スクール】
                    Me.pnlSchool.Visible = True
                    Me.btnSchool.ForeColor = Color.Yellow

            End Select

            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター

                Me.btnPRINT02.Text = "打席管理"
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機

                Me.btnPRINT02.Text = "ベンダー管理"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "▼業務ボタン関連"

    ''' <summary>
    ''' 受付画面ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFRONT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFRONT01.Click
        Try

            If String.IsNullOrEmpty(UIUtility.SYSTEM.NOWPASSCD) Then Exit Sub

            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //

                If CType(Me.btnFRONT01.Tag, Integer).Equals(1) Then
                    '【受付画面表示】
                    Using frm As New frmFRONT01(iDatabase, _dcICR700)
                        frm.ShowDialog()
                    End Using
                Else
                    '【打席情報表示】
                    Using frm As New frmSEATSELECT01(iDatabase)
                        frm.MODE = frmSEATSELECT01.Mode_Type.NORMAL
                        frm.ShowDialog()
                    End Using
                End If
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                '【受付画面表示】
                Using frm As New frmFRONT02(iDatabase, _dcICR700)
                    frm.ShowDialog()
                End Using

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ドロワ管理ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDRA01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDRA01.Click
        Try
            Using frm As New frmDRA01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' カード初期化ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCARDINIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARDINIT.Click
        Try
            Using frm As New frmMSGBOX01("カード初期化を行ってよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '【ICリーダーライター書き込み情報セット】
            '店番号
            _dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            _dcICR700.PASSCD_WR = "00"
            'シリアルナンバー
            _dcICR700.SERIALNO_WR = "000"
            '種別
            _dcICR700.SYUBETU_WR = "0"
            '金額
            _dcICR700.KINGAKU_WR = "00000"
            '予備
            _dcICR700.YOBI_WR = "0"
            'カード区分
            _dcICR700.CARDKBN_WR = "1"
            'カード番号
            _dcICR700.CARDNO_WR = "00000000"
            '顧客番号
            _dcICR700.NCSNO_WR = "00000000"
            'スクール生番号
            _dcICR700.SCLMANNO_WR = "000000"
            '顧客種別
            _dcICR700.NKBNO_WR = "0"
            '会員期限
            _dcICR700.DMEMBER_WR = "00000000"
            '残金額
            _dcICR700.ZANKN_WR = "00000"
            'P残金額
            _dcICR700.PREZANKN_WR = "00000"
            '残ポイント
            _dcICR700.POINT_WR = "00000"
            '前回来場日
            _dcICR700.ZENENTDATE_WR = "00000000"
            '入場区分
            _dcICR700.ENTKBN_WR = "0"
            'ボール単価
            _dcICR700.BALLKIN_WR = "00"
            '打席番号
            _dcICR700.SEATNO_WR = "FFF"

            Using frm As New frmREQUESTCARD(_dcICR700)
                'カード初期化処理
                frm.COMMAND = frmREQUESTCARD.Command_Type.CARDINIT
                frm.ShowDialog()
                'キャンセル押下
                If frm.CANCEL Then Exit Sub
            End Using



            'MessageBox.Show("初期化")

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' RW再接続ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReRW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReRW.Click
        Dim blnErr As Boolean = False
        Dim blnEnabled As Boolean = True
        Dim intCount As Integer = 1
        Try
            Me.Enabled = False

            '*** ICR700リーダライター接続と初期化 ***'
            Do
                If _dcICR700.IsOpen Then
                    _dcICR700.Close()
                End If
                If Not _dcICR700.Open(UIUtility.SYSTEM.ICR700COM) Then
                    blnErr = True
                    Exit Do
                End If
                Dim keys = New Byte() {&H36, &H37, &H37, &H37, &H37, &H32}
                If Not _dcICR700.AuthKeySet(keys, False, 2, 4) Then
                    blnErr = True
                    Exit Do
                End If
                If Not _dcICR700.Cancel Then
                    blnErr = True
                    Exit Do
                End If
                ''ﾘｾｯﾄ
                'If Not _dcICR700.Reset Then
                '    blnErr = True
                '    Exit Do
                'End If
                Exit Do
            Loop

            If blnErr Then
                Using frm As New frmMSGBOX01("ICリーダライターとの接続に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Enabled = True
            If blnErr Then
                Me.btnFRONT01.Text = "打席情報"
                Me.btnFRONT01.Tag = 0
                blnEnabled = False
            Else
                Me.btnFRONT01.Text = "受付"
                Me.btnFRONT01.Tag = 1
                blnEnabled = True
            End If
            '受付
            Me.btnFRONT01.Enabled = True
            'カード初期化
            Me.btnCARDINIT.Enabled = blnEnabled
            '入金
            Me.btnNKNDISP01.Enabled = blnEnabled
            'サービス入金
            Me.btnNKNDISP02.Enabled = blnEnabled
            'カード支出
            'Me.btnNKNDISP03.Enabled = blnEnabled
            '商品引落し
            Me.btnHINDISP01.Enabled = blnEnabled
            'ポイント還元
            Me.btnDREPODISP01.Enabled = blnEnabled
            '無記名ｶｰﾄﾞ
            Me.btnNAMELESS01.Enabled = blnEnabled
            'フリーカード
            'Me.btnFREECARD01.Enabled = blnEnabled

        End Try
    End Sub

    ''' <summary>
    ''' 入金処理ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNDISP01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNDISP01.Click
        Try
            Using frm As New frmNKNDISP01(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ｻｰﾋﾞｽ入金処理ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNDISP02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNDISP02.Click
        Try
            Using frm As New frmNKNDISP02(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード支出ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNDISP03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNDISP03.Click
        Try
            Using frm As New frmNKNDISP03(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ﾎﾟｲﾝﾄ還元ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDREPODISP01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDREPODISP01.Click
        Try
            Using frm As New frmDREPODISP01(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 名無しカード作成ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNAMELESS01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNAMELESS01.Click
        Try
            Using frm As New frmNAMELESS01(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ﾌﾘｰｶｰﾄﾞ作成ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFREECARD01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFREECARD01.Click
        Try
            Using frm As New frmFREECARD01(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 停止中カードボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDCSTPTRN01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDCSTPTRN01.Click
        Try
            Using frm As New frmDCSTPTRN01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 商品引落し画面押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHINDISP01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHINDISP01.Click
        Try
            Using frm As New frmHINDISP01(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 顧客検索画面押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCSSERCH02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSSERCH02.Click
        Try
            Using frm As New frmCSSERCH02(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼マスタ登録ボタン関連"

    ''' <summary>
    ''' 営業カレンダー登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCALMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCALMT01.Click
        Try
            Using frm As New frmCALMT01(iDatabase)
                frm.ShowDialog()
            End Using



            '本日料金体系情報取得
            If Not GetRknInfo() Then
                Using frm As New frmMSGBOX01("料金体系情報の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 営業情報登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEIGMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIGMT01.Click
        Try
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //

                Using frm As New frmEIGMT01(iDatabase)
                    frm.ShowDialog()
                End Using

            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                Using frm As New frmEIGMT03(iDatabase)
                    frm.ShowDialog()
                End Using
            End If



            '営業情報マスタ取得
            If Not GetEIGMT() Then
                Using frm As New frmMSGBOX01("営業情報マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'スレッド処理再開

        End Try
    End Sub

    ''' <summary>
    ''' 打ち放題情報登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEIGMT02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIGMT02.Click
        Try
            Using frm As New frmEIGMT02(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 月間来場ポイントマスタ登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnETPMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnETPMT01.Click
        Try
            Using frm As New frmETPMT01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' システム情報登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSYSMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSYSMT01.Click
        Dim blnSEFLG As Boolean = False
        Try

            Using frm As New frmPWDISP01
                frm.PWKBN = 2
                frm.ShowDialog()
                If Not frm.CLEAR Then Exit Sub
                blnSEFLG = frm.SEFLG
            End Using

            Using frm As New frmSYSMT01(iDatabase)
                frm.SEFLG = blnSEFLG
                frm.ShowDialog()
            End Using

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'メニュー区分
            Me.btnBusiness.Enabled = False
            Me.pnlBusiness.Enabled = False
            If UIUtility.SYSTEM.MENUKBN.Substring(0, 1).Equals("1") Then
                Me.btnBusiness.Enabled = True
                Me.pnlBusiness.Enabled = True
            End If
            'Me.btnMaster.Enabled = False
            'Me.pnlMaster.Enabled = False
            'If UIUtility.SYSTEM.MENUKBN.Substring(1, 1).Equals("1") Then
            '    Me.btnMaster.Enabled = True
            '    Me.pnlMaster.Enabled = True
            'End If
            Me.btnPrint.Enabled = False
            Me.pnlPrint.Enabled = False
            If UIUtility.SYSTEM.MENUKBN.Substring(2, 1).Equals("1") Then
                Me.btnPrint.Enabled = True
                Me.pnlPrint.Enabled = True
            End If
            Me.btnSchool.Enabled = False
            Me.pnlSchool.Enabled = False
            If UIUtility.SYSTEM.MENUKBN.Substring(3, 1).Equals("1") Then
                Me.btnSchool.Enabled = True
                Me.pnlSchool.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKBMAST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKBMAST01.Click
        Try
            Using frm As New frmKBMAST01(iDatabase)
                frm.ShowDialog()
            End Using



            '顧客種別情報マスタ取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別マスタの取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 顧客情報登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCSMAST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSMAST01.Click
        Try
            Using frm As New frmCSMAST01(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 業務終了ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEND.Click
        Try
            '呼び出し音関連
            Call mciSendString("stop MySound", "", 0, 0)
            Call mciSendString("close MySound", "", 0, 0)

            '営業終了(スレッドで使用)
            _blnEndFlg = True

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 入金マスタ登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNMST01.Click
        Try
            Using frm As New frmNKNMST01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' サービス入金マスタ登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNMT02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNMST02.Click
        Try
            Using frm As New frmNKNMST02(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード支出マスタ登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNMT03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNMST03.Click
        Try
            Using frm As New frmNKNMST03(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ポイント還元マスタ登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDREPOMST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDREPOMST01.Click
        Try
            Using frm As New frmDREPOMST01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 商品マスタ登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHINMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHINMT01.Click
        Try
            Using frm As New frmHINMT01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テロップ登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDTELOP01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDTELOP01.Click
        Try
            Using frm As New frmDTELOP01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ポイントマスタ登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPOINTMST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOINTMST01.Click
        Try
            Using frm As New frmPOINTMST01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' スタッフ登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSTAFFMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTAFFMT01.Click
        Try
            Using frm As New frmSTAFFMT01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 画面セキュリティ登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRGMT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRGMT01.Click
        Dim blnSEFLG As Boolean = False
        Try
            Using frm As New frmPWDISP01
                frm.PWKBN = 2
                frm.ShowDialog()
                If Not frm.CLEAR Then Exit Sub
                blnSEFLG = frm.SEFLG
            End Using

            Using frm As New frmPRGMT01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼帳票ボタン関連"

    ''' <summary>
    ''' 顧客一覧/ラベルボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT01.Click
        Try
            Using frm As New frmPRINT01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 打席管理ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT02.Click
        Try
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター

                Using frm As New frmPRINT02(iDatabase)
                    frm.ShowDialog()
                End Using
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機
                Using frm As New frmPRINT14(iDatabase)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 売上管理ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT03.Click
        Try
            Using frm As New frmPRINT03(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 時間帯別入場者情報ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT04_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT04.Click
        Try
            Using frm As New frmPRINT04(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 期間来場者分析ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT05_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT05.Click
        Try
            Using frm As New frmPRINT05(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 商品売上ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT06_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT06.Click
        Try
            Using frm As New frmPRINT06(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 推移表ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT07_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT07.Click
        Try
            Using frm As New frmPRINT07(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 入金履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT08_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT08.Click
        Try
            Using frm As New frmPRINT08(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ｶｰﾄﾞ支出履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT09_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT09.Click
        Try
            Using frm As New frmPRINT09(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ﾎﾟｲﾝﾄ還元履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT10.Click
        Try
            Using frm As New frmPRINT10(iDatabase, _dcICR700)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 入金額別情報ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT11.Click
        Try
            Using frm As New frmPRINT11(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード再発行履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT12.Click
        Try
            Using frm As New frmPRINT12(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 領収書発行履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPRINT13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT13.Click
        Try
            Using frm As New frmPRINT13(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ミニゲームポイント履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGAMEPOHIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGAMENPOHIST01.Click
        Try
            Using frm As New frmGAMEPOHIST01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 残高クリア履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLKINHIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLKINHIST01.Click
        Try
            Using frm As New frmLKINHIST01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' ドロアオープンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOpenDwa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDwa.Click
        Dim rePrint As New Receipt
        Try
            Me.btnOpenDwa.Enabled = False

            Dim strErrMsg2 As String = String.Empty
            Dim isOpen As Boolean = True

            Application.DoEvents()

            rePrint.OpenDrawer(isOpen, strErrMsg2)

            Application.DoEvents()



        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.btnOpenDwa.Enabled = True
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
            '料金体系
            Me.lblRKNNM.Text = String.Empty
            '本日日付時間
            Me.lblDAYTIME.Text = String.Empty

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
            sql.Append(",TO_CHAR(UPDDTM,'YYYYMMDD') AS UPDDAY")
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
            UIUtility.SYSTEM.UPDDTM = resultDt.Rows(0).Item("UPDDAY").ToString()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' カレンダーマスタ作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsCALMTA() As Boolean
        Try

            If String.IsNullOrEmpty(UIUtility.SYSTEM.CALLSTDT) Then
                '//２年分のカレンダー情報作成
                Dim strSQL01 As String
                Dim strSQL02 As String

                Dim intYear As Integer = CType(Now.Year.ToString, Integer)
                Dim intMonth As Integer = CType(Now.Month.ToString, Integer)
                Dim strCALLSTDT As String = String.Empty
                Dim intYoubi As Integer = 0

                '指定した月の日数保持用
                Dim intMonthDay As Integer = 0

                'カレンダーマスタデータ削除
                If Not (iDatabase.ExecuteUpdate("DELETE FROM CALMTA")) Then Return False

                '2年分
                For intI As Integer = intYear To (intYear + 2)

                    For intJ As Integer = 1 To 12
                        If intJ < intMonth Then
                            Continue For
                        End If
                        If intI.Equals((intYear + 2)) Then
                            If intJ = CType(DateTime.Now.Month.ToString, Integer) Then
                                Exit For
                            End If
                        End If
                        intMonthDay = DateTime.DaysInMonth(intI, intJ)
                        For intK As Integer = 1 To intMonthDay

                            strSQL01 = "INSERT INTO CALMTA ("
                            strSQL02 = "VALUES ("

                            strCALLSTDT = intI.ToString & intJ.ToString.PadLeft(2, "0"c) & intK.ToString.PadLeft(2, "0"c)

                            intYoubi = Weekday(CType(intI.ToString & "/" & intJ.ToString.PadLeft(2, "0"c) & "/" & intK.ToString.PadLeft(2, "0"c), Date))

                            'カレンダー日付
                            strSQL01 &= "CALDT, "
                            strSQL02 &= "'" & strCALLSTDT & "',"
                            '料金体系区分
                            strSQL01 &= "RKNKB, "
                            If intYoubi.Equals(1) Or intYoubi.Equals(7) Then
                                strSQL02 &= 2 & ","
                            Else
                                strSQL02 &= 1 & ","
                            End If

                            '作成日時
                            strSQL01 &= "INSDTM,"
                            strSQL02 &= "NOW(),"
                            '更新日時
                            strSQL01 &= "UPDDTM"
                            strSQL02 &= "NOW()"

                            strSQL01 &= ") "
                            strSQL02 &= ")"

                            If Not (iDatabase.ExecuteUpdate(strSQL01 & strSQL02)) Then Return False
                        Next
                    Next
                    intMonth = 1
                Next

                'システム情報へカレンダー作成最終日付登録
                If Not (iDatabase.ExecuteUpdate("UPDATE SYSMTA SET CALLSTDT = '" & strCALLSTDT & "' WHERE SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")) Then Return False

            Else
                '//システム起動するたびに１日分作成

                Dim sql As New System.Text.StringBuilder

                '本日より過去のカレンダー情報削除
                If Not (iDatabase.ExecuteUpdate("DELETE FROM CALMTA WHERE CALDT < '" & Now.Date.Year.ToString & Now.Date.Month.ToString.PadLeft(2, "0"c) & Now.Date.Day.ToString.PadLeft(2, "0"c) & "'")) Then
                    iDatabase.RollBack()
                    Return False
                End If

                ' 日付と時刻を格納するための変数を宣言する
                Dim dtCALLSTDT As DateTime = DateTime.Parse(UIUtility.SYSTEM.CALLSTDT.Substring(0, 4) _
                                            & "/" & UIUtility.SYSTEM.CALLSTDT.Substring(4, 2) _
                                            & "/" & UIUtility.SYSTEM.CALLSTDT.Substring(6, 2))

                '1日加算する
                dtCALLSTDT = dtCALLSTDT.AddDays(1)

                'カレンダーマスタ１日分作成
                sql.Clear()
                sql.Append("INSERT INTO CALMTA VALUES('" & dtCALLSTDT.ToString("yyyyMMdd") & "'")
                Dim intYoubi As Integer = Weekday(CType(dtCALLSTDT.ToString("yyyy/MM/dd"), DateTime))
                If intYoubi.Equals(1) Or intYoubi.Equals(7) Then
                    sql.Append(",2,NOW(),NOW())")
                Else
                    sql.Append(",1,NOW(),NOW())")
                End If
                If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                    iDatabase.RollBack()
                    Return False
                End If

                'システム情報へカレンダー作成最終日付登録
                If Not (iDatabase.ExecuteUpdate("UPDATE SYSMTA SET CALLSTDT = '" & dtCALLSTDT.ToString("yyyyMMdd") & "',UPDDTM = NOW()")) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
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

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

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
            If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 月間来場回数0クリア処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClrENTCNT2() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append("UPDATE SYSMTA SET")
            sql.Append(" CLRENTMCNT = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & "'")
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")
            If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                iDatabase.RollBack()
                Return False
            End If

            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            sql.Append(" ENTCNT2 = 0")
            sql.Append(",DUPDATE = NOW()")
            If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 時間貸しトラン削除
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClrTIMTRA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append("DELETE FROM TIMTRA WHERE")
            sql.Append(" UDNDT < '" & UIUtility.SYSTEM.UPDDAY & "'")

            If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 打席情報ワーク作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsSEATWORK() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEATWORK")
            sql.Append(" WHERE SEATDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" ORDER BY")
            sql.Append(" SEATNO,LEFTKB")


            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                '【打席予約状況テーブル】
                If Not (iDatabase.ExecuteUpdate("DELETE FROM SEATRSV WHERE SEATDT < '" & Now.ToString("yyyyMMdd") & "'")) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '【打席情報ワーク作成】
                If Not (iDatabase.ExecuteUpdate("DELETE FROM SEATWORK")) Then
                    iDatabase.RollBack()
                    Return False
                End If

                '右打席
                For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                    strSQL01 = "INSERT INTO SEATWORK ("
                    strSQL02 = "VALUES ("

                    '営業日付
                    strSQL01 &= "SEATDT, "
                    strSQL02 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"

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
                        strSQL02 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"

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
                Return True
            End If

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

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 打席情報サマリA作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsSEATSMA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEATSMA")
            sql.Append(" WHERE SEATDT = '" & UIUtility.SYSTEM.UPDDAY & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                '【打席情報サマリA作成】

                For i As Integer = 1 To UIUtility.SYSTEM.FLRSU
                    For j As Integer = 1 To 5   '時間帯(1～5)
                        strSQL01 = "INSERT INTO SEATSMA ("
                        strSQL02 = "VALUES ("

                        '日付
                        strSQL01 &= "SEATDT, "
                        strSQL02 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
                        'フロア区分
                        strSQL01 &= "FLRKB, "
                        strSQL02 &= i & ","
                        '時間帯区分
                        strSQL01 &= "TIMEKB, "
                        strSQL02 &= j & ","

                        For k As Integer = 1 To 15  '種別1～15
                            '打球数
                            strSQL01 &= "NKB" & k.ToString.PadLeft(2, "0"c) & "BALL, "
                            strSQL02 &= "0,"
                            '打球金額
                            strSQL01 &= "NKB" & k.ToString.PadLeft(2, "0"c) & "KIN, "
                            strSQL02 &= "0,"
                            '打球消費税
                            strSQL01 &= "NKB" & k.ToString.PadLeft(2, "0"c) & "TAXKIN, "
                            strSQL02 &= "0,"
                            '打球小税率
                            strSQL01 &= "NKB" & k.ToString.PadLeft(2, "0"c) & "TAX, "
                            strSQL02 &= "0,"
                            '度数
                            strSQL01 &= "NKB" & k.ToString.PadLeft(2, "0"c) & "DOSU, "
                            strSQL02 &= "0,"
                        Next

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
                Next
                Return True
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 打席情報サマリB作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsSEATSMB() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEATSMB")
            sql.Append(" WHERE SEATDT = '" & UIUtility.SYSTEM.UPDDAY & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                '【打席情報サマリB作成】

                strSQL01 = "INSERT INTO SEATSMB ("
                strSQL02 = "VALUES ("

                '日付
                strSQL01 &= "SEATDT, "
                strSQL02 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"

                For k As Integer = 1 To 100
                    '打球数
                    strSQL01 &= "BALL" & k.ToString.PadLeft(3, "0"c) & ", "
                    strSQL02 &= "0,"

                    '度数
                    strSQL01 &= "DOSU" & k.ToString.PadLeft(3, "0"c) & ", "
                    strSQL02 &= "0,"

                    '利用時間
                    strSQL01 &= "TIME" & k.ToString.PadLeft(3, "0"c) & ", "
                    strSQL02 &= "0,"
                Next

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

                Return True
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 打席情報サマリC作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsSEATSMC() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEATSMC")
            sql.Append(" WHERE SEATDT = '" & Now.ToString("yyyyMMdd") & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                '【打席情報サマリC作成】

                For i As Integer = 0 To 24
                    strSQL01 = "INSERT INTO SEATSMC ("
                    strSQL02 = "VALUES ("

                    '日付
                    strSQL01 &= "SEATDT, "
                    strSQL02 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
                    '時間
                    strSQL01 &= "JIKAN, "
                    strSQL02 &= "'" & i.ToString.PadLeft(2, "0"c) & "',"
                    'サービス人数
                    strSQL01 &= "SERVICECNT, "
                    strSQL02 &= "0,"
                    '入場者数
                    strSQL01 &= "ENTCNT, "
                    strSQL02 &= "0,"
                    '打球数
                    strSQL01 &= "BALLSU, "
                    strSQL02 &= "0,"
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



                Return True
            End If

            Return True

        Catch ex As Exception
            Throw ex
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

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

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
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If Not UIUtility.TABLE.KBMAST Is Nothing Then
                UIUtility.TABLE.KBMAST.Clear()
            End If

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY KBMAST ")

            UIUtility.TABLE.KBMAST = iDatabase.ExecuteRead(sql.ToString())

            If UIUtility.TABLE.KBMAST.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex

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
            If Not UIUtility.TABLE.EIGMT Is Nothing Then
                UIUtility.TABLE.EIGMT.Clear()
            End If

            sql.Append(" SELECT")
            sql.Append(" *")
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                ' // オートセッター //

                sql.Append(" FROM EIGMTA")
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                sql.Append(" FROM EIGMTC")
            End If
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
    ''' パスワード更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdPASSCD() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strTable As String = String.Empty
        Try
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //
                strTable = "EIGMTA"
            Else
                '// ボール貸出機 //
                strTable = "EIGMTC"
            End If

            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(",TO_CHAR(UPDDTM,'YYYYMMDD') AS UPDDAY")
            sql.Append(" FROM")
            sql.Append(" " & strTable)
            sql.Append(" WHERE")
            sql.Append(" NKBNO = 1")
            sql.Append(" AND RKNKB = " & UIUtility.SYSTEM.RKNKB)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            If resultDt.Rows(0).Item("UPDDAY").Equals(UIUtility.SYSTEM.UPDDAY) Then
                'パスワードは既に更新済み
                Return True
            End If

            Dim cRandom As New System.Random()
            Dim intPASSCD1 As Integer = cRandom.Next(1, 90)
            Dim intPASSCD2 As Integer = cRandom.Next(1, 90)
            Dim intPASSCD3 As Integer = cRandom.Next(1, 90)
            Dim intPASSCD4 As Integer = cRandom.Next(1, 90)
            Dim intPASSCD5 As Integer = cRandom.Next(1, 90)

            'パスワードがかぶらないようにする
            Do
                If intPASSCD1.Equals(intPASSCD2) Or intPASSCD1.Equals(intPASSCD3) Or intPASSCD1.Equals(intPASSCD4) Or intPASSCD1.Equals(intPASSCD5) Then
                    intPASSCD1 = cRandom.Next(1, 90)
                Else
                    Exit Do
                End If
            Loop
            Do
                If intPASSCD2.Equals(intPASSCD1) Or intPASSCD2.Equals(intPASSCD3) Or intPASSCD2.Equals(intPASSCD4) Or intPASSCD2.Equals(intPASSCD5) Then
                    intPASSCD2 = cRandom.Next(1, 90)
                Else
                    Exit Do
                End If
            Loop
            Do
                If intPASSCD3.Equals(intPASSCD1) Or intPASSCD3.Equals(intPASSCD2) Or intPASSCD3.Equals(intPASSCD4) Or intPASSCD3.Equals(intPASSCD5) Then
                    intPASSCD3 = cRandom.Next(1, 90)
                Else
                    Exit Do
                End If
            Loop
            Do
                If intPASSCD4.Equals(intPASSCD1) Or intPASSCD4.Equals(intPASSCD2) Or intPASSCD4.Equals(intPASSCD3) Or intPASSCD4.Equals(intPASSCD5) Then
                    intPASSCD4 = cRandom.Next(1, 90)
                Else
                    Exit Do
                End If
            Loop
            Do
                If intPASSCD5.Equals(intPASSCD1) Or intPASSCD5.Equals(intPASSCD2) Or intPASSCD5.Equals(intPASSCD3) Or intPASSCD5.Equals(intPASSCD4) Then
                    intPASSCD5 = cRandom.Next(1, 90)
                Else
                    Exit Do
                End If
            Loop


            Dim dr As DataRow()
            '時間帯1
            dr = resultDt.Select("TIMEKB = 1")
            If dr.Length > 0 Then
                sql.Clear()
                sql.Append("UPDATE " & strTable & " SET")
                If (Not dr(0).Item("PASSCD").ToString.Equals("00")) And (Not dr(0).Item("PASSCD").ToString.Equals("99")) Then
                    sql.Append(" PASSCD = '" & intPASSCD1.ToString.PadLeft(2, "0"c) & "'")
                    sql.Append(",UPDDTM = NOW()")
                Else
                    sql.Append(" UPDDTM = NOW()")
                End If
                sql.Append(" WHERE")
                sql.Append(" PASSCD = '" & dr(0).Item("PASSCD").ToString & "'")
                sql.Append(" AND RKNKB = " & UIUtility.SYSTEM.RKNKB)
                If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If
            '時間帯2
            dr = resultDt.Select("TIMEKB = 2")
            If dr.Length > 0 Then
                sql.Clear()
                sql.Append("UPDATE " & strTable & " SET")
                If (Not dr(0).Item("PASSCD").ToString.Equals("00")) And (Not dr(0).Item("PASSCD").ToString.Equals("99")) Then
                    sql.Append(" PASSCD = '" & intPASSCD2.ToString.PadLeft(2, "0"c) & "'")
                    sql.Append(",UPDDTM = NOW()")
                Else
                    sql.Append(" UPDDTM = NOW()")
                End If
                sql.Append(" WHERE")
                sql.Append(" PASSCD = '" & dr(0).Item("PASSCD").ToString & "'")
                sql.Append(" AND RKNKB = " & UIUtility.SYSTEM.RKNKB)
                If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If
            '時間帯3
            dr = resultDt.Select("TIMEKB = 3")
            If dr.Length > 0 Then
                sql.Clear()
                sql.Append("UPDATE " & strTable & " SET")
                If (Not dr(0).Item("PASSCD").ToString.Equals("00")) And (Not dr(0).Item("PASSCD").ToString.Equals("99")) Then
                    sql.Append(" PASSCD = '" & intPASSCD3.ToString.PadLeft(2, "0"c) & "'")
                    sql.Append(",UPDDTM = NOW()")
                Else
                    sql.Append(" UPDDTM = NOW()")
                End If
                sql.Append(" WHERE")
                sql.Append(" PASSCD = '" & dr(0).Item("PASSCD").ToString & "'")
                sql.Append(" AND RKNKB = " & UIUtility.SYSTEM.RKNKB)
                If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If
            '時間帯4
            dr = resultDt.Select("TIMEKB = 4")
            If dr.Length > 0 Then
                sql.Clear()
                sql.Append("UPDATE " & strTable & " SET")
                If (Not dr(0).Item("PASSCD").ToString.Equals("00")) And (Not dr(0).Item("PASSCD").ToString.Equals("99")) Then
                    sql.Append(" PASSCD = '" & intPASSCD4.ToString.PadLeft(2, "0"c) & "'")
                    sql.Append(",UPDDTM = NOW()")
                Else
                    sql.Append(" UPDDTM = NOW()")
                End If
                sql.Append(" WHERE")
                sql.Append(" PASSCD = '" & dr(0).Item("PASSCD").ToString & "'")
                sql.Append(" AND RKNKB = " & UIUtility.SYSTEM.RKNKB)
                If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If
            '時間帯5
            dr = resultDt.Select("TIMEKB = 5")
            If dr.Length > 0 Then
                sql.Clear()
                sql.Append("UPDATE " & strTable & " SET")
                If (Not dr(0).Item("PASSCD").ToString.Equals("00")) And (Not dr(0).Item("PASSCD").ToString.Equals("99")) Then
                    sql.Append(" PASSCD = '" & intPASSCD5.ToString.PadLeft(2, "0"c) & "'")
                    sql.Append(",UPDDTM = NOW()")
                Else
                    sql.Append(" UPDDTM = NOW()")
                End If
                sql.Append(" WHERE")
                sql.Append(" PASSCD = '" & dr(0).Item("PASSCD").ToString & "'")
                sql.Append(" AND RKNKB = " & UIUtility.SYSTEM.RKNKB)
                If Not (iDatabase.ExecuteUpdate(sql.ToString)) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 総打球数取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSUMBALL() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //

                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" SEATDT")
                sql.Append(",(")
                For i As Integer = 1 To 15  '種別1～15
                    sql.Append("SUM(NKB" & i.ToString.PadLeft(2, "0"c) & "BALL)")
                    If Not i.Equals(15) Then
                        sql.Append(" + ")
                    End If
                Next
                sql.Append(" + (CASE WHEN (SELECT SUM(USEBALLCNT) FROM SEATWORK WHERE UPDFLG = 1) IS NULL THEN 0 ELSE (SELECT SUM(USEBALLCNT) FROM SEATWORK WHERE UPDFLG = 1) END)")
                sql.Append(") AS SUMBALL")
                sql.Append(" FROM SEATSMA ")
                sql.Append(" WHERE SEATDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
                sql.Append(" GROUP BY SEATDT")
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                sql.Clear()
                sql.Append(" SELECT")
                sql.Append(" (CASE WHEN SUM(BALLSU) IS NULL THEN 0 ELSE SUM(BALLSU) END) AS SUMBALL FROM SRTTRA")
                sql.Append(" WHERE UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            End If


            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                UIUtility.SYSTEM.SUMBALL = 0
                Return False
            End If

            UIUtility.SYSTEM.SUMBALL = CType(resultDt.Rows(0).Item("SUMBALL").ToString, Integer)

            Return True

        Catch ex As Exception
            MessageBox.Show(sql.ToString)
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 来場者情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSUMENTCNT() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            '【総球数金額】
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                '// オートセッター //

                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" SUM(USEKIN) AS USEKIN")
                sql.Append(" FROM BALLTRN")
                sql.Append(" WHERE")
                sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            ElseIf UIUtility.SYSTEM.SYSTEMMODE.Equals(1) Then
                '// ボール貸出機 //

                sql.Clear()
                sql.Append(" SELECT")
                sql.Append(" (CASE WHEN SUM(KAGOAKN) IS NULL THEN 0 ELSE SUM(KAGOAKN) END) AS USEKIN FROM SRTTRA")
                sql.Append(" WHERE UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            End If



            Dim dtBALLTRN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If String.IsNullOrEmpty(dtBALLTRN.Rows(0).Item("USEKIN").ToString) Then
                UIUtility.ENTRY.USEKIN_SUM = 0
            Else
                UIUtility.ENTRY.USEKIN_SUM = CType(dtBALLTRN.Rows(0).Item("USEKIN").ToString, Integer)
            End If


            '【総来場者数】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DISTINCT")
            sql.Append(" MANNO")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND KSBKB <> '14'")

            Dim dtENTCNT As DataTable = iDatabase.ExecuteRead(sql.ToString())

            UIUtility.ENTRY.ENT_CNT = dtENTCNT.Rows.Count

            '【総受付数】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" MANNO")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND KSBKB <> '14'")

            Dim dtACCEPT As DataTable = iDatabase.ExecuteRead(sql.ToString())

            UIUtility.ENTRY.ACCEPT_CNT = dtACCEPT.Rows.Count

            '【無人営業来場者】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DISTINCT")
            sql.Append(" MANNO")
            sql.Append(" FROM ENTTRB")
            sql.Append(" WHERE")
            sql.Append(" ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")

            Dim dtENTCNT2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            UIUtility.ENTRY.ENT_CNT += dtENTCNT2.Rows.Count

            '【無人営業受付数】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" MANNO")
            sql.Append(" FROM ENTTRB")
            sql.Append(" WHERE")
            sql.Append(" ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")

            Dim dtACCEPT2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            UIUtility.ENTRY.ACCEPT_CNT += dtACCEPT2.Rows.Count


            '【1球貸し受付回数】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" KSBKB")
            sql.Append(",COUNT(*) AS CNT")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND EIGKB = '1'")
            sql.Append(" AND ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" GROUP BY KSBKB")

            Dim dtNKBNO As DataTable = iDatabase.ExecuteRead(sql.ToString())

            UIUtility.ENTRY.NKBNO01_CNT = 0
            UIUtility.ENTRY.NKBNO02_CNT = 0
            UIUtility.ENTRY.NKBNO03_CNT = 0
            UIUtility.ENTRY.NKBNO04_CNT = 0
            UIUtility.ENTRY.NKBNO05_CNT = 0
            UIUtility.ENTRY.NKBNO06_CNT = 0
            UIUtility.ENTRY.NKBNO07_CNT = 0
            UIUtility.ENTRY.NKBNO08_CNT = 0
            UIUtility.ENTRY.NKBNO09_CNT = 0
            UIUtility.ENTRY.NKBNO10_CNT = 0
            UIUtility.ENTRY.NKBNO11_CNT = 0
            UIUtility.ENTRY.NKBNO12_CNT = 0
            UIUtility.ENTRY.NKBNO13_CNT = 0
            UIUtility.ENTRY.NKBNO14_CNT = 0

            If dtNKBNO.Rows.Count > 0 Then
                For i As Integer = 0 To dtNKBNO.Rows.Count - 1
                    Select Case dtNKBNO.Rows(i).Item("KSBKB").ToString
                        Case "1" : UIUtility.ENTRY.NKBNO01_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "2" : UIUtility.ENTRY.NKBNO02_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "3" : UIUtility.ENTRY.NKBNO03_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "4" : UIUtility.ENTRY.NKBNO04_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "5" : UIUtility.ENTRY.NKBNO05_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "6" : UIUtility.ENTRY.NKBNO06_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "7" : UIUtility.ENTRY.NKBNO07_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "8" : UIUtility.ENTRY.NKBNO08_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "9" : UIUtility.ENTRY.NKBNO09_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "10" : UIUtility.ENTRY.NKBNO10_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "11" : UIUtility.ENTRY.NKBNO11_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "12" : UIUtility.ENTRY.NKBNO12_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "13" : UIUtility.ENTRY.NKBNO13_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                        Case "14" : UIUtility.ENTRY.NKBNO14_CNT = CType(dtNKBNO.Rows(i).Item("CNT").ToString, Integer)
                    End Select
                Next
            End If

            '【時間貸し受付回数】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" KSBKB")
            sql.Append(",COUNT(*) AS CNT")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND EIGKB = '2'")
            sql.Append(" AND ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" GROUP BY KSBKB")

            Dim dtTIME As DataTable = iDatabase.ExecuteRead(sql.ToString())

            UIUtility.ENTRY.JKNKB01_CNT = 0
            UIUtility.ENTRY.JKNKB02_CNT = 0
            UIUtility.ENTRY.JKNKB03_CNT = 0
            UIUtility.ENTRY.JKNKB04_CNT = 0
            UIUtility.ENTRY.JKNKB05_CNT = 0
            UIUtility.ENTRY.JKNKB06_CNT = 0
            UIUtility.ENTRY.JKNKB07_CNT = 0
            UIUtility.ENTRY.JKNKB08_CNT = 0
            UIUtility.ENTRY.JKNKB09_CNT = 0
            UIUtility.ENTRY.JKNKB10_CNT = 0

            If dtTIME.Rows.Count > 0 Then
                For i As Integer = 0 To dtTIME.Rows.Count - 1
                    Select Case dtTIME.Rows(i).Item("KSBKB").ToString
                        Case "1" : UIUtility.ENTRY.JKNKB01_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "2" : UIUtility.ENTRY.JKNKB02_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "3" : UIUtility.ENTRY.JKNKB03_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "4" : UIUtility.ENTRY.JKNKB04_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "5" : UIUtility.ENTRY.JKNKB05_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "6" : UIUtility.ENTRY.JKNKB06_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "7" : UIUtility.ENTRY.JKNKB07_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "8" : UIUtility.ENTRY.JKNKB08_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "9" : UIUtility.ENTRY.JKNKB09_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                        Case "10" : UIUtility.ENTRY.JKNKB10_CNT = CType(dtTIME.Rows(i).Item("CNT").ToString, Integer)
                    End Select
                Next
            End If
            '打ち放題受付合計
            UIUtility.ENTRY.NKBNO15_CNT = UIUtility.ENTRY.JKNKB01_CNT + UIUtility.ENTRY.JKNKB02_CNT + UIUtility.ENTRY.JKNKB03_CNT + UIUtility.ENTRY.JKNKB04_CNT + UIUtility.ENTRY.JKNKB05_CNT _
                                        + UIUtility.ENTRY.JKNKB06_CNT + UIUtility.ENTRY.JKNKB07_CNT + UIUtility.ENTRY.JKNKB08_CNT + UIUtility.ENTRY.JKNKB09_CNT + UIUtility.ENTRY.JKNKB10_CNT

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ﾍﾞﾝﾀﾞｰ呼び出し情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBndCall() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM BNDMTA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" CALLFLG = '1'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 呼び出し開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PlayCallSound()
        Try
            _strSoundBuffer = New String(Chr(0), 255)
            _strSoundMode = Nothing

            Call mciSendString("status MySound mode", _strSoundBuffer, Len(_strSoundBuffer), 0)
            _strSoundMode = Replace(_strSoundBuffer, Chr(0), "")

            If _strSoundMode = "" OrElse _strSoundMode = "stopped" Then
                Call mciSendString(String.Format("open ""{0}"" alias MySound", UIUtility.FILE_PATH.YOBIDASHI), "", 0, 0)
                Call mciSendString("play MySound", "", 0, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 呼び出し停止処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StopCallSound()
        Try
            _strSoundBuffer = New String(Chr(0), 255)
            _strSoundMode = Nothing

            Call mciSendString("status MySound mode", _strSoundBuffer, Len(_strSoundBuffer), 0)
            _strSoundMode = Replace(_strSoundBuffer, Chr(0), "")

            If _strSoundMode = "" OrElse _strSoundMode = "stopped" Then
                Call mciSendString("stop MySound", "", 0, 0)
                Call mciSendString("close MySound", "", 0, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


End Class
