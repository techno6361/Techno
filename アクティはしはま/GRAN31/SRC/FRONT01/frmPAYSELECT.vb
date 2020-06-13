Public Class frmPAYSELECT

#Region "▼プロパティ"

    ''' <summary>
    ''' 精算区分
    ''' </summary>
    ''' <value>【0】キャンセル【1】現金精算【2】打席カード</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PaySelect() As Integer
        Get
            Return _intPaySelect
        End Get
    End Property
    Private _intPaySelect As Integer = 0

#End Region

#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPAYSELECT_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
    Private Sub frmPAYSELECT_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
            Me.btnPaySelect2.PerformClick()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 精算区分ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPaySelect_Click(sender As System.Object, e As System.EventArgs) Handles btnPaySelect1.Click, btnPaySelect2.Click, btnPaySelect0.Click
        Try
            Dim btnPaySelect As Button
            btnPaySelect = CType(sender, Button)

            _intPaySelect = CType(btnPaySelect.Tag, Integer)

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "▼関数定義"

#End Region





End Class