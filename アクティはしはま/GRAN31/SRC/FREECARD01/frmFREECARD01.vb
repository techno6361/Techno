Imports TECHNO.DataBase

Public Class frmFREECARD01

#Region "▼宣言部"

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "貸出カード作成"

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

            MyBase.l_Title_FormName = "貸出カード作成"

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

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFREECARD01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()


            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'ﾌﾘｰｶｰﾄﾞ番号取得
            If Not GetFREENOSEQ() Then
                Using frm As New frmMSGBOX01("ﾌﾘｰｶｰﾄﾞ番号の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '本日作成履歴取得
            GetLESSCTRN()

            Me.cmbKBMAST.SelectedIndex = 1

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
    Private Sub frmFREECARD01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try

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
        Try

            Me.btnCard.Enabled = False

            'ﾌﾘｰｶｰﾄﾞ番号再取得
            If Not GetFREENOSEQ() Then
                Using frm As New frmMSGBOX01("ﾌﾘｰｶｰﾄﾞ番号の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '種別
            Dim strSYUBETU As String = (Me.cmbKBMAST.SelectedIndex + 1).ToString
            If strSYUBETU.Equals("10") Then
                strSYUBETU = "A"
            End If


            '【プリカRW書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = "00"
            'シリアルナンバー
            dcICR700.SERIALNO_WR = "000"
            '種別
            dcICR700.SYUBETU_WR = strSYUBETU
            '金額
            dcICR700.KINGAKU_WR = "00000"
            '予備
            dcICR700.YOBI_WR = "0"

            '【V31RW書き込み情報セット】
            '店番
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'カード区分
            dcICR700.CARDKBN_WR = "1"
            'カード番号
            dcICR700.CARDNO_WR = Me.lblFREENOSEQ.Text
            '顧客番号
            dcICR700.NCSNO_WR = Me.lblFREENOSEQ.Text
            'スクール生番号
            dcICR700.SCLMANNO_WR = "000000"
            '顧客種別
            dcICR700.NKBNO_WR = strSYUBETU
            '会員期限
            dcICR700.DMEMBER_WR = "00000000"
            'パスワード
            dcICR700.PASSCD_WR = "00"
            '残金額
            dcICR700.ZANKN_WR = "00000"
            'P残金額
            dcICR700.PREZANKN_WR = "00000"
            '残ポイント
            dcICR700.POINT_WR = "00000"
            '前回来場日
            dcICR700.ZENENTDATE_WR = "00000000"
            '入場区分
            dcICR700.ENTKBN_WR = "0"
            'ボール単価
            dcICR700.BALLKIN_WR = "00"
            '打席番号
            dcICR700.SEATNO_WR = "FFF"

            'カード番号カウントアップ
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE SEQTRN SET FREENOSEQ = FREENOSEQ + 1")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("ﾌﾘｰｶｰﾄﾞ番号の更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '無記名ｶｰﾄﾞ作成ﾄﾗﾝ登録
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO LESSCTRN VALUES("
            strSQL &= CType(Me.lblFREENOSEQ.Text, Integer) & ","
            strSQL &= CType(Me.lblFREENOSEQ.Text, Integer) & ","
            strSQL &= Me.cmbKBMAST.SelectedIndex + 1 & ","
            strSQL &= "'" & Me.cmbKBMAST.Text & "',"
            strSQL &= "0,"
            strSQL &= "0,"
            strSQL &= "0,"
            strSQL &= "1,"
            strSQL &= "NULL,"
            strSQL &= "NULL,"
            strSQL &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("ﾌﾘｰｶｰﾄﾞの作成に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
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

            'ﾌﾘｰｶｰﾄﾞ番号取得
            If Not GetFREENOSEQ() Then
                Using frm As New frmMSGBOX01("ﾌﾘｰｶｰﾄﾞ番号の取得に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '本日作成履歴取得
            GetLESSCTRN()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.btnCard.Enabled = True
            Me.Close()
        End Try
    End Sub

    ''' <summary>
    ''' 残金クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        Dim blnERRSHOPFLG As Boolean = False
        Try
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                'カードリード処理
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                'キャンセル押下
                If frm.CANCEL Then Exit Sub
                'エラー発生
                If frm.ERRFLG Then Exit Sub
                '店番チェック
                blnERRSHOPFLG = frm.ERRSHOPFLG
            End Using
            '店番エラー発生
            If blnERRSHOPFLG Then
                Using frm As New frmMSGBOX01("店番が一致しません。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            If Not dcICR700.NCSNO >= "50000000" Then
                Using frm As New frmMSGBOX01("貸出カードではありません。", 3)
                    frm.ShowDialog()
                    Exit Sub
                End Using
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
            dcICR700.KINGAKU_WR = "00000"
            '予備
            dcICR700.YOBI_WR = dcICR700.YOBI

            '【V31RW書き込み情報セット】
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
            '前回来場日
            dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
            '残金額
            dcICR700.ZANKN_WR = "00000"
            'P残金額
            dcICR700.PREZANKN_WR = "00000"
            '残ポイント
            dcICR700.POINT_WR = "00000"
            '入場区分 
            dcICR700.ENTKBN_WR = "0"
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

            'ﾌﾘｰｶｰﾄﾞ番号
            Me.lblFREENOSEQ.Text = String.Empty

            '本日作成枚数
            Me.lblZERO.Text = "0"
            Me.lbl1000.Text = "0"
            Me.lbl2000.Text = "0"
            Me.lbl3000.Text = "0"
            Me.lbl5000.Text = "0"
            Me.lbl10000.Text = "0"
            Me.lbl20000.Text = "0"
            Me.lbl20001.Text = "0"

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

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.cmbKBMAST.DataSource = resultDt
            Me.cmbKBMAST.ValueMember = "NKBNO"
            Me.cmbKBMAST.DisplayMember = "CKBNAME"
            Me.cmbKBMAST.SelectedValue = 1

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' ﾌﾘｰｶｰﾄﾞ番号取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetFREENOSEQ() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" (FREENOSEQ + 1) AS FREENO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.lblFREENOSEQ.Text = resultDt.Rows(0).Item("FREENO").ToString

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 本日作成枚数情報習得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetLESSCTRN() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("(")
            sql.Append("SELECT 0 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN = 0 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 1000 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 1 AND ZANKN <= 1000 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 2000 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 1001 AND ZANKN <= 2000 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 3000 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 2001 AND ZANKN <= 3000 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 5000 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 3001 AND ZANKN <= 5000 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 10000 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 5001 AND ZANKN <= 10000 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 20000 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 10001 AND ZANKN <= 20000 AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" UNION ")
            sql.Append("SELECT 20001 AS ZANKN,COUNT(*) AS CNT FROM LESSCTRN WHERE ZANKN >= 20001  AND CARDKBN = 1 AND TO_CHAR(INSDTM,'YYYYMMDD') = '" & Now.ToString("yyyyMMdd") & "'")

            sql.Append(") ORDER BY ZANKN ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Select Case resultDt.Rows(i).Item("ZANKN").ToString
                    Case "0"
                        Me.lblZERO.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "1000"
                        Me.lbl1000.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "2000"
                        Me.lbl2000.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "3000"
                        Me.lbl3000.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "5000"
                        Me.lbl5000.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "10000"
                        Me.lbl10000.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "20000"
                        Me.lbl20000.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                    Case "20001"
                        Me.lbl20001.Text = CType(resultDt.Rows(i).Item("CNT"), Integer).ToString("#,##0")
                End Select
            Next

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

#End Region





End Class
