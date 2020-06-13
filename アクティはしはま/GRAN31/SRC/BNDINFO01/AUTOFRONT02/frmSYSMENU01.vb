Imports System.IO.Ports
Imports System.Threading
Imports Techno.DataBase

Public Class frmSYSMENU01

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
    Private _dcICR700 As New TECHNO.DeviceControls.ICR700Control
    ''' <summary>
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcMCH3000 As New TECHNO.DeviceControls.MCH3000Control
    ''' <summary>
    ''' ビルバリコントロール
    ''' </summary>
    ''' <remarks></remarks>
    Private _dcAD1 As New Techno.DeviceControls.SC1708Control
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
    ''' MCH3000ユニット制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MCH3000 As TECHNO.DeviceControls.MCH3000Control
        Set(ByVal value As TECHNO.DeviceControls.MCH3000Control)
            _dcMCH3000 = value
        End Set
    End Property

    ''' <summary>
    ''' ビルバリ制御
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AD1 As Techno.DeviceControls.SC1708Control
        Get
            Return _dcAD1
        End Get
        Set(ByVal value As Techno.DeviceControls.SC1708Control)
            _dcAD1 = value
        End Set
    End Property

    ''' <summary>
    ''' 電源断が押されたかどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PowerOff As Boolean
        Get
            Return _blnPowerOff
        End Get
    End Property
    Private _blnPowerOff As Boolean = False

    ''' <summary>
    ''' 再起動するかどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ReStart As Boolean
        Get
            Return _blnReStart
        End Get
    End Property
    Private _blnReStart As Boolean = False

