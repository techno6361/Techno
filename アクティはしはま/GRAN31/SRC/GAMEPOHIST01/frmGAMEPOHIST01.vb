Imports TECHNO.DataBase
Imports Microsoft.Office.Interop

Public Class frmGAMEPOHIST01

#Region "▼宣言部"


#End Region


#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ミニゲームポイント履歴"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ミニゲームポイント履歴"

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
    Private Sub frmGAMEPOHIST01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            '画面初期設定
            Init()

            Me.btnSearch.PerformClick()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Me.Cursor = Cursors.Default
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

            sql.Append(" SELECT")
            sql.Append(" CASE WHEN SUM(OUTCNT)  IS NULL THEN 0 ELSE SUM(OUTCNT)  END AS OUTCNT")
            sql.Append(",CASE WHEN SUM(HIT1CNT) IS NULL THEN 0 ELSE SUM(HIT1CNT) END AS HIT1CNT")
            sql.Append(",CASE WHEN SUM(HIT1PO)  IS NULL THEN 0 ELSE SUM(HIT1PO)  END AS HIT1PO")
            sql.Append(",CASE WHEN SUM(HIT2CNT) IS NULL THEN 0 ELSE SUM(HIT2CNT) END AS HIT2CNT")
            sql.Append(",CASE WHEN SUM(HIT2PO)  IS NULL THEN 0 ELSE SUM(HIT2PO)  END AS HIT2PO")
            sql.Append(",CASE WHEN SUM(HIT3CNT) IS NULL THEN 0 ELSE SUM(HIT3CNT) END AS HIT3CNT")
            sql.Append(",CASE WHEN SUM(HIT3PO)  IS NULL THEN 0 ELSE SUM(HIT3PO)  END AS HIT3PO")
            sql.Append(",CASE WHEN SUM(HIT4CNT) IS NULL THEN 0 ELSE SUM(HIT4CNT) END AS HIT4CNT")
            sql.Append(",CASE WHEN SUM(HIT4PO)  IS NULL THEN 0 ELSE SUM(HIT4PO)  END AS HIT4PO")
            sql.Append(",CASE WHEN SUM(OUTCNT + HIT1CNT + HIT2CNT + HIT3CNT + HIT4CNT)  IS NULL THEN 0 ELSE SUM(OUTCNT + HIT1CNT + HIT2CNT + HIT3CNT + HIT4CNT)  END AS CNTKEI")
            sql.Append(" FROM GAMEPOTRN ")
            sql.Append(" WHERE 1=1")
            '営業日付
            sql.Append(" AND REPLACE(GAMEDT, '/', '')  >= '" & Me.dtpStaSEATDT.Text.Replace("/", "") & "'")
            sql.Append(" AND REPLACE(GAMEDT, '/', '')  <= '" & Me.dtpEndSEATDT.Text.Replace("/", "") & "'")


            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count = 0 Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Me.lblOUTCNT.Text = CType(resultDt.Rows(0).Item("OUTCNT").ToString, Integer).ToString("#,##0")
            Me.lblHIT1CNT.Text = CType(resultDt.Rows(0).Item("HIT1CNT").ToString, Integer).ToString("#,##0")
            Me.lblHIT2CNT.Text = CType(resultDt.Rows(0).Item("HIT2CNT").ToString, Integer).ToString("#,##0")
            Me.lblHIT3CNT.Text = CType(resultDt.Rows(0).Item("HIT3CNT").ToString, Integer).ToString("#,##0")
            Me.lblHIT4CNT.Text = CType(resultDt.Rows(0).Item("HIT4CNT").ToString, Integer).ToString("#,##0")

            Me.lblHIT1PO.Text = CType(resultDt.Rows(0).Item("HIT1PO").ToString, Integer).ToString("#,##0")
            Me.lblHIT2PO.Text = CType(resultDt.Rows(0).Item("HIT2PO").ToString, Integer).ToString("#,##0")
            Me.lblHIT3PO.Text = CType(resultDt.Rows(0).Item("HIT3PO").ToString, Integer).ToString("#,##0")
            Me.lblHIT4PO.Text = CType(resultDt.Rows(0).Item("HIT4PO").ToString, Integer).ToString("#,##0")

            If CType(resultDt.Rows(0).Item("CNTKEI").ToString, Integer) > 0 Then
                Me.lblOUTRATIO.Text = ((CType(Me.lblOUTCNT.Text, Integer) / CType(resultDt.Rows(0).Item("CNTKEI").ToString, Integer)) * 100).ToString("#,##0.0")
                Me.lblHIT1RATIO.Text = ((CType(Me.lblHIT1CNT.Text, Integer) / CType(resultDt.Rows(0).Item("CNTKEI").ToString, Integer)) * 100).ToString("#,##0.0")
                Me.lblHIT2RATIO.Text = ((CType(Me.lblHIT2CNT.Text, Integer) / CType(resultDt.Rows(0).Item("CNTKEI").ToString, Integer)) * 100).ToString("#,##0.0")
                Me.lblHIT3RATIO.Text = ((CType(Me.lblHIT3CNT.Text, Integer) / CType(resultDt.Rows(0).Item("CNTKEI").ToString, Integer)) * 100).ToString("#,##0.0")
                Me.lblHIT4RATIO.Text = ((CType(Me.lblHIT4CNT.Text, Integer) / CType(resultDt.Rows(0).Item("CNTKEI").ToString, Integer)) * 100).ToString("#,##0.0")
            Else
                Me.lblOUTRATIO.Text = "0"
                Me.lblHIT1RATIO.Text = "0"
                Me.lblHIT2RATIO.Text = "0"
                Me.lblHIT3RATIO.Text = "0"
                Me.lblHIT4RATIO.Text = "0"
            End If

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

            Dim strReportName As String = "ミニゲームポイント履歴"
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
            sheet.Cells(1, 15) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString
            '営業日
            sheet.Cells(5, 4) = Me.dtpStaSEATDT.Text & "～" & Me.dtpEndSEATDT.Text

            sheet.Cells(22, 3) = Me.lblOUTCNT.Text
            sheet.Cells(22, 6) = Me.lblHIT1CNT.Text
            sheet.Cells(22, 9) = Me.lblHIT2CNT.Text
            sheet.Cells(22, 12) = Me.lblHIT3CNT.Text
            sheet.Cells(22, 15) = Me.lblHIT4CNT.Text

            sheet.Cells(23, 6) = Me.lblHIT1PO.Text
            sheet.Cells(23, 9) = Me.lblHIT2PO.Text
            sheet.Cells(23, 12) = Me.lblHIT3PO.Text
            sheet.Cells(23, 15) = Me.lblHIT4PO.Text

            sheet.Cells(24, 3) = Me.lblOUTRATIO.Text
            sheet.Cells(24, 6) = Me.lblHIT1RATIO.Text
            sheet.Cells(24, 9) = Me.lblHIT2RATIO.Text
            sheet.Cells(24, 12) = Me.lblHIT3RATIO.Text
            sheet.Cells(24, 15) = Me.lblHIT4RATIO.Text

 
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
    ''' 画面初期表示
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
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
