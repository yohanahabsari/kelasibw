<%@ WebHandler Language="VB" Class="konfirmlogin" %>

Imports System
Imports System.Web
Imports System.Data

Public Class konfirmlogin : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim kue As New kuki
        Dim ds As DataTable
        Dim o As New ambilData
        Dim oJson As New joanelibweb.myJson
        Dim skrip As String
        Dim hasil As String

        dim em,na,fbid,wa as String
        em = context.Server.UrlDecode(kue.getData("email").ToString)
        na = context.Server.UrlDecode(kue.getData("nama").ToString)
        fbid = HttpUtility.UrlDecode(kue.getData("fbid").ToString)
        wa = HttpUtility.UrlDecode(kue.getData("wa").ToString)
        'jika login fb bisa masuk dan terkonfirmasi
        'jika email dan wa belum terkonfirmasi  
        Dim kode As String
        kode = GenerateRandomString(4)
        If (fbid <> "" And fbid <> "undefined") Or (em <> "" And em <> "undefined") Or (wa <> "" And wa <> "undefined") Then

            If fbid <> "" And fbid <> "undefined" Then

                skrip = "select id, fbid,email,wa,nama,isconfirmed from membernew where " &
                    "fbid ='" & fbid & "' "
                ds = o.ambilData(skrip)

                If ds.Rows.Count < 1 Then
                    skrip = "insert into membernew(kode,fbid,nama,email,isconfirmed) values('" & kode & "','" & fbid & "','" & na & "','" & em & "' ,'true') "
                    o.updateData(skrip)
                    ds = o.ambilData(skrip)
                ElseIf ds.Rows(0).Item("email") <> em Then
                    skrip = "select fbid,email,wa,nama from membernew where " &
                                     "email ='" & em & "' "
                    ds = o.ambilData(skrip)
                    If ds.Rows.Count < 1 Then
                        skrip = "insert into membernew(kode,email,wa,nama,isconfirmed) values('" & kode & "','" & em & "','" & wa &
                                "','" & na & "','false' ) "
                        o.updateData(skrip)
                    End If
                    skrip = "select id, fbid,email,wa,nama,isconfirmed from membernew where " &
             "(email ='" & em & "' and email<>'') or (wa='" & wa & "' and wa<>'' )"
                    ds = o.ambilData(skrip)
                End If

            Else ' login dengan email saja
                skrip = "select fbid,email,wa,nama from membernew where " &
                "email ='" & em & "' or " &
                "wa ='" & wa & "' "
                ds = o.ambilData(skrip)
                If ds.Rows.Count < 1 Then
                    skrip = "insert into membernew(kode,email,wa,nama,isconfirmed) values('" & kode & "','" & em & "','" & wa &
                            "','" & na & "','false' ) "
                    o.updateData(skrip)
                End If
                skrip = "select id, fbid,email,wa,nama,isconfirmed from membernew where " &
             "(email ='" & em & "' and email<>'') or (wa='" & wa & "' and wa<>'' )"
                ds = o.ambilData(skrip)

            End If
            If kode <> "" Then
                kirimEmail(em, na, "Kode konfirmasi Anda adalah " & kode, "Kode login KelasIBW.com", AddressOf fungsiEmailNamaCat2)
            End If
            Dim b As New newJoaneJson

            hasil = b.jsonfromTable(ds)
        Else
            ds = o.ambilData("select id, fbid,email,wa,nama from membernew where 1=2" )

            Dim b As New newJoaneJson

            hasil = b.jsonfromTable(ds)

        End If




        context.Response.ContentType = "application/json"
        context.Response.Write(hasil)
    End Sub
    Function fungsiEmailNamaCat2(ByVal email As String, ByVal nama As String, ByVal catatan As String
) As String
        Dim hasil As String
        hasil = "Yth, " & nama
        hasil &= vbCrLf
        hasil &= catatan
        hasil &= vbCrLf
        hasil &= vbCrLf
        hasil &= "Salam"
        hasil &= vbCrLf
        hasil &= "Yohana"
        Return hasil
    End Function
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class