Imports TECHNO.DataBase

Public Class frmFEEHIST01

#Region "▼宣言部"

    ''' <summary>
    ''' カード挿入済み確認フラグ【True】挿入済み
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnCardFLG As Boolean = False
    ''' <summary>
    ''' 年会費情報ﾃｰﾌﾞﾙ
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtFeeInfo As DataTable
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
    Private _intMaxRowCount As Integer = 13
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "年会費履歴"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "年会費履歴"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod,
                   ByVal ICR700 As TECHNO.DeviceControls.ICR700Control,
                   ByVal CardFLG As Boolean)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "年会費履歴"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            dcICR700 = ICR700

            _blnCardFLG = CardFLG
            'データベース接続情報を引き継ぐ
            iDatabase = iDB
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MANNO() As String
        Set(ByVal value As String)
            _strMANNO = value
        End Set
    End Property
    Private _strMANNO As String = String.Empty

    ''' <summary>
    ''' カードフラグ(挿入中か排出済みか)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CARDFLG As Boolean
        Get
            Return _blnCardFLG
        End Get
    End Property

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFEEHIST01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            Me.cmbTerm.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFEEHIST01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.btnSearch.PerformClick()

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
    Private Sub frmFEEHIST01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not _dtFeeInfo Is Nothing Then
                _dtFeeInfo.Dispose()
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
    ''' 顧客番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNCSNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStaNCSNO.Validated
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
    ''' グリッド_CellContentClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvFEEHIST_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFEEHIST.CellContentClick
        Dim sql As New System.Text.StringBuilder
        Dim strNCSNO As String = String.Empty
        Dim intFEE As Integer = 0
        Dim intPREMKN As Integer = 0
        Dim intKINGAKU As Integer = 0
        Dim intZANKN As Integer = 0
        Dim intPREZANKN As Integer = 0
        Dim intV31POINT As Integer = 0
        Try
            Dim dgv As DataGridView = CType(sender, DataGridView)

            If dgv.Columns(e.ColumnIndex).Name = "colCANCEL" Then
                '【取消】

                If Not Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNDT").Value.ToString.Equals(Now.ToString("yyyy/MM/dd")) Then
                    Using frm As New frmMSGBOX01("過去のデータは取消できません。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
                If Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colDRAKBN").Value.ToString.Equals("1") Then
                    Using frm As New frmMSGBOX01("ドロアチェック済みの為取消できません。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
                Using frm As New frmMSGBOX01("№" & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colNO").Value.ToString & "のデータを取消してよろしいですか？", 1)
                    frm.ShowDialog()
                    If Not frm.Reply Then
                        Exit Sub
                    End If
                End Using

                If Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colCPAYKBN").Value.ToString.Equals("打席カード") Then
                    'カード読み込み
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                        frm.ShowDialog()
                        If frm.CANCEL Then Exit Sub
                        If frm.ERRFLG Then Exit Sub
                    End Using

                    '顧客情報取得
                    sql.Clear()
                    sql.Append(" SELECT ")
                    sql.Append(" A.*")
                    sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS TO_DBIRTH")  '誕生日
                    sql.Append(",TO_CHAR(A.DENTRY,'YYYY/MM/DD') AS TO_DENTRY") '会員登録日
                    sql.Append(",TO_CHAR(A.DMEMBER,'YYYY/MM/DD') AS TO_DMEMBER") '会員期限
                    sql.Append(",B.ZANKN")
                    sql.Append(",B.PREZANKN")
                    sql.Append(",C.SRTPO")
                    sql.Append(",D.CKBNAME")
                    sql.Append(",E.SCLKBN")
                    sql.Append(" FROM CSMAST AS A")
                    sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
                    sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
                    sql.Append(" LEFT JOIN KBMAST AS D ON D.NKBNO = A.NCSRANK")
                    sql.Append(" LEFT JOIN MANMST AS E ON E.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
                    sql.Append(" WHERE")
                    sql.Append(" NCARDID = " & CType(dcICR700.NCSNO, Integer))

                    Dim dtCSMAST As DataTable = iDatabase.ExecuteRead(sql.ToString())

                    If dtCSMAST.Rows.Count.Equals(0) Then
                        Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                            frm.ShowDialog()
                        End Using
                        If Not _blnCardFLG Then
                            Using frm As New frmREQUESTCARD(dcICR700)
                                'カード排出処理
                                frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                                frm.ShowDialog()
                            End Using
                        End If
                        Exit Sub
                    End If

                    '顧客番号
                    strNCSNO = Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colMANNO").Value.ToString
                    If Not strNCSNO.Equals(dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)) Then
                        Using frm As New frmMSGBOX01("顧客番号が一致しません。", 3)
                            frm.ShowDialog()
                        End Using
                        If Not _blnCardFLG Then
                            Using frm As New frmREQUESTCARD(dcICR700)
                                'カード排出処理
                                frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                                frm.ShowDialog()
                            End Using
                        End If
                        Exit Sub
                    End If

                    '年会費
                    intFEE = CType(Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colFEE").Value.ToString, Integer)
                    '支払ﾌﾟﾚﾐｱﾑ
                    intPREMKN = CType(Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colPREMKN").Value.ToString, Integer)

                    intKINGAKU = CType(dcICR700.KINGAKU, Integer) + (intFEE)

                    If intKINGAKU > UIUtility.SYSTEM.ZANMAX Then
                        Using frm As New frmMSGBOX01("入金限度額を超えている為取消できません。", 2)
                            frm.ShowDialog()
                        End Using
                        If Not _blnCardFLG Then
                            Using frm As New frmREQUESTCARD(dcICR700)
                                'カード排出処理
                                frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                                frm.ShowDialog()
                            End Using
                        End If
                        Exit Sub
                    End If

                    '【プリカRW書き込み情報セット】
                    '店番号
                    dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
                    'パスワード
                    dcICR700.PASSCD_WR = dcICR700.PASSCD
                    'シリアルナンバー
                    dcICR700.SERIALNO_WR = dcICR700.SERIALNO
                    '種別
                    dcICR700.SYUBETU_WR = dcICR700.SYUBETU
                    '金額
                    dcICR700.KINGAKU_WR = intKINGAKU.ToString.PadLeft(5, "0"c)
                    '予備
                    dcICR700.YOBI_WR = dcICR700.YOBI

                    '【V31RW書き込み情報セット】

                    intZANKN = CType(dcICR700.ZANKN, Integer) + (intFEE - intPREMKN)
                    intPREZANKN = CType(dcICR700.PREZANKN, Integer) + intPREMKN
                    intV31POINT = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
                    '店番
                    dcICR700.SHOPNO_WR = dcICR700.SHOPNO
                    'カード区分
                    dcICR700.CARDKBN_WR = dcICR700.CARDKBN
                    'カード番号
                    dcICR700.CARDNO_WR = dcICR700.CARDNO
                    '顧客番号
                    dcICR700.NCSNO_WR = dcICR700.NCSNO
                    'スクール生番号
                    dcICR700.SCLMANNO_WR = dcICR700.SCLMANNO
                    '顧客種別
                    dcICR700.NKBNO_WR = dcICR700.NKBNO
                    '会員期限
                    dcICR700.DMEMBER_WR = dcICR700.DMEMBER
                    'パスワード
                    dcICR700.PASSCD_WR = dcICR700.PASSCD
                    '残金額
                    dcICR700.ZANKN_WR = intZANKN.ToString.PadLeft(5, "0"c)
                    'P残金額
                    dcICR700.PREZANKN_WR = intPREZANKN.ToString.PadLeft(5, "0"c)
                    '残ポイント
                    dcICR700.POINT_WR = intV31POINT.ToString.PadLeft(5, "0"c)
                    '前回来場日
                    dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
                    '入場区分
                    dcICR700.ENTKBN_WR = dcICR700.ENTKBN
                    'ボール単価
                    dcICR700.BALLKIN_WR = dcICR700.BALLKIN
                    '打席番号
                    dcICR700.SEATNO_WR = dcICR700.SEATNO

                End If

                iDatabase.BeginTransaction()


                sql.Clear()
                sql.Append("UPDATE DUDNTRN SET DATKB = '9' WHERE")
                sql.Append(" UDNDT = '" & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNDT").Value.ToString.Replace("/", String.Empty) & "'")
                sql.Append(" AND UDNNO = " & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNNO").Value.ToString)

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("取消処理に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
                sql.Clear()
                sql.Append("UPDATE DENTRA SET DATKB = '9' WHERE")
                sql.Append(" UDNDT = '" & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNDT").Value.ToString.Replace("/", String.Empty) & "'")
                sql.Append(" AND UDNNO = " & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNNO").Value.ToString)

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("取消処理に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                sql.Clear()
                sql.Append("UPDATE HINTRN SET DATKB = '9' WHERE")
                sql.Append(" DENDT = '" & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNDT").Value.ToString.Replace("/", String.Empty) & "'")
                sql.Append(" AND DENNO = " & Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colUDNNO").Value.ToString)

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("取消処理に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                If Me.dgvFEEHIST.Rows(e.RowIndex).Cells("colCPAYKBN").Value.ToString.Equals("打席カード") Then
                    '【金額サマリ】
                    sql.Clear()
                    sql.Append("UPDATE KINSMA SET")
                    sql.Append(" ZANKN = " & intZANKN)
                    sql.Append(",PREZANKN = " & intPREZANKN)
                    sql.Append(",UPDDTM = NOW()")
                    sql.Append(" WHERE")
                    sql.Append(" MANNO = '" & strNCSNO & "'")
                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                    End If

                    '【カード書き込み】
                    Dim blnERRFLG As Boolean = False
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                        frm.ShowDialog()
                        blnERRFLG = frm.ERRFLG
                    End Using
                    If blnERRFLG Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If
                    _blnCardFLG = False
                End If


                iDatabase.Commit()

                Using frm As New frmMSGBOX01("正常に取消できました。" & vbCr & "期限の修正は顧客登録画面より行って下さい。", 0)
                    frm.ShowDialog()
                End Using

                Me.btnSearch.PerformClick()

            End If


        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 検索ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strStaSEATDT As String = String.Empty   '営業開始日付
        Dim strEndSEATDT As String = String.Empty   '営業終了日付
        Dim sql As New System.Text.StringBuilder
        Dim dr As DataRow()
        Dim strKSBKB As String = String.Empty
        Dim intNKBNO As Integer = 0
        Try

            If Not _dtFeeInfo Is Nothing Then
                _dtFeeInfo.Clear()
            End If

            Me.dgvFEEHIST.RowCount = 0

            strStaSEATDT = Me.dtpStaSEATDT.Text.Replace("/", String.Empty)
            strEndSEATDT = Me.dtpEndSEATDT.Text.Replace("/", String.Empty)

            If Me.cmbKBMAST.SelectedIndex > 0 Then
                If Me.cmbKBMAST.SelectedIndex.Equals(10) Then
                    strKSBKB = "A"
                Else
                    strKSBKB = Me.cmbKBMAST.SelectedIndex.ToString
                End If
            End If

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ROW_NUMBER() OVER(ORDER BY A.INSDTM DESC) AS NO")
            sql.Append(",A.*")
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI') AS UDNTIME")
            sql.Append(",B.CCSNAME")
            sql.Append(" FROM DUDNTRN AS A ")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) = A.MANNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '001'")
            sql.Append(" AND A.BUNCDB = '001'")
            sql.Append(" AND A.BUNCDC = '999'")
            sql.Append(" AND A.UDNDT >= '" & strStaSEATDT & "'")
            sql.Append(" AND A.UDNDT <= '" & strEndSEATDT & "'")
            If Not (String.IsNullOrEmpty(Me.txtStaNCSNO.Text) Or Me.txtStaNCSNO.Text.Equals("00000000")) Then
                sql.Append(" AND A.MANNO = '" & Me.txtStaNCSNO.Text & "'")
            End If
            If Not String.IsNullOrEmpty(strKSBKB) Then
                sql.Append(" AND A.KSBKB = '" & strKSBKB & "'")
            End If
            sql.Append(" ORDER BY NO")

            _dtFeeInfo = iDatabase.ExecuteRead(sql.ToString())

            If _dtFeeInfo.Rows.Count.Equals(0) Then
                Using frm As New frmMSGBOX01("対象のデータがありません。", 3)
                    frm.ShowDialog()
                End Using
                Me.btnBack.Enabled = False
                Me.btnNext.Enabled = False
                Exit Sub
            End If

            _intPageNo = 1

            '最終ページ数計算
            Dim dblLastPageNo As Double = _dtFeeInfo.Rows.Count / _intMaxRowCount
            _intLastPageNo = CType(Math.Ceiling(dblLastPageNo), Integer)

            Me.btnBack.Enabled = False
            Me.btnNext.Enabled = False

            If _intLastPageNo > 1 Then
                Me.btnNext.Enabled = True
            End If

            '年会費情報グリッド表示
            SetFeeInfo(_dtFeeInfo.Select("NO >= 1"))


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
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Try
            If _intPageNo.Equals(0) Then _intPageNo = 1

            Me.dgvFEEHIST.RowCount = 0

            '年会費情報グリッド表示
            SetFeeInfo(_dtFeeInfo.Select("NO >= " & (_intPageNo * _intMaxRowCount) + 1))

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
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.dgvFEEHIST.RowCount = 0
            _intPageNo -= 1

            '年会費情報グリッド表示
            SetFeeInfo(_dtFeeInfo.Select("NO >= " & (_intPageNo * _intMaxRowCount) - (_intMaxRowCount - 1)))

            Me.btnNext.Enabled = True
            If _intPageNo.Equals(1) Then
                Me.btnBack.Enabled = False
            End If

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
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            '顧客番号
            Me.txtStaNCSNO.Text = _strMANNO

            'グリッド
            Me.dgvFEEHIST.RowCount = 0

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
    ''' 年会費情報グリッド表示
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub SetFeeInfo(ByVal dr As DataRow())
        Dim drKBMAST As DataRow()
        Dim intNKBNO As Integer = 0
        Try
            For i As Integer = 0 To dr.Length - 1
                Me.dgvFEEHIST.RowCount += 1

                '連番
                Me.dgvFEEHIST.SetValue("colNO", i, dr(i).Item("NO").ToString)
                '日付
                Me.dgvFEEHIST.SetValue("colUDNDT", i, dr(i).Item("UDNDT").ToString.Substring(0, 4) & "/" & dr(i).Item("UDNDT").ToString.Substring(4, 2) & "/" & dr(i).Item("UDNDT").ToString.Substring(6, 2))
                '時間
                Me.dgvFEEHIST.SetValue("colUDNTIME", i, dr(i).Item("UDNTIME").ToString)
                '顧客番号
                Me.dgvFEEHIST.SetValue("colMANNO", i, dr(i).Item("MANNO").ToString)
                '氏名
                Me.dgvFEEHIST.SetValue("colCCSNAME", i, dr(i).Item("CCSNAME").ToString)
                '顧客種別
                Select Case dr(i).Item("KSBKB").ToString
                    Case "A" : intNKBNO = 10
                    Case Else : intNKBNO = CType(dr(i).Item("KSBKB").ToString, Integer)
                End Select
                drKBMAST = UIUtility.TABLE.KBMAST.Select("NKBNO = " & intNKBNO)
                If drKBMAST.Length > 0 Then Me.dgvFEEHIST.SetValue("colCKBNAME", i, drKBMAST(0).Item("CKBNAME").ToString)
                '精算区分
                Select Case CType(dr(i).Item("CPAYKBN").ToString, Integer)
                    Case 0 '【現金】
                        Me.dgvFEEHIST.SetValue("colCPAYKBN", i, "現金")
                    Case 1 '【カード払い】
                        Me.dgvFEEHIST.SetValue("colCPAYKBN", i, "カード")
                    Case 2 '【商品券】
                        Me.dgvFEEHIST.SetValue("colCPAYKBN", i, "商品券")
                    Case 3 '【銀行振込】
                        Me.dgvFEEHIST.SetValue("colCPAYKBN", i, "銀行振込")
                    Case 4 '【打席カード】
                        Me.dgvFEEHIST.SetValue("colCPAYKBN", i, "打席カード")
                End Select
                '担当者
                Me.dgvFEEHIST.SetValue("colSTFNAME", i, dr(i).Item("STFNAME").ToString)
                '年会費
                Me.dgvFEEHIST.SetValue("colFEE", i, CType(dr(i).Item("UDNBKN").ToString, Integer).ToString("#,##0"))
                '取消
                Me.dgvFEEHIST.SetValue("colCANCEL", i, "取消")
                '伝票番号
                Me.dgvFEEHIST.SetValue("colUDNNO", i, dr(i).Item("UDNNO").ToString)
                'ドロア区分
                Me.dgvFEEHIST.SetValue("colDRAKBN", i, dr(i).Item("DRAKBN").ToString)
                'ﾌﾟﾚﾐｱﾑ
                Me.dgvFEEHIST.SetValue("colPREMKN", i, dr(i).Item("PREMKN").ToString)

                If i.Equals(_intMaxRowCount - 1) Then
                    Exit For
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region







End Class
