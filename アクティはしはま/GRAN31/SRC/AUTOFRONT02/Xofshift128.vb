Imports System

''' <summary>
''' "Xorshift RNGs" (George Marsaglia) の xor128 アルゴリズムを使用した
''' 擬似乱数ジェネレーターを表します。
''' </summary>
''' <remarks>
''' "Xorshift RNGs"
''' http://www.jstatsoft.org/v08/i14/paper
''' </remarks>
Public Class Xorshift128
    Inherits Random
    Private _x As UInteger
    Private _y As UInteger
    Private _z As UInteger
    Private _w As UInteger

    ''' <summary>
    ''' 指定した4つのシード値を使用して、
    ''' Xorshift128クラスのインスタンスを初期化します。
    ''' </summary>
    ''' <param name="seedX">シード値の1つ (x)。</param>
    ''' <param name="seedY">シード値の1つ (y)。</param>
    ''' <param name="seedZ">シード値の1つ (z)。</param>
    ''' <param name="seedW">シード値の1つ (w)。</param>
    Public Sub New(ByVal seedX As UInteger, _
                   ByVal seedY As UInteger, _
                   ByVal seedZ As UInteger, _
                   ByVal seedW As UInteger)
        'すべて0であってはならない
        If seedX = 0 AndAlso seedY = 0 AndAlso seedZ = 0 AndAlso seedW = 0 Then
            Throw New ArgumentException()
        End If
        _x = seedX
        _y = seedY
        _z = seedZ
        _w = seedW
    End Sub

    'seedに負数を指定した場合の挙動は、Randomクラスとは異なる
    Public Sub New(ByVal seed As Integer)
        Me.New(CUInt((seed And 2147483647)), 362436069, 521288629, 88675123)
    End Sub

    Public Sub New()
        Me.New(Environment.TickCount)
    End Sub

    ''' <summary>
    ''' 乱数を返します。
    ''' </summary>
    ''' <returns>
    ''' UInt32.MinValue 以上 UInt32.MaxValue 以下の
    ''' 32ビット符号なし整数。
    ''' </returns>
    Public Function NextUInt32() As UInteger
        '実際に乱数を生成している部分
        Dim t As UInteger = _x Xor (_x << 11)
        _x = _y
        _y = _z
        _z = _w
        _w = (_w Xor (_w >> 19)) Xor (t Xor (t >> 8))
        Return _w
    End Function

    Protected Overloads Overrides Function Sample() As Double
        'UInt32の値を、0.0以上1.0未満のDouble値に変換する
        Return NextUInt32() * (1.0 / (UInteger.MaxValue + 1.0))
    End Function

    Public Overloads Overrides Function NextDouble() As Double
        Return Sample()
    End Function

    Public Overloads Overrides Function [Next](ByVal maxValue As Integer) _
            As Integer
        If maxValue < 0 Then
            Throw New ArgumentOutOfRangeException("maxValue")
        End If
        '偏りのある方法だが、簡易的にSample()を使用して計算する
        '偏りのない方法は、Math.NET NumericsのRandomSource.csを参照
        Return CInt(Math.Truncate((Sample() * maxValue)))
    End Function

    Public Overloads Overrides Function [Next](ByVal minValue As Integer, _
                                               ByVal maxValue As Integer) _
                                           As Integer
        If maxValue < minValue Then
            Throw New ArgumentOutOfRangeException("minValue")
        End If
        Return CInt(Math.Truncate(
            (Sample() * (CDbl(maxValue) - minValue) + minValue)))
    End Function

    Public Overloads Overrides Function [Next]() As Integer
        Return CInt(Math.Truncate((Sample() * Integer.MaxValue)))
    End Function

    Public Overloads Overrides Sub NextBytes(ByVal buffer As Byte())
        If buffer Is Nothing Then
            Throw New ArgumentNullException("buffer")
        End If
        Dim len As Integer = buffer.Length
        For i As Integer = 0 To len - 1
            buffer(i) = CByte((NextUInt32() Mod 256))
        Next
    End Sub
End Class