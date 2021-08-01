
Imports System.Net
Imports Newtonsoft.Json
Partial Class Pages_testvideo
    Inherits System.Web.UI.Page

    Private Sub Pages_testvideo_Load(sender As Object, e As EventArgs) Handles Me.Load
        '  j0IiBZEwmLgs1rBNYDznsbP3cpsfGAqFRbEYT7IoMi9Z2vMGbWHAGzDZVuYKbnJJ
        Dim j As New myGrabbernew ' joanelibweb.myGrabber
        Dim hasil As String
        Dim obyOtp As getotp
        Dim idvideo As String
        idvideo = "5b19804afd5b45dfba765ea9860cc6bd"
        Dim a As New NetworkCredential
        a.UserName = "Apisecret"
        a.Password = "j0IiBZEwmLgs1rBNYDznsbP3cpsfGAqFRbEYT7IoMi9Z2vMGbWHAGzDZVuYKbnJJ"
        hasil = j.getPageJsonwithKey("https://dev.vdocipher.com/api/videos/" & idvideo & "/otp", a.UserName & " " & a.Password)
        Dim ss As New joanelibweb.myJson
        obyOtp = JsonConvert.DeserializeObject(Of getotp)(hasil)

        ' testku.Text = (getSkrip(obyOtp))
        getvdocipher.idvideo = "5b19804afd5b45dfba765ea9860cc6bd"
        Dim isian As String = ""
        Dim k As New myVideoCipher
        Dim g As myVideoList

        isian = "<table>"
        For i As Integer = 1 To 6
            g = k.getVideos("set" & i, 30)
            For Each r As myVideo In g.rows
                isian &= "<tr><td>" & r.title & "<img src='" & r.poster & "' alt='" & r.title & "'/></td><td>" & r.id & "</td></tr>"
            Next
        Next

        isian &= "</table>"
        isiVid.Text = isian
    End Sub
    Structure getotp
        Dim otp As String
        Dim playbackInfo As String
    End Structure

    Function getSkrip(o As getotp) As String
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
    Public Function GenerateRandomString(ByRef lenStr As Integer, Optional ByVal upper As Boolean = False) As String
        'use
        'TextBox1.Text = GenerateRandomString(18)
        Dim rand As New Random()
        Dim allowableChars() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray()
        '"abcdefghighlmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim final As New System.Text.StringBuilder
        Do
            'final += allowableChars(rand.Next(allowableChars.Length - 1))
            final.Append(allowableChars(rand.Next(0, allowableChars.Length)))
        Loop Until final.Length = lenStr
        ' Debug.WriteLine(final.Length)
        Return If(upper, final.ToString.ToUpper(), final.ToString)
    End Function
End Class
