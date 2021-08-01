<%@ WebHandler Language="VB" Class="getEvent" %>

Imports System
Imports System.Web
Imports System.Data
Public Class getEvent : Implements IHttpHandler
    Dim kota, namaKomlok As String
    Dim komlokid As Integer
    Dim o As New ambilData
    Dim dtKomlok As Komlok
    Public dtKategori As DataTable

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim ds As DataTable
        Dim o As New ambilData
        Dim oJson As New joanelibweb.myJson
        komlokid = If(joanelibweb.myRequest.getData("komlokid"), joanelibweb.mycookies.getData("komlokid"))
        ds = o.ambilData("select event.id, komlokid , eventkategori.kategori, judul , uraian, tglevent , icon from event,eventkategori where event.eventkategori= eventkategori.kategori and event.komlokid=" & komlokid & " order by event.tglevent desc")  'o.getKluperKategori(peminta.getData("kategori"))   'oRef.getKPPs '
        Dim hasil As String
        Dim b As New List(Of autoComp)
        Dim a As String
        For Each row As DataRow In ds.Rows
            a = row(0)
            b.Add(New autoComp(row("id"), row("judul"), row("kategori") & " - " & row("tglevent").ToString, row("icon"))) '& " - " & row(1)))
        Next
        hasil = oJson.toJSON(b)

        context.Response.ContentType = "text/plain"
        context.Response.Write(hasil)
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class