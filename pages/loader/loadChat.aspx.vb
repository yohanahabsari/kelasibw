
Imports System.Data


Partial Class pages_loader_loadChat
    Inherits System.Web.UI.Page

    Public kelas As String
    Public kelaspenyelenggara, picemail, picwa As String
    Public lastid As String
    Dim listNumbers As List(Of Integer) = New List(Of Integer)()
    Private Sub pages_loader_loadChat_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim skrip As String
        Dim dt As DataTable
        Dim j As New ambilData
        Dim s As New initsesi
        '  skrip = "select email,kelasid from akseskelas where email='" & s.CurEmail & "'"
        '  dt = j.ambilData(skrip)
        Dim curkelasid, kodepromo As String
        curkelasid = s.getSesiParamString("curkelasid") 'dt.Rows(0).Item("kelasid")

        curkelasid = If(s.getSesiParamString("curkelasid"), joanelibweb.myRequest.getData("id")) ' dt.Rows(0).Item("kelasid")
        kodepromo = If(s.getSesiParamString("curkodepromo"), joanelibweb.myRequest.getData("kodepromo"))
        s.updateSesiParam("curkelasid", curkelasid, s.CurEmail, True, True)
        s.updateSesiParam("curkodepromo", kodepromo, s.CurEmail, True, True)
        s.updateSesiParam("ispenyelenggara", 0, s.CurEmail)
        ' s.updateSesiParam("curkelaspenyelenggara", "", s.CurEmail)
        Me.kelas = curkelasid
        skrip = "SELECT komunitas, picwa,picemail FROM kelaspromo,kelaspenyelenggara WHERE kelaspromo.penyelenggaraid=kelaspenyelenggara.id and kelaspromo.kodepromo='" & kodepromo & "'"
        dt = j.ambilData(skrip)
        Try
            Me.kelaspenyelenggara = dt.Rows(0).Item("komunitas")
            Me.picemail = dt.Rows(0).Item("picemail")
            Me.picwa = dt.Rows(0).Item("picwa")

        Catch ex As Exception

        End Try


        skrip = "select " & j.namakolomlengkap("kelaschat") & " from kelaschat where kelaskolektifid = (select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' )order by id asc"
        dt = j.ambilData(skrip)
        rptchat.DataSource = dt
        rptchat.DataBind()
        skrip = "select max(id) from kelaschat where kelaskolektifid = (select id from kelaskolektif where kelasid=" & curkelasid & " and kodepromo='" & kodepromo & "' )"
        lastid = j.ambilInteger(skrip)
    End Sub
    Public Function classChat(len As Integer) As String
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
