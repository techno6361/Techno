Imports Techno.DataBase

Public Class frmPRINT05

#Region "▼宣言部"

    ''' <summary>
    ''' 一覧から取得した顧客番号セットテキストボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtNCSNO As TextBox

    Private _init As Boolean

    Private _abort As Boolean = False

    Enum eFugou
        PLUS
        MINUS
    End Enum

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "期間来場者分析"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "期間来場者分析"

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
    Private Sub frmPRINT04_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' 画面初期設定
            Init()

            ' 種別マスタの取得
            GetKBMAST()

            _init = True

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
        Try
            ApplySEATDT(Me.dtpStaSEATDT, Me.dtpEndSEATDT, Me.cmbTerm.SelectedIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 期間コンボボックス２
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbTerm2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTerm2.SelectedIndexChanged
        Try
            ApplySEATDT(Me.dtpStaSEATDT2, Me.dtpEndSEATDT2, Me.cmbTerm2.SelectedIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Try
            Cursor = Cursors.WaitCursor

            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.LOAD)

            If Me.rbSheet1.Checked Then
                ' 期間来場者集計
                If Not PrintSheet1() Then
                    Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.pnlStatus.Hide()
                    Exit Sub
                End If
            ElseIf Me.rbSheet2.Checked Then
                ' 期間来場者比較集計
                If Not PrintSheet2() Then
                    Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.pnlStatus.Hide()
                    Exit Sub
                End If
            ElseIf Me.rbSheet3.Checked Then
                ' 期間来場者個人一覧
                If Not PrintSheet3() Then
                    Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.pnlStatus.Hide()
                    Exit Sub
                End If
            ElseIf Me.rbSheet4.Checked Then
                ' 期間来場者個人比較一覧
                If Not PrintSheet4() Then
                    Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.pnlStatus.Hide()
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.pnlStatus.Hide()
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 期間来場者集計_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSheet1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSheet1.CheckedChanged
        Me.plDate2.Enabled = False
        Me.plNum.Enabled = False
    End Sub

    ''' <summary>
    ''' 期間来場者比較集計_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSheet2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSheet2.CheckedChanged
        Me.plDate2.Enabled = True
        Me.plNum.Enabled = False
    End Sub

    ''' <summary>
    ''' 期間来場者個人一覧_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSheet3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSheet3.CheckedChanged
        Me.plDate2.Enabled = False
        Me.plNum.Enabled = False
    End Sub

    ''' <summary>
    ''' 期間来場者個人比較一覧_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSheet4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSheet4.CheckedChanged
        Me.plDate2.Enabled = True
        Me.plNum.Enabled = True
    End Sub

    ''' <summary>
    ''' ﾌﾘｰｶｰﾄﾞを選んだらコントロール無効化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbKSBKB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKSBKB.SelectedIndexChanged
        Try
            If Not _init Then Return
            If Me.cmbKSBKB.SelectedIndex >= Me.cmbKSBKB.Items.Count - 1 Then
                Me.txtStaNCSNO.Enabled = False
                Me.txtEndNCSNO.Enabled = False
                Me.gbSEX.Enabled = False
                Me.txtStaAGE.Enabled = False
                Me.txtEndAGE.Enabled = False
                Me.txtCADDRESS.Enabled = False
                Me.lblMANNO.Enabled = False
                Me.lblSEX.Enabled = False
                Me.lblAGE.Enabled = False
                Me.lblADDRESS.Enabled = False
            Else
                Me.txtStaNCSNO.Enabled = True
                Me.txtEndNCSNO.Enabled = True
                Me.gbSEX.Enabled = True
                Me.txtStaAGE.Enabled = True
                Me.txtEndAGE.Enabled = True
                Me.txtCADDRESS.Enabled = True
                Me.lblMANNO.Enabled = True
                Me.lblSEX.Enabled = True
                Me.lblAGE.Enabled = True
                Me.lblADDRESS.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 数値以外入力禁止
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtStaNCSNO.KeyPress, txtEndNCSNO.KeyPress, txtStaAGE.KeyPress, txtEndAGE.KeyPress, _
        txtStaENTNum.KeyPress, txtEndENTNum.KeyPress, txtStaENTNum2.KeyPress, txtEndENTNum2.KeyPress, _
        txtStaZougenNum.KeyPress, txtEndZougenNum.KeyPress
        '0～9と、バックスペース以外の時は、イベントをキャンセルする
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso _
                e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' 顧客番号を0詰め
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtStaNCSNO_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStaNCSNO.Validated, txtEndNCSNO.Validated
        Dim obj = CType(sender, TextBox)
        If Not String.IsNullOrEmpty(obj.Text) Then
            obj.Text = obj.Text.PadLeft(obj.MaxLength, "0"c)
        End If
    End Sub

    ''' <summary>
    ''' 顧客番号テキストボックス_Enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNCSNO_Enter(sender As System.Object, e As System.EventArgs) Handles txtStaNCSNO.Enter, txtEndNCSNO.Enter
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            _txtNCSNO = txtBox

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ±トグルボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStaZougen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStaZougen.Click
        If Me.btnStaZougen.Text = "+" Then
            Me.btnStaZougen.Text = "-"
        Else
            Me.btnStaZougen.Text = "+"
        End If
    End Sub

    ''' <summary>
    ''' ±トグルボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEndZougen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEndZougen.Click
        If Me.btnEndZougen.Text = "+" Then
            Me.btnEndZougen.Text = "-"
        Else
            Me.btnEndZougen.Text = "+"
        End If
    End Sub

    ''' <summary>
    ''' ESCキーで処理の中断
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPRINT05_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            _abort = True
        End If
    End Sub

    ''' <summary>
    ''' F3一覧ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func3()
        Dim intNCSNO As Integer = 0
        Try
            Using frm As New frmCSSEARCH01(iDatabase)
                frm.ShowDialog()
                intNCSNO = frm.GetNCSNO
            End Using

            If intNCSNO.Equals(0) Then Exit Sub

            _txtNCSNO.Text = intNCSNO.ToString.PadLeft(8, "0"c)


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
            Me.tspFunc3.Enabled = True
            '印刷
            Me.tspFunc8.Enabled = True
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            ' SelectedIndex
            Me.cmbTerm.SelectedIndex = 0
            Me.cmbTerm2.SelectedIndex = 0
            Me.cmbKSBKB.SelectedIndex = 0

            ' Text
            Me.txtStaNCSNO.Text = String.Empty
            Me.txtEndNCSNO.Text = String.Empty
            Me.txtStaAGE.Text = String.Empty
            Me.txtEndAGE.Text = String.Empty
            Me.txtCADDRESS.Text = String.Empty
            Me.txtStaENTNum.Text = String.Empty
            Me.txtEndENTNum.Text = String.Empty
            Me.txtStaENTNum2.Text = String.Empty
            Me.txtEndENTNum2.Text = String.Empty
            Me.txtStaZougenNum.Text = String.Empty
            Me.txtEndZougenNum.Text = String.Empty

            ' Checked
            Me.rbSheet1.Checked = True
            Me.rbSEXAll.Checked = True

            ' Enabled
            Me.plDate2.Enabled = True
            Me.plDate2.Enabled = False
            Me.plNum.Enabled = False

            ' pnlstatus
            Me.pnlStatus.Init(Me)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 入場者クエリと顧客マスタ、顧客種別マスタを統合したクエリを取得する(フリーカードを含む)
    ''' </summary>
    ''' <param name="query"></param>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJoinENTTRA_View(ByRef query As System.Collections.Generic.IEnumerable(Of JoinENTTRA_View), _
                                  ByVal date1 As String, ByVal date2 As String) As Boolean

        Dim sql As New System.Text.StringBuilder
        Dim ksbkb = Me.cmbKSBKB.SelectedIndex
        Dim isFreecard = Me.cmbKSBKB.SelectedIndex >= Me.cmbKSBKB.Items.Count - 1

        Try

            ' ENTTRA,CSMAST,KBMASTを結合
            sql.Clear()
            sql.Append(" SELECT DISTINCT")
            sql.Append(" A.ENTDT,A.MANNO,A.EIGKB,A.KSBKB")
            'sql.Append(" A.ENTDT,A.ENTNO,A.MANNO,A.EIGKB,A.KSBKB")
            sql.Append(",B.*")
            sql.Append(",C.CKBNAME")
            sql.Append(" ,COALESCE(AGE, -1) AS INT_AGE")
            sql.Append(" ,COALESCE(NSEX, -1) AS INT_NSEX")
            sql.Append(" FROM ")
            sql.Append("(")
            'フロント受付
            sql.Append("SELECT ENTDT,ENTNO,MANNO,EIGKB,KSBKB FROM ENTTRA WHERE DATKB = '1'")
            sql.Append(String.Format(" AND ENTDT >= '{0}'", date1))
            sql.Append(String.Format(" AND ENTDT <= '{0}'", date2))

            sql.Append(" UNION ")
            '無人営業時
            sql.Append("SELECT ENTDT,0 AS ENTNO,MANNO,'1' AS EIGKB,KSBKB FROM ENTTRB WHERE ")
            sql.Append(String.Format(" ENTDT >= '{0}'", date1))
            sql.Append(String.Format(" AND ENTDT <= '{0}'", date2))

            sql.Append(") AS A")
            sql.Append(" LEFT JOIN (SELECT *, DATE_PART('YEAR', AGE(CURRENT_DATE, DBIRTH)) AS AGE FROM CSMAST) AS B ON TO_NUMBER(A.MANNO, '99999999') = B.ncsno")
            sql.Append(" LEFT JOIN KBMAST AS C ON B.NCSRANK = C.NKBNO")
            sql.Append(" WHERE ")
            sql.Append(String.Format(" A.ENTDT >= '{0}'", date1))
            sql.Append(String.Format(" AND A.ENTDT <= '{0}'", date2))
            sql.Append(" AND A.MANNO > '00000000'")


            If isFreecard Then
                ' 顧客種別がﾌﾘｰｶｰﾄﾞのときはすべてのフィルタリングが無効

            ElseIf Me.cmbKSBKB.SelectedIndex = 0 Then

                ' 顧客種別が空白のとき

                ' 顧客番号
                If Not String.IsNullOrEmpty(txtStaNCSNO.Text.Trim) Then
                    Dim v = txtStaNCSNO.Text.PadLeft(8, "0"c)
                    sql.Append(String.Format(" AND A.MANNO >= '{0}'", v))
                End If
                If Not String.IsNullOrEmpty(txtEndNCSNO.Text.Trim) Then
                    Dim v = txtEndNCSNO.Text.PadLeft(8, "0"c)
                    sql.Append(String.Format(" AND A.MANNO <= '{0}'", v))
                End If

                ' 性別
                If Me.rbSEXMale.Checked Then
                    sql.Append(String.Format(" AND B.NSEX = {0}", 1))
                End If
                If Me.rbSEXFemale.Checked Then
                    sql.Append(String.Format(" AND B.NSEX = {0}", 2))
                End If
                If Me.rbSEXOther.Checked Then
                    sql.Append(String.Format(" AND (B.NSEX = {0} OR A.MANNO >= '50000000')", 3))
                End If

                ' 年齢
                If Not String.IsNullOrEmpty(txtStaAGE.Text.Trim) Then
                    Dim v = CInt(txtStaAGE.Text)
                    sql.Append(String.Format(" AND AGE >= {0}", v))
                End If
                If Not String.IsNullOrEmpty(txtEndAGE.Text.Trim) Then
                    Dim v = CInt(txtEndAGE.Text)
                    sql.Append(String.Format(" AND AGE <= {0}", v))
                End If

                ' 住所
                If Not String.IsNullOrEmpty(txtCADDRESS.Text.Trim) Then
                    Dim v = txtCADDRESS.Text.Trim
                    sql.Append(String.Format(" AND (B.CADDRESS1 LIKE '%{0}%' OR B.CADDRESS2 LIKE '%{0}%')", v))
                End If

            ElseIf Me.cmbKSBKB.SelectedIndex > 0 Then

                ' 顧客種別が空白、フリーカード以外のとき

                sql.Append(" AND A.MANNO < '50000000'")

                ' 顧客番号
                If Not String.IsNullOrEmpty(txtStaNCSNO.Text.Trim) Then
                    Dim v = txtStaNCSNO.Text.PadLeft(8, "0"c)
                    sql.Append(String.Format(" AND A.MANNO >= '{0}'", v))
                End If
                If Not String.IsNullOrEmpty(txtEndNCSNO.Text.Trim) Then
                    Dim v = txtEndNCSNO.Text.PadLeft(8, "0"c)
                    sql.Append(String.Format(" AND A.MANNO <= '{0}'", v))
                End If

                ' 顧客種別
                If ksbkb > 0 Then
                    sql.Append(String.Format(" AND B.NCSRANK = {0}", ksbkb))
                End If

                ' 性別
                If Me.rbSEXMale.Checked Then
                    sql.Append(String.Format(" AND B.NSEX = {0}", 1))
                End If
                If Me.rbSEXFemale.Checked Then
                    sql.Append(String.Format(" AND B.NSEX = {0}", 2))
                End If
                If Me.rbSEXOther.Checked Then
                    sql.Append(String.Format(" AND B.NSEX = {0}", 3))
                End If

                ' 年齢
                If Not String.IsNullOrEmpty(txtStaAGE.Text.Trim) Then
                    Dim v = CInt(txtStaAGE.Text)
                    sql.Append(String.Format(" AND AGE >= {0}", v))
                End If
                If Not String.IsNullOrEmpty(txtEndAGE.Text.Trim) Then
                    Dim v = CInt(txtEndAGE.Text)
                    sql.Append(String.Format(" AND AGE <= {0}", v))
                End If

                ' 住所
                If Not String.IsNullOrEmpty(txtCADDRESS.Text.Trim) Then
                    Dim v = txtCADDRESS.Text.Trim
                    sql.Append(String.Format(" AND (B.CADDRESS1 LIKE '%{0}%' OR B.CADDRESS2 LIKE '%{0}%')", v))
                End If

            End If

            ' SQL実行
            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return False

            query = From x In dt.AsEnumerable
                    Select New JoinENTTRA_View With {
                        .ENTDT = x("ENTDT").ToString,
                        .MANNO = x("MANNO").ToString,
                        .EIGKB = x("EIGKB").ToString,
                        .KSBKB = x("KSBKB").ToString,
                        .KBNAME = x("CKBNAME").ToString,
                        .NCSRANK = x("NCSRANK").ToString,
                        .CCSNAME = x("CCSNAME").ToString,
                        .NZIP = x("NZIP").ToString,
                        .CADDRESS = x("CADDRESS1").ToString & x("CADDRESS2").ToString,
                        .CTELEPHONE = x("CTELEPHONE").ToString,
                        .AGE = CInt(x("INT_AGE")),
                        .SEX = CInt(x("INT_NSEX"))
                    }

            Return True

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' 入場者クエリと顧客マスタ、顧客種別マスタを統合して入場数をカウントしたクエリを取得する（フリーカードも含む）*SQL改善
    ''' </summary>
    ''' <param name="query"></param>
    ''' <param name="date1"></param>
    ''' <param name="date2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEntCount_View(ByRef query As System.Collections.Generic.IEnumerable(Of EntCount_View), _
                                  ByVal date1 As String, ByVal date2 As String) As Boolean

        Dim sql As New System.Text.StringBuilder
        Dim ksbkb = Me.cmbKSBKB.SelectedIndex

        Try
            ' ENTTRA,CSMAST,KBMASTを結合
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" NCSNO, COUNT(MANNO), TO_CHAR(NCSNO, '00000000') AS MANNO, CCSNAME, CKBNAME, AGE, NSEX")
            sql.Append(" FROM")

            sql.Append(" (SELECT A.NCSNO, A.CCSNAME, B.CKBNAME, COALESCE(DATE_PART('YEAR', AGE(CURRENT_DATE, DBIRTH)), -1) AS AGE, COALESCE(A.NSEX, -1) AS NSEX")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT OUTER JOIN KBMAST AS B ON A.NCSRANK = B.NKBNO")
            sql.Append(" WHERE A.NCSNO IS NOT NULL")


            ' 顧客番号
            If Not String.IsNullOrEmpty(txtStaNCSNO.Text.Trim) Then
                Dim v = txtStaNCSNO.Text.PadLeft(8, "0"c)
                sql.Append(String.Format(" AND A.NCSNO >= {0}", v))
            End If
            If Not String.IsNullOrEmpty(txtEndNCSNO.Text.Trim) Then
                Dim v = txtEndNCSNO.Text.PadLeft(8, "0"c)
                sql.Append(String.Format(" AND A.NCSNO <= {0}", v))
            End If

            ' 顧客種別
            If ksbkb > 0 Then
                sql.Append(String.Format(" AND A.NCSRANK = {0}", ksbkb))
            End If

            ' 性別
            If Me.rbSEXMale.Checked Then
                sql.Append(String.Format(" AND A.NSEX = {0}", 1))
            End If
            If Me.rbSEXFemale.Checked Then
                sql.Append(String.Format(" AND A.NSEX = {0}", 2))
            End If
            If Me.rbSEXOther.Checked Then
                sql.Append(String.Format(" AND A.NSEX = {0}", 3))
            End If

            ' 住所
            If Not String.IsNullOrEmpty(txtCADDRESS.Text.Trim) Then
                Dim v = txtCADDRESS.Text.Trim
                sql.Append(String.Format(" AND (A.CADDRESS1 LIKE '%{0}%' OR A.CADDRESS2 LIKE '%{0}%')", v))
            End If


            sql.Append(" ) AS XA")

            sql.Append(" LEFT OUTER JOIN (")

            sql.Append(" SELECT ENTDT,0 AS ENTNO,MANNO,'1' AS EIGKB,KSBKB FROM ENTTRB")
            sql.Append(" WHERE ")
            sql.Append(" MANNO IS NOT NULL AND MANNO != ''")
            sql.Append(String.Format(" AND ENTDT >= '{0}'", date1))
            sql.Append(String.Format(" AND ENTDT <= '{0}'", date2))

            sql.Append(" UNION ")

            sql.Append(" SELECT ENTDT,ENTNO,MANNO,EIGKB,KSBKB FROM ENTTRA")
            sql.Append(" WHERE DATKB = '1'")
            sql.Append(" AND MANNO IS NOT NULL AND MANNO != ''")
            sql.Append(String.Format(" AND ENTDT >= '{0}'", date1))
            sql.Append(String.Format(" AND ENTDT <= '{0}'", date2))

            sql.Append(" ) AS XB")
            sql.Append(" ON XA.NCSNO = TO_NUMBER(XB.MANNO, '999999999')")

            sql.Append(" WHERE XA.NCSNO IS NOT NULL")

            ' 年齢
            If Not String.IsNullOrEmpty(txtStaAGE.Text.Trim) Then
                Dim v = CInt(txtStaAGE.Text)
                sql.Append(String.Format(" AND XA.AGE >= {0}", v))
            End If
            If Not String.IsNullOrEmpty(txtEndAGE.Text.Trim) Then
                Dim v = CInt(txtEndAGE.Text)
                sql.Append(String.Format(" AND XA.AGE <= {0}", v))
            End If

            sql.Append(" GROUP BY NCSNO, MANNO, CCSNAME, CKBNAME, AGE, NSEX")
            sql.Append(" ORDER BY NCSNO")

            ' SQL実行
            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count > 0 Then
                query = From c As DataRow In dt.AsEnumerable
                    Select New EntCount_View With {
                        .MANNO = c("MANNO").ToString,
                        .CCSNAME = c("CCSNAME").ToString,
                        .KBNAME = c("CKBNAME").ToString,
                        .NSEX = CInt(c("NSEX")),
                        .AGE = CInt(c("AGE")),
                        .COUNT1 = CInt(c("COUNT"))
                    }
            End If

  
            If query Is Nothing Then Return False

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 期間来場者集計の印刷(SQL改善)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintSheet1() As Boolean

        Dim sql As New System.Text.StringBuilder
        Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
        Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
        Dim ksbkb = Me.cmbKSBKB.SelectedIndex
        Dim isFreecard = Me.cmbKSBKB.SelectedIndex >= Me.cmbKSBKB.Items.Count - 1

        ' 集計クラス
        Dim block1 = New Sheet1Model_Block1
        Dim block2 = New Sheet1Model_Block2
        Dim block3 = New List(Of Sheet1Model_Block3)
        Dim block4 = New Sheet1Model_Block4

        ' *** 【出力日】 ***
        Dim outputDate = DateTime.Now.ToString("yyyy/MM/dd")

        ' *** 【来場期間】 ***
        Dim strStaENTDate = Me.dtpStaSEATDT.Value.ToString("yyyy年MM月dd日")
        Dim strEndENTDate = Me.dtpEndSEATDT.Value.ToString("yyyy年MM月dd日")

        ' *** 【総来場者数】 ***
        Dim totalEntNum = 0

        ' *** 【エクセル処理関係】 ***
        Dim strReportName As String = "期間来場者集計"
        Dim strOpenReportName = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"

        Dim excel = New UIExcel

        Try
            ' *** 顧客種別マスタの取得

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY NKBNO")
            ' SQL 実行
            Dim kbmast_dt = iDatabase.ExecuteRead(sql.ToString())
            If kbmast_dt.Rows.Count <= 0 Then Return False

            ' 種別名の取得とカウントリセット
            Dim j = 1
            For Each row As DataRow In kbmast_dt.Rows
                If j > 10 Then Exit For
                Dim x = New Sheet1Model_Block3(row("CKBNAME").ToString)
                block3.Add(x)
                j += 1
            Next

            ' *** 結合済の入場者クエリを取得
            Dim enttra_query As System.Collections.Generic.IEnumerable(Of JoinENTTRA_View) = Nothing
            If Not GetJoinENTTRA_View(enttra_query, date1, date2) Then Return False


            Dim not_free_query = enttra_query.Where(Function(x) CInt(x.MANNO) < 50000000)

            ' 男女別
            block1.Male = not_free_query.Where(Function(x) x.SEX = 1).Count
            block1.Female = not_free_query.Where(Function(x) x.SEX = 2).Count
            block1.Other = not_free_query.Where(Function(x) x.SEX = 3).Count

            ' 年齢別
            block2.Age0 = not_free_query.Where(Function(x) x.AGE >= 0 And x.AGE <= 9).Count
            block2.Age1 = not_free_query.Where(Function(x) x.AGE >= 10 And x.AGE <= 19).Count
            block2.Age2 = not_free_query.Where(Function(x) x.AGE >= 20 And x.AGE <= 29).Count
            block2.Age3 = not_free_query.Where(Function(x) x.AGE >= 30 And x.AGE <= 39).Count
            block2.Age4 = not_free_query.Where(Function(x) x.AGE >= 40 And x.AGE <= 49).Count
            block2.Age5 = not_free_query.Where(Function(x) x.AGE >= 50 And x.AGE <= 59).Count
            block2.Age6 = not_free_query.Where(Function(x) x.AGE >= 60 And x.AGE <= 69).Count
            block2.Age7 = not_free_query.Where(Function(x) x.AGE >= 70 And x.AGE <= 79).Count
            block2.Age8 = not_free_query.Where(Function(x) x.AGE >= 80).Count
            block2.Other = not_free_query.Where(Function(x) x.AGE < 0).Count

            ' 種別区分別
            For kb = 1 To 10
                Dim ncsrank = kb.ToString
                block3(kb - 1).Count = not_free_query.Where(Function(x) x.NCSRANK = ncsrank).Count
            Next

            ' 利用別
            ' 1KSBKB
            block4.KB1 = not_free_query.Where(Function(x) x.EIGKB = "1" And CInt(x.KSBKB) >= 1 And CInt(x.KSBKB) <= 10).Count
            ' 打ち放題
            block4.KB2 = not_free_query.Where(Function(x) x.EIGKB = "2").Count

            ' 総来場者数
            totalEntNum = not_free_query.Count


            ' *** 帳票の出力

            ' ファイルを開く
            excel.Open(strOpenReportName, 1, False)

            ' ステータスを表示
            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE)

            ' 出力日
            excel.Cells(1, 13) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 来場期間
            excel.Cells(4, 2) = String.Format("[来場期間]:{0} ～ {1}", strStaENTDate, strEndENTDate)

            ' 総来場者数
            excel.Cells(6, 11) = String.Format("総来場者数:{0}件", totalEntNum.ToString("N0"))

            ' 男女別
            excel.Cells(8, 3) = "男 " & (block1.Male / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(8, 4) = block1.Male.ToString("N0")
            excel.Cells(9, 3) = "女 " & (block1.Female / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(9, 4) = block1.Female.ToString("N0")
            excel.Cells(10, 3) = "不明 " & (block1.Other / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(10, 4) = block1.Other.ToString("N0")

            ' 年齢別
            excel.Cells(24, 3) = "10歳未満 " & (block2.Age0 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(24, 4) = block2.Age0.ToString("N0")
            excel.Cells(25, 3) = "10-19歳 " & (block2.Age1 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(25, 4) = block2.Age1.ToString("N0")
            excel.Cells(26, 3) = "20-29歳 " & (block2.Age2 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(26, 4) = block2.Age2.ToString("N0")
            excel.Cells(27, 3) = "30-39歳 " & (block2.Age3 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(27, 4) = block2.Age3.ToString("N0")
            excel.Cells(28, 3) = "40-49歳 " & (block2.Age4 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(28, 4) = block2.Age4.ToString("N0")
            excel.Cells(29, 3) = "50-59歳 " & (block2.Age5 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(29, 4) = block2.Age5.ToString("N0")
            excel.Cells(30, 3) = "60-69歳 " & (block2.Age6 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(30, 4) = block2.Age6.ToString("N0")
            excel.Cells(31, 3) = "70-79歳 " & (block2.Age7 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(31, 4) = block2.Age7.ToString("N0")
            excel.Cells(32, 3) = "80歳以上 " & (block2.Age8 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(32, 4) = block2.Age8.ToString("N0")
            excel.Cells(34, 3) = "年齢不明 " & (block2.Other / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(34, 4) = block2.Other.ToString("N0")

            ' 種別区分別
            Dim i = 8
            For Each x In block3
                excel.Cells(i, 8) = x.Name & (x.Count / totalEntNum * 100).ToString("0.0") & " " & "%"
                excel.Cells(i, 9) = x.Count.ToString("N0")
                i += 1
            Next

            ' 利用別
            excel.Cells(24, 8) = "1球貸し " & (block4.KB1 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(24, 9) = block4.KB1.ToString("N0")
            excel.Cells(25, 8) = "打ち放題 " & (block4.KB2 / totalEntNum * 100).ToString("0.0") & "%"
            excel.Cells(25, 9) = block4.KB2.ToString("N0")

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 期間来場者比較集計の印刷(SQL改善)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintSheet2() As Boolean

        Dim sql As New System.Text.StringBuilder
        Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
        Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
        Dim date3 = Me.dtpStaSEATDT2.Value.ToString("yyyyMMdd")
        Dim date4 = Me.dtpEndSEATDT2.Value.ToString("yyyyMMdd")
        Dim ksbkb = Me.cmbKSBKB.SelectedIndex
        Dim isFreecard = Me.cmbKSBKB.SelectedIndex >= Me.cmbKSBKB.Items.Count - 1

        ' 集計クラス
        Dim block1 = New List(Of Sheet1Model_Block3) ' 参照元
        Dim block2 = New List(Of Sheet1Model_Block3) ' 参照先

        ' *** 【出力日】 ***
        Dim outputDate = DateTime.Now.ToString("yyyy/MM/dd")

        ' *** 【来場期間】 ***
        Dim staSEATDT = Me.dtpStaSEATDT.Value.ToString("yyyy年MM月dd日")
        Dim endSEATDT = Me.dtpEndSEATDT.Value.ToString("yyyy年MM月dd日")
        Dim staSEATDT2 = Me.dtpStaSEATDT2.Value.ToString("yyyy年MM月dd日")
        Dim endSEATDT2 = Me.dtpEndSEATDT2.Value.ToString("yyyy年MM月dd日")

        ' *** 【総来場者数】 ***
        Dim totalEntNum = 0  ' 参照元
        Dim totalEntNum2 = 0 ' 参照先

        ' *** 【エクセル処理関係】 ***
        Dim strReportName As String = "期間来場者比較集計"
        Dim strOpenReportName = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"
        Dim excel = New UIExcel

        Try
            ' 【参照元】入場者クエリ
            Dim enttra_query As System.Collections.Generic.IEnumerable(Of JoinENTTRA_View) = Nothing
            If Not GetJoinENTTRA_View(enttra_query, date1, date2) Then Return False

            ' 【参照先】入場者クエリ
            Dim enttra_query2 As System.Collections.Generic.IEnumerable(Of JoinENTTRA_View) = Nothing
            If Not GetJoinENTTRA_View(enttra_query2, date3, date4) Then Return False

            ' 顧客種別マスタ
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY NKBNO")
            Dim kbmast_dt = iDatabase.ExecuteRead(sql.ToString())
            If kbmast_dt.Rows.Count <= 0 Then Return False

            ' 比較元の生成
            For i = 1 To 10
                block1.Add(New Sheet1Model_Block3())
            Next
            block1.Add(New Sheet1Model_Block3())

            ' 比較先の生成
            For i = 1 To 10
                block2.Add(New Sheet1Model_Block3())
            Next
            block2.Add(New Sheet1Model_Block3())

            ' ﾌﾘｰｶｰﾄﾞ以外の集計
            If Not isFreecard Then

                ' 【参照元】種別区分別
                Dim not_free_query = enttra_query.Where(Function(x) CInt(x.MANNO) < 50000000)
                For kb = 1 To 10
                    Dim ncsrank = kb.ToString
                    block1(kb - 1).Count = not_free_query.Where(Function(x) x.NCSRANK = ncsrank).Count
                Next

                ' 【参照先】種別区分別
                Dim not_free_query2 = enttra_query2.Where(Function(x) CInt(x.MANNO) < 50000000)
                For kb = 1 To 10
                    Dim ncsrank = kb.ToString
                    block2(kb - 1).Count = not_free_query2.Where(Function(x) x.NCSRANK = ncsrank).Count
                Next

                ' 総来場者数
                totalEntNum = not_free_query.Count
                totalEntNum2 = not_free_query2.Count

            End If

            ' *** 帳票の出力

            ' ファイルを開く
            excel.Open(strOpenReportName, 1, False)

            ' ステータスを表示
            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE)

            ' 出力日
            excel.Cells(1, 15) = String.Format("出力日:{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 種別名
            For i = 0 To 9
                excel.Cells(7, 6 + i) = kbmast_dt(i)("CKBNAME")
            Next

            ' 比較元
            excel.Cells(8, 3) = staSEATDT
            excel.Cells(10, 3) = endSEATDT
            For i = 0 To 9
                excel.Cells(8, 6 + i) = block1(i).Count.ToString("N0")
            Next

            ' 比較先
            excel.Cells(11, 3) = staSEATDT2
            excel.Cells(13, 3) = endSEATDT2
            For i = 0 To 9
                excel.Cells(11, 6 + i) = block2(i).Count.ToString("N0")
            Next

            ' パーセンテージ
            Dim list = New List(Of HikakuValue)
            Dim totalValue = New HikakuValue
            totalValue.Value1 = totalEntNum
            totalValue.value2 = totalEntNum2
            list.Add(totalValue)
            For i = 0 To 9
                Dim kbValue = New HikakuValue
                kbValue.Value1 = block1(i).Count
                kbValue.value2 = block2(i).Count
                list.Add(kbValue)
            Next
            Dim idx = 0
            For Each v In list
                Dim per = 0.0
                Dim prefix As String
                If v.Value1 = 0 Or v.value2 = 0 Then
                    per = 0
                    prefix = ""
                Else
                    per = Math.Round((v.value2 - v.Value1) / v.Value1 * 100, 1)
                    If v.Value1 > v.value2 Then
                        prefix = "↑"
                    ElseIf v.Value1 < v.value2 Then
                        prefix = "↓"
                    Else
                        prefix = ""
                    End If
                End If
                excel.Cells(14, 5 + idx) = prefix & Math.Abs(per) & "％"
                idx += 1
            Next

            ' グラフの日付
            excel.Cells(41, 3) = String.Format("{0}～{1}", staSEATDT, endSEATDT)
            excel.Cells(42, 3) = String.Format("{0}～{1}", staSEATDT2, endSEATDT2)

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 期間来場者個人一覧の印刷(SQL改善)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintSheet3() As Boolean

        Dim sql As New System.Text.StringBuilder
        Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
        Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
        Dim ksbkb = Me.cmbKSBKB.SelectedIndex
        'Dim isFreecard = Me.cmbKSBKB.SelectedIndex >= Me.cmbKSBKB.Items.Count - 1

        ' *** 【出力日】 ***
        Dim outputDate = DateTime.Now.ToString("yyyy/MM/dd")

        ' *** 【来場期間】 ***
        Dim strStaENTDate = Me.dtpStaSEATDT.Value.ToString("yyyy年MM月dd日")
        Dim strEndENTDate = Me.dtpEndSEATDT.Value.ToString("yyyy年MM月dd日")

        ' *** 【エクセル処理関係】 ***
        Dim strReportName As String = "期間来場者個人一覧"
        Dim strOpenReportName = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"
        Dim excel = New UIExcel

        Try
            ' 入場者クエリ
            Dim enttra_query As System.Collections.Generic.IEnumerable(Of JoinENTTRA_View) = Nothing
            If Not GetJoinENTTRA_View(enttra_query, date1, date2) Then Return False

            Dim not_free_query As System.Collections.Generic.IEnumerable(Of Customer) = Nothing
            Dim free_query As System.Collections.Generic.IEnumerable(Of Customer) = Nothing
            Dim merge_query As System.Collections.Generic.IEnumerable(Of Customer) = Nothing

            '' 種別区分 = 空白 or 種別区分 = ﾌﾘｰｶｰﾄﾞ
            'If Me.cmbKSBKB.SelectedIndex <= 0 Or isFreecard Then

            '    free_query = From e In enttra_query
            '                 Where CInt(e.MANNO) >= 10000000
            '                 Select New Customer With {
            '                    .MANNO = e.MANNO,
            '                    .ENTDT = String.Format("{0}/{1}/{2}", e.ENTDT.Substring(0, 4), e.ENTDT.Substring(4, 2), e.ENTDT.Substring(6, 2)),
            '                    .ENTNO = e.ENTNO,
            '                    .KBNAME = "フリーカード",
            '                    .SEX = -1,
            '                    .AGE = -1
            '                 }

            '    ' 結合用
            '    merge_query = free_query

            'End If


            not_free_query = From e In enttra_query
                             Where CInt(e.MANNO) < 50000000
                             Select New Customer With {
                                       .MANNO = e.MANNO,
                                       .ENTDT = String.Format("{0}/{1}/{2}", e.ENTDT.Substring(0, 4), e.ENTDT.Substring(4, 2), e.ENTDT.Substring(6, 2)),
                                       .KBNAME = e.KBNAME,
                                       .SEX = e.SEX,
                                       .AGE = e.AGE,
                                       .NAME = e.CCSNAME,
                                       .ZIP = e.NZIP,
                                       .ADDRESS = e.CADDRESS,
                                       .TELEPHONE = e.CTELEPHONE
                                    }

            'If Not merge_query Is Nothing And merge_query.Any Then
            '    ' free_query が存在していれば結合
            '    merge_query = merge_query.Concat(not_free_query)
            'Else
            ' free_query が存在していなければ not_free_query のみ
            merge_query = not_free_query
            'End If


            ' 日付順にソート
            If Not merge_query Is Nothing Then
                merge_query = From m In merge_query
                              Order By m.ENTDT
            End If

            If merge_query Is Nothing Or Not merge_query.Any Then Return False

            ' *** 帳票の出力 ***

            ' ファイルを開く
            excel.Open(strOpenReportName, 1, False)

            ' ステータスを表示
            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE_ESC, merge_query.Count)

            ' 出力日
            excel.Cells(1, 13) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 来場期間
            excel.Cells(4, 2) = String.Format("[来場期間]:{0} ～ {1}", strStaENTDate, strEndENTDate)

            ' 検索結果
            excel.Cells(6, 12) = String.Format("検索結果：{0}件", merge_query.Count.ToString("N0"))

            Dim i = 1                     ' 件数
            Dim row_index = 8             ' 開始行数
            Dim max_per_page = 31         ' 1ページの最大行数
            Dim offset = max_per_page + 1 ' 罫線を入れる行数

            ResetAbort()

            For Each row In merge_query

                ' 処理の中断
                If IsAbort() Then Exit For

                ' 連番
                excel.Cells(row_index, 2) = i

                ' 来場日
                excel.Cells(row_index, 3) = row.ENTDT

                ' 顧客番号
                excel.Cells(row_index, 4) = row.MANNO

                ' 顧客種別
                excel.Cells(row_index, 5) = row.KBNAME

                ' 氏名
                excel.Cells(row_index, 6) = row.NAME

                ' 性別
                Dim sex = ""
                If row.SEX = 1 Then
                    sex = "男"
                ElseIf row.SEX = 2 Then
                    sex = "女"
                ElseIf row.SEX = 3 Then
                    sex = "不明"
                End If
                excel.Cells(row_index, 7) = sex

                ' 年齢
                If row.AGE > 0 Then
                    excel.Cells(row_index, 8) = row.AGE
                End If

                ' 住所
                Dim address = row.ADDRESS
                If Not String.IsNullOrEmpty(row.ZIP) Then
                    address = String.Format("〒{0} {1}", row.ZIP.Insert(3, "-"), row.ADDRESS)
                End If
                excel.Cells(row_index, 9) = address

                ' 電話番号
                excel.Cells(row_index, 13) = row.TELEPHONE

                Me.pnlStatus.Count = i

                row_index += 1
                i += 1

                ' 罫線
                If i = offset Then
                    excel.DrawBoldLine(row_index, 2, 13)
                    offset += max_per_page
                End If
            Next

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 期間来場者個人比較一覧の印刷(SQL改善)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintSheet4() As Boolean

        Dim sql As New System.Text.StringBuilder
        Dim date1 = Me.dtpStaSEATDT.Value.ToString("yyyyMMdd")
        Dim date2 = Me.dtpEndSEATDT.Value.ToString("yyyyMMdd")
        Dim date3 = Me.dtpStaSEATDT2.Value.ToString("yyyyMMdd")
        Dim date4 = Me.dtpEndSEATDT2.Value.ToString("yyyyMMdd")
        Dim ksbkb = Me.cmbKSBKB.SelectedIndex

        ' *** 【出力日】 ***
        Dim outputDate = DateTime.Now.ToString("yyyy/MM/dd")

        ' *** 【来場期間】 ***
        Dim strStaENTDate = Me.dtpStaSEATDT.Value.ToString("yyyy年MM月dd日")
        Dim strEndENTDate = Me.dtpEndSEATDT.Value.ToString("yyyy年MM月dd日")
        Dim strStaENTDate2 = Me.dtpStaSEATDT2.Value.ToString("yyyy年MM月dd日")
        Dim strEndENTDate2 = Me.dtpEndSEATDT2.Value.ToString("yyyy年MM月dd日")

        ' *** 【エクセル処理関係】 ***
        Dim strReportName As String = "期間来場者個人比較一覧"
        Dim strOpenReportName = UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName
        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"
        Dim excel = New UIExcel

        Try
            ' ① 比較元
            Dim ent_count_query1 As System.Collections.Generic.IEnumerable(Of EntCount_View) = Nothing
            If Not GetEntCount_View(ent_count_query1, date1, date2) Then Return False

            ' ② 比較先
            Dim ent_count_query2 As System.Collections.Generic.IEnumerable(Of EntCount_View) = Nothing
            If Not GetEntCount_View(ent_count_query2, date3, date4) Then Return False

            Dim ent_count_query = CreateDiffCountView(ent_count_query1, ent_count_query2)

            If Not ent_count_query.Any Then Return False

            Dim not_free_query As System.Collections.Generic.IEnumerable(Of EntCount_View) = Nothing
            Dim free_query As System.Collections.Generic.IEnumerable(Of EntCount_View) = Nothing


            not_free_query = ent_count_query.Where(Function(x) CInt(x.MANNO) < 50000000)


            ' *** 帳票の出力 ***

            ' ファイルを開く
            excel.Open(strOpenReportName, 1, False)

            ' ステータスを表示
            Dim totalCount = 0
            If Not not_free_query Is Nothing Then totalCount += not_free_query.Count
            If Not free_query Is Nothing Then totalCount += free_query.Count
            Me.pnlStatus.Show(BaseControl.StatusPanel.eShowOption.WRITE_ESC, totalCount)

            ' 出力日
            excel.Cells(1, 12) = String.Format("出力日：{0}", DateTime.Now.ToString("yyyy/MM/dd"))

            ' 比較元
            excel.Cells(7, 2) = String.Format("①{0} ～ {1}", strStaENTDate, strEndENTDate)

            ' 比較先
            excel.Cells(8, 2) = String.Format("②{0} ～ {1}", strStaENTDate2, strEndENTDate2)

            Dim i = 1                     ' 件数
            Dim row_index = 12            ' 開始行数
            Dim max_per_page = 28         ' 1ページの最大行数
            Dim offset = max_per_page + 1 ' 罫線を入れる行数

            ' 処理の中断が呼ばれたか
            Dim abort_flg = False

            For Each row In not_free_query

                ' 処理の中断
                If IsAbort() Then
                    abort_flg = True
                    Exit For
                End If

                ' 連番
                excel.Cells(row_index, 3) = i

                ' 顧客番号
                excel.Cells(row_index, 4) = row.MANNO

                ' 顧客種別
                excel.Cells(row_index, 5) = row.KBNAME

                ' 氏名
                excel.Cells(row_index, 6) = row.CCSNAME

                ' 性別
                Dim sex = ""
                If row.NSEX = 1 Then
                    sex = "男"
                ElseIf row.NSEX = 2 Then
                    sex = "女"
                ElseIf row.NSEX = 3 Then
                    sex = "不明"
                End If
                excel.Cells(row_index, 7) = sex

                ' 年齢
                If row.AGE > 0 Then
                    excel.Cells(row_index, 8) = row.AGE
                End If

                ' 来場回数①
                excel.Cells(row_index, 9) = row.COUNT1.ToString("N0")

                ' 来場回数②
                excel.Cells(row_index, 10) = row.COUNT2.ToString("N0")

                ' 増減
                excel.Cells(row_index, 11) = row.ZOUGEN.ToString("N0")

                Me.pnlStatus.Count = i

                row_index += 1
                i += 1

                ' 罫線
                If i = offset Then
                    excel.DrawBoldLine(row_index, 3, 11)
                    offset += max_per_page
                End If

            Next

            ' 検索結果
            excel.Cells(8, 12) = String.Format("検索結果：{0}件", (i - 1).ToString("N0"))

            'ファイル保存
            excel.SaveAs(strSaveReportName, True)

            Return True

        Catch ex As Exception
            excel.Dispose()
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 二つの入場カウントクエリから結果を生成。フィルタリングも行う。
    ''' </summary>
    ''' <param name="ent_count_query1"></param>
    ''' <param name="ent_count_query2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDiffCountView(ByVal ent_count_query1 As System.Collections.Generic.IEnumerable(Of EntCount_View), _
                                         ByVal ent_count_query2 As System.Collections.Generic.IEnumerable(Of EntCount_View)) _
                                         As System.Collections.Generic.IEnumerable(Of EntCount_View)

        Dim ent_count_query As System.Collections.Generic.IEnumerable(Of EntCount_View) = Nothing

        If Not ent_count_query1 Is Nothing And ent_count_query2 Is Nothing Then
            ' ① のみ
            ent_count_query = From e1 In ent_count_query1
                              Select New EntCount_View With {
                                .MANNO = e1.MANNO,
                                .CCSNAME = e1.CCSNAME,
                                .KBNAME = e1.KBNAME,
                                .NSEX = e1.NSEX,
                                .AGE = e1.AGE,
                                .COUNT1 = e1.COUNT1,
                                .COUNT2 = 0,
                                .ZOUGEN = 0 - e1.COUNT1
                              }
        End If

        If ent_count_query1 Is Nothing And Not ent_count_query2 Is Nothing Then
            ' ② のみ
            ent_count_query = From e1 In ent_count_query2
                              Select New EntCount_View With {
                                .MANNO = e1.MANNO,
                                .CCSNAME = e1.CCSNAME,
                                .KBNAME = e1.KBNAME,
                                .NSEX = e1.NSEX,
                                .AGE = e1.AGE,
                                .COUNT1 = 0,
                                .COUNT2 = e1.COUNT1,
                                .ZOUGEN = e1.COUNT1 - 0
                              }
        End If

        If Not ent_count_query1 Is Nothing And Not ent_count_query2 Is Nothing Then

            ' ①と②の結合
            ent_count_query = From e1 In ent_count_query1
                              Join e2 In ent_count_query2 On e1.MANNO Equals e2.MANNO
                              Select New EntCount_View With {
                                .MANNO = e1.MANNO,
                                .CCSNAME = e1.CCSNAME,
                                .KBNAME = e1.KBNAME,
                                .NSEX = e1.NSEX,
                                .AGE = e1.AGE,
                                .COUNT1 = e1.COUNT1,
                                .COUNT2 = e2.COUNT1,
                                .ZOUGEN = e2.COUNT1 - e1.COUNT1
                              }

        End If

        If Not ent_count_query Is Nothing Then

            ' フィルタリング

            ' 来場回数
            If Not String.IsNullOrEmpty(Me.txtStaENTNum.Text.Trim) Then
                Dim v = CInt(txtStaENTNum.Text.Trim)
                ent_count_query = ent_count_query.Where(Function(x) x.COUNT1 >= v Or x.COUNT2 >= v)
            End If
            If Not String.IsNullOrEmpty(Me.txtEndENTNum.Text.Trim) Then
                Dim v = CInt(txtEndENTNum.Text.Trim)
                ent_count_query = ent_count_query.Where(Function(x) x.COUNT1 <= v And x.COUNT2 <= v)
            End If

            ' 比較来場回数
            If Not String.IsNullOrEmpty(Me.txtStaENTNum2.Text.Trim) Then
                Dim v = CInt(txtStaENTNum2.Text.Trim)
                ent_count_query = ent_count_query.Where(Function(x) x.COUNT2 >= v)
            End If
            If Not String.IsNullOrEmpty(Me.txtEndENTNum2.Text.Trim) Then
                Dim v = CInt(txtEndENTNum2.Text.Trim)
                ent_count_query = ent_count_query.Where(Function(x) x.COUNT2 <= v)
            End If

            ' 増減回数
            If Not String.IsNullOrEmpty(Me.txtStaZougenNum.Text.Trim) Then
                Dim v = CInt(Me.txtStaZougenNum.Text.Trim)
                If Me.btnStaZougen.Text = "-" Then
                    v = -v
                End If
                ent_count_query = ent_count_query.Where(Function(x) x.ZOUGEN >= v)
            End If
            If Not String.IsNullOrEmpty(Me.txtEndZougenNum.Text.Trim) Then
                Dim v = CInt(Me.txtEndZougenNum.Text.Trim)
                If Me.btnEndZougen.Text = "-" Then
                    v = -v
                End If
                ent_count_query = ent_count_query.Where(Function(x) x.ZOUGEN <= v)
            End If

            'If Not ent_count_query.Any Then Return False

        End If

        Return ent_count_query

    End Function

    ''' <summary>
    ''' 顧客種別マスタの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            ' 種別マスタ
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY NKBNO")
            Dim kbmast_dt = iDatabase.ExecuteRead(sql.ToString())

            If kbmast_dt.Rows.Count <= 0 Then Return False

            For i = 0 To 9
                Dim row = kbmast_dt.Rows(i)
                Dim name = row("CKBNAME").ToString
                If Not String.IsNullOrEmpty(name) Then
                    Me.cmbKSBKB.Items.Add(name)
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

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
    ''' 処理の中断の初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetAbort()
        _abort = False
    End Sub

    ''' <summary>
    ''' 年齢を取得する
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAge(ByVal str As String) As Integer
        If String.IsNullOrEmpty(str) Then Return 0
        Dim last = DateTime.Now
        Dim start = DateTime.Parse(str)
        Try
            If ((start.Month * 100 + start.Day) <= (last.Month * 100 + last.Day)) Then
                Return CInt(DateDiff(DateInterval.Year, start, last))
            Else
                Return CInt(DateDiff(DateInterval.Year, start, last) - 1)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 選択したTEAMに応じてSEATDTをセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ApplySEATDT(ByRef StaSEATDT As DateTimePicker, ByRef EndSEATDT As DateTimePicker, ByVal index As Integer)
        Dim intYear As Integer = Now.Date.Year
        Dim intMonth As Integer = Now.Date.Month
        Dim intDay As Integer = Now.Date.Day
        Try
            StaSEATDT.Enabled = False
            EndSEATDT.Enabled = False

            Select Case index
                Case 0      '任意入力
                    StaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                    EndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                    StaSEATDT.Enabled = True
                    EndSEATDT.Enabled = True
                    StaSEATDT.Focus()
                    StaSEATDT.Select()
                Case 1      '今月
                    StaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/01"
                    EndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                Case 2      '前月
                    intMonth -= 1
                    If intMonth.Equals(0) Then
                        intMonth = 12
                        intYear -= 1
                    End If
                    StaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/01"
                    EndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & DateTime.DaysInMonth(intYear, intMonth).ToString.PadLeft(2, "0"c)
                Case 3      '今年
                    StaSEATDT.Text = intYear.ToString & "/01/01"
                    EndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                Case 4      '前年
                    intYear -= 1
                    StaSEATDT.Text = intYear.ToString & "/01/01"
                    EndSEATDT.Text = intYear.ToString & "/12/31"
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


End Class

