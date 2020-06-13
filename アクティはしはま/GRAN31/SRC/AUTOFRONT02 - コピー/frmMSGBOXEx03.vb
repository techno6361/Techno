Public Class frmMSGBOXEx03

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

    'Const CS_DROPSHADOW As Integer = &H20000

    'Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
    '    Get
    '        Dim cp = MyBase.CreateParams
    '        cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
    '        Return cp
    '    End Get
    'End Property

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
    ''' <remarks></remarks>
    Public Sub New(ByVal Msg As List(Of String))
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            Dim i = 0
            For Each b In Msg
                Select Case i
                    Case 0
                        Me.lblMSG1.Text = b
                        Me.lblMSG1.Visible = True
                        Me.lblMSG1.Enabled = True
                    Case 1
                        Me.lblMSG2.Text = b
                        Me.lblMSG2.Visible = True
                        Me.lblMSG2.Enabled = True
                    Case 2
                        Me.lblMSG3.Text = b
                        Me.lblMSG3.Visible = True
                        Me.lblMSG3.Enabled = True
                    Case 3
                        Me.lblMSG4.Text = b
                        Me.lblMSG4.Visible = True
                        Me.lblMSG4.Enabled = True
                    Case 4
                        Me.lblMSG5.Text = b
                        Me.lblMSG5.Visible = True
                        Me.lblMSG5.Enabled = True
                End Select
                i += 1
            Next

            ' フォームの境界線をなくす
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

            ' 背景色を透過
            Me.BackColor = Color.Green
            Me.TransparencyKey = Color.Green

            ' テキストとアイコンの位置調整
            'Dim x = CInt((Me.Width / 2) - (Me.lblMSG1.Width / 2))
            'Me.lblMSG1.Location = New Point(x, Me.lblMSG1.Location.Y)

            ' タイトルの位置調整
            Me.lblTitle.AutoSize = False
            Me.lblTitle.TextAlign = ContentAlignment.MiddleCenter
            Me.lblTitle.Size = New Size(Me.Width, Me.lblTitle.Size.Height)
            Me.lblTitle.Location = New Point(0, Me.lblTitle.Location.Y)

            Me.btnOK.Visible = False
            Me.btnCancel.Visible = False

            Me.picImage.Image = Images.IconBonus
            Me.lblTitle.Text = "おめでとうございます！"
            Me.Timer1.Interval = 5000
            Me.Timer1.Start()

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
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMSGBOX01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    ''' フォーム_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMSGBOXEx03_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        Try
            Me.Hide()
        Catch ex As Exception
            Throw ex
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

    ''' <summary>
    ''' OKボタンの実行
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnOK_PerformClick()
        Try
            _blnReply = True
            Me.Hide()
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
            Me.Hide()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ×ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        btnCancel_Click(sender, e)
    End Sub

    ''' <summary>
    ''' タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.Timer1.Stop()
            btnOK_PerformClick()

        Catch ex As Exception
            Me.Timer1.Stop()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
