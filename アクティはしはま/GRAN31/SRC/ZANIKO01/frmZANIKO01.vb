Imports TECHNO.DataBase
Imports System.Threading

Public Class frmZANIKO01

#Region "▼宣言部"
    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "ZANIKO01"

    Private _dtCSMAST As DataTable

    Private _blnJIKKO As Boolean = False
    ''' <summary>
    ''' ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnPreCard As Boolean = False
    ''' <summary>
    ''' 更新球数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intUseKINGAKU As Integer = 0
    ''' <summary>
    ''' 支払金額
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPayBallZANKN As Integer = 0
    ''' <summary>
    ''' 支払プレミアム
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPayBallPREMKN As Integer = 0

    ''' <summary>
    ''' 加算情報クリアフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnClrFlg As Boolean = False

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

    Private _dtHINMTA As DataTable
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "残高移行画面"

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

            MyBase.l_Title_FormName = "残高移行画面"

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
    ''' プリペイド移行金額保持
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property PreKingaku As Integer
        Get
            Return _intPreKingaku
        End Get
        Set(ByVal value As Integer)
            _intPreKingaku = value
        End Set
    End Property
    Private _intPreKingaku As Integer = 0
#End Region



#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmZANIKO01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    Private Sub frmZANIKO01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.Refresh()

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
                    Me.Close()
                End If
            End If
            '*** 【スタッフ確認】 ***'

            '*** KU-A201リーダライター接続 ***'
            Dim blnErr As Boolean = False
            dcIA240 = New TECHNO.DeviceControls.IA240Control
            dcIA240.Open(UIUtility.SYSTEM.IA240COM)
            Do
                'ソフトウェアリセット
                If Not dcIA240.CL_Command Then
                    blnErr = True
                    Exit Do
                End If
                Thread.Sleep(700)
                '記録密度設定
                If Not dcIA240.BP_Command Then
                    blnErr = True
                    Exit Do
                End If
                Thread.Sleep(700)
                'LED設定
                If Not dcIA240.LE_Command Then
                    blnErr = True
                    Exit Do
                End If
                Exit Do
            Loop

            If blnErr Then
                Me.btnPreCard1.Enabled = False
                'Using frm As New frmMSGBOX01("プリペイドリーダライターとの接続に失敗しました。", 2)
                '    frm.ShowDialog()
                'End Using
                'Me.Close()
            End If

            'カード発行料情報確認
            If Not GetHakkoInfo() Then
                Me.lblHakkoInfo.Visible = True
                Me.lblHakkoKin.Visible = False
            End If

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
            '顧客情報ﾃｰﾌﾞﾙ削除
            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Dispose()
            End If
            '商品マスタテーブル削除
            If Not _dtHINMTA Is Nothing Then
                _dtHINMTA.Dispose()
            End If

            'KU-A201リーダライターポートクローズ
            dcIA240.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行金額から発行料の精算を行うチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkHakko_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHakko.CheckedChanged
        Try

            Me.chkHakko.ForeColor = Color.Black

            If Me.chkHakko.Checked Then
                Me.chkHakko.ForeColor = Color.Red

                Me.lblUpZANKN.Text = (CType(Me.lblUpZANKN.Text, Integer) - CType(Me.chkHakko.Tag.ToString, Integer)).ToString("#,##0")
                Me.lblUpPREMKN.Text = (CType(Me.lblUpPREMKN.Text, Integer) + CType(Me.chkHakko.Tag.ToString, Integer)).ToString("#,##0")
                Me.lblZANKN3.Text = (CType(Me.lblZANKN2.Text, Integer) + CType(Me.lblUpZANKN.Text, Integer)).ToString("#,##0")
                Me.lblPREM3.Text = (CType(Me.lblPREM2.Text, Integer) + CType(Me.lblUpPREMKN.Text, Integer)).ToString("#,##0")
            Else

                Me.lblUpZANKN.Text = (CType(Me.lblUpZANKN.Text, Integer) + CType(Me.chkHakko.Tag.ToString, Integer)).ToString("#,##0")
                Me.lblUpPREMKN.Text = (CType(Me.lblUpPREMKN.Text, Integer) - CType(Me.chkHakko.Tag.ToString, Integer)).ToString("#,##0")
                Me.lblZANKN3.Text = (CType(Me.lblZANKN2.Text, Integer) + CType(Me.lblUpZANKN.Text, Integer)).ToString("#,##0")
                Me.lblPREM3.Text = (CType(Me.lblPREM2.Text, Integer) + CType(Me.lblUpPREMKN.Text, Integer)).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' レシートを出力するチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkRECEIPT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRECEIPT.CheckedChanged
        Try
            Me.chkRECEIPT.ForeColor = Color.Black

            If Me.chkRECEIPT.Checked Then
                Me.chkRECEIPT.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行履歴ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnIKOHIST01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIKOHIST01.Click
        Try
            Using frm As New frmIKOHIST01(iDatabase)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try

            Using frm As New frmMSGBOX01("クリアしてよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '【移行元カード情報】
            '顧客番号
            Me.lblNCSNO1.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME1.Text = String.Empty
            '氏名
            Me.lblCCSNAME1.Text = String.Empty
            '氏名ｶﾅ
            Me.lblCCSKANA1.Text = String.Empty
            '残金額
            Me.lblZANKN1.Text = String.Empty
            '残プレミアム
            Me.lblPREM1.Text = String.Empty
            '残ポイント
            Me.lblSRTPO1.Text = String.Empty

            '【移行先カード情報】
            '顧客番号
            Me.lblNCSNO2.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME2.Text = String.Empty
            '氏名
            Me.lblCCSNAME2.Text = String.Empty
            '氏名ｶﾅ
            Me.lblCCSKANA2.Text = String.Empty
            '残金額
            Me.lblZANKN2.Text = String.Empty
            '残プレミアム
            Me.lblPREM2.Text = String.Empty
            '残ポイント
            Me.lblSRTPO2.Text = String.Empty

            '【移行後カード情報】
            '加算金額
            Me.lblUpZANKN.Text = "0"
            '加算ﾌﾟﾚﾐｱﾑ
            Me.lblUpPREMKN.Text = "0"
            '加算ﾎﾟｲﾝﾄ
            Me.lblUpSRTPO.Text = "0"

            '残金額
            Me.lblZANKN3.Text = "0"
            '残プレミアム
            Me.lblPREM3.Text = "0"
            '残ポイント
            Me.lblSRTPO3.Text = "0"

            Me.btnCard1.Enabled = False
            Me.btnCard1.Visible = True

            '【移行先カード情報】
            Me.btnCard2.Enabled = False

            _blnJIKKO = False

            _intPreKingaku = 0

            Me.btnClear.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行元ﾌﾟﾘﾍﾟｲﾄﾞカード挿入ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPreCard1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreCard1.Click
        Dim blnCANCEL As Boolean = False
        Dim blnERRSHOPFLG As Boolean = False
        Dim blnERRFLG As Boolean = False
        Try
            If CType(Me.btnPreCard1.Tag, Integer).Equals(0) Then
                '【カード挿入】

                If _blnClrFlg Then
                    Me.lblUpZANKN.Text = "0"
                    Me.lblUpPREMKN.Text = "0"
                    Me.lblUpSRTPO.Text = "0"

                    Me.lblZANKN3.Text = "0"
                    Me.lblPREM3.Text = "0"
                    Me.lblSRTPO3.Text = "0"

                    '顧客番号
                    Me.lblNCSNO2.Text = String.Empty
                    '顧客種別
                    Me.lblCKBNAME2.Text = String.Empty
                    '氏名
                    Me.lblCCSNAME2.Text = String.Empty
                    '氏名ｶﾅ
                    Me.lblCCSKANA2.Text = String.Empty
                    '残金額
                    Me.lblZANKN2.Text = String.Empty
                    '残プレミアム
                    Me.lblPREM2.Text = String.Empty
                    '残ポイント
                    Me.lblSRTPO2.Text = String.Empty

                    _blnClrFlg = False
                End If

                Using frm As New frmREQUESTCARD(dcIA240)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.PREREAD
                    frm.ShowDialog()
                    blnCANCEL = frm.CANCEL
                    blnERRSHOPFLG = frm.ERRSHOPFLG
                    blnERRFLG = frm.ERRFLG
                End Using

                '取消押された
                If blnCANCEL Then Exit Sub
                'エラー発生
                If blnERRFLG Then Exit Sub

                _blnPreCard = True

                '店番エラー発生
                If blnERRSHOPFLG Then
                    Using frm As New frmMSGBOX01("店番が一致しません。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                If CType(dcIA240.KINGAKU, Integer).Equals(0) Then
                    Using frm As New frmMSGBOX01("残高がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject1.Enabled = True
                    Me.btnEject1.PerformClick()
                    Exit Sub
                End If

                '顧客番号
                Me.lblNCSNO1.Text = "00000000"
                '顧客種別
                Me.lblCKBNAME1.Text = String.Empty
                '氏名
                Me.lblCCSNAME1.Text = "プリペイドカード"
                '氏名ｶﾅ
                Me.lblCCSKANA1.Text = String.Empty


                '残金額
                Me.lblZANKN1.Text = CType(dcIA240.KINGAKU, Integer).ToString("#,##0")
                '残プレミアム
                Me.lblPREM1.Text = "0"
                '残ポイント
                Me.lblSRTPO1.Text = "0"

                'カード排出ボタン
                Me.btnEject1.Enabled = True

                Me.btnPreCard1.Text = "実行"
                Me.btnPreCard1.Tag = 1
                Me.btnCard1.Enabled = False

            Else
                '【実行】

                Me.btnPreCard1.Text = "ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞ挿入"
                Me.btnPreCard1.Tag = 0
                Me.btnPreCard1.Enabled = True

                Me.btnEject1.Enabled = False

                '【プリカRW書き込み情報セット】
                '店番号
                dcIA240.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
                'パスワード
                dcIA240.PASSCD_WR = dcIA240.PASSCD
                'シリアルナンバー
                dcIA240.SERIALNO_WR = dcIA240.SERIALNO
                '種別
                dcIA240.SYUBETU_WR = dcIA240.SYUBETU
                '金額
                dcIA240.KINGAKU_WR = "00000"
                '予備
                dcIA240.YOBI_WR = dcIA240.YOBI

                '【カード書き込み】
                Using frm As New frmREQUESTCARD(dcIA240)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.PREWRITE
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                '加算情報
                Me.lblUpZANKN.Text = (CType(Me.lblUpZANKN.Text, Integer) + CType(Me.lblZANKN1.Text, Integer)).ToString("#,##0")
                Me.lblUpPREMKN.Text = (CType(Me.lblUpPREMKN.Text, Integer) + CType(Me.lblPREM1.Text, Integer)).ToString("#,##0")
                Me.lblUpSRTPO.Text = (CType(Me.lblUpSRTPO.Text, Integer) + CType(Me.lblSRTPO1.Text, Integer)).ToString("#,##0")

                Me.btnCard1.Enabled = True


                Me.btnCard1.Visible = True

                '【移行先カード情報】
                Me.btnCard2.Enabled = True

                _blnJIKKO = True


            End If






        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行元カード挿入ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard1.Click
        Dim blnCANCEL As Boolean = False
        Dim blnERRSHOPFLG As Boolean = False
        Dim blnERRFLG As Boolean = False
        Dim intERRRWKBN As Integer = 0
        Try
            _blnPreCard = False

            If CType(Me.btnCard1.Tag, Integer).Equals(0) Then
                '【カード挿入】

                If _blnClrFlg Then
                    Me.lblUpZANKN.Text = "0"
                    Me.lblUpPREMKN.Text = "0"
                    Me.lblUpSRTPO.Text = "0"

                    Me.lblZANKN3.Text = "0"
                    Me.lblPREM3.Text = "0"
                    Me.lblSRTPO3.Text = "0"

                    '顧客番号
                    Me.lblNCSNO2.Text = String.Empty
                    '顧客種別
                    Me.lblCKBNAME2.Text = String.Empty
                    '氏名
                    Me.lblCCSNAME2.Text = String.Empty
                    '氏名ｶﾅ
                    Me.lblCCSKANA2.Text = String.Empty
                    '残金額
                    Me.lblZANKN2.Text = String.Empty
                    '残プレミアム
                    Me.lblPREM2.Text = String.Empty
                    '残ポイント
                    Me.lblSRTPO2.Text = String.Empty

                    _blnClrFlg = False
                End If

                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ERRSKIPV31 = True
                    frm.ShowDialog()
                    blnCANCEL = frm.CANCEL
                    blnERRSHOPFLG = frm.ERRSHOPFLG
                    blnERRFLG = frm.ERRFLG
                    intERRRWKBN = frm.ERRRWKBN
                End Using

                '取消押された
                If blnCANCEL Then Exit Sub
                'エラー発生
                If blnERRFLG Then
                    Exit Sub
                End If

                '店番エラー発生
                If blnERRSHOPFLG Then
                    Using frm As New frmMSGBOX01("店番が一致しません。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If

                If CType(dcICR700.KINGAKU, Integer).Equals(0) And CType(dcICR700.POINT, Integer).Equals(0) Then
                    Using frm As New frmMSGBOX01("残高がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject1.Enabled = True
                    Me.btnEject1.PerformClick()
                    Exit Sub
                End If

                '【顧客情報あり】

                If Not GetCSMAST() Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject1.Enabled = True
                    Me.btnEject1.PerformClick()
                    Exit Sub
                End If
                '顧客番号
                Me.lblNCSNO1.Text = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)

                '顧客種別
                Me.lblCKBNAME1.Text = _dtCSMAST.Rows(0).Item("CKBNAME").ToString
                '氏名
                Me.lblCCSNAME1.Text = _dtCSMAST.Rows(0).Item("CCSNAME").ToString
                '氏名ｶﾅ
                Me.lblCCSKANA1.Text = _dtCSMAST.Rows(0).Item("CCSKANA").ToString

                'カード番号
                Me.lblCARDNO.Text = dcICR700.NCSNO


                '【残金整合処理】
                'V31残金
                Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
                'V31残プレミアム
                Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)
                UIFunction.ZANSEIGO(intV31ZANKN, intV31PREZANKN, CType(dcICR700.KINGAKU, Integer), _intUseKINGAKU, _intPayBallZANKN, _intPayBallPREMKN)


                '残金額
                Me.lblZANKN1.Text = intV31ZANKN.ToString("#,##0")
                '残プレミアム
                Me.lblPREM1.Text = intV31PREZANKN.ToString("#,##0")
                '残ポイント
                Me.lblSRTPO1.Text = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer).ToString("#,##0")

                'カード排出ボタン
                Me.btnEject1.Text = "キャンセル"
                Me.btnEject1.Enabled = True
                'ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞ挿入ボタン
                Me.btnPreCard1.Enabled = False

                Me.btnCard1.Text = "実行"
                Me.btnCard1.Tag = 1
            Else
                '【実行】

                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ERRSKIPV31 = True
                    frm.ShowDialog()
                    blnCANCEL = frm.CANCEL
                    blnERRSHOPFLG = frm.ERRSHOPFLG
                    blnERRFLG = frm.ERRFLG
                    intERRRWKBN = frm.ERRRWKBN
                End Using

                '取消押された
                If blnCANCEL Then Exit Sub
                'エラー発生
                If blnERRFLG Then
                    Exit Sub
                End If

                If Not Me.lblCARDNO.Text.Equals(dcICR700.NCSNO) Then
                    Using frm As New frmMSGBOX01("受付けたカードと異なります。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If


                '書き込む前の前回来場セット(ボールトラン更新用)
                Dim strZENENTDATE As String = dcICR700.ZENENTDATE
                If dcICR700.ZENENTDATE.Equals("00000000") Then
                    strZENENTDATE = UIUtility.SYSTEM.UPDDAY
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
                dcICR700.ZANKN_WR = "00000"
                'P残金額
                dcICR700.PREZANKN_WR = "00000"
                '残ポイント
                dcICR700.POINT_WR = "00000"
                '前回来場日
                dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
                '入場区分
                dcICR700.ENTKBN_WR = dcICR700.ENTKBN
                'ボール単価
                dcICR700.BALLKIN_WR = dcICR700.BALLKIN
                '打席番号
                dcICR700.SEATNO_WR = dcICR700.SEATNO

                'トランザクション開始
                iDatabase.BeginTransaction()

                '金額サマリ・ﾎﾟｲﾝﾄｻﾏﾘ更新
                If Not UpdKINSMA(1) Then
                    Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject1.PerformClick()
                    Exit Sub
                End If

                'ボールトラン更新
                Dim intZANAKN As Integer = 0
                Dim intZANBKN As Integer = 0
                Dim intPREZANAKN As Integer = 0
                Dim intPREZANBKN As Integer = 0
                Dim intZANAPO As Integer = 0
                Dim intZANBPO As Integer = 0

                intZANAKN = CType(dcICR700.ZANKN, Integer)
                intZANBKN = CType(dcICR700.ZANKN, Integer) - _intPayBallZANKN
                intPREZANAKN = CType(dcICR700.PREZANKN, Integer)
                intPREZANBKN = CType(dcICR700.PREZANKN, Integer) - _intPayBallPREMKN
                intZANAPO = CType(dcICR700.POINT, Integer)
                intZANBPO = CType(dcICR700.POINT, Integer)

                If Not _intUseKINGAKU.Equals(0) Then
                    If Not UIFunction.UpdBALLTRN(CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer), dcICR700.NKBNO, strZENENTDATE, _intUseKINGAKU, _intPayBallZANKN, _intPayBallPREMKN, CType(dcICR700.BALLKIN, Integer) _
                              , intZANAKN, intZANBKN, intPREZANAKN, intPREZANBKN, intZANAPO, intZANBPO, iDatabase) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("ボールトランの更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If
                End If

                '残高移行データ更新
                If Not UpdIKOTRN(1) Then
                    Using frm As New frmMSGBOX01("残高移行データの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject2.PerformClick()
                    Exit Sub
                End If

                '【カード書き込み】
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If

                'コミット
                iDatabase.Commit()

                '加算情報
                Me.lblUpZANKN.Text = (CType(Me.lblUpZANKN.Text, Integer) + CType(Me.lblZANKN1.Text, Integer)).ToString("#,##0")
                Me.lblUpPREMKN.Text = (CType(Me.lblUpPREMKN.Text, Integer) + CType(Me.lblPREM1.Text, Integer)).ToString("#,##0")
                Me.lblUpSRTPO.Text = (CType(Me.lblUpSRTPO.Text, Integer) + CType(Me.lblSRTPO1.Text, Integer)).ToString("#,##0")

                Me.btnCard1.Enabled = False
                Me.btnPreCard1.Enabled = False
                Me.btnEject1.Enabled = False

                '【移行先カード情報】
                Me.btnCard2.Enabled = True

                _blnJIKKO = True
            End If






        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行先カード挿入ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCard2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard2.Click
        Dim blnCANCEL As Boolean = False
        Dim blnERRSHOPFLG As Boolean = False
        Dim blnERRFLG As Boolean = False
        Try
            If CType(Me.btnCard2.Tag, Integer).Equals(0) Then
                '【カード挿入】

                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ShowDialog()
                    blnCANCEL = frm.CANCEL
                    blnERRSHOPFLG = frm.ERRSHOPFLG
                    blnERRFLG = frm.ERRFLG
                End Using

                '取消押された
                If blnCANCEL Then Exit Sub
                'エラー発生
                If blnERRFLG Then Exit Sub
                '店番エラー発生
                If blnERRSHOPFLG Then
                    Using frm As New frmMSGBOX01("店番が一致しません。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If

                If Not GetCSMAST() Then
                    Using frm As New frmMSGBOX01("顧客情報がありません。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject2.Enabled = True
                    Me.btnEject2.PerformClick()
                    Exit Sub
                End If
                '顧客番号
                Me.lblNCSNO2.Text = _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)

                '顧客種別
                Me.lblCKBNAME2.Text = _dtCSMAST.Rows(0).Item("CKBNAME").ToString
                '氏名
                Me.lblCCSNAME2.Text = _dtCSMAST.Rows(0).Item("CCSNAME").ToString
                '氏名ｶﾅ
                Me.lblCCSKANA2.Text = _dtCSMAST.Rows(0).Item("CCSKANA").ToString

                'カード番号
                Me.lblCARDNO.Text = dcICR700.NCSNO


                '【残金整合処理】
                'V31残金
                Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
                'V31残プレミアム
                Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)
                UIFunction.ZANSEIGO(intV31ZANKN, intV31PREZANKN, CType(dcICR700.KINGAKU, Integer), _intUseKINGAKU, _intPayBallZANKN, _intPayBallPREMKN)


                '残金額
                Me.lblZANKN2.Text = intV31ZANKN.ToString("#,##0")
                '残プレミアム
                Me.lblPREM2.Text = intV31PREZANKN.ToString("#,##0")
                '残ポイント
                Me.lblSRTPO2.Text = CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer).ToString("#,##0")

                '【移行後カード情報】
                '残金額
                Me.lblZANKN3.Text = (intV31ZANKN + CType(Me.lblUpZANKN.Text, Integer)).ToString("#,##0")
                '残プレミアム
                Me.lblPREM3.Text = (intV31PREZANKN + CType(Me.lblUpPREMKN.Text, Integer)).ToString("#,##0")
                '残ポイント
                Me.lblSRTPO3.Text = (CType(_dtCSMAST.Rows(0).Item("SRTPO").ToString.PadLeft(5, "0"c), Integer) + CType(Me.lblUpSRTPO.Text, Integer)).ToString("#,##0")

                'カード排出ボタン
                Me.btnEject2.Enabled = True

                Me.lblErr.Visible = False
                Me.lblZANKN3.ForeColor = Color.Blue
                Me.lblPREM3.ForeColor = Color.Blue
                Me.lblSRTPO3.ForeColor = Color.Blue
                If (CType(Me.lblZANKN3.Text, Integer) + CType(Me.lblPREM3.Text, Integer)) > UIUtility.SYSTEM.ZANMAX Then
                    Me.lblErr.Visible = True
                    Me.lblZANKN3.ForeColor = Color.Red
                    Me.lblPREM3.ForeColor = Color.Red
                    Me.btnCard2.Enabled = False
                    Exit Sub
                End If
                If CType(Me.lblSRTPO3.Text, Integer) > 99999 Then
                    Me.lblErr.Visible = True
                    Me.lblSRTPO3.ForeColor = Color.Red
                    Me.btnCard2.Enabled = False
                    Exit Sub
                End If

                Me.btnPreCard1.Enabled = False
                Me.btnCard1.Enabled = False

                Me.btnCard2.Text = "実行"
                Me.btnCard2.Tag = 1

                If dcICR700.ENTKBN.Equals("1") And (Not Me.lblHakkoInfo.Visible) Then
                    Me.lblHakkoMsg.Visible = True
                    Me.lblHakkoKin.Visible = True
                    Me.chkHakko.Checked = True
                    Me.chkHakko.Enabled = True
                Else
                    Me.lblHakkoMsg.Visible = False
                    Me.lblHakkoKin.Visible = False
                    Me.chkHakko.Checked = False
                    Me.chkHakko.Enabled = False
                End If

            Else
                '【実行】
                If CType(Me.lblZANKN3.Text, Integer) < 0 Then
                    Using frm As New frmMSGBOX01("金額が不足しています。" & vbCrLf & "発行料の精算はできません。", 3)
                        frm.ShowDialog()
                    End Using
                    Me.chkHakko.Checked = False
                    Exit Sub
                End If



                Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                    frm.ERRSKIPV31 = True
                    frm.ShowDialog()
                    blnCANCEL = frm.CANCEL
                    blnERRSHOPFLG = frm.ERRSHOPFLG
                    blnERRFLG = frm.ERRFLG
                End Using

                '取消押された
                If blnCANCEL Then Exit Sub
                'エラー発生
                If blnERRFLG Then
                    Exit Sub
                End If

                If Not Me.lblCARDNO.Text.Equals(dcICR700.NCSNO) Then
                    Using frm As New frmMSGBOX01("受付けたカードと異なります。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If


                '【セクタ２情報セット】
                '店番号
                dcICR700.SHOPNO_WR = UIUtility.SYSTEM.SHOPNO
                'パスワード
                dcICR700.PASSCD_WR = dcICR700.PASSCD
                'シリアルナンバー
                dcICR700.SERIALNO_WR = dcICR700.SERIALNO
                '種別
                dcICR700.SYUBETU_WR = dcICR700.SYUBETU
                '金額
                dcICR700.KINGAKU_WR = (CType(Me.lblZANKN3.Text, Integer) + CType(Me.lblPREM3.Text, Integer)).ToString.PadLeft(5, "0"c)
                '予備
                dcICR700.YOBI_WR = dcICR700.YOBI

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
                dcICR700.ZANKN_WR = CType(Me.lblZANKN3.Text, Integer).ToString.PadLeft(5, "0"c)
                'P残金額
                dcICR700.PREZANKN_WR = CType(Me.lblPREM3.Text, Integer).ToString.PadLeft(5, "0"c)
                '残ポイント
                dcICR700.POINT_WR = CType(Me.lblSRTPO3.Text, Integer).ToString.PadLeft(5, "0"c)
                '前回来場日
                dcICR700.ZENENTDATE_WR = dcICR700.ZENENTDATE
                '入場区分
                If Me.chkHakko.Checked Then
                    dcICR700.ENTKBN_WR = "0"
                Else
                    dcICR700.ENTKBN_WR = dcICR700.ENTKBN
                End If
                'ボール単価
                dcICR700.BALLKIN_WR = dcICR700.BALLKIN
                '打席番号
                dcICR700.SEATNO_WR = dcICR700.SEATNO

                'トランザクション開始
                iDatabase.BeginTransaction()

                '金額サマリ・ﾎﾟｲﾝﾄｻﾏﾘ更新
                If Not UpdKINSMA() Then
                    Using frm As New frmMSGBOX01("金額サマリの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject2.PerformClick()
                    Exit Sub
                End If

                '残高移行データ更新
                If Not UpdIKOTRN() Then
                    Using frm As New frmMSGBOX01("残高移行データの更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Me.btnEject2.PerformClick()
                    Exit Sub
                End If

                If Me.chkHakko.Checked Then
                    '売上データ更新
                    If Not UpdURIAGE() Then
                        Using frm As New frmMSGBOX01("売上データの更新に失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Me.btnEject2.PerformClick()
                        Exit Sub
                    End If
                End If

                '【カード書き込み】
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                    frm.ShowDialog()
                    blnERRFLG = frm.ERRFLG
                End Using
                If blnERRFLG Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                        frm.ShowDialog()
                        Exit Sub
                    End Using
                End If

                'コミット
                iDatabase.Commit()

                Dim dtGOODS As DataTable ' 売上商品一覧
                dtGOODS = New DataTable("GOODS")
                dtGOODS.Columns.Add("GDSNAME", GetType(String))
                dtGOODS.Columns.Add("GDSCOUNT", GetType(String))
                dtGOODS.Columns.Add("GDSTAX", GetType(String))
                dtGOODS.Columns.Add("GDSKIN", GetType(String))
                dtGOODS.Columns.Add("CPAYKBN", GetType(String))

                Dim intHAKKOKIN As Integer = 0
                If Me.chkHakko.Checked Then
                    intHAKKOKIN = CType(Me.chkHakko.Tag, Integer)
                End If

                Dim dr As DataRow
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行 残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = (CType(Me.lblUpZANKN.Text, Integer) + intHAKKOKIN).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行 P)残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = (CType(Me.lblUpPREMKN.Text, Integer) - intHAKKOKIN).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行 POINT"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblUpSRTPO.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)

                '***
                If intHAKKOKIN > 0 Then
                    dr = dtGOODS.NewRow
                    dr("GDSNAME") = "発行料精算"
                    dr("GDSCOUNT") = 1
                    dr("GDSTAX") = 0
                    dr("GDSKIN") = CType(Me.chkHakko.Tag, Integer).ToString("#,##0")
                    dr("CPAYKBN") = String.Empty
                    dtGOODS.Rows.Add(dr)
                    dr = dtGOODS.NewRow
                    dr("GDSNAME") = "ﾌﾟﾚﾐｱﾑ還元"
                    dr("GDSCOUNT") = 1
                    dr("GDSTAX") = 0
                    dr("GDSKIN") = CType(Me.chkHakko.Tag, Integer).ToString("#,##0")
                    dr("CPAYKBN") = String.Empty
                    dtGOODS.Rows.Add(dr)
                    dr = dtGOODS.NewRow
                    dr("GDSNAME") = String.Empty
                    dr("GDSCOUNT") = String.Empty
                    dr("GDSTAX") = String.Empty
                    dr("GDSKIN") = String.Empty
                    dr("CPAYKBN") = String.Empty
                    dtGOODS.Rows.Add(dr)
                End If
                '***

                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行前 残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblZANKN2.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行前 P)残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblPREM2.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行前 POINT"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblSRTPO2.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)

                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行後 残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblZANKN3.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行後 P)残金額"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblPREM3.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)
                dr = dtGOODS.NewRow
                dr("GDSNAME") = "移行後 POINT"
                dr("GDSCOUNT") = 1
                dr("GDSTAX") = 0
                dr("GDSKIN") = CType(Me.lblSRTPO3.Text, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dtGOODS.Rows.Add(dr)

                '【レシート印刷】
                Dim rePrint As New TMT90.Receipt

                rePrint.intGetPremKn = CType(Me.lblPREM1.Text, Integer)
                rePrint.intGetPoint = CType(Me.lblSRTPO1.Text, Integer)

                rePrint.intPrintKbn = 3
                rePrint.strManno = Me.lblNCSNO2.Text        '顧客番号
                rePrint.strccsname = Me.lblCCSNAME2.Text    '氏名
                Dim dtmInsDt As DateTime = Now
                rePrint.insDTTM = dtmInsDt
                rePrint.dtGoods = dtGOODS
                rePrint.strHostName = Net.Dns.GetHostName
                rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)

                If Me.chkRECEIPT.Checked Then rePrint.RePrint(False, String.Empty)


                Me.btnEject2.Enabled = False
                Me.btnCard2.Text = "ICｶｰﾄﾞ確認"
                Me.btnCard2.Tag = 0
                Me.btnCard2.Enabled = False

                '【移行元カード情報】
                Me.btnPreCard1.Text = "ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞ挿入"
                Me.btnPreCard1.Tag = 0
                Me.btnPreCard1.Enabled = True
                Me.btnCard1.Text = "ICｶｰﾄﾞ確認"
                Me.btnCard1.Tag = 0
                Me.btnCard1.Enabled = True

                Me.btnPreCard1.Visible = True
                Me.btnCard1.Visible = True

                _blnClrFlg = True

                _blnJIKKO = False

                _intPreKingaku = 0

                Me.Close()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行元カード排出ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEject1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEject1.Click
        Dim blnERRFLG As Boolean = False
        Try
            If _blnPreCard Then
                Do
                    For i As Integer = 1 To 5
                        If dcIA240.CO_Command Then
                            Exit Do
                        End If
                        If i.Equals(5) Then
                            Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
                                frm.ShowDialog()
                            End Using
                            Exit Sub
                        End If
                    Next
                Loop

            Else
                'カード排出コマンド
                Using frm As New frmREQUESTCARD(dcICR700)
                    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                    If _blnPreCard Then frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
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


            Me.btnPreCard1.Visible = True
            Me.btnCard1.Visible = True

            If Not _blnJIKKO Then
                Me.btnPreCard1.Text = "ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞ挿入"
                Me.btnPreCard1.Tag = 0
                Me.btnPreCard1.Enabled = True
                Me.btnCard1.Text = "ICｶｰﾄﾞ確認"
                Me.btnCard1.Tag = 0
                Me.btnCard1.Enabled = True
            End If

            If CType(Me.btnPreCard1.Tag, Integer).Equals(1) Then
                Me.btnPreCard1.Text = "ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞ挿入"
                Me.btnPreCard1.Tag = 0
                Me.btnPreCard1.Enabled = True
            End If

            Me.btnCard1.Enabled = True
            Me.btnEject1.Text = "カード排出"
            Me.btnEject1.Enabled = False

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 移行先カード排出ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEject2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEject2.Click
        Dim blnERRFLG As Boolean = False
        Try
            ''カード排出コマンド
            'Using frm As New frmREQUESTCARD(dcICR700)
            '    frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
            '    frm.ShowDialog()
            '    blnERRFLG = frm.ERRFLG
            'End Using
            'If blnERRFLG Then
            '    Using frm As New frmMSGBOX01("カード排出に失敗しました。", 2)
            '        frm.ShowDialog()
            '        Exit Sub
            '    End Using
            'End If

            Me.btnCard2.Text = "ICｶｰﾄﾞ確認"
            Me.btnCard2.Tag = 0
            Me.btnCard2.Enabled = True

            Me.btnEject2.Enabled = False

            If CType(Me.btnCard1.Tag, Integer).Equals(0) Then
                Me.btnPreCard1.Enabled = True
            End If

            If Me.chkHakko.Enabled Then
                Me.chkHakko.Checked = False
                Me.chkHakko.Enabled = False
                Me.lblZANKN3.Text = "0"
                Me.lblPREM3.Text = "0"
                Me.lblSRTPO3.Text = "0"
                Me.lblHakkoMsg.Visible = False
                Me.lblHakkoKin.Visible = False
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 終了ボタン
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func1()
        Try
            If _blnJIKKO Then
                Using frm As New frmMSGBOX01("残高移行中ですが終了してよろしいですか？", 1)
                    frm.ShowDialog()
                    If Not frm.Reply Then
                        Exit Sub
                    End If
                End Using
            End If
            _intPreKingaku = CType(Me.lblUpPREMKN.Text, Integer)

            Me.btnEject1.PerformClick()
            Me.btnEject2.PerformClick()

            Me.Close()

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

            '【移行元カード情報】
            '顧客番号
            Me.lblNCSNO1.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME1.Text = String.Empty
            '氏名
            Me.lblCCSNAME1.Text = String.Empty
            '氏名ｶﾅ
            Me.lblCCSKANA1.Text = String.Empty
            '残金額
            Me.lblZANKN1.Text = String.Empty
            '残プレミアム
            Me.lblPREM1.Text = String.Empty
            '残ポイント
            Me.lblSRTPO1.Text = String.Empty

            '【移行先カード情報】
            '顧客番号
            Me.lblNCSNO2.Text = String.Empty
            '顧客種別
            Me.lblCKBNAME2.Text = String.Empty
            '氏名
            Me.lblCCSNAME2.Text = String.Empty
            '氏名ｶﾅ
            Me.lblCCSKANA2.Text = String.Empty
            '残金額
            Me.lblZANKN2.Text = String.Empty
            '残プレミアム
            Me.lblPREM2.Text = String.Empty
            '残ポイント
            Me.lblSRTPO2.Text = String.Empty

            '【移行後カード情報】
            '加算金額
            Me.lblUpZANKN.Text = "0"
            '加算ﾌﾟﾚﾐｱﾑ
            Me.lblUpPREMKN.Text = "0"
            '加算ﾎﾟｲﾝﾄ
            Me.lblUpSRTPO.Text = "0"

            '残金額
            Me.lblZANKN3.Text = "0"
            '残プレミアム
            Me.lblPREM3.Text = "0"
            '残ポイント
            Me.lblSRTPO3.Text = "0"

            'レシート区分
            Me.chkRECEIPT.Checked = True
            'If UIUtility.SYSTEM.RECEIPTFLG.Equals(1) Then
            '    Me.chbRECEIPT.Checked = True
            'Else
            '    Me.chbRECEIPT.Checked = False
            'End If

            If _intPreKingaku > 0 Then
                '顧客番号
                Me.lblNCSNO1.Text = "00000000"
                '顧客種別
                Me.lblCKBNAME1.Text = String.Empty
                '氏名
                Me.lblCCSNAME1.Text = "プリペイドカード"
                '氏名ｶﾅ
                Me.lblCCSKANA1.Text = String.Empty


                '残金額
                Me.lblZANKN1.Text = "0"
                '残プレミアム
                Me.lblPREM1.Text = _intPreKingaku.ToString("#,##0")
                '残ポイント
                Me.lblSRTPO1.Text = "0"

                Me.lblUpPREMKN.Text = _intPreKingaku.ToString("#,##0")

                '【移行先カード情報】
                Me.btnCard2.Enabled = True
                btnCard1.Visible = False
                _blnJIKKO = True
            Else
                Me.btnClear.Visible = False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード発行料情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHakkoInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If Not _dtHINMTA Is Nothing Then
                _dtHINMTA.Clear()
            End If

            'カード発行料情報取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTA")
            sql.Append(" WHERE")
            sql.Append(" HINKB = '1'")

            _dtHINMTA = iDatabase.ExecuteRead(sql.ToString())

            If _dtHINMTA.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.lblHakkoKin.Text = "カード発行料金 " & CType(_dtHINMTA.Rows(0).Item("URIATK").ToString, Integer).ToString("#,##0") & "円"
            Me.chkHakko.Tag = CType(_dtHINMTA.Rows(0).Item("URIATK").ToString, Integer)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If Not _dtCSMAST Is Nothing Then
                _dtCSMAST.Clear()
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
            sql.Append(",E.SCLKBN")
            sql.Append(",F.USEKIN")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN DPOINTSMA AS C ON C.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN KBMAST AS D ON D.NKBNO = A.NCSRANK")
            sql.Append(" LEFT JOIN MANMST AS E ON E.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" LEFT JOIN BALLTRN AS F ON F.NCSNO = A.NCSNO AND UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" WHERE")
            sql.Append(" A.NCARDID = " & CType(dcICR700.NCSNO, Integer))

            _dtCSMAST = iDatabase.ExecuteRead(sql.ToString())

            If _dtCSMAST.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 金額サマリ・ﾎﾟｲﾝﾄｻﾏﾘ更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdKINSMA(Optional ByVal intKBN As Integer = 2) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            If intKBN.Equals(2) Then
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = " & CType(Me.lblZANKN3.Text, Integer))
                sql.Append(",PREZANKN = " & CType(Me.lblPREM3.Text, Integer))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If


                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = " & CType(Me.lblSRTPO3.Text, Integer))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Else
                sql.Clear()
                sql.Append("UPDATE KINSMA SET")
                sql.Append(" ZANKN = 0")
                sql.Append(",PREZANKN = 0")
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If


                sql.Clear()
                sql.Append("UPDATE DPOINTSMA SET")
                sql.Append(" SRTPO = 0")
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" MANNO = '" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "'")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If



            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 残高移行データ登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdIKOTRN(Optional ByVal intKBN As Integer = 2) As Boolean
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try

            '【新規】

            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO IKOTRN("
            strSQL2 &= " VALUES("
            '営業日付
            strSQL1 &= "IKODT,"
            strSQL2 &= "'" & Now.ToString("yyyyMMdd") & "',"
            '移行時間
            strSQL1 &= "IKOTIME,"
            strSQL2 &= "'" & Now.Hour.ToString.PadLeft(2, "0"c) & Now.Minute.ToString.PadLeft(2, "0"c) & "',"
            '顧客番号
            strSQL1 &= "NCSNO,"
            strSQL2 &= CType(_dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c), Integer) & ","
            If intKBN.Equals(2) Then
                '移行後残金
                strSQL1 &= "ZANKN,"
                strSQL2 &= CType(Me.lblZANKN3.Text, Integer) & ","
                '移行後残プレミアム
                strSQL1 &= "PREZANKN,"
                strSQL2 &= CType(Me.lblPREM3.Text, Integer) & ","
                '移行後残ポイント
                strSQL1 &= "SRTPO,"
                strSQL2 &= CType(Me.lblSRTPO3.Text, Integer) & ","
                '移行元顧客番号
                strSQL1 &= "MOTONCSNO,"
                strSQL2 &= CType(Me.lblNCSNO1.Text, Integer) & ","
                '移行元顧客種別
                strSQL1 &= "MOTOCKBNAME,"
                strSQL2 &= "'" & Me.lblCKBNAME1.Text & "',"
                '移行元氏名
                strSQL1 &= "MOTOCCSNAME,"
                strSQL2 &= "'" & Me.lblCCSNAME1.Text & "',"
                '移行元氏名ｶﾅ
                strSQL1 &= "MOTOCCSKANA,"
                strSQL2 &= "'" & Me.lblCCSKANA1.Text & "',"
                '移行元残金
                strSQL1 &= "MOTOZANKN,"
                strSQL2 &= CType(Me.lblUpZANKN.Text, Integer) & ","
                '移行元残プレミアム
                strSQL1 &= "MOTOPREZANKN,"
                strSQL2 &= CType(Me.lblUpPREMKN.Text, Integer) & ","
                '移行元残ポイント
                strSQL1 &= "MOTOSRTPO,"
                strSQL2 &= CType(Me.lblUpSRTPO.Text, Integer) & ","
            Else
                '移行後残金
                strSQL1 &= "ZANKN,"
                strSQL2 &= "0,"
                '移行後残プレミアム
                strSQL1 &= "PREZANKN,"
                strSQL2 &= "0,"
                '移行後残ポイント
                strSQL1 &= "SRTPO,"
                strSQL2 &= "0,"
                '移行元顧客番号
                strSQL1 &= "MOTONCSNO,"
                strSQL2 &= "0,"
                '移行元顧客種別
                strSQL1 &= "MOTOCKBNAME,"
                strSQL2 &= "NULL,"
                '移行元氏名
                strSQL1 &= "MOTOCCSNAME,"
                strSQL2 &= "NULL,"
                '移行元氏名ｶﾅ
                strSQL1 &= "MOTOCCSKANA,"
                strSQL2 &= "NULL,"
                '移行元残金
                strSQL1 &= "MOTOZANKN,"
                strSQL2 &= CType("-" & Me.lblZANKN1.Text, Integer) & ","
                '移行元残プレミアム
                strSQL1 &= "MOTOPREZANKN,"
                strSQL2 &= CType("-" & Me.lblPREM1.Text, Integer) & ","
                '移行元残ポイント
                strSQL1 &= "MOTOSRTPO,"
                strSQL2 &= CType("-" & Me.lblSRTPO1.Text, Integer) & ","
            End If
            'カード発行料
            strSQL1 &= "HAKKOKIN,"
            If Me.chkHakko.Checked Then
                strSQL2 &= CType(Me.chkHakko.Tag, Integer) & ","
            Else
                strSQL2 &= "0,"
            End If

            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= UIFunction.NullCheck(_strSTFCODE) & ","
            'スタッフ名
            strSQL1 &= "STFNAME,"
            strSQL2 &= UIFunction.NullCheck(_strSTFNAME) & ","
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"

            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
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
    ''' 売上データ登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdURIAGE() As Boolean
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim sql As New System.Text.StringBuilder
        Try

            '【伝票番号】
            sql.Clear()
            sql.Append("UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
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
                Return False
            End If

            '処理日時
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
            '部門コード【001】スクール【002】ベンダー
            strSQL1 &= "BMNCD,"
            strSQL2 &= "'002',"
            '分類コード１【001】商品
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'001',"
            '分類コード２ 
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'001',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'001',"
            '売上個数区分 
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'001',"
            '売上金額(入金額)
            strSQL1 &= "UDNKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) & ","
            '売上数
            strSQL1 &= "TKTSU,"
            strSQL2 &= "1,"
            '経理締日付
            strSQL1 &= "SMADT,"
            strSQL2 &= "'999',"
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
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) - CType(_dtHINMTA.Rows(0).Item("ZEITK").ToString, Integer) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("ZEITK").ToString, Integer) & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= "0,"
            '預かり金額
            strSQL1 &= "NYUKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) & ","
            'おつり
            strSQL1 &= "TURIKN,"
            strSQL2 &= "0,"
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "0,"
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
            strSQL2 &= "'4',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "NULL,"
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
            '分類コード１
            strSQL1 &= "BUNCDA,"
            strSQL2 &= "'001',"
            '分類コード２
            strSQL1 &= "BUNCDB,"
            strSQL2 &= "'" & _dtHINMTA.Rows(0).Item("BUNCDB").ToString & "',"
            '分類コード３
            strSQL1 &= "BUNCDC,"
            strSQL2 &= "'" & _dtHINMTA.Rows(0).Item("BUNCDC").ToString & "',"
            '売上個数区分
            strSQL1 &= "TKTKBN,"
            strSQL2 &= "'" & _dtHINMTA.Rows(0).Item("HINCD").ToString & "',"
            '売上名
            strSQL1 &= "TKTNMA,"
            strSQL2 &= "'カード発行料',"
            '売上金額
            strSQL1 &= "UDNKN,"
            strSQL2 &= _dtHINMTA.Rows(0).Item("URIBTK").ToString & ","
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
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) - CType(_dtHINMTA.Rows(0).Item("ZEITK").ToString, Integer) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("ZEITK").ToString, Integer) & ","
            '商品消費税区分【1】税抜【2】税込【9】非課税
            strSQL1 &= "HINZEIKB,"
            strSQL2 &= "'2',"
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= "0,"
            '固定区分
            strSQL1 &= "KOTEIKBN,"
            strSQL2 &= "0,"
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
            strSQL2 &= "'4',"
            'ホスト名
            strSQL1 &= "HOSTNAME,"
            strSQL2 &= "'" & Net.Dns.GetHostName & "',"
            'ドロア区分【0】未【1】済
            strSQL1 &= "DRAKBN,"
            strSQL2 &= "'0',"
            '品名
            strSQL1 &= "HINNMA,"
            strSQL2 &= "'カード発行料',"
            '取得プレミアム
            strSQL1 &= "PREMKN)"
            strSQL2 &= "0)"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
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
            strSQL2 &= "'" & _dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c) & "',"
            '税抜売上金額
            strSQL1 &= "UDNAKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) - CType(_dtHINMTA.Rows(0).Item("ZEITK").ToString, Integer) & ","
            '税込売上金額
            strSQL1 &= "UDNBKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("URIBTK").ToString, Integer) & ","
            '消費税
            strSQL1 &= "UDNZKN,"
            strSQL2 &= CType(_dtHINMTA.Rows(0).Item("ZEITK").ToString, Integer) & ","
            'ポイント
            strSQL1 &= "POINT,"
            strSQL2 &= "0,"
            '残金入金前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= CType(Me.lblZANKN2.Text, Integer) & ","
            '残金入金後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= CType(Me.lblZANKN3.Text, Integer) & ","
            'P)残金入金前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= CType(Me.lblPREM2.Text, Integer) & ","
            'P)残金入金後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= CType(Me.lblPREM3.Text, Integer) & ","
            '残ポイント入金前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= CType(Me.lblSRTPO2.Text, Integer) & ","
            '残ポイント入金後
            strSQL1 &= "ZANBPO,"
            strSQL2 &= CType(Me.lblSRTPO3.Text, Integer) & ","
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
                Return False
            End If


            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function

#End Region








End Class
