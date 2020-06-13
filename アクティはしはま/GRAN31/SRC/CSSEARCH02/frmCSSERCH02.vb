Imports TECHNO.DataBase
Imports Microsoft.Office.Interop

Public Class frmCSSERCH02

#Region "▼宣言部"
    ''' <summary>
    ''' 顧客情報ﾃｰﾌﾞﾙ
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtCSMAST As DataTable
    ''' <summary>
    ''' ページ数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPageNo As Integer = 1
    ''' <summary>
    ''' 最終ページ数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intLastPageNo As Integer = 0
    ''' <summary>
    ''' グリッド最大行数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intMaxRowCount As Integer = 16
    ''' <summary>
    ''' 選択顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _intNCSNO As Integer = 0
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客検索画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod, ByVal ICR700 As TECHNO.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客検索画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
            dcICR700 = ICR700

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼イベント定義"

    Private Sub frmCSSERCH02_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCSSERCH02_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード未発行チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkCard_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCard.CheckedChanged
        Try
            If Me.chkCard.Checked Then
                Me.chkCard2.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード発行済みチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkCard2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCard2.CheckedChanged
        Try
            If Me.chkCard2.Checked Then
                Me.chkCard.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 対象期間の・・・・チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkInfo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkInfo.CheckedChanged
        Try
            If Me.chkInfo.Checked Then
                Me.pnlInfo.Enabled = True
            Else
                Me.pnlInfo.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 協力金番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKRKNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaKRKNO.Validated, txtEndKRKNO.Validated
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            txtBox.Text = txtBox.Text.PadLeft(4, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ハンディキャップテキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtHANDICAP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStaHANDICAP.KeyPress, txtEndHANDICAP.KeyPress
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack And Not e.KeyChar.Equals("-"c) Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCTELEPHONE.KeyPress, txtCPOTABLENUM.KeyPress, txtCFAX.KeyPress
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
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtCCSNAME.Enter, txtCCSKANA.Enter, txtCADDRESS.Enter, txtCTELEPHONE.Enter, txtCPOTABLENUM.Enter, txtCFAX.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace("/", String.Empty)
            txtBox.Text = txtBox.Text.Replace("-", String.Empty)

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
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtCCSNAME.MouseDown, txtCCSKANA.MouseDown, txtCADDRESS.MouseDown, txtCTELEPHONE.MouseDown, txtCPOTABLENUM.MouseDown, txtCFAX.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 全ﾁｪｯｸONボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChkOn_Click(sender As System.Object, e As System.EventArgs) Handles btnChkOn.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            For Each row As DataRow In _dtCSMAST.Select
                row.Item("CHK") = "1"
                row.EndEdit()
            Next

            'チェックボックス
            For Each row As DataGridViewRow In dgvCSMAST.Rows
                row.Cells(0).Value = True
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 全ﾁｪｯｸOFFボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChkOff_Click(sender As System.Object, e As System.EventArgs) Handles btnChkOff.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            For Each row As DataRow In _dtCSMAST.Select
                row.Item("CHK") = "0"
                row.EndEdit()
            Next

            'チェックボックス
            For Each row As DataGridViewRow In dgvCSMAST.Rows
                row.Cells(0).Value = False
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' CSV出力ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCSV_Click(sender As System.Object, e As System.EventArgs) Handles btnCSV.Click
        Dim dr As DataRow()
        Try
            Me.Cursor = Cursors.WaitCursor

            If UIUtility.SYSTEM.PWKBN.Substring(1, 1).Equals("1") Then
                Using frm As New frmPWDISP01
                    frm.PWKBN = 2
                    frm.ShowDialog()
                    If Not frm.CLEAR Then Exit Sub
                End Using
            End If

            dr = _dtCSMAST.Select("CHK = 1")
            If dr.Length.Equals(0) Then
                Using frm As New frmMSGBOX01("印刷対象にﾁｪｯｸを入れてください。", 3)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            'SaveFileDialogクラスのインスタンスを作成
            Dim sfd As New SaveFileDialog()

            'はじめのファイル名を指定する
            sfd.FileName = "新しいファイル.csv"
            'はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = "C:\"
            '[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = _
             "HTMLファイル(*.csv;*.csv)|*.csv;*.csv|すべてのファイル(*.*)|*.*"
            '[ファイルの種類]ではじめに
            '「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2
            'タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください"
            'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = True
            '既に存在するファイル名を指定したとき警告する
            'デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = True
            '存在しないパスが指定されたとき警告を表示する
            'デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = True

            'ダイアログを表示する
            If sfd.ShowDialog() = DialogResult.OK Then
                'OKボタンがクリックされたとき
                '選択されたファイル名を表示する
                Console.WriteLine(sfd.FileName)

                Dim csvPath As String = sfd.FileName  '保存先のCSVファイルのパス

                Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(("Shift_JIS"))
                Dim sr As New System.IO.StreamWriter(csvPath, False, enc) '開く

                sr.Write("顧客番号")
                sr.Write(","c)
                sr.Write("氏名")
                sr.Write(","c)
                sr.Write("氏名ｶﾅ")
                sr.Write(","c)
                sr.Write("顧客種別")
                sr.Write(","c)
                sr.Write("性別")
                sr.Write(","c)
                sr.Write("生年月日")
                sr.Write(","c)
                sr.Write("年齢")
                sr.Write(","c)
                sr.Write("郵便番号")
                sr.Write(","c)
                sr.Write("住所")
                sr.Write(","c)
                sr.Write("電話番号")
                sr.Write(","c)
                sr.Write("携帯電話番号")
                sr.Write(","c)
                sr.Write("緊急連絡先")
                sr.Write(","c)
                sr.Write("会員登録日")
                sr.Write(","c)
                sr.Write("会員期限")
                sr.Write(","c)
                sr.Write("前回来場日")
                sr.Write(","c)
                sr.Write("ﾒｰﾙｱﾄﾞﾚｽ")
                sr.Write(","c)
                sr.Write("配信区分")
                sr.Write(","c)
                sr.Write("利き手")
                sr.Write(","c)
                sr.Write("メッセージ")
                sr.Write(","c)
                sr.Write("備考１")
                sr.Write(","c)
                sr.Write("備考２")
                sr.Write(","c)
                sr.Write("備考３")
                sr.Write(","c)
                sr.Write("総来場回数")
                sr.Write(","c)
                sr.Write("月間来場回数")
                sr.Write(","c)
                sr.Write("ｶｰﾄﾞ有効期限")
                sr.Write(","c)
                sr.Write("残高有効期限")
                sr.Write(","c)
                sr.Write("残金額")
                sr.Write(","c)
                sr.Write("P)残金額")
                sr.Write(","c)
                sr.Write("残ポイント")
                sr.Write(","c)
                sr.Write("協力金番号")
                sr.Write(","c)
                sr.Write("ハンディキャップ")
                sr.Write(","c)
                sr.Write("来場回数")
                sr.Write(","c)
                sr.Write("使用球数")
                sr.Write(","c)
                sr.Write("入金額")

                sr.Write(vbCrLf)



                For i As Integer = 0 To _dtCSMAST.Rows.Count - 1
                    If _dtCSMAST.Rows(i).Item("CHK").ToString.Equals("0") Then Continue For

                    '顧客番号
                    sr.Write(_dtCSMAST.Rows(i).Item("NCSNO").ToString.PadLeft(8, "0"c))
                    sr.Write(","c)
                    '氏名
                    sr.Write(_dtCSMAST.Rows(i).Item("CCSNAME").ToString)
                    sr.Write(","c)
                    '氏名ｶﾅ
                    sr.Write(_dtCSMAST.Rows(i).Item("CCSKANA").ToString)
                    sr.Write(","c)
                    '顧客種別
                    sr.Write(_dtCSMAST.Rows(i).Item("CKBNAME").ToString)
                    sr.Write(","c)
                    '性別
                    Select Case CType(_dtCSMAST.Rows(i).Item("NSEX").ToString, Integer)
                        Case 1 : sr.Write("男")
                        Case 2 : sr.Write("女")
                        Case Else : sr.Write("不明")
                    End Select
                    sr.Write(","c)
                    '生年月日
                    sr.Write(_dtCSMAST.Rows(i).Item("TO_DBIRTH").ToString)
                    sr.Write(","c)
                    '年齢
                    If Not String.IsNullOrEmpty(_dtCSMAST.Rows(i).Item("TO_DBIRTH").ToString) Then
                        sr.Write(UIFunction.GetAge(CType(_dtCSMAST.Rows(i).Item("TO_DBIRTH").ToString, Date), Now))
                    Else
                        sr.Write(String.Empty)
                    End If
                    sr.Write(","c)
                    '郵便番号
                    If Not String.IsNullOrEmpty(_dtCSMAST.Rows(i).Item("NZIP").ToString) Then
                        sr.Write(_dtCSMAST.Rows(i).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(0, 3) & "-" & _dtCSMAST.Rows(i).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(3, 4))
                    Else
                        sr.Write(String.Empty)
                    End If
                    sr.Write(","c)
                    '住所
                    sr.Write(_dtCSMAST.Rows(i).Item("CADDRESS1").ToString & _dtCSMAST.Rows(i).Item("CADDRESS2").ToString)
                    sr.Write(","c)
                    '電話番号
                    sr.Write(_dtCSMAST.Rows(i).Item("CTELEPHONE").ToString)
                    sr.Write(","c)
                    '携帯電話番号
                    sr.Write(_dtCSMAST.Rows(i).Item("CPOTABLENUM").ToString)
                    sr.Write(","c)
                    '緊急連絡先
                    sr.Write(_dtCSMAST.Rows(i).Item("CFAX").ToString)
                    sr.Write(","c)
                    '会員登録日
                    sr.Write(_dtCSMAST.Rows(i).Item("TO_DENTRY").ToString)
                    sr.Write(","c)
                    '会員期限
                    sr.Write(_dtCSMAST.Rows(i).Item("TO_DMEMBER").ToString)
                    sr.Write(","c)
                    '前回来場日
                    If (Not String.IsNullOrEmpty(_dtCSMAST.Rows(i).Item("ZENENTDATE").ToString) And Not _dtCSMAST.Rows(i).Item("ZENENTDATE").ToString.Equals("00000000")) Then
                        sr.Write(_dtCSMAST.Rows(i).Item("ZENENTDATE").ToString.Substring(0, 4) & "/" _
                                              & _dtCSMAST.Rows(i).Item("ZENENTDATE").ToString.Substring(4, 2) & "/" _
                                              & _dtCSMAST.Rows(i).Item("ZENENTDATE").ToString.Substring(6, 2))
                    End If
                    sr.Write(","c)
                    'メールアドレス
                    sr.Write(_dtCSMAST.Rows(i).Item("CEMAIL").ToString)
                    sr.Write(","c)
                    'メール配信区分
                    If _dtCSMAST.Rows(i).Item("CEMAILKBN").ToString.Equals("1") Then
                        sr.Write("配信")
                    Else
                        sr.Write("不要")
                    End If
                    sr.Write(","c)
                    '利き手
                    If _dtCSMAST.Rows(i).Item("DLEFTKBN").ToString.Equals("1") Then
                        sr.Write("左")
                    Else
                        sr.Write("右")
                    End If
                    sr.Write(","c)
                    'メッセージ
                    sr.Write(_dtCSMAST.Rows(i).Item("MANCOMMENT").ToString)
                    sr.Write(","c)
                    '備考１
                    sr.Write(_dtCSMAST.Rows(i).Item("BIKO1").ToString)
                    sr.Write(","c)
                    '備考２
                    sr.Write(_dtCSMAST.Rows(i).Item("BIKO2").ToString)
                    sr.Write(","c)
                    '備考３
                    sr.Write(_dtCSMAST.Rows(i).Item("BIKO3").ToString)
                    sr.Write(","c)
                    '総来場回数
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("ENTCNT").ToString, Integer).ToString)
                    sr.Write(","c)
                    '月間来場回数
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("ENTCNT2").ToString, Integer).ToString)
                    sr.Write(","c)
                    'ｶｰﾄﾞ有効期限
                    If (Not String.IsNullOrEmpty(_dtCSMAST.Rows(i).Item("CARDLIMIT").ToString) And Not _dtCSMAST.Rows(i).Item("CARDLIMIT").ToString.Equals("00000000")) Then
                        sr.Write(_dtCSMAST.Rows(i).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" _
                                                                & _dtCSMAST.Rows(i).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" _
                                                                & _dtCSMAST.Rows(i).Item("CARDLIMIT").ToString.Substring(6, 2))
                    End If
                    sr.Write(","c)
                    '残高有効期限
                    If (Not String.IsNullOrEmpty(_dtCSMAST.Rows(i).Item("PREMLIMIT").ToString) And Not _dtCSMAST.Rows(i).Item("PREMLIMIT").ToString.Equals("00000000")) Then
                        sr.Write(_dtCSMAST.Rows(i).Item("PREMLIMIT").ToString.Substring(0, 4) & "/" _
                                                                & _dtCSMAST.Rows(i).Item("PREMLIMIT").ToString.Substring(4, 2) & "/" _
                                                                & _dtCSMAST.Rows(i).Item("PREMLIMIT").ToString.Substring(6, 2))
                    End If
                    sr.Write(","c)
                    '残金額
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("ZANKN").ToString, Integer).ToString)
                    sr.Write(","c)
                    'P)残金額
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("PREZANKN").ToString, Integer).ToString)
                    sr.Write(","c)
                    '残ポイント
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("SRTPO").ToString, Integer).ToString)
                    sr.Write(","c)
                    '協力金番号
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("KRKNO").ToString, Integer).ToString)
                    sr.Write(","c)
                    'ハンディキャップ
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("HANDICAP").ToString, Integer).ToString)
                    sr.Write(","c)
                    '来場回数
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("ENTSU").ToString, Integer).ToString)
                    sr.Write(","c)
                    '使用球数
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("USEBALL").ToString, Integer).ToString)
                    sr.Write(","c)
                    '入金額
                    sr.Write(CType(_dtCSMAST.Rows(i).Item("CHARGE").ToString, Integer).ToString)


                    sr.Write(vbCrLf)

                Next

                '閉じる
                sr.Close()
                sr.Dispose()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 顧客一覧ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnList_Click(sender As System.Object, e As System.EventArgs) Handles btnList.Click
        Dim dr As DataRow()
        Try
            Me.Cursor = Cursors.WaitCursor

            dr = _dtCSMAST.Select("CHK = 1")
            If dr.Length.Equals(0) Then
                Using frm As New frmMSGBOX01("印刷対象にﾁｪｯｸを入れてください。", 3)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

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
            sheet.Cells(4, 7) = _dtCSMAST.Rows.Count & "件"

            Dim strNCSNO As String = String.Empty
            Dim intI As Integer = 0
            For i As Integer = 0 To _dtCSMAST.Rows.Count - 1
                If _dtCSMAST.Rows(i).Item("CHK").ToString.Equals("0") Then Continue For
                '連番
                sheet.Cells(intLine + (intI * 2), 2) = (intI + 1).ToString
                '会員種別
                sheet.Cells(intLine + (intI * 2) + 1, 2) = _dtCSMAST.Rows(i).Item("CKBNAME").ToString()
                '顧客番号
                strNCSNO = _dtCSMAST.Rows(i).Item("NCSNO").ToString().PadLeft(8, "0"c)
                If Not String.IsNullOrEmpty(_dtCSMAST.Rows(i).Item("DSCLMANNO").ToString()) Then
                    strNCSNO &= "(" & _dtCSMAST.Rows(i).Item("DSCLMANNO").ToString() & ")"
                End If
                sheet.Cells(intLine + (intI * 2), 3) = strNCSNO
                'ｶﾅ
                sheet.Cells(intLine + (intI * 2), 4) = _dtCSMAST.Rows(i).Item("CCSKANA").ToString()
                '氏名
                sheet.Cells(intLine + (intI * 2) + 1, 4) = _dtCSMAST.Rows(i).Item("CCSNAME").ToString()
                '郵便番号
                sheet.Cells(intLine + (intI * 2), 6) = _dtCSMAST.Rows(i).Item("NZIP").ToString
                '住所
                sheet.Cells(intLine + (intI * 2), 7) = _dtCSMAST.Rows(i).Item("CADDRESS1").ToString
                sheet.Cells(intLine + (intI * 2) + 1, 7) = _dtCSMAST.Rows(i).Item("CADDRESS2").ToString
                '電話番号
                sheet.Cells(intLine + (intI * 2), 8) = _dtCSMAST.Rows(i).Item("CTELEPHONE").ToString
                '携帯番号
                sheet.Cells(intLine + (intI * 2) + 1, 8) = _dtCSMAST.Rows(i).Item("CPOTABLENUM").ToString
                '月間/総来場回数
                sheet.Cells(intLine + (intI * 2) + 1, 3) = _dtCSMAST.Rows(i).Item("ENTCNT2").ToString & "/" & _dtCSMAST.Rows(i).Item("ENTCNT").ToString
                '登録日
                sheet.Cells(intLine + (intI * 2), 5) = _dtCSMAST.Rows(i).Item("TO_DENTRY").ToString
                '会員番号
                sheet.Cells(intLine + (intI * 2) + 1, 5) = _dtCSMAST.Rows(i).Item("MEMBERNO").ToString
                '会員期限
                sheet.Cells(intLine + (intI * 2) + 1, 6) = _dtCSMAST.Rows(i).Item("TO_DMEMBER").ToString

                '1ページは14件まで
                If ((intI + 1) Mod 14).Equals(0) Then
                    If i.Equals(_dtCSMAST.Rows.Count - 1) Then
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
                intI += 1
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
    ''' 宛名ラベルボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLabel_Click(sender As System.Object, e As System.EventArgs) Handles btnLabel.Click
        Dim dr As DataRow()
        Try
            Me.Cursor = Cursors.WaitCursor

            dr = _dtCSMAST.Select("CHK = 1")
            If dr.Length.Equals(0) Then
                Using frm As New frmMSGBOX01("印刷対象にﾁｪｯｸを入れてください。", 3)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

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
            Dim intI As Integer = 0
            For i As Integer = 0 To _dtCSMAST.Rows.Count - 1
                If _dtCSMAST.Rows(i).Item("CHK").ToString.Equals("0") Then Continue For

                If ((intI + 1) Mod 2).Equals(0) Then
                    '偶数
                    intCol = 6
                Else
                    '奇数

                    intCol = 2
                End If
                '郵便番号
                sheet.Cells(2 + intLine, intCol) = "〒" & _dtCSMAST.Rows(i).Item("NZIP").ToString
                '住所１
                sheet.Cells(3 + intLine, intCol) = _dtCSMAST.Rows(i).Item("CADDRESS1").ToString
                '住所２
                sheet.Cells(4 + intLine, intCol) = _dtCSMAST.Rows(i).Item("CADDRESS2").ToString
                '氏名
                sheet.Cells(6 + intLine, intCol) = _dtCSMAST.Rows(i).Item("CCSNAME").ToString() & "　様"
                '会員番号
                sheet.Cells(7 + intLine, intCol) = "顧客番号 " & _dtCSMAST.Rows(i).Item("NCSNO").ToString().PadLeft(8, "0"c)
                If ((intI + 1) Mod 2).Equals(0) Then
                    '偶数
                    intLine += 9
                End If

                '1ページは12件まで
                If ((intI + 1) Mod 12).Equals(0) Then
                    If i.Equals(_dtCSMAST.Rows.Count - 1) Then
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
                intI += 1
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
    ''' 顧客情報登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCSMAST01_Click(sender As System.Object, e As System.EventArgs) Handles btnCSMAST01.Click
        Try
            Using frm As New frmCSMAST01(iDatabase, dcICR700, _intNCSNO)
                frm.ShowDialog()
            End Using
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
    ''' 会員番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtMEMBERNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaMEMBERNO.Validated, txtEndMEMBERNO.Validated
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
    ''' 年齢テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtAGE_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaAGE.Validated, txtEndAGE.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'CurrentCellDirtyStateChangedイベントハンドラ
    Private Sub dgvCSMAST_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvCSMAST.CurrentCellDirtyStateChanged
        Try
            If dgvCSMAST.CurrentCellAddress.X = 0 AndAlso dgvCSMAST.IsCurrentCellDirty Then
                'コミットする
                dgvCSMAST.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'CellValueChangedイベントハンドラ
    Private Sub dgvCSMAST_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvCSMAST.CellValueChanged
        Dim dr As DataRow()
        Try
            '列のインデックスを確認する
            If e.ColumnIndex = 0 Then
                dr = _dtCSMAST.Select("NO = " & dgvCSMAST.Rows(e.RowIndex).Cells("colNO").Value.ToString)
                If dr.Length.Equals(0) Then Exit Sub

                If CType(dgvCSMAST(e.ColumnIndex, e.RowIndex).Value, Boolean) Then
                    dr(0).Item("CHK") = 1
                Else
                    dr(0).Item("CHK") = 0
                End If
                dr(0).EndEdit()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 顧客グリッド_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvCSMAST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvCSMAST.Click
        Try
            If Me.dgvCSMAST.RowCount = 0 Then
                Exit Sub
            End If

            Dim intRow As Integer

            If Me.dgvCSMAST.Rows.Count <> 0 Then
                intRow = Me.dgvCSMAST.CurrentRow.Index

                _intNCSNO = CType(Me.dgvCSMAST.getValue("colMANNO", intRow).ToString, Integer)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' 次ページボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click

        Try
            If _intPageNo.Equals(0) Then _intPageNo = 1

            Me.dgvCSMAST.RowCount = 0



            '顧客情報グリッド表示
            SetCSMAST(_dtCSMAST.Select("NO >= " & (_intPageNo * _intMaxRowCount) + 1))

            _intPageNo += 1
            Me.btnBack.Enabled = True
            If _intPageNo.Equals(_intLastPageNo) Then
                Me.btnNext.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 前ページボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Me.dgvCSMAST.RowCount = 0
            _intPageNo -= 1

            '顧客情報グリッド表示
            SetCSMAST(_dtCSMAST.Select("NO >= " & (_intPageNo * _intMaxRowCount) - (_intMaxRowCount - 1)))

            Me.btnNext.Enabled = True
            If _intPageNo.Equals(1) Then
                Me.btnBack.Enabled = False
            End If

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
        Dim strOrder1 As String = String.Empty
        Dim strOrder2 As String = String.Empty
        Try
            Cursor = Cursors.WaitCursor

            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Clear()
            End If

            'ハンディキャップ
            Dim intNum As Integer = 0
            If Not String.IsNullOrEmpty(Me.txtStaHANDICAP.Text) Then
                If Not Integer.TryParse(Me.txtStaHANDICAP.Text, intNum) = True Then
                    Me.txtStaHANDICAP.Text = String.Empty
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndHANDICAP.Text) Then
                If Not Integer.TryParse(Me.txtEndHANDICAP.Text, intNum) = True Then
                    Me.txtEndHANDICAP.Text = String.Empty
                End If
            End If

            _intNCSNO = 0
            'CSV出力
            Me.btnCSV.Enabled = False
            '顧客一覧
            Me.btnList.Enabled = False
            '宛名ラベル
            Me.btnLabel.Enabled = False
            '総残金額
            Me.lblGokeiZANKN.Text = String.Empty
            '総P)残金額
            Me.lblGokeiPREZANKN.Text = String.Empty
            '総残ポイント
            Me.lblGokeiSRTPO.Text = String.Empty
            '総残金額(期限切れ)
            Me.lblOutGokeiZANKN.Text = String.Empty
            '総P)残金額(期限切れ)
            Me.lblOutGokeiPREZANKN.Text = String.Empty
            '総残ポイント(期限切れ)
            Me.lblOutGokeiSRTPO.Text = String.Empty
            '総残金額(残高有効期限切れ)
            Me.lblOutGokeiZANKN2.Text = String.Empty
            '検索件数
            Me.lblKensu.Text = String.Empty

            If rdoNCSNO.Checked Then strOrder1 = " A.NCSNO"
            If rdoCCSNAME.Checked Then strOrder1 = " A.CCSNAME"
            If rdoCCSKANA.Checked Then strOrder1 = " A.CCSKANA"
            If rdoAsc.Checked Then strOrder2 = " ASC"
            If rdoDesc.Checked Then strOrder2 = " DESC"

            '顧客情報取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" 0 AS CHK")
            sql.Append(",ROW_NUMBER() OVER(ORDER BY" & strOrder1 & strOrder2 & ") AS NO")
            sql.Append(",A.*")
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS TO_DBIRTH")  '誕生日
            sql.Append(",TO_CHAR(A.DENTRY,'YYYY/MM/DD') AS TO_DENTRY") '会員登録日
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYY/MM/DD') AS TO_DMEMBER") '会員期限
            If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                sql.Append(",CASE WHEN A.CARDLIMIT IS NULL THEN NULL ELSE TO_CHAR(TO_DATE(CARDLIMIT, 'YYYYMMDD') - INTERVAL '" & UIUtility.SYSTEM.PREMLIMIT & "  MONTH','YYYYMMDD') END AS PREMLIMIT")
            Else
                sql.Append(",NULL AS PREMLIMIT")
            End If
            sql.Append(",B.ZANKN")
            sql.Append(",B.PREZANKN")
            sql.Append(",C.SRTPO")
            sql.Append(",D.CKBNAME")
            sql.Append(",E.SCLKBN")

            If Me.chkInfo.Checked Then
                '期間使用球数
                sql.Append(",CASE WHEN F.USEBALL IS NULL THEN 0 ELSE F.USEBALL END AS USEBALL")
                sql.Append(",CASE WHEN F.USEKIN IS NULL THEN 0 ELSE F.USEKIN END AS USEKIN")
                '期間入金額
                sql.Append(",CASE WHEN G.NKNKN IS NULL THEN 0 ELSE G.NKNKN END AS CHARGE")
                '期間来場回数
                sql.Append(",CASE WHEN H.CNT IS NULL THEN 0 ELSE H.CNT END AS ENTSU")
            Else
                sql.Append(",0 AS USEBALL")
                sql.Append(",0 AS USEKIN")
                sql.Append(",0 AS CHARGE")
                sql.Append(",0 AS ENTSU")
            End If


            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS D ON D.NKBNO = A.NCSRANK")
            sql.Append(" LEFT JOIN MANMST AS E ON E.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")

            If Me.chkInfo.Checked Then
                '期間使用球数
                sql.Append(" LEFT JOIN (SELECT NCSNO,SUM(USEBALL) AS USEBALL,SUM(USEKIN) AS USEKIN")
                sql.Append(" FROM BALLTRN WHERE 1=1")
                If Me.dtpStaENTDT.Checked Then
                    sql.Append(" AND UDNDT >= '" & Me.dtpStaENTDT.Text.Replace("/", String.Empty) & "'")
                End If
                If Me.dtpEndENTDT.Checked Then
                    sql.Append(" AND UDNDT <= '" & Me.dtpEndENTDT.Text.Replace("/", String.Empty) & "'")
                End If

                sql.Append(" GROUP BY NCSNO) AS F ON F.NCSNO = A.NCSNO")
                '期間入金額
                sql.Append(" LEFT JOIN (")
                sql.Append(" SELECT MANNO,SUM(NKNKN) AS NKNKN FROM NKNTRN WHERE DATKB = '1' AND STSFLG = '0'")
                If Me.dtpStaENTDT.Checked Then
                    sql.Append(" AND DENDT >= '" & Me.dtpStaENTDT.Text.Replace("/", String.Empty) & "'")
                End If
                If Me.dtpEndENTDT.Checked Then
                    sql.Append(" AND DENDT <= '" & Me.dtpEndENTDT.Text.Replace("/", String.Empty) & "'")
                End If
                sql.Append(" GROUP BY MANNO) AS G ON G.MANNO =  LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
                '期間来場回数
                sql.Append(" LEFT JOIN (SELECT MANNO,COUNT(*) AS CNT FROM")
                sql.Append(" (")
                sql.Append(" SELECT DISTINCT ENTDT,MANNO FROM ENTTRA WHERE DATKB = '1'")
                sql.Append(" UNION")
                sql.Append(" SELECT DISTINCT ENTDT,MANNO FROM ENTTRB")
                sql.Append(" ) ENTTR WHERE 1=1")
                If Me.dtpStaENTDT.Checked Then
                    sql.Append(" AND ENTDT >= '" & Me.dtpStaENTDT.Text.Replace("/", String.Empty) & "'")
                End If
                If Me.dtpEndENTDT.Checked Then
                    sql.Append(" AND ENTDT <= '" & Me.dtpEndENTDT.Text.Replace("/", String.Empty) & "'")
                End If
                sql.Append(" GROUP BY MANNO ORDER BY MANNO")
                sql.Append(" ) AS H ON H.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            End If
   

            sql.Append(" WHERE 1=1")
            '顧客番号
            If (Not String.IsNullOrEmpty(Me.txtStaNCSNO.Text)) And Not (Me.txtStaNCSNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.NCSNO >=" & CType(Me.txtStaNCSNO.Text, Integer))
            End If
            If (Not String.IsNullOrEmpty(Me.txtEndNCSNO.Text)) And Not (Me.txtEndNCSNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.NCSNO <=" & CType(Me.txtEndNCSNO.Text, Integer))
            End If
            '氏名
            If chkNAMELESS.Checked Then
                sql.Append(" AND A.CCSNAME IS NULL")
            Else
                If Not String.IsNullOrEmpty(Me.txtCCSNAME.Text) Then
                    sql.Append(" AND REPLACE(REPLACE(A.CCSNAME,'　',''),' ','')  LIKE '%" & Me.txtCCSNAME.Text.Trim.Replace("　", String.Empty).Replace(" ", String.Empty) & "%'")
                End If
            End If
            If chkNAME.Checked Then
                '氏名あり
                sql.Append(" AND A.CCSNAME IS NOT NULL")
            End If
            '氏名ｶﾅ
            If Not String.IsNullOrEmpty(Me.txtCCSKANA.Text) Then
                sql.Append(" AND REPLACE(REPLACE(A.CCSKANA,'　',''),' ','')  LIKE '%" & Me.txtCCSKANA.Text.Trim.Replace("　", String.Empty).Replace(" ", String.Empty) & "%'")
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
            '年齢
            If (Not String.IsNullOrEmpty(Me.txtStaAGE.Text)) And Not (Me.txtStaAGE.Text.Equals("00")) Then
                sql.Append(" AND TRUNC(((TO_NUMBER(TO_CHAR(CURRENT_DATE,'yyyymmdd'),'99999999') - TO_NUMBER(TO_CHAR(A.DBIRTH,'yyyymmdd'),'99999999')) / 10000)) >=" & CType(Me.txtStaAGE.Text.Trim, Integer))
            End If
            If (Not String.IsNullOrEmpty(Me.txtEndAGE.Text)) And Not (Me.txtEndAGE.Text.Equals("00")) Then
                sql.Append(" AND TRUNC(((TO_NUMBER(TO_CHAR(CURRENT_DATE,'yyyymmdd'),'99999999') - TO_NUMBER(TO_CHAR(A.DBIRTH,'yyyymmdd'),'99999999')) / 10000)) <=" & CType(Me.txtEndAGE.Text.Trim, Integer))
            End If
            '住所
            If Not String.IsNullOrEmpty(Me.txtCADDRESS.Text) Then
                sql.Append(" AND (A.CADDRESS1 || A.CADDRESS2) LIKE '%" & Me.txtCADDRESS.Text & "%'")
            End If
            '電話番号
            If Not String.IsNullOrEmpty(Me.txtCTELEPHONE.Text) Then
                sql.Append(" AND REPLACE(A.CTELEPHONE,'-','') LIKE '%" & Me.txtCTELEPHONE.Text & "%'")
            End If
            '携帯電話番号
            If Not String.IsNullOrEmpty(Me.txtCPOTABLENUM.Text) Then
                sql.Append(" AND REPLACE(A.CPOTABLENUM,'-','') LIKE '%" & Me.txtCPOTABLENUM.Text & "%'")
            End If
            '緊急連絡先
            If Not String.IsNullOrEmpty(Me.txtCFAX.Text) Then
                sql.Append(" AND REPLACE(A.CFAX,'-','') LIKE '%" & Me.txtCFAX.Text & "%'")
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
            'ｶｰﾄﾞ有効期限
            If Me.dtpStaCARDLIMIT.Checked Then
                sql.Append(" AND A.CARDLIMIT >= '" & Me.dtpStaCARDLIMIT.Text.Replace("/", String.Empty) & "'")
            End If
            If Me.dtpEndCARDLIMIT.Checked Then
                sql.Append(" AND A.CARDLIMIT <= '" & Me.dtpEndCARDLIMIT.Text.Replace("/", String.Empty) & "'")
            End If
            If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                '残高有効期限
                If Me.dtpStaPREMLIMIT.Checked Then
                    sql.Append(" AND CASE WHEN A.CARDLIMIT IS NULL THEN '00000000' ELSE TO_CHAR(TO_DATE(CARDLIMIT, 'YYYYMMDD') - INTERVAL '" & UIUtility.SYSTEM.PREMLIMIT & "  MONTH','YYYYMMDD') END >= '" & Me.dtpStaPREMLIMIT.Text.Replace("/", String.Empty) & "'")
                End If
                If Me.dtpEndPREMLIMIT.Checked Then
                    sql.Append(" AND CASE WHEN A.CARDLIMIT IS NULL THEN '00000000' ELSE TO_CHAR(TO_DATE(CARDLIMIT, 'YYYYMMDD') - INTERVAL '" & UIUtility.SYSTEM.PREMLIMIT & "  MONTH','YYYYMMDD') END <= '" & Me.dtpEndPREMLIMIT.Text.Replace("/", String.Empty) & "'")
                End If
            End If
            '月間来場回数
            If Not String.IsNullOrEmpty(Me.txtStaENTCNT2.Text) Then
                If CType(Me.txtStaENTCNT2.Text, Integer) >= 0 Then
                    sql.Append(" AND A.ENTCNT2 >= " & CType(Me.txtStaENTCNT2.Text, Integer))
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndENTCNT2.Text) Then
                If CType(Me.txtEndENTCNT2.Text, Integer) >= 0 Then
                    sql.Append(" AND A.ENTCNT2 <= " & CType(Me.txtEndENTCNT2.Text, Integer))
                End If
            End If
            '総来場回数
            If Not String.IsNullOrEmpty(Me.txtStaENTCNT.Text) Then
                If CType(Me.txtStaENTCNT.Text, Integer) >= 0 Then
                    sql.Append(" AND A.ENTCNT >= " & CType(Me.txtStaENTCNT.Text, Integer))
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndENTCNT.Text) Then
                If CType(Me.txtEndENTCNT.Text, Integer) >= 0 Then
                    sql.Append(" AND A.ENTCNT <= " & CType(Me.txtEndENTCNT.Text, Integer))
                End If
            End If
            '残金額
            If Not String.IsNullOrEmpty(Me.txtStaZANKN.Text) Then
                If CType(Me.txtStaZANKN.Text, Integer) >= 0 Then
                    sql.Append(" AND B.ZANKN >= " & CType(Me.txtStaZANKN.Text, Integer))
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndZANKN.Text) Then
                If CType(Me.txtEndZANKN.Text, Integer) >= 0 Then
                    sql.Append(" AND B.ZANKN <= " & CType(Me.txtEndZANKN.Text, Integer))
                End If
            End If
            'P)残金額
            If Not String.IsNullOrEmpty(Me.txtStaPREZANKN.Text) Then
                If CType(Me.txtStaPREZANKN.Text, Integer) >= 0 Then
                    sql.Append(" AND B.PREZANKN >= " & CType(Me.txtStaPREZANKN.Text, Integer))
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndPREZANKN.Text) Then
                If CType(Me.txtEndPREZANKN.Text, Integer) >= 0 Then
                    sql.Append(" AND B.PREZANKN <= " & CType(Me.txtEndPREZANKN.Text, Integer))
                End If
            End If
            '残ﾎﾟｲﾝﾄ
            If Not String.IsNullOrEmpty(Me.txtStaSRTPO.Text) Then
                If CType(Me.txtStaSRTPO.Text, Integer) >= 0 Then
                    sql.Append(" AND C.SRTPO >= " & CType(Me.txtStaSRTPO.Text, Integer))
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndSRTPO.Text) Then
                If CType(Me.txtEndSRTPO.Text, Integer) >= 0 Then
                    sql.Append(" AND C.SRTPO <= " & CType(Me.txtEndSRTPO.Text, Integer))
                End If
            End If
            '協力金番号
            If Not String.IsNullOrEmpty(Me.txtStaKRKNO.Text) Then
                If CType(Me.txtStaKRKNO.Text, Integer) >= 0 Then
                    sql.Append(" AND A.KRKNO >= " & CType(Me.txtStaKRKNO.Text, Integer))
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtEndKRKNO.Text) Then
                If CType(Me.txtEndKRKNO.Text, Integer) >= 0 Then
                    sql.Append(" AND A.KRKNO <= " & CType(Me.txtEndKRKNO.Text, Integer))
                End If
            End If
            'ハンディキャップ
            If Not String.IsNullOrEmpty(Me.txtStaHANDICAP.Text) Then
                sql.Append(" AND A.HANDICAP >= " & CType(Me.txtStaHANDICAP.Text, Integer))
            End If
            If Not String.IsNullOrEmpty(Me.txtEndHANDICAP.Text) Then
                sql.Append(" AND A.HANDICAP <= " & CType(Me.txtEndHANDICAP.Text, Integer))
            End If

            'カード未発行
            If Me.chkCard.Checked Then
                sql.Append(" AND (A.NCARDID IS NULL OR A.NCARDID = 0)")
            End If
            'カード発行済み
            If Me.chkCard2.Checked Then
                sql.Append(" AND NOT (A.NCARDID IS NULL OR A.NCARDID = 0)")
            End If


            'ｽｸｰﾙ状態
            If Me.cmbSCLKBN.SelectedIndex > 0 Then
                Select Case Me.cmbSCLKBN.SelectedIndex
                    Case 1 : sql.Append(" AND E.SCLKBN IS NULL")
                    Case 2 : sql.Append(" AND E.SCLKBN = '0'")
                    Case 3 : sql.Append(" AND E.SCLKBN = '1'")
                    Case 4 : sql.Append(" AND E.SCLKBN = '8'")
                    Case 5 : sql.Append(" AND E.SCLKBN = '9'")
                End Select
            End If
            '備考入力有り
            If Me.chkBIKO.Checked Then
                sql.Append(" AND ((A.BIKO1 IS NOT NULL OR A.BIKO2 IS NOT NULL OR A.BIKO3 IS NOT NULL) AND (A.BIKO1 <> '' OR A.BIKO2 <> '' OR A.BIKO3 <> ''))")
                'sql.Append(" AND ((A.BIKO1 || A.BIKO2 || A.BIKO3) IS NOT NULL AND ((A.BIKO1 || A.BIKO2 || A.BIKO3) <> ''))")
            End If

            '会員番号
            If (Not String.IsNullOrEmpty(Me.txtStaMEMBERNO.Text)) And Not (Me.txtStaMEMBERNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.MEMBERNO >= '" & Me.txtStaMEMBERNO.Text.PadLeft(8, "0"c) & "'")
            End If
            If (Not String.IsNullOrEmpty(Me.txtEndMEMBERNO.Text)) And Not (Me.txtEndMEMBERNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.MEMBERNO <= '" & Me.txtEndMEMBERNO.Text.PadLeft(8, "0"c) & "'")
            End If

            sql.Append(" ORDER BY" & strOrder1 & strOrder2)



            _dtCSMAST = iDatabase.ExecuteRead(sql.ToString())

            If _dtCSMAST.Rows.Count.Equals(0) Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            Me.btnCSV.Enabled = True
            Me.btnList.Enabled = True
            Me.btnLabel.Enabled = True

            Me.lblKensu.Text = "検索件数　" & _dtCSMAST.Rows.Count.ToString("#,##0") & "件"

            _intPageNo = 1

            '最終ページ数計算
            Dim dblLastPageNo As Double = _dtCSMAST.Rows.Count / _intMaxRowCount
            _intLastPageNo = CType(Math.Ceiling(dblLastPageNo), Integer)

            Me.btnBack.Enabled = False
            Me.btnNext.Enabled = False

            If _intLastPageNo > 1 Then
                Me.btnNext.Enabled = True
            End If


            Me.dgvCSMAST.RowCount = 0

            SetCSMAST(_dtCSMAST.Select)

            Me.pnlSearch.Visible = False
            Me.pnlGrid.Visible = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 検索条件ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnJoken_Click(sender As System.Object, e As System.EventArgs) Handles btnJoken.Click
        Try
            Me.pnlGrid.Visible = False
            Me.pnlSearch.Visible = True

            Me.btnLabel.Enabled = False
            Me.btnList.Enabled = False
            Me.btnCSV.Enabled = False
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
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = False

            '検索パネル
            Me.pnlSearch.Visible = True
            'グリッドパネル
            Me.pnlGrid.Visible = False

            'CSV出力
            Me.btnCSV.Enabled = False
            '顧客一覧
            Me.btnList.Enabled = False
            '宛名ラベル
            Me.btnLabel.Enabled = False

            '顧客番号
            Me.txtStaNCSNO.Text = String.Empty
            Me.txtEndNCSNO.Text = String.Empty
            '氏名
            Me.txtCCSNAME.Text = String.Empty
            '氏名ｶﾅ
            Me.txtCCSKANA.Text = String.Empty
            '顧客種別
            Me.cmbKBMAST.SelectedIndex = -1
            '性別
            Me.cmbSEXKB.SelectedIndex = 0
            '誕生月
            Me.txtBIRTHMM.Text = String.Empty
            '年齢
            Me.txtStaAGE.Text = String.Empty
            Me.txtEndAGE.Text = String.Empty
            '住所
            Me.txtCADDRESS.Text = String.Empty
            '電話番号
            Me.txtCTELEPHONE.Text = String.Empty
            'FAX番号
            Me.txtCFAX.Text = String.Empty
            '携帯電話番号
            Me.txtCPOTABLENUM.Text = String.Empty
            '会員登録日
            Me.dtpStaDENTRY.Value = Now
            Me.dtpEndDENTRY.Value = Now
            Me.dtpStaDENTRY.Checked = False
            Me.dtpEndDENTRY.Checked = False
            '会員期限
            Me.dtpStaDMEMBER.Value = Now
            Me.dtpEndDMEMBER.Value = Now
            Me.dtpStaDMEMBER.Checked = False
            Me.dtpEndDMEMBER.Checked = False
            '前回来場日
            Me.dtpStaZENENTDATE.Value = Now
            Me.dtpEndZENENTDATE.Value = Now
            Me.dtpStaZENENTDATE.Checked = False
            Me.dtpEndZENENTDATE.Checked = False
            'ｶｰﾄﾞ有効期限
            Me.dtpStaCARDLIMIT.Value = Now
            Me.dtpEndCARDLIMIT.Value = Now
            Me.dtpStaCARDLIMIT.Checked = False
            Me.dtpEndCARDLIMIT.Checked = False
            '残高有効期限
            Me.dtpStaPREMLIMIT.Value = Now
            Me.dtpEndPREMLIMIT.Value = Now
            Me.dtpStaPREMLIMIT.Checked = False
            Me.dtpEndPREMLIMIT.Checked = False
            '月間来場回数
            Me.txtStaENTCNT2.Text = String.Empty
            Me.txtEndENTCNT2.Text = String.Empty
            '総来場回数
            Me.txtStaENTCNT.Text = String.Empty
            Me.txtEndENTCNT.Text = String.Empty
            '残金額
            Me.txtStaZANKN.Text = String.Empty
            Me.txtEndZANKN.Text = String.Empty
            'P)残金額
            Me.txtStaPREZANKN.Text = String.Empty
            Me.txtEndPREZANKN.Text = String.Empty
            '残ポイント
            Me.txtStaSRTPO.Text = String.Empty
            Me.txtEndSRTPO.Text = String.Empty
            '備考入力有り
            Me.chkBIKO.Checked = False
            'ｽｸｰﾙ状態
            Me.cmbSCLKBN.SelectedIndex = 0
            '会員番号
            Me.txtStaMEMBERNO.Text = String.Empty
            Me.txtEndMEMBERNO.Text = String.Empty
            '氏名無記名
            Me.chkNAMELESS.Checked = False
            '氏名あり
            Me.chkNAME.Checked = False
            'カード未発行
            Me.chkCard.Checked = False
            'カード発行済み
            Me.chkCard2.Checked = False
            '協力金番号
            Me.txtStaKRKNO.Text = String.Empty
            Me.txtEndKRKNO.Text = String.Empty
            'ハンディキャップ
            Me.txtStaHANDICAP.Text = String.Empty
            Me.txtEndHANDICAP.Text = String.Empty
            '対象期間
            Me.dtpStaENTDT.Value = Now
            Me.dtpEndENTDT.Value = Now
            Me.dtpStaENTDT.Checked = False
            Me.dtpEndENTDT.Checked = False

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

        End Try

    End Function

    ''' <summary>
    ''' 顧客情報グリッド表示
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub SetCSMAST(ByVal dr As DataRow())
        Try
            For i As Integer = 0 To dr.Length - 1
                If i.Equals(0) Then _intNCSNO = CType(dr(i).Item("NCSNO").ToString, Integer)
                Me.dgvCSMAST.RowCount += 1
                '№
                Me.dgvCSMAST.SetValue("colNO", i, dr(i).Item("NO").ToString)
                '顧客番号
                Me.dgvCSMAST.SetValue("colMANNO", i, dr(i).Item("NCSNO").ToString.PadLeft(8, "0"c))
                '氏名
                Me.dgvCSMAST.SetValue("colCCSNAME", i, dr(i).Item("CCSNAME").ToString)
                '氏名ｶﾅ
                Me.dgvCSMAST.SetValue("colCCSKANA", i, dr(i).Item("CCSKANA").ToString)
                '顧客種別
                Me.dgvCSMAST.SetValue("colCKBNAME", i, dr(i).Item("CKBNAME").ToString)
                '性別
                Select Case CType(dr(i).Item("NSEX").ToString, Integer)
                    Case 1 : Me.dgvCSMAST.SetValue("colNSEX", i, "男")
                    Case 2 : Me.dgvCSMAST.SetValue("colNSEX", i, "女")
                    Case Else : Me.dgvCSMAST.SetValue("colNSEX", i, "不明")
                End Select
                '生年月日
                Me.dgvCSMAST.SetValue("colDBIRTH", i, dr(i).Item("TO_DBIRTH").ToString)
                '年齢
                If Not String.IsNullOrEmpty(dr(i).Item("TO_DBIRTH").ToString) Then
                    Me.dgvCSMAST.SetValue("colAGE", i, UIFunction.GetAge(CType(dr(i).Item("TO_DBIRTH").ToString, Date), Now))
                End If
                '郵便番号
                If Not String.IsNullOrEmpty(dr(i).Item("NZIP").ToString) Then
                    Me.dgvCSMAST.SetValue("colNZIP", i, dr(i).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(0, 3) & "-" & dr(i).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(3, 4))
                End If
                '住所
                Me.dgvCSMAST.SetValue("colCADDRESS", i, dr(i).Item("CADDRESS1").ToString & dr(i).Item("CADDRESS2").ToString)
                '電話番号
                Me.dgvCSMAST.SetValue("colCTELEPHONE", i, dr(i).Item("CTELEPHONE").ToString)
                'FAX番号
                Me.dgvCSMAST.SetValue("colCFAX", i, dr(i).Item("CFAX").ToString)
                '携帯電話番号
                Me.dgvCSMAST.SetValue("colCPOTABLENUM", i, dr(i).Item("CPOTABLENUM").ToString)
                '会員登録日
                Me.dgvCSMAST.SetValue("colDMEMBER", i, dr(i).Item("TO_DMEMBER").ToString)
                '会員期限
                Me.dgvCSMAST.SetValue("colDENTRY", i, dr(i).Item("TO_DENTRY").ToString)
                '前回来場日
                If (Not String.IsNullOrEmpty(dr(i).Item("ZENENTDATE").ToString) And Not dr(i).Item("ZENENTDATE").ToString.Equals("00000000")) Then
                    Me.dgvCSMAST.SetValue("colZENENTDATE", i, dr(i).Item("ZENENTDATE").ToString.Substring(0, 4) & "/" _
                                          & dr(i).Item("ZENENTDATE").ToString.Substring(4, 2) & "/" _
                                          & dr(i).Item("ZENENTDATE").ToString.Substring(6, 2))
                End If
                'メールアドレス
                Me.dgvCSMAST.SetValue("colCEMAIL", i, dr(i).Item("CEMAIL").ToString)
                'メール配信区分
                If dr(i).Item("CEMAILKBN").ToString.Equals("1") Then
                    Me.dgvCSMAST.SetValue("colCEMAILKBN", i, "配信")
                Else
                    Me.dgvCSMAST.SetValue("colCEMAILKBN", i, "不要")
                End If

                '利き手
                If dr(i).Item("DLEFTKBN").ToString.Equals("1") Then
                    Me.dgvCSMAST.SetValue("colDLEFTKBN", i, "左")
                Else
                    Me.dgvCSMAST.SetValue("colDLEFTKBN", i, "右")
                End If
                'ﾒｯｾｰｼﾞ
                Me.dgvCSMAST.SetValue("colMANCOMMENT", i, dr(i).Item("MANCOMMENT").ToString)
                'ｽｸｰﾙ生番号
                Me.dgvCSMAST.SetValue("colDSCLMANNO", i, dr(i).Item("DSCLMANNO").ToString)
                '状態
                If Not String.IsNullOrEmpty(dr(i).Item("SCLKBN").ToString) Then
                    Select Case CType(dr(i).Item("SCLKBN").ToString, Integer)
                        Case 0 : Me.dgvCSMAST.SetValue("colSCLKBN", i, "本科生")
                        Case 1 : Me.dgvCSMAST.SetValue("colSCLKBN", i, "体験")
                        Case 8 : Me.dgvCSMAST.SetValue("colSCLKBN", i, "休会中")
                        Case 9 : Me.dgvCSMAST.SetValue("colSCLKBN", i, "退会")
                    End Select
                End If
                '備考１
                Me.dgvCSMAST.SetValue("colBIKO1", i, dr(i).Item("BIKO1").ToString)
                '備考２
                Me.dgvCSMAST.SetValue("colBIKO2", i, dr(i).Item("BIKO2").ToString)
                '備考３
                Me.dgvCSMAST.SetValue("colBIKO3", i, dr(i).Item("BIKO3").ToString)
                '総来場回数
                Me.dgvCSMAST.SetValue("colENTCNT", i, CType(dr(i).Item("ENTCNT").ToString, Integer))
                '月間来場回数
                Me.dgvCSMAST.SetValue("colENTCNT2", i, CType(dr(i).Item("ENTCNT2").ToString, Integer))
                'ｶｰﾄﾞ有効期限
                If (Not String.IsNullOrEmpty(dr(i).Item("CARDLIMIT").ToString) And Not dr(i).Item("CARDLIMIT").ToString.Equals("00000000")) Then
                    Me.dgvCSMAST.SetValue("colCARDLIMIT", i, dr(i).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" _
                                                            & dr(i).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" _
                                                            & dr(i).Item("CARDLIMIT").ToString.Substring(6, 2))
                End If
                '残高有効期限
                If (Not String.IsNullOrEmpty(dr(i).Item("PREMLIMIT").ToString) And Not dr(i).Item("PREMLIMIT").ToString.Equals("00000000")) Then
                    Me.dgvCSMAST.SetValue("colPREMLIMIT", i, dr(i).Item("PREMLIMIT").ToString.Substring(0, 4) & "/" _
                                                            & dr(i).Item("PREMLIMIT").ToString.Substring(4, 2) & "/" _
                                                            & dr(i).Item("PREMLIMIT").ToString.Substring(6, 2))
                End If
                '残金額
                Me.dgvCSMAST.SetValue("colZANKN", i, CType(dr(i).Item("ZANKN").ToString, Integer).ToString("#,##0"))
                'P)残金額
                Me.dgvCSMAST.SetValue("colPREZANKN", i, CType(dr(i).Item("PREZANKN").ToString, Integer).ToString("#,##0"))
                '残ﾎﾟｲﾝﾄ
                Me.dgvCSMAST.SetValue("colSRTPO", i, CType(dr(i).Item("SRTPO").ToString, Integer).ToString("#,##0"))
                '会員番号
                Me.dgvCSMAST.SetValue("colMEMBERNO", i, dr(i).Item("MEMBERNO").ToString)
                '協力金番号
                Me.dgvCSMAST.SetValue("colKRKNO", i, dr(i).Item("KRKNO").ToString.PadLeft(4, "0"c))
                'ハンディキャップ
                Me.dgvCSMAST.SetValue("colHANDICAP", i, dr(i).Item("HANDICAP").ToString)
                '来場回数
                Me.dgvCSMAST.SetValue("colENTSU", i, dr(i).Item("ENTSU").ToString)
                '使用球数
                Me.dgvCSMAST.SetValue("colUSEBALL", i, dr(i).Item("USEBALL").ToString)
                '入金額
                Me.dgvCSMAST.SetValue("colCHARGE", i, dr(i).Item("CHARGE").ToString)

                If i.Equals(_intMaxRowCount - 1) Then
                    Exit For
                End If
            Next

            'チェックボックス
            For Each row As DataGridViewRow In dgvCSMAST.Rows
                dr = _dtCSMAST.Select("NO = " & row.Cells("colNO").Value.ToString)
                If dr.Length.Equals(0) Then
                    Continue For
                End If
                If dr(0).Item("CHK").ToString.Equals("0") Then
                    row.Cells(0).Value = False
                Else
                    row.Cells(0).Value = True
                End If

            Next

            Dim strZANKN As String = _dtCSMAST.Compute("SUM(ZANKN)", "NO > 0").ToString
            Dim strPREZANKN As String = _dtCSMAST.Compute("SUM(PREZANKN)", "NO > 0").ToString
            Dim strSRTPO As String = _dtCSMAST.Compute("SUM(SRTPO)", "NO > 0").ToString

            Me.lblGokeiZANKN.Text = "0"
            If Not String.IsNullOrEmpty(strZANKN) Then
                Me.lblGokeiZANKN.Text = CType(strZANKN, Integer).ToString("#,##0")
            End If
            Me.lblGokeiPREZANKN.Text = "0"
            If Not String.IsNullOrEmpty(strPREZANKN) Then
                Me.lblGokeiPREZANKN.Text = CType(strPREZANKN, Integer).ToString("#,##0")
            End If
            Me.lblGokeiSRTPO.Text = "0"
            If Not String.IsNullOrEmpty(strSRTPO) Then
                Me.lblGokeiSRTPO.Text = CType(strSRTPO, Integer).ToString("#,##0")
            End If

            Dim strOutZANKN As String = _dtCSMAST.Compute("SUM(ZANKN)", "CARDLIMIT < '" & Now.ToString("yyyyMMdd") & "' AND (CARDLIMIT IS NOT NULL AND CARDLIMIT <> '00000000')").ToString
            Dim strOutPREZANKN As String = _dtCSMAST.Compute("SUM(PREZANKN)", "CARDLIMIT < '" & Now.ToString("yyyyMMdd") & "' AND (CARDLIMIT IS NOT NULL AND CARDLIMIT <> '00000000')").ToString
            Dim strOutSRTPO As String = _dtCSMAST.Compute("SUM(SRTPO)", "CARDLIMIT < '" & Now.ToString("yyyyMMdd") & "' AND (CARDLIMIT IS NOT NULL AND CARDLIMIT <> '00000000')").ToString

            Me.lblOutGokeiZANKN.Text = "0"
            If Not String.IsNullOrEmpty(strOutZANKN) Then
                Me.lblOutGokeiZANKN.Text = CType(strOutZANKN, Integer).ToString("#,##0")
            End If
            Me.lblOutGokeiPREZANKN.Text = "0"
            If Not String.IsNullOrEmpty(strOutPREZANKN) Then
                Me.lblOutGokeiPREZANKN.Text = CType(strOutPREZANKN, Integer).ToString("#,##0")
            End If
            Me.lblOutGokeiSRTPO.Text = "0"
            If Not String.IsNullOrEmpty(strOutSRTPO) Then
                Me.lblOutGokeiSRTPO.Text = CType(strOutSRTPO, Integer).ToString("#,##0")
            End If

            If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                Dim strOutZANKN2 As String = _dtCSMAST.Compute("SUM(ZANKN)", "PREMLIMIT < '" & Now.ToString("yyyyMMdd") & "' AND (PREMLIMIT IS NOT NULL AND PREMLIMIT <> '00000000')").ToString
                If Not String.IsNullOrEmpty(strOutZANKN2) Then
                    Me.lblOutGokeiZANKN2.Text = CType(strOutZANKN2, Integer).ToString("#,##0")
                End If
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region




 







End Class
