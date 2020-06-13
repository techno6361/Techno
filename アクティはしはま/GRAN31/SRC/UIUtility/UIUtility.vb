Imports System.Drawing

Public Class UIUtility

    ''' <summary>
    ''' システム情報関連
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SYSTEM
        ''' <summary>
        ''' システムモード 【0】オートセッター【1】ボール貸出機
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SYSTEMMODE As Integer
        ''' <summary>
        ''' システム区分【0】打席管理【0以外】打席と通信しない端末
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SYSTEMKBN As Integer
        ''' <summary>
        ''' 店番
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SHOPNO As String
        ''' <summary>
        ''' 店名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SHOPNM As String
        ''' <summary>
        ''' 管理者パスワード
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ADMNPW As String
        ''' <summary>
        ''' SEパスワード
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SEPW As String
        ''' <summary>
        ''' 球数税率
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared TAMATAX As Integer
        ''' <summary>
        ''' 税率
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared TAX As Integer
        ''' <summary>
        ''' 税区分
        ''' </summary>
        ''' <remarks>【0】内税【1】外税</remarks>
        Public Shared TAXKBN As Integer
        ''' <summary>
        ''' 税端数区分
        ''' </summary>
        ''' <remarks>【0】四捨五入【1】切り捨て【2】切り上げ</remarks>
        Public Shared TAXHASUKBN As Integer
        ''' <summary>
        ''' フロア数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared FLRSU As Integer
        ''' <summary>
        ''' 打席数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SEATSU As Integer
        ''' <summary>
        ''' 打席数1F
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LSTNO1F As Integer
        ''' <summary>
        ''' 打席数2F
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LSTNO2F As Integer
        ''' <summary>
        ''' 打席数3F
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LSTNO3F As Integer
        ''' <summary>
        ''' カレンダー作成最終日付
        ''' </summary>
        ''' <remarks>YYYYMMDD</remarks>
        Public Shared CALLSTDT As String
        ''' <summary>
        ''' パスワード区分
        ''' </summary>
        ''' <remarks>【0】ランダム【1】固定</remarks>
        Public Shared PASSKBN As Integer
        ''' <summary>
        ''' 担当者確認フラグ
        ''' </summary>
        ''' <remarks>【0】しない【1】する</remarks>
        Public Shared TANCHKFLG As Integer
        ''' <summary>
        ''' 会員期限調整日数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared CALLMT As Integer
        ''' <summary>
        ''' 左右兼用打席１
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LRMULTI01 As Integer
        ''' <summary>
        ''' 左右兼用打席２
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LRMULTI02 As Integer
        ''' <summary>
        ''' 左右兼用打席３
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LRMULTI03 As Integer
        ''' <summary>
        ''' 左右兼用打席４
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LRMULTI04 As Integer
        ''' <summary>
        ''' 左右兼用打席５
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared LRMULTI05 As Integer
        ''' <summary>
        ''' 打席情報画面フラグ
        ''' </summary>
        ''' <remarks>【0】なし【1】あり</remarks>
        Public Shared DISPMULTI As Integer
        ''' <summary>
        ''' 月間来場回数クリア月
        ''' </summary>
        ''' <remarks>YYYYMM</remarks>
        Public Shared CLRENTMCNT As String
        ''' <summary>
        ''' 指定打席区分
        ''' </summary>
        ''' <remarks>【0】なし【1】あり</remarks>
        Public Shared SITEIKBN As Integer
        ''' <summary>
        ''' OSシャットダウン
        ''' </summary>
        ''' <remarks>【0】なし【1】あり</remarks>
        Public Shared OSDOWNFLG As Integer
        ''' <summary>
        ''' レシート印刷フラグ
        ''' </summary>
        ''' <remarks>【0】なし【1】あり</remarks>
        Public Shared RECEIPTFLG As Integer
        ''' <summary>
        ''' ﾁｪｯｸｱｳﾄﾎﾟｲﾝﾄ
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared CHKPOINT As Integer
        ''' <summary>
        ''' カード入金限度額
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ZANMAX As Integer
        ''' <summary>
        ''' カード残金有効期限
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared CARDLIMIT As Integer
        ''' <summary>
        ''' 商品引落しﾌﾟﾚﾐｱﾑ精算区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared HINPREMPAYKBN As Integer
        ''' <summary>
        ''' ｶｰﾄﾞ支出ﾌﾟﾚﾐｱﾑ精算区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SHTPREMPAYKBN As Integer
        ''' <summary>
        ''' 入金残高有効期限
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared PREMLIMIT As Integer
        ''' <summary>
        ''' システム更新日時
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared UPDDTM As String
        ''' <summary>
        ''' 本日料金体系区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RKNKB As Integer
        ''' <summary>
        ''' 本日料金体系名称
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RKNNM As String
        ''' <summary>
        ''' DB登録用本日営業日
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared UPDDAY As String
        ''' <summary>
        ''' 総打球数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SUMBALL As Integer
        ''' <summary>
        ''' 現在の時間帯のパスワード
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NOWPASSCD As String
        ''' <summary>
        ''' 現在の時間帯区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NOWTIMEKB As Integer
        ''' <summary>
        ''' 次回単価切り替え時間
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NEXTTIMENM As String
        ''' <summary>
        ''' ICR700リーダライターポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ICR700COM As String
        ''' <summary>
        ''' KUA201リーダライターポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared IA240COM As String
        ''' <summary>
        ''' MCH3000ユニットポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared MCH3000COM As String
        ''' <summary>
        ''' 通信変換機COMポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SEATCOM As String
        ''' <summary>
        ''' AD1COMポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared AD1COM As String
        ''' <summary>
        ''' ｺｲﾝﾒｯｸCOMポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared CMCOM As String
        ''' <summary>
        ''' ｶｰﾄﾞ発券機ポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared STR610COM As String
        ''' <summary>
        ''' レシートプリンタポート名
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SK121COM As String
        ''' <summary>
        ''' カードリードインターバル
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadInterval As Integer
        ''' <summary>
        ''' リーダーライター区分【0】ICR700のみ【1】MCH3000 + ICR700
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RWUnitKB As Integer
        ''' <summary>
        ''' 顧客種別数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared MAXNKBNO As Integer = 10
        ''' <summary>
        ''' 本日日付時間(yyyy/MM/dd 99:99 (曜日))
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared DAYTIME As String
        ''' <summary>
        ''' テロップ（打席モニター）
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared TELOP As String
        ''' <summary>
        ''' カードに印字するコメント
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared COMMENT As String
        ''' <summary>
        ''' フリーカードに印字するコメント
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared FCOMMENT As String
        ''' <summary>
        ''' 音声ファイルフォルダパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SOUND_PAHT As String
        ''' <summary>
        ''' ﾍﾞﾝﾀﾞｰ呼び出しフラグ
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared BNDCALL As Boolean
        ''' <summary>
        ''' 優先単価
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared YUSENTANKA As Integer
        ''' <summary>
        ''' 時間貸し上限打球数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared MAXJDCNT As Integer
        ''' <summary>
        ''' カード発行手数料
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared CARDFEE As Integer
        ''' <summary>
        ''' メニュー区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared MENUKBN As String
        ''' <summary>
        ''' ﾊﾟｽﾜｰﾄﾞ区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared PWKBN As String
        ''' <summary>
        ''' 金額不一致メッセージ出すかどうか
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared FUITCHIMSG As Boolean = False
    End Class

    ''' <summary>
    ''' 来場者・受付人数情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ENTRY
        ''' <summary>
        ''' 総来場者数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ENT_CNT As Integer
        ''' <summary>
        ''' 総受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ACCEPT_CNT As Integer
        ''' <summary>
        ''' 種別１受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO01_CNT As Integer
        ''' <summary>
        ''' 種別２受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO02_CNT As Integer
        ''' <summary>
        ''' 種別３受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO03_CNT As Integer
        ''' <summary>
        ''' 種別４受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO04_CNT As Integer
        ''' <summary>
        ''' 種別５受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO05_CNT As Integer
        ''' <summary>
        ''' 種別６受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO06_CNT As Integer
        ''' <summary>
        ''' 種別７受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO07_CNT As Integer
        ''' <summary>
        ''' 種別８受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO08_CNT As Integer
        ''' <summary>
        ''' 種別９受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO09_CNT As Integer
        ''' <summary>
        ''' 種別１０受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO10_CNT As Integer
        ''' <summary>
        ''' 種別１１受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO11_CNT As Integer
        ''' <summary>
        ''' 種別１２受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO12_CNT As Integer
        ''' <summary>
        ''' 種別１３受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO13_CNT As Integer
        ''' <summary>
        ''' 種別１４受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO14_CNT As Integer
        ''' <summary>
        ''' 種別１５受付人数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared NKBNO15_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分１受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB01_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分２受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB02_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分３受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB03_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分４受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB04_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分５受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB05_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分６受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB06_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分７受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB07_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分８受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB08_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分９受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB09_CNT As Integer
        ''' <summary>
        ''' 打ち放題区分１０受付回数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared JKNKB10_CNT As Integer
        ''' <summary>
        ''' 総球数金額
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared USEKIN_SUM As Integer
    End Class

    ''' <summary>
    ''' ファイルパス情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FILE_PATH
        ''' <summary>
        ''' 帳票用エクセルテンプレートファイルパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared TEMPLATE As String
        ''' <summary>
        ''' 帳票用エクセル保存先ファイルパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared REPORT As String
        ''' <summary>
        ''' スクールメニューファイルパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SCLMENU As String
        ''' <summary>
        ''' 予約受講画面ファイルパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SCL010 As String
        ''' <summary>
        ''' 呼び出し音ファイルパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared YOBIDASHI As String
        ''' <summary>
        ''' 画像ファイルパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared IMAGE As String
    End Class

    ''' <summary>
    ''' DB情報ﾃｰﾌﾞﾙ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE
        ''' <summary>
        ''' 営業情報マスタ(1球貸し・カゴ貸し)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared EIGMT As DataTable
        ''' <summary>
        ''' 打席情報保持
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SEATINFO As DataTable
        ''' <summary>
        ''' 顧客種別情報保持
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared KBMAST As DataTable
    End Class
    ''' <summary>
    ''' 打席情報関連
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SEATSTATE
        ''' <summary>
        ''' 空き
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FREE_SPACE As Integer = 0
        ''' <summary>
        ''' 使用中
        ''' </summary>
        ''' <remarks></remarks>
        Public Const USE_SPACE As Integer = 1
        ''' <summary>
        ''' 呼び出し
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CALL_SPACE As Integer = 2
        ''' <summary>
        ''' 通信エラー
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ERR_SPACE As Integer = 3
        ''' <summary>
        ''' RW異常
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ERR_RW As Integer = 4
        ''' <summary>
        ''' 単価切り替え終了
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PRICE_CHANGE As Integer = 5
        ''' <summary>
        ''' 待機中
        ''' </summary>
        ''' <remarks></remarks>
        Public Const WAIT_SPACE As Integer = 6
        ''' <summary>
        ''' システム起動時初期値
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INIT_DATA As Integer = 99
    End Class
    ''' <summary>
    ''' 色情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class COLOR_INFO
        ''' <summary>
        ''' 料金体系色カラーR
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RKN_R As Integer
        ''' <summary>
        ''' 料金体系色カラーG
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RKN_G As Integer
        ''' <summary>
        ''' 料金体系色カラーB
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RKN_B As Integer
        ''' <summary>
        ''' 空き打席
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared FREE As Color = Color.Gray
        ''' <summary>
        ''' 使用中(1球貸し)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared USE_TAMA As Color = Color.RoyalBlue
        ''' <summary>
        ''' 使用中(時間貸し)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared USE_TIME As Color = Color.PaleVioletRed
        ''' <summary>
        ''' 通信不能
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ERR_COM As Color = Color.Red
        ''' <summary>
        ''' RW異常
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ERR_RW As Color = Color.Blue
        ''' <summary>
        ''' 呼び出し中
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared CALL_COM As Color = Color.Goldenrod
        ''' <summary>
        ''' 単価切り替え完了
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared END_PRICE As Color = Color.YellowGreen
        ''' <summary>
        ''' 予約打席
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared RESERV As Color = Color.Green
        ''' <summary>
        ''' 清掃待ち打席
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared SOJI As Color = Color.SaddleBrown
    End Class

    ''' <summary>
    ''' ビルバリ情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Bv_INFO
        ''' <summary>
        ''' ビルバリ接続状態
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared BvConnection As Boolean
        ''' <summary>
        ''' 1000円札投入枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared InBill1000 As Integer
        ''' <summary>
        ''' 5000円札投入枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared InBill5000 As Integer
        ''' <summary>
        ''' 10000円札投入枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared InBill10000 As Integer
        ''' <summary>
        ''' 2000円札投入枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared InBill2000 As Integer
        ''' <summary>
        ''' 1000円札払出可能枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared OutPossibleBill1000 As Integer
        ''' <summary>
        ''' 1000円札払出枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared OutBill1000 As Integer
        ''' <summary>
        ''' 5000円札払出枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared OutBill5000 As Integer
        ''' <summary>
        ''' 10000円札払出枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared OutBill10000 As Integer
        ''' <summary>
        ''' 2000円札払出枚数
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared OutBill2000 As Integer
    End Class

#Region "パソコンシャットダウンで使う情報関連"
    ''' <summary>
    ''' パソコンシャットダウンで使う情報関連
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ExitWindows
        EWX_LOGOFF = &H0
        EWX_SHUTDOWN = &H1
        EWX_REBOOT = &H2
        EWX_POWEROFF = &H8
        EWX_RESTARTAPPS = &H40
        EWX_FORCE = &H4
        EWX_FORCEIFHUNG = &H10
    End Enum

    <Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> Public Shared Function ExitWindowsEx(ByVal uFlags As ExitWindows, _
    ByVal dwReason As Integer) As Boolean
    End Function
#End Region
End Class
