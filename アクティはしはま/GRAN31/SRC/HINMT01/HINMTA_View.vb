''' <summary>
''' 商品マスタビュー
''' </summary>
''' <remarks></remarks>
Public Class HINMTA_View
    Private _HINCD As String
    Private _HINNMA As String
    Private _URIATK As String
    Private _ZEITK As String
    Private _HINKB As Boolean

    Public Property HINCD As String
        Get
            Return _HINCD
        End Get
        Set(ByVal value As String)
            _HINCD = value
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

    Public Property URIATK As String
        Get
            Return _URIATK
        End Get
        Set(ByVal value As String)
            _URIATK = value
        End Set
    End Property

    Public Property ZEITK As String
        Get
            Return _ZEITK
        End Get
        Set(ByVal value As String)
            _ZEITK = value
        End Set
    End Property

    Public Property HINKB As Boolean
        Get
            Return _HINKB
        End Get
        Set(ByVal value As Boolean)
            _HINKB = value
        End Set
    End Property


End Class
