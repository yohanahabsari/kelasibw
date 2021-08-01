<%@ Page Language="VB" AutoEventWireup="false" CodeFile="komentar.aspx.vb" Inherits="Pages_loader_komentar" %>

<!DOCTYPE html>


<body>
    <form id="form1" runat="server">
     <article class="post-comments">
      <div class="container">
       <div class="row">
        <div class="col listkomen card-block">


<asp:Repeater ID="komentarku" runat="server" >
    
    <ItemTemplate>
        <div class="comments-list " style="padding: 8px 10px;font-size:medium">
        <div  class="comment-body"><img src='https://graph.facebook.com/<%#Eval("fbid")%>/picture?type=square' /><span style="" class="author-name"><%# Eval("fbnama") %>(<%# Eval("tglkomen", "{0:dd/MMM/yyyy H:mm:ss zzz}") %>)</span> 
         <p class="author-post"><%# Eval("komentar") %></p>   </div>
                </div>
    </ItemTemplate>

</asp:Repeater>

		
        </div><!-- /col-lg-8 -->
       </div><!-- /row -->
      </div><!-- /container -->
     </article>

    </form>
</body>
</html>
