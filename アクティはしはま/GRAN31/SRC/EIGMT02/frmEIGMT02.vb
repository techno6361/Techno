Imports TECHNO.DataBase

Public Class frmEIGMT02

#Region "▼宣言部"

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打ち放題マスタ"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打ち放題マスタ"

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
    Private Sub frmEIGMT02_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '上限打球数取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

            '料金体系マスタ情報取得
            If Not GetRKNMTA() Then
                Using frm As New frmMSGBOX01("料金体系マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '打ち放題マスタ情報取得
            If Not GetEIGMTB() Then

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 料金体系コンボボックス_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbRKNKB_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRKNKB.SelectedIndexChanged
        Try
            '画面初期表示
            Init()



            '打ち放題情報取得
            If Not GetEIGMTB() Then
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

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
            Msg = Me.cmbCopyRKNKB.Text & "の打ち放題情報をコピーしてよろしいですか？"
            Using frm As New frmMSGBOX01(Msg, 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '画面初期化
            Init()



            '営業情報取得
            GetEIGMTB(True)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 上限打球数テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtMAXJDCNT_Validated(sender As System.Object, e As System.EventArgs) Handles txtMAXJDCNT.Validated
        Try
            If String.IsNullOrEmpty(Me.txtMAXJDCNT.Text) Then Me.txtMAXJDCNT.Text = "0"

            ChangeMaxBallsu()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 貸し時間テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtJKNMM_Validated(sender As System.Object, e As System.EventArgs) Handles txtJKNMM01.Validated, txtJKNMM02.Validated, txtJKNMM03.Validated, txtJKNMM04.Validated, txtJKNMM05.Validated _
                                                                                        , txtJKNMM06.Validated, txtJKNMM07.Validated, txtJKNMM08.Validated, txtJKNMM09.Validated, txtJKNMM10.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtJKNMM01"
                        Me.txtJKNNAME01.Text = String.Empty
                        Me.txtJKNKIN01_01.Text = String.Empty
                        Me.txtJKNKIN01_02.Text = String.Empty
                        Me.txtJKNKIN01_03.Text = String.Empty
                        Me.txtJKNKIN01_04.Text = String.Empty
                        Me.txtJKNKIN01_05.Text = String.Empty
                        Me.txtJKNKIN01_06.Text = String.Empty
                        Me.txtJKNKIN01_07.Text = String.Empty
                        Me.txtJKNKIN01_08.Text = String.Empty
                        Me.txtJKNKIN01_09.Text = String.Empty
                        Me.txtJKNKIN01_10.Text = String.Empty
                        Me.txtPOINT01.Text = String.Empty
                        Me.txtENDTIME01.Text = String.Empty
                        Me.cmbJKNFLR01.SelectedIndex = 0
                        Me.txtMAXBALLSU01.Text = String.Empty
                    Case "txtJKNMM02"
                        Me.txtJKNNAME02.Text = String.Empty
                        Me.txtJKNKIN02_01.Text = String.Empty
                        Me.txtJKNKIN02_02.Text = String.Empty
                        Me.txtJKNKIN02_03.Text = String.Empty
                        Me.txtJKNKIN02_04.Text = String.Empty
                        Me.txtJKNKIN02_05.Text = String.Empty
                        Me.txtJKNKIN02_06.Text = String.Empty
                        Me.txtJKNKIN02_07.Text = String.Empty
                        Me.txtJKNKIN02_08.Text = String.Empty
                        Me.txtJKNKIN02_09.Text = String.Empty
                        Me.txtJKNKIN02_10.Text = String.Empty
                        Me.txtPOINT02.Text = String.Empty
                        Me.txtENDTIME02.Text = String.Empty
                        Me.cmbJKNFLR02.SelectedIndex = 0
                        Me.txtMAXBALLSU02.Text = String.Empty
                    Case "txtJKNMM03"
                        Me.txtJKNNAME03.Text = String.Empty
                        Me.txtJKNKIN03_01.Text = String.Empty
                        Me.txtJKNKIN03_02.Text = String.Empty
                        Me.txtJKNKIN03_03.Text = String.Empty
                        Me.txtJKNKIN03_04.Text = String.Empty
                        Me.txtJKNKIN03_05.Text = String.Empty
                        Me.txtJKNKIN03_06.Text = String.Empty
                        Me.txtJKNKIN03_07.Text = String.Empty
                        Me.txtJKNKIN03_08.Text = String.Empty
                        Me.txtJKNKIN03_09.Text = String.Empty
                        Me.txtJKNKIN03_10.Text = String.Empty
                        Me.txtPOINT03.Text = String.Empty
                        Me.txtENDTIME03.Text = String.Empty
                        Me.cmbJKNFLR03.SelectedIndex = 0
                        Me.txtMAXBALLSU03.Text = String.Empty
                    Case "txtJKNMM04"
                        Me.txtJKNNAME04.Text = String.Empty
                        Me.txtJKNKIN04_01.Text = String.Empty
                        Me.txtJKNKIN04_02.Text = String.Empty
                        Me.txtJKNKIN04_03.Text = String.Empty
                        Me.txtJKNKIN04_04.Text = String.Empty
                        Me.txtJKNKIN04_05.Text = String.Empty
                        Me.txtJKNKIN04_06.Text = String.Empty
                        Me.txtJKNKIN04_07.Text = String.Empty
                        Me.txtJKNKIN04_08.Text = String.Empty
                        Me.txtJKNKIN04_09.Text = String.Empty
                        Me.txtJKNKIN04_10.Text = String.Empty
                        Me.txtPOINT04.Text = String.Empty
                        Me.txtENDTIME04.Text = String.Empty
                        Me.cmbJKNFLR04.SelectedIndex = 0
                        Me.txtMAXBALLSU04.Text = String.Empty
                    Case "txtJKNMM05"
                        Me.txtJKNNAME05.Text = String.Empty
                        Me.txtJKNKIN05_01.Text = String.Empty
                        Me.txtJKNKIN05_02.Text = String.Empty
                        Me.txtJKNKIN05_03.Text = String.Empty
                        Me.txtJKNKIN05_04.Text = String.Empty
                        Me.txtJKNKIN05_05.Text = String.Empty
                        Me.txtJKNKIN05_06.Text = String.Empty
                        Me.txtJKNKIN05_07.Text = String.Empty
                        Me.txtJKNKIN05_08.Text = String.Empty
                        Me.txtJKNKIN05_09.Text = String.Empty
                        Me.txtJKNKIN05_10.Text = String.Empty
                        Me.txtPOINT05.Text = String.Empty
                        Me.txtENDTIME05.Text = String.Empty
                        Me.cmbJKNFLR05.SelectedIndex = 0
                        Me.txtMAXBALLSU05.Text = String.Empty
                    Case "txtJKNMM06"
                        Me.txtJKNNAME06.Text = String.Empty
                        Me.txtJKNKIN06_01.Text = String.Empty
                        Me.txtJKNKIN06_02.Text = String.Empty
                        Me.txtJKNKIN06_03.Text = String.Empty
                        Me.txtJKNKIN06_04.Text = String.Empty
                        Me.txtJKNKIN06_05.Text = String.Empty
                        Me.txtJKNKIN06_06.Text = String.Empty
                        Me.txtJKNKIN06_07.Text = String.Empty
                        Me.txtJKNKIN06_08.Text = String.Empty
                        Me.txtJKNKIN06_09.Text = String.Empty
                        Me.txtJKNKIN06_10.Text = String.Empty
                        Me.txtPOINT06.Text = String.Empty
                        Me.txtENDTIME06.Text = String.Empty
                        Me.cmbJKNFLR06.SelectedIndex = 0
                        Me.txtMAXBALLSU06.Text = String.Empty
                    Case "txtJKNMM07"
                        Me.txtJKNNAME07.Text = String.Empty
                        Me.txtJKNKIN07_01.Text = String.Empty
                        Me.txtJKNKIN07_02.Text = String.Empty
                        Me.txtJKNKIN07_03.Text = String.Empty
                        Me.txtJKNKIN07_04.Text = String.Empty
                        Me.txtJKNKIN07_05.Text = String.Empty
                        Me.txtJKNKIN07_06.Text = String.Empty
                        Me.txtJKNKIN07_07.Text = String.Empty
                        Me.txtJKNKIN07_08.Text = String.Empty
                        Me.txtJKNKIN07_09.Text = String.Empty
                        Me.txtJKNKIN07_10.Text = String.Empty
                        Me.txtPOINT07.Text = String.Empty
                        Me.txtENDTIME07.Text = String.Empty
                        Me.cmbJKNFLR07.SelectedIndex = 0
                        Me.txtMAXBALLSU07.Text = String.Empty
                    Case "txtJKNMM08"
                        Me.txtJKNNAME08.Text = String.Empty
                        Me.txtJKNKIN08_01.Text = String.Empty
                        Me.txtJKNKIN08_02.Text = String.Empty
                        Me.txtJKNKIN08_03.Text = String.Empty
                        Me.txtJKNKIN08_04.Text = String.Empty
                        Me.txtJKNKIN08_05.Text = String.Empty
                        Me.txtJKNKIN08_06.Text = String.Empty
                        Me.txtJKNKIN08_07.Text = String.Empty
                        Me.txtJKNKIN08_08.Text = String.Empty
                        Me.txtJKNKIN08_09.Text = String.Empty
                        Me.txtJKNKIN08_10.Text = String.Empty
                        Me.txtPOINT08.Text = String.Empty
                        Me.txtENDTIME08.Text = String.Empty
                        Me.cmbJKNFLR08.SelectedIndex = 0
                        Me.txtMAXBALLSU08.Text = String.Empty
                    Case "txtJKNMM09"
                        Me.txtJKNNAME09.Text = String.Empty
                        Me.txtJKNKIN09_01.Text = String.Empty
                        Me.txtJKNKIN09_02.Text = String.Empty
                        Me.txtJKNKIN09_03.Text = String.Empty
                        Me.txtJKNKIN09_04.Text = String.Empty
                        Me.txtJKNKIN09_05.Text = String.Empty
                        Me.txtJKNKIN09_06.Text = String.Empty
                        Me.txtJKNKIN09_07.Text = String.Empty
                        Me.txtJKNKIN09_08.Text = String.Empty
                        Me.txtJKNKIN09_09.Text = String.Empty
                        Me.txtJKNKIN09_10.Text = String.Empty
                        Me.txtPOINT09.Text = String.Empty
                        Me.txtENDTIME09.Text = String.Empty
                        Me.cmbJKNFLR09.SelectedIndex = 0
                        Me.txtMAXBALLSU09.Text = String.Empty
                    Case "txtJKNMM10"
                        Me.txtJKNNAME10.Text = String.Empty
                        Me.txtJKNKIN10_01.Text = String.Empty
                        Me.txtJKNKIN10_02.Text = String.Empty
                        Me.txtJKNKIN10_03.Text = String.Empty
                        Me.txtJKNKIN10_04.Text = String.Empty
                        Me.txtJKNKIN10_05.Text = String.Empty
                        Me.txtJKNKIN10_06.Text = String.Empty
                        Me.txtJKNKIN10_07.Text = String.Empty
                        Me.txtJKNKIN10_08.Text = String.Empty
                        Me.txtJKNKIN10_09.Text = String.Empty
                        Me.txtJKNKIN10_10.Text = String.Empty
                        Me.txtPOINT10.Text = String.Empty
                        Me.txtENDTIME10.Text = String.Empty
                        Me.cmbJKNFLR10.SelectedIndex = 0
                        Me.txtMAXBALLSU10.Text = String.Empty
                End Select
                Exit Sub
            End If

            If CType(txtBox.Text, Integer) > 240 Then
                txtBox.Text = "240"
            End If

            ChangeMaxBallsu()

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
    Private Sub txtJKNKIN_Validated(sender As System.Object, e As System.EventArgs) Handles txtJKNKIN01_01.Validated, txtJKNKIN02_01.Validated, txtJKNKIN03_01.Validated, txtJKNKIN04_01.Validated, txtJKNKIN05_01.Validated _
                                                                                        , txtJKNKIN06_01.Validated, txtJKNKIN07_01.Validated, txtJKNKIN08_01.Validated, txtJKNKIN09_01.Validated, txtJKNKIN10_01.Validated _
                                                                                        , txtJKNKIN01_02.Validated, txtJKNKIN02_02.Validated, txtJKNKIN03_02.Validated, txtJKNKIN04_02.Validated, txtJKNKIN05_02.Validated _
                                                                                        , txtJKNKIN06_02.Validated, txtJKNKIN07_02.Validated, txtJKNKIN08_02.Validated, txtJKNKIN09_02.Validated, txtJKNKIN10_02.Validated _
                                                                                        , txtJKNKIN01_03.Validated, txtJKNKIN02_03.Validated, txtJKNKIN03_03.Validated, txtJKNKIN04_03.Validated, txtJKNKIN05_03.Validated _
                                                                                        , txtJKNKIN06_03.Validated, txtJKNKIN07_03.Validated, txtJKNKIN08_03.Validated, txtJKNKIN09_03.Validated, txtJKNKIN10_03.Validated _
                                                                                        , txtJKNKIN01_04.Validated, txtJKNKIN02_04.Validated, txtJKNKIN03_04.Validated, txtJKNKIN04_04.Validated, txtJKNKIN05_04.Validated _
                                                                                        , txtJKNKIN06_04.Validated, txtJKNKIN07_04.Validated, txtJKNKIN08_04.Validated, txtJKNKIN09_04.Validated, txtJKNKIN10_04.Validated _
                                                                                        , txtJKNKIN01_05.Validated, txtJKNKIN02_05.Validated, txtJKNKIN03_05.Validated, txtJKNKIN04_05.Validated, txtJKNKIN05_05.Validated _
                                                                                        , txtJKNKIN06_05.Validated, txtJKNKIN07_05.Validated, txtJKNKIN08_05.Validated, txtJKNKIN09_05.Validated, txtJKNKIN10_05.Validated _
                                                                                        , txtJKNKIN01_06.Validated, txtJKNKIN02_06.Validated, txtJKNKIN03_06.Validated, txtJKNKIN04_06.Validated, txtJKNKIN05_06.Validated _
                                                                                        , txtJKNKIN06_06.Validated, txtJKNKIN07_06.Validated, txtJKNKIN08_06.Validated, txtJKNKIN09_06.Validated, txtJKNKIN10_06.Validated _
                                                                                        , txtJKNKIN01_07.Validated, txtJKNKIN02_07.Validated, txtJKNKIN03_07.Validated, txtJKNKIN04_07.Validated, txtJKNKIN05_07.Validated _
                                                                                        , txtJKNKIN06_07.Validated, txtJKNKIN07_07.Validated, txtJKNKIN08_07.Validated, txtJKNKIN09_07.Validated, txtJKNKIN10_07.Validated _
                                                                                        , txtJKNKIN01_08.Validated, txtJKNKIN02_08.Validated, txtJKNKIN03_08.Validated, txtJKNKIN04_08.Validated, txtJKNKIN05_08.Validated _
                                                                                        , txtJKNKIN06_08.Validated, txtJKNKIN07_08.Validated, txtJKNKIN08_08.Validated, txtJKNKIN09_08.Validated, txtJKNKIN10_08.Validated _
                                                                                        , txtJKNKIN01_09.Validated, txtJKNKIN02_09.Validated, txtJKNKIN03_09.Validated, txtJKNKIN04_09.Validated, txtJKNKIN05_09.Validated _
                                                                                        , txtJKNKIN06_09.Validated, txtJKNKIN07_09.Validated, txtJKNKIN08_09.Validated, txtJKNKIN09_09.Validated, txtJKNKIN10_09.Validated _
                                                                                        , txtJKNKIN01_10.Validated, txtJKNKIN02_10.Validated, txtJKNKIN03_10.Validated, txtJKNKIN04_10.Validated, txtJKNKIN05_10.Validated _
                                                                                        , txtJKNKIN06_10.Validated, txtJKNKIN07_10.Validated, txtJKNKIN08_10.Validated, txtJKNKIN09_10.Validated, txtJKNKIN10_10.Validated
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
    Private Sub txtPOINT_Validated(sender As System.Object, e As System.EventArgs) Handles txtPOINT01.Validated, txtPOINT02.Validated, txtPOINT03.Validated, txtPOINT04.Validated, txtPOINT05.Validated _
                                                                                          , txtPOINT06.Validated, txtPOINT07.Validated, txtPOINT08.Validated, txtPOINT09.Validated, txtPOINT10.Validated
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
    ''' 終了時間テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtENDTIME_Validated(sender As System.Object, e As System.EventArgs) Handles txtENDTIME01.Validated, txtENDTIME02.Validated, txtENDTIME03.Validated, txtENDTIME04.Validated, txtENDTIME05.Validated _
                                                                                          , txtENDTIME06.Validated, txtENDTIME07.Validated, txtENDTIME08.Validated, txtENDTIME09.Validated, txtENDTIME10.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            txtBox.Text = txtBox.Text.PadLeft(4, "0"c).Substring(0, 2) & ":" & txtBox.Text.PadLeft(4, "0"c).Substring(2, 2)


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 利用限度球数テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtMAXBALLSU_Validated(sender As System.Object, e As System.EventArgs) Handles txtMAXBALLSU01.Validated, txtMAXBALLSU02.Validated, txtMAXBALLSU03.Validated, txtMAXBALLSU04.Validated, txtMAXBALLSU05.Validated _
                                                                                          , txtMAXBALLSU06.Validated, txtMAXBALLSU07.Validated, txtMAXBALLSU08.Validated, txtMAXBALLSU09.Validated, txtMAXBALLSU10.Validated
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
    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtJKNMM01.KeyPress, txtJKNMM02.KeyPress, txtJKNMM03.KeyPress, txtJKNMM04.KeyPress, txtJKNMM05.KeyPress _
                                                                                                                        , txtJKNMM06.KeyPress, txtJKNMM07.KeyPress, txtJKNMM08.KeyPress, txtJKNMM09.KeyPress, txtJKNMM10.KeyPress _
                                                                                                                        , txtJKNKIN01_01.KeyPress, txtJKNKIN02_01.KeyPress, txtJKNKIN03_01.KeyPress, txtJKNKIN04_01.KeyPress, txtJKNKIN05_01.KeyPress _
                                                                                                                        , txtJKNKIN06_01.KeyPress, txtJKNKIN07_01.KeyPress, txtJKNKIN08_01.KeyPress, txtJKNKIN09_01.KeyPress, txtJKNKIN10_01.KeyPress _
                                                                                                                        , txtJKNKIN01_02.KeyPress, txtJKNKIN02_02.KeyPress, txtJKNKIN03_02.KeyPress, txtJKNKIN04_02.KeyPress, txtJKNKIN05_02.KeyPress _
                                                                                                                        , txtJKNKIN06_02.KeyPress, txtJKNKIN07_02.KeyPress, txtJKNKIN08_02.KeyPress, txtJKNKIN09_02.KeyPress, txtJKNKIN10_02.KeyPress _
                                                                                                                        , txtJKNKIN01_03.KeyPress, txtJKNKIN02_03.KeyPress, txtJKNKIN03_03.KeyPress, txtJKNKIN04_03.KeyPress, txtJKNKIN05_03.KeyPress _
                                                                                                                        , txtJKNKIN06_03.KeyPress, txtJKNKIN07_03.KeyPress, txtJKNKIN08_03.KeyPress, txtJKNKIN09_03.KeyPress, txtJKNKIN10_03.KeyPress _
                                                                                                                        , txtJKNKIN01_04.KeyPress, txtJKNKIN02_04.KeyPress, txtJKNKIN03_04.KeyPress, txtJKNKIN04_04.KeyPress, txtJKNKIN05_04.KeyPress _
                                                                                                                        , txtJKNKIN06_04.KeyPress, txtJKNKIN07_04.KeyPress, txtJKNKIN08_04.KeyPress, txtJKNKIN09_04.KeyPress, txtJKNKIN10_04.KeyPress _
                                                                                                                        , txtJKNKIN01_05.KeyPress, txtJKNKIN02_05.KeyPress, txtJKNKIN03_05.KeyPress, txtJKNKIN04_05.KeyPress, txtJKNKIN05_05.KeyPress _
                                                                                                                        , txtJKNKIN06_05.KeyPress, txtJKNKIN07_05.KeyPress, txtJKNKIN08_05.KeyPress, txtJKNKIN09_05.KeyPress, txtJKNKIN10_05.KeyPress _
                                                                                                                        , txtJKNKIN01_06.KeyPress, txtJKNKIN02_06.KeyPress, txtJKNKIN03_06.KeyPress, txtJKNKIN04_06.KeyPress, txtJKNKIN05_06.KeyPress _
                                                                                                                        , txtJKNKIN06_06.KeyPress, txtJKNKIN07_06.KeyPress, txtJKNKIN08_06.KeyPress, txtJKNKIN09_06.KeyPress, txtJKNKIN10_06.KeyPress _
                                                                                                                        , txtJKNKIN01_07.KeyPress, txtJKNKIN02_07.KeyPress, txtJKNKIN03_07.KeyPress, txtJKNKIN04_07.KeyPress, txtJKNKIN05_07.KeyPress _
                                                                                                                        , txtJKNKIN06_07.KeyPress, txtJKNKIN07_07.KeyPress, txtJKNKIN08_07.KeyPress, txtJKNKIN09_07.KeyPress, txtJKNKIN10_07.KeyPress _
                                                                                                                        , txtJKNKIN01_08.KeyPress, txtJKNKIN02_08.KeyPress, txtJKNKIN03_08.KeyPress, txtJKNKIN04_08.KeyPress, txtJKNKIN05_08.KeyPress _
                                                                                                                        , txtJKNKIN06_08.KeyPress, txtJKNKIN07_08.KeyPress, txtJKNKIN08_08.KeyPress, txtJKNKIN09_08.KeyPress, txtJKNKIN10_08.KeyPress _
                                                                                                                        , txtJKNKIN01_09.KeyPress, txtJKNKIN02_09.KeyPress, txtJKNKIN03_09.KeyPress, txtJKNKIN04_09.KeyPress, txtJKNKIN05_09.KeyPress _
                                                                                                                        , txtJKNKIN06_09.KeyPress, txtJKNKIN07_09.KeyPress, txtJKNKIN08_09.KeyPress, txtJKNKIN09_09.KeyPress, txtJKNKIN10_09.KeyPress _
                                                                                                                        , txtJKNKIN01_10.KeyPress, txtJKNKIN02_10.KeyPress, txtJKNKIN03_10.KeyPress, txtJKNKIN04_10.KeyPress, txtJKNKIN05_10.KeyPress _
                                                                                                                        , txtJKNKIN06_10.KeyPress, txtJKNKIN07_10.KeyPress, txtJKNKIN08_10.KeyPress, txtJKNKIN09_10.KeyPress, txtJKNKIN10_10.KeyPress _
                                                                                                                        , txtPOINT01.KeyPress, txtPOINT02.KeyPress, txtPOINT03.KeyPress, txtPOINT04.KeyPress, txtPOINT05.KeyPress _
                                                                                                                        , txtPOINT06.KeyPress, txtPOINT07.KeyPress, txtPOINT08.KeyPress, txtPOINT09.KeyPress, txtPOINT10.KeyPress _
                                                                                                                        , txtENDTIME01.KeyPress, txtENDTIME02.KeyPress, txtENDTIME03.KeyPress, txtENDTIME04.KeyPress, txtENDTIME05.KeyPress _
                                                                                                                        , txtENDTIME06.KeyPress, txtENDTIME07.KeyPress, txtENDTIME08.KeyPress, txtENDTIME09.KeyPress, txtENDTIME10.KeyPress _
                                                                                                                        , txtMAXBALLSU01.KeyPress, txtMAXBALLSU02.KeyPress, txtMAXBALLSU03.KeyPress, txtMAXBALLSU04.KeyPress, txtMAXBALLSU05.KeyPress _
                                                                                                                        , txtMAXBALLSU06.KeyPress, txtMAXBALLSU07.KeyPress, txtMAXBALLSU08.KeyPress, txtMAXBALLSU09.KeyPress, txtMAXBALLSU10.KeyPress



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
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtJKNMM01.MouseDown, txtJKNMM02.MouseDown, txtJKNMM03.MouseDown, txtJKNMM04.MouseDown, txtJKNMM05.MouseDown _
                                                                                                                        , txtJKNMM06.MouseDown, txtJKNMM07.MouseDown, txtJKNMM08.MouseDown, txtJKNMM09.MouseDown, txtJKNMM10.MouseDown _
                                                                                                                        , txtJKNKIN01_01.MouseDown, txtJKNKIN02_01.MouseDown, txtJKNKIN03_01.MouseDown, txtJKNKIN04_01.MouseDown, txtJKNKIN05_01.MouseDown _
                                                                                                                        , txtJKNKIN06_01.MouseDown, txtJKNKIN07_01.MouseDown, txtJKNKIN08_01.MouseDown, txtJKNKIN09_01.MouseDown, txtJKNKIN10_01.MouseDown _
                                                                                                                        , txtJKNKIN01_02.MouseDown, txtJKNKIN02_02.MouseDown, txtJKNKIN03_02.MouseDown, txtJKNKIN04_02.MouseDown, txtJKNKIN05_02.MouseDown _
                                                                                                                        , txtJKNKIN06_02.MouseDown, txtJKNKIN07_02.MouseDown, txtJKNKIN08_02.MouseDown, txtJKNKIN09_02.MouseDown, txtJKNKIN10_02.MouseDown _
                                                                                                                        , txtJKNKIN01_03.MouseDown, txtJKNKIN02_03.MouseDown, txtJKNKIN03_03.MouseDown, txtJKNKIN04_03.MouseDown, txtJKNKIN05_03.MouseDown _
                                                                                                                        , txtJKNKIN06_03.MouseDown, txtJKNKIN07_03.MouseDown, txtJKNKIN08_03.MouseDown, txtJKNKIN09_03.MouseDown, txtJKNKIN10_03.MouseDown _
                                                                                                                        , txtJKNKIN01_04.MouseDown, txtJKNKIN02_04.MouseDown, txtJKNKIN03_04.MouseDown, txtJKNKIN04_04.MouseDown, txtJKNKIN05_04.MouseDown _
                                                                                                                        , txtJKNKIN06_04.MouseDown, txtJKNKIN07_04.MouseDown, txtJKNKIN08_04.MouseDown, txtJKNKIN09_04.MouseDown, txtJKNKIN10_04.MouseDown _
                                                                                                                        , txtJKNKIN01_05.MouseDown, txtJKNKIN02_05.MouseDown, txtJKNKIN03_05.MouseDown, txtJKNKIN04_05.MouseDown, txtJKNKIN05_05.MouseDown _
                                                                                                                        , txtJKNKIN06_05.MouseDown, txtJKNKIN07_05.MouseDown, txtJKNKIN08_05.MouseDown, txtJKNKIN09_05.MouseDown, txtJKNKIN10_05.MouseDown _
                                                                                                                        , txtJKNKIN01_06.MouseDown, txtJKNKIN02_06.MouseDown, txtJKNKIN03_06.MouseDown, txtJKNKIN04_06.MouseDown, txtJKNKIN05_06.MouseDown _
                                                                                                                        , txtJKNKIN06_06.MouseDown, txtJKNKIN07_06.MouseDown, txtJKNKIN08_06.MouseDown, txtJKNKIN09_06.MouseDown, txtJKNKIN10_06.MouseDown _
                                                                                                                        , txtJKNKIN01_07.MouseDown, txtJKNKIN02_07.MouseDown, txtJKNKIN03_07.MouseDown, txtJKNKIN04_07.MouseDown, txtJKNKIN05_07.MouseDown _
                                                                                                                        , txtJKNKIN06_07.MouseDown, txtJKNKIN07_07.MouseDown, txtJKNKIN08_07.MouseDown, txtJKNKIN09_07.MouseDown, txtJKNKIN10_07.MouseDown _
                                                                                                                        , txtJKNKIN01_08.MouseDown, txtJKNKIN02_08.MouseDown, txtJKNKIN03_08.MouseDown, txtJKNKIN04_08.MouseDown, txtJKNKIN05_08.MouseDown _
                                                                                                                        , txtJKNKIN06_08.MouseDown, txtJKNKIN07_08.MouseDown, txtJKNKIN08_08.MouseDown, txtJKNKIN09_08.MouseDown, txtJKNKIN10_08.MouseDown _
                                                                                                                        , txtJKNKIN01_09.MouseDown, txtJKNKIN02_09.MouseDown, txtJKNKIN03_09.MouseDown, txtJKNKIN04_09.MouseDown, txtJKNKIN05_09.MouseDown _
                                                                                                                        , txtJKNKIN06_09.MouseDown, txtJKNKIN07_09.MouseDown, txtJKNKIN08_09.MouseDown, txtJKNKIN09_09.MouseDown, txtJKNKIN10_09.MouseDown _
                                                                                                                        , txtJKNKIN01_10.MouseDown, txtJKNKIN02_10.MouseDown, txtJKNKIN03_10.MouseDown, txtJKNKIN04_10.MouseDown, txtJKNKIN05_10.MouseDown _
                                                                                                                        , txtJKNKIN06_10.MouseDown, txtJKNKIN07_10.MouseDown, txtJKNKIN08_10.MouseDown, txtJKNKIN09_10.MouseDown, txtJKNKIN10_10.MouseDown _
                                                                                                                        , txtPOINT01.MouseDown, txtPOINT02.MouseDown, txtPOINT03.MouseDown, txtPOINT04.MouseDown, txtPOINT05.MouseDown _
                                                                                                                        , txtPOINT06.MouseDown, txtPOINT07.MouseDown, txtPOINT08.MouseDown, txtPOINT09.MouseDown, txtPOINT10.MouseDown _
                                                                                                                        , txtENDTIME01.MouseDown, txtENDTIME02.MouseDown, txtENDTIME03.MouseDown, txtENDTIME04.MouseDown, txtENDTIME05.MouseDown _
                                                                                                                        , txtENDTIME06.MouseDown, txtENDTIME07.MouseDown, txtENDTIME08.MouseDown, txtENDTIME09.MouseDown, txtENDTIME10.MouseDown _
                                                                                                                        , txtMAXBALLSU01.MouseDown, txtMAXBALLSU02.MouseDown, txtMAXBALLSU03.MouseDown, txtMAXBALLSU04.MouseDown, txtMAXBALLSU05.MouseDown _
                                                                                                                        , txtMAXBALLSU06.MouseDown, txtMAXBALLSU07.MouseDown, txtMAXBALLSU08.MouseDown, txtMAXBALLSU09.MouseDown, txtMAXBALLSU10.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace(",", String.Empty).Replace(":", String.Empty)

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
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtJKNMM01.Enter, txtJKNMM02.Enter, txtJKNMM03.Enter, txtJKNMM04.Enter, txtJKNMM05.Enter _
                                                                                   , txtJKNMM06.Enter, txtJKNMM07.Enter, txtJKNMM08.Enter, txtJKNMM09.Enter, txtJKNMM10.Enter _
                                                                                   , txtJKNKIN01_01.Enter, txtJKNKIN02_01.Enter, txtJKNKIN03_01.Enter, txtJKNKIN04_01.Enter, txtJKNKIN05_01.Enter _
                                                                                   , txtJKNKIN06_01.Enter, txtJKNKIN07_01.Enter, txtJKNKIN08_01.Enter, txtJKNKIN09_01.Enter, txtJKNKIN10_01.Enter _
                                                                                   , txtJKNKIN01_02.Enter, txtJKNKIN02_02.Enter, txtJKNKIN03_02.Enter, txtJKNKIN04_02.Enter, txtJKNKIN05_02.Enter _
                                                                                   , txtJKNKIN06_02.Enter, txtJKNKIN07_02.Enter, txtJKNKIN08_02.Enter, txtJKNKIN09_02.Enter, txtJKNKIN10_02.Enter _
                                                                                   , txtJKNKIN01_03.Enter, txtJKNKIN02_03.Enter, txtJKNKIN03_03.Enter, txtJKNKIN04_03.Enter, txtJKNKIN05_03.Enter _
                                                                                   , txtJKNKIN06_03.Enter, txtJKNKIN07_03.Enter, txtJKNKIN08_03.Enter, txtJKNKIN09_03.Enter, txtJKNKIN10_03.Enter _
                                                                                   , txtJKNKIN01_04.Enter, txtJKNKIN02_04.Enter, txtJKNKIN03_04.Enter, txtJKNKIN04_04.Enter, txtJKNKIN05_04.Enter _
                                                                                   , txtJKNKIN06_04.Enter, txtJKNKIN07_04.Enter, txtJKNKIN08_04.Enter, txtJKNKIN09_04.Enter, txtJKNKIN10_04.Enter _
                                                                                   , txtJKNKIN01_05.Enter, txtJKNKIN02_05.Enter, txtJKNKIN03_05.Enter, txtJKNKIN04_05.Enter, txtJKNKIN05_05.Enter _
                                                                                   , txtJKNKIN06_05.Enter, txtJKNKIN07_05.Enter, txtJKNKIN08_05.Enter, txtJKNKIN09_05.Enter, txtJKNKIN10_05.Enter _
                                                                                   , txtJKNKIN01_06.Enter, txtJKNKIN02_06.Enter, txtJKNKIN03_06.Enter, txtJKNKIN04_06.Enter, txtJKNKIN05_06.Enter _
                                                                                   , txtJKNKIN06_06.Enter, txtJKNKIN07_06.Enter, txtJKNKIN08_06.Enter, txtJKNKIN09_06.Enter, txtJKNKIN10_06.Enter _
                                                                                   , txtJKNKIN01_07.Enter, txtJKNKIN02_07.Enter, txtJKNKIN03_07.Enter, txtJKNKIN04_07.Enter, txtJKNKIN05_07.Enter _
                                                                                   , txtJKNKIN06_07.Enter, txtJKNKIN07_07.Enter, txtJKNKIN08_07.Enter, txtJKNKIN09_07.Enter, txtJKNKIN10_07.Enter _
                                                                                   , txtJKNKIN01_08.Enter, txtJKNKIN02_08.Enter, txtJKNKIN03_08.Enter, txtJKNKIN04_08.Enter, txtJKNKIN05_08.Enter _
                                                                                   , txtJKNKIN06_08.Enter, txtJKNKIN07_08.Enter, txtJKNKIN08_08.Enter, txtJKNKIN09_08.Enter, txtJKNKIN10_08.Enter _
                                                                                   , txtJKNKIN01_09.Enter, txtJKNKIN02_09.Enter, txtJKNKIN03_09.Enter, txtJKNKIN04_09.Enter, txtJKNKIN05_09.Enter _
                                                                                   , txtJKNKIN06_09.Enter, txtJKNKIN07_09.Enter, txtJKNKIN08_09.Enter, txtJKNKIN09_09.Enter, txtJKNKIN10_09.Enter _
                                                                                   , txtJKNKIN01_10.Enter, txtJKNKIN02_10.Enter, txtJKNKIN03_10.Enter, txtJKNKIN04_10.Enter, txtJKNKIN05_10.Enter _
                                                                                   , txtJKNKIN06_10.Enter, txtJKNKIN07_10.Enter, txtJKNKIN08_10.Enter, txtJKNKIN09_10.Enter, txtJKNKIN10_10.Enter _
                                                                                   , txtPOINT01.Enter, txtPOINT02.Enter, txtPOINT03.Enter, txtPOINT04.Enter, txtPOINT05.Enter _
                                                                                   , txtPOINT06.Enter, txtPOINT07.Enter, txtPOINT08.Enter, txtPOINT09.Enter, txtPOINT10.Enter _
                                                                                   , txtENDTIME01.Enter, txtENDTIME02.Enter, txtENDTIME03.Enter, txtENDTIME04.Enter, txtENDTIME05.Enter _
                                                                                   , txtENDTIME06.Enter, txtENDTIME07.Enter, txtENDTIME08.Enter, txtENDTIME09.Enter, txtENDTIME10.Enter _
                                                                                   , txtMAXBALLSU01.Enter, txtMAXBALLSU02.Enter, txtMAXBALLSU03.Enter, txtMAXBALLSU04.Enter, txtMAXBALLSU05.Enter _
                                                                                   , txtMAXBALLSU06.Enter, txtMAXBALLSU07.Enter, txtMAXBALLSU08.Enter, txtMAXBALLSU09.Enter, txtMAXBALLSU10.Enter

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace(",", String.Empty).Replace(":", String.Empty)
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



            '打ち放題マスタ情報取得
            GetEIGMTB()

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
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM EIGMTB WHERE RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1) & " ORDER BY RKNKB,JKNKB")
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
                Using frm As New frmMSGBOX01("打ち放題情報の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            '画面初期設定
            Init()

            '打ち放題マスタ情報取得
            If Not GetEIGMTB() Then
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

            '顧客種別
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

            '貸し時間
            Me.txtJKNMM01.Text = String.Empty
            Me.txtJKNMM02.Text = String.Empty
            Me.txtJKNMM03.Text = String.Empty
            Me.txtJKNMM04.Text = String.Empty
            Me.txtJKNMM05.Text = String.Empty
            Me.txtJKNMM06.Text = String.Empty
            Me.txtJKNMM07.Text = String.Empty
            Me.txtJKNMM08.Text = String.Empty
            Me.txtJKNMM09.Text = String.Empty
            Me.txtJKNMM10.Text = String.Empty
            '貸し時間名称
            Me.txtJKNNAME01.Text = String.Empty
            Me.txtJKNNAME02.Text = String.Empty
            Me.txtJKNNAME03.Text = String.Empty
            Me.txtJKNNAME04.Text = String.Empty
            Me.txtJKNNAME05.Text = String.Empty
            Me.txtJKNNAME06.Text = String.Empty
            Me.txtJKNNAME07.Text = String.Empty
            Me.txtJKNNAME08.Text = String.Empty
            Me.txtJKNNAME09.Text = String.Empty
            Me.txtJKNNAME10.Text = String.Empty
            '単価
            Me.txtJKNKIN01_01.Text = String.Empty
            Me.txtJKNKIN02_01.Text = String.Empty
            Me.txtJKNKIN03_01.Text = String.Empty
            Me.txtJKNKIN04_01.Text = String.Empty
            Me.txtJKNKIN05_01.Text = String.Empty
            Me.txtJKNKIN06_01.Text = String.Empty
            Me.txtJKNKIN07_01.Text = String.Empty
            Me.txtJKNKIN08_01.Text = String.Empty
            Me.txtJKNKIN09_01.Text = String.Empty
            Me.txtJKNKIN10_01.Text = String.Empty

            Me.txtJKNKIN01_02.Text = String.Empty
            Me.txtJKNKIN02_02.Text = String.Empty
            Me.txtJKNKIN03_02.Text = String.Empty
            Me.txtJKNKIN04_02.Text = String.Empty
            Me.txtJKNKIN05_02.Text = String.Empty
            Me.txtJKNKIN06_02.Text = String.Empty
            Me.txtJKNKIN07_02.Text = String.Empty
            Me.txtJKNKIN08_02.Text = String.Empty
            Me.txtJKNKIN09_02.Text = String.Empty
            Me.txtJKNKIN10_02.Text = String.Empty

            Me.txtJKNKIN01_03.Text = String.Empty
            Me.txtJKNKIN02_03.Text = String.Empty
            Me.txtJKNKIN03_03.Text = String.Empty
            Me.txtJKNKIN04_03.Text = String.Empty
            Me.txtJKNKIN05_03.Text = String.Empty
            Me.txtJKNKIN06_03.Text = String.Empty
            Me.txtJKNKIN07_03.Text = String.Empty
            Me.txtJKNKIN08_03.Text = String.Empty
            Me.txtJKNKIN09_03.Text = String.Empty
            Me.txtJKNKIN10_03.Text = String.Empty

            Me.txtJKNKIN01_04.Text = String.Empty
            Me.txtJKNKIN02_04.Text = String.Empty
            Me.txtJKNKIN03_04.Text = String.Empty
            Me.txtJKNKIN04_04.Text = String.Empty
            Me.txtJKNKIN05_04.Text = String.Empty
            Me.txtJKNKIN06_04.Text = String.Empty
            Me.txtJKNKIN07_04.Text = String.Empty
            Me.txtJKNKIN08_04.Text = String.Empty
            Me.txtJKNKIN09_04.Text = String.Empty
            Me.txtJKNKIN10_04.Text = String.Empty

            Me.txtJKNKIN01_05.Text = String.Empty
            Me.txtJKNKIN02_05.Text = String.Empty
            Me.txtJKNKIN03_05.Text = String.Empty
            Me.txtJKNKIN04_05.Text = String.Empty
            Me.txtJKNKIN05_05.Text = String.Empty
            Me.txtJKNKIN06_05.Text = String.Empty
            Me.txtJKNKIN07_05.Text = String.Empty
            Me.txtJKNKIN08_05.Text = String.Empty
            Me.txtJKNKIN09_05.Text = String.Empty
            Me.txtJKNKIN10_05.Text = String.Empty

            Me.txtJKNKIN01_06.Text = String.Empty
            Me.txtJKNKIN02_06.Text = String.Empty
            Me.txtJKNKIN03_06.Text = String.Empty
            Me.txtJKNKIN04_06.Text = String.Empty
            Me.txtJKNKIN05_06.Text = String.Empty
            Me.txtJKNKIN06_06.Text = String.Empty
            Me.txtJKNKIN07_06.Text = String.Empty
            Me.txtJKNKIN08_06.Text = String.Empty
            Me.txtJKNKIN09_06.Text = String.Empty
            Me.txtJKNKIN10_06.Text = String.Empty

            Me.txtJKNKIN01_07.Text = String.Empty
            Me.txtJKNKIN02_07.Text = String.Empty
            Me.txtJKNKIN03_07.Text = String.Empty
            Me.txtJKNKIN04_07.Text = String.Empty
            Me.txtJKNKIN05_07.Text = String.Empty
            Me.txtJKNKIN06_07.Text = String.Empty
            Me.txtJKNKIN07_07.Text = String.Empty
            Me.txtJKNKIN08_07.Text = String.Empty
            Me.txtJKNKIN09_07.Text = String.Empty
            Me.txtJKNKIN10_07.Text = String.Empty

            Me.txtJKNKIN01_08.Text = String.Empty
            Me.txtJKNKIN02_08.Text = String.Empty
            Me.txtJKNKIN03_08.Text = String.Empty
            Me.txtJKNKIN04_08.Text = String.Empty
            Me.txtJKNKIN05_08.Text = String.Empty
            Me.txtJKNKIN06_08.Text = String.Empty
            Me.txtJKNKIN07_08.Text = String.Empty
            Me.txtJKNKIN08_08.Text = String.Empty
            Me.txtJKNKIN09_08.Text = String.Empty
            Me.txtJKNKIN10_08.Text = String.Empty

            Me.txtJKNKIN01_09.Text = String.Empty
            Me.txtJKNKIN02_09.Text = String.Empty
            Me.txtJKNKIN03_09.Text = String.Empty
            Me.txtJKNKIN04_09.Text = String.Empty
            Me.txtJKNKIN05_09.Text = String.Empty
            Me.txtJKNKIN06_09.Text = String.Empty
            Me.txtJKNKIN07_09.Text = String.Empty
            Me.txtJKNKIN08_09.Text = String.Empty
            Me.txtJKNKIN09_09.Text = String.Empty
            Me.txtJKNKIN10_09.Text = String.Empty

            Me.txtJKNKIN01_10.Text = String.Empty
            Me.txtJKNKIN02_10.Text = String.Empty
            Me.txtJKNKIN03_10.Text = String.Empty
            Me.txtJKNKIN04_10.Text = String.Empty
            Me.txtJKNKIN05_10.Text = String.Empty
            Me.txtJKNKIN06_10.Text = String.Empty
            Me.txtJKNKIN07_10.Text = String.Empty
            Me.txtJKNKIN08_10.Text = String.Empty
            Me.txtJKNKIN09_10.Text = String.Empty
            Me.txtJKNKIN10_10.Text = String.Empty

            'ポイント
            Me.txtPOINT01.Text = String.Empty
            Me.txtPOINT02.Text = String.Empty
            Me.txtPOINT03.Text = String.Empty
            Me.txtPOINT04.Text = String.Empty
            Me.txtPOINT05.Text = String.Empty
            Me.txtPOINT06.Text = String.Empty
            Me.txtPOINT07.Text = String.Empty
            Me.txtPOINT08.Text = String.Empty
            Me.txtPOINT09.Text = String.Empty
            Me.txtPOINT10.Text = String.Empty
            '終了時間
            Me.txtENDTIME01.Text = String.Empty
            Me.txtENDTIME02.Text = String.Empty
            Me.txtENDTIME03.Text = String.Empty
            Me.txtENDTIME04.Text = String.Empty
            Me.txtENDTIME05.Text = String.Empty
            Me.txtENDTIME06.Text = String.Empty
            Me.txtENDTIME07.Text = String.Empty
            Me.txtENDTIME08.Text = String.Empty
            Me.txtENDTIME09.Text = String.Empty
            Me.txtENDTIME10.Text = String.Empty
            'フロア指定
            Me.cmbJKNFLR01.SelectedIndex = 0
            Me.cmbJKNFLR02.SelectedIndex = 0
            Me.cmbJKNFLR03.SelectedIndex = 0
            Me.cmbJKNFLR04.SelectedIndex = 0
            Me.cmbJKNFLR05.SelectedIndex = 0
            Me.cmbJKNFLR06.SelectedIndex = 0
            Me.cmbJKNFLR07.SelectedIndex = 0
            Me.cmbJKNFLR08.SelectedIndex = 0
            Me.cmbJKNFLR09.SelectedIndex = 0
            Me.cmbJKNFLR10.SelectedIndex = 0
            '利用限度球数
            Me.txtMAXBALLSU01.Text = String.Empty
            Me.txtMAXBALLSU02.Text = String.Empty
            Me.txtMAXBALLSU03.Text = String.Empty
            Me.txtMAXBALLSU04.Text = String.Empty
            Me.txtMAXBALLSU05.Text = String.Empty
            Me.txtMAXBALLSU06.Text = String.Empty
            Me.txtMAXBALLSU07.Text = String.Empty
            Me.txtMAXBALLSU08.Text = String.Empty
            Me.txtMAXBALLSU09.Text = String.Empty
            Me.txtMAXBALLSU10.Text = String.Empty
            If UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                Me.txtMAXBALLSU01.ForeColor = Color.DimGray
                Me.txtMAXBALLSU02.ForeColor = Color.DimGray
                Me.txtMAXBALLSU03.ForeColor = Color.DimGray
                Me.txtMAXBALLSU04.ForeColor = Color.DimGray
                Me.txtMAXBALLSU05.ForeColor = Color.DimGray
                Me.txtMAXBALLSU06.ForeColor = Color.DimGray
                Me.txtMAXBALLSU07.ForeColor = Color.DimGray
                Me.txtMAXBALLSU08.ForeColor = Color.DimGray
                Me.txtMAXBALLSU09.ForeColor = Color.DimGray
                Me.txtMAXBALLSU10.ForeColor = Color.DimGray

                Me.txtMAXBALLSU01.ReadOnly = True
                Me.txtMAXBALLSU02.ReadOnly = True
                Me.txtMAXBALLSU03.ReadOnly = True
                Me.txtMAXBALLSU04.ReadOnly = True
                Me.txtMAXBALLSU05.ReadOnly = True
                Me.txtMAXBALLSU06.ReadOnly = True
                Me.txtMAXBALLSU07.ReadOnly = True
                Me.txtMAXBALLSU08.ReadOnly = True
                Me.txtMAXBALLSU09.ReadOnly = True
                Me.txtMAXBALLSU10.ReadOnly = True
            End If

            '延長区分
            Me.chkCHARGEFLG01.Checked = False
            Me.chkCHARGEFLG02.Checked = False
            Me.chkCHARGEFLG03.Checked = False
            Me.chkCHARGEFLG04.Checked = False
            Me.chkCHARGEFLG05.Checked = False
            Me.chkCHARGEFLG06.Checked = False
            Me.chkCHARGEFLG07.Checked = False
            Me.chkCHARGEFLG08.Checked = False
            Me.chkCHARGEFLG09.Checked = False
            Me.chkCHARGEFLG10.Checked = False

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' システム情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSYSMTA() As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(",TO_CHAR(UPDDTM,'YYYYMMDD') AS UPDDAY")
            sql.Append(" FROM SYSMTA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            '時間貸し上限打球数
            UIUtility.SYSTEM.MAXJDCNT = CType(resultDt.Rows(0).Item("MAXJDCNT").ToString(), Integer)
            'システム更新日時
            UIUtility.SYSTEM.UPDDTM = resultDt.Rows(0).Item("UPDDAY").ToString()

            Return True

        Catch ex As Exception
            Throw ex
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
            sql.Append(" WHERE")
            sql.Append(" NKBNO <= 10")
            sql.Append(" ORDER BY NKBNO ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Select Case resultDt.Rows(i).Item("NKBNO").ToString
                    Case "1" : Me.lblCKBNAME01.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "2" : Me.lblCKBNAME02.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "3" : Me.lblCKBNAME03.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "4" : Me.lblCKBNAME04.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "5" : Me.lblCKBNAME05.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "6" : Me.lblCKBNAME06.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "7" : Me.lblCKBNAME07.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "8" : Me.lblCKBNAME08.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "9" : Me.lblCKBNAME09.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                    Case "10" : Me.lblCKBNAME10.Text = resultDt.Rows(i).Item("CKBNAME").ToString
                End Select
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
                Me.cmbRKNKB.Items.Add(resultDt.Rows(i).Item("RKNNM").ToString)
                Me.cmbCopyRKNKB.Items.Add(resultDt.Rows(i).Item("RKNNM").ToString)
                intCLRR = CType(resultDt.Rows(i).Item("CLRR").ToString, Integer)
                intCLRG = CType(resultDt.Rows(i).Item("CLRG").ToString, Integer)
                intCLRB = CType(resultDt.Rows(i).Item("CLRB").ToString, Integer)
            Next

            Me.cmbRKNKB.SelectedIndex = 0
            Me.cmbCopyRKNKB.SelectedIndex = 0

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 打ち放題マスタ情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEIGMTB(Optional CopyFlg As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            '上限打球数
            Me.txtMAXJDCNT.Text = UIUtility.SYSTEM.MAXJDCNT.ToString

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM EIGMTB")
            sql.Append(" WHERE")
            If CopyFlg Then
                sql.Append(" RKNKB = " & (Me.cmbCopyRKNKB.SelectedIndex + 1))
            Else
                sql.Append(" RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1))
            End If
            sql.Append(" ORDER BY RKNKB,JKNKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            '最新の更新日時取得
            If Not CopyFlg Then _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Dim txtJKNKIN As TextBox = Nothing
            Dim dr() As DataRow

            For j As Integer = 1 To 10
                dr = resultDt.Select("NKBNO = " & j)
                If dr.Length > 0 Then
                    For i As Integer = 0 To dr.Length - 1

                        '単価
                        If j.Equals(1) And i.Equals(0) Then
                            Me.txtJKNKIN01_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(1) Then
                            Me.txtJKNKIN02_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(2) Then
                            Me.txtJKNKIN03_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(3) Then
                            Me.txtJKNKIN04_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(4) Then
                            Me.txtJKNKIN05_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(5) Then
                            Me.txtJKNKIN06_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(6) Then
                            Me.txtJKNKIN07_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(7) Then
                            Me.txtJKNKIN08_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(8) Then
                            Me.txtJKNKIN09_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(1) And i.Equals(9) Then
                            Me.txtJKNKIN10_01.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(2) And i.Equals(0) Then
                            Me.txtJKNKIN01_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(1) Then
                            Me.txtJKNKIN02_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(2) Then
                            Me.txtJKNKIN03_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(3) Then
                            Me.txtJKNKIN04_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(4) Then
                            Me.txtJKNKIN05_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(5) Then
                            Me.txtJKNKIN06_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(6) Then
                            Me.txtJKNKIN07_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(7) Then
                            Me.txtJKNKIN08_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(8) Then
                            Me.txtJKNKIN09_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(2) And i.Equals(9) Then
                            Me.txtJKNKIN10_02.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(3) And i.Equals(0) Then
                            Me.txtJKNKIN01_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(1) Then
                            Me.txtJKNKIN02_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(2) Then
                            Me.txtJKNKIN03_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(3) Then
                            Me.txtJKNKIN04_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(4) Then
                            Me.txtJKNKIN05_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(5) Then
                            Me.txtJKNKIN06_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(6) Then
                            Me.txtJKNKIN07_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(7) Then
                            Me.txtJKNKIN08_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(8) Then
                            Me.txtJKNKIN09_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(3) And i.Equals(9) Then
                            Me.txtJKNKIN10_03.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(4) And i.Equals(0) Then
                            Me.txtJKNKIN01_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(1) Then
                            Me.txtJKNKIN02_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(2) Then
                            Me.txtJKNKIN03_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(3) Then
                            Me.txtJKNKIN04_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(4) Then
                            Me.txtJKNKIN05_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(5) Then
                            Me.txtJKNKIN06_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(6) Then
                            Me.txtJKNKIN07_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(7) Then
                            Me.txtJKNKIN08_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(8) Then
                            Me.txtJKNKIN09_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(4) And i.Equals(9) Then
                            Me.txtJKNKIN10_04.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(5) And i.Equals(0) Then
                            Me.txtJKNKIN01_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(1) Then
                            Me.txtJKNKIN02_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(2) Then
                            Me.txtJKNKIN03_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(3) Then
                            Me.txtJKNKIN04_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(4) Then
                            Me.txtJKNKIN05_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(5) Then
                            Me.txtJKNKIN06_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(6) Then
                            Me.txtJKNKIN07_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(7) Then
                            Me.txtJKNKIN08_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(8) Then
                            Me.txtJKNKIN09_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(5) And i.Equals(9) Then
                            Me.txtJKNKIN10_05.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(6) And i.Equals(0) Then
                            Me.txtJKNKIN01_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(1) Then
                            Me.txtJKNKIN02_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(2) Then
                            Me.txtJKNKIN03_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(3) Then
                            Me.txtJKNKIN04_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(4) Then
                            Me.txtJKNKIN05_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(5) Then
                            Me.txtJKNKIN06_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(6) Then
                            Me.txtJKNKIN07_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(7) Then
                            Me.txtJKNKIN08_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(8) Then
                            Me.txtJKNKIN09_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(6) And i.Equals(9) Then
                            Me.txtJKNKIN10_06.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(7) And i.Equals(0) Then
                            Me.txtJKNKIN01_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(1) Then
                            Me.txtJKNKIN02_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(2) Then
                            Me.txtJKNKIN03_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(3) Then
                            Me.txtJKNKIN04_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(4) Then
                            Me.txtJKNKIN05_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(5) Then
                            Me.txtJKNKIN06_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(6) Then
                            Me.txtJKNKIN07_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(7) Then
                            Me.txtJKNKIN08_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(8) Then
                            Me.txtJKNKIN09_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(7) And i.Equals(9) Then
                            Me.txtJKNKIN10_07.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(8) And i.Equals(0) Then
                            Me.txtJKNKIN01_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(1) Then
                            Me.txtJKNKIN02_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(2) Then
                            Me.txtJKNKIN03_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(3) Then
                            Me.txtJKNKIN04_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(4) Then
                            Me.txtJKNKIN05_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(5) Then
                            Me.txtJKNKIN06_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(6) Then
                            Me.txtJKNKIN07_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(7) Then
                            Me.txtJKNKIN08_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(8) Then
                            Me.txtJKNKIN09_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(8) And i.Equals(9) Then
                            Me.txtJKNKIN10_08.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(9) And i.Equals(0) Then
                            Me.txtJKNKIN01_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(1) Then
                            Me.txtJKNKIN02_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(2) Then
                            Me.txtJKNKIN03_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(3) Then
                            Me.txtJKNKIN04_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(4) Then
                            Me.txtJKNKIN05_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(5) Then
                            Me.txtJKNKIN06_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(6) Then
                            Me.txtJKNKIN07_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(7) Then
                            Me.txtJKNKIN08_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(8) Then
                            Me.txtJKNKIN09_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(9) And i.Equals(9) Then
                            Me.txtJKNKIN10_09.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        If j.Equals(10) And i.Equals(0) Then
                            Me.txtJKNKIN01_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(1) Then
                            Me.txtJKNKIN02_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(2) Then
                            Me.txtJKNKIN03_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(3) Then
                            Me.txtJKNKIN04_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(4) Then
                            Me.txtJKNKIN05_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(5) Then
                            Me.txtJKNKIN06_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(6) Then
                            Me.txtJKNKIN07_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(7) Then
                            Me.txtJKNKIN08_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(8) Then
                            Me.txtJKNKIN09_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        ElseIf j.Equals(10) And i.Equals(9) Then
                            Me.txtJKNKIN10_10.Text = CType(dr(i).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                        End If

                        Select Case i
                            Case 0
                                Me.txtJKNMM01.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME01.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT01.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME01.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR01.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU01.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG01.Checked = False
                                Else
                                    Me.chkCHARGEFLG01.Checked = True
                                End If
                            Case 1
                                Me.txtJKNMM02.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME02.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT02.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME02.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR02.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU02.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG02.Checked = False
                                Else
                                    Me.chkCHARGEFLG02.Checked = True
                                End If
                            Case 2
                                Me.txtJKNMM03.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME03.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT03.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME03.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR03.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU03.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG03.Checked = False
                                Else
                                    Me.chkCHARGEFLG03.Checked = True
                                End If
                            Case 3
                                Me.txtJKNMM04.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME04.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT04.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME04.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR04.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU04.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG04.Checked = False
                                Else
                                    Me.chkCHARGEFLG04.Checked = True
                                End If
                            Case 4
                                Me.txtJKNMM05.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME05.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT05.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME05.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR05.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU05.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG05.Checked = False
                                Else
                                    Me.chkCHARGEFLG05.Checked = True
                                End If
                            Case 5
                                Me.txtJKNMM06.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME06.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT06.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME06.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR06.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU06.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG06.Checked = False
                                Else
                                    Me.chkCHARGEFLG06.Checked = True
                                End If
                            Case 6
                                Me.txtJKNMM07.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME07.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT07.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME07.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR07.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU07.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG07.Checked = False
                                Else
                                    Me.chkCHARGEFLG07.Checked = True
                                End If
                            Case 7
                                Me.txtJKNMM08.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME08.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT08.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME08.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR08.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU08.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG08.Checked = False
                                Else
                                    Me.chkCHARGEFLG08.Checked = True
                                End If
                            Case 8
                                Me.txtJKNMM09.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME09.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT09.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME09.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR09.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU09.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG09.Checked = False
                                Else
                                    Me.chkCHARGEFLG09.Checked = True
                                End If
                            Case 9
                                Me.txtJKNMM10.Text = dr(i).Item("JKNMM").ToString                                                                               '貸し時間
                                Me.txtJKNNAME10.Text = dr(i).Item("JKNNAME").ToString                                                                           '名称
                                Me.txtPOINT10.Text = CType(dr(i).Item("POINT").ToString, Integer).ToString("#,##0")                                             'ポイント
                                Me.txtENDTIME10.Text = dr(i).Item("ENDTIME").ToString.Substring(0, 2) & ":" & dr(i).Item("ENDTIME").ToString.Substring(2, 2)    '終了時間
                                Me.cmbJKNFLR10.SelectedIndex = CType(dr(i).Item("JKNFLR").ToString, Integer)                                                    'フロア指定
                                Me.txtMAXBALLSU10.Text = CType(dr(i).Item("MAXBALLSU").ToString, Integer).ToString("#,##0")                                     '最大利用球数
                                If CType(dr(i).Item("CHARGEFLG").ToString, Integer).Equals(0) Then                                                              '延長フラグ
                                    Me.chkCHARGEFLG10.Checked = False
                                Else
                                    Me.chkCHARGEFLG10.Checked = True
                                End If
                        End Select

                    Next
                End If
            Next





            ChangeMaxBallsu()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 利用限度球数計算
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeMaxBallsu()
        Dim intMAXJDCNT As Integer = 0
        Try
            If Not UIUtility.SYSTEM.SYSTEMMODE.Equals(0) Then
                Return
            End If

            If Not String.IsNullOrEmpty(Me.txtMAXJDCNT.Text) Then
                intMAXJDCNT = CType(Me.txtMAXJDCNT.Text, Integer)
            End If

            Me.txtMAXBALLSU01.Text = String.Empty
            Me.txtMAXBALLSU02.Text = String.Empty
            Me.txtMAXBALLSU03.Text = String.Empty
            Me.txtMAXBALLSU04.Text = String.Empty
            Me.txtMAXBALLSU05.Text = String.Empty
            Me.txtMAXBALLSU06.Text = String.Empty
            Me.txtMAXBALLSU07.Text = String.Empty
            Me.txtMAXBALLSU08.Text = String.Empty
            Me.txtMAXBALLSU09.Text = String.Empty
            Me.txtMAXBALLSU10.Text = String.Empty

            If Not String.IsNullOrEmpty(Me.txtJKNMM01.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU01.Text = "99,999"
                Else
                    Me.txtMAXBALLSU01.Text = (CType(Me.txtJKNMM01.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM02.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU02.Text = "99,999"
                Else
                    Me.txtMAXBALLSU02.Text = (CType(Me.txtJKNMM02.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM03.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU03.Text = "99,999"
                Else
                    Me.txtMAXBALLSU03.Text = (CType(Me.txtJKNMM03.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM04.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU04.Text = "99,999"
                Else
                    Me.txtMAXBALLSU04.Text = (CType(Me.txtJKNMM04.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM05.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU05.Text = "99,999"
                Else
                    Me.txtMAXBALLSU05.Text = (CType(Me.txtJKNMM05.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM06.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU06.Text = "99,999"
                Else
                    Me.txtMAXBALLSU06.Text = (CType(Me.txtJKNMM06.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM07.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU07.Text = "99,999"
                Else
                    Me.txtMAXBALLSU07.Text = (CType(Me.txtJKNMM07.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM08.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU08.Text = "99,999"
                Else
                    Me.txtMAXBALLSU08.Text = (CType(Me.txtJKNMM08.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM09.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU09.Text = "99,999"
                Else
                    Me.txtMAXBALLSU09.Text = (CType(Me.txtJKNMM09.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtJKNMM10.Text) Then
                If intMAXJDCNT.Equals(0) Then
                    Me.txtMAXBALLSU10.Text = "99,999"
                Else
                    Me.txtMAXBALLSU10.Text = (CType(Me.txtJKNMM10.Text, Integer) * intMAXJDCNT).ToString("#,##0")
                End If
            End If

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
            Dim JKNMM As TextBox = Nothing
            Dim JKNNAME As TextBox = Nothing
            Dim JKNKIN As TextBox = Nothing
            Dim POINT As TextBox = Nothing
            Dim ENDTIME As TextBox = Nothing
            Dim JKNFLR As ComboBox = Nothing
            Dim MAXBALLSU As TextBox = Nothing

            For j As Integer = 1 To 10
                For i As Integer = 1 To 10
                    '単価
                    If j.Equals(1) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_01
                    ElseIf j.Equals(1) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_01
                    ElseIf j.Equals(1) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_01
                    ElseIf j.Equals(1) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_01
                    ElseIf j.Equals(1) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_01
                    ElseIf j.Equals(1) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_01
                    ElseIf j.Equals(1) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_01
                    ElseIf j.Equals(1) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_01
                    ElseIf j.Equals(1) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_01
                    ElseIf j.Equals(1) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_01
                    End If

                    If j.Equals(2) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_02
                    ElseIf j.Equals(2) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_02
                    ElseIf j.Equals(2) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_02
                    ElseIf j.Equals(2) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_02
                    ElseIf j.Equals(2) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_02
                    ElseIf j.Equals(2) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_02
                    ElseIf j.Equals(2) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_02
                    ElseIf j.Equals(2) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_02
                    ElseIf j.Equals(2) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_02
                    ElseIf j.Equals(2) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_02
                    End If

                    If j.Equals(3) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_03
                    ElseIf j.Equals(3) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_03
                    ElseIf j.Equals(3) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_03
                    ElseIf j.Equals(3) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_03
                    ElseIf j.Equals(3) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_03
                    ElseIf j.Equals(3) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_03
                    ElseIf j.Equals(3) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_03
                    ElseIf j.Equals(3) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_03
                    ElseIf j.Equals(3) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_03
                    ElseIf j.Equals(3) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_03
                    End If

                    If j.Equals(4) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_04
                    ElseIf j.Equals(4) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_04
                    ElseIf j.Equals(4) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_04
                    ElseIf j.Equals(4) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_04
                    ElseIf j.Equals(4) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_04
                    ElseIf j.Equals(4) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_04
                    ElseIf j.Equals(4) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_04
                    ElseIf j.Equals(4) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_04
                    ElseIf j.Equals(4) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN09_04
                    ElseIf j.Equals(4) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_04
                    End If

                    If j.Equals(5) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_05
                    ElseIf j.Equals(5) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_05
                    ElseIf j.Equals(5) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_05
                    ElseIf j.Equals(5) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_05
                    ElseIf j.Equals(5) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_05
                    ElseIf j.Equals(5) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_05
                    ElseIf j.Equals(5) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_05
                    ElseIf j.Equals(5) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_05
                    ElseIf j.Equals(5) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_05
                    ElseIf j.Equals(5) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_05
                    End If

                    If j.Equals(6) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_06
                    ElseIf j.Equals(6) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_06
                    ElseIf j.Equals(6) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_06
                    ElseIf j.Equals(6) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_06
                    ElseIf j.Equals(6) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_06
                    ElseIf j.Equals(6) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_06
                    ElseIf j.Equals(6) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_06
                    ElseIf j.Equals(6) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_06
                    ElseIf j.Equals(6) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_06
                    ElseIf j.Equals(6) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_06
                    End If

                    If j.Equals(7) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_07
                    ElseIf j.Equals(7) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_07
                    ElseIf j.Equals(7) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_07
                    ElseIf j.Equals(7) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_07
                    ElseIf j.Equals(7) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_07
                    ElseIf j.Equals(7) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_07
                    ElseIf j.Equals(7) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_07
                    ElseIf j.Equals(7) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_07
                    ElseIf j.Equals(7) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_07
                    ElseIf j.Equals(7) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_07
                    End If

                    If j.Equals(8) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_08
                    ElseIf j.Equals(8) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_08
                    ElseIf j.Equals(8) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_08
                    ElseIf j.Equals(8) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_08
                    ElseIf j.Equals(8) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_08
                    ElseIf j.Equals(8) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_08
                    ElseIf j.Equals(8) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_08
                    ElseIf j.Equals(8) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_08
                    ElseIf j.Equals(8) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_08
                    ElseIf j.Equals(8) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_08
                    End If

                    If j.Equals(9) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_09
                    ElseIf j.Equals(9) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_09
                    ElseIf j.Equals(9) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_09
                    ElseIf j.Equals(9) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_09
                    ElseIf j.Equals(9) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_09
                    ElseIf j.Equals(9) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_09
                    ElseIf j.Equals(9) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_09
                    ElseIf j.Equals(9) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_09
                    ElseIf j.Equals(9) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_09
                    ElseIf j.Equals(9) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_09
                    End If

                    If j.Equals(10) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_10
                    ElseIf j.Equals(10) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_10
                    ElseIf j.Equals(10) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_10
                    ElseIf j.Equals(10) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_10
                    ElseIf j.Equals(10) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_10
                    ElseIf j.Equals(10) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_10
                    ElseIf j.Equals(10) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_10
                    ElseIf j.Equals(10) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_10
                    ElseIf j.Equals(10) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_10
                    ElseIf j.Equals(10) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_10
                    End If

                    Select Case i
                        Case 1
                            If String.IsNullOrEmpty(Me.txtJKNMM01.Text) Then Continue For
                            JKNMM = Me.txtJKNMM01
                            JKNNAME = Me.txtJKNNAME01
                            POINT = Me.txtPOINT01
                            ENDTIME = Me.txtENDTIME01
                            JKNFLR = Me.cmbJKNFLR01
                            MAXBALLSU = Me.txtMAXBALLSU01
                        Case 2
                            If String.IsNullOrEmpty(Me.txtJKNMM02.Text) Then Continue For
                            JKNMM = Me.txtJKNMM02
                            JKNNAME = Me.txtJKNNAME02
                            POINT = Me.txtPOINT02
                            ENDTIME = Me.txtENDTIME02
                            JKNFLR = Me.cmbJKNFLR02
                            MAXBALLSU = Me.txtMAXBALLSU02
                        Case 3
                            If String.IsNullOrEmpty(Me.txtJKNMM03.Text) Then Continue For
                            JKNMM = Me.txtJKNMM03
                            JKNNAME = Me.txtJKNNAME03
                            POINT = Me.txtPOINT03
                            ENDTIME = Me.txtENDTIME03
                            JKNFLR = Me.cmbJKNFLR03
                            MAXBALLSU = Me.txtMAXBALLSU03
                        Case 4
                            If String.IsNullOrEmpty(Me.txtJKNMM04.Text) Then Continue For
                            JKNMM = Me.txtJKNMM04
                            JKNNAME = Me.txtJKNNAME04
                            POINT = Me.txtPOINT04
                            ENDTIME = Me.txtENDTIME04
                            JKNFLR = Me.cmbJKNFLR04
                            MAXBALLSU = Me.txtMAXBALLSU04
                        Case 5
                            If String.IsNullOrEmpty(Me.txtJKNMM05.Text) Then Continue For
                            JKNMM = Me.txtJKNMM05
                            JKNNAME = Me.txtJKNNAME05
                            POINT = Me.txtPOINT05
                            ENDTIME = Me.txtENDTIME05
                            JKNFLR = Me.cmbJKNFLR05
                            MAXBALLSU = Me.txtMAXBALLSU05
                        Case 6
                            If String.IsNullOrEmpty(Me.txtJKNMM06.Text) Then Continue For
                            JKNMM = Me.txtJKNMM06
                            JKNNAME = Me.txtJKNNAME06
                            POINT = Me.txtPOINT06
                            ENDTIME = Me.txtENDTIME06
                            JKNFLR = Me.cmbJKNFLR06
                            MAXBALLSU = Me.txtMAXBALLSU06
                        Case 7
                            If String.IsNullOrEmpty(Me.txtJKNMM07.Text) Then Continue For
                            JKNMM = Me.txtJKNMM07
                            JKNNAME = Me.txtJKNNAME07
                            POINT = Me.txtPOINT07
                            ENDTIME = Me.txtENDTIME07
                            JKNFLR = Me.cmbJKNFLR07
                            MAXBALLSU = Me.txtMAXBALLSU07
                        Case 8
                            If String.IsNullOrEmpty(Me.txtJKNMM08.Text) Then Continue For
                            JKNMM = Me.txtJKNMM08
                            JKNNAME = Me.txtJKNNAME08
                            POINT = Me.txtPOINT08
                            ENDTIME = Me.txtENDTIME08
                            JKNFLR = Me.cmbJKNFLR08
                            MAXBALLSU = Me.txtMAXBALLSU08
                        Case 9
                            If String.IsNullOrEmpty(Me.txtJKNMM09.Text) Then Continue For
                            JKNMM = Me.txtJKNMM09
                            JKNNAME = Me.txtJKNNAME09
                            POINT = Me.txtPOINT09
                            ENDTIME = Me.txtENDTIME09
                            JKNFLR = Me.cmbJKNFLR09
                            MAXBALLSU = Me.txtMAXBALLSU09
                        Case 10
                            If String.IsNullOrEmpty(Me.txtJKNMM10.Text) Then Continue For
                            JKNMM = Me.txtJKNMM10
                            JKNNAME = Me.txtJKNNAME10
                            POINT = Me.txtPOINT10
                            ENDTIME = Me.txtENDTIME10
                            JKNFLR = Me.cmbJKNFLR10
                            MAXBALLSU = Me.txtMAXBALLSU10
                    End Select
                    If String.IsNullOrEmpty(JKNNAME.Text) Then
                        Msg = "名称を入力して下さい。"
                        JKNNAME.Focus()
                        Return False
                    End If
                    If String.IsNullOrEmpty(JKNKIN.Text) Then
                        JKNKIN.Text = "0"
                        'Msg = "単価を入力して下さい。"
                        'JKNKIN.Focus()
                        'Return False
                    End If
                    If String.IsNullOrEmpty(POINT.Text) Then
                        POINT.Text = "0"
                        'Msg = "ポイントを入力して下さい。"
                        'POINT.Focus()
                        'Return False
                    End If
                    If String.IsNullOrEmpty(ENDTIME.Text) Then
                        ENDTIME.Text = "99:99"
                        'Msg = "終了時間を入力して下さい。"
                        'POINT.Focus()
                        'Return False
                    End If
                    If String.IsNullOrEmpty(MAXBALLSU.Text) Then
                        Msg = "最大利用球数を入力して下さい。"
                        MAXBALLSU.Focus()
                        Return False
                    End If
                Next
            Next

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

            If String.IsNullOrEmpty(Me.txtMAXJDCNT.Text) Then
                UIUtility.SYSTEM.MAXJDCNT = 0
            Else
                UIUtility.SYSTEM.MAXJDCNT = CType(Me.txtMAXJDCNT.Text, Integer)
            End If
            sql.Clear()
            sql.Append("UPDATE SYSMTA SET ")
            '時間貸し上限打球数
            sql.Append("MAXJDCNT = " & UIUtility.SYSTEM.MAXJDCNT & ",")
            sql.Append("UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            If Not iDatabase.ExecuteUpdate("DELETE FROM EIGMTB WHERE RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1)) Then
                Return False
            End If

            Dim intJKNKB As Integer = 1
            Dim JKNMM As TextBox = Nothing
            Dim JKNNAME As TextBox = Nothing
            Dim JKNKIN As TextBox = Nothing
            Dim POINT As TextBox = Nothing
            Dim ENDTIME As TextBox = Nothing
            Dim JKNFLR As ComboBox = Nothing
            Dim MAXBALLSU As TextBox = Nothing
            Dim CHARGEFLG As CheckBox = Nothing

            For j As Integer = 1 To 10
                intJKNKB = 1
                For i As Integer = 1 To 10
                    '単価
                    If j.Equals(1) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_01
                    ElseIf j.Equals(1) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_01
                    ElseIf j.Equals(1) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_01
                    ElseIf j.Equals(1) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_01
                    ElseIf j.Equals(1) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_01
                    ElseIf j.Equals(1) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_01
                    ElseIf j.Equals(1) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_01
                    ElseIf j.Equals(1) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_01
                    ElseIf j.Equals(1) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_01
                    ElseIf j.Equals(1) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_01
                    End If

                    If j.Equals(2) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_02
                    ElseIf j.Equals(2) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_02
                    ElseIf j.Equals(2) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_02
                    ElseIf j.Equals(2) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_02
                    ElseIf j.Equals(2) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_02
                    ElseIf j.Equals(2) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_02
                    ElseIf j.Equals(2) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_02
                    ElseIf j.Equals(2) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_02
                    ElseIf j.Equals(2) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_02
                    ElseIf j.Equals(2) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_02
                    End If

                    If j.Equals(3) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_03
                    ElseIf j.Equals(3) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_03
                    ElseIf j.Equals(3) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_03
                    ElseIf j.Equals(3) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_03
                    ElseIf j.Equals(3) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_03
                    ElseIf j.Equals(3) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_03
                    ElseIf j.Equals(3) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_03
                    ElseIf j.Equals(3) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_03
                    ElseIf j.Equals(3) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_03
                    ElseIf j.Equals(3) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_03
                    End If

                    If j.Equals(4) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_04
                    ElseIf j.Equals(4) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_04
                    ElseIf j.Equals(4) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_04
                    ElseIf j.Equals(4) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_04
                    ElseIf j.Equals(4) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_04
                    ElseIf j.Equals(4) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_04
                    ElseIf j.Equals(4) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_04
                    ElseIf j.Equals(4) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_04
                    ElseIf j.Equals(4) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN09_04
                    ElseIf j.Equals(4) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_04
                    End If

                    If j.Equals(5) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_05
                    ElseIf j.Equals(5) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_05
                    ElseIf j.Equals(5) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_05
                    ElseIf j.Equals(5) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_05
                    ElseIf j.Equals(5) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_05
                    ElseIf j.Equals(5) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_05
                    ElseIf j.Equals(5) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_05
                    ElseIf j.Equals(5) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_05
                    ElseIf j.Equals(5) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_05
                    ElseIf j.Equals(5) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_05
                    End If

                    If j.Equals(6) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_06
                    ElseIf j.Equals(6) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_06
                    ElseIf j.Equals(6) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_06
                    ElseIf j.Equals(6) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_06
                    ElseIf j.Equals(6) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_06
                    ElseIf j.Equals(6) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_06
                    ElseIf j.Equals(6) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_06
                    ElseIf j.Equals(6) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_06
                    ElseIf j.Equals(6) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_06
                    ElseIf j.Equals(6) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_06
                    End If

                    If j.Equals(7) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_07
                    ElseIf j.Equals(7) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_07
                    ElseIf j.Equals(7) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_07
                    ElseIf j.Equals(7) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_07
                    ElseIf j.Equals(7) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_07
                    ElseIf j.Equals(7) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_07
                    ElseIf j.Equals(7) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_07
                    ElseIf j.Equals(7) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_07
                    ElseIf j.Equals(7) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_07
                    ElseIf j.Equals(7) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_07
                    End If

                    If j.Equals(8) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_08
                    ElseIf j.Equals(8) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_08
                    ElseIf j.Equals(8) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_08
                    ElseIf j.Equals(8) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_08
                    ElseIf j.Equals(8) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_08
                    ElseIf j.Equals(8) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_08
                    ElseIf j.Equals(8) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_08
                    ElseIf j.Equals(8) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_08
                    ElseIf j.Equals(8) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_08
                    ElseIf j.Equals(8) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_08
                    End If

                    If j.Equals(9) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_09
                    ElseIf j.Equals(9) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_09
                    ElseIf j.Equals(9) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_09
                    ElseIf j.Equals(9) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_09
                    ElseIf j.Equals(9) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_09
                    ElseIf j.Equals(9) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_09
                    ElseIf j.Equals(9) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_09
                    ElseIf j.Equals(9) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_09
                    ElseIf j.Equals(9) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_09
                    ElseIf j.Equals(9) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_09
                    End If

                    If j.Equals(10) And i.Equals(1) Then
                        JKNKIN = Me.txtJKNKIN01_10
                    ElseIf j.Equals(10) And i.Equals(2) Then
                        JKNKIN = Me.txtJKNKIN02_10
                    ElseIf j.Equals(10) And i.Equals(3) Then
                        JKNKIN = Me.txtJKNKIN03_10
                    ElseIf j.Equals(10) And i.Equals(4) Then
                        JKNKIN = Me.txtJKNKIN04_10
                    ElseIf j.Equals(10) And i.Equals(5) Then
                        JKNKIN = Me.txtJKNKIN05_10
                    ElseIf j.Equals(10) And i.Equals(6) Then
                        JKNKIN = Me.txtJKNKIN06_10
                    ElseIf j.Equals(10) And i.Equals(7) Then
                        JKNKIN = Me.txtJKNKIN07_10
                    ElseIf j.Equals(10) And i.Equals(8) Then
                        JKNKIN = Me.txtJKNKIN08_10
                    ElseIf j.Equals(10) And i.Equals(9) Then
                        JKNKIN = Me.txtJKNKIN09_10
                    ElseIf j.Equals(10) And i.Equals(10) Then
                        JKNKIN = Me.txtJKNKIN10_10
                    End If

                    Select Case i
                        Case 1
                            If String.IsNullOrEmpty(Me.txtJKNMM01.Text) Then Continue For
                            JKNMM = Me.txtJKNMM01
                            JKNNAME = Me.txtJKNNAME01
                            POINT = Me.txtPOINT01
                            ENDTIME = Me.txtENDTIME01
                            JKNFLR = Me.cmbJKNFLR01
                            MAXBALLSU = Me.txtMAXBALLSU01
                            CHARGEFLG = Me.chkCHARGEFLG01
                        Case 2
                            If String.IsNullOrEmpty(Me.txtJKNMM02.Text) Then Continue For
                            JKNMM = Me.txtJKNMM02
                            JKNNAME = Me.txtJKNNAME02
                            POINT = Me.txtPOINT02
                            ENDTIME = Me.txtENDTIME02
                            JKNFLR = Me.cmbJKNFLR02
                            MAXBALLSU = Me.txtMAXBALLSU02
                            CHARGEFLG = Me.chkCHARGEFLG02
                        Case 3
                            If String.IsNullOrEmpty(Me.txtJKNMM03.Text) Then Continue For
                            JKNMM = Me.txtJKNMM03
                            JKNNAME = Me.txtJKNNAME03
                            POINT = Me.txtPOINT03
                            ENDTIME = Me.txtENDTIME03
                            JKNFLR = Me.cmbJKNFLR03
                            MAXBALLSU = Me.txtMAXBALLSU03
                            CHARGEFLG = Me.chkCHARGEFLG03
                        Case 4
                            If String.IsNullOrEmpty(Me.txtJKNMM04.Text) Then Continue For
                            JKNMM = Me.txtJKNMM04
                            JKNNAME = Me.txtJKNNAME04
                            POINT = Me.txtPOINT04
                            ENDTIME = Me.txtENDTIME04
                            JKNFLR = Me.cmbJKNFLR04
                            MAXBALLSU = Me.txtMAXBALLSU04
                            CHARGEFLG = Me.chkCHARGEFLG04
                        Case 5
                            If String.IsNullOrEmpty(Me.txtJKNMM05.Text) Then Continue For
                            JKNMM = Me.txtJKNMM05
                            JKNNAME = Me.txtJKNNAME05
                            POINT = Me.txtPOINT05
                            ENDTIME = Me.txtENDTIME05
                            JKNFLR = Me.cmbJKNFLR05
                            MAXBALLSU = Me.txtMAXBALLSU05
                            CHARGEFLG = Me.chkCHARGEFLG05
                        Case 6
                            If String.IsNullOrEmpty(Me.txtJKNMM06.Text) Then Continue For
                            JKNMM = Me.txtJKNMM06
                            JKNNAME = Me.txtJKNNAME06
                            POINT = Me.txtPOINT06
                            ENDTIME = Me.txtENDTIME06
                            JKNFLR = Me.cmbJKNFLR06
                            MAXBALLSU = Me.txtMAXBALLSU06
                            CHARGEFLG = Me.chkCHARGEFLG06
                        Case 7
                            If String.IsNullOrEmpty(Me.txtJKNMM07.Text) Then Continue For
                            JKNMM = Me.txtJKNMM07
                            JKNNAME = Me.txtJKNNAME07
                            POINT = Me.txtPOINT07
                            ENDTIME = Me.txtENDTIME07
                            JKNFLR = Me.cmbJKNFLR07
                            MAXBALLSU = Me.txtMAXBALLSU07
                            CHARGEFLG = Me.chkCHARGEFLG07
                        Case 8
                            If String.IsNullOrEmpty(Me.txtJKNMM08.Text) Then Continue For
                            JKNMM = Me.txtJKNMM08
                            JKNNAME = Me.txtJKNNAME08
                            POINT = Me.txtPOINT08
                            ENDTIME = Me.txtENDTIME08
                            JKNFLR = Me.cmbJKNFLR08
                            MAXBALLSU = Me.txtMAXBALLSU08
                            CHARGEFLG = Me.chkCHARGEFLG08
                        Case 9
                            If String.IsNullOrEmpty(Me.txtJKNMM09.Text) Then Continue For
                            JKNMM = Me.txtJKNMM09
                            JKNNAME = Me.txtJKNNAME09
                            POINT = Me.txtPOINT09
                            ENDTIME = Me.txtENDTIME09
                            JKNFLR = Me.cmbJKNFLR09
                            MAXBALLSU = Me.txtMAXBALLSU09
                            CHARGEFLG = Me.chkCHARGEFLG09
                        Case 10
                            If String.IsNullOrEmpty(Me.txtJKNMM10.Text) Then Continue For
                            JKNMM = Me.txtJKNMM10
                            JKNNAME = Me.txtJKNNAME10
                            POINT = Me.txtPOINT10
                            ENDTIME = Me.txtENDTIME10
                            JKNFLR = Me.cmbJKNFLR10
                            MAXBALLSU = Me.txtMAXBALLSU10
                            CHARGEFLG = Me.chkCHARGEFLG10
                    End Select

                    sql.Clear()
                    sql.Append("INSERT INTO EIGMTB VALUES(")
                    sql.Append((Me.cmbRKNKB.SelectedIndex + 1) & ",")
                    sql.Append(j & ",")
                    sql.Append(intJKNKB & ",")
                    sql.Append(CType(JKNMM.Text, Integer) & ",")
                    sql.Append("'" & JKNNAME.Text & "',")
                    sql.Append(CType(JKNKIN.Text, Integer) & ",")
                    sql.Append("0,")
                    sql.Append(CType(POINT.Text, Integer) & ",")
                    sql.Append(JKNFLR.SelectedIndex & ",")
                    sql.Append(CType(MAXBALLSU.Text, Integer) & ",")
                    If CHARGEFLG.Checked Then
                        sql.Append("1,")
                    Else
                        sql.Append("0,")
                    End If
                    sql.Append("'" & ENDTIME.Text.Replace(":", String.Empty) & "',")
                    sql.Append("NOW(),")
                    sql.Append("NOW())")

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If

                    intJKNKB += 1
                Next
            Next

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

