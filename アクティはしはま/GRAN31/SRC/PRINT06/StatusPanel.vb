Public Class StatusPanel

#Region "▼ 宣言部"

    Dim _count As Integer
    Dim _title As String
    Dim _owner As Form

    ''' <summary>
    ''' Showオプション
    ''' </summary>
    ''' <remarks></remarks>
    Enum eShowOption
        LOAD
        WRITE
        WRITE_ESC
    End Enum

#End Region

#Region "▼ プロパティ"

    Public Property MaxCount As Integer = 0

    Public Property Count As Integer
        Get
            Return _count
        End Get
        Set(ByVal value As Integer)
            _count = value
            Me.lblCount.Text = String.Format("({0}/{1})", value, MaxCount)
        End Set
    End Property

    Public Property Title As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
            Me.lblTitle.Text = value
        End Set
    End Property

    Public Property CountVisible As Boolean
        Get
            Return Me.lblCount.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.lblCount.Visible = value
        End Set
    End Property

#End Region

#Region "▼ 関数"

    ''' <summary>
    ''' 初期化
    ''' </summary>
    ''' <param name="owner"></param>
    ''' <remarks></remarks>
    Public Sub Init(ByVal owner As Form)
        Me.Visible = False
        If Not owner Is Nothing Then
            Me.Height = 189
            Me.Width = owner.Width
            Me.Location = New Point(0, CInt((owner.Height / 2) - (Me.Height / 2)))
            _owner = owner
        End If
    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Title = "処理中…しばらくお待ちください。"

    End Sub

    ''' <summary>
    ''' スタータスパネルの表示
    ''' </summary>
    ''' <remarks></remarks>
    Public Shadows Sub Show(ByVal st As eShowOption, Optional ByVal max As Integer = 1)
        Select Case st
            Case eShowOption.LOAD
                Me.Title = "データ取得中…しばらくお待ちください。"
            Case eShowOption.WRITE
                Me.Title = "データ出力中…しばらくお待ちください。"
            Case eShowOption.WRITE_ESC
                Me.Title = "データ出力中…しばらくお待ちください。【ESC】:中断"
        End Select
        Me.MaxCount = max
        Me.Count = 1
        Me.Visible = True
    End Sub
#End Region


End Class
