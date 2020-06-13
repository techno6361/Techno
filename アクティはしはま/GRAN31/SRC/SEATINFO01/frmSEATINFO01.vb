Imports Techno.DataBase

Public Class frmSEATINFO01

#Region "▼宣言部"

    ''' <summary>
    ''' 予約状況テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtSEATRSV As DataTable
    ''' <summary>
    ''' 打席開始位置【0】左から【1】右から
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _cintSeatPosition As Integer = 0
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


#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打席情報"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "打席情報"

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
    Private Sub frmSEATINFO01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '予約状況取得
            GetSEATRSV()

            'タイマー開始
            Me.Timer1.Start()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                            strSTATE = "清掃待ち打席"
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
                    Case UIUtility.SEATSTATE.WAIT_SPACE, 7
                        '【待機中】
                        strSTATE = "待機中"
                        clrBack = UIUtility.COLOR_INFO.USE_TAMA
                    Case Else
                        strSTATE = "通信エラー"
                        clrBack = UIUtility.COLOR_INFO.ERR_COM
                        blnErr = True
                End Select
                Me.dgvSEAT.Rows(i).Cells("SEATNO").Style.BackColor = clrBack
                Me.dgvSEAT.Rows(i).Cells("STATE").Value = strSTATE
                Me.dgvSEAT.Rows(i).Cells("STATE").Style.BackColor = clrBack

                '顧客種別
                dr = UIUtility.TABLE.KBMAST.Select("NKBNO = " & CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("NKBNO").ToString, Integer))
                If dr.Length > 0 Then Me.dgvSEAT.Rows(i).Cells("CKBNAME").Value = dr(0).Item("CKBNAME").ToString
                '氏名
                Me.dgvSEAT.Rows(i).Cells("CCSNAME").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("CCSNAME").ToString
                'ボール単価
                Me.dgvSEAT.Rows(i).Cells("BALLKIN").Value = (CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("BALLKIN").ToString, Integer) + UIFunction.GetTaxTanka(CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("BALLKIN").ToString, Integer))).ToString & "円"
                '使用球数
                Me.dgvSEAT.Rows(i).Cells("USEBALL").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("USEBALLCNT").ToString & "球"
                '使用開始時間
                Me.dgvSEAT.Rows(i).Cells("STARTTIME").Value = (Math.Floor(CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("STARTTIME").ToString, Integer) / 60)).ToString.PadLeft(2, "0"c) & ":" & (CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("STARTTIME").ToString, Integer) Mod 60).ToString.PadLeft(2, "0"c)
                '使用時間
                Me.dgvSEAT.Rows(i).Cells("USETIME").Value = (Math.Floor(CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("USETIME").ToString, Integer) / 60)).ToString.PadLeft(2, "0"c) & ":" & (CType(UIUtility.TABLE.SEATINFO.Rows(i).Item("USETIME").ToString, Integer) Mod 60).ToString.PadLeft(2, "0"c)

                Me.dgvSEAT.CurrentCell = Nothing

                '【エラーグリッド】
                If blnErr Then
                    '階
                    Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRFLRKB").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("FLRKB").ToString & "階"
                    Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRFLRKB").Style.BackColor = Color.White
                    '№
                    If UIUtility.TABLE.SEATINFO.Rows(i).Item("LEFTKB").ToString.Equals("0") Then
                        '【右打席】
                        Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRSEATNO").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString
                    Else
                        '【左打席】
                        Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRSEATNO").Value = "(左)" & UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString
                    End If
                    Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRSEATNO").Style.BackColor = clrBack
                    Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRSTATE").Value = strSTATE
                    Me.dgvErrSEAT.Rows(intErrGrdRow).Cells("ERRSTATE").Style.BackColor = clrBack
                    intErrGrdRow += 1
                End If
                '【呼び出しグリッド】
                If blnCall Then

                    '階
                    Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLFLRKB").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("FLRKB").ToString & "階"
                    Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLFLRKB").Style.BackColor = Color.White
                    '№
                    If UIUtility.TABLE.SEATINFO.Rows(i).Item("LEFTKB").ToString.Equals("0") Then
                        '【右打席】
                        Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLSEATNO").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString
                    Else
                        '【左打席】
                        Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLSEATNO").Value = "(左)" & UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString
                    End If
                    Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLSEATNO").Style.BackColor = clrBack
                    Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLSTATE").Value = strSTATE
                    Me.dgvCallSEAT.Rows(intCallGrdRow).Cells("CALLSTATE").Style.BackColor = clrBack
                    intCallGrdRow += 1
                End If

            Next

            For i As Integer = intErrGrdRow To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                Me.dgvErrSEAT.Rows(i).Cells("ERRFLRKB").Value = String.Empty
                Me.dgvErrSEAT.Rows(i).Cells("ERRSEATNO").Value = String.Empty
                Me.dgvErrSEAT.Rows(i).Cells("ERRSEATNO").Style.BackColor = Color.White
                Me.dgvErrSEAT.Rows(i).Cells("ERRSTATE").Value = String.Empty
                Me.dgvErrSEAT.Rows(i).Cells("ERRSTATE").Style.BackColor = Color.White
            Next
            Me.dgvErrSEAT.CurrentCell = Nothing

            For i As Integer = intCallGrdRow To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                Me.dgvCallSEAT.Rows(i).Cells("CALLFLRKB").Value = String.Empty
                Me.dgvCallSEAT.Rows(i).Cells("CALLSEATNO").Value = String.Empty
                Me.dgvCallSEAT.Rows(i).Cells("CALLSEATNO").Style.BackColor = Color.White
                Me.dgvCallSEAT.Rows(i).Cells("CALLSTATE").Value = String.Empty
                Me.dgvCallSEAT.Rows(i).Cells("CALLSTATE").Style.BackColor = Color.White
            Next

            Me.dgvCallSEAT.CurrentCell = Nothing

        Catch ex As Exception
            'Me.Timer1.Stop()
            'MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _blnTimerFlg = False
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

            '打席グリッド
            Me.dgvSEAT.RowTemplate.Height = 25
            Me.dgvSEAT.DefaultCellStyle.SelectionBackColor = Color.Transparent
            'エラー打席グリッド
            Me.dgvErrSEAT.RowTemplate.Height = 25
            Me.dgvErrSEAT.DefaultCellStyle.SelectionBackColor = Color.Transparent
            '呼び出し打席グリッド
            Me.dgvCallSEAT.RowTemplate.Height = 25
            Me.dgvCallSEAT.DefaultCellStyle.SelectionBackColor = Color.Transparent
            For i As Integer = 0 To UIUtility.TABLE.SEATINFO.Rows.Count - 1
                Me.dgvSEAT.Rows.Add()

                '階
                Me.dgvSEAT.Rows(i).Cells("FLRKB").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("FLRKB").ToString & "階"
                Me.dgvSEAT.Rows(i).Cells("FLRKB").Style.BackColor = Color.White
                '№
                If UIUtility.TABLE.SEATINFO.Rows(i).Item("LEFTKB").ToString.Equals("0") Then
                    '【右打席】
                    Me.dgvSEAT.Rows(i).Cells("SEATNO").Value = UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString
                Else
                    '【左打席】
                    Me.dgvSEAT.Rows(i).Cells("SEATNO").Value = "(左)" & UIUtility.TABLE.SEATINFO.Rows(i).Item("SEATNO").ToString
                End If
                Me.dgvSEAT.Rows(i).Cells("SEATNO").Style.BackColor = UIUtility.COLOR_INFO.FREE
                Me.dgvSEAT.Rows(i).Cells("SEATNO").Style.ForeColor = Color.White

                Me.dgvSEAT.Rows(i).Cells("STATE").Value = "空き打席"
                Me.dgvSEAT.Rows(i).Cells("STATE").Style.BackColor = UIUtility.COLOR_INFO.FREE
                Me.dgvSEAT.Rows(i).Cells("STATE").Style.ForeColor = Color.White

                '種別
                Me.dgvSEAT.Rows(i).Cells("CKBNAME").Value = "種別外"

                '単価
                Me.dgvSEAT.Rows(i).Cells("BALLKIN").Value = "0円"
                Me.dgvSEAT.Rows(i).Cells("BALLKIN").Style.Alignment = DataGridViewContentAlignment.MiddleRight
                '使用球数
                Me.dgvSEAT.Rows(i).Cells("USEBALL").Value = "0球"
                Me.dgvSEAT.Rows(i).Cells("USEBALL").Style.Alignment = DataGridViewContentAlignment.MiddleRight
                '開始時間
                Me.dgvSEAT.Rows(i).Cells("STARTTIME").Value = "00:00"
                '使用時間
                Me.dgvSEAT.Rows(i).Cells("USETIME").Value = "00:00"

                Me.dgvErrSEAT.Rows.Add()
                Me.dgvErrSEAT.Rows(i).Cells("ERRSEATNO").Style.ForeColor = Color.White
                Me.dgvErrSEAT.Rows(i).Cells("ERRSTATE").Style.ForeColor = Color.White
                Me.dgvCallSEAT.Rows.Add()
                Me.dgvCallSEAT.Rows(i).Cells("CALLSEATNO").Style.ForeColor = Color.White
                Me.dgvCallSEAT.Rows(i).Cells("CALLSTATE").Style.ForeColor = Color.White
            Next

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
                dr(0).Item("CCSNAME") = _dtSEATRSV.Rows(i).Item("CCSNAME").ToString
                dr(0).EndEdit()
                If UIUtility.SYSTEM.LRMULTI01.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI02.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI03.Equals(intSEATNO) _
                                Or UIUtility.SYSTEM.LRMULTI04.Equals(intSEATNO) Or UIUtility.SYSTEM.LRMULTI05.Equals(intSEATNO) Then
                    '予約打席
                    dr(1).Item("RSVKBN") = _dtSEATRSV.Rows(i).Item("RSVKBN").ToString
                    dr(1).Item("CCSNAME") = _dtSEATRSV.Rows(i).Item("CCSNAME").ToString
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
                sql.Append("INSERT INTO SEATRSV VALUES('" & Now.ToString("yyyyMMdd") & "'," & intSEATNO & ",NULL" & ",1,NOW())")
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
                drow(0).Item("SEISANKIN") = resultDt.Rows(i).Item("SEISANKIN").ToString

                drow(0).EndEdit()
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Function


#End Region




End Class
