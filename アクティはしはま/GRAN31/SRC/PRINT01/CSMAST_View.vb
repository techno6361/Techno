''' <summary>
''' 検索結果のビュー
''' </summary>
''' <remarks></remarks>
Public Class CSMAST_View
    Private _CHKBOX As Boolean
    Private _NCSNO As String
    Private _CCSNAME As String
    Private _CCSKANA As String
    Private _NCSRANK As String
    Private _NSEX As String
    Private _NZIP As String
    Private _CADDRESS As String
    Private _DBIRTH As String
    Private _CTELEPHONE As String
    Private _CPOTABLENUM As String
    Private _DMEMBER As String

    Public Property CHKBOX As Boolean
        Get
            Return _CHKBOX
        End Get
        Set(ByVal value As Boolean)
            _CHKBOX = value
        End Set
    End Property

    Public Property NCSNO As String
        Get
            Return _NCSNO
        End Get
        Set(ByVal value As String)
            _NCSNO = value
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

    Public Property CCSKANA As String
        Get
            Return _CCSKANA
        End Get
        Set(ByVal value As String)
            _CCSKANA = value
        End Set
    End Property

    Public Property NCSRANK As String
        Get
            Return _NCSRANK
        End Get
        Set(ByVal value As String)
            _NCSRANK = value
        End Set
    End Property

    Public Property NSEX As String
        Get
            Return _NSEX
        End Get
        Set(ByVal value As String)
            _NSEX = value
        End Set
    End Property

    Public Property NZIP As String
        Get
            Return _NZIP
        End Get
        Set(ByVal value As String)
            _NZIP = value
        End Set
    End Property

    Public Property CADDRESS As String
        Get
            Return _CADDRESS
        End Get
        Set(ByVal value As String)
            _CADDRESS = value
        End Set
    End Property

    Public Property DBIRTH As String
        Get
            Return _DBIRTH
        End Get
        Set(ByVal value As String)
            _DBIRTH = value
        End Set
    End Property

    Public Property CTELEPHONE As String
        Get
            Return _CTELEPHONE
        End Get
        Set(ByVal value As String)
            _CTELEPHONE = value
        End Set
    End Property

    Public Property CPOTABLENUM As String
        Get
            Return _CPOTABLENUM
        End Get
        Set(ByVal value As String)
            _CPOTABLENUM = value
        End Set
    End Property

    Public Property DMEMBER As String
        Get
            Return _DMEMBER
        End Get
        Set(ByVal value As String)
            _DMEMBER = value
        End Set
    End Property

End Class
