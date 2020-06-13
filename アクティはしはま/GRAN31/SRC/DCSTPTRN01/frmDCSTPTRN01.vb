Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmDCSTPTRN01

#Region "▼宣言部"

    Dim _currentPage As Integer ' 現在のページ
    Dim _maxPage As Integer ' 最大ページ

    ' 開始時のみ結果を格納する保存用バッファ
    Dim _dcstptrnDt As DataTable

    ' 1ページに対する最大行数
    Private Const RECORD_OF_PAGE As Integer = 16

    ' 現在のページのデータ
    Dim _list As BindingList(Of DCSTPTRN_View) = New BindingList(Of DCSTPTRN_View)

    ' ページング前の状態を保存しておくデータ
    Dim _oldList As BindingList(Of DCSTPTRN_View) = New BindingList(Of DCSTPTRN_View)

    ' 更新日時
    Dim _dtUPDDTM As DateTime

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "停止中カード"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "停止中カード"

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

            ' カード停止トランの取得
            GetDCSTPTRN(0)

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

            ' カード停止トランの取得
            GetDCSTPTRN(0)

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


            ' 更新の確定
            Me.dgvENTHIST.EndEdit()
            UpdateOldList()

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            Cursor = Cursors.WaitCursor

            ' 他端末からの更新チェック
            If ChkUPDDTM() Then
                Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'トランザクション開始
            iDatabase.BeginTransaction()

            ' DCSTPTRNの削除処理
            If Not DeleteDCSTPTRN() Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'コミット処理
            iDatabase.Commit()

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            ' 画面初期化
            Init()

            ' カード停止トランの取得
            GetDCSTPTRN(0)

        Catch ex As Exception
            ' ロールバック処理
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Cursor = Cursors.Default
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
            UpdateOldList()
            GetDCSTPTRN(_currentPage)
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
            UpdateOldList()
            GetDCSTPTRN(_currentPage)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' 行選択でチェックボックスON/OFF
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvCSMAST_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvENTHIST.CellClick
        Try
            If e.RowIndex >= 0 Then
                If CType(Me.dgvENTHIST.Rows(e.RowIndex).Cells(5).Value, Boolean) Then
                    Me.dgvENTHIST.Rows(e.RowIndex).Cells(5).Value = False
                Else
                    Me.dgvENTHIST.Rows(e.RowIndex).Cells(5).Value = True
                End If
            End If

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
        Me.tspFunc12.Enabled = True

        ' ページの初期化
        _currentPage = 0

        ' ページング機能の初期化
        btnPrev.Enabled = False
        btnNext.Enabled = False

        ' 件数の初期化
        lblCount.Text = "0件中0～0件表示"

        ' その他の初期化
        _dcstptrnDt = Nothing
        _oldList = New BindingList(Of DCSTPTRN_View)

    End Sub

    ''' <summary>
    ''' 停止中カードトランの取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDCSTPTRN(ByVal currentPage As Integer)
        Dim sql As New System.Text.StringBuilder
        Me.dgvENTHIST.DataSource = _list
        _list.Clear()
        Try

            ' DCSTPTRNの取得
            If _dcstptrnDt Is Nothing Then
                sql.Clear()
                sql.Append(" SELECT ")
                sql.Append(" *")
                sql.Append(" FROM DCSTPTRN")

                _dcstptrnDt = iDatabase.ExecuteRead(sql.ToString())

                If _dcstptrnDt.Rows.Count <= 0 Then Return

                '最新の更新日時を取得
                _dtUPDDTM = GetCurrentUPDDTM("DCSTPTRN")
            End If

            Dim query = From x In _dcstptrnDt.AsEnumerable
                        Order By x("INSDTM") Descending

            If query.Count <= 0 Then Return

            ' 件数の最大値
            Dim max_count = query.Count

            ' 指定ページ分取り出す
            Dim staPage = currentPage * RECORD_OF_PAGE
            Dim endPage = RECORD_OF_PAGE
            If (max_count - staPage) < endPage Then
                endPage = max_count - staPage
            End If
            Dim query_paging = query.Skip(staPage).Take(endPage).ToList()

            Dim i = staPage
            For Each row In query_paging
                Dim data = New DCSTPTRN_View
                If row Is Nothing Then Exit For

                data.NO = (i + 1).ToString.PadLeft(4, "0"c)
                data.DATE_ = DateTime.Parse(row("INSDTM").ToString).ToString("yyyy/MM/dd")
                data.TIME = DateTime.Parse(row("INSDTM").ToString).ToString("HH:mm")
                data.CDNO = row("NCARDID").ToString.PadLeft(8, "0"c)
                data.MANNO = row("NCSNO").ToString.PadLeft(8, "0"c)

                ' 以前のチェック状態を復元
                data.CHECK = GetOldCheck(data.NO)

                i += 1
                _list.Add(data)
            Next

            ' 復元用データを新規作成
            InsertOldList()

            ' 件数の表示
            Dim a = max_count.ToString & " 件中 "
            Dim b = (staPage + 1).ToString & "～" & (staPage + endPage).ToString & " 件表示"
            lblCount.Text = a & b

            ' ページングの制御
            _maxPage = 0
            If max_count > 0 Then
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
    ''' DCSTPTRNの削除処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteDCSTPTRN() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()

            sql.Append(" DELETE FROM DCSTPTRN ")
            sql.Append(" WHERE ")

            Dim count = 0

            For Each old In _oldList
                If old.CHECK Then
                    If count > 0 Then sql.Append(" OR ")
                    sql.Append(" NCARDID = " & old.CDNO.ToString.TrimStart("0"c))
                    sql.Append(" AND ")
                    sql.Append(" NCSNO = " & old.MANNO.TrimStart("0"c))
                    count += 1
                End If
            Next

            If count > 0 Then
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 復元用データを新規作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertOldList()
        For Each l In _list
            Dim flg = False
            For Each old In _oldList
                If l.NO = old.NO Then
                    flg = True
                    Exit For
                End If
            Next
            If Not flg Then
                _oldList.Add(l)
            End If
        Next
    End Sub

    ''' <summary>
    ''' 復元用データを更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateOldList()
        For Each l In _list
            For Each old In _oldList
                If old.NO = l.NO Then
                    old.CHECK = l.CHECK
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' ページング前のチェック状態を復元
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOldCheck(ByVal key As String) As Boolean
        For Each l In _oldList
            Console.WriteLine(l.CHECK)
            If l.NO = key Then
                Return l.CHECK
            End If
        Next
        Return False
    End Function

#End Region

#Region "▼汎用関数定義"

    ''' <summary>
    ''' 他端末からの更新チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkUPDDTM() As Boolean
        Try
            Dim dtUPDDTM = GetCurrentUPDDTM("DCSTPTRN")
            Dim flg1 = Not dtUPDDTM.Equals(Nothing) And Not dtUPDDTM.Equals(_dtUPDDTM)
            Return flg1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 現在の最新更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentUPDDTM(ByVal table As String) As DateTime
        Try

            Dim sql As New System.Text.StringBuilder
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM " & table)
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
