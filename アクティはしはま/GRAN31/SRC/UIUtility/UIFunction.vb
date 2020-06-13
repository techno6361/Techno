Imports Techno.DataBase

Public Class UIFunction

#Region "パソコンをシャットダウンするために使う関数"
    <System.Runtime.InteropServices.DllImport("kernel32.dll")> _
    Private Shared Function GetCurrentProcess() As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError:=True)> _
    Shared Function OpenProcessToken(ByVal ProcessHandle As IntPtr, _
        ByVal DesiredAccess As Integer, _
        ByRef TokenHandle As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError:=True, _
        CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
    Shared Function LookupPrivilegeValue(ByVal lpSystemName As String, _
        ByVal lpName As String, _
        ByRef lpLuid As Long) As Boolean
    End Function

    <System.Runtime.InteropServices.StructLayout( _
        System.Runtime.InteropServices.LayoutKind.Sequential, Pack:=1)> _
    Private Structure TOKEN_PRIVILEGES
        Public PrivilegeCount As Integer
        Public Luid As Long
        Public Attributes As Integer
    End Structure

    <System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError:=True)> _
    Private Shared Function AdjustTokenPrivileges(ByVal TokenHandle As IntPtr, _
        ByVal DisableAllPrivileges As Boolean, _
        ByRef NewState As TOKEN_PRIVILEGES, _
        ByVal BufferLength As Integer, _
        ByVal PreviousState As IntPtr, _
        ByVal ReturnLength As IntPtr) As Boolean
    End Function

    'シャットダウンするためのセキュリティ特権を有効にする
    Public Shared Sub AdjustToken()
        Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
        Const TOKEN_QUERY As Integer = &H8
        Const SE_PRIVILEGE_ENABLED As Integer = &H2
        Const SE_SHUTDOWN_NAME As String = "SeShutdownPrivilege"

        If Environment.OSVersion.Platform <> PlatformID.Win32NT Then
            Return
        End If

        Dim procHandle As IntPtr = GetCurrentProcess()

        'トークンを取得する
        Dim tokenHandle As IntPtr
        OpenProcessToken(procHandle, _
            TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, tokenHandle)
        'LUIDを取得する
        Dim tp As New TOKEN_PRIVILEGES()
        tp.Attributes = SE_PRIVILEGE_ENABLED
        tp.PrivilegeCount = 1
        LookupPrivilegeValue(Nothing, SE_SHUTDOWN_NAME, tp.Luid)
        '特権を有効にする
        AdjustTokenPrivileges(tokenHandle, False, tp, 0, IntPtr.Zero, IntPtr.Zero)

    End Sub

