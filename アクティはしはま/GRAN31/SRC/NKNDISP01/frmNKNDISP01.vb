Imports TECHNO.DataBase
Imports TMT90

Public Class frmNKNDISP01

#Region "▼宣言部"

    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "NKNDISP01"

    ''' <summary>
    ''' カード挿入済み確認フラグ【True】挿入済み
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnCardFLG As Boolean = False

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _rePrint As New Receipt
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

    Private _blnNoCardCheck As Boolean = False
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "入金画面"

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

            MyBase.l_Title_FormName = "入金画面"

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

            MyBase.l_Title_FormName = "入金画面"

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

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNKNDISP01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    ''' フォーム_Closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNKNDISP01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            ''カード排出
            'If _blnCardFLG Then
            '    Using frm As New frmREQUESTCARD(dcICR700)
            '        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
            '        frm.EJECTSTOP = True
            '        frm.ShowDialog()
            '    End Using
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Dim intCPAYKBN As Integer = 0
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【入金】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.NYUKIN
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.PRINT
                ' *** STA ADD 2018/04/11 TERAYAMA 特定ホスト以外非表示
                frm.CurrentHostOnly = True
                ' *** END ADD
                frm.ShowDialog()

                If frm.CANCEL Then Exit Sub

                '伝票番号
                strDENNO = frm.DENNO
                '支払区分
                intCPAYKBN = frm.CPAYKBN
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
            dr("GDSNAME") = "カード入金"
            dr("GDSCOUNT") = resultDt.Rows(0).Item("TKTSU").ToString
            dr("GDSTAX") = CType(resultDt.Rows(0).Item("UDNZKN").ToString, Integer).ToString("#,##0")
            dr("GDSKIN") = CType(resultDt.Rows(0).Item("UDNBKN").ToString, Integer).ToString("#,##0")
            dr("CPAYKBN") = intCPAYKBN.ToString
            dtGOODS.Rows.Add(dr)

            '【レシート印刷】
            Dim rePrint As New TMT90.Receipt

            rePrint.intDatKbn = CType(resultDt.Rows(0).Item("DATKB").ToString, Integer)

            rePrint.intGetPremKn = CType(resultDt.Rows(0).Item("PREMKN").ToString, Integer)
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
            rePrint.intGokei = CType(resultDt.Rows(0).Item("UDNBKN").ToString, Integer)
            rePrint.intDeposit = CType(resultDt.Rows(0).Item("NYUKN").ToString, Integer)
            rePrint.intChange = CType(resultDt.Rows(0).Item("TURIKN").ToString, Integer)
            rePrint.strHostName = resultDt.Rows(0).Item("HOSTNAME").ToString
            rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
            rePrint.RePrint(False, String.Empty)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 入金取消ボタン_Click
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
        Dim intSRTPO As Integer = 0

        Try
            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
                If frm.ERRFLG Then Exit Sub
            End Using
            'If Not _blnCardFLG Then
            '    Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
            '        frm.COMMAND = frmREQUESTCARD.Command_Type.READ
            '        frm.ShowDialog()
            '        If frm.CANCEL Then Exit Sub
            '        If frm.ERRFLG Then Exit Sub
            '    End Using
            'End If


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

            Dim strNCSNO As String = String.Empty
            If dcICR700.NCSNO >= "50000000" Then
                strNCSNO = dcICR700.NCSNO
                intSRTPO = CType(dcICR700.POINT, Integer)
            Else
                If dtCSMAST.Rows.Count.Equals(0) Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If
                strNCSNO = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                intSRTPO = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            End If




            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【入金】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.NYUKIN
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.CANCEL
                '顧客番号
                frm.MANNO = strNCSNO
                ' *** STA ADD 2018/04/11 TERAYAMA 特定ホスト以外非表示
                frm.CurrentHostOnly = True
                ' *** END ADD
                frm.ShowDialog()

                'キャンセル
                blnCANCEL = frm.CANCEL
                '入金額
                intUDNKN = frm.UDNKN
                'プレミアム
                intPREMKN = frm.PREMKN
                'ポイント
                intPOINT = frm.POINT
                '伝票番号
                strDENNO = frm.DENNO
                '合計金額
                intUDNBKN = frm.UDNBKN
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

                intKINGAKU = CType(dcICR700.KINGAKU, Integer) - (intUDNKN + intPREMKN)
                Dim intPOINT2 As Integer = intSRTPO - (intPOINT)

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
                ElseIf intPOINT2 > 99999 Then
                    Using frm As New frmMSGBOX01("ポイントの上限を超えている為取消できません。", 2)
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
                ElseIf intPOINT2 < 0 Then
                    Using frm As New frmMSGBOX01("ポイントがマイナスになる為取消できません。", 2)
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

                intZANKN = CType(dcICR700.ZANKN, Integer) - intUDNKN
                intPREZANKN = CType(dcICR700.PREZANKN, Integer) - intPREMKN
                intV31POINT = intSRTPO - intPOINT
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

                '【データベース更新処理】



                Do
                    'トランザクション開始
                    iDatabase.BeginTransaction()

                    If Not dcICR700.NCSNO >= "50000000" Then
                        '【金額サマリ】
                        sql.Clear()
                        sql.Append("UPDATE KINSMA SET")
                        sql.Append(" ZANKN = " & intZANKN)
                        sql.Append(",PREZANKN = " & intPREZANKN)
                        sql.Append(",UPDDTM = NOW()")
                        sql.Append(" WHERE")
                        sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            iDatabase.RollBack()
                            Exit Do
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
                    End If


                    Exit Do
                Loop

                '*** ドロアオープン ***'
                Dim strErrMsg2 As String = String.Empty
                Dim isOpen As Boolean = True

                Application.DoEvents()

                _rePrint.OpenDrawer(isOpen, strErrMsg2)

                Application.DoEvents()
                '**********************'


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
                UIFunction.UpdBackDATKB(3, CType(strDENNO, Integer), iDatabase)
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
            Dim intNKNKN As Integer = 0
            'プレミアム
            Dim intPREMKN As Integer = 0
            'ポイント
            Dim intPOINT As Integer = 0
            '消費税
            Dim intNKNTAX As Integer = 0

            intNKNKN = CType(btnNKNKN.Tag, Integer)
            Select Case btnNKNKN.Name.ToString
                Case "btnNKNKN01_001" '【入金タグ１】
                    intPREMKN = CType(Me.lblPREMKN01_001.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT01_001.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX01_001.Tag, Integer)
                Case "btnNKNKN02_001"
                    intPREMKN = CType(Me.lblPREMKN02_001.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT02_001.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX02_001.Tag, Integer)
                Case "btnNKNKN03_001"
                    intPREMKN = CType(Me.lblPREMKN03_001.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT03_001.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX03_001.Tag, Integer)
                Case "btnNKNKN04_001"
                    intPREMKN = CType(Me.lblPREMKN04_001.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT04_001.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX04_001.Tag, Integer)
                Case "btnNKNKN05_001"
                    intPREMKN = CType(Me.lblPREMKN05_001.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT05_001.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX05_001.Tag, Integer)
                Case "btnNKNKN06_001"
                    intPREMKN = CType(Me.lblPREMKN06_001.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT06_001.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX06_001.Tag, Integer)
                Case "btnNKNKN01_002" '【入金タグ2】
                    intPREMKN = CType(Me.lblPREMKN01_002.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT01_002.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX01_002.Tag, Integer)
                Case "btnNKNKN02_002"
                    intPREMKN = CType(Me.lblPREMKN02_002.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT02_002.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX02_002.Tag, Integer)
                Case "btnNKNKN03_002"
                    intPREMKN = CType(Me.lblPREMKN03_002.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT03_002.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX03_002.Tag, Integer)
                Case "btnNKNKN04_002"
                    intPREMKN = CType(Me.lblPREMKN04_002.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT04_002.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX04_002.Tag, Integer)
                Case "btnNKNKN05_002"
                    intPREMKN = CType(Me.lblPREMKN05_002.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT05_002.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX05_002.Tag, Integer)
                Case "btnNKNKN06_002"
                    intPREMKN = CType(Me.lblPREMKN06_002.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT06_002.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX06_002.Tag, Integer)
                Case "btnNKNKN01_003" '【入金タグ3】
                    intPREMKN = CType(Me.lblPREMKN01_003.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT01_003.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX01_003.Tag, Integer)
                Case "btnNKNKN02_003"
                    intPREMKN = CType(Me.lblPREMKN02_003.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT02_003.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX02_003.Tag, Integer)
                Case "btnNKNKN03_003"
                    intPREMKN = CType(Me.lblPREMKN03_003.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT03_003.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX03_003.Tag, Integer)
                Case "btnNKNKN04_003"
                    intPREMKN = CType(Me.lblPREMKN04_003.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT04_003.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX04_003.Tag, Integer)
                Case "btnNKNKN05_003"
                    intPREMKN = CType(Me.lblPREMKN05_003.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT05_003.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX05_003.Tag, Integer)
                Case "btnNKNKN06_003"
                    intPREMKN = CType(Me.lblPREMKN06_003.Tag, Integer)
                    intPOINT = CType(Me.lblPOINT06_003.Tag, Integer)
                    intNKNTAX = CType(Me.lblNKNTAX06_003.Tag, Integer)
            End Select

            'カード読み込み
            If Not _blnCardFLG Then
                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then Exit Sub
                    If frm.ERRFLG Then Exit Sub
                End Using
            End If

            Me.tabNKN.Enabled = False

            'レジ精算画面表示
            Dim strMsg As String = String.Empty
            Dim blnCARD As Boolean = True
            If Not SetREGISTER(intNKNKN, intPREMKN, intPOINT, intNKNTAX, blnCARD, strMsg) Then
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
                If _blnNoCardCheck Then
                    Me.Close()
                End If
                Exit Sub
            End If

            If _blnCardFLG Then
                Me.Close()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.tabNKN.Enabled = True
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

            '入金額
            Dim intNKNKN As Integer = 0
            'プレミアム
            Dim intPREMKN As Integer = 0
            'ポイント
            Dim intPOINT As Integer = 0
            '消費税
            Dim intNKNTAX As Integer = 0

            Select Case CType(btnUpDown.Tag, Integer)
                Case 1
                    '【加算】

                    intNKNKN = CType(Me.txtNKNKN01_004.Text, Integer)
                    intPREMKN = CType(Me.txtPREMKN01_004.Text, Integer)
                    intPOINT = CType(Me.txtPOINT01_004.Text, Integer)
                    intNKNTAX = CType(Me.txtNKNTAX01_004.Text, Integer)

                Case 2
                    '【減算】

                    intNKNKN = CType("-" & Me.txtNKNKN01_004.Text, Integer)
                    intPREMKN = CType("-" & Me.txtPREMKN01_004.Text, Integer)
                    intPOINT = CType("-" & Me.txtPOINT01_004.Text, Integer)
                    intNKNTAX = 0

            End Select

            If CType(intNKNKN.ToString.Replace("-", String.Empty), Integer).Equals(0) Then
                Using frm As New frmMSGBOX01("入金額を指定してください。", 2)
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
            If Not SetREGISTER(intNKNKN, intPREMKN, intPOINT, intNKNTAX, blnCARD, strMsg) Then
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
                If _blnNoCardCheck Then
                    Me.Close()
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

            'プレミアムラベル
            Me.lblPREMKN01_001.Visible = False
            Me.lblPREMKN02_001.Visible = False
            Me.lblPREMKN03_001.Visible = False
            Me.lblPREMKN04_001.Visible = False
            Me.lblPREMKN05_001.Visible = False
            Me.lblPREMKN06_001.Visible = False

            Me.lblPREMKN01_002.Visible = False
            Me.lblPREMKN02_002.Visible = False
            Me.lblPREMKN03_002.Visible = False
            Me.lblPREMKN04_002.Visible = False
            Me.lblPREMKN05_002.Visible = False
            Me.lblPREMKN06_002.Visible = False

            Me.lblPREMKN01_003.Visible = False
            Me.lblPREMKN02_003.Visible = False
            Me.lblPREMKN03_003.Visible = False
            Me.lblPREMKN04_003.Visible = False
            Me.lblPREMKN05_003.Visible = False
            Me.lblPREMKN06_003.Visible = False

            'ポイントラベル
            Me.lblPOINT01_001.Visible = False
            Me.lblPOINT02_001.Visible = False
            Me.lblPOINT03_001.Visible = False
            Me.lblPOINT04_001.Visible = False
            Me.lblPOINT05_001.Visible = False
            Me.lblPOINT06_001.Visible = False

            Me.lblPOINT01_002.Visible = False
            Me.lblPOINT02_002.Visible = False
            Me.lblPOINT03_002.Visible = False
            Me.lblPOINT04_002.Visible = False
            Me.lblPOINT05_002.Visible = False
            Me.lblPOINT06_002.Visible = False

            Me.lblPOINT01_003.Visible = False
            Me.lblPOINT02_003.Visible = False
            Me.lblPOINT03_003.Visible = False
            Me.lblPOINT04_003.Visible = False
            Me.lblPOINT05_003.Visible = False
            Me.lblPOINT06_003.Visible = False

            '消費税ラベル
            Me.lblNKNTAX01_001.Visible = False
            Me.lblNKNTAX02_001.Visible = False
            Me.lblNKNTAX03_001.Visible = False
            Me.lblNKNTAX04_001.Visible = False
            Me.lblNKNTAX05_001.Visible = False
            Me.lblNKNTAX06_001.Visible = False

            Me.lblNKNTAX01_002.Visible = False
            Me.lblNKNTAX02_002.Visible = False
            Me.lblNKNTAX03_002.Visible = False
            Me.lblNKNTAX04_002.Visible = False
            Me.lblNKNTAX05_002.Visible = False
            Me.lblNKNTAX06_002.Visible = False

            Me.lblNKNTAX01_003.Visible = False
            Me.lblNKNTAX02_003.Visible = False
            Me.lblNKNTAX03_003.Visible = False
            Me.lblNKNTAX04_003.Visible = False
            Me.lblNKNTAX05_003.Visible = False
            Me.lblNKNTAX06_003.Visible = False

            '入金額手入力
            Me.txtNKNKN01_004.Text = "0"
            'プレミアム手入力
            Me.txtPREMKN01_004.Text = "0"
            'ポイント手入力
            Me.txtPOINT01_004.Text = "0"
            '消費税手入力
            Me.txtNKNTAX01_004.Text = "0"


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
            sql.Append(" AND BUNCDA = '003'")
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
            sql.Append(" STSFLG = '0'")
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
                'プレミアム01
                Me.lblPREMKN01_001.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN01_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN01_001.Visible = True
                'ポイント01
                Me.lblPOINT01_001.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT01_001.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT01_001.Visible = True
                '消費税01
                Me.lblNKNTAX01_001.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX01_001.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX01_001.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.btnNKNKN02_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN02_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN02_001.Visible = True
                'プレミアム02
                Me.lblPREMKN02_001.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN02_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN02_001.Visible = True
                'ポイント02
                Me.lblPOINT02_001.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT02_001.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT02_001.Visible = True
                '消費税02
                Me.lblNKNTAX02_001.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX02_001.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX02_001.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.btnNKNKN03_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN03_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN03_001.Visible = True
                'プレミアム03
                Me.lblPREMKN03_001.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN03_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN03_001.Visible = True
                'ポイント03
                Me.lblPOINT03_001.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT03_001.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT03_001.Visible = True
                '消費税03
                Me.lblNKNTAX03_001.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX03_001.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX03_001.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.btnNKNKN04_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN04_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN04_001.Visible = True
                'プレミアム04
                Me.lblPREMKN04_001.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN04_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN04_001.Visible = True
                'ポイント04
                Me.lblPOINT04_001.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT04_001.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT04_001.Visible = True
                '消費税04
                Me.lblNKNTAX04_001.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX04_001.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX04_001.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.btnNKNKN05_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN05_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN05_001.Visible = True
                'プレミアム05
                Me.lblPREMKN05_001.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN05_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN05_001.Visible = True
                'ポイント05
                Me.lblPOINT05_001.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT05_001.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT05_001.Visible = True
                '消費税05
                Me.lblNKNTAX05_001.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX05_001.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX05_001.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.btnNKNKN06_001.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN06_001.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN06_001.Visible = True
                'プレミアム06
                Me.lblPREMKN06_001.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN06_001.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN06_001.Visible = True
                'ポイント06
                Me.lblPOINT06_001.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT06_001.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT06_001.Visible = True
                '消費税06
                Me.lblNKNTAX06_001.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX06_001.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX06_001.Visible = False
            End If
            '//【タグ区分002】//
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.btnNKNKN01_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN01_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN01_002.Visible = True
                'プレミアム01
                Me.lblPREMKN01_002.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN01_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN01_002.Visible = True
                'ポイント01
                Me.lblPOINT01_002.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT01_002.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT01_002.Visible = True
                '消費税01
                Me.lblNKNTAX01_002.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX01_002.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX01_002.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.btnNKNKN02_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN02_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN02_002.Visible = True
                'プレミアム02
                Me.lblPREMKN02_002.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN02_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN02_002.Visible = True
                'ポイント02
                Me.lblPOINT02_002.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT02_002.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT02_002.Visible = True
                '消費税02
                Me.lblNKNTAX02_002.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX02_002.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX02_002.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.btnNKNKN03_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN03_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN03_002.Visible = True
                'プレミアム03
                Me.lblPREMKN03_002.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN03_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN03_002.Visible = True
                'ポイント03
                Me.lblPOINT03_002.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT03_002.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT03_002.Visible = True
                '消費税03
                Me.lblNKNTAX03_002.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX03_002.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX03_002.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.btnNKNKN04_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN04_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN04_002.Visible = True
                'プレミアム04
                Me.lblPREMKN04_002.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN04_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN04_002.Visible = True
                'ポイント04
                Me.lblPOINT04_002.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT04_002.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT04_002.Visible = True
                '消費税04
                Me.lblNKNTAX04_002.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX04_002.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX04_002.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.btnNKNKN05_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN05_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN05_002.Visible = True
                'プレミアム05
                Me.lblPREMKN05_002.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN05_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN05_002.Visible = True
                'ポイント05
                Me.lblPOINT05_002.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT05_002.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT05_002.Visible = True
                '消費税05
                Me.lblNKNTAX05_002.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX05_002.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX05_002.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.btnNKNKN06_002.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN06_002.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN06_002.Visible = True
                'プレミアム06
                Me.lblPREMKN06_002.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN06_002.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN06_002.Visible = True
                'ポイント06
                Me.lblPOINT06_002.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT06_002.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT06_002.Visible = True
                '消費税06
                Me.lblNKNTAX06_002.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX06_002.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX06_002.Visible = False
            End If
            '//【タグ区分003】//
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.btnNKNKN01_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN01_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN01_003.Visible = True
                'プレミアム01
                Me.lblPREMKN01_003.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN01_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN01_003.Visible = True
                'ポイント01
                Me.lblPOINT01_003.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT01_003.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT01_003.Visible = True
                '消費税01
                Me.lblNKNTAX01_003.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX01_003.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX01_003.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.btnNKNKN02_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN02_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN02_003.Visible = True
                'プレミアム02
                Me.lblPREMKN02_003.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN02_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN02_003.Visible = True
                'ポイント02
                Me.lblPOINT02_003.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT02_003.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT02_003.Visible = True
                '消費税02
                Me.lblNKNTAX02_003.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX02_003.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX02_003.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.btnNKNKN03_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN03_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN03_003.Visible = True
                'プレミアム03
                Me.lblPREMKN03_003.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN03_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN03_003.Visible = True
                'ポイント03
                Me.lblPOINT03_003.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT03_003.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT03_003.Visible = True
                '消費税03
                Me.lblNKNTAX03_003.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX03_003.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX03_003.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.btnNKNKN04_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN04_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN04_003.Visible = True
                'プレミアム04
                Me.lblPREMKN04_003.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN04_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN04_003.Visible = True
                'ポイント04
                Me.lblPOINT04_003.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT04_003.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT04_003.Visible = True
                '消費税04
                Me.lblNKNTAX04_003.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX04_003.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX04_003.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.btnNKNKN05_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN05_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN05_003.Visible = True
                'プレミアム05
                Me.lblPREMKN05_003.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN05_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN05_003.Visible = True
                'ポイント05
                Me.lblPOINT05_003.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT05_003.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT05_003.Visible = True
                '消費税05
                Me.lblNKNTAX05_003.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX05_003.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX05_003.Visible = False
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.btnNKNKN06_003.Text = dr(0).Item("NKNNM").ToString
                Me.btnNKNKN06_003.Tag = CType(dr(0).Item("NKNKN").ToString, Integer)
                Me.btnNKNKN06_003.Visible = True
                'プレミアム06
                Me.lblPREMKN06_003.Text = "プレミアム " & CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0") & "円"
                Me.lblPREMKN06_003.Tag = CType(dr(0).Item("PREMKN").ToString, Integer)
                Me.lblPREMKN06_003.Visible = True
                'ポイント06
                Me.lblPOINT06_003.Text = "ポイント " & CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0") & "P"
                Me.lblPOINT06_003.Tag = CType(dr(0).Item("POINT").ToString, Integer)
                Me.lblPOINT06_003.Visible = True
                '消費税06
                Me.lblNKNTAX06_003.Text = "消費税 " & CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0") & "円"
                Me.lblNKNTAX06_003.Tag = CType(dr(0).Item("NKNTAX").ToString, Integer)
                Me.lblNKNTAX06_003.Visible = False
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' レジ精算画面
    ''' </summary>
    ''' <param name="intNKNKN">入金額</param>
    ''' <param name="intPREMKN">プレミアム</param>
    ''' <param name="intPOINT">ポイント</param>
    ''' <param name="intNKNTAX">消費税</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetREGISTER(ByVal intNKNKN As Integer, ByVal intPREMKN As Integer, ByVal intPOINT As Integer, ByVal intNKNTAX As Integer, ByRef blnCARD As Boolean, ByRef strMsg As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intSRTPO As Integer = 0
        Try
            '入金限度額チェック
            If (CType(dcICR700.KINGAKU, Integer) + (intNKNKN + intPREMKN)) > UIUtility.SYSTEM.ZANMAX Then
                strMsg = "入金限度額を超えています。"
                Return False
            End If

            If CType(dcICR700.ZANKN, Integer) + intNKNKN < 0 Then
                strMsg = "残金がマイナスになります。"
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

            If Not dcICR700.NCSNO >= "50000000" Then
                If dtCSMAST.Rows.Count.Equals(0) Then
                    strMsg = "顧客データがありません。"
                    If _blnCardFLG Then
                        blnCARD = False
                    End If
                    Return False
                End If
                If (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intPOINT) > 99999 Then
                    strMsg = "ﾎﾟｲﾝﾄの上限数を超えています。"
                    If _blnCardFLG Then
                        blnCARD = False
                    End If
                    Return False
                End If
                If CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intPOINT < 0 Then
                    strMsg = "ポイントがマイナスになります。"
                    If _blnCardFLG Then
                        blnCARD = False
                    End If
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

                        If (Now.ToString("yyyyMMdd") > dtCSMAST.Rows(0).Item("CARDLIMIT").ToString) Or (Now.ToString("yyyy/MM/dd") > strCARDLIMIT) Then
                            strMsg = "有効期限が切れています。" & vbCrLf & "※受付画面から期限を確認してください。※"
                            If _blnCardFLG Then
                                blnCARD = False
                            End If
                            Return False
                        End If
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

            '伝票番号取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" (MAX(DENNOSEQ) + 1) AS DENNO")
            sql.Append(" FROM SEQTRN")

            Dim dtSEQTRN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dtSEQTRN.Rows.Count.Equals(0) Then
                strMsg = "伝票番号の取得に失敗しました。"
                Return False
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
            dr("GDSNAME") = "カード入金"
            dr("GDSCOUNT") = 1
            dr("GDSTAX") = intNKNTAX.ToString("#,##0")
            dr("GDSKIN") = (intNKNKN + intNKNTAX).ToString("#,##0")
            dr("CPAYKBN") = String.Empty
            dtGOODS.Rows.Add(dr)

            'レジ精算画面
            '預り金
            Dim intRsvDEPOSIT As Integer = 0
            'つり銭
            Dim intRsvCHANGE As Integer = 0
            '支払区分
            Dim intRsvCPAYKBN As Integer = 0
            '取得プレミアム
            Dim intRsvPREMKN As Integer = 0
            '取得ﾎﾟｲﾝﾄ
            Dim intRsvPOINT As Integer = 0
            'レシート出力区分
            Dim blnRsvRECEIPT As Boolean = False
            If UIUtility.SYSTEM.RECEIPTFLG.Equals(1) Then
                blnRsvRECEIPT = True
            End If
            'キャンセルフラグ
            Dim blnRsvCANCEL As Boolean = False
            Using frm As New frmREGISTER01
                '伝票番号
                frm.DENNO = dtSEQTRN.Rows(0).Item("DENNO").ToString.PadLeft(5, "0"c)

                If dcICR700.NCSNO >= "50000000" Then
                    '顧客番号
                    frm.NCSNO = dcICR700.NCSNO
                    '氏名
                    frm.CCSNAME = "貸出カード"
                    'ｶﾅ
                    frm.CCSKANA = String.Empty
                    '顧客種別
                    frm.CKBNAME = String.Empty
                    'スクール生番号
                    frm.SCLMANNO = String.Empty
                    '会員期限
                    frm.DMEMBER = String.Empty
                    '誕生日
                    frm.DBIRTH = String.Empty
                    '残金
                    frm.ZANKN = CType(dcICR700.ZANKN, Integer).ToString("#,##0")
                    'プレミアム
                    frm.PREZANKN = CType(dcICR700.PREZANKN, Integer).ToString("#,##0")
                    'ポイント
                    frm.POINT = CType(dcICR700.POINT, Integer).ToString("#,##0")
                    intSRTPO = CType(dcICR700.POINT, Integer)
                Else
                    '顧客番号
                    frm.NCSNO = CType(dtCSMAST.Rows(0).Item("NCSNO"), Integer).ToString.PadLeft(8, "0"c)
                    '氏名
                    frm.CCSNAME = dtCSMAST.Rows(0).Item("CCSNAME").ToString
                    'ｶﾅ
                    frm.CCSKANA = dtCSMAST.Rows(0).Item("CCSKANA").ToString
                    '顧客種別
                    frm.CKBNAME = dtCSMAST.Rows(0).Item("CKBNAME").ToString
                    'スクール生番号
                    frm.SCLMANNO = dtCSMAST.Rows(0).Item("DSCLMANNO").ToString
                    '会員期限
                    frm.DMEMBER = dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString
                    '誕生日
                    frm.DBIRTH = dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString
                    '残金
                    frm.ZANKN = CType(dtCSMAST.Rows(0).Item("ZANKN"), Integer).ToString("#,##0")
                    'プレミアム
                    frm.PREZANKN = CType(dtCSMAST.Rows(0).Item("PREZANKN"), Integer).ToString("#,##0")
                    'ポイント
                    frm.POINT = CType(dtCSMAST.Rows(0).Item("SRTPO"), Integer).ToString("#,##0")
                    intSRTPO = CType(dtCSMAST.Rows(0).Item("SRTPO"), Integer)
                End If
         
                '商品売上
                frm.GOODS = dtGOODS
                '現金
                frm.PAYMENT = intNKNKN + intNKNTAX
                '取得プレミアム
                frm.GETPREMKN = intPREMKN
                '取得ﾎﾟｲﾝﾄ
                frm.GETPOINT = intPOINT
                'レシート出力区分
                frm.RECEIPT = blnRsvRECEIPT

                frm.ShowDialog()

                'キャンセル
                blnRsvCANCEL = frm.CANCEL
                '預り金
                intRsvDEPOSIT = frm.DEPOSIT
                'つり銭
                intRsvCHANGE = frm.CHANGE
                '支払区分
                intRsvCPAYKBN = frm.CPAYKBN
                '取得プレミアム
                intRsvPREMKN = frm.GETPREMKN
                '取得ﾎﾟｲﾝﾄ
                intRsvPOINT = frm.GETPOINT
                'レシート出力区分
                blnRsvRECEIPT = frm.RECEIPT
            End Using

            If blnRsvCANCEL Then
                'カード排出
                If _blnCardFLG Then
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.EJECTSTOP = True
                        frm.ShowDialog()
                    End Using
                    Return True
                Else
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Return True
                End If
            End If

            '*** 再度チェック ***'

            '受付たｶｰﾄﾞが同一か確認
            _blnNoCardCheck = False
            If Not CheckCard() Then
                strMsg = "最初に受付けたカードと異なります。"
                _blnNoCardCheck = True
                Return False
            End If

            '入金限度額チェック
            If (intKINGAKU + (intNKNKN + intRsvPREMKN)) > UIUtility.SYSTEM.ZANMAX Then
                strMsg = "入金限度額を超えています。"
                Return False
            End If


            If (intSRTPO + intRsvPOINT) > 99999 Then
                strMsg = "ﾎﾟｲﾝﾄの上限数を超えています。"
                Return False
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
            dcICR700.KINGAKU_WR = (intKINGAKU + (intNKNKN + intRsvPREMKN)).ToString.PadLeft(5, "0"c)
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
            dcICR700.ZANKN_WR = (intV31ZANKN + intNKNKN).ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = (intV31PREZANKN + intRsvPREMKN).ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = (intSRTPO + intRsvPOINT).ToString.PadLeft(5, "0"c)
            '入場区分
            dcICR700.ENTKBN_WR = dcICR700.ENTKBN
            'ボール単価
            dcICR700.BALLKIN_WR = dcICR700.BALLKIN
            '打席番号
            dcICR700.SEATNO_WR = dcICR700.SEATNO

            frmWait.Show()
            frmWait.Refresh()

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
            '伝票番号再取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" DENNOSEQ AS DENNO")
            sql.Append(" FROM SEQTRN")

            dtSEQTRN.Clear()
            dtSEQTRN = iDatabase.ExecuteRead(sql.ToString())

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
            '分類コード１【003】入金
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'003',"
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
            strSQL2 &= intNKNKN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            If dcICR700.NCSNO >= "50000000" Then
                '顧客番号
                strSQL1 &= "MANNO,"
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
                '顧客種別
                strSQL1 &= "KSBKB,"
                If dcICR700.NKBNO.Equals("A") Then
                    strSQL2 &= "'10',"
                Else
                    strSQL2 &= "'" & dcICR700.NKBNO & "',"
                End If
                'スクール生番号
                strSQL1 &= "SCLMANNO,"
                strSQL2 &= "NULL,"
                'スクール生区分
                strSQL1 &= "SCLKBN,"
                strSQL2 &= "NULL,"
            Else
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
            End If
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= intNKNKN & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= (intNKNKN + intNKNTAX) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intNKNTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intRsvPOINT & ","
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= intRsvDEPOSIT & ","
            'おつり
            strSQL1 &= "TURIKN,"
            strSQL2 &= intRsvCHANGE & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
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
            strSQL2 &= "'" & intRsvCPAYKBN & "',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "入金 " & intNKNKN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intRsvPREMKN & ","
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
            '分類コード１【003】入金
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'003',"
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
            strSQL2 &= "'" & "入金 " & intNKNKN.ToString("#,##0") & "円" & "',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= intNKNKN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            If dcICR700.NCSNO >= "50000000" Then
                '顧客番号
                strSQL1 &= "MANNO,"
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
                '顧客種別
                strSQL1 &= "KSBKB,"
                If dcICR700.NKBNO.Equals("A") Then
                    strSQL2 &= "'10',"
                Else
                    strSQL2 &= "'" & dcICR700.NKBNO & "',"
                End If
                'スクール生番号
                strSQL1 &= "SCLMANNO,"
                strSQL2 &= "NULL,"
                'スクール生区分
                strSQL1 &= "SCLKBN,"
                strSQL2 &= "NULL,"
            Else
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
            End If
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= intNKNKN & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= (intNKNKN + intNKNTAX) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intNKNTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intRsvPOINT & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
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
            strSQL2 &= "'" & intRsvCPAYKBN & "',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "入金 " & intNKNKN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= intRsvPREMKN & ")"
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
            If dcICR700.NCSNO >= "50000000" Then
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
            Else
                strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            End If
            '入金名
            strSQL1 &= "NKNNM,"
            strSQL2 &= "'" & "入金 " & intNKNKN.ToString("#,##0") & "円" & "',"
            '入金額
            strSQL1 &= "NKNKN,"
            strSQL2 &= intNKNKN & ","
            '税抜売上金額
            strSQL1 &= "NKNAKN,"
            strSQL2 &= intNKNKN & ","
            '税込売上金額
            strSQL1 &= "NKNBKN,"
            strSQL2 &= intNKNKN & ","
            '消費税
            strSQL1 &= "NZEIKN,"
            strSQL2 &= intNKNTAX & ","
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intRsvPOINT & ","
            '用途不明
            strSQL1 &= "PRERT,"
            strSQL2 &= "NULL,"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intRsvPREMKN & ","
            '残金入金前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intV31ZANKN & ","
            '残金入金後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intV31ZANKN + (intNKNKN) & ","
            'P)残金入金前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intV31PREZANKN & ","
            'P)残金入金後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= intV31PREZANKN + (intRsvPREMKN) & ","
            '残ポイント入金前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= intSRTPO & ","
            '残ポイント入金後
            strSQL1 &= "ZANBPO,"
            strSQL2 &= (intSRTPO + intRsvPOINT) & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            '種別フラグ
            strSQL1 &= "STSFLG,"
            strSQL2 &= "'0',"
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "入金トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If

            'コミット
            iDatabase.Commit()

            '*** ドロアオープン ***'
            Dim strErrMsg2 As String = String.Empty
            Dim isOpen As Boolean = True

            Application.DoEvents()

            _rePrint.OpenDrawer(isOpen, strErrMsg2)

            Application.DoEvents()
            '**********************'

            frmWait.Close()

            iDatabase.BeginTransaction()

            If Not dcICR700.NCSNO >= "50000000" Then
                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & (intV31ZANKN + intNKNKN))
                sql.Append(",PREZANKN = " & (intV31PREZANKN + intRsvPREMKN))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), strZENENTDATE)
                    Return False
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & (intSRTPO + intRsvPOINT))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "ポイントサマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), strZENENTDATE)
                    Return False
                End If
                '【カード有効期限】
                sql.Clear()
                sql.Append("UPDATE CSMAST SET")
                'カード有効期限
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
                    DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), strZENENTDATE)
                    Return False
                End If

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
                DelNknInfo(CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer), strZENENTDATE)
                Return False
            End If

            'コミット
            iDatabase.Commit()

            If blnRsvRECEIPT Then

                Dim drGoods As DataRow()
                drGoods = dtGOODS.Select()
                drGoods(0).Item("CPAYKBN") = intRsvCPAYKBN.ToString
                drGoods(0).EndEdit()

                '【レシート印刷】
                Dim rePrint As New TMT90.Receipt

                rePrint.intGetPremKn = intRsvPREMKN
                rePrint.intGetPoint = intRsvPOINT
                rePrint.intzankingaku = intV31ZANKN + intNKNKN + intV31PREZANKN + intRsvPREMKN

                If dcICR700.NCSNO >= "50000000" Then
                    rePrint.intzanpoint = CType(dcICR700.POINT, Integer) + intRsvPOINT

                    rePrint.strManno = dcICR700.NCSNO
                    rePrint.strccsname = "貸出カード"
                Else
                    rePrint.intzanpoint = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intRsvPOINT

                    rePrint.strManno = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                    rePrint.strccsname = dtCSMAST.Rows(0).Item("CCSNAME").ToString
                End If


                rePrint.intPrintKbn = 1
                rePrint.strDENNO = dtSEQTRN.Rows(0).Item("DENNO").ToString.PadLeft(4, "0"c)
                rePrint.insDTTM = dtmInsDt
                rePrint.dtGoods = dtGOODS
                rePrint.intGokei = CType(dtGOODS.Rows(0).Item("GDSKIN").ToString, Integer)
                rePrint.intDeposit = intRsvDEPOSIT
                rePrint.intChange = intRsvCHANGE
                rePrint.strHostName = Net.Dns.GetHostName
                rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
                rePrint.RePrint(False, String.Empty)
            End If

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally
            frmWait.Close()
        End Try
    End Function


    ''' <summary>
    ''' カード書き込み失敗による入場情報削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DelNknInfo(ByVal intDENNO As Integer, ByVal strUDNDT As String)
        Dim sql As New System.Text.StringBuilder
        Try
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE DUDNTRN SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND UDNNO = " & intDENNO)

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
            End If

            sql.Clear()
            sql.Append("UPDATE DENTRA SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND UDNNO = " & intDENNO)

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
            End If

            sql.Clear()
            sql.Append("UPDATE NKNTRN SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  DENDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND DENNO = " & intDENNO)

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
            End If

            iDatabase.Commit()

        Catch ex As Exception
            '失敗してもほっとく
            iDatabase.RollBack()
        End Try
    End Sub

    ''' <summary>
    ''' 受付カードが同じか確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckCard() As Boolean
        Dim strCARDNO As String = String.Empty
        Dim intTmp As Integer = 0
        Try
            strCARDNO = dcICR700.CARDNO

            ' ICRW初期化
            dcICR700.Init()

            Do
                ' カード読み込み
                Dim ret = dcICR700.Read3(-1)

                If ret = Techno.DeviceControls.ICR700Control.eResult.SUCCESS Then

                    If Not strCARDNO.Equals(dcICR700.CARDNO) Then
                        Return False
                    End If

                    Exit Do
                Else
                    If ret = (Techno.DeviceControls.ICR700Control.eResult.vbERROR Or Techno.DeviceControls.ICR700Control.eResult.TIMEOUT) Or intTmp.Equals(3000) Then
                        '【失敗】
                        Return False
                        dcICR700.Init()
                    End If
                End If
                intTmp += 200
            Loop

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region






 
End Class

