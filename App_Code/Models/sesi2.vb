Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Web.HttpContext
Public Class sesi2
    Dim orang As memberibw
    Dim _nama As String
    Dim skrip As String
    Dim dt As New DataTable
    Dim j As New ambilData
    Dim _pass As String
    Public Sub New()
        MyBase.New
        '   Me.Usr = joanelibweb.mycookies.getData("username")
        bikinbaru(joanelibweb.mycookies.getData("username"))
        If IsNothing(joanelibweb.mycookies.getData("username")) Then
            HapusSesi()
            FormsAuthentication.SignOut()
        End If

    End Sub
    Sub bikinbaru(user As String)
        joanelibweb.mycookies.setData("/", "username", user, 300000)
        Me.orang = New memberibw(user)
        '  Me.CurMember = Me.orang
        skrip = "select count(*) from sesi where memberid=" & Me.orang.memberId
        If j.ambilInteger(skrip) > 0 Then
            skrip = "update sesi set sesiid='" & Me.sesiID & "' where memberid=" & Me.orang.memberId
        Else
            skrip = "insert into sesi (sesiid,memberid)values('" & Me.sesiID & "'," & Me.CurMember.memberId & ")"
        End If
        j.updateData(skrip)
        j = New ambilData
        Me.Usr = user
        Me.CurStep = 1
        Me.CurStepEdit = 1
        setinit()
    End Sub
    Public Sub New(user As String)
        MyBase.New

        bikinbaru(user)
    End Sub
    Public Sub setinit()
        j = New ambilData

        skrip = "select * from sesi where memberid=" & Me.CurMember.memberId
        dt = j.ambilData(skrip)
    End Sub
    Public Sub HapusSesi()
        Current.Session.Clear()
    End Sub
    Public ReadOnly Property sesiID As String
        Get
            Return HttpContext.Current.Session.SessionID
        End Get
    End Property
    Public Property CurArea As String
        Get
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curarea")
            Else
                Return "member"
            End If
            'cekSesi(HttpContext.Current.Session.Item("area"))
        End Get
        Set(ByVal value As String)
            skrip = "update sesi set curarea='" & value & "' where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property

    Public ReadOnly Property CurMemberID As Integer
        Get
            If Me.CurMember Is Nothing Then
                Return 0

            Else
                Return Me.CurMember.memberId

            End If
        End Get

    End Property
    Public Property CurNama As String
        Get
            If Me.CurMember Is Nothing Then
                Return "" 'HttpContext.Current.Session.Item("nama")

            Else
                Return Me.CurMember.Nama

            End If
        End Get
        Set(ByVal value As String)
            Me.CurMember.Nama = value
        End Set
    End Property
    Public Property CurMember As memberibw
        Get
            Return Me.orang ' TryCast(HttpContext.Current.Session.Item("datamember"), memberibw)
        End Get
        Set(ByVal value As memberibw)
            Me.orang = value
        End Set
    End Property
    Public Property CurKomlokID As String
        Get
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curkomlokfavid").ToString
            Else
                Return Me.CurMember.ToString
            End If

            'cekSesi(HttpContext.Current.Session.Item("komlokid"))
        End Get
        Set(ByVal value As String)
            skrip = "update sesi set  curkomlokfavid=" & value & " where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property

    Public Property CurKomlokidEdit As String
        Get
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curkomlokfavid")
            Else
                Return Me.CurMember.komlokid
            End If
        End Get
        Set(ByVal value As String)
            skrip = "update sesi set  curkomlokeditid=" & value & " where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property
    Public Property Usr As String
        Get
            Return Me.CurMember.Username 'cekSesi(HttpContext.Current.Session.Item("username"))
        End Get
        Set(ByVal value As String)
            If value <> Me.Usr Then
                orang = New memberibw(value)
                Me.CurMember = orang
                setinit()
            End If

        End Set
    End Property

    Public WriteOnly Property Pass As String
        Set(ByVal value As String)
            ' HttpContext.Current.Session.Item("pass") = value
            Me.CurMember.Pass = value
            Me._pass = value
        End Set
    End Property
    Public ReadOnly Property Path As String
        Get
            Return cekSesi(joanelibweb.myRequest.getpageFolder, "/")
        End Get
    End Property
    Public Property Kelasid As String
        Get

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curkelasid")
            Else
                Return Me.CurMember.komlokid
            End If
        End Get
        Set(ByVal value As String)
            skrip = "update sesi set  curkelasid=" & value & " where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property

    Public Property Kelaseditid As String
        Get

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curkelaseditid")
            Else
                Return Me.CurMember.komlokid
            End If
        End Get
        Set(ByVal value As String)
            skrip = "update sesi set  curkelaseditid=" & value & " where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property
    Public Property CurStep As Integer
        Get

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curkelasstep")
            Else
                Return Me.CurMember.komlokid
            End If
        End Get
        Set(ByVal value As Integer)
            skrip = "update sesi set  curkelasstep=" & value & " where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property
    Public Property CurStepEdit As Integer
        Get

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("curkelaseditstep")
            Else
                Return Me.CurMember.komlokid
            End If
        End Get
        Set(ByVal value As Integer)
            skrip = "update sesi set  curkelaseditstep=" & value & " where memberid=" & Me.CurMember.memberId
            j.updateData(skrip)
            setinit()
        End Set
    End Property
    Private Function cekSesi(par As Object, Optional def As String = "") As String
        If par Is Nothing Then
            Return def
        Else
            Return par
        End If

    End Function
    Private Function cekSesiInt(par As Object, Optional def As Integer = 0) As Integer
        If par Is Nothing Then
            Return def
        Else
            If Val(par) < def Then
                Return def
            Else
                Return Val(par)
            End If

        End If

    End Function
    Public Function bolehAkses() As Boolean
        Return Me.CurMember.cekPass(Me._pass)

    End Function
End Class
