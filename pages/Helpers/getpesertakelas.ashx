<%@ WebHandler Language="VB" Class="getpesertakelas" %>

Imports System.Data
Imports System
Imports System.Web

Public Class getpesertakelas : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim list1 As New ArrayList
        ' Create a separate ArrayList.
        Dim kelasid, kodepromo As String
        Dim skrip As String
        Dim dt As DataTable
        Dim j As New ambilData
        kelasid = context.Request.Item("id")
        kodepromo = context.Request.Item("kodepromo")

        skrip = "select  nama, username,email,kodeakses,date_format(mulai,'%d-%m-%Y') as mulai ,date_format(lastakses,'%d-%m-%Y') as lastakses,foto  from akseskelas where kelasid=" & kelasid & " and kodepromo='" & kodepromo & "' order by id desc"
        dt = j.ambilData(skrip)
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim listRow As New ArrayList
                Dim listCol As New ArrayList
                For Each col As DataColumn In dt.Columns
                    If col.ColumnName = "foto" Then
                        Dim fotoku As String
                        fotoku = row.Item(col.ColumnName).ToString
                        If fotoku = "" Then
                        Else
                            row.Item(col.ColumnName) = "<a href='" & fotoku & "' target='_blank' class='btn btn-link'>sertifikat</a>"
                        End If


                    End If
                    listCol.Add(row.Item(col.ColumnName).ToString)
                Next
                listRow.Add(listCol)
                list1.Add(listCol)
            Next
        End If
        Dim hasil As New joanelibweb.myJson

        ' Add this ArrayList to the other one.

        context.Response.ContentType = "application/json"
        context.Response.Write(hasil.toJSON(list1))
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class