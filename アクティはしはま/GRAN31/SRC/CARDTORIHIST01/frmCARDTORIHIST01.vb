Imports TECHNO.DataBase

Public Class frmCARDTORIHIST01

#Region "▼宣言部"

    ''' <summary>
    ''' 取引履歴テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtTORIHIKI As DataTable
    ''' <summary>
    ''' ページ数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intPageNo As Integer = 1
    ''' <summary>
    ''' 最終ページ数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intLastPageNo As Integer = 0
    ''' <summary>
    ''' グリッド最大行数
    ''' </summary>
    ''' <remarks></remarks>
    Private _intMaxRowCount As Integer = 17
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "カード取引履歴"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "カード取引履歴"

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
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MANNO() As String
        Set(value As String)
            _strMANNO = value
        End Set
    End Property
    Private _strMANNO As String = String.Empty

    ''' <summary>
    ''' 氏名
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CCSNAME() As String
        Set(value As String)
            _strCCSNAME = value
        End Set
    End Property
    Private _strCCSNAME As String = String.Empty

    ''' <summary>
    ''' 残金
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ZANKN() As Integer
        Set(value As Integer)
            _intZANKN = value
        End Set
    End Property
    Private _intZANKN As Integer = 0

    ''' <summary>
    ''' P残金
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PREZANKN() As Integer
        Set(value As Integer)
            _intPREZANKN = value
        End Set
    End Property
    Private _intPREZANKN As Integer = 0

    ''' <summary>
    ''' ﾎﾟｲﾝﾄ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SRTPO() As Integer
        Set(value As Integer)
            _intSRTPO = value
        End Set
    End Property
    Private _intSRTPO As Integer = 0

    ''' <summary>
    ''' 取引データ削除区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DELKBN As Integer
        Get
            Return _intDELKBN
        End Get
    End Property
    Private _intDELKBN As Integer = 0

