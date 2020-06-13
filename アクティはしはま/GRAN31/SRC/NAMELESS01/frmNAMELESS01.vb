Imports TECHNO.DataBase

Public Class frmNAMELESS01

#Region "▼宣言部"
    ''' <summary>
    ''' 顧客種別情報
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtKBMAST As DataTable

    Private _intMAXNCSNO As Integer = 0
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "新規カード作成"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod,
                   ByVal ICR700 As TECHNO.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "新規カード作成"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 処理後フォームを閉じるかどうか
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FormClose As Boolean
        Set(ByVal value As Boolean)
            _blnFormClose = value
        End Set
    End Property
    Private _blnFormClose As Boolean = False

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNAMELESS01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '画面初期設定
            Init()


            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '顧客番号取得
            If Not GetMANNOSEQ() Then
                Using frm As New frmMSGBOX01("顧客番号の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

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
    Private Sub frmNAMELESS01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            '顧客種別
            Me.cmbNCSRANK.SelectedIndex = 1
            Me.txtCCSNAME.Focus()

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
    Private Sub cmbNCSRANK_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNCSRANK.SelectedIndexChanged
        Try
            If Me.cmbNCSRANK.SelectedIndex.Equals(-1) Then Exit Sub

            Dim dr As DataRow()
            dr = _dtKBMAST.Select("NKBNO = " & (Me.cmbNCSRANK.SelectedIndex + 1))

            Dim intCKBLIMIT As Integer = CType(dr(0).Item("CKBLIMIT").ToString, Integer)

            If intCKBLIMIT.Equals(0) Then
                Me.txtDMEMBER.Text = String.Empty
                Exit Sub
            End If

            'Dim dtToday As DateTime = DateTime.Parse(Now.Year.ToString & "/" & Now.Month.ToString.PadLeft(2, "0"c) & "/" & Now.Day.ToString.PadLeft(2, "0"c))
            'Me.txtDMEMBER.Text = dtToday.AddMonths(intCKBLIMIT).ToString("yyyy/MM/dd")
            Me.txtDMEMBER.Text = Now.ToString("yyyy/MM/dd")

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
    Private Sub txtNCSNO_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtNCSNO.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                    If Me.txtNCSNO.Text.Equals("00000000") Or Me.txtNCSNO.Text >= "40000000" Then
                        Using frm As New frmMSGBOX01("登録できない顧客番号です。", 2)
                            frm.ShowDialog()
                        End Using
                        '顧客番号取得
                        If Not GetMANNOSEQ() Then
                            Using frm As New frmMSGBOX01("顧客番号の取得に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                        End If
                        Exit Sub
                    End If
                    If _intMAXNCSNO < CType(Me.txtNCSNO.Text, Integer) Then
                        Using frm As New frmMSGBOX01("登録できない顧客番号です。", 2)
                            frm.ShowDialog()
                        End Using
                        '顧客番号取得
                        If Not GetMANNOSEQ() Then
                            Using frm As New frmMSGBOX01("顧客番号の取得に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                        End If
                        Exit Sub
                    End If
                    If GetCSMAST() Then
                        Using frm As New frmMSGBOX01("既に登録済みの顧客番号です。", 3)
                            frm.ShowDialog()
                        End Using
                        '顧客番号取得
                        If Not GetMANNOSEQ() Then
                            Using frm As New frmMSGBOX01("顧客番号の取得に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                        End If
                        Exit Sub
                    End If
                End If
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
    Private Sub txtNCSNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNCSNO.Validated
        Try
            If Not String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                Me.txtNCSNO.Text = Me.txtNCSNO.Text.PadLeft(8, "0"c)
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
    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDMEMBER.KeyPress
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
    ''' 会員期限テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDMEMBER_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDMEMBER.Validated
        Try
            If String.IsNullOrEmpty(Me.txtDMEMBER.Text) Then Exit Sub

            Dim strDMEMBER As String = Me.txtDMEMBER.Text.Replace("/", String.Empty).PadLeft(8, "0"c)

            Me.txtDMEMBER.Text = strDMEMBER.Substring(0, 4) & "/" & strDMEMBER.Substring(4, 2) & "/" & strDMEMBER.Substring(6, 2)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 作成ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard.Click
        Dim sql As New System.Text.StringBuilder
        Dim blnERRFLG As Boolean = False
        Dim intNKNKN As Integer = 0
        Dim intPREMKN As Integer = 0
        Dim intSRTPO As Integer = 0
        Try
            Me.btnCard.Enabled = False

            '種別
            Dim strSYUBETU As String = (Me.cmbNCSRANK.SelectedIndex + 1).ToString
            If strSYUBETU.Equals("10") Then
                strSYUBETU = "A"
            End If


            '【ICカードセクタ２書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = "00"
            'シリアルナンバー
            dcICR700.SERIALNO_WR = "000"
            '種別
            dcICR700.SYUBETU_WR = strSYUBETU
            '金額
            dcICR700.KINGAKU_WR = (intNKNKN + intPREMKN).ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = "0"

            '【ICカードセクタ３、４書き込み情報セット】
            '店番
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'カード区分
            dcICR700.CARDKBN_WR = "1"
            'カード番号
            dcICR700.CARDNO_WR = Me.lblCARDNO.Text
            '顧客番号
            dcICR700.NCSNO_WR = Me.lblCARDNO.Text
            'スクール生番号
            dcICR700.SCLMANNO_WR = "000000"
            '顧客種別
            dcICR700.NKBNO_WR = strSYUBETU
            '会員期限
            dcICR700.DMEMBER_WR = "00000000"
            'パスワード
            dcICR700.PASSCD_WR = "00"
            '残金額
            dcICR700.ZANKN_WR = intNKNKN.ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = intPREMKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = intSRTPO.ToString.PadLeft(5, "0"c)
            '前回来場日
            dcICR700.ZENENTDATE_WR = "00000000"
            '入場区分
            dcICR700.ENTKBN_WR = "0"
            If Me.chkHakko.Checked Then
                dcICR700.ENTKBN_WR = "1"
            End If
            'ボール単価
            dcICR700.BALLKIN_WR = "00"
            '打席番号
            dcICR700.SEATNO_WR = "FFF"

            'カード番号カウントアップ
            Try
                iDatabase.BeginTransaction()

                If _intMAXNCSNO.Equals(CType(Me.txtNCSNO.Text, Integer)) Then
                    If Not InsSEQTRN(1) Then
                        Using frm As New frmMSGBOX01("顧客番号の更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        _blnFormClose = False
                        Exit Sub
                    End If

                    If Not ChkMANNOSEQ(1) Then
                        Using frm As New frmMSGBOX01("顧客番号又はカード番号は既に登録されています。" & vbCrLf & "※顧客番号を確認して再作成して下さい。。※", 2)
                            frm.ShowDialog()
                        End Using
                        iDatabase.RollBack()
                        _blnFormClose = False
                        Exit Sub
                    End If
                Else
                    '*** 顧客番号手入力 ***

                    If Not InsSEQTRN(2) Then
                        Using frm As New frmMSGBOX01("顧客番号の更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        _blnFormClose = False
                        Exit Sub
                    End If

                    If GetCSMAST() Then
                        Using frm As New frmMSGBOX01("既に登録済みの顧客番号です。", 3)
                            frm.ShowDialog()
                        End Using
                        iDatabase.RollBack()
                        _blnFormClose = False
                        Exit Sub
                    End If

                    If Not ChkMANNOSEQ(2) Then
                        Using frm As New frmMSGBOX01("顧客番号又はカード番号は既に登録されています。" & vbCrLf & "※顧客番号を確認して再作成して下さい。。※", 2)
                            frm.ShowDialog()
                        End Using
                        iDatabase.RollBack()
                        _blnFormClose = False
                        Exit Sub
                    End If
                End If

                If Not InsRegister() Then
                    Using frm As New frmMSGBOX01("顧客番号の更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    _blnFormClose = False
                    Exit Sub
                End If

                Using frm As New frmREQUESTCARD(dcICR700)
                    'カード初期化処理
                    frm.COMMAND = frmREQUESTCARD.Command_Type.CARDINIT
                    frm.ShowDialog()
                    'キャンセル押下
                    If frm.CANCEL Then
                        iDatabase.RollBack()
                        Exit Sub
                    End If
                    'エラー
                    blnERRFLG = frm.ERRFLG
                End Using

                If blnERRFLG Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                iDatabase.Commit()
            Catch ex As Exception
                iDatabase.RollBack()
            End Try

            '顧客番号取得
            If Not GetMANNOSEQ() Then
                Using frm As New frmMSGBOX01("顧客番号の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

            dcICR700.Init()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.btnCard.Enabled = True

            If _blnFormClose Then
                Me.Close()
            Else
                Dim strNCSNO As String = Me.txtNCSNO.Text
                '顧客番号取得
                If Not GetMANNOSEQ() Then
                    Using frm As New frmMSGBOX01("顧客番号の取得に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
                If CType(strNCSNO, Integer) < 20000 Then
                    Me.txtNCSNO.Text = strNCSNO
                End If
                _blnFormClose = True
            End If
        End Try
    End Sub

    ''' <summary>
    ''' F3一覧ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func3()
        Dim intNCSNO As Integer = 0
        Try
            Using frm As New frmLESSCHIST01(iDatabase)
                frm.ShowDialog()
            End Using

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
            Me.tspFunc10.Enabled = False
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            '顧客番号
            Me.txtNCSNO.Text = String.Empty
            '氏名
            Me.txtCCSNAME.Text = String.Empty
            '氏名カナ
            Me.txtCCSKANA.Text = String.Empty
            '会員期限
            Me.txtDMEMBER.Text = String.Empty


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" WHERE NKBNO <= " & UIUtility.SYSTEM.MAXNKBNO)
            sql.Append(" ORDER BY KBMAST ")

            _dtKBMAST = iDatabase.ExecuteRead(sql.ToString())

            If _dtKBMAST.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.cmbNCSRANK.DataSource = _dtKBMAST
            Me.cmbNCSRANK.ValueMember = "NKBNO"
            Me.cmbNCSRANK.DisplayMember = "CKBNAME"
            Me.cmbNCSRANK.SelectedValue = 1

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 顧客番号登録済みかどうかチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkMANNOSEQ(ByVal intKBN As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" (MANNOSEQ) AS MANNO")
            sql.Append(",(DCARDSEQ) AS CARDNO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            If intKBN.Equals(1) Then
                If Not Me.txtNCSNO.Text.Equals(resultDt.Rows(0).Item("MANNO").ToString.PadLeft(8, "0"c)) Then
                    Return False
                End If
            Else
                '*** 顧客番号手入力 ***
                If Not Me.lblCARDNO.Text.Equals(resultDt.Rows(0).Item("CARDNO").ToString.PadLeft(8, "0"c)) Then
                    Return False
                End If
            End If



            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 顧客番号取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetMANNOSEQ() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" (MANNOSEQ + 1) AS MANNO")
            sql.Append(",(DCARDSEQ + 1) AS CARDNO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.txtNCSNO.Text = resultDt.Rows(0).Item("MANNO").ToString.PadLeft(8, "0"c)
            Me.lblCARDNO.Text = resultDt.Rows(0).Item("CARDNO").ToString.PadLeft(8, "0"c)

            _intMAXNCSNO = CType(resultDt.Rows(0).Item("MANNO").ToString.PadLeft(8, "0"c), Integer)

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 顧客情報確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS TO_DBIRTH")  '誕生日
            sql.Append(",TO_CHAR(A.DENTRY,'YYYY/MM/DD') AS TO_DENTRY") '会員登録日
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYY/MM/DD') AS TO_DMEMBER") '会員期限
            sql.Append(",B.ZANKN")
            sql.Append(",B.PREZANKN")
            sql.Append(",C.SRTPO")
            sql.Append(",D.SCLKBN") 'スクール生区分
            sql.Append(",E.CKBLIMIT")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN MANMST D ON D.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS E ON E.NKBNO = A.NCSRANK")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(Me.txtNCSNO.Text, Integer))

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 顧客番号更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsSEQTRN(ByVal intKBN As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            '顧客番号カウントアップ
            sql.Clear()
            sql.Append("UPDATE SEQTRN SET")
            If intKBN.Equals(1) Then
                sql.Append(" MANNOSEQ = MANNOSEQ + 1")
                sql.Append(",DCARDSEQ = DCARDSEQ + 1")
            Else
                '*** 顧客番号手入力 ***'
                sql.Append(" DCARDSEQ = DCARDSEQ + 1")
            End If

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsRegister() As Boolean
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try

            strSQL1 &= "INSERT INTO CSMAST("
            strSQL2 &= " VALUES("
            '顧客番号
            strSQL1 &= "NCSNO,"
            strSQL2 &= CType(Me.txtNCSNO.Text, Integer) & ","
            'カード番号
            strSQL1 &= "NCARDID,"
            strSQL2 &= CType(Me.lblCARDNO.Text, Integer) & ","
            '顧客名
            strSQL1 &= "CCSNAME,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCCSNAME.Text) & ","
            '顧客名(ｶﾅ)
            strSQL1 &= "CCSKANA,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCCSKANA.Text) & ","
            '顧客種別
            strSQL1 &= "NCSRANK,"
            strSQL2 &= Me.cmbNCSRANK.SelectedIndex + 1 & ","
            '性別
            strSQL1 &= "NSEX,"
            strSQL2 &= "1,"
            'カードコメント
            strSQL1 &= "MANINFO,"
            strSQL2 &= "NULL,"
            '郵便番号
            strSQL1 &= "NZIP,"
            strSQL2 &= "NULL,"
            '住所１
            strSQL1 &= "CADDRESS1,"
            strSQL2 &= "NULL,"
            '住所２
            strSQL1 &= "CADDRESS2,"
            strSQL2 &= "NULL,"
            '電話番号
            strSQL1 &= "CTELEPHONE,"
            strSQL2 &= "NULL,"
            'FAX番号
            strSQL1 &= "CFAX,"
            strSQL2 &= "NULL,"
            '携帯電話番号
            strSQL1 &= "CPOTABLENUM,"
            strSQL2 &= "NULL,"
            '会員状態
            strSQL1 &= "NMRKBN,"
            strSQL2 &= "0,"
            '誕生日
            strSQL1 &= "DBIRTH,"
            strSQL2 &= "NULL,"
            '会員期限
            strSQL1 &= "DMEMBER,"
            strSQL2 &= UIFunction.NullCheck(Me.txtDMEMBER.Text) & ","
            '前回来場日
            strSQL1 &= "ZENENTDATE,"
            strSQL2 &= "NULL,"
            'メールアドレス
            strSQL1 &= "CEMAIL,"
            strSQL2 &= "NULL,"
            '会員メモ
            strSQL1 &= "MANCOMMENT,"
            strSQL2 &= "NULL,"
            'スクール生番号
            strSQL1 &= "DSCLMANNO,"
            strSQL2 &= "NULL,"
            '削除フラグ
            strSQL1 &= "NFLAGDEL,"
            strSQL2 &= "0,"
            'メール配信区分
            strSQL1 &= "CEMAILKBN,"
            strSQL2 &= "9,"
            '前回来店日
            strSQL1 &= "ENTDT,"
            strSQL2 &= "NULL,"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '更新日
            strSQL1 &= "DUPDATE,"
            strSQL2 &= "NOW(),"
            '会員登録日
            strSQL1 &= "DENTRY,"
            strSQL2 &= "NOW(),"
            '備考１
            strSQL1 &= "BIKO1,"
            strSQL2 &= "NULL,"
            '備考２
            strSQL1 &= "BIKO2,"
            strSQL2 &= "NULL,"
            '備考３
            strSQL1 &= "BIKO3,"
            strSQL2 &= "NULL,"
            '総来場回数
            strSQL1 &= "ENTCNT,"
            strSQL2 &= "0,"
            '左打ち区分
            strSQL1 &= "DLEFTKBN,"
            strSQL2 &= "0,"
            '月間来場回数
            strSQL1 &= "ENTCNT2,"
            strSQL2 &= "0,"
            'カード期限
            strSQL1 &= "CARDLIMIT,"
            strSQL2 &= "NULL,"
            'カード管理番号
            strSQL1 &= "CARDADMINNO,"
            strSQL2 &= "NULL,"
            '協力金番号
            strSQL1 &= "KRKNO,"
            strSQL2 &= "0,"
            'ハンディキャップ
            strSQL1 &= "HANDICAP)"
            strSQL2 &= "0)"

            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
                Return False
            End If

            '金額サマリ登録
            strSQL1 = "INSERT INTO KINSMA VALUES("
            strSQL1 &= "'" & Me.txtNCSNO.Text & "',"
            strSQL1 &= "NULL,"
            strSQL1 &= "0,"
            strSQL1 &= "0,"
            strSQL1 &= "NOW(),"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If
            'ポイントサマリ登録
            strSQL1 = "INSERT INTO DPOINTSMA VALUES("
            strSQL1 &= "'" & Me.txtNCSNO.Text & "',"
            strSQL1 &= "NULL,"
            strSQL1 &= "0,"
            strSQL1 &= "NOW(),"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If
            '新規カード発行トラン登録
            strSQL1 = "INSERT INTO NEWCARDTRN VALUES("
            strSQL1 &= "'" & Now.ToString("yyyyMMdd") & "',"
            strSQL1 &= CType(Me.lblCARDNO.Text, Integer) & ","
            strSQL1 &= "'" & Me.txtNCSNO.Text & "',"
            strSQL1 &= "NULL,"
            strSQL1 &= "NULL,"
            strSQL1 &= "NOW(),"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try
    End Function

#End Region


End Class
