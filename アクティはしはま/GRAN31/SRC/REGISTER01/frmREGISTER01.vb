Imports TECHNO.DataBase
Imports System.ComponentModel

Public Class frmREGISTER01

    ' デバッグ
    Private Const _DEBUG As Boolean = False

    ''' <summary>
    ''' テストデータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Test()
        _DENNO = "000001"
        _NCSNO = Nothing
        _CCSNAME = "テクノ太郎"
        _CCSKANA = "ﾃｸﾉﾀﾛｳ"
        _CKBNAME = "種別1"
        _SCLMANNO = "000001"
        _DMEMBER = "2017/12/31"
        _DBIRTH = "2000/01/01"
        _ZANKN = "10000"
        _PREZANKN = "10000"
        _POINT = "10000"
        _PAYMENT = 1000
        _DEPOSIT = 10000
        _CHANGE = _DEPOSIT - _PAYMENT
        _GETPREMKN = 10000
        _GETPOINT = 10000
        _RECEIPT = True
        _HINFLG = True

        _GOODS = New DataTable("GOODS")
        _GOODS.Columns.Add("GDSNAME", GetType(String))
        _GOODS.Columns.Add("GDSCOUNT", GetType(Integer))
        _GOODS.Columns.Add("GDSTAX", GetType(Double))
        _GOODS.Columns.Add("GDSKIN", GetType(Integer))

        For i = 1 To 10
            Dim dr As DataRow
            dr = _GOODS.NewRow
            dr("GDSNAME") = "商品" & i
            dr("GDSCOUNT") = 1 * i
            dr("GDSTAX") = 0.08
            dr("GDSKIN") = 1000 * i * 1.08
            _GOODS.Rows.Add(dr)
        Next

    End Sub

#Region "▼宣言部"

    ' 書き込み専用
    Private _DENNO As String    ' 伝票番号
    Private _NCSNO As String    ' 顧客番号
    Private _CCSNAME As String  ' 氏名
    Private _CCSKANA As String  ' カナ
    Private _CKBNAME As String  ' 顧客種別
    Private _SCLMANNO As String ' スクール生番号
    Private _DMEMBER As String  ' 会員期限
    Private _DBIRTH As String   ' 誕生日
    Private _ZANKN As String    ' 残金
    Private _PREZANKN As String ' プレミアム
    Private _POINT As String    ' ポイント
    Private _GOODS As DataTable ' 売上商品一覧
    Private _PAYMENT As Integer ' 現金
    Private _HINFLG As Boolean  ' 商品フラグ
    Private _FEECARDFLG As Boolean  '年会費打席カード精算フラグ

    ' 読み取り専用
    Private _DEPOSIT As Integer ' 預かり金
    Private _CHANGE As Integer  ' 釣銭
    Private _CPAYKBN As Integer ' 支払い区分
    Private _CANCEL As Boolean  ' キャンセル

    ' 読み書き可
    Private _GETPREMKN As Integer ' 取得プレミアム
    Private _GETPOINT As Integer  ' 取得ポイント
    Private _RECEIPT As Boolean   ' レシート出力区分

    ' フォーカス中のテキストボックス
    Dim _currentTextBox As TextBox

#End Region

