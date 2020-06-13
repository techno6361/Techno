''' <summary>
''' データグリッド表示用のビューモデル
''' </summary>
''' <remarks></remarks>
Public Class GOODS_View
    Private _GDSNAME As String
    Private _GDSCOUNT As Integer
    Private _GDSTAX As Double
    Private _GDSKIN As Integer

    Public Property GDSNAME As String
        Get
            Return _GDSNAME
        End Get
        Set(ByVal value As String)
            _GDSNAME = value
        End Set
    End Property

    Public Property GDSCOUNT As Integer
        Get
            Return _GDSCOUNT
        End Get
        Set(ByVal value As Integer)
            _GDSCOUNT = value
        End Set
    End Property

    Public Property GDSTAX As Double
        Get
            Return _GDSTAX
        End Get
        Set(ByVal value As Double)
            _GDSTAX = value
        End Set
    End Property

    Public Property GDSKIN As Integer
        Get
            Return _GDSKIN
        End Get
        Set(ByVal value As Integer)
            _GDSKIN = value
        End Set
    End Property

    Public Sub New(ByVal name As String, ByVal count As Integer, ByVal tax As Double, ByVal kin As Integer)
        GDSNAME = name
        GDSCOUNT = count
        GDSTAX = tax
        GDSKIN = kin
    End Sub

End Class
