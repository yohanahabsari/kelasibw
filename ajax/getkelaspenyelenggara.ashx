<%@ WebHandler Language="VB" Class="getkelaspenyelenggara" %>

Imports System
Imports System.Web
Imports System.Data

Public Class getkelaspenyelenggara : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim ds As DataTable
        Dim o As New ambilData
        Dim oJson As New joanelibweb.myJson
        Dim s As New initsesi

        If s.CurEmail <> "" Then
            ds = o.ambilData("select nama from kelaspenyelenggara where memberemail='" & s.CurEmail & "'  group by nama order by nama")
        Else
            ds = o.ambilData("select nama from kelaspenyelenggara where 1=2  group by nama order by nama")

        End If
        Dim hasil As String
        Dim b As New List(Of autoComp)
        Dim a As String
        If ds.Rows.Count > 0 Then
            For Each row As DataRow In ds.Rows
                a = row(0)
                b.Add(New autoComp(row(0), row(0))) '& " - " & row(1)))
            Next
            hasil = oJson.toJSON(b)
        Else
            hasil = ""
        End If


        context.Response.ContentType = "text/plain"
        context.Response.Write(hasil)
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class