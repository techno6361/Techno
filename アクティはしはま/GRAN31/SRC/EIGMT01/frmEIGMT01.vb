Imports TECHNO.DataBase

Public Class frmEIGMT01

#Region "▼宣言部"

    Private Const _cstClrMoji As String = "--"

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region


#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "営業情報登録(1球貸し)"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "営業情報登録(1球貸し)"

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
    Private Sub frmEIGMT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            '画面初期設定
            Init()

            '優先単価情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

            '料金体系マスタ情報取得
            If Not GetRKNMTA() Then
                Using frm As New frmMSGBOX01("料金体系マスタ情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            'コピー元顧客種別情報取得
            If Not GetCopyKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

            '営業情報取得
            If Not GetNkbEIGMTA() Then
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


    ''' <summary>
    ''' 料金体系コピーボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCopyRKNKB_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyRKNKB.Click
        Try
            If String.IsNullOrEmpty(Me.cmbRKNKB_A.Text) Then
                Using frm As New frmMSGBOX01("料金体系を選択してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.cmbRKNKB_A.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(Me.cmbRKNKB_B.Text) Then
                Using frm As New frmMSGBOX01("料金体系を選択してください。", 3)
                    frm.ShowDialog()
                End Using
                Me.cmbRKNKB_B.Focus()
                Exit Sub
            End If
            If Me.cmbRKNKB_A.Text.Equals(Me.cmbRKNKB_B.Text) Then
                Using frm As New frmMSGBOX01("料金体系が同じです。", 3)
                    frm.ShowDialog()
                End Using
                Me.cmbRKNKB_B.Focus()
                Exit Sub
            End If

            'コピー元
            Dim TIMENM01_A As TextBox = Nothing
            Dim TIMENM02_A As TextBox = Nothing
            Dim TIMENM03_A As TextBox = Nothing
            Dim TIMENM04_A As TextBox = Nothing
            Dim TIMENM05_A As TextBox = Nothing
            Dim PASSCD01_A As TextBox = Nothing
            Dim PASSCD02_A As TextBox = Nothing
            Dim PASSCD03_A As TextBox = Nothing
            Dim PASSCD04_A As TextBox = Nothing
            Dim PASSCD05_A As TextBox = Nothing
            Dim ENTKIN01_A As TextBox = Nothing
            Dim ENTKIN02_A As TextBox = Nothing
            Dim ENTKIN03_A As TextBox = Nothing
            Dim ENTKIN04_A As TextBox = Nothing
            Dim ENTKIN05_A As TextBox = Nothing
            Dim POINT01_A As TextBox = Nothing
            Dim POINT02_A As TextBox = Nothing
            Dim POINT03_A As TextBox = Nothing
            Dim POINT04_A As TextBox = Nothing
            Dim POINT05_A As TextBox = Nothing
            Dim POINTW01_A As TextBox = Nothing
            Dim POINTW02_A As TextBox = Nothing
            Dim POINTW03_A As TextBox = Nothing
            Dim POINTW04_A As TextBox = Nothing
            Dim POINTW05_A As TextBox = Nothing
            Dim POINTS01_A As TextBox = Nothing
            Dim POINTS02_A As TextBox = Nothing
            Dim POINTS03_A As TextBox = Nothing
            Dim POINTS04_A As TextBox = Nothing
            Dim POINTS05_A As TextBox = Nothing
            Dim BALLKIN1F01_A As TextBox = Nothing
            Dim BALLKIN1F02_A As TextBox = Nothing
            Dim BALLKIN1F03_A As TextBox = Nothing
            Dim BALLKIN1F04_A As TextBox = Nothing
            Dim BALLKIN1F05_A As TextBox = Nothing
            Dim BALLKIN2F01_A As TextBox = Nothing
            Dim BALLKIN2F02_A As TextBox = Nothing
            Dim BALLKIN2F03_A As TextBox = Nothing
            Dim BALLKIN2F04_A As TextBox = Nothing
            Dim BALLKIN2F05_A As TextBox = Nothing
            Dim SITEIKBN01_A As CheckBox = Nothing
            Dim SITEIKBN02_A As CheckBox = Nothing
            Dim SITEIKBN03_A As CheckBox = Nothing
            Dim SITEIKBN04_A As CheckBox = Nothing
            Dim SITEIKBN05_A As CheckBox = Nothing
            Select Case Me.cmbRKNKB_A.SelectedIndex
                Case 1  '【平日】
                    TIMENM01_A = Me.txtTIMENM01_01
                    TIMENM02_A = Me.txtTIMENM02_01
                    TIMENM03_A = Me.txtTIMENM03_01
                    TIMENM04_A = Me.txtTIMENM04_01
                    TIMENM05_A = Me.txtTIMENM05_01
                    PASSCD01_A = Me.txtPASSCD01_01
                    PASSCD02_A = Me.txtPASSCD02_01
                    PASSCD03_A = Me.txtPASSCD03_01
                    PASSCD04_A = Me.txtPASSCD04_01
                    PASSCD05_A = Me.txtPASSCD05_01
                    ENTKIN01_A = Me.txtENTKIN01_01
                    ENTKIN02_A = Me.txtENTKIN02_01
                    ENTKIN03_A = Me.txtENTKIN03_01
                    ENTKIN04_A = Me.txtENTKIN04_01
                    ENTKIN05_A = Me.txtENTKIN05_01
                    POINT01_A = Me.txtPOINT01_01
                    POINT02_A = Me.txtPOINT02_01
                    POINT03_A = Me.txtPOINT03_01
                    POINT04_A = Me.txtPOINT04_01
                    POINT05_A = Me.txtPOINT05_01
                    POINTW01_A = Me.txtPOINTW01_01
                    POINTW02_A = Me.txtPOINTW02_01
                    POINTW03_A = Me.txtPOINTW03_01
                    POINTW04_A = Me.txtPOINTW04_01
                    POINTW05_A = Me.txtPOINTW05_01
                    POINTS01_A = Me.txtPOINTS01_01
                    POINTS02_A = Me.txtPOINTS02_01
                    POINTS03_A = Me.txtPOINTS03_01
                    POINTS04_A = Me.txtPOINTS04_01
                    POINTS05_A = Me.txtPOINTS05_01
                    BALLKIN1F01_A = Me.txtBALLKIN1F01_01
                    BALLKIN1F02_A = Me.txtBALLKIN1F02_01
                    BALLKIN1F03_A = Me.txtBALLKIN1F03_01
                    BALLKIN1F04_A = Me.txtBALLKIN1F04_01
                    BALLKIN1F05_A = Me.txtBALLKIN1F05_01
                    BALLKIN2F01_A = Me.txtBALLKIN2F01_01
                    BALLKIN2F02_A = Me.txtBALLKIN2F02_01
                    BALLKIN2F03_A = Me.txtBALLKIN2F03_01
                    BALLKIN2F04_A = Me.txtBALLKIN2F04_01
                    BALLKIN2F05_A = Me.txtBALLKIN2F05_01
                    SITEIKBN01_A = Me.chkSITEIKBN01_01
                    SITEIKBN02_A = Me.chkSITEIKBN02_01
                    SITEIKBN03_A = Me.chkSITEIKBN03_01
                    SITEIKBN04_A = Me.chkSITEIKBN04_01
                    SITEIKBN05_A = Me.chkSITEIKBN05_01
                Case 2  '【休日】
                    TIMENM01_A = Me.txtTIMENM01_02
                    TIMENM02_A = Me.txtTIMENM02_02
                    TIMENM03_A = Me.txtTIMENM03_02
                    TIMENM04_A = Me.txtTIMENM04_02
                    TIMENM05_A = Me.txtTIMENM05_02
                    PASSCD01_A = Me.txtPASSCD01_02
                    PASSCD02_A = Me.txtPASSCD02_02
                    PASSCD03_A = Me.txtPASSCD03_02
                    PASSCD04_A = Me.txtPASSCD04_02
                    PASSCD05_A = Me.txtPASSCD05_02
                    ENTKIN01_A = Me.txtENTKIN01_02
                    ENTKIN02_A = Me.txtENTKIN02_02
                    ENTKIN03_A = Me.txtENTKIN03_02
                    ENTKIN04_A = Me.txtENTKIN04_02
                    ENTKIN05_A = Me.txtENTKIN05_02
                    POINT01_A = Me.txtPOINT01_02
                    POINT02_A = Me.txtPOINT02_02
                    POINT03_A = Me.txtPOINT03_02
                    POINT04_A = Me.txtPOINT04_02
                    POINT05_A = Me.txtPOINT05_02
                    POINTW01_A = Me.txtPOINTW01_02
                    POINTW02_A = Me.txtPOINTW02_02
                    POINTW03_A = Me.txtPOINTW03_02
                    POINTW04_A = Me.txtPOINTW04_02
                    POINTW05_A = Me.txtPOINTW05_02
                    POINTS01_A = Me.txtPOINTS01_02
                    POINTS02_A = Me.txtPOINTS02_02
                    POINTS03_A = Me.txtPOINTS03_02
                    POINTS04_A = Me.txtPOINTS04_02
                    POINTS05_A = Me.txtPOINTS05_02
                    BALLKIN1F01_A = Me.txtBALLKIN1F01_02
                    BALLKIN1F02_A = Me.txtBALLKIN1F02_02
                    BALLKIN1F03_A = Me.txtBALLKIN1F03_02
                    BALLKIN1F04_A = Me.txtBALLKIN1F04_02
                    BALLKIN1F05_A = Me.txtBALLKIN1F05_02
                    BALLKIN2F01_A = Me.txtBALLKIN2F01_02
                    BALLKIN2F02_A = Me.txtBALLKIN2F02_02
                    BALLKIN2F03_A = Me.txtBALLKIN2F03_02
                    BALLKIN2F04_A = Me.txtBALLKIN2F04_02
                    BALLKIN2F05_A = Me.txtBALLKIN2F05_02
                    SITEIKBN01_A = Me.chkSITEIKBN01_02
                    SITEIKBN02_A = Me.chkSITEIKBN02_02
                    SITEIKBN03_A = Me.chkSITEIKBN03_02
                    SITEIKBN04_A = Me.chkSITEIKBN04_02
                    SITEIKBN05_A = Me.chkSITEIKBN05_02
                Case 3  '【特日1】
                    TIMENM01_A = Me.txtTIMENM01_03
                    TIMENM02_A = Me.txtTIMENM02_03
                    TIMENM03_A = Me.txtTIMENM03_03
                    TIMENM04_A = Me.txtTIMENM04_03
                    TIMENM05_A = Me.txtTIMENM05_03
                    PASSCD01_A = Me.txtPASSCD01_03
                    PASSCD02_A = Me.txtPASSCD02_03
                    PASSCD03_A = Me.txtPASSCD03_03
                    PASSCD04_A = Me.txtPASSCD04_03
                    PASSCD05_A = Me.txtPASSCD05_03
                    ENTKIN01_A = Me.txtENTKIN01_03
                    ENTKIN02_A = Me.txtENTKIN02_03
                    ENTKIN03_A = Me.txtENTKIN03_03
                    ENTKIN04_A = Me.txtENTKIN04_03
                    ENTKIN05_A = Me.txtENTKIN05_03
                    POINT01_A = Me.txtPOINT01_03
                    POINT02_A = Me.txtPOINT02_03
                    POINT03_A = Me.txtPOINT03_03
                    POINT04_A = Me.txtPOINT04_03
                    POINT05_A = Me.txtPOINT05_03
                    POINTW01_A = Me.txtPOINTW01_03
                    POINTW02_A = Me.txtPOINTW02_03
                    POINTW03_A = Me.txtPOINTW03_03
                    POINTW04_A = Me.txtPOINTW04_03
                    POINTW05_A = Me.txtPOINTW05_03
                    POINTS01_A = Me.txtPOINTS01_03
                    POINTS02_A = Me.txtPOINTS02_03
                    POINTS03_A = Me.txtPOINTS03_03
                    POINTS04_A = Me.txtPOINTS04_03
                    POINTS05_A = Me.txtPOINTS05_03
                    BALLKIN1F01_A = Me.txtBALLKIN1F01_03
                    BALLKIN1F02_A = Me.txtBALLKIN1F02_03
                    BALLKIN1F03_A = Me.txtBALLKIN1F03_03
                    BALLKIN1F04_A = Me.txtBALLKIN1F04_03
                    BALLKIN1F05_A = Me.txtBALLKIN1F05_03
                    BALLKIN2F01_A = Me.txtBALLKIN2F01_03
                    BALLKIN2F02_A = Me.txtBALLKIN2F02_03
                    BALLKIN2F03_A = Me.txtBALLKIN2F03_03
                    BALLKIN2F04_A = Me.txtBALLKIN2F04_03
                    BALLKIN2F05_A = Me.txtBALLKIN2F05_03
                    SITEIKBN01_A = Me.chkSITEIKBN01_03
                    SITEIKBN02_A = Me.chkSITEIKBN02_03
                    SITEIKBN03_A = Me.chkSITEIKBN03_03
                    SITEIKBN04_A = Me.chkSITEIKBN04_03
                    SITEIKBN05_A = Me.chkSITEIKBN05_03
                Case 4  '【特日2】
                    TIMENM01_A = Me.txtTIMENM01_04
                    TIMENM02_A = Me.txtTIMENM02_04
                    TIMENM03_A = Me.txtTIMENM03_04
                    TIMENM04_A = Me.txtTIMENM04_04
                    TIMENM05_A = Me.txtTIMENM05_04
                    PASSCD01_A = Me.txtPASSCD01_04
                    PASSCD02_A = Me.txtPASSCD02_04
                    PASSCD03_A = Me.txtPASSCD03_04
                    PASSCD04_A = Me.txtPASSCD04_04
                    PASSCD05_A = Me.txtPASSCD05_04
                    ENTKIN01_A = Me.txtENTKIN01_04
                    ENTKIN02_A = Me.txtENTKIN02_04
                    ENTKIN03_A = Me.txtENTKIN03_04
                    ENTKIN04_A = Me.txtENTKIN04_04
                    ENTKIN05_A = Me.txtENTKIN05_04
                    POINT01_A = Me.txtPOINT01_04
                    POINT02_A = Me.txtPOINT02_04
                    POINT03_A = Me.txtPOINT03_04
                    POINT04_A = Me.txtPOINT04_04
                    POINT05_A = Me.txtPOINT05_04
                    POINTW01_A = Me.txtPOINTW01_04
                    POINTW02_A = Me.txtPOINTW02_04
                    POINTW03_A = Me.txtPOINTW03_04
                    POINTW04_A = Me.txtPOINTW04_04
                    POINTW05_A = Me.txtPOINTW05_04
                    POINTS01_A = Me.txtPOINTS01_04
                    POINTS02_A = Me.txtPOINTS02_04
                    POINTS03_A = Me.txtPOINTS03_04
                    POINTS04_A = Me.txtPOINTS04_04
                    POINTS05_A = Me.txtPOINTS05_04
                    BALLKIN1F01_A = Me.txtBALLKIN1F01_04
                    BALLKIN1F02_A = Me.txtBALLKIN1F02_04
                    BALLKIN1F03_A = Me.txtBALLKIN1F03_04
                    BALLKIN1F04_A = Me.txtBALLKIN1F04_04
                    BALLKIN1F05_A = Me.txtBALLKIN1F05_04
                    BALLKIN2F01_A = Me.txtBALLKIN2F01_04
                    BALLKIN2F02_A = Me.txtBALLKIN2F02_04
                    BALLKIN2F03_A = Me.txtBALLKIN2F03_04
                    BALLKIN2F04_A = Me.txtBALLKIN2F04_04
                    BALLKIN2F05_A = Me.txtBALLKIN2F05_04
                    SITEIKBN01_A = Me.chkSITEIKBN01_04
                    SITEIKBN02_A = Me.chkSITEIKBN02_04
                    SITEIKBN03_A = Me.chkSITEIKBN03_04
                    SITEIKBN04_A = Me.chkSITEIKBN04_04
                    SITEIKBN05_A = Me.chkSITEIKBN05_04
                Case 5  '【特日3】
                    TIMENM01_A = Me.txtTIMENM01_05
                    TIMENM02_A = Me.txtTIMENM02_05
                    TIMENM03_A = Me.txtTIMENM03_05
                    TIMENM04_A = Me.txtTIMENM04_05
                    TIMENM05_A = Me.txtTIMENM05_05
                    PASSCD01_A = Me.txtPASSCD01_05
                    PASSCD02_A = Me.txtPASSCD02_05
                    PASSCD03_A = Me.txtPASSCD03_05
                    PASSCD04_A = Me.txtPASSCD04_05
                    PASSCD05_A = Me.txtPASSCD05_05
                    ENTKIN01_A = Me.txtENTKIN01_05
                    ENTKIN02_A = Me.txtENTKIN02_05
                    ENTKIN03_A = Me.txtENTKIN03_05
                    ENTKIN04_A = Me.txtENTKIN04_05
                    ENTKIN05_A = Me.txtENTKIN05_05
                    POINT01_A = Me.txtPOINT01_05
                    POINT02_A = Me.txtPOINT02_05
                    POINT03_A = Me.txtPOINT03_05
                    POINT04_A = Me.txtPOINT04_05
                    POINT05_A = Me.txtPOINT05_05
                    POINTW01_A = Me.txtPOINTW01_05
                    POINTW02_A = Me.txtPOINTW02_05
                    POINTW03_A = Me.txtPOINTW03_05
                    POINTW04_A = Me.txtPOINTW04_05
                    POINTW05_A = Me.txtPOINTW05_05
                    POINTS01_A = Me.txtPOINTS01_05
                    POINTS02_A = Me.txtPOINTS02_05
                    POINTS03_A = Me.txtPOINTS03_05
                    POINTS04_A = Me.txtPOINTS04_05
                    POINTS05_A = Me.txtPOINTS05_05
                    BALLKIN1F01_A = Me.txtBALLKIN1F01_05
                    BALLKIN1F02_A = Me.txtBALLKIN1F02_05
                    BALLKIN1F03_A = Me.txtBALLKIN1F03_05
                    BALLKIN1F04_A = Me.txtBALLKIN1F04_05
                    BALLKIN1F05_A = Me.txtBALLKIN1F05_05
                    BALLKIN2F01_A = Me.txtBALLKIN2F01_05
                    BALLKIN2F02_A = Me.txtBALLKIN2F02_05
                    BALLKIN2F03_A = Me.txtBALLKIN2F03_05
                    BALLKIN2F04_A = Me.txtBALLKIN2F04_05
                    BALLKIN2F05_A = Me.txtBALLKIN2F05_05
                    SITEIKBN01_A = Me.chkSITEIKBN01_05
                    SITEIKBN02_A = Me.chkSITEIKBN02_05
                    SITEIKBN03_A = Me.chkSITEIKBN03_05
                    SITEIKBN04_A = Me.chkSITEIKBN04_05
                    SITEIKBN05_A = Me.chkSITEIKBN05_05
                Case 6  '【特日4】
                    TIMENM01_A = Me.txtTIMENM01_06
                    TIMENM02_A = Me.txtTIMENM02_06
                    TIMENM03_A = Me.txtTIMENM03_06
                    TIMENM04_A = Me.txtTIMENM04_06
                    TIMENM05_A = Me.txtTIMENM05_06
                    PASSCD01_A = Me.txtPASSCD01_06
                    PASSCD02_A = Me.txtPASSCD02_06
                    PASSCD03_A = Me.txtPASSCD03_06
                    PASSCD04_A = Me.txtPASSCD04_06
                    PASSCD05_A = Me.txtPASSCD05_06
                    ENTKIN01_A = Me.txtENTKIN01_06
                    ENTKIN02_A = Me.txtENTKIN02_06
                    ENTKIN03_A = Me.txtENTKIN03_06
                    ENTKIN04_A = Me.txtENTKIN04_06
                    ENTKIN05_A = Me.txtENTKIN05_06
                    POINT01_A = Me.txtPOINT01_06
                    POINT02_A = Me.txtPOINT02_06
                    POINT03_A = Me.txtPOINT03_06
                    POINT04_A = Me.txtPOINT04_06
                    POINT05_A = Me.txtPOINT05_06
                    POINTW01_A = Me.txtPOINTW01_06
                    POINTW02_A = Me.txtPOINTW02_06
                    POINTW03_A = Me.txtPOINTW03_06
                    POINTW04_A = Me.txtPOINTW04_06
                    POINTW05_A = Me.txtPOINTW05_06
                    POINTS01_A = Me.txtPOINTS01_06
                    POINTS02_A = Me.txtPOINTS02_06
                    POINTS03_A = Me.txtPOINTS03_06
                    POINTS04_A = Me.txtPOINTS04_06
                    POINTS05_A = Me.txtPOINTS05_06
                    BALLKIN1F01_A = Me.txtBALLKIN1F01_06
                    BALLKIN1F02_A = Me.txtBALLKIN1F02_06
                    BALLKIN1F03_A = Me.txtBALLKIN1F03_06
                    BALLKIN1F04_A = Me.txtBALLKIN1F04_06
                    BALLKIN1F05_A = Me.txtBALLKIN1F05_06
                    BALLKIN2F01_A = Me.txtBALLKIN2F01_06
                    BALLKIN2F02_A = Me.txtBALLKIN2F02_06
                    BALLKIN2F03_A = Me.txtBALLKIN2F03_06
                    BALLKIN2F04_A = Me.txtBALLKIN2F04_06
                    BALLKIN2F05_A = Me.txtBALLKIN2F05_06
                    SITEIKBN01_A = Me.chkSITEIKBN01_06
                    SITEIKBN02_A = Me.chkSITEIKBN02_06
                    SITEIKBN03_A = Me.chkSITEIKBN03_06
                    SITEIKBN04_A = Me.chkSITEIKBN04_06
                    SITEIKBN05_A = Me.chkSITEIKBN05_06
            End Select
            'コピー先
            Dim TIMENM01_B As TextBox = Nothing
            Dim TIMENM02_B As TextBox = Nothing
            Dim TIMENM03_B As TextBox = Nothing
            Dim TIMENM04_B As TextBox = Nothing
            Dim TIMENM05_B As TextBox = Nothing
            Dim PASSCD01_B As TextBox = Nothing
            Dim PASSCD02_B As TextBox = Nothing
            Dim PASSCD03_B As TextBox = Nothing
            Dim PASSCD04_B As TextBox = Nothing
            Dim PASSCD05_B As TextBox = Nothing
            Dim ENTKIN01_B As TextBox = Nothing
            Dim ENTKIN02_B As TextBox = Nothing
            Dim ENTKIN03_B As TextBox = Nothing
            Dim ENTKIN04_B As TextBox = Nothing
            Dim ENTKIN05_B As TextBox = Nothing
            Dim POINT01_B As TextBox = Nothing
            Dim POINT02_B As TextBox = Nothing
            Dim POINT03_B As TextBox = Nothing
            Dim POINT04_B As TextBox = Nothing
            Dim POINT05_B As TextBox = Nothing
            Dim POINTW01_B As TextBox = Nothing
            Dim POINTW02_B As TextBox = Nothing
            Dim POINTW03_B As TextBox = Nothing
            Dim POINTW04_B As TextBox = Nothing
            Dim POINTW05_B As TextBox = Nothing
            Dim POINTS01_B As TextBox = Nothing
            Dim POINTS02_B As TextBox = Nothing
            Dim POINTS03_B As TextBox = Nothing
            Dim POINTS04_B As TextBox = Nothing
            Dim POINTS05_B As TextBox = Nothing
            Dim BALLKIN1F01_B As TextBox = Nothing
            Dim BALLKIN1F02_B As TextBox = Nothing
            Dim BALLKIN1F03_B As TextBox = Nothing
            Dim BALLKIN1F04_B As TextBox = Nothing
            Dim BALLKIN1F05_B As TextBox = Nothing
            Dim BALLKIN2F01_B As TextBox = Nothing
            Dim BALLKIN2F02_B As TextBox = Nothing
            Dim BALLKIN2F03_B As TextBox = Nothing
            Dim BALLKIN2F04_B As TextBox = Nothing
            Dim BALLKIN2F05_B As TextBox = Nothing
            Dim SITEIKBN01_B As CheckBox = Nothing
            Dim SITEIKBN02_B As CheckBox = Nothing
            Dim SITEIKBN03_B As CheckBox = Nothing
            Dim SITEIKBN04_B As CheckBox = Nothing
            Dim SITEIKBN05_B As CheckBox = Nothing
            Select Case Me.cmbRKNKB_B.SelectedIndex
                Case 1  '【平日】
                    TIMENM01_B = Me.txtTIMENM01_01
                    TIMENM02_B = Me.txtTIMENM02_01
                    TIMENM03_B = Me.txtTIMENM03_01
                    TIMENM04_B = Me.txtTIMENM04_01
                    TIMENM05_B = Me.txtTIMENM05_01
                    PASSCD01_B = Me.txtPASSCD01_01
                    PASSCD02_B = Me.txtPASSCD02_01
                    PASSCD03_B = Me.txtPASSCD03_01
                    PASSCD04_B = Me.txtPASSCD04_01
                    PASSCD05_B = Me.txtPASSCD05_01
                    ENTKIN01_B = Me.txtENTKIN01_01
                    ENTKIN02_B = Me.txtENTKIN02_01
                    ENTKIN03_B = Me.txtENTKIN03_01
                    ENTKIN04_B = Me.txtENTKIN04_01
                    ENTKIN05_B = Me.txtENTKIN05_01
                    POINT01_B = Me.txtPOINT01_01
                    POINT02_B = Me.txtPOINT02_01
                    POINT03_B = Me.txtPOINT03_01
                    POINT04_B = Me.txtPOINT04_01
                    POINT05_B = Me.txtPOINT05_01
                    POINTW01_B = Me.txtPOINTW01_01
                    POINTW02_B = Me.txtPOINTW02_01
                    POINTW03_B = Me.txtPOINTW03_01
                    POINTW04_B = Me.txtPOINTW04_01
                    POINTW05_B = Me.txtPOINTW05_01
                    POINTS01_B = Me.txtPOINTS01_01
                    POINTS02_B = Me.txtPOINTS02_01
                    POINTS03_B = Me.txtPOINTS03_01
                    POINTS04_B = Me.txtPOINTS04_01
                    POINTS05_B = Me.txtPOINTS05_01
                    BALLKIN1F01_B = Me.txtBALLKIN1F01_01
                    BALLKIN1F02_B = Me.txtBALLKIN1F02_01
                    BALLKIN1F03_B = Me.txtBALLKIN1F03_01
                    BALLKIN1F04_B = Me.txtBALLKIN1F04_01
                    BALLKIN1F05_B = Me.txtBALLKIN1F05_01
                    BALLKIN2F01_B = Me.txtBALLKIN2F01_01
                    BALLKIN2F02_B = Me.txtBALLKIN2F02_01
                    BALLKIN2F03_B = Me.txtBALLKIN2F03_01
                    BALLKIN2F04_B = Me.txtBALLKIN2F04_01
                    BALLKIN2F05_B = Me.txtBALLKIN2F05_01
                    SITEIKBN01_B = Me.chkSITEIKBN01_01
                    SITEIKBN02_B = Me.chkSITEIKBN02_01
                    SITEIKBN03_B = Me.chkSITEIKBN03_01
                    SITEIKBN04_B = Me.chkSITEIKBN04_01
                    SITEIKBN05_B = Me.chkSITEIKBN05_01
                Case 2  '【休日】
                    TIMENM01_B = Me.txtTIMENM01_02
                    TIMENM02_B = Me.txtTIMENM02_02
                    TIMENM03_B = Me.txtTIMENM03_02
                    TIMENM04_B = Me.txtTIMENM04_02
                    TIMENM05_B = Me.txtTIMENM05_02
                    PASSCD01_B = Me.txtPASSCD01_02
                    PASSCD02_B = Me.txtPASSCD02_02
                    PASSCD03_B = Me.txtPASSCD03_02
                    PASSCD04_B = Me.txtPASSCD04_02
                    PASSCD05_B = Me.txtPASSCD05_02
                    ENTKIN01_B = Me.txtENTKIN01_02
                    ENTKIN02_B = Me.txtENTKIN02_02
                    ENTKIN03_B = Me.txtENTKIN03_02
                    ENTKIN04_B = Me.txtENTKIN04_02
                    ENTKIN05_B = Me.txtENTKIN05_02
                    POINT01_B = Me.txtPOINT01_02
                    POINT02_B = Me.txtPOINT02_02
                    POINT03_B = Me.txtPOINT03_02
                    POINT04_B = Me.txtPOINT04_02
                    POINT05_B = Me.txtPOINT05_02
                    POINTW01_B = Me.txtPOINTW01_02
                    POINTW02_B = Me.txtPOINTW02_02
                    POINTW03_B = Me.txtPOINTW03_02
                    POINTW04_B = Me.txtPOINTW04_02
                    POINTW05_B = Me.txtPOINTW05_02
                    POINTS01_B = Me.txtPOINTS01_02
                    POINTS02_B = Me.txtPOINTS02_02
                    POINTS03_B = Me.txtPOINTS03_02
                    POINTS04_B = Me.txtPOINTS04_02
                    POINTS05_B = Me.txtPOINTS05_02
                    BALLKIN1F01_B = Me.txtBALLKIN1F01_02
                    BALLKIN1F02_B = Me.txtBALLKIN1F02_02
                    BALLKIN1F03_B = Me.txtBALLKIN1F03_02
                    BALLKIN1F04_B = Me.txtBALLKIN1F04_02
                    BALLKIN1F05_B = Me.txtBALLKIN1F05_02
                    BALLKIN2F01_B = Me.txtBALLKIN2F01_02
                    BALLKIN2F02_B = Me.txtBALLKIN2F02_02
                    BALLKIN2F03_B = Me.txtBALLKIN2F03_02
                    BALLKIN2F04_B = Me.txtBALLKIN2F04_02
                    BALLKIN2F05_B = Me.txtBALLKIN2F05_02
                    SITEIKBN01_B = Me.chkSITEIKBN01_02
                    SITEIKBN02_B = Me.chkSITEIKBN02_02
                    SITEIKBN03_B = Me.chkSITEIKBN03_02
                    SITEIKBN04_B = Me.chkSITEIKBN04_02
                    SITEIKBN05_B = Me.chkSITEIKBN05_02
                Case 3  '【特日1】
                    TIMENM01_B = Me.txtTIMENM01_03
                    TIMENM02_B = Me.txtTIMENM02_03
                    TIMENM03_B = Me.txtTIMENM03_03
                    TIMENM04_B = Me.txtTIMENM04_03
                    TIMENM05_B = Me.txtTIMENM05_03
                    PASSCD01_B = Me.txtPASSCD01_03
                    PASSCD02_B = Me.txtPASSCD02_03
                    PASSCD03_B = Me.txtPASSCD03_03
                    PASSCD04_B = Me.txtPASSCD04_03
                    PASSCD05_B = Me.txtPASSCD05_03
                    ENTKIN01_B = Me.txtENTKIN01_03
                    ENTKIN02_B = Me.txtENTKIN02_03
                    ENTKIN03_B = Me.txtENTKIN03_03
                    ENTKIN04_B = Me.txtENTKIN04_03
                    ENTKIN05_B = Me.txtENTKIN05_03
                    POINT01_B = Me.txtPOINT01_03
                    POINT02_B = Me.txtPOINT02_03
                    POINT03_B = Me.txtPOINT03_03
                    POINT04_B = Me.txtPOINT04_03
                    POINT05_B = Me.txtPOINT05_03
                    POINTW01_B = Me.txtPOINTW01_03
                    POINTW02_B = Me.txtPOINTW02_03
                    POINTW03_B = Me.txtPOINTW03_03
                    POINTW04_B = Me.txtPOINTW04_03
                    POINTW05_B = Me.txtPOINTW05_03
                    POINTS01_B = Me.txtPOINTS01_03
                    POINTS02_B = Me.txtPOINTS02_03
                    POINTS03_B = Me.txtPOINTS03_03
                    POINTS04_B = Me.txtPOINTS04_03
                    POINTS05_B = Me.txtPOINTS05_03
                    BALLKIN1F01_B = Me.txtBALLKIN1F01_03
                    BALLKIN1F02_B = Me.txtBALLKIN1F02_03
                    BALLKIN1F03_B = Me.txtBALLKIN1F03_03
                    BALLKIN1F04_B = Me.txtBALLKIN1F04_03
                    BALLKIN1F05_B = Me.txtBALLKIN1F05_03
                    BALLKIN2F01_B = Me.txtBALLKIN2F01_03
                    BALLKIN2F02_B = Me.txtBALLKIN2F02_03
                    BALLKIN2F03_B = Me.txtBALLKIN2F03_03
                    BALLKIN2F04_B = Me.txtBALLKIN2F04_03
                    BALLKIN2F05_B = Me.txtBALLKIN2F05_03
                    SITEIKBN01_B = Me.chkSITEIKBN01_03
                    SITEIKBN02_B = Me.chkSITEIKBN02_03
                    SITEIKBN03_B = Me.chkSITEIKBN03_03
                    SITEIKBN04_B = Me.chkSITEIKBN04_03
                    SITEIKBN05_B = Me.chkSITEIKBN05_03
                Case 4  '【特日2】
                    TIMENM01_B = Me.txtTIMENM01_04
                    TIMENM02_B = Me.txtTIMENM02_04
                    TIMENM03_B = Me.txtTIMENM03_04
                    TIMENM04_B = Me.txtTIMENM04_04
                    TIMENM05_B = Me.txtTIMENM05_04
                    PASSCD01_B = Me.txtPASSCD01_04
                    PASSCD02_B = Me.txtPASSCD02_04
                    PASSCD03_B = Me.txtPASSCD03_04
                    PASSCD04_B = Me.txtPASSCD04_04
                    PASSCD05_B = Me.txtPASSCD05_04
                    ENTKIN01_B = Me.txtENTKIN01_04
                    ENTKIN02_B = Me.txtENTKIN02_04
                    ENTKIN03_B = Me.txtENTKIN03_04
                    ENTKIN04_B = Me.txtENTKIN04_04
                    ENTKIN05_B = Me.txtENTKIN05_04
                    POINT01_B = Me.txtPOINT01_04
                    POINT02_B = Me.txtPOINT02_04
                    POINT03_B = Me.txtPOINT03_04
                    POINT04_B = Me.txtPOINT04_04
                    POINT05_B = Me.txtPOINT05_04
                    POINTW01_B = Me.txtPOINTW01_04
                    POINTW02_B = Me.txtPOINTW02_04
                    POINTW03_B = Me.txtPOINTW03_04
                    POINTW04_B = Me.txtPOINTW04_04
                    POINTW05_B = Me.txtPOINTW05_04
                    POINTS01_B = Me.txtPOINTS01_04
                    POINTS02_B = Me.txtPOINTS02_04
                    POINTS03_B = Me.txtPOINTS03_04
                    POINTS04_B = Me.txtPOINTS04_04
                    POINTS05_B = Me.txtPOINTS05_04
                    BALLKIN1F01_B = Me.txtBALLKIN1F01_04
                    BALLKIN1F02_B = Me.txtBALLKIN1F02_04
                    BALLKIN1F03_B = Me.txtBALLKIN1F03_04
                    BALLKIN1F04_B = Me.txtBALLKIN1F04_04
                    BALLKIN1F05_B = Me.txtBALLKIN1F05_04
                    BALLKIN2F01_B = Me.txtBALLKIN2F01_04
                    BALLKIN2F02_B = Me.txtBALLKIN2F02_04
                    BALLKIN2F03_B = Me.txtBALLKIN2F03_04
                    BALLKIN2F04_B = Me.txtBALLKIN2F04_04
                    BALLKIN2F05_B = Me.txtBALLKIN2F05_04
                    SITEIKBN01_B = Me.chkSITEIKBN01_04
                    SITEIKBN02_B = Me.chkSITEIKBN02_04
                    SITEIKBN03_B = Me.chkSITEIKBN03_04
                    SITEIKBN04_B = Me.chkSITEIKBN04_04
                    SITEIKBN05_B = Me.chkSITEIKBN05_04
                Case 5  '【特日3】
                    TIMENM01_B = Me.txtTIMENM01_05
                    TIMENM02_B = Me.txtTIMENM02_05
                    TIMENM03_B = Me.txtTIMENM03_05
                    TIMENM04_B = Me.txtTIMENM04_05
                    TIMENM05_B = Me.txtTIMENM05_05
                    PASSCD01_B = Me.txtPASSCD01_05
                    PASSCD02_B = Me.txtPASSCD02_05
                    PASSCD03_B = Me.txtPASSCD03_05
                    PASSCD04_B = Me.txtPASSCD04_05
                    PASSCD05_B = Me.txtPASSCD05_05
                    ENTKIN01_B = Me.txtENTKIN01_05
                    ENTKIN02_B = Me.txtENTKIN02_05
                    ENTKIN03_B = Me.txtENTKIN03_05
                    ENTKIN04_B = Me.txtENTKIN04_05
                    ENTKIN05_B = Me.txtENTKIN05_05
                    POINT01_B = Me.txtPOINT01_05
                    POINT02_B = Me.txtPOINT02_05
                    POINT03_B = Me.txtPOINT03_05
                    POINT04_B = Me.txtPOINT04_05
                    POINT05_B = Me.txtPOINT05_05
                    POINTW01_B = Me.txtPOINTW01_05
                    POINTW02_B = Me.txtPOINTW02_05
                    POINTW03_B = Me.txtPOINTW03_05
                    POINTW04_B = Me.txtPOINTW04_05
                    POINTW05_B = Me.txtPOINTW05_05
                    POINTS01_B = Me.txtPOINTS01_05
                    POINTS02_B = Me.txtPOINTS02_05
                    POINTS03_B = Me.txtPOINTS03_05
                    POINTS04_B = Me.txtPOINTS04_05
                    POINTS05_B = Me.txtPOINTS05_05
                    BALLKIN1F01_B = Me.txtBALLKIN1F01_05
                    BALLKIN1F02_B = Me.txtBALLKIN1F02_05
                    BALLKIN1F03_B = Me.txtBALLKIN1F03_05
                    BALLKIN1F04_B = Me.txtBALLKIN1F04_05
                    BALLKIN1F05_B = Me.txtBALLKIN1F05_05
                    BALLKIN2F01_B = Me.txtBALLKIN2F01_05
                    BALLKIN2F02_B = Me.txtBALLKIN2F02_05
                    BALLKIN2F03_B = Me.txtBALLKIN2F03_05
                    BALLKIN2F04_B = Me.txtBALLKIN2F04_05
                    BALLKIN2F05_B = Me.txtBALLKIN2F05_05
                    SITEIKBN01_B = Me.chkSITEIKBN01_05
                    SITEIKBN02_B = Me.chkSITEIKBN02_05
                    SITEIKBN03_B = Me.chkSITEIKBN03_05
                    SITEIKBN04_B = Me.chkSITEIKBN04_05
                    SITEIKBN05_B = Me.chkSITEIKBN05_05
                Case 6  '【特日4】
                    TIMENM01_B = Me.txtTIMENM01_06
                    TIMENM02_B = Me.txtTIMENM02_06
                    TIMENM03_B = Me.txtTIMENM03_06
                    TIMENM04_B = Me.txtTIMENM04_06
                    TIMENM05_B = Me.txtTIMENM05_06
                    PASSCD01_B = Me.txtPASSCD01_06
                    PASSCD02_B = Me.txtPASSCD02_06
                    PASSCD03_B = Me.txtPASSCD03_06
                    PASSCD04_B = Me.txtPASSCD04_06
                    PASSCD05_B = Me.txtPASSCD05_06
                    ENTKIN01_B = Me.txtENTKIN01_06
                    ENTKIN02_B = Me.txtENTKIN02_06
                    ENTKIN03_B = Me.txtENTKIN03_06
                    ENTKIN04_B = Me.txtENTKIN04_06
                    ENTKIN05_B = Me.txtENTKIN05_06
                    POINT01_B = Me.txtPOINT01_06
                    POINT02_B = Me.txtPOINT02_06
                    POINT03_B = Me.txtPOINT03_06
                    POINT04_B = Me.txtPOINT04_06
                    POINT05_B = Me.txtPOINT05_06
                    POINTW01_B = Me.txtPOINTW01_06
                    POINTW02_B = Me.txtPOINTW02_06
                    POINTW03_B = Me.txtPOINTW03_06
                    POINTW04_B = Me.txtPOINTW04_06
                    POINTW05_B = Me.txtPOINTW05_06
                    POINTS01_B = Me.txtPOINTS01_06
                    POINTS02_B = Me.txtPOINTS02_06
                    POINTS03_B = Me.txtPOINTS03_06
                    POINTS04_B = Me.txtPOINTS04_06
                    POINTS05_B = Me.txtPOINTS05_06
                    BALLKIN1F01_B = Me.txtBALLKIN1F01_06
                    BALLKIN1F02_B = Me.txtBALLKIN1F02_06
                    BALLKIN1F03_B = Me.txtBALLKIN1F03_06
                    BALLKIN1F04_B = Me.txtBALLKIN1F04_06
                    BALLKIN1F05_B = Me.txtBALLKIN1F05_06
                    BALLKIN2F01_B = Me.txtBALLKIN2F01_06
                    BALLKIN2F02_B = Me.txtBALLKIN2F02_06
                    BALLKIN2F03_B = Me.txtBALLKIN2F03_06
                    BALLKIN2F04_B = Me.txtBALLKIN2F04_06
                    BALLKIN2F05_B = Me.txtBALLKIN2F05_06
                    SITEIKBN01_B = Me.chkSITEIKBN01_06
                    SITEIKBN02_B = Me.chkSITEIKBN02_06
                    SITEIKBN03_B = Me.chkSITEIKBN03_06
                    SITEIKBN04_B = Me.chkSITEIKBN04_06
                    SITEIKBN05_B = Me.chkSITEIKBN05_06
            End Select

            If String.IsNullOrEmpty(TIMENM01_A.Text) Then
                PASSCD01_B.ReadOnly = True
                ENTKIN01_B.Visible = False
                POINT01_B.Visible = False
                POINTW01_B.Visible = False
                POINTS01_B.Visible = False
                BALLKIN1F01_B.Visible = False
                BALLKIN2F01_B.Visible = False
            Else
                If Not Me.cmbKBMAST.SelectedIndex.Equals(1) Then PASSCD01_B.ReadOnly = False
                ENTKIN01_B.Visible = True
                POINT01_B.Visible = True
                POINTW01_B.Visible = True
                POINTS01_B.Visible = True
                BALLKIN1F01_B.Visible = True
                BALLKIN2F01_B.Visible = True
            End If
            If String.IsNullOrEmpty(TIMENM02_A.Text) Then
                PASSCD02_B.ReadOnly = True
                ENTKIN02_B.Visible = False
                POINT02_B.Visible = False
                POINTW02_B.Visible = False
                POINTS02_B.Visible = False
                BALLKIN1F02_B.Visible = False
                BALLKIN2F02_B.Visible = False
            Else
                If Not Me.cmbKBMAST.SelectedIndex.Equals(1) Then PASSCD02_B.ReadOnly = False
                ENTKIN02_B.Visible = True
                POINT02_B.Visible = True
                POINTW02_B.Visible = True
                POINTS02_B.Visible = True
                BALLKIN1F02_B.Visible = True
                BALLKIN2F02_B.Visible = True
            End If
            If String.IsNullOrEmpty(TIMENM03_A.Text) Then
                PASSCD03_B.ReadOnly = True
                ENTKIN03_B.Visible = False
                POINT03_B.Visible = False
                POINTW03_B.Visible = False
                POINTS03_B.Visible = False
                BALLKIN1F03_B.Visible = False
                BALLKIN2F03_B.Visible = False
            Else
                If Not Me.cmbKBMAST.SelectedIndex.Equals(1) Then PASSCD03_B.ReadOnly = False
                ENTKIN03_B.Visible = True
                POINT03_B.Visible = True
                POINTW03_B.Visible = True
                POINTS03_B.Visible = True
                BALLKIN1F03_B.Visible = True
                BALLKIN2F03_B.Visible = True
            End If
            If String.IsNullOrEmpty(TIMENM04_A.Text) Then
                PASSCD04_B.ReadOnly = True
                ENTKIN04_B.Visible = False
                POINT04_B.Visible = False
                POINTW04_B.Visible = False
                POINTS04_B.Visible = False
                BALLKIN1F04_B.Visible = False
                BALLKIN2F04_B.Visible = False
            Else
                If Not Me.cmbKBMAST.SelectedIndex.Equals(1) Then PASSCD04_B.ReadOnly = False
                ENTKIN04_B.Visible = True
                POINT04_B.Visible = True
                POINTW04_B.Visible = True
                POINTS04_B.Visible = True
                BALLKIN1F04_B.Visible = True
                BALLKIN2F04_B.Visible = True
            End If
            If String.IsNullOrEmpty(TIMENM05_A.Text) Then
                PASSCD05_B.ReadOnly = True
                ENTKIN05_B.Visible = False
                POINT05_B.Visible = False
                POINTW05_B.Visible = False
                POINTS05_B.Visible = False
                BALLKIN1F05_B.Visible = False
                BALLKIN2F05_B.Visible = False
            Else
                If Not Me.cmbKBMAST.SelectedIndex.Equals(1) Then PASSCD05_B.ReadOnly = False
                ENTKIN05_B.Visible = True
                POINT05_B.Visible = True
                POINTW05_B.Visible = True
                POINTS05_B.Visible = True
                BALLKIN1F05_B.Visible = True
                BALLKIN2F05_B.Visible = True
            End If


            TIMENM01_B.Text = TIMENM01_A.Text
            TIMENM02_B.Text = TIMENM02_A.Text
            TIMENM03_B.Text = TIMENM03_A.Text
            TIMENM04_B.Text = TIMENM04_A.Text
            TIMENM05_B.Text = TIMENM05_A.Text
            PASSCD01_B.Text = PASSCD01_A.Text
            PASSCD02_B.Text = PASSCD02_A.Text
            PASSCD03_B.Text = PASSCD03_A.Text
            PASSCD04_B.Text = PASSCD04_A.Text
            PASSCD05_B.Text = PASSCD05_A.Text
            ENTKIN01_B.Text = ENTKIN01_A.Text
            ENTKIN02_B.Text = ENTKIN02_A.Text
            ENTKIN03_B.Text = ENTKIN03_A.Text
            ENTKIN04_B.Text = ENTKIN04_A.Text
            ENTKIN05_B.Text = ENTKIN05_A.Text
            POINT01_B.Text = POINT01_A.Text
            POINT02_B.Text = POINT02_A.Text
            POINT03_B.Text = POINT03_A.Text
            POINT04_B.Text = POINT04_A.Text
            POINT05_B.Text = POINT05_A.Text
            POINTW01_B.Text = POINTW01_A.Text
            POINTW02_B.Text = POINTW02_A.Text
            POINTW03_B.Text = POINTW03_A.Text
            POINTW04_B.Text = POINTW04_A.Text
            POINTW05_B.Text = POINTW05_A.Text
            POINTS01_B.Text = POINTS01_A.Text
            POINTS02_B.Text = POINTS02_A.Text
            POINTS03_B.Text = POINTS03_A.Text
            POINTS04_B.Text = POINTS04_A.Text
            POINTS05_B.Text = POINTS05_A.Text
            BALLKIN1F01_B.Text = BALLKIN1F01_A.Text
            BALLKIN1F02_B.Text = BALLKIN1F02_A.Text
            BALLKIN1F03_B.Text = BALLKIN1F03_A.Text
            BALLKIN1F04_B.Text = BALLKIN1F04_A.Text
            BALLKIN1F05_B.Text = BALLKIN1F05_A.Text
            BALLKIN2F01_B.Text = BALLKIN2F01_A.Text
            BALLKIN2F02_B.Text = BALLKIN2F02_A.Text
            BALLKIN2F03_B.Text = BALLKIN2F03_A.Text
            BALLKIN2F04_B.Text = BALLKIN2F04_A.Text
            BALLKIN2F05_B.Text = BALLKIN2F05_A.Text
            SITEIKBN01_B.Checked = SITEIKBN01_A.Checked
            SITEIKBN02_B.Checked = SITEIKBN02_A.Checked
            SITEIKBN03_B.Checked = SITEIKBN03_A.Checked
            SITEIKBN04_B.Checked = SITEIKBN04_A.Checked
            SITEIKBN05_B.Checked = SITEIKBN05_A.Checked


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' コピーボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy.Click
        Try
            Dim Msg As String
            Msg = Me.cmbCopyKBMAST.Text & "の営業情報をコピーしてよろしいですか？"
            Using frm As New frmMSGBOX01(Msg, 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using

            '画面初期化
            Init()



            '営業情報取得
            GetNkbEIGMTA(True)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 顧客種別コンボボックス_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbKBMAST_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbKBMAST.SelectedIndexChanged
        Try
            '画面初期表示
            Init()



            '営業情報取得
            If Not GetNkbEIGMTA() Then
            End If

            '打席指定区分
            Me.chkSITEIKBN01_01.Enabled = True
            Me.chkSITEIKBN02_01.Enabled = True
            Me.chkSITEIKBN03_01.Enabled = True
            Me.chkSITEIKBN04_01.Enabled = True
            Me.chkSITEIKBN05_01.Enabled = True
            Me.chkSITEIKBN01_02.Enabled = True
            Me.chkSITEIKBN02_02.Enabled = True
            Me.chkSITEIKBN03_02.Enabled = True
            Me.chkSITEIKBN04_02.Enabled = True
            Me.chkSITEIKBN05_02.Enabled = True
            Me.chkSITEIKBN01_03.Enabled = True
            Me.chkSITEIKBN02_03.Enabled = True
            Me.chkSITEIKBN03_03.Enabled = True
            Me.chkSITEIKBN04_03.Enabled = True
            Me.chkSITEIKBN05_03.Enabled = True
            Me.chkSITEIKBN01_04.Enabled = True
            Me.chkSITEIKBN02_04.Enabled = True
            Me.chkSITEIKBN03_04.Enabled = True
            Me.chkSITEIKBN04_04.Enabled = True
            Me.chkSITEIKBN05_04.Enabled = True
            Me.chkSITEIKBN01_05.Enabled = True
            Me.chkSITEIKBN02_05.Enabled = True
            Me.chkSITEIKBN03_05.Enabled = True
            Me.chkSITEIKBN04_05.Enabled = True
            Me.chkSITEIKBN05_05.Enabled = True
            Me.chkSITEIKBN01_06.Enabled = True
            Me.chkSITEIKBN02_06.Enabled = True
            Me.chkSITEIKBN03_06.Enabled = True
            Me.chkSITEIKBN04_06.Enabled = True
            Me.chkSITEIKBN05_06.Enabled = True
            If Not Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.chkSITEIKBN01_01.Enabled = False
                Me.chkSITEIKBN02_01.Enabled = False
                Me.chkSITEIKBN03_01.Enabled = False
                Me.chkSITEIKBN04_01.Enabled = False
                Me.chkSITEIKBN05_01.Enabled = False
                Me.chkSITEIKBN01_02.Enabled = False
                Me.chkSITEIKBN02_02.Enabled = False
                Me.chkSITEIKBN03_02.Enabled = False
                Me.chkSITEIKBN04_02.Enabled = False
                Me.chkSITEIKBN05_02.Enabled = False
                Me.chkSITEIKBN01_03.Enabled = False
                Me.chkSITEIKBN02_03.Enabled = False
                Me.chkSITEIKBN03_03.Enabled = False
                Me.chkSITEIKBN04_03.Enabled = False
                Me.chkSITEIKBN05_03.Enabled = False
                Me.chkSITEIKBN01_04.Enabled = False
                Me.chkSITEIKBN02_04.Enabled = False
                Me.chkSITEIKBN03_04.Enabled = False
                Me.chkSITEIKBN04_04.Enabled = False
                Me.chkSITEIKBN05_04.Enabled = False
                Me.chkSITEIKBN01_05.Enabled = False
                Me.chkSITEIKBN02_05.Enabled = False
                Me.chkSITEIKBN03_05.Enabled = False
                Me.chkSITEIKBN04_05.Enabled = False
                Me.chkSITEIKBN05_05.Enabled = False
                Me.chkSITEIKBN01_06.Enabled = False
                Me.chkSITEIKBN02_06.Enabled = False
                Me.chkSITEIKBN03_06.Enabled = False
                Me.chkSITEIKBN04_06.Enabled = False
                Me.chkSITEIKBN05_06.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

#Region "▼時間帯テキストボックス_Validated"
    ''' <summary>
    ''' 時間帯テキストボックス【平日】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_01_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_01.Validated, txtTIMENM02_01.Validated, txtTIMENM03_01.Validated, txtTIMENM04_01.Validated, txtTIMENM05_01.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM01_01"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD01_01.ReadOnly = True
                        Me.txtPASSCD01_01.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN01_01.Visible = False
                        'ポイント
                        Me.txtPOINT01_01.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW01_01.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS01_01.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F01_01.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F01_01.Visible = False
                    Case "txtTIMENM02_01"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD02_01.ReadOnly = True
                        Me.txtPASSCD02_01.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN02_01.Visible = False
                        'ポイント
                        Me.txtPOINT02_01.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW02_01.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS02_01.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F02_01.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F02_01.Visible = False
                    Case "txtTIMENM03_01"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD03_01.ReadOnly = True
                        Me.txtPASSCD03_01.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN03_01.Visible = False
                        'ポイント
                        Me.txtPOINT03_01.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW03_01.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS03_01.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F03_01.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F03_01.Visible = False

                    Case "txtTIMENM04_01"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD04_01.ReadOnly = True
                        Me.txtPASSCD04_01.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN04_01.Visible = False
                        'ポイント
                        Me.txtPOINT04_01.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW04_01.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS04_01.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F04_01.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F04_01.Visible = False
                    Case "txtTIMENM05_01"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD05_01.ReadOnly = True
                        Me.txtPASSCD05_01.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN05_01.Visible = False
                        'ポイント
                        Me.txtPOINT05_01.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW05_01.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS05_01.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F05_01.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F05_01.Visible = False
                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM01_01"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD01_01.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_01.ReadOnly = False
                    If Me.txtPASSCD01_01.Text.Equals(_cstClrMoji) Then Me.txtPASSCD01_01.Text = "01"
                Case "txtTIMENM02_01"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD02_01.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_01.ReadOnly = False
                    If Me.txtPASSCD02_01.Text.Equals(_cstClrMoji) Then Me.txtPASSCD02_01.Text = "01"
                Case "txtTIMENM03_01"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD03_01.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_01.ReadOnly = False
                    If Me.txtPASSCD03_01.Text.Equals(_cstClrMoji) Then Me.txtPASSCD03_01.Text = "01"
                Case "txtTIMENM04_01"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD04_01.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_01.ReadOnly = False
                    If Me.txtPASSCD04_01.Text.Equals(_cstClrMoji) Then Me.txtPASSCD04_01.Text = "01"
                Case "txtTIMENM05_01"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD05_01.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_01.ReadOnly = False
                    If Me.txtPASSCD05_01.Text.Equals(_cstClrMoji) Then Me.txtPASSCD05_01.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 時間帯テキストボックス【休日】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_02_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_02.Validated, txtTIMENM02_02.Validated, txtTIMENM03_02.Validated, txtTIMENM04_02.Validated, txtTIMENM05_02.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM01_02"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD01_02.ReadOnly = True
                        Me.txtPASSCD01_02.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN01_02.Visible = False
                        'ポイント
                        Me.txtPOINT01_02.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW01_02.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS01_02.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F01_02.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F01_02.Visible = False
                    Case "txtTIMENM02_02"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD02_02.ReadOnly = True
                        Me.txtPASSCD02_02.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN02_02.Visible = False
                        'ポイント
                        Me.txtPOINT02_02.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW02_02.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS02_02.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F02_02.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F02_02.Visible = False
                    Case "txtTIMENM03_02"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD03_02.ReadOnly = True
                        Me.txtPASSCD03_02.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN03_02.Visible = False
                        'ポイント
                        Me.txtPOINT03_02.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW03_02.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS03_02.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F03_02.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F03_02.Visible = False

                    Case "txtTIMENM04_02"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD04_02.ReadOnly = True
                        Me.txtPASSCD04_02.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN04_02.Visible = False
                        'ポイント
                        Me.txtPOINT04_02.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW04_02.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS04_02.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F04_02.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F04_02.Visible = False
                    Case "txtTIMENM05_02"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD05_02.ReadOnly = True
                        Me.txtPASSCD05_02.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN05_02.Visible = False
                        'ポイント
                        Me.txtPOINT05_02.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW05_02.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS05_02.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F05_02.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F05_02.Visible = False
                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM01_02"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD01_02.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_02.ReadOnly = False
                    If Me.txtPASSCD01_02.Text.Equals(_cstClrMoji) Then Me.txtPASSCD01_02.Text = "01"
                Case "txtTIMENM02_02"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD02_02.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_02.ReadOnly = False
                    If Me.txtPASSCD02_02.Text.Equals(_cstClrMoji) Then Me.txtPASSCD02_02.Text = "01"
                Case "txtTIMENM03_02"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD03_02.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_02.ReadOnly = False
                    If Me.txtPASSCD03_02.Text.Equals(_cstClrMoji) Then Me.txtPASSCD03_02.Text = "01"
                Case "txtTIMENM04_02"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD04_02.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_02.ReadOnly = False
                    If Me.txtPASSCD04_02.Text.Equals(_cstClrMoji) Then Me.txtPASSCD04_02.Text = "01"
                Case "txtTIMENM05_02"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD05_02.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_02.ReadOnly = False
                    If Me.txtPASSCD05_02.Text.Equals(_cstClrMoji) Then Me.txtPASSCD05_02.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 時間帯テキストボックス【特日1】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_03_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_03.Validated, txtTIMENM02_03.Validated, txtTIMENM03_03.Validated, txtTIMENM04_03.Validated, txtTIMENM05_03.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM01_03"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD01_03.ReadOnly = True
                        Me.txtPASSCD01_03.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN01_03.Visible = False
                        'ポイント
                        Me.txtPOINT01_03.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW01_03.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS01_03.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F01_03.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F01_03.Visible = False
                    Case "txtTIMENM02_03"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD02_03.ReadOnly = True
                        Me.txtPASSCD02_03.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN02_03.Visible = False
                        'ポイント
                        Me.txtPOINT02_03.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW02_03.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS02_03.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F02_03.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F02_03.Visible = False
                    Case "txtTIMENM03_03"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD03_03.ReadOnly = True
                        Me.txtPASSCD03_03.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN03_03.Visible = False
                        'ポイント
                        Me.txtPOINT03_03.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW03_03.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS03_03.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F03_03.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F03_03.Visible = False

                    Case "txtTIMENM04_03"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD04_03.ReadOnly = True
                        Me.txtPASSCD04_03.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN04_03.Visible = False
                        'ポイント
                        Me.txtPOINT04_03.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW04_03.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS04_03.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F04_03.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F04_03.Visible = False
                    Case "txtTIMENM05_03"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD05_03.ReadOnly = True
                        Me.txtPASSCD05_03.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN05_03.Visible = False
                        'ポイント
                        Me.txtPOINT05_03.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW05_03.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS05_03.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F05_03.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F05_03.Visible = False
                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM01_03"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD01_03.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_03.ReadOnly = False
                    If Me.txtPASSCD01_03.Text.Equals(_cstClrMoji) Then Me.txtPASSCD01_03.Text = "01"
                Case "txtTIMENM02_03"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD02_03.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_03.ReadOnly = False
                    If Me.txtPASSCD02_03.Text.Equals(_cstClrMoji) Then Me.txtPASSCD02_03.Text = "01"
                Case "txtTIMENM03_03"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD03_03.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_03.ReadOnly = False
                    If Me.txtPASSCD03_03.Text.Equals(_cstClrMoji) Then Me.txtPASSCD03_03.Text = "01"
                Case "txtTIMENM04_03"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD04_03.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_03.ReadOnly = False
                    If Me.txtPASSCD04_03.Text.Equals(_cstClrMoji) Then Me.txtPASSCD04_03.Text = "01"
                Case "txtTIMENM05_03"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD05_03.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_03.ReadOnly = False
                    If Me.txtPASSCD05_03.Text.Equals(_cstClrMoji) Then Me.txtPASSCD05_03.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 時間帯テキストボックス【特日2】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_04_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_04.Validated, txtTIMENM02_04.Validated, txtTIMENM03_04.Validated, txtTIMENM04_04.Validated, txtTIMENM05_04.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM01_04"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD01_04.ReadOnly = True
                        Me.txtPASSCD01_04.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN01_04.Visible = False
                        'ポイント
                        Me.txtPOINT01_04.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW01_04.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS01_04.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F01_04.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F01_04.Visible = False
                    Case "txtTIMENM02_04"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD02_04.ReadOnly = True
                        Me.txtPASSCD02_04.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN02_04.Visible = False
                        'ポイント
                        Me.txtPOINT02_04.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW02_04.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS02_04.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F02_04.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F02_04.Visible = False
                    Case "txtTIMENM03_04"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD03_04.ReadOnly = True
                        Me.txtPASSCD03_04.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN03_04.Visible = False
                        'ポイント
                        Me.txtPOINT03_04.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW03_04.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS03_04.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F03_04.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F03_04.Visible = False

                    Case "txtTIMENM04_04"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD04_04.ReadOnly = True
                        Me.txtPASSCD04_04.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN04_04.Visible = False
                        'ポイント
                        Me.txtPOINT04_04.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW04_04.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS04_04.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F04_04.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F04_04.Visible = False
                    Case "txtTIMENM05_04"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD05_04.ReadOnly = True
                        Me.txtPASSCD05_04.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN05_04.Visible = False
                        'ポイント
                        Me.txtPOINT05_04.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW05_04.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS05_04.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F05_04.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F05_04.Visible = False
                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM01_04"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD01_04.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_04.ReadOnly = False
                    If Me.txtPASSCD01_04.Text.Equals(_cstClrMoji) Then Me.txtPASSCD01_04.Text = "01"
                Case "txtTIMENM02_04"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD02_04.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_04.ReadOnly = False
                    If Me.txtPASSCD02_04.Text.Equals(_cstClrMoji) Then Me.txtPASSCD02_04.Text = "01"
                Case "txtTIMENM03_04"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD03_04.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_04.ReadOnly = False
                    If Me.txtPASSCD03_04.Text.Equals(_cstClrMoji) Then Me.txtPASSCD03_04.Text = "01"
                Case "txtTIMENM04_04"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD04_04.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_04.ReadOnly = False
                    If Me.txtPASSCD04_04.Text.Equals(_cstClrMoji) Then Me.txtPASSCD04_04.Text = "01"
                Case "txtTIMENM05_04"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD05_04.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_04.ReadOnly = False
                    If Me.txtPASSCD05_04.Text.Equals(_cstClrMoji) Then Me.txtPASSCD05_04.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 時間帯テキストボックス【特日3】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_05_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_05.Validated, txtTIMENM02_05.Validated, txtTIMENM03_05.Validated, txtTIMENM04_05.Validated, txtTIMENM05_05.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM01_05"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD01_05.ReadOnly = True
                        Me.txtPASSCD01_05.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN01_05.Visible = False
                        'ポイント
                        Me.txtPOINT01_05.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW01_05.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS01_05.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F01_05.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F01_05.Visible = False
                    Case "txtTIMENM02_05"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD02_05.ReadOnly = True
                        Me.txtPASSCD02_05.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN02_05.Visible = False
                        'ポイント
                        Me.txtPOINT02_05.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW02_05.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS02_05.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F02_05.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F02_05.Visible = False
                    Case "txtTIMENM03_05"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD03_05.ReadOnly = True
                        Me.txtPASSCD03_05.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN03_05.Visible = False
                        'ポイント
                        Me.txtPOINT03_05.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW03_05.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS03_05.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F03_05.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F03_05.Visible = False

                    Case "txtTIMENM04_05"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD04_05.ReadOnly = True
                        Me.txtPASSCD04_05.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN04_05.Visible = False
                        'ポイント
                        Me.txtPOINT04_05.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW04_05.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS04_05.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F04_05.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F04_05.Visible = False
                    Case "txtTIMENM05_05"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD05_05.ReadOnly = True
                        Me.txtPASSCD05_05.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN05_05.Visible = False
                        'ポイント
                        Me.txtPOINT05_05.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW05_05.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS05_05.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F05_05.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F05_05.Visible = False
                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM01_05"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD01_05.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_05.ReadOnly = False
                    If Me.txtPASSCD01_05.Text.Equals(_cstClrMoji) Then Me.txtPASSCD01_05.Text = "01"
                Case "txtTIMENM02_05"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD02_05.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_05.ReadOnly = False
                    If Me.txtPASSCD02_05.Text.Equals(_cstClrMoji) Then Me.txtPASSCD02_05.Text = "01"
                Case "txtTIMENM03_05"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD03_05.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_05.ReadOnly = False
                    If Me.txtPASSCD03_05.Text.Equals(_cstClrMoji) Then Me.txtPASSCD03_05.Text = "01"
                Case "txtTIMENM04_05"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD04_05.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_05.ReadOnly = False
                    If Me.txtPASSCD04_05.Text.Equals(_cstClrMoji) Then Me.txtPASSCD04_05.Text = "01"
                Case "txtTIMENM05_05"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD05_05.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_05.ReadOnly = False
                    If Me.txtPASSCD05_05.Text.Equals(_cstClrMoji) Then Me.txtPASSCD05_05.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 時間帯テキストボックス【特日4】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTIMENM_06_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_06.Validated, txtTIMENM02_06.Validated, txtTIMENM03_06.Validated, txtTIMENM04_06.Validated, txtTIMENM05_06.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtTIMENM01_06"
                        '【時間帯1】

                        'パスワード
                        Me.txtPASSCD01_06.ReadOnly = True
                        Me.txtPASSCD01_06.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN01_06.Visible = False
                        'ポイント
                        Me.txtPOINT01_06.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW01_06.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS01_06.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F01_06.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F01_06.Visible = False
                    Case "txtTIMENM02_06"
                        '【時間帯2】

                        'パスワード
                        Me.txtPASSCD02_06.ReadOnly = True
                        Me.txtPASSCD02_06.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN02_06.Visible = False
                        'ポイント
                        Me.txtPOINT02_06.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW02_06.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS02_06.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F02_06.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F02_06.Visible = False
                    Case "txtTIMENM03_06"
                        '【時間帯3】

                        'パスワード
                        Me.txtPASSCD03_06.ReadOnly = True
                        Me.txtPASSCD03_06.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN03_06.Visible = False
                        'ポイント
                        Me.txtPOINT03_06.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW03_06.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS03_06.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F03_06.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F03_06.Visible = False

                    Case "txtTIMENM04_06"
                        '【時間帯4】

                        'パスワード
                        Me.txtPASSCD04_06.ReadOnly = True
                        Me.txtPASSCD04_06.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN04_06.Visible = False
                        'ポイント
                        Me.txtPOINT04_06.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW04_06.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS04_06.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F04_06.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F04_06.Visible = False
                    Case "txtTIMENM05_06"
                        '【時間帯5】

                        'パスワード
                        Me.txtPASSCD05_06.ReadOnly = True
                        Me.txtPASSCD05_06.Text = _cstClrMoji
                        '入場料
                        Me.txtENTKIN05_06.Visible = False
                        'ポイント
                        Me.txtPOINT05_06.Visible = False
                        'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                        Me.txtPOINTW05_06.Visible = False
                        'ｼﾆｱﾎﾟｲﾝﾄ
                        Me.txtPOINTS05_06.Visible = False
                        'ボール単価(1F)
                        Me.txtBALLKIN1F05_06.Visible = False
                        'ボール単価(2F)
                        Me.txtBALLKIN2F05_06.Visible = False
                End Select
                Exit Sub
            End If


            Dim strTIMENM As String = txtBox.Text.Replace(":", String.Empty).PadLeft(4, "0"c)

            If strTIMENM.Substring(0, 2) >= "24" Then strTIMENM = "0000"
            If strTIMENM.Substring(2, 2) >= "60" Then strTIMENM = "0000"

            txtBox.Text = strTIMENM.Substring(0, 2) & ":" & strTIMENM.Substring(2, 2)

            Select Case txtBox.Name
                Case "txtTIMENM01_06"
                    '【時間帯1】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD01_06.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_06.ReadOnly = False
                    If Me.txtPASSCD01_06.Text.Equals(_cstClrMoji) Then Me.txtPASSCD01_06.Text = "01"
                Case "txtTIMENM02_06"
                    '【時間帯2】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD02_06.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_06.ReadOnly = False
                    If Me.txtPASSCD02_06.Text.Equals(_cstClrMoji) Then Me.txtPASSCD02_06.Text = "01"
                Case "txtTIMENM03_06"
                    '【時間帯3】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD03_06.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_06.ReadOnly = False
                    If Me.txtPASSCD03_06.Text.Equals(_cstClrMoji) Then Me.txtPASSCD03_06.Text = "01"
                Case "txtTIMENM04_06"
                    '【時間帯4】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD04_06.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_06.ReadOnly = False
                    If Me.txtPASSCD04_06.Text.Equals(_cstClrMoji) Then Me.txtPASSCD04_06.Text = "01"
                Case "txtTIMENM05_06"
                    '【時間帯5】

                    'パスワード99の時は処理しない
                    If Me.txtPASSCD05_06.Text.Equals("99") Then Exit Sub

                    'パスワード
                    If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_06.ReadOnly = False
                    If Me.txtPASSCD05_06.Text.Equals(_cstClrMoji) Then Me.txtPASSCD05_06.Text = "01"
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "▼パスワードテキストボックス_Validated"

    ''' <summary>
    ''' パスワードテキストボックス【平日】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_01_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD01_01.Validated, txtPASSCD02_01.Validated, txtPASSCD03_01.Validated, txtPASSCD04_01.Validated, txtPASSCD05_01.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD01_01"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD01_01.Text = "01"
                    Case "txtPASSCD02_01"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD02_01.Text = "01"
                    Case "txtPASSCD03_01"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD03_01.Text = "01"
                    Case "txtPASSCD04_01"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD04_01.Text = "01"
                    Case "txtPASSCD05_01"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD05_01.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD01_01"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN01_01.Visible = bln
                    Me.txtENTKIN01_01.Focus()
                    'ポイント
                    Me.txtPOINT01_01.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW01_01.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS01_01.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F01_01.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F01_01.Visible = bln
                Case "txtPASSCD02_01"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN02_01.Visible = bln
                    Me.txtENTKIN02_01.Focus()
                    'ポイント
                    Me.txtPOINT02_01.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW02_01.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS02_01.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F02_01.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F02_01.Visible = bln
                Case "txtPASSCD03_01"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN03_01.Visible = bln
                    Me.txtENTKIN03_01.Focus()
                    'ポイント
                    Me.txtPOINT03_01.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW03_01.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS03_01.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F03_01.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F03_01.Visible = bln
                Case "txtPASSCD04_01"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN04_01.Visible = bln
                    Me.txtENTKIN04_01.Focus()
                    'ポイント
                    Me.txtPOINT04_01.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW04_01.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS04_01.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F04_01.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F04_01.Visible = bln
                Case "txtPASSCD05_01"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN05_01.Visible = bln
                    Me.txtENTKIN05_01.Focus()
                    'ポイント
                    Me.txtPOINT05_01.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW05_01.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS05_01.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F05_01.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F05_01.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' パスワードテキストボックス【休日】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_02_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD01_02.Validated, txtPASSCD02_02.Validated, txtPASSCD03_02.Validated, txtPASSCD04_02.Validated, txtPASSCD05_02.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD01_02"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD01_02.Text = "01"
                    Case "txtPASSCD02_02"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD02_02.Text = "01"
                    Case "txtPASSCD03_02"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD03_02.Text = "01"
                    Case "txtPASSCD04_02"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD04_02.Text = "01"
                    Case "txtPASSCD05_02"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD05_02.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD01_02"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN01_02.Visible = bln
                    Me.txtENTKIN01_02.Focus()
                    'ポイント
                    Me.txtPOINT01_02.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW01_02.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS01_02.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F01_02.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F01_02.Visible = bln
                Case "txtPASSCD02_02"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN02_02.Visible = bln
                    Me.txtENTKIN02_02.Focus()
                    'ポイント
                    Me.txtPOINT02_02.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW02_02.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS02_02.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F02_02.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F02_02.Visible = bln
                Case "txtPASSCD03_02"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN03_02.Visible = bln
                    Me.txtENTKIN03_02.Focus()
                    'ポイント
                    Me.txtPOINT03_02.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW03_02.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS03_02.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F03_02.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F03_02.Visible = bln
                Case "txtPASSCD04_02"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN04_02.Visible = bln
                    Me.txtENTKIN04_02.Focus()
                    'ポイント
                    Me.txtPOINT04_02.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW04_02.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS04_02.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F04_02.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F04_02.Visible = bln
                Case "txtPASSCD05_02"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN05_02.Visible = bln
                    Me.txtENTKIN05_02.Focus()
                    'ポイント
                    Me.txtPOINT05_02.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW05_02.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS05_02.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F05_02.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F05_02.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' パスワードテキストボックス【特日1】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_03_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD01_03.Validated, txtPASSCD02_03.Validated, txtPASSCD03_03.Validated, txtPASSCD04_03.Validated, txtPASSCD05_03.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD01_03"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD01_03.Text = "01"
                    Case "txtPASSCD02_03"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD02_03.Text = "01"
                    Case "txtPASSCD03_03"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD03_03.Text = "01"
                    Case "txtPASSCD04_03"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD04_03.Text = "01"
                    Case "txtPASSCD05_03"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD05_03.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD01_03"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN01_03.Visible = bln
                    Me.txtENTKIN01_03.Focus()
                    'ポイント
                    Me.txtPOINT01_03.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW01_03.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS01_03.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F01_03.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F01_03.Visible = bln
                Case "txtPASSCD02_03"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN02_03.Visible = bln
                    Me.txtENTKIN02_03.Focus()
                    'ポイント
                    Me.txtPOINT02_03.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW02_03.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS02_03.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F02_03.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F02_03.Visible = bln
                Case "txtPASSCD03_03"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN03_03.Visible = bln
                    Me.txtENTKIN03_03.Focus()
                    'ポイント
                    Me.txtPOINT03_03.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW03_03.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS03_03.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F03_03.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F03_03.Visible = bln
                Case "txtPASSCD04_03"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN04_03.Visible = bln
                    Me.txtENTKIN04_03.Focus()
                    'ポイント
                    Me.txtPOINT04_03.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW04_03.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS04_03.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F04_03.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F04_03.Visible = bln
                Case "txtPASSCD05_03"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN05_03.Visible = bln
                    Me.txtENTKIN05_03.Focus()
                    'ポイント
                    Me.txtPOINT05_03.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW05_03.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS05_03.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F05_03.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F05_03.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' パスワードテキストボックス【特日2】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_04_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD01_04.Validated, txtPASSCD02_04.Validated, txtPASSCD03_04.Validated, txtPASSCD04_04.Validated, txtPASSCD05_04.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD01_04"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD01_04.Text = "01"
                    Case "txtPASSCD02_04"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD02_04.Text = "01"
                    Case "txtPASSCD03_04"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD03_04.Text = "01"
                    Case "txtPASSCD04_04"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD04_04.Text = "01"
                    Case "txtPASSCD05_04"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD05_04.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD01_04"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN01_04.Visible = bln
                    Me.txtENTKIN01_04.Focus()
                    'ポイント
                    Me.txtPOINT01_04.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW01_04.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS01_04.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F01_04.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F01_04.Visible = bln
                Case "txtPASSCD02_04"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN02_04.Visible = bln
                    Me.txtENTKIN02_04.Focus()
                    'ポイント
                    Me.txtPOINT02_04.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW02_04.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS02_04.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F02_04.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F02_04.Visible = bln
                Case "txtPASSCD03_04"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN03_04.Visible = bln
                    Me.txtENTKIN03_04.Focus()
                    'ポイント
                    Me.txtPOINT03_04.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW03_04.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS03_04.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F03_04.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F03_04.Visible = bln
                Case "txtPASSCD04_04"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN04_04.Visible = bln
                    Me.txtENTKIN04_04.Focus()
                    'ポイント
                    Me.txtPOINT04_04.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW04_04.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS04_04.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F04_04.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F04_04.Visible = bln

                Case "txtPASSCD05_04"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN05_04.Visible = bln
                    Me.txtENTKIN05_04.Focus()
                    'ポイント
                    Me.txtPOINT05_04.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW05_04.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS05_04.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F05_04.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F05_04.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' パスワードテキストボックス【特日3】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_05_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD01_05.Validated, txtPASSCD02_05.Validated, txtPASSCD03_05.Validated, txtPASSCD04_05.Validated, txtPASSCD05_05.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD01_05"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD01_05.Text = "01"
                    Case "txtPASSCD02_05"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD02_05.Text = "01"
                    Case "txtPASSCD03_05"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD03_05.Text = "01"
                    Case "txtPASSCD04_05"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD04_05.Text = "01"
                    Case "txtPASSCD05_05"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD05_05.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD01_05"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN01_05.Visible = bln
                    Me.txtENTKIN01_05.Focus()
                    'ポイント
                    Me.txtPOINT01_05.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW01_05.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS01_05.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F01_05.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F01_05.Visible = bln
                Case "txtPASSCD02_05"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN02_05.Visible = bln
                    Me.txtENTKIN02_05.Focus()
                    'ポイント
                    Me.txtPOINT02_05.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW02_05.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS02_05.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F02_05.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F02_05.Visible = bln
                Case "txtPASSCD03_05"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN03_05.Visible = bln
                    Me.txtENTKIN03_05.Focus()
                    'ポイント
                    Me.txtPOINT03_05.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW03_05.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS03_05.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F03_05.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F03_05.Visible = bln
                Case "txtPASSCD04_05"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN04_05.Visible = bln
                    Me.txtENTKIN04_05.Focus()
                    'ポイント
                    Me.txtPOINT04_05.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW04_05.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS04_05.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F04_05.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F04_05.Visible = bln
                Case "txtPASSCD05_05"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN05_05.Visible = bln
                    Me.txtENTKIN05_05.Focus()
                    'ポイント
                    Me.txtPOINT05_05.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW05_05.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS05_05.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F05_05.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F05_05.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' パスワードテキストボックス【特日4】_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPASSCD_06_Validated(sender As System.Object, e As System.EventArgs) Handles txtPASSCD01_06.Validated, txtPASSCD02_06.Validated, txtPASSCD03_06.Validated, txtPASSCD04_06.Validated, txtPASSCD05_06.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If txtBox.Text.Equals(_cstClrMoji) Then Exit Sub

            If String.IsNullOrEmpty(txtBox.Text) Then
                Select Case txtBox.Name
                    Case "txtPASSCD01_06"
                        '【時間帯1】
                        'パスワード
                        Me.txtPASSCD01_06.Text = "01"
                    Case "txtPASSCD02_06"
                        '【時間帯2】
                        'パスワード
                        Me.txtPASSCD02_06.Text = "01"
                    Case "txtPASSCD03_06"
                        '【時間帯3】
                        'パスワード
                        Me.txtPASSCD03_06.Text = "01"
                    Case "txtPASSCD04_06"
                        '【時間帯4】
                        'パスワード
                        Me.txtPASSCD04_06.Text = "01"
                    Case "txtPASSCD05_06"
                        '【時間帯5】
                        'パスワード
                        Me.txtPASSCD05_06.Text = "01"
                End Select
                Exit Sub
            End If

            txtBox.Text = txtBox.Text.PadLeft(2, "0"c)

            Dim bln As Boolean = False

            Select Case txtBox.Name
                Case "txtPASSCD01_06"
                    '【時間帯1】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN01_06.Visible = bln
                    Me.txtENTKIN01_06.Focus()
                    'ポイント
                    Me.txtPOINT01_06.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW01_06.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS01_06.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F01_06.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F01_06.Visible = bln
                Case "txtPASSCD02_06"
                    '【時間帯2】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN02_06.Visible = bln
                    Me.txtENTKIN02_06.Focus()
                    'ポイント
                    Me.txtPOINT02_06.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW02_06.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS02_06.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F02_06.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F02_06.Visible = bln
                Case "txtPASSCD03_06"
                    '【時間帯3】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN03_06.Visible = bln
                    Me.txtENTKIN03_06.Focus()
                    'ポイント
                    Me.txtPOINT03_06.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW03_06.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS03_06.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F03_06.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F03_06.Visible = bln
                Case "txtPASSCD04_06"
                    '【時間帯4】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN04_06.Visible = bln
                    Me.txtENTKIN04_06.Focus()
                    'ポイント
                    Me.txtPOINT04_06.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW04_06.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS04_06.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F04_06.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F04_06.Visible = bln
                Case "txtPASSCD05_06"
                    '【時間帯5】

                    If txtBox.Text.Equals("99") Then
                        bln = False
                    Else
                        bln = True
                    End If
                    '入場料
                    Me.txtENTKIN05_06.Visible = bln
                    Me.txtENTKIN05_06.Focus()
                    'ポイント
                    Me.txtPOINT05_06.Visible = bln
                    'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                    Me.txtPOINTW05_06.Visible = bln
                    'ｼﾆｱﾎﾟｲﾝﾄ
                    Me.txtPOINTS05_06.Visible = bln
                    'ボール単価(1F)
                    Me.txtBALLKIN1F05_06.Visible = bln
                    'ボール単価(2F)
                    Me.txtBALLKIN2F05_06.Visible = bln
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' 入場料テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtENTKIN_Validated(sender As System.Object, e As System.EventArgs) Handles txtENTKIN01_01.Validated, txtENTKIN02_01.Validated, txtENTKIN03_01.Validated, txtENTKIN04_01.Validated, txtENTKIN05_01.Validated _
                                                                                        , txtENTKIN01_02.Validated, txtENTKIN02_02.Validated, txtENTKIN03_02.Validated, txtENTKIN04_02.Validated, txtENTKIN05_02.Validated _
                                                                                        , txtENTKIN01_03.Validated, txtENTKIN02_03.Validated, txtENTKIN03_03.Validated, txtENTKIN04_03.Validated, txtENTKIN05_03.Validated _
                                                                                        , txtENTKIN01_04.Validated, txtENTKIN02_04.Validated, txtENTKIN03_04.Validated, txtENTKIN04_04.Validated, txtENTKIN05_04.Validated _
                                                                                        , txtENTKIN01_05.Validated, txtENTKIN02_05.Validated, txtENTKIN03_05.Validated, txtENTKIN04_05.Validated, txtENTKIN05_05.Validated _
                                                                                        , txtENTKIN01_06.Validated, txtENTKIN02_06.Validated, txtENTKIN03_06.Validated, txtENTKIN04_06.Validated, txtENTKIN05_06.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ポイントテキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPOINT_Validated(sender As System.Object, e As System.EventArgs) Handles txtPOINT01_01.Validated, txtPOINT02_01.Validated, txtPOINT03_01.Validated, txtPOINT04_01.Validated, txtPOINT05_01.Validated _
                                                                                        , txtPOINT01_02.Validated, txtPOINT02_02.Validated, txtPOINT03_02.Validated, txtPOINT04_02.Validated, txtPOINT05_02.Validated _
                                                                                        , txtPOINT01_03.Validated, txtPOINT02_03.Validated, txtPOINT03_03.Validated, txtPOINT04_03.Validated, txtPOINT05_03.Validated _
                                                                                        , txtPOINT01_04.Validated, txtPOINT02_04.Validated, txtPOINT03_04.Validated, txtPOINT04_04.Validated, txtPOINT05_04.Validated _
                                                                                        , txtPOINT01_05.Validated, txtPOINT02_05.Validated, txtPOINT03_05.Validated, txtPOINT04_05.Validated, txtPOINT05_05.Validated _
                                                                                        , txtPOINT01_06.Validated, txtPOINT02_06.Validated, txtPOINT03_06.Validated, txtPOINT04_06.Validated, txtPOINT05_06.Validated _
                                                                                        , txtPOINTW01_01.Validated, txtPOINTW02_01.Validated, txtPOINTW03_01.Validated, txtPOINTW04_01.Validated, txtPOINTW05_01.Validated _
                                                                                        , txtPOINTW01_02.Validated, txtPOINTW02_02.Validated, txtPOINTW03_02.Validated, txtPOINTW04_02.Validated, txtPOINTW05_02.Validated _
                                                                                        , txtPOINTW01_03.Validated, txtPOINTW02_03.Validated, txtPOINTW03_03.Validated, txtPOINTW04_03.Validated, txtPOINTW05_03.Validated _
                                                                                        , txtPOINTW01_04.Validated, txtPOINTW02_04.Validated, txtPOINTW03_04.Validated, txtPOINTW04_04.Validated, txtPOINTW05_04.Validated _
                                                                                        , txtPOINTW01_05.Validated, txtPOINTW02_05.Validated, txtPOINTW03_05.Validated, txtPOINTW04_05.Validated, txtPOINTW05_05.Validated _
                                                                                        , txtPOINTW01_06.Validated, txtPOINTW02_06.Validated, txtPOINTW03_06.Validated, txtPOINTW04_06.Validated, txtPOINTW05_06.Validated _
                                                                                        , txtPOINTS01_01.Validated, txtPOINTS02_01.Validated, txtPOINTS03_01.Validated, txtPOINTS04_01.Validated, txtPOINTS05_01.Validated _
                                                                                        , txtPOINTS01_02.Validated, txtPOINTS02_02.Validated, txtPOINTS03_02.Validated, txtPOINTS04_02.Validated, txtPOINTS05_02.Validated _
                                                                                        , txtPOINTS01_03.Validated, txtPOINTS02_03.Validated, txtPOINTS03_03.Validated, txtPOINTS04_03.Validated, txtPOINTS05_03.Validated _
                                                                                        , txtPOINTS01_04.Validated, txtPOINTS02_04.Validated, txtPOINTS03_04.Validated, txtPOINTS04_04.Validated, txtPOINTS05_04.Validated _
                                                                                        , txtPOINTS01_05.Validated, txtPOINTS02_05.Validated, txtPOINTS03_05.Validated, txtPOINTS04_05.Validated, txtPOINTS05_05.Validated _
                                                                                        , txtPOINTS01_06.Validated, txtPOINTS02_06.Validated, txtPOINTS03_06.Validated, txtPOINTS04_06.Validated, txtPOINTS05_06.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ボール単価テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBALLKIN_Validated(sender As System.Object, e As System.EventArgs) Handles txtBALLKIN1F01_01.Validated, txtBALLKIN1F02_01.Validated, txtBALLKIN1F03_01.Validated, txtBALLKIN1F04_01.Validated, txtBALLKIN1F05_01.Validated _
                                                                                            , txtBALLKIN2F01_01.Validated, txtBALLKIN2F02_01.Validated, txtBALLKIN2F03_01.Validated, txtBALLKIN2F04_01.Validated, txtBALLKIN2F05_01.Validated _
                                                                                            , txtBALLKIN1F01_02.Validated, txtBALLKIN1F02_02.Validated, txtBALLKIN1F03_02.Validated, txtBALLKIN1F04_02.Validated, txtBALLKIN1F05_02.Validated _
                                                                                            , txtBALLKIN2F01_02.Validated, txtBALLKIN2F02_02.Validated, txtBALLKIN2F03_02.Validated, txtBALLKIN2F04_02.Validated, txtBALLKIN2F05_02.Validated _
                                                                                            , txtBALLKIN1F01_03.Validated, txtBALLKIN1F02_03.Validated, txtBALLKIN1F03_03.Validated, txtBALLKIN1F04_03.Validated, txtBALLKIN1F05_03.Validated _
                                                                                            , txtBALLKIN2F01_03.Validated, txtBALLKIN2F02_03.Validated, txtBALLKIN2F03_03.Validated, txtBALLKIN2F04_03.Validated, txtBALLKIN2F05_03.Validated _
                                                                                            , txtBALLKIN1F01_04.Validated, txtBALLKIN1F02_04.Validated, txtBALLKIN1F03_04.Validated, txtBALLKIN1F04_04.Validated, txtBALLKIN1F05_04.Validated _
                                                                                            , txtBALLKIN2F01_04.Validated, txtBALLKIN2F02_04.Validated, txtBALLKIN2F03_04.Validated, txtBALLKIN2F04_04.Validated, txtBALLKIN2F05_04.Validated _
                                                                                            , txtBALLKIN1F01_05.Validated, txtBALLKIN1F02_05.Validated, txtBALLKIN1F03_05.Validated, txtBALLKIN1F04_05.Validated, txtBALLKIN1F05_05.Validated _
                                                                                            , txtBALLKIN2F01_05.Validated, txtBALLKIN2F02_05.Validated, txtBALLKIN2F03_05.Validated, txtBALLKIN2F04_05.Validated, txtBALLKIN2F05_05.Validated _
                                                                                            , txtBALLKIN1F01_06.Validated, txtBALLKIN1F02_06.Validated, txtBALLKIN1F03_06.Validated, txtBALLKIN1F04_06.Validated, txtBALLKIN1F05_06.Validated _
                                                                                            , txtBALLKIN2F01_06.Validated, txtBALLKIN2F02_06.Validated, txtBALLKIN2F03_06.Validated, txtBALLKIN2F04_06.Validated, txtBALLKIN2F05_06.Validated

        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = "0"
            Else
                txtBox.Text = CType(txtBox.Text, Integer).ToString("#,##0")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTIMENM01_01.KeyPress, txtTIMENM02_01.KeyPress, txtTIMENM03_01.KeyPress, txtTIMENM04_01.KeyPress, txtTIMENM05_01.KeyPress _
                                                                                                                        , txtPASSCD01_01.KeyPress, txtPASSCD02_01.KeyPress, txtPASSCD03_01.KeyPress, txtPASSCD04_01.KeyPress, txtPASSCD05_01.KeyPress _
                                                                                                                        , txtENTKIN01_01.KeyPress, txtENTKIN02_01.KeyPress, txtENTKIN03_01.KeyPress, txtENTKIN04_01.KeyPress, txtENTKIN05_01.KeyPress _
                                                                                                                        , txtPOINT01_01.KeyPress, txtPOINT02_01.KeyPress, txtPOINT03_01.KeyPress, txtPOINT04_01.KeyPress, txtPOINT05_01.KeyPress _
                                                                                                                        , txtPOINTW01_01.KeyPress, txtPOINTW02_01.KeyPress, txtPOINTW03_01.KeyPress, txtPOINTW04_01.KeyPress, txtPOINTW05_01.KeyPress _
                                                                                                                        , txtPOINTS01_01.KeyPress, txtPOINTS02_01.KeyPress, txtPOINTS03_01.KeyPress, txtPOINTS04_01.KeyPress, txtPOINTS05_01.KeyPress _
                                                                                                                        , txtBALLKIN1F01_01.KeyPress, txtBALLKIN1F02_01.KeyPress, txtBALLKIN1F03_01.KeyPress, txtBALLKIN1F04_01.KeyPress, txtBALLKIN1F05_01.KeyPress _
                                                                                                                        , txtBALLKIN2F01_01.KeyPress, txtBALLKIN2F02_01.KeyPress, txtBALLKIN2F03_01.KeyPress, txtBALLKIN2F04_01.KeyPress, txtBALLKIN2F05_01.KeyPress _
                                                                                                                        , txtTIMENM01_02.KeyPress, txtTIMENM02_02.KeyPress, txtTIMENM03_02.KeyPress, txtTIMENM04_02.KeyPress, txtTIMENM05_02.KeyPress _
                                                                                                                        , txtPASSCD01_02.KeyPress, txtPASSCD02_02.KeyPress, txtPASSCD03_02.KeyPress, txtPASSCD04_02.KeyPress, txtPASSCD05_02.KeyPress _
                                                                                                                        , txtENTKIN01_02.KeyPress, txtENTKIN02_02.KeyPress, txtENTKIN03_02.KeyPress, txtENTKIN04_02.KeyPress, txtENTKIN05_02.KeyPress _
                                                                                                                        , txtPOINT01_02.KeyPress, txtPOINT02_02.KeyPress, txtPOINT03_02.KeyPress, txtPOINT04_02.KeyPress, txtPOINT05_02.KeyPress _
                                                                                                                        , txtPOINTW01_02.KeyPress, txtPOINTW02_02.KeyPress, txtPOINTW03_02.KeyPress, txtPOINTW04_02.KeyPress, txtPOINTW05_02.KeyPress _
                                                                                                                        , txtPOINTS01_02.KeyPress, txtPOINTS02_02.KeyPress, txtPOINTS03_02.KeyPress, txtPOINTS04_02.KeyPress, txtPOINTS05_02.KeyPress _
                                                                                                                        , txtBALLKIN1F01_02.KeyPress, txtBALLKIN1F02_02.KeyPress, txtBALLKIN1F03_02.KeyPress, txtBALLKIN1F04_02.KeyPress, txtBALLKIN1F05_02.KeyPress _
                                                                                                                        , txtBALLKIN2F01_02.KeyPress, txtBALLKIN2F02_02.KeyPress, txtBALLKIN2F03_02.KeyPress, txtBALLKIN2F04_02.KeyPress, txtBALLKIN2F05_02.KeyPress _
                                                                                                                        , txtTIMENM01_03.KeyPress, txtTIMENM02_03.KeyPress, txtTIMENM03_03.KeyPress, txtTIMENM04_03.KeyPress, txtTIMENM05_03.KeyPress _
                                                                                                                        , txtPASSCD01_03.KeyPress, txtPASSCD02_03.KeyPress, txtPASSCD03_03.KeyPress, txtPASSCD04_03.KeyPress, txtPASSCD05_03.KeyPress _
                                                                                                                        , txtENTKIN01_03.KeyPress, txtENTKIN02_03.KeyPress, txtENTKIN03_03.KeyPress, txtENTKIN04_03.KeyPress, txtENTKIN05_03.KeyPress _
                                                                                                                        , txtPOINT01_03.KeyPress, txtPOINT02_03.KeyPress, txtPOINT03_03.KeyPress, txtPOINT04_03.KeyPress, txtPOINT05_03.KeyPress _
                                                                                                                        , txtPOINTW01_03.KeyPress, txtPOINTW02_03.KeyPress, txtPOINTW03_03.KeyPress, txtPOINTW04_03.KeyPress, txtPOINTW05_03.KeyPress _
                                                                                                                        , txtPOINTS01_03.KeyPress, txtPOINTS02_03.KeyPress, txtPOINTS03_03.KeyPress, txtPOINTS04_03.KeyPress, txtPOINTS05_03.KeyPress _
                                                                                                                        , txtBALLKIN1F01_03.KeyPress, txtBALLKIN1F02_03.KeyPress, txtBALLKIN1F03_03.KeyPress, txtBALLKIN1F04_03.KeyPress, txtBALLKIN1F05_03.KeyPress _
                                                                                                                        , txtBALLKIN2F01_03.KeyPress, txtBALLKIN2F02_03.KeyPress, txtBALLKIN2F03_03.KeyPress, txtBALLKIN2F04_03.KeyPress, txtBALLKIN2F05_03.KeyPress _
                                                                                                                        , txtTIMENM01_04.KeyPress, txtTIMENM02_04.KeyPress, txtTIMENM03_04.KeyPress, txtTIMENM04_04.KeyPress, txtTIMENM05_04.KeyPress _
                                                                                                                        , txtPASSCD01_04.KeyPress, txtPASSCD02_04.KeyPress, txtPASSCD03_04.KeyPress, txtPASSCD04_04.KeyPress, txtPASSCD05_04.KeyPress _
                                                                                                                        , txtENTKIN01_04.KeyPress, txtENTKIN02_04.KeyPress, txtENTKIN03_04.KeyPress, txtENTKIN04_04.KeyPress, txtENTKIN05_04.KeyPress _
                                                                                                                        , txtPOINT01_04.KeyPress, txtPOINT02_04.KeyPress, txtPOINT03_04.KeyPress, txtPOINT04_04.KeyPress, txtPOINT05_04.KeyPress _
                                                                                                                        , txtPOINTW01_04.KeyPress, txtPOINTW02_04.KeyPress, txtPOINTW03_04.KeyPress, txtPOINTW04_04.KeyPress, txtPOINTW05_04.KeyPress _
                                                                                                                        , txtPOINTS01_04.KeyPress, txtPOINTS02_04.KeyPress, txtPOINTS03_04.KeyPress, txtPOINTS04_04.KeyPress, txtPOINTS05_04.KeyPress _
                                                                                                                        , txtBALLKIN1F01_04.KeyPress, txtBALLKIN1F02_04.KeyPress, txtBALLKIN1F03_04.KeyPress, txtBALLKIN1F04_04.KeyPress, txtBALLKIN1F05_04.KeyPress _
                                                                                                                        , txtBALLKIN2F01_04.KeyPress, txtBALLKIN2F02_04.KeyPress, txtBALLKIN2F03_04.KeyPress, txtBALLKIN2F04_04.KeyPress, txtBALLKIN2F05_04.KeyPress _
                                                                                                                        , txtTIMENM01_05.KeyPress, txtTIMENM02_05.KeyPress, txtTIMENM03_05.KeyPress, txtTIMENM04_05.KeyPress, txtTIMENM05_05.KeyPress _
                                                                                                                        , txtPASSCD01_05.KeyPress, txtPASSCD02_05.KeyPress, txtPASSCD03_05.KeyPress, txtPASSCD04_05.KeyPress, txtPASSCD05_05.KeyPress _
                                                                                                                        , txtENTKIN01_05.KeyPress, txtENTKIN02_05.KeyPress, txtENTKIN03_05.KeyPress, txtENTKIN04_05.KeyPress, txtENTKIN05_05.KeyPress _
                                                                                                                        , txtPOINT01_05.KeyPress, txtPOINT02_05.KeyPress, txtPOINT03_05.KeyPress, txtPOINT04_05.KeyPress, txtPOINT05_05.KeyPress _
                                                                                                                        , txtPOINTW01_05.KeyPress, txtPOINTW02_05.KeyPress, txtPOINTW03_05.KeyPress, txtPOINTW04_05.KeyPress, txtPOINTW05_05.KeyPress _
                                                                                                                        , txtPOINTS01_05.KeyPress, txtPOINTS02_05.KeyPress, txtPOINTS03_05.KeyPress, txtPOINTS04_05.KeyPress, txtPOINTS05_05.KeyPress _
                                                                                                                        , txtBALLKIN1F01_05.KeyPress, txtBALLKIN1F02_05.KeyPress, txtBALLKIN1F03_05.KeyPress, txtBALLKIN1F04_05.KeyPress, txtBALLKIN1F05_05.KeyPress _
                                                                                                                        , txtBALLKIN2F01_05.KeyPress, txtBALLKIN2F02_05.KeyPress, txtBALLKIN2F03_05.KeyPress, txtBALLKIN2F04_05.KeyPress, txtBALLKIN2F05_05.KeyPress _
                                                                                                                        , txtTIMENM01_06.KeyPress, txtTIMENM02_06.KeyPress, txtTIMENM03_06.KeyPress, txtTIMENM04_06.KeyPress, txtTIMENM05_06.KeyPress _
                                                                                                                        , txtPASSCD01_06.KeyPress, txtPASSCD02_06.KeyPress, txtPASSCD03_06.KeyPress, txtPASSCD04_06.KeyPress, txtPASSCD05_06.KeyPress _
                                                                                                                        , txtENTKIN01_06.KeyPress, txtENTKIN02_06.KeyPress, txtENTKIN03_06.KeyPress, txtENTKIN04_06.KeyPress, txtENTKIN05_06.KeyPress _
                                                                                                                        , txtPOINT01_06.KeyPress, txtPOINT02_06.KeyPress, txtPOINT03_06.KeyPress, txtPOINT04_06.KeyPress, txtPOINT05_06.KeyPress _
                                                                                                                        , txtPOINTW01_06.KeyPress, txtPOINTW02_06.KeyPress, txtPOINTW03_06.KeyPress, txtPOINTW04_06.KeyPress, txtPOINTW05_06.KeyPress _
                                                                                                                        , txtPOINTS01_06.KeyPress, txtPOINTS02_06.KeyPress, txtPOINTS03_06.KeyPress, txtPOINTS04_06.KeyPress, txtPOINTS05_06.KeyPress _
                                                                                                                        , txtBALLKIN1F01_06.KeyPress, txtBALLKIN1F02_06.KeyPress, txtBALLKIN1F03_06.KeyPress, txtBALLKIN1F04_06.KeyPress, txtBALLKIN1F05_06.KeyPress _
                                                                                                                        , txtBALLKIN2F01_06.KeyPress, txtBALLKIN2F02_06.KeyPress, txtBALLKIN2F03_06.KeyPress, txtBALLKIN2F04_06.KeyPress, txtBALLKIN2F05_06.KeyPress

        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_MouseDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtTIMENM01_01.MouseDown, txtTIMENM02_01.MouseDown, txtTIMENM03_01.MouseDown, txtTIMENM04_01.MouseDown, txtTIMENM05_01.MouseDown _
                                                                                                                        , txtPASSCD01_01.MouseDown, txtPASSCD02_01.MouseDown, txtPASSCD03_01.MouseDown, txtPASSCD04_01.MouseDown, txtPASSCD05_01.MouseDown _
                                                                                                                        , txtENTKIN01_01.MouseDown, txtENTKIN02_01.MouseDown, txtENTKIN03_01.MouseDown, txtENTKIN04_01.MouseDown, txtENTKIN05_01.MouseDown _
                                                                                                                        , txtPOINT01_01.MouseDown, txtPOINT02_01.MouseDown, txtPOINT03_01.MouseDown, txtPOINT04_01.MouseDown, txtPOINT05_01.MouseDown _
                                                                                                                        , txtPOINTW01_01.MouseDown, txtPOINTW02_01.MouseDown, txtPOINTW03_01.MouseDown, txtPOINTW04_01.MouseDown, txtPOINTW05_01.MouseDown _
                                                                                                                        , txtPOINTS01_01.MouseDown, txtPOINTS02_01.MouseDown, txtPOINTS03_01.MouseDown, txtPOINTS04_01.MouseDown, txtPOINTS05_01.MouseDown _
                                                                                                                        , txtBALLKIN1F01_01.MouseDown, txtBALLKIN1F02_01.MouseDown, txtBALLKIN1F03_01.MouseDown, txtBALLKIN1F04_01.MouseDown, txtBALLKIN1F05_01.MouseDown _
                                                                                                                        , txtBALLKIN2F01_01.MouseDown, txtBALLKIN2F02_01.MouseDown, txtBALLKIN2F03_01.MouseDown, txtBALLKIN2F04_01.MouseDown, txtBALLKIN2F05_01.MouseDown _
                                                                                                                        , txtTIMENM01_02.MouseDown, txtTIMENM02_02.MouseDown, txtTIMENM03_02.MouseDown, txtTIMENM04_02.MouseDown, txtTIMENM05_02.MouseDown _
                                                                                                                        , txtPASSCD01_02.MouseDown, txtPASSCD02_02.MouseDown, txtPASSCD03_02.MouseDown, txtPASSCD04_02.MouseDown, txtPASSCD05_02.MouseDown _
                                                                                                                        , txtENTKIN01_02.MouseDown, txtENTKIN02_02.MouseDown, txtENTKIN03_02.MouseDown, txtENTKIN04_02.MouseDown, txtENTKIN05_02.MouseDown _
                                                                                                                        , txtPOINT01_02.MouseDown, txtPOINT02_02.MouseDown, txtPOINT03_02.MouseDown, txtPOINT04_02.MouseDown, txtPOINT05_02.MouseDown _
                                                                                                                        , txtPOINTW01_02.MouseDown, txtPOINTW02_02.MouseDown, txtPOINTW03_02.MouseDown, txtPOINTW04_02.MouseDown, txtPOINTW05_02.MouseDown _
                                                                                                                        , txtPOINTS01_02.MouseDown, txtPOINTS02_02.MouseDown, txtPOINTS03_02.MouseDown, txtPOINTS04_02.MouseDown, txtPOINTS05_02.MouseDown _
                                                                                                                        , txtBALLKIN1F01_02.MouseDown, txtBALLKIN1F02_02.MouseDown, txtBALLKIN1F03_02.MouseDown, txtBALLKIN1F04_02.MouseDown, txtBALLKIN1F05_02.MouseDown _
                                                                                                                        , txtBALLKIN2F01_02.MouseDown, txtBALLKIN2F02_02.MouseDown, txtBALLKIN2F03_02.MouseDown, txtBALLKIN2F04_02.MouseDown, txtBALLKIN2F05_02.MouseDown _
                                                                                                                        , txtTIMENM01_03.MouseDown, txtTIMENM02_03.MouseDown, txtTIMENM03_03.MouseDown, txtTIMENM04_03.MouseDown, txtTIMENM05_03.MouseDown _
                                                                                                                        , txtPASSCD01_03.MouseDown, txtPASSCD02_03.MouseDown, txtPASSCD03_03.MouseDown, txtPASSCD04_03.MouseDown, txtPASSCD05_03.MouseDown _
                                                                                                                        , txtENTKIN01_03.MouseDown, txtENTKIN02_03.MouseDown, txtENTKIN03_03.MouseDown, txtENTKIN04_03.MouseDown, txtENTKIN05_03.MouseDown _
                                                                                                                        , txtPOINT01_03.MouseDown, txtPOINT02_03.MouseDown, txtPOINT03_03.MouseDown, txtPOINT04_03.MouseDown, txtPOINT05_03.MouseDown _
                                                                                                                        , txtPOINTW01_03.MouseDown, txtPOINTW02_03.MouseDown, txtPOINTW03_03.MouseDown, txtPOINTW04_03.MouseDown, txtPOINTW05_03.MouseDown _
                                                                                                                        , txtPOINTS01_03.MouseDown, txtPOINTS02_03.MouseDown, txtPOINTS03_03.MouseDown, txtPOINTS04_03.MouseDown, txtPOINTS05_03.MouseDown _
                                                                                                                        , txtBALLKIN1F01_03.MouseDown, txtBALLKIN1F02_03.MouseDown, txtBALLKIN1F03_03.MouseDown, txtBALLKIN1F04_03.MouseDown, txtBALLKIN1F05_03.MouseDown _
                                                                                                                        , txtBALLKIN2F01_03.MouseDown, txtBALLKIN2F02_03.MouseDown, txtBALLKIN2F03_03.MouseDown, txtBALLKIN2F04_03.MouseDown, txtBALLKIN2F05_03.MouseDown _
                                                                                                                        , txtTIMENM01_04.MouseDown, txtTIMENM02_04.MouseDown, txtTIMENM03_04.MouseDown, txtTIMENM04_04.MouseDown, txtTIMENM05_04.MouseDown _
                                                                                                                        , txtPASSCD01_04.MouseDown, txtPASSCD02_04.MouseDown, txtPASSCD03_04.MouseDown, txtPASSCD04_04.MouseDown, txtPASSCD05_04.MouseDown _
                                                                                                                        , txtENTKIN01_04.MouseDown, txtENTKIN02_04.MouseDown, txtENTKIN03_04.MouseDown, txtENTKIN04_04.MouseDown, txtENTKIN05_04.MouseDown _
                                                                                                                        , txtPOINT01_04.MouseDown, txtPOINT02_04.MouseDown, txtPOINT03_04.MouseDown, txtPOINT04_04.MouseDown, txtPOINT05_04.MouseDown _
                                                                                                                        , txtPOINTW01_04.MouseDown, txtPOINTW02_04.MouseDown, txtPOINTW03_04.MouseDown, txtPOINTW04_04.MouseDown, txtPOINTW05_04.MouseDown _
                                                                                                                        , txtPOINTS01_04.MouseDown, txtPOINTS02_04.MouseDown, txtPOINTS03_04.MouseDown, txtPOINTS04_04.MouseDown, txtPOINTS05_04.MouseDown _
                                                                                                                        , txtBALLKIN1F01_04.MouseDown, txtBALLKIN1F02_04.MouseDown, txtBALLKIN1F03_04.MouseDown, txtBALLKIN1F04_04.MouseDown, txtBALLKIN1F05_04.MouseDown _
                                                                                                                        , txtBALLKIN2F01_04.MouseDown, txtBALLKIN2F02_04.MouseDown, txtBALLKIN2F03_04.MouseDown, txtBALLKIN2F04_04.MouseDown, txtBALLKIN2F05_04.MouseDown _
                                                                                                                        , txtTIMENM01_05.MouseDown, txtTIMENM02_05.MouseDown, txtTIMENM03_05.MouseDown, txtTIMENM04_05.MouseDown, txtTIMENM05_05.MouseDown _
                                                                                                                        , txtPASSCD01_05.MouseDown, txtPASSCD02_05.MouseDown, txtPASSCD03_05.MouseDown, txtPASSCD04_05.MouseDown, txtPASSCD05_05.MouseDown _
                                                                                                                        , txtENTKIN01_05.MouseDown, txtENTKIN02_05.MouseDown, txtENTKIN03_05.MouseDown, txtENTKIN04_05.MouseDown, txtENTKIN05_05.MouseDown _
                                                                                                                        , txtPOINT01_05.MouseDown, txtPOINT02_05.MouseDown, txtPOINT03_05.MouseDown, txtPOINT04_05.MouseDown, txtPOINT05_05.MouseDown _
                                                                                                                        , txtPOINTW01_05.MouseDown, txtPOINTW02_05.MouseDown, txtPOINTW03_05.MouseDown, txtPOINTW04_05.MouseDown, txtPOINTW05_05.MouseDown _
                                                                                                                        , txtPOINTS01_05.MouseDown, txtPOINTS02_05.MouseDown, txtPOINTS03_05.MouseDown, txtPOINTS04_05.MouseDown, txtPOINTS05_05.MouseDown _
                                                                                                                        , txtBALLKIN1F01_05.MouseDown, txtBALLKIN1F02_05.MouseDown, txtBALLKIN1F03_05.MouseDown, txtBALLKIN1F04_05.MouseDown, txtBALLKIN1F05_05.MouseDown _
                                                                                                                        , txtBALLKIN2F01_05.MouseDown, txtBALLKIN2F02_05.MouseDown, txtBALLKIN2F03_05.MouseDown, txtBALLKIN2F04_05.MouseDown, txtBALLKIN2F05_05.MouseDown _
                                                                                                                        , txtTIMENM01_06.MouseDown, txtTIMENM02_06.MouseDown, txtTIMENM03_06.MouseDown, txtTIMENM04_06.MouseDown, txtTIMENM05_06.MouseDown _
                                                                                                                        , txtPASSCD01_06.MouseDown, txtPASSCD02_06.MouseDown, txtPASSCD03_06.MouseDown, txtPASSCD04_06.MouseDown, txtPASSCD05_06.MouseDown _
                                                                                                                        , txtENTKIN01_06.MouseDown, txtENTKIN02_06.MouseDown, txtENTKIN03_06.MouseDown, txtENTKIN04_06.MouseDown, txtENTKIN05_06.MouseDown _
                                                                                                                        , txtPOINT01_06.MouseDown, txtPOINT02_06.MouseDown, txtPOINT03_06.MouseDown, txtPOINT04_06.MouseDown, txtPOINT05_06.MouseDown _
                                                                                                                        , txtPOINTW01_06.MouseDown, txtPOINTW02_06.MouseDown, txtPOINTW03_06.MouseDown, txtPOINTW04_06.MouseDown, txtPOINTW05_06.MouseDown _
                                                                                                                        , txtPOINTS01_06.MouseDown, txtPOINTS02_06.MouseDown, txtPOINTS03_06.MouseDown, txtPOINTS04_06.MouseDown, txtPOINTS05_06.MouseDown _
                                                                                                                        , txtBALLKIN1F01_06.MouseDown, txtBALLKIN1F02_06.MouseDown, txtBALLKIN1F03_06.MouseDown, txtBALLKIN1F04_06.MouseDown, txtBALLKIN1F05_06.MouseDown _
                                                                                                                        , txtBALLKIN2F01_06.MouseDown, txtBALLKIN2F02_06.MouseDown, txtBALLKIN2F03_06.MouseDown, txtBALLKIN2F04_06.MouseDown, txtBALLKIN2F05_06.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' テキストボックス_Enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtTIMENM01_01.Enter, txtTIMENM02_01.Enter, txtTIMENM03_01.Enter, txtTIMENM04_01.Enter, txtTIMENM05_01.Enter _
                                                                                   , txtPASSCD01_01.Enter, txtPASSCD02_01.Enter, txtPASSCD03_01.Enter, txtPASSCD04_01.Enter, txtPASSCD05_01.Enter _
                                                                                   , txtENTKIN01_01.Enter, txtENTKIN02_01.Enter, txtENTKIN03_01.Enter, txtENTKIN04_01.Enter, txtENTKIN05_01.Enter _
                                                                                   , txtPOINT01_01.Enter, txtPOINT02_01.Enter, txtPOINT03_01.Enter, txtPOINT04_01.Enter, txtPOINT05_01.Enter _
                                                                                   , txtPOINTW01_01.Enter, txtPOINTW02_01.Enter, txtPOINTW03_01.Enter, txtPOINTW04_01.Enter, txtPOINTW05_01.Enter _
                                                                                   , txtPOINTS01_01.Enter, txtPOINTS02_01.Enter, txtPOINTS03_01.Enter, txtPOINTS04_01.Enter, txtPOINTS05_01.Enter _
                                                                                   , txtBALLKIN1F01_01.Enter, txtBALLKIN1F02_01.Enter, txtBALLKIN1F03_01.Enter, txtBALLKIN1F04_01.Enter, txtBALLKIN1F05_01.Enter _
                                                                                   , txtBALLKIN2F01_01.Enter, txtBALLKIN2F02_01.Enter, txtBALLKIN2F03_01.Enter, txtBALLKIN2F04_01.Enter, txtBALLKIN2F05_01.Enter _
                                                                                   , txtTIMENM01_02.Enter, txtTIMENM02_02.Enter, txtTIMENM03_02.Enter, txtTIMENM04_02.Enter, txtTIMENM05_02.Enter _
                                                                                   , txtPASSCD01_02.Enter, txtPASSCD02_02.Enter, txtPASSCD03_02.Enter, txtPASSCD04_02.Enter, txtPASSCD05_02.Enter _
                                                                                   , txtENTKIN01_02.Enter, txtENTKIN02_02.Enter, txtENTKIN03_02.Enter, txtENTKIN04_02.Enter, txtENTKIN05_02.Enter _
                                                                                   , txtPOINT01_02.Enter, txtPOINT02_02.Enter, txtPOINT03_02.Enter, txtPOINT04_02.Enter, txtPOINT05_02.Enter _
                                                                                   , txtPOINTW01_02.Enter, txtPOINTW02_02.Enter, txtPOINTW03_02.Enter, txtPOINTW04_02.Enter, txtPOINTW05_02.Enter _
                                                                                   , txtPOINTS01_02.Enter, txtPOINTS02_02.Enter, txtPOINTS03_02.Enter, txtPOINTS04_02.Enter, txtPOINTS05_02.Enter _
                                                                                   , txtBALLKIN1F01_02.Enter, txtBALLKIN1F02_02.Enter, txtBALLKIN1F03_02.Enter, txtBALLKIN1F04_02.Enter, txtBALLKIN1F05_02.Enter _
                                                                                   , txtBALLKIN2F01_02.Enter, txtBALLKIN2F02_02.Enter, txtBALLKIN2F03_02.Enter, txtBALLKIN2F04_02.Enter, txtBALLKIN2F05_02.Enter _
                                                                                   , txtTIMENM01_03.Enter, txtTIMENM02_03.Enter, txtTIMENM03_03.Enter, txtTIMENM04_03.Enter, txtTIMENM05_03.Enter _
                                                                                   , txtPASSCD01_03.Enter, txtPASSCD02_03.Enter, txtPASSCD03_03.Enter, txtPASSCD04_03.Enter, txtPASSCD05_03.Enter _
                                                                                   , txtENTKIN01_03.Enter, txtENTKIN02_03.Enter, txtENTKIN03_03.Enter, txtENTKIN04_03.Enter, txtENTKIN05_03.Enter _
                                                                                   , txtPOINT01_03.Enter, txtPOINT02_03.Enter, txtPOINT03_03.Enter, txtPOINT04_03.Enter, txtPOINT05_03.Enter _
                                                                                   , txtPOINTW01_03.Enter, txtPOINTW02_03.Enter, txtPOINTW03_03.Enter, txtPOINTW04_03.Enter, txtPOINTW05_03.Enter _
                                                                                   , txtPOINTS01_03.Enter, txtPOINTS02_03.Enter, txtPOINTS03_03.Enter, txtPOINTS04_03.Enter, txtPOINTS05_03.Enter _
                                                                                   , txtBALLKIN1F01_03.Enter, txtBALLKIN1F02_03.Enter, txtBALLKIN1F03_03.Enter, txtBALLKIN1F04_03.Enter, txtBALLKIN1F05_03.Enter _
                                                                                   , txtBALLKIN2F01_03.Enter, txtBALLKIN2F02_03.Enter, txtBALLKIN2F03_03.Enter, txtBALLKIN2F04_03.Enter, txtBALLKIN2F05_03.Enter _
                                                                                   , txtTIMENM01_04.Enter, txtTIMENM02_04.Enter, txtTIMENM03_04.Enter, txtTIMENM04_04.Enter, txtTIMENM05_04.Enter _
                                                                                   , txtPASSCD01_04.Enter, txtPASSCD02_04.Enter, txtPASSCD03_04.Enter, txtPASSCD04_04.Enter, txtPASSCD05_04.Enter _
                                                                                   , txtENTKIN01_04.Enter, txtENTKIN02_04.Enter, txtENTKIN03_04.Enter, txtENTKIN04_04.Enter, txtENTKIN05_04.Enter _
                                                                                   , txtPOINT01_04.Enter, txtPOINT02_04.Enter, txtPOINT03_04.Enter, txtPOINT04_04.Enter, txtPOINT05_04.Enter _
                                                                                   , txtPOINTW01_04.Enter, txtPOINTW02_04.Enter, txtPOINTW03_04.Enter, txtPOINTW04_04.Enter, txtPOINTW05_04.Enter _
                                                                                   , txtPOINTS01_04.Enter, txtPOINTS02_04.Enter, txtPOINTS03_04.Enter, txtPOINTS04_04.Enter, txtPOINTS05_04.Enter _
                                                                                   , txtBALLKIN1F01_04.Enter, txtBALLKIN1F02_04.Enter, txtBALLKIN1F03_04.Enter, txtBALLKIN1F04_04.Enter, txtBALLKIN1F05_04.Enter _
                                                                                   , txtBALLKIN2F01_04.Enter, txtBALLKIN2F02_04.Enter, txtBALLKIN2F03_04.Enter, txtBALLKIN2F04_04.Enter, txtBALLKIN2F05_04.Enter _
                                                                                   , txtTIMENM01_05.Enter, txtTIMENM02_05.Enter, txtTIMENM03_05.Enter, txtTIMENM04_05.Enter, txtTIMENM05_05.Enter _
                                                                                   , txtPASSCD01_05.Enter, txtPASSCD02_05.Enter, txtPASSCD03_05.Enter, txtPASSCD04_05.Enter, txtPASSCD05_05.Enter _
                                                                                   , txtENTKIN01_05.Enter, txtENTKIN02_05.Enter, txtENTKIN03_05.Enter, txtENTKIN04_05.Enter, txtENTKIN05_05.Enter _
                                                                                   , txtPOINT01_05.Enter, txtPOINT02_05.Enter, txtPOINT03_05.Enter, txtPOINT04_05.Enter, txtPOINT05_05.Enter _
                                                                                   , txtPOINTW01_05.Enter, txtPOINTW02_05.Enter, txtPOINTW03_05.Enter, txtPOINTW04_05.Enter, txtPOINTW05_05.Enter _
                                                                                   , txtPOINTS01_05.Enter, txtPOINTS02_05.Enter, txtPOINTS03_05.Enter, txtPOINTS04_05.Enter, txtPOINTS05_05.Enter _
                                                                                   , txtBALLKIN1F01_05.Enter, txtBALLKIN1F02_05.Enter, txtBALLKIN1F03_05.Enter, txtBALLKIN1F04_05.Enter, txtBALLKIN1F05_05.Enter _
                                                                                   , txtBALLKIN2F01_05.Enter, txtBALLKIN2F02_05.Enter, txtBALLKIN2F03_05.Enter, txtBALLKIN2F04_05.Enter, txtBALLKIN2F05_05.Enter _
                                                                                   , txtTIMENM01_06.Enter, txtTIMENM02_06.Enter, txtTIMENM03_06.Enter, txtTIMENM04_06.Enter, txtTIMENM05_06.Enter _
                                                                                   , txtPASSCD01_06.Enter, txtPASSCD02_06.Enter, txtPASSCD03_06.Enter, txtPASSCD04_06.Enter, txtPASSCD05_06.Enter _
                                                                                   , txtENTKIN01_06.Enter, txtENTKIN02_06.Enter, txtENTKIN03_06.Enter, txtENTKIN04_06.Enter, txtENTKIN05_06.Enter _
                                                                                   , txtPOINT01_06.Enter, txtPOINT02_06.Enter, txtPOINT03_06.Enter, txtPOINT04_06.Enter, txtPOINT05_06.Enter _
                                                                                   , txtPOINTW01_06.Enter, txtPOINTW02_06.Enter, txtPOINTW03_06.Enter, txtPOINTW04_06.Enter, txtPOINTW05_06.Enter _
                                                                                   , txtPOINTS01_06.Enter, txtPOINTS02_06.Enter, txtPOINTS03_06.Enter, txtPOINTS04_06.Enter, txtPOINTS05_06.Enter _
                                                                                   , txtBALLKIN1F01_06.Enter, txtBALLKIN1F02_06.Enter, txtBALLKIN1F03_06.Enter, txtBALLKIN1F04_06.Enter, txtBALLKIN1F05_06.Enter _
                                                                                   , txtBALLKIN2F01_06.Enter, txtBALLKIN2F02_06.Enter, txtBALLKIN2F03_06.Enter, txtBALLKIN2F04_06.Enter, txtBALLKIN2F05_06.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace(":", String.Empty)
            txtBox.Text = txtBox.Text.Replace(",", String.Empty)
            txtBox.SelectAll()

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



            '営業情報取得
            GetNkbEIGMTA()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


    ''' <summary>
    ''' F12登録ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func12()
        Try
            Me.cmbKBMAST.Focus()

            '登録内容チェック
            Dim Msg As String = String.Empty
            If Not CheckRegister(Msg) Then
                Using frm As New frmMSGBOX01(Msg, 3)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("登録してもよろしいですか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            Me.Cursor = Cursors.WaitCursor

            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM EIGMTA WHERE NKBNO = " & (Me.cmbKBMAST.SelectedIndex + 1) & " ORDER BY RKNKB,TIMEKB")
            If Not resultDt.Rows.Count.Equals(0) Then
                '最新の更新日時を取得

                Dim drSelectRow() As DataRow = resultDt.Select
                Dim dtUPDDTM As DateTime = DirectCast(drSelectRow(0).Item("UPDDTM"), DateTime)

                If Not dtUPDDTM.Equals(_dtUPDDTM) Then
                    Using frm As New frmMSGBOX01("他端末からの更新があったため、更新に失敗しました。", 2)
                        frm.ShowDialog()
                    End Using
                    Exit Sub
                End If
            End If

            '営業情報登録
            If Not Register() Then
                Using frm As New frmMSGBOX01("営業情報の登録に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            '画面初期設定
            Init()

            '営業情報取得
            If Not GetNkbEIGMTA() Then
            End If

        Catch ex As Exception
            iDatabase.RollBack()
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
            Me.tspFunc11.Enabled = True
            '登録
            Me.tspFunc12.Enabled = True

            '料金体系コピーコンボボックス
            Me.cmbRKNKB_A.SelectedIndex = -1
            Me.cmbRKNKB_B.SelectedIndex = -1

            Me.lblRknInfo.Text = "本日の料金体系は【" & UIUtility.SYSTEM.RKNNM & "】です。"
            Me.lblRknInfo.ForeColor = Color.FromArgb(UIUtility.COLOR_INFO.RKN_R, UIUtility.COLOR_INFO.RKN_G, UIUtility.COLOR_INFO.RKN_B)

            '【平日】
            '時間帯
            Me.txtTIMENM01_01.Text = String.Empty
            Me.txtTIMENM02_01.Text = String.Empty
            Me.txtTIMENM03_01.Text = String.Empty
            Me.txtTIMENM04_01.Text = String.Empty
            Me.txtTIMENM05_01.Text = String.Empty

            '打ち放題単価メッセージ
            Me.lblMsg2.Visible = False

            If Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.txtTIMENM01_01.ReadOnly = False
                Me.txtTIMENM01_01.BackColor = Color.Moccasin
                Me.txtPASSCD01_01.ReadOnly = False
                Me.txtPASSCD01_01.BackColor = Color.Moccasin

                Me.txtTIMENM02_01.ReadOnly = False
                Me.txtTIMENM02_01.BackColor = Color.White
                Me.txtPASSCD02_01.ReadOnly = False
                Me.txtPASSCD02_01.BackColor = Color.White

                Me.txtTIMENM03_01.ReadOnly = False
                Me.txtTIMENM03_01.BackColor = Color.Moccasin
                Me.txtPASSCD03_01.ReadOnly = False
                Me.txtPASSCD03_01.BackColor = Color.Moccasin

                Me.txtTIMENM04_01.ReadOnly = False
                Me.txtTIMENM04_01.BackColor = Color.White
                Me.txtPASSCD04_01.ReadOnly = False
                Me.txtPASSCD04_01.BackColor = Color.White

                Me.txtTIMENM05_01.ReadOnly = False
                Me.txtTIMENM05_01.BackColor = Color.Moccasin
                Me.txtPASSCD05_01.ReadOnly = False
                Me.txtPASSCD05_01.BackColor = Color.Moccasin

                Me.lblMsg.Visible = False
            Else
                Me.txtTIMENM01_01.ReadOnly = True
                Me.txtTIMENM01_01.BackColor = Color.Silver
                Me.txtPASSCD01_01.ReadOnly = True
                Me.txtPASSCD01_01.BackColor = Color.Silver

                Me.txtTIMENM02_01.ReadOnly = True
                Me.txtTIMENM02_01.BackColor = Color.Silver
                Me.txtPASSCD02_01.ReadOnly = True
                Me.txtPASSCD02_01.BackColor = Color.Silver

                Me.txtTIMENM03_01.ReadOnly = True
                Me.txtTIMENM03_01.BackColor = Color.Silver
                Me.txtPASSCD03_01.ReadOnly = True
                Me.txtPASSCD03_01.BackColor = Color.Silver

                Me.txtTIMENM04_01.ReadOnly = True
                Me.txtTIMENM04_01.BackColor = Color.Silver
                Me.txtPASSCD04_01.ReadOnly = True
                Me.txtPASSCD04_01.BackColor = Color.Silver

                Me.txtTIMENM05_01.ReadOnly = True
                Me.txtTIMENM05_01.BackColor = Color.Silver
                Me.txtPASSCD05_01.ReadOnly = True
                Me.txtPASSCD05_01.BackColor = Color.Silver

                Me.lblMsg.Visible = True
                If Me.cmbKBMAST.SelectedIndex.Equals(14) Then
                    Me.lblMsg2.Visible = True
                End If
            End If

            'パスワード
            Me.txtPASSCD01_01.Text = _cstClrMoji
            Me.txtPASSCD02_01.Text = _cstClrMoji
            Me.txtPASSCD03_01.Text = _cstClrMoji
            Me.txtPASSCD04_01.Text = _cstClrMoji
            Me.txtPASSCD05_01.Text = _cstClrMoji

            Me.txtPASSCD01_01.ReadOnly = True
            Me.txtPASSCD02_01.ReadOnly = True
            Me.txtPASSCD03_01.ReadOnly = True
            Me.txtPASSCD04_01.ReadOnly = True
            Me.txtPASSCD05_01.ReadOnly = True

            '入場料
            Me.txtENTKIN01_01.Text = "0"
            Me.txtENTKIN02_01.Text = "0"
            Me.txtENTKIN03_01.Text = "0"
            Me.txtENTKIN04_01.Text = "0"
            Me.txtENTKIN05_01.Text = "0"

            Me.txtENTKIN01_01.Visible = False
            Me.txtENTKIN02_01.Visible = False
            Me.txtENTKIN03_01.Visible = False
            Me.txtENTKIN04_01.Visible = False
            Me.txtENTKIN05_01.Visible = False

            'ポイント
            Me.txtPOINT01_01.Text = "0"
            Me.txtPOINT02_01.Text = "0"
            Me.txtPOINT03_01.Text = "0"
            Me.txtPOINT04_01.Text = "0"
            Me.txtPOINT05_01.Text = "0"

            Me.txtPOINT01_01.Visible = False
            Me.txtPOINT02_01.Visible = False
            Me.txtPOINT03_01.Visible = False
            Me.txtPOINT04_01.Visible = False
            Me.txtPOINT05_01.Visible = False

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            Me.txtPOINTW01_01.Text = "0"
            Me.txtPOINTW02_01.Text = "0"
            Me.txtPOINTW03_01.Text = "0"
            Me.txtPOINTW04_01.Text = "0"
            Me.txtPOINTW05_01.Text = "0"

            Me.txtPOINTW01_01.Visible = False
            Me.txtPOINTW02_01.Visible = False
            Me.txtPOINTW03_01.Visible = False
            Me.txtPOINTW04_01.Visible = False
            Me.txtPOINTW05_01.Visible = False

            'ｼﾆｱﾎﾟｲﾝﾄ
            Me.txtPOINTS01_01.Text = "0"
            Me.txtPOINTS02_01.Text = "0"
            Me.txtPOINTS03_01.Text = "0"
            Me.txtPOINTS04_01.Text = "0"
            Me.txtPOINTS05_01.Text = "0"

            Me.txtPOINTS01_01.Visible = False
            Me.txtPOINTS02_01.Visible = False
            Me.txtPOINTS03_01.Visible = False
            Me.txtPOINTS04_01.Visible = False
            Me.txtPOINTS05_01.Visible = False

            'ボール単価(1F)
            Me.txtBALLKIN1F01_01.Text = "0"
            Me.txtBALLKIN1F02_01.Text = "0"
            Me.txtBALLKIN1F03_01.Text = "0"
            Me.txtBALLKIN1F04_01.Text = "0"
            Me.txtBALLKIN1F05_01.Text = "0"

            Me.txtBALLKIN1F01_01.Visible = False
            Me.txtBALLKIN1F02_01.Visible = False
            Me.txtBALLKIN1F03_01.Visible = False
            Me.txtBALLKIN1F04_01.Visible = False
            Me.txtBALLKIN1F05_01.Visible = False

            'ボール単価(2F)
            Me.txtBALLKIN2F01_01.Text = "0"
            Me.txtBALLKIN2F02_01.Text = "0"
            Me.txtBALLKIN2F03_01.Text = "0"
            Me.txtBALLKIN2F04_01.Text = "0"
            Me.txtBALLKIN2F05_01.Text = "0"

            Me.txtBALLKIN2F01_01.Visible = False
            Me.txtBALLKIN2F02_01.Visible = False
            Me.txtBALLKIN2F03_01.Visible = False
            Me.txtBALLKIN2F04_01.Visible = False
            Me.txtBALLKIN2F05_01.Visible = False

            '打席指定区分
            Me.chkSITEIKBN01_01.Checked = False
            Me.chkSITEIKBN02_01.Checked = False
            Me.chkSITEIKBN03_01.Checked = False
            Me.chkSITEIKBN04_01.Checked = False
            Me.chkSITEIKBN05_01.Checked = False

            '【休日】
            '時間帯
            Me.txtTIMENM01_02.Text = String.Empty
            Me.txtTIMENM02_02.Text = String.Empty
            Me.txtTIMENM03_02.Text = String.Empty
            Me.txtTIMENM04_02.Text = String.Empty
            Me.txtTIMENM05_02.Text = String.Empty

            If Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.txtTIMENM01_02.ReadOnly = False
                Me.txtTIMENM01_02.BackColor = Color.Moccasin
                Me.txtPASSCD01_02.ReadOnly = False
                Me.txtPASSCD01_02.BackColor = Color.Moccasin

                Me.txtTIMENM02_02.ReadOnly = False
                Me.txtTIMENM02_02.BackColor = Color.White
                Me.txtPASSCD02_02.ReadOnly = False
                Me.txtPASSCD02_02.BackColor = Color.White

                Me.txtTIMENM03_02.ReadOnly = False
                Me.txtTIMENM03_02.BackColor = Color.Moccasin
                Me.txtPASSCD03_02.ReadOnly = False
                Me.txtPASSCD03_02.BackColor = Color.Moccasin

                Me.txtTIMENM04_02.ReadOnly = False
                Me.txtTIMENM04_02.BackColor = Color.White
                Me.txtPASSCD04_02.ReadOnly = False
                Me.txtPASSCD04_02.BackColor = Color.White

                Me.txtTIMENM05_02.ReadOnly = False
                Me.txtTIMENM05_02.BackColor = Color.Moccasin
                Me.txtPASSCD05_02.ReadOnly = False
                Me.txtPASSCD05_02.BackColor = Color.Moccasin
            Else
                Me.txtTIMENM01_02.ReadOnly = True
                Me.txtTIMENM01_02.BackColor = Color.Silver
                Me.txtPASSCD01_02.ReadOnly = True
                Me.txtPASSCD01_02.BackColor = Color.Silver

                Me.txtTIMENM02_02.ReadOnly = True
                Me.txtTIMENM02_02.BackColor = Color.Silver
                Me.txtPASSCD02_02.ReadOnly = True
                Me.txtPASSCD02_02.BackColor = Color.Silver

                Me.txtTIMENM03_02.ReadOnly = True
                Me.txtTIMENM03_02.BackColor = Color.Silver
                Me.txtPASSCD03_02.ReadOnly = True
                Me.txtPASSCD03_02.BackColor = Color.Silver

                Me.txtTIMENM04_02.ReadOnly = True
                Me.txtTIMENM04_02.BackColor = Color.Silver
                Me.txtPASSCD04_02.ReadOnly = True
                Me.txtPASSCD04_02.BackColor = Color.Silver

                Me.txtTIMENM05_02.ReadOnly = True
                Me.txtTIMENM05_02.BackColor = Color.Silver
                Me.txtPASSCD05_02.ReadOnly = True
                Me.txtPASSCD05_02.BackColor = Color.Silver
            End If

            'パスワード
            Me.txtPASSCD01_02.Text = _cstClrMoji
            Me.txtPASSCD02_02.Text = _cstClrMoji
            Me.txtPASSCD03_02.Text = _cstClrMoji
            Me.txtPASSCD04_02.Text = _cstClrMoji
            Me.txtPASSCD05_02.Text = _cstClrMoji

            Me.txtPASSCD01_02.ReadOnly = True
            Me.txtPASSCD02_02.ReadOnly = True
            Me.txtPASSCD03_02.ReadOnly = True
            Me.txtPASSCD04_02.ReadOnly = True
            Me.txtPASSCD05_02.ReadOnly = True

            '入場料
            Me.txtENTKIN01_02.Text = "0"
            Me.txtENTKIN02_02.Text = "0"
            Me.txtENTKIN03_02.Text = "0"
            Me.txtENTKIN04_02.Text = "0"
            Me.txtENTKIN05_02.Text = "0"

            Me.txtENTKIN01_02.Visible = False
            Me.txtENTKIN02_02.Visible = False
            Me.txtENTKIN03_02.Visible = False
            Me.txtENTKIN04_02.Visible = False
            Me.txtENTKIN05_02.Visible = False

            'ポイント
            Me.txtPOINT01_02.Text = "0"
            Me.txtPOINT02_02.Text = "0"
            Me.txtPOINT03_02.Text = "0"
            Me.txtPOINT04_02.Text = "0"
            Me.txtPOINT05_02.Text = "0"

            Me.txtPOINT01_02.Visible = False
            Me.txtPOINT02_02.Visible = False
            Me.txtPOINT03_02.Visible = False
            Me.txtPOINT04_02.Visible = False
            Me.txtPOINT05_02.Visible = False

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            Me.txtPOINTW01_02.Text = "0"
            Me.txtPOINTW02_02.Text = "0"
            Me.txtPOINTW03_02.Text = "0"
            Me.txtPOINTW04_02.Text = "0"
            Me.txtPOINTW05_02.Text = "0"

            Me.txtPOINTW01_02.Visible = False
            Me.txtPOINTW02_02.Visible = False
            Me.txtPOINTW03_02.Visible = False
            Me.txtPOINTW04_02.Visible = False
            Me.txtPOINTW05_02.Visible = False

            'ｼﾆｱﾎﾟｲﾝﾄ
            Me.txtPOINTS01_02.Text = "0"
            Me.txtPOINTS02_02.Text = "0"
            Me.txtPOINTS03_02.Text = "0"
            Me.txtPOINTS04_02.Text = "0"
            Me.txtPOINTS05_02.Text = "0"

            Me.txtPOINTS01_02.Visible = False
            Me.txtPOINTS02_02.Visible = False
            Me.txtPOINTS03_02.Visible = False
            Me.txtPOINTS04_02.Visible = False
            Me.txtPOINTS05_02.Visible = False

            'ボール単価(1F)
            Me.txtBALLKIN1F01_02.Text = "0"
            Me.txtBALLKIN1F02_02.Text = "0"
            Me.txtBALLKIN1F03_02.Text = "0"
            Me.txtBALLKIN1F04_02.Text = "0"
            Me.txtBALLKIN1F05_02.Text = "0"

            Me.txtBALLKIN1F01_02.Visible = False
            Me.txtBALLKIN1F02_02.Visible = False
            Me.txtBALLKIN1F03_02.Visible = False
            Me.txtBALLKIN1F04_02.Visible = False
            Me.txtBALLKIN1F05_02.Visible = False

            'ボール単価(2F)
            Me.txtBALLKIN2F01_02.Text = "0"
            Me.txtBALLKIN2F02_02.Text = "0"
            Me.txtBALLKIN2F03_02.Text = "0"
            Me.txtBALLKIN2F04_02.Text = "0"
            Me.txtBALLKIN2F05_02.Text = "0"

            Me.txtBALLKIN2F01_02.Visible = False
            Me.txtBALLKIN2F02_02.Visible = False
            Me.txtBALLKIN2F03_02.Visible = False
            Me.txtBALLKIN2F04_02.Visible = False
            Me.txtBALLKIN2F05_02.Visible = False


            '打席指定区分
            Me.chkSITEIKBN01_02.Checked = False
            Me.chkSITEIKBN02_02.Checked = False
            Me.chkSITEIKBN03_02.Checked = False
            Me.chkSITEIKBN04_02.Checked = False
            Me.chkSITEIKBN05_02.Checked = False

            '【特日1】
            '時間帯
            Me.txtTIMENM01_03.Text = String.Empty
            Me.txtTIMENM02_03.Text = String.Empty
            Me.txtTIMENM03_03.Text = String.Empty
            Me.txtTIMENM04_03.Text = String.Empty
            Me.txtTIMENM05_03.Text = String.Empty

            If Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.txtTIMENM01_03.ReadOnly = False
                Me.txtTIMENM01_03.BackColor = Color.Moccasin
                Me.txtPASSCD01_03.ReadOnly = False
                Me.txtPASSCD01_03.BackColor = Color.Moccasin

                Me.txtTIMENM02_03.ReadOnly = False
                Me.txtTIMENM02_03.BackColor = Color.White
                Me.txtPASSCD02_03.ReadOnly = False
                Me.txtPASSCD02_03.BackColor = Color.White

                Me.txtTIMENM03_03.ReadOnly = False
                Me.txtTIMENM03_03.BackColor = Color.Moccasin
                Me.txtPASSCD03_03.ReadOnly = False
                Me.txtPASSCD03_03.BackColor = Color.Moccasin

                Me.txtTIMENM04_03.ReadOnly = False
                Me.txtTIMENM04_03.BackColor = Color.White
                Me.txtPASSCD04_03.ReadOnly = False
                Me.txtPASSCD04_03.BackColor = Color.White

                Me.txtTIMENM05_03.ReadOnly = False
                Me.txtTIMENM05_03.BackColor = Color.Moccasin
                Me.txtPASSCD05_03.ReadOnly = False
                Me.txtPASSCD05_03.BackColor = Color.Moccasin
            Else
                Me.txtTIMENM01_03.ReadOnly = True
                Me.txtTIMENM01_03.BackColor = Color.Silver
                Me.txtPASSCD01_03.ReadOnly = True
                Me.txtPASSCD01_03.BackColor = Color.Silver

                Me.txtTIMENM02_03.ReadOnly = True
                Me.txtTIMENM02_03.BackColor = Color.Silver
                Me.txtPASSCD02_03.ReadOnly = True
                Me.txtPASSCD02_03.BackColor = Color.Silver

                Me.txtTIMENM03_03.ReadOnly = True
                Me.txtTIMENM03_03.BackColor = Color.Silver
                Me.txtPASSCD03_03.ReadOnly = True
                Me.txtPASSCD03_03.BackColor = Color.Silver

                Me.txtTIMENM04_03.ReadOnly = True
                Me.txtTIMENM04_03.BackColor = Color.Silver
                Me.txtPASSCD04_03.ReadOnly = True
                Me.txtPASSCD04_03.BackColor = Color.Silver

                Me.txtTIMENM05_03.ReadOnly = True
                Me.txtTIMENM05_03.BackColor = Color.Silver
                Me.txtPASSCD05_03.ReadOnly = True
                Me.txtPASSCD05_03.BackColor = Color.Silver
            End If

            'パスワード
            Me.txtPASSCD01_03.Text = _cstClrMoji
            Me.txtPASSCD02_03.Text = _cstClrMoji
            Me.txtPASSCD03_03.Text = _cstClrMoji
            Me.txtPASSCD04_03.Text = _cstClrMoji
            Me.txtPASSCD05_03.Text = _cstClrMoji

            Me.txtPASSCD01_03.ReadOnly = True
            Me.txtPASSCD02_03.ReadOnly = True
            Me.txtPASSCD03_03.ReadOnly = True
            Me.txtPASSCD04_03.ReadOnly = True
            Me.txtPASSCD05_03.ReadOnly = True

            '入場料
            Me.txtENTKIN01_03.Text = "0"
            Me.txtENTKIN02_03.Text = "0"
            Me.txtENTKIN03_03.Text = "0"
            Me.txtENTKIN04_03.Text = "0"
            Me.txtENTKIN05_03.Text = "0"

            Me.txtENTKIN01_03.Visible = False
            Me.txtENTKIN02_03.Visible = False
            Me.txtENTKIN03_03.Visible = False
            Me.txtENTKIN04_03.Visible = False
            Me.txtENTKIN05_03.Visible = False

            'ポイント
            Me.txtPOINT01_03.Text = "0"
            Me.txtPOINT02_03.Text = "0"
            Me.txtPOINT03_03.Text = "0"
            Me.txtPOINT04_03.Text = "0"
            Me.txtPOINT05_03.Text = "0"

            Me.txtPOINT01_03.Visible = False
            Me.txtPOINT02_03.Visible = False
            Me.txtPOINT03_03.Visible = False
            Me.txtPOINT04_03.Visible = False
            Me.txtPOINT05_03.Visible = False

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            Me.txtPOINTW01_03.Text = "0"
            Me.txtPOINTW02_03.Text = "0"
            Me.txtPOINTW03_03.Text = "0"
            Me.txtPOINTW04_03.Text = "0"
            Me.txtPOINTW05_03.Text = "0"

            Me.txtPOINTW01_03.Visible = False
            Me.txtPOINTW02_03.Visible = False
            Me.txtPOINTW03_03.Visible = False
            Me.txtPOINTW04_03.Visible = False
            Me.txtPOINTW05_03.Visible = False

            'ｼﾆｱﾎﾟｲﾝﾄ
            Me.txtPOINTS01_03.Text = "0"
            Me.txtPOINTS02_03.Text = "0"
            Me.txtPOINTS03_03.Text = "0"
            Me.txtPOINTS04_03.Text = "0"
            Me.txtPOINTS05_03.Text = "0"

            Me.txtPOINTS01_03.Visible = False
            Me.txtPOINTS02_03.Visible = False
            Me.txtPOINTS03_03.Visible = False
            Me.txtPOINTS04_03.Visible = False
            Me.txtPOINTS05_03.Visible = False

            'ボール単価(1F)
            Me.txtBALLKIN1F01_03.Text = "0"
            Me.txtBALLKIN1F02_03.Text = "0"
            Me.txtBALLKIN1F03_03.Text = "0"
            Me.txtBALLKIN1F04_03.Text = "0"
            Me.txtBALLKIN1F05_03.Text = "0"

            Me.txtBALLKIN1F01_03.Visible = False
            Me.txtBALLKIN1F02_03.Visible = False
            Me.txtBALLKIN1F03_03.Visible = False
            Me.txtBALLKIN1F04_03.Visible = False
            Me.txtBALLKIN1F05_03.Visible = False

            'ボール単価(2F)
            Me.txtBALLKIN2F01_03.Text = "0"
            Me.txtBALLKIN2F02_03.Text = "0"
            Me.txtBALLKIN2F03_03.Text = "0"
            Me.txtBALLKIN2F04_03.Text = "0"
            Me.txtBALLKIN2F05_03.Text = "0"

            Me.txtBALLKIN2F01_03.Visible = False
            Me.txtBALLKIN2F02_03.Visible = False
            Me.txtBALLKIN2F03_03.Visible = False
            Me.txtBALLKIN2F04_03.Visible = False
            Me.txtBALLKIN2F05_03.Visible = False

            '打席指定区分
            Me.chkSITEIKBN01_03.Checked = False
            Me.chkSITEIKBN02_03.Checked = False
            Me.chkSITEIKBN03_03.Checked = False
            Me.chkSITEIKBN04_03.Checked = False
            Me.chkSITEIKBN05_03.Checked = False

            '【特日2】
            '時間帯
            Me.txtTIMENM01_04.Text = String.Empty
            Me.txtTIMENM02_04.Text = String.Empty
            Me.txtTIMENM03_04.Text = String.Empty
            Me.txtTIMENM04_04.Text = String.Empty
            Me.txtTIMENM05_04.Text = String.Empty

            If Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.txtTIMENM01_04.ReadOnly = False
                Me.txtTIMENM01_04.BackColor = Color.Moccasin
                Me.txtPASSCD01_04.ReadOnly = False
                Me.txtPASSCD01_04.BackColor = Color.Moccasin

                Me.txtTIMENM02_04.ReadOnly = False
                Me.txtTIMENM02_04.BackColor = Color.White
                Me.txtPASSCD02_04.ReadOnly = False
                Me.txtPASSCD02_04.BackColor = Color.White

                Me.txtTIMENM03_04.ReadOnly = False
                Me.txtTIMENM03_04.BackColor = Color.Moccasin
                Me.txtPASSCD03_04.ReadOnly = False
                Me.txtPASSCD03_04.BackColor = Color.Moccasin

                Me.txtTIMENM04_04.ReadOnly = False
                Me.txtTIMENM04_04.BackColor = Color.White
                Me.txtPASSCD04_04.ReadOnly = False
                Me.txtPASSCD04_04.BackColor = Color.White

                Me.txtTIMENM05_04.ReadOnly = False
                Me.txtTIMENM05_04.BackColor = Color.Moccasin
                Me.txtPASSCD05_04.ReadOnly = False
                Me.txtPASSCD05_04.BackColor = Color.Moccasin
            Else
                Me.txtTIMENM01_04.ReadOnly = True
                Me.txtTIMENM01_04.BackColor = Color.Silver
                Me.txtPASSCD01_04.ReadOnly = True
                Me.txtPASSCD01_04.BackColor = Color.Silver

                Me.txtTIMENM02_04.ReadOnly = True
                Me.txtTIMENM02_04.BackColor = Color.Silver
                Me.txtPASSCD02_04.ReadOnly = True
                Me.txtPASSCD02_04.BackColor = Color.Silver

                Me.txtTIMENM03_04.ReadOnly = True
                Me.txtTIMENM03_04.BackColor = Color.Silver
                Me.txtPASSCD03_04.ReadOnly = True
                Me.txtPASSCD03_04.BackColor = Color.Silver

                Me.txtTIMENM04_04.ReadOnly = True
                Me.txtTIMENM04_04.BackColor = Color.Silver
                Me.txtPASSCD04_04.ReadOnly = True
                Me.txtPASSCD04_04.BackColor = Color.Silver

                Me.txtTIMENM05_04.ReadOnly = True
                Me.txtTIMENM05_04.BackColor = Color.Silver
                Me.txtPASSCD05_04.ReadOnly = True
                Me.txtPASSCD05_04.BackColor = Color.Silver
            End If

            'パスワード
            Me.txtPASSCD01_04.Text = _cstClrMoji
            Me.txtPASSCD02_04.Text = _cstClrMoji
            Me.txtPASSCD03_04.Text = _cstClrMoji
            Me.txtPASSCD04_04.Text = _cstClrMoji
            Me.txtPASSCD05_04.Text = _cstClrMoji

            Me.txtPASSCD01_04.ReadOnly = True
            Me.txtPASSCD02_04.ReadOnly = True
            Me.txtPASSCD03_04.ReadOnly = True
            Me.txtPASSCD04_04.ReadOnly = True
            Me.txtPASSCD05_04.ReadOnly = True

            '入場料
            Me.txtENTKIN01_04.Text = "0"
            Me.txtENTKIN02_04.Text = "0"
            Me.txtENTKIN03_04.Text = "0"
            Me.txtENTKIN04_04.Text = "0"
            Me.txtENTKIN05_04.Text = "0"

            Me.txtENTKIN01_04.Visible = False
            Me.txtENTKIN02_04.Visible = False
            Me.txtENTKIN03_04.Visible = False
            Me.txtENTKIN04_04.Visible = False
            Me.txtENTKIN05_04.Visible = False

            'ポイント
            Me.txtPOINT01_04.Text = "0"
            Me.txtPOINT02_04.Text = "0"
            Me.txtPOINT03_04.Text = "0"
            Me.txtPOINT04_04.Text = "0"
            Me.txtPOINT05_04.Text = "0"

            Me.txtPOINT01_04.Visible = False
            Me.txtPOINT02_04.Visible = False
            Me.txtPOINT03_04.Visible = False
            Me.txtPOINT04_04.Visible = False
            Me.txtPOINT05_04.Visible = False

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            Me.txtPOINTW01_04.Text = "0"
            Me.txtPOINTW02_04.Text = "0"
            Me.txtPOINTW03_04.Text = "0"
            Me.txtPOINTW04_04.Text = "0"
            Me.txtPOINTW05_04.Text = "0"

            Me.txtPOINTW01_04.Visible = False
            Me.txtPOINTW02_04.Visible = False
            Me.txtPOINTW03_04.Visible = False
            Me.txtPOINTW04_04.Visible = False
            Me.txtPOINTW05_04.Visible = False

            'ｼﾆｱﾎﾟｲﾝﾄ
            Me.txtPOINTS01_04.Text = "0"
            Me.txtPOINTS02_04.Text = "0"
            Me.txtPOINTS03_04.Text = "0"
            Me.txtPOINTS04_04.Text = "0"
            Me.txtPOINTS05_04.Text = "0"

            Me.txtPOINTS01_04.Visible = False
            Me.txtPOINTS02_04.Visible = False
            Me.txtPOINTS03_04.Visible = False
            Me.txtPOINTS04_04.Visible = False
            Me.txtPOINTS05_04.Visible = False

            'ボール単価(1F)
            Me.txtBALLKIN1F01_04.Text = "0"
            Me.txtBALLKIN1F02_04.Text = "0"
            Me.txtBALLKIN1F03_04.Text = "0"
            Me.txtBALLKIN1F04_04.Text = "0"
            Me.txtBALLKIN1F05_04.Text = "0"

            Me.txtBALLKIN1F01_04.Visible = False
            Me.txtBALLKIN1F02_04.Visible = False
            Me.txtBALLKIN1F03_04.Visible = False
            Me.txtBALLKIN1F04_04.Visible = False
            Me.txtBALLKIN1F05_04.Visible = False

            'ボール単価(2F)
            Me.txtBALLKIN2F01_04.Text = "0"
            Me.txtBALLKIN2F02_04.Text = "0"
            Me.txtBALLKIN2F03_04.Text = "0"
            Me.txtBALLKIN2F04_04.Text = "0"
            Me.txtBALLKIN2F05_04.Text = "0"

            Me.txtBALLKIN2F01_04.Visible = False
            Me.txtBALLKIN2F02_04.Visible = False
            Me.txtBALLKIN2F03_04.Visible = False
            Me.txtBALLKIN2F04_04.Visible = False
            Me.txtBALLKIN2F05_04.Visible = False

            '打席指定区分
            Me.chkSITEIKBN01_04.Checked = False
            Me.chkSITEIKBN02_04.Checked = False
            Me.chkSITEIKBN03_04.Checked = False
            Me.chkSITEIKBN04_04.Checked = False
            Me.chkSITEIKBN05_04.Checked = False

            '【特日3】
            '時間帯
            Me.txtTIMENM01_05.Text = String.Empty
            Me.txtTIMENM02_05.Text = String.Empty
            Me.txtTIMENM03_05.Text = String.Empty
            Me.txtTIMENM04_05.Text = String.Empty
            Me.txtTIMENM05_05.Text = String.Empty

            If Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.txtTIMENM01_05.ReadOnly = False
                Me.txtTIMENM01_05.BackColor = Color.Moccasin
                Me.txtPASSCD01_05.ReadOnly = False
                Me.txtPASSCD01_05.BackColor = Color.Moccasin

                Me.txtTIMENM02_05.ReadOnly = False
                Me.txtTIMENM02_05.BackColor = Color.White
                Me.txtPASSCD02_05.ReadOnly = False
                Me.txtPASSCD02_05.BackColor = Color.White

                Me.txtTIMENM03_05.ReadOnly = False
                Me.txtTIMENM03_05.BackColor = Color.Moccasin
                Me.txtPASSCD03_05.ReadOnly = False
                Me.txtPASSCD03_05.BackColor = Color.Moccasin

                Me.txtTIMENM04_05.ReadOnly = False
                Me.txtTIMENM04_05.BackColor = Color.White
                Me.txtPASSCD04_05.ReadOnly = False
                Me.txtPASSCD04_05.BackColor = Color.White

                Me.txtTIMENM05_05.ReadOnly = False
                Me.txtTIMENM05_05.BackColor = Color.Moccasin
                Me.txtPASSCD05_05.ReadOnly = False
                Me.txtPASSCD05_05.BackColor = Color.Moccasin
            Else
                Me.txtTIMENM01_05.ReadOnly = True
                Me.txtTIMENM01_05.BackColor = Color.Silver
                Me.txtPASSCD01_05.ReadOnly = True
                Me.txtPASSCD01_05.BackColor = Color.Silver

                Me.txtTIMENM02_05.ReadOnly = True
                Me.txtTIMENM02_05.BackColor = Color.Silver
                Me.txtPASSCD02_05.ReadOnly = True
                Me.txtPASSCD02_05.BackColor = Color.Silver

                Me.txtTIMENM03_05.ReadOnly = True
                Me.txtTIMENM03_05.BackColor = Color.Silver
                Me.txtPASSCD03_05.ReadOnly = True
                Me.txtPASSCD03_05.BackColor = Color.Silver

                Me.txtTIMENM04_05.ReadOnly = True
                Me.txtTIMENM04_05.BackColor = Color.Silver
                Me.txtPASSCD04_05.ReadOnly = True
                Me.txtPASSCD04_05.BackColor = Color.Silver

                Me.txtTIMENM05_05.ReadOnly = True
                Me.txtTIMENM05_05.BackColor = Color.Silver
                Me.txtPASSCD05_05.ReadOnly = True
                Me.txtPASSCD05_05.BackColor = Color.Silver
            End If

            'パスワード
            Me.txtPASSCD01_05.Text = _cstClrMoji
            Me.txtPASSCD02_05.Text = _cstClrMoji
            Me.txtPASSCD03_05.Text = _cstClrMoji
            Me.txtPASSCD04_05.Text = _cstClrMoji
            Me.txtPASSCD05_05.Text = _cstClrMoji

            Me.txtPASSCD01_05.ReadOnly = True
            Me.txtPASSCD02_05.ReadOnly = True
            Me.txtPASSCD03_05.ReadOnly = True
            Me.txtPASSCD04_05.ReadOnly = True
            Me.txtPASSCD05_05.ReadOnly = True

            '入場料
            Me.txtENTKIN01_05.Text = "0"
            Me.txtENTKIN02_05.Text = "0"
            Me.txtENTKIN03_05.Text = "0"
            Me.txtENTKIN04_05.Text = "0"
            Me.txtENTKIN05_05.Text = "0"

            Me.txtENTKIN01_05.Visible = False
            Me.txtENTKIN02_05.Visible = False
            Me.txtENTKIN03_05.Visible = False
            Me.txtENTKIN04_05.Visible = False
            Me.txtENTKIN05_05.Visible = False

            'ポイント
            Me.txtPOINT01_05.Text = "0"
            Me.txtPOINT02_05.Text = "0"
            Me.txtPOINT03_05.Text = "0"
            Me.txtPOINT04_05.Text = "0"
            Me.txtPOINT05_05.Text = "0"

            Me.txtPOINT01_05.Visible = False
            Me.txtPOINT02_05.Visible = False
            Me.txtPOINT03_05.Visible = False
            Me.txtPOINT04_05.Visible = False
            Me.txtPOINT05_05.Visible = False

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
            Me.txtPOINTW01_05.Text = "0"
            Me.txtPOINTW02_05.Text = "0"
            Me.txtPOINTW03_05.Text = "0"
            Me.txtPOINTW04_05.Text = "0"
            Me.txtPOINTW05_05.Text = "0"

            Me.txtPOINTW01_05.Visible = False
            Me.txtPOINTW02_05.Visible = False
            Me.txtPOINTW03_05.Visible = False
            Me.txtPOINTW04_05.Visible = False
            Me.txtPOINTW05_05.Visible = False

            'ｼﾆｱﾎﾟｲﾝﾄ
            Me.txtPOINTS01_05.Text = "0"
            Me.txtPOINTS02_05.Text = "0"
            Me.txtPOINTS03_05.Text = "0"
            Me.txtPOINTS04_05.Text = "0"
            Me.txtPOINTS05_05.Text = "0"

            Me.txtPOINTS01_05.Visible = False
            Me.txtPOINTS02_05.Visible = False
            Me.txtPOINTS03_05.Visible = False
            Me.txtPOINTS04_05.Visible = False
            Me.txtPOINTS05_05.Visible = False

            'ボール単価(1F)
            Me.txtBALLKIN1F01_05.Text = "0"
            Me.txtBALLKIN1F02_05.Text = "0"
            Me.txtBALLKIN1F03_05.Text = "0"
            Me.txtBALLKIN1F04_05.Text = "0"
            Me.txtBALLKIN1F05_05.Text = "0"

            Me.txtBALLKIN1F01_05.Visible = False
            Me.txtBALLKIN1F02_05.Visible = False
            Me.txtBALLKIN1F03_05.Visible = False
            Me.txtBALLKIN1F04_05.Visible = False
            Me.txtBALLKIN1F05_05.Visible = False

            'ボール単価(2F)
            Me.txtBALLKIN2F01_05.Text = "0"
            Me.txtBALLKIN2F02_05.Text = "0"
            Me.txtBALLKIN2F03_05.Text = "0"
            Me.txtBALLKIN2F04_05.Text = "0"
            Me.txtBALLKIN2F05_05.Text = "0"

            Me.txtBALLKIN2F01_05.Visible = False
            Me.txtBALLKIN2F02_05.Visible = False
            Me.txtBALLKIN2F03_05.Visible = False
            Me.txtBALLKIN2F04_05.Visible = False
            Me.txtBALLKIN2F05_05.Visible = False

            '打席指定区分
            Me.chkSITEIKBN01_05.Checked = False
            Me.chkSITEIKBN02_05.Checked = False
            Me.chkSITEIKBN03_05.Checked = False
            Me.chkSITEIKBN04_05.Checked = False
            Me.chkSITEIKBN05_05.Checked = False

            '【特日4】
            '時間帯
            Me.txtTIMENM01_06.Text = String.Empty
            Me.txtTIMENM02_06.Text = String.Empty
            Me.txtTIMENM03_06.Text = String.Empty
            Me.txtTIMENM04_06.Text = String.Empty
            Me.txtTIMENM05_06.Text = String.Empty

            If Me.cmbKBMAST.SelectedIndex.Equals(0) Then
                Me.txtTIMENM01_06.ReadOnly = False
                Me.txtTIMENM01_06.BackColor = Color.Moccasin
                Me.txtPASSCD01_06.ReadOnly = False
                Me.txtPASSCD01_06.BackColor = Color.Moccasin

                Me.txtTIMENM02_06.ReadOnly = False
                Me.txtTIMENM02_06.BackColor = Color.White
                Me.txtPASSCD02_06.ReadOnly = False
                Me.txtPASSCD02_06.BackColor = Color.White

                Me.txtTIMENM03_06.ReadOnly = False
                Me.txtTIMENM03_06.BackColor = Color.Moccasin
                Me.txtPASSCD03_06.ReadOnly = False
                Me.txtPASSCD03_06.BackColor = Color.Moccasin

                Me.txtTIMENM04_06.ReadOnly = False
                Me.txtTIMENM04_06.BackColor = Color.White
                Me.txtPASSCD04_06.ReadOnly = False
                Me.txtPASSCD04_06.BackColor = Color.White

                Me.txtTIMENM05_06.ReadOnly = False
                Me.txtTIMENM05_06.BackColor = Color.Moccasin
                Me.txtPASSCD05_06.ReadOnly = False
                Me.txtPASSCD05_06.BackColor = Color.Moccasin
            Else
                Me.txtTIMENM01_06.ReadOnly = True
                Me.txtTIMENM01_06.BackColor = Color.Silver
                Me.txtPASSCD01_06.ReadOnly = True
                Me.txtPASSCD01_06.BackColor = Color.Silver

                Me.txtTIMENM02_06.ReadOnly = True
                Me.txtTIMENM02_06.BackColor = Color.Silver
                Me.txtPASSCD02_06.ReadOnly = True
                Me.txtPASSCD02_06.BackColor = Color.Silver

                Me.txtTIMENM03_06.ReadOnly = True
                Me.txtTIMENM03_06.BackColor = Color.Silver
                Me.txtPASSCD03_06.ReadOnly = True
                Me.txtPASSCD03_06.BackColor = Color.Silver

                Me.txtTIMENM04_06.ReadOnly = True
                Me.txtTIMENM04_06.BackColor = Color.Silver
                Me.txtPASSCD04_06.ReadOnly = True
                Me.txtPASSCD04_06.BackColor = Color.Silver

                Me.txtTIMENM05_06.ReadOnly = True
                Me.txtTIMENM05_06.BackColor = Color.Silver
                Me.txtPASSCD05_06.ReadOnly = True
                Me.txtPASSCD05_06.BackColor = Color.Silver
            End If

            'パスワード
            Me.txtPASSCD01_06.Text = _cstClrMoji
            Me.txtPASSCD02_06.Text = _cstClrMoji
            Me.txtPASSCD03_06.Text = _cstClrMoji
            Me.txtPASSCD04_06.Text = _cstClrMoji
            Me.txtPASSCD05_06.Text = _cstClrMoji

            Me.txtPASSCD01_06.ReadOnly = True
            Me.txtPASSCD02_06.ReadOnly = True
            Me.txtPASSCD03_06.ReadOnly = True
            Me.txtPASSCD04_06.ReadOnly = True
            Me.txtPASSCD05_06.ReadOnly = True

            '入場料
            Me.txtENTKIN01_06.Text = "0"
            Me.txtENTKIN02_06.Text = "0"
            Me.txtENTKIN03_06.Text = "0"
            Me.txtENTKIN04_06.Text = "0"
            Me.txtENTKIN05_06.Text = "0"

            Me.txtENTKIN01_06.Visible = False
            Me.txtENTKIN02_06.Visible = False
            Me.txtENTKIN03_06.Visible = False
            Me.txtENTKIN04_06.Visible = False
            Me.txtENTKIN05_06.Visible = False

            'ポイント
            Me.txtPOINT01_06.Text = "0"
            Me.txtPOINT02_06.Text = "0"
            Me.txtPOINT03_06.Text = "0"
            Me.txtPOINT04_06.Text = "0"
            Me.txtPOINT05_06.Text = "0"

            Me.txtPOINT01_06.Visible = False
            Me.txtPOINT02_06.Visible = False
            Me.txtPOINT03_06.Visible = False
            Me.txtPOINT04_06.Visible = False
            Me.txtPOINT05_06.Visible = False

            'ﾚﾃﾞｨｰｽﾎﾟｲﾝ
            Me.txtPOINTW01_06.Text = "0"
            Me.txtPOINTW02_06.Text = "0"
            Me.txtPOINTW03_06.Text = "0"
            Me.txtPOINTW04_06.Text = "0"
            Me.txtPOINTW05_06.Text = "0"

            Me.txtPOINTW01_06.Visible = False
            Me.txtPOINTW02_06.Visible = False
            Me.txtPOINTW03_06.Visible = False
            Me.txtPOINTW04_06.Visible = False
            Me.txtPOINTW05_06.Visible = False

            'ｼﾆｱﾎﾟｲﾝﾄ
            Me.txtPOINTS01_06.Text = "0"
            Me.txtPOINTS02_06.Text = "0"
            Me.txtPOINTS03_06.Text = "0"
            Me.txtPOINTS04_06.Text = "0"
            Me.txtPOINTS05_06.Text = "0"

            Me.txtPOINTS01_06.Visible = False
            Me.txtPOINTS02_06.Visible = False
            Me.txtPOINTS03_06.Visible = False
            Me.txtPOINTS04_06.Visible = False
            Me.txtPOINTS05_06.Visible = False

            'ボール単価(1F)
            Me.txtBALLKIN1F01_06.Text = "0"
            Me.txtBALLKIN1F02_06.Text = "0"
            Me.txtBALLKIN1F03_06.Text = "0"
            Me.txtBALLKIN1F04_06.Text = "0"
            Me.txtBALLKIN1F05_06.Text = "0"

            Me.txtBALLKIN1F01_06.Visible = False
            Me.txtBALLKIN1F02_06.Visible = False
            Me.txtBALLKIN1F03_06.Visible = False
            Me.txtBALLKIN1F04_06.Visible = False
            Me.txtBALLKIN1F05_06.Visible = False

            'ボール単価(2F)
            Me.txtBALLKIN2F01_06.Text = "0"
            Me.txtBALLKIN2F02_06.Text = "0"
            Me.txtBALLKIN2F03_06.Text = "0"
            Me.txtBALLKIN2F04_06.Text = "0"
            Me.txtBALLKIN2F05_06.Text = "0"

            Me.txtBALLKIN2F01_06.Visible = False
            Me.txtBALLKIN2F02_06.Visible = False
            Me.txtBALLKIN2F03_06.Visible = False
            Me.txtBALLKIN2F04_06.Visible = False
            Me.txtBALLKIN2F05_06.Visible = False

            '打席指定区分
            Me.chkSITEIKBN01_06.Checked = False
            Me.chkSITEIKBN02_06.Checked = False
            Me.chkSITEIKBN03_06.Checked = False
            Me.chkSITEIKBN04_06.Checked = False
            Me.chkSITEIKBN05_06.Checked = False

            '現在の時間帯をのフォントカラー変更
            If UIUtility.SYSTEM.RKNKB.Equals(1) Then
                '【平日】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_01.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_01.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_01.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_01.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_01.ForeColor = Color.Yellow
                End Select
            ElseIf UIUtility.SYSTEM.RKNKB.Equals(2) Then
                '【休日】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_02.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_02.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_02.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_02.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_02.ForeColor = Color.Yellow
                End Select
            ElseIf UIUtility.SYSTEM.RKNKB.Equals(3) Then
                '【特日1】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_03.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_03.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_03.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_03.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_03.ForeColor = Color.Yellow
                End Select
            ElseIf UIUtility.SYSTEM.RKNKB.Equals(4) Then
                '【特日2】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_04.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_04.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_04.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_04.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_04.ForeColor = Color.Yellow
                End Select
            ElseIf UIUtility.SYSTEM.RKNKB.Equals(5) Then
                '【特日3】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_05.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_05.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_05.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_05.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_05.ForeColor = Color.Yellow
                End Select
            ElseIf UIUtility.SYSTEM.RKNKB.Equals(6) Then
                '【特日4】
                Select Case UIUtility.SYSTEM.NOWTIMEKB
                    Case 1
                        Me.lblTIMEKB01_06.ForeColor = Color.Yellow
                    Case 2
                        Me.lblTIMEKB02_06.ForeColor = Color.Yellow
                    Case 3
                        Me.lblTIMEKB03_06.ForeColor = Color.Yellow
                    Case 4
                        Me.lblTIMEKB04_06.ForeColor = Color.Yellow
                    Case 5
                        Me.lblTIMEKB05_06.ForeColor = Color.Yellow
                End Select
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 登録データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckRegister(ByRef Msg As String) As Boolean
        Try
            '時間帯
            Dim txtTIMENM01 As TextBox = Nothing
            Dim txtTIMENM02 As TextBox = Nothing
            Dim txtTIMENM03 As TextBox = Nothing
            Dim txtTIMENM04 As TextBox = Nothing
            Dim txtTIMENM05 As TextBox = Nothing

            Dim strTIMENM01 As String = String.Empty
            Dim strTIMENM02 As String = String.Empty
            Dim strTIMENM03 As String = String.Empty
            Dim strTIMENM04 As String = String.Empty
            Dim strTIMENM05 As String = String.Empty

            For i As Integer = 1 To 6   '料金体系(1～6)
                Select Case i
                    Case 1  '平日
                        txtTIMENM01 = Me.txtTIMENM01_01
                        txtTIMENM02 = Me.txtTIMENM02_01
                        txtTIMENM03 = Me.txtTIMENM03_01
                        txtTIMENM04 = Me.txtTIMENM04_01
                        txtTIMENM05 = Me.txtTIMENM05_01
                    Case 2  '休日
                        txtTIMENM01 = Me.txtTIMENM01_02
                        txtTIMENM02 = Me.txtTIMENM02_02
                        txtTIMENM03 = Me.txtTIMENM03_02
                        txtTIMENM04 = Me.txtTIMENM04_02
                        txtTIMENM05 = Me.txtTIMENM05_02
                    Case 3  '特日1
                        txtTIMENM01 = Me.txtTIMENM01_03
                        txtTIMENM02 = Me.txtTIMENM02_03
                        txtTIMENM03 = Me.txtTIMENM03_03
                        txtTIMENM04 = Me.txtTIMENM04_03
                        txtTIMENM05 = Me.txtTIMENM05_03
                    Case 4  '特日2
                        txtTIMENM01 = Me.txtTIMENM01_04
                        txtTIMENM02 = Me.txtTIMENM02_04
                        txtTIMENM03 = Me.txtTIMENM03_04
                        txtTIMENM04 = Me.txtTIMENM04_04
                        txtTIMENM05 = Me.txtTIMENM05_04
                    Case 5  '特日3
                        txtTIMENM01 = Me.txtTIMENM01_05
                        txtTIMENM02 = Me.txtTIMENM02_05
                        txtTIMENM03 = Me.txtTIMENM03_05
                        txtTIMENM04 = Me.txtTIMENM04_05
                        txtTIMENM05 = Me.txtTIMENM05_05
                    Case 6  '特日4
                        txtTIMENM01 = Me.txtTIMENM01_06
                        txtTIMENM02 = Me.txtTIMENM02_06
                        txtTIMENM03 = Me.txtTIMENM03_06
                        txtTIMENM04 = Me.txtTIMENM04_06
                        txtTIMENM05 = Me.txtTIMENM05_06
                End Select

                strTIMENM01 = txtTIMENM01.Text.Replace(":", String.Empty)
                strTIMENM02 = txtTIMENM02.Text.Replace(":", String.Empty)
                strTIMENM03 = txtTIMENM03.Text.Replace(":", String.Empty)
                strTIMENM04 = txtTIMENM04.Text.Replace(":", String.Empty)
                strTIMENM05 = txtTIMENM05.Text.Replace(":", String.Empty)

                Msg = "時間帯に矛盾があります。"
                If Not String.IsNullOrEmpty(strTIMENM01) Then
                    If (Not String.IsNullOrEmpty(strTIMENM02)) And (strTIMENM01 >= strTIMENM02) Then
                        txtTIMENM02.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM03)) And (strTIMENM01 >= strTIMENM03) Then
                        txtTIMENM03.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM04)) And (strTIMENM01 >= strTIMENM04) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM01 >= strTIMENM05) Then
                        txtTIMENM05.Focus()
                        Return False
                    End If
                End If
                If Not String.IsNullOrEmpty(strTIMENM02) Then
                    If (Not String.IsNullOrEmpty(strTIMENM03)) And (strTIMENM02 >= strTIMENM03) Then
                        txtTIMENM03.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM04)) And (strTIMENM02 >= strTIMENM04) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM02 >= strTIMENM05) Then
                        txtTIMENM05.Focus()
                        Return False
                    End If
                End If
                If Not String.IsNullOrEmpty(strTIMENM03) Then
                    If (Not String.IsNullOrEmpty(strTIMENM04)) And (strTIMENM03 >= strTIMENM04) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM03 >= strTIMENM05) Then
                        txtTIMENM05.Focus()
                        Return False
                    End If
                End If
                If Not String.IsNullOrEmpty(strTIMENM04) Then
                    If (Not String.IsNullOrEmpty(strTIMENM05)) And (strTIMENM04 >= strTIMENM05) Then
                        txtTIMENM04.Focus()
                        Return False
                    End If
                End If
            Next



            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' システム情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSYSMTA() As Boolean
        Try
            Dim sql As New System.Text.StringBuilder

            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(",TO_CHAR(UPDDTM,'YYYYMMDD') AS UPDDAY")
            sql.Append(" FROM SYSMTA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            '優先単価
            UIUtility.SYSTEM.YUSENTANKA = CType(resultDt.Rows(0).Item("YUSENTANKA").ToString(), Integer)
            'システム更新日時
            UIUtility.SYSTEM.UPDDTM = resultDt.Rows(0).Item("UPDDAY").ToString()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 料金体系マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetRKNMTA() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM RKNMTA")
            sql.Append(" ORDER BY RKNKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim intCLRR As Integer = 0
            Dim intCLRG As Integer = 0
            Dim intCLRB As Integer = 0

            Me.cmbRKNKB_A.Items.Add(String.Empty)
            Me.cmbRKNKB_B.Items.Add(String.Empty)

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Me.cmbRKNKB_A.Items.Add(resultDt.Rows(i).Item("RKNNM").ToString)
                Me.cmbRKNKB_B.Items.Add(resultDt.Rows(i).Item("RKNNM").ToString)
                intCLRR = CType(resultDt.Rows(i).Item("CLRR").ToString, Integer)
                intCLRG = CType(resultDt.Rows(i).Item("CLRG").ToString, Integer)
                intCLRB = CType(resultDt.Rows(i).Item("CLRB").ToString, Integer)
                Select Case i
                    Case 0  '平日
                        Me.lblRKNNM01.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.lblRKNNM01.BackColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 1  '休日
                        Me.lblRKNNM02.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.lblRKNNM02.BackColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 2  '特日1
                        Me.lblRKNNM03.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.lblRKNNM03.BackColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 3  '特日2
                        Me.lblRKNNM04.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.lblRKNNM04.BackColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 4  '特日3
                        Me.lblRKNNM05.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.lblRKNNM05.BackColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                    Case 5  '特日4
                        Me.lblRKNNM06.Text = resultDt.Rows(i).Item("RKNNM").ToString
                        Me.lblRKNNM06.BackColor = Color.FromArgb(intCLRR, intCLRG, intCLRB)
                End Select
            Next

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY NKBNO ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.cmbKBMAST.DataSource = resultDt
            Me.cmbKBMAST.ValueMember = "NKBNO"
            Me.cmbKBMAST.DisplayMember = "CKBNAME"
            Me.cmbKBMAST.SelectedValue = 1

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' コピー元顧客種別情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCopyKBMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KBMAST")
            sql.Append(" ORDER BY KBMAST ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Me.cmbCopyKBMAST.DataSource = resultDt      '料金体系
            Me.cmbCopyKBMAST.ValueMember = "NKBNO"
            Me.cmbCopyKBMAST.DisplayMember = "CKBNAME"
            Me.cmbCopyKBMAST.SelectedValue = 1

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 営業情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNkbEIGMTA(Optional CopyFlg As Boolean = False) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM EIGMTA")
            sql.Append(" WHERE")
            If CopyFlg Then
                sql.Append(" NKBNO = " & (Me.cmbCopyKBMAST.SelectedIndex + 1))
            Else
                sql.Append(" NKBNO = " & (Me.cmbKBMAST.SelectedIndex + 1))
            End If
            sql.Append(" ORDER BY RKNKB,TIMEKB ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            '最新の更新日時取得
            If Not CopyFlg Then _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Dim dr As DataRow()

            '【平日】

            '時間帯1
            dr = resultDt.Select("RKNKB = 1 AND TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM01_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD01_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_01.ReadOnly = False
                '入場料
                Me.txtENTKIN01_01.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_01.Text.Equals("99") Then Me.txtENTKIN01_01.Visible = True
                'ポイント
                Me.txtPOINT01_01.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_01.Text.Equals("99") Then Me.txtPOINT01_01.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW01_01.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_01.Text.Equals("99") Then Me.txtPOINTW01_01.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS01_01.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_01.Text.Equals("99") Then Me.txtPOINTS01_01.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F01_01.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD01_01.Text.Equals("99") Then Me.txtBALLKIN1F01_01.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F01_01.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD01_01.Text.Equals("99") Then Me.txtBALLKIN2F01_01.Visible = True
                '打席指定区分
                Me.chkSITEIKBN01_01.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN01_01.Checked = True
            End If
            '時間帯2
            dr = resultDt.Select("RKNKB = 1 AND TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM02_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD02_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_01.ReadOnly = False
                '入場料
                Me.txtENTKIN02_01.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_01.Text.Equals("99") Then Me.txtENTKIN02_01.Visible = True
                'ポイント
                Me.txtPOINT02_01.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_01.Text.Equals("99") Then Me.txtPOINT02_01.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW02_01.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_01.Text.Equals("99") Then Me.txtPOINTW02_01.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS02_01.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_01.Text.Equals("99") Then Me.txtPOINTS02_01.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F02_01.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD02_01.Text.Equals("99") Then Me.txtBALLKIN1F02_01.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F02_01.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD02_01.Text.Equals("99") Then Me.txtBALLKIN2F02_01.Visible = True
                '打席指定区分
                Me.chkSITEIKBN02_01.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN02_01.Checked = True
            End If
            '時間帯3
            dr = resultDt.Select("RKNKB = 1 AND TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM03_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD03_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_01.ReadOnly = False
                '入場料
                Me.txtENTKIN03_01.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_01.Text.Equals("99") Then Me.txtENTKIN03_01.Visible = True
                'ポイント
                Me.txtPOINT03_01.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_01.Text.Equals("99") Then Me.txtPOINT03_01.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW03_01.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_01.Text.Equals("99") Then Me.txtPOINTW03_01.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS03_01.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_01.Text.Equals("99") Then Me.txtPOINTS03_01.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F03_01.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD03_01.Text.Equals("99") Then Me.txtBALLKIN1F03_01.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F03_01.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD03_01.Text.Equals("99") Then Me.txtBALLKIN2F03_01.Visible = True
                '打席指定区分
                Me.chkSITEIKBN03_01.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN03_01.Checked = True
            End If
            '時間帯4
            dr = resultDt.Select("RKNKB = 1 AND TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM04_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD04_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_01.ReadOnly = False
                '入場料
                Me.txtENTKIN04_01.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_01.Text.Equals("99") Then Me.txtENTKIN04_01.Visible = True
                'ポイント
                Me.txtPOINT04_01.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_01.Text.Equals("99") Then Me.txtPOINT04_01.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ　
                Me.txtPOINTW04_01.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_01.Text.Equals("99") Then Me.txtPOINTW04_01.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS04_01.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_01.Text.Equals("99") Then Me.txtPOINTS04_01.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F04_01.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD04_01.Text.Equals("99") Then Me.txtBALLKIN1F04_01.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F04_01.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD04_01.Text.Equals("99") Then Me.txtBALLKIN2F04_01.Visible = True
                '打席指定区分
                Me.chkSITEIKBN04_01.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN04_01.Checked = True
            End If
            '時間帯5
            dr = resultDt.Select("RKNKB = 1 AND TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM05_01.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD05_01.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_01.ReadOnly = False
                '入場料
                Me.txtENTKIN05_01.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_01.Text.Equals("99") Then Me.txtENTKIN05_01.Visible = True
                'ポイント
                Me.txtPOINT05_01.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_01.Text.Equals("99") Then Me.txtPOINT05_01.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW05_01.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_01.Text.Equals("99") Then Me.txtPOINTW05_01.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS05_01.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_01.Text.Equals("99") Then Me.txtPOINTS05_01.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F05_01.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD05_01.Text.Equals("99") Then Me.txtBALLKIN1F05_01.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F05_01.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD05_01.Text.Equals("99") Then Me.txtBALLKIN2F05_01.Visible = True
                '打席指定区分
                Me.chkSITEIKBN05_01.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN05_01.Checked = True
            End If

            '【休日】

            '時間帯1
            dr = resultDt.Select("RKNKB = 2 AND TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM01_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD01_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_02.ReadOnly = False
                '入場料
                Me.txtENTKIN01_02.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_02.Text.Equals("99") Then Me.txtENTKIN01_02.Visible = True
                'ポイント
                Me.txtPOINT01_02.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_02.Text.Equals("99") Then Me.txtPOINT01_02.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW01_02.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_02.Text.Equals("99") Then Me.txtPOINTW01_02.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS01_02.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_02.Text.Equals("99") Then Me.txtPOINTS01_02.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F01_02.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD01_02.Text.Equals("99") Then Me.txtBALLKIN1F01_02.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F01_02.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD01_02.Text.Equals("99") Then Me.txtBALLKIN2F01_02.Visible = True
                '打席指定区分
                Me.chkSITEIKBN01_02.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN01_02.Checked = True
            End If
            '時間帯2
            dr = resultDt.Select("RKNKB = 2 AND TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM02_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD02_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_02.ReadOnly = False
                '入場料
                Me.txtENTKIN02_02.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_02.Text.Equals("99") Then Me.txtENTKIN02_02.Visible = True
                'ポイント
                Me.txtPOINT02_02.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_02.Text.Equals("99") Then Me.txtPOINT02_02.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW02_02.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_02.Text.Equals("99") Then Me.txtPOINTW02_02.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS02_02.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_02.Text.Equals("99") Then Me.txtPOINTS02_02.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F02_02.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD02_02.Text.Equals("99") Then Me.txtBALLKIN1F02_02.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F02_02.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD02_02.Text.Equals("99") Then Me.txtBALLKIN2F02_02.Visible = True
                '打席指定区分
                Me.chkSITEIKBN02_02.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN02_02.Checked = True
            End If
            '時間帯3
            dr = resultDt.Select("RKNKB = 2 AND TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM03_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD03_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_02.ReadOnly = False
                '入場料
                Me.txtENTKIN03_02.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_02.Text.Equals("99") Then Me.txtENTKIN03_02.Visible = True
                'ポイント
                Me.txtPOINT03_02.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_02.Text.Equals("99") Then Me.txtPOINT03_02.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW03_02.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_02.Text.Equals("99") Then Me.txtPOINTW03_02.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS03_02.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_02.Text.Equals("99") Then Me.txtPOINTS03_02.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F03_02.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD03_02.Text.Equals("99") Then Me.txtBALLKIN1F03_02.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F03_02.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD03_02.Text.Equals("99") Then Me.txtBALLKIN2F03_02.Visible = True
                '打席指定区分
                Me.chkSITEIKBN03_02.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN03_02.Checked = True
            End If
            '時間帯4
            dr = resultDt.Select("RKNKB = 2 AND TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM04_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD04_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_02.ReadOnly = False
                '入場料
                Me.txtENTKIN04_02.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_02.Text.Equals("99") Then Me.txtENTKIN04_02.Visible = True
                'ポイント
                Me.txtPOINT04_02.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_02.Text.Equals("99") Then Me.txtPOINT04_02.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW04_02.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_02.Text.Equals("99") Then Me.txtPOINTW04_02.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS04_02.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_02.Text.Equals("99") Then Me.txtPOINTS04_02.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F04_02.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD04_02.Text.Equals("99") Then Me.txtBALLKIN1F04_02.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F04_02.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD04_02.Text.Equals("99") Then Me.txtBALLKIN2F04_02.Visible = True
                '打席指定区分
                Me.chkSITEIKBN04_02.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN04_02.Checked = True
            End If
            '時間帯5
            dr = resultDt.Select("RKNKB = 2 AND TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM05_02.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD05_02.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_02.ReadOnly = False
                '入場料
                Me.txtENTKIN05_02.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_02.Text.Equals("99") Then Me.txtENTKIN05_02.Visible = True
                'ポイント
                Me.txtPOINT05_02.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_02.Text.Equals("99") Then Me.txtPOINT05_02.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW05_02.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_02.Text.Equals("99") Then Me.txtPOINTW05_02.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS05_02.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_02.Text.Equals("99") Then Me.txtPOINTS05_02.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F05_02.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD05_02.Text.Equals("99") Then Me.txtBALLKIN1F05_02.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F05_02.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD05_02.Text.Equals("99") Then Me.txtBALLKIN2F05_02.Visible = True
                '打席指定区分
                Me.chkSITEIKBN05_02.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN05_02.Checked = True
            End If

            '【特日1】

            '時間帯1
            dr = resultDt.Select("RKNKB = 3 AND TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM01_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD01_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_03.ReadOnly = False
                '入場料
                Me.txtENTKIN01_03.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_03.Text.Equals("99") Then Me.txtENTKIN01_03.Visible = True
                'ポイント
                Me.txtPOINT01_03.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_03.Text.Equals("99") Then Me.txtPOINT01_03.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW01_03.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_03.Text.Equals("99") Then Me.txtPOINTW01_03.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS01_03.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_03.Text.Equals("99") Then Me.txtPOINTS01_03.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F01_03.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD01_03.Text.Equals("99") Then Me.txtBALLKIN1F01_03.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F01_03.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD01_03.Text.Equals("99") Then Me.txtBALLKIN2F01_03.Visible = True
                '打席指定区分
                Me.chkSITEIKBN01_03.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN01_03.Checked = True
            End If
            '時間帯2
            dr = resultDt.Select("RKNKB = 3 AND TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM02_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD02_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_03.ReadOnly = False
                '入場料
                Me.txtENTKIN02_03.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_03.Text.Equals("99") Then Me.txtENTKIN02_03.Visible = True
                'ポイント
                Me.txtPOINT02_03.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_03.Text.Equals("99") Then Me.txtPOINT02_03.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW02_03.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_03.Text.Equals("99") Then Me.txtPOINTW02_03.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS02_03.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_03.Text.Equals("99") Then Me.txtPOINTS02_03.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F02_03.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD02_03.Text.Equals("99") Then Me.txtBALLKIN1F02_03.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F02_03.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD02_03.Text.Equals("99") Then Me.txtBALLKIN2F02_03.Visible = True
                '打席指定区分
                Me.chkSITEIKBN02_03.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN02_03.Checked = True
            End If
            '時間帯3
            dr = resultDt.Select("RKNKB = 3 AND TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM03_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD03_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_03.ReadOnly = False
                '入場料
                Me.txtENTKIN03_03.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_03.Text.Equals("99") Then Me.txtENTKIN03_03.Visible = True
                'ポイント
                Me.txtPOINT03_03.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_03.Text.Equals("99") Then Me.txtPOINT03_03.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW03_03.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_03.Text.Equals("99") Then Me.txtPOINTW03_03.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS03_03.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_03.Text.Equals("99") Then Me.txtPOINTS03_03.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F03_03.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD03_03.Text.Equals("99") Then Me.txtBALLKIN1F03_03.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F03_03.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD03_03.Text.Equals("99") Then Me.txtBALLKIN2F03_03.Visible = True
                '打席指定区分
                Me.chkSITEIKBN03_03.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN03_03.Checked = True
            End If
            '時間帯4
            dr = resultDt.Select("RKNKB = 3 AND TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM04_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD04_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_03.ReadOnly = False
                '入場料
                Me.txtENTKIN04_03.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_03.Text.Equals("99") Then Me.txtENTKIN04_03.Visible = True
                'ポイント
                Me.txtPOINT04_03.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_03.Text.Equals("99") Then Me.txtPOINT04_03.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW04_03.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_03.Text.Equals("99") Then Me.txtPOINTW04_03.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS04_03.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_03.Text.Equals("99") Then Me.txtPOINTS04_03.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F04_03.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD04_03.Text.Equals("99") Then Me.txtBALLKIN1F04_03.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F04_03.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD04_03.Text.Equals("99") Then Me.txtBALLKIN2F04_03.Visible = True
                '打席指定区分
                Me.chkSITEIKBN04_03.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN04_03.Checked = True
            End If
            '時間帯5
            dr = resultDt.Select("RKNKB = 3 AND TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM05_03.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD05_03.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_03.ReadOnly = False
                '入場料
                Me.txtENTKIN05_03.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_03.Text.Equals("99") Then Me.txtENTKIN05_03.Visible = True
                'ポイント
                Me.txtPOINT05_03.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_03.Text.Equals("99") Then Me.txtPOINT05_03.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW05_03.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_03.Text.Equals("99") Then Me.txtPOINTW05_03.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS05_03.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_03.Text.Equals("99") Then Me.txtPOINTS05_03.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F05_03.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD05_03.Text.Equals("99") Then Me.txtBALLKIN1F05_03.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F05_03.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD05_03.Text.Equals("99") Then Me.txtBALLKIN2F05_03.Visible = True
                '打席指定区分
                Me.chkSITEIKBN05_03.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN05_03.Checked = True
            End If

            '【特日2】

            '時間帯1
            dr = resultDt.Select("RKNKB = 4 AND TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM01_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD01_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_04.ReadOnly = False
                '入場料
                Me.txtENTKIN01_04.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_04.Text.Equals("99") Then Me.txtENTKIN01_04.Visible = True
                'ポイント
                Me.txtPOINT01_04.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_04.Text.Equals("99") Then Me.txtPOINT01_04.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW01_04.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_04.Text.Equals("99") Then Me.txtPOINTW01_04.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS01_04.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_04.Text.Equals("99") Then Me.txtPOINTS01_04.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F01_04.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD01_04.Text.Equals("99") Then Me.txtBALLKIN1F01_04.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F01_04.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD01_04.Text.Equals("99") Then Me.txtBALLKIN2F01_04.Visible = True
                '打席指定区分
                Me.chkSITEIKBN01_04.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN01_04.Checked = True
            End If
            '時間帯2
            dr = resultDt.Select("RKNKB = 4 AND TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM02_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD02_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_04.ReadOnly = False
                '入場料
                Me.txtENTKIN02_04.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_04.Text.Equals("99") Then Me.txtENTKIN02_04.Visible = True
                'ポイント
                Me.txtPOINT02_04.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_04.Text.Equals("99") Then Me.txtPOINT02_04.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW02_04.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_04.Text.Equals("99") Then Me.txtPOINTW02_04.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS02_04.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_04.Text.Equals("99") Then Me.txtPOINTS02_04.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F02_04.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD02_04.Text.Equals("99") Then Me.txtBALLKIN1F02_04.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F02_04.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD02_04.Text.Equals("99") Then Me.txtBALLKIN2F02_04.Visible = True
                '打席指定区分
                Me.chkSITEIKBN02_04.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN02_04.Checked = True
            End If
            '時間帯3
            dr = resultDt.Select("RKNKB = 4 AND TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM03_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD03_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_04.ReadOnly = False
                '入場料
                Me.txtENTKIN03_04.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_04.Text.Equals("99") Then Me.txtENTKIN03_04.Visible = True
                'ポイント
                Me.txtPOINT03_04.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_04.Text.Equals("99") Then Me.txtPOINT03_04.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW03_04.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_04.Text.Equals("99") Then Me.txtPOINTW03_04.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS03_04.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_04.Text.Equals("99") Then Me.txtPOINTS03_04.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F03_04.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD03_04.Text.Equals("99") Then Me.txtBALLKIN1F03_04.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F03_04.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD03_04.Text.Equals("99") Then Me.txtBALLKIN2F03_04.Visible = True
                '打席指定区分
                Me.chkSITEIKBN03_04.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN03_04.Checked = True
            End If
            '時間帯4
            dr = resultDt.Select("RKNKB = 4 AND TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM04_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD04_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_04.ReadOnly = False
                '入場料
                Me.txtENTKIN04_04.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_04.Text.Equals("99") Then Me.txtENTKIN04_04.Visible = True
                'ポイント
                Me.txtPOINT04_04.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_04.Text.Equals("99") Then Me.txtPOINT04_04.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW04_04.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_04.Text.Equals("99") Then Me.txtPOINTW04_04.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS04_04.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_04.Text.Equals("99") Then Me.txtPOINTS04_04.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F04_04.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD04_04.Text.Equals("99") Then Me.txtBALLKIN1F04_04.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F04_04.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD04_04.Text.Equals("99") Then Me.txtBALLKIN2F04_04.Visible = True
                '打席指定区分
                Me.chkSITEIKBN04_04.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN04_04.Checked = True
            End If
            '時間帯5
            dr = resultDt.Select("RKNKB = 4 AND TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM05_04.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD05_04.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_04.ReadOnly = False
                '入場料
                Me.txtENTKIN05_04.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_04.Text.Equals("99") Then Me.txtENTKIN05_04.Visible = True
                'ポイント
                Me.txtPOINT05_04.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_04.Text.Equals("99") Then Me.txtPOINT05_04.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW05_04.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_04.Text.Equals("99") Then Me.txtPOINTW05_04.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS05_04.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_04.Text.Equals("99") Then Me.txtPOINTS05_04.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F05_04.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD05_04.Text.Equals("99") Then Me.txtBALLKIN1F05_04.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F05_04.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD05_04.Text.Equals("99") Then Me.txtBALLKIN2F05_04.Visible = True
                '打席指定区分
                Me.chkSITEIKBN05_04.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN05_04.Checked = True
            End If

            '【特日3】

            '時間帯1
            dr = resultDt.Select("RKNKB = 5 AND TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM01_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD01_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_05.ReadOnly = False
                '入場料
                Me.txtENTKIN01_05.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_05.Text.Equals("99") Then Me.txtENTKIN01_05.Visible = True
                'ポイント
                Me.txtPOINT01_05.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_05.Text.Equals("99") Then Me.txtPOINT01_05.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW01_05.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_05.Text.Equals("99") Then Me.txtPOINTW01_05.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS01_05.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_05.Text.Equals("99") Then Me.txtPOINTS01_05.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F01_05.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD01_05.Text.Equals("99") Then Me.txtBALLKIN1F01_05.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F01_05.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD01_05.Text.Equals("99") Then Me.txtBALLKIN2F01_05.Visible = True
                '打席指定区分
                Me.chkSITEIKBN01_05.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN01_05.Checked = True
            End If
            '時間帯2
            dr = resultDt.Select("RKNKB = 5 AND TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM02_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD02_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_05.ReadOnly = False
                '入場料
                Me.txtENTKIN02_05.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_05.Text.Equals("99") Then Me.txtENTKIN02_05.Visible = True
                'ポイント
                Me.txtPOINT02_05.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_05.Text.Equals("99") Then Me.txtPOINT02_05.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW02_05.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_05.Text.Equals("99") Then Me.txtPOINTW02_05.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS02_05.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_05.Text.Equals("99") Then Me.txtPOINTS02_05.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F02_05.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD02_05.Text.Equals("99") Then Me.txtBALLKIN1F02_05.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F02_05.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD02_05.Text.Equals("99") Then Me.txtBALLKIN2F02_05.Visible = True
                '打席指定区分
                Me.chkSITEIKBN02_05.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN02_05.Checked = True
            End If
            '時間帯3
            dr = resultDt.Select("RKNKB = 5 AND TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM03_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD03_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_05.ReadOnly = False
                '入場料
                Me.txtENTKIN03_05.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_05.Text.Equals("99") Then Me.txtENTKIN03_05.Visible = True
                'ポイント
                Me.txtPOINT03_05.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_05.Text.Equals("99") Then Me.txtPOINT03_05.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW03_05.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_05.Text.Equals("99") Then Me.txtPOINTW03_05.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS03_05.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_05.Text.Equals("99") Then Me.txtPOINTS03_05.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F03_05.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD03_05.Text.Equals("99") Then Me.txtBALLKIN1F03_05.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F03_05.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD03_05.Text.Equals("99") Then Me.txtBALLKIN2F03_05.Visible = True
                '打席指定区分
                Me.chkSITEIKBN03_05.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN03_05.Checked = True
            End If
            '時間帯4
            dr = resultDt.Select("RKNKB = 5 AND TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM04_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD04_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_05.ReadOnly = False
                '入場料
                Me.txtENTKIN04_05.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_05.Text.Equals("99") Then Me.txtENTKIN04_05.Visible = True
                'ポイント
                Me.txtPOINT04_05.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_05.Text.Equals("99") Then Me.txtPOINT04_05.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW04_05.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_05.Text.Equals("99") Then Me.txtPOINTW04_05.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS04_05.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_05.Text.Equals("99") Then Me.txtPOINTS04_05.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F04_05.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD04_05.Text.Equals("99") Then Me.txtBALLKIN1F04_05.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F04_05.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD04_05.Text.Equals("99") Then Me.txtBALLKIN2F04_05.Visible = True
                '打席指定区分
                Me.chkSITEIKBN04_05.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN04_05.Checked = True
            End If
            '時間帯5
            dr = resultDt.Select("RKNKB = 5 AND TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM05_05.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD05_05.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_05.ReadOnly = False
                '入場料
                Me.txtENTKIN05_05.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_05.Text.Equals("99") Then Me.txtENTKIN05_05.Visible = True
                'ポイント
                Me.txtPOINT05_05.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_05.Text.Equals("99") Then Me.txtPOINT05_05.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW05_05.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_05.Text.Equals("99") Then Me.txtPOINTW05_05.Visible = True
                'ｼﾆｱﾎﾟｲｲﾝﾄ
                Me.txtPOINTS05_05.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_05.Text.Equals("99") Then Me.txtPOINTS05_05.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F05_05.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD05_05.Text.Equals("99") Then Me.txtBALLKIN1F05_05.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F05_05.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD05_05.Text.Equals("99") Then Me.txtBALLKIN2F05_05.Visible = True
                '打席指定区分
                Me.chkSITEIKBN05_05.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN05_05.Checked = True
            End If

            '【特日4】

            '時間帯1
            dr = resultDt.Select("RKNKB = 6 AND TIMEKB = 1")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM01_06.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD01_06.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD01_06.ReadOnly = False
                '入場料
                Me.txtENTKIN01_06.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_06.Text.Equals("99") Then Me.txtENTKIN01_06.Visible = True
                'ポイント
                Me.txtPOINT01_06.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_06.Text.Equals("99") Then Me.txtPOINT01_06.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW01_06.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_06.Text.Equals("99") Then Me.txtPOINTW01_06.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS01_06.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD01_06.Text.Equals("99") Then Me.txtPOINTS01_06.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F01_06.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD01_06.Text.Equals("99") Then Me.txtBALLKIN1F01_06.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F01_06.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD01_06.Text.Equals("99") Then Me.txtBALLKIN2F01_06.Visible = True
                '打席指定区分
                Me.chkSITEIKBN01_06.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN01_06.Checked = True
            End If
            '時間帯2
            dr = resultDt.Select("RKNKB = 6 AND TIMEKB = 2")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM02_06.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD02_06.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD02_06.ReadOnly = False
                '入場料
                Me.txtENTKIN02_06.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_06.Text.Equals("99") Then Me.txtENTKIN02_06.Visible = True
                'ポイント
                Me.txtPOINT02_06.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_06.Text.Equals("99") Then Me.txtPOINT02_06.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW02_06.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_06.Text.Equals("99") Then Me.txtPOINTW02_06.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS02_06.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD02_06.Text.Equals("99") Then Me.txtPOINTS02_06.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F02_06.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD02_06.Text.Equals("99") Then Me.txtBALLKIN1F02_06.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F02_06.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD02_06.Text.Equals("99") Then Me.txtBALLKIN2F02_06.Visible = True
                '打席指定区分
                Me.chkSITEIKBN02_06.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN02_06.Checked = True
            End If
            '時間帯3
            dr = resultDt.Select("RKNKB = 6 AND TIMEKB = 3")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM03_06.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD03_06.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD03_06.ReadOnly = False
                '入場料
                Me.txtENTKIN03_06.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_06.Text.Equals("99") Then Me.txtENTKIN03_06.Visible = True
                'ポイント
                Me.txtPOINT03_06.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_06.Text.Equals("99") Then Me.txtPOINT03_06.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW03_06.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_06.Text.Equals("99") Then Me.txtPOINTW03_06.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS03_06.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD03_06.Text.Equals("99") Then Me.txtPOINTS03_06.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F03_06.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD03_06.Text.Equals("99") Then Me.txtBALLKIN1F03_06.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F03_06.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD03_06.Text.Equals("99") Then Me.txtBALLKIN2F03_06.Visible = True
                '打席指定区分
                Me.chkSITEIKBN03_06.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN03_06.Checked = True
            End If
            '時間帯4
            dr = resultDt.Select("RKNKB = 6 AND TIMEKB = 4")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM04_06.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD04_06.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD04_06.ReadOnly = False
                '入場料
                Me.txtENTKIN04_06.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_06.Text.Equals("99") Then Me.txtENTKIN04_06.Visible = True
                'ポイント
                Me.txtPOINT04_06.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_06.Text.Equals("99") Then Me.txtPOINT04_06.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW04_06.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_06.Text.Equals("99") Then Me.txtPOINTW04_06.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS04_06.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD04_06.Text.Equals("99") Then Me.txtPOINTS04_06.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F04_06.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD04_06.Text.Equals("99") Then Me.txtBALLKIN1F04_06.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F04_06.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD04_06.Text.Equals("99") Then Me.txtBALLKIN2F04_06.Visible = True
                '打席指定区分
                Me.chkSITEIKBN04_06.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN04_06.Checked = True
            End If
            '時間帯5
            dr = resultDt.Select("RKNKB = 6 AND TIMEKB = 5")
            If dr.Length > 0 Then
                '時間帯名
                Me.txtTIMENM05_06.Text = dr(0).Item("TIMENM").ToString.Substring(0, 2) & ":" & dr(0).Item("TIMENM").ToString.Substring(2, 2)
                'パスワード
                Me.txtPASSCD05_06.Text = dr(0).Item("PASSCD").ToString
                If Me.cmbKBMAST.SelectedIndex.Equals(0) Then Me.txtPASSCD05_06.ReadOnly = False
                '入場料
                Me.txtENTKIN05_06.Text = CType(dr(0).Item("ENTKIN").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_06.Text.Equals("99") Then Me.txtENTKIN05_06.Visible = True
                'ポイント
                Me.txtPOINT05_06.Text = CType(dr(0).Item("POINT").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_06.Text.Equals("99") Then Me.txtPOINT05_06.Visible = True
                'ﾚﾃﾞｨｰｽﾎﾟｲﾝﾄ
                Me.txtPOINTW05_06.Text = CType(dr(0).Item("POINTW").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_06.Text.Equals("99") Then Me.txtPOINTW05_06.Visible = True
                'ｼﾆｱﾎﾟｲﾝﾄ
                Me.txtPOINTS05_06.Text = CType(dr(0).Item("POINTS").ToString, Integer).ToString("#,##0")
                If Not Me.txtPASSCD05_06.Text.Equals("99") Then Me.txtPOINTS05_06.Visible = True
                'ボール単価(1F)
                Me.txtBALLKIN1F05_06.Text = dr(0).Item("BALLKIN1F").ToString
                If Not Me.txtPASSCD05_06.Text.Equals("99") Then Me.txtBALLKIN1F05_06.Visible = True
                'ボール単価(2F)
                Me.txtBALLKIN2F05_06.Text = dr(0).Item("BALLKIN2F").ToString
                If Not Me.txtPASSCD05_06.Text.Equals("99") Then Me.txtBALLKIN2F05_06.Visible = True
                '打席指定区分
                Me.chkSITEIKBN05_06.Checked = False
                If dr(0).Item("SITEIKBN").ToString.Equals("1") Then Me.chkSITEIKBN05_06.Checked = True
            End If

            Me.txtYUSENTANKA.Text = UIUtility.SYSTEM.YUSENTANKA.ToString

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Register() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            'トランザクション開始
            iDatabase.BeginTransaction()


            If String.IsNullOrEmpty(Me.txtYUSENTANKA.Text) Then
                UIUtility.SYSTEM.YUSENTANKA = 0
            Else
                UIUtility.SYSTEM.YUSENTANKA = CType(Me.txtYUSENTANKA.Text, Integer)
            End If
            sql.Clear()
            sql.Append("UPDATE SYSMTA SET ")
            '優先単価
            sql.Append("YUSENTANKA = " & UIUtility.SYSTEM.YUSENTANKA & ",")
            sql.Append("UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            If Not iDatabase.ExecuteUpdate("DELETE FROM EIGMTA WHERE NKBNO = " & (Me.cmbKBMAST.SelectedIndex + 1)) Then
                Return False
            End If

            Dim TIMENM As TextBox
            Dim PASSCD As TextBox
            Dim ENTKIN As TextBox
            Dim POINT As TextBox
            Dim POINTW As TextBox
            Dim POINTS As TextBox
            Dim BALLKIN1F As TextBox
            Dim BALLKIN2F As TextBox
            Dim intSITEIKBN As Integer
            Dim intUpdTIMEKB As Integer = 1
            Dim intUpdCount As Integer = 0  'SQL更新処理件数

            '【平日】
            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                POINTW = Nothing
                POINTS = Nothing
                BALLKIN1F = Nothing
                BALLKIN2F = Nothing
                intSITEIKBN = 0

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM01_01
                        PASSCD = Me.txtPASSCD01_01
                        ENTKIN = Me.txtENTKIN01_01
                        POINT = Me.txtPOINT01_01
                        POINTW = Me.txtPOINTW01_01
                        POINTS = Me.txtPOINTS01_01
                        BALLKIN1F = Me.txtBALLKIN1F01_01
                        BALLKIN2F = Me.txtBALLKIN2F01_01
                        If Me.chkSITEIKBN01_01.Checked Then intSITEIKBN = 1
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM02_01
                        PASSCD = Me.txtPASSCD02_01
                        ENTKIN = Me.txtENTKIN02_01
                        POINT = Me.txtPOINT02_01
                        POINTW = Me.txtPOINTW02_01
                        POINTS = Me.txtPOINTS02_01
                        BALLKIN1F = Me.txtBALLKIN1F02_01
                        BALLKIN2F = Me.txtBALLKIN2F02_01
                        If Me.chkSITEIKBN02_01.Checked Then intSITEIKBN = 1
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM03_01
                        PASSCD = Me.txtPASSCD03_01
                        ENTKIN = Me.txtENTKIN03_01
                        POINT = Me.txtPOINT03_01
                        POINTW = Me.txtPOINTW03_01
                        POINTS = Me.txtPOINTS03_01
                        BALLKIN1F = Me.txtBALLKIN1F03_01
                        BALLKIN2F = Me.txtBALLKIN2F03_01
                        If Me.chkSITEIKBN03_01.Checked Then intSITEIKBN = 1
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM04_01
                        PASSCD = Me.txtPASSCD04_01
                        ENTKIN = Me.txtENTKIN04_01
                        POINT = Me.txtPOINT04_01
                        POINTW = Me.txtPOINTW04_01
                        POINTS = Me.txtPOINTS04_01
                        BALLKIN1F = Me.txtBALLKIN1F04_01
                        BALLKIN2F = Me.txtBALLKIN2F04_01
                        If Me.chkSITEIKBN04_01.Checked Then intSITEIKBN = 1
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM05_01
                        PASSCD = Me.txtPASSCD05_01
                        ENTKIN = Me.txtENTKIN05_01
                        POINT = Me.txtPOINT05_01
                        POINTW = Me.txtPOINTW05_01
                        POINTS = Me.txtPOINTS05_01
                        BALLKIN1F = Me.txtBALLKIN1F05_01
                        BALLKIN2F = Me.txtBALLKIN2F05_01
                        If Me.chkSITEIKBN05_01.Checked Then intSITEIKBN = 1
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTA VALUES(")
                sql.Append("1,")                                                            '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワード・打席指定区分を更新
                intUpdCount = 0
                For j As Integer = 1 To 12  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTA SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "',SITEIKBN = " & intSITEIKBN & " WHERE RKNKB = 1 AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTA VALUES(")
                        sql.Append("1,")                                                            '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                        sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                        sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                        sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                        sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                        sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTA WHERE RKNKB = 1 AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            '【休日】
            intUpdTIMEKB = 1
            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                POINTW = Nothing
                POINTS = Nothing
                BALLKIN1F = Nothing
                BALLKIN2F = Nothing
                intSITEIKBN = 0

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM01_02
                        PASSCD = Me.txtPASSCD01_02
                        ENTKIN = Me.txtENTKIN01_02
                        POINT = Me.txtPOINT01_02
                        POINTW = Me.txtPOINTW01_02
                        POINTS = Me.txtPOINTS01_02
                        BALLKIN1F = Me.txtBALLKIN1F01_02
                        BALLKIN2F = Me.txtBALLKIN2F01_02
                        If Me.chkSITEIKBN01_02.Checked Then intSITEIKBN = 1
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM02_02
                        PASSCD = Me.txtPASSCD02_02
                        ENTKIN = Me.txtENTKIN02_02
                        POINT = Me.txtPOINT02_02
                        POINTW = Me.txtPOINTW02_02
                        POINTS = Me.txtPOINTS02_02
                        BALLKIN1F = Me.txtBALLKIN1F02_02
                        BALLKIN2F = Me.txtBALLKIN2F02_02
                        If Me.chkSITEIKBN02_02.Checked Then intSITEIKBN = 1
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM03_02
                        PASSCD = Me.txtPASSCD03_02
                        ENTKIN = Me.txtENTKIN03_02
                        POINT = Me.txtPOINT03_02
                        POINTW = Me.txtPOINTW03_02
                        POINTS = Me.txtPOINTS03_02
                        BALLKIN1F = Me.txtBALLKIN1F03_02
                        BALLKIN2F = Me.txtBALLKIN2F03_02
                        If Me.chkSITEIKBN03_02.Checked Then intSITEIKBN = 1
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM04_02
                        PASSCD = Me.txtPASSCD04_02
                        ENTKIN = Me.txtENTKIN04_02
                        POINT = Me.txtPOINT04_02
                        POINTW = Me.txtPOINTW04_02
                        POINTS = Me.txtPOINTS04_02
                        BALLKIN1F = Me.txtBALLKIN1F04_02
                        BALLKIN2F = Me.txtBALLKIN2F04_02
                        If Me.chkSITEIKBN04_02.Checked Then intSITEIKBN = 1
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM05_02
                        PASSCD = Me.txtPASSCD05_02
                        ENTKIN = Me.txtENTKIN05_02
                        POINT = Me.txtPOINT05_02
                        POINTW = Me.txtPOINTW05_02
                        POINTS = Me.txtPOINTS05_02
                        BALLKIN1F = Me.txtBALLKIN1F05_02
                        BALLKIN2F = Me.txtBALLKIN2F05_02
                        If Me.chkSITEIKBN05_02.Checked Then intSITEIKBN = 1
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTA VALUES(")
                sql.Append("2,")                                                            '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワードを更新
                intUpdCount = 0
                For j As Integer = 1 To 12  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTA SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "',SITEIKBN = " & intSITEIKBN & " WHERE RKNKB = 2 AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTA VALUES(")
                        sql.Append("2,")                                                            '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                        sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                        sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                        sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                        sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                        sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTA WHERE RKNKB = 2 AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            '【特日1】
            intUpdTIMEKB = 1
            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                POINTW = Nothing
                POINTS = Nothing
                BALLKIN1F = Nothing
                BALLKIN2F = Nothing
                intSITEIKBN = 0

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM01_03
                        PASSCD = Me.txtPASSCD01_03
                        ENTKIN = Me.txtENTKIN01_03
                        POINT = Me.txtPOINT01_03
                        POINTW = Me.txtPOINTW01_03
                        POINTS = Me.txtPOINTS01_03
                        BALLKIN1F = Me.txtBALLKIN1F01_03
                        BALLKIN2F = Me.txtBALLKIN2F01_03
                        If Me.chkSITEIKBN01_03.Checked Then intSITEIKBN = 1
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM02_03
                        PASSCD = Me.txtPASSCD02_03
                        ENTKIN = Me.txtENTKIN02_03
                        POINT = Me.txtPOINT02_03
                        POINTW = Me.txtPOINTW02_03
                        POINTS = Me.txtPOINTS02_03
                        BALLKIN1F = Me.txtBALLKIN1F02_03
                        BALLKIN2F = Me.txtBALLKIN2F02_03
                        If Me.chkSITEIKBN02_03.Checked Then intSITEIKBN = 1
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM03_03
                        PASSCD = Me.txtPASSCD03_03
                        ENTKIN = Me.txtENTKIN03_03
                        POINT = Me.txtPOINT03_03
                        POINTW = Me.txtPOINTW03_03
                        POINTS = Me.txtPOINTS03_03
                        BALLKIN1F = Me.txtBALLKIN1F03_03
                        BALLKIN2F = Me.txtBALLKIN2F03_03
                        If Me.chkSITEIKBN03_03.Checked Then intSITEIKBN = 1
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM04_03
                        PASSCD = Me.txtPASSCD04_03
                        ENTKIN = Me.txtENTKIN04_03
                        POINT = Me.txtPOINT04_03
                        POINTW = Me.txtPOINTW04_03
                        POINTS = Me.txtPOINTS04_03
                        BALLKIN1F = Me.txtBALLKIN1F04_03
                        BALLKIN2F = Me.txtBALLKIN2F04_03
                        If Me.chkSITEIKBN04_03.Checked Then intSITEIKBN = 1
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM05_03
                        PASSCD = Me.txtPASSCD05_03
                        ENTKIN = Me.txtENTKIN05_03
                        POINT = Me.txtPOINT05_03
                        POINTW = Me.txtPOINTW05_03
                        POINTS = Me.txtPOINTS05_03
                        BALLKIN1F = Me.txtBALLKIN1F05_03
                        BALLKIN2F = Me.txtBALLKIN2F05_03
                        If Me.chkSITEIKBN05_03.Checked Then intSITEIKBN = 1
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTA VALUES(")
                sql.Append("3,")                                                            '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワードを更新
                intUpdCount = 0
                For j As Integer = 1 To 12  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTA SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "',SITEIKBN = " & intSITEIKBN & " WHERE RKNKB = 3 AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTA VALUES(")
                        sql.Append("3,")                                                            '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                        sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                        sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                        sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                        sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                        sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTA WHERE RKNKB = 3 AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            '【特日2】
            intUpdTIMEKB = 1
            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                POINTW = Nothing
                POINTS = Nothing
                BALLKIN1F = Nothing
                BALLKIN2F = Nothing
                intSITEIKBN = 0

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM01_04
                        PASSCD = Me.txtPASSCD01_04
                        ENTKIN = Me.txtENTKIN01_04
                        POINT = Me.txtPOINT01_04
                        POINTW = Me.txtPOINTW01_04
                        POINTS = Me.txtPOINTS01_04
                        BALLKIN1F = Me.txtBALLKIN1F01_04
                        BALLKIN2F = Me.txtBALLKIN2F01_04
                        If Me.chkSITEIKBN01_04.Checked Then intSITEIKBN = 1
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM02_04
                        PASSCD = Me.txtPASSCD02_04
                        ENTKIN = Me.txtENTKIN02_04
                        POINT = Me.txtPOINT02_04
                        POINTW = Me.txtPOINTW02_04
                        POINTS = Me.txtPOINTS02_04
                        BALLKIN1F = Me.txtBALLKIN1F02_04
                        BALLKIN2F = Me.txtBALLKIN2F02_04
                        If Me.chkSITEIKBN02_04.Checked Then intSITEIKBN = 1
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM03_04
                        PASSCD = Me.txtPASSCD03_04
                        ENTKIN = Me.txtENTKIN03_04
                        POINT = Me.txtPOINT03_04
                        POINTW = Me.txtPOINTW03_04
                        POINTS = Me.txtPOINTS03_04
                        BALLKIN1F = Me.txtBALLKIN1F03_04
                        BALLKIN2F = Me.txtBALLKIN2F03_04
                        If Me.chkSITEIKBN03_04.Checked Then intSITEIKBN = 1
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM04_04
                        PASSCD = Me.txtPASSCD04_04
                        ENTKIN = Me.txtENTKIN04_04
                        POINT = Me.txtPOINT04_04
                        POINTW = Me.txtPOINTW04_04
                        POINTS = Me.txtPOINTS04_04
                        BALLKIN1F = Me.txtBALLKIN1F04_04
                        BALLKIN2F = Me.txtBALLKIN2F04_04
                        If Me.chkSITEIKBN04_04.Checked Then intSITEIKBN = 1
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM05_04
                        PASSCD = Me.txtPASSCD05_04
                        ENTKIN = Me.txtENTKIN05_04
                        POINT = Me.txtPOINT05_04
                        POINTW = Me.txtPOINTW05_04
                        POINTS = Me.txtPOINTS05_04
                        BALLKIN1F = Me.txtBALLKIN1F05_04
                        BALLKIN2F = Me.txtBALLKIN2F05_04
                        If Me.chkSITEIKBN05_04.Checked Then intSITEIKBN = 1
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTA VALUES(")
                sql.Append("4,")                                                            '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワードを更新
                intUpdCount = 0
                For j As Integer = 1 To 12  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTA SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "',SITEIKBN = " & intSITEIKBN & " WHERE RKNKB = 4 AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTA VALUES(")
                        sql.Append("4,")                                                            '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                        sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                        sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                        sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                        sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                        sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTA WHERE RKNKB = 4 AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            '【特日3】
            intUpdTIMEKB = 1
            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                POINTW = Nothing
                POINTS = Nothing
                BALLKIN1F = Nothing
                BALLKIN2F = Nothing
                intSITEIKBN = 0

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM01_05
                        PASSCD = Me.txtPASSCD01_05
                        ENTKIN = Me.txtENTKIN01_05
                        POINT = Me.txtPOINT01_05
                        POINTW = Me.txtPOINTW01_05
                        POINTS = Me.txtPOINTS01_05
                        BALLKIN1F = Me.txtBALLKIN1F01_05
                        BALLKIN2F = Me.txtBALLKIN2F01_05
                        If Me.chkSITEIKBN01_05.Checked Then intSITEIKBN = 1
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM02_05
                        PASSCD = Me.txtPASSCD02_05
                        ENTKIN = Me.txtENTKIN02_05
                        POINT = Me.txtPOINT02_05
                        POINTW = Me.txtPOINTW02_05
                        POINTS = Me.txtPOINTS02_05
                        BALLKIN1F = Me.txtBALLKIN1F02_05
                        BALLKIN2F = Me.txtBALLKIN2F02_05
                        If Me.chkSITEIKBN02_05.Checked Then intSITEIKBN = 1
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM03_05
                        PASSCD = Me.txtPASSCD03_05
                        ENTKIN = Me.txtENTKIN03_05
                        POINT = Me.txtPOINT03_05
                        POINTW = Me.txtPOINTW03_05
                        POINTS = Me.txtPOINTS03_05
                        BALLKIN1F = Me.txtBALLKIN1F03_05
                        BALLKIN2F = Me.txtBALLKIN2F03_05
                        If Me.chkSITEIKBN03_05.Checked Then intSITEIKBN = 1
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM04_05
                        PASSCD = Me.txtPASSCD04_05
                        ENTKIN = Me.txtENTKIN04_05
                        POINT = Me.txtPOINT04_05
                        POINTW = Me.txtPOINTW04_05
                        POINTS = Me.txtPOINTS04_05
                        BALLKIN1F = Me.txtBALLKIN1F04_05
                        BALLKIN2F = Me.txtBALLKIN2F04_05
                        If Me.chkSITEIKBN04_05.Checked Then intSITEIKBN = 1
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM05_05
                        PASSCD = Me.txtPASSCD05_05
                        ENTKIN = Me.txtENTKIN05_05
                        POINT = Me.txtPOINT05_05
                        POINTW = Me.txtPOINTW05_05
                        POINTS = Me.txtPOINTS05_05
                        BALLKIN1F = Me.txtBALLKIN1F05_05
                        BALLKIN2F = Me.txtBALLKIN2F05_05
                        If Me.chkSITEIKBN05_05.Checked Then intSITEIKBN = 1
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTA VALUES(")
                sql.Append("5,")                                                            '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワードを更新
                intUpdCount = 0
                For j As Integer = 1 To 12  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTA SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "',SITEIKBN = " & intSITEIKBN & " WHERE RKNKB = 5 AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTA VALUES(")
                        sql.Append("5,")                                                            '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                        sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                        sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                        sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                        sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                        sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTA WHERE RKNKB = 5 AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            '【特日4】
            intUpdTIMEKB = 1
            For i As Integer = 1 To 5       '時間帯区分(1～5)

                TIMENM = Nothing
                PASSCD = Nothing
                ENTKIN = Nothing
                POINT = Nothing
                POINTW = Nothing
                POINTS = Nothing
                BALLKIN1F = Nothing
                BALLKIN2F = Nothing
                intSITEIKBN = 0

                Select Case i
                    Case 1
                        '時間帯1
                        TIMENM = Me.txtTIMENM01_06
                        PASSCD = Me.txtPASSCD01_06
                        ENTKIN = Me.txtENTKIN01_06
                        POINT = Me.txtPOINT01_06
                        POINTW = Me.txtPOINTW01_06
                        POINTS = Me.txtPOINTS01_06
                        BALLKIN1F = Me.txtBALLKIN1F01_06
                        BALLKIN2F = Me.txtBALLKIN2F01_06
                        If Me.chkSITEIKBN01_06.Checked Then intSITEIKBN = 1
                    Case 2
                        '時間帯2
                        TIMENM = Me.txtTIMENM02_06
                        PASSCD = Me.txtPASSCD02_06
                        ENTKIN = Me.txtENTKIN02_06
                        POINT = Me.txtPOINT02_06
                        POINTW = Me.txtPOINTW02_06
                        POINTS = Me.txtPOINTS02_06
                        BALLKIN1F = Me.txtBALLKIN1F02_06
                        BALLKIN2F = Me.txtBALLKIN2F02_06
                        If Me.chkSITEIKBN02_06.Checked Then intSITEIKBN = 1
                    Case 3
                        '時間帯3
                        TIMENM = Me.txtTIMENM03_06
                        PASSCD = Me.txtPASSCD03_06
                        ENTKIN = Me.txtENTKIN03_06
                        POINT = Me.txtPOINT03_06
                        POINTW = Me.txtPOINTW03_06
                        POINTS = Me.txtPOINTS03_06
                        BALLKIN1F = Me.txtBALLKIN1F03_06
                        BALLKIN2F = Me.txtBALLKIN2F03_06
                        If Me.chkSITEIKBN03_06.Checked Then intSITEIKBN = 1
                    Case 4
                        '時間帯4
                        TIMENM = Me.txtTIMENM04_06
                        PASSCD = Me.txtPASSCD04_06
                        ENTKIN = Me.txtENTKIN04_06
                        POINT = Me.txtPOINT04_06
                        POINTW = Me.txtPOINTW04_06
                        POINTS = Me.txtPOINTS04_06
                        BALLKIN1F = Me.txtBALLKIN1F04_06
                        BALLKIN2F = Me.txtBALLKIN2F04_06
                        If Me.chkSITEIKBN04_06.Checked Then intSITEIKBN = 1
                    Case 5
                        '時間帯5
                        TIMENM = Me.txtTIMENM05_06
                        PASSCD = Me.txtPASSCD05_06
                        ENTKIN = Me.txtENTKIN05_06
                        POINT = Me.txtPOINT05_06
                        POINTW = Me.txtPOINTW05_06
                        POINTS = Me.txtPOINTS05_06
                        BALLKIN1F = Me.txtBALLKIN1F05_06
                        BALLKIN2F = Me.txtBALLKIN2F05_06
                        If Me.chkSITEIKBN05_06.Checked Then intSITEIKBN = 1
                End Select
                If String.IsNullOrEmpty(TIMENM.Text) Then
                    Continue For
                End If
                sql.Clear()
                sql.Append("INSERT INTO EIGMTA VALUES(")
                sql.Append("6,")                                                            '料金体系区分
                sql.Append((Me.cmbKBMAST.SelectedIndex + 1) & ",")                          '種別区分
                sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                sql.Append("NOW(),")
                sql.Append("NOW())")
                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
                '全種別共通の時間帯・パスワードを更新
                intUpdCount = 0
                For j As Integer = 1 To 12  '(顧客種別数)
                    sql.Clear()
                    sql.Append("UPDATE EIGMTA SET TIMENM = " & "'" & TIMENM.Text.Replace(":", String.Empty) & "',PASSCD = '" & PASSCD.Text & "',SITEIKBN = " & intSITEIKBN & " WHERE RKNKB = 6 AND TIMEKB = " & intUpdTIMEKB _
                              & " AND NKBNO = " & j)
                    If Not iDatabase.ExecuteUpdate(sql.ToString, intUpdCount) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                    If intUpdCount.Equals(0) Then
                        sql.Clear()
                        sql.Append("INSERT INTO EIGMTA VALUES(")
                        sql.Append("6,")                                                            '料金体系区分
                        sql.Append(j & ",")                                                         '種別区分
                        sql.Append(intUpdTIMEKB & ",")                                              '時間帯区分
                        sql.Append("'" & TIMENM.Text.Replace(":", String.Empty) & "',")             '時間帯名
                        sql.Append("'" & PASSCD.Text & "',")                                        'パスワード
                        sql.Append(CType(ENTKIN.Text, Integer) & ",")                               '入場料
                        sql.Append(CType(POINT.Text, Integer) & ",")                                'ポイント
                        sql.Append(CType(POINTW.Text, Integer) & ",")                               'レディースポイント
                        sql.Append(CType(POINTS.Text, Integer) & ",")                               'シニアポイント
                        sql.Append(CType(BALLKIN1F.Text, Integer) & ",")                            'ボール単価(1F)
                        sql.Append(CType(BALLKIN2F.Text, Integer) & ",")                            'ボール単価(2F)
                        sql.Append("0,")                                                            'ボール単価(3F) ※未使用
                        sql.Append(intSITEIKBN & ",")                                               '打席指定区分
                        sql.Append("NOW(),")
                        sql.Append("NOW())")
                        If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                            iDatabase.RollBack()
                            Return False
                        End If
                    End If
                Next
                intUpdTIMEKB += 1
            Next
            '全種別の時間帯を種別1に合わせる為に削除
            sql.Clear()
            sql.Append("DELETE FROM EIGMTA WHERE RKNKB = 6 AND TIMEKB > " & (intUpdTIMEKB - 1))
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            'コミット
            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function

#End Region



End Class




