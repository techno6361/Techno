Public Class Sound

    ''' <summary>
    ''' 【NORMAL】通常再生【SYNC】再生が終わるまで待機【LOOPING】ループ再生
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PlayType
        NORMAL
        SYNC
        LOOPING
    End Enum

    ''' <summary>
    ''' 音声を再生する
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Private Shared Sub Play(ByVal name As String, ByVal type As PlayType)
        Try
            Dim player = New System.Media.SoundPlayer
            player.SoundLocation = UIUtility.SYSTEM.SOUND_PAHT & name
            Select Case type
                Case PlayType.NORMAL
                    player.Play()
                Case PlayType.SYNC
                    player.PlaySync()
                Case PlayType.LOOPING
                    player.PlayLooping()
            End Select
        Catch ex As Exception
            Throw ex
        End Try        
    End Sub

    ''' <summary>
    ''' カードをお入れ下さい
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayInsertCard(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("04.wav", type)
    End Sub

    ''' <summary>
    ''' カードをお受け取りください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayReceiveCard(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("12.wav", type)
    End Sub

    ''' <summary>
    ''' お金を入れてください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayPaymentMoney(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("30.wav", type)
    End Sub

    ''' <summary>
    ''' 入金限度額がオーバーしています
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayPaymentLimit(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("09.wav", type)
    End Sub

    ''' <summary>
    ''' 紙幣は20枚まで一括で入金できます
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayPaymentable20(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("28.wav", type)
    End Sub

    ''' <summary>
    ''' お釣りが出ます
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayPayoutChange(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("11.wav", type)
    End Sub

    ''' <summary>
    ''' お金を返却します
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayPayoutMoney(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("18.wav", type)
    End Sub

    ''' <summary>
    ''' お金をお入れください。
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayTouchReceipt(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("31.wav", type)
    End Sub

    ''' <summary>
    ''' ご希望の金額をタッチしてください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayTouchAmount(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("21a.wav", type)
    End Sub

    ''' <summary>
    ''' ご希望の打席をお選びください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayTouchSeat(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("22.wav", type)
    End Sub

    ''' <summary>
    ''' お取り忘れがないようにご注意ください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayForgetAttention(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("20.wav", type)
    End Sub

    ''' <summary>
    ''' ありがとうございました
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayThank(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("02.wav", type)
    End Sub

    ''' <summary>
    ''' しばらくお待ちください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayLoading(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("14.wav", type)
    End Sub

    ''' <summary>
    ''' 発券中です
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayTicketing(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("15.wav", type)
    End Sub

    ''' <summary>
    ''' ご希望のボタンにタッチしてください
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlayTouchAnyButton(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("30.wav", type)
    End Sub

    ''' <summary>
    ''' スロット_リール回転
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlaySlotReel(Optional ByVal type As PlayType = PlayType.NORMAL)
        'Play("slot_reel.wav", type)
        Play("doramu.wav", type)
    End Sub

    ''' <summary>
    ''' スロット_ストップ
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlaySlotStop(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("hazure.wav", type)
        'Play("slot_stop2.wav", type)
    End Sub

    ''' <summary>
    ''' スロット_勝利
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlaySlotWin(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("atari.wav", type)
        'Play("slot_win3.wav", type)
    End Sub

    ''' <summary>
    ''' スロット_勝利2
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub PlaySlotWin2(Optional ByVal type As PlayType = PlayType.NORMAL)
        Play("slot_win2.wav", type)
    End Sub

End Class
