Public Class Images

    ''' <summary>
    ''' 画像ルートパスの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRootImagePath() As String
        Return System.Configuration.ConfigurationManager.AppSettings("ROOT_IMAGE_PATH")
    End Function

    Public Shared Function BtnReceiptChecked() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_receipt_enable.png", GetRootImagePath))
    End Function

    Public Shared Function BtnReceiptUnchecked() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_receipt_disable.png", GetRootImagePath))
    End Function

    Public Shared Function BtnCheckIn() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_checkin_small.png", GetRootImagePath))
    End Function

    Public Shared Function BtnCheckOut() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_checkout_small.png", GetRootImagePath))
    End Function

    Public Shared Function BtnEjectCard() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_eject_card.png", GetRootImagePath))
    End Function

    Public Shared Function IconChargeResultCardAndReceipt() As Bitmap
        Return New Bitmap(String.Format("{0}\terminate_icon_01.png", GetRootImagePath))
    End Function

    Public Shared Function IconChargeResultCardOnly() As Bitmap
        Return New Bitmap(String.Format("{0}\terminate_icon_02.png", GetRootImagePath))
    End Function

    Public Shared Function FrameMenuBlank() As Bitmap
        Return New Bitmap(GetRootImagePath & "\frame_menu_02.png")
    End Function

    Public Shared Function LblError() As Bitmap
        Return New Bitmap(GetRootImagePath & "\pop_info_error_02.png")
    End Function

    Public Shared Function LblCaution() As Bitmap
        Return New Bitmap(GetRootImagePath & "\pop_info_error_03.png")
    End Function

    Public Shared Function IconCheckIn() As Bitmap
        Return New Bitmap(String.Format("{0}\icon_checkin.png", GetRootImagePath))
    End Function

    Public Shared Function IconCheckOut() As Bitmap
        Return New Bitmap(String.Format("{0}\icon_checkout.png", GetRootImagePath))
    End Function

    Public Shared Function IconInfomation() As Bitmap
        Return New Bitmap(String.Format("{0}\INFOMATION.png", GetRootImagePath))
    End Function

    Public Shared Function IconExclamation() As Bitmap
        Return New Bitmap(String.Format("{0}\EXCLAMATION.png", GetRootImagePath))
    End Function

    Public Shared Function IconQuestion() As Bitmap
        Return New Bitmap(String.Format("{0}\QUESTION.png", GetRootImagePath))
    End Function

    Public Shared Function IconError() As Bitmap
        Return New Bitmap(String.Format("{0}\ERROR.png", GetRootImagePath))
    End Function

    Public Shared Function IconBonus() As Bitmap
        Return New Bitmap(String.Format("{0}\PRESENT.png", GetRootImagePath))
    End Function

    Public Shared Function IconMoney() As Bitmap
        Return New Bitmap(String.Format("{0}\MONEY.png", GetRootImagePath))
    End Function

    Public Shared Function IconSlot() As Bitmap
        Return New Bitmap(String.Format("{0}\SLOT.png", GetRootImagePath))
    End Function

    Public Shared Function GIFLoading() As Bitmap
        Return New Bitmap(String.Format("{0}\loading_01.gif", GetRootImagePath))
    End Function

    Public Shared Function GIFInsertCard() As Bitmap
        Return New Bitmap(String.Format("{0}\insert_card_01.gif", GetRootImagePath))
    End Function

    Public Shared Function GIFSetICCard() As Bitmap
        Return New Bitmap(String.Format("{0}\insert_card_03.gif", GetRootImagePath))
    End Function

    Public Shared Function GIFEjectCard() As Bitmap
        Return New Bitmap(String.Format("{0}\eject_card_01.gif", GetRootImagePath))
    End Function

    Public Shared Function PicBillStop01() As Bitmap
        Return New Bitmap(String.Format("{0}\pic_billstop_01.png", GetRootImagePath))
    End Function

    Public Shared Function PicBillStop02() As Bitmap
        Return New Bitmap(String.Format("{0}\pic_billstop_02.png", GetRootImagePath))
    End Function

    Public Shared Function PicBillStop03() As Bitmap
        Return New Bitmap(String.Format("{0}\pic_billstop_03.png", GetRootImagePath))
    End Function

    Public Shared Function bgDummy3() As Bitmap
        Return New Bitmap(String.Format("{0}\frmDummy_03.png", GetRootImagePath))
    End Function

    Public Shared Function bgDummy4() As Bitmap
        Return New Bitmap(String.Format("{0}\frmDummy_04.png", GetRootImagePath))
    End Function

    Public Shared Function PicSLOT1() As Bitmap
        Return New Bitmap(String.Format("{0}\slot_reel_01B.png", GetRootImagePath))
    End Function

    Public Shared Function PicSLOT2() As Bitmap
        Return New Bitmap(String.Format("{0}\slot_reel_02B.png", GetRootImagePath))
    End Function

    Public Shared Function PicSLOT3() As Bitmap
        Return New Bitmap(String.Format("{0}\slot_reel_03B.png", GetRootImagePath))
    End Function

    Public Shared Function PicSLOT4() As Bitmap
        Return New Bitmap(String.Format("{0}\slot_reel_04B.png", GetRootImagePath))
    End Function

    Public Shared Function BtnRetry() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_retry.png", GetRootImagePath))
    End Function

    Public Shared Function BtnExit() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_exit.png", GetRootImagePath))
    End Function

    Public Shared Function BtnClose() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_close.png", GetRootImagePath))
    End Function

    Public Shared Function BtnBack() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_back.png", GetRootImagePath))
    End Function

    Public Shared Function BtnSkip() As Bitmap
        Return New Bitmap(String.Format("{0}\btn_skip.png", GetRootImagePath))
    End Function

End Class
