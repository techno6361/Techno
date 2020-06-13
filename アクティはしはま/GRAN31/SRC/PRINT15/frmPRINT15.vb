Imports Techno.DataBase
Imports Microsoft.Office.Interop

Public Class frmPRINT15

#Region "▼宣言部"

    '平日
    Private _intTanka1_MEMBER As Integer = 0
    Private _intTanka1_VISITOR As Integer = 0
    Private _intTanka1_SINIOR_M As Integer = 0
    Private _intTanka1_SINIOR_V As Integer = 0
    Private _intTanka1_JUNIOR As Integer = 0
    Private _intTanka1_MORNING_M As Integer = 0
    Private _intTanka1_MORNING_V As Integer = 0
    Private _intTanka1_U40 As Integer = 0
    Private _intTanka1_BT As Integer = 0
    '休日
    Private _intTanka2_MEMBER As Integer = 0
    Private _intTanka2_VISITOR As Integer = 0
    Private _intTanka2_SINIOR_M As Integer = 0
    Private _intTanka2_SINIOR_V As Integer = 0
    Private _intTanka2_JUNIOR As Integer = 0
    Private _intTanka2_MORNING_M As Integer = 0
    Private _intTanka2_MORNING_V As Integer = 0
    Private _intTanka2_U40 As Integer = 0
    Private _intTanka2_BT As Integer = 0


