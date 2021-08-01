<%@ WebHandler Language="VB" Class="curFBUserhandler" %>

Imports System
Imports System.Web

Public Class curFBUserhandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim op As New Operasional
        Dim ad As New ambilData
        Dim j As New joanelibweb.myRequest2
        Dim fbnama, fbid, komlokid, tabel As String
        komlokid = joanelibweb.mycookies.getData("komlokid")
        fbid = j.getData("userid")
        fbnama = j.getData("nama")
        Dim z As New Collection From {op.ArrayNonTeks("komlokid", komlokid), op.ArrayTeks("userid", fbid), op.ArrayTeks("nama", fbnama), op.ArrayTeks("jenis", "facebook")}
        If j.getData("path") = "admin" Then
            tabel = "komlokadminguest"
        Else
            tabel = "komlokguest"
        End If

        If ad.ambilInteger(op.CreateSqlSelect(z, tabel)) < 1 Then
            ad.updateData(op.CreateSqlInsert(z, tabel))
        End If

        context.Response.ContentType = "text/plain"
        context.Response.Write("welcome " & fbnama)
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class