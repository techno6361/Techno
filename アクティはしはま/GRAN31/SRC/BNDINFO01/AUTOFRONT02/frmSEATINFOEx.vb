Imports Techno.DataBase

Public Class frmSEATINFOEx

#Region "▼宣言部"

    ''' <summary>
    ''' 打席開始位置【0】左から【1】右から
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _cintSeatPosition As Integer = 0
    ''' <summary>
    ''' 選択拡大ボタン情報【0】左【1】中央【右】
    ''' </summary>
    ''' <remarks></remarks>
    Private _intSelectZoom As Integer = 0
    
#End Region

#Region "▼ コンストラクタ"

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        ' すべてのコントロールにダブルバッファリングを有効化
        For Each c As Control In GetAllControls(Me)
            EnableDoubleBuffering(c)
        Next

    End Sub

#End Region

#Region "▼プロパティ"

    Public WriteOnly Property iDatabase As IDatabase.IMethod
        Set(ByVal value As IDatabase.IMethod)
            _iDatabase = value
        End Set
    End Property
    Private _iDatabase As IDatabase.IMethod

    ''' <summary>
    ''' 選択打席
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SELECTSEAT As Integer
        Get
            Return _intSELECTSEAT
        End Get
    End Property
    Private _intSELECTSEAT As Integer = 0
    ''' <summary>
    ''' キャンセルされたかどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CANCEL As Boolean
        Get
            Return _blnCANCEL
        End Get
    End Property
    Private _blnCANCEL As Boolean = False

#End Region


