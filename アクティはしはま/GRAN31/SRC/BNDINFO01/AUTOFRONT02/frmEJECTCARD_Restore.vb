Imports System.Threading
Imports System.ComponentModel
Imports Techno.DataBase

''' <summary>
''' カード排出でエラーが起こった場合、複数テーブルを一斉に復元する
''' </summary>
''' <remarks></remarks>
Public Class frmEJECTCARD_Restore

#Region "▼宣言部"

    Private _iDatabase As IDatabase.IMethod

    Enum eCreateResult
        ERR_GET_DATA
        ERR_SET_DATA
        ERR_OTHER
        SUCCESS
    End Enum

    Public Property iDatabase As IDatabase.IMethod
        Get
            Return _iDatabase
        End Get
        Set(ByVal value As IDatabase.IMethod)
            _iDatabase = value
        End Set
    End Property

    ' 復元用
    Public Property Custmer As CustmerModel
    Public Property DUDNTRN As DUDNTRNModel
    Public Property DENTRA As DENTRAModel
    Public Property REPOCHARGE_M As REPOCHARGE_MModel
    Public Property SEQTRN As SEQTRNModel

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
            Me.mCreateCustmer.RunWorkerAsync()

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

            ' 情報の復元
            If Not RestoreRegister() Then
                e.Result = eCreateResult.ERR_SET_DATA
                Return
            End If

            ' コミット
            iDatabase.Commit()

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
            Case eCreateResult.ERR_SET_DATA
                Using frm As New frmMSGBOXEx("データの復元に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eCreateResult.ERR_OTHER
                Using frm As New frmMSGBOXEx("データの復元に失敗しました。", 3)
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
    ''' 復元
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RestoreRegister() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            
            ' 新規作成した無記名顧客を削除
            If Me.Custmer.Enabled Then

                Dim key1 = Me.Custmer.MANNO
                Dim key2 = Me.Custmer.CARDNO

                ' 顧客番号、カード番号を元に戻す
                sql.Clear()
                sql.Append(String.Format("UPDATE SEQTRN SET MANNOSEQ = {0},DCARDSEQ = {1}", CInt(Me.Custmer.OLD_MANNO), CInt(Me.Custmer.OLD_CARDNO)))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

                ' 顧客マスタ削除
                sql.Clear()
                sql.Append(String.Format("DELETE FROM CSMAST WHERE NCSNO = {0} AND NCARDID = {1}", CInt(key1), CInt(key2)))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

                ' 金額サマリ削除
                sql.Clear()
                sql.Append(String.Format("DELETE FROM KINSMA WHERE MANNO = '{0}'", key1))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

                ' ポイントサマリ削除
                sql.Clear()
                sql.Append(String.Format("DELETE FROM DPOINTSMA WHERE MANNO = '{0}'", key1))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

                ' 無記名ｶｰﾄﾞ作成ﾄﾗﾝ削除
                sql.Clear()
                sql.Append(String.Format("DELETE FROM LESSCTRN WHERE NCSNO = {0} AND CARDNO = {1}", CInt(key1), CInt(key2)))
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

            End If

            ' *** ここから下は未使用(frmChargeExでロールバックを行うため) ***

            '' DUDNTRN
            'If Me.DUDNTRN.Enabled Then
            '    sql.Clear()
            '    sql.Append(String.Format(" DELETE FROM DUDNTRN"))
            '    sql.Append(String.Format(" WHERE DATKB = '{0}'", DUDNTRN.DATKB))
            '    sql.Append(String.Format(" AND UDNDT = '{0}'", DUDNTRN.UDNDT))
            '    sql.Append(String.Format(" AND UDNNO = {0}", DUDNTRN.UDNNO))
            '    sql.Append(String.Format(" AND INSDTM = '{0}'", DUDNTRN.INSDTM))
            '    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
            '        iDatabase.RollBack()
            '        Return False
            '    End If
            'End If

            '' DENTRA
            'If Me.DENTRA.Enabled Then
            '    sql.Clear()
            '    sql.Append(String.Format(" DELETE FROM DENTRA"))
            '    sql.Append(String.Format(" WHERE DATKB = '{0}'", DENTRA.DATKB))
            '    sql.Append(String.Format(" AND UDNDT = '{0}'", DENTRA.UDNDT))
            '    sql.Append(String.Format(" AND UDNNO = {0}", DENTRA.UDNNO))
            '    sql.Append(String.Format(" AND LINNO = {0}", DENTRA.LINNO))
            '    sql.Append(String.Format(" AND INSDTM = '{0}'", DENTRA.INSDTM))
            '    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
            '        iDatabase.RollBack()
            '        Return False
            '    End If
            'End If

            '' REPOCHARGE_M
            'If Me.REPOCHARGE_M.Enabled Then
            '    sql.Clear()
            '    sql.Append(String.Format("UPDATE REPOCHARGE_M SET HAKKOKAISU = {0} WHERE CHARGEDAY = '{1}' AND HOSTNAME = '{2}'", CInt(REPOCHARGE_M.HAKKOKAISU), REPOCHARGE_M.CHARGEDAY, REPOCHARGE_M.HOSTNAME))
            '    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
            '        iDatabase.RollBack()
            '        Return False
            '    End If
            'End If

            '' SEQTRN
            'If Me.SEQTRN.Enabled Then
            '    sql.Clear()
            '    sql.Append(String.Format("UPDATE SEQTRN SET DENNOSEQ = {0}", SEQTRN.DENNOSEQ))
            '    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
            '        iDatabase.RollBack()
            '        Return False
            '    End If
            'End If
            
            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try
    End Function

#End Region

End Class
