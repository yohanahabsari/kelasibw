Imports System.Net
Imports System.IO




Partial Class Pages_getVdoCipher
    Inherits System.Web.UI.Page

    Protected Sub VdoCipher_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Output the result using asp.net literal control
        Dim idvideo As String
        '  idvideo = Request("idvideo")
        ' Dim idvideo As String
        idvideo = "5b19804afd5b45dfba765ea9860cc6bd"
        Dim o As New myVideoCipher
        Dim hasil As String
        hasil = o.get2VideoSkrip(idvideo, "08eecf8d87d541c3ac16986f488a2fd2")
        txtvideo.Text = hasil
    End Sub

End Class
