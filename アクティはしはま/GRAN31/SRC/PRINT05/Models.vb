''' <summary>
''' 男女別
''' </summary>
''' <remarks></remarks>
Public Class Sheet1Model_Block1
    Public Male As Integer = 0
    Public Female As Integer = 0
    Public Other As Integer = 0
End Class

''' <summary>
''' 年齢別
''' </summary>
''' <remarks></remarks>
Public Class Sheet1Model_Block2
    Public Age0 As Integer = 0 ' 10歳未満
    Public Age1 As Integer = 0 ' 10-19
    Public Age2 As Integer = 0 ' 20-29
    Public Age3 As Integer = 0 ' 30-39
    Public Age4 As Integer = 0 ' 40-49
    Public Age5 As Integer = 0 ' 50-59
    Public Age6 As Integer = 0 ' 60-69
    Public Age7 As Integer = 0 ' 70-79
    Public Age8 As Integer = 0 ' 80歳以上
    Public Other As Integer = 0 ' 年齢不明
End Class

''' <summary>
''' 種別区分
''' </summary>
''' <remarks></remarks>
Public Class Sheet1Model_Block3
    Public Name As String
    Public Count As Integer
    Public Sub New()

    End Sub
    Public Sub New(ByVal _name As String)
        Name = _name
    End Sub
End Class

''' <summary>
''' 利用別
''' </summary>
''' <remarks></remarks>
Public Class Sheet1Model_Block4
    Public KB1 As Integer = 0 ' １球貸し
    Public KB2 As Integer = 0 ' ｻｰﾋﾞｽｶｰﾄﾞ
    Public KB3 As Integer = 0 ' 打ち放題
End Class

''' <summary>
''' 比較用のモデル
''' </summary>
''' <remarks></remarks>
Public Class HikakuValue
    Public Value1 As Integer
    Public value2 As Integer
End Class

''' <summary>
''' 顧客クラス
''' </summary>
''' <remarks></remarks>
Public Class Customer
    Public ENTDT As String
    Public MANNO As String
    Public KBNAME As String
    Public NAME As String
    Public SEX As Integer
    Public DBIRTH As Integer
    Public AGE As Integer
    Public ZIP As String
    Public ADDRESS As String
    Public TELEPHONE As String
End Class

Public Class JoinENTTRA_View

    ' ENTTRA
    Public ENTDT As String
    Public MANNO As String
    Public EIGKB As String
    Public KSBKB As String

    ' CSMAST
    Public NCSRANK As String
    Public SEX As Integer
    Public AGE As Integer
    Public CCSNAME As String
    Public NZIP As String
    Public CADDRESS As String
    Public CTELEPHONE As String

    ' KBMAST
    Public KBNAME As String

End Class

Public Class FREE_QUERY
    Public MANNO As String
    Public KBNAME As String
    Public COUNT1 As Integer
    Public COUNT2 As Integer
End Class

Public Class EntCount_View
    Public MANNO As String
    Public CCSNAME As String
    Public KBNAME As String
    Public NSEX As Integer
    Public AGE As Integer
    Public COUNT1 As Integer
    Public COUNT2 As Integer
    Public ZOUGEN As Integer
End Class
