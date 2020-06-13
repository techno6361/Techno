Imports TECHNO.DataBase

Public Class frmCSMAST01

#Region "▼宣言部"

    ''' <summary>
    ''' 顧客種別情報
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtKBMAST As DataTable
    ''' <summary>
    ''' 住所情報
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtZIPMTA As DataTable
    ''' <summary>
    ''' 新規・更新区別【0】新規【1】更新
    ''' </summary>
    ''' <remarks></remarks>
    Private _intRegisterKbn As Integer
    ''' <summary>
    ''' 画面表示時の最新の更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtUPDDTM As DateTime
    ''' <summary>
    ''' 紐付削除スクール生番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _strDelSCLMANNO As String
    ''' <summary>
    ''' スクール生状態保持(年会費処理用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSCLKBN As String
    ''' <summary>
    ''' 画面呼び出し時顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _intNCSNO As Integer = 0
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

    ''' <summary>
    ''' 画面表示した時点での顧客種別
    ''' </summary>
    ''' <remarks></remarks>
    Private _intDbNCSRANK As Integer = 0
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客情報登録"

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

            MyBase.l_Title_FormName = "顧客情報登録"

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
                   ByVal NCSNO As Integer)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客情報登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700

            _intNCSNO = NCSNO
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
    Private Sub frmCSMAST01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            '画面初期設定
            Init()

            '住所情報取得
            If Not GetZIPMTA() Then
                Using frm As New frmMSGBOX01("住所情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '顧客検索画面から呼び出された場合
            If Not _intNCSNO.Equals(0) Then
                Me.txtNCSNO.Text = _intNCSNO.ToString.PadLeft(8, "0"c)
                If Not GetCSMAST() Then
                    Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Init()
                    Me.txtNCSNO.Focus()
                    Exit Sub
                End If

                Me.pnlRegisterInfo.Enabled = True
                Me.chkCDSTOP.Enabled = True
                Me.txtCCSNAME.Focus()
                '更新
                _intRegisterKbn = 1

                '削除
                Me.tspFunc9.Enabled = True
                '会員期限更新ボタン
                Me.btnUpdDMEMBER.Enabled = True
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCSMAST01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            '住所情報
            If _dtZIPMTA IsNot Nothing Then _dtZIPMTA.Dispose()

            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                '【カード排出】※カード排出ユニット有りの時のみ
                If UIUtility.SYSTEM.RWUnitKB = 1 Then
                    Me.btnCard.PerformClick()
                End If
                Me.Close()
            End If

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
    Private Sub btnColorKbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClrKbn1.Click, btnClrKbn2.Click, btnClrKbn3.Click, btnClrKbn4.Click, btnClrKbn5.Click
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
                If String.IsNullOrEmpty(Me.txtNCSNO.Text) Then frm.chkAll.Checked = True
                frm.ShowDialog()
                strSelectColor = frm.SelectColor
                blnSelectAll = frm.SelectAll
            End Using

            If String.IsNullOrEmpty(strSelectColor) Then Exit Sub
            If String.IsNullOrEmpty(Me.txtNCSNO.Text) And Not blnSelectAll Then Exit Sub

            btn.Text = strSelectColor
            btn.ForeColor = Color.FromName(strSelectColor)
            btn.BackColor = Color.FromName(strSelectColor)

            iDatabase.BeginTransaction()

            Try
                If Not String.IsNullOrEmpty(Me.txtNCSNO.Text) And _intRegisterKbn.Equals(1) Then
                    If Not (iDatabase.ExecuteUpdate("DELETE FROM CLRSMA WHERE MANNO = '" & Me.txtNCSNO.Text & "'")) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01(strMSG, 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If

                    strSQL01 = "INSERT INTO CLRSMA ("
                    strSQL02 = "VALUES ("

                    strSQL01 &= "MANNO,"
                    strSQL02 &= "'" & Me.txtNCSNO.Text & "',"
                    strSQL01 &= "SCLMANNO,"
                    strSQL02 &= UIFunction.NullCheck(Me.txtDSCLMANNO.Text) & ","

                    strSQL01 &= "CLRKBN1,"
                    strSQL02 &= "'" & Me.btnClrKbn1.Text & "',"
                    strSQL01 &= "CLRKBN2,"
                    strSQL02 &= "'" & Me.btnClrKbn2.Text & "',"
                    strSQL01 &= "CLRKBN3,"
                    strSQL02 &= "'" & Me.btnClrKbn3.Text & "',"
                    strSQL01 &= "CLRKBN4,"
                    strSQL02 &= "'" & Me.btnClrKbn4.Text & "',"
                    strSQL01 &= "CLRKBN5,"
                    strSQL02 &= "'" & Me.btnClrKbn5.Text & "',"

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
    ''' カード取引履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCARDTORIIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCARDTORIHIST01.Click
        Dim intDELKBN As Integer = 0
        Try
            Using frm As New frmCARDTORIHIST01(iDatabase)
                frm.MANNO = Me.txtNCSNO.Text
                frm.CCSNAME = Me.txtCCSNAME.Text
                frm.ZANKN = CType(Me.txtZANKN.Text, Integer)
                frm.PREZANKN = CType(Me.txtPREZANKN.Text, Integer)
                frm.SRTPO = CType(Me.txtSRTPO.Text, Integer)
                frm.ShowDialog()
                intDELKBN = frm.DELKBN
            End Using

            If intDELKBN.Equals(1) Then
                If Not GetCSMAST() Then
                    Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
                Using frm As New frmMSGBOX01("カードを新規発行してください。", 0)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 年会費履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFEEHIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFEEHIST01.Click
        Dim blnCardFlg As Boolean = False
        Try
            If CType(Me.btnCard.Tag, Integer).Equals(1) Then blnCardFlg = True

            Using frm As New frmFEEHIST01(iDatabase, dcICR700, blnCardFlg)
                frm.MANNO = Me.txtNCSNO.Text
                frm.ShowDialog()
                blnCardFlg = frm.CARDFLG
            End Using

            If String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                Exit Sub
            End If
            If Not GetCSMAST() Then
                Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                    frm.ShowDialog()
                End Using
                Init()
                Me.txtNCSNO.Focus()
                Exit Sub
            End If

            Me.pnlRegisterInfo.Enabled = True
            Me.chkCDSTOP.Enabled = True
            Me.txtCCSNAME.Focus()
            '更新
            _intRegisterKbn = 1

            '削除
            Me.tspFunc9.Enabled = True
            '会員期限更新ボタン
            Me.btnUpdDMEMBER.Enabled = True

            If Not GetCSMAST() Then
                Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                    frm.ShowDialog()
                End Using
                Init()
                Me.txtNCSNO.Focus()
                Exit Sub
            End If

            Me.pnlRegisterInfo.Enabled = True
            Me.chkCDSTOP.Enabled = True
            Me.txtCCSNAME.Focus()
            '更新
            _intRegisterKbn = 1

            '削除
            Me.tspFunc9.Enabled = True
            '会員期限更新ボタン
            Me.btnUpdDMEMBER.Enabled = True

            If Not blnCardFlg Then
                'カード挿入ボタン
                Me.btnCard.Text = "カード読込"
                Me.btnCard.BackColor = Color.DarkOliveGreen
                Me.btnCard.Tag = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 住所ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnADDRESS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDRESS.Click
        Try
            If Me.txtNZIP.Text.Trim.Replace("-", String.Empty).Length.Equals(7) And Me.txtCADDRESS1.Text.Trim.Length.Equals(0) And Me.txtCADDRESS2.Text.Trim.Length.Equals(0) Then
                Dim drADDRESS() As DataRow = _dtZIPMTA.Select("ZIPCD='" & Me.txtNZIP.Text.Trim.Replace("-", String.Empty) & "'")
                If Not drADDRESS.Length.Equals(0) Then
                    Me.txtCADDRESS1.Text = drADDRESS(0).Item("ADDRESS1").ToString & drADDRESS(0).Item("ADDRESS2").ToString
                    Me.txtCADDRESS2.Text = drADDRESS(0).Item("ADDRESS3").ToString
                End If
            End If
            Me.txtCTELEPHONE.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 〒ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnZIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZIP.Click
        Try
            Dim strADDRESS As String = Me.txtCADDRESS1.Text.Trim & Me.txtCADDRESS2.Text.Trim

            If Not strADDRESS.Length.Equals(0) And Me.txtNZIP.Text.Trim.Replace("-", "").Length.Equals(0) Then
                Dim drADDRESS() As DataRow = _dtZIPMTA.Select("(ADDRESS1 + ADDRESS2 + ADDRESS3) LIKE '%" & strADDRESS & "%'")
                If Not drADDRESS.Length.Equals(0) Then
                    Me.txtNZIP.Text = drADDRESS(0).Item("ZIPCD").ToString
                    Me.txtNZIP.Focus()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 期限更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdDMEMBER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdDMEMBER.Click
        Dim strMsg As String = String.Empty
        Try
            If String.IsNullOrEmpty(Me.txtDMEMBER.Text) Then
                Exit Sub
            End If

            If Not Me.txtDMEMBER.Text.Replace("/", String.Empty).Length.Equals(8) Then
                Exit Sub
            End If
            Dim dtDMEMBER3 As DateTime
            If Not DateTime.TryParse(Me.txtDMEMBER.Text, dtDMEMBER3) Then
                Me.txtDMEMBER.Focus()
                Using frm As New frmMSGBOX01("日付として認識できません。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Dim dtDMEMBER As DateTime = DateTime.Parse(Me.txtDMEMBER.Text)
            Dim strUpdDMEMBER As String = String.Empty
            strUpdDMEMBER = dtDMEMBER.AddMonths(CType(Me.btnUpdDMEMBER.Tag, Integer)).ToString("yyyy/MM/dd")

            '会員期限調整日数反映
            Dim dtDMEMBER2 As DateTime = DateTime.Parse(strUpdDMEMBER)
            strUpdDMEMBER = dtDMEMBER2.AddDays(UIUtility.SYSTEM.CALLMT).ToString("yyyy/MM/dd")

            If Me.txtDMEMBER.ForeColor = Color.Red Then
                '期限切れ
                strUpdDMEMBER = Now.AddMonths(CType(Me.btnUpdDMEMBER.Tag, Integer)).ToString("yyyy/MM/dd")
            End If

            strMsg = "会員期限を" & strUpdDMEMBER & "で更新しますか？"
            If Not _intDbNCSRANK.Equals(Me.cmbNCSRANK.SelectedIndex) Then
                strMsg &= vbCrLf & "※顧客種別を【" & Me.cmbNCSRANK.Text & "】に変更します※"
            End If
            Using frm As New frmMSGBOX01(strMsg, 1)
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
            strMsg = String.Empty
            Dim intCKBFEE As Integer = 0
            Dim dr As DataRow()
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & Me.cmbNCSRANK.SelectedIndex)
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

            'If intCKBFEE.Equals(0) Then
            '    iDatabase.RollBack()
            '    Exit Sub
            'End If
            Dim intNCSRANK As Integer = 0
            If Me.cmbNCSRANK.SelectedIndex > 0 Then
                intNCSRANK = Me.cmbNCSRANK.SelectedIndex
            End If
            If Not UIFunction.UpdDMEMBER(Me.txtNCSNO.Text, strUpdDMEMBER, iDatabase, intNCSRANK) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("会員期限の更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If intPaySelect.Equals(2) Then
                '【打席カード】     

                '受付たｶｰﾄﾞが同一か確認
                If Not CheckCard() Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("最初に受付けたカードと異なります。", 3)
                        frm.ShowDialog()
                    End Using
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
                Me.btnCard.PerformClick()
            End If

            'Me.txtDMEMBER.Text = strUpdDMEMBER

            iDatabase.Commit()

            If Not String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                If Not GetCSMAST() Then
                    Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' カード挿入ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard.Click
        Dim blnERRSHOPFLG As Boolean = False    '店番エラーが発生したかどうか
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


            If Not GetCSMAST(True) Then
                Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                    frm.ShowDialog()
                End Using
                Using frm As New frmREQUESTCARD(dcICR700)
                    'カード排出処理
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                End Using
                Init()
                Me.txtNCSNO.Focus()
                Exit Sub
            End If

            '更新
            _intRegisterKbn = 1
            '削除
            Me.tspFunc9.Enabled = True


            'カード発行ボタン
            Me.btnISSUE.Enabled = False

            Me.btnCard.Tag = 1

            Me.pnlRegisterInfo.Enabled = True
            Me.chkCDSTOP.Enabled = True
            Me.txtCCSNAME.Focus()

            Me.pnlCsInfo.Visible = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード発行ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnISSUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnISSUE.Click
        Dim strMsg As String = String.Empty
        Dim strBfrNCARDID As String = String.Empty
        Dim blnReCardFlg As Boolean = False             '再発行かどうか
        Try
            '更新前カード番号セット
            strBfrNCARDID = Me.txtNCARDID.Text
            'カード発行
            Dim blnCARDHAKKO As Boolean = False
            Dim blnUpdKINSMA As Boolean = False
            Dim num As Integer
            If Not Integer.TryParse(dcICR700.NCSNO, num) Then
                num = 0
            End If
            If num >= 50000000 And (CType(Me.btnCard.Tag, Integer).Equals(1)) Then
                '【フリーカード】
                blnCARDHAKKO = True
                blnUpdKINSMA = True
            Else
                If String.IsNullOrEmpty(Me.txtNCARDID.Text) Or Me.txtNCARDID.Text.Equals("00000000") Then
                    strMsg = "カードを発行しますか？"
                Else
                    strMsg = "カードは発行済みです。" & vbCr & "再発行しますか？"
                    blnReCardFlg = True
                End If

                Using frm As New frmMSGBOX01(strMsg, 1)
                    frm.ShowDialog()
                    If frm.Reply Then
                        blnCARDHAKKO = True
                    End If
                End Using
            End If

            If blnReCardFlg And blnCARDHAKKO Then
                '*** 【スタッフ確認】 ***'
                _strSTFCODE = String.Empty
                _strSTFNAME = String.Empty
                If UIUtility.SYSTEM.TANCHKFLG.Equals(1) And UIFunction.ChkPgScrty("RECARD", iDatabase) Then
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
            End If


            If blnCARDHAKKO Then
                GetNewCARDID()

                Dim strNKBNO As String = String.Empty
                If Me.cmbNCSRANK.SelectedIndex >= 10 Then
                    Select Case Me.cmbNCSRANK.SelectedIndex
                        Case 10
                            strNKBNO = "A"
                        Case 11
                            strNKBNO = "B"
                        Case 12
                            strNKBNO = "C"
                        Case 13
                            strNKBNO = "D"
                    End Select
                Else
                    strNKBNO = Me.cmbNCSRANK.SelectedIndex.ToString
                End If

                '【プリカRW書き込み情報セット】
                '店番号
                dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
                'パスワード
                dcICR700.PASSCD_WR = "00"
                'シリアルナンバー
                dcICR700.SERIALNO_WR = "000"
                '種別
                dcICR700.SYUBETU_WR = strNKBNO
                '金額
                dcICR700.KINGAKU_WR = CType(Me.txtSUMZANKN.Text, Integer).ToString.PadLeft(5, "0"c)
                '予備
                dcICR700.YOBI_WR = "0"

                '【V31RW書き込み情報セット】
                '店番
                dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
                'カード区分
                dcICR700.CARDKBN_WR = "1"
                'カード番号
                dcICR700.CARDNO_WR = Me.txtNCARDID.Text
                '顧客番号
                dcICR700.NCSNO_WR = Me.txtNCARDID.Text
                'スクール生番号
                If String.IsNullOrEmpty(Me.txtDSCLMANNO.Text) Then
                    dcICR700.SCLMANNO_WR = "000000"
                Else
                    dcICR700.SCLMANNO_WR = Me.txtDSCLMANNO.Text
                End If
                '顧客種別
                dcICR700.NKBNO_WR = strNKBNO
                '会員期限
                If String.IsNullOrEmpty(Me.txtDMEMBER.Text) Then
                    dcICR700.DMEMBER_WR = "00000000"
                Else
                    dcICR700.DMEMBER_WR = Me.txtDMEMBER.Text.Replace("/", String.Empty)
                End If
                'パスワード
                dcICR700.PASSCD_WR = "00"
                '前回来場日
                If String.IsNullOrEmpty(Me.txtENTDT.Text) Or Me.txtENTDT.Text.Replace("/", String.Empty).Equals("00000000") Then
                    dcICR700.ZENENTDATE_WR = "00000000"
                Else
                    dcICR700.ZENENTDATE_WR = Me.txtENTDT.Text.Replace("/", String.Empty)
                End If
                '残金額
                If String.IsNullOrEmpty(Me.txtZANKN.Text) Then
                    dcICR700.ZANKN_WR = "00000"
                Else
                    dcICR700.ZANKN_WR = CType(Me.txtZANKN.Text, Integer).ToString.PadLeft(5, "0"c)
                End If
                'P残金額
                If String.IsNullOrEmpty(Me.txtPREZANKN.Text) Then
                    dcICR700.PREZANKN_WR = "00000"
                Else
                    dcICR700.PREZANKN_WR = CType(Me.txtPREZANKN.Text, Integer).ToString.PadLeft(5, "0"c)
                End If
                '残ポイント
                If String.IsNullOrEmpty(Me.txtSRTPO.Text) Then
                    dcICR700.POINT_WR = "00000"
                Else
                    dcICR700.POINT_WR = CType(Me.txtSRTPO.Text, Integer).ToString.PadLeft(5, "0"c)
                End If
                '入場区分 入金機でカード発行料の精算が必要な場合１
                dcICR700.ENTKBN_WR = "0"
                If Not blnReCardFlg Then
                    If Me.chkHakko.Checked Then
                        dcICR700.ENTKBN_WR = "1"
                    End If
                End If
                'ボール単価
                dcICR700.BALLKIN_WR = "00"
                '打席番号
                dcICR700.SEATNO_WR = "FFF"

                Using frm As New frmREQUESTCARD(dcICR700)
                    'カード初期化処理
                    frm.COMMAND = frmREQUESTCARD.Command_Type.CARDINIT
                    frm.ShowDialog()
                    'キャンセル押下
                    If frm.CANCEL Then
                        Me.txtNCARDID.Text = strBfrNCARDID
                        Exit Sub
                    End If
                End Using

                '更新登録
                If Not UpdRegister(True, blnUpdKINSMA) Then
                    Using frm As New frmMSGBOX01("顧客情報の更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                If blnReCardFlg Then
                    '再発行
                    Dim sql As New System.Text.StringBuilder

                    Try
                        iDatabase.BeginTransaction()

                        sql.Clear()
                        sql.Append("INSERT INTO RECARDTRN VALUES(")
                        sql.Append("'" & Now.ToString("yyyyMMdd") & "',")
                        sql.Append(CType(Me.txtNCARDID.Text, Integer) & ",")
                        sql.Append("'" & Me.txtNCSNO.Text & "',")
                        sql.Append(CType(strBfrNCARDID, Integer) & ",")
                        sql.Append(UIFunction.NullCheck(_strSTFCODE) & ",")
                        sql.Append(UIFunction.NullCheck(_strSTFNAME) & ",")
                        sql.Append(CType(Me.txtZANKN.Text, Integer) & ",")
                        sql.Append(CType(Me.txtZANKN.Text, Integer) & ",")
                        sql.Append(CType(Me.txtPREZANKN.Text, Integer) & ",")
                        sql.Append(CType(Me.txtPREZANKN.Text, Integer) & ",")
                        sql.Append(CType(Me.txtSRTPO.Text, Integer) & ",")
                        sql.Append(CType(Me.txtSRTPO.Text, Integer) & ",")
                        sql.Append("NOW())")

                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Using frm As New frmMSGBOX01("カード再発行履歴の更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                        Else
                            iDatabase.Commit()
                        End If

                    Catch ex As Exception
                        iDatabase.RollBack()
                        MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Else
                    '新規発行
                    Dim sql As New System.Text.StringBuilder

                    Try
                        iDatabase.BeginTransaction()

                        sql.Clear()
                        sql.Append("INSERT INTO NEWCARDTRN VALUES(")
                        sql.Append("'" & Now.ToString("yyyyMMdd") & "',")
                        sql.Append(CType(Me.txtNCARDID.Text, Integer) & ",")
                        sql.Append("'" & Me.txtNCSNO.Text & "',")
                        sql.Append(UIFunction.NullCheck(_strSTFCODE) & ",")
                        sql.Append(UIFunction.NullCheck(_strSTFNAME) & ",")
                        sql.Append("NOW(),")
                        sql.Append("NOW())")

                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Using frm As New frmMSGBOX01("カード再発行履歴の更新に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                        Else
                            iDatabase.Commit()
                        End If

                    Catch ex As Exception
                        iDatabase.RollBack()
                        MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If

            '画面初期設定
            Init()

            Me.txtNCSNO.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 発行料精算クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHakkoClear_Click(sender As System.Object, e As System.EventArgs) Handles btnHakkoClear.Click
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
            dcICR700.KINGAKU_WR = dcICR700.KINGAKU
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
            dcICR700.ZANKN_WR = dcICR700.ZANKN
            'P残金額
            dcICR700.PREZANKN_WR = dcICR700.PREZANKN
            '残ポイント
            dcICR700.POINT_WR = CType(Me.txtSRTPO.Text, Integer).ToString.PadLeft(5, "0"c)
            '入場区分 入金機でカード発行料の精算が必要な場合１
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

            Me.btnHakkoClear.Enabled = False

            Me.chkHakko.Checked = False


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード顧客種別書き換えボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCardNCSRANK_Click(sender As System.Object, e As System.EventArgs) Handles btnCardNCSRANK.Click
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

            If (Not Me.txtNCARDID.Text.Equals(dcICR700.NCSNO)) Or dcICR700.NCSNO.Equals("00000000") Then
                Using frm As New frmMSGBOX01("カード情報が一致しません。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            Dim strNKBNO As String = String.Empty
            If Me.cmbNCSRANK.SelectedIndex >= 10 Then
                Select Case Me.cmbNCSRANK.SelectedIndex
                    Case 10
                        strNKBNO = "A"
                    Case 11
                        strNKBNO = "B"
                    Case 12
                        strNKBNO = "C"
                    Case 13
                        strNKBNO = "D"
                End Select
            Else
                strNKBNO = Me.cmbNCSRANK.SelectedIndex.ToString
            End If

            '【プリカRW書き込み情報セット】
            '店番号
            dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = dcICR700.PASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = dcICR700.SERIALNO
            '種別
            dcICR700.SYUBETU_WR = strNKBNO
            '金額
            dcICR700.KINGAKU_WR = dcICR700.KINGAKU
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
            dcICR700.NKBNO_WR = strNKBNO
            '会員期限
            dcICR700.DMEMBER_WR = dcICR700.DMEMBER
            'パスワード
            dcICR700.PASSCD_WR = dcICR700.PASSCD
            '前回来場日
            dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
            '残金額
            dcICR700.ZANKN_WR = dcICR700.ZANKN
            'P残金額
            dcICR700.PREZANKN_WR = dcICR700.PREZANKN
            '残ポイント
            dcICR700.POINT_WR = dcICR700.POINT
            '入場区分 入金機でカード発行料の精算が必要な場合１
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

            Me.btnHakkoClear.Enabled = False

            Me.chkHakko.Checked = False


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 個人情報表示ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCsOpen_Click(sender As System.Object, e As System.EventArgs) Handles btnCsOpen.Click
        Dim blnSEFLG As Boolean = False
        Try
            Using frm As New frmPWDISP01
                frm.PWKBN = 2
                frm.ShowDialog()
                If Not frm.CLEAR Then Exit Sub
                blnSEFLG = frm.SEFLG
            End Using

            Me.pnlCsInfo.Visible = True

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
            If Me.cmbNCSRANK.SelectedIndex.Equals(0) Then
                Me.txtDMEMBER.Text = String.Empty
                Exit Sub
            End If

            Dim dr As DataRow()
            dr = _dtKBMAST.Select("NKBNO = " & Me.cmbNCSRANK.SelectedIndex)

            Dim intCKBLIMIT As Integer = CType(dr(0).Item("CKBLIMIT").ToString, Integer)

            Me.btnUpdDMEMBER.Tag = intCKBLIMIT
            If intCKBLIMIT.Equals(0) Then
                Me.txtDMEMBER.Text = String.Empty
                Exit Sub
            End If
            If Not String.IsNullOrEmpty(Me.txtDMEMBER.Text) Then Exit Sub

            Me.txtDMEMBER.Text = Now.ToString("yyyy/MM/dd")
            'Dim dtToday As DateTime = DateTime.Parse(Now.Year.ToString & "/" & Now.Month.ToString.PadLeft(2, "0"c) & "/" & Now.Day.ToString.PadLeft(2, "0"c))

            'Me.txtDMEMBER.Text = dtToday.AddMonths(intCKBLIMIT).ToString("yyyy/MM/dd")

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
    ''' スクール生番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDSCLMANNO_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDSCLMANNO.Validated
        Try
            If String.IsNullOrEmpty(Me.txtDSCLMANNO.Text) Then
                Me.txtSCLKBN.Text = String.Empty
                Exit Sub
            End If

            Me.txtDSCLMANNO.Text = Me.txtDSCLMANNO.Text.PadLeft(6, "0"c)

            'スクール生番号重複チェック
            If Not ChkSclManInfo() Then
                Using frm As New frmMSGBOX01("既に登録されているスクール生番号です。", 2)
                    frm.ShowDialog()
                End Using
                Me.txtDSCLMANNO.Focus()
                Exit Sub
            End If
            'スクール生情報取得
            If Not SetSclManInfo() Then
                Using frm As New frmMSGBOX01("スクール生情報が見つかりません。", 2)
                    frm.ShowDialog()
                End Using
                Me.txtDSCLMANNO.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 協力金番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKRKNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtKRKNO.Validated
        Try
            If String.IsNullOrEmpty(Me.txtKRKNO.Text) Then Exit Sub

            Me.txtKRKNO.Text = Me.txtKRKNO.Text.PadLeft(4, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 一覧ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMANSEARCH01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMANSEARCH01.Click
        Dim strSCLMANNO As String = String.Empty
        Try
            Using frm As New frmMANSEARCH01(iDatabase)
                frm.ShowDialog()
                strSCLMANNO = frm.GetSCLMANNO
            End Using

            If String.IsNullOrEmpty(strSCLMANNO) Then Exit Sub

            Me.txtDSCLMANNO.Text = strSCLMANNO

            'スクール生番号重複チェック
            If Not ChkSclManInfo() Then
                Using frm As New frmMSGBOX01("既に登録されているスクール生番号です。", 2)
                    frm.ShowDialog()
                End Using
                Me.txtDSCLMANNO.Focus()
                Exit Sub
            End If
            'スクール生情報取得
            If Not SetSclManInfo() Then
                Using frm As New frmMSGBOX01("スクール生情報が見つかりません。", 2)
                    frm.ShowDialog()
                End Using
                Me.txtDSCLMANNO.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 顧客番号テキストボックス_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNCSNO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNCSNO.KeyDown
        Dim strNCSNO As String = String.Empty
        Try
            Me.pnlCsInfo.Visible = False

            If e.KeyCode = Keys.Enter Then
                If String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                    '【新規登録】

                    '画面初期設定
                    Init()
                    Me.pnlRegisterInfo.Enabled = True
                    Me.chkCDSTOP.Enabled = True

                    '新規登録
                    _intRegisterKbn = 0
                    '新規顧客番号
                    GetNewNCSNO()

                    Me.txtNCSNO.ReadOnly = True
                    Me.txtCCSNAME.Focus()

                    '会員期限更新ボタン
                    Me.btnUpdDMEMBER.Enabled = False
                    'カード停止チェックボックス
                    Me.chkCDSTOP.Enabled = False

                    Me.chkHakko.Checked = True
                    Me.chkHakko.Enabled = True

                    '初期表示ビジター
                    Me.cmbNCSRANK.SelectedIndex = 2

                    Me.pnlCsInfo.Visible = True
                Else
                    '【更新】

                    If Not GetCSMAST() Then
                        If Me.txtNCSNO.Text.Equals("00000000") Or Me.txtNCSNO.Text >= "40000000" Then
                            Using frm As New frmMSGBOX01("登録できない顧客番号です。", 2)
                                frm.ShowDialog()
                            End Using
                            Init()
                            Me.txtNCSNO.Focus()
                            Exit Sub
                        End If
                        If ChkNewNCSNO() < CType(Me.txtNCSNO.Text, Integer) Then
                            Using frm As New frmMSGBOX01("登録できない顧客番号です。", 2)
                                frm.ShowDialog()
                            End Using
                            Init()
                            Me.txtNCSNO.Focus()
                            Exit Sub
                        End If
                        Using frm As New frmMSGBOX01("顧客データがありません。" & vbCr & "顧客番号【" & Me.txtNCSNO.Text & "】で新規登録しますか？", 1)
                            frm.ShowDialog()
                            If Not frm.Reply Then
                                Init()
                                Me.txtNCSNO.Focus()
                                Exit Sub
                            End If
                        End Using

                        strNCSNO = Me.txtNCSNO.Text
                        '画面初期設定
                        Init()
                        Me.pnlRegisterInfo.Enabled = True
                        Me.chkCDSTOP.Enabled = True

                        '新規登録
                        _intRegisterKbn = 0

                        Me.txtNCSNO.Text = strNCSNO

                        Me.txtNCSNO.ReadOnly = True
                        Me.txtCCSNAME.Focus()

                        '会員期限更新ボタン
                        Me.btnUpdDMEMBER.Enabled = False

                        Me.chkHakko.Checked = True
                        Me.chkHakko.Enabled = True

                        Me.pnlCsInfo.Visible = True

                        Exit Sub
                    End If
                    If Me.txtNCARDID.Text.Equals("00000000") Or String.IsNullOrEmpty(Me.txtNCARDID.Text) Then
                        Me.chkHakko.Checked = True
                        Me.chkHakko.Enabled = True
                    Else
                        Me.chkHakko.Checked = False
                        Me.chkHakko.Enabled = False
                    End If


                    Me.pnlRegisterInfo.Enabled = True
                    Me.txtCCSNAME.Focus()
                    '更新
                    _intRegisterKbn = 1

                    '削除
                    Me.tspFunc9.Enabled = True
                    '会員期限更新ボタン
                    Me.btnUpdDMEMBER.Enabled = True
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 誕生日テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDBIRTH_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDBIRTH.Validated
        Try
            Me.lblAge.Text = String.Empty
            If String.IsNullOrEmpty(Me.txtDBIRTH.Text) Then Exit Sub

            Dim strDBIRTH As String = Me.txtDBIRTH.Text.Replace("/", String.Empty).PadLeft(8, "0"c)

            Me.txtDBIRTH.Text = strDBIRTH.Substring(0, 4) & "/" & strDBIRTH.Substring(4, 2) & "/" & strDBIRTH.Substring(6, 2)

            Me.lblAge.Text = UIFunction.GetAge(DateTime.Parse(Me.txtDBIRTH.Text), Now).ToString & "歳"


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 郵便番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNZIP_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNZIP.Validated
        Try
            If String.IsNullOrEmpty(Me.txtNZIP.Text) Then Exit Sub

            Dim strNZIP As String = Me.txtNZIP.Text.PadLeft(7, "0"c)

            Me.txtNZIP.Text = strNZIP.Substring(0, 3) & "-" & strNZIP.Substring(3, 4)


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
    ''' メールアドレステキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCEMAIL_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCEMAIL.KeyPress
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack And (e.KeyChar < "a"c Or e.KeyChar > "z"c) And (e.KeyChar < "A"c Or e.KeyChar > "Z"c) _
                And (Not e.KeyChar.Equals("."c)) And (Not e.KeyChar.Equals("@"c)) And (Not e.KeyChar.Equals("-"c)) And (Not e.KeyChar.Equals("_"c)) Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 電話番号テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTelBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCTELEPHONE.KeyPress, txtCFAX.KeyPress, txtCPOTABLENUM.KeyPress, txtHANDICAP.KeyPress
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack And Not e.KeyChar.Equals("-"c) Then
                e.Handled = True
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
    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNCSNO.KeyPress, txtDBIRTH.KeyPress, txtNZIP.KeyPress, txtDMEMBER.KeyPress, txtDSCLMANNO.KeyPress, txtKRKNO.KeyPress
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
    ''' テキストボックス_Enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNZIP.Enter, txtDBIRTH.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace("/", String.Empty)
            txtBox.Text = txtBox.Text.Replace("-", String.Empty)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_MouseDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtDBIRTH.MouseDown, txtNZIP.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

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
            Using frm As New frmCSSEARCH01(iDatabase)
                frm.ShowDialog()
                intNCSNO = frm.GetNCSNO
            End Using

            If intNCSNO.Equals(0) Then Exit Sub

            Me.txtNCSNO.Text = intNCSNO.ToString.PadLeft(8, "0"c)

            If Not GetCSMAST() Then
                Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                    frm.ShowDialog()
                End Using
                Init()
                Me.txtNCSNO.Focus()
                Exit Sub
            End If

            Me.pnlRegisterInfo.Enabled = True
            Me.chkCDSTOP.Enabled = True
            Me.txtCCSNAME.Focus()
            '更新
            _intRegisterKbn = 1

            '削除
            Me.tspFunc9.Enabled = True


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F9削除ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func9()
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmMSGBOX01("データを削除してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            'トランザクション開始
            iDatabase.BeginTransaction()

            '顧客データ削除
            sql.Clear()
            sql.Append("DELETE FROM CSMAST WHERE NCSNO = " & CType(Me.txtNCSNO.Text, Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("顧客情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '金額サマリ削除
            sql.Clear()
            sql.Append("DELETE FROM KINSMA WHERE MANNO = '" & Me.txtNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("顧客情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            'ポイントサマリ削除
            sql.Clear()
            sql.Append("DELETE FROM DPOINTSMA WHERE MANNO = '" & Me.txtNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("顧客情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'コミット
            iDatabase.Commit()



            Using frm As New frmMSGBOX01("正常に削除できました。", 0)
                frm.ShowDialog()
            End Using

            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                Using frm As New frmREQUESTCARD(dcICR700)
                    'カード排出処理
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                End Using
            End If
            Init()
            Me.txtNCSNO.Focus()

            '画面初期化
            Init()

            Me.txtNCSNO.Focus()

        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

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

            If CType(Me.btnCard.Tag, Integer).Equals(1) Then
                Using frm As New frmREQUESTCARD(dcICR700)
                    'カード排出処理
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    frm.ShowDialog()
                End Using
            End If

            '画面初期化
            Init()



            Me.txtNCSNO.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try
            '登録内容チェック
            Dim Msg As String = String.Empty
            If Not CheckRegister(Msg) Then
                Using frm As New frmMSGBOX01(Msg, 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            Me.Cursor = Cursors.WaitCursor

            If _intRegisterKbn.Equals(0) Then
                '【新規】


                '顧客番号重複チェック
                Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM CSMAST WHERE NCSNO = " & CType(Me.txtNCSNO.Text, Integer))

                If Not resultDt.Rows.Count.Equals(0) Then
                    '既に登録されていた場合採番しなおし
                    GetNewNCSNO()
                End If

                '新規登録
                If Not InsRegister() Then
                    Using frm As New frmMSGBOX01("顧客情報の新規登録に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

            Else
                '【更新】


                '他端末からの更新チェック
                Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM CSMAST WHERE NCSNO = " & CType(Me.txtNCSNO.Text, Integer))

                If Not resultDt.Rows.Count.Equals(0) Then
                    '最新の更新日時を取得

                    Dim drSelectRow() As DataRow = resultDt.Select
                    Dim dtUPDDTM As DateTime = DirectCast(drSelectRow(0).Item("DUPDATE"), DateTime)

                    If Not dtUPDDTM.Equals(_dtUPDDTM) Then
                        Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If
                End If

                '更新登録
                If Not UpdRegister() Then
                    Using frm As New frmMSGBOX01("顧客情報の更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using


            If _intRegisterKbn.Equals(0) Then
                '【カード発行】
                Me.btnISSUE.Enabled = True
                Me.btnISSUE.PerformClick()
            End If


            If _intRegisterKbn.Equals(0) Then
                '*** 新規 ***'

                '画面初期設定
                Init()

                Me.txtNCSNO.Focus()
            Else
                '*** 更新 ***'

                If Not GetCSMAST() Then
                    '画面初期設定
                    Init()

                    Me.txtNCSNO.Focus()
                    Exit Sub
                End If

                Me.pnlRegisterInfo.Enabled = True
                Me.txtCCSNAME.Focus()
                '更新
                _intRegisterKbn = 1

                '削除
                Me.tspFunc9.Enabled = True
                '会員期限更新ボタン
                Me.btnUpdDMEMBER.Enabled = True

            End If



        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Me.Cursor = Cursors.Default
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
            Me.tspFunc3.Enabled = True
            '印刷
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = True

            'カード取引履歴ボタン
            Me.btnCARDTORIHIST01.Enabled = False

            'カード挿入ボタン
            Me.btnCard.Text = UIMessage.InsertButton
            Me.btnCard.BackColor = Color.DarkOliveGreen
            Me.btnCard.Tag = 0
            'カード発行ボタン
            Me.btnISSUE.Enabled = False

            'カード停止
            Me.chkCDSTOP.Checked = False

            '種別書き換え
            Me.btnCardNCSRANK.Enabled = False

            '顧客番号
            Me.txtNCSNO.Text = String.Empty
            Me.txtNCSNO.ReadOnly = False
            '顧客種別
            Me.cmbNCSRANK.SelectedIndex = -1
            '氏名
            Me.txtCCSNAME.Text = String.Empty
            '氏名ｶﾅ
            Me.txtCCSKANA.Text = String.Empty
            '性別
            Me.rdoNSEX1.Checked = True
            '左打ち区分
            Me.chkDLEFTKBN.Checked = False
            '誕生日
            Me.txtDBIRTH.Text = String.Empty
            Me.lblAge.Text = String.Empty
            '郵便番号
            Me.txtNZIP.Text = String.Empty
            '住所１
            Me.txtCADDRESS1.Text = String.Empty
            '住所２
            Me.txtCADDRESS2.Text = String.Empty
            '電話番号
            Me.txtCTELEPHONE.Text = String.Empty
            'FAX番号
            Me.txtCFAX.Text = String.Empty
            '携帯電話番号
            Me.txtCPOTABLENUM.Text = String.Empty
            'メールアドレス
            Me.txtCEMAIL.Text = String.Empty
            'メールアドレス配信区分
            Me.chkCEMAILKBN.Checked = False
            'カード番号
            Me.txtNCARDID.Text = String.Empty
            '登録日
            Me.txtDENTRY.Text = String.Empty
            '前回来場日
            Me.txtENTDT.Text = String.Empty
            'カード期限
            Me.txtCARDLIMIT.Text = String.Empty
            'メッセージ
            Me.txtMANCOMMENT.Text = String.Empty
            '会員期限
            Me.txtDMEMBER.Text = String.Empty
            '総来場回数
            Me.txtENTCNT.Text = String.Empty
            '月間来場回数
            Me.txtENTCNT2.Text = String.Empty
            'カードコメント
            Me.txtMANINFO.Text = String.Empty
            '備考１
            Me.txtBIKO1.Text = String.Empty
            '備考２
            Me.txtBIKO2.Text = String.Empty
            '備考３
            Me.txtBIKO3.Text = String.Empty
            'ポップアップ
            Me.chkPASSNO.Checked = False
            '残金額
            Me.txtZANKN.Text = "0"
            'P)残金額
            Me.txtPREZANKN.Text = "0"
            '残金額計
            Me.txtSUMZANKN.Text = "0"
            '残ポイント
            Me.txtSRTPO.Text = "0"
            '総利用球数金額
            Me.txtSUMUSEKIN.Text = String.Empty
            'スクール生番号
            Me.txtDSCLMANNO.Text = String.Empty
            'スクール生種別
            Me.txtSCLKBN.Text = String.Empty
            '協力金番号
            Me.txtKRKNO.Text = String.Empty
            'ハンディキャップ
            Me.txtHANDICAP.Text = String.Empty


            '発行料入金機精算
            Me.chkHakko.Checked = False
            Me.chkHakko.Enabled = False
            '発行料精算クリア
            Me.btnHakkoClear.Enabled = False

            'カラー情報
            Me.btnClrKbn1.BackColor = Color.White
            Me.btnClrKbn1.ForeColor = Color.White
            Me.btnClrKbn1.Text = "White"
            Me.btnClrKbn2.BackColor = Color.White
            Me.btnClrKbn2.ForeColor = Color.White
            Me.btnClrKbn2.Text = "White"
            Me.btnClrKbn3.BackColor = Color.White
            Me.btnClrKbn3.ForeColor = Color.White
            Me.btnClrKbn3.Text = "White"
            Me.btnClrKbn4.BackColor = Color.White
            Me.btnClrKbn4.ForeColor = Color.White
            Me.btnClrKbn4.Text = "White"
            Me.btnClrKbn5.BackColor = Color.White
            Me.btnClrKbn5.ForeColor = Color.White
            Me.btnClrKbn5.Text = "White"

            Me.pnlRegisterInfo.Enabled = False
            Me.chkCDSTOP.Enabled = False

            Me.pnlCsInfo.Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 住所情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetZIPMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM ZIPMTA")
            sql.Append(" ORDER BY ZIPCD ")

            _dtZIPMTA = iDatabase.ExecuteRead(sql.ToString())

            If _dtZIPMTA.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 現時点での最終顧客番号確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkNewNCSNO() As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" (MAX(MANNOSEQ) + 1) AS NEWNO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("NEWNO").ToString, Integer)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 顧客番号取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNewNCSNO() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" (MAX(MANNOSEQ) + 1) AS NEWNO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Me.txtNCSNO.Text = resultDt.Rows(0).Item("NEWNO").ToString.ToString.PadLeft(8, "0"c)

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' カード番号取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNewCARDID() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" (MAX(DCARDSEQ) + 1) AS NEWNO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Me.txtNCARDID.Text = resultDt.Rows(0).Item("NEWNO").ToString.PadLeft(8, "0"c)

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally

        End Try
    End Function

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


            Me.cmbNCSRANK.Items.Add(String.Empty)
            For i As Integer = 0 To _dtKBMAST.Rows.Count - 1
                Me.cmbNCSRANK.Items.Add(_dtKBMAST.Rows(i).Item("CKBNAME").ToString)
            Next

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 総利用球数金額
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetSUMUSEKIN() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" NCSNO")
            sql.Append(",SUM(USEKIN) AS USEKIN")
            sql.Append(" FROM BALLTRN")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(Me.txtNCSNO.Text, Integer))
            sql.Append(" GROUP BY NCSNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Me.txtSUMUSEKIN.Text = "0"
                Return False
            End If

            Me.txtSUMUSEKIN.Text = CType(resultDt.Rows(0).Item("USEKIN"), Integer).ToString("#,##0")

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <param name="blnCard">カードによる検索</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST(Optional ByVal blnCard As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            'カード取引履歴ボタン
            Me.btnCARDTORIHIST01.Enabled = False

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
            If blnCard Then
                sql.Append(" NCARDID = " & CType(dcICR700.NCSNO, Integer))
                If dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
                    Return False
                End If
            Else
                sql.Append(" NCSNO = " & CType(Me.txtNCSNO.Text, Integer))
            End If

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            '顧客番号
            Me.txtNCSNO.Text = resultDt.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)

            'カラーサマリ情報取得
            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" CLRSMA")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & Me.txtNCSNO.Text & "'")

            Dim dtCLRSMA As DataTable = iDatabase.ExecuteRead(sql.ToString)

            Me.btnClrKbn1.BackColor = Color.White : Me.btnClrKbn1.ForeColor = Color.White : Me.btnClrKbn1.Text = "White"
            Me.btnClrKbn2.BackColor = Color.White : Me.btnClrKbn2.ForeColor = Color.White : Me.btnClrKbn2.Text = "White"
            Me.btnClrKbn3.BackColor = Color.White : Me.btnClrKbn3.ForeColor = Color.White : Me.btnClrKbn3.Text = "White"
            Me.btnClrKbn4.BackColor = Color.White : Me.btnClrKbn4.ForeColor = Color.White : Me.btnClrKbn4.Text = "White"
            Me.btnClrKbn5.BackColor = Color.White : Me.btnClrKbn5.ForeColor = Color.White : Me.btnClrKbn5.Text = "White"
            If Not dtCLRSMA.Rows.Count.Equals(0) Then
                Me.btnClrKbn1.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN1").ToString) : Me.btnClrKbn1.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN1").ToString) : Me.btnClrKbn1.Text = dtCLRSMA.Rows(0).Item("CLRKBN1").ToString
                Me.btnClrKbn2.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN2").ToString) : Me.btnClrKbn2.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN2").ToString) : Me.btnClrKbn2.Text = dtCLRSMA.Rows(0).Item("CLRKBN2").ToString
                Me.btnClrKbn3.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN3").ToString) : Me.btnClrKbn3.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN3").ToString) : Me.btnClrKbn3.Text = dtCLRSMA.Rows(0).Item("CLRKBN3").ToString
                Me.btnClrKbn4.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN4").ToString) : Me.btnClrKbn4.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN4").ToString) : Me.btnClrKbn4.Text = dtCLRSMA.Rows(0).Item("CLRKBN4").ToString
                Me.btnClrKbn5.BackColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN5").ToString) : Me.btnClrKbn5.ForeColor = Color.FromName(dtCLRSMA.Rows(0).Item("CLRKBN5").ToString) : Me.btnClrKbn5.Text = dtCLRSMA.Rows(0).Item("CLRKBN5").ToString
            End If

            '種別書き換え
            Me.btnCardNCSRANK.Enabled = True

            'カード取引履歴ボタン
            Me.btnCARDTORIHIST01.Enabled = True

            '氏名
            Me.txtCCSNAME.Text = resultDt.Rows(0).Item("CCSNAME").ToString
            'ｶﾅ
            Me.txtCCSKANA.Text = resultDt.Rows(0).Item("CCSKANA").ToString
            '性別
            If CType(resultDt.Rows(0).Item("NSEX").ToString, Integer).Equals(2) Then
                '【女】
                Me.rdoNSEX2.Checked = True
            Else
                '【男】
                Me.rdoNSEX1.Checked = True
            End If
            '左打ち区分
            If CType(resultDt.Rows(0).Item("DLEFTKBN").ToString, Integer).Equals(1) Then
                '【左打ち】
                Me.chkDLEFTKBN.Checked = True
            Else
                Me.chkDLEFTKBN.Checked = False
            End If
            '誕生日
            Me.txtDBIRTH.Text = resultDt.Rows(0).Item("TO_DBIRTH").ToString
            Me.lblAge.Text = String.Empty
            If Not String.IsNullOrEmpty(Me.txtDBIRTH.Text) Then
                Me.lblAge.Text = UIFunction.GetAge(DateTime.Parse(Me.txtDBIRTH.Text), Now).ToString & "歳"
            End If
            '顧客種別
            Me.cmbNCSRANK.SelectedIndex = CType(resultDt.Rows(0).Item("NCSRANK").ToString, Integer)
            _intDbNCSRANK = Me.cmbNCSRANK.SelectedIndex
            '郵便番号
            Me.txtNZIP.Text = String.Empty
            If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("NZIP").ToString) Then
                Me.txtNZIP.Text = resultDt.Rows(0).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(0, 3) & "-" & resultDt.Rows(0).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(3, 4)
            End If
            '住所１
            Me.txtCADDRESS1.Text = resultDt.Rows(0).Item("CADDRESS1").ToString
            '住所２
            Me.txtCADDRESS2.Text = resultDt.Rows(0).Item("CADDRESS2").ToString
            '電話番号
            Me.txtCTELEPHONE.Text = resultDt.Rows(0).Item("CTELEPHONE").ToString
            'FAX番号
            Me.txtCFAX.Text = resultDt.Rows(0).Item("CFAX").ToString
            '携帯電話番号
            Me.txtCPOTABLENUM.Text = resultDt.Rows(0).Item("CPOTABLENUM").ToString
            'メールアドレス
            Me.txtCEMAIL.Text = resultDt.Rows(0).Item("CEMAIL").ToString
            'メール配信区分
            If CType(resultDt.Rows(0).Item("CEMAILKBN").ToString, Integer).Equals(1) Then
                Me.chkCEMAILKBN.Checked = True
            End If
            'カード番号
            Me.txtNCARDID.Text = resultDt.Rows(0).Item("NCARDID").ToString.PadLeft(8, "0"c)
            '会員登録日
            Me.txtDENTRY.Text = resultDt.Rows(0).Item("TO_DENTRY").ToString
            '前回来場日
            Me.txtENTDT.Text = String.Empty
            If (Not String.IsNullOrEmpty(resultDt.Rows(0).Item("ENTDT").ToString) And Not resultDt.Rows(0).Item("ENTDT").ToString.Equals("00000000")) Then
                Me.txtENTDT.Text = resultDt.Rows(0).Item("ENTDT").ToString.Substring(0, 4) & "/" _
                                & resultDt.Rows(0).Item("ENTDT").ToString.Substring(4, 2) & "/" _
                                & resultDt.Rows(0).Item("ENTDT").ToString.Substring(6, 2)
            End If
            'カード有効期限
            Me.txtCARDLIMIT.Text = String.Empty
            If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("CARDLIMIT").ToString) Then
                Me.txtCARDLIMIT.Text = resultDt.Rows(0).Item("CARDLIMIT").ToString.Substring(0, 4) & "/" _
                                & resultDt.Rows(0).Item("CARDLIMIT").ToString.Substring(4, 2) & "/" _
                                & resultDt.Rows(0).Item("CARDLIMIT").ToString.Substring(6, 2)
            End If
            'メッセージ
            Me.txtMANCOMMENT.Text = resultDt.Rows(0).Item("MANCOMMENT").ToString
            '会員期限
            Me.txtDMEMBER.Text = resultDt.Rows(0).Item("TO_DMEMBER").ToString
            Me.txtDMEMBER.ForeColor = Color.Black
            '会員期限チェック
            Dim strDMEMBER As String = String.Empty
            Dim strToday As String = Now.Year.ToString & Now.Month.ToString.PadLeft(2, "0"c) & Now.Day.ToString.PadLeft(2, "0"c)
            If Not String.IsNullOrEmpty(Me.txtDMEMBER.Text) Then
                Dim dtDMEMBER As DateTime = DateTime.Parse(Me.txtDMEMBER.Text)
                ' 1ヶ月減算する
                dtDMEMBER = dtDMEMBER.AddMonths(-1)
                strDMEMBER = Me.txtDMEMBER.Text.Replace("/", String.Empty)
                If strDMEMBER < strToday Then
                    Me.txtDMEMBER.ForeColor = Color.Red
                    Using frm As New frmMSGBOX01("会員期限が切れています。", 0)
                        frm.ShowDialog()
                    End Using
                ElseIf dtDMEMBER.ToString("yyyyMMdd") <= strToday Then
                    Me.txtDMEMBER.ForeColor = Color.Orange
                End If
            End If
            If String.IsNullOrEmpty(resultDt.Rows(0).Item("CKBLIMIT").ToString) Then
                Me.btnUpdDMEMBER.Tag = "0"
            Else
                Me.btnUpdDMEMBER.Tag = CType(resultDt.Rows(0).Item("CKBLIMIT").ToString, Integer)
            End If

            '総来場回数
            Me.txtENTCNT.Text = CType(resultDt.Rows(0).Item("ENTCNT").ToString, Integer).ToString("#,##0")
            '月間来場回数
            Me.txtENTCNT2.Text = CType(resultDt.Rows(0).Item("ENTCNT2").ToString, Integer).ToString("#,##0")
            'カードコメント
            Me.txtMANINFO.Text = resultDt.Rows(0).Item("MANINFO").ToString
            '備考１
            Me.txtBIKO1.Text = resultDt.Rows(0).Item("BIKO1").ToString
            '備考２
            Me.txtBIKO2.Text = resultDt.Rows(0).Item("BIKO2").ToString
            '備考３
            Me.txtBIKO3.Text = resultDt.Rows(0).Item("BIKO3").ToString
            'ポップアップ
            Me.chkPASSNO.Checked = False
            If resultDt.Rows(0).Item("MEMBERNO").ToString.Equals("1") Then
                Me.chkPASSNO.Checked = True
            End If
            '協力金番号
            Me.txtKRKNO.Text = resultDt.Rows(0).Item("KRKNO").ToString.PadLeft(4, "0"c)
            'ハンディキャップ
            Me.txtHANDICAP.Text = resultDt.Rows(0).Item("HANDICAP").ToString

            '残金額
            Me.txtZANKN.Text = CType(resultDt.Rows(0).Item("ZANKN").ToString, Integer).ToString("#,##0")
            'P)残金額
            Me.txtPREZANKN.Text = CType(resultDt.Rows(0).Item("PREZANKN").ToString, Integer).ToString("#,##0")
            Me.txtSUMZANKN.Text = (CType(Me.txtZANKN.Text, Integer) + CType(Me.txtPREZANKN.Text, Integer)).ToString("#,##0")
            '残ポイント
            Me.txtSRTPO.Text = CType(resultDt.Rows(0).Item("SRTPO").ToString, Integer).ToString("#,##0")

            'スクール生番号
            Me.txtDSCLMANNO.Text = resultDt.Rows(0).Item("DSCLMANNO").ToString
            _strDelSCLMANNO = Me.txtDSCLMANNO.Text
            'スクール生区分
            Select Case resultDt.Rows(0).Item("SCLKBN").ToString
                Case "0" : Me.txtSCLKBN.Text = "本科生"
                Case "1" : Me.txtSCLKBN.Text = "体験"
                Case "8" : Me.txtSCLKBN.Text = "休会中"
                Case "9" : Me.txtSCLKBN.Text = "退会"
                Case Else : Me.txtSCLKBN.Text = String.Empty
            End Select
            _strSCLKBN = resultDt.Rows(0).Item("SCLKBN").ToString
            If Not String.IsNullOrEmpty(Me.txtDSCLMANNO.Text) And String.IsNullOrEmpty(_strSCLKBN) Then
                Using frm As New frmMSGBOX01("スクール生情報が削除された可能性があります。", 3)
                    frm.ShowDialog()
                End Using
            End If

            _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("DUPDATE"), DateTime)

            'カード発行ボタン
            Me.btnISSUE.Enabled = True

            'カード停止
            If String.IsNullOrEmpty(Me.txtNCARDID.Text) Or Me.txtNCARDID.Text.Equals("00000000") Then
                Me.chkCDSTOP.Checked = False
                'カード停止チェックボックス
                Me.chkCDSTOP.Enabled = False
            Else
                Me.chkCDSTOP.Checked = UIFunction.ChkDCSTPTRN(Me.txtNCARDID.Text, Me.txtNCSNO.Text, iDatabase)
                'カード停止チェックボックス
                Me.chkCDSTOP.Enabled = True
            End If

            '総利用球数金額取得
            GetSUMUSEKIN()

            Me.chkHakko.Checked = True
            Me.btnHakkoClear.Enabled = True
            If blnCard Then
                If dcICR700.ENTKBN.Equals("0") Then
                    Me.chkHakko.Checked = False
                    Me.btnHakkoClear.Enabled = False
                End If
            ElseIf Me.txtNCARDID.Text.Equals("00000000") Or String.IsNullOrEmpty(Me.txtNCARDID.Text) Then
                Me.chkHakko.Checked = True
                Me.chkHakko.Enabled = True
                Me.btnHakkoClear.Enabled = False
            Else
                Me.btnHakkoClear.Enabled = False
                Me.chkHakko.Checked = False
                Me.chkHakko.Enabled = False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 登録データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckRegister(ByRef Msg As String) As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtCCSNAME.Text) Then
                Msg = "氏名を入力して下さい。"
                Me.txtCCSNAME.Focus()
                Return False
            End If
            If Me.cmbNCSRANK.SelectedIndex.Equals(0) Or Me.cmbNCSRANK.SelectedIndex.Equals(-1) Then
                Msg = "顧客種別を選択して下さい。"
                Me.cmbNCSRANK.Focus()
                Return False
            End If
            '誕生日
            Dim dtDBIRTH As DateTime
            Dim strDBIRTH As String = String.Empty
            If Not String.IsNullOrEmpty(Me.txtDBIRTH.Text) Then
                strDBIRTH = Me.txtDBIRTH.Text.Replace("/", String.Empty).PadLeft(8, "0"c)
                strDBIRTH = strDBIRTH.Substring(0, 4) & "/" & strDBIRTH.Substring(4, 2) & "/" & strDBIRTH.Substring(6, 2)
                If Not DateTime.TryParse(strDBIRTH, dtDBIRTH) Then
                    Msg = "日付として認識できません。"
                    Me.txtDBIRTH.Focus()
                    Return False
                End If
            End If
            '会員期限
            Dim dtDMEMBER As DateTime
            Dim strDMEMBER As String = String.Empty
            If Not String.IsNullOrEmpty(Me.txtDMEMBER.Text) Then
                strDMEMBER = Me.txtDMEMBER.Text.Replace("/", String.Empty)
                If Not strDMEMBER.Length.Equals(8) Then
                    Msg = "日付として認識できません。"
                    Me.txtDMEMBER.Focus()
                    Return False
                End If
                strDMEMBER = strDMEMBER.Substring(0, 4) & "/" & strDMEMBER.Substring(4, 2) & "/" & strDMEMBER.Substring(6, 2)
                If Not DateTime.TryParse(strDMEMBER, dtDMEMBER) Then
                    Msg = "日付として認識できません。"
                    Me.txtDMEMBER.Focus()
                    Return False
                End If
            End If

            'ハンディキャップ
            Dim intNum As Integer = 0
            If Not String.IsNullOrEmpty(Me.txtHANDICAP.Text) Then
                If Not Integer.TryParse(Me.txtHANDICAP.Text, intNum) = True Then
                    Me.txtHANDICAP.Text = "0"
                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsRegister() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intFLG As Integer = 0
        Try

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If CType(Me.txtNCSNO.Text, Integer) > CType(resultDt.Rows(0).Item("MANNOSEQ").ToString, Integer) Then
                intFLG = 0
            Else
                intFLG = 1
            End If

            'トランザクション開始
            iDatabase.BeginTransaction()

            If intFLG.Equals(0) Then
                '顧客番号カウントアップ
                sql.Clear()
                sql.Append("UPDATE SEQTRN SET MANNOSEQ = MANNOSEQ + 1")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If

            strSQL1 &= "INSERT INTO CSMAST("
            strSQL2 &= " VALUES("
            '顧客番号
            strSQL1 &= "NCSNO,"
            strSQL2 &= CType(Me.txtNCSNO.Text, Integer) & ","
            'カード番号
            strSQL1 &= "NCARDID,"
            strSQL2 &= "NULL,"
            '顧客名
            strSQL1 &= "CCSNAME,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCCSNAME.Text) & ","
            '顧客名(ｶﾅ)
            strSQL1 &= "CCSKANA,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCCSKANA.Text) & ","
            '顧客種別
            strSQL1 &= "NCSRANK,"
            strSQL2 &= Me.cmbNCSRANK.SelectedIndex & ","
            '性別
            strSQL1 &= "NSEX,"
            If Me.rdoNSEX1.Checked Then
                '【男】
                strSQL2 &= "1,"
            Else
                '【女】
                strSQL2 &= "2,"
            End If
            'カードコメント
            strSQL1 &= "MANINFO,"
            strSQL2 &= UIFunction.NullCheck(Me.txtMANINFO.Text) & ","
            '郵便番号
            strSQL1 &= "NZIP,"
            strSQL2 &= UIFunction.NullCheck(Me.txtNZIP.Text.Replace("-", String.Empty)) & ","
            '住所１
            strSQL1 &= "CADDRESS1,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCADDRESS1.Text) & ","
            '住所２
            strSQL1 &= "CADDRESS2,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCADDRESS2.Text) & ","
            '電話番号
            strSQL1 &= "CTELEPHONE,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCTELEPHONE.Text) & ","
            'FAX番号
            strSQL1 &= "CFAX,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCFAX.Text) & ","
            '携帯電話番号
            strSQL1 &= "CPOTABLENUM,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCPOTABLENUM.Text) & ","
            '会員状態
            strSQL1 &= "NMRKBN,"
            strSQL2 &= "0,"
            '誕生日
            strSQL1 &= "DBIRTH,"
            strSQL2 &= UIFunction.NullCheck(Me.txtDBIRTH.Text) & ","
            '会員期限
            strSQL1 &= "DMEMBER,"
            strSQL2 &= UIFunction.NullCheck(Me.txtDMEMBER.Text) & ","
            '前回来場日
            strSQL1 &= "ZENENTDATE,"
            strSQL2 &= UIFunction.NullCheck(Me.txtENTDT.Text) & ","
            'メールアドレス
            strSQL1 &= "CEMAIL,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCEMAIL.Text) & ","
            '会員メモ
            strSQL1 &= "MANCOMMENT,"
            strSQL2 &= UIFunction.NullCheck(Me.txtMANCOMMENT.Text) & ","
            'スクール生番号
            strSQL1 &= "DSCLMANNO,"
            strSQL2 &= UIFunction.NullCheck(Me.txtDSCLMANNO.Text) & ","
            '削除フラグ
            strSQL1 &= "NFLAGDEL,"
            strSQL2 &= "0,"
            'メール配信区分
            strSQL1 &= "CEMAILKBN,"
            If chkCEMAILKBN.Checked Then
                '【配信】
                strSQL2 &= "1,"
            Else
                '【配信不要】
                strSQL2 &= "9,"
            End If
            '前回来店日
            strSQL1 &= "ENTDT,"
            strSQL2 &= UIFunction.NullCheck(Me.txtENTDT.Text.Replace("/", String.Empty)) & ","
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
            strSQL2 &= UIFunction.NullCheck(Me.txtBIKO1.Text) & ","
            '備考２
            strSQL1 &= "BIKO2,"
            strSQL2 &= UIFunction.NullCheck(Me.txtBIKO2.Text) & ","
            '備考３
            strSQL1 &= "BIKO3,"
            strSQL2 &= UIFunction.NullCheck(Me.txtBIKO3.Text) & ","
            'ポップアップ
            strSQL1 &= "MEMBERNO,"
            If chkPASSNO.Checked Then
                strSQL2 &= "'1',"
            Else
                strSQL2 &= "'0',"
            End If

            '総来場回数
            strSQL1 &= "ENTCNT,"
            strSQL2 &= "0,"
            '左打ち区分
            strSQL1 &= "DLEFTKBN,"
            If chkDLEFTKBN.Checked Then
                '【左打ち】
                strSQL2 &= "1,"
            Else
                strSQL2 &= "0,"
            End If
            '月間来場回数
            strSQL1 &= "ENTCNT2,"
            strSQL2 &= "0,"
            'カード期限
            strSQL1 &= "CARDLIMIT,"
            strSQL2 &= UIFunction.NullCheck(Me.txtCARDLIMIT.Text.Replace("/", String.Empty)) & ","
            'カード管理番号
            strSQL1 &= "CARDADMINNO,"
            strSQL2 &= "NULL,"

            '協力金番号
            strSQL1 &= "KRKNO,"
            If String.IsNullOrEmpty(Me.txtKRKNO.Text) Then
                strSQL2 &= "0,"
            Else
                strSQL2 &= CType(Me.txtKRKNO.Text, Integer) & ","
            End If

            'ハンディキャップ
            strSQL1 &= "HANDICAP)"
            If String.IsNullOrEmpty(Me.txtHANDICAP.Text) Then
                strSQL2 &= "0)"
            Else
                strSQL2 &= CType(Me.txtHANDICAP.Text, Integer) & ")"
            End If



            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
                Return False
            End If

            '金額サマリ登録
            strSQL1 = "INSERT INTO KINSMA VALUES("
            strSQL1 &= "'" & Me.txtNCSNO.Text & "',"
            strSQL1 &= UIFunction.NullCheck(Me.txtDSCLMANNO.Text) & ","
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
            strSQL1 &= UIFunction.NullCheck(Me.txtDSCLMANNO.Text) & ","
            strSQL1 &= "0,"
            strSQL1 &= "NOW(),"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If

            'スクール生情報更新
            If Not UpdMANMST() Then
                iDatabase.RollBack()
                Return False
            End If

            'コミット
            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="blnUpdDCARDSEQ">カード番号を更新するかどうか【True】する【False】しない</param>
    ''' <param name="blnUpdKINSMA">金額サマリを更新するかどうか【True】する【False】しない</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdRegister(Optional ByVal blnUpdDCARDSEQ As Boolean = False, Optional ByVal blnUpdKINSMA As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            'トランザクション開始
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE CSMAST SET ")
            'カード番号
            sql.Append("NCARDID = " & CType(Me.txtNCARDID.Text, Integer) & ",")
            '顧客名
            sql.Append("CCSNAME = " & UIFunction.NullCheck(Me.txtCCSNAME.Text) & ",")
            '顧客名(ｶﾅ)
            sql.Append("CCSKANA = " & UIFunction.NullCheck(Me.txtCCSKANA.Text) & ",")
            '顧客種別
            sql.Append("NCSRANK = " & Me.cmbNCSRANK.SelectedIndex & ",")

            '性別
            If Me.rdoNSEX1.Checked Then
                '【男】
                sql.Append("NSEX = 1,")
            Else
                '【女】
                sql.Append("NSEX = 2,")
            End If
            'カードコメント
            sql.Append("MANINFO = " & UIFunction.NullCheck(Me.txtMANINFO.Text) & ",")
            '郵便番号
            sql.Append("NZIP = " & UIFunction.NullCheck(Me.txtNZIP.Text.Replace("-", String.Empty)) & ",")
            '住所１
            sql.Append("CADDRESS1 = " & UIFunction.NullCheck(Me.txtCADDRESS1.Text) & ",")
            '住所２
            sql.Append("CADDRESS2 = " & UIFunction.NullCheck(Me.txtCADDRESS2.Text) & ",")
            '電話番号
            sql.Append("CTELEPHONE = " & UIFunction.NullCheck(Me.txtCTELEPHONE.Text) & ",")
            'FAX番号
            sql.Append("CFAX = " & UIFunction.NullCheck(Me.txtCFAX.Text) & ",")
            '携帯電話番号
            sql.Append("CPOTABLENUM = " & UIFunction.NullCheck(Me.txtCPOTABLENUM.Text) & ",")
            '誕生日
            sql.Append("DBIRTH = " & UIFunction.NullCheck(Me.txtDBIRTH.Text) & ",")
            '会員期限
            sql.Append("DMEMBER = " & UIFunction.NullCheck(Me.txtDMEMBER.Text) & ",")
            'メールアドレス
            sql.Append("CEMAIL = " & UIFunction.NullCheck(Me.txtCEMAIL.Text) & ",")
            '会員メモ
            sql.Append("MANCOMMENT = " & UIFunction.NullCheck(Me.txtMANCOMMENT.Text) & ",")
            'スクール生番号
            sql.Append("DSCLMANNO = " & UIFunction.NullCheck(Me.txtDSCLMANNO.Text) & ",")
            '前回スクール生番号
            sql.Append("BDSCLMANNO = " & UIFunction.NullCheck(Me.txtDSCLMANNO.Text) & ",")
            'メール配信区分
            If chkCEMAILKBN.Checked Then
                '【配信】
                sql.Append("CEMAILKBN = 1,")
            Else
                '【配信不要】
                sql.Append("CEMAILKBN = 9,")
            End If
            ''前回来店日
            'sql.Append("ENTDT = " & UIFunction.NullCheck(Me.txtENTDT.Text.Replace("/", String.Empty)) & ",")
            '更新日
            sql.Append("DUPDATE = NOW(),")
            '備考１
            sql.Append("BIKO1 = " & UIFunction.NullCheck(Me.txtBIKO1.Text) & ",")
            '備考２
            sql.Append("BIKO2 = " & UIFunction.NullCheck(Me.txtBIKO2.Text) & ",")
            '備考３
            sql.Append("BIKO3 = " & UIFunction.NullCheck(Me.txtBIKO3.Text) & ",")
            'ポップアップ
            If chkPASSNO.Checked Then
                sql.Append("MEMBERNO = '1',")
            Else
                sql.Append("MEMBERNO = '0',")
            End If
            '左打ち区分
            If chkDLEFTKBN.Checked Then
                '【左打ち】
                sql.Append("DLEFTKBN = 1,")
            Else
                sql.Append("DLEFTKBN = 0,")
            End If
            ''カード期限
            'sql.Append("CARDLIMIT = " & UIFunction.NullCheck(Me.txtCARDLIMIT.Text.Replace("/", String.Empty)) & ",")
            '協力金番号
            If String.IsNullOrEmpty(Me.txtKRKNO.Text) Then
                sql.Append("KRKNO = 0,")
            Else
                sql.Append("KRKNO = " & CType(Me.txtKRKNO.Text, Integer) & ",")
            End If
            'ハンディキャップ
            If String.IsNullOrEmpty(Me.txtHANDICAP.Text) Then
                sql.Append("HANDICAP = 0")
            Else
                sql.Append("HANDICAP = " & CType(Me.txtHANDICAP.Text, Integer))
            End If



            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(Me.txtNCSNO.Text, Integer))

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            'スクール生情報更新
            If Not UpdMANMST() Then
                iDatabase.RollBack()
                Return False
            End If

            If blnUpdDCARDSEQ Then
                'カード番号カウントアップ
                sql.Clear()
                sql.Append("UPDATE SEQTRN SET DCARDSEQ = DCARDSEQ + 1")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If

            If blnUpdKINSMA Then
                '金額サマリ・ポイントサマリ更新

                '【金額サマリ】
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & CType(Me.txtZANKN.Text, Integer))
                sql.Append(",PREZANKN = " & CType(Me.txtPREZANKN.Text, Integer))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & Me.txtNCSNO.Text & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '【ポイントサマリ】
                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & CType(Me.txtSRTPO.Text, Integer))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & Me.txtNCSNO.Text & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If

            'カード停止
            If Not (String.IsNullOrEmpty(Me.txtNCARDID.Text)) Or Not (Me.txtNCARDID.Text.Equals("00000000")) Then
                If Not blnUpdDCARDSEQ Then
                    If Me.chkCDSTOP.Checked Then
                        '【カード停止】
                        If Not UIFunction.ChkDCSTPTRN(Me.txtNCARDID.Text, Me.txtNCSNO.Text, iDatabase) Then
                            sql.Clear()
                            sql.Append("INSERT INTO DCSTPTRN VALUES(")
                            sql.Append(CType(Me.txtNCARDID.Text, Integer) & ",")
                            sql.Append(CType(Me.txtNCSNO.Text, Integer) & ",")
                            sql.Append("NOW(),")
                            sql.Append("NOW())")
                        End If
                    Else
                        '【停止解除】
                        sql.Clear()
                        sql.Append("DELETE FROM DCSTPTRN WHERE")
                        sql.Append(" NCARDID = " & CType(Me.txtNCARDID.Text, Integer))
                        sql.Append(" AND NCSNO = " & CType(Me.txtNCSNO.Text, Integer))
                    End If
                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                End If
            End If

            'コミット
            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' スクール生情報更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdMANMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSCLMANNO As String = String.Empty
        Try
            If String.IsNullOrEmpty(Me.txtDSCLMANNO.Text) Then
                If String.IsNullOrEmpty(_strDelSCLMANNO) Then
                    Return True
                Else
                    strSCLMANNO = _strDelSCLMANNO
                End If
            Else
                strSCLMANNO = Me.txtDSCLMANNO.Text
            End If

            'スクール生情報更新
            sql.Clear()
            sql.Append("UPDATE MANMST SET ")
            If String.IsNullOrEmpty(Me.txtDSCLMANNO.Text) Then
                sql.Append("MANNO = 'ZZZZZZZZ',")
            Else
                sql.Append("MANNO = '" & Me.txtNCSNO.Text & "',")
            End If
            If String.IsNullOrEmpty(Me.txtNCARDID.Text) Then
                sql.Append("MANCARDID = NULL,")
            Else
                If String.IsNullOrEmpty(Me.txtDSCLMANNO.Text) Then
                    sql.Append("MANCARDID = NULL,")
                Else
                    sql.Append("MANCARDID = '" & Me.txtNCARDID.Text & "',")
                End If
            End If

            sql.Append("KSBKB = '" & Me.cmbNCSRANK.SelectedIndex.ToString() & "',")
            Select Case Me.chkDLEFTKBN.Checked
                Case True : sql.Append("LEFTKBN = '1',")
                Case False : sql.Append("LEFTKBN = '0',")
            End Select

            sql.Append("UPDDTM = NOW()")
            ' スクール生番号指定
            sql.Append(" WHERE SCLMANNO = '" & strSCLMANNO & "'")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' スクール生重複チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkSclManInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT *")
            sql.Append(" FROM CSMAST")
            sql.Append(" WHERE")
            sql.Append(" DSCLMANNO = '" & Me.txtDSCLMANNO.Text & "'")
            If Not String.IsNullOrEmpty(Me.txtNCSNO.Text) Then
                sql.Append(" AND")
                sql.Append(" NCSNO <> " & CType(Me.txtNCSNO.Text, Integer))
            End If

            ' ***** 実行 *****
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' 件数取得
            Dim c_cnt As Integer = resultDt.Rows.Count

            ' ***** 検証 *****
            If c_cnt = 0 Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' スクール生情報を取得し画面コントロールに設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSclManInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT *")
            sql.Append(" FROM MANMST")
            sql.Append(" WHERE SCLMANNO = '" & Me.txtDSCLMANNO.Text & "'")

            ' ***** 実行 *****
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' 件数取得
            Dim c_cnt As Integer = resultDt.Rows.Count

            ' ***** 検証 *****
            If c_cnt = 0 Then
                Return False
            End If

            ' ***** 取得した内容をコントロールにセット ****
            For Each arr As DataRow In resultDt.Rows
                '氏名
                If String.IsNullOrEmpty(Me.txtCCSNAME.Text) Then
                    Me.txtCCSNAME.Text = arr("MANNM").ToString()
                End If
                '氏名ｶﾅ
                If String.IsNullOrEmpty(Me.txtCCSKANA.Text) Then
                    Me.txtCCSKANA.Text = arr("MANKN").ToString()
                End If
                '性別【1】男【2】女
                Select Case arr("MANSEX").ToString()
                    Case "1" : Me.rdoNSEX1.Checked = True
                    Case "2" : Me.rdoNSEX2.Checked = True
                End Select
                '左打ち区分
                Select Case arr("LEFTKBN").ToString()
                    Case "0" : Me.chkDLEFTKBN.Checked = False
                    Case "1" : Me.chkDLEFTKBN.Checked = True
                End Select
                '誕生日
                If String.IsNullOrEmpty(Me.txtDBIRTH.Text) Then
                    If Not String.IsNullOrEmpty(arr("MANBITH").ToString) Then
                        Me.txtDBIRTH.Text = arr("MANBITH").ToString().Substring(0, 4) & "/" & _
                                         arr("MANBITH").ToString().Substring(4, 2) & "/" & _
                                         arr("MANBITH").ToString().Substring(6, 2)
                    End If
                End If
                '郵便番号
                If String.IsNullOrEmpty(Me.txtNZIP.Text) Then
                    If Not String.IsNullOrEmpty(arr("MANZIP").ToString) Then
                        Me.txtNZIP.Text = arr("MANZIP").ToString().PadLeft(7, "0"c).Substring(0, 3) & "-" & arr("MANZIP").ToString().PadLeft(7, "0"c).Substring(3, 4)
                    End If
                End If
                '住所１
                If String.IsNullOrEmpty(Me.txtCADDRESS1.Text) Then
                    Me.txtCADDRESS1.Text = arr("MANADDA").ToString()
                End If
                '住所２
                If String.IsNullOrEmpty(Me.txtCADDRESS2.Text) Then
                    Me.txtCADDRESS2.Text = arr("MANADDB").ToString()
                End If
                '電話番号
                If String.IsNullOrEmpty(Me.txtCTELEPHONE.Text) Then
                    Me.txtCTELEPHONE.Text = arr("MANTELNO").ToString()
                End If
                'FAX番号
                If String.IsNullOrEmpty(Me.txtCFAX.Text) Then
                    Me.txtCFAX.Text = arr("MANFAXNO").ToString()
                End If
                '携帯電話番号
                If String.IsNullOrEmpty(Me.txtCPOTABLENUM.Text) Then
                    Me.txtCPOTABLENUM.Text = arr("MANKTELNO").ToString()
                End If
                'メールアドレス
                If String.IsNullOrEmpty(Me.txtCEMAIL.Text) Then
                    Me.txtCEMAIL.Text = arr("MANMAIL").ToString()
                End If
                'メールアドレス配信区分
                Select Case arr("MAILKBN").ToString()
                    Case "1" : Me.chkCEMAILKBN.Checked = True
                    Case "9" : Me.chkCEMAILKBN.Checked = False
                End Select

                Me.txtSCLKBN.Text = arr("SCLKBN").ToString()
                If IsNumeric(arr("SCLKBN")) Then
                    Select Case CInt(arr("SCLKBN"))
                        Case 0 : Me.txtSCLKBN.Text = "本科生"
                        Case 1 : Me.txtSCLKBN.Text = "体験"
                        Case 8 : Me.txtSCLKBN.Text = "休会中"
                        Case 9 : Me.txtSCLKBN.Text = "退会"
                    End Select
                End If
                _strSCLKBN = arr("SCLKBN").ToString
            Next

            Return True
        Catch ex As Exception
            Throw ex
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
        Dim strNCSNO As String = String.Empty
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
            ElseIf intPaySelect.Equals(2) Then
                strNCSNO = Me.txtNCARDID.Text

                'カード読み込み
                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    If frm.CANCEL Then
                        intCKBFEE = 0
                        intPaySelect = 0
                        Return True
                    End If
                    If frm.ERRFLG Then
                        intCKBFEE = 0
                        intPaySelect = 0
                        Return False
                    End If
                End Using

                If Not strNCSNO.Equals(dcICR700.NCSNO) Then
                    Using frm As New frmMSGBOX01("顧客情報が一致しません。", 3)
                        frm.ShowDialog()
                    End Using
                    intPaySelect = 0
                    intCKBFEE = 0
                    Return True
                End If

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
                frm.NCSNO = Me.txtNCSNO.Text
                '氏名
                frm.CCSNAME = Me.txtCCSNAME.Text
                'ｶﾅ
                frm.CCSKANA = Me.txtCCSKANA.Text
                '顧客種別
                frm.CKBNAME = Me.cmbNCSRANK.Text
                'スクール生番号
                frm.SCLMANNO = Me.txtDSCLMANNO.Text
                '会員期限
                frm.DMEMBER = Me.txtDMEMBER.Text
                '誕生日
                frm.DBIRTH = Me.txtDBIRTH.Text
                '残金
                frm.ZANKN = CType(Me.txtZANKN.Text, Integer).ToString("#,##0")
                'プレミアム
                frm.PREZANKN = CType(Me.txtPREZANKN.Text, Integer).ToString("#,##0")
                'ポイント
                frm.POINT = CType(Me.txtSRTPO.Text, Integer).ToString("#,##0")
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
                intAftKINGAKU = (CType(dcICR700.KINGAKU, Integer) - intCKBFEE)
                If intAftKINGAKU < 0 Then
                    Using frm As New frmMSGBOX01("カード残金が不足しています。", 2)
                        frm.ShowDialog()
                        intCKBFEE = 0
                        intPaySelect = 0
                        Return True
                    End Using
                End If

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
                'intV31PREZANKN = CType(dcICR700.PREZANKN, Integer)



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
                dcICR700.POINT_WR = CType(Me.txtSRTPO.Text, Integer).ToString.PadLeft(5, "0"c)
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
                sql.Append(" MANNO = '" & Me.txtNCSNO.Text & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    strMsg = "金額サマリの更新に失敗しました。"
                    Return False
                End If


                strMANNO = Me.txtNCSNO.Text
                intZANAKN = CType(dcICR700.ZANKN, Integer)
                intZANBKN = intV31ZANKN
                intPREZANAKN = CType(dcICR700.PREZANKN, Integer)
                intPREZANBKN = intV31PREZANKN
                intZANAPO = CType(CType(Me.txtSRTPO.Text, Integer).ToString.PadLeft(5, "0"c), Integer)
                intZANBPO = CType(CType(Me.txtSRTPO.Text, Integer).ToString.PadLeft(5, "0"c), Integer)
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

                    rePrint.strManno = Me.txtNCSNO.Text
                    rePrint.strccsname = Me.txtCCSNAME.Text

                    rePrint.intzankingaku = intV31ZANKN + intV31PREZANKN
                    rePrint.intzanpoint = CType(Me.txtSRTPO.Text, Integer)

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
                strSQL2 &= "'" & Me.txtNCSNO.Text & "',"
                '顧客種別
                strSQL1 &= "KSBKB,"
                strSQL2 &= "'" & Me.cmbNCSRANK.SelectedIndex & "',"
                'スクール生番号
                strSQL1 &= "SCLMANNO,"
                strSQL2 &= "'" & Me.txtDSCLMANNO.Text & "',"
                'スクール生区分
                strSQL1 &= "SCLKBN,"
                strSQL2 &= "'" & _strSCLKBN & "',"
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
                strSQL2 &= "'" & Me.txtNCSNO.Text & "',"
                '顧客種別
                strSQL1 &= "KSBKB,"
                strSQL2 &= "'" & Me.cmbNCSRANK.SelectedIndex & "',"
                'スクール生番号
                strSQL1 &= "SCLMANNO,"
                strSQL2 &= "'" & Me.txtDSCLMANNO.Text & "',"
                'スクール生区分
                strSQL1 &= "SCLKBN,"
                strSQL2 &= "'" & _strSCLKBN & "',"
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
