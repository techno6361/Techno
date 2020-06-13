Imports TECHNO.DataBase

Public Class frmCARDREPAIR01

#Region "▼宣言部"

    ''' <summary>
    ''' 顧客情報
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtCSMAST As DataTable
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _strNCSNO As String = String.Empty
    ''' <summary>
    ''' 氏名
    ''' </summary>
    ''' <remarks></remarks>
    Private _strCCSNAME As String = String.Empty
    ''' <summary>
    ''' カード番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _strCARDNO As String = String.Empty
    ''' <summary>
    ''' スクール生番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSCLMANNO As String = String.Empty
    ''' <summary>
    ''' 顧客種別
    ''' </summary>
    ''' <remarks></remarks>
    Private _strNCSRANK As String = String.Empty
    ''' <summary>
    ''' 会員期限
    ''' </summary>
    ''' <remarks></remarks>
    Private _strDMEMBER As String = String.Empty
    ''' <summary>
    ''' 前回来場日
    ''' </summary>
    ''' <remarks></remarks>
    Private _strZENENTDATE As String = String.Empty
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "カード修復"

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

            MyBase.l_Title_FormName = "カード修復"

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


#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCARDREPAIR01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

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
    Private Sub frmCARDREPAIR01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.btnCard.PerformClick()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCARDREPAIR01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Dim blnERRFLG As Boolean = False
        Try
            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                CardEject()
            End If

            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード挿入・修復ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard.Click
        Dim blnCard As Boolean = False
        Dim blnERRFLG As Boolean = False
        Try

            If CType(Me.btnCard.Tag, Integer).Equals(0) Then
                '【カード挿入】

                Me.lblErrMsg1.Visible = False
                Me.lblErrMsg2.Visible = False

                'カード情報取得
                If GetCardInfo(blnCard) Then
                    Me.btnCard.Text = "カード修復"
                    Me.btnCard.Tag = 1
                End If
                If blnCard Then
                    'カード排出コマンド
                    Using frm As New frmREQUESTCARD(dcICR700)
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
                End If

            Else
                '【カード修復】

                Using frm As New frmMSGBOX01("ｶｰﾄﾞ修復を行ってよろしいですか？", 1)
                    frm.ShowDialog()
                    If Not frm.Reply Then
                        Exit Sub
                    End If
                End Using

                If Repair() Then
                    Me.btnCard.Text = "カード挿入"
                    Me.btnCard.Tag = 0
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

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

            Me.btnCard.Tag = 0


            '【PREリーダライター】
            Me.txtRW1ZANKN.Text = "0"
            Me.lblRW1ZANKN.Text = "0"
            '【V31リーダライター】
            Me.txtRW2ZANKN.Text = "0"
            Me.txtRW2PREMKN.Text = "0"
            Me.txtRW2SRTPO.Text = "0"
            Me.lblRW2ZANKN.Text = "0"
            Me.lblRW2PREMKN.Text = "0"
            Me.lblRW2SRTPO.Text = "0"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード情報取得
    ''' </summary>
    ''' <param name="blnCard"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCardInfo(ByRef blnCard As Boolean) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmREQUESTCARD(dcICR700)
                frm.COMMAND = frmREQUESTCARD.Command_Type.REPAIR
                frm.ShowDialog()
                '取消
                If frm.CANCEL Then Return False
                If frm.ERRFLG Then Return False
            End Using

            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Clear()
            End If

            '顧客情報取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS TO_DBIRTH")  '誕生日
            sql.Append(",TO_CHAR(A.DENTRY,'YYYY/MM/DD') AS TO_DENTRY") '会員登録日
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYYMMDD') AS TO_DMEMBER") '会員期限
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
            sql.Append(" NCSNO = " & CType(dcICR700.NCSNO, Integer))
            sql.Append(" AND NCARDID = " & CType(dcICR700.CARDNO, Integer))

            _dtCSMAST = iDatabase.ExecuteRead(sql.ToString())

            If _dtCSMAST.Rows.Count.Equals(0) Then
                If Not (CType(dcICR700.NCSNO, Integer) >= 10000000) Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    blnCard = True
                    Return False
                End If
            End If

            Dim intZANKN As Integer = 0
            Dim intPREZANKN As Integer = 0
            Dim intPOINT As Integer = 0
            If CType(dcICR700.NCSNO, Integer) >= 10000000 Then
                '【ﾌﾘｰｶｰﾄﾞ】

                'RW①
                If DamageCheck(1) Then
                    '【破損無し】

                    Me.lblRW1ZANKN.Text = CType(dcICR700.KINGAKU, Integer).ToString("#,##0")
                    Me.txtRW1ZANKN.Text = Me.lblRW1ZANKN.Text
                Else
                    Me.lblRW1ZANKN.Text = "0"
                    Me.txtRW1ZANKN.Text = "0"
                    Me.lblErrMsg1.Visible = True
                End If
                'RW②
                If DamageCheck(2) Then
                    '【破損無し】
                    Me.lblRW2ZANKN.Text = CType(dcICR700.ZANKN, Integer).ToString("#,##0")
                    Me.lblRW2PREMKN.Text = "0"
                    Me.lblRW2SRTPO.Text = "0"
                    Me.txtRW2ZANKN.Text = CType(dcICR700.ZANKN, Integer).ToString("#,##0")
                    Me.txtRW2PREMKN.Text = "0"
                    Me.txtRW2SRTPO.Text = "0"
                Else
                    Me.lblErrMsg2.Visible = True
                    Using frm As New frmMSGBOX01("修復ができないカードです。", 2)
                        frm.ShowDialog()
                    End Using
                    blnCard = True
                    Return False
                End If

                '顧客番号
                _strNCSNO = dcICR700.NCSNO
                '氏名
                _strCCSNAME = "ﾌﾘｰｶｰﾄﾞ"
                'カード番号
                _strCARDNO = dcICR700.CARDNO
                'スクール生番号
                _strSCLMANNO = dcICR700.SCLMANNO
                '顧客種別
                _strSCLMANNO = dcICR700.NKBNO
                '会員期限
                _strDMEMBER = dcICR700.DMEMBER
                '前回来場日
                _strZENENTDATE = dcICR700.ZENENTDATE
            Else
                '【顧客登録済みカード】

                'RW①
                If DamageCheck(1) Then
                    '【破損無し】

                    Me.lblRW1ZANKN.Text = (CType(_dtCSMAST.Rows(0).Item("ZANKN").ToString, Integer) + CType(_dtCSMAST.Rows(0).Item("PREZANKN").ToString, Integer)).ToString("#,##0")
                    Me.txtRW1ZANKN.Text = (CType(_dtCSMAST.Rows(0).Item("ZANKN").ToString, Integer) + CType(_dtCSMAST.Rows(0).Item("PREZANKN").ToString, Integer)).ToString("#,##0")

                Else
                    Me.lblRW1ZANKN.Text = "0"
                    Me.txtRW1ZANKN.Text = (CType(_dtCSMAST.Rows(0).Item("ZANKN").ToString, Integer) + CType(_dtCSMAST.Rows(0).Item("PREZANKN").ToString, Integer)).ToString("#,##0")
                    Me.lblErrMsg1.Visible = True
                End If
                'RW②
                If DamageCheck(2) Then
                    '【破損無し】

                    Me.lblRW2ZANKN.Text = CType(dcICR700.ZANKN, Integer).ToString("#,##0")
                    Me.lblRW2PREMKN.Text = CType(dcICR700.PREZANKN, Integer).ToString("#,##0")
                    Me.lblRW2SRTPO.Text = CType(dcICR700.POINT, Integer).ToString("#,##0")
                    Me.txtRW2ZANKN.Text = CType(dcICR700.ZANKN, Integer).ToString("#,##0")
                    Me.txtRW2PREMKN.Text = CType(dcICR700.PREZANKN, Integer).ToString("#,##0")
                    Me.txtRW2SRTPO.Text = CType(dcICR700.POINT, Integer).ToString("#,##0")
                Else
                    Me.lblRW2ZANKN.Text = "0"
                    Me.lblRW2PREMKN.Text = "0"
                    Me.lblRW2SRTPO.Text = "0"
                    Me.txtRW2ZANKN.Text = CType(_dtCSMAST.Rows(0).Item("ZANKN").ToString, Integer).ToString("#,##0")
                    Me.txtRW2PREMKN.Text = CType(_dtCSMAST.Rows(0).Item("PREZANKN").ToString, Integer).ToString("#,##0")
                    Me.txtRW2SRTPO.Text = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString, Integer).ToString("#,##0")
                    Me.lblErrMsg2.Visible = True
                End If
                '顧客番号
                _strNCSNO = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                '氏名
                _strCCSNAME = _dtCSMAST.Rows(0).Item("CCSNAME").ToString
                'カード番号
                If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("NCARDID").ToString) Then
                    _strCARDNO = "000000"
                Else
                    _strCARDNO = _dtCSMAST.Rows(0).Item("NCARDID").ToString.PadLeft(8, "0"c)
                End If
                'スクール生番号
                If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("DSCLMANNO").ToString) Then
                    _strSCLMANNO = "000000"
                Else
                    _strSCLMANNO = _dtCSMAST.Rows(0).Item("DSCLMANNO").ToString.PadLeft(6, "0"c)
                End If
                '顧客種別
                _strNCSRANK = _dtCSMAST.Rows(0).Item("NCSRANK").ToString
                '会員期限
                If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString) Then
                    _strDMEMBER = "00000000"
                Else
                    _strDMEMBER = _dtCSMAST.Rows(0).Item("TO_DMEMBER").ToString.PadLeft(8, "0"c)
                End If
                '前回来場日
                If String.IsNullOrEmpty(_dtCSMAST.Rows(0).Item("ZENENTDATE").ToString) Then
                    _strZENENTDATE = "00000000"
                Else
                    _strZENENTDATE = _dtCSMAST.Rows(0).Item("ZENENTDATE").ToString.PadLeft(8, "0"c)
                End If
            End If

            If Not Me.lblErrMsg1.Visible And Not Me.lblErrMsg2.Visible Then
                Using frm As New frmMSGBOX01("修復する必要がないカードです。", 2)
                    frm.ShowDialog()
                End Using
                blnCard = True
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' カード修復
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Repair() As Boolean
        Try
            '【プリカRW書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = "00"
            'シリアルナンバー
            dcICR700.SERIALNO_WR = "FFF"
            '種別
            dcICR700.SYUBETU_WR = _strNCSRANK
            '金額
            dcICR700.KINGAKU_WR = (CType(Me.txtRW2ZANKN.Text, Integer) + CType(Me.txtRW2PREMKN.Text, Integer)).ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = "F"

            '【V31RW書き込み情報セット】
            '店番
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'カード区分
            dcICR700.CARDKBN_WR = "1"
            'カード番号
            dcICR700.CARDNO_WR = _strCARDNO
            '顧客番号
            dcICR700.NCSNO_WR = _strNCSNO
            'スクール生番号
            dcICR700.SCLMANNO_WR = _strSCLMANNO
            '顧客種別
            dcICR700.NKBNO_WR = _strNCSRANK
            '会員期限
            dcICR700.DMEMBER_WR = _strDMEMBER
            'パスワード
            dcICR700.PASSCD_WR = "00"
            '残金額
            dcICR700.ZANKN_WR = CType(Me.txtRW2ZANKN.Text, Integer).ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = CType(Me.txtRW2PREMKN.Text, Integer).ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = CType(Me.txtRW2SRTPO.Text, Integer).ToString.PadLeft(5, "0"c)
            '前回来場日
            dcICR700.ZENENTDATE_WR = _strZENENTDATE
            '入場区分
            dcICR700.ENTKBN_WR = "0"
            'ボール単価
            dcICR700.BALLKIN_WR = "0"

            iDatabase.BeginTransaction()

            Dim sql As New System.Text.StringBuilder
            Dim strSQL1 As String = String.Empty
            Dim strSQL2 As String = String.Empty

            If Not CType(_strNCSNO, Integer) >= 10000000 Then
                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & CType(Me.txtRW2ZANKN.Text, Integer))
                sql.Append(",PREZANKN = " & CType(Me.txtRW2PREMKN.Text, Integer))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _strNCSNO & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.Close()
                    Return False
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & CType(Me.txtRW2SRTPO.Text, Integer))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & dcICR700.NCSNO & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("ポイントサマリの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.Close()
                    Return False
                End If
                strSQL1 = String.Empty
                strSQL2 = String.Empty
                strSQL1 &= "INSERT INTO CRTTRN("
                strSQL2 &= " VALUES("
                '修正日
                strSQL1 &= "CRTDT,"
                strSQL2 &= "'" & Now.ToString("yyyyMMdd") & "',"
                '修正時間
                strSQL1 &= "CRTTIME,"
                strSQL2 &= "'" & Now.Hour.ToString.PadLeft(2, "0"c) & Now.Minute.ToString.PadLeft(2, "0"c) & "',"
                '顧客番号
                strSQL1 &= "NCSNO,"
                strSQL2 &= CType(_strNCSNO, Integer) & ","
                '修正後残金
                strSQL1 &= "ZANKN,"
                strSQL2 &= CType(Me.txtRW2ZANKN.Text, Integer) & ","
                '修正後残プレミアム
                strSQL1 &= "PREZANKN,"
                strSQL2 &= CType(Me.txtRW2PREMKN.Text, Integer) & ","
                '修正後残ポイント
                strSQL1 &= "SRTPO,"
                strSQL2 &= CType(Me.txtRW2SRTPO.Text, Integer) & ","
                '修正前残金
                strSQL1 &= "MAEZANKN,"
                strSQL2 &= CType(Me.lblRW2ZANKN.Text, Integer) & ","
                '移行元残プレミアム
                strSQL1 &= "MAEPREZANKN,"
                strSQL2 &= CType(Me.lblRW2PREMKN.Text, Integer) & ","
                '移行元残ポイント
                strSQL1 &= "MAESRTPO,"
                strSQL2 &= CType(Me.lblRW2SRTPO.Text, Integer) & ","
                '修正区分
                strSQL1 &= "CRTKBN,"
                If Me.lblErrMsg1.Visible And Me.lblErrMsg2.Visible Then
                    '【両方破損】
                    strSQL2 &= "3,"
                ElseIf Me.lblErrMsg1.Visible Then
                    '【RW①破損】
                    strSQL2 &= "1,"
                ElseIf Me.lblErrMsg2.Visible Then
                    '【RW②破損】
                    strSQL2 &= "2,"
                Else
                    '【正常】
                    strSQL2 &= "0,"
                End If
                'スタッフコード
                strSQL1 &= "STFCODE,"
                strSQL2 &= "NULL,"
                'スタッフ名
                strSQL1 &= "STFNAME,"
                strSQL2 &= "NULL,"
                '作成日時
                strSQL1 &= "INSDTM)"
                strSQL2 &= "NOW())"

                If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("残高修正トランの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.Close()
                    Return False
                End If
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
                Return True
            End If

            iDatabase.Commit()

            Return True
        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' カード排出
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CardEject() As Boolean
        Dim blnERRFLG As Boolean = False
        Try
            'カード排出コマンド
            Using frm As New frmREQUESTCARD(dcICR700)
                frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                    frm.ShowDialog()
                    Return False
                End Using
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' カードダメージチェック
    ''' </summary>
    ''' <param name="intRwKbn">【1】RW①【2】RW②</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DamageCheck(ByVal intRwKbn As Integer) As Boolean
        Try
            If intRwKbn.Equals(1) Then
                '【RW①】

                If String.IsNullOrEmpty(dcICR700.SHOPNO) Then Return False
                If String.IsNullOrEmpty(dcICR700.PASSCD) Then Return False
                If String.IsNullOrEmpty(dcICR700.SERIALNO) Then Return False
                If String.IsNullOrEmpty(dcICR700.SYUBETU) Then Return False
                If String.IsNullOrEmpty(dcICR700.KINGAKU) Then Return False
                If String.IsNullOrEmpty(dcICR700.YOBI) Then Return False

            Else
                '【RW②】

                '店番
                If String.IsNullOrEmpty(dcICR700.SHOPNO) Then Return False
                'カード区分
                If String.IsNullOrEmpty(dcICR700.CARDKBN) Then Return False
                'カード番号
                If String.IsNullOrEmpty(dcICR700.CARDNO) Then Return False
                '顧客番号
                If String.IsNullOrEmpty(dcICR700.NCSNO) Then Return False
                'スクール生番号
                If String.IsNullOrEmpty(dcICR700.SCLMANNO) Then Return False
                '顧客種別
                If String.IsNullOrEmpty(dcICR700.NKBNO) Then Return False
                '会員期限
                If String.IsNullOrEmpty(dcICR700.DMEMBER) Then Return False
                'パスワード
                If String.IsNullOrEmpty(dcICR700.PASSCD) Then Return False
                '残金額
                If String.IsNullOrEmpty(dcICR700.ZANKN) Then Return False
                'P残金額
                If String.IsNullOrEmpty(dcICR700.PREZANKN) Then Return False
                '残ポイント
                If String.IsNullOrEmpty(dcICR700.POINT) Then Return False
                '前回来場日
                If String.IsNullOrEmpty(dcICR700.ZENENTDATE) Then Return False
                '入場区分
                If String.IsNullOrEmpty(dcICR700.ENTKBN) Then Return False
                'ボール単価
                If String.IsNullOrEmpty(dcICR700.BALLKIN) Then Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region





End Class
