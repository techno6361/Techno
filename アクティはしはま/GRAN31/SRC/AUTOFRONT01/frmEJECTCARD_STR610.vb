Imports System.Threading
Imports System.ComponentModel
Imports Techno.DataBase

Public Class frmEJECTCARD_STR610

#Region "▼宣言部"

    Private _iDatabase As IDatabase.IMethod

    Private _dcSTR610 As New Techno.DeviceControls.STR610Control

    Enum eEjectResult
        ERR_GET_DATA
        ERR_SET_DATA
        ERR_EJECT_CARD
        ERR_OTHER
        SUCCESS
    End Enum

    ' カード発券機のエラー
    Private _cardDispenserUnitMessage As Techno.DeviceControls.STR610Control.UNIT_STATUS

    Public Property iDatabase As IDatabase.IMethod
        Get
            Return _iDatabase
        End Get
        Set(ByVal value As IDatabase.IMethod)
            _iDatabase = value
        End Set
    End Property

    Public Property dcSTR610 As Techno.DeviceControls.STR610Control
        Get
            Return _dcSTR610
        End Get
        Set(ByVal value As Techno.DeviceControls.STR610Control)
            _dcSTR610 = value
        End Set
    End Property

    Public Property Err As Boolean = False

#End Region

#Region "▼ コンストラクタ"

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        ' すべてのコントロールにダブルバッファリングを有効化
        For Each c As Control In GetAllControls(Me)
            EnableDoubleBuffering(c)
        Next

    End Sub

#End Region

