Imports System.Reflection

Public Class frmBase

    ' 子ウィンドウからメニューに帰ってきたとき自動処理するための戻り値
    Public Enum eResult
        CHECKIN  ' チェックイン
        CHECKOUT ' チェックアウト
        CHARGE   ' 入金
        HAKKEN   ' 発券
        NONE     ' 何もしない
        CANCEL   ' 中断
        vbERROR  ' エラーで中断
    End Enum

    Public Property vbDialogResult As eResult = eResult.NONE

    Public Property ScreenSize As Size

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        ScreenSize = New Size(1280, 800)

        Me.mAnimation.WorkerReportsProgress = True

    End Sub

    ''' <summary>
    ''' フォーム_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' 全画面表示
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        ' ダブルバッファ
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub

    ''' <summary>
    ''' 画像の透明度を変更
    ''' </summary>
    ''' <param name="img"></param>
    ''' <param name="opacityvalue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChangeOpacity(ByVal img As Image, ByVal opacityvalue As Single) As Bitmap
        Dim bmp As New Bitmap(img.Width, img.Height)
        Dim graphics__1 As Graphics = Graphics.FromImage(bmp)
        Dim colormatrix As New Imaging.ColorMatrix
        colormatrix.Matrix33 = opacityvalue
        Dim imgAttribute As New Imaging.ImageAttributes
        imgAttribute.SetColorMatrix(colormatrix, Imaging.ColorMatrixFlag.[Default], Imaging.ColorAdjustType.Bitmap)
        graphics__1.DrawImage(img, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, _
         GraphicsUnit.Pixel, imgAttribute)
        graphics__1.Dispose()
        Return bmp
    End Function

    ''' <summary>
    ''' ボタンを押したときのアニメーション
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Sub PushAnimation(ByVal obj As Control, Optional ByVal blnBack As Boolean = False)
        Dim back = obj.Location
        obj.Location = New Point(obj.Location.X, obj.Location.Y + 5)
        Me.Refresh()
        If blnBack Then
            obj.Location = back
            Me.Refresh()
        End If
    End Sub

    Public Sub PushAnimation(ByVal obj As Control, ByVal point As System.Drawing.Point)
        obj.Location = point
        Me.Refresh()
    End Sub

    ''' <summary>
    ''' 画像付きボタンを無効化したとき透過度も変更
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Public Sub ChangePictureBoxEnabled(ByRef obj As PictureBox, ByVal enabled As Boolean)
        Try
            ' 初回のみ元画像のバックアップ
            If obj.Tag Is Nothing Then
                obj.Tag = CType(obj.Image.Clone, Bitmap)
            End If

            If enabled Then
                If Not obj.Tag Is Nothing Then
                    Dim image = CType(obj.Tag, Bitmap)
                    obj.Image = frmBase.ChangeOpacity(image, 1.0)
                End If
            Else
                If Not obj.Tag Is Nothing Then
                    Dim image = CType(obj.Tag, Bitmap)
                    obj.Image = frmBase.ChangeOpacity(image, 0.5)
                End If
            End If
            obj.Enabled = enabled
            obj.Refresh()
            Me.Refresh()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' コントロールのDoubleBufferedプロパティをTrueにする
    ''' </summary>
    ''' <param name="control">対象のコントロール</param>
    Public Shared Sub EnableDoubleBuffering(ByVal control As Control)
        control.GetType().InvokeMember( _
            "DoubleBuffered", _
            BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.SetProperty, _
            Nothing, _
            control, _
            New Object() {True})
    End Sub

    ''' <summary>
    ''' 全てのコントロールを取得
    ''' </summary>
    ''' <param name="top"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllControls(ByVal top As Control) As Control()
        Dim buf As ArrayList = New ArrayList
        For Each c As Control In top.Controls
            buf.Add(c)
            buf.AddRange(GetAllControls(c))
        Next
        Return CType(buf.ToArray(GetType(Control)), Control())
    End Function

End Class