Imports TECHNO.DataBase

Public Class frmSYSMT01

#Region "▼宣言部"

    Private _dtUPDDTM As DateTime     '画面表示時の最新の更新日時

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' SEフラグ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SEFLG As Boolean
        Set(ByVal value As Boolean)
            _blnSEFLG = value
        End Set
    End Property
    Private _blnSEFLG As Boolean = False
#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "システム情報登録"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "システム情報登録"

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
    Private Sub frmSYSMT01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            '画面初期設定
            Init()

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

            'コンフィグ情報表示
            SetConfigInfo()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 消費税テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtTAX_Validated(sender As System.Object, e As System.EventArgs) Handles txtTAX.Validated
        Try
            If String.IsNullOrEmpty(Me.txtTAX.Text) Then
                Me.txtTAX.Text = "0"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 全打席数テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSEATSU_Validated(sender As System.Object, e As System.EventArgs) Handles txtSEATSU.Validated
        Try
            If String.IsNullOrEmpty(Me.txtSEATSU.Text) Then
                Exit Sub
            End If
            If CType(Me.txtSEATSU.Text, Integer) > 100 Then
                Me.txtSEATSU.Text = "100"
            End If

            Select Case Me.txtFLRSU.Text
                Case "1"
                    Me.txtLSTNO1F.Text = Me.txtSEATSU.Text
                Case "2"
                    Me.txtLSTNO2F.Text = Me.txtSEATSU.Text
                Case "3"
                    Me.txtLSTNO3F.Text = Me.txtSEATSU.Text
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フロア数テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtFLRSU_Validated(sender As System.Object, e As System.EventArgs) Handles txtFLRSU.Validated
        Try
            If String.IsNullOrEmpty(Me.txtFLRSU.Text) Then
                Me.txtFLRSU.Text = "1"
            ElseIf Me.txtFLRSU.Text > "3" Then
                Me.txtFLRSU.Text = "1"
            End If

            Select Case Me.txtFLRSU.Text
                Case "1"
                    Me.lblLSTNO1F.Visible = True
                    Me.txtLSTNO1F.Visible = True
                    Me.txtLSTNO1F.Text = Me.txtSEATSU.Text
                    Me.lblLSTNO2F.Visible = False
                    Me.txtLSTNO2F.Visible = False
                    Me.txtLSTNO2F.Text = "0"
                    Me.lblLSTNO3F.Visible = False
                    Me.txtLSTNO3F.Visible = False
                    Me.txtLSTNO3F.Text = "0"
                Case "2"
                    Me.lblLSTNO1F.Visible = True
                    Me.txtLSTNO1F.Visible = True
                    Me.lblLSTNO2F.Visible = True
                    Me.txtLSTNO2F.Visible = True
                    Me.txtLSTNO2F.Text = Me.txtSEATSU.Text
                    Me.lblLSTNO3F.Visible = False
                    Me.txtLSTNO3F.Visible = False
                    Me.txtLSTNO3F.Text = "0"
                Case "3"
                    Me.lblLSTNO1F.Visible = True
                    Me.txtLSTNO1F.Visible = True
                    Me.lblLSTNO2F.Visible = True
                    Me.txtLSTNO2F.Visible = True
                    Me.lblLSTNO3F.Visible = True
                    Me.txtLSTNO3F.Visible = True
                    Me.txtLSTNO3F.Text = Me.txtSEATSU.Text
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 入金限度額
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtZANMAX_Validated(sender As System.Object, e As System.EventArgs) Handles txtZANMAX.Validated
        Try
            If String.IsNullOrEmpty(Me.txtZANMAX.Text) Then
                Me.txtZANMAX.Text = "0"
                Exit Sub
            End If

            If CType(Me.txtZANMAX.Text, Integer) > 65000 Then
                Using frm As New frmMSGBOX01("65,000円以下で入力してください。", 2)
                    frm.ShowDialog()
                End Using
                Me.txtZANMAX.Text = "65,000"
                Me.txtZANMAX.Focus()
                Exit Sub
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
    Private Sub txtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFLRSU.KeyPress, txtSEATSU.KeyPress, txtLSTNO1F.KeyPress, txtLSTNO2F.KeyPress, txtLSTNO3F.KeyPress _
                                                                                                                , txtLRMULTI01.KeyPress, txtLRMULTI02.KeyPress, txtLRMULTI03.KeyPress, txtLRMULTI04.KeyPress, txtLRMULTI05.KeyPress _
                                                                                                                , txtTAX.KeyPress
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
    ''' 帳票クリアボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRepoClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepoClear.Click
        Try
            Using frm As New frmMSGBOX01("帳票データの削除を行いますか？", 1)
                frm.ShowDialog()
                If Not frm.Reply Then
                    Exit Sub
                End If
            End Using



            '*** トランザクション開始 ***'
            iDatabase.BeginTransaction()


            '打席情報ワーク
            If Not iDatabase.ExecuteUpdate("DELETE FROM SEATWORK") Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("打席情報ワークの削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '打席情報サマリA
            If Not iDatabase.ExecuteUpdate("DELETE FROM SEATSMA") Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("打席情報サマリAの削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '打席情報サマリB
            If Not iDatabase.ExecuteUpdate("DELETE FROM SEATSMB") Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("打席情報サマリBの削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If
            '打席情報サマリC
            If Not iDatabase.ExecuteUpdate("DELETE FROM SEATSMC") Then
                iDatabase.RollBack()
                Using frm As New frmMSGBOX01("打席情報サマリCの削除に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            '*** コミット ***'
            iDatabase.Commit()

            Using frm As New frmMSGBOX01("正常にデータ削除できました。システムを再起動してください。", 0)
                frm.ShowDialog()
            End Using

        Catch ex As Exception
            '*** ロールバック ***'
            iDatabase.RollBack()
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

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

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
            End If

            'コンフィグ情報取得
            SetConfigInfo()

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




            '【更新】

            '他端末からの更新チェック
            Dim resultDt As DataTable = iDatabase.ExecuteRead(" SELECT * FROM SYSMTA WHERE SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")
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

            '更新登録
            If Not UpdRegister() Then
                Using frm As New frmMSGBOX01("システム情報の更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
                Exit Sub
            End If

            Using frm As New frmMSGBOX01("正常に登録できました。", 0)
                frm.ShowDialog()
            End Using

            '画面初期設定
            Init()

            'システム情報取得
            If Not GetSYSMTA() Then
                Using frm As New frmMSGBOX01("システム情報が見つかりませんでした。", 3)
                    frm.ShowDialog()
                End Using
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

            'センター名
            Me.txtSHOPNM.Text = String.Empty
            '管理者パスワード
            Me.txtADMNPW.Text = String.Empty
            'SEパスワード
            Me.txtSEPW.Text = String.Empty
            '球数税率
            Me.txtTAMATAX.Text = String.Empty
            '税率
            Me.txtTAX.Text = String.Empty
            '税区分
            Me.cmbTAXKBN.SelectedIndex = 0
            '税端数区分
            Me.cmbTAXHASUKBN.SelectedIndex = 0
            'フロア数
            Me.txtFLRSU.Text = "0"
            '全打席数
            Me.txtSEATSU.Text = "0"
            '1F最終打席№
            Me.lblLSTNO1F.Visible = True
            Me.txtLSTNO1F.Text = "0"
            '2F最終打席№
            Me.lblLSTNO2F.Visible = True
            Me.txtLSTNO2F.Text = "0"
            '3F最終打席№
            Me.lblLSTNO3F.Visible = True
            Me.txtLSTNO3F.Text = "0"
            'カレンダー作成最終日付
            Me.txtCALLSTDT.Text = String.Empty
            '左右兼用打席
            Me.txtLRMULTI01.Text = String.Empty
            Me.txtLRMULTI02.Text = String.Empty
            Me.txtLRMULTI03.Text = String.Empty
            Me.txtLRMULTI04.Text = String.Empty
            Me.txtLRMULTI05.Text = String.Empty
            '担当者確認フラグ
            Me.cmbTANCHKFLG.SelectedIndex = 0
            '会員期限調整日数
            Me.cmbCALLMT.SelectedIndex = 5
            '打席情報画面フラグ
            Me.cmbDISPMULTI.SelectedIndex = 0
            '月間来場クリア月
            Me.txtCLRENTMCNT.Text = String.Empty
            'OSシャットダウン
            Me.cmbOSDOWNFLG.SelectedIndex = 0
            'レシート印刷フラグ
            Me.cmbRECEIPTFLG.SelectedIndex = 0
            '打ち放題終了時刻
            Me.txtJrHH.Text = String.Empty
            Me.txtJrMM.Text = String.Empty
            '入金限度額
            Me.txtZANMAX.Text = "0"
            'カード残金有効期限
            Me.txtCARDLIMIT.Text = "0"
            '商品引落しﾌﾟﾚﾐｱﾑ精算区分
            Me.cmbHINPREMPAYKBN.SelectedIndex = 0
            'ｶｰﾄﾞ支出ﾌﾟﾚﾐｱﾑ精算区分
            Me.cmbSHTPREMPAYKBN.SelectedIndex = 0
            '入金残高有効期限
            Me.txtPREMLIMIT.Text = "0"
            

            For Each sp As String In My.Computer.Ports.SerialPortNames
                Me.cmbPreRW.Items.Add(sp)
                Me.cmbSEAT.Items.Add(sp)
            Next

            If _blnSEFLG Then
                Me.pnlNoUse.Visible = True
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' コンフィグ情報表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConfigInfo()
        Try
            '構成ファイルのパスを取得
            Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim appConfigPath As String = System.IO.Directory.GetCurrentDirectory() & "\MENU01.exe.config"

            '構成ファイルをXML DOMに読み込む
            Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            doc.Load(appConfigPath)

            Dim node As System.Xml.XmlNode = doc("configuration")("appSettings")

            '--------------------------------------------------------------------
            '構成ファイルの"key"="Application Name"の値を
            '"value"="MyNewApplication"に変更する
            '--------------------------------------------------------------------
            'ノードを探す
            Dim n As System.Xml.XmlNode
            For Each n In doc("configuration")("appSettings")
                If n.Name = "add" Then
                    '"key"="Application Name"のとき、"value"を変更する
                    Select Case n.Attributes.GetNamedItem("key").Value
                        Case "SYSTEMKBN"
                            'システム区分【0】打席管理【1】その他
                            If n.Attributes.GetNamedItem("value").Value.Equals("0") Then
                                Me.rdoSYSTEMKBN0.Checked = True
                            Else
                                Me.rdoSYSTEMKBN1.Checked = True
                            End If
                        Case "ICR700RW_COM"
                            'ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞRWポート
                            Me.cmbPreRW.Text = n.Attributes.GetNamedItem("value").Value
                        Case "SEAT_COM"
                            '打席通信ポート
                            Me.cmbSEAT.Text = n.Attributes.GetNamedItem("value").Value
                        Case "MULTIDISP_TYPE"
                            'ストーブ表示【0】なし【1】あり
                            If n.Attributes.GetNamedItem("value").Value.Equals("0") Then
                                Me.rdoStoveDisabled.Checked = True
                            Else
                                Me.rdoStoveEnabled.Checked = True
                            End If
                        Case "MENU_KBN"
                            'メニュー選択区分
                            Me.chkMENUKBN1.Checked = False : Me.chkMENUKBN2.Checked = False : Me.chkMENUKBN3.Checked = False
                            If n.Attributes.GetNamedItem("value").Value.Substring(0, 1).Equals("1") Then
                                '業務
                                Me.chkMENUKBN1.Checked = True
                            End If
                            If n.Attributes.GetNamedItem("value").Value.Substring(1, 1).Equals("1") Then
                                'マスタ登録
                                Me.chkMENUKBN2.Checked = True
                            End If
                            If n.Attributes.GetNamedItem("value").Value.Substring(2, 1).Equals("1") Then
                                '帳票印刷
                                Me.chkMENUKBN3.Checked = True
                            End If

                        Case "PW_KBN"
                            'パスワード区分
                            Me.chkPWKBN1.Checked = False : Me.chkPWKBN2.Checked = False
                            If n.Attributes.GetNamedItem("value").Value.Substring(0, 1).Equals("1") Then
                                'メニュー
                                Me.chkPWKBN1.Checked = True
                            End If
                            If n.Attributes.GetNamedItem("value").Value.Substring(1, 1).Equals("1") Then
                                'CSV出力
                                Me.chkPWKBN2.Checked = True
                            End If
                    End Select
                End If
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' コンフィグ情報更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdConfigInfo() As Boolean
        Try
            '構成ファイルのパスを取得
            Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim appConfigPath As String = System.IO.Directory.GetCurrentDirectory() & "\MENU01.exe.config"

            '構成ファイルをXML DOMに読み込む
            Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            doc.Load(appConfigPath)

            Dim node As System.Xml.XmlNode = doc("configuration")("appSettings")

            '--------------------------------------------------------------------
            '構成ファイルの"key"="Application Name"の値を
            '"value"="MyNewApplication"に変更する
            '--------------------------------------------------------------------
            'ノードを探す
            Dim n As System.Xml.XmlNode
            For Each n In doc("configuration")("appSettings")
                If n.Name = "add" Then
                    '"key"="Application Name"のとき、"value"を変更する
                    Select Case n.Attributes.GetNamedItem("key").Value
                        Case "SYSTEMKBN"
                            'システム区分【0】打席管理【1】その他
                            If Me.rdoSYSTEMKBN0.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "0"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "1"
                            End If
                        Case "ICR700RW_COM"
                            'ﾌﾟﾘﾍﾟｲﾄﾞｶｰﾄﾞRWポート
                            n.Attributes.GetNamedItem("value").Value = Me.cmbPreRW.Text
                        Case "SEAT_COM"
                            '打席通信ポート
                            n.Attributes.GetNamedItem("value").Value = Me.cmbSEAT.Text
                        Case "MULTIDISP_TYPE"
                            'ストーブ表示【0】なし【1】あり
                            If Me.rdoStoveDisabled.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "0"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "1"
                            End If
                        Case "MENU_KBN"
                            UIUtility.SYSTEM.MENUKBN = String.Empty
                            If Me.chkMENUKBN1.Checked Then
                                UIUtility.SYSTEM.MENUKBN &= "1"
                            Else
                                UIUtility.SYSTEM.MENUKBN &= "0"
                            End If
                            If Me.chkMENUKBN2.Checked Then
                                UIUtility.SYSTEM.MENUKBN &= "1"
                            Else
                                UIUtility.SYSTEM.MENUKBN &= "0"
                            End If
                            If Me.chkMENUKBN3.Checked Then
                                UIUtility.SYSTEM.MENUKBN &= "1"
                            Else
                                UIUtility.SYSTEM.MENUKBN &= "0"
                            End If
                            UIUtility.SYSTEM.MENUKBN &= "0"

                            n.Attributes.GetNamedItem("value").Value = UIUtility.SYSTEM.MENUKBN
                        Case "PW_KBN"
                            UIUtility.SYSTEM.PWKBN = String.Empty
                            If Me.chkPWKBN1.Checked Then
                                UIUtility.SYSTEM.PWKBN &= "1"
                            Else
                                UIUtility.SYSTEM.PWKBN &= "0"
                            End If
                            If Me.chkPWKBN2.Checked Then
                                UIUtility.SYSTEM.PWKBN &= "1"
                            Else
                                UIUtility.SYSTEM.PWKBN &= "0"
                            End If
                            n.Attributes.GetNamedItem("value").Value = UIUtility.SYSTEM.PWKBN
                    End Select
                End If
            Next
            '変更された構成ファイルを保存する
            doc.Save(appConfigPath)

            Return True

        Catch ex As Exception
            Throw ex
            Return False
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

            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM SYSMTA")
            sql.Append(" WHERE")
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            'センター名
            Me.txtSHOPNM.Text = resultDt.Rows(0).Item("SHOPNM").ToString()
            '管理者パスワード
            Me.txtADMNPW.Text = resultDt.Rows(0).Item("ADMNPW").ToString()
            'SEパスワード
            Me.txtSEPW.Text = resultDt.Rows(0).Item("SEPW").ToString()
            '球数税率
            Me.txtTAMATAX.Text = resultDt.Rows(0).Item("TAMATAX").ToString()
            '税率
            Me.txtTAX.Text = resultDt.Rows(0).Item("TAX").ToString()
            '税区分
            Me.cmbTAXKBN.SelectedIndex = CType(resultDt.Rows(0).Item("TAXKBN").ToString(), Integer)
            '税端数区分
            Me.cmbTAXHASUKBN.SelectedIndex = CType(resultDt.Rows(0).Item("TAXHASUKBN").ToString(), Integer)
            'パスワード区分
            If CType(resultDt.Rows(0).Item("PASSKBN").ToString(), Integer).Equals(0) Then
                '【ランダム】
                rdoPASSKBN0.Checked = True
            Else
                '【固定】
                rdoPASSKBN0.Checked = False
            End If
            'フロア数
            Me.txtFLRSU.Text = resultDt.Rows(0).Item("FLRSU").ToString()
            '全打席数
            Me.txtSEATSU.Text = resultDt.Rows(0).Item("SEATSU").ToString()
            '1F最終打席№
            Me.txtLSTNO1F.Text = resultDt.Rows(0).Item("LSTNO1F").ToString()
            If Not Me.txtLSTNO1F.Text.Equals("0") Then
                Me.lblLSTNO1F.Visible = True
                Me.txtLSTNO1F.Visible = True
            End If
            '2F最終打席№
            Me.txtLSTNO2F.Text = resultDt.Rows(0).Item("LSTNO2F").ToString()
            If Not Me.txtLSTNO2F.Text.Equals("0") Then
                Me.lblLSTNO2F.Visible = True
                Me.txtLSTNO2F.Visible = True
            End If
            '3F最終打席№
            Me.txtLSTNO3F.Text = resultDt.Rows(0).Item("LSTNO3F").ToString()
            If Not Me.txtLSTNO3F.Text.Equals("0") Then
                Me.lblLSTNO3F.Visible = True
                Me.txtLSTNO3F.Visible = True
            End If
            'カレンダー作成最終日付
            If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("CALLSTDT").ToString()) Then
                Me.txtCALLSTDT.Text = resultDt.Rows(0).Item("CALLSTDT").ToString().Substring(0, 4) & "/" _
                                    & resultDt.Rows(0).Item("CALLSTDT").ToString().Substring(4, 2) & "/" _
                                    & resultDt.Rows(0).Item("CALLSTDT").ToString().Substring(6, 2)
            End If

            '会員期限調整日数
            Me.cmbCALLMT.Text = resultDt.Rows(0).Item("CALLMT").ToString()
            '左右兼用打席1
            Me.txtLRMULTI01.Text = resultDt.Rows(0).Item("LRMULTI01").ToString()
            '左右兼用打席2
            Me.txtLRMULTI02.Text = resultDt.Rows(0).Item("LRMULTI02").ToString()
            '左右兼用打席3
            Me.txtLRMULTI03.Text = resultDt.Rows(0).Item("LRMULTI03").ToString()
            '左右兼用打席4
            Me.txtLRMULTI04.Text = resultDt.Rows(0).Item("LRMULTI04").ToString()
            '左右兼用打席5
            Me.txtLRMULTI05.Text = resultDt.Rows(0).Item("LRMULTI05").ToString()
            '担当者確認
            Me.cmbTANCHKFLG.SelectedIndex = CType(resultDt.Rows(0).Item("TANCHKFLG").ToString(), Integer)
            '打席情報画面フラグ
            Me.cmbDISPMULTI.SelectedIndex = CType(resultDt.Rows(0).Item("DISPMULTI").ToString(), Integer)
            '月間来場回数クリア月
            Me.txtCLRENTMCNT.Text = resultDt.Rows(0).Item("CLRENTMCNT").ToString()
            'OSシャットダウン
            Me.cmbOSDOWNFLG.SelectedIndex = CType(resultDt.Rows(0).Item("OSDOWNFLG").ToString(), Integer)
            'レシート印刷フラグ
            Me.cmbRECEIPTFLG.SelectedIndex = CType(resultDt.Rows(0).Item("RECEIPTFLG").ToString(), Integer)
            'ﾁｪｯｸｱｳﾄﾎﾟｲﾝﾄ
            Me.txtJrHH.Text = resultDt.Rows(0).Item("CHKPOINT").ToString.PadLeft(4, "0"c).Substring(0, 2)
            Me.txtJrMM.Text = resultDt.Rows(0).Item("CHKPOINT").ToString.PadLeft(4, "0"c).Substring(2, 2)
            '入金限度額
            Me.txtZANMAX.Text = CType(resultDt.Rows(0).Item("ZANMAX").ToString(), Integer).ToString("#,##0")
            'カー残金有効期限
            Me.txtCARDLIMIT.Text = resultDt.Rows(0).Item("CARDLIMIT").ToString()
            '商品引落しﾌﾟﾚﾐｱﾑ精算区分
            Me.cmbHINPREMPAYKBN.SelectedIndex = CType(resultDt.Rows(0).Item("HINPREMPAYKBN").ToString(), Integer)
            'ｶｰﾄﾞ支出ﾌﾟﾚﾐｱﾑ精算区分
            Me.cmbSHTPREMPAYKBN.SelectedIndex = CType(resultDt.Rows(0).Item("SHTPREMPAYKBN").ToString(), Integer)
            '入金残高有効期限
            Me.txtPREMLIMIT.Text = resultDt.Rows(0).Item("PREMLIMIT").ToString()

            '最新の更新日時を取得
            _dtUPDDTM = DirectCast(resultDt.Rows(0).Item("UPDDTM"), DateTime)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

    ''' <summary>
    ''' 登録データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckRegister(ByRef Msg As String) As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtSEATSU.Text) Then
                Msg = "全打席数を入力して下さい。"
                Me.txtSEATSU.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtLSTNO1F.Text) Then
                Msg = "1F最終打席№を入力して下さい。"
                Me.txtLSTNO1F.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtLSTNO2F.Text) Then
                Msg = "2F最終打席№を入力して下さい。"
                Me.txtLSTNO1F.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtLSTNO3F.Text) Then
                Msg = "3F最終打席№を入力して下さい。"
                Me.txtLSTNO1F.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtJrHH.Text) Then
                Msg = "打ち放題終了時刻を入力して下さい。"
                Me.txtJrHH.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtJrMM.Text) Then
                Msg = "打ち放題終了時刻を入力して下さい。"
                Me.txtJrMM.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtZANMAX.Text) Then
                Msg = "入金限度額をを入力して下さい。"
                Me.txtZANMAX.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(Me.txtCARDLIMIT.Text) Then
                Msg = "カード残金有効期限をを入力して下さい。"
                Me.txtCARDLIMIT.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdRegister() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            'トランザクション開始
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE SYSMTA SET ")
            'センター名
            sql.Append("SHOPNM = '" & Me.txtSHOPNM.Text & "',")
            '管理者パスワード
            sql.Append("ADMNPW = '" & Me.txtADMNPW.Text & "',")
            'SEパスワード
            sql.Append("SEPW = '" & Me.txtSEPW.Text & "',")
            '球数税率
            sql.Append("TAMATAX = " & Me.txtTAMATAX.Text & ",")
            '税率
            sql.Append("TAX = " & Me.txtTAX.Text & ",")
            '税区分
            sql.Append("TAXKBN = " & Me.cmbTAXKBN.SelectedIndex & ",")
            '税端数区分
            sql.Append("TAXHASUKBN = " & Me.cmbTAXHASUKBN.SelectedIndex & ",")
            'フロア数
            sql.Append("FLRSU = " & Me.txtFLRSU.Text & ",")
            '全打席数
            sql.Append("SEATSU = " & Me.txtSEATSU.Text & ",")
            '1F最終打席
            sql.Append("LSTNO1F = " & Me.txtLSTNO1F.Text & ",")
            '2F最終打席
            sql.Append("LSTNO2F = " & Me.txtLSTNO2F.Text & ",")
            '3F最終打席
            sql.Append("LSTNO3F = " & Me.txtLSTNO3F.Text & ",")
            'パスワード区分
            If rdoPASSKBN0.Checked Then
                '【ランダム】
                sql.Append("PASSKBN = 0,")
            Else
                '【固定】
                sql.Append("PASSKBN = 1,")
            End If
            '担当者フラグ
            sql.Append("TANCHKFLG = " & Me.cmbTANCHKFLG.SelectedIndex & ",")
            '会員期限調整日数
            sql.Append("CALLMT = " & Me.cmbCALLMT.Text & ",")
            '左右兼用打席1
            sql.Append("LRMULTI01 = " & Me.txtLRMULTI01.Text & ",")
            '左右兼用打席2
            sql.Append("LRMULTI02 = " & Me.txtLRMULTI02.Text & ",")
            '左右兼用打席3
            sql.Append("LRMULTI03 = " & Me.txtLRMULTI03.Text & ",")
            '左右兼用打席4
            sql.Append("LRMULTI04 = " & Me.txtLRMULTI04.Text & ",")
            '左右兼用打席5
            sql.Append("LRMULTI05 = " & Me.txtLRMULTI05.Text & ",")
            '打席情報画面フラグ
            sql.Append("DISPMULTI = " & Me.cmbDISPMULTI.SelectedIndex & ",")
            'OSシャットダウン
            sql.Append("OSDOWNFLG = " & Me.cmbOSDOWNFLG.SelectedIndex & ",")
            'レシート印刷フラグ
            sql.Append("RECEIPTFLG = " & Me.cmbRECEIPTFLG.SelectedIndex & ",")
            '打ち放題終了時刻
            sql.Append("CHKPOINT = " & Me.txtJrHH.Text.PadLeft(2, "0"c) & Me.txtJrMM.Text.PadLeft(2, "0"c) & ",")
            '入金限度額
            sql.Append("ZANMAX = " & CType(Me.txtZANMAX.Text, Integer) & ",")
            'カード残金有効期限
            sql.Append("CARDLIMIT = " & CType(Me.txtCARDLIMIT.Text, Integer) & ",")
            '商品引落しﾌﾟﾚﾐｱﾑ精算区分
            sql.Append("HINPREMPAYKBN = " & Me.cmbHINPREMPAYKBN.SelectedIndex & ",")
            'ｶｰﾄﾞ支出ﾌﾟﾚﾐｱﾑ精算区分
            sql.Append("SHTPREMPAYKBN = " & Me.cmbSHTPREMPAYKBN.SelectedIndex & ",")
            '入金残高有効期限
            sql.Append("PREMLIMIT = " & CType(Me.txtPREMLIMIT.Text, Integer) & ",")

            sql.Append("UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" SHOPNO = '" & UIUtility.SYSTEM.SHOPNO & "'")

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            'システム情報更新
            If Not UpdConfigInfo() Then
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
