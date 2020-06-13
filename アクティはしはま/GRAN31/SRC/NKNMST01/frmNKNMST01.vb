Imports TECHNO.DataBase

Public Class frmNKNMST01

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

            MyBase.l_Title_FormName = "入金マスタ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "入金マスタ登録"

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
    Private Sub frmNKNMST01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            'ラジオボタン
            Me.rbSTSFLG0.Checked = True
            Me.rbSTSFLG1.Checked = False

            '画面初期設定
            Init()

            'カード発行手数料取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '入金マスタ情報取得
            GetNKNMST()

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
    Private Sub frmNKNMST01_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            _dtHINMTB.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フロント・入金機ラジオボタン_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbSTSFLG_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSTSFLG0.CheckedChanged, rbSTSFLG1.CheckedChanged
        Try
            '画面初期表示
            Init()

            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If


            '入金マスタ情報取得
            GetNKNMST()

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

            'ラジオボタン
            Me.rbSTSFLG0.Checked = True
            Me.rbSTSFLG1.Checked = False


            '商品分類マスタ情報取得
            If Not GetHINMTB() Then
                Using frm As New frmMSGBOX01("商品分類マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '入金マスタ情報取得
            GetNKNMST()

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

            Dim intSTSFLG As Integer = 0

            If rbSTSFLG1.Checked Then intSTSFLG = 1


            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM NKNMST WHERE STSFLG = '" & intSTSFLG & "'")
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

            '入金マスタ情報登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("入金マスタ情報の登録に失敗しました。", 2)
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

            '入金マスタ情報取得
            GetNKNMST()

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

            Me.lblBUNNMB001.Visible = True
            Me.lblBUNNMB002.Visible = True
            Me.lblBUNNMB003.Visible = True
            Me.txtBUNNMB001.Visible = True
            Me.txtBUNNMB002.Visible = True
            Me.txtBUNNMB003.Visible = True
            Me.Panel1.Visible = True
            Me.Panel2.Visible = True
            Me.Panel3.Visible = True
            Me.pnlCardHakken.Visible = False

            'タグ名
            Me.txtBUNNMB001.Text = String.Empty
            Me.txtBUNNMB002.Text = String.Empty
            Me.txtBUNNMB003.Text = String.Empty
            '入金額
            Me.txtNKNKN01_001.Text = String.Empty
            Me.txtNKNKN02_001.Text = String.Empty
            Me.txtNKNKN03_001.Text = String.Empty
            Me.txtNKNKN04_001.Text = String.Empty
            Me.txtNKNKN05_001.Text = String.Empty
            Me.txtNKNKN06_001.Text = String.Empty

            Me.txtNKNKN01_002.Text = String.Empty
            Me.txtNKNKN02_002.Text = String.Empty
            Me.txtNKNKN03_002.Text = String.Empty
            Me.txtNKNKN04_002.Text = String.Empty
            Me.txtNKNKN05_002.Text = String.Empty
            Me.txtNKNKN06_002.Text = String.Empty

            Me.txtNKNKN01_003.Text = String.Empty
            Me.txtNKNKN02_003.Text = String.Empty
            Me.txtNKNKN03_003.Text = String.Empty
            Me.txtNKNKN04_003.Text = String.Empty
            Me.txtNKNKN05_003.Text = String.Empty
            Me.txtNKNKN06_003.Text = String.Empty
            'プレミアム
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
            'ポイント
            Me.txtPOINT01_001.Text = String.Empty
            Me.txtPOINT02_001.Text = String.Empty
            Me.txtPOINT03_001.Text = String.Empty
            Me.txtPOINT04_001.Text = String.Empty
            Me.txtPOINT05_001.Text = String.Empty
            Me.txtPOINT06_001.Text = String.Empty

            Me.txtPOINT01_002.Text = String.Empty
            Me.txtPOINT02_002.Text = String.Empty
            Me.txtPOINT03_002.Text = String.Empty
            Me.txtPOINT04_002.Text = String.Empty
            Me.txtPOINT05_002.Text = String.Empty
            Me.txtPOINT06_002.Text = String.Empty

            Me.txtPOINT01_003.Text = String.Empty
            Me.txtPOINT02_003.Text = String.Empty
            Me.txtPOINT03_003.Text = String.Empty
            Me.txtPOINT04_003.Text = String.Empty
            Me.txtPOINT05_003.Text = String.Empty
            Me.txtPOINT06_003.Text = String.Empty
            '消費税
            Me.txtNKNTAX01_001.Text = String.Empty
            Me.txtNKNTAX02_001.Text = String.Empty
            Me.txtNKNTAX03_001.Text = String.Empty
            Me.txtNKNTAX04_001.Text = String.Empty
            Me.txtNKNTAX05_001.Text = String.Empty
            Me.txtNKNTAX06_001.Text = String.Empty

            Me.txtNKNTAX01_002.Text = String.Empty
            Me.txtNKNTAX02_002.Text = String.Empty
            Me.txtNKNTAX03_002.Text = String.Empty
            Me.txtNKNTAX04_002.Text = String.Empty
            Me.txtNKNTAX05_002.Text = String.Empty
            Me.txtNKNTAX06_002.Text = String.Empty

            Me.txtNKNTAX01_003.Text = String.Empty
            Me.txtNKNTAX02_003.Text = String.Empty
            Me.txtNKNTAX03_003.Text = String.Empty
            Me.txtNKNTAX04_003.Text = String.Empty
            Me.txtNKNTAX05_003.Text = String.Empty
            Me.txtNKNTAX06_003.Text = String.Empty

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

            'カード発行手数料
            UIUtility.SYSTEM.CARDFEE = CType(resultDt.Rows(0).Item("CARDFEE").ToString(), Integer)
            'システム更新日時
            UIUtility.SYSTEM.UPDDTM = resultDt.Rows(0).Item("UPDDAY").ToString()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

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
            sql.Append(" AND BUNCDA = '003'")
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
    ''' 入金マスタ情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNKNMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intSTSFLG As Integer = 0
        Try


            If Me.rbSTSFLG1.Checked Then
                intSTSFLG = 1
                Me.lblBUNNMB001.Visible = False
                Me.lblBUNNMB002.Visible = False
                Me.lblBUNNMB003.Visible = False
                Me.txtBUNNMB001.Visible = False
                Me.txtBUNNMB002.Visible = False
                Me.txtBUNNMB003.Visible = False
                Me.Panel2.Visible = False
                Me.Panel3.Visible = False
                Me.pnlCardHakken.Visible = True
            End If

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNMST")
            sql.Append(" WHERE ")
            sql.Append(" STSFLG = '" & intSTSFLG & "'")
            sql.Append(" ORDER BY NKNKBN,SEQNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Dim dr() As DataRow

            '//【タグ区分001】//
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.txtNKNKN01_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム01
                Me.txtPREMKN01_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント01
                Me.txtPOINT01_001.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税01
                Me.txtNKNTAX01_001.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.txtNKNKN02_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム02
                Me.txtPREMKN02_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント02
                Me.txtPOINT02_001.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税02
                Me.txtNKNTAX02_001.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.txtNKNKN03_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム03
                Me.txtPREMKN03_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント03
                Me.txtPOINT03_001.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税03
                Me.txtNKNTAX03_001.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.txtNKNKN04_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム04
                Me.txtPREMKN04_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント04
                Me.txtPOINT04_001.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税04
                Me.txtNKNTAX04_001.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.txtNKNKN05_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム05
                Me.txtPREMKN05_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント05
                Me.txtPOINT05_001.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税05
                Me.txtNKNTAX05_001.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.txtNKNKN06_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム06
                Me.txtPREMKN06_001.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント06
                Me.txtPOINT06_001.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税06
                Me.txtNKNTAX06_001.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If


            '//【タグ区分002】//
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.txtNKNKN01_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム01
                Me.txtPREMKN01_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント01
                Me.txtPOINT01_002.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税01
                Me.txtNKNTAX01_002.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.txtNKNKN02_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム02
                Me.txtPREMKN02_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント02
                Me.txtPOINT02_002.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税02
                Me.txtNKNTAX02_002.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.txtNKNKN03_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム03
                Me.txtPREMKN03_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント03
                Me.txtPOINT03_002.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税03
                Me.txtNKNTAX03_002.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.txtNKNKN04_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム04
                Me.txtPREMKN04_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント04
                Me.txtPOINT04_002.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税04
                Me.txtNKNTAX04_002.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.txtNKNKN05_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム05
                Me.txtPREMKN05_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント05
                Me.txtPOINT05_002.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税05
                Me.txtNKNTAX05_002.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.txtNKNKN06_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム06
                Me.txtPREMKN06_002.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント06
                Me.txtPOINT06_002.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税06
                Me.txtNKNTAX06_002.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If

            '//【タグ区分003】//
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 1")
            If dr.Length > 0 Then
                '入金額01
                Me.txtNKNKN01_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム01
                Me.txtPREMKN01_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント01
                Me.txtPOINT01_003.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税01
                Me.txtNKNTAX01_003.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 2")
            If dr.Length > 0 Then
                '入金額02
                Me.txtNKNKN02_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム02
                Me.txtPREMKN02_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント02
                Me.txtPOINT02_003.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税02
                Me.txtNKNTAX02_003.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 3")
            If dr.Length > 0 Then
                '入金額03
                Me.txtNKNKN03_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム03
                Me.txtPREMKN03_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント03
                Me.txtPOINT03_003.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税03
                Me.txtNKNTAX03_003.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 4")
            If dr.Length > 0 Then
                '入金額04
                Me.txtNKNKN04_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム04
                Me.txtPREMKN04_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント04
                Me.txtPOINT04_003.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税04
                Me.txtNKNTAX04_003.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 5")
            If dr.Length > 0 Then
                '入金額05
                Me.txtNKNKN05_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム05
                Me.txtPREMKN05_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント05
                Me.txtPOINT05_003.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税05
                Me.txtNKNTAX05_003.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 6")
            If dr.Length > 0 Then
                '入金額06
                Me.txtNKNKN06_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
                'プレミアム06
                Me.txtPREMKN06_003.Text = CType(dr(0).Item("PREMKN").ToString, Integer).ToString("#,##0")
                'ポイント06
                Me.txtPOINT06_003.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                '消費税06
                Me.txtNKNTAX06_003.Text = CType(dr(0).Item("NKNTAX").ToString, Integer).ToString("#,##0")
            End If

            'カード発行手数料
            Me.txtCARDFEE.Text = UIUtility.SYSTEM.CARDFEE.ToString("#,##0")

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
        Dim intSTSFLG As Integer = 0
        Try
            If rbSTSFLG1.Checked Then intSTSFLG = 1

            'トランザクション開始
            iDatabase.BeginTransaction()

            '商品分類マスタ　区分名変更
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB001.Text & "' WHERE BMNCD = '002' AND BUNCDA = '003' AND BUNCDB = '001'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB002.Text & "' WHERE BMNCD = '002' AND BUNCDA = '003' AND BUNCDB = '002'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB003.Text & "' WHERE BMNCD = '002' AND BUNCDA = '003' AND BUNCDB = '003'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If

            If Not iDatabase.ExecuteUpdate("DELETE FROM NKNMST WHERE STSFLG = '" & intSTSFLG & "'") Then
                Return False
            End If

            '入金マスタ更新
            Dim NKNKN As TextBox = Nothing
            Dim PREMKN As TextBox = Nothing
            Dim POINT As TextBox = Nothing
            Dim NKNTAX As TextBox = Nothing
            Dim intSEQNO As Integer = 0

            'タグ【001】
            intSEQNO = 1
            For i As Integer = 1 To 6
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        'シーケンス【01】
                        If String.IsNullOrEmpty(Me.txtNKNKN01_001.Text) Or Me.txtNKNKN01_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN01_001
                        PREMKN = Me.txtPREMKN01_001
                        POINT = Me.txtPOINT01_001
                        NKNTAX = Me.txtNKNTAX01_001
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtNKNKN02_001.Text) Or Me.txtNKNKN02_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN02_001
                        PREMKN = Me.txtPREMKN02_001
                        POINT = Me.txtPOINT02_001
                        NKNTAX = Me.txtNKNTAX02_001
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtNKNKN03_001.Text) Or Me.txtNKNKN03_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN03_001
                        PREMKN = Me.txtPREMKN03_001
                        POINT = Me.txtPOINT03_001
                        NKNTAX = Me.txtNKNTAX03_001
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtNKNKN04_001.Text) Or Me.txtNKNKN04_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN04_001
                        PREMKN = Me.txtPREMKN04_001
                        POINT = Me.txtPOINT04_001
                        NKNTAX = Me.txtNKNTAX04_001
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtNKNKN05_001.Text) Or Me.txtNKNKN05_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN05_001
                        PREMKN = Me.txtPREMKN05_001
                        POINT = Me.txtPOINT05_001
                        NKNTAX = Me.txtNKNTAX05_001
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtNKNKN06_001.Text) Or Me.txtNKNKN06_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN06_001
                        PREMKN = Me.txtPREMKN06_001
                        POINT = Me.txtPOINT06_001
                        NKNTAX = Me.txtNKNTAX06_001
                End Select
                strSQL01 &= "INSERT INTO NKNMST("
                strSQL02 &= " VALUES("
                '種別フラグ
                strSQL01 &= "STSFLG,"
                strSQL02 &= "'" & intSTSFLG & "',"
                'タグ区分
                strSQL01 &= "NKNKBN,"
                strSQL02 &= "'001',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                '入金名称
                strSQL01 &= "NKNNM,"
                strSQL02 &= "'入金 " & NKNKN.Text & "円',"
                '入金
                strSQL01 &= "NKNKN,"
                strSQL02 &= CType(NKNKN.Text, Integer) & ","
                'プレミアム金額
                strSQL01 &= "PREMKN,"
                If String.IsNullOrEmpty(PREMKN.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(PREMKN.Text, Integer) & ","
                End If
                'ポイント
                strSQL01 &= "POINT,"
                If String.IsNullOrEmpty(POINT.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(POINT.Text, Integer) & ","
                End If
                '消費税
                strSQL01 &= "NKNTAX,"
                If String.IsNullOrEmpty(NKNTAX.Text) Then
                    strSQL02 &= "0,"
                Else
                    strSQL02 &= CType(NKNTAX.Text, Integer) & ","
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

            If intSTSFLG.Equals(0) Then
                'タグ【002】
                intSEQNO = 1
                For i As Integer = 1 To 6
                    strSQL01 = String.Empty
                    strSQL02 = String.Empty
                    Select Case i
                        Case 1
                            'シーケンス【01】
                            If String.IsNullOrEmpty(Me.txtNKNKN01_002.Text) Or Me.txtNKNKN01_002.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN01_002
                            PREMKN = Me.txtPREMKN01_002
                            POINT = Me.txtPOINT01_002
                            NKNTAX = Me.txtNKNTAX01_002
                        Case 2
                            'シーケンス【02】
                            If String.IsNullOrEmpty(Me.txtNKNKN02_002.Text) Or Me.txtNKNKN02_002.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN02_002
                            PREMKN = Me.txtPREMKN02_002
                            POINT = Me.txtPOINT02_002
                            NKNTAX = Me.txtNKNTAX02_002
                        Case 3
                            'シーケンス【03】
                            If String.IsNullOrEmpty(Me.txtNKNKN03_002.Text) Or Me.txtNKNKN03_002.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN03_002
                            PREMKN = Me.txtPREMKN03_002
                            POINT = Me.txtPOINT03_002
                            NKNTAX = Me.txtNKNTAX03_002
                        Case 4
                            'シーケンス【04】
                            If String.IsNullOrEmpty(Me.txtNKNKN04_002.Text) Or Me.txtNKNKN04_002.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN04_002
                            PREMKN = Me.txtPREMKN04_002
                            POINT = Me.txtPOINT04_002
                            NKNTAX = Me.txtNKNTAX04_002
                        Case 5
                            'シーケンス【05】
                            If String.IsNullOrEmpty(Me.txtNKNKN05_002.Text) Or Me.txtNKNKN05_002.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN05_002
                            PREMKN = Me.txtPREMKN05_002
                            POINT = Me.txtPOINT05_002
                            NKNTAX = Me.txtNKNTAX05_002
                        Case 6
                            'シーケンス【06】
                            If String.IsNullOrEmpty(Me.txtNKNKN06_002.Text) Or Me.txtNKNKN06_002.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN06_002
                            PREMKN = Me.txtPREMKN06_002
                            POINT = Me.txtPOINT06_002
                            NKNTAX = Me.txtNKNTAX06_002
                    End Select
                    strSQL01 &= "INSERT INTO NKNMST("
                    strSQL02 &= " VALUES("
                    '種別フラグ
                    strSQL01 &= "STSFLG,"
                    strSQL02 &= "'0',"
                    'タグ区分
                    strSQL01 &= "NKNKBN,"
                    strSQL02 &= "'002',"
                    'シーケンス番号
                    strSQL01 &= "SEQNO,"
                    strSQL02 &= intSEQNO & ","
                    '入金名称
                    strSQL01 &= "NKNNM,"
                    strSQL02 &= "'入金 " & NKNKN.Text & "円',"
                    '入金
                    strSQL01 &= "NKNKN,"
                    strSQL02 &= CType(NKNKN.Text, Integer) & ","
                    'プレミアム金額
                    strSQL01 &= "PREMKN,"
                    If String.IsNullOrEmpty(PREMKN.Text) Then
                        strSQL02 &= "0,"
                    Else
                        strSQL02 &= CType(PREMKN.Text, Integer) & ","
                    End If
                    'ポイント
                    strSQL01 &= "POINT,"
                    If String.IsNullOrEmpty(POINT.Text) Then
                        strSQL02 &= "0,"
                    Else
                        strSQL02 &= CType(POINT.Text, Integer) & ","
                    End If
                    '消費税
                    strSQL01 &= "NKNTAX,"
                    If String.IsNullOrEmpty(NKNTAX.Text) Then
                        strSQL02 &= "0,"
                    Else
                        strSQL02 &= CType(NKNTAX.Text, Integer) & ","
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
                            If String.IsNullOrEmpty(Me.txtNKNKN01_003.Text) Or Me.txtNKNKN01_003.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN01_003
                            PREMKN = Me.txtPREMKN01_003
                            POINT = Me.txtPOINT01_003
                            NKNTAX = Me.txtNKNTAX01_003
                        Case 2
                            'シーケンス【02】
                            If String.IsNullOrEmpty(Me.txtNKNKN02_003.Text) Or Me.txtNKNKN02_003.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN02_003
                            PREMKN = Me.txtPREMKN02_003
                            POINT = Me.txtPOINT02_003
                            NKNTAX = Me.txtNKNTAX02_003
                        Case 3
                            'シーケンス【03】
                            If String.IsNullOrEmpty(Me.txtNKNKN03_003.Text) Or Me.txtNKNKN03_003.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN03_003
                            PREMKN = Me.txtPREMKN03_003
                            POINT = Me.txtPOINT03_003
                            NKNTAX = Me.txtNKNTAX03_003
                        Case 4
                            'シーケンス【04】
                            If String.IsNullOrEmpty(Me.txtNKNKN04_003.Text) Or Me.txtNKNKN04_003.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN04_003
                            PREMKN = Me.txtPREMKN04_003
                            POINT = Me.txtPOINT04_003
                            NKNTAX = Me.txtNKNTAX04_003
                        Case 5
                            'シーケンス【05】
                            If String.IsNullOrEmpty(Me.txtNKNKN05_003.Text) Or Me.txtNKNKN05_003.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN05_003
                            PREMKN = Me.txtPREMKN05_003
                            POINT = Me.txtPOINT05_003
                            NKNTAX = Me.txtNKNTAX05_003
                        Case 6
                            'シーケンス【06】
                            If String.IsNullOrEmpty(Me.txtNKNKN06_003.Text) Or Me.txtNKNKN06_003.Text.Equals("0") Then Continue For
                            NKNKN = Me.txtNKNKN06_003
                            PREMKN = Me.txtPREMKN06_003
                            POINT = Me.txtPOINT06_003
                            NKNTAX = Me.txtNKNTAX06_003
                    End Select
                    strSQL01 &= "INSERT INTO NKNMST("
                    strSQL02 &= " VALUES("
                    '種別フラグ
                    strSQL01 &= "STSFLG,"
                    strSQL02 &= "'0',"
                    'タグ区分
                    strSQL01 &= "NKNKBN,"
                    strSQL02 &= "'003',"
                    'シーケンス番号
                    strSQL01 &= "SEQNO,"
                    strSQL02 &= intSEQNO & ","
                    '入金名称
                    strSQL01 &= "NKNNM,"
                    strSQL02 &= "'入金 " & NKNKN.Text & "円',"
                    '入金
                    strSQL01 &= "NKNKN,"
                    strSQL02 &= CType(NKNKN.Text, Integer) & ","
                    'プレミアム金額
                    strSQL01 &= "PREMKN,"
                    If String.IsNullOrEmpty(PREMKN.Text) Then
                        strSQL02 &= "0,"
                    Else
                        strSQL02 &= CType(PREMKN.Text, Integer) & ","
                    End If
                    'ポイント
                    strSQL01 &= "POINT,"
                    If String.IsNullOrEmpty(POINT.Text) Then
                        strSQL02 &= "0,"
                    Else
                        strSQL02 &= CType(POINT.Text, Integer) & ","
                    End If
                    '消費税
                    strSQL01 &= "NKNTAX,"
                    If String.IsNullOrEmpty(NKNTAX.Text) Then
                        strSQL02 &= "0,"
                    Else
                        strSQL02 &= CType(NKNTAX.Text, Integer) & ","
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
            Else
                If String.IsNullOrEmpty(Me.txtCARDFEE.Text) Then
                    UIUtility.SYSTEM.CARDFEE = 0
                Else
                    UIUtility.SYSTEM.CARDFEE = CType(Me.txtCARDFEE.Text, Integer)
                End If
                sql.Clear()
                sql.Append("UPDATE SYSMTA SET ")
                'カード発行手数料
                sql.Append("CARDFEE = " & UIUtility.SYSTEM.CARDFEE & ",")
                sql.Append("UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
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

