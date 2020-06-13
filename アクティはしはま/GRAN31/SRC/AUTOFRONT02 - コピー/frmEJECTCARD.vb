Imports System.Threading
Imports System.ComponentModel
Imports Techno.DataBase
Imports Techno.DeviceControls

Public Class frmEJECTCARD

#Region "▼宣言部"

    Enum eEjectResult
        ERR_GET_DATA
        ERR_SET_DATA
        ERR_EJECT_CARD
        ERR_OTHER
        SUCCESS
    End Enum

    ' カード発券機のエラー
    Private _cardDispenserUnitMessage As Techno.DeviceControls.STR610Control.UNIT_STATUS

    ' ICR700コントロール
    Public Property dcICR700 As TECHNO.DeviceControls.ICR700Control
    ' MCH3000コントロール
    Public Property dcMCH3000 As TECHNO.DeviceControls.MCH3000Control
    ' 顧客情報
    Public Property Custmer As CustmerModel = New CustmerModel
    ' カード内容を保持
    Public Property ICR700Data As ICR700Model = New ICR700Model
    ' 発券タイプ
    Public Property EjectType As eEjectType = eEjectType.WithCreateCustmer
    ' エラー
    Public Property Err As Boolean = False

    Public Enum eEjectType
        WithCreateCustmer
        WithoutCreateCustmer
    End Enum

#End Region

#Region "▼ プロパティ"

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

#Region "▼ イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEJECTCARD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' イメージの位置調整
            Dim x = CInt((frmBase.ScreenSize.Width / 2) - (Me.picImage.Width / 2))
            Me.picImage.Location = New Point(x, Me.picImage.Location.Y)

            Me.mEjectCard.WorkerReportsProgress = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmEJECTCARD_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.Refresh()
            Me.mEjectCard.RunWorkerAsync()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' カード発券
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mEjectCard_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mEjectCard.DoWork

        Me.mEjectCard.ReportProgress(0)

        ' *** カード発券処理
        ' ①カード発券ユニットのエラーを検出。エラーがあれば処理終了
        ' ②無記名カードの手順で顧客マスタに自動採番した仮の顧客番号を作成
        ' ③入金画面を表示。入金後、発券処理へ。
        ' ④発券処理でエラーが起こったら②③すべてをロールバック(Delete)
        Try
            e.Result = eEjectResult.ERR_OTHER

            Dim states = _dcMCH3000.SR_Command
            If states(2) = 0 Then
                ' エンプティ
                Throw New Exception
                Return
            End If

            ' 発行(CD→コンタクト部に停止)
            If Not _dcMCH3000.SD_Command() Then
                Throw New Exception
                Return
            End If

            ' コンタクト部にたどり着くまで待機
            Dim blnErr1 = True
            For i = 0 To 1000
                states = _dcMCH3000.SR_Command
                If states(1) = 1 Then
                    blnErr1 = False
                    Exit For
                End If
                If states(0).Equals(1) Or i > 100 Then
                    'エラー
                    Exit For
                End If
            Next

            ' カード詰まり1
            If blnErr1 Then
                Throw New Exception
                Return
            End If

            '【書き込み情報セット】
            '店番号
            Me.ICR700Data.SHOPNO = UIUtility.SYSTEM.SHOPNO
            'パスワード
            Me.ICR700Data.PASSCD = "00"
            'シリアルナンバー
            Me.ICR700Data.SERIALNO = "000"
            '種別
            Me.ICR700Data.SYUBETU = "0"
            '金額
            Me.ICR700Data.KINGAKU = "00000"
            '予備
            Me.ICR700Data.YOBI = "0"
            'カード区分
            Me.ICR700Data.CARDKBN = "1"
            'カード番号
            Me.ICR700Data.CARDNO = _Custmer.CARDNO
            '顧客番号
            Me.ICR700Data.NCSNO = _Custmer.CARDNO
            'スクール生番号
            Me.ICR700Data.SCLMANNO = "000000"
            '顧客種別
            Me.ICR700Data.NKBNO = _Custmer.KBMAST.ToString
            '会員期限
            Me.ICR700Data.DMEMBER = "00000000"
            '残金額
            Me.ICR700Data.ZANKN = "00000"
            'P残金額
            Me.ICR700Data.PREZANKN = "00000"
            '残ポイント
            Me.ICR700Data.POINT = "00000"
            '前回来場日
            Me.ICR700Data.ZENENTDATE = "00000000"
            '入場区分
            Me.ICR700Data.ENTKBN = "0"
            'ボール単価
            Me.ICR700Data.BALLKIN = "00"
            '打席番号
            Me.ICR700Data.SEATNO = "000"

            e.Result = eEjectResult.SUCCESS
            
            Return

        Catch ex As Exception
            e.Result = eEjectResult.ERR_EJECT_CARD
            Return

        End Try

    End Sub

    Private Sub mEjectCard_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles mEjectCard.ProgressChanged
        Select Case e.ProgressPercentage
            Case 0
                Me.lblMSG.Text = "発券準備中…"
                'Me.lblMSG.Text = "カード発券中…"
                Me.picImage.Image = Images.GIFLoading
            Case 1
                Me.lblMSG.Text = "カードをお受け取りください。"
                Me.picImage.Image = Images.GIFEjectCard
            Case -1
                Me.lblMSG.Text = "カードを挿入口からお取りください。"
                Me.picImage.Image = Images.GIFEjectCard
        End Select
    End Sub

    Private Sub mEjectCard_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles mEjectCard.RunWorkerCompleted
        Select Case CType(e.Result, eEjectResult)
            Case eEjectResult.ERR_GET_DATA
                Using frm As New frmMSGBOXEx("データの取得に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.ERR_SET_DATA
                Using frm As New frmMSGBOXEx("登録に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.ERR_EJECT_CARD
                'Using frm As New frmMSGBOXEx("カード発券に失敗しました。", 3)
                '    frm.ShowDialog()
                'End Using
                Me.Err = True
            Case eEjectResult.ERR_OTHER
                Using frm As New frmMSGBOXEx("カード発券に失敗しました。", 3)
                    frm.ShowDialog()
                End Using
                Me.Err = True
            Case eEjectResult.SUCCESS
                Me.Err = False
        End Select

        Me.Close()
    End Sub

#End Region

End Class