#Region "▼プロパティ"

    ' プロパティ
    Public WriteOnly Property DENNO As String
        Set(ByVal value As String)
            _DENNO = value
        End Set
    End Property

    Public WriteOnly Property NCSNO As String
        Set(ByVal value As String)
            _NCSNO = value
        End Set
    End Property

    Public WriteOnly Property CCSNAME As String
        Set(ByVal value As String)
            _CCSNAME = value
        End Set
    End Property

    Public WriteOnly Property CCSKANA As String
        Set(ByVal value As String)
            _CCSKANA = value
        End Set
    End Property

    Public WriteOnly Property CKBNAME As String
        Set(ByVal value As String)
            _CKBNAME = value
        End Set
    End Property

    Public WriteOnly Property SCLMANNO As String
        Set(ByVal value As String)
            _SCLMANNO = value
        End Set
    End Property

    Public WriteOnly Property DMEMBER As String
        Set(ByVal value As String)
            _DMEMBER = value
        End Set
    End Property

    Public WriteOnly Property DBIRTH As String
        Set(ByVal value As String)
            _DBIRTH = value
        End Set
    End Property

    Public WriteOnly Property ZANKN As String
        Set(ByVal value As String)
            _ZANKN = value
        End Set
    End Property

    Public WriteOnly Property PREZANKN As String
        Set(ByVal value As String)
            _PREZANKN = value
        End Set
    End Property

    Public WriteOnly Property POINT As String
        Set(ByVal value As String)
            _POINT = value
        End Set
    End Property

    Public WriteOnly Property GOODS As DataTable
        Set(ByVal value As DataTable)
            _GOODS = value
        End Set
    End Property

    Public WriteOnly Property PAYMENT As Integer
        Set(ByVal value As Integer)
            _PAYMENT = value
        End Set
    End Property

    Public WriteOnly Property HINFLG As Boolean
        Set(ByVal value As Boolean)
            _HINFLG = value
        End Set
    End Property

    Public WriteOnly Property FEECARDFLG As Boolean
        Set(ByVal value As Boolean)
            _FEECARDFLG = value
        End Set
    End Property

    Public ReadOnly Property DEPOSIT As Integer
        Get
            Return _DEPOSIT
        End Get
    End Property

    Public ReadOnly Property CHANGE As Integer
        Get
            Return _CHANGE
        End Get
    End Property

    Public ReadOnly Property CPAYKBN As Integer
        Get
            Return _CPAYKBN
        End Get
    End Property

    Public ReadOnly Property CANCEL As Boolean
        Get
            Return _CANCEL
        End Get
    End Property

    Public Property GETPREMKN As Integer
        Get
            Return _GETPREMKN
        End Get
        Set(ByVal value As Integer)
            _GETPREMKN = value
        End Set
    End Property

    Public Property GETPOINT As Integer
        Get
            Return _GETPOINT
        End Get
        Set(ByVal value As Integer)
            _GETPOINT = value
        End Set
    End Property

    Public Property RECEIPT As Boolean
        Get
            Return _RECEIPT
        End Get
        Set(ByVal value As Boolean)
            _RECEIPT = value
        End Set
    End Property

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.Text = "商品引き落とし画面表示"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
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
    Private Sub frmETPMT01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' テストデータの作成
            If _DEBUG Then Test()
            
            ' 初期化
            Init()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 現在の入力フィールドを取得
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDEPOSIT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles txtDEPOSIT.Enter, txtGETPREMKN.Enter, txtGETPOINT.Enter
        Dim obj = CType(txtDEPOSIT, BaseControl.CustomTextBoxNum)
        OnEnterTextBox(obj)
    End Sub    

    ''' <summary>
    ''' テキストボックス_預かり金入力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDEPOSIT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDEPOSIT.TextChanged
        Calc()
    End Sub

    ''' <summary>
    ''' ボタン_預かり金入力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn0.Click, btn00.Click
        Dim btn = CType(sender, Button)
        If Not GetCurrentTextBox() Is Nothing Then
            GetCurrentTextBox.Focus()
            InputValue(GetCurrentTextBox, btn.Text)
            btn.Focus()
        End If
    End Sub
    Private Sub btnCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLEAR.Click
        GetCurrentTextBox.Focus()
        GetCurrentTextBox.Text = "0"
        btnCLEAR.Focus()
    End Sub
    Private Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        GetCurrentTextBox.Focus()
        GetCurrentTextBox.Text = GetCurrentTextBox.Text.Replace(",", "")
        GetCurrentTextBox.Text = GetCurrentTextBox.Text.Substring(0, GetCurrentTextBox.Text.Length - 1)
        If String.IsNullOrEmpty(GetCurrentTextBox.Text) Then
            GetCurrentTextBox.Text = "0"
        End If
        btnBACK.Focus()
    End Sub

    ''' <summary>
    ''' チェックボックス_単一選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chbCPAYKBN1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCPAYKBN1.CheckedChanged
        If chbCPAYKBN1.Checked Then
            chbCPAYKBN2.Checked = False
            chbCPAYKBN3.Checked = False
        End If
    End Sub
    Private Sub chbCPAYKBN2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCPAYKBN2.CheckedChanged
        If chbCPAYKBN2.Checked Then
            chbCPAYKBN1.Checked = False
            chbCPAYKBN3.Checked = False
        End If
    End Sub
    Private Sub chbCPAYKBN3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCPAYKBN3.CheckedChanged
        If chbCPAYKBN3.Checked Then
            chbCPAYKBN1.Checked = False
            chbCPAYKBN2.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Cボタン_各種ポイントの0クリア
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGETPREMKN_CLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGETPREMKN_CLEAR.Click
        txtGETPREMKN.Text = "0"
    End Sub
    Private Sub btnGETPOINT_CLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGETPOINT_CLEAR.Click
        txtGETPOINT.Text = "0"
    End Sub

    ''' <summary>
    ''' ボタン_清算
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSEISAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSEISAN.Click

        ' 2.レシート出力区分
        _RECEIPT = chbRECEIPT.Checked

        ' 15.預かり金
        _DEPOSIT = TextToInt(txtDEPOSIT.Text)

        ' 16.釣銭
        _CHANGE = TextToInt(txtCHANGE.Text)

        ' 預かり金が0以外のとき、預かり金が現金に満たない場合警告表示
        If _DEPOSIT >= 0 And _PAYMENT > _DEPOSIT Then
            Using frm As New frmMSGBOX01("預かり金が不足しています。", 2)
                frm.ShowDialog()
            End Using
            Exit Sub
        End If

        ' 預かり金が未入力または0の場合、[預かり金=現金]となり[釣銭=0]になる
        If _DEPOSIT = 0 Then
            'txtDEPOSIT.Text = txtPAYMENT.Text
            _DEPOSIT = _PAYMENT
            _CHANGE = 0
        End If

        ' 17.支払い区分(初期値0)
        _CPAYKBN = 0
        If chbCPAYKBN1.Checked Then _CPAYKBN = 1
        If chbCPAYKBN2.Checked Then _CPAYKBN = 2
        If chbCPAYKBN3.Checked Then _CPAYKBN = 3

        '' *** STA ADD 2018/05/09 TERAYAMA カード払い清算ウィンドウの表示
        '' カード払い清算ウィンドウの表示
        'If CInt(Me.txtPAYMENT.Text) >= 0 Then
        '    If CInt(Me.txtDEPOSIT.Text) <= 0 Then
        '        ' カード払い、商品券、銀行振込のいずれかにチェックが入っていたら表示しない
        '        ' カード払い、商品券、銀行振込のいずれかが非表示だったら表示しない
        '        Dim flg1 = False
        '        Dim flg2 = False
        '        flg1 = Me.chbCPAYKBN1.Checked Or Me.chbCPAYKBN2.Checked Or Me.chbCPAYKBN3.Checked
        '        flg2 = Not Me.chbCPAYKBN1.Visible Or Not Me.chbCPAYKBN2.Visible Or Not Me.chbCPAYKBN3.Visible
        '        If Not flg1 And Not flg2 Then
        '            Using frm As New frmConfirmation
        '                frm.ShowDialog()
        '                If frm.Reply Then
        '                    _CPAYKBN = 1
        '                Else
        '                    Exit Sub
        '                End If
        '            End Using
        '        End If
        '    End If
        'End If
        '' *** END ADD

        ' 18.取得プレミアム
        _GETPREMKN = TextToInt(txtGETPREMKN.Text)

        ' 19.取得ポイント
        _GETPOINT = TextToInt(txtGETPOINT.Text)

        ' 20.キャンセル
        _CANCEL = False

        Me.Close()
    End Sub

    ''' <summary>
    ''' ボタン_戻る
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRETURN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRETURN.Click
        ' 2.キャンセル
        _CANCEL = True

        Me.Close()
    End Sub

    ''' <summary>
    ''' ファンクションキー割り当て
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnF_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                ' キャンセル
                btnRETURN.PerformClick()
            Case Keys.F12
                ' 清算
                btnSEISAN.PerformClick()
            Case Keys.Enter
                ' タブ送り
                If Not e.Alt AndAlso Not e.Control Then
                    Me.ProcessTabKey(Not e.Shift)
                    e.Handled = True
                    e.SuppressKeyPress = True
                End If
        End Select
    End Sub

    ''' <summary>
    ''' 閉じるボタン無効可
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim params As System.Windows.Forms.CreateParams = MyBase.CreateParams
            params.ClassStyle = params.ClassStyle Or CS_NOCLOSE

            Return params
        End Get
    End Property

