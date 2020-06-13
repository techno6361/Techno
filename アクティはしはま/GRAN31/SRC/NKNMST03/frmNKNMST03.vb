Imports TECHNO.DataBase

Public Class frmNKNMST03

#Region "▼宣言部"

    ''' <summary>
    ''' 画面表示時の最新の更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtUPDDTM As DateTime

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "コース料金マスタ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "コース料金マスタ登録"

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
    Private Sub frmKOSUMTA_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            '料金体系マスタ情報取得
            If Not GetRKNMTA() Then
                Using frm As New frmMSGBOX01("料金体系マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If


            '画面初期設定
            Init()

            'ｺｰｽ料金マスタ情報取得
            GetKOSUMTA()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmKOSUMTA_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
             '画面初期設定
            Init()

            'ｺｰｽ料金マスタ情報取得
            GetKOSUMTA()
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
            Msg = Me.cmbCopyRKNKB.Text & "のコース料金情報をコピーしてよろしいですか？"
            Using frm As New frmMSGBOX01(Msg, 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '画面初期化
            Init()



            'コース料金情報取得
            GetKOSUMTA(True)

        Catch ex As Exception
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

            '画面初期化
            Init()

            'ｺｰｽ料金マスタ情報取得
            GetKOSUMTA()

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
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM KOSUMTA WHERE RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1))
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

            'ｺｰｽ料金マスタ情報登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("コース料金マスタ情報の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            '画面初期設定
            Init()

            'コース料金マスタ情報取得
            GetKOSUMTA()

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

            '保険料・カート料・コンペ料
            Me.txtHOKENKIN.Text = "0"
            Me.txtCARTKIN.Text = "0"
            Me.txtCOMPEKIN.Text = "0"

            '誕生月・U40ポイント
            Me.txtPOINT2.Text = "0"


            '価格名称
            Me.txtPRCNM1.Text = String.Empty
            Me.txtPRCNM2.Text = String.Empty
            Me.txtPRCNM3.Text = String.Empty
            Me.txtPRCNM4.Text = String.Empty
            Me.txtPRCNM5.Text = String.Empty

            'メンバー
            Me.txtH09KIN_M1.Text = "0"
            Me.txtH09KIN_M2.Text = "0"
            Me.txtH09KIN_M3.Text = "0"
            Me.txtH09KIN_M4.Text = "0"
            Me.txtH09KIN_M5.Text = "0"
            Me.txtH18KIN_M1.Text = "0"
            Me.txtH18KIN_M2.Text = "0"
            Me.txtH18KIN_M3.Text = "0"
            Me.txtH18KIN_M4.Text = "0"
            Me.txtH18KIN_M5.Text = "0"
            Me.txtH00KIN_M1.Text = "0"
            Me.txtH00KIN_M2.Text = "0"
            Me.txtH00KIN_M3.Text = "0"
            Me.txtH00KIN_M4.Text = "0"
            Me.txtH00KIN_M5.Text = "0"
            'ビジター
            Me.txtH09KIN_V1.Text = "0"
            Me.txtH09KIN_V2.Text = "0"
            Me.txtH09KIN_V3.Text = "0"
            Me.txtH09KIN_V4.Text = "0"
            Me.txtH09KIN_V5.Text = "0"
            Me.txtH18KIN_V1.Text = "0"
            Me.txtH18KIN_V2.Text = "0"
            Me.txtH18KIN_V3.Text = "0"
            Me.txtH18KIN_V4.Text = "0"
            Me.txtH18KIN_V5.Text = "0"
            Me.txtH00KIN_V1.Text = "0"
            Me.txtH00KIN_V2.Text = "0"
            Me.txtH00KIN_V3.Text = "0"
            Me.txtH00KIN_V4.Text = "0"
            Me.txtH00KIN_V5.Text = "0"
            'ジュニア
            Me.txtH09KIN_J1.Text = "0"
            Me.txtH09KIN_J2.Text = "0"
            Me.txtH09KIN_J3.Text = "0"
            Me.txtH09KIN_J4.Text = "0"
            Me.txtH09KIN_J5.Text = "0"
            Me.txtH18KIN_J1.Text = "0"
            Me.txtH18KIN_J2.Text = "0"
            Me.txtH18KIN_J3.Text = "0"
            Me.txtH18KIN_J4.Text = "0"
            Me.txtH18KIN_J5.Text = "0"
            Me.txtH00KIN_J1.Text = "0"
            Me.txtH00KIN_J2.Text = "0"
            Me.txtH00KIN_J3.Text = "0"
            Me.txtH00KIN_J4.Text = "0"
            Me.txtH00KIN_J5.Text = "0"
            '使用区分
            Me.chkUsePRC1.Checked = False
            Me.chkUsePRC2.Checked = False
            Me.chkUsePRC3.Checked = False
            Me.chkUsePRC4.Checked = False
            Me.chkUsePRC5.Checked = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
    ''' ｺｰｽ料金マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKOSUMTA(Optional CopyFlg As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            'メンバー
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE ")
            If CopyFlg Then
                sql.Append(" RKNKB = " & (Me.cmbCopyRKNKB.SelectedIndex + 1))
            Else
                sql.Append(" RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1))
            End If
            sql.Append(" AND NKBNO = 1")

            Dim resultDt1 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt1.Rows.Count - 1
                Select Case resultDt1.Rows(i).Item("PRCKB").ToString
                    Case "1"
                        '一般
                        Me.txtPRCNM1.Text = resultDt1.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_M1.Text = CType(resultDt1.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_M1.Text = CType(resultDt1.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_M1.Text = CType(resultDt1.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt1.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC1.Checked = False
                    Case "2"
                        'シニア
                        Me.txtPRCNM2.Text = resultDt1.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_M2.Text = CType(resultDt1.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_M2.Text = CType(resultDt1.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_M2.Text = CType(resultDt1.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt1.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC2.Checked = True
                    Case "3"
                        '早朝薄暮
                        Me.txtPRCNM3.Text = resultDt1.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_M3.Text = CType(resultDt1.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_M3.Text = CType(resultDt1.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_M3.Text = CType(resultDt1.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt1.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC3.Checked = True
                    Case "4"
                        '誕生月
                        Me.txtPRCNM4.Text = resultDt1.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_M4.Text = CType(resultDt1.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_M4.Text = CType(resultDt1.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_M4.Text = CType(resultDt1.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt1.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC4.Checked = True
                    Case "5"
                        'U40
                        Me.txtPRCNM5.Text = resultDt1.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_M5.Text = CType(resultDt1.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_M5.Text = CType(resultDt1.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_M5.Text = CType(resultDt1.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt1.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC5.Checked = True
                End Select
                '保険料
                Me.txtHOKENKIN.Text = CType(resultDt1.Rows(i).Item("HOKENKIN").ToString, Integer).ToString("#,##0")
                'カート料
                Me.txtCARTKIN.Text = CType(resultDt1.Rows(i).Item("CARTKIN").ToString, Integer).ToString("#,##0")
                'コンペ料
                Me.txtCOMPEKIN.Text = CType(resultDt1.Rows(i).Item("COMPEKIN").ToString, Integer).ToString("#,##0")
                '誕生月・U40ポイント
                Me.txtPOINT2.Text = CType(resultDt1.Rows(i).Item("POINT2").ToString, Integer).ToString("#,##0")
                If Not CopyFlg Then _dtUPDDTM = DirectCast(resultDt1.Rows(0).Item("UPDDTM"), DateTime)
            Next

            'ビジター
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE ")
            If CopyFlg Then
                sql.Append(" RKNKB = " & (Me.cmbCopyRKNKB.SelectedIndex + 1))
            Else
                sql.Append(" RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1))
            End If
            sql.Append(" AND NKBNO = 2")

            Dim resultDt2 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt2.Rows.Count - 1
                Select Case resultDt2.Rows(i).Item("PRCKB").ToString
                    Case "1"
                        '一般
                        Me.txtPRCNM1.Text = resultDt2.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_V1.Text = CType(resultDt2.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_V1.Text = CType(resultDt2.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_V1.Text = CType(resultDt2.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt2.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC1.Checked = True
                    Case "2"
                        'シニア
                        Me.txtPRCNM2.Text = resultDt2.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_V2.Text = CType(resultDt2.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_V2.Text = CType(resultDt2.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_V2.Text = CType(resultDt2.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt2.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC2.Checked = True
                    Case "3"
                        '早朝薄暮
                        Me.txtPRCNM3.Text = resultDt2.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_V3.Text = CType(resultDt2.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_V3.Text = CType(resultDt2.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_V3.Text = CType(resultDt2.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt2.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC3.Checked = True
                    Case "4"
                        '誕生月
                        Me.txtPRCNM4.Text = resultDt2.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_V4.Text = CType(resultDt2.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_V4.Text = CType(resultDt2.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_V4.Text = CType(resultDt2.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt2.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC4.Checked = True
                    Case "5"
                        'U40
                        Me.txtPRCNM5.Text = resultDt2.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_V5.Text = CType(resultDt2.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_V5.Text = CType(resultDt2.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_V5.Text = CType(resultDt2.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt2.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC5.Checked = True
                End Select
                '保険料
                Me.txtHOKENKIN.Text = CType(resultDt1.Rows(i).Item("HOKENKIN").ToString, Integer).ToString("#,##0")
                'カート料
                Me.txtCARTKIN.Text = CType(resultDt1.Rows(i).Item("CARTKIN").ToString, Integer).ToString("#,##0")
                'コンペ料
                Me.txtCOMPEKIN.Text = CType(resultDt1.Rows(i).Item("COMPEKIN").ToString, Integer).ToString("#,##0")
                '誕生月・U40ポイント
                Me.txtPOINT2.Text = CType(resultDt1.Rows(i).Item("POINT2").ToString, Integer).ToString("#,##0")
                If Not CopyFlg Then _dtUPDDTM = DirectCast(resultDt2.Rows(0).Item("UPDDTM"), DateTime)
            Next

            'ジュニア
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUMTA")
            sql.Append(" WHERE ")
            If CopyFlg Then
                sql.Append(" RKNKB = " & (Me.cmbCopyRKNKB.SelectedIndex + 1))
            Else
                sql.Append(" RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1))
            End If
            sql.Append(" AND NKBNO = 3")

            Dim resultDt3 As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt3.Rows.Count - 1
                Select Case resultDt3.Rows(i).Item("PRCKB").ToString
                    Case "1"
                        '一般
                        Me.txtPRCNM1.Text = resultDt3.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_J1.Text = CType(resultDt3.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_J1.Text = CType(resultDt3.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_J1.Text = CType(resultDt3.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt3.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC1.Checked = True
                    Case "2"
                        'シニア
                        Me.txtPRCNM2.Text = resultDt3.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_J2.Text = CType(resultDt3.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_J2.Text = CType(resultDt3.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_J2.Text = CType(resultDt3.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt3.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC2.Checked = True
                    Case "3"
                        '早朝薄暮
                        Me.txtPRCNM3.Text = resultDt3.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_J3.Text = CType(resultDt3.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_J3.Text = CType(resultDt3.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_J3.Text = CType(resultDt3.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt3.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC3.Checked = True
                    Case "4"
                        '誕生月
                        Me.txtPRCNM4.Text = resultDt3.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_J4.Text = CType(resultDt3.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_J4.Text = CType(resultDt3.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_J4.Text = CType(resultDt3.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt3.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC4.Checked = True
                    Case "5"
                        'U40
                        Me.txtPRCNM5.Text = resultDt3.Rows(i).Item("PRCNM").ToString
                        Me.txtH09KIN_J5.Text = CType(resultDt3.Rows(i).Item("H09KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH18KIN_J5.Text = CType(resultDt3.Rows(i).Item("H18KIN").ToString, Integer).ToString("#,##0")
                        Me.txtH00KIN_J5.Text = CType(resultDt3.Rows(i).Item("H00KIN").ToString, Integer).ToString("#,##0")
                        If resultDt3.Rows(i).Item("USEKB").ToString.Equals("1") Then Me.chkUsePRC5.Checked = True
                End Select
                '保険料
                Me.txtHOKENKIN.Text = CType(resultDt1.Rows(i).Item("HOKENKIN").ToString, Integer).ToString("#,##0")
                'カート料
                Me.txtCARTKIN.Text = CType(resultDt1.Rows(i).Item("CARTKIN").ToString, Integer).ToString("#,##0")
                'コンペ料
                Me.txtCOMPEKIN.Text = CType(resultDt1.Rows(i).Item("COMPEKIN").ToString, Integer).ToString("#,##0")
                '誕生月・U40ポイント
                Me.txtPOINT2.Text = CType(resultDt1.Rows(i).Item("POINT2").ToString, Integer).ToString("#,##0")
                If Not CopyFlg Then _dtUPDDTM = DirectCast(resultDt3.Rows(0).Item("UPDDTM"), DateTime)
            Next



            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 登録データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckRegister(ByRef Msg As String) As Boolean
        Try
            '保険料・カート料・コンペ料
            If String.IsNullOrEmpty(Me.txtHOKENKIN.Text) Then Me.txtHOKENKIN.Text = "0"
            If String.IsNullOrEmpty(Me.txtCARTKIN.Text) Then Me.txtCARTKIN.Text = "0"
            If String.IsNullOrEmpty(Me.txtCOMPEKIN.Text) Then Me.txtCOMPEKIN.Text = "0"
            '誕生月・U40ポイント
            If String.IsNullOrEmpty(Me.txtPOINT2.Text) Then Me.txtPOINT2.Text = "0"

            'メンバー
            If String.IsNullOrEmpty(Me.txtH09KIN_M1.Text) Then Me.txtH09KIN_M1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_M2.Text) Then Me.txtH09KIN_M2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_M3.Text) Then Me.txtH09KIN_M3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_M4.Text) Then Me.txtH09KIN_M4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_M5.Text) Then Me.txtH09KIN_M5.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_M1.Text) Then Me.txtH18KIN_M1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_M2.Text) Then Me.txtH18KIN_M2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_M3.Text) Then Me.txtH18KIN_M3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_M4.Text) Then Me.txtH18KIN_M4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_M5.Text) Then Me.txtH18KIN_M5.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_M1.Text) Then Me.txtH00KIN_M1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_M2.Text) Then Me.txtH00KIN_M2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_M3.Text) Then Me.txtH00KIN_M3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_M4.Text) Then Me.txtH00KIN_M4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_M5.Text) Then Me.txtH00KIN_M5.Text = "0"
            'ビジター
            If String.IsNullOrEmpty(Me.txtH09KIN_V1.Text) Then Me.txtH09KIN_V1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_V2.Text) Then Me.txtH09KIN_V2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_V3.Text) Then Me.txtH09KIN_V3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_V4.Text) Then Me.txtH09KIN_V4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_V5.Text) Then Me.txtH09KIN_V5.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_V1.Text) Then Me.txtH18KIN_V1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_V2.Text) Then Me.txtH18KIN_V2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_V3.Text) Then Me.txtH18KIN_V3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_V4.Text) Then Me.txtH18KIN_V4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_V5.Text) Then Me.txtH18KIN_V5.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_V1.Text) Then Me.txtH00KIN_V1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_V2.Text) Then Me.txtH00KIN_V2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_V3.Text) Then Me.txtH00KIN_V3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_V4.Text) Then Me.txtH00KIN_V4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_V5.Text) Then Me.txtH00KIN_V5.Text = "0"
            'ジュニア
            If String.IsNullOrEmpty(Me.txtH09KIN_J1.Text) Then Me.txtH09KIN_J1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_J2.Text) Then Me.txtH09KIN_J2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_J3.Text) Then Me.txtH09KIN_J3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_J4.Text) Then Me.txtH09KIN_J4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH09KIN_J5.Text) Then Me.txtH09KIN_J5.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_J1.Text) Then Me.txtH18KIN_J1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_J2.Text) Then Me.txtH18KIN_J2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_J3.Text) Then Me.txtH18KIN_J3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_J4.Text) Then Me.txtH18KIN_J4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH18KIN_J5.Text) Then Me.txtH18KIN_J5.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_J1.Text) Then Me.txtH00KIN_J1.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_J2.Text) Then Me.txtH00KIN_J2.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_J3.Text) Then Me.txtH00KIN_J3.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_J4.Text) Then Me.txtH00KIN_J4.Text = "0"
            If String.IsNullOrEmpty(Me.txtH00KIN_J5.Text) Then Me.txtH00KIN_J5.Text = "0"


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
        Dim strSQL01 As String = String.Empty
        Dim strSQL02 As String = String.Empty
        Try
            'トランザクション開始
            iDatabase.BeginTransaction()

            If Not iDatabase.ExecuteUpdate("DELETE FROM KOSUMTA WHERE RKNKB = " & (Me.cmbRKNKB.SelectedIndex + 1)) Then
                Return False
            End If

            Dim txtPRCNM As TextBox = Nothing
            Dim txtH09KIN As TextBox = Nothing
            Dim txtH18KIN As TextBox = Nothing
            Dim txtH00KIN As TextBox = Nothing
            Dim chkUSEKB As CheckBox = Nothing

            'メンバー
            For i As Integer = 1 To 5
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        txtPRCNM = Me.txtPRCNM1
                        txtH09KIN = Me.txtH09KIN_M1
                        txtH18KIN = Me.txtH18KIN_M1
                        txtH00KIN = Me.txtH00KIN_M1
                        chkUSEKB = Me.chkUsePRC1
                    Case 2
                        txtPRCNM = Me.txtPRCNM2
                        txtH09KIN = Me.txtH09KIN_M2
                        txtH18KIN = Me.txtH18KIN_M2
                        txtH00KIN = Me.txtH00KIN_M2
                        chkUSEKB = Me.chkUsePRC2
                    Case 3
                        txtPRCNM = Me.txtPRCNM3
                        txtH09KIN = Me.txtH09KIN_M3
                        txtH18KIN = Me.txtH18KIN_M3
                        txtH00KIN = Me.txtH00KIN_M3
                        chkUSEKB = Me.chkUsePRC3
                    Case 4
                        txtPRCNM = Me.txtPRCNM4
                        txtH09KIN = Me.txtH09KIN_M4
                        txtH18KIN = Me.txtH18KIN_M4
                        txtH00KIN = Me.txtH00KIN_M4
                        chkUSEKB = Me.chkUsePRC4
                    Case 5
                        txtPRCNM = Me.txtPRCNM5
                        txtH09KIN = Me.txtH09KIN_M5
                        txtH18KIN = Me.txtH18KIN_M5
                        txtH00KIN = Me.txtH00KIN_M5
                        chkUSEKB = Me.chkUsePRC5
                End Select

                strSQL01 &= "INSERT INTO KOSUMTA("
                strSQL02 &= " VALUES("
                '料金体系
                strSQL01 &= "RKNKB,"
                strSQL02 &= (Me.cmbRKNKB.SelectedIndex + 1) & ","
                '顧客種別
                strSQL01 &= "NKBNO,"
                strSQL02 &= "1,"
                '料金区分
                strSQL01 &= "PRCKB,"
                strSQL02 &= i & ","
                '料金名称
                strSQL01 &= "PRCNM,"
                strSQL02 &= "'" & txtPRCNM.Text & "',"
                '9H料金
                strSQL01 &= "H09KIN,"
                strSQL02 &= CType(txtH09KIN.Text, Integer) & ","
                '18H料金
                strSQL01 &= "H18KIN,"
                strSQL02 &= CType(txtH18KIN.Text, Integer) & ","
                '廻放題
                strSQL01 &= "H00KIN,"
                strSQL02 &= CType(txtH00KIN.Text, Integer) & ","
                '保険料
                strSQL01 &= "HOKENKIN,"
                strSQL02 &= CType(txtHOKENKIN.Text, Integer) & ","
                'カート料
                strSQL01 &= "CARTKIN,"
                strSQL02 &= CType(txtCARTKIN.Text, Integer) & ","
                'コンペ料
                strSQL01 &= "COMPEKIN,"
                strSQL02 &= CType(txtCOMPEKIN.Text, Integer) & ","
                '来場ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '誕生月・U40ポイント
                strSQL01 &= "POINT2,"
                strSQL02 &= CType(txtPOINT2.Text, Integer) & ","
                '使用区分
                strSQL01 &= "USEKB,"
                If chkUSEKB.Checked Then
                    strSQL02 &= "1,"
                Else
                    strSQL02 &= "0,"
                End If

                '作成日時
                strSQL01 &= "INSDTM,"
                strSQL02 &= "NOW(),"
                '更新日時
                strSQL01 &= "UPDDTM)"
                strSQL02 &= "NOW())"

                If Not iDatabase.ExecuteUpdate(strSQL01 & strSQL02) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Next

            'ビジター
            For i As Integer = 1 To 5
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        txtPRCNM = Me.txtPRCNM1
                        txtH09KIN = Me.txtH09KIN_V1
                        txtH18KIN = Me.txtH18KIN_V1
                        txtH00KIN = Me.txtH00KIN_V1
                        chkUSEKB = Me.chkUsePRC1
                    Case 2
                        txtPRCNM = Me.txtPRCNM2
                        txtH09KIN = Me.txtH09KIN_V2
                        txtH18KIN = Me.txtH18KIN_V2
                        txtH00KIN = Me.txtH00KIN_V2
                        chkUSEKB = Me.chkUsePRC2
                    Case 3
                        txtPRCNM = Me.txtPRCNM3
                        txtH09KIN = Me.txtH09KIN_V3
                        txtH18KIN = Me.txtH18KIN_V3
                        txtH00KIN = Me.txtH00KIN_V3
                        chkUSEKB = Me.chkUsePRC3
                    Case 4
                        txtPRCNM = Me.txtPRCNM4
                        txtH09KIN = Me.txtH09KIN_V4
                        txtH18KIN = Me.txtH18KIN_V4
                        txtH00KIN = Me.txtH00KIN_V4
                        chkUSEKB = Me.chkUsePRC4
                    Case 5
                        txtPRCNM = Me.txtPRCNM5
                        txtH09KIN = Me.txtH09KIN_V5
                        txtH18KIN = Me.txtH18KIN_V5
                        txtH00KIN = Me.txtH00KIN_V5
                        chkUSEKB = Me.chkUsePRC5
                End Select

                strSQL01 &= "INSERT INTO KOSUMTA("
                strSQL02 &= " VALUES("
                '料金体系
                strSQL01 &= "RKNKB,"
                strSQL02 &= (Me.cmbRKNKB.SelectedIndex + 1) & ","
                '顧客種別
                strSQL01 &= "NKBNO,"
                strSQL02 &= "2,"
                '料金区分
                strSQL01 &= "PRCKB,"
                strSQL02 &= i & ","
                '料金名称
                strSQL01 &= "PRCNM,"
                strSQL02 &= "'" & txtPRCNM.Text & "',"
                '9H料金
                strSQL01 &= "H09KIN,"
                strSQL02 &= CType(txtH09KIN.Text, Integer) & ","
                '18H料金
                strSQL01 &= "H18KIN,"
                strSQL02 &= CType(txtH18KIN.Text, Integer) & ","
                '廻放題
                strSQL01 &= "H00KIN,"
                strSQL02 &= CType(txtH00KIN.Text, Integer) & ","
                '保険料
                strSQL01 &= "HOKENKIN,"
                strSQL02 &= CType(txtHOKENKIN.Text, Integer) & ","
                'カート料
                strSQL01 &= "CARTKIN,"
                strSQL02 &= CType(txtCARTKIN.Text, Integer) & ","
                'コンペ料
                strSQL01 &= "COMPEKIN,"
                strSQL02 &= CType(txtCOMPEKIN.Text, Integer) & ","
                '来場ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '誕生月・U40ポイント
                strSQL01 &= "POINT2,"
                strSQL02 &= CType(txtPOINT2.Text, Integer) & ","
                '使用区分
                strSQL01 &= "USEKB,"
                If chkUSEKB.Checked Then
                    strSQL02 &= "1,"
                Else
                    strSQL02 &= "0,"
                End If

                '作成日時
                strSQL01 &= "INSDTM,"
                strSQL02 &= "NOW(),"
                '更新日時
                strSQL01 &= "UPDDTM)"
                strSQL02 &= "NOW())"

                If Not iDatabase.ExecuteUpdate(strSQL01 & strSQL02) Then
                    iDatabase.RollBack()
                    Return False
                End If
            Next

            'ジュニア
            For i As Integer = 1 To 5
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        txtPRCNM = Me.txtPRCNM1
                        txtH09KIN = Me.txtH09KIN_J1
                        txtH18KIN = Me.txtH18KIN_J1
                        txtH00KIN = Me.txtH00KIN_J1
                        chkUSEKB = Me.chkUsePRC1
                    Case 2
                        txtPRCNM = Me.txtPRCNM2
                        txtH09KIN = Me.txtH09KIN_J2
                        txtH18KIN = Me.txtH18KIN_J2
                        txtH00KIN = Me.txtH00KIN_J2
                        chkUSEKB = Me.chkUsePRC2
                    Case 3
                        txtPRCNM = Me.txtPRCNM3
                        txtH09KIN = Me.txtH09KIN_J3
                        txtH18KIN = Me.txtH18KIN_J3
                        txtH00KIN = Me.txtH00KIN_J3
                        chkUSEKB = Me.chkUsePRC3
                    Case 4
                        txtPRCNM = Me.txtPRCNM4
                        txtH09KIN = Me.txtH09KIN_J4
                        txtH18KIN = Me.txtH18KIN_J4
                        txtH00KIN = Me.txtH00KIN_J4
                        chkUSEKB = Me.chkUsePRC4
                    Case 5
                        txtPRCNM = Me.txtPRCNM5
                        txtH09KIN = Me.txtH09KIN_J5
                        txtH18KIN = Me.txtH18KIN_J5
                        txtH00KIN = Me.txtH00KIN_J5
                        chkUSEKB = Me.chkUsePRC5
                End Select

                strSQL01 &= "INSERT INTO KOSUMTA("
                strSQL02 &= " VALUES("
                '料金体系
                strSQL01 &= "RKNKB,"
                strSQL02 &= (Me.cmbRKNKB.SelectedIndex + 1) & ","
                '顧客種別
                strSQL01 &= "NKBNO,"
                strSQL02 &= "3,"
                '料金区分
                strSQL01 &= "PRCKB,"
                strSQL02 &= i & ","
                '料金名称
                strSQL01 &= "PRCNM,"
                strSQL02 &= "'" & txtPRCNM.Text & "',"
                '9H料金
                strSQL01 &= "H09KIN,"
                strSQL02 &= CType(txtH09KIN.Text, Integer) & ","
                '18H料金
                strSQL01 &= "H18KIN,"
                strSQL02 &= CType(txtH18KIN.Text, Integer) & ","
                '廻放題
                strSQL01 &= "H00KIN,"
                strSQL02 &= CType(txtH00KIN.Text, Integer) & ","
                '保険料
                strSQL01 &= "HOKENKIN,"
                strSQL02 &= CType(txtHOKENKIN.Text, Integer) & ","
                'カート料
                strSQL01 &= "CARTKIN,"
                strSQL02 &= CType(txtCARTKIN.Text, Integer) & ","
                'コンペ料
                strSQL01 &= "COMPEKIN,"
                strSQL02 &= CType(txtCOMPEKIN.Text, Integer) & ","
                '来場ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '誕生月・U40ポイント
                strSQL01 &= "POINT2,"
                strSQL02 &= CType(txtPOINT2.Text, Integer) & ","
                '使用区分
                strSQL01 &= "USEKB,"
                If chkUSEKB.Checked Then
                    strSQL02 &= "1,"
                Else
                    strSQL02 &= "0,"
                End If

                '作成日時
                strSQL01 &= "INSDTM,"
                strSQL02 &= "NOW(),"
                '更新日時
                strSQL01 &= "UPDDTM)"
                strSQL02 &= "NOW())"

                If Not iDatabase.ExecuteUpdate(strSQL01 & strSQL02) Then
                    iDatabase.RollBack()
                    Return False
                End If
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

