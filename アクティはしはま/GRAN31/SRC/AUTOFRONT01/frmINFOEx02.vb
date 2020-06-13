Public Class frmINFOEx02

#Region "▼宣言部"

    Const CLOSE_TIME_INBERVAL As Integer = 10000

#End Region

#Region "▼プロパティ"

    Public Property iDataBase As Techno.DataBase.IDatabase.IMethod
    Public Property ICR700 As Techno.DeviceControls.ICR700Control = New Techno.DeviceControls.ICR700Control
    Public Property ERRFLG As Boolean = False
    Public Property USER_INFO As UserInfo = New UserInfo

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Msg">メッセージ内容</param>
    ''' <param name="MsgType">【0】情報【1】確認【2】エラー【3】注意 </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Msg As String, ByVal MsgType As Integer)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
    ' *** STA
    ''' <summary>
    ''' OKボタンの位置をダイアログの真ん中に表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AdjustButtonPosition()
        Try
            Dim x = CInt((MyBase.Width / 2) - (Me.btnCheckIn.Width / 2))
            Me.btnCheckIn.Location = New Point(x, Me.btnCheckIn.Location.Y)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' *** END

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
            Me.lblNCARDID.Text = Me.ICR700.CARDNO
            Me.lblNCSRANK.Text = Me.ICR700.NKBNO
            Me.lblCCSNAME.Text = ""
            Me.lblKINGAKU.Text = Me.ICR700.KINGAKU
            Me.lblZANKN.Text = Me.ICR700.ZANKN
            Me.lblPREZANKN.Text = Me.ICR700.PREZANKN
            Me.lblSRTPO.Text = Me.ICR700.POINT
            Me.lblZENENTDATE.Text = Me.ICR700.ZENENTDATE
            Me.lblZENNYUKIN.Text = ""
        Catch ex As Exception
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
    ''' 画面タッチで終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMSGBOXEx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        btnExit_Click(sender, e)
    End Sub

    ''' <summary>
    ''' タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmClose_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmClose.Tick
        Try
            Me.tmClose.Stop()
            btnExit_Click(sender, e)

        Catch ex As Exception
            Me.tmClose.Stop()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "▼ 関数定義"

    Private Function GetCSMAST() As Boolean
        Try
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class
