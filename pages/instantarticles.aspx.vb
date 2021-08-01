Imports System.Data
Partial Class Pages_instantarticles
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ad As ambilData
    Dim skrip As String
    Public linkcur As String
    Public skrip2 As String
    Private Sub Pages_instantarticles_Load(sender As Object, e As EventArgs) Handles Me.Load
        ad = New ambilData
        skrip = "select " & ad.namakolomlengkap("rssfeed") & " from rssfeed where id=1"
        dt = ad.ambilData(skrip)
        datachanel.DataSource = dt
        datachanel.DataBind()
        skrip2 = skrip
        linkcur = " https://ibw.news/" & "rss" & "/" & Server.UrlEncode(dt.Rows(0).Item("title"))
        skrip = "select " & ad.namakolomlengkap("rssfeeditem") & " from rssfeeditem where rssfeedid=1"
        dt = ad.ambilData(skrip)
        artikelku.DataSource = dt
        artikelku.DataBind()
    End Sub

    Private Sub artikelku_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles artikelku.ItemDataBound
        '  If (e.Item.ItemType = ListItemType.Item) Or
        '(e.Item.ItemType = ListItemType.AlternatingItem) Then
        '       Dim datakonten As Controls_ctlinstantA = DirectCast(e.Item.FindControl("iteminstanta"), Controls_ctlinstantA)
        '      datakonten.jenis = DirectCast(e.Item.DataItem, DataRowView)("Address_ID").ToString ' DirectCast(e.Item.DataItem, DataRowView)("Address_ID").ToString
        '     datakonten.uraian =
        '    datakonten.caption =
        '    End If
    End Sub

    Function cekkonten(jenis As String, caption As String, uraian As String) As String
        Dim hasil As String = ""
        Select Case jenis
            Case "video"
                hasil = "<figure><video><source src = '"
                hasil &= uraian
                hasil &= "' Type = 'video/mp4' />  </video>  </figure>"
            Case "img"
                hasil = "<figure><img src ='"
                hasil &= uraian & "'  />"
                hasil &= "<figcaption>" & caption & "</figcaption>"
                hasil &= "</figure>"

            Case "fb"
                hasil = " <figure Class='op-interactive'>"
                hasil &= uraian
                hasil &= "</figure>"

            Case "youtube"
            Case Else
                hasil = "<p>"
                hasil &= uraian
                hasil &= "</p>"
        End Select
        Return hasil
    End Function
End Class
