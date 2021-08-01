Imports System.Data
Partial Class Pages_loader_kurikulum
    Inherits System.Web.UI.Page

    Private Sub Pages_loader_kurikulum_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim skrip As String
        Dim dt As DataTable
        Dim kelasid As String
        Dim ad As New ambilData
        kelasid = Request("idkelas")
        skrip = "select * from listkurikulum where kelasid= " & kelasid & " order by urutan "
        dt = ad.ambilData(skrip)
        data1.DataSource = dt
        data1.DataBind()
    End Sub
End Class
