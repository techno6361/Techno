Imports TECHNO.DataBase

Public Class frmCALMT01

#Region "▼宣言部"

    Private _lblCalCol(7) As Label      '曜日ラベル
    Private _lblCalRow(42) As Label     '日付ラベル

    Private _strRKNKB(42) As String     '料金体系区分保持
    Private _strChkRKNKB(42) As String  '料金体系に変更があったかどうかチェック
    Private _strDay(42) As String       '日付保管

    Private _intYear As Integer         '現在年
    Private _intMonth As Integer        '現在月
    Private _intDay As Integer          '現在日

    Private _dsRKNMTA As New DataSet    '料金体系マスタ情報保持
    Private _dsCALMTA As New DataSet    'カレンダーマスタ情報保持

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "営業カレンダー登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "営業カレンダー登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCALMT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            '画面初期設定
            Init()



            '料金体系マスタ情報取得
            GetRKNMTA()

            'カレンダーマスタ情報取得
            GetCALMTA()



            'カレンダー作成
            CreateCalendar()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCALMT01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            _dsRKNMTA.Dispose()
            _dsCALMTA.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カレンダー日付ラベル_Click
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub _lblCalRow_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            Dim lblCal = CType(sender, Label)

            _strRKNKB(CType(lblCal.Tag.ToString, Integer)) = (CType(_strRKNKB(CType(lblCal.Tag.ToString, Integer)), Integer) + 1).ToString

            Dim drRKNMTARow() As DataRow = _dsRKNMTA.Tables(0).Select("RKNKB=" & _strRKNKB(CType(lblCal.Tag.ToString, Integer)))

            If drRKNMTARow.Length.Equals(0) Then
                _strRKNKB(CType(lblCal.Tag.ToString, Integer)) = "1"

                drRKNMTARow = _dsRKNMTA.Tables(0).Select("RKNKB=" & _strRKNKB(CType(lblCal.Tag.ToString, Integer)))
            End If

            lblCal.Text = _strDay(CType(lblCal.Tag, Integer)) & vbCrLf & drRKNMTARow(0).Item("RKNNM").ToString()
            lblCal.BackColor = Color.FromArgb(CType(drRKNMTARow(0).Item("CLRR").ToString, Integer), CType(drRKNMTARow(0).Item("CLRG").ToString, Integer), CType(drRKNMTARow(0).Item("CLRB").ToString, Integer))


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 次月ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Dim intChkYear As Integer = 0
        Dim intChkMonth As Integer = 0


        Try
            For i As Integer = 0 To 41
                If Not _strRKNKB(i).Equals(_strChkRKNKB(i)) Then
                    Using frm As New frmMSGBOX01("変更内容を保存しなくてよろしいですか？", 1)
                        frm.ShowDialog()
                        If Not frm.Reply Then
                            Exit Sub
                        Else
                            Exit For
                        End If
                    End Using
                End If
            Next

            If _intMonth.Equals(12) Then
                _intMonth = 1
                _intYear += 1

                intChkMonth = _intMonth + 1
                intChkYear = _intYear
            Else
                _intMonth += 1
                intChkYear = _intYear
                intChkMonth = _intMonth + 1
                If intChkMonth.Equals(13) Then
                    intChkMonth = 1
                    intChkYear += 1
                End If
            End If


            '次月ボタンチェック
            Dim drChkCALDT() As DataRow = _dsCALMTA.Tables(0).Select("CALDT >='" & intChkYear & intChkMonth.ToString.PadLeft(2, "0"c) & "01" & "'")
            If drChkCALDT.Length.Equals(0) Then
                Me.btnNext.Enabled = False
            Else
                Me.btnNext.Enabled = True
            End If
            Me.btnBack.Enabled = True


            '現在年月セット
            Me.lblNenGetu.Text = _intYear.ToString & "年" & _intMonth.ToString.PadLeft(2, "0"c) & "月"

            'カレンダー情報セット
            SetCalendar()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 前月ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Dim intChkYear As Integer = 0
        Dim intChkMonth As Integer = 0

        Try

            For i As Integer = 0 To 41
                If Not _strRKNKB(i).Equals(_strChkRKNKB(i)) Then
                    Using frm As New frmMSGBOX01("変更内容を保存しなくてよろしいですか？", 1)
                        frm.ShowDialog()
                        If Not frm.Reply Then
                            Exit Sub
                        Else
                            Exit For
                        End If
                    End Using
                End If
            Next

            If _intMonth.Equals(1) Then
                _intMonth = 12
                _intYear -= 1

                intChkMonth = _intMonth - 1
                intChkYear = _intYear
            Else
                _intMonth -= 1
                intChkYear = _intYear
                intChkMonth = _intMonth - 1
                If intChkMonth.Equals(1) Then
                    intChkMonth = 12
                    intChkYear -= 1
                End If
            End If


            '前月ボタンチェック
            Dim drChkCALDT() As DataRow = _dsCALMTA.Tables(0).Select("CALDT <='" & intChkYear & intChkMonth.ToString.PadLeft(2, "0"c) & "31" & "'")
            If drChkCALDT.Length.Equals(0) Then
                Me.btnBack.Enabled = False
            Else
                Me.btnBack.Enabled = True
            End If
            Me.btnNext.Enabled = True


            '現在年月セット
            Me.lblNenGetu.Text = _intYear.ToString & "年" & _intMonth.ToString.PadLeft(2, "0"c) & "月"

            'カレンダー情報セット
            SetCalendar()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F11クリアボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func11()
        Try
            Using frm As New frmMSGBOX01("画面をクリアしてもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '画面初期化
            Init()

            '現在年月セット
            Me.lblNenGetu.Text = _intYear.ToString & "年" & _intMonth.ToString.PadLeft(2, "0"c) & "月"

            'カレンダー情報セット
            SetCalendar()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try
            Cursor = Cursors.WaitCursor

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            '*** トランザクション開始 ***'
            iDatabase.BeginTransaction()

            If Not UpdCALMTA() Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("カレンダーマスタの更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '*** コミット ***'
            iDatabase.Commit()



            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using



            'カレンダー情報再取得
            _dsCALMTA.Clear()
            GetCALMTA()



            For i As Integer = 0 To 41
                _strChkRKNKB(i) = _strRKNKB(i)
            Next


        Catch ex As Exception
            '*** ロールバック ***'
            iDatabase.RollBack()

            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "▼関数定義"
    ''' <summary>
    ''' 画面初期表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            '一覧
            Me.tspFunc3.Enabled = False
            '印刷
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = True

            Me.btnBack.Enabled = False
            Me.btnNext.Enabled = True

            '現在年
            _intYear = CType(DateTime.Now.Year.ToString, Integer)
            '現在月
            _intMonth = CType(DateTime.Now.Month.ToString, Integer)
            '現在日
            _intDay = CType(DateTime.Now.Day.ToString, Integer)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 料金体系マスタ情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRKNMTA() As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM RKNMTA")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            _dsRKNMTA.Tables.Clear()
            _dsRKNMTA.Tables.Add(resultDt)

            If _dsRKNMTA.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' カレンダーマスタ情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCALMTA() As Boolean
        Try

            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" A.*,B.RKNNM,B.CLRR,B.CLRG,B.CLRB")
            sql.Append(" FROM CALMTA A LEFT JOIN RKNMTA B ON A.RKNKB = B.RKNKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            _dsCALMTA.Tables.Clear()
            _dsCALMTA.Tables.Add(resultDt)

            If _dsCALMTA.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' カレンダーマスタ情報更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdCALMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim intI As Integer = 1
        '指定した年月の日数を取得
        Dim intMonthDay As Integer = DateTime.DaysInMonth(_intYear, _intMonth)
        Dim strHizuke As String = _intYear.ToString & "," & _intMonth.ToString.ToString.PadLeft(2, "0"c) & ",01"
        '曜日を0から数えるので-1する
        Dim intYoubi As Integer = Weekday(DateTime.Parse(strHizuke)) - 1

        Try

            UpdCALMTA = False

            If _intYear.Equals(CType(Now.Date.Year.ToString, Integer)) And _intMonth.Equals(CType(Now.Date.Month.ToString, Integer)) Then
                intI = _intDay
            End If

            For i As Integer = intI To intMonthDay

                If _strRKNKB(i + (intYoubi - 1)).Equals(String.Empty) Then
                    Continue For
                End If

                sql.Clear()
                sql.Append("UPDATE CALMTA SET")
                sql.Append(" RKNKB = " & _strRKNKB(i + (intYoubi - 1)))
                sql.Append(",UPDDTM = NOW()")
                sql.Append(" WHERE")
                sql.Append(" CALDT = '" & _intYear.ToString & _intMonth.ToString.PadLeft(2, "0"c) & i.ToString.PadLeft(2, "0"c) & "'")

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then Return False

            Next


            Return True


        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 曜日ラベル作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateCalendar()
        Dim intStaX As Integer = 380
        Dim intStaY As Integer = 320
        Dim intX As Integer = intStaX
        Dim intY As Integer = intStaY
        Dim strYoubi() As String = {"日", "月", "火", "水", "木", "金", "土"}

        Dim intCount As Integer = 0
        Dim intDayCount As Integer = 1
        Dim strHizuke As String = _intYear.ToString & "," & _intMonth.ToString.ToString.PadLeft(2, "0"c) & ",01"
        '曜日を0から数えるので-1する
        Dim intYoubi As Integer = Weekday(DateTime.Parse(strHizuke)) - 1
        '指定した年月の日数を取得
        Dim intMonthDay As Integer = DateTime.DaysInMonth(_intYear, _intMonth)
        Try

            '現在年月セット
            Me.lblNenGetu.Text = _intYear.ToString & "年" & _intMonth.ToString.PadLeft(2, "0"c) & "月"

            For i As Integer = 0 To 6
                _lblCalCol(i) = New Label
                _lblCalCol(i).TextAlign = ContentAlignment.MiddleCenter
                _lblCalCol(i).Font = New Font(_lblCalCol(0).Font.FontFamily.Name, Single.Parse("20"))
                _lblCalCol(i).Size = New System.Drawing.Size(New System.Drawing.Point(160, 35))
                _lblCalCol(i).Location = New System.Drawing.Point(intX, intY)
                _lblCalCol(i).BackColor = Color.LightSkyBlue
                '日曜日赤、土曜日青
                If i = 0 Then
                    _lblCalCol(i).ForeColor = Color.Red
                ElseIf i = 6 Then
                    _lblCalCol(i).ForeColor = Color.Blue
                Else
                    _lblCalCol(i).ForeColor = Color.Black
                End If
                _lblCalCol(i).Cursor = Cursors.Hand
                _lblCalCol(i).Text = strYoubi(i)
                _lblCalCol(i).TabStop = False

                Me.Controls.Add(_lblCalCol(i))

                intX += 161
            Next


            '// iは行 jは列

            intY = intStaY + 35
            For i As Integer = 0 To 5
                intX = intStaX
                For j As Integer = 0 To 6
                    '日付ラベル
                    _lblCalRow(intCount) = New Label
                    _lblCalRow(intCount).ForeColor = Color.White
                    _lblCalRow(intCount).TextAlign = ContentAlignment.MiddleCenter
                    _lblCalRow(intCount).Font = New Font(_lblCalRow(intCount).Font.FontFamily.Name, Single.Parse("18"))
                    _lblCalRow(intCount).Size = New System.Drawing.Size(New System.Drawing.Point(160, 80))
                    _lblCalRow(intCount).Cursor = Cursors.Hand
                    _lblCalRow(intCount).Tag = intCount.ToString
                    _lblCalRow(intCount).Location = New System.Drawing.Point(intX, intY)
                    _lblCalRow(intCount).Name = intCount.ToString
                    _lblCalRow(intCount).BackColor = Color.White

                    _strRKNKB(intCount) = String.Empty
                    _strChkRKNKB(intCount) = String.Empty

                    If ((j = intYoubi) And intDayCount = 1) Or ((intDayCount > 1) And (intDayCount <= intMonthDay)) Then
                        '// 日付内の処理 //'

                        '営業カレンダー情報取得

                        Dim strCALMTA = _intYear.ToString & _intMonth.ToString.PadLeft(2, "0"c) & intDayCount.ToString.PadLeft(2, "0"c)
                        Dim drCALMTARow() As DataRow = _dsCALMTA.Tables(0).Select("CALDT='" & strCALMTA & "'")

                        If drCALMTARow.Length.Equals(0) Then
                            _lblCalRow(intCount).Text = intDayCount.ToString
                        Else
                            'カレンダーマスタにデータあり
                            _lblCalRow(intCount).Text = intDayCount.ToString & vbCrLf & drCALMTARow(0).Item("RKNNM").ToString
                            _strDay(intCount) = intDayCount.ToString


                            _lblCalRow(intCount).BackColor = Color.FromArgb(CType(drCALMTARow(0).Item("CLRR").ToString, Integer), CType(drCALMTARow(0).Item("CLRG").ToString, Integer), CType(drCALMTARow(0).Item("CLRB").ToString, Integer))

                            _strRKNKB(intCount) = drCALMTARow(0).Item("RKNKB").ToString
                            _strChkRKNKB(intCount) = drCALMTARow(0).Item("RKNKB").ToString
                        End If

                        intDayCount += 1
                    Else
                        '---日付外の処理---'
                        _lblCalRow(intCount).Text = ""
                        _lblCalRow(intCount).BackColor = Color.Silver

                    End If

                    '今日の日付以降ならEnabledをFalseに
                    If (intDayCount - 1) < _intDay Then
                        _lblCalRow(intCount).Enabled = False
                    End If

                    If intDayCount <= (intMonthDay + 1) Then
                        AddHandler _lblCalRow(intCount).Click, AddressOf Me._lblCalRow_Click
                        If intDayCount = (intMonthDay + 1) Then
                            intDayCount += 1
                        End If
                    Else
                        _lblCalRow(intCount).Cursor = Cursors.Default
                    End If
                    Me.Controls.Add(_lblCalRow(intCount))

                    intX += 161
                    intCount += 1

                Next
                intY += 81
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カレンダー情報セット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCalendar()

        Dim intChkYear As Integer = 0
        Dim intChkMonth As Integer = 0

        '行数(0～41)
        Dim intCount As Integer = 0
        '日付
        Dim intDayCount As Integer = 1

        Try
            '曜日を0から数えるので-1する
            '月の初めが何曜日からか
            Dim intYoubi As Integer = Weekday(DateTime.Parse(_intYear.ToString & "," & _intMonth.ToString.ToString.PadLeft(2, "0"c) & ",01")) - 1
            '指定したXXXX年XX月の日数を取得
            Dim intMonthDay As Integer = DateTime.DaysInMonth(_intYear, _intMonth)

            '// iは行 jは列

            For i As Integer = 0 To 5
                For j As Integer = 0 To 6
                    '最初にイベント削除しておく
                    RemoveHandler _lblCalRow(intCount).Click, AddressOf Me._lblCalRow_Click
                    If ((j = intYoubi) And intDayCount = 1) Or ((intDayCount > 1) And (intDayCount <= intMonthDay)) Then
                        '// 日付内の処理 //'

                        '日付ラベル
                        _lblCalRow(intCount).Cursor = Cursors.Hand
                        _lblCalRow(intCount).Tag = intCount.ToString
                        _lblCalRow(intCount).Enabled = True

                        _strRKNKB(intCount) = String.Empty
                        _strChkRKNKB(intCount) = String.Empty


                        '営業カレンダー情報取得

                        Dim strCALMTA = _intYear.ToString & _intMonth.ToString.PadLeft(2, "0"c) & intDayCount.ToString.PadLeft(2, "0"c)
                        Dim drCALMTARow() As DataRow = _dsCALMTA.Tables(0).Select("CALDT='" & strCALMTA & "'")

                        If drCALMTARow.Length.Equals(0) Then
                            _lblCalRow(intCount).Text = intDayCount.ToString
                            _lblCalRow(intCount).Enabled = False
                            _lblCalRow(intCount).BackColor = Color.White
                        Else
                            'カレンダーマスタにデータあり
                            _lblCalRow(intCount).Text = intDayCount.ToString & vbCrLf & drCALMTARow(0).Item("RKNNM").ToString
                            _strDay(intCount) = intDayCount.ToString


                            _lblCalRow(intCount).BackColor = Color.FromArgb(CType(drCALMTARow(0).Item("CLRR").ToString, Integer), CType(drCALMTARow(0).Item("CLRG").ToString, Integer), CType(drCALMTARow(0).Item("CLRB").ToString, Integer))

                            _strRKNKB(intCount) = drCALMTARow(0).Item("RKNKB").ToString
                            _strChkRKNKB(intCount) = drCALMTARow(0).Item("RKNKB").ToString
                        End If

                        intDayCount += 1
                    Else
                        '---日付外の処理---'
                        _lblCalRow(intCount).Text = ""
                        _lblCalRow(intCount).BackColor = Color.Silver
                        _lblCalRow(intCount).Enabled = False
                    End If


                    If intDayCount <= (intMonthDay + 1) Then
                        AddHandler _lblCalRow(intCount).Click, AddressOf Me._lblCalRow_Click
                        If intDayCount = (intMonthDay + 1) Then
                            intDayCount += 1
                        End If
                    Else
                        _lblCalRow(intCount).Cursor = Cursors.Default
                    End If

                    intCount += 1

                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region


End Class
