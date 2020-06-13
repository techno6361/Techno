''' <summary>
''' 共有設定
''' </summary>
''' <remarks></remarks>
Public Class CommonSettings

    Public Const MSGBOX_CLOSE_INTERVAL = 200 ' メッセージの表示時間

    Public Const DEBUG = False ' デバッグモード

    ''' <summary>
    ''' チェック結果画面が閉じる時間
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckResultCloseInterval() As Integer
        Return 4000
    End Function

    ''' <summary>
    ''' チェック結果画面が閉じる時間(入力を促す場合)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckResultCloseIntervalLong() As Integer
        Return 10000
    End Function

    ''' <summary>
    ''' チェック結果画面が閉じる時間(Short)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckResultCloseIntervalShort() As Integer
        Return 8000
    End Function

    ''' <summary>
    ''' 入金結果画面が閉じる時間
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChargeResultCloseInterval() As Integer
        Return 12000
    End Function

    ''' <summary>
    ''' カード確認画面が閉じる時間
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InfoCloseInterval() As Integer
        Return 60000
    End Function

    ''' <summary>
    ''' ミニゲーム画面が終了する時間
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MinigameCloseInteravl() As Integer
        Return 6000
    End Function

    ''' <summary>
    ''' チェック結果表示画面でカード排出を停止し入金操作を続けるモード
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsCheckResultEjectStopMode() As Boolean
        Dim a = System.Configuration.ConfigurationManager.AppSettings("RESULT_EJECTSTOP_MODE") = "1"
        Dim b = ChargeEnabled()
        Return a And b
    End Function

    ''' <summary>
    ''' 入金結果表示画面でカード排出を停止しチェックイン/アウトを続けるモード
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsChargeResultEjectStopMode() As Boolean
        Dim a = System.Configuration.ConfigurationManager.AppSettings("RESULT_EJECTSTOP_MODE") = "1"
        Dim b = CheckInEnabled()
        Dim c = CheckOutEnabled()
        Return a And b And c
    End Function

    ''' <summary>
    ''' レイアウトタイプ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LayoutType() As Integer
        Return CInt(System.Configuration.ConfigurationManager.AppSettings("LAYOUT"))
    End Function

    ''' <summary>
    ''' 1111 機能 【0】なし【1】あり 【1桁目】ﾁｪｯｸｲﾝ【2桁目】ﾁｪｯｸｱｳﾄ【3桁目】入金【4桁目】ｶｰﾄﾞ発券
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AppFunction() As String
        Return System.Configuration.ConfigurationManager.AppSettings("FUNCTION")
    End Function

    ''' <summary>
    ''' チェックイン機能が有効か
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckInEnabled() As Boolean
        Return AppFunction().Substring(0, 1) = "1"
    End Function

    ''' <summary>
    ''' チェックアウト機能が有効か
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckOutEnabled() As Boolean
        Return AppFunction().Substring(1, 1) = "1"
    End Function

    ''' <summary>
    ''' 入金機能が有効か
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChargeEnabled() As Boolean
        Return AppFunction().Substring(2, 1) = "1"
    End Function

    ''' <summary>
    ''' 発券機能が有効か
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HakkenEnabled() As Boolean
        Return AppFunction().Substring(3, 1) = "1"
    End Function

    ''' <summary>
    ''' ﾐﾆｹﾞｰﾑが有効か
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MinigameEnabled() As Boolean
        Return System.Configuration.ConfigurationManager.AppSettings("MINIGAME") = "1"
    End Function

    ''' <summary>
    ''' スロット_確率テーブル
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SlotPerTables() As Integer()
        'Return {67, 89, 112, 137, 162, 187, 212, 353, 494, 699, 904, 1829, 2754, 3877, 4999, 6596, 8192}
        'Return {10, 25, 50, 100, 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000}
        Return {1, 2, 5, 10, 50, 100, 200, 300, 400, 500, 600, 700}
    End Function

    ''' <summary>
    ''' スロット_ポイントテーブル
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SlotPointTables() As Integer()
        Return {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500}
    End Function

    ''' <summary>
    ''' スロット_補正レート
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SlotCorrectionRateTables() As Double()
        Return {0.5, 1.0, 1.5, 2.0, 2.5, 3.0}
    End Function

    ''' <summary>
    ''' スロットの最大確率
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SlotMaxPer() As Integer
        'Return 16383
        'Return 10000
        Return 1000
    End Function

    ''' <summary>
    ''' スロット確率取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SlotPer(ByVal i As Integer) As Integer
        Dim settings = System.Configuration.ConfigurationManager.AppSettings("SLOT_SETTING").Split(","c)
        Dim pers = SlotPerTables()
        Return pers(CInt(settings(i)))
    End Function

    ''' <summary>
    ''' スロット確率補正の有効/無効を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SlotCorrectionEnabled() As Boolean
        Return System.Configuration.ConfigurationManager.AppSettings("SLOT_CORRECTION_ENABLED") = "1"
    End Function

    ''' <summary>
    ''' スロット確率補正取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SlotCorrectionRate(ByVal i As Integer) As Double
        Dim settings = System.Configuration.ConfigurationManager.AppSettings("SLOT_CORRECTION_RATE_SETTING").Split(","c)
        Dim pers = SlotCorrectionRateTables()
        Return pers(CInt(settings(i)))
    End Function

    ''' <summary>
    ''' スロットポイント取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SlotPoint(ByVal i As Integer) As Integer
        Dim settings = System.Configuration.ConfigurationManager.AppSettings("SLOT_SETTING").Split(","c)
        Dim pers = SlotPointTables()
        Return pers(CInt(settings(i)))
    End Function

    ' 役1
    Public Shared Function SlotPer1() As Integer
        Return SlotPer(0)
    End Function
    Public Shared Function SlotPoint1() As Integer
        Return SlotPoint(1)
    End Function
    Public Shared Function SlotCorrectionRate1() As Double
        Return 4.0
    End Function

    ' 役2
    Public Shared Function SlotPer2() As Integer
        Return SlotPer(2)
    End Function
    Public Shared Function SlotPoint2() As Integer
        Return SlotPoint(3)
    End Function
    Public Shared Function SlotCorrectionRate2() As Double
        Return 3.0
    End Function

    ' 役3
    Public Shared Function SlotPer3() As Integer
        Return SlotPer(4)
    End Function
    Public Shared Function SlotPoint3() As Integer
        Return SlotPoint(5)
    End Function
    Public Shared Function SlotCorrectionRate3() As Double
        Return 1.5
    End Function

    ' 役4
    Public Shared Function SlotPer4() As Integer
        Return SlotPer(6)
    End Function
    Public Shared Function SlotPoint4() As Integer
        Return SlotPoint(7)
    End Function
    Public Shared Function SlotCorrectionRate4() As Double
        Return 0.25
    End Function

    ''' <summary>
    ''' カード発券時の顧客区分
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HakkenKSBKB() As String
        Return System.Configuration.ConfigurationManager.AppSettings("HAKKEN_NKBNO")
    End Function

End Class