Public Class frmAD1

#Region "▼宣言部"
    ''' <summary>
    ''' AD1コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.AD1Control

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' AD1制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AD1 As Techno.DeviceControls.AD1Control
        Get
            Return _dcAD1
        End Get
        Set(ByVal value As Techno.DeviceControls.AD1Control)
            _dcAD1 = value
        End Set
    End Property
#End Region

#Region "▼イベント定義"

    Private Sub frmAD1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            Me.Cursor = Cursors.WaitCursor
            Me.btnConnect.BackColor = Color.Orange
            Me.btnConnect.Refresh()

            _dcAD1.Close()

            _dcAD1.Open(UIUtility.SYSTEM.AD1COM)
            If _dcAD1.Connect Then
                Me.lblConnect.ForeColor = Color.Yellow
                'リセット
                If Not _dcAD1.Reset_Command Then
                End If
                '機能ﾁｪｯｸ
                If Not _dcAD1.Check_Command(20) Then
                    Using frm As New frmMSGBOX01("機能ﾁｪｯｸに失敗しました。" & vbCrLf & "もう一度接続ボタンを押してください。", 2)
                        frm.ShowDialog()
                    End Using
                End If
                'エラー確認
                CheckAD1()
                If Not _dcAD1.DeviceStatus1.Equals(4) Then
                    '待機中でない場合もう一度取得
                    CheckAD1()
                End If
            Else
                '【接続エラー】
                Me.lblConnect.ForeColor = Color.Silver
                Using frm As New frmMSGBOX01("ビルバリと接続できませんでした。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            Me.btnConnect.BackColor = Color.ForestGreen
        End Try
    End Sub

    ''' <summary>
    ''' クリーニングボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCleaning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCleaning.Click
        Dim blnErr As Boolean = False
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.btnCleaning.BackColor = Color.Orange
            Me.btnCleaning.Refresh()

            _dcAD1.Cleaning_Command()

            '_dcAD1.Status2_Command()
            ''[スタックイン]
            'If _dcAD1.Sensor2.Substring(7, 1).Equals("0") Then
            '    '【通光】
            '    blnErr = True
            'End If
            ''[スタッカ残留]
            'If _dcAD1.Sensor2.Substring(6, 1).Equals("0") Then
            '    '【通光】
            '    blnErr = True
            'End If

            'If blnErr Then
            '    Using frm As New frmMSGBOX01("ｸﾘｰﾆﾝｸﾞｶｰﾄﾞをセットして下さい。", 3)
            '        frm.ShowDialog()
            '    End Using
            'Else
            '    _dcAD1.Cleaning_Command()
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            Me.btnCleaning.BackColor = Color.Salmon
        End Try
    End Sub

    ''' <summary>
    ''' 情報更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdInfo.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.btnUpdInfo.BackColor = Color.Orange
            Me.btnUpdInfo.Refresh()

            CheckAD1()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            Me.btnUpdInfo.BackColor = Color.LightSteelBlue
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
            If _dcAD1.Connect Then
                '【ビルバリ接続中】

                '接続中
                Me.lblConnect.ForeColor = Color.Yellow

                'ビルバリ情報取得
                CheckAD1()

            Else

                '【ビルバリ未接続】

                '接続中
                Me.lblConnect.ForeColor = Color.Silver

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ビルバリ状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckAD1()
        Try

            _dcAD1.Status3_Command()



            '装置ステータス1
            Me.lblDeviceStatus1_0.ForeColor = Color.Silver
            Me.lblDeviceStatus1_1.ForeColor = Color.Silver
            Me.lblDeviceStatus1_2.ForeColor = Color.Silver
            Me.lblDeviceStatus1_4.ForeColor = Color.Silver
            Me.lblDeviceStatus1_8.ForeColor = Color.Silver
            Me.lblDeviceStatus1_16.ForeColor = Color.Silver
            Me.lblDeviceStatus1_32.ForeColor = Color.Silver
            Me.lblDeviceStatus1_64.ForeColor = Color.Silver
            Me.lblDeviceStatus1_128.ForeColor = Color.Silver
            Select Case _dcAD1.DeviceStatus1
                Case 1
                    'パワーオン
                    Me.lblDeviceStatus1_1.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 2
                    'リセット動作中
                    Me.lblDeviceStatus1_2.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 4
                    '待機中
                    Me.lblDeviceStatus1_4.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 8
                    '紙幣挿入待ち
                    Me.lblDeviceStatus1_8.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 16
                    '入金動作中
                    Me.lblDeviceStatus1_16.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 32
                    '出金動作中
                    Me.lblDeviceStatus1_32.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 64
                    '入出金口紙幣受取待ち
                    Me.lblDeviceStatus1_64.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 128
                    'クリーニング動作中
                    Me.lblDeviceStatus1_128.ForeColor = Color.Yellow
                    Me.lblDeviceStatus1_0.ForeColor = Color.Yellow
                Case 0
                    'なし
                Case Else
                    '不明
            End Select

            '装置ステータス2
            Me.lblDeviceStatus2_0.ForeColor = Color.Silver
            Me.lblDeviceStatus2_1.ForeColor = Color.Silver
            Me.lblDeviceStatus2_2.ForeColor = Color.Silver
            Me.lblDeviceStatus2_4.ForeColor = Color.Silver
            Me.lblDeviceStatus2_8.ForeColor = Color.Silver
            Me.lblDeviceStatus2_16.ForeColor = Color.Silver
            Me.lblDeviceStatus2_32.ForeColor = Color.Silver
            Me.lblDeviceStatus2_64.ForeColor = Color.Silver
            Me.lblDeviceStatus2_128.ForeColor = Color.Silver
            Select Case _dcAD1.DeviceStatus2
                Case 1
                    'アラーム発生中
                    Me.lblDeviceStatus2_1.ForeColor = Color.Yellow
                Case 2
                    '払出停止動作中
                    Me.lblDeviceStatus2_2.ForeColor = Color.Yellow
                Case 4
                    '計数動作中
                    Me.lblDeviceStatus2_4.ForeColor = Color.Yellow
                Case 8
                    '計数待機中
                    Me.lblDeviceStatus2_8.ForeColor = Color.Yellow
                Case 16
                    '計数払出動作中
                    Me.lblDeviceStatus2_16.ForeColor = Color.Yellow
                Case 32
                    '計数リジェクト動作中
                    Me.lblDeviceStatus2_32.ForeColor = Color.Yellow
                Case 64
                    '自己診断モード待機中
                    Me.lblDeviceStatus2_64.ForeColor = Color.Yellow
                Case 128
                    '自己診断モード動作中
                    Me.lblDeviceStatus2_128.ForeColor = Color.Yellow
                Case 0
                    'なし
                    Me.lblDeviceStatus2_0.ForeColor = Color.Yellow
                Case Else
                    '不明
            End Select

            '紙幣収納状態1
            Me.lblBillState1_0.ForeColor = Color.Silver
            Me.lblBillState1_1.ForeColor = Color.Silver
            Me.lblBillState1_8.ForeColor = Color.Silver
            Me.lblBillState1_16.ForeColor = Color.Silver

            '還流スタッカ1満杯検出
            If Convert.ToString(_dcAD1.BillState1, 2).PadLeft(8, "0"c).Substring(7, 1).Equals("1") Then
                Me.lblBillState1_1.ForeColor = Color.Yellow
            End If
            '還流スタッカベース満杯検出
            If Convert.ToString(_dcAD1.BillState1, 2).PadLeft(8, "0"c).Substring(4, 1).Equals("1") Then
                Me.lblBillState1_8.ForeColor = Color.Yellow
            End If
            '還流スタッカ1ニアエンド検出
            If Convert.ToString(_dcAD1.BillState1, 2).PadLeft(8, "0"c).Substring(3, 1).Equals("1") Then
                Me.lblBillState1_16.ForeColor = Color.Yellow
            End If
            'なし
            If _dcAD1.BillState1.Equals(0) Then
                Me.lblBillState1_0.ForeColor = Color.Yellow
            End If


            '紙幣収納状態2
            Me.lblBillState2_0.ForeColor = Color.Silver
            Me.lblBillState2_1.ForeColor = Color.Silver
            Me.lblBillState2_2.ForeColor = Color.Silver
            Me.lblBillState2_4.ForeColor = Color.Silver
            Me.lblBillState2_128.ForeColor = Color.Silver

            'リジェクトボックス満杯検出
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(7, 1).Equals("1") Then
                Me.lblBillState2_1.ForeColor = Color.Yellow
            End If
            '扉開検出
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(6, 1).Equals("1") Then
                Me.lblBillState2_2.ForeColor = Color.Yellow
            End If
            'ユニット引出し検出
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(5, 1).Equals("1") Then
                Me.lblBillState2_4.ForeColor = Color.Yellow
            End If
            '模擬券モード
            If Convert.ToString(_dcAD1.BillState2, 2).PadLeft(8, "0"c).Substring(0, 1).Equals("1") Then
                Me.lblBillState2_128.ForeColor = Color.Yellow
            End If
            'なし
            If _dcAD1.BillState2.Equals(0) Or _dcAD1.BillState2.Equals(16) Then
                Me.lblBillState2_0.ForeColor = Color.Yellow
            End If

            ' **** センサーステータス取得 ****

            Me.lblSensorState1_0.ForeColor = Color.Yellow
            Me.lblSensorState1_1.ForeColor = Color.Silver
            Me.lblSensorState1_2.ForeColor = Color.Silver
            Me.lblSensorState1_3.ForeColor = Color.Silver
            Me.lblSensorState1_4.ForeColor = Color.Silver
            Me.lblSensorState1_5.ForeColor = Color.Silver
            Me.lblSensorState1_6.ForeColor = Color.Silver
            Me.lblSensorState1_7.ForeColor = Color.Silver
            Me.lblSensorState1_8.ForeColor = Color.Silver

            Me.lblSensorState2_0.ForeColor = Color.Yellow
            Me.lblSensorState2_1.ForeColor = Color.Silver
            Me.lblSensorState2_2.ForeColor = Color.Silver
            Me.lblSensorState2_3.ForeColor = Color.Silver
            Me.lblSensorState2_4.ForeColor = Color.Silver
            Me.lblSensorState2_5.ForeColor = Color.Silver
            Me.lblSensorState2_6.ForeColor = Color.Silver
            Me.lblSensorState2_7.ForeColor = Color.Silver
            Me.lblSensorState2_8.ForeColor = Color.Silver

            ' センサーステータス2取得
            If Not _dcAD1.Status2_Command Then
            End If

            System.Threading.Thread.Sleep(100)

            ' センサーステータス3取得
            If Not _dcAD1.Status3_Command Then
            End If

            Dim blnSensorError1 = False

            If Not String.IsNullOrEmpty(_dcAD1.Sensor1) Then
                '[一括入口右]
                If _dcAD1.Sensor1.Substring(7, 1).Equals("1") Then
                    Me.lblSensorState1_1.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[一括入口左]
                If _dcAD1.Sensor1.Substring(6, 1).Equals("1") Then
                    Me.lblSensorState1_2.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[識別部入口右]
                If _dcAD1.Sensor1.Substring(5, 1).Equals("1") Then
                    Me.lblSensorState1_3.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[識別部入口左]
                If _dcAD1.Sensor1.Substring(4, 1).Equals("1") Then
                    Me.lblSensorState1_4.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[通過上]
                If _dcAD1.Sensor1.Substring(3, 1).Equals("1") Then
                    Me.lblSensorState1_5.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[通過下]
                If _dcAD1.Sensor1.Substring(2, 1).Equals("1") Then
                    Me.lblSensorState1_6.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[プッシャ上] ※
                If _dcAD1.Sensor1.Substring(1, 1).Equals("0") Then
                    Me.lblSensorState1_7.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
                '[プッシャ下]
                If _dcAD1.Sensor1.Substring(0, 1).Equals("1") Then
                    Me.lblSensorState1_8.ForeColor = Color.Yellow
                    blnSensorError1 = True
                End If
            End If

            If blnSensorError1 Then
                Me.lblSensorState1_0.ForeColor = Color.Silver
            End If

            Dim blnSensorError2 = False

            If Not String.IsNullOrEmpty(_dcAD1.Sensor2) Then
                '[スタックイン]
                If _dcAD1.Sensor2.Substring(7, 1).Equals("1") Then
                    Me.lblSensorState2_1.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[スタッカ残留]
                If _dcAD1.Sensor2.Substring(6, 1).Equals("1") Then
                    Me.lblSensorState2_2.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[払出口]
                If _dcAD1.Sensor2.Substring(5, 1).Equals("1") Then
                    Me.lblSensorState2_3.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[ﾘｼﾞｪｸﾄBOX・ｴﾝﾌﾟﾃｨ]
                If _dcAD1.Sensor2.Substring(4, 1).Equals("1") Then
                    Me.lblSensorState2_4.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[リフタ上]
                If _dcAD1.Sensor2.Substring(3, 1).Equals("1") Then
                    Me.lblSensorState2_5.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[リフタ下] ※
                If _dcAD1.Sensor2.Substring(2, 1).Equals("0") Then
                    Me.lblSensorState2_6.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[識別部内部]
                If _dcAD1.Sensor2.Substring(1, 1).Equals("1") Then
                    Me.lblSensorState2_7.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
                '[一括ポジション]
                If _dcAD1.Sensor2.Substring(0, 1).Equals("1") Then
                    Me.lblSensorState2_8.ForeColor = Color.Yellow
                    blnSensorError2 = True
                End If
            End If

            If blnSensorError2 Then
                Me.lblSensorState2_0.ForeColor = Color.Silver
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region


End Class