Imports TECHNO.DataBase
Imports System.ComponentModel
Imports TMT90

Public Class frmHINDISP01

    Dim _DEBUG As Boolean = False

#Region "▼宣言部"
    ''' <summary>
    ''' プログラムID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const c_strPRGID As String = "HINDISP01"

    ''' <summary>
    ''' 打席カード精算ボタン表示・非表示
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnCard1 As Boolean = False
    Private _blnCard2 As Boolean = False
    Private _blnCard3 As Boolean = False


    ' 更新日時
    Dim _dtUPDDTM_SEQTRN As DateTime

    ' 商品項目ボタンの配列
    Dim _btns(6, 16) As HINMSTButton

    ' ポイント付与率
    Dim _pointRate As Double

    ' 商品リスト
    Dim _list As BindingList(Of HINMTA_View) = New BindingList(Of HINMTA_View)

    ' 現在選択されている商品項目
    Dim _selectHINMTA As HINMSTButton

    ' 初期化完了フラグ
    Dim _bInited As Boolean

    ' グリッドビューのフォーカスフラグ
    Dim _dataGridViewFocused As Boolean

    ' カード挿入済み確認フラグ【True】挿入済み    
    Private _blnCardFLG As Boolean = False

    ' *** 定数 ***
    Private Const MAX_NUM_PAGE = 6 ' タブに表示される最大商品タグ数(手入力は含まない)
    Private Const MAX_NUM_ITEM = 16 ' タブページに表示される最大商品項目数
    Private Const TAX_PREFIX = "（内） " ' 内税の接頭文

    ''' <summary>
    ''' レシート発行フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _blnRECEIPT As Boolean = False

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

    ' レジ画面の戻り値
    Enum eResultRegistor
        SUCCESS       ' 成功
        FAILE         ' 失敗
        ZANKN_LOWER   ' カード残高不足
        DEPOSIT_LOWER ' 預り金不足
        CANCEL        ' 戻る
    End Enum


    '使用球数金額
    Private _intUseKINGAKU As Integer = 0
    Private _intPayBallZANKN As Integer = 0
    Private _intPayBallPREMKN As Integer = 0
    Private _strZENENTDATE As String
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品引き落とし"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品引き落とし"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod, ByVal ICR700 As TECHNO.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "商品引き落とし"

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

            MyBase.l_Title_FormName = "商品引き落とし"

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

