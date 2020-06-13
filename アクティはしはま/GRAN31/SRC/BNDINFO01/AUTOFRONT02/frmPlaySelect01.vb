Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmPlaySelect01

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
    ''' プレイセレクト
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property PlaySelect As Integer
        Get
            Return _intPlaySelect
        End Get
    End Property
    Private _intPlaySelect As Integer = 0

    ''' <summary>
    ''' 顧客種別
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property NKBNO As Integer
        Set(value As Integer)
            _intNKBNO = value
        End Set
    End Property
    Private _intNKBNO As Integer = 0

    ''' <summary>
    ''' 所持ポイント
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SRTPO As Integer
        Set(value As Integer)
            _intSRTPO = value
        End Set
    End Property
    Private _intSRTPO As Integer = 0

    ''' <summary>
    ''' 入場料
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ENTKIN() As Integer
        Get
            Return _intENTKIN
        End Get
        Set(ByVal value As Integer)
            _intENTKIN = value
        End Set
    End Property
    Private _intENTKIN As Integer

    ''' <summary>
    ''' 割引額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WARIBIKI() As Integer
        Get
            Return _intWARIBIKI
        End Get
        Set(ByVal value As Integer)
            _intWARIBIKI = value
        End Set
    End Property
    Private _intWARIBIKI As Integer

    ''' <summary>
    ''' 時間区分
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property JKNKB As Integer
        Get
            Return _intJKNKB
        End Get
    End Property
    Private _intJKNKB As Integer = 0

    ''' <summary>
    ''' 貸し時間
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property JKNMM As Integer
        Get
            Return _intJKNMM
        End Get
    End Property
    Private _intJKNMM As Integer = 0

    ''' <summary>
    ''' 指定フロア
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property FLRKB As Integer
        Get
            Return _intFLRKB
        End Get
    End Property
    Private _intFLRKB As Integer = 0

    ''' <summary>
    ''' ポイント
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property POINT As Integer
        Get
            Return _intPOINT
        End Get
    End Property
    Private _intPOINT As Integer = 0

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

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPlaySelect01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            Sound.PlayTouchAnyButton()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 一球打ちボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTama_Click(sender As System.Object, e As System.EventArgs) Handles btnTama.Click
        Try
            If CType(Me.lblZANKN.Text.Replace("円", String.Empty), Integer) < _intENTKIN Then
                Using frm As New frmMSGBOXEx("カード残金が不足しています。", 0)
                    frm.ShowDialog()
                End Using
            End If

            _intPlaySelect = 1

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 打ち放題ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnJKNKB_Click(sender As System.Object, e As System.EventArgs) Handles btnJKNKB01.Click, btnJKNKB02.Click, btnJKNKB03.Click, btnJKNKB04.Click, btnJKNKB05.Click _
                                                                                    , btnJKNKB06.Click, btnJKNKB07.Click, btnJKNKB08.Click, btnJKNKB09.Click, btnJKNKB10.Click
        Try
            Dim btnJKNKB As Button
            btnJKNKB = CType(sender, Button)

            _intPlaySelect = 2
            Select Case btnJKNKB.Name
                Case "btnJKNKB01"
                    _intENTKIN = CType(Me.lblJKNKIN01.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB01.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN01.Tag, Integer)
                    _intPOINT = CType(Me.lblEn01.Tag, Integer)
                    _intJKNKB = 1
                Case "btnJKNKB02"
                    _intENTKIN = CType(Me.lblJKNKIN02.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB02.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN02.Tag, Integer)
                    _intPOINT = CType(Me.lblEn02.Tag, Integer)
                    _intJKNKB = 2
                Case "btnJKNKB03"
                    _intENTKIN = CType(Me.lblJKNKIN03.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB03.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN03.Tag, Integer)
                    _intPOINT = CType(Me.lblEn03.Tag, Integer)
                    _intJKNKB = 3
                Case "btnJKNKB04"
                    _intENTKIN = CType(Me.lblJKNKIN04.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB04.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN04.Tag, Integer)
                    _intPOINT = CType(Me.lblEn04.Tag, Integer)
                    _intJKNKB = 4
                Case "btnJKNKB05"
                    _intENTKIN = CType(Me.lblJKNKIN05.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB05.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN05.Tag, Integer)
                    _intPOINT = CType(Me.lblEn05.Tag, Integer)
                    _intJKNKB = 5
                Case "btnJKNKB06"
                    _intENTKIN = CType(Me.lblJKNKIN06.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB06.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN06.Tag, Integer)
                    _intPOINT = CType(Me.lblEn06.Tag, Integer)
                    _intJKNKB = 6
                Case "btnJKNKB07"
                    _intENTKIN = CType(Me.lblJKNKIN07.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB07.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN07.Tag, Integer)
                    _intPOINT = CType(Me.lblEn07.Tag, Integer)
                    _intJKNKB = 7
                Case "btnJKNKB08"
                    _intENTKIN = CType(Me.lblJKNKIN08.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB08.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN08.Tag, Integer)
                    _intPOINT = CType(Me.lblEn08.Tag, Integer)
                    _intJKNKB = 8
                Case "btnJKNKB09"
                    _intENTKIN = CType(Me.lblJKNKIN09.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB09.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN09.Tag, Integer)
                    _intPOINT = CType(Me.lblEn09.Tag, Integer)
                    _intJKNKB = 9
                Case "btnJKNKB10"
                    _intENTKIN = CType(Me.lblJKNKIN10.Text, Integer)
                    _intJKNMM = CType(Me.btnJKNKB10.Tag, Integer)
                    _intFLRKB = CType(Me.lblJKNKIN10.Tag, Integer)
                    _intPOINT = CType(Me.lblEn10.Tag, Integer)
                    _intJKNKB = 10
            End Select

            If CType(Me.lblZANKN.Text.Replace("円", String.Empty), Integer) < _intENTKIN Then
                Using frm As New frmMSGBOXEx("カード残金が不足しています。", 0)
                    frm.ShowDialog()
                End Using
            End If

            '打ち放題のポイント無し
            _intPOINT = 0

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 戻るボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            frmBase.PushAnimation(CType(sender, Control))
            Me.vbDialogResult = eResult.CANCEL
            Me.Close()
        Catch ex As Exception
            Throw ex
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
            'カード残金
            Me.lblZANKN.Text = (CType(_dcICR700.ZANKN, Integer) + CType(_dcICR700.PREZANKN, Integer)).ToString("#,##0") & "円"
            'ポイント
            Me.lblSRTPO.Text = _intSRTPO.ToString("#,##0") & "Ｐ"

            '一球打ち
            If _intENTKIN > 0 Then
                Me.lblENTKIN.Text = "入場料 " & _intENTKIN.ToString("#,##0") & "円"
                Me.lblENTKIN.Visible = True
            End If

            '打ち放題
            If Not UIUtility.SYSTEM.NOWPASSCD.Equals("00") Then
                Me.btnJKNKB01.Visible = False
                Me.btnJKNKB02.Visible = False
                Me.btnJKNKB03.Visible = False
                Me.btnJKNKB04.Visible = False
                Me.btnJKNKB05.Visible = False
                Me.btnJKNKB06.Visible = False
                Me.btnJKNKB07.Visible = False
                Me.btnJKNKB08.Visible = False
                Me.btnJKNKB09.Visible = False
                Me.btnJKNKB10.Visible = False

                Me.lblJKNKIN01.Visible = False
                Me.lblJKNKIN02.Visible = False
                Me.lblJKNKIN03.Visible = False
                Me.lblJKNKIN04.Visible = False
                Me.lblJKNKIN05.Visible = False
                Me.lblJKNKIN06.Visible = False
                Me.lblJKNKIN07.Visible = False
                Me.lblJKNKIN08.Visible = False
                Me.lblJKNKIN09.Visible = False
                Me.lblJKNKIN10.Visible = False

                Me.lblEn01.Visible = False
                Me.lblEn02.Visible = False
                Me.lblEn03.Visible = False
                Me.lblEn04.Visible = False
                Me.lblEn05.Visible = False
                Me.lblEn06.Visible = False
                Me.lblEn07.Visible = False
                Me.lblEn08.Visible = False
                Me.lblEn09.Visible = False
                Me.lblEn10.Visible = False


                '打ち放題情報取得
                If Not GetEIGMTB() Then
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 打ち放題マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetEIGMTB() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM EIGMTB")
            sql.Append(" WHERE")
            sql.Append(" RKNKB = " & UIUtility.SYSTEM.RKNKB)
            sql.Append(" AND NKBNO = " & _intNKBNO)

            sql.Append(" ORDER BY JKNKB")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim blnENDFLG As Boolean = False
            Dim drEIGMTB() As DataRow

            '時間区分1
            drEIGMTB = resultDt.Select("JKNKB = 1")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB01.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB01.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN01.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN01.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If

                Me.lblJKNKIN01.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn01.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB01.Visible = True
                    Me.lblJKNKIN01.Visible = True
                    Me.lblEn01.Visible = True
                End If
                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB01.Enabled = False
                    Me.lblJKNKIN01.Enabled = False
                    Me.lblEn01.Enabled = False
                End If
                If _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB01.Enabled = True
                    Me.lblJKNKIN01.Enabled = True
                    Me.lblEn01.Enabled = True
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB01.Enabled = False
                    Me.lblJKNKIN01.Enabled = False
                    Me.lblEn01.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分2
            drEIGMTB = resultDt.Select("JKNKB = 2")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB02.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB02.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN02.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN02.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN02.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn02.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB02.Visible = True
                    Me.lblJKNKIN02.Visible = True
                    Me.lblEn02.Visible = True
                End If
                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB02.Enabled = False
                    Me.lblJKNKIN02.Enabled = False
                    Me.lblEn02.Enabled = False
                End If
                If _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB02.Enabled = True
                    Me.lblJKNKIN02.Enabled = True
                    Me.lblEn02.Enabled = True
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB02.Enabled = False
                    Me.lblJKNKIN02.Enabled = False
                    Me.lblEn02.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分3
            drEIGMTB = resultDt.Select("JKNKB = 3")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB03.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB03.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN03.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN03.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN03.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn03.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB03.Visible = True
                    Me.lblJKNKIN03.Visible = True
                    Me.lblEn03.Visible = True
                End If
                '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") Then
                    Me.btnJKNKB03.Enabled = False
                    Me.lblJKNKIN03.Enabled = False
                    Me.lblEn03.Enabled = False
                End If
                If _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.SYUBETU.Equals("C") Then
                    Me.btnJKNKB03.Enabled = True
                    Me.lblJKNKIN03.Enabled = True
                    Me.lblEn03.Enabled = True
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB03.Enabled = False
                    Me.lblJKNKIN03.Enabled = False
                    Me.lblEn03.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分4
            drEIGMTB = resultDt.Select("JKNKB = 4")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB04.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB04.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN04.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN04.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN04.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn04.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB04.Visible = True
                    Me.lblJKNKIN04.Visible = True
                    Me.lblEn04.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB04.Enabled = False
                    Me.lblJKNKIN04.Enabled = False
                    Me.lblEn04.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB04.Enabled = False
                    Me.lblJKNKIN04.Enabled = False
                    Me.lblEn04.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分5
            drEIGMTB = resultDt.Select("JKNKB = 5")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB05.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB05.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN05.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN05.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN05.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn05.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB05.Visible = True
                    Me.lblJKNKIN05.Visible = True
                    Me.lblEn05.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB05.Enabled = False
                    Me.lblJKNKIN05.Enabled = False
                    Me.lblEn05.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB05.Enabled = False
                    Me.lblJKNKIN05.Enabled = False
                    Me.lblEn05.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分6
            drEIGMTB = resultDt.Select("JKNKB = 6")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB06.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB06.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN06.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN06.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN06.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn06.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB06.Visible = True
                    Me.lblJKNKIN06.Visible = True
                    Me.lblEn06.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB06.Enabled = False
                    Me.lblJKNKIN06.Enabled = False
                    Me.lblEn06.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB06.Enabled = False
                    Me.lblJKNKIN06.Enabled = False
                    Me.lblEn06.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分7
            drEIGMTB = resultDt.Select("JKNKB = 7")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB07.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB07.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN07.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN07.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN07.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn07.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB07.Visible = True
                    Me.lblJKNKIN07.Visible = True
                    Me.lblEn07.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB07.Enabled = False
                    Me.lblJKNKIN07.Enabled = False
                    Me.lblEn07.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB07.Enabled = False
                    Me.lblJKNKIN07.Enabled = False
                    Me.lblEn07.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分8
            drEIGMTB = resultDt.Select("JKNKB = 8")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB08.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB08.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN08.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN08.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN08.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn08.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB08.Visible = True
                    Me.lblJKNKIN08.Visible = True
                    Me.lblEn08.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB08.Enabled = False
                    Me.lblJKNKIN08.Enabled = False
                    Me.lblEn08.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB08.Enabled = False
                    Me.lblJKNKIN08.Enabled = False
                    Me.lblEn08.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分9
            drEIGMTB = resultDt.Select("JKNKB = 9")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB09.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB09.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN09.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN09.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN09.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn09.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB09.Visible = True
                    Me.lblJKNKIN09.Visible = True
                    Me.lblEn09.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB09.Enabled = False
                    Me.lblJKNKIN09.Enabled = False
                    Me.lblEn09.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB09.Enabled = False
                    Me.lblJKNKIN09.Enabled = False
                    Me.lblEn09.Enabled = False
                    blnENDFLG = True
                End If
            End If
            '時間区分10
            drEIGMTB = resultDt.Select("JKNKB = 10")
            If drEIGMTB.Length > 0 Then
                Me.btnJKNKB10.Text = drEIGMTB(0).Item("JKNNAME").ToString
                Me.btnJKNKB10.Tag = drEIGMTB(0).Item("JKNMM").ToString
                If drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("0") Then
                    Me.lblJKNKIN10.Text = (CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) - _intWARIBIKI).ToString("#,##0")
                Else
                    Me.lblJKNKIN10.Text = CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer).ToString("#,##0")
                End If
                Me.lblJKNKIN10.Tag = CType(drEIGMTB(0).Item("JKNFLR").ToString, Integer)
                Me.lblEn10.Tag = CType(drEIGMTB(0).Item("POINT").ToString, Integer)
                If CType(drEIGMTB(0).Item("JKNKIN").ToString, Integer) > 0 Then
                    Me.btnJKNKB10.Visible = True
                    Me.lblJKNKIN10.Visible = True
                    Me.lblEn10.Visible = True
                End If
                If Not (drEIGMTB(0).Item("CHARGEFLG").ToString.Equals("1") And _dcICR700.ZENENTDATE.Equals(Now.ToString("yyyyMMdd")) And _dcICR700.NKBNO.Equals("C")) Then
                    '本日受付済みかつ打ち放題ﾌﾟﾚｲ後の場合
                    Me.btnJKNKB10.Enabled = False
                    Me.lblJKNKIN10.Enabled = False
                    Me.lblEn10.Enabled = False
                End If
                '終了時間確認
                If drEIGMTB(0).Item("ENDTIME").ToString < Now.ToString("HHmm") Then
                    Me.btnJKNKB10.Enabled = False
                    Me.lblJKNKIN10.Enabled = False
                    Me.lblEn10.Enabled = False
                    blnENDFLG = True
                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

#End Region



End Class