#End Region

#Region "▼ 入力制御とフォーマット"

#End Region

#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            ' 項目の表示
            txtDENNO.Text = _DENNO
            txtNCSNO.Text = _NCSNO
            txtCCSNAME.Text = _CCSNAME
            txtCCSKANA.Text = _CCSKANA
            txtCKBNAME.Text = _CKBNAME
            txtSCLMANNO.Text = _SCLMANNO
            txtDMEMBER.Text = _DMEMBER
            txtDBIRTH.Text = _DBIRTH
            txtZANKN.Text = _ZANKN
            txtPREZANKN.Text = _PREZANKN
            txtPOINT.Text = _POINT
            txtPAYMENT.Text = _PAYMENT.ToString
            txtDEPOSIT.Text = _DEPOSIT.ToString
            txtCHANGE.Text = _CHANGE.ToString
            txtGETPREMKN.Text = _GETPREMKN.ToString
            txtGETPOINT.Text = _GETPOINT.ToString
            chbRECEIPT.Checked = _RECEIPT
            dgvGOODS.DataSource = _GOODS

            ' カンマ表示
            ToCammaText(txtZANKN)
            ToCammaText(txtPREZANKN)
            ToCammaText(txtPOINT)
            ToCammaText(txtPAYMENT)
            ToCammaText(txtDEPOSIT)
            ToCammaText(txtCHANGE)
            ToCammaText(txtGETPREMKN)
            ToCammaText(txtGETPOINT)

            'チェックボックスの非表示()
            If _HINFLG And Not _NCSNO Is Nothing Then
                Me.chbCPAYKBN1.Visible = False
                Me.chbCPAYKBN2.Visible = False
                Me.chbCPAYKBN3.Visible = False
                Me.txtDEPOSIT.Text = Me.txtPAYMENT.Text
            End If

            ' 取得プレミアムの非表示
            If _HINFLG Then
                Me.lblGETPREMKN.Visible = False
                Me.txtGETPREMKN.Visible = False
                Me.btnGETPREMKN_CLEAR.Visible = False
            End If

            ' 取得ポイントの非表示
            If _HINFLG And _NCSNO Is Nothing Then
                Me.lblGETPOINT.Visible = False
                Me.txtGETPOINT.Visible = False
                Me.btnGETPOINT_CLEAR.Visible = False
            End If

            '　年会費打席カード精算の場合
            If _FEECARDFLG Then
                Me.chbCPAYKBN1.Visible = False
                Me.chbCPAYKBN2.Visible = False
                Me.chbCPAYKBN3.Visible = False
                Me.lblGETPREMKN.Visible = False
                Me.txtGETPREMKN.Visible = False
                Me.btnGETPREMKN_CLEAR.Visible = False
                Me.lblGETPOINT.Visible = False
                Me.txtGETPOINT.Visible = False
                Me.btnGETPOINT_CLEAR.Visible = False
                Me.txtDEPOSIT.Text = Me.txtPAYMENT.Text
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックスの内容をカンマ表示する
    ''' </summary>
    ''' <param name="txt"></param>
    ''' <remarks></remarks>
    Private Sub ToCammaText(ByVal txt As TextBox)
        Try
            If String.IsNullOrEmpty(txt.Text) Then
                txt.Text = String.Empty
            Else
                txt.Text = CInt(txt.Text).ToString("#,0")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 文字列の数値を整数型に変換する
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TextToInt(ByVal str As String) As Integer
        Try
            str = str.Trim
            str.Replace(",", "")
            Return CInt(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 先頭の0を削除してボタンの値をテキストボックスへ挿入
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Private Sub InputValue(ByRef txt As TextBox, ByVal value As String)
        Try
            txt.Text = txt.Text.TrimStart("0"c)
            If txt.TextLength < txt.MaxLength Then
                txt.Text &= value
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 釣銭計算
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Calc()
        Try
            If Not String.IsNullOrEmpty(txtDEPOSIT.Text) Then
                Dim deposit = CInt(txtDEPOSIT.Text.Replace(",", ""))
                Dim payment = CInt(txtPAYMENT.Text.Replace(",", ""))
                Dim change = deposit - payment
                txtCHANGE.Text = change.ToString("#,0")
                If change >= 0 Then
                    txtCHANGE.ForeColor = Color.Black
                Else
                    txtCHANGE.ForeColor = Color.Red
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 現在フォーカス中のテキストボックスを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentTextBox() As TextBox
        Return _currentTextBox
    End Function

    ''' <summary>
    ''' テキストボックス_Inフォーカス処理
    ''' </summary>
    ''' <param name="_txt"></param>
    ''' <remarks></remarks>
    Private Sub OnEnterTextBox(ByVal _txt As TextBox)
        Dim txts() = {txtDEPOSIT, txtGETPREMKN, txtGETPOINT}
        For Each txt In txts
            If txt.Name = _txt.Name Then
                'txt.SelectionLength = 0
                'txt.BackColor = SystemColors.Highlight
                'txt.ForeColor = SystemColors.Window
                _currentTextBox = txt
            Else
                'txt.BackColor = SystemColors.Window
                'txt.ForeColor = SystemColors.ControlText
            End If
        Next
    End Sub

#End Region

End Class
