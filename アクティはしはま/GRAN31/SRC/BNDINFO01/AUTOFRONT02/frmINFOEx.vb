Imports Techno.DeviceControls

Public Class frmINFOEx

#Region "▼宣言部"

    Private _drwPoint As System.Drawing.Point
#End Region

#Region "▼プロパティ"

    Public Property iDataBase As Techno.DataBase.IDatabase.IMethod
    Public Property ICR700 As Techno.DeviceControls.ICR700Control = New Techno.DeviceControls.ICR700Control
    Public Property ERRFLG As Boolean = False
    Public Property ChargeEnabled As Boolean = True
    Public Property CheckInEnabled As Boolean = True
    Public Property BallKin1F As Integer
    Public Property BallKin2F As Integer
    Public Property DMEMBER As String
    Public Property CCSNAME As String
    Public Property NCSNO As String
    Public Property SRTPO As Integer



#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            ' すべてのコントロールにダブルバッファリングを有効化
            For Each c As Control In GetAllControls(Me)
                EnableDoubleBuffering(c)
            Next

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
    Private Sub frmMSGBOX01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            ' カード残高
            Me.lblKINGAKU.Text = CInt(Me.ICR700.KINGAKU).ToString("#,0")
            ' 残金額
            Me.lblZANKN.Text = CInt(Me.ICR700.ZANKN).ToString("#,0")
            ' P)残金額
            Me.lblPREZANKN.Text = CInt(Me.ICR700.PREZANKN).ToString("#,0")
            ' ポイント残高
            Me.lblSRTPO.Text = SRTPO.ToString("#,0")
            ' 残球数情報
            Dim dblKINGAKU As Double = 0
            Me.lblTanka1F.Text = "1F " & (BallKin1F + UIFunction.GetTaxTanka(BallKin1F)).ToString & "円"
            dblKINGAKU = CInt(Me.ICR700.KINGAKU) / (BallKin1F + UIFunction.GetTaxTanka(BallKin1F))
            Me.lblZanTama1F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0")
            Me.lblTanka2F.Text = "2F " & (BallKin2F + UIFunction.GetTaxTanka(BallKin2F)).ToString & "円"
            dblKINGAKU = CInt(Me.ICR700.KINGAKU) / (BallKin2F + UIFunction.GetTaxTanka(BallKin2F))
            Me.lblZanTama2F.Text = Math.Ceiling(dblKINGAKU).ToString("#,##0")
            ' 月間来場回数
            Dim sql = String.Format("select entcnt2 from csmast where ncsno = {0}", CInt(NCSNO))
            Dim dt = iDataBase.ExecuteRead(sql)
            If dt.Rows.Count <= 0 Then Throw New Exception
            Me.lblCOUNT.Text = CInt(dt.Rows(0).Item("entcnt2")).ToString("#,0")
            ' 会員期限
            Me.lblDMEMBER.Text = DMEMBER
            If String.IsNullOrEmpty(DMEMBER) Then
                Me.Label8.Visible = False
            End If
            If DMEMBER < Now.ToString("yyyy/MM/dd") Then
                Me.lblDMEMBER.ForeColor = Color.Yellow
            End If
            '顧客番号
            Me.lblNCSNO.Text = "【顧客番号】" & NCSNO
            '氏名
            Me.lblCCSNAME.Text = CCSNAME
            If Not String.IsNullOrEmpty(CCSNAME) Then
                Me.lblCCSNAME.Text &= "  様"
            End If

            ' ログイン/ログアウト

            Dim blnCheckIn = Me.ICR700.ENTKBN = "1"

            ' 終了タイマースタート
            Me.tmClose.Interval = CommonSettings.InfoCloseInterval
            Me.tmClose.Start()

            ' ボタンの表示/非表示
            Dim flg1 = CommonSettings.CheckInEnabled And Not blnCheckIn And Me.CheckInEnabled
            Dim flg2 = CommonSettings.ChargeEnabled And Me.ChargeEnabled
            'frmBase.ChangePictureBoxEnabled(Me.btnCheckIn, flg1)
            Me.btnCheckIn.Visible = flg1


        Catch ex As Exception
            Me.ERRFLG = True
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' チェックインボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheckIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckIn.Click
        frmBase.PushAnimation(CType(sender, Control))
        btnCheckIn_PerformClick()
    End Sub

    ''' <summary>
    ''' チェックインボタン_実行
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnCheckIn_PerformClick()
        Try
            Me.vbDialogResult = eResult.CHECKIN
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 入金画面_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCharge.Click
        frmBase.PushAnimation(CType(sender, Control))
        btnCharge_PerformClick()
    End Sub

    ''' <summary>
    ''' 入金画面_実行
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnCharge_PerformClick()
        Try
            Me.vbDialogResult = eResult.CHARGE
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Exitボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        frmBase.PushAnimation(CType(sender, Control))
        btnExit_PerformClick()
    End Sub

    ''' <summary>
    ''' Exitボタン_実行
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnExit_PerformClick()
        Try
            ' カード排出
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 終了タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmClose_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmClose.Tick
        Try
            Me.tmClose.Stop()
            btnExit_PerformClick()

        Catch ex As Exception
            Me.tmClose.Stop()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼ 関数定義"

#End Region


End Class
