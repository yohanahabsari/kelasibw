Imports Microsoft.VisualBasic

Public Class StaticRoleProvider
    Inherits RoleProvider
    Dim _appName As String
    Public Overrides Property ApplicationName As String
        Get
            Return _appName
            ' Throw New NotImplementedException()
        End Get
        Set(value As String)
            _appName = value
            '  Throw New NotImplementedException()
        End Set

    End Property

    Public Overrides Sub CreateRole(roleName As String)
        'Throw New NotImplementedException()

    End Sub

    Public Overrides Sub AddUsersToRoles(usernames() As String, roleNames() As String)
        'Throw New NotImplementedException()
    End Sub

    Public Overrides Sub RemoveUsersFromRoles(usernames() As String, roleNames() As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function IsUserInRole(username As String, roleName As String) As Boolean
        Dim j As New ambilData
        Dim skrip As String
        '   Dim orang As New memberibw(username)

        skrip = "select count(1) as hasil from aksesrole where email='" & username & "' and role='" & roleName & "' "
        If j.ambilInteger(skrip) > 0 Then
            Return True
        Else
            Return False
        End If


        'Throw New NotImplementedException()
    End Function

    Public Overrides Function GetRolesForUser(username As String) As String()
        Dim j As New ambilData
        Dim skrip As String
        skrip = "select role from aksesrole where email='" & username & "' "
        Dim a As String()
        '  a = New String(-1) {}

        Return If(j.ambilStringArray(skrip), New String(-1) {})
        'Throw New NotImplementedException()
    End Function

    Public Overrides Function DeleteRole(roleName As String, throwOnPopulatedRole As Boolean) As Boolean
        Return True
        '  Throw New NotImplementedException()
    End Function

    Public Overrides Function RoleExists(roleName As String) As Boolean
        Dim j As New ambilData
        Dim skrip As String
        skrip = "select role from aksesrole where role='" & roleName & "' "

        Return If(j.ambilString(skrip) = roleName, True, False)
        ' Throw New NotImplementedException()
    End Function

    Public Overrides Function GetUsersInRole(roleName As String) As String()
        Dim j As New ambilData
        Dim skrip As String
        skrip = "select role from aksesrole group by role "
        Return j.ambilStringArray(skrip)
        ' Throw New NotImplementedException()
    End Function

    Public Overrides Function GetAllRoles() As String()
        Return New String() {"admin", "komunitas", "partner", "super", "member", "academy", "vendor"}
        'Throw New NotImplementedException()
    End Function

    Public Overrides Function FindUsersInRole(roleName As String, usernameToMatch As String) As String()
        Dim j As New ambilData
        Dim skrip As String
        skrip = "select email from aksesrole where email = '" & usernameToMatch & "' and role ='" & roleName & "'"
        Dim user As String
        user = j.ambilString(skrip)
        If (user <> "") Then
            Return New String() {usernameToMatch}
        Else
            Return New String(-1) {}
        End If


        ' Throw New NotImplementedException()
    End Function


End Class
