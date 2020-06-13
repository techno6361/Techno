Public Class frmAD1

#Region "▼宣言部"
    ''' <summary>
    ''' AD1コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.SC1708Control

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' AD1制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property AD1 As Techno.DeviceControls.SC1708Control
        Get
            Return _dcAD1
        End Get
        Set(ByVal value As Techno.DeviceControls.SC1708Control)
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
            If _dcAD1.Reset Then
                '接続があるか確認
                If Not _dcAD1.ConnectionCheck.Equals("2") Then
                    Me.lblConnect.ForeColor = Color.Yellow
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
            _dcAD1.BvDetailData()

            If _dcAD1.ConnectionCheck.Equals("2") Then
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
            _dcAD1.BvDetailData()

            '紙幣受け入れ
            If _dcAD1.BvState1.Substring(7, 1).Equals("1") Then Me.lblBvState1_1.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(7, 1).Equals("0") Then Me.lblBvState1_1.ForeColor = Color.Silver
            '紙幣回収中
            If _dcAD1.BvState1.Substring(6, 1).Equals("1") Then Me.lblBvState1_2.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(6, 1).Equals("0") Then Me.lblBvState1_2.ForeColor = Color.Silver
            '後続金無し
            If _dcAD1.BvState1.Substring(5, 1).Equals("1") Then Me.lblBvState1_3.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(5, 1).Equals("0") Then Me.lblBvState1_3.ForeColor = Color.Silver
            '払出終了
            If _dcAD1.BvState1.Substring(4, 1).Equals("1") Then Me.lblBvState1_4.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(4, 1).Equals("0") Then Me.lblBvState1_4.ForeColor = Color.Silver
            'クリア済み
            If _dcAD1.BvState1.Substring(3, 1).Equals("1") Then Me.lblBvState1_5.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(3, 1).Equals("0") Then Me.lblBvState1_5.ForeColor = Color.Silver
            '識別動作中
            If _dcAD1.BvState1.Substring(2, 1).Equals("1") Then Me.lblBvState1_6.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(2, 1).Equals("0") Then Me.lblBvState1_6.ForeColor = Color.Silver
            '収金動作中
            If _dcAD1.BvState1.Substring(1, 1).Equals("1") Then Me.lblBvState1_7.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(1, 1).Equals("0") Then Me.lblBvState1_7.ForeColor = Color.Silver
            '返金動作中
            If _dcAD1.BvState1.Substring(0, 1).Equals("1") Then Me.lblBvState1_8.ForeColor = Color.Yellow
            If _dcAD1.BvState1.Substring(0, 1).Equals("0") Then Me.lblBvState1_8.ForeColor = Color.Silver

            '引き抜き
            If _dcAD1.BvState2.Substring(0, 1).Equals("1") Then Me.lblBvState2_8.ForeColor = Color.Yellow
            If _dcAD1.BvState2.Substring(0, 1).Equals("0") Then Me.lblBvState2_8.ForeColor = Color.Silver
            '1000円紙幣受け入れ
            If _dcAD1.BvState2.Substring(7, 1).Equals("1") Then Me.lblBvState2_1.ForeColor = Color.Yellow
            If _dcAD1.BvState2.Substring(7, 1).Equals("0") Then Me.lblBvState2_1.ForeColor = Color.Silver


            '代表異常
            If _dcAD1.BvAbnormal1.Substring(0, 1).Equals("1") Then Me.lblBvAbnormal1_8.ForeColor = Color.Yellow
            If _dcAD1.BvAbnormal1.Substring(0, 1).Equals("0") Then Me.lblBvAbnormal1_8.ForeColor = Color.Silver
            '識別部異常
            If _dcAD1.BvAbnormal1.Substring(2, 1).Equals("1") Then Me.lblBvAbnormal2_6.ForeColor = Color.Yellow
            If _dcAD1.BvAbnormal1.Substring(2, 1).Equals("0") Then Me.lblBvAbnormal2_6.ForeColor = Color.Silver
            'スタッカー異常
            If _dcAD1.BvAbnormal1.Substring(3, 1).Equals("1") Then Me.lblBvAbnormal2_5.ForeColor = Color.Yellow
            If _dcAD1.BvAbnormal1.Substring(3, 1).Equals("0") Then Me.lblBvAbnormal2_5.ForeColor = Color.Silver
            '札詰まり
            If _dcAD1.BvAbnormal1.Substring(4, 1).Equals("1") Then Me.lblBvAbnormal2_4.ForeColor = Color.Yellow
            If _dcAD1.BvAbnormal1.Substring(4, 1).Equals("0") Then Me.lblBvAbnormal2_4.ForeColor = Color.Silver
            '紙幣払出異常
            If _dcAD1.BvAbnormal1.Substring(5, 1).Equals("1") Then Me.lblBvAbnormal2_3.ForeColor = Color.Yellow
            If _dcAD1.BvAbnormal1.Substring(5, 1).Equals("0") Then Me.lblBvAbnormal2_3.ForeColor = Color.Silver
            '金庫満杯
            If _dcAD1.BvAbnormal1.Substring(7, 1).Equals("1") Then Me.lblBvAbnormal2_1.ForeColor = Color.Yellow
            If _dcAD1.BvAbnormal1.Substring(7, 1).Equals("0") Then Me.lblBvAbnormal2_1.ForeColor = Color.Silver


        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region


End Class