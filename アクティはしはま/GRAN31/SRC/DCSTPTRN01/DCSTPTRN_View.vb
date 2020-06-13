Public Class DCSTPTRN_View

    Private _NO As String
    Private _DATE As String
    Private _TIME As String
    Private _CDNO As String
    Private _MANNO As String
    Private _CHECK As Boolean

    Public Property NO As String
        Get
            Return _NO
        End Get
        Set(ByVal value As String)
            _NO = value
        End Set
    End Property

    Public Property DATE_ As String
        Get
            Return _DATE
        End Get
        Set(ByVal value As String)
            _DATE = value
        End Set
    End Property

    Public Property TIME As String
        Get
            Return _TIME
        End Get
        Set(ByVal value As String)
            _TIME = value
        End Set
    End Property

    Public Property CDNO As String
        Get
            Return _CDNO
        End Get
        Set(ByVal value As String)
            _CDNO = value
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

    Public Property CHECK As Boolean
        Get
            Return _CHECK
        End Get
        Set(ByVal value As Boolean)
            _CHECK = value
        End Set
    End Property

End Class
