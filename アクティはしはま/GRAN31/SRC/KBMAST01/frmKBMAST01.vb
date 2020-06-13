Imports TECHNO.DataBase
Imports System.Threading

Public Class frmKBMAST01

#Region "▼宣言部"

    'グリッド表示件数
    Private _cstGridMaxRows As Integer = 10

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客種別登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客種別登録"

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
    Private Sub frmKBMAST01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()

            '顧客種別情報取得
            GetKBMAST()

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
    Private Sub dgvKBMAST_EditingControlShowing(sender As System.Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvKBMAST.EditingControlShowing
        Try
            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
                Dim dgv As DataGridView = CType(sender, DataGridView)

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                'イベントハンドラ削除
                RemoveHandler tb.KeyPress, AddressOf dgvKBMAST_KeyPress

                '該当する列か調べる
                If dgv.CurrentCell.OwningColumn.Index.Equals(3) Or dgv.CurrentCell.OwningColumn.Index.Equals(4) Then
                    '期限(月)またはタグ番号

                    'KeyPressイベントハンドラ追加
                    AddHandler tb.KeyPress, AddressOf dgvKBMAST_KeyPress
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
    Private Sub dgvKBMAST_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
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
    Private Sub dgvKBMAST_CellEnter(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKBMAST.CellEnter
        Try

            Select Case e.ColumnIndex
                Case 1
                    '種別名称
                    Me.dgvKBMAST.ImeMode = Windows.Forms.ImeMode.Hiragana
                Case 2
                    '種別略称
                    Me.dgvKBMAST.ImeMode = Windows.Forms.ImeMode.Hiragana
                Case 3
                    '期限(月)
                    Me.dgvKBMAST.ImeMode = Windows.Forms.ImeMode.Off
                Case 4
                    'タグ番号
                    Me.dgvKBMAST.ImeMode = Windows.Forms.ImeMode.Off
            End Select

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
    Private Sub dgvKBMAST_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvKBMAST.CellFormatting
        Try
            '年会費
            If IsNothing(e.Value) Then
                '【起動時エラーでるときあるのではじく】
                Return
            End If
            If e.ColumnIndex = 4 Then
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
            Me.dgvKBMAST.EndEdit()

            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '顧客種別情報取得
            GetKBMAST()

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
            Me.dgvKBMAST.EndEdit()

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            Cursor = Cursors.WaitCursor

            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM KBMAST")
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

            '顧客種別情報取得
            GetKBMAST()

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

            'グリッド行数
            Me.dgvKBMAST.RowCount = _cstGridMaxRows

            'ReadOnlyセル飛ばす
            Me.dgvKBMAST.ReadOnlyThrough = True


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetKBMAST()
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" WHERE")
            sql.Append(" NKBNO <= 10")
            sql.Append(" ORDER BY KBMAST ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                MessageBox.Show("顧客種別情報が見つかりません。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For i As Integer = 0 To _cstGridMaxRows - 1
                '種別番号
                Me.dgvKBMAST.SetValue("NKBNO", i, (i + 1).ToString.PadLeft(2, "0"c))
                '種別名称
                Me.dgvKBMAST.SetValue("CKBNAME", i, resultDt.Rows(i).Item("CKBNAME").ToString)
                '種別略称
                Me.dgvKBMAST.SetValue("CKBRN", i, resultDt.Rows(i).Item("CKBRN").ToString)
                '期限(月)
                Me.dgvKBMAST.SetValue("CKBLIMIT", i, resultDt.Rows(i).Item("CKBLIMIT").ToString)
                '年会費
                Me.dgvKBMAST.SetValue("CKBFEE", i, CType(resultDt.Rows(i).Item("CKBFEE").ToString, Integer))
                'タグ番号
                Me.dgvKBMAST.SetValue("TAGNO", i, resultDt.Rows(i).Item("TAGNO").ToString)
                '作成日時
                Me.dgvKBMAST.SetValue("INSDTM", i, resultDt.Rows(i).Item("INSDTM").ToString)
                '更新日時日時
                Me.dgvKBMAST.SetValue("UPDDTM", i, resultDt.Rows(i).Item("UPDDTM").ToString)

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

            If Not iDatabase.ExecuteUpdate("DELETE FROM KBMAST") Then
                Return False
            End If

            For i As Integer = 0 To _cstGridMaxRows - 1

                sql.Clear()
                sql.Append("INSERT INTO KBMAST VALUES(")
                '種別番号
                sql.Append((i + 1) & ",")
                '種別名称
                sql.Append("'" & Me.dgvKBMAST.getValue("CKBNAME", i).ToString & "',")
                '種別略称
                sql.Append("'" & Me.dgvKBMAST.getValue("CKBRN", i).ToString & "',")
                '種別期限
                If String.IsNullOrEmpty(Me.dgvKBMAST.getValue("CKBLIMIT", i).ToString) Then
                    sql.Append("0,")
                Else
                    sql.Append(Me.dgvKBMAST.getValue("CKBLIMIT", i).ToString & ",")
                End If
                '年会費
                If String.IsNullOrEmpty(Me.dgvKBMAST.getValue("CKBFEE", i).ToString) Or String.IsNullOrEmpty(Me.dgvKBMAST.getValue("CKBNAME", i).ToString) Then
                    sql.Append("0,")
                Else
                    sql.Append(CType(Me.dgvKBMAST.getValue("CKBFEE", i).ToString, Integer) & ",")
                End If
                'タグ番号
                sql.Append(Me.dgvKBMAST.getValue("TAGNO", i).ToString & ",")
                '作成日時
                sql.Append("NOW(),")
                '更新日時
                sql.Append("NOW())")

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Next

            sql.Clear()
            sql.Append("INSERT INTO KBMAST VALUES(")
            '種別番号
            sql.Append("11,")
            '種別名称
            sql.Append("'ﾒﾝﾃﾅﾝｽｶｰﾄﾞ',")
            '種別略称
            sql.Append("'ﾒﾝﾃﾅﾝｽｶｰﾄﾞ',")
            '種別期限
            sql.Append("0,")
            '年会費
            sql.Append("0,")
            'タグ番号
            sql.Append("0,")
            '作成日時
            sql.Append("NOW(),")
            '更新日時
            sql.Append("NOW())")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            sql.Clear()
            sql.Append("INSERT INTO KBMAST VALUES(")
            '種別番号
            sql.Append("12,")
            '種別名称
            sql.Append("'打ち放題',")
            '種別略称
            sql.Append("'打ち放題',")
            '種別期限
            sql.Append("0,")
            '年会費
            sql.Append("0,")
            'タグ番号
            sql.Append("0,")
            '作成日時
            sql.Append("NOW(),")
            '更新日時
            sql.Append("NOW())")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

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
