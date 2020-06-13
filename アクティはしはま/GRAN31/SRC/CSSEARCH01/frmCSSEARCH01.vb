Imports TECHNO.DataBase

Public Class frmCSSEARCH01

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 検索顧客番号取得
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetNCSNO As Integer
        Get
            Return _intNCSNO
        End Get
    End Property
    Private _intNCSNO As Integer = 0


#End Region

#Region "▼コンストラクタ"

    Public Sub New()
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客検索"

            ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New(ByVal iDB As IDatabase.IMethod)
        Try
            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            MyBase.l_Title_FormName = "顧客検索"

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
    Private Sub frmCSSEARCH01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            '顧客種別情報取得
            If Not GetKBMAST() Then
                Using frm As New frmMSGBOX01("顧客種別情報がありません。", 2)
                    frm.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' 顧客グリッド_DoubleClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvCSMAST_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvCSMAST.DoubleClick
        Try
            If Me.dgvCSMAST.RowCount = 0 Then
                Exit Sub
            End If

            Dim intRow As Integer

            If Me.dgvCSMAST.Rows.Count <> 0 Then
                intRow = Me.dgvCSMAST.CurrentRow.Index

                _intNCSNO = CType(Me.dgvCSMAST.getValue("NCSNO", intRow).ToString, Integer)
            End If

            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 顧客番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNCSNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaNCSNO.Validated, txtEndNCSNO.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If Not String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = txtBox.Text.PadLeft(8, "0"c)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 会員番号テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtMEMBERNO_Validated(sender As System.Object, e As System.EventArgs) Handles txtStaMEMBERNO.Validated, txtEndMEMBERNO.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If Not String.IsNullOrEmpty(txtBox.Text) Then
                txtBox.Text = txtBox.Text.PadLeft(8, "0"c)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 日付テキストボックス_Validated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtYYYMMDD_Validated(sender As System.Object, e As System.EventArgs) Handles txtDBIRTH.Validated, txtStaDMEMBER.Validated, txtEndDMEMBER.Validated
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            Dim strYYYYMMDD As String = txtBox.Text.PadLeft(8, "0"c)

            txtBox.Text = strYYYYMMDD.Substring(0, 4) & "/" & strYYYYMMDD.Substring(4, 2) & "/" & strYYYYMMDD.Substring(6, 2)


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
    Private Sub txtBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtStaNCSNO.KeyPress, txtEndNCSNO.KeyPress, txtDBIRTH.KeyPress, txtStaDMEMBER.KeyPress, txtEndDMEMBER.KeyPress, txtCTELEPHONE.KeyPress, txtCPOTABLENUM.KeyPress, txtStaMEMBERNO.KeyPress, txtEndMEMBERNO.KeyPress
        Try
            '*** 数値とBackspace以外入力不可制御 ***'
            If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
                e.Handled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ''' <summary>
    ' ''' テキストボックス_Enter
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub txtBox_Enter(sender As System.Object, e As System.EventArgs) Handles txtCCSNAME.Enter, txtCCSKANA.Enter, txtDBIRTH.Enter, txtCADDRESS.Enter, txtStaDMEMBER.Enter, txtEndDMEMBER.Enter, txtCTELEPHONE.Enter, txtCPOTABLENUM.Enter
    '    Try
    '        Dim txtBox As TextBox

    '        txtBox = CType(sender, TextBox)

    '        txtBox.Text = txtBox.Text.Replace("/", String.Empty)
    '        txtBox.Text = txtBox.Text.Replace("-", String.Empty)

    '        txtBox.SelectAll()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' テキストボックス_MouseDown
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub txtBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtCCSNAME.MouseDown, txtCCSKANA.MouseDown, txtDBIRTH.MouseDown, txtCADDRESS.MouseDown, txtStaDMEMBER.MouseDown, txtEndDMEMBER.MouseDown, txtCTELEPHONE.MouseDown, txtCPOTABLENUM.MouseDown

    '    Try
    '        Dim txtBox As TextBox

    '        txtBox = CType(sender, TextBox)

    '        txtBox.SelectAll()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    ''' <summary>
    ''' カード未発行チェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkCard_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCard.CheckedChanged
        Try
            If Me.chkCard.Checked Then
                Me.chkCard2.Checked = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カード発行済みチェックボックス_CheckedChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkCard2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCard2.CheckedChanged
        Try
            If Me.chkCard2.Checked Then
                Me.chkCard.Checked = False
            End If

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


            '顧客データ取得
            GetCSMAST()

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

            '顧客番号
            Me.txtStaNCSNO.Text = String.Empty
            Me.txtEndNCSNO.Text = String.Empty
            '氏名
            Me.txtCCSNAME.Text = String.Empty
            '氏名ｶﾅ
            Me.txtCCSKANA.Text = String.Empty
            '住所
            Me.txtCADDRESS.Text = String.Empty
            '誕生日
            Me.txtDBIRTH.Text = String.Empty
            '会員期限
            Me.txtStaDMEMBER.Text = String.Empty
            Me.txtEndDMEMBER.Text = String.Empty
            '電話番号
            Me.txtCTELEPHONE.Text = String.Empty
            '携帯電話番号
            Me.txtCPOTABLENUM.Text = String.Empty
            '会員番号
            Me.txtStaMEMBERNO.Text = String.Empty
            Me.txtEndMEMBERNO.Text = String.Empty

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
            sql.Append(" ORDER BY KBMAST ")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If


            Me.cmbNCSRANK.Items.Add(String.Empty)
            For i As Integer = 0 To resultDt.Rows.Count - 1
                Me.cmbNCSRANK.Items.Add(resultDt.Rows(i).Item("CKBNAME").ToString)
            Next

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    ''' <summary>
    ''' 顧客情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCSMAST() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try


            sql.Append(" SELECT ")
            sql.Append(" A.NCSNO")                                      '顧客番号
            sql.Append(",B.CKBNAME")                                    '顧客種別
            sql.Append(",A.CCSNAME")                                    '氏名
            sql.Append(",A.CCSKANA")                                    '氏名ｶﾅ
            sql.Append(",A.NSEX")                                       '性別
            sql.Append(",A.NZIP")                                       '郵便番号
            sql.Append(",(A.CADDRESS1 || A.CADDRESS2) AS CADDRESS")     '住所
            sql.Append(",TO_CHAR(A.DBIRTH,'YYYY/MM/DD') AS DBIRTH")     '誕生日
            sql.Append(",A.CTELEPHONE")                                 '電話番号
            sql.Append(",A.CPOTABLENUM")                                '携帯電話番号
            sql.Append(",TO_CHAR(A.DMEMBER,'YYYY/MM/DD') AS DMEMBER")   '会員期限
            sql.Append(",A.MEMBERNO")                                   '会員番号
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KBMAST AS B ON B.NKBNO = A.NCSRANK")
            sql.Append(" WHERE 1=1")
            '顧客番号
            If Not String.IsNullOrEmpty(Me.txtStaNCSNO.Text) Then
                sql.Append(" AND A.NCSNO >= " & CType(Me.txtStaNCSNO.Text, Integer))
            End If
            If Not String.IsNullOrEmpty(Me.txtEndNCSNO.Text) Then
                sql.Append(" AND A.NCSNO <= " & CType(Me.txtEndNCSNO.Text, Integer))
            End If
            '顧客種別
            If Me.cmbNCSRANK.SelectedIndex > 0 Then
                sql.Append(" AND A.NCSRANK = " & Me.cmbNCSRANK.SelectedIndex)
            End If
            '氏名
            If Not String.IsNullOrEmpty(Me.txtCCSNAME.Text) Then
                sql.Append(" AND REPLACE(REPLACE(A.CCSNAME,'　',''),' ','') LIKE '%" & Me.txtCCSNAME.Text.Trim.Replace("　", String.Empty).Replace(" ", String.Empty) & "%'")
            End If
            '氏名ｶﾅ
            If Not String.IsNullOrEmpty(Me.txtCCSKANA.Text) Then
                sql.Append(" AND REPLACE(REPLACE(A.CCSKANA,'　',''),' ','') LIKE '%" & Me.txtCCSKANA.Text.Trim.Replace("　", String.Empty).Replace(" ", String.Empty) & "%'")
            End If
            '性別
            If Me.cmbNSEX.SelectedIndex > 0 Then
                sql.Append(" AND A.NSEX = " & Me.cmbNSEX.SelectedIndex)
            End If
            '住所
            If Not String.IsNullOrEmpty(Me.txtCADDRESS.Text) Then
                sql.Append(" AND (A.CADDRESS1 || A.CADDRESS2) LIKE '%" & Me.txtCADDRESS.Text & "%'")
            End If
            '誕生日
            If Not String.IsNullOrEmpty(Me.txtDBIRTH.Text) Then
                sql.Append(" AND TO_CHAR(A.DBIRTH,'YYYYMMDD') = '" & Me.txtDBIRTH.Text.Replace("/", String.Empty) & "'")
            End If
            'カード未発行
            If Me.chkCard.Checked Then
                sql.Append(" AND (A.NCARDID IS NULL OR A.NCARDID = 0)")
            End If
            'カード発行済み
            If Me.chkCard2.Checked Then
                sql.Append(" AND NOT (A.NCARDID IS NULL OR A.NCARDID = 0)")
            End If
            '会員期限
            If Not String.IsNullOrEmpty(Me.txtStaDMEMBER.Text) Then
                sql.Append(" AND TO_CHAR(A.DMEMBER,'YYYYMMDD') >= '" & Me.txtStaDMEMBER.Text.Replace("/", String.Empty) & "'")
            End If
            If Not String.IsNullOrEmpty(Me.txtEndDMEMBER.Text) Then
                sql.Append(" AND TO_CHAR(A.DMEMBER,'YYYYMMDD') <= '" & Me.txtEndDMEMBER.Text.Replace("/", String.Empty) & "'")
            End If
            '電話番号
            If Not String.IsNullOrEmpty(Me.txtCTELEPHONE.Text) Then
                sql.Append(" AND A.CTELEPHONE LIKE '%" & Me.txtCTELEPHONE.Text & "%'")
            End If
            '携帯電話番号
            If Not String.IsNullOrEmpty(Me.txtCPOTABLENUM.Text) Then
                sql.Append(" AND A.CPOTABLENUM LIKE '%" & Me.txtCPOTABLENUM.Text & "%'")
            End If
            '会員番号
            If Not String.IsNullOrEmpty(Me.txtStaMEMBERNO.Text) Then
                sql.Append(" AND A.MEMBERNO >= '" & Me.txtStaMEMBERNO.Text.PadLeft(8, "0"c) & "'")
            End If
            If Not String.IsNullOrEmpty(Me.txtEndMEMBERNO.Text) Then
                sql.Append(" AND A.MEMBERNO <= '" & Me.txtEndMEMBERNO.Text.PadLeft(8, "0"c) & "'")
            End If

            sql.Append(" ORDER BY A.NCSNO")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Me.dgvCSMAST.RowCount = resultDt.Rows.Count
            Me.lblMsg.Visible = False
            If resultDt.Rows.Count.Equals(0) Then
                Me.lblMsg.Visible = True
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                '顧客番号
                Me.dgvCSMAST.SetValue("NCSNO", i, resultDt.Rows(i).Item("NCSNO").ToString.PadLeft(8, "0"c))
                '顧客種別
                Me.dgvCSMAST.SetValue("CKBNAME", i, resultDt.Rows(i).Item("CKBNAME"))
                '氏名
                Me.dgvCSMAST.SetValue("CCSNAME", i, resultDt.Rows(i).Item("CCSNAME").ToString)
                '氏名ｶﾅ
                Me.dgvCSMAST.SetValue("CCSKANA", i, resultDt.Rows(i).Item("CCSKANA").ToString)
                '性別
                If CType(resultDt.Rows(i).Item("NSEX").ToString, Integer).Equals(1) Then
                    '【男】
                    Me.dgvCSMAST.SetValue("NSEX", i, "男")
                Else
                    '【女】
                    Me.dgvCSMAST.SetValue("NSEX", i, "女")
                End If
                '郵便番号
                Dim strNZIP As String = String.Empty
                If Not String.IsNullOrEmpty(resultDt.Rows(i).Item("NZIP").ToString) Then
                    strNZIP = resultDt.Rows(i).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(0, 3) & "-" & resultDt.Rows(i).Item("NZIP").ToString.PadLeft(7, "0"c).Substring(3, 4)
                End If
                Me.dgvCSMAST.SetValue("NZIP", i, strNZIP)
                '住所
                Me.dgvCSMAST.SetValue("CADDRESS", i, resultDt.Rows(i).Item("CADDRESS").ToString)
                '誕生日
                Me.dgvCSMAST.SetValue("DBIRTH", i, resultDt.Rows(i).Item("DBIRTH").ToString)
                '電話番号
                Me.dgvCSMAST.SetValue("CTELEPHONE", i, resultDt.Rows(i).Item("CTELEPHONE").ToString)
                '携帯電話番号
                Me.dgvCSMAST.SetValue("CPOTABLENUM", i, resultDt.Rows(i).Item("CPOTABLENUM").ToString)
                '会員期限
                Me.dgvCSMAST.SetValue("DMEMBER", i, resultDt.Rows(i).Item("DMEMBER").ToString)
                '会員番号
                Me.dgvCSMAST.SetValue("MEMBERNO", i, resultDt.Rows(i).Item("MEMBERNO").ToString)
            Next

            dgvCSMAST.Focus()

            Return True

        Catch ex As Exception
            Throw ex

        End Try
    End Function

#End Region



End Class
