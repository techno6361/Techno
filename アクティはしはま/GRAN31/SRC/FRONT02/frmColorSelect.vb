Public Class frmColorSelect

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 選択カラー
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectColor() As String
        Get
            Return _strSelectColor
        End Get
    End Property
    Private _strSelectColor As String = String.Empty

    ''' <summary>
    ''' 更新範囲区分
    ''' </summary>
    ''' <value>【False】特定の顧客情報【True】全ての顧客情報</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectAll As Boolean
        Get
            Return _blnSelectAll
        End Get
    End Property
    Private _blnSelectAll As Boolean = False

#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmColorSelect_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            _blnSelectAll = False

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
    Private Sub txtMANNO_Validated(sender As System.Object, e As System.EventArgs)
        Try
            Dim txtBox As TextBox
            txtBox = CType(sender, TextBox)

            If String.IsNullOrEmpty(txtBox.Text) Then Exit Sub

            txtBox.Text = txtBox.Text.PadLeft(8, "0"c)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' カラーボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnColor_Click(sender As System.Object, e As System.EventArgs) Handles btnColor1.Click, btnColor2.Click, btnColor3.Click, btnColor4.Click
        Try
            Dim btn As Button
            btn = CType(sender, Button)

            Select Case btn.Tag.ToString
                Case "1"
                    _strSelectColor = "Green"
                Case "2"
                    _strSelectColor = "Yellow"
                Case "3"
                    _strSelectColor = "Red"
                Case "4"
                    _strSelectColor = "White"
            End Select

            _blnSelectAll = Me.chkAll.Checked



            If _blnSelectAll Then
                Using frm As New frmMSGBOX01("全ての顧客情報に反映されます。よろしいですか？", 1)
                    frm.ShowDialog()
                    If Not frm.Reply Then
                        Exit Sub
                    End If
                End Using
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ''' <summary>
    ''' 閉じるボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEnd_Click(sender As System.Object, e As System.EventArgs) Handles btnEnd.Click
        Try
            _strSelectColor = String.Empty

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

#End Region





End Class