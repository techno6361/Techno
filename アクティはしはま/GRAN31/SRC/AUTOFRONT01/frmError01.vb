Public Class frmError01

#Region "▼宣言部"

    ''' <summary>
    ''' AD1コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.AD1Control
    ''' <summary>
    ''' コインメック基板コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcCM As New Techno.DeviceControls.SC1708Control
    ''' <summary>
    ''' ICリーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New Techno.DeviceControls.ICR700Control
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New Techno.DeviceControls.MCH3000Control

    Private _intTouchCnt As Integer = 0

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' エラーメッセージ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ErrMessage As String
        Set(ByVal value As String)
            _strErrMessage = value
        End Set
    End Property
    Private _strErrMessage As String = String.Empty
    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ICR700 As Techno.DeviceControls.ICR700Control
        Set(ByVal value As Techno.DeviceControls.ICR700Control)
            _dcICR700 = value
        End Set
    End Property
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MCH3000 As Techno.DeviceControls.MCH3000Control
        Set(ByVal value As Techno.DeviceControls.MCH3000Control)
            _dcMCH3000 = value
        End Set
    End Property
    ''' <summary>
    ''' AD1制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AD1 As Techno.DeviceControls.AD1Control
        Set(ByVal value As Techno.DeviceControls.AD1Control)
            _dcAD1 = value
        End Set
    End Property
    ''' <summary>
    ''' コインメック制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CM As Techno.DeviceControls.SC1708Control
        Set(ByVal value As Techno.DeviceControls.SC1708Control)
            _dcCM = value
        End Set
    End Property


