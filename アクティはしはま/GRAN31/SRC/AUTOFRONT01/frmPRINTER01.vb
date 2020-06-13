Imports Techno.DeviceControls


Public Class frmPRINTER01

#Region "▼宣言部"
    ''' <summary>
    ''' レシートプリンタコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSK121 As New Techno.DeviceControls.SK121Control

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' レシートプリンター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property SK121 As Techno.DeviceControls.SK121Control
        Get
            Return _dcSK121
        End Get
        Set(value As Techno.DeviceControls.SK121Control)
            _dcSK121 = value
        End Set
    End Property
#End Region

#Region "▼イベント定義"

    Private Sub frmPRINTER_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テスト印刷ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTest_Click(sender As System.Object, e As System.EventArgs) Handles btnTest.Click
        Try
            ' 発行日
            _dcSK121.PrintDate = DateTime.Now

            ' 料金
            _dcSK121.ReceiptPrice = 99999

            ' 発行者
            Dim publisher = New Publisher
            publisher.Name = "アクティはしはま"
            publisher.Address = "愛媛県今治市内堀2丁目1番15号"
            publisher.PhoneNumber = "0898-41-9006"
            _dcSK121.Publisher = publisher

            ' 領収書番号
            _dcSK121.ReceiptNumber = "99-99999"

            If Not _dcSK121.PrintReceipt() Then
                Using frm As New frmMSGBOX01("テストプリントに失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

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
    Private Sub btnConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnConnect.Click
        Try
            _dcSK121.Close()

            _dcSK121.Open(UIUtility.SYSTEM.SK121COM)
            If _dcSK121.IsOpen Then
                Me.lblConnect.ForeColor = Color.Yellow

                'エラー確認
                CheckPrinter()
            Else
                '【接続エラー】
                Me.lblConnect.ForeColor = Color.Silver
                Using frm As New frmMSGBOX01("レシートプリンターと接続できませんでした。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 戻るボタン_Click
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

#End Region


#Region "▼関数定義"

    ''' <summary>
    ''' 画面初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            If _dcSK121.IsOpen Then
                '【プリンタ接続中】

                '接続中
                Me.lblConnect.ForeColor = Color.Yellow

                'プリンタ情報
                CheckPrinter()

                Me.btnTest.Enabled = True
            Else

                '【プリンタ未接続】

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
    Private Sub CheckPrinter()
        Try
            Me.lblOffLine.ForeColor = Color.Silver
            Me.lblNone0.ForeColor = Color.Silver

            Me.lblAutoCutterError.ForeColor = Color.Silver
            Me.lblAcError.ForeColor = Color.Silver
            Me.lblAutoReturnDisable.ForeColor = Color.Silver
            Me.lblNone1.ForeColor = Color.Silver

            Me.lblPaperLess.ForeColor = Color.Silver
            Me.lblNearEnd.ForeColor = Color.Silver
            Me.lblNone2.ForeColor = Color.Silver

            Me.lblPaperRemaining.ForeColor = Color.Silver
            Me.lblNone3.ForeColor = Color.Silver

            If _dcSK121.GetPrintStatus() Then
                '【エラーあり】

                'エラー区分１
                Select Case _dcSK121.Messages(0)
                    Case Techno.DeviceControls.SK121Control.ErrorType.OFFLINE
                        '【オフライン】
                        Me.lblOffLine.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.NONE
                        Me.lblNone0.ForeColor = Color.Yellow
                End Select
                'エラー区分２
                Select Case _dcSK121.Messages(1)
                    Case Techno.DeviceControls.SK121Control.ErrorType.AUTO_CUTTER_ERROR
                        '【オートカッターエラー】
                        Me.lblAutoCutterError.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.AC_ERROR
                        '【電圧エラー】
                        Me.lblAcError.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.AUTO_RETURN_DISABLE
                        '【自動復帰エラー】
                        Me.lblAutoReturnDisable.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.NONE
                        Me.lblNone1.ForeColor = Color.Yellow
                End Select
                'エラー区分３
                Select Case _dcSK121.Messages(2)
                    Case Techno.DeviceControls.SK121Control.ErrorType.PAPER_LESS
                        '【紙切れ】
                        Me.lblPaperLess.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.NEAR_END
                        '【ニアエンド】
                        Me.lblNearEnd.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.BOTH_ERROR
                        '【用紙検出器エラー】
                        Me.lblPaperSensorError.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.NONE
                        Me.lblNone2.ForeColor = Color.Yellow
                End Select
                'エラー区分４
                Select Case _dcSK121.Messages(3)
                    Case Techno.DeviceControls.SK121Control.ErrorType.PAPER_REMAINING
                        '【プレゼンタに用紙あり】
                        Me.lblPaperRemaining.ForeColor = Color.Yellow
                    Case Techno.DeviceControls.SK121Control.ErrorType.NONE
                        Me.lblNone3.ForeColor = Color.Yellow
                End Select
            Else
                Me.lblNone0.ForeColor = Color.Yellow
                Me.lblNone1.ForeColor = Color.Yellow
                Me.lblNone2.ForeColor = Color.Yellow
                Me.lblNone3.ForeColor = Color.Yellow
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region



End Class