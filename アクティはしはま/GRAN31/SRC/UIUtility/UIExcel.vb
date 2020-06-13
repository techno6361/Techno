''' <summary>
''' エクセル用の機能をまとめたクラス
''' </summary>
''' <remarks></remarks>
Public Class UIExcel

    Private _app As Microsoft.Office.Interop.Excel.Application = Nothing
    Private _book As Microsoft.Office.Interop.Excel.Workbook = Nothing
    Private _sheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing

    Public ReadOnly Property Cells As Microsoft.Office.Interop.Excel.Range
        Get
            Return _sheet.Cells
        End Get
    End Property

    ' 罫線の始点、終点
    Public Property LineIndex_S As Integer = -1
    Public Property LineIndex_E As Integer = -1

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Try
            _app = CType(CreateObject("Excel.Application"), Microsoft.Office.Interop.Excel.Application)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ブックとワークシートを開く
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="index"></param>
    ''' <param name="visible"></param>
    ''' <remarks></remarks>
    Public Sub Open(ByVal fileName As String, ByVal index As Integer, Optional ByVal visible As Boolean = True)
        Try
            BookOpen(fileName, visible)
            SheetOpen(index)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 別プロセスでファイルを開く
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub ProccessOpen(ByVal fileName As String)
        System.Diagnostics.Process.Start(fileName)
    End Sub

    ''' <summary>
    ''' ブックを開く
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="visible"></param>
    ''' <remarks></remarks>
    Public Sub BookOpen(ByVal fileName As String, ByVal visible As Boolean)
        Try
            _app.Visible = visible
            _book = _app.Workbooks.Open(fileName)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ワークシートを選択する(インデックス)
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Public Sub SheetOpen(ByVal index As Integer)
        Try
            _sheet = CType(_book.Worksheets(index), Microsoft.Office.Interop.Excel.Worksheet)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ワークシートを選択する(ワークシート名)
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub SheetOpen(ByVal name As String)
        Try
            _sheet = CType(_book.Worksheets(name), Microsoft.Office.Interop.Excel.Worksheet)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' セルに値を書き込む
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index"></param>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Sub CellWrite(ByVal row_index As Integer, ByVal col_index As Integer, ByVal obj As Object)
        Try
            _sheet.Cells(row_index, col_index) = obj
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 改ページする
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <remarks></remarks>
    Public Sub HPageBreak(ByVal row_index As Integer)
        Try
            _sheet.HPageBreaks.Add(_sheet.Range(String.Format("A{0}", row_index)))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' セルを選択する
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index"></param>
    ''' <remarks></remarks>
    Public Sub CellSelect(ByVal row_index As Integer, ByVal col_index As Integer)
        Try
            Dim strRange = String.Format("{0}{1}", ConvertToLetter(col_index), row_index)
            _sheet.Range(strRange).Select()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' フォント太字
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index"></param>
    ''' <remarks></remarks>
    Public Sub FontBold(ByVal row_index As Integer, ByVal col_index As Integer)
        Try
            Dim strRange = String.Format("{0}{1}", ConvertToLetter(col_index), row_index)
            _sheet.Range(strRange).Font.Bold = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ブックを保存する
    ''' </summary>
    ''' <param name="saveName"></param>
    ''' <remarks></remarks>
    Public Sub BookSave(ByVal saveName As String)
        Try
            _book.SaveAs(saveName)
            _book.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' ブックを保存する
    ''' </summary>
    ''' <param name="saveName"></param>
    ''' <param name="autoOpenFlg">ファイル保存後、自動でプロセスを終了し保存したファイルを開くオプション</param>
    ''' <remarks></remarks>
    Public Sub SaveAs(ByVal saveName As String, Optional ByVal autoOpenFlg As Boolean = False)
        Try
            BookSave(saveName)
            If autoOpenFlg Then
                Dispose()
                ProccessOpen(saveName)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' エクセルを解放する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose()
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_book)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_sheet)

            _app.Quit()

            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app)
            _app = Nothing

            GC.Collect()

            ' プロセスを強制的に終了させる
            Dim ps As System.Diagnostics.Process() = _
            System.Diagnostics.Process.GetProcessesByName("Excel")
            For Each p As System.Diagnostics.Process In ps
                p.Kill()
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 罫線の始点と終点を設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetLineRange(ByVal intStart As Integer, ByVal intEnd As Integer)
        LineIndex_S = intStart
        LineIndex_E = intEnd
    End Sub

    ''' <summary>
    ''' 水平線を描画
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index1"></param>
    ''' <param name="col_index2"></param>
    ''' <param name="lineStyle"></param>
    ''' <param name="weight"></param>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Public Sub DrawLine(ByVal row_index As Integer, _
                        Optional ByVal col_index1 As Integer = -1, _
                        Optional ByVal col_index2 As Integer = -1, _
                         Optional ByVal lineStyle As Microsoft.Office.Interop.Excel.XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, _
                         Optional ByVal weight As Microsoft.Office.Interop.Excel.XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                         Optional ByVal index As Microsoft.Office.Interop.Excel.XlBordersIndex = Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop)

        If col_index1 < 0 Then
            col_index1 = LineIndex_S
        End If
        If col_index2 < 0 Then
            col_index2 = LineIndex_E
        End If

        Dim staRangePrefix = ConvertToLetter(col_index1)
        Dim endRangePrefix = ConvertToLetter(col_index2)
        Dim staRange = String.Format("{0}{1}", staRangePrefix, row_index.ToString)
        Dim endRange = String.Format("{0}{1}", endRangePrefix, row_index.ToString)
        Dim range = _sheet.Range(staRange, endRange)
        Dim border = range.Borders(index)
        border.LineStyle = lineStyle
        border.Weight = weight

        System.Runtime.InteropServices.Marshal.ReleaseComObject(range)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(border)

    End Sub

    ''' <summary>
    ''' 二重線を描画
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index1"></param>
    ''' <param name="col_index2"></param>
    ''' <remarks></remarks>
    Public Sub DrawDoubleLine(ByVal row_index As Integer, Optional ByVal col_index1 As Integer = -1, Optional ByVal col_index2 As Integer = -1)
        If col_index1 < 0 Then
            col_index1 = LineIndex_S
        End If
        If col_index2 < 0 Then
            col_index2 = LineIndex_E
        End If
        DrawLine(row_index, col_index1, col_index2, Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick)
    End Sub

    ''' <summary>
    ''' 太線を描画
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index1"></param>
    ''' <param name="col_index2"></param>
    ''' <remarks></remarks>
    Public Sub DrawBoldLine(ByVal row_index As Integer, Optional ByVal col_index1 As Integer = -1, Optional ByVal col_index2 As Integer = -1)
        If col_index1 < 0 Then
            col_index1 = LineIndex_S
        End If
        If col_index2 < 0 Then
            col_index2 = LineIndex_E
        End If
        DrawLine(row_index, col_index1, col_index2, , Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
    End Sub

    ''' <summary>
    ''' 通常の直線を描画
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index1"></param>
    ''' <param name="col_index2"></param>
    ''' <remarks></remarks>
    Public Sub DrawBasicLine(ByVal row_index As Integer, Optional ByVal col_index1 As Integer = -1, Optional ByVal col_index2 As Integer = -1)
        If col_index1 < 0 Then
            col_index1 = LineIndex_S
        End If
        If col_index2 < 0 Then
            col_index2 = LineIndex_E
        End If
        DrawLine(row_index, col_index1, col_index2, , Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
    End Sub

    ''' <summary>
    ''' 破線を描画
    ''' </summary>
    ''' <param name="row_index"></param>
    ''' <param name="col_index1"></param>
    ''' <param name="col_index2"></param>
    ''' <remarks></remarks>
    Public Sub DrawDashLine(ByVal row_index As Integer, Optional ByVal col_index1 As Integer = -1, Optional ByVal col_index2 As Integer = -1)
        If col_index1 < 0 Then
            col_index1 = LineIndex_S
        End If
        If col_index2 < 0 Then
            col_index2 = LineIndex_E
        End If
        DrawLine(row_index, col_index1, col_index2, Microsoft.Office.Interop.Excel.XlLineStyle.xlDash, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
    End Sub

    ''' <summary>
    ''' 列番号を英文字に変換する
    ''' </summary>
    ''' <param name="ColIndex">列番号</param>
    ''' <remarks>アルファベット3文字まで対応</remarks>
    Private Function ConvertToLetter(ByVal ColIndex As Integer) As String
        Dim val As String = String.Empty
        Dim Alpha As Integer
        Dim Beta As Integer
        Dim Remainder As Integer
        Try
            If ColIndex > 26 + (26 * 26) Then
                Alpha = CInt(Int((ColIndex - 26 - 1) / (26 * 26)))
            End If
            Beta = CInt(Int((ColIndex - (Alpha * 26 * 26) - 1) / 26))
            Remainder = ColIndex - (Alpha * 26 * 26) - (Beta * 26)
            If Alpha > 0 Then
                val = Chr(Alpha + 64)
            End If
            If Beta > 0 Then
                val = val & Chr(Beta + 64)
            End If
            If Remainder > 0 Then
                val = val & Chr(Remainder + 64)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return val
    End Function

End Class
