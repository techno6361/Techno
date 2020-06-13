Imports TECHNO.DataBase

Public Class frmNKNDISP02

#Region "▼宣言部"
    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "NKNDISP02"
    ''' <summary>
    ''' カード挿入済み確認フラグ【True】挿入済み
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnCardFLG As Boolean = False

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

            MyBase.l_Title_FormName = "ｻｰﾋﾞｽ入金画面"

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

            MyBase.l_Title_FormName = "ｻｰﾋﾞｽ入金画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700

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

            MyBase.l_Title_FormName = "ｻｰﾋﾞｽ入金画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700

            _blnCardFLG = CardFLG
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼イベント定義"

    Private Sub frmNKNDISP02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()



            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '入金マスタ情報取得
            GetNKNMST()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' レシート再発行ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRePrint.Click
        Dim strDENNO As String
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【入金】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.SNYUKIN
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.PRINT
                frm.ShowDialog()

                If frm.CANCEL Then Exit Sub

                '伝票番号
                strDENNO = frm.DENNO
            End Using



            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" A.*")
            sql.Append(",B.CCSNAME")
            sql.Append(" FROM")
            sql.Append(" DUDNTRN AS A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) =  A.MANNO")
            sql.Append(" WHERE")
            sql.Append(" A.UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND A.UDNNO = " & CType(strDENNO, Integer))

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Using frm As New frmMSGBOX01("売上情報がありません。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
                Exit Sub
            End If

            Dim dtGOODS As DataTable ' 売上商品一覧
            dtGOODS = New DataTable("GOODS")
            dtGOODS.Columns.Add("GDSNAME", GetType(String))
            dtGOODS.Columns.Add("GDSCOUNT", GetType(String))
            dtGOODS.Columns.Add("GDSTAX", GetType(String))
            dtGOODS.Columns.Add("GDSKIN", GetType(String))
            dtGOODS.Columns.Add("CPAYKBN", GetType(String))

            Dim dr As DataRow
            dr = dtGOODS.NewRow
            dr("GDSNAME") = "カードサービス入金"
            dr("GDSCOUNT") = 0
            dr("GDSTAX") = 0
            dr("GDSKIN") = CType(resultDt.Rows(0).Item("UDNBKN").ToString, Integer).ToString("#,##0")
            dr("CPAYKBN") = "99"
            dtGOODS.Rows.Add(dr)

            '【レシート印刷】
            Dim rePrint As New TMT90.Receipt


            rePrint.intDatKbn = CType(resultDt.Rows(0).Item("DATKB").ToString, Integer)

            rePrint.intGetPremKn = CType(resultDt.Rows(0).Item("UDNBKN").ToString, Integer)
            rePrint.intGetPoint = CType(resultDt.Rows(0).Item("POINT").ToString, Integer)

            Dim intZANKINGAKU As Integer = 0
            Dim intZANPOINT As Integer = 0

            UIFunction.GetTRNZANKIN(intZANKINGAKU, intZANPOINT, Now.ToString("yyyyMMdd"), CType(strDENNO, Integer), iDatabase)

            rePrint.intzankingaku = intZANKINGAKU
            rePrint.intzanpoint = intZANPOINT

            rePrint.strManno = resultDt.Rows(0).Item("MANNO").ToString
            rePrint.strccsname = resultDt.Rows(0).Item("CCSNAME").ToString

            rePrint.intPrintKbn = 1
            rePrint.strDENNO = strDENNO
            rePrint.insDTTM = CType(resultDt.Rows(0).Item("INSDTMSTR").ToString, DateTime)
            rePrint.dtGoods = dtGOODS
            rePrint.intGokei = 0
            rePrint.intDeposit = 0
            rePrint.intChange = 0
            rePrint.strHostName = resultDt.Rows(0).Item("HOSTNAME").ToString
            rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
            rePrint.RePrint(False, String.Empty)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' ｻｰﾋﾞｽ入金取消ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCANCEL.Click
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intUDNKN As Integer = 0             '入金額
        Dim intPREMKN As Integer = 0            'プレミアム
        Dim intPOINT As Integer = 0             'ポイント
        Dim blnCANCEL As Boolean = False        '戻るが押されたかどうか
        Dim strDENNO As String = String.Empty   '伝票番号
        Dim intUDNBKN As Integer = 0            '合計金額
        Dim intKINGAKU As Integer = 0
        Dim intZANKN As Integer = 0
        Dim intPREZANKN As Integer = 0
        Dim intV31POINT As Integer = 0
        Dim blnUpdDATKB As Boolean = False          '【True】伝票のDATKBを1に戻す

        Try
            'カード読み込み
            If Not _blnCardFLG Then
                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then Exit Sub
                    If frm.ERRFLG Then Exit Sub
                End Using
            End If

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
                    Exit Sub
                End Using
            End If



            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【入金】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.SNYUKIN
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.CANCEL
                '顧客番号
                frm.MANNO = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                '氏名
                frm.CCSNAME = dtCSMAST.Rows(0).Item("CCSNAME").ToString
                frm.ShowDialog()


                '伝票番号
                strDENNO = frm.DENNO
                'キャンセル
                blnCANCEL = frm.CANCEL
                'プレミアム
                intPREMKN = frm.UDNKN
            End Using

            If blnCANCEL Then
                'カード排出
                If _blnCardFLG Then
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.EJECTSTOP = True
                        frm.ShowDialog()
                    End Using
                Else
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                End If
            Else
                '【取消処理】

                '伝票取消が完了しているのでここからエラーが発生したら伝票取消を戻さないといけない
                blnUpdDATKB = True

                intKINGAKU = CType(dcICR700.PREZANKN, Integer) - intPREMKN

                If intKINGAKU < 0 Then
                    Using frm As New frmMSGBOX01("残高不足の為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    If _blnCardFLG Then
                        Using frm As New frmREQUESTCARD(dcICR700)
                            frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                            frm.EJECTSTOP = True
                            frm.ShowDialog()
                        End Using
                        Me.Close()
                    Else
                        Using frm As New frmREQUESTCARD(dcICR700)
                            frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                            frm.ShowDialog()
                        End Using
                    End If
                    Exit Sub
                ElseIf intKINGAKU > UIUtility.SYSTEM.ZANMAX Then
                    Using frm As New frmMSGBOX01("入金限度額を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出
                    If _blnCardFLG Then
                        Using frm As New frmREQUESTCARD(dcICR700)
                            frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                            frm.EJECTSTOP = True
                            frm.ShowDialog()
                        End Using
                        Me.Close()
                    Else
                        Using frm As New frmREQUESTCARD(dcICR700)
                            frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                            frm.ShowDialog()
                        End Using
                    End If
                    Exit Sub
                End If

                intZANKN = CType(dcICR700.ZANKN, Integer)
                intPREZANKN = CType(dcICR700.PREZANKN, Integer) - intPREMKN
                intV31POINT = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)

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
                dcICR700.KINGAKU_WR = (intZANKN + intPREZANKN).ToString.PadLeft(5, "0"c)
                '予備
                dcICR700.YOBI_WR = dcICR700.YOBI

                '【V31RW書き込み情報セット】


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

                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then
                        Exit Sub
                    End If
                    If frm.ERRFLG Then
                        Exit Sub
                    End If
                End Using
                If Not dcICR700.NCSNO.Equals(dtCSMAST.Rows(0).Item("NCARDID").ToString.PadLeft(8, "0"c)) Then
                    Using frm As New frmMSGBOX01("カード情報が異なります。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                '【データベース更新処理】

                Do
                    'トランザクション開始
                    iDatabase.BeginTransaction()

                    '【金額サマリ】
                    sql.Clear()
                    sql.Append("UPDATE KINSMA SET")
                    sql.Append(" PREZANKN = " & intPREZANKN)
                    sql.Append(",UPDDTM = NOW()")
                    sql.Append(" WHERE")
                    sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If

                    Exit Do
                Loop

 

                '【カード書き込み】
                Dim blnERRFLG As Boolean = False
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                    If _blnCardFLG Then
                        frm.EJECTSTOP = True
                    End If
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

                'コミット
                iDatabase.Commit()

                'ここまできたら戻さなくてＯＫ
                blnUpdDATKB = False
            End If

            If _blnCardFLG Then
                Me.Close()
            End If

        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If blnUpdDATKB Then
                '伝票情報を戻す
                UIFunction.UpdBackDATKB(4, CType(strDENNO, Integer), iDatabase)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 入金ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNNM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNKN01_001.Click, btnNKNKN02_001.Click, btnNKNKN03_001.Click, btnNKNKN04_001.Click, btnNKNKN05_001.Click, btnNKNKN06_001.Click _
                                                                                    , btnNKNKN01_002.Click, btnNKNKN02_002.Click, btnNKNKN03_002.Click, btnNKNKN04_002.Click, btnNKNKN05_002.Click, btnNKNKN06_002.Click _
                                                                                    , btnNKNKN01_003.Click, btnNKNKN02_003.Click, btnNKNKN03_003.Click, btnNKNKN04_003.Click, btnNKNKN05_003.Click, btnNKNKN06_003.Click
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
                    Exit Sub
                End If
            End If
            '*** 【スタッフ確認】 ***'

            Dim btnNKNKN As Button
            btnNKNKN = CType(sender, Button)

            '入金額
            Dim intNKNKN As Integer = CType(btnNKNKN.Tag, Integer)

            If Not _blnCardFLG Then
                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then Exit Sub
                    If frm.ERRFLG Then Exit Sub
                End Using
            End If

            'ｻｰﾋﾞｽ入金処理
            Dim strMsg As String = String.Empty
            Dim blnCARD As Boolean = True
            If Not UpdNKNMST(intNKNKN, blnCARD, strMsg) Then
                Using frm As New frmMSGBOX01(strMsg, 3)
                    frm.ShowDialog()
                End Using
                If blnCARD Then
                    If Not _blnCardFLG Then
                        Using frm As New frmREQUESTCARD(dcICR700)
                            'カード排出処理
                            frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                            frm.ShowDialog()
                        End Using
                    End If
                End If
                Exit Sub
            End If

            If _blnCardFLG Then
                Me.Close()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 加算・減算ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click, btnDown.Click
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
                    Exit Sub
                End If
            End If
            '*** 【スタッフ確認】 ***'

            Dim btnUpDown As Button
            btnUpDown = CType(sender, Button)

            'プレミアム
            Dim intPREMKN As Integer = 0

            Select Case CType(btnUpDown.Tag, Integer)
                Case 1
                    '【加算】

                    intPREMKN = CType(Me.txtPREMKN01_004.Text, Integer)

                Case 2
                    '【減算】

                    intPREMKN = CType("-" & Me.txtPREMKN01_004.Text, Integer)

            End Select

            If CType(intPREMKN.ToString.Replace("-", String.Empty), Integer).Equals(0) Then
                Using frm As New frmMSGBOX01("ｻｰﾋﾞｽ入金額を指定してください。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
                Exit Sub
            End If

            'カード読み込み
            If Not _blnCardFLG Then
                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then Exit Sub
                End Using
            End If

            'レジ精算画面表示
            Dim strMsg As String = String.Empty
            Dim blnCARD As Boolean = True
            If Not UpdNKNMST(intPREMKN, blnCARD, strMsg) Then
                Using frm As New frmMSGBOX01(strMsg, 3)
                    frm.ShowDialog()
                End Using
                If blnCARD Then
                    If Not _blnCardFLG Then
                        Using frm As New frmREQUESTCARD(dcICR700)
                            'カード排出処理
                            frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                            frm.ShowDialog()
                        End Using
                    End If
                End If
                Exit Sub
            End If

            If _blnCardFLG Then
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼プロパティ"

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

            '入金ボタン
            Me.btnNKNKN01_001.Visible = False
            Me.btnNKNKN02_001.Visible = False
            Me.btnNKNKN03_001.Visible = False
            Me.btnNKNKN04_001.Visible = False
            Me.btnNKNKN05_001.Visible = False
            Me.btnNKNKN06_001.Visible = False

            Me.btnNKNKN01_002.Visible = False
            Me.btnNKNKN02_002.Visible = False
            Me.btnNKNKN03_002.Visible = False
            Me.btnNKNKN04_002.Visible = False
            Me.btnNKNKN05_002.Visible = False
            Me.btnNKNKN06_002.Visible = False

            Me.btnNKNKN01_003.Visible = False
            Me.btnNKNKN02_003.Visible = False
            Me.btnNKNKN03_003.Visible = False
            Me.btnNKNKN04_003.Visible = False
            Me.btnNKNKN05_003.Visible = False
            Me.btnNKNKN06_003.Visible = False

            'プレミアム手入力
            Me.txtPREMKN01_004.Text = "0"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 商品分類マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetHINMTB() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTB")
            sql.Append(" WHERE ")
            sql.Append(" BMNCD = '002'")
            sql.Append(" AND BUNCDA = '004'")
            sql.Append(" ORDER BY BUNCDB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Me.tabNKN.TabPages(i).Text = resultDt.Rows(i).Item("BUNNMB").ToString
            Next

            Me.tabNKN.TabPages(3).Text = "手入力"


            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 入金マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNKNMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNMST")
            sql.Append(" WHERE ")
            sql.Append(" STSFLG = '9'")
            sql.Append(" ORDER BY NKNKBN,SEQNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim dr() As DataRow

            '//【タグ区分001】//
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.btnNKNKN01_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN01_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN01_001.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.btnNKNKN02_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN02_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN02_001.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.btnNKNKN03_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN03_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN03_001.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.btnNKNKN04_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN04_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN04_001.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.btnNKNKN05_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN05_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN05_001.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.btnNKNKN06_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN06_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN06_001.Visible = True
            End If
            '//【タグ区分002】//
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.btnNKNKN01_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN01_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN01_002.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.btnNKNKN02_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN02_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN02_002.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.btnNKNKN03_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN03_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN03_002.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.btnNKNKN04_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN04_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN04_002.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.btnNKNKN05_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN05_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN05_002.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.btnNKNKN06_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN06_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN06_002.Visible = True
            End If
            '//【タグ区分003】//
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.btnNKNKN01_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN01_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN01_003.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.btnNKNKN02_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN02_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN02_003.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.btnNKNKN03_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN03_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN03_003.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.btnNKNKN04_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN04_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN04_003.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.btnNKNKN05_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN05_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN05_003.Visible = True
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.btnNKNKN06_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN06_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN06_003.Visible = True
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' ｻｰﾋﾞｽ入金処理
    ''' </summary>
    ''' <param name="intPREMKN"></param>
    ''' <param name="strMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdNKNMST(ByVal intPREMKN As Integer, ByRef blnCARD As Boolean, ByRef strMsg As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try

            '入金限度額チェック
            If (CType(dcICR700.KINGAKU, Integer) + intPREMKN) > UIUtility.SYSTEM.ZANMAX Then
                strMsg = "入金限度額を超えています。"
                Return False
            End If
            If CType(dcICR700.PREZANKN, Integer) + intPREMKN < 0 Then
                strMsg = "P)残金がマイナスになります。"
                Return False
            End If

            If dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
                strMsg = "顧客データがありません。"
                If _blnCardFLG Then
                    blnCARD = False
                End If
                Return False
            End If

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
                strMsg = "顧客データがありません。"
                If _blnCardFLG Then
                    blnCARD = False
                End If
                Return False
            End If

            If _blnCardFLG Then
                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then
                        If _blnCardFLG Then
                            blnCARD = False
                        End If
                        Return True
                    End If
                    If frm.ERRFLG Then
                        If _blnCardFLG Then
                            blnCARD = False
                        End If
                        Return False
                    End If
                End Using
                If Not dtCSMAST.Rows(0).Item("NCARDID").ToString.PadLeft(8, "0"c).Equals(dcICR700.NCSNO) Then
                    strMsg = "顧客情報が異なります。"
                    If _blnCardFLG Then
                        blnCARD = False
                    End If
                    Return False
                End If
            End If

            If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                If Not String.IsNullOrEmpty(dtCSMAST.Rows(0).Item("CARDLIMIT").ToString) Then
                    Dim dtCARDLIMIT As DateTime = DateTime.Parse(dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" & dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" & dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(6, 2))
                    ' 入金残高有効期限
                    Dim intPREMLIMIT As Integer = 0
                    Dim strCARDLIMIT As String = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                        intPREMLIMIT = CType("-" & UIUtility.SYSTEM.PREMLIMIT, Integer)
                        dtCARDLIMIT = dtCARDLIMIT.AddMonths(intPREMLIMIT)
                        strCARDLIMIT = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    End If
                    If Now.ToString("yyyyMMdd") > dtCSMAST.Rows(0).Item("CARDLIMIT").ToString Or Now.ToString("yyyy/MM/dd") > strCARDLIMIT Then
                        strMsg = "有効期限が切れています。" & vbCrLf & "※受付画面から期限を確認してください。※"
                        If _blnCardFLG Then
                            blnCARD = False
                        End If
                        Return False
                    End If
                End If
            End If

            '*** 金額計算 ***'

            'プリカ金額
            Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)
            'V31残金
            Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)


            '書き込む前の前回来場セット(ボールトラン更新用)
            Dim strZENENTDATE As String = dcICR700.ZENENTDATE
            If dcICR700.ZENENTDATE.Equals("00000000") Then
                strZENENTDATE = UIUtility.SYSTEM.UPDDAY
            End If
            '********************'

            '【プリカRW書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = dcICR700.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = dcICR700.PASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = dcICR700.SERIALNO
            '種別
            dcICR700.SYUBETU_WR = dcICR700.SYUBETU
            '金額
            dcICR700.KINGAKU_WR = (intKINGAKU + intPREMKN).ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = dcICR700.YOBI

            '【V31RW書き込み情報セット】
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
            '前回来場日
            dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
            '残金額
            dcICR700.ZANKN_WR = intV31ZANKN.ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = (intV31PREZANKN + intPREMKN).ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer).ToString.PadLeft(5, "0"c)
            '入場区分
            dcICR700.ENTKBN_WR = dcICR700.ENTKBN
            'ボール単価
            dcICR700.BALLKIN_WR = dcICR700.BALLKIN
            '打席番号
            dcICR700.SEATNO_WR = dcICR700.SEATNO

            '**************************'
            '***【データベース更新】***'
            '**************************'



            '処理日時
            Dim dtmInsDt As DateTime = Now

            'トランザクション開始
            iDatabase.BeginTransaction()

            '【金額サマリ】
            sql.Clear()
            sql.Append("UPDATE KINSMA SET")
            sql.Append(" ZANKN = " & intV31ZANKN)
            sql.Append(",PREZANKN = " & (intV31PREZANKN + intPREMKN))
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                strMsg = "金額サマリの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【カード有効期限】
            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                Dim dtCARDLIMIT As DateTime = Now
                sql.Append(" CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
            Else
                sql.Append(" CARDLIMIT = NULL")
            End If
            sql.Append(",DUPDATE = NOW()")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                strMsg = "有効期限の更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【伝票番号】
            sql.Clear()
            sql.Append("UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                strMsg = "伝票番号の更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '伝票番号取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" DENNOSEQ AS DENNO")
            sql.Append(" FROM SEQTRN")

            Dim dtSEQTRN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dtSEQTRN.Rows.Count.Equals(0) Then
                strMsg = "伝票番号の取得に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If

            '【売上トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO DUDNTRN("
            strSQL2 &= " VALUES("
            '削除区分
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '売上日付
            strSQL1 &= "UDNDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '売上番号(伝票番号)
            strSQL1 &= "UDNNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '部門コード【001】スクール【002】ベンダー
            strSQL1 &= "BMNCD,"
            strSQL2 &= "'002',"
            '分類コード１【004】ｻｰﾋﾞｽ入金
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'004',"
            '分類コード２ 入金区分(入金マスタの区分)
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'" & (Me.tabNKN.SelectedIndex + 1).ToString.PadLeft(3, "0"c) & "',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分 入金区分(入金マスタの区分)
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'" & (Me.tabNKN.SelectedIndex + 1).ToString.PadLeft(3, "0"c) & "',"
            '売上金額(入金額)
            strSQL1 &= "UDNKN,"
            strSQL2 &= intPREMKN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '顧客種別
            strSQL1 &= "KSBKB,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSRANK").ToString & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= intPREMKN & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intPREMKN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= "0,"
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= "0,"
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= "0,"
            'おつり
            strSQL1 &= "TURIKN,"
            strSQL2 &= "0,"
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "NULL,"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "NULL,"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'1'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'9',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "ｻｰﾋﾞｽ入金 " & intPREMKN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= "0,"
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= UIFunction.NullCheck(_strSTFCODE) & ","
            'スタッフ名
            strSQL1 &= "STFNAME,"
            strSQL2 &= UIFunction.NullCheck(_strSTFNAME) & ","
            ''カード期限
            strSQL1 &= "CARDLIMIT)"
            strSQL2 &= "NULL)"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "売上トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【伝票トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO DENTRA("
            strSQL2 &= " VALUES("
            '削除区分【1】使用中【9】削除
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '伝票日付
            strSQL1 &= "UDNDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '伝票番号
            strSQL1 &= "UDNNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '伝票行番号
            strSQL1 &= "LINNO,"
            strSQL2 &= "1,"
            '部門コード【001】スクール【002】ベンダー
            strSQL1 &= "BMNCD,"
            strSQL2 &= "'002',"
            '分類コード１【004】ｻｰﾋﾞｽ入金
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'004',"
            '分類コード２ 入金区分(入金マスタの区分)
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'" & (Me.tabNKN.SelectedIndex + 1).ToString.PadLeft(3, "0"c) & "',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'" & (Me.tabNKN.SelectedIndex + 1).ToString.PadLeft(3, "0"c) & "',"
            '売上名
            strSQL1 &= "TKTNMA,"
            strSQL2 &= "'" & "ｻｰﾋﾞｽ入金 " & intPREMKN.ToString("#,##0") & "円" & "',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= intPREMKN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '顧客種別
            strSQL1 &= "KSBKB,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSRANK").ToString & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= intPREMKN & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intPREMKN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= "0,"
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= "0,"
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "NULL,"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "NULL,"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'1'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'9',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "ｻｰﾋﾞｽ入金 " & intPREMKN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= "0)"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "伝票トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【入金トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO NKNTRN("
            strSQL2 &= " VALUES("
            '削除区分【1】使用中【9】削除
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '伝票日付
            strSQL1 &= "DENDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '伝票番号
            strSQL1 &= "DENNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '入金名
            strSQL1 &= "NKNNM,"
            strSQL2 &= "'" & "ｻｰﾋﾞｽ入金 " & intPREMKN.ToString("#,##0") & "円" & "',"
            '入金額
            strSQL1 &= "NKNKN,"
            strSQL2 &= intPREMKN & ","
            '税抜売上金額
            strSQL1 &= "NKNAKN,"
            strSQL2 &= intPREMKN & ","
            '税込売上金額
            strSQL1 &= "NKNBKN,"
            strSQL2 &= intPREMKN & ","
            '消費税
            strSQL1 &= "NZEIKN,"
            strSQL2 &= "0,"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= "0,"
            '用途不明
            strSQL1 &= "PRERT,"
            strSQL2 &= "NULL,"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= "0,"
            '残金入金前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intV31ZANKN & ","
            '残金入金後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intV31ZANKN & ","
            'P)残金入金前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intV31PREZANKN & ","
            'P)残金入金後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= intV31PREZANKN + intPREMKN & ","
            '残ポイント入金前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) & ","
            '残ポイント入金後
            strSQL1 &= "ZANBPO,"
            strSQL2 &= CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'0',"
            '種別フラグ
            strSQL1 &= "STSFLG,"
            strSQL2 &= "'9',"
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "入金トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If

            'ここまで来たらエラー時カード排出不要の為 false 
            blnCARD = False

            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARD(dcICR700)
                'カードライト
                frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                If _blnCardFLG Then
                    frm.EJECTSTOP = True
                End If
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                iDatabase.RollBack()
                strMsg = "カードの書き込みに失敗しました。"
                Return False
            End If

            'コミット
            iDatabase.Commit()


            If UIUtility.SYSTEM.RECEIPTFLG.Equals(1) Then
                Dim dtGOODS As DataTable ' 売上商品一覧
                dtGOODS = New DataTable("GOODS")
                dtGOODS.Columns.Add("GDSNAME", GetType(String))
                dtGOODS.Columns.Add("GDSCOUNT", GetType(String))
                dtGOODS.Columns.Add("GDSTAX", GetType(String))
                dtGOODS.Columns.Add("GDSKIN", GetType(String))
                dtGOODS.Columns.Add("CPAYKBN", GetType(String))

                Dim dr As DataRow
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "カードサービス入金"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = intPREMKN.ToString("#,##0")
                dr("CPAYKBN") = 99
                dtGOODS.Rows.Add(dr)

                '【レシート印刷】
                Dim rePrint As New TMT90.Receipt

                rePrint.intzankingaku = intV31ZANKN + intV31PREZANKN + intPREMKN
                rePrint.intzanpoint = CType(dcICR700.POINT, Integer)

                rePrint.strManno = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                rePrint.strccsname = dtCSMAST.Rows(0).Item("CCSNAME").ToString

                rePrint.intPrintKbn = 1
                rePrint.intGetPremKn = intPREMKN
                rePrint.intGetPoint = 0
                rePrint.strDENNO = dtSEQTRN.Rows(0).Item("DENNO").ToString.PadLeft(4, "0"c)
                rePrint.insDTTM = dtmInsDt
                rePrint.dtGoods = dtGOODS
                rePrint.intGokei = 0
                rePrint.intDeposit = 0
                rePrint.intChange = 0
                rePrint.strHostName = Net.Dns.GetHostName
                rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
                rePrint.RePrint(False, String.Empty)
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

