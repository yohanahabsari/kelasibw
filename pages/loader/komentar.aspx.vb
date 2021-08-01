Imports System.Data
Partial Class Pages_loader_komentar
    Inherits System.Web.UI.Page
    Public Property live_0_kuis_1_kelas_2 As Integer
    Public Property id As Integer
    Private Sub Pages_loader_komentar_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New DataTable
        Dim skrip As String
        Dim o As New ambilData
        live_0_kuis_1_kelas_2 = joanelibweb.myRequest.getData("jenis")
        id = joanelibweb.myRequest.getData("id")
        Dim materiid As Integer
        materiid = joanelibweb.myRequest.getData("materiid", 0)
        Select Case live_0_kuis_1_kelas_2
            Case 0
                skrip = "select * from eventlivekomentar where komentar <>'live' and eventliveid=" & id & " order by id desc"
            Case 1
                skrip = "select * from kuiskomentar where kuisid=" & id
            Case Else
                skrip = "select * from kelaskomentar where kelasid=" & id
        End Select

        Try
            ' Response.Write(skrip)
            dt = o.ambilData(skrip)
            komentarku.DataSource = dt
            komentarku.DataBind()

        Catch ex As Exception

            Response.Write(skrip)
        End Try

    End Sub
End Class
