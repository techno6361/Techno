Imports TECHNO.DataBase

Public Class frmPRGMT01

#Region "▼宣言部"

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "画面セキュリティ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "画面セキュリティ登録"

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
    Private Sub frmSYSMT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            '画面初期設定
            Init()

            'プログラム情報取得
            If Not GetPRGMTA() Then
                Using frm As New frmMSGBOX01("プログラム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

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

            'プログラム情報取得
            If Not GetPRGMTA() Then
                Using frm As New frmMSGBOX01("プログラム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

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

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            Me.Cursor = Cursors.WaitCursor


            '【更新】

            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM PRGMTA WHERE SYSKBN = '1'")
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

            '更新登録
            If Not UpdRegister() Then
                Using frm As New frmMSGBOX01("システム情報の更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            '画面初期設定
            Init()

            'プログラム情報取得
            If Not GetPRGMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
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



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' プログラム情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPRGMTA() As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM PRGMTA")
            sql.Append(" WHERE")
            sql.Append(" SYSKBN = '1'")
            sql.Append(" ORDER BY SORTNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Select Case i
                    Case 0
                        '画面／処理名
                        Me.lblPRGIDNM01.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo01.Checked = True
                        Else
                            'する
                            Me.rdoYes01.Checked = True
                        End If
                    Case 1
                        '画面／処理名
                        Me.lblPRGIDNM02.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo02.Checked = True
                        Else
                            'する
                            Me.rdoYes02.Checked = True
                        End If
                    Case 2
                        '画面／処理名
                        Me.lblPRGIDNM03.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo03.Checked = True
                        Else
                            'する
                            Me.rdoYes03.Checked = True
                        End If
                    Case 3
                        '画面／処理名
                        Me.lblPRGIDNM04.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo04.Checked = True
                        Else
                            'する
                            Me.rdoYes04.Checked = True
                        End If
                    Case 4
                        '画面／処理名
                        Me.lblPRGIDNM05.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo05.Checked = True
                        Else
                            'する
                            Me.rdoYes05.Checked = True
                        End If
                    Case 5
                        '画面／処理名
                        Me.lblPRGIDNM06.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo06.Checked = True
                        Else
                            'する
                            Me.rdoYes06.Checked = True
                        End If
                    Case 6
                        '画面／処理名
                        Me.lblPRGIDNM07.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo07.Checked = True
                        Else
                            'する
                            Me.rdoYes07.Checked = True
                        End If
                    Case 7
                        '画面／処理名
                        Me.lblPRGIDNM08.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo08.Checked = True
                        Else
                            'する
                            Me.rdoYes08.Checked = True
                        End If
                    Case 8
                        '画面／処理名
                        Me.lblPRGIDNM09.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo09.Checked = True
                        Else
                            'する
                            Me.rdoYes09.Checked = True
                        End If
                    Case 9
                        '画面／処理名
                        Me.lblPRGIDNM10.Text = resultDt.Rows(i).Item("PRGNAME").ToString()
                        'ｾｷｭﾘﾃｨ
                        If CType(resultDt.Rows(i).Item("SCRTYKBN"), Integer).Equals(0) Then
                            'しない
                            Me.rdoNo10.Checked = True
                        Else
                            'する
                            Me.rdoYes10.Checked = True
                        End If
                End Select

            Next




            '最新の更新日時を取得
            _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdRegister() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intSCRTYKBN As Integer = 0
        Try
            'トランザクション開始
            iDatabase.BeginTransaction()

            For i As Integer = 0 To 9
                intSCRTYKBN = 0

                Select Case i
                    Case 0 : If Me.rdoYes01.Checked Then intSCRTYKBN = 1
                    Case 1 : If Me.rdoYes02.Checked Then intSCRTYKBN = 1
                    Case 2 : If Me.rdoYes03.Checked Then intSCRTYKBN = 1
                    Case 3 : If Me.rdoYes04.Checked Then intSCRTYKBN = 1
                    Case 4 : If Me.rdoYes05.Checked Then intSCRTYKBN = 1
                    Case 5 : If Me.rdoYes06.Checked Then intSCRTYKBN = 1
                    Case 6 : If Me.rdoYes07.Checked Then intSCRTYKBN = 1
                    Case 7 : If Me.rdoYes08.Checked Then intSCRTYKBN = 1
                    Case 8 : If Me.rdoYes09.Checked Then intSCRTYKBN = 1
                    Case 9 : If Me.rdoYes10.Checked Then intSCRTYKBN = 1
                End Select

                sql.Clear()
                sql.Append("UPDATE PRGMTA SET ")
                sql.Append("SCRTYKBN = '" & intSCRTYKBN & "',")
                sql.Append("UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" SYSKBN = '1'")
                sql.Append(" AND SORTNO = " & i + 1)

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
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
