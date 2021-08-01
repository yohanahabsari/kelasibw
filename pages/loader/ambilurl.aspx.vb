
Partial Class Pages_loader_ambilurl
    Inherits System.Web.UI.Page

    Private Sub Pages_loader_ambilurl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim uri, seleksi, induk, divawal As String
        Dim karakter As Integer
        uri = joanelibweb.myRequest.getData("url", "")
        seleksi = joanelibweb.myRequest.getData("seleksi", "")
        induk = joanelibweb.myRequest.getData("induk", "")
        ' divawal = joanelibweb.myRequest.getData("awaldiv", "")
        '  karakter = joanelibweb.myRequest.getData("karakter", "")
        If uri = "" Then
            divLoader.Text = "artikel tidak ditemukan"
        Else
            Dim j As New myGrabbernew
            Dim halaman As String
            halaman = j.getPage(uri, uri).ToString
            'Dim letakheadawal As Integer
            ' Dim letakheadakhir As Integer

            '  letakheadakhir = InStr(halaman, divawal)
            '  Response.Write(letakheadakhir)
            ' Response.Write(divawal)
            '  letakheadawal = 1 'InStr(letakheadakhir, halaman, "<div")
            '  halaman = Replace(halaman, Mid(halaman, letakheadawal, letakheadakhir - letakheadawal), "")
            Dim halaman1 As String
            Dim dotawal, dotakhir, tombol As String
            dotawal = "<span id='dots'>...</span><span id='more'>"
            dotakhir = "</span></p>"
            tombol = "<button onclick='myFunction()' id='myBtn'>Read more</button>"

            divsementara.Text = "<div class='d-none2' id='" & induk & "sementara'>" & halaman & "</div>"
            Dim csText As New StringBuilder()
            csText.Append("<script type=""text/javascript""> ")
            csText.Append("  $('#" & induk & "').html($('#" & induk & "sementara').find('" & seleksi & "').html());  ")
            csText.Append(" // $('#" & induk & "sementara').html(''); ")
            csText.Append("</script>")
            teksSkrip.Text = csText.ToString()
        End If
    End Sub
End Class
