<%@ WebHandler Language="VB" Class="getKota" %>

Imports System
Imports System.Web
Imports System.Data

Public Class getKota : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim ds As DataTable
        Dim o As New ambilData
        Dim oJson As New joanelibweb.myJson
        If context.Request.Item("prov") <> "" Then
            ds = o.ambilData("select kabupaten from tbl_kodepos where provinsi='" & context.Request.Item("prov") & "' group by kabupaten asc")
        Else
            ds = o.ambilData("select kabupaten from tbl_kodepos group by kabupaten asc")
        End If
        'o.getKluperKategori(peminta.getData("kategori"))   'oRef.getKPPs '
        Dim hasil As String
        Dim b As New List(Of autoComp)
        Dim a As String
        For Each row As DataRow In ds.Rows
            a = row(0)
            b.Add(New autoComp(row(0), row(0))) '& " - " & row(1)))
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