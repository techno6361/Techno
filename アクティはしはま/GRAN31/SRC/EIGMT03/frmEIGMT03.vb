Imports TECHNO.DataBase

Public Class frmEIGMT03

#Region "▼宣言部"

    ''' <summary>
    ''' 選択中の料金体系
    ''' </summary>
    ''' <remarks></remarks>
    Private _intRKNKB As Integer = 1
    Private _intRKNKB_Copy As Integer = 1
    ''' <summary>
    ''' 選択中のフロア数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intFLRKB As Integer = 1
    Private _intFLRKB_Copy As Integer = 1

    Private Const _cstClrMoji As String = "--"

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region


#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "営業情報登録(カゴ貸し)"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "営業情報登録(カゴ貸し)"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
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
    Private Sub frmEIGMT03_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()

            '料金体系マスタ情報取得
            If Not GetRKNMTA() Then
                Using frm As New frmMSGBOX01("料金体系マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'コピー元顧客種別情報取得
            If Not GetCopyKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '営業情報取得
            If Not GetNkbEIGMTC() Then
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 料金体系ラジオボタン_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdoRKNKB_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoRKNKB1.CheckedChanged, rdoRKNKB2.CheckedChanged, rdoRKNKB3.CheckedChanged, rdoRKNKB4.CheckedChanged, rdoRKNKB5.CheckedChanged, rdoRKNKB6.CheckedChanged
        Try
            Me.rdoRKNKB1.BackColor = Color.Transparent
            Me.rdoRKNKB2.BackColor = Color.Transparent
            Me.rdoRKNKB3.BackColor = Color.Transparent
            Me.rdoRKNKB4.BackColor = Color.Transparent
            Me.rdoRKNKB5.BackColor = Color.Transparent
            Me.rdoRKNKB6.BackColor = Color.Transparent

            If rdoRKNKB1.Checked Then
                Me.rdoRKNKB1.BackColor = Color.Yellow
                _intRKNKB = 1
            ElseIf rdoRKNKB2.Checked Then
                Me.rdoRKNKB2.BackColor = Color.Yellow
                _intRKNKB = 2
            ElseIf rdoRKNKB3.Checked Then
                Me.rdoRKNKB3.BackColor = Color.Yellow
                _intRKNKB = 3
            ElseIf rdoRKNKB4.Checked Then
                Me.rdoRKNKB4.BackColor = Color.Yellow
                _intRKNKB = 4
            ElseIf rdoRKNKB5.Checked Then
                Me.rdoRKNKB5.BackColor = Color.Yellow
                _intRKNKB = 5
            ElseIf rdoRKNKB6.Checked Then
                Me.rdoRKNKB6.BackColor = Color.Yellow
                _intRKNKB = 6
            End If

            'テキストクリア
            ClrText()

            If Not Me.cmbKBMAST.SelectedIndex < 0 Then
                Me.cmbKBMAST.SelectedIndex = 0
            End If
            Me.rdoFLRKB1.Checked = True

            '営業情報取得
            If Not GetNkbEIGMTC() Then
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フロアラジオボタン_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdoFLRKB_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoFLRKB1.CheckedChanged, rdoFLRKB2.CheckedChanged, rdoFLRKB3.CheckedChanged
        Try
            Me.rdoFLRKB1.BackColor = Color.Transparent
            Me.rdoFLRKB2.BackColor = Color.Transparent
            Me.rdoFLRKB3.BackColor = Color.Transparent

            If rdoFLRKB1.Checked Then
                Me.rdoFLRKB1.BackColor = Color.Yellow
                _intFLRKB = 1
            ElseIf rdoFLRKB2.Checked Then
                Me.rdoFLRKB2.BackColor = Color.Yellow
                _intFLRKB = 2
            ElseIf rdoFLRKB3.Checked Then
                Me.rdoFLRKB3.BackColor = Color.Yellow
                _intFLRKB = 3
            End If

            'テキストクリア
            ClrText()

            '営業情報取得
            If Not GetNkbEIGMTC() Then
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' コピーボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy.Click
        Try
            Dim Msg As String
            Msg = "選択した項目で営業情報を" & vbCrLf & "コピーしてよろしいですか？"
            Using frm As New frmMSGBOX01(Msg, 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            If Not (Me.rdoRKNKB1_Copy.Checked Or Me.rdoRKNKB2_Copy.Checked Or Me.rdoRKNKB3_Copy.Checked Or Me.rdoRKNKB4_Copy.Checked Or Me.rdoRKNKB5_Copy.Checked Or Me.rdoRKNKB6_Copy.Checked) Then
                Using frm As New frmMSGBOX01("コピー元料金体系を選択して下さい。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If Not (Me.rdoFLRKB1_Copy.Checked Or Me.rdoFLRKB2_Copy.Checked Or Me.rdoFLRKB3_Copy.Checked) Then
                Using frm As New frmMSGBOX01("コピー元フロア数を選択して下さい。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            If Me.rdoRKNKB1_Copy.Checked Then _intRKNKB_Copy = 1
            If Me.rdoRKNKB2_Copy.Checked Then _intRKNKB_Copy = 2
            If Me.rdoRKNKB3_Copy.Checked Then _intRKNKB_Copy = 3
            If Me.rdoRKNKB4_Copy.Checked Then _intRKNKB_Copy = 4
            If Me.rdoRKNKB5_Copy.Checked Then _intRKNKB_Copy = 5
            If Me.rdoRKNKB6_Copy.Checked Then _intRKNKB_Copy = 6

            If Me.rdoFLRKB1_Copy.Checked Then _intFLRKB_Copy = 1
            If Me.rdoFLRKB2_Copy.Checked Then _intFLRKB_Copy = 2
            If Me.rdoFLRKB3_Copy.Checked Then _intFLRKB_Copy = 3

            ClrText()

            '営業情報取得
            GetNkbEIGMTC(True)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別コンボボックス_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbKBMAST_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbKBMAST.SelectedIndexChanged
        Try

            ClrText()

            '営業情報取得
            If Not GetNkbEIGMTC() Then
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

#Region "▼時間帯テキストボックス_Validated"
    ''' <summary>
    ''' 時間帯テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM_01.Validated, txtTIMENM_02.Validated, txtTIMENM_03.Validated, txtTIMENM_04.Validated, txtTIMENM_05.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM_01"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD_01.ReadOnly = True
                        Me.txtPASSCD_01.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN_01.Visible = False
                        'ポイント
                        Me.txtPOINT_01.Visible = False
                    Case "txtTIMENM_02"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD_02.ReadOnly = True
                        Me.txtPASSCD_02.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN_02.Visible = False
                        'ポイント
                        Me.txtPOINT_02.Visible = False
                    Case "txtTIMENM_03"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD_03.ReadOnly = True
                        Me.txtPASSCD_03.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN_03.Visible = False
                        'ポイント
                        Me.txtPOINT_03.Visible = False

                    Case "txtTIMENM_04"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD_04.ReadOnly = True
                        Me.txtPASSCD_04.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN_04.Visible = False
                        'ポイント
                        Me.txtPOINT_04.Visible = False

                    Case "txtTIMENM_05"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD_05.ReadOnly = True
                        Me.txtPASSCD_05.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN_05.Visible = False
                        'ポイント
                        Me.txtPOINT_05.Visible = False

                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM_01"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD_01.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_01.ReadOnly = False
                    If Me.txtPASSCD_01.Text.Equals(_cstClrMoji) Then Me.txtPASSCD_01.Text = "01"
                Case "txtTIMENM_02"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD_02.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_02.ReadOnly = False
                    If Me.txtPASSCD_02.Text.Equals(_cstClrMoji) Then Me.txtPASSCD_02.Text = "01"
                Case "txtTIMENM_03"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD_03.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_03.ReadOnly = False
                    If Me.txtPASSCD_03.Text.Equals(_cstClrMoji) Then Me.txtPASSCD_03.Text = "01"
                Case "txtTIMENM_04"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD_04.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_04.ReadOnly = False
                    If Me.txtPASSCD_04.Text.Equals(_cstClrMoji) Then Me.txtPASSCD_04.Text = "01"
                Case "txtTIMENM_05"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD_05.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_05.ReadOnly = False
                    If Me.txtPASSCD_05.Text.Equals(_cstClrMoji) Then Me.txtPASSCD_05.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼パスワードテキストボックス_Validated"

    ''' <summary>
    ''' パスワードテキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD_01.Validated, txtPASSCD_02.Validated, txtPASSCD_03.Validated, txtPASSCD_04.Validated, txtPASSCD_05.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD_01"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD_01.Text = "01"
                    Case "txtPASSCD_02"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD_02.Text = "01"
                    Case "txtPASSCD_03"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD_03.Text = "01"
                    Case "txtPASSCD_04"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD_04.Text = "01"
                    Case "txtPASSCD_05"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD_05.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD_01"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN_01.Visible = bln
                    Me.txtENTKIN_01.Focus()
                    'ポイント
                    Me.txtPOINT_01.Visible = bln
                    'カゴ１
                    Me.txtBALLSU1_01.Visible = bln
                    Me.txtKAGO1KN_01.Visible = bln
                    Me.txtKAGO1ZEI_01.Visible = bln
                    'カゴ２
                    Me.txtBALLSU2_01.Visible = bln
                    Me.txtKAGO2KN_01.Visible = bln
                    Me.txtKAGO2ZEI_01.Visible = bln
                    'カゴ３
                    Me.txtBALLSU3_01.Visible = bln
                    Me.txtKAGO3KN_01.Visible = bln
                    Me.txtKAGO3ZEI_01.Visible = bln
                    'カゴ４
                    Me.txtBALLSU4_01.Visible = bln
                    Me.txtKAGO4KN_01.Visible = bln
                    Me.txtKAGO4ZEI_01.Visible = bln
                    'カゴ５
                    Me.txtBALLSU5_01.Visible = bln
                    Me.txtKAGO5KN_01.Visible = bln
                    Me.txtKAGO5ZEI_01.Visible = bln
                    'カゴ６
                    Me.txtBALLSU6_01.Visible = bln
                    Me.txtKAGO6KN_01.Visible = bln
                    Me.txtKAGO6ZEI_01.Visible = bln

                Case "txtPASSCD_02"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN_02.Visible = bln
                    Me.txtENTKIN_02.Focus()
                    'ポイント
                    Me.txtPOINT_02.Visible = bln
                    'カゴ１
                    Me.txtBALLSU1_02.Visible = bln
                    Me.txtKAGO1KN_02.Visible = bln
                    Me.txtKAGO1ZEI_02.Visible = bln
                    'カゴ２
                    Me.txtBALLSU2_02.Visible = bln
                    Me.txtKAGO2KN_02.Visible = bln
                    Me.txtKAGO2ZEI_02.Visible = bln
                    'カゴ３
                    Me.txtBALLSU3_02.Visible = bln
                    Me.txtKAGO3KN_02.Visible = bln
                    Me.txtKAGO3ZEI_02.Visible = bln
                    'カゴ４
                    Me.txtBALLSU4_02.Visible = bln
                    Me.txtKAGO4KN_02.Visible = bln
                    Me.txtKAGO4ZEI_02.Visible = bln
                    'カゴ５
                    Me.txtBALLSU5_02.Visible = bln
                    Me.txtKAGO5KN_02.Visible = bln
                    Me.txtKAGO5ZEI_02.Visible = bln
                    'カゴ６
                    Me.txtBALLSU6_02.Visible = bln
                    Me.txtKAGO6KN_02.Visible = bln
                    Me.txtKAGO6ZEI_02.Visible = bln
                Case "txtPASSCD_03"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN_03.Visible = bln
                    Me.txtENTKIN_03.Focus()
                    'ポイント
                    Me.txtPOINT_03.Visible = bln
                    'カゴ１
                    Me.txtBALLSU1_03.Visible = bln
                    Me.txtKAGO1KN_03.Visible = bln
                    Me.txtKAGO1ZEI_03.Visible = bln
                    'カゴ２
                    Me.txtBALLSU2_03.Visible = bln
                    Me.txtKAGO2KN_03.Visible = bln
                    Me.txtKAGO2ZEI_03.Visible = bln
                    'カゴ３
                    Me.txtBALLSU3_03.Visible = bln
                    Me.txtKAGO3KN_03.Visible = bln
                    Me.txtKAGO3ZEI_03.Visible = bln
                    'カゴ４
                    Me.txtBALLSU4_03.Visible = bln
                    Me.txtKAGO4KN_03.Visible = bln
                    Me.txtKAGO4ZEI_03.Visible = bln
                    'カゴ５
                    Me.txtBALLSU5_03.Visible = bln
                    Me.txtKAGO5KN_03.Visible = bln
                    Me.txtKAGO5ZEI_03.Visible = bln
                    'カゴ６
                    Me.txtBALLSU6_03.Visible = bln
                    Me.txtKAGO6KN_03.Visible = bln
                    Me.txtKAGO6ZEI_03.Visible = bln
                Case "txtPASSCD_04"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN_04.Visible = bln
                    Me.txtENTKIN_04.Focus()
                    'ポイント
                    Me.txtPOINT_04.Visible = bln
                    'カゴ１
                    Me.txtBALLSU1_04.Visible = bln
                    Me.txtKAGO1KN_04.Visible = bln
                    Me.txtKAGO1ZEI_04.Visible = bln
                    'カゴ２
                    Me.txtBALLSU2_04.Visible = bln
                    Me.txtKAGO2KN_04.Visible = bln
                    Me.txtKAGO2ZEI_04.Visible = bln
                    'カゴ３
                    Me.txtBALLSU3_04.Visible = bln
                    Me.txtKAGO3KN_04.Visible = bln
                    Me.txtKAGO3ZEI_04.Visible = bln
                    'カゴ４
                    Me.txtBALLSU4_04.Visible = bln
                    Me.txtKAGO4KN_04.Visible = bln
                    Me.txtKAGO4ZEI_04.Visible = bln
                    'カゴ５
                    Me.txtBALLSU5_04.Visible = bln
                    Me.txtKAGO5KN_04.Visible = bln
                    Me.txtKAGO5ZEI_04.Visible = bln
                    'カゴ６
                    Me.txtBALLSU6_04.Visible = bln
                    Me.txtKAGO6KN_04.Visible = bln
                    Me.txtKAGO6ZEI_04.Visible = bln
                Case "txtPASSCD_05"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN_05.Visible = bln
                    Me.txtENTKIN_05.Focus()
                    'ポイント
                    Me.txtPOINT_05.Visible = bln
                    'カゴ１
                    Me.txtBALLSU1_05.Visible = bln
                    Me.txtKAGO1KN_05.Visible = bln
                    Me.txtKAGO1ZEI_05.Visible = bln
                    'カゴ２
                    Me.txtBALLSU2_05.Visible = bln
                    Me.txtKAGO2KN_05.Visible = bln
                    Me.txtKAGO2ZEI_05.Visible = bln
                    'カゴ３
                    Me.txtBALLSU3_05.Visible = bln
                    Me.txtKAGO3KN_05.Visible = bln
                    Me.txtKAGO3ZEI_05.Visible = bln
                    'カゴ４
                    Me.txtBALLSU4_05.Visible = bln
                    Me.txtKAGO4KN_05.Visible = bln
                    Me.txtKAGO4ZEI_05.Visible = bln
                    'カゴ５
                    Me.txtBALLSU5_05.Visible = bln
                    Me.txtKAGO5KN_05.Visible = bln
                    Me.txtKAGO5ZEI_05.Visible = bln
                    'カゴ６
                    Me.txtBALLSU6_05.Visible = bln
                    Me.txtKAGO6KN_05.Visible = bln
                    Me.txtKAGO6ZEI_05.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

    ''' <summary>
    ''' 入場料テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtENTKIN_Validated(sender As System.Object, e As System.EventArgs) Handles txtENTKIN_01.Validated, txtENTKIN_02.Validated, txtENTKIN_03.Validated, txtENTKIN_04.Validated, txtENTKIN_05.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ポイントテキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPOINT_Validated(sender As System.Object, e As System.EventArgs) Handles txtPOINT_01.Validated, txtPOINT_02.Validated, txtPOINT_03.Validated, txtPOINT_04.Validated, txtPOINT_05.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボール数テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBALLSU_Validated(sender As System.Object, e As System.EventArgs) Handles txtBALLSU1_01.Validated, txtBALLSU2_01.Validated, txtBALLSU3_01.Validated, txtBALLSU4_01.Validated, txtBALLSU5_01.Validated, txtBALLSU6_01.Validated _
                                                                                        , txtBALLSU1_02.Validated, txtBALLSU2_02.Validated, txtBALLSU3_02.Validated, txtBALLSU4_02.Validated, txtBALLSU5_02.Validated, txtBALLSU6_02.Validated _
                                                                                        , txtBALLSU1_03.Validated, txtBALLSU2_03.Validated, txtBALLSU3_03.Validated, txtBALLSU4_03.Validated, txtBALLSU5_03.Validated, txtBALLSU6_03.Validated _
                                                                                        , txtBALLSU1_04.Validated, txtBALLSU2_04.Validated, txtBALLSU3_04.Validated, txtBALLSU4_04.Validated, txtBALLSU5_04.Validated, txtBALLSU6_04.Validated _
                                                                                        , txtBALLSU1_05.Validated, txtBALLSU2_05.Validated, txtBALLSU3_05.Validated, txtBALLSU4_05.Validated, txtBALLSU5_05.Validated, txtBALLSU6_05.Validated

        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 単価テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKAGOKN_Validated(sender As System.Object, e As System.EventArgs) Handles txtKAGO1KN_01.Validated, txtKAGO2KN_01.Validated, txtKAGO3KN_01.Validated, txtKAGO4KN_01.Validated, txtKAGO5KN_01.Validated, txtKAGO6KN_01.Validated _
                                                                                        , txtKAGO1KN_02.Validated, txtKAGO2KN_02.Validated, txtKAGO3KN_02.Validated, txtKAGO4KN_02.Validated, txtKAGO5KN_02.Validated, txtKAGO6KN_02.Validated _
                                                                                        , txtKAGO1KN_03.Validated, txtKAGO2KN_03.Validated, txtKAGO3KN_03.Validated, txtKAGO4KN_03.Validated, txtKAGO5KN_03.Validated, txtKAGO6KN_03.Validated _
                                                                                        , txtKAGO1KN_04.Validated, txtKAGO2KN_04.Validated, txtKAGO3KN_04.Validated, txtKAGO4KN_04.Validated, txtKAGO5KN_04.Validated, txtKAGO6KN_04.Validated _
                                                                                        , txtKAGO1KN_05.Validated, txtKAGO2KN_05.Validated, txtKAGO3KN_05.Validated, txtKAGO4KN_05.Validated, txtKAGO5KN_05.Validated, txtKAGO6KN_05.Validated

        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 消費税テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKAGOZEI_Validated(sender As System.Object, e As System.EventArgs) Handles txtKAGO1ZEI_01.Validated, txtKAGO2ZEI_01.Validated, txtKAGO3ZEI_01.Validated, txtKAGO4ZEI_01.Validated, txtKAGO5ZEI_01.Validated, txtKAGO6ZEI_01.Validated _
                                                                                        , txtKAGO1ZEI_02.Validated, txtKAGO2ZEI_02.Validated, txtKAGO3ZEI_02.Validated, txtKAGO4ZEI_02.Validated, txtKAGO5ZEI_02.Validated, txtKAGO6ZEI_02.Validated _
                                                                                        , txtKAGO1ZEI_03.Validated, txtKAGO2ZEI_03.Validated, txtKAGO3ZEI_03.Validated, txtKAGO4ZEI_03.Validated, txtKAGO5ZEI_03.Validated, txtKAGO6ZEI_03.Validated _
                                                                                        , txtKAGO1ZEI_04.Validated, txtKAGO2ZEI_04.Validated, txtKAGO3ZEI_04.Validated, txtKAGO4ZEI_04.Validated, txtKAGO5ZEI_04.Validated, txtKAGO6ZEI_04.Validated _
                                                                                        , txtKAGO1ZEI_05.Validated, txtKAGO2ZEI_05.Validated, txtKAGO3ZEI_05.Validated, txtKAGO4ZEI_05.Validated, txtKAGO5ZEI_05.Validated, txtKAGO6ZEI_05.Validated

        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
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
    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTIMENM_01.KeyPress, txtTIMENM_02.KeyPress, txtTIMENM_03.KeyPress, txtTIMENM_04.KeyPress, txtTIMENM_05.KeyPress _
                                                                                                                        , txtPASSCD_01.KeyPress, txtPASSCD_02.KeyPress, txtPASSCD_03.KeyPress, txtPASSCD_04.KeyPress, txtPASSCD_05.KeyPress _
                                                                                                                        , txtENTKIN_01.KeyPress, txtENTKIN_02.KeyPress, txtENTKIN_03.KeyPress, txtENTKIN_04.KeyPress, txtENTKIN_05.KeyPress _
                                                                                                                        , txtPOINT_01.KeyPress, txtPOINT_02.KeyPress, txtPOINT_03.KeyPress, txtPOINT_04.KeyPress, txtPOINT_05.KeyPress _
                                                                                                                        , txtBALLSU1_01.KeyPress, txtBALLSU2_01.KeyPress, txtBALLSU3_01.KeyPress, txtBALLSU4_01.KeyPress, txtBALLSU5_01.KeyPress, txtBALLSU6_01.KeyPress _
                                                                                                                        , txtBALLSU1_02.KeyPress, txtBALLSU2_02.KeyPress, txtBALLSU3_02.KeyPress, txtBALLSU4_02.KeyPress, txtBALLSU5_02.KeyPress, txtBALLSU6_02.KeyPress _
                                                                                                                        , txtBALLSU1_03.KeyPress, txtBALLSU2_03.KeyPress, txtBALLSU3_03.KeyPress, txtBALLSU4_03.KeyPress, txtBALLSU5_03.KeyPress, txtBALLSU6_03.KeyPress _
                                                                                                                        , txtBALLSU1_04.KeyPress, txtBALLSU2_04.KeyPress, txtBALLSU3_04.KeyPress, txtBALLSU4_04.KeyPress, txtBALLSU5_04.KeyPress, txtBALLSU6_04.KeyPress _
                                                                                                                        , txtBALLSU1_05.KeyPress, txtBALLSU2_05.KeyPress, txtBALLSU3_05.KeyPress, txtBALLSU4_05.KeyPress, txtBALLSU5_05.KeyPress, txtBALLSU6_05.KeyPress _
                                                                                                                        , txtKAGO1KN_01.KeyPress, txtKAGO2KN_01.KeyPress, txtKAGO3KN_01.KeyPress, txtKAGO4KN_01.KeyPress, txtKAGO5KN_01.KeyPress, txtKAGO6KN_01.KeyPress _
                                                                                                                        , txtKAGO1KN_02.KeyPress, txtKAGO2KN_02.KeyPress, txtKAGO3KN_02.KeyPress, txtKAGO4KN_02.KeyPress, txtKAGO5KN_02.KeyPress, txtKAGO6KN_02.KeyPress _
                                                                                                                        , txtKAGO1KN_03.KeyPress, txtKAGO2KN_03.KeyPress, txtKAGO3KN_03.KeyPress, txtKAGO4KN_03.KeyPress, txtKAGO5KN_03.KeyPress, txtKAGO6KN_03.KeyPress _
                                                                                                                        , txtKAGO1KN_04.KeyPress, txtKAGO2KN_04.KeyPress, txtKAGO3KN_04.KeyPress, txtKAGO4KN_04.KeyPress, txtKAGO5KN_04.KeyPress, txtKAGO6KN_04.KeyPress _
                                                                                                                        , txtKAGO1KN_05.KeyPress, txtKAGO2KN_05.KeyPress, txtKAGO3KN_05.KeyPress, txtKAGO4KN_05.KeyPress, txtKAGO5KN_05.KeyPress, txtKAGO6KN_05.KeyPress _
                                                                                                                        , txtKAGO1ZEI_01.KeyPress, txtKAGO2ZEI_01.KeyPress, txtKAGO3ZEI_01.KeyPress, txtKAGO4ZEI_01.KeyPress, txtKAGO5ZEI_01.KeyPress, txtKAGO6ZEI_01.KeyPress _
                                                                                                                        , txtKAGO1ZEI_02.KeyPress, txtKAGO2ZEI_02.KeyPress, txtKAGO3ZEI_02.KeyPress, txtKAGO4ZEI_02.KeyPress, txtKAGO5ZEI_02.KeyPress, txtKAGO6ZEI_02.KeyPress _
                                                                                                                        , txtKAGO1ZEI_03.KeyPress, txtKAGO2ZEI_03.KeyPress, txtKAGO3ZEI_03.KeyPress, txtKAGO4ZEI_03.KeyPress, txtKAGO5ZEI_03.KeyPress, txtKAGO6ZEI_03.KeyPress _
                                                                                                                        , txtKAGO1ZEI_04.KeyPress, txtKAGO2ZEI_04.KeyPress, txtKAGO3ZEI_04.KeyPress, txtKAGO4ZEI_04.KeyPress, txtKAGO5ZEI_04.KeyPress, txtKAGO6ZEI_04.KeyPress _
                                                                                                                        , txtKAGO1ZEI_05.KeyPress, txtKAGO2ZEI_05.KeyPress, txtKAGO3ZEI_05.KeyPress, txtKAGO4ZEI_05.KeyPress, txtKAGO5ZEI_05.KeyPress, txtKAGO6ZEI_05.KeyPress

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
    ''' テキストボックス_MouseDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtTIMENM_01.MouseDown, txtTIMENM_02.MouseDown, txtTIMENM_03.MouseDown, txtTIMENM_04.MouseDown, txtTIMENM_05.MouseDown _
                                                                                                                        , txtPASSCD_01.MouseDown, txtPASSCD_02.MouseDown, txtPASSCD_03.MouseDown, txtPASSCD_04.MouseDown, txtPASSCD_05.MouseDown _
                                                                                                                        , txtENTKIN_01.MouseDown, txtENTKIN_02.MouseDown, txtENTKIN_03.MouseDown, txtENTKIN_04.MouseDown, txtENTKIN_05.MouseDown _
                                                                                                                        , txtPOINT_01.MouseDown, txtPOINT_02.MouseDown, txtPOINT_03.MouseDown, txtPOINT_04.MouseDown, txtPOINT_05.MouseDown _
                                                                                                                        , txtBALLSU1_01.MouseDown, txtBALLSU2_01.MouseDown, txtBALLSU3_01.MouseDown, txtBALLSU4_01.MouseDown, txtBALLSU5_01.MouseDown, txtBALLSU6_01.MouseDown _
                                                                                                                        , txtBALLSU1_02.MouseDown, txtBALLSU2_02.MouseDown, txtBALLSU3_02.MouseDown, txtBALLSU4_02.MouseDown, txtBALLSU5_02.MouseDown, txtBALLSU6_02.MouseDown _
                                                                                                                        , txtBALLSU1_03.MouseDown, txtBALLSU2_03.MouseDown, txtBALLSU3_03.MouseDown, txtBALLSU4_03.MouseDown, txtBALLSU5_03.MouseDown, txtBALLSU6_03.MouseDown _
                                                                                                                        , txtBALLSU1_04.MouseDown, txtBALLSU2_04.MouseDown, txtBALLSU3_04.MouseDown, txtBALLSU4_04.MouseDown, txtBALLSU5_04.MouseDown, txtBALLSU6_04.MouseDown _
                                                                                                                        , txtBALLSU1_05.MouseDown, txtBALLSU2_05.MouseDown, txtBALLSU3_05.MouseDown, txtBALLSU4_05.MouseDown, txtBALLSU5_05.MouseDown, txtBALLSU6_05.MouseDown _
                                                                                                                        , txtKAGO1KN_01.MouseDown, txtKAGO2KN_01.MouseDown, txtKAGO3KN_01.MouseDown, txtKAGO4KN_01.MouseDown, txtKAGO5KN_01.MouseDown, txtKAGO6KN_01.MouseDown _
                                                                                                                        , txtKAGO1KN_02.MouseDown, txtKAGO2KN_02.MouseDown, txtKAGO3KN_02.MouseDown, txtKAGO4KN_02.MouseDown, txtKAGO5KN_02.MouseDown, txtKAGO6KN_02.MouseDown _
                                                                                                                        , txtKAGO1KN_03.MouseDown, txtKAGO2KN_03.MouseDown, txtKAGO3KN_03.MouseDown, txtKAGO4KN_03.MouseDown, txtKAGO5KN_03.MouseDown, txtKAGO6KN_03.MouseDown _
                                                                                                                        , txtKAGO1KN_04.MouseDown, txtKAGO2KN_04.MouseDown, txtKAGO3KN_04.MouseDown, txtKAGO4KN_04.MouseDown, txtKAGO5KN_04.MouseDown, txtKAGO6KN_04.MouseDown _
                                                                                                                        , txtKAGO1KN_05.MouseDown, txtKAGO2KN_05.MouseDown, txtKAGO3KN_05.MouseDown, txtKAGO4KN_05.MouseDown, txtKAGO5KN_05.MouseDown, txtKAGO6KN_05.MouseDown _
                                                                                                                        , txtKAGO1ZEI_01.MouseDown, txtKAGO2ZEI_01.MouseDown, txtKAGO3ZEI_01.MouseDown, txtKAGO4ZEI_01.MouseDown, txtKAGO5ZEI_01.MouseDown, txtKAGO6ZEI_01.MouseDown _
                                                                                                                        , txtKAGO1ZEI_02.MouseDown, txtKAGO2ZEI_02.MouseDown, txtKAGO3ZEI_02.MouseDown, txtKAGO4ZEI_02.MouseDown, txtKAGO5ZEI_02.MouseDown, txtKAGO6ZEI_02.MouseDown _
                                                                                                                        , txtKAGO1ZEI_03.MouseDown, txtKAGO2ZEI_03.MouseDown, txtKAGO3ZEI_03.MouseDown, txtKAGO4ZEI_03.MouseDown, txtKAGO5ZEI_03.MouseDown, txtKAGO6ZEI_03.MouseDown _
                                                                                                                        , txtKAGO1ZEI_04.MouseDown, txtKAGO2ZEI_04.MouseDown, txtKAGO3ZEI_04.MouseDown, txtKAGO4ZEI_04.MouseDown, txtKAGO5ZEI_04.MouseDown, txtKAGO6ZEI_04.MouseDown _
                                                                                                                        , txtKAGO1ZEI_05.MouseDown, txtKAGO2ZEI_05.MouseDown, txtKAGO3ZEI_05.MouseDown, txtKAGO4ZEI_05.MouseDown, txtKAGO5ZEI_05.MouseDown, txtKAGO6ZEI_05.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

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
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtTIMENM_01.Enter, txtTIMENM_02.Enter, txtTIMENM_03.Enter, txtTIMENM_04.Enter, txtTIMENM_05.Enter _
                                                                                   , txtPASSCD_01.Enter, txtPASSCD_02.Enter, txtPASSCD_03.Enter, txtPASSCD_04.Enter, txtPASSCD_05.Enter _
                                                                                   , txtENTKIN_01.Enter, txtENTKIN_02.Enter, txtENTKIN_03.Enter, txtENTKIN_04.Enter, txtENTKIN_05.Enter _
                                                                                   , txtPOINT_01.Enter, txtPOINT_02.Enter, txtPOINT_03.Enter, txtPOINT_04.Enter, txtPOINT_05.Enter _
                                                                                   , txtBALLSU1_01.Enter, txtBALLSU2_01.Enter, txtBALLSU3_01.Enter, txtBALLSU4_01.Enter, txtBALLSU5_01.Enter, txtBALLSU6_01.Enter _
                                                                                   , txtBALLSU1_02.Enter, txtBALLSU2_02.Enter, txtBALLSU3_02.Enter, txtBALLSU4_02.Enter, txtBALLSU5_02.Enter, txtBALLSU6_02.Enter _
                                                                                   , txtBALLSU1_03.Enter, txtBALLSU2_03.Enter, txtBALLSU3_03.Enter, txtBALLSU4_03.Enter, txtBALLSU5_03.Enter, txtBALLSU6_03.Enter _
                                                                                   , txtBALLSU1_04.Enter, txtBALLSU2_04.Enter, txtBALLSU3_04.Enter, txtBALLSU4_04.Enter, txtBALLSU5_04.Enter, txtBALLSU6_04.Enter _
                                                                                   , txtBALLSU1_05.Enter, txtBALLSU2_05.Enter, txtBALLSU3_05.Enter, txtBALLSU4_05.Enter, txtBALLSU5_05.Enter, txtBALLSU6_05.Enter _
                                                                                   , txtKAGO1KN_01.Enter, txtKAGO2KN_01.Enter, txtKAGO3KN_01.Enter, txtKAGO4KN_01.Enter, txtKAGO5KN_01.Enter, txtKAGO6KN_01.Enter _
                                                                                   , txtKAGO1KN_02.Enter, txtKAGO2KN_02.Enter, txtKAGO3KN_02.Enter, txtKAGO4KN_02.Enter, txtKAGO5KN_02.Enter, txtKAGO6KN_02.Enter _
                                                                                   , txtKAGO1KN_03.Enter, txtKAGO2KN_03.Enter, txtKAGO3KN_03.Enter, txtKAGO4KN_03.Enter, txtKAGO5KN_03.Enter, txtKAGO6KN_03.Enter _
                                                                                   , txtKAGO1KN_04.Enter, txtKAGO2KN_04.Enter, txtKAGO3KN_04.Enter, txtKAGO4KN_04.Enter, txtKAGO5KN_04.Enter, txtKAGO6KN_04.Enter _
                                                                                   , txtKAGO1KN_05.Enter, txtKAGO2KN_05.Enter, txtKAGO3KN_05.Enter, txtKAGO4KN_05.Enter, txtKAGO5KN_05.Enter, txtKAGO6KN_05.Enter _
                                                                                   , txtKAGO1ZEI_01.Enter, txtKAGO2ZEI_01.Enter, txtKAGO3ZEI_01.Enter, txtKAGO4ZEI_01.Enter, txtKAGO5ZEI_01.Enter, txtKAGO6ZEI_01.Enter _
                                                                                   , txtKAGO1ZEI_02.Enter, txtKAGO2ZEI_02.Enter, txtKAGO3ZEI_02.Enter, txtKAGO4ZEI_02.Enter, txtKAGO5ZEI_02.Enter, txtKAGO6ZEI_02.Enter _
                                                                                   , txtKAGO1ZEI_03.Enter, txtKAGO2ZEI_03.Enter, txtKAGO3ZEI_03.Enter, txtKAGO4ZEI_03.Enter, txtKAGO5ZEI_03.Enter, txtKAGO6ZEI_03.Enter _
                                                                                   , txtKAGO1ZEI_04.Enter, txtKAGO2ZEI_04.Enter, txtKAGO3ZEI_04.Enter, txtKAGO4ZEI_04.Enter, txtKAGO5ZEI_04.Enter, txtKAGO6ZEI_04.Enter _
                                                                                   , txtKAGO1ZEI_05.Enter, txtKAGO2ZEI_05.Enter, txtKAGO3ZEI_05.Enter, txtKAGO4ZEI_05.Enter, txtKAGO5ZEI_05.Enter, txtKAGO6ZEI_05.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace(":", String.Empty)
            txtBox.Text = txtBox.Text.Replace(",", String.Empty)
            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            '画面初期化
            Init()



            '営業情報取得
            GetNkbEIGMTC()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try
            Me.cmbKBMAST.Focus()

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


            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM EIGMTC WHERE RKNKB = " & _intRKNKB & " AND  NKBNO = " & (Me.cmbKBMAST.SelectedIndex + 1) & " AND FLRKB = " & _intFLRKB & " ORDER BY TIMEKB")
            If Not resultDt.Rows.Count.Equals(0) Then
                '最新の更新日時を取得

                Dim drSelectRow() As DataRow = resultDt.Select
                Dim dtUPDDTM As DateTime = DirectCast(drSelectRow(0).Item("UPDDTM"), DateTime)

                If Not dtUPDDTM.Equals(_dtUPDDTM) Then
                    Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

            '営業情報登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("営業情報の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            ClrText()

            '営業情報取得
            If Not GetNkbEIGMTC() Then
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
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = True

            Me.lblRknInfo.Text = "本日の料金体系は【" & UIUtility.SYSTEM.RKNNM & "】です。"
            Me.lblRknInfo.ForeColor = Color.FromArgb(UIUtility.COLOR_INFO.RKN_R, UIUtility.COLOR_INFO.RKN_G, UIUtility.COLOR_INFO.RKN_B)

            Select Case UIUtility.SYSTEM.RKNKB
                Case 1 : Me.rdoRKNKB1.Checked = True : _intRKNKB = 1
                Case 2 : Me.rdoRKNKB2.Checked = True : _intRKNKB = 2
                Case 3 : Me.rdoRKNKB3.Checked = True : _intRKNKB = 3
                Case 4 : Me.rdoRKNKB4.Checked = True : _intRKNKB = 4
                Case 5 : Me.rdoRKNKB5.Checked = True : _intRKNKB = 5
                Case 6 : Me.rdoRKNKB6.Checked = True : _intRKNKB = 6
            End Select

            'フロア数
            _intFLRKB = 1
            Me.rdoFLRKB1.Checked = True

            'テキストクリア
            ClrText()


            '現在の時間帯をのフォントカラー変更
            If UIUtility.SYSTEM.RKNKB.Equals(1) Then
                '【平日】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_01.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_01.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_01.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_01.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_01.ForeColor = Color.Yellow
                End Select

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' テキストクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClrText()
        Try
            '時間帯
            Me.txtTIMENM_01.Text = String.Empty
            Me.txtTIMENM_02.Text = String.Empty
            Me.txtTIMENM_03.Text = String.Empty
            Me.txtTIMENM_04.Text = String.Empty
            Me.txtTIMENM_05.Text = String.Empty


            If Me.cmbKBMAST.SelectedIndex.Equals(0) And (Me.rdoFLRKB1.Checked) Then
                Me.txtTIMENM_01.ReadOnly = False
                Me.txtTIMENM_01.BackColor = Color.Moccasin
                Me.txtPASSCD_01.ReadOnly = False
                Me.txtPASSCD_01.BackColor = Color.Moccasin

                Me.txtTIMENM_02.ReadOnly = False
                Me.txtTIMENM_02.BackColor = Color.White
                Me.txtPASSCD_02.ReadOnly = False
                Me.txtPASSCD_02.BackColor = Color.White

                Me.txtTIMENM_03.ReadOnly = False
                Me.txtTIMENM_03.BackColor = Color.Moccasin
                Me.txtPASSCD_03.ReadOnly = False
                Me.txtPASSCD_03.BackColor = Color.Moccasin

                Me.txtTIMENM_04.ReadOnly = False
                Me.txtTIMENM_04.BackColor = Color.White
                Me.txtPASSCD_04.ReadOnly = False
                Me.txtPASSCD_04.BackColor = Color.White

                Me.txtTIMENM_05.ReadOnly = False
                Me.txtTIMENM_05.BackColor = Color.Moccasin
                Me.txtPASSCD_05.ReadOnly = False
                Me.txtPASSCD_05.BackColor = Color.Moccasin

                Me.lblMsg.Visible = False
            Else
                Me.txtTIMENM_01.ReadOnly = True
                Me.txtTIMENM_01.BackColor = Color.Silver
                Me.txtPASSCD_01.ReadOnly = True
                Me.txtPASSCD_01.BackColor = Color.Silver

                Me.txtTIMENM_02.ReadOnly = True
                Me.txtTIMENM_02.BackColor = Color.Silver
                Me.txtPASSCD_02.ReadOnly = True
                Me.txtPASSCD_02.BackColor = Color.Silver

                Me.txtTIMENM_03.ReadOnly = True
                Me.txtTIMENM_03.BackColor = Color.Silver
                Me.txtPASSCD_03.ReadOnly = True
                Me.txtPASSCD_03.BackColor = Color.Silver

                Me.txtTIMENM_04.ReadOnly = True
                Me.txtTIMENM_04.BackColor = Color.Silver
                Me.txtPASSCD_04.ReadOnly = True
                Me.txtPASSCD_04.BackColor = Color.Silver

                Me.txtTIMENM_05.ReadOnly = True
                Me.txtTIMENM_05.BackColor = Color.Silver
                Me.txtPASSCD_05.ReadOnly = True
                Me.txtPASSCD_05.BackColor = Color.Silver

                Me.lblMsg.Visible = True
            End If

            'パスワード
            Me.txtPASSCD_01.Text = _cstClrMoji
            Me.txtPASSCD_02.Text = _cstClrMoji
            Me.txtPASSCD_03.Text = _cstClrMoji
            Me.txtPASSCD_04.Text = _cstClrMoji
            Me.txtPASSCD_05.Text = _cstClrMoji

            Me.txtPASSCD_01.ReadOnly = True
            Me.txtPASSCD_02.ReadOnly = True
            Me.txtPASSCD_03.ReadOnly = True
            Me.txtPASSCD_04.ReadOnly = True
            Me.txtPASSCD_05.ReadOnly = True

            '入場料
            Me.txtENTKIN_01.Text = "0"
            Me.txtENTKIN_02.Text = "0"
            Me.txtENTKIN_03.Text = "0"
            Me.txtENTKIN_04.Text = "0"
            Me.txtENTKIN_05.Text = "0"

            Me.txtENTKIN_01.Visible = False
            Me.txtENTKIN_02.Visible = False
            Me.txtENTKIN_03.Visible = False
            Me.txtENTKIN_04.Visible = False
            Me.txtENTKIN_05.Visible = False

            'ポイント
            Me.txtPOINT_01.Text = "0"
            Me.txtPOINT_02.Text = "0"
            Me.txtPOINT_03.Text = "0"
            Me.txtPOINT_04.Text = "0"
            Me.txtPOINT_05.Text = "0"

            Me.txtPOINT_01.Visible = False
            Me.txtPOINT_02.Visible = False
            Me.txtPOINT_03.Visible = False
            Me.txtPOINT_04.Visible = False
            Me.txtPOINT_05.Visible = False

            'ボール数
            Me.txtBALLSU1_01.Text = "0"
            Me.txtBALLSU2_01.Text = "0"
            Me.txtBALLSU3_01.Text = "0"
            Me.txtBALLSU4_01.Text = "0"
            Me.txtBALLSU5_01.Text = "0"
            Me.txtBALLSU6_01.Text = "0"
            Me.txtBALLSU1_02.Text = "0"
            Me.txtBALLSU2_02.Text = "0"
            Me.txtBALLSU3_02.Text = "0"
            Me.txtBALLSU4_02.Text = "0"
            Me.txtBALLSU5_02.Text = "0"
            Me.txtBALLSU6_02.Text = "0"
            Me.txtBALLSU1_03.Text = "0"
            Me.txtBALLSU2_03.Text = "0"
            Me.txtBALLSU3_03.Text = "0"
            Me.txtBALLSU4_03.Text = "0"
            Me.txtBALLSU5_03.Text = "0"
            Me.txtBALLSU6_03.Text = "0"
            Me.txtBALLSU1_04.Text = "0"
            Me.txtBALLSU2_04.Text = "0"
            Me.txtBALLSU3_04.Text = "0"
            Me.txtBALLSU4_04.Text = "0"
            Me.txtBALLSU5_04.Text = "0"
            Me.txtBALLSU6_04.Text = "0"
            Me.txtBALLSU1_05.Text = "0"
            Me.txtBALLSU2_05.Text = "0"
            Me.txtBALLSU3_05.Text = "0"
            Me.txtBALLSU4_05.Text = "0"
            Me.txtBALLSU5_05.Text = "0"
            Me.txtBALLSU6_05.Text = "0"

            Me.txtBALLSU1_01.Visible = False
            Me.txtBALLSU2_01.Visible = False
            Me.txtBALLSU3_01.Visible = False
            Me.txtBALLSU4_01.Visible = False
            Me.txtBALLSU5_01.Visible = False
            Me.txtBALLSU6_01.Visible = False
            Me.txtBALLSU1_02.Visible = False
            Me.txtBALLSU2_02.Visible = False
            Me.txtBALLSU3_02.Visible = False
            Me.txtBALLSU4_02.Visible = False
            Me.txtBALLSU5_02.Visible = False
            Me.txtBALLSU6_02.Visible = False
            Me.txtBALLSU1_03.Visible = False
            Me.txtBALLSU2_03.Visible = False
            Me.txtBALLSU3_03.Visible = False
            Me.txtBALLSU4_03.Visible = False
            Me.txtBALLSU5_03.Visible = False
            Me.txtBALLSU6_03.Visible = False
            Me.txtBALLSU1_04.Visible = False
            Me.txtBALLSU2_04.Visible = False
            Me.txtBALLSU3_04.Visible = False
            Me.txtBALLSU4_04.Visible = False
            Me.txtBALLSU5_04.Visible = False
            Me.txtBALLSU6_04.Visible = False
            Me.txtBALLSU1_05.Visible = False
            Me.txtBALLSU2_05.Visible = False
            Me.txtBALLSU3_05.Visible = False
            Me.txtBALLSU4_05.Visible = False
            Me.txtBALLSU5_05.Visible = False
            Me.txtBALLSU6_05.Visible = False

            '単価
            Me.txtKAGO1KN_01.Text = "0"
            Me.txtKAGO2KN_01.Text = "0"
            Me.txtKAGO3KN_01.Text = "0"
            Me.txtKAGO4KN_01.Text = "0"
            Me.txtKAGO5KN_01.Text = "0"
            Me.txtKAGO6KN_01.Text = "0"
            Me.txtKAGO1KN_02.Text = "0"
            Me.txtKAGO2KN_02.Text = "0"
            Me.txtKAGO3KN_02.Text = "0"
            Me.txtKAGO4KN_02.Text = "0"
            Me.txtKAGO5KN_02.Text = "0"
            Me.txtKAGO6KN_02.Text = "0"
            Me.txtKAGO1KN_03.Text = "0"
            Me.txtKAGO2KN_03.Text = "0"
            Me.txtKAGO3KN_03.Text = "0"
            Me.txtKAGO4KN_03.Text = "0"
            Me.txtKAGO5KN_03.Text = "0"
            Me.txtKAGO6KN_03.Text = "0"
            Me.txtKAGO1KN_04.Text = "0"
            Me.txtKAGO2KN_04.Text = "0"
            Me.txtKAGO3KN_04.Text = "0"
            Me.txtKAGO4KN_04.Text = "0"
            Me.txtKAGO5KN_04.Text = "0"
            Me.txtKAGO6KN_04.Text = "0"
            Me.txtKAGO1KN_05.Text = "0"
            Me.txtKAGO2KN_05.Text = "0"
            Me.txtKAGO3KN_05.Text = "0"
            Me.txtKAGO4KN_05.Text = "0"
            Me.txtKAGO5KN_05.Text = "0"
            Me.txtKAGO6KN_05.Text = "0"

            Me.txtKAGO1KN_01.Visible = False
            Me.txtKAGO2KN_01.Visible = False
            Me.txtKAGO3KN_01.Visible = False
            Me.txtKAGO4KN_01.Visible = False
            Me.txtKAGO5KN_01.Visible = False
            Me.txtKAGO6KN_01.Visible = False
            Me.txtKAGO1KN_02.Visible = False
            Me.txtKAGO2KN_02.Visible = False
            Me.txtKAGO3KN_02.Visible = False
            Me.txtKAGO4KN_02.Visible = False
            Me.txtKAGO5KN_02.Visible = False
            Me.txtKAGO6KN_02.Visible = False
            Me.txtKAGO1KN_03.Visible = False
            Me.txtKAGO2KN_03.Visible = False
            Me.txtKAGO3KN_03.Visible = False
            Me.txtKAGO4KN_03.Visible = False
            Me.txtKAGO5KN_03.Visible = False
            Me.txtKAGO6KN_03.Visible = False
            Me.txtKAGO1KN_04.Visible = False
            Me.txtKAGO2KN_04.Visible = False
            Me.txtKAGO3KN_04.Visible = False
            Me.txtKAGO4KN_04.Visible = False
            Me.txtKAGO5KN_04.Visible = False
            Me.txtKAGO6KN_04.Visible = False
            Me.txtKAGO1KN_05.Visible = False
            Me.txtKAGO2KN_05.Visible = False
            Me.txtKAGO3KN_05.Visible = False
            Me.txtKAGO4KN_05.Visible = False
            Me.txtKAGO5KN_05.Visible = False
            Me.txtKAGO6KN_05.Visible = False

            '消費税
            Me.txtKAGO1ZEI_01.Text = "0"
            Me.txtKAGO2ZEI_01.Text = "0"
            Me.txtKAGO3ZEI_01.Text = "0"
            Me.txtKAGO4ZEI_01.Text = "0"
            Me.txtKAGO5ZEI_01.Text = "0"
            Me.txtKAGO6ZEI_01.Text = "0"
            Me.txtKAGO1ZEI_02.Text = "0"
            Me.txtKAGO2ZEI_02.Text = "0"
            Me.txtKAGO3ZEI_02.Text = "0"
            Me.txtKAGO4ZEI_02.Text = "0"
            Me.txtKAGO5ZEI_02.Text = "0"
            Me.txtKAGO6ZEI_02.Text = "0"
            Me.txtKAGO1ZEI_03.Text = "0"
            Me.txtKAGO2ZEI_03.Text = "0"
            Me.txtKAGO3ZEI_03.Text = "0"
            Me.txtKAGO4ZEI_03.Text = "0"
            Me.txtKAGO5ZEI_03.Text = "0"
            Me.txtKAGO6ZEI_03.Text = "0"
            Me.txtKAGO1ZEI_04.Text = "0"
            Me.txtKAGO2ZEI_04.Text = "0"
            Me.txtKAGO3ZEI_04.Text = "0"
            Me.txtKAGO4ZEI_04.Text = "0"
            Me.txtKAGO5ZEI_04.Text = "0"
            Me.txtKAGO6ZEI_04.Text = "0"
            Me.txtKAGO1ZEI_05.Text = "0"
            Me.txtKAGO2ZEI_05.Text = "0"
            Me.txtKAGO3ZEI_05.Text = "0"
            Me.txtKAGO4ZEI_05.Text = "0"
            Me.txtKAGO5ZEI_05.Text = "0"
            Me.txtKAGO6ZEI_05.Text = "0"

            Me.txtKAGO1ZEI_01.Visible = False
            Me.txtKAGO2ZEI_01.Visible = False
            Me.txtKAGO3ZEI_01.Visible = False
            Me.txtKAGO4ZEI_01.Visible = False
            Me.txtKAGO5ZEI_01.Visible = False
            Me.txtKAGO6ZEI_01.Visible = False
            Me.txtKAGO1ZEI_02.Visible = False
            Me.txtKAGO2ZEI_02.Visible = False
            Me.txtKAGO3ZEI_02.Visible = False
            Me.txtKAGO4ZEI_02.Visible = False
            Me.txtKAGO5ZEI_02.Visible = False
            Me.txtKAGO6ZEI_02.Visible = False
            Me.txtKAGO1ZEI_03.Visible = False
            Me.txtKAGO2ZEI_03.Visible = False
            Me.txtKAGO3ZEI_03.Visible = False
            Me.txtKAGO4ZEI_03.Visible = False
            Me.txtKAGO5ZEI_03.Visible = False
            Me.txtKAGO6ZEI_03.Visible = False
            Me.txtKAGO1ZEI_04.Visible = False
            Me.txtKAGO2ZEI_04.Visible = False
            Me.txtKAGO3ZEI_04.Visible = False
            Me.txtKAGO4ZEI_04.Visible = False
            Me.txtKAGO5ZEI_04.Visible = False
            Me.txtKAGO6ZEI_04.Visible = False
            Me.txtKAGO1ZEI_05.Visible = False
            Me.txtKAGO2ZEI_05.Visible = False
            Me.txtKAGO3ZEI_05.Visible = False
            Me.txtKAGO4ZEI_05.Visible = False
            Me.txtKAGO5ZEI_05.Visible = False
            Me.txtKAGO6ZEI_05.Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 登録データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckRegister(ByRef Msg As String) As Boolean
        Try
            '時間帯
            Dim txtTIMENM01 As TextBox = Nothing
            Dim txtTIMENM02 As TextBox = Nothing
            Dim txtTIMENM03 As TextBox = Nothing
            Dim txtTIMENM04 As TextBox = Nothing
            Dim txtTIMENM05 As TextBox = Nothing

            Dim strTIMENM01 As String = String.Empty
            Dim strTIMENM02 As String = String.Empty
            Dim strTIMENM03 As String = String.Empty
            Dim strTIMENM04 As String = String.Empty
            Dim strTIMENM05 As String = String.Empty

            For i As Integer = 1 To 6   '料金体系(1～6)
                Select Case i
                    Case 1  '平日
                        txtTIMENM01 = Me.txtTIMENM_01
                        txtTIMENM02 = Me.txtTIMENM_02
                        txtTIMENM03 = Me.txtTIMENM_03
                        txtTIMENM04 = Me.txtTIMENM_04
                        txtTIMENM05 = Me.txtTIMENM_05
                End Select

                strTIMENM01 = txtTIMENM01.Text.Replace(":", String.Empty)
                strTIMENM02 = txtTIMENM02.Text.Replace(":", String.Empty)
                strTIMENM03 = txtTIMENM03.Text.Replace(":", String.Empty)
                strTIMENM04 = txtTIMENM04.Text.Replace(":", String.Empty)
                strTIMENM05 = txtTIMENM05.Text.Replace(":", String.Empty)

                Msg = "時間帯に矛盾があります。"
                If Not String.IsNullOrEmpty(strTIMENM01) Then
                    If (Not String.IsNullOrEmpty(strTIMENM02)) And (strTIMENM01 >= strTIMENM02) Then
                        txtTIMENM02.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM03)) And (strTIMENM01 >= strTIMENM03) Then
                        txtTIMENM03.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM04)) And (strTIMENM01 >= strTIMENM04) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM01 >= strTIMENM05) Then
                        txtTIMENM05.Focus()
                        Return False
                    End If
                End If
                If Not String.IsNullOrEmpty(strTIMENM02) Then
                    If (Not String.IsNullOrEmpty(strTIMENM03)) And (strTIMENM02 >= strTIMENM03) Then
                        txtTIMENM03.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM04)) And (strTIMENM02 >= strTIMENM04) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM02 >= strTIMENM05) Then
                        txtTIMENM05.Focus()
                        Return False
                    End If
                End If
                If Not String.IsNullOrEmpty(strTIMENM03) Then
                    If (Not String.IsNullOrEmpty(strTIMENM04)) And (strTIMENM03 >= strTIMENM04) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM03 >= strTIMENM05) Then
                        txtTIMENM05.Focus()
                        Return False
                    End If
                End If
                If Not String.IsNullOrEmpty(strTIMENM04) Then
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM04 >= strTIMENM05) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                End If
            Next



            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 料金体系マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetRKNMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM RKNMTA")
            sql.Append(" ORDER BY RKNKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim intCLRR As Integer = 0
            Dim intCLRG As Integer = 0
            Dim intCLRB As Integer = 0

            For i As Integer = 0 To resultDt.Rows.Count - 1
                intCLRR = CType(resultDt.Rows(i).Item("CLRR").ToString, Integer)
                intCLRG = CType(resultDt.Rows(i).Item("CLRG").ToString, Integer)
                intCLRB = CType(resultDt.Rows(i).Item("CLRB").ToString, Integer)
                Select Case CType(resultDt.Rows(i).Item("RKNKB").ToString, Integer)
                    Case 1  '平日
                        Me.rdoRKNKB1.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB1.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                        Me.rdoRKNKB1_Copy.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB1_Copy.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 2  '休日
                        Me.rdoRKNKB2_Copy.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB2_Copy.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 3  '特日１
                        Me.rdoRKNKB3_Copy.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB3_Copy.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 4  '特日２
                        Me.rdoRKNKB4_Copy.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB4_Copy.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 5  '特日３
                        Me.rdoRKNKB5_Copy.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB5_Copy.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 6  '特日４
                        Me.rdoRKNKB6_Copy.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.rdoRKNKB6_Copy.ForeColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                End Select
            Next

            Return True

        Catch ex As Exception
            Throw ex

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
            sql.Append(" WHERE")
            sql.Append(" NKBNO <= 10")
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
    ''' コピー元顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCopyKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" WHERE")
            sql.Append(" NKBNO <= 10")
            sql.Append(" ORDER BY KBMAST ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.cmbKBMAST_Copy.DataSource = resultDt      '料金体系
            Me.cmbKBMAST_Copy.ValueMember = "NKBNO"
            Me.cmbKBMAST_Copy.DisplayMember = "CKBNAME"
            Me.cmbKBMAST_Copy.SelectedValue = 1

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 営業情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNkbEIGMTC(Optional CopyFlg As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM EIGMTC")
            sql.Append(" WHERE")
            '料金体系
            If CopyFlg Then
                sql.Append(" RKNKB = " & _intRKNKB_Copy)
            Else
                sql.Append(" RKNKB = " & _intRKNKB)
            End If
            '顧客種別
            If CopyFlg Then
                sql.Append(" AND NKBNO = " & (Me.cmbKBMAST_Copy.SelectedIndex + 1))
            Else
                sql.Append(" AND NKBNO = " & (Me.cmbKBMAST.SelectedIndex + 1))
            End If
            'フロア数
            If CopyFlg Then
                sql.Append(" AND FLRKB = " & _intFLRKB_Copy)
            Else
                sql.Append(" AND FLRKB = " & _intFLRKB)
            End If
            sql.Append(" ORDER BY TIMEKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                If _intFLRKB > 1 Then
                    GetTimeKbInfo()
                End If
                Return False
            End If

            '最新の更新日時取得
            If Not CopyFlg Then _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Dim dr As DataRow()

            '時間帯1
            dr = resultDt.Select("TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_01.ReadOnly = False
                '入場料
                Me.txtENTKIN_01.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtENTKIN_01.Visible = True
                'ポイント
                Me.txtPOINT_01.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtPOINT_01.Visible = True
                'カゴ１
                Me.txtBALLSU1_01.Text = CType(dr(0).Item("BALLSU1").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU1_01.Visible = True
                Me.txtKAGO1KN_01.Text = CType(dr(0).Item("KAGO1BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO1KN_01.Visible = True
                Me.txtKAGO1ZEI_01.Text = CType(dr(0).Item("KAGO1ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO1ZEI_01.Visible = True
                'カゴ２
                Me.txtBALLSU2_01.Text = CType(dr(0).Item("BALLSU2").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU2_01.Visible = True
                Me.txtKAGO2KN_01.Text = CType(dr(0).Item("KAGO2BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO2KN_01.Visible = True
                Me.txtKAGO2ZEI_01.Text = CType(dr(0).Item("KAGO2ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO2ZEI_01.Visible = True
                'カゴ３
                Me.txtBALLSU3_01.Text = CType(dr(0).Item("BALLSU3").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU3_01.Visible = True
                Me.txtKAGO3KN_01.Text = CType(dr(0).Item("KAGO3BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO3KN_01.Visible = True
                Me.txtKAGO3ZEI_01.Text = CType(dr(0).Item("KAGO3ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO3ZEI_01.Visible = True
                'カゴ４
                Me.txtBALLSU4_01.Text = CType(dr(0).Item("BALLSU4").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU4_01.Visible = True
                Me.txtKAGO4KN_01.Text = CType(dr(0).Item("KAGO4BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO4KN_01.Visible = True
                Me.txtKAGO4ZEI_01.Text = CType(dr(0).Item("KAGO4ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO4ZEI_01.Visible = True
                'カゴ５
                Me.txtBALLSU5_01.Text = CType(dr(0).Item("BALLSU5").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU5_01.Visible = True
                Me.txtKAGO5KN_01.Text = CType(dr(0).Item("KAGO5BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO5KN_01.Visible = True
                Me.txtKAGO5ZEI_01.Text = CType(dr(0).Item("KAGO5ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO5ZEI_01.Visible = True
                'カゴ６
                Me.txtBALLSU6_01.Text = CType(dr(0).Item("BALLSU6").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU6_01.Visible = True
                Me.txtKAGO6KN_01.Text = CType(dr(0).Item("KAGO6BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO6KN_01.Visible = True
                Me.txtKAGO6ZEI_01.Text = CType(dr(0).Item("KAGO6ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO6ZEI_01.Visible = True
            End If

            '時間帯2
            dr = resultDt.Select("TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_02.ReadOnly = False
                '入場料
                Me.txtENTKIN_02.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtENTKIN_02.Visible = True
                'ポイント
                Me.txtPOINT_02.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtPOINT_02.Visible = True
                'カゴ１
                Me.txtBALLSU1_02.Text = CType(dr(0).Item("BALLSU1").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU1_02.Visible = True
                Me.txtKAGO1KN_02.Text = CType(dr(0).Item("KAGO1BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO1KN_02.Visible = True
                Me.txtKAGO1ZEI_02.Text = CType(dr(0).Item("KAGO1ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO1ZEI_02.Visible = True
                'カゴ２
                Me.txtBALLSU2_02.Text = CType(dr(0).Item("BALLSU2").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU2_02.Visible = True
                Me.txtKAGO2KN_02.Text = CType(dr(0).Item("KAGO2BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO2KN_02.Visible = True
                Me.txtKAGO2ZEI_02.Text = CType(dr(0).Item("KAGO2ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO2ZEI_02.Visible = True
                'カゴ３
                Me.txtBALLSU3_02.Text = CType(dr(0).Item("BALLSU3").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU3_02.Visible = True
                Me.txtKAGO3KN_02.Text = CType(dr(0).Item("KAGO3BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO3KN_02.Visible = True
                Me.txtKAGO3ZEI_02.Text = CType(dr(0).Item("KAGO3ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO3ZEI_02.Visible = True
                'カゴ４
                Me.txtBALLSU4_02.Text = CType(dr(0).Item("BALLSU4").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU4_02.Visible = True
                Me.txtKAGO4KN_02.Text = CType(dr(0).Item("KAGO4BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO4KN_02.Visible = True
                Me.txtKAGO4ZEI_02.Text = CType(dr(0).Item("KAGO4ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO4ZEI_02.Visible = True
                'カゴ５
                Me.txtBALLSU5_02.Text = CType(dr(0).Item("BALLSU5").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU5_02.Visible = True
                Me.txtKAGO5KN_02.Text = CType(dr(0).Item("KAGO5BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO5KN_02.Visible = True
                Me.txtKAGO5ZEI_02.Text = CType(dr(0).Item("KAGO5ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO5ZEI_02.Visible = True
                'カゴ６
                Me.txtBALLSU6_02.Text = CType(dr(0).Item("BALLSU6").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU6_02.Visible = True
                Me.txtKAGO6KN_02.Text = CType(dr(0).Item("KAGO6BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO6KN_02.Visible = True
                Me.txtKAGO6ZEI_02.Text = CType(dr(0).Item("KAGO6ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO6ZEI_02.Visible = True
            End If

            '時間帯3
            dr = resultDt.Select("TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_03.ReadOnly = False
                '入場料
                Me.txtENTKIN_03.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtENTKIN_03.Visible = True
                'ポイント
                Me.txtPOINT_03.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtPOINT_03.Visible = True
                'カゴ１
                Me.txtBALLSU1_03.Text = CType(dr(0).Item("BALLSU1").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU1_03.Visible = True
                Me.txtKAGO1KN_03.Text = CType(dr(0).Item("KAGO1BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO1KN_03.Visible = True
                Me.txtKAGO1ZEI_03.Text = CType(dr(0).Item("KAGO1ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO1ZEI_03.Visible = True
                'カゴ２
                Me.txtBALLSU2_03.Text = CType(dr(0).Item("BALLSU2").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU2_03.Visible = True
                Me.txtKAGO2KN_03.Text = CType(dr(0).Item("KAGO2BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO2KN_03.Visible = True
                Me.txtKAGO2ZEI_03.Text = CType(dr(0).Item("KAGO2ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO2ZEI_03.Visible = True
                'カゴ３
                Me.txtBALLSU3_03.Text = CType(dr(0).Item("BALLSU3").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU3_03.Visible = True
                Me.txtKAGO3KN_03.Text = CType(dr(0).Item("KAGO3BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO3KN_03.Visible = True
                Me.txtKAGO3ZEI_03.Text = CType(dr(0).Item("KAGO3ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO3ZEI_03.Visible = True
                'カゴ４
                Me.txtBALLSU4_03.Text = CType(dr(0).Item("BALLSU4").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU4_03.Visible = True
                Me.txtKAGO4KN_03.Text = CType(dr(0).Item("KAGO4BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO4KN_03.Visible = True
                Me.txtKAGO4ZEI_03.Text = CType(dr(0).Item("KAGO4ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO4ZEI_03.Visible = True
                'カゴ５
                Me.txtBALLSU5_03.Text = CType(dr(0).Item("BALLSU5").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU5_03.Visible = True
                Me.txtKAGO5KN_03.Text = CType(dr(0).Item("KAGO5BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO5KN_03.Visible = True
                Me.txtKAGO5ZEI_03.Text = CType(dr(0).Item("KAGO5ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO5ZEI_03.Visible = True
                'カゴ６
                Me.txtBALLSU6_03.Text = CType(dr(0).Item("BALLSU6").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU6_03.Visible = True
                Me.txtKAGO6KN_03.Text = CType(dr(0).Item("KAGO6BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO6KN_03.Visible = True
                Me.txtKAGO6ZEI_03.Text = CType(dr(0).Item("KAGO6ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO6ZEI_03.Visible = True
            End If

            '時間帯4
            dr = resultDt.Select("TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_04.ReadOnly = False
                '入場料
                Me.txtENTKIN_04.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtENTKIN_04.Visible = True
                'ポイント
                Me.txtPOINT_04.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtPOINT_04.Visible = True
                'カゴ１
                Me.txtBALLSU1_04.Text = CType(dr(0).Item("BALLSU1").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU1_04.Visible = True
                Me.txtKAGO1KN_04.Text = CType(dr(0).Item("KAGO1BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO1KN_04.Visible = True
                Me.txtKAGO1ZEI_04.Text = CType(dr(0).Item("KAGO1ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO1ZEI_04.Visible = True
                'カゴ２
                Me.txtBALLSU2_04.Text = CType(dr(0).Item("BALLSU2").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU2_04.Visible = True
                Me.txtKAGO2KN_04.Text = CType(dr(0).Item("KAGO2BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO2KN_04.Visible = True
                Me.txtKAGO2ZEI_04.Text = CType(dr(0).Item("KAGO2ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO2ZEI_04.Visible = True
                'カゴ３
                Me.txtBALLSU3_04.Text = CType(dr(0).Item("BALLSU3").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU3_04.Visible = True
                Me.txtKAGO3KN_04.Text = CType(dr(0).Item("KAGO3BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO3KN_04.Visible = True
                Me.txtKAGO3ZEI_04.Text = CType(dr(0).Item("KAGO3ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO3ZEI_04.Visible = True
                'カゴ４
                Me.txtBALLSU4_04.Text = CType(dr(0).Item("BALLSU4").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU4_04.Visible = True
                Me.txtKAGO4KN_04.Text = CType(dr(0).Item("KAGO4BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO4KN_04.Visible = True
                Me.txtKAGO4ZEI_04.Text = CType(dr(0).Item("KAGO4ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO4ZEI_04.Visible = True
                'カゴ５
                Me.txtBALLSU5_04.Text = CType(dr(0).Item("BALLSU5").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU5_04.Visible = True
                Me.txtKAGO5KN_04.Text = CType(dr(0).Item("KAGO5BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO5KN_04.Visible = True
                Me.txtKAGO5ZEI_04.Text = CType(dr(0).Item("KAGO5ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO5ZEI_04.Visible = True
                'カゴ６
                Me.txtBALLSU6_04.Text = CType(dr(0).Item("BALLSU6").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU6_04.Visible = True
                Me.txtKAGO6KN_04.Text = CType(dr(0).Item("KAGO6BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO6KN_04.Visible = True
                Me.txtKAGO6ZEI_04.Text = CType(dr(0).Item("KAGO6ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO6ZEI_04.Visible = True
            End If

            '時間帯5
            dr = resultDt.Select("TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_05.ReadOnly = False
                '入場料
                Me.txtENTKIN_05.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtENTKIN_05.Visible = True
                'ポイント
                Me.txtPOINT_05.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtPOINT_05.Visible = True
                'カゴ１
                Me.txtBALLSU1_05.Text = CType(dr(0).Item("BALLSU1").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU1_05.Visible = True
                Me.txtKAGO1KN_05.Text = CType(dr(0).Item("KAGO1BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO1KN_05.Visible = True
                Me.txtKAGO1ZEI_05.Text = CType(dr(0).Item("KAGO1ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO1ZEI_05.Visible = True
                'カゴ２
                Me.txtBALLSU2_05.Text = CType(dr(0).Item("BALLSU2").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU2_05.Visible = True
                Me.txtKAGO2KN_05.Text = CType(dr(0).Item("KAGO2BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO2KN_05.Visible = True
                Me.txtKAGO2ZEI_05.Text = CType(dr(0).Item("KAGO2ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO2ZEI_05.Visible = True
                'カゴ３
                Me.txtBALLSU3_05.Text = CType(dr(0).Item("BALLSU3").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU3_05.Visible = True
                Me.txtKAGO3KN_05.Text = CType(dr(0).Item("KAGO3BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO3KN_05.Visible = True
                Me.txtKAGO3ZEI_05.Text = CType(dr(0).Item("KAGO3ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO3ZEI_05.Visible = True
                'カゴ４
                Me.txtBALLSU4_05.Text = CType(dr(0).Item("BALLSU4").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU4_05.Visible = True
                Me.txtKAGO4KN_05.Text = CType(dr(0).Item("KAGO4BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO4KN_05.Visible = True
                Me.txtKAGO4ZEI_05.Text = CType(dr(0).Item("KAGO4ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO4ZEI_05.Visible = True
                'カゴ５
                Me.txtBALLSU5_05.Text = CType(dr(0).Item("BALLSU5").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU5_05.Visible = True
                Me.txtKAGO5KN_05.Text = CType(dr(0).Item("KAGO5BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO5KN_05.Visible = True
                Me.txtKAGO5ZEI_05.Text = CType(dr(0).Item("KAGO5ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO5ZEI_05.Visible = True
                'カゴ６
                Me.txtBALLSU6_05.Text = CType(dr(0).Item("BALLSU6").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU6_05.Visible = True
                Me.txtKAGO6KN_05.Text = CType(dr(0).Item("KAGO6BKN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO6KN_05.Visible = True
                Me.txtKAGO6ZEI_05.Text = CType(dr(0).Item("KAGO6ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO6ZEI_05.Visible = True
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 2階以上の営業情報がない場合1階の時間帯情報を参照表示
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTimeKbInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM EIGMTC")
            sql.Append(" WHERE")
            '料金体系
            sql.Append(" RKNKB = " & _intRKNKB)
            '顧客種別
            sql.Append(" AND NKBNO = 1")
            'フロア数
            sql.Append(" AND FLRKB = 1")

            sql.Append(" ORDER BY TIMEKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim dr As DataRow()

            '時間帯1
            dr = resultDt.Select("TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_01.ReadOnly = False
                '入場料
                Me.txtENTKIN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtENTKIN_01.Visible = True
                'ポイント
                Me.txtPOINT_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtPOINT_01.Visible = True
                'カゴ１
                Me.txtBALLSU1_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU1_01.Visible = True
                Me.txtKAGO1KN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO1KN_01.Visible = True
                Me.txtKAGO1ZEI_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO1ZEI_01.Visible = True
                'カゴ２
                Me.txtBALLSU2_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU2_01.Visible = True
                Me.txtKAGO2KN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO2KN_01.Visible = True
                Me.txtKAGO2ZEI_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO2ZEI_01.Visible = True
                'カゴ３
                Me.txtBALLSU3_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU3_01.Visible = True
                Me.txtKAGO3KN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO3KN_01.Visible = True
                Me.txtKAGO3ZEI_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO3ZEI_01.Visible = True
                'カゴ４
                Me.txtBALLSU4_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU4_01.Visible = True
                Me.txtKAGO4KN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO4KN_01.Visible = True
                Me.txtKAGO4ZEI_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO4ZEI_01.Visible = True
                'カゴ５
                Me.txtBALLSU5_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU5_01.Visible = True
                Me.txtKAGO5KN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO5KN_01.Visible = True
                Me.txtKAGO5ZEI_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO5ZEI_01.Visible = True
                'カゴ６
                Me.txtBALLSU6_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtBALLSU6_01.Visible = True
                Me.txtKAGO6KN_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO6KN_01.Visible = True
                Me.txtKAGO6ZEI_01.Text = "0"
                If Not Me.txtPASSCD_01.Text.Equals("99") Then Me.txtKAGO6ZEI_01.Visible = True
            End If

            '時間帯2
            dr = resultDt.Select("TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_02.ReadOnly = False
                '入場料
                Me.txtENTKIN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtENTKIN_02.Visible = True
                'ポイント
                Me.txtPOINT_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtPOINT_02.Visible = True
                'カゴ１
                Me.txtBALLSU1_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU1_02.Visible = True
                Me.txtKAGO1KN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO1KN_02.Visible = True
                Me.txtKAGO1ZEI_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO1ZEI_02.Visible = True
                'カゴ２
                Me.txtBALLSU2_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU2_02.Visible = True
                Me.txtKAGO2KN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO2KN_02.Visible = True
                Me.txtKAGO2ZEI_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO2ZEI_02.Visible = True
                'カゴ３
                Me.txtBALLSU3_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU3_02.Visible = True
                Me.txtKAGO3KN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO3KN_02.Visible = True
                Me.txtKAGO3ZEI_02.Text = CType(dr(0).Item("KAGO3ZEI").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO3ZEI_02.Visible = True
                'カゴ４
                Me.txtBALLSU4_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU4_02.Visible = True
                Me.txtKAGO4KN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO4KN_02.Visible = True
                Me.txtKAGO4ZEI_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO4ZEI_02.Visible = True
                'カゴ５
                Me.txtBALLSU5_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU5_02.Visible = True
                Me.txtKAGO5KN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO5KN_02.Visible = True
                Me.txtKAGO5ZEI_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO5ZEI_02.Visible = True
                'カゴ６
                Me.txtBALLSU6_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtBALLSU6_02.Visible = True
                Me.txtKAGO6KN_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO6KN_02.Visible = True
                Me.txtKAGO6ZEI_02.Text = "0"
                If Not Me.txtPASSCD_02.Text.Equals("99") Then Me.txtKAGO6ZEI_02.Visible = True
            End If

            '時間帯3
            dr = resultDt.Select("TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_03.ReadOnly = False
                '入場料
                Me.txtENTKIN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtENTKIN_03.Visible = True
                'ポイント
                Me.txtPOINT_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtPOINT_03.Visible = True
                'カゴ１
                Me.txtBALLSU1_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU1_03.Visible = True
                Me.txtKAGO1KN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO1KN_03.Visible = True
                Me.txtKAGO1ZEI_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO1ZEI_03.Visible = True
                'カゴ２
                Me.txtBALLSU2_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU2_03.Visible = True
                Me.txtKAGO2KN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO2KN_03.Visible = True
                Me.txtKAGO2ZEI_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO2ZEI_03.Visible = True
                'カゴ３
                Me.txtBALLSU3_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU3_03.Visible = True
                Me.txtKAGO3KN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO3KN_03.Visible = True
                Me.txtKAGO3ZEI_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO3ZEI_03.Visible = True
                'カゴ４
                Me.txtBALLSU4_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU4_03.Visible = True
                Me.txtKAGO4KN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO4KN_03.Visible = True
                Me.txtKAGO4ZEI_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO4ZEI_03.Visible = True
                'カゴ５
                Me.txtBALLSU5_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU5_03.Visible = True
                Me.txtKAGO5KN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO5KN_03.Visible = True
                Me.txtKAGO5ZEI_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO5ZEI_03.Visible = True
                'カゴ６
                Me.txtBALLSU6_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtBALLSU6_03.Visible = True
                Me.txtKAGO6KN_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO6KN_03.Visible = True
                Me.txtKAGO6ZEI_03.Text = "0"
                If Not Me.txtPASSCD_03.Text.Equals("99") Then Me.txtKAGO6ZEI_03.Visible = True
            End If

            '時間帯4
            dr = resultDt.Select("TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_04.ReadOnly = False
                '入場料
                Me.txtENTKIN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtENTKIN_04.Visible = True
                'ポイント
                Me.txtPOINT_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtPOINT_04.Visible = True
                'カゴ１
                Me.txtBALLSU1_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU1_04.Visible = True
                Me.txtKAGO1KN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO1KN_04.Visible = True
                Me.txtKAGO1ZEI_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO1ZEI_04.Visible = True
                'カゴ２
                Me.txtBALLSU2_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU2_04.Visible = True
                Me.txtKAGO2KN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO2KN_04.Visible = True
                Me.txtKAGO2ZEI_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO2ZEI_04.Visible = True
                'カゴ３
                Me.txtBALLSU3_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU3_04.Visible = True
                Me.txtKAGO3KN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO3KN_04.Visible = True
                Me.txtKAGO3ZEI_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO3ZEI_04.Visible = True
                'カゴ４
                Me.txtBALLSU4_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU4_04.Visible = True
                Me.txtKAGO4KN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO4KN_04.Visible = True
                Me.txtKAGO4ZEI_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO4ZEI_04.Visible = True
                'カゴ５
                Me.txtBALLSU5_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU5_04.Visible = True
                Me.txtKAGO5KN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO5KN_04.Visible = True
                Me.txtKAGO5ZEI_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO5ZEI_04.Visible = True
                'カゴ６
                Me.txtBALLSU6_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtBALLSU6_04.Visible = True
                Me.txtKAGO6KN_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO6KN_04.Visible = True
                Me.txtKAGO6ZEI_04.Text = "0"
                If Not Me.txtPASSCD_04.Text.Equals("99") Then Me.txtKAGO6ZEI_04.Visible = True
            End If

            '時間帯5
            dr = resultDt.Select("TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD_05.ReadOnly = False
                '入場料
                Me.txtENTKIN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtENTKIN_05.Visible = True
                'ポイント
                Me.txtPOINT_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtPOINT_05.Visible = True
                'カゴ１
                Me.txtBALLSU1_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU1_05.Visible = True
                Me.txtKAGO1KN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO1KN_05.Visible = True
                Me.txtKAGO1ZEI_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO1ZEI_05.Visible = True
                'カゴ２
                Me.txtBALLSU2_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU2_05.Visible = True
                Me.txtKAGO2KN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO2KN_05.Visible = True
                Me.txtKAGO2ZEI_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO2ZEI_05.Visible = True
                'カゴ３
                Me.txtBALLSU3_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU3_05.Visible = True
                Me.txtKAGO3KN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO3KN_05.Visible = True
                Me.txtKAGO3ZEI_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO3ZEI_05.Visible = True
                'カゴ４
                Me.txtBALLSU4_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU4_05.Visible = True
                Me.txtKAGO4KN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO4KN_05.Visible = True
                Me.txtKAGO4ZEI_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO4ZEI_05.Visible = True
                'カゴ５
                Me.txtBALLSU5_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU5_05.Visible = True
                Me.txtKAGO5KN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO5KN_05.Visible = True
                Me.txtKAGO5ZEI_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO5ZEI_05.Visible = True
                'カゴ６
                Me.txtBALLSU6_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtBALLSU6_05.Visible = True
                Me.txtKAGO6KN_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO6KN_05.Visible = True
                Me.txtKAGO6ZEI_05.Text = "0"
                If Not Me.txtPASSCD_05.Text.Equals("99") Then Me.txtKAGO6ZEI_05.Visible = True
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Register() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            'トランザクション開始
            iDatabase.BeginTransaction()

            If Not iDatabase.ExecuteUpdate("DELETE FROM EIGMTC WHERE RKNKB = " & _intRKNKB & " AND NKBNO = " & (Me.cmbKBMAST.SelectedIndex + 1) & " AND FLRKB = " & _intFLRKB) Then
                iDatabase.RollBack()
                Return False
            End If

            Dim TIMENM As TextBox
            Dim PASSCD As TextBox
            Dim ENTKIN As TextBox
            Dim POINT As TextBox
            Dim BALLSU1 As TextBox : Dim KAGO1KN As TextBox : Dim KAGO1ZEI As TextBox
            Dim BALLSU2 As TextBox : Dim KAGO2KN As TextBox : Dim KAGO2ZEI As TextBox
            Dim BALLSU3 As TextBox : Dim KAGO3KN As TextBox : Dim KAGO3ZEI As TextBox
            Dim BALLSU4 As TextBox : Dim KAGO4KN As TextBox : Dim KAGO4ZEI As TextBox
            Dim BALLSU5 As TextBox : Dim KAGO5KN As TextBox : Dim KAGO5ZEI As TextBox
            Dim BALLSU6 As TextBox : Dim KAGO6KN As TextBox : Dim KAGO6ZEI As TextBox
            Dim intUpdTIMEKB As Integer = 1
            Dim intUpdCount As Integer = 0  'SQL更新処理件数

            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                BALLSU1 = Nothing : KAGO1KN = Nothing : KAGO1ZEI = Nothing
                BALLSU2 = Nothing : KAGO2KN = Nothing : KAGO2ZEI = Nothing
                BALLSU3 = Nothing : KAGO3KN = Nothing : KAGO3ZEI = Nothing
                BALLSU4 = Nothing : KAGO4KN = Nothing : KAGO4ZEI = Nothing
                BALLSU5 = Nothing : KAGO5KN = Nothing : KAGO5ZEI = Nothing
                BALLSU6 = Nothing : KAGO6KN = Nothing : KAGO6ZEI = Nothing

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM_01
                        PASSCD = Me.txtPASSCD_01
                        ENTKIN = Me.txtENTKIN_01
                        POINT = Me.txtPOINT_01
                        BALLSU1 = Me.txtBALLSU1_01
                        KAGO1KN = Me.txtKAGO1KN_01
                        KAGO1ZEI = Me.txtKAGO1ZEI_01
                        BALLSU2 = Me.txtBALLSU2_01
                        KAGO2KN = Me.txtKAGO2KN_01
                        KAGO2ZEI = Me.txtKAGO2ZEI_01
                        BALLSU3 = Me.txtBALLSU3_01
                        KAGO3KN = Me.txtKAGO3KN_01
                        KAGO3ZEI = Me.txtKAGO3ZEI_01
                        BALLSU4 = Me.txtBALLSU4_01
                        KAGO4KN = Me.txtKAGO4KN_01
                        KAGO4ZEI = Me.txtKAGO4ZEI_01
                        BALLSU5 = Me.txtBALLSU5_01
                        KAGO5KN = Me.txtKAGO5KN_01
                        KAGO5ZEI = Me.txtKAGO5ZEI_01
                        BALLSU6 = Me.txtBALLSU6_01
                        KAGO6KN = Me.txtKAGO6KN_01
                        KAGO6ZEI = Me.txtKAGO6ZEI_01
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM_02
                        PASSCD = Me.txtPASSCD_02
                        ENTKIN = Me.txtENTKIN_02
                        POINT = Me.txtPOINT_02
                        BALLSU1 = Me.txtBALLSU1_02
                        KAGO1KN = Me.txtKAGO1KN_02
                        KAGO1ZEI = Me.txtKAGO1ZEI_02
                        BALLSU2 = Me.txtBALLSU2_02
                        KAGO2KN = Me.txtKAGO2KN_02
                        KAGO2ZEI = Me.txtKAGO2ZEI_02
                        BALLSU3 = Me.txtBALLSU3_02
                        KAGO3KN = Me.txtKAGO3KN_02
                        KAGO3ZEI = Me.txtKAGO3ZEI_02
                        BALLSU4 = Me.txtBALLSU4_02
                        KAGO4KN = Me.txtKAGO4KN_02
                        KAGO4ZEI = Me.txtKAGO4ZEI_02
                        BALLSU5 = Me.txtBALLSU5_02
                        KAGO5KN = Me.txtKAGO5KN_02
                        KAGO5ZEI = Me.txtKAGO5ZEI_02
                        BALLSU6 = Me.txtBALLSU6_02
                        KAGO6KN = Me.txtKAGO6KN_02
                        KAGO6ZEI = Me.txtKAGO6ZEI_02
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM_03
                        PASSCD = Me.txtPASSCD_03
                        ENTKIN = Me.txtENTKIN_03
                        POINT = Me.txtPOINT_03
                        BALLSU1 = Me.txtBALLSU1_03
                        KAGO1KN = Me.txtKAGO1KN_03
                        KAGO1ZEI = Me.txtKAGO1ZEI_03
                        BALLSU2 = Me.txtBALLSU2_03
                        KAGO2KN = Me.txtKAGO2KN_03
                        KAGO2ZEI = Me.txtKAGO2ZEI_03
                        BALLSU3 = Me.txtBALLSU3_03
                        KAGO3KN = Me.txtKAGO3KN_03
                        KAGO3ZEI = Me.txtKAGO3ZEI_03
                        BALLSU4 = Me.txtBALLSU4_03
                        KAGO4KN = Me.txtKAGO4KN_03
                        KAGO4ZEI = Me.txtKAGO4ZEI_03
                        BALLSU5 = Me.txtBALLSU5_03
                        KAGO5KN = Me.txtKAGO5KN_03
                        KAGO5ZEI = Me.txtKAGO5ZEI_03
                        BALLSU6 = Me.txtBALLSU6_03
                        KAGO6KN = Me.txtKAGO6KN_03
                        KAGO6ZEI = Me.txtKAGO6ZEI_03
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM_04
                        PASSCD = Me.txtPASSCD_04
                        ENTKIN = Me.txtENTKIN_04
                        POINT = Me.txtPOINT_04
                        BALLSU1 = Me.txtBALLSU1_04
                        KAGO1KN = Me.txtKAGO1KN_04
                        KAGO1ZEI = Me.txtKAGO1ZEI_04
                        BALLSU2 = Me.txtBALLSU2_04
                        KAGO2KN = Me.txtKAGO2KN_04
                        KAGO2ZEI = Me.txtKAGO2ZEI_04
                        BALLSU3 = Me.txtBALLSU3_04
                        KAGO3KN = Me.txtKAGO3KN_04
                        KAGO3ZEI = Me.txtKAGO3ZEI_04
                        BALLSU4 = Me.txtBALLSU4_04
                        KAGO4KN = Me.txtKAGO4KN_04
                        KAGO4ZEI = Me.txtKAGO4ZEI_04
                        BALLSU5 = Me.txtBALLSU5_04
                        KAGO5KN = Me.txtKAGO5KN_04
                        KAGO5ZEI = Me.txtKAGO5ZEI_04
                        BALLSU6 = Me.txtBALLSU6_04
                        KAGO6KN = Me.txtKAGO6KN_04
                        KAGO6ZEI = Me.txtKAGO6ZEI_04
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM_05
                        PASSCD = Me.txtPASSCD_05
                        ENTKIN = Me.txtENTKIN_05
                        POINT = Me.txtPOINT_05
                        BALLSU1 = Me.txtBALLSU1_05
                        KAGO1KN = Me.txtKAGO1KN_05
                        KAGO1ZEI = Me.txtKAGO1ZEI_05
                        BALLSU2 = Me.txtBALLSU2_05
                        KAGO2KN = Me.txtKAGO2KN_05
                        KAGO2ZEI = Me.txtKAGO2ZEI_05
                        BALLSU3 = Me.txtBALLSU3_05
                        KAGO3KN = Me.txtKAGO3KN_05
                        KAGO3ZEI = Me.txtKAGO3ZEI_05
                        BALLSU4 = Me.txtBALLSU4_05
                        KAGO4KN = Me.txtKAGO4KN_05
                        KAGO4ZEI = Me.txtKAGO4ZEI_05
                        BALLSU5 = Me.txtBALLSU5_05
                        KAGO5KN = Me.txtKAGO5KN_05
                        KAGO5ZEI = Me.txtKAGO5ZEI_05
                        BALLSU6 = Me.txtBALLSU6_05
                        KAGO6KN = Me.txtKAGO6KN_05
                        KAGO6ZEI = Me.txtKAGO6ZEI_05
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTC VALUES(")
                sql.Append(_intRKNKB & ",")                                                 '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(_intFLRKB & ",")                                                 'フロア区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(BALLSU1.Text, Integer) & ",")                              'カゴ１
                sql.Append(CType(KAGO1KN.Text, Integer) & ",")
                sql.Append(CType(KAGO1KN.Text, Integer) & ",")
                sql.Append(CType(KAGO1ZEI.Text, Integer) & ",")
                sql.Append(CType(BALLSU2.Text, Integer) & ",")                              'カゴ２
                sql.Append(CType(KAGO2KN.Text, Integer) & ",")
                sql.Append(CType(KAGO2KN.Text, Integer) & ",")
                sql.Append(CType(KAGO2ZEI.Text, Integer) & ",")
                sql.Append(CType(BALLSU3.Text, Integer) & ",")                              'カゴ３
                sql.Append(CType(KAGO3KN.Text, Integer) & ",")
                sql.Append(CType(KAGO3KN.Text, Integer) & ",")
                sql.Append(CType(KAGO3ZEI.Text, Integer) & ",")
                sql.Append(CType(BALLSU4.Text, Integer) & ",")                              'カゴ４
                sql.Append(CType(KAGO4KN.Text, Integer) & ",")
                sql.Append(CType(KAGO4KN.Text, Integer) & ",")
                sql.Append(CType(KAGO4ZEI.Text, Integer) & ",")
                sql.Append(CType(BALLSU5.Text, Integer) & ",")                              'カゴ５
                sql.Append(CType(KAGO5KN.Text, Integer) & ",")
                sql.Append(CType(KAGO5KN.Text, Integer) & ",")
                sql.Append(CType(KAGO5ZEI.Text, Integer) & ",")
                sql.Append(CType(BALLSU6.Text, Integer) & ",")                              'カゴ６
                sql.Append(CType(KAGO6KN.Text, Integer) & ",")
                sql.Append(CType(KAGO6KN.Text, Integer) & ",")
                sql.Append(CType(KAGO6ZEI.Text, Integer) & ",")
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワード・打席指定区分を更新
                intUpdCount = 0
                For j As Integer = 1 To 10  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTC SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "' WHERE RKNKB = " & _intRKNKB & " AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j & " AND FLRKB = " & _intFLRKB)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTC VALUES(")
                        sql.Append(_intRKNKB & ",")                                                 '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(_intFLRKB & ",")                                                 'フロア区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(BALLSU1.Text, Integer) & ",")                              'カゴ１
                        sql.Append(CType(KAGO1KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO1KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO1ZEI.Text, Integer) & ",")
                        sql.Append(CType(BALLSU2.Text, Integer) & ",")                              'カゴ２
                        sql.Append(CType(KAGO2KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO2KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO2ZEI.Text, Integer) & ",")
                        sql.Append(CType(BALLSU3.Text, Integer) & ",")                              'カゴ３
                        sql.Append(CType(KAGO3KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO3KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO3ZEI.Text, Integer) & ",")
                        sql.Append(CType(BALLSU4.Text, Integer) & ",")                              'カゴ４
                        sql.Append(CType(KAGO4KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO4KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO4ZEI.Text, Integer) & ",")
                        sql.Append(CType(BALLSU5.Text, Integer) & ",")                              'カゴ５
                        sql.Append(CType(KAGO5KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO5KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO5ZEI.Text, Integer) & ",")
                        sql.Append(CType(BALLSU6.Text, Integer) & ",")                              'カゴ６
                        sql.Append(CType(KAGO6KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO6KN.Text, Integer) & ",")
                        sql.Append(CType(KAGO6ZEI.Text, Integer) & ",")
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTC WHERE RKNKB = " & _intRKNKB & " AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            'コミット
            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function

#End Region





End Class




