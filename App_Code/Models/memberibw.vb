Imports System.Data
Imports Microsoft.VisualBasic

Public Class memberibw

    Public Property memberId As Integer
    Public Property Telp As String
    Public Property Email As String
    Public Property Ig As String
    Public Property Fb As String
    Public Property Fp As String
    Public Property Googleid As String
    Public Property Username As String
    Public Property komlokid As Integer
    Public Property komlokfavid As Integer
    Public Property Nama As String
    Public Property Alamat As String
    Public Property Provinsi As String
    Public Property Kota As String
    Public Property ismember As Boolean
    Public Property issuper As Boolean
    Public Property isacademy As Boolean
    Public Property isnarasumber As Boolean
    Public Property ispartner As Boolean
    Public Property iskomunitas As Boolean
    Public Property iskomlok As Boolean
    Public Property saldopoin As Integer
    Public Property saldopoinbonus As Integer
    Public Property saldopoinutama As Integer
    Public Property level As Integer
    Public Property tglakhir As DateTime
    Public Property noakses As Boolean

    Public Property curpass As String
    Dim o As New ambilData

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(username As String)
        MyBase.New()
        setinit(username)

    End Sub
    Sub setInit(Username As String)
        Dim dt As DataTable
        Dim i As Integer
        dt = o.ambilData("select * from member where email='" & username & "' or telp='" & username & "' or fbid='" & username & "' or googleid='" & username & "' ")

        If dt.Rows.Count < 1 Then

            Me.ismember = False

            Me.noakses = True

            ' MyBase.New()
        Else

            Me.ismember = True
            i = dt.Rows.Count

            With dt.Rows(0)
                Me.memberId = If(.Item("id").ToString, "")
                Me.curpass = If(.Item("password").ToString, "")
                Me.Username = If(.Item("telp").ToString, "")
            End With
            dt = o.ambilData("select * from member where id=" & Me.memberId & " ")
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    Me.Nama = .Item("nama").ToString
                    Me.Alamat = .Item("alamat").ToString
                    Me.Provinsi = .Item("provinsi").ToString
                    Me.Kota = If(.Item("kota").ToString, "")
                    Me.issuper = If(.Item("issuper"), False)
                    Me.isacademy = If(.Item("isacademy"), False)
                    Me.ispartner = If(.Item("ispartner"), False)
                    Me.isnarasumber = If(.Item("isnarasumber"), False)
                    Me.iskomlok = If(.Item("iskomlok"), False)
                    Me.iskomunitas = If(.Item("iskomunitas").ToString, "")
                    Me.Googleid = If(.Item("googleid").ToString, "")
                    Me.komlokid = If(.Item("komlokid").ToString, "")
                    Try
                        Me.komlokfavid = If(.Item("komlokfavid"), "")
                    Catch ex As Exception
                        Me.komlokfavid = Me.komlokid
                    End Try

                    Me.Telp = If(.Item("telp").ToString, "")
                    Me.Ig = If(.Item("ig").ToString, "")
                    Me.Fb = If(.Item("fb").ToString, "")
                    Me.Fp = If(.Item("fp").ToString, "")
                    Me.level = If(.Item("levelmember"), 0)
                    Dim tgl As String
                    tgl = If(.Item("tglakhirmember").ToString, "") '
                    Try
                        Me.tglakhir = DateAndTime.DateSerial(Left(tgl, 4), Mid(tgl, 6, 2), Mid(tgl, 9, 2))

                    Catch ex As Exception
                        Me.tglakhir = DateAdd(DateInterval.Day, 1, Now())
                    End Try
                    If i < 1 Then
                        Me.saldopoin = 0
                        Me.saldopoinbonus = 0
                        Me.saldopoinutama = 0
                    Else

                        Me.saldopoin = Val(.Item("poin"))
                        Me.saldopoinbonus = Val(.Item("poinbonus"))
                        Me.saldopoinutama = Val(.Item("poinutama"))

                    End If
                End With
            Else

            End If




        End If
        ' dt = o.ambilData("select * from saldopoin where memberid= " & Me.memberId & " order by id desc")

        'i = dt.Rows.Count
    End Sub
    Public Sub New(username As String, pwd As String)
        MyBase.New()
        Dim dt As DataTable
        Dim i As Integer
        dt = o.ambilData("select * from aksesmember where email='" & username & "' or telp='" & username & "' or fbid='" & username & "' or googleid='" & username & "' ")

        If dt.Rows.Count < 1 Then

            Me.ismember = False
            Me.noakses = True


            ' MyBase.New()
        Else

            Me.ismember = True
            setInit(username)

            If Me.cekPass(pwd) Then
                Me.noakses = False
            Else
                Me.noakses = True
            End If


        End If


    End Sub
    Public WriteOnly Property Pass As String

        Set(ByVal value As String)
            noakses = cekPass(value)

        End Set
    End Property

    Public Function cekPass(pass As String) As Boolean
        If pass = Me.curpass Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function cekAksesKelas(kelas As Integer, pwd As String) As Boolean
        Dim skrip As String
        Dim dt As New DataTable
        Dim o As New ambilData
        If Me.issuper Then
            Return True
        ElseIf Me.ismember Then
            If cekPass(pwd) Then
                skrip = "select * from kelassesi where kelasid=" & kelas & " and memberid=" & Me.memberId & "  and timediff(berakhir, now()) >0"
                dt = o.ambilData(skrip)
                If dt.Rows.Count > 0 Then
                    Return True
                Else

                    Return False
                End If
            Else
                skrip = "select * from akseskelas where username = '" & Me.Telp & "' and kelasid= " & kelas & " and kodeakses='" & pwd & "'   and timediff(berakhir, now()) >0"

                'skrip = "select * from kelassesi where kelasid=" & kelas & " and username=" & Me.Username & "  and timediff(berakhir, now()) >0"
                dt = o.ambilData(skrip)
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Else
            skrip = "select * from kelassesi where kelasid=" & kelas & " and username=" & Me.Username & "  and timediff(berakhir, now()) >0"
            dt = o.ambilData(skrip)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End If
    End Function
End Class
