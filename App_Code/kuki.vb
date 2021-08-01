Imports Microsoft.VisualBasic

Public Class kuki
    Public Function getData(k As String) As String
    if joanelibweb.mycookies.getData(k)="undefined" then
        Return ""
    Else
        Return joanelibweb.mycookies.getData(k).ToString
    end if

        
    End Function
    Public Sub setData(path As String, key As String, nilai As String)
        joanelibweb.mycookies.setData(path, key, nilai, 72)
    End Sub
End Class
