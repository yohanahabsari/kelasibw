<%@ WebHandler Language="VB" Class="curlogin"  %>

Imports System
Imports System.Web

Public Class curlogin : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim op As New Operasional
        Dim ad As New ambilData
        Dim j As New joanelibweb.myRequest2
        Dim nama, email, pass, tabel As String
        pass = j.getData("initpassword")
        email = j.getData("initemail")
        nama = j.getData("initnama")
        Dim z As New Collection From {op.ArrayNonTeks("nama", nama), op.ArrayTeks("email", email), op.ArrayTeks("pass", pass)}
        Dim s As New initsesi(email, nama)

        tabel = "member"
        If ad.ambilString(op.CreateSqlSelect(z, tabel)) = "" Then
            If j.getData("initmember") = "login" Then
                ' context.Response.StatusCode = 401
                context.Response.ContentType = "text/plain"
                context.Response.Write("Yuk mendaftar sebagai member IBW di https://ibw.news/daftar.aspx")
            Else
                ad.updateData(op.CreateSqlInsert(z, tabel))
                context.Response.ContentType = "text/plain"
                context.Response.Write("welcome " & nama)
            End If

        Else
            context.Response.ContentType = "text/plain"
            context.Response.Write("welcome " & nama)
        End If


    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class