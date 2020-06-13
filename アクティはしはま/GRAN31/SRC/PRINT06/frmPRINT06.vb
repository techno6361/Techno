Imports TECHNO.DataBase
Imports Microsoft.Office.Interop
Imports System.ComponentModel

Public Class frmPRINT06

#Region "▼宣言部"

    ' 1ページに表示する最大行数(グリッド)
    Const MAX_ROW_NUM = 14

    ' リスト(全体)
    Dim _list As BindingList(Of DUDNTRN_View) = New BindingList(Of DUDNTRN_View)

    ' 現在のページ
    Dim _currentPage As Integer = 0

    Dim _abort As Boolean = False

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品売上管理帳票"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品売上管理帳票"

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
    ''' ESCキーで処理中断
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPRINT06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                _abort = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPRINT04_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            Me.cmbTerm.SelectedIndex = 0

            Me.rbSalesDailyReport.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 営業日付テキストボックス_ValueChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtpSEATDT_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpStaSEATDT.ValueChanged, dtpEndSEATDT.ValueChanged
        Try
            If Me.dtpStaSEATDT.Text.Equals(Me.dtpEndSEATDT.Text) Then

            Else

            End If

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
    ''' 顧客番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNCSNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtNCSNO.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            txtBox.Text = txtBox.Text.PadLeft(8, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
        Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
        Try
            Cursor = Cursors.WaitCursor
            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)
            Me.Refresh()
            Me.Update()

            If Me.rbSalesDailyReport.Checked Then
                ' 商品売上管理帳票(日報)の印刷
                If Not PrintSalesDailyReport(date1, date2) Then
                    Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.pnlStatus.Visible = False
                    Exit Sub
                End If
            Else
                ' 商品売上一覧の印刷
                btnFIND.PerformClick()
                If Not PrintSalesList(_list) Then
                    Me.pnlStatus.Visible = False
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            Me.pnlStatus.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' 一覧クリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSalesList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSalesList.CheckedChanged

        ' グリッドのリセット
        Me.dgvNKNTRN.ClearGrid()
        Dim currentList = New BindingList(Of DUDNTRN_View)
        Me.dgvNKNTRN.DataSource = currentList

        rbDAT1Only.Enabled = True
        rbDAT9Only.Enabled = True
        rbBoth.Enabled = True
        Me.btnFIND.Enabled = True
        Me.btnFIND.BackColor = Color.Orange
        Me.dgvNKNTRN.Enabled = True

        Me.pnlPageButtons.Visible = True
        Me.pnlPageInfo.Visible = True
        Me.dgvNKNTRN.Visible = True

        Me.rdoFee.Enabled = True

    End Sub

    ''' <summary>
    ''' 日報クリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSalesDailyReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSalesDailyReport.CheckedChanged

        ' グリッドのリセット
        Me.dgvNKNTRN.ClearGrid()
        Dim currentList = New BindingList(Of DUDNTRN_View)
        Me.dgvNKNTRN.DataSource = currentList

        rbDAT1Only.Enabled = False
        rbDAT9Only.Enabled = False
        rbBoth.Enabled = False
        Me.btnFIND.Enabled = False
        Me.btnFIND.BackColor = Color.Silver
        Me.dgvNKNTRN.Enabled = False
        AllPaging_Disabled()

        Me.pnlPageButtons.Visible = False
        Me.pnlPageInfo.Visible = False
        Me.dgvNKNTRN.Visible = False


        Me.rdoFee.Enabled = False
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

            ' Enable / Disable
            Me.rbDAT1Only.Enabled = False
            Me.rbDAT9Only.Enabled = False
            Me.rbBoth.Enabled = False
            Me.rbDAT1Only.Checked = True

            ' ページング
            _currentPage = 0
            AllPaging_Disabled()

            Me.pnlPageButtons.Visible = False
            Me.pnlPageInfo.Visible = False
            Me.dgvNKNTRN.Visible = False

            ' pnlStatus
            Me.pnlStatus.Init(Me)

            '商品区分1
            GetShohinList("001")
            '商品区分2
            GetShohinList("002")
            '商品区分3
            GetShohinList("003")
            Me.chkBUNCDB004.Checked = False
            Me.txtNCSNO.Text = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 商品売上管理帳票(日報)の印刷(SQL改善)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintSalesDailyReport(ByVal date1 As String, ByVal date2 As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim dr As DataRow()
        Dim excel = New UIExcel
        Try
            ' *** 帳票の出力 ***

            Dim strReportName As String = "商品売上詳細"
            Dim strOpenReportName As String = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
            Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                             & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                             & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"

            excel.Open(strOpenReportName, 1, False)

            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE)

            ' 出力日
            excel.Cells(2, 39) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString

            ' 対象日付
            excel.Cells(2, 5) = Me.dtpStaSEATDT.Text & "～" & Me.dtpEndSEATDT.Text

            ' *** 分類名の取得 ***
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTB")
            sql.Append(" WHERE BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND BUNCDC = '001'")

            ' SQL実行
            Dim dthinmtb = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dthinmtb.Rows.Count - 1
                Select Case dthinmtb.Rows(i).Item("BUNCDB").ToString
                    Case "001" : excel.Cells(7, 2) = dthinmtb.Rows(i).Item("BUNNMB").ToString
                    Case "002" : excel.Cells(23, 2) = dthinmtb.Rows(i).Item("BUNNMB").ToString
                    Case "003" : excel.Cells(39, 2) = dthinmtb.Rows(i).Item("BUNNMB").ToString
                    Case "004" : excel.Cells(7, 22) = dthinmtb.Rows(i).Item("BUNNMB").ToString
                    Case "005" : excel.Cells(23, 22) = dthinmtb.Rows(i).Item("BUNNMB").ToString
                    Case "006" : excel.Cells(39, 22) = dthinmtb.Rows(i).Item("BUNNMB").ToString
                End Select
            Next

            ' 品名
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTA")
            sql.Append(" WHERE BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")

            ' SQL実行
            Dim dthinmta = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 1 To 16
                dr = dthinmta.Select("BUNCDB = '001' AND HINCD = '" & i.ToString.PadLeft(3, "0"c) & "'")
                If dr.Length > 0 Then
                    excel.Cells(7 + (i - 1), 3) = dr(0).Item("HINNMA").ToString
                    excel.Cells(7 + (i - 1), 9) = dr(0).Item("URIBTK").ToString
                End If
                dr = dthinmta.Select("BUNCDB = '002' AND HINCD = '" & i.ToString.PadLeft(3, "0"c) & "'")
                If dr.Length > 0 Then
                    excel.Cells(23 + (i - 1), 3) = dr(0).Item("HINNMA").ToString
                    excel.Cells(23 + (i - 1), 9) = dr(0).Item("URIBTK").ToString
                End If
                dr = dthinmta.Select("BUNCDB = '003' AND HINCD = '" & i.ToString.PadLeft(3, "0"c) & "'")
                If dr.Length > 0 Then
                    excel.Cells(39 + (i - 1), 3) = dr(0).Item("HINNMA").ToString
                    excel.Cells(39 + (i - 1), 9) = dr(0).Item("URIBTK").ToString
                End If
                dr = dthinmta.Select("BUNCDB = '004' AND HINCD = '" & i.ToString.PadLeft(3, "0"c) & "'")
                If dr.Length > 0 Then
                    excel.Cells(7 + (i - 1), 23) = dr(0).Item("HINNMA").ToString
                    excel.Cells(7 + (i - 1), 29) = dr(0).Item("URIBTK").ToString
                End If
                dr = dthinmta.Select("BUNCDB = '005' AND HINCD = '" & i.ToString.PadLeft(3, "0"c) & "'")
                If dr.Length > 0 Then
                    excel.Cells(23 + (i - 1), 23) = dr(0).Item("HINNMA").ToString
                    excel.Cells(23 + (i - 1), 29) = dr(0).Item("URIBTK").ToString
                End If
                dr = dthinmta.Select("BUNCDB = '006' AND HINCD = '" & i.ToString.PadLeft(3, "0"c) & "'")
                If dr.Length > 0 Then
                    excel.Cells(39 + (i - 1), 23) = dr(0).Item("HINNMA").ToString
                    excel.Cells(39 + (i - 1), 29) = dr(0).Item("URIBTK").ToString
                End If

            Next

            ' 売上

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" BUNCDB")
            sql.Append(",TKTKBN")
            sql.Append(",CPAYKBN")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(",SUM(TKTSU) AS TKTSU")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BUNCDC <> '999'")
            sql.Append(String.Format(" AND UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND UDNDT <= '{0}'", date2))
            sql.Append(" GROUP BY BUNCDB,TKTKBN,CPAYKBN")

            ' SQL実行
            Dim dtdentra = iDatabase.ExecuteRead(sql.ToString())

            ' 年会費
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" BUNCDB")
            sql.Append(",TKTKBN")
            sql.Append(",CPAYKBN")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(",SUM(TKTSU) AS TKTSU")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BUNCDC = '999'")
            sql.Append(String.Format(" AND UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND UDNDT <= '{0}'", date2))
            sql.Append(" GROUP BY BUNCDB,TKTKBN,CPAYKBN")

            ' SQL実行
            Dim dtFee = iDatabase.ExecuteRead(sql.ToString())

            'カード発行料(入金機)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" COUNT(*) AS TKTSU")
            sql.Append(",CASE WHEN SUM(A.PRERT) IS NULL THEN 0 ELSE SUM(A.PRERT) END AS UDNBKN")
            sql.Append(" FROM NKNTRN AS A")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.PRERT > 0")
            sql.Append(String.Format(" AND DENDT >= '{0}'", date1))
            sql.Append(String.Format(" AND DENDT <= '{0}'", date2))

            Dim dtHAKKO As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim intUDNBKN0 As Integer = 0
            Dim intUDNBKN1 As Integer = 0
            Dim intTKTSU As Integer = 0

            '商品区分１
            For i As Integer = 1 To 16
                intUDNBKN0 = 0
                intUDNBKN1 = 0
                intTKTSU = 0
                dr = dtdentra.Select("BUNCDB = '001' AND TKTKBN = '" & i.ToString.PadLeft(3, "0"c) & "'")
                For j As Integer = 0 To dr.Length - 1
                    If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                        intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    Else
                        intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    End If
                    intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
                Next
                '数量
                excel.Cells(7 + (i - 1), 7) = intTKTSU
                '現金
                excel.Cells(7 + (i - 1), 11) = intUDNBKN0
                'その他
                excel.Cells(7 + (i - 1), 14) = intUDNBKN1
                '合計
                excel.Cells(7 + (i - 1), 17) = intUDNBKN0 + intUDNBKN1
            Next

            '商品区分２
            For i As Integer = 1 To 16
                intUDNBKN0 = 0
                intUDNBKN1 = 0
                intTKTSU = 0
                dr = dtdentra.Select("BUNCDB = '002' AND TKTKBN = '" & i.ToString.PadLeft(3, "0"c) & "'")
                For j As Integer = 0 To dr.Length - 1
                    If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                        intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    Else
                        intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    End If
                    intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
                Next
                '数量
                excel.Cells(23 + (i - 1), 7) = intTKTSU
                '現金
                excel.Cells(23 + (i - 1), 11) = intUDNBKN0
                'その他
                excel.Cells(23 + (i - 1), 14) = intUDNBKN1
                '合計
                excel.Cells(23 + (i - 1), 17) = intUDNBKN0 + intUDNBKN1
            Next

            '商品区分３
            For i As Integer = 1 To 16
                intUDNBKN0 = 0
                intUDNBKN1 = 0
                intTKTSU = 0
                dr = dtdentra.Select("BUNCDB = '003' AND TKTKBN = '" & i.ToString.PadLeft(3, "0"c) & "'")
                For j As Integer = 0 To dr.Length - 1
                    If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                        intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    Else
                        intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    End If
                    intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
                Next
                '数量
                excel.Cells(39 + (i - 1), 7) = intTKTSU
                '現金
                excel.Cells(39 + (i - 1), 11) = intUDNBKN0
                'その他
                excel.Cells(39 + (i - 1), 14) = intUDNBKN1
                '合計
                excel.Cells(39 + (i - 1), 17) = intUDNBKN0 + intUDNBKN1
            Next


            '商品区分４
            For i As Integer = 1 To 16
                intUDNBKN0 = 0
                intUDNBKN1 = 0
                intTKTSU = 0
                dr = dtdentra.Select("BUNCDB = '004' AND TKTKBN = '" & i.ToString.PadLeft(3, "0"c) & "'")
                For j As Integer = 0 To dr.Length - 1
                    If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                        intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    Else
                        intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    End If
                    intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
                Next
                '数量
                excel.Cells(7 + (i - 1), 27) = intTKTSU
                '現金
                excel.Cells(7 + (i - 1), 31) = intUDNBKN0
                'その他
                excel.Cells(7 + (i - 1), 34) = intUDNBKN1
                '合計
                excel.Cells(7 + (i - 1), 37) = intUDNBKN0 + intUDNBKN1
            Next

            '商品区分５
            For i As Integer = 1 To 16
                intUDNBKN0 = 0
                intUDNBKN1 = 0
                intTKTSU = 0
                dr = dtdentra.Select("BUNCDB = '005' AND TKTKBN = '" & i.ToString.PadLeft(3, "0"c) & "'")
                For j As Integer = 0 To dr.Length - 1
                    If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                        intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    Else
                        intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    End If
                    intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
                Next
                '数量
                excel.Cells(23 + (i - 1), 27) = intTKTSU
                '現金
                excel.Cells(23 + (i - 1), 31) = intUDNBKN0
                'その他
                excel.Cells(23 + (i - 1), 34) = intUDNBKN1
                '合計
                excel.Cells(23 + (i - 1), 37) = intUDNBKN0 + intUDNBKN1
            Next

            '商品区分６
            For i As Integer = 1 To 16
                intUDNBKN0 = 0
                intUDNBKN1 = 0
                intTKTSU = 0
                dr = dtdentra.Select("BUNCDB = '006' AND TKTKBN = '" & i.ToString.PadLeft(3, "0"c) & "'")
                For j As Integer = 0 To dr.Length - 1
                    If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                        intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    Else
                        intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                    End If
                    intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
                Next
                '数量
                excel.Cells(39 + (i - 1), 27) = intTKTSU
                '現金
                excel.Cells(39 + (i - 1), 31) = intUDNBKN0
                'その他
                excel.Cells(39 + (i - 1), 34) = intUDNBKN1
                '合計
                excel.Cells(39 + (i - 1), 37) = intUDNBKN0 + intUDNBKN1
            Next

            '年会費
            intUDNBKN0 = 0
            intUDNBKN1 = 0
            intTKTSU = 0
            For j As Integer = 0 To dtFee.Rows.Count - 1
                If dtFee.Rows(j).Item("CPAYKBN").ToString.Equals("0") Then
                    intUDNBKN0 += CType(dtFee.Rows(j).Item("UDNBKN").ToString, Integer)
                Else
                    intUDNBKN1 += CType(dtFee.Rows(j).Item("UDNBKN").ToString, Integer)
                End If
                intTKTSU += CType(dtFee.Rows(j).Item("TKTSU").ToString, Integer)
            Next

            '数量
            excel.Cells(55, 7) = intTKTSU
            '現金
            excel.Cells(55, 11) = intUDNBKN0
            'その他
            excel.Cells(55, 14) = intUDNBKN1
            '合計
            excel.Cells(55, 17) = intUDNBKN0 + intUDNBKN1

            '手入力(商品)
            intUDNBKN0 = 0
            intUDNBKN1 = 0
            intTKTSU = 0
            dr = dtdentra.Select("BUNCDB = '007' AND TKTKBN <> '002'")
            For j As Integer = 0 To dr.Length - 1
                If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                    intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                Else
                    intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                End If
                intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
            Next
            '数量
            excel.Cells(55, 27) = intTKTSU
            '現金
            excel.Cells(55, 31) = intUDNBKN0
            'その他
            excel.Cells(55, 34) = intUDNBKN1
            '合計
            excel.Cells(55, 37) = intUDNBKN0 + intUDNBKN1

            '手入力(雑収入)
            intUDNBKN0 = 0
            intUDNBKN1 = 0
            intTKTSU = 0
            dr = dtdentra.Select("BUNCDB = '007' AND TKTKBN = '002'")
            For j As Integer = 0 To dr.Length - 1
                If dr(j).Item("CPAYKBN").ToString.Equals("0") Then
                    intUDNBKN0 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                Else
                    intUDNBKN1 += CType(dr(j).Item("UDNBKN").ToString, Integer)
                End If
                intTKTSU += CType(dr(j).Item("TKTSU").ToString, Integer)
            Next
            '数量
            excel.Cells(56, 27) = intTKTSU
            '現金
            excel.Cells(56, 31) = intUDNBKN0
            'その他
            excel.Cells(56, 34) = intUDNBKN1
            '合計
            excel.Cells(56, 37) = intUDNBKN0 + intUDNBKN1

            'カード発行料(入金機)
            '数量
            excel.Cells(57, 27) = dtHAKKO.Rows(0).Item("TKTSU").ToString
            '単価
            If CType(dtHAKKO.Rows(0).Item("TKTSU").ToString, Integer) > 0 Then
                excel.Cells(57, 29) = CType(dtHAKKO.Rows(0).Item("UDNBKN").ToString, Integer) / CType(dtHAKKO.Rows(0).Item("TKTSU").ToString, Integer)
            Else
                excel.Cells(57, 29) = 0
            End If

            '現金
            excel.Cells(57, 31) = 0
            'その他
            excel.Cells(57, 34) = dtHAKKO.Rows(0).Item("UDNBKN").ToString
            '合計
            excel.Cells(57, 37) = dtHAKKO.Rows(0).Item("UDNBKN").ToString

            '総合計

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CPAYKBN")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND DATKB = '1'")
            'sql.Append(" AND BUNCDC <> '999'")
            sql.Append(String.Format(" AND UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND UDNDT <= '{0}'", date2))
            sql.Append(" GROUP BY CPAYKBN")

            ' SQL実行
            Dim dtdentra2 = iDatabase.ExecuteRead(sql.ToString())
            Dim intCPAYKBN4 As Integer = 0
            For i As Integer = 0 To dtdentra2.Rows.Count - 1
                Select Case dtdentra2.Rows(i).Item("CPAYKBN").ToString
                    Case "0" : excel.Cells(60, 22) = dtdentra2.Rows(i).Item("UDNBKN").ToString
                    Case "1" : excel.Cells(60, 25) = dtdentra2.Rows(i).Item("UDNBKN").ToString
                    Case "2" : excel.Cells(60, 28) = dtdentra2.Rows(i).Item("UDNBKN").ToString
                    Case "3" : excel.Cells(60, 31) = dtdentra2.Rows(i).Item("UDNBKN").ToString
                    Case "4"
                        excel.Cells(60, 34) = CType(dtdentra2.Rows(i).Item("UDNBKN").ToString, Integer) + CType(dtHAKKO.Rows(0).Item("UDNBKN").ToString, Integer)
                        intCPAYKBN4 = 1
                End Select
            Next
            If intCPAYKBN4.Equals(0) Then
                excel.Cells(60, 34) = CType(dtHAKKO.Rows(0).Item("UDNBKN").ToString, Integer)
            End If

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

 

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try
    End Function

    ' ''' <summary>
    ' ''' 商品売上管理帳票(日報)の印刷(SQL改善)
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Function PrintSalesDailyReport(ByVal date1 As String, ByVal date2 As String) As Boolean
    '    Dim sql As New System.Text.StringBuilder
    '    Dim excel = New UIExcel
    '    Try
    '        ' *** 集計 ***

    '        ' *** 分類名の取得 ***
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" *")
    '        sql.Append(" FROM HINMTB")
    '        sql.Append(" WHERE BMNCD = '002'")
    '        sql.Append(" AND BUNCDA = '001'")
    '        sql.Append(" AND BUNCDC = '001'")

    '        ' SQL実行
    '        Dim hinmtb_dt = iDatabase.ExecuteRead(sql.ToString())
    '        If hinmtb_dt.Rows.Count <= 0 Then Return False
    '        Dim hinmtb_query = hinmtb_dt.AsEnumerable

    '        Dim buncdb1 = hinmtb_query.Where(Function(x As DataRow) x("BUNCDB").ToString = "001").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
    '        Dim buncdb2 = hinmtb_query.Where(Function(x As DataRow) x("BUNCDB").ToString = "002").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
    '        Dim buncdb3 = hinmtb_query.Where(Function(x As DataRow) x("BUNCDB").ToString = "003").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()

    '        ' 手入力の中の各支払い区分ごとの合計値
    '        Dim manual_cpaykbns As Integer() = {0, 0, 0, 0}

    '        ' *** 手入力の現金、その他の取得 ***
    '        Dim total_cash = 0
    '        Dim total_other = 0
    '        Dim total_cash_tax = 0
    '        Dim total_other_tax = 0
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" *")
    '        sql.Append(" FROM DENTRA")
    '        sql.Append(" WHERE DATKB = '1'")
    '        sql.Append(String.Format(" AND UDNDT >= '{0}'", date1))
    '        sql.Append(String.Format(" AND UDNDT <= '{0}'", date2))
    '        sql.Append(" AND BMNCD = '002'")
    '        sql.Append(" AND BUNCDA = '001'")
    '        sql.Append(" AND BUNCDC = '001'")

    '        ' SQL実行
    '        Dim dentra_dt = iDatabase.ExecuteRead(sql.ToString())

    '        If dentra_dt.Rows.Count <= 0 Then Return False

    '        Dim dentra_query = From x In dentra_dt.AsEnumerable
    '                           Select New DENTRA With {
    '                               .BUNCDA = x("BUNCDA").ToString,
    '                               .BUNCDB = x("BUNCDB").ToString,
    '                               .BUNCDC = x("BUNCDC").ToString,
    '                               .CPAYKBN = x("CPAYKBN").ToString,
    '                               .UDNBKN = CInt(x("UDNBKN")),
    '                               .UDNZKN = CInt(x("UDNZKN")),
    '                               .TKTKBN = x("TKTKBN").ToString
    '                               }
    '        ' 手入力のみ
    '        Dim manual_query = dentra_query.Where(Function(x) x.BUNCDB = "004")

    '        ' MANNO = NULL and CPAYKBN = 0 の場合のみ、現金精算として扱う
    '        total_cash = manual_query.Where(Function(x) x.CPAYKBN = "0").Select(Function(x) x.UDNBKN).Sum
    '        total_cash_tax = manual_query.Where(Function(x) x.CPAYKBN = "0").Select(Function(x) x.UDNZKN).Sum
    '        total_other = manual_query.Where(Function(x) x.CPAYKBN <> "0").Select(Function(x) x.UDNBKN).Sum
    '        total_other_tax = manual_query.Where(Function(x) x.CPAYKBN <> "0").Select(Function(x) x.UDNZKN).Sum

    '        ' 手入力の料金区分ごとの合計
    '        For i = 0 To 3
    '            Dim cpaykbn = (i + 1).ToString
    '            manual_cpaykbns(i) = manual_query.Where(Function(x) x.CPAYKBN = cpaykbn).Select(Function(x) x.UDNBKN).Sum
    '        Next

    '        ' *** 各分類ごとの商品名の出力 ****
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" *")
    '        sql.Append(" FROM HINMTA")
    '        sql.Append(" WHERE BMNCD = '002'")            

    '        ' SQL実行
    '        Dim hinmta_dt = iDatabase.ExecuteRead(sql.ToString())

    '        If hinmta_dt.Rows.Count <= 0 Then Return False

    '        Dim hin_query = From x In hinmta_dt.AsEnumerable
    '                        Select New HINMTA With {
    '                            .BUNCDB = x("BUNCDB").ToString,
    '                            .HINCD = x("HINCD").ToString,
    '                            .HINNMA = x("HINNMA").ToString
    '                            }

    '        ' *** 支払い区分ごとに集計 ***
    '        Dim pay_other_totals As Integer() = {0, 0, 0, 0}
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" *")
    '        sql.Append(" FROM DUDNTRN")
    '        sql.Append(" WHERE DATKB = '1'")
    '        sql.Append(String.Format(" AND UDNDT >= '{0}'", date1))
    '        sql.Append(String.Format(" AND UDNDT <= '{0}'", date2))
    '        sql.Append(" AND BMNCD = '002'")
    '        sql.Append(" AND BUNCDA = '001'")
    '        sql.Append(" AND BUNCDB = '001'")
    '        sql.Append(" AND BUNCDC = '001'")

    '        ' SQL実行
    '        Dim dudntrn_dt = iDatabase.ExecuteRead(sql.ToString())

    '        If dudntrn_dt.Rows.Count <= 0 Then Return False

    '        Dim dudntrn_query = From x In dudntrn_dt.AsEnumerable
    '                            Select New With {
    '                                .CPAYKBN = x("CPAYKBN").ToString,
    '                                .UDNBKN = CInt(x("UDNBKN"))
    '                            }

    '        ' カード払い
    '        Dim bun1_query = dudntrn_query.Where(Function(x) x.CPAYKBN = "1")

    '        ' 商品券
    '        Dim bun2_query = dudntrn_query.Where(Function(x) x.CPAYKBN = "2")

    '        ' 銀行振込
    '        Dim bun3_query = dudntrn_query.Where(Function(x) x.CPAYKBN = "3")

    '        ' 打席カード
    '        Dim bun4_query = dudntrn_query.Where(Function(x) x.CPAYKBN = "4")

    '        pay_other_totals(0) = bun1_query.Select(Function(x) x.UDNBKN).Sum
    '        pay_other_totals(1) = bun2_query.Select(Function(x) x.UDNBKN).Sum
    '        pay_other_totals(2) = bun3_query.Select(Function(x) x.UDNBKN).Sum
    '        pay_other_totals(3) = bun4_query.Select(Function(x) x.UDNBKN).Sum

    '        ' データチェック
    '        If Not hinmtb_query.Any Then Return False
    '        If Not dentra_query.Any Then Return False
    '        If Not hin_query.Any Then Return False
    '        If Not dudntrn_query.Any Then Return False

    '        ' *** 帳票の出力 ***

    '        Dim strReportName As String = "商品売上管理帳票(日報)"
    '        Dim strOpenReportName As String = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
    '        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
    '                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
    '                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"

    '        excel.Open(strOpenReportName, 1, False)

    '        Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE)

    '        ' 出力日
    '        excel.Cells(1, 14) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString

    '        ' 対象日付
    '        excel.Cells(4, 11) = "対象日付:" & Me.dtpStaSEATDT.Text & "～" & Me.dtpEndSEATDT.Text

    '        ' 分類           
    '        excel.Cells(7, 2) = buncdb1
    '        excel.Cells(8, 2) = buncdb2
    '        excel.Cells(9, 2) = buncdb3

    '        ' 手入力
    '        excel.Cells(10, 5) = total_cash
    '        excel.Cells(10, 7) = total_other

    '        ' 分類名の表示
    '        excel.Cells(14, 2) = buncdb1
    '        excel.Cells(14, 6) = buncdb2
    '        excel.Cells(14, 10) = buncdb3

    '        ' 分類ごとに集計
    '        Dim total1 = PrintBunTotal(dentra_query, hin_query, "001", excel, 2)
    '        Dim total2 = PrintBunTotal(dentra_query, hin_query, "002", excel, 6)
    '        Dim total3 = PrintBunTotal(dentra_query, hin_query, "003", excel, 10)

    '        ' その他（内訳）
    '        excel.Cells(38, 5) = pay_other_totals(0) - manual_cpaykbns(0)
    '        excel.Cells(39, 5) = pay_other_totals(1) - manual_cpaykbns(1)
    '        excel.Cells(40, 5) = pay_other_totals(2) - manual_cpaykbns(2)
    '        excel.Cells(41, 5) = pay_other_totals(3) - manual_cpaykbns(3)

    '        ' 内税の出力
    '        excel.Cells(12, 5) = total1.CashTaxTotal + total2.CashTaxTotal + total3.CashTaxTotal + total_cash_tax
    '        excel.Cells(12, 7) = total1.OtherTaxTotal + total2.OtherTaxTotal + total3.OtherTaxTotal + total_other_tax

    '        excel.Cells(33, 3) = total1.CashTaxTotal
    '        excel.Cells(33, 4) = total1.OtherTaxTotal
    '        excel.Cells(33, 7) = total2.CashTaxTotal
    '        excel.Cells(33, 8) = total2.OtherTaxTotal
    '        excel.Cells(33, 11) = total3.CashTaxTotal
    '        excel.Cells(33, 12) = total3.OtherTaxTotal

    '        'ファイル保存
    '        excel.SaveAs(strSaveReportName, True)

    '        Return True

    '    Catch ex As Exception
    '        excel.Dispose()
    '        Throw ex
    '    End Try
    'End Function

    ''' <summary>
    ''' 分類ごとに集計してシートに書き出す
    ''' </summary>
    ''' <param name="query"></param>
    ''' <param name="buncdb"></param>
    ''' <param name="excel"></param>
    ''' <param name="col"></param>
    ''' <remarks></remarks>
    Private Function PrintBunTotal(ByRef query As System.Data.EnumerableRowCollection(Of DENTRA), _
                              ByRef hin_query As System.Data.EnumerableRowCollection(Of HINMTA), _
                              ByVal buncdb As String, ByRef excel As UIExcel, ByVal col As Integer) As BunTotalData

        Dim result = New BunTotalData

        Try
            ' *** 分類nの計算 ***
            Dim bun_query = query.Where(Function(x) x.BUNCDB = buncdb And Not String.IsNullOrEmpty(x.TKTKBN))

            Dim bun_totals = New List(Of BunTotalData)

            If bun_query.Count > 0 Then

                ' 分類nの現金精算クエリ
                Dim query_cash = bun_query.Where(Function(x) x.CPAYKBN = "0")

                ' 分類nのその他の精算クエリ
                Dim query_other = bun_query.Where(Function(x) x.CPAYKBN <> "0")

                ' 商品名
                For i = 1 To 16

                    Dim item = New BunTotalData
                    Dim idx = i

                    ' 商品名
                    If hin_query.Count > 0 Then
                        Dim hincd_query = hin_query.Where(Function(x) x.BUNCDB = buncdb And x.HINCD = idx.ToString.PadLeft(3, "0"c))
                        If hincd_query.Any Then
                            item.Name = hincd_query.First.HINNMA
                        End If
                    End If

                    ' 現金精算の集計
                    If query_cash.Any Then
                        Dim tmp = query_cash.Where(Function(x) x.TKTKBN = idx.ToString.PadLeft(3, "0"c))
                        item.CashTotal = tmp.Select(Function(x) x.UDNBKN).Sum
                        item.CashTaxTotal = tmp.Select(Function(x) x.UDNZKN).Sum
                    End If

                    ' その他の集計
                    If query_other.Any Then
                        Dim tmp = query_other.Where(Function(x) x.TKTKBN = idx.ToString.PadLeft(3, "0"c))
                        item.OtherTotal = tmp.Select(Function(x) x.UDNBKN).Sum
                        item.OtherTaxTotal = tmp.Select(Function(x) x.UDNZKN).Sum
                    End If

                    bun_totals.Add(item)

                Next
            End If

            ' 出力
            Dim j = 0
            For Each item In bun_totals
                If Not item.Name Is Nothing Then
                    excel.Cells(16 + j, col) = item.Name
                    excel.Cells(16 + j, col + 1) = item.CashTotal
                    excel.Cells(16 + j, col + 2) = item.OtherTotal

                    ' 内税の集計
                    result.CashTaxTotal += item.CashTaxTotal
                    result.OtherTaxTotal += item.OtherTaxTotal

                End If
                j += 1
            Next

            Return result

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 商品売上一覧の印刷(SQL改善)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintSalesList(ByVal list As BindingList(Of DUDNTRN_View)) As Boolean

        ' *** 帳票の出力 ***
        Dim strReportName As String = "商品売上一覧"
        Dim strOpenReportName As String = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"

        Dim excel = New UIExcel

        Try
            If Not list.Any Then Return False

            ' *** テーブルの取得 ***

            excel.Open(strOpenReportName, 1, False)
            excel.SetLineRange(2, 12)

            ' 最大件数
            Dim max_count = _list.Count

            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE_ESC, max_count)

            ' 処理件数
            Dim real_index = 1

            ' 開始行数
            Dim header_margin = 7
            Dim row_index = header_margin

            ' 罫線を入れる行数
            Dim max_per_page = 31
            Dim offset = max_per_page + 1

            If rdoFee.Checked Then
                excel.Cells(2, 2) = "年会費一覧"
            End If

            ' 出力日
            excel.Cells(1, 11) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 総合計
            Dim total_udnbkn = 0
            Dim total_udnzkn = 0
            Dim total_tktsu = 0
            Dim total_point = 0
            Dim total_nyukn = 0
            Dim total_turikn = 0

            ' 一日の合計
            Dim day_udnbkn = 0
            Dim day_udnzkn = 0
            Dim day_tktsu = 0
            Dim day_point = 0
            Dim day_nyukn = 0
            Dim day_turikn = 0

            ResetAbort()

            Dim den_index = 1
            Dim oldDt = ""

            ' 日付、伝票番号でグループ化
            Dim group_list = list.GroupBy(Function(x) New With {Key x.UDNDT1, Key x.UDNNO, Key x.HOSTNAME})

            For Each g_row In group_list

                ' 中断
                If IsAbort() Then Exit For

                Dim row = g_row.First

                If oldDt <> row.UDNDT1 Then

                    ' 日付変更初回時
                    If den_index = 1 Then
                        oldDt = row.UDNDT1
                        excel.Cells(row_index, 3) = row.UDNDT1
                        excel.Cells(row_index + 1, 3) = row.UDNDT2
                    Else
                        ' 一日の集計

                        oldDt = row.UDNDT1

                        ' 罫線
                        If row_index - header_margin + 1 = offset Then
                            excel.DrawBoldLine(row_index)
                            offset += max_per_page
                        Else
                            'excel.DrawDoubleLine(row_index)
                        End If

                        For i As Integer = 6 To 12
                            excel.FontBold(row_index, i)
                        Next

                        ' 合計
                        excel.Cells(row_index, 6) = "【合　計】"

                        ' 数量
                        excel.Cells(row_index, 7) = day_tktsu.ToString("#,0")

                        ' 金額
                        excel.Cells(row_index, 8) = day_udnbkn.ToString("#,0")

                        ' 消費税
                        excel.Cells(row_index, 9) = day_udnzkn.ToString("#,0")

                        ' 加算ポイント
                        excel.Cells(row_index, 10) = day_point.ToString("#,0")

                        ' 入金
                        excel.Cells(row_index, 11) = day_nyukn.ToString("#,0")

                        ' 釣り金
                        excel.Cells(row_index, 12) = day_turikn.ToString("#,0")

                        ' 総合計の計算
                        total_udnbkn += day_udnbkn
                        total_udnzkn += day_udnzkn
                        total_point += day_point
                        total_nyukn += day_nyukn
                        total_turikn += day_turikn
                        total_tktsu += day_tktsu

                        ' 一日の合計のリセット
                        day_udnbkn = 0
                        day_udnzkn = 0
                        day_point = 0
                        day_nyukn = 0
                        day_turikn = 0
                        day_tktsu = 0

                        ' 伝票番号リセット
                        den_index = 1
                        row_index += 1

                        ' 罫線(通常)
                        excel.DrawBasicLine(row_index)

                        ' 罫線
                        If row_index - header_margin + 1 = offset Then
                            excel.DrawBoldLine(row_index)
                            offset += max_per_page
                        End If

                        ' 日付
                        excel.Cells(row_index, 3) = row.UDNDT1
                        excel.Cells(row_index + 1, 3) = row.UDNDT2

                    End If

                Else
                    excel.Cells(row_index, 3) = row.UDNDT2
                End If

                ' 伝票カウント
                excel.Cells(row_index, 2) = den_index

                ' 消
                excel.Cells(row_index, 4) = row.DATKB

                ' 顧客番号
                excel.Cells(row_index, 5) = row.MANNO

                ' 担当者
                excel.Cells(row_index + 1, 5) = row.STFNAME

                ' 顧客名
                excel.Cells(row_index, 6) = row.MANNM

                ' 伝票番号(UDNNO)
                excel.Cells(row_index, 9) = String.Format("【{0}】", row.UDNNO)

                ' ドロワNo
                excel.Cells(row_index, 10) = String.Format("【{0}】", row.HOSTNAME)

                ' ドロワチェック
                excel.Cells(row_index, 11) = row.DRAKBN

                ' 支払区分
                excel.Cells(row_index, 12) = row.CPAYKBN

                ' *** 伝票合計 ***

                row_index += 1

                ' 罫線
                If row_index - header_margin + 1 = offset Then
                    excel.DrawBoldLine(row_index)
                    offset += max_per_page
                End If


                excel.Cells(row_index, 12) = row.CPAYKBN

                ' 伝票合計
                excel.Cells(row_index, 6) = "伝票合計"
                excel.FontBold(row_index, 6)

                ' 合計金額
                excel.Cells(row_index, 8) = row.UDNBKN.ToString("#,0")
                excel.FontBold(row_index, 8)
                day_udnbkn += row.UDNBKN

                ' 合計消費税
                excel.Cells(row_index, 9) = row.UDNZKN.ToString("#,0")
                excel.FontBold(row_index, 9)
                day_udnzkn += row.UDNZKN

                ' 加算ポイント
                excel.Cells(row_index, 10) = row.POINT.ToString("#,0")
                day_point += row.POINT

                ' 預かり金
                excel.Cells(row_index, 11) = row.NYUKN.ToString("#,0")
                day_nyukn += row.NYUKN

                ' 釣金
                excel.Cells(row_index, 12) = row.TURIKN.ToString("#,0")
                day_turikn += row.TURIKN

                ' *** 伝票内容(詳細) ***

                Dim udndt = row.UDNDT1
                Dim udnno = row.UDNNO

                Dim dentra_query = g_row.Where(Function(x) x.UDNDT1 = udndt And x.UDNNO = udnno)

                For Each d_row In dentra_query

                    Me.pnlStatus.Count = real_index

                    row_index += 1
                    real_index += 1

                    ' 罫線
                    If row_index - header_margin + 1 = offset Then
                        excel.DrawBoldLine(row_index)
                        offset += max_per_page
                    End If


                    ' 商品名
                    excel.Cells(row_index, 6) = " ･" & d_row.B_TKTNMA

                    ' 数量
                    excel.Cells(row_index, 7) = d_row.B_TKTSU.ToString("#,0")
                    day_tktsu += row.B_TKTSU

                    ' 金額
                    excel.Cells(row_index, 8) = d_row.B_UDNBKN.ToString("#,0")

                    ' 消費税
                    excel.Cells(row_index, 9) = d_row.B_UDNZKN.ToString("#,0")

                    ' 罫線(破線)
                    'excel.DrawDashLine(row_index + 1)
                Next

                row_index += 1
                den_index += 1

                ' 罫線
                If row_index - header_margin + 1 = offset Then
                    excel.DrawBoldLine(row_index)
                    offset += max_per_page
                End If

            Next

            ' *** 最終日の集計 ***

            Dim last_query = _list.GroupBy(Function(x) x.UDNDT1).Last

            Dim last_udnno_query = last_query.GroupBy(Function(x) x.UDNNO)
            day_point = 0
            day_nyukn = 0
            day_turikn = 0
            For Each g In last_udnno_query
                Dim gg = g
                day_point += gg.First.POINT
                day_nyukn += gg.First.NYUKN
                day_turikn += gg.First.TURIKN
            Next

            If last_query.Any Then

                Dim row = last_query.First

                ' 罫線
                If row_index - header_margin + 1 = offset Then
                    excel.DrawBoldLine(row_index)
                    offset += max_per_page
                Else
                    'excel.DrawDoubleLine(row_index)
                End If


                For i As Integer = 6 To 12
                    excel.FontBold(row_index, i)
                Next

                ' 合計
                excel.Cells(row_index, 6) = "【合　計】"

                ' 数量
                Dim tktsu = last_query.Select(Function(x) x.B_TKTSU).Sum
                excel.Cells(row_index, 7) = tktsu.ToString("#,0")

                ' 金額
                Dim udnbkn = last_query.Select(Function(x) x.B_UDNBKN).Sum
                excel.Cells(row_index, 8) = udnbkn.ToString("#,0")

                ' 消費税
                Dim udnzkn = last_query.Select(Function(x) x.B_UDNZKN).Sum
                excel.Cells(row_index, 9) = udnzkn.ToString("#,0")

                ' 加算ポイント
                excel.Cells(row_index, 10) = day_point.ToString("#,0")

                ' 入金
                excel.Cells(row_index, 11) = day_nyukn.ToString("#,0")

                ' 釣り金
                excel.Cells(row_index, 12) = day_turikn.ToString("#,0")

                ' 総合計の計算
                total_tktsu += tktsu
                total_udnbkn += udnbkn
                total_udnzkn += udnzkn
                total_point += day_point
                total_nyukn += day_nyukn
                total_turikn += day_turikn

                row_index += 1

                ' 罫線
                If row_index - header_margin + 1 = offset Then
                    excel.DrawBoldLine(row_index)
                    offset += max_per_page
                    row_index += 1
                End If

            End If

            ' 総合計の表示
            For i As Integer = 3 To 12
                excel.FontBold(row_index, i)
            Next

            row_index += 0
            excel.DrawDoubleLine(row_index)
            excel.Cells(row_index, 3) = dtpStaSEATDT.Value.ToString("yyyy/MM/dd")
            excel.Cells(row_index, 4) = "～"
            excel.Cells(row_index, 5) = dtpEndSEATDT.Value.ToString("yyyy/MM/dd")
            excel.Cells(row_index, 6) = "【総　合　計】"
            excel.Cells(row_index, 7) = total_tktsu.ToString("#,0")
            excel.Cells(row_index, 8) = total_udnbkn.ToString("#,0")
            excel.Cells(row_index, 9) = total_udnzkn.ToString("#,0")
            excel.Cells(row_index, 10) = total_point.ToString("#,0")
            excel.Cells(row_index, 11) = total_nyukn.ToString("#,0")
            excel.Cells(row_index, 12) = total_turikn.ToString("#,0")

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 商品売上一覧の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDUDNTRN(ByVal date1 As String, ByVal date2 As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 初期化
            _currentPage = 0
            _list.Clear()

            ' *** テーブルの取得 ***

            ' 売上トランの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.DATKB AS DATKB, A.UDNDT AS UDNDT, A.UDNNO AS UDNNO, A.INSDTMSTR AS INSDTMSTR, A.MANNO AS MANNO,")
            sql.Append(" A.HOSTNAME AS HOSTNAME, A.DRAKBN AS DRAKBN, A.CPAYKBN AS CPAYKBN, A.TKTSU AS TKTSU, A.UDNBKN AS UDNBKN,")
            sql.Append(" A.UDNZKN AS UDNZKN, A.POINT AS POINT, A.NYUKN AS NYUKN, A.TURIKN AS TURIKN,")
            sql.Append(" A.STFNAME,")
            sql.Append(" B.LINNO AS B_LINNO, B.TKTNMA AS B_TKTNMA, B.TKTSU AS B_TKTSU, B.UDNBKN AS B_UDNBKN, B.UDNZKN AS B_UDNZKN,")
            sql.Append(" C.CCSNAME AS C_MANNM, D.CKBNAME AS D_CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" JOIN DENTRA AS B ON A.UDNDT = B.UDNDT AND A.UDNNO = B.UDNNO AND A.HOSTNAME = B.HOSTNAME")
            sql.Append(" LEFT JOIN CSMAST AS C ON A.MANNO = LTRIM(TO_CHAR(C.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS D ON A.KSBKB = ''||D.NKBNO")
            sql.Append(" WHERE A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '001'")
            If Me.rdoFee.Checked Then
                sql.Append(" AND A.BUNCDC = '999'")
                sql.Append(" AND A.DATKB = '1'")
                sql.Append(" AND B.DATKB = '1'")
            Else
                sql.Append(" AND A.BUNCDC = '001'")
            End If
            sql.Append(String.Format(" AND A.UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND A.UDNDT <= '{0}'", date2))

            ' 取り消し区分
            If rbDAT1Only.Checked Then
                sql.Append(" AND A.DATKB = '1'")
                sql.Append(" AND B.DATKB = '1'")
            End If
            If rbDAT9Only.Checked Then
                sql.Append(" AND A.DATKB = '9'")
                sql.Append(" AND B.DATKB = '9'")
            End If
            '*** Sta Add 2019/06/18 Kitahara
            Dim i As Integer = 0
            '商品区分1
            Dim strBUNCDB001 As String = "'000'"
            For Each i In Me.clbKBN001.CheckedIndices
                strBUNCDB001 &= ","
                strBUNCDB001 &= "'" & (i + 1).ToString.PadLeft(3, "0"c) & "'"
            Next
            i = 0
            '商品区分2
            Dim strBUNCDB002 As String = "'000'"
            For Each i In Me.clbKBN002.CheckedIndices
                strBUNCDB002 &= ","
                strBUNCDB002 &= "'" & (i + 1).ToString.PadLeft(3, "0"c) & "'"
            Next
            i = 0
            '商品区分3
            Dim strBUNCDB003 As String = "'000'"
            For Each i In Me.clbKBN003.CheckedIndices
                strBUNCDB003 &= ","
                strBUNCDB003 &= "'" & (i + 1).ToString.PadLeft(3, "0"c) & "'"
            Next

            sql.Append(" AND (1=1 ")
            Dim strAndOr As String = "AND"
            '商品区分1
            If Not strBUNCDB001.Equals("'000'") Then
                sql.Append(strAndOr & " (B.BUNCDB = '001' AND B.TKTKBN IN (" & strBUNCDB001 & "))")
                strAndOr = "OR"
            End If
            '商品区分2
            If Not strBUNCDB002.Equals("'000'") Then
                sql.Append(strAndOr & " (B.BUNCDB = '002' AND B.TKTKBN IN (" & strBUNCDB002 & "))")
                strAndOr = "OR"
            End If
            '商品区分3
            If Not strBUNCDB003.Equals("'000'") Then
                sql.Append(strAndOr & " (B.BUNCDB = '003' AND B.TKTKBN IN (" & strBUNCDB003 & "))")
                strAndOr = "OR"
            End If
            '手入力
            If Me.chkBUNCDB004.Checked Then
                sql.Append(strAndOr & " B.BUNCDB = '004'")
            End If
            sql.Append(")")
            '顧客番号
            If Not String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                sql.Append(" AND A.MANNO = '" & Me.txtNCSNO.Text.PadLeft(8, "0"c) & "'")
            End If
            '*** End Add 2019/06/18 Kitahara

            sql.Append(" ORDER BY A.UDNDT, A.UDNNO DESC, A.HOSTNAME, A.MANNO")

            ' SQL実行
            Dim join_dt = iDatabase.ExecuteRead(sql.ToString())

            If join_dt.Rows.Count <= 0 Then Return False

            Dim query = join_dt.AsEnumerable

            Dim join_query = From x In join_dt.AsEnumerable
                             Select New With {
                                 .DATKB = x("DATKB").ToString,
                                 .UDNDT = x("UDNDT").ToString,
                                 .UDNNO = CInt(x("UDNNO")),
                                 .INSDTMSTR = x("INSDTMSTR").ToString,
                                 .MANNO = x("MANNO").ToString,
                                 .C_MANNM = x("C_MANNM").ToString,
                                 .D_CKBNAME = x("D_CKBNAME").ToString,
                                 .HOSTNAME = x("HOSTNAME").ToString,
                                 .DRAKBN = x("DRAKBN").ToString,
                                 .CPAYKBN = x("CPAYKBN").ToString,
                                 .TKTSU = CInt(x("TKTSU")),
                                 .UDNBKN = CInt(x("UDNBKN")),
                                 .UDNZKN = CInt(x("UDNZKN")),
                                 .POINT = CInt(x("POINT")),
                                 .NYUKN = CInt(x("NYUKN")),
                                 .TURIKN = CInt(x("TURIKN")),
                                 .B_LINNO = CInt(x("B_LINNO")),
                                 .B_TKTNMA = x("B_TKTNMA").ToString,
                                 .B_TKTSU = CInt(x("B_TKTSU")),
                                 .B_UDNBKN = CInt(x("B_UDNBKN")),
                                 .B_UDNZKN = CInt(x("B_UDNZKN")),
                                 .STFNAME = x("STFNAME").ToString
                                 }

            ' UDNDT, UDNNO をグループ化したクエリ
            Dim udndt_query = join_query.GroupBy(Function(x) New With {Key x.UDNDT, Key x.UDNNO, Key x.MANNO})

            ' 最大件数
            Dim max_count = 1

            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD_ESC, max_count)

            ResetAbort()

            For Each u_row In udndt_query

                ' 中断
                If IsAbort(False) Then Exit For

                Dim udndt = u_row.First.UDNDT
                Dim udnno = u_row.First.UDNNO
                Dim manno = u_row.First.MANNO
                Dim hostname = u_row.First.HOSTNAME
                Dim insdtmstr = u_row.First.INSDTMSTR

                Dim sum_query = u_row.Where(Function(x) x.UDNDT = udndt And x.UDNNO = udnno _
                                                And x.INSDTMSTR = insdtmstr _
                                                And x.MANNO = manno And x.HOSTNAME = hostname)

                Dim total_udnbkn = sum_query.Select(Function(x) x.B_UDNBKN).Sum
                Dim total_udnzkn = sum_query.Select(Function(x) x.B_UDNZKN).Sum
                Dim total_tktsu = sum_query.Select(Function(x) x.B_TKTSU).Sum

                Dim current_dudntrn_query = u_row

                For Each row In current_dudntrn_query

                    ' 中断
                    If IsAbort(False) Then Exit For

                    ' *** 売上内容 ***

                    Dim data = New DUDNTRN_View

                    ' 処理時刻
                    data.UDNDT1 = DateTime.Parse(row.INSDTMSTR).ToString("yyyy/MM/dd")
                    data.UDNDT2 = DateTime.Parse(row.INSDTMSTR).ToString("HH:mm:ss")

                    ' 消
                    If row.DATKB = "9" Then
                        data.DATKB = "消"
                    End If

                    ' 顧客番号
                    data.MANNO = row.MANNO

                    ' 顧客名
                    If String.IsNullOrEmpty(row.D_CKBNAME) Then
                        data.MANNM = row.C_MANNM
                    Else
                        data.MANNM = String.Format("{0}({1})", row.C_MANNM, row.D_CKBNAME)
                    End If

                    ' 伝票番号(UDNNO)
                    data.UDNNO = row.UDNNO.ToString.PadLeft(4, "0"c)

                    ' ドロワNo
                    data.HOSTNAME = row.HOSTNAME

                    ' ドロワチェック
                    Dim drastr = ""
                    If row.DRAKBN = "1" Then
                        drastr = "済"
                    End If
                    data.DRAKBN = drastr

                    ' 支払区分
                    Dim cpaystr = "現金"
                    Select Case row.CPAYKBN
                        Case "1"
                            cpaystr = "ｶｰﾄﾞ払い"
                        Case "2"
                            cpaystr = "商品券"
                        Case "3"
                            cpaystr = "銀行振込"
                        Case "4"
                            cpaystr = "打席ｶｰﾄﾞ"
                    End Select
                    data.CPAYKBN = cpaystr

                    ' *** 合計金額など ***

                    ' 合計金額(*)
                    data.UDNBKN = total_udnbkn

                    ' 合計消費税(*)
                    data.UDNZKN = total_udnzkn

                    ' 合計数量(*)
                    data.TKTSU = total_tktsu

                    ' 加算ポイント
                    data.POINT = row.POINT

                    ' 預かり金
                    data.NYUKN = row.NYUKN

                    ' 釣金
                    data.TURIKN = row.TURIKN

                    ' *** 伝票内容(詳細) ***

                    ' 商品名
                    data.B_TKTNMA = row.B_TKTNMA

                    ' 数量
                    data.B_TKTSU = row.B_TKTSU

                    ' 金額
                    data.B_UDNBKN = row.B_UDNBKN

                    ' 消費税
                    data.B_UDNZKN = row.B_UDNZKN

                    ' 担当者
                    data.STFNAME = row.STFNAME

                    _list.Add(data)

                Next
            Next

            ApplyGrid(0)

            If _list.Count > MAX_ROW_NUM Then
                btnNext_Enabled()
            Else
                btnNext_Disabled()
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "▼ ヘルパー"

    ''' <summary>
    ''' 処理の中断が呼ばれた
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsAbort(Optional ByVal reset_abort As Boolean = True) As Boolean
        Application.DoEvents()
        If _abort Then
            If reset_abort Then
                _abort = False
            End If
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 処理中断の初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetAbort()
        _abort = False
    End Sub

#End Region

#Region "▽ ページング"

    ''' <summary>
    ''' ページングとグリッド処理
    ''' </summary>
    ''' <param name="page"></param>
    ''' <remarks></remarks>
    Private Sub ApplyGrid(ByVal page As Integer)

        Me.dgvNKNTRN.ClearGrid()

        ' グリッドにデータをバインド
        Dim currentList = New BindingList(Of DUDNTRN_View)
        Me.dgvNKNTRN.DataSource = currentList

        ' ページング処理
        Dim currentIndex = page * MAX_ROW_NUM
        Dim take_list = _list.Skip(currentIndex).Take(MAX_ROW_NUM)
        For Each x In take_list
            currentList.Add(x)
        Next

        ' 最大ページ数
        Dim maxPage = Math.Ceiling(_list.Count / MAX_ROW_NUM)

        ' コントロールの制御
        If _currentPage > 0 Then
            btnPrev_Enabled()
        Else
            btnPrev_Disabled()
        End If

        If page < maxPage - 1 Then
            btnNext_Enabled()
        Else
            btnNext_Disabled()
        End If

        ' ラベルに反映
        lblMaxPage.Text = maxPage.ToString.PadLeft(2, "0"c)
        lblPage.Text = (page + 1).ToString.PadLeft(2, "0"c)

        lblMaxCount.Text = _list.Count.ToString
        lblStaCount.Text = (currentIndex + 1).ToString
        lblEndCount.Text = (currentIndex + currentList.Count).ToString

    End Sub

    ''' <summary>
    ''' 次へボタンの有効化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnNext_Enabled()
        btnNext.Enabled = True
        btnNext.BackColor = Color.Yellow
    End Sub

    ''' <summary>
    ''' 次へボタンの無効化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnNext_Disabled()
        btnNext.Enabled = False
        btnNext.BackColor = Color.Silver
    End Sub

    ''' <summary>
    ''' 前へボタンの有効化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnPrev_Enabled()
        btnPrev.Enabled = True
        btnPrev.BackColor = Color.Yellow
    End Sub

    ''' <summary>
    ''' 前へボタンの無効化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnPrev_Disabled()
        btnPrev.Enabled = False
        btnPrev.BackColor = Color.Silver
    End Sub

    ''' <summary>
    ''' ページング処理の無効化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AllPaging_Disabled()
        btnPrev_Disabled()
        btnNext_Disabled()
        Me.lblPage.Text = "1"
        Me.lblMaxPage.Text = "1"
        Me.lblMaxCount.Text = "0"
        Me.lblStaCount.Text = "0"
        Me.lblEndCount.Text = "0"
    End Sub

    ''' <summary>
    ''' 条件クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        Try
            Me.txtNCSNO.Text = String.Empty

            Me.clbKBN001.ClearSelected()
            For i As Integer = 0 To Me.clbKBN001.Items.Count - 1
                Me.clbKBN001.SetItemChecked(i, False)
            Next
            Me.clbKBN002.ClearSelected()
            For i As Integer = 0 To Me.clbKBN002.Items.Count - 1
                Me.clbKBN002.SetItemChecked(i, False)
            Next
            Me.clbKBN003.ClearSelected()
            For i As Integer = 0 To Me.clbKBN003.Items.Count - 1
                Me.clbKBN003.SetItemChecked(i, False)
            Next

            Me.chkBUNCDB004.Checked = False

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 条件選択閉じるボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Try
            Me.pnlJoken.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 検索条件ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnJoken_Click(sender As System.Object, e As System.EventArgs) Handles btnJoken.Click
        Dim bln As Boolean = False
        Try
            If Not Me.rdoFee.Checked Then
                bln = True
            End If
            Me.clbKBN001.Enabled = bln
            Me.clbKBN002.Enabled = bln
            Me.clbKBN003.Enabled = bln
            Me.chkBUNCDB004.Enabled = bln
            Me.pnlJoken.Visible = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 検索ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFIND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFIND.Click
        Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
        Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
        Try


            Cursor = Cursors.WaitCursor
            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)

            If Not GetDUDNTRN(date1, date2) Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                End Using
                Me.pnlStatus.Visible = False
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            Me.pnlStatus.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' Prevボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Try
            _currentPage -= 1
            ApplyGrid(_currentPage)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Nextボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            _currentPage += 1
            ApplyGrid(_currentPage)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 商品リスト情報取得
    ''' </summary>
    ''' <param name="strBUNCDB"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetShohinList(ByVal strBUNCDB As String) As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" BMNCD = '002' AND BUNCDA = '001' AND BUNCDB = '" & strBUNCDB & "'")
            'sql.a()

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim clb As CheckedListBox = Nothing

            Select Case strBUNCDB
                Case "001"
                    clb = Me.clbKBN001
                Case "002"
                    clb = Me.clbKBN002
                Case "003"
                    clb = Me.clbKBN003
            End Select

            clb.Items.Clear()

            For i As Integer = 0 To resultDt.Rows.Count - 1
                clb.Items.Add(resultDt.Rows(i).Item("HINNMA").ToString())
            Next


            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

#End Region




End Class
