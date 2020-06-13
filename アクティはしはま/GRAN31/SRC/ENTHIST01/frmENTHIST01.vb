Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmENTHIST01

#Region "▼宣言部"

    Dim _currentPage As Integer ' 現在のページ
    Dim _maxPage As Integer ' 最大ページ

    ' 開始時のみ結果を格納する保存用バッファ
    Dim _enttraDt As DataTable
    Dim _csmastDt As DataTable
    Dim _kbmastDt As DataTable
    Dim _eigmtbDt As DataTable

    Dim _joinEnttraQuery As System.Data.EnumerableRowCollection(Of JoinENTTRA_View)

    ' 1ページに対する最大行数
    Private Const RECORD_OF_PAGE As Integer = 16

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "入場履歴"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "入場履歴"

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
    ''' 開始処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmENTHIST01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' 画面初期化
            Init()

            ' 入場トランの取得
            GetENTTRA(0, True)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' ボタン押下_前のページ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Try
            If _currentPage > 0 Then _currentPage -= 1
            GetENTTRA(_currentPage)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' ボタン押下_次のページ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            If _currentPage < _maxPage Then _currentPage += 1
            GetENTTRA(_currentPage)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' F11クリアボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func11()
        Try
            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            ' 画面初期化
            Init()

            ' 入場トランの取得
            GetENTTRA(0, True)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        ' 余計なボタンを非表示
        Me.tspFunc1.Enabled = True
        Me.tspFunc3.Enabled = False
        Me.tspFunc8.Enabled = False
        Me.tspFunc9.Enabled = False
        Me.tspFunc10.Enabled = True
        Me.tspFunc11.Enabled = True
        Me.tspFunc12.Enabled = False

        ' ページの初期化
        _currentPage = 0

        ' ページング機能の初期化
        btnPrev.Enabled = False
        btnNext.Enabled = False

        ' 件数の初期化
        lblCount.Text = "全0件"

    End Sub

    ''' <summary>
    ''' 入場トランの取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetENTTRA_old(ByVal currentPage As Integer)
        Dim sql As New System.Text.StringBuilder
        Dim list As BindingList(Of ENTHIST_View) = New BindingList(Of ENTHIST_View)
        Me.dgvENTHIST.DataSource = list
        Try

            Dim strDate = DateTime.Now.ToString("yyyyMMdd")

            ' ENTTRAの取得
            If _enttraDt Is Nothing Then
                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM ENTTRA")
                '*** Sta Add 2018/06/06 Kitahara
                sql.Append(" WHERE")
                sql.Append(" ENTDT = '" & strDate & "'")
                '*** End Add 2018/06/06 Kitahara
                _enttraDt = iDatabase.ExecuteRead(sql.ToString())
            End If

            If _enttraDt.Rows.Count <= 0 Then Return

            ' CSMASTの取得
            If _csmastDt Is Nothing Then
                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM CSMAST")
                _csmastDt = iDatabase.ExecuteRead(sql.ToString())
            End If

            If _csmastDt.Rows.Count <= 0 Then Return

            ' KBMASTの取得
            If _kbmastDt Is Nothing Then
                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM KBMAST")
                _kbmastDt = iDatabase.ExecuteRead(sql.ToString())
            End If

            If _kbmastDt.Rows.Count <= 0 Then Return

            ' EIGMTBの取得
            If _eigmtbDt Is Nothing Then
                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM EIGMTB")
                _eigmtbDt = iDatabase.ExecuteRead(sql.ToString())
            End If

            Dim query = From x In _enttraDt.AsEnumerable
                        Where x("DATKB").ToString = "1" And x("ENTDT").ToString = strDate
                        Order By x("ENTNO") Descending

            If query.Count <= 0 Then Return

            ' 種別14を除いた最終結果を取得
            Dim query_final = query.Where(Function(x) Not x("KSBKB").ToString = "14")
            Dim max_count = query_final.Count

            ' 指定ページ分取り出す
            Dim staPage = currentPage * RECORD_OF_PAGE
            Dim endPage = RECORD_OF_PAGE
            If (max_count - staPage) < endPage Then
                endPage = max_count - staPage
            End If
            Dim query_paging = query_final.Skip(staPage).Take(endPage).ToList()

            Dim i = max_count - staPage
            For Each row In query_paging
                Dim data = New ENTHIST_View
                Dim enttra = row
                If enttra Is Nothing Then Exit For

                ' 受付順
                data.ENTNO = i.ToString.PadLeft(4, "0"c)

                ' 時間
                data.ENTTM = DateTime.Parse(enttra("INSDTM").ToString).ToString("HH:mm")

                ' 受付内容
                Dim kbmast = _kbmastDt.AsEnumerable.Where(Function(x) x("NKBNO").ToString = enttra("KSBKB").ToString).First()
                ' EIGKB 1:1球貸し 2:打ち放題
                If enttra("EIGKB").ToString.Equals("2") Then
                    ' 打ち放題なら「打ち放題+JKNMM+分」と表示
                    If _eigmtbDt.Rows.Count > 0 Then
                        Dim eigmtb = _eigmtbDt.AsEnumerable.Where(Function(x) x("RKNKB").ToString = enttra("RKNKB").ToString And x("JKNKB").ToString = enttra("KSBKB").ToString).First()
                        data.CKBNAME = "打ち放題" & eigmtb("JKNMM").ToString & "分"
                    Else
                        data.CKBNAME = "打ち放題"
                    End If
                Else
                    data.CKBNAME = kbmast("CKBNAME").ToString
                End If

                ' 顧客番号
                data.MANNO = enttra("MANNO").ToString.PadLeft(8, "0"c)

                ' 氏名
                If CInt(data.MANNO) >= 1000000 Then
                    data.CCSNAME = "貸出カード"
                Else
                    data.CCSNAME = _csmastDt.AsEnumerable.Where(Function(x) x("NCSNO").ToString = data.MANNO.TrimStart("0"c)).First()("CCSNAME").ToString
                End If

                ' 入場料
                data.ENTBKN = CInt(enttra("ENTBKN").ToString)

                'ポイント
                data.POINT = CType(enttra("SRTPO").ToString, Integer)

                i -= 1

                list.Add(data)
            Next

            ' 件数の表示            
            lblCount.Text = "全" & query_final.Count.ToString & "件"

            ' ページングの制御
            _maxPage = 0
            If max_count > 1 Then
                _maxPage = CInt(Math.Floor((max_count - 1) / RECORD_OF_PAGE))
            End If

            btnPrev.Enabled = currentPage > 0
            btnNext.Enabled = currentPage < _maxPage

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' 入場トランの取得(SQL改善)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetENTTRA(ByVal currentPage As Integer, Optional ByVal bRefresh As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim list As BindingList(Of ENTHIST_View) = New BindingList(Of ENTHIST_View)
        Me.dgvENTHIST.DataSource = list
        Try
            ' 更新
            If bRefresh Then

                Dim strNow = DateTime.Now.ToString("yyyyMMdd")

                If bRefresh Then
                    sql.Clear()
                    sql.Append(" SELECT")
                    sql.Append(" *")
                    sql.Append(" FROM ENTTRA AS A")
                    sql.Append(" LEFT OUTER JOIN CSMAST AS B")
                    sql.Append(" ON A.MANNO = TO_CHAR(B.NCSNO, 'FM00000000')")
                    sql.Append(" WHERE A.DATKB = '1'")
                    sql.Append(" AND A.KSBKB != '14'")
                    sql.Append(String.Format(" AND A.ENTDT = '{0}'", strNow))
                    sql.Append(" ORDER BY A.ENTNO DESC")
                    _enttraDt = iDatabase.ExecuteRead(sql.ToString())
                End If

                If _enttraDt.Rows.Count <= 0 Then Return False

                _joinEnttraQuery = From x In _enttraDt.AsEnumerable
                                    Select New JoinENTTRA_View With {
                                    .ENTNO = x("ENTNO").ToString.PadLeft(4, "0"c),
                                    .ENTTM = DateTime.Parse(x("INSDTM").ToString).ToString("HH:mm"),
                                    .EIGKB = x("EIGKB").ToString,
                                    .MANNO = x("MANNO").ToString,
                                    .ENTBKN = CInt(x("ENTBKN")),
                                    .SRTPO = CInt(x("SRTPO")),
                                    .RKNKB = x("RKNKB").ToString,
                                    .KSBKB = x("KSBKB").ToString,
                                    .CCSNAME = x("CCSNAME").ToString,
                                    .NCSRANK = x("NCSRANK").ToString
                                    }

                ' KBMASTの取得

                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM KBMAST")
                _kbmastDt = iDatabase.ExecuteRead(sql.ToString())

                If _kbmastDt.Rows.Count <= 0 Then Return False

                ' EIGMTBの取得

                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM EIGMTB")
                _eigmtbDt = iDatabase.ExecuteRead(sql.ToString())

            End If

            ' 指定ページ分取り出す
            Dim max_count = _joinEnttraQuery.Count
            Dim staPage = currentPage * RECORD_OF_PAGE
            Dim endPage = RECORD_OF_PAGE
            If (max_count - staPage) < endPage Then
                endPage = max_count - staPage
            End If
            Dim query_paging = _joinEnttraQuery.Skip(staPage).Take(endPage).ToList()

            Dim i = max_count - staPage
            For Each row In query_paging
                Dim data = New ENTHIST_View

                ' フリーカードの判別
                Dim blnFreeCard = Not String.IsNullOrEmpty(row.MANNO) And CInt(row.MANNO) >= 50000000

                ' 受付順
                data.ENTNO = i.ToString.PadLeft(4, "0"c) 'row.ENTNO

                ' 時間
                data.ENTTM = row.ENTTM

                ' 受付内容
                ' EIGKB 1:1球貸し 2:打ち放題
                If row.EIGKB = "2" Then
                    ' 打ち放題なら「打ち放題+JKNMM+分」と表示
                    If _eigmtbDt.Rows.Count > 0 Then
                        Dim rknkb = row.RKNKB
                        Dim ksbkb = row.KSBKB
                        Dim eigmtb = _eigmtbDt.AsEnumerable.Where(Function(x) x("RKNKB").ToString = rknkb And x("JKNKB").ToString = ksbkb).First()
                        data.CKBNAME = "打ち放題" & eigmtb("JKNMM").ToString & "分"
                    Else
                        data.CKBNAME = "打ち放題"
                    End If
                Else
                    ' MANNO = NULL or フリーカード なら ENTTRA の KSBKB をキーに、
                    ' MANNO が存在すれば CSMAST の NCSRANK をキーに
                    ' KBMST から CKBNAME を参照する
                    Dim key = row.KSBKB
                    If Not blnFreeCard And Not String.IsNullOrEmpty(row.MANNO) Then
                        key = row.NCSRANK
                    End If
                    ' 種別の取得
                    If Not String.IsNullOrEmpty(key) Then
                        Dim kbmast = _kbmastDt.AsEnumerable.Where(Function(x) x("NKBNO").ToString = key).First()
                        data.CKBNAME = kbmast("CKBNAME").ToString
                    End If
                End If

                ' 顧客番号
                If Not String.IsNullOrEmpty(row.MANNO) Then
                    data.MANNO = row.MANNO.PadLeft(8, "0"c)
                End If

                ' 氏名
                If blnFreeCard Then
                    data.CCSNAME = "貸出カード"
                Else
                    data.CCSNAME = row.CCSNAME
                End If
                
                ' 入場料
                data.ENTBKN = row.ENTBKN

                'ポイント
                data.POINT = row.SRTPO

                i -= 1

                list.Add(data)
            Next

            ' 件数の表示            
            lblCount.Text = "全" & max_count.ToString & "件"

            ' ページングの制御
            _maxPage = 0
            If max_count > 1 Then
                _maxPage = CInt(Math.Floor((max_count - 1) / RECORD_OF_PAGE))
            End If

            btnPrev.Enabled = currentPage > 0
            btnNext.Enabled = currentPage < _maxPage

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' エラーメッセージの表示
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Private Sub ShowErrorMessage(ByVal msg As String)
        MessageBox.Show(msg, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

End Class
