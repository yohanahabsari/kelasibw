Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://ibw.news/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class kirimemailibw
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
    <WebMethod()>
    Function kirimstandard(em As String, nama As String, catatan As String, judul As String) As String
        Dim hasil As String
        hasil = kirimEmail(em, nama, catatan, judul, AddressOf bikinisiemailstandard)
        Return hasil

    End Function
    <WebMethod()>
    Function kirimjadwal(em As String, nama As String, catatan As String, judul As String, startyyyymmdd As String, starthhss As String, endyyyymmdd As String, endhhss As String, lokasi As String, kodetemplate As Integer) As String
        Dim hasil As String
        hasil = kirimEmailKalender(em, nama, catatan, judul, startyyyymmdd, starthhss, endyyyymmdd, endhhss, lokasi, AddressOf BikinIsiEmail)
        Return hasil

    End Function
    Function BikinIsiEmail(ringkasan As String, deskripsi As String, startyyyymmdd As String, starthhss As String, endyyyymmdd As String, endhhss As String, nama As String, email As String) As String
        Dim hasil As String
        Dim s As String
        s = " <html>"
        s &= "  <body>"

        s &= "<div itemscope itemtype='http://schema.org/EventReservation'>"
        s &= "<meta itemprop='reservationNumber' content='" & "testemail" & "'/>"
        s &= "  <link itemprop ='reservationStatus' href='http://schema.org/Confirmed'/>"
        s &= "  <div itemprop = 'underName' itemscope itemtype='http://schema.org/Person'>"
        s &= "      <meta itemprop='name' content='" & nama & "'/>"
        s &= "  </div>"
        s &= "  <div itemprop = 'reservationFor' itemscope itemtype='http://schema.org/Event'>"
        s &= "    <meta itemprop='name' content='" & ringkasan & "'/>"
        s &= "    <meta itemprop = 'startDate' content='" & startyyyymmdd & "T" & starthhss & ":00+07:00'/>"
        s &= "    <meta itemprop = 'endDate' content='" & endyyyymmdd & "T" & endhhss & ":00-07:00'/>"
        s &= "                 <div itemprop = 'performer' itemscope itemtype='http://schema.org/Organization'>"
        s &= "       <meta itemprop = 'name' content='" & "Indonesian Babywearers" & "'/>"
        s &= "       <link itemprop = 'image' href='http://www.amprocktv.com/wp-content/uploads/2027/01/foo-fighters-1-680x383.jpg'/>"
        s &= "     </div>"


        s &= "    <div itemprop='location' itemscope itemtype='http://schema.org/Place'>"
        s &= "        <meta itemprop='name' content='WAG / Zoom / FB room'/>"
        s &= "        <div itemprop='address' itemscope itemtype='http://schema.org/PostalAddress'>"
        s &= "            <meta itemprop='streetAddress' content='Surabaya'/>"
        s &= "            <meta itemprop='addressLocality' content='Surabaya'/>"
        s &= "            <meta itemprop='addressRegion' content='Jawa Timur'/>"
        s &= "            <meta itemprop='postalCode' content='40245'/>"
        s &= "            <meta itemprop='addressCountry' content='ID'/>"
        s &= "        </div>"
        s &= "    </div>"
        s &= "                          </div>"
        s &= "</div>"
        s &= "                <p>"
        s &= "      Dear " & nama & ", terimakasih telah mendaftar untuk kelas kolektif Indonesian Babywearers."
        s &= "    </p>"
        s &= "    <p>"
        s &= "      Data Kelas<br/>"
        s &= "      " & deskripsi & "<br/>"
        s &= "      Untuk Komunitas: " & "untuk test" & " < br />"
        s &= "      Start time:" & startyyyymmdd & "<br/>"
        s &= "    </p>"
        s &= "  </body>"
        s &= "</html>"


        Return s
        Return hasil
    End Function
    Function bikinisiemailstandard(
    ByVal email As String,
    ByVal nama As String,
    ByVal catatan As String
) As String
        Return "test standard"
    End Function
End Class