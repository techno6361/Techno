Imports Techno.DataBase
Imports Microsoft.Office.Interop

Public Class frmPRINT01

#Region "▼宣言部"

    ''' <summary>
    ''' 一覧から取得した顧客番号セットテキストボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtNCSNO As TextBox

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客一覧/ラベル"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客一覧/ラベル"

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
    Private Sub frmPRINT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 顧客番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNCSNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaNCSNO.Validated, txtEndNCSNO.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            txtBox.Text = txtBox.Text.PadLeft(8, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
    ''' 誕生月テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBIRTHMM_Validated(sender As System.Object, e As System.EventArgs) Handles txtBIRTHMM.Validated
        Try
            If String.IsNullOrEmpty(Me.txtBIRTHMM.Text) Then Exit Sub

            Me.txtBIRTHMM.Text = Me.txtBIRTHMM.Text.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

            '画面初期化
            Init()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            '顧客データ取得
            dtCSMAST = GetCSMAST()

            If dtCSMAST.Rows.Count.Equals(0) Then
                Using frm As New frmMSGBOX01("対象データがありませんでした。", 1)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            ' *** HACK ADD 2018/01/16 TERAYAMA グリッドビューの選択行のみdtCSMASTを上書き
            SetSelectedTable(dtCSMAST)

            If Me.rdoPrint1.Checked Then
                '【顧客一覧】

                Dim strReportName As String = "顧客一覧"
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



                '書き込み対象行番号
                Dim intLine As Integer = 7
                Dim intKOKNMLine As Integer = 8
                Dim intPageLine As Integer = 37
                '改ページ行数
                Dim intBLine As Integer = 38
                'ページ数
                Dim intPage As Integer = 1

                '出力日
                sheet.Cells(1, 7) = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString
                '件数
                sheet.Cells(4, 7) = dtCSMAST.Rows.Count & "件"

                Dim strNCSNO As String = String.Empty

                For i As Integer = 0 To dtCSMAST.Rows.Count - 1

                    '連番
                    sheet.Cells(intLine + (i * 2), 2) = (i + 1).ToString
                    '会員種別
                    sheet.Cells(intLine + (i * 2) + 1, 2) = dtCSMAST.Rows(i).Item("CKBNAME").ToString()
                    '顧客番号
                    strNCSNO = dtCSMAST.Rows(i).Item("NCSNO").ToString().PadLeft(8, "0"c)
                    If Not String.IsNullOrEmpty(dtCSMAST.Rows(i).Item("DSCLMANNO").ToString()) Then
                        strNCSNO &= "(" & dtCSMAST.Rows(i).Item("DSCLMANNO").ToString() & ")"
                    End If
                    sheet.Cells(intLine + (i * 2), 3) = strNCSNO
                    '氏名
                    sheet.Cells(intLine + (i * 2), 4) = dtCSMAST.Rows(i).Item("CCSNAME").ToString()
                    '郵便番号
                    sheet.Cells(intLine + (i * 2), 5) = dtCSMAST.Rows(i).Item("NZIP").ToString
                    '住所
                    sheet.Cells(intLine + (i * 2), 6) = dtCSMAST.Rows(i).Item("CADDRESS1").ToString
                    sheet.Cells(intLine + (i * 2) + 1, 6) = dtCSMAST.Rows(i).Item("CADDRESS2").ToString
                    '電話番号
                    sheet.Cells(intLine + (i * 2), 7) = dtCSMAST.Rows(i).Item("CTELEPHONE").ToString
                    '携帯番号
                    sheet.Cells(intLine + (i * 2) + 1, 7) = dtCSMAST.Rows(i).Item("CPOTABLENUM").ToString
                    '月間/総来場回数
                    sheet.Cells(intLine + (i * 2) + 1, 3) = dtCSMAST.Rows(i).Item("ENTCNT2").ToString & "/" & dtCSMAST.Rows(i).Item("ENTCNT").ToString
                    '入会日
                    sheet.Cells(intLine + (i * 2) + 1, 4) = dtCSMAST.Rows(i).Item("TO_DENTRY").ToString
                    '会員期限
                    sheet.Cells(intLine + (i * 2) + 1, 5) = dtCSMAST.Rows(i).Item("TO_DMEMBER").ToString

                    '1ページは14件まで
                    If ((i + 1) Mod 14).Equals(0) Then
                        If i.Equals(dtCSMAST.Rows.Count - 1) Then
                            Exit For
                        End If

                        '改ページ処理
                        break = sheet.HPageBreaks.Add(sheet.Range("A" & (intBLine + ((intPage - 1) * 37))))
                        '改ページ後は－１
                        intLine += 9
                        'セル書式コピー
                        xlrange = sheet.Cells.Range("A1:G37")
                        xlpasterange = sheet.Range("A" & (intBLine + ((intPage - 1) * 37)))
                        xlrange.Copy(xlpasterange)
                        'コピー後の値削除
                        xlrange = sheet.Cells.Range("A" & (intBLine + ((intPage - 1) * 37) + 6) & ":G" & (37 * (intPage + 1)))
                        xlrange.ClearContents()
                        'ページ数カウントアップ
                        intPage += 1
                    End If
                    'ページ数
                    sheet.Cells(intPageLine + (intPageLine * (intPage - 1)), 2) = intPage
                Next

                'ファイル保存
                book.SaveAs(strSaveReportName)

                book.Close()

                System.Diagnostics.Process.Start(strSaveReportName)
            Else
                '【宛名ラベル】

                Dim strReportName As String = "宛名ラベル"
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


                '書き込み対象列番号
                Dim intCol As Integer = 2
                '書き込み対象行番号
                Dim intLine As Integer = 0
                '改ページ行数
                Dim intBLine As Integer = 54
                'ページ数
                Dim intPage As Integer = 1

                For i As Integer = 0 To dtCSMAST.Rows.Count - 1
                    If ((i + 1) Mod 2).Equals(0) Then
                        '偶数
                        intCol = 6
                    Else
                        '奇数

                        intCol = 2
                    End If
                    '郵便番号
                    sheet.Cells(2 + intLine, intCol) = "〒" & dtCSMAST.Rows(i).Item("NZIP").ToString
                    '住所１
                    sheet.Cells(3 + intLine, intCol) = dtCSMAST.Rows(i).Item("CADDRESS1").ToString
                    '住所２
                    sheet.Cells(4 + intLine, intCol) = dtCSMAST.Rows(i).Item("CADDRESS2").ToString
                    '氏名
                    sheet.Cells(6 + intLine, intCol) = dtCSMAST.Rows(i).Item("CCSNAME").ToString()
                    '会員番号
                    sheet.Cells(7 + intLine, intCol) = "顧客番号 " & dtCSMAST.Rows(i).Item("NCSNO").ToString().PadLeft(8, "0"c)
                    If ((i + 1) Mod 2).Equals(0) Then
                        '偶数
                        intLine += 9
                    End If

                    '1ページは12件まで
                    If ((i + 1) Mod 12).Equals(0) Then
                        If i.Equals(dtCSMAST.Rows.Count - 1) Then
                            Exit For
                        End If
                        '改ページ処理
                        break = sheet.HPageBreaks.Add(sheet.Range("A" & (intBLine + ((intPage - 1) * 53))))
                        '改ページ後は－１
                        intLine -= 1
                        'セル書式コピー
                        xlrange = sheet.Cells.Range("A1:G53")
                        xlpasterange = sheet.Range("A" & (intBLine + ((intPage - 1) * 53)))
                        xlrange.Copy(xlpasterange)
                        'コピー後の値削除
                        xlrange = sheet.Cells.Range("A" & (intBLine + ((intPage - 1) * 53)) & ":G" & (53 * (intPage + 1)))
                        xlrange.ClearContents()
                        'ページ数カウントアップ
                        intPage += 1
                    End If
                Next

                'ファイル保存
                book.SaveAs(strSaveReportName)

                book.Close()

                System.Diagnostics.Process.Start(strSaveReportName)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ' HACK ADD 2018/01/16 TERAYAMA 検索機能の追加
    ' *** STA
    ''' <summary>
    ''' クリック_検索ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim list As System.ComponentModel.BindingList(Of CSMAST_View) = New System.ComponentModel.BindingList(Of CSMAST_View)
        Me.dgvCSMAST.DataSource = list
        Try
            Dim resultDt = GetCSMAST()

            For Each row As DataRow In resultDt.Rows
                Dim data = New CSMAST_View

                ' 顧客番号
                data.NCSNO = row("NCSNO").ToString.PadLeft(8, "0"c)

                ' 顧客種別
                data.NCSRANK = row("CKBNAME").ToString

                ' 氏名
                data.CCSNAME = row("CCSNAME").ToString

                ' 氏名ｶﾅ
                data.CCSKANA = row("CCSKANA").ToString

                ' 性別
                Dim sex As Integer
                If Int32.TryParse(row("NSEX").ToString, sex) Then
                    If sex = 1 Then
                        data.NSEX = "男性"
                    ElseIf sex = 2 Then
                        data.NSEX = "女性"
                    Else
                        data.NSEX = "不明"
                    End If
                Else
                    data.NSEX = ""
                End If

                ' 郵便番号
                If Not String.IsNullOrEmpty(row("NZIP").ToString) Then
                    Dim zip = row("NZIP").ToString
                    data.NZIP = zip.Substring(0, 3) & "-" & zip.Substring(3, 4)
                End If

                ' 住所
                data.CADDRESS = row("CADDRESS1").ToString & " " & row("CADDRESS2").ToString

                ' 誕生日
                If Not String.IsNullOrEmpty(row("DBIRTH").ToString) Then
                    data.DBIRTH = DateTime.Parse(row("DBIRTH").ToString).ToString("yyyy/MM/dd")
                End If

                ' 電話番号
                data.CTELEPHONE = row("CTELEPHONE").ToString

                ' 携帯電話
                data.CPOTABLENUM = row("CPOTABLENUM").ToString

                ' 会員期限
                If Not String.IsNullOrEmpty(row("DMEMBER").ToString) Then
                    data.DMEMBER = DateTime.Parse(row("DMEMBER").ToString).ToString("yyyy/MM/dd")
                End If

                list.Add(data)
            Next

            ' 件数の表示
            lblMaxCount.Text = GetMaxCount().ToString
            lblCount.Text = resultDt.Rows.Count.ToString

            ' チェックボックスの有効化
            chkAll.Enabled = list.Count > 0

            ' ツールチップ
            For Each row As DataGridViewRow In Me.dgvCSMAST.Rows
                For Each cell As DataGridViewCell In row.Cells                    
                    cell.ToolTipText = "行を選択すると印刷対象を選択できます。"
                Next
            Next
            
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 行選択でチェックボックスON/OFF
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvCSMAST_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCSMAST.CellClick
        Try
            If e.RowIndex >= 0 Then
                If CType(Me.dgvCSMAST.Rows(e.RowIndex).Cells(0).Value, Boolean) Then
                    Me.dgvCSMAST.Rows(e.RowIndex).Cells(0).Value = False
                Else
                    Me.dgvCSMAST.Rows(e.RowIndex).Cells(0).Value = True                    
                End If
            End If

            ShowPrintMode()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' チェックボックス_全チェック/全チェック外す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Try
            If chkAll.Checked Then
                chkAll.Text = "全チェックを外す"
                lblMode.Text = "選択"
            Else
                chkAll.Text = "全チェック"
                lblMode.Text = "全件"
            End If

            For Each row As DataGridViewRow In dgvCSMAST.Rows
                row.Cells(0).Value = chkAll.Checked
            Next

            ShowPrintMode()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' *** END

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
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = False

            '顧客番号
            Me.txtStaNCSNO.Text = String.Empty
            Me.txtEndNCSNO.Text = String.Empty
            _txtNCSNO = Me.txtStaNCSNO
            '顧客種別
            Me.cmbKBMAST.SelectedIndex = -1
            '性別
            Me.cmbSEXKB.SelectedIndex = 0
            '誕生月
            Me.txtBIRTHMM.Text = String.Empty
            '住所
            Me.txtCADDRESS.Text = String.Empty
            '会員登録日
            Me.dtpStaDENTRY.Checked = False
            Me.dtpEndDENTRY.Checked = False
            '会員期限
            Me.dtpStaDMEMBER.Checked = False
            Me.dtpEndDMEMBER.Checked = False
            '前回来場日
            Me.dtpStaZENENTDATE.Checked = False
            Me.dtpEndZENENTDATE.Checked = False
            '月間来場回数
            Me.txtStaENTCNT2.Text = String.Empty
            Me.txtEndENTCNT2.Text = String.Empty

            ' HACK ADD 2018/01/16 TERAYAMA
            Me.chkAll.Enabled = False
            Dim tip As ToolTip = New ToolTip
            tip.SetToolTip(Me.chkAll, "チェックをすべて外すと検索結果が全件印刷されます。")

            ' HACK ADD 2018/01/17 TERAYAMA グリッドビュー初期化
            Me.dgvCSMAST.DataSource = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Try
            ' HACK ADD 2018/01/15 TERAYAMA スレッドの中断/再開


            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST ")
            sql.Append(" WHERE")
            sql.Append(" NKBNO <= " & UIUtility.SYSTEM.MAXNKBNO)
            sql.Append(" ORDER BY NKBNO ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count = 0 Then
                Return False
            End If

            Dim drKSBMTA As DataRow
            drKSBMTA = resultDt.NewRow
            drKSBMTA("NKBNO") = -1
            drKSBMTA("CKBNAME") = String.Empty
            resultDt.Rows.InsertAt(drKSBMTA, 0)
            Me.cmbKBMAST.DataSource = resultDt
            Me.cmbKBMAST.ValueMember = "NKBNO"
            Me.cmbKBMAST.DisplayMember = "CKBNAME"

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            ' HACK ADD 2018/01/15 TERAYAMA スレッドの中断/再開

        End Try

    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST() As DataTable
        Dim sql As New System.Text.StringBuilder

        Try
            ' HACK ADD 2018/01/15 TERAYAMA スレッドの中断/再開


            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS TO_DBIRTH")  '誕生日
            sql.Append(",TO_CHAR(A.DENTRY,'YYYY/MM/DD') AS TO_DENTRY")  '会員登録日
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYY/MM/DD') AS TO_DMEMBER") '会員期限
            sql.Append(",(A.CADDRESS1 || A.CADDRESS2) AS CADDRESS")     '住所
            sql.Append(",CKBNAME")
            sql.Append(",(CASE A.NSEX WHEN 1 THEN '男性' WHEN 2 THEN '女性' WHEN 0 THEN '不明' END) AS SEXNM")
            sql.Append(" FROM CSMAST AS A ")
            sql.Append(" LEFT JOIN KBMAST AS B ON B.NKBNO = A.NCSRANK")
            sql.Append(" WHERE 1=1")
            '顧客番号
            If (Not String.IsNullOrEmpty(Me.txtStaNCSNO.Text)) And Not (Me.txtStaNCSNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.NCSNO >=" & CType(Me.txtStaNCSNO.Text, Integer))
            End If
            If (Not String.IsNullOrEmpty(Me.txtEndNCSNO.Text)) And Not (Me.txtEndNCSNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.NCSNO <=" & CType(Me.txtEndNCSNO.Text, Integer))
            End If
            '顧客種別
            If (Not Me.cmbKBMAST.SelectedIndex.Equals(0)) And (Not Me.cmbKBMAST.SelectedIndex.Equals(-1)) Then
                sql.Append(" AND A.NCSRANK =" & Me.cmbKBMAST.SelectedIndex)
            End If
            '性別
            If Not Me.cmbSEXKB.SelectedIndex.Equals(0) Then
                sql.Append(" AND A.NSEX =" & Me.cmbSEXKB.SelectedIndex)
            End If
            '誕生月
            If Not (String.IsNullOrEmpty(Me.txtBIRTHMM.Text)) And (Not Me.txtBIRTHMM.Text.Equals("00")) Then
                sql.Append(" AND (SUBSTRING(case when TO_CHAR(A.DBIRTH,'YYYYMMDD') is null then '00000000' else TO_CHAR(A.DBIRTH,'YYYYMMDD') end,5,2) = CASE WHEN '" & Me.txtBIRTHMM.Text.PadLeft(2, "0"c) & "' <> '' THEN '" & Me.txtBIRTHMM.Text.PadLeft(2, "0"c) & "'")
                sql.Append(" ELSE SUBSTRING(case when TO_CHAR(A.DBIRTH,'YYYYMMDD') is null then '00000000' else TO_CHAR(A.DBIRTH,'YYYYMMDD') end,5,2) END) ")
            End If
            '住所
            If Not String.IsNullOrEmpty(Me.txtCADDRESS.Text) Then
                sql.Append(" AND (A.CADDRESS1 || A.CADDRESS2) LIKE '%" & Me.txtCADDRESS.Text & "%'")
            End If
            '会員登録日
            If Me.dtpStaDENTRY.Checked Then
                sql.Append(" AND TO_CHAR(A.DENTRY,'YYYY/MM/DD') >= '" & Me.dtpStaDENTRY.Text & "'")
            End If
            If Me.dtpEndDENTRY.Checked Then
                sql.Append(" AND TO_CHAR(A.DENTRY,'YYYY/MM/DD') <= '" & Me.dtpEndDENTRY.Text & "'")
            End If
            '会員期限
            If Me.dtpStaDMEMBER.Checked Then
                sql.Append(" AND TO_CHAR(A.DMEMBER,'YYYY/MM/DD') >= '" & Me.dtpStaDMEMBER.Text & "'")
            End If
            If Me.dtpEndDMEMBER.Checked Then
                sql.Append(" AND TO_CHAR(A.DMEMBER,'YYYY/MM/DD') <= '" & Me.dtpEndDMEMBER.Text & "'")
            End If
            '前回来場日
            If Me.dtpStaZENENTDATE.Checked Then
                sql.Append(" AND A.ZENENTDATE >= '" & Me.dtpStaZENENTDATE.Text.Replace("/", String.Empty) & "'")
            End If
            If Me.dtpEndZENENTDATE.Checked Then
                sql.Append(" AND A.ZENENTDATE <= '" & Me.dtpEndZENENTDATE.Text.Replace("/", String.Empty) & "'")
            End If
            '月間来場回数
            If (Not String.IsNullOrEmpty(Me.txtStaENTCNT2.Text)) And (Not Me.txtStaENTCNT2.Text.Equals("0")) Then
                sql.Append(" AND A.ENTCNT2 >= " & Me.txtStaENTCNT2.Text)
            End If
            If (Not String.IsNullOrEmpty(Me.txtEndENTCNT2.Text)) And (Not Me.txtEndENTCNT2.Text.Equals("0")) Then
                sql.Append(" AND A.ENTCNT2 <= " & Me.txtEndENTCNT2.Text)
            End If

            sql.Append(" ORDER BY NCSNO ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count = 0 Then
                Return New DataTable
            End If

            Return resultDt

        Catch ex As Exception
            Throw ex
        Finally
            ' HACK ADD 2018/01/15 TERAYAMA スレッドの中断/再開

        End Try
    End Function

    ' HACK ADD 2018/01/16 TERAYAMA 関数の追加
    ' *** STA
    ''' <summary>
    ''' 最大件数の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMaxCount() As Integer
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM CSMAST")
            Return iDatabase.ExecuteRead(sql.ToString()).Rows.Count
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 選択中の行のみ出力対象にする
    ''' </summary>
    ''' <param name="dtCSMAST"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectedTable(ByRef dtCSMAST As DataTable)
        Dim idx = 0
        Dim dt = dtCSMAST.Clone
        Try
            Me.dgvCSMAST.CurrentCell = Nothing ' 選択中のセルを解除しないとチェックボックスを検出しないバグの解消

            For Each row As DataGridViewRow In Me.dgvCSMAST.Rows
                If Not row.Cells(0).Value Is Nothing Then
                    If CType(row.Cells(0).Value, Boolean) Then
                        dt.ImportRow(dtCSMAST.Rows(idx))
                    End If
                End If
                idx += 1
            Next
            If dt.Rows.Count > 0 Then
                dtCSMAST.Rows.Clear()
                For Each row As DataRow In dt.Rows
                    dtCSMAST.ImportRow(row)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 印刷モードの描画
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowPrintMode()
        lblMode.Text = "全件"
        For Each row As DataGridViewRow In Me.dgvCSMAST.Rows
            If Not row.Cells(0).Value Is Nothing Then
                If CType(row.Cells(0).Value, Boolean) Then
                    lblMode.Text = "選択"
                End If
            End If
        Next
    End Sub

    ' *** END

#End Region


End Class
