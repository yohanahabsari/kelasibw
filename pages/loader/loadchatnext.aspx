<%@ Page Language="VB" AutoEventWireup="false" CodeFile="loadchatnext.aspx.vb" Inherits="pages_loader_loadchatnext" %>

                  <asp:repeater id="rptchat" runat="server">
                      <ItemTemplate>
                             <div class='card <%# classChat(Eval("id")) %> rounded w-75 float-right z-depth-0 mb-1'>
                  <div class="card-body p-2">
                      <p><span><%#Eval("nama") %></span></p> 
                    <p class="card-text text-white"><%#Eval("komentar") %></p>
                  </div>
                </div>

                      </ItemTemplate>
       <AlternatingItemTemplate>
  <div class="card bg-light rounded w-75 z-depth-0 mb-1 message-text">
                  <div class="card-body p-2">
                         <p><span><%#Eval("nama") %></span></p> 
                    <p class="card-text text-white"><%#Eval("komentar") %></p>
                  </div>
                </div>

       </AlternatingItemTemplate>
                  </asp:repeater>
   <input type="text" class="d-none"  id="loadhasil" value='<%=lastid  %>' />
