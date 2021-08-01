
Imports System.Data
Imports System
Imports System.Web
Imports System.IO
Imports Newtonsoft.Json

Imports System.Net.Mail
Partial Class pages_curlogin
    Inherits System.Web.UI.Page
    Dim s As initsesi
    Private Sub pages_curlogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim op As New Operasional
        Dim ad As New ambilData
        Dim j As New joanelibweb.myRequest2
        Dim nama, email, pass, tabel, wa, kegiatan As String
        Dim skrip As String
        pass = j.getData("s[pass]")
        email = j.getData("s[email]")
        nama = j.getData("s[name]")
        wa = j.getData("s[wa]")
        kegiatan = j.getData("s[event]")
        Dim z As New Collection From {op.ArrayTeks("nama", nama), op.ArrayTeks("email", email), op.ArrayTeks("password", pass)}
        tabel = "member"
        Dim dt As DataTable
        s = New initsesi(email, nama)
        skrip = "select * from member where email='" & email & "'"
        dt = ad.ambilData(skrip)

        If dt.Rows.Count < 1 Then
            If j.getData("initmember") = "login" Then

                '  litwelcome.Text = ("not login")
                HttpContext.Current.Response.StatusCode = 401
                HttpContext.Current.Response.Status = "belum terdaftar"

            Else

                '  skrip = op.CreateSqlSelect(z, tabel)
                If wa <> "" Then
                    z.Add(op.ArrayTeks("wa", wa))
                End If
                ad.updateData(op.CreateSqlInsert(z, tabel))
                SendSimpleMailDaftar(email, nama, pass)
                If kegiatan <> "" Then
                    Dim x As New Collection From {op.ArrayTeks("nama", nama), op.ArrayTeks("email", email), op.ArrayTeks("wa", wa), op.ArrayNonTeks("eventid", kegiatan)}
                    ad = New ambilData
                    ad.updateData(op.CreateSqlInsert(x, "eventpeserta"))
                    SendSimpleMail(email, nama, kegiatan)
                End If


                '  Context.Response.ContentType = "text/plain"
                Dim s As New initsesi(email, nama)
                litwelcome.Text = ("new")
            End If

        Else

            If pass = "" Then
                ' Dim s As New initsesi
                ' nama = s.CurNama
                ' email = s.CurEmail
                If kegiatan <> "" Then
                    Dim t As New Collection From {op.ArrayTeks("nama", nama), op.ArrayTeks("email", email), op.ArrayTeks("wa", wa), op.ArrayNonTeks("eventid", kegiatan)}
                    ad = New ambilData
                    ad.updateData(op.CreateSqlInsert(t, "eventpeserta"))
                End If
            Else
                If dt.Rows(0).Item("password") = pass Then
                    If nama = "" Then
                        skrip = "select nama from member where email='" & email & "'"
                        nama = ad.ambilString(skrip)
                    End If
                    s = New initsesi(email, nama)
                    s.CurEmail = email
                    s.CurNama = nama
                    'Context.Response.ContentType = "text/plain"

                    litwelcome.Text = ("old" & s.CurEmail & s.CurNama)

                    If kegiatan <> "" Then
                        Dim x As New Collection From {op.ArrayTeks("nama", nama), op.ArrayTeks("email", email), op.ArrayTeks("wa", wa), op.ArrayNonTeks("eventid", kegiatan)}
                        ad = New ambilData
                        If ad.ambilData(op.CreateSqlSelect(x, "eventpeserta")).Rows.Count < 1 Then
                            ad.updateData(op.CreateSqlInsert(x, "eventpeserta"))
                        End If
                        SendSimpleMail(email, nama, kegiatan)

                    End If

                Else
                    HttpContext.Current.Response.StatusCode = 401
                    HttpContext.Current.Response.Status = "salah password"
                    ' litwelcome.Text = ("salah password")
                End If
            End If

        End If

    End Sub
    Function SendSimpleMail(em As String, nama As String, eventid As Integer) As String


        Dim dari As New MailAddress("support@ibw.news", "Event Support IBW")
        Dim kepada As New MailAddress(em, nama) 'inggrid053@gmail.com
        ' Dim kepada2 As New MailAddress("indonesianbabywearer@gmail.com", "Admin IBW") '
        Dim client As New SmtpClient()
        Dim Message As New Net.Mail.MailMessage(dari, kepada)
        'Dim message2 As New Net.Mail.MailMessage(dari, kepada2)
        Dim hasil As String
        Dim c As New myGrabbernew

        hasil = "  <html>"
        hasil &= nama & "Anda diundang"
        hasil &= "  </html>"
        hasil = c.getPagewithKeyHttps("https://ibw.news/event/tempinvite.aspx?email=" & em & "&id=" & eventid, New Net.NetworkCredential)
        Message.Body = hasil
        Message.IsBodyHtml = True
        Message.Subject = "Daftar event"

        '("admin@indonesianbabywearers.com", "depokbabywearersnew@gmail.com", "test", "uji coba")
        Try
            client.Send(Message)

        Catch ex As Exception
            Throw ex
        End Try
        Return hasil

    End Function

    Function SendSimpleMailDaftar(em As String, nama As String, pass As String) As String


        Dim dari As New MailAddress("support@ibw.news", "Web Support IBW")
        Dim kepada As New MailAddress(em, nama) 'inggrid053@gmail.com
        ' Dim kepada2 As New MailAddress("indonesianbabywearer@gmail.com", "Admin IBW") '
        Dim client As New SmtpClient()
        Dim Message As New Net.Mail.MailMessage(dari, kepada)
        'Dim message2 As New Net.Mail.MailMessage(dari, kepada2)
        Dim hasil As String
        Dim c As New myGrabbernew

        hasil = "  <html><body>"
        hasil &= "Yth" & nama & ", terimakasih telah mendaftar sebagai member https.://IBW.news"
        hasil &= "<p>Berikut adalah data pendaftaran Anda</p>"
        hasil &= "<p> User : " & em & "</p>"
        hasil &= "<p> password : " & pass & "</p>"
        hasil &= "<p> Anda dapat gunakan info untuk login </p>"
        hasil &= " </body> </html>"
        '  hasil = c.getPagewithKeyHttps("https://ibw.news/event/tempinvite.aspx?email=" & em, New Net.NetworkCredential)
        Message.Body = hasil
        Message.IsBodyHtml = True
        Message.Subject = "Daftar event"

        '("admin@indonesianbabywearers.com", "depokbabywearersnew@gmail.com", "test", "uji coba")
        Try
            client.Send(Message)

        Catch ex As Exception
            Throw ex
        End Try
        Return hasil

    End Function
End Class
