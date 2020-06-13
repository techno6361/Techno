Imports TECHNO.DataBase

Public Class frmBIKO

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
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property NCSNO As Integer
        Set(value As Integer)
            _intNCSNO = value
        End Set
    End Property
    Private _intNCSNO As Integer
    ''' <summary>
    ''' ポップアップ
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MEMBERNO As String
        Set(value As String)
            _strMEMBERNO = value
        End Set
    End Property
    Private _strMEMBERNO As String
    ''' <summary>
    ''' 備考１
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property BIKO1 As String
        Get
            Return _strBIKO1
        End Get
        Set(value As String)
            _strBIKO1 = value
        End Set
    End Property
    Private _strBIKO1 As String = String.Empty
    ''' <summary>
    ''' 備考２
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property BIKO2 As String
        Get
            Return _strBIKO2
        End Get
        Set(value As String)
            _strBIKO2 = value
        End Set
    End Property
    Private _strBIKO2 As String = String.Empty
    ''' <summary>
    ''' 備考３
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property BIKO3 As String
        Get
            Return _strBIKO3
        End Get
        Set(value As String)
            _strBIKO3 = value
        End Set
    End Property
    Private _strBIKO3 As String = String.Empty

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

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' キーバインド
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmBIKO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    SendKeys.Send("{TAB}")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' クリア備考１ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClrBIKO1_Click(sender As System.Object, e As System.EventArgs) Handles btnClrBIKO1.Click
        Try
            Me.txtBIKO1.Text = String.Empty

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' クリア備考２ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClrBIKO2_Click(sender As System.Object, e As System.EventArgs) Handles btnClrBIKO2.Click
        Try
            Me.txtBIKO2.Text = String.Empty

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' クリア備考３ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClrBIKO3_Click(sender As System.Object, e As System.EventArgs) Handles btnClrBIKO3.Click
        Try
            Me.txtBIKO3.Text = String.Empty

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 登録ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpd_Click(sender As System.Object, e As System.EventArgs) Handles btnUpd.Click
        Dim sql As New System.Text.StringBuilder
        Try
            _iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            sql.Append(" BIKO1 = '" & Me.txtBIKO1.Text & "'")
            sql.Append(",BIKO2 = '" & Me.txtBIKO2.Text & "'")
            sql.Append(",BIKO3 = '" & Me.txtBIKO3.Text & "'")
            If Me.chkPASSNO.Checked Then
                sql.Append(",MEMBERNO = '1'")
            Else
                sql.Append(",MEMBERNO = '0'")
            End If
            sql.Append(",DUPDATE = NOW()")
            sql.Append(" WHERE")
            sql.Append(" NCSNO = " & _intNCSNO)
            If Not _iDatabase.ExecuteUpdate(sql.ToString) Then
                _iDatabase.RollBack()
                Using frm As New frmMSGBOX01("顧客情報の更新に失敗しました。", 2)
                    frm.ShowDialog()
                    Exit Sub
                End Using
            End If

            _iDatabase.Commit()

            _strBIKO1 = Me.txtBIKO1.Text
            _strBIKO2 = Me.txtBIKO2.Text
            _strBIKO3 = Me.txtBIKO3.Text


            Me.Close()

        Catch ex As Exception
            _iDatabase.RollBack()
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
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


#Region "関数定義"

    ''' <summary>
    ''' 画面初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Init()
        Try

            Me.txtBIKO1.Text = _strBIKO1
            Me.txtBIKO2.Text = _strBIKO2
            Me.txtBIKO3.Text = _strBIKO3

            Me.chkPASSNO.Checked = False
            If _strMEMBERNO.Equals("1") Then
                Me.chkPASSNO.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region






End Class