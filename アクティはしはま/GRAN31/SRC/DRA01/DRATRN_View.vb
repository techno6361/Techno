''' <summary>
''' データモデル_レジ情報
''' </summary>
''' <remarks></remarks>
Public Class DRATRN_View
    Private _ASU As String
    Private _BSU As String
    Private _CSU As String
    Private _DSU As String
    Private _ESU As String
    Private _FSU As String
    Private _GSU As String
    Private _HSU As String
    Private _ISU As String
    Private _JSU As String

    Public Property ASU As String
        Get
            Return _ASU
        End Get
        Set(ByVal value As String)
            _ASU = value
        End Set
    End Property

    Public Property BSU As String
        Get
            Return _BSU
        End Get
        Set(ByVal value As String)
            _BSU = value
        End Set
    End Property

    Public Property CSU As String
        Get
            Return _CSU
        End Get
        Set(ByVal value As String)
            _CSU = value
        End Set
    End Property

    Public Property DSU As String
        Get
            Return _DSU
        End Get
        Set(ByVal value As String)
            _DSU = value
        End Set
    End Property

    Public Property ESU As String
        Get
            Return _ESU
        End Get
        Set(ByVal value As String)
            _ESU = value
        End Set
    End Property

    Public Property FSU As String
        Get
            Return _FSU
        End Get
        Set(ByVal value As String)
            _FSU = value
        End Set
    End Property

    Public Property GSU As String
        Get
            Return _GSU
        End Get
        Set(ByVal value As String)
            _GSU = value
        End Set
    End Property

    Public Property HSU As String
        Get
            Return _HSU
        End Get
        Set(ByVal value As String)
            _HSU = value
        End Set
    End Property

    Public Property ISU As String
        Get
            Return _ISU
        End Get
        Set(ByVal value As String)
            _ISU = value
        End Set
    End Property

    Public Property JSU As String
        Get
            Return _JSU
        End Get
        Set(ByVal value As String)
            _JSU = value
        End Set
    End Property

    Public Sub New(ByVal a As String, ByVal b As String, ByVal c As String, ByVal d As String, ByVal e As String, ByVal f As String, ByVal g As String, ByVal h As String, ByVal i As String, ByVal j As String)
        ASU = a
        BSU = b
        CSU = c
        ESU = e
        FSU = f
        GSU = g
        HSU = h
        ISU = i
        JSU = j
    End Sub
End Class

''' <summary>
''' データモデル_売り上げ情報
''' </summary>
''' <remarks></remarks>
Public Class URIAGE_View
    Private _UDNTM As String
    Private _UDNKN As Integer
    Private _HINNMA As String
    Private _MANNO As String
    Private _SCLMANNO As String
    Private _MANNM As String
    Private _STFNAME As String
    ' 隠し属性
    Private _hide_DRAKBN As String
    Private _hide_UDNNO As Integer
    Private _hide_UDNDT As String

    Public Property UDNTM As String
        Get
            Return _UDNTM
        End Get
        Set(ByVal value As String)
            _UDNTM = value
        End Set
    End Property

    Public Property UDNKN As Integer
        Get
            Return _UDNKN
        End Get
        Set(ByVal value As Integer)
            _UDNKN = value
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

    Public Property SCLMANNO As String
        Get
            Return _SCLMANNO
        End Get
        Set(ByVal value As String)
            _SCLMANNO = value
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

    Public Property MANNM As String
        Get
            Return _MANNM
        End Get
        Set(ByVal value As String)
            _MANNM = value
        End Set
    End Property

    Public Property STFNAME As String
        Get
            Return _STFNAME
        End Get
        Set(ByVal value As String)
            _STFNAME = value
        End Set
    End Property

    Public Property hide_DRAKBN As String
        Get
            Return _hide_DRAKBN
        End Get
        Set(ByVal value As String)
            _hide_DRAKBN = value
        End Set
    End Property

    Public Property hide_UDNNO As Integer
        Get
            Return _hide_UDNNO
        End Get
        Set(ByVal value As Integer)
            _hide_UDNNO = value
        End Set
    End Property

    Public Property hide_UDNDT As String
        Get
            Return _hide_UDNDT
        End Get
        Set(ByVal value As String)
            _hide_UDNDT = value
        End Set
    End Property

End Class

''' <summary>
''' データモデル_チェック状況
''' </summary>
''' <remarks></remarks>
Public Class CHECK_View
    Private _CHKTM As String
    Private _CHKRET As String
    Private _CHKKN As Integer

    Public Property CHKTM As String
        Get
            Return _CHKTM
        End Get
        Set(ByVal value As String)
            _CHKTM = value
        End Set
    End Property

    Public Property CHKRET As String
        Get
            Return _CHKRET
        End Get
        Set(ByVal value As String)
            _CHKRET = value
        End Set
    End Property

    Public Property CHKKN As Integer
        Get
            Return _CHKKN
        End Get
        Set(ByVal value As Integer)
            _CHKKN = value
        End Set
    End Property
End Class

''' <summary>
''' テキストボックスと価格を保持するクラス
''' </summary>
''' <remarks></remarks>
Class PriceTextBox
    Public _TextBox As TextBox
    Public _Price As Integer
    Public Sub New(ByVal textbox As TextBox, ByVal price As Integer)
        _TextBox = textbox
        _Price = price
    End Sub
End Class