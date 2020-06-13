Imports System.Threading
Imports Techno.DataBase
Imports Microsoft.Office.Interop
Imports TMT90
Imports System.Security.Permissions

Public Class frmFRONT01

#Region "▼宣言部"
    Private _threadCardRead As New Thread(New ThreadStart(AddressOf ThreadCardRead))
    Private _blnRead As Boolean = False
    Private _blnTdEnd As Boolean = False

    Private Const PICK_CARD As String = "キャンセル"

    Private _dtCSMAST As DataTable
    ''' <summary>
    ''' 月間来場ポイント
    ''' </summary>
    ''' <remarks></remarks>
    Private _intETPPO As Integer
    ''' <summary>
    ''' 誕生月ポイント
    ''' </summary>
    ''' <remarks></remarks>
    Private _intBIRTHMPO As Integer
    ''' <summary>
    ''' 誕生日ﾎﾟｲﾝﾄ
    ''' </summary>
    ''' <remarks></remarks>
    Private _intBIRTHDPO As Integer
    ''' <summary>
    ''' カードリクエストラベル用
    ''' </summary>
    ''' <remarks></remarks>
    Private _intCardReqestCnt As Integer
    ''' <summary>
    ''' 備考1
    ''' </summary>
    ''' <remarks></remarks>
    Private _strBIKO1 As String
    ''' <summary>
    ''' 備考2
    ''' </summary>
    ''' <remarks></remarks>
    Private _strBIKO2 As String
    ''' <summary>
    ''' 備考3
    ''' </summary>
    ''' <remarks></remarks>
    Private _strBIKO3 As String
    ''' <summary>
    ''' ポップアップ
    ''' </summary>
    ''' <remarks></remarks>
    Private _strMEMBERNO As String
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

    Public Delegate Sub myDlegate(ByVal mode As Integer)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _intSwitch As Integer = 0

    ''' <summary>
    ''' 次の時間帯のﾊﾟｽﾜｰﾄﾞ保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _strNextPASSCD As String = String.Empty
    ''' <summary>
    ''' カードを取ったかの確認省く
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnNoEject As Boolean = False
    ''' <summary>
    ''' 残高移行プレペイド金額保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPreKingaku As Integer = 0
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "受付画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod,
                   ByVal ICR700 As Techno.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "受付画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼フォーム移動不可"
    'Imports System.Security.Permissions

    <SecurityPermission(SecurityAction.Demand, _
        Flags:=SecurityPermissionFlag.UnmanagedCode)> _
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_SYSCOMMAND As Integer = &H112
        Const SC_MOVE As Long = &HF010L

        If m.Msg = WM_SYSCOMMAND AndAlso _
            (m.WParam.ToInt64() And &HFFF0L) = SC_MOVE Then
            m.Result = IntPtr.Zero
            Return
        End If

        MyBase.WndProc(m)
    End Sub