#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmError01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 閉じるボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 電源断ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPowerOff_Click(sender As System.Object, e As System.EventArgs) Handles btnPowerOff.Click
        Try
            '【電源断(シャットダウン)】
            '強制的にシャットダウン()
            UIFunction.AdjustToken()
            UIUtility.ExitWindowsEx(UIUtility.ExitWindows.EWX_POWEROFF Or UIUtility.ExitWindows.EWX_POWEROFF, 0)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' リセットボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Dim blnIcRw As Boolean = False
        Dim blnMch3000 As Boolean = False
        Dim blnAD1 As Boolean = False
        Dim blnCM As Boolean = False
        Try
            Me.btnReset.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            'ICRW初期化
            blnIcRw = Reset_IcRw()
            'カードユニット初期化
            blnMch3000 = Reset_MCH3000()
            '紙幣識別機初期化
            blnAD1 = Reset_AD1()
            'コインメック初期化
            blnCM = Reset_CM()

            If blnIcRw And blnMch3000 And blnAD1 Then
                Me.btnBack.Enabled = True
            Else
                Me.btnBack.Enabled = False
            End If

            If blnIcRw Then
                Me.lblIcRw.ForeColor = Color.Blue
            Else
                Me.lblIcRw.ForeColor = Color.Red
            End If
            If blnMch3000 Then
                Me.lblMCH3000.ForeColor = Color.Blue
            Else
                Me.lblMCH3000.ForeColor = Color.Red
            End If
            If blnAD1 Then
                Me.lblAD1.ForeColor = Color.Blue
            Else
                Me.lblAD1.ForeColor = Color.Red
            End If
            If blnCM Then
                Me.lblCM.ForeColor = Color.Blue
            Else
                Me.lblCM.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            Me.btnReset.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' カード排出ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEject_Click(sender As System.Object, e As System.EventArgs)
        Dim blnERRFLG As Boolean = False
        Try
            'カード排出コマンド
            Using frm As New frmREQUESTCARDEx(_dcICR700, _dcMCH3000)
                frm.COMMAND = frmREQUESTCARDEx.Command_Type.EJECT
                frm.ShowDialog()
                blnERRFLG = frm.ERRFLG
            End Using
            If blnERRFLG Then
                Using frm As New frmMSGBOXEx("カード排出に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                _dcMCH3000.Reset()
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub picError_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles picError.MouseUp
        Try
            _intTouchCnt += 1

            If _intTouchCnt >= 5 Then
                Me.pnlErrorInfo.Visible = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

    Private Sub Init()
        Try
            Me.lblErrMessage.Text = _strErrMessage

            Me.lblInBill1000.Text = _dcAD1.InBill1000.ToString
            Me.lblInBill2000.Text = _dcAD1.InBill2000.ToString
            Me.lblInBill5000.Text = _dcAD1.InBill5000.ToString
            Me.lblInBill10000.Text = _dcAD1.InBill10000.ToString

            Me.lblOutBill1000.Text = _dcAD1.OutBill1000.ToString
            If _dcAD1.OutBill1000.Equals(99) Then
                Me.lblOutBill1000.Text = "0"
            End If

            Dim states = _dcMCH3000.SR_Command
            If states(1).Equals(1) Then
            End If

            Me.lblInCM10.Text = _dcCM.InCm10.ToString
            Me.lblInCM50.Text = _dcCM.InCm50.ToString
            Me.lblInCM100.Text = _dcCM.InCm100.ToString
            Me.lblInCM500.Text = _dcCM.InCm500.ToString

            Me.lblOutCM10.Text = _dcCM.OutCm10.ToString
            Me.lblOutCM50.Text = _dcCM.OutCm50.ToString
            Me.lblOutCM100.Text = _dcCM.OutCm100.ToString
            Me.lblOutCM500.Text = _dcCM.OutCm500.ToString


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ICリーダーライター初期化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reset_IcRw() As Boolean
        Dim blnErr As Boolean = False
        Try
            '【ICR700リーダライター接続】'
            If _dcICR700.IsOpen Then
                _dcICR700.Close()
            End If
            Do
                If Not _dcICR700.Open(UIUtility.SYSTEM.ICR700COM) Then
                    blnErr = True
                    Exit Do
                End If
                Dim keys = New Byte() {&H36, &H37, &H37, &H37, &H37, &H32}
                If Not _dcICR700.AuthKeySet(keys, False, 2, 4) Then
                    blnErr = True
                    Exit Do
                End If
                If Not _dcICR700.Cancel Then
                    blnErr = True
                    Exit Do
                End If
                Exit Do
            Loop

            If blnErr Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' カードユニット初期化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reset_MCH3000() As Boolean
        Dim blnErr As Boolean = False
        Try
            If _dcMCH3000.IsOpen Then
                _dcMCH3000.Close()
            End If
            Do
                If Not _dcMCH3000.Open(UIUtility.SYSTEM.MCH3000COM) Then
                    blnErr = True
                    Exit Do
                End If
                If Not _dcMCH3000.Init() Then
                    ' 何故か電源投下時のみInitしたら初回動作＆エラーが返ってくるのでスルー
                    'blnErr = True
                End If
                If Not _dcMCH3000.Reset() Then
                    blnErr = True
                    Exit Do
                End If
                Exit Do
                ' カード詰まり強制リセット
                If Not _dcMCH3000.RS_Command(2) Then
                    blnErr = True
                    Exit Do
                End If
            Loop


            If blnErr Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ビルバリ初期化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reset_AD1() As Boolean
        Try
            _dcAD1.Close()

            _dcAD1.Open(UIUtility.SYSTEM.AD1COM)
            If _dcAD1.Connect Then
                'リセット
                If Not _dcAD1.Reset_Command Then
                End If
                '機能ﾁｪｯｸ
                If Not _dcAD1.Check_Command(20) Then
                    Return False
                End If
                'エラー確認
                If CheckAD1() Then Return True
                If Not _dcAD1.DeviceStatus1.Equals(4) Then
                    '待機中でない場合もう一度取得
                    If CheckAD1() Then Return True
                End If
            Else
                '【接続エラー】
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ビルバリ状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckAD1() As Boolean
        Try

            If Not _dcAD1.Connect Then
                Return False
            End If

            _dcAD1.Status3_Command()

            '装置ステータス1
            Select Case _dcAD1.DeviceStatus1
                Case 1
                    'パワーオン
                    Return False
                Case 2
                    'リセット動作中
                    Return False
                Case 4
                    '待機中
                Case 8
                    '紙幣挿入待ち
                    Return False
                Case 16
                    '入金動作中
                    Return False
                Case 32
                    '出金動作中
                    Return False
                Case 64
                    '入出金口紙幣受取待ち
                    Return False
                Case 128
                    'クリーニング動作中
                    Return False
                Case 0
                    'なし
                    Return False
                Case Else
                    '不明
                    Return False
            End Select

            '装置ステータス2
            Select Case _dcAD1.DeviceStatus2
                Case 1
                    'アラーム発生中
                    Return False
                Case 2
                    '払出停止動作中
                    Return False
                Case 4
                    '計数動作中
                    Return False
                Case 8
                    '計数待機中
                    Return False
                Case 16
                    '計数払出動作中
                    Return False
                Case 32
                    '計数リジェクト動作中
                    Return False
                Case 64
                    '自己診断モード待機中
                    Return False
                Case 128
                    '自己診断モード動作中
                    Return False
                Case 0
                    'なし
                Case Else
                    '不明
                    Return False
            End Select

            '紙幣収納状態1
            '還流スタッカ1満杯検出
            If Convert.ToString(_dcAD1.BillState1, 2).PadLeft(8, "0"c).Substring(7, 1).Equals("1") Then
                Return False
            End If
            '還流スタッカベース満杯検出
            If Convert.ToString(_dcAD1.BillState1, 2).PadLeft(8, "0"c).Substring(4, 1).Equals("1") Then
                Return False
            End If
            '還流スタッカ1ニアエンド検出
            If Convert.ToString(_dcAD1.BillState1, 2).PadLeft(8, "0"c).Substring(3, 1).Equals("1") Then
                'Return False
            End If
            'なし
            If _dcAD1.BillState1.Equals(0) Then
            End If

            '紙幣収納状態2
            'リジェクトボックス満杯検出
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(7, 1).Equals("1") Then
                Return False
            End If
            '扉開検出
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(6, 1).Equals("1") Then
                Return False
            End If
            'ユニット引出し検出
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(5, 1).Equals("1") Then
                Return False
            End If
            '模擬券モード
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(0, 1).Equals("1") Then
                Return False
            End If
            'なし
            If _dcAD1.BillState2.Equals(0) Then
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' コインメック初期化
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reset_CM() As Boolean
        Try
            _dcCM.Close()

            _dcCM.Open(UIUtility.SYSTEM.CMCOM)

            Return CheckCM()


        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' コインメック状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckCM() As Boolean
        Try

            _dcCM.CmDetailData()

            '【異常】

            '代表異常
            If _dcCM.CmAbnormal1.Substring(7, 1).Equals("1") Then
                Return False
            End If
            '動作可
            If _dcCM.CmAbnormal1.Substring(6, 1).Equals("1") Then
                Return False
            End If
            'アクセプター異常
            If _dcCM.CmAbnormal1.Substring(4, 1).Equals("1") Then
                Return False
            End If
            '10円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(3, 1).Equals("1") Then
                Return False
            End If
            '50円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(2, 1).Equals("1") Then
                Return False
            End If
            '100円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(1, 1).Equals("1") Then
                Return False
            End If
            '500円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(0, 1).Equals("1") Then
                Return False
            End If
            '返却スイッチ異常
            If _dcCM.CmAbnormal2.Substring(3, 1).Equals("1") Then
                Return False
            End If
            'コイン払出不良
            If _dcCM.CmAbnormal2.Substring(2, 1).Equals("1") Then
                Return False
            End If
            'セーフティスイッチ異常
            If _dcCM.CmAbnormal2.Substring(1, 1).Equals("1") Then
                Return False
            End If
            'パルススイッチ異常
            If _dcCM.CmAbnormal2.Substring(0, 1).Equals("1") Then
                Return False
            End If

            '【状態】

            'CREAM ON
            If _dcCM.CmState.Substring(7, 1).Equals("1") Then
            End If
            'インベントリ中
            If _dcCM.CmState.Substring(6, 1).Equals("1") Then
                Return False
            End If
            'つり銭払出可能
            If _dcCM.CmState.Substring(5, 1).Equals("1") Then
            End If
            '返却スイッチＯＮ
            If _dcCM.CmState.Substring(4, 1).Equals("1") Then
                Return False
            End If
            '払出終了
            If _dcCM.CmState.Substring(3, 1).Equals("1") Then
                Return False
            End If
            'クリア済み
            If _dcCM.CmState.Substring(2, 1).Equals("1") Then
                Return False
            End If
            'インベントリ状態
            If _dcCM.CmState.Substring(1, 1).Equals("1") Then
                Return False
            End If
            'つり銭あわせ SW
            If _dcCM.CmState.Substring(0, 1).Equals("1") Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region



 


End Class