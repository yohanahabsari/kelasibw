Imports Microsoft.VisualBasic
Imports System.Web.HttpContext
Public Class sesi
    Dim orang As memberibw
    Dim _nama As String
    Public Sub New()
        MyBase.New
        Me.orang = New memberibw
    End Sub
    Public Sub New(user As String)
        MyBase.New
        Me.Usr = user
        Me.CurStep = 1
        Me.CurStepEdit = 1
        joanelibweb.mycookies.setData("/", "username", user, 200000)
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
            Return cekSesi(HttpContext.Current.Session.Item("area"))
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session.Item("area") = value
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
                Return HttpContext.Current.Session.Item("nama")

            Else
                Return Me.CurMember.Nama

            End If
        End Get
        Set(ByVal value As String)

            If Me.CurMember Is Nothing Then
                HttpContext.Current.Session.Item("nama") = value

            Else
                HttpContext.Current.Session.Item("nama") = value

            End If

        End Set
    End Property
    Public Property CurMember As memberibw
        Get
            Return TryCast(HttpContext.Current.Session.Item("datamember"), memberibw)
        End Get
        Set(ByVal value As memberibw)
            HttpContext.Current.Session.Item("datamember") = value
        End Set
    End Property
    Public Property CurKomlokID As String
        Get
            Return cekSesi(HttpContext.Current.Session.Item("komlokid"))
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session.Item("komlokid") = value
        End Set
    End Property
    Public Property CurKomlokidEdit As String
        Get
            Return cekSesi(HttpContext.Current.Session.Item("komlokidedit"))
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session.Item("komlokidedit") = value
        End Set
    End Property
    Public Property Usr As String
        Get
            Return cekSesi(HttpContext.Current.Session.Item("username"))
        End Get
        Set(ByVal value As String)
            If value <> Me.Usr Then
                orang = New memberibw(value)
                Me.CurMember = orang
            End If
            HttpContext.Current.Session.Item("username") = value
        End Set
    End Property

    Public Property Pass As String
        Get
            Return cekSesi(HttpContext.Current.Session.Item("pass"))
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session.Item("pass") = value
            Me.orang.Pass = value
        End Set
    End Property
    Public ReadOnly Property Path As String
        Get
            Return cekSesi(joanelibweb.myRequest.getpageFolder, "/")
        End Get
    End Property
    Public Property Kelasid As String
        Get
            Return cekSesi(HttpContext.Current.Session.Item("kelasid"))
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session.Item("kelasid") = value
        End Set
    End Property

    Public Property Kelaseditid As String
        Get
            Return cekSesi(HttpContext.Current.Session.Item("kelaseditid"))
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session.Item("kelaseditid") = value
        End Set
    End Property
    Public Property CurStep As Integer
        Get
            Return cekSesiInt(HttpContext.Current.Session.Item("step"), 1)
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session.Item("step") = value
        End Set
    End Property
    Public Property CurStepEdit As Integer
        Get
            Return cekSesiInt(HttpContext.Current.Session.Item("stepedit"), 1)
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session.Item("stepedit") = value
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
End Class
