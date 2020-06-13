Public Class UDNDETAIL_View
    Private _UDNDT As String
    Private _DATKB As String
    Private _UDNNO As String
    Private _UDNBKN As Integer
    Private _HOSTNAME As String
    Private _DRAKBN As String
    Private _MANNO As String
    Private _CCSNAME As String
    Private _HINNMA As String
    ' 隠し属性                         ' 列番 項目名
    Private _hide_UDNDT As String     ' (09) 日付
    Private _hide_UDNKN As Integer    ' (10) 入金額
    Private _hide_PREMKN As Integer   ' (11) プレミアム
    Private _hide_POINT As Integer    ' (12) ポイント
    Private _hide_DENNO As String     ' (13) 伝票番号
    Private _hide_ZENNKNKN As Integer ' (14) 前回入金額
    Private _hide_UDNBKN As Integer   ' (15) 合計金額

    Public Property UDNDT As String
        Get
            Return _UDNDT
        End Get
        Set(ByVal value As String)
            _UDNDT = value
        End Set
    End Property

    Public Property DATKB As String
        Get
            Return _DATKB
        End Get
        Set(ByVal value As String)
            _DATKB = value
        End Set
    End Property

    Public Property UDNNO As String
        Get
            Return _UDNNO
        End Get
        Set(ByVal value As String)
            _UDNNO = value
        End Set
    End Property

    Public Property UDNBKN As Integer
        Get
            Return _UDNBKN
        End Get
        Set(ByVal value As Integer)
            _UDNBKN = value
        End Set
    End Property

    Public Property HOSTNAME As String
        Get
            Return _HOSTNAME
        End Get
        Set(ByVal value As String)
            _HOSTNAME = value
        End Set
    End Property

    Public Property DRAKBN As String
        Get
            Return _DRAKBN
        End Get
        Set(ByVal value As String)
            _DRAKBN = value
        End Set
    End Property

    Public Property MANNO As String
        Get
            Return _MANNO
        End Get
        Set(ByVal value As String)
            _MANNO = value
        End Set
    End Property

    Public Property CCSNAME As String
        Get
            Return _CCSNAME
        End Get
        Set(ByVal value As String)
            _CCSNAME = value
        End Set
    End Property

    Public Property HINNMA As String
        Get
            Return _HINNMA
        End Get
        Set(ByVal value As String)
            _HINNMA = value
        End Set
    End Property

    ' 隠し属性

    Public Property hide_UDNDT As String
        Get
            Return _hide_UDNDT
        End Get
        Set(ByVal value As String)
            _hide_UDNDT = value
        End Set
    End Property

    Public Property hide_UDNKN As Integer
        Get
            Return _hide_UDNKN
        End Get
        Set(ByVal value As Integer)
            _hide_UDNKN = value
        End Set
    End Property

    Public Property hide_PREMKN As Integer
        Get
            Return _hide_PREMKN
        End Get
        Set(ByVal value As Integer)
            _hide_PREMKN = value
        End Set
    End Property

    Public Property hide_POINT As Integer
        Get
            Return _hide_POINT
        End Get
        Set(ByVal value As Integer)
            _hide_POINT = value
        End Set
    End Property

    Public Property hide_DENNO As String
        Get
            Return _hide_DENNO
        End Get
        Set(ByVal value As String)
            _hide_DENNO = value
        End Set
    End Property

    Public Property hide_ZENNKNKN As Integer
        Get
            Return _hide_ZENNKNKN
        End Get
        Set(ByVal value As Integer)
            _hide_ZENNKNKN = value
        End Set
    End Property

    Public Property hide_UDNBKN As Integer
        Get
            Return _hide_UDNBKN
        End Get
        Set(ByVal value As Integer)
            _hide_UDNBKN = value
        End Set
    End Property

    Public Property CPAYKBN As String = ""

End Class
