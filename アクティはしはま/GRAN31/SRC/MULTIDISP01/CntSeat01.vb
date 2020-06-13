Imports System.Threading.Tasks

Public Class CntSeat01

    ' ランプの種類
    Enum LampType
        BLANK    ' 空席
        USED     ' 使用中
        CALLED   ' 呼び出し中
        DISABLED ' 使用不可
    End Enum

    ' 現在のランプの画像ファイル
    Private _Image As System.Drawing.Image

    ' 以前のランプの画像ファイル
    Private _ImageBack As System.Drawing.Image

    ' 現在のランプの状態
    Private _Lamp As LampType

    Private _bTaskStoped As Boolean

    ' ランプの画像ファイル
    Private Const BLANK_IMAGE = "D:\GRAN31\IMAGE\Lamp_OFF.png"
    Private Const USED_IMAGE = "D:\GRAN31\IMAGE\Lamp_ON.png"
    Private Const CALLED_IMAGE = "D:\GRAN31\IMAGE\Lamp_ERROR.png"
    Private Const DISABLED_IMAGE = "D:\GRAN31\IMAGE\Lamp_OFF.png"

    Private Const LR_IMAGE = "D:\GRAN31\IMAGE\LR.png"
    Private Const L_IMAGE = "D:\GRAN31\IMAGE\L.png"
    Private Const R_IMAGE = "D:\GRAN31\IMAGE\R.png"

    Private Const SCHOOL_IMAGE = "D:\GRAN31\IMAGE\School.png"

    Public Shadows Property Text As String
        Get
            Return btnNo.Text
        End Get
        Set(ByVal value As String)
            btnNo.Text = value
        End Set
    End Property

    Public Shadows Property Size As Size
        Get
            Return MyBase.Size
        End Get
        Set(ByVal value As Size)
            MyBase.Size = value

            'Dim h_LR = CInt(value.Height / 5) + 1
            'Dim h_No = CInt(value.Height / 2.5) + 1

            Dim h_LR = 48
            Dim h_No = 45
            Dim h_Lamp = MyBase.Height - (h_LR + h_No) + 1

            btnLR.Size = New Size(value.Width, h_LR)

            btnNo.Size = New Size(value.Width, h_No)
            btnNo.Location = New Point(btnNo.Location.X, btnLR.Height + 1)

            btnLamp.Size = New Size(value.Width, h_Lamp)
            btnLamp.Location = New Point(btnLamp.Location.X, (btnLR.Height + btnNo.Height) + 1)
        End Set
    End Property

    Public Property Size_No As Size
        Get
            Return btnNo.Size
        End Get
        Set(ByVal value As Size)
            btnNo.Size = value
        End Set
    End Property

    Public Property Size_Lamp As Size
        Get
            Return btnLamp.Size
        End Get
        Set(ByVal value As Size)
            btnLamp.Size = value
        End Set
    End Property

    Public Shadows Property Font As Font
        Get
            Return btnNo.Font
        End Get
        Set(ByVal value As Font)
            btnNo.Font = value
        End Set
    End Property

    Public Shadows Property ForeColor As Color
        Get
            Return btnNo.ForeColor
        End Get
        Set(ByVal value As Color)
            btnNo.ForeColor = value
        End Set
    End Property

    Public Property Lamp As LampType
        Get
            Return _Lamp
        End Get
        Set(ByVal value As LampType)
            _Lamp = value
            tmLamp.Stop()
            btnLR.ForeColor = Color.DarkOrange
            btnNo.ForeColor = Color.DimGray
            Select Case value
                Case LampType.BLANK
                    _Image = Image.FromFile(BLANK_IMAGE)
                Case LampType.USED
                    _Image = Image.FromFile(USED_IMAGE)
                Case LampType.CALLED
                    _ImageBack = btnLamp.Image
                    _Image = Image.FromFile(CALLED_IMAGE)
                    'tmLamp.Start() // 点滅
                Case Else
                    _Image = Image.FromFile(DISABLED_IMAGE)
                    btnLR.ForeColor = Color.Sienna
                    btnNo.ForeColor = Color.Gray
            End Select
            btnLamp.Image = _Image
        End Set
    End Property

    Public Property SeatColor As Color
        Get
            Return btnNo.BackColor
        End Get
        Set(ByVal value As Color)
            btnNo.BackColor = value
        End Set
    End Property

    Public Property LeftSeat As Boolean
        Get
            Return btnLR.Visible
        End Get
        Set(ByVal value As Boolean)
            btnLR.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' 左右兼用打席の画像タイプ(定義)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eLeftSeatType
        L
        R
        LR
    End Enum

    ''' <summary>
    ''' 左右兼用打席の画像タイプ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property LeftSeatType As eLeftSeatType
        Set(value As eLeftSeatType)
            Select Case value
                Case eLeftSeatType.L
                    btnLR.Image = Image.FromFile(L_IMAGE)
                Case eLeftSeatType.R
                    btnLR.Image = Image.FromFile(R_IMAGE)
                Case Else
                    btnLR.Image = Image.FromFile(LR_IMAGE)
            End Select
        End Set

    End Property

    ''' <summary>
    ''' スクール専用打席の設定
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SchoolSeat As Boolean
        Set(ByVal value As Boolean)
            If value Then
                btnLR.Visible = True
                btnLR.Image = Image.FromFile(SCHOOL_IMAGE)
                'Me.btnNo.ForeColor = Color.Blue
            End If
        End Set
    End Property

    ' 親フォームに返すイベント
    Public Event OnButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Sub btnLamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent OnButtonClick(Me, e)
    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent OnButtonClick(Me, e)
    End Sub

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.BackColor = Color.Transparent

        ' LR非表示
        btnLR.Visible = False

        ' デフォルト表示
        Lamp = LampType.BLANK

        ' ランプの点滅速度
        tmLamp.Interval = 1000

        ' スクール専用打席
        Me.SchoolSeat = False

    End Sub

    ''' <summary>
    ''' ランプの点滅
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmLamp_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmLamp.Tick
        If btnLamp.Image.GetHashCode.Equals(_ImageBack.GetHashCode()) Then
            btnLamp.Image = _Image
        Else
            btnLamp.Image = _ImageBack
        End If
    End Sub

#Region "▼ 並列処理"

    Delegate Sub RunTask()

    ''' <summary>
    ''' ランプの点滅
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RunLamp()
        If btnLamp.Image Is Nothing Then
            btnLamp.Image = _Image
        Else
            btnLamp.Image = _ImageBack
        End If
    End Sub

#End Region
End Class
