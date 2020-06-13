Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmHINMT01

#Region "▼宣言部"

    ' 商品マスタの登録最大数
    Const MAX_HINMTA_ROW_NUM As Integer = 16

    ' 更新日時
    Dim _dtUPDDTM_HINMTB As DateTime
    Dim _dtUPDDTM_HINMTA As DateTime
    Dim _dtUPDDTM_POINTMST As DateTime

    ' バインドされるリスト
    Dim _listA As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)
    Dim _listB As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)
    Dim _listC As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)
    Dim _listD As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)
    Dim _listE As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)
    Dim _listF As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)

    ' ReadOnlyタブのフォーカス操作フラグ
    Dim _dataGridViewFocused As Boolean

    ' 現在編集中の商品タグ
    Dim _currentHINMTB As String

    ' 初期化フラグ
    Dim _inited As Boolean = False

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品マスタ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品マスタ登録"

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
    ''' Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmHINMT01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            ' 画面初期化
            Init()

            _inited = True

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
            ' セルの編集終了
            EndEditGridViews()

            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '画面初期化
            Init()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try


            ' セルの編集終了
            EndEditGridViews()

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

            ' HINMTBの登録処理
            If Not UpdateHINMTB() Then
                Using frm As New frmMSGBOX01("顧客種別の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            ' HINMTAの登録処理            
            If InsertHINMTAs() Then
                Using frm As New frmMSGBOX01("顧客種別の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            ' POINTMSTの登録処理
            If Not UpdatePOINTMST() Then
                Using frm As New frmMSGBOX01("顧客種別の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'コミット処理
            iDatabase.Commit()

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            ' 画面初期化(位置の復元機能付き)
            Init(True)

        Catch ex As Exception
            ' ロールバック処理
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Cursor = Cursors.Default
        End Try

    End Sub

#End Region

#Region "▼関数定義"

#End Region

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init(Optional ByVal RecoveryPosition As Boolean = False)
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

            ' 初期フォーカスの設定
            Dim obj = txtBUNCDA
            If RecoveryPosition Then
                If _currentHINMTB = "dgvHINMTA" Then
                    obj = txtBUNCDA
                ElseIf _currentHINMTB = "dgvHINMTB" Then
                    obj = txtBUNCDB
                ElseIf _currentHINMTB = "dgvHINMTC" Then
                    obj = txtBUNCDC
                ElseIf _currentHINMTB = "dgvHINMTD" Then
                    obj = txtBUNCDD
                ElseIf _currentHINMTB = "dgvHINMTE" Then
                    obj = txtBUNCDE
                ElseIf _currentHINMTB = "dgvHINMTF" Then
                    obj = txtBUNCDF
                End If
            End If
            obj.Focus()
            obj.SelectAll()

            ' 商品項目タグの取得
            GetHINMTB()

            ' 商品マスタの取得
            GetHINMTAs()

            ' ポイント付与率の取得
            GetPOINTMST()

            ' フォーカス不具合の解消
            Me.dgvHINMTA.CurrentCell = Nothing
            Me.dgvHINMTB.CurrentCell = Nothing
            Me.dgvHINMTC.CurrentCell = Nothing
            Me.dgvHINMTD.CurrentCell = Nothing
            Me.dgvHINMTE.CurrentCell = Nothing
            Me.dgvHINMTF.CurrentCell = Nothing

            ' タブストップの設定
            SetTabStopAllControles(True)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' 商品項目タグ名の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHINMTB() As Boolean
        Try


            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTB ")
            sql.Append(" ORDER BY BUNCDB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count <= 0 Then Return False

            Dim query = resultDt.AsEnumerable.Where(Function(x As DataRow) x("BMNCD").Equals("002") And x("BUNCDA").Equals("001"))

            If query.Count <= 0 Then Return False

            txtBUNCDA.Text = query.Where(Function(x As DataRow) x("BUNCDB").Equals("001")).Select(Function(x As DataRow) x("BUNNMB")).First().ToString
            txtBUNCDB.Text = query.Where(Function(x As DataRow) x("BUNCDB").Equals("002")).Select(Function(x As DataRow) x("BUNNMB")).First().ToString
            txtBUNCDC.Text = query.Where(Function(x As DataRow) x("BUNCDB").Equals("003")).Select(Function(x As DataRow) x("BUNNMB")).First().ToString
            txtBUNCDD.Text = query.Where(Function(x As DataRow) x("BUNCDB").Equals("004")).Select(Function(x As DataRow) x("BUNNMB")).First().ToString
            txtBUNCDE.Text = query.Where(Function(x As DataRow) x("BUNCDB").Equals("005")).Select(Function(x As DataRow) x("BUNNMB")).First().ToString
            txtBUNCDF.Text = query.Where(Function(x As DataRow) x("BUNCDB").Equals("006")).Select(Function(x As DataRow) x("BUNNMB")).First().ToString

            If query.Where(Function(x As DataRow) x("BUNCDB").Equals("001")).Select(Function(x As DataRow) x("KOTEIKBN")).First().ToString.Equals("0") Then
                Me.chkKOTEIKBN1.Checked = False
            Else
                Me.chkKOTEIKBN1.Checked = True
            End If
            If query.Where(Function(x As DataRow) x("BUNCDB").Equals("002")).Select(Function(x As DataRow) x("KOTEIKBN")).First().ToString.Equals("0") Then
                Me.chkKOTEIKBN2.Checked = False
            Else
                Me.chkKOTEIKBN2.Checked = True
            End If
            If query.Where(Function(x As DataRow) x("BUNCDB").Equals("003")).Select(Function(x As DataRow) x("KOTEIKBN")).First().ToString.Equals("0") Then
                Me.chkKOTEIKBN3.Checked = False
            Else
                Me.chkKOTEIKBN3.Checked = True
            End If


            '最新の更新日時を取得
            _dtUPDDTM_HINMTB = GetCurrentUPDDTM()

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 商品マスタの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHINMTAs() As Boolean
        Try


            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTA ")
            sql.Append(" ORDER BY BUNCDB, HINCD ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim result = resultDt.Rows.Count <= 0

            Dim query = resultDt.AsEnumerable.Where(Function(x As DataRow) x("BMNCD").Equals("002") And x("BUNCDA").Equals("001"))

            GetHINMTA(query, "001", Me.dgvHINMTA, _listA)
            GetHINMTA(query, "002", Me.dgvHINMTB, _listB)
            GetHINMTA(query, "003", Me.dgvHINMTC, _listC)
            GetHINMTA(query, "004", Me.dgvHINMTD, _listD)
            GetHINMTA(query, "005", Me.dgvHINMTE, _listE)
            GetHINMTA(query, "006", Me.dgvHINMTF, _listF)

            '最新の更新日時を取得
            _dtUPDDTM_HINMTA = GetCurrentUPDDTM()

            Return result

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 商品マスタをグリッドビューに反映させる
    ''' </summary>
    ''' <param name="querys"></param>
    ''' <param name="buncdb"></param>
    ''' <param name="dgv"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHINMTA( _
                               ByRef querys As System.Data.EnumerableRowCollection(Of DataRow), _
                               ByVal buncdb As String, _
                               ByRef dgv As BaseControl.CustomGridView, _
                               ByRef list As BindingList(Of HINMTA_View)
                               ) As Boolean
        Try
            list.Clear()
            dgv.DataSource = list

            Dim query = querys.Where(Function(x As DataRow) x("BUNCDB").Equals(buncdb))

            Dim result = query.Count <= 0

            For i = 0 To MAX_HINMTA_ROW_NUM - 1
                Dim data = New HINMTA_View
                Dim row As DataRow = query(i)

                data.HINCD = (i + 1).ToString.PadLeft(3, "0"c)

                data.HINNMA = String.Empty
                data.URIATK = String.Empty
                data.ZEITK = String.Empty

                If Not row Is Nothing Then
                    If Not String.IsNullOrEmpty(row("HINNMA").ToString) Then
                        data.HINNMA = row("HINNMA").ToString
                    End If
                    If Not String.IsNullOrEmpty(row("URIATK").ToString) Then
                        data.URIATK = row("URIATK").ToString
                    End If
                    If Not String.IsNullOrEmpty(row("ZEITK").ToString) Then
                        data.ZEITK = row("ZEITK").ToString
                    End If

                    If row("HINKB").Equals("1") Then
                        Me.cmbHakkoTag.Text = buncdb
                        Me.cmbHakkoNo.Text = row("HINCD").ToString.PadLeft(3, "0"c)
                    End If

                End If

                list.Add(data)
            Next

            Return result

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ポイント付与率の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPOINTMST() As Boolean
        Try


            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM POINTMST ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count <= 0 Then Return False

            Dim query = resultDt.AsEnumerable.Where(Function(x As DataRow) x("BMNCD").Equals("002") And x("POINTNO").Equals("007"))

            txtPOINT.Text = query.Select(Function(x As DataRow) x("POINT")).First().ToString

            '最新の更新日時を取得
            _dtUPDDTM_POINTMST = GetCurrentUPDDTM()

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' HINMTAグループの登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertHINMTAs() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If Not iDatabase.ExecuteUpdate("DELETE FROM HINMTA WHERE BMNCD = '002' AND BUNCDA = '001'") Then
                Return False
            End If

            Dim flg1 = Not InsertHINMTA(_listA, "001")
            Dim flg2 = Not InsertHINMTA(_listB, "002")
            Dim flg3 = Not InsertHINMTA(_listC, "003")
            Dim flg4 = Not InsertHINMTA(_listD, "004")
            Dim flg5 = Not InsertHINMTA(_listE, "005")
            Dim flg6 = Not InsertHINMTA(_listF, "006")

            Return flg1 Or flg2 Or flg3 Or flg4 Or flg5 Or flg6

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' HINMTAの登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertHINMTA(ByRef list As BindingList(Of HINMTA_View), ByVal buncdb As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 実際に挿入されるENTKBN
            Dim hincd As Integer = 1

            For Each row As HINMTA_View In list
                '' 商品名が入力されていなければ削除
                'If row.HINNMA Is Nothing Then
                '    Continue For
                'End If
                'If String.IsNullOrEmpty(row.HINNMA.ToString.Trim) Then
                '    Continue For
                'End If

                ' 空白なら自動で0を入力
                If String.IsNullOrEmpty(row.URIATK.ToString.Trim) Then
                    row.URIATK = "0"
                End If
                If String.IsNullOrEmpty(row.ZEITK.ToString.Trim) Then
                    row.ZEITK = "0"
                End If

                ' 文字列を数値に変換
                Dim v1 As Integer
                Dim v2 As Integer
                If Not Int32.TryParse(row.URIATK.ToString.Replace(",", ""), v1) Or _
                   Not Int32.TryParse(row.ZEITK.ToString.Replace(",", ""), v2) Then
                    Continue For
                End If

                sql.Clear()
                sql.Append("INSERT INTO HINMTA(BMNCD, BUNCDA, BUNCDB, BUNCDC, HINCD, HINNMA, URIATK, URIBTK, HINZEIKB, ZEITK, HINKB, UPDDTM, INSDTM) VALUES(")
                sql.Append("'002',") ' BMNCD
                sql.Append("'001',") ' BUNCDA
                sql.Append("'" & buncdb & "',") ' BUNCDB
                sql.Append("'001',") ' BUNCDC
                sql.Append("'" & hincd.ToString.PadLeft(3, "0"c) & "',") ' HINCD
                sql.Append("'" & row.HINNMA & "',") ' HINNMA
                sql.Append(v1 & ",") ' URIATK
                sql.Append(v1 & ",") ' URIBTK
                'sql.Append(v1 + v2 & ",") ' URIBTK
                sql.Append("'2',") ' HINZEIKB
                sql.Append("0,") ' ZEITK
                'sql.Append(v2 & ",") ' ZEITK
                If Me.cmbHakkoTag.Text.Equals(buncdb) And hincd.ToString.PadLeft(3, "0"c).Equals(Me.cmbHakkoNo.Text) Then
                    sql.Append("1,")       ' HINKB
                Else
                    sql.Append(0 & ",")       ' HINKB
                End If

                sql.Append("NOW(),") ' UPDDTM
                sql.Append("NOW())") ' INSDTM
                hincd += 1
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' HINMTBの更新処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateHINMTB() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            Dim i = 1
            Dim list = New List(Of TextBox)
            list.Add(Me.txtBUNCDA)
            list.Add(Me.txtBUNCDB)
            list.Add(Me.txtBUNCDC)
            list.Add(Me.txtBUNCDD)
            list.Add(Me.txtBUNCDE)
            list.Add(Me.txtBUNCDF)
            For Each txt In list
                sql.Clear()
                sql.Append(" UPDATE HINMTB")
                sql.Append(" SET BUNNMB = '" & txt.Text & "' ")
                Select Case CType(txt.Tag, Integer)
                    Case 1
                        If Me.chkKOTEIKBN1.Checked Then
                            sql.Append(",KOTEIKBN = '1'")
                        Else
                            sql.Append(",KOTEIKBN = '0'")
                        End If
                    Case 2
                        If Me.chkKOTEIKBN2.Checked Then
                            sql.Append(",KOTEIKBN = '1'")
                        Else
                            sql.Append(",KOTEIKBN = '0'")
                        End If
                    Case 3
                        If Me.chkKOTEIKBN3.Checked Then
                            sql.Append(",KOTEIKBN = '1'")
                        Else
                            sql.Append(",KOTEIKBN = '0'")
                        End If
                End Select
                sql.Append(" WHERE BMNCD = '002' AND BUNCDA = '001'")
                sql.Append(" AND BUNCDB = '" & i.ToString.PadLeft(3, "0"c) & "'")
                i += 1
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' POINTMSTの更新処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePOINTMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 空白なら0に補正
            If String.IsNullOrEmpty(Me.txtPOINT.Text) Then
                Me.txtPOINT.Text = "0"
            End If

            sql.Clear()
            sql.Append(" UPDATE POINTMST")
            sql.Append(" SET POINT = " & CInt(Me.txtPOINT.Text) & " ")
            sql.Append(" WHERE BMNCD = '002' AND POINTNO = '007'")
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
    ''' 現在の最新更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentUPDDTM() As DateTime
        Try

            Dim sql As New System.Text.StringBuilder
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTA")
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
    ''' 他端末からの更新チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkUPDDTM() As Boolean
        Try
            Dim dtUPDDTM = GetCurrentUPDDTM()
            Dim flg1 = Not dtUPDDTM.Equals(Nothing) And Not dtUPDDTM.Equals(_dtUPDDTM_HINMTB)
            Dim flg2 = Not dtUPDDTM.Equals(Nothing) And Not dtUPDDTM.Equals(_dtUPDDTM_HINMTA)
            Dim flg3 = Not dtUPDDTM.Equals(Nothing) And Not dtUPDDTM.Equals(_dtUPDDTM_POINTMST)
            Return flg1 Or flg2 Or flg3
        Catch ex As Exception
            Throw ex
        End Try        
    End Function

    ''' <summary>
    ''' エラー表示
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Private Sub ShowErrorMessage(ByVal msg As String)
        MessageBox.Show(msg, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ''' <summary>
    ''' グリッドビューの編集完了
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EndEditGridViews()
        Me.dgvHINMTA.EndEdit()
        Me.dgvHINMTB.EndEdit()
        Me.dgvHINMTC.EndEdit()
        Me.dgvHINMTD.EndEdit()
        Me.dgvHINMTE.EndEdit()
        Me.dgvHINMTF.EndEdit()
    End Sub

#Region "▼ 入力制御とフォーマット"

    Private Sub dgvHINMTA_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) _
        Handles dgvHINMTA.EditingControlShowing, dgvHINMTB.EditingControlShowing, dgvHINMTC.EditingControlShowing _
               , dgvHINMTD.EditingControlShowing, dgvHINMTE.EditingControlShowing, dgvHINMTF.EditingControlShowing
        Try
            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
                Dim dgv As DataGridView = CType(sender, DataGridView)

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                'イベントハンドラ削除
                RemoveHandler tb.KeyPress, AddressOf dgvs_KeyPress

                '該当する列か調べる
                If dgv.CurrentCell.OwningColumn.Index.Equals(2) Or dgv.CurrentCell.OwningColumn.Index.Equals(3) Then
                    'KeyPressイベントハンドラ追加
                    AddHandler tb.KeyPress, AddressOf dgvs_KeyPress
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
    Private Sub dgvs_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
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
    ''' グリッドビューのフォーカス状態でReadOnlyセルのフォーカス制御を行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvHINMTA_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTA.Enter, dgvHINMTB.Enter, dgvHINMTC.Enter, dgvHINMTD.Enter, dgvHINMTE.Enter, dgvHINMTF.Enter
        _dataGridViewFocused = True
        _currentHINMTB = CType(sender, DataGridView).Name
    End Sub
    Private Sub dgvHINMTA_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTA.Leave, dgvHINMTB.Leave, dgvHINMTC.Leave, dgvHINMTD.Leave, dgvHINMTE.Leave, dgvHINMTF.Leave
        _dataGridViewFocused = False
        _currentHINMTB = String.Empty
    End Sub

    ''' <summary>
    ''' グリッド_CellEnter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvs_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles dgvHINMTA.CellEnter, dgvHINMTB.CellEnter, dgvHINMTC.CellEnter, dgvHINMTD.CellEnter, dgvHINMTE.CellEnter, dgvHINMTF.CellEnter
        Try
            ' グリッドビューのフォーカス状態で以降の処理を行うか制御する
            If Not _dataGridViewFocused Then Return

            Dim dgv = CType(sender, DataGridView)

            ' ReadOnlyセルのフォーカスを外す
            If dgv.Columns(e.ColumnIndex).ReadOnly Then
                SendKeys.Send("{TAB}")
            End If

            ' IME制御
            Select Case e.ColumnIndex
                Case 1
                    dgv.ImeMode = Windows.Forms.ImeMode.On
                Case 2
                    dgv.ImeMode = Windows.Forms.ImeMode.Off
                Case 3
                    dgv.ImeMode = Windows.Forms.ImeMode.Off
            End Select

            ' テキストを選択状態にする
            dgv.BeginEdit(True)

            If Not _inited Then Return

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_入力終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvHINMTA_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles dgvHINMTA.CellEndEdit, dgvHINMTB.CellEndEdit, dgvHINMTC.CellEndEdit, dgvHINMTD.CellEndEdit, dgvHINMTE.CellEndEdit, dgvHINMTF.CellEndEdit
        Try
            If Not _inited Then Return

            Dim dgv = CType(sender, DataGridView)

            ' 単価入力時、消費税が未入力なら自動計算
            If e.ColumnIndex = 2 Or e.ColumnIndex = 3 Then
                Dim tanCol = dgv(2, e.RowIndex) ' 単価
                Dim zeiCol = dgv(3, e.RowIndex) ' 消費税
                If Not String.IsNullOrEmpty(CStr(tanCol.Value)) Then
                    If String.IsNullOrEmpty(CStr(zeiCol.Value)) Then
                        If UIUtility.SYSTEM.TAX >= 1 Then
                            Dim tax = CDbl(tanCol.Value) * (UIUtility.SYSTEM.TAX * 0.01)
                            zeiCol.Value = Math.Round(tax).ToString
                        Else
                            zeiCol.Value = "0"
                        End If
                    End If
                Else
                    tanCol.Value = "0"
                    zeiCol.Value = "0"
                End If
            End If

            'If e.ColumnIndex = 2 Or e.ColumnIndex = 3 Then
            '    ' 空白行なら0を入力
            '    If String.IsNullOrEmpty(dgv.CurrentCell.Value.ToString) Then
            '        dgv.CurrentCell.Value = "0"
            '    End If
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' グリッド_フォーマット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvs_CellFormatting(ByVal sender As Object, _
            ByVal e As DataGridViewCellFormattingEventArgs) _
            Handles dgvHINMTA.CellFormatting, dgvHINMTB.CellFormatting, dgvHINMTC.CellFormatting, dgvHINMTD.CellFormatting, dgvHINMTE.CellFormatting, dgvHINMTF.CellFormatting
        Try
            If e.ColumnIndex = 2 Or e.ColumnIndex = 3 Then
                ' 空白は無視
                If String.IsNullOrEmpty(CStr(e.Value)) Then
                    Return
                End If
                ' カンマ表示
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

