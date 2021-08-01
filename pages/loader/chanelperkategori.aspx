<%@ Page Language="VB" AutoEventWireup="false" CodeFile="chanelperkategori.aspx.vb" Inherits="Pages_loader_chanelperkategori" %>

<%@ Register Src="~/Controls/skripgrabber.ascx" TagPrefix="uc1" TagName="skripgrabber" %>
<%@ Register Src="~/Controls/chanelpilihan.ascx" TagPrefix="uc1" TagName="chanelpilihan" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="accordion<%=joanelibweb.myRequest.getData("kategori", "") %>"> 
            
            <nav aria-label="breadcrumb">
  <ol class="breadcrumb" >
    <li class="breadcrumb-item active"><a id='headingkat<%=joanelibweb.myRequest.getData("kategori", "") %>' style="color:black" data-toggle="collapse" href='#divkat<%=joanelibweb.myRequest.getData("kategori", "") %>' onclick="return false;"  aria-controls='divkat<%=joanelibweb.myRequest.getData("kategori", "") %>'>
        Chanel  Favorit saat ini
        </a></li>
     <asp:Repeater ID="loadInduk" runat="server">
            <ItemTemplate>
           <li class="breadcrumb-item">   <a style="color:black" data-toggle="collapse" href='#div<%#Eval("chanelid") %>' onclick="return false;"  aria-controls='div<%#Eval("chanelid") %>'>
           <%#Eval("topik") %>
        </a></li>     
         </ItemTemplate>
        </asp:Repeater>
  
  </ol>
</nav>
           
                 
         
          
            
            <div class="row">
  <div class="col">
       <div id='divkat<%=joanelibweb.myRequest.getData("kategori", "") %>' class="card collapse show" aria-labelledby='headingkat<%=joanelibweb.myRequest.getData("kategori", "") %>' data-parent="#accordion<%=joanelibweb.myRequest.getData("kategori", "") %>">
      <div class="card-body">

   <h4 id="statusfav" runat="server" ></h4>



      </div>
    </div>
              <asp:Repeater ID="loaddiv" runat="server">
            <ItemTemplate>
                
    <div id='div<%#Eval("chanelid") %>' class="card collapse" aria-labelledby='headingkat<%#Eval("chanelid") %>' data-parent="#accordion<%=joanelibweb.myRequest.getData("kategori", "") %>">
      <div class="card-body">
          <div class="row" >
              <div class="col-8"> <h4><%#Eval("topik") %></h4>
                  <p><%#Eval("chaneluraian") %></p>
                       </div>
              <div class="col-4"><button class="btn btn-primary pilihchanelfav" data-chanel-hasil='divkat<%#Eval("chanelid") %>' onclick="return pilihchanel(<%#Eval("chanelid") %>,<%#Eval("chanelparamid") %>,'');" data-chanel-pilihan='' 
                  data-chanel='<%#Eval("chanelparamid") %>'><i class="fa fa-heart"></i>&nbsp Pilih</button>  </div>

          </div>
          <h4><%#Eval("judul") %></h4>
  <%#Eval("uraian") %>
          <uc1:chanelpilihan runat="server" chanelid='<%#Eval("chanelparamid") %>' kategori='<%=joanelibweb.myRequest.getData("kategori", "") %>' chanelparamid='<%# Eval("chanelparamid") %>' kat='<%=joanelibweb.myRequest.getData("kategori", "") %>' ID="chanelpilihan" />
      </div>
    </div>

            
            </ItemTemplate>
        </asp:Repeater>
      </div>
                </div>
            <div class="row">
                <div class="col">

                     <asp:Repeater ID="dataKu" runat="server" >
          <ItemTemplate>
              
                    	<div class="card">
            <div class="card-body ">
               <div class="card-title">
    <h3 ><%#Eval("judul") %></h3>
    <!-- /.card-tools -->
  </div>
  <!-- /.card-header -->
  <div class="card-text">
  <a href='https://indonesianbabywearers.com/artikel?id=<%#Eval("id") %> ' ><%#Eval("uraian") %> </a> 

                    
  </div>
</div>
      </div>
            
          </ItemTemplate>
      </asp:Repeater>
                </div>
                

            </div>
        </div>
      
     
    </form>
</body>
</html>
