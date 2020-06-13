Imports TECHNO.DataBase
Imports Microsoft.Office.Interop

Public Class frmPRINT04

#Region "▼宣言部"

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "時間帯別入場者情報"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "時間帯別入場者情報"

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
    Private Sub frmPRINT04_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '顧客種別取得
            GetKBMAST()

            Me.cmbTerm.SelectedIndex = 0

            Me.btnSearch.PerformClick()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 天気ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnWEATHER_Click(sender As System.Object, e As System.EventArgs) Handles btnWEATHER01.Click, btnWEATHER02.Click, btnWEATHER03.Click, btnWEATHER04.Click, btnWEATHER05.Click, btnWEATHER06.Click, btnWEATHER07.Click _
                                                                                        , btnWEATHER08.Click, btnWEATHER09.Click, btnWEATHER10.Click, btnWEATHER11.Click, btnWEATHER12.Click, btnWEATHER13.Click, btnWEATHER14.Click _
                                                                                        , btnWEATHER15.Click, btnWEATHER16.Click, btnWEATHER17.Click, btnWEATHER18.Click, btnWEATHER19.Click, btnWEATHER20.Click, btnWEATHER21.Click _
                                                                                        , btnWEATHER22.Click, btnWEATHER23.Click, btnWEATHER24.Click
        Try

            Dim strTenki As String = String.Empty

            Using frm As New frmWEATHER
                frm.ShowDialog()
                strTenki = frm.SelectTenki
            End Using

            If String.IsNullOrEmpty(strTenki) Then Exit Sub

            Dim btnWEATHER As Button
            btnWEATHER = CType(sender, Button)

            Select Case CType(btnWEATHER.Tag, Integer)
                Case 1 : Me.txtWEATHER01.Text = strTenki
                Case 2 : Me.txtWEATHER02.Text = strTenki
                Case 3 : Me.txtWEATHER03.Text = strTenki
                Case 4 : Me.txtWEATHER04.Text = strTenki
                Case 5 : Me.txtWEATHER05.Text = strTenki
                Case 6 : Me.txtWEATHER06.Text = strTenki
                Case 7 : Me.txtWEATHER07.Text = strTenki
                Case 8 : Me.txtWEATHER08.Text = strTenki
                Case 9 : Me.txtWEATHER09.Text = strTenki
                Case 10 : Me.txtWEATHER10.Text = strTenki
                Case 11 : Me.txtWEATHER11.Text = strTenki
                Case 12 : Me.txtWEATHER12.Text = strTenki
                Case 13 : Me.txtWEATHER13.Text = strTenki
                Case 14 : Me.txtWEATHER14.Text = strTenki
                Case 15 : Me.txtWEATHER15.Text = strTenki
                Case 16 : Me.txtWEATHER16.Text = strTenki
                Case 17 : Me.txtWEATHER17.Text = strTenki
                Case 18 : Me.txtWEATHER18.Text = strTenki
                Case 19 : Me.txtWEATHER19.Text = strTenki
                Case 20 : Me.txtWEATHER20.Text = strTenki
                Case 21 : Me.txtWEATHER21.Text = strTenki
                Case 22 : Me.txtWEATHER22.Text = strTenki
                Case 23 : Me.txtWEATHER23.Text = strTenki
                Case 24 : Me.txtWEATHER24.Text = strTenki
            End Select


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
    Private Sub dtpSEATDT_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpStaSEATDT.ValueChanged, dtpEndSEATDT.ValueChanged
        Dim blnWEATHER As Boolean = False
        Dim blnTEMPERATURE As Boolean = False
        Try
            If Me.dtpStaSEATDT.Text.Equals(Me.dtpEndSEATDT.Text) Then
                '登録ボタン使用可
                Me.tspFunc12.Enabled = True
                blnWEATHER = True
                blnTEMPERATURE = True
            Else
                '登録ボタン使用不可
                Me.tspFunc12.Enabled = False
                blnWEATHER = False
                blnTEMPERATURE = False
            End If
            '天気
            Me.txtWEATHER01.Enabled = blnWEATHER
            Me.txtWEATHER02.Enabled = blnWEATHER
            Me.txtWEATHER03.Enabled = blnWEATHER
            Me.txtWEATHER04.Enabled = blnWEATHER
            Me.txtWEATHER05.Enabled = blnWEATHER
            Me.txtWEATHER06.Enabled = blnWEATHER
            Me.txtWEATHER07.Enabled = blnWEATHER
            Me.txtWEATHER08.Enabled = blnWEATHER
            Me.txtWEATHER09.Enabled = blnWEATHER
            Me.txtWEATHER10.Enabled = blnWEATHER
            Me.txtWEATHER11.Enabled = blnWEATHER
            Me.txtWEATHER12.Enabled = blnWEATHER
            Me.txtWEATHER13.Enabled = blnWEATHER
            Me.txtWEATHER14.Enabled = blnWEATHER
            Me.txtWEATHER15.Enabled = blnWEATHER
            Me.txtWEATHER16.Enabled = blnWEATHER
            Me.txtWEATHER17.Enabled = blnWEATHER
            Me.txtWEATHER18.Enabled = blnWEATHER
            Me.txtWEATHER19.Enabled = blnWEATHER
            Me.txtWEATHER20.Enabled = blnWEATHER
            Me.txtWEATHER21.Enabled = blnWEATHER
            Me.txtWEATHER22.Enabled = blnWEATHER
            Me.txtWEATHER23.Enabled = blnWEATHER
            Me.txtWEATHER24.Enabled = blnWEATHER
            '気温
            Me.txtTEMPERATURE01.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE02.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE03.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE04.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE05.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE06.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE07.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE08.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE09.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE10.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE11.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE12.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE13.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE14.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE15.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE16.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE17.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE18.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE19.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE20.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE21.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE22.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE23.Enabled = blnTEMPERATURE
            Me.txtTEMPERATURE24.Enabled = blnTEMPERATURE

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別コンボボックス_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbKSBKB_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbKSBKB.SelectedIndexChanged
        Dim bln As Boolean = True
        Try

            If Me.cmbKSBKB.SelectedIndex > 0 Then
                bln = False
            End If

            Me.Label8.Visible = bln
            Me.lblBALLSU01.Visible = bln
            Me.lblBALLSU02.Visible = bln
            Me.lblBALLSU03.Visible = bln
            Me.lblBALLSU04.Visible = bln
            Me.lblBALLSU05.Visible = bln
            Me.lblBALLSU06.Visible = bln
            Me.lblBALLSU07.Visible = bln
            Me.lblBALLSU08.Visible = bln
            Me.lblBALLSU09.Visible = bln
            Me.lblBALLSU10.Visible = bln
            Me.lblBALLSU11.Visible = bln
            Me.lblBALLSU12.Visible = bln
            Me.lblBALLSU13.Visible = bln
            Me.lblBALLSU14.Visible = bln
            Me.lblBALLSU15.Visible = bln
            Me.lblBALLSU16.Visible = bln
            Me.lblBALLSU17.Visible = bln
            Me.lblBALLSU18.Visible = bln
            Me.lblBALLSU19.Visible = bln
            Me.lblBALLSU20.Visible = bln
            Me.lblBALLSU21.Visible = bln
            Me.lblBALLSU22.Visible = bln
            Me.lblBALLSU23.Visible = bln
            Me.lblBALLSU24.Visible = bln

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
        Dim intYear As Integer = Now.Date.Year
        Dim intMonth As Integer = Now.Date.Month
        Dim intDay As Integer = Now.Date.Day
        Try
            Me.dtpStaSEATDT.Enabled = False
            Me.dtpEndSEATDT.Enabled = False

            Select Case Me.cmbTerm.SelectedIndex
                Case 0      '任意入力
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                    Me.dtpStaSEATDT.Enabled = True
                    Me.dtpEndSEATDT.Enabled = True
                    Me.dtpStaSEATDT.Focus()
                    Me.dtpStaSEATDT.Select()
                Case 1      '今月
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                Case 2      '前月
                    intMonth -= 1
                    If intMonth.Equals(0) Then
                        intMonth = 12
                        intYear -= 1
                    End If
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & DateTime.DaysInMonth(intYear, intMonth).ToString.PadLeft(2, "0"c)
                Case 3      '今年
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/01/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/" & intMonth.ToString.PadLeft(2, "0"c) & "/" & intDay.ToString.PadLeft(2, "0"c)
                Case 4      '前年
                    intYear -= 1
                    Me.dtpStaSEATDT.Text = intYear.ToString & "/01/01"
                    Me.dtpEndSEATDT.Text = intYear.ToString & "/12/31"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 気温テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTEMPERATURE_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTEMPERATURE01.KeyPress, txtTEMPERATURE02.KeyPress, txtTEMPERATURE03.KeyPress, txtTEMPERATURE04.KeyPress, txtTEMPERATURE05.KeyPress _
                                                                                                                    , txtTEMPERATURE06.KeyPress, txtTEMPERATURE07.KeyPress, txtTEMPERATURE08.KeyPress, txtTEMPERATURE09.KeyPress, txtTEMPERATURE10.KeyPress _
                                                                                                                    , txtTEMPERATURE11.KeyPress, txtTEMPERATURE12.KeyPress, txtTEMPERATURE13.KeyPress, txtTEMPERATURE14.KeyPress, txtTEMPERATURE15.KeyPress _
                                                                                                                    , txtTEMPERATURE16.KeyPress, txtTEMPERATURE17.KeyPress, txtTEMPERATURE18.KeyPress, txtTEMPERATURE19.KeyPress, txtTEMPERATURE20.KeyPress _
                                                                                                                    , txtTEMPERATURE21.KeyPress, txtTEMPERATURE22.KeyPress, txtTEMPERATURE23.KeyPress, txtTEMPERATURE24.KeyPress
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack And (Not e.KeyChar.Equals("-"c)) Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 気温テキストボックス_MouseDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTEMPERATURE_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles txtTEMPERATURE01.MouseDown, txtTEMPERATURE02.MouseDown, txtTEMPERATURE03.MouseDown, txtTEMPERATURE04.MouseDown, txtTEMPERATURE05.MouseDown _
                                                                                                                    , txtTEMPERATURE06.MouseDown, txtTEMPERATURE07.MouseDown, txtTEMPERATURE08.MouseDown, txtTEMPERATURE09.MouseDown, txtTEMPERATURE10.MouseDown _
                                                                                                                    , txtTEMPERATURE11.MouseDown, txtTEMPERATURE12.MouseDown, txtTEMPERATURE13.MouseDown, txtTEMPERATURE14.MouseDown, txtTEMPERATURE15.MouseDown _
                                                                                                                    , txtTEMPERATURE16.MouseDown, txtTEMPERATURE17.MouseDown, txtTEMPERATURE18.MouseDown, txtTEMPERATURE19.MouseDown, txtTEMPERATURE20.MouseDown _
                                                                                                                    , txtTEMPERATURE21.MouseDown, txtTEMPERATURE22.MouseDown, txtTEMPERATURE23.MouseDown, txtTEMPERATURE24.MouseDown
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 気温テキストボックス_Enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTEMPERATURE_Enter(sender As System.Object, e As System.EventArgs) Handles txtTEMPERATURE01.Enter, txtTEMPERATURE02.Enter, txtTEMPERATURE03.Enter, txtTEMPERATURE04.Enter, txtTEMPERATURE05.Enter _
                                                                                           , txtTEMPERATURE06.Enter, txtTEMPERATURE07.Enter, txtTEMPERATURE08.Enter, txtTEMPERATURE09.Enter, txtTEMPERATURE10.Enter _
                                                                                           , txtTEMPERATURE11.Enter, txtTEMPERATURE12.Enter, txtTEMPERATURE13.Enter, txtTEMPERATURE14.Enter, txtTEMPERATURE15.Enter _
                                                                                           , txtTEMPERATURE16.Enter, txtTEMPERATURE17.Enter, txtTEMPERATURE18.Enter, txtTEMPERATURE19.Enter, txtTEMPERATURE20.Enter _
                                                                                           , txtTEMPERATURE21.Enter, txtTEMPERATURE22.Enter, txtTEMPERATURE23.Enter, txtTEMPERATURE24.Enter
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 検索ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Dim sql As New System.Text.StringBuilder
        Dim dr As DataRow()
        Dim strStaJIKAN As String = "05"            '営業開始時間
        Dim strEndJIKAN As String = "24"            '営業終了時間
        Dim strStaSEATDT As String = String.Empty   '営業開始日付
        Dim strEndSEATDT As String = String.Empty   '営業終了日付
        Dim intCalENTSU As Integer = 0              '入数計算用
        Try
            '画面初期設定
            Init()

            strStaSEATDT = Me.dtpStaSEATDT.Text.Replace("/", String.Empty)
            strEndSEATDT = Me.dtpEndSEATDT.Text.Replace("/", String.Empty)

            If strStaSEATDT.Equals(strEndSEATDT) Then
                '【1日分表示】

                '登録ボタン使用可
                Me.tspFunc12.Enabled = True

                sql.Append(" SELECT ")
                sql.Append(" JIKAN")
                sql.Append(",WEATHER")
                sql.Append(",TEMPERATURE")
                sql.Append(",SUM(SERVICECNT) AS SERVICECNT")
                sql.Append(",SUM(ENTCNT) AS ENTCNT")
                sql.Append(",SUM(BALLSU) AS BALLSU")
                sql.Append(" FROM SEATSMC ")
                sql.Append(" WHERE 1=1")
                '営業日付
                sql.Append(" AND REPLACE(SEATDT, '/', '')  >= '" & strStaSEATDT & "'")
                sql.Append(" AND REPLACE(SEATDT, '/', '')  <= '" & strEndSEATDT & "'")
                '時間帯
                sql.Append(" AND JIKAN >= '" & strStaJIKAN & "'")
                sql.Append(" AND JIKAN <= '" & strEndJIKAN & "'")
                sql.Append(" GROUP BY JIKAN,WEATHER,TEMPERATURE")
                sql.Append(" ORDER BY JIKAN")
            Else
                '【2日以上表示】

                sql.Append(" SELECT ")
                sql.Append(" JIKAN")
                sql.Append(",SUM(SERVICECNT) AS SERVICECNT")
                sql.Append(",SUM(ENTCNT) AS ENTCNT")
                sql.Append(",SUM(BALLSU) AS BALLSU")
                sql.Append(" FROM SEATSMC ")
                sql.Append(" WHERE 1=1")
                '営業日付
                sql.Append(" AND REPLACE(SEATDT, '/', '')  >= '" & strStaSEATDT & "'")
                sql.Append(" AND REPLACE(SEATDT, '/', '')  <= '" & strEndSEATDT & "'")
                '時間帯
                sql.Append(" AND JIKAN >= '" & strStaJIKAN & "'")
                sql.Append(" AND JIKAN <= '" & strEndJIKAN & "'")
                sql.Append(" GROUP BY JIKAN")
                sql.Append(" ORDER BY JIKAN")
            End If

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count = 0 Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '時間帯別来場回数取得
            sql.Clear()
            sql.Append("SELECT TO_CHAR(INSDTM,'HH24') AS JIKAN,COUNT(*) AS ENTSU FROM (")
            sql.Append("SELECT ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM FROM ENTTRB  WHERE")
            sql.Append(" (ENTDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND ENTDT <= '" & strEndSEATDT & "')")
            If Me.cmbKSBKB.SelectedIndex > 0 Then
                sql.Append(" AND KSBKB = '" & Me.cmbKSBKB.SelectedIndex & "'")
            End If
            sql.Append(" UNION ")

            sql.Append("SELECT ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM FROM ENTTRA WHERE DATKB = '1'")
            sql.Append(" AND (ENTDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND ENTDT <= '" & strEndSEATDT & "')")
            If Me.cmbKSBKB.SelectedIndex > 0 Then
                sql.Append(" AND KSBKB = '" & Me.cmbKSBKB.SelectedIndex & "'")
            End If

            sql.Append(") AS A GROUP BY TO_CHAR(INSDTM,'HH24') ORDER BY TO_CHAR(INSDTM,'HH24')")

            Dim dtEntInfo As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '時間帯別打ち放題回数取得
            sql.Clear()
            sql.Append("SELECT TO_CHAR(A.INSDTM,'HH24') AS JIKAN,COUNT(A.*) AS ENTSU FROM ENTTRA AS A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" WHERE A.DATKB = '1'")
            sql.Append(" AND (A.ENTDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND A.ENTDT <= '" & strEndSEATDT & "')")
            sql.Append(" AND A.EIGKB = '2'")
            If Me.cmbKSBKB.SelectedIndex > 0 Then
                sql.Append(" AND B.NCSRANK = " & Me.cmbKSBKB.SelectedIndex)
            End If
            sql.Append(" GROUP BY  TO_CHAR(A.INSDTM,'HH24') ORDER BY TO_CHAR(A.INSDTM,'HH24')")

            Dim dtTEntInfo As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt.Rows.Count - 1
                '時間帯
                If i.Equals(0) Then
                    Me.lblJIKAN01.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY01.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT01.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(1) Then
                    Me.lblJIKAN02.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY02.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT02.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(2) Then
                    Me.lblJIKAN03.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY03.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT03.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(3) Then
                    Me.lblJIKAN04.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY04.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT04.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(4) Then
                    Me.lblJIKAN05.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY05.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT05.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(5) Then
                    Me.lblJIKAN06.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY06.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT06.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(6) Then
                    Me.lblJIKAN07.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY07.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT07.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(7) Then
                    Me.lblJIKAN08.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY08.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT08.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(8) Then
                    Me.lblJIKAN09.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY09.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT09.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(9) Then
                    Me.lblJIKAN10.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY10.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT10.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(10) Then
                    Me.lblJIKAN11.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY11.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT11.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(11) Then
                    Me.lblJIKAN12.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY12.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT12.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(12) Then
                    Me.lblJIKAN13.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY13.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT13.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(13) Then
                    Me.lblJIKAN14.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY14.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT14.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(14) Then
                    Me.lblJIKAN15.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY15.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT15.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(15) Then
                    Me.lblJIKAN16.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY16.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT16.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(16) Then
                    Me.lblJIKAN17.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY17.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT17.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(17) Then
                    Me.lblJIKAN18.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY18.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT18.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(18) Then
                    Me.lblJIKAN19.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY19.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT19.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(19) Then
                    Me.lblJIKAN20.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY20.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT20.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(20) Then
                    Me.lblJIKAN21.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY21.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT21.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(21) Then
                    Me.lblJIKAN22.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY22.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT22.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(22) Then
                    Me.lblJIKAN23.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY23.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT23.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If
                If i.Equals(23) Then
                    Me.lblJIKAN24.Text = resultDt.Rows(i).Item("JIKAN").ToString() & ":00"
                    '来場回数
                    dr = dtEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblQUANTITY24.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                    '打ち放題
                    dr = dtTEntInfo.Select("JIKAN = '" & (CType(resultDt.Rows(i).Item("JIKAN").ToString(), Integer) - 1).ToString.PadLeft(2, "0"c) & "'")
                    If dr.Length > 0 Then Me.lblSERVICECNT24.Text = CType(dr(0).Item("ENTSU").ToString, Integer).ToString("#,##0")
                End If

                If strStaSEATDT.Equals(strEndSEATDT) Then
                    '【1日分表示】
                    '天気
                    If i.Equals(0) Then Me.txtWEATHER01.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(1) Then Me.txtWEATHER02.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(2) Then Me.txtWEATHER03.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(3) Then Me.txtWEATHER04.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(4) Then Me.txtWEATHER05.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(5) Then Me.txtWEATHER06.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(6) Then Me.txtWEATHER07.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(7) Then Me.txtWEATHER08.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(8) Then Me.txtWEATHER09.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(9) Then Me.txtWEATHER10.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(10) Then Me.txtWEATHER11.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(11) Then Me.txtWEATHER12.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(12) Then Me.txtWEATHER13.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(13) Then Me.txtWEATHER14.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(14) Then Me.txtWEATHER15.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(15) Then Me.txtWEATHER16.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(16) Then Me.txtWEATHER17.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(17) Then Me.txtWEATHER18.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(18) Then Me.txtWEATHER19.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(19) Then Me.txtWEATHER20.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(20) Then Me.txtWEATHER21.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(21) Then Me.txtWEATHER22.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(22) Then Me.txtWEATHER23.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    If i.Equals(23) Then Me.txtWEATHER24.Text = resultDt.Rows(i).Item("WEATHER").ToString()
                    '気温
                    If i.Equals(0) Then Me.txtTEMPERATURE01.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(1) Then Me.txtTEMPERATURE02.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(2) Then Me.txtTEMPERATURE03.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(3) Then Me.txtTEMPERATURE04.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(4) Then Me.txtTEMPERATURE05.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(5) Then Me.txtTEMPERATURE06.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(6) Then Me.txtTEMPERATURE07.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(7) Then Me.txtTEMPERATURE08.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(8) Then Me.txtTEMPERATURE09.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(9) Then Me.txtTEMPERATURE10.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(10) Then Me.txtTEMPERATURE11.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(11) Then Me.txtTEMPERATURE12.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(12) Then Me.txtTEMPERATURE13.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(13) Then Me.txtTEMPERATURE14.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(14) Then Me.txtTEMPERATURE15.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(15) Then Me.txtTEMPERATURE16.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(16) Then Me.txtTEMPERATURE17.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(17) Then Me.txtTEMPERATURE18.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(18) Then Me.txtTEMPERATURE19.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(19) Then Me.txtTEMPERATURE20.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(20) Then Me.txtTEMPERATURE21.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(21) Then Me.txtTEMPERATURE22.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(22) Then Me.txtTEMPERATURE23.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                    If i.Equals(23) Then Me.txtTEMPERATURE24.Text = resultDt.Rows(i).Item("TEMPERATURE").ToString()
                End If
                '合計
                Me.lblENTCNT01.Text = CType(Me.lblQUANTITY01.Text, Integer).ToString("#,##0")
                Me.lblENTCNT02.Text = (CType(Me.lblENTCNT01.Text, Integer) + CType(Me.lblQUANTITY02.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT03.Text = (CType(Me.lblENTCNT02.Text, Integer) + CType(Me.lblQUANTITY03.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT04.Text = (CType(Me.lblENTCNT03.Text, Integer) + CType(Me.lblQUANTITY04.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT05.Text = (CType(Me.lblENTCNT04.Text, Integer) + CType(Me.lblQUANTITY05.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT06.Text = (CType(Me.lblENTCNT05.Text, Integer) + CType(Me.lblQUANTITY06.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT07.Text = (CType(Me.lblENTCNT06.Text, Integer) + CType(Me.lblQUANTITY07.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT08.Text = (CType(Me.lblENTCNT07.Text, Integer) + CType(Me.lblQUANTITY08.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT09.Text = (CType(Me.lblENTCNT08.Text, Integer) + CType(Me.lblQUANTITY09.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT10.Text = (CType(Me.lblENTCNT09.Text, Integer) + CType(Me.lblQUANTITY10.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT11.Text = (CType(Me.lblENTCNT10.Text, Integer) + CType(Me.lblQUANTITY11.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT12.Text = (CType(Me.lblENTCNT11.Text, Integer) + CType(Me.lblQUANTITY12.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT13.Text = (CType(Me.lblENTCNT12.Text, Integer) + CType(Me.lblQUANTITY13.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT14.Text = (CType(Me.lblENTCNT13.Text, Integer) + CType(Me.lblQUANTITY14.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT15.Text = (CType(Me.lblENTCNT14.Text, Integer) + CType(Me.lblQUANTITY15.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT16.Text = (CType(Me.lblENTCNT15.Text, Integer) + CType(Me.lblQUANTITY16.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT17.Text = (CType(Me.lblENTCNT16.Text, Integer) + CType(Me.lblQUANTITY17.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT18.Text = (CType(Me.lblENTCNT17.Text, Integer) + CType(Me.lblQUANTITY18.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT19.Text = (CType(Me.lblENTCNT18.Text, Integer) + CType(Me.lblQUANTITY19.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT20.Text = (CType(Me.lblENTCNT19.Text, Integer) + CType(Me.lblQUANTITY20.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT21.Text = (CType(Me.lblENTCNT20.Text, Integer) + CType(Me.lblQUANTITY21.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT22.Text = (CType(Me.lblENTCNT21.Text, Integer) + CType(Me.lblQUANTITY22.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT23.Text = (CType(Me.lblENTCNT22.Text, Integer) + CType(Me.lblQUANTITY23.Text, Integer)).ToString("#,##0")
                Me.lblENTCNT24.Text = (CType(Me.lblENTCNT23.Text, Integer) + CType(Me.lblQUANTITY24.Text, Integer)).ToString("#,##0")

                '打球数
                If i.Equals(0) Then Me.lblBALLSU01.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(1) Then Me.lblBALLSU02.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(2) Then Me.lblBALLSU03.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(3) Then Me.lblBALLSU04.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(4) Then Me.lblBALLSU05.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(5) Then Me.lblBALLSU06.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(6) Then Me.lblBALLSU07.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(7) Then Me.lblBALLSU08.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(8) Then Me.lblBALLSU09.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(9) Then Me.lblBALLSU10.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(10) Then Me.lblBALLSU11.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(11) Then Me.lblBALLSU12.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(12) Then Me.lblBALLSU13.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(13) Then Me.lblBALLSU14.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(14) Then Me.lblBALLSU15.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(15) Then Me.lblBALLSU16.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(16) Then Me.lblBALLSU17.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(17) Then Me.lblBALLSU18.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(18) Then Me.lblBALLSU19.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(19) Then Me.lblBALLSU20.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(20) Then Me.lblBALLSU21.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(21) Then Me.lblBALLSU22.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(22) Then Me.lblBALLSU23.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
                If i.Equals(23) Then Me.lblBALLSU24.Text = CType(resultDt.Rows(i).Item("BALLSU").ToString(), Integer).ToString("#,##0")
            Next

            If String.IsNullOrEmpty(Me.lblJIKAN01.Text) Then Me.lblQUANTITY01.Text = String.Empty : Me.lblSERVICECNT01.Text = String.Empty : Me.lblENTCNT01.Text = String.Empty : Me.lblBALLSU01.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN02.Text) Then Me.lblQUANTITY02.Text = String.Empty : Me.lblSERVICECNT02.Text = String.Empty : Me.lblENTCNT02.Text = String.Empty : Me.lblBALLSU02.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN03.Text) Then Me.lblQUANTITY03.Text = String.Empty : Me.lblSERVICECNT03.Text = String.Empty : Me.lblENTCNT03.Text = String.Empty : Me.lblBALLSU03.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN04.Text) Then Me.lblQUANTITY04.Text = String.Empty : Me.lblSERVICECNT04.Text = String.Empty : Me.lblENTCNT04.Text = String.Empty : Me.lblBALLSU04.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN05.Text) Then Me.lblQUANTITY05.Text = String.Empty : Me.lblSERVICECNT05.Text = String.Empty : Me.lblENTCNT05.Text = String.Empty : Me.lblBALLSU05.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN06.Text) Then Me.lblQUANTITY06.Text = String.Empty : Me.lblSERVICECNT06.Text = String.Empty : Me.lblENTCNT06.Text = String.Empty : Me.lblBALLSU06.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN07.Text) Then Me.lblQUANTITY07.Text = String.Empty : Me.lblSERVICECNT07.Text = String.Empty : Me.lblENTCNT07.Text = String.Empty : Me.lblBALLSU07.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN08.Text) Then Me.lblQUANTITY08.Text = String.Empty : Me.lblSERVICECNT08.Text = String.Empty : Me.lblENTCNT08.Text = String.Empty : Me.lblBALLSU08.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN09.Text) Then Me.lblQUANTITY09.Text = String.Empty : Me.lblSERVICECNT09.Text = String.Empty : Me.lblENTCNT09.Text = String.Empty : Me.lblBALLSU09.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN10.Text) Then Me.lblQUANTITY10.Text = String.Empty : Me.lblSERVICECNT10.Text = String.Empty : Me.lblENTCNT10.Text = String.Empty : Me.lblBALLSU10.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN11.Text) Then Me.lblQUANTITY11.Text = String.Empty : Me.lblSERVICECNT11.Text = String.Empty : Me.lblENTCNT11.Text = String.Empty : Me.lblBALLSU11.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN12.Text) Then Me.lblQUANTITY12.Text = String.Empty : Me.lblSERVICECNT12.Text = String.Empty : Me.lblENTCNT12.Text = String.Empty : Me.lblBALLSU12.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN13.Text) Then Me.lblQUANTITY13.Text = String.Empty : Me.lblSERVICECNT13.Text = String.Empty : Me.lblENTCNT13.Text = String.Empty : Me.lblBALLSU13.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN14.Text) Then Me.lblQUANTITY14.Text = String.Empty : Me.lblSERVICECNT14.Text = String.Empty : Me.lblENTCNT14.Text = String.Empty : Me.lblBALLSU14.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN15.Text) Then Me.lblQUANTITY15.Text = String.Empty : Me.lblSERVICECNT15.Text = String.Empty : Me.lblENTCNT15.Text = String.Empty : Me.lblBALLSU15.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN16.Text) Then Me.lblQUANTITY16.Text = String.Empty : Me.lblSERVICECNT16.Text = String.Empty : Me.lblENTCNT16.Text = String.Empty : Me.lblBALLSU16.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN17.Text) Then Me.lblQUANTITY17.Text = String.Empty : Me.lblSERVICECNT17.Text = String.Empty : Me.lblENTCNT17.Text = String.Empty : Me.lblBALLSU17.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN18.Text) Then Me.lblQUANTITY18.Text = String.Empty : Me.lblSERVICECNT18.Text = String.Empty : Me.lblENTCNT18.Text = String.Empty : Me.lblBALLSU18.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN19.Text) Then Me.lblQUANTITY19.Text = String.Empty : Me.lblSERVICECNT19.Text = String.Empty : Me.lblENTCNT19.Text = String.Empty : Me.lblBALLSU19.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN20.Text) Then Me.lblQUANTITY20.Text = String.Empty : Me.lblSERVICECNT20.Text = String.Empty : Me.lblENTCNT20.Text = String.Empty : Me.lblBALLSU20.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN21.Text) Then Me.lblQUANTITY21.Text = String.Empty : Me.lblSERVICECNT21.Text = String.Empty : Me.lblENTCNT21.Text = String.Empty : Me.lblBALLSU21.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN22.Text) Then Me.lblQUANTITY22.Text = String.Empty : Me.lblSERVICECNT22.Text = String.Empty : Me.lblENTCNT22.Text = String.Empty : Me.lblBALLSU22.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN23.Text) Then Me.lblQUANTITY23.Text = String.Empty : Me.lblSERVICECNT23.Text = String.Empty : Me.lblENTCNT23.Text = String.Empty : Me.lblBALLSU23.Text = String.Empty
            If String.IsNullOrEmpty(Me.lblJIKAN24.Text) Then Me.lblQUANTITY24.Text = String.Empty : Me.lblSERVICECNT24.Text = String.Empty : Me.lblENTCNT24.Text = String.Empty : Me.lblBALLSU24.Text = String.Empty

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Dim dtCSMAST As DataTable
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim strReportName As String = "時間帯別入場者情報"
            Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
                                             & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
                                             & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"
            'エクセル処理関連
            Dim app As Excel.Application
            Dim book As Excel.Workbook
            Dim sheet As Excel.Worksheet
            Dim border As Excel.Border = Nothing
            Dim xlrange As Excel.Range
            Dim xlpasterange As Excel.Range
            Dim break As Excel.HPageBreak

            app = CType(CreateObject("Excel.Application"), Excel.Application)
            app.Visible = False
            book = app.Workbooks.Open(UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName)

            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            '出力日
            sheet.Cells(1, 8) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString
            '営業日
            sheet.Cells(6, 3) = Me.dtpStaSEATDT.Text & "～" & Me.dtpEndSEATDT.Text

            Dim strJIKAN As String = String.Empty
            Dim strWEATHER As String = String.Empty
            Dim strTEMPERATURE As String = String.Empty
            Dim intQUANTITY As Integer = 0
            Dim intSERVICECNT As Integer = 0
            Dim intENTCNT As Integer = 0
            Dim intBALLSU As Integer = 0

            For i As Integer = 1 To 24
                If (i.Equals(1)) And (Not String.IsNullOrEmpty(Me.lblJIKAN01.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN01.Text
                    strWEATHER = Me.txtWEATHER01.Text
                    strTEMPERATURE = Me.txtTEMPERATURE01.Text
                    intQUANTITY = CType(Me.lblQUANTITY01.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT01.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT01.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU01.Text, Integer)
                ElseIf (i.Equals(2)) And (Not String.IsNullOrEmpty(Me.lblJIKAN02.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN02.Text
                    strWEATHER = Me.txtWEATHER02.Text
                    strTEMPERATURE = Me.txtTEMPERATURE02.Text
                    intQUANTITY = CType(Me.lblQUANTITY02.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT02.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT02.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU02.Text, Integer)
                ElseIf (i.Equals(3)) And (Not String.IsNullOrEmpty(Me.lblJIKAN03.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN03.Text
                    strWEATHER = Me.txtWEATHER03.Text
                    strTEMPERATURE = Me.txtTEMPERATURE03.Text
                    intQUANTITY = CType(Me.lblQUANTITY03.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT03.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT03.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU03.Text, Integer)
                ElseIf (i.Equals(4)) And (Not String.IsNullOrEmpty(Me.lblJIKAN04.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN04.Text
                    strWEATHER = Me.txtWEATHER04.Text
                    strTEMPERATURE = Me.txtTEMPERATURE04.Text
                    intQUANTITY = CType(Me.lblQUANTITY04.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT04.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT04.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU04.Text, Integer)
                ElseIf (i.Equals(5)) And (Not String.IsNullOrEmpty(Me.lblJIKAN05.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN05.Text
                    strWEATHER = Me.txtWEATHER05.Text
                    strTEMPERATURE = Me.txtTEMPERATURE05.Text
                    intQUANTITY = CType(Me.lblQUANTITY05.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT05.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT05.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU05.Text, Integer)
                ElseIf (i.Equals(6)) And (Not String.IsNullOrEmpty(Me.lblJIKAN06.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN06.Text
                    strWEATHER = Me.txtWEATHER06.Text
                    strTEMPERATURE = Me.txtTEMPERATURE06.Text
                    intQUANTITY = CType(Me.lblQUANTITY06.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT06.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT06.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU06.Text, Integer)
                ElseIf (i.Equals(7)) And (Not String.IsNullOrEmpty(Me.lblJIKAN07.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN07.Text
                    strWEATHER = Me.txtWEATHER07.Text
                    strTEMPERATURE = Me.txtTEMPERATURE07.Text
                    intQUANTITY = CType(Me.lblQUANTITY07.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT07.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT07.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU07.Text, Integer)
                ElseIf (i.Equals(8)) And (Not String.IsNullOrEmpty(Me.lblJIKAN08.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN08.Text
                    strWEATHER = Me.txtWEATHER08.Text
                    strTEMPERATURE = Me.txtTEMPERATURE08.Text
                    intQUANTITY = CType(Me.lblQUANTITY08.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT08.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT08.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU08.Text, Integer)
                ElseIf (i.Equals(9)) And (Not String.IsNullOrEmpty(Me.lblJIKAN09.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN09.Text
                    strWEATHER = Me.txtWEATHER09.Text
                    strTEMPERATURE = Me.txtTEMPERATURE09.Text
                    intQUANTITY = CType(Me.lblQUANTITY09.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT09.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT09.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU09.Text, Integer)
                ElseIf (i.Equals(10)) And (Not String.IsNullOrEmpty(Me.lblJIKAN10.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN10.Text
                    strWEATHER = Me.txtWEATHER10.Text
                    strTEMPERATURE = Me.txtTEMPERATURE10.Text
                    intQUANTITY = CType(Me.lblQUANTITY10.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT10.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT10.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU10.Text, Integer)
                ElseIf (i.Equals(11)) And (Not String.IsNullOrEmpty(Me.lblJIKAN11.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN11.Text
                    strWEATHER = Me.txtWEATHER11.Text
                    strTEMPERATURE = Me.txtTEMPERATURE11.Text
                    intQUANTITY = CType(Me.lblQUANTITY11.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT11.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT11.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU11.Text, Integer)
                ElseIf (i.Equals(12)) And (Not String.IsNullOrEmpty(Me.lblJIKAN12.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN12.Text
                    strWEATHER = Me.txtWEATHER12.Text
                    strTEMPERATURE = Me.txtTEMPERATURE12.Text
                    intQUANTITY = CType(Me.lblQUANTITY12.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT12.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT12.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU12.Text, Integer)
                ElseIf (i.Equals(13)) And (Not String.IsNullOrEmpty(Me.lblJIKAN13.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN13.Text
                    strWEATHER = Me.txtWEATHER13.Text
                    strTEMPERATURE = Me.txtTEMPERATURE13.Text
                    intQUANTITY = CType(Me.lblQUANTITY13.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT13.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT13.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU13.Text, Integer)
                ElseIf (i.Equals(14)) And (Not String.IsNullOrEmpty(Me.lblJIKAN14.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN14.Text
                    strWEATHER = Me.txtWEATHER14.Text
                    strTEMPERATURE = Me.txtTEMPERATURE14.Text
                    intQUANTITY = CType(Me.lblQUANTITY14.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT14.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT14.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU14.Text, Integer)
                ElseIf (i.Equals(15)) And (Not String.IsNullOrEmpty(Me.lblJIKAN15.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN15.Text
                    strWEATHER = Me.txtWEATHER15.Text
                    strTEMPERATURE = Me.txtTEMPERATURE15.Text
                    intQUANTITY = CType(Me.lblQUANTITY15.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT15.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT15.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU15.Text, Integer)
                ElseIf (i.Equals(16)) And (Not String.IsNullOrEmpty(Me.lblJIKAN16.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN16.Text
                    strWEATHER = Me.txtWEATHER16.Text
                    strTEMPERATURE = Me.txtTEMPERATURE16.Text
                    intQUANTITY = CType(Me.lblQUANTITY16.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT16.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT16.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU16.Text, Integer)
                ElseIf (i.Equals(17)) And (Not String.IsNullOrEmpty(Me.lblJIKAN17.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN17.Text
                    strWEATHER = Me.txtWEATHER17.Text
                    strTEMPERATURE = Me.txtTEMPERATURE17.Text
                    intQUANTITY = CType(Me.lblQUANTITY17.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT17.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT17.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU17.Text, Integer)
                ElseIf (i.Equals(18)) And (Not String.IsNullOrEmpty(Me.lblJIKAN18.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN18.Text
                    strWEATHER = Me.txtWEATHER18.Text
                    strTEMPERATURE = Me.txtTEMPERATURE18.Text
                    intQUANTITY = CType(Me.lblQUANTITY18.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT18.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT18.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU18.Text, Integer)
                ElseIf (i.Equals(19)) And (Not String.IsNullOrEmpty(Me.lblJIKAN19.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN19.Text
                    strWEATHER = Me.txtWEATHER19.Text
                    strTEMPERATURE = Me.txtTEMPERATURE19.Text
                    intQUANTITY = CType(Me.lblQUANTITY19.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT19.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT19.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU19.Text, Integer)
                ElseIf (i.Equals(20)) And (Not String.IsNullOrEmpty(Me.lblJIKAN20.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN20.Text
                    strWEATHER = Me.txtWEATHER20.Text
                    strTEMPERATURE = Me.txtTEMPERATURE20.Text
                    intQUANTITY = CType(Me.lblQUANTITY20.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT20.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT20.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU20.Text, Integer)
                ElseIf (i.Equals(21)) And (Not String.IsNullOrEmpty(Me.lblJIKAN21.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN21.Text
                    strWEATHER = Me.txtWEATHER21.Text
                    strTEMPERATURE = Me.txtTEMPERATURE21.Text
                    intQUANTITY = CType(Me.lblQUANTITY21.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT21.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT21.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU21.Text, Integer)
                ElseIf (i.Equals(22)) And (Not String.IsNullOrEmpty(Me.lblJIKAN22.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN22.Text
                    strWEATHER = Me.txtWEATHER22.Text
                    strTEMPERATURE = Me.txtTEMPERATURE22.Text
                    intQUANTITY = CType(Me.lblQUANTITY22.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT22.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT22.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU22.Text, Integer)
                ElseIf (i.Equals(23)) And (Not String.IsNullOrEmpty(Me.lblJIKAN23.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN23.Text
                    strWEATHER = Me.txtWEATHER23.Text
                    strTEMPERATURE = Me.txtTEMPERATURE23.Text
                    intQUANTITY = CType(Me.lblQUANTITY23.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT23.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT23.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU23.Text, Integer)
                ElseIf (i.Equals(24)) And (Not String.IsNullOrEmpty(Me.lblJIKAN24.Text)) Then
                    strJIKAN = "～" & Me.lblJIKAN24.Text
                    strWEATHER = Me.txtWEATHER24.Text
                    strTEMPERATURE = Me.txtTEMPERATURE24.Text
                    intQUANTITY = CType(Me.lblQUANTITY24.Text, Integer)
                    intSERVICECNT = CType(Me.lblSERVICECNT24.Text, Integer)
                    intENTCNT = CType(Me.lblENTCNT24.Text, Integer)
                    intBALLSU = CType(Me.lblBALLSU24.Text, Integer)
                Else
                    Continue For
                End If

                '選択種別
                sheet.Cells(7, 2) = Me.cmbKSBKB.Text

                '時間帯
                sheet.Cells(10 + (i - 1), 2) = strJIKAN
                '天気
                sheet.Cells(10 + (i - 1), 3) = strWEATHER
                '気温
                If Not String.IsNullOrEmpty(strTEMPERATURE) Then
                    sheet.Cells(10 + (i - 1), 4) = strTEMPERATURE & "℃"
                End If
                '来場回数
                sheet.Cells(10 + (i - 1), 5) = intQUANTITY.ToString("#,##0")
                '内法愛
                sheet.Cells(10 + (i - 1), 6) = intSERVICECNT.ToString("#,##0")
                '合計
                sheet.Cells(10 + (i - 1), 7) = intENTCNT.ToString("#,##0")
                '打球数
                If Me.cmbKSBKB.SelectedIndex < 1 Then
                    sheet.Cells(10 + (i - 1), 8) = intBALLSU.ToString("#,##0")
                End If

                If i.Equals(UIUtility.SYSTEM.SEATSU) Then
                    border = sheet.Range("B" & 10 + (i - 1), "H" & 10 + (i - 1)).Borders(Excel.XlBordersIndex.xlEdgeTop)
                    border.LineStyle = Excel.XlLineStyle.xlDash
                    border = sheet.Range("B" & 10 + i, "H" & 10 + i).Borders(Excel.XlBordersIndex.xlEdgeTop)
                    border.LineStyle = Excel.XlLineStyle.xlDouble

                Else
                    If i.Equals(1) Then Continue For
                    border = sheet.Range("B" & 10 + (i - 1), "H" & 10 + (i - 1)).Borders(Excel.XlBordersIndex.xlEdgeTop)
                    border.LineStyle = Excel.XlLineStyle.xlDash
                End If
            Next
            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Dim sql As New System.Text.StringBuilder
        Dim strJIKAN As String = String.Empty
        Dim strWEATHER As String = String.Empty
        Dim strTEMPERATURE As String = String.Empty
        Try
            If Not Me.dtpStaSEATDT.Text.Equals(Me.dtpEndSEATDT.Text) Then
                Using frm As New frmMSGBOX01("２日以上の天気・気温は登録できません。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If


            Using frm As New frmMSGBOX01("天気・気温を登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            Me.Cursor = Cursors.WaitCursor



            'トランザクション開始
            iDatabase.BeginTransaction()


            For i As Integer = 1 To 24
                If i.Equals(1) Then strJIKAN = Me.lblJIKAN01.Text : strWEATHER = Me.txtWEATHER01.Text : strTEMPERATURE = Me.txtTEMPERATURE01.Text
                If i.Equals(2) Then strJIKAN = Me.lblJIKAN02.Text : strWEATHER = Me.txtWEATHER02.Text : strTEMPERATURE = Me.txtTEMPERATURE02.Text
                If i.Equals(3) Then strJIKAN = Me.lblJIKAN03.Text : strWEATHER = Me.txtWEATHER03.Text : strTEMPERATURE = Me.txtTEMPERATURE03.Text
                If i.Equals(4) Then strJIKAN = Me.lblJIKAN04.Text : strWEATHER = Me.txtWEATHER04.Text : strTEMPERATURE = Me.txtTEMPERATURE04.Text
                If i.Equals(5) Then strJIKAN = Me.lblJIKAN05.Text : strWEATHER = Me.txtWEATHER05.Text : strTEMPERATURE = Me.txtTEMPERATURE05.Text
                If i.Equals(6) Then strJIKAN = Me.lblJIKAN06.Text : strWEATHER = Me.txtWEATHER06.Text : strTEMPERATURE = Me.txtTEMPERATURE06.Text
                If i.Equals(7) Then strJIKAN = Me.lblJIKAN07.Text : strWEATHER = Me.txtWEATHER07.Text : strTEMPERATURE = Me.txtTEMPERATURE07.Text
                If i.Equals(8) Then strJIKAN = Me.lblJIKAN08.Text : strWEATHER = Me.txtWEATHER08.Text : strTEMPERATURE = Me.txtTEMPERATURE08.Text
                If i.Equals(9) Then strJIKAN = Me.lblJIKAN09.Text : strWEATHER = Me.txtWEATHER09.Text : strTEMPERATURE = Me.txtTEMPERATURE09.Text
                If i.Equals(10) Then strJIKAN = Me.lblJIKAN10.Text : strWEATHER = Me.txtWEATHER10.Text : strTEMPERATURE = Me.txtTEMPERATURE10.Text
                If i.Equals(11) Then strJIKAN = Me.lblJIKAN11.Text : strWEATHER = Me.txtWEATHER11.Text : strTEMPERATURE = Me.txtTEMPERATURE11.Text
                If i.Equals(12) Then strJIKAN = Me.lblJIKAN12.Text : strWEATHER = Me.txtWEATHER12.Text : strTEMPERATURE = Me.txtTEMPERATURE12.Text
                If i.Equals(13) Then strJIKAN = Me.lblJIKAN13.Text : strWEATHER = Me.txtWEATHER13.Text : strTEMPERATURE = Me.txtTEMPERATURE13.Text
                If i.Equals(14) Then strJIKAN = Me.lblJIKAN14.Text : strWEATHER = Me.txtWEATHER14.Text : strTEMPERATURE = Me.txtTEMPERATURE14.Text
                If i.Equals(15) Then strJIKAN = Me.lblJIKAN15.Text : strWEATHER = Me.txtWEATHER15.Text : strTEMPERATURE = Me.txtTEMPERATURE15.Text
                If i.Equals(16) Then strJIKAN = Me.lblJIKAN16.Text : strWEATHER = Me.txtWEATHER16.Text : strTEMPERATURE = Me.txtTEMPERATURE16.Text
                If i.Equals(17) Then strJIKAN = Me.lblJIKAN17.Text : strWEATHER = Me.txtWEATHER17.Text : strTEMPERATURE = Me.txtTEMPERATURE17.Text
                If i.Equals(18) Then strJIKAN = Me.lblJIKAN18.Text : strWEATHER = Me.txtWEATHER18.Text : strTEMPERATURE = Me.txtTEMPERATURE18.Text
                If i.Equals(19) Then strJIKAN = Me.lblJIKAN19.Text : strWEATHER = Me.txtWEATHER19.Text : strTEMPERATURE = Me.txtTEMPERATURE19.Text
                If i.Equals(20) Then strJIKAN = Me.lblJIKAN20.Text : strWEATHER = Me.txtWEATHER20.Text : strTEMPERATURE = Me.txtTEMPERATURE20.Text
                If i.Equals(21) Then strJIKAN = Me.lblJIKAN21.Text : strWEATHER = Me.txtWEATHER21.Text : strTEMPERATURE = Me.txtTEMPERATURE21.Text
                If i.Equals(22) Then strJIKAN = Me.lblJIKAN22.Text : strWEATHER = Me.txtWEATHER22.Text : strTEMPERATURE = Me.txtTEMPERATURE22.Text
                If i.Equals(23) Then strJIKAN = Me.lblJIKAN23.Text : strWEATHER = Me.txtWEATHER23.Text : strTEMPERATURE = Me.txtTEMPERATURE23.Text
                If i.Equals(24) Then strJIKAN = Me.lblJIKAN24.Text : strWEATHER = Me.txtWEATHER24.Text : strTEMPERATURE = Me.txtTEMPERATURE24.Text

                If Not strJIKAN.Equals(String.Empty) Then
                    sql.Append("UPDATE SEATSMC SET WEATHER = '" & strWEATHER & "',TEMPERATURE = '" & strTEMPERATURE & "' WHERE SEATDT = '" & Now.ToString("yyyyMMdd") & "' AND JIKAN = '" & strJIKAN.Substring(0, 2) & "';")
                End If
            Next
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("天気・気温の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'コミット
            iDatabase.Commit()

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using



        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Me.btnSearch.PerformClick()
            Me.Cursor = Cursors.Default
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
            Me.tspFunc3.Enabled = False
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

            '時間帯
            Me.lblJIKAN01.Text = String.Empty : Me.lblJIKAN02.Text = String.Empty : Me.lblJIKAN03.Text = String.Empty : Me.lblJIKAN04.Text = String.Empty : Me.lblJIKAN05.Text = String.Empty
            Me.lblJIKAN06.Text = String.Empty : Me.lblJIKAN07.Text = String.Empty : Me.lblJIKAN08.Text = String.Empty : Me.lblJIKAN09.Text = String.Empty : Me.lblJIKAN10.Text = String.Empty
            Me.lblJIKAN11.Text = String.Empty : Me.lblJIKAN12.Text = String.Empty : Me.lblJIKAN13.Text = String.Empty : Me.lblJIKAN14.Text = String.Empty : Me.lblJIKAN15.Text = String.Empty
            Me.lblJIKAN16.Text = String.Empty : Me.lblJIKAN17.Text = String.Empty : Me.lblJIKAN18.Text = String.Empty : Me.lblJIKAN19.Text = String.Empty : Me.lblJIKAN20.Text = String.Empty
            Me.lblJIKAN21.Text = String.Empty : Me.lblJIKAN22.Text = String.Empty : Me.lblJIKAN23.Text = String.Empty : Me.lblJIKAN24.Text = String.Empty
            '天気
            Me.txtWEATHER01.Text = String.Empty : Me.txtWEATHER02.Text = String.Empty : Me.txtWEATHER03.Text = String.Empty : Me.txtWEATHER04.Text = String.Empty : Me.txtWEATHER05.Text = String.Empty
            Me.txtWEATHER06.Text = String.Empty : Me.txtWEATHER07.Text = String.Empty : Me.txtWEATHER08.Text = String.Empty : Me.txtWEATHER09.Text = String.Empty : Me.txtWEATHER10.Text = String.Empty
            Me.txtWEATHER11.Text = String.Empty : Me.txtWEATHER12.Text = String.Empty : Me.txtWEATHER13.Text = String.Empty : Me.txtWEATHER14.Text = String.Empty : Me.txtWEATHER15.Text = String.Empty
            Me.txtWEATHER16.Text = String.Empty : Me.txtWEATHER17.Text = String.Empty : Me.txtWEATHER18.Text = String.Empty : Me.txtWEATHER19.Text = String.Empty : Me.txtWEATHER20.Text = String.Empty
            Me.txtWEATHER21.Text = String.Empty : Me.txtWEATHER22.Text = String.Empty : Me.txtWEATHER23.Text = String.Empty : Me.txtWEATHER24.Text = String.Empty
            '気温
            Me.txtTEMPERATURE01.Text = String.Empty : Me.txtTEMPERATURE02.Text = String.Empty : Me.txtTEMPERATURE03.Text = String.Empty : Me.txtTEMPERATURE04.Text = String.Empty : Me.txtTEMPERATURE05.Text = String.Empty
            Me.txtTEMPERATURE06.Text = String.Empty : Me.txtTEMPERATURE07.Text = String.Empty : Me.txtTEMPERATURE08.Text = String.Empty : Me.txtTEMPERATURE09.Text = String.Empty : Me.txtTEMPERATURE10.Text = String.Empty
            Me.txtTEMPERATURE11.Text = String.Empty : Me.txtTEMPERATURE12.Text = String.Empty : Me.txtTEMPERATURE13.Text = String.Empty : Me.txtTEMPERATURE14.Text = String.Empty : Me.txtTEMPERATURE15.Text = String.Empty
            Me.txtTEMPERATURE16.Text = String.Empty : Me.txtTEMPERATURE17.Text = String.Empty : Me.txtTEMPERATURE18.Text = String.Empty : Me.txtTEMPERATURE19.Text = String.Empty : Me.txtTEMPERATURE20.Text = String.Empty
            Me.txtTEMPERATURE21.Text = String.Empty : Me.txtTEMPERATURE22.Text = String.Empty : Me.txtTEMPERATURE23.Text = String.Empty : Me.txtTEMPERATURE24.Text = String.Empty
            '来場回数
            Me.lblQUANTITY01.Text = "0" : Me.lblQUANTITY02.Text = "0" : Me.lblQUANTITY03.Text = "0" : Me.lblQUANTITY04.Text = "0" : Me.lblQUANTITY05.Text = "0"
            Me.lblQUANTITY06.Text = "0" : Me.lblQUANTITY07.Text = "0" : Me.lblQUANTITY08.Text = "0" : Me.lblQUANTITY09.Text = "0" : Me.lblQUANTITY10.Text = "0"
            Me.lblQUANTITY11.Text = "0" : Me.lblQUANTITY12.Text = "0" : Me.lblQUANTITY13.Text = "0" : Me.lblQUANTITY14.Text = "0" : Me.lblQUANTITY15.Text = "0"
            Me.lblQUANTITY16.Text = "0" : Me.lblQUANTITY17.Text = "0" : Me.lblQUANTITY18.Text = "0" : Me.lblQUANTITY19.Text = "0" : Me.lblQUANTITY20.Text = "0"
            Me.lblQUANTITY21.Text = "0" : Me.lblQUANTITY22.Text = "0" : Me.lblQUANTITY23.Text = "0" : Me.lblQUANTITY24.Text = "0"
            '打ち放題
            Me.lblSERVICECNT01.Text = "0" : Me.lblSERVICECNT02.Text = "0" : Me.lblSERVICECNT03.Text = "0" : Me.lblSERVICECNT04.Text = "0" : Me.lblSERVICECNT05.Text = "0"
            Me.lblSERVICECNT06.Text = "0" : Me.lblSERVICECNT07.Text = "0" : Me.lblSERVICECNT08.Text = "0" : Me.lblSERVICECNT09.Text = "0" : Me.lblSERVICECNT10.Text = "0"
            Me.lblSERVICECNT11.Text = "0" : Me.lblSERVICECNT12.Text = "0" : Me.lblSERVICECNT13.Text = "0" : Me.lblSERVICECNT14.Text = "0" : Me.lblSERVICECNT15.Text = "0"
            Me.lblSERVICECNT16.Text = "0" : Me.lblSERVICECNT17.Text = "0" : Me.lblSERVICECNT18.Text = "0" : Me.lblSERVICECNT19.Text = "0" : Me.lblSERVICECNT20.Text = "0"
            Me.lblSERVICECNT21.Text = "0" : Me.lblSERVICECNT22.Text = "0" : Me.lblSERVICECNT23.Text = "0" : Me.lblSERVICECNT24.Text = "0"
            '合計
            Me.lblENTCNT01.Text = "0" : Me.lblENTCNT02.Text = "0" : Me.lblENTCNT03.Text = "0" : Me.lblENTCNT04.Text = "0" : Me.lblENTCNT05.Text = "0"
            Me.lblENTCNT06.Text = "0" : Me.lblENTCNT07.Text = "0" : Me.lblENTCNT08.Text = "0" : Me.lblENTCNT09.Text = "0" : Me.lblENTCNT10.Text = "0"
            Me.lblENTCNT11.Text = "0" : Me.lblENTCNT12.Text = "0" : Me.lblENTCNT13.Text = "0" : Me.lblENTCNT14.Text = "0" : Me.lblENTCNT15.Text = "0"
            Me.lblENTCNT16.Text = "0" : Me.lblENTCNT17.Text = "0" : Me.lblENTCNT18.Text = "0" : Me.lblENTCNT19.Text = "0" : Me.lblENTCNT20.Text = "0"
            Me.lblENTCNT21.Text = "0" : Me.lblENTCNT22.Text = "0" : Me.lblENTCNT23.Text = "0" : Me.lblENTCNT24.Text = "0"
            '打球数
            Me.lblBALLSU01.Text = "0" : Me.lblBALLSU02.Text = "0" : Me.lblBALLSU03.Text = "0" : Me.lblBALLSU04.Text = "0" : Me.lblBALLSU05.Text = "0"
            Me.lblBALLSU06.Text = "0" : Me.lblBALLSU07.Text = "0" : Me.lblBALLSU08.Text = "0" : Me.lblBALLSU09.Text = "0" : Me.lblBALLSU10.Text = "0"
            Me.lblBALLSU11.Text = "0" : Me.lblBALLSU12.Text = "0" : Me.lblBALLSU13.Text = "0" : Me.lblBALLSU14.Text = "0" : Me.lblBALLSU15.Text = "0"
            Me.lblBALLSU16.Text = "0" : Me.lblBALLSU17.Text = "0" : Me.lblBALLSU18.Text = "0" : Me.lblBALLSU19.Text = "0" : Me.lblBALLSU20.Text = "0"
            Me.lblBALLSU21.Text = "0" : Me.lblBALLSU22.Text = "0" : Me.lblBALLSU23.Text = "0" : Me.lblBALLSU24.Text = "0"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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








End Class
