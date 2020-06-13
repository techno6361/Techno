Imports TECHNO.DataBase
Imports Microsoft.Office.Interop

Public Class frmPRINT03


#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "売上管理帳票"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "売上管理帳票"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
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
    Private Sub frmPRINT03_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            Me.cmbTerm.SelectedIndex = 0

            Me.btnSearch.PerformClick()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' 期間コンボボックス
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbTerm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTerm.SelectedIndexChanged
        Dim intYear As Integer = Now.Date.Year
        Dim intMonth As Integer = Now.Date.Month
        Dim intDay As Integer = Now.Date.Day
        Try
            Me.dtpStaSEATDT.Enabled = False
            Me.dtpEndSEATDT.Enabled = False

            Select Case Me.cmbTerm.SelectedIndex
                Case 0      '任意入力
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                    Me.dtpStaSEATDT.Enabled = True
                    Me.dtpEndSEATDT.Enabled = True
                    Me.dtpStaSEATDT.Focus()
                    Me.dtpStaSEATDT.Select()
                Case 1      '今月
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                Case 2      '前月
                    intMonth -= 1
                    If intMonth.Equals(0) Then
                        intMonth = 12
                        intYear -= 1
                    End If
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & DateTime.DaysInMonth(intYear, intMonth).ToString.PadLeft(2, "0"c)
                Case 3      '今年
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/01/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                Case 4      '前年
                    intYear -= 1
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/01/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/12/31"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Try
            Me.Cursor = Cursors.WaitCursor

            '印刷前に再検索
            Me.btnSearch.PerformClick()

            Dim strReportName As String = "売上管理帳票"
            Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                             & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                             & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"
            'エクセル処理関連
            Dim app As Excel.Application
            Dim book As Excel.Workbook
            Dim sheet As Excel.Worksheet
            Dim border As Excel.Border = Nothing
            Dim xlrange As Excel.Range
            Dim xlpasterange As Excel.Range
            Dim break As Excel.HPageBreak

            app = CType(CreateObject("Excel.Application"), Excel.Application)
            app.Visible = False
            book = app.Workbooks.Open(UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName)

            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            '出力日
            sheet.Cells(1, 13) = Now.ToString("yyyy/MM/dd HH:mm")
            'sheet.Cells(1, 14) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString
            '営業日
            sheet.Cells(4, 3) = Me.dtpStaSEATDT.Text & "～" & Me.dtpEndSEATDT.Text

            '■客単価(打席利用)
            sheet.Cells(4, 14) = Me.lblAvePrice.Text

            '■入場者数

            '種別名
            sheet.Cells(7, 3) = Me.lblEntNKBNM01.Text
            sheet.Cells(7, 4) = Me.lblEntNKBNM02.Text
            sheet.Cells(7, 5) = Me.lblEntNKBNM03.Text
            sheet.Cells(7, 6) = Me.lblEntNKBNM04.Text
            sheet.Cells(7, 7) = Me.lblEntNKBNM05.Text
            sheet.Cells(7, 8) = Me.lblEntNKBNM06.Text
            sheet.Cells(7, 9) = Me.lblEntNKBNM07.Text
            sheet.Cells(7, 10) = Me.lblEntNKBNM08.Text
            sheet.Cells(7, 11) = Me.lblEntNKBNM09.Text
            sheet.Cells(7, 12) = Me.lblEntNKBNM10.Text
            '1球貸し
            sheet.Cells(8, 3) = Me.lblEntTamaNKB01.Text
            sheet.Cells(8, 4) = Me.lblEntTamaNKB02.Text
            sheet.Cells(8, 5) = Me.lblEntTamaNKB03.Text
            sheet.Cells(8, 6) = Me.lblEntTamaNKB04.Text
            sheet.Cells(8, 7) = Me.lblEntTamaNKB05.Text
            sheet.Cells(8, 8) = Me.lblEntTamaNKB06.Text
            sheet.Cells(8, 9) = Me.lblEntTamaNKB07.Text
            sheet.Cells(8, 10) = Me.lblEntTamaNKB08.Text
            sheet.Cells(8, 11) = Me.lblEntTamaNKB09.Text
            sheet.Cells(8, 12) = Me.lblEntTamaNKB10.Text
            sheet.Cells(8, 14) = Me.lblEntTamaGokei.Text
            '打ち放題
            sheet.Cells(9, 3) = Me.lblEntTimeNKB01.Text
            sheet.Cells(9, 4) = Me.lblEntTimeNKB02.Text
            sheet.Cells(9, 5) = Me.lblEntTimeNKB03.Text
            sheet.Cells(9, 6) = Me.lblEntTimeNKB04.Text
            sheet.Cells(9, 7) = Me.lblEntTimeNKB05.Text
            sheet.Cells(9, 8) = Me.lblEntTimeNKB06.Text
            sheet.Cells(9, 9) = Me.lblEntTimeNKB07.Text
            sheet.Cells(9, 10) = Me.lblEntTimeNKB08.Text
            sheet.Cells(9, 11) = Me.lblEntTimeNKB09.Text
            sheet.Cells(9, 12) = Me.lblEntTimeNKB10.Text
            sheet.Cells(9, 14) = Me.lblEntTimeGokei.Text
            '合計
            sheet.Cells(11, 3) = Me.lblEntNKB01Gokei.Text
            sheet.Cells(11, 4) = Me.lblEntNKB02Gokei.Text
            sheet.Cells(11, 5) = Me.lblEntNKB03Gokei.Text
            sheet.Cells(11, 6) = Me.lblEntNKB04Gokei.Text
            sheet.Cells(11, 7) = Me.lblEntNKB05Gokei.Text
            sheet.Cells(11, 8) = Me.lblEntNKB06Gokei.Text
            sheet.Cells(11, 9) = Me.lblEntNKB07Gokei.Text
            sheet.Cells(11, 10) = Me.lblEntNKB08Gokei.Text
            sheet.Cells(11, 11) = Me.lblEntNKB09Gokei.Text
            sheet.Cells(11, 12) = Me.lblEntNKB10Gokei.Text
            sheet.Cells(11, 14) = Me.lblEntGokei.Text

            '■利用ボール数

            '種別名
            sheet.Cells(14, 3) = Me.lblBallNKBNM01.Text
            sheet.Cells(14, 4) = Me.lblBallNKBNM02.Text
            sheet.Cells(14, 5) = Me.lblBallNKBNM03.Text
            sheet.Cells(14, 6) = Me.lblBallNKBNM04.Text
            sheet.Cells(14, 7) = Me.lblBallNKBNM05.Text
            sheet.Cells(14, 8) = Me.lblBallNKBNM06.Text
            sheet.Cells(14, 9) = Me.lblBallNKBNM07.Text
            sheet.Cells(14, 10) = Me.lblBallNKBNM08.Text
            sheet.Cells(14, 11) = Me.lblBallNKBNM09.Text
            sheet.Cells(14, 12) = Me.lblBallNKBNM10.Text
            '1球貸し
            sheet.Cells(15, 3) = Me.lblBallTamaNKB01.Text
            sheet.Cells(15, 4) = Me.lblBallTamaNKB02.Text
            sheet.Cells(15, 5) = Me.lblBallTamaNKB03.Text
            sheet.Cells(15, 6) = Me.lblBallTamaNKB04.Text
            sheet.Cells(15, 7) = Me.lblBallTamaNKB05.Text
            sheet.Cells(15, 8) = Me.lblBallTamaNKB06.Text
            sheet.Cells(15, 9) = Me.lblBallTamaNKB07.Text
            sheet.Cells(15, 10) = Me.lblBallTamaNKB08.Text
            sheet.Cells(15, 11) = Me.lblBallTamaNKB09.Text
            sheet.Cells(15, 12) = Me.lblBallTamaNKB10.Text
            sheet.Cells(15, 14) = Me.lblBallTamaGokei.Text
            '打ち放題
            sheet.Cells(17, 3) = Me.lblBallTime.Text
            'メンテナンス
            sheet.Cells(17, 6) = Me.lblBallMente.Text

            '■ｶｰﾄﾞ発券
            sheet.Cells(20, 3) = Me.lblCardFHakken.Text
            sheet.Cells(20, 4) = Me.lblHakkenFKin.Text
            sheet.Cells(21, 3) = Me.lblCardHakken.Text
            sheet.Cells(21, 4) = Me.lblHakkenKin.Text
            sheet.Cells(22, 3) = Me.lblCardHakken2.Text
            sheet.Cells(22, 4) = Me.lblHakkenKin2.Text

            '■ｻｰﾋﾞｽ入金額
            sheet.Cells(22, 14) = Me.lblSvcKin.Text

            '■入金

            '種別名
            sheet.Cells(24, 3) = Me.lblNyukinNKBNM01.Text
            sheet.Cells(24, 4) = Me.lblNyukinNKBNM02.Text
            sheet.Cells(24, 5) = Me.lblNyukinNKBNM03.Text
            sheet.Cells(24, 6) = Me.lblNyukinNKBNM04.Text
            sheet.Cells(24, 7) = Me.lblNyukinNKBNM05.Text
            sheet.Cells(24, 8) = Me.lblNyukinNKBNM06.Text
            sheet.Cells(24, 9) = Me.lblNyukinNKBNM07.Text
            sheet.Cells(24, 10) = Me.lblNyukinNKBNM08.Text
            sheet.Cells(24, 11) = Me.lblNyukinNKBNM09.Text
            sheet.Cells(24, 12) = Me.lblNyukinNKBNM10.Text
            'ﾌﾛﾝﾄ
            sheet.Cells(25, 3) = Me.lblNyukinFNKB01.Text
            sheet.Cells(25, 4) = Me.lblNyukinFNKB02.Text
            sheet.Cells(25, 5) = Me.lblNyukinFNKB03.Text
            sheet.Cells(25, 6) = Me.lblNyukinFNKB04.Text
            sheet.Cells(25, 7) = Me.lblNyukinFNKB05.Text
            sheet.Cells(25, 8) = Me.lblNyukinFNKB06.Text
            sheet.Cells(25, 9) = Me.lblNyukinFNKB07.Text
            sheet.Cells(25, 10) = Me.lblNyukinFNKB08.Text
            sheet.Cells(25, 11) = Me.lblNyukinFNKB09.Text
            sheet.Cells(25, 12) = Me.lblNyukinFNKB10.Text
            sheet.Cells(25, 14) = Me.lblNyukinFGokei.Text
            '入金機
            sheet.Cells(26, 3) = Me.lblNyukinMNKB01.Text
            sheet.Cells(26, 4) = Me.lblNyukinMNKB02.Text
            sheet.Cells(26, 5) = Me.lblNyukinMNKB03.Text
            sheet.Cells(26, 6) = Me.lblNyukinMNKB04.Text
            sheet.Cells(26, 7) = Me.lblNyukinMNKB05.Text
            sheet.Cells(26, 8) = Me.lblNyukinMNKB06.Text
            sheet.Cells(26, 9) = Me.lblNyukinMNKB07.Text
            sheet.Cells(26, 10) = Me.lblNyukinMNKB08.Text
            sheet.Cells(26, 11) = Me.lblNyukinMNKB09.Text
            sheet.Cells(26, 12) = Me.lblNyukinMNKB10.Text
            sheet.Cells(26, 14) = Me.lblNyukinMGokei.Text
            '簡易型入金機
            sheet.Cells(27, 3) = Me.lblNyukinM2NKB01.Text
            sheet.Cells(27, 4) = Me.lblNyukinM2NKB02.Text
            sheet.Cells(27, 5) = Me.lblNyukinM2NKB03.Text
            sheet.Cells(27, 6) = Me.lblNyukinM2NKB04.Text
            sheet.Cells(27, 7) = Me.lblNyukinM2NKB05.Text
            sheet.Cells(27, 8) = Me.lblNyukinM2NKB06.Text
            sheet.Cells(27, 9) = Me.lblNyukinM2NKB07.Text
            sheet.Cells(27, 10) = Me.lblNyukinM2NKB08.Text
            sheet.Cells(27, 11) = Me.lblNyukinM2NKB09.Text
            sheet.Cells(27, 12) = Me.lblNyukinM2NKB10.Text
            sheet.Cells(27, 14) = Me.lblNyukinM2Gokei.Text
            '合計
            sheet.Cells(28, 3) = Me.lblNyukinNKB01Gokei.Text
            sheet.Cells(28, 4) = Me.lblNyukinNKB02Gokei.Text
            sheet.Cells(28, 5) = Me.lblNyukinNKB03Gokei.Text
            sheet.Cells(28, 6) = Me.lblNyukinNKB04Gokei.Text
            sheet.Cells(28, 7) = Me.lblNyukinNKB05Gokei.Text
            sheet.Cells(28, 8) = Me.lblNyukinNKB06Gokei.Text
            sheet.Cells(28, 9) = Me.lblNyukinNKB07Gokei.Text
            sheet.Cells(28, 10) = Me.lblNyukinNKB08Gokei.Text
            sheet.Cells(28, 11) = Me.lblNyukinNKB09Gokei.Text
            sheet.Cells(28, 12) = Me.lblNyukinNKB10Gokei.Text
            sheet.Cells(28, 14) = Me.lblNyukinGokei.Text

            '■売上

            '打席ｶｰﾄﾞ
            sheet.Cells(32, 3) = Me.lblTamaCardGokei.Text
            sheet.Cells(32, 4) = Me.lblTimeCardGokei.Text
            sheet.Cells(32, 5) = Me.lblBallCardGokei.Text
            sheet.Cells(32, 6) = Me.lblShohinCardGokei.Text
            sheet.Cells(32, 7) = Me.lblNyukinCardGokei.Text
            sheet.Cells(32, 8) = Me.lblHakkenCardGokei.Text
            sheet.Cells(32, 9) = Me.lblFeeCardGokei.Text
            sheet.Cells(32, 10) = Me.lblKosuCardGokei.Text
            sheet.Cells(32, 14) = Me.lblCardGokei.Text
            '内消費税
            sheet.Cells(33, 3) = Me.lblTamaTaxCardGokei.Text
            sheet.Cells(33, 4) = Me.lblTimeTaxCardGokei.Text
            sheet.Cells(33, 5) = Me.lblBallTaxCardGokei.Text
            sheet.Cells(33, 6) = Me.lblShohinTaxCardGokei.Text
            sheet.Cells(33, 7) = Me.lblNyukinTaxCardGokei.Text
            sheet.Cells(33, 8) = Me.lblHakkenTaxCardGokei.Text
            sheet.Cells(33, 9) = Me.lblFeeTaxCardGokei.Text
            sheet.Cells(33, 10) = Me.lblKosuTaxCardGokei.Text
            sheet.Cells(33, 14) = Me.lblTaxCardGokei.Text
            '現金
            sheet.Cells(35, 3) = Me.lblTamaCashGokei.Text
            sheet.Cells(35, 4) = Me.lblTimeCashGokei.Text
            sheet.Cells(35, 5) = Me.lblBallCashGokei.Text
            sheet.Cells(35, 6) = Me.lblShohinCashGokei.Text
            sheet.Cells(35, 7) = Me.lblNyukinCashGokei.Text
            sheet.Cells(35, 8) = Me.lblHakkenCashGokei.Text
            sheet.Cells(35, 9) = Me.lblFeeCashGokei.Text
            sheet.Cells(35, 10) = Me.lblKosuCashGokei.Text
            sheet.Cells(35, 14) = Me.lblCashGokei.Text
            'クレジット
            sheet.Cells(36, 3) = Me.lblTamaCreGokei.Text
            sheet.Cells(36, 4) = Me.lblTimeCreGokei.Text
            sheet.Cells(36, 5) = Me.lblBallCreGokei.Text
            sheet.Cells(36, 6) = Me.lblShohinCreGokei.Text
            sheet.Cells(36, 7) = Me.lblNyukinCreGokei.Text
            sheet.Cells(36, 8) = Me.lblHakkenCreGokei.Text
            sheet.Cells(36, 9) = Me.lblFeeCreGokei.Text
            sheet.Cells(36, 10) = Me.lblKosuCreGokei.Text
            sheet.Cells(36, 14) = Me.lblCreGokei.Text
            '商品券
            sheet.Cells(37, 3) = Me.lblTamaGiftGokei.Text
            sheet.Cells(37, 4) = Me.lblTimeGiftGokei.Text
            sheet.Cells(37, 5) = Me.lblBallGiftGokei.Text
            sheet.Cells(37, 6) = Me.lblShohinGiftGokei.Text
            sheet.Cells(37, 7) = Me.lblNyukinGiftGokei.Text
            sheet.Cells(37, 8) = Me.lblHakkenGiftGokei.Text
            sheet.Cells(37, 9) = Me.lblFeeGiftGokei.Text
            sheet.Cells(37, 10) = Me.lblKosuGiftGokei.Text
            sheet.Cells(37, 14) = Me.lblGiftGokei.Text
            '銀行振込
            sheet.Cells(38, 3) = Me.lblTamaBankGokei.Text
            sheet.Cells(38, 4) = Me.lblTimeBankGokei.Text
            sheet.Cells(38, 5) = Me.lblBallBankGokei.Text
            sheet.Cells(38, 6) = Me.lblShohinBankGokei.Text
            sheet.Cells(38, 7) = Me.lblNyukinBankGokei.Text
            sheet.Cells(38, 8) = Me.lblHakkenBankGokei.Text
            sheet.Cells(38, 9) = Me.lblFeeBankGokei.Text
            sheet.Cells(38, 10) = Me.lblKosuBankGokei.Text
            sheet.Cells(38, 14) = Me.lblBankGokei.Text
            '合計
            sheet.Cells(39, 3) = Me.lblTamaUriGokei.Text
            sheet.Cells(39, 4) = Me.lblTimeUriGokei.Text
            sheet.Cells(39, 5) = Me.lblBallUriGokei.Text
            sheet.Cells(39, 6) = Me.lblShohinUriGokei.Text
            sheet.Cells(39, 7) = Me.lblNyukinUriGokei.Text
            sheet.Cells(39, 8) = Me.lblHakkenUriGokei.Text
            sheet.Cells(39, 9) = Me.lblFeeUriGokei.Text
            sheet.Cells(39, 10) = Me.lblKosuUriGokei.Text
            sheet.Cells(39, 14) = Me.lblUriGokei.Text
            '内消費税
            sheet.Cells(40, 3) = Me.lblTamaTaxGokei.Text
            sheet.Cells(40, 4) = Me.lblTimeTaxGokei.Text
            sheet.Cells(40, 5) = Me.lblBallTaxGokei.Text
            sheet.Cells(40, 6) = Me.lblShohinTaxGokei.Text
            sheet.Cells(40, 7) = Me.lblNyukinTaxGokei.Text
            sheet.Cells(40, 8) = Me.lblHakkenTaxGokei.Text
            sheet.Cells(40, 9) = Me.lblFeeTaxGokei.Text
            sheet.Cells(40, 10) = Me.lblKosuTaxGokei.Text
            sheet.Cells(40, 14) = Me.lblTaxGokei.Text

            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 検索ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Dim strStaSEATDT As String = String.Empty   '営業開始日付
        Dim strEndSEATDT As String = String.Empty   '営業終了日付
        Dim dr As DataRow()
        Dim sql As New System.Text.StringBuilder
        Dim intEntTamaGokei As Integer = 0
        Dim intEntTimeGokei As Integer = 0
        Dim intEntSvcGokei As Integer = 0
        Dim intNyukinFGokei As Integer = 0
        Dim intNyukinMGokei As Integer = 0
        Dim intNyukinM2Gokei As Integer = 0
        Dim strWeek As String = String.Empty
        Try
            '画面初期設定
            Init()

            If (Me.chkSun.Checked And Me.chkMon.Checked And Me.chkTue.Checked And Me.chkWed.Checked And Me.chkThu.Checked And Me.chkFri.Checked And Me.chkSat.Checked) Or _
               (Not Me.chkSun.Checked And Not Me.chkMon.Checked And Not Me.chkTue.Checked And Not Me.chkWed.Checked And Not Me.chkThu.Checked And Not Me.chkFri.Checked And Not Me.chkSat.Checked) Then
                strWeek = String.Empty
            Else
                strWeek &= "(99,"
                If Me.chkSun.Checked Then strWeek &= "0,"
                If Me.chkMon.Checked Then strWeek &= "1,"
                If Me.chkTue.Checked Then strWeek &= "2,"
                If Me.chkWed.Checked Then strWeek &= "3,"
                If Me.chkThu.Checked Then strWeek &= "4,"
                If Me.chkFri.Checked Then strWeek &= "5,"
                If Me.chkSat.Checked Then strWeek &= "6,"
                strWeek &= "99)"
            End If

            strStaSEATDT = Me.dtpStaSEATDT.Text.Replace("/", String.Empty)
            strEndSEATDT = Me.dtpEndSEATDT.Text.Replace("/", String.Empty)

            '種別ラベル
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 1")
            If dr.Length > 0 Then
                Me.lblEntNKBNM01.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM01.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM01.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 2")
            If dr.Length > 0 Then
                Me.lblEntNKBNM02.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM02.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM02.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 3")
            If dr.Length > 0 Then
                Me.lblEntNKBNM03.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM03.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM03.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 4")
            If dr.Length > 0 Then
                Me.lblEntNKBNM04.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM04.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM04.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 5")
            If dr.Length > 0 Then
                Me.lblEntNKBNM05.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM05.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM05.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 6")
            If dr.Length > 0 Then
                Me.lblEntNKBNM06.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM06.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM06.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 7")
            If dr.Length > 0 Then
                Me.lblEntNKBNM07.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM07.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM07.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 8")
            If dr.Length > 0 Then
                Me.lblEntNKBNM08.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM08.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM08.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 9")
            If dr.Length > 0 Then
                Me.lblEntNKBNM09.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM09.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM09.Text = dr(0).Item("CKBNAME").ToString
            End If
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = 10")
            If dr.Length > 0 Then
                Me.lblEntNKBNM10.Text = dr(0).Item("CKBNAME").ToString : Me.lblBallNKBNM10.Text = dr(0).Item("CKBNAME").ToString : Me.lblNyukinNKBNM10.Text = dr(0).Item("CKBNAME").ToString
            End If

            '【入場者数】

            Dim strTB1 As String = String.Empty

            strTB1 &= "(SELECT DATKB,EIGKB,ENTDT,KSBKB,MANNO FROM ENTTRA"
            If rdoEntKbn0.Checked Then strTB1 &= " UNION "
            If rdoEntKbn1.Checked Then strTB1 &= " UNION ALL "
            strTB1 &= "SELECT '1' AS DATKB,'1' AS EIGKB,ENTDT,KSBKB,MANNO FROM ENTTRB) AS TB1"

            '■1球貸し
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" KSBKB")
            sql.Append(",COUNT(MANNO) AS ENTSU")
            sql.Append(" FROM " & strTB1)
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND EIGKB = '1'")
            '営業日付
            sql.Append(" AND REPLACE(ENTDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(ENTDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(ENTDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY KSBKB")
            sql.Append(" ORDER BY KSBKB")

            Dim dtENTTAMA As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtENTTAMA.Rows.Count - 1
                Select Case dtENTTAMA.Rows(i).Item("KSBKB").ToString
                    Case "1" : Me.lblEntTamaNKB01.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblEntTamaNKB02.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblEntTamaNKB03.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "4" : Me.lblEntTamaNKB04.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "5" : Me.lblEntTamaNKB05.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "6" : Me.lblEntTamaNKB06.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "7" : Me.lblEntTamaNKB07.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "8" : Me.lblEntTamaNKB08.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "9" : Me.lblEntTamaNKB09.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "10" : Me.lblEntTamaNKB10.Text = CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End Select

                If CType(dtENTTAMA.Rows(i).Item("KSBKB").ToString, Integer) <= 10 Then
                    intEntTamaGokei += CType(dtENTTAMA.Rows(i).Item("ENTSU").ToString, Integer)
                End If
            Next

            ''■1球貸し(無人営業)
            'sql.Clear()
            'sql.Append(" SELECT ")
            'sql.Append(" KSBKB")
            'sql.Append(",COUNT(MANNO) AS ENTSU")
            'If rdoEntKbn0.Checked Then sql.Append(" FROM (SELECT DISTINCT ENTDT,KSBKB,MANNO FROM ENTTRB) AS ENTTRB ")
            'If rdoEntKbn1.Checked Then sql.Append(" FROM ENTTRB ")
            'sql.Append(" WHERE")
            ''営業日付
            'sql.Append(" REPLACE(ENTDT, '/', '')  >= '" & strStaSEATDT & "'")
            'sql.Append(" AND REPLACE(ENTDT, '/', '')  <= '" & strEndSEATDT & "'")
            'If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(ENTDT,'yyyyMMdd')) IN " & strWeek)
            'sql.Append(" GROUP BY KSBKB")
            'sql.Append(" ORDER BY KSBKB")

            'Dim dtENT2TAMA As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'For i As Integer = 0 To dtENT2TAMA.Rows.Count - 1
            '    Select Case dtENT2TAMA.Rows(i).Item("KSBKB").ToString
            '        Case "1" : Me.lblEntTamaNKB01.Text = (CType(Me.lblEntTamaNKB01.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "2" : Me.lblEntTamaNKB02.Text = (CType(Me.lblEntTamaNKB02.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "3" : Me.lblEntTamaNKB03.Text = (CType(Me.lblEntTamaNKB03.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "4" : Me.lblEntTamaNKB04.Text = (CType(Me.lblEntTamaNKB04.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "5" : Me.lblEntTamaNKB05.Text = (CType(Me.lblEntTamaNKB05.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "6" : Me.lblEntTamaNKB06.Text = (CType(Me.lblEntTamaNKB06.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "7" : Me.lblEntTamaNKB07.Text = (CType(Me.lblEntTamaNKB07.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "8" : Me.lblEntTamaNKB08.Text = (CType(Me.lblEntTamaNKB08.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "9" : Me.lblEntTamaNKB09.Text = (CType(Me.lblEntTamaNKB09.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '        Case "10" : Me.lblEntTamaNKB10.Text = (CType(Me.lblEntTamaNKB10.Text, Integer) + CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)).ToString("#,##0")
            '    End Select

            '    If CType(dtENT2TAMA.Rows(i).Item("KSBKB").ToString, Integer) <= 10 Then
            '        intEntTamaGokei += CType(dtENT2TAMA.Rows(i).Item("ENTSU").ToString, Integer)
            '    End If
            'Next

            Me.lblEntTamaGokei.Text = intEntTamaGokei.ToString("#,##0")

            '■打ち放題
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" B.NCSRANK")
            sql.Append(",COUNT(*) AS ENTSU")
            If rdoEntKbn0.Checked Then sql.Append(" FROM (SELECT DISTINCT DATKB,EIGKB,ENTDT,KSBKB,MANNO FROM ENTTRA) AS A")
            If rdoEntKbn1.Checked Then sql.Append(" FROM ENTTRA A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.EIGKB = '2'")
            '営業日付
            sql.Append(" AND REPLACE(A.ENTDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.ENTDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(A.ENTDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY B.NCSRANK")
            sql.Append(" ORDER BY B.NCSRANK")

            Dim dtENTTIME As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtENTTIME.Rows.Count - 1
                Select Case dtENTTIME.Rows(i).Item("NCSRANK").ToString
                    Case "1" : Me.lblEntTimeNKB01.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblEntTimeNKB02.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblEntTimeNKB03.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "4" : Me.lblEntTimeNKB04.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "5" : Me.lblEntTimeNKB05.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "6" : Me.lblEntTimeNKB06.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "7" : Me.lblEntTimeNKB07.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "8" : Me.lblEntTimeNKB08.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "9" : Me.lblEntTimeNKB09.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    Case "10" : Me.lblEntTimeNKB10.Text = CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End Select
                intEntTimeGokei += CType(dtENTTIME.Rows(i).Item("ENTSU").ToString, Integer)
            Next
            Me.lblEntTimeGokei.Text = intEntTimeGokei.ToString("#,##0")

            Me.lblEntGokei.Text = (intEntTamaGokei + intEntTimeGokei + intEntSvcGokei).ToString("#,##0")
            '種別毎合計
            Me.lblEntNKB01Gokei.Text = (CType(Me.lblEntTamaNKB01.Text, Integer) + CType(Me.lblEntTimeNKB01.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB02Gokei.Text = (CType(Me.lblEntTamaNKB02.Text, Integer) + CType(Me.lblEntTimeNKB02.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB03Gokei.Text = (CType(Me.lblEntTamaNKB03.Text, Integer) + CType(Me.lblEntTimeNKB03.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB04Gokei.Text = (CType(Me.lblEntTamaNKB04.Text, Integer) + CType(Me.lblEntTimeNKB04.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB05Gokei.Text = (CType(Me.lblEntTamaNKB05.Text, Integer) + CType(Me.lblEntTimeNKB05.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB06Gokei.Text = (CType(Me.lblEntTamaNKB06.Text, Integer) + CType(Me.lblEntTimeNKB06.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB07Gokei.Text = (CType(Me.lblEntTamaNKB07.Text, Integer) + CType(Me.lblEntTimeNKB07.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB08Gokei.Text = (CType(Me.lblEntTamaNKB08.Text, Integer) + CType(Me.lblEntTimeNKB08.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB09Gokei.Text = (CType(Me.lblEntTamaNKB09.Text, Integer) + CType(Me.lblEntTimeNKB09.Text, Integer)).ToString("#,##0")
            Me.lblEntNKB10Gokei.Text = (CType(Me.lblEntTamaNKB10.Text, Integer) + CType(Me.lblEntTimeNKB10.Text, Integer)).ToString("#,##0")

            '【利用ボール数】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(NKB01BALL) IS NULL THEN 0 ELSE SUM(NKB01BALL) END AS NKB01BALL")
            sql.Append(",CASE WHEN SUM(NKB02BALL) IS NULL THEN 0 ELSE SUM(NKB02BALL) END AS NKB02BALL")
            sql.Append(",CASE WHEN SUM(NKB03BALL) IS NULL THEN 0 ELSE SUM(NKB03BALL) END AS NKB03BALL")
            sql.Append(",CASE WHEN SUM(NKB04BALL) IS NULL THEN 0 ELSE SUM(NKB04BALL) END AS NKB04BALL")
            sql.Append(",CASE WHEN SUM(NKB05BALL) IS NULL THEN 0 ELSE SUM(NKB05BALL) END AS NKB05BALL")
            sql.Append(",CASE WHEN SUM(NKB06BALL) IS NULL THEN 0 ELSE SUM(NKB06BALL) END AS NKB06BALL")
            sql.Append(",CASE WHEN SUM(NKB07BALL) IS NULL THEN 0 ELSE SUM(NKB07BALL) END AS NKB07BALL")
            sql.Append(",CASE WHEN SUM(NKB08BALL) IS NULL THEN 0 ELSE SUM(NKB08BALL) END AS NKB08BALL")
            sql.Append(",CASE WHEN SUM(NKB09BALL) IS NULL THEN 0 ELSE SUM(NKB09BALL) END AS NKB09BALL")
            sql.Append(",CASE WHEN SUM(NKB10BALL) IS NULL THEN 0 ELSE SUM(NKB10BALL) END AS NKB10BALL")
            sql.Append(",CASE WHEN SUM(NKB11BALL) IS NULL THEN 0 ELSE SUM(NKB11BALL) END AS NKB11BALL")
            sql.Append(",CASE WHEN SUM(NKB12BALL) IS NULL THEN 0 ELSE SUM(NKB12BALL) END AS NKB12BALL")
            sql.Append(",CASE WHEN SUM(NKB13BALL) IS NULL THEN 0 ELSE SUM(NKB13BALL) END AS NKB13BALL")
            sql.Append(",CASE WHEN SUM(NKB14BALL) IS NULL THEN 0 ELSE SUM(NKB14BALL) END AS NKB14BALL")
            sql.Append(",CASE WHEN SUM(NKB15BALL) IS NULL THEN 0 ELSE SUM(NKB15BALL) END AS NKB15BALL")
            sql.Append(" FROM SEATSMA ")
            sql.Append(" WHERE")
            '営業日付
            sql.Append(" REPLACE(SEATDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(SEATDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(SEATDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtBALL As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtBALL.Rows.Count - 1
                Me.lblBallTamaNKB01.Text = CType(dtBALL.Rows(i).Item("NKB01BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB02.Text = CType(dtBALL.Rows(i).Item("NKB02BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB03.Text = CType(dtBALL.Rows(i).Item("NKB03BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB04.Text = CType(dtBALL.Rows(i).Item("NKB04BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB05.Text = CType(dtBALL.Rows(i).Item("NKB05BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB06.Text = CType(dtBALL.Rows(i).Item("NKB06BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB07.Text = CType(dtBALL.Rows(i).Item("NKB07BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB08.Text = CType(dtBALL.Rows(i).Item("NKB08BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB09.Text = CType(dtBALL.Rows(i).Item("NKB09BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaNKB10.Text = CType(dtBALL.Rows(i).Item("NKB10BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallTamaGokei.Text = (CType(dtBALL.Rows(i).Item("NKB01BALL").ToString, Integer) + CType(dtBALL.Rows(i).Item("NKB02BALL").ToString, Integer) _
                                        + CType(dtBALL.Rows(i).Item("NKB03BALL").ToString, Integer) + CType(dtBALL.Rows(i).Item("NKB04BALL").ToString, Integer) _
                                        + CType(dtBALL.Rows(i).Item("NKB05BALL").ToString, Integer) + +CType(dtBALL.Rows(i).Item("NKB06BALL").ToString, Integer) _
                                        + CType(dtBALL.Rows(i).Item("NKB07BALL").ToString, Integer) + CType(dtBALL.Rows(i).Item("NKB08BALL").ToString, Integer) _
                                        + CType(dtBALL.Rows(i).Item("NKB09BALL").ToString, Integer) + CType(dtBALL.Rows(i).Item("NKB10BALL").ToString, Integer)).ToString("#,##0")
                Me.lblBallTime.Text = CType(dtBALL.Rows(i).Item("NKB12BALL").ToString, Integer).ToString("#,##0")
                Me.lblBallMente.Text = CType(dtBALL.Rows(i).Item("NKB11BALL").ToString, Integer).ToString("#,##0")
            Next

            '【ｶｰﾄﾞ発券(フロント)】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" COUNT(*) AS HAKKENSU")
            sql.Append(",CASE WHEN SUM(UDNKN) IS NULL THEN 0 ELSE SUM(UDNKN) END AS HAKKENKIN")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '001'")
            sql.Append(" AND A.SMADT = '999'")
            '営業日付
            sql.Append(" AND REPLACE(A.UDNDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.UDNDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtHAKKENF As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim intHakkenFTax As Integer = 0

            For i As Integer = 0 To dtHAKKENF.Rows.Count - 1
                Me.lblCardFHakken.Text = CType(dtHAKKENF.Rows(i).Item("HAKKENSU").ToString, Integer).ToString("#,##0")
                Me.lblHakkenFKin.Text = CType(dtHAKKENF.Rows(i).Item("HAKKENKIN").ToString, Integer).ToString("#,##0")
            Next
            intHakkenFTax = UIFunction.CalcExcludedTax(CType(Me.lblHakkenFKin.Text, Integer))

            '【ｶｰﾄﾞ発券】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" COUNT(*) AS HAKKENSU")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN NKNTRN AS B ON")
            sql.Append(" B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '010'")
            sql.Append(" AND B.STSFLG = '1'")
            '営業日付
            sql.Append(" AND REPLACE(A.UDNDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.UDNDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtHAKKEN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtHAKKEN.Rows.Count - 1
                Me.lblCardHakken.Text = CType(dtHAKKEN.Rows(i).Item("HAKKENSU").ToString, Integer).ToString("#,##0")
            Next

            '【ｶｰﾄﾞ発券金額】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(PRERT) IS NULL THEN 0 ELSE SUM(PRERT) END AS HAKKENKIN")
            sql.Append(" FROM NKNTRN ")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND STSFLG = '1'")
            '営業日付
            sql.Append(" AND REPLACE(DENDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(DENDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(DENDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtHAKKENKIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtHAKKENKIN.Rows.Count - 1
                Me.lblHakkenKin.Text = CType(dtHAKKENKIN.Rows(i).Item("HAKKENKIN").ToString, Integer).ToString("#,##0")
            Next
            Me.lblHakkenCardGokei.Text = Me.lblHakkenKin.Text
            If CType(Me.lblHakkenCardGokei.Text, Integer) > 0 Then
                Me.lblHakkenTaxCardGokei.Text = UIFunction.CalcExcludedTax(CType(Me.lblHakkenCardGokei.Text, Integer)).ToString("#,##0")
            End If

            '【ｶｰﾄﾞ発券(簡易型)】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" COUNT(*) AS HAKKENSU")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN NKNTRN AS B ON")
            sql.Append(" B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '010'")
            sql.Append(" AND B.STSFLG = '2'")
            '営業日付
            sql.Append(" AND REPLACE(A.UDNDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.UDNDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtHAKKEN2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtHAKKEN2.Rows.Count - 1
                Me.lblCardHakken2.Text = CType(dtHAKKEN2.Rows(i).Item("HAKKENSU").ToString, Integer).ToString("#,##0")
            Next

            '【ｶｰﾄﾞ発券金額(簡易型)】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(PRERT) IS NULL THEN 0 ELSE SUM(PRERT) END AS HAKKENKIN")
            sql.Append(" FROM NKNTRN ")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND STSFLG = '2'")
            '営業日付
            sql.Append(" AND REPLACE(DENDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(DENDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(DENDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtHAKKENKIN2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtHAKKENKIN2.Rows.Count - 1
                Me.lblHakkenKin2.Text = CType(dtHAKKENKIN2.Rows(i).Item("HAKKENKIN").ToString, Integer).ToString("#,##0")
            Next
            Me.lblHakkenCardGokei.Text = (CType(Me.lblHakkenFKin.Text, Integer) + CType(Me.lblHakkenCardGokei.Text, Integer) + CType(Me.lblHakkenKin2.Text, Integer)).ToString("#,##0")
            If CType(Me.lblHakkenCardGokei.Text, Integer) > 0 Then
                Me.lblHakkenTaxCardGokei.Text = UIFunction.CalcExcludedTax(CType(Me.lblHakkenCardGokei.Text, Integer)).ToString("#,##0")
            End If

            '【サービス入金額】
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(NKNKN) IS NULL THEN 0 ELSE SUM(NKNKN) END AS SVCKIN")
            sql.Append(" FROM NKNTRN ")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND STSFLG = '9'")
            '営業日付
            sql.Append(" AND REPLACE(DENDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(DENDT, '/', '')  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(DENDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtSvcKin As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtSvcKin.Rows.Count - 1
                Me.lblSvcKin.Text = CType(dtSvcKin.Rows(i).Item("SVCKIN").ToString, Integer).ToString("#,##0")
            Next

            '【入金】

            'ﾌﾛﾝﾄ
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" B.NCSRANK")
            sql.Append(",SUM(A.NKNBKN) AS NKNKN")
            sql.Append(" FROM NKNTRN A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            '営業日付
            sql.Append(" AND REPLACE(A.DENDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.DENDT, '/', '')  <= '" & strEndSEATDT & "'")
            sql.Append(" AND STSFLG = '0'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(A.DENDT,'yyyyMMdd')) IN " & strWeek)

            sql.Append(" GROUP BY B.NCSRANK")
            sql.Append(" ORDER BY B.NCSRANK")

            Dim dtFNYUKIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtFNYUKIN.Rows.Count - 1
                Select Case dtFNYUKIN.Rows(i).Item("NCSRANK").ToString
                    Case "1" : Me.lblNyukinFNKB01.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblNyukinFNKB02.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblNyukinFNKB03.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "4" : Me.lblNyukinFNKB04.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "5" : Me.lblNyukinFNKB05.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "6" : Me.lblNyukinFNKB06.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "7" : Me.lblNyukinFNKB07.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "8" : Me.lblNyukinFNKB08.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "9" : Me.lblNyukinFNKB09.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "10" : Me.lblNyukinFNKB10.Text = CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case Else : Me.lblNyukinFNKB02.Text = (CType(Me.lblNyukinFNKB02.Text, Integer) + CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer)).ToString("#,##0")
                End Select
                intNyukinFGokei += CType(dtFNYUKIN.Rows(i).Item("NKNKN").ToString, Integer)
            Next
            Me.lblNyukinFGokei.Text = intNyukinFGokei.ToString("#,##0")

            '入金機
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" B.NCSRANK")
            sql.Append(",SUM(A.NKNBKN) AS NKNKN")
            sql.Append(" FROM NKNTRN A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            '営業日付
            sql.Append(" AND REPLACE(A.DENDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.DENDT, '/', '')  <= '" & strEndSEATDT & "'")
            sql.Append(" AND STSFLG = '1'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(A.DENDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY B.NCSRANK")
            sql.Append(" ORDER BY B.NCSRANK")

            Dim dtMNYUKIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtMNYUKIN.Rows.Count - 1
                Select Case dtMNYUKIN.Rows(i).Item("NCSRANK").ToString
                    Case "1" : Me.lblNyukinMNKB01.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblNyukinMNKB02.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblNyukinMNKB03.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "4" : Me.lblNyukinMNKB04.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "5" : Me.lblNyukinMNKB05.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "6" : Me.lblNyukinMNKB06.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "7" : Me.lblNyukinMNKB07.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "8" : Me.lblNyukinMNKB08.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "9" : Me.lblNyukinMNKB09.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "10" : Me.lblNyukinMNKB10.Text = CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case Else : Me.lblNyukinMNKB02.Text = (CType(Me.lblNyukinMNKB02.Text, Integer) + CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer)).ToString("#,##0")
                End Select
                intNyukinMGokei += CType(dtMNYUKIN.Rows(i).Item("NKNKN").ToString, Integer)
            Next
            Me.lblNyukinMGokei.Text = intNyukinMGokei.ToString("#,##0")

            '簡易型入金機
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" B.NCSRANK")
            sql.Append(",SUM(A.NKNBKN) AS NKNKN")
            sql.Append(" FROM NKNTRN A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            '営業日付
            sql.Append(" AND REPLACE(A.DENDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(A.DENDT, '/', '')  <= '" & strEndSEATDT & "'")
            sql.Append(" AND STSFLG = '2'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(A.DENDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY B.NCSRANK")
            sql.Append(" ORDER BY B.NCSRANK")

            Dim dtMNYUKIN2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtMNYUKIN2.Rows.Count - 1
                Select Case dtMNYUKIN2.Rows(i).Item("NCSRANK").ToString
                    Case "1" : Me.lblNyukinM2NKB01.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblNyukinM2NKB02.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblNyukinM2NKB03.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "4" : Me.lblNyukinM2NKB04.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "5" : Me.lblNyukinM2NKB05.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "6" : Me.lblNyukinM2NKB06.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "7" : Me.lblNyukinM2NKB07.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "8" : Me.lblNyukinM2NKB08.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "9" : Me.lblNyukinM2NKB09.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case "10" : Me.lblNyukinM2NKB10.Text = CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer).ToString("#,##0")
                    Case Else : Me.lblNyukinM2NKB02.Text = (CType(Me.lblNyukinM2NKB02.Text, Integer) + CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer)).ToString("#,##0")
                End Select
                intNyukinM2Gokei += CType(dtMNYUKIN2.Rows(i).Item("NKNKN").ToString, Integer)
            Next
            Me.lblNyukinM2Gokei.Text = intNyukinM2Gokei.ToString("#,##0")

            Me.lblNyukinGokei.Text = (intNyukinFGokei + intNyukinMGokei + intNyukinM2Gokei).ToString("#,##0")

            '種別毎合計
            Me.lblNyukinNKB01Gokei.Text = (CType(Me.lblNyukinFNKB01.Text, Integer) + CType(Me.lblNyukinMNKB01.Text, Integer) + CType(Me.lblNyukinM2NKB01.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB02Gokei.Text = (CType(Me.lblNyukinFNKB02.Text, Integer) + CType(Me.lblNyukinMNKB02.Text, Integer) + CType(Me.lblNyukinM2NKB02.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB03Gokei.Text = (CType(Me.lblNyukinFNKB03.Text, Integer) + CType(Me.lblNyukinMNKB03.Text, Integer) + CType(Me.lblNyukinM2NKB03.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB04Gokei.Text = (CType(Me.lblNyukinFNKB04.Text, Integer) + CType(Me.lblNyukinMNKB04.Text, Integer) + CType(Me.lblNyukinM2NKB04.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB05Gokei.Text = (CType(Me.lblNyukinFNKB05.Text, Integer) + CType(Me.lblNyukinMNKB05.Text, Integer) + CType(Me.lblNyukinM2NKB05.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB06Gokei.Text = (CType(Me.lblNyukinFNKB06.Text, Integer) + CType(Me.lblNyukinMNKB06.Text, Integer) + CType(Me.lblNyukinM2NKB06.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB07Gokei.Text = (CType(Me.lblNyukinFNKB07.Text, Integer) + CType(Me.lblNyukinMNKB07.Text, Integer) + CType(Me.lblNyukinM2NKB07.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB08Gokei.Text = (CType(Me.lblNyukinFNKB08.Text, Integer) + CType(Me.lblNyukinMNKB08.Text, Integer) + CType(Me.lblNyukinM2NKB08.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB09Gokei.Text = (CType(Me.lblNyukinFNKB09.Text, Integer) + CType(Me.lblNyukinMNKB09.Text, Integer) + CType(Me.lblNyukinM2NKB09.Text, Integer)).ToString("#,##0")
            Me.lblNyukinNKB10Gokei.Text = (CType(Me.lblNyukinFNKB10.Text, Integer) + CType(Me.lblNyukinMNKB10.Text, Integer) + CType(Me.lblNyukinM2NKB10.Text, Integer)).ToString("#,##0")

            '【売上】

            '1球貸し
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(ENTBKN) IS NULL THEN 0 ELSE SUM(ENTBKN) END AS ENTBKN")
            sql.Append(",CASE WHEN SUM(ENTZEI) IS NULL THEN 0 ELSE SUM(ENTZEI) END AS ENTZEI")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            '営業日付
            sql.Append(" AND REPLACE(ENTDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(ENTDT, '/', '')  <= '" & strEndSEATDT & "'")
            sql.Append(" AND EIGKB = '1'")
            sql.Append(" AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10')")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(ENTDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtURITAMA As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtURITAMA.Rows.Count - 1
                Me.lblTamaCardGokei.Text = CType(dtURITAMA.Rows(i).Item("ENTBKN").ToString, Integer).ToString("#,##0")
                Me.lblTamaTaxCardGokei.Text = CType(dtURITAMA.Rows(i).Item("ENTZEI").ToString, Integer).ToString("#,##0")
            Next


            '打ち放題
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(ENTBKN) IS NULL THEN 0 ELSE SUM(ENTBKN) END AS ENTBKN")
            sql.Append(",CASE WHEN SUM(ENTZEI) IS NULL THEN 0 ELSE SUM(ENTZEI) END AS ENTZEI")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            '営業日付
            sql.Append(" AND REPLACE(ENTDT, '/', '')  >= '" & strStaSEATDT & "'")
            sql.Append(" AND REPLACE(ENTDT, '/', '')  <= '" & strEndSEATDT & "'")
            sql.Append(" AND EIGKB = '2'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(ENTDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtURITIME As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtURITIME.Rows.Count - 1
                Me.lblTimeCardGokei.Text = CType(dtURITIME.Rows(i).Item("ENTBKN").ToString, Integer).ToString("#,##0")
                Me.lblTimeTaxCardGokei.Text = CType(dtURITIME.Rows(i).Item("ENTZEI").ToString, Integer).ToString("#,##0")
            Next


            '■ボール料金
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN ")
            sql.Append(" (")
            sql.Append("SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN)")
            sql.Append(" + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN) + SUM(NKB12KIN)")
            sql.Append(")")
            sql.Append(" IS NULL THEN 0 ELSE")
            sql.Append(" (")
            sql.Append("SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN)")
            sql.Append(" + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN) + SUM(NKB12KIN)")
            sql.Append(") END AS NKBKIN")
            sql.Append(",CASE WHEN")
            sql.Append("(")
            sql.Append("SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN)")
            sql.Append(" + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN) + SUM(NKB12TAXKIN)")
            sql.Append(")")
            sql.Append(" IS NULL THEN 0 ELSE")
            sql.Append("(")
            sql.Append("SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN)")
            sql.Append(" + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN) + SUM(NKB12TAXKIN)")
            sql.Append(") END AS NKBTAXKIN")
            sql.Append(" FROM SEATSMA")
            sql.Append(" WHERE")
            sql.Append(" SEATDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND SEATDT  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(SEATDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtURIBALL As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtURIBALL.Rows.Count - 1
                Me.lblBallCardGokei.Text = CType(dtURIBALL.Rows(i).Item("NKBKIN").ToString, Integer).ToString("#,##0")
                Me.lblBallTaxCardGokei.Text = CType(dtURIBALL.Rows(i).Item("NKBTAXKIN").ToString, Integer).ToString("#,##0")
            Next

            '■商品

            Dim intTAX As Integer = 0

            '打席ｶｰﾄﾞ
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(UDNBKN) IS NULL THEN 0 ELSE SUM(UDNBKN) END AS UDNBKN")
            sql.Append(",CASE WHEN SUM(UDNZKN) IS NULL THEN 0 ELSE SUM(UDNZKN) END AS UDNZKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB ='1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND BUNCDB = '001'")
            sql.Append(" AND BUNCDC = '001'")
            sql.Append(" AND MANNO IS NOT NULL")
            sql.Append(" AND CPAYKBN = '4'")
            sql.Append(" AND UDNDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND UDNDT  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtSHOHINCARD As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtSHOHINCARD.Rows.Count - 1
                Me.lblShohinCardGokei.Text = (CType(dtSHOHINCARD.Rows(i).Item("UDNBKN").ToString, Integer) - CType(Me.lblHakkenFKin.Text, Integer)).ToString("#,##0")
                intTAX += CType(dtSHOHINCARD.Rows(i).Item("UDNZKN").ToString, Integer)
            Next

            If intHakkenFTax > 0 Then
                'intTAX -= intHakkenFTax
            End If

            Me.lblShohinTaxCardGokei.Text = intTAX.ToString("#,##0")

            intTAX = 0

            '現金・クレジット・商品券・銀行振込
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CPAYKBN")
            sql.Append(",CASE WHEN SUM(UDNBKN) IS NULL THEN 0 ELSE SUM(UDNBKN) END AS UDNBKN")
            sql.Append(",CASE WHEN SUM(UDNZKN) IS NULL THEN 0 ELSE SUM(UDNZKN) END AS UDNZKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB ='1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND BUNCDB = '001'")
            sql.Append(" AND BUNCDC = '001'")
            'sql.Append(" AND MANNO IS NULL")
            sql.Append(" AND (CPAYKBN = '0' OR CPAYKBN = '1' OR CPAYKBN = '2' OR CPAYKBN = '3')")
            sql.Append(" AND UDNDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND UDNDT  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY CPAYKBN")
            sql.Append(" ORDER BY CPAYKBN")

            Dim dtURISHOHIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtURISHOHIN.Rows.Count - 1
                Select Case dtURISHOHIN.Rows(i).Item("CPAYKBN").ToString
                    Case "0" : Me.lblShohinCashGokei.Text = CType(dtURISHOHIN.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                    Case "1" : Me.lblShohinCreGokei.Text = CType(dtURISHOHIN.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblShohinGiftGokei.Text = CType(dtURISHOHIN.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblShohinBankGokei.Text = CType(dtURISHOHIN.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                End Select
                intTAX += CType(dtURISHOHIN.Rows(i).Item("UDNZKN").ToString, Integer)
            Next

            Me.lblShohinUriGokei.Text = (CType(Me.lblShohinCashGokei.Text, Integer) + CType(Me.lblShohinCreGokei.Text, Integer) _
                                        + CType(Me.lblShohinGiftGokei.Text, Integer) + CType(Me.lblShohinBankGokei.Text, Integer)).ToString("#,##0")
            Me.lblShohinTaxGokei.Text = intTAX.ToString("#,##0")

            '■入金
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CPAYKBN")
            sql.Append(",CASE WHEN SUM(UDNBKN) IS NULL THEN 0 ELSE SUM(UDNBKN) END AS UDNBKN")
            sql.Append(",CASE WHEN SUM(UDNZKN) IS NULL THEN 0 ELSE SUM(UDNZKN) END AS UDNZKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB ='1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND (BUNCDA = '003' OR BUNCDA = '010')")
            sql.Append(" AND UDNDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND UDNDT  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY CPAYKBN")
            sql.Append(" ORDER BY CPAYKBN")

            Dim dtUriNyukin As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtUriNyukin.Rows.Count - 1
                Select Case dtUriNyukin.Rows(i).Item("CPAYKBN").ToString
                    Case "0" : Me.lblNyukinCashGokei.Text = Me.lblNyukinGokei.Text
                        'Case "0" : Me.lblNyukinCashGokei.Text = CType(dtUriNyukin.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                    Case "1" : Me.lblNyukinCreGokei.Text = CType(dtUriNyukin.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                    Case "2" : Me.lblNyukinGiftGokei.Text = CType(dtUriNyukin.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                    Case "3" : Me.lblNyukinBankGokei.Text = CType(dtUriNyukin.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                End Select
                Me.lblNyukinTaxGokei.Text = (CType(Me.lblNyukinTaxGokei.Text, Integer) + CType(dtUriNyukin.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
            Next

            '現金以外の売り上げを引く
            If CType(Me.lblNyukinCashGokei.Text, Integer) > 0 Then
                Me.lblNyukinCashGokei.Text = (CType(Me.lblNyukinCashGokei.Text, Integer) - CType(Me.lblNyukinCreGokei.Text, Integer) - CType(Me.lblNyukinGiftGokei.Text, Integer) - CType(Me.lblNyukinBankGokei.Text, Integer)).ToString("#,##0")

            End If

            Me.lblNyukinUriGokei.Text = (CType(Me.lblNyukinCardGokei.Text, Integer) + CType(Me.lblNyukinCashGokei.Text, Integer) + CType(Me.lblNyukinCreGokei.Text, Integer) _
                                        + CType(Me.lblNyukinGiftGokei.Text, Integer) + CType(Me.lblNyukinBankGokei.Text, Integer)).ToString("#,##0")


            '■年会費
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CPAYKBN")
            sql.Append(",CASE WHEN SUM(UDNBKN) IS NULL THEN 0 ELSE SUM(UDNBKN) END AS UDNBKN")
            sql.Append(",CASE WHEN SUM(UDNZKN) IS NULL THEN 0 ELSE SUM(UDNZKN) END AS UDNZKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB ='1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND BUNCDB = '001'")
            sql.Append(" AND BUNCDC = '999'")
            sql.Append(" AND UDNDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND UDNDT  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)
            sql.Append(" GROUP BY CPAYKBN")
            sql.Append(" ORDER BY CPAYKBN")

            Dim dtUriFee As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtUriFee.Rows.Count - 1
                Select Case dtUriFee.Rows(i).Item("CPAYKBN").ToString
                    Case "0"
                        Me.lblFeeCashGokei.Text = CType(dtUriFee.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                        Me.lblFeeTaxGokei.Text = (CType(Me.lblFeeTaxGokei.Text, Integer) + CType(dtUriFee.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
                    Case "1"
                        Me.lblFeeCreGokei.Text = CType(dtUriFee.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                        Me.lblFeeTaxGokei.Text = (CType(Me.lblFeeTaxGokei.Text, Integer) + CType(dtUriFee.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
                    Case "2"
                        Me.lblFeeGiftGokei.Text = CType(dtUriFee.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                        Me.lblFeeTaxGokei.Text = (CType(Me.lblFeeTaxGokei.Text, Integer) + CType(dtUriFee.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
                    Case "3"
                        Me.lblFeeBankGokei.Text = CType(dtUriFee.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                        Me.lblFeeTaxGokei.Text = (CType(Me.lblFeeTaxGokei.Text, Integer) + CType(dtUriFee.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
                    Case "4"
                        Me.lblFeeCardGokei.Text = CType(dtUriFee.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                        Me.lblFeeTaxCardGokei.Text = (CType(Me.lblFeeTaxCardGokei.Text, Integer) + CType(dtUriFee.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
                End Select
            Next

            Me.lblFeeUriGokei.Text = (CType(Me.lblFeeCashGokei.Text, Integer) + CType(Me.lblFeeCreGokei.Text, Integer) _
                                        + CType(Me.lblFeeGiftGokei.Text, Integer) + CType(Me.lblFeeBankGokei.Text, Integer)).ToString("#,##0")

            '■コース
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(UDNBKN) IS NULL THEN 0 ELSE SUM(UDNBKN) END AS UDNBKN")
            sql.Append(",CASE WHEN SUM(UDNZKN) IS NULL THEN 0 ELSE SUM(UDNZKN) END AS UDNZKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB ='1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '006'")
            sql.Append(" AND UDNDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND UDNDT  <= '" & strEndSEATDT & "'")
            If Not String.IsNullOrEmpty(strWeek) Then sql.Append(" AND EXTRACT(dow FROM TO_DATE(UDNDT,'yyyyMMdd')) IN " & strWeek)

            Dim dtKosu As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtKosu.Rows.Count - 1
                Me.lblKosuCardGokei.Text = CType(dtKosu.Rows(i).Item("UDNBKN").ToString, Integer).ToString("#,##0")
                Me.lblKosuTaxCardGokei.Text = (CType(Me.lblKosuTaxCardGokei.Text, Integer) + CType(dtKosu.Rows(i).Item("UDNZKN").ToString, Integer)).ToString("#,##0")
            Next

            Me.lblKosuUriGokei.Text = (CType(Me.lblKosuCashGokei.Text, Integer) + CType(Me.lblKosuCreGokei.Text, Integer) _
                                        + CType(Me.lblKosuGiftGokei.Text, Integer) + CType(Me.lblKosuBankGokei.Text, Integer)).ToString("#,##0")


            '■合計

            '打席ｶｰﾄﾞ
            Me.lblCardGokei.Text = (CType(Me.lblTamaCardGokei.Text, Integer) + CType(Me.lblTimeCardGokei.Text, Integer) _
                                + CType(Me.lblBallCardGokei.Text, Integer) + CType(Me.lblShohinCardGokei.Text, Integer) + CType(Me.lblNyukinCardGokei.Text, Integer) _
                                + CType(Me.lblHakkenCardGokei.Text, Integer) + CType(Me.lblFeeCardGokei.Text, Integer) + CType(Me.lblKosuCardGokei.Text, Integer)).ToString("#,##0")
            '内消費税
            Me.lblTaxCardGokei.Text = (CType(Me.lblTamaTaxCardGokei.Text, Integer) + CType(Me.lblTimeTaxCardGokei.Text, Integer) _
                    + CType(Me.lblBallTaxCardGokei.Text, Integer) + CType(Me.lblShohinTaxCardGokei.Text, Integer) + CType(Me.lblNyukinTaxCardGokei.Text, Integer) _
                    + CType(Me.lblHakkenTaxCardGokei.Text, Integer) + CType(Me.lblFeeTaxCardGokei.Text, Integer) + CType(Me.lblKosuTaxCardGokei.Text, Integer)).ToString("#,##0")

            '現金
            Me.lblCashGokei.Text = (CType(Me.lblTamaCashGokei.Text, Integer) + CType(Me.lblTimeCashGokei.Text, Integer) _
                                + CType(Me.lblBallCashGokei.Text, Integer) + CType(Me.lblShohinCashGokei.Text, Integer) + CType(Me.lblNyukinCashGokei.Text, Integer) _
                                + CType(Me.lblFeeCashGokei.Text, Integer) + CType(Me.lblKosuCashGokei.Text, Integer)).ToString("#,##0")
            'クレジット
            Me.lblCreGokei.Text = (CType(Me.lblTamaCreGokei.Text, Integer) + CType(Me.lblTimeCreGokei.Text, Integer) _
                                + CType(Me.lblBallCreGokei.Text, Integer) + CType(Me.lblShohinCreGokei.Text, Integer) + CType(Me.lblNyukinCreGokei.Text, Integer) _
                                + CType(Me.lblFeeCreGokei.Text, Integer) + CType(Me.lblKosuCreGokei.Text, Integer)).ToString("#,##0")
            '商品券
            Me.lblGiftGokei.Text = (CType(Me.lblTamaGiftGokei.Text, Integer) + CType(Me.lblTimeGiftGokei.Text, Integer) _
                                + CType(Me.lblBallGiftGokei.Text, Integer) + CType(Me.lblShohinGiftGokei.Text, Integer) + CType(Me.lblNyukinGiftGokei.Text, Integer) _
                                + CType(Me.lblFeeGiftGokei.Text, Integer) + CType(Me.lblKosuGiftGokei.Text, Integer)).ToString("#,##0")
            '銀行振込
            Me.lblBankGokei.Text = (CType(Me.lblTamaBankGokei.Text, Integer) + CType(Me.lblTimeBankGokei.Text, Integer) _
                                + CType(Me.lblBallBankGokei.Text, Integer) + CType(Me.lblShohinBankGokei.Text, Integer) + CType(Me.lblNyukinBankGokei.Text, Integer) _
                                + CType(Me.lblFeeBankGokei.Text, Integer) + CType(Me.lblKosuBankGokei.Text, Integer)).ToString("#,##0")
            '合計
            Me.lblUriGokei.Text = (CType(Me.lblCashGokei.Text, Integer) + CType(Me.lblCreGokei.Text, Integer) _
                                + CType(Me.lblGiftGokei.Text, Integer) + CType(Me.lblBankGokei.Text, Integer)).ToString("#,##0")
            '内消費税
            Me.lblTaxGokei.Text = (CType(Me.lblTamaTaxGokei.Text, Integer) + CType(Me.lblTimeTaxGokei.Text, Integer) _
                                + CType(Me.lblBallTaxGokei.Text, Integer) + CType(Me.lblShohinTaxGokei.Text, Integer) + CType(Me.lblNyukinTaxGokei.Text, Integer) _
                                + CType(Me.lblFeeTaxGokei.Text, Integer) + CType(Me.lblKosuTaxGokei.Text, Integer)).ToString("#,##0")

            If Me.chkAverage.Checked Then
                '// 平均値 //

                Dim intSunCnt As Integer = 0
                Dim intMonCnt As Integer = 0
                Dim intTueCnt As Integer = 0
                Dim intWedCnt As Integer = 0
                Dim intThuCnt As Integer = 0
                Dim intFriCnt As Integer = 0
                Dim intStaCnt As Integer = 0
                Dim intYear As Integer = 0
                Dim intMonth As Integer = 0
                Dim intDay As Integer = 0
                If chkAverage.Checked Then
                    For i As Integer = CType(strStaSEATDT, Integer) To CType(strEndSEATDT, Integer)
                        Try
                            Select Case Weekday(DateTime.Parse(i.ToString.Substring(0, 4) & "/" & i.ToString.Substring(4, 2) & "/" & i.ToString.Substring(6, 2))) - 1
                                Case 0 : intSunCnt += 1
                                Case 1 : intMonCnt += 1
                                Case 2 : intTueCnt += 1
                                Case 3 : intWedCnt += 1
                                Case 4 : intThuCnt += 1
                                Case 5 : intFriCnt += 1
                                Case 6 : intStaCnt += 1
                            End Select
                        Catch ex As Exception
                            intYear = CType(i.ToString.Substring(0, 4), Integer)
                            intMonth = CType(i.ToString.Substring(4, 2), Integer)
                            intDay = CType(i.ToString.Substring(6, 2), Integer)
                            If intDay > 28 Then
                                intMonth += 1
                                intDay = 0
                            ElseIf intMonCnt > 12 Then
                                intMonth = 1
                                intDay = 0
                                intYear += 1
                            End If
                            i = CType(intYear.ToString & intMonth.ToString.PadLeft(2, "0"c) & intDay.ToString.PadLeft(2, "0"c), Integer)
                            Continue For
                        End Try
                    Next
                End If
                Dim intDayGokei As Integer = 0
                If String.IsNullOrEmpty(strWeek) Then
                    intDayGokei = intSunCnt + intMonCnt + intTueCnt + intWedCnt + intThuCnt + intFriCnt + intStaCnt
                Else
                    If Me.chkSun.Checked Then intDayGokei += intSunCnt
                    If Me.chkMon.Checked Then intDayGokei += intMonCnt
                    If Me.chkTue.Checked Then intDayGokei += intTueCnt
                    If Me.chkWed.Checked Then intDayGokei += intWedCnt
                    If Me.chkThu.Checked Then intDayGokei += intThuCnt
                    If Me.chkFri.Checked Then intDayGokei += intFriCnt
                    If Me.chkSat.Checked Then intDayGokei += intStaCnt
                End If


                '【入場者数】
                '1球貸し
                Me.lblEntTamaNKB01.Text = (CType(Me.lblEntTamaNKB01.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB02.Text = (CType(Me.lblEntTamaNKB02.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB03.Text = (CType(Me.lblEntTamaNKB03.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB04.Text = (CType(Me.lblEntTamaNKB04.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB05.Text = (CType(Me.lblEntTamaNKB05.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB06.Text = (CType(Me.lblEntTamaNKB06.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB07.Text = (CType(Me.lblEntTamaNKB07.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB08.Text = (CType(Me.lblEntTamaNKB08.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB09.Text = (CType(Me.lblEntTamaNKB09.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTamaNKB10.Text = (CType(Me.lblEntTamaNKB10.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblEntTamaGokei.Text = (CType(Me.lblEntTamaNKB01.Text, Integer) + CType(Me.lblEntTamaNKB02.Text, Integer) + CType(Me.lblEntTamaNKB03.Text, Integer) + CType(Me.lblEntTamaNKB04.Text, Integer) _
                                        + CType(Me.lblEntTamaNKB05.Text, Integer) + CType(Me.lblEntTamaNKB06.Text, Integer) + CType(Me.lblEntTamaNKB07.Text, Integer) + CType(Me.lblEntTamaNKB08.Text, Integer) _
                                        + CType(Me.lblEntTamaNKB09.Text, Integer) + CType(Me.lblEntTamaNKB10.Text, Integer)).ToString("#,##0")
                '打ち放題
                Me.lblEntTimeNKB01.Text = (CType(Me.lblEntTimeNKB01.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB02.Text = (CType(Me.lblEntTimeNKB02.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB03.Text = (CType(Me.lblEntTimeNKB03.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB04.Text = (CType(Me.lblEntTimeNKB04.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB05.Text = (CType(Me.lblEntTimeNKB05.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB06.Text = (CType(Me.lblEntTimeNKB06.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB07.Text = (CType(Me.lblEntTimeNKB07.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB08.Text = (CType(Me.lblEntTimeNKB08.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB09.Text = (CType(Me.lblEntTimeNKB09.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblEntTimeNKB10.Text = (CType(Me.lblEntTimeNKB10.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblEntTimeGokei.Text = (CType(Me.lblEntTimeNKB01.Text, Integer) + CType(Me.lblEntTimeNKB02.Text, Integer) + CType(Me.lblEntTimeNKB03.Text, Integer) + CType(Me.lblEntTimeNKB04.Text, Integer) _
                                        + CType(Me.lblEntTimeNKB05.Text, Integer) + CType(Me.lblEntTimeNKB06.Text, Integer) + CType(Me.lblEntTimeNKB07.Text, Integer) + CType(Me.lblEntTimeNKB08.Text, Integer) _
                                        + CType(Me.lblEntTimeNKB09.Text, Integer) + CType(Me.lblEntTimeNKB10.Text, Integer)).ToString("#,##0")

                '【利用ボール数】
                '1球貸し
                Me.lblBallTamaNKB01.Text = (CType(Me.lblBallTamaNKB01.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB02.Text = (CType(Me.lblBallTamaNKB02.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB03.Text = (CType(Me.lblBallTamaNKB03.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB04.Text = (CType(Me.lblBallTamaNKB04.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB05.Text = (CType(Me.lblBallTamaNKB05.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB06.Text = (CType(Me.lblBallTamaNKB06.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB07.Text = (CType(Me.lblBallTamaNKB07.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB08.Text = (CType(Me.lblBallTamaNKB08.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB09.Text = (CType(Me.lblBallTamaNKB09.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTamaNKB10.Text = (CType(Me.lblBallTamaNKB10.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblBallTamaGokei.Text = (CType(Me.lblBallTamaNKB01.Text, Integer) + CType(Me.lblBallTamaNKB02.Text, Integer) + CType(Me.lblBallTamaNKB03.Text, Integer) + CType(Me.lblBallTamaNKB04.Text, Integer) _
                                        + CType(Me.lblBallTamaNKB05.Text, Integer) + CType(Me.lblBallTamaNKB06.Text, Integer) + CType(Me.lblBallTamaNKB07.Text, Integer) + CType(Me.lblBallTamaNKB08.Text, Integer) _
                                        + CType(Me.lblBallTamaNKB09.Text, Integer) + CType(Me.lblBallTamaNKB10.Text, Integer)).ToString("#,##0")
                '打ち放題
                Me.lblBallTime.Text = (CType(Me.lblBallTime.Text, Integer) / intDayGokei).ToString("#,##0")
                'メンテナンス
                Me.lblBallMente.Text = (CType(Me.lblBallMente.Text, Integer) / intDayGokei).ToString("#,##0")

                '【ｶｰﾄﾞ発行】
                Me.lblCardFHakken.Text = (CType(Me.lblCardFHakken.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblHakkenFKin.Text = (CType(Me.lblHakkenFKin.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblCardHakken.Text = (CType(Me.lblCardHakken.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblHakkenKin.Text = (CType(Me.lblHakkenKin.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblCardHakken2.Text = (CType(Me.lblCardHakken2.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblHakkenKin2.Text = (CType(Me.lblHakkenKin2.Text, Integer) / intDayGokei).ToString("#,##0")

                '【入金】
                'フロント
                Me.lblNyukinFNKB01.Text = (CType(lblNyukinFNKB01.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB02.Text = (CType(lblNyukinFNKB02.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB03.Text = (CType(lblNyukinFNKB03.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB04.Text = (CType(lblNyukinFNKB04.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB05.Text = (CType(lblNyukinFNKB05.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB06.Text = (CType(lblNyukinFNKB06.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB07.Text = (CType(lblNyukinFNKB07.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB08.Text = (CType(lblNyukinFNKB08.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB09.Text = (CType(lblNyukinFNKB09.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinFNKB10.Text = (CType(lblNyukinFNKB10.Text, Integer) / intDayGokei).ToString("#,##0")

                lblNyukinFGokei.Text = (CType(Me.lblNyukinFNKB01.Text, Integer) + CType(Me.lblNyukinFNKB02.Text, Integer) + CType(Me.lblNyukinFNKB03.Text, Integer) + CType(Me.lblNyukinFNKB04.Text, Integer) _
                                    + CType(Me.lblNyukinFNKB05.Text, Integer) + CType(Me.lblNyukinFNKB06.Text, Integer) + CType(Me.lblNyukinFNKB07.Text, Integer) + CType(Me.lblNyukinFNKB08.Text, Integer) _
                                    + CType(Me.lblNyukinFNKB09.Text, Integer) + CType(Me.lblNyukinFNKB10.Text, Integer)).ToString("#,##0")
                '入金機
                Me.lblNyukinMNKB01.Text = (CType(lblNyukinMNKB01.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB02.Text = (CType(lblNyukinMNKB02.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB03.Text = (CType(lblNyukinMNKB03.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB04.Text = (CType(lblNyukinMNKB04.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB05.Text = (CType(lblNyukinMNKB05.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB06.Text = (CType(lblNyukinMNKB06.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB07.Text = (CType(lblNyukinMNKB07.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB08.Text = (CType(lblNyukinMNKB08.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB09.Text = (CType(lblNyukinMNKB09.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinMNKB10.Text = (CType(lblNyukinMNKB10.Text, Integer) / intDayGokei).ToString("#,##0")

                lblNyukinMGokei.Text = (CType(Me.lblNyukinMNKB01.Text, Integer) + CType(Me.lblNyukinMNKB02.Text, Integer) + CType(Me.lblNyukinMNKB03.Text, Integer) + CType(Me.lblNyukinMNKB04.Text, Integer) _
                                    + CType(Me.lblNyukinMNKB05.Text, Integer) + CType(Me.lblNyukinMNKB06.Text, Integer) + CType(Me.lblNyukinMNKB07.Text, Integer) + CType(Me.lblNyukinMNKB08.Text, Integer) _
                                    + CType(Me.lblNyukinMNKB09.Text, Integer) + CType(Me.lblNyukinMNKB10.Text, Integer)).ToString("#,##0")
                '合計
                Me.lblNyukinNKB01Gokei.Text = (CType(Me.lblNyukinFNKB01.Text, Integer) + CType(Me.lblNyukinMNKB01.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB02Gokei.Text = (CType(Me.lblNyukinFNKB02.Text, Integer) + CType(Me.lblNyukinMNKB02.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB03Gokei.Text = (CType(Me.lblNyukinFNKB03.Text, Integer) + CType(Me.lblNyukinMNKB03.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB04Gokei.Text = (CType(Me.lblNyukinFNKB04.Text, Integer) + CType(Me.lblNyukinMNKB04.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB05Gokei.Text = (CType(Me.lblNyukinFNKB05.Text, Integer) + CType(Me.lblNyukinMNKB05.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB06Gokei.Text = (CType(Me.lblNyukinFNKB06.Text, Integer) + CType(Me.lblNyukinMNKB06.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB07Gokei.Text = (CType(Me.lblNyukinFNKB07.Text, Integer) + CType(Me.lblNyukinMNKB07.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB08Gokei.Text = (CType(Me.lblNyukinFNKB08.Text, Integer) + CType(Me.lblNyukinMNKB08.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB09Gokei.Text = (CType(Me.lblNyukinFNKB09.Text, Integer) + CType(Me.lblNyukinMNKB09.Text, Integer)).ToString("#,##0")
                Me.lblNyukinNKB10Gokei.Text = (CType(Me.lblNyukinFNKB10.Text, Integer) + CType(Me.lblNyukinMNKB10.Text, Integer)).ToString("#,##0")

                Me.lblNyukinGokei.Text = (CType(Me.lblNyukinFGokei.Text, Integer) + CType(Me.lblNyukinMGokei.Text, Integer)).ToString("#,##0")

                '【売上】
                '1球貸し
                Me.lblTamaCardGokei.Text = (CType(Me.lblTamaCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblTamaTaxCardGokei.Text = (CType(Me.lblTamaTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                '打ち放題
                Me.lblTimeCardGokei.Text = (CType(Me.lblTimeCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblTimeTaxCardGokei.Text = (CType(Me.lblTimeTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                'ボール料金
                Me.lblBallCardGokei.Text = (CType(Me.lblBallCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblBallTaxCardGokei.Text = (CType(Me.lblBallTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                '商品
                Me.lblShohinCardGokei.Text = (CType(Me.lblShohinCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblShohinTaxCardGokei.Text = (CType(Me.lblShohinTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblShohinCashGokei.Text = (CType(Me.lblShohinCashGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblShohinCreGokei.Text = (CType(Me.lblShohinCreGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblShohinGiftGokei.Text = (CType(Me.lblShohinGiftGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblShohinBankGokei.Text = (CType(Me.lblShohinBankGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblShohinUriGokei.Text = (CType(Me.lblShohinCashGokei.Text, Integer) + CType(Me.lblShohinCreGokei.Text, Integer) + CType(Me.lblShohinGiftGokei.Text, Integer) _
                                    + CType(Me.lblShohinBankGokei.Text, Integer)).ToString("#,##0")
                Me.lblShohinTaxGokei.Text = (CType(Me.lblShohinTaxGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                '入金
                Me.lblNyukinCashGokei.Text = (CType(Me.lblNyukinCashGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinCreGokei.Text = (CType(Me.lblNyukinCreGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinGiftGokei.Text = (CType(Me.lblNyukinGiftGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblNyukinBankGokei.Text = (CType(Me.lblNyukinBankGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblNyukinUriGokei.Text = (CType(Me.lblNyukinCashGokei.Text, Integer) + CType(Me.lblNyukinCreGokei.Text, Integer) + CType(Me.lblNyukinGiftGokei.Text, Integer) _
                                    + CType(Me.lblNyukinBankGokei.Text, Integer)).ToString("#,##0")

                'ｶｰﾄﾞ発行(入金機)
                Me.lblHakkenCardGokei.Text = (CType(Me.lblHakkenCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblHakkenTaxCardGokei.Text = (CType(Me.lblHakkenTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                '年会費
                Me.lblFeeCardGokei.Text = (CType(Me.lblFeeCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblFeeTaxCardGokei.Text = (CType(Me.lblFeeTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblFeeCashGokei.Text = (CType(Me.lblFeeCashGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblFeeCreGokei.Text = (CType(Me.lblFeeCreGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblFeeGiftGokei.Text = (CType(Me.lblFeeGiftGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblFeeBankGokei.Text = (CType(Me.lblFeeBankGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblFeeUriGokei.Text = (CType(Me.lblFeeCashGokei.Text, Integer) + CType(Me.lblFeeCreGokei.Text, Integer) + CType(Me.lblFeeGiftGokei.Text, Integer) _
                                    + CType(Me.lblFeeBankGokei.Text, Integer)).ToString("#,##0")
                Me.lblFeeTaxGokei.Text = (CType(Me.lblFeeTaxGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                'コース
                Me.lblKosuCardGokei.Text = (CType(Me.lblKosuCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblKosuTaxCardGokei.Text = (CType(Me.lblKosuTaxCardGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblKosuCashGokei.Text = (CType(Me.lblKosuCashGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblKosuCreGokei.Text = (CType(Me.lblKosuCreGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblKosuGiftGokei.Text = (CType(Me.lblKosuGiftGokei.Text, Integer) / intDayGokei).ToString("#,##0")
                Me.lblKosuBankGokei.Text = (CType(Me.lblKosuBankGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                Me.lblKosuUriGokei.Text = (CType(Me.lblKosuCashGokei.Text, Integer) + CType(Me.lblKosuCreGokei.Text, Integer) + CType(Me.lblKosuGiftGokei.Text, Integer) _
                                    + CType(Me.lblKosuBankGokei.Text, Integer)).ToString("#,##0")
                Me.lblKosuTaxGokei.Text = (CType(Me.lblKosuTaxGokei.Text, Integer) / intDayGokei).ToString("#,##0")

                '合計
                Me.lblCardGokei.Text = (CType(Me.lblTamaCardGokei.Text, Integer) + CType(Me.lblTimeCardGokei.Text, Integer) + CType(Me.lblBallCardGokei.Text, Integer) _
                                + CType(Me.lblShohinCardGokei.Text, Integer) + CType(Me.lblHakkenCardGokei.Text, Integer) + CType(Me.lblFeeCardGokei.Text, Integer) + CType(Me.lblKosuCardGokei.Text, Integer)).ToString("#,##0")
                Me.lblTaxCardGokei.Text = (CType(Me.lblTamaTaxCardGokei.Text, Integer) + CType(Me.lblTimeTaxCardGokei.Text, Integer) + CType(Me.lblBallTaxCardGokei.Text, Integer) _
                                + CType(Me.lblShohinTaxCardGokei.Text, Integer) + CType(Me.lblHakkenTaxCardGokei.Text, Integer) + CType(Me.lblFeeTaxCardGokei.Text, Integer) + CType(Me.lblKosuTaxCardGokei.Text, Integer)).ToString("#,##0")

                Me.lblCashGokei.Text = (CType(Me.lblShohinCashGokei.Text, Integer) + CType(Me.lblNyukinCashGokei.Text, Integer) + CType(Me.lblFeeCashGokei.Text, Integer)).ToString("#,##0")
                Me.lblCreGokei.Text = (CType(Me.lblShohinCreGokei.Text, Integer) + CType(Me.lblNyukinCreGokei.Text, Integer) + CType(Me.lblFeeCreGokei.Text, Integer)).ToString("#,##0")
                Me.lblGiftGokei.Text = (CType(Me.lblShohinGiftGokei.Text, Integer) + CType(Me.lblNyukinGiftGokei.Text, Integer) + CType(Me.lblFeeGiftGokei.Text, Integer)).ToString("#,##0")
                Me.lblBankGokei.Text = (CType(Me.lblShohinBankGokei.Text, Integer) + CType(Me.lblNyukinBankGokei.Text, Integer) + CType(Me.lblFeeBankGokei.Text, Integer)).ToString("#,##0")

                Me.lblUriGokei.Text = (CType(Me.lblShohinUriGokei.Text, Integer) + CType(Me.lblNyukinUriGokei.Text, Integer) + CType(Me.lblFeeUriGokei.Text, Integer)).ToString("#,##0")
                Me.lblTaxGokei.Text = (CType(Me.lblShohinTaxGokei.Text, Integer) + CType(Me.lblFeeTaxGokei.Text, Integer) + CType(Me.lblKosuTaxGokei.Text, Integer)).ToString("#,##0")
            End If

            '客単価(打席利用)
            If (CType(Me.lblTamaCardGokei.Text, Integer) + CType(Me.lblTimeCardGokei.Text, Integer) + CType(Me.lblBallCardGokei.Text, Integer)) > 0 Then
                Me.lblAvePrice.Text = ((CType(Me.lblTamaCardGokei.Text, Integer) + CType(Me.lblTimeCardGokei.Text, Integer) + CType(Me.lblBallCardGokei.Text, Integer)) _
                                    / CType(Me.lblEntGokei.Text, Integer)).ToString("#,##0")
            Else
                Me.lblAvePrice.Text = "0"
            End If

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
            '一覧
            Me.tspFunc3.Enabled = False
            '印刷
            Me.tspFunc8.Enabled = True
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            '客単価(打席)
            Me.lblAvePrice.Text = "0"

            '【入場者数】

            '種別ラベル
            Me.lblEntNKBNM01.Text = String.Empty : Me.lblEntNKBNM02.Text = String.Empty : Me.lblEntNKBNM03.Text = String.Empty : Me.lblEntNKBNM04.Text = String.Empty : Me.lblEntNKBNM05.Text = String.Empty
            Me.lblEntNKBNM06.Text = String.Empty : Me.lblEntNKBNM07.Text = String.Empty : Me.lblEntNKBNM08.Text = String.Empty : Me.lblEntNKBNM09.Text = String.Empty : Me.lblEntNKBNM10.Text = String.Empty
            '1球貸し
            Me.lblEntTamaNKB01.Text = "0" : Me.lblEntTamaNKB02.Text = "0" : Me.lblEntTamaNKB03.Text = "0" : Me.lblEntTamaNKB04.Text = "0" : Me.lblEntTamaNKB05.Text = "0"
            Me.lblEntTamaNKB06.Text = "0" : Me.lblEntTamaNKB07.Text = "0" : Me.lblEntTamaNKB08.Text = "0" : Me.lblEntTamaNKB09.Text = "0" : Me.lblEntTamaNKB10.Text = "0"
            Me.lblEntTamaGokei.Text = "0"
            '打ち放題
            Me.lblEntTimeNKB01.Text = "0" : Me.lblEntTimeNKB02.Text = "0" : Me.lblEntTimeNKB03.Text = "0" : Me.lblEntTimeNKB04.Text = "0" : Me.lblEntTimeNKB05.Text = "0"
            Me.lblEntTimeNKB06.Text = "0" : Me.lblEntTimeNKB07.Text = "0" : Me.lblEntTimeNKB08.Text = "0" : Me.lblEntTimeNKB09.Text = "0" : Me.lblEntTimeNKB10.Text = "0"
            Me.lblEntTimeGokei.Text = "0"
            '合計
            Me.lblEntNKB01Gokei.Text = "0" : Me.lblEntNKB02Gokei.Text = "0" : Me.lblEntNKB03Gokei.Text = "0" : Me.lblEntNKB04Gokei.Text = "0" : Me.lblEntNKB05Gokei.Text = "0"
            Me.lblEntNKB06Gokei.Text = "0" : Me.lblEntNKB07Gokei.Text = "0" : Me.lblEntNKB08Gokei.Text = "0" : Me.lblEntNKB09Gokei.Text = "0" : Me.lblEntNKB10Gokei.Text = "0"
            Me.lblEntGokei.Text = "0"

            '【利用ボール数】

            '種別ラベル
            Me.lblBallNKBNM01.Text = String.Empty : Me.lblBallNKBNM02.Text = String.Empty : Me.lblBallNKBNM03.Text = String.Empty : Me.lblBallNKBNM04.Text = String.Empty : Me.lblBallNKBNM05.Text = String.Empty
            Me.lblBallNKBNM06.Text = String.Empty : Me.lblBallNKBNM07.Text = String.Empty : Me.lblBallNKBNM08.Text = String.Empty : Me.lblBallNKBNM09.Text = String.Empty : Me.lblBallNKBNM10.Text = String.Empty
            '1球貸し
            Me.lblBallTamaNKB01.Text = "0" : Me.lblBallTamaNKB02.Text = "0" : Me.lblBallTamaNKB03.Text = "0" : Me.lblBallTamaNKB04.Text = "0" : Me.lblBallTamaNKB05.Text = "0"
            Me.lblBallTamaNKB06.Text = "0" : Me.lblBallTamaNKB07.Text = "0" : Me.lblBallTamaNKB08.Text = "0" : Me.lblBallTamaNKB09.Text = "0" : Me.lblBallTamaNKB10.Text = "0"
            Me.lblBallTamaGokei.Text = "0"
            '打ち放題
            Me.lblBallTime.Text = "0"
            'メンテナンス
            Me.lblBallMente.Text = "0"

            '【ｶｰﾄﾞ発券】
            Me.lblCardFHakken.Text = "0"
            Me.lblHakkenFKin.Text = "0"
            Me.lblCardHakken.Text = "0"
            Me.lblHakkenKin.Text = "0"
            Me.lblCardHakken2.Text = "0"
            Me.lblHakkenKin2.Text = "0"

            ' 【サービス入金】
            Me.lblSvcKin.Text = "0"

            '【入金】

            '種別ラベル
            Me.lblNyukinNKBNM01.Text = String.Empty : Me.lblNyukinNKBNM02.Text = String.Empty : Me.lblNyukinNKBNM03.Text = String.Empty : Me.lblNyukinNKBNM04.Text = String.Empty : Me.lblNyukinNKBNM05.Text = String.Empty
            Me.lblNyukinNKBNM06.Text = String.Empty : Me.lblNyukinNKBNM07.Text = String.Empty : Me.lblNyukinNKBNM08.Text = String.Empty : Me.lblNyukinNKBNM09.Text = String.Empty : Me.lblNyukinNKBNM10.Text = String.Empty
            'ﾌﾛﾝﾄ
            Me.lblNyukinFNKB01.Text = "0" : Me.lblNyukinFNKB02.Text = "0" : Me.lblNyukinFNKB03.Text = "0" : Me.lblNyukinFNKB04.Text = "0" : Me.lblNyukinFNKB05.Text = "0"
            Me.lblNyukinFNKB06.Text = "0" : Me.lblNyukinFNKB07.Text = "0" : Me.lblNyukinFNKB08.Text = "0" : Me.lblNyukinFNKB09.Text = "0" : Me.lblNyukinFNKB10.Text = "0"
            Me.lblNyukinFGokei.Text = "0"
            '入金機
            Me.lblNyukinMNKB01.Text = "0" : Me.lblNyukinMNKB02.Text = "0" : Me.lblNyukinMNKB03.Text = "0" : Me.lblNyukinMNKB04.Text = "0" : Me.lblNyukinMNKB05.Text = "0"
            Me.lblNyukinMNKB06.Text = "0" : Me.lblNyukinMNKB07.Text = "0" : Me.lblNyukinMNKB08.Text = "0" : Me.lblNyukinMNKB09.Text = "0" : Me.lblNyukinMNKB10.Text = "0"
            Me.lblNyukinMGokei.Text = "0"
            '簡易型入金機
            Me.lblNyukinM2NKB01.Text = "0" : Me.lblNyukinM2NKB02.Text = "0" : Me.lblNyukinM2NKB03.Text = "0" : Me.lblNyukinM2NKB04.Text = "0" : Me.lblNyukinM2NKB05.Text = "0"
            Me.lblNyukinM2NKB06.Text = "0" : Me.lblNyukinM2NKB07.Text = "0" : Me.lblNyukinM2NKB08.Text = "0" : Me.lblNyukinM2NKB09.Text = "0" : Me.lblNyukinM2NKB10.Text = "0"
            Me.lblNyukinM2Gokei.Text = "0"
            '合計
            Me.lblNyukinNKB01Gokei.Text = "0" : Me.lblNyukinNKB02Gokei.Text = "0" : Me.lblNyukinNKB03Gokei.Text = "0" : Me.lblNyukinNKB04Gokei.Text = "0" : Me.lblNyukinNKB05Gokei.Text = String.Empty
            Me.lblNyukinNKB06Gokei.Text = "0" : Me.lblNyukinNKB07Gokei.Text = "0" : Me.lblNyukinNKB08Gokei.Text = "0" : Me.lblNyukinNKB09Gokei.Text = "0" : Me.lblNyukinNKB10Gokei.Text = "0"
            Me.lblNyukinGokei.Text = "0"

            '【売上】

            '打席ｶｰﾄﾞ
            Me.lblTamaCardGokei.Text = "0"
            Me.lblTimeCardGokei.Text = "0"
            Me.lblBallCardGokei.Text = "0"
            Me.lblShohinCardGokei.Text = "0"
            Me.lblNyukinCardGokei.Text = "0"
            Me.lblHakkenCardGokei.Text = "0"
            Me.lblFeeCardGokei.Text = "0"
            Me.lblKosuCardGokei.Text = "0"
            Me.lblCardGokei.Text = "0"
            '消費税
            Me.lblTamaTaxCardGokei.Text = "0"
            Me.lblTimeTaxCardGokei.Text = "0"
            Me.lblBallTaxCardGokei.Text = "0"
            Me.lblShohinTaxCardGokei.Text = "0"
            Me.lblNyukinTaxCardGokei.Text = "0"
            Me.lblHakkenTaxCardGokei.Text = "0"
            Me.lblFeeTaxCardGokei.Text = "0"
            Me.lblKosuTaxCardGokei.Text = "0"
            Me.lblTaxCardGokei.Text = "0"

            '現金
            Me.lblTamaCashGokei.Text = "0"
            Me.lblTimeCashGokei.Text = "0"
            Me.lblBallCashGokei.Text = "0"
            Me.lblShohinCashGokei.Text = "0"
            Me.lblNyukinCashGokei.Text = "0"
            Me.lblHakkenCashGokei.Text = "0"
            Me.lblFeeCashGokei.Text = "0"
            Me.lblKosuCashGokei.Text = "0"
            Me.lblCashGokei.Text = "0"
            'クレジット
            Me.lblTamaCreGokei.Text = "0"
            Me.lblTimeCreGokei.Text = "0"
            Me.lblBallCreGokei.Text = "0"
            Me.lblShohinCreGokei.Text = "0"
            Me.lblNyukinCreGokei.Text = "0"
            Me.lblHakkenCreGokei.Text = "0"
            Me.lblFeeCreGokei.Text = "0"
            Me.lblKosuCreGokei.Text = "0"
            Me.lblCreGokei.Text = "0"
            '商品券
            Me.lblTamaGiftGokei.Text = "0"
            Me.lblTimeGiftGokei.Text = "0"
            Me.lblBallGiftGokei.Text = "0"
            Me.lblShohinGiftGokei.Text = "0"
            Me.lblNyukinGiftGokei.Text = "0"
            Me.lblHakkenGiftGokei.Text = "0"
            Me.lblFeeGiftGokei.Text = "0"
            Me.lblKosuGiftGokei.Text = "0"
            Me.lblGiftGokei.Text = "0"
            '銀行振込
            Me.lblTamaBankGokei.Text = "0"
            Me.lblTimeBankGokei.Text = "0"
            Me.lblBallBankGokei.Text = "0"
            Me.lblShohinBankGokei.Text = "0"
            Me.lblNyukinBankGokei.Text = "0"
            Me.lblHakkenBankGokei.Text = "0"
            Me.lblFeeBankGokei.Text = "0"
            Me.lblKosuBankGokei.Text = "0"
            Me.lblBankGokei.Text = "0"
            '合計
            Me.lblTamaUriGokei.Text = "0"
            Me.lblTimeUriGokei.Text = "0"
            Me.lblBallUriGokei.Text = "0"
            Me.lblShohinUriGokei.Text = "0"
            Me.lblNyukinUriGokei.Text = "0"
            Me.lblHakkenUriGokei.Text = "0"
            Me.lblFeeUriGokei.Text = "0"
            Me.lblKosuUriGokei.Text = "0"
            Me.lblUriGokei.Text = "0"
            '消費税
            Me.lblTamaTaxGokei.Text = "0"
            Me.lblTimeTaxGokei.Text = "0"
            Me.lblBallTaxGokei.Text = "0"
            Me.lblShohinTaxGokei.Text = "0"
            Me.lblNyukinTaxGokei.Text = "0"
            Me.lblHakkenTaxGokei.Text = "0"
            Me.lblFeeTaxGokei.Text = "0"
            Me.lblKosuTaxGokei.Text = "0"
            Me.lblTaxGokei.Text = "0"


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region



End Class
