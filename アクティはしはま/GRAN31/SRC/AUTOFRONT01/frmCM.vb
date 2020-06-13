Public Class frmCM

#Region "▼宣言部"
    ''' <summary>
    ''' CMコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcCM As New TECHNO.DeviceControls.SC1708Control

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' コインメック基板制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property CM As TECHNO.DeviceControls.SC1708Control
        Get
            Return _dcCM
        End Get
        Set(ByVal value As TECHNO.DeviceControls.SC1708Control)
            _dcCM = value
        End Set
    End Property
#End Region

#Region "▼イベント定義"

    Private Sub frmCM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            _dcCM.Close()

            _dcCM.Open(UIUtility.SYSTEM.CMCOM)

            Me.btnUpdInfo.PerformClick()
  

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            Me.btnConnect.BackColor = Color.ForestGreen
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

            CheckCM()

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
            _dcCM.CmDetailData()

            If _dcCM.ConnectionCheck.Equals("1") Then
                '【コインメック接続中】

                '接続中
                Me.lblConnect.ForeColor = Color.Yellow

                'コインメック情報取得
                CheckCM()

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
    ''' コインメック状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckCM()
        Try

            _dcCM.CmDetailData()

            'つり銭確認
            If _dcCM.OutPossibleCm10 <= 10 Then
                Me.lbl10yen.ForeColor = Color.Yellow
            Else
                Me.lbl10yen.ForeColor = Color.Gray
            End If
            If _dcCM.OutPossibleCm100 <= 10 Then
                Me.lbl100yen.ForeColor = Color.Yellow
            Else
                Me.lbl100yen.ForeColor = Color.Gray
            End If

            '【異常】

            '代表異常
            If _dcCM.CmAbnormal1.Substring(7, 1).Equals("1") Then
                Me.lblAbNormal1.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal1.ForeColor = Color.Gray
            End If
            '動作可
            If _dcCM.CmAbnormal1.Substring(6, 1).Equals("1") Then
                Me.lblAbNormal2.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal2.ForeColor = Color.Gray
            End If
            'アクセプター異常
            If _dcCM.CmAbnormal1.Substring(4, 1).Equals("1") Then
                Me.lblAbNormal3.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal3.ForeColor = Color.Gray
            End If
            '10円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(3, 1).Equals("1") Then
                Me.lblAbNormal4.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal4.ForeColor = Color.Gray
            End If
            '50円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(2, 1).Equals("1") Then
                Me.lblAbNormal5.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal5.ForeColor = Color.Gray
            End If
            '100円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(1, 1).Equals("1") Then
                Me.lblAbNormal6.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal6.ForeColor = Color.Gray
            End If
            '500円ｴﾝﾌﾟﾃｨ異常
            If _dcCM.CmAbnormal1.Substring(0, 1).Equals("1") Then
                Me.lblAbNormal7.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal7.ForeColor = Color.Gray
            End If
            '返却スイッチ異常
            If _dcCM.CmAbnormal2.Substring(3, 1).Equals("1") Then
                Me.lblAbNormal8.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal8.ForeColor = Color.Gray
            End If
            'コイン払出不良
            If _dcCM.CmAbnormal2.Substring(2, 1).Equals("1") Then
                Me.lblAbNormal9.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal9.ForeColor = Color.Gray
            End If
            'セーフティスイッチ異常
            If _dcCM.CmAbnormal2.Substring(1, 1).Equals("1") Then
                Me.lblAbNormal10.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal10.ForeColor = Color.Gray
            End If
            'パルススイッチ異常
            If _dcCM.CmAbnormal2.Substring(0, 1).Equals("1") Then
                Me.lblAbNormal11.ForeColor = Color.Yellow
            Else
                Me.lblAbNormal11.ForeColor = Color.Gray
            End If

            '【状態】

            'CREAM ON
            If _dcCM.CmState.Substring(7, 1).Equals("1") Then
                Me.lblState1.ForeColor = Color.Yellow
            Else
                Me.lblState1.ForeColor = Color.Gray
            End If
            'インベントリ中
            If _dcCM.CmState.Substring(6, 1).Equals("1") Then
                Me.lblState2.ForeColor = Color.Yellow
            Else
                Me.lblState2.ForeColor = Color.Gray
            End If
            'つり銭払出可能
            If _dcCM.CmState.Substring(5, 1).Equals("1") Then
                Me.lblState3.ForeColor = Color.Yellow
            Else
                Me.lblState3.ForeColor = Color.Gray
            End If
            '返却スイッチＯＮ
            If _dcCM.CmState.Substring(4, 1).Equals("1") Then
                Me.lblState4.ForeColor = Color.Yellow
            Else
                Me.lblState4.ForeColor = Color.Gray
            End If
            '払出終了
            If _dcCM.CmState.Substring(3, 1).Equals("1") Then
                Me.lblState5.ForeColor = Color.Yellow
            Else
                Me.lblState5.ForeColor = Color.Gray
            End If
            'クリア済み
            If _dcCM.CmState.Substring(2, 1).Equals("1") Then
                Me.lblState6.ForeColor = Color.Yellow
            Else
                Me.lblState6.ForeColor = Color.Gray
            End If
            'インベントリ状態
            If _dcCM.CmState.Substring(1, 1).Equals("1") Then
                Me.lblState7.ForeColor = Color.Yellow
            Else
                Me.lblState7.ForeColor = Color.Gray
            End If
            'つり銭あわせ SW
            If _dcCM.CmState.Substring(0, 1).Equals("1") Then
                Me.lblState8.ForeColor = Color.Yellow
            Else
                Me.lblState8.ForeColor = Color.Gray
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region


End Class