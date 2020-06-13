Imports Techno.DataBase
Imports Microsoft.Office.Interop

Public Class frmPRINT07

#Region "▼宣言部"

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "推移表"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "推移表"

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


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Dim dtTable As DataTable
        Try
            Me.Cursor = Cursors.WaitCursor

            If String.IsNullOrEmpty(Me.txtYear1.Text) And String.IsNullOrEmpty(Me.txtYear2.Text) And String.IsNullOrEmpty(Me.txtYear3.Text) And String.IsNullOrEmpty(Me.txtYear4.Text) And String.IsNullOrEmpty(Me.txtYear5.Text) Then
                Using frm As New frmMSGBOX01("対象年を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtYear1.Focus()
                Exit Sub
            End If


            Dim strReportName As String = "推移表"
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

            'タイトル
            If Me.rdoRepo01.Checked Then
                sheet.Cells(1, 1) = Me.rdoRepo01.Text & "推移表"
            ElseIf Me.rdoRepo02.Checked Then
                sheet.Cells(1, 1) = Me.rdoRepo02.Text & "推移表"
            ElseIf Me.rdoRepo03.Checked Then
                sheet.Cells(1, 1) = Me.rdoRepo03.Text & "推移表"
            ElseIf Me.rdoRepo05.Checked Then
                sheet.Cells(1, 1) = Me.rdoRepo05.Text & "推移表"
            ElseIf Me.rdoRepo06.Checked Then
                sheet.Cells(1, 1) = Me.rdoRepo06.Text & "推移表"
            ElseIf Me.rdoRepo07.Checked Then
                sheet.Cells(1, 1) = Me.rdoRepo07.Text & "推移表"
            End If



            Dim strYear As String = String.Empty
            For i As Integer = 1 To 5
                strYear = String.Empty
                If i.Equals(1) And Not String.IsNullOrEmpty(Me.txtYear1.Text) Then
                    strYear = Me.txtYear1.Text
                End If
                If i.Equals(2) And Not String.IsNullOrEmpty(Me.txtYear2.Text) Then
                    strYear = Me.txtYear2.Text
                End If
                If i.Equals(3) And Not String.IsNullOrEmpty(Me.txtYear3.Text) Then
                    strYear = Me.txtYear3.Text
                End If
                If i.Equals(4) And Not String.IsNullOrEmpty(Me.txtYear4.Text) Then
                    strYear = Me.txtYear4.Text
                End If
                If i.Equals(5) And Not String.IsNullOrEmpty(Me.txtYear5.Text) Then
                    strYear = Me.txtYear5.Text
                End If
                If String.IsNullOrEmpty(strYear) Then Continue For

                sheet.Cells(4 + i, 2) = strYear & "年"

                For j As Integer = 1 To 12
                    dtTable = GetTable(strYear, j.ToString.PadLeft(2, "0"c))

                    sheet.Cells(4 + i, 2 + j) = dtTable.Rows(0).Item("VALUE").ToString
                Next

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
    ''' テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtYear1.KeyPress, txtYear2.KeyPress, txtYear3.KeyPress, txtYear4.KeyPress, txtYear5.KeyPress
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
    ''' テキストボックス_Enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtYear1.Enter, txtYear2.Enter, txtYear3.Enter, txtYear4.Enter, txtYear5.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_MouseDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtYear1.MouseDown, txtYear2.MouseDown, txtYear3.MouseDown, txtYear4.MouseDown, txtYear5.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

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

            '年
            Me.txtYear1.Text = Now.Year.ToString
            Me.txtYear2.Text = String.Empty
            Me.txtYear3.Text = String.Empty
            Me.txtYear4.Text = String.Empty
            Me.txtYear5.Text = String.Empty


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTable(ByVal strYear As String, ByVal strMonth As String) As DataTable
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()

            If Me.rdoRepo01.Checked Then
                '【1球貸し打球数】
                sql.Append(" SELECT ")
                sql.Append(" CASE WHEN")
                sql.Append(" SUM(NKB01BALL) + SUM(NKB02BALL) + SUM(NKB03BALL) + SUM(NKB04BALL) + SUM(NKB05BALL) + SUM(NKB06BALL) + SUM(NKB07BALL) + SUM(NKB08BALL) + SUM(NKB09BALL) + SUM(NKB10BALL)")
                sql.Append(" IS NULL THEN 0 ELSE ")
                sql.Append(" SUM(NKB01BALL) + SUM(NKB02BALL) + SUM(NKB03BALL) + SUM(NKB04BALL) + SUM(NKB05BALL) + SUM(NKB06BALL) + SUM(NKB07BALL) + SUM(NKB08BALL) + SUM(NKB09BALL) + SUM(NKB10BALL)")
                sql.Append(" END AS VALUE")
                sql.Append(" FROM SEATSMA ")
                sql.Append(" WHERE")
                sql.Append(" SEATDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND SEATDT <= '" & strYear & strMonth & "31'")
            ElseIf Me.rdoRepo02.Checked Then
                '【1球貸し球数売上】
                sql.Append(" SELECT ")
                sql.Append(" CASE WHEN")
                sql.Append(" SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN) + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN)")
                sql.Append(" + SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN) + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN)")
                sql.Append(" IS NULL THEN 0 ELSE ")
                sql.Append(" SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN) + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN)")
                sql.Append(" + SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN) + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN)")
                sql.Append(" END AS VALUE")
                sql.Append(" FROM SEATSMA ")
                sql.Append(" WHERE")
                sql.Append(" SEATDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND SEATDT <= '" & strYear & strMonth & "31'")
            ElseIf Me.rdoRepo03.Checked Then
                '【打ち放題売上】
                sql.Append(" SELECT ")
                sql.Append(" CASE WHEN SUM(ENTBKN) + SUM(ENTZEI) IS NULL THEN 0 ELSE SUM(ENTBKN) + SUM(ENTZEI) END AS VALUE")
                sql.Append(" FROM ENTTRA")
                sql.Append(" WHERE")
                sql.Append(" DATKB = '1'")
                sql.Append(" AND EIGKB = '2'")
                sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            ElseIf Me.rdoRepo05.Checked Then
                '【入場者数】
                sql.Append(" SELECT COUNT(MANNO) AS VALUE FROM (")

                sql.Append(" SELECT ")
                sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
                sql.Append(" FROM ENTTRB ")
                sql.Append(" WHERE")
                sql.Append(" ENTDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")

                sql.Append(" UNION ")

                sql.Append(" SELECT ")
                sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
                sql.Append(" FROM ENTTRA ")
                sql.Append(" WHERE")
                sql.Append(" DATKB = '1'")
                sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
                sql.Append(" AND (EIGKB = '2' OR (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10' OR KSBKB = '11' OR KSBKB = '12' OR KSBKB = '13')))")
                sql.Append(") AS A")

                'sql.Append(" SELECT ")
                'sql.Append(" COUNT(MANNO) AS VALUE")
                'sql.Append(" FROM ENTTRA ")
                'sql.Append(" WHERE")
                'sql.Append(" DATKB = '1'")
                'sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
                'sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
                'sql.Append(" AND (EIGKB = '2' OR (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10' OR KSBKB = '11' OR KSBKB = '12' OR KSBKB = '13')))")
            ElseIf Me.rdoRepo06.Checked Then
                '【1球貸し利用者数】

                sql.Append(" SELECT COUNT(MANNO) AS VALUE FROM (")

                sql.Append(" SELECT ")
                sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
                sql.Append(" FROM ENTTRB ")
                sql.Append(" WHERE")
                sql.Append(" ENTDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")

                sql.Append(" UNION ")

                sql.Append(" SELECT ")
                sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
                sql.Append(" FROM ENTTRA ")
                sql.Append(" WHERE")
                sql.Append(" DATKB = '1'")
                sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
                sql.Append(" AND (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10'))")
                sql.Append(") AS A")

                'sql.Append(" SELECT ")
                'sql.Append(" COUNT(MANNO) AS VALUE")
                'sql.Append(" FROM ENTTRA ")
                'sql.Append(" WHERE")
                'sql.Append(" DATKB = '1'")
                'sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
                'sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
                'sql.Append(" AND (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10'))")
            ElseIf Me.rdoRepo07.Checked Then
                '【打ち放題利用者数】
                sql.Append(" SELECT ")
                sql.Append(" COUNT(MANNO) AS VALUE")
                sql.Append(" FROM ENTTRA ")
                sql.Append(" WHERE")
                sql.Append(" DATKB = '1'")
                sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
                sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
                sql.Append(" AND EIGKB = '2'")
            End If

            Return iDatabase.ExecuteRead(sql.ToString())

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
