Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Ipc
Imports System.Runtime.Remoting.Lifetime

Public Class IPCSERVICE
    Public Shared CHANNEL As IpcClientChannel
End Class

Public Class IpcRemoteObject
    Inherits MarshalByRefObject

    Public Property LRMULTI01() As Integer
    Public Property LRMULTI02() As Integer
    Public Property LRMULTI03() As Integer
    Public Property LRMULTI04() As Integer
    Public Property LRMULTI05() As Integer
    Public Property LSTNO1F() As Integer
    Public Property LSTNO2F() As Integer
    Public Property LSTNO3F() As Integer
    Public Property FLRSU() As Integer
    Public Property TELOP() As String
    Public Property SEATINFO() As DataTable
    Public Property LOCATION_X As Integer
    Public Property MULTIDISP_TYPE As Integer

    ' 更新タイミングで使用
    Public Property UPDATED As Boolean

    ''' <summary>
    ''' 自動的に切断されるのを回避する
    ''' </summary>
    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function

    '*******
    Public Property NOWTIMEKB As Integer
    Public Property YOBIDASHI As Boolean
    Public Property NKBNO11_CNT As Integer
    Public Property NKBNO12_CNT As Integer
    Public Property NKBNO13_CNT As Integer
    Public Property ENT_CNT As Integer
    Public Property SUMBALL As Integer
    Public Property SHOPNO As String
    Public Property DbInfo_PATH As String
    Public Property SEAT_COM As String
    Public Property SLEEP_TIME As Integer
    '*******

End Class

Public Class IpcServer

    Public Property SYSTEM() As IpcRemoteObject

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' リース期間を無期限にする
        Lifetime.LifetimeServices.LeaseTime = TimeSpan.Zero

        ' サーバーチャンネルの生成
        Dim channel = New IpcServerChannel("ipcSample")        

        ' チャンネルを登録
        ChannelServices.RegisterChannel(channel, True)

        ' リモートオブジェクトを生成して公開
        SYSTEM = New IpcRemoteObject()
        RemotingServices.Marshal(SYSTEM, "test", GetType(IpcRemoteObject))
    End Sub
End Class

Public Class IpcClient

    Public Property SYSTEM() As IpcRemoteObject

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' クライアントチャンネルの生成
        ' チャンネルを登録
        If IPCSERVICE.CHANNEL Is Nothing Then
            IPCSERVICE.CHANNEL = New IpcClientChannel()
            ChannelServices.RegisterChannel(IPCSERVICE.CHANNEL, True)
        End If

        ' リモートオブジェクトを取得
        SYSTEM = TryCast(Activator.GetObject(GetType(IpcRemoteObject), "ipc://ipcSample/test"), IpcRemoteObject)
    End Sub
End Class
