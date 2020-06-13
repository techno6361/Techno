Public Class ResultModel
    Public Property Result As Boolean
    Public Property StrErrNo As String
    Public Property ErrCode As Integer
    Public Sub New(ByVal _result As Boolean, ByVal _strErrNo As String)
        Me.Result = _result
        Me.StrErrNo = _strErrNo
    End Sub

    Public Sub New(ByVal _result As Boolean, ByVal _ErrCode As Integer)
        Me.Result = _result
        Me.ErrCode = _ErrCode
    End Sub

    Public Sub New()

    End Sub
End Class

Public Class ICR700ResultModel
    Public Property Result As Boolean
    Public Property ErrCode As Integer
    Public Sub New(ByVal _result As Boolean, ByVal _errCode As Integer)
        Me.Result = _result
        Me.ErrCode = _errCode
    End Sub

    Public Sub New()

    End Sub
End Class

''' <summary>
''' ログイン状態/カード残高/残ポイント
''' </summary>
''' <remarks></remarks>
Public Class UserInfo
    Public Property ENTKBN As String = "0" '【0】チェックアウト【1】チェックイン
    Public Property NCSNO As String = String.Empty
    Public Property CCSNAME As String = String.Empty
    Public Property ZANKN As Integer = 0
    Public Property SRTPO As Integer = 0

    Public Function IsCheckIn() As Boolean
        Return Me.ENTKBN = "1"
    End Function
End Class

''' <summary>
''' チェックイン/チェックアウト画面の結果を格納
''' </summary>
''' <remarks></remarks>
Public Class CheckResult
    Public Property ENTKIN As Integer = 0 ' 入場料
    Public Property ENTPO As Integer = 0 ' 入場ボーナス
    Public Property OUTPO As Integer = 0 ' 退場ボーナス
    Public Property BIRTHDPO As Integer = 0 ' 誕生日ボーナス
    Public Property BIRTHMPO As Integer = 0 ' 誕生月ボーナス
    Public Property M_ENTCNT As Integer = 0 ' 月間来場回数
    Public Property M_ENTCNTPO As Integer = 0 ' 月間来場ボーナス
    Public Property MINIGAMEPO As Integer = 0 ' ﾐﾆｹﾞｰﾑで取得したボーナス
    Public Property USER_INFO As UserInfo = New UserInfo ' カード残高
    Public Property PLAYKBN As Integer = 1   '【1】一球打ち【2】打ち放題

    Public Sub New()
        ENTKIN = 0
        ENTPO = 0
        OUTPO = 0
        BIRTHDPO = 0
        BIRTHMPO = 0
        M_ENTCNT = 0
        M_ENTCNTPO = 0
        USER_INFO = New UserInfo
        PLAYKBN = 1
    End Sub
End Class

''' <summary>
''' 顧客モデル
''' </summary>
''' <remarks></remarks>
Public Class CustmerModel
    Public Property MANNO As String = ""
    Public Property CARDNO As String = ""
    Public Property OLD_MANNO As String = ""
    Public Property OLD_CARDNO As String = ""
    Public Property KBMAST As Integer = 1
    Public Property NKNKN As Integer = 0
    Public Property PREMKN As Integer = 0
    Public Property SRTPO As Integer = 0
    Public Property Enabled As Boolean = False
    Public Function Clone() As CustmerModel
        Dim obj = New CustmerModel
        obj.MANNO = Me.MANNO
        obj.CARDNO = Me.CARDNO
        obj.OLD_MANNO = Me.OLD_MANNO
        obj.OLD_CARDNO = Me.OLD_CARDNO
        obj.KBMAST = Me.KBMAST
        obj.NKNKN = Me.NKNKN
        obj.PREMKN = Me.PREMKN
        obj.SRTPO = Me.SRTPO
        obj.Enabled = Me.Enabled
        Return (obj)
    End Function
End Class

Public Class DUDNTRNModel
    Public Property DATKB As String = ""
    Public Property UDNDT As String = ""
    Public Property UDNNO As Integer = 0
    Public Property INSDTM As String
    Public Property Enabled As Boolean = False
    Public Function Clone() As DUDNTRNModel
        Dim obj = New DUDNTRNModel
        obj.DATKB = Me.DATKB
        obj.UDNDT = Me.UDNDT
        obj.UDNNO = Me.UDNNO
        obj.INSDTM = Me.INSDTM
        obj.Enabled = Me.Enabled
        Return obj
    End Function
End Class

Public Class DENTRAModel
    Public Property DATKB As String = ""
    Public Property UDNDT As String = ""
    Public Property UDNNO As Integer = 0
    Public Property LINNO As Integer = 0
    Public Property INSDTM As String
    Public Property Enabled As Boolean = False
    Public Function Clone() As DENTRAModel
        Dim obj = New DENTRAModel
        obj.DATKB = Me.DATKB
        obj.UDNDT = Me.UDNDT
        obj.UDNNO = Me.UDNNO
        obj.LINNO = Me.LINNO
        obj.INSDTM = Me.INSDTM
        obj.Enabled = Me.Enabled
        Return obj
    End Function
End Class

Public Class REPOCHARGE_MModel
    Public Property CHARGEDAY As String = ""
    Public Property HOSTNAME As String = ""
    Public Property HAKKOKAISU As Integer = 0
    Public Property Enabled As Boolean = False
    Public Function Clone() As REPOCHARGE_MModel
        Dim obj = New REPOCHARGE_MModel
        obj.CHARGEDAY = Me.CHARGEDAY
        obj.HOSTNAME = Me.HOSTNAME
        obj.HAKKOKAISU = Me.HAKKOKAISU
        obj.Enabled = Me.Enabled
        Return obj
    End Function
End Class

Public Class SEQTRNModel
    Public Property DENNOSEQ As Integer = 0
    Public Property Enabled As Boolean = False
    Public Function Clone() As SEQTRNModel
        Dim obj = New SEQTRNModel
        obj.DENNOSEQ = Me.DENNOSEQ
        obj.Enabled = Me.Enabled
        Return obj
    End Function
End Class

Public Class EjectCardRestoreModel
    Public Property CUSTMER As CustmerModel
    Public Property DUDNTRN As DUDNTRNModel
    Public Property DENTRA As DENTRAModel
    Public Property REPOCHARGE_M As REPOCHARGE_MModel
    Public Property SEQTRN As SEQTRNModel
    Public Sub New()
        Me.CUSTMER = New CustmerModel
        Me.DUDNTRN = New DUDNTRNModel
        Me.DENTRA = New DENTRAModel
        Me.REPOCHARGE_M = New REPOCHARGE_MModel
        Me.SEQTRN = New SEQTRNModel
    End Sub
End Class

' キャンセル用の例外
Public Class CancelException
    Inherits System.Exception
End Class