#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "売上集計"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "売上集計"

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
    Private Sub frmPRINT04_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '商品コードコンボボックス取得
            GetHININFO()

            '帳票設定マスタ取得
            GetPRTMST()

            'コース料単価取得
            GetKOSUMTA()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 対象年テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtYear_Validated(sender As System.Object, e As System.EventArgs) Handles txtYear.Validated
        Try
            '天気・気温情報取得
            GetTenki()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 対象月コンボボックス_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        Try
            '天気・気温情報取得
            GetTenki()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func8()
        Try
            Me.Cursor = Cursors.WaitCursor

            If String.IsNullOrEmpty(Me.txtYear.Text) Then
                Using frm As New frmMSGBOX01("対象年を入力してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.txtYear.Focus()
                Exit Sub
            End If

            GetTenki()

            'ゴルフ場売上総計表
            If Me.chkPrt1.Checked Then
                If Not Print1() Then
                    Using frm As New frmMSGBOX01("ゴルフ場売上総計表の出力に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If

            '入金売上集計表
            If Me.chkPrt2.Checked Then
                If Not Print2() Then
                    Using frm As New frmMSGBOX01("入金売上集計表の出力に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If

            '雑収入集計表
            If Me.chkPrt3.Checked Then
                If Not Print3() Then
                    Using frm As New frmMSGBOX01("雑収入集計表の出力に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If

            'グリーンフィー集計表
            If Me.chkPrt4.Checked Then
                If Not Print4() Then
                    Using frm As New frmMSGBOX01("グリーンフィー集計表の出力に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If

            '打放し球数集計表
            If Me.chkPrt5.Checked Then
                If Not Print5() Then
                    Using frm As New frmMSGBOX01("打放し球数集計表の出力に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 気温テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKION_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtKION1.KeyPress, txtKION2.KeyPress, txtKION3.KeyPress, txtKION4.KeyPress, txtKION5.KeyPress, txtKION6.KeyPress _
                                                                                                            , txtKION7.KeyPress, txtKION8.KeyPress, txtKION9.KeyPress, txtKION10.KeyPress, txtKION11.KeyPress, txtKION12.KeyPress _
                                                                                                            , txtKION13.KeyPress, txtKION14.KeyPress, txtKION15.KeyPress, txtKION16.KeyPress, txtKION17.KeyPress, txtKION18.KeyPress _
                                                                                                            , txtKION19.KeyPress, txtKION20.KeyPress, txtKION21.KeyPress, txtKION22.KeyPress, txtKION23.KeyPress, txtKION24.KeyPress _
                                                                                                            , txtKION25.KeyPress, txtKION26.KeyPress, txtKION27.KeyPress, txtKION28.KeyPress, txtKION29.KeyPress, txtKION30.KeyPress _
                                                                                                            , txtKION31.KeyPress
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
    ''' テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtYear.KeyPress
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
    ''' 商品コードクリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHINClear_Click(sender As System.Object, e As System.EventArgs) Handles btnHINClear4.Click, btnHINClear5.Click _
                                                                                        , btnHINClear6.Click, btnHINClear7.Click, btnHINClear8.Click, btnHINClear9.Click, btnHINClear10.Click, btnHINClear11.Click
        Try
            Dim btn As Button

            btn = CType(sender, Button)

            Select Case btn.Name
                Case "btnHINClear4" : Me.cmbHINCD4.SelectedIndex = -1
                Case "btnHINClear5" : Me.cmbHINCD5.SelectedIndex = -1
                Case "btnHINClear6" : Me.cmbHINCD6.SelectedIndex = -1
                Case "btnHINClear7" : Me.cmbHINCD7.SelectedIndex = -1
                Case "btnHINClear8" : Me.cmbHINCD8.SelectedIndex = -1
                Case "btnHINClear9" : Me.cmbHINCD9.SelectedIndex = -1
                Case "btnHINClear10" : Me.cmbHINCD10.SelectedIndex = -1
                Case "btnHINClear11" : Me.cmbHINCD11.SelectedIndex = -1
            End Select

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
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtYear.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

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
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtYear.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 商品コード更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdPRT05_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdPRT05.Click
        Dim sql As New System.Text.StringBuilder
        Dim strHINBUN1 As String = "000"
        Dim strHINCD1 As String = "000"
        Dim strHINBUN2 As String = "000"
        Dim strHINCD2 As String = "000"
        Dim strHINBUN3 As String = "000"
        Dim strHINCD3 As String = "000"
        Dim strHINBUN4 As String = "000"
        Dim strHINCD4 As String = "000"
        Dim strHINBUN5 As String = "000"
        Dim strHINCD5 As String = "000"
        Dim strHINBUN6 As String = "000"
        Dim strHINCD6 As String = "000"
        Dim strHINBUN7 As String = "000"
        Dim strHINCD7 As String = "000"
        Dim strHINBUN8 As String = "000"
        Dim strHINCD8 As String = "000"
        Dim strHINBUN9 As String = "000"
        Dim strHINCD9 As String = "000"
        Dim strHINBUN10 As String = "000"
        Dim strHINCD10 As String = "000"
        Dim strHINBUN11 As String = "000"
        Dim strHINCD11 As String = "000"
        Try
        
            If Me.cmbHINCD4.SelectedIndex >= 0 Then
                strHINBUN4 = Me.cmbHINCD4.SelectedValue.ToString.Substring(0, 3)
                strHINCD4 = Me.cmbHINCD4.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD5.SelectedIndex >= 0 Then
                strHINBUN5 = Me.cmbHINCD5.SelectedValue.ToString.Substring(0, 3)
                strHINCD5 = Me.cmbHINCD5.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD6.SelectedIndex >= 0 Then
                strHINBUN6 = Me.cmbHINCD6.SelectedValue.ToString.Substring(0, 3)
                strHINCD6 = Me.cmbHINCD6.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD7.SelectedIndex >= 0 Then
                strHINBUN7 = Me.cmbHINCD7.SelectedValue.ToString.Substring(0, 3)
                strHINCD7 = Me.cmbHINCD7.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD8.SelectedIndex >= 0 Then
                strHINBUN8 = Me.cmbHINCD8.SelectedValue.ToString.Substring(0, 3)
                strHINCD8 = Me.cmbHINCD8.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD9.SelectedIndex >= 0 Then
                strHINBUN9 = Me.cmbHINCD9.SelectedValue.ToString.Substring(0, 3)
                strHINCD9 = Me.cmbHINCD9.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD10.SelectedIndex >= 0 Then
                strHINBUN10 = Me.cmbHINCD10.SelectedValue.ToString.Substring(0, 3)
                strHINCD10 = Me.cmbHINCD10.SelectedValue.ToString.Substring(3, 3)
            End If
            If Me.cmbHINCD11.SelectedIndex >= 0 Then
                strHINBUN11 = Me.cmbHINCD11.SelectedValue.ToString.Substring(0, 3)
                strHINCD11 = Me.cmbHINCD11.SelectedValue.ToString.Substring(3, 3)
            End If
            iDatabase.BeginTransaction()

            Try
                sql.Clear()
                sql.Append("UPDATE PRTMST SET")
                sql.Append(" HINBUN1 = " & CType(strHINBUN1, Integer))
                sql.Append(",HINCD1 = " & CType(strHINCD1, Integer))
                sql.Append(",HINBUN2 = " & CType(strHINBUN2, Integer))
                sql.Append(",HINCD2 = " & CType(strHINCD2, Integer))
                sql.Append(",HINBUN3 = " & CType(strHINBUN3, Integer))
                sql.Append(",HINCD3 = " & CType(strHINCD3, Integer))
                sql.Append(",HINBUN4 = " & CType(strHINBUN4, Integer))
                sql.Append(",HINCD4 = " & CType(strHINCD4, Integer))
                sql.Append(",HINBUN5 = " & CType(strHINBUN5, Integer))
                sql.Append(",HINCD5 = " & CType(strHINCD5, Integer))
                sql.Append(",HINBUN6 = " & CType(strHINBUN6, Integer))
                sql.Append(",HINCD6 = " & CType(strHINCD6, Integer))
                sql.Append(",HINBUN7 = " & CType(strHINBUN7, Integer))
                sql.Append(",HINCD7 = " & CType(strHINCD7, Integer))
                sql.Append(",HINBUN8 = " & CType(strHINBUN8, Integer))
                sql.Append(",HINCD8 = " & CType(strHINCD8, Integer))
                sql.Append(",HINBUN9 = " & CType(strHINBUN9, Integer))
                sql.Append(",HINCD9 = " & CType(strHINCD9, Integer))
                sql.Append(",HINBUN10 = " & CType(strHINBUN10, Integer))
                sql.Append(",HINCD10 = " & CType(strHINCD10, Integer))
                sql.Append(",HINBUN11 = " & CType(strHINBUN11, Integer))
                sql.Append(",HINCD11 = " & CType(strHINCD11, Integer))

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Using frm As New frmMSGBOX01("帳票マスタの更新に失敗しました。", 2)
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
    ''' 天気更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdTenki_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdTenki.Click
        Dim dcKION As Decimal = 0
        Dim sql As New System.Text.StringBuilder
        Try
            Dim cmbTENKI As ComboBox = Nothing
            Dim txtKION As TextBox = Nothing

            For i As Integer = 1 To 31
                If i.Equals(1) Then txtKION = Me.txtKION1
                If i.Equals(2) Then txtKION = Me.txtKION2
                If i.Equals(3) Then txtKION = Me.txtKION3
                If i.Equals(4) Then txtKION = Me.txtKION4
                If i.Equals(5) Then txtKION = Me.txtKION5
                If i.Equals(6) Then txtKION = Me.txtKION6
                If i.Equals(7) Then txtKION = Me.txtKION7
                If i.Equals(8) Then txtKION = Me.txtKION8
                If i.Equals(9) Then txtKION = Me.txtKION9
                If i.Equals(10) Then txtKION = Me.txtKION10
                If i.Equals(11) Then txtKION = Me.txtKION11
                If i.Equals(12) Then txtKION = Me.txtKION12
                If i.Equals(13) Then txtKION = Me.txtKION13
                If i.Equals(14) Then txtKION = Me.txtKION14
                If i.Equals(15) Then txtKION = Me.txtKION15
                If i.Equals(16) Then txtKION = Me.txtKION16
                If i.Equals(17) Then txtKION = Me.txtKION17
                If i.Equals(18) Then txtKION = Me.txtKION18
                If i.Equals(19) Then txtKION = Me.txtKION19
                If i.Equals(20) Then txtKION = Me.txtKION20
                If i.Equals(21) Then txtKION = Me.txtKION21
                If i.Equals(22) Then txtKION = Me.txtKION22
                If i.Equals(23) Then txtKION = Me.txtKION23
                If i.Equals(24) Then txtKION = Me.txtKION24
                If i.Equals(25) Then txtKION = Me.txtKION25
                If i.Equals(26) Then txtKION = Me.txtKION26
                If i.Equals(27) Then txtKION = Me.txtKION27
                If i.Equals(28) Then txtKION = Me.txtKION28
                If i.Equals(29) Then txtKION = Me.txtKION29
                If i.Equals(30) Then txtKION = Me.txtKION30
                If i.Equals(31) Then txtKION = Me.txtKION31

                If Not String.IsNullOrEmpty(txtKION.Text) Then
                    If Not Decimal.TryParse(txtKION.Text.Trim, dcKION) Then
                        Using frm As New frmMSGBOX01("気温の値が異常です。", 3)
                            frm.ShowDialog()
                        End Using
                        txtKION.Focus()
                        txtKION.SelectAll()
                        Exit Sub
                    End If
                End If
            Next





            iDatabase.BeginTransaction()

            For i As Integer = 1 To 31
                If i.Equals(1) Then
                    If Not Me.pnl1.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI1 : txtKION = Me.txtKION1
                End If
                If i.Equals(2) Then
                    If Not Me.pnl2.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI2 : txtKION = Me.txtKION2
                End If
                If i.Equals(3) Then
                    If Not Me.pnl3.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI3 : txtKION = Me.txtKION3
                End If
                If i.Equals(4) Then
                    If Not Me.pnl4.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI4 : txtKION = Me.txtKION4
                End If
                If i.Equals(5) Then
                    If Not Me.pnl5.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI5 : txtKION = Me.txtKION5
                End If
                If i.Equals(6) Then
                    If Not Me.pnl6.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI6 : txtKION = Me.txtKION6
                End If
                If i.Equals(7) Then
                    If Not Me.pnl7.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI7 : txtKION = Me.txtKION7
                End If
                If i.Equals(8) Then
                    If Not Me.pnl8.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI8 : txtKION = Me.txtKION8
                End If
                If i.Equals(9) Then
                    If Not Me.pnl9.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI9 : txtKION = Me.txtKION9
                End If
                If i.Equals(10) Then
                    If Not Me.pnl10.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI10 : txtKION = Me.txtKION10
                End If
                If i.Equals(11) Then
                    If Not Me.pnl11.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI11 : txtKION = Me.txtKION11
                End If
                If i.Equals(12) Then
                    If Not Me.pnl12.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI12 : txtKION = Me.txtKION12
                End If
                If i.Equals(13) Then
                    If Not Me.pnl13.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI13 : txtKION = Me.txtKION13
                End If
                If i.Equals(14) Then
                    If Not Me.pnl14.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI14 : txtKION = Me.txtKION14
                End If
                If i.Equals(15) Then
                    If Not Me.pnl15.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI15 : txtKION = Me.txtKION15
                End If
                If i.Equals(16) Then
                    If Not Me.pnl16.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI16 : txtKION = Me.txtKION16
                End If
                If i.Equals(17) Then
                    If Not Me.pnl17.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI17 : txtKION = Me.txtKION17
                End If
                If i.Equals(18) Then
                    If Not Me.pnl18.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI18 : txtKION = Me.txtKION18
                End If
                If i.Equals(19) Then
                    If Not Me.pnl19.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI19 : txtKION = Me.txtKION19
                End If
                If i.Equals(20) Then
                    If Not Me.pnl20.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI20 : txtKION = Me.txtKION20
                End If
                If i.Equals(21) Then
                    If Not Me.pnl21.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI21 : txtKION = Me.txtKION21
                End If
                If i.Equals(22) Then
                    If Not Me.pnl22.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI22 : txtKION = Me.txtKION22
                End If
                If i.Equals(23) Then
                    If Not Me.pnl23.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI23 : txtKION = Me.txtKION23
                End If
                If i.Equals(24) Then
                    If Not Me.pnl24.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI24 : txtKION = Me.txtKION24
                End If
                If i.Equals(25) Then
                    If Not Me.pnl25.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI25 : txtKION = Me.txtKION25
                End If
                If i.Equals(26) Then
                    If Not Me.pnl26.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI26 : txtKION = Me.txtKION26
                End If
                If i.Equals(27) Then
                    If Not Me.pnl27.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI27 : txtKION = Me.txtKION27
                End If
                If i.Equals(28) Then
                    If Not Me.pnl28.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI28 : txtKION = Me.txtKION28
                End If
                If i.Equals(29) Then
                    If Not Me.pnl29.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI29 : txtKION = Me.txtKION29
                End If
                If i.Equals(30) Then
                    If Not Me.pnl30.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI30 : txtKION = Me.txtKION30
                End If
                If i.Equals(31) Then
                    If Not Me.pnl31.Visible Then Continue For
                    cmbTENKI = Me.cmbTENKI31 : txtKION = Me.txtKION31
                End If
                Try
                    sql.Clear()
                    sql.Append("UPDATE CALMTA SET")
                    If String.IsNullOrEmpty(cmbTENKI.Text) Then
                        sql.Append(" TENKI = NULL")
                    Else
                        sql.Append(" TENKI = '" & cmbTENKI.Text & "'")
                    End If
                    If String.IsNullOrEmpty(txtKION.Text) Then
                        sql.Append(",KION = NULL")
                    Else
                        sql.Append(",KION = '" & txtKION.Text & "'")
                    End If
                    sql.Append(" WHERE")
                    sql.Append(" CALDT = '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & i.ToString.PadLeft(2, "0"c) & "'")

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
            Next

            iDatabase.Commit()

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
            Me.tspFunc8.Enabled = True
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            '年
            Me.txtYear.Text = Now.Year.ToString
            '月
            Me.cmbMonth.SelectedIndex = Now.Month - 1

            'Me.chkPrt1.Checked = True
            'Me.chkPrt2.Checked = True
            'Me.chkPrt3.Checked = True
            'Me.chkPrt4.Checked = True
            'Me.chkPrt5.Checked = True

   

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 商品コード取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHININFO() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(",(BUNCDB || HINCD) AS HINBUNCD")
            sql.Append(" FROM HINMTA")
            sql.Append(" WHERE")
            sql.Append(" BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND BUNCDB IN ('005','006')")
            sql.Append(" ORDER BY BUNCDB,HINCD")

            Dim resultDt1 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt2 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt3 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt4 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt5 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt6 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt7 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt8 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt9 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt10 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            Dim resultDt11 As DataTable = iDatabase.ExecuteRead(sql.ToString())


            If resultDt1.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.cmbHINCD4.DataSource = resultDt4
            Me.cmbHINCD4.ValueMember = "HINBUNCD"
            Me.cmbHINCD4.DisplayMember = "HINNMA"
            Me.cmbHINCD4.SelectedValue = 1

            Me.cmbHINCD5.DataSource = resultDt5
            Me.cmbHINCD5.ValueMember = "HINBUNCD"
            Me.cmbHINCD5.DisplayMember = "HINNMA"
            Me.cmbHINCD5.SelectedValue = 1

            Me.cmbHINCD6.DataSource = resultDt6
            Me.cmbHINCD6.ValueMember = "HINBUNCD"
            Me.cmbHINCD6.DisplayMember = "HINNMA"
            Me.cmbHINCD6.SelectedValue = 1

            Me.cmbHINCD7.DataSource = resultDt7
            Me.cmbHINCD7.ValueMember = "HINBUNCD"
            Me.cmbHINCD7.DisplayMember = "HINNMA"
            Me.cmbHINCD7.SelectedValue = 1

            Me.cmbHINCD8.DataSource = resultDt8
            Me.cmbHINCD8.ValueMember = "HINBUNCD"
            Me.cmbHINCD8.DisplayMember = "HINNMA"
            Me.cmbHINCD8.SelectedValue = 1

            Me.cmbHINCD9.DataSource = resultDt9
            Me.cmbHINCD9.ValueMember = "HINBUNCD"
            Me.cmbHINCD9.DisplayMember = "HINNMA"
            Me.cmbHINCD9.SelectedValue = 1

            Me.cmbHINCD10.DataSource = resultDt10
            Me.cmbHINCD10.ValueMember = "HINBUNCD"
            Me.cmbHINCD10.DisplayMember = "HINNMA"
            Me.cmbHINCD10.SelectedValue = 1

            Me.cmbHINCD11.DataSource = resultDt11
            Me.cmbHINCD11.ValueMember = "HINBUNCD"
            Me.cmbHINCD11.DisplayMember = "HINNMA"
            Me.cmbHINCD11.SelectedValue = 1

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 帳票設定マスタ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetPRTMST()
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM PRTMST")

            Dim dt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt.Rows.Count - 1
                Me.cmbHINCD4.SelectedValue = dt.Rows(i).Item("HINBUN4").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD4").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD5.SelectedValue = dt.Rows(i).Item("HINBUN5").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD5").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD6.SelectedValue = dt.Rows(i).Item("HINBUN6").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD6").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD7.SelectedValue = dt.Rows(i).Item("HINBUN7").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD7").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD8.SelectedValue = dt.Rows(i).Item("HINBUN8").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD8").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD9.SelectedValue = dt.Rows(i).Item("HINBUN9").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD9").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD10.SelectedValue = dt.Rows(i).Item("HINBUN10").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD10").ToString.PadLeft(3, "0"c)
                Me.cmbHINCD11.SelectedValue = dt.Rows(i).Item("HINBUN11").ToString.PadLeft(3, "0"c) & dt.Rows(i).Item("HINCD11").ToString.PadLeft(3, "0"c)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' コース料単価取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetKOSUMTA()
        Dim sql As New System.Text.StringBuilder
        Try

            'メンバー料金(平日)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = 1 AND NKBNO = 1")
            sql.Append(" ORDER BY PRCKB")

            Dim dt1 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt1.Rows.Count - 1
                Select Case dt1.Rows(i).Item("PRCKB").ToString
                    Case "1"    '一般
                        _intTanka1_MEMBER = CType(dt1.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "2"    'シニア
                        _intTanka1_SINIOR_M = CType(dt1.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "3"    '早朝・薄暮
                        _intTanka1_MORNING_M = CType(dt1.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "4"    '誕生月
                        _intTanka1_BT = _intTanka1_MEMBER - CType(dt1.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "5"    'U40
                        _intTanka1_U40 = _intTanka1_MEMBER - CType(dt1.Rows(i).Item("H09KIN").ToString, Integer)
                End Select
            Next
            'ビジター料金(平日)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = 1 AND NKBNO = 2")
            sql.Append(" ORDER BY PRCKB")

            Dim dt2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt2.Rows.Count - 1
                Select Case dt2.Rows(i).Item("PRCKB").ToString
                    Case "1"    '一般
                        _intTanka1_VISITOR = CType(dt2.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "2"    'シニア
                        _intTanka1_SINIOR_V = CType(dt2.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "3"    '早朝・薄暮
                        _intTanka1_MORNING_V = CType(dt2.Rows(i).Item("H09KIN").ToString, Integer)
                End Select
            Next
            'ジュニア料金(平日)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = 1 AND NKBNO = 3 AND PRCKB = 1")
            sql.Append(" ORDER BY PRCKB")

            Dim dt3 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt3.Rows.Count - 1
                _intTanka1_JUNIOR = CType(dt3.Rows(i).Item("H09KIN").ToString, Integer)
            Next

            'メンバー料金(休日)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = 2 AND NKBNO = 1")
            sql.Append(" ORDER BY PRCKB")

            Dim dt4 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt4.Rows.Count - 1
                Select Case dt4.Rows(i).Item("PRCKB").ToString
                    Case "1"    '一般
                        _intTanka2_MEMBER = CType(dt4.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "2"    'シニア
                        _intTanka2_SINIOR_M = CType(dt4.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "3"    '早朝・薄暮
                        _intTanka2_MORNING_M = CType(dt4.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "4"    '誕生月
                        _intTanka2_BT = _intTanka1_MEMBER - CType(dt4.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "5"    'U40
                        _intTanka2_U40 = _intTanka1_MEMBER - CType(dt4.Rows(i).Item("H09KIN").ToString, Integer)
                End Select
            Next
            'ビジター料金(休日)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = 2 AND NKBNO = 2")
            sql.Append(" ORDER BY PRCKB")

            Dim dt5 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt5.Rows.Count - 1
                Select Case dt5.Rows(i).Item("PRCKB").ToString
                    Case "1"    '一般
                        _intTanka2_VISITOR = CType(dt5.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "2"    'シニア
                        _intTanka2_SINIOR_V = CType(dt5.Rows(i).Item("H09KIN").ToString, Integer)
                    Case "3"    '早朝・薄暮
                        _intTanka2_MORNING_V = CType(dt5.Rows(i).Item("H09KIN").ToString, Integer)
                End Select
            Next
            'ジュニア料金(休日)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = 1 AND NKBNO = 3 AND PRCKB = 1")
            sql.Append(" ORDER BY PRCKB")

            Dim dt6 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dt6.Rows.Count - 1
                _intTanka2_JUNIOR = CType(dt6.Rows(i).Item("H09KIN").ToString, Integer)
            Next
     

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ゴルフ場売上総計表出力
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print1() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim drKOSU() As DataRow
        Dim drNYUKIN() As DataRow
        Dim drSHOHIN() As DataRow
        Dim drZATU() As DataRow
        Dim drGENKIN() As DataRow
        Dim drOTHER() As DataRow
        Dim drURIBALL() As DataRow
        Dim drURITIME() As DataRow
        Dim drURITAMA() As DataRow
        Dim drHAKKO() As DataRow
        Dim intRANGEKN As Integer = 0
        Dim intZATUKN As Integer = 0
        Dim intHAKKO As Integer = 0
        Try

            Dim strReportName As String = "ゴルフ場売上総計表"
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

            '出力日
            sheet.Cells(2, 40) = Now.ToString("yyyy/MM/dd HH:mm")
            '対象月
            sheet.Cells(5, 2) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分"

            '1球貸し
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ENTDT")
            sql.Append(",CASE WHEN SUM(ENTBKN) IS NULL THEN 0 ELSE SUM(ENTBKN) END AS ENTBKN")
            sql.Append(",CASE WHEN SUM(ENTZEI) IS NULL THEN 0 ELSE SUM(ENTZEI) END AS ENTZEI")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            '営業日付
            sql.Append(" AND ENTDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND ENTDT  <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND EIGKB = '1'")
            sql.Append(" AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10')")
            sql.Append(" GROUP BY ENTDT")

            Dim dtURITAMA As DataTable = iDatabase.ExecuteRead(sql.ToString())


            '打ち放題
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ENTDT")
            sql.Append(",CASE WHEN SUM(ENTBKN) IS NULL THEN 0 ELSE SUM(ENTBKN) END AS ENTBKN")
            sql.Append(",CASE WHEN SUM(ENTZEI) IS NULL THEN 0 ELSE SUM(ENTZEI) END AS ENTZEI")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            '営業日付
            sql.Append(" AND ENTDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND ENTDT  <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND EIGKB = '2'")
            sql.Append(" GROUP BY ENTDT")

            Dim dtURITIME As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'ボール料金
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" SEATDT")
            sql.Append(",CASE WHEN ")
            sql.Append(" (")
            sql.Append("SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN)")
            sql.Append(" + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN) + SUM(NKB12KIN)")
            sql.Append(")")
            sql.Append(" IS NULL THEN 0 ELSE")
            sql.Append(" (")
            sql.Append("SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN)")
            sql.Append(" + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN) + SUM(NKB12KIN)")
            sql.Append(") END AS NKBKIN")
            sql.Append(",CASE WHEN")
            sql.Append("(")
            sql.Append("SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN)")
            sql.Append(" + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN) + SUM(NKB12TAXKIN)")
            sql.Append(")")
            sql.Append(" IS NULL THEN 0 ELSE")
            sql.Append("(")
            sql.Append("SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN)")
            sql.Append(" + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN) + SUM(NKB12TAXKIN)")
            sql.Append(") END AS NKBTAXKIN")
            sql.Append(" FROM SEATSMA")
            sql.Append(" WHERE")
            sql.Append(" SEATDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND SEATDT  <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" GROUP BY SEATDT")

            Dim dtURIBALL As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '商品売上
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND BUNCDC <> '999'")
            sql.Append(" AND (BUNCDB IN ('001','002','003','004') OR (BUNCDB = '007' AND TKTKBN <> '002'))")
            sql.Append(" GROUP BY UDNDT")
            Dim dtSHOHIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'コース売上
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(",SUM(HOKENKIN + CARTKIN + COMPEKIN) AS ZATUKN")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" GROUP BY DENDT")
            Dim dtKOSU As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '入金売上
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '003' OR BUNCDA = '010'")
            sql.Append(" GROUP BY UDNDT")
            Dim dtNYUKIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '雑収入売上
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA = '001'")
            sql.Append(" AND (BUNCDB IN ('005','006') OR (BUNCDB = '007' AND TKTKBN = '002') OR (BUNCDC = '999'))")
            sql.Append(" GROUP BY UDNDT")
            Dim dtZATU As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'カード発行料(入金機)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append("A.DENDT")
            sql.Append(",SUM(A.PRERT) AS UDNBKN")
            sql.Append(" FROM NKNTRN AS A")
            sql.Append(" WHERE")
            sql.Append(" A.DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND A.DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND A.DATKB = '1'")
            sql.Append(" AND A.PRERT > 0")
            sql.Append(" GROUP BY DENDT")
            Dim dtHAKKO As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '現金売上
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA IN ('001','003','006','010')")
            sql.Append(" AND CPAYKBN = '0'")
            sql.Append(" GROUP BY UDNDT")
            Dim dtGENKIN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'その他売上
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM DUDNTRN")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002'")
            sql.Append(" AND BUNCDA IN ('001','003','006')")
            sql.Append(" AND CPAYKBN <> '0'")
            sql.Append(" GROUP BY UDNDT")
            Dim dtOTHER As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim strYM As String = Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c)

            '曜日・天気・気温
            For i As Integer = 1 To 31
                intRANGEKN = 0
                intZATUKN = 0
                intHAKKO = 0

                '入場料
                drURITAMA = dtURITAMA.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '打ち放題
                drURITIME = dtURITIME.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'ボール料金
                drURIBALL = dtURIBALL.Select("SEATDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品売上
                drSHOHIN = dtSHOHIN.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'コース売上
                drKOSU = dtKOSU.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '入金売上
                drNYUKIN = dtNYUKIN.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '雑収入売上
                drZATU = dtZATU.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'カード発行料(入金機)
                drHAKKO = dtHAKKO.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '現金売上
                drGENKIN = dtGENKIN.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'その他売上
                drOTHER = dtOTHER.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

                '入金
                If drNYUKIN.Length > 0 Then
                    sheet.Cells(9 + i, 12) = drNYUKIN(0).Item("UDNBKN").ToString
                End If

                '入場料
                If drURITAMA.Length > 0 Then
                    intRANGEKN += CType(drURITAMA(0).Item("ENTBKN").ToString, Integer)
                End If
                '打ち放題
                If drURITIME.Length > 0 Then
                    intRANGEKN += CType(drURITIME(0).Item("ENTBKN").ToString, Integer)
                End If
                'ボール料金
                If drURIBALL.Length > 0 Then
                    intRANGEKN += CType(drURIBALL(0).Item("NKBKIN").ToString, Integer)
                End If
                sheet.Cells(9 + i, 16) = intRANGEKN.ToString

                'コース
                If drKOSU.Length > 0 Then
                    sheet.Cells(9 + i, 20) = (CType(drKOSU(0).Item("UDNBKN").ToString, Integer) - CType(drKOSU(0).Item("ZATUKN").ToString, Integer)).ToString
                    intZATUKN = CType(drKOSU(0).Item("ZATUKN").ToString, Integer)
                End If
                '商品
                If drSHOHIN.Length > 0 Then
                    sheet.Cells(9 + i, 24) = drSHOHIN(0).Item("UDNBKN").ToString
                End If
                'カード発行料(入金機)
                If drHAKKO.Length > 0 Then
                    intZATUKN += CType(drHAKKO(0).Item("UDNBKN").ToString, Integer)
                    intHAKKO = CType(drHAKKO(0).Item("UDNBKN").ToString, Integer)
                End If
                '雑収入
                If drZATU.Length > 0 Then
                    sheet.Cells(9 + i, 28) = (CType(drZATU(0).Item("UDNBKN").ToString, Integer) + intZATUKN).ToString
                Else
                    sheet.Cells(9 + i, 28) = intZATUKN.ToString
                End If
                If drGENKIN.Length > 0 Then
                    sheet.Cells(9 + i, 36) = drGENKIN(0).Item("UDNBKN").ToString
                End If
                If drOTHER.Length > 0 Then
                    sheet.Cells(9 + i, 40) = (CType(drOTHER(0).Item("UDNBKN").ToString, Integer) + intHAKKO + intRANGEKN).ToString
                Else
                    sheet.Cells(9 + i, 40) = (intHAKKO + intRANGEKN).ToString
                End If

                If i.Equals(1) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI1.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI1.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION1.Text
                End If

                If i.Equals(2) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI2.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI2.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION2.Text
                End If

                If i.Equals(3) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI3.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI3.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION3.Text
                End If

                If i.Equals(4) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI4.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI4.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION4.Text
                End If

                If i.Equals(5) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI5.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI5.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION5.Text
                End If

                If i.Equals(6) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI6.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI6.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION6.Text
                End If

                If i.Equals(7) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI7.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI7.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION7.Text
                End If

                If i.Equals(8) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI8.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI8.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION8.Text
                End If

                If i.Equals(9) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI9.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI9.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION9.Text
                End If

                If i.Equals(10) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI10.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI10.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION10.Text
                End If

                If i.Equals(11) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI11.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI11.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION11.Text
                End If

                If i.Equals(12) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI12.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI12.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION12.Text
                End If

                If i.Equals(13) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI13.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI13.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION13.Text
                End If

                If i.Equals(14) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI14.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI14.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION14.Text
                End If

                If i.Equals(15) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI15.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI15.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION15.Text
                End If

                If i.Equals(16) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI16.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI16.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION16.Text
                End If

                If i.Equals(17) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI17.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI17.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION17.Text
                End If

                If i.Equals(18) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI18.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI18.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION18.Text
                End If

                If i.Equals(19) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI19.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI19.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION19.Text
                End If

                If i.Equals(20) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI20.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI20.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION20.Text
                End If

                If i.Equals(21) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI21.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI21.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION21.Text
                End If

                If i.Equals(22) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI22.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI22.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION22.Text
                End If

                If i.Equals(23) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI23.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI23.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION23.Text
                End If

                If i.Equals(24) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI24.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI24.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION24.Text
                End If

                If i.Equals(25) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI25.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI25.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION25.Text
                End If

                If i.Equals(26) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI26.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI26.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION26.Text
                End If

                If i.Equals(27) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI27.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI27.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION27.Text
                End If

                If i.Equals(28) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI28.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI28.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION28.Text
                End If

                If i.Equals(29) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI29.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI29.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION29.Text
                End If

                If i.Equals(30) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI30.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI30.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION30.Text
                End If

                If i.Equals(31) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI31.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI31.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION31.Text
                End If
            Next

            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 入金売上総計表出力
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print2() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim drCHARGE0() As DataRow
        Dim drCHARGE1() As DataRow
        Dim drFKaisu() As DataRow
        Dim drFGokeiKin() As DataRow
        Dim drHAKKO() As DataRow

        Try

            Dim strReportName As String = "入金売上集計表"
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

            '出力日
            sheet.Cells(2, 46) = Now.ToString("yyyy/MM/dd HH:mm")
            '対象月
            sheet.Cells(5, 2) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分"

            '入金額
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNMST")
            sql.Append(" WHERE")
            sql.Append(" STSFLG = '1'")
            Dim dtNKNMST As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtNKNMST.Rows.Count - 1
                Select Case dtNKNMST.Rows(i).Item("SEQNO").ToString
                    Case "1" : sheet.Cells(8, 14) = dtNKNMST.Rows(i).Item("NKNKN").ToString
                    Case "2" : sheet.Cells(8, 18) = dtNKNMST.Rows(i).Item("NKNKN").ToString
                    Case "3" : sheet.Cells(8, 22) = dtNKNMST.Rows(i).Item("NKNKN").ToString
                    Case "4" : sheet.Cells(8, 26) = dtNKNMST.Rows(i).Item("NKNKN").ToString
                    Case "5" : sheet.Cells(8, 30) = dtNKNMST.Rows(i).Item("NKNKN").ToString
                    Case "6" : sheet.Cells(8, 34) = dtNKNMST.Rows(i).Item("NKNKN").ToString
                End Select
            Next

            '入金機情報
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM REPOCHARGE_M")
            sql.Append(" WHERE")
            sql.Append(" CHARGEDAY >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND CHARGEDAY <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND NKNKBN = 0")
            Dim dtCHARGE0 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '簡易型入金機情報
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM REPOCHARGE_M")
            sql.Append(" WHERE")
            sql.Append(" CHARGEDAY >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND CHARGEDAY <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND NKNKBN = 1")
            Dim dtCHARGE1 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'フロント入金回数
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(",COUNT(*) AS KAISU")
            sql.Append(" FROM NKNTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND STSFLG = '0'")
            sql.Append(" GROUP BY DENDT")
            Dim dtFKaisu As DataTable = iDatabase.ExecuteRead(sql.ToString())
            'フロント入金金額情報
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(",SUM(NKNKN) AS GOKEIKIN")
            sql.Append(" FROM NKNTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND STSFLG = '0'")
            sql.Append(" GROUP BY DENDT")
            Dim dtFGokeiKin As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'カード発行料(フロント)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.UDNDT")
            sql.Append(",COUNT(A.*) AS HAKKENSU")
            sql.Append(",CASE WHEN SUM(A.UDNKN) IS NULL THEN 0 ELSE SUM(A.UDNKN) END AS HAKKENKIN")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '001'")
            sql.Append(" AND A.SMADT = '999'")
            '営業日付
            sql.Append(" AND A.UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND A.UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" GROUP BY A.UDNDT")
            Dim dtHAKKO As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim strYM As String = Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c)
            Dim intHakko As Integer = 0
            Dim intFKaisu As Integer = 0
            Dim intFGokeiKin As Integer = 0

            '曜日・天気・気温
            For i As Integer = 1 To 31
                '入金機情報
                drCHARGE0 = dtCHARGE0.Select("CHARGEDAY = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '簡易型入金機情報
                drCHARGE1 = dtCHARGE1.Select("CHARGEDAY = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'フロント入金回数情報
                drFKaisu = dtFKaisu.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'フロント入金金額情報
                drFGokeiKin = dtFGokeiKin.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'カード発行料(フロント)
                drHAKKO = dtHAKKO.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

                intHakko = 0
                intFKaisu = 0
                intFGokeiKin = 0
                If drCHARGE0.Length > 0 Then
                    intHakko += CType(drCHARGE0(0).Item("HAKKOKAISU").ToString, Integer)
                    intFKaisu += CType(drCHARGE0(0).Item("SHITEIKAISU").ToString, Integer)
                    intFGokeiKin += CType(drCHARGE0(0).Item("SHITEIGOKEIKIN").ToString, Integer)
                    sheet.Cells(9 + i, 14) = drCHARGE0(0).Item("CHARGE1KAISU").ToString
                    sheet.Cells(9 + i, 16) = drCHARGE0(0).Item("CHARGE1GOKEIKIN").ToString
                    sheet.Cells(9 + i, 18) = drCHARGE0(0).Item("CHARGE2KAISU").ToString
                    sheet.Cells(9 + i, 20) = drCHARGE0(0).Item("CHARGE2GOKEIKIN").ToString
                    sheet.Cells(9 + i, 22) = drCHARGE0(0).Item("CHARGE3KAISU").ToString
                    sheet.Cells(9 + i, 24) = drCHARGE0(0).Item("CHARGE3GOKEIKIN").ToString
                    sheet.Cells(9 + i, 26) = drCHARGE0(0).Item("CHARGE4KAISU").ToString
                    sheet.Cells(9 + i, 28) = drCHARGE0(0).Item("CHARGE4GOKEIKIN").ToString
                    sheet.Cells(9 + i, 30) = drCHARGE0(0).Item("CHARGE5KAISU").ToString
                    sheet.Cells(9 + i, 32) = drCHARGE0(0).Item("CHARGE5GOKEIKIN").ToString
                    sheet.Cells(9 + i, 34) = drCHARGE0(0).Item("CHARGE6KAISU").ToString
                    sheet.Cells(9 + i, 36) = drCHARGE0(0).Item("CHARGE6GOKEIKIN").ToString
                End If
                If drCHARGE1.Length > 0 Then
                    intHakko += CType(drCHARGE1(0).Item("HAKKOKAISU").ToString, Integer)
                    sheet.Cells(9 + i, 38) = drCHARGE1(0).Item("CHARGE1KAISU").ToString
                    sheet.Cells(9 + i, 40) = drCHARGE1(0).Item("CHARGE1GOKEIKIN").ToString
                End If
                If drFKaisu.Length > 0 Then
                    intFKaisu += CType(drFKaisu(0).Item("KAISU").ToString, Integer)
                End If
                If drFGokeiKin.Length > 0 Then
                    intFGokeiKin += CType(drFGokeiKin(0).Item("GOKEIKIN").ToString, Integer)
                End If
                sheet.Cells(9 + i, 42) = intFKaisu
                sheet.Cells(9 + i, 44) = intFGokeiKin
                If drHAKKO.Length > 0 Then
                    intHakko += CType(drHAKKO(0).Item("HAKKENSU").ToString, Integer)
                End If
                sheet.Cells(9 + i, 12) = intHakko

                If i.Equals(1) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI1.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI1.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION1.Text
                End If

                If i.Equals(2) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI2.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI2.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION2.Text
                End If

                If i.Equals(3) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI3.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI3.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION3.Text
                End If

                If i.Equals(4) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI4.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI4.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION4.Text
                End If

                If i.Equals(5) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI5.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI5.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION5.Text
                End If

                If i.Equals(6) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI6.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI6.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION6.Text
                End If

                If i.Equals(7) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI7.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI7.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION7.Text
                End If

                If i.Equals(8) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI8.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI8.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION8.Text
                End If

                If i.Equals(9) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI9.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI9.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION9.Text
                End If

                If i.Equals(10) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI10.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI10.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION10.Text
                End If

                If i.Equals(11) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI11.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI11.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION11.Text
                End If

                If i.Equals(12) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI12.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI12.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION12.Text
                End If

                If i.Equals(13) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI13.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI13.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION13.Text
                End If

                If i.Equals(14) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI14.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI14.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION14.Text
                End If

                If i.Equals(15) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI15.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI15.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION15.Text
                End If

                If i.Equals(16) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI16.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI16.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION16.Text
                End If

                If i.Equals(17) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI17.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI17.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION17.Text
                End If

                If i.Equals(18) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI18.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI18.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION18.Text
                End If

                If i.Equals(19) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI19.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI19.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION19.Text
                End If

                If i.Equals(20) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI20.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI20.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION20.Text
                End If

                If i.Equals(21) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI21.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI21.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION21.Text
                End If

                If i.Equals(22) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI22.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI22.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION22.Text
                End If

                If i.Equals(23) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI23.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI23.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION23.Text
                End If

                If i.Equals(24) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI24.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI24.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION24.Text
                End If

                If i.Equals(25) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI25.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI25.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION25.Text
                End If

                If i.Equals(26) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI26.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI26.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION26.Text
                End If

                If i.Equals(27) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI27.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI27.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION27.Text
                End If

                If i.Equals(28) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI28.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI28.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION28.Text
                End If

                If i.Equals(29) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI29.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI29.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION29.Text
                End If

                If i.Equals(30) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI30.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI30.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION30.Text
                End If

                If i.Equals(31) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI31.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI31.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION31.Text
                End If
            Next

            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 雑収入集計表出力
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print3() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim drHIN1() As DataRow
        Dim drHIN2() As DataRow
        Dim drHIN3() As DataRow
        Dim drHIN4() As DataRow
        Dim drHIN5() As DataRow
        Dim drHIN6() As DataRow
        Dim drHIN7() As DataRow
        Dim drHIN8() As DataRow
        Dim drHIN9() As DataRow
        Dim drHIN10() As DataRow
        Dim drHIN11() As DataRow
        Dim drHIN12() As DataRow
        Dim drHAKKO() As DataRow

        Try

            Dim strReportName As String = "雑収入集計表"
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

            '出力日
            sheet.Cells(2, 57) = Now.ToString("yyyy/MM/dd HH:mm")
            '対象月
            sheet.Cells(5, 2) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分"

            '商品1
            sheet.Cells(8, 12) = Me.lblHINCD1.Text
            '商品2
            sheet.Cells(8, 16) = Me.lblHINCD2.Text
            '商品3
            sheet.Cells(8, 20) = Me.lblHINCD3.Text
            '商品4
            sheet.Cells(8, 24) = Me.cmbHINCD4.Text
            '商品5
            sheet.Cells(8, 28) = Me.cmbHINCD5.Text
            '商品6
            sheet.Cells(8, 32) = Me.cmbHINCD6.Text
            '商品7
            sheet.Cells(8, 36) = Me.cmbHINCD7.Text
            '商品8
            sheet.Cells(8, 40) = Me.cmbHINCD8.Text
            '商品9
            sheet.Cells(8, 44) = Me.cmbHINCD9.Text
            '商品10
            sheet.Cells(8, 48) = Me.cmbHINCD10.Text
            '商品11
            sheet.Cells(8, 52) = Me.cmbHINCD11.Text

            '保険料
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(",COUNT(*) AS SURYO")
            sql.Append(",SUM(HOKENKIN) AS UDNBKN")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND HOKENKIN <> 0")
            sql.Append(" GROUP BY DENDT")
            Dim dtHIN1 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'カート料
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(",COUNT(*) AS SURYO")
            sql.Append(",SUM(CARTKIN) AS UDNBKN")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND CARTKIN <> 0")
            sql.Append(" GROUP BY DENDT")
            Dim dtHIN2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'コンペ料
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(",COUNT(*) AS SURYO")
            sql.Append(",SUM(COMPEKIN) AS UDNBKN")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND COMPEKIN <> 0")
            sql.Append(" GROUP BY DENDT")
            Dim dtHIN3 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '商品4
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD4.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD4.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD4.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN4 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品5
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD5.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD5.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD5.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN5 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品6
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD6.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD6.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD6.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN6 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品7
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD7.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD7.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD7.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN7 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品8
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD8.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD8.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD8.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN8 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品9
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD9.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD9.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD9.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN9 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品10
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD10.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD10.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD10.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN10 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            '商品11
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            If Me.cmbHINCD11.SelectedIndex >= 0 Then
                sql.Append(" AND BUNCDB = '" & Me.cmbHINCD11.SelectedValue.ToString.Substring(0, 3) & "'")
                sql.Append(" AND TKTKBN = '" & Me.cmbHINCD11.SelectedValue.ToString.Substring(3, 3) & "'")
            Else
                sql.Append(" AND BUNCDB = '000'")
                sql.Append(" AND TKTKBN = '000'")
            End If
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN11 As DataTable = iDatabase.ExecuteRead(sql.ToString())
            'その他
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" UDNDT")
            sql.Append(",SUM(TKTSU) AS SURYO")
            sql.Append(",SUM(UDNBKN) AS UDNBKN")
            sql.Append(" FROM ")
            sql.Append(" DENTRA")
            sql.Append(" WHERE")
            sql.Append(" UDNDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND UDNDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND BMNCD = '002' AND BUNCDA = '001'")
            sql.Append(" AND (BUNCDB IN ('005','006') OR (BUNCDB = '007' AND TKTKBN = '002') OR (BUNCDC = '999'))")
            sql.Append(" GROUP BY UDNDT")
            Dim dtHIN12 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'カード発行料(フロント)
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append("A.DENDT")
            sql.Append(",SUM(A.PRERT) AS UDNBKN")
            sql.Append(" FROM NKNTRN AS A")
            sql.Append(" WHERE")
            sql.Append(" A.DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND A.DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND A.DATKB = '1'")
            sql.Append(" AND A.PRERT > 0")
            sql.Append(" GROUP BY DENDT")
            Dim dtHAKKO As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim strYM As String = Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c)

            Dim HINCD12 As Integer = 0

            '曜日・天気・気温
            For i As Integer = 1 To 31
                '保険
                drHIN1 = dtHIN1.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'カート
                drHIN2 = dtHIN2.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'コンペ
                drHIN3 = dtHIN3.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品4
                drHIN4 = dtHIN4.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品5
                drHIN5 = dtHIN5.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品6
                drHIN6 = dtHIN6.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品7
                drHIN7 = dtHIN7.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品8
                drHIN8 = dtHIN8.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品9
                drHIN9 = dtHIN9.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品10
                drHIN10 = dtHIN10.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品11
                drHIN11 = dtHIN11.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                '商品12
                drHIN12 = dtHIN12.Select("UDNDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'カード発行料(入金機)
                drHAKKO = dtHAKKO.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

                HINCD12 = 0
                If drHIN1.Length > 0 Then
                    sheet.Cells(10 + i, 12) = drHIN1(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 14) = drHIN1(0).Item("UDNBKN").ToString
                End If
                If drHIN2.Length > 0 Then
                    sheet.Cells(10 + i, 16) = drHIN2(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 18) = drHIN2(0).Item("UDNBKN").ToString
                End If
                If drHIN3.Length > 0 Then
                    sheet.Cells(10 + i, 20) = drHIN3(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 22) = drHIN3(0).Item("UDNBKN").ToString
                End If
                If drHIN4.Length > 0 Then
                    sheet.Cells(10 + i, 24) = drHIN4(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 26) = drHIN4(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN4(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN5.Length > 0 Then
                    sheet.Cells(10 + i, 28) = drHIN5(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 30) = drHIN5(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN5(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN6.Length > 0 Then
                    sheet.Cells(10 + i, 32) = drHIN6(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 34) = drHIN6(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN6(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN7.Length > 0 Then
                    sheet.Cells(10 + i, 36) = drHIN7(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 38) = drHIN7(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN7(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN8.Length > 0 Then
                    sheet.Cells(10 + i, 40) = drHIN8(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 42) = drHIN8(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN8(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN9.Length > 0 Then
                    sheet.Cells(10 + i, 44) = drHIN9(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 46) = drHIN9(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN9(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN10.Length > 0 Then
                    sheet.Cells(10 + i, 48) = drHIN10(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 50) = drHIN10(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN10(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN11.Length > 0 Then
                    sheet.Cells(10 + i, 52) = drHIN11(0).Item("SURYO").ToString
                    sheet.Cells(10 + i, 54) = drHIN11(0).Item("UDNBKN").ToString
                    HINCD12 -= CType(drHIN11(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHIN12.Length > 0 Then
                    HINCD12 += CType(drHIN12(0).Item("UDNBKN").ToString, Integer)
                End If
                If drHAKKO.Length > 0 Then
                    HINCD12 += CType(drHAKKO(0).Item("UDNBKN").ToString, Integer)
                End If
                If HINCD12 < 0 Then HINCD12 = 0
                sheet.Cells(10 + i, 56) = HINCD12


                If i.Equals(1) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI1.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI1.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION1.Text
                End If

                If i.Equals(2) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI2.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI2.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION2.Text
                End If

                If i.Equals(3) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI3.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI3.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION3.Text
                End If

                If i.Equals(4) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI4.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI4.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION4.Text
                End If

                If i.Equals(5) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI5.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI5.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION5.Text
                End If

                If i.Equals(6) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI6.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI6.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION6.Text
                End If

                If i.Equals(7) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI7.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI7.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION7.Text
                End If

                If i.Equals(8) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI8.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI8.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION8.Text
                End If

                If i.Equals(9) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI9.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI9.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION9.Text
                End If

                If i.Equals(10) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI10.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI10.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION10.Text
                End If

                If i.Equals(11) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI11.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI11.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION11.Text
                End If

                If i.Equals(12) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI12.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI12.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION12.Text
                End If

                If i.Equals(13) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI13.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI13.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION13.Text
                End If

                If i.Equals(14) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI14.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI14.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION14.Text
                End If

                If i.Equals(15) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI15.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI15.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION15.Text
                End If

                If i.Equals(16) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI16.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI16.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION16.Text
                End If

                If i.Equals(17) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI17.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI17.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION17.Text
                End If

                If i.Equals(18) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI18.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI18.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION18.Text
                End If

                If i.Equals(19) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI19.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI19.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION19.Text
                End If

                If i.Equals(20) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI20.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI20.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION20.Text
                End If

                If i.Equals(21) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI21.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI21.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION21.Text
                End If

                If i.Equals(22) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI22.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI22.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION22.Text
                End If

                If i.Equals(23) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI23.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI23.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION23.Text
                End If

                If i.Equals(24) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI24.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI24.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION24.Text
                End If

                If i.Equals(25) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI25.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI25.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION25.Text
                End If

                If i.Equals(26) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI26.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI26.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION26.Text
                End If

                If i.Equals(27) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI27.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI27.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION27.Text
                End If

                If i.Equals(28) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI28.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI28.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION28.Text
                End If

                If i.Equals(29) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI29.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI29.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION29.Text
                End If

                If i.Equals(30) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI30.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI30.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION30.Text
                End If

                If i.Equals(31) Then
                    sheet.Cells(10 + i, 4) = Me.lblYOUBI31.Text
                    sheet.Cells(10 + i, 6) = Me.cmbTENKI31.Text
                    sheet.Cells(10 + i, 9) = Me.txtKION31.Text
                End If
            Next


            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' グリーンフィー集計表出力
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print4() As Boolean
        Dim sql As New System.Text.StringBuilder

        Dim drKOSUTRN() As DataRow
        Dim drHOKEN() As DataRow
        Dim drCART() As DataRow
        Dim drCOMPE() As DataRow

        Try

            Dim strReportName As String = "グリーンフィー集計表"
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

            '出力日
            sheet.Cells(2, 53) = Now.ToString("yyyy/MM/dd HH:mm")
            '対象月
            sheet.Cells(5, 2) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分"
            '４ページ目表題
            sheet.Cells(5, 186) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分 グリーンフィー集計表(合計)"

            'コース売上情報
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" DENDT,NKBNO,PRCKB,HOLESU")
            sql.Append(",COUNT(*) AS CNT")
            sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE")
            sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" GROUP BY DENDT,NKBNO,PRCKB,HOLESU")
            Dim dtKOSUTRN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ''保険料
            'sql.Clear()
            'sql.Append(" SELECT ")
            'sql.Append(" DENDT")
            'sql.Append(",COUNT(*) AS CNT")
            'sql.Append(",SUM(HOKENKIN) AS KINGAKU")
            'sql.Append(" FROM KOSUTRN")
            'sql.Append(" WHERE")
            'sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            'sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            'sql.Append(" AND DATKB = '1'")
            'sql.Append(" AND HOKENKIN <> 0")
            'sql.Append(" GROUP BY DENDT")
            'Dim dtHOKEN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ''カート料
            'sql.Clear()
            'sql.Append(" SELECT ")
            'sql.Append(" DENDT")
            'sql.Append(",COUNT(*) AS CNT")
            'sql.Append(",SUM(CARTKIN) AS KINGAKU")
            'sql.Append(" FROM KOSUTRN")
            'sql.Append(" WHERE")
            'sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            'sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            'sql.Append(" AND DATKB = '1'")
            'sql.Append(" AND CARTKIN <> 0")
            'sql.Append(" GROUP BY DENDT")
            'Dim dtCART As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ''コンペ料
            'sql.Clear()
            'sql.Append(" SELECT ")
            'sql.Append(" DENDT")
            'sql.Append(",COUNT(*) AS CNT")
            'sql.Append(",SUM(COMPEKIN) AS KINGAKU")
            'sql.Append(" FROM KOSUTRN")
            'sql.Append(" WHERE")
            'sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            'sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            'sql.Append(" AND DATKB = '1'")
            'sql.Append(" AND COMPEKIN <> 0")
            'sql.Append(" GROUP BY DENDT")
            'Dim dtCOMPE As DataTable = iDatabase.ExecuteRead(sql.ToString())


            Dim strYM As String = Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c)

            '曜日・天気・気温
            For i As Integer = 1 To 31
                '【ハーフ】正会員
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 1 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 10) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 12) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】非会員
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 1 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 14) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 16) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】正会員(シニア)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 2 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 18) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 20) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】非会員(シニア)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 2 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 22) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 24) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】正会員(早朝・薄暮)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 3 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 26) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 28) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】非会員(早朝・薄暮)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 3 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 30) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 32) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】ジュニア
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 3 AND PRCKB = 1 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 34) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 36) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】正会員(U40)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 5 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 38) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 40) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】非会員(U40)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 5 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 42) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 44) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】正会員(誕生月)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 4 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 46) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 48) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ハーフ】非会員(誕生月)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 4 AND HOLESU = 9")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 50) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 52) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If

                '【ラウンドパック】正会員
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 1 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 66) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 68) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】非会員
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 1 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 70) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 72) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】正会員(シニア)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 2 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 74) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 76) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】非会員(シニア)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 2 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 78) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 80) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】正会員(早朝・薄暮)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 3 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 82) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 84) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】非会員(早朝・薄暮)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 3 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 86) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 88) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】ジュニア
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 3 AND PRCKB = 1 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 90) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 92) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】正会員(U40)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 5 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 94) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 96) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】非会員(U40)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 5 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 98) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 100) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】正会員(誕生月)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 4 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 102) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 104) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【ラウンドパック】非会員(誕生月)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 4 AND HOLESU = 18")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 106) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 108) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If

                '【廻放題】正会員
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 1 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 127) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 129) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】非会員
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 1 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 131) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 133) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】正会員(シニア)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 2 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 135) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 137) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】非会員(シニア)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 2 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 139) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 141) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】正会員(早朝・薄暮)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 3 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 143) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 145) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】非会員(早朝・薄暮)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 3 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 147) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 149) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】ジュニア
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 3 AND PRCKB = 1 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 151) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 153) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】正会員(U40)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 5 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 155) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 157) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】非会員(U40)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 5 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 159) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 161) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】正会員(誕生月)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 1 AND PRCKB = 4 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 163) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 165) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                '【廻放題】非会員(誕生月)
                drKOSUTRN = dtKOSUTRN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND NKBNO = 2 AND PRCKB = 4 AND HOLESU = 0")
                If drKOSUTRN.Length > 0 Then
                    sheet.Cells(11 + i, 167) = drKOSUTRN(0).Item("CNT").ToString
                    sheet.Cells(11 + i, 169) = drKOSUTRN(0).Item("KINGAKU").ToString
                End If
                ''【保険】
                'drHOKEN = dtHOKEN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'If drHOKEN.Length > 0 Then
                '    sheet.Cells(11 + i, 234) = drHOKEN(0).Item("CNT").ToString
                '    sheet.Cells(11 + i, 236) = drHOKEN(0).Item("KINGAKU").ToString
                'End If
                ''【カート】
                'drCART = dtCART.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'If drCART.Length > 0 Then
                '    sheet.Cells(11 + i, 238) = drCART(0).Item("CNT").ToString
                '    sheet.Cells(11 + i, 240) = drCART(0).Item("KINGAKU").ToString
                'End If
                ''【コンペ】
                'drCOMPE = dtCOMPE.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'If drCOMPE.Length > 0 Then
                '    sheet.Cells(11 + i, 242) = drCOMPE(0).Item("CNT").ToString
                '    sheet.Cells(11 + i, 244) = drCOMPE(0).Item("KINGAKU").ToString
                'End If

                If i.Equals(1) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI1.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI1.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION1.Text
                End If

                If i.Equals(2) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI2.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI2.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION2.Text
                End If

                If i.Equals(3) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI3.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI3.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION3.Text
                End If

                If i.Equals(4) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI4.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI4.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION4.Text
                End If

                If i.Equals(5) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI5.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI5.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION5.Text
                End If

                If i.Equals(6) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI6.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI6.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION6.Text
                End If

                If i.Equals(7) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI7.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI7.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION7.Text
                End If

                If i.Equals(8) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI8.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI8.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION8.Text
                End If

                If i.Equals(9) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI9.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI9.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION9.Text
                End If

                If i.Equals(10) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI10.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI10.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION10.Text
                End If

                If i.Equals(11) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI11.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI11.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION11.Text
                End If

                If i.Equals(12) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI12.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI12.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION12.Text
                End If

                If i.Equals(13) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI13.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI13.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION13.Text
                End If

                If i.Equals(14) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI14.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI14.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION14.Text
                End If

                If i.Equals(15) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI15.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI15.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION15.Text
                End If

                If i.Equals(16) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI16.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI16.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION16.Text
                End If

                If i.Equals(17) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI17.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI17.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION17.Text
                End If

                If i.Equals(18) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI18.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI18.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION18.Text
                End If

                If i.Equals(19) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI19.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI19.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION19.Text
                End If

                If i.Equals(20) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI20.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI20.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION20.Text
                End If

                If i.Equals(21) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI21.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI21.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION21.Text
                End If

                If i.Equals(22) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI22.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI22.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION22.Text
                End If

                If i.Equals(23) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI23.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI23.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION23.Text
                End If

                If i.Equals(24) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI24.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI24.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION24.Text
                End If

                If i.Equals(25) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI25.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI25.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION25.Text
                End If

                If i.Equals(26) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI26.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI26.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION26.Text
                End If

                If i.Equals(27) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI27.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI27.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION27.Text
                End If

                If i.Equals(28) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI28.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI28.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION28.Text
                End If

                If i.Equals(29) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI29.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI29.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION29.Text
                End If

                If i.Equals(30) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI30.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI30.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION30.Text
                End If

                If i.Equals(31) Then
                    sheet.Cells(11 + i, 4) = Me.lblYOUBI31.Text
                    sheet.Cells(11 + i, 6) = Me.cmbTENKI31.Text
                    sheet.Cells(11 + i, 8) = Me.txtKION31.Text
                End If
            Next


            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' ''' <summary>
    ' ''' グリーンフィー集計表出力
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function Print4() As Boolean
    '    Dim sql As New System.Text.StringBuilder
    '    Dim intTanka_MEMBER As Integer = 1580
    '    Dim intTanka_VISITOR As Integer = 1980
    '    Dim intTanka_SINIOR_M As Integer = 1080
    '    Dim intTanka_SINIOR_V As Integer = 1480
    '    Dim intTanka_JUNIOR As Integer = 530
    '    Dim intTanka_MORNING_M As Integer = 1080
    '    Dim intTanka_MORNING_V As Integer = 1480
    '    Dim intTanka_U40 As Integer = 500
    '    Dim intTanka_BT As Integer = 500

    '    Dim intMEMBERSU As Integer = 0
    '    Dim intVISITORSU As Integer = 0
    '    Dim intSINIORSU_M As Integer = 0
    '    Dim intSINIORSU_V As Integer = 0
    '    Dim intMORNINGSU_M As Integer = 0
    '    Dim intMORNINGSU_V As Integer = 0

    '    Dim drMEMBER() As DataRow
    '    Dim drVISITOR() As DataRow
    '    Dim drSINIOR_M() As DataRow
    '    Dim drSINIOR_V() As DataRow
    '    Dim drJUNIOR() As DataRow

    '    Dim drMORNING9_M() As DataRow
    '    Dim drMORNING9_V() As DataRow

    '    Dim drR_MEMBER() As DataRow
    '    Dim drR_VISITOR() As DataRow
    '    Dim drR_SINIOR_M() As DataRow
    '    Dim drR_SINIOR_V() As DataRow
    '    Dim drR_MORNING_M() As DataRow
    '    Dim drR_MORNING_V() As DataRow
    '    Dim drR_JUNIOR() As DataRow

    '    Dim drU40_MEMBER() As DataRow
    '    Dim drU40_VISITOR() As DataRow

    '    Dim drBT_MEMBER() As DataRow
    '    Dim drBT_VISITOR() As DataRow

    '    Dim drHOKEN() As DataRow
    '    Dim drCART() As DataRow
    '    Dim drCOMPE() As DataRow

    '    Try

    '        Dim strReportName As String = "グリーンフィー集計表"
    '        Dim strSaveReportName As String = UIUtility.FILE_PATH.REPORT & "\" & strReportName _
    '                                         & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString _
    '                                         & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".xls"
    '        'エクセル処理関連
    '        Dim app As Excel.Application
    '        Dim book As Excel.Workbook
    '        Dim sheet As Excel.Worksheet
    '        Dim border As Excel.Border = Nothing
    '        Dim xlrange As Excel.Range
    '        Dim xlpasterange As Excel.Range
    '        Dim break As Excel.HPageBreak

    '        app = CType(CreateObject("Excel.Application"), Excel.Application)
    '        app.Visible = False
    '        book = app.Workbooks.Open(UIUtility.FILE_PATH.TEMPLATE & "\" & strReportName)

    '        sheet = CType(book.Worksheets(1), Excel.Worksheet)

    '        '出力日
    '        sheet.Cells(2, 57) = Now.ToString("yyyy/MM/dd HH:mm")
    '        '対象月
    '        sheet.Cells(5, 2) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分"

    '        '正会員人数
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB <> 2")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtMEMBER As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '非会員人数
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB <> 2")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtVISITOR As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '会員シニア人数
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB = 2")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtSINIOR_M As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '非会員シニア人数
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB = 2")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtSINIOR_V As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ジュニア会員
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 3")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtJUNIOR As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '早朝・薄暮正会員
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB = 3")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtMORNING9_M As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '早朝・薄暮非会員
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB = 3")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtMORNING9_V As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(正会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB IN (1,4,5) AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_MEMBER As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(非会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB IN (1,4,5) AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_VISITOR As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(シニア正会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB = 2 AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_SINIOR_M As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(シニア非会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB = 2 AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_SINIOR_V As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(ジュニア会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 3 AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_JUNIOR As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(早朝・薄暮 正会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB = 3 AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_MORNING_M As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'ラウンドパック(早朝・薄暮 非会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM((UDNBKN - (HOKENKIN + CARTKIN + COMPEKIN))) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB = 3 AND HOLESU <> 9")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtR_MORNING_V As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'U40(正会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB = 5")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtU40_MEMBER As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'U40(非正会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB = 5")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtU40_VISITOR As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '誕生月(正会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 1 AND PRCKB = 4")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtBT_MEMBER As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '誕生月(非会員)
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND NKBNO = 2 AND PRCKB = 4")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtBT_VISITOR As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        '保険料
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM(HOKENKIN) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND HOKENKIN <> 0")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtHOKEN As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'カート料
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM(CARTKIN) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND CARTKIN <> 0")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtCART As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '        'コンペ料
    '        sql.Clear()
    '        sql.Append(" SELECT ")
    '        sql.Append(" DENDT")
    '        sql.Append(",COUNT(*) AS CNT")
    '        sql.Append(",SUM(COMPEKIN) AS KINGAKU")
    '        sql.Append(" FROM KOSUTRN")
    '        sql.Append(" WHERE")
    '        sql.Append(" DENDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
    '        sql.Append(" AND DENDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
    '        sql.Append(" AND DATKB = '1'")
    '        sql.Append(" AND COMPEKIN <> 0")
    '        sql.Append(" GROUP BY DENDT")
    '        Dim dtCOMPE As DataTable = iDatabase.ExecuteRead(sql.ToString())


    '        Dim strYM As String = Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c)

    '        '曜日・天気・気温
    '        For i As Integer = 1 To 31
    '            sql.Clear()
    '            sql.Append(" SELECT ")
    '            sql.Append(" *")
    '            sql.Append(" FROM CALMTA")
    '            sql.Append(" WHERE")
    '            sql.Append(" CALDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            Dim dtCALMTA As DataTable = iDatabase.ExecuteRead(sql.ToString())

    '            If dtCALMTA.Rows.Count > 0 Then
    '                If dtCALMTA.Rows(0).Item("RKNKB").ToString.Equals("1") Then
    '                    '平日
    '                    intTanka_MEMBER = _intTanka1_MEMBER
    '                    intTanka_VISITOR = _intTanka1_VISITOR
    '                    intTanka_SINIOR_M = _intTanka1_SINIOR_M
    '                    intTanka_SINIOR_V = _intTanka1_SINIOR_V
    '                    intTanka_JUNIOR = _intTanka1_JUNIOR
    '                    intTanka_MORNING_M = _intTanka1_MORNING_M
    '                    intTanka_MORNING_V = _intTanka1_MORNING_V
    '                    intTanka_U40 = _intTanka1_U40
    '                    intTanka_BT = _intTanka1_BT
    '                Else
    '                    '休日
    '                    intTanka_MEMBER = _intTanka2_MEMBER
    '                    intTanka_VISITOR = _intTanka2_VISITOR
    '                    intTanka_SINIOR_M = _intTanka2_SINIOR_M
    '                    intTanka_SINIOR_V = _intTanka2_SINIOR_V
    '                    intTanka_JUNIOR = _intTanka2_JUNIOR
    '                    intTanka_MORNING_M = _intTanka2_MORNING_M
    '                    intTanka_MORNING_V = _intTanka2_MORNING_V
    '                    intTanka_U40 = _intTanka2_U40
    '                    intTanka_BT = _intTanka2_BT
    '                End If
    '            End If



    '            intMEMBERSU = 0
    '            intVISITORSU = 0
    '            intSINIORSU_M = 0
    '            intSINIORSU_V = 0
    '            intMORNINGSU_M = 0
    '            intMORNINGSU_V = 0

    '            '正会員人数
    '            drMEMBER = dtMEMBER.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            '非会員人数
    '            drVISITOR = dtVISITOR.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            '会員ｼﾆｱ人数
    '            drSINIOR_M = dtSINIOR_M.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            '非会員ｼﾆｱ人数
    '            drSINIOR_V = dtSINIOR_V.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'ジュニア人数
    '            drJUNIOR = dtJUNIOR.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            '早朝・薄暮(正会員)
    '            drMORNING9_M = dtMORNING9_M.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            '早朝・薄暮(非会員)
    '            drMORNING9_V = dtMORNING9_V.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            'ラウンドパック(正会員)
    '            drR_MEMBER = dtR_MEMBER.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'ラウンドパック(非会員)
    '            drR_VISITOR = dtR_VISITOR.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'ラウンドパック(シニア正会員)
    '            drR_SINIOR_M = dtR_SINIOR_M.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'ラウンドパック(シニア非会員)
    '            drR_SINIOR_V = dtR_SINIOR_V.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            'ラウンドパック(ジュニア会員)
    '            drR_JUNIOR = dtR_JUNIOR.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            'ラウンドパック(早朝・薄暮 正会員)
    '            drR_MORNING_M = dtR_MORNING_M.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'ラウンドパック(早朝・薄暮 非会員)
    '            drR_MORNING_V = dtR_MORNING_V.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            'U40(正会員)
    '            drU40_MEMBER = dtU40_MEMBER.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'U40(非会員)
    '            drU40_VISITOR = dtU40_VISITOR.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            '誕生月(正会員)
    '            drBT_MEMBER = dtBT_MEMBER.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            '誕生月(非会員)
    '            drBT_VISITOR = dtBT_VISITOR.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            '保険料
    '            drHOKEN = dtHOKEN.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'カート料
    '            drCART = dtCART.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
    '            'コンペ料
    '            drCOMPE = dtCOMPE.Select("DENDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

    '            If drMEMBER.Length > 0 Then
    '                '会員人数
    '                sheet.Cells(10 + i, 12) = drMEMBER(0).Item("CNT").ToString
    '                intMEMBERSU += CType(drMEMBER(0).Item("CNT"), Integer)
    '            End If
    '            If drVISITOR.Length > 0 Then
    '                '非会員人数
    '                sheet.Cells(10 + i, 19) = drVISITOR(0).Item("CNT").ToString
    '                intVISITORSU += CType(drVISITOR(0).Item("CNT"), Integer)
    '            End If
    '            If drSINIOR_M.Length > 0 Then
    '                '会員ｼﾆｱ
    '                sheet.Cells(10 + i, 26) = drSINIOR_M(0).Item("CNT").ToString

    '                intSINIORSU_M += CType(drSINIOR_M(0).Item("CNT"), Integer)
    '            End If
    '            If drSINIOR_V.Length > 0 Then
    '                '非会員ｼﾆｱ
    '                sheet.Cells(10 + i, 33) = drSINIOR_V(0).Item("CNT").ToString

    '                intSINIORSU_V += CType(drSINIOR_V(0).Item("CNT"), Integer)
    '            End If
    '            If drJUNIOR.Length > 0 Then
    '                'ジュニア会員
    '                sheet.Cells(10 + i, 40) = drJUNIOR(0).Item("CNT").ToString
    '                '回数
    '                sheet.Cells(10 + i, 42) = CType(drJUNIOR(0).Item("CNT").ToString, Integer) + CType(drR_JUNIOR(0).Item("CNT").ToString, Integer)
    '                '金額
    '                sheet.Cells(10 + i, 44) = (CType(drJUNIOR(0).Item("CNT").ToString, Integer) * intTanka_JUNIOR) - (CType(drR_JUNIOR(0).Item("CNT").ToString, Integer) * intTanka_JUNIOR)
    '            End If

    '            If drMORNING9_M.Length > 0 Then
    '                '早朝・薄暮(正会員)
    '                sheet.Cells(10 + i, 47) = drMORNING9_M(0).Item("CNT").ToString
    '                intMEMBERSU += CType(drMORNING9_M(0).Item("CNT"), Integer)
    '                intMORNINGSU_M += CType(drMORNING9_M(0).Item("CNT"), Integer)
    '            End If
    '            If drMORNING9_V.Length > 0 Then
    '                '早朝・薄暮(非会員)
    '                sheet.Cells(10 + i, 54) = drMORNING9_V(0).Item("CNT").ToString
    '                intVISITORSU += CType(drMORNING9_V(0).Item("CNT"), Integer)
    '                intMORNINGSU_V += CType(drMORNING9_V(0).Item("CNT"), Integer)
    '            End If

    '            If drR_MEMBER.Length > 0 Then
    '                'ラウンドパック(正会員)
    '                sheet.Cells(10 + i, 64) = drR_MEMBER(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 66) = drR_MEMBER(0).Item("KINGAKU").ToString

    '                intMEMBERSU += CType(drR_MEMBER(0).Item("CNT"), Integer)
    '            End If
    '            If drR_VISITOR.Length > 0 Then
    '                'ラウンドパック(非会員)
    '                sheet.Cells(10 + i, 68) = drR_VISITOR(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 70) = drR_VISITOR(0).Item("KINGAKU").ToString

    '                intVISITORSU += CType(drR_VISITOR(0).Item("CNT"), Integer)
    '            End If

    '            If drR_SINIOR_M.Length > 0 Then
    '                'ラウンドパック(シニア正会員)
    '                sheet.Cells(10 + i, 72) = drR_SINIOR_M(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 74) = drR_SINIOR_M(0).Item("KINGAKU").ToString

    '                intSINIORSU_M += CType(drR_SINIOR_M(0).Item("CNT"), Integer)
    '            End If
    '            If drR_SINIOR_V.Length > 0 Then
    '                'ラウンドパック(シニア非会員)
    '                sheet.Cells(10 + i, 76) = drR_SINIOR_V(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 78) = drR_SINIOR_V(0).Item("KINGAKU").ToString

    '                intSINIORSU_V += CType(drR_SINIOR_V(0).Item("CNT"), Integer)
    '            End If

    '            If drR_MORNING_M.Length > 0 Then
    '                'ラウンドパック(早朝・薄暮 正会員)
    '                sheet.Cells(10 + i, 80) = drR_MORNING_M(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 82) = drR_MORNING_M(0).Item("KINGAKU").ToString

    '                intMORNINGSU_M += CType(drR_MORNING_M(0).Item("CNT"), Integer)
    '            End If
    '            If drR_MORNING_V.Length > 0 Then
    '                'ラウンドパック(早朝薄暮 非会員)
    '                sheet.Cells(10 + i, 84) = drR_MORNING_V(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 86) = drR_MORNING_V(0).Item("KINGAKU").ToString

    '                intMORNINGSU_V += CType(drR_MORNING_V(0).Item("CNT"), Integer)
    '            End If

    '            If drU40_MEMBER.Length > 0 Then
    '                'U40(正会員)
    '                sheet.Cells(10 + i, 88) = drU40_MEMBER(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 90) = CType(drU40_MEMBER(0).Item("CNT").ToString, Integer) * intTanka_U40
    '            End If
    '            If drU40_VISITOR.Length > 0 Then
    '                'U40(非会員)
    '                sheet.Cells(10 + i, 92) = drU40_VISITOR(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 94) = CType(drU40_VISITOR(0).Item("CNT").ToString, Integer) * intTanka_U40
    '            End If

    '            If drBT_MEMBER.Length > 0 Then
    '                '誕生月(正会員)
    '                sheet.Cells(10 + i, 96) = drBT_MEMBER(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 98) = CType(drBT_MEMBER(0).Item("CNT").ToString, Integer) * intTanka_BT
    '            End If
    '            If drBT_VISITOR.Length > 0 Then
    '                'U40(非会員)
    '                sheet.Cells(10 + i, 100) = drBT_VISITOR(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 102) = CType(drBT_VISITOR(0).Item("CNT").ToString, Integer) * intTanka_BT
    '            End If

    '            If drHOKEN.Length > 0 Then
    '                '保険料
    '                sheet.Cells(10 + i, 104) = drHOKEN(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 106) = drHOKEN(0).Item("KINGAKU").ToString
    '            End If

    '            If drCART.Length > 0 Then
    '                'カート料
    '                sheet.Cells(10 + i, 108) = drCART(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 110) = drCART(0).Item("KINGAKU").ToString
    '            End If

    '            If drCOMPE.Length > 0 Then
    '                'コンペ料
    '                sheet.Cells(10 + i, 112) = drCOMPE(0).Item("CNT").ToString
    '                sheet.Cells(10 + i, 114) = drCOMPE(0).Item("KINGAKU").ToString
    '            End If

    '            '【回数】

    '            '正会員
    '            If intMEMBERSU > 0 Then
    '                sheet.Cells(10 + i, 14) = intMEMBERSU
    '                sheet.Cells(10 + i, 16) = (CType(drMEMBER(0).Item("CNT"), Integer) * intTanka_MEMBER) - ((intMEMBERSU - CType(drMEMBER(0).Item("CNT"), Integer)) * intTanka_MEMBER)
    '            End If
    '            '非会員
    '            If intVISITORSU > 0 Then
    '                sheet.Cells(10 + i, 21) = intVISITORSU
    '                sheet.Cells(10 + i, 23) = (CType(drVISITOR(0).Item("CNT"), Integer) * intTanka_VISITOR) - ((intVISITORSU - CType(drVISITOR(0).Item("CNT"), Integer)) * intTanka_VISITOR)
    '            End If
    '            '正会員(シニア)
    '            If intSINIORSU_M > 0 Then
    '                sheet.Cells(10 + i, 28) = intSINIORSU_M
    '                sheet.Cells(10 + i, 30) = (CType(drSINIOR_M(0).Item("CNT"), Integer) * intTanka_SINIOR_M) - ((intSINIORSU_M - CType(drSINIOR_M(0).Item("CNT"), Integer)) * intTanka_SINIOR_M)
    '            End If
    '            '非会員(シニア)
    '            If intSINIORSU_V > 0 Then
    '                sheet.Cells(10 + i, 35) = intSINIORSU_V
    '                sheet.Cells(10 + i, 37) = (CType(drSINIOR_V(0).Item("CNT"), Integer) * intTanka_SINIOR_V) - ((intSINIORSU_V - CType(drSINIOR_V(0).Item("CNT"), Integer)) * intTanka_SINIOR_V)
    '            End If
    '            '正会員(早朝・薄暮)
    '            If intMORNINGSU_M > 0 Then
    '                sheet.Cells(10 + i, 49) = intMORNINGSU_M
    '                sheet.Cells(10 + i, 51) = (CType(drMORNING9_M(0).Item("CNT"), Integer) * intTanka_MORNING_M) - ((intMORNINGSU_M - CType(drMORNING9_M(0).Item("CNT"), Integer)) * intTanka_MORNING_M)
    '            End If
    '            '非会員(早朝・薄暮)
    '            If intMORNINGSU_V > 0 Then
    '                sheet.Cells(10 + i, 56) = intMORNINGSU_V
    '                sheet.Cells(10 + i, 58) = (CType(drMORNING9_V(0).Item("CNT"), Integer) * intTanka_MORNING_V) - ((intMORNINGSU_V - CType(drMORNING9_V(0).Item("CNT"), Integer)) * intTanka_MORNING_V)
    '            End If

    '            If i.Equals(1) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI1.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI1.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION1.Text
    '            End If

    '            If i.Equals(2) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI2.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI2.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION2.Text
    '            End If

    '            If i.Equals(3) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI3.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI3.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION3.Text
    '            End If

    '            If i.Equals(4) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI4.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI4.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION4.Text
    '            End If

    '            If i.Equals(5) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI5.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI5.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION5.Text
    '            End If

    '            If i.Equals(6) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI6.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI6.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION6.Text
    '            End If

    '            If i.Equals(7) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI7.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI7.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION7.Text
    '            End If

    '            If i.Equals(8) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI8.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI8.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION8.Text
    '            End If

    '            If i.Equals(9) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI9.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI9.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION9.Text
    '            End If

    '            If i.Equals(10) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI10.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI10.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION10.Text
    '            End If

    '            If i.Equals(11) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI11.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI11.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION11.Text
    '            End If

    '            If i.Equals(12) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI12.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI12.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION12.Text
    '            End If

    '            If i.Equals(13) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI13.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI13.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION13.Text
    '            End If

    '            If i.Equals(14) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI14.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI14.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION14.Text
    '            End If

    '            If i.Equals(15) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI15.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI15.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION15.Text
    '            End If

    '            If i.Equals(16) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI16.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI16.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION16.Text
    '            End If

    '            If i.Equals(17) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI17.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI17.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION17.Text
    '            End If

    '            If i.Equals(18) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI18.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI18.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION18.Text
    '            End If

    '            If i.Equals(19) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI19.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI19.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION19.Text
    '            End If

    '            If i.Equals(20) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI20.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI20.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION20.Text
    '            End If

    '            If i.Equals(21) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI21.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI21.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION21.Text
    '            End If

    '            If i.Equals(22) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI22.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI22.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION22.Text
    '            End If

    '            If i.Equals(23) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI23.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI23.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION23.Text
    '            End If

    '            If i.Equals(24) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI24.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI24.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION24.Text
    '            End If

    '            If i.Equals(25) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI25.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI25.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION25.Text
    '            End If

    '            If i.Equals(26) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI26.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI26.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION26.Text
    '            End If

    '            If i.Equals(27) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI27.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI27.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION27.Text
    '            End If

    '            If i.Equals(28) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI28.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI28.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION28.Text
    '            End If

    '            If i.Equals(29) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI29.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI29.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION29.Text
    '            End If

    '            If i.Equals(30) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI30.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI30.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION30.Text
    '            End If

    '            If i.Equals(31) Then
    '                sheet.Cells(10 + i, 4) = Me.lblYOUBI31.Text
    '                sheet.Cells(10 + i, 6) = Me.cmbTENKI31.Text
    '                sheet.Cells(10 + i, 9) = Me.txtKION31.Text
    '            End If
    '        Next





    '        'ファイル保存
    '        book.SaveAs(strSaveReportName)

    '        book.Close()

    '        System.Diagnostics.Process.Start(strSaveReportName)

    '        Return True

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    ''' <summary>
    ''' 打放し球数集計表出力
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print5() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim drENTSU() As DataRow
        Dim drENTKN() As DataRow
        Dim drTENTSU() As DataRow
        Dim drSEATSMA() As DataRow
        'Dim strENTTRA As String = "(SELECT DISTINCT DATKB,ENTDT,KSBKB,EIGKB FROM ENTTRA) AS A"
        Dim intENTSU As Integer = 0
        Dim intENTKIN As Integer = 0
        Try

            Dim strReportName As String = "打放し球数集計表"
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

            '出力日
            sheet.Cells(2, 45) = Now.ToString("yyyy/MM/dd HH:mm")
            '対象月
            sheet.Cells(5, 2) = Me.txtYear.Text & "年" & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "月分"

            '種別名セット
            sql.Clear()
            sql.Append(" SELECT * FROM KBMAST WHERE NKBNO <= 10 ORDER BY NKBNO")
            Dim dtKBMAST As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To dtKBMAST.Rows.Count - 1
                Select Case dtKBMAST.Rows(i).Item("NKBNO").ToString
                    Case "1" : sheet.Cells(7, 12) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "2" : sheet.Cells(7, 18) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "3" : sheet.Cells(7, 24) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "4" : sheet.Cells(7, 30) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "5" : sheet.Cells(7, 36) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "6" : sheet.Cells(7, 42) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "7" : sheet.Cells(7, 51) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "8" : sheet.Cells(7, 57) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "9" : sheet.Cells(7, 63) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                    Case "10" : sheet.Cells(7, 69) = dtKBMAST.Rows(i).Item("CKBNAME").ToString
                End Select
            Next

            '種別毎の入場者数
            Dim strTB1 As String = String.Empty

            strTB1 &= "(SELECT DATKB,EIGKB,ENTDT,KSBKB,MANNO FROM ENTTRA"
            strTB1 &= " UNION "
            strTB1 &= "SELECT '1' AS DATKB,'1' AS EIGKB,ENTDT,KSBKB,MANNO FROM ENTTRB) AS TB1"
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ENTDT")
            sql.Append(",KSBKB")
            sql.Append(",COUNT(MANNO) AS ENTSU")
            sql.Append(" FROM " & strTB1)
            sql.Append(" WHERE")
            sql.Append(" ENTDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND ENTDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND EIGKB = '1'")
            sql.Append(" GROUP BY ENTDT,KSBKB")

            Dim dtENTSU As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '入場料金
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ENTDT")
            sql.Append(",SUM(ENTKN) AS ENTKN")
            sql.Append(" FROM ")
            sql.Append(" ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" ENTDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND ENTDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND EIGKB = '1'")
            sql.Append(" GROUP BY ENTDT")
            Dim dtENTKN As DataTable = iDatabase.ExecuteRead(sql.ToString())

            '打ち放題入場者数
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" ENTDT")
            sql.Append(",COUNT(*) AS ENTSU")
            sql.Append(",SUM(ENTKN) AS ENTKN")
            sql.Append(" FROM ")
            sql.Append(" ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" ENTDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND ENTDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" AND DATKB = '1'")
            sql.Append(" AND EIGKB = '2'")
            sql.Append(" GROUP BY ENTDT")
            Dim dtTENTSU As DataTable = iDatabase.ExecuteRead(sql.ToString())

            'ボール情報
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" SEATDT")
            sql.Append(",SUM(NKB01KIN) AS NKB01KIN")
            sql.Append(",SUM(NKB02KIN) AS NKB02KIN")
            sql.Append(",SUM(NKB03KIN) AS NKB03KIN")
            sql.Append(",SUM(NKB04KIN) AS NKB04KIN")
            sql.Append(",SUM(NKB05KIN) AS NKB05KIN")
            sql.Append(",SUM(NKB06KIN) AS NKB06KIN")
            sql.Append(",SUM(NKB07KIN) AS NKB07KIN")
            sql.Append(",SUM(NKB08KIN) AS NKB08KIN")
            sql.Append(",SUM(NKB09KIN) AS NKB09KIN")
            sql.Append(",SUM(NKB10KIN) AS NKB10KIN")
            sql.Append(",SUM(NKB12KIN) AS NKB12KIN")
            sql.Append(",SUM(NKB01BALL) AS NKB01BALL")
            sql.Append(",SUM(NKB02BALL) AS NKB02BALL")
            sql.Append(",SUM(NKB03BALL) AS NKB03BALL")
            sql.Append(",SUM(NKB04BALL) AS NKB04BALL")
            sql.Append(",SUM(NKB05BALL) AS NKB05BALL")
            sql.Append(",SUM(NKB06BALL) AS NKB06BALL")
            sql.Append(",SUM(NKB07BALL) AS NKB07BALL")
            sql.Append(",SUM(NKB08BALL) AS NKB08BALL")
            sql.Append(",SUM(NKB09BALL) AS NKB09BALL")
            sql.Append(",SUM(NKB10BALL) AS NKB10BALL")
            sql.Append(",SUM(NKB12BALL) AS NKB12BALL")
            sql.Append(" FROM SEATSMA")
            sql.Append(" WHERE")
            sql.Append(" SEATDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND SEATDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")
            sql.Append(" GROUP BY SEATDT")
            Dim dtSEATSMA As DataTable = iDatabase.ExecuteRead(sql.ToString())



            Dim strYM As String = Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c)

            '曜日・天気・気温
            For i As Integer = 1 To 31
                intENTSU = 0
                intENTKIN = 0

                '打ち放題人数
                drTENTSU = dtTENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                'ボール情報
                drSEATSMA = dtSEATSMA.Select("SEATDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")

                '【種別１】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '1'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 12) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 14) = drSEATSMA(0).Item("NKB01KIN").ToString
                    sheet.Cells(9 + i, 16) = drSEATSMA(0).Item("NKB01BALL").ToString
                End If
                '【種別２】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '2'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 18) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 20) = drSEATSMA(0).Item("NKB02KIN").ToString
                    sheet.Cells(9 + i, 22) = drSEATSMA(0).Item("NKB02BALL").ToString
                End If
                '【種別３】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '3'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 24) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 26) = drSEATSMA(0).Item("NKB03KIN").ToString
                    sheet.Cells(9 + i, 28) = drSEATSMA(0).Item("NKB03BALL").ToString
                End If
                '【種別４】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '4'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 30) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 32) = drSEATSMA(0).Item("NKB04KIN").ToString
                    sheet.Cells(9 + i, 34) = drSEATSMA(0).Item("NKB04BALL").ToString
                End If
                '【種別５】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '5'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 36) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 38) = drSEATSMA(0).Item("NKB05KIN").ToString
                    sheet.Cells(9 + i, 40) = drSEATSMA(0).Item("NKB05BALL").ToString
                End If
                '【種別６】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '6'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 42) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 44) = drSEATSMA(0).Item("NKB06KIN").ToString
                    sheet.Cells(9 + i, 46) = drSEATSMA(0).Item("NKB06BALL").ToString
                End If
                '【種別７】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '7'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 51) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 53) = drSEATSMA(0).Item("NKB07KIN").ToString
                    sheet.Cells(9 + i, 55) = drSEATSMA(0).Item("NKB07BALL").ToString
                End If
                '【種別８】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '8'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 57) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 59) = drSEATSMA(0).Item("NKB08KIN").ToString
                    sheet.Cells(9 + i, 61) = drSEATSMA(0).Item("NKB08BALL").ToString
                End If
                '【種別９】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '9'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 63) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 65) = drSEATSMA(0).Item("NKB09KIN").ToString
                    sheet.Cells(9 + i, 67) = drSEATSMA(0).Item("NKB09BALL").ToString
                End If
                '【種別１０】
                '人数
                drENTSU = dtENTSU.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "' AND KSBKB = '10'")
                If drENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 69) = drENTSU(0).Item("ENTSU").ToString
                    intENTSU += CType(drENTSU(0).Item("ENTSU").ToString, Integer)
                End If
                '金額・球数
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 71) = drSEATSMA(0).Item("NKB10KIN").ToString
                    sheet.Cells(9 + i, 73) = drSEATSMA(0).Item("NKB10BALL").ToString
                End If
                '【打ち放題】
                If drTENTSU.Length > 0 Then
                    sheet.Cells(9 + i, 75) = drTENTSU(0).Item("ENTSU").ToString
                    sheet.Cells(9 + i, 77) = drTENTSU(0).Item("ENTKN").ToString
                End If
                If drSEATSMA.Length > 0 Then
                    sheet.Cells(9 + i, 79) = drSEATSMA(0).Item("NKB12BALL").ToString
                End If
                '【入場料】
                sheet.Cells(9 + i, 81) = intENTSU.ToString

                drENTKN = dtENTKN.Select("ENTDT = '" & strYM & i.ToString.PadLeft(2, "0"c) & "'")
                If drENTKN.Length > 0 Then
                    sheet.Cells(9 + i, 83) = drENTKN(0).Item("ENTKN").ToString
                End If



                If i.Equals(1) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI1.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI1.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION1.Text
                End If

                If i.Equals(2) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI2.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI2.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION2.Text
                End If

                If i.Equals(3) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI3.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI3.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION3.Text
                End If

                If i.Equals(4) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI4.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI4.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION4.Text
                End If

                If i.Equals(5) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI5.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI5.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION5.Text
                End If

                If i.Equals(6) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI6.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI6.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION6.Text
                End If

                If i.Equals(7) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI7.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI7.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION7.Text
                End If

                If i.Equals(8) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI8.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI8.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION8.Text
                End If

                If i.Equals(9) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI9.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI9.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION9.Text
                End If

                If i.Equals(10) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI10.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI10.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION10.Text
                End If

                If i.Equals(11) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI11.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI11.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION11.Text
                End If

                If i.Equals(12) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI12.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI12.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION12.Text
                End If

                If i.Equals(13) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI13.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI13.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION13.Text
                End If

                If i.Equals(14) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI14.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI14.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION14.Text
                End If

                If i.Equals(15) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI15.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI15.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION15.Text
                End If

                If i.Equals(16) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI16.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI16.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION16.Text
                End If

                If i.Equals(17) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI17.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI17.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION17.Text
                End If

                If i.Equals(18) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI18.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI18.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION18.Text
                End If

                If i.Equals(19) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI19.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI19.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION19.Text
                End If

                If i.Equals(20) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI20.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI20.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION20.Text
                End If

                If i.Equals(21) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI21.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI21.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION21.Text
                End If

                If i.Equals(22) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI22.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI22.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION22.Text
                End If

                If i.Equals(23) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI23.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI23.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION23.Text
                End If

                If i.Equals(24) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI24.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI24.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION24.Text
                End If

                If i.Equals(25) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI25.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI25.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION25.Text
                End If

                If i.Equals(26) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI26.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI26.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION26.Text
                End If

                If i.Equals(27) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI27.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI27.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION27.Text
                End If

                If i.Equals(28) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI28.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI28.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION28.Text
                End If

                If i.Equals(29) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI29.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI29.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION29.Text
                End If

                If i.Equals(30) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI30.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI30.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION30.Text
                End If

                If i.Equals(31) Then
                    sheet.Cells(9 + i, 4) = Me.lblYOUBI31.Text
                    sheet.Cells(9 + i, 6) = Me.cmbTENKI31.Text
                    sheet.Cells(9 + i, 9) = Me.txtKION31.Text
                End If
            Next


            'ファイル保存
            book.SaveAs(strSaveReportName)

            book.Close()

            System.Diagnostics.Process.Start(strSaveReportName)

            Return True

        Catch ex As Exception
            Throw ex
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
            '曜日
            Me.lblYOUBI1.Text = String.Empty
            Me.lblYOUBI2.Text = String.Empty
            Me.lblYOUBI3.Text = String.Empty
            Me.lblYOUBI4.Text = String.Empty
            Me.lblYOUBI5.Text = String.Empty
            Me.lblYOUBI6.Text = String.Empty
            Me.lblYOUBI7.Text = String.Empty
            Me.lblYOUBI8.Text = String.Empty
            Me.lblYOUBI9.Text = String.Empty
            Me.lblYOUBI10.Text = String.Empty
            Me.lblYOUBI11.Text = String.Empty
            Me.lblYOUBI12.Text = String.Empty
            Me.lblYOUBI13.Text = String.Empty
            Me.lblYOUBI14.Text = String.Empty
            Me.lblYOUBI15.Text = String.Empty
            Me.lblYOUBI16.Text = String.Empty
            Me.lblYOUBI17.Text = String.Empty
            Me.lblYOUBI18.Text = String.Empty
            Me.lblYOUBI19.Text = String.Empty
            Me.lblYOUBI20.Text = String.Empty
            Me.lblYOUBI21.Text = String.Empty
            Me.lblYOUBI22.Text = String.Empty
            Me.lblYOUBI23.Text = String.Empty
            Me.lblYOUBI24.Text = String.Empty
            Me.lblYOUBI25.Text = String.Empty
            Me.lblYOUBI26.Text = String.Empty
            Me.lblYOUBI27.Text = String.Empty
            Me.lblYOUBI28.Text = String.Empty
            Me.lblYOUBI29.Text = String.Empty
            Me.lblYOUBI30.Text = String.Empty
            Me.lblYOUBI31.Text = String.Empty
            '天気
            Me.cmbTENKI1.Text = String.Empty
            Me.cmbTENKI2.Text = String.Empty
            Me.cmbTENKI3.Text = String.Empty
            Me.cmbTENKI4.Text = String.Empty
            Me.cmbTENKI5.Text = String.Empty
            Me.cmbTENKI6.Text = String.Empty
            Me.cmbTENKI7.Text = String.Empty
            Me.cmbTENKI8.Text = String.Empty
            Me.cmbTENKI9.Text = String.Empty
            Me.cmbTENKI10.Text = String.Empty
            Me.cmbTENKI11.Text = String.Empty
            Me.cmbTENKI12.Text = String.Empty
            Me.cmbTENKI13.Text = String.Empty
            Me.cmbTENKI14.Text = String.Empty
            Me.cmbTENKI15.Text = String.Empty
            Me.cmbTENKI16.Text = String.Empty
            Me.cmbTENKI17.Text = String.Empty
            Me.cmbTENKI18.Text = String.Empty
            Me.cmbTENKI19.Text = String.Empty
            Me.cmbTENKI20.Text = String.Empty
            Me.cmbTENKI21.Text = String.Empty
            Me.cmbTENKI22.Text = String.Empty
            Me.cmbTENKI23.Text = String.Empty
            Me.cmbTENKI24.Text = String.Empty
            Me.cmbTENKI25.Text = String.Empty
            Me.cmbTENKI26.Text = String.Empty
            Me.cmbTENKI27.Text = String.Empty
            Me.cmbTENKI28.Text = String.Empty
            Me.cmbTENKI29.Text = String.Empty
            Me.cmbTENKI30.Text = String.Empty
            Me.cmbTENKI31.Text = String.Empty
            '気温
            Me.txtKION1.Text = String.Empty
            Me.txtKION2.Text = String.Empty
            Me.txtKION3.Text = String.Empty
            Me.txtKION4.Text = String.Empty
            Me.txtKION5.Text = String.Empty
            Me.txtKION6.Text = String.Empty
            Me.txtKION7.Text = String.Empty
            Me.txtKION8.Text = String.Empty
            Me.txtKION9.Text = String.Empty
            Me.txtKION10.Text = String.Empty
            Me.txtKION11.Text = String.Empty
            Me.txtKION12.Text = String.Empty
            Me.txtKION13.Text = String.Empty
            Me.txtKION14.Text = String.Empty
            Me.txtKION15.Text = String.Empty
            Me.txtKION16.Text = String.Empty
            Me.txtKION17.Text = String.Empty
            Me.txtKION18.Text = String.Empty
            Me.txtKION19.Text = String.Empty
            Me.txtKION20.Text = String.Empty
            Me.txtKION21.Text = String.Empty
            Me.txtKION22.Text = String.Empty
            Me.txtKION23.Text = String.Empty
            Me.txtKION24.Text = String.Empty
            Me.txtKION25.Text = String.Empty
            Me.txtKION26.Text = String.Empty
            Me.txtKION27.Text = String.Empty
            Me.txtKION28.Text = String.Empty
            Me.txtKION29.Text = String.Empty
            Me.txtKION30.Text = String.Empty
            Me.txtKION31.Text = String.Empty
            '天気・気温ﾊﾟﾈﾙ
            Me.pnl1.Visible = False
            Me.pnl2.Visible = False
            Me.pnl3.Visible = False
            Me.pnl4.Visible = False
            Me.pnl5.Visible = False
            Me.pnl6.Visible = False
            Me.pnl7.Visible = False
            Me.pnl8.Visible = False
            Me.pnl9.Visible = False
            Me.pnl10.Visible = False
            Me.pnl11.Visible = False
            Me.pnl12.Visible = False
            Me.pnl13.Visible = False
            Me.pnl14.Visible = False
            Me.pnl15.Visible = False
            Me.pnl16.Visible = False
            Me.pnl17.Visible = False
            Me.pnl18.Visible = False
            Me.pnl19.Visible = False
            Me.pnl20.Visible = False
            Me.pnl21.Visible = False
            Me.pnl22.Visible = False
            Me.pnl23.Visible = False
            Me.pnl24.Visible = False
            Me.pnl25.Visible = False
            Me.pnl26.Visible = False
            Me.pnl27.Visible = False
            Me.pnl28.Visible = False
            Me.pnl29.Visible = False
            Me.pnl30.Visible = False
            Me.pnl31.Visible = False


            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM CALMTA")
            sql.Append(" WHERE")
            sql.Append(" CALDT >= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "01'")
            sql.Append(" AND CALDT <= '" & Me.txtYear.Text & (Me.cmbMonth.SelectedIndex + 1).ToString.PadLeft(2, "0"c) & "31'")

            Dim dtCALMTA As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If dtCALMTA.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim strYoubi() As String = {"日", "月", "火", "水", "木", "金", "土"}

            For i As Integer = 0 To dtCALMTA.Rows.Count - 1
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("01") Then
                    Me.pnl1.Visible = True
                    '曜日
                    Me.lblYOUBI1.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI1.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION1.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("02") Then
                    Me.pnl2.Visible = True
                    '曜日
                    Me.lblYOUBI2.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI2.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION2.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("03") Then
                    Me.pnl3.Visible = True
                    '曜日
                    Me.lblYOUBI3.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI3.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION3.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("04") Then
                    Me.pnl4.Visible = True
                    '曜日
                    Me.lblYOUBI4.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI4.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION4.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("05") Then
                    Me.pnl5.Visible = True
                    '曜日
                    Me.lblYOUBI5.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI5.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION5.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("06") Then
                    Me.pnl6.Visible = True
                    '曜日
                    Me.lblYOUBI6.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI6.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION6.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("07") Then
                    Me.pnl7.Visible = True
                    '曜日
                    Me.lblYOUBI7.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI7.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION7.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("08") Then
                    Me.pnl8.Visible = True
                    '曜日
                    Me.lblYOUBI8.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI8.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION8.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("09") Then
                    Me.pnl9.Visible = True
                    '曜日
                    Me.lblYOUBI9.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI9.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION9.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("10") Then
                    Me.pnl10.Visible = True
                    '曜日
                    Me.lblYOUBI10.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI10.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION10.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("11") Then
                    Me.pnl11.Visible = True
                    '曜日
                    Me.lblYOUBI11.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI11.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION11.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("12") Then
                    Me.pnl12.Visible = True
                    '曜日
                    Me.lblYOUBI12.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI12.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION12.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("13") Then
                    Me.pnl13.Visible = True
                    '曜日
                    Me.lblYOUBI13.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI13.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION13.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("14") Then
                    Me.pnl14.Visible = True
                    '曜日
                    Me.lblYOUBI14.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI14.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION14.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("15") Then
                    Me.pnl15.Visible = True
                    '曜日
                    Me.lblYOUBI15.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI15.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION15.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("16") Then
                    Me.pnl16.Visible = True
                    '曜日
                    Me.lblYOUBI16.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI16.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION16.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("17") Then
                    Me.pnl17.Visible = True
                    '曜日
                    Me.lblYOUBI17.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI17.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION17.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("18") Then
                    Me.pnl18.Visible = True
                    '曜日
                    Me.lblYOUBI18.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI18.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION18.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("19") Then
                    Me.pnl19.Visible = True
                    '曜日
                    Me.lblYOUBI19.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI19.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION19.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("20") Then
                    Me.pnl20.Visible = True
                    '曜日
                    Me.lblYOUBI20.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI20.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION20.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("21") Then
                    Me.pnl21.Visible = True
                    '曜日
                    Me.lblYOUBI21.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI21.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION21.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("22") Then
                    Me.pnl22.Visible = True
                    '曜日
                    Me.lblYOUBI22.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI22.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION22.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("23") Then
                    Me.pnl23.Visible = True
                    '曜日
                    Me.lblYOUBI23.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI23.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION23.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("24") Then
                    Me.pnl24.Visible = True
                    '曜日
                    Me.lblYOUBI24.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI24.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION24.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("25") Then
                    Me.pnl25.Visible = True
                    '曜日
                    Me.lblYOUBI25.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI25.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION25.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("26") Then
                    Me.pnl26.Visible = True
                    '曜日
                    Me.lblYOUBI26.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI26.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION26.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("27") Then
                    Me.pnl27.Visible = True
                    '曜日
                    Me.lblYOUBI27.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI27.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION27.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("28") Then
                    Me.pnl28.Visible = True
                    '曜日
                    Me.lblYOUBI28.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI28.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION28.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("29") Then
                    Me.pnl29.Visible = True
                    '曜日
                    Me.lblYOUBI29.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI29.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION29.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("30") Then
                    Me.pnl30.Visible = True
                    '曜日
                    Me.lblYOUBI30.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI30.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION30.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
                If dtCALMTA.Rows(i).Item("CALDT").ToString.Substring(6, 2).Equals("31") Then
                    Me.pnl31.Visible = True
                    '曜日
                    Me.lblYOUBI31.Text = strYoubi(CType(dtCALMTA.Rows(i).Item("YOUBIKB").ToString, Integer) - 1)
                    '天気
                    Me.cmbTENKI31.Text = dtCALMTA.Rows(i).Item("TENKI").ToString
                    '気温
                    Me.txtKION31.Text = dtCALMTA.Rows(i).Item("KION").ToString
                End If
            Next



            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTable(ByVal strYear As String, ByVal strMonth As String) As DataTable
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()

            'If Me.rdoRepo01.Checked Then
            '    '【1球貸し打球数】
            '    sql.Append(" SELECT ")
            '    sql.Append(" CASE WHEN")
            '    sql.Append(" SUM(NKB01BALL) + SUM(NKB02BALL) + SUM(NKB03BALL) + SUM(NKB04BALL) + SUM(NKB05BALL) + SUM(NKB06BALL) + SUM(NKB07BALL) + SUM(NKB08BALL) + SUM(NKB09BALL) + SUM(NKB10BALL)")
            '    sql.Append(" IS NULL THEN 0 ELSE ")
            '    sql.Append(" SUM(NKB01BALL) + SUM(NKB02BALL) + SUM(NKB03BALL) + SUM(NKB04BALL) + SUM(NKB05BALL) + SUM(NKB06BALL) + SUM(NKB07BALL) + SUM(NKB08BALL) + SUM(NKB09BALL) + SUM(NKB10BALL)")
            '    sql.Append(" END AS VALUE")
            '    sql.Append(" FROM SEATSMA ")
            '    sql.Append(" WHERE")
            '    sql.Append(" SEATDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND SEATDT <= '" & strYear & strMonth & "31'")
            'ElseIf Me.rdoRepo02.Checked Then
            '    '【1球貸し球数売上】
            '    sql.Append(" SELECT ")
            '    sql.Append(" CASE WHEN")
            '    sql.Append(" SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN) + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN)")
            '    sql.Append(" + SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN) + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN)")
            '    sql.Append(" IS NULL THEN 0 ELSE ")
            '    sql.Append(" SUM(NKB01KIN) + SUM(NKB02KIN) + SUM(NKB03KIN) + SUM(NKB04KIN) + SUM(NKB05KIN) + SUM(NKB06KIN) + SUM(NKB07KIN) + SUM(NKB08KIN) + SUM(NKB09KIN) + SUM(NKB10KIN)")
            '    sql.Append(" + SUM(NKB01TAXKIN) + SUM(NKB02TAXKIN) + SUM(NKB03TAXKIN) + SUM(NKB04TAXKIN) + SUM(NKB05TAXKIN) + SUM(NKB06TAXKIN) + SUM(NKB07TAXKIN) + SUM(NKB08TAXKIN) + SUM(NKB09TAXKIN) + SUM(NKB10TAXKIN)")
            '    sql.Append(" END AS VALUE")
            '    sql.Append(" FROM SEATSMA ")
            '    sql.Append(" WHERE")
            '    sql.Append(" SEATDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND SEATDT <= '" & strYear & strMonth & "31'")
            'ElseIf Me.rdoRepo03.Checked Then
            '    '【打ち放題売上】
            '    sql.Append(" SELECT ")
            '    sql.Append(" CASE WHEN SUM(ENTBKN) + SUM(ENTZEI) IS NULL THEN 0 ELSE SUM(ENTBKN) + SUM(ENTZEI) END AS VALUE")
            '    sql.Append(" FROM ENTTRA")
            '    sql.Append(" WHERE")
            '    sql.Append(" DATKB = '1'")
            '    sql.Append(" AND EIGKB = '2'")
            '    sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            'ElseIf Me.rdoRepo05.Checked Then
            '    '【入場者数】
            '    sql.Append(" SELECT COUNT(MANNO) AS VALUE FROM (")

            '    sql.Append(" SELECT ")
            '    sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
            '    sql.Append(" FROM ENTTRB ")
            '    sql.Append(" WHERE")
            '    sql.Append(" ENTDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")

            '    sql.Append(" UNION ")

            '    sql.Append(" SELECT ")
            '    sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
            '    sql.Append(" FROM ENTTRA ")
            '    sql.Append(" WHERE")
            '    sql.Append(" DATKB = '1'")
            '    sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            '    sql.Append(" AND (EIGKB = '2' OR (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10' OR KSBKB = '11' OR KSBKB = '12' OR KSBKB = '13')))")
            '    sql.Append(") AS A")

            '    'sql.Append(" SELECT ")
            '    'sql.Append(" COUNT(MANNO) AS VALUE")
            '    'sql.Append(" FROM ENTTRA ")
            '    'sql.Append(" WHERE")
            '    'sql.Append(" DATKB = '1'")
            '    'sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
            '    'sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            '    'sql.Append(" AND (EIGKB = '2' OR (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10' OR KSBKB = '11' OR KSBKB = '12' OR KSBKB = '13')))")
            'ElseIf Me.rdoRepo06.Checked Then
            '    '【1球貸し利用者数】

            '    sql.Append(" SELECT COUNT(MANNO) AS VALUE FROM (")

            '    sql.Append(" SELECT ")
            '    sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
            '    sql.Append(" FROM ENTTRB ")
            '    sql.Append(" WHERE")
            '    sql.Append(" ENTDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")

            '    sql.Append(" UNION ")

            '    sql.Append(" SELECT ")
            '    sql.Append(" ENTDT,MANNO,KSBKB,RKNKB,TIMCD,TIMTM,INSDTM")
            '    sql.Append(" FROM ENTTRA ")
            '    sql.Append(" WHERE")
            '    sql.Append(" DATKB = '1'")
            '    sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            '    sql.Append(" AND (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10'))")
            '    sql.Append(") AS A")

            '    'sql.Append(" SELECT ")
            '    'sql.Append(" COUNT(MANNO) AS VALUE")
            '    'sql.Append(" FROM ENTTRA ")
            '    'sql.Append(" WHERE")
            '    'sql.Append(" DATKB = '1'")
            '    'sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
            '    'sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            '    'sql.Append(" AND (EIGKB = '1' AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10'))")
            'ElseIf Me.rdoRepo07.Checked Then
            '    '【打ち放題利用者数】
            '    sql.Append(" SELECT ")
            '    sql.Append(" COUNT(MANNO) AS VALUE")
            '    sql.Append(" FROM ENTTRA ")
            '    sql.Append(" WHERE")
            '    sql.Append(" DATKB = '1'")
            '    sql.Append(" AND ENTDT >= '" & strYear & strMonth & "01'")
            '    sql.Append(" AND ENTDT <= '" & strYear & strMonth & "31'")
            '    sql.Append(" AND EIGKB = '2'")
            'End If

            Return iDatabase.ExecuteRead(sql.ToString())

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region






End Class


