Imports Microsoft.VisualBasic

Public Class Produk
    Public Sub New(id As Integer, nama As String, uraian As String, harga As Double, kategori As String, owner As String)
        Me.Id = id
        Me.Nama = nama
        Me.Uraian = uraian
        Me.Harga = harga
        Me.Kategori = kategori
        Me.Owner = owner
    End Sub

    Public Property Id() As Integer
    Public Property Nama() As String
    Public Property Uraian() As String
    Public Property Harga() As Double
    Public Property Kategori() As String
    Public Property Owner() As String
End Class
