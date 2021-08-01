<%@ Page Language="VB" AutoEventWireup="false" CodeFile="instantarticles.aspx.vb" Inherits="Pages_instantarticles" %>

   

        <!doctype html>
        <html lang="id" prefix="op: https://media.facebook.com/op#">
          <head>
            <meta charset="utf-8">
            <link rel="canonical" href='<%=linkcur %>'>
            <meta property="op:markup_version" content="v1.0">
          </head>
          <body>
  
              <form id="form1" runat="server">
  
            <article>
                <header>
                    <asp:Repeater ID="datachanel" runat="server" >
                        <ItemTemplate >
                                               <!-- The cover image shown inside your article -->
                    <figure>
                       
                        <img src='<%#Eval("imgcover") %>' />
                        <figcaption><%#Eval("imgcaption") %></figcaption>
                    </figure>

                    <!-- The title and subtitle shown in your article -->
                    <h1><%#Eval("title") %> </h1>
                    <h2><%#Eval("subtitle") %></h2>

                    <!-- A kicker for your article -->
                    <h3 class="op-kicker"><%#Eval("kicker") %>
                    </h3>

                    <!-- The author of your article -->
                    <address>
                        <a rel="facebook" href="https://facebook.com/indonesianbabywearers">Indonesian Babywearers</a>
                        IBW adalah komunitas berbasis keluarga
                    </address>

                    <!-- The published and last modified time stamps -->
                    <time class="op-published" datetime='<%#Eval("pubdaterss").ToString  %>'><%#Eval("pubdate") %></time>
                    <time class="op-modified" datetime='<%#Eval("modifdaterss").ToString  %>'><%#Eval("modifdate") %></time>
                            
                        </ItemTemplate>
                    </asp:Repeater>
 
                </header>
                 <asp:Repeater runat="server" ID="artikelku" >
                  <ItemTemplate>
                       <%#cekkonten(Eval("jenis"), Eval("uraiancaption"), Eval("uraian"))%>
                  </ItemTemplate>
              </asp:Repeater>

                <!-- An advertisement
                <figure class="op-ad">
                    <iframe src="https://www.adserver.com/ss;adtype=banner320x50" height="60" width="320"></iframe>
                </figure> 
                    -->
              <!— Article body goes here -->

              <footer>
                <!— Article footer goes here -->
              </footer>
            </article>
              </form>
          </body>
        </html>

     