#Region "▼ フォーカスや選択状態の制御"

    ''' <summary>
    ''' すべてのコントロールのTabStopプロパティを設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTabStopAllControles(ByVal flg As Boolean)
        txtBUNCDA.TabStop = flg
        txtBUNCDB.TabStop = flg
        txtBUNCDC.TabStop = flg
        txtBUNCDD.TabStop = flg
        txtBUNCDE.TabStop = flg
        txtBUNCDF.TabStop = flg
        txtPOINT.TabStop = flg
        dgvHINMTA.TabStop = flg
        dgvHINMTB.TabStop = flg
        dgvHINMTC.TabStop = flg
        dgvHINMTD.TabStop = flg
        dgvHINMTE.TabStop = flg
        dgvHINMTF.TabStop = flg
    End Sub

    ' グリッドビューにフォーカスが移ると他のコントロールにフォーカスを移動できないようにする
    Private Sub dgvHINMTA_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTA.GotFocus
        SetTabStopAllControles(False)
        dgvHINMTA.TabStop = True
    End Sub
    Private Sub dgvHINMTB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTB.GotFocus
        SetTabStopAllControles(False)
        dgvHINMTB.TabStop = True
    End Sub
    Private Sub dgvHINMTC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTC.GotFocus
        SetTabStopAllControles(False)
        dgvHINMTC.TabStop = True
    End Sub
    Private Sub dgvHINMTD_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTD.GotFocus
        SetTabStopAllControles(False)
        dgvHINMTD.TabStop = True
    End Sub
    Private Sub dgvHINMTE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTE.GotFocus
        SetTabStopAllControles(False)
        dgvHINMTE.TabStop = True
    End Sub
    Private Sub dgvHINMTF_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTF.GotFocus
        SetTabStopAllControles(False)
        dgvHINMTF.TabStop = True
    End Sub
    Private Sub frmHINMT01_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        SetTabStopAllControles(True)
    End Sub

    ' テキストボックスにフォーカスが移るとフォーカス復活
    Private Sub txtBUNCDA_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBUNCDA.Enter, txtBUNCDB.Enter, txtBUNCDC.Enter, txtBUNCDD.Enter, txtBUNCDE.Enter, txtBUNCDF.Enter
        ' テキストを選択状態にする
        Dim obj = CType(sender, TextBox)
        obj.SelectAll()

        ' 他のコントロールをすべてフォーカス可能にする
        SetTabStopAllControles(True)
    End Sub

    ''' <summary>
    ''' ReadOnlyセルのフォーカスを外す/無効(※未使用)
    ''' </summary>
    ''' <param name="sw"></param>
    ''' <remarks></remarks>
    Private Sub SetReadOnlyThrough(ByVal sw As Boolean)
        Me.dgvHINMTA.ReadOnlyThrough = sw
        Me.dgvHINMTB.ReadOnlyThrough = sw
        Me.dgvHINMTC.ReadOnlyThrough = sw
        Me.dgvHINMTD.ReadOnlyThrough = sw
        Me.dgvHINMTE.ReadOnlyThrough = sw
        Me.dgvHINMTF.ReadOnlyThrough = sw
    End Sub

#End Region
        
End Class

''' <summary>
''' グリッドビューのコントロールと座標を保存する(※未使用)
''' </summary>
''' <remarks></remarks>
Class GridViewPosition
    Private _dgv As DataGridView
    Private _row As Integer
    Private _col As Integer
    Public Property Control As DataGridView
        Get
            Return _dgv
        End Get
        Set(ByVal value As DataGridView)
            _dgv = value
        End Set
    End Property
    Public Property RowIndex As Integer
        Get
            Return _row
        End Get
        Set(ByVal value As Integer)
            _row = value
        End Set
    End Property
    Public Property CollumnIndex As Integer
        Get
            Return _col
        End Get
        Set(ByVal value As Integer)
            _col = value
        End Set
    End Property
    Public Sub SavePosition(ByVal row As Integer, ByVal col As Integer)
        _row = row
        _col = col
    End Sub
    Public Sub RecoveryPosition()
        _dgv.Focus()
        _dgv.CurrentCell = _dgv(_row, _col)
    End Sub

End Class
