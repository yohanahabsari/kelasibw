<%@ WebHandler Language="VB" Class="komlokcoverhandler" %>

Imports System.Web
Imports System.Web.Services
Imports System.Drawing
Imports System.Drawing.Bitmap
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Image

Public Class komlokcoverhandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim request As HttpRequest = context.Request
        Dim id As String
        If String.IsNullOrEmpty(request.QueryString("komlokid")) Then
            id = "0"
        Else
            id = request.QueryString("komlokid")
        End If
        If Not (String.IsNullOrEmpty(id)) Then
            Dim skrip As String
            skrip = "select cover from komlok where id='" & id & "'"
            'skrip = "select pic from titipanagen where kodeorder='" & id & "'"
            Dim hash As Hashtable = New Hashtable()
            hash.Add("@imageID", id)
            Dim DBHelper As New ambilData 'DataBaseHelper = New DataBaseHelper()
            Dim imageByte As Byte()

            ' Try
            imageByte = CType(DBHelper.ambilImage(skrip), Byte())
            '  Catch ex As Exception
            '  skrip = "select pic from titipanagen where kodeorder='" & id & "'"
            ' imageByte = CType(DBHelper.ambilImage(skrip), Byte())
            ' End Try

            If imageByte IsNot Nothing AndAlso imageByte.Length > 0 Then
                context.Response.ContentType = "image/jpeg"
                context.Response.BinaryWrite(imageByte)
            Else
                context.Response.ContentType = "plain/text"
                context.Response.Write(skrip)
            End If
        End If


    End Sub
    Public Shared Function ScaleImage(ByVal image As System.Drawing.Image, ByVal maxHeight As Integer) As System.Drawing.Image
        Try

            Dim ratio = CDbl(maxHeight) / Convert.ToDouble(image.Height)
            Dim newWidth = CInt((Convert.ToDouble(image.Width) * ratio))
            Dim newHeight = CInt((Convert.ToDouble(image.Height) * ratio))
            Dim newImage = New Bitmap(newWidth, newHeight)
            Using g = Graphics.FromImage(newImage)
                g.DrawImage(image, 0, 0, newWidth, newHeight)
            End Using

            Return newImage
        Catch ex As Exception
            Return image
        End Try

    End Function
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class