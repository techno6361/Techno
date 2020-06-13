Imports System.Drawing.Printing
Imports com.epson.pos.driver
Imports System.Object
Imports System.Management

Public Interface TMT90
    'プロパティ
    'Property receiptAPI As StatusAPI
    Property goods As DataTable
    Property insDate As DateTime
    Property gokei As Integer
    Property tax As Integer
    Property deposit As Integer
    Property change As Integer
    Property hostName As String
    Property denno As String

    Property receiptData As DataTable
    Property systba As DataTable
    Property systbb As DataTable
    Property azukariKin As Decimal
    Property turiKin As Decimal
    'Property denno As String
    Property manno As String
    Property premiam As Decimal
    Property flg As Integer
    Property sclflg As Boolean
    Property datkbn As Integer      '取り消し区分【9】取り消し
    Property printKbn As Integer '【0】領収書【1】入金明細
    Property getpremkn As Integer   '取得プレミアム
    Property getpoint As Integer    '取得ﾎﾟｲﾝﾄ
    Property tadashi As String      '領収書　但し

    Property zankingaku As Integer
    Property zanpoint As Integer

    Property ccsname As String
    'メソッド
    Sub OpenDrawer(ByRef isOpen As Boolean, ByRef strErrMsg As String)
    Sub RePrint(ByVal isDraOpen As Boolean, ByRef strErrMsg As String)
    Sub RyPrint(ByVal isDraOpen As Boolean, ByRef strErrMsg As String)
End Interface

Public Class Receipt
    Implements TMT90

    Property dtGoods As DataTable Implements TMT90.goods
    Property insDTTM As DateTime Implements TMT90.insDate
    Property intGokei As Integer Implements TMT90.gokei
    Property intTax As Integer Implements TMT90.tax
    Property intDeposit As Integer Implements TMT90.deposit
    Property intChange As Integer Implements TMT90.change
    Property strHostName As String Implements TMT90.hostName
    Property strDENNO As String Implements TMT90.denno
    Property intDatKbn As Integer Implements TMT90.datkbn
    Property intPrintKbn As Integer Implements TMT90.printKbn
    Property intGetPremKn As Integer Implements TMT90.getpremkn
    Property intGetPoint As Integer Implements TMT90.getpoint
    Property strTadashi As String Implements TMT90.tadashi

    Property intzankingaku As Integer Implements TMT90.zankingaku
    Property intzanpoint As Integer Implements TMT90.zanpoint
    Property strccsname As String Implements TMT90.ccsname

    'Property objAPI As StatusAPI Implements TMT90.receiptAPI
    Property dtReceipt As DataTable Implements TMT90.receiptData
    Property dtSYSTBA As DataTable Implements TMT90.systba
    Property dtSYSTBB As DataTable Implements TMT90.systbb
    Property decAzukari As Decimal Implements TMT90.azukariKin
    Property decTuri As Decimal Implements TMT90.turiKin
    'Property strDenno As String Implements TMT90.denno
    Property strManno As String Implements TMT90.manno
    Property decPremiam As Decimal Implements TMT90.premiam
    Property intFlg As Integer Implements TMT90.flg
    Property isSclFlg As Boolean Implements TMT90.sclflg

#Region "定数"
    Private Const cstPRINTER_NAME = "EPSON TM-T90 Receipt"  ' プリンタ名

    Private objReAPI As New StatusAPI
    Private objRyAPI As New StatusAPI
    Private printStatus As ASB = Nothing
    Private WithEvents pdPrint As PrintDocument = Nothing
    Private isFinish As Boolean = False
    Private isTimeout As Boolean
    Private isCancelErr As Boolean = False
#End Region

