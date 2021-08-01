<%@ WebHandler Language="VB" Class="certhandler" %>


Imports System.Web
Imports System.Web.Services
Imports System.Drawing
Imports System.Drawing.Bitmap
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Image
Public Class certhandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim request As HttpRequest = context.Request
        Dim id As String
        If String.IsNullOrEmpty(request.QueryString("namafile")) Then
            id = "img/imgerror.jpg"
        Else
            id = request.QueryString("namafile")
        End If

        If Not (String.IsNullOrEmpty(id)) Then

            Dim imageByte As Byte()

            Try
                imageByte = GenerateCertificate(loadImage(id), loadFrame("img/framekelas/framekelas" & HttpContext.Current.Request("kelasid").ToString & ".jpg"))

            Catch ex As Exception
                '  skrip = "select pic from titipanagen where kodeorder='" & id & "'"
                ' imageByte = CType(DBHelper.ambilImage(skrip), Byte())
                ' HttpContext.Current.Response.Write("3 : " & ex.Message)
                imageByte = GenerateThumbnail(System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/") & "img/blankimg.jpg"), "full") ' CType(DBHelper.ambilImage(skrip), Byte())

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
    Function loadFrame(namafile As String) As Image
        Dim img, newimg As System.Drawing.Image
        namafile = HttpContext.Current.Server.MapPath("~/") & namafile
        img = System.Drawing.Image.FromFile(namafile)
        newimg = ScaleImageWidth(img, 800)

        Return newimg
    End Function

    Function loadImage(namafile As String) As Image
        Dim img, newimg As System.Drawing.Image

        Dim folderPath As String = HttpContext.Current.Server.MapPath("~/app_data/fotokelas/")
        namafile = folderPath & namafile
        img = System.Drawing.Image.FromFile(namafile)
        newimg = ScaleImage(img, 400)

        Return newimg
    End Function
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
    Private Function GenerateThumbnail(ByVal img As Image, mode As String) As Byte()
        Dim loBMP As Bitmap = Nothing
        Dim bmpOut As Bitmap = Nothing
        Dim jarak, jaraky As Integer
        Dim b As Brush
        Dim frameSize As Size = New Size()

        Try
            loBMP = New Bitmap(img)
            '    Dim loFormat As System.Drawing.Imaging.ImageFormat = loBMP.RawFormat
            Dim newSize As Size = New Size()
            If mode = "full" Then
                newSize.Height = img.Height
                newSize.Width = img.Width
                frameSize.Height = img.Height
                frameSize.Width = img.Width
            ElseIf mode = "thumb" Then
                ' newSize.Width = HttpContext.Current.Request("fw")
                ' newSize.Height = HttpContext.Current.Request("fh")
                img = ScaleImage(img, HttpContext.Current.Request("h"))
                frameSize.Height = img.Height ' HttpContext.Current.Request("h")
                frameSize.Width = img.Width ' HttpContext.Current.Request("w")
            ElseIf mode = "part" Then
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



        Catch ex As Exception
            HttpContext.Current.Response.Write("2 : " & ex.Message)
        End Try

        Dim imageStream As MemoryStream = New MemoryStream()
        bmpOut.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim imageContent As Byte() = New Byte(imageStream.Length - 1) {}
        imageStream.Position = 0
        imageStream.Read(imageContent, 0, CInt(imageStream.Length))
        Return imageContent
    End Function
    Private Function GenerateCertificate(ByVal img As Image, frameimg As Image) As Byte()
        Dim loBMP As Bitmap = Nothing
        Dim bmpOut As Bitmap = Nothing
        Dim logoBMP As Bitmap = Nothing
        Dim jarak, jaraky As Integer
        Dim b As Brush
        Dim frameSize As Size = New Size()
        Dim imgLogo, newImagelogo As Image
        Dim namafile As String
        ' Try
        Dim folderPath As String = HttpContext.Current.Server.MapPath("~/img/")
        namafile = folderPath & "logocert.png"
        imgLogo = System.Drawing.Image.FromFile(namafile)
        newImagelogo = ScaleImage(imgLogo, 120)
        loBMP = New Bitmap(img)
        bmpOut = New Bitmap(frameimg)
        '    Dim loFormat As System.Drawing.Imaging.ImageFormat = loBMP.RawFormat
        Dim fotoSize As Size = New Size()
        logoBMP = New Bitmap(newImagelogo)
        fotoSize.Height = img.Height
        fotoSize.Width = img.Width
        frameSize.Height = frameimg.Height
        frameSize.Width = frameimg.Width

        jaraky = 88 'Math.Truncate((frameimg.Height - (img.Height)) / 2) - 80
        jarak = (Math.Truncate((830 - (img.Width)) / 2)) 'Math.Truncate((frameimg.Width - (img.Width)) / 2)

        ' bmpOut = New Bitmap(fotoSize.Width, fotoSize.Height)
        bmpOut = New Bitmap(frameimg)
        Dim canvas As Graphics = Graphics.FromImage(bmpOut)
        canvas.SmoothingMode = SmoothingMode.AntiAlias
        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic
        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality
        '   canvas.

        Dim size2 As Size = New Size()
        Dim sizeLogo As Size = New Size()
        size2.Width = img.Width
        size2.Height = img.Height
        sizeLogo.Height = newImagelogo.Height
        sizeLogo.Width = newImagelogo.Width
        canvas.DrawImage(loBMP, New Rectangle(New Point(jarak, jaraky), size2))
        canvas.DrawImage(logoBMP, New Rectangle(New Point(260, 390), sizeLogo))
        canvas.FillRectangle(Brushes.Beige, 380, 400, 410, 120)
        canvas.FillRectangle(Brushes.Beige, 380, 77, 410, 30)
        canvas.DrawString("ONLINE WORKSHOP INDONESIAN BABYWEARERS", New Font("Monoscript", 11,
FontStyle.Bold), New SolidBrush(Color.Black), 394,
82)
        ' canvas.DrawString("CONGRATULATIONS", New Font("Verdana", 12,
        ' FontStyle.Bold), New SolidBrush(Color.BlueViolet), 430,
        '430)
        ' canvas.DrawImage(logoBMP, New Rectangle(New Point(jarak, jaraky), size2))
        Dim namaku As String
        namaku = HttpContext.Current.Request("nama")
        namaku = StrConv(namaku, vbProperCase)
        canvas.DrawString(namaku, New Font("Script", 20,
    FontStyle.Bold), New SolidBrush(Color.Black), 410,
    410)
        canvas.DrawString("telah menyelesaikan kelas: ", New Font("Verdana", 14,
FontStyle.Bold), New SolidBrush(Color.DarkSlateBlue), 410,
445)
        Dim namakelas As String
        namakelas = HttpContext.Current.Request("namakelas")
        If namakelas.Length > 26 Then
            Dim letakspasi As Integer
            letakspasi = InStrRev(Mid(namakelas, 1, 26), " ")
            namakelas = Mid(namakelas, 1, letakspasi)
        End If
        canvas.DrawString(namakelas, New Font("Times New Roman", 20,
    FontStyle.Bold), New SolidBrush(Color.Black), 410,
    472)
        'canvas.DrawString("copyright:IBW", New Font("Arial", 16), Brushes.Gray, New Point(25, 25))



        '  Catch ex As Exception
        '     HttpContext.Current.Response.Write("2 : " & ex.Message)
        ' End Try

        Dim imageStream As MemoryStream = New MemoryStream()
        bmpOut.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        Dim imageContent As Byte() = New Byte(imageStream.Length - 1) {}
        imageStream.Position = 0
        imageStream.Read(imageContent, 0, CInt(imageStream.Length))
        Return imageContent
    End Function
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class