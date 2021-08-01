Imports Microsoft.VisualBasic
Imports System.Web.Routing

Public Class RouteConfig
    Public Shared Sub RegisterRoutes(routes As RouteCollection)
        routes.MapPageRoute(Nothing, "aboutus", "~/aboutus.aspx")
        routes.MapPageRoute(Nothing, "aboutus/", "~/aboutus.aspx")
        routes.MapPageRoute(Nothing, "hubungi", "~/contactus.aspx")
        routes.MapPageRoute(Nothing, "hubungi/{bidang}", "~/contactus.aspx")
        routes.MapPageRoute(Nothing, "contactus", "~/contactus.aspx")
        routes.MapPageRoute(Nothing, "k", "~/kelas/default.aspx")
        routes.MapPageRoute(Nothing, "k/{namakelas}/ringkasan", "~/kelasbw/masukkelas.aspx")
        routes.MapPageRoute(Nothing, "k/{namakelas}/welcome", "~/kelas/ruangkelas.aspx")
        routes.MapPageRoute(Nothing, "k/{namakelas}/welcome/{idpromo}", "~/kelas/ruangkelas.aspx")
        routes.MapPageRoute(Nothing, "k/{namakelas}/komentar", "~/kelas/komentar.aspx")
        routes.MapPageRoute(Nothing, "k/{namakelas}/p/{urutanid}", "~/kelasbw/lihatmateri2.aspx")
        routes.MapPageRoute(Nothing, "a/{chanel}/{title}", "~/article/lihat.aspx")
        routes.MapPageRoute(Nothing, "rss/{title}", "~/article/default.aspx")
        routes.MapPageRoute(Nothing, "rss/{title}/", "~/article/default.aspx")
        routes.MapPageRoute(Nothing, "e/{id}/daftar/", "~/daftar.aspx")
        routes.MapPageRoute(Nothing, "e/{id}/{title}", "~/event/detail.aspx")
        routes.MapPageRoute(Nothing, "p/{penyelenggara}/event/", "~/event/partner.aspx")
        routes.MapPageRoute(Nothing, "p/{penyelenggara}/{title}/", "~/event/partnerdetail.aspx")
    End Sub
End Class
