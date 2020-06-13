Public Class frmMSGBOX01

#Region "▼宣言部"

    ''' <summary>
    ''' 画面終了カウント(10で終了)
    ''' </summary>
    ''' <remarks></remarks>
    Private _intEndCnt As Integer = 0

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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Msg">メッセージ内容</param>
    ''' <param name="MsgType">【0】情報【1】確認【2】エラー【3】注意【4】確認2 </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Msg As String, ByVal MsgType As Integer)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            Me.lblMSG.Text = Msg
            Select Case MsgType
                Case 0
                    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
                    ' *** STA
                    AdjustButtonPosition()
                    ' *** END
                    Me.picInfomation.Visible = True
                    Me.Timer1.Start()
                Case 1
                    Me.picQuestion.Visible = True
                    Me.btnCancel.Visible = True
                Case 2
                    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
                    ' *** STA
                    AdjustButtonPosition()
                    ' *** END
                    Me.picError.Visible = True
                    Me.Timer1.Start()
                Case 3
                    ' HACK ADD 2018/01/26 TERAYAMA OKボタンの位置を調整
                    ' *** STA
                    AdjustButtonPosition()
                    ' *** END
                    Me.picExclamation.Visible = True
                    Me.Timer1.Start()
                Case 4
                    Me.btnOK.Text = "確認"
                    Me.picInfomation.Visible = True
                    Me.btnCancel.Visible = True
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
            Me.btnOK.Location = New Point((MyBase.Width / 2) - (Me.btnOK.Width / 2), Me.btnOK.Location.Y)
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
    Private Sub frmMSGBOX01_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
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
    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
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
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Try
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
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            _intEndCnt += 1

            If _intEndCnt.Equals(10) Then
                Me.Timer1.Stop()
                Me.btnOK.PerformClick()
            End If

        Catch ex As Exception
            Me.Timer1.Stop()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region






End Class
