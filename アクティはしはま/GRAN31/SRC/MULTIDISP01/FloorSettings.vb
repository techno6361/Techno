Public Class FloorSettings

    Public FloorNumber As Integer
    Public TextLength As Integer
    Public AspectRate As Double
    Public SeatNumber As Integer
    Public Margin_X As Integer
    Public Margin_Y As Integer
    Public Padding_X As Integer
    Public Padding_Y As Integer
    Public Span_X As Integer
    Public Span_Y As Integer
    Public RightToLeft As Boolean
    Public Align As AlignType
    Public Width As Integer
    Public Height As Integer
    Public StartSeatNumber As Integer
    Public Font As Font
    Public ArcRange As Integer ' 円弧
    Public ArcRate As Double ' 円弧
    Public VerticalAlign As VerticalAlignType
    Public SeatColor As Color
    Public LeftSeats As List(Of Integer)

    Enum AlignType
        NONE
        LEFT
        CENTER
        RIGHT
    End Enum

    Enum VerticalAlignType
        NONE
        TOP
        MIDDLE
        BUTTOM
    End Enum

    Public Sub New()
        FloorNumber = 1
        TextLength = 2
        AspectRate = 2.5
        SeatNumber = 30
        Margin_X = 25
        Margin_Y = 25
        Padding_X = 5
        Padding_Y = 5
        Span_X = 0
        Span_Y = 0
        Align = AlignType.LEFT
        VerticalAlign = VerticalAlignType.NONE
        ArcRate = 0.0
        Width = -1
        Height = -1
        StartSeatNumber = 1
        Font = New Font("MS UI Gothic", 20, FontStyle.Bold)
        ArcRange = -1
        ArcRate = -1
        'SeatColor = ColorTranslator.FromHtml("#000000")
        SeatColor = Color.Transparent
        LeftSeats = New List(Of Integer)
    End Sub

    Public WriteOnly Property ArcEnabled As Boolean        
        Set(ByVal value As Boolean)
            If value Then
                ArcRange = CInt(SeatNumber / 2) - 1
                ArcRate = 1.5
            Else
                ArcRange = 0
                ArcRate = 0
            End If
        End Set
    End Property
       
End Class