#Region "アクセス関数"
    ''' <summary>
    ''' ドロワオープン
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenDrawer(ByRef isOpen As Boolean, ByRef strErrMsg As String) Implements TMT90.OpenDrawer
        Try
            'isOpen = False

            'AddHandler objReAPI.StatusCallback, AddressOf StatusMonitoring

            ' BiOpenMonPrinterの実行
            If objReAPI.OpenMonPrinter(OpenType.TYPE_PRINTER, cstPRINTER_NAME) = ErrorCode.SUCCESS Then
                'If objReAPI.SetStatusBack() = ErrorCode.SUCCESS Then
                If Not objReAPI.OpenDrawer(Drawer.EPS_BI_DRAWER_1, Pulse.EPS_BI_PULSE_100) = ErrorCode.SUCCESS Then
                    strErrMsg = "ドロワのオープンに失敗しました。"
                End If
                'End If
            Else
                'strErrMsg = "監視プリンタのオープンに失敗しました。" 2014/11/07
            End If

            'isOpen = True

        Catch ex As Exception
            'MessageBox.Show("ドロワのオープンに失敗しました。", "Program09", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            strErrMsg = ex.Message
        Finally
            '一旦とめてみる　RemoveHandler objReAPI.StatusCallback, AddressOf StatusMonitoring
            If Not objReAPI.CloseMonPrinter = ErrorCode.SUCCESS Then
                'strErrMsg = "監視プリンタのクローズに失敗しました。"
            End If
            If Not isOpen Then
                objReAPI = Nothing
                objRyAPI = Nothing
            End If
            isOpen = True
        End Try

    End Sub

    ' プリンタのステータス監視
    Public Sub StatusMonitoring(ByVal dwStatus As ASB)

        If (dwStatus And ASB.ASB_PRINT_SUCCESS) = ASB.ASB_PRINT_SUCCESS Then
            printStatus = dwStatus
            isFinish = True

        ElseIf (dwStatus And ASB.ASB_NO_RESPONSE) = ASB.ASB_NO_RESPONSE Or _
                (dwStatus And ASB.ASB_COVER_OPEN) = ASB.ASB_COVER_OPEN Or _
                (dwStatus And ASB.ASB_AUTOCUTTER_ERR) = ASB.ASB_AUTOCUTTER_ERR Or _
                (dwStatus And ASB.ASB_PAPER_END) = ASB.ASB_PAPER_END Then
            printStatus = dwStatus
            isFinish = True
            isCancelErr = True
        End If

    End Sub

    ''' <summary>
    ''' レシート印刷
    ''' </summary>
    ''' <param name="strErrMsg">エラー内容</param>
    ''' <remarks></remarks>
    Public Sub RePrint(ByVal isDraOpen As Boolean, ByRef strErrMsg As String) Implements TMT90.RePrint

        Try
            '一旦とめてみる　AddHandler objReAPI.StatusCallback, AddressOf StatusMonitoring

            ' BiOpenMonPrinterの実行
            If objReAPI.OpenMonPrinter(OpenType.TYPE_PRINTER, cstPRINTER_NAME) = ErrorCode.SUCCESS Then

                isFinish = False
                isCancelErr = False
                isTimeout = False

                ' BiSetStatusBackFunctionの実行
                If objReAPI.SetStatusBack() = ErrorCode.SUCCESS Then

                    pdPrint = New PrintDocument
                    pdPrint.PrinterSettings.PrinterName = cstPRINTER_NAME

                    If pdPrint.PrinterSettings.IsValid Then
                        pdPrint.DocumentName = "レシート印刷"
                        ' 印刷
                        pdPrint.Print()

                        Dim iStartTime = DateTime.Now.Ticks / 100000

                        '一旦とめてみる　' 印刷中のステータス監視
                        'Do
                        '    If isFinish Then
                        '        objReAPI.CancelStatusBack()
                        '    End If
                        '    'If iStartTime + 150000 < DateTime.Now.Ticks / 100000 Then
                        '    '    isFinish = True
                        '    '    isTimeout = True
                        '    'End If
                        'Loop While Not isFinish

                        If isTimeout Then
                            ForceResetPrinter(objReAPI, strErrMsg)
                        Else
                            'strErrMsg = DisplayStatusMessage()

                            '一旦とめてみる　If (printStatus And ASB.ASB_PAPER_END) = ASB.ASB_PAPER_END Then
                            '    CancelJob(strErrMsg)
                            'End If

                            If isCancelErr Then
                                objReAPI.CancelError()
                            Else
                                'If isDraOpen Then
                                '    'System.Threading.Thread.Sleep(1000)
                                '    If Not objReAPI.OpenDrawer(Drawer.EPS_BI_DRAWER_1, Pulse.EPS_BI_PULSE_100) = ErrorCode.SUCCESS Then
                                '        strErrMsg = "ドロワのオープンに失敗しました。"
                                '    End If
                                'End If
                            End If
                        End If
                    Else
                        strErrMsg = "プリンタを利用できません。"
                    End If

                Else
                    strErrMsg = "コールバック関数の登録に失敗しました。"
                End If

                'If Not objReAPI.CloseMonPrinter = ErrorCode.SUCCESS Then
                '    strErrMsg = "監視プリンタのクローズに失敗しました。"
                'End If
            Else
                'strErrMsg = "監視プリンタのオープンに失敗しました。" 2014/11/07
            End If

            Return

        Catch ex As Exception
            'MessageBox.Show("StatusAPIのロードに失敗しました。", "Program09", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            strErrMsg = ex.Message
        Finally
            '一旦とめてみる　RemoveHandler objReAPI.StatusCallback, AddressOf StatusMonitoring
            If Not objReAPI.CloseMonPrinter = ErrorCode.SUCCESS Then
                'strErrMsg = "監視プリンタのクローズに失敗しました。"
            End If
            If Not isDraOpen Then
                objReAPI = Nothing
                objRyAPI = Nothing
            End If
            'System.Threading.Thread.Sleep(3000)
            If pdPrint IsNot Nothing Then
                pdPrint.Dispose()
                pdPrint = Nothing
            End If
        End Try

    End Sub

    ''' <summary>
    ''' 領収書印刷
    ''' </summary>
    ''' <param name="strErrMsg">エラー内容</param>
    ''' <remarks></remarks>
    Public Sub RyPrint(ByVal isDraOpen As Boolean, ByRef strErrMsg As String) Implements TMT90.RyPrint

        Try
            '一旦とめてみる　AddHandler objRyAPI.StatusCallback, AddressOf StatusMonitoring

            ' BiOpenMonPrinterの実行
            If objRyAPI.OpenMonPrinter(OpenType.TYPE_PRINTER, cstPRINTER_NAME) = ErrorCode.SUCCESS Then

                isFinish = False
                isCancelErr = False
                isTimeout = False

                ' BiSetStatusBackFunctionの実行
                If objRyAPI.SetStatusBack() = ErrorCode.SUCCESS Then

                    pdPrint = New PrintDocument
                    pdPrint.PrinterSettings.PrinterName = cstPRINTER_NAME

                    If pdPrint.PrinterSettings.IsValid Then
                        pdPrint.DocumentName = "領収書印刷"
                        ' 印刷
                        pdPrint.Print()

                        Dim iStartTime = DateTime.Now.Ticks / 100000

                        '一旦とめてみる　' 印刷中のステータス監視
                        'Do
                        '    If isFinish Then
                        '        objRyAPI.CancelStatusBack()
                        '    End If
                        '    'If iStartTime + 150000 < DateTime.Now.Ticks / 100000 Then
                        '    '    isFinish = True
                        '    '    isTimeout = True
                        '    'End If
                        'Loop While Not isFinish

                        If isTimeout Then
                            ForceResetPrinter(objRyAPI, strErrMsg)
                        Else
                            'strErrMsg = DisplayStatusMessage()

                            '一旦とめてみる　If (printStatus And ASB.ASB_PAPER_END) = ASB.ASB_PAPER_END Then
                            '    CancelJob(strErrMsg)
                            'End If

                            If isCancelErr Then
                                objRyAPI.CancelError()
                            Else
                                'If isDraOpen Then
                                '    If Not objRyAPI.OpenDrawer(Drawer.EPS_BI_DRAWER_1, Pulse.EPS_BI_PULSE_100) = ErrorCode.SUCCESS Then
                                '        strErrMsg = "ドロワのオープンに失敗しました。"
                                '    End If
                                'End If
                            End If
                        End If
                    Else
                        strErrMsg = "プリンタを利用できません。"
                    End If

                Else
                    strErrMsg = "コールバック関数の登録に失敗しました。"
                End If

                'If Not objRyAPI.CloseMonPrinter = ErrorCode.SUCCESS Then
                '    strErrMsg = "監視プリンタのクローズに失敗しました。"
                'End If
            Else
                'strErrMsg = "監視プリンタのオープンに失敗しました。"
            End If

            Return

        Catch ex As Exception
            'MessageBox.Show("StatusAPIのロードに失敗しました。", "Program09", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            strErrMsg = ex.Message
        Finally
            '一旦とめてみる　RemoveHandler objRyAPI.StatusCallback, AddressOf StatusMonitoring
            If Not objRyAPI.CloseMonPrinter = ErrorCode.SUCCESS Then
                'strErrMsg = "監視プリンタのクローズに失敗しました。"
            End If
            objRyAPI = Nothing
            objReAPI = Nothing
            'System.Threading.Thread.Sleep(3000)
            If pdPrint IsNot Nothing Then
                pdPrint.Dispose()
                pdPrint = Nothing
            End If
        End Try

    End Sub

    ' メッセージ表示
    Private Function DisplayStatusMessage() As String

        DisplayStatusMessage = String.Empty

        If (printStatus And ASB.ASB_PRINT_SUCCESS) = ASB.ASB_PRINT_SUCCESS Then
            'DisplayStatusMessage = "印字が終了しました。"

        ElseIf (printStatus And ASB.ASB_NO_RESPONSE) = ASB.ASB_NO_RESPONSE Then
            DisplayStatusMessage = "プリンタの応答がありません。"

        ElseIf (printStatus And ASB.ASB_COVER_OPEN) = ASB.ASB_COVER_OPEN Then
            DisplayStatusMessage = "カバーが開いています。"

        ElseIf (printStatus And ASB.ASB_AUTOCUTTER_ERR) = ASB.ASB_AUTOCUTTER_ERR Then
            DisplayStatusMessage = "カットエラーが発生しました。"

        ElseIf (printStatus And ASB.ASB_PAPER_END) = ASB.ASB_PAPER_END Then
            DisplayStatusMessage = "ロール紙がなくなりました。"
        End If
    End Function

    Private Sub ForceResetPrinter(ByVal obj As StatusAPI, ByRef strErrMsg As String)
        strErrMsg = ""
        ' 印字完了もエラーも取得できない場合は、BiForceResetPrinterを用いる
        'If MessageBox.Show("異常が発生しました。" & vbLf & "BiForceResetPrinterを実行します。", "Program16", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
        If obj.ForceResetPrinter() = ErrorCode.SUCCESS Then
            'MessageBox.Show("強制リセットに成功しました。", "Program16")
            strErrMsg = "強制リセットに成功しました。"
        Else
            'MessageBox.Show("強制リセットの実行に失敗しました。", "Program16")
            strErrMsg = "強制リセットの実行に失敗しました。"
        End If
        'End If
    End Sub

    Private Sub CancelJob(ByRef strErrMsg As String)
        Dim searchPrintJobs As ManagementObjectSearcher
        Dim printJobCollection As ManagementObjectCollection
        Dim printJob As ManagementObject
        Dim isDeleted As Boolean = False
        searchPrintJobs = New ManagementObjectSearcher("SELECT * FROM Win32_PrintJob")

        Try
            printJobCollection = searchPrintJobs.Get

            For Each printJob In printJobCollection
                'Job's Name would be in the format: [PrinterName],[JobID]
                If System.String.Compare(printJob.Properties("Name").Value.ToString(), cstPRINTER_NAME) Then
                    printJob.Delete()
                    isDeleted = True
                    Exit For
                End If
            Next

            If isDeleted Then
                'MessageBox.Show("印刷ジョブをキャンセルしました。", "Program16", MessageBoxButtons.OK, MessageBoxIcon.Information)
                strErrMsg = "印刷ジョブをキャンセルしました。"
            Else
                'MessageBox.Show("印刷ジョブの削除に失敗しました。", "Program16", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                strErrMsg = "印刷ジョブの削除に失敗しました。"
            End If

        Catch ex As Exception
        Finally
            searchPrintJobs.Dispose()
            searchPrintJobs = Nothing
        End Try
        strErrMsg = ""
    End Sub

    'Private Function pdPrint_String(ByVal sender As System.Object, ByVal FontSize As Integer, ByVal x As Integer, ByVal y As Integer, ByVal Str As String, ByVal e As PrintPageEventArgs) As Integer

    '    Dim lineOffset As Integer
    '    Dim printFont As System.Drawing.Font

    '    e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Point

    '    If FontSize = 85 Then
    '        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8.5, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' JapaneseA フォント置換用
    '    ElseIf FontSize = 115 Then
    '        printFont = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.5, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' JapaneseC フォント置換用
    '    ElseIf FontSize = 170 Then
    '        printFont = New System.Drawing.Font("ＭＳ ゴシック", 17.0, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' JapaneseA フォント置換用
    '    Else
    '        printFont = New System.Drawing.Font("Arial", 1.0, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Control フォント置換用
    '    End If

    '    e.Graphics.DrawString(Str, printFont, System.Drawing.Brushes.Black, x, y)

    '    lineOffset = printFont.GetHeight(e.Graphics)
    '    y += lineOffset

    '    pdPrint_String = y
    'End Function

    Private Sub pdPrint_Print(ByVal sender As System.Object, ByVal e As PrintPageEventArgs) Handles pdPrint.PrintPage

        Select Case intFlg
            Case 7706
                '【アクティはしはま】
                If intPrintKbn.Equals(0) Then
                    '【領収書】

                    Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                    Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                    Dim intY2 As Integer = 0
                    If intDatKbn.Equals(9) Then
                        intY2 = 20
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString("【訂正】", printFont, System.Drawing.Brushes.Black, 40, 0)
                    End If

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("【領   収   書】", printFont, System.Drawing.Brushes.Black, 40, 0 + intY2)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    If String.IsNullOrEmpty(strccsname) Then strccsname = "　　　　　"
                    e.Graphics.DrawString(strccsname & " 様", printFont, System.Drawing.Brushes.Black, 5, 20 + intY2)

                    Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, 10, 40 + intY2)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    Dim intY As Integer = 60 + intY2
                    For i As Integer = 0 To dtGoods.Rows.Count - 1
                        e.Graphics.DrawString(dtGoods.Rows(i).Item("GDSNAME").ToString, printFont, System.Drawing.Brushes.Black, 0, intY)
                        intY += 15
                        e.Graphics.DrawString("×" & dtGoods.Rows(i).Item("GDSCOUNT").ToString & ("  \" & dtGoods.Rows(i).Item("GDSKIN")).ToString.PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 115, intY)
                        intY += 15
                        'e.Graphics.DrawString(dtGoods.Rows(i).Item("GDSNAME").ToString & "×" & dtGoods.Rows(i).Item("GDSCOUNT").ToString, printFont, System.Drawing.Brushes.Black, 0, intY)
                        'intY += 15
                        'e.Graphics.DrawString(("\" & dtGoods.Rows(i).Item("GDSKIN")).ToString.PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 115, intY)
                        'intY += 15
                    Next

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("合　計", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 11, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intGokei.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 125, intY)

                    intY += 15
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("(内消費税等", printFont, System.Drawing.Brushes.Black, 5, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intTax.ToString("#,##0")).PadLeft(7, " "c) & ")", printFont, System.Drawing.Brushes.Black, 135, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("お預り", printFont, System.Drawing.Brushes.Black, 10, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intDeposit.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 125, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("お　釣", printFont, System.Drawing.Brushes.Black, 10, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intChange.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 125, intY)

                    Dim strCPay As String = "【現金】"
                    Select Case dtGoods.Rows(0).Item("CPAYKBN").ToString
                        Case "1" : strCPay = "【カード】"
                        Case "2" : strCPay = "【商品券】"
                        Case "3" : strCPay = "【銀行振込】"
                        Case "4" : strCPay = "【打カード】"
                        Case "98" : strCPay = "【打カード】"
                        Case "99" : strCPay = String.Empty
                    End Select
                    intY += 20
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(strCPay.PadLeft(6, " "c), printFont, System.Drawing.Brushes.Black, 115, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    '取得プレミアム
                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("取得ﾌﾟﾚﾐｱﾑ", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intGetPremKn.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 130, intY)
                    '取得ﾎﾟｲﾝﾄ
                    If intGetPoint > 0 Then
                        intY += 10
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString("取得ﾎﾟｲﾝﾄ", printFont, System.Drawing.Brushes.Black, 0, intY)
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString(intGetPoint.ToString("#,##0").PadLeft(7, " "c) & "P", printFont, System.Drawing.Brushes.Black, 130, intY)
                    End If

                    If strCPay.Equals("【打カード】") Then
                        intY += 10
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                        'カード残高
                        intY += 10
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString(" ｶｰﾄﾞ残高", printFont, System.Drawing.Brushes.Black, 0, intY)
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString(intzankingaku.ToString("#,##0").PadLeft(7, " "c) & "円", printFont, System.Drawing.Brushes.Black, 120, intY)
                        If intzanpoint > 0 Then
                            'ﾎﾟｲﾝﾄ残高
                            intY += 10
                            printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                            e.Graphics.DrawString("ﾎﾟｲﾝﾄ残高", printFont, System.Drawing.Brushes.Black, 0, intY)
                            printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                            e.Graphics.DrawString(intzanpoint.ToString("#,##0").PadLeft(7, " "c) & "P", printFont, System.Drawing.Brushes.Black, 125, intY)
                        End If
                    End If

                    intY += 20
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　　　　　伝№" & strDENNO, printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　アクティはしはま", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　電話：0898-41-9006", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　            顧客番号" & strManno.PadLeft(8, "0"c), printFont, System.Drawing.Brushes.Black, 0, intY)
                ElseIf intPrintKbn.Equals(1) Then
                    '【ご入金明細】

                    Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                    Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                    e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Point
                    lineOffset = CDec(printFont.GetHeight(e.Graphics) - 1)

                    Dim intY2 As Integer = 0
                    If intDatKbn.Equals(9) Then
                        intY2 = 20
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString("【訂正】", printFont, System.Drawing.Brushes.Black, 40, 0)
                    End If

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("【ご入金明細】", printFont, System.Drawing.Brushes.Black, 40, 0 + intY2)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(strccsname & " 様", printFont, System.Drawing.Brushes.Black, 5, 15 + intY2)

                    Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, 10, 30 + intY2)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    Dim intY As Integer = 45  + intY2
                    For i As Integer = 0 To dtGoods.Rows.Count - 1
                        e.Graphics.DrawString(dtGoods.Rows(i).Item("GDSNAME").ToString, printFont, System.Drawing.Brushes.Black, 0, intY)
                        e.Graphics.DrawString(("\" & dtGoods.Rows(i).Item("GDSKIN")).ToString.PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)
                        intY += 10
                    Next

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 11, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("合　計", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 11, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intGokei.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("お預り", printFont, System.Drawing.Brushes.Black, 10, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intDeposit.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("お　釣", printFont, System.Drawing.Brushes.Black, 10, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intChange.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)

                    Dim strCPay As String = "【現金】"
                    Select Case dtGoods.Rows(0).Item("CPAYKBN").ToString
                        Case "1" : strCPay = "【カード】"
                        Case "2" : strCPay = "【商品券】"
                        Case "3" : strCPay = "【銀行振込】"
                        Case "99" : strCPay = String.Empty
                    End Select
                    intY += 20
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(strCPay.PadLeft(6, " "c), printFont, System.Drawing.Brushes.Black, 90, intY)

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    '取得プレミアム
                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("取得ﾌﾟﾚﾐｱﾑ", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString((intGetPremKn.ToString("#,##0")).PadLeft(7, " "c) & "円", printFont, System.Drawing.Brushes.Black, 105, intY)
                    If intGetPoint > 0 Then
                        '取得ﾎﾟｲﾝﾄ
                        intY += 12
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString("取得ﾎﾟｲﾝﾄ", printFont, System.Drawing.Brushes.Black, 0, intY)
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString(intGetPoint.ToString("#,##0").PadLeft(7, " "c) & "P", printFont, System.Drawing.Brushes.Black, 105, intY)
                    End If

                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    'カード残高
                    intY += 12
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(" ｶｰﾄﾞ残高", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(intzankingaku.ToString("#,##0").PadLeft(7, " "c) & "円", printFont, System.Drawing.Brushes.Black, 105, intY)
                    If intzanpoint > 0 Then
                        'ﾎﾟｲﾝﾄ残高
                        intY += 12
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString("ﾎﾟｲﾝﾄ残高", printFont, System.Drawing.Brushes.Black, 0, intY)
                        printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                        e.Graphics.DrawString(intzanpoint.ToString("#,##0").PadLeft(7, " "c) & "P", printFont, System.Drawing.Brushes.Black, 105, intY)
                    End If

                    intY += 15
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　　　　　伝№" & strDENNO, printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　アクティはしはま", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　電話：0898-41-9006", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　            顧客番号" & strManno.PadLeft(8, "0"c), printFont, System.Drawing.Brushes.Black, 0, intY)
                ElseIf intPrintKbn.Equals(2) Then
                    '【ポイント還元明細】

                    Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                    Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("【ポイント還元明細】", printFont, System.Drawing.Brushes.Black, 33, 0)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(strccsname & " 様", printFont, System.Drawing.Brushes.Black, 5, 20)

                    Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, 10, 40)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    Dim intY As Integer = 60
                    For i As Integer = 0 To dtGoods.Rows.Count - 1
                        e.Graphics.DrawString(dtGoods.Rows(i).Item("GDSNAME").ToString, printFont, System.Drawing.Brushes.Black, 0, intY)
                        e.Graphics.DrawString(("\" & dtGoods.Rows(i).Item("GDSKIN")).ToString.PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)
                        intY += 10
                    Next

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    '取得プレミアム
                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("取得ﾌﾟﾚﾐｱﾑ", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(("\" & intGetPremKn.ToString("#,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)
                    '取得ﾎﾟｲﾝﾄ
                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("取得ﾎﾟｲﾝﾄ", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(intGetPoint.ToString("#,##0").PadLeft(7, " "c) & "P", printFont, System.Drawing.Brushes.Black, 105, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    'カード残高
                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(" ｶｰﾄﾞ残高", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(intzankingaku.ToString("#,##0").PadLeft(7, " "c) & "円", printFont, System.Drawing.Brushes.Black, 105, intY)
                    'ﾎﾟｲﾝﾄ残高
                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("ﾎﾟｲﾝﾄ残高", printFont, System.Drawing.Brushes.Black, 0, intY)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(intzanpoint.ToString("#,##0").PadLeft(7, " "c) & "P", printFont, System.Drawing.Brushes.Black, 105, intY)

                    intY += 15
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　　　　　伝№" & strDENNO, printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　アクティはしはま", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　電話：0898-41-9006", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　            顧客番号" & strManno.PadLeft(8, "0"c), printFont, System.Drawing.Brushes.Black, 0, intY)
                ElseIf intPrintKbn.Equals(3) Then
                    '【残高移行明細】

                    Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                    Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                    e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Point
                    lineOffset = CDec(printFont.GetHeight(e.Graphics) - 1)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("【残高移行明細】", printFont, System.Drawing.Brushes.Black, 33, 0)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(strccsname & " 様", printFont, System.Drawing.Brushes.Black, 5, 15)

                    Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, 10, 30)

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    Dim intY As Integer = 45
                    For i As Integer = 0 To dtGoods.Rows.Count - 1
                        If i.Equals(3) Or i.Equals(6) Or i.Equals(9) Then
                            printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                            e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)
                            intY += 10
                        End If
                        e.Graphics.DrawString(dtGoods.Rows(i).Item("GDSNAME").ToString, printFont, System.Drawing.Brushes.Black, 0, intY)
                        If i.Equals(2) Or i.Equals(5) Or i.Equals(8) Or i.Equals(11) Then
                            e.Graphics.DrawString((dtGoods.Rows(i).Item("GDSKIN")).ToString.PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)
                        Else
                            e.Graphics.DrawString(("\" & dtGoods.Rows(i).Item("GDSKIN")).ToString.PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, 105, intY)
                        End If

                        intY += 10
                    Next

                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("-------------------------", printFont, System.Drawing.Brushes.Black, 10, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　　アクティはしはま", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　　　　　　電話：0898-41-9006", printFont, System.Drawing.Brushes.Black, 0, intY)

                    intY += 10
                    printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    e.Graphics.DrawString("　　　　            顧客番号" & strManno.PadLeft(8, "0"c), printFont, System.Drawing.Brushes.Black, 0, intY)
                ElseIf intPrintKbn.Equals(4) Then
                    '【領収書】ﾌﾘｰ

                    '未使用かな

                    'Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                    'Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                    'e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Point
                    'lineOffset = CDec(printFont.GetHeight(e.Graphics) - 1)

                    'printFont = New System.Drawing.Font("HG行書体", 18, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("TOMATO GOLF", printFont, System.Drawing.Brushes.Black, 20, 5)
                    'printFont = New System.Drawing.Font("HG行書体", 18, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("CENTER", printFont, System.Drawing.Brushes.Black, 45, 23)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("和歌山県紀の川市下井阪345", printFont, System.Drawing.Brushes.Black, 10, 45)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("電話：0736-77-3201", printFont, System.Drawing.Brushes.Black, 10, 60)

                    'Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, 10, 75)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 17, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("領収書", printFont, System.Drawing.Brushes.Black, 45, 95)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("_________________________", printFont, System.Drawing.Brushes.Black, 10, 110)


                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 13, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("㈱テクノ・アシスト", printFont, System.Drawing.Brushes.Black, 10, 135)
                    ''e.Graphics.DrawString("ああああああああああ", printFont, System.Drawing.Brushes.Black, 10, 135)
                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("　　　　　　　　　様", printFont, System.Drawing.Brushes.Black, 10, 155)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("_________________________", printFont, System.Drawing.Brushes.Black, 10, 160)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("金額", printFont, System.Drawing.Brushes.Black, 10, 186)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 20, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("\" & intGokei.ToString("#,##0") & "-", printFont, System.Drawing.Brushes.Black, 40, 180)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("_________________________", printFont, System.Drawing.Brushes.Black, 10, 190)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("但 " & strTadashi, printFont, System.Drawing.Brushes.Black, 10, 210)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("_________________________", printFont, System.Drawing.Brushes.Black, 10, 215)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("上記正に領収致しました。", printFont, System.Drawing.Brushes.Black, 10, 230)

                    'Dim tempImg2 As System.Drawing.Image = System.Drawing.Image.FromFile("D:\GRAN31\IMAGE\収入印紙.png")
                    'e.Graphics.DrawImage(tempImg2, 40, 260, tempImg2.Width - 30, tempImg2.Height - 30)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 13, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("収　入", printFont, System.Drawing.Brushes.Black, 50, 270)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 13, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("印　紙", printFont, System.Drawing.Brushes.Black, 50, 290)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("_________________________", printFont, System.Drawing.Brushes.Black, 10, 330)


                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("№" & strDENNO, printFont, System.Drawing.Brushes.Black, 0, 350)

                    'printFont = New System.Drawing.Font("ＭＳ ゴシック", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                    'e.Graphics.DrawString("レジ " & strHostName, printFont, System.Drawing.Brushes.Black, 0, 360)



                End If



            Case 1
                Dim strCPay As String = "　現金"
                Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("** " & Me.MidB(Me.dtSYSTBA.Rows(0).Item("USRNMA").ToString().Trim().PadRight(20, " "c), 1, 20).Trim & " **", printFont, System.Drawing.Brushes.Black, x, y)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                y += (lineOffset * 2)
                e.Graphics.DrawString(Me.MidB(Me.dtSYSTBA.Rows(0).Item("USRADA").ToString().Trim().PadRight(32, " "c), 1, 32).Trim(), printFont, System.Drawing.Brushes.Black, x, y)
                y += lineOffset
                e.Graphics.DrawString("              TEL  " & Me.dtSYSTBA.Rows(0).Item("USRTL").ToString().Trim(), printFont, System.Drawing.Brushes.Black, x, y)
                y += lineOffset
                e.Graphics.DrawString(Me.MidB(Me.dtSYSTBB.Rows(0).Item("HTTP").ToString().Trim().PadRight(32, " "c), 1, 32).Trim(), printFont, System.Drawing.Brushes.Black, x, y)

                y += (lineOffset * 3)
                Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, x, y - 2)

                y += (lineOffset * 2)
                Dim decTKin As Decimal = 0
                Dim decZekin As Decimal = 0
                Dim decPoint As Decimal = 0
                For i As Integer = 0 To Me.dtReceipt.Rows.Count - 1
                    If i <> 0 Then
                        y += (lineOffset * 1.5)
                    End If
                    e.Graphics.DrawString(Me.MidB(Me.dtReceipt.Rows(i).Item("HINNMA").ToString().Trim().PadRight(32, " "c), 1, 32).Trim(), printFont, System.Drawing.Brushes.Black, x, y)
                    y += (lineOffset * 1.2)
                    Dim decKin As Decimal = 0
                    If Me.dtReceipt.Rows(i).Item("HINZEIKB").ToString() = "9" Then
                        decKin = Me.ChgNum(Me.dtReceipt.Rows(i).Item("URIATK")) * Me.ChgNum(Me.dtReceipt.Rows(i).Item("SU"))
                    Else
                        decKin = Me.ChgNum(Me.dtReceipt.Rows(i).Item("URIBTK")) * Me.ChgNum(Me.dtReceipt.Rows(i).Item("SU"))
                    End If
                    e.Graphics.DrawString("         " & Me.ChgNum(Me.dtReceipt.Rows(i).Item("SU")).ToString("##0").PadLeft(3, " "c) & "点" & "     " & ("\" & decKin.ToString("##,##0")).PadLeft(7, " "c), printFont, System.Drawing.Brushes.Black, x, y)

                    decTKin += decKin
                    decZekin += Me.ChgNum(Me.dtReceipt.Rows(i).Item("ZEITK"))

                    '*** Sta Mnt 2014/07/14 Kitahara
                    If Me.dtReceipt.Rows(i).Item("CPAYKBN").ToString().Equals("1") Then
                        strCPay = "カード"
                    ElseIf Me.dtReceipt.Rows(i).Item("CPAYKBN").ToString().Equals("2") Then
                        strCPay = "商品券"
                    ElseIf Me.dtReceipt.Rows(i).Item("CPAYKBN").ToString().Equals("3") Then
                        strCPay = "銀行振込"
                    End If
                    'If Me.dtReceipt.Rows(i).Item("CPAYKBN").ToString().Equals("1") Then
                    '    strCPay = "カード"
                    'End If
                    '*** End Mnt 2014/07/14 Kiatahara

                    If Not strManno.Trim.Equals(String.Empty) Then
                        decPoint += Me.ChgNum(Me.dtReceipt.Rows(i).Item("POINT"))
                    End If
                Next

                y += (lineOffset * 3)
                e.Graphics.DrawString("小計(税込)           " & ("\" & decTKin.ToString("###,##0")).PadLeft(8, " "c), printFont, System.Drawing.Brushes.Black, x, y)
                y += lineOffset
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
                lineOffset = CDec(printFont.GetHeight(e.Graphics) - 4)
                y += (lineOffset * 2)
                e.Graphics.DrawString("合計            " & ("\" & decTKin.ToString("###,##0")).PadLeft(8, " "c), printFont, System.Drawing.Brushes.Black, x - 1, y)

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
                lineOffset = CDec(printFont.GetHeight(e.Graphics))
                y += lineOffset
                e.Graphics.DrawString(" (うち消費税          " & ("\" & decZekin.ToString("##,##0")).PadLeft(6, " "c) & ")", printFont, System.Drawing.Brushes.Black, x, y)

                y += (lineOffset * 2)
                e.Graphics.DrawString("お預り              " & ("\" & decAzukari.ToString("###,##0")).PadLeft(8, " "c), printFont, System.Drawing.Brushes.Black, x, y)

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
                y += lineOffset
                e.Graphics.DrawString("お釣り              " & ("\" & decTuri.ToString("###,##0")).PadLeft(8, " "c), printFont, System.Drawing.Brushes.Black, x, y)

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
                y += (lineOffset * 2)
                If Not strManno.Trim.Equals(String.Empty) Then
                    y += (lineOffset * 1)
                    'e.Graphics.DrawString("【" & MidB(strHostName, 1, 4) & "】       伝№:" & strDenno, printFont, System.Drawing.Brushes.Black, x, y - 2)
                    If isSclFlg Then
                        e.Graphics.DrawString("ｽ№:" & strManno.PadRight(8, " "c) & "    POINT:" & decPoint.ToString("##0").PadLeft(3, " "c), printFont, System.Drawing.Brushes.Black, x, y - 2)
                    Else
                        e.Graphics.DrawString("ｺ№:" & strManno.PadRight(8, " "c) & "    POINT:" & decPoint.ToString("##0").PadLeft(3, " "c), printFont, System.Drawing.Brushes.Black, x, y - 2)
                    End If
                    If decPremiam <> 0 Then
                        y += lineOffset
                        e.Graphics.DrawString("            " & "     PREM: \" & decPremiam.ToString("#,##0").PadLeft(5, " "c), printFont, System.Drawing.Brushes.Black, x, y - 2)
                    End If
                    y += (lineOffset * 2)
                End If
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
                e.Graphics.DrawString("【" & strHostName.Trim().PadRight(8, " "c).Substring(0, 8) & "】 №:" & strDENNO & " " & strCPay, printFont, System.Drawing.Brushes.Black, x, y - 2)
                y += (lineOffset * 2)
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)

                'printFont = New System.Drawing.Font("Control", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
                'y += (lineOffset * 2)
                'e.Graphics.DrawString("O", printFont, System.Drawing.Brushes.Black, x, y)

                e.HasMorePages = False

            Case 2
                Dim strCPay As String = "　現金"
                '*** Sta Mnt 2014/07/17 Kitahara
                If Me.dtReceipt.Rows(0).Item("CPAYKBN").ToString().Equals("1") Then
                    strCPay = "カード"
                ElseIf Me.dtReceipt.Rows(0).Item("CPAYKBN").ToString().Equals("2") Then
                    strCPay = "商品券"
                ElseIf Me.dtReceipt.Rows(0).Item("CPAYKBN").ToString().Equals("3") Then
                    strCPay = "銀行振込"
                End If
                '    If Me.dtReceipt.Rows(0).Item("CPAYKBN").ToString().Equals("1") Then
                '        strCPay = "カード"
                'End If
                '*** End Mnt 2014/07/17 Kitahara 

                Dim x As Decimal = 8, y As Decimal = 15, lineOffset As Decimal
                Dim printFont As New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用

                e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Point
                lineOffset = CDec(printFont.GetHeight(e.Graphics) - 1)

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("** " & Me.MidB(Me.dtSYSTBA.Rows(0).Item("USRNMA").ToString().Trim().PadRight(20, " "c), 1, 20).Trim & " **", printFont, System.Drawing.Brushes.Black, x, y)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                y += (lineOffset * 2)
                e.Graphics.DrawString(Me.MidB(Me.dtSYSTBA.Rows(0).Item("USRADA").ToString().Trim().PadRight(32, " "c), 1, 32).Trim(), printFont, System.Drawing.Brushes.Black, x, y)
                y += lineOffset
                e.Graphics.DrawString("              TEL  " & Me.dtSYSTBA.Rows(0).Item("USRTL").ToString().Trim(), printFont, System.Drawing.Brushes.Black, x, y)
                y += lineOffset
                e.Graphics.DrawString(Me.MidB(Me.dtSYSTBB.Rows(0).Item("HTTP").ToString().Trim().PadRight(32, " "c), 1, 32).Trim(), printFont, System.Drawing.Brushes.Black, x, y)

                y += (lineOffset * 3)
                Dim strWeekName As String = WeekdayName(Weekday(CDate(insDTTM))).Replace("曜日", String.Empty)
                e.Graphics.DrawString(insDTTM.ToString("yyyy年MM月dd日(") & strWeekName & ") " & insDTTM.ToString("HH:mm"), printFont, System.Drawing.Brushes.Black, x, y - 2)

                y += (lineOffset * 3)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 17, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("    領収書", printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 1.5)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 2)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                y += (lineOffset * 2)
                e.Graphics.DrawString("                  様", printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 1.5)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 3)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("合計       " & ("\" & decAzukari.ToString("###,##0")).PadLeft(8, " "c), printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 1.5)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)

                y += (lineOffset * 3)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("但し　　　　　　    として", printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 1.5)
                e.Graphics.DrawString("上記正に領収致しました。", printFont, System.Drawing.Brushes.Black, x, y)

                y += (lineOffset * 4)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point) ' Japanese フォント置換用
                e.Graphics.DrawString("          印           ", printFont, System.Drawing.Brushes.Black, x, y)
                y += lineOffset

                printFont = New System.Drawing.Font("ＭＳ ゴシック", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
                y += (lineOffset * 3)
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)
                y += (lineOffset * 2)
                printFont = New System.Drawing.Font("ＭＳ ゴシック", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
                e.Graphics.DrawString("【" & strHostName.Trim().PadRight(8, " "c).Substring(0, 8) & "】 №:" & strDENNO & " " & strCPay, printFont, System.Drawing.Brushes.Black, x, y - 2)
                y += (lineOffset * 2)
                e.Graphics.DrawString("_______________________________", printFont, System.Drawing.Brushes.Black, x, y)

                e.HasMorePages = False

        End Select

    End Sub

#Region "MidB関数"
    ''' -----------------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iStart">
    '''     取り出しを開始する位置。</param>
    ''' <param name="iByteSize">
    '''     取り出すバイト数。</param>
    ''' <returns>
    '''     指定されたバイト位置から指定されたバイト数分の文字列。</returns>
    ''' -----------------------------------------------------------------------------------------
    Private Function MidB(ByVal stTarget As String, ByVal iStart As Integer, ByVal iByteSize As Integer) As String
        Try
            Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

            Dim st1 As String = hEncoding.GetString(btBytes, iStart - 1, iByteSize)
            Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - iStart + 1

            If Asc(Strings.Right(st1, 1)) = 0 Then
                Return st1.Substring(0, st1.Length - 1)
            ElseIf iByteSize = ResultLength - 1 Then
                Return st1.Substring(0, st1.Length - 1)
            Else
                'その他の場合
                Return st1
            End If

            Return hEncoding.GetString(btBytes, iStart - 1, iByteSize)

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
#End Region

    Private Function ChgNum(ByVal obj As Object) As Decimal
        Try
            If obj Is Nothing Then Return 0
            If Not IsNumeric(obj) Then Return 0
            Return CType(obj, Decimal)
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region


End Class
