Imports Microsoft.VisualBasic

Public Class Operasional
    Inherits joanelib.operasional
    Public Overloads Function CreateSqlSelect(koleksi As Collection, namatabel As String) As String
        Dim hasil, namakolom As String
        Dim j As New ambilData

        hasil = joanelib.operasional.createSqlSelect(koleksi, namatabel)
        namakolom = j.namakolomlengkap(namatabel)
        hasil = Replace(hasil, "*", namakolom)
        Return hasil
    End Function
    Public Overloads Function CreateSqlUpdate(koleksi As Collection, namatabel As String, skripWhere As String) As String
        Return joanelib.operasional.createSqlUpdate(koleksi, namatabel, skripWhere)
    End Function
    Public Overloads Function CreateSqlDelete(koleksi As Collection, namatabel As String, skripWhere As String) As String
        Return joanelib.operasional.createSqlDelete(koleksi, namatabel)
    End Function
    Public Overloads Function CreateSqlInsert(koleksi As Collection, namatabel As String) As String
        Return joanelib.operasional.createSqlInsert(koleksi, namatabel)
    End Function
    Public Overloads Function CreateSqlUpdate(ByVal Array2DimensiRowValue As Collection, ByVal namatabel As String, ByVal ArraywHERE2DimensiRowValue As Collection) As String
        Return joanelib.operasional.createSqlUpdate(Array2DimensiRowValue, namatabel, ArraywHERE2DimensiRowValue)
    End Function
    Public Overloads Function CreateSqlDelete(ByVal ArraywHERE2DimensiRowValue As Collection, ByVal namatabel As String) As String
        Return joanelib.operasional.createSqlDelete(ArraywHERE2DimensiRowValue, namatabel)
    End Function
    Public Function ArrayTeks(ByVal namaKol As String, ByVal nilai As String) As String(,)
        Return joanelib.operasional.bikinArrayTeks(namaKol, nilai)
    End Function
    Public Function ArrayNonTeks(ByVal namaKol As String, ByVal nilai As String) As String(,)
        Return joanelib.operasional.bikinArrayNonTeks(namaKol, nilai)
    End Function
    Public Overloads Function TanggalSekarang() As String

        Return joanelib.operasional.tanggalSekarang
    End Function
    Public Overloads Function TanggalKemarin() As String
        Return joanelib.operasional.tanggalKemarin

    End Function
    Public Overloads Function FormatTanggal(tanggal As Date) As String
        Return joanelib.operasional.formatTanggal(tanggal)
    End Function
    Public Function TampilErrorWeb(ex As Exception) As String
        Return joanelib.operasionalWeb.tampilErrorWeb(ex)
    End Function
    Public Function TanggalMySql(dd_mm_yyyy As String) As String
        Dim a As String
        a = Trim(dd_mm_yyyy)

        Return "date('" & Mid(a, 7, 4) & "-" & Mid(a, 4, 2) & "-" & Mid(a, 1, 2) & "')"
    End Function
End Class
