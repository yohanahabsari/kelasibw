<%@ WebHandler Language="VB" Class="curFBUserhandler" %>

Imports System
Imports System.Web

Public Class curFBUserhandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim op As New Operasional
        Dim ad As New ambilData
        Dim j As New joanelibweb.myRequest2
        Dim fbnama, fbid, fbemail, tabel, liveid, komentar As String
        ' komlokid = joanelibweb.mycookies.getData("komlokid")
        fbid = j.getData("fbid", "")
        fbnama = j.getData("fbnama", "")
        liveid = j.getData("eventliveid", 0)
        komentar = j.getData("komentar", "live")
        fbemail = j.getData("fbemail", "")
        Dim z As New Collection From {op.ArrayNonTeks("eventliveid", liveid), op.ArrayTeks("fbemail", fbemail), op.ArrayTeks("fbid", fbid), op.ArrayTeks("fbnama", fbnama), op.ArrayTeks("komentar", komentar), op.ArrayTeks("ip", joanelib.GetIP.GetIPAddress())}

        tabel = "eventlivekomentar"
        '   If ad.ambilInteger(op.CreateSqlSelect(z, tabel)) < 1 Then
        ad.updateData(op.CreateSqlInsert(z, tabel))
        '  End If

        context.Response.ContentType = "text/plain"
        context.Response.Write("welcome " & fbnama)
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class