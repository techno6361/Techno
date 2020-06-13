Public Class HINMTA_View
    ' 表示
    Private _HINNMA As String
    Private _HINNUM As String
    Private _URIBTK As String
    ' 非表示    
    Private _BMNCD As String
    Private _BUNCDA As String
    Private _BUNCDB As String
    Private _BUNCDC As String
    Private _HINCD As String
    Private _ZEITK As Integer
    Private _POINT As Integer

    Public Property HINNMA As String
        Get
            Return _HINNMA
        End Get
        Set(ByVal value As String)
            _HINNMA = value
        End Set
    End Property

    Public Property HINNUM As String
        Get
            Return _HINNUM
        End Get
        Set(ByVal value As String)
            _HINNUM = value
        End Set
    End Property

    Public Property URIBTK As String
        Get
            Return _URIBTK
        End Get
        Set(ByVal value As String)
            _URIBTK = value
        End Set
    End Property

    Public Property BMNCD As String
        Get
            Return _BMNCD
        End Get
        Set(ByVal value As String)
            _BMNCD = value
        End Set
    End Property

    Public Property BUNCDA As String
        Get
            Return _BUNCDA
        End Get
        Set(ByVal value As String)
            _BUNCDA = value
        End Set
    End Property

    Public Property BUNCDB As String
        Get
            Return _BUNCDB
        End Get
        Set(ByVal value As String)
            _BUNCDB = value
        End Set
    End Property

    Public Property BUNCDC As String
        Get
            Return _BUNCDC
        End Get
        Set(ByVal value As String)
            _BUNCDC = value
        End Set
    End Property

    Public Property HINCD As String
        Get
            Return _HINCD
        End Get
        Set(ByVal value As String)
            _HINCD = value
        End Set
    End Property

    Public Property ZEITK As Integer
        Get
            Return _ZEITK
        End Get
        Set(ByVal value As Integer)
            _ZEITK = value
        End Set
    End Property

    Public Property POINT As Integer
        Get
            Return _POINT
        End Get
        Set(ByVal value As Integer)
            _POINT = value
        End Set
    End Property

End Class
