Imports Techno.DataBase

Public Class frmPOINTMST01

#Region "▼宣言部"

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ポイントマスタ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ポイントマスタ登録"

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
    Private Sub frmPOINTMST01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            'ポイントマスタ情報取得
            GetPOINT()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' グリッド_EditingControlShowing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvPOINTMST_EditingControlShowing(sender As System.Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvPOINTMST.EditingControlShowing
        Try
            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
                Dim dgv As DataGridView = CType(sender, DataGridView)

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                'イベントハンドラ削除
                RemoveHandler tb.KeyPress, AddressOf dgvPOINTMST_KeyPress

                '該当する列か調べる
                If dgv.CurrentCell.OwningColumn.Index.Equals(2) Then

                    'KeyPressイベントハンドラ追加
                    AddHandler tb.KeyPress, AddressOf dgvPOINTMST_KeyPress
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
    Private Sub dgvPOINTMST_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
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
    ''' グリッド_CellFormatting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvPOINTMST_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvPOINTMST.CellFormatting
        Try
            If IsNothing(e.Value) Then
                '【起動時エラーでるときあるのではじく】
                Return
            End If
            If e.ColumnIndex = 2 Then
                If Not String.IsNullOrEmpty(e.Value.ToString) Then
                    e.Value = CInt(e.Value).ToString("n0")
                End If
                'フォーマットの必要がないことを知らせる
                e.FormattingApplied = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F11クリアボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func11()
        Try


            'セルの編集終了
            Me.dgvPOINTMST.EndEdit()

            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            'ポイントマスタ情報取得
            GetPOINT()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try
            'セルの編集終了
            Me.dgvPOINTMST.EndEdit()

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            Cursor = Cursors.WaitCursor

            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM POINTMST WHERE BMNCD = '002' AND (POINTNO = '002' OR POINTNO ='003')")
            If Not resultDt.Rows.Count.Equals(0) Then
                '最新の更新日時を取得

                Dim drSelectRow() As DataRow = resultDt.Select
                Dim dtUPDDTM As DateTime = DirectCast(drSelectRow(0).Item("UPDDTM"), DateTime)

                If Not dtUPDDTM.Equals(_dtUPDDTM) Then
                    Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

            '顧客種別登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("顧客種別の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            'ポイントマスタ情報取得
            GetPOINT()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Cursor = Cursors.Default
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
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = True

            'ReadOnlyセル飛ばす
            Me.dgvPOINTMST.ReadOnlyThrough = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ポイントマスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetPOINT()
        Dim sql As New System.Text.StringBuilder
        Try
            Me.dgvPOINTMST.RowCount = 0

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM POINTMST")
            sql.Append(" WHERE")
            sql.Append(" BMNCD = '002'")
            sql.Append(" AND (POINTNO = '002' OR POINTNO = '003')")
            sql.Append(" ORDER BY POINTNO ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                MessageBox.Show("ポイントマスタ情報が見つかりません。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Me.dgvPOINTMST.RowCount += 1
                '№
                Me.dgvPOINTMST.SetValue("colNO", i, (i + 1).ToString.PadLeft(2, "0"c))
                'ポイント№
                Me.dgvPOINTMST.SetValue("colPOINTNO", i, resultDt.Rows(i).Item("POINTNO").ToString)
                'ポイント名称
                Me.dgvPOINTMST.SetValue("colPOINTNM", i, resultDt.Rows(i).Item("POINTNM").ToString)
                'ポイント数
                Me.dgvPOINTMST.SetValue("colPOINT", i, resultDt.Rows(i).Item("POINT").ToString)
                '作成日時
                Me.dgvPOINTMST.SetValue("colINSDTM", i, resultDt.Rows(i).Item("INSDTM").ToString)
                '更新日時日時
                Me.dgvPOINTMST.SetValue("colUPDDTM", i, resultDt.Rows(i).Item("UPDDTM").ToString)

                '最新の更新日時取得
                _dtUPDDTM = DirectCast(resultDt.Rows(i).Item("UPDDTM"), DateTime)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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

            If Not iDatabase.ExecuteUpdate("DELETE FROM POINTMST WHERE BMNCD = '002' AND ( POINTNO = '002' OR POINTNO = '003')") Then
                Return False
            End If

            For i As Integer = 0 To Me.dgvPOINTMST.RowCount - 1

                sql.Clear()
                sql.Append("INSERT INTO POINTMST VALUES(")
                '分類
                sql.Append("'002',")
                'ポイント№
                sql.Append("'" & Me.dgvPOINTMST.getValue("colPOINTNO", i).ToString & "',")
                'ポイント名称
                sql.Append("'" & Me.dgvPOINTMST.getValue("colPOINTNM", i).ToString & "',")
                'ポイント
                If String.IsNullOrEmpty(Me.dgvPOINTMST.getValue("colPOINT", i).ToString) Then
                    sql.Append("0,")
                Else
                    sql.Append(CType(Me.dgvPOINTMST.getValue("colPOINT", i).ToString, Integer) & ",")
                End If
                '消費税区分
                sql.Append("'2',")
                '作成日時
                sql.Append("NOW(),")
                '更新日時
                sql.Append("NOW())")

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
        End Try
    End Function
#End Region

End Class
