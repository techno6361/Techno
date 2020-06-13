Public Class ENTHIST_View
    Private _ENTNO As String
    Private _ENTTM As String
    Private _CKBNAME As String
    Private _MANNO As String
    Private _CCSNAME As String
    Private _ENTBKN As Integer
    Private _POINT As Integer

    Public Property ENTNO As String
        Get
            Return _ENTNO
        End Get
        Set(ByVal value As String)
            _ENTNO = value
        End Set
    End Property

    Public Property ENTTM As String
        Get
            Return _ENTTM
        End Get
        Set(ByVal value As String)
            _ENTTM = value
        End Set
    End Property

    Public Property CKBNAME As String
        Get
            Return _CKBNAME
        End Get
        Set(ByVal value As String)
            _CKBNAME = value
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

    Public Property ENTBKN As Integer
        Get
            Return _ENTBKN
        End Get
        Set(ByVal value As Integer)
            _ENTBKN = value
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
