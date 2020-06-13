Imports TECHNO.DataBase

Public Class frmSEATSELECT01

#Region "▼宣言部"

    ''' <summary>
    ''' 予約状況テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtSEATRSV As DataTable
    ''' <summary>
    ''' モード選択プロパティ列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Mode_Type As Integer
        ''' <summary>
        ''' 通常
        ''' </summary>
        ''' <remarks></remarks>
        NORMAL = 0
        ''' <summary>
        ''' 打席選択
        ''' </summary>
        ''' <remarks></remarks>
        CHOICE = 1
    End Enum

    Private _blnTimerFlg As Boolean = False
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打席指定"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打席指定"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼プロパティ"

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
    ''' 顧客氏名
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CCSNAME As String
        Set(value As String)
            _strCCSNAME = value
        End Set
    End Property
    Private _strCCSNAME As String = String.Empty

    ''' <summary>
    ''' コマンド指定
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MODE As Mode_Type
        Set(value As Mode_Type)
            _intMode = value
        End Set
    End Property
    Private _intMode As Integer = 0

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATSELECT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()
            '予約状況取得
            GetSEATRSV()

            'タイマー開始
            Me.Timer1.Start()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSEATINFO01_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not _dtSEATRSV Is Nothing Then
                _dtSEATRSV.Dispose()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' タイマー_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim dr As DataRow()
        Dim blnErr As Boolean = False
        Dim blnCall As Boolean = False
        Dim intErrGrdRow As Integer = 0
        Dim intCallGrdRow As Integer = 0
        Try
            _blnTimerFlg = True

            If UIUtility.SYSTEM.SYSTEMKBN.Equals(1) Then
                '【打席管理以外の場合】

                '打席情報取得
                SetSeatInfo()
            End If
            '打席予約状況取得
            GetSEATRSV()


            '打席情報グリッド反映
            Dim strSTATE As String = "空き打席"
            Dim clrBack As Color = UIUtility.COLOR_INFO.FREE
            For i As Integer = 0 To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                blnErr = False
                blnCall = False
                Select Case CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATSTATE").ToString, Integer)
                    Case UIUtility.SEATSTATE.FREE_SPACE
                        '【空き打席】
                        strSTATE = "空き打席"
                        clrBack = UIUtility.COLOR_INFO.FREE
                        If UIUtility.TABLE.SEATINFO.Rows(i).Item("RSVKBN").ToString.Equals("1") Then
                            strSTATE = "予約打席"
                            clrBack = UIUtility.COLOR_INFO.RESERV
                        End If
                        If UIUtility.TABLE.SEATINFO.Rows(i).Item("RSVKBN").ToString.Equals("2") Then
                            strSTATE = "掃除待ち"
                            clrBack = UIUtility.COLOR_INFO.SOJI
                        End If
                    Case UIUtility.SEATSTATE.USE_SPACE
                        '【使用中】
                        strSTATE = "使用中"
                        If CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("JKNKB").ToString, Integer).Equals(0) Then
                            '【1球貸し】
                            clrBack = UIUtility.COLOR_INFO.USE_TAMA
                        Else
                            '【打ち放題】
                            clrBack = UIUtility.COLOR_INFO.USE_TIME
                        End If
                    Case UIUtility.SEATSTATE.CALL_SPACE
                        '【呼び出し中】
                        strSTATE = "呼び出し中"
                        clrBack = UIUtility.COLOR_INFO.CALL_COM
                        blnCall = True
                    Case UIUtility.SEATSTATE.ERR_SPACE
                        '【通信不能】
                        strSTATE = "通信エラー"
                        clrBack = UIUtility.COLOR_INFO.ERR_COM
                        blnErr = True
                    Case UIUtility.SEATSTATE.ERR_RW
                        '【RW異常】
                        strSTATE = "RW異常"
                        clrBack = UIUtility.COLOR_INFO.ERR_RW
                        blnErr = True
                    Case UIUtility.SEATSTATE.PRICE_CHANGE
                        '【単価切り替え完了】
                        strSTATE = "空き打席"
                        clrBack = UIUtility.COLOR_INFO.FREE
                    Case UIUtility.SEATSTATE.WAIT_SPACE
                        '【待機中】
                        strSTATE = "待機中"
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                    Case Else
                        strSTATE = "通信エラー"
                        clrBack = UIUtility.COLOR_INFO.ERR_COM
                        blnErr = True
                End Select


                '打席情報ボタン
                SetSeatButton(CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString, Integer), CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATSTATE").ToString, Integer), clrBack)
            Next


        Catch ex As Exception
            'Me.Timer1.Stop()
            'MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _blnTimerFlg = False
        End Try
    End Sub

    ''' <summary>
    ''' 打席ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeat_Click(sender As System.Object, e As System.EventArgs)
        Dim drow() As DataRow
        Dim intRSVKBN As Integer = 0
        Try
            '【タイマー終了まで待機】
            Do
                If Not _blnTimerFlg Then
                    Me.Timer1.Stop()
                    Exit Do
                End If
            Loop

            Dim btnSeat As Button
            btnSeat = CType(sender, Button)

            If _intMode = Mode_Type.NORMAL Then
                If Not CType(btnSeat.Tag, Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE) Then
                    '【空き打席でない】
                    Exit Sub
                End If

                drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & CType(btnSeat.Text, Integer))
                If drow(0).Item("RSVKBN").ToString().Equals("0") Then
                    '予約打席
                    drow(0).Item("RSVKBN") = 1
                    drow(0).Item("CCSNAME") = String.Empty
                Else
                    '通常打席
                    drow(0).Item("RSVKBN") = 0
                    drow(0).Item("CCSNAME") = String.Empty
                End If
                intRSVKBN = CType(drow(0).Item("RSVKBN").ToString, Integer)
                drow(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(CType(btnSeat.Text, Integer)) Or UIUtility.SYSTEM.LRMULTI02.Equals(CType(btnSeat.Text, Integer)) Or UIUtility.SYSTEM.LRMULTI03.Equals(CType(btnSeat.Text, Integer)) _
                                                Or UIUtility.SYSTEM.LRMULTI04.Equals(CType(btnSeat.Text, Integer)) Or UIUtility.SYSTEM.LRMULTI05.Equals(CType(btnSeat.Text, Integer)) Then
                    drow(1).Item("RSVKBN") = intRSVKBN
                    drow(0).Item("CCSNAME") = String.Empty
                    drow(1).EndEdit()
                End If
                '打席予約情報更新
                UpdSEATRSV(intRSVKBN, CType(btnSeat.Text, Integer))
            Else
                '打席指定の時間帯でない場合処理しない
                If UIUtility.SYSTEM.SITEIKBN.Equals(0) Then Exit Sub

                drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & CType(btnSeat.Text, Integer))

                '【打席指定】
                If (Not CType(btnSeat.Tag, Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE)) Then
                    Using frm As New frmMSGBOX01("空き打席を選択して下さい。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
                If drow(0).Item("RSVKBN").ToString.Equals("1") Then
                    Using frm As New frmMSGBOX01("予約中ですが、この打席でよろしいですか？ ", 1)
                        frm.ShowDialog()
                        If Not frm.Reply Then
                            Exit Sub
                        End If
                    End Using
                ElseIf drow(0).Item("RSVKBN").ToString.Equals("2") Then
                    Using frm As New frmMSGBOX01("清掃待ちですが、この打席でよろしいですか？ ", 1)
                        frm.ShowDialog()
                        If Not frm.Reply Then
                            Exit Sub
                        End If
                    End Using
                End If


                '予約打席
                drow(0).Item("RSVKBN") = 1
                drow(0).Item("CCSNAME") = _strCCSNAME
                drow(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(CType(btnSeat.Text, Integer)) Or UIUtility.SYSTEM.LRMULTI02.Equals(CType(btnSeat.Text, Integer)) Or UIUtility.SYSTEM.LRMULTI03.Equals(CType(btnSeat.Text, Integer)) _
                                    Or UIUtility.SYSTEM.LRMULTI04.Equals(CType(btnSeat.Text, Integer)) Or UIUtility.SYSTEM.LRMULTI05.Equals(CType(btnSeat.Text, Integer)) Then
                    '予約打席
                    drow(1).Item("RSVKBN") = 1
                    drow(1).Item("CCSNAME") = _strCCSNAME
                    drow(1).EndEdit()
                End If
                '打席予約情報更新
                If Not UpdSEATRSV(1, CType(btnSeat.Text, Integer)) Then
                    Using frm As New frmMSGBOX01("この打席は選択できません。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If

                _intSELECTSEAT = CType(btnSeat.Text, Integer)

                Me.Close()

            End If
  
   

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Timer1.Start()
        End Try
    End Sub

    ''' <summary>
    ''' 打席詳細ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSEATINFO01_Click(sender As System.Object, e As System.EventArgs) Handles btnSEATINFO01.Click
        Try
            Using frm As New frmSEATINFO01(iDatabase)
                frm.MODE = frmSEATINFO01.Mode_Type.NORMAL
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            'イベントハンドル
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
    ''' 打席予約状況取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSEATRSV() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim dr As DataRow()
        Dim intSEATNO As Integer = 0
        Try


            For i As Integer = 0 To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                intSEATNO = CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString, Integer)
                dr = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSEATNO)
                '通常打席
                dr(0).Item("RSVKBN") = 0
                dr(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSEATNO) _
                                Or UIUtility.SYSTEM.LRMULTI04.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSEATNO) Then
                    '予約打席
                    dr(1).Item("RSVKBN") = 0
                    dr(1).EndEdit()
                End If
            Next

            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SEATRSV")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" SEATDT = '" & Now.ToString("yyyyMMdd") & "'")

            _dtSEATRSV = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To _dtSEATRSV.Rows.Count - 1
                intSEATNO = CType(_dtSEATRSV.Rows(i).Item("SEATNO").ToString, Integer)

                dr = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & intSEATNO)

                '予約打席
                dr(0).Item("RSVKBN") = _dtSEATRSV.Rows(i).Item("RSVKBN").ToString
                dr(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSEATNO) _
                                Or UIUtility.SYSTEM.LRMULTI04.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSEATNO) Then
                    '予約打席
                    dr(1).Item("RSVKBN") = _dtSEATRSV.Rows(i).Item("RSVKBN").ToString
                    dr(1).EndEdit()
                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

    ''' <summary>
    ''' 打席使用状況取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSeatInfo() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM SEATWORK")
            sql.Append(" WHERE SEATDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" ORDER BY")
            sql.Append(" SEATNO,LEFTKB")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Dim drow() As DataRow
            For i As Integer = 0 To resultDt.Rows.Count - 1
                drow = UIUtility.TABLE.SEATINFO.Select("SEATNO = " & CType(resultDt.Rows(i).Item("SEATNO").ToString, Integer) _
                                                       & " AND LEFTKB = " & CType(resultDt.Rows(i).Item("LEFTKB").ToString, Integer))
                '時間貸し区分
                drow(0).Item("JKNKB") = CType(resultDt.Rows(i).Item("JKNKB").ToString, Integer)
                '時間帯
                drow(0).Item("TIMEKB") = CType(resultDt.Rows(i).Item("TIMEKB").ToString, Integer)
                '打席状況
                drow(0).Item("SEATSTATE") = CType(resultDt.Rows(i).Item("SEATSTATE").ToString, Integer)
                '使用総球数
                drow(0).Item("ALLBALLCNT") = CType(resultDt.Rows(i).Item("ALLBALLCNT").ToString, Integer)
                '使用球数
                drow(0).Item("USEBALLCNT") = CType(resultDt.Rows(i).Item("USEBALLCNT").ToString, Integer)
                '使用開始時間
                drow(0).Item("STARTTIME") = CType(resultDt.Rows(i).Item("STARTTIME").ToString, Integer)
                '使用時間
                drow(0).Item("USETIME") = CType(resultDt.Rows(i).Item("USETIME").ToString, Integer)
                '顧客種別
                drow(0).Item("NKBNO") = CType(resultDt.Rows(i).Item("NKBNO").ToString, Integer)
                'ボール単価
                drow(0).Item("BALLKIN") = CType(resultDt.Rows(i).Item("BALLKIN").ToString, Integer)
                '球数更新フラグ
                drow(0).Item("UPDFLG") = CType(resultDt.Rows(i).Item("UPDFLG").ToString, Integer)
                '予約打席
                drow(0).Item("RSVKBN") = CType(resultDt.Rows(i).Item("RSVKBN").ToString, Integer)
                '顧客番号
                drow(0).Item("NCSNO") = resultDt.Rows(i).Item("NCSNO").ToString
                '氏名
                drow(0).Item("CCSNAME") = resultDt.Rows(i).Item("CCSNAME").ToString
                '精算金額
                drow(0).Item("SEISANKIN") = CType(resultDt.Rows(i).Item("SEISANKIN").ToString, Integer)
                '使用前残金額
                drow(0).Item("ZANKN") = CType(resultDt.Rows(i).Item("ZANKN").ToString, Integer)
                '使用前P)残金額
                drow(0).Item("PREZANKN") = CType(resultDt.Rows(i).Item("PREZANKN").ToString, Integer)
                '使用前残ポイント
                drow(0).Item("SRTPO") = CType(resultDt.Rows(i).Item("SRTPO").ToString, Integer)
                '顧客番号(DB取得時)
                drow(0).Item("DBNCSNO") = resultDt.Rows(i).Item("DBNCSNO").ToString

                drow(0).EndEdit()
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function

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
                ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_SPACE) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_SPACE) Then
                    '【通信不能】
                    intSEATSTATE = UIUtility.SEATSTATE.ERR_SPACE
                    clrBack = UIUtility.COLOR_INFO.ERR_COM
                ElseIf CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_RW) Or CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.ERR_RW) Then
                    '【RW異常】
                    intSEATSTATE = UIUtility.SEATSTATE.ERR_RW
                    clrBack = UIUtility.COLOR_INFO.ERR_RW
                ElseIf (CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE) And CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.FREE_SPACE)) _
                        Or (CType(drRight(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.PRICE_CHANGE) And CType(drLeft(0).Item("SEATSTATE").ToString(), Integer).Equals(UIUtility.SEATSTATE.PRICE_CHANGE)) Then
                    '【空き打席】【単価切り替え完了】
                    intSEATSTATE = UIUtility.SEATSTATE.FREE_SPACE
                    clrBack = UIUtility.COLOR_INFO.FREE
                    '予約打席
                    If drLeft(0).Item("RSVKBN").ToString.Equals("1") Or drRight(0).Item("RSVKBN").ToString.Equals("1") Then
                        clrBack = UIUtility.COLOR_INFO.RESERV
                    End If
                    If drLeft(0).Item("RSVKBN").ToString.Equals("2") Or drRight(0).Item("RSVKBN").ToString.Equals("2") Then
                        clrBack = UIUtility.COLOR_INFO.SOJI
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
    ''' 打席予約情報更新処理
    ''' </summary>
    ''' <param name="intRSVKBN"></param>
    ''' <param name="intSEATNO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdSEATRSV(ByVal intRSVKBN As Integer, ByVal intSEATNO As Integer) As Boolean
        Dim sql As New System.Text.StringBuilder

        Try


            iDatabase.BeginTransaction()

            sql.Clear()
            If intRSVKBN.Equals(1) Then
                '【予約打席】
                sql.Append("INSERT INTO SEATRSV VALUES('" & Now.ToString("yyyyMMdd") & "'," & intSEATNO & ",'" & _strCCSNAME & "',1,NOW())")
            Else
                '【通常打席】
                sql.Append("DELETE FROM SEATRSV WHERE SEATDT = '" & Now.ToString("yyyyMMdd") & "' AND SEATNO = " & intSEATNO)
            End If


            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            iDatabase.Commit()

            Return True


        Catch ex As SqlClient.SqlException
            iDatabase.RollBack()
            Throw ex
        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        Finally

        End Try

    End Function

#End Region

End Class
