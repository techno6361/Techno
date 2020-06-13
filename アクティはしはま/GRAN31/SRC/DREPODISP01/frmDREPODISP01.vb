Imports TECHNO.DataBase

Public Class frmDREPODISP01

#Region "▼宣言部"
    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "DREPODISP01"

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

            MyBase.l_Title_FormName = "ﾎﾟｲﾝﾄ還元画面"

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

            MyBase.l_Title_FormName = "ﾎﾟｲﾝﾄ還元画面"

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

            MyBase.l_Title_FormName = "ﾎﾟｲﾝﾄ還元画面"

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

    Private Sub frmDREPODISP01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()



            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'ﾎﾟｲﾝﾄ還元マスタ情報取得
            GetDREPOMST()
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
        Dim strDENNO As String = String.Empty
        Dim strTKTKBN As String = String.Empty
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【ﾎﾟｲﾝﾄ還元】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.POINT
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.PRINT
                frm.ShowDialog()

                If frm.CANCEL Then Exit Sub

                '伝票番号
                strDENNO = frm.DENNO
                'ﾁｹｯﾄ区分
                strTKTKBN = frm.TKTKBN
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

            Dim intPREMKN As Integer = CType(resultDt.Rows(0).Item("UDNKN").ToString, Integer)
            Dim dr As DataRow
            dr = dtGOODS.NewRow
            If strTKTKBN.Equals("0") Then
                '【ﾌﾟﾚﾐｱﾑ】
                dr("GDSNAME") = "ﾌﾟﾚﾐｱﾑ"
            ElseIf strTKTKBN.Equals("1") Then
                '【ﾁｹｯﾄ】
                dr("GDSNAME") = "ﾁｹｯﾄ"
                dr("GDSCOUNT") = intPREMKN.ToString("#,##0") & "枚"
                intPREMKN = 0
            Else
                dr("GDSNAME") = "ﾎﾟｲﾝﾄ加算・減算"
                intPREMKN = 0
            End If
            dr("GDSTAX") = 0
            dr("GDSKIN") = intPREMKN
            dtGOODS.Rows.Add(dr)

            '【レシート印刷】
            Dim rePrint As New TMT90.Receipt

            Dim intZANKINGAKU As Integer = 0
            Dim intZANPOINT As Integer = 0

            UIFunction.GetTRNZANKIN2(intZANKINGAKU, intZANPOINT, Now.ToString("yyyyMMdd"), CType(strDENNO, Integer), iDatabase)

            rePrint.intzankingaku = intZANKINGAKU
            rePrint.intzanpoint = intZANPOINT

            rePrint.strManno = resultDt.Rows(0).Item("MANNO").ToString
            rePrint.strccsname = resultDt.Rows(0).Item("CCSNAME").ToString

            rePrint.intGetPremKn = intPREMKN
            rePrint.intGetPoint = CType(resultDt.Rows(0).Item("POINT").ToString, Integer)

            rePrint.intPrintKbn = 2

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
        Dim intV31ZANKN As Integer = 0
        Dim intV31PREM As Integer = 0
        Dim intV31POINT As Integer = 0
        Dim strTKTKBN As String = String.Empty
        Dim blnUpdDATKB As Boolean = False          '【True】伝票のDATKBを1に戻す

        Try
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
                    Exit Sub
                End Using
            End If



            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【ポイント還元】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.POINT
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.CANCEL
                '顧客番号
                frm.MANNO = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                frm.ShowDialog()

                'キャンセル
                blnCANCEL = frm.CANCEL
                '還元情報
                intPREMKN = frm.PREMKN  'ﾌﾟﾚﾐｱﾑ/ﾁｹｯﾄ
                strTKTKBN = frm.TKTKBN  '【0】ﾌﾟﾚﾐｱﾑ【1】ﾁｹｯﾄ
                intPOINT = frm.POINT    '消費ポイント
                '伝票番号
                strDENNO = frm.DENNO
            End Using

            If blnCANCEL Then
                'カード排出
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                End Using
            Else
                '【取消処理】

                '伝票取消が完了しているのでここからエラーが発生したら伝票取消を戻さないといけない
                blnUpdDATKB = True

                Dim intPOINT2 As Integer = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) - intPOINT

                If intPOINT2 > 99999 Then
                    Using frm As New frmMSGBOX01("ポイントの上限を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                ElseIf intPOINT2 < 0 Then
                    Using frm As New frmMSGBOX01("ポイントがマイナスになる為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                intKINGAKU = CType(dcICR700.KINGAKU, Integer)
                intV31PREM = CType(dcICR700.PREZANKN, Integer)
                intV31POINT = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) - intPOINT
                If strTKTKBN.Equals("0") Then
                    '【ﾌﾟﾚﾐｱﾑ】
                    intV31PREM = CType(dcICR700.PREZANKN, Integer) - intPREMKN
                    If intV31PREM < 0 Then intV31PREM = 0
                    If intV31PREM.Equals(0) Then
                        intKINGAKU -= CType(dcICR700.PREZANKN, Integer)
                    Else
                        intKINGAKU -= intPREMKN
                    End If
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
                dcICR700.ZANKN_WR = dcICR700.ZANKN
                'P残金額
                dcICR700.PREZANKN_WR = intV31PREM.ToString.PadLeft(5, "0"c)
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

                '【データベース更新処理】
                Do
                    'トランザクション開始
                    iDatabase.BeginTransaction()

                    If strTKTKBN.Equals("0") Then
                        '【ﾌﾟﾚﾐｱﾑ】

                        sql.Clear()
                        sql.Append("UPDATE KINSMA SET")
                        sql.Append(" PREZANKN = " & intV31PREM)
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
                    Else
                        '【ﾁｹｯﾄ】
                        sql.Clear()
                        sql.Append("UPDATE TICHETSMA SET")
                        sql.Append(" TICHET = TICHET - " & intPREMKN)
                        sql.Append(",UPDDTM = NOW()")
                        sql.Append(" WHERE")
                        sql.Append(" SCLMANNO = '" & dcICR700.SCLMANNO & "'")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Using frm As New frmMSGBOX01("チケットサマリの更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            Exit Sub
                        End If
                        '【スクール売上トラン】
                        sql.Clear()
                        sql.Append("UPDATE UDNTRN SET")
                        sql.Append(" DATKB = '9'")
                        sql.Append(" WHERE")
                        sql.Append(" UDNNO = " & CType(strDENNO, Integer))
                        sql.Append(" AND UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Using frm As New frmMSGBOX01("スクール売上トランの更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            Exit Sub
                        End If
                    End If

                    '【ポイントサマリ】
                    sql.Clear()
                    sql.Append("UPDATE DPOINTSMA SET")
                    sql.Append(" SRTPO = " & intV31POINT)
                    sql.Append(",UPDDTM = NOW()")
                    sql.Append(" WHERE")
                    sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("ポイントサマリの更新に失敗しました。", 2)
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



        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If blnUpdDATKB Then
                '伝票情報を戻す
                UIFunction.UpdBackDATKB(7, CType(strDENNO, Integer), iDatabase)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 還元ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDREPONM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDREPONM01_001.Click, btnDREPONM02_001.Click, btnDREPONM03_001.Click, btnDREPONM04_001.Click, btnDREPONM05_001.Click, btnDREPONM06_001.Click _
                                                                                    , btnDREPONM01_002.Click, btnDREPONM02_002.Click, btnDREPONM03_002.Click, btnDREPONM04_002.Click, btnDREPONM05_002.Click, btnDREPONM06_002.Click _
                                                                                    , btnDREPONM01_003.Click, btnDREPONM02_003.Click, btnDREPONM03_003.Click, btnDREPONM04_003.Click, btnDREPONM05_003.Click, btnDREPONM06_003.Click
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

            Dim btnDREPONM As Button
            btnDREPONM = CType(sender, Button)

            '区分【0】ﾌﾟﾚﾐｱﾑ【1】ﾁｹｯﾄ
            Dim intTKTKBN As Integer = CType(btnDREPONM.Tag, Integer)
            'ﾌﾟﾚﾐｱﾑ額またはﾁｹｯﾄ数
            Dim intPREMKN As Integer = 0
            '消費ポイント
            Dim intREPOINT As Integer = 0

            Select Case btnDREPONM.Name.ToString
                Case "btnDREPONM01_001" '【還元タグ１】
                    intPREMKN = CType(Me.lblPREMKN01_001.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT01_001.Tag, Integer)
                Case "btnDREPONM02_001"
                    intPREMKN = CType(Me.lblPREMKN02_001.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT02_001.Tag, Integer)
                Case "btnDREPONM03_001"
                    intPREMKN = CType(Me.lblPREMKN03_001.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT03_001.Tag, Integer)
                Case "btnDREPONM04_001"
                    intPREMKN = CType(Me.lblPREMKN04_001.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT04_001.Tag, Integer)
                Case "btnDREPONM05_001"
                    intPREMKN = CType(Me.lblPREMKN05_001.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT05_001.Tag, Integer)
                Case "btnDREPONM06_001"
                    intPREMKN = CType(Me.lblPREMKN06_001.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT06_001.Tag, Integer)
                Case "btnDREPONM01_002" '【還元タグ2】
                    intPREMKN = CType(Me.lblPREMKN01_002.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT01_002.Tag, Integer)
                Case "btnDREPONM02_002"
                    intPREMKN = CType(Me.lblPREMKN02_002.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT02_002.Tag, Integer)
                Case "btnDREPONM03_002"
                    intPREMKN = CType(Me.lblPREMKN03_002.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT03_002.Tag, Integer)
                Case "btnDREPONM04_002"
                    intPREMKN = CType(Me.lblPREMKN04_002.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT04_002.Tag, Integer)
                Case "btnDREPONM05_002"
                    intPREMKN = CType(Me.lblPREMKN05_002.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT05_002.Tag, Integer)
                Case "btnDREPONM06_002"
                    intPREMKN = CType(Me.lblPREMKN06_002.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT06_002.Tag, Integer)
                Case "btnDREPONM01_003" '【還元タグ3】
                    intPREMKN = CType(Me.lblPREMKN01_003.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT01_003.Tag, Integer)
                Case "btnDREPONM02_003"
                    intPREMKN = CType(Me.lblPREMKN02_003.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT02_003.Tag, Integer)
                Case "btnDREPONM03_003"
                    intPREMKN = CType(Me.lblPREMKN03_003.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT03_003.Tag, Integer)
                Case "btnDREPONM04_003"
                    intPREMKN = CType(Me.lblPREMKN04_003.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT04_003.Tag, Integer)
                Case "btnDREPONM05_003"
                    intPREMKN = CType(Me.lblPREMKN05_003.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT05_003.Tag, Integer)
                Case "btnDREPONM06_003"
                    intPREMKN = CType(Me.lblPREMKN06_003.Tag, Integer)
                    intREPOINT = CType(Me.lblREPOINT06_003.Tag, Integer)
            End Select

            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
                If frm.ERRFLG Then Exit Sub
            End Using

            '還元処理
            Dim strMsg As String = String.Empty
            Dim blnCARD As Boolean = True
            If Not UpdREPOINT(intTKTKBN, btnDREPONM.Text, intPREMKN, intREPOINT, blnCARD, strMsg) Then
                Using frm As New frmMSGBOX01(strMsg, 3)
                    frm.ShowDialog()
                End Using
                If blnCARD Then
                    Using frm As New frmREQUESTCARD(dcICR700)
                        'カード排出処理
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                End If
                Exit Sub
            End If




        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 精算ボタン_Click
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

            'ポイント
            Dim intREPOINT As Integer = CType(Me.txtREPOINT01_004.Text, Integer)

            Select Case CType(btnUpDown.Tag, Integer)
                Case 1
                    '【加算】

                    intREPOINT = CType(Me.txtREPOINT01_004.Text, Integer)

                Case 2
                    '【減算】

                    intREPOINT = CType("-" & Me.txtREPOINT01_004.Text, Integer)

            End Select

            If CType(intREPOINT.ToString, Integer).Equals(0) Then
                Using frm As New frmMSGBOX01("ﾎﾟｲﾝﾄを入力してください。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
                Exit Sub
            End If

            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
            End Using

            'レジ精算画面表示
            Dim strMsg As String = String.Empty
            Dim blnCARD As Boolean = True
            If Not UpdREPOINT(2, "ﾎﾟｲﾝﾄ修正", 0, intREPOINT, blnCARD, strMsg) Then
                Using frm As New frmMSGBOX01(strMsg, 3)
                    frm.ShowDialog()
                End Using
                If blnCARD Then
                    Using frm As New frmREQUESTCARD(dcICR700)
                        'カード排出処理
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                End If
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼イベント定義"

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

            '還元ボタン
            Me.btnDREPONM01_001.Visible = False
            Me.btnDREPONM02_001.Visible = False
            Me.btnDREPONM03_001.Visible = False
            Me.btnDREPONM04_001.Visible = False
            Me.btnDREPONM05_001.Visible = False
            Me.btnDREPONM06_001.Visible = False

            Me.lblPREMKN01_001.Visible = False
            Me.lblPREMKN02_001.Visible = False
            Me.lblPREMKN03_001.Visible = False
            Me.lblPREMKN04_001.Visible = False
            Me.lblPREMKN05_001.Visible = False
            Me.lblPREMKN06_001.Visible = False

            Me.lblREPOINT01_001.Visible = False
            Me.lblREPOINT02_001.Visible = False
            Me.lblREPOINT03_001.Visible = False
            Me.lblREPOINT04_001.Visible = False
            Me.lblREPOINT05_001.Visible = False
            Me.lblREPOINT06_001.Visible = False


            Me.btnDREPONM01_002.Visible = False
            Me.btnDREPONM02_002.Visible = False
            Me.btnDREPONM03_002.Visible = False
            Me.btnDREPONM04_002.Visible = False
            Me.btnDREPONM05_002.Visible = False
            Me.btnDREPONM06_002.Visible = False

            Me.lblPREMKN01_002.Visible = False
            Me.lblPREMKN02_002.Visible = False
            Me.lblPREMKN03_002.Visible = False
            Me.lblPREMKN04_002.Visible = False
            Me.lblPREMKN05_002.Visible = False
            Me.lblPREMKN06_002.Visible = False

            Me.lblREPOINT01_002.Visible = False
            Me.lblREPOINT02_002.Visible = False
            Me.lblREPOINT03_002.Visible = False
            Me.lblREPOINT04_002.Visible = False
            Me.lblREPOINT05_002.Visible = False
            Me.lblREPOINT06_002.Visible = False


            Me.btnDREPONM01_003.Visible = False
            Me.btnDREPONM02_003.Visible = False
            Me.btnDREPONM03_003.Visible = False
            Me.btnDREPONM04_003.Visible = False
            Me.btnDREPONM05_003.Visible = False
            Me.btnDREPONM06_003.Visible = False

            Me.lblPREMKN01_003.Visible = False
            Me.lblPREMKN02_003.Visible = False
            Me.lblPREMKN03_003.Visible = False
            Me.lblPREMKN04_003.Visible = False
            Me.lblPREMKN05_003.Visible = False
            Me.lblPREMKN06_003.Visible = False

            Me.lblREPOINT01_003.Visible = False
            Me.lblREPOINT02_003.Visible = False
            Me.lblREPOINT03_003.Visible = False
            Me.lblREPOINT04_003.Visible = False
            Me.lblREPOINT05_003.Visible = False
            Me.lblREPOINT06_003.Visible = False

            '支出手入力
            Me.txtREPOINT01_004.Text = "0"

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
            sql.Append(" AND BUNCDA = '007'")
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
    ''' ﾎﾟｲﾝﾄ還元マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetDREPOMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DREPOMST")
            sql.Append(" WHERE ")
            sql.Append(" REKBN = '01'")
            sql.Append(" ORDER BY RETAGKBN,SEQNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim dr() As DataRow

            '//【タグ区分001】//
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 1")
            If dr.Length > 0 Then
                '還元01
                Me.btnDREPONM01_001.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM01_001.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN01_001.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN01_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN01_001.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN01_001.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT01_001.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT01_001.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM01_001.Visible = True
                Me.lblPREMKN01_001.Visible = True
                Me.lblREPOINT01_001.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 2")
            If dr.Length > 0 Then
                '還元02
                Me.btnDREPONM02_001.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM02_001.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN02_001.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN02_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN02_001.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN02_001.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT02_001.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT02_001.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM02_001.Visible = True
                Me.lblPREMKN02_001.Visible = True
                Me.lblREPOINT02_001.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 3")
            If dr.Length > 0 Then
                '還元03
                Me.btnDREPONM03_001.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM03_001.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN03_001.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN03_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN03_001.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN03_001.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT03_001.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT03_001.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM03_001.Visible = True
                Me.lblPREMKN03_001.Visible = True
                Me.lblREPOINT03_001.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 4")
            If dr.Length > 0 Then
                '還元04
                Me.btnDREPONM04_001.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM04_001.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN04_001.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN04_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN04_001.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN04_001.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT04_001.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT04_001.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM04_001.Visible = True
                Me.lblPREMKN04_001.Visible = True
                Me.lblREPOINT04_001.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 5")
            If dr.Length > 0 Then
                '還元05
                Me.btnDREPONM05_001.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM05_001.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN05_001.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN05_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN05_001.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN05_001.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT05_001.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT05_001.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM05_001.Visible = True
                Me.lblPREMKN05_001.Visible = True
                Me.lblREPOINT05_001.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 6")
            If dr.Length > 0 Then
                '還元06
                Me.btnDREPONM06_001.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM06_001.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN06_001.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN06_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN06_001.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN06_001.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT06_001.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT06_001.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM06_001.Visible = True
                Me.lblPREMKN06_001.Visible = True
                Me.lblREPOINT06_001.Visible = True
            End If
            '//【タグ区分002】//
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 1")
            If dr.Length > 0 Then
                '還元02
                Me.btnDREPONM01_002.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM01_002.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN01_002.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN01_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN01_002.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN01_002.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT01_002.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT01_002.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM01_002.Visible = True
                Me.lblPREMKN01_002.Visible = True
                Me.lblREPOINT01_002.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 2")
            If dr.Length > 0 Then
                '還元02
                Me.btnDREPONM02_002.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM02_002.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN02_002.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN02_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN02_002.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN02_002.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT02_002.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT02_002.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM02_002.Visible = True
                Me.lblPREMKN02_002.Visible = True
                Me.lblREPOINT02_002.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 3")
            If dr.Length > 0 Then
                '還元03
                Me.btnDREPONM03_002.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM03_002.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN03_002.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN03_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN03_002.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN03_002.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT03_002.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT03_002.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM03_002.Visible = True
                Me.lblPREMKN03_002.Visible = True
                Me.lblREPOINT03_002.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 4")
            If dr.Length > 0 Then
                '還元04
                Me.btnDREPONM04_002.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM04_002.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN04_002.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN04_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN04_002.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN04_002.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT04_002.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT04_002.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM04_002.Visible = True
                Me.lblPREMKN04_002.Visible = True
                Me.lblREPOINT04_002.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 5")
            If dr.Length > 0 Then
                '還元05
                Me.btnDREPONM05_002.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM05_002.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN05_002.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN05_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN05_002.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN05_002.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT05_002.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT05_002.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM05_002.Visible = True
                Me.lblPREMKN05_002.Visible = True
                Me.lblREPOINT05_002.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 6")
            If dr.Length > 0 Then
                '還元06
                Me.btnDREPONM06_002.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM06_002.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN06_002.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN06_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN06_002.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN06_002.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT06_002.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT06_002.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM06_002.Visible = True
                Me.lblPREMKN06_002.Visible = True
                Me.lblREPOINT06_002.Visible = True
            End If
            '//【タグ区分003】//
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 1")
            If dr.Length > 0 Then
                '還元02
                Me.btnDREPONM01_003.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM01_003.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN01_003.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN01_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN01_003.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN01_003.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT01_003.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT01_003.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM01_003.Visible = True
                Me.lblPREMKN01_003.Visible = True
                Me.lblREPOINT01_003.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 2")
            If dr.Length > 0 Then
                '還元02
                Me.btnDREPONM02_003.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM02_003.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN02_003.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN02_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN02_003.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN02_003.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT02_003.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT02_003.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM02_003.Visible = True
                Me.lblPREMKN02_003.Visible = True
                Me.lblREPOINT02_003.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 3")
            If dr.Length > 0 Then
                '還元03
                Me.btnDREPONM03_003.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM03_003.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN03_003.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN03_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN03_003.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN03_003.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT03_003.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT03_003.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM03_003.Visible = True
                Me.lblPREMKN03_003.Visible = True
                Me.lblREPOINT03_003.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 4")
            If dr.Length > 0 Then
                '還元04
                Me.btnDREPONM04_003.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM04_003.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN04_003.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN04_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN04_003.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN04_003.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT04_003.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT04_003.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM04_003.Visible = True
                Me.lblPREMKN04_003.Visible = True
                Me.lblREPOINT04_003.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 5")
            If dr.Length > 0 Then
                '還元05
                Me.btnDREPONM05_003.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM05_003.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN05_003.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN05_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN05_003.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN05_003.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT05_003.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT05_003.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM05_003.Visible = True
                Me.lblPREMKN05_003.Visible = True
                Me.lblREPOINT05_003.Visible = True
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 6")
            If dr.Length > 0 Then
                '還元06
                Me.btnDREPONM06_003.Text = dr(0).Item("REPONM").ToString
                Me.btnDREPONM06_003.Tag = CType(dr(0).Item("TKTKBN").ToString, Integer)
                If CType(dr(0).Item("TKTKBN").ToString, Integer).Equals(0) Then
                    '【プレミアム】
                    Me.lblPREMKN06_003.Text = "ﾌﾟﾚﾐｱﾑ " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                    Me.lblPREMKN06_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Else
                    '【レッスンチケット】
                    Me.lblPREMKN06_003.Text = "ﾁｹｯﾄ " & CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0") & "枚"
                    Me.lblPREMKN06_003.Tag = CType(dr(0).Item("TKTSU").ToString, Integer)
                End If
                Me.lblREPOINT06_003.Text = "消費ﾎﾟｲﾝﾄ " & CType(dr(0).Item("REPOINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblREPOINT06_003.Tag = "-" & CType(dr(0).Item("REPOINT").ToString, Integer)

                Me.btnDREPONM06_003.Visible = True
                Me.lblPREMKN06_003.Visible = True
                Me.lblREPOINT06_003.Visible = True
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 還元処理
    ''' </summary>
    ''' <param name="intKBN">【0】ﾌﾟﾚﾐｱﾑ【1】ﾁｹｯﾄ【2】ﾎﾟｲﾝﾄ修正 </param>
    ''' <param name="strREPONM"></param>
    ''' <param name="intPREMKN"></param>
    ''' <param name="intREPOINT"></param>
    ''' <param name="strMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdREPOINT(ByVal intKBN As Integer, ByVal strREPONM As String, ByVal intPREMKN As Integer, ByVal intREPOINT As Integer, ByRef blnCARD As Boolean, ByRef strMsg As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try
            If dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
                strMsg = "顧客データがありません。"
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
            sql.Append(",F.TICHET")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS D ON D.NKBNO = A.NCSRANK")
            sql.Append(" LEFT JOIN MANMST AS E ON E.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN TICHETSMA AS F ON F.SCLMANNO = '" & dcICR700.SCLMANNO & "'")
            sql.Append(" WHERE")
            sql.Append(" NCARDID = " & CType(dcICR700.NCSNO, Integer))

            Dim dtCSMAST As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dtCSMAST.Rows.Count.Equals(0) Then
                strMsg = "顧客データがありません。"
                Return False
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
                        Return False
                    End If
                End If
            End If

            If intREPOINT > 0 Then
                If (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intREPOINT) > 99999 Then
                    strMsg = "ﾎﾟｲﾝﾄ限度数を超えています。"
                    Return False
                End If
            Else
                If CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) < System.Math.Abs(intREPOINT) Then
                    strMsg = "ﾎﾟｲﾝﾄが不足しています。"
                    Return False
                End If
            End If

            '*** 金額計算 ***'

            '使用球数金額
            Dim intUseKINGAKU As Integer = 0
            'プリカ金額
            Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)
            'V31残金
            Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)
            Dim intPayBallZANKN As Integer = 0
            Dim intPayBallPREMKN As Integer = 0
            Select Case dcICR700.SYUBETU
                Case "B", "C", "D"
                    intUseKINGAKU = 0
                    intKINGAKU = (intV31ZANKN + intV31PREZANKN)
                Case Else
                    '【残金整合処理】
                    If Not UIFunction.ZANSEIGO(intV31ZANKN, intV31PREZANKN, CType(dcICR700.KINGAKU, Integer), intUseKINGAKU, intPayBallZANKN, intPayBallPREMKN) Then
                        strMsg = "ｶｰﾄﾞ残金が不正です。" & vbCrLf & "ｶｰﾄﾞ修復又は再発行を行って下さい。"
                        Return False
                    End If
            End Select

            '書き込む前の前回来場セット(ボールトラン更新用)
            Dim strZENENTDATE As String = dcICR700.ZENENTDATE
            If dcICR700.ZENENTDATE.Equals("00000000") Then
                strZENENTDATE = UIUtility.SYSTEM.UPDDAY
            End If
            '********************'

            If intKBN.Equals(0) Or intKBN.Equals(2) Then
                '【ﾌﾟﾚﾐｱﾑ】

                '入金限度額チェック
                If (CType(dcICR700.KINGAKU, Integer) + intPREMKN) > UIUtility.SYSTEM.ZANMAX Then
                    strMsg = "入金限度額を超えています。"
                    Return False
                End If

            Else
                '【ﾁｹｯﾄ】

                If dcICR700.SCLMANNO.Equals("000000") Then
                    strMsg = "スクール生ではありません。"
                    Return False
                End If
            End If


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
            If intKBN.Equals(0) Or intKBN.Equals(2) Then
                '【ﾌﾟﾚﾐｱﾑ】
                dcICR700.KINGAKU_WR = (intKINGAKU + intPREMKN).ToString.PadLeft(5, "0"c)
            Else
                '【ﾁｹｯﾄ】
                dcICR700.KINGAKU_WR = intKINGAKU.ToString.PadLeft(5, "0"c)
            End If

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
            If intKBN.Equals(0) Or intKBN.Equals(2) Then
                '【ﾌﾟﾚﾐｱﾑ】
                dcICR700.PREZANKN_WR = (intV31PREZANKN + intPREMKN).ToString.PadLeft(5, "0"c)
            Else
                '【ﾁｹｯﾄ】
                dcICR700.PREZANKN_WR = intV31PREZANKN.ToString.PadLeft(5, "0"c)
            End If
            '残ポイント
            dcICR700.POINT_WR = (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intREPOINT).ToString.PadLeft(5, "0"c)
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

            '【金額サマリ】
            If intKBN.Equals(0) Or intKBN.Equals(2) Then
                '【ﾌﾟﾚﾐｱﾑ】
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
            Else
                '【ﾁｹｯﾄ】
                sql.Clear()
                sql.Append("UPDATE TICHETSMA SET")
                sql.Append(" TICHET = TICHET + " & intPREMKN)
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" SCLMANNO = '" & dcICR700.SCLMANNO & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "ﾁｹｯﾄサマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    Return False
                End If
                '【スクール売上トラン】
                sql.Clear()
                sql.Append("INSERT INTO UDNTRN VALUES(")
                '伝票日付
                sql.Append(UIUtility.SYSTEM.UPDDAY & ",")
                '伝票番号
                sql.Append(dtSEQTRN.Rows(0).Item("DENNO").ToString & ",")
                'チケット区分【001】ｽｸｰﾙﾁｹｯﾄ【999】ポイント還元
                sql.Append("'999',")
                '売上金額
                sql.Append("0,")
                'チケット枚数
                sql.Append(intPREMKN & ",")
                '経理締日付
                sql.Append("NULL,")
                'スクール生番号
                sql.Append("'" & dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',")
                'スクール生区分
                sql.Append("'" & dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',")
                '作成日時
                sql.Append("NOW(),")
                '部門コード
                sql.Append("'001',")
                '分類コード１
                sql.Append("'001',")
                '分類コード２
                sql.Append("'999',")
                '分類コード３
                sql.Append("'001',")
                '税抜売上金額
                sql.Append("NULL,")
                '税込売上金額
                sql.Append("NULL,")
                '消費税金額
                sql.Append("NULL,")
                '商品消費税区分
                sql.Append("NULL,")
                '消費または取得ポイント
                sql.Append(intREPOINT & ",")
                '預かり金額
                sql.Append("NULL,")
                'おつり
                sql.Append("NULL,")
                '固定区分
                sql.Append("NULL,")
                'ポイント対象区分
                sql.Append("NULL,")
                '売上区分
                sql.Append("'9',")   '1:チケット購入、9:チケット還元
                'ホスト名
                sql.Append("'" & Net.Dns.GetHostName & "',")
                'ドロア区分
                sql.Append("'0',")
                'サービス区分
                sql.Append("'2',")   '0:通常 1:サービス 2:ポイント
                '作成日時加工 yyyy/mm/dd HH:mm:ss
                sql.Append("'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',")
                'カード支払区分
                sql.Append("NULL,")  '0:現金、1:カード払い
                '削除区分
                sql.Append("'0',")   '0:通常、9:削除  
                '商品名
                sql.Append("NULL,")   '
                'スタッフコード
                sql.Append(UIFunction.NullCheck(_strSTFCODE) & ",")   'スタッフコード
                'スタッフ名
                sql.Append(UIFunction.NullCheck(_strSTFNAME) & ")")   'スタッフ名

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "スクール売上トランの更新に失敗しました。"
                    iDatabase.RollBack()
                    Return False
                End If
            End If
            '【ポイントサマリ】
            sql.Clear()
            sql.Append("UPDATE DPOINTSMA SET")
            sql.Append(" SRTPO = " & (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intREPOINT))
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                strMsg = "ポイントサマリの更新に失敗しました。"
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
            '分類コード１【007】ﾎﾟｲﾝﾄ還元
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'007',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'" & (Me.tabNKN.SelectedIndex + 1).ToString.PadLeft(3, "0"c) & "',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分 【0】ﾌﾟﾚﾐｱﾑ【1】ﾁｹｯﾄ【2】ﾎﾟｲﾝﾄ修正
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'" & intKBN & "',"
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
            If intKBN.Equals(0) Or intKBN.Equals(2) Then
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= intPREMKN & ","
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= intPREMKN & ","
            Else
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= "0,"
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= "0,"
            End If

            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= "0,"
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'9',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intREPOINT & ","
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
            strSQL2 &= "'9'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
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
            strSQL2 &= "'" & strREPONM & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPREMKN & ","
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= UIFunction.NullCheck(_strSTFCODE) & ","
            'スタッフ名
            strSQL1 &= "STFNAME,"
            strSQL2 &= UIFunction.NullCheck(_strSTFNAME) & ","
            'カード期限
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
            '分類コード１【007】ﾎﾟｲﾝﾄ還元
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'007',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'" & (Me.tabNKN.SelectedIndex + 1).ToString.PadLeft(3, "0"c) & "',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分【0】ﾌﾟﾚﾐｱﾑ【1】ﾁｹｯﾄ
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'" & intKBN & "',"
            '売上名
            strSQL1 &= "TKTNMA,"
            strSQL2 &= "'" & strREPONM & "',"
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
            If intKBN.Equals(0) Or intKBN.Equals(2) Then
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= intPREMKN & ","
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= intPREMKN & ","
            Else
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= "0,"
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= "0,"
            End If
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= "0,"
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'9',"
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
            strSQL2 &= "'9'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
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
            strSQL2 &= "'" & strREPONM & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= intPREMKN & ")"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "伝票トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【ポイント還元トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO DREPOTRN("
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
            'ポイント還元名称
            strSQL1 &= "REPONM,"
            strSQL2 &= "'" & strREPONM & "',"
            '消費ポイント
            strSQL1 &= "REPOINT,"
            strSQL2 &= intREPOINT & ","
            'ﾁｹｯﾄ区分
            strSQL1 &= "TKTKBN,"
            strSQL2 &= intKBN & ","
            '還元数(ﾌﾟﾚﾐｱﾑ・チケット数)
            strSQL1 &= "REPOSU,"
            strSQL2 &= intPREMKN & ","
            If intKBN.Equals(0) Then
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
                strSQL2 &= (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intREPOINT) & ","
            Else
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
                strSQL2 &= intV31PREZANKN & ","
                '残ポイント入金前
                strSQL1 &= "ZANAPO,"
                strSQL2 &= CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) & ","
                '残ポイント入金後
                strSQL1 &= "ZANBPO,"
                strSQL2 &= (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intREPOINT) & ","
            End If

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

                Dim dr As DataRow
                dr = dtGOODS.NewRow
                If intKBN.Equals(0) Then
                    '【ﾌﾟﾚﾐｱﾑ】
                    dr("GDSNAME") = "ﾌﾟﾚﾐｱﾑ"
                ElseIf intKBN.Equals(1) Then
                    '【ﾁｹｯﾄ】
                    dr("GDSNAME") = "ﾁｹｯﾄ " & intPREMKN.ToString("#,##0") & "枚"
                    intPREMKN = 0
                Else
                    dr("GDSNAME") = "ﾎﾟｲﾝﾄ加算・減算"
                    intPREMKN = 0
                End If
                dr("GDSTAX") = 0
                dr("GDSKIN") = intPREMKN.ToString("#,##0")
                dtGOODS.Rows.Add(dr)

                '【レシート印刷】
                Dim rePrint As New TMT90.Receipt

                rePrint.intGetPremKn = intPREMKN
                rePrint.intGetPoint = intREPOINT

                rePrint.intzankingaku = intV31ZANKN + intV31PREZANKN + intPREMKN
                rePrint.intzanpoint = (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intREPOINT)

                rePrint.strManno = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                rePrint.strccsname = dtCSMAST.Rows(0).Item("CCSNAME").ToString

                rePrint.intPrintKbn = 2

                rePrint.strDENNO = dtSEQTRN.Rows(0).Item("DENNO").ToString.PadLeft(4, "0"c)
                rePrint.insDTTM = dtmInsDt
                rePrint.dtGoods = dtGOODS
                rePrint.intGokei = CType(dtGOODS.Rows(0).Item("GDSKIN").ToString, Integer)
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

