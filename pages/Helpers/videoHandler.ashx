<%@ WebHandler Language="VB" Class="videoHandler" %>

Imports System
Imports System.Web
Imports System.Net
Imports System.IO

Public Class videoHandler : Implements IHttpHandler
    ' A function to interact with API server based paramaters
    Public Function vdocipher_sendCommand(action As String, getData As String) As String
        Dim strGetURL As String = "http://api.vdocipher.com/v2/" + action + "?" + getData
        Dim strResult As String
        Using client As New Net.WebClient
            Dim reqparm As New Specialized.NameValueCollection
            reqparm.Add("clientSecretKey", "D6w8IRNLoIOzxk4fNtg9wpTWNyNFCFBlVB4OFVVrz6NHa1pFzBLkPBLUsCe4veRD")
            Dim responsebytes = client.UploadValues(strGetURL, "POST", reqparm)
            Dim responsebody = (New Text.UTF8Encoding).GetString(responsebytes)
            strResult = responsebody
        End Using
        Return strResult
    End Function

    '
    Public Function vdocipher_player(strVideoID As String) As String
        Dim strOtp As String
        Dim strVideoCode As String = ""
        Dim strHeight As String = 480
        Dim strWidth As String = 720

        strOtp = vdocipher_sendCommand("otp", "video=" + strVideoID)
        strOtp = strOtp.Substring(8, strOtp.Length - 10)
        If Not strOtp Is Nothing Then
            strVideoCode = "<div id='vdo" + strOtp + "' ></div>"
            strVideoCode += "<scr" + "ipt src='https://de122v0opjemw.cloudfront.net/utils/playerInit.php?otp=" + strOtp + "&height=" + strHeight + "&width=" + strWidth + "'></" + "scr" + "ipt>"
        End If
        Return strVideoCode
    End Function
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/html"
        'Output the result using asp.net literal control
        Dim idvideo As String
        idvideo = "5b19804afd5b45dfba765ea9860cc6bd"
        idvideo = joanelibweb.myRequest.getData("id", "5b19804afd5b45dfba765ea9860cc6bd")
        context.Response.Write(cekVideo(context.Request("id")))
        '  context.Response.Write("Hello World")
    End Sub





    Function cekVideo(urutanid As String) As String
        'Output the result using asp.net literal control
        Dim idvideo As String
        'idvideo = "5b19804afd5b45dfba765ea9860cc6bd"
        ' idvideo = joanelibweb.myRequest.getData("id", "5b19804afd5b45dfba765ea9860cc6bd")
        Dim j As New ambilData
        idvideo = j.ambilString("select uraian from listkurikulum where urutan=" & urutanid)
        Return vdocipher_player(idvideo)
    End Function





    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class