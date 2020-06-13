Imports System.Threading
Imports System.ComponentModel
Imports Techno.DataBase

''' <summary>
''' 無記名の顧客を作成
''' </summary>
''' <remarks></remarks>
Public Class frmNAMELESS

#Region "▼宣言部"

    Private _iDatabase As IDatabase.IMethod

    Enum eCreateResult
        ERR_GET_DATA
        ERR_SET_DATA
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

    ' バックアップ用
    Public Custmer As CustmerModel = New CustmerModel

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

            Me.mCreateCustmer.WorkerReportsProgress = True

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
            Me.Refresh()
            Me.mCreateCustmer.RunWorkerAsync()

            Thread.Sleep(1000)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 無記名顧客作成_スレッド
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mCreateCustmer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mCreateCustmer.DoWork
        Try
            Me.mCreateCustmer.ReportProgress(0)

            ' トランザクション開始
            iDatabase.BeginTransaction()

            '【新規作成モード】

            '顧客種別情報取得
            If Not GetKBMAST() Then
                e.Result = eCreateResult.ERR_GET_DATA
            End If

            ' 顧客番号とカード番号の取得
            If Not GetMANNOSEQ() Then
                e.Result = eCreateResult.ERR_GET_DATA
            End If

            ' 新規登録
            If Not InsRegister() Then
                e.Result = eCreateResult.ERR_SET_DATA
                Return
            End If

            ' コミット
            iDatabase.Commit()

            ' バックアップ成功
            Me.Custmer.Enabled = True

            e.Result = eCreateResult.SUCCESS
            Return

        Catch ex As Exception
            e.Result = eCreateResult.ERR_OTHER
            Return
        End Try
    End Sub

    ''' <summary>
    ''' 無記名顧客作成_表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mCreateCustmer_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mCreateCustmer.ProgressChanged
        Select Case e.ProgressPercentage
            Case 0
                Me.lblMSG.Text = "処理中…しばらくお待ちください。"
                Me.picImage.Image = Images.GIFLoading
        End Select
    End Sub

    ''' <summary>
    ''' 無記名顧客作成_スレッド終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mCreateCustmer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles mCreateCustmer.RunWorkerCompleted
        Select Case CType(e.Result, eCreateResult)
            Case eCreateResult.ERR_GET_DATA
                Using frm As New frmMSGBOXEx("データの取得に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eCreateResult.ERR_SET_DATA
                Using frm As New frmMSGBOXEx("データの登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eCreateResult.ERR_OTHER
                Using frm As New frmMSGBOXEx("カード発券に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eCreateResult.SUCCESS
                Me.Err = False
        End Select

        Me.Close()
    End Sub

#End Region

#Region "▼無記名カード作成メソッド"

    ''' <summary>
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            Dim ksbkb = CInt(CommonSettings.HakkenKSBKB)

            ' 存在チェック
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" WHERE NKBNO = " & ksbkb)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            ' 存在しない場合は最小値に補正
            If resultDt.Rows.Count.Equals(0) Then
                sql.Clear()
                sql.Append(" SELECT NKBNO FROM KBMAST ORDER BY NKBNO")
                resultDt = iDatabase.ExecuteRead(sql.ToString())
                If resultDt.Rows.Count <= 0 Then Return False
                ksbkb = CInt(resultDt.Rows(0)(0))
            End If

            ' バックアップ
            Me.Custmer.KBMAST = ksbkb

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 顧客番号取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetMANNOSEQ() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Append(" SELECT ")
            sql.Append(" (MANNOSEQ + 1) AS MANNO")
            sql.Append(",(DCARDSEQ + 1) AS CARDNO")
            sql.Append(" FROM SEQTRN")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            ' バックアップ

            ' +1 された値
            Me.Custmer.MANNO = resultDt.Rows(0).Item("MANNO").ToString.PadLeft(8, "0"c)
            Me.Custmer.CARDNO = resultDt.Rows(0).Item("CARDNO").ToString.PadLeft(8, "0"c)

            ' 元の値
            Me.Custmer.OLD_MANNO = (CInt(Me.Custmer.MANNO) - 1).ToString.PadLeft(8, "0"c)
            Me.Custmer.OLD_CARDNO = (CInt(Me.Custmer.CARDNO) - 1).ToString.PadLeft(8, "0"c)

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsRegister() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try

            '顧客番号カウントアップ
            sql.Clear()
            sql.Append("UPDATE SEQTRN SET MANNOSEQ = MANNOSEQ + 1,DCARDSEQ = DCARDSEQ + 1")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            strSQL1 &= "INSERT INTO CSMAST("
            strSQL2 &= " VALUES("
            '顧客番号
            strSQL1 &= "NCSNO,"
            strSQL2 &= CType(Me.Custmer.MANNO, Integer) & ","
            'カード番号
            strSQL1 &= "NCARDID,"
            strSQL2 &= CType(Me.Custmer.CARDNO, Integer) & ","
            '顧客名
            strSQL1 &= "CCSNAME,"
            strSQL2 &= "NULL,"
            '顧客名(ｶﾅ)
            strSQL1 &= "CCSKANA,"
            strSQL2 &= "NULL,"
            '顧客種別
            strSQL1 &= "NCSRANK,"
            strSQL2 &= Me.Custmer.KBMAST & ","
            '性別
            strSQL1 &= "NSEX,"
            strSQL2 &= "3,"
            'カードコメント
            strSQL1 &= "MANINFO,"
            strSQL2 &= "NULL,"
            '郵便番号
            strSQL1 &= "NZIP,"
            strSQL2 &= "NULL,"
            '住所１
            strSQL1 &= "CADDRESS1,"
            strSQL2 &= "NULL,"
            '住所２
            strSQL1 &= "CADDRESS2,"
            strSQL2 &= "NULL,"
            '電話番号
            strSQL1 &= "CTELEPHONE,"
            strSQL2 &= "NULL,"
            'FAX番号
            strSQL1 &= "CFAX,"
            strSQL2 &= "NULL,"
            '携帯電話番号
            strSQL1 &= "CPOTABLENUM,"
            strSQL2 &= "NULL,"
            '会員状態
            strSQL1 &= "NMRKBN,"
            strSQL2 &= "0,"
            '誕生日
            strSQL1 &= "DBIRTH,"
            strSQL2 &= "NULL,"
            '会員期限
            strSQL1 &= "DMEMBER,"
            strSQL2 &= "NULL,"
            '前回来場日
            strSQL1 &= "ZENENTDATE,"
            strSQL2 &= "NULL,"
            'メールアドレス
            strSQL1 &= "CEMAIL,"
            strSQL2 &= "NULL,"
            '会員メモ
            strSQL1 &= "MANCOMMENT,"
            strSQL2 &= "NULL,"
            'スクール生番号
            strSQL1 &= "DSCLMANNO,"
            strSQL2 &= "NULL,"
            '削除フラグ
            strSQL1 &= "NFLAGDEL,"
            strSQL2 &= "0,"
            'メール配信区分
            strSQL1 &= "CEMAILKBN,"
            strSQL2 &= "9,"
            '前回来店日
            strSQL1 &= "ENTDT,"
            strSQL2 &= "NULL,"
            '作成日時
            strSQL1 &= "INSDTM,"
            strSQL2 &= "NOW(),"
            '更新日
            strSQL1 &= "DUPDATE,"
            strSQL2 &= "NOW(),"
            '会員登録日
            strSQL1 &= "DENTRY,"
            strSQL2 &= "NOW(),"
            '備考１
            strSQL1 &= "BIKO1,"
            strSQL2 &= "NULL,"
            '備考２
            strSQL1 &= "BIKO2,"
            strSQL2 &= "NULL,"
            '備考３
            strSQL1 &= "BIKO3,"
            strSQL2 &= "NULL,"
            '総来場回数
            strSQL1 &= "ENTCNT,"
            strSQL2 &= "0,"
            '左打ち区分
            strSQL1 &= "DLEFTKBN,"
            strSQL2 &= "0,"
            '月間来場回数
            strSQL1 &= "ENTCNT2,"
            strSQL2 &= "0,"
            'カード期限
            strSQL1 &= "CARDLIMIT,"
            strSQL2 &= "NULL,"
            'カード管理番号
            strSQL1 &= "CARDADMINNO,"
            strSQL2 &= "NULL,"
            '協力金番号
            strSQL1 &= "KRKNO,"
            strSQL2 &= "0,"
            'ハンディキャップ
            strSQL1 &= "HANDICAP)"
            strSQL2 &= "0)"

            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
                Return False
            End If

            '金額サマリ登録
            strSQL1 = "INSERT INTO KINSMA VALUES("
            strSQL1 &= "'" & Me.Custmer.MANNO & "',"
            strSQL1 &= "NULL,"
            strSQL1 &= Me.Custmer.NKNKN & ","
            strSQL1 &= Me.Custmer.PREMKN & ","
            strSQL1 &= "NOW(),"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If
            'ポイントサマリ登録
            strSQL1 = "INSERT INTO DPOINTSMA VALUES("
            strSQL1 &= "'" & Me.Custmer.MANNO & "',"
            strSQL1 &= "NULL,"
            strSQL1 &= Me.Custmer.SRTPO & ","
            strSQL1 &= "NOW(),"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If

            '無記名ｶｰﾄﾞ作成ﾄﾗﾝ登録
            strSQL1 = "INSERT INTO LESSCTRN VALUES("
            strSQL1 &= Me.Custmer.MANNO & ","
            strSQL1 &= Me.Custmer.CARDNO & ","
            strSQL1 &= Me.Custmer.KBMAST & ","
            strSQL1 &= "'" & Me.Custmer.KBMAST.ToString & "',"
            strSQL1 &= Me.Custmer.NKNKN & ","
            strSQL1 &= Me.Custmer.PREMKN & ","
            strSQL1 &= Me.Custmer.SRTPO & ","
            strSQL1 &= "0,"
            strSQL1 &= "NULL,"
            strSQL1 &= "NULL,"
            strSQL1 &= "NOW())"
            If Not iDatabase.ExecuteUpdate(strSQL1) Then
                iDatabase.RollBack()
                Return False
            End If

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try
    End Function

#End Region

End Class
