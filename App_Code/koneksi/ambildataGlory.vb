Imports System.Data
Imports System.Data.DataSet

Public Class ambilDataGlory
    Public Property Con As String = "gloryman_Indonesia@123_67.227.183.115_glory_3306"
    Dim obyek As New joanelib.myMySQL

    Public Sub New()
        obyek.Koneksi = Con ' "server=localhost;port=3306;User Id=adminibw;password=Indonesia@333;Persist Security Info=True;database=ibw;SslMode=none"

    End Sub
    Public Function koneksi() As String
        Return obyek.Koneksi
    End Function
    Public Function ambilData(ByVal skrip As String) As DataTable
        '  Dim obyek As New joanelib.myMySQL

        Return obyek.ambilData(skrip, Con)
    End Function
    Public Function updateData(ByVal skrip As String) As Boolean
        Dim obyek As New joanelib.myMySQL
        Return obyek.executeData(skrip, Con)
    End Function
    Public Function ambilString(ByVal skrip As String) As String
        Return obyek.ambilsingleData(skrip, Con)
    End Function

    Public Function ambilStringArray(ByVal skrip As String) As String()
        Return obyek.ambilarrayData(skrip, Con)
    End Function
    Public Function ambilInteger(ByVal skrip As String) As Integer
        Return obyek.ambilsingleDataNumber(skrip, Con)
    End Function

    Public Function insertBLOB(skrip As String, namaParam4Blob As String, blob As Byte()) As String
        Return obyek.insertBLOB(skrip, namaParam4Blob, blob, Con)
    End Function
    Public Function ambilImage(ByVal sql As String) As Byte()
        Dim a() As Byte

        ' Dim obyek As New joanelib.myMySQL
        a = obyek.ambilImage(sql, Con)

        Return a
    End Function
End Class
