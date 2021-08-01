<%@ WebHandler Language="VB" Class="keepalivehandler" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Public Class keepalivehandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        ' Dim s As New sesi2
        Dim s As New initsesi
        s.updateDariCookies()
        FormsAuthentication.SetAuthCookie(joanelibweb.mycookies.getData("email"), True)
        context.Response.AddHeader("Cache-Control", "no-cache")
        context.Response.AddHeader("Pragma", "no-cache")
        context.Response.ContentType = "text/plain"
        context.Response.Write("OK")
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class