Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmSALES01

#Region "▼宣言部"
    ''' <summary>
    ''' データベース制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _iDatabase As IDatabase.IMethod
    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcICR700 As New Techno.DeviceControls.ICR700Control
    ''' <summary>
    ''' レシートプリンタコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcSK121 As New Techno.DeviceControls.SK121Control

#End Region


#Region "▼プロパティ"

    ''' <summary>
    ''' データベース制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property iDatabase As IDatabase.IMethod
        Set(ByVal value As IDatabase.IMethod)
            _iDatabase = value
        End Set
    End Property
    ''' <summary>
    ''' ICR700リーダライター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ICR700 As Techno.DeviceControls.ICR700Control
        Set(ByVal value As Techno.DeviceControls.ICR700Control)
            _dcICR700 = value
        End Set
    End Property
    ''' <summary>
    ''' レシートプリンター制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property SK121 As Techno.DeviceControls.SK121Control
        Get
            Return _dcSK121
        End Get
        Set(ByVal value As Techno.DeviceControls.SK121Control)
            _dcSK121 = value
        End Set
    End Property

#End Region

#Region "▼イベント定義"


    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSALES_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

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

    ''' <summary>
    ''' 開始年アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpStaYear_Click(sender As System.Object, e As System.EventArgs) Handles btnUpStaYear.Click
        Try
            If CType(Me.lblStaYear.Text, Integer).Equals(9999) Then Exit Sub

            Me.lblStaYear.Text = (CType(Me.lblStaYear.Text, Integer) + 1).ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 開始年ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownStaYear_Click(sender As System.Object, e As System.EventArgs) Handles btnDownStaYear.Click
        Try
            If CType(Me.lblStaYear.Text, Integer).Equals(0) Then Exit Sub

            Me.lblStaYear.Text = (CType(Me.lblStaYear.Text, Integer) - 1).ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 開始月アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpStaMonth_Click(sender As System.Object, e As System.EventArgs) Handles btnUpStaMonth.Click
        Try
            If CType(Me.lblStaMonth.Text, Integer).Equals(12) Then Me.lblStaMonth.Text = "0"

            Me.lblStaMonth.Text = (CType(Me.lblStaMonth.Text, Integer) + 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 開始月ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownStaMonth_Click(sender As System.Object, e As System.EventArgs) Handles btnDownStaMonth.Click
        Try
            If CType(Me.lblStaMonth.Text, Integer).Equals(1) Then Me.lblStaMonth.Text = "13"

            Me.lblStaMonth.Text = (CType(Me.lblStaMonth.Text, Integer) - 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 開始日アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpStaDay_Click(sender As System.Object, e As System.EventArgs) Handles btnUpStaDay.Click
        Try
            If CType(Me.lblStaDay.Text, Integer).Equals(31) Then Me.lblStaDay.Text = "0"

            Me.lblStaDay.Text = (CType(Me.lblStaDay.Text, Integer) + 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 開始日ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownStaDay_Click(sender As System.Object, e As System.EventArgs) Handles btnDownStaDay.Click
        Try
            If CType(Me.lblStaDay.Text, Integer).Equals(1) Then Me.lblStaDay.Text = "32"

            Me.lblStaDay.Text = (CType(Me.lblStaDay.Text, Integer) - 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 終了年アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpEndYear_Click(sender As System.Object, e As System.EventArgs) Handles btnUpEndYear.Click
        Try
            If CType(Me.lblEndYear.Text, Integer).Equals(9999) Then Exit Sub

            Me.lblEndYear.Text = (CType(Me.lblEndYear.Text, Integer) + 1).ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 終了年ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownEndYear_Click(sender As System.Object, e As System.EventArgs) Handles btnDownEndYear.Click
        Try
            If CType(Me.lblEndYear.Text, Integer).Equals(0) Then Exit Sub

            Me.lblEndYear.Text = (CType(Me.lblEndYear.Text, Integer) - 1).ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 終了月アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpEndMonth_Click(sender As System.Object, e As System.EventArgs) Handles btnUpEndMonth.Click
        Try
            If CType(Me.lblEndMonth.Text, Integer).Equals(12) Then Me.lblEndMonth.Text = "0"

            Me.lblEndMonth.Text = (CType(Me.lblEndMonth.Text, Integer) + 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 終了月ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownMonth_Click(sender As System.Object, e As System.EventArgs) Handles btnDownEndMonth.Click
        Try
            If CType(Me.lblEndMonth.Text, Integer).Equals(1) Then Me.lblEndMonth.Text = "13"

            Me.lblEndMonth.Text = (CType(Me.lblEndMonth.Text, Integer) - 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 終了日アップボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpEndDay_Click(sender As System.Object, e As System.EventArgs) Handles btnUpEndDay.Click
        Try
            If CType(Me.lblEndDay.Text, Integer).Equals(31) Then Me.lblEndDay.Text = "0"

            Me.lblEndDay.Text = (CType(Me.lblEndDay.Text, Integer) + 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' 終了日ダウンボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDownEndDay_Click(sender As System.Object, e As System.EventArgs) Handles btnDownEndDay.Click
        Try
            If CType(Me.lblEndDay.Text, Integer).Equals(1) Then Me.lblEndDay.Text = "32"

            Me.lblEndDay.Text = (CType(Me.lblEndDay.Text, Integer) - 1).ToString.PadLeft(2, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 印刷ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            If Not Print() Then
                Using frm As New frmMSGBOX01("売上印刷に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
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
            Me.lblStaYear.Text = Now.Year.ToString
            Me.lblStaMonth.Text = Now.Month.ToString.PadLeft(2, "0"c)
            Me.lblStaDay.Text = Now.Day.ToString.PadLeft(2, "0"c)

            Me.lblEndYear.Text = Now.Year.ToString
            Me.lblEndMonth.Text = Now.Month.ToString.PadLeft(2, "0"c)
            Me.lblEndDay.Text = Now.Day.ToString.PadLeft(2, "0"c)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print() As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strStaCHARGEDAY As String = String.Empty
        Dim strEndCHARGEDAY As String = String.Empty
        Try
            strStaCHARGEDAY = Me.lblStaYear.Text & "/" & Me.lblStaMonth.Text & "/" & Me.lblStaDay.Text
            strEndCHARGEDAY = Me.lblEndYear.Text & "/" & Me.lblEndMonth.Text & "/" & Me.lblEndDay.Text


            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(HAKKOKAISU) IS NULL THEN 0 ELSE SUM(HAKKOKAISU) END AS HAKKOKAISU")                          'カード発行回数
            sql.Append(",CASE WHEN SUM(HAKKOGOKEIKIN) IS NULL THEN 0 ELSE SUM(HAKKOGOKEIKIN) END AS HAKKOGOKEIKIN")                 'カード発行合計金額
            sql.Append(",CASE WHEN CEILING(AVG(CHARGE1KIN)) IS NULL THEN 0 ELSE CEILING(AVG(CHARGE1KIN)) END AS CHARGE1KIN")        '入金ボタン１金額
            sql.Append(",CASE WHEN SUM(CHARGE1KAISU) IS NULL THEN 0 ELSE SUM(CHARGE1KAISU) END AS CHARGE1KAISU")                    '入金ボタン１回数
            sql.Append(",CASE WHEN SUM(CHARGE1GOKEIKIN) IS NULl THEN 0 ELSE SUM(CHARGE1GOKEIKIN) END AS CHARGE1GOKEIKIN")           '入金ボタン１合計金額
            sql.Append(",CASE WHEN CEILING(AVG(CHARGE2KIN)) IS NULL THEN 0 ELSE CEILING(AVG(CHARGE2KIN)) END AS CHARGE2KIN")        '入金ボタン２金額
            sql.Append(",CASE WHEN SUM(CHARGE2KAISU) IS NULL THEN 0 ELSE SUM(CHARGE2KAISU) END AS CHARGE2KAISU")                    '入金ボタン２回数
            sql.Append(",CASE WHEN SUM(CHARGE2GOKEIKIN) IS NULL THEN 0 ELSE SUM(CHARGE2GOKEIKIN) END AS CHARGE2GOKEIKIN")           '入金ボタン２合計金額
            sql.Append(",CASE WHEN CEILING(AVG(CHARGE3KIN)) IS NULL THEN 0 ELSE CEILING(AVG(CHARGE3KIN)) END AS CHARGE3KIN")        '入金ボタン３金額
            sql.Append(",CASE WHEN SUM(CHARGE3KAISU) IS NULL THEN 0 ELSE SUM(CHARGE3KAISU) END AS CHARGE3KAISU")                    '入金ボタン３回数
            sql.Append(",CASE WHEN SUM(CHARGE3GOKEIKIN) IS NULL THEN 0 ELSE SUM(CHARGE3GOKEIKIN) END AS CHARGE3GOKEIKIN")           '入金ボタン３合計金額
            sql.Append(",CASE WHEN CEILING(AVG(CHARGE4KIN)) IS NULL THEN 0 ELSE CEILING(AVG(CHARGE4KIN)) END AS CHARGE4KIN")        '入金ボタン４金額
            sql.Append(",CASE WHEN SUM(CHARGE4KAISU) IS NULL THEN 0 ELSE SUM(CHARGE4KAISU) END AS CHARGE4KAISU")                    '入金ボタン４回数
            sql.Append(",CASE WHEN SUM(CHARGE4GOKEIKIN) IS NULL THEN 0 ELSE SUM(CHARGE4GOKEIKIN) END AS CHARGE4GOKEIKIN")           '入金ボタン４合計金額
            sql.Append(",CASE WHEN CEILING(AVG(CHARGE5KIN)) IS NULL THEN 0 ELSE CEILING(AVG(CHARGE5KIN)) END AS CHARGE5KIN")        '入金ボタン５金額
            sql.Append(",CASE WHEN SUM(CHARGE5KAISU) IS NULL THEN 0 ELSE SUM(CHARGE5KAISU) END AS CHARGE5KAISU")                    '入金ボタン５回数
            sql.Append(",CASE WHEN SUM(CHARGE5GOKEIKIN) IS NULL THEN 0 ELSE SUM(CHARGE5GOKEIKIN) END AS CHARGE5GOKEIKIN")           '入金ボタン５合計金額
            sql.Append(",CASE WHEN CEILING(AVG(CHARGE6KIN)) IS NULL THEN 0 ELSE CEILING(AVG(CHARGE6KIN)) END AS CHARGE6KIN")        '入金ボタン６金額
            sql.Append(",CASE WHEN SUM(CHARGE6KAISU) IS NULL THEN 0 ELSE SUM(CHARGE6KAISU) END AS CHARGE6KAISU")                    '入金ボタン６回数
            sql.Append(",CASE WHEN SUM(CHARGE6GOKEIKIN) IS NULL THEN 0 ELSE SUM(CHARGE6GOKEIKIN) END AS CHARGE6GOKEIKIN")           '入金ボタン６合計金額
            sql.Append(",CASE WHEN SUM(SHITEIKAISU) IS NULL THEN 0 ELSE SUM(SHITEIKAISU) END AS SHITEIKAISU")                       '金額指定回数
            sql.Append(",CASE WHEN SUM(SHITEIGOKEIKIN) IS NULL THEN 0 ELSE SUM(SHITEIGOKEIKIN) END AS SHITEIGOKEIKIN")              '金額指定合計金額
            sql.Append(",CASE WHEN SUM(SENENINKAISU) IS NULL THEN 0 ELSE SUM(SENENINKAISU) END AS SENENINKAISU")                    '千円札投入回数
            sql.Append(",CASE WHEN SUM(NISENENINKAISU) IS NULL THEN 0 ELSE SUM(NISENENINKAISU) END AS NISENENINKAISU")              '二千円札投入回数
            sql.Append(",CASE WHEN SUM(GOSENENINKAISU) IS NULL THEN 0 ELSE SUM(GOSENENINKAISU) END AS GOSENENINKAISU")              '五千円札投入回数
            sql.Append(",CASE WHEN SUM(ICHIMANENINKAISU) IS NULL THEN 0 ELSE SUM(ICHIMANENINKAISU) END AS ICHIMANENINKAISU")        '一万円札投入回数
            sql.Append(",CASE WHEN SUM(SENENOUTKAISU) IS NULL THEN 0 ELSE SUM(SENENOUTKAISU) END AS SENENOUTKAISU")                 '千円札払出回数
            sql.Append(",CASE WHEN SUM(GOSENENOUTKAISU) IS NULL THEN 0 ELSE SUM(GOSENENOUTKAISU) END AS GOSENENOUTKAISU")           '五千円札払出回数
            sql.Append(",CASE WHEN SUM(JYUENINKAISU) IS NULL THEN 0 ELSE SUM(JYUENINKAISU) END AS JYUENINKAISU")                    '十円投入回数
            sql.Append(",CASE WHEN SUM(GOJYUENINKAISU) IS NULL THEN 0 ELSE SUM(GOJYUENINKAISU) END AS GOJYUENINKAISU")              '五十円投入回数
            sql.Append(",CASE WHEN SUM(HYAKUENINKAISU) IS NULL THEN 0 ELSE SUM(HYAKUENINKAISU) END AS HYAKUENINKAISU")              '百円投入回数
            sql.Append(",CASE WHEN SUM(GOHYAKUENINKAISU) IS NULL THEN 0 ELSE SUM(GOHYAKUENINKAISU) END AS GOHYAKUENINKAISU")        '五百円投入回数
            sql.Append(",CASE WHEN SUM(JYUENOUTKAISU) IS NULL THEN 0 ELSE SUM(JYUENOUTKAISU) END AS JYUENOUTKAISU")                 '十円払出回数
            sql.Append(",CASE WHEN SUM(GOJYUENOUTKAISU) IS NULL THEN 0 ELSE SUM(GOJYUENOUTKAISU) END AS GOJYUENOUTKAISU")           '五十円払出回数
            sql.Append(",CASE WHEN SUM(HYAKUENOUTKAISU) IS NULL THEN 0 ELSE SUM(HYAKUENOUTKAISU) END AS HYAKUENOUTKAISU")           '百円払出回数
            sql.Append(",CASE WHEN SUM(GOHYAKUENOUTKAISU) IS NULL THEN 0 ELSE SUM(GOHYAKUENOUTKAISU) END AS GOHYAKUENOUTKAISU")     '五百円払出回数
            sql.Append(" FROM REPOCHARGE_M")
            sql.Append(" WHERE")
            sql.Append(" HOSTNAME = '" & Net.Dns.GetHostName & "'")
            sql.Append(" AND NKNKBN = 0 ")
            sql.Append(" AND (CHARGEDAY >= '" & strStaCHARGEDAY.Replace("/", String.Empty) & "'")
            sql.Append(" AND CHARGEDAY <= '" & strEndCHARGEDAY.Replace("/", String.Empty) & "')")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            ' 印刷日
            _dcSK121.PrintDate = DateTime.Now

            ' 営業日
            _dcSK121.SaleDateStart = DateTime.Parse(strStaCHARGEDAY)
            _dcSK121.SaleDateEnd = DateTime.Parse(strEndCHARGEDAY)

            ' カード発行回数
            _dcSK121.CardIssuingNumber = CType(resultDt.Rows(0).Item("HAKKOKAISU").ToString, Integer)
            ' カード発行合計金額
            _dcSK121.CardIssuingKingaku = CType(resultDt.Rows(0).Item("HAKKOGOKEIKIN").ToString, Integer)

            ' 入金履歴の追加
            ' 1.PaymentHistoryModelのインスタンスを作成し、各プロパティに値を設定
            ' 2.インスタンスをリストPaymentHistorysにAdd追加することで入金履歴データを追加
            Dim paymentHistorys = New List(Of Techno.DeviceControls.PaymentHistoryModel)
            Dim model = New Techno.DeviceControls.PaymentHistoryModel
            Dim prices() As Integer = {CType(resultDt.Rows(0).Item("CHARGE1KIN").ToString, Integer) _
                                     , CType(resultDt.Rows(0).Item("CHARGE2KIN").ToString, Integer) _
                                     , CType(resultDt.Rows(0).Item("CHARGE3KIN").ToString, Integer) _
                                     , CType(resultDt.Rows(0).Item("CHARGE4KIN").ToString, Integer) _
                                     , CType(resultDt.Rows(0).Item("CHARGE5KIN").ToString, Integer) _
                                     , CType(resultDt.Rows(0).Item("CHARGE6KIN").ToString, Integer) _
                                      , 99999}

            For Each price In prices
                model = New Techno.DeviceControls.PaymentHistoryModel
                model.Price = price
                Select Case price
                    Case CType(resultDt.Rows(0).Item("CHARGE1KIN").ToString, Integer)
                        model.Number = CType(resultDt.Rows(0).Item("CHARGE1KAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("CHARGE1GOKEIKIN").ToString, Integer)
                    Case CType(resultDt.Rows(0).Item("CHARGE2KIN").ToString, Integer)
                        model.Number = CType(resultDt.Rows(0).Item("CHARGE2KAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("CHARGE2GOKEIKIN").ToString, Integer)
                    Case CType(resultDt.Rows(0).Item("CHARGE3KIN").ToString, Integer)
                        model.Number = CType(resultDt.Rows(0).Item("CHARGE3KAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("CHARGE3GOKEIKIN").ToString, Integer)
                    Case CType(resultDt.Rows(0).Item("CHARGE4KIN").ToString, Integer)
                        model.Number = CType(resultDt.Rows(0).Item("CHARGE4KAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("CHARGE4GOKEIKIN").ToString, Integer)
                    Case CType(resultDt.Rows(0).Item("CHARGE5KIN").ToString, Integer)
                        model.Number = CType(resultDt.Rows(0).Item("CHARGE5KAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("CHARGE5GOKEIKIN").ToString, Integer)
                    Case CType(resultDt.Rows(0).Item("CHARGE6KIN").ToString, Integer)
                        model.Number = CType(resultDt.Rows(0).Item("CHARGE6KAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("CHARGE6GOKEIKIN").ToString, Integer)
                    Case Else
                        model.Number = CType(resultDt.Rows(0).Item("SHITEIKAISU").ToString, Integer)
                        model.TotalPrice = CType(resultDt.Rows(0).Item("SHITEIGOKEIKIN").ToString, Integer)
                End Select
                paymentHistorys.Add(model)
            Next
            _dcSK121.PaymentHistorys = paymentHistorys

            ' 紙幣投入履歴
            ' 各紙幣に対応した末尾のBillEntryNumberプロパティに値を設定(10000, 5000, 2000, 1000)
            _dcSK121.BillEntryNumber10000 = CType(resultDt.Rows(0).Item("ICHIMANENINKAISU").ToString, Integer)
            _dcSK121.BillEntryNumber5000 = CType(resultDt.Rows(0).Item("GOSENENINKAISU").ToString, Integer)
            _dcSK121.BillEntryNumber2000 = CType(resultDt.Rows(0).Item("NISENENINKAISU").ToString, Integer)
            _dcSK121.BillEntryNumber1000 = CType(resultDt.Rows(0).Item("SENENINKAISU").ToString, Integer)

            '硬貨投入履歴
            _dcSK121.CmEntryNumber10 = CType(resultDt.Rows(0).Item("JYUENINKAISU").ToString, Integer)
            _dcSK121.CmEntryNumber50 = CType(resultDt.Rows(0).Item("GOJYUENINKAISU").ToString, Integer)
            _dcSK121.CmEntryNumber100 = CType(resultDt.Rows(0).Item("HYAKUENINKAISU").ToString, Integer)
            _dcSK121.CmEntryNumber500 = CType(resultDt.Rows(0).Item("GOHYAKUENINKAISU").ToString, Integer)

            ' 紙幣払出履歴
            ' 各紙幣に対応した末尾のBillEntryNumberプロパティに値を設定(5000, 1000)
            _dcSK121.BillPayoutNumber5000 = CType(resultDt.Rows(0).Item("GOSENENOUTKAISU").ToString, Integer)
            _dcSK121.BillPayoutNumber1000 = CType(resultDt.Rows(0).Item("SENENOUTKAISU").ToString, Integer)

            '硬貨払出履歴
            _dcSK121.CmPayoutNumber10 = CType(resultDt.Rows(0).Item("JYUENOUTKAISU").ToString, Integer)
            _dcSK121.CmPayoutNumber50 = CType(resultDt.Rows(0).Item("GOJYUENOUTKAISU").ToString, Integer)
            _dcSK121.CmPayoutNumber100 = CType(resultDt.Rows(0).Item("HYAKUENOUTKAISU").ToString, Integer)
            _dcSK121.CmPayoutNumber500 = CType(resultDt.Rows(0).Item("GOHYAKUENOUTKAISU").ToString, Integer)

            ' 売上日報の印刷
            If Not _dcSK121.PrintReport() Then
                Return False
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region




End Class