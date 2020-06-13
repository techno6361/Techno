Imports TECHNO.DataBase
Imports Microsoft.Office.Interop
Imports TMT90

Public Class frmRECEIPT01

#Region "▼宣言部"
    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "RECEIPT01"

    ''' <summary>
    ''' スタッフコード
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSTFCODE As String = String.Empty
    ''' <summary>
    ''' スタッフ名
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSTFNAME As String = String.Empty

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "領収書印刷"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "領収書印刷"

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
    Private Sub frmRECEIPT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '伝票番号取得
            GetRCTTRN()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmRECEIPT01_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 領収書グリッド_DoubleClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvRCTTRN_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvRCTTRN.DoubleClick
        Try
            If Me.dgvRCTTRN.RowCount = 0 Then
                Exit Sub
            End If

            Dim intRow As Integer

            If Me.dgvRCTTRN.Rows.Count <> 0 Then

                intRow = Me.dgvRCTTRN.CurrentRow.Index

                Me.txtYear.Text = Now.Year.ToString
                Me.txtMonth.Text = Now.Month.ToString.PadLeft(2, "0"c)
                Me.txtDay.Text = Now.Day.ToString.PadLeft(2, "0"c)
                Me.lblDENNO.Text = Me.dgvRCTTRN.getValue("RCTNO", intRow).ToString
                Me.txtNAME.Text = Me.dgvRCTTRN.getValue("ATENA", intRow).ToString
                Me.txtKINGAKU.Text = Me.dgvRCTTRN.getValue("KINGAKU", intRow).ToString
                Me.txtTADASHI.Text = Me.dgvRCTTRN.getValue("TADASHI", intRow).ToString
            End If

            '伝票番号取得
            GetRCTTRN()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        Try
            Me.txtYear.Text = Now.Year.ToString
            Me.txtMonth.Text = Now.Month.ToString.PadLeft(2, "0"c)
            Me.txtDay.Text = Now.Day.ToString.PadLeft(2, "0"c)

            Me.txtNAME.Text = String.Empty
            Me.txtKINGAKU.Text = String.Empty
            Me.txtTADASHI.Text = String.Empty

            '伝票番号取得
            GetRCTTRN()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            '*** 【スタッフ確認】 ***'
            _strSTFCODE = String.Empty
            _strSTFNAME = String.Empty
            If UIUtility.SYSTEM.TANCHKFLG.Equals(1) And UIFunction.ChkPgScrty(c_strPRGID, iDatabase) Then
                '処理スタッフ確認

                Using frm As New frmSTFSELECT01(iDatabase)
                    frm.ShowDialog()
                    _strSTFCODE = frm.STFCODE
                    _strSTFNAME = frm.STFNAME
                End Using

                If String.IsNullOrEmpty(_strSTFCODE) Then
                    Me.Close()
                    Exit Sub
                End If
            End If
            '*** 【スタッフ確認】 ***'

            If String.IsNullOrEmpty(Me.txtNAME.Text) Then
                Using frm As New frmMSGBOX01("宛名を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtNAME.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Me.txtKINGAKU.Text) Then
                Using frm As New frmMSGBOX01("金額を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtKINGAKU.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Me.txtTADASHI.Text) Then
                Using frm As New frmMSGBOX01("但を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtTADASHI.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Me.txtYear.Text) Then
                Using frm As New frmMSGBOX01("発行日を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtYear.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Me.txtMonth.Text) Then
                Using frm As New frmMSGBOX01("発行日を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtMonth.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Me.txtDay.Text) Then
                Using frm As New frmMSGBOX01("発行日を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtDay.Focus()
                Exit Sub
            End If

            Me.txtYear.Text = Me.txtYear.Text.PadLeft(4, "0"c)
            Me.txtMonth.Text = Me.txtMonth.Text.PadLeft(2, "0"c)
            Me.txtDay.Text = Me.txtDay.Text.PadLeft(2, "0"c)

            If Not (Me.txtYear.Text >= "2000" And Me.txtYear.Text <= "2100") Then
                Using frm As New frmMSGBOX01("発行日の値が不正です。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtYear.Focus()
                Exit Sub
            End If

            If Not (Me.txtMonth.Text >= "01" And Me.txtMonth.Text <= "12") Then
                Using frm As New frmMSGBOX01("発行日の値が不正です。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtMonth.Focus()
                Exit Sub
            End If

            If Not (Me.txtDay.Text >= "01" And Me.txtDay.Text <= "31") Then
                Using frm As New frmMSGBOX01("発行日の値が不正です。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtDay.Focus()
                Exit Sub
            End If

            '更新前に再取得
            GetRCTTRN()

            '領収書トラン更新
            If Not Register() Then
                Using frm As New frmMSGBOX01("領収書トランの更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Dim strReportName As String = "領収書"
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


            sheet.Cells(6, 2) = Me.txtNAME.Text

            sheet.Cells(11, 6) = "\" & Me.txtKINGAKU.Text & "-"

            sheet.Cells(15, 3) = Me.txtTADASHI.Text

            sheet.Cells(1, 18) = Me.lblDENNO.Text

            sheet.Cells(4, 17) = Me.txtYear.Text & "年" & Me.txtMonth.Text & "月" & Me.txtDay.Text & "日"


            'ファイル保存
            book.SaveAs(strSaveReportName)

            'book.PrintOutEx()
            book.PrintOutEx(, , , , "EPSON TM-T90 Receipt")

            book.Close()

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Me.tspFunc12.Enabled = False

            Me.txtNAME.Text = String.Empty
            Me.txtKINGAKU.Text = String.Empty
            Me.txtTADASHI.Text = String.Empty
            Me.lblDENNO.Text = String.Empty

            Me.txtYear.Text = Now.Year.ToString
            Me.txtMonth.Text = Now.Month.ToString.PadLeft(2, "0"c)
            Me.txtDay.Text = Now.Day.ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 伝票番号取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRCTTRN() As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM RCTTRN")
            sql.Append(" WHERE")
            sql.Append(" RCTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" ORDER BY RCTNO DESC")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count = 0 Then
                Me.lblDENNO.Text = "0001"
            Else
                Me.lblDENNO.Text = (CType(resultDt.Rows(0).Item("RCTNO").ToString, Integer) + 1).ToString.PadLeft(4, "0"c)

                For i As Integer = 0 To resultDt.Rows.Count - 1
                    Me.dgvRCTTRN.RowCount = i + 1
                    '日付
                    Me.dgvRCTTRN.SetValue("RCTDT", i, resultDt.Rows(i).Item("RCTDT").ToString.Substring(0, 4) & "/" & resultDt.Rows(i).Item("RCTDT").ToString.Substring(4, 2) & "/" & resultDt.Rows(i).Item("RCTDT").ToString.Substring(6, 2))
                    '伝票番号
                    Me.dgvRCTTRN.SetValue("RCTNO", i, resultDt.Rows(i).Item("RCTNO").ToString.PadLeft(4, "0"c))
                    '宛名
                    Me.dgvRCTTRN.SetValue("ATENA", i, resultDt.Rows(i).Item("ATENA").ToString)
                    '金額
                    Me.dgvRCTTRN.SetValue("KINGAKU", i, CType(resultDt.Rows(i).Item("KINGAKU").ToString, Integer).ToString("#,##0"))
                    '但
                    Me.dgvRCTTRN.SetValue("TADASHI", i, resultDt.Rows(i).Item("TADASHI").ToString)
                    'スタッフ名
                    Me.dgvRCTTRN.SetValue("STFNAME", i, resultDt.Rows(i).Item("STFNAME").ToString)
                Next
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

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


            sql.Clear()
            sql.Append("INSERT INTO RCTTRN VALUES(")
            '日付
            sql.Append("'" & Now.ToString("yyyyMMdd") & "',")
            '伝票番号
            sql.Append(CType(Me.lblDENNO.Text, Integer) & ",")
            '宛名
            sql.Append("'" & Me.txtNAME.Text & "',")
            '金額
            sql.Append(CType(Me.txtKINGAKU.Text, Integer) & ",")
            '但
            sql.Append("'" & Me.txtTADASHI.Text & "',")
            'スタッフコード
            sql.Append(UIFunction.NullCheck(_strSTFCODE) & ",")
            'スタッフ名
            sql.Append(UIFunction.NullCheck(_strSTFNAME) & ",")
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
