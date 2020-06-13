Imports TECHNO.DataBase

Public Class frmDREPOMST01

#Region "▼宣言部"

    ''' <summary>
    ''' 商品分類マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtHINMTB As DataTable
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

            MyBase.l_Title_FormName = "ポイント還元マスタ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ポイント還元マスタ登録"

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
    Private Sub frmDREPOMST01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            '画面初期設定
            Init()



            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'ポイント還元マスタ情報取得
            GetDREPOMST()

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
    Private Sub frmDREPOMST01_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            _dtHINMTB.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 区分コンボボックス_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbTKTKBN01_001_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTKTKBN01_001.SelectedIndexChanged, cmbTKTKBN02_001.SelectedIndexChanged, cmbTKTKBN03_001.SelectedIndexChanged, cmbTKTKBN04_001.SelectedIndexChanged, cmbTKTKBN05_001.SelectedIndexChanged, cmbTKTKBN06_001.SelectedIndexChanged _
                                                                                                    , cmbTKTKBN01_002.SelectedIndexChanged, cmbTKTKBN02_002.SelectedIndexChanged, cmbTKTKBN03_002.SelectedIndexChanged, cmbTKTKBN04_002.SelectedIndexChanged, cmbTKTKBN05_002.SelectedIndexChanged, cmbTKTKBN06_002.SelectedIndexChanged _
                                                                                                    , cmbTKTKBN01_003.SelectedIndexChanged, cmbTKTKBN02_003.SelectedIndexChanged, cmbTKTKBN03_003.SelectedIndexChanged, cmbTKTKBN04_003.SelectedIndexChanged, cmbTKTKBN05_003.SelectedIndexChanged, cmbTKTKBN06_003.SelectedIndexChanged
        Try
            Dim cmbBox As ComboBox
            cmbBox = CType(sender, ComboBox)

            If cmbBox.SelectedIndex.Equals(-1) Then Exit Sub

            Dim PREMKN As TextBox = Nothing
            Dim TKTSU As TextBox = Nothing

            Select Case cmbBox.Name
                Case "cmbTKTKBN01_001"
                    PREMKN = Me.txtPREMKN01_001
                    TKTSU = Me.txtTKTSU01_001
                Case "cmbTKTKBN02_001"
                    PREMKN = Me.txtPREMKN02_001
                    TKTSU = Me.txtTKTSU02_001
                Case "cmbTKTKBN03_001"
                    PREMKN = Me.txtPREMKN03_001
                    TKTSU = Me.txtTKTSU03_001
                Case "cmbTKTKBN04_001"
                    PREMKN = Me.txtPREMKN04_001
                    TKTSU = Me.txtTKTSU04_001
                Case "cmbTKTKBN05_001"
                    PREMKN = Me.txtPREMKN05_001
                    TKTSU = Me.txtTKTSU05_001
                Case "cmbTKTKBN06_001"
                    PREMKN = Me.txtPREMKN06_001
                    TKTSU = Me.txtTKTSU06_001
                Case "cmbTKTKBN01_002"
                    PREMKN = Me.txtPREMKN01_002
                    TKTSU = Me.txtTKTSU01_002
                Case "cmbTKTKBN02_002"
                    PREMKN = Me.txtPREMKN02_002
                    TKTSU = Me.txtTKTSU02_002
                Case "cmbTKTKBN03_002"
                    PREMKN = Me.txtPREMKN03_002
                    TKTSU = Me.txtTKTSU03_002
                Case "cmbTKTKBN04_002"
                    PREMKN = Me.txtPREMKN04_002
                    TKTSU = Me.txtTKTSU04_002
                Case "cmbTKTKBN05_002"
                    PREMKN = Me.txtPREMKN05_002
                    TKTSU = Me.txtTKTSU05_002
                Case "cmbTKTKBN06_002"
                    PREMKN = Me.txtPREMKN06_002
                    TKTSU = Me.txtTKTSU06_002
                Case "cmbTKTKBN01_003"
                    PREMKN = Me.txtPREMKN01_003
                    TKTSU = Me.txtTKTSU01_003
                Case "cmbTKTKBN02_003"
                    PREMKN = Me.txtPREMKN02_003
                    TKTSU = Me.txtTKTSU02_003
                Case "cmbTKTKBN03_003"
                    PREMKN = Me.txtPREMKN03_003
                    TKTSU = Me.txtTKTSU03_003
                Case "cmbTKTKBN04_003"
                    PREMKN = Me.txtPREMKN04_003
                    TKTSU = Me.txtTKTSU04_003
                Case "cmbTKTKBN05_003"
                    PREMKN = Me.txtPREMKN05_003
                    TKTSU = Me.txtTKTSU05_003
                Case "cmbTKTKBN06_003"
                    PREMKN = Me.txtPREMKN06_003
                    TKTSU = Me.txtTKTSU06_003
            End Select

            If cmbBox.SelectedIndex.Equals(0) Then
                '【プレミアム】
                PREMKN.Enabled = True
                TKTSU.Enabled = False
                TKTSU.Text = "0"
            Else
                '【チケット】
                PREMKN.Enabled = False
                PREMKN.Text = "0"
                TKTSU.Enabled = True
            End If

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



            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'ポイント還元マスタ情報取得
            GetDREPOMST()

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
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM DREPOMST WHERE REKBN = '01'")
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

            'ポイント還元マスタ情報登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("ポイント還元マスタ情報の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            '画面初期設定
            Init()

            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'ポイント還元マスタ情報取得
            GetDREPOMST()

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

            'タグ名
            Me.txtBUNNMB001.Text = String.Empty
            Me.txtBUNNMB002.Text = String.Empty
            Me.txtBUNNMB003.Text = String.Empty

            'ポイント還元名称
            Me.txtREPONM01_002.Text = String.Empty
            Me.txtREPONM02_001.Text = String.Empty
            Me.txtREPONM03_001.Text = String.Empty
            Me.txtREPONM04_001.Text = String.Empty
            Me.txtREPONM05_001.Text = String.Empty
            Me.txtREPONM06_001.Text = String.Empty

            Me.txtREPONM01_002.Text = String.Empty
            Me.txtREPONM02_002.Text = String.Empty
            Me.txtREPONM03_002.Text = String.Empty
            Me.txtREPONM04_002.Text = String.Empty
            Me.txtREPONM05_002.Text = String.Empty
            Me.txtREPONM06_002.Text = String.Empty

            Me.txtREPONM01_003.Text = String.Empty
            Me.txtREPONM02_003.Text = String.Empty
            Me.txtREPONM03_003.Text = String.Empty
            Me.txtREPONM04_003.Text = String.Empty
            Me.txtREPONM05_003.Text = String.Empty
            Me.txtREPONM06_003.Text = String.Empty

            '消費ポイント
            Me.txtREPOINT01_001.Text = String.Empty
            Me.txtREPOINT02_001.Text = String.Empty
            Me.txtREPOINT03_001.Text = String.Empty
            Me.txtREPOINT04_001.Text = String.Empty
            Me.txtREPOINT05_001.Text = String.Empty
            Me.txtREPOINT06_001.Text = String.Empty

            Me.txtREPOINT01_002.Text = String.Empty
            Me.txtREPOINT02_002.Text = String.Empty
            Me.txtREPOINT03_002.Text = String.Empty
            Me.txtREPOINT04_002.Text = String.Empty
            Me.txtREPOINT05_002.Text = String.Empty
            Me.txtREPOINT06_002.Text = String.Empty

            Me.txtREPOINT01_003.Text = String.Empty
            Me.txtREPOINT02_003.Text = String.Empty
            Me.txtREPOINT03_003.Text = String.Empty
            Me.txtREPOINT04_003.Text = String.Empty
            Me.txtREPOINT05_003.Text = String.Empty
            Me.txtREPOINT06_003.Text = String.Empty

            '区分
            Me.cmbTKTKBN01_001.SelectedIndex = 0
            Me.cmbTKTKBN02_001.SelectedIndex = 0
            Me.cmbTKTKBN03_001.SelectedIndex = 0
            Me.cmbTKTKBN04_001.SelectedIndex = 0
            Me.cmbTKTKBN05_001.SelectedIndex = 0
            Me.cmbTKTKBN06_001.SelectedIndex = 0

            Me.cmbTKTKBN01_002.SelectedIndex = 0
            Me.cmbTKTKBN02_002.SelectedIndex = 0
            Me.cmbTKTKBN03_002.SelectedIndex = 0
            Me.cmbTKTKBN04_002.SelectedIndex = 0
            Me.cmbTKTKBN05_002.SelectedIndex = 0
            Me.cmbTKTKBN06_002.SelectedIndex = 0

            Me.cmbTKTKBN01_003.SelectedIndex = 0
            Me.cmbTKTKBN02_003.SelectedIndex = 0
            Me.cmbTKTKBN03_003.SelectedIndex = 0
            Me.cmbTKTKBN04_003.SelectedIndex = 0
            Me.cmbTKTKBN05_003.SelectedIndex = 0
            Me.cmbTKTKBN06_003.SelectedIndex = 0

            '還元金額
            Me.txtPREMKN01_001.Text = String.Empty
            Me.txtPREMKN02_001.Text = String.Empty
            Me.txtPREMKN03_001.Text = String.Empty
            Me.txtPREMKN04_001.Text = String.Empty
            Me.txtPREMKN05_001.Text = String.Empty
            Me.txtPREMKN06_001.Text = String.Empty

            Me.txtPREMKN01_002.Text = String.Empty
            Me.txtPREMKN02_002.Text = String.Empty
            Me.txtPREMKN03_002.Text = String.Empty
            Me.txtPREMKN04_002.Text = String.Empty
            Me.txtPREMKN05_002.Text = String.Empty
            Me.txtPREMKN06_002.Text = String.Empty

            Me.txtPREMKN01_003.Text = String.Empty
            Me.txtPREMKN02_003.Text = String.Empty
            Me.txtPREMKN03_003.Text = String.Empty
            Me.txtPREMKN04_003.Text = String.Empty
            Me.txtPREMKN05_003.Text = String.Empty
            Me.txtPREMKN06_003.Text = String.Empty

            'チケット枚数
            Me.txtTKTSU01_001.Text = String.Empty
            Me.txtTKTSU02_001.Text = String.Empty
            Me.txtTKTSU03_001.Text = String.Empty
            Me.txtTKTSU04_001.Text = String.Empty
            Me.txtTKTSU05_001.Text = String.Empty
            Me.txtTKTSU06_001.Text = String.Empty

            Me.txtTKTSU01_002.Text = String.Empty
            Me.txtTKTSU02_002.Text = String.Empty
            Me.txtTKTSU03_002.Text = String.Empty
            Me.txtTKTSU04_002.Text = String.Empty
            Me.txtTKTSU05_002.Text = String.Empty
            Me.txtTKTSU06_002.Text = String.Empty

            Me.txtTKTSU01_003.Text = String.Empty
            Me.txtTKTSU02_003.Text = String.Empty
            Me.txtTKTSU03_003.Text = String.Empty
            Me.txtTKTSU04_003.Text = String.Empty
            Me.txtTKTSU05_003.Text = String.Empty
            Me.txtTKTSU06_003.Text = String.Empty



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 商品分類マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetHINMTB() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINMTB")
            sql.Append(" WHERE ")
            sql.Append(" BMNCD = '002'")
            sql.Append(" AND BUNCDA = '007'")
            sql.Append(" ORDER BY BUNCDB ")

            _dtHINMTB = iDatabase.ExecuteRead(sql.ToString())

            If _dtHINMTB.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.txtBUNNMB001.Text = _dtHINMTB.Rows(0).Item("BUNNMB").ToString
            Me.txtBUNNMB002.Text = _dtHINMTB.Rows(1).Item("BUNNMB").ToString
            Me.txtBUNNMB003.Text = _dtHINMTB.Rows(2).Item("BUNNMB").ToString

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' ポイント還元マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetDREPOMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DREPOMST")
            sql.Append(" WHERE ")
            sql.Append(" REKBN = '01'")
            sql.Append(" ORDER BY RETAGKBN,SEQNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Dim dr() As DataRow

            '//【タグ区分001】//
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 1")
            If dr.Length > 0 Then
                'ポイント還元名称01
                Me.txtREPONM01_001.Text = dr(0).Item("REPONM").ToString
                '消費ポイント01
                Me.txtREPOINT01_001.Text = dr(0).Item("REPOINT").ToString
                '区分01
                Me.cmbTKTKBN01_001.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額01
                Me.txtPREMKN01_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数01
                Me.txtTKTSU01_001.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 2")
            If dr.Length > 0 Then
                'ポイント還元名称02
                Me.txtREPONM02_001.Text = dr(0).Item("REPONM").ToString
                '消費ポイント02
                Me.txtREPOINT02_001.Text = dr(0).Item("REPOINT").ToString
                '区分02
                Me.cmbTKTKBN02_001.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額02
                Me.txtPREMKN02_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数02
                Me.txtTKTSU02_001.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 3")
            If dr.Length > 0 Then
                'ポイント還元名称03
                Me.txtREPONM03_001.Text = dr(0).Item("REPONM").ToString
                '消費ポイント03
                Me.txtREPOINT03_001.Text = dr(0).Item("REPOINT").ToString
                '区分03
                Me.cmbTKTKBN03_001.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額03
                Me.txtPREMKN03_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数03
                Me.txtTKTSU03_001.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 4")
            If dr.Length > 0 Then
                'ポイント還元名称04
                Me.txtREPONM04_001.Text = dr(0).Item("REPONM").ToString
                '消費ポイント04
                Me.txtREPOINT04_001.Text = dr(0).Item("REPOINT").ToString
                '区分04
                Me.cmbTKTKBN04_001.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額04
                Me.txtPREMKN04_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数04
                Me.txtTKTSU04_001.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 5")
            If dr.Length > 0 Then
                'ポイント還元名称05
                Me.txtREPONM05_001.Text = dr(0).Item("REPONM").ToString
                '消費ポイント05
                Me.txtREPOINT05_001.Text = dr(0).Item("REPOINT").ToString
                '区分05
                Me.cmbTKTKBN05_001.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額05
                Me.txtPREMKN05_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数05
                Me.txtTKTSU05_001.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '001' AND SEQNO = 6")
            If dr.Length > 0 Then
                'ポイント還元名称06
                Me.txtREPONM06_001.Text = dr(0).Item("REPONM").ToString
                '消費ポイント06
                Me.txtREPOINT06_001.Text = dr(0).Item("REPOINT").ToString
                '区分06
                Me.cmbTKTKBN06_001.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額06
                Me.txtPREMKN06_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数06
                Me.txtTKTSU06_001.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            '//【タグ区分002】//
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 1")
            If dr.Length > 0 Then
                'ポイント還元名称01
                Me.txtREPONM01_002.Text = dr(0).Item("REPONM").ToString
                '消費ポイント01
                Me.txtREPOINT01_002.Text = dr(0).Item("REPOINT").ToString
                '区分01
                Me.cmbTKTKBN01_002.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額01
                Me.txtPREMKN01_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数01
                Me.txtTKTSU01_002.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 2")
            If dr.Length > 0 Then
                'ポイント還元名称02
                Me.txtREPONM02_002.Text = dr(0).Item("REPONM").ToString
                '消費ポイント02
                Me.txtREPOINT02_002.Text = dr(0).Item("REPOINT").ToString
                '区分02
                Me.cmbTKTKBN02_002.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額02
                Me.txtPREMKN02_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数02
                Me.txtTKTSU02_002.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 3")
            If dr.Length > 0 Then
                'ポイント還元名称03
                Me.txtREPONM03_002.Text = dr(0).Item("REPONM").ToString
                '消費ポイント03
                Me.txtREPOINT03_002.Text = dr(0).Item("REPOINT").ToString
                '区分03
                Me.cmbTKTKBN03_002.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額03
                Me.txtPREMKN03_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数03
                Me.txtTKTSU03_002.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 4")
            If dr.Length > 0 Then
                'ポイント還元名称04
                Me.txtREPONM04_002.Text = dr(0).Item("REPONM").ToString
                '消費ポイント04
                Me.txtREPOINT04_002.Text = dr(0).Item("REPOINT").ToString
                '区分04
                Me.cmbTKTKBN04_002.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額04
                Me.txtPREMKN04_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数04
                Me.txtTKTSU04_002.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 5")
            If dr.Length > 0 Then
                'ポイント還元名称05
                Me.txtREPONM05_002.Text = dr(0).Item("REPONM").ToString
                '消費ポイント05
                Me.txtREPOINT05_002.Text = dr(0).Item("REPOINT").ToString
                '区分05
                Me.cmbTKTKBN05_002.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額05
                Me.txtPREMKN05_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数05
                Me.txtTKTSU05_002.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '002' AND SEQNO = 6")
            If dr.Length > 0 Then
                'ポイント還元名称06
                Me.txtREPONM06_002.Text = dr(0).Item("REPONM").ToString
                '消費ポイント06
                Me.txtREPOINT06_002.Text = dr(0).Item("REPOINT").ToString
                '区分06
                Me.cmbTKTKBN06_002.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額06
                Me.txtPREMKN06_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数06
                Me.txtTKTSU06_002.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If

            '//【タグ区分003】//
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 1")
            If dr.Length > 0 Then
                'ポイント還元名称01
                Me.txtREPONM01_003.Text = dr(0).Item("REPONM").ToString
                '消費ポイント01
                Me.txtREPOINT01_003.Text = dr(0).Item("REPOINT").ToString
                '区分01
                Me.cmbTKTKBN01_003.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額01
                Me.txtPREMKN01_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数01
                Me.txtTKTSU01_003.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 2")
            If dr.Length > 0 Then
                'ポイント還元名称02
                Me.txtREPONM02_003.Text = dr(0).Item("REPONM").ToString
                '消費ポイント02
                Me.txtREPOINT02_003.Text = dr(0).Item("REPOINT").ToString
                '区分02
                Me.cmbTKTKBN02_003.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額02
                Me.txtPREMKN02_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数02
                Me.txtTKTSU02_003.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 3")
            If dr.Length > 0 Then
                'ポイント還元名称03
                Me.txtREPONM03_003.Text = dr(0).Item("REPONM").ToString
                '消費ポイント03
                Me.txtREPOINT03_003.Text = dr(0).Item("REPOINT").ToString
                '区分03
                Me.cmbTKTKBN03_003.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額03
                Me.txtPREMKN03_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数03
                Me.txtTKTSU03_003.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 4")
            If dr.Length > 0 Then
                'ポイント還元名称04
                Me.txtREPONM04_003.Text = dr(0).Item("REPONM").ToString
                '消費ポイント04
                Me.txtREPOINT04_003.Text = dr(0).Item("REPOINT").ToString
                '区分04
                Me.cmbTKTKBN04_003.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額04
                Me.txtPREMKN04_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数04
                Me.txtTKTSU04_003.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 5")
            If dr.Length > 0 Then
                'ポイント還元名称05
                Me.txtREPONM05_003.Text = dr(0).Item("REPONM").ToString
                '消費ポイント05
                Me.txtREPOINT05_003.Text = dr(0).Item("REPOINT").ToString
                '区分05
                Me.cmbTKTKBN05_003.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額05
                Me.txtPREMKN05_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数05
                Me.txtTKTSU05_003.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("RETAGKBN = '003' AND SEQNO = 6")
            If dr.Length > 0 Then
                'ポイント還元名称06
                Me.txtREPONM06_003.Text = dr(0).Item("REPONM").ToString
                '消費ポイント06
                Me.txtREPOINT06_003.Text = dr(0).Item("REPOINT").ToString
                '区分06
                Me.cmbTKTKBN06_003.SelectedIndex = CType(dr(0).Item("TKTKBN").ToString, Integer)
                '還元金額06
                Me.txtPREMKN06_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'チケット数06
                Me.txtTKTSU06_003.Text = CType(dr(0).Item("TKTSU").ToString, Integer).ToString("#,##0")
            End If


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

            '商品分類マスタ　区分名変更
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB001.Text & "' WHERE BMNCD = '002' AND BUNCDA = '007' AND BUNCDB = '001'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB002.Text & "' WHERE BMNCD = '002' AND BUNCDA = '007' AND BUNCDB = '002'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB003.Text & "' WHERE BMNCD = '002' AND BUNCDA = '007' AND BUNCDB = '003'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If

            If Not iDatabase.ExecuteUpdate("DELETE FROM DREPOMST WHERE REKBN = '01'") Then
                Return False
            End If

            'ポイント還元マスタ更新
            Dim REPONM As TextBox = Nothing
            Dim REPOINT As TextBox = Nothing
            Dim TKTKBN As ComboBox = Nothing
            Dim PREMKN As TextBox = Nothing
            Dim TKTSU As TextBox = Nothing
            Dim intSEQNO As Integer = 0

            'タグ【001】
            intSEQNO = 1
            For i As Integer = 1 To 6
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        'シーケンス【01】
                        If String.IsNullOrEmpty(Me.txtREPOINT01_001.Text) Or Me.txtREPOINT01_001.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM01_001
                        REPOINT = Me.txtREPOINT01_001
                        TKTKBN = Me.cmbTKTKBN01_001
                        PREMKN = Me.txtPREMKN01_001
                        TKTSU = Me.txtTKTSU01_001
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtREPOINT02_001.Text) Or Me.txtREPOINT02_001.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM02_001
                        REPOINT = Me.txtREPOINT02_001
                        TKTKBN = Me.cmbTKTKBN02_001
                        PREMKN = Me.txtPREMKN02_001
                        TKTSU = Me.txtTKTSU02_001
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtREPOINT03_001.Text) Or Me.txtREPOINT03_001.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM03_001
                        REPOINT = Me.txtREPOINT03_001
                        TKTKBN = Me.cmbTKTKBN03_001
                        PREMKN = Me.txtPREMKN03_001
                        TKTSU = Me.txtTKTSU03_001
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtREPOINT04_001.Text) Or Me.txtREPOINT04_001.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM04_001
                        REPOINT = Me.txtREPOINT04_001
                        TKTKBN = Me.cmbTKTKBN04_001
                        PREMKN = Me.txtPREMKN04_001
                        TKTSU = Me.txtTKTSU04_001
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtREPOINT05_001.Text) Or Me.txtREPOINT05_001.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM05_001
                        REPOINT = Me.txtREPOINT05_001
                        TKTKBN = Me.cmbTKTKBN05_001
                        PREMKN = Me.txtPREMKN05_001
                        TKTSU = Me.txtTKTSU05_001
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtREPOINT06_001.Text) Or Me.txtREPOINT06_001.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM06_001
                        REPOINT = Me.txtREPOINT06_001
                        TKTKBN = Me.cmbTKTKBN06_001
                        PREMKN = Me.txtPREMKN06_001
                        TKTSU = Me.txtTKTSU06_001
                End Select
                strSQL01 &= "INSERT INTO DREPOMST("
                strSQL02 &= " VALUES("
                '還元区分
                strSQL01 &= "REKBN,"
                strSQL02 &= "'01',"
                'タグ区分
                strSQL01 &= "RETAGKBN,"
                strSQL02 &= "'001',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                'ポイント還元名称
                strSQL01 &= "REPONM,"
                If String.IsNullOrEmpty(REPONM.Text) Then
                    strSQL02 &= "NULL,"
                Else
                    strSQL02 &= "'" & REPONM.Text & "',"
                End If
                '消費ポイント
                strSQL01 &= "REPOINT,"
                strSQL02 &= CType(REPOINT.Text, Integer) & ","
                '区分
                strSQL01 &= "TKTKBN,"
                strSQL02 &= TKTKBN.SelectedIndex & ","
                '還元金額
                strSQL01 &= "PREMKN,"
                If String.IsNullOrEmpty(PREMKN.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(PREMKN.Text, Integer) & ","
                End If
                'チケット数
                strSQL01 &= "TKTSU,"
                If String.IsNullOrEmpty(TKTSU.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(TKTSU.Text, Integer) & ","
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

                intSEQNO += 1
            Next
            'タグ【002】
            intSEQNO = 1
            For i As Integer = 1 To 6
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        'シーケンス【01】
                        If String.IsNullOrEmpty(Me.txtREPOINT01_002.Text) Or Me.txtREPOINT01_002.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM01_002
                        REPOINT = Me.txtREPOINT01_002
                        TKTKBN = Me.cmbTKTKBN01_002
                        PREMKN = Me.txtPREMKN01_002
                        TKTSU = Me.txtTKTSU01_002
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtREPOINT02_002.Text) Or Me.txtREPOINT02_002.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM02_002
                        REPOINT = Me.txtREPOINT02_002
                        TKTKBN = Me.cmbTKTKBN02_002
                        PREMKN = Me.txtPREMKN02_002
                        TKTSU = Me.txtTKTSU02_002
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtREPOINT03_002.Text) Or Me.txtREPOINT03_002.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM03_002
                        REPOINT = Me.txtREPOINT03_002
                        TKTKBN = Me.cmbTKTKBN03_002
                        PREMKN = Me.txtPREMKN03_002
                        TKTSU = Me.txtTKTSU03_002
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtREPOINT04_002.Text) Or Me.txtREPOINT04_002.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM04_002
                        REPOINT = Me.txtREPOINT04_002
                        TKTKBN = Me.cmbTKTKBN04_002
                        PREMKN = Me.txtPREMKN04_002
                        TKTSU = Me.txtTKTSU04_002
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtREPOINT05_002.Text) Or Me.txtREPOINT05_002.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM05_002
                        REPOINT = Me.txtREPOINT05_002
                        TKTKBN = Me.cmbTKTKBN05_002
                        PREMKN = Me.txtPREMKN05_002
                        TKTSU = Me.txtTKTSU05_002
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtREPOINT06_002.Text) Or Me.txtREPOINT06_002.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM06_002
                        REPOINT = Me.txtREPOINT06_002
                        TKTKBN = Me.cmbTKTKBN06_002
                        PREMKN = Me.txtPREMKN06_002
                        TKTSU = Me.txtTKTSU06_002
                End Select
                strSQL01 &= "INSERT INTO DREPOMST("
                strSQL02 &= " VALUES("
                '還元区分
                strSQL01 &= "REKBN,"
                strSQL02 &= "'01',"
                'タグ区分
                strSQL01 &= "RETAGKBN,"
                strSQL02 &= "'002',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                'ポイント還元名称
                strSQL01 &= "REPONM,"
                If String.IsNullOrEmpty(REPONM.Text) Then
                    strSQL02 &= "NULL,"
                Else
                    strSQL02 &= "'" & REPONM.Text & "',"
                End If
                '消費ポイント
                strSQL01 &= "REPOINT,"
                strSQL02 &= CType(REPOINT.Text, Integer) & ","
                '区分
                strSQL01 &= "TKTKBN,"
                strSQL02 &= TKTKBN.SelectedIndex & ","
                '還元金額
                strSQL01 &= "PREMKN,"
                If String.IsNullOrEmpty(PREMKN.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(PREMKN.Text, Integer) & ","
                End If
                'チケット数
                strSQL01 &= "TKTSU,"
                If String.IsNullOrEmpty(TKTSU.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(TKTSU.Text, Integer) & ","
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

                intSEQNO += 1
            Next
            'タグ【003】
            intSEQNO = 1
            For i As Integer = 1 To 6
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        'シーケンス【01】
                        If String.IsNullOrEmpty(Me.txtREPOINT01_003.Text) Or Me.txtREPOINT01_003.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM01_003
                        REPOINT = Me.txtREPOINT01_003
                        TKTKBN = Me.cmbTKTKBN01_003
                        PREMKN = Me.txtPREMKN01_003
                        TKTSU = Me.txtTKTSU01_003
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtREPOINT02_003.Text) Or Me.txtREPOINT02_003.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM02_003
                        REPOINT = Me.txtREPOINT02_003
                        TKTKBN = Me.cmbTKTKBN02_003
                        PREMKN = Me.txtPREMKN02_003
                        TKTSU = Me.txtTKTSU02_003
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtREPOINT03_003.Text) Or Me.txtREPOINT03_003.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM03_003
                        REPOINT = Me.txtREPOINT03_003
                        TKTKBN = Me.cmbTKTKBN03_003
                        PREMKN = Me.txtPREMKN03_003
                        TKTSU = Me.txtTKTSU03_003
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtREPOINT04_003.Text) Or Me.txtREPOINT04_003.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM04_003
                        REPOINT = Me.txtREPOINT04_003
                        TKTKBN = Me.cmbTKTKBN04_003
                        PREMKN = Me.txtPREMKN04_003
                        TKTSU = Me.txtTKTSU04_003
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtREPOINT05_003.Text) Or Me.txtREPOINT05_003.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM05_003
                        REPOINT = Me.txtREPOINT05_003
                        TKTKBN = Me.cmbTKTKBN05_003
                        PREMKN = Me.txtPREMKN05_003
                        TKTSU = Me.txtTKTSU05_003
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtREPOINT06_003.Text) Or Me.txtREPOINT06_003.Text.Equals("0") Then Continue For
                        REPONM = Me.txtREPONM06_003
                        REPOINT = Me.txtREPOINT06_003
                        TKTKBN = Me.cmbTKTKBN06_003
                        PREMKN = Me.txtPREMKN06_003
                        TKTSU = Me.txtTKTSU06_003
                End Select
                strSQL01 &= "INSERT INTO DREPOMST("
                strSQL02 &= " VALUES("
                '還元区分
                strSQL01 &= "REKBN,"
                strSQL02 &= "'01',"
                'タグ区分
                strSQL01 &= "RETAGKBN,"
                strSQL02 &= "'003',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                'ポイント還元名称
                strSQL01 &= "REPONM,"
                If String.IsNullOrEmpty(REPONM.Text) Then
                    strSQL02 &= "NULL,"
                Else
                    strSQL02 &= "'" & REPONM.Text & "',"
                End If
                '消費ポイント
                strSQL01 &= "REPOINT,"
                strSQL02 &= CType(REPOINT.Text, Integer) & ","
                '区分
                strSQL01 &= "TKTKBN,"
                strSQL02 &= TKTKBN.SelectedIndex & ","
                '還元金額
                strSQL01 &= "PREMKN,"
                If String.IsNullOrEmpty(PREMKN.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(PREMKN.Text, Integer) & ","
                End If
                'チケット数
                strSQL01 &= "TKTSU,"
                If String.IsNullOrEmpty(TKTSU.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(TKTSU.Text, Integer) & ","
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

                intSEQNO += 1
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

