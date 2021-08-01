
Imports System.Data

Partial Class Pages_loader_chanelperkategori
    Inherits System.Web.UI.Page

    Private Sub Pages_loader_chanelperkategori_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim kat As String
        Dim s As New sesi
        kat = joanelibweb.myRequest.getData("kategori", "")
        Dim dt As DataTable
        Dim j As New ambilData
        Dim skrip As String
        '  skrip = "select chanel.kategori,chanelfeed.id,memberid,judul,chanelfeed.uraian,divambil,teks0video1url2iframe3 , awaldiv from chanelfeed,chanel where chanel.id= chanelfeed.chanelid and chanel.kategori='" & kat & "' "
        ' skrip &= " and  exists(select 'x' from chanelparam where chanelparam.chanelfeedrefid = chanelfeed.id)"
        skrip = "select ifnull(chanelparam.id,'') as chanelparamid, ifnull(chanelparam.judul,'') as judul,ifnull(chanelparam.uraian,'') as uraian, chanel.id as chanelid, chanel.kategori ,chanel.uraian as chaneluraian, chanel.topik,ifnull(chanelparam.chanelfeedrefid,'') as chanelfeedrefid from " &
            "(select * from chanel where chanel.kategori='" & kat & " ' ) chanel left join chanelparam on chanelparam.chanelid=chanel.id  "
        'Response.Write(skrip)
        dt = j.ambilData(skrip)
        loaddiv.DataSource = dt
        loaddiv.DataBind()
        loadInduk.DataSource = dt
        loadInduk.DataBind()
        If dt.Rows.Count < 1 Then
            statusfav.InnerText = "Belum ada pilihan topik tersedia untuk chanel " & kat
        Else
            skrip = "select * from chanelmember where kategori='" & kat & "' and memberid=" & s.CurMember.memberId
            dt = j.ambilData(skrip)
            If dt.Rows.Count > 0 Then
                Dim chanelid As String
                Dim chanelpilihanid As String
                Dim chanelpilihan As String
                chanelid = dt.Rows(0).Item("chanelid").ToString
                chanelpilihanid = dt.Rows(0).Item("chanelparampilihanid").ToString
                chanelpilihan = dt.Rows(0).Item("kategori").ToString & "-" & dt.Rows(0).Item("chanelparamjudul").ToString & "-" & dt.Rows(0).Item("chanelparampilihanjudul").ToString
                If Val(chanelpilihanid) > 0 Then
                    skrip = " select chanelparampilihanfeed.id, tabelpilihan.chanelid, tabelpilihan.kategori,uraianpilihan,tabelpilihan.chanelparamid, chanelparampilihanid, chanelfeed.judul,chanelfeed.uraian,chanelfeed.divambil, jenispost from chanelparampilihanfeed, " &
                    " (select chanelparampilihan.id as pilihanid,chanelparampilihan.chanelparamid,chanelparampilihan.chanelid, chanel.kategori , chanelparampilihan.uraianpilihan from chanelparampilihan,chanel where chanelparampilihan.chanelid=chanel.id) tabelpilihan ," &
                    " chanelfeed where chanelparampilihanfeed.chanelfeedid=chanelfeed.id " &
                           "  and tabelpilihan.pilihanid=chanelparampilihanfeed.chanelparampilihanid and tabelpilihan.chanelid=" & chanelid & " and chanelparampilihanid=" & chanelpilihanid

                Else
                    skrip = "select * from chanelfeed where chanelid=" & chanelid & " and exists (select * from chanelmember where chanelfeed.chanelid=chanelmember.chanelid)"
                    ' statusfav.InnerText = "Pilihan chanel  " & kat & " Anda hari ini : " & chanelpilihan
                End If
                statusfav.InnerText = "Pilihan chanel  " & kat & " Anda hari ini : " & chanelpilihan
                dt = j.ambilData(skrip)
                dataKu.DataSource = dt
                dataKu.DataBind()
                '  Response.Write(skrip)
            Else
                statusfav.InnerText = "Anda BELUM memilih chanel  " & kat & " yang akan menjadi fokus Anda hari ini "

            End If
        End If

    End Sub


End Class
