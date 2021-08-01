<%@ Page Language="VB" AutoEventWireup="false" CodeFile="loadChat.aspx.vb" Inherits="pages_loader_loadChat" %>

     <!-- Main layout -->
<main class="mt-n5" id="windowchat">
  <div class="container">  

    <!-- Grid row -->
    <div class="row d-flex flex-row-reverse">

      <!-- Grid column -->
      <div class="col-md-6 mb-4 d-flex flex-row-reverse">

        <!-- <div class="fixed-action-btn" style="bottom: 45px; right: 24px;">
          <a class="btn-floating btn-lg red open-button" onclick="openForm()">
            <i class="fas fa-comments"></i>
          </a>
        </div> -->

        <div class="card chat-room small-chat wide" id="myForm">
          <div class="card-header white d-flex justify-content-between p-2" id="toggle" style="cursor: pointer;">
            <div class="heading d-flex justify-content-start">
              <div class="profile-photo">
                <img src="https://mdbootstrap.com/img/Photos/Avatars/avatar-6.jpg" alt="avatar" class="avatar rounded-circle mr-2 ml-0">
                <span class="state"></span>
              </div>
              <div class="data">
                <p class="name mb-0"><strong><%=Me.kelaspenyelenggara  %></strong></p>
                <p class="activity text-muted mb-0">Active now</p>
              </div>
            </div>
            <div class="icons grey-text">
            
              <a class="feature"><i class="fas fa-phone mr-2"></i></a>
             
              <a type="button" id="closeButton"><i class="fas fa-times mr-2"></i></a>
            </div>
          </div>
          <div class="my-custom-scrollbar">
            <div class="card-body p-3">
              <div class="chat-message">
                <div class="media mb-3">
                
                  <div class="media-body">
                    <p class="my-0"><%=Me.picwa %></p>
                     <p class="mb-0 text-muted"><%=Me.picemail %></p>
                  </div>
                </div>
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
                  <div id="loadnow" class="" data-lastid='<%=lastid  %>' ></div>
             <div id="loadnext" class="" data-lastid='<%=lastid  %>' ></div>
              
              </div>
            </div>
          </div>
          <div class="card-footer text-muted white pt-1 pb-2 px-3">
            <input type="text" id="inputchat" class="form-control" placeholder="Type a message...">
            <div>
                <a><i class="fas fa-thumbs-up"></i></a>
              <a id="sendchat" class="float-right">send </a>
             
              
            </div>
          </div>

        </div>

      </div>
      <!-- Grid column -->

    </div>
    <!-- Grid row -->

  </div>
</main>
<!-- Main layout -->

 <input type="text" class="d-none" value='<%=lastid  %>' id="loadlastid" />

     <script type="text/javascript">
            $().ready(function () {
                setInterval(function () {
                    $('#loadnext').load('https://ibw.news/pages/loader/loadchatnext.aspx?lastid='+  $('#loadlastid').val(), function () {
                        $('#loadlastid').val( $('#loadhasil').val());
                        $('#loadnow').append($('#loadnext>div'))
                        scrollToEnd('.my-custom-scrollbar');
                   });
                }, 4000);

            // Perfect scrollbar
const myCustomScrollbar = document.querySelector('.my-custom-scrollbar');
const ps = new PerfectScrollbar(myCustomScrollbar);
const scrollbarY = myCustomScrollbar.querySelector('.ps.ps--active-y>.ps__scrollbar-y-rail');
myCustomScrollbar.onscroll = function () {
  scrollbarY.style.cssText = `top: ${this.scrollTop}px!important; height: 288px; right: ${-this.scrollLeft}px`;
}

const $myForm = $('#myForm');
 $myForm.hide();
$('#chat').on('click', function () {

  if ($myForm.hasClass('slim') || !$myForm.is(':visible')) {

    $myForm.css('display', 'block');
    $myForm.removeClass('slim');
  };
})

$('#closeButton').not('#toggle').on('click', function () {

  $myForm.hide();
})

$("#toggle").on('click', function () {

  $myForm.toggleClass('slim');
});
        })

         $('#sendchat').on('click', function () {
             var uri =  getInputChat() 
             var res = 'https://ibw.news/pages/loader/loadchatnext.aspx?lastid='+   $('#loadlastid').val() + '&komentar=' + encodeURIComponent(uri); 
              $('#loadnext').load(res, function () {
                 
                  $('#loadlastid').val($('#loadhasil').val());
                  $('#loadnow').append($('#loadnext>div'));
                     scrollToEnd('.my-custom-scrollbar');
                  $('#inputchat').val("");
             });
                   function getInputChat() {
             return $('#inputchat').val();
         }

         });
scrollToEnd = function(div){
setTimeout(() => {
const container = $(div);
container.scrollTop = container.scrollHeight;
}, 0);
}
    </script>
