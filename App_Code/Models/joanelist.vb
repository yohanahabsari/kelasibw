Imports Microsoft.VisualBasic

Public Class joanelist

    Dim  _value As String


    Public Property value() As String
        Get
            Return _value
        End Get
        Set(ByVal val As String)
            _value = val
        End Set
    End Property

    Public Sub New()

    End Sub
    Public Sub New(ByVal val As String)
        _value = val


    End Sub
    Public Sub New(ByVal val As String)
        _value = val

    End Sub
End Class
