Imports Techno.DataBase
Imports TMT90

Public Class frmNKNDISP03

#Region "▼宣言部"
    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "NKNDISP03"
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

            MyBase.l_Title_FormName = "コース料金精算画面"

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

            MyBase.l_Title_FormName = "コース料金精算画面"

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

            MyBase.l_Title_FormName = "コース料金精算画面"

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

    Private Sub tabNKN_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tabNKN.DrawItem
        '------------------------------------
        'TabControlのDrawItemイベントハンドラ
        '------------------------------------
        '対象のTabControl取得
        Dim Tab As TabControl = CType(sender, TabControl)
        'タブページのテキスト取得(コレやらないとテキスト消える)　　
        Dim Txt As String = Tab.TabPages(e.Index).Text
        'タブのブラシを決める　　
        Dim BackBrush As Brush

        If e.State = DrawItemState.Selected Then
            BackBrush = Brushes.Red
        Else
            BackBrush = Brushes.WhiteSmoke
        End If

        '背景描画　　
        e.Graphics.FillRectangle(BackBrush, e.Bounds)
        'テキスト描画　　
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center
        Dim rectd As New RectangleF(e.Bounds.X - 15, e.Bounds.Y, e.Bounds.Width + 15, e.Bounds.Height)
        e.Graphics.DrawString(Txt, e.Font, Brushes.Black, rectd, sf)
    End Sub

    Private Sub frmNKNDISP03_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'TabControlをオーナードローする　　
            tabNKN.DrawMode = TabDrawMode.OwnerDrawFixed
            'TabControlのDrawItemイベントハンドラ　　
            AddHandler tabNKN.DrawItem, AddressOf tabNKN_DrawItem

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNKNDISP03_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
            '顧客番号
            Me.lblNCSNO.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME.Text = String.Empty
            '氏名
            Me.lblCCSNAME.Text = String.Empty
            '誕生日
            Me.lblDBIRTH.Text = String.Empty
            '年齢
            Me.lblAge.Text = String.Empty
            '残金額
            Me.lblKINGAKU.Text = String.Empty
            '残ポイント
            Me.lblPOINT.Text = String.Empty

            '画面初期設定
            Init()

            'コース料金マスタ情報取得
            GetKOSUMTA()

            'ﾎﾟｲﾝﾄ情報取得
            GetEIGMTA(1)


            Me.btnCard.PerformClick()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' カード確認ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard_Click(sender As System.Object, e As System.EventArgs) Handles btnCard.Click
        Try
            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.ERRFLG Then Exit Sub
            End Using

            'コース料金選択
            SelectKosu()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 種別ラジオボタン_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdoMember_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoMember.CheckedChanged, rdoVisitor.CheckedChanged, rdoJunior.CheckedChanged
        Try
            '画面初期設定
            Init()

            Me.rdoMember.ForeColor = Color.Black
            Me.rdoVisitor.ForeColor = Color.Black
            Me.rdoJunior.ForeColor = Color.Black

            If Me.rdoMember.Checked Then
                Me.rdoMember.ForeColor = Color.Red
            ElseIf Me.rdoVisitor.Checked Then
                Me.rdoVisitor.ForeColor = Color.Red
            Else
                Me.rdoJunior.ForeColor = Color.Red
            End If

            Try
                'コース料金マスタ情報取得
                GetKOSUMTA()
            Catch ex As Exception

            End Try



        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 保険料チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkHOKENKIN_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHOKENKIN.CheckedChanged
        Try
            Me.chkHOKENKIN.ForeColor = Color.Black

            If Me.chkHOKENKIN.Checked Then
                Me.chkHOKENKIN.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カート料チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkCARTKIN_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCARTKIN.CheckedChanged
        Try
            Me.chkCARTKIN.ForeColor = Color.Black

            If Me.chkCARTKIN.Checked Then
                Me.chkCARTKIN.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' コンペ料チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkCOMPEKIN_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCOMPEKIN.CheckedChanged
        Try
            Me.chkCOMPEKIN.ForeColor = Color.Black

            If Me.chkCOMPEKIN.Checked Then
                Me.chkCOMPEKIN.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 来場ポイントチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPOINT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPOINT.CheckedChanged
        Try
            Me.chkPOINTW.Enabled = False
            Me.chkPOINTW.Checked = False
            Me.chkPOINTS.Enabled = False
            Me.chkPOINTS.Checked = False

            Me.chkPOINT.ForeColor = Color.Black

            If Me.chkPOINT.Checked Then
                Me.chkPOINT.ForeColor = Color.Red
                Me.chkPOINTW.Enabled = True
                Me.chkPOINTS.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ﾚﾃﾞｨｰｽポイントチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPOINTW_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPOINTW.CheckedChanged
        Try
            Me.chkPOINTW.ForeColor = Color.Black

            If Me.chkPOINTW.Checked Then
                Me.chkPOINTW.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' シニアポイントチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPOINTS_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPOINTS.CheckedChanged
        Try
            Me.chkPOINTS.ForeColor = Color.Black

            If Me.chkPOINTS.Checked Then
                Me.chkPOINTS.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 誕生月・Ｕ40ポイントチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPOINT2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPOINT2.CheckedChanged
        Try
            Me.chkPOINT2.ForeColor = Color.Black
            Me.txtPOINT2.Enabled = False

            If Me.chkPOINT2.Checked Then
                Me.chkPOINT2.ForeColor = Color.Red
                Me.txtPOINT2.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ポイント精算チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkSeisan_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSeisan.CheckedChanged
        Try
            Me.chkSeisan.ForeColor = Color.Black

            If Me.chkSeisan.Checked Then
                Me.chkSeisan.ForeColor = Color.Red
            End If

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
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【カード支出】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.SHISYUTU
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
            dr("GDSNAME") = resultDt.Rows(0).Item("HINNMA").ToString
            dr("GDSCOUNT") = resultDt.Rows(0).Item("TKTSU").ToString
            dr("GDSTAX") = CType(resultDt.Rows(0).Item("UDNZKN").ToString, Integer).ToString("#,##0")
            dr("GDSKIN") = CType(resultDt.Rows(0).Item("UDNBKN").ToString, Integer).ToString("#,##0")
            dr("CPAYKBN") = "98"
            dtGOODS.Rows.Add(dr)

            '【レシート印刷】
            Dim rePrint As New TMT90.Receipt

            rePrint.intDatKbn = CType(resultDt.Rows(0).Item("DATKB").ToString, Integer)

            rePrint.intGetPoint = CType(resultDt.Rows(0).Item("POINT").ToString, Integer)

            Dim intZANKINGAKU As Integer = 0
            Dim intZANPOINT As Integer = 0

            UIFunction.GetTRNZANKIN4(intZANKINGAKU, intZANPOINT, Now.ToString("yyyyMMdd"), CType(strDENNO, Integer), iDatabase)

            rePrint.intzankingaku = intZANKINGAKU
            rePrint.intzanpoint = intZANPOINT

            rePrint.strManno = resultDt.Rows(0).Item("MANNO").ToString
            rePrint.strccsname = resultDt.Rows(0).Item("CCSNAME").ToString

            rePrint.intPrintKbn = 0
            rePrint.strDENNO = strDENNO
            rePrint.insDTTM = CType(resultDt.Rows(0).Item("INSDTMSTR").ToString, DateTime)
            rePrint.dtGoods = dtGOODS
            rePrint.intGokei = CType(resultDt.Rows(0).Item("UDNBKN").ToString, Integer)
            rePrint.intDeposit = CType(resultDt.Rows(0).Item("NYUKN").ToString, Integer)
            rePrint.intChange = 0
            rePrint.intTax = CType(resultDt.Rows(0).Item("UDNZKN").ToString, Integer)
            rePrint.strHostName = resultDt.Rows(0).Item("HOSTNAME").ToString
            rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
            rePrint.RePrint(False, String.Empty)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' コース料金取消ボタン_Click
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

        Dim strNCSNO As String = String.Empty
        Dim strCCSNAME As String = String.Empty
        Dim intSRTPO As Integer = 0

        Try
            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
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
                If Not CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If
                strNCSNO = dcICR700.NCSNO
                strCCSNAME = "貸出カード"
                intSRTPO = CType(dcICR700.POINT, Integer)
            Else
                strNCSNO = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                strCCSNAME = dtCSMAST.Rows(0).Item("CCSNAME").ToString
                intSRTPO = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)
            End If




            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【カード支出】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.SHISYUTU
                '処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.CANCEL
                '顧客番号
                frm.MANNO = strNCSNO
                '氏名
                frm.CCSNAME = strCCSNAME
                frm.ShowDialog()


                '伝票番号
                strDENNO = frm.DENNO
                'キャンセル
                blnCANCEL = frm.CANCEL
                '支出額
                intUDNKN = frm.UDNKN
                intPREMKN = frm.PREMKN
                'ポイント
                intPOINT = frm.POINT
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

                intKINGAKU = CType(dcICR700.KINGAKU, Integer) + intUDNKN
                Dim intPOINT2 As Integer = intSRTPO - intPOINT


                If intKINGAKU > UIUtility.SYSTEM.ZANMAX Then
                    Using frm As New frmMSGBOX01("入金限度額を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                ElseIf intPOINT2 > 99999 Then
                    Using frm As New frmMSGBOX01("ポイントの上限を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    'カード排出
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
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

                'トランザクション開始
                iDatabase.BeginTransaction()

                If Not CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                    Do


                        '【金額サマリ】
                        sql.Clear()
                        sql.Append("UPDATE KINSMA SET")
                        sql.Append(" ZANKN = " & intZANKN)
                        sql.Append(",PREZANKN = " & intPREZANKN)
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
                UIFunction.UpdBackDATKB(6, CType(strDENNO, Integer), iDatabase)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' コース料金ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNNM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNKN01_001.Click, btnNKNKN02_001.Click, btnNKNKN03_001.Click _
                                                                                                 , btnNKNKN01_002.Click, btnNKNKN02_002.Click, btnNKNKN03_002.Click _
                                                                                                 , btnNKNKN01_003.Click, btnNKNKN02_003.Click, btnNKNKN03_003.Click _
                                                                                                 , btnNKNKN01_004.Click, btnNKNKN02_004.Click, btnNKNKN03_004.Click _
                                                                                                 , btnNKNKN01_005.Click, btnNKNKN02_005.Click, btnNKNKN03_005.Click
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

            'コース料金
            Dim intNKNKN As Integer = CType(btnNKNKN.Tag, Integer)


            Dim intHOLESU As Integer = 0
            Select Case btnNKNKN.Name.ToString
                Case "btnNKNKN01_001" : intHOLESU = 9
                Case "btnNKNKN02_001" : intHOLESU = 18
                Case "btnNKNKN03_001" : intHOLESU = 0
                Case "btnNKNKN01_002" : intHOLESU = 9
                Case "btnNKNKN02_002" : intHOLESU = 18
                Case "btnNKNKN03_002" : intHOLESU = 0
                Case "btnNKNKN01_003" : intHOLESU = 9
                Case "btnNKNKN02_003" : intHOLESU = 18
                Case "btnNKNKN03_003" : intHOLESU = 0
                Case "btnNKNKN01_004" : intHOLESU = 9
                Case "btnNKNKN02_004" : intHOLESU = 18
                Case "btnNKNKN03_004" : intHOLESU = 0
                Case "btnNKNKN01_005" : intHOLESU = 9
                Case "btnNKNKN02_005" : intHOLESU = 18
                Case "btnNKNKN03_005" : intHOLESU = 0
            End Select


            'カード読み込み
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
                If frm.ERRFLG Then Exit Sub
            End Using

            'コース料金精算処理
            Dim strMsg As String = String.Empty
            Dim blnCARD As Boolean = True
            If Not UpdKOSUMTA(btnNKNKN.Text, intNKNKN, intHOLESU, blnCARD, strMsg) Then
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
        Finally
            '顧客番号
            Me.lblNCSNO.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME.Text = String.Empty
            '氏名
            Me.lblCCSNAME.Text = String.Empty
            '誕生日
            Me.lblDBIRTH.Text = String.Empty
            '年齢
            Me.lblAge.Text = String.Empty
            '残金額
            Me.lblKINGAKU.Text = String.Empty
            '残ポイント
            Me.lblPOINT.Text = String.Empty
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

            '支出ボタン
            Me.btnNKNKN01_001.Visible = False
            Me.btnNKNKN02_001.Visible = False
            Me.btnNKNKN03_001.Visible = False


            Me.lblNKNKN01_001.Visible = False
            Me.lblNKNKN02_001.Visible = False
            Me.lblNKNKN03_001.Visible = False


            Me.btnNKNKN01_002.Visible = False
            Me.btnNKNKN02_002.Visible = False
            Me.btnNKNKN03_002.Visible = False


            Me.lblNKNKN01_002.Visible = False
            Me.lblNKNKN02_002.Visible = False
            Me.lblNKNKN03_002.Visible = False


            Me.btnNKNKN01_003.Visible = False
            Me.btnNKNKN02_003.Visible = False
            Me.btnNKNKN03_003.Visible = False


            Me.lblNKNKN01_003.Visible = False
            Me.lblNKNKN02_003.Visible = False
            Me.lblNKNKN03_003.Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' コース料金選択
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectKosu() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            Me.chkPOINT.Checked = False
            Me.chkPOINT2.Checked = False
            Me.chkPOINTW.Checked = False
            Me.chkPOINTS.Checked = False

            '顧客番号
            Me.lblNCSNO.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME.Text = String.Empty
            '氏名
            Me.lblCCSNAME.Text = String.Empty
            '誕生日
            Me.lblDBIRTH.Text = String.Empty
            '年齢
            Me.lblAge.Text = String.Empty
            '残金額
            Me.lblKINGAKU.Text = String.Empty
            '残ポイント
            Me.lblPOINT.Text = String.Empty

            If dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
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
                Return False
            End If

            GetEIGMTA(CType(dtCSMAST.Rows(0).Item("NCSRANK").ToString, Integer))


            '来場ポイント
            If String.IsNullOrEmpty(dtCSMAST.Rows(0).Item("ZENENTDATE").ToString) Or dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals("00000000") Then
                Me.chkPOINT.Checked = True
            Else
                If Not dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.Equals(Now.ToString("yyyyMMdd")) Then
                    Me.chkPOINT.Checked = True
                End If
            End If
            If UIFunction.GetKosuEnt(dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), iDatabase) > 0 Then
                Me.chkPOINT.Checked = False
            End If

            Select Case dtCSMAST.Rows(0).Item("NCSRANK").ToString
                Case "2"    'ビジター
                    Me.rdoVisitor.Checked = True
                Case "7"    'ジュニア
                    Me.rdoJunior.Checked = True
                Case Else   'メンバー
                    Me.rdoMember.Checked = True
            End Select

            Dim intTab As Integer = 0

            If Not Me.rdoJunior.Checked Then
                'ビジター・メンバーの時
                If Not String.IsNullOrEmpty(dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString) Then
                    Dim intBirthMM As Integer = 0
                    intBirthMM = CType(dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString.Replace("/", String.Empty).Substring(4, 2), Integer)
                    Dim intAge As Integer = 0
                    intAge = UIFunction.GetAge(DateTime.Parse(dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString), Now)

                    If intAge >= 70 Then
                        '70歳以上
                        intTab = 1
                        Me.chkPOINTS.Checked = True
                    ElseIf intAge <= 40 Then
                        '40歳以下
                        intTab = 4
                        Me.chkPOINT2.Checked = True
                    ElseIf intBirthMM.Equals(Now.Month) Then
                        '誕生月の人
                        intTab = 3
                        Me.chkPOINT2.Checked = True
                    End If
                End If
            End If

            If dtCSMAST.Rows(0).Item("NSEX").ToString.Equals("2") Then
                Me.chkPOINTW.Checked = True
            End If

            If Not Me.chkPOINT.Checked Then
                Me.chkPOINTW.Checked = False
                Me.chkPOINTS.Checked = False
            End If

            Me.tabNKN.SelectedIndex = intTab

            '顧客番号
            Me.lblNCSNO.Text = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
            '顧客種別
            Me.lblCKBNAME.Text = dtCSMAST.Rows(0).Item("CKBNAME").ToString
            '氏名
            Me.lblCCSNAME.Text = dtCSMAST.Rows(0).Item("CCSNAME").ToString
            '誕生日
            Me.lblDBIRTH.Text = dtCSMAST.Rows(0).Item("TO_DBIRTH").ToString
            If Not String.IsNullOrEmpty(Me.lblDBIRTH.Text) Then
                Me.lblAge.Text = UIFunction.GetAge(DateTime.Parse(Me.lblDBIRTH.Text), Now).ToString & "歳"
            End If
            '残金額
            Me.lblKINGAKU.Text = (CType(dcICR700.ZANKN, Integer) + CType(dcICR700.PREZANKN, Integer)).ToString("#,##0")
            'ポイント
            Me.lblPOINT.Text = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString, Integer).ToString("#,##0")

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ﾎﾟｲﾝﾄ情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEIGMTA(ByVal intNKBNO As Integer) As Boolean
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
            sql.Append(" AND A.NKBNO = " & intNKBNO)
            sql.Append(" ORDER BY A.NKBNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim drEIGMTA() As DataRow

            drEIGMTA = resultDt.Select("NKBNO = " & intNKBNO)
            If drEIGMTA.Length > 0 Then
                '来場ポイント
                Me.chkPOINT.Text = "来場ポイント " & CType(resultDt.Rows(0).Item("POINT").ToString, Integer) & "P"
                Me.chkPOINT.Tag = CType(resultDt.Rows(0).Item("POINT").ToString, Integer)
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.chkPOINTW.Text = "レディースポイント " & CType(resultDt.Rows(0).Item("POINTW").ToString, Integer) & "P"
                Me.chkPOINTW.Tag = CType(resultDt.Rows(0).Item("POINTW").ToString, Integer)
                'シニアポイント
                Me.chkPOINTS.Text = "シニアポイント " & CType(resultDt.Rows(0).Item("POINTS").ToString, Integer) & "P"
                Me.chkPOINTS.Tag = CType(resultDt.Rows(0).Item("POINTS").ToString, Integer)
            Else
                Me.chkPOINT.Text = "来場ポイント " & "0P"
                Me.chkPOINT.Tag = 0
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.chkPOINTW.Text = "レディースポイント " & "0P"
                Me.chkPOINTW.Tag = 0
                'シニアポイント
                Me.chkPOINTS.Text = "シニアポイント " & "0P"
                Me.chkPOINTS.Tag = 0
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' コース料金マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKOSUMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intNKBNO As Integer = 0
        Try
            If Me.rdoMember.Checked Then
                intNKBNO = 1
            ElseIf Me.rdoVisitor.Checked Then
                intNKBNO = 2
            Else
                intNKBNO = 3
            End If

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE ")
            sql.Append(" RKNKB = " & UIUtility.SYSTEM.RKNKB)
            sql.Append(" AND NKBNO = " & intNKBNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            '保険料・カート料・コンペ料
            Me.lblHOKENKIN.Text = CType(resultDt.Rows(0).Item("HOKENKIN").ToString, Integer).ToString("#,##0円")
            Me.lblHOKENKIN.Tag = CType(resultDt.Rows(0).Item("HOKENKIN").ToString, Integer)
            Me.lblCARTKIN.Text = CType(resultDt.Rows(0).Item("CARTKIN").ToString, Integer).ToString("#,##0円")
            Me.lblCARTKIN.Tag = CType(resultDt.Rows(0).Item("CARTKIN").ToString, Integer)
            Me.lblCOMPEKIN.Text = CType(resultDt.Rows(0).Item("COMPEKIN").ToString, Integer).ToString("#,##0円")
            Me.lblCOMPEKIN.Tag = CType(resultDt.Rows(0).Item("COMPEKIN").ToString, Integer)

            '誕生月・U40ポイント
            Me.txtPOINT2.Text = CType(resultDt.Rows(0).Item("POINT2").ToString, Integer).ToString

            Dim NKNTAX As Integer = 0
            Dim dr() As DataRow

            '//【タグ区分001】//
            '一般
            dr = resultDt.Select("PRCKB =  1")
            If dr.Length > 0 Then
                Me.TabPage1.Text = dr(0).Item("PRCNM").ToString
                Me.btnNKNKN01_001.Text = "9H" & vbCr & CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_001.Tag = CType(dr(0).Item("H09KIN").ToString, Integer)
                Me.lblNKNKN01_001.Text = CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_001.Visible = True
                Me.lblNKNKN01_001.Visible = True

                Me.btnNKNKN02_001.Text = "18H" & vbCr & CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_001.Tag = CType(dr(0).Item("H18KIN").ToString, Integer)
                Me.lblNKNKN02_001.Text = CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_001.Visible = True
                Me.lblNKNKN02_001.Visible = True

                Me.btnNKNKN03_001.Text = "廻放題" & vbCr & CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_001.Tag = CType(dr(0).Item("H00KIN").ToString, Integer)
                Me.lblNKNKN03_001.Text = CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_001.Visible = True
                Me.lblNKNKN03_001.Visible = True
            End If

            '//【タグ区分002】//
            'シニア
            dr = resultDt.Select("PRCKB =  2")
            If dr.Length > 0 Then
                Me.TabPage2.Text = dr(0).Item("PRCNM").ToString
                Me.btnNKNKN01_002.Text = "9H" & vbCr & CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_002.Tag = CType(dr(0).Item("H09KIN").ToString, Integer)
                Me.lblNKNKN01_002.Text = CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_002.Visible = True
                Me.lblNKNKN01_002.Visible = True

                Me.btnNKNKN02_002.Text = "18H" & vbCr & CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_002.Tag = CType(dr(0).Item("H18KIN").ToString, Integer)
                Me.lblNKNKN02_002.Text = CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_002.Visible = True
                Me.lblNKNKN02_002.Visible = True

                Me.btnNKNKN03_002.Text = "廻放題" & vbCr & CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_002.Tag = CType(dr(0).Item("H00KIN").ToString, Integer)
                Me.lblNKNKN03_002.Text = CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_002.Visible = True
                Me.lblNKNKN03_002.Visible = True
            End If

            '//【タグ区分003】//
            '早朝薄暮
            dr = resultDt.Select("PRCKB =  3")
            If dr.Length > 0 Then
                Me.TabPage3.Text = dr(0).Item("PRCNM").ToString
                Me.btnNKNKN01_003.Text = "9H" & vbCr & CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_003.Tag = CType(dr(0).Item("H09KIN").ToString, Integer)
                Me.lblNKNKN01_003.Text = CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_003.Visible = True
                Me.lblNKNKN01_003.Visible = True

                Me.btnNKNKN02_003.Text = "18H" & vbCr & CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_003.Tag = CType(dr(0).Item("H18KIN").ToString, Integer)
                Me.lblNKNKN02_003.Text = CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_003.Visible = True
                Me.lblNKNKN02_003.Visible = True

                Me.btnNKNKN03_003.Text = "廻放題" & vbCr & CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_003.Tag = CType(dr(0).Item("H00KIN").ToString, Integer)
                Me.lblNKNKN03_003.Text = CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_003.Visible = True
                Me.lblNKNKN03_003.Visible = True
            End If

            '//【タグ区分004】//
            '誕生月
            dr = resultDt.Select("PRCKB =  4")
            If dr.Length > 0 Then
                Me.TabPage4.Text = dr(0).Item("PRCNM").ToString
                Me.btnNKNKN01_004.Text = "9H" & vbCr & CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_004.Tag = CType(dr(0).Item("H09KIN").ToString, Integer)
                Me.lblNKNKN01_004.Text = CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_004.Visible = True
                Me.lblNKNKN01_004.Visible = True

                Me.btnNKNKN02_004.Text = "18H" & vbCr & CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_004.Tag = CType(dr(0).Item("H18KIN").ToString, Integer)
                Me.lblNKNKN02_004.Text = CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_004.Visible = True
                Me.lblNKNKN02_004.Visible = True

                Me.btnNKNKN03_004.Text = "廻放題" & vbCr & CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_004.Tag = CType(dr(0).Item("H00KIN").ToString, Integer)
                Me.lblNKNKN03_004.Text = CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_004.Visible = True
                Me.lblNKNKN03_004.Visible = True
            End If

            '//【タグ区分005】//
            'U40
            dr = resultDt.Select("PRCKB =  5")
            If dr.Length > 0 Then
                Me.TabPage5.Text = dr(0).Item("PRCNM").ToString
                Me.btnNKNKN01_005.Text = "9H" & vbCr & CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_005.Tag = CType(dr(0).Item("H09KIN").ToString, Integer)
                Me.lblNKNKN01_005.Text = CType(dr(0).Item("H09KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN01_005.Visible = True
                Me.lblNKNKN01_005.Visible = True

                Me.btnNKNKN02_005.Text = "18H" & vbCr & CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_005.Tag = CType(dr(0).Item("H18KIN").ToString, Integer)
                Me.lblNKNKN02_005.Text = CType(dr(0).Item("H18KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN02_005.Visible = True
                Me.lblNKNKN02_005.Visible = True

                Me.btnNKNKN03_005.Text = "廻放題" & vbCr & CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_005.Tag = CType(dr(0).Item("H00KIN").ToString, Integer)
                Me.lblNKNKN03_005.Text = CType(dr(0).Item("H00KIN").ToString, Integer).ToString("#,##0") & "円"
                Me.btnNKNKN03_005.Visible = True
                Me.lblNKNKN03_005.Visible = True
            End If


            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function


    ''' <summary>
    ''' コース料金精算処理
    ''' </summary>
    ''' <param name="strNKNNM"></param>
    ''' <param name="intNKNKN"></param>
    ''' <param name="intHOLESU"></param>
    ''' <param name="blnCARD"></param>
    ''' <param name="strMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdKOSUMTA(ByVal strNKNNM As String, ByVal intNKNKN As Integer, ByVal intHOLESU As Integer, ByRef blnCARD As Boolean, ByRef strMsg As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intHOKENKIN As Integer = 0
        Dim intCARTKIN As Integer = 0
        Dim intCOMPEKIN As Integer = 0
        Dim intSRTPO As Integer = 0
        Dim intPOINT As Integer = 0
        Dim intPOINT2 As Integer = 0
        Dim intGetPOINT As Integer = 0
        Dim intPREMKN As Integer = 0
        Try
            strNKNNM = String.Empty

            If Me.tabNKN.SelectedIndex.Equals(1) Then
                strNKNNM &= "S,"
            ElseIf Me.tabNKN.SelectedIndex.Equals(3) Then
                strNKNNM &= "誕生,"
            ElseIf Me.tabNKN.SelectedIndex.Equals(4) Then
                strNKNNM &= "U40,"
            End If

            If intHOLESU.Equals(0) Then
                strNKNNM &= "廻放題"
            Else
                strNKNNM &= intHOLESU.ToString & "H"
            End If

            '保険料
            If Me.chkHOKENKIN.Checked Then
                strNKNNM &= ",保険"
                intHOKENKIN = CType(Me.lblHOKENKIN.Tag, Integer)
                intNKNKN += intHOKENKIN
            End If
            'カート料
            If Me.chkCARTKIN.Checked Then
                strNKNNM &= ",ｶｰﾄ"
                intCARTKIN = CType(Me.lblCARTKIN.Tag, Integer)
                intNKNKN += intCARTKIN
            End If
            'コンペ料
            If Me.chkCOMPEKIN.Checked Then
                strNKNNM &= ",ｺﾝﾍﾟ"
                intCOMPEKIN = CType(Me.lblCOMPEKIN.Tag, Integer)
                intNKNKN += intCOMPEKIN
            End If

            '来場ポイント
            If Me.chkPOINT.Checked Then
                intPOINT = CType(Me.chkPOINT.Tag, Integer)
                If Me.chkPOINTW.Checked Then
                    'レディースポイント
                    intPOINT += CType(Me.chkPOINTW.Tag, Integer)
                End If
                If Me.chkPOINTS.Checked Then
                    'シニアポイント
                    intPOINT += CType(Me.chkPOINTS.Tag, Integer)
                End If
            End If
            '誕生月・U40ポイント
            If Me.chkPOINT2.Checked And Not String.IsNullOrEmpty(Me.txtPOINT2.Text) Then
                intPOINT2 = CType(Me.txtPOINT2.Text, Integer)
            End If
            '精算に含めるかどうか
            If Me.chkSeisan.Checked Then
                intPREMKN = intPOINT2
                intNKNKN -= intPOINT2
                intPOINT2 = 0
            End If
            intGetPOINT = intPOINT + intPOINT2


            'プリカ金額
            Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)
            'V31残金
            Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
            'V31残プレミアム
            Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)


            'ﾌﾟﾚﾐｱﾑからの精算あり
            If CType(dcICR700.KINGAKU, Integer) - intNKNKN < 0 Then
                strMsg = "残金が不足しています。"
                Return False
            End If

            '残金から支払われた金額
            Dim intPayKINGAKU As Integer = 0
            'プレミアムから支払われた金額
            Dim intPayPREMKN As Integer = 0


            'ﾌﾟﾚﾐｱﾑからの精算あり


            If intV31PREZANKN >= intNKNKN Then
                '【残プレミアム >= 支出額】
                'プレミアムから支払った金額
                intPayPREMKN = intNKNKN
            Else
                '残金から支払った金額
                intPayKINGAKU = intNKNKN - intV31PREZANKN
                'プレミアムから支払った金額
                intPayPREMKN = intV31PREZANKN
            End If


            Dim intNKNTAX As Integer = 0
            If intNKNKN > 0 Then
                intNKNTAX = UIFunction.CalcExcludedTax(intNKNKN)
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
                If Not CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                    strMsg = "顧客データがありません。"
                    Return False
                End If
                intSRTPO = CType(dcICR700.POINT, Integer)
            Else
                intSRTPO = CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer)

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
            dcICR700.KINGAKU_WR = (intKINGAKU - intNKNKN).ToString.PadLeft(5, "0"c)
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
            dcICR700.ZANKN_WR = (intV31ZANKN - intPayKINGAKU).ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = (intV31PREZANKN - intPayPREMKN).ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = (intSRTPO + intGetPOINT).ToString.PadLeft(5, "0"c)
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

            If Not CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & (intV31ZANKN - intPayKINGAKU))
                sql.Append(",PREZANKN = " & (intV31PREZANKN - intPayPREMKN))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    iDatabase.RollBack()
                    Return False
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & (CType(dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + intGetPOINT))
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
            '分類コード１【006】コース料金
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'006',"
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
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
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
            strSQL2 &= (intNKNKN - intNKNTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intNKNKN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intNKNTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intGetPOINT & ","
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= intNKNKN & ","
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
            strSQL2 &= "'9',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & strNKNNM & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPayPREMKN & ","
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
            '分類コード１【005】コース料金
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'006',"
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
            strSQL2 &= "'" & strNKNNM & "',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= intNKNKN & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "NULL,"
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
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
            strSQL2 &= (intNKNKN - intNKNTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intNKNKN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intNKNTAX & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intGetPOINT & ","
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "NULL,"
            'ポイント付与対象区分
            strSQL1 &= "POZEIKB,"
            strSQL2 &= "NULL,"
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
            strSQL2 &= "'9',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'" & strNKNNM & "',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= intPayPREMKN & ")"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "伝票トランの更新に失敗しました。"
                iDatabase.RollBack()
                Return False
            End If
            '【コース料金トラン】
            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO KOSUTRN("
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
            If CType(dcICR700.NCSNO, Integer) >= 50000000 Then
                strSQL2 &= "'" & dcICR700.NCSNO & "',"
            Else
                strSQL2 &= "'" & dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            End If
            '料金体系
            strSQL1 &= "RKNKB,"
            strSQL2 &= UIUtility.SYSTEM.RKNKB & ","
            '顧客種別区分
            strSQL1 &= "NKBNO,"
            If Me.rdoMember.Checked Then
                strSQL2 &= "1,"
            ElseIf Me.rdoVisitor.Checked Then
                strSQL2 &= "2,"
            Else
                strSQL2 &= "3,"
            End If
            '料金区分
            strSQL1 &= "PRCKB,"
            strSQL2 &= Me.tabNKN.SelectedIndex + 1 & ","
            'ホール数
            strSQL1 &= "HOLESU,"
            strSQL2 &= intHOLESU & ","
            '保険料
            strSQL1 &= "HOKENKIN,"
            strSQL2 &= intHOKENKIN & ","
            'カート料
            strSQL1 &= "CARTKIN,"
            strSQL2 &= intCARTKIN & ","
            'コンペ料
            strSQL1 &= "COMPEKIN,"
            strSQL2 &= intCOMPEKIN & ","
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= (intNKNKN - intNKNTAX) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= intNKNKN & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= intNKNTAX & ","
            '来場ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= intPOINT & ","
            '誕生月・U40ポイント
            strSQL1 &= "POINT2,"
            strSQL2 &= intPOINT2 & ","
            'プレミアム
            strSQL1 &= "PREMKN,"
            strSQL2 &= intPREMKN & ","
            '残金精算前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intV31ZANKN & ","
            '残金精算後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intV31ZANKN - intPayKINGAKU & ","
            'P)残金精算前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intV31PREZANKN & ","
            'P)残金精算後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= intV31PREZANKN - intPayPREMKN & ","
            '残ポイント精算前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= intSRTPO & ","
            '残ポイント精算後
            strSQL1 &= "ZANBPO,"
            strSQL2 &= intSRTPO + intGetPOINT & ","
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                strMsg = "コース料金トランの更新に失敗しました。"
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
            End Using
            If blnERRFLG Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                    frm.ShowDialog()
                End Using
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
                dr("GDSNAME") = strNKNNM
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = intNKNKN.ToString("#,##0")
                dr("CPAYKBN") = "98"
                dtGOODS.Rows.Add(dr)

                '【レシート印刷】
                Dim rePrint As New TMT90.Receipt

                rePrint.intzankingaku = (intKINGAKU - intNKNKN)
                rePrint.intzanpoint = intSRTPO

                rePrint.strManno = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                rePrint.strccsname = dtCSMAST.Rows(0).Item("CCSNAME").ToString

                rePrint.intGetPoint = intGetPOINT

                rePrint.intPrintKbn = 0
                rePrint.strDENNO = dtSEQTRN.Rows(0).Item("DENNO").ToString.PadLeft(4, "0"c)
                rePrint.insDTTM = dtmInsDt
                rePrint.dtGoods = dtGOODS
                rePrint.intGokei = CType(dtGOODS.Rows(0).Item("GDSKIN").ToString, Integer)
                rePrint.intDeposit = intNKNKN
                rePrint.intChange = 0
                rePrint.intTax = intNKNTAX
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