#Region "▼関数定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATINFO01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()


            '打席情報取得
            If Not SetSeatInfo() Then
                Using frm As New frmMSGBOXEx02("打席情報を取得できませんでした。", 3)
                    frm.ShowDialog()
                End Using
                _blnCANCEL = True
                Me.Close()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATINFO01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Sound.PlayTouchSeat()

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
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            _blnCANCEL = True
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 打席ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim btnSeat As Button
            btnSeat = CType(sender, Button)

            '【打席指定】
            If (Not CType(btnSeat.Tag, Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE)) Then
                Using frm As New frmMSGBOXEx02("空き打席を選択して下さい。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOXEx02(btnSeat.Text & "番打席でよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            If Not UpdSEATRSV(CType(btnSeat.Text, Integer)) Then
                Using frm As New frmMSGBOXEx02("空き打席を選択して下さい。", 2)
                    frm.ShowDialog()
                End Using
                '打席情報取得
                If Not SetSeatInfo() Then
                    Using frm As New frmMSGBOXEx02("打席情報を取得できませんでした。", 3)
                        frm.ShowDialog()
                    End Using
                    _blnCANCEL = True
                    Me.Close()
                End If
                Exit Sub
            End If

            _intSELECTSEAT = CType(btnSeat.Text, Integer)

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 拡大ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim intSta1F As Integer = 0
        Dim intSta2F As Integer = 0
        Try
            Dim btnZoom As Button
            btnZoom = CType(sender, Button)

            _intSelectZoom = CType(btnZoom.Tag, Integer)
            Select Case _intSelectZoom
                Case 0
                    If _cintSeatPosition.Equals(0) Then
                        '【左から】
                        intSta1F = 1
                        intSta2F = UIUtility.SYSTEM.LSTNO1F + 1
                    Else
                        '【右から】
                        intSta1F = UIUtility.SYSTEM.LSTNO1F
                        intSta2F = UIUtility.SYSTEM.LSTNO2F
                    End If
                Case 1
                    If _cintSeatPosition.Equals(0) Then
                        '【左から】
                        intSta1F = CType(UIUtility.SYSTEM.LSTNO1F / 2, Integer) - 8
                        intSta2F = CType((UIUtility.SYSTEM.LSTNO2F - (UIUtility.SYSTEM.LSTNO1F + 1) / 2), Integer) - 8
                    Else
                        '【右から】
                        intSta1F = CType(UIUtility.SYSTEM.LSTNO1F / 2, Integer) + 8
                        intSta2F = CType((UIUtility.SYSTEM.LSTNO2F - (UIUtility.SYSTEM.LSTNO1F + 1) / 2), Integer) + 8
                    End If
                Case 2
                    If _cintSeatPosition.Equals(0) Then
                        '【左から】
                        intSta1F = UIUtility.SYSTEM.LSTNO1F - 16
                        intSta2F = UIUtility.SYSTEM.LSTNO2F - 16
                    Else
                        '【右から】
                        intSta1F = 17
                        intSta2F = UIUtility.SYSTEM.LSTNO1F + 17
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' 画面初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try
            '空き打席ラベル
            Me.lblBLANK.BackColor = UIUtility.COLOR_INFO.FREE

            '使用中ラベル
            Me.lblUSED.BackColor = UIUtility.COLOR_INFO.USE_TAMA

            'イベントハンドル
            '【打席指定あり】
            AddHandler btnSeat001.Click, AddressOf btnSeat_Click
            AddHandler btnSeat002.Click, AddressOf btnSeat_Click
            AddHandler btnSeat003.Click, AddressOf btnSeat_Click
            AddHandler btnSeat004.Click, AddressOf btnSeat_Click
            AddHandler btnSeat005.Click, AddressOf btnSeat_Click
            AddHandler btnSeat006.Click, AddressOf btnSeat_Click
            AddHandler btnSeat007.Click, AddressOf btnSeat_Click
            AddHandler btnSeat008.Click, AddressOf btnSeat_Click
            AddHandler btnSeat009.Click, AddressOf btnSeat_Click
            AddHandler btnSeat010.Click, AddressOf btnSeat_Click
            AddHandler btnSeat011.Click, AddressOf btnSeat_Click
            AddHandler btnSeat012.Click, AddressOf btnSeat_Click
            AddHandler btnSeat013.Click, AddressOf btnSeat_Click
            AddHandler btnSeat014.Click, AddressOf btnSeat_Click
            AddHandler btnSeat015.Click, AddressOf btnSeat_Click
            AddHandler btnSeat016.Click, AddressOf btnSeat_Click
            AddHandler btnSeat017.Click, AddressOf btnSeat_Click
            AddHandler btnSeat018.Click, AddressOf btnSeat_Click
            AddHandler btnSeat019.Click, AddressOf btnSeat_Click
            AddHandler btnSeat020.Click, AddressOf btnSeat_Click
            AddHandler btnSeat021.Click, AddressOf btnSeat_Click
            AddHandler btnSeat022.Click, AddressOf btnSeat_Click
            AddHandler btnSeat023.Click, AddressOf btnSeat_Click
            AddHandler btnSeat024.Click, AddressOf btnSeat_Click
            AddHandler btnSeat025.Click, AddressOf btnSeat_Click
            AddHandler btnSeat026.Click, AddressOf btnSeat_Click
            AddHandler btnSeat027.Click, AddressOf btnSeat_Click
            AddHandler btnSeat028.Click, AddressOf btnSeat_Click
            AddHandler btnSeat029.Click, AddressOf btnSeat_Click
            AddHandler btnSeat030.Click, AddressOf btnSeat_Click
            AddHandler btnSeat031.Click, AddressOf btnSeat_Click
            AddHandler btnSeat032.Click, AddressOf btnSeat_Click
            AddHandler btnSeat033.Click, AddressOf btnSeat_Click
            AddHandler btnSeat034.Click, AddressOf btnSeat_Click
            AddHandler btnSeat035.Click, AddressOf btnSeat_Click
            AddHandler btnSeat036.Click, AddressOf btnSeat_Click
            AddHandler btnSeat037.Click, AddressOf btnSeat_Click
            AddHandler btnSeat038.Click, AddressOf btnSeat_Click
            AddHandler btnSeat039.Click, AddressOf btnSeat_Click
            AddHandler btnSeat040.Click, AddressOf btnSeat_Click
            AddHandler btnSeat041.Click, AddressOf btnSeat_Click
            AddHandler btnSeat042.Click, AddressOf btnSeat_Click
            AddHandler btnSeat043.Click, AddressOf btnSeat_Click
            AddHandler btnSeat044.Click, AddressOf btnSeat_Click
            AddHandler btnSeat045.Click, AddressOf btnSeat_Click
            AddHandler btnSeat046.Click, AddressOf btnSeat_Click
            AddHandler btnSeat047.Click, AddressOf btnSeat_Click
            AddHandler btnSeat048.Click, AddressOf btnSeat_Click
            AddHandler btnSeat049.Click, AddressOf btnSeat_Click
            AddHandler btnSeat050.Click, AddressOf btnSeat_Click
            AddHandler btnSeat051.Click, AddressOf btnSeat_Click
            AddHandler btnSeat052.Click, AddressOf btnSeat_Click
            AddHandler btnSeat053.Click, AddressOf btnSeat_Click
            AddHandler btnSeat054.Click, AddressOf btnSeat_Click
            AddHandler btnSeat055.Click, AddressOf btnSeat_Click
            AddHandler btnSeat056.Click, AddressOf btnSeat_Click
            AddHandler btnSeat057.Click, AddressOf btnSeat_Click
            AddHandler btnSeat058.Click, AddressOf btnSeat_Click
            AddHandler btnSeat059.Click, AddressOf btnSeat_Click
            AddHandler btnSeat060.Click, AddressOf btnSeat_Click
    


        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    ''' <summary>
    ''' 打席情報ボタンセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSeatButton(ByVal intSeatNo As Integer, ByVal intSEATSTATE As Integer, ByVal clrBack As Color)
        Dim drRight As DataRow()
        Dim drLeft As DataRow()
        Try
            If UIUtility.SYSTEM.LRMULTI01.Equals(intSeatNo) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSeatNo) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSeatNo) _
                                                            Or UIUtility.SYSTEM.LRMULTI04.Equals(intSeatNo) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSeatNo) Then
                '【左右兼用打席】
                drRight = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSeatNo & " AND LEFTKB = 0")
                drLeft = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSeatNo & " AND LEFTKB = 1")
                If drRight.Length.Equals(0) Or drLeft.Length.Equals(0) Then Exit Sub
                If CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.CALL_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.CALL_SPACE) Then
                    '【呼び出し中】
                    intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                    clrBack = UIUtility.COLOR_INFO.CALL_COM
                ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_SPACE) Then
                    '【通信不能】
                    intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                    clrBack = UIUtility.COLOR_INFO.ERR_COM
                ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_RW) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_RW) Then
                    '【RW異常】
                    intSEATSTATE = UIUtility.SEATSTATE.ERR_RW
                    clrBack = UIUtility.COLOR_INFO.ERR_RW
                ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Then
                    '【使用中】
                    intSEATSTATE = UIUtility.SEATSTATE.USE_SPACE
                    If CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Then
                        If CType(drRight(0).Item("JKNKB").ToString(), Integer).Equals(0) Then
                            '【1球貸し】
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        Else
                            '【打ち放題】
                            clrBack = UIUtility.COLOR_INFO.USE_TIME
                        End If
                    End If
                    If CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Then
                        If CType(drLeft(0).Item("JKNKB").ToString(), Integer).Equals(0) Then
                            '【1球貸し】
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        Else
                            '【打ち放題】
                            clrBack = UIUtility.COLOR_INFO.USE_TIME
                        End If
                    End If
                ElseIf (CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE) And CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE)) _
                        Or (CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.PRICE_CHANGE) And CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.PRICE_CHANGE)) Then
                    '【空き打席】【単価切り替え完了】
                    intSEATSTATE = UIUtility.SEATSTATE.FREE_SPACE
                    clrBack = UIUtility.COLOR_INFO.FREE
                    '予約打席
                    If drLeft(0).Item("RSVKBN").ToString.Equals("1") Or drRight(0).Item("RSVKBN").ToString.Equals("1") Then
                        clrBack = UIUtility.COLOR_INFO.RESERV
                    End If
                    '清掃待ち打席
                    If drLeft(0).Item("RSVKBN").ToString.Equals("2") Or drRight(0).Item("RSVKBN").ToString.Equals("2") Then
                        '予約の色にしておく
                        clrBack = UIUtility.COLOR_INFO.RESERV
                    End If
                Else
                    intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                    clrBack = UIUtility.COLOR_INFO.ERR_COM
                End If

            End If

            Select Case intSeatNo
                Case 1 : Me.btnSeat001.BackColor = clrBack : Me.btnSeat001.Tag = intSEATSTATE
                Case 2 : Me.btnSeat002.BackColor = clrBack : Me.btnSeat002.Tag = intSEATSTATE
                Case 3 : Me.btnSeat003.BackColor = clrBack : Me.btnSeat003.Tag = intSEATSTATE
                Case 4 : Me.btnSeat004.BackColor = clrBack : Me.btnSeat004.Tag = intSEATSTATE
                Case 5 : Me.btnSeat005.BackColor = clrBack : Me.btnSeat005.Tag = intSEATSTATE
                Case 6 : Me.btnSeat006.BackColor = clrBack : Me.btnSeat006.Tag = intSEATSTATE
                Case 7 : Me.btnSeat007.BackColor = clrBack : Me.btnSeat007.Tag = intSEATSTATE
                Case 8 : Me.btnSeat008.BackColor = clrBack : Me.btnSeat008.Tag = intSEATSTATE
                Case 9 : Me.btnSeat009.BackColor = clrBack : Me.btnSeat009.Tag = intSEATSTATE
                Case 10 : Me.btnSeat010.BackColor = clrBack : Me.btnSeat010.Tag = intSEATSTATE
                Case 11 : Me.btnSeat011.BackColor = clrBack : Me.btnSeat011.Tag = intSEATSTATE
                Case 12 : Me.btnSeat012.BackColor = clrBack : Me.btnSeat012.Tag = intSEATSTATE
                Case 13 : Me.btnSeat013.BackColor = clrBack : Me.btnSeat013.Tag = intSEATSTATE
                Case 14 : Me.btnSeat014.BackColor = clrBack : Me.btnSeat014.Tag = intSEATSTATE
                Case 15 : Me.btnSeat015.BackColor = clrBack : Me.btnSeat015.Tag = intSEATSTATE
                Case 16 : Me.btnSeat016.BackColor = clrBack : Me.btnSeat016.Tag = intSEATSTATE
                Case 17 : Me.btnSeat017.BackColor = clrBack : Me.btnSeat017.Tag = intSEATSTATE
                Case 18 : Me.btnSeat018.BackColor = clrBack : Me.btnSeat018.Tag = intSEATSTATE
                Case 19 : Me.btnSeat019.BackColor = clrBack : Me.btnSeat019.Tag = intSEATSTATE
                Case 20 : Me.btnSeat020.BackColor = clrBack : Me.btnSeat020.Tag = intSEATSTATE
                Case 21 : Me.btnSeat021.BackColor = clrBack : Me.btnSeat021.Tag = intSEATSTATE
                Case 22 : Me.btnSeat022.BackColor = clrBack : Me.btnSeat022.Tag = intSEATSTATE
                Case 23 : Me.btnSeat023.BackColor = clrBack : Me.btnSeat023.Tag = intSEATSTATE
                Case 24 : Me.btnSeat024.BackColor = clrBack : Me.btnSeat024.Tag = intSEATSTATE
                Case 25 : Me.btnSeat025.BackColor = clrBack : Me.btnSeat025.Tag = intSEATSTATE
                Case 26 : Me.btnSeat026.BackColor = clrBack : Me.btnSeat026.Tag = intSEATSTATE
                Case 27 : Me.btnSeat027.BackColor = clrBack : Me.btnSeat027.Tag = intSEATSTATE
                Case 28 : Me.btnSeat028.BackColor = clrBack : Me.btnSeat028.Tag = intSEATSTATE
                Case 29 : Me.btnSeat029.BackColor = clrBack : Me.btnSeat029.Tag = intSEATSTATE
                Case 30 : Me.btnSeat030.BackColor = clrBack : Me.btnSeat030.Tag = intSEATSTATE
                Case 31 : Me.btnSeat031.BackColor = clrBack : Me.btnSeat031.Tag = intSEATSTATE
                Case 32 : Me.btnSeat032.BackColor = clrBack : Me.btnSeat032.Tag = intSEATSTATE
                Case 33 : Me.btnSeat033.BackColor = clrBack : Me.btnSeat033.Tag = intSEATSTATE
                Case 34 : Me.btnSeat034.BackColor = clrBack : Me.btnSeat034.Tag = intSEATSTATE
                Case 35 : Me.btnSeat035.BackColor = clrBack : Me.btnSeat035.Tag = intSEATSTATE
                Case 36 : Me.btnSeat036.BackColor = clrBack : Me.btnSeat036.Tag = intSEATSTATE
                Case 37 : Me.btnSeat037.BackColor = clrBack : Me.btnSeat037.Tag = intSEATSTATE
                Case 38 : Me.btnSeat038.BackColor = clrBack : Me.btnSeat038.Tag = intSEATSTATE
                Case 39 : Me.btnSeat039.BackColor = clrBack : Me.btnSeat039.Tag = intSEATSTATE
                Case 40 : Me.btnSeat040.BackColor = clrBack : Me.btnSeat040.Tag = intSEATSTATE
                Case 41 : Me.btnSeat041.BackColor = clrBack : Me.btnSeat041.Tag = intSEATSTATE
                Case 42 : Me.btnSeat042.BackColor = clrBack : Me.btnSeat042.Tag = intSEATSTATE
                Case 43 : Me.btnSeat043.BackColor = clrBack : Me.btnSeat043.Tag = intSEATSTATE
                Case 44 : Me.btnSeat044.BackColor = clrBack : Me.btnSeat044.Tag = intSEATSTATE
                Case 45 : Me.btnSeat045.BackColor = clrBack : Me.btnSeat045.Tag = intSEATSTATE
                Case 46 : Me.btnSeat046.BackColor = clrBack : Me.btnSeat046.Tag = intSEATSTATE
                Case 47 : Me.btnSeat047.BackColor = clrBack : Me.btnSeat047.Tag = intSEATSTATE
                Case 48 : Me.btnSeat048.BackColor = clrBack : Me.btnSeat048.Tag = intSEATSTATE
                Case 49 : Me.btnSeat049.BackColor = clrBack : Me.btnSeat049.Tag = intSEATSTATE
                Case 50 : Me.btnSeat050.BackColor = clrBack : Me.btnSeat050.Tag = intSEATSTATE
                Case 51 : Me.btnSeat051.BackColor = clrBack : Me.btnSeat051.Tag = intSEATSTATE
                Case 52 : Me.btnSeat052.BackColor = clrBack : Me.btnSeat052.Tag = intSEATSTATE
                Case 53 : Me.btnSeat053.BackColor = clrBack : Me.btnSeat053.Tag = intSEATSTATE
                Case 54 : Me.btnSeat054.BackColor = clrBack : Me.btnSeat054.Tag = intSEATSTATE
                Case 55 : Me.btnSeat055.BackColor = clrBack : Me.btnSeat055.Tag = intSEATSTATE
                Case 56 : Me.btnSeat056.BackColor = clrBack : Me.btnSeat056.Tag = intSEATSTATE
                Case 57 : Me.btnSeat057.BackColor = clrBack : Me.btnSeat057.Tag = intSEATSTATE
                Case 58 : Me.btnSeat058.BackColor = clrBack : Me.btnSeat058.Tag = intSEATSTATE
                Case 59 : Me.btnSeat059.BackColor = clrBack : Me.btnSeat059.Tag = intSEATSTATE
                Case 60 : Me.btnSeat060.BackColor = clrBack : Me.btnSeat060.Tag = intSEATSTATE
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 打席使用状況取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSeatInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEATWORK")
            sql.Append(" WHERE")
            sql.Append(" SEATDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" ORDER BY SEATNO")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim clrBack As Color = UIUtility.COLOR_INFO.FREE
            Dim intSeatNo As Integer = 0
            Dim intSEATSTATE As Integer = 0
            Dim drRight As DataRow()
            Dim drLeft As DataRow()
            For i As Integer = 0 To resultDt.Rows.Count - 1
                intSeatNo = CType(resultDt.Rows(i).Item("SEATNO").ToString, Integer)
                intSEATSTATE = CType(resultDt.Rows(i).Item("SEATSTATE").ToString, Integer)
                Select Case intSEATSTATE
                    Case UIUtility.SEATSTATE.FREE_SPACE
                        '【空き打席】
                        clrBack = UIUtility.COLOR_INFO.FREE
                        If resultDt.Rows(i).Item("RSVKBN").ToString.Equals("1") Then
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                            intSEATSTATE = UIUtility.SEATSTATE.USE_SPACE
                        End If
                    Case UIUtility.SEATSTATE.USE_SPACE
                        '【使用中】
                        If CType(resultDt.Rows(i).Item("JKNKB").ToString, Integer).Equals(0) Then
                            '【1球貸し】
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        Else
                            '【打ち放題】
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        End If
                    Case UIUtility.SEATSTATE.CALL_SPACE
                        '【呼び出し中】
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                    Case UIUtility.SEATSTATE.ERR_SPACE
                        '【通信不能】
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                    Case UIUtility.SEATSTATE.ERR_RW
                        '【RW異常】
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                    Case UIUtility.SEATSTATE.PRICE_CHANGE
                        '【単価切り替え完了】
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                    Case Else
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                End Select

                If UIUtility.SYSTEM.LRMULTI01.Equals(intSeatNo) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSeatNo) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSeatNo) _
                                                Or UIUtility.SYSTEM.LRMULTI04.Equals(intSeatNo) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSeatNo) Then

                    '左打席かどうか
                    If CType(resultDt.Rows(i).Item("LEFTKB").ToString, Integer).Equals(1) Then
                        '【左右兼用打席】
                        drRight = resultDt.Select("SEATNO = " & intSeatNo & " AND LEFTKB = 0")
                        drLeft = resultDt.Select("SEATNO = " & intSeatNo & " AND LEFTKB = 1")
                        If drRight.Length.Equals(0) Or drLeft.Length.Equals(0) Then Continue For
                        If CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.CALL_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.CALL_SPACE) Then
                            '【呼び出し中】
                            intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_SPACE) Then
                            '【通信不能】
                            intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_RW) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_RW) Then
                            '【RW異常】
                            intSEATSTATE = UIUtility.SEATSTATE.ERR_RW
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Then
                            '【使用中】
                            intSEATSTATE = UIUtility.SEATSTATE.USE_SPACE
                            If CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Then
                                If CType(drRight(0).Item("JKNKB").ToString(), Integer).Equals(0) Then
                                    '【1球貸し】
                                    clrBack = UIUtility.COLOR_INFO.USE_TAMA
                                Else
                                    '【打ち放題】
                                    clrBack = UIUtility.COLOR_INFO.USE_TAMA
                                End If
                            End If
                            If CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.USE_SPACE) Then
                                If CType(drLeft(0).Item("JKNKB").ToString(), Integer).Equals(0) Then
                                    '【1球貸し】
                                    clrBack = UIUtility.COLOR_INFO.USE_TAMA
                                Else
                                    '【打ち放題】
                                    clrBack = UIUtility.COLOR_INFO.USE_TAMA
                                End If
                            End If
                        ElseIf (CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE) And CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE)) _
                                Or (CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.PRICE_CHANGE) And CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.PRICE_CHANGE)) Then
                            '【空き打席】【単価切り替え完了】
                            intSEATSTATE = UIUtility.SEATSTATE.FREE_SPACE
                            clrBack = UIUtility.COLOR_INFO.FREE
                            '予約打席
                            If drLeft(0).Item("RSVKBN").ToString.Equals("1") Or drRight(0).Item("RSVKBN").ToString.Equals("1") Then
                                clrBack = UIUtility.COLOR_INFO.USE_TAMA
                                intSEATSTATE = UIUtility.SEATSTATE.USE_SPACE
                            End If
                            '清掃待ち打席
                            If drLeft(0).Item("RSVKBN").ToString.Equals("2") Or drRight(0).Item("RSVKBN").ToString.Equals("2") Then
                                clrBack = UIUtility.COLOR_INFO.USE_TAMA
                                intSEATSTATE = UIUtility.SEATSTATE.USE_SPACE
                            End If
                        Else
                            intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        End If
                    End If
                End If

                '打席予約状況取得
                If GetSEATRSV(intSeatNo) Then
                    intSEATSTATE = UIUtility.SEATSTATE.USE_SPACE
                    clrBack = UIUtility.COLOR_INFO.USE_TAMA
                End If

                Select Case intSeatNo
                    Case 1 : Me.btnSeat001.BackColor = clrBack : Me.btnSeat001.Tag = intSEATSTATE
                    Case 2 : Me.btnSeat002.BackColor = clrBack : Me.btnSeat002.Tag = intSEATSTATE
                    Case 3 : Me.btnSeat003.BackColor = clrBack : Me.btnSeat003.Tag = intSEATSTATE
                    Case 4 : Me.btnSeat004.BackColor = clrBack : Me.btnSeat004.Tag = intSEATSTATE
                    Case 5 : Me.btnSeat005.BackColor = clrBack : Me.btnSeat005.Tag = intSEATSTATE
                    Case 6 : Me.btnSeat006.BackColor = clrBack : Me.btnSeat006.Tag = intSEATSTATE
                    Case 7 : Me.btnSeat007.BackColor = clrBack : Me.btnSeat007.Tag = intSEATSTATE
                    Case 8 : Me.btnSeat008.BackColor = clrBack : Me.btnSeat008.Tag = intSEATSTATE
                    Case 9 : Me.btnSeat009.BackColor = clrBack : Me.btnSeat009.Tag = intSEATSTATE
                    Case 10 : Me.btnSeat010.BackColor = clrBack : Me.btnSeat010.Tag = intSEATSTATE
                    Case 11 : Me.btnSeat011.BackColor = clrBack : Me.btnSeat011.Tag = intSEATSTATE
                    Case 12 : Me.btnSeat012.BackColor = clrBack : Me.btnSeat012.Tag = intSEATSTATE
                    Case 13 : Me.btnSeat013.BackColor = clrBack : Me.btnSeat013.Tag = intSEATSTATE
                    Case 14 : Me.btnSeat014.BackColor = clrBack : Me.btnSeat014.Tag = intSEATSTATE
                    Case 15 : Me.btnSeat015.BackColor = clrBack : Me.btnSeat015.Tag = intSEATSTATE
                    Case 16 : Me.btnSeat016.BackColor = clrBack : Me.btnSeat016.Tag = intSEATSTATE
                    Case 17 : Me.btnSeat017.BackColor = clrBack : Me.btnSeat017.Tag = intSEATSTATE
                    Case 18 : Me.btnSeat018.BackColor = clrBack : Me.btnSeat018.Tag = intSEATSTATE
                    Case 19 : Me.btnSeat019.BackColor = clrBack : Me.btnSeat019.Tag = intSEATSTATE
                    Case 20 : Me.btnSeat020.BackColor = clrBack : Me.btnSeat020.Tag = intSEATSTATE
                    Case 21 : Me.btnSeat021.BackColor = clrBack : Me.btnSeat021.Tag = intSEATSTATE
                    Case 22 : Me.btnSeat022.BackColor = clrBack : Me.btnSeat022.Tag = intSEATSTATE
                    Case 23 : Me.btnSeat023.BackColor = clrBack : Me.btnSeat023.Tag = intSEATSTATE
                    Case 24 : Me.btnSeat024.BackColor = clrBack : Me.btnSeat024.Tag = intSEATSTATE
                    Case 25 : Me.btnSeat025.BackColor = clrBack : Me.btnSeat025.Tag = intSEATSTATE
                    Case 26 : Me.btnSeat026.BackColor = clrBack : Me.btnSeat026.Tag = intSEATSTATE
                    Case 27 : Me.btnSeat027.BackColor = clrBack : Me.btnSeat027.Tag = intSEATSTATE
                    Case 28 : Me.btnSeat028.BackColor = clrBack : Me.btnSeat028.Tag = intSEATSTATE
                    Case 29 : Me.btnSeat029.BackColor = clrBack : Me.btnSeat029.Tag = intSEATSTATE
                    Case 30 : Me.btnSeat030.BackColor = clrBack : Me.btnSeat030.Tag = intSEATSTATE
                    Case 31 : Me.btnSeat031.BackColor = clrBack : Me.btnSeat031.Tag = intSEATSTATE
                    Case 32 : Me.btnSeat032.BackColor = clrBack : Me.btnSeat032.Tag = intSEATSTATE
                    Case 33 : Me.btnSeat033.BackColor = clrBack : Me.btnSeat033.Tag = intSEATSTATE
                    Case 34 : Me.btnSeat034.BackColor = clrBack : Me.btnSeat034.Tag = intSEATSTATE
                    Case 35 : Me.btnSeat035.BackColor = clrBack : Me.btnSeat035.Tag = intSEATSTATE
                    Case 36 : Me.btnSeat036.BackColor = clrBack : Me.btnSeat036.Tag = intSEATSTATE
                    Case 37 : Me.btnSeat037.BackColor = clrBack : Me.btnSeat037.Tag = intSEATSTATE
                    Case 38 : Me.btnSeat038.BackColor = clrBack : Me.btnSeat038.Tag = intSEATSTATE
                    Case 39 : Me.btnSeat039.BackColor = clrBack : Me.btnSeat039.Tag = intSEATSTATE
                    Case 40 : Me.btnSeat040.BackColor = clrBack : Me.btnSeat040.Tag = intSEATSTATE
                    Case 41 : Me.btnSeat041.BackColor = clrBack : Me.btnSeat041.Tag = intSEATSTATE
                    Case 42 : Me.btnSeat042.BackColor = clrBack : Me.btnSeat042.Tag = intSEATSTATE
                    Case 43 : Me.btnSeat043.BackColor = clrBack : Me.btnSeat043.Tag = intSEATSTATE
                    Case 44 : Me.btnSeat044.BackColor = clrBack : Me.btnSeat044.Tag = intSEATSTATE
                    Case 45 : Me.btnSeat045.BackColor = clrBack : Me.btnSeat045.Tag = intSEATSTATE
                    Case 46 : Me.btnSeat046.BackColor = clrBack : Me.btnSeat046.Tag = intSEATSTATE
                    Case 47 : Me.btnSeat047.BackColor = clrBack : Me.btnSeat047.Tag = intSEATSTATE
                    Case 48 : Me.btnSeat048.BackColor = clrBack : Me.btnSeat048.Tag = intSEATSTATE
                    Case 49 : Me.btnSeat049.BackColor = clrBack : Me.btnSeat049.Tag = intSEATSTATE
                    Case 50 : Me.btnSeat050.BackColor = clrBack : Me.btnSeat050.Tag = intSEATSTATE
                    Case 51 : Me.btnSeat051.BackColor = clrBack : Me.btnSeat051.Tag = intSEATSTATE
                    Case 52 : Me.btnSeat052.BackColor = clrBack : Me.btnSeat052.Tag = intSEATSTATE
                    Case 53 : Me.btnSeat053.BackColor = clrBack : Me.btnSeat053.Tag = intSEATSTATE
                    Case 54 : Me.btnSeat054.BackColor = clrBack : Me.btnSeat054.Tag = intSEATSTATE
                    Case 55 : Me.btnSeat055.BackColor = clrBack : Me.btnSeat055.Tag = intSEATSTATE
                    Case 56 : Me.btnSeat056.BackColor = clrBack : Me.btnSeat056.Tag = intSEATSTATE
                    Case 57 : Me.btnSeat057.BackColor = clrBack : Me.btnSeat057.Tag = intSEATSTATE
                    Case 58 : Me.btnSeat058.BackColor = clrBack : Me.btnSeat058.Tag = intSEATSTATE
                    Case 59 : Me.btnSeat059.BackColor = clrBack : Me.btnSeat059.Tag = intSEATSTATE
                    Case 60 : Me.btnSeat060.BackColor = clrBack : Me.btnSeat060.Tag = intSEATSTATE

                End Select
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 打席予約状況取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSEATRSV(ByVal intSEATNO As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEATRSV")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" SEATDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND SEATNO = " & intSEATNO)

            Dim resultDT As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDT.Rows.Count <= 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 打席予約情報更新処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdSEATRSV(ByVal intSEATNO As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder

        Try
            If GetSEATRSV(intSEATNO) Then
                Return False
            End If


            _iDatabase.BeginTransaction()

            sql.Clear()
            '【予約打席】
            sql.Append("INSERT INTO SEATRSV VALUES('" & Now.ToString("yyyyMMdd") & "'," & intSEATNO & ",NULL" & ",1,NOW())")

            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Return False
            End If

            _iDatabase.Commit()

            Return True


        Catch ex As SqlClient.SqlException
            _iDatabase.RollBack()
            Throw ex
        Catch ex As Exception
            _iDatabase.RollBack()
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' ズーム打席ボタン情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetZoomSeatInfo(ByRef btn As Button)
        Dim CpyBtn As Button = Nothing
        Try
            Select Case CType(btn.Text, Integer)
                Case 1 : CpyBtn = Me.btnSeat001
                Case 2 : CpyBtn = Me.btnSeat002
                Case 3 : CpyBtn = Me.btnSeat003
                Case 4 : CpyBtn = Me.btnSeat004
                Case 5 : CpyBtn = Me.btnSeat005
                Case 6 : CpyBtn = Me.btnSeat006
                Case 7 : CpyBtn = Me.btnSeat007
                Case 8 : CpyBtn = Me.btnSeat008
                Case 9 : CpyBtn = Me.btnSeat009
                Case 10 : CpyBtn = Me.btnSeat010
                Case 11 : CpyBtn = Me.btnSeat011
                Case 12 : CpyBtn = Me.btnSeat012
                Case 13 : CpyBtn = Me.btnSeat013
                Case 14 : CpyBtn = Me.btnSeat014
                Case 15 : CpyBtn = Me.btnSeat015
                Case 16 : CpyBtn = Me.btnSeat016
                Case 17 : CpyBtn = Me.btnSeat017
                Case 18 : CpyBtn = Me.btnSeat018
                Case 19 : CpyBtn = Me.btnSeat019
                Case 20 : CpyBtn = Me.btnSeat020
                Case 21 : CpyBtn = Me.btnSeat021
                Case 22 : CpyBtn = Me.btnSeat022
                Case 23 : CpyBtn = Me.btnSeat023
                Case 24 : CpyBtn = Me.btnSeat024
                Case 25 : CpyBtn = Me.btnSeat025
                Case 26 : CpyBtn = Me.btnSeat026
                Case 27 : CpyBtn = Me.btnSeat027
                Case 28 : CpyBtn = Me.btnSeat028
                Case 29 : CpyBtn = Me.btnSeat029
                Case 30 : CpyBtn = Me.btnSeat030
                Case 31 : CpyBtn = Me.btnSeat031
                Case 32 : CpyBtn = Me.btnSeat032
                Case 33 : CpyBtn = Me.btnSeat033
                Case 34 : CpyBtn = Me.btnSeat034
                Case 35 : CpyBtn = Me.btnSeat035
                Case 36 : CpyBtn = Me.btnSeat036
                Case 37 : CpyBtn = Me.btnSeat037
                Case 38 : CpyBtn = Me.btnSeat038
                Case 39 : CpyBtn = Me.btnSeat039
                Case 40 : CpyBtn = Me.btnSeat040
                Case 41 : CpyBtn = Me.btnSeat041
                Case 42 : CpyBtn = Me.btnSeat042
                Case 43 : CpyBtn = Me.btnSeat043
                Case 44 : CpyBtn = Me.btnSeat044
                Case 45 : CpyBtn = Me.btnSeat045
                Case 46 : CpyBtn = Me.btnSeat046
                Case 47 : CpyBtn = Me.btnSeat047
                Case 48 : CpyBtn = Me.btnSeat048
                Case 49 : CpyBtn = Me.btnSeat049
                Case 50 : CpyBtn = Me.btnSeat050
                Case 51 : CpyBtn = Me.btnSeat051
                Case 52 : CpyBtn = Me.btnSeat052
                Case 53 : CpyBtn = Me.btnSeat053
                Case 54 : CpyBtn = Me.btnSeat054
                Case 55 : CpyBtn = Me.btnSeat055
                Case 56 : CpyBtn = Me.btnSeat056
                Case 57 : CpyBtn = Me.btnSeat057
                Case 58 : CpyBtn = Me.btnSeat058
                Case 59 : CpyBtn = Me.btnSeat059
                Case 60 : CpyBtn = Me.btnSeat060

            End Select


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class