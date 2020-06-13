Public Class frmHAKKEN01_STR610

#Region "▼宣言部"
    ''' <summary>
    ''' レシートプリンタコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSTR610 As New Techno.DeviceControls.STR610Control

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' レシートプリンター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property STR610 As Techno.DeviceControls.STR610Control
        Get
            Return _dcSTR610
        End Get
        Set(ByVal value As Techno.DeviceControls.STR610Control)
            _dcSTR610 = value
        End Set
    End Property
#End Region

#Region "▼イベント定義"

    Private Sub frmPRINTER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 接続ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Try
            _dcSTR610.Close()

            _dcSTR610.Open(UIUtility.SYSTEM.STR610COM)
            If _dcSTR610.IsOpen Then
                Me.lblConnect.ForeColor = Color.Yellow

                _dcSTR610.Initialize()
            Else
                '【接続エラー】
                Me.lblConnect.ForeColor = Color.Silver
                Using frm As New frmMSGBOX01("カード発券機と接続できませんでした。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CheckCardDispenser()
        End Try
    End Sub

    ''' <summary>
    ''' テスト印刷ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Try
            Cursor = Cursors.WaitCursor
            Me.btnTest.Enabled = False

            ' カード待ち受け状態をスキップ
            _dcSTR610.EjectCard(0)

            ' カードが早く抜かれすぎた時の対策
            ' 1: カード排出 2: カード引き抜き命令終了 3: カード引き抜き待機中
            Dim stage = _dcSTR610._ejectStage

            If stage >= 2 Then
                ' 【カード発券成功】
                ' カードを引き抜くまでループ
                Do
                    Application.DoEvents() ' 多重クリック対策
                    _dcSTR610.GetUnitStatus()
                    If Not _dcSTR610.UnitMessage = Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_41H Then
                        Exit Do
                    End If
                Loop

            Else
                ' 【カード発券エラー】
                Using frm As New frmMSGBOX01("カード発券に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            Me.btnTest.Enabled = True
            CheckCardDispenser()
        End Try
    End Sub

    ''' <summary>
    ''' 戻るボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            If _dcSTR610.IsOpen Then
                '【接続中】

                '接続中
                Me.lblConnect.ForeColor = Color.Yellow

                'エラー取得
                CheckCardDispenser()

                Me.btnTest.Enabled = True
            Else

                '【未接続】

                '接続中
                Me.lblConnect.ForeColor = Color.Silver

                Me.btnTest.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' プリンター状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckCardDispenser()
        Try
            Me.lbl41H.ForeColor = Color.Silver
            Me.lbl42H.ForeColor = Color.Silver
            Me.lbl43H.ForeColor = Color.Silver
            Me.lbl44H.ForeColor = Color.Silver
            Me.lblNONE0.ForeColor = Color.Silver

            Me.lblTSNS.ForeColor = Color.Silver
            Me.lblESNS.ForeColor = Color.Silver
            Me.lblHSNS.ForeColor = Color.Silver
            Me.lblNSNS.ForeColor = Color.Silver
            Me.lblNONE1.ForeColor = Color.Silver

            Me.lblDIP1.ForeColor = Color.Silver
            Me.lblDIP2.ForeColor = Color.Silver
            Me.lblDIP3.ForeColor = Color.Silver
            Me.lblDIP4.ForeColor = Color.Silver
            Me.lblNONE2.ForeColor = Color.Silver

            Me.lblTIN1.ForeColor = Color.Silver
            Me.lblTIN2.ForeColor = Color.Silver
            Me.lblTIN3.ForeColor = Color.Silver
            Me.lblTOUT3.ForeColor = Color.Silver
            Me.lblNONE3.ForeColor = Color.Silver

            Dim error0 = False
            Dim error1 = False
            Dim error2 = False
            Dim error3 = False
            Dim near_end = False

            ' 接続エラー
            If Not _dcSTR610.IsOpen Then
                _dcSTR610.Close()
                btnTest.Enabled = False
                lblConnect.ForeColor = Color.Silver
                Exit Sub
            End If

            ' ユニットステータス
            If Not _dcSTR610.GetUnitStatus() Then
                Select Case _dcSTR610.UnitMessage
                    Case Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_41H
                        Me.lbl41H.ForeColor = Color.Yellow
                        error0 = True
                    Case Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_42H
                        Me.lbl42H.ForeColor = Color.Yellow
                        error0 = True
                    Case Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_43H ' 初期化中
                        Me.lbl43H.ForeColor = Color.Yellow
                        Me.Panel4.Refresh()
                        Threading.Thread.Sleep(100)
                        Me.lbl43H.ForeColor = Color.Silver
                    Case Techno.DeviceControls.STR610Control.UNIT_STATUS.UNIT_44H
                        Me.lbl44H.ForeColor = Color.Yellow
                        error0 = True
                End Select
            End If

            ' センサーステータス
            If Not _dcSTR610.GetSensorStatus Then
                For Each msg In _dcSTR610.SensorMessages
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.TSNS_ON Then
                        Me.lblTSNS.ForeColor = Color.Yellow
                        error1 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.ESNS_ON Then
                        Me.lblESNS.ForeColor = Color.Yellow
                        error1 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.HSNS_ON Then
                        Me.lblHSNS.ForeColor = Color.Yellow
                        error1 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.NSNS_ON Then ' ニアエンド
                        Me.lblNSNS.ForeColor = Color.Yellow
                        near_end = True
                    End If

                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.DIP1_OFF Then
                        Me.lblDIP1.ForeColor = Color.Yellow
                        error2 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.DIP2_OFF Then
                        Me.lblDIP2.ForeColor = Color.Yellow
                        error2 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.DIP3_OFF Then
                        Me.lblDIP3.ForeColor = Color.Yellow
                        error2 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.DIP4_OFF Then
                        Me.lblDIP4.ForeColor = Color.Yellow
                        error2 = True
                    End If

                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.TOUT3_LOW Then
                        Me.lblTOUT3.ForeColor = Color.Yellow
                        error3 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.TIN1_LOW Then
                        Me.lblTIN1.ForeColor = Color.Yellow
                        error3 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.TIN2_LOW Then
                        Me.lblTIN2.ForeColor = Color.Yellow
                        error3 = True
                    End If
                    If msg = Techno.DeviceControls.STR610Control.SENSOR_STATUS.TIN3_LOW Then
                        Me.lblTIN3.ForeColor = Color.Yellow
                        error3 = True
                    End If
                Next
            End If

            If Not error0 Then
                Me.lblNONE0.ForeColor = Color.Yellow
            End If

            If Not error1 And Not near_end Then
                Me.lblNONE1.ForeColor = Color.Yellow
            End If

            If Not error2 Then
                Me.lblNONE2.ForeColor = Color.Yellow
            End If

            If Not error3 Then
                Me.lblNONE3.ForeColor = Color.Yellow
            End If

            If error0 Or error1 Or error2 Or error3 Then
                _dcSTR610.Close()
                btnTest.Enabled = False
                lblConnect.ForeColor = Color.Silver
            Else
                btnTest.Enabled = True
                lblConnect.ForeColor = Color.Yellow
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class