Public Class frmMSGBOXEx

#Region "▼宣言部"

    ''' <summary>
    ''' 画面終了カウント(10で終了)
    ''' </summary>
    ''' <remarks></remarks>
    Private _intEndCnt As Integer = 0

    Private _msgType As Integer = 0

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 確認メッセージ返答
    ''' </summary>
    ''' <value>【OK】True【キャンセル】False</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Reply() As Boolean
        Get
            Return _blnReply
        End Get
    End Property
    Private _blnReply As Boolean = True


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

            ' テキストの位置調整
            Dim x = CInt((MyBase.ScreenSize.Width / 2) - (Me.lblMSG.Width / 2))
            Me.lblMSG.Location = New Point(x, Me.lblMSG.Location.Y)

            ' アイコンの位置調整
            Dim xx = CInt((MyBase.ScreenSize.Width / 2) - (Me.picImage.Width / 2))
            Me.picImage.Location = New Point(xx, Me.picImage.Location.Y)

            Me.lblMSG.Text = Msg

            Me.tmClose.Interval = CommonSettings.MSGBOX_CLOSE_INTERVAL

            _msgType = MsgType

            Select Case MsgType
                Case 0
                    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
                    ' *** STA
                    AdjustButtonPosition()
                    ' *** END
                    Me.picImage.Image = Images.IconInfomation()
                    Me.btnOK.Visible = False
                    Me.btnCancel.Visible = False
                    Me.tmClose.Start()
                Case 1
                    Me.picImage.Image = Images.IconQuestion()
                    Me.btnCancel.Visible = True
                Case 2
                    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
                    ' *** STA
                    AdjustButtonPosition()
                    ' *** END
                    Me.picImage.Image = Images.IconError
                    Me.btnOK.Visible = False
                    Me.btnCancel.Visible = False
                    Me.tmClose.Start()
                Case 3
                    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
                    ' *** STA
                    AdjustButtonPosition()
                    ' *** END
                    Me.picImage.Image = Images.IconExclamation
                    Me.btnOK.Visible = False
                    Me.btnCancel.Visible = False
                    Me.tmClose.Start()
            End Select

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
            Dim x = CInt((MyBase.Width / 2) - (Me.btnOK.Width / 2))
            Me.btnOK.Location = New Point(x, Me.btnOK.Location.Y)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' *** END

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' 画面タッチで終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMSGBOXEx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If Not _msgType = 1 Then
            btnOK_PerformClick()
        End If
    End Sub

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMSGBOX01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.Refresh()
            Threading.Thread.Sleep(2000)
            If Me.btnCancel.Visible Then
                Me.btnCancel.Focus()
            Else
                Me.btnOK.Focus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' OKボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        frmBase.PushAnimation(CType(sender, Control))
        btnOK_PerformClick()
    End Sub

    Private Sub btnOK_PerformClick()
        Try
            _blnReply = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' キャンセルボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            _blnReply = False
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmClose_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmClose.Tick
        Try
            _intEndCnt += 1

            If _intEndCnt.Equals(10) Then
                Me.tmClose.Stop()
                btnOK_PerformClick()
            End If

        Catch ex As Exception
            Me.tmClose.Stop()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
