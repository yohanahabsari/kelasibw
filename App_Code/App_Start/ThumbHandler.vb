Imports Microsoft.VisualBasic
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Web
Imports System.Web.Services

Public Class ThumbHandler
    Implements System.Web.IHttpHandler

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim h As Integer = Integer.Parse(context.Request.QueryString("h").ToString())
        Dim w As Integer = Integer.Parse(context.Request.QueryString("w").ToString())
        Dim file As String = context.Request.QueryString("file").ToString()
        Dim filePath As String = context.Server.MapPath("~/images/" & file)

        Using img As System.Drawing.Image = System.Drawing.Image.FromFile(filePath)
            Dim objBmp As Bitmap = New Bitmap(img, w, h)
            Dim extension As String = Path.GetExtension(filePath)
            Dim ms As MemoryStream
            Dim bmpBytes As Byte()

            Select Case extension.ToLower()
                Case ".jpg", ".jpeg"
                    ms = New MemoryStream()
                    objBmp.Save(ms, ImageFormat.Jpeg)
                    bmpBytes = ms.GetBuffer()
                    context.Response.ContentType = "image/jpeg"
                    context.Response.BinaryWrite(bmpBytes)
                    objBmp.Dispose()
                    ms.Close()
                    context.Response.[End]()
                Case ".png"
                    ms = New MemoryStream()
                    objBmp.Save(ms, ImageFormat.Png)
                    bmpBytes = ms.GetBuffer()
                    context.Response.ContentType = "image/png"
                    context.Response.BinaryWrite(bmpBytes)
                    objBmp.Dispose()
                    ms.Close()
                    context.Response.[End]()
                Case ".gif"
                    ms = New MemoryStream()
                    objBmp.Save(ms, ImageFormat.Gif)
                    bmpBytes = ms.GetBuffer()
                    context.Response.ContentType = "image/png"
                    context.Response.BinaryWrite(bmpBytes)
                    objBmp.Dispose()
                    ms.Close()
                    context.Response.[End]()
            End Select

            img.Dispose()
        End Using
    End Sub


    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Throw New NotImplementedException()
        End Get
    End Property
End Class

