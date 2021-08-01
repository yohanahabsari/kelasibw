Imports System.Data
Imports Microsoft.VisualBasic

Public Class kelas
    Public Property kelasid As String
    Public Property kodepromo As String
    Public Property namapeserta As String
    Public Property jenis As String
    Public Property topik As String
    Public Property imgheader As String
    Public Property deskripsi As String
    Public Property mulai As String
    Public Property berakhir As String
    Public Property curstep As Integer
    Public Property prevstep As Integer
    Public Property nextstep As Integer
    Public Property jmlpeserta As Integer
    Public Property username As String
    Public Property jmlstep As Integer
    Public Property foto As String
    ' Public curKelas As New obyekkelas
    Dim skrip As String
    Dim dt As DataTable
    Dim listNumbers As List(Of Integer) = New List(Of Integer)()
    Public Property dtkat As DataTable
    Public Property dtKomentar As DataTable
    Public Property dtlike As DataTable
    Public serverstep As Integer
    Public Sub New()
        MyBase.New
    End Sub
    Public Sub New(idkelas As String)
        '  Me.kelasid = idkelas
        tampilKelas(idkelas)
    End Sub

    Sub tampilKelas(kelasid As String)
        Dim ad As New ambilData
        '  curKelas = New obyekkelas
        skrip = "select kelas.id as kelasid,kelas.jenis, kelas.topik,kelas.deskripsi, kelas.imgheader,count(username) as jmlpeserta, date_format(akseskelas.berakhir,'%d-%m-%Y') as berakhir , akseskelas.nama as nama "
        skrip &= " from akseskelas,kelas "
        skrip &= " where akseskelas.kelas = kelas.id "
        skrip &= " and kelas.id=" & kelasid
        ' Response.Write(skrip)
        dt = ad.ambilData(skrip)
        Dim s As New initsesi
        Dim curkelasstep, curkelasprevstep, curkelasnextstep As Integer
        curkelasstep = s.getSesiParamInteger("curkelasstep")
        If curkelasstep < 1 Then
            curkelasprevstep = 1
            curkelasstep = 1

        ElseIf curkelasstep = 1 Then
            curkelasprevstep = 1
            curkelasnextstep = curkelasstep + 1
        Else
            curkelasnextstep = curkelasstep + 1
            curkelasprevstep = curkelasstep - 1
        End If
        s.updateSesiParam("curkelasstep", curkelasstep, s.CurEmail)
        With dt.Rows(0)

            Me.topik = .Item("topik")
            Me.imgheader = .Item("imgheader")
            Me.kelasid = kelasid
            Me.deskripsi = .Item("deskripsi")
            Me.jmlpeserta = .Item("jmlpeserta")
            Me.jenis = .Item("jenis")
            Me.curstep = curkelasstep
            Me.prevstep = curkelasprevstep

            Me.nextstep = curkelasnextstep
            '  me.username = akses
            Try
                Me.berakhir = joanelib.operasionalWeb.formatTanggal(.Item("berakhir"))
            Catch

            End Try
        End With

    End Sub
End Class
