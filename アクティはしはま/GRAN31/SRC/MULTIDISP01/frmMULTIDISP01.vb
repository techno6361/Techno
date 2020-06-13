Imports System.Configuration
Imports System.Threading

''' <summary>
''' 単独アプリバージョン
''' </summary>
''' <remarks></remarks>
Public Class frmMULTIDISP01

    Dim _DEBUG As Boolean = False

#Region "▼ コンストラクタ"

#End Region

#Region "▼ 定義"

    ' IPCクライアント
    Public _ipc As IpcClient

    ' 右から表示
    Dim _RightToLeft As Boolean

    ' 打席
    Dim _seats As List(Of CntSeat01) = New List(Of CntSeat01)

    ' 設定ファイル名
    Const CONFIG_FILE = "MULTIDISP01.config"

    ' テロップの設定
    Const INTERVAL = 0
    Const START_EMPTY_COUNT = 41
    Const END_EMPTY_COUNT = 0
    Const TELOP_INTERVAL = 400
    Const SEATS_INTERVAL = 1000
    Const AUTO_CLOSE = False
    Dim _interval As Integer = 0

    Enum Align
        TOP_LEFT
        TOP_MIDDLE
        TOP_RIGHT
        MIDDLE_LEFT
        MIDDLE_CENTER
        MIDDLE_RIGHT
        BUTTOM_LEFT
        BUTTOM_CENTER
        BUTTOM_RIGHT
    End Enum

#End Region

