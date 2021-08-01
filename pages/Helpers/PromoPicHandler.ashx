<%@ WebHandler Language="VB" Class="kelasPicHandlerGray" %>

Imports System.Web
Imports System.Web.Services
Imports System.Drawing
Imports System.Drawing.Bitmap
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Image
Imports System.Drawing.Imaging
Public Class kelasPicHandlerGray : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim request As HttpRequest = context.Request
        Dim id As String
        If String.IsNullOrEmpty(request.QueryString("namafile")) Then
            id = "imgerror.jpg"
        Else
            id = request.QueryString("namafile")
        End If
        If Not (String.IsNullOrEmpty(id)) Then

            Dim imageByte As Byte()

            Try
                imageByte = loadImage(id)
            Catch ex As Exception
                '  skrip = "select pic from titipanagen where kodeorder='" & id & "'"
                ' imageByte = CType(DBHelper.ambilImage(skrip), Byte())
                '  HttpContext.Current.Response.Write("3 : " & ex.Message)
                imageByte = GenerateThumbnail(System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/") & "/img/blankimg.jpg")) ' CType(DBHelper.ambilImage(skrip), Byte())

            End Try

            If imageByte IsNot Nothing AndAlso imageByte.Length > 0 Then
                context.Response.ContentType = "image/jpeg"
                context.Response.BinaryWrite(imageByte)
            Else
                context.Response.ContentType = "plain/text"
                context.Response.Write("file tidak ditemukan")
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
    Public Shared Function ScaleImageWidth(ByVal image As System.Drawing.Image, ByVal maxWidth As Integer) As System.Drawing.Image
        Try

            Dim ratio = CDbl(maxWidth) / Convert.ToDouble(image.Width)
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
    Function loadImage(namafile As String) As Byte()
        Dim img, newimg As System.Drawing.Image
        namafile = HttpContext.Current.Server.MapPath("~/") & namafile
        img = System.Drawing.Image.FromFile(namafile)
        newimg = ScaleImage(img, 400)

        Dim imageByte As Byte()

        Try
            imageByte = GenerateThumbnail(newimg)
        Catch ex As Exception
            '  skrip = "select pic from titipanagen where kodeorder='" & id & "'"
            ' imageByte = CType(DBHelper.ambilImage(skrip), Byte())
            ' HttpContext.Current.Response.Write("1 : " & ex.Message)
            imageByte = GenerateThumbnail(System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/") & "/img/blankimg.jpg")) ' CType(DBHelper.ambilImage(skrip), Byte())

        End Try

        Return imageByte
    End Function
    Private Function GenerateThumbnail(ByVal img As Image) As Byte()
        Dim loBMP As Bitmap = Nothing
        Dim bmpOut As Bitmap = Nothing
        Dim jarak, jaraky As Integer
        Dim b As Brush
        Dim frameSize As Size = New Size()

        Try
            loBMP = New Bitmap(img)
            '    Dim loFormat As System.Drawing.Imaging.ImageFormat = loBMP.RawFormat
            Dim newSize As Size = New Size()
            If HttpContext.Current.Request("mode") = "full" Then
                newSize.Height = img.Height
                newSize.Width = img.Width
                frameSize.Height = img.Height
                frameSize.Width = img.Width
            ElseIf HttpContext.Current.Request("mode") = "thumb" Then
                ' newSize.Width = HttpContext.Current.Request("fw")
                ' newSize.Height = HttpContext.Current.Request("fh")
                img = ScaleImage(img, HttpContext.Current.Request("h"))
                frameSize.Height = img.Height ' HttpContext.Current.Request("h")
                frameSize.Width = img.Width ' HttpContext.Current.Request("w")
            ElseIf HttpContext.Current.Request("mode") = "part" Then
                frameSize.Height = HttpContext.Current.Request("fh")
                frameSize.Width = HttpContext.Current.Request("fw")

            Else
                frameSize.Height = 300 'img.Height
                frameSize.Width = 400 'img.Width
            End If
            jaraky = 0
            jarak = Math.Truncate((400 - (img.Width)) / 2)

            ' bmpOut = New Bitmap(newSize.Width, newSize.Height)
            bmpOut = New Bitmap(frameSize.Width, frameSize.Height)

            Dim canvas As Graphics = Graphics.FromImage(bmpOut)
            canvas.SmoothingMode = SmoothingMode.AntiAlias
            canvas.InterpolationMode = InterpolationMode.HighQualityBicubic
            canvas.PixelOffsetMode = PixelOffsetMode.HighQuality
            '   canvas.

            Dim size2 As Size = New Size()

            size2.Width = img.Width
            If HttpContext.Current.Request("mode") = "full" Then
                size2.Height = img.Height
                jarak = 0
            ElseIf HttpContext.Current.Request("mode") = "thumb" Then

                '  img = ScaleImage(img, HttpContext.Current.Request("h"))
                jarak = 0

                size2.Width = img.Width '(HttpContext.Current.Request("h") / img.Height) * img.Width
                size2.Height = img.Height  'HttpContext.Current.Request("h")
            ElseIf HttpContext.Current.Request("mode") = "part" Then
                If HttpContext.Current.Request("h") < HttpContext.Current.Request("fh") Then
                    img = ScaleImage(img, HttpContext.Current.Request("fh"))
                Else
                    img = ScaleImage(img, HttpContext.Current.Request("h"))
                End If
                jarak = Math.Truncate((img.Width - HttpContext.Current.Request("fw")) / 2) * (-1)
                jaraky = Math.Truncate((img.Height - HttpContext.Current.Request("fh")) / 2) * (-1)
                size2.Width = img.Width '(HttpContext.Current.Request("h") / img.Height) * img.Width
                size2.Height = img.Height  'HttpContext.Current.Request("h")
            Else
                size2.Height = (400 / img.Width) * img.Height
            End If

            canvas.FillRectangle(Brushes.White, 0, 0, frameSize.Width, frameSize.Height)
            canvas.DrawImage(loBMP, New Rectangle(New Point(jarak, jaraky), size2))

            'canvas.DrawString("copyright:IBW", New Font("Arial", 16), Brushes.Gray, New Point(25, 25))
            Try
                If InStr(HttpContext.Current.Request.UrlReferrer.ToString, "mulaikelas") > 0 Then
                ElseIf InStr(HttpContext.Current.Request.UrlReferrer.ToString, "kelasoffline") > 0 Then
                Else
                    canvas.DrawString("dari kelas IBW", New Font("Verdana", 12,
    FontStyle.Bold), New SolidBrush(Color.Beige), ((size2.Width / 4)),
    (size2.Height / 3))
                    canvas.DrawString("dibuka oleh:" & joanelibweb.mycookies.getData("username"), New Font("Verdana", 14,
    FontStyle.Bold), New SolidBrush(Color.Beige), ((size2.Width / 6)),
    (size2.Height / 2))
                End If
            Catch ex As Exception
                canvas.DrawString("dari kelas IBW", New Font("Verdana", 14,
FontStyle.Bold), New SolidBrush(Color.Beige), ((size2.Width / 4)),
(size2.Height / 3))
                canvas.DrawString("dibuka oleh:" & joanelibweb.mycookies.getData("username"), New Font("Verdana", 14,
FontStyle.Bold), New SolidBrush(Color.Beige), ((size2.Width / 6)),
(size2.Height / 2))
            End Try


        Catch ex As Exception
            HttpContext.Current.Response.Write("2 : " & ex.Message)
        End Try

        bmpOut = MakeGrayscale(bmpOut)

        Dim imageStream As MemoryStream = New MemoryStream()
        bmpOut.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim imageContent As Byte() = New Byte(imageStream.Length - 1) {}
        imageStream.Position = 0
        imageStream.Read(imageContent, 0, CInt(imageStream.Length))
        Return imageContent
    End Function
    Public Shared Function MakeGrayscale(ByVal original As Bitmap) As Bitmap
        'create a blank bitmap the same size as original
        Dim newBitmap As Bitmap = New Bitmap(original.Width, original.Height)

        'get a graphics object from the new image
        Dim g As Graphics = Graphics.FromImage(newBitmap)

        'create the grayscale ColorMatrix
        Dim colorMatrix As ColorMatrix = New ColorMatrix(New Single()() {New Single() {0.3F, 0.3F, 0.3F, 0, 0}, New Single() {0.59F, 0.59F, 0.59F, 0, 0}, New Single() {0.11F, 0.11F, 0.11F, 0, 0}, New Single() {0, 0, 0, 1, 0}, New Single() {0, 0, 0, 0, 1}})

        'create some image attributes
        Dim attributes As ImageAttributes = New ImageAttributes()

        'set the color matrix attribute
        attributes.SetColorMatrix(colorMatrix)

        'draw the original image on the new image
        'using the grayscale color matrix
        g.DrawImage(original, New Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes)

        'dispose the Graphics object
        g.Dispose()
        Return newBitmap
    End Function

End Class


