Imports TECHNO.DataBase

Public Class frmBNDINFO01

#Region "▼宣言部"

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ﾍﾞﾝﾀﾞｰ情報"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "ﾍﾞﾝﾀﾞｰ情報"

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
    Private Sub frmBNDINFO01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()

            '更新ボタンクリック
            Me.btnUpd.PerformClick()


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 更新ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpd_Click(sender As System.Object, e As System.EventArgs) Handles btnUpd.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            '利用回数
            Me.lblCount1.Text = GetCount("01").ToString("#,##0")
            Me.lblCount2.Text = GetCount("02").ToString("#,##0")
            Me.lblCount3.Text = GetCount("03").ToString("#,##0")
            Me.lblCount4.Text = GetCount("04").ToString("#,##0")
            Me.lblCount5.Text = GetCount("05").ToString("#,##0")
            Me.lblCount6.Text = GetCount("06").ToString("#,##0")

            Me.lblCountGokei.Text = (CType(Me.lblCount1.Text, Integer) + CType(Me.lblCount2.Text, Integer) + CType(Me.lblCount3.Text, Integer) _
                                + CType(Me.lblCount4.Text, Integer) + CType(Me.lblCount5.Text, Integer) + CType(Me.lblCount6.Text, Integer)).ToString("#,##0")

            '金額計
            Me.lblKin1.Text = GetKin("01").ToString("#,##0")
            Me.lblKin2.Text = GetKin("02").ToString("#,##0")
            Me.lblKin3.Text = GetKin("03").ToString("#,##0")
            Me.lblKin4.Text = GetKin("04").ToString("#,##0")
            Me.lblKin5.Text = GetKin("05").ToString("#,##0")
            Me.lblKin6.Text = GetKin("06").ToString("#,##0")

            Me.lblKinGokei.Text = (CType(Me.lblKin1.Text, Integer) + CType(Me.lblKin2.Text, Integer) + CType(Me.lblKin3.Text, Integer) _
                                + CType(Me.lblKin4.Text, Integer) + CType(Me.lblKin5.Text, Integer) + CType(Me.lblKin6.Text, Integer)).ToString("#,##0")

            '内税計
            Me.lblZei1.Text = GetZei("01").ToString("#,##0")
            Me.lblZei2.Text = GetZei("02").ToString("#,##0")
            Me.lblZei3.Text = GetZei("03").ToString("#,##0")
            Me.lblZei4.Text = GetZei("04").ToString("#,##0")
            Me.lblZei5.Text = GetZei("05").ToString("#,##0")
            Me.lblZei6.Text = GetZei("06").ToString("#,##0")

            Me.lblZeiGokei.Text = (CType(Me.lblZei1.Text, Integer) + CType(Me.lblZei2.Text, Integer) + CType(Me.lblZei3.Text, Integer) _
                             + CType(Me.lblZei4.Text, Integer) + CType(Me.lblZei5.Text, Integer) + CType(Me.lblZei6.Text, Integer)).ToString("#,##0")

            'カゴ球数計
            Me.lblKago1.Text = GetKago("01").ToString("#,##0")
            Me.lblKago2.Text = GetKago("02").ToString("#,##0")
            Me.lblKago3.Text = GetKago("03").ToString("#,##0")
            Me.lblKago4.Text = GetKago("04").ToString("#,##0")
            Me.lblKago5.Text = GetKago("05").ToString("#,##0")
            Me.lblKago6.Text = GetKago("06").ToString("#,##0")

            Me.lblKagoGokei.Text = (CType(Me.lblKago1.Text, Integer) + CType(Me.lblKago2.Text, Integer) + CType(Me.lblKago3.Text, Integer) _
                                + CType(Me.lblKago4.Text, Integer) + CType(Me.lblKago5.Text, Integer) + CType(Me.lblKago6.Text, Integer)).ToString("#,##0")

            '打放球数計
            Me.lblTime1.Text = GetTime("01").ToString("#,##0")
            Me.lblTime2.Text = GetTime("02").ToString("#,##0")
            Me.lblTime3.Text = GetTime("03").ToString("#,##0")
            Me.lblTime4.Text = GetTime("04").ToString("#,##0")
            Me.lblTime5.Text = GetTime("05").ToString("#,##0")
            Me.lblTime6.Text = GetTime("06").ToString("#,##0")

            Me.lblTimeGokei.Text = (CType(Me.lblTime1.Text, Integer) + CType(Me.lblTime2.Text, Integer) + CType(Me.lblTime3.Text, Integer) _
                                 + CType(Me.lblTime4.Text, Integer) + CType(Me.lblTime5.Text, Integer) + CType(Me.lblTime6.Text, Integer)).ToString("#,##0")

            'メンテ球数計
            Me.lblMente1.Text = GetMente("01").ToString("#,##0")
            Me.lblMente2.Text = GetMente("02").ToString("#,##0")
            Me.lblMente3.Text = GetMente("03").ToString("#,##0")
            Me.lblMente4.Text = GetMente("04").ToString("#,##0")
            Me.lblMente5.Text = GetMente("05").ToString("#,##0")
            Me.lblMente6.Text = GetMente("06").ToString("#,##0")

            Me.lblMenteGokei.Text = (CType(Me.lblMente1.Text, Integer) + CType(Me.lblMente2.Text, Integer) + CType(Me.lblMente3.Text, Integer) _
                               + CType(Me.lblMente4.Text, Integer) + CType(Me.lblMente5.Text, Integer) + CType(Me.lblMente6.Text, Integer)).ToString("#,##0")

            '総球数計
            Me.lblTama1.Text = (CType(Me.lblKago1.Text, Integer) + CType(Me.lblTime1.Text, Integer) + CType(Me.lblMente1.Text, Integer)).ToString("#,##0")
            Me.lblTama2.Text = (CType(Me.lblKago2.Text, Integer) + CType(Me.lblTime2.Text, Integer) + CType(Me.lblMente2.Text, Integer)).ToString("#,##0")
            Me.lblTama3.Text = (CType(Me.lblKago3.Text, Integer) + CType(Me.lblTime3.Text, Integer) + CType(Me.lblMente3.Text, Integer)).ToString("#,##0")
            Me.lblTama4.Text = (CType(Me.lblKago4.Text, Integer) + CType(Me.lblTime4.Text, Integer) + CType(Me.lblMente4.Text, Integer)).ToString("#,##0")
            Me.lblTama5.Text = (CType(Me.lblKago5.Text, Integer) + CType(Me.lblTime5.Text, Integer) + CType(Me.lblMente5.Text, Integer)).ToString("#,##0")
            Me.lblTama6.Text = (CType(Me.lblKago6.Text, Integer) + CType(Me.lblTime6.Text, Integer) + CType(Me.lblMente6.Text, Integer)).ToString("#,##0")

            Me.lblTamaGokei.Text = (CType(Me.lblTama1.Text, Integer) + CType(Me.lblTama2.Text, Integer) + CType(Me.lblTama3.Text, Integer) _
                                + CType(Me.lblTama4.Text, Integer) + CType(Me.lblTama5.Text, Integer) + CType(Me.lblTama6.Text, Integer)).ToString("#,##0")


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
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

            '利用回数
            Me.lblCount1.Text = "0"
            Me.lblCount2.Text = "0"
            Me.lblCount3.Text = "0"
            Me.lblCount4.Text = "0"
            Me.lblCount5.Text = "0"
            Me.lblCount6.Text = "0"
            Me.lblCountGokei.Text = "0"

            '金額計
            Me.lblKin1.Text = "0"
            Me.lblKin2.Text = "0"
            Me.lblKin3.Text = "0"
            Me.lblKin4.Text = "0"
            Me.lblKin5.Text = "0"
            Me.lblKin6.Text = "0"
            Me.lblKinGokei.Text = "0"

            '内税計
            Me.lblZei1.Text = "0"
            Me.lblZei2.Text = "0"
            Me.lblZei3.Text = "0"
            Me.lblZei4.Text = "0"
            Me.lblZei5.Text = "0"
            Me.lblZei6.Text = "0"
            Me.lblZeiGokei.Text = "0"

            'カゴ球数計
            Me.lblKago1.Text = "0"
            Me.lblKago2.Text = "0"
            Me.lblKago3.Text = "0"
            Me.lblKago4.Text = "0"
            Me.lblKago5.Text = "0"
            Me.lblKago6.Text = "0"
            Me.lblKagoGokei.Text = "0"

            '打放球数計
            Me.lblTime1.Text = "0"
            Me.lblTime2.Text = "0"
            Me.lblTime3.Text = "0"
            Me.lblTime4.Text = "0"
            Me.lblTime5.Text = "0"
            Me.lblTime6.Text = "0"
            Me.lblTimeGokei.Text = "0"

            'メンテ球数計
            Me.lblMente1.Text = "0"
            Me.lblMente2.Text = "0"
            Me.lblMente3.Text = "0"
            Me.lblMente4.Text = "0"
            Me.lblMente5.Text = "0"
            Me.lblMente6.Text = "0"
            Me.lblMenteGokei.Text = "0"

            '総球数計
            Me.lblTama1.Text = "0"
            Me.lblTama2.Text = "0"
            Me.lblTama3.Text = "0"
            Me.lblTama4.Text = "0"
            Me.lblTama5.Text = "0"
            Me.lblTama6.Text = "0"
            Me.lblTamaGokei.Text = "0"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 利用回数取得
    ''' </summary>
    ''' <param name="strBNDNO"></param>
    ''' <remarks></remarks>
    Private Function GetCount(ByVal strBNDNO As String) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" COUNT(*) AS CNT")
            sql.Append(" FROM SRTTRA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND BNDNO = '" & strBNDNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("CNT"), Integer)

        Catch ex As Exception
            Return 0
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 金額計取得
    ''' </summary>
    ''' <param name="strBNDNO"></param>
    ''' <remarks></remarks>
    Private Function GetKin(ByVal strBNDNO As String) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" CASE WHEN SUM(KAGOAKN) IS NULL THEN 0 ELSE SUM(KAGOAKN) END AS KIN")
            sql.Append(" FROM SRTTRA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND BNDNO = '" & strBNDNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("KIN"), Integer)

        Catch ex As Exception
            Return 0
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 内税計取得
    ''' </summary>
    ''' <param name="strBNDNO"></param>
    ''' <remarks></remarks>
    Private Function GetZei(ByVal strBNDNO As String) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" CASE WHEN SUM(KAGOZEI) IS NULL THEN 0 ELSE SUM(KAGOZEI) END AS ZEI")
            sql.Append(" FROM SRTTRA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND BNDNO = '" & strBNDNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("ZEI"), Integer)

        Catch ex As Exception
            Return 0
        Finally
        End Try

    End Function

    ''' <summary>
    ''' カゴ球数計取得
    ''' </summary>
    ''' <param name="strBNDNO"></param>
    ''' <remarks></remarks>
    Private Function GetKago(ByVal strBNDNO As String) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" CASE WHEN SUM(BALLSU) IS NULL THEN 0 ELSE SUM(BALLSU) END AS KAGO")
            sql.Append(" FROM SRTTRA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND BNDNO = '" & strBNDNO & "'")
            sql.Append(" AND (KSBKB = '1' OR KSBKB = '2' OR KSBKB = '3' OR KSBKB = '4' OR KSBKB = '5' OR KSBKB = '6' OR KSBKB = '7' OR KSBKB = '8' OR KSBKB = '9' OR KSBKB = '10'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("KAGO"), Integer)

        Catch ex As Exception
            Return 0
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 打放球数計取得
    ''' </summary>
    ''' <param name="strBNDNO"></param>
    ''' <remarks></remarks>
    Private Function GetTime(ByVal strBNDNO As String) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" CASE WHEN SUM(BALLSU) IS NULL THEN 0 ELSE SUM(BALLSU) END AS TIME")
            sql.Append(" FROM SRTTRA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND BNDNO = '" & strBNDNO & "'")
            sql.Append(" AND KSBKB = '15'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("TIME"), Integer)

        Catch ex As Exception
            Return 0
        Finally
        End Try

    End Function

    ''' <summary>
    ''' メンテ球数計取得
    ''' </summary>
    ''' <param name="strBNDNO"></param>
    ''' <remarks></remarks>
    Private Function GetMente(ByVal strBNDNO As String) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" CASE WHEN SUM(BALLSU) IS NULL THEN 0 ELSE SUM(BALLSU) END AS MENTE")
            sql.Append(" FROM SRTTRA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" UDNDT = '" & UIUtility.SYSTEM.UPDDAY & "'")
            sql.Append(" AND BNDNO = '" & strBNDNO & "'")
            sql.Append(" AND KSBKB = '14'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return CType(resultDt.Rows(0).Item("MENTE"), Integer)

        Catch ex As Exception
            Return 0
        Finally
        End Try

    End Function

#End Region


End Class
