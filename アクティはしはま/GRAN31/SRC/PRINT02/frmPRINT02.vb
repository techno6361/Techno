Imports TECHNO.DataBase
Imports Microsoft.Office.Interop

Public Class frmPRINT02

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打席管理帳票"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打席管理帳票"

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
    Private Sub frmPRINT02_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            Me.dgvSEATSMB.RowCount = UIUtility.SYSTEM.SEATSU + 1

            For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                Me.dgvSEATSMB.SetValue("SEATNO", i - 1, i.ToString)
            Next
            Me.dgvSEATSMB.SetValue("SEATNO", UIUtility.SYSTEM.SEATSU, "【合計】")

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
    ''' 検索ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Dim strTIME As String = String.Empty    '使用時間
        Dim dblBALLDOSU As Double = 0           '球数/回数
        Dim dblDOSURITU As Double = 0           '回数率
        Dim dblBALLRITU As Double = 0           '球数率
        Dim intSumBALL As Integer = 0           '全使用球数
        Dim intSumDOSU As Integer = 0           '全使用度数
        Dim intSumTIME As Integer = 0           '全使用時間
        Try


            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            '打席打球数
            For i As Integer = 1 To 100
                If i.Equals(1) Then
                    sql.Append("CASE WHEN SUM(BALL001) IS NULL THEN 0 ELSE SUM(BALL001) END AS BALL001")
                Else
                    sql.Append(",CASE WHEN SUM(BALL" & i.ToString.PadLeft(3, "0"c) & ") IS NULL THEN 0 ELSE SUM(BALL" & i.ToString.PadLeft(3, "0"c) & ") END AS BALL" & i.ToString.PadLeft(3, "0"c))
                End If
            Next
            '打席度数
            For i As Integer = 1 To 100
                sql.Append(",CASE WHEN SUM(DOSU" & i.ToString.PadLeft(3, "0"c) & ") IS NULL THEN 0 ELSE SUM(DOSU" & i.ToString.PadLeft(3, "0"c) & ") END AS DOSU" & i.ToString.PadLeft(3, "0"c))
            Next
            '打席利用時間計
            For i As Integer = 1 To 100
                sql.Append(",CASE WHEN SUM(TIME" & i.ToString.PadLeft(3, "0"c) & ") IS NULL THEN 0 ELSE SUM(TIME" & i.ToString.PadLeft(3, "0"c) & ") END AS TIME" & i.ToString.PadLeft(3, "0"c))
            Next
            sql.Append(" FROM SEATSMB ")
            sql.Append(" WHERE 1=1")
            '営業日付
            sql.Append(" AND REPLACE(SEATDT, '/', '')  >= '" & Me.dtpStaSEATDT.Text.Replace("/", "") & "'")
            sql.Append(" AND REPLACE(SEATDT, '/', '')  <= '" & Me.dtpEndSEATDT.Text.Replace("/", "") & "'")


            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count = 0 Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                '使用球数
                Me.dgvSEATSMB.SetValue("BALL", i - 1, CType(resultDt.Rows(0).Item("BALL" & i.ToString.PadLeft(3, "0"c)).ToString, Integer).ToString("#,##0"))
                intSumBALL += CType(resultDt.Rows(0).Item("BALL" & i.ToString.PadLeft(3, "0"c)).ToString, Integer)
                '使用回数
                Me.dgvSEATSMB.SetValue("DOSU", i - 1, resultDt.Rows(0).Item("DOSU" & i.ToString.PadLeft(3, "0"c)).ToString)
                intSumDOSU += CType(resultDt.Rows(0).Item("DOSU" & i.ToString.PadLeft(3, "0"c)).ToString, Integer)
                '使用時間
                strTIME = (Math.Floor(CType(resultDt.Rows(0).Item("TIME" & i.ToString.PadLeft(3, "0"c)), Double) / 60)).ToString.PadLeft(2, "0"c) & ":" & (CType(resultDt.Rows(0).Item("TIME" & i.ToString.PadLeft(3, "0"c)), Double) Mod 60).ToString.PadLeft(2, "0"c)
                Me.dgvSEATSMB.SetValue("TIME", i - 1, strTIME)
                intSumTIME += CType(resultDt.Rows(0).Item("TIME" & i.ToString.PadLeft(3, "0"c)).ToString, Integer)
                '球数/回数
                If CType(resultDt.Rows(0).Item("BALL" & i.ToString.PadLeft(3, "0"c)).ToString, Integer).Equals(0) Or CType(resultDt.Rows(0).Item("DOSU" & i.ToString.PadLeft(3, "0"c)).ToString, Integer).Equals(0) Then
                    Me.dgvSEATSMB.SetValue("BALLDOSU", i - 1, 0)
                Else
                    dblBALLDOSU = CType(resultDt.Rows(0).Item("BALL" & i.ToString.PadLeft(3, "0"c)).ToString, Double) / CType(resultDt.Rows(0).Item("DOSU" & i.ToString.PadLeft(3, "0"c)).ToString, Double)
                    Me.dgvSEATSMB.SetValue("BALLDOSU", i - 1, ToHalfAdjust(dblBALLDOSU, 2))
                End If

            Next

            '使用球数合計
            Me.dgvSEATSMB.SetValue("BALL", UIUtility.SYSTEM.SEATSU, intSumBALL.ToString("#,##0"))
            '使用度数合計
            Me.dgvSEATSMB.SetValue("DOSU", UIUtility.SYSTEM.SEATSU, intSumDOSU.ToString("#,##0"))
            '使用時間合計
            strTIME = (Math.Floor(CType(intSumTIME, Double) / 60)).ToString.PadLeft(2, "0"c) & ":" & (CType(intSumTIME, Double) Mod 60).ToString.PadLeft(2, "0"c)
            Me.dgvSEATSMB.SetValue("TIME", UIUtility.SYSTEM.SEATSU, strTIME)
            '球数/回数合計
            If intSumBALL.Equals(0) Or intSumDOSU.Equals(0) Then
                Me.dgvSEATSMB.SetValue("BALLDOSU", UIUtility.SYSTEM.SEATSU, 0)
            Else
                dblBALLDOSU = CType(intSumBALL, Double) / CType(intSumDOSU, Double)
                Me.dgvSEATSMB.SetValue("BALLDOSU", UIUtility.SYSTEM.SEATSU, ToHalfAdjust(dblBALLDOSU, 2))
            End If
            '回数率合計
            If intSumDOSU.Equals(0) Then
                Me.dgvSEATSMB.SetValue("DOSURITU", UIUtility.SYSTEM.SEATSU, "0.00%")
            Else
                Me.dgvSEATSMB.SetValue("DOSURITU", UIUtility.SYSTEM.SEATSU, "100.00%")
            End If

            '球数率合計
            If intSumDOSU.Equals(0) Then
                Me.dgvSEATSMB.SetValue("BALLRITU", UIUtility.SYSTEM.SEATSU, "0.00%")
            Else
                Me.dgvSEATSMB.SetValue("BALLRITU", UIUtility.SYSTEM.SEATSU, "100.00%")
            End If

            '度数率
            For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                If CType(resultDt.Rows(0).Item("DOSU" & i.ToString.PadLeft(3, "0"c)).ToString, Integer).Equals(0) Or intSumDOSU.Equals(0) Then
                    Me.dgvSEATSMB.SetValue("DOSURITU", i - 1, "0.00%")
                Else
                    dblDOSURITU = (CType(resultDt.Rows(0).Item("DOSU" & i.ToString.PadLeft(3, "0"c)).ToString, Integer) / intSumDOSU) * 100
                    Me.dgvSEATSMB.SetValue("DOSURITU", i - 1, ToHalfAdjust(dblDOSURITU, 2) & "%")
                End If
            Next
            '球数率
            For i As Integer = 1 To UIUtility.SYSTEM.SEATSU
                If CType(resultDt.Rows(0).Item("BALL" & i.ToString.PadLeft(3, "0"c)).ToString, Integer).Equals(0) Or intSumBALL.Equals(0) Then
                    Me.dgvSEATSMB.SetValue("BALLRITU", i - 1, "0.00%")
                Else
                    dblBALLRITU = (CType(resultDt.Rows(0).Item("BALL" & i.ToString.PadLeft(3, "0"c)).ToString, Integer) / intSumBALL) * 100
                    Me.dgvSEATSMB.SetValue("BALLRITU", i - 1, ToHalfAdjust(dblBALLRITU, 2) & "%")
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Dim dtCSMAST As DataTable
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.btnSearch.PerformClick()

            Dim strReportName As String = "打席管理帳票"
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
            sheet.Cells(1, 8) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString
            '営業日
            sheet.Cells(6, 3) = Me.dtpStaSEATDT.Text & "～" & Me.dtpEndSEATDT.Text

            For i As Integer = 1 To UIUtility.SYSTEM.SEATSU

                '打席番号
                sheet.Cells(10 + (i - 1), 2) = i.ToString
                '使用球数
                sheet.Cells(10 + (i - 1), 3) = Me.dgvSEATSMB.getValue("BALL", i - 1).ToString
                '使用度数
                sheet.Cells(10 + (i - 1), 4) = Me.dgvSEATSMB.getValue("DOSU", i - 1).ToString
                '使用時間
                sheet.Cells(10 + (i - 1), 5) = Me.dgvSEATSMB.getValue("TIME", i - 1).ToString
                '球数/度数
                sheet.Cells(10 + (i - 1), 6) = Me.dgvSEATSMB.getValue("BALLDOSU", i - 1).ToString
                '度数率
                sheet.Cells(10 + (i - 1), 7) = Me.dgvSEATSMB.getValue("DOSURITU", i - 1).ToString
                '球数率
                sheet.Cells(10 + (i - 1), 8) = Me.dgvSEATSMB.getValue("BALLRITU", i - 1).ToString

                If i.Equals(UIUtility.SYSTEM.SEATSU) Then
                    border = sheet.Range("B" & 10 + (i - 1), "H" & 10 + (i - 1)).Borders(Excel.XlBordersIndex.xlEdgeTop)
                    border.LineStyle = Excel.XlLineStyle.xlDash
                    border = sheet.Range("B" & 10 + i, "H" & 10 + i).Borders(Excel.XlBordersIndex.xlEdgeTop)
                    border.LineStyle = Excel.XlLineStyle.xlDouble

                    sheet.Cells(10 + i, 2) = "【合計】"
                    '使用球数合計
                    sheet.Cells(10 + i, 3) = Me.dgvSEATSMB.getValue("BALL", UIUtility.SYSTEM.SEATSU).ToString
                    '使用度数合計
                    sheet.Cells(10 + i, 4) = Me.dgvSEATSMB.getValue("DOSU", UIUtility.SYSTEM.SEATSU).ToString
                    '使用時間合計
                    sheet.Cells(10 + i, 5) = Me.dgvSEATSMB.getValue("TIME", UIUtility.SYSTEM.SEATSU).ToString
                    '球数/度数合計
                    sheet.Cells(10 + i, 6) = Me.dgvSEATSMB.getValue("BALLDOSU", UIUtility.SYSTEM.SEATSU).ToString
                    '度数率合計
                    sheet.Cells(10 + i, 7) = Me.dgvSEATSMB.getValue("DOSURITU", UIUtility.SYSTEM.SEATSU).ToString
                    '球数率合計
                    sheet.Cells(10 + i, 8) = Me.dgvSEATSMB.getValue("BALLRITU", UIUtility.SYSTEM.SEATSU).ToString
                Else
                    If i.Equals(1) Then Continue For
                    border = sheet.Range("B" & 10 + (i - 1), "H" & 10 + (i - 1)).Borders(Excel.XlBordersIndex.xlEdgeTop)
                    border.LineStyle = Excel.XlLineStyle.xlDash
                End If
            Next
            app.Visible = False

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

            '期間コンボボックス
            Me.cmbTerm.SelectedIndex = 0

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' ------------------------------------------------------------------------
    ''' <summary>
    '''     指定した精度の数値に四捨五入します。</summary>
    ''' <param name="dValue">
    '''     丸め対象の倍精度浮動小数点数。</param>
    ''' <param name="iDigits">
    '''     戻り値の有効桁数の精度。</param>
    ''' <returns>
    '''     iDigits に等しい精度の数値に四捨五入された数値。</returns>
    ''' ------------------------------------------------------------------------
    Public Shared Function ToHalfAdjust(ByVal dValue As Double, ByVal iDigits As Integer) As Double
        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return System.Math.Floor((dValue * dCoef) + 0.5) / dCoef
        Else
            Return System.Math.Ceiling((dValue * dCoef) - 0.5) / dCoef
        End If
    End Function

#End Region


End Class