#Region "▼ イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEJECTCARD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' イメージの位置調整
            Dim x = CInt((frmBase.ScreenSize.Width / 2) - (Me.picImage.Width / 2))
            Me.picImage.Location = New Point(x, Me.picImage.Location.Y)

            Me.mEjectCard.WorkerReportsProgress = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEJECTCARD_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.mEjectCard.RunWorkerAsync()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード発券
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mEjectCard_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mEjectCard.DoWork

        ' DB主キー
        Dim udndt = DateTime.Now.ToString("yyyyMMdd")
        Dim udnno As Integer = 0
        Dim insdtm = DateTime.Now
        Dim insdtmstr = DateTime.Now.ToString

        Me.mEjectCard.ReportProgress(0)

        ' *** DB処理
        Try

            ' トランザクション開始
            _iDatabase.BeginTransaction()

            ' データベースの更新
            If Not UpdateSEQTRN() Then
                e.Result = -1
                Return
            End If
            Try
                udnno = GetUDNNO()
            Catch ex As Exception
                _iDatabase.RollBack()
                e.Result = eEjectResult.ERR_GET_DATA
                Return
            End Try
            If Not InsDUDNTRN_ForCardDispenser(udndt, udnno, insdtm, insdtmstr) Then
                e.Result = eEjectResult.ERR_SET_DATA
                Return
            End If
            If Not InsDENTRA_ForCardDispenser(udndt, udnno, insdtm, insdtmstr) Then
                e.Result = eEjectResult.ERR_SET_DATA
                Return
            End If
            If Not UpdateREPOCHARGE_M(udndt) Then
                e.Result = eEjectResult.ERR_SET_DATA
                Return
            End If

        Catch ex As Exception
            _iDatabase.RollBack()
            e.Result = eEjectResult.ERR_SET_DATA
            Return
        End Try

        ' *** カード発券処理
        Try
            e.Result = eEjectResult.ERR_OTHER

            ' カード待ち受け状態をスキップ
            _dcSTR610.EjectCard(0)

            Thread.Sleep(100)

            ' カード発券後、問題がなければ引き抜かれるまでの②のループに入る(ループに入った時点でデータベースをコミット)
            ' 何らかのエラーが発生した場合①でエラーメッセージ＆ロールバック

            ' ①
            Dim result = _dcSTR610.GetUnitStatus()
            If Not _dcSTR610.UnitMessage = Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_41H Then
                If Not result Then
                    ' 【カード発券エラー】
                    _iDatabase.RollBack()
                    Throw New Exception
                    Return
                End If
            End If

            ' カードが早く抜かれすぎた時の対策
            ' 1: カード排出 2: カード引き抜き命令終了 3: カード引き抜き待機中
            Dim stage = _dcSTR610._ejectStage

            ' ②
            If stage >= 2 Then
                ' 【カード発券成功】
                _iDatabase.Commit()

                Me.mEjectCard.ReportProgress(1)
                Sound.PlayReceiveCard()

                ' カードを引き抜くまでループ
                Do
                    Application.DoEvents() ' 多重クリック対策
                    result = _dcSTR610.GetUnitStatus()
                    If Not _dcSTR610.UnitMessage = Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_41H Then
                        If Not result Then
                            ' 【カード発券エラー】
                            Throw New Exception
                            Return
                        End If
                        Exit Do
                    End If
                Loop

            Else
                ' 【カード発券エラー】
                _iDatabase.RollBack()
                Throw New Exception
                Return
            End If

            Thread.Sleep(1000)
            e.Result = eEjectResult.SUCCESS
            Return

        Catch ex As Exception
            e.Result = eEjectResult.ERR_EJECT_CARD
            Return

        End Try

    End Sub

    Private Sub mEjectCard_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mEjectCard.ProgressChanged
        Select Case e.ProgressPercentage
            Case 0
                Me.lblMSG.Text = "カード発券中…"
                Me.picImage.Image = Images.GIFLoading
            Case 1
                Me.lblMSG.Text = "カードをお受け取りください。"
                Me.picImage.Image = Images.GIFEjectCard
        End Select
    End Sub

    Private Sub mEjectCard_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles mEjectCard.RunWorkerCompleted
        Select Case CType(e.Result, eEjectResult)
            Case eEjectResult.ERR_GET_DATA
                Using frm As New frmMSGBOXEx("データの取得に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.ERR_SET_DATA
                Using frm As New frmMSGBOXEx("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.ERR_EJECT_CARD
                Using frm As New frmMSGBOXEx("カード発券に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.ERR_OTHER
                Using frm As New frmMSGBOXEx("カード発券に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.SUCCESS
                Me.Err = False
        End Select

        Me.Close()
    End Sub

#End Region

#Region "▼ 関数定義"

    ''' <summary>
    ''' 売上トランに新規作成(カード発券機用)
    ''' </summary>
    ''' <param name="key_udndt"></param>
    ''' <param name="key_udnno"></param>
    ''' <param name="key_insdtm"></param>
    ''' <param name="key_insdtmstr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsDUDNTRN_ForCardDispenser(ByVal key_udndt As String, ByVal key_udnno As Integer, ByVal key_insdtm As Date, ByVal key_insdtmstr As String) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim datkb = "1"
            Dim bmncd = "002"
            Dim buncda = "010"
            Dim buncdb = "001"
            Dim buncdc = "001"
            Dim tktkbn = "001"
            Dim udnkn = 0
            Dim tktsu = 1
            Dim udnakn = 0
            Dim udnbkn = 0
            Dim udnzkn = 0
            Dim hinzeikb = "2"
            Dim point = 0
            Dim nyukn = 0
            Dim turikn = 0
            Dim koteikbn = "0"
            Dim udnkbn = "0"
            Dim cpaykbn = "0"
            Dim hostname = My.Computer.Name
            Dim drakbn = "0"
            Dim hinnma = "カード発券"
            Dim premkn = 0

            sql.Clear()
            sql.Append(" INSERT INTO DUDNTRN (")
            sql.Append(" DATKB, UDNDT, UDNNO, BMNCD, BUNCDA, BUNCDB, BUNCDC, TKTKBN, UDNKN, TKTSU,")
            sql.Append(" INSDTMSTR, INSDTM, UDNAKN, UDNBKN, UDNZKN, HINZEIKB, POINT, NYUKN, TURIKN,")
            sql.Append(" KOTEIKBN, UDNKBN, CPAYKBN, HOSTNAME, DRAKBN, HINNMA, PREMKN")
            sql.Append(" )")
            sql.Append(" VALUES(")
            sql.Append(" '" & datkb & "',")
            sql.Append(" '" & key_udndt & "',")
            sql.Append(" " & key_udnno & ",")
            sql.Append(" '" & bmncd & "',")
            sql.Append(" '" & buncda & "',")
            sql.Append(" '" & buncdb & "',")
            sql.Append(" '" & buncdc & "',")
            sql.Append(" '" & tktkbn & "',")
            sql.Append(" " & udnkn & ",")
            sql.Append(" " & tktsu & ",")
            sql.Append(" '" & key_insdtmstr & "',")
            sql.Append(" '" & key_insdtm & "',")
            sql.Append(" " & udnakn & ",")
            sql.Append(" " & udnbkn & ",")
            sql.Append(" " & udnzkn & ",")
            sql.Append(" '" & hinzeikb & "',")
            sql.Append(" " & point & ",")
            sql.Append(" " & nyukn & ",")
            sql.Append(" " & turikn & ",")
            sql.Append(" '" & koteikbn & "',")
            sql.Append(" '" & udnkbn & "',")
            sql.Append(" '" & cpaykbn & "',")
            sql.Append(" '" & hostname & "',")
            sql.Append(" '" & drakbn & "',")
            sql.Append(" '" & hinnma & "',")
            sql.Append(" " & premkn & " ")
            sql.Append(" )")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 伝票トランに新規作成(カード発券機用)
    ''' </summary>
    ''' <param name="key_udndt"></param>
    ''' <param name="key_udnno"></param>
    ''' <param name="key_insdtm"></param>
    ''' <param name="key_insdtmstr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsDENTRA_ForCardDispenser(ByVal key_udndt As String, ByVal key_udnno As Integer, ByVal key_insdtm As Date, ByVal key_insdtmstr As String) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            Dim datkb = "1"
            Dim linno = 1
            Dim bmncd = "002"
            Dim buncda = "010"
            Dim buncdb = "001"
            Dim buncdc = "001"
            Dim tktkbn = "001"
            Dim tktnma = "カード発券"
            Dim udnkn = 0
            Dim tktsu = 0
            Dim udnakn = 0
            Dim udnbkn = 0
            Dim udnzkn = 0
            Dim hinzeikb = "2"
            Dim point = 0
            Dim koteikbn = "0"
            Dim udnkbn = "3"
            Dim cpaykbn = "0"
            Dim hostname = My.Computer.Name
            Dim drakbn = "0"
            Dim hinnma = "カード発券"
            Dim premkn = 0

            sql.Clear()
            sql.Append(" INSERT INTO DENTRA (")
            sql.Append(" DATKB, UDNDT, UDNNO, LINNO, BMNCD, BUNCDA, BUNCDB, BUNCDC, TKTKBN, TKTNMA, UDNKN, TKTSU,")
            sql.Append(" INSDTMSTR, INSDTM, UDNAKN, UDNBKN, UDNZKN, HINZEIKB, POINT,")
            sql.Append(" KOTEIKBN, UDNKBN, CPAYKBN, HOSTNAME, DRAKBN, HINNMA, PREMKN")
            sql.Append(" )")
            sql.Append(" VALUES(")
            sql.Append(" '" & datkb & "',")
            sql.Append(" '" & key_udndt & "',")
            sql.Append(" " & key_udnno & ",")
            sql.Append(" " & linno & ",")
            sql.Append(" '" & bmncd & "',")
            sql.Append(" '" & buncda & "',")
            sql.Append(" '" & buncdb & "',")
            sql.Append(" '" & buncdc & "',")
            sql.Append(" '" & tktkbn & "',")
            sql.Append(" '" & tktnma & "',")
            sql.Append(" " & udnkn & ",")
            sql.Append(" " & tktsu & ",")
            sql.Append(" '" & key_insdtmstr & "',")
            sql.Append(" '" & key_insdtm & "',")
            sql.Append(" " & udnakn & ",")
            sql.Append(" " & udnbkn & ",")
            sql.Append(" " & udnzkn & ",")
            sql.Append(" '" & hinzeikb & "',")
            sql.Append(" " & point & ",")
            sql.Append(" '" & koteikbn & "',")
            sql.Append(" '" & udnkbn & "',")
            sql.Append(" '" & cpaykbn & "',")
            sql.Append(" '" & hostname & "',")
            sql.Append(" '" & drakbn & "',")
            sql.Append(" '" & hinnma & "',")
            sql.Append(" " & premkn & " ")
            sql.Append(" )")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 帳票用入金機履歴更新(ｶｰﾄﾞ発行回数)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateREPOCHARGE_M(ByVal key_udndt As String) As Boolean
        Try
            Dim sql = New System.Text.StringBuilder

            sql.Clear()
            sql.Append("UPDATE REPOCHARGE_M SET HAKKOKAISU = HAKKOKAISU + 1 WHERE CHARGEDAY = '" & key_udndt & "' AND HOSTNAME = '" & My.Computer.Name & "'")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 伝票番号の取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUDNNO() As Integer
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEQTRN")
            Dim dt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If Not dt.Rows.Count > 0 Then
                Return Nothing
            Else
                Return CInt(dt.AsEnumerable.Max(Function(x As DataRow) x("DENNOSEQ")))
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' SEQTRNの伝票番号の更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSEQTRN() As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE SEQTRN SET DENNOSEQ = DENNOSEQ + 1")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function UpdateDUDNTRN_ForCardDispenser(ByVal udndt As String, ByVal udnno As Integer, ByVal insdtm As Date) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE DUDNTRN SET DATKB = '9'")
            sql.Append(" WHERE")
            sql.Append(" UDNDT = '" & udndt & "' AND")
            sql.Append(" UDNNO = " & udnno & " AND")
            sql.Append(" INSDTM = '" & insdtm.ToString & "' ")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function UpdateDENTRA_ForCardDispenser(ByVal udndt As String, ByVal udnno As Integer, ByVal insdtm As Date) As Boolean
        Dim sql = New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" UPDATE DENTRA SET DATKB = '9'")
            sql.Append(" WHERE")
            sql.Append(" UDNDT = '" & udndt & "' AND")
            sql.Append(" UDNNO = " & udnno & " AND")
            sql.Append(" LINNO = " & 1 & " AND")
            sql.Append(" INSDTM = '" & insdtm.ToString & "' ")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
