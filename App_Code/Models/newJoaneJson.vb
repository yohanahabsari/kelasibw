Imports System.Data
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json.Linq

Public Class newJoaneJson
    Inherits joanelibweb.myJson
    Public Function jsonfromTable(dt As DataTable) As String
            Dim writer As JTokenWriter =  New JTokenWriter() 

     if dt.rows.count >0 Then 
        Dim i As Integer
        i = 0

writer.WriteStartObject()
writer.WritePropertyName("status")
writer.WriteValue("200")
'writer.WritePropertyName("data")
     '' writer.WriteStartArray()
        For Each row As DataRow In dt.Rows
           writer.WritePropertyName("row" & i)
           writer.WriteStartObject()
   
            For Each col As DataColumn In dt.Columns
            if row(col.ColumnName).ToString <>"" Then 
            writer.WritePropertyName(col.ColumnName)
            writer.WriteValue(row(col.ColumnName).ToString)
            end if

      
            Next
           
            i += 1
writer.WriteEndObject()
        Next
     ''    writer.WriteEndArray()
     

writer.WriteEndObject()

 
Dim o As JObject = CType(writer.Token, JObject)

   Dim json As String = o.ToString()
   return json
   Else
writer.WriteStartObject()
writer.WritePropertyName("status")
writer.WriteValue("404")
writer.WriteEndObject()
Dim o As JObject = CType(writer.Token, JObject)

   Dim json As String = o.ToString()
   return json
       return ""
end if
       






      

    End Function
End Class