Public Class HINMSTButton

    Private _HINNMA As String  ' 商品名
    Private _URIBTK As Integer ' 単価
    Private _POINT As Integer  ' ポイント
    Private _BACK_COLOR As Color = Color.Orange  ' 背景色
    Private _FOCUS_COLOR As Color = Color.Orange ' 背景色(マウスオーバー)
    Private _SELECT_COLOR As Color = Color.YellowGreen ' 背景色(選択)
    ' 非表示
    Private _BMNCD As String
    Private _BUNCDA As String
    Private _BUNCDB As String
    Private _BUNCDC As String
    Private _HINCD As String
    Private _ZEITK As Integer

    ' 親フォームに返すイベント
    Public Event OnButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' 公開プロパティ
    Public Property HINNMA As String
        Get
            Return _HINNMA
        End Get
        Set(ByVal value As String)
            _HINNMA = value
            lblHINNMA.Text = value
        End Set
    End Property

    Public Property URIBTK As Integer
        Get
            Return _URIBTK
        End Get
        Set(ByVal value As Integer)
            _URIBTK = value
            lblURIATK.Text = value.ToString("#,0") & "円"
        End Set
    End Property

    Public Property POINT As Integer
        Get
            Return _POINT
        End Get
        Set(ByVal value As Integer)
            _POINT = value
            lblPOINT.Text = "(Point: " & value.ToString("#,0").PadLeft(4, " "c) & ")"
        End Set
    End Property

    Public Property SELECT_COLOR As Color
        Get
            Return _SELECT_COLOR
        End Get
        Set(ByVal value As Color)
            _SELECT_COLOR = value
        End Set
    End Property

    Public Property BACK_COLOR As Color
        Get
            Return _BACK_COLOR
        End Get
        Set(ByVal value As Color)
            _BACK_COLOR = value
        End Set
    End Property

    ' 選択状態フラグ
    Dim _bSelected As Boolean

    Public Property Selected As Boolean
        Get
            Return _bSelected
        End Get
        Set(ByVal value As Boolean)
            _bSelected = value
            If _bSelected Then
                ChangeColor(_SELECT_COLOR)
            Else
                ChangeColor(_BACK_COLOR)
            End If
        End Set
    End Property

    ' 非表示プロパティ
    Public Property BMNCD As String
        Get
            Return _BMNCD
        End Get
        Set(ByVal value As String)
            _BMNCD = value
        End Set
    End Property

    Public Property BUNCDA As String
        Get
            Return _BUNCDA
        End Get
        Set(ByVal value As String)
            _BUNCDA = value
        End Set
    End Property

    Public Property BUNCDB As String
        Get
            Return _BUNCDB
        End Get
        Set(ByVal value As String)
            _BUNCDB = value
        End Set
    End Property

    Public Property BUNCDC As String
        Get
            Return _BUNCDC
        End Get
        Set(ByVal value As String)
            _BUNCDC = value
        End Set
    End Property

    Public Property HINCD As String
        Get
            Return _HINCD
        End Get
        Set(ByVal value As String)
            _HINCD = value
        End Set
    End Property

    Public Property ZEITK As Integer
        Get
            Return _ZEITK
        End Get
        Set(ByVal value As Integer)
            _ZEITK = value
        End Set
    End Property

    ''' <summary>
    ''' クリックすると親フォームにイベント発行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnHINNMA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHINNMA.Click        
        If _bSelected Then
            _bSelected = False
            ChangeColor(_BACK_COLOR)
        Else
            _bSelected = True
            ChangeColor(_SELECT_COLOR)
        End If
        RaiseEvent OnButtonClick(Me, e)
    End Sub
    Private Sub lblHINNMA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHINNMA.Click
        btnHINNMA.PerformClick()
    End Sub
    Private Sub lblURIATK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblURIATK.Click
        btnHINNMA.PerformClick()
    End Sub

    ' ''' <summary>
    ' ''' マウスオーバーで背景色変更
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub lblHINNMA_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblHINNMA.MouseMove
    '    ChangeColor(_FOCUS_COLOR)
    'End Sub
    'Private Sub btnHINNMA_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHINNMA.MouseMove
    '    ChangeColor(_FOCUS_COLOR)
    'End Sub
    'Private Sub lblURIATK_MouseMove(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblURIATK.MouseMove
    '    ChangeColor(_FOCUS_COLOR)
    'End Sub

    ' ''' <summary>
    ' ''' マウスを離すと背景色を元に戻す
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub btnHINNMA_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHINNMA.MouseLeave
    '    ChangeColor(_BACK_COLOR)
    'End Sub
    'Private Sub lblHINNMA_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblHINNMA.MouseLeave
    '    ChangeColor(_BACK_COLOR)
    'End Sub
    'Private Sub lblURIATK_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblURIATK.MouseLeave
    '    ChangeColor(_BACK_COLOR)
    'End Sub

    ''' <summary>
    ''' 背景色を変更する
    ''' </summary>
    ''' <param name="col"></param>
    ''' <remarks></remarks>
    Private Sub ChangeColor(ByVal col As Color)
        btnHINNMA.BackColor = col
        lblHINNMA.BackColor = btnHINNMA.BackColor
        lblURIATK.BackColor = btnHINNMA.BackColor
    End Sub
    
End Class
