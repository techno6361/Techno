Public Class frmRETURNSHOPNO

#Region "▼宣言部"

#End Region

#Region "▼プロパティ"

    ''' <summary>
    ''' 種別
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property NKBNO As String
        Set(value As String)
            _strNKBNO = value
        End Set
    End Property
    Private _strNKBNO As String = String.Empty
    ''' <summary>
    ''' シリアルナンバー
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property SERIALNO As String
        Get
            Return _strSERIALNO
        End Get
        Set(value As String)
            _strSERIALNO = value
        End Set
    End Property
    Private _strSERIALNO As String = String.Empty
    ''' <summary>
    ''' 予備
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property YOBI As String
        Get
            Return _strYOBI
        End Get
        Set(value As String)
            _strYOBI = value
        End Set
    End Property
    Private _strYOBI As String = String.Empty
    ''' <summary>
    ''' ボール単価1F
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property BALL1F As Double
        Set(value As Double)
            _dblBALL1F = value
        End Set
    End Property
    Private _dblBALL1F As Double = 0
    ''' <summary>
    ''' ボール単価2F
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property BALL2F As Double
        Set(value As Double)
            _dblBALL2F = value
        End Set
    End Property
    Private _dblBALL2F As Double = 0
    ''' <summary>
    ''' 使用金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property USEKINGAKU As Integer
        Get
            Return _intUSEKINGAKU
        End Get
    End Property
    Private _intUSEKINGAKU As Integer = 0
    ''' <summary>
    ''' 使用ボール数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property USEBALLSU As Integer
        Get
            Return _intUSEBALLSU
        End Get
    End Property
    Private _intUSEBALLSU As Integer = 0

#End Region


#Region "▼イベント定義"

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmRETURNSHOPNO_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
    Private Sub frmRETURNSHOPNO_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
            If Me.pnlTama.Visible Then
                Me.txtUSEBALL.Focus()
            Else
                Me.txtPlayTime.Focus()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 店番復帰ボタン_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Try
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 使用球数テキストボックス_TextChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtUSEBALL_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtUSEBALL.TextChanged
        Dim dblBALL As Double = 0
        Try
            If String.IsNullOrEmpty(Me.txtUSEBALL.Text) Then Exit Sub

            If rdoBALL1F.Checked Then
                dblBALL = _dblBALL1F
            Else
                dblBALL = _dblBALL2F
            End If

            _intUSEKINGAKU = CType(dblBALL * CType(Me.txtUSEBALL.Text, Integer), Integer)
            Me.txtUSEKINGAKU.Text = _intUSEKINGAKU.ToString("#,##0")

            _intUSEBALLSU = CType(Me.txtUSEBALL.Text, Integer)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 残時間テキストボックス_TextChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPlayTime_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPlayTime.TextChanged
        Dim intZANTIME As Integer = 0
        Try
            If String.IsNullOrEmpty(Me.txtPlayTime.Text) Then Exit Sub
            If String.IsNullOrEmpty(Me.lblSelectTime.Text) Then Exit Sub
            If String.IsNullOrEmpty(Me.lblZANTIME.Text) Then Exit Sub

            If CType(Me.lblSelectTime.Text, Integer) < CType(Me.txtPlayTime.Text, Integer) Then
                intZANTIME = 0
            Else
                intZANTIME = CType(Me.lblSelectTime.Text, Integer) - CType(Me.txtPlayTime.Text, Integer)
            End If

            _strSERIALNO = _strSERIALNO.Substring(0, 2) & Convert.ToString(intZANTIME, 16).ToString.PadLeft(2, "0"c).Substring(0, 1).ToUpper
            _strYOBI = Convert.ToString(intZANTIME, 16).ToString.PadLeft(2, "0"c).Substring(1, 1).ToUpper

            Me.lblZANTIME.Text = intZANTIME.ToString



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
            Me.pnlTama.Visible = False
            Me.pnlTime.Visible = False
            If _strNKBNO.Equals("C") Then
                '【打ち放題】
                Me.pnlTime.Visible = True

                Me.txtPlayTime.Text = "0"

                Me.lblSelectTime.Text = Convert.ToInt32(_strSERIALNO.Substring(2, 1).ToString & _strYOBI.ToString, 16).ToString

                Me.lblZANTIME.Text = Convert.ToInt32(_strSERIALNO.Substring(2, 1).ToString & _strYOBI.ToString, 16).ToString
            Else
                '【1球貸し】
                Me.pnlTama.Visible = True
            End If

            Me.lblBALL1F.Text = _dblBALL1F.ToString
            Me.lblBALL2F.Text = _dblBALL2F.ToString

            Me.txtUSEBALL.Text = "0"

            Me.txtUSEKINGAKU.Text = "0"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region







End Class