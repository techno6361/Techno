Public Class frmError01

#Region "▼宣言部"

    ''' <summary>
    ''' AD1コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.SC1708Control
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
    Public WriteOnly Property AD1 As Techno.DeviceControls.SC1708Control
        Set(ByVal value As Techno.DeviceControls.SC1708Control)
            _dcAD1 = value
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
        Try
            Me.btnReset.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            'ICRW初期化
            blnIcRw = Reset_IcRw()
            'カードユニット初期化
            blnMch3000 = Reset_MCH3000()
            '紙幣識別機初期化
            blnAD1 = Reset_AD1()

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

            Dim states = _dcMCH3000.SR_Command
            If states(1).Equals(1) Then
            End If


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
            If _dcAD1.Reset Then
                '接続があるか確認
                If Not _dcAD1.ConnectionCheck.Equals("2") Then
                    Return False
                End If
            Else
                Return False
            End If
            If Not CheckAD1() Then
                Return False
            End If

            Return True
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
            _dcAD1.BvDetailData()

            '代表異常
            If _dcAD1.BvAbnormal1.Substring(7, 1).Equals("1") Then
                lblErrMessage.Text = "【代表異常】"
            End If
            '識別部異常
            If _dcAD1.BvAbnormal1.Substring(5, 1).Equals("1") Then
                lblErrMessage.Text &= "【識別部異常】"
                Return False
            End If
            'スタッカー異常
            If _dcAD1.BvAbnormal1.Substring(4, 1).Equals("1") Then
                lblErrMessage.Text &= "【スタッカー異常】"
                Return False
            End If
            '札詰まり
            If _dcAD1.BvAbnormal1.Substring(3, 1).Equals("1") Then
                lblErrMessage.Text &= "【札詰まり】"
                Return False
            End If
            '紙幣払出異常
            If _dcAD1.BvAbnormal1.Substring(2, 1).Equals("1") Then
                lblErrMessage.Text &= "【紙幣払出異常】"
                Return False
            End If
            '金庫満杯
            If _dcAD1.BvAbnormal1.Substring(1, 1).Equals("1") Then
                lblErrMessage.Text &= "【金庫満杯】"
                Return False
            End If

            '紙幣回収中
            If _dcAD1.BvState1.Substring(6, 1).Equals("1") Then
                lblErrMessage.Text &= "【紙幣回収中】"
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region



 


End Class