Imports System.Data
Partial Class Pages_loader_chanel
    Inherits System.Web.UI.Page
    Public Property batas As Integer
    Private Sub Pages_loader_chanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New DataTable
        Dim skrip As String
        Dim o As New ambilData
        Try
            batas = Val(HttpContext.Current.Request("batas").ToString)
            skrip = "select * from chanelfeed  order by id desc limit " & batas
        Catch ex As Exception
            batas = -1
            skrip = "select * from chanelfeed  "
        End Try

        Try
            ' Response.Write(skrip)
            dt = o.ambilData(skrip)
            dataKu.DataSource = dt
            dataKu.DataBind()

        Catch ex As Exception

            Response.Write(skrip)
        End Try

    End Sub
End Class
