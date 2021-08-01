Imports Microsoft.VisualBasic
Imports System.Data
Public Class initsesi
    Dim j As New ambilData
    Dim skrip As String
    Dim _nama As String
    Dim _email As String
    Dim _id As String
    Dim _kelasid As Integer
    Dim _kodepromo As String
    Dim dt As DataTable
    Public listparamdata As List(Of paramdata)
    Public newEmail As String
    Public newNama As String
    Public isSuper As Integer
    Public Sub New()
        MyBase.New
        'lihat sesi di database ambil masukkan ke dt
        setinit()
        '  updateSesi()
    End Sub
    Public Sub setinit()

        ' If CurNama = "" Or CurEmail = "" Then
        j = New ambilData
        '  Dim dt As New DataTa

        If _email = "" Then
            Try
                _email = joanelibweb.mycookies.getData("email")
            Catch ex As Exception

            End Try
        Else
            skrip = "select email from  sesi where sesiid='" & Me.sesiID & "'"
            _email = j.ambilString(skrip)
        End If

        If _email <> "" Then
            skrip = "select  member.nama as nama, member.email as email, member.id as id ,SESI.CURKELASID AS CURKELASID," &
                " SESI.CURKODEPROMO as curkodepromo, member.issuper from sesi left join member on sesi.email =member.email " &
                "where sesi.email='" & CurEmail & "'   order by email desc"
            '  Throw New Exception(skrip)
        Else
            skrip = "select  member.nama as nama, member.email as email, member.id as id ,SESI.CURKELASID AS CURKELASID,SESI.CURKODEPROMO " &
                " as curkodepromo,member.issuper  from sesi left join member on sesi.email =member.email where sesiid='" & Me.sesiID & "' order by email desc"

        End If
        dt = j.ambilData(skrip)
        '     
        Try
            If dt.Rows.Count > 0 Then
                ' Throw New Exception(skrip)
                CurNama = dt.Rows(0).Item("nama")
                CurEmail = dt.Rows(0).Item("email")
                CurID = dt.Rows(0).Item("id")
                CurKelasID = dt.Rows(0).Item("curkelasid")
                Curkodepromo = dt.Rows(0).Item("curkodepromo")
                isSuper = If(dt.Rows(0).Item("issuper"), 0)
                Dim adasesi As Boolean = False
                For Each row As DataRow In dt.Rows
                    If row("sesiid") = sesiID Then
                        adasesi = True
                        Exit For
                    End If
                Next
                If adasesi = False Then
                    '  skrip = "update sesi set sesiid='" & Me.sesiID & "' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
                    skrip = "insert into sesi (sesiid,lastpage,email)values('" & Me.sesiID & "','" & joanelibweb.myRequest.getcurURL & "','" & CurEmail & "')"
                    j.updateData(skrip)
                End If

            Else
                skrip = "insert into sesi (sesiid,lastpage)values('" & Me.sesiID & "','" & joanelibweb.myRequest.getcurURL & "')"

                j.updateData(skrip)
                Try
                    skrip = "select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat from sesi where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "' "
                    dt = j.ambilData(skrip)
                Catch ex As Exception
                    skrip = "Select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat from sesi where 1=2" 'email='" & CurEmail & "' or sesiid='" & Me.sesiID & "' "
                    dt = j.ambilData(skrip)
                End Try
            End If
            skrip = "select " & j.namakolomlengkap("sesi") & " from sesi where  email='" & CurEmail & "' or sesiid='" & sesiID & "'"
            dt = j.ambilData(skrip)
        Catch ex As Exception
            ' Throw New Exception(skrip) 'ex.Message & "##" & ex.StackTrace)
            'CurNama & "-" & dt.Rows(0).Item("nama")
        End Try

    End Sub
    Public Sub New(email As String, Optional nama As String = "")
        MyBase.New
        Me.newNama = nama
        Me.newEmail = email
        Me.CurEmail = email
        ' setinit()
        updateSesi()
    End Sub
    Sub updateSesi()
        Dim j As New ambilData
        Dim skrip As String
        Dim dt As New DataTable

        '  skrip = "select * from sesi where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
        '  dt = j.ambilData(skrip)
        If InStr(joanelibweb.myRequest.getcurURL, "https://ibw.news/keepalive.aspx") < 1 Then
            If CurEmail <> newEmail Then
                skrip = "update sesi set email='" & Me.newEmail & "' where  sesiid='" & Me.sesiID & "'"
                j.updateData(skrip)
                CurEmail = newEmail
            Else
                If CurNama = "" Then
                    skrip = "update sesi set sesiid='" & Me.sesiID & "', email='" & CurEmail & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
                Else
                    skrip = "update sesi set nama='" & CurNama & "', sesiid='" & Me.sesiID & "', email='" & CurEmail & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"

                End If
                j.updateData(skrip)
            End If

        Else
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("email") <> CurEmail Or dt.Rows(0).Item("sesiid") <> sesiID Then
                    skrip = "update sesi set sesiid='" & Me.sesiID & "', email='" & CurEmail & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
                    j.updateData(skrip)
                End If
            End If
        End If

        HttpContext.Current.Session.Item("email") = CurEmail

        HttpContext.Current.Session.Item("nama") = CurNama
        joanelibweb.mycookies.setData("", "email", CurEmail, 300000)
        joanelibweb.mycookies.setData("", "nama", CurNama, 300000)

        skrip = "select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat  from sesi where email='" & CurNama & "' or sesiid='" & Me.sesiID & "'"
        dt = j.ambilData(skrip)
    End Sub


    Public Sub New(email As String)
        MyBase.New
        '  Me.CurNama = CurNama
        Me.newEmail = email
        ' Me.CurEmail = email
        updateSesi()
    End Sub
    Public Property CurID As String
        Get
            If _id = "" Then
                ' Return HttpContext.Current.Session.Item("nama")
                Return ""
            Else
                Return _id

            End If
        End Get
        Set(ByVal value As String)


            'Session.Item("nama") = value
            _id = value


        End Set
    End Property
    Public Property CurNama As String
        Get
            If _nama = "" Then
                ' Return HttpContext.Current.Session.Item("nama")
                Return ""
            Else
                Return _nama

            End If
        End Get
        Set(ByVal value As String)
            updateSesiParam("nama", value, CurEmail)

            'Session.Item("nama") = value
            _nama = value
            joanelibweb.mycookies.setData("/", "nama", _nama, 7200)

        End Set
    End Property
    Public Property CurEmail As String
        Get

            Return _email
        End Get
        Set(ByVal value As String)

            _email = value

            Try
                updateSesiParam("email", value, CurEmail)
                joanelibweb.mycookies.setData("/", "email", _email, 7200)
            Catch ex As Exception

            End Try
        End Set
    End Property
    Public ReadOnly Property sesiID As String
        Get
            Try
                Return HttpContext.Current.Session.SessionID
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    Public Property CurKelasID As Integer
        Get
            If _kelasid = 0 Then
                ' Return HttpContext.Current.Session.Item("nama")
                Return 0
            Else
                Return _kelasid

            End If
        End Get
        Set(ByVal value As Integer)


            'Session.Item("nama") = value
            _kelasid = value
            Try
                updateSesiParam("curkelasid", value, CurEmail)
            Catch ex As Exception

            End Try

        End Set
    End Property

    Public Property Curkodepromo As String
        Get
            If _kodepromo = "" Then
                ' Return HttpContext.Current.Session.Item("nama")
                Return ""
            Else
                Return _kodepromo

            End If
        End Get
        Set(ByVal value As String)


            'Session.Item("nama") = value
            _kodepromo = value
            Try
                updateSesiParam("curkodepromo", value, CurEmail)
            Catch ex As Exception

            End Try

        End Set
    End Property


    Sub updateSesiParam(namaParam As String, nilaiParam As String, email As String, Optional paksaUpdate As Boolean = True, Optional showCookies As Boolean = True)
        Dim j As New ambilData
        Dim skrip As String
        Dim dt As New DataTable
        skrip = "select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat from sesi where email='" & email & "' or sesiid='" & Me.sesiID & "'"
        dt = j.ambilData(skrip)
        If joanelibweb.myRequest.getcurURL <> "https://ibw.news/keepalive.aspx" Then
            If paksaUpdate Then
                skrip = "update sesi set sesiid='" & Me.sesiID & "', " & namaParam & "='" & nilaiParam & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & email & "' or sesiid='" & Me.sesiID & "'"
                j.updateData(skrip)

            Else
                If dt.Rows(0).Item(namaParam) <> nilaiParam Then
                    skrip = "update sesi set sesiid='" & Me.sesiID & "', " & namaParam & "='" & nilaiParam & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & email & "' or sesiid='" & Me.sesiID & "'"
                    j.updateData(skrip)
                End If
            End If
        End If



        '  HttpContext.Current.Session.Item("email") = CurEmail
        ' HttpContext.Current.Session.Item("nama") = CurNama
        If showCookies Then
            ' HttpContext.Current.Session.Item(namaParam) = nilaiParam
            joanelibweb.mycookies.setData("/", namaParam, nilaiParam, 300000)
        End If

        joanelibweb.mycookies.setData("/", "email", CurEmail, 300000)
        joanelibweb.mycookies.setData("/", "nama", CurNama, 300000)

    End Sub
    Sub updateDariCookies()
        Dim j As New ambilData
        Dim skrip As String
        Dim dt As New DataTable
        Dim emailtersimpan As String
        Dim namatersimpan As String
        emailtersimpan = If(joanelibweb.mycookies.getData("email"), HttpContext.Current.Session.Item("email"))
        namatersimpan = If(joanelibweb.mycookies.getData("nama"), HttpContext.Current.Session.Item("nama"))

        skrip = "select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat from sesi where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
        dt = j.ambilData(skrip)
        If joanelibweb.myRequest.getcurURL <> "https://ibw.news/keepalive.aspx" Then
            If dt.Rows.Count > 0 Then
                If namatersimpan = "" Then
                    skrip = "update sesi set sesiid='" & Me.sesiID & "', email='" & emailtersimpan & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
                Else
                    skrip = "update sesi set nama='" & namatersimpan & "', sesiid='" & Me.sesiID & "', email='" & emailtersimpan & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"

                End If
            Else
                skrip = "insert into sesi (sesiid,email,nama,lastpage)values('" & Me.sesiID & "','" & CurEmail & "','" & CurNama & "','" & joanelibweb.myRequest.getcurURL & "')"
            End If
            j.updateData(skrip)
        End If


        HttpContext.Current.Session.Item("email") = CurEmail

        HttpContext.Current.Session.Item("nama") = CurNama
        joanelibweb.mycookies.setData("/", "email", CurEmail, 300000)
        joanelibweb.mycookies.setData("/", "nama", CurNama, 300000)
    End Sub
    Sub updateSesiListParam(daftarparam As List(Of paramdata), email As String, Optional paksaUpdate As Boolean = True, Optional showCookies As Boolean = True)
        Dim j As New ambilData
        Dim skrip As String
        Dim dt As New DataTable
        Dim namaparam, nilaiparam, jenis As String

        skrip = "select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat from sesi where email='" & email & "' or sesiid='" & Me.sesiID & "'"
        dt = j.ambilData(skrip)
        Dim hasil, skripparam As String
        hasil = ""
        For Each d As paramdata In daftarparam
            namaparam = d.paramname
            nilaiparam = d.paramvalue
            jenis = d.int1string2

            If jenis = 1 Then
                skripparam = namaparam & "='" & nilaiparam
            Else
                skripparam = namaparam & "='" & nilaiparam & "'"
            End If
            hasil &= "," & skripparam
            If showCookies Then
                HttpContext.Current.Session.Item(namaparam) = nilaiparam
                joanelibweb.mycookies.setData("/", namaparam, nilaiparam, 300000)
            End If
        Next

        '  skrip = "update sesi set sesiid='" & Me.sesiID & "', " & namaParam & "='" & nilaiParam & "',lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & email & "' or sesiid='" & Me.sesiID & "'"
        skrip = "update sesi set sesiid='" & Me.sesiID & "' " & hasil & ",lastpage='" & joanelibweb.myRequest.getcurURL & "' where email='" & email & "' or sesiid='" & Me.sesiID & "'"
        If joanelibweb.myRequest.getcurURL <> "https://ibw.news/keepalive.aspx" Then
            j.updateData(skrip)
        End If



        HttpContext.Current.Session.Item("email") = CurEmail
        HttpContext.Current.Session.Item("nama") = CurNama


        joanelibweb.mycookies.setData("/", "email", CurEmail, 300000)
        joanelibweb.mycookies.setData("/", "nama", CurNama, 300000)

    End Sub
    Function getSesiParamString(namaParam As String) As String
        Dim j As New ambilData
        Dim skrip As String
        Dim hasil As String
        ' skrip = "select " & namaParam & " from sesi where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
        ' hasil = j.ambilString(skrip)
        skrip = "select " & j.namakolomlengkap("sesi") & " from sesi where  email='" & CurEmail & "' or sesiid='" & sesiID & "'"
        dt = j.ambilData(skrip)
        Try
            hasil = dt.Rows(0).Item(namaParam).ToString
        Catch ex As Exception
            hasil = namaParam & "-" & ex.Message
        End Try

        HttpContext.Current.Session.Item("email") = CurEmail
        HttpContext.Current.Session.Item("nama") = CurNama
        joanelibweb.mycookies.setData("/", "email", CurEmail, 300000)
        joanelibweb.mycookies.setData("/", "nama", CurNama, 300000)
        Return hasil
    End Function
    Function getSesiParamInteger(namaParam As String) As Integer
        Dim j As New ambilData
        Dim skrip As String
        Dim hasil As Integer
        'skrip = "select " & namaParam & " from sesi where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
        'hasil = j.ambilInteger(skrip)
        skrip = "select " & j.namakolomlengkap("sesi") & " from sesi where  email='" & CurEmail & "' or sesiid='" & sesiID & "'"
        dt = j.ambilData(skrip)
        Try
            hasil = Val(dt.Rows(0).Item(namaParam).ToString)
        Catch ex As Exception
            hasil = 0 'namaParam & "-" & ex.Message
            ' Throw New Exception(namaParam & "#" & dt.TableName)
        End Try

        HttpContext.Current.Session.Item("email") = CurEmail
        HttpContext.Current.Session.Item("nama") = CurNama
        joanelibweb.mycookies.setData("/", "email", CurEmail, 300000)
        joanelibweb.mycookies.setData("/", "nama", CurNama, 300000)
        Return hasil
    End Function

    Public Sub hapusSesi()

        ' If CurNama = "" Or CurEmail = "" Then
        j = New ambilData
        '  Dim dt As New DataTable

        skrip = "select id,sesiid,memberid,curkelasid,curkelasstep,curkelaseditid,curkelaseditstep,curarea,lastpage,curkomlokfavid,curkomlokeditid,nama,email,curkodepromo,curkelaspenyelenggara,ispenyelenggara,ispartner,picemail,picwa,lastidchat from sesi where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
        Try

            skrip = "update sesi set email='' , nama='' where email='" & CurEmail & "' or sesiid='" & Me.sesiID & "'"
            CurNama = "" 'dt.Rows(0).Item("nama")
            CurEmail = "" 'dt.Rows(0).Item("email")
            j.updateData(skrip)

        Catch ex As Exception

        End Try

        '   CurEmail = If(joanelibweb.mycookies.getData("email"), HttpContext.Current.Session.Item("email")) ' joanelibweb.mycookies.getData("email")
        '  CurNama = If(joanelibweb.mycookies.getData("nama"), HttpContext.Current.Session.Item("nama")) 'joanelibweb.mycookies.getData("nama")
    End Sub
    Public ReadOnly Property lastPage As String
        Get
            Return cekKosong(joanelibweb.myRequest.getpageFolder, "/")
        End Get
    End Property
    Private Function cekKosong(par As Object, Optional def As String = "") As String
        If par Is Nothing Then
            Return def
        Else
            Return par
        End If

    End Function
    Structure paramdata
        Dim paramname As String
        Dim paramvalue As String
        Dim int1string2 As Integer
    End Structure
End Class
