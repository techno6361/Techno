Imports TECHNO.DataBase

Public Class frmSTAFFMT01

#Region "▼宣言部"

    'グリッド表示件数
    Private _cstGridMaxRows As Integer = 20

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "スタッフ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "スタッフ情報登録"

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
    Private Sub frmSTFFMT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()

            'スタッフ情報取得
            GetSTAFFMT()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' グリッド_CellEnter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvSTAFFMT_CellEnter(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSTAFFMT.CellEnter
        Try

            Select Case e.ColumnIndex
                Case 1
                    '名称
                    Me.dgvSTAFFMT.ImeMode = Windows.Forms.ImeMode.Hiragana
            End Select

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
            Me.dgvSTAFFMT.EndEdit()

            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            'スタッフ情報取得
            GetSTAFFMT()

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
            Me.dgvSTAFFMT.EndEdit()

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            Cursor = Cursors.WaitCursor

            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM STAFFMTA WHERE STFCODE > '0000'")
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

            'スタッフ登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("スタッフの登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            'スタッフ情報取得
            GetSTAFFMT()

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
            Me.dgvSTAFFMT.RowCount = _cstGridMaxRows

            'ReadOnlyセル飛ばす
            Me.dgvSTAFFMT.ReadOnlyThrough = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' スタッフ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSTAFFMT()
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM STAFFMTA")
            sql.Append(" WHERE STFCODE > '0000'")
            sql.Append(" ORDER BY STFCODE ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                MessageBox.Show("スタッフ情報が見つかりません。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For i As Integer = 0 To _cstGridMaxRows - 1
                'コード
                Me.dgvSTAFFMT.SetValue("colSTFCODE", i, (i + 1).ToString.PadLeft(4, "0"c))
                If i > resultDt.Rows.Count - 1 Then
                    '氏名
                    Me.dgvSTAFFMT.SetValue("colSTFNAME", i, String.Empty)
                Else
                    '氏名
                    Me.dgvSTAFFMT.SetValue("colSTFNAME", i, resultDt.Rows(i).Item("STFNAME").ToString)
                    '作成日時
                    Me.dgvSTAFFMT.SetValue("colINSDTM", i, resultDt.Rows(i).Item("INSDTM").ToString)
                    '更新日時日時
                    Me.dgvSTAFFMT.SetValue("colUPDDTM", i, resultDt.Rows(i).Item("UPDDTM").ToString)

                    '最新の更新日時取得
                    _dtUPDDTM = DirectCast(resultDt.Rows(i).Item("UPDDTM"), DateTime)
                End If

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

            If Not iDatabase.ExecuteUpdate("DELETE FROM STAFFMTA WHERE STFCODE > '0000'") Then
                Return False
            End If

            For i As Integer = 0 To _cstGridMaxRows - 1

                sql.Clear()
                sql.Append("INSERT INTO STAFFMTA VALUES(")
                'コード
                sql.Append("'" & (i + 1).ToString.PadLeft(4, "0"c) & "',")
                '名称
                sql.Append("'" & Me.dgvSTAFFMT.getValue("colSTFNAME", i).ToString & "',")
                'セキュリティコード
                sql.Append("'1111111111',")
                '打席画面権限
                sql.Append("'11111111111111111111111111111111111111111111111111',")
                'ｽｸｰﾙ画面権限
                sql.Append("'11111111111111111111111111111111111111111111111111',")
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
