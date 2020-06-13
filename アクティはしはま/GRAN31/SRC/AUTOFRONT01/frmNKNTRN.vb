Imports Techno.DataBase
Imports System.ComponentModel

Public Class frmNKNTRN

#Region "▼宣言部"

    ' 1ページに表示する最大行数(グリッド)
    Const MAX_ROW_NUM = 16

    ' 1ページに表示する最大行数(帳票)
    Const MAX_EXCEL_ROW_NUM = 32

    ' 罫線の視点、終点
    Const LINE_COL_INDEX_S = 2
    Const LINE_COL_INDEX_E = 15

    ' 入金リスト(全体)
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

            MyBase.l_Title_FormName = "入金履歴帳票"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "入金履歴帳票"

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
            '画面初期設定
            Init()

            Me.rbKB2.Checked = True

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
    Private Sub frmNKNTRN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.btnFIND.PerformClick()
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

            ' 商品売上一覧の印刷
            btnFIND.PerformClick()
            'If Not PrintList(_list) Then
            '    Me.pnlPrintStatus.Visible = False
            '    Exit Sub
            'End If

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
            Dim stsflg As String = Nothing

            If Me.rbKB1.Checked Then
                stsflg = "0"
            ElseIf Me.rbKB2.Checked Then
                stsflg = "1"
            ElseIf Me.rbKB3.Checked Then
                stsflg = "9"
            End If

            Cursor = Cursors.WaitCursor
            Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)
            Me.Refresh()
            Me.Update()

            If Not GetNKNTRN(date1, date2, stsflg) Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                    AllPaging_Disabled()
                    Me.pnlPrintStatus.Visible = False
                End Using
                Exit Sub
            End If

            Me.lblPage.Enabled = True
            Me.lblMaxPage.Enabled = True

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
            Me.pnlPrintStatus.Visible = False
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
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = False
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            Me.cmbTerm.SelectedIndex = 0
            Me.rbKB_ALL.Checked = True

            _currentPage = 0

            AllPaging_Disabled()

            ' ステータスパネルの初期化
            Me.pnlPrintStatus.Init(Me)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' ''' <summary>
    ' ''' グリッドに表示中の商品売上管理帳票(日報)を印刷
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Function PrintList(ByVal list As BindingList(Of NKNTRN_View)) As Boolean
    '    Dim excel = New UIExcel
    '    Try
    '        If Not list.Any Then Return False

    '        ' *** 帳票の出力 ***

    '        Dim strReportName As String = "入金履歴"
    '        Dim strOpenReportName As String = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
    '        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
    '                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
    '                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"

    '        ' ファイルを開く
    '        excel.Open(strOpenReportName, 1, False)

    '        ' ステータスを表示
    '        Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE_ESC, _list.Count)

    '        ' 罫線の始点と終点を設定
    '        excel.SetLineRange(2, 14)

    '        ' 出力日
    '        excel.Cells(1, 13) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

    '        ' 対象日付
    '        Dim strStaSEATDT = Me.dtpStaSEATDT.Value.ToString("yyyy/MM/dd")
    '        Dim strEndSEATDT = Me.dtpEndSEATDT.Value.ToString("yyyy/MM/dd")
    '        excel.Cells(5, 3) = String.Format("{0}～{1}", strStaSEATDT, strEndSEATDT)

    '        ' 日付でグループ化
    '        Dim group_list = list.GroupBy(Function(x) x.INSDTM1)

    '        Dim index = 1 ' 件数
    '        Dim header_margin = 8 ' 開始行
    '        Dim row_index = header_margin ' 行数
    '        Dim offset = MAX_EXCEL_ROW_NUM + 1 ' 罫線を入れる行数

    '        For Each day_rows In group_list

    '            For Each row In day_rows

    '                ' 処理の中断を判定
    '                If IsAbort() Then Exit For

    '                ' 取引日
    '                excel.Cells(row_index, 2) = row.INSDTM1

    '                ' 時間
    '                excel.Cells(row_index, 3) = row.INSDTM2

    '                ' 入金区分
    '                excel.Cells(row_index, 4) = row.STSFLG

    '                ' 顧客番号
    '                excel.Cells(row_index, 5) = row.MANNO

    '                ' 入金額
    '                excel.Cells(row_index, 6) = row.NKNKN.ToString("N0")

    '                ' プレミアム
    '                excel.Cells(row_index, 7) = row.PREMKN.ToString("N0")

    '                ' ポイント
    '                excel.Cells(row_index, 8) = row.POINT.ToString("N0")

    '                ' 担当者
    '                excel.Cells(row_index, 9) = row.STFNAME

    '                ' 投入金額
    '                excel.Cells(row_index, 10) = row.NYUKN.ToString("N0")

    '                ' おつり
    '                excel.Cells(row_index, 11) = row.TURIKN.ToString("N0")

    '                ' 支払い区分
    '                excel.Cells(row_index, 12) = row.CPAYKBN

    '                ' 入金前
    '                excel.Cells(row_index, 13) = row.ZANAKN.ToString("N0")

    '                ' 入金後
    '                excel.Cells(row_index, 14) = row.ZANBKN.ToString("N0")

    '                Me.pnlPrintStatus.Count = index

    '                index += 1
    '                row_index += 1

    '                ' 罫線
    '                If row_index - header_margin + 1 = offset Then
    '                    excel.DrawBoldLine(row_index)
    '                    offset += MAX_EXCEL_ROW_NUM
    '                End If

    '            Next

    '            ' １日の集計
    '            excel.DrawLine(row_index)
    '            excel.Cells(row_index, 5) = "【合　計】"
    '            excel.Cells(row_index, 6) = day_rows.Select(Function(x) x.NKNKN).Sum().ToString("N0")
    '            excel.Cells(row_index, 7) = day_rows.Select(Function(x) x.PREMKN).Sum().ToString("N0")
    '            excel.Cells(row_index, 8) = day_rows.Select(Function(x) x.POINT).Sum().ToString("N0")
    '            excel.Cells(row_index, 10) = day_rows.Select(Function(x) x.NKNKN).Sum().ToString("N0")
    '            excel.Cells(row_index, 11) = day_rows.Select(Function(x) x.TURIKN).Sum().ToString("N0")
    '            excel.Cells(row_index, 13) = day_rows.Select(Function(x) x.ZANAKN).Sum().ToString("N0")
    '            excel.Cells(row_index, 14) = day_rows.Select(Function(x) x.ZANBKN).Sum().ToString("N0")

    '            row_index += 1

    '            ' 罫線
    '            If row_index - header_margin + 1 = offset Then
    '                excel.DrawBoldLine(row_index)
    '                offset += MAX_EXCEL_ROW_NUM
    '            End If

    '        Next

    '        ' 総合計
    '        excel.DrawDoubleLine(row_index)

    '        excel.Cells(row_index, 5) = "【総　合　計】"
    '        excel.Cells(row_index, 6) = list.Select(Function(x) x.NKNKN).Sum().ToString("N0")
    '        excel.Cells(row_index, 7) = list.Select(Function(x) x.PREMKN).Sum().ToString("N0")
    '        excel.Cells(row_index, 8) = list.Select(Function(x) x.POINT).Sum().ToString("N0")
    '        excel.Cells(row_index, 10) = list.Select(Function(x) x.NKNKN).Sum().ToString("N0")
    '        excel.Cells(row_index, 11) = list.Select(Function(x) x.TURIKN).Sum().ToString("N0")
    '        excel.Cells(row_index, 13) = list.Select(Function(x) x.ZANAKN).Sum().ToString("N0")
    '        excel.Cells(row_index, 14) = list.Select(Function(x) x.ZANBKN).Sum().ToString("N0")

    '        ' 罫線
    '        If row_index - header_margin + 1 = offset Then
    '            excel.DrawBoldLine(row_index)
    '            offset += MAX_EXCEL_ROW_NUM
    '        End If

    '        'ファイル保存
    '        excel.SaveAs(strSaveReportName, True)

    '        Return True

    '    Catch ex As Exception
    '        excel.Dispose()
    '        Throw ex
    '    End Try
    'End Function

    ''' <summary>
    ''' 入金トランを取得し、グリッドに反映
    ''' </summary>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <param name="stsflg"></param>
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
            sql.Append(" *")
            sql.Append(" FROM NKNTRN")
            sql.Append(" WHERE DATKB = '1'")
            sql.Append(" AND (STSFLG = '0' OR STSFLG = '1' OR STSFLG = '9')")
            sql.Append(String.Format(" AND DENDT >= '{0}'", date1))
            sql.Append(String.Format(" AND DENDT <= '{0}'", date2))
            sql.Append(" ORDER BY DENDT DESC,DENNO DESC")
            Dim nkntrn_dt = iDatabase.ExecuteRead(sql.ToString())

            If nkntrn_dt.Rows.Count <= 0 Then Return False

            Dim nkntrn_query = From x In nkntrn_dt.AsEnumerable
                               Select New With {
                                   .DENDT = x("DENDT").ToString,
                                   .DENNO = CInt(x("DENNO")),
                                   .INSDTM = DateTime.Parse(x("INSDTM").ToString),
                                   .STSFLG = CInt(x("STSFLG")),
                                   .MANNO = x("MANNO").ToString,
                                   .NKNKN = CInt(x("NKNKN")),
                                   .PREMKN = CInt(x("PREMKN")),
                                   .POINT = CInt(x("POINT")),
                                   .ZANAKN = CInt(x("ZANAKN")) + CInt(x("PREZANAKN")),
                                   .ZANBKN = CInt(x("ZANBKN")) + CInt(x("PREZANBKN"))
                                }

            ' フィルタリング
            If Not String.IsNullOrEmpty(stsflg) Then
                nkntrn_query = nkntrn_query.Where(Function(x) x.STSFLG = CInt(stsflg))
            End If

            If Not nkntrn_query.Any Then Return False

            ' 売上トラン
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE DATKB = '1'")
            sql.Append(String.Format(" AND UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND UDNDT <= '{0}'", date2))
            Dim dudntrn_dt = iDatabase.ExecuteRead(sql.ToString())
            If dudntrn_dt.Rows.Count <= 0 Then Return False

            Dim dudntrn_query = From x In dudntrn_dt.AsEnumerable
                                Select New With {
                                   .UDNDT = x("UDNDT").ToString,
                                   .UDNNO = CInt(x("UDNNO")),
                                   .STFNAME = x("STFNAME").ToString,
                                   .NYUKN = CInt(x("NYUKN")),
                                   .TURIKN = CInt(x("TURIKN")),
                                   .CPAYKBN = CInt(x("CPAYKBN"))
                                 }

            Dim join_query = From n In nkntrn_query
                             Join d In dudntrn_query On _
                             New With {Key .DENDT = n.DENDT, Key .DENNO = n.DENNO} Equals _
                             New With {Key .DENDT = d.UDNDT, Key .DENNO = d.UDNNO}
                             Select New With {
                                 .INSDTM1 = n.INSDTM.ToString("yyyy/MM/dd"),
                                 .INSDTM2 = n.INSDTM.ToString("HH:mm"),
                                 .STSFLG = n.STSFLG,
                                 .MANNO = n.MANNO,
                                 .NKNKN = n.NKNKN,
                                 .PREMKN = n.PREMKN,
                                 .POINT = n.POINT,
                                 .STFNAME = d.STFNAME,
                                 .NYUKN = d.NYUKN,
                                 .TURIKN = d.TURIKN,
                                 .CPAYKBN = d.CPAYKBN,
                                 .ZANAKN = n.ZANAKN,
                                 .ZANBKN = n.ZANBKN
                             }

            If Not join_query.Any Then Return False

            ' データグリッドに反映
            For Each row In join_query

                Dim data = New NKNTRN_View

                data.INSDTM1 = row.INSDTM1
                data.INSDTM2 = row.INSDTM2

                Select Case row.STSFLG
                    Case 0
                        data.STSFLG = "入金"
                    Case 1
                        data.STSFLG = "入金機"
                    Case 9
                        data.STSFLG = "サービス"
                End Select

                data.MANNO = row.MANNO
                data.NKNKN = row.NKNKN
                data.PREMKN = row.PREMKN
                data.POINT = row.POINT
                data.STFNAME = row.STFNAME
                data.NYUKN = row.NYUKN
                data.TURIKN = row.TURIKN

                Select Case row.CPAYKBN
                    Case 0
                        data.CPAYKBN = "現金"
                    Case 1
                        data.CPAYKBN = "カード"
                    Case 2
                        data.CPAYKBN = "商品券"
                    Case 3
                        data.CPAYKBN = "銀行振込"
                End Select

                data.ZANAKN = row.ZANAKN
                data.ZANBKN = row.ZANBKN

                _list.Add(data)

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

    ''' <summary>
    ''' ページングとグリッド処理
    ''' </summary>
    ''' <param name="page"></param>
    ''' <remarks></remarks>
    Private Sub ApplyGrid(ByVal page As Integer)

        Me.dgvNKNTRN.ClearGrid()

        ' グリッドにデータをバインド
        Dim currentList = New BindingList(Of NKNTRN_View)
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
