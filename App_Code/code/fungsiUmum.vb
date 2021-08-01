Imports System.Data
Imports Microsoft.VisualBasic
Imports System.IO
Imports Newtonsoft.Json

Imports System.Net.Mail
Imports Ical.Net
Imports Ical.Net.DataTypes
'Imports Ical.Net.Serialization.iCalendar.Serializers
Imports Ical.Net.Serialization
Public Module fungsiUmum
    Dim dtKomlok As Komlok
    Public listNumbers As List(Of Integer) = New List(Of Integer)()
    Public Function GenerateRandomAlpha(ByRef lenStr As Integer, Optional ByVal upper As Boolean = False) As String
        'use
        'TextBox1.Text = GenerateRandomString(18)
        Dim rand As New Random()
        Dim allowableChars() As Char = "abcdefghijklmnpqrstuvwxyz1234567890".ToCharArray()
        '"abcdefghighlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim final As New System.Text.StringBuilder
        Do
            'final += allowableChars(rand.Next(allowableChars.Length - 1))
            final.Append(allowableChars(rand.Next(0, allowableChars.Length)))
        Loop Until final.Length = lenStr
        ' Debug.WriteLine(final.Length)
        Return If(upper, final.ToString.ToUpper(), final.ToString)
    End Function
    Function bikinisiemailstandard(
    ByVal email As String,
    ByVal nama As String,
    ByVal catatan As String
) As String
        Return "test standard"
    End Function
    Public Function GenerateRandomString(ByRef lenStr As Integer, Optional ByVal upper As Boolean = False) As String
        'use
        'TextBox1.Text = GenerateRandomString(18)
        Dim rand As New Random()
        Dim allowableChars() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray()
        '"abcdefghighlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim final As New System.Text.StringBuilder
        Do
            'final += allowableChars(rand.Next(allowableChars.Length - 1))
            final.Append(allowableChars(rand.Next(0, allowableChars.Length)))
        Loop Until final.Length = lenStr
        ' Debug.WriteLine(final.Length)
        Return If(upper, final.ToString.ToUpper(), final.ToString)
    End Function
    Public Function GenerateRandomInt(ByVal len As Integer, Optional ByVal dariNol As Boolean = False) As List(Of Integer)
        Dim listNumbers As List(Of Integer) = New List(Of Integer)()
        Dim number As Integer
        Dim rand As New Random()
        For i As Integer = 0 To len - 1
            rand = New Random()
            Do
                If dariNol Then
                    number = rand.Next(0, len)
                Else
                    number = rand.Next(1, len)

                End If

            Loop While listNumbers.Contains(number)
            i += 1
            listNumbers.Add(number)
        Next

        Return listNumbers
    End Function
    Public Function GenerateRandomIntKoma(ByVal len As Integer, Optional ByVal dariNol As Boolean = False) As String
        Dim listNumbers As List(Of Integer) = New List(Of Integer)()
        Dim number As Integer
        Dim rand As New Random()
        If dariNol Then
            For i As Integer = 0 To (len - 1)

                Do

                    number = rand.Next(0, len - 1)

                Loop While listNumbers.Contains(number)
                i += 1
                listNumbers.Add(number)
            Next
        Else
            For i As Integer = 1 To len

                Do

                    number = rand.Next(1, len)

                Loop While listNumbers.Contains(number)
                i += 1
                listNumbers.Add(number)
            Next
        End If
        Dim hasil As String = ","
        For Each a As Integer In listNumbers
            hasil &= a
        Next
        hasil = Mid(hasil, 2, hasil.Length)
        Return hasil
    End Function
    Public Function cekAksesKelas(username As String, kelas As Integer, pass As String) As Boolean
        Dim skrip As String
        Dim dt As New DataTable
        Dim o As New ambilData
        If Roles.IsUserInRole(username, "super") Then
            Return True
        ElseIf Roles.IsUserInRole(username, "member") Then
            Dim orang As New memberibw(username, pass)
            skrip = "select * from kelassesi where kelasid=" & kelas & " and memberid=" & orang.memberId & "  and timediff(berakhir, now()) >0"
            dt = o.ambilData(skrip)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            skrip = "select * from kelassesi where kelasid=" & kelas & " and username=" & username & "  and timediff(berakhir, now()) >0"
            dt = o.ambilData(skrip)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End If
    End Function
    Public Function urlstandard(url As String, p As Page) As String
        Dim urlku As String
        urlku = joanelibweb.myRequest.getAbsoluteURL(url, p)
        urlku = urlku.ToLower.Replace("http", "https")
        Return urlku
    End Function

    Delegate Function MarkupEmail(
    ByVal email As String,
    ByVal nama As String,
    ByVal catatan As String
) As String
    Delegate Function markupEmailString(ringkasan As String, deskripsi As String, startyyyymmdd As String, starthhss As String, endyyyymmdd As String, endhhss As String, nama As String, email As String) As String
    Delegate Function markupEmailTiket(ringkasan As String, deskripsi As String, startyyyymmdd As String, starthhss As String, endyyyymmdd As String, endhhss As String, nama As String, email As String) As Net.Mail.Attachment

    Function markupemaildefault(ByVal email As String, ByVal nama As String, ByVal catatan As String
) As String
        Dim hasil As String
        hasil = "Kiriman ke :" & email
        hasil &= vbCrLf
        hasil &= nama
        hasil &= vbCrLf
        hasil &= "catatan : " & catatan
        Return hasil
    End Function
    Function markupAttach(ringkasan As String, deskripsi As String, startyyyymmdd As String, starthhss As String, endyyyymmdd As String, endhhss As String, lokasi As String, nama As String, email As String) As Net.Mail.Attachment
        Dim calendar = New Ical.Net.Calendar()
        '.[Class] = "PUBLIC",
        '   For Each res In reg.Reservations
        Dim aa As New CalendarComponents.CalendarEvent
        Dim o As New Operasional
        aa.Class = "PUBLIC"
        Dim b As New Organizer
        With b
            .CommonName = nama
            .Value = New Uri("mailto:" & email)
        End With
        '  Dim starty, startm, startd, starth, starts, endy, endm, endd, endh, ends As String
        Dim starttanggal As datatanggal
        Dim endtanggal As datatanggal
        starttanggal = New datatanggal(startyyyymmdd, starthhss)
        endtanggal = New datatanggal(endyyyymmdd, endhhss)
        With aa
            .Summary = ringkasan
            .Created = New CalDateTime(Now())
            .Description = deskripsi
            .Start = New CalDateTime(starttanggal.tahun, starttanggal.bulan, starttanggal.tanggal, starttanggal.jam, starttanggal.menit, starttanggal.detik)
            .End = New CalDateTime(endtanggal.tahun, endtanggal.bulan, endtanggal.tanggal, endtanggal.jam, endtanggal.menit, endtanggal.detik)
            .Sequence = 0
            .Uid = Guid.NewGuid().ToString()
            .Location = lokasi
            .Organizer = b
        End With
        '  calendar.Events.Add(New Calendar.event)
        calendar.Events.Add(aa)
        '   Next

        Dim serializer = New CalendarSerializer(New SerializationContext())
        Dim serializedCalendar = serializer.SerializeToString(calendar)
        Dim bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar)
        Dim ms As MemoryStream = New MemoryStream(bytesCalendar)
        Dim attachment As Net.Mail.Attachment = New Net.Mail.Attachment(ms, "event.ics", "text/calendar")

        Return attachment
    End Function
    Structure datatanggal
        Dim tahun As String
        Dim bulan As String
        Dim tanggal As String
        Dim jam As String
        Dim menit As String
        Dim detik As String
        Sub New(yyyymmdd As String, hhss As String)
            tahun = Mid(yyyymmdd, 1, 4)
            If Len(yyyymmdd) = 10 Then
                bulan = Mid(yyyymmdd, 6, 2)
                tanggal = Mid(yyyymmdd, 9, 2)
            Else
                Dim letakstrip1 As Integer
                Dim letakstrip2 As Integer
                letakstrip1 = InStr(yyyymmdd, "-")
                letakstrip2 = InStr(letakstrip1 + 1, yyyymmdd, "-")
                bulan = Mid(yyyymmdd, letakstrip1 + 1, 2)
                tanggal = Mid(yyyymmdd, letakstrip2 + 1, 2)
            End If
            Dim letakbagi As Integer
            letakbagi = InStr(hhss, ":")
            jam = Mid(hhss, 1, letakbagi - 1)
            menit = Mid(hhss, letakbagi + 1, 2)
        End Sub
    End Structure
    Function kirimEmail(em As String, nama As String, catatan As String, judul As String, markup As MarkupEmail) As String


        Dim dari As New MailAddress("support@ibw.news", "IBW Support")
        Dim kepada As New MailAddress(em, nama) 'inggrid053@gmail.com
        ' Dim kepada2 As New MailAddress("indonesianbabywearer@gmail.com", "Admin IBW") '
        Dim client As New SmtpClient()
        Dim Message As New Net.Mail.MailMessage(dari, kepada)
        'Dim message2 As New Net.Mail.MailMessage(dari, kepada2)
        Dim hasil As String
        Dim c As New myGrabbernew

        ' hasil = "  <html>"
        'hasil &= nama & "Anda diundang"
        'hasil &= "  </html>"
        'hasil = c.getPagewithKeyHttps("https://ibw.news/event/tempinvite.aspx?email=" & em & "&id=" & eventid, New Net.NetworkCredential)
        hasil = markup(em, nama, catatan)
        Message.Body = hasil
        Message.IsBodyHtml = True
        Message.Subject = judul
        ' Message.Attachments.Add(markupAttach)
        '("admin@indonesianbabywearers.com", "depokbabywearersnew@gmail.com", "test", "uji coba")
        Try
            client.Send(Message)

        Catch ex As Exception
            Throw ex
        End Try
        Return hasil

    End Function
    Function kirimEmailError(em As String, nama As String, catatan As String, judul As String, markup As MarkupEmail) As String


        Dim dari As New MailAddress("support@ibw.news", "IBW Support")
        Dim kepada As New MailAddress("joane3005@gmail.com", nama) 'inggrid053@gmail.com
        ' Dim kepada2 As New MailAddress("indonesianbabywearer@gmail.com", "Admin IBW") '
        Dim client As New SmtpClient()
        Dim Message As New Net.Mail.MailMessage(dari, kepada)
        'Dim message2 As New Net.Mail.MailMessage(dari, kepada2)
        Dim hasil As String
        Dim c As New myGrabbernew

        ' hasil = "  <html>"
        'hasil &= nama & "Anda diundang"
        'hasil &= "  </html>"
        'hasil = c.getPagewithKeyHttps("https://ibw.news/event/tempinvite.aspx?email=" & em & "&id=" & eventid, New Net.NetworkCredential)
        hasil = markup(em, nama, catatan)
        Message.Body = hasil
        Message.IsBodyHtml = True
        Message.Subject = judul
        ' Message.Attachments.Add(markupAttach)
        '("admin@indonesianbabywearers.com", "depokbabywearersnew@gmail.com", "test", "uji coba")
        Try
            client.Send(Message)

        Catch ex As Exception
            Throw ex
        End Try
        Return hasil

    End Function
    Function kirimEmailKalender(em As String, nama As String, catatan As String, judul As String, startyyyymmdd As String, starthhss As String, endyyyymmdd As String, endhhss As String, lokasi As String, markup As markupEmailString) As String


        Dim dari As New MailAddress("support@ibw.news", "IBW Support")
        Dim kepada As New MailAddress(em, nama) 'inggrid053@gmail.com
        Dim kepada2 As New MailAddress("indonesianbabywearer@gmail.com", "Admin IBW") '
        Dim copy As MailAddress = New MailAddress("indonesianbabywearer@gmail.com")
        Dim copy2 As MailAddress = New MailAddress("joane3005@gmail.com")
        'Dim copy3 As MailAddress = New MailAddress("renitriokta@gmail.com")

        Dim client As New SmtpClient()
        Dim Message As New Net.Mail.MailMessage(dari, kepada)
        Dim message2 As New Net.Mail.MailMessage(dari, kepada2)
        Dim hasil As String
        Dim c As New myGrabbernew

        ' hasil = "  <html>"
        'hasil &= nama & "Anda diundang"
        'hasil &= "  </html>"
        'hasil = c.getPagewithKeyHttps("https://ibw.news/event/tempinvite.aspx?email=" & em & "&id=" & eventid, New Net.NetworkCredential)
        hasil = markup(judul, catatan, startyyyymmdd, starthhss, endyyyymmdd, endhhss, nama, em)
        Message.Body = hasil
        Message.IsBodyHtml = True
        Message.Subject = judul
        Try
            Message.Attachments.Add(markupAttach(judul, catatan, startyyyymmdd, starthhss, endyyyymmdd, endhhss, lokasi, nama, em))

        Catch ex As Exception

        End Try
        Message.CC.Add(copy)
        Message.Bcc.Add(copy2)
        '("admin@indonesianbabywearers.com", "depokbabywearersnew@gmail.com", "test", "uji coba")
        Try
            client.Send(Message)


        Catch ex As Exception
            kirimEmailError(em, "tim error", ex.StackTrace, catatan, AddressOf markupemaildefault)
        End Try
        Return hasil

    End Function
    Public Function classBtn(len As Integer) As String
        Dim a As String
        Dim number As Integer

        Dim darinol As Boolean = False

        For i As Integer = 0 To len
            i += 1
            listNumbers.Add(number)
            If listNumbers.Contains(number) = False Then Exit For
            number = i
        Next



        Select Case number - (Math.Truncate(number / 10) * 10)
            Case 1
                a = " btn-outline-primary "
            Case 2
                a = " btn-outline-default "
            Case 3
                a = " btn-outline-secondary "
            Case 4
                a = " btn-outline-success "
            Case 5
                a = "  btn-outline-info  "
            Case 6
                a = " btn-outline-warning "
            Case 7
                a = " btn-outline-danger "
            Case 8
                a = " btn-outline-primary "
            Case 9
                a = " btn-outline-default "
            Case Else
                a = " btn-outline-secondary "
        End Select
        Return a
    End Function
    Public Function classText(len As Integer) As String
        Dim a As String
        Dim number As Integer

        Dim darinol As Boolean = False

        For i As Integer = 0 To len
            i += 1
            listNumbers.Add(number)
            If listNumbers.Contains(number) = False Then Exit For
            number = i
        Next



        Select Case number - (Math.Truncate(number / 10) * 10)
            Case 1
                a = " text-primary "
            Case 2
                a = " text-default "
            Case 3
                a = " text-secondary "
            Case 4
                a = " text-success "
            Case 5
                a = "  text-info  "
            Case 6
                a = " text-warning "
            Case 7
                a = " text-danger "
            Case 8
                a = " text-primary "
            Case 9
                a = " text-default "
            Case Else
                a = " text-secondary "
        End Select
        Return a
    End Function

End Module
