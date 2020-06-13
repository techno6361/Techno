Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmPRINT14

#Region "▼宣言部"

    ' 1ページに表示する最大行数(グリッド)
    Const MAX_ROW_NUM = 15

    ' 1ページに表示する最大行数(帳票)
    Const MAX_EXCEL_ROW_NUM = 32

    ' 開始行数
    Const HEADER_MARGIN = 6

    ' 罫線の視点、終点
    Const LINE_COL_INDEX_S = 2
    Const LINE_COL_INDEX_E = 11

    ' OPEN/SAVEファイル
    Const REPORT_NAME = "ベンダー管理帳票"

    ' フォーム名
    Const FORM_NAME = "ベンダー管理帳票"

    ' リスト(全体)
    Dim _list As BindingList(Of SRTTRA_View) = New BindingList(Of SRTTRA_View)

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
    Private Sub frmPRINT14_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

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

            Cursor = Cursors.WaitCursor
            Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)
            Me.Refresh()
            Me.Update()

            If Not GetSRTTRA(date1, date2) Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                    AllPaging_Disabled()
                    Me.pnlPrintStatus.Visible = False
                End Using
                _currentPage = 0

                AllPaging_Disabled()

                ' ステータスパネルの初期化
                Me.pnlPrintStatus.Init(Me)
                Me.dgvRECARDTRN.DataSource = Nothing
                '総金額
                Me.lblKinGokei.Text = "0"
                '総球数
                Me.lblTamaGokei.Text = "0"
                '総内税
                Me.lblZeiGokei.Text = "0"
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
    Private Sub frmPRINT14_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            'ﾍﾞﾝﾀﾞｰ№
            Me.cmbBNDNO.SelectedIndex = 0

            '総金額
            Me.lblKinGokei.Text = "0"
            '総球数
            Me.lblTamaGokei.Text = "0"
            '総内税
            Me.lblZeiGokei.Text = "0"

            Me.cmbTerm.SelectedIndex = 0

            _currentPage = 0

            AllPaging_Disabled()

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
    Private Function PrintList(ByVal list As BindingList(Of SRTTRA_View)) As Boolean
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
            excel.Cells(1, 10) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 対象日付
            Dim strStaSEATDT = Me.dtpStaSEATDT.Value.ToString("yyyy/MM/dd")
            Dim strEndSEATDT = Me.dtpEndSEATDT.Value.ToString("yyyy/MM/dd")
            excel.Cells(3, 4) = String.Format("対象日付：{0}～{1}", strStaSEATDT, strEndSEATDT)

            ' 日付でグループ化
            Dim group_list = list.GroupBy(Function(x) x.INSDTM1)
            
            Dim index = 1 ' 件数
            Dim row_index = HEADER_MARGIN ' 行数
            Dim offset = MAX_EXCEL_ROW_NUM + 1 ' 罫線を入れる行数

            For Each day_rows In group_list

                For Each row In day_rows

                    ' 処理の中断を判定
                    If IsAbort() Then Exit For

                    ' 営業日
                    excel.Cells(row_index, 3) = row.INSDTM1

                    ' 時間
                    excel.Cells(row_index, 4) = row.INSDTM2

                    ' 顧客番号
                    excel.Cells(row_index, 5) = row.MANNO

                    ' 氏名(顧客種別)
                    excel.Cells(row_index, 6) = row.CCSNAME

                    ' ベンダー番号
                    excel.Cells(row_index, 7) = row.BNDNO

                    ' ボール数
                    excel.Cells(row_index, 8) = row.BALLSU

                    ' 金額
                    excel.Cells(row_index, 9) = row.KAGOAKN

                    ' 消費税
                    excel.Cells(row_index, 10) = row.KAGOZEI


                    Me.pnlPrintStatus.Count = index

                    index += 1
                    row_index += 1

                    ' 罫線
                    If row_index - HEADER_MARGIN + 1 = offset Then
                        excel.DrawBoldLine(row_index)
                        offset += MAX_EXCEL_ROW_NUM
                    End If

                Next

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
    ''' 打席トランを取得し、グリッドに反映
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSRTTRA(ByVal date1 As String, ByVal date2 As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 初期化
            _currentPage = 0
            _list.Clear()
            Me.lblKinGokei.Text = "0"
            Me.lblTamaGokei.Text = "0"
            Me.lblZeiGokei.Text = "0"

            ' 領収書トラン
            sql.Clear()
            sql.Append(" SELECT A.*,B.CCSNAME,C.CKBNAME FROM SRTTRA AS A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" LEFT JOIN KBMAST AS C ON C.NKBNO = TO_NUMBER(A.KSBKB,'99')")
            sql.Append(" WHERE 1=1")
            sql.Append(String.Format(" AND A.UDNDT >= '{0}'", date1))
            sql.Append(String.Format(" AND A.UDNDT <= '{0}'", date2))
            If Me.cmbBNDNO.SelectedIndex > 0 Then
                sql.Append(" AND BNDNO = '" & Me.cmbBNDNO.SelectedIndex.ToString.PadLeft(2, "0"c) & "'")
            End If
            sql.Append(" ORDER BY A.INSDTM DESC ")

            ' SQL実行
            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return False

            Dim query = From x In dt.AsEnumerable
                        Select New SRTTRA_View With {
                            .INSDTM1 = DateTime.Parse(x("INSDTM").ToString).ToString("yyyy/MM/dd"),
                            .INSDTM2 = DateTime.Parse(x("INSDTM").ToString).ToString("HH:mm:ss"),
                            .MANNO = x("MANNO").ToString,
                            .CCSNAME = x("CCSNAME").ToString & "(" & x("CKBNAME").ToString & ")",
                            .BNDNO = x("BNDNO").ToString & "号機",
                            .KAGOAKN = CType(x("KAGOAKN").ToString, Integer).ToString("#,##0"),
                            .BALLSU = CType(x("BALLSU").ToString, Integer).ToString("#,##0"),
                            .KAGOZEI = CType(x("KAGOZEI").ToString, Integer).ToString("#,##0")
            }

            ' データグリッドに反映
            For Each row In query

                Dim data = New SRTTRA_View

                data.INSDTM1 = row.INSDTM1
                data.INSDTM2 = row.INSDTM2
                data.MANNO = row.MANNO
                data.CCSNAME = row.CCSNAME
                data.BNDNO = row.BNDNO
                data.KAGOAKN = row.KAGOAKN
                data.BALLSU = row.BALLSU
                data.KAGOZEI = row.KAGOZEI
                _list.Add(data)

                '総金額
                Me.lblKinGokei.Text = (CType(Me.lblKinGokei.Text, Integer) + CType(row.KAGOAKN, Integer)).ToString("#,##0")
                '総球数
                Me.lblTamaGokei.Text = (CType(Me.lblTamaGokei.Text, Integer) + CType(row.BALLSU, Integer)).ToString("#,##0")
                '総内税
                Me.lblZeiGokei.Text = (CType(Me.lblZeiGokei.Text, Integer) + CType(row.KAGOZEI, Integer)).ToString("#,##0")
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

        Me.dgvRECARDTRN.ClearGrid()

        ' グリッドにデータをバインド
        Dim currentList = New BindingList(Of SRTTRA_View)
        Me.dgvRECARDTRN.DataSource = currentList

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
