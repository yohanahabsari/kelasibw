<%@ WebHandler Language="VB" Class="InstantA" %>



Imports System
Imports System.Web
Imports System.Xml
Imports System.Text
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Public Class InstantA : Implements IHttpHandler
    Dim dt As New DataTable
    Dim ad As ambilData
    Dim skrip As String
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        BuildFeedXML(context, 1)
    End Sub

    Private Sub BuildFeedXML(context As HttpContext, channelId As Integer)
        Using writer As New XmlTextWriter(context.Response.OutputStream, Encoding.UTF8)
            ad = New ambilData
            skrip = "select " & ad.namakolomlengkap("rsschanel") & " from rsschanel where id=1"
            Dim dt As DataTable = ad.ambilData(skrip) 'GetData("SELECT * FROM Channel WHERE Id = @ChannelId", channelId)
            writer.WriteStartDocument()
            writer.WriteStartElement("rss")
            writer.WriteAttributeString("version", "2.0")
            writer.WriteAttributeString("xmlns:content", "http://purl.org/rss/1.0/modules/content/")
            writer.WriteStartElement("channel")
            writer.WriteElementString("title", dt.Rows(0)("Title").ToString())
            writer.WriteElementString("link", " https://ibw.news/" & "rss" & "/" & context.Server.UrlEncode(dt.Rows(0).Item("title")))
            writer.WriteElementString("description", dt.Rows(0)("Description").ToString())
            ' writer.WriteElementString("copyright", dt.Rows(0)("Copyright").ToString())
            writer.WriteElementString("language", dt.Rows(0)("language").ToString())
            writer.WriteElementString("lastBuildDate", dt.Rows(0)("lastBuildDate").ToString())
            skrip = "select " & ad.namakolomlengkap ("rssfeed") & " from rssfeed where id=1"
            dt = ad.ambilData(skrip) 'GetData("SELECT * FROM Feeds WHERE ChannelId = @ChannelId", channelId)
            For Each dr As DataRow In dt.Rows
                writer.WriteStartElement("item")
                writer.WriteElementString("title", " https://ibw.news/" & "rss" & "/" & context.Server.UrlEncode(dt.Rows(0).Item("title")))
                writer.WriteElementString("description", dr("Description").ToString())
                writer.WriteElementString("link", dr("Link").ToString())
                writer.WriteElementString("guid", dr("Id").ToString())
                writer.WriteElementString("pubDate", dr("pubDaterss").ToString) 'Convert.ToDateTime(dr("pubDate")).ToString("R")
                writer.WriteStartElement("content:encoded")
                writer.WriteCData(ambilisi(dr("Id").ToString()))
                writer.WriteEndElement()
                writer.WriteEndElement()
            Next

            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Flush()
            writer.Close()
        End Using
    End Sub
    Private Function ambilisi(idrssfeed As Integer) As String
        Dim g As New myGrabbernew
        Dim hasil As String
        hasil = g.getPage("https://ibw.news/pages/instantarticles.aspx?id=" & idrssfeed)
        Return hasil
    End Function
    Private Function GetData(query As String, channelId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@ChannelId", channelId)
                cmd.Connection = con
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function


    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class