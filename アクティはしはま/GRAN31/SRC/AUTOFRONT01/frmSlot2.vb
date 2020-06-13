﻿Public Class frmSlot2

#Region "▼ 宣言部"

    Const DEBUG = False

    Dim _egara As Integer = 0 ' 判定結果
    Dim _reel1 As SlotPattern ' リールの絵柄1
    Dim _reel2 As SlotPattern ' リールの絵柄2
    Dim _reel3 As SlotPattern ' リールの絵柄3
    Dim _count As Integer = 0 ' リール回転数
    Dim _stopCount As Integer = 0 ' リールストップフラグ（1～3段階）

    Dim _forceMode As Boolean = False ' 抽選結果を外部から行い、内部的に抽選しない場合Trueにする。

#End Region

#Region "▼ プロパティ"

    Private _coin As Integer
    Public Property Coin As Integer
        Get
            Return _coin
        End Get
        Set(ByVal value As Integer)
            _coin = value
            Me.lblCoin.Text = value.ToString("N0")
        End Set
    End Property

    ' 本日の利用料金(チェックイン/入金 - チェックアウト時の差額で計算)
    Public Property GetPoint As Integer ' 入手ポイント

    ' 利用料金
    Public Property Sagaku As Integer

    ' チェックイン/チェックアウト用の結果格納クラス
    Public Property Result As CheckResult = New CheckResult

#End Region

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Public Sub New(ByVal egara As Integer)
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _egara = egara
        _forceMode = True

    End Sub

