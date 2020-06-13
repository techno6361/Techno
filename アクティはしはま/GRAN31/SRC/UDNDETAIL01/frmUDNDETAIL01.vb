Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmUDNDETAIL01

    ' デバッグモード
    Const _DEBUG = False

    ''' <summary>
    ''' テストデータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Test()
        _BUNCDA = "003"
        _PROCESS = PROCESS_Type.CANCEL
        _MANNO = Nothing
        _CCSNAME = "ﾃｸﾉﾀﾛｳ"
    End Sub

#Region "▼宣言部"

    ' 列挙体_BUNCDA
    Enum BUNCDA_Type
        SHOHIN
        TICHET
        NYUKIN
        SNYUKIN
        NYUJO
        SHISYUTU
        POINT
    End Enum

    ' 列挙体_PROCESS
    Enum PROCESS_Type
        CANCEL = 1
        REVISE = 2
        PRINT = 3
    End Enum

    ' 書き込み専用
    Private _BUNCDA As String
    Private _PROCESS As Integer
    Private _MANNO As String
    Private _CCSNAME As String

    Public WriteOnly Property BUNCDA As BUNCDA_Type
        Set(ByVal value As BUNCDA_Type)
            Dim table = New Dictionary(Of BUNCDA_Type, String) _
                        From {{BUNCDA_Type.SHOHIN, DB.BUNCDA.SHOHIN}, _
                              {BUNCDA_Type.TICHET, DB.BUNCDA.TICHET}, _
                              {BUNCDA_Type.NYUKIN, DB.BUNCDA.NYUKIN}, _
                              {BUNCDA_Type.SNYUKIN, DB.BUNCDA.SNYUKIN}, _
                              {BUNCDA_Type.NYUJO, DB.BUNCDA.NYUJO}, _
                              {BUNCDA_Type.SHISYUTU, DB.BUNCDA.SHISYUTU}, _
                              {BUNCDA_Type.POINT, DB.BUNCDA.POINT}}
            _BUNCDA = table(value)
        End Set
    End Property

    Public WriteOnly Property PROCESS As PROCESS_Type
        Set(ByVal value As PROCESS_Type)
            _PROCESS = value
        End Set
    End Property

    Public WriteOnly Property MANNO As String
        Set(ByVal value As String)
            _MANNO = value
        End Set
    End Property

    Public WriteOnly Property CCSNAME As String
        Set(ByVal value As String)
            _CCSNAME = value
        End Set
    End Property

    ' 読み取り専用
    Private _UDNKN As Integer
    Private _PREMKN As Integer
    Private _POINT As Integer
    'Private _ZENNKNKN As Integer
    Private _CANCEL As Boolean
    Private _DENNO As String
    Private _UDNBKN As Integer
    Private _TKTKBN As String
    Private _CPAYKBN As Integer

    Public ReadOnly Property UDNKN As Integer
        Get
            Return _UDNKN
        End Get
    End Property

    Public ReadOnly Property PREMKN As Integer
        Get
            Return _PREMKN
        End Get
    End Property

    Public ReadOnly Property POINT As Integer
        Get
            Return _POINT
        End Get
    End Property

    'Public ReadOnly Property ZENNKNKN As Integer
    '    Get
    '        Return _ZENNKNKN
    '    End Get
    'End Property

    Public ReadOnly Property CANCEL As Boolean
        Get
            Return _CANCEL
        End Get
    End Property

    Public ReadOnly Property DENNO As String
        Get
            Return _DENNO
        End Get
    End Property

    Public ReadOnly Property UDNBKN As Integer
        Get
            Return _UDNBKN
        End Get
    End Property

    Public ReadOnly Property TKTKBN As String
        Get
            Return _TKTKBN
        End Get
    End Property

    Public ReadOnly Property CPAYKBN As Integer
        Get
            Return _CPAYKBN
        End Get
    End Property

    Private Const DEFAULT_BTN_SELECT_TEXT As String = "伝票が選択されていません"

    ' 選択ボタンを押してもDBを更新させないためのプロパティ
    Private _UpdateDisabled As Boolean = False
    Public Property UpdateDisabled As Boolean
        Get
            Return _UpdateDisabled
        End Get
        Set(value As Boolean)
            _UpdateDisabled = value
        End Set
    End Property

    ' 特定ホストで表示を制限するためのプロパティ
    Public Property CurrentHostOnly As Boolean = False

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "伝票番号選択画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "伝票番号選択画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod, ByVal ICR700 As TECHNO.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品引き落とし"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700


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
    Private Sub frmDRA01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Start()
            ' グリッドビューの不具合があるので再実行
            GetUDNDETAIL()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボタン_戻る
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRETURN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRETURN.Click
        Try
            _CANCEL = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボタン_伝票番号決定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try        
            Dim udndt = dgvUDNDETAIL.CurrentRow.Cells(10).Value.ToString ' 伝票日付
            Dim denno = dgvUDNDETAIL.CurrentRow.Cells(2).Value.ToString ' 伝票番号
            Dim udnno = CInt(denno.TrimStart("0"c))

            ' 未更新フラグがある場合何もせずに閉じる
            If _UpdateDisabled Then
                SetProperties(udndt, udnno, denno, _BUNCDA)
                Me.Close()
                Exit Sub
            End If

            ' 処理区分がキャンセル(PROCESS.CANCEL)のときのみ分類コード(BUNCDA)により各テーブルの削除区分(DATKB)に9を更新
            If _PROCESS = PROCESS_Type.CANCEL Then
                UpdateTables(GetUpdateTableList(_BUNCDA), udndt, udnno, "9", iDatabase)
            End If

            SetProperties(udndt, udnno, denno, _BUNCDA)

            Me.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_行選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvUDNDETAIL_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvUDNDETAIL.SelectionChanged
        Try
            ' 空白行は無視
            If dgvUDNDETAIL.CurrentRow Is Nothing Then Return

            ' 選択可否のフラグ(デフォルト:True)
            Dim bSelect As Boolean = True

            ' PROCESS.PRINT 以外かつドロアチェック済みなら選択不可にする
            If Not _PROCESS = PROCESS_Type.PRINT Then
                If dgvUDNDETAIL.CurrentRow.Cells(5).Value.ToString.Equals(DB.DRAKBN.CHECKED) Then bSelect = False
            End If

            ' BUNCDAがNYUJOかつPROCESSがCANCELの場合、一行目以外選択できないようにする
            If _BUNCDA = DB.BUNCDA.NYUJO And _PROCESS = PROCESS_Type.CANCEL Then
                If Not dgvUDNDETAIL.CurrentRow.Index = 0 Then bSelect = False
            End If

            ' 選択可否の判定
            If bSelect Then
                ' 選択可
                Dim denno = dgvUDNDETAIL.CurrentRow.Cells(2).Value.ToString
                btnSelect.Text = "伝票番号「" & denno & "」の伝票を選択"
                btnSelect.Enabled = True
            Else
                ' 選択不可
                dgvUDNDETAIL.ClearSelection()
                btnSelect.Text = DEFAULT_BTN_SELECT_TEXT
                btnSelect.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' メイン開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Start()
        Try
            ' テストデータの呼び出し
            If _DEBUG Then Test()

            ' 初期化
            Init()

            ' 売り上げ詳細の表示
            GetUDNDETAIL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 画面初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            ' 余計なボタンを非表示
            Me.tspFunc1.Enabled = False
            Me.tspFunc3.Enabled = False
            Me.tspFunc8.Enabled = False
            Me.tspFunc9.Enabled = False
            Me.tspFunc11.Enabled = False
            Me.tspFunc12.Enabled = False

            ' 選択ボタンの初期化
            btnSelect.Text = DEFAULT_BTN_SELECT_TEXT
            btnSelect.Enabled = False

            ' BUNCDAがPOINTの場合、列名「合計金額」を「プレミアム/チケット」に変更
            If _BUNCDA = DB.BUNCDA.POINT Then
                Me.dgvUDNDETAIL.Columns(3).HeaderText = "ﾌﾟﾚﾐﾑｱﾑ/ﾁｹｯﾄ"
            Else
                Me.dgvUDNDETAIL.Columns(3).HeaderText = "合計金額"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 売り上げ情報を取得しグリッドに一覧表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetUDNDETAIL()
        Dim sql As New System.Text.StringBuilder
        Dim total = 0
        Dim list As BindingList(Of UDNDETAIL_View) = New BindingList(Of UDNDETAIL_View)
        Me.dgvUDNDETAIL.DataSource = list
        Try
            Dim udndt = Now.ToString("yyyyMMdd")

            ' 商品売上トランの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE UDNDT = '" & udndt & "'")
            sql.Append(" AND BMNCD = '002'")
            If _BUNCDA = DB.BUNCDA.SHOHIN Then
                sql.Append(" AND BUNCDC <> '999'")
            End If
            sql.Append(String.Format(" AND BUNCDA = '{0}'", _BUNCDA))

            ' *** STA ADD 2018/04/11 TERAYAMA 特定のホスト以外非表示
            If CurrentHostOnly Then
                sql.Append(String.Format(" AND HOSTNAME = '{0}'", My.Computer.Name))
            End If
            ' *** END ADD

            ' PROCESS.PRINT 以外は削除区分「１」のみ表示
            If Not _PROCESS = PROCESS_Type.PRINT Then
                sql.Append(String.Format(" AND DATKB = '{0}'", 1))
            End If

            ' MANNO がNULL以外なら検索条件にMANNOの条件を加える
            If Not String.IsNullOrEmpty(_MANNO) Then
                sql.Append(String.Format(" AND MANNO = '{0}'", _MANNO))
            End If

            sql.Append(" ORDER BY INSDTM DESC")

            ' SQL実行
            Dim dudntrnDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dudntrnDt.Rows.Count <= 0 Then
                Exit Sub
            End If

            Dim uriageQuery = dudntrnDt.AsEnumerable

            ' 伝票トランの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE UDNDT = '" & udndt & "'")
            If _BUNCDA = DB.BUNCDA.SHOHIN Then
                sql.Append(" AND BUNCDC <> '999'")
            End If
            sql.Append(" ORDER BY LINNO DESC")
            Dim dentraDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dentraDt.Rows.Count <= 0 Then
                Exit Sub
            End If

            ' 商品売上をビューに反映
            For Each row As DataRow In uriageQuery
                Dim data = New UDNDETAIL_View
                data.UDNDT = DateTime.Parse(row("INSDTMSTR").ToString).ToString("MM/dd HH:mm")

                data.DATKB = String.Empty
                Select Case row("DATKB").ToString
                    Case "1"
                        data.DATKB = DB.DATKB.UNDELETED
                    Case "9"
                        data.DATKB = DB.DATKB.DELETED
                End Select

                data.UDNNO = row("UDNNO").ToString.PadLeft(4, "0"c)
                data.UDNBKN = CInt(row("UDNBKN"))
                data.HOSTNAME = row("HOSTNAME").ToString

                data.DRAKBN = String.Empty
                Select Case row("DRAKBN").ToString
                    Case "0"
                        data.DRAKBN = DB.DRAKBN.UNCHECKED
                    Case "1"
                        data.DRAKBN = DB.DRAKBN.CHECKED
                End Select

                ' *** STA ADD 2018/08/31 TERAYAMA
                ' 支払い区分
                Select Case row("CPAYKBN").ToString
                    Case "0"
                        data.CPAYKBN = DB.CPAYKBN.KBN_0
                    Case "1"
                        data.CPAYKBN = DB.CPAYKBN.KBN_1
                    Case "2"
                        data.CPAYKBN = DB.CPAYKBN.KBN_2
                    Case "3"
                        data.CPAYKBN = DB.CPAYKBN.KBN_3
                    Case Else
                        data.CPAYKBN = DB.CPAYKBN.KBN_4
                End Select
                ' *** END

                data.MANNO = row("MANNO").ToString

                ' 顧客番号が50000000番以上の場合、氏名をフリーカードと表示
                Dim manno As Integer
                Dim result = Int32.TryParse(data.MANNO, manno)
                If result Then
                    If CInt(manno) >= 50000000 Then
                        data.CCSNAME = "貸出カード"
                    Else
                        ' BUNCDAがTICKETのとき:スクール生氏名 それ以外:顧客氏名
                        If _BUNCDA = DB.BUNCDA.TICHET Then
                            data.CCSNAME = getSCLMANNM(data.MANNO)
                        Else
                            data.CCSNAME = getMANNM(data.MANNO)
                        End If
                    End If
                Else
                    data.CCSNAME = String.Empty
                End If

                ' _CCSNAMEが指定していれば_CCSNAMEを設定
                If Not String.IsNullOrEmpty(_CCSNAME) Then
                    data.CCSNAME = _CCSNAME
                End If

                ' 同じ伝票番号で複数伝票があれば繋げて表示
                Dim udnno = CInt(row("UDNNO"))
                Dim dentraQuery = dentraDt.AsEnumerable.Where(Function(x As DataRow) x("UDNNO").Equals(udnno)).OrderBy(Function(x As DataRow) x("INSDTM"))
                If dentraQuery.Count >= 1 Then
                    Dim hinnmas = dentraQuery.Select(Function(x As DataRow) x("HINNMA")).ToList
                    data.HINNMA = String.Join(",", hinnmas)
                Else
                    data.HINNMA = row("HINNMA").ToString
                End If

                data.hide_UDNDT = DateTime.Parse(row("INSDTMSTR").ToString).ToString("yyyyMMdd")

                list.Add(data)
            Next

            ' PROCESS.PRINT(以外かつドロアチェック済みなら選択不可にする)
            If Not _PROCESS = PROCESS_Type.PRINT Then
                For Each row As DataGridViewRow In Me.dgvUDNDETAIL.Rows
                    If row.Cells(5).Value.ToString().Equals(DB.DRAKBN.CHECKED) Then
                        row.DefaultCellStyle.BackColor = SystemColors.ControlDark
                    Else
                        row.DefaultCellStyle.BackColor = SystemColors.Window
                    End If
                Next
            End If

            ' BUNCDAがNYUJOかつPROCESSがCANCELの場合、一行目以外選択できないようにする
            If _BUNCDA = DB.BUNCDA.NYUJO And _PROCESS = PROCESS_Type.CANCEL Then
                Dim j As Integer
                For Each row As DataGridViewRow In Me.dgvUDNDETAIL.Rows
                    If j = 0 Then
                        row.DefaultCellStyle.BackColor = SystemColors.Window
                    Else
                        row.DefaultCellStyle.BackColor = SystemColors.ControlDark
                    End If
                    j += 1
                Next
            End If

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 顧客氏名を取得する
    ''' </summary>
    ''' <param name="manno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getMANNM(ByVal manno As String) As String

        If String.IsNullOrEmpty(manno) Then
            Return String.Empty
        End If

        Dim sql As New System.Text.StringBuilder

        Try

            ' 氏名取得用のテーブル取得(顧客マスタ)
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM CSMAST")
            sql.Append(String.Format(" WHERE NCSNO = {0}", manno.TrimStart("0"c)))

            Dim csDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If csDt.Rows.Count <= 0 Then Return String.Empty

            Return csDt.AsEnumerable.Select(Function(x As DataRow) x("CCSNAME")).First().ToString

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' スクール生氏名を取得する
    ''' </summary>
    ''' <param name="sclmanno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getSCLMANNM(ByVal sclmanno As String) As String

        If String.IsNullOrEmpty(sclmanno) Then
            Return String.Empty
        End If

        Dim sql As New System.Text.StringBuilder
        Try

            ' 氏名取得用のテーブル取得(スクール生マスタ)
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM MANMST")
            sql.Append(String.Format(" WHERE SCLMANNO = '{0}'", sclmanno))

            Dim manDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If manDt.Rows.Count <= 0 Then Return String.Empty

            Return manDt.AsEnumerable.Select(Function(x As DataRow) x("MANNM")).First().ToString

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 更新テーブルを取得
    ''' </summary>
    ''' <param name="buncda"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetUpdateTableList(ByVal buncda As String) As List(Of String)
        Dim tables As List(Of String) = New List(Of String)
        Select Case buncda
            Case DB.BUNCDA.SHOHIN
                tables.Add(DB.TABLE.DUDNTRN)
                tables.Add(DB.TABLE.DENTRA)
                tables.Add(DB.TABLE.HINTRN)
            Case DB.BUNCDA.TICHET
                ' 現状未実装
            Case DB.BUNCDA.NYUKIN
                tables.Add(DB.TABLE.DUDNTRN)
                tables.Add(DB.TABLE.DENTRA)
                tables.Add(DB.TABLE.NKNTRN)
            Case DB.BUNCDA.SNYUKIN
                tables.Add(DB.TABLE.DUDNTRN)
                tables.Add(DB.TABLE.DENTRA)
                tables.Add(DB.TABLE.NKNTRN)
            Case DB.BUNCDA.NYUJO
                tables.Add(DB.TABLE.DUDNTRN)
                tables.Add(DB.TABLE.DENTRA)
                tables.Add(DB.TABLE.ENTTRA)
            Case DB.BUNCDA.SHISYUTU
                tables.Add(DB.TABLE.DUDNTRN)
                tables.Add(DB.TABLE.DENTRA)
                tables.Add(DB.TABLE.KOSUTRN)
            Case DB.BUNCDA.POINT
                tables.Add(DB.TABLE.DUDNTRN)
                tables.Add(DB.TABLE.DENTRA)
        End Select
        Return tables
    End Function

    ''' <summary>
    ''' 複数TABLEにUPDATE
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function UpdateTables(ByVal tables As List(Of String), ByVal udndt As String, ByVal udnno As Integer, ByVal value As String, iDB As TECHNO.DataBase.IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            'トランザクション開始
            iDB.BeginTransaction()

            For Each table In tables
                sql.Clear()
                sql.Append("UPDATE " & table & " SET")
                sql.Append(" DATKB = " & value)
                sql.Append(" WHERE ")

                '各テーブルによってフィールド名が異なるので補正
                Select Case table
                    Case DB.TABLE.DUDNTRN
                        sql.Append(" UDNDT = '" & udndt & "' AND")
                        sql.Append(" UDNNO = " & udnno)
                    Case DB.TABLE.DENTRA
                        sql.Append(" UDNDT = '" & udndt & "' AND")
                        sql.Append(" UDNNO = " & udnno)
                    Case DB.TABLE.NKNTRN
                        sql.Append(" DENDT = '" & udndt & "' AND")
                        sql.Append(" DENNO = " & udnno)
                    Case DB.TABLE.ENTTRA
                        sql.Append(" ENTDT = '" & udndt & "' AND")
                        sql.Append(" ENTNO = " & udnno)
                    Case DB.TABLE.HINTRN
                        sql.Append(" DENDT = '" & udndt & "' AND")
                        sql.Append(" DENNO = " & udnno)
                    Case DB.TABLE.KOSUTRN
                        sql.Append(" DENDT = '" & udndt & "' AND")
                        sql.Append(" DENNO = " & udnno)
                End Select

                If Not iDB.ExecuteUpdate(sql.ToString) Then
                    iDB.RollBack()
                    Return False
                End If
            Next

            'コミット処理
            iDB.Commit()

            Return True

        Catch ex As Exception
            iDB.RollBack()
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 処理プロパティ列挙体メンバ共通処理        
    ''' </summary>
    ''' <param name="udndt"></param>
    ''' <param name="udnno"></param>
    ''' <remarks></remarks>
    Public Sub SetProperties(ByVal udndt As String, ByVal udnno As Integer, ByVal denno As String, ByVal buncda As String)
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DUDNTRN")
            sql.Append(String.Format(" WHERE UDNDT = '{0}' AND UDNNO = {1}", udndt, udnno))

            Dim dudntrnDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If dudntrnDt.Rows.Count <= 0 Then Exit Sub

            Dim query = dudntrnDt.AsEnumerable.First()

            ' 呼び出し元に返すプロパティを設定
            _UDNKN = CInt(query("UDNKN"))
            _PREMKN = CInt(query("PREMKN"))
            _POINT = CInt(query("POINT"))
            _CANCEL = False
            _DENNO = denno
            'If buncda.Equals(DB.BUNCDA.NYUKIN) Then
            '    _ZENNKNKN = GetZENNKNKN()
            'Else
            '    _ZENNKNKN = GetZENNKNKN()
            'End If
            _UDNBKN = CInt(query("UDNBKN"))
            _TKTKBN = query("TKTKBN").ToString
            _CPAYKBN = CInt(query("CPAYKBN").ToString)

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 前回入金額を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetZENNKNKN() As Integer
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNTRN")

            Dim dt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If dt.Rows.Count <= 0 Then Return 0

            ' 検索条件
            Dim datkb = "1"
            Dim stsflg = "0"

            Dim query = dt.AsEnumerable. _
                Where(Function(x) x("DATKB").Equals(datkb) And x("STSFLG").Equals(stsflg)). _
                OrderByDescending(Function(x) x("INSDTM"))

            If query.Count > 0 Then
                Return CInt(query(0)("NKNKN").ToString)
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

#End Region

#Region "▼ 公開メソッド"

    ''' <summary>
    ''' frmHINDISP01で伝票取消処理を行った際にDB更新を遅延実行するために使用する
    ''' </summary>
    ''' <param name="udnno"></param>
    ''' <param name="iDB"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateTablesForHINDISP01(ByVal udnno As Integer, iDB As TECHNO.DataBase.IDatabase.IMethod) As Boolean
        Dim udndt = DateTime.Now.ToString("yyyyMMdd")
        Return UpdateTables(GetUpdateTableList(DB.BUNCDA.SHOHIN), udndt, udnno, "9", iDB)
    End Function

#End Region

End Class

' 定数
Namespace DB
    Class BUNCDA
        Public Const SHOHIN As String = "001"
        Public Const TICHET As String = "002"
        Public Const NYUKIN As String = "003"
        Public Const SNYUKIN As String = "004"
        Public Const NYUJO As String = "005"
        Public Const SHISYUTU As String = "006"
        Public Const POINT As String = "007"
    End Class

    Class TABLE
        Public Const DUDNTRN As String = "DUDNTRN"
        Public Const DENTRA As String = "DENTRA"
        Public Const ENTTRA As String = "ENTTRA"
        Public Const NKNTRN As String = "NKNTRN"
        Public Const HINTRN As String = "HINTRN"
        Public Const KOSUTRN As String = "KOSUTRN"
    End Class

    Class DRAKBN
        Public Const CHECKED As String = "済"
        Public Const UNCHECKED As String = "-"
    End Class

    Class DATKB
        Public Const DELETED As String = "消"
        Public Const UNDELETED As String = ""
    End Class

    Class CPAYKBN
        Public Const KBN_0 As String = "現金"
        Public Const KBN_1 As String = "カード払い"
        Public Const KBN_2 As String = "商品券"
        Public Const KBN_3 As String = "銀行振込"
        Public Const KBN_4 As String = "打席カード"
    End Class
End Namespace
