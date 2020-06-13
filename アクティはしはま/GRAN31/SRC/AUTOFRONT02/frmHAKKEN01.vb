Public Class frmHAKKEN01

#Region "▼宣言部"
    ''' <summary>
    ''' 搬送ユニットコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New TECHNO.DeviceControls.MCH3000Control

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' レシートプリンター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property MCH3000 As TECHNO.DeviceControls.MCH3000Control
        Get
            Return _dcMCH3000
        End Get
        Set(ByVal value As TECHNO.DeviceControls.MCH3000Control)
            _dcMCH3000 = value
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
        Dim blnErr = False
        Try
            '【MCH3000カード搬送ユニット接続】'

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
            Loop


            If blnErr Then
                Using frm As New frmMSGBOX01("カード搬送ユニットとの接続に失敗しました。", 2)
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
    ''' テストボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Try
            Cursor = Cursors.WaitCursor
            Me.btnTest.Enabled = False

            Dim states = _dcMCH3000.SR_Command
            If states(2) = 0 Then
                ' エンプティ
                Throw New Exception
            End If

            ' 発行(CD→コンタクト部に停止)
            If Not _dcMCH3000.SD_Command() Then
                ' 発券エラー
                Throw New Exception
            End If

            ' コンタクト部にたどり着くまで待機
            Dim blnErr1 = True
            For i = 0 To 1000
                states = _dcMCH3000.SR_Command
                If states(1) = 1 Then
                    blnErr1 = False
                    Exit For
                End If
            Next

            ' カード詰まり1
            If blnErr1 Then
                Throw New Exception
            End If

            ' 払い出し
            If Not _dcMCH3000.CE_Command() Then
                Throw New Exception
            End If

            ' カードが引き抜かれるまで待機
            Dim blnErr2 = True
            For i = 0 To 1000
                states = _dcMCH3000.SR_Command
                If states(0) = 0 Then
                    blnErr2 = False
                    Exit For
                End If
            Next

            ' カード詰まり2
            If blnErr2 Then
                Throw New Exception
            End If

        Catch ex As Exception
            'MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Using frm As New frmMSGBOX01("カード発券に失敗しました。", 3)
                frm.ShowDialog()
            End Using
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
            If _dcMCH3000.IsOpen Then
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
            Me.lblA0.ForeColor = Color.Silver
            Me.lblA1.ForeColor = Color.Silver
            Me.lblA2.ForeColor = Color.Silver
            Me.lblA3.ForeColor = Color.Silver
            Me.lblNONE0.ForeColor = Color.Silver

            Me.lblB0.ForeColor = Color.Silver
            Me.lblB1.ForeColor = Color.Silver
            Me.lblB2.ForeColor = Color.Silver
            Me.lblB3.ForeColor = Color.Silver
            Me.lblNONE1.ForeColor = Color.Silver

            Me.lblC0.ForeColor = Color.Silver
            Me.lblNONE2.ForeColor = Color.Silver

            Me.lblD0.ForeColor = Color.Silver
            Me.lblD1.ForeColor = Color.Silver
            Me.lblD2.ForeColor = Color.Silver
            Me.lblD3.ForeColor = Color.Silver
            Me.lblNONE3.ForeColor = Color.Silver

            Dim error0 = False
            Dim error1 = False
            Dim error2 = False
            Dim error3 = False
            Dim near_end = False

            ' 接続エラー
            If Not _dcMCH3000.IsOpen Then
                _dcMCH3000.Close()
                btnTest.Enabled = False
                lblConnect.ForeColor = Color.Silver
                Exit Sub
            End If

            ' ユニットステータス
            Dim states = _dcMCH3000.SR_Command

            ' 状態
            Select Case states(0)
                Case 0
                    ' アイドル
                    error0 = False
                Case 1
                    ' 詰り
                    Me.lblA0.ForeColor = Color.Yellow
                    error0 = True
                Case 2
                    ' タンパー
                    Me.lblA1.ForeColor = Color.Yellow
                    error0 = True
                Case 3
                    ' 受け入れ可能
                    error0 = False
                Case 4
                    ' サイクル可能
                    error0 = False
                Case 5
                    ' 払い出し
                    Me.lblA3.ForeColor = Color.Yellow
                    error0 = True
                Case 6
                    ' 未定義
                Case 7
                    ' 送り出し
                    Me.lblA3.ForeColor = Color.Yellow
                    error0 = True
                Case 8
                    ' 取り込み
                    Me.lblA3.ForeColor = Color.Yellow
                    error0 = True
                Case 9
                    ' 後方排出
                    Me.lblA3.ForeColor = Color.Yellow
                    error0 = True
                Case 10
                    Me.lblA3.ForeColor = Color.Yellow
                    error0 = True
            End Select

            ' カードの位置
            Select Case states(1)
                Case 0
                    ' ユニット内にカード無し
                    error1 = False
                Case 1
                    ' コンタクト
                    Me.lblB0.ForeColor = Color.Yellow
                    error1 = True
                Case 2
                    ' 出入口
                    Me.lblB1.ForeColor = Color.Yellow
                    error1 = True
                Case 3
                    ' ユニット内
                    Me.lblB2.ForeColor = Color.Yellow
                    error1 = True
                Case 4
                    ' 後方挿入排出ユニット
                    Me.lblB3.ForeColor = Color.Yellow
                    error1 = True
            End Select

            ' カセットの状態
            Select Case states(2)
                Case 0
                    ' エンプティ
                    Me.lblC0.ForeColor = Color.Yellow
                    error2 = True
                Case 1
                    ' エンプティでない
                    error2 = False
            End Select

            ' シャッター
            Select Case states(3)
                Case 0
                    ' オープン
                    Me.lblA2.ForeColor = Color.Yellow
                    error0 = True
                Case 1
                    ' クローズ
            End Select

            ' カード検出センサー1
            Select Case states(5)
                Case 0
                    ' オープン
                    error3 = False
                Case 1
                    ' クローズ(遮光状態)
                    Me.lblD0.ForeColor = Color.Yellow
                    error3 = True
            End Select

            ' カード検出センサー2
            Select Case states(6)
                Case 0
                    ' オープン
                Case 1
                    ' クローズ(遮光状態)
                    Me.lblD1.ForeColor = Color.Yellow
                    error3 = True
            End Select

            ' カード検出センサー3
            Select Case states(7)
                Case 0
                    ' オープン
                Case 1
                    ' クローズ(遮光状態)
                    Me.lblD2.ForeColor = Color.Yellow
                    error3 = True
            End Select

            ' カード検出センサー4
            Select Case states(8)
                Case 0
                    ' オープン
                Case 1
                    ' クローズ(遮光状態)
                    Me.lblD3.ForeColor = Color.Yellow
                    error3 = True
            End Select

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
                _dcMCH3000.Close()
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