#Region "▼ イベント定義"

    ''' <summary>
    ''' フォーム_ロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSlot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If CommonSettings.DEBUG Then
                Me.Result.OUTPO = 10
            End If

            _reel1 = New SlotPattern(Me.picReel1) ' リールの絵柄1
            _reel2 = New SlotPattern(Me.picReel2) ' リールの絵柄2
            _reel3 = New SlotPattern(Me.picReel3) ' リールの絵柄3

            Me.Coin = 2

            Me.lblResult.Text = "ミニゲームスタート！"
            Me.lblResult.Visible = True

            Me.lblDebug.Visible = DEBUG

            Dim p4 = CommonSettings.SlotPoint4 ' + Me.Result.OUTPO
            Dim p3 = CommonSettings.SlotPoint3 ' + Me.Result.OUTPO
            Dim p2 = CommonSettings.SlotPoint2 ' + Me.Result.OUTPO
            Dim p1 = CommonSettings.SlotPoint1 ' + Me.Result.OUTPO

            Me.picReelM1.Image = Images.PicSLOT4
            Me.lblPoint1.Text = String.Format("+{0}pt", p4.ToString("N0"))
            Me.picReelM2.Image = Images.PicSLOT3
            Me.lblPoint2.Text = String.Format("+{0}pt", p3.ToString("N0"))
            Me.picReelM3.Image = Images.PicSLOT2
            Me.lblPoint3.Text = String.Format("+{0}pt", p2.ToString("N0"))
            Me.picReelM4.Image = Images.PicSLOT1
            Me.lblPoint4.Text = String.Format("+{0}pt", p1.ToString("N0"))

            ' はずれ
            Me.lblPoint0.Text = String.Format("{0}pt", Me.Result.OUTPO.ToString("N0"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSlot_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'If _egara.Equals(4) Then
            '    Sound.PlaySlotWin()
            'Else
            '    Sound.PlaySlotStop()
            'End If

            If DEBUG Then

                Dim cc = 100

                Dim list = CreateLotteryTable(Me.Sagaku)

                Dim aa = New List(Of Integer)()
                For i = 0 To cc - 1
                    Lottery(list)
                    aa.Add(_egara)

                    Application.DoEvents()
                    Threading.Thread.Sleep(1)
                Next

                Dim egara1 = 0
                Dim egara2 = 0
                Dim egara3 = 0
                Dim egara4 = 0
                Dim hazure = 0
                For Each a In aa
                    Select Case a
                        Case 1
                            egara1 += 1
                        Case 2
                            egara2 += 1
                        Case 3
                            egara3 += 1
                        Case 4
                            egara4 += 1
                        Case Else
                            hazure += 1
                    End Select
                Next
                Dim count = egara1 + egara2 + egara3 + egara4 + hazure

                Me.lblDebug.Text = String.Format("回転数:{5} リプレイ:{0} ベル:{1} スイカ:{2} 777:{3} はずれ:{4}", egara1, egara2, egara3, egara4, hazure, count)

            Else
                Me.picReel1.Visible = False
                Me.picReel2.Visible = False
                Me.picReel3.Visible = False
                'Me.lblResult.Visible = False
                Me.lblResult.Visible = True
                'Me.lblResult.Location = New Point(Me.lblResult.Location.X, 454)
                Me.lblResult.Text = "カードを書き込み中です。"

                'Me.timStart.Interval = 4000
                Me.timStart.Interval = 1
                Me.timStart.Start()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_スロット開始
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timStart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timStart.Tick
        Me.timStart.Stop()
        If Not _forceMode Then
            Lottery(CreateLotteryTable(Me.Sagaku))
        End If
        SlotStart()
    End Sub

    Dim _skip As Boolean = False

    Private Sub ShowResult()
        Dim point = Me.Result.OUTPO

        Me.timClose.Interval = CommonSettings.MinigameCloseInteravl

        Select Case _egara
            Case 0
                'lblResult.Text = String.Format("残念！はずれ。 +{0}ptゲット！", point)
                lblResult.Text = String.Format("{0}pt", point)
                'Sound.PlaySlotStop()
                Me.lblPoint0.ForeColor = Color.Yellow
                Me.timClose.Start()
            Case 1
                point += CommonSettings.SlotPoint1
                lblResult.Text = String.Format("やったね。{0}pt", point)
                Sound.PlaySlotWin()
                Me.lblPoint4.ForeColor = Color.Yellow
                Me.timClose.Start()
            Case 2
                point += CommonSettings.SlotPoint2
                lblResult.Text = String.Format("やったね。{0}pt", point)
                Sound.PlaySlotWin()
                Me.lblPoint3.ForeColor = Color.Yellow
                Me.timClose.Start()
            Case 3
                point += CommonSettings.SlotPoint3
                lblResult.Text = String.Format("やったね。{0}pt", point)
                Sound.PlaySlotWin()
                Me.lblPoint2.ForeColor = Color.Yellow
                Me.timClose.Start()
            Case 4
                point += CommonSettings.SlotPoint4
                lblResult.Text = String.Format("やったね。{0}pt", point)
                'Sound.PlaySlotWin2()
                Sound.PlaySlotWin()
                Me.lblPoint1.ForeColor = Color.Yellow
                Me.timClose.Start()
        End Select

        Me.GetPoint = point
        Me.btnExit.Image = Images.BtnExit
        Me.btnExit.Visible = True
        Me.lblResult.Visible = True
        Me.timBlinkText.Interval = 1000
        Me.timBlinkText.Start()
        _stopCount = 3

    End Sub

    ''' <summary>
    ''' タイマー_絵柄表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timSlot_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timSlot.Tick
        Try
            Dim max_count1 = 60
            Dim max_count2 = 85
            Dim max_count3 = 110

            If _count >= max_count3 Then
                '【3番目のリールストップ】
                Me.timSlot.Stop()

                Select Case _egara
                    Case 0
                        ReelAnimation(_stopCount)
                        ShowHazure()
                    Case 1
                        _reel3.Pattern = 1
                    Case 2
                        _reel3.Pattern = 2
                    Case 3
                        _reel3.Pattern = 3
                    Case 4
                        _reel3.Pattern = 4
                End Select

                ShowResult()

                Exit Sub

            ElseIf _count >= max_count2 Then
                '【2番目のリールストップ】

                ' 2番目は1番のﾊﾟﾀｰﾝと同じ
                _reel2.Pattern = _reel1.Pattern

                _stopCount = 2

            ElseIf _count >= max_count1 Then
                '【1番目のリールストップ】
                Select Case _egara
                    Case 1
                        _reel1.Pattern = 1
                    Case 2
                        _reel1.Pattern = 2
                    Case 3
                        _reel1.Pattern = 3
                    Case 4
                        _reel1.Pattern = 4
                End Select

                _stopCount = 1

            End If

            ReelAnimation(_stopCount)
            _count += 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_点滅
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timBlinkText_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBlinkText.Tick
        Me.lblResult.Visible = Not Me.lblResult.Visible
    End Sub

    ''' <summary>
    ''' タイマー_終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub timClose_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timClose.Tick
        Me.Close()
    End Sub

    ''' <summary>
    ''' ボタン_スキップ/終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'If _stopCount.Equals(0) Then Exit Sub

        If _stopCount < 3 Then
            'Sound.PlaySlotStop()
            Me.timSlot.Stop()
            Select Case _egara
                Case 0
                    ReelAnimation(_stopCount)
                    ShowHazure()
                Case 1
                    _reel1.Pattern = 1
                    _reel2.Pattern = 1
                    _reel3.Pattern = 1
                Case 2
                    _reel1.Pattern = 2
                    _reel2.Pattern = 2
                    _reel3.Pattern = 2
                Case 3
                    _reel1.Pattern = 3
                    _reel2.Pattern = 3
                    _reel3.Pattern = 3
                Case 4
                    _reel1.Pattern = 4
                    _reel2.Pattern = 4
                    _reel3.Pattern = 4
            End Select
            ShowResult()
        Else
            Me.Close()

        End If
    End Sub

    ''' <summary>
    ''' ボタン_リトライ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRetry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.timClose.Stop()
            Lottery(CreateLotteryTable(Me.Sagaku))
            SlotStart()
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' ボタン_入金
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


#End Region

#Region "▼ 関数定義"

    ''' <summary>
    ''' 抽選テーブルの作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Function CreateLotteryTable(Optional ByVal sagaku As Integer = 0) As List(Of Integer)
        Try
            If DEBUG Then
                'Me.Sagaku = 50 * 8
            End If
            Dim hosei1 = 0
            Dim hosei2 = 0
            Dim hosei3 = 0
            Dim hosei4 = 0
            If CommonSettings.SlotCorrectionEnabled Then
                hosei1 = CInt(sagaku * CommonSettings.SlotCorrectionRate1)
                hosei2 = CInt(sagaku * CommonSettings.SlotCorrectionRate2)
                hosei3 = CInt(sagaku * CommonSettings.SlotCorrectionRate3)
                hosei4 = CInt(sagaku * CommonSettings.SlotCorrectionRate4)
            End If

            Dim max_per = CommonSettings.SlotMaxPer
            Dim per1 = CommonSettings.SlotPer1 + hosei1
            Dim per2 = CommonSettings.SlotPer2 + hosei2
            Dim per3 = CommonSettings.SlotPer3 + hosei3
            Dim per4 = CommonSettings.SlotPer4 + hosei4

            Dim count1 = 0
            Dim count2 = 0
            Dim count3 = 0
            Dim count4 = 0
            Dim count0 = 0

            Dim list = New List(Of Integer)
            For i = 0 To per1 - 1
                list.Add(1)
                count1 += 1
            Next
            For i = 0 To per2 - 1
                list.Add(2)
                count2 += 1
            Next
            For i = 0 To per3 - 1
                list.Add(3)
                count3 += 1
            Next
            For i = 0 To per4 - 1
                list.Add(4)
                count4 += 1
            Next
            For i = 0 To max_per - list.Count()
                list.Add(0)
                count0 += 1
            Next

            If DEBUG Then
                MsgBox(String.Format("0:{0} 1:{1} 2:{2} 3:{3} 4:{4}", count0, count1, count2, count3, count4))
            End If

            Return list

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 抽選
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Lottery(ByVal list As List(Of Integer))
        Try
            Dim r As New System.Random
            Dim i As Integer = r.Next(0, list.Count() - 1)

            _egara = list(i)

            If DEBUG Then
                Dim hosei1 = 0
                Dim hosei2 = 0
                Dim hosei3 = 0
                Dim hosei4 = 0
                Dim per1 = CommonSettings.SlotPer1 + hosei1
                Dim per2 = CommonSettings.SlotPer2 + hosei2
                Dim per3 = CommonSettings.SlotPer3 + hosei3
                Dim per4 = CommonSettings.SlotPer4 + hosei4
                Me.lblDebug.Text = String.Format("当選:{0} 乱数:{1} {2} {3} {4} {5}", _egara, i, per1, per2, per3, per4)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 抽選後、抽選した当選役を取得する
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetLotteryValue(Optional ByVal sagaku As Integer = 0) As Integer
        Try
            Dim list = CreateLotteryTable(sagaku)

            Dim r As New System.Random
            Dim i As Integer = r.Next(0, list.Count() - 1)

            Return list(i)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 役に対するポイントを取得する
    ''' </summary>
    ''' <param name="egara"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPointValue(ByVal egara As Integer) As Integer

        Select Case egara
            Case 1
                Return CommonSettings.SlotPoint1
            Case 2
                Return CommonSettings.SlotPoint2
            Case 3
                Return CommonSettings.SlotPoint3
            Case 4
                Return CommonSettings.SlotPoint4
        End Select

        Return 0

    End Function

    ''' <summary>
    ''' 確率判定
    ''' </summary>
    ''' <param name="par"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RandRet(ByVal par As Integer) As Boolean
        Dim r As New System.Random()
        Dim i As Integer = r.Next(0, 100)
        If par >= i Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' スロット開始
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SlotStart()
        Try
            Me.timBlinkText.Stop()

            'Me.lblResult.Visible = False
            'Me.lblResult.Location = New Point(Me.lblResult.Location.X, 237)
            'Me.lblResult.Location = New Point(Me.lblResult.Location.X, 217)

            Me.picReel1.Visible = True
            Me.picReel2.Visible = True
            Me.picReel3.Visible = True

            Me.btnExit.Visible = False

            'Me.lblResult.Text = "絵柄がそろうとポイントをもらえるよ。"
            Me.lblResult.Text = "スロットスタート！！"
            _count = 0
            _stopCount = 0
            _reel1.Pattern = 1
            _reel2.Pattern = 1
            _reel3.Pattern = 1
            Me.Coin -= 1
            Sound.PlaySlotReel()

            ' 退場ポイントの注釈
            If Me.Result.OUTPO > 0 Then
                Me.lblComment.Text = String.Format("ハズレても+{0}ptの退場ボーナス！", Me.Result.OUTPO)
            Else
                Me.lblComment.Visible = False
            End If

            timSlot.Interval = 40
            timSlot.Start()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' リールアニメーション
    ''' </summary>
    ''' <param name="i"></param>
    ''' <remarks></remarks>
    Private Sub ReelAnimation(ByVal i As Integer)
        Try
            If i <= 0 Then
                Dim r As New System.Random()
                Dim v As Integer = r.Next(4) + 1
                _reel1.Pattern = v
            End If
            If i <= 1 Then
                Dim r As New System.Random()
                Dim v As Integer = r.Next(4) + 1
                _reel2.Pattern = v
            End If
            If i <= 2 Then
                Dim r As New System.Random()
                Dim v As Integer = r.Next(4) + 1
                _reel3.Pattern = v
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 3つ目のリールを必ず外れるように補正
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowHazure()
        Dim r As New System.Random()
        Dim i As Integer = r.Next(3)
        If _reel1.Pattern = 1 And _reel2.Pattern = 1 Then
            If i = 0 Then
                _reel3.Pattern = 2
            ElseIf i = 1 Then
                _reel3.Pattern = 3
            Else
                _reel3.Pattern = 4
            End If
        ElseIf _reel1.Pattern = 2 And _reel2.Pattern = 2 Then
            If i = 0 Then
                _reel3.Pattern = 1
            ElseIf i = 1 Then
                _reel3.Pattern = 3
            Else
                _reel3.Pattern = 4
            End If
        ElseIf _reel1.Pattern = 3 And _reel2.Pattern = 3 Then
            If i = 0 Then
                _reel3.Pattern = 1
            ElseIf i = 1 Then
                _reel3.Pattern = 2
            Else
                _reel3.Pattern = 4
            End If
        ElseIf _reel1.Pattern = 4 And _reel2.Pattern = 4 Then
            If i = 0 Then
                _reel3.Pattern = 1
            ElseIf i = 1 Then
                _reel3.Pattern = 2
            Else
                _reel3.Pattern = 3
            End If
        End If

    End Sub

#End Region

End Class
