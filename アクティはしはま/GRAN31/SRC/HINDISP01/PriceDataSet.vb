''' <summary>
''' カードに書き込む入金額、残高、プレミアム金額、ポイントを扱うクラス
''' </summary>
''' <remarks></remarks>
Public Class PriceDataSet
    Public ZANKN As Integer
    Public PREMKN As Integer
    Public POINT As Integer
    Public ADD_ZANKN As Integer  ' 加算する残金
    Public ADD_PREMKN As Integer ' 加算するプレミアム残金
    Public ADD_POINT As Integer  ' 加算するポイント
    Public ADD_NKNKN As Integer  ' 加算する入金額
End Class
