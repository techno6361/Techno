Imports Techno.DataBase

Public Class frmDTELOP01

#Region "▼宣言部"

    Private iDatabase As IDatabase.IMethod

    Private _UPDDTM As DateTime

    Private _ipc As IpcClient = New IpcClient

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
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
    Private Sub frmDTELOP01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            ' 開始処理
            Start()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try


            'Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
            '    frm.ShowDialog()
            '    If Not frm.Reply Then
            '        Exit Sub
            '    End If
            'End Using

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

            ' POINTMSTの登録処理
            If Not updateDTELOP() Then
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

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(0) Then
                ' UIUtilityの更新
                UIUtility.SYSTEM.TELOP = Me.txtTELOP.Text
                UIUtility.SYSTEM.COMMENT = Me.txtCOMMENT.Text
                UIUtility.SYSTEM.FCOMMENT = Me.txtFCOMMENT.Text

                '' IPC接続で更新            
                _ipc.SYSTEM.TELOP = Me.txtTELOP.Text
            End If

            ' 開始処理
            Start()

        Catch ex As Exception
            ' ロールバック処理
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' キャンセルボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' キーバインド
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmDTELOP01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnOK.PerformClick()
            Case Keys.Escape
                btnCancel.PerformClick()
            Case Keys.Enter
                SendKeys.Send("{TAB}")
        End Select
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' 開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Start()
        Try
            ' 初期化
            Init()

            ' DTELOPマスタを取得
            GetDTELOP()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 表示初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            btnOK.Visible = True
            btnOK.Enabled = True
            btnCancel.Enabled = True
            btnCancel.Visible = True
            txtTELOP.Text = String.Empty
            Me.txtCOMMENT.Text = String.Empty
            Me.txtFCOMMENT.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' DTELOPマスタを取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDTELOP()
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DTELOP")
            Dim dt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If dt.Rows.Count <= 0 Then
                Exit Sub
            End If
            txtTELOP.Text = dt.Rows(0).Item("TELOP").ToString().Trim
            Me.txtCOMMENT.Text = dt.Rows(0).Item("COMMENT").ToString.Trim
            Me.txtFCOMMENT.Text = dt.Rows(0).Item("FCOMMENT").ToString.Trim

            _UPDDTM = DirectCast(dt(0).Item("UPDDTM"), DateTime)
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' DTELOPの更新処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function updateDTELOP() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE DTELOP")
            sql.Append(" SET TELOP = '" & Me.txtTELOP.Text & "', ")
            sql.Append(" COMMENT = '" & Me.txtCOMMENT.Text & "',")
            sql.Append(" FCOMMENT = '" & Me.txtFCOMMENT.Text & "',")
            sql.Append(" UPDDTM = '" & DateTime.Now & "' ")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 他端末からの更新チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkUPDDTM() As Boolean
        Try
            Return Not GetCurrentUPDDTM().Equals(_UPDDTM)
        Catch ex As Exception
            Throw ex
        End Try
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
            sql.Append(" FROM DTELOP")
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If resultDt.Rows.Count > 0 Then
                Dim drSelectRow() As DataRow = resultDt.Select
                If Not String.IsNullOrEmpty(drSelectRow(0).Item("UPDDTM").ToString()) Then
                    Return DirectCast(drSelectRow(0).Item("UPDDTM"), DateTime)
                Else
                    Return Nothing
                End If
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
