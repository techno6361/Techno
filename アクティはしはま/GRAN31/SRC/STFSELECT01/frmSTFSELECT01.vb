Imports TECHNO.DataBase

Public Class frmSTFSELECT01

#Region "▼宣言部"

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "スタッフ選択"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "スタッフ選択"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' スタッフコード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property STFCODE As String
        Get
            Return _strSTFCODE
        End Get
    End Property
    Private _strSTFCODE As String = String.Empty

    ''' <summary>
    ''' スタッフ名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property STFNAME As String
        Get
            Return _strSTFNAME
        End Get
    End Property
    Private _strSTFNAME As String = String.Empty


#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSTFSELECT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            '画面初期設定
            Init()

            'スタッフ情報取得
            GetSTAFFMT()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' スタッフボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSTF_Click(sender As System.Object, e As System.EventArgs) Handles btnSTF01.Click, btnSTF02.Click, btnSTF03.Click, btnSTF04.Click, btnSTF05.Click, btnSTF06.Click, btnSTF07.Click _
                                                                                    , btnSTF08.Click, btnSTF09.Click, btnSTF10.Click, btnSTF11.Click, btnSTF12.Click, btnSTF13.Click, btnSTF14.Click, btnSTF15.Click _
                                                                                    , btnSTF16.Click, btnSTF17.Click, btnSTF18.Click, btnSTF19.Click, btnSTF20.Click

        Try
            Dim btn As Button
            btn = CType(sender, Button)

            _strSTFCODE = btn.Tag.ToString
            _strSTFNAME = btn.Text

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False





        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' スタッフ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSTAFFMT()
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM STAFFMTA")
            sql.Append(" WHERE STFCODE > '0000'")
            sql.Append(" ORDER BY STFCODE ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                MessageBox.Show("スタッフ情報が見つかりません。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                If String.IsNullOrEmpty(resultDt.Rows(i).Item("STFNAME").ToString) Then Continue For
                Select Case i
                    Case 0
                        Me.btnSTF01.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF01.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF01.Visible = True
                    Case 1
                        Me.btnSTF02.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF02.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF02.Visible = True
                    Case 2
                        Me.btnSTF03.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF03.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF03.Visible = True
                    Case 3
                        Me.btnSTF04.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF04.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF04.Visible = True
                    Case 4
                        Me.btnSTF05.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF05.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF05.Visible = True
                    Case 5
                        Me.btnSTF06.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF06.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF06.Visible = True
                    Case 6
                        Me.btnSTF07.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF07.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF07.Visible = True
                    Case 7
                        Me.btnSTF08.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF08.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF08.Visible = True
                    Case 8
                        Me.btnSTF09.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF09.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF09.Visible = True
                    Case 9
                        Me.btnSTF10.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF10.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF10.Visible = True
                    Case 10
                        Me.btnSTF11.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF11.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF11.Visible = True
                    Case 11
                        Me.btnSTF12.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF12.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF12.Visible = True
                    Case 12
                        Me.btnSTF13.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF13.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF13.Visible = True
                    Case 13
                        Me.btnSTF14.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF14.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF14.Visible = True
                    Case 14
                        Me.btnSTF15.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF15.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF15.Visible = True
                    Case 15
                        Me.btnSTF16.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF16.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF16.Visible = True
                    Case 16
                        Me.btnSTF17.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF17.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF17.Visible = True
                    Case 17
                        Me.btnSTF18.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF18.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF18.Visible = True
                    Case 18
                        Me.btnSTF19.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF19.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF19.Visible = True
                    Case 19
                        Me.btnSTF20.Text = resultDt.Rows(i).Item("STFNAME").ToString
                        Me.btnSTF20.Tag = resultDt.Rows(i).Item("STFCODE").ToString
                        Me.btnSTF20.Visible = True
                End Select

            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region



End Class
