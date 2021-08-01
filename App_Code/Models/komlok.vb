Imports System.Data
Imports Microsoft.VisualBasic

Public Class Komlok
    Dim o As New ambilData
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(id As Integer, nama As String, tglberdiri As Date)
        Me.Id = id
        Me.Nama = nama
        Me.Tglberdiri = tglberdiri
        Me.Kontak = New KomlokKontak
        Me.Kota = New List(Of Komlokkota)

    End Sub
    Public Sub New(id As Integer)
        MyBase.New()
        Me.Id = id

        Dim dt, dtkota, dtkontak As DataTable
        dt = o.ambilData("select id,nama from komlok where id=" & id)
        Me.Nama = dt.Rows(0).Item("nama")
        Me.Tglberdiri = DateSerial(2017, 9, 1) ' dt.Rows(0).Item("Tglberdiri")
        dtkota = o.ambilData("select * from komlokkota where komlokid=" & id)
        Me.Kota = New List(Of Komlokkota)
        For Each r As DataRow In dtkota.Rows
            Me.Kota.Add(New Komlokkota(id, r("id"), r("kota")))
        Next
        Me.Kontak = New KomlokKontak()
        dtkontak = o.ambilData("select * from komlokkontak where komlokid=" & id)
        For Each r As DataRow In dtkontak.Rows
            Me.Kontak = (New KomlokKontak(id, r("id"), If(r("telp"), "").ToString, If(r("email"), "").ToString, If(r("ig"), "").ToString, If(r("fp"), "").ToString, If(r("fbgroup"), "").ToString, If(r("web"), "").ToString, If(r("alamat"), "").ToString))
        Next
    End Sub
    Public Property Id() As Integer
    Public Property Nama() As String
    Public Property Picid() As Integer
    Public Property Tglberdiri() As Date
    Public Property TglUpdate() As Date
    Public Property IsInactive() As Integer
    Public Property Kota() As List(Of Komlokkota)
    Public Property Kontak() As KomlokKontak
End Class

Public Class Komlokkota

    Public Sub New()

    End Sub
    Public Sub New(komlokid As Integer, id As Integer, kota As String)
        MyBase.New()
        Me.Komlokid = komlokid
        Me.Id = id
        Me.Kota = kota
    End Sub
    Public Property Id() As Integer
    Public Property Kota() As String
    Public Property Komlokid() As Integer

End Class

Public Class KomlokKontak
    Public Sub New()

    End Sub
    Public Sub New(komlokid As Integer, id As Integer, telp As String, email As String,
                   ig As String, fp As String, fbgroup As String, web As String, alamat As String)
        MyBase.New()
        Me.KomlokId = komlokid
        Me.Id = id
        Me.Telp = telp
        Me.Email = email
        Me.Ig = ig
        Me.Fp = fp
        Me.FBgroup = fbgroup
        Me.Web = web
        Me.Alamat = alamat
    End Sub
    Public Property KomlokId() As Integer
    Public Property Id() As Integer
    Public Property Telp() As String
    Public Property Email() As String
    Public Property Ig() As String
    Public Property Fp() As String
    Public Property FBgroup() As String
    Public Property Web() As String
    Public Property Alamat() As String

End Class
