''' <summary>
''' システムで共有するメッセージを管理します
''' </summary>
''' <remarks></remarks>
Public Class UIMessage

    ''' <summary>
    ''' カード挿入メッセージ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertCard() As String
        If UIUtility.SYSTEM.RWUnitKB = 1 Then
            Return "カードを入れてください。"
        Else
            Return "カードを置いてください。"
        End If
    End Function

    ''' <summary>
    ''' カード排出メッセージ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EjectCard() As String
        If UIUtility.SYSTEM.RWUnitKB = 1 Then
            Return "カードを排出しています。"
        Else
            Return "カードをお取りください。"
        End If
    End Function

    ''' <summary>
    ''' カード挿入ボタンメッセージ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertButton() As String
        If UIUtility.SYSTEM.RWUnitKB = 1 Then
            Return "カード挿入"
        Else
            Return "カード読込"
        End If
    End Function

    ''' <summary>
    ''' カード排出ボタンメッセージ
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EjectButton() As String
        If UIUtility.SYSTEM.RWUnitKB = 1 Then
            Return "カード排出"
        Else
            Return "読込済み"
        End If
    End Function

End Class
