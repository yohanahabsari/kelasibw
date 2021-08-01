<%@ Page Title="" Language="VB" MasterPageFile="~/komunitas/MasterPage.master" AutoEventWireup="false" CodeFile="testvideo.aspx.vb" Inherits="Pages_testvideo" %>

<%@ Register Src="~/Controls/getvdocipher.ascx" TagPrefix="uc1" TagName="getvdocipher" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPost" Runat="Server">
                <script type="text/javascript">
(function(v,i,d,e,o){v[o]=v[o]||{}; v[o].add = v[o].add || function V(a){ (v[o].d=v[o].d||[]).push(a);};
if(!v[o].l) { v[o].l=1*new Date(); a=i.createElement(d), m=i.getElementsByTagName(d)[0];
a.async=1; a.src=e; m.parentNode.insertBefore(a,m);}
        })(window, document, "script", "https://player.vdocipher.com/playerAssets/1.x/vdo.js", "vdo");

       </script>
    <div class="row">
        <div class="col" >
            <div class="crsl-items">
	<div class="crsl-wrap" style="height:100px;">
		<figure class="crsl-item" >
			<img  src="https://indonesianbabywearers.com/img/banner/1.jpg">
		</figure>
		<figure class="crsl-item" style="width:100%;">
			<img  src="https://indonesianbabywearers.com/img/banner/2.jpg" />
		</figure>
		<figure class="crsl-item" style="width:100%;">
			<img  src="https://indonesianbabywearers.com/img/banner/3.jpg">
		</figure>
        	<figure class="crsl-item" style="width:100%;">
			<img  src="https://indonesianbabywearers.com/img/banner/5.jpg">
		</figure>
	</div>
</div>
        </div>
         <div class="col"></div>
    </div>
   
    <asp:Literal ID="testku" runat="server" ></asp:Literal>
    <div class="row">
<div class="col">
     <asp:Literal ID="isiVid" runat="server"></asp:Literal>

</div><div class="col">
        <uc1:getvdocipher runat="server" ID="getvdocipher" />

</div>

    </div>
     <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="../js/responsiveCarousel.min.js"></script>
    <script>
          // Activate Carousel
             $('.crsl-items').carousel({  overflow: false, visible: 1, itemMinWidth: 200, itemMargin: 10 ,speed: 1000, autoRotate: 6000 });

    </script>
</asp:Content>

