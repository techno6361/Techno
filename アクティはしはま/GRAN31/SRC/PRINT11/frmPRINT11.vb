Imports TECHNO.DataBase
Imports Microsoft.Office.Interop
Imports System.ComponentModel

Public Class frmPRINT11

#Region "▼宣言部"

    ' 1ページに表示する最大行数(グリッド)
    Const MAX_ROW_NUM = 16

    ' 1ページに表示する最大行数(帳票)
    Const MAX_EXCEL_ROW_NUM = 32

    ' 開始行数
    Const HEADER_MARGIN = 8

    ' 罫線の視点、終点
    Const LINE_COL_INDEX_S = 2
    Const LINE_COL_INDEX_E = 13

    ' OPEN/SAVEファイル
    Const REPORT_NAME = "入金額別情報"

    ' フォーム名
    Const FORM_NAME = "入金額別情報"

    ' リスト(全体)
    Dim _list As BindingList(Of NKNTRN_View) = New BindingList(Of NKNTRN_View)

    ' 現在のページ
    Dim _currentPage As Integer = 0

    ' 処理の中断フラグ
    Dim _abort As Boolean = False

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = FORM_NAME

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = FORM_NAME

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
    Private Sub frmPRINT04_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' 画面初期設定
            Init()

            ' 種別マスタの取得
            If Not GetKBMAST Then
                Using frm As New frmMSGBOX01("種別マスタを取得できませんでした。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

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
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Try
            Cursor = Cursors.WaitCursor

            Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)

            ' 一覧の印刷
            btnFIND.PerformClick()
            If Not PrintList(_list) Then
                Me.pnlPrintStatus.Visible = False
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.pnlPrintStatus.Visible = False
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 検索ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFIND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFIND.Click
        Try
            Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
            Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
            Dim tktkbn As String = Nothing

            If Me.rbKB1.Checked Then
                tktkbn = "0"
            ElseIf Me.rbKB2.Checked Then
                tktkbn = "1"
            ElseIf Me.rbKB3.Checked Then
                tktkbn = "2"
            End If

            Cursor = Cursors.WaitCursor
            Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)
            Me.Refresh()
            Me.Update()

            If Not GetNKNTRN(date1, date2, tktkbn) Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                    Me.pnlPrintStatus.Visible = False
                End Using
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
            Me.pnlPrintStatus.Visible = False
        End Try
    End Sub

    ''' <summary>
    ''' ESCキーで処理の中断
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPRINT08_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            _abort = True
        End If
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

            Me.cmbTerm.SelectedIndex = 0
            Me.rbKB_ALL.Checked = True

            _currentPage = 0

            ' ステータスパネルの初期化
            Me.pnlPrintStatus.Init(Me)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' グリッドに表示中のデータを印刷
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintList(ByVal list As BindingList(Of NKNTRN_View)) As Boolean
        Dim excel = New UIExcel
        Try
            If Not list.Any Then Return False

            ' *** 帳票の出力 ***

            Dim strReportName As String = REPORT_NAME
            Dim strOpenReportName As String = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
            Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                             & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                             & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"

            ' ファイルを開く
            excel.Open(strOpenReportName, 1, False)

            ' ステータスを表示
            Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE_ESC, _list.Count)

            ' 罫線の始点と終点を設定
            excel.SetLineRange(LINE_COL_INDEX_S, LINE_COL_INDEX_E)

            ' 出力日
            excel.Cells(1, 12) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 対象日付
            Dim strStaSEATDT = Me.dtpStaSEATDT.Value.ToString("yyyy/MM/dd")
            Dim strEndSEATDT = Me.dtpEndSEATDT.Value.ToString("yyyy/MM/dd")
            excel.Cells(5, 2) = String.Format("対象日付：{0}～{1}", strStaSEATDT, strEndSEATDT)

            Dim index = 1 ' 件数
            Dim row_index = HEADER_MARGIN ' 行数
            Dim offset = MAX_EXCEL_ROW_NUM + 1 ' 罫線を入れる行数

            ' グリッドの列名を帳票にも反映
            For i = 1 To 10
                excel.Cells(7, 3 + (i - 1)) = Me.dgvNKNTRN.Columns(i).HeaderText.ToString()
            Next

            For Each row In list

                ' 処理の中断を判定
                If IsAbort() Then Exit For

                ' 入金額
                excel.Cells(row_index, 2) = row.UDNKN

                ' 種別１
                excel.Cells(row_index, 3) = row.KSBKB1.ToString("N0")

                ' 種別２
                excel.Cells(row_index, 4) = row.KSBKB2.ToString("N0")

                ' 種別３
                excel.Cells(row_index, 5) = row.KSBKB3.ToString("N0")

                ' 種別４
                excel.Cells(row_index, 6) = row.KSBKB4.ToString("N0")

                ' 種別５
                excel.Cells(row_index, 7) = row.KSBKB5.ToString("N0")

                ' 種別６
                excel.Cells(row_index, 8) = row.KSBKB6.ToString("N0")

                ' 種別７
                excel.Cells(row_index, 9) = row.KSBKB7.ToString("N0")

                ' 種別８
                excel.Cells(row_index, 10) = row.KSBKB8.ToString("N0")

                ' 種別９
                excel.Cells(row_index, 11) = row.KSBKB9.ToString("N0")

                ' 種別１０
                excel.Cells(row_index, 12) = row.KSBKB10.ToString("N0")

                ' 合計
                excel.Cells(row_index, 13) = row.TOTAL.ToString("N0")

                Me.pnlPrintStatus.Count = index

                index += 1
                row_index += 1

                ' 罫線
                If row_index - HEADER_MARGIN + 1 = offset Then
                    excel.DrawBoldLine(row_index)
                    offset += MAX_EXCEL_ROW_NUM
                End If

            Next

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 種別別入金トランを取得し、グリッドに反映
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNKNTRN(ByVal date1 As String, ByVal date2 As String, ByVal stsflg As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 初期化
            _currentPage = 0
            _list.Clear()

            ' 入金トラン
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.UDNKN, A.KSBKB, COUNT(*), A.BUNCDB")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN NKNTRN AS B")
            sql.Append(" ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE A.DATKB = '1' AND A.BMNCD = '002' AND (A.BUNCDA = '003' OR A.BUNCDA = '010') AND A.BMNCD <> '004' AND A.KSBKB IS NOT NULL")
            sql.Append(String.Format(" AND A.UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND A.UDNDT <= '{0}'", date2))

            ' フィルタリング
            If Not String.IsNullOrEmpty(stsflg) Then
                sql.Append(String.Format(" AND B.STSFLG = '{0}'", stsflg))
            End If

            sql.Append(" GROUP BY A.UDNKN, A.KSBKB, B.STSFLG, A.BUNCDB")
            sql.Append(" ORDER BY A.UDNKN")

            ' SQL実行
            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return False

            Dim query = From x In dt.AsEnumerable
                        Select New With {
                            .UDNKN = CInt(x("UDNKN")),
                            .KSBKB = x("KSBKB").ToString,
                            .COUNT = CInt(x("COUNT")),
                            .BUNCDB = x("BUNCDB").ToString
                        }

            ' 入金額でグループ化
            Dim group_query = query.GroupBy(Function(x) x.UDNKN)

            ' その他のデータ
            Dim other_data = New NKNTRN_View
            Dim other_flg = False ' 追加フラグ

            ' 総合計のデータ
            Dim total_data = New NKNTRN_View
            total_data.UDNKN = "総合計"
            Dim total = 0 ' 総合計の合計

            ' データグリッドに反映
            For Each g_row In group_query

                Dim data = New NKNTRN_View
                Dim flg = False ' 追加フラグ

                For Each row In g_row

                    If row.BUNCDB = "004" Then

                        '【その他の集計】
                        other_flg = True
                        other_data.UDNKN = "その他"

                        Select Case row.KSBKB
                            Case "1"
                                other_data.KSBKB1 += row.COUNT
                                total_data.KSBKB1 += row.COUNT
                            Case "2"
                                other_data.KSBKB2 += row.COUNT
                                total_data.KSBKB2 += row.COUNT
                            Case "3"
                                other_data.KSBKB3 += row.COUNT
                                total_data.KSBKB3 += row.COUNT
                            Case "4"
                                other_data.KSBKB4 += row.COUNT
                                total_data.KSBKB4 += row.COUNT
                            Case "5"
                                other_data.KSBKB5 += row.COUNT
                                total_data.KSBKB5 += row.COUNT
                            Case "6"
                                other_data.KSBKB6 += row.COUNT
                                total_data.KSBKB6 += row.COUNT
                            Case "7"
                                other_data.KSBKB7 += row.COUNT
                                total_data.KSBKB7 += row.COUNT
                            Case "8"
                                other_data.KSBKB8 += row.COUNT
                                total_data.KSBKB8 += row.COUNT
                            Case "9"
                                other_data.KSBKB9 += row.COUNT
                                total_data.KSBKB9 += row.COUNT
                            Case "10"
                                other_data.KSBKB10 += row.COUNT
                                total_data.KSBKB10 += row.COUNT
                        End Select

                        other_data.TOTAL += row.COUNT
                        total += row.COUNT

                    Else

                        '【その他以外の集計】
                        flg = True
                        data.UDNKN = row.UDNKN.ToString("N0") & "円"

                        Select Case row.KSBKB
                            Case "1"
                                data.KSBKB1 += row.COUNT
                                total_data.KSBKB1 += row.COUNT
                            Case "2"
                                data.KSBKB2 += row.COUNT
                                total_data.KSBKB2 += row.COUNT
                            Case "3"
                                data.KSBKB3 += row.COUNT
                                total_data.KSBKB3 += row.COUNT
                            Case "4"
                                data.KSBKB4 += row.COUNT
                                total_data.KSBKB4 += row.COUNT
                            Case "5"
                                data.KSBKB5 += row.COUNT
                                total_data.KSBKB5 += row.COUNT
                            Case "6"
                                data.KSBKB6 += row.COUNT
                                total_data.KSBKB6 += row.COUNT
                            Case "7"
                                data.KSBKB7 += row.COUNT
                                total_data.KSBKB7 += row.COUNT
                            Case "8"
                                data.KSBKB8 += row.COUNT
                                total_data.KSBKB8 += row.COUNT
                            Case "9"
                                data.KSBKB9 += row.COUNT
                                total_data.KSBKB9 += row.COUNT
                            Case "10"
                                data.KSBKB10 += row.COUNT
                                total_data.KSBKB10 += row.COUNT
                        End Select

                        data.TOTAL += row.COUNT
                        total += row.COUNT

                    End If

                Next

                If flg Then
                    _list.Add(data)
                End If

            Next

            ' その他の集計
            If other_flg Then
                _list.Add(other_data)
            End If

            ' 総合計
            total_data.TOTAL = total
            _list.Add(total_data)

            Me.dgvNKNTRN.ClearGrid()

            ' グリッドにデータをバインド
            Me.dgvNKNTRN.DataSource = _list

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 種別マスタを取得し、グリッドに反映
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 初期化
            _currentPage = 0
            _list.Clear()

            ' 入金トラン
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" CKBNAME")
            sql.Append(" FROM KBMAST")
            sql.Append(" WHERE NKBNO >= 1 AND NKBNO <= 10")
            sql.Append(" ORDER BY NKBNO")
            
            ' SQL実行
            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return False

            Dim query = From x In dt.AsEnumerable
                        Select New With {
                            .CKBNAME = x("CKBNAME").ToString
                        }

            Dim i = 1

            ' データグリッドに反映
            For Each row In query
                Me.dgvNKNTRN.Columns(i).HeaderText = row.CKBNAME
                i += 1
            Next

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
    Private Function IsAbort() As Boolean
        Application.DoEvents()
        If _abort Then
            _abort = False
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


End Class
