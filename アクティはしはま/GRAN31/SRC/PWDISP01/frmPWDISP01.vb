Imports Techno.DataBase

Public Class frmPWDISP01

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' パスワード区分
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PWKBN As Integer
        Set(ByVal value As Integer)
            _intPWKBN = value
        End Set
    End Property
    Private _intPWKBN As Integer = 0

    ''' <summary>
    ''' パスワード入力成功かどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CLEAR As Boolean
        Get
            Return _blnClear
        End Get
    End Property
    Private _blnClear As Boolean = False

    Public ReadOnly Property SEFLG As Boolean
        Get
            Return _blnSEFLG
        End Get
    End Property
    Private _blnSEFLG As Boolean = False
#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPWDISP01_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            '画面初期設定
            Init()

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
    Private Sub frmPWDISP01_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.txtPw.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' フォーム_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPWDISP01_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) AndAlso Not e.Alt AndAlso Not e.Control Then
                'あたかもTabキーが押されたかのようにする
                'Shiftが押されている時は前のコントロールのフォーカスを移動
                Me.ProcessTabKey(Not e.Shift)

                e.Handled = True
                '.NET Framework 2.0以降
                e.SuppressKeyPress = True
            End If
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
            If _intPWKBN.Equals(1) Then
                If Me.txtPw.Text.Equals("1907") Or Me.txtPw.Text.Equals(UIUtility.SYSTEM.SEPW) Then
                    If Me.txtPw.Text.Equals(UIUtility.SYSTEM.SEPW) Then _blnSEFLG = True
                    _blnClear = True
                    Me.Close()
                    Exit Sub
                End If
            Else
                If Me.txtPw.Text.Equals(UIUtility.SYSTEM.ADMNPW) Or Me.txtPw.Text.Equals(UIUtility.SYSTEM.SEPW) Then
                    If Me.txtPw.Text.Equals(UIUtility.SYSTEM.SEPW) Then _blnSEFLG = True
                    _blnClear = True
                    Me.Close()
                    Exit Sub
                End If
            End If

            Me.lblMsg.Text = "パスワードが違います。"
            Me.lblMsg.ForeColor = Color.Red
            Me.lblMsg.Refresh()

            Threading.Thread.Sleep(2000)


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.lblMsg.Text = "パスワードを入力して下さい。"
            Me.lblMsg.ForeColor = Color.Black
            Me.txtPw.Focus()
            Me.txtPw.SelectAll()
        End Try
    End Sub

    ''' <summary>
    ''' ｷｬﾝｾﾙボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Try
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
            Select Case _intPWKBN
                Case 1
                    Me.pnlKBN1.Visible = True
                    Me.txtPw.Text = String.Empty
                Case 2
                    Me.pnlKBN1.Visible = True
                    Me.txtPw.Text = String.Empty
                    Me.btnCancel.Visible = True
            End Select


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region






End Class
