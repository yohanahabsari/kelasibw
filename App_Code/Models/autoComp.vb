Imports Microsoft.VisualBasic

Public Class autoComp

    Dim _id, _value, _label, _desc, _icon As String


    Public Property value() As String
        Get
            Return _value
        End Get
        Set(ByVal val As String)
            _value = val
        End Set
    End Property
    Public Property id() As String
        Get
            If _id = "" Then _id = _value
            Return _id
        End Get
        Set(ByVal val As String)
            _id = val
        End Set
    End Property
    Public Property label() As String
        Get
            If _label = "" Then _label = _value
            Return _label
        End Get
        Set(ByVal value As String)
            _label = value
        End Set
    End Property
    Public Property desc() As String
        Get

            Return _desc
        End Get
        Set(ByVal value As String)
            _desc = value
        End Set
    End Property
    Public Property icon() As String
        Get

            Return _icon
        End Get
        Set(ByVal value As String)
            _icon = value
        End Set
    End Property


    Public Sub New()

    End Sub
    Public Sub New(ByVal val As String, ByVal label As String)
        _value = val
        _label = label

    End Sub
    Public Sub New(ByVal val As String, ByVal label As String, ByVal desc As String, ByVal icon As String)
        _value = val
        _label = label
        _desc = desc
        _icon = icon
    End Sub
End Class
