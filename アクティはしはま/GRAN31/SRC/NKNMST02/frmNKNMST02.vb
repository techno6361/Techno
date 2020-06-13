Imports TECHNO.DataBase

Public Class frmNKNMST02

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

            MyBase.l_Title_FormName = "サービス入金マスタ登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "サービス入金マスタ登録"

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
    Private Sub frmNKNMST02_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
    Private Sub frmNKNMST02_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            _dtHINMTB.Dispose()

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



            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM NKNMST WHERE STSFLG = '9'")
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

            Dim dr As DataRow()

            Me.lblCKBNAME01_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 1)
            If dr.Length > 0 Then Me.lblCKBNAME01_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME02_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 2)
            If dr.Length > 0 Then Me.lblCKBNAME02_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME03_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 3)
            If dr.Length > 0 Then Me.lblCKBNAME03_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME04_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 4)
            If dr.Length > 0 Then Me.lblCKBNAME04_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME05_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 5)
            If dr.Length > 0 Then Me.lblCKBNAME05_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME06_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 6)
            If dr.Length > 0 Then Me.lblCKBNAME06_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME07_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 7)
            If dr.Length > 0 Then Me.lblCKBNAME07_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME08_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 8)
            If dr.Length > 0 Then Me.lblCKBNAME08_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME09_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 9)
            If dr.Length > 0 Then Me.lblCKBNAME09_004.Text = dr(0).Item("CKBNAME").ToString
            Me.lblCKBNAME10_004.Text = String.Empty
            dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & 10)
            If dr.Length > 0 Then Me.lblCKBNAME10_004.Text = dr(0).Item("CKBNAME").ToString

            Me.txtNKNKN01_004.Text = String.Empty
            Me.txtNKNKN02_004.Text = String.Empty
            Me.txtNKNKN03_004.Text = String.Empty
            Me.txtNKNKN04_004.Text = String.Empty
            Me.txtNKNKN05_004.Text = String.Empty
            Me.txtNKNKN06_004.Text = String.Empty
            Me.txtNKNKN07_004.Text = String.Empty
            Me.txtNKNKN08_004.Text = String.Empty
            Me.txtNKNKN09_004.Text = String.Empty
            Me.txtNKNKN10_004.Text = String.Empty

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
            sql.Append(" AND BUNCDA = '004'")
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
    ''' <remarks></remarks>
    Private Function GetNKNMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNMST")
            sql.Append(" WHERE ")
            sql.Append(" STSFLG = '9'")
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
                'サービス入金額01
                Me.txtNKNKN01_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 2")
            If dr.Length > 0 Then
                'サービス入金額02
                Me.txtNKNKN02_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 3")
            If dr.Length > 0 Then
                'サービス入金額03
                Me.txtNKNKN03_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 4")
            If dr.Length > 0 Then
                'サービス入金額04
                Me.txtNKNKN04_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 5")
            If dr.Length > 0 Then
                'サービス入金額05
                Me.txtNKNKN05_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '001' AND SEQNO = 6")
            If dr.Length > 0 Then
                'サービス入金額06
                Me.txtNKNKN06_001.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If

            '//【タグ区分002】//
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 1")
            If dr.Length > 0 Then
                'サービス入金額01
                Me.txtNKNKN01_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 2")
            If dr.Length > 0 Then
                'サービス入金額02
                Me.txtNKNKN02_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 3")
            If dr.Length > 0 Then
                'サービス入金額03
                Me.txtNKNKN03_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 4")
            If dr.Length > 0 Then
                'サービス入金額04
                Me.txtNKNKN04_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 5")
            If dr.Length > 0 Then
                'サービス入金額05
                Me.txtNKNKN05_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '002' AND SEQNO = 6")
            If dr.Length > 0 Then
                'サービス入金額06
                Me.txtNKNKN06_002.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If

            '//【タグ区分003】//
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 1")
            If dr.Length > 0 Then
                'サービス入金額01
                Me.txtNKNKN01_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 2")
            If dr.Length > 0 Then
                'サービス入金額02
                Me.txtNKNKN02_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 3")
            If dr.Length > 0 Then
                'サービス入金額03
                Me.txtNKNKN03_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 4")
            If dr.Length > 0 Then
                'サービス入金額04
                Me.txtNKNKN04_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 5")
            If dr.Length > 0 Then
                'サービス入金額05
                Me.txtNKNKN05_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '003' AND SEQNO = 6")
            If dr.Length > 0 Then
                'サービス入金額06
                Me.txtNKNKN06_003.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If

            '//【誕生月サービス入金額】//
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 1")
            If dr.Length > 0 Then
                '種別01サービス入金額
                Me.txtNKNKN01_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 2")
            If dr.Length > 0 Then
                '種別02サービス入金額
                Me.txtNKNKN02_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 3")
            If dr.Length > 0 Then
                '種別03サービス入金額
                Me.txtNKNKN03_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 4")
            If dr.Length > 0 Then
                '種別04サービス入金額
                Me.txtNKNKN04_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 5")
            If dr.Length > 0 Then
                '種別05サービス入金額
                Me.txtNKNKN05_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 6")
            If dr.Length > 0 Then
                '種別06サービス入金額
                Me.txtNKNKN06_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 7")
            If dr.Length > 0 Then
                '種別07サービス入金額
                Me.txtNKNKN07_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 8")
            If dr.Length > 0 Then
                '種別08サービス入金額
                Me.txtNKNKN08_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 9")
            If dr.Length > 0 Then
                '種別09サービス入金額
                Me.txtNKNKN09_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
            End If
            dr = resultDt.Select("NKNKBN = '004' AND SEQNO = 10")
            If dr.Length > 0 Then
                '種別10サービス入金額
                Me.txtNKNKN10_004.Text = CType(dr(0).Item("NKNKN").ToString, Integer).ToString("#,##0")
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
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB001.Text & "' WHERE BMNCD = '002' AND BUNCDA = '004' AND BUNCDB = '001'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB002.Text & "' WHERE BMNCD = '002' AND BUNCDA = '004' AND BUNCDB = '002'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If
            sql.Clear()
            sql.Append("UPDATE HINMTB SET BUNNMB = '" & Me.txtBUNNMB003.Text & "' WHERE BMNCD = '002' AND BUNCDA = '004' AND BUNCDB = '003'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If

            If Not iDatabase.ExecuteUpdate("DELETE FROM NKNMST WHERE STSFLG = '9'") Then
                Return False
            End If

            '入金マスタ更新
            Dim NKNKN As TextBox = Nothing
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
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtNKNKN02_001.Text) Or Me.txtNKNKN02_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN02_001
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtNKNKN03_001.Text) Or Me.txtNKNKN03_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN03_001
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtNKNKN04_001.Text) Or Me.txtNKNKN04_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN04_001
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtNKNKN05_001.Text) Or Me.txtNKNKN05_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN05_001
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtNKNKN06_001.Text) Or Me.txtNKNKN06_001.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN06_001
                End Select
                strSQL01 &= "INSERT INTO NKNMST("
                strSQL02 &= " VALUES("
                '種別フラグ
                strSQL01 &= "STSFLG,"
                strSQL02 &= "'9',"
                'タグ区分
                strSQL01 &= "NKNKBN,"
                strSQL02 &= "'001',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                '入金名称
                strSQL01 &= "NKNNM,"
                strSQL02 &= "'ｻｰﾋﾞｽ入金 " & NKNKN.Text & "円',"
                '入金
                strSQL01 &= "NKNKN,"
                strSQL02 &= CType(NKNKN.Text, Integer) & ","
                'プレミアム金額
                strSQL01 &= "PREMKN,"
                strSQL02 &= "0,"
                'ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '消費税
                strSQL01 &= "NKNTAX,"
                strSQL02 &= "0,"
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
                        If String.IsNullOrEmpty(Me.txtNKNKN01_002.Text) Or Me.txtNKNKN01_002.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN01_002
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtNKNKN02_002.Text) Or Me.txtNKNKN02_002.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN02_002
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtNKNKN03_002.Text) Or Me.txtNKNKN03_002.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN03_002
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtNKNKN04_002.Text) Or Me.txtNKNKN04_002.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN04_002
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtNKNKN05_002.Text) Or Me.txtNKNKN05_002.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN05_002
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtNKNKN06_002.Text) Or Me.txtNKNKN06_002.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN06_002
                End Select
                strSQL01 &= "INSERT INTO NKNMST("
                strSQL02 &= " VALUES("
                '種別フラグ
                strSQL01 &= "STSFLG,"
                strSQL02 &= "'9',"
                'タグ区分
                strSQL01 &= "NKNKBN,"
                strSQL02 &= "'002',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                '入金名称
                strSQL01 &= "NKNNM,"
                strSQL02 &= "'ｻｰﾋﾞｽ入金 " & NKNKN.Text & "円',"
                '入金
                strSQL01 &= "NKNKN,"
                strSQL02 &= CType(NKNKN.Text, Integer) & ","
                'プレミアム金額
                strSQL01 &= "PREMKN,"
                strSQL02 &= "0,"
                'ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '消費税
                strSQL01 &= "NKNTAX,"
                strSQL02 &= "0,"
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
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtNKNKN02_003.Text) Or Me.txtNKNKN02_003.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN02_003
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtNKNKN03_003.Text) Or Me.txtNKNKN03_003.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN03_003
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtNKNKN04_003.Text) Or Me.txtNKNKN04_003.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN04_003
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtNKNKN05_003.Text) Or Me.txtNKNKN05_003.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN05_003
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtNKNKN06_003.Text) Or Me.txtNKNKN06_003.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN06_003
                End Select
                strSQL01 &= "INSERT INTO NKNMST("
                strSQL02 &= " VALUES("
                '種別フラグ
                strSQL01 &= "STSFLG,"
                strSQL02 &= "'9',"
                'タグ区分
                strSQL01 &= "NKNKBN,"
                strSQL02 &= "'003',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= intSEQNO & ","
                '入金名称
                strSQL01 &= "NKNNM,"
                strSQL02 &= "'ｻｰﾋﾞｽ入金 " & NKNKN.Text & "円',"
                '入金
                strSQL01 &= "NKNKN,"
                strSQL02 &= CType(NKNKN.Text, Integer) & ","
                'プレミアム金額
                strSQL01 &= "PREMKN,"
                strSQL02 &= "0,"
                'ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '消費税
                strSQL01 &= "NKNTAX,"
                strSQL02 &= "0,"
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

            'タグ【004】(誕生月サービス入金額)
            For i As Integer = 1 To 10
                strSQL01 = String.Empty
                strSQL02 = String.Empty
                Select Case i
                    Case 1
                        'シーケンス【01】
                        If String.IsNullOrEmpty(Me.txtNKNKN01_004.Text) Or Me.txtNKNKN01_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN01_004
                    Case 2
                        'シーケンス【02】
                        If String.IsNullOrEmpty(Me.txtNKNKN02_004.Text) Or Me.txtNKNKN02_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN02_004
                    Case 3
                        'シーケンス【03】
                        If String.IsNullOrEmpty(Me.txtNKNKN03_004.Text) Or Me.txtNKNKN03_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN03_004
                    Case 4
                        'シーケンス【04】
                        If String.IsNullOrEmpty(Me.txtNKNKN04_004.Text) Or Me.txtNKNKN04_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN04_004
                    Case 5
                        'シーケンス【05】
                        If String.IsNullOrEmpty(Me.txtNKNKN05_004.Text) Or Me.txtNKNKN05_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN05_004
                    Case 6
                        'シーケンス【06】
                        If String.IsNullOrEmpty(Me.txtNKNKN06_004.Text) Or Me.txtNKNKN06_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN06_004
                    Case 7
                        'シーケンス【07】
                        If String.IsNullOrEmpty(Me.txtNKNKN07_004.Text) Or Me.txtNKNKN07_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN07_004
                    Case 8
                        'シーケンス【08】
                        If String.IsNullOrEmpty(Me.txtNKNKN08_004.Text) Or Me.txtNKNKN08_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN08_004
                    Case 9
                        'シーケンス【09】
                        If String.IsNullOrEmpty(Me.txtNKNKN09_004.Text) Or Me.txtNKNKN09_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN09_004
                    Case 10
                        'シーケンス【10】
                        If String.IsNullOrEmpty(Me.txtNKNKN10_004.Text) Or Me.txtNKNKN10_004.Text.Equals("0") Then Continue For
                        NKNKN = Me.txtNKNKN10_004
                End Select
                strSQL01 &= "INSERT INTO NKNMST("
                strSQL02 &= " VALUES("
                '種別フラグ
                strSQL01 &= "STSFLG,"
                strSQL02 &= "'9',"
                'タグ区分
                strSQL01 &= "NKNKBN,"
                strSQL02 &= "'004',"
                'シーケンス番号
                strSQL01 &= "SEQNO,"
                strSQL02 &= i & ","
                '入金名称
                strSQL01 &= "NKNNM,"
                strSQL02 &= "'誕生月ｻｰﾋﾞｽ入金 " & NKNKN.Text & "円',"
                '入金
                strSQL01 &= "NKNKN,"
                strSQL02 &= CType(NKNKN.Text, Integer) & ","
                'プレミアム金額
                strSQL01 &= "PREMKN,"
                strSQL02 &= "0,"
                'ポイント
                strSQL01 &= "POINT,"
                strSQL02 &= "0,"
                '消費税
                strSQL01 &= "NKNTAX,"
                strSQL02 &= "0,"
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