#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSYSMENU01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_Shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSYSMENU01_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.Refresh()

            '画面初期設定
            Init()

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
    Private Sub frmSYSMENU01_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' フォーム_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSYSMENU01_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            '各キー入力時のイベント
            Select Case e.KeyCode
                Case Keys.Escape
                    Me.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' システム区分ﾁｪｯｸﾎﾞｯｸｽ_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdoSYSTEMKBN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoSYSTEMKBN0.CheckedChanged, rdoSYSTEMKBN1.CheckedChanged
        Try
            If Me.rdoSYSTEMKBN0.Checked Then
                '【ATM】
                Me.rdoSYSTEMKBN0.ForeColor = Color.Yellow
                Me.rdoSYSTEMKBN1.ForeColor = Color.Black
                Me.rdoSYSTEMKBN99.ForeColor = Color.Black
                Me.chkFunction1.Enabled = True
                Me.chkFunction2.Enabled = True
                Me.chkFunction3.Enabled = True
                Me.chkFunction4.Enabled = True

                Me.chkEjectStopMode.Enabled = True
                Me.chkEjectStopMode.Checked = True
            ElseIf Me.rdoSYSTEMKBN1.Checked Then
                '【自動受付機】
                Me.rdoSYSTEMKBN1.ForeColor = Color.Yellow
                Me.rdoSYSTEMKBN0.ForeColor = Color.Black
                Me.rdoSYSTEMKBN99.ForeColor = Color.Black
                Me.chkFunction1.Enabled = False
                Me.chkFunction2.Enabled = False
                Me.chkFunction3.Enabled = False
                Me.chkFunction4.Enabled = False

                Me.chkEjectStopMode.Enabled = False
                Me.chkEjectStopMode.Checked = False
            Else
                '【営業中止】
                Me.rdoSYSTEMKBN99.ForeColor = Color.Yellow
                Me.rdoSYSTEMKBN1.ForeColor = Color.Black
                Me.rdoSYSTEMKBN0.ForeColor = Color.Black
                Me.chkFunction1.Enabled = False
                Me.chkFunction2.Enabled = False
                Me.chkFunction3.Enabled = False
                Me.chkFunction4.Enabled = False

                Me.chkEjectStopMode.Enabled = True
                Me.chkEjectStopMode.Checked = True
            End If

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
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' MCH3000ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMCH3000_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMCH3000.Click
        Try
            Using frm As New frmHAKKEN01
                frm.MCH3000 = _dcMCH3000
                frm.ShowDialog()
                _dcMCH3000 = frm.MCH3000
            End Using

            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' ビルバリ情報ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAD1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAD1.Click
        Try
            Using frm As New frmAD1
                frm.AD1 = _dcAD1
                frm.ShowDialog()
                _dcAD1 = frm.AD1
            End Using

            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 電源断ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            _blnPowerOff = True

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' コンフィグ保存ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdConfig.Click
        Try
            '' *** STA ADD 2018/11/19 TERAYAMA
            'Dim table = CommonSettings.SlotPerTables()
            'Dim per1 = table(Me.cmbSlotPer1.SelectedIndex)
            'Dim per2 = table(Me.cmbSlotPer2.SelectedIndex)
            'Dim per3 = table(Me.cmbSlotPer3.SelectedIndex)
            'Dim per4 = table(Me.cmbSlotPer4.SelectedIndex)
            'If (per1 + per2 + per3 + per4 > CommonSettings.SlotMaxPer) Then
            '    Using frm As New frmMSGBOX01("スロット設定の合計値が100%を超えています。", 2)
            '        frm.ShowDialog()
            '    End Using
            '    Exit Sub
            'End If
            '' *** END ADD 2018/11/19 TERAYAMA

            If UpdConfigInfo() Then
                Using frm As New frmMSGBOX01("正常に更新されました。" & vbCrLf & "システムを再起動します。", 0)
                    frm.ShowDialog()
                End Using
                _blnReStart = True
                Me.btnBack.PerformClick()
            Else
                Using frm As New frmMSGBOX01("通信ポート設定の更新に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 入金履歴_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNKNTRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNKNTRN.Click
        ' *** ADD TERAYAMA 2018/08/10
        Try
            Using frm As New frmNKNTRN(_iDatabase)
                frm.ShowDialog()
            End Using
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
            ' *** STA ADD 2018/09/26 TERAYAMA リスト表示バグ修正
            Dim tmp1 = Me.cmbICR700Port.SelectedIndex
            Dim tmp2 = Me.cmbMCH3000Port.SelectedIndex
            Dim tmp4 = Me.cmbAD1Port.SelectedIndex
            Me.cmbICR700Port.Items.Clear()
            Me.cmbMCH3000Port.Items.Clear()
            Me.cmbAD1Port.Items.Clear()
            ' *** END ADD 2018/09/26 TERAYAMA リスト表示バグ修正

            For Each sp As String In My.Computer.Ports.SerialPortNames
                Me.cmbICR700Port.Items.Add(sp)
                Me.cmbMCH3000Port.Items.Add(sp)
                Me.cmbAD1Port.Items.Add(sp)
            Next

            ' *** STA ADD 2018/09/26 TERAYAMA リスト表示バグ修正
            Me.cmbICR700Port.SelectedIndex = tmp1
            Me.cmbMCH3000Port.SelectedIndex = tmp2
            Me.cmbAD1Port.SelectedIndex = tmp4
            ' *** END ADD 2018/09/26 TERAYAMA リスト表示バグ修正

            ' カード搬送ユニットの情報取得
            If _dcMCH3000.IsOpen Then
                Me.btnMCH3000.BackColor = Color.ForestGreen
            Else
                Me.btnMCH3000.BackColor = Color.Red
            End If

            ''ビルバリ情報取得
            'If _dcAD1.Connect Then
            '    CheckAD1()
            'Else
            '    Me.btnAD1.BackColor = Color.Red
            'End If

            ' *** STA ADD 2018/09/26 TERAYAMA リスト表示バグ修正
            Dim v1 = Me.cmbSlotPer1.SelectedIndex
            Dim v2 = Me.cmbSlotPer2.SelectedIndex
            Dim v3 = Me.cmbSlotPer3.SelectedIndex
            Dim v4 = Me.cmbSlotPer4.SelectedIndex
            Dim v5 = Me.cmbSlotPoint1.SelectedIndex
            Dim v6 = Me.cmbSlotPoint2.SelectedIndex
            Dim v7 = Me.cmbSlotPoint3.SelectedIndex
            Dim v8 = Me.cmbSlotPoint4.SelectedIndex
            Me.cmbSlotPer1.Items.Clear()
            Me.cmbSlotPer2.Items.Clear()
            Me.cmbSlotPer3.Items.Clear()
            Me.cmbSlotPer4.Items.Clear()
            Me.cmbSlotPoint1.Items.Clear()
            Me.cmbSlotPoint2.Items.Clear()
            Me.cmbSlotPoint3.Items.Clear()
            Me.cmbSlotPoint4.Items.Clear()
            ' *** END ADD 2018/09/26 TERAYAMA リスト表示バグ修正

            ' 発券設定の取得
            If Not GetKBMAST() Then
                Throw New Exception
            End If

            ' *** ADD 2018/08/09 TERAYAMA
            ' ﾐﾆｹﾞｰﾑ設定
            Dim max_per = CommonSettings.SlotMaxPer
            Dim intPers = CommonSettings.SlotPerTables
            Dim intPoints = CommonSettings.SlotPointTables
            For Each per In intPers

                Dim v As Double = per / max_per * 100

                Dim str As String
                If v < 1.0 Then
                    str = v.ToString("0.00") & "％"
                Else
                    str = v.ToString("0") & "％"
                End If

                Me.cmbSlotPer1.Items.Add(str)
                Me.cmbSlotPer2.Items.Add(str)
                Me.cmbSlotPer3.Items.Add(str)
                Me.cmbSlotPer4.Items.Add(str)
            Next
            For Each point In intPoints
                Me.cmbSlotPoint1.Items.Add(point)
                Me.cmbSlotPoint2.Items.Add(point)
                Me.cmbSlotPoint3.Items.Add(point)
                Me.cmbSlotPoint4.Items.Add(point)
            Next

            ' *** STA ADD 2018/09/26 TERAYAMA リスト表示バグ修正
            Me.cmbSlotPer1.SelectedIndex = v1
            Me.cmbSlotPer2.SelectedIndex = v2
            Me.cmbSlotPer3.SelectedIndex = v3
            Me.cmbSlotPer4.SelectedIndex = v4
            Me.cmbSlotPoint1.SelectedIndex = v5
            Me.cmbSlotPoint2.SelectedIndex = v6
            Me.cmbSlotPoint3.SelectedIndex = v7
            Me.cmbSlotPoint4.SelectedIndex = v8
            ' *** END ADD 2018/09/26 TERAYAMA リスト表示バグ修正

            picSlot1.Image = Images.PicSLOT1
            picSlot2.Image = Images.PicSLOT2
            picSlot3.Image = Images.PicSLOT3
            picSlot4.Image = Images.PicSLOT4

            'コンフィグ情報表示
            SetConfigInfo()

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
            Dim appConfigPath As String = System.IO.Directory.GetCurrentDirectory() & "\AUTOFRONT02.exe.config"

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
                        Case "ICR700RW_COM"
                            'ICRWポート
                            Me.cmbICR700Port.Text = n.Attributes.GetNamedItem("value").Value
                        Case "MCH3000_COM"
                            'カード搬送ユニットポート
                            Me.cmbMCH3000Port.Text = n.Attributes.GetNamedItem("value").Value
                        Case "AD1_COM"
                            'ビルバリポート
                            Me.cmbAD1Port.Text = n.Attributes.GetNamedItem("value").Value
                        Case "SYSTEMKBN"
                            'システム区分

                            If n.Attributes.GetNamedItem("value").Value.Equals("0") Then
                                'ATM
                                Me.rdoSYSTEMKBN0.Checked = True
                            ElseIf n.Attributes.GetNamedItem("value").Value.Equals("1") Then
                                '自動受付機
                                Me.rdoSYSTEMKBN1.Checked = True
                            Else
                                '営業中止
                                Me.rdoSYSTEMKBN99.Checked = True
                            End If
                        Case "FUNCTION"
                            '機能
                            If n.Attributes.GetNamedItem("value").Value.Substring(0, 1).Equals("1") Then
                                'ﾁｪｯｸｲﾝ
                                Me.chkFunction1.Checked = True
                            End If
                            If n.Attributes.GetNamedItem("value").Value.Substring(1, 1).Equals("1") Then
                                'ﾁｪｯｸｱｳﾄ
                                Me.chkFunction2.Checked = True
                            End If
                            If n.Attributes.GetNamedItem("value").Value.Substring(2, 1).Equals("1") Then
                                '入金
                                Me.chkFunction3.Checked = True
                            End If
                            If n.Attributes.GetNamedItem("value").Value.Substring(3, 1).Equals("1") Then
                                'ｶｰﾄﾞ発券
                                Me.chkFunction4.Checked = True
                            End If
                        Case "RESULT_EJECTSTOP_MODE"
                            ' *** ADD TERAYAMA 2018/08/07
                            Me.chkEjectStopMode.Checked = n.Attributes.GetNamedItem("value").Value.Equals("1")
                        Case "SLOT_SETTING"
                            ' *** ADD TERAYAMA 2018/08/09
                            Dim settings = n.Attributes.GetNamedItem("value").Value.Split(","c)
                            Me.cmbSlotPer1.SelectedIndex = CInt(settings(0))
                            Me.cmbSlotPoint1.SelectedIndex = CInt(settings(1))
                            Me.cmbSlotPer2.SelectedIndex = CInt(settings(2))
                            Me.cmbSlotPoint2.SelectedIndex = CInt(settings(3))
                            Me.cmbSlotPer3.SelectedIndex = CInt(settings(4))
                            Me.cmbSlotPoint3.SelectedIndex = CInt(settings(5))
                            Me.cmbSlotPer4.SelectedIndex = CInt(settings(6))
                            Me.cmbSlotPoint4.SelectedIndex = CInt(settings(7))
                        Case "MINIGAME"
                            ' *** ADD TERAYAMA 2018/08/09
                            Me.chkMinigame.Checked = n.Attributes.GetNamedItem("value").Value.Equals("1")
                        Case "SLOT_CORRECTION_ENABLED"
                            ' *** ADD TERAYAMA 2018/08/17
                            Me.chkSLOT_CORRECTION_ENABLED.Checked = n.Attributes.GetNamedItem("value").Value.Equals("1")
                        Case "HAKKEN_NKBNO"
                            Me.cmbKBMAST.SelectedIndex = CInt(n.Attributes.GetNamedItem("value").Value) - 1
                        Case "ZANKIN_CLEARE"
                            Me.chkZANCLEARE.Checked = False
                            If n.Attributes.GetNamedItem("value").Value.Substring(0, 1).Equals("1") Then
                                Me.chkZANCLEARE.Checked = True
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
            Dim appConfigPath As String = System.IO.Directory.GetCurrentDirectory() & "\AUTOFRONT02.exe.config"

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
                        Case "ICR700RW_COM"
                            'ICRWポート
                            n.Attributes.GetNamedItem("value").Value = Me.cmbICR700Port.Text
                        Case "MCH3000_COM"
                            'カード搬送ユニットポート
                            n.Attributes.GetNamedItem("value").Value = Me.cmbMCH3000Port.Text
                        Case "AD1_COM"
                            'ビルバリポート
                            n.Attributes.GetNamedItem("value").Value = Me.cmbAD1Port.Text
                        Case "SYSTEMKBN"
                            'システム区分

                            If Me.rdoSYSTEMKBN0.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "0"
                            ElseIf Me.rdoSYSTEMKBN1.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "1"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "99"
                            End If
                        Case "FUNCTION"
                            Dim strFunction As String = String.Empty

                            If Me.chkFunction1.Checked Then
                                strFunction &= "1"
                            Else
                                strFunction &= "0"
                            End If
                            If Me.chkFunction2.Checked Then
                                strFunction &= "1"
                            Else
                                strFunction &= "0"
                            End If
                            If Me.chkFunction3.Checked Then
                                strFunction &= "1"
                            Else
                                strFunction &= "0"
                            End If
                            If Me.chkFunction4.Checked Then
                                strFunction &= "1"
                            Else
                                strFunction &= "0"
                            End If

                            n.Attributes.GetNamedItem("value").Value = strFunction
                        Case "RESULT_EJECTSTOP_MODE"
                            ' *** ADD TERAYAMA 2018/08/07
                            If Me.chkEjectStopMode.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "1"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "0"
                            End If
                        Case "SLOT_SETTING"
                            ' *** ADD TERAYAMA 2018/08/09
                            Dim per1 = Me.cmbSlotPer1.SelectedIndex
                            Dim per2 = Me.cmbSlotPer2.SelectedIndex
                            Dim per3 = Me.cmbSlotPer3.SelectedIndex
                            Dim per4 = Me.cmbSlotPer4.SelectedIndex
                            Dim point1 = Me.cmbSlotPoint1.SelectedIndex
                            Dim point2 = Me.cmbSlotPoint2.SelectedIndex
                            Dim point3 = Me.cmbSlotPoint3.SelectedIndex
                            Dim point4 = Me.cmbSlotPoint4.SelectedIndex
                            Dim settings = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", per1, point1, per2, point2, per3, point3, per4, point4)
                            n.Attributes.GetNamedItem("value").Value = settings
                        Case "MINIGAME"
                            ' *** ADD TERAYAMA 2018/08/09
                            If Me.chkMinigame.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "1"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "0"
                            End If
                        Case "SLOT_CORRECTION_ENABLED"
                            ' *** ADD TERAYAMA 2018/08/17
                            If Me.chkSLOT_CORRECTION_ENABLED.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "1"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "0"
                            End If
                        Case "HAKKEN_NKBNO"
                            ' *** ADD TERAYAMA 2019/03/16
                            n.Attributes.GetNamedItem("value").Value = CStr(Me.cmbKBMAST.SelectedIndex + 1)
                        Case "ZANKIN_CLEARE"
                            If Me.chkZANCLEARE.Checked Then
                                n.Attributes.GetNamedItem("value").Value = "1"
                            Else
                                n.Attributes.GetNamedItem("value").Value = "0"
                            End If
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
    ''' ビルバリ状態確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckAD1()
        Try
            'Me.btnAD1.BackColor = Color.ForestGreen

            '_dcAD1.Status3_Command()

            ''装置ステータス1
            'Select Case _dcAD1.DeviceStatus1
            '    Case 1
            '        'パワーオン
            '    Case 2
            '        'リセット動作中
            '    Case 4
            '        '待機中
            '    Case 8
            '        '紙幣挿入待ち
            '    Case 16
            '        '入金動作中
            '    Case 32
            '        '出金動作中
            '    Case 64
            '        '入出金口紙幣受取待ち
            '    Case 128
            '        'クリーニング動作中
            '    Case 0
            '        'なし
            '    Case Else
            '        '不明
            '        Me.btnAD1.BackColor = Color.Red
            'End Select

            ''装置ステータス2
            'Select Case _dcAD1.DeviceStatus2
            '    Case 1
            '        'アラーム発生中
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 2
            '        '払出停止動作中
            '    Case 4
            '        '計数動作中
            '    Case 8
            '        '計数待機中
            '    Case 16
            '        '計数払出動作中
            '    Case 32
            '        '計数リジェクト動作中
            '    Case 64
            '        '自己診断モード待機中
            '    Case 128
            '        '自己診断モード動作中
            '    Case 0
            '        'なし
            '    Case Else
            '        '不明
            '        Me.btnAD1.BackColor = Color.Red
            'End Select

            ''紙幣収納状態1
            'Select Case _dcAD1.BillState1
            '    Case 1
            '        '還流スタッカ1満杯検出
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 2
            '        '還流スタッカ2満杯検出
            '    Case 4
            '        '還流スタッカ3満杯検出
            '    Case 8
            '        '還流スタッカベース満杯検出
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 16
            '        '還流スタッカ1ニアエンド検出
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 32
            '        '還流スタッカ2ニアエンド検出
            '    Case 64
            '        '還流スタッカ3ニアエンド検出
            '    Case 128
            '        '還流スタッカベースニアエンド検出
            '    Case 0
            '        'なし
            '    Case Else
            '        '不明
            '        Me.btnAD1.BackColor = Color.Red
            'End Select


            ''紙幣収納状態2
            'Select Case _dcAD1.BillState2
            '    Case 1
            '        'リジェクトボックス満杯検出
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 2
            '        '扉開検出
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 4
            '        'ユニット引出し検出
            '        Me.btnAD1.BackColor = Color.Red
            '    Case 128
            '        '模擬券モード
            '    Case 0
            '        'なし
            '    Case Else
            '        '不明
            'End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
            sql.Append(" WHERE NKBNO <= " & UIUtility.SYSTEM.MAXNKBNO)
            sql.Append(" ORDER BY KBMAST ")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

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

#End Region








    ''' <summary>
    ''' ICR700リセット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRwReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRwReset.Click
        Dim blnErr As Boolean = False
        Try
            '【ICR700リーダライター接続】'
            If _dcICR700.IsOpen Then
                _dcICR700.Close()
            End If
            Do
                If Not _dcICR700.Open(UIUtility.SYSTEM.ICR700COM) Then
                    blnErr = True
                    Exit Do
                End If
                Dim keys = New Byte() {&H36, &H37, &H37, &H37, &H37, &H32}
                If Not _dcICR700.AuthKeySet(keys, False, 2, 4) Then
                    blnErr = True
                    Exit Do
                End If
                If Not _dcICR700.Cancel Then
                    blnErr = True
                    Exit Do
                End If
                Exit Do
            Loop

            If blnErr Then
                Using frm As New frmMSGBOX01("ICリーダライターとの接続に失敗しました。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