#End Region

    ''' <summary>
    ''' 未入力ならNULLを返す
    ''' </summary>
    ''' <param name="strMoji"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NullCheck(strMoji As String) As String
        Dim strRtnMoji As String = "NULL"
        Try
            If Not String.IsNullOrEmpty(strMoji) Then
                strRtnMoji = "'" & strMoji & "'"
            End If

            Return strRtnMoji

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 年齢計算
    ''' </summary>
    ''' <param name="start">生年月日</param>
    ''' <param name="last">本日日付</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAge(ByVal start As Date, ByVal last As Date) As Integer
        Try
            If ((start.Month * 100 + start.Day) <= (last.Month * 100 + last.Day)) Then
                Return DateDiff(DateInterval.Year, start, last)
            Else
                Return DateDiff(DateInterval.Year, start, last) - 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 1球単価消費税金額取得
    ''' </summary>
    ''' <param name="intTanka">1球単価</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTaxTanka(ByVal intTanka As Integer) As Double
        Dim dcTaxKin As Decimal = 0
        Try
            If UIUtility.SYSTEM.TAMATAX.Equals(0) Then Return 0
            If intTanka.Equals(0) Then Return 0

            dcTaxKin = intTanka * (UIUtility.SYSTEM.TAMATAX / 100)

            Return dcTaxKin

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 残金整合処理
    ''' </summary>
    ''' <param name="intV31ZANKN"></param>
    ''' <param name="intV31PREZANKN"></param>
    ''' <param name="intKINGAKU">プリカ金額</param>
    ''' <param name="intUseKINGAKU">使用球数金額</param>
    ''' <param name="intPayZANKN">支払残金</param>
    ''' <param name="intPayPREMKN">支払プレミアム</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ZANSEIGO(ByRef intV31ZANKN As Integer, ByRef intV31PREZANKN As Integer, ByVal intKINGAKU As Integer, ByRef intUseKINGAKU As Integer, ByRef intPayZANKN As Integer, ByRef intPayPREMKN As Integer) As Boolean
        Try
            If intKINGAKU > (intV31ZANKN + intV31PREZANKN) Then
                '【リライトよりプリカ金額の方が大きいのはありえない為リライト金額優先】
                Return False
            End If

            '*** 【プリカ金額とV31金額に差額がある場合の処理】***'
            intUseKINGAKU = (intV31ZANKN + intV31PREZANKN) - intKINGAKU
            If Not intUseKINGAKU.Equals(0) Then
                If intV31PREZANKN >= intUseKINGAKU Then
                    '【残プレミアム >= 入場料金】
                    intV31PREZANKN -= intUseKINGAKU

                    intPayZANKN = 0
                    intPayPREMKN = intUseKINGAKU
                Else
                    intPayZANKN = intUseKINGAKU - intV31PREZANKN
                    intPayPREMKN = intV31PREZANKN

                    intV31PREZANKN = 0
                    intV31ZANKN = intV31ZANKN - (intUseKINGAKU - intPayPREMKN)

                End If
            End If
            '****************************************************'

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 本日入場回数取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetINCNT(ByVal strMANNO As String, ByVal iDatabase As IDatabase.IMethod) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND MANNO = '" & strMANNO & "'")
            sql.Append(" AND OUTFLG = 0")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return resultDt.Rows.Count

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 本日退場回数取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOUTCNT(ByVal strMANNO As String, ByVal iDatabase As IDatabase.IMethod) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND MANNO = '" & strMANNO & "'")
            sql.Append(" AND OUTFLG = 1")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return resultDt.Rows.Count

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 退場更新伝票番号取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMaxENTNO(ByVal strMANNO As String, ByVal iDatabase As IDatabase.IMethod) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" MAX(ENTNO) AS ENTNO")
            sql.Append(" FROM ENTTRA")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND MANNO = '" & strMANNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If String.IsNullOrEmpty(resultDt.Rows(0).Item("ENTNO").ToString) Then
                Return 0
            Else
                Return CType(resultDt.Rows(0).Item("ENTNO").ToString, Integer)
            End If

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 前回入金額取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetZENNKNKN(ByVal strMANNO As String, ByVal iDatabase As IDatabase.IMethod) As String
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" NKNKN")
            sql.Append(" FROM NKNTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND STSFLG = '0'")
            sql.Append(" AND NKNKN > 0")
            sql.Append(" AND MANNO = '" & strMANNO & "'")
            sql.Append(" ORDER BY INSDTM DESC")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return 0
            End If

            Return CType(resultDt.Rows(0).Item("NKNKN").ToString, Integer).ToString("#,##0")

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' テロップテーブル取得
    ''' </summary>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDTELOP(ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DTELOP")
            Dim dt As DataTable = iDatabase.ExecuteRead(sql.ToString())
            If dt.Rows.Count <= 0 Then
                UIUtility.SYSTEM.TELOP = String.Empty
                UIUtility.SYSTEM.COMMENT = String.Empty
                UIUtility.SYSTEM.FCOMMENT = String.Empty
                Return False
            End If
            UIUtility.SYSTEM.TELOP = dt.Rows(0).Item("TELOP").ToString()
            UIUtility.SYSTEM.COMMENT = dt.Rows(0).Item("COMMENT").ToString()
            UIUtility.SYSTEM.FCOMMENT = dt.Rows(0).Item("FCOMMENT").ToString()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' カードコメント取得
    ''' </summary>
    ''' <param name="strNCSNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCARDCOMMENT(ByVal strNCSNO As String, ByVal iDatabase As IDatabase.IMethod) As String
        Dim sql As New System.Text.StringBuilder
        Dim strCOMMENT As String = String.Empty
        Try
            If CType(strNCSNO, Integer) >= 50000000 Then
                '【ﾌﾘｰｶｰﾄﾞ】
                Return UIUtility.SYSTEM.FCOMMENT
            End If

            sql.Append(" SELECT ")
            sql.Append(" MANINFO")
            sql.Append(" FROM CSMAST")
            sql.Append(" WHERE NCSNO = " & CType(strNCSNO, Integer))

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return String.Empty
            End If

            If Not String.IsNullOrEmpty(resultDt.Rows(0).Item("MANINFO").ToString) Then
                strCOMMENT = resultDt.Rows(0).Item("MANINFO").ToString
            Else
                strCOMMENT = UIUtility.SYSTEM.COMMENT
            End If

            Return strCOMMENT

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    ''' <summary>
    ''' カード停止確認
    ''' </summary>
    ''' <param name="strNCARDID"></param>
    ''' <param name="strNCSNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChkDCSTPTRN(ByVal strNCARDID As String, ByVal strNCSNO As String, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DCSTPTRN")
            sql.Append(" WHERE NCSNO = " & CType(strNCSNO, Integer))
            sql.Append(" AND NCARDID = " & CType(strNCARDID, Integer))

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 入金時残高取得
    ''' </summary>
    ''' <param name="intZANKINGAKU"></param>
    ''' <param name="intZANPOINT"></param>
    ''' <param name="strDENDT"></param>
    ''' <param name="intDENNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTRNZANKIN(ByRef intZANKINGAKU As Integer, ByRef intZANPOINT As Integer, ByVal strDENDT As String, ByVal intDENNO As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNTRN")
            sql.Append(" WHERE DENDT = '" & strDENDT & "'")
            sql.Append(" AND DENNO = " & intDENNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt.Rows.Count - 1
                intZANKINGAKU = CType(resultDt.Rows(0).Item("ZANBKN"), Integer) + CType(resultDt.Rows(0).Item("PREZANBKN"), Integer)
                intZANPOINT = CType(resultDt.Rows(0).Item("ZANBPO"), Integer)
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ポイント還元時残高取得
    ''' </summary>
    ''' <param name="intZANKINGAKU"></param>
    ''' <param name="intZANPOINT"></param>
    ''' <param name="strDENDT"></param>
    ''' <param name="intDENNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTRNZANKIN2(ByRef intZANKINGAKU As Integer, ByRef intZANPOINT As Integer, ByVal strDENDT As String, ByVal intDENNO As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM DREPOTRN")
            sql.Append(" WHERE DENDT = '" & strDENDT & "'")
            sql.Append(" AND DENNO = " & intDENNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt.Rows.Count - 1
                intZANKINGAKU = CType(resultDt.Rows(0).Item("ZANBKN"), Integer) + CType(resultDt.Rows(0).Item("PREZANBKN"), Integer)
                intZANPOINT = CType(resultDt.Rows(0).Item("ZANBPO"), Integer)
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 商品引落し時残高取得
    ''' </summary>
    ''' <param name="intZANKINGAKU"></param>
    ''' <param name="intZANPOINT"></param>
    ''' <param name="strDENDT"></param>
    ''' <param name="intDENNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTRNZANKIN3(ByRef intZANKINGAKU As Integer, ByRef intZANPOINT As Integer, ByVal strDENDT As String, ByVal intDENNO As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM HINTRN")
            sql.Append(" WHERE DENDT = '" & strDENDT & "'")
            sql.Append(" AND DENNO = " & intDENNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt.Rows.Count - 1
                intZANKINGAKU = CType(resultDt.Rows(0).Item("ZANBKN"), Integer) + CType(resultDt.Rows(0).Item("PREZANBKN"), Integer)
                intZANPOINT = CType(resultDt.Rows(0).Item("ZANBPO"), Integer)
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' コース清算時残高取得
    ''' </summary>
    ''' <param name="intZANKINGAKU"></param>
    ''' <param name="intZANPOINT"></param>
    ''' <param name="strDENDT"></param>
    ''' <param name="intDENNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTRNZANKIN4(ByRef intZANKINGAKU As Integer, ByRef intZANPOINT As Integer, ByVal strDENDT As String, ByVal intDENNO As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE DENDT = '" & strDENDT & "'")
            sql.Append(" AND DENNO = " & intDENNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            For i As Integer = 0 To resultDt.Rows.Count - 1
                intZANKINGAKU = CType(resultDt.Rows(0).Item("ZANBKN"), Integer) + CType(resultDt.Rows(0).Item("PREZANBKN"), Integer)
                intZANPOINT = CType(resultDt.Rows(0).Item("ZANBPO"), Integer)
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 誕生月ｻｰﾋﾞｽ入金額確認
    ''' </summary>
    ''' <param name="intNKBNO"></param>
    ''' <param name="intPREMKN"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChkBirthPREMKN(ByVal intNKBNO As Integer, ByRef intPREMKN As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM NKNMST")
            sql.Append(" WHERE STSFLG = '9'")
            sql.Append(" AND NKNKBN = '004'")
            sql.Append(" AND SEQNO = " & intNKBNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                intPREMKN = 0
                Return False
            End If

            intPREMKN = CType(resultDt.Rows(0).Item("NKNKN"), Integer)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 誕生月サービス入金取得確認
    ''' </summary>
    ''' <param name="strMANNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChkGetBirthPREMKN(ByVal strMANNO As String, ByVal intKBN As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" DENDT")
            sql.Append(" FROM NKNTRN")
            sql.Append(" WHERE DATKB = '1'")
            If intKBN.Equals(0) Then
                sql.Append(" AND (DENDT >= '" & Now.Year & "0101' AND DENDT <= '" & Now.Year & "1231'" & ")")
            Else
                If Now.ToString("MM").Equals("01") Then
                    sql.Append(" AND (DENDT >= '" & (CType(Now.Year.ToString, Integer) - 1) & "0101' AND DENDT <= '" & (CType(Now.Year.ToString, Integer) - 1) & "1231'" & ")")
                Else
                    sql.Append(" AND (DENDT >= '" & Now.Year & "0101' AND DENDT <= '" & Now.Year & "1231'" & ")")
                End If
            End If
            sql.Append(" AND STSFLG = '9'")
            sql.Append(" AND PRERT = 1")
            sql.Append(" AND MANNO = '" & strMANNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 本日打席利用ﾁｪｯｸ
    ''' </summary>
    ''' <param name="strMANNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChkTodayPlay(ByVal strMANNO As String, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" CASE WHEN SUM(USEKIN) IS NULL THEN 0 ELSE SUM(USEKIN) END AS USEKIN")
            sql.Append(" FROM BALLTRN")
            sql.Append(" WHERE UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND NCSNO = " & CType(strMANNO, Integer))

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            If CType(resultDt.Rows(0).Item("USEKIN"), Integer).Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ポイントマスタ情報取得
    ''' </summary>
    ''' <param name="intBIRTHMPO">誕生月ポイント</param>
    ''' <param name="intBIRTHDPO">誕生日ﾎﾟｲﾝﾄ</param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPOINTMST(ByRef intBIRTHMPO As Integer, ByRef intBIRTHDPO As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM POINTMST")
            sql.Append(" WHERE BMNCD = '002'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            For i As Integer = 0 To resultDt.Rows.Count - 1
                Select Case resultDt.Rows(i).Item("POINTNO").ToString
                    Case "002"  '誕生日ﾎﾟｲﾝﾄ
                        intBIRTHDPO = CType(resultDt.Rows(i).Item("POINT").ToString, Integer)
                    Case "003"  '誕生月ﾎﾟｲﾝﾄ
                        intBIRTHMPO = CType(resultDt.Rows(i).Item("POINT").ToString, Integer)
                End Select
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 画面セキュリティチェック
    ''' </summary>
    ''' <param name="strPRGID"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChkPgScrty(ByVal strPRGID As String, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM PRGMTA")
            sql.Append(" WHERE ")
            sql.Append(" PRGID = '" & strPRGID & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            If resultDt.Rows.Count.Equals(0) Then
                Return False
            End If

            If CType(resultDt.Rows(0).Item("SCRTYKBN"), Integer).Equals(0) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 会員期限更新
    ''' </summary>
    ''' <param name="strNCSNO"></param>
    ''' <param name="strUpdDMEMBER"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdDMEMBER(ByVal strNCSNO As String, ByVal strUpdDMEMBER As String, ByVal iDatabase As IDatabase.IMethod, Optional ByVal intNCSRANK As Integer = 0) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Clear()
            sql.Append("UPDATE CSMAST SET")
            sql.Append(" DMEMBER = '" & strUpdDMEMBER & "'")
            If intNCSRANK > 0 Then
                sql.Append(",NCSRANK = " & intNCSRANK)
            End If
            sql.Append(",DUPDATE = NOW()")
            sql.Append(" WHERE NCSNO = " & CType(strNCSNO, Integer))

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 伝票情報修正
    ''' </summary>
    ''' <param name="intKBN">【1】商品【3】入金【4】サービス入金【5】入場【6】コース料金【7】ポイント還元 </param>
    ''' <param name="intDENNO"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdBackDATKB(ByVal intKBN As Integer, ByVal intDENNO As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try
            iDatabase.BeginTransaction()

            sql.Clear()
            sql.Append("UPDATE DUDNTRN SET")
            sql.Append(" DATKB = '1'")
            sql.Append(" WHERE  UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND UDNNO = " & intDENNO)

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            sql.Clear()
            sql.Append("UPDATE DENTRA SET")
            sql.Append(" DATKB = '1'")
            sql.Append(" WHERE  UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND UDNNO = " & intDENNO)

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            Select Case intKBN
                Case 1  '商品引落し
                    sql.Clear()
                    sql.Append("UPDATE HINTRN SET")
                    sql.Append(" DATKB = '1'")
                    sql.Append(" WHERE  DENDT = '" & Now.ToString("yyyyMMdd") & "'")
                    sql.Append(" AND DENNO = " & intDENNO)

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If

                Case 3, 4  '入金,ｻｰﾋﾞｽ入金
                    sql.Clear()
                    sql.Append("UPDATE NKNTRN SET")
                    sql.Append(" DATKB = '1'")
                    sql.Append(" WHERE  DENDT = '" & Now.ToString("yyyyMMdd") & "'")
                    sql.Append(" AND DENNO = " & intDENNO)

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                Case 5  '入場取消
                    sql.Clear()
                    sql.Append("UPDATE ENTTRA SET")
                    sql.Append(" DATKB = '1'")
                    sql.Append(" WHERE  ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
                    sql.Append(" AND ENTNO = " & intDENNO)

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                Case 6  'コース料金
                    sql.Clear()
                    sql.Append("UPDATE KOSUTRN SET")
                    sql.Append(" DATKB = '1'")
                    sql.Append(" WHERE  DENDT = '" & Now.ToString("yyyyMMdd") & "'")
                    sql.Append(" AND DENNO = " & intDENNO)

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If
                Case 7  'ポイント還元
                    sql.Clear()
                    sql.Append("UPDATE DREPOTRN SET")
                    sql.Append(" DATKB = '1'")
                    sql.Append(" WHERE  DENDT = '" & Now.ToString("yyyyMMdd") & "'")
                    sql.Append(" AND DENNO = " & intDENNO)

                    If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                        iDatabase.RollBack()
                        Return False
                    End If
            End Select

            iDatabase.Commit()

            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 退場情報更新
    ''' </summary>
    ''' <param name="intENTNO"></param>
    ''' <param name="intPOINT"></param>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdOutInfo(ByVal intENTNO As Integer, ByVal intPOINT As Integer, ByVal iDatabase As IDatabase.IMethod) As Boolean
        Dim sql As New System.Text.StringBuilder
        Try

            sql.Clear()
            sql.Append("UPDATE ENTTRA SET")
            sql.Append(" SRTPO = SRTPO +" & intPOINT)
            sql.Append(" ,OUTPO = " & intPOINT)
            sql.Append(" ,OUTFLG = 1")
            sql.Append(" ,ZANBPO = ZANBPO + " & intPOINT)
            sql.Append(" ,UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" ENTDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND ENTNO =" & intENTNO)

            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If

            If Not intPOINT.Equals(0) Then
                sql.Clear()
                sql.Append("UPDATE DUDNTRN SET")
                sql.Append(" POINT = POINT +" & intPOINT)
                sql.Append(" WHERE")
                sql.Append(" UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
                sql.Append(" AND UDNNO =" & intENTNO)

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If

                sql.Clear()
                sql.Append("UPDATE DENTRA SET")
                sql.Append(" POINT = POINT +" & intPOINT)
                sql.Append(" WHERE")
                sql.Append(" UDNDT = '" & Now.ToString("yyyyMMdd") & "'")
                sql.Append(" AND UDNNO =" & intENTNO)

                If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                    iDatabase.RollBack()
                    Return False
                End If
            End If



            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' 利用ボール金額・球数更新
    ''' </summary>
    ''' <param name="intNCSNO">顧客番号</param>
    ''' <param name="strNKBNO">顧客種別</param>
    ''' <param name="strUDNDT">前回来場日</param>
    ''' <param name="intUSEKIN">使用球数金額</param>
    ''' <param name="intPayBallZANKN">支払残金額</param>
    ''' <param name="intPayBallPREMKN">支払ﾌﾟﾚﾐｱﾑ</param>
    ''' <param name="intBALLSU">ﾎﾞｰﾙ数</param>
    ''' <param name="intZANAKN">残金額処理前</param>
    ''' <param name="intZANBKN">残金額処理後</param>
    ''' <param name="intPREZANAKN">P)残金額処理前</param>
    ''' <param name="intPREZANBKN">P)残金額処理後</param>
    ''' <param name="intZANAPO">残ポイント処理前</param>
    ''' <param name="intZANBPO">残ポイント処理後</param>
    ''' <param name="iDatabase">データベース</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdBALLTRN(ByVal intNCSNO As Integer, ByVal strNKBNO As String _
                                    , ByVal strUDNDT As String, ByVal intUSEKIN As Integer _
                                    , ByVal intPayBallZANKN As Integer, ByVal intPayBallPREMKN As Integer _
                                    , ByVal intBALLSU As Integer _
                                    , ByVal intZANAKN As Integer _
                                    , ByVal intZANBKN As Integer _
                                    , ByVal intPREZANAKN As Integer _
                                    , ByVal intPREZANBKN As Integer _
                                    , ByVal intZANAPO As Integer _
                                    , ByVal intZANBPO As Integer _
                                    , ByVal iDatabase As IDatabase.IMethod _
                                    , Optional ByVal strINSDTM As String = "" _
                                    , Optional ByRef intLENNO As Integer = 0) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Dim intNKBNO As Integer = 0
        Try

            Select Case strNKBNO
                Case "A"
                    intNKBNO = 10
                Case "B"
                    intNKBNO = 11
                Case "C"
                    intNKBNO = 12
                Case "D"
                    intNKBNO = 13
                Case "E"
                    intNKBNO = 14
                Case "F"
                    intNKBNO = 15
                Case Else
                    intNKBNO = CType(strNKBNO, Integer)
            End Select

            sql.Clear()
            sql.Append("SELECT")
            sql.Append(" *")
            sql.Append(" FROM")
            sql.Append(" BALLTRN")
            sql.Append(" WHERE")
            sql.Append(" UDNDT = '" & strUDNDT & "'")
            sql.Append(" AND NCSNO = " & intNCSNO)

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            strSQL1 = String.Empty
            strSQL2 = String.Empty
            strSQL1 &= "INSERT INTO BALLTRN("
            strSQL2 &= " VALUES("
            '営業日付
            strSQL1 &= "UDNDT,"
            strSQL2 &= "'" & strUDNDT & "',"
            '顧客番号
            strSQL1 &= "NCSNO,"
            strSQL2 &= intNCSNO & ","
            '連番
            strSQL1 &= "LENNO,"
            strSQL2 &= resultDt.Rows.Count + 1 & ","
            intLENNO = resultDt.Rows.Count + 1
            '顧客種別
            strSQL1 &= "NKBNO,"
            strSQL2 &= intNKBNO & ","


            '利用球数
            strSQL1 &= "USEBALL,"
            strSQL2 &= intBALLSU & ","
            '利用球数金額
            strSQL1 &= "USEKIN,"
            strSQL2 &= intUSEKIN & ","
            '消費税金額
            strSQL1 &= "TAXKIN,"
            strSQL2 &= UIFunction.CalcExcludedTax(intUSEKIN) & ","
            '消費税率
            strSQL1 &= "TAX,"
            strSQL2 &= UIUtility.SYSTEM.TAMATAX & ","
            '支払金額
            strSQL1 &= "PAYZANKN,"
            strSQL2 &= intPayBallZANKN & ","
            '支払ﾌﾟﾚﾐｱﾑ
            strSQL1 &= "PAYPREMKN,"
            strSQL2 &= intPayBallPREMKN & ","
            '残金額処理前
            strSQL1 &= "ZANAKN,"
            strSQL2 &= intZANAKN & ","
            '残金額処理後
            strSQL1 &= "ZANBKN,"
            strSQL2 &= intZANBKN & ","
            'P)残金額処理前
            strSQL1 &= "PREZANAKN,"
            strSQL2 &= intPREZANAKN & ","
            'P)残金額処理後
            strSQL1 &= "PREZANBKN,"
            strSQL2 &= intPREZANBKN & ","
            '残ポイント処理前
            strSQL1 &= "ZANAPO,"
            strSQL2 &= intZANAPO & ","
            '残ポイント処理後
            strSQL1 &= "ZANBPO,"
            strSQL2 &= intZANBPO & ","
            If strINSDTM.Equals("") Then
                '作成日時
                strSQL1 &= "INSDTM,"
                strSQL2 &= "NOW(),"
                '更新日時
                strSQL1 &= "UPDDTM)"
                strSQL2 &= "NOW())"
            Else
                '作成日時
                strSQL1 &= "INSDTM,"
                strSQL2 &= "'" & strINSDTM & "',"
                '更新日時
                strSQL1 &= "UPDDTM)"
                strSQL2 &= "'" & strINSDTM & "')"
            End If


            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' *** Sta Add 2018/04/09 TERAYAMA

    Enum CalcTaxType
        ROUND   ' 四捨五入
        FLOOR   ' 切り捨て
        CEILING ' 切り上げ
    End Enum

    ''' <summary>
    ''' 外税計算(切り上げ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcExcludedTax(ByVal price As Integer) As Integer
        Dim rate As Double = UIUtility.SYSTEM.TAMATAX * 0.01
        Dim value = price / (1 + rate) * rate
        Select Case UIUtility.SYSTEM.TAXHASUKBN
            Case CalcTaxType.FLOOR
                ' 切り捨て
                Return CInt(Math.Floor(value))
            Case CalcTaxType.CEILING
                ' 切り上げ
                Return CInt(Math.Ceiling(value))
            Case Else
                ' 四捨五入
                Return CInt(Math.Round(value))
        End Select
    End Function

    ' End Add

    ' *** STA ADD 20181221 TERAYAMA 金額補正機能の追加

    Public Enum eChkCardKingakuType
        vbDBNOTHING ' 金額データが存在しない
        vbERROR     ' 矛盾あり
        vbNONE      ' 問題なし
    End Enum

    ''' <summary>
    ''' DB金額情報とカード金額の矛盾を修正
    ''' </summary>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ChkCardKingaku(ByVal iDatabase As IDatabase.IMethod, ByVal dcICR700 As Techno.DeviceControls.ICR700Control) As eChkCardKingakuType
        Dim sql As New System.Text.StringBuilder
        Try
            '顧客情報取得
            sql.Clear()
            sql.Append(" SELECT ")
            sql.Append(" A.*")
            sql.Append(",B.ZANKN")
            sql.Append(",B.PREZANKN")
            sql.Append(" FROM CSMAST AS A")
            sql.Append(" LEFT JOIN KINSMA AS B ON B.MANNO = LTRIM(TO_CHAR(A.NCSNO,'00000000'))")
            sql.Append(" WHERE")
            sql.Append(" A.NCARDID = " & CType(dcICR700.NCSNO, Integer))

            Dim dtCSMAST = iDatabase.ExecuteRead(sql.ToString())

            If dtCSMAST.Rows.Count.Equals(0) Then
                Return eChkCardKingakuType.vbDBNOTHING
            End If

            Dim dbZankn = CType(dtCSMAST.Rows(0).Item("ZANKN"), Integer)
            Dim dbPrezankn = CType(dtCSMAST.Rows(0).Item("PREZANKN"), Integer)

            Dim cZankn = CInt(dcICR700.ZANKN)
            Dim cPrezankn = CInt(dcICR700.PREZANKN)

            Dim blnZanknErr = dbZankn <> cZankn
            Dim blnPrezanknErr = dbPrezankn <> cPrezankn

            If blnZanknErr Or blnPrezanknErr Then
                Return eChkCardKingakuType.vbERROR
            End If

            Return eChkCardKingakuType.vbNONE

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' DB金額情報とカード金額の矛盾を修正
    ''' </summary>
    ''' <param name="iDatabase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateKINSMAFromCard(ByVal iDatabase As IDatabase.IMethod, ByVal dcICR700 As Techno.DeviceControls.ICR700Control) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim strSQL1 As String = String.Empty
        Dim strSQL2 As String = String.Empty
        Try
            Dim cZankn = CInt(dcICR700.ZANKN)
            Dim cPrezankn = CInt(dcICR700.PREZANKN)

            sql.Clear()
            sql.Append("SELECT * FROM CSMAST WHERE")
            sql.Append(" NCARDID = " & CType(dcICR700.NCSNO, Integer))
            Dim dtCSMAST = iDatabase.ExecuteRead(sql.ToString())

            If dtCSMAST.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim manno As String = dtCSMAST.Rows(0).Item("NCSNO").ToString.PadLeft(8, "0"c)


            sql.Clear()
            sql.Append("SELECT * FROM KINSMA")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & manno & "'")
            Dim dtKINSMA = iDatabase.ExecuteRead(sql.ToString())

            If dtKINSMA.Rows.Count.Equals(0) Then
                Return False
            End If

            Dim dbZankn = CType(dtKINSMA.Rows(0).Item("ZANKN"), Integer)
            Dim dbPrezankn = CType(dtKINSMA.Rows(0).Item("PREZANKN"), Integer)

            iDatabase.BeginTransaction()

            '残高修正トラン更新
            strSQL1 &= "INSERT INTO CRTTRN("
            strSQL2 &= " VALUES("
            '修正日
            strSQL1 &= "CRTDT,"
            strSQL2 &= "'" & Now.ToString("yyyyMMdd") & "',"
            '修正時間
            strSQL1 &= "CRTTIME,"
            strSQL2 &= "'" & Now.Hour.ToString.PadLeft(2, "0"c) & Now.Minute.ToString.PadLeft(2, "0"c) & "',"
            '顧客番号
            strSQL1 &= "NCSNO,"
            strSQL2 &= manno & ","
            '修正後残金
            strSQL1 &= "ZANKN,"
            strSQL2 &= cZankn & ","
            '修正後残プレミアム
            strSQL1 &= "PREZANKN,"
            strSQL2 &= cPrezankn & ","
            '修正後残ポイント
            strSQL1 &= "SRTPO,"
            strSQL2 &= CType(dcICR700.POINT, Integer) & ","
            '修正前残金
            strSQL1 &= "MAEZANKN,"
            strSQL2 &= dbZankn & ","
            '修正前残プレミアム
            strSQL1 &= "MAEPREZANKN,"
            strSQL2 &= dbPrezankn & ","
            '修正前残ポイント
            strSQL1 &= "MAESRTPO,"
            strSQL2 &= CType(dcICR700.POINT, Integer) & ","
            '修正区分
            strSQL1 &= "CRTKBN,"
            strSQL2 &= "0,"
            'スタッフコード
            strSQL1 &= "STFCODE,"
            strSQL2 &= "NULL,"
            'スタッフ名
            strSQL1 &= "STFNAME,"
            strSQL2 &= "NULL,"
            '作成日時
            strSQL1 &= "INSDTM)"
            strSQL2 &= "NOW())"

            If Not iDatabase.ExecuteUpdate(strSQL1 & strSQL2) Then
                iDatabase.RollBack()
                Return False
            End If

            ' 金額サマリ更新
            sql.Clear()
            sql.Append("UPDATE KINSMA SET")
            sql.Append(" ZANKN =" & cZankn)
            sql.Append(",PREZANKN =" & cPrezankn)
            sql.Append(",UPDDTM = NOW()")
            sql.Append(" WHERE")
            sql.Append(" MANNO = '" & manno & "'")
            If Not iDatabase.ExecuteUpdate(sql.ToString) Then
                iDatabase.RollBack()
                Return False
            End If
            iDatabase.Commit()
            Return True

        Catch ex As Exception
            iDatabase.RollBack()
            Return False

        End Try
    End Function

    ' *** END ADD

    ''' <summary>
    ''' 本日コース入場回数取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetKosuEnt(ByVal strMANNO As String, ByVal iDatabase As IDatabase.IMethod) As Integer
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append(" SELECT ")
            sql.Append(" *")
            sql.Append(" FROM KOSUTRN")
            sql.Append(" WHERE")
            sql.Append(" DATKB = '1'")
            sql.Append(" AND DENDT = '" & Now.ToString("yyyyMMdd") & "'")
            sql.Append(" AND MANNO = '" & strMANNO & "'")

            Dim resultDt As DataTable = iDatabase.ExecuteRead(sql.ToString())

            Return resultDt.Rows.Count

        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

End Class
