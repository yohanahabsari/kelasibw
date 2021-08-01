Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Net
Imports System.Web
Imports System.Web.UI
Imports System.IO


Public Class myGrabbernew
    Inherits joanelibweb.myGrabber

    Overloads Function getPagewithKey(ByVal url As String, key As String) As String

        ' We'll use WebClient class for reading HTML of web page
        Dim MyWebClient As WebClient = New WebClient()
        ' MyWebClient.Credentials = keypair ' CredentialCache.DefaultCredentials
        MyWebClient.Headers.Add("Authorization", key)
        MyWebClient.Headers.Add("User-Agent", "Mozilla/5.0")
        ' My
        ' Read web page HTML to byte array
        Dim PageHTMLBytes() As Byte
        Try
            PageHTMLBytes = MyWebClient.DownloadData(url)

            ' Convert result from byte array to string
            ' and display it in TextBox txtPageHTML
            Dim oUTF8 As UTF8Encoding = New UTF8Encoding()
            Return oUTF8.GetString(PageHTMLBytes)
        Catch ex As Exception
            Return "error : " & ex.Message & " untuk url :" & url
        End Try
        MyWebClient = Nothing
        MyWebClient.Dispose()
    End Function
    Overloads Function getPageJson(ByVal url As String) As String

        ' We'll use WebClient class for reading HTML of web page
        Dim MyWebClient As WebClient = New WebClient()
        ' MyWebClient.Credentials = keypair ' CredentialCache.DefaultCredentials
        ' MyWebClient.Headers.Add("Authorization", key)
        MyWebClient.Headers.Add("Accept", "application/json")
        MyWebClient.Headers.Add("Content-Type", "application/json")
        MyWebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0")
        ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        ' My
        ' Read web page HTML to byte array
        Dim PageHTMLBytes() As Byte
        Try
            PageHTMLBytes = MyWebClient.DownloadData(url)

            ' Convert result from byte array to string
            ' and display it in TextBox txtPageHTML
            Dim oUTF8 As UTF8Encoding = New UTF8Encoding()
            Return oUTF8.GetString(PageHTMLBytes)
        Catch ex As Exception
            Return "error : " & ex.Message & " untuk url :" & url
        End Try
        MyWebClient = Nothing
        MyWebClient.Dispose()
    End Function
    Overloads Function getPageJsonwithKey(ByVal url As String, key As String) As String

        ' We'll use WebClient class for reading HTML of web page
        Dim MyWebClient As WebClient = New WebClient()
        ' MyWebClient.Credentials = keypair ' CredentialCache.DefaultCredentials
        MyWebClient.Headers.Add("Authorization", key)
        MyWebClient.Headers.Add("Accept", "application/json")
        MyWebClient.Headers.Add("Content-Type", "application/json")
        MyWebClient.Headers.Add("User-Agent", "Mozilla/5.0")
        ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        ' My
        ' Read web page HTML to byte array
        Dim PageHTMLBytes() As Byte
        Try
            PageHTMLBytes = MyWebClient.DownloadData(url)

            ' Convert result from byte array to string
            ' and display it in TextBox txtPageHTML
            Dim oUTF8 As UTF8Encoding = New UTF8Encoding()
            Return oUTF8.GetString(PageHTMLBytes)
        Catch ex As Exception
            Return "error : " & ex.Message & " untuk url :" & url
        End Try
        MyWebClient = Nothing
        MyWebClient.Dispose()
    End Function
    Overloads Function getPageJsonwithKey(ByVal url As String, key As String, dataReq As String) As String

        ' We'll use WebClient class for reading HTML of web page
        Dim MyWebClient As WebClient = New WebClient()
        ' MyWebClient.Credentials = keypair ' CredentialCache.DefaultCredentials
        MyWebClient.Headers.Add("Authorization", key)
        MyWebClient.Headers.Add("Accept", "application/json")
        MyWebClient.Headers.Add("Content-Type", "application/json")
        MyWebClient.Headers.Add("User-Agent", "Mozilla/5.0")
        ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        ' My
        ' Read web page HTML to byte array
        Dim PageHTMLBytes() As Byte

        Dim data As Byte()
        data = System.Text.ASCIIEncoding.ASCII.GetBytes(dataReq)
        Try
            PageHTMLBytes = MyWebClient.UploadData(url, "post", data)
            '  PageHTMLBytes = MyWebClient.DownloadData(url)

            ' Convert result from byte array to string
            ' and display it in TextBox txtPageHTML
            Dim oUTF8 As UTF8Encoding = New UTF8Encoding()
            Return oUTF8.GetString(PageHTMLBytes)
        Catch ex As Exception
            Return "error : " & ex.Message & " untuk url :" & url
        End Try
        MyWebClient = Nothing
        MyWebClient.Dispose()
    End Function
    Overloads Function getPageJsonwithKey(ByVal url As String, keys As WebHeaderCollection, ishttps As Boolean) As String

        ' We'll use WebClient class for reading HTML of web page
        Dim MyWebClient As WebClient = New WebClient()
        ' MyWebClient.Credentials = keypair ' CredentialCache.DefaultCredentials
        MyWebClient.Headers.Add(keys)
        MyWebClient.Headers.Add("Accept", "application/json")
        MyWebClient.Headers.Add("Content-Type", "application/json")
        If ishttps Then
            ServicePointManager.Expect100Continue = True
            'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        End If

        ' My
        ' Read web page HTML to byte array
        Dim PageHTMLBytes() As Byte
        Try
            PageHTMLBytes = MyWebClient.DownloadData(url)

            ' Convert result from byte array to string
            ' and display it in TextBox txtPageHTML
            Dim oUTF8 As UTF8Encoding = New UTF8Encoding()
            Return oUTF8.GetString(PageHTMLBytes)
        Catch ex As Exception
            Return "error : " & ex.Message & " untuk url :" & url
        End Try
        MyWebClient = Nothing
        MyWebClient.Dispose()
    End Function
    Overloads Function getPagewithKeyHttps(ByVal url As String, keypair As NetworkCredential) As String

        ' We'll use WebClient class for reading HTML of web page
        Dim MyWebClient As WebClient = New WebClient()
        MyWebClient.Credentials = keypair ' CredentialCache.DefaultCredentials
        MyWebClient.Headers.Add("User-Agent", "Mozilla/5.0")
        'MyWebClient.
        ' MyWebClient.
        ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        ' Read web page HTML to byte array
        Dim PageHTMLBytes() As Byte
        Try
            PageHTMLBytes = MyWebClient.DownloadData(url)

            ' Convert result from byte array to string
            ' and display it in TextBox txtPageHTML
            Dim oUTF8 As UTF8Encoding = New UTF8Encoding()
            Return oUTF8.GetString(PageHTMLBytes)
        Catch ex As Exception
            Return "error : " & ex.Message & " untuk url :" & url
        End Try
        MyWebClient = Nothing
        MyWebClient.Dispose()
    End Function
End Class
