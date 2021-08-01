
Imports System.Data
Partial Class pages_loader_loadchatnext
    Inherits System.Web.UI.Page
    Public kelas As String
    Public kelaspenyelenggara, picemail, picwa As String
    Public lastid As String
    Dim listNumbers As List(Of Integer) = New List(Of Integer)()

    Private Sub pages_loader_loadchatnext_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim skrip As String
        Dim dt As DataTable
        Dim j As New ambilData
        Dim s As New initsesi
        '  skrip = "select email,kelasid from akseskelas where email='" & s.CurEmail & "'"
        '  dt = j.ambilData(skrip)
        Dim curkelasid, kodepromo As String
        Dim lastidchat, lastidchatCookies As Integer
        lastidchat = Val(joanelibweb.myRequest.getData("lastid").ToString)
        lastid = lastidchat
        Try
            lastidchatCookies = If(joanelibweb.mycookies.getData("lastidloaded"), lastid)
            '      Response.Write("1-" & lastid)
        Catch ex As Exception
            lastidchatCookies = lastidchat
            '    Response.Write("2-" & lastid)
        End Try
        joanelibweb.mycookies.setData("/", "lastidloaded", lastidchat, 72)
        curkelasid = If(s.getSesiParamString("curkelasid"), joanelibweb.myRequest.getData("id")) ' dt.Rows(0).Item("kelasid")
        kodepromo = If(s.getSesiParamString("curkodepromo"), joanelibweb.myRequest.getData("kodepromo"))


        If joanelibweb.myRequest.getData("komentar").ToString <> "" Then
            skrip = "select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' "
            Dim kelaskolektifid As Integer
            kelaskolektifid = j.ambilInteger(skrip)
            skrip = "insert into kelaschat (nama, komentar,kelaskolektifid)" &
                "values ('" & s.CurNama & "',@komentar," & kelaskolektifid & ")"
            j.insertCLOB(skrip, "@komentar", joanelibweb.myRequest.getData("komentar").ToString)
            j = New ambilData
            skrip = "select * from kelaschat where kelaskolektifid = (select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' ) and id>" & joanelibweb.myRequest.getData("lastid") & " order by id asc"
            dt = j.ambilData(skrip)
            rptchat.DataSource = dt
            rptchat.DataBind()
            skrip = "select max(id) from kelaschat where kelaskolektifid = (select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' )" 'and id>" & joanelibweb.myRequest.getData("lastid")
            lastid = j.ambilInteger(skrip)
            joanelibweb.mycookies.setData("/", "lastidloaded", lastid, 72)
            '   Response.Write("4-" & lastid)
        Else
            If lastidchat = lastidchatCookies Then
                lastid = lastidchat
                '  Response.Write("no db check" & lastid)
            Else
                'Response.Write("3-" & lastid)
                skrip = "select * from kelaschat where kelaskolektifid = (select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' ) and id>" & joanelibweb.myRequest.getData("lastid")
                lastid = j.ambilInteger(skrip) & " order by id asc"
                dt = j.ambilData(skrip)
                rptchat.DataSource = dt
                rptchat.DataBind()
                skrip = "select max(id) from kelaschat where kelaskolektifid = (select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' )"
                lastid = j.ambilInteger(skrip)


            End If
            joanelibweb.mycookies.setData("/", "lastidloaded", lastid, 72)
        End If


    End Sub
    Public Function classChat(len As Integer) As String
        Dim a As String
        Dim number As Integer
        len = joanelibweb.myRequest.getData("lastid") + len
        Dim darinol As Boolean = False

        For i As Integer = joanelibweb.myRequest.getData("lastid") To len
            i += 1
            listNumbers.Add(number)
            If listNumbers.Contains(number) = False Then Exit For
            number = i
        Next



        Select Case number - (Math.Truncate(number / 10) * 10)
            Case 1
                a = " bg-primary "
            Case 2
                a = " bg-default "
            Case 3
                a = " bg-secondary "
            Case 4
                a = " bg-success "
            Case 5
                a = "  bg-info  "
            Case 6
                a = " bg-warning "
            Case 7
                a = " bg-danger "
            Case 8
                a = " bg-primary "
            Case 9
                a = " bg-default "
            Case Else
                a = " bg-secondary "
        End Select
        Return a
    End Function
End Class
