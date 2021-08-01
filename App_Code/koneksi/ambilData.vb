Imports System.Data
Imports System.Data.DataSet

Public Class ambilData

    Dim obyek As New com.ibw.WebService  'joanelib.myMySQL

    Public Sub New()
        '  obyek.Koneksi = Con ' "server=localhost;port=3306;User Id=adminibw;password=Indonesia@333;Persist Security Info=True;database=ibw;SslMode=none"

    End Sub
    Public Function namakolomtanggalstring(kolom As String, Optional date1jam2 As Integer = 1) As String
        Dim hasil, a As String
        hasil = kolom
        ' a = kolom
        If date1jam2 = 1 Then
            ' hasil &= ","
            hasil &= "DATE_FORMAT(" & a & ", '%d-%m-%Y') as " & a
            hasil &= ","
            hasil &= "DATE_FORMAT(" & a & ", '%Y-%m-%d') as " & a & "ymd"
            hasil &= ","
            hasil &= "day(" & a & ") as " & a & "hari"
            hasil &= ","
            hasil &= "month(" & a & ") as " & a & "bulan"
            hasil &= ","
            hasil &= "year(" & a & ") as " & a & "tahun"

        Else
            '  hasil &= ","
            hasil &= "TIME_FORMAT(" & a & ", '%H:%i') as " & a
        End If
        Return hasil
    End Function
    Public Function namakolom(ByVal tabel As String) As String
        Dim namakol As String
        namakol = ambilString("SELECT group_concat(COLUMN_NAME) FROM INFORMATION_SCHEMA.COLUMNS " &
                                    " WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE NOT IN ( 'datetime','time','date')")


        Return namakol 'namakolomlengkap(tabel)
    End Function
    Public Function namakolomtanpaid(tabel1 As String) As String
        ' Dim namakol As String
        Dim skrip As String
        Dim dt As New DataTable
        skrip = "SELECT group_concat(COLUMN_NAME) AS kolom ,'biasa' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel1 & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE NOT IN ( 'datetime','time','date') And COLUMN_NAME <> 'id' " &
" UNION " &
" SELECT group_concat(COLUMN_NAME) AS kolom,'tanggal' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel1 & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE IN ( 'datetime','date')" &
" UNION " &
" SELECT group_concat(COLUMN_NAME) AS kolom,'waktu' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel1 & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE IN ( 'time') "

        ' skrip = " SELECT group_concat(COLUMN_NAME) AS kolom,'tanggal' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE IN ( 'datetime','date')"
        dt = ambilData(skrip)
        Dim hasilnet As String = ""
        For Each row As DataRow In dt.Rows
            If row("keterangan") = "tanggal" Then
                Dim hasil As String = ""
                Dim koloms As String()
                Try
                    koloms = Split(row("kolom"), ",")
                    For Each a As String In koloms
                        hasil &= ","
                        hasil &= "DATE_FORMAT(" & a & ", '%d-%m-%Y') as " & a
                        hasil &= ","
                        hasil &= "DATE_FORMAT(" & a & ", '%Y-%m-%d') as " & a & "ymd"
                        hasil &= ","
                        hasil &= "day(" & a & ") as " & a & "hari"
                        hasil &= ","
                        hasil &= "month(" & a & ") as " & a & "bulan"
                        hasil &= ","
                        hasil &= "year(" & a & ") as " & a & "tahun"
                    Next
                    hasilnet &= ","
                    hasilnet &= Mid(hasil, 2, hasil.Length)
                Catch ex As Exception

                End Try


            ElseIf row("keterangan") = "waktu" Then
                Dim koloms As String()
                Dim hasil As String = ""
                Try
                    koloms = Split(row("kolom"), ",")
                    For Each a As String In koloms
                        hasil &= ","
                        hasil &= "TIME_FORMAT(" & a & ", '%H:%i') as " & a
                    Next
                    hasilnet &= ","
                    hasilnet &= Mid(hasil, 2, hasil.Length)
                Catch ex As Exception

                End Try

            Else
                hasilnet &= ","
                hasilnet &= row("kolom")
            End If



        Next
        hasilnet = Mid(hasilnet, 2, hasilnet.Length)
        Return hasilnet
    End Function
    Public Function namakolomlengkap(tabel As String) As String
        ' Dim namakol As String
        Dim skrip As String
        Dim dt As New DataTable
        skrip = "SELECT group_concat(COLUMN_NAME) AS kolom ,'biasa' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE NOT IN ( 'datetime','time','date') " &
