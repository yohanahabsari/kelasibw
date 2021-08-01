Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.Services

Public Class PicHandler
    Implements System.Web.IHttpHandler

    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest

        Throw New NotImplementedException()
    End Sub
End Class
