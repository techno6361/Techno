Imports TECHNO.DataBase
Imports System.ComponentModel
Imports TMT90

Public Class frmDRA01

#Region "テスト"

    Private Function GetHostName() As String
        'Return "TS-PC"
        Return My.Computer.Name
    End Function

#End Region

#Region "▼宣言部"

    ' DRATRNの新規/更新フラグ
    Private _updateFlg As Boolean

    ' 更新日付
    Private _dtUPDDTM As DateTime

    ' 自動計算の有効/無効
    Private _enableCalc As Boolean

    ' 初回起動フラグ
    Private _blnInited As Boolean

    ' 売り上げ情報
    Dim _listDRATRN As BindingList(Of URIAGE_View) = New BindingList(Of URIAGE_View)

    ' DRATRN挿入モード
    Enum INSERT_DRATRN_MODE
        FIRST
        CHECK_OK
        CHECK_NG
    End Enum

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ドロア管理"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ドロア管理"

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
    Private Sub frmDRA01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' 初回起動開始
            _blnInited = False
            ' 開始処理
            Start()
            ' 本日の日付をカレンダーに反映
            dtpDate.Value = Now
            dtpDate.MaxDate = Now
            ' すべて読み込み
            ReadAll()
            ' グリッドビューの不具合があるので再実行
            GetURIAGE()
            ' 初回起動終了
            _blnInited = True
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
            Start()
            ReadAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ドロアオープンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOpenDwa_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenDwa.Click
        Dim rePrint As New Receipt
        Try
            Me.btnOpenDwa.Enabled = False

            Dim strErrMsg2 As String = String.Empty
            Dim isOpen As Boolean = True

            Application.DoEvents()

            rePrint.OpenDrawer(isOpen, strErrMsg2)

            Application.DoEvents()



        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.btnOpenDwa.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_一つでも有効な値ならレジ金登録ボタンを有効化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txts_L_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        txt10000L.TextChanged, txt5000L.TextChanged, txt2000L.TextChanged, txt1000L.TextChanged, _
        txt500L.TextChanged, txt100L.TextChanged, txt50L.TextChanged, txt10L.TextChanged, _
        txt5L.TextChanged, txt1L.TextChanged
        Try
            CalcLeft()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_一つでも有効な値ならレジ金登録ボタンを有効化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txts_R_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        txt10000R.TextChanged, txt5000R.TextChanged, txt2000R.TextChanged, txt1000R.TextChanged, _
        txt500R.TextChanged, txt100R.TextChanged, txt50R.TextChanged, txt10R.TextChanged, _
        txt5R.TextChanged, txt1R.TextChanged
        Try
            CalcRight()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボタン_レジ金登録ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Try
            ' 更新時のみメッセージ表示
            If _updateFlg Then
                Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                    frm.ShowDialog()
                    If Not frm.Reply Then
                        Exit Sub
                    End If
                End Using
            End If

            Cursor = Cursors.WaitCursor

            ' 他端末からの更新チェック
            If IsDBUpdated() Then
                Exit Sub
            End If

            ' 新規/更新
            Dim result As Boolean
            If _updateFlg Then
                result = UpdateDRATRN()
            Else
                result = InsertDRATRN(INSERT_DRATRN_MODE.FIRST)
            End If

            If Not result Then
                Using frm As New frmMSGBOX01("ドロア管理トランの登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'Using frm As New frmMSGBOX01("正常に登録できました。", 0)
            '    frm.ShowDialog()
            'End Using

            ReadAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' カレンダー_値変更(初回起動時は起動しない)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtpDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        Try
            If Not _blnInited Then Return

            ' すべて読み込み
            ReadAll()

            ' 過去の日付ならすべてのテキストボックスを入力不可にする
            If Me.dtpDate.Value.ToString("yyyyMMdd").Equals(Now.ToString("yyyyMMdd")) Then
                For Each txt In GetTextBox_All()
                    txt._TextBox.ReadOnly = False
                    btnRegister.Enabled = True
                    btnCheck.Enabled = True
                Next
            Else
                For Each txt In GetTextBox_All()
                    txt._TextBox.ReadOnly = True
                    btnRegister.Enabled = False
                    btnCheck.Enabled = False
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボタン_チェック実施
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        Try           
            If CInt(txtTotalL.Text) + CInt(txtTotal.Text) = CInt(txtTotalR.Text) Then
                lblResult.ForeColor = lblCheck.ForeColor
                lblResult.Text = "OK"
                InsertDRATRN(INSERT_DRATRN_MODE.CHECK_OK)
                ' ドロアチェック済に更新
                If Not UpdateDRAKBN() Then
                    Throw New Exception
                End If
            Else
                lblResult.ForeColor = Color.Red
                lblResult.Text = "NG"
                InsertDRATRN(INSERT_DRATRN_MODE.CHECK_NG)
            End If

            ' 結果を表示
            ReadCheck()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 列の書式
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvDRATRN_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvDRATRN.CellFormatting
        If e.ColumnIndex.Equals(1) Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Else
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End If
    End Sub

    ''' <summary>
    ''' 列を選択不可にする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvDRATRN_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDRATRN.SelectionChanged
        Me.dgvDRATRN.ClearSelection()
    End Sub
    Private Sub dgvCHECK_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCHECK.SelectionChanged
        Me.dgvCHECK.ClearSelection()
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' メイン開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Start()
        Try
            ' 初期化
            Init()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 画面初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            ' 余計なボタンを非表示
            Me.tspFunc3.Enabled = False
            Me.tspFunc8.Enabled = False
            Me.tspFunc9.Enabled = False
            Me.tspFunc12.Enabled = False

            ' コンピュータ名を表示
            lblHOSTPC.Text = "【" & GetHostName() & "】"

            ' チェック実施ボタン無効
            btnCheck.Enabled = False

            ' レジ金登録ボタン無効
            btnRegister.Enabled = False

            ' チェックの結果を初期化
            lblResult.Text = String.Empty

            ' すべてのテキストボックスを初期化
            ClearAllText()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' すべて読み込み
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadAll()
        Try            
            ' 自動計算無効
            _enableCalc = False
            ' 画面初期化
            Init()
            ' 選択中の日付をグループボックスに表示
            gbRegisterL.Text = Me.dtpDate.Value.ToString("yyyy年MM月dd日 レジ金額")
            ' DRATRNの取得
            GetDRATRN_L()
            GetDRATRN_R()
            ' 売り上げ情報の取得
            GetURIAGE()
            ' チェック状況の取得
            GetCHECK()
            ' 自動計算有効
            _enableCalc = True
            ' 合計値を表示
            CalcLeft()
            CalcRight()
            ' 最新日時を設定
            SaveUPDDTM()
            ' グリッドのフォーカスを外す
            Me.dgvDRATRN.CurrentCell = Nothing
            Me.dgvCHECK.CurrentCell = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' すべて読み込み
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadCheck(Optional ByVal bInit As Boolean = True)
        Try           
            ' 売り上げ情報の取得
            GetURIAGE()
            ' チェック状況の取得
            GetCHECK()
            ' 最新日時を設定
            SaveUPDDTM()
            ' グリッドのフォーカスを外す
            Me.dgvDRATRN.CurrentCell = Nothing
            Me.dgvCHECK.CurrentCell = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' すべてのテキストボックスの初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearAllText()
        For Each txt In GetTextBox_All()
            txt._TextBox.Text = "0"
        Next
        txtTotal.Text = "0"
        txtTotalL.Text = "0"
        txtTotalR.Text = "0"
    End Sub

    ''' <summary>
    ''' DRATRNを取得しテキストボックスに結果を表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDRATRN_L()
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" ASU, BSU, CSU, DSU, ESU, FSU, GSU, HSU, ISU, JSU")
            sql.Append(" FROM DRATRN")
            sql.Append(" WHERE HOSTNAME = '" & GetHostName() & "'")
            sql.Append(" AND UDNDT = '" & GetSelectedDateText() & "'")
            sql.Append(" AND FSTKBN = " & "'1'")
            sql.Append(" AND CHKKBN = " & "'0'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' データがなければ0で初期化
            If resultDt.Rows.Count <= 0 Then
                _updateFlg = False
                btnRegister.Text = "レジ金登録"
                Exit Sub
            Else
                ' DRATRNにすでに本日のレコードが登録されていたら更新フラグを立てる            
                _updateFlg = True
                btnRegister.Text = "レジ金修正"
            End If

            ' レジ金額のテキストボックスに読み込んだデータを表示
            Dim i = 0
            For Each txt In GetTextBox_L()
                Dim v = resultDt(0)(i).ToString
                If String.IsNullOrEmpty(v) Then
                    txt._TextBox.Text = "0"
                Else
                    txt._TextBox.Text = v
                End If
                i += 1
            Next

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    ''' <summary>
    ''' DRATRNを取得しテキストボックスに結果を表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDRATRN_R()
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" ASU, BSU, CSU, DSU, ESU, FSU, GSU, HSU, ISU, JSU, UDNNO")
            sql.Append(" FROM DRATRN")
            sql.Append(" WHERE HOSTNAME = '" & GetHostName() & "'")
            sql.Append(" AND UDNDT = '" & GetSelectedDateText() & "'")
            sql.Append(" ORDER BY UDNNO DESC")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' データがなければ0で初期化
            If resultDt.Rows.Count <= 0 Then
                Exit Sub
            End If

            ' レジ金額のテキストボックスに読み込んだデータを表示        
            Dim i = 0
            For Each txt In GetTextBox_R()
                Dim v = resultDt(0)(i).ToString
                If String.IsNullOrEmpty(v) Then
                    txt._TextBox.Text = "0"
                Else
                    txt._TextBox.Text = v
                End If
                i += 1
            Next

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    ''' <summary>
    ''' 売り上げ情報を取得しグリッドに一覧表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetURIAGE()
        Dim sql As New System.Text.StringBuilder
        Dim total = 0
        Dim list As BindingList(Of URIAGE_View) = New BindingList(Of URIAGE_View)
        Try
            _listDRATRN.Clear()



            Dim host = GetHostName()
            Dim udndt = GetSelectedDateText()

            ' 売り上げトラン(スクール)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM UDNTRN")
            sql.Append(" WHERE HOSTNAME = '" & host & "'")
            sql.Append(" AND UDNDT = '" & udndt & "'")
            sql.Append(" AND DATKB = '0'")
            sql.Append(" ORDER BY INSDTM DESC")
            Dim udntrnDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' 商品売上トランの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE HOSTNAME = '" & host & "'")
            sql.Append(" AND UDNDT = '" & udndt & "'")
            sql.Append(" ORDER BY INSDTM DESC")
            Dim dudntrnDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' 伝票トランの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE DATKB = '1'")
            sql.Append(" AND UDNDT = '" & udndt & "'")
            sql.Append(" ORDER BY LINNO DESC")
            Dim dentraDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' チケットクエリ
            Dim ticketQuery = udntrnDt.AsEnumerable.Where(Function(x As DataRow) _
                                                        x("SVCKBN").Equals("0") And _
                                                        x("CPAYKBN").Equals("0"))
            ' 商品売上クエリ
            Dim uriageQuery = dudntrnDt.AsEnumerable.Where(Function(x As DataRow) _
                                                        x("DATKB").Equals("1") And _
                                                        x("BMNCD").Equals("002") And _
                                                        x("BUNCDA").Equals("001") And _
                                                        x("CPAYKBN").Equals("0"))

            ' 現金精算（打席カードは含まない）、年会費のみ表示
            uriageQuery = uriageQuery.Where(Function(x As DataRow) _
                                            x("CPAYKBN").Equals("0") Or _
                                            x("BUNCDC").Equals("999"))

            ' 入金クエリ
            Dim nyukinQuery = dudntrnDt.AsEnumerable.Where(Function(x As DataRow) _
                                                        x("DATKB").Equals("1") And _
                                                        x("BMNCD").Equals("002") And _
                                                        x("BUNCDA").Equals("003") And _
                                                        x("CPAYKBN").Equals("0"))

            ' チケット売り上げをビューに反映
            For Each row As DataRow In ticketQuery
                Dim uriage = New URIAGE_View()
                uriage.UDNTM = DateTime.Parse(row("INSDTMSTR").ToString()).ToString("HH:mm:ss")
                uriage.UDNKN = CInt(row("UDNBKN"))
                uriage.HINNMA = row("HINNMA").ToString
                uriage.MANNO = String.Empty
                uriage.SCLMANNO = row("SCLMANNO").ToString
                uriage.MANNM = getMANNM(uriage.MANNO, uriage.SCLMANNO)
                uriage.STFNAME = row("STFNAME").ToString
                uriage.hide_DRAKBN = row("DRAKBN").ToString
                uriage.hide_UDNNO = CInt(row("UDNNO"))
                list.Add(uriage)
                total += uriage.UDNKN
            Next

            ' 商品売上をビューに反映
            For Each row As DataRow In uriageQuery
                Dim uriage = New URIAGE_View()
                uriage.UDNTM = DateTime.Parse(row("INSDTMSTR").ToString()).ToString("HH:mm:ss")
                uriage.UDNKN = CInt(row("UDNBKN").ToString)

                ' 同じ伝票番号で複数伝票があれば繋げて表示
                Dim udnno = CInt(row("UDNNO"))
                Dim dentraQuery = dentraDt.AsEnumerable.Where(Function(x As DataRow) x("UDNNO").Equals(udnno))
                If dentraQuery.Count >= 1 Then
                    Dim hinnmas = dentraQuery.Select(Function(x As DataRow) x("HINNMA")).ToList
                    uriage.HINNMA = String.Join(",", hinnmas)
                Else
                    uriage.HINNMA = row("HINNMA").ToString
                End If
                uriage.MANNO = row("MANNO").ToString
                uriage.SCLMANNO = row("SCLMANNO").ToString
                uriage.MANNM = getMANNM(uriage.MANNO, uriage.SCLMANNO)
                uriage.STFNAME = row("STFNAME").ToString
                uriage.hide_DRAKBN = row("DRAKBN").ToString
                uriage.hide_UDNNO = CInt(row("UDNNO"))
                list.Add(uriage)
                total += uriage.UDNKN
            Next

            ' 入金をビューに反映
            For Each row As DataRow In nyukinQuery
                Dim uriage = New URIAGE_View()
                uriage.UDNTM = DateTime.Parse(row("INSDTMSTR").ToString()).ToString("HH:mm:ss")
                uriage.UDNKN = CInt(row("UDNBKN"))
                uriage.HINNMA = row("HINNMA").ToString
                uriage.MANNO = row("MANNO").ToString
                uriage.SCLMANNO = row("SCLMANNO").ToString
                uriage.MANNM = getMANNM(uriage.MANNO, uriage.SCLMANNO)
                uriage.STFNAME = row("STFNAME").ToString
                uriage.hide_DRAKBN = row("DRAKBN").ToString
                uriage.hide_UDNNO = CInt(row("UDNNO"))
                list.Add(uriage)
                total += uriage.UDNKN
            Next

            ' 時間の降順でソート
            Dim sortList = list.OrderByDescending(Function(x As URIAGE_View) x.UDNTM).ToList()
            _listDRATRN = New BindingList(Of URIAGE_View)(sortList)
            Me.dgvDRATRN.DataSource = _listDRATRN

            txtTotal.Text = total.ToString("#,0")

            ' チェック済みなら背景色をグレーにする
            For Each row As DataGridViewRow In Me.dgvDRATRN.Rows
                If row.Cells(7).Value.ToString.Equals("1") Then
                    row.DefaultCellStyle.BackColor = SystemColors.ControlDark
                Else
                    row.DefaultCellStyle.BackColor = SystemColors.Window
                End If
                row.DefaultCellStyle.Font = New Font("ＭＳ ゴシック", 20)
            Next

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 顧客氏名かスクール生氏名を取得する
    ''' </summary>
    ''' <param name="manno"></param>
    ''' <param name="sclmanno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getMANNM(ByVal manno As String, ByVal sclmanno As String) As String
        Dim sql As New System.Text.StringBuilder
        Try

            ' 顧客番号があればCSMASTから氏名を取得
            If Not String.IsNullOrEmpty(manno) Then

                Dim ncsno = CInt(manno.TrimStart("0"c))

                ' 氏名取得用のテーブル取得(顧客マスタ)
                sql.Clear()
                sql.Append(" SELECT")
                sql.Append(" *")
                sql.Append(" FROM CSMAST")
                sql.Append(String.Format(" WHERE NCSNO  = '{0}'", ncsno))

                Dim csDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
                Dim query = csDt.AsEnumerable

                If query.Any Then
                    Return query.Select(Function(x As DataRow) x("CCSNAME")).First().ToString
                End If
            End If

            ' スクール生番号があればMANMSTから氏名を取得
            If Not String.IsNullOrEmpty(sclmanno) Then

                ' 氏名取得用のテーブル取得(スクール生マスタ)
                sql.Clear()
                sql.Append(" SELECT")
                sql.Append(" *")
                sql.Append(" FROM MANMST")
                sql.Append(String.Format(" WHERE SCLMANNO = '{0}'", sclmanno))

                Dim manDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
                Dim query = manDt.AsEnumerable

                If query.Any Then
                    Return query.Select(Function(x As DataRow) x("MANNM")).First().ToString
                End If
            End If

            Return String.Empty

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function


    ''' <summary>
    ''' チェック状況を取得しグリッドに表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCHECK()
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DRATRN")
            sql.Append(" WHERE HOSTNAME = '" & GetHostName() & "'")
            sql.Append(" AND UDNDT = '" & GetSelectedDateText() & "'")
            sql.Append(" AND FSTKBN = " & "'0'")
            sql.Append(" ORDER BY INSDTM DESC")
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' バインド
            Dim list As BindingList(Of CHECK_View) = New BindingList(Of CHECK_View)
            Me.dgvCHECK.DataSource = list
            list.Clear()

            If resultDt.Rows.Count <= 0 Then
                Exit Sub
            End If

            ' グリッドに表示
            For Each row As DataRow In resultDt.Rows
                Dim data = New CHECK_View()
                data.CHKTM = DateTime.Parse(row("INSDTM").ToString).ToString("HH:mm:ss")
                If row("CHKKBN").Equals("1") Then
                    data.CHKRET = "OK"
                Else
                    data.CHKRET = "NG"
                End If
                data.CHKKN = CInt(row("TOTALKN").ToString)
                list.Add(data)
            Next

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    ''' <summary>
    ''' DRATRNにINSERT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertDRATRN(ByVal mode As INSERT_DRATRN_MODE) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            ' INSERT_MODE で処理変更
            Dim fstkbn As String
            Dim chkkbn As String
            Dim udnno As Integer
            Dim txts() As PriceTextBox
            Dim strTotal As String
            Select Case mode
                Case INSERT_DRATRN_MODE.CHECK_OK
                    fstkbn = "0"
                    chkkbn = "1"
                    udnno = GetMaxUDNNO() + 1
                    txts = GetTextBox_R()
                    strTotal = txtTotalR.Text
                Case INSERT_DRATRN_MODE.CHECK_NG
                    fstkbn = "0"
                    chkkbn = "2"
                    udnno = GetMaxUDNNO() + 1
                    txts = GetTextBox_R()
                    strTotal = txtTotalR.Text
                Case Else
                    fstkbn = "1"
                    chkkbn = "0"
                    udnno = 1
                    txts = GetTextBox_L()
                    strTotal = txtTotalL.Text
            End Select

            'トランザクション開始
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("INSERT INTO DRATRN VALUES(")
            sql.Append("'" & GetHostName() & "',")
            sql.Append("'" & GetSelectedDateText() & "',")
            sql.Append(udnno.ToString & ",")
            sql.Append(fstkbn & ",")
            For Each txt In txts
                sql.Append(IIf(String.IsNullOrEmpty(txt._TextBox.Text), "0", txt._TextBox.Text).ToString & ",")
            Next
            sql.Append(strTotal.Replace(",", "") & ",")
            sql.Append(chkkbn & ",")
            sql.Append("NOW(),")
            sql.Append("NOW())")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            'コミット処理
            iDatabase.Commit()

            ' 最新日時の更新
            SaveUPDDTM()

            Return True

        Catch ex As Exception
            'ロールバック
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' DRATRNにUPDATE
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateDRATRN() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            'トランザクション開始
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE DRATRN SET")
            sql.Append(" ASU = " & IIf(String.IsNullOrEmpty(txt10000L.Text), "0", txt10000L.Text).ToString & ",")
            sql.Append(" BSU = " & IIf(String.IsNullOrEmpty(txt5000L.Text), "0", txt5000L.Text).ToString & ",")
            sql.Append(" CSU = " & IIf(String.IsNullOrEmpty(txt2000L.Text), "0", txt2000L.Text).ToString & ",")
            sql.Append(" DSU = " & IIf(String.IsNullOrEmpty(txt1000L.Text), "0", txt1000L.Text).ToString & ",")
            sql.Append(" ESU = " & IIf(String.IsNullOrEmpty(txt500L.Text), "0", txt500L.Text).ToString & ",")
            sql.Append(" FSU = " & IIf(String.IsNullOrEmpty(txt100L.Text), "0", txt100L.Text).ToString & ",")
            sql.Append(" GSU = " & IIf(String.IsNullOrEmpty(txt50L.Text), "0", txt50L.Text).ToString & ",")
            sql.Append(" HSU = " & IIf(String.IsNullOrEmpty(txt10L.Text), "0", txt10L.Text).ToString & ",")
            sql.Append(" ISU = " & IIf(String.IsNullOrEmpty(txt5L.Text), "0", txt5L.Text).ToString & ",")
            sql.Append(" JSU = " & IIf(String.IsNullOrEmpty(txt1L.Text), "0", txt1L.Text).ToString & ",")
            sql.Append(" TOTALKN = " & txtTotalL.Text.Replace(",", "") & ",")
            sql.Append(" UPDDTM = NOW()")
            sql.Append(" WHERE ")
            sql.Append(" HOSTNAME = '" & GetHostName() & "' AND")
            sql.Append(" UDNDT = '" & GetSelectedDateText() & "' AND")
            sql.Append(" FSTKBN = '1' AND")
            sql.Append(" CHKKBN = '0'")


            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            'コミット処理
            iDatabase.Commit()

            ' 最新日時の更新
            SaveUPDDTM()

            Return True

        Catch ex As Exception
            'ロールバック
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' DUDNTRN, UDNTRN, DENTRA にDRAKBNを済にUPDATE
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateDRAKBN() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            ' 更新するテーブル
            Dim tables() As String = {"DUDNTRN", "UDNTRN", "DENTRA"}

            'トランザクション開始
            iDatabase.BeginTransaction()

            For Each row In _listDRATRN
                For Each table In tables
                    sql.Clear()
                    sql.Append("UPDATE " & table & " SET")
                    sql.Append(" DRAKBN = '1'")
                    sql.Append(" WHERE ")
                    sql.Append(" HOSTNAME = '" & GetHostName() & "' AND")
                    sql.Append(" UDNDT = '" & GetSelectedDateText() & "' AND")
                    sql.Append(" UDNNO = " & row.hide_UDNNO)

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                Next
            Next

            'コミット処理
            iDatabase.Commit()

            ' 最新日時の更新
            SaveUPDDTM()

            Return True

        Catch ex As Exception
            'ロールバック
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' UDNNO の最大値を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMaxUDNNO() As Integer
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Clear()
            sql.Append(" SELECT UDNNO FROM DRATRN WHERE")
            sql.Append(" HOSTNAME = '" & GetHostName() & "' AND")
            sql.Append(" UDNDT = '" & GetSelectedDateText() & "'")
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim udnno As Integer = 1
            If resultDt.Rows.Count > 0 Then
                udnno = resultDt.AsEnumerable.Select(Function(x As DataRow) CInt(x("UDNNO").ToString)).Max
            End If
            Return udnno
        Catch ex As Exception
            Throw ex
        Finally

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
            sql.Append(" FROM DRATRN")
            sql.Append(" WHERE")
            sql.Append(" HOSTNAME = '" & GetHostName() & "'")
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
    ''' ' 他端末からの更新チェック(メッセージ機能付き)
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDBUpdated(Optional ByVal msg As Boolean = True) As Boolean
        Dim dtUPDDTM = GetCurrentUPDDTM()
        If Not dtUPDDTM.Equals(Nothing) Then
            If Not dtUPDDTM.Equals(_dtUPDDTM) Then
                If msg Then
                    Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 最新日時を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveUPDDTM()
        _dtUPDDTM = GetCurrentUPDDTM()
    End Sub

    ''' <summary>
    ''' 選択中の日付文字列を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSelectedDateText() As String
        Return Me.dtpDate.Value.ToString("yyyyMMdd")
    End Function

    ''' <summary>
    ''' 左側のフィールドを計算して合計値を表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalcLeft()
        If Not _enableCalc Then Return

        Dim total As Integer = 0

        ' 一つでも有効な値ならレジ金登録ボタンを有効化 / 合計値の加算
        btnRegister.Enabled = False
        For Each txt In GetTextBox_L()
            If Not (String.IsNullOrEmpty(txt._TextBox.Text) Or txt._TextBox.Text = "0") Then
                btnRegister.Enabled = True
                total += CInt(txt._TextBox.Text) * txt._Price
            End If
        Next

        ' 合計計算してカンマ表示
        txtTotalL.Text = total.ToString("#,0")
    End Sub

    ''' <summary>
    ''' 右側のフィールドを計算して合計値を表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalcRight()
        If Not _enableCalc Then Return

        Dim total As Integer = 0

        ' 一つでも有効な値ならレジ金登録ボタンを有効化 / 合計値の加算
        btnCheck.Enabled = False
        For Each txt In GetTextBox_R()
            If Not (String.IsNullOrEmpty(txt._TextBox.Text) Or txt._TextBox.Text = "0") Then
                ' レジ金登録されていなければチェック不可
                If _updateFlg Then
                    btnCheck.Enabled = True
                End If
                total += CInt(txt._TextBox.Text) * txt._Price
            End If
        Next

        ' 合計計算してカンマ表示
        txtTotalR.Text = total.ToString("#,0")
    End Sub

    ''' <summary>
    ''' テキストボックスの配列を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTextBox_L() As PriceTextBox()
        Dim txts() As PriceTextBox = {New PriceTextBox(txt10000L, 10000), _
                                      New PriceTextBox(txt5000L, 5000), _
                                      New PriceTextBox(txt2000L, 2000), _
                                      New PriceTextBox(txt1000L, 1000), _
                                      New PriceTextBox(txt500L, 500), _
                                      New PriceTextBox(txt100L, 100), _
                                      New PriceTextBox(txt50L, 50), _
                                      New PriceTextBox(txt10L, 10), _
                                      New PriceTextBox(txt5L, 5), _
                                      New PriceTextBox(txt1L, 1)}
        Return txts
    End Function

    Private Function GetTextBox_R() As PriceTextBox()
        Dim txts() As PriceTextBox = {New PriceTextBox(txt10000R, 10000), _
                                      New PriceTextBox(txt5000R, 5000), _
                                      New PriceTextBox(txt2000R, 2000), _
                                      New PriceTextBox(txt1000R, 1000), _
                                      New PriceTextBox(txt500R, 500), _
                                      New PriceTextBox(txt100R, 100), _
                                      New PriceTextBox(txt50R, 50), _
                                      New PriceTextBox(txt10R, 10), _
                                      New PriceTextBox(txt5R, 5), _
                                      New PriceTextBox(txt1R, 1)}
        Return txts
    End Function

    Private Function GetTextBox_All() As PriceTextBox()
        Dim txts() As PriceTextBox = {New PriceTextBox(txt10000L, 10000), _
                                      New PriceTextBox(txt5000L, 5000), _
                                      New PriceTextBox(txt2000L, 2000), _
                                      New PriceTextBox(txt1000L, 1000), _
                                      New PriceTextBox(txt500L, 500), _
                                      New PriceTextBox(txt100L, 100), _
                                      New PriceTextBox(txt50L, 50), _
                                      New PriceTextBox(txt10L, 10), _
                                      New PriceTextBox(txt5L, 5), _
                                      New PriceTextBox(txt1L, 1), _
                                      New PriceTextBox(txt10000R, 10000), _
                                      New PriceTextBox(txt5000R, 5000), _
                                      New PriceTextBox(txt2000R, 2000), _
                                      New PriceTextBox(txt1000R, 1000), _
                                      New PriceTextBox(txt500R, 500), _
                                      New PriceTextBox(txt100R, 100), _
                                      New PriceTextBox(txt50R, 50), _
                                      New PriceTextBox(txt10R, 10), _
                                      New PriceTextBox(txt5R, 5), _
                                      New PriceTextBox(txt1R, 1)}
        Return txts
    End Function

#End Region



End Class
