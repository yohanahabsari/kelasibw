Imports System.Net
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

Public Class myVideoCipher
    Dim apikey As String = "j0IiBZEwmLgs1rBNYDznsbP3cpsfGAqFRbEYT7IoMi9Z2vMGbWHAGzDZVuYKbnJJ"
    Public Sub New()
        MyBase.New
    End Sub

    Public Function getVideos() As myVideoList
        Dim j As New myGrabbernew 'joanelibweb.myGrabber
        Dim hasil As String
        Dim daftarVideo As myVideoList
        ' Dim idvideo As String
        '  idvideo = "5b19804afd5b45dfba765ea9860cc6bd"

        hasil = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos?page=1", cekKey)
        Dim ss As New joanelibweb.myJson
        daftarVideo = JsonConvert.DeserializeObject(Of myVideoList)(hasil)
        Return daftarVideo
    End Function
    Public Function getVideos(tagComma As String, Optional jumlahVideo As Integer = 20) As myVideoList
        Dim j As New myGrabbernew 'joanelibweb.myGrabber
        Dim hasil As String
        Dim daftarVideo As myVideoList
        ' Dim idvideo As String
        '  idvideo = "5b19804afd5b45dfba765ea9860cc6bd"

        hasil = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos?page=1&tags=" & tagComma & "&limit=" & jumlahVideo, cekKey)
        Dim ss As New joanelibweb.myJson
        daftarVideo = JsonConvert.DeserializeObject(Of myVideoList)(hasil)
        Return daftarVideo
    End Function
    Public Function getVideo(id As String) As myVideo
        Dim j As New myGrabbernew 'joanelibweb.myGrabber
        Dim hasil As String
        Dim Video As myVideo
        ' Dim idvideo As String
        '  idvideo = "5b19804afd5b45dfba765ea9860cc6bd"

        hasil = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos/" & id, cekKey)
        Dim ss As New joanelibweb.myJson
        Video = JsonConvert.DeserializeObject(Of myVideo)(hasil)
        Return Video
    End Function
    Public Function getVideoSkrip(idvideo As String) As String
        Dim j As New myGrabbernew 'joanelibweb.myGrabber
        Dim hasil As String
        Dim obyOtp As getotp
        hasil = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos/" & idvideo & "/otp", cekKey)
        Dim ss As New joanelibweb.myJson
        obyOtp = JsonConvert.DeserializeObject(Of getotp)(hasil)
        Return (getSkrip(obyOtp))
    End Function
    Public Function get2VideoSkrip(idvideo As String, idvideo2 As String) As String
        Dim j As New myGrabbernew 'joanelibweb.myGrabber
        Dim otp1, otp2 As String
        Dim hasilakhir As String
        Dim selector As String
        selector = GenerateRandomString(6)
        Dim obyOtp As getotp
        otp1 = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos/" & idvideo & "/otp", cekKey)
        Dim ss As New joanelibweb.myJson
        obyOtp = JsonConvert.DeserializeObject(Of getotp)(otp1)
        hasilakhir = (getSkripDiv(obyOtp, selector))
        hasilakhir &= (getSkripAdd(obyOtp, selector))
        otp2 = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos/" & idvideo2 & "/otp", cekKey)
        ' Dim ss As New joanelibweb.myJson
        obyOtp = JsonConvert.DeserializeObject(Of getotp)(otp2)
        hasilakhir &= (getSkripAdd(obyOtp, selector))
        Return hasilakhir
    End Function
    Private Function cekKey() As String
        Dim a As New NetworkCredential
        a.UserName = "Apisecret"
        a.Password = apikey
        Return a.UserName & " " & a.Password
    End Function
    Private Structure getotp
        Dim otp As String
        Dim playbackInfo As String
    End Structure
    Private Function getSkrip(o As getotp) As String
        Dim selector As String
        selector = GenerateRandomString(6)
        Dim strVideoCode As String
        strVideoCode = " <div id = '" & selector & "' style='width:auto;max-width:100%;height:480px;'></div>"
        strVideoCode &= "<script>"
        strVideoCode &= "  vdo.add({"
        strVideoCode &= "otp:   '" & o.otp & "',"
        strVideoCode &= "playbackInfo: '" & o.playbackInfo & "', "
        strVideoCode &= " theme: '9ae8bbe8dd964ddc9bdb932cca1cb59a',"
        strVideoCode &= " container: document.querySelector('#" & selector & "'),"
        strVideoCode &= "});"
        strVideoCode &= "</script>"
        Return strVideoCode
    End Function
    Private Function getSkripDiv(o As getotp, selector As String) As String

        Dim strVideoCode As String
        strVideoCode = " <div id = '" & selector & "' style='width:auto;max-width:100%;height:480px;'></div>"
        Return strVideoCode
    End Function

    Private Function getSkripAdd(o As getotp, selector As String) As String

        Dim strVideoCode As String

        strVideoCode = "<script>"
        strVideoCode &= "  vdo.add({"
        strVideoCode &= "otp:   '" & o.otp & "',"
        strVideoCode &= "playbackInfo: '" & o.playbackInfo & "', "
        strVideoCode &= " theme: '9ae8bbe8dd964ddc9bdb932cca1cb59a',"
        strVideoCode &= " container: document.querySelector('#" & selector & "'),"
        strVideoCode &= "});"
        strVideoCode &= "</script>"
        Return strVideoCode
    End Function
End Class

Public Class myVideoList
    Public Property count As Integer
    Public Property rows As myVideo()
End Class
Public Class myVideo
    Public Property id As String
    Public Property title As String
    Public Property upload_time As Double
    Public Property length As Integer
    Public Property status As String
    Public Property posters As videoPoster()
    Public Property poster As String
    Public Property tags As String()
End Class

Public Class videoPoster
    Public Property width As Integer
    Public Property height As Integer
    Public Property posterUrl As String
End Class
