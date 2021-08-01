<%@ Application Language="VB" %>
<%@ import Namespace="System.Web.Routing"  %>
<%@ import Namespace="System.Data.Entity"  %>
<%@ import Namespace="System.Web.Optimization"  %>
<script runat="server">
    ' Inherits System.Web.HttpApplication
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        ' Dim a As New System.Data.Entity.DbConfiguration()

        ' DbConfiguration.SetConfiguration(New System.Data.Entity.DbConfi) 'System.Data.Entity.MySqlEFConfiguration())
        '  BundleConfig.RegisterBundles(BundleTable.Bundles) 'bundletable.bundles)
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim serverError = TryCast(Server.GetLastError, HttpException)

        If serverError IsNot Nothing Then

            If serverError.GetHttpCode = 404 Then
                Server.ClearError()
                Server.Transfer("/route.aspx")
            End If
        End If
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
    Protected Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        If Response.StatusCode = 401 Then
            'Use the built in 403 Forbidden response
            Response.StatusCode = 403

            'OR redirect to custom page
            'Response.Redirect("FailedAuthorization.aspx");
        End If
    End Sub

    Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        If Request.IsAuthenticated Then
            ' Requires ASP.NET >= 4.5
            Response.SuppressFormsAuthenticationRedirect = True
        End If
    End Sub
</script>