#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFRONT01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            GetTenki()

            '営業情報マスタ(1球貸し)セット
            If Not GetEIGMTA() Then
                Using frm As New frmMSGBOX01("営業情報マスタ(1球貸し)の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If
            ''打ち放題情報取得
            'If Not GetEIGMTB() Then
            'End If

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
    Private Sub frmFRONT01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.Refresh()

            'カード要求開始
            ReadStart(False)

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
    Private Sub frmFRONT01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Me.Cursor = Cursors.WaitCursor

            'If CType(Me.btnCard.Tag, Integer).Equals(1) Then
            '    '【カード排出】
            '    Me.btnCard.PerformClick()

            '    Me.Close()
            'End If

            _blnTdEnd = True
            'カード要求ストップ
            ReadStop()

            '顧客情報ﾃｰﾌﾞﾙ削除
            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 気温1テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKION1_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtKION.KeyPress
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack And (Not e.KeyChar.Equals("-"c)) And (Not e.KeyChar.Equals("."c)) Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 天気更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdTenki_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdTenki.Click
        Dim dcKION As Decimal = 0
        Dim sql As New System.Text.StringBuilder
        Try
            If Not String.IsNullOrEmpty(Me.txtKION.Text) Then
                If Not Decimal.TryParse(Me.txtKION.Text.Trim, dcKION) Then
                    Using frm As New frmMSGBOX01("気温の値が異常です。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.txtKION.Focus()
                    Me.txtKION.SelectAll()
                    Exit Sub
                End If
            End If

            iDatabase.BeginTransaction()

            Try
                sql.Clear()
                sql.Append("UPDATE CALMTA SET")
                If String.IsNullOrEmpty(Me.cmbTENKI.Text) Then
                    sql.Append(" TENKI = NULL")
                Else
                    sql.Append(" TENKI = '" & Me.cmbTENKI.Text & "'")
                End If
                If String.IsNullOrEmpty(Me.txtKION.Text) Then
                    sql.Append(",KION = NULL")
                Else
                    sql.Append(",KION = '" & Me.txtKION.Text & "'")
                End If
                sql.Append(" WHERE")
                sql.Append(" CALDT = '" & Now.ToString("yyyyMMdd") & "'")

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("天気情報の更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            Catch ex As Exception
                iDatabase.RollBack()
                Exit Sub
            End Try

            iDatabase.Commit()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' カラーボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnColorKbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClrKbn1.Click, btnClrKbn2.Click, btnClrKbn3.Click
        Dim strSelectColor As String = String.Empty
        Dim blnSelectAll As Boolean = False
        Dim sql As New System.Text.StringBuilder
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Dim strMSG As String = "カラー情報の更新に失敗しました。"
        Try
            Me.Cursor = Cursors.WaitCursor


            Dim btn As Button

            btn = CType(sender, Button)

            Using frm As New frmColorSelect
                If String.IsNullOrEmpty(Me.lblNCSNO.Text) Then frm.chkAll.Checked = True
                frm.ShowDialog()
                strSelectColor = frm.SelectColor
                blnSelectAll = frm.SelectAll
            End Using

            If String.IsNullOrEmpty(strSelectColor) Then Exit Sub
            If String.IsNullOrEmpty(Me.lblNCSNO.Text) And Not blnSelectAll Then Exit Sub

            btn.Text = strSelectColor
            btn.ForeColor = Color.FromName(strSelectColor)
            btn.BackColor = Color.FromName(strSelectColor)

            iDatabase.BeginTransaction()

            Try
                If Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                    If Not (iDatabase.ExecuteUpdate("DELETE FROM CLRSMA WHERE MANNO = '" & Me.lblNCSNO.Text & "'")) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01(strMSG, 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If

                    strSQL01 = "INSERT INTO CLRSMA ("
                    strSQL02 = "VALUES ("

                    strSQL01 &= "MANNO,"
                    strSQL02 &= "'" & Me.lblNCSNO.Text & "',"
                    strSQL01 &= "SCLMANNO,"
                    strSQL02 &= "NULL,"

                    strSQL01 &= "CLRKBN1,"
                    strSQL02 &= "'" & Me.btnClrKbn1.Text & "',"
                    strSQL01 &= "CLRKBN2,"
                    strSQL02 &= "'" & Me.btnClrKbn2.Text & "',"
                    strSQL01 &= "CLRKBN3,"
                    strSQL02 &= "'" & Me.btnClrKbn3.Text & "',"
                    strSQL01 &= "CLRKBN4,"
                    strSQL02 &= "NULL,"
                    strSQL01 &= "CLRKBN5,"
                    strSQL02 &= "NULL,"

                    '作成日時
                    strSQL01 &= "INSDTM,"
                    strSQL02 &= "NOW(),"
                    '更新日時
                    strSQL01 &= "UPDDTM"
                    strSQL02 &= "NOW()"

                    strSQL01 &= ") "
                    strSQL02 &= ")"

                    If Not (iDatabase.ExecuteUpdate(strSQL01 & strSQL02)) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01(strMSG, 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If
                End If


                If blnSelectAll Then
                    '全顧客情報のカラー区分更新
                    If Not (iDatabase.ExecuteUpdate("UPDATE CLRSMA SET CLRKBN" & btn.Tag.ToString & "= '" & strSelectColor & "'")) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01(strMSG, 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If
                End If


            Catch ex As Exception
                iDatabase.RollBack()
                Exit Sub
            End Try

            iDatabase.Commit()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 入場取消ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intUDNKN As Integer = 0             '入金額
        Dim intPREMKN As Integer = 0            'プレミアム
        Dim intPOINT As Integer = 0             'ポイント
        Dim blnCANCEL As Boolean = False        '戻るが押されたかどうか
        Dim strDENNO As String = String.Empty   '伝票番号
        Dim intUDNBKN As Integer = 0            '合計金額
        Dim intPreKINGAKU As Integer = 0        'プリペイドの金額
        Dim intZANKN As Integer = 0
        Dim intPREZANKN As Integer = 0
        Dim intV31POINT As Integer = 0
        Dim strZENENTDT As String = String.Empty    '前回来場日
        Dim blnUpdDATKB As Boolean = False          '【True】伝票のDATKBを1に戻す

        Try
            If Not Me.btnCard.Visible Then
                blnCANCEL = True
                Using frm As New frmMSGBOX01(UIMessage.InsertCard, 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If String.IsNullOrEmpty(dcICR700.SYUBETU) Then
                blnCANCEL = True
                Exit Sub
            End If

            'プリカ残金
            Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)

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
            Dim strCARDNO As String = String.Empty
            Dim intSRTPO As Integer = 0

            If dtCSMAST.Rows.Count.Equals(0) Then
                If Not (CType(dcICR700.NCSNO, Integer) >= 50000000) Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    blnCANCEL = True
                    Me.btnCard.PerformClick()
                    Exit Sub
                End If
                strNCSNO = dcICR700.NCSNO
                strCARDNO = dcICR700.CARDNO
                intSRTPO = CType(dcICR700.POINT, Integer)
            Else
                strNCSNO = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                strCARDNO = dtCSMAST.Rows(0).Item("NCARDID").ToString.PadLeft(8, "0"c)
                intSRTPO = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            End If



            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【入場料】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.NYUJO
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.CANCEL
                '顧客番号
                frm.MANNO = strNCSNO

                frm.ShowDialog()

                'キャンセル
                blnCANCEL = frm.CANCEL

                '伝票番号
                strDENNO = frm.DENNO
                '入場料金
                intUDNKN = frm.UDNKN
                'ﾌﾟﾚﾐｱﾑから支払った金額
                intPREMKN = frm.PREMKN
                'ポイント
                intPOINT = frm.POINT
            End Using

            If blnCANCEL Then
                Exit Sub
            Else
                '【取消処理】

                '伝票取消が完了しているのでここからエラーが発生したら伝票取消を戻さないといけない
                blnUpdDATKB = True

                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then
                        blnCANCEL = True
                        Me.btnCard.PerformClick()
                        Exit Sub
                    End If
                    If frm.ERRFLG Then
                        Using frm2 As New frmMSGBOX01("顧客情報が一致しません。", 2)
                            frm2.ShowDialog()
                        End Using
                        blnCANCEL = True
                        Me.btnCard.PerformClick()
                        Exit Sub
                    End If
                End Using

                If Not strCARDNO.Equals(dcICR700.NCSNO) Then
                    Using frm2 As New frmMSGBOX01("顧客情報が一致しません。", 2)
                        frm2.ShowDialog()
                    End Using
                    blnCANCEL = True
                    Me.btnCard.PerformClick()
                    Exit Sub
                End If


                '前回来場日取得
                strZENENTDT = GetZENENTDT(strDENNO)

                intPreKINGAKU = intKINGAKU + intUDNKN

                Dim intpoint2 As Integer = intSRTPO - intPOINT

                If intPreKINGAKU > UIUtility.SYSTEM.ZANMAX Then
                    Using frm As New frmMSGBOX01("入金限度額を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    blnCANCEL = True
                    Me.btnCard.PerformClick()
                    Exit Sub
                ElseIf intPreKINGAKU < 0 Then
                    Using frm As New frmMSGBOX01("残高不足の為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    blnCANCEL = True
                    Me.btnCard.PerformClick()
                    Exit Sub
                ElseIf intpoint2 < 0 Then
                    Using frm As New frmMSGBOX01("ポイントがマイナスになる為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    blnCANCEL = True
                    Me.btnCard.PerformClick()
                    Exit Sub
                End If



                '【プリカRW書き込み情報セット】
                '店番号
                dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
                'パスワード
                dcICR700.PASSCD_WR = "00"
                'シリアルナンバー
                dcICR700.SERIALNO_WR = dcICR700.SERIALNO
                '種別
                dcICR700.SYUBETU_WR = dcICR700.NKBNO
                '金額
                dcICR700.KINGAKU_WR = intPreKINGAKU.ToString.PadLeft(5, "0"c)
                '予備
                dcICR700.YOBI_WR = dcICR700.YOBI

                '【V31RW書き込み情報セット】

                intZANKN = CType(dcICR700.ZANKN, Integer) + (intUDNKN - intPREMKN)
                intPREZANKN = CType(dcICR700.PREZANKN, Integer) + intPREMKN
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
                dcICR700.PASSCD_WR = "00"
                '残金額
                dcICR700.ZANKN_WR = intZANKN.ToString.PadLeft(5, "0"c)
                'P残金額
                dcICR700.PREZANKN_WR = intPREZANKN.ToString.PadLeft(5, "0"c)
                '残ポイント
                dcICR700.POINT_WR = intV31POINT.ToString.PadLeft(5, "0"c)
                '前回来場日
                dcICR700.ZENENTDATE_WR = strZENENTDT
                '入場区分
                dcICR700.ENTKBN_WR = "0"
                'ボール単価
                dcICR700.BALLKIN_WR = dcICR700.BALLKIN
                '打席番号
                dcICR700.SEATNO_WR = dcICR700.SEATNO

                '【データベース更新処理】
                If Not (CType(strNCSNO, Integer) >= 50000000) Then
                    Do
                        'トランザクション開始
                        iDatabase.BeginTransaction()

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
                            Exit Sub
                        End If
                        '【ポイントサマリ】
                        sql.Clear()
                        sql.Append("UPDATE DPOINTSMA SET")
                        sql.Append(" SRTPO = " & intV31POINT)
                        sql.Append(",UPDDTM = NOW()")
                        sql.Append(" WHERE")
                        sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            iDatabase.RollBack()
                            Exit Sub
                        End If

                        If Not strZENENTDT.Equals(UIUtility.SYSTEM.UPDDAY) Then
                            '【顧客情報更新】
                            sql.Clear()
                            sql.Append("UPDATE CSMAST SET")
                            sql.Append(" ZENENTDATE = '" & strZENENTDT & "'")
                            sql.Append(",ENTDT = '" & strZENENTDT & "'")
                            sql.Append(",ENTCNT = ENTCNT - 1")
                            sql.Append(",ENTCNT2 = ENTCNT2 - 1")
                            sql.Append(",DUPDATE = NOW()")
                            sql.Append(" WHERE")
                            sql.Append(" NCSNO = " & dtCSMAST.Rows(0).Item("NCSNO").ToString)
                            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                                iDatabase.RollBack()
                                Using frm As New frmMSGBOX01("顧客情報の更新に失敗しました。", 2)
                                    frm.ShowDialog()
                                End Using
                                Exit Sub
                            End If
                        End If

                        Exit Do
                    Loop
                End If


                '【カード書き込み】
                Dim blnERRFLG As Boolean = False
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    If Not (CType(strNCSNO, Integer) >= 50000000) Then iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    'Me.pnlEIGMTA.Enabled = False
                    'Me.pnlEIGMTB.Enabled = False

                    '入場取消ボタン
                    Me.btnCancel.Enabled = False
                    '残高移行ボタン
                    Me.btnZANIKO01.Enabled = True
                    '商品引落しボタン
                    Me.btnHINDISP01.Enabled = True
                    '顧客情報ボタン
                    Me.btnCSMAST01.Enabled = True
                    'Me.btnNKNDISP02.Enabled = True
                    Exit Sub
                End If

                If Not (CType(strNCSNO, Integer) >= 50000000) Then
                    'コミット
                    iDatabase.Commit()
                End If

                'ここまできたら戻さなくてＯＫ
                blnUpdDATKB = False

                Me.btnCard.Visible = False
                Me.btnCard.Tag = 0


                '入場取消ボタン
                Me.btnCancel.Enabled = False
                '残高移行ボタン
                Me.btnZANIKO01.Enabled = True
                '商品引落しボタン
                Me.btnHINDISP01.Enabled = True
                '顧客情報ボタン
                Me.btnCSMAST01.Enabled = True
                'Me.btnNKNDISP02.Enabled = True

            End If

            '顧客情報表示
            If strNCSNO >= "50000000" Then
                SetFreeCard(dcICR700.SYUBETU, intPreKINGAKU, String.Empty)
            Else
                SetCSMAST(dcICR700.SYUBETU, intPreKINGAKU, String.Empty)
            End If





        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If blnUpdDATKB Then
                '伝票情報を戻す
                UIFunction.UpdBackDATKB(5, CType(strDENNO, Integer), iDatabase)
            End If
            If Not blnCANCEL Then
                ButtonFontColor(CType(sender, Button), 1)
                'カード要求開始
                ReadStart()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 打席情報ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSEATINFO01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSEATINFO01.Click
        Try
            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            Using frm As New frmSEATSELECT01(iDatabase)
                frm.MODE = frmSEATSELECT01.Mode_Type.NORMAL
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try
    End Sub


    ''' <summary>
    ''' 顧客情報ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCSMAST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSMAST01.Click
        Try
            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            _intSwitch = 1

            If Me.btnCard.Visible And Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                Using frm As New frmCSMAST01(iDatabase, dcICR700, CType(Me.lblNCSNO.Text, Integer))
                    frm.ShowDialog()
                End Using
                Me.btnCard.Tag = 0
            Else
                Using frm As New frmCSMAST01(iDatabase, dcICR700)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try

    End Sub

    ''' <summary>
    ''' 商品引落しボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHINDISP01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHINDISP01.Click
        Dim bln As Boolean = False
        Try

            If Not Me.Panel1.Enabled Then Exit Sub

            ButtonFontColor(CType(sender, Button), 0)
            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1


                '【カード挿入状態】
                bln = True

            Else
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1
            End If

            Using frm As New frmHINDISP01(iDatabase, dcICR700)
                If Me.btnCard.Visible Then
                    frm.NCSNO = Me.lblNCSNO.Text
                    frm.CCSNAME = Me.lblCCSNAME.Text
                End If
                frm.ShowDialog()
            End Using

            If bln Then
                Me.btnCard.Tag = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            'カード要求開始
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()

        End Try
    End Sub

    ''' <summary>
    ''' 入場履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnENTHIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnENTHIST01.Click
        Try
            If Not Me.Panel1.Enabled Then Exit Sub

            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            Using frm As New frmENTHIST01(iDatabase)
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try
    End Sub

    ''' <summary>
    ''' 残高移行ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnZANIKO01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZANIKO01.Click
        Try
            If Not Me.Panel1.Enabled Then Exit Sub

            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            Using frm As New frmZANIKO01(iDatabase, dcICR700)
                frm.PreKingaku = _intPreKingaku
                frm.ShowDialog()
                _intPreKingaku = frm.PreKingaku
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If Me.btnCard.Visible Then
                Me.btnCard.PerformClick()
            Else
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
            End If
        End Try
    End Sub


    ''' <summary>
    ''' コース料金精算ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNDISP03_Click(sender As System.Object, e As System.EventArgs) Handles btnNKNDISP03.Click
        Dim bln As Boolean = False
        Try

            If Not Me.Panel1.Enabled Then Exit Sub

            ButtonFontColor(CType(sender, Button), 0)
            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1


                '【カード挿入状態】
                bln = True

                'Me.Panel1.Enabled = False
                'Me.Panel2.Enabled = False
            Else
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1
            End If

            Using frm As New frmNKNDISP03(iDatabase, dcICR700, bln)
                frm.ShowDialog()
            End Using


            'Me.pnlEIGMTA.Enabled = False
            'Me.pnlEIGMTB.Enabled = False

            If bln Then
                Me.btnCard.Tag = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            'カード要求開始
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()

        End Try
    End Sub

    ''' <summary>
    ''' 入金ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNDISP01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNDISP01.Click
        Dim bln As Boolean = False
        Try

            If Not Me.Panel1.Enabled Then Exit Sub

            ButtonFontColor(CType(sender, Button), 0)
            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1


                '【カード挿入状態】
                bln = True

                'Me.Panel1.Enabled = False
                'Me.Panel2.Enabled = False
            Else
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1
            End If

            Using frm As New frmNKNDISP01(iDatabase, dcICR700, bln)
                frm.ShowDialog()
            End Using


            'Me.pnlEIGMTA.Enabled = False
            'Me.pnlEIGMTB.Enabled = False

            If bln Then
                Me.btnCard.Tag = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            'カード要求開始
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()

        End Try
    End Sub

    ''' <summary>
    ''' サービス入金ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNDISP02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNDISP02.Click
        Dim bln As Boolean = False
        Try

            If Not Me.Panel1.Enabled Then Exit Sub

            ButtonFontColor(CType(sender, Button), 0)
            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1


                '【カード挿入状態】
                bln = True

                'Me.Panel1.Enabled = False
                'Me.Panel2.Enabled = False
            Else
                'カード要求ストップ
                If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()
                _intSwitch = 1
            End If

            Using frm As New frmNKNDISP02(iDatabase, dcICR700, bln)
                frm.ShowDialog()
            End Using


            'Me.pnlEIGMTA.Enabled = False
            'Me.pnlEIGMTB.Enabled = False

            If bln Then
                Me.btnCard.Tag = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            'カード要求開始
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()

        End Try
    End Sub

    ''' <summary>
    ''' カード排出ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard.Click
        Dim blnERRFLG As Boolean = False        'エラーが発生したかどうか
        Try
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then Exit Sub

            Me.chkFREE.Checked = False
            Me.picBirthday.Visible = False

            'カード排出コマンド
            If Not _blnNoEject Then
                Using frm As New frmREQUESTCARD(dcICR700)
                    frmWait.Close()
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            Else
                _blnNoEject = False
            End If


            'Me.pnlEIGMTA.Enabled = False
            'Me.pnlEIGMTB.Enabled = False

            '入場取消ボタン
            Me.btnCancel.Enabled = False
            '残高移行ボタン
            Me.btnZANIKO01.Enabled = True
            '顧客情報ボタン
            Me.btnCSMAST01.Enabled = True
            '商品引落しボタン
            Me.btnHINDISP01.Enabled = True
            '期限更新ボタン
            btnUpdDMEMBER.Enabled = False
            Me.btnNKNDISP02.Enabled = True


            Me.btnCard.Tag = 0
            Me.btnCard.Visible = False

            If Not blnERRFLG Then
                'カード要求開始
                ReadStart()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' カード取引履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCARDTORIIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARDTORIHIST01.Click
        Try
            Using frm As New frmCARDTORIHIST01(iDatabase)
                frm.MANNO = Me.lblNCSNO.Text
                frm.CCSNAME = Me.lblCCSNAME.Text
                frm.ZANKN = CType(dcICR700.ZANKN, Integer)
                frm.PREZANKN = CType(dcICR700.PREZANKN, Integer)
                frm.SRTPO = CType(dcICR700.POINT, Integer)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 期限更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdDMEMBER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdDMEMBER.Click
        Try
            If String.IsNullOrEmpty(Me.lblDMEMBER.Text) Then
                Using frm As New frmMSGBOX01("会員期限が登録されていません。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If Not Me.btnCard.Visible Then
                Using frm As New frmMSGBOX01(UIMessage.InsertCard, 3)
                    frm.ShowDialog()
                End Using
                Me.btnUpdDMEMBER.Enabled = False
                Exit Sub
            End If

            Dim dtDMEMBER As DateTime = DateTime.Parse(Me.lblDMEMBER.Text)
            Dim strUpdDMEMBER As String = String.Empty
            strUpdDMEMBER = dtDMEMBER.AddMonths(CType(Me.btnUpdDMEMBER.Tag, Integer)).ToString("yyyy/MM/dd")

            '会員期限調整日数反映
            Dim dtDMEMBER2 As DateTime = DateTime.Parse(strUpdDMEMBER)
            strUpdDMEMBER = dtDMEMBER2.AddDays(UIUtility.SYSTEM.CALLMT).ToString("yyyy/MM/dd")

            If Me.lblDMEMBER.ForeColor = Color.Red Then
                '期限切れ
                strUpdDMEMBER = Now.AddMonths(CType(Me.btnUpdDMEMBER.Tag, Integer)).ToString("yyyy/MM/dd")
            End If

            Using frm As New frmMSGBOX01("会員期限を" & strUpdDMEMBER & "で更新しますか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '*** 【スタッフ確認】 ***'
            _strSTFCODE = String.Empty
            _strSTFNAME = String.Empty
            If UIUtility.SYSTEM.TANCHKFLG.Equals(1) And UIFunction.ChkPgScrty("FEE", iDatabase) Then
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

            '年会費精算処理
            Dim strMsg As String = String.Empty
            Dim intCKBFEE As Integer = 0
            Dim dr As DataRow()
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & CType(_dtCSMAST.Rows(0).Item("NCSRANK").ToString(), Integer))
            If dr.Length > 0 Then intCKBFEE = CType(dr(0).Item("CKBFEE").ToString, Integer)

            iDatabase.BeginTransaction()

            Dim intPaySelect As Integer = 0

            If Not UpdCKBFEE(strMsg, intCKBFEE, intPaySelect) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("年会費の精算に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If intPaySelect.Equals(0) Then
                iDatabase.RollBack()
                Exit Sub
            End If

            '期限更新
            If Not UIFunction.UpdDMEMBER(Me.lblNCSNO.Text, strUpdDMEMBER, iDatabase) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("会員期限の更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If intPaySelect.Equals(2) And intCKBFEE > 0 Then
                '【打席カード】

                If Not CheckCard() Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("顧客情報が一致しません。", 3)
                        frm.ShowDialog()
                    End Using
                    intPaySelect = 0
                    intCKBFEE = 0
                    'カード要求開始
                    ReadStart()
                    Exit Sub
                End If


                '【カード書き込み】
                Dim blnERRFLG As Boolean = False
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.EJECTSTOP = True

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
                'カード要求開始
                ReadStart()
            End If


            iDatabase.Commit()

            Me.lblDMEMBER.ForeColor = Color.Blue
            Me.lblDMEMBER.Text = strUpdDMEMBER


        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 入場料・ポイント無しチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkFREE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFREE.CheckedChanged
        Try
            Me.chkFREE.ForeColor = Color.Black

            Me.lblENTKIN01.Text = CType(Me.lblENTKIN01.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN02.Text = CType(Me.lblENTKIN02.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN03.Text = CType(Me.lblENTKIN03.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN04.Text = CType(Me.lblENTKIN04.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN05.Text = CType(Me.lblENTKIN05.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN06.Text = CType(Me.lblENTKIN06.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN07.Text = CType(Me.lblENTKIN07.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN08.Text = CType(Me.lblENTKIN08.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN09.Text = CType(Me.lblENTKIN09.Tag, Integer).ToString("#,##0")
            Me.lblENTKIN10.Text = CType(Me.lblENTKIN10.Tag, Integer).ToString("#,##0")

            'Me.lblJKNKIN01.Text = CType(Me.lblJKNKIN01.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN02.Text = CType(Me.lblJKNKIN02.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN03.Text = CType(Me.lblJKNKIN03.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN04.Text = CType(Me.lblJKNKIN04.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN05.Text = CType(Me.lblJKNKIN05.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN06.Text = CType(Me.lblJKNKIN06.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN07.Text = CType(Me.lblJKNKIN07.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN08.Text = CType(Me.lblJKNKIN08.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN09.Text = CType(Me.lblJKNKIN09.Tag, Integer).ToString("#,##0")
            'Me.lblJKNKIN10.Text = CType(Me.lblJKNKIN10.Tag, Integer).ToString("#,##0")
            Dim intWaribiki As Integer = 0
            If Me.chkFREE.Checked Then
                Select Case dcICR700.NKBNO
                    Case "1" : intWaribiki = CType(Me.lblENTKIN01.Tag, Integer)
                    Case "2" : intWaribiki = CType(Me.lblENTKIN02.Tag, Integer)
                    Case "3" : intWaribiki = CType(Me.lblENTKIN03.Tag, Integer)
                    Case "4" : intWaribiki = CType(Me.lblENTKIN04.Tag, Integer)
                    Case "5" : intWaribiki = CType(Me.lblENTKIN05.Tag, Integer)
                    Case "6" : intWaribiki = CType(Me.lblENTKIN06.Tag, Integer)
                    Case "7" : intWaribiki = CType(Me.lblENTKIN07.Tag, Integer)
                    Case "8" : intWaribiki = CType(Me.lblENTKIN08.Tag, Integer)
                    Case "9" : intWaribiki = CType(Me.lblENTKIN09.Tag, Integer)
                    Case Else : intWaribiki = CType(Me.lblENTKIN10.Tag, Integer)
                End Select
            End If
            If Me.chkFREE.Checked Then
                Me.chkFREE.ForeColor = Color.Red

                Me.lblENTKIN01.Text = "0"
                Me.lblENTKIN02.Text = "0"
                Me.lblENTKIN03.Text = "0"
                Me.lblENTKIN04.Text = "0"
                Me.lblENTKIN05.Text = "0"
                Me.lblENTKIN06.Text = "0"
                Me.lblENTKIN07.Text = "0"
                Me.lblENTKIN08.Text = "0"
                Me.lblENTKIN09.Text = "0"
                Me.lblENTKIN10.Text = "0"

                'If Me.lblJKNPOINT01.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN01.Text = (CType(Me.lblJKNKIN01.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT02.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN02.Text = (CType(Me.lblJKNKIN02.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT03.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN03.Text = (CType(Me.lblJKNKIN03.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT04.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN04.Text = (CType(Me.lblJKNKIN04.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT05.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN05.Text = (CType(Me.lblJKNKIN05.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT06.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN06.Text = (CType(Me.lblJKNKIN06.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT07.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN07.Text = (CType(Me.lblJKNKIN07.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT08.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN08.Text = (CType(Me.lblJKNKIN08.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT09.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN09.Text = (CType(Me.lblJKNKIN09.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
                'If Me.lblJKNPOINT10.Tag.ToString.Equals("0") Then
                '    Me.lblJKNKIN10.Text = (CType(Me.lblJKNKIN10.Text, Integer) - intWaribiki).ToString("#,##0")
                'End If
            End If

            If String.IsNullOrEmpty(Me.btnNKBNO01.Text) Then Me.lblENTKIN01.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO02.Text) Then Me.lblENTKIN02.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO03.Text) Then Me.lblENTKIN03.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO04.Text) Then Me.lblENTKIN04.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO05.Text) Then Me.lblENTKIN05.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO06.Text) Then Me.lblENTKIN06.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO07.Text) Then Me.lblENTKIN07.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO08.Text) Then Me.lblENTKIN08.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO09.Text) Then Me.lblENTKIN09.Text = String.Empty
            If String.IsNullOrEmpty(Me.btnNKBNO10.Text) Then Me.lblENTKIN10.Text = String.Empty

            'If Not Me.btnJKNKB01.Visible Then Me.lblJKNKIN01.Text = String.Empty
            'If Not Me.btnJKNKB02.Visible Then Me.lblJKNKIN02.Text = String.Empty
            'If Not Me.btnJKNKB03.Visible Then Me.lblJKNKIN03.Text = String.Empty
            'If Not Me.btnJKNKB04.Visible Then Me.lblJKNKIN04.Text = String.Empty
            'If Not Me.btnJKNKB05.Visible Then Me.lblJKNKIN05.Text = String.Empty
            'If Not Me.btnJKNKB06.Visible Then Me.lblJKNKIN06.Text = String.Empty
            'If Not Me.btnJKNKB07.Visible Then Me.lblJKNKIN07.Text = String.Empty
            'If Not Me.btnJKNKB08.Visible Then Me.lblJKNKIN08.Text = String.Empty
            'If Not Me.btnJKNKB09.Visible Then Me.lblJKNKIN09.Text = String.Empty
            'If Not Me.btnJKNKB10.Visible Then Me.lblJKNKIN10.Text = String.Empty

            GetEIGMTB()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 発行料入金機精算チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkHakko_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHakko.CheckedChanged
        Try
            If Me.chkHakko.Checked Then
                Me.chkHakko.ForeColor = Color.Red
            Else
                Me.chkHakko.ForeColor = Color.Black
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 1球貸し種別ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKBNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKBNO01.Click, btnNKBNO02.Click, btnNKBNO03.Click, btnNKBNO04.Click, btnNKBNO05.Click _
                                                                                    , btnNKBNO06.Click, btnNKBNO07.Click, btnNKBNO08.Click, btnNKBNO09.Click, btnNKBNO10.Click
        Dim strSERIALNO As String = "FFF"       'シリアルナンバー
        Dim strCKBNAME As String = String.Empty '種別名
        Dim strNKBNO As String = "1"            '顧客種別
        Dim strYOBI As String = "F"             '予備
        Dim intENTKIN As Integer = 0            '入場料
        Dim intTAX As Integer = 0               '消費税
        Dim intPOINT As Integer = 0             '入場ポイント
        Dim intPOINTW As Integer = 0            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
        Dim intPOINTS As Integer = 0            'ｼﾆｱﾎﾟｲﾝﾄ
        Dim intBALLKIN1F As Integer = 0         'ボール単価1F
        Dim intBALLKIN2F As Integer = 0         'ボール単価2F
        Dim intBALLKIN3F As Integer = 0         'ボール単価3F
        Dim intAftKINGAKU As Integer            '処理後残金額
        Dim blnErrZANKN As Boolean = False      '残金不足なら【True】
        Dim intSELECTSEAT As Integer = 0
        Dim blnCard As Boolean = True

        Try
            Me.Cursor = Cursors.WaitCursor

            If Not Me.btnCard.Visible Then
                ReadStop()
                Using frm As New frmMSGBOX01(UIMessage.InsertCard, 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If String.IsNullOrEmpty(dcICR700.SYUBETU) Then
                blnErrZANKN = True
                blnCard = True
                Exit Sub
            End If

            '受付たｶｰﾄﾞが同一か確認
            If Not CheckCard() Then
                Using frm As New frmMSGBOX01("最初に受付けたカードと異なります。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
                blnErrZANKN = True
                blnCard = True
                Exit Sub
            End If

            Dim btnNKBNO As Button
            btnNKBNO = CType(sender, Button)

            '打席指定あり
            If UIUtility.SYSTEM.SITEIKBN.Equals(1) Then
                Using frm As New frmSEATSELECT01(iDatabase)
                    frm.MODE = frmSEATSELECT01.Mode_Type.CHOICE
                    frm.CCSNAME = Me.lblCCSNAME.Text & "【" & Me.lblNCSNO.Text & "】"
                    frm.ShowDialog()
                    intSELECTSEAT = frm.SELECTSEAT
                End Using
                If intSELECTSEAT.Equals(0) Then
                    blnErrZANKN = True
                    Exit Sub
                End If
                Me.lblSELECTSEAT.Text = intSELECTSEAT.ToString & "番"
            End If


            strCKBNAME = btnNKBNO.Text
            Select Case CType(btnNKBNO.Tag, Integer)
                Case 1
                    '【種別1】
                    strNKBNO = "1"
                    intENTKIN = CType(Me.lblENTKIN01.Text, Integer)
                    intPOINT = CType(Me.lblPOINT01.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW01.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS01.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F01.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F01.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F01.Tag, Integer)
                Case 2
                    '【種別2】
                    strNKBNO = "2"
                    intENTKIN = CType(Me.lblENTKIN02.Text, Integer)
                    intPOINT = CType(Me.lblPOINT02.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW02.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS02.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F02.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F02.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F02.Tag, Integer)
                Case 3
                    '【種別3】
                    strNKBNO = "3"
                    intENTKIN = CType(Me.lblENTKIN03.Text, Integer)
                    intPOINT = CType(Me.lblPOINT03.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW03.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS03.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F03.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F03.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F03.Tag, Integer)
                Case 4
                    '【種別4】
                    strNKBNO = "4"
                    intENTKIN = CType(Me.lblENTKIN04.Text, Integer)
                    intPOINT = CType(Me.lblPOINT04.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW04.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS04.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F04.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F04.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F04.Tag, Integer)
                Case 5
                    '【種別5】
                    strNKBNO = "5"
                    intENTKIN = CType(Me.lblENTKIN05.Text, Integer)
                    intPOINT = CType(Me.lblPOINT05.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW05.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS05.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F05.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F05.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F05.Tag, Integer)
                Case 6
                    '【種別6】
                    strNKBNO = "6"
                    intENTKIN = CType(Me.lblENTKIN06.Text, Integer)
                    intPOINT = CType(Me.lblPOINT06.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW06.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS06.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F06.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F06.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F06.Tag, Integer)
                Case 7
                    '【種別7】
                    strNKBNO = "7"
                    intENTKIN = CType(Me.lblENTKIN07.Text, Integer)
                    intPOINT = CType(Me.lblPOINT07.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW07.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS07.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F07.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F07.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F07.Tag, Integer)
                Case 8
                    '【種別8】
                    strNKBNO = "8"
                    intENTKIN = CType(Me.lblENTKIN08.Text, Integer)
                    intPOINT = CType(Me.lblPOINT08.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW08.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS08.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F08.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F08.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F08.Tag, Integer)
                Case 9
                    '【種別9】
                    strNKBNO = "9"
                    intENTKIN = CType(Me.lblENTKIN09.Text, Integer)
                    intPOINT = CType(Me.lblPOINT09.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW09.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS09.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F09.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F09.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F09.Tag, Integer)
                Case 10
                    '【種別10】
                    strNKBNO = "A"
                    intENTKIN = CType(Me.lblENTKIN10.Text, Integer)
                    intPOINT = CType(Me.lblPOINT10.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW10.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS10.Text, Integer)
                    intBALLKIN1F = CType(Me.lblBALLKIN1F10.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F10.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F10.Tag, Integer)
            End Select

            _strNextPASSCD = String.Empty
            'If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
            '    '次の時間帯以降のﾊﾟｽﾜｰﾄﾞ00以外の時間帯の情報書き込み
            '    If strNKBNO.Equals("A") Then
            '        GetNextEIGMTA(10, intENTKIN, intPOINT)
            '    Else
            '        GetNextEIGMTA(CType(strNKBNO, Integer), intENTKIN, intPOINT)
            '    End If
            'End If
            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            If Me.lblPOINTW.Visible Then
                intPOINT += intPOINTW
            End If
            'ｼﾆｱﾎﾟｲﾝﾄ
            If Me.lblPOINTS.Visible Then
                intPOINT += intPOINTS
            End If

            'コース来場回数確認
            If UIFunction.GetKosuEnt(Me.lblNCSNO.Text, iDatabase) > 0 Then
                intPOINT = 0
            End If

            '月間来場ポイント加算
            If Not Me.lblZENENTDATE.Text.Replace("/", String.Empty).Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                intPOINT += _intETPPO
            End If
            '誕生月ポイント加算
            intPOINT += _intBIRTHMPO
            '誕生日ポイント加算
            intPOINT += _intBIRTHDPO

            '入場料・ﾎﾟｲﾝﾄなし
            If chkFREE.Checked Then
                intENTKIN = 0
                intPOINT = 0
            End If

            '消費税
            If intENTKIN > 0 Then
                intTAX = UIFunction.CalcExcludedTax(intENTKIN)
            End If


            '残金で足りるか
            intAftKINGAKU = (CType(Me.lblKINGAKU.Text, Integer) - intENTKIN)
            If intAftKINGAKU < 0 Then
                blnErrZANKN = True
                Using frm As New frmMSGBOX01("カード残金が不足しています。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            '【残金整合処理】
            'ICカード残金
            Dim intIcZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'ICカード残プレミアム
            Dim intIcPREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)


            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0

            If intIcPREZANKN >= intENTKIN Then
                '【残プレミアム >= 入場料金】
                intIcPREZANKN -= intENTKIN
                'プレミアムから支払った金額
                intPayPREMKN = intENTKIN
            Else
                intIcZANKN = intIcZANKN - (intENTKIN - intIcPREZANKN)
                '残金から支払った金額
                intPayKINGAKU = (intENTKIN - intIcPREZANKN)
                'プレミアムから支払った金額
                intPayPREMKN = intIcPREZANKN
                intIcPREZANKN = 0
            End If

            '【ICカードセクタ２書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            If String.IsNullOrEmpty(_strNextPASSCD) Then
                dcICR700.PASSCD_WR = UIUtility.SYSTEM.NOWPASSCD
            Else
                dcICR700.PASSCD_WR = _strNextPASSCD
            End If

            'シリアルナンバー
            dcICR700.SERIALNO_WR = strSERIALNO
            '種別
            dcICR700.SYUBETU_WR = strNKBNO
            '金額
            dcICR700.KINGAKU_WR = intAftKINGAKU.ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = strYOBI

            '【ICカードセクタ３、４書き込み情報セット】
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
            If dcICR700.NCSNO >= "50000000" Then
                dcICR700.NKBNO_WR = dcICR700.NKBNO
            Else
                If _dtCSMAST.Rows(0).Item("NCSRANK").ToString.Equals("10") Then
                    dcICR700.NKBNO_WR = "A"
                Else
                    dcICR700.NKBNO_WR = _dtCSMAST.Rows(0).Item("NCSRANK").ToString
                End If
            End If
            '会員期限
            dcICR700.DMEMBER_WR = dcICR700.DMEMBER
            'パスワード
            If String.IsNullOrEmpty(_strNextPASSCD) Then
                dcICR700.PASSCD_WR = UIUtility.SYSTEM.NOWPASSCD
            Else
                dcICR700.PASSCD_WR = _strNextPASSCD
            End If

            '残金額
            dcICR700.ZANKN_WR = intIcZANKN.ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = intIcPREZANKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            If dcICR700.NCSNO >= "50000000" Then
                dcICR700.POINT_WR = (CType(dcICR700.POINT, Integer) + intPOINT).ToString.PadLeft(5, "0"c)
            Else
                dcICR700.POINT_WR = (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intPOINT).ToString.PadLeft(5, "0"c)
            End If
            '前回来場日
            dcICR700.ZENENTDATE_WR = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            '入場区分
            If Me.chkHakko.Checked Then
                dcICR700.ENTKBN_WR = "1"
            Else
                dcICR700.ENTKBN_WR = "0"
            End If
            'ボール単価
            dcICR700.BALLKIN_WR = "00"
            ' 【打席番号】
            dcICR700.SEATNO_WR = "FFF"
            If intSELECTSEAT > 0 Then
                dcICR700.SEATNO_WR = intSELECTSEAT.ToString.PadLeft(3, "0"c)
            End If

            Me.Panel1.Enabled = False
            Me.Panel1.Refresh()
            frmWait.Show()
            frmWait.Refresh()

            '*** 【データベース更新処理】***'

            'トランザクション開始
            iDatabase.BeginTransaction()

            Dim intDelDENNO As Integer = 0
            Dim strMsg As String = String.Empty
            If Not UpdDatabase(1, strNKBNO, intIcZANKN, intIcPREZANKN, intPOINT, intENTKIN, intTAX, intPayKINGAKU, intPayPREMKN, strMsg, intDelDENNO) Then
                Using frm As New frmMSGBOX01(strMsg, 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If
            'コミット
            iDatabase.Commit()

            iDatabase.BeginTransaction()

            If Not dcICR700.NCSNO >= "50000000" Then
                Dim sql As New System.Text.StringBuilder
                '【顧客情報更新】
                sql.Clear()
                sql.Append("UPDATE CSMAST SET")
                sql.Append(" ZENENTDATE = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c) & "'")
                sql.Append(",ENTDT = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c) & "'")
                If Not Me.lblZENENTDATE.Text.Replace("/", String.Empty).Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                    '【本日初来場の場合】
                    sql.Append(",ENTCNT = ENTCNT + 1")
                    sql.Append(",ENTCNT2 = ENTCNT2 + 1")
                End If
                'If Not dcICR700.ZENENTDATE.Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                '    '【本日初来場の場合】
                '    sql.Append(",ENTCNT = ENTCNT + 1")
                '    sql.Append(",ENTCNT2 = ENTCNT2 + 1")
                'End If
                'カード有効期限
                If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                    Dim dtCARDLIMIT As DateTime = Now
                    sql.Append(",CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
                Else
                    sql.Append(",CARDLIMIT = NULL")
                End If

                sql.Append(",DUPDATE = NOW()")
                sql.Append(" WHERE")
                sql.Append(" NCSNO = " & CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "顧客情報の更新に失敗しました。"
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                    CardEject()
                    DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Exit Sub
                End If

                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & intIcZANKN)
                sql.Append(",PREZANKN = " & intIcPREZANKN)
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                    CardEject()
                    DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Exit Sub
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intPOINT))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "ポイントサマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                    CardEject()
                    DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Exit Sub
                End If
            End If


            blnCard = False

            frmWait.Close()
            Me.Panel1.Enabled = True

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
                DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Exit Sub
            End If

            iDatabase.Commit()

            '顧客情報表示
            If dcICR700.NCSNO >= "50000000" Then
                SetFreeCard(strNKBNO, intAftKINGAKU, String.Empty)
            Else
                SetCSMAST(strNKBNO, intAftKINGAKU, String.Empty)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not blnErrZANKN Or Not blnCard Then

                '入場料取消ボタン
                Me.btnCancel.Enabled = False
                '残高移行ボタン
                Me.btnZANIKO01.Enabled = True
                '顧客情報ボタン
                Me.btnCSMAST01.Enabled = True
                '商品引落しボタン
                Me.btnHINDISP01.Enabled = True
                '入場料・ﾎﾟｲﾝﾄなし
                Me.chkFREE.Checked = False
                Me.btnNKNDISP02.Enabled = True

                Me.btnCard.Visible = False
                Me.btnCard.Tag = 0
                'カード要求開始
                ReadStart()
            End If
            frmWait.Close()
            Me.Panel1.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 打ち放題ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnJKNKB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJKNKB04.Click, btnJKNKB05.Click _
                                                                                    , btnJKNKB06.Click, btnJKNKB07.Click, btnJKNKB08.Click, btnJKNKB09.Click, btnJKNKB10.Click, btnJKNKB03.Click, btnJKNKB02.Click, btnJKNKB01.Click
        Dim intJKNMM As Integer = 0             '貸し時間
        Dim intJKNKIN As Integer = 0            '料金
        Dim intJKNTAX As Integer = 0            '消費税
        Dim intAftKINGAKU As Integer            '処理後残金額
        Dim intFLR As Integer = 0               'フロア情報 
        Dim intPOINT As Integer = 0
        Dim intPOINTW As Integer = 0
        Dim intPOINTS As Integer = 0
        Dim strSERIALNO As String = "FF"
        Dim intSELECTSEAT As Integer = 0
        Dim blnCard As Boolean = True
        Try
            Me.Cursor = Cursors.WaitCursor

            If Not Me.btnCard.Visible Then
                ReadStop()
                Using frm As New frmMSGBOX01(UIMessage.InsertCard, 3)
                    frm.ShowDialog()
                End Using
                ReadStart()
                Exit Sub
            End If

            If String.IsNullOrEmpty(dcICR700.SYUBETU) Then
                blnCard = True
                Exit Sub
            End If

            '受付たｶｰﾄﾞが同一か確認
            If Not CheckCard() Then
                ReadStop()
                Using frm As New frmMSGBOX01("最初に受付けたカードと異なります。", 3)
                    frm.ShowDialog()
                End Using
                ReadStart()
                Exit Sub
            End If

            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                ReadStop()
                Using frm As New frmMSGBOX01("現在の時間帯は打ち放題を利用できません。", 3)
                    frm.ShowDialog()
                End Using
                ReadStart()
                Exit Sub
            End If

            Dim btnJKNKB As Button
            btnJKNKB = CType(sender, Button)

            Select Case CType(btnJKNKB.Tag, Integer)
                Case 1
                    '【時間区分１】

                    intJKNMM = CType(Me.lblJKNMM01.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN01.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX01.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR01.Tag, Integer)

                Case 2
                    '【時間区分2】

                    intJKNMM = CType(Me.lblJKNMM02.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN02.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX02.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR02.Tag, Integer)
                Case 3
                    '【時間区分3】

                    intJKNMM = CType(Me.lblJKNMM03.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN03.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX03.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR03.Tag, Integer)
                Case 4
                    '【時間区分4】

                    intJKNMM = CType(Me.lblJKNMM04.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN04.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX04.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR04.Tag, Integer)
                Case 5
                    '【時間区分5】

                    intJKNMM = CType(Me.lblJKNMM05.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN05.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX05.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR05.Tag, Integer)
                Case 6
                    '【時間区分6】

                    intJKNMM = CType(Me.lblJKNMM06.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN06.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX06.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR06.Tag, Integer)
                Case 7
                    '【時間区分7】

                    intJKNMM = CType(Me.lblJKNMM07.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN07.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX07.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR07.Tag, Integer)
                Case 8
                    '【時間区分8】

                    intJKNMM = CType(Me.lblJKNMM08.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN08.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX08.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR08.Tag, Integer)
                Case 9
                    '【時間区分9】

                    intJKNMM = CType(Me.lblJKNMM09.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN09.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX09.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR09.Tag, Integer)
                Case 10
                    '【時間区分10】

                    intJKNMM = CType(Me.lblJKNMM10.Text, Integer)
                    intJKNKIN = CType(Me.lblJKNKIN10.Text, Integer)
                    intJKNTAX = CType(Me.lblJKNTAX10.Text, Integer)
                    intFLR = CType(Me.lblJKNFLR10.Tag, Integer)
            End Select
            Dim intNCSRANK As Integer = 1
            If dcICR700.NCSNO >= "50000000" Then
                If dcICR700.NKBNO.Equals("A") Then
                    intNCSRANK = 10
                Else
                    intNCSRANK = CType(dcICR700.SYUBETU, Integer)
                End If
            Else
                intNCSRANK = CType(_dtCSMAST.Rows(0).Item("NCSRANK").ToString, Integer)
            End If
            Select Case intNCSRANK
                Case 1
                    '【種別1】
                    intPOINT = CType(Me.lblPOINT01.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW01.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS01.Text, Integer)
                Case 2
                    '【種別2】
                    intPOINT = CType(Me.lblPOINT02.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW02.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS02.Text, Integer)
                Case 3
                    '【種別3】
                    intPOINT = CType(Me.lblPOINT03.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW03.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS03.Text, Integer)
                Case 4
                    '【種別4】
                    intPOINT = CType(Me.lblPOINT04.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW04.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS04.Text, Integer)
                Case 5
                    '【種別5】
                    intPOINT = CType(Me.lblPOINT05.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW05.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS05.Text, Integer)
                Case 6
                    '【種別6】
                    intPOINT = CType(Me.lblPOINT06.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW06.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS06.Text, Integer)
                Case 7
                    '【種別7】
                    intPOINT = CType(Me.lblPOINT07.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW07.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS07.Text, Integer)
                Case 8
                    '【種別8】
                    intPOINT = CType(Me.lblPOINT08.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW08.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS08.Text, Integer)
                Case 9
                    '【種別9】
                    intPOINT = CType(Me.lblPOINT09.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW09.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS09.Text, Integer)
                Case 10
                    '【種別10】
                    intPOINT = CType(Me.lblPOINT10.Text, Integer)
                    intPOINTW = CType(Me.lblPOINTW10.Text, Integer)
                    intPOINTS = CType(Me.lblPOINTS10.Text, Integer)
            End Select

            intJKNTAX = UIFunction.CalcExcludedTax(intJKNKIN)

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            If Me.lblPOINTW.Visible Then
                intPOINT += intPOINTW
            End If
            'ｼﾆｱﾎﾟｲﾝﾄ
            If Me.lblPOINTS.Visible Then
                intPOINT += intPOINTS
            End If

            'コース来場回数確認
            If UIFunction.GetKosuEnt(Me.lblNCSNO.Text, iDatabase) > 0 Then
                intPOINT = 0
            End If

            '月間来場ポイント加算
            If Not Me.lblZENENTDATE.Text.Replace("/", String.Empty).Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                intPOINT += _intETPPO
            End If
            '誕生月ポイント加算
            intPOINT += _intBIRTHMPO
            '誕生日ポイント加算
            intPOINT += _intBIRTHDPO

            Using frm As New frmMSGBOX01(intJKNMM.ToString & "分で受付します。", 4)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using


            'フロア指定
            Select Case intFLR
                Case 1
                    strSERIALNO = "0A"
                Case 2
                    strSERIALNO = "0B"
                Case 3
                    strSERIALNO = "0C"
                Case Else
                    strSERIALNO = "FF"
            End Select
            '打席指定あり
            If UIUtility.SYSTEM.SITEIKBN.Equals(1) Then
                Using frm As New frmSEATSELECT01(iDatabase)
                    frm.MODE = frmSEATSELECT01.Mode_Type.CHOICE
                    frm.CCSNAME = Me.lblCCSNAME.Text
                    frm.ShowDialog()
                    intSELECTSEAT = frm.SELECTSEAT
                End Using
                If intSELECTSEAT.Equals(0) Then
                    Exit Sub
                End If
                Me.lblSELECTSEAT.Text = intSELECTSEAT.ToString & "番"
            End If

            '残金で足りるか
            intAftKINGAKU = (CType(Me.lblKINGAKU.Text, Integer) - intJKNKIN)
            If intAftKINGAKU < 0 Then
                Using frm As New frmMSGBOX01("カード残金が不足しています。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            '【残金整合処理】
            'ICカード残金
            Dim intIcZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'ICカード残プレミアム
            Dim intIcPREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)

            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0

            If intIcPREZANKN >= intJKNKIN Then
                '【残プレミアム >= 入場料金】
                intIcPREZANKN -= intJKNKIN
                'プレミアムから支払った金額
                intPayPREMKN = intJKNKIN
            Else
                intIcZANKN = intIcZANKN - (intJKNKIN - intIcPREZANKN)
                '残金から支払った金額
                intPayKINGAKU = (intJKNKIN - intIcPREZANKN)
                'プレミアムから支払った金額
                intPayPREMKN = intIcPREZANKN
                intIcPREZANKN = 0
            End If


            '【ICカードセクタ２書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = UIUtility.SYSTEM.NOWPASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = strSERIALNO & Convert.ToString(intJKNMM, 16).ToString.PadLeft(2, "0"c).Substring(0, 1).ToUpper
            '種別
            dcICR700.SYUBETU_WR = "C"
            '金額
            dcICR700.KINGAKU_WR = intAftKINGAKU.ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = Convert.ToString(intJKNMM, 16).ToString.PadLeft(2, "0"c).Substring(1, 1).ToUpper

            '【ICカードセクタ３，４書き込み情報セット】
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
            dcICR700.PASSCD_WR = UIUtility.SYSTEM.NOWPASSCD
            '残金額
            dcICR700.ZANKN_WR = intIcZANKN.ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = intIcPREZANKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            If dcICR700.NCSNO >= "50000000" Then
                dcICR700.POINT_WR = (CType(dcICR700.POINT, Integer) + intPOINT).ToString.PadLeft(5, "0"c)
            Else
                dcICR700.POINT_WR = (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intPOINT).ToString.PadLeft(5, "0"c)
            End If
            '前回来場日
            dcICR700.ZENENTDATE_WR = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            '入場区分
            If Me.chkHakko.Checked Then
                dcICR700.ENTKBN_WR = "1"
            Else
                dcICR700.ENTKBN_WR = "0"
            End If
            'ボール単価
            dcICR700.BALLKIN_WR = "00"
            ' 【打席番号】
            dcICR700.SEATNO_WR = "FFF"
            If intSELECTSEAT > 0 Then
                dcICR700.SEATNO_WR = intSELECTSEAT.ToString.PadLeft(3, "0"c)
            End If

            Me.Panel1.Enabled = False
            Me.Panel1.Refresh()
            frmWait.Show()
            frmWait.Refresh()

            '*** 【データベース更新処理】***'

            'トランザクション開始
            iDatabase.BeginTransaction()

            Dim intDelDENNO As Integer = 0
            Dim strMsg As String = String.Empty
            If Not UpdDatabase(2, CType(btnJKNKB.Tag, String), intIcZANKN, intIcPREZANKN, intPOINT, intJKNKIN, intJKNTAX, intPayKINGAKU, intPayPREMKN, strMsg, intDelDENNO) Then
                Using frm As New frmMSGBOX01(strMsg, 2)
                    frm.ShowDialog()
                End Using
                CardEject()
                Exit Sub
            End If

            'コミット
            iDatabase.Commit()

            Me.Panel1.Enabled = True
            frmWait.Close()

            iDatabase.BeginTransaction()

            If Not dcICR700.NCSNO >= "50000000" Then
                Dim sql As New System.Text.StringBuilder
                '【顧客情報更新】
                sql.Clear()
                sql.Append("UPDATE CSMAST SET")
                sql.Append(" ZENENTDATE = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c) & "'")
                sql.Append(",ENTDT = '" & Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c) & "'")
                If Not dcICR700.ZENENTDATE.Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                    '【本日初来場の場合】
                    sql.Append(",ENTCNT = ENTCNT + 1")
                    sql.Append(",ENTCNT2 = ENTCNT2 + 1")
                End If
                'カード有効期限
                If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                    Dim dtCARDLIMIT As DateTime = Now
                    sql.Append(",CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
                Else
                    sql.Append(",CARDLIMIT = NULL")
                End If

                sql.Append(",DUPDATE = NOW()")
                sql.Append(" WHERE")
                sql.Append(" NCSNO = " & CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "顧客情報の更新に失敗しました。"
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                    CardEject()
                    DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Exit Sub
                End If

                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & intIcZANKN)
                sql.Append(",PREZANKN = " & intIcPREZANKN)
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                    CardEject()
                    DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Exit Sub
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intPOINT))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "ポイントサマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01(strMsg, 2)
                        frm.ShowDialog()
                    End Using
                    CardEject()
                    DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                    Exit Sub
                End If
            End If

            blnCard = False

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
                DelEntInfo(intDelDENNO, CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
                Exit Sub
            End If

            iDatabase.Commit()

            '顧客情報表示
            If dcICR700.NCSNO >= "50000000" Then
                SetFreeCard("C", intAftKINGAKU, intJKNMM.ToString & "分")
            Else
                SetCSMAST("C", intAftKINGAKU, intJKNMM.ToString & "分")
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not blnCard Then

                'Me.pnlEIGMTA.Enabled = False
                'Me.pnlEIGMTB.Enabled = False


                '入場料取消ボタン
                Me.btnCancel.Enabled = False
                '残高移行ボタン
                Me.btnZANIKO01.Enabled = True
                '顧客情報ボタン
                Me.btnCSMAST01.Enabled = True
                '商品引落しボタン
                Me.btnHINDISP01.Enabled = True
                '入場料・ﾎﾟｲﾝﾄなし
                Me.chkFREE.Checked = False
                'Me.btnNKNDISP02.Enabled = True

                Me.btnCard.Visible = False
                Me.btnCard.Tag = 0
                'カード要求開始
                ReadStart()
            End If
            Me.Panel1.Enabled = True
            frmWait.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 貸出カードボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFreeCard_Click(sender As System.Object, e As System.EventArgs) Handles btnFreeCARD.Click
        Try
            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            _intSwitch = 1

            Using frm As New frmFREECARD01(iDatabase, dcICR700)
                frm.ShowDialog()
            End Using

            dcICR700.Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try
    End Sub

    ''' <summary>
    ''' 新規発行ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNewCard_Click(sender As System.Object, e As System.EventArgs) Handles btnNewCard.Click
        Try
            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            _intSwitch = 1

            Using frm As New frmNAMELESS01(iDatabase, dcICR700)
                frm.FormClose = True
                frm.ShowDialog()
            End Using

            dcICR700.Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try
    End Sub

    ''' <summary>
    ''' 残高印刷ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrintCsInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnPrintCsInfo.Click
        Try
            If String.IsNullOrEmpty(Me.lblNCSNO.Text) Then Exit Sub

            Dim strReportName As String = "カード情報"
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


            sheet.Cells(4, 1) = Me.lblCCSNAME.Text & " 様"

            sheet.Cells(7, 3) = Me.lblKINGAKU.Text

            If Me.lblPOINT.Text.Equals("0") Then
                sheet.Cells(9, 1) = String.Empty
                sheet.Cells(9, 5) = String.Empty
            Else
                sheet.Cells(9, 3) = Me.lblPOINT.Text
            End If

            sheet.Cells(11, 3) = Me.lblDMEMBER.Text

            sheet.Cells(14, 1) = "印刷日 " & Now.ToString("yyyy/MM/dd HH:mm:ss")

            'ファイル保存
            book.SaveAs(strSaveReportName)



            'book.PrintOutEx()
            book.PrintOutEx(, , , , "EPSON TM-T90 Receipt")

            book.Close()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 領収書ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReceipt_Click(sender As System.Object, e As System.EventArgs) Handles btnReceipt.Click
        Try
            Using frm As New frmRECEIPT01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' メンテナンスボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKBNO14_Click(sender As System.Object, e As System.EventArgs) Handles btnNKBNO14.Click
        Try
            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
                If frm.ERRFLG Then Exit Sub
            End Using

            If Not String.IsNullOrEmpty(dcICR700.NCSNO) Then
                If CType(dcICR700.NCSNO, Integer) > 0 Then
                    Using frm As New frmMSGBOX01("顧客情報が書き込まれています！" & vbCrLf & "メンテナンスカードを作成しますか？", 1)
                        frm.ShowDialog()
                        If Not frm.Reply Then
                            Exit Sub
                        End If
                    End Using
                End If
            End If

            _strNextPASSCD = UIUtility.SYSTEM.NOWPASSCD
            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                '次の時間帯以降のﾊﾟｽﾜｰﾄﾞ00以外の時間帯の情報書き込み
                GetNextEIGMTA(12, 0, 0)
            End If


            '【ICカードセクタ２書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = _strNextPASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = "FFF"
            '種別
            dcICR700.SYUBETU_WR = "B"
            '金額
            dcICR700.KINGAKU_WR = "0800"
            '予備
            dcICR700.YOBI_WR = "F"

            '【Icカードセクタ３、４書き込み情報セット】
            '店番
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'カード区分
            dcICR700.CARDKBN_WR = "1"
            'カード番号
            dcICR700.CARDNO_WR = "00000000"
            '顧客番号
            dcICR700.NCSNO_WR = "00000000"
            'スクール生番号
            dcICR700.SCLMANNO_WR = "000000"
            '顧客種別
            dcICR700.NKBNO_WR = "1"
            '会員期限
            dcICR700.DMEMBER_WR = "00000000"
            'パスワード
            dcICR700.PASSCD_WR = _strNextPASSCD
            '残金額
            dcICR700.ZANKN_WR = "00000"
            'P残金額
            dcICR700.PREZANKN_WR = "00500"
            '残ポイント
            dcICR700.POINT_WR = "00000"
            '前回来場日
            dcICR700.ZENENTDATE_WR = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            '入場区分
            dcICR700.ENTKBN_WR = "0"
            'ボール単価
            dcICR700.BALLKIN_WR = "00"
            '打席番号
            dcICR700.SEATNO_WR = "FFF"

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

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try
    End Sub

    ''' <summary>
    ''' 店番クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClrShopNo_Click(sender As System.Object, e As System.EventArgs) Handles btnClrShopNo.Click
        Try
            ButtonFontColor(CType(sender, Button), 0)

            'カード要求ストップ
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStop()

            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
                If frm.ERRFLG Then Exit Sub
            End Using


            '【ICカードセクタ２書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = "0000"
            'パスワード
            dcICR700.PASSCD_WR = dcICR700.PASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = dcICR700.SERIALNO
            '種別
            dcICR700.SYUBETU_WR = dcICR700.SYUBETU
            '金額
            dcICR700.KINGAKU_WR = dcICR700.KINGAKU
            '予備
            dcICR700.YOBI_WR = dcICR700.YOBI

            '【Icカードセクタ３、４書き込み情報セット】
            '店番
            dcICR700.SHOPNO_WR = "0000"
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
            dcICR700.PREZANKN_WR = dcICR700.PREZANKN
            '残ポイント
            dcICR700.POINT_WR = dcICR700.POINT
            '前回来場日
            dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
            '入場区分
            dcICR700.ENTKBN_WR = dcICR700.ENTKBN
            'ボール単価
            dcICR700.BALLKIN_WR = dcICR700.BALLKIN
            '打席番号
            dcICR700.SEATNO_WR = dcICR700.SEATNO

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

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ButtonFontColor(CType(sender, Button), 1)
            If CType(Me.btnCard.Tag, Integer).Equals(0) Then ReadStart()
        End Try
    End Sub

    ''' <summary>
    ''' カードリードタイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timRead_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timRead.Tick
        Dim blnERRFLG As Boolean = False

        Try
            'ﾚﾃﾞｨｰｽ
            Me.lblPOINTW.Visible = False
            'ｼﾆｱ
            Me.lblPOINTS.Visible = False

            Me.btnCard.Visible = False
            _intCardReqestCnt += 1
            If (_intCardReqestCnt Mod 4).Equals(0) Then
                Me.lblCardRequest.Visible = Not Me.lblCardRequest.Visible
                Me.lblCardRequest.Refresh()
                _intCardReqestCnt = 1
            End If

            '営業情報画面表示
            Try
                EigyoIndicate()
            Catch ex As Exception
                MessageBox.Show("EigyoIndicateでエラー")
            End Try

            If Not _blnRead Then Exit Sub
            Thread.Sleep(300)
            Me.timRead.Stop()
            Me.lblCardRequest.Visible = False
            '_blnRead = False

            ' ボタンの有効化
            myMethod(0)

            '天気情報取得
            GetTenki()

            Try
                If CType(dcICR700.CARDKBN, Integer).Equals(2) Then
                    Using frm As New frmMSGBOX01("スクールカードは使用できません。", 3)
                        frm.ShowDialog()
                    End Using
                    'カード排出
                    CardEject()
                    'カード要求開始
                    ReadStart()
                    Exit Sub
                End If
            Catch ex As Exception
                'カード排出コマンド
                Using frm As New frmMSGBOX01("使用することが出来ないカードです。", 2)
                    frm.ShowDialog()
                End Using
                Using frm As New frmREQUESTCARD(dcICR700)
                    frmWait.Close()
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If
                'カード要求開始
                ReadStart()
                Exit Sub
            End Try

            'メンテナンスカード
            If dcICR700.SYUBETU.Equals("B") Then
                Using frm As New frmMSGBOX01("メンテナンスカードです。", 0)
                    frm.ShowDialog()
                End Using
                'カード排出コマンド
                Using frm As New frmREQUESTCARD(dcICR700)
                    frmWait.Close()
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If
                'カード要求開始
                ReadStart()
                Exit Sub
            End If


            Me.chkFREE.Checked = False

            Dim strZANTIME As String = String.Empty

            If dcICR700.SYUBETU.Equals("C") Then
                '【打ち放題の場合】
                'If dcICR700.ENTKBN.Equals("1") Then
                '    Me.btnEIGMTB.PerformClick()
                'Else
                '    Me.btnEIGMTA.PerformClick()
                'End If
                strZANTIME = Convert.ToInt32(dcICR700.SERIALNO.Substring(2, 1).ToString & dcICR700.YOBI.ToString, 16).ToString & "分"
            Else
                '【1球貸し】
                Me.btnEIGMTA.PerformClick()
            End If

            '-----------
            Me.lblA.Text = CType(dcICR700.ZANKN.ToString, Integer).ToString("#,##0")
            Me.lblB.Text = CType(dcICR700.PREZANKN.ToString, Integer).ToString("#,##0")
            '-----------

            'ICR700情報金額
            Dim intPreKingaku As Integer = CType(dcICR700.KINGAKU, Integer)
            'V31情報金額
            Dim intV31Kingaku As Integer = CType(dcICR700.ZANKN, Integer) + CType(dcICR700.PREZANKN, Integer)

            '期限更新ボタン
            Me.btnUpdDMEMBER.Enabled = False

            '備考
            Me.btnBIKO1.BackColor = Color.DarkGray
            Me.btnBIKO2.BackColor = Color.DarkGray
            Me.btnBIKO3.BackColor = Color.DarkGray
            _strBIKO1 = String.Empty
            _strBIKO2 = String.Empty
            _strBIKO3 = String.Empty
            _strMEMBERNO = "0"

            If dcICR700.NCSNO >= "50000000" Then
                '【貸出カード】
                If Not SetFreeCard(dcICR700.SYUBETU, intPreKingaku, strZANTIME) Then
                End If
            Else
                '顧客情報表示
                If Not SetCSMAST(dcICR700.SYUBETU, intPreKingaku, strZANTIME, 1) Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出コマンド
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frmWait.Close()
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                        blnERRFLG = frm.ERRFLG
                    End Using
                    If blnERRFLG Then
                        Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                            frm.ShowDialog()
                            Exit Sub
                        End Using
                    End If
                    'カード要求開始
                    ReadStart()
                    Exit Sub
                End If
                '期限更新ボタン
                If CType(Me.btnUpdDMEMBER.Tag, Integer) > 0 Then
                    Me.btnUpdDMEMBER.Enabled = True
                End If

                'カード停止中確認

                If UIFunction.ChkDCSTPTRN(dcICR700.CARDNO, _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), iDatabase) Then

                    Using frm As New frmMSGBOX01("カード停止中です。【顧客番号】" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出コマンド
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frmWait.Close()
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                        blnERRFLG = frm.ERRFLG
                    End Using
                    If blnERRFLG Then
                        Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        'カード要求開始
                        ReadStart()
                        Exit Sub
                    End If
                    'カード要求開始
                    ReadStart()
                    Exit Sub
                End If

                '会員期限チェック
                Me.lblDMEMBER.ForeColor = Color.Blue
                Dim strDMEMBER As String = String.Empty
                Dim strToday As String = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
                If Not String.IsNullOrEmpty(Me.lblDMEMBER.Text) Then
                    Dim dtDMEMBER As DateTime = DateTime.Parse(Me.lblDMEMBER.Text)
                    ' 1ヶ月減算する
                    dtDMEMBER = dtDMEMBER.AddMonths(-1)
                    strDMEMBER = Me.lblDMEMBER.Text.Replace("/", String.Empty)
                    If strDMEMBER < strToday Then
                        Me.lblDMEMBER.ForeColor = Color.Red
                        Using frm As New frmMSGBOX01("会員期限が切れています。", 0)
                            frm.ShowDialog()
                        End Using
                    ElseIf dtDMEMBER.ToString("yyyyMMdd") <= strToday Then
                        Me.lblDMEMBER.ForeColor = Color.Orange
                    End If
                End If
            End If

            '発行料入金機精算
            Me.chkHakko.Checked = False
            If dcICR700.ENTKBN.Equals("1") Then Me.chkHakko.Checked = True

            Me.btnCard.Text = PICK_CARD
            Me.btnCard.BackColor = Color.Orange


            '入場取消ボタン
            Me.btnCancel.Enabled = True
            '入金ボタン
            Me.btnNKNDISP01.Enabled = True
            'サービス入金ボタン
            Me.btnNKNDISP02.Enabled = True

            '残高移行ボタン
            Me.btnZANIKO01.Enabled = True
            '顧客情報ボタン
            Me.btnCSMAST01.Enabled = True
            '商品引落しボタン
            Me.btnHINDISP01.Enabled = True
            'Me.btnNKNDISP02.Enabled = False


            Me.btnNKBNO14.Enabled = False

            Me.btnNewCard.Enabled = False
            Me.btnFreeCARD.Enabled = False

            Me.btnCard.Tag = 1
            Me.btnCard.Visible = True

            '店番復帰確認
            If dcICR700.SHOPNO.Equals("0000") Then
                frmWait.Close()
                If Not CheckShopNo() Then
                    Using frm As New frmMSGBOX01("店番復帰に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
                _blnNoEject = True
                Me.btnCard.PerformClick()
                Exit Sub
            End If

            If dcICR700.NCSNO >= "50000000" Then
                '【貸出カード】
            Else
                '*** カード有効期限チェック ***'
                If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                    Me.lblCARDLIMIT.ForeColor = Color.Blue
                    Dim blnReply As Boolean = False
                    If Not String.IsNullOrEmpty(Me.lblCARDLIMIT.Text) Then
                        Dim intCLRKBN As Integer = 0
                        Dim dtCARDLIMIT As DateTime = DateTime.Parse(Me.lblCARDLIMIT.Text)
                        ' 残高有効期限
                        Dim intPREMLIMIT As Integer = 0
                        Dim strCARDLIMIT As String = String.Empty
                        If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                            intPREMLIMIT = CType("-" & UIUtility.SYSTEM.PREMLIMIT, Integer)
                            dtCARDLIMIT = dtCARDLIMIT.AddMonths(intPREMLIMIT)
                            strCARDLIMIT = dtCARDLIMIT.ToString("yyyy/MM/dd")
                        End If


                        'プリカ金額
                        Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)
                        'V31残金
                        Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
                        'V31残プレミアム
                        Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)

                        If Now.ToString("yyyy/MM/dd") > Me.lblCARDLIMIT.Text Then
                            Me.lblCARDLIMIT.ForeColor = Color.Red
                            Using frm As New frmLKINDISP01(iDatabase, dcICR700)
                                '残金額
                                frm.ZANKN = intV31ZANKN
                                'P)残金額
                                frm.PREZANKN = intV31PREZANKN
                                '残ポイント
                                frm.SRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
                                'カード有効期限
                                frm.CARDLIMIT = Me.lblCARDLIMIT.Text
                                '入金残高有効期限
                                frm.PREMLIMIT = strCARDLIMIT
                                'クリア区分
                                frm.CLRKBN = 1
                                intCLRKBN = 1

                                frm.ShowDialog()

                                '残金クリアOK・キャンセル
                                blnReply = frm.Reply
                            End Using

                        ElseIf Now.ToString("yyyy/MM/dd") > strCARDLIMIT And Not String.IsNullOrEmpty(strCARDLIMIT) Then
                            Me.lblCARDLIMIT.ForeColor = Color.Orange
                            Using frm As New frmLKINDISP01(iDatabase, dcICR700)
                                '残金額
                                frm.ZANKN = intV31ZANKN
                                'P)残金額
                                frm.PREZANKN = intV31PREZANKN
                                '残ポイント
                                frm.SRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
                                'カード有効期限
                                frm.CARDLIMIT = Me.lblCARDLIMIT.Text
                                '入金残高有効期限
                                frm.PREMLIMIT = strCARDLIMIT
                                'クリア区分
                                frm.CLRKBN = 0
                                intCLRKBN = 0

                                frm.ShowDialog()

                                '残金クリアOK・キャンセル
                                blnReply = frm.Reply
                            End Using
                        End If
                        If blnReply Then
                            '*** 【スタッフ確認】 ***'
                            _strSTFCODE = String.Empty
                            _strSTFNAME = String.Empty
                            If UIUtility.SYSTEM.TANCHKFLG.Equals(1) And UIFunction.ChkPgScrty("LKINDISP01", iDatabase) Then
                                '処理スタッフ確認

                                Using frm As New frmSTFSELECT01(iDatabase)
                                    frm.ShowDialog()
                                    _strSTFCODE = frm.STFCODE
                                    _strSTFNAME = frm.STFNAME
                                End Using

                                If String.IsNullOrEmpty(_strSTFCODE) Then
                                    Me.btnCard.PerformClick()
                                    Exit Sub
                                End If
                            End If
                            '*** 【スタッフ確認】 ***'
                            frmWait.Close()
                            If Not ClearZANKN(intCLRKBN) Then
                                Using frm As New frmMSGBOX01("残高クリアに失敗しました。", 2)
                                    frm.ShowDialog()
                                End Using
                                Me.btnCard.PerformClick()
                                Exit Sub
                            End If
                            Exit Sub
                        End If
                    End If
                End If
            End If



            'プレイ打席
            Me.lblSELECTSEAT.Text = String.Empty
            If dcICR700.SERIALNO.Substring(0, 2).Equals("FF") And dcICR700.SEATNO.Equals("FFF") Then
                Me.lblSELECTSEAT.Text = "フリー"
            ElseIf dcICR700.SERIALNO.Substring(0, 2).Equals("0C") Then
                Me.lblSELECTSEAT.Text = "3階"
            ElseIf dcICR700.SERIALNO.Substring(0, 2).Equals("0B") Then
                Me.lblSELECTSEAT.Text = "2階"
            ElseIf dcICR700.SERIALNO.Substring(0, 2).Equals("0A") Then
                Me.lblSELECTSEAT.Text = "1階"
            End If
            If Not dcICR700.SEATNO.Equals("FFF") Then
                Me.lblSELECTSEAT.Text &= CType(dcICR700.SEATNO, Integer).ToString & "番"
            End If

            If Not dcICR700.NCSNO >= "50000000" Then
                ' *** STA ADD 2018/12/20 TERAYAMA DBの金額情報とカードの金額情報の矛盾チェック
                If Not iDatabase Is Nothing Then
                    Dim result = UIFunction.ChkCardKingaku(iDatabase, dcICR700)
                    If result = UIFunction.eChkCardKingakuType.vbERROR Then
                        Dim bSuccess = UIFunction.UpdateKINSMAFromCard(iDatabase, dcICR700)
                        If UIUtility.SYSTEM.FUITCHIMSG Then
                            Using frm As New frmMSGBOX01("金額情報とカード金額が一致しませんでした。" + vbCrLf + "金額情報を更新します。", 3)
                                frm.ShowDialog()
                            End Using
                            If bSuccess Then
                                Using frm As New frmMSGBOX01("金額情報の更新に成功しました。", 0)
                                    frm.ShowDialog()
                                End Using
                            Else
                                Using frm As New frmMSGBOX01("金額情報の更新に失敗しました。", 2)
                                    frm.ShowDialog()
                                End Using
                            End If
                        End If
                    ElseIf result = UIFunction.eChkCardKingakuType.vbDBNOTHING Then
                        Using frm As New frmMSGBOX01("金額情報が存在しません。", 2)
                            frm.ShowDialog()
                        End Using
                    End If
                End If
                ' *** END ADD
            End If

            '打ち放題情報取得
            If Not GetEIGMTB() Then
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRead Then
                Me.Panel1.Enabled = True
                Me.Panel2.Enabled = True
                _blnRead = False
            End If
        End Try
    End Sub


    ''' <summary>
    ''' 1球貸しボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEIGMTA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIGMTA.Click
        Try
            '1球貸しボタン
            Me.btnEIGMTA.ForeColor = Color.Yellow
            Me.pnlEIGMTA.Visible = True
            '打ち放題ボタン
            Me.btnEIGMTB.ForeColor = Color.White
            Me.pnlEIGMTB.Visible = False


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 打ち放題ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEIGMTB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEIGMTB.Click
        Try
            '1球貸しボタン
            Me.btnEIGMTA.ForeColor = Color.White
            Me.pnlEIGMTA.Visible = False
            '打ち放題ボタン
            Me.btnEIGMTB.ForeColor = Color.Yellow
            Me.pnlEIGMTB.Visible = True


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 備考ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBIKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBIKO1.Click, btnBIKO2.Click, btnBIKO3.Click
        Try
            If String.IsNullOrEmpty(dcICR700.NCSNO) Then Exit Sub
            If dcICR700.NCSNO.Equals("00000000") Then Exit Sub

            Using frm As New frmBIKO
                frm.iDatabase = iDatabase
                frm.NCSNO = CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer)
                frm.BIKO1 = _strBIKO1
                frm.BIKO2 = _strBIKO2
                frm.BIKO3 = _strBIKO3
                frm.MEMBERNO = _strMEMBERNO
                frm.StartPosition = FormStartPosition.Manual
                frm.Location = New Point(420, 400)
                frm.ShowDialog()
                _strBIKO1 = frm.BIKO1
                _strBIKO2 = frm.BIKO2
                _strBIKO3 = frm.BIKO3
            End Using
            Me.btnBIKO1.Text = _strBIKO1
            Me.btnBIKO2.Text = _strBIKO2
            Me.btnBIKO3.Text = _strBIKO3
            Me.btnBIKO1.BackColor = Color.DarkGray
            Me.btnBIKO2.BackColor = Color.DarkGray
            Me.btnBIKO3.BackColor = Color.DarkGray
            If Not String.IsNullOrEmpty(_strBIKO1) Then Me.btnBIKO1.BackColor = Color.Gold
            If Not String.IsNullOrEmpty(_strBIKO2) Then Me.btnBIKO2.BackColor = Color.Gold
            If Not String.IsNullOrEmpty(_strBIKO3) Then Me.btnBIKO3.BackColor = Color.Gold


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
            Me.tspFunc10.Enabled = False
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False
            '入場取消ボタン
            Me.btnCancel.Enabled = False

            '本日日付時間
            Me.lblDAYTIME.ForeColor = Color.FromArgb(UIUtility.COLOR_INFO.RKN_R, UIUtility.COLOR_INFO.RKN_G, UIUtility.COLOR_INFO.RKN_B)
            Me.lblDAYTIME.Text = String.Empty

            '入場済メッセージ
            Me.lblEntMsg.Visible = False

            'カード排出ボタン
            Me.btnCard.Tag = 0

            'カラーボタン
            Me.btnClrKbn1.BackColor = Color.White
            Me.btnClrKbn1.ForeColor = Color.White
            Me.btnClrKbn1.Text = ""
            Me.btnClrKbn2.BackColor = Color.White
            Me.btnClrKbn2.ForeColor = Color.White
            Me.btnClrKbn2.Text = ""
            Me.btnClrKbn3.BackColor = Color.White
            Me.btnClrKbn3.ForeColor = Color.White
            Me.btnClrKbn3.Text = ""

            '天気
            Me.cmbTENKI.Text = String.Empty
            '気温
            Me.txtKION.Text = String.Empty

            '備考
            Me.btnBIKO1.BackColor = Color.DarkGray
            Me.btnBIKO1.Text = String.Empty
            Me.btnBIKO2.BackColor = Color.DarkGray
            Me.btnBIKO2.Text = String.Empty
            Me.btnBIKO3.BackColor = Color.DarkGray
            Me.btnBIKO3.Text = String.Empty

            '顧客番号
            Me.lblNCSNO.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME.Text = String.Empty
            '氏名
            Me.lblCCSNAME.Text = String.Empty
            'ｶﾅ
            Me.lblCCSKANA.Text = String.Empty
            '誕生日
            Me.lblDBIRTH.Text = String.Empty
            '会員期限
            Me.lblDMEMBER.Text = String.Empty
            Me.btnUpdDMEMBER.Enabled = False
            '残金額
            Me.lblKINGAKU.Text = String.Empty
            '残ポイント
            Me.lblPOINT.Text = String.Empty
            '総来場回数
            Me.lblENTCNT.Text = String.Empty
            '月間来場回数
            Me.lblENTCNT2.Text = String.Empty
            '前回来場日
            Me.lblZENENTDATE.Text = String.Empty
            'カード有効期限
            Me.lblCARDLIMIT.Text = String.Empty
            '単価切り替え時間
            Me.lblNEXTTIMENM.Text = String.Empty

            '利用球数金額
            Me.lblUSEKIN.Text = String.Empty
            '総球数金額
            Me.lblSUMUSEKIN.Text = String.Empty

            'ボール単価
            Me.lblTanka1F.Text = String.Empty
            Me.lblTanka2F.Text = String.Empty
            '残球数
            Me.lblUSEBALL1F.Text = String.Empty
            Me.lblUSEBALL2F.Text = String.Empty
            Me.lblUSEBALL3F.Text = String.Empty

            '月間来場ポイント
            Me.lblETPMSG.Text = String.Empty
            Me.lblETPMSG.Tag = 0

            'プレイ打席
            Me.lblSELECTSEAT.Text = String.Empty

            '本日の入金額
            Me.lblTodayNyukin.Text = String.Empty


            '現在のパスワード
            Me.lblNOWPASSCD.Text = UIUtility.SYSTEM.NOWPASSCD
            '総球数
            Me.lblSUMBALL.Text = String.Empty
            '来場者数
            Me.lblSUMENTCNT.Text = String.Empty
            '受付回数
            Me.lblACCEPTCNT.Text = String.Empty
            '現在の時間帯区分
            Me.lblNOWTIMEKB.Text = UIUtility.SYSTEM.NOWTIMEKB.ToString



            '受付種別
            Me.lblSltNKBNO.Text = String.Empty

            '1球貸しボタン
            Me.btnEIGMTA.ForeColor = Color.Yellow
            Me.pnlEIGMTA.Visible = True
            '打ち放題ボタン
            Me.btnEIGMTB.ForeColor = Color.White
            Me.pnlEIGMTB.Visible = False

            '【1球貸し】
            'Me.pnlEIGMTA.Enabled = False

            '入場料・ﾎﾟｲﾝﾄなし
            Me.chkFREE.Checked = False

            '入場ボタン
            Me.btnNKBNO01.Text = String.Empty
            Me.btnNKBNO02.Text = String.Empty
            Me.btnNKBNO03.Text = String.Empty
            Me.btnNKBNO04.Text = String.Empty
            Me.btnNKBNO05.Text = String.Empty
            Me.btnNKBNO06.Text = String.Empty
            Me.btnNKBNO07.Text = String.Empty
            Me.btnNKBNO08.Text = String.Empty
            Me.btnNKBNO09.Text = String.Empty
            Me.btnNKBNO10.Text = String.Empty
            Me.btnNKBNO01.BackColor = Color.SteelBlue
            Me.btnNKBNO02.BackColor = Color.SteelBlue
            Me.btnNKBNO03.BackColor = Color.SteelBlue
            Me.btnNKBNO04.BackColor = Color.SteelBlue
            Me.btnNKBNO05.BackColor = Color.SteelBlue
            Me.btnNKBNO06.BackColor = Color.SteelBlue
            Me.btnNKBNO07.BackColor = Color.SteelBlue
            Me.btnNKBNO08.BackColor = Color.SteelBlue
            Me.btnNKBNO09.BackColor = Color.SteelBlue
            Me.btnNKBNO10.BackColor = Color.SteelBlue
            Me.btnNKBNO14.BackColor = Color.LightCoral
            Me.btnNKBNO01.Enabled = True
            Me.btnNKBNO02.Enabled = True
            Me.btnNKBNO03.Enabled = True
            Me.btnNKBNO04.Enabled = True
            Me.btnNKBNO05.Enabled = True
            Me.btnNKBNO06.Enabled = True
            Me.btnNKBNO07.Enabled = True
            Me.btnNKBNO08.Enabled = True
            Me.btnNKBNO09.Enabled = True
            Me.btnNKBNO10.Enabled = True
            '種別名ラベル
            Me.lblCKBNAME01.Text = String.Empty
            Me.lblCKBNAME02.Text = String.Empty
            Me.lblCKBNAME03.Text = String.Empty
            Me.lblCKBNAME04.Text = String.Empty
            Me.lblCKBNAME05.Text = String.Empty
            Me.lblCKBNAME06.Text = String.Empty
            Me.lblCKBNAME07.Text = String.Empty
            Me.lblCKBNAME08.Text = String.Empty
            Me.lblCKBNAME09.Text = String.Empty
            Me.lblCKBNAME10.Text = String.Empty
            Me.lblCKBNAME11.Text = String.Empty
            Me.lblCKBNAME12.Text = String.Empty
            Me.lblCKBNAME13.Text = String.Empty
            '入場料ラベル
            Me.lblENTKIN01.Text = String.Empty
            Me.lblENTKIN02.Text = String.Empty
            Me.lblENTKIN03.Text = String.Empty
            Me.lblENTKIN04.Text = String.Empty
            Me.lblENTKIN05.Text = String.Empty
            Me.lblENTKIN06.Text = String.Empty
            Me.lblENTKIN07.Text = String.Empty
            Me.lblENTKIN08.Text = String.Empty
            Me.lblENTKIN09.Text = String.Empty
            Me.lblENTKIN10.Text = String.Empty
            Me.lblENTKIN13.Text = String.Empty
            'ポイントラベル
            Me.lblPOINT01.Text = String.Empty
            Me.lblPOINT02.Text = String.Empty
            Me.lblPOINT03.Text = String.Empty
            Me.lblPOINT04.Text = String.Empty
            Me.lblPOINT05.Text = String.Empty
            Me.lblPOINT06.Text = String.Empty
            Me.lblPOINT07.Text = String.Empty
            Me.lblPOINT08.Text = String.Empty
            Me.lblPOINT09.Text = String.Empty
            Me.lblPOINT10.Text = String.Empty
            Me.lblPOINT11.Text = String.Empty
            Me.lblPOINT12.Text = String.Empty
            Me.lblPOINT13.Text = String.Empty
            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄラベル
            Me.lblPOINTW01.Text = String.Empty
            Me.lblPOINTW02.Text = String.Empty
            Me.lblPOINTW03.Text = String.Empty
            Me.lblPOINTW04.Text = String.Empty
            Me.lblPOINTW05.Text = String.Empty
            Me.lblPOINTW06.Text = String.Empty
            Me.lblPOINTW07.Text = String.Empty
            Me.lblPOINTW08.Text = String.Empty
            Me.lblPOINTW09.Text = String.Empty
            Me.lblPOINTW10.Text = String.Empty
            'ｼﾆｱﾎﾟｲﾝﾄラベル
            Me.lblPOINTS01.Text = String.Empty
            Me.lblPOINTS02.Text = String.Empty
            Me.lblPOINTS03.Text = String.Empty
            Me.lblPOINTS04.Text = String.Empty
            Me.lblPOINTS05.Text = String.Empty
            Me.lblPOINTS06.Text = String.Empty
            Me.lblPOINTS07.Text = String.Empty
            Me.lblPOINTS08.Text = String.Empty
            Me.lblPOINTS09.Text = String.Empty
            Me.lblPOINTS10.Text = String.Empty

            '1Fボール単価ラベル
            Me.lblBALLKIN1F01.Text = String.Empty
            Me.lblBALLKIN1F02.Text = String.Empty
            Me.lblBALLKIN1F03.Text = String.Empty
            Me.lblBALLKIN1F04.Text = String.Empty
            Me.lblBALLKIN1F05.Text = String.Empty
            Me.lblBALLKIN1F06.Text = String.Empty
            Me.lblBALLKIN1F07.Text = String.Empty
            Me.lblBALLKIN1F08.Text = String.Empty
            Me.lblBALLKIN1F09.Text = String.Empty
            Me.lblBALLKIN1F10.Text = String.Empty
            Me.lblBALLKIN1F11.Text = String.Empty
            Me.lblBALLKIN1F12.Text = String.Empty
            Me.lblBALLKIN1F13.Text = String.Empty
            '2Fボール単価ラベル
            Me.lblBALLKIN2F01.Text = String.Empty
            Me.lblBALLKIN2F02.Text = String.Empty
            Me.lblBALLKIN2F03.Text = String.Empty
            Me.lblBALLKIN2F04.Text = String.Empty
            Me.lblBALLKIN2F05.Text = String.Empty
            Me.lblBALLKIN2F06.Text = String.Empty
            Me.lblBALLKIN2F07.Text = String.Empty
            Me.lblBALLKIN2F08.Text = String.Empty
            Me.lblBALLKIN2F09.Text = String.Empty
            Me.lblBALLKIN2F10.Text = String.Empty
            Me.lblBALLKIN2F11.Text = String.Empty
            Me.lblBALLKIN2F12.Text = String.Empty
            Me.lblBALLKIN2F13.Text = String.Empty
            '3Fボール単価ラベル
            Me.lblBALLKIN3F01.Text = String.Empty
            Me.lblBALLKIN3F02.Text = String.Empty
            Me.lblBALLKIN3F03.Text = String.Empty
            Me.lblBALLKIN3F04.Text = String.Empty
            Me.lblBALLKIN3F05.Text = String.Empty
            Me.lblBALLKIN3F06.Text = String.Empty
            Me.lblBALLKIN3F07.Text = String.Empty
            Me.lblBALLKIN3F08.Text = String.Empty
            Me.lblBALLKIN3F09.Text = String.Empty
            Me.lblBALLKIN3F10.Text = String.Empty
            Me.lblBALLKIN3F11.Text = String.Empty
            Me.lblBALLKIN3F12.Text = String.Empty
            Me.lblBALLKIN3F13.Text = String.Empty

            '受付回数
            Me.lblNKBNO01_CNT.Text = String.Empty
            Me.lblNKBNO02_CNT.Text = String.Empty
            Me.lblNKBNO03_CNT.Text = String.Empty
            Me.lblNKBNO04_CNT.Text = String.Empty
            Me.lblNKBNO05_CNT.Text = String.Empty
            Me.lblNKBNO06_CNT.Text = String.Empty
            Me.lblNKBNO07_CNT.Text = String.Empty
            Me.lblNKBNO08_CNT.Text = String.Empty
            Me.lblNKBNO09_CNT.Text = String.Empty
            Me.lblNKBNO10_CNT.Text = String.Empty
            Me.lblNKBNO11_CNT.Text = String.Empty
            Me.lblNKBNO12_CNT.Text = String.Empty
            Me.lblNKBNO13_CNT.Text = String.Empty
            Me.lblNKBNO14_CNT.Text = String.Empty

            '【打ち放題】
            'Me.pnlEIGMTB.Enabled = False
            '残時間
            Me.lblZANTIME.Text = String.Empty

            '打ち放題ラベル
            Me.lblJKNNAME01.Text = String.Empty
            Me.lblJKNNAME02.Text = String.Empty
            Me.lblJKNNAME03.Text = String.Empty
            Me.lblJKNNAME04.Text = String.Empty
            Me.lblJKNNAME05.Text = String.Empty
            Me.lblJKNNAME06.Text = String.Empty
            Me.lblJKNNAME07.Text = String.Empty
            Me.lblJKNNAME08.Text = String.Empty
            Me.lblJKNNAME09.Text = String.Empty
            Me.lblJKNNAME10.Text = String.Empty
            '打ち放題ボタン
            Me.btnJKNKB01.Text = String.Empty
            Me.btnJKNKB02.Text = String.Empty
            Me.btnJKNKB03.Text = String.Empty
            Me.btnJKNKB04.Text = String.Empty
            Me.btnJKNKB05.Text = String.Empty
            Me.btnJKNKB06.Text = String.Empty
            Me.btnJKNKB07.Text = String.Empty
            Me.btnJKNKB08.Text = String.Empty
            Me.btnJKNKB09.Text = String.Empty
            Me.btnJKNKB10.Text = String.Empty

            Me.btnJKNKB01.Visible = False
            Me.btnJKNKB02.Visible = False
            Me.btnJKNKB03.Visible = False
            Me.btnJKNKB04.Visible = False
            Me.btnJKNKB05.Visible = False
            Me.btnJKNKB06.Visible = False
            Me.btnJKNKB07.Visible = False
            Me.btnJKNKB08.Visible = False
            Me.btnJKNKB09.Visible = False
            Me.btnJKNKB10.Visible = False

            Me.btnJKNKB01.BackColor = Color.Gray
            Me.btnJKNKB02.BackColor = Color.Gray
            Me.btnJKNKB03.BackColor = Color.Gray
            Me.btnJKNKB04.BackColor = Color.Gray
            Me.btnJKNKB05.BackColor = Color.Gray
            Me.btnJKNKB06.BackColor = Color.Gray
            Me.btnJKNKB07.BackColor = Color.Gray
            Me.btnJKNKB08.BackColor = Color.Gray
            Me.btnJKNKB09.BackColor = Color.Gray
            Me.btnJKNKB10.BackColor = Color.Gray

            '貸し時間
            Me.lblJKNMM01.Text = String.Empty
            Me.lblJKNMM02.Text = String.Empty
            Me.lblJKNMM03.Text = String.Empty
            Me.lblJKNMM04.Text = String.Empty
            Me.lblJKNMM05.Text = String.Empty
            Me.lblJKNMM06.Text = String.Empty
            Me.lblJKNMM07.Text = String.Empty
            Me.lblJKNMM08.Text = String.Empty
            Me.lblJKNMM09.Text = String.Empty
            Me.lblJKNMM10.Text = String.Empty
            '料金
            Me.lblJKNKIN01.Text = String.Empty
            Me.lblJKNKIN02.Text = String.Empty
            Me.lblJKNKIN03.Text = String.Empty
            Me.lblJKNKIN04.Text = String.Empty
            Me.lblJKNKIN05.Text = String.Empty
            Me.lblJKNKIN06.Text = String.Empty
            Me.lblJKNKIN07.Text = String.Empty
            Me.lblJKNKIN08.Text = String.Empty
            Me.lblJKNKIN09.Text = String.Empty
            Me.lblJKNKIN10.Text = String.Empty
            '消費税
            Me.lblJKNTAX01.Text = String.Empty
            Me.lblJKNTAX02.Text = String.Empty
            Me.lblJKNTAX03.Text = String.Empty
            Me.lblJKNTAX04.Text = String.Empty
            Me.lblJKNTAX05.Text = String.Empty
            Me.lblJKNTAX06.Text = String.Empty
            Me.lblJKNTAX07.Text = String.Empty
            Me.lblJKNTAX08.Text = String.Empty
            Me.lblJKNTAX09.Text = String.Empty
            Me.lblJKNTAX10.Text = String.Empty
            'ポイント
            Me.lblJKNPOINT01.Text = String.Empty
            Me.lblJKNPOINT02.Text = String.Empty
            Me.lblJKNPOINT03.Text = String.Empty
            Me.lblJKNPOINT04.Text = String.Empty
            Me.lblJKNPOINT05.Text = String.Empty
            Me.lblJKNPOINT06.Text = String.Empty
            Me.lblJKNPOINT07.Text = String.Empty
            Me.lblJKNPOINT08.Text = String.Empty
            Me.lblJKNPOINT09.Text = String.Empty
            Me.lblJKNPOINT10.Text = String.Empty
            Me.lblJKNPOINT01.Tag = 0
            Me.lblJKNPOINT02.Tag = 0
            Me.lblJKNPOINT03.Tag = 0
            Me.lblJKNPOINT04.Tag = 0
            Me.lblJKNPOINT05.Tag = 0
            Me.lblJKNPOINT06.Tag = 0
            Me.lblJKNPOINT07.Tag = 0
            Me.lblJKNPOINT08.Tag = 0
            Me.lblJKNPOINT09.Tag = 0
            Me.lblJKNPOINT10.Tag = 0
            'フロア
            Me.lblJKNFLR01.Text = String.Empty
            Me.lblJKNFLR02.Text = String.Empty
            Me.lblJKNFLR03.Text = String.Empty
            Me.lblJKNFLR04.Text = String.Empty
            Me.lblJKNFLR05.Text = String.Empty
            Me.lblJKNFLR06.Text = String.Empty
            Me.lblJKNFLR07.Text = String.Empty
            Me.lblJKNFLR08.Text = String.Empty
            Me.lblJKNFLR09.Text = String.Empty
            Me.lblJKNFLR10.Text = String.Empty

            '受付回数
            Me.lblJKNKB01_CNT.Text = String.Empty
            Me.lblJKNKB02_CNT.Text = String.Empty
            Me.lblJKNKB03_CNT.Text = String.Empty
            Me.lblJKNKB04_CNT.Text = String.Empty
            Me.lblJKNKB05_CNT.Text = String.Empty
            Me.lblJKNKB06_CNT.Text = String.Empty
            Me.lblJKNKB07_CNT.Text = String.Empty
            Me.lblJKNKB08_CNT.Text = String.Empty
            Me.lblJKNKB09_CNT.Text = String.Empty
            Me.lblJKNKB10_CNT.Text = String.Empty

            If UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                '【打ち放題不可】
                Me.btnEIGMTB.Enabled = False
            Else
                Me.btnEIGMTB.Enabled = True
            End If

            'カード発行料精算
            Me.chkHakko.Checked = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 打ち放題マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetEIGMTB() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM EIGMTB")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = " & UIUtility.SYSTEM.RKNKB)
            If dcICR700.NCSNO >= "50000000" Then
                If dcICR700.NKBNO.Equals("A") Then
                    sql.Append(" AND NKBNO = 10")
                Else
                    sql.Append(" AND NKBNO = " & dcICR700.NKBNO)
                End If
            Else
                sql.Append(" AND NKBNO = " & _dtCSMAST.Rows(0).Item("NCSRANK").ToString)
            End If


            sql.Append(" ORDER BY JKNKB")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim drEIGMTB() As DataRow

            Dim intTAX As Integer = 0

            Me.lblJKNKIN01.Tag = 0
            Me.lblJKNKIN02.Tag = 0
            Me.lblJKNKIN03.Tag = 0
            Me.lblJKNKIN04.Tag = 0
            Me.lblJKNKIN05.Tag = 0
            Me.lblJKNKIN06.Tag = 0
            Me.lblJKNKIN07.Tag = 0
            Me.lblJKNKIN08.Tag = 0
            Me.lblJKNKIN09.Tag = 0
            Me.lblJKNKIN10.Tag = 0

            Dim intWaribiki As Integer = 0
            If Me.chkFREE.Checked Then
                Select Case dcICR700.NKBNO
                    Case "1" : intWaribiki = CType(Me.lblENTKIN01.Tag, Integer)
                    Case "2" : intWaribiki = CType(Me.lblENTKIN02.Tag, Integer)
                    Case "3" : intWaribiki = CType(Me.lblENTKIN03.Tag, Integer)
                    Case "4" : intWaribiki = CType(Me.lblENTKIN04.Tag, Integer)
                    Case "5" : intWaribiki = CType(Me.lblENTKIN05.Tag, Integer)
                    Case "6" : intWaribiki = CType(Me.lblENTKIN06.Tag, Integer)
                    Case "7" : intWaribiki = CType(Me.lblENTKIN07.Tag, Integer)
                    Case "8" : intWaribiki = CType(Me.lblENTKIN08.Tag, Integer)
                    Case "9" : intWaribiki = CType(Me.lblENTKIN09.Tag, Integer)
                    Case Else : intWaribiki = CType(Me.lblENTKIN10.Tag, Integer)
                End Select
            End If

            '時間区分1
            drEIGMTB = resultDt.Select("JKNKB = 1")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB01.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME01.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM01.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT01.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN01.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN01.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN01.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN01.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX01.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT01.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR01.Text = "指定無し"
                    Me.lblJKNFLR01.Tag = 0
                Else
                    Me.lblJKNFLR01.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR01.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB01.Visible = True
                Me.btnJKNKB01.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB01.Enabled = False
                    Me.lblJKNKIN01.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB01.Enabled = True
                    Me.lblJKNKIN01.Enabled = True
                End If
            End If
            '時間区分2
            drEIGMTB = resultDt.Select("JKNKB = 2")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB02.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME02.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM02.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT02.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN02.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN02.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN02.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN02.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX02.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT02.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR02.Text = "指定無し"
                    Me.lblJKNFLR02.Tag = 0
                Else
                    Me.lblJKNFLR02.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR02.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB02.Visible = True
                Me.btnJKNKB02.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB02.Enabled = False
                    Me.lblJKNKIN02.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB02.Enabled = True
                    Me.lblJKNKIN02.Enabled = True
                End If
            End If
            '時間区分3
            drEIGMTB = resultDt.Select("JKNKB = 3")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB03.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME03.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM03.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT03.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN03.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN03.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN03.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN03.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX03.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT03.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR03.Text = "指定無し"
                    Me.lblJKNFLR03.Tag = 0
                Else
                    Me.lblJKNFLR03.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR03.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB03.Visible = True
                Me.btnJKNKB03.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB03.Enabled = False
                    Me.lblJKNKIN03.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB03.Enabled = True
                    Me.lblJKNKIN03.Enabled = True
                End If
            End If
            '時間区分4
            drEIGMTB = resultDt.Select("JKNKB = 4")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB04.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME04.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM04.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT04.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN04.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN04.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN04.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN04.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX04.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT04.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR04.Text = "指定無し"
                    Me.lblJKNFLR04.Tag = 0
                Else
                    Me.lblJKNFLR04.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR04.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB04.Visible = True
                Me.btnJKNKB04.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB04.Enabled = False
                    Me.lblJKNKIN04.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB04.Enabled = True
                    Me.lblJKNKIN04.Enabled = True
                End If
            End If
            '時間区分5
            drEIGMTB = resultDt.Select("JKNKB = 5")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB05.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME05.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM05.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT05.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN05.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN05.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN05.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN05.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX05.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT05.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR05.Text = "指定無し"
                    Me.lblJKNFLR05.Tag = 0
                Else
                    Me.lblJKNFLR05.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR05.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB05.Visible = True
                Me.btnJKNKB05.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB05.Enabled = False
                    Me.lblJKNKIN05.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB05.Enabled = True
                    Me.lblJKNKIN05.Enabled = True
                End If
            End If
            '時間区分6
            drEIGMTB = resultDt.Select("JKNKB = 6")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB06.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME06.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM06.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT06.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN06.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN06.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN06.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN06.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX06.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT06.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR06.Text = "指定無し"
                    Me.lblJKNFLR06.Tag = 0
                Else
                    Me.lblJKNFLR06.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR06.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB06.Visible = True
                Me.btnJKNKB06.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB06.Enabled = False
                    Me.lblJKNKIN06.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB06.Enabled = True
                    Me.lblJKNKIN06.Enabled = True
                End If
            End If
            '時間区分7
            drEIGMTB = resultDt.Select("JKNKB = 7")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB07.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME07.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM07.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT07.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN07.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN07.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN07.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN07.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX07.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT07.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR07.Text = "指定無し"
                    Me.lblJKNFLR07.Tag = 0
                Else
                    Me.lblJKNFLR07.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR07.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB07.Visible = True
                Me.btnJKNKB07.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB07.Enabled = False
                    Me.lblJKNKIN07.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB07.Enabled = True
                    Me.lblJKNKIN07.Enabled = True
                End If
            End If
            '時間区分8
            drEIGMTB = resultDt.Select("JKNKB = 8")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB08.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME08.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM08.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT08.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN08.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN08.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN08.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN08.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX08.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT08.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR08.Text = "指定無し"
                    Me.lblJKNFLR08.Tag = 0
                Else
                    Me.lblJKNFLR08.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR08.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB08.Visible = True
                Me.btnJKNKB08.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB08.Enabled = False
                    Me.lblJKNKIN08.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB08.Enabled = True
                    Me.lblJKNKIN08.Enabled = True
                End If
            End If
            '時間区分9
            drEIGMTB = resultDt.Select("JKNKB = 9")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB09.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME09.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM09.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT09.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN09.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN09.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN09.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN09.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX09.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT09.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR09.Text = "指定無し"
                    Me.lblJKNFLR09.Tag = 0
                Else
                    Me.lblJKNFLR09.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR09.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB09.Visible = True
                Me.btnJKNKB09.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB09.Enabled = False
                    Me.lblJKNKIN09.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB09.Enabled = True
                    Me.lblJKNKIN09.Enabled = True
                End If
            End If
            '時間区分10
            drEIGMTB = resultDt.Select("JKNKB = 10")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB10.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNNAME10.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.lblJKNMM10.Text = drEIGMTB(0).Item("JKNMM").ToString
                intTAX = CType(drEIGMTB(0).Item("JKNTAX").ToString, Integer)
                Me.lblJKNPOINT10.Tag = CType(drEIGMTB(0).Item("CHARGEFLG").ToString, Integer)
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN10.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki).ToString("#,##0")
                    Me.lblJKNKIN10.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - intWaribiki
                Else
                    Me.lblJKNKIN10.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                    Me.lblJKNKIN10.Tag = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)
                End If
                Me.lblJKNTAX10.Text = UIFunction.CalcExcludedTax(CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer)).ToString("#,##0")
                Me.lblJKNPOINT10.Text = CType(drEIGMTB(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer).Equals(0) Then
                    Me.lblJKNFLR10.Text = "指定無し"
                    Me.lblJKNFLR10.Tag = 0
                Else
                    Me.lblJKNFLR10.Text = drEIGMTB(0).Item("JKNFLR").ToString & "F"
                    Me.lblJKNFLR10.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                End If
                Me.btnJKNKB10.Visible = True
                Me.btnJKNKB10.BackColor = Color.Firebrick

                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB10.Enabled = False
                    Me.lblJKNKIN10.Enabled = False
                End If
                If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB10.Enabled = True
                    Me.lblJKNKIN10.Enabled = True
                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 営業情報マスタ(1球貸し)取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetEIGMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",B.CKBNAME")
            sql.Append(" FROM EIGMTA AS A")
            sql.Append(" LEFT JOIN KBMAST AS B ON B.NKBNO = A.NKBNO")
            sql.Append(" WHERE")
            sql.Append(" A.RKNKB = " & UIUtility.SYSTEM.RKNKB)
            sql.Append(" AND A.TIMEKB = " & UIUtility.SYSTEM.NOWTIMEKB)
            sql.Append(" ORDER BY A.NKBNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim drEIGMTA() As DataRow

            '種別1
            drEIGMTA = resultDt.Select("NKBNO = 1")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO01.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME01.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN01.Text = String.Empty
                    Me.lblENTKIN01.Tag = 0
                    Me.lblPOINT01.Text = String.Empty
                    Me.lblPOINTW01.Text = String.Empty
                    Me.lblPOINTS01.Text = String.Empty
                    Me.lblBALLKIN1F01.Text = String.Empty
                    Me.lblBALLKIN2F01.Text = String.Empty
                    Me.lblBALLKIN3F01.Text = String.Empty
                    Me.btnNKBNO01.Enabled = False
                Else
                    Me.lblENTKIN01.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN01.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT01.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW01.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS01.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F01.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F01.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F01.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F01.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F01.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F01.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO01.Enabled = True
                End If
            End If
            '種別2
            drEIGMTA = resultDt.Select("NKBNO = 2")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO02.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME02.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN02.Text = String.Empty
                    Me.lblENTKIN02.Tag = 0
                    Me.lblPOINT02.Text = String.Empty
                    Me.lblPOINTW02.Text = String.Empty
                    Me.lblPOINTS02.Text = String.Empty
                    Me.lblBALLKIN1F02.Text = String.Empty
                    Me.lblBALLKIN2F02.Text = String.Empty
                    Me.lblBALLKIN3F02.Text = String.Empty
                    Me.btnNKBNO02.Enabled = False
                Else
                    Me.lblENTKIN02.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN02.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT02.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW02.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS02.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F02.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F02.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F02.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F02.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F02.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F02.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO02.Enabled = True
                End If
            End If
            '種別3
            drEIGMTA = resultDt.Select("NKBNO = 3")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO03.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME03.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN03.Text = String.Empty
                    Me.lblENTKIN03.Tag = 0
                    Me.lblPOINT03.Text = String.Empty
                    Me.lblPOINTW03.Text = String.Empty
                    Me.lblPOINTS03.Text = String.Empty
                    Me.lblBALLKIN1F03.Text = String.Empty
                    Me.lblBALLKIN2F03.Text = String.Empty
                    Me.lblBALLKIN3F03.Text = String.Empty
                    Me.btnNKBNO03.Enabled = False
                Else
                    Me.lblENTKIN03.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN03.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT03.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW03.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS03.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F03.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F03.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F03.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F03.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F03.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F03.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO03.Enabled = True
                End If
            End If
            '種別4
            drEIGMTA = resultDt.Select("NKBNO = 4")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO04.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME04.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN04.Text = String.Empty
                    Me.lblENTKIN04.Tag = 0
                    Me.lblPOINT04.Text = String.Empty
                    Me.lblPOINTW04.Text = String.Empty
                    Me.lblPOINTS04.Text = String.Empty
                    Me.lblBALLKIN1F04.Text = String.Empty
                    Me.lblBALLKIN2F04.Text = String.Empty
                    Me.lblBALLKIN3F04.Text = String.Empty
                    Me.btnNKBNO04.Enabled = False
                Else
                    Me.lblENTKIN04.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN04.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT04.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW04.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS04.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F04.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F04.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F04.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F04.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F04.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F04.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO04.Enabled = True
                End If
            End If
            '種別5
            drEIGMTA = resultDt.Select("NKBNO = 5")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO05.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME05.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN05.Text = String.Empty
                    Me.lblENTKIN05.Tag = 0
                    Me.lblPOINT05.Text = String.Empty
                    Me.lblPOINTW05.Text = String.Empty
                    Me.lblPOINTS05.Text = String.Empty
                    Me.lblBALLKIN1F05.Text = String.Empty
                    Me.lblBALLKIN2F05.Text = String.Empty
                    Me.lblBALLKIN3F05.Text = String.Empty
                    Me.btnNKBNO05.Enabled = False
                Else
                    Me.lblENTKIN05.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN05.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT05.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW05.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS05.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F05.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F05.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F05.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F05.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F05.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F05.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO05.Enabled = True
                End If
            End If
            '種別6
            drEIGMTA = resultDt.Select("NKBNO = 6")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO06.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME06.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN06.Text = String.Empty
                    Me.lblENTKIN06.Tag = 0
                    Me.lblPOINT06.Text = String.Empty
                    Me.lblPOINTW06.Text = String.Empty
                    Me.lblPOINTS06.Text = String.Empty
                    Me.lblBALLKIN1F06.Text = String.Empty
                    Me.lblBALLKIN2F06.Text = String.Empty
                    Me.lblBALLKIN3F06.Text = String.Empty
                    Me.btnNKBNO06.Enabled = False
                Else
                    Me.lblENTKIN06.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN06.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT06.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW06.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS06.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F06.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F06.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F06.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F06.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F06.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F06.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO06.Enabled = True
                End If
            End If
            '種別7
            drEIGMTA = resultDt.Select("NKBNO = 7")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO07.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME07.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN07.Text = String.Empty
                    Me.lblENTKIN07.Tag = 0
                    Me.lblPOINT07.Text = String.Empty
                    Me.lblPOINTW07.Text = String.Empty
                    Me.lblPOINTS07.Text = String.Empty
                    Me.lblBALLKIN1F07.Text = String.Empty
                    Me.lblBALLKIN2F07.Text = String.Empty
                    Me.lblBALLKIN3F07.Text = String.Empty
                    Me.btnNKBNO07.Enabled = False
                Else
                    Me.lblENTKIN07.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN07.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT07.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW07.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS07.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F07.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F07.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F07.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F07.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F07.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F07.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO07.Enabled = True
                End If
            End If
            '種別8
            drEIGMTA = resultDt.Select("NKBNO = 8")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO08.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME08.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN08.Text = String.Empty
                    Me.lblENTKIN08.Tag = 0
                    Me.lblPOINT08.Text = String.Empty
                    Me.lblPOINTW08.Text = String.Empty
                    Me.lblPOINTS08.Text = String.Empty
                    Me.lblBALLKIN1F08.Text = String.Empty
                    Me.lblBALLKIN2F08.Text = String.Empty
                    Me.lblBALLKIN3F08.Text = String.Empty
                    Me.btnNKBNO08.Enabled = False
                Else
                    Me.lblENTKIN08.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN08.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT08.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW08.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS08.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F08.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F08.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F08.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F08.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F08.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F08.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO08.Enabled = True
                End If
            End If
            '種別9
            drEIGMTA = resultDt.Select("NKBNO = 9")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO09.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME09.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN09.Text = String.Empty
                    Me.lblENTKIN09.Tag = 0
                    Me.lblPOINT09.Text = String.Empty
                    Me.lblPOINTW09.Text = String.Empty
                    Me.lblPOINTS09.Text = String.Empty
                    Me.lblBALLKIN1F09.Text = String.Empty
                    Me.lblBALLKIN2F09.Text = String.Empty
                    Me.lblBALLKIN3F09.Text = String.Empty
                    Me.btnNKBNO09.Enabled = False
                Else
                    Me.lblENTKIN09.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN09.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT09.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW09.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS09.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F09.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F09.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F09.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F09.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F09.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F09.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO09.Enabled = True
                End If
            End If
            '種別10
            drEIGMTA = resultDt.Select("NKBNO = 10")
            If drEIGMTA.Length > 0 Then
                Me.btnNKBNO10.Text = drEIGMTA(0).Item("CKBNAME").ToString
                Me.lblCKBNAME10.Text = drEIGMTA(0).Item("CKBNAME").ToString
                If String.IsNullOrEmpty(drEIGMTA(0).Item("CKBNAME").ToString) Then
                    Me.lblENTKIN10.Text = String.Empty
                    Me.lblENTKIN10.Tag = 0
                    Me.lblPOINT10.Text = String.Empty
                    Me.lblPOINTW10.Text = String.Empty
                    Me.lblPOINTS10.Text = String.Empty
                    Me.lblBALLKIN1F10.Text = String.Empty
                    Me.lblBALLKIN2F10.Text = String.Empty
                    Me.lblBALLKIN3F10.Text = String.Empty
                    Me.btnNKBNO10.Enabled = False
                Else
                    Me.lblENTKIN10.Text = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                    Me.lblENTKIN10.Tag = CType(drEIGMTA(0).Item("ENTKIN").ToString, Integer)
                    Me.lblPOINT10.Text = CType(drEIGMTA(0).Item("POINT").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTW10.Text = CType(drEIGMTA(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                    Me.lblPOINTS10.Text = CType(drEIGMTA(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                    Me.lblBALLKIN1F10.Text = (CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer))).ToString
                    Me.lblBALLKIN1F10.Tag = CType(drEIGMTA(0).Item("BALLKIN1F").ToString, Integer)
                    Me.lblBALLKIN2F10.Text = (CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer))).ToString
                    Me.lblBALLKIN2F10.Tag = CType(drEIGMTA(0).Item("BALLKIN2F").ToString, Integer)
                    Me.lblBALLKIN3F10.Text = (CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer) + UIFunction.GetTaxTanka(CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer))).ToString
                    Me.lblBALLKIN3F10.Tag = CType(drEIGMTA(0).Item("BALLKIN3F").ToString, Integer)
                    Me.btnNKBNO10.Enabled = True
                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 営業情報マスタ(1球貸し)取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNextEIGMTA(ByVal intNKBNO As Integer, ByRef intENTKIN As Integer, ByRef intPOINT As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(" FROM EIGMTA AS A")
            sql.Append(" WHERE")
            sql.Append(" A.RKNKB = " & UIUtility.SYSTEM.RKNKB)
            sql.Append(" AND A.TIMEKB > " & UIUtility.SYSTEM.NOWTIMEKB)
            sql.Append(" AND A.NKBNO = " & intNKBNO)
            sql.Append(" AND PASSCD <> '00'")
            sql.Append(" ORDER BY A.TIMEKB")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            intENTKIN = CType(resultDt.Rows(0).Item("ENTKIN").ToString, Integer)
            intPOINT = CType(resultDt.Rows(0).Item("POINT").ToString, Integer)
            _strNextPASSCD = resultDt.Rows(0).Item("PASSCD").ToString

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 貸出カード情報表示
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFreeCard(ByVal strSYUBETU As String, ByVal intKINGAKU As Integer, ByVal strZANTIME As String) As Boolean
        Dim intBALLKIN1F As Integer = 0     'ボール単価1F
        Dim intBALLKIN2F As Integer = 0     'ボール単価2F
        Dim intBALLKIN3F As Integer = 0     'ボール単価3F
        Try
            Me.btnCARDTORIHIST01.Enabled = False
            Me.lblETPMSG.Visible = False


            '顧客番号
            Me.lblNCSNO.Text = dcICR700.NCSNO

            '氏名
            Me.lblCCSNAME.Text = "貸出カード"
            'カナ
            Me.lblCCSKANA.Text = String.Empty
            '誕生日
            Me.lblDBIRTH.Text = String.Empty
            '会員期限
            Me.lblDMEMBER.Text = String.Empty
            Me.btnUpdDMEMBER.Tag = 0
            '残ポイント
            Me.lblPOINT.Text = CType(dcICR700.POINT, Integer).ToString("#,##0")
            '総来場回数
            Me.lblENTCNT.Text = String.Empty
            Me.lblENTCNT.ForeColor = Color.Black
            Me.lblENTCNT.BackColor = Color.White

            '月間来場回数
            Me.lblENTCNT2.Text = String.Empty
            '前回来場日
            Me.lblZENENTDATE.Text = String.Empty
            Me.lblEntMsg.Visible = False
            If dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) Then
                Me.chkFREE.Checked = True
            Else
                Me.chkFREE.Checked = False
            End If

            'カード有効期限
            Me.lblCARDLIMIT.Text = String.Empty

            'カード金額
            Me.lblKINGAKU.Text = intKINGAKU.ToString("#,##0")

            '残時間
            Me.lblZANTIME.Text = String.Empty


            '種別ボタン
            Me.btnNKBNO01.Enabled = False
            Me.btnNKBNO02.Enabled = False
            Me.btnNKBNO03.Enabled = False
            Me.btnNKBNO04.Enabled = False
            Me.btnNKBNO05.Enabled = False
            Me.btnNKBNO06.Enabled = False
            Me.btnNKBNO07.Enabled = False
            Me.btnNKBNO08.Enabled = False
            Me.btnNKBNO09.Enabled = False
            Me.btnNKBNO10.Enabled = False
            Me.btnNKBNO01.BackColor = Color.SteelBlue
            Me.btnNKBNO02.BackColor = Color.SteelBlue
            Me.btnNKBNO03.BackColor = Color.SteelBlue
            Me.btnNKBNO04.BackColor = Color.SteelBlue
            Me.btnNKBNO05.BackColor = Color.SteelBlue
            Me.btnNKBNO06.BackColor = Color.SteelBlue
            Me.btnNKBNO07.BackColor = Color.SteelBlue
            Me.btnNKBNO08.BackColor = Color.SteelBlue
            Me.btnNKBNO09.BackColor = Color.SteelBlue
            Me.btnNKBNO10.BackColor = Color.SteelBlue
            Me.btnNKBNO14.BackColor = Color.LightCoral
            Select Case dcICR700.NKBNO
                Case "1"
                    Me.btnNKBNO01.Enabled = True
                    Me.btnNKBNO01.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F01.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F01.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F01.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO01.Text
                Case "2"
                    Me.btnNKBNO02.Enabled = True
                    Me.btnNKBNO02.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F02.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F02.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F02.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO02.Text
                Case "3"
                    Me.btnNKBNO03.Enabled = True
                    Me.btnNKBNO03.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F03.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F03.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F03.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO03.Text
                Case "4"
                    Me.btnNKBNO04.Enabled = True
                    Me.btnNKBNO04.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F04.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F04.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F04.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO04.Text
                Case "5"
                    Me.btnNKBNO05.Enabled = True
                    Me.btnNKBNO05.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F05.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F05.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F05.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO05.Text
                Case "6"
                    Me.btnNKBNO06.Enabled = True
                    Me.btnNKBNO06.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F06.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F06.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F06.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO06.Text
                Case "7"
                    Me.btnNKBNO07.Enabled = True
                    Me.btnNKBNO07.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F07.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F07.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F07.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO07.Text
                Case "8"
                    Me.btnNKBNO08.Enabled = True
                    Me.btnNKBNO08.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F08.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F08.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F08.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO08.Text
                Case "9"
                    Me.btnNKBNO09.Enabled = True
                    Me.btnNKBNO09.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F09.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F09.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F09.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO09.Text
                Case "A"
                    Me.btnNKBNO10.Enabled = True
                    Me.btnNKBNO10.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F10.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F10.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F10.Tag, Integer)

                    Me.lblCKBNAME.Text = Me.btnNKBNO10.Text
            End Select

            '受付種別
            Select Case strSYUBETU
                Case "1"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO01.Text
                Case "2"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO02.Text
                Case "3"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO03.Text
                Case "4"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO04.Text
                Case "5"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO05.Text
                Case "6"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO06.Text
                Case "7"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO07.Text
                Case "8"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO08.Text
                Case "9"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO09.Text
                Case "A"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO10.Text
                Case "B"
                    Me.lblSltNKBNO.Text = "メンテナンス"
                Case "C"
                    Me.lblSltNKBNO.Text = "打ち放題"
            End Select

            '残時間
            Me.lblZANTIME.Text = strZANTIME

            '残球数
            Dim dblKINGAKU As Double = 0
            If intBALLKIN1F.Equals(0) Then
                Me.lblTanka1F.Text = String.Empty
                Me.lblUSEBALL1F.Text = "無制限"
            Else
                Me.lblTanka1F.Text = (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN1F)).ToString & "円"
                dblKINGAKU = intKINGAKU / (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN1F))
                Me.lblUSEBALL1F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0") & "球"
                'Me.lblUSEBALL1F.Text = (CType(intKINGAKU / (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN1F)), Integer)).ToString("#,##0") & "球"
            End If
            If intBALLKIN2F.Equals(0) Then
                Me.lblTanka2F.Text = String.Empty
                Me.lblUSEBALL2F.Text = "無制限"
            Else
                Me.lblTanka2F.Text = (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN2F)).ToString & "円"
                dblKINGAKU = intKINGAKU / (intBALLKIN2F + UIFunction.GetTaxTanka(intBALLKIN2F))
                Me.lblUSEBALL2F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0") & "球"
                'Me.lblUSEBALL2F.Text = (CType(intKINGAKU / (intBALLKIN2F + UIFunction.GetTaxTanka(intBALLKIN2F)), Integer)).ToString("#,##0") & "球"
            End If
            If intBALLKIN3F.Equals(0) Then
                Me.lblUSEBALL3F.Text = "無制限"
            Else
                dblKINGAKU = intKINGAKU / (intBALLKIN3F + UIFunction.GetTaxTanka(intBALLKIN3F))
                Me.lblUSEBALL3F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0") & "球"
                'Me.lblUSEBALL3F.Text = (CType(intKINGAKU / (intBALLKIN3F + UIFunction.GetTaxTanka(intBALLKIN3F)), Integer)).ToString("#,##0") & "球"
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 顧客情報表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetCSMAST(ByVal strSYUBETU As String, ByVal intKINGAKU As Integer, ByVal strZANTIME As String, Optional ByVal intBikoFlg As Integer = 0) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intBALLKIN1F As Integer = 0     'ボール単価1F
        Dim intBALLKIN2F As Integer = 0     'ボール単価2F
        Dim intBALLKIN3F As Integer = 0     'ボール単価3F
        Try

            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Clear()
            End If


            If dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
                Me.btnCARDTORIHIST01.Enabled = False
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
            sql.Append(",D.CKBLIMIT")
            sql.Append(",E.SCLMANNO")   'スクール生番号
            sql.Append(",E.SCLKBN")     'スクール生区分
            sql.Append(",F.USEKIN")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS D ON D.NKBNO = A.NCSRANK")
            sql.Append(" LEFT JOIN MANMST AS E ON E.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN (SELECT UDNDT,NCSNO,SUM(USEKIN) AS USEKIN FROM BALLTRN GROUP BY UDNDT,NCSNO) AS F ON F.NCSNO = A.NCSNO AND UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" WHERE")
            sql.Append(" A.NCARDID = " & CType(dcICR700.NCSNO, Integer))

            _dtCSMAST = iDatabase.ExecuteRead(sql.ToString())

            If _dtCSMAST.Rows.Count.Equals(0) Then
                Me.btnCARDTORIHIST01.Enabled = False
                Return False
            End If
            Me.btnCARDTORIHIST01.Enabled = True

            '月間来場ポイント情報取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" ETPMTA")
            sql.Append(" WHERE")
            sql.Append(" NKBNO = " & _dtCSMAST.Rows(0).Item("NCSRANK").ToString)
            sql.Append(" AND ENTCNT >=" & (CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer) + 1))
            sql.Append(" ORDER BY ENTCNT")

            Dim dtETPMTA As DataTable = iDatabase.ExecuteRead(sql.ToString)

            Me.lblETPMSG.Text = String.Empty
            _intETPPO = 0
            If Not dtETPMTA.Rows.Count.Equals(0) Then
                If CType(dtETPMTA.Rows(0).Item("ENTCNT").ToString, Integer).Equals(CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer) + 1) Then
                    If _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals(Now.ToString("yyyyMMdd")) Then
                        Me.lblETPMSG.Text = "次回の来場で" & dtETPMTA.Rows(0).Item("POINT").ToString & "P付きます！"
                    Else
                        Me.lblETPMSG.Text = "今回の来場で" & dtETPMTA.Rows(0).Item("POINT").ToString & "P付きます！"
                    End If
                    _intETPPO = CType(dtETPMTA.Rows(0).Item("POINT").ToString, Integer)
                Else
                    Me.lblETPMSG.Text = "月間" & dtETPMTA.Rows(0).Item("ENTCNT").ToString & "回目の来場で" & dtETPMTA.Rows(0).Item("POINT").ToString & "P付きます！"
                End If
            End If

            'ポイントマスタ情報取得
            If Not Not UIFunction.GetPOINTMST(_intBIRTHMPO, _intBIRTHDPO, iDatabase) Then
            End If

            '誕生日確認
            Me.picBirthday.Visible = False
            Me.picBirthday.Tag = 0
            Me.lblDBIRTH.ForeColor = Color.Black
            Dim strBirthday As String = _dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString.Replace("/", String.Empty)
            If String.IsNullOrEmpty(strBirthday) Then
                _intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
                _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
            Else
                If strBirthday.Substring(4, 4).Equals(Now.ToString("MMdd")) Then
                    '【本日誕生日】
                    Me.lblDBIRTH.ForeColor = Color.Red
                    Me.picBirthday.Visible = True
                ElseIf strBirthday.Substring(4, 2).Equals(Now.ToString("MM")) Then
                    '【誕生月】

                    _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
                    Me.picBirthday.Visible = True
                Else
                    _intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
                    _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0

                    'Dim intBirthMM As Integer = CType(strBirthday.Substring(4, 2), Integer) + 1
                    'Dim intNowMM As Integer = CType(Now.ToString("MM"), Integer)
                    'If intBirthMM > 12 Then intBirthMM = 1
                    'If intBirthMM.Equals(intNowMM) Then
                    '    If CType(Now.ToString("dd"), Integer) < 16 Then
                    '        Me.picBirthday.Visible = True
                    '        Me.picBirthday.Tag = 1
                    '    End If
                    'End If
                End If
            End If
            If _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals(Now.ToString("yyyyMMdd")) Then
                '来場済みなので誕生日ポイントなし

                _intBIRTHMPO = 0    '誕生月ﾎﾟｲﾝﾄ0
                _intBIRTHDPO = 0    '誕生日ﾎﾟｲﾝﾄ0
            End If

            'カラーサマリ情報取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" CLRSMA")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")

            Dim dtCLRSMA As DataTable = iDatabase.ExecuteRead(sql.ToString)

            Me.btnClrKbn1.BackColor = Color.White : Me.btnClrKbn1.ForeColor = Color.White : Me.btnClrKbn1.Text = ""
            Me.btnClrKbn2.BackColor = Color.White : Me.btnClrKbn2.ForeColor = Color.White : Me.btnClrKbn2.Text = ""
            Me.btnClrKbn3.BackColor = Color.White : Me.btnClrKbn3.ForeColor = Color.White : Me.btnClrKbn3.Text = ""
            If Not dtCLRSMA.Rows.Count.Equals(0) Then
                Me.btnClrKbn1.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN1").ToString) : Me.btnClrKbn1.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN1").ToString) : Me.btnClrKbn1.Text = dtCLRSMA.Rows(0).Item("CLRKBN1").ToString
                Me.btnClrKbn2.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN2").ToString) : Me.btnClrKbn2.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN2").ToString) : Me.btnClrKbn2.Text = dtCLRSMA.Rows(0).Item("CLRKBN2").ToString
                Me.btnClrKbn3.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN3").ToString) : Me.btnClrKbn3.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN3").ToString) : Me.btnClrKbn3.Text = dtCLRSMA.Rows(0).Item("CLRKBN3").ToString
            End If

            '本日最終入金額
            Me.lblTodayNyukin.Text = String.Empty
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" NKNTRN")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND STSFLG = '0'")
            sql.Append(" AND DENDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" ORDER BY DENNO DESC")

            Dim dtNKNTRN As DataTable = iDatabase.ExecuteRead(sql.ToString)
            If Not dtNKNTRN.Rows.Count.Equals(0) Then
                Me.lblTodayNyukin.Text = CType(dtNKNTRN.Rows(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If


            '顧客番号
            Me.lblNCSNO.Text = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
            '顧客種別
            Me.lblCKBNAME.Text = _dtCSMAST.Rows(0).Item("CKBNAME").ToString
            '氏名
            Me.lblCCSNAME.Text = _dtCSMAST.Rows(0).Item("CCSNAME").ToString
            'カナ
            Me.lblCCSKANA.Text = _dtCSMAST.Rows(0).Item("CCSKANA").ToString
            '誕生日
            Me.lblDBIRTH.Text = _dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString
            '会員期限
            Me.lblDMEMBER.Text = _dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString
            Me.btnUpdDMEMBER.Tag = CType(_dtCSMAST.Rows(0).Item("CKBLIMIT").ToString, Integer)
            '残ポイント
            Me.lblPOINT.Text = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString, Integer).ToString("#,##0")
            '総来場回数
            Me.lblENTCNT.Text = CType(_dtCSMAST.Rows(0).Item("ENTCNT").ToString, Integer).ToString("#,##0")
            Me.lblENTCNT.ForeColor = Color.White
            Me.lblENTCNT.BackColor = Color.Red
            If CType(Me.lblENTCNT.Text, Integer) >= 8 Then
                Me.lblENTCNT.ForeColor = Color.Black
                Me.lblENTCNT.BackColor = Color.White
            End If

            '月間来場回数
            Me.lblENTCNT2.Text = CType(_dtCSMAST.Rows(0).Item("ENTCNT2").ToString, Integer).ToString("#,##0")
            '前回来場日
            If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("ZENENTDATE").ToString) Or _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals("00000000") Then
                Me.lblZENENTDATE.Text = String.Empty
                Me.lblEntMsg.Visible = False
                Me.chkFREE.Checked = False
            Else
                Me.lblZENENTDATE.Text = _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Substring(0, 4) & "/" & _
                                        _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Substring(4, 2) & "/" & _
                                        _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Substring(6, 2)
                If Me.lblZENENTDATE.Text.Replace("/", String.Empty).Equals(Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)) Then
                    If Not dcICR700.PASSCD.Equals("00") Then
                        If dcICR700.PASSCD.Equals(UIUtility.SYSTEM.NOWPASSCD) Then
                            '入場料・ﾎﾟｲﾝﾄ無しチェックボックス
                            Me.chkFREE.Checked = True
                            Me.lblEntMsg.Visible = True
                            '月間来場ポイント情報クリア
                            _intETPPO = 0
                            '誕生日ﾎﾟｲﾝﾄ情報クリア
                            _intBIRTHMPO = 0
                            _intBIRTHDPO = 0
                        End If
                    End If
                Else
                    Me.lblEntMsg.Visible = False
                End If

            End If
            'カード有効期限
            Me.lblCARDLIMIT.ForeColor = Color.Blue
            If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("CARDLIMIT").ToString) Then
                Me.lblCARDLIMIT.Text = String.Empty
            Else
                Me.lblCARDLIMIT.Text = _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" & _
                                    _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" & _
                                    _dtCSMAST.Rows(0).Item("CARDLIMIT").ToString.Substring(6, 2)
                If Now.ToString("yyyy/MM/dd") > Me.lblCARDLIMIT.Text Then
                    Me.lblCARDLIMIT.ForeColor = Color.Red
                End If
            End If

            '備考
            _strBIKO1 = _dtCSMAST.Rows(0).Item("BIKO1").ToString.Trim
            Me.btnBIKO1.Text = _strBIKO1
            _strBIKO2 = _dtCSMAST.Rows(0).Item("BIKO2").ToString.Trim
            Me.btnBIKO2.Text = _strBIKO2
            _strBIKO3 = _dtCSMAST.Rows(0).Item("BIKO3").ToString.Trim
            Me.btnBIKO3.Text = _strBIKO3
            _strMEMBERNO = _dtCSMAST.Rows(0).Item("MEMBERNO").ToString.Trim
            If Not String.IsNullOrEmpty(_strBIKO1) Then Me.btnBIKO1.BackColor = Color.Gold
            If Not String.IsNullOrEmpty(_strBIKO2) Then Me.btnBIKO2.BackColor = Color.Gold
            If Not String.IsNullOrEmpty(_strBIKO3) Then Me.btnBIKO3.BackColor = Color.Gold
            If Not String.IsNullOrEmpty(_strBIKO1) Or Not String.IsNullOrEmpty(_strBIKO2) Or Not String.IsNullOrEmpty(_strBIKO3) Then
                If intBikoFlg.Equals(1) And _dtCSMAST.Rows(0).Item("MEMBERNO").ToString.Equals("1") Then
                    Me.btnBIKO1.PerformClick()
                End If
            End If


            'カード金額
            Me.lblKINGAKU.Text = CType(intKINGAKU, Integer).ToString("#,##0")

            '残時間
            Me.lblZANTIME.Text = String.Empty

            '利用球数金額
            If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("USEKIN").ToString) Then
                Me.lblUSEKIN.Text = String.Empty
            Else
                Me.lblUSEKIN.Text = CType(_dtCSMAST.Rows(0).Item("USEKIN").ToString, Integer).ToString("#,##0")
            End If

            'ﾚﾃﾞｨｰｽ
            If _dtCSMAST.Rows(0).Item("NSEX").ToString.Equals("2") Then
                Me.lblPOINTW.Visible = True
            End If
            'ｼﾆｱ
            Dim intBirthMM2 As Integer = 0
            If Not String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString) Then
                intBirthMM2 = CType(_dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString.Replace("/", String.Empty).Substring(4, 2), Integer)
                Dim intAge As Integer = 0
                intAge = UIFunction.GetAge(DateTime.Parse(_dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString), Now)
                If intAge >= 70 Then
                    Me.lblPOINTS.Visible = True
                End If
            End If



            '種別ボタン
            Me.btnNKBNO01.Enabled = False
            Me.btnNKBNO02.Enabled = False
            Me.btnNKBNO03.Enabled = False
            Me.btnNKBNO04.Enabled = False
            Me.btnNKBNO05.Enabled = False
            Me.btnNKBNO06.Enabled = False
            Me.btnNKBNO07.Enabled = False
            Me.btnNKBNO08.Enabled = False
            Me.btnNKBNO09.Enabled = False
            Me.btnNKBNO10.Enabled = False
            Me.btnNKBNO01.BackColor = Color.SteelBlue
            Me.btnNKBNO02.BackColor = Color.SteelBlue
            Me.btnNKBNO03.BackColor = Color.SteelBlue
            Me.btnNKBNO04.BackColor = Color.SteelBlue
            Me.btnNKBNO05.BackColor = Color.SteelBlue
            Me.btnNKBNO06.BackColor = Color.SteelBlue
            Me.btnNKBNO07.BackColor = Color.SteelBlue
            Me.btnNKBNO08.BackColor = Color.SteelBlue
            Me.btnNKBNO09.BackColor = Color.SteelBlue
            Me.btnNKBNO10.BackColor = Color.SteelBlue
            Me.btnNKBNO14.BackColor = Color.LightCoral
            Select Case _dtCSMAST.Rows(0).Item("NCSRANK").ToString
                Case "1"
                    Me.btnNKBNO01.Enabled = True
                    Me.btnNKBNO01.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F01.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F01.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F01.Tag, Integer)
                Case "2"
                    Me.btnNKBNO02.Enabled = True
                    Me.btnNKBNO02.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F02.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F02.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F02.Tag, Integer)
                Case "3"
                    Me.btnNKBNO03.Enabled = True
                    Me.btnNKBNO03.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F03.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F03.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F03.Tag, Integer)
                Case "4"
                    Me.btnNKBNO04.Enabled = True
                    Me.btnNKBNO04.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F04.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F04.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F04.Tag, Integer)
                Case "5"
                    Me.btnNKBNO05.Enabled = True
                    Me.btnNKBNO05.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F05.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F05.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F05.Tag, Integer)
                Case "6"
                    Me.btnNKBNO06.Enabled = True
                    Me.btnNKBNO06.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F06.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F06.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F06.Tag, Integer)
                Case "7"
                    Me.btnNKBNO07.Enabled = True
                    Me.btnNKBNO07.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F07.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F07.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F07.Tag, Integer)
                Case "8"
                    Me.btnNKBNO08.Enabled = True
                    Me.btnNKBNO08.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F08.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F08.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F08.Tag, Integer)
                Case "9"
                    Me.btnNKBNO09.Enabled = True
                    Me.btnNKBNO09.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F09.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F09.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F09.Tag, Integer)
                Case "10"
                    Me.btnNKBNO10.Enabled = True
                    Me.btnNKBNO10.BackColor = Color.Orange
                    intBALLKIN1F = CType(Me.lblBALLKIN1F10.Tag, Integer)
                    intBALLKIN2F = CType(Me.lblBALLKIN2F10.Tag, Integer)
                    intBALLKIN3F = CType(Me.lblBALLKIN3F10.Tag, Integer)
            End Select

            '受付種別
            Select Case strSYUBETU
                Case "1"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO01.Text
                Case "2"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO02.Text
                Case "3"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO03.Text
                Case "4"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO04.Text
                Case "5"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO05.Text
                Case "6"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO06.Text
                Case "7"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO07.Text
                Case "8"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO08.Text
                Case "9"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO09.Text
                Case "A"
                    Me.lblSltNKBNO.Text = Me.btnNKBNO10.Text
                Case "B"
                    Me.lblSltNKBNO.Text = "メンテナンス"
                Case "C"
                    Me.lblSltNKBNO.Text = "打ち放題"
            End Select

            '残時間
            Me.lblZANTIME.Text = strZANTIME

            '残球数
            Dim dblKINGAKU As Double = 0
            If intBALLKIN1F.Equals(0) Then
                Me.lblTanka1F.Text = String.Empty
                Me.lblUSEBALL1F.Text = "無制限"
            Else
                Me.lblTanka1F.Text = (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN1F)).ToString & "円"
                dblKINGAKU = intKINGAKU / (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN1F))
                Me.lblUSEBALL1F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0") & "球"
                'Me.lblUSEBALL1F.Text = (CType(intKINGAKU / (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN1F)), Integer)).ToString("#,##0") & "球"
            End If
            If intBALLKIN2F.Equals(0) Then
                Me.lblTanka2F.Text = String.Empty
                Me.lblUSEBALL2F.Text = "無制限"
            Else
                Me.lblTanka2F.Text = (intBALLKIN1F + UIFunction.GetTaxTanka(intBALLKIN2F)).ToString & "円"
                dblKINGAKU = intKINGAKU / (intBALLKIN2F + UIFunction.GetTaxTanka(intBALLKIN2F))
                Me.lblUSEBALL2F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0") & "球"
                'Me.lblUSEBALL2F.Text = (CType(intKINGAKU / (intBALLKIN2F + UIFunction.GetTaxTanka(intBALLKIN2F)), Integer)).ToString("#,##0") & "球"
            End If
            If intBALLKIN3F.Equals(0) Then
                Me.lblUSEBALL3F.Text = "無制限"
            Else
                dblKINGAKU = intKINGAKU / (intBALLKIN3F + UIFunction.GetTaxTanka(intBALLKIN3F))
                Me.lblUSEBALL3F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0") & "球"
                'Me.lblUSEBALL3F.Text = (CType(intKINGAKU / (intBALLKIN3F + UIFunction.GetTaxTanka(intBALLKIN3F)), Integer)).ToString("#,##0") & "球"
            End If

            Return True

        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function


    ''' <summary>
    ''' データベース更新処理
    ''' </summary>
    ''' <param name="intEIGKB">営業種別区分【1】1球貸し【2】打ち放題 </param>
    ''' <param name="strSltNKBNO">選択された種別</param>
    ''' <param name="intZANKN">残金額</param>
    ''' <param name="intPREZANKN">残プレミアム</param>
    ''' <param name="intGetPOINT">取得ﾎﾟｲﾝﾄ</param>
    ''' <param name="intENTKIN">入場料金(打ち放題料金)</param>
    ''' <param name="intTAX">消費税</param>
    ''' <param name="intPayKINGAKU">支払った残金額</param>
    ''' <param name="intPayPREMKN">支払ったプレミアム額</param>
    ''' <param name="strMsg">エラーメッセージ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdDatabase(ByVal intEIGKB As Integer, ByVal strSltNKBNO As String, ByVal intZANKN As Integer, ByVal intPREZANKN As Integer, ByVal intGetPOINT As Integer _
                                 , ByVal intENTKIN As Integer, ByVal intTAX As Integer, ByVal intPayKINGAKU As Integer, ByVal intPayPREMKN As Integer, ByRef strMsg As String, ByRef intDENNO As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try
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
                iDatabase.RollBack()
                strMsg = "伝票番号の取得に失敗しました。"
                Return False
            End If


            Dim dtmInsDt As DateTime = Now
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
            intDENNO = CType(dtSEQTRN.Rows(0).Item("DENNO").ToString, Integer)
            '部門コード【001】スクール【002】ベンダー
            strSQL1 &= "BMNCD,"
            strSQL2 &= "'002',"
            '分類コード１【005】入場料
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'005',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分 入金区分(入金マスタの区分)
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "1,"
            '売上金額(入場料)
            strSQL1 &= "UDNKN,"
            strSQL2 &= intENTKIN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '顧客番号
            strSQL1 &= "MANNO,"
            If dcICR700.NCSNO >= "50000000" Then
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            End If
            '顧客種別
            strSQL1 &= "KSBKB,"
            strSQL2 &= "'" & strSltNKBNO & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            End If
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            End If
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= (intENTKIN - intTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intENTKIN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intGetPOINT & ","
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= intENTKIN & ","
            'おつり
            strSQL1 &= "TURIKN,"
            strSQL2 &= "0,"
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'2'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込【4】打席カード
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'4',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "入場料 " & intENTKIN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPayPREMKN & ","
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= "NULL,"
            'スタッフ名
            strSQL1 &= "STFNAME,"
            strSQL2 &= "NULL,"
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
            '分類コード１【005】入場料
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'005',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'1',"
            '売上名
            strSQL1 &= "TKTNMA,"
            strSQL2 &= "'" & "入場料 " & intENTKIN.ToString("#,##0") & "円" & "',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= intENTKIN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '顧客番号
            strSQL1 &= "MANNO,"
            If dcICR700.NCSNO >= "50000000" Then
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            End If
            '顧客種別
            strSQL1 &= "KSBKB,"
            strSQL2 &= "'" & strSltNKBNO & "',"
            'スクール生番号
            strSQL1 &= "SCLMANNO,"
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
            End If
            'スクール生区分
            strSQL1 &= "SCLKBN,"
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                strSQL2 &= "NULL,"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
            End If
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= (intENTKIN - intTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intENTKIN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intGetPOINT & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "'1',"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "'2',"
            '売上区分
            strSQL1 &= "UDNKBN,"
            strSQL2 &= "'2'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
            '前回売上日付
            strSQL1 &= "ZENUDNDT,"
            strSQL2 &= "NULL,"
            '前回売上番号
            strSQL1 &= "ZENUDNNO,"
            strSQL2 &= "NULL,"
            '前回作成日時
            strSQL1 &= "ZENINSDTM,"
            strSQL2 &= "NULL,"
            'カード支払区分【0】現金【1】カード【2】商品券【3】銀行振込【4】打席カード
            strSQL1 &= "CPAYKBN,"
            strSQL2 &= "'4',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & "入場料 " & intENTKIN.ToString("#,##0") & "円" & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= intPayPREMKN & ")"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "伝票トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【入場トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO ENTTRA("
            strSQL2 &= " VALUES("
            '削除区分【1】使用中【9】削除
            strSQL1 &= "DATKB,"
            strSQL2 &= "'1',"
            '伝票日付
            strSQL1 &= "ENTDT,"
            strSQL2 &= "'" & UIUtility.SYSTEM.UPDDAY & "',"
            '伝票番号
            strSQL1 &= "ENTNO,"
            strSQL2 &= dtSEQTRN.Rows(0).Item("DENNO").ToString & ","
            '顧客種別区分
            strSQL1 &= "KSBKB,"
            Select Case strSltNKBNO
                Case "A" : strSQL2 &= "'10',"
                Case "B" : strSQL2 &= "'11',"
                Case "C" : strSQL2 &= "'12',"
                Case "D" : strSQL2 &= "'13',"
                Case "E" : strSQL2 &= "'14',"
                Case "F" : strSQL2 &= "'15',"
                Case Else : strSQL2 &= "'" & strSltNKBNO & "',"
            End Select

            '顧客番号
            strSQL1 &= "MANNO,"
            If dcICR700.NCSNO >= "50000000" Then
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            End If
            '営業種別区分【1】1球貸し【2】打ち放題
            strSQL1 &= "EIGKB,"
            strSQL2 &= "'" & intEIGKB & "',"
            '料金体系
            strSQL1 &= "RKNKB,"
            strSQL2 &= "'" & UIUtility.SYSTEM.RKNKB & "',"
            '時間帯コード
            strSQL1 &= "TIMCD,"
            strSQL2 &= "'" & UIUtility.SYSTEM.NOWTIMEKB & "',"
            '時間帯
            strSQL1 &= "TIMTM,"
            strSQL2 &= "NULL,"
            'パスワード
            strSQL1 &= "PASSNO,"
            strSQL2 &= "'" & UIUtility.SYSTEM.NOWPASSCD & "',"
            '入場料
            strSQL1 &= "ENTKN,"
            strSQL2 &= intENTKIN & ","
            '入場料(税抜)
            strSQL1 &= "ENTAKN,"
            strSQL2 &= (intENTKIN - intTAX) & ","
            '入場料(税込)
            strSQL1 &= "ENTBKN,"
            strSQL2 &= intENTKIN & ","
            '消費税区分
            strSQL1 &= "ENTZEIKB,"
            strSQL2 &= "2,"
            '消費税
            strSQL1 &= "ENTZEI,"
            strSQL2 &= intTAX & ","
            '入場料備考１
            strSQL1 &= "ENTCMA,"
            strSQL2 &= "NULL,"
            '入場料備考２
            strSQL1 &= "ENTCMB,"
            strSQL2 &= "NULL,"
            'ポイント
            strSQL1 &= "SRTPO,"
            strSQL2 &= intGetPOINT & ","
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            '前回来場日
            strSQL1 &= "ZENENTDT,"
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                If dcICR700.ZENENTDATE.Equals("00000000") Then
                    strSQL2 &= "NULL,"
                Else
                    strSQL2 &= "'" & dcICR700.ZENENTDATE & "',"
                End If
            Else
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString & "',"
            End If
            '前回顧客種別
            strSQL1 &= "ZENKSBKB,"
            strSQL2 &= "NULL,"
            '残金から支払った金額
            strSQL1 &= "ZENKIN,"
            strSQL2 &= intPayKINGAKU & ","
            '残プレミアムから支払った金額
            strSQL1 &= "ZENPKIN,"
            strSQL2 &= intPayPREMKN & ","
            '月間来場ポイント
            strSQL1 &= "ETPPO,"
            strSQL2 &= _intETPPO & ","
            '誕生日月ポイント
            strSQL1 &= "BIRTHMPO,"
            strSQL2 &= _intBIRTHMPO & ","
            '誕生日ﾎﾟｲﾝﾄ
            strSQL1 &= "BIRTHDPO,"
            strSQL2 &= _intBIRTHDPO & ","
            '退場ポイント
            strSQL1 &= "OUTPO,"
            strSQL2 &= "0,"
            '退場フラグ
            strSQL1 &= "OUTFLG,"
            strSQL2 &= "0,"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '更新日時
            strSQL1 &= "UPDDTM,"
            strSQL2 &= "NOW(),"
            '残金額処理前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= (intZANKN + intPayKINGAKU) & ","
            '残金額処理後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intZANKN & ","
            'P)残金額処理前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= (intPREZANKN + intPayPREMKN) & ","
            'P)残金額処理後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= intPREZANKN & ","
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                '残ポイント処理前
                strSQL1 &= "ZANAPO,"
                strSQL2 &= CType(dcICR700.POINT, Integer) & ","
                '残ポイント処理後
                strSQL1 &= "ZANBPO)"
                strSQL2 &= CType(dcICR700.POINT, Integer) + intGetPOINT & ")"
            Else
                '残ポイント処理前
                strSQL1 &= "ZANAPO,"
                strSQL2 &= CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) & ","
                '残ポイント処理後
                strSQL1 &= "ZANBPO)"
                strSQL2 &= CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intGetPOINT & ")"
            End If

            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "入場トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function



    ''' <summary>
    ''' 前回来場日取得(取消時)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetZENENTDT(ByVal strDENNO As String) As String
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ZENENTDT")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" ENTNO = " & CType(strDENNO, Integer))
            sql.Append(" AND ENTDT = '" & UIUtility.SYSTEM.UPDDAY & "'")

            Dim dtENTTRA As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dtENTTRA.Rows.Count.Equals(0) Then
                Return "00000000"
            ElseIf String.IsNullOrEmpty(dtENTTRA.Rows(0).Item("ZENENTDT").ToString) Then
                Return "00000000"
            End If

            Return dtENTTRA.Rows(0).Item("ZENENTDT").ToString

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 年会費精算処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdCKBFEE(ByRef strMsg As String, ByRef intCKBFEE As Integer, ByRef intPaySelect As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intTax As Integer = 0
        Dim intAftKINGAKU As Integer            '処理後残金額
        Dim strMANNO As String = "ZZZZZZZZ"
        Dim intZANAKN As Integer = 0
        Dim intZANBKN As Integer = 0
        Dim intPREZANAKN As Integer = 0
        Dim intPREZANBKN As Integer = 0
        Dim intZANAPO As Integer = 0
        Dim intZANBPO As Integer = 0
        Try
            Using frm As New frmPAYSELECT
                frm.ShowDialog()
                intPaySelect = frm.PaySelect
            End Using

            If intPaySelect.Equals(0) Then
                intCKBFEE = 0
                Return True
            End If

            '内消費税
            intTax = UIFunction.CalcExcludedTax(intCKBFEE)

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
            dr("GDSNAME") = "年会費 " & intCKBFEE.ToString("#,##0") & "円"
            dr("GDSCOUNT") = 1
            dr("GDSTAX") = intTax
            dr("GDSKIN") = intCKBFEE.ToString("#,##0")
            dr("CPAYKBN") = "0"
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
                '顧客番号
                frm.NCSNO = CType(_dtCSMAST.Rows(0).Item("NCSNO"), Integer).ToString.PadLeft(5, "0"c)
                '氏名
                frm.CCSNAME = _dtCSMAST.Rows(0).Item("CCSNAME").ToString
                'ｶﾅ
                frm.CCSKANA = _dtCSMAST.Rows(0).Item("CCSKANA").ToString
                '顧客種別
                frm.CKBNAME = _dtCSMAST.Rows(0).Item("CKBNAME").ToString
                'スクール生番号
                frm.SCLMANNO = _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString
                '会員期限
                frm.DMEMBER = _dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString
                '誕生日
                frm.DBIRTH = _dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString
                '残金
                frm.ZANKN = CType(_dtCSMAST.Rows(0).Item("ZANKN"), Integer).ToString("#,##0")
                'プレミアム
                frm.PREZANKN = CType(_dtCSMAST.Rows(0).Item("PREZANKN"), Integer).ToString("#,##0")
                'ポイント
                frm.POINT = CType(_dtCSMAST.Rows(0).Item("SRTPO"), Integer).ToString("#,##0")
                '商品売上
                frm.GOODS = dtGOODS
                '現金
                frm.PAYMENT = intCKBFEE
                '取得プレミアム
                frm.GETPREMKN = 0
                '取得ﾎﾟｲﾝﾄ
                frm.GETPOINT = 0
                'レシート出力区分
                frm.RECEIPT = blnRsvRECEIPT
                '年会費打席カード精算フラグ
                If intPaySelect.Equals(2) Then
                    frm.FEECARDFLG = True
                End If

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
                intCKBFEE = 0
                intPaySelect = 0
                Return True
            End If

            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0

            'V31残金
            Dim intV31ZANKN As Integer = 0
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = 0
            If intPaySelect.Equals(2) Then
                '【打席カード支払】
                intRsvCPAYKBN = 4

                '残金で足りるか
                intAftKINGAKU = (CType(Me.lblKINGAKU.Text, Integer) - intCKBFEE)
                If intAftKINGAKU < 0 Then
                    Using frm As New frmMSGBOX01("カード残金が不足しています。", 2)
                        frm.ShowDialog()
                        intCKBFEE = 0
                        intPaySelect = 0
                        Return True
                    End Using
                End If
                'If (intAftKINGAKU - CType(dcICR700.PREZANKN, Integer)) < 0 Then
                '    Using frm As New frmMSGBOX01("カード残金が不足しています。", 2)
                '        frm.ShowDialog()
                '        intCKBFEE = 0
                '        intPaySelect = 0
                '        Return True
                '    End Using
                'End If

                '【残金整合処理】
                'V31残金
                intV31ZANKN = CType(dcICR700.ZANKN, Integer)
                'V31残プレミアム
                intV31PREZANKN = CType(dcICR700.PREZANKN, Integer)



                If intV31PREZANKN >= intCKBFEE Then
                    '【残プレミアム >= 支出額】
                    'プレミアムから支払った金額
                    intPayPREMKN = intCKBFEE
                Else
                    '残金から支払った金額
                    intPayKINGAKU = intCKBFEE - intV31PREZANKN
                    'プレミアムから支払った金額
                    intPayPREMKN = intV31PREZANKN
                End If
                intV31ZANKN = intV31ZANKN - intPayKINGAKU
                intV31PREZANKN = intV31PREZANKN - intPayPREMKN
                ''残金から支払った金額
                'intPayKINGAKU -= intCKBFEE
                ''プレミアムから支払った金額
                'intPayPREMKN = 0
                'intV31ZANKN = intV31ZANKN - intCKBFEE
                'intV31PREZANKN = intV31PREZANKN


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
                dcICR700.KINGAKU_WR = intAftKINGAKU.ToString.PadLeft(5, "0"c)
                'dcICR700.KINGAKU_WR = (intV31ZANKN + intV31PREZANKN).ToString.PadLeft(5, "0"c)
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
                dcICR700.ZANKN_WR = intV31ZANKN.ToString.PadLeft(5, "0"c)
                'P残金額
                dcICR700.PREZANKN_WR = intV31PREZANKN.ToString.PadLeft(5, "0"c)
                '残ポイント
                dcICR700.POINT_WR = _dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c)
                '前回来場日
                dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
                '入場区分
                dcICR700.ENTKBN_WR = dcICR700.ENTKBN
                'ボール単価
                dcICR700.BALLKIN_WR = dcICR700.BALLKIN
                '打席番号
                dcICR700.SEATNO_WR = dcICR700.SEATNO

                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & intV31ZANKN)
                sql.Append(",PREZANKN = " & intV31PREZANKN)
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    Return False
                End If

                strMANNO = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                intZANAKN = CType(dcICR700.ZANKN, Integer)
                intZANBKN = intV31ZANKN
                intPREZANAKN = CType(dcICR700.PREZANKN, Integer)
                intPREZANBKN = intV31PREZANKN
                intZANAPO = CType(dcICR700.POINT, Integer)
                intZANBPO = CType(dcICR700.POINT, Integer)
            End If

            If Not blnRsvCANCEL Then
                '【年会費支払有り】

                '処理日時
                Dim dtmInsDt As DateTime = Now

                If blnRsvRECEIPT Then
                    '【レシート印刷】
                    Dim rePrint As New TMT90.Receipt
                    Dim drGoods As DataRow()
                    drGoods = dtGOODS.Select()
                    drGoods(0).Item("CPAYKBN") = intRsvCPAYKBN.ToString
                    drGoods(0).EndEdit()

                    rePrint.intGetPremKn = 0
                    rePrint.intGetPoint = 0

                    rePrint.strManno = Me.lblNCSNO.Text
                    rePrint.strccsname = Me.lblCCSNAME.Text

                    rePrint.intzankingaku = intV31ZANKN + intV31PREZANKN
                    rePrint.intzanpoint = CType(dcICR700.POINT, Integer)

                    rePrint.intPrintKbn = 0
                    rePrint.strDENNO = dtSEQTRN.Rows(0).Item("DENNO").ToString.PadLeft(4, "0"c)
                    rePrint.insDTTM = dtmInsDt
                    rePrint.dtGoods = dtGOODS
                    rePrint.intTax = intTax
                    rePrint.intGokei = CType(dtGOODS.Rows(0).Item("GDSKIN").ToString, Integer)
                    rePrint.intDeposit = intRsvDEPOSIT
                    rePrint.intChange = intRsvCHANGE
                    rePrint.strHostName = Net.Dns.GetHostName
                    rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
                    rePrint.RePrint(False, String.Empty)
                End If

                '【伝票番号】
                sql.Clear()
                sql.Append("UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "伝票番号の更新に失敗しました。"
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
                '分類コード１【001】商品引落し
                strSQL1 &= "BUNCDA,"
                strSQL2 &= "'001',"
                '分類コード２ 
                strSQL1 &= "BUNCDB,"
                strSQL2 &= "'001',"
                '分類コード３【999】年会費
                strSQL1 &= "BUNCDC,"
                strSQL2 &= "'999',"
                '売上個数区分 
                strSQL1 &= "TKTKBN,"
                strSQL2 &= "'001',"
                '売上金額
                strSQL1 &= "UDNKN,"
                strSQL2 &= intCKBFEE & ","
                '売上数
                strSQL1 &= "TKTSU,"
                strSQL2 &= "1,"
                '経理締日付
                strSQL1 &= "SMADT,"
                strSQL2 &= "NULL,"
                '顧客番号
                strSQL1 &= "MANNO,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
                '顧客種別
                strSQL1 &= "KSBKB,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSRANK").ToString & "',"
                'スクール生番号
                strSQL1 &= "SCLMANNO,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
                'スクール生区分
                strSQL1 &= "SCLKBN,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
                '作成日時(タイムスタンプ加工)
                strSQL1 &= "INSDTMSTR,"
                strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
                '作成日時
                strSQL1 &= "INSDTM,"
                strSQL2 &= "NOW(),"
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= (intCKBFEE - intTax) & ","
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= intCKBFEE & ","
                '消費税
                strSQL1 &= "UDNZKN,"
                strSQL2 &= intTax & ","
                '商品消費税区分【1】税抜【2】税込【9】非課税
                strSQL1 &= "HINZEIKB,"
                strSQL2 &= "'2',"
                'ポイント
                strSQL1 &= "POINT,"
                strSQL2 &= "0,"
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
                strSQL2 &= "'3'," '1:入金、2:入場料精算、3:商品引落し、9:ポイント還元
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
                strSQL2 &= "'" & "年会費 " & intCKBFEE.ToString("#,##0") & "円" & "',"
                '取得プレミアム
                strSQL1 &= "PREMKN,"
                strSQL2 &= intPayPREMKN & ","
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
                '分類コード１【001】商品引落し
                strSQL1 &= "BUNCDA,"
                strSQL2 &= "'001',"
                '分類コード２
                strSQL1 &= "BUNCDB,"
                strSQL2 &= "'001',"
                '分類コード３ 【999】年会費
                strSQL1 &= "BUNCDC,"
                strSQL2 &= "'999',"
                '売上個数区分
                strSQL1 &= "TKTKBN,"
                strSQL2 &= "'001',"
                '売上名
                strSQL1 &= "TKTNMA,"
                strSQL2 &= "'" & "年会費 " & intCKBFEE.ToString("#,##0") & "円" & "',"
                '売上金額
                strSQL1 &= "UDNKN,"
                strSQL2 &= intCKBFEE & ","
                '売上数
                strSQL1 &= "TKTSU,"
                strSQL2 &= "1,"
                '経理締日付
                strSQL1 &= "SMADT,"
                strSQL2 &= "NULL,"
                '顧客番号
                strSQL1 &= "MANNO,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
                '顧客種別
                strSQL1 &= "KSBKB,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSRANK").ToString & "',"
                'スクール生番号
                strSQL1 &= "SCLMANNO,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString & "',"
                'スクール生区分
                strSQL1 &= "SCLKBN,"
                strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("SCLKBN").ToString & "',"
                '作成日時(タイムスタンプ加工)
                strSQL1 &= "INSDTMSTR,"
                strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
                '作成日時
                strSQL1 &= "INSDTM,"
                strSQL2 &= "NOW(),"
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= (intCKBFEE - intTax) & ","
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= intCKBFEE & ","
                '消費税
                strSQL1 &= "UDNZKN,"
                strSQL2 &= intTax & ","
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
                strSQL2 &= "'" & "年会費 " & intCKBFEE.ToString("#,##0") & "円" & "',"
                '取得プレミアム
                strSQL1 &= "PREMKN)"
                strSQL2 &= intPayPREMKN & ")"
                If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                    strMsg = "伝票トランの更新に失敗しました。"
                    Return False
                End If
                '【商品トラン】
                strSQL1 = String.Empty
                strSQL2 = String.Empty
                strSQL1 &= "INSERT INTO HINTRN("
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
                strSQL2 &= "'" & strMANNO & "',"
                '税抜売上金額
                strSQL1 &= "UDNAKN,"
                strSQL2 &= (intCKBFEE - intTax) & ","
                '税込売上金額
                strSQL1 &= "UDNBKN,"
                strSQL2 &= intCKBFEE & ","
                '消費税
                strSQL1 &= "UDNZKN,"
                strSQL2 &= intTax & ","
                'ポイント
                strSQL1 &= "POINT,"
                strSQL2 &= intRsvPOINT & ","
                '残金額処理前
                strSQL1 &= "ZANAKN,"
                strSQL2 &= intZANAKN & ","
                '残金額処理後
                strSQL1 &= "ZANBKN,"
                strSQL2 &= intZANBKN & ","
                'P)残金額処理前
                strSQL1 &= "PREZANAKN,"
                strSQL2 &= intPREZANAKN & ","
                'P)残金額処理後
                strSQL1 &= "PREZANBKN,"
                strSQL2 &= intPREZANBKN & ","
                '残ポイント処理前
                strSQL1 &= "ZANAPO,"
                strSQL2 &= intZANAPO & ","
                '残ポイント処理後
                strSQL1 &= "ZANBPO,"
                strSQL2 &= intZANBPO & ","
                '作成日時
                strSQL1 &= "INSDTM)"
                strSQL2 &= "NOW())"

                If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                    strMsg = "商品トランの更新に失敗しました。"
                    Return False
                End If
            Else
                intCKBFEE = 0
                intPaySelect = 0
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 残高0円クリア
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClearZANKN(ByVal intCLRKBN As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intZANKN As Integer = 0
        Dim intPREMKN As Integer = 0
        Dim intSRTPO As Integer = 0
        Dim intClrZANKN As Integer = 0
        Dim intClrPREMKN As Integer = 0
        Dim intClrSRTPO As Integer = 0
        Try
            '*** 金額計算 ***'

            'プリカ金額
            Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)
            'V31残金
            Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)


            If intCLRKBN.Equals(0) Then
                intClrZANKN = intV31ZANKN
                intClrPREMKN = intClrZANKN

                intZANKN = 0
                intPREMKN = intV31ZANKN + intV31PREZANKN
                intSRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            Else
                intClrZANKN = intV31ZANKN
                intClrPREMKN = intV31PREZANKN
                intClrSRTPO = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            End If

            iDatabase.BeginTransaction()

            '【カード有効期限】
            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            If UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                sql.Append(" CARDLIMIT = NULL")
            Else
                Dim dtCARDLIMIT As DateTime = Now
                sql.Append(" CARDLIMIT = '" & dtCARDLIMIT.AddMonths(UIUtility.SYSTEM.CARDLIMIT).ToString("yyyyMMdd") & "'")
            End If

            sql.Append(",DUPDATE = NOW()")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            '【金額サマリ】
            sql.Clear()
            sql.Append("UPDATE KINSMA SET")
            sql.Append(" ZANKN =" & intZANKN)
            sql.Append(",PREZANKN = " & intPREMKN)
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If
            '【ポイントサマリ】
            sql.Clear()
            sql.Append("UPDATE DPOINTSMA SET")
            sql.Append(" SRTPO = " & intSRTPO)
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If
            Dim dtmInsDt As DateTime = Now
            '【金額クリアトラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO LKINTRA("
            strSQL2 &= " VALUES("
            '金額クリア番号
            strSQL1 &= "LKINNO,"
            strSQL2 &= "(SELECT CASE WHEN MAX(LKINNO) + 1 IS NULL THEN 1 ELSE (MAX(LKINNO) + 1) END FROM LKINTRA),"
            '顧客番号
            strSQL1 &= "MANNO,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '顧客名
            strSQL1 &= "MANNM,"
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("CCSNAME").ToString & "',"
            '残金額
            strSQL1 &= "ZANKN,"
            strSQL2 &= intClrZANKN & ","
            'P)残金額
            strSQL1 &= "PREZANKN,"
            strSQL2 &= intClrPREMKN & ","
            '残ポイント
            strSQL1 &= "SRTPO,"
            strSQL2 &= intClrSRTPO & ","
            'カード期限
            strSQL1 &= "CARDLIMIT,"
            strSQL2 &= "'" & Me.lblCARDLIMIT.Text.Replace("/", String.Empty) & "',"
            'クリア区分
            strSQL1 &= "CLRKBN,"
            strSQL2 &= intCLRKBN & ","
            '残金額処理前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intV31ZANKN & ","
            'P)残金額処理前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intV31PREZANKN & ","
            '残ポイント処理前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) & ","
            If intCLRKBN.Equals(0) Then
                '残金額処理後
                strSQL1 &= "ZANBKN,"
                strSQL2 &= "0,"
                'P)残金額処理後
                strSQL1 &= "PREZANBKN,"
                strSQL2 &= intPREMKN & ","
                '残ポイント処理後
                strSQL1 &= "ZANBPO,"
                strSQL2 &= intSRTPO & ","
            Else
                '残金額処理後
                strSQL1 &= "ZANBKN,"
                strSQL2 &= "0,"

                'P)残金額処理後
                strSQL1 &= "PREZANBKN,"
                strSQL2 &= "0,"
                '残ポイント処理後
                strSQL1 &= "ZANBPO,"
                strSQL2 &= "0,"
            End If
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= UIFunction.NullCheck(_strSTFCODE) & ","
            'スタッフ名  
            strSQL1 &= "STFNAME,"
            strSQL2 &= UIFunction.NullCheck(_strSTFNAME) & ","
            '作成日時(タイムスタンプ加工)
            strSQL1 &= "INSDTMSTR,"
            strSQL2 &= "'" & dtmInsDt.ToString("yyyy/MM/dd HH:mm:ss") & "',"
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"

            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
                Return False
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
            dcICR700.KINGAKU_WR = (intZANKN + intPREMKN).ToString.PadLeft(5, "0"c)
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
            dcICR700.PREZANKN_WR = intPREMKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = intSRTPO.ToString.PadLeft(5, "0"c)
            '前回来場日
            dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
            '入場区分
            dcICR700.ENTKBN_WR = dcICR700.ENTKBN
            'ボール単価
            dcICR700.BALLKIN_WR = dcICR700.BALLKIN
            '打席番号
            dcICR700.SEATNO_WR = dcICR700.SEATNO

            '【カード書き込み】
            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARD(dcICR700)
                frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                frm.EJECTSTOP = True
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                    frm.ShowDialog()
                    iDatabase.RollBack()
                    Return False
                End Using
            End If

            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
            Return False
        Finally


            '入場料取消ボタン
            Me.btnCancel.Enabled = False
            '残高移行ボタン
            Me.btnZANIKO01.Enabled = True
            '顧客情報ボタン
            Me.btnCSMAST01.Enabled = True
            '商品引落しボタン
            Me.btnHINDISP01.Enabled = True
            '入場料・ﾎﾟｲﾝﾄなし
            Me.chkFREE.Checked = False
            'Me.btnNKNDISP02.Enabled = True

            Me.btnCard.Tag = 0

            'カード要求開始
            ReadStart()
        End Try
    End Function

    ''' <summary>
    ''' 天気情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTenki() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM CALMTA")
            sql.Append(" WHERE")
            sql.Append(" CALDT = '" & UIUtility.SYSTEM.UPDDAY & "'")

            Dim dtCALMTA As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dtCALMTA.Rows.Count.Equals(0) Then
                Return False
            End If

            '天気
            Me.cmbTENKI.Text = dtCALMTA.Rows(0).Item("TENKI").ToString
            '気温
            Me.txtKION.Text = dtCALMTA.Rows(0).Item("KION").ToString

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function



#Region "MidB関数"
    ''' -----------------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iStart">
    '''     取り出しを開始する位置。</param>
    ''' <param name="iByteSize">
    '''     取り出すバイト数。</param>
    ''' <returns>
    '''     指定されたバイト位置から指定されたバイト数分の文字列。</returns>
    ''' -----------------------------------------------------------------------------------------
    Private Function MidB(ByVal stTarget As String, ByVal iStart As Integer, ByVal iByteSize As Integer) As String
        Try
            Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

            Dim st1 As String = hEncoding.GetString(btBytes, iStart - 1, iByteSize)
            Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - iStart + 1

            If Asc(Strings.Right(st1, 1)) = 0 Then
                Return st1.Substring(0, st1.Length - 1)
            ElseIf iByteSize = ResultLength - 1 Then
                Return st1.Substring(0, st1.Length - 1)
            Else
                'その他の場合
                Return st1
            End If

            Return hEncoding.GetString(btBytes, iStart - 1, iByteSize)

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' 営業情報画面表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EigyoIndicate()
        Try
            '本日日付時間
            Me.lblDAYTIME.Text = "【" & UIUtility.SYSTEM.RKNNM & "】　" & UIUtility.SYSTEM.DAYTIME

            '打席指定
            If UIUtility.SYSTEM.SITEIKBN.Equals(0) Then
                Me.lblSITEIKBN.Text = "なし"
            Else
                Me.lblSITEIKBN.Text = "あり"
            End If

            '総球数
            Me.lblSUMBALL.Text = UIUtility.SYSTEM.SUMBALL.ToString("#,##0")

            If Not Me.lblNOWTIMEKB.Text.Equals(UIUtility.SYSTEM.NOWTIMEKB.ToString) Then
                '【時間帯が変わった】

                '画面初期設定
                Init()

                '営業情報マスタ(1球貸し)セット
                If Not GetEIGMTA() Then
                    Using frm As New frmMSGBOX01("営業情報マスタ(1球貸し)の取得に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
                ''打ち放題情報取得
                'If Not GetEIGMTB() Then
                'End If
            End If

            '総球数金額
            Me.lblSUMUSEKIN.Text = UIUtility.ENTRY.USEKIN_SUM.ToString("#,##0")

            '単価切り替え時間
            Me.lblNEXTTIMENM.Text = UIUtility.SYSTEM.NEXTTIMENM

            '来場者数
            Me.lblSUMENTCNT.Text = UIUtility.ENTRY.ENT_CNT.ToString("#,##0")
            '受付回数
            Me.lblACCEPTCNT.Text = UIUtility.ENTRY.ACCEPT_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME01.Text) Then Me.lblNKBNO01_CNT.Text = UIUtility.ENTRY.NKBNO01_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME02.Text) Then Me.lblNKBNO02_CNT.Text = UIUtility.ENTRY.NKBNO02_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME03.Text) Then Me.lblNKBNO03_CNT.Text = UIUtility.ENTRY.NKBNO03_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME04.Text) Then Me.lblNKBNO04_CNT.Text = UIUtility.ENTRY.NKBNO04_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME05.Text) Then Me.lblNKBNO05_CNT.Text = UIUtility.ENTRY.NKBNO05_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME06.Text) Then Me.lblNKBNO06_CNT.Text = UIUtility.ENTRY.NKBNO06_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME07.Text) Then Me.lblNKBNO07_CNT.Text = UIUtility.ENTRY.NKBNO07_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME08.Text) Then Me.lblNKBNO08_CNT.Text = UIUtility.ENTRY.NKBNO08_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME09.Text) Then Me.lblNKBNO09_CNT.Text = UIUtility.ENTRY.NKBNO09_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME10.Text) Then Me.lblNKBNO10_CNT.Text = UIUtility.ENTRY.NKBNO10_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME11.Text) Then Me.lblNKBNO11_CNT.Text = UIUtility.ENTRY.NKBNO11_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME12.Text) Then Me.lblNKBNO12_CNT.Text = UIUtility.ENTRY.NKBNO12_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME13.Text) Then Me.lblNKBNO13_CNT.Text = UIUtility.ENTRY.NKBNO13_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblCKBNAME14.Text) Then Me.lblNKBNO14_CNT.Text = UIUtility.ENTRY.NKBNO14_CNT.ToString("#,##0")

            If Not String.IsNullOrEmpty(Me.lblJKNNAME01.Text) Then Me.lblJKNKB01_CNT.Text = UIUtility.ENTRY.JKNKB01_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME02.Text) Then Me.lblJKNKB02_CNT.Text = UIUtility.ENTRY.JKNKB02_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME03.Text) Then Me.lblJKNKB03_CNT.Text = UIUtility.ENTRY.JKNKB03_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME04.Text) Then Me.lblJKNKB04_CNT.Text = UIUtility.ENTRY.JKNKB04_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME05.Text) Then Me.lblJKNKB05_CNT.Text = UIUtility.ENTRY.JKNKB05_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME06.Text) Then Me.lblJKNKB06_CNT.Text = UIUtility.ENTRY.JKNKB06_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME07.Text) Then Me.lblJKNKB07_CNT.Text = UIUtility.ENTRY.JKNKB07_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME08.Text) Then Me.lblJKNKB08_CNT.Text = UIUtility.ENTRY.JKNKB08_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME09.Text) Then Me.lblJKNKB09_CNT.Text = UIUtility.ENTRY.JKNKB09_CNT.ToString("#,##0")
            If Not String.IsNullOrEmpty(Me.lblJKNNAME10.Text) Then Me.lblJKNKB10_CNT.Text = UIUtility.ENTRY.JKNKB10_CNT.ToString("#,##0")

            '打席情報確認
            If UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
                SetSeatInfo()
            End If

            Dim blnErr As Boolean = False   '打席異常あり【True】
            Dim blnCALL As Boolean = False  '打席呼び出し有り
            For i As Integer = 0 To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                Select Case CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATSTATE").ToString, Integer)
                    Case UIUtility.SEATSTATE.FREE_SPACE
                        '【空き打席】

                    Case UIUtility.SEATSTATE.USE_SPACE
                        '【使用中】

                    Case UIUtility.SEATSTATE.CALL_SPACE
                        '【呼び出し中】
                        blnCALL = True
                    Case UIUtility.SEATSTATE.ERR_SPACE
                        '【通信不能】
                        blnErr = True
                    Case UIUtility.SEATSTATE.ERR_RW
                        '【RW異常】
                        blnErr = True
                    Case UIUtility.SEATSTATE.PRICE_CHANGE
                        '【単価切り替え完了】

                    Case Else
                        blnErr = True
                End Select
            Next

            If blnCALL Then
                '【呼び出しあり】
                Me.btnSEATINFO01.BackColor = UIUtility.COLOR_INFO.CALL_COM
            ElseIf blnErr Then
                '【打席異常あり】
                Me.btnSEATINFO01.BackColor = UIUtility.COLOR_INFO.ERR_COM
            Else
                Me.btnSEATINFO01.BackColor = Color.SeaGreen
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ボタンフォントカラー
    ''' </summary>
    ''' <param name="btn"></param>
    ''' <param name="intKBN"></param>
    ''' <remarks></remarks>
    Private Sub ButtonFontColor(ByRef btn As Button, ByVal intKBN As Integer)
        Try
            If intKBN.Equals(0) Then
                btn.ForeColor = Color.Yellow
                btn.Refresh()
                Me.Cursor = Cursors.WaitCursor
            Else
                btn.ForeColor = Color.White
                btn.Refresh()
                Me.Cursor = Cursors.Default
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード要求開始
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadStart(Optional ByVal blnSleep As Boolean = True)
        Try
            Me.btnNKBNO14.Enabled = True
            Me.btnNewCard.Enabled = True
            Me.btnFreeCARD.Enabled = True

            ' ICRW初期化
            dcICR700.Init()

            Try
                _threadCardRead.Priority = ThreadPriority.Highest
                _threadCardRead.IsBackground = True
                _threadCardRead.Start()
            Catch ex As Exception
                _threadCardRead.Resume()
            End Try

            Me.timRead.Start()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード要求ストップ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadStop()
        Try
            Me.timRead.Stop()
            Me.lblCardRequest.Visible = False
            _threadCardRead.Suspend()

            dcICR700.Init()

            'コマンド取消
            dcICR700.Cancel(False)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード排出処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CardEject()

        Try
            frmWait.Close()

            ' キャンセル
            dcICR700.Cancel(False)

        Catch ex As Exception
            Throw ex
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

    ''' <summary>
    ''' 店番整合正チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckShopNo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim dblBALL1F As Double = 0
        Dim dblBALL2F As Double = 0
        Dim intUSEKINAKU As Integer = 0
        Dim intUSEBALLSU As Integer = 0
        Dim intKINGAKU As Integer = 0
        Dim strSERIALNO As String = String.Empty
        Dim strYOBI As String = String.Empty
        Try
            Select Case dcICR700.SYUBETU
                Case "1"
                    dblBALL1F = CType(Me.lblBALLKIN1F01.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F01.Text, Double)
                Case "2"
                    dblBALL1F = CType(Me.lblBALLKIN1F02.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F02.Text, Double)
                Case "3"
                    dblBALL1F = CType(Me.lblBALLKIN1F03.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F03.Text, Double)
                Case "4"
                    dblBALL1F = CType(Me.lblBALLKIN1F04.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F04.Text, Double)
                Case "5"
                    dblBALL1F = CType(Me.lblBALLKIN1F05.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F05.Text, Double)
                Case "6"
                    dblBALL1F = CType(Me.lblBALLKIN1F06.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F06.Text, Double)
                Case "7"
                    dblBALL1F = CType(Me.lblBALLKIN1F07.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F07.Text, Double)
                Case "8"
                    dblBALL1F = CType(Me.lblBALLKIN1F08.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F08.Text, Double)
                Case "9"
                    dblBALL1F = CType(Me.lblBALLKIN1F09.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F09.Text, Double)
                Case "A"
                    dblBALL1F = CType(Me.lblBALLKIN1F10.Text, Double)
                    dblBALL2F = CType(Me.lblBALLKIN2F10.Text, Double)
            End Select


            Using frm As New frmRETURNSHOPNO
                frm.NKBNO = dcICR700.SYUBETU
                frm.SERIALNO = dcICR700.SERIALNO
                frm.YOBI = dcICR700.YOBI
                frm.BALL1F = dblBALL1F
                frm.BALL2F = dblBALL2F
                frm.ShowDialog()
                intUSEKINAKU = frm.USEKINGAKU
                intUSEBALLSU = frm.USEBALLSU
                strSERIALNO = frm.SERIALNO
                strYOBI = frm.YOBI
            End Using

            intKINGAKU = (CType(dcICR700.KINGAKU, Integer) - intUSEKINAKU)
            If intKINGAKU < 0 Then intKINGAKU = 0

            '【残金整合処理】
            'ICカード残金
            Dim intIcZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'ICカード残プレミアム
            Dim intIcPREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)
            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0
            If intKINGAKU.Equals(0) Then
                intIcZANKN = 0
                intIcPREZANKN = 0
            Else
                If intIcPREZANKN >= intUSEKINAKU Then
                    '【残プレミアム >= 入場料金】
                    intIcPREZANKN -= intUSEKINAKU
                    'プレミアムから支払った金額
                    intPayPREMKN = intUSEKINAKU
                Else
                    intIcZANKN = intIcZANKN - (intUSEKINAKU - intIcPREZANKN)
                    '残金から支払った金額
                    intPayKINGAKU = (intUSEKINAKU - intIcPREZANKN)
                    'プレミアムから支払った金額
                    intPayPREMKN = intIcPREZANKN
                    intIcPREZANKN = 0
                End If
            End If

            '【セクタ２書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = dcICR700.PASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = strSERIALNO
            '種別
            dcICR700.SYUBETU_WR = dcICR700.SYUBETU
            '金額
            dcICR700.KINGAKU_WR = intKINGAKU.ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = strYOBI

            '【セクタ３、４書き込み情報セット】

            '店番
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
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
            dcICR700.ZANKN_WR = intIcZANKN.ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = intIcPREZANKN.ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = _dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c)
            '前回来場日
            dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
            '入場区分【0】未入場【1】プレイ中
            dcICR700.ENTKBN_WR = dcICR700.ENTKBN
            'ボール単価
            dcICR700.BALLKIN_WR = dcICR700.BALLKIN
            '打席番号
            dcICR700.SEATNO_WR = dcICR700.SEATNO

            '【カード書き込み】
            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARD(dcICR700)

                frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                frm.EJECTSTOP = True
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Return False
            End If

            If dcICR700.NCSNO >= "50000000" Then
                '【貸出カード】
                If dcICR700.SYUBETU.Equals("C") Then
                    Me.lblZANTIME.Text = Convert.ToInt32(strSERIALNO.Substring(2, 1).ToString & strYOBI.ToString, 16).ToString & "分"
                End If
                Return True
            End If

            If Not dcICR700.SYUBETU.Equals("B") And Not dcICR700.SYUBETU.Equals("C") Then
                Try

                    iDatabase.BeginTransaction()

                    'ボール料金更新
                    UIFunction.UpdBALLTRN(CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) _
                                          , dcICR700.NKBNO _
                                          , Now.ToString("yyyyMMdd") _
                                          , intUSEKINAKU _
                                          , intPayKINGAKU _
                                          , intPayPREMKN _
                                          , intUSEBALLSU _
                                          , CType(dcICR700.ZANKN, Integer) _
                                          , intIcZANKN _
                                          , CType(dcICR700.PREZANKN, Integer) _
                                          , intIcPREZANKN _
                                          , CType(dcICR700.POINT, Integer) _
                                          , CType(dcICR700.POINT, Integer) _
                                          , iDatabase)

                    '【金額サマリ】
                    sql.Clear()
                    sql.Append("UPDATE KINSMA SET")
                    sql.Append(" ZANKN = " & intIcZANKN)
                    sql.Append(",PREZANKN = " & intIcPREZANKN)
                    sql.Append(",UPDDTM = NOW()")
                    sql.Append(" WHERE")
                    sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Return False
                    End If

                    iDatabase.Commit()
                Catch ex As Exception
                    iDatabase.RollBack()
                    Return False
                End Try
            End If


            If dcICR700.SYUBETU.Equals("C") Then
                Me.lblZANTIME.Text = Convert.ToInt32(strSERIALNO.Substring(2, 1).ToString & strYOBI.ToString, 16).ToString & "分"
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' カード書き込み失敗による入場情報削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DelEntInfo(ByVal intDENNO As Integer, ByVal intNCSNO As Integer)
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
            sql.Append("UPDATE ENTTRA SET")
            sql.Append(" DATKB = '9'")
            sql.Append(" WHERE  ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND ENTNO = " & intDENNO)

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
    ''' 打席使用状況取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSeatInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEATWORK")
            sql.Append(" WHERE SEATDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" ORDER BY")
            sql.Append(" SEATNO,LEFTKB")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim drow() As DataRow
            For i As Integer = 0 To resultDt.Rows.Count - 1
                drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & CType(resultDt.Rows(i).Item("SEATNO").ToString, Integer) _
                                                       & " AND LEFTKB = " & CType(resultDt.Rows(i).Item("LEFTKB").ToString, Integer))
                '時間貸し区分
                drow(0).Item("JKNKB") = CType(resultDt.Rows(i).Item("JKNKB").ToString, Integer)
                '時間帯
                drow(0).Item("TIMEKB") = CType(resultDt.Rows(i).Item("TIMEKB").ToString, Integer)
                '打席状況
                drow(0).Item("SEATSTATE") = CType(resultDt.Rows(i).Item("SEATSTATE").ToString, Integer)
                '使用総球数
                drow(0).Item("ALLBALLCNT") = CType(resultDt.Rows(i).Item("ALLBALLCNT").ToString, Integer)
                '使用球数
                drow(0).Item("USEBALLCNT") = CType(resultDt.Rows(i).Item("USEBALLCNT").ToString, Integer)
                '使用開始時間
                drow(0).Item("STARTTIME") = CType(resultDt.Rows(i).Item("STARTTIME").ToString, Integer)
                '使用時間
                drow(0).Item("USETIME") = CType(resultDt.Rows(i).Item("USETIME").ToString, Integer)
                '顧客種別
                drow(0).Item("NKBNO") = CType(resultDt.Rows(i).Item("NKBNO").ToString, Integer)
                'ボール単価
                drow(0).Item("BALLKIN") = CType(resultDt.Rows(i).Item("BALLKIN").ToString, Integer)
                '球数更新フラグ
                drow(0).Item("UPDFLG") = CType(resultDt.Rows(i).Item("UPDFLG").ToString, Integer)
                '予約打席
                drow(0).Item("RSVKBN") = CType(resultDt.Rows(i).Item("RSVKBN").ToString, Integer)
                '顧客番号
                drow(0).Item("NCSNO") = resultDt.Rows(i).Item("NCSNO").ToString
                '氏名
                drow(0).Item("CCSNAME") = resultDt.Rows(i).Item("CCSNAME").ToString
                '精算金額
                drow(0).Item("SEISANKIN") = CType(resultDt.Rows(i).Item("SEISANKIN").ToString, Integer)

                drow(0).EndEdit()
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function
#End Region

#Region "▼スレッド"
    ''' <summary>
    ''' カードリードスレッド
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ThreadCardRead()
        Dim intCnt As Integer
        Dim strErrNo As String = String.Empty
        Dim blnRead As Boolean = False
        Try
            Do
                If _blnTdEnd Then Exit Do

                If _intSwitch.Equals(1) Then
                    dcICR700.Init()
                    _intSwitch = 0
                End If

                ' カード読み込み
                Dim ret = dcICR700.Read(-1)

                If ret = Techno.DeviceControls.ICR700Control.eResult.PROCESSING Then

                    Me.Panel1.Invoke(New myDlegate(AddressOf myMethod), New Object() {3})
                Else
                    Me.Panel1.Invoke(New myDlegate(AddressOf myMethod), New Object() {0})
                End If

                If ret = Techno.DeviceControls.ICR700Control.eResult.SUCCESS Then

                    '【読み込み完了】
                    If (Not dcICR700.SHOPNO.Equals(UIUtility.SYSTEM.SHOPNO)) And Not (dcICR700.SHOPNO.Equals("0000")) Then
                        '【店番不一致】
                        Using frm As New frmMSGBOX01("店番が一致しません。", 2)
                            frm.ShowDialog()
                        End Using
                        ''カード排出
                        'CardEject()
                        _intSwitch = 1
                        Continue Do
                    End If

                    ' スレッド中断
                    _blnRead = True
                    _threadCardRead.Suspend()

                Else
                    If ret = (Techno.DeviceControls.ICR700Control.eResult.vbERROR Or Techno.DeviceControls.ICR700Control.eResult.TIMEOUT) Then
                        '【失敗】
                        Using frm As New frmMSGBOX01("カードの読み取りに失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Thread.Sleep(3000)
                        dcICR700.Init()
                    End If
                    Thread.Sleep(100)
                End If

                'Application.DoEvents()

            Loop

            _threadCardRead.Abort()
            _threadCardRead.Join()

        Catch ex As Exception
            MessageBox.Show("スレッドエラー")
        End Try

    End Sub

    Private Sub myMethod(ByVal mode As Integer)
        If mode = 0 Then
            Me.lblCardRequest.Text = UIMessage.InsertCard
            Me.Enabled = True
        ElseIf mode = 1 Then
            Me.lblCardRequest.Text = "カードを読込中です。"
            Me.Enabled = False
        ElseIf mode = 2 Then
            Me.lblCardRequest.Text = "カードを抜いてください。"
            Me.Enabled = False
        ElseIf mode = 3 Then
            Me.lblCardRequest.Text = "カード確認中・・・"
            Me.Enabled = False
        Else
            Me.lblCardRequest.Text = "カードが詰まっています。"
            Me.Enabled = False
        End If
    End Sub

#End Region


#End Region





End Class

