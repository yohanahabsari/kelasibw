<%@ WebHandler Language="VB" Class="CalendarEvent" %>

Imports System
Imports System.Web
Imports System.Data

Public Class CalendarEvent : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim daftar As New List(Of kalender)
        Dim skrip As String
        Dim dt As DataTable
        Dim ad As New ambilData
        skrip = "select kelas.topik ,concat( kelaspromo.komunitas , ' ( ' , narasumber.namapublish, ')' ) as deskripsi, komunitas, DATE_FORMAT(tanggaldiskusi,'%d-%c-%Y') as tanggaldiskusi,DATE_FORMAT(jamdiskusi,'%r') as jamdiskusi,DATE_FORMAT(DATE_ADD(jamdiskusi, INTERVAL 1 hour),'%r') as jamdiskusi2  from kelaspromo,kelas ,narasumber where kelasid=kelas.id " &
" and tanggaldiskusi is not null and pembicaraid=narasumber.id and kelaspromo.isgendong=1 order by kelaspromo.id desc"
        dt = ad.ambilData(skrip)
        For Each row As DataRow In dt.Rows
            Dim a As New kalender With {
                   .summary = row.Item("topik"), .description = row.Item("deskripsi"), .color = New kalenderwarna With {.background = "#39C0ED", .foreground = "white"},
                .starttime = New kalenderwaktu With {.DateString = row.Item("tanggaldiskusi").ToString & " " & row.Item("jamdiskusi").ToString, .dateTime = row.Item("tanggaldiskusi").ToString & " " & row.Item("jamdiskusi").ToString},
                .Endtime = New kalenderwaktu With {.DateString = row.Item("tanggaldiskusi").ToString & " " & row.Item("jamdiskusi").ToString, .dateTime = row.Item("tanggaldiskusi").ToString & " " & row.Item("jamdiskusi2").ToString}}
            daftar.Add(a)
        Next
        Dim j As New joanelibweb.myJson
        Dim hasil As String
        hasil = j.toJSON(daftar)
        hasil = hasil.Replace("DateString", "date")
        hasil = hasil.Replace("starttime", "start")
        hasil = hasil.Replace("Endtime", "end")
        context.Response.ContentType = "application/json"
        context.Response.Write(hasil)
    End Sub


    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
Public Class kalender
    Public summary As String
    Public description As String
    Public starttime As kalenderwaktu
    Public Endtime As kalenderwaktu
    Public color As kalenderwarna
End Class
Public Class kalenderwaktu
    Public DateString As String '               Calendar.moment.subtract(3, 'day').format('DD/MM/YYYY'),
    Public dateTime As String ': Calendar.moment.subtract(3, 'day').format('DD/MM/YYYY') + ' 10:00 AM',
End Class
Public Class kalenderwarna
    Public background As String '               Calendar.moment.subtract(3, 'day').format('DD/MM/YYYY'),
    Public foreground As String ': Calendar.moment.subtract(3, 'day').format('DD/MM/YYYY') + ' 10:00 AM',
End Class