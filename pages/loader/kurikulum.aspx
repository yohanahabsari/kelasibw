<%@ Page Language="VB" AutoEventWireup="false" CodeFile="kurikulum.aspx.vb" Inherits="Pages_loader_kurikulum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                   <asp:ListView ID="data1" ItemPlaceholderID="isian" runat="server" >
                       <LayoutTemplate>
                            <table class="table">  <thead>
    <tr>
      <th scope="col">No</th>
      <th scope="col">kategori</th>
      <th scope="col">judul</th>
       <th scope="col">tag</th>
      <th scope="col"></th>
    </tr>
                                
  </thead>
                                <tbody>
                                    <tr id="isian" runat="server"></tr>

                                </tbody>
</table>
                       </LayoutTemplate>
       <EmptyDataTemplate>  </EmptyDataTemplate>
       <ItemTemplate>

               <tr>
                   <th scope="row"><%#  DataBinder.Eval(Container.DataItem, "urutan") %></t>
                   <td scope="row"><%#  DataBinder.Eval(Container.DataItem, "kategori") %></td>
                   <td scope="row"><a href='indonesianbabywearers.com/admin/detailmateri.aspx?id=<%#  DataBinder.Eval(Container.DataItem, "materiid") %>'><%#  DataBinder.Eval(Container.DataItem, "judul") %></a></td>
                   <td scope="row"></td>
                   <td scope="row"><a class="btn btn-primary" href="#" role="button">pilih</a></td>
               </tr>

                     		 
	   

       </ItemTemplate>
   </asp:ListView>
    </form>
</body>
</html>
