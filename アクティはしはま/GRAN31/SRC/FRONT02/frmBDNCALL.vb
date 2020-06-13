Imports Techno.DataBase

Public Class frmBDNCALL

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"
    ''' <summary>
    ''' データベース制御
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property iDatabase As IDatabase.IMethod
        Set(value As IDatabase.IMethod)
            _iDatabase = value
        End Set
    End Property
    Private _iDatabase As IDatabase.IMethod
#End Region

#Region "▼イベント定義"


    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmBIKO_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            '画面初期設定
            Init()

            GetBndCall()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 最小化ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMin_Click(sender As System.Object, e As System.EventArgs) Handles btnMin.Click
        Try
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 呼出しOFFボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStop1_Click(sender As System.Object, e As System.EventArgs) Handles btnStop1.Click, btnStop2.Click, btnStop3.Click, btnStop4.Click, btnStop5.Click, btnStop6.Click
        Dim sql As New System.Text.StringBuilder
        Try
            Dim btn As Button
            btn = CType(sender, Button)

            _iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE BNDMTA SET")
            sql.Append(" CALLFLG = NULL")
            sql.Append(" WHERE")
            sql.Append(" BNDNO = '" & btn.Tag.ToString.PadLeft(2, "0"c) & "'")
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Using frm As New frmMSGBOX01("ﾍﾞﾝﾀﾞｰ情報の更新に失敗しました。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            _iDatabase.Commit()

            If btn.Tag.ToString.Equals("1") Then Me.lblBND1.ForeColor = Color.Silver
            If btn.Tag.ToString.Equals("2") Then Me.lblBND2.ForeColor = Color.Silver
            If btn.Tag.ToString.Equals("3") Then Me.lblBND3.ForeColor = Color.Silver
            If btn.Tag.ToString.Equals("4") Then Me.lblBND4.ForeColor = Color.Silver
            If btn.Tag.ToString.Equals("5") Then Me.lblBND5.ForeColor = Color.Silver
            If btn.Tag.ToString.Equals("6") Then Me.lblBND6.ForeColor = Color.Silver
            btn.BackColor = Color.Gray
            btn.ForeColor = Color.Silver
            btn.Enabled = False

        Catch ex As Exception
            _iDatabase.RollBack()
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


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ﾍﾞﾝﾀﾞｰ呼び出し情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBndCall() As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT")
            sql.Append(" *")
            sql.Append(" FROM BNDMTA")
            sql.Append(" WHERE")
            '----------検索条件----------'
            sql.Append(" CALLFLG = '1'")
            sql.Append(" ORDER BY BNDNO")

            Dim resultDt As DataTable = _iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For Each arr As DataRow In resultDt.Rows
                If Not arr("CALLFLG").ToString.Equals("1") Then
                    Continue For
                End If
                Select Case arr("BNDNO").ToString
                    Case "01"
                        Me.lblBND1.ForeColor = Color.Yellow
                        Me.btnStop1.BackColor = Color.Orange
                        Me.btnStop1.ForeColor = Color.White
                        Me.btnStop1.Enabled = True
                    Case "02"
                        Me.lblBND2.ForeColor = Color.Yellow
                        Me.btnStop2.BackColor = Color.Orange
                        Me.btnStop2.ForeColor = Color.White
                        Me.btnStop2.Enabled = True
                    Case "03"
                        Me.lblBND3.ForeColor = Color.Yellow
                        Me.btnStop3.BackColor = Color.Orange
                        Me.btnStop3.ForeColor = Color.White
                        Me.btnStop3.Enabled = True
                    Case "04"
                        Me.lblBND4.ForeColor = Color.Yellow
                        Me.btnStop4.BackColor = Color.Orange
                        Me.btnStop4.ForeColor = Color.White
                        Me.btnStop4.Enabled = True
                    Case "05"
                        Me.lblBND5.ForeColor = Color.Yellow
                        Me.btnStop5.BackColor = Color.Orange
                        Me.btnStop5.ForeColor = Color.White
                        Me.btnStop5.Enabled = True
                    Case "06"
                        Me.lblBND6.ForeColor = Color.Yellow
                        Me.btnStop6.BackColor = Color.Orange
                        Me.btnStop6.ForeColor = Color.White
                        Me.btnStop6.Enabled = True
                End Select
            Next

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try

    End Function

#End Region




End Class