" UNION " &
" SELECT group_concat(COLUMN_NAME) AS kolom,'tanggal' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE IN ( 'datetime','date')" &
" UNION " &
" SELECT group_concat(COLUMN_NAME) AS kolom,'waktu' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE IN ( 'time') "

        ' skrip = " SELECT group_concat(COLUMN_NAME) AS kolom,'tanggal' AS keterangan FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_SCHEMA = 'ibw' AND TABLE_NAME = '" & tabel & "' AND INFORMATION_SCHEMA.COLUMNS.DATA_TYPE IN ( 'datetime','date')"
        dt = ambilData(skrip)
        Dim hasilnet As String = ""
        For Each row As DataRow In dt.Rows
            If row("keterangan") = "tanggal" Then
                Dim hasil As String = ""
                Dim koloms As String()
                Try
                    koloms = Split(row("kolom"), ",")
                    For Each a As String In koloms
                        hasil &= ","
                        hasil &= "DATE_FORMAT(" & a & ", '%d-%m-%Y') as " & a
                        hasil &= ","
                        hasil &= "DATE_FORMAT(" & a & ", '%Y-%m-%d') as " & a & "ymd"
                        hasil &= ","
                        hasil &= "day(" & a & ") as " & a & "hari"
                        hasil &= ","
                        hasil &= "month(" & a & ") as " & a & "bulan"
                        hasil &= ","
                        hasil &= "year(" & a & ") as " & a & "tahun"
                        hasil &= ","
                        hasil &= "concat(DATE_FORMAT(" & a & ",'%Y-%m-%d'), 'T' , TIME_FORMAT(" & a & ",'%H:%i:%s') , 'Z' ) as " & a & "rss"
                    Next
                    hasilnet &= ","
                    hasilnet &= Mid(hasil, 2, hasil.Length)
                Catch ex As Exception

                End Try

            ElseIf row("keterangan") = "waktu" Then
                Try
                    Dim koloms As String()
                    Dim hasil As String = ""
                    koloms = Split(row("kolom"), ",")
                    For Each a As String In koloms
                        hasil &= ","
                        hasil &= "TIME_FORMAT(" & a & ", '%H:%i') as " & a
                    Next
                    hasilnet &= ","
                    hasilnet &= Mid(hasil, 2, hasil.Length)
                Catch ex As Exception
                    hasilnet &= ""
                End Try

            Else
                hasilnet &= ","
                hasilnet &= row("kolom")
            End If



        Next
        hasilnet = Mid(hasilnet, 2, hasilnet.Length)
        Return hasilnet
    End Function
    Public Function kolomtanggal(namakolom As String, Optional labelkolom As String = "") As String
        Dim hasil, a, label As String
        Dim hasilnet As String
        a = namakolom
        If labelkolom = "" Then
            label = namakolom
        Else
            label = labelkolom
        End If
        hasil = ""
        hasil &= "DATE_FORMAT(" & a & ", '%d-%m-%Y') as " & label
        hasil &= ","
        hasil &= "DATE_FORMAT(" & a & ", '%Y-%m-%d') as " & label & "ymd"
        hasil &= ","
        hasil &= "day(" & a & ") as " & label & "hari"
        hasil &= ","
        hasil &= "month(" & a & ") as " & label & "bulan"
        hasil &= ","
        hasil &= "year(" & a & ") as " & label & "tahun"

        Return hasil
    End Function
    Public Function kolomjam(namakolom As String, Optional labelkolom As String = "") As String
        Dim hasil, a, label As String
        Dim hasilnet As String
        a = namakolom
        If labelkolom = "" Then
            label = namakolom
        Else
            label = labelkolom
        End If
        hasil = ""
        hasil &= "TIME_FORMAT(" & a & ", '%H:%i') as " & label
        Return hasil
    End Function
    Public Function ambilData(ByVal skrip As String) As DataTable
        '  Dim obyek As New joanelib.myMySQL

        Return obyek.ambilData(skrip)
    End Function
    Public Function updateData(ByVal skrip As String) As Boolean
        ' Dim obyek As New joanelib.myMySQL
        Return obyek.updateData(skrip)
    End Function
    Public Function ambilString(ByVal skrip As String) As String
        Return obyek.ambilString(skrip)
    End Function
    Public Function ambilStringArray(ByVal skrip As String) As String()
        Return obyek.ambilStringArray(skrip)
    End Function
    Public Function ambilInteger(ByVal skrip As String) As Integer
        Try
            Return Val(obyek.ambilInteger(skrip).ToString)
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function insertBLOB(skrip As String, namaParam4Blob As String, blob As Byte()) As String
        Return obyek.insertBLOB(skrip, namaParam4Blob, blob)
    End Function
    Public Function insertCLOB(skrip As String, namaParam4Blob As String, clob As String) As String
        Return obyek.insertCLOB(skrip, namaParam4Blob, clob)
    End Function
    Public Function ambilImage(ByVal sql As String) As Byte()
        Dim a() As Byte

        ' Dim obyek As New joanelib.myMySQL
        a = obyek.ambilImage(sql)

        Return a
    End Function
End Class
