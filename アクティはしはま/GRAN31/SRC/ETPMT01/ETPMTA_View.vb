''' <summary>
''' データグリッドに表示するビューモデル
''' </summary>
''' <remarks></remarks>
Public Class ETPMTA_View
    Private _ETPKBN As String
    Private _ENTCNT As String
    Private _POINT As String

    Public Property ETPKBN As String
        Get
            Return _ETPKBN
        End Get
        Set(ByVal value As String)
            _ETPKBN = value
        End Set
    End Property

    Public Property ENTCNT As String
        Get
            Return _ENTCNT
        End Get
        Set(ByVal value As String)
            _ENTCNT = value
        End Set
    End Property

    Public Property POINT As String
        Get
            Return _POINT
        End Get
        Set(ByVal value As String)
            _POINT = value
        End Set
    End Property

    Public Sub New(ByVal entkbn As String, ByVal entcnt As String, ByVal point As String)
        _ETPKBN = entkbn
        _ENTCNT = entcnt
        _POINT = point
    End Sub

End Class