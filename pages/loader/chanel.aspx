<%@ Page Language="VB" AutoEventWireup="false" CodeFile="chanel.aspx.vb" Inherits="Pages_loader_chanel" %>

                <asp:Repeater ID="dataKu" runat="server" >
          <ItemTemplate>
              
                    	<div class="card">
            <div class="card-block ">
               <div class="card-title">
    <h3 class="card-title"><%#Eval("judul") %></h3>
    <!-- /.card-tools -->
  </div>
  <!-- /.card-header -->
  <div class="card-text">
   <%#Eval("uraian") %>

                    
  </div>
</div>
      </div>
            
          </ItemTemplate>
      </asp:Repeater>

            