#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCARDTRIHIST01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            '画面初期設定
            Init()

            'ｶｰﾄﾞ取引情報取得
            GetTORIHIKI()

            'ｶｰﾄﾞ取引情報表示
            SetTORIHIKI(_dtTORIHIKI.Select)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmCARDTRIHIST01_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not _dtTORIHIKI Is Nothing Then
                _dtTORIHIKI.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F9削除ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func9()
        Dim sql As New System.Text.StringBuilder
        Try
            Using frm As New frmMSGBOX01("取引履歴を削除してもよろしいですか？" & vbCrLf & "※金額・ポイントもクリアされます※", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            Me.Cursor = Cursors.WaitCursor


            'トランザクション開始
            iDatabase.BeginTransaction()

            '顧客情報カード番号クリア
            sql.Clear()
            sql.Append("UPDATE CSMAST SET NCARDID = 0,ENTCNT = 0,ENTCNT2 = 0,ZENENTDATE = NULL,ENTDT = NULL,DUPDATE = NOW() WHERE NCSNO = " & CType(Me.lblNCSNO.Text, Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("カード番号の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '金額情報0クリア
            sql.Clear()
            sql.Append("UPDATE KINSMA SET ZANKN = 0,PREZANKN = 0,UPDDTM = NOW() WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("金額情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            'ポイント情報0クリア
            sql.Clear()
            sql.Append("UPDATE DPOINTSMA SET SRTPO = 0,UPDDTM = NOW() WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("ポイント情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '売上トランデータ削除
            sql.Clear()
            sql.Append("UPDATE DUDNTRN SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("売上トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '伝票トランデータ削除
            sql.Clear()
            sql.Append("UPDATE DENTRA SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("伝票トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '入金トランデータ削除
            sql.Clear()
            sql.Append("UPDATE NKNTRN SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("入金トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '残高移行トランデータ削除
            sql.Clear()
            sql.Append("UPDATE IKOTRN SET NCSNO = 0 WHERE NCSNO = " & CType(Me.lblNCSNO.Text, Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("残高移行トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '商品トランデータ削除
            sql.Clear()
            sql.Append("UPDATE HINTRN SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("商品トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '入場トランデータ削除
            sql.Clear()
            sql.Append("UPDATE ENTTRA SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("入場トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '無人入場トランデータ削除
            sql.Clear()
            sql.Append("UPDATE ENTTRB SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("無人入場トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            'コース料金トランデータ削除
            sql.Clear()
            sql.Append("UPDATE KOSUTRN SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("コース料金トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            'ポイント還元トランデータ削除
            sql.Clear()
            sql.Append("UPDATE DREPOTRN SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("ポイント還元トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '残高修正トランデータ削除
            sql.Clear()
            sql.Append("UPDATE CRTTRN SET NCSNO = 0 WHERE NCSNO = " & CType(Me.lblNCSNO.Text, Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("残高修正トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '金額クリアトランデータ削除
            sql.Clear()
            sql.Append("UPDATE LKINTRA SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("金額クリアトラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            'ボールトランデータ削除
            sql.Clear()
            sql.Append("UPDATE BALLTRN SET NCSNO = 0 WHERE NCSNO = " & CType(Me.lblNCSNO.Text, Integer))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("ボールトラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            'カード再発行トランデータ削除
            sql.Clear()
            sql.Append("UPDATE RECARDTRN SET MANNO = '00000000' WHERE MANNO = '" & Me.lblNCSNO.Text & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("カード再発行トラン情報の削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            'コミット
            iDatabase.Commit()

            _intDELKBN = 1

            Using frm As New frmMSGBOX01("正常に削除できました。", 0)
                frm.ShowDialog()
            End Using

            Me.Close()

            'If CType(Me.btnCard.Tag, Integer).Equals(1) Then
            '    Using frm As New frmREQUESTCARD(dcICR700)
            '        'カード排出処理
            '        frm.COMMAND = frmREQUESTCARD.Command_Type.EJECT
            '        frm.ShowDialog()
            '    End Using
            'End If
            'Init()
            'Me.txtNCSNO.Focus()


            'Me.txtNCSNO.Focus()

        Catch ex As Exception
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' 次ページボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click

        Try
            If _intPageNo.Equals(0) Then _intPageNo = 1

            Me.dgvTORIHIKI.RowCount = 0

            '取引情報表示
            SetTORIHIKI(_dtTORIHIKI.Select("NO >= " & (_intPageNo * _intMaxRowCount) + 1))

            _intPageNo += 1
            Me.btnBack.Enabled = True
            If _intPageNo.Equals(_intLastPageNo) Then
                Me.btnNext.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 前ページボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Me.dgvTORIHIKI.RowCount = 0
            _intPageNo -= 1

            '取引情報表示
            SetTORIHIKI(_dtTORIHIKI.Select("NO >= " & (_intPageNo * _intMaxRowCount) - (_intMaxRowCount - 1)))

            Me.btnNext.Enabled = True
            If _intPageNo.Equals(1) Then
                Me.btnBack.Enabled = False
            End If

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
            '一覧
            Me.tspFunc3.Enabled = False
            '印刷
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = True
            '画面印刷
            Me.tspFunc10.Enabled = True
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            '顧客番号
            Me.lblNCSNO.Text = _strMANNO
            '氏名
            Me.lblCCSNAME.Text = _strCCSNAME

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 取引履歴情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTORIHIKI()
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()

            sql.Append("SELECT ROW_NUMBER() OVER(ORDER BY T.UDNDT DESC,T.UDNTIME DESC) AS NO,T.* FROM (")

            '■商品001
            sql.Append(" SELECT ")
            sql.Append("'商品' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(A.UDNBKN - A.PREMKN) AS KINGAKU")                 '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN HINTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '001'")
            sql.Append(" AND A.BUNCDB = '001'")
            sql.Append(" AND A.BUNCDC <> '999'")
            sql.Append(" AND A.SMADT IS NULL") '残高移行時のカード発行料省く
            'sql.Append(" AND A.SMADT <> '999'") '残高移行時のカード発行料省く ※何故かこれだと抽出できない。
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")
            sql.Append(" AND CPAYKBN = '4'")

            sql.Append(" UNION")

            '■年会費
            sql.Append(" SELECT ")
            sql.Append("'年会費' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(A.UDNBKN - A.PREMKN) AS KINGAKU")                 '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN HINTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '001'")
            sql.Append(" AND A.BUNCDB = '001'")
            sql.Append(" AND A.BUNCDC = '999'")
            sql.Append(" AND A.CPAYKBN = '4'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■ｶｰﾄﾞ発行010
            sql.Append(" SELECT ")
            sql.Append("'入金(発行)' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",B.ZANBKN AS KINGAKU")                            '金額
            sql.Append(",B.PREZANBKN AS PREMKN")                          'ﾌﾟﾚﾐｱﾑ
            sql.Append(",B.ZANBPO AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN NKNTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '010'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■入金003
            sql.Append(" SELECT ")
            sql.Append("'入金' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",A.UDNKN  AS KINGAKU")                            '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN NKNTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '003'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■ｻｰﾋﾞｽ入金004
            sql.Append(" SELECT ")
            sql.Append("'ｻｰﾋﾞｽ入金' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",0 AS KINGAKU")                                 '金額
            sql.Append(",A.UDNBKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN NKNTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '004'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■入場料005
            sql.Append(" SELECT ")
            sql.Append("'入場料' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(A.UDNBKN - A.PREMKN) AS KINGAKU")               '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",C.CKBNAME AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN ENTTRA AS B ON B.ENTDT = A.UDNDT AND B.ENTNO = A.UDNNO")
            sql.Append(" LEFT JOIN KBMAST AS C ON CAST(C.NKBNO AS VARCHAR) = B.KSBKB")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '005'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")
            sql.Append(" AND B.EIGKB = '1'")
            sql.Append(" AND (B.KSBKB = '1' OR B.KSBKB = '2' OR B.KSBKB = '3' OR B.KSBKB = '4' OR B.KSBKB = '5' OR B.KSBKB = '6' OR B.KSBKB = '7' OR B.KSBKB = '8' OR B.KSBKB = '9' OR B.KSBKB = '10')")

            sql.Append(" UNION")

            sql.Append(" SELECT ")
            sql.Append("'打ち放題' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(A.UDNBKN - A.PREMKN) AS KINGAKU")               '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN ENTTRA AS B ON B.ENTDT = A.UDNDT AND B.ENTNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '005'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")
            sql.Append(" AND B.EIGKB = '2'")

            sql.Append(" UNION")

            '■無人営業時取得ポイント
            sql.Append(" SELECT ")
            sql.Append("'無人営業時取得ポイント' AS KBN")
            sql.Append(",A.ENTDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",0 AS KINGAKU")                                   '金額
            sql.Append(",0 AS PREMKN")                                    'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",0 AS BZANKN")
            sql.Append(",0 AS BPREMKN")
            sql.Append(",0 AS BPOINT")
            sql.Append(",0 AS AZANKN")
            sql.Append(",0 AS APREMKN")
            sql.Append(",0 AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",B.CKBNAME AS CKBNAME")
            sql.Append(" FROM ENTTRB AS A")
            sql.Append(" LEFT JOIN KBMAST AS B ON CAST(B.NKBNO AS VARCHAR) = A.KSBKB")
            sql.Append(" WHERE")
            sql.Append(" A.MANNO = '" & _strMANNO & "'")
            sql.Append(" AND (A.KSBKB = '1' OR A.KSBKB = '2' OR A.KSBKB = '3' OR A.KSBKB = '4' OR A.KSBKB = '5' OR A.KSBKB = '6' OR A.KSBKB = '7' OR A.KSBKB = '8' OR A.KSBKB = '9' OR A.KSBKB = '10')")
            sql.Append(" AND POINT > 0")

            ' ''sql.Append(" UNION")

            '' ''■ﾁｪｯｸｱｳﾄﾎﾟｲﾝﾄ
            ' ''sql.Append(" SELECT ")
            ' ''sql.Append("'退場ポイント' AS KBN")
            ' ''sql.Append(",ENTDT AS ENTDT")                               '日付
            ' ''sql.Append(",TO_CHAR(UPDDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            ' ''sql.Append(",0 AS KINGAKU")               '金額
            ' ''sql.Append(",0 AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            ' ''sql.Append(",OUTPO AS POINT")                               'ﾎﾟｲﾝﾄ
            ' ''sql.Append(",0 AS BZANKN")
            ' ''sql.Append(",0 AS BPREMKN")
            ' ''sql.Append(",0 AS BPOINT")
            ' ''sql.Append(",0 AS AZANKN")
            ' ''sql.Append(",0 AS APREMKN")
            ' ''sql.Append(",0 AS APOINT")
            ' ''sql.Append(",0 AS CLRKBN")
            ' ''sql.Append(" FROM ENTTRA")
            ' ''sql.Append(" WHERE")
            ' ''sql.Append(" DATKB = '1'")
            ' ''sql.Append(" AND MANNO = '" & _strMANNO & "'")
            ' ''sql.Append(" AND OUTFLG = 1")

            sql.Append(" UNION")

            '■コース料金006
            sql.Append(" SELECT ")
            sql.Append("'コース料金' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(A.UDNBKN - A.PREMKN) AS KINGAKU")                 '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN KOSUTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '006'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■ﾎﾟｲﾝﾄ還元007
            sql.Append(" SELECT ")
            sql.Append("'ﾎﾟｲﾝﾄ還元' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(A.UDNBKN - A.PREMKN) AS KINGAKU")                 '金額
            sql.Append(",A.PREMKN AS PREMKN")                             'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN DREPOTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '007'")
            sql.Append(" AND (A.TKTKBN = '0' OR A.TKTKBN = '2')")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            sql.Append(" SELECT ")
            sql.Append("'ﾎﾟｲﾝﾄ還元(ﾁｹｯﾄ)' AS KBN")
            sql.Append(",A.UDNDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(A.INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",0 AS KINGAKU")                                 '金額
            sql.Append(",0 AS PREMKN")                                  'ﾌﾟﾚﾐｱﾑ
            sql.Append(",A.POINT AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",B.ZANAKN AS BZANKN")
            sql.Append(",B.PREZANAKN AS BPREMKN")
            sql.Append(",B.ZANAPO AS BPOINT")
            sql.Append(",B.ZANBKN AS AZANKN")
            sql.Append(",B.PREZANBKN AS APREMKN")
            sql.Append(",B.ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM DUDNTRN AS A")
            sql.Append(" LEFT JOIN DREPOTRN AS B ON B.DENDT = A.UDNDT AND B.DENNO = A.UDNNO")
            sql.Append(" WHERE")
            sql.Append(" A.DATKB = '1'")
            sql.Append(" AND A.BMNCD = '002'")
            sql.Append(" AND A.BUNCDA = '007'")
            sql.Append(" AND A.TKTKBN = '1'")
            sql.Append(" AND A.MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■残高移行
            sql.Append(" SELECT ")
            sql.Append("'残高移行' AS KBN")
            sql.Append(",IKODT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",MOTOZANKN AS KINGAKU")                         '金額
            sql.Append(",MOTOPREZANKN AS PREMKN")                       'ﾌﾟﾚﾐｱﾑ
            sql.Append(",MOTOSRTPO AS POINT")                           'ﾎﾟｲﾝﾄ
            sql.Append(",(ZANKN - MOTOZANKN) AS BZANKN")
            sql.Append(",(PREZANKN - MOTOPREZANKN) AS BPREMKN")
            sql.Append(",(SRTPO - MOTOSRTPO) AS BPOINT")
            sql.Append(",ZANKN AS AZANKN")
            sql.Append(",PREZANKN AS APREMKN")
            sql.Append(",SRTPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM IKOTRN")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(_strMANNO, Integer))

            sql.Append(" UNION")

            '■残高修正
            sql.Append(" SELECT ")
            sql.Append("'球数金額※' AS KBN")
            sql.Append(",CRTDT AS UDNDT")                               '日付
            sql.Append(",TO_CHAR(INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",(ZANKN - MAEZANKN) AS KINGAKU")                '金額
            sql.Append(",(PREZANKN - MAEPREZANKN) AS PREMKN")           'ﾌﾟﾚﾐｱﾑ
            sql.Append(",(SRTPO - MAESRTPO) AS POINT")                  'ﾎﾟｲﾝﾄ
            sql.Append(",MAEZANKN AS BZANKN")
            sql.Append(",MAEPREZANKN AS BPREMKN")
            sql.Append(",MAESRTPO AS BPOINT")
            sql.Append(",ZANKN AS AZANKN")
            sql.Append(",PREZANKN AS APREMKN")
            sql.Append(",SRTPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM CRTTRN")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(_strMANNO, Integer))

            sql.Append(" UNION")

            '■ｶｰﾄﾞ有効期限切れ
            sql.Append(" SELECT ")
            sql.Append("'有効期限切れ' AS KBN")
            sql.Append(",TO_CHAR(INSDTM,'YYYYMMDD') AS UDNDT")          '日付
            sql.Append(",TO_CHAR(INSDTM,'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",ZANKN AS KINGAKU")                             '金額
            sql.Append(",PREZANKN AS PREMKN")                           'ﾌﾟﾚﾐｱﾑ
            sql.Append(",SRTPO AS POINT")                               'ﾎﾟｲﾝﾄ
            sql.Append(",ZANAKN AS BZANKN")
            sql.Append(",PREZANAKN AS BPREMKN")
            sql.Append(",ZANAPO AS BPOINT")
            sql.Append(",ZANBKN AS AZANKN")
            sql.Append(",PREZANBKN AS APREMKN")
            sql.Append(",ZANBPO AS APOINT")
            sql.Append(",CLRKBN AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM LKINTRA")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & _strMANNO & "'")

            sql.Append(" UNION")

            '■ボール使用球数金額
            sql.Append(" SELECT ")
            sql.Append("'球数金額' AS KBN")
            sql.Append(",TO_CHAR(UPDDTM,'YYYYMMDD') AS UDNDT")          '日付
            sql.Append(",TO_CHAR((UPDDTM + INTERVAL '-1 SECOND'),'HH24:MI:SS') AS UDNTIME")      '時間
            sql.Append(",PAYZANKN AS KINGAKU")                          '金額
            sql.Append(",PAYPREMKN AS PREMKN")                          'ﾌﾟﾚﾐｱﾑ
            sql.Append(",0 AS POINT")                                   'ﾎﾟｲﾝﾄ
            sql.Append(",ZANAKN AS BZANKN")
            sql.Append(",PREZANAKN AS BPREMKN")
            sql.Append(",ZANAPO AS BPOINT")
            sql.Append(",ZANBKN AS AZANKN")
            sql.Append(",PREZANBKN AS APREMKN")
            sql.Append(",ZANBPO AS APOINT")
            sql.Append(",0 AS CLRKBN")
            sql.Append(",NULL AS CKBNAME")
            sql.Append(" FROM BALLTRN")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & CType(_strMANNO, Integer))
            sql.Append(" AND NKBNO IN (1,2,3,4,5,6,7,8,9,10)")

            sql.Append(" ORDER BY UDNDT DESC,UDNTIME DESC")

            sql.Append(") T")

            _dtTORIHIKI = iDatabase.ExecuteRead(sql.ToString())

            If _dtTORIHIKI.Rows.Count.Equals(0) Then
                Me.btnBack.Enabled = False
                Me.btnNext.Enabled = False
                Exit Sub
            End If

            _intPageNo = 1

            '最終ページ数計算
            Dim dblLastPageNo As Double = _dtTORIHIKI.Rows.Count / _intMaxRowCount
            _intLastPageNo = CType(Math.Ceiling(dblLastPageNo), Integer)

            Me.btnBack.Enabled = False
            Me.btnNext.Enabled = False

            If _intLastPageNo > 1 Then
                Me.btnNext.Enabled = True
            End If

            ''前後残金額計算
            'Dim intKINGAKU As Integer = 0
            'Dim intPREMKN As Integer = 0
            'Dim intPOINT As Integer = 0
            'For Each arr As DataRow In _dtTORIHIKI.Rows

            '    '金額
            '    intKINGAKU = CType(arr.Item("KINGAKU").ToString, Integer)
            '    'ﾌﾟﾚﾐｱﾑ
            '    intPREMKN = CType(arr.Item("PREMKN").ToString, Integer)
            '    'ﾎﾟｲﾝﾄ
            '    intPOINT = CType(arr.Item("POINT").ToString, Integer)
            '    Select Case arr.Item("KBN").ToString
            '        Case "商品"
            '            intKINGAKU = CType("-" & intKINGAKU, Integer)
            '            intPREMKN = CType("-" & intPREMKN, Integer)
            '            intPOINT = intPOINT
            '        Case "年会費"
            '            intKINGAKU = CType("-" & intKINGAKU, Integer)
            '            intPREMKN = CType("-" & intPREMKN, Integer)
            '            intPOINT = intPOINT
            '        Case "入金"
            '        Case "ｻｰﾋﾞｽ入金"
            '        Case "1球貸し", "打ち放題", "ｻｰﾋﾞｽｶｰﾄﾞ"
            '            intKINGAKU = CType("-" & intKINGAKU, Integer)
            '            intPREMKN = CType("-" & intPREMKN, Integer)
            '            intPOINT = intPOINT
            '        Case "ｶｰﾄﾞ支出"
            '            intKINGAKU = CType("-" & intKINGAKU, Integer)
            '            intPREMKN = CType("-" & intPREMKN, Integer)
            '            intPOINT = intPOINT
            '        Case "ﾎﾟｲﾝﾄ還元"
            '        Case "ﾎﾟｲﾝﾄ還元(ﾁｹｯﾄ)"
            '        Case "残高移行"
            '        Case "残高修正"
            '        Case "有効期限切れ"
            '            intKINGAKU = CType("-" & intKINGAKU, Integer)
            '            intPREMKN = CType("-" & intPREMKN, Integer)
            '            intPOINT = CType("-" & intPOINT, Integer)
            '            If CType(arr.Item("CLRKBN").ToString, Integer).Equals(0) Then
            '                intPREMKN = CType(arr.Item("KINGAKU").ToString, Integer)
            '            End If
            '        Case "球数金額"
            '            intKINGAKU = CType("-" & intKINGAKU, Integer)
            '            intPREMKN = CType("-" & intPREMKN, Integer)
            '            intPOINT = CType("-" & intPOINT, Integer)
            '    End Select
            '    ' ''(後)残金
            '    ''arr.Item("AZANKN") = _intZANKN
            '    ' ''(後)残ﾌﾟﾚﾐｱﾑ
            '    ''arr.Item("APREMKN") = _intPREZANKN
            '    ' ''(後)残ﾎﾟｲﾝﾄ
            '    ''arr.Item("APOINT") = _intSRTPO
            '    ''_intZANKN -= intKINGAKU
            '    ''_intPREZANKN -= intPREMKN
            '    ''_intSRTPO -= intPOINT
            '    ' ''(前)残金
            '    ''arr.Item("BZANKN") = _intZANKN
            '    ' ''(前)残ﾌﾟﾚﾐｱﾑ
            '    ''arr.Item("BPREMKN") = _intPREZANKN
            '    ' ''(前)残ﾎﾟｲﾝﾄ
            '    ''arr.Item("BPOINT") = _intSRTPO
            '    arr.EndEdit()
            'Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 取引情報表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTORIHIKI(ByVal dr As DataRow())
        Dim intKINGAKU As Integer = 0
        Dim intPREMKN As Integer = 0
        Dim intPOINT As Integer = 0
        Try

            For i As Integer = 0 To dr.Length - 1
                Me.dgvTORIHIKI.RowCount += 1

                '連番
                Me.dgvTORIHIKI.SetValue("colNO", i, dr(i).Item("NO").ToString)
                '日付
                Me.dgvTORIHIKI.SetValue("colUDNDT", i, dr(i).Item("UDNDT").ToString.Substring(0, 4) & "/" & dr(i).Item("UDNDT").ToString.Substring(4, 2) & "/" & dr(i).Item("UDNDT").ToString.Substring(6, 2))
                '時間
                Me.dgvTORIHIKI.SetValue("colUDNTIME", i, dr(i).Item("UDNTIME").ToString)
                '区分
                Me.dgvTORIHIKI.SetValue("colKBN", i, dr(i).Item("KBN").ToString)
                '金額
                intKINGAKU = CType(dr(i).Item("KINGAKU").ToString, Integer)
                Me.dgvTORIHIKI.SetValue("colKINGAKU", i, System.Math.Abs(CType(dr(i).Item("KINGAKU").ToString, Integer)).ToString("#,##0"))
                'ﾌﾟﾚﾐｱﾑ
                intPREMKN = CType(dr(i).Item("PREMKN").ToString, Integer)
                Me.dgvTORIHIKI.SetValue("colPREMKN", i, System.Math.Abs(CType(dr(i).Item("PREMKN").ToString, Integer)).ToString("#,##0"))
                'ﾎﾟｲﾝﾄ
                intPOINT = CType(dr(i).Item("POINT").ToString, Integer)
                Me.dgvTORIHIKI.SetValue("colPOINT", i, System.Math.Abs(CType(dr(i).Item("POINT").ToString, Integer)).ToString("#,##0"))

                Select Case dr(i).Item("KBN").ToString
                    Case "商品"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = intPOINT
                    Case "年会費"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = intPOINT
                    Case "入金", "入金(発行)", "無人営業時取得ポイント"
                        If intKINGAKU >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        End If
                        If intPREMKN >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        End If
                        If intPOINT >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                        End If
                    Case "ｻｰﾋﾞｽ入金"
                        If intKINGAKU >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        End If
                        If intPREMKN >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        End If
                        If intPOINT >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                        End If
                    Case "入場料"
                        Me.dgvTORIHIKI.SetValue("colKBN", i, dr(i).Item("CKBNAME").ToString)
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = intPOINT
                    Case "打ち放題"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = intPOINT
                    Case "コース料金"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = intPOINT
                    Case "ﾎﾟｲﾝﾄ還元"
                        If intKINGAKU >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        End If
                        If intPREMKN >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        End If
                        If intPOINT >= 0 Then
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        Else
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                        End If
                    Case "ﾎﾟｲﾝﾄ還元(ﾁｹｯﾄ)"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                    Case "残高移行"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Blue
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Blue
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Blue
                        If (CType(dr(i).Item("AZANKN").ToString, Integer) + CType(dr(i).Item("APREMKN").ToString, Integer) + CType(dr(i).Item("APOINT").ToString, Integer)).Equals(0) Then
                            dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                            dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                            Me.dgvTORIHIKI.SetValue("colKBN", i, dr(i).Item("KBN").ToString & "(元)")
                        End If
                    Case "球数金額※"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                    Case "有効期限切れ"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        If CType(dr(i).Item("CLRKBN").ToString, Integer).Equals(0) Then
                            dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Blue
                            Me.dgvTORIHIKI.SetValue("colKBN", i, "残高ﾌﾟﾚﾐｱﾑ移行")
                        End If
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = CType("-" & intPOINT, Integer)
                    Case "球数金額"
                        dgvTORIHIKI.Rows(i).Cells("colKINGAKU").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPREMKN").Style.ForeColor = Color.Red
                        dgvTORIHIKI.Rows(i).Cells("colPOINT").Style.ForeColor = Color.Red
                        intKINGAKU = CType("-" & intKINGAKU, Integer)
                        intPREMKN = CType("-" & intPREMKN, Integer)
                        intPOINT = CType("-" & intPOINT, Integer)
                End Select

                '(後)残金
                Me.dgvTORIHIKI.SetValue("colAZANKN", i, CType(dr(i).Item("AZANKN").ToString, Integer).ToString("#,##0"))
                '(後)残ﾌﾟﾚﾐｱﾑ
                Me.dgvTORIHIKI.SetValue("colAPREMKN", i, CType(dr(i).Item("APREMKN").ToString, Integer).ToString("#,##0"))
                '(後)残ﾎﾟｲﾝﾄ
                Me.dgvTORIHIKI.SetValue("colAPOINT", i, CType(dr(i).Item("APOINT").ToString, Integer).ToString("#,##0"))
                '(前)残金
                Me.dgvTORIHIKI.SetValue("colBZANKN", i, CType(dr(i).Item("BZANKN").ToString, Integer).ToString("#,##0"))
                '(前)残ﾌﾟﾚﾐｱﾑ
                Me.dgvTORIHIKI.SetValue("colBPREMKN", i, CType(dr(i).Item("BPREMKN").ToString, Integer).ToString("#,##0"))
                '(前)残ﾎﾟｲﾝﾄ
                Me.dgvTORIHIKI.SetValue("colBPOINT", i, CType(dr(i).Item("BPOINT").ToString, Integer).ToString("#,##0"))

                If i.Equals(_intMaxRowCount - 1) Then
                    Exit For
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class

