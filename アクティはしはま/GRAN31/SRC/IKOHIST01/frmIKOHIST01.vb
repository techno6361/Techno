Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmIKOHIST01

#Region "▼宣言部"

    ' 1ページに表示する最大行数(グリッド)
    Const MAX_ROW_NUM = 16

    ' フォーム名
    Const FORM_NAME = "残高移行履歴"

    ' リスト(全体)
    Dim _list As BindingList(Of IKOHIST_View) = New BindingList(Of IKOHIST_View)

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
            IDatabase = iDB
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

            ' 初期読込
            DoFind(False)


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
    ''' レシート再発行ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipt.Click
        Try
            Cursor = Cursors.WaitCursor

            ' 一覧の印刷
            If Not PrintList(_list) Then
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
            DoFind(True)
        Catch ex As Exception
            Throw ex
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

    ''' <summary>
    ''' グリッドビューを行選択で選択状態にする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvIKOHIST_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvIKOHIST.MouseClick
        Dim pos As DataGridView.HitTestInfo = Me.dgvIKOHIST.HitTest(e.X, e.Y)
        If pos.Type = DataGrid.HitTestType.Cell Then
            If CType(Me.dgvIKOHIST(0, pos.RowIndex).Value, Boolean) Then
                Me.dgvIKOHIST(0, pos.RowIndex).Value = False
            Else
                Me.dgvIKOHIST(0, pos.RowIndex).Value = True
            End If
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
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

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
    ''' 検索してグリッドに表示
    ''' </summary>
    ''' <param name="bShowMsg">データが見つからなかった場合メッセージを表示する</param>
    ''' <remarks></remarks>
    Private Sub DoFind(ByVal bShowMsg As Boolean)
        Try
            Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
            Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")

            Cursor = Cursors.WaitCursor
            Me.pnlPrintStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)
            Me.Refresh()
            Me.Update()

            If Not GetIKOHIST(date1, date2) Then
                If bShowMsg Then
                    Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                        frm.ShowDialog()
                    End Using
                End If
                AllPaging_Disabled()
                Me.dgvIKOHIST.ClearGrid()
                Me.pnlPrintStatus.Visible = False
                Me.dgvIKOHIST.RowCount = 0
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
    ''' グリッドに表示中のデータをレシート印刷
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintList(ByVal list As BindingList(Of IKOHIST_View)) As Boolean
        Dim blnPrint = False
        Try
            If Not list.Any Then
                Using frm As New frmMSGBOX01("検索ボタンを押してください。", 3)
                    frm.ShowDialog()
                End Using
                Return False
            End If

            Dim intHAKKOKIN As Integer = 0

            For Each x In list
                If Not x.CHECK Then Continue For

                intHAKKOKIN = 0

                If CType(x.HAKKOKIN.ToString, Integer) > 0 Then
                    intHAKKOKIN = CType(x.HAKKOKIN.ToString, Integer)
                End If

                Dim dtGOODS As DataTable ' 売上商品一覧
                dtGOODS = New DataTable("GOODS")
                dtGOODS.Columns.Add("GDSNAME", GetType(String))
                dtGOODS.Columns.Add("GDSCOUNT", GetType(String))
                dtGOODS.Columns.Add("GDSTAX", GetType(String))
                dtGOODS.Columns.Add("GDSKIN", GetType(String))
                dtGOODS.Columns.Add("CPAYKBN", GetType(String))

                Dim dr As DataRow
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行 残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = (CType(x.MOTOZANKN.ToString, Integer) + intHAKKOKIN).ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行 P)残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = (CType(x.MOTOPREZANKN.ToString, Integer) - intHAKKOKIN).ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行 POINT"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.MOTOSRTPO.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)

                '***
                If intHAKKOKIN > 0 Then
                    dr = dtGOODS.NewRow
                    dr("GDSNAME") = "発行料精算"
                    dr("GDSCOUNT") = 1
                    dr("GDSTAX") = 0
                    dr("GDSKIN") = intHAKKOKIN.ToString("#,##0")
                    dr("CPAYKBN") = String.Empty
                    dtGOODS.Rows.Add(dr)
                    dr = dtGOODS.NewRow
                    dr("GDSNAME") = "ﾌﾟﾚﾐｱﾑ還元"
                    dr("GDSCOUNT") = 1
                    dr("GDSTAX") = 0
                    dr("GDSKIN") = intHAKKOKIN.ToString("#,##0")
                    dr("CPAYKBN") = String.Empty
                    dtGOODS.Rows.Add(dr)
                    dr = dtGOODS.NewRow
                    dr("GDSNAME") = String.Empty
                    dr("GDSCOUNT") = String.Empty
                    dr("GDSTAX") = String.Empty
                    dr("GDSKIN") = String.Empty
                    dr("CPAYKBN") = String.Empty
                    dtGOODS.Rows.Add(dr)
                End If
                '***

                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行前 残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.MOTOZANKN2.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行前 P)残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.MOTOPREZANKN2.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行前 POINT"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.MOTOSRTPO2.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)

                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行後 残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.ZANKN.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行後 P)残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.PREZANKN.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行後 POINT"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = x.SRTPO.ToString("N0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)

                '【レシート印刷】
                Dim rePrint As New TMT90.Receipt

                rePrint.intGetPremKn = 0
                rePrint.intGetPoint = 0

                rePrint.intPrintKbn = 3
                rePrint.strManno = x.NCSNO ' 顧客番号

                rePrint.strccsname = x.CCSNAME    '氏名
                Dim dtmInsDt As DateTime = DateTime.Parse(String.Format("{0} {1}", x.IKODT, x.IKOTIME))
                rePrint.insDTTM = dtmInsDt ' 発行日時
                rePrint.dtGoods = dtGOODS
                rePrint.strHostName = Net.Dns.GetHostName
                rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)

                rePrint.RePrint(False, String.Empty)

                blnPrint = True
            Next

            If Not blnPrint Then
                Using frm As New frmMSGBOX01("対象が選択されていません。", 3)
                    frm.ShowDialog()
                End Using
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ポイント還元履歴トランを取得し、グリッドに反映
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIKOHIST(ByVal date1 As String, ByVal date2 As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 初期化
            _currentPage = 0
            _list.Clear()

            ' IKOTRN
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",B.CCSNAME")
            sql.Append(" FROM IKOTRN AS A")
            sql.Append(" LEFT JOIN CSMAST AS B ON B.NCSNO = A.NCSNO")
            sql.Append(String.Format(" WHERE A.IKODT >= '{0}'", date1))
            sql.Append(String.Format(" AND A.IKODT <= '{0}'", date2))
            sql.Append(" ORDER BY A.INSDTM DESC")

            ' SQL実行
            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return False

            Dim query = From x In dt.AsEnumerable
                        Select New IKOHIST_View With {
                            .IKODT = String.Format("{0}/{1}/{2}", x("IKODT").ToString.Substring(0, 4), x("IKODT").ToString.Substring(4, 2), x("IKODT").ToString.Substring(6, 2)),
                            .IKOTIME = String.Format("{0}:{1}", x("IKOTIME").ToString.Substring(0, 2), x("IKOTIME").ToString.Substring(2, 2)),
                            .NCSNO = x("NCSNO").ToString.PadLeft(8, "0"c),
                            .CCSNAME = x("CCSNAME").ToString,
                            .MOTOZANKN = CInt(x("MOTOZANKN")),
                            .MOTOPREZANKN = CInt(x("MOTOPREZANKN")),
                            .MOTOSRTPO = CInt(x("MOTOSRTPO")),
                            .MOTOZANKN2 = CInt(x("ZANKN")) - CInt(x("MOTOZANKN")),
                            .MOTOPREZANKN2 = CInt(x("PREZANKN")) - CInt(x("MOTOPREZANKN")),
                            .MOTOSRTPO2 = CInt(x("SRTPO")) - CInt(x("MOTOSRTPO")),
                            .HAKKOKIN = CInt(x("HAKKOKIN")),
                            .ZANKN = CInt(x("ZANKN")),
                            .PREZANKN = CInt(x("PREZANKN")),
                            .SRTPO = CInt(x("SRTPO")),
                            .STFNAME = x("STFNAME").ToString
                        }

            ' データグリッドに反映
            For Each row In query

                Dim data = New IKOHIST_View

                data.IKODT = row.IKODT
                data.IKOTIME = row.IKOTIME
                data.NCSNO = row.NCSNO
                data.CCSNAME = row.CCSNAME
                data.MOTOZANKN = row.MOTOZANKN
                data.MOTOPREZANKN = row.MOTOPREZANKN
                data.MOTOSRTPO = row.MOTOSRTPO
                data.MOTOZANKN2 = row.MOTOZANKN2
                data.MOTOPREZANKN2 = row.MOTOPREZANKN2
                data.MOTOSRTPO2 = row.MOTOSRTPO2
                data.ZANKN = row.ZANKN
                data.PREZANKN = row.PREZANKN
                data.SRTPO = row.SRTPO
                data.HAKKOKIN = row.HAKKOKIN
                data.STFNAME = row.STFNAME

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

        Me.dgvIKOHIST.ClearGrid()

        ' グリッドにデータをバインド
        Dim currentList = New BindingList(Of IKOHIST_View)
        Me.dgvIKOHIST.DataSource = currentList

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
