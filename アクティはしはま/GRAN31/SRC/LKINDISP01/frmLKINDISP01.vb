Imports TECHNO.DataBase

Public Class frmLKINDISP01

#Region "▼宣言部"

#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "カード有効期限切れ確認画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod, ByVal ICR700 As TECHNO.DeviceControls.ICR700Control)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "カード有効期限切れ確認画面"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            'データベース接続情報を引き継ぐ
            iDatabase = iDB

            dcICR700 = ICR700

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 残金
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ZANKN As Integer
        Set(value As Integer)
            _intZANKN = value
        End Set
    End Property
    Private _intZANKN As Integer

    ''' <summary>
    ''' P)残金
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PREZANKN As Integer
        Set(value As Integer)
            _intPREZANKN = value
        End Set
    End Property
    Private _intPREZANKN As Integer

    ''' <summary>
    ''' 残ポイント
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SRTPO As Integer
        Set(value As Integer)
            _intSRTPO = value
        End Set
    End Property
    Private _intSRTPO As Integer

    ''' <summary>
    ''' カード有効期限
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CARDLIMIT As String
        Set(value As String)
            _strCARDLIMIT = value
        End Set
    End Property
    Private _strCARDLIMIT As String = String.Empty

    ''' <summary>
    ''' 入金残高有効期限
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PREMLIMIT As String
        Set(value As String)
            _strPREMLIMIT = value
        End Set
    End Property
    Private _strPREMLIMIT As String = String.Empty

    ''' <summary>
    ''' クリア区分
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CLRKBN As Integer
        Set(value As Integer)
            _intCLRKBN = value
        End Set
    End Property
    Private _intCLRKBN As Integer = 0

    ''' <summary>
    ''' 確認メッセージ返答
    ''' </summary>
    ''' <value>【OK】True【キャンセル】False</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Reply() As Boolean
        Get
            Return _blnReply
        End Get
    End Property
    Private _blnReply As Boolean = True

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmLKINDISP01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' OKボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Try
            _blnReply = True

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' キャンセルボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            _blnReply = False

            Me.Close()

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
            '閉じるボタン
            Me.tspFunc1.Enabled = False
            '一覧
            Me.tspFunc3.Enabled = False
            '印刷
            Me.tspFunc8.Enabled = False
            '削除
            Me.tspFunc9.Enabled = False
            '画面印刷
            Me.tspFunc10.Enabled = False
            'クリア
            Me.tspFunc11.Enabled = False
            '登録
            Me.tspFunc12.Enabled = False

            'カード有効期限
            Me.lblCARDLIMIT.Text = _strCARDLIMIT
            '入金残高有効期限
            Me.lblPREMLIMIT.Text = _strPREMLIMIT
            '残金
            Me.lblZANKN.Text = _intZANKN.ToString("#,##0")
            'P)残金
            Me.lblPREZANKN.Text = _intPREZANKN.ToString("#,##0")
            '残ポイント
            Me.lblSRTPO.Text = _intSRTPO.ToString("#,##0")

            If _intCLRKBN.Equals(1) Then
                Me.Panel1.Visible = False
            Else
                Me.Label1.Text = "入金残高の有効期限が切れています。入金残高をﾌﾟﾚﾐｱﾑに移行します。"
                Me.lblZANKN2.Text = "0"
                Me.lblPREZANKN2.Text = (_intZANKN + _intPREZANKN).ToString("#,##0")
                Me.lblSRTPO2.Text = _intSRTPO.ToString("#,##0")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region



End Class
