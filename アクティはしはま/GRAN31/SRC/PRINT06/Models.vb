''' <summary>
''' 分類ごとのデータモデル
''' </summary>
''' <remarks></remarks>
Class BunTotalData
    ' 商品名
    Public Property Name As String = Nothing
    ' 現金精算の合計
    Public Property CashTotal As Integer = 0
    ' その他の合計
    Public Property OtherTotal As Integer = 0
    ' 現金精算の内税の合計
    Public Property CashTaxTotal As Integer = 0
    ' その他の内税の合計
    Public Property OtherTaxTotal As Integer = 0
End Class

Class DENTRA
    Public BUNCDA As String
    Public BUNCDB As String
    Public BUNCDC As String
    Public CPAYKBN As String
    Public UDNBKN As Integer
    Public UDNZKN As Integer
    Public TKTKBN As String
End Class

Class HINMTA
    Public BUNCDB As String
    Public HINCD As String
    Public HINNMA As String
End Class