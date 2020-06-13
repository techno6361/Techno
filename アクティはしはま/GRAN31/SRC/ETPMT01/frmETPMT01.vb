Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmETPMT01

#Region "▼宣言部"

    ' データグリッドとバインドするリスト
    Dim _list As BindingList(Of ETPMTA_View) = New BindingList(Of ETPMTA_View)

    ' ロード時にコンボボックスのイベントが発動してしまう不具合の解消
    Dim _inited As Boolean = False

    '画面表示時の最新の更新日時
    Private _dtUPDDTM As DateTime

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "月間来場ポイントマスタ"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "月間来場ポイントマスタ"

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
    Private Sub frmETPMT01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' 初期化
            Init()
            ' 月間来場ポイントマスタの取得(デフォルト:種別１)
            GetETPMTA(1)
        Catch ex As Exception
            Throw ex
        End Try        
    End Sub

    ''' <summary>
    ''' コンボボックス_変更
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbNKBNO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNKBNO.TextChanged
        Try
            If _inited = False Then Return
            GetETPMTA(GetCurrentNKBNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' コピー表示ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCOPY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCOPY.Click
        Try
            If Me.cmbNKBNO_COPY.Text = String.Empty Then
                Using frm As New frmMSGBOX01("コピー元顧客種別が選択されていません。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Dim xxx = Me.cmbNKBNO_COPY.Text
            Using frm As New frmMSGBOX01(xxx & "の来場ポイント情報をコピーしてよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            If Not Me.cmbNKBNO_COPY.Text = String.Empty Then
                GetETPMTA(GetCopyNKBNO)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' F11クリアボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func11()
        Try
            'セルの編集終了
            Me.dgvETPMTA.EndEdit()

            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            ' 初期化
            Init()

            ' 月間来場ポイントマスタの取得(デフォルト:種別１)
            GetETPMTA(1)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try
            'セルの編集終了
            Me.dgvETPMTA.EndEdit()

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            Cursor = Cursors.WaitCursor

            ' 他端末からの更新チェック
            Dim dtUPDDTM = GetCurrentUPDDTM()
            If Not dtUPDDTM.Equals(Nothing) Then
                If Not dtUPDDTM.Equals(_dtUPDDTM) Then
                    Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

            ' Nullを空白に置き換え(バグ解消)
            For Each x In _list
                If String.IsNullOrEmpty(x.ENTCNT) Then
                    x.ENTCNT = String.Empty
                End If
            Next

            ' 一つもデータがなければ強制終了
            Dim query = _list.Select(Function(x As ETPMTA_View) x.ENTCNT)
            If query.Count <= 0 Then
                Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                    frm.ShowDialog()
                End Using
                GetETPMTA(GetCurrentNKBNO)
                Exit Sub
            End If

            ' ソート
            Dim sortList = _list.OrderBy(Of String)(Function(x As ETPMTA_View) x.ENTCNT.PadLeft(2, "0"c))
            _list = New BindingList(Of ETPMTA_View)(sortList.ToList())

            ' 項目チェック
            If Not CheckENTCNT() Then
                Using frm As New frmMSGBOX01("来場回数が重複しています。", 2)
                    frm.ShowDialog()
                End Using
                Me.dgvETPMTA.Focus()
                Exit Sub
            End If

            ' 顧客種別登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("顧客種別の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Me.dgvETPMTA.Focus()
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            ' 顧客種別情報取得
            GetETPMTA(GetCurrentNKBNO)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "▼ 入力制御とフォーマット"

    ''' <summary>
    ''' グリッド_EditingControlShowing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvETPMTA_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvETPMTA.EditingControlShowing
        Try
            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
                Dim dgv As DataGridView = CType(sender, DataGridView)

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                'イベントハンドラ削除
                RemoveHandler tb.KeyPress, AddressOf dgvETPMTA_KeyPress

                '該当する列か調べる
                If dgv.CurrentCell.OwningColumn.Index.Equals(1) Or dgv.CurrentCell.OwningColumn.Index.Equals(2) Then
                    'KeyPressイベントハンドラ追加
                    AddHandler tb.KeyPress, AddressOf dgvETPMTA_KeyPress
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvETPMTA_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_CellEnter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvETPMTA_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvETPMTA.CellEnter
        Try
            Select Case e.ColumnIndex
                Case 0
                    Me.dgvETPMTA.ImeMode = Windows.Forms.ImeMode.Off
                Case 1
                    Me.dgvETPMTA.ImeMode = Windows.Forms.ImeMode.Off
                Case 2
                    Me.dgvETPMTA.ImeMode = Windows.Forms.ImeMode.Off
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_フォーマット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, _
            ByVal e As DataGridViewCellFormattingEventArgs) _
            Handles dgvETPMTA.CellFormatting
        Try
            ' ポイント列のみカンマ表示
            If e.ColumnIndex = 2 Then
                If Not String.IsNullOrEmpty(e.Value.ToString) Then
                    e.Value = CInt(e.Value).ToString("#,0")
                End If
                'フォーマットの必要がないことを知らせる
                e.FormattingApplied = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            ' 余計なボタンを非表示
            Me.tspFunc3.Enabled = False
            Me.tspFunc8.Enabled = False
            Me.tspFunc9.Enabled = False

            ' 顧客区分マスタの取得とコンボボックスの初期化
            GetKBMAST()

            ' ReadOnlyセルにフォーカスを当てない
            Me.dgvETPMTA.ReadOnlyThrough = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 顧客区分マスタの取得とコンボボックスの初期化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" CKBNAME")
            sql.Append(" FROM KBMAST")
            sql.Append(" WHERE NKBNO <= " & UIUtility.SYSTEM.MAXNKBNO) ' 顧客種別は1～10まで操作可能
            sql.Append(" ORDER BY NKBNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            ' *** STA コンボボックスの初期化処理
            _inited = False

            cmbNKBNO.Items.Clear()
            cmbNKBNO_COPY.Items.Clear()

            For Each row As DataRow In resultDt.Rows
                If Not String.IsNullOrEmpty(row("CKBNAME").ToString) Then
                    cmbNKBNO.Items.Add(row("CKBNAME").ToString())
                    cmbNKBNO_COPY.Items.Add(row("CKBNAME").ToString())
                End If
            Next
            cmbNKBNO.Text = CStr(cmbNKBNO.Items(0))
            cmbNKBNO_COPY.Text = String.Empty

            _inited = True
            ' *** END コンボボックスの初期化処理

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 月間来場ポイントマスタの取得(デフォルト:1)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetETPMTA(ByVal nkbno As Integer) As Boolean
        ' フォーカス不具合の解消
        Me.dgvETPMTA.Focus()

        Dim sql As New System.Text.StringBuilder
        Try


            ' リストの削除
            _list.Clear()

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM ETPMTA")
            sql.Append(" WHERE NKBNO = " & nkbno.ToString())
            sql.Append(" ORDER BY ETPKBN,ENTCNT")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' グリッドにデータをバインド
            Me.dgvETPMTA.DataSource = _list

            ' 01～31 のビューを作成
            For i = 1 To 31
                Dim id = i.ToString().PadLeft(2, "0"c)
                Dim query = resultDt.AsEnumerable.Where(Function(x As DataRow) x("ETPKBN").Equals(id))
                Dim cnt = String.Empty
                Dim point = String.Empty
                If query.Count > 0 Then
                    cnt = CStr(query.Select(Function(x As DataRow) x("ENTCNT")).First())
                    point = CStr(query.Select(Function(x As DataRow) x("POINT")).First())
                End If
                _list.Add(New ETPMTA_View(id, cnt, point))
            Next

            '最新の更新日時を取得
            _dtUPDDTM = GetCurrentUPDDTM()

            ' フォーカス不具合の解消
            Me.dgvETPMTA.CurrentCell = Nothing

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Register() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            'トランザクション開始
            iDatabase.BeginTransaction()

            If Not iDatabase.ExecuteUpdate("DELETE FROM ETPMTA WHERE NKBNO = " & GetCurrentNKBNO()) Then
                Return False
            End If

            ' 実際に挿入されるENTKBN
            Dim entkbn As Integer = 1

            For Each row In _list
                ' 空白 or 0 で削除
                If row.ENTCNT Is Nothing Then
                    Continue For
                End If

                row.ENTCNT = row.ENTCNT.TrimStart("0"c)
                If String.IsNullOrEmpty(row.ENTCNT) Or String.IsNullOrEmpty(row.POINT) Or row.ENTCNT.Equals("0") Then
                    Continue For
                End If

                sql.Clear()
                sql.Append("INSERT INTO ETPMTA VALUES(")
                sql.Append("'" & entkbn.ToString.PadLeft(2, "0"c) & "',")
                sql.Append(GetCurrentNKBNO() & ",")
                sql.Append(row.ENTCNT & ",")
                sql.Append(row.POINT.ToString() & ",")
                sql.Append("NOW(),")
                sql.Append("NOW())")
                entkbn += 1

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Next

            'コミット処理
            iDatabase.Commit()

            Return True

        Catch ex As Exception
            'ロールバック
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 重複した数値がないかチェックする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckENTCNT() As Boolean
        Try
            For Each row In _list
                If String.IsNullOrEmpty(row.ENTCNT) Or String.IsNullOrEmpty(row.POINT) Then Continue For
                Dim oldValue = row.ENTCNT
                Dim query = _list.Where(Function(x As ETPMTA_View) x.ENTCNT = oldValue)
                If query.Count >= 2 Then
                    Dim kbns = query.Select(Function(x As ETPMTA_View) x.ETPKBN)
                    Me.dgvETPMTA.CurrentCell = Me.dgvETPMTA("ENTCNT", CInt(kbns.Max) - 1)
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 現在の顧客区分を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentNKBNO() As Integer
        Return Me.cmbNKBNO.SelectedIndex + 1
    End Function

    ''' <summary>
    ''' コピー元区分を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCopyNKBNO() As Integer
        Return Me.cmbNKBNO_COPY.SelectedIndex + 1
    End Function

    ''' <summary>
    ''' 現在の最新更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentUPDDTM() As DateTime
        Try


            Dim sql As New System.Text.StringBuilder
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM ETPMTA")
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If resultDt.Rows.Count > 0 Then
                Dim drSelectRow() As DataRow = resultDt.Select
                Return DirectCast(drSelectRow(0).Item("UPDDTM"), DateTime)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

#End Region

End Class