#Region "▼ イベント定義"

    Private Sub frmMULTIDISP01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ' IPCに接続
            _ipc = New IpcClient

            ' 表示位置の変更
            Me.Location = New Point(_ipc.SYSTEM.LOCATION_X, 0)

            ' 設定ファイルの読込
            Dim filemap As ExeConfigurationFileMap = New ExeConfigurationFileMap()
            filemap.ExeConfigFilename = System.IO.Directory.GetCurrentDirectory() & "\" & CONFIG_FILE
            Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None)

            ' 設定の読込
            Dim c_padding = config.AppSettings.Settings("SEAT_PADDING")
            Dim c_size = config.AppSettings.Settings("SEAT_SIZE")
            Dim c_right_to_left = config.AppSettings.Settings("SEAT_RIGHT_TO_LEFT")
            Dim c_align = config.AppSettings.Settings("SEAT_ALIGN")
            Dim c_vertical_align = config.AppSettings.Settings("SEAT_VERTICAL_ALIGN")
            Dim c_font = config.AppSettings.Settings("SEAT_FONT")
            Dim c_arc = config.AppSettings.Settings("SEAT_ARC")
            Dim c_seat_color = config.AppSettings.Settings("SEAT_COLOR")
            Dim c_left_seats = config.AppSettings.Settings("LEFT_SEATS")

            ' 背景画像のタイプを選択
            Dim c_bg As System.Configuration.KeyValueConfigurationElement
            If (_ipc.SYSTEM.MULTIDISP_TYPE = 0) Then
                c_bg = config.AppSettings.Settings("BACK_IMAGE")
            Else
                c_bg = config.AppSettings.Settings("BACK_IMAGE2")
            End If

            ' 背景画像の適応
            Me.BackgroundImageLayout = ImageLayout.Stretch
            Me.BackgroundImage = System.Drawing.Image.FromFile(c_bg.Value)

            For i = 1 To _ipc.SYSTEM.FLRSU

                Dim f = New FloorSettings

                ' SeatColor
                f.SeatColor = ColorTranslator.FromHtml(c_seat_color.Value)

                ' 個別設定の読込
                'Dim seats_number = config.AppSettings.Settings((i & "F_SEATS_NUMBER"))
                'If seats_number Is Nothing Then Exit Do

                ' 左右兼用席
                f.LeftSeats.Add(_ipc.SYSTEM.LRMULTI01)
                f.LeftSeats.Add(_ipc.SYSTEM.LRMULTI02)
                f.LeftSeats.Add(_ipc.SYSTEM.LRMULTI03)
                f.LeftSeats.Add(_ipc.SYSTEM.LRMULTI04)
                f.LeftSeats.Add(_ipc.SYSTEM.LRMULTI05)
                'Dim left_seats = c_left_seats.Value.Split(","c)
                'For Each seat In left_seats
                '    f.LeftSeats.Add(CInt(seat))
                'Next

                Dim margin = config.AppSettings.Settings((i & "F_SEAT_LOCATION"))
                Dim padding = config.AppSettings.Settings((i & "F_SEAT_PADDING"))
                Dim size = config.AppSettings.Settings((i & "F_SEAT_SIZE"))
                Dim right_to_left = config.AppSettings.Settings((i & "F_SEAT_RIGHT_TO_LEFT"))
                Dim align = config.AppSettings.Settings((i & "F_SEAT_ALIGN"))
                Dim font = config.AppSettings.Settings((i & "F_SEAT_FONT"))
                Dim arc = config.AppSettings.Settings((i & "F_SEAT_ARC"))

                ' 1F～3Fの打席数
                'If Not seats_number Is Nothing Then
                '    Dim params = seats_number.Value.Split(","c)
                '    f.StartSeatNumber = CInt(params(0))
                '    f.SeatNumber = CInt(params(1)) - CInt(params(0)) + 1
                'End If
                If i = 1 Then
                    ' 1F
                    f.StartSeatNumber = 1
                    f.SeatNumber = _ipc.SYSTEM.LSTNO1F
                ElseIf i = 2 Then
                    ' 2F
                    f.StartSeatNumber = _ipc.SYSTEM.LSTNO1F + 1
                    f.SeatNumber = _ipc.SYSTEM.LSTNO2F - _ipc.SYSTEM.LSTNO1F
                Else
                    ' 3F
                    f.StartSeatNumber = _ipc.SYSTEM.LSTNO2F + 1
                    f.SeatNumber = _ipc.SYSTEM.LSTNO3F - _ipc.SYSTEM.LSTNO2F - _ipc.SYSTEM.LSTNO1F
                End If

                If Not margin Is Nothing Then
                    Dim params = margin.Value.Split(","c)
                    f.Margin_X = CInt(params(0))
                    f.Margin_Y = CInt(params(1))
                End If

                If Not padding Is Nothing Then
                    f.Padding_X = CInt(padding.Value)
                Else
                    f.Padding_X = CInt(c_padding.Value)
                End If

                If Not size Is Nothing Then
                    Dim params = size.Value.Split(","c)
                    f.Width = CInt(params(0))
                    f.Height = CInt(params(1))
                Else
                    Dim params = c_size.Value.Split(","c)
                    f.Width = CInt(params(0))
                    f.Height = CInt(params(1))
                End If

                If Not right_to_left Is Nothing Then
                    Select Case CInt(right_to_left.Value)
                        Case 0
                            f.RightToLeft = False
                            _RightToLeft = False
                        Case 1
                            f.RightToLeft = True
                            _RightToLeft = True
                    End Select
                Else
                    Select Case CInt(c_right_to_left.Value)
                        Case 0
                            f.RightToLeft = False
                            _RightToLeft = False
                        Case 1
                            f.RightToLeft = True
                            _RightToLeft = True
                    End Select
                End If

                If Not align Is Nothing Then
                    Select Case CInt(align.Value)
                        Case 0
                            f.Align = FloorSettings.AlignType.LEFT
                        Case 1
                            f.Align = FloorSettings.AlignType.CENTER
                        Case 2
                            f.Align = FloorSettings.AlignType.RIGHT
                    End Select
                Else
                    Select Case CInt(c_align.Value)
                        Case 0
                            f.Align = FloorSettings.AlignType.LEFT
                        Case 1
                            f.Align = FloorSettings.AlignType.CENTER
                        Case 2
                            f.Align = FloorSettings.AlignType.RIGHT
                    End Select
                End If

                If Not font Is Nothing Then
                    Dim params = font.Value.Split(","c)
                    Dim style As FontStyle
                    Select Case CInt(params(2))
                        Case 0
                            style = FontStyle.Regular
                        Case 1
                            style = FontStyle.Bold
                        Case 2
                            style = FontStyle.Italic
                        Case 4
                            style = FontStyle.Underline
                        Case 8
                            style = FontStyle.Strikeout
                    End Select
                    f.Font = New Font(params(0), CInt(params(1)), style)
                Else
                    Dim params = c_font.Value.Split(","c)
                    Dim style As FontStyle
                    Select Case CInt(params(2))
                        Case 0
                            style = FontStyle.Regular
                        Case 1
                            style = FontStyle.Bold
                        Case 2
                            style = FontStyle.Italic
                        Case 4
                            style = FontStyle.Underline
                        Case 8
                            style = FontStyle.Strikeout
                    End Select
                    f.Font = New Font(params(0), CInt(params(1)), style)
                End If

                If Not arc Is Nothing Then
                    Dim params = arc.Value.Split(","c)
                    f.ArcRange = CInt(params(0))
                    If CInt(params(0)) = 0 Then f.ArcEnabled = True
                    f.ArcRate = CDbl(params(1))
                Else
                    Dim params = c_arc.Value.Split(","c)
                    f.ArcRange = CInt(params(0))
                    If CInt(params(0)) = 0 Then f.ArcEnabled = True
                    f.ArcRate = CDbl(params(1))
                End If

                ' 打席の描画
                DrawSeats(f, i)

            Next

            ' 電光掲示板のスクロール開始
            _interval = 0
            SetTelopStartPosition()
            'bwTelop.RunWorkerAsync()
            tmTelop.Interval = TELOP_INTERVAL
            tmTelop.Start()

            If Not _DEBUG Then
                ' 打席監視プロセスの開始
                'bwSeats.RunWorkerAsync()
                tmSeats.Interval = SEATS_INTERVAL
                tmSeats.Start()
            End If

            'bwMouse.RunWorkerAsync()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_MouseMove
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMULTIDISP02_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Try
            If Cursor.Position.X >= Me.Bounds.X Then
                Cursor.Clip = New Rectangle(0, 0, Me.Bounds.Width, Me.Bounds.Height)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 打席押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeat_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim obj = CType(sender, cntSeat01)
        MsgBox(obj.Text)
    End Sub

    ''' <summary>
    ''' 打席監視タイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmSeats_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmSeats.Tick
        Try
            Seats_Tick()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 電光掲示板の表示タイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmTelop_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmTelop.Tick
        Try
            Telop_Tick()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼ 関数定義"

    ''' <summary>
    ''' 打席を描画する
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Private Sub DrawSeats(ByVal s As FloorSettings, ByVal intFloor As Integer)
        Try
            Dim intStart = s.StartSeatNumber - 1
            Dim intEnd = intStart + s.SeatNumber

            ' シートの横幅、高さ
            Dim width = s.Width
            Dim height = s.Height

            If width < 0 Or height < 0 Then
                width = CInt((MyBase.Width - (s.Margin_X * 2)) / s.SeatNumber)
                height = CInt(width * s.AspectRate)
            End If

            Dim width_daseki = ((width * s.SeatNumber) + (s.Padding_X * (s.SeatNumber - 1)))
            Dim width_screen = MyBase.Width - (s.Margin_X * 2)

            'If width_daseki >= width_screen Then
            '    s.Padding_X = 0
            'End If

            ' シート移動値のオフセット値
            Dim ox = width + s.Padding_X + s.Span_X
            Dim oy = height + s.Padding_Y + s.Span_Y

            ' 水平のAlign
            Select Case s.Align
                Case FloorSettings.AlignType.LEFT
                    s.Margin_X = s.Padding_X
                Case FloorSettings.AlignType.CENTER
                    s.Margin_X = CInt(MyBase.Width / 2 - CInt(width_daseki / 2))
                Case FloorSettings.AlignType.RIGHT
                    s.Margin_X = MyBase.Width - s.Margin_X - width_daseki
                Case Else
                    '
            End Select

            ' 左揃え/右揃え
            Dim value = intStart + 1
            Dim addValue = 1
            If s.RightToLeft Then
                ox = -ox
                oy = -oy
                s.Margin_X += width_daseki - (width + s.Padding_X)
                'y = (height + s.Padding_Y) * (kaisu - 1) + s.Margin_Y
                'value = intEnd
                'addValue = -1
            End If

            Dim x = s.Margin_X
            Dim y = s.Margin_Y

            ' 垂直のAlign
            Select Case s.VerticalAlign
                Case FloorSettings.VerticalAlignType.TOP
                    y = s.Margin_Y
                Case FloorSettings.VerticalAlignType.MIDDLE
                    y += CInt((MyBase.Height / 2) - (s.Height * s.ArcRate ^ 2))
                Case FloorSettings.VerticalAlignType.BUTTOM
                    y += CInt(MyBase.Height - (s.Height * s.ArcRate ^ 2 * 1.8))
                Case Else
                    y = s.Margin_Y
            End Select

            Dim cRandom = New System.Random()

            For k = 0 To s.FloorNumber - 1

                Dim arc_v = 1
                x = s.Margin_X

                For i = 0 To s.SeatNumber - 1
                    Dim seat = New CntSeat01

                    seat.Text = value.ToString.PadLeft(s.TextLength, "0"c)
                    seat.Size = New System.Drawing.Size(New System.Drawing.Point(width, height))
                    seat.Font = s.Font
                    'seat.SeatColor = s.SeatColor
                    seat.LeftSeat = s.LeftSeats.Contains(value)
                    seat.Location = New System.Drawing.Point(x, y)

                    '' *** STA ADD 20180925 TERAYAMA 左右兼用打席タイプの追加
                    'If value = 42 Then
                    '    seat.LeftSeatType = CntSeat01.eLeftSeatType.LR
                    'Else
                    '    seat.LeftSeatType = CntSeat01.eLeftSeatType.L
                    'End If
                    ' *** END ADD 20180925 TERAYAMA 左右兼用打席タイプの追加

                    '' *** STA ADD 20181001 TERAYAMA スクール専用打席の追加
                    'Me.picIcon1.Location = New Point(376, 135)
                    'Me.picIcon1.Visible = True
                    'If value >= 37 And value <= 42 Then
                    '    seat.SchoolSeat = True
                    'End If
                    '' *** END ADD 20180925 TERAYAMA スクール専用打席の追加

                    ' テスト用
                    If _DEBUG Then
                        Dim result = cRandom.Next(70)
                        seat.Lamp = CntSeat01.LampType.BLANK
                        If result <= 35 Then
                            seat.Lamp = CntSeat01.LampType.USED
                        End If
                        If result = 2 Then
                            seat.Lamp = CntSeat01.LampType.CALLED
                        End If
                    End If

                    If s.ArcRange > 0 And s.ArcRate > 0 Then
                        ' 円弧の座標計算
                        If i < s.ArcRange Then
                            y += CInt(s.ArcRate * (s.ArcRange - arc_v))
                            arc_v += 1
                        ElseIf i > s.SeatNumber - s.ArcRange - 1 Then
                            y -= CInt(s.ArcRate * arc_v)
                            arc_v += 1
                        Else
                            arc_v = 1
                        End If
                    End If

                    Me.Controls.Add(seat)
                    AddHandler seat.OnButtonClick, AddressOf btnSeat_Click

                    _seats.Add(seat)

                    ' 左右兼用打席ならもう一度追加
                    If seat.LeftSeat Then
                        _seats.Add(seat)
                    End If

                    x += ox
                    value += addValue

                Next
                y += oy
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' シートの状態を調べる
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeatState(ByVal i As Integer) As CntSeat01.LampType
        Try
            Dim row = _ipc.SYSTEM.SEATINFO.Rows(i)

            ' SEATINFOの参照
            Select Case CType(row.Item("SEATSTATE").ToString, Integer)
                Case UIUtility.SEATSTATE.FREE_SPACE
                    '【空き打席】
                    ' 予約席
                    If row.Item("RSVKBN").ToString.Equals("1") Or row.Item("RSVKBN").ToString.Equals("2") Then
                        Return CntSeat01.LampType.USED
                    End If
                    Return CntSeat01.LampType.BLANK
                Case UIUtility.SEATSTATE.USE_SPACE
                    '【使用中】
                    Return CntSeat01.LampType.USED
                Case UIUtility.SEATSTATE.CALL_SPACE
                    '【呼び出し中】                        
                    Return CntSeat01.LampType.CALLED
                Case UIUtility.SEATSTATE.ERR_SPACE
                    '【通信不能】
                    Return CntSeat01.LampType.USED
                Case UIUtility.SEATSTATE.ERR_RW
                    '【RW異常】                        
                    Return CntSeat01.LampType.USED
                Case UIUtility.SEATSTATE.PRICE_CHANGE
                    '【単価切り替え完了】
                    Return CntSeat01.LampType.BLANK
                Case UIUtility.SEATSTATE.WAIT_SPACE
                    '【待機中】
                    Return CntSeat01.LampType.USED
                Case Else
                    '【その他】
                    Return CntSeat01.LampType.USED
            End Select
        Catch ex As Exception
            'エラーでも無視
            '【その他】
            Return CntSeat01.LampType.USED
        End Try
    End Function

    ''' <summary>
    ''' ランプを変更
    ''' </summary>
    ''' <param name="seat"></param>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub ChangeSeatLamp(ByRef seat As CntSeat01, ByVal state As CntSeat01.LampType)
        Try
            Select Case state
                Case CntSeat01.LampType.USED
                    If Not seat.Lamp = CntSeat01.LampType.USED Then
                        seat.Lamp = CntSeat01.LampType.USED
                    End If
                Case CntSeat01.LampType.BLANK
                    If Not seat.Lamp = CntSeat01.LampType.BLANK Then
                        seat.Lamp = CntSeat01.LampType.BLANK
                    End If
                Case CntSeat01.LampType.CALLED
                    If Not seat.Lamp = CntSeat01.LampType.CALLED Then
                        seat.Lamp = CntSeat01.LampType.CALLED
                    End If
                Case Else
                    If Not seat.Lamp = CntSeat01.LampType.DISABLED Then
                        seat.Lamp = CntSeat01.LampType.DISABLED
                    End If
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 電光掲示板のメッセージの位置設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTelopStartPosition()
        Try
            ' 全角文字列に変換
            Dim telop = StrConv(_ipc.SYSTEM.TELOP, VbStrConv.Wide)

            ' 文字列が何もなければテロップパネルの非表示
            Me.pnlTELOP.Visible = Not String.IsNullOrEmpty(telop)

            lblTelop.Text = telop

            For i = 0 To START_EMPTY_COUNT
                lblTelop.Text = "　" & lblTelop.Text
            Next
            For i = 0 To END_EMPTY_COUNT
                lblTelop.Text = lblTelop.Text & "　"
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 打席監視のタイマー処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Seats_Tick()
        Try
            ' 更新チェック
            If _ipc.SYSTEM.UPDATED Then
                _ipc.SYSTEM.UPDATED = False
            Else
                Return
            End If

            For i = 0 To _seats.Count - 1
                Dim seat = CType(_seats(i), CntSeat01)
                If seat.LeftSeat Then
                    ' 左右兼用打席
                    Dim state1 = GetSeatState(i)
                    Dim state2 = GetSeatState(i + 1)
                    ChangeSeatLamp(seat, state1)
                    If Not state2 = CntSeat01.LampType.BLANK Then
                        ChangeSeatLamp(seat, state2)
                    End If
                    If state1 = CntSeat01.LampType.CALLED Or state2 = CntSeat01.LampType.CALLED Then
                        seat.Lamp = CntSeat01.LampType.CALLED
                    End If
                    i += 1
                Else
                    ' 通常打席
                    Dim state = GetSeatState(i)

                    ChangeSeatLamp(seat, state)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' テロップのタイマー処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Telop_Tick()
        Try
            If lblTelop.Text.Length > 0 Then
                lblTelop.Text = lblTelop.Text.Substring(1)
            Else
                _interval += 1
                If _interval >= INTERVAL Then
                    _interval = 0
                    SetTelopStartPosition()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼ 並列処理"

    Private Sub bwTelop_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTelop.DoWork
        Try
            Do
                Thread.Sleep(TELOP_INTERVAL)
                bwTelop.ReportProgress(0)
            Loop
        Catch ex As Exception
            MessageBox.Show("worker1")
            Throw ex
        End Try
    End Sub

    Private Sub bwTelop_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTelop.ProgressChanged
        Try
            Telop_Tick()
        Catch ex As Exception
            MessageBox.Show("worker2")
            Throw ex
        End Try
    End Sub

    Private Sub bwSeats_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwSeats.DoWork
        Try
            Do
                Thread.Sleep(SEATS_INTERVAL)
                bwSeats.ReportProgress(0)
            Loop
        Catch ex As Exception
            MessageBox.Show("worker3")
            Throw ex
        End Try
    End Sub

    Private Sub bwSeats_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwSeats.ProgressChanged
        Try
            Seats_Tick()
        Catch ex As Exception
            MessageBox.Show("worker4")
            Throw ex
        End Try
    End Sub

    Private Sub bwMouse_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMouse.DoWork
        Try
            Do
                Thread.Sleep(10)
                bwMouse.ReportProgress(0)
            Loop
        Catch ex As Exception
            MessageBox.Show("worker5")
            Throw ex
        End Try
    End Sub

    Private Sub bwMouse_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMouse.ProgressChanged
        Try
            If Cursor.Position.X >= Me.Bounds.X Then
                Cursor.Position = New Point(Me.Bounds.X - 1, Cursor.Position.Y)
            End If

        Catch ex As Exception
            MessageBox.Show("worker6")
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub lblTelop_Click(sender As System.Object, e As System.EventArgs) Handles lblTelop.Click

    End Sub
End Class