#Region "▼プロパティ"

    Public WriteOnly Property NCSNO As String
        Set(ByVal value As String)
            _strNCSNO = value
        End Set
    End Property
    Private _strNCSNO As String = String.Empty

    Public WriteOnly Property CCSNAME As String
        Set(ByVal value As String)
            _strCCSNAME = value
        End Set
    End Property
    Private _strCCSNAME As String = String.Empty

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' 開始処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmENTHIST01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' 開始処理
            Start()

            ' 初期化完了
            _bInited = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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

            ' 開始処理
            Start()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 画面クリアボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClearDisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearDisp.Click
        Try
            _list.Clear()
            CalcPrice()
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 数量アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnItemNumUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemNumUp.Click
        Try
            Dim v As Integer
            If Int32.TryParse(txtItemNum.Text, v) Then
                If v < 99 Then
                    txtItemNum.Text = CStr(v + 1)
                End If
            End If
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 数量ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnItemNumDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemNumDown.Click
        Try
            Dim v As Integer
            If Int32.TryParse(txtItemNum.Text, v) Then
                If v > 0 Then
                    txtItemNum.Text = CStr(v - 1)
                End If
            End If
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 手入力_商品名入力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtHINNMA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHINNMA.TextChanged
        SetAllButtonsEnable()
    End Sub

    ''' <summary>
    ''' 手入力_金額入力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            If String.IsNullOrEmpty(Me.txtPrice.Text) Then Exit Sub
            Dim v As Integer
            If Int32.TryParse(txtPrice.Text, v) Then
                lblPOINT.Text = "(Point:" & GetPointRateInt(CInt(txtPrice.Text)).ToString.PadLeft(4, " "c) & ")"
                '*** Sta Add 2018/04/24 Kitahara
                Me.lblPOINT.Tag = GetPointRateInt(CInt(txtPrice.Text))
                '*** End Add 2018/04/24 Kitahara
            End If
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 商品項目ボタンを選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHINMASTButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _selectHINMTA = CType(sender, HINMSTButton)
            ' 選択したボタン以外を非選択
            For i = 0 To MAX_NUM_ITEM - 1
                Dim btn = _btns(tabHINDISP.SelectedIndex, i)
                If Not btn.Equals(_selectHINMTA) Then
                    btn.Selected = False
                End If
            Next
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' タブを切り替えたら前のタブページの選択ボタンをすべて解除する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tabHINDISP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabHINDISP.SelectedIndexChanged
        Try
            If Not _bInited Then Return
            For page = 0 To MAX_NUM_PAGE - 1
                For i = 0 To MAX_NUM_ITEM - 1
                    _btns(page, i).Selected = False
                Next
            Next
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' カートボタン押下で商品をグリッドビューに追加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCart.Click
        Try
            If CInt(txtItemNum.Text) <= 0 Then Return

            Dim data = New HINMTA_View
            Dim flg = False

            If tabHINDISP.SelectedIndex < 6 Then
                ' 商品１～商品６
                If Not _selectHINMTA Is Nothing Then
                    If _selectHINMTA.Selected Then
                        data.HINNMA = _selectHINMTA.HINNMA
                        data.HINNUM = txtItemNum.Text
                        data.URIBTK = (_selectHINMTA.URIBTK * CInt(txtItemNum.Text)).ToString
                        data.BMNCD = _selectHINMTA.BMNCD
                        data.BUNCDA = _selectHINMTA.BUNCDA
                        data.BUNCDB = _selectHINMTA.BUNCDB
                        data.BUNCDC = _selectHINMTA.BUNCDC
                        data.HINCD = _selectHINMTA.HINCD
                        ' *** Sta Add 2018/04/09 TERAYAMA 外税計算
                        Dim zeitk = _selectHINMTA.ZEITK
                        If zeitk = 0 Then
                            zeitk = UIFunction.CalcExcludedTax(CInt(data.URIBTK))
                        End If
                        ' *** End Add
                        data.ZEITK = zeitk
                        data.POINT = _selectHINMTA.POINT
                    End If
                End If
                _list.Add(data)
            Else
                ' 手入力
                data.HINNMA = txtHINNMA.Text
                data.HINNUM = txtItemNum.Text
                'data.URIBTK = txtPrice.Text
                data.URIBTK = (CInt(txtPrice.Text.Trim(","c)) * CInt(txtItemNum.Text)).ToString
                ' *** Sta Add 2018/04/09 TERAYAMA 外税計算
                data.ZEITK = UIFunction.CalcExcludedTax(CInt(data.URIBTK))
                ' *** End Add
                '*** Sta Add 2018/04/24 Kitahara
                data.POINT = CType(Me.lblPOINT.Tag, Integer)
                '*** End Add 2018/04/24 Kitahara
                If Me.rdoHINKBN1.Checked Then
                    data.HINCD = "001"
                Else
                    data.HINCD = "002"
                End If
                _list.Add(data)
            End If

            CalcPrice()
            SetAllButtonsEnable()

            Me.txtItemNum.Text = "1"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' ゴミ箱ボタン_グリッドの選択行を消去
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTrash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrash.Click
        Try
            If Me.dgvHINMTA.RowCount > 0 Then
                Me.dgvHINMTA.Rows.RemoveAt(Me.dgvHINMTA.CurrentCell.RowIndex)
            End If
            CalcPrice()
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_セルクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvHINMTA_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHINMTA.CellEnter
        Try
            SetAllButtonsEnable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' カード確認ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChkCard_Click(sender As System.Object, e As System.EventArgs) Handles btnChkCard.Click
        Try

            'カード読み込み
            Dim blnERRSHOPFLG As Boolean
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Exit Sub
                If frm.ERRFLG Then Exit Sub
                blnERRSHOPFLG = frm.ERRSHOPFLG
            End Using

            GetCCSNAME(CType(dcICR700.NCSNO, Integer))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 現金精算ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCashAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCashAdjust.Click
        Dim rePrint As New TMT90.Receipt
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

            ' *** レジ画面

            ' レジ画面の呼び出し
            Dim resultRegister = New ResultRegister
            Dim result = CallRegister(resultRegister, rePrint, Nothing)

            If Not result = eResultRegistor.SUCCESS Then Return

            ' *** DB処理

            Cursor = Cursors.WaitCursor

            ' 更新チェック
            If CheckUPDDTM("SEQTRN", _dtUPDDTM_SEQTRN) Then
                Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If



            ' トランザクション開始
            iDatabase.BeginTransaction()

            If Not UpdateSEQTRN() Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            Dim denno As String = String.Empty
            Try
                denno = GetREGISTER_DENNO()
            Catch ex As Exception
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End Try
            resultRegister.UDNNO = (CType(denno, Integer) - 1).ToString.PadLeft(4, "0"c)

            ' データベースの更新
            If Not InsDUDNTRN(resultRegister) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not InsDENTRA(resultRegister) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not InsHINTRN(resultRegister) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            ' コミット処理
            iDatabase.Commit()

            '*** ドロアオープン ***'
            Dim strErrMsg2 As String = String.Empty
            Dim isOpen As Boolean = True

            Application.DoEvents()

            _rePrint.OpenDrawer(isOpen, strErrMsg2)

            Application.DoEvents()
            '**********************'


            If _blnRECEIPT Then
                Dim strErrMsg As String = Nothing
                rePrint.strManno = String.Empty
                rePrint.strccsname = String.Empty
                If Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                    rePrint.strManno = Me.lblNCSNO.Text
                    rePrint.strccsname = Me.lblCCSNAME.Text
                End If

                rePrint.strDENNO = resultRegister.UDNNO
                rePrint.RePrint(False, strErrMsg)

                If Not strErrMsg Is Nothing Then
                    Using f As New frmMSGBOX01("レシートの発行に失敗しました。", 3)
                        f.ShowDialog()
                    End Using
                End If
            End If


            Start()

        Catch ex As Exception
            iDatabase.RollBack()
            ShowErrorMessage(ex.Message)
        Finally

            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' カード清算ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCardAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCardAdjust.Click
        Dim rePrint As New TMT90.Receipt
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

            ' *** レジ処理

            Me.lblNCSNO.Text = String.Empty
            Me.lblCCSNAME.Text = String.Empty

            Dim custmer = New Custmer

            If _DEBUG Then
                custmer = GetCSMAST("10")
            Else
                'カード読み込み
                If Not RequestCard(custmer) Then Return
            End If

            If Not custmer Is Nothing Then
                '*** 金額計算 ***'

                ''使用球数金額
                '_intUseKINGAKU = 0
                ''プリカ金額
                'Dim intKINGAKU As Integer = CType(dcICR700.KINGAKU, Integer)
                'V31残金
                Dim intV31ZANKN As Integer = CType(dcICR700.ZANKN, Integer)
                'V31残プレミアム
                Dim intV31PREZANKN As Integer = CType(dcICR700.PREZANKN, Integer)
                '_intPayBallZANKN = 0
                '_intPayBallPREMKN = 0
                'Select Case dcICR700.SYUBETU
                '    Case "B", "C", "D"
                '        _intUseKINGAKU = 0
                '        intKINGAKU = (intV31ZANKN + intV31PREZANKN)
                '    Case Else
                '        '【残金整合処理】
                '        If Not UIFunction.ZANSEIGO(intV31ZANKN, intV31PREZANKN, CType(dcICR700.KINGAKU, Integer), _intUseKINGAKU, _intPayBallZANKN, _intPayBallPREMKN) Then
                '            Using frm As New frmMSGBOX01("ｶｰﾄﾞ残金が不正です。" & vbCrLf & "ｶｰﾄﾞ修復又は再発行を行って下さい。", 3)
                '                frm.ShowDialog()
                '            End Using
                '            If Not _DEBUG Then EjectCard()
                '            Exit Sub
                '        End If
                'End Select

                ''書き込む前の前回来場セット(ボールトラン更新用)
                '_strZENENTDATE = dcICR700.ZENENTDATE
                'If dcICR700.ZENENTDATE.Equals("00000000") Then
                '    _strZENENTDATE = UIUtility.SYSTEM.UPDDAY
                'End If

                custmer.ZANKN = intV31ZANKN
                custmer.PREZANKN = intV31PREZANKN
                'ポイントはサーバー優先に変更
                'custmer.SRTPO = CType(dcICR700.POINT, Integer)
            End If

            ' プレミアム残金とカード残金が合計額より不足していれば注意メッセージを表示し、清算を行えないようにする
            If Not _DEBUG Then
                If Not custmer Is Nothing Then
                    If UIUtility.SYSTEM.HINPREMPAYKBN.Equals(0) Then
                        'ﾌﾟﾚﾐｱﾑからの精算なし
                        If custmer.ZANKN < CInt(txtTotal.Text) Then
                            Using frm As New frmMSGBOX01("カード残高が不足しています。", 3)
                                frm.ShowDialog()
                            End Using
                            If Not _DEBUG Then EjectCard()
                            'btnCardAdjust.Enabled = False
                            Exit Sub
                        End If
                    Else
                        'ﾌﾟﾚﾐｱﾑからの精算あり
                        If (custmer.ZANKN + custmer.PREZANKN) < CInt(txtTotal.Text) Then
                            Using frm As New frmMSGBOX01("カード残高が不足しています。", 3)
                                frm.ShowDialog()
                            End Using
                            If Not _DEBUG Then EjectCard()
                            'btnCardAdjust.Enabled = False
                            Exit Sub
                        End If
                    End If

                End If
            End If

            ' レジ画面の呼び出し
            Dim resultRegister = New ResultRegister
            Dim result = CallRegister(resultRegister, rePrint, custmer)

            If Not result = eResultRegistor.SUCCESS Then
                If Not _DEBUG Then EjectCard()
                Exit Sub
            End If

            If (CType(custmer.SRTPO, Integer) + resultRegister.GETPOINT) > 99999 Then
                Using frm As New frmMSGBOX01("ポイントの上限数を超えています。", 3)
                    frm.ShowDialog()
                End Using
                If Not _DEBUG Then EjectCard()
                'btnCardAdjust.Enabled = False
                Exit Sub
            End If

            ' *** カード清算処理

            Dim price = CInt(txtTotal.Text)
            Dim prezankn = CInt(custmer.PREZANKN)
            Dim zankn = 0
            Dim point = resultRegister.GETPOINT

            ' プレミアム残金より取引額が多ければカード残高から差し引く
            '*** Sta Mnt 2018/05/31 Kitahara
            If UIUtility.SYSTEM.HINPREMPAYKBN.Equals(0) Then
                'ﾌﾟﾚﾐｱﾑから精算なし
                zankn = price
                prezankn = 0
            Else
                'ﾌﾟﾚﾐｱﾑから精算あり
                If price > prezankn Then
                    zankn = price - prezankn
                    prezankn = prezankn
                Else
                    zankn = 0
                    prezankn = price
                End If
            End If
            'If price > prezankn Then
            '    zankn = price - prezankn
            '    prezankn = prezankn
            'Else
            '    zankn = 0
            '    prezankn = price
            'End If
            '*** End Mnt 2018/05/31 Kitahara

            ' *** DB処理

            Cursor = Cursors.WaitCursor

            ' 更新チェック
            If CheckUPDDTM("SEQTRN", _dtUPDDTM_SEQTRN) Then
                Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If


            ' トランザクション開始
            iDatabase.BeginTransaction()

            If Not UpdateSEQTRN() Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            Dim denno As String = String.Empty
            Try
                denno = GetREGISTER_DENNO()
            Catch ex As Exception
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End Try
            resultRegister.UDNNO = (CType(denno, Integer) - 1).ToString.PadLeft(4, "0"c)

            ' データベースの更新
            If Not UpdateKINSMA(resultRegister, custmer) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not UpdateDPOINTSMA(resultRegister, custmer) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not UpdateCARDLIMIT(resultRegister, custmer) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not InsDUDNTRN(resultRegister, custmer) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not InsDENTRA(resultRegister, custmer) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            If Not InsHINTRN(resultRegister, custmer) Then
                Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Dim intZANAKN As Integer = 0
            Dim intZANBKN As Integer = 0
            Dim intPREZANAKN As Integer = 0
            Dim intPREZANBKN As Integer = 0
            Dim intZANAPO As Integer = 0
            Dim intZANBPO As Integer = 0

            If Not custmer Is Nothing Then
                intZANAKN = CType(dcICR700.ZANKN, Integer)
                intZANBKN = CType(dcICR700.ZANKN, Integer) - _intPayBallZANKN
                intPREZANAKN = CType(dcICR700.PREZANKN, Integer)
                intPREZANBKN = CType(dcICR700.PREZANKN, Integer) - _intPayBallPREMKN
                intZANAPO = CType(custmer.SRTPO, Integer)
                intZANBPO = CType(custmer.SRTPO, Integer) + point
            End If

            ' カード書込
            If Not _DEBUG Then
                Dim data = New PriceDataSet
                data.ADD_ZANKN = -zankn
                data.ADD_PREMKN = -prezankn
                data.ADD_POINT = point
                If Not WriteCard(data, custmer) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If



            ' コミット処理
            iDatabase.Commit()

            If _blnRECEIPT Then
                Dim strErrMsg As String = Nothing
                rePrint.strManno = custmer.NCSNO
                rePrint.strccsname = custmer.CCSNAME
                rePrint.strDENNO = resultRegister.UDNNO
                rePrint.RePrint(False, strErrMsg)

                If Not strErrMsg Is Nothing Then
                    Using f As New frmMSGBOX01("レシートの発行に失敗しました。", 3)
                        f.ShowDialog()
                    End Using
                End If
            End If


            Start()

        Catch ex As Exception
            iDatabase.RollBack()
            ShowErrorMessage(ex.Message)
        Finally

            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' レシート再発行ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceipt.Click
        Dim strDENNO As String
        Dim intCPAYKBN As Integer = 0
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmUDNDETAIL01(iDatabase)
                '分類【商品】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.SHOHIN
                '処理【印刷】
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

            Dim dtGOODS As DataTable = Nothing
            Dim drDUDNTRN As DataRow = Nothing

            If Not GetURIDETAIL(dtGOODS, drDUDNTRN, strDENNO) Then
                Using frm As New frmMSGBOX01("売上情報がありません。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
                Exit Sub
            End If

           
            '【レシート印刷】
            Dim rePrint As New TMT90.Receipt

            rePrint.intDatKbn = CType(drDUDNTRN.Item("DATKB").ToString, Integer)

            rePrint.intGetPoint = CType(drDUDNTRN.Item("POINT").ToString, Integer)

            rePrint.intPrintKbn = 0
            rePrint.strDENNO = strDENNO
            rePrint.insDTTM = CType(drDUDNTRN("INSDTMSTR").ToString, DateTime)
            rePrint.dtGoods = dtGOODS
            rePrint.intGokei = CInt(drDUDNTRN("UDNBKN"))

            rePrint.strManno = String.Empty
            If Not String.IsNullOrEmpty(drDUDNTRN("MANNO").ToString) Then
                rePrint.intTax = 0
                rePrint.intDeposit = CInt(drDUDNTRN("UDNBKN"))
                rePrint.intChange = 0

                Dim intZANKINGAKU As Integer = 0
                Dim intZANPOINT As Integer = 0
                UIFunction.GetTRNZANKIN3(intZANKINGAKU, intZANPOINT, Now.ToString("yyyyMMdd"), CType(strDENNO, Integer), iDatabase)

                rePrint.strManno = drDUDNTRN("MANNO").ToString
                rePrint.strccsname = drDUDNTRN("CCSNAME").ToString

                rePrint.intzankingaku = intZANKINGAKU
                rePrint.intzanpoint = intZANPOINT
            Else
                rePrint.intTax = CInt(drDUDNTRN("UDNZKN"))
                rePrint.intDeposit = CInt(drDUDNTRN("NYUKN"))
                rePrint.intChange = CInt(drDUDNTRN("TURIKN"))
            End If


            rePrint.strHostName = drDUDNTRN("HOSTNAME").ToString
            rePrint.intFlg = CInt(UIUtility.SYSTEM.SHOPNO)
            rePrint.RePrint(False, String.Empty)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 伝票取り消しボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSlipCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlipCancel.Click
        Dim strDENNO As String = String.Empty
        Dim sql As New System.Text.StringBuilder
        Dim blnUpdDATKB As Boolean = False          '【True】伝票のDATKBを1に戻す
        Try
            Using frm As New frmUDNDETAIL01(iDatabase)
                ' 分類【商品】
                frm.BUNCDA = frmUDNDETAIL01.BUNCDA_Type.SHOHIN
                ' 処理【取消】
                frm.PROCESS = frmUDNDETAIL01.PROCESS_Type.CANCEL
                ' 売上詳細画面でDB更新処理を行わない
                frm.UpdateDisabled = True
                ' *** STA ADD 2018/04/11 TERAYAMA 特定ホスト以外非表示
                frm.CurrentHostOnly = True
                ' *** END ADD
                frm.ShowDialog()

                If frm.CANCEL Then Exit Sub

                '伝票番号
                strDENNO = frm.DENNO
            End Using

            Dim dtGOODS As DataTable = Nothing
            Dim drDUDNTRN As DataRow = Nothing

            If Not GetURIDETAIL(dtGOODS, drDUDNTRN, strDENNO) Then
                Using f As New frmMSGBOX01("売上情報がありません。", 2)
                    f.ShowDialog()
                    Exit Sub
                End Using
                Exit Sub
            End If

            Dim manno = drDUDNTRN("MANNO").ToString
            Dim buncdc = drDUDNTRN("BUNCDC").ToString
            Dim cpaykbn As String = drDUDNTRN("CPAYKBN").ToString

            If cpaykbn.Equals("0") Or buncdc = "999" Then
                'If String.IsNullOrEmpty(manno) Or buncdc = "999" Then
                ' 現金精算の取消処理

                ' DBの取消処理を行う
                frmUDNDETAIL01.UpdateTablesForHINDISP01(CInt(strDENNO), iDatabase)


                Dim strErrMsg2 As String = String.Empty
                Dim isOpen As Boolean = True


                Application.DoEvents()

                _rePrint.OpenDrawer(isOpen, strErrMsg2)

                Application.DoEvents()

            Else
                ' 打席カード清算の取消処理

                ' *** カード読み込みと顧客情報の取得

                Dim custmer = New Custmer
                If _DEBUG Then
                    custmer = GetCSMAST(manno)
                Else
                    If Not RequestCard(custmer, manno) Then Return
                End If

                ' 顧客番号チェック
                If Not dcICR700.NCSNO = custmer.NCARDID Then
                    Using f As New frmMSGBOX01("顧客番号が一致しません。", 3)
                        f.ShowDialog()
                    End Using
                    EjectCard()
                    Exit Sub
                End If

                Dim strMsg As String = "打席でﾌﾟﾚｲ済みです。受付画面のｶｰﾄﾞ再印字をしてください。"
                If Not CType(dcICR700.KINGAKU, Integer).Equals(CType(dcICR700.ZANKN, Integer) + CType(dcICR700.PREZANKN, Integer)) Then
                    If dcICR700.ENTKBN.Equals("1") Then
                        'ﾁｪｯｸｲﾝ中
                        strMsg = "打席でﾌﾟﾚｲ済みです。先にﾁｪｯｸｱｳﾄをしてください。"
                    End If
                    Using f As New frmMSGBOX01(strMsg, 3)
                        f.ShowDialog()
                    End Using
                    EjectCard()
                    Exit Sub
                End If

                ' 顧客番号が一致したときのみDBの取消処理を行う
                frmUDNDETAIL01.UpdateTablesForHINDISP01(CInt(strDENNO), iDatabase)

                ' *** ポイント計算とカード書込処理

                Dim result = New ResultRegister
                Dim premkn = CInt(drDUDNTRN("PREMKN")) ' 前回のプレミアム金額から差し引いた値
                Dim price = CInt(drDUDNTRN("UDNBKN"))  ' 前回の取引額
                Dim sagaku = 0                         ' プレミアム金額から差し引いた取引額の差額
                Dim point = CInt(drDUDNTRN("POINT"))   ' 前回の取得ポイント

                ' 前回の残りプレミアム金額より取引額が多ければカード残高から差し引いたと見なし、残金に加算する差額を計算する
                If price > premkn Then
                    sagaku = price - premkn
                Else
                    sagaku = 0
                End If
                result.ZANKN = CInt(dcICR700.ZANKN) + sagaku
                result.PREZANKN = CInt(dcICR700.PREZANKN) + premkn
                result.GETPOINT = -point

                '伝票取消が完了しているのでここからエラーが発生したら伝票取消を戻さないといけない
                blnUpdDATKB = True

                Dim intKINGAKU As Integer = result.ZANKN + result.PREZANKN
                Dim intPOINT As Integer = result.GETPOINT

                If intKINGAKU > UIUtility.SYSTEM.ZANMAX Then
                    Using frm As New frmMSGBOX01("入金限度額を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                ElseIf (custmer.SRTPO - intPOINT) > 99999 Then
                    Using frm As New frmMSGBOX01("ポイント上限を超えている為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                ElseIf (custmer.SRTPO - intPOINT) < 0 Then
                    Using frm As New frmMSGBOX01("ポイントがマイナスになる為取消できません。", 2)
                        frm.ShowDialog()
                    End Using
                    Using frm As New frmREQUESTCARD(dcICR700)
                        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If



                ' *** DB更新

                ' トランザクション開始
                iDatabase.BeginTransaction()

                If Not UpdateKINSMA(result, custmer) Then
                    Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
                If Not UpdateDPOINTSMA(result, custmer) Then
                    Using frm As New frmMSGBOX01("登録に失敗しました。", 3)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                ' カード書込
                If Not _DEBUG Then
                    Dim data = New PriceDataSet
                    data.ADD_ZANKN = sagaku
                    data.ADD_PREMKN = premkn
                    data.ADD_POINT = -point
                    If Not WriteCard(data, custmer) Then
                        iDatabase.RollBack()
                        Using frm As New frmMSGBOX01("カードの書き込みに失敗しました。", 2)
                            frm.ShowDialog()
                        End Using
                        Exit Sub
                    End If
                End If



                ' コミット処理
                iDatabase.Commit()

                'ここまできたら戻さなくてＯＫ
                blnUpdDATKB = False

            End If

            Using frm As New frmMSGBOX01("伝票を取り消しました。", 0)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            iDatabase.RollBack()
            ShowErrorMessage(ex.Message)
        Finally
            If blnUpdDATKB Then
                '伝票情報を戻す
                UIFunction.UpdBackDATKB(1, CType(strDENNO, Integer), iDatabase)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 伝票修正ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSlipMod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 未実装
    End Sub

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' 開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Start()
        Try
            ' 画面初期化
            Init()

            ' ポイント付与率の取得
            GetPOINTMST()

            ' 商品ボタンの生成
            SetHINMAButton()

            ' 商品項目タグの取得
            GetHINMTB()

            ' 商品マスタの取得
            GetHINMTA()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        ' 余計なボタンを非表示
        Me.tspFunc1.Enabled = True
        Me.tspFunc3.Enabled = False
        Me.tspFunc8.Enabled = False
        Me.tspFunc9.Enabled = False
        Me.tspFunc10.Enabled = True
        Me.tspFunc11.Enabled = True
        Me.tspFunc12.Enabled = True

        ' テキストボックスの初期化
        txtHINNMA.Text = "手入力"
        'txtHINNMA.Text = String.Empty
        txtPrice.Text = "0"
        txtItemNum.Text = "1"
        txtTotal.Text = "0"
        txtTotalTax.Text = TAX_PREFIX & "0"
        txtTotalPoint.Text = "0"

        ' ボタンの初期化
        btnCart.Enabled = False
        btnTrash.Enabled = False
        btnCashAdjust.Enabled = False
        btnCardAdjust.Enabled = False

        ' 商品リストに関連付け
        Me.dgvHINMTA.DataSource = _list
        _list.Clear()

        '顧客番号
        Me.lblNCSNO.Text = _strNCSNO
        Me.lblCCSNAME.Text = _strCCSNAME

    End Sub

    ''' <summary>
    ''' 商品項目タグの取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHINMTB()
        Dim sql As New System.Text.StringBuilder
        Try


            ' HINMTBの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTB")

            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return

            Dim query = From x In dt.AsEnumerable
                        Where x("BMNCD").ToString = "002" And x("BUNCDA").ToString = "001"
                        Order By x("BUNCDB")

            TabPage1.Text = query.Where(Function(x As DataRow) x("BUNCDB").ToString = "001").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
            TabPage2.Text = query.Where(Function(x As DataRow) x("BUNCDB").ToString = "002").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
            TabPage3.Text = query.Where(Function(x As DataRow) x("BUNCDB").ToString = "003").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
            TabPage4.Text = query.Where(Function(x As DataRow) x("BUNCDB").ToString = "004").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
            TabPage5.Text = query.Where(Function(x As DataRow) x("BUNCDB").ToString = "005").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()
            TabPage6.Text = query.Where(Function(x As DataRow) x("BUNCDB").ToString = "006").Select(Function(x As DataRow) x("BUNNMB")).First().ToString()

            If query.Where(Function(x As DataRow) x("BUNCDB").Equals("001")).Select(Function(x As DataRow) x("KOTEIKBN")).First().ToString.Equals("0") Then
                _blnCard1 = False
            Else
                _blnCard1 = True
            End If
            Me.btnCardAdjust.Visible = _blnCard1
            If query.Where(Function(x As DataRow) x("BUNCDB").Equals("002")).Select(Function(x As DataRow) x("KOTEIKBN")).First().ToString.Equals("0") Then
                _blnCard2 = False
            Else
                _blnCard2 = True
            End If
            If query.Where(Function(x As DataRow) x("BUNCDB").Equals("003")).Select(Function(x As DataRow) x("KOTEIKBN")).First().ToString.Equals("0") Then
                _blnCard3 = False
            Else
                _blnCard3 = True
            End If

            ' タブページの設定
            SetTabPage()

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 商品項目の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHINMTA()
        Dim sql As New System.Text.StringBuilder
        Try


            ' HINMTBの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTA")

            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return

            Dim query = From x In dt.AsEnumerable
                        Where x("BMNCD").ToString = "002" And x("BUNCDA").ToString = "001"
                        Order By x("BUNCDB"), x("HINCD")

            GetHINMTA_Tab(query, "001", 0)
            GetHINMTA_Tab(query, "002", 1)
            GetHINMTA_Tab(query, "003", 2)
            GetHINMTA_Tab(query, "004", 3)
            GetHINMTA_Tab(query, "005", 4)
            GetHINMTA_Tab(query, "006", 5)

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    ''' <summary>
    ''' 分類ごとに商品項目を設置する
    ''' </summary>
    ''' <param name="query"></param>
    ''' <param name="buncdb"></param>
    ''' <param name="page"></param>
    ''' <remarks></remarks>
    Private Sub GetHINMTA_Tab(ByRef query As System.Data.OrderedEnumerableRowCollection(Of DataRow), ByVal buncdb As String, ByVal page As Integer)
        Dim sql As New System.Text.StringBuilder
        Try
            Dim q = query.Where(Function(x As DataRow) x("BUNCDB").ToString = buncdb)
            Dim i = 0
            For Each row As DataRow In q
                Dim btn = _btns(page, i)
                btn.HINNMA = row("HINNMA").ToString
                btn.URIBTK = CInt(row("URIBTK"))
                Dim point = CInt(GetPointRateInt(CInt(row("URIBTK"))))
                btn.POINT = point
                btn.BMNCD = row("BMNCD").ToString
                btn.BUNCDA = row("BUNCDA").ToString
                btn.BUNCDB = row("BUNCDB").ToString
                btn.BUNCDC = row("BUNCDC").ToString
                btn.HINCD = row("HINCD").ToString
                btn.ZEITK = CInt(row("ZEITK"))
                btn.Visible = True
                i += 1
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' ポイント付与率の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetPOINTMST()
        Dim sql As New System.Text.StringBuilder
        Try


            ' POINTMSTの取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM POINTMST")

            Dim dt = iDatabase.ExecuteRead(sql.ToString())

            If dt.Rows.Count <= 0 Then Return

            ' ポイント付与率の取得
            Dim qPoint = From x In dt.AsEnumerable
                         Where x("BMNCD").ToString = "002" And x("POINTNO").ToString = "007"
                         Select x("POINT")

            _pointRate = CInt(qPoint.First()) * 0.01

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    ''' <summary>
    ''' ポイントの取得_Int
    ''' </summary>
    ''' <param name="price"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPointRateInt(ByVal price As Integer) As Integer
        Return CInt(Math.Floor(price * _pointRate))
    End Function

    ''' <summary>
    ''' 各タブページに商品ボタンを配置する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHINMAButton()
        Try
            Dim x = 0
            Dim y = 0
            Dim ox = 28
            For page = 0 To MAX_NUM_PAGE - 1
                Me.tabHINDISP.TabPages(page).Controls.Clear()
                For i = 0 To MAX_NUM_ITEM - 1
                    If i < 8 Then
                        x = ox
                    Else
                        x = 315 + ox
                    End If
                    _btns(page, i) = New HINMSTButton
                    If i = 0 Or i = 8 Then
                        y = 27
                    Else
                        y += _btns(page, i).Height + 10
                    End If
                    _btns(page, i).Location = New Point(x, y)
                    _btns(page, i).Visible = False
                    Me.tabHINDISP.TabPages(page).Controls.Add(_btns(page, i))
                    AddHandler _btns(page, i).OnButtonClick, AddressOf btnHINMASTButton_Click
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タブの背景色や縦書きを適応
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tabHINDISP_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tabHINDISP.DrawItem
        '対象のTabControlを取得
        Dim tab As TabControl = CType(sender, TabControl)
        'タブページのテキストを取得
        Dim txt As String = StrConv(tab.TabPages(e.Index).Text, VbStrConv.Wide)

        'タブのテキストと背景を描画するためのブラシを決定する
        Dim foreBrush, backBrush As Brush
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            '選択されているタブのテキストを赤、背景を青とする
            foreBrush = Brushes.Black
            backBrush = Brushes.Yellow
        Else
            '選択されていないタブのテキストは灰色、背景を白とする
            foreBrush = Brushes.Black
            backBrush = Brushes.LightGray
        End If

        'StringFormatを作成
        Dim sf As New StringFormat
        '中央に表示する
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        '背景の描画
        e.Graphics.FillRectangle(backBrush, e.Bounds)
        'Textの描画
        e.Graphics.DrawString(txt, e.Font, foreBrush, RectangleF.op_Implicit(e.Bounds), sf)
    End Sub

    ''' <summary>
    ''' タブページの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTabPage()
        Try
            'タブをオーナードローする
            tabHINDISP.DrawMode = TabDrawMode.OwnerDrawFixed

            ' 文字数に応じたフォントサイズの設定
            Dim fontSizeSettings() = {16, 16, 16, 16, 14, 14, 12, 12, 10, 10}

            Dim max = 0
            For Each page As TabPage In tabHINDISP.TabPages
                If (page.Text.Length - 1) > max Then
                    max = page.Text.Length - 1
                End If
            Next
            ' 文字数の最大数でフォント設定を適応
            With tabHINDISP.Font
                Dim size = fontSizeSettings(max)
                tabHINDISP.Font = New Font(.FontFamily, size, .Style, .Unit, .GdiCharSet, True)
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カートボタンの有効/無効を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCartEnable()
        Try
            If Not _bInited Then Return

            btnCart.Enabled = False
            If tabHINDISP.SelectedIndex < MAX_NUM_PAGE Then
                ' 商品１～商品３
                For i = 0 To MAX_NUM_ITEM - 1
                    If CInt(txtItemNum.Text) > 0 Then
                        If _btns(tabHINDISP.SelectedIndex, i).Selected Then
                            btnCart.Enabled = True
                            Exit For
                        End If
                    End If
                Next
            Else
                ' 手入力
                If Not String.IsNullOrEmpty(txtHINNMA.Text.Trim) Then
                    btnCart.Enabled = CInt(txtPrice.Text) > 0 And CInt(txtItemNum.Text) > 0
                End If
            End If

            Select Case tabHINDISP.SelectedIndex
                Case 0
                    Me.btnCardAdjust.Visible = _blnCard1
                Case 1
                    Me.btnCardAdjust.Visible = _blnCard2
                Case 2
                    Me.btnCardAdjust.Visible = _blnCard3
                Case Else
                    Me.btnCardAdjust.Visible = True
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ゴミ箱ボタンの有効/無効の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTrashEnable()
        Try
            btnTrash.Enabled = Not Me.dgvHINMTA.CurrentCell Is Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 現金精算、カード清算ボタン、画面クリアの有効/無効を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAdjustmentEnable()
        Try
            btnCashAdjust.Enabled = _list.Count > 0
            btnCardAdjust.Enabled = _list.Count > 0
            btnClearDisp.Enabled = _list.Count > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ボタンの有効/無効をまとめて設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAllButtonsEnable()
        SetCartEnable()
        SetTrashEnable()
        SetAdjustmentEnable()
    End Sub

    ''' <summary>
    ''' グリッド内の合計金額、内税、取得ポイントなどを計算して表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalcPrice()
        Dim total = 0
        Dim totalTax = 0
        Dim totalPoint = 0
        For Each list In _list
            total += CInt(list.URIBTK)
            totalTax += list.ZEITK
            totalPoint += list.POINT
        Next
        txtTotal.Text = total.ToString("#,0")
        txtTotalTax.Text = TAX_PREFIX & totalTax.ToString("#,0")
        txtTotalPoint.Text = totalPoint.ToString("#,0")
    End Sub

    ''' <summary>
    ''' レジ画面を呼び出し
    ''' </summary>
    ''' <param name="custmer"></param>
    ''' <remarks></remarks>
    Private Function CallRegister(ByRef result As ResultRegister, ByRef rePrint As TMT90.Receipt, Optional ByVal custmer As Custmer = Nothing) As eResultRegistor
        Using frm As New frmREGISTER01

            ' *** レジ画面呼び出し

            Dim denno = GetREGISTER_DENNO()

            If denno Is Nothing Then
                Return eResultRegistor.FAILE
            End If

            Dim goods = GetREGISTER_GOODS()

            frm.DENNO = denno
            frm.GOODS = goods
            If Not custmer Is Nothing Then
                ' カード精算のみ
                frm.NCSNO = custmer.NCSNO
                frm.CCSNAME = custmer.CCSNAME
                frm.CCSKANA = custmer.CCSKANA
                frm.CKBNAME = custmer.CKBNAME
                frm.SCLMANNO = custmer.DSCLMANNO
                frm.DMEMBER = custmer.DMEMBER
                frm.DBIRTH = custmer.DBIRTH
                frm.ZANKN = custmer.ZANKN.ToString.PadLeft(5, "0"c)
                frm.PREZANKN = custmer.PREZANKN.ToString.PadLeft(5, "0"c)
                frm.POINT = custmer.SRTPO.ToString.PadLeft(5, "0"c)
                frm.GETPREMKN = 0
            End If

            frm.PAYMENT = CInt(txtTotal.Text)
            frm.GETPOINT = CInt(txtTotalPoint.Text)
            If UIUtility.SYSTEM.RECEIPTFLG.Equals(1) Then
                frm.RECEIPT = True
            Else
                frm.RECEIPT = False
            End If
            frm.HINFLG = True
            frm.ShowDialog()

            ' 戻るボタン
            If frm.CANCEL Then Return eResultRegistor.CANCEL

            '受付たｶｰﾄﾞが同一か確認
            If Not custmer Is Nothing Then
                If Not CheckCard() Then
                    Using f As New frmMSGBOX01("最初に受付けたカードと異なります。", 3)
                        f.ShowDialog()
                    End Using
                    Return eResultRegistor.CANCEL
                End If
            End If

            ' 金額不足
            If frm.DEPOSIT < CInt(txtTotal.Text) Then
                Using f As New frmMSGBOX01("お預かり金額が不足しています。", 3)
                    f.ShowDialog()
                End Using
                Return eResultRegistor.DEPOSIT_LOWER
            End If

            '支払区分
            Dim cpaykbn = 0
            cpaykbn = frm.CPAYKBN

            ' 預かり金額とつり銭の登録
            Dim deposit = 0
            Dim change = 0
            Dim getpremkn = 0
            Dim getpoin = 0
            If custmer Is Nothing Then
                ' 現金精算のみ
                deposit = frm.DEPOSIT
                change = frm.CHANGE
            Else
                ' カード精算のみ
                deposit = CInt(txtTotal.Text)
                change = 0
                getpoin = frm.GETPOINT
                cpaykbn = 98
            End If


            ' *** レジ画面終了処理

            '伝票番号再取得
            denno = GetREGISTER_DENNO()

            Dim insDTTM = DateTime.Now

            ' DB登録用データの登録
            result.UDNNO = denno
            result.INSDTMSTR = insDTTM.ToString("yyyy/MM/dd HH:mm:ss")
            Dim dtNow = DateTime.Now
            result.INSDTM = dtNow.ToString & "." & dtNow.Millisecond
            result.UDNBKN = CInt(txtTotal.Text)
            result.UDNZKN = CInt(txtTotalTax.Text.Replace(TAX_PREFIX, "").Trim)
            result.UDNAKN = result.UDNBKN - result.UDNZKN
            result.GETPOINT = frm.GETPOINT
            result.GETPREMKN = frm.GETPREMKN
            result.NYUKN = frm.DEPOSIT
            result.TURIKN = frm.CHANGE
            result.CPAYKBN = frm.CPAYKBN

            If Not custmer Is Nothing Then
                ' プレミアム残金があれば優先的に消費する
                ' プレミアム残金が商品代金に満たなければ不足分をカード残金から消費する
                ' それでも足りなければエラーにする
                Dim price = result.UDNBKN
                Dim zankn = CInt(custmer.ZANKN)
                Dim prezankn = CInt(custmer.PREZANKN)

                '*** Sta Mnt 2018/05/31 Kitahara
                If UIUtility.SYSTEM.HINPREMPAYKBN.Equals(0) Then
                    'ﾌﾟﾚﾐｱﾑから精算なし
                    result.PREMKN = 0
                    result.PREZANKN = prezankn
                Else
                    'ﾌﾟﾚﾐｱﾑから精算あり
                    If price > prezankn Then
                        price = result.UDNBKN - prezankn
                        result.PREMKN = prezankn
                        result.PREZANKN = 0
                    Else
                        result.PREMKN = result.UDNBKN
                        result.PREZANKN = prezankn - result.UDNBKN
                        price = 0
                    End If
                End If
                'If price > prezankn Then
                '    price = result.UDNBKN - prezankn
                '    result.PREMKN = prezankn
                '    result.PREZANKN = 0
                'Else
                '    result.PREMKN = result.UDNBKN
                '    result.PREZANKN = prezankn - result.UDNBKN
                '    price = 0
                'End If
                '*** End Mnt 2018/05/31 Kitahara
                If price <= zankn Then
                    result.ZANKN = zankn - price
                Else
                    Using f As New frmMSGBOX01("カード残額が不足しています。", 3)
                        f.ShowDialog()
                    End Using
                    Return eResultRegistor.ZANKN_LOWER
                End If
            End If

            ' レシート印刷
            If frm.RECEIPT Then
                _blnRECEIPT = True

                Dim drGoods As DataRow()
                drGoods = goods.Select()
                drGoods(0).Item("CPAYKBN") = cpaykbn
                drGoods(0).EndEdit()

                rePrint.intGetPremKn = getpremkn
                rePrint.intGetPoint = getpoin

                If Not custmer Is Nothing Then
                    rePrint.intzankingaku = custmer.ZANKN + custmer.PREZANKN - CType(Me.txtTotal.Text, Integer)
                    rePrint.intzanpoint = custmer.SRTPO
                End If


                rePrint.intPrintKbn = 0
                rePrint.strDENNO = denno
                rePrint.insDTTM = insDTTM
                rePrint.dtGoods = goods
                rePrint.intGokei = CInt(txtTotal.Text)
                rePrint.intDeposit = deposit
                rePrint.intChange = change
                rePrint.intTax = CInt(txtTotalTax.Text.Replace(TAX_PREFIX, "").Trim)
                rePrint.strHostName = My.Computer.Name
                rePrint.intFlg = CType(UIUtility.SYSTEM.SHOPNO, Integer)
            Else
                _blnRECEIPT = False
            End If

            Return eResultRegistor.SUCCESS

        End Using

    End Function

    ''' <summary>
    ''' DUNDNTRNの登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsDUDNTRN(ByRef result As ResultRegister, Optional ByRef custmer As Custmer = Nothing) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            ' *** 共通項目            
            Dim datkb = "1"
            Dim udndt = DateTime.Now.ToString("yyyyMMdd")
            Dim udnno = result.UDNNO.TrimStart("0"c)
            Dim bmncd = "002"
            Dim buncda = "001"
            Dim buncdb = "001"
            Dim buncdc = "001"
            Dim tktkbn = "001"
            Dim udnkn = result.UDNBKN
            Dim tktsu = 1
            Dim insdtmstr = result.INSDTMSTR
            Dim insdtm = result.INSDTM
            Dim udnakn = result.UDNAKN
            Dim udnbkn = result.UDNBKN
            Dim udnzkn = result.UDNZKN
            Dim hinzeikb = "2"
            Dim koteikbn = "0"
            Dim udnkbn = "3"
            Dim hostname = My.Computer.Name
            Dim drakbn = "0"

            Dim nyukn = result.NYUKN
            Dim turikn = result.TURIKN

            ' *** 会員と非会員で異なる項目
            Dim point As Integer
            Dim cpaykbn As String
            Dim premkn As Integer
            If Not custmer Is Nothing Then
                ' 会員(打席カード)
                point = result.GETPOINT
                ' STA MOD *** 2018/05/10 TERAYAMA 打席カード払いはCPAYKBN = 4
                cpaykbn = "4"
                ' END MOD
                premkn = result.PREMKN
            Else
                ' 非会員
                point = 0
                cpaykbn = result.CPAYKBN.ToString
                premkn = 0
            End If

            sql.Clear()
            sql.Append(" INSERT INTO DUDNTRN (")
            sql.Append(" DATKB, UDNDT, UDNNO, BMNCD, BUNCDA, BUNCDB, BUNCDC, TKTKBN, UDNKN, TKTSU,")
            sql.Append(" INSDTMSTR, INSDTM, UDNAKN, UDNBKN, UDNZKN, HINZEIKB, POINT, KOTEIKBN,")
            sql.Append(" UDNKBN, CPAYKBN, HOSTNAME, DRAKBN, PREMKN,")

            sql.Append(" NYUKN, TURIKN, STFCODE, STFNAME")
            ' 会員専用
            If Not custmer Is Nothing Then
                sql.Append(" ,")
                sql.Append(" MANNO, KSBKB, SCLMANNO, SCLKBN, POZEIKB")
            ElseIf Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                sql.Append(" ,")
                sql.Append(" MANNO")
            End If
            sql.Append(" )")
            sql.Append(" VALUES(")
            sql.Append(" '" & datkb & "',")
            sql.Append(" '" & udndt & "',")
            sql.Append(" " & udnno & ",")
            sql.Append(" '" & bmncd & "',")
            sql.Append(" '" & buncda & "',")
            sql.Append(" '" & buncdb & "',")
            sql.Append(" '" & buncdc & "',")
            sql.Append(" '" & tktkbn & "',")
            sql.Append(" " & udnkn & ",")
            sql.Append(" " & tktsu & ",")
            sql.Append(" '" & insdtmstr & "',")
            sql.Append(" '" & insdtm & "',")
            sql.Append(" " & udnakn & ",")
            sql.Append(" " & udnbkn & ",")
            sql.Append(" " & udnzkn & ",")
            sql.Append(" '" & hinzeikb & "',")
            sql.Append(" " & point & ",")
            sql.Append(" '" & koteikbn & "',")
            sql.Append(" '" & udnkbn & "',")
            sql.Append(" '" & cpaykbn & "',")
            sql.Append(" '" & hostname & "',")
            sql.Append(" '" & drakbn & "',")
            sql.Append(" " & premkn & ",")

            sql.Append(" " & nyukn & ",")
            sql.Append(" " & turikn & ",")
            sql.Append(" " & UIFunction.NullCheck(_strSTFCODE) & ",")
            sql.Append(" " & UIFunction.NullCheck(_strSTFNAME) & "")
            ' 会員専用
            If Not custmer Is Nothing Then
                sql.Append(" ,")
                sql.Append(" '" & custmer.NCSNO & "',")
                sql.Append(" '" & custmer.NCSRANK & "',")
                sql.Append(" '" & custmer.DSCLMANNO & "',")
                sql.Append(" '" & custmer.SCLKBN & "',")
                sql.Append(" '2' ")
            ElseIf Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                sql.Append(" ,")
                sql.Append(" '" & Me.lblNCSNO.Text & "'")
            End If
            sql.Append(" )")

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
    ''' DENTRAの登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsDENTRA(ByRef result As ResultRegister, Optional ByRef custmer As Custmer = Nothing) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim linno = 1
            For Each lt In _list

                ' *** 共通項目
                Dim datkb = "1"
                Dim udndt = DateTime.Now.ToString("yyyyMMdd")
                Dim udnno = result.UDNNO.TrimStart("0"c)
                Dim bmncd = "002"
                Dim buncda = "001"
                Dim buncdb = lt.BUNCDB
                Dim buncdc = "001"
                Dim tktkbn = lt.HINCD
                Dim udnkn = result.UDNBKN
                Dim tktsu = CInt(lt.HINNUM)
                Dim insdtmstr = result.INSDTMSTR
                Dim insdtm = result.INSDTM
                Dim udnakn = CInt(lt.URIBTK) - lt.ZEITK
                Dim udnbkn = CInt(lt.URIBTK)
                Dim udnzkn = lt.ZEITK
                Dim hinzeikb = "2"
                Dim koteikbn = "0"
                Dim udnkbn = "3"
                Dim hostname = My.Computer.Name
                Dim drakbn = "0"

                Dim tktnma = lt.HINNMA
                Dim hinnma = lt.HINNMA

                ' STA ADD *** 2018/05/10 TERAYAMA
                ' 手入力は商品区分【007】
                If String.IsNullOrEmpty(lt.BUNCDB) Then
                    buncdb = "007"
                End If
                ' END ADD

                ' *** 会員と非会員で異なる項目
                Dim point As Integer
                Dim cpaykbn As String
                Dim premkn As Integer
                If Not custmer Is Nothing Then
                    ' 会員
                    point = result.GETPOINT
                    ' STA MOD *** 2018/05/10 TERAYAMA 打席カード払いはCPAYKBN = 4
                    cpaykbn = "4"
                    ' END MOD
                    premkn = result.PREMKN
                Else
                    ' 非会員
                    point = 0
                    cpaykbn = result.CPAYKBN.ToString
                    premkn = 0
                End If

                sql.Clear()
                sql.Append(" INSERT INTO DENTRA (")
                sql.Append(" DATKB, UDNDT, UDNNO, BMNCD, BUNCDA, BUNCDB, BUNCDC, TKTKBN, UDNKN, TKTSU,")
                sql.Append(" INSDTMSTR, INSDTM, UDNAKN, UDNBKN, UDNZKN, HINZEIKB, POINT, KOTEIKBN,")
                sql.Append(" UDNKBN, CPAYKBN, HOSTNAME, DRAKBN, PREMKN,")

                sql.Append(" LINNO, TKTNMA, HINNMA")
                ' 会員専用
                If Not custmer Is Nothing Then
                    sql.Append(" ,")
                    sql.Append(" MANNO, KSBKB, SCLMANNO, SCLKBN, POZEIKB")
                ElseIf Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                    sql.Append(" ,")
                    sql.Append(" MANNO")
                End If
                sql.Append(" )")
                sql.Append(" VALUES(")
                sql.Append(" '" & datkb & "',")
                sql.Append(" '" & udndt & "',")
                sql.Append(" " & udnno & ",")
                sql.Append(" '" & bmncd & "',")
                sql.Append(" '" & buncda & "',")
                sql.Append(" '" & buncdb & "',")
                sql.Append(" '" & buncdc & "',")
                sql.Append(" '" & tktkbn & "',")
                sql.Append(" " & udnkn & ",")
                sql.Append(" " & tktsu & ",")
                sql.Append(" '" & insdtmstr & "',")
                sql.Append(" '" & insdtm & "',")
                sql.Append(" " & udnakn & ",")
                sql.Append(" " & udnbkn & ",")
                sql.Append(" " & udnzkn & ",")
                sql.Append(" '" & hinzeikb & "',")
                sql.Append(" " & point & ",")
                sql.Append(" '" & koteikbn & "',")
                sql.Append(" '" & udnkbn & "',")
                sql.Append(" '" & cpaykbn & "',")
                sql.Append(" '" & hostname & "',")
                sql.Append(" '" & drakbn & "',")
                sql.Append(" " & premkn & ",")

                sql.Append(" " & linno & ",")
                sql.Append(" '" & tktnma & "',")
                sql.Append(" '" & hinnma & "' ")
                ' 会員専用
                If Not custmer Is Nothing Then
                    sql.Append(" ,")
                    sql.Append(" '" & custmer.NCSNO & "',")
                    sql.Append(" '" & custmer.NCSRANK & "',")
                    sql.Append(" '" & custmer.DSCLMANNO & "',")
                    sql.Append(" '" & custmer.SCLKBN & "',")
                    sql.Append(" '2' ")
                ElseIf Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                    sql.Append(" ,")
                    sql.Append(" '" & Me.lblNCSNO.Text & "'")
                End If
                sql.Append(" )")

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

                linno += 1
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' HINTRNの登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsHINTRN(ByRef result As ResultRegister, Optional ByRef custmer As Custmer = Nothing) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try

            ' *** 共通項目
            Dim datkb = "1"
            Dim udndt = DateTime.Now.ToString("yyyyMMdd")
            Dim udnno = result.UDNNO.TrimStart("0"c)
            Dim insdtm = result.INSDTM
            Dim udnakn = result.UDNAKN
            Dim udnbkn = result.UDNBKN
            Dim udnzkn = result.UDNZKN

            Dim manno As String = "ZZZZZZZZ"
            Dim zanakn As Integer = 0
            Dim zanbkn As Integer = 0
            Dim prezanakn As Integer = 0
            Dim prezanbkn As Integer = 0
            Dim zanapo As Integer = 0
            Dim zanbpo As Integer = 0

            ' *** 会員と非会員で異なる項目
            Dim point As Integer = 0
            If Not custmer Is Nothing Then
                ' 会員

                manno = custmer.NCSNO
                zanakn = custmer.ZANKN
                zanbkn = result.ZANKN
                prezanakn = custmer.PREZANKN
                prezanbkn = result.PREZANKN
                zanapo = CType(dcICR700.POINT, Integer)
                zanbpo = CType(dcICR700.POINT, Integer) + result.GETPOINT
                point = result.GETPOINT
            ElseIf Not String.IsNullOrEmpty(Me.lblNCSNO.Text) Then
                manno = Me.lblNCSNO.Text
            End If

            sql.Clear()
            sql.Append(" INSERT INTO HINTRN (")
            sql.Append(" DATKB, DENDT, DENNO, UDNAKN, UDNBKN, UDNZKN, POINT, MANNO, ZANAKN, ZANBKN,PREZANAKN,PREZANBKN,ZANAPO,ZANBPO,INSDTM")
            sql.Append(" )")
            sql.Append(" VALUES(")
            sql.Append(" '" & datkb & "',")
            sql.Append(" '" & udndt & "',")
            sql.Append(" " & udnno & ",")
            sql.Append(" " & udnakn & ",")
            sql.Append(" " & udnbkn & ",")
            sql.Append(" " & udnzkn & ",")
            sql.Append(" " & point & ",")
            sql.Append(" '" & manno & "',")
            sql.Append(" " & zanakn & ",")
            sql.Append(" " & zanbkn & ",")
            sql.Append(" " & prezanakn & ",")
            sql.Append(" " & prezanbkn & ",")
            sql.Append(" " & zanapo & ",")
            sql.Append(" " & zanbpo & ",")
            sql.Append(" '" & insdtm & "'")
            sql.Append(" )")

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
    ''' SEQTRNの伝票番号の更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSEQTRN() As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")

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
    ''' KINSMAの更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKINSMA(ByRef result As ResultRegister, ByRef custmer As Custmer) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim manno = custmer.NCSNO
            Dim zankn = result.ZANKN
            Dim prezankn = result.PREZANKN

            sql.Clear()
            sql.Append(" UPDATE KINSMA SET")
            sql.Append(" ZANKN = " & zankn)
            sql.Append(" ,PREZANKN = " & prezankn)
            sql.Append(" WHERE MANNO = '" & manno & "'")

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
    ''' DPOINTSMAの更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateDPOINTSMA(ByRef result As ResultRegister, ByRef custmer As Custmer) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim manno = custmer.NCSNO
            Dim point = result.GETPOINT

            sql.Clear()
            sql.Append(" UPDATE DPOINTSMA SET")
            sql.Append(" SRTPO = SRTPO + " & point)
            sql.Append(" WHERE MANNO = '" & manno & "'")

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
    ''' カード有効期限の更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCARDLIMIT(ByRef result As ResultRegister, ByRef custmer As Custmer) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try

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
            sql.Append(" NCSNO = " & CType(custmer.NCSNO, Integer))

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
    ''' 氏名取得
    ''' </summary>
    ''' <param name="intNCSNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCCSNAME(ByVal intNCSNO As Integer) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            If intNCSNO.Equals(0) Then Return False

            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(" FROM CSMAST AS A")

            sql.Append(" WHERE")
            sql.Append(" A.NCARDID = " & intNCSNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Me.lblNCSNO.Text = String.Empty
            Me.lblCCSNAME.Text = String.Empty

            If Not resultDt.Rows.Count.Equals(0) Then
                '顧客番号
                Me.lblNCSNO.Text = resultDt.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)
                '氏名
                Me.lblCCSNAME.Text = resultDt.Rows(0).Item("CCSNAME").ToString
                Return True
            End If

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <param name="NCSNO"></param>
    ''' <param name="NCARDID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCSMAST(ByVal NCSNO As String, Optional ByVal NCARDID As String = Nothing) As Custmer
        Dim sql = New System.Text.StringBuilder
        Try
            If dcICR700.NCSNO.ToString.PadLeft(8, "0"c).Equals("00000000") Then
                Return Nothing
            End If

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
            sql.Append(" LEFT JOIN MANMST AS E ON E.SCLMANNO = A.DSCLMANNO")
            sql.Append(" WHERE")
            If Not NCARDID Is Nothing Then
                sql.Append(" NCARDID = " & CType(NCARDID, Integer))
            Else
                sql.Append(" NCSNO = " & CType(NCSNO, Integer))
            End If

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count <= 0 Then Return Nothing

            If Not NCARDID Is Nothing Then
                If Not NCSNO.Equals("0") Then
                    If Not NCSNO.Equals(resultDt.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)) Then
                        Return Nothing
                    End If
                End If

            End If

            Dim row As DataRow = resultDt.Rows(0)

            Dim data = New Custmer

            '顧客番号
            data.NCSNO = row("NCSNO").ToString.PadLeft(8, "0"c)
            '氏名
            data.CCSNAME = row("CCSNAME").ToString
            'ｶﾅ
            data.CCSKANA = row("CCSKANA").ToString
            '性別
            data.NSEX = row("NSEX").ToString
            '左打ち区分
            data.DLEFTKBN = row("DLEFTKBN").ToString
            '誕生日
            data.DBIRTH = row("TO_DBIRTH").ToString
            '顧客種別
            data.NCSRANK = row("NCSRANK").ToString
            '顧客種別(種別名)
            data.CKBNAME = row("CKBNAME").ToString
            '郵便番号
            If Not String.IsNullOrEmpty(row("NZIP").ToString) Then
                data.NZIP = row("NZIP").ToString.Substring(0, 3) & "-" & row("NZIP").ToString.Substring(3, 4)
            End If
            '住所１
            data.CADDRESS1 = row("CADDRESS1").ToString
            '住所２
            data.CADDRESS2 = row("CADDRESS2").ToString
            '電話番号
            data.CTELEPHONE = row("CTELEPHONE").ToString
            'FAX番号
            data.CFAX = row("CFAX").ToString
            '携帯電話番号
            data.CPOTABLENUM = row("CPOTABLENUM").ToString
            'メールアドレス            
            data.CEMAIL = row("CEMAIL").ToString
            'メール配信区分
            data.CEMAILKBN = row("CEMAILKBN").ToString
            'カード番号
            data.NCARDID = row("NCARDID").ToString.PadLeft(8, "0"c)
            '会員登録日
            data.DENTRY = row("TO_DENTRY").ToString
            '前回来場日
            data.ENTDT = row("ENTDT").ToString
            'メッセージ
            data.MANCOMMENT = row("MANCOMMENT").ToString
            '会員期限
            data.DMEMBER = row("TO_DMEMBER").ToString
            '総来場回数
            data.ENTCNT = CType(row("ENTCNT").ToString, Integer)
            '月間来場回数
            data.ENTCNT2 = CType(row("ENTCNT2").ToString, Integer)
            'カードコメント
            data.MANINFO = row("MANINFO").ToString
            '備考１
            data.BIKO1 = row("BIKO1").ToString
            '備考２
            data.BIKO2 = row("BIKO2").ToString
            '備考３
            data.BIKO3 = row("BIKO3").ToString
            '残金額
            data.ZANKN = CType(row("ZANKN").ToString, Integer)
            'P)残金額
            data.PREZANKN = CType(row("PREZANKN").ToString, Integer)
            '残ポイント
            data.SRTPO = CType(row("SRTPO").ToString, Integer)
            'スクール生番号
            data.DSCLMANNO = row("DSCLMANNO").ToString
            'スクール区分
            data.SCLKBN = row("SCLKBN").ToString
            'カード有効期限
            data.CARDLIMIT = row("CARDLIMIT").ToString

            Return data

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' レジ画面用の伝票番号を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetREGISTER_DENNO() As String
        Dim sql = New System.Text.StringBuilder
        Try

            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEQTRN")
            Dim dt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '最新の更新日時を取得
            _dtUPDDTM_SEQTRN = GetCurrentUPDDTM("SEQTRN")

            If Not dt.Rows.Count > 0 Then
                Return Nothing
            Else
                Dim v = CInt(dt.AsEnumerable.Max(Function(x As DataRow) x("DENNOSEQ"))) + 1
                Return v.ToString.PadLeft(4, "0"c)
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' レジ画面用の商品一覧を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetREGISTER_GOODS() As DataTable
        Try
            Dim dt = New DataTable("GOODS")
            dt.Columns.Add("GDSNAME", GetType(String))
            dt.Columns.Add("GDSCOUNT", GetType(String))
            dt.Columns.Add("GDSTAX", GetType(String))
            dt.Columns.Add("GDSKIN", GetType(String))
            dt.Columns.Add("CPAYKBN", GetType(String))
            For Each row In _list
                Dim dr As DataRow
                dr = dt.NewRow
                dr("GDSNAME") = row.HINNMA
                dr("GDSCOUNT") = row.HINNUM
                dr("GDSTAX") = row.ZEITK.ToString("#,0")
                dr("GDSKIN") = CInt(row.URIBTK).ToString("#,0")
                dr("CPAYKBN") = String.Empty
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 売上情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetURIDETAIL(ByRef dtGOODS As DataTable, ByRef drDUDNTRN As DataRow, ByVal strDENNO As String) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            ' DUDNTRN の取得
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" A.*")
            sql.Append(",B.CCSNAME")
            sql.Append(" FROM")
            sql.Append(" DUDNTRN AS A")
            sql.Append(" LEFT JOIN CSMAST AS B ON LTRIM(TO_CHAR(B.NCSNO,'00000000')) =  A.MANNO")
            sql.Append(String.Format(" WHERE UDNDT = '{0}' AND UDNNO = {1}", UIUtility.SYSTEM.UPDDAY, CInt(strDENNO)))
            Dim dudntrnDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If dudntrnDt.Rows.Count <= 0 Then Return False
            Dim qDudntrn = dudntrnDt.AsEnumerable

            ' DENTRA の取得
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" DENTRA")
            sql.Append(String.Format(" WHERE UDNDT = '{0}' AND UDNNO = {1}", UIUtility.SYSTEM.UPDDAY, CInt(strDENNO)))
            Dim dentraDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If dentraDt.Rows.Count <= 0 Then Return False
            Dim qDentra = dentraDt.AsEnumerable

            ' 売上商品一覧
            dtGOODS = New DataTable("GOODS")
            dtGOODS.Columns.Add("GDSNAME", GetType(String))
            dtGOODS.Columns.Add("GDSCOUNT", GetType(String))
            dtGOODS.Columns.Add("GDSTAX", GetType(String))
            dtGOODS.Columns.Add("GDSKIN", GetType(String))
            dtGOODS.Columns.Add("CPAYKBN", GetType(String))
            dtGOODS.Columns.Add("DATKB", GetType(String))

            ' 商品テーブルを作成
            For Each row As DataRow In qDentra
                Dim dr As DataRow
                dr = dtGOODS.NewRow
                dr("GDSNAME") = row.Item("HINNMA").ToString
                dr("GDSCOUNT") = row.Item("TKTSU").ToString
                dr("GDSTAX") = CType(row.Item("UDNZKN").ToString, Integer).ToString("#,##0")
                dr("GDSKIN") = CType(row.Item("UDNBKN").ToString, Integer).ToString("#,##0")
                dr("CPAYKBN") = String.Empty
                dr("DATKB") = row.Item("DATKB").ToString
                dtGOODS.Rows.Add(dr)
            Next

            drDUDNTRN = qDudntrn.First()

            Dim drGoods As DataRow()

            drGoods = dtGOODS.Select()

            drGoods(0).Item("CPAYKBN") = drDUDNTRN.Item("CPAYKBN").ToString
            'If Not String.IsNullOrEmpty(drDUDNTRN.Item("MANNO").ToString) Then
            '    drGoods(0).Item("CPAYKBN") = "98"
            'Else
            '    drGoods(0).Item("CPAYKBN") = drDUDNTRN.Item("CPAYKBN").ToString
            'End If
            drGoods(0).EndEdit()

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' カード読込処理と顧客情報の取得
    ''' </summary>
    ''' <param name="custmer"></param>
    ''' <remarks></remarks>
    Private Function RequestCard(ByRef custmer As Custmer, Optional ByVal manno As String = "0") As Boolean
        Try
            'カード読み込み
            Dim blnERRSHOPFLG As Boolean
            Using frm As New frmREQUESTCARD(dcICR700, iDatabase)
                frm.COMMAND = frmREQUESTCARD.Command_Type.READ
                frm.ShowDialog()
                If frm.CANCEL Then Return False
                If frm.ERRFLG Then Return False
                blnERRSHOPFLG = frm.ERRSHOPFLG
            End Using
            '店番エラー発生
            If blnERRSHOPFLG Then
                Using frm As New frmMSGBOX01("店番が一致しません。", 2)
                    frm.ShowDialog()
                End Using
                EjectCard()
                Return False
            End If

            custmer = GetCSMAST(manno, dcICR700.NCSNO)

            If custmer Is Nothing Then
                Using frm As New frmMSGBOX01("顧客データがありません。", 3)
                    frm.ShowDialog()
                End Using
                EjectCard()
                Return False
            End If

            If Not UIUtility.SYSTEM.CARDLIMIT.Equals(0) Then
                If Not String.IsNullOrEmpty(custmer.CARDLIMIT) Then
                    Dim dtCARDLIMIT As DateTime = DateTime.Parse(custmer.CARDLIMIT.ToString.Substring(0, 4) & "/" & custmer.CARDLIMIT.ToString.Substring(4, 2) & "/" & custmer.CARDLIMIT.ToString.Substring(6, 2))
                    ' 入金残高有効期限
                    Dim intPREMLIMIT As Integer = 0
                    Dim strCARDLIMIT As String = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    If Not UIUtility.SYSTEM.PREMLIMIT.Equals(0) Then
                        intPREMLIMIT = CType("-" & UIUtility.SYSTEM.PREMLIMIT, Integer)
                        dtCARDLIMIT = dtCARDLIMIT.AddMonths(intPREMLIMIT)
                        strCARDLIMIT = dtCARDLIMIT.ToString("yyyy/MM/dd")
                    End If
                    If Now.ToString("yyyyMMdd") > custmer.CARDLIMIT Or Now.ToString("yyyy/MM/dd") > strCARDLIMIT Then
                        Using frm As New frmMSGBOX01("有効期限が切れています。" & vbCrLf & "※受付画面から期限を確認してください。※", 3)
                            frm.ShowDialog()
                        End Using
                        EjectCard()
                        Return False
                    End If
                End If
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' カードの書き込み
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WriteCard(ByVal data As PriceDataSet, ByVal custmer As Custmer) As Boolean
        Try
            ' *** プリカRW書き込み情報セット
            '店番号
            dcICR700.SHOPNO_WR = dcICR700.SHOPNO
            'パスワード
            dcICR700.PASSCD_WR = dcICR700.PASSCD
            'シリアルナンバー
            dcICR700.SERIALNO_WR = dcICR700.SERIALNO
            '種別
            dcICR700.SYUBETU_WR = dcICR700.SYUBETU
            '金額
            dcICR700.KINGAKU_WR = ((custmer.ZANKN + custmer.PREZANKN) + (data.ADD_ZANKN + data.ADD_PREMKN)).ToString.PadLeft(5, "0"c)
            '予備
            dcICR700.YOBI_WR = dcICR700.YOBI

            ' *** V31RW書き込み情報セット
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
            dcICR700.ZANKN_WR = (custmer.ZANKN + data.ADD_ZANKN).ToString.PadLeft(5, "0"c)
            'P残金額
            dcICR700.PREZANKN_WR = (custmer.PREZANKN + data.ADD_PREMKN).ToString.PadLeft(5, "0"c)
            '残ポイント
            dcICR700.POINT_WR = (CType(dcICR700.POINT, Integer) + data.ADD_POINT).ToString.PadLeft(5, "0"c)
            '入場区分
            dcICR700.ENTKBN_WR = dcICR700.ENTKBN
            'ボール単価
            dcICR700.BALLKIN_WR = dcICR700.BALLKIN
            '打席番号
            dcICR700.SEATNO_WR = dcICR700.SEATNO

            Dim blnERRFLG As Boolean = False
            Using frm As New frmREQUESTCARD(dcICR700)
                'カードライト
                frm.COMMAND = frmREQUESTCARD.Command_Type.WRITE
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' カード排出処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EjectCard()
        Try
            Using frm As New frmREQUESTCARD(dcICR700)
                frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
                frm.ShowDialog()
            End Using
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

#End Region

#Region "▼ 汎用関数定義"

    ''' <summary>
    ''' 他端末からの更新チェック(TRUE:更新あり FALSE:更新なし)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckUPDDTM(ByVal table As String, ByRef oldDtUPDDTM As DateTime) As Boolean
        Try
            Dim dtUPDDTM = GetCurrentUPDDTM(table)
            Return Not dtUPDDTM.Equals(Nothing) And Not dtUPDDTM.Equals(oldDtUPDDTM)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 現在の最新更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentUPDDTM(ByVal table As String) As DateTime
        Try

            Dim sql As New System.Text.StringBuilder
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM " & table)
            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If resultDt.Rows.Count > 0 Then
                Dim drSelectRow() As DataRow = resultDt.Select
                Return DirectCast(drSelectRow(0).Item("UPDDTM"), DateTime)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' エラーメッセージの表示
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Private Sub ShowErrorMessage(ByVal msg As String)
        MessageBox.Show(msg, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

#Region "▼ 電卓型入力フォーム"

    ''' <summary>
    ''' ボタン_電卓型入力フォーム
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, _
        btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn0.Click, btn00.Click
        Dim btn = CType(sender, Button)
        If Not txtPrice.Text Is Nothing Then
            txtPrice.Focus()
            InputValue(txtPrice, btn.Text)
            btn.Focus()
        End If
    End Sub
    Private Sub btnCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLEAR.Click
        txtPrice.Focus()
        txtPrice.Text = "0"
        btnCLEAR.Focus()
    End Sub
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        txtPrice.Focus()
        txtPrice.Text = txtPrice.Text.Replace(",", "")
        txtPrice.Text = txtPrice.Text.Substring(0, txtPrice.Text.Length - 1)
        If String.IsNullOrEmpty(txtPrice.Text) Then
            txtPrice.Text = "0"
        End If
        btnBACK.Focus()
    End Sub

    ''' <summary>
    ''' 先頭の0を削除してボタンの値をテキストボックスへ挿入
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Private Sub InputValue(ByRef txt As BaseControl.CustomTextBoxNum, ByVal value As String)
        Try
            txt.Text = txt.Text.TrimStart("0"c)
            If txt.TextLength < txt.MaxLength Then
                txt.Text &= value
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼ 入力制御とフォーマット"

    Private Sub dgvHINMTA_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvHINMTA.EditingControlShowing
        Try
            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then
                Dim dgv As DataGridView = CType(sender, DataGridView)

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                'イベントハンドラ削除
                RemoveHandler tb.KeyPress, AddressOf dgvs_KeyPress

                'KeyPressイベントハンドラ追加
                Select Case dgv.CurrentCell.OwningColumn.Index
                    Case 1
                        AddHandler tb.KeyPress, AddressOf dgvs_KeyPress
                    Case 2
                        AddHandler tb.KeyPress, AddressOf dgvs_KeyPress
                End Select

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvs_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
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
    ''' グリッドビューのフォーカス状態でReadOnlyセルのフォーカス制御を行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvHINMTA_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTA.Enter
        _dataGridViewFocused = True
    End Sub
    Private Sub dgvHINMTA_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHINMTA.Leave
        _dataGridViewFocused = False
    End Sub

    ''' <summary>
    ''' グリッド_CellEnter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvs_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles dgvHINMTA.CellEnter
        Try
            ' グリッドビューのフォーカス状態で以降の処理を行うか制御する
            If Not _dataGridViewFocused Then Return

            Dim dgv = CType(sender, DataGridView)

            ' IME制御
            Select Case e.ColumnIndex
                Case 1
                    dgv.ImeMode = Windows.Forms.ImeMode.Off
                Case 2
                    dgv.ImeMode = Windows.Forms.ImeMode.Off
            End Select

            ' テキストを選択状態にする
            dgv.BeginEdit(True)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_入力終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvHINMTA_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles dgvHINMTA.CellEndEdit
        Try
            If Not _bInited Then Return

            Dim dgv = CType(sender, DataGridView)

            ' 入力が終わったら実行する処理
            CalcPrice()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' グリッド_フォーマット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvs_CellFormatting(ByVal sender As Object, _
            ByVal e As DataGridViewCellFormattingEventArgs) _
            Handles dgvHINMTA.CellFormatting
        Try
            If e.ColumnIndex = 1 Or e.ColumnIndex = 2 Then
                ' 空白は無視
                If String.IsNullOrEmpty(CStr(e.Value)) Then
                    Return
                End If
                ' カンマ表示
                If Not String.IsNullOrEmpty(e.Value.ToString) Then
                    e.Value = CInt(e.Value).ToString("#,0")
                End If
                'フォーマットの必要がないことを知らせる
                e.FormattingApplied = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region




End Class

