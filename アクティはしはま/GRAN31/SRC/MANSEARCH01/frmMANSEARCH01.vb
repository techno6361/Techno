Imports TECHNO.DataBase

Public Class frmMANSEARCH01

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 検索スクール生番号取得
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetSCLMANNO As String
        Get
            Return _strSCLMANNO
        End Get
    End Property
    Private _strSCLMANNO As String = String.Empty


#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "スクール生検索"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "スクール生検索"

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
    Private Sub frmMANSEARCH01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' スクール生グリッド_DoubleClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvMANMST_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvMANMST.DoubleClick
        Try
            If Me.dgvMANMST.RowCount = 0 Then
                Exit Sub
            End If

            Dim intRow As Integer

            If Me.dgvMANMST.Rows.Count <> 0 Then
                intRow = Me.dgvMANMST.CurrentRow.Index

                _strSCLMANNO = Me.dgvMANMST.getValue("SCLMANNO", intRow).ToString
            End If

            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' スクール番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSCLMANNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaSCLMANNO.Validated, txtEndSCLMANNO.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If Not String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = txtBox.Text.PadLeft(6, "0"c)
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
    Private Sub txtBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtStaSCLMANNO.KeyPress, txtEndSCLMANNO.KeyPress, txtMANTELNO.KeyPress, txtMANKTELNO.KeyPress
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
    ''' テキストボックス_Enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtMANNM.Enter, txtMANKN.Enter, txtMANADDRESS.Enter, txtMANTELNO.Enter, txtMANKTELNO.Enter
        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.Text = txtBox.Text.Replace("/", String.Empty)
            txtBox.Text = txtBox.Text.Replace("-", String.Empty)

            txtBox.SelectAll()

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
    Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMANNM.MouseDown, txtMANKN.MouseDown, txtMANADDRESS.MouseDown, txtMANTELNO.MouseDown, txtMANKTELNO.MouseDown

        Try
            Dim txtBox As TextBox

            txtBox = CType(sender, TextBox)

            txtBox.SelectAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' F3検索ボタン押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub func3()
        Try


            'スクールデータ取得
            GetMANMST()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

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
            '検索
            Me.tspFunc3.Enabled = True
            Me.tspFunc3.Text = "検索" & vbCr & "F3"
            '印刷
            Me.tspFunc8.Enabled = False
            Me.tspFunc8.Visible = False
            '削除
            Me.tspFunc9.Enabled = False
            Me.tspFunc9.Visible = False
            '画面印刷
            Me.tspFunc10.Enabled = False
            Me.tspFunc10.Visible = False
            'クリア
            Me.tspFunc11.Enabled = False
            Me.tspFunc11.Visible = False
            '登録
            Me.tspFunc12.Enabled = False
            Me.tspFunc12.Visible = False

            'スクール生番号
            Me.txtStaSCLMANNO.Text = String.Empty
            Me.txtEndSCLMANNO.Text = String.Empty
            '種別
            Me.chkSCLKBN0.Checked = False
            Me.chkSCLKBN1.Checked = False
            Me.chkSCLKBN8.Checked = False
            Me.chkSCLKBN9.Checked = False
            '氏名
            Me.txtMANNM.Text = String.Empty
            '氏名ｶﾅ
            Me.txtMANKN.Text = String.Empty
            '住所
            Me.txtMANADDRESS.Text = String.Empty
            '電話番号
            Me.txtMANTELNO.Text = String.Empty
            '携帯電話番号
            Me.txtMANKTELNO.Text = String.Empty


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' スクール情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetMANMST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Append(" SELECT ")
            sql.Append(" A.SCLMANNO")                                   'スクール生番号
            sql.Append(",A.SCLKBN")                                     '種別
            sql.Append(",A.MANNM")                                      '氏名
            sql.Append(",A.MANKN")                                      '氏名ｶﾅ
            sql.Append(",A.MANSEX")                                     '性別
            sql.Append(",A.MANZIP")                                     '郵便番号
            sql.Append(",(A.MANADDA || A.MANADDB) AS MANADDRESS")       '住所
            sql.Append(",A.MANTELNO")                                   '電話番号
            sql.Append(",A.MANKTELNO")                                  '携帯電話番号
            sql.Append(" FROM MANMST AS A")
            sql.Append(" WHERE 1=1")
            'スクール生番号
            If Not String.IsNullOrEmpty(Me.txtStaSCLMANNO.Text) Then
                sql.Append(" AND A.SCLMANNO >= '" & Me.txtStaSCLMANNO.Text & "'")
            End If
            If Not String.IsNullOrEmpty(Me.txtEndSCLMANNO.Text) Then
                sql.Append(" AND A.SCLMANNO <= '" & Me.txtEndSCLMANNO.Text & "'")
            End If
            '種別
            If Me.chkSCLKBN0.Checked Or Me.chkSCLKBN1.Checked Or Me.chkSCLKBN8.Checked Or Me.chkSCLKBN9.Checked Then
                sql.Append(" AND (SCLKBN = '99'")
                If Me.chkSCLKBN0.Checked Then
                    '本科生
                    sql.Append(" OR SCLKBN = '0'")
                End If
                If Me.chkSCLKBN1.Checked Then
                    '体験
                    sql.Append(" OR SCLKBN = '1'")
                End If
                If Me.chkSCLKBN8.Checked Then
                    '休会
                    sql.Append(" OR SCLKBN = '8'")
                End If
                If Me.chkSCLKBN9.Checked Then
                    '退会
                    sql.Append(" OR SCLKBN = '9'")
                End If

                sql.Append(")")
            End If
            '氏名
            If Not String.IsNullOrEmpty(Me.txtMANNM.Text) Then
                sql.Append(" AND A.MANNM LIKE '%" & Me.txtMANNM.Text & "%'")
            End If
            '氏名ｶﾅ
            If Not String.IsNullOrEmpty(Me.txtMANKN.Text) Then
                sql.Append(" AND A.MANKN LIKE '%" & Me.txtMANKN.Text & "%'")
            End If
            '性別
            If Me.cmbMANSEX.SelectedIndex > 0 Then
                sql.Append(" AND A.MANSEX = '" & Me.cmbMANSEX.SelectedIndex & "'")
            End If
            '住所
            If Not String.IsNullOrEmpty(Me.txtMANADDRESS.Text) Then
                sql.Append(" AND (A.MANADDA || A.MANADDB) LIKE '%" & Me.txtMANADDRESS.Text & "%'")
            End If
            '電話番号
            If Not String.IsNullOrEmpty(Me.txtMANTELNO.Text) Then
                sql.Append(" AND A.MANTELNO LIKE '%" & Me.txtMANTELNO.Text & "%'")
            End If
            '携帯電話番号
            If Not String.IsNullOrEmpty(Me.txtMANKTELNO.Text) Then
                sql.Append(" AND A.MANKTELNO LIKE '%" & Me.txtMANKTELNO.Text & "%'")
            End If

            sql.Append(" ORDER BY A.SCLMANNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Me.dgvMANMST.RowCount = resultDt.Rows.Count
            Me.lblMsg.Visible = False
            If resultDt.Rows.Count.Equals(0) Then
                Me.lblMsg.Visible = True
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                'スクール生番号
                Me.dgvMANMST.SetValue("SCLMANNO", i, resultDt.Rows(i).Item("SCLMANNO").ToString.PadLeft(6, "0"c))
                '種別
                Select Case resultDt.Rows(i).Item("SCLKBN").ToString
                    Case "0"
                        Me.dgvMANMST.SetValue("SCLKBN", i, "本科生")
                    Case "1"
                        Me.dgvMANMST.SetValue("SCLKBN", i, "体験")
                    Case "8"
                        Me.dgvMANMST.SetValue("SCLKBN", i, "休会")
                    Case "9"
                        Me.dgvMANMST.SetValue("SCLKBN", i, "退会")
                End Select
                '氏名
                Me.dgvMANMST.SetValue("MANNM", i, resultDt.Rows(i).Item("MANNM").ToString)
                '氏名ｶﾅ
                Me.dgvMANMST.SetValue("MANKN", i, resultDt.Rows(i).Item("MANKN").ToString)
                '性別
                If CType(resultDt.Rows(i).Item("MANSEX").ToString, Integer).Equals(1) Then
                    '【男】
                    Me.dgvMANMST.SetValue("MANSEX", i, "男")
                Else
                    '【女】
                    Me.dgvMANMST.SetValue("MANSEX", i, "女")
                End If
                '郵便番号
                Dim strMANZIP As String = String.Empty
                If Not String.IsNullOrEmpty(resultDt.Rows(i).Item("MANZIP").ToString) Then
                    strMANZIP = resultDt.Rows(i).Item("MANZIP").ToString.PadLeft(7, "0"c).Substring(0, 3) & "-" & resultDt.Rows(i).Item("MANZIP").ToString.PadLeft(7, "0"c).Substring(3, 4)
                End If
                Me.dgvMANMST.SetValue("MANZIP", i, strMANZIP)
                '住所
                Me.dgvMANMST.SetValue("MANADDRESS", i, resultDt.Rows(i).Item("MANADDRESS").ToString)
                '電話番号
                Me.dgvMANMST.SetValue("MANTELNO", i, resultDt.Rows(i).Item("MANTELNO").ToString)
                '携帯電話番号
                Me.dgvMANMST.SetValue("MANKTELNO", i, resultDt.Rows(i).Item("MANKTELNO").ToString)
            Next

            dgvMANMST.Focus()

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

#End Region

End Class
