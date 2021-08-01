
function checkLoginState() {
                 FB.getLoginStatus(function(response) {
                   statusChangeCallback(response);
                 });
               }
function statusChangeCallback(response) {
                 
 if (response.status === 'connected') {
                         // The user is logged in and has authenticated your
                         // app, and response.authResponse supplies
                         // the user's ID, a valid access token, a signed
                         // request, and the time the access token 
                         // and signed request each expire.
                         var uid = response.authResponse.userID;
                         var accessToken = response.authResponse.accessToken;
                         //todo: insert ke cookie dia udah login 
                         FB.api('/me', { locale: 'id_ID', fields: 'name, email' },function(response) {
                           
                           console.log('Good to see you, ' + response.name + '.');
                           document.getElementById('spanNama').innerText=response.name;
                           let basicInstance = mdb.Toast.getInstance(document.getElementById('basic-success-example'));
                           basicInstance.show();
                           $('.showNama').text(response.name )

                           Cookie.set('fbid', uid, {expires: 30});
                           Cookie.set('nama', response.name, {expires: 30});
                           Cookie.set('email', response.email, {expires: 30});
                           Cookie.set('isconfirmed', true, { expires: 30 });
                          // loadAwal();
                           location.reload();
                         });
 } else if (response.status === 'not_authorized') {
                     // The user hasn't authorized your application.  They
                     // must click the Login button, or you must call FB.login
                     // in response to a user gesture, to launch a login dialog.
                           FB.login(function(response) {
                           if (response.authResponse) {
                           console.log('Welcome!  Fetching your information.... ');
                           var uid = response.authResponse.userID;
                           FB.api('/me',function(response) {
                             console.log(response);
                             console.log('Good to see you, ' + response.name + '.');
                             
                             //todo: insert ke cookie dia udah login 
                             Cookie.set('fbid', response.uid, {expires: 30})
                             Cookie.set('nama', response.name, {expires: 30})
                           });
                           } else {
                             Cookie.remove('fbid');
                             //$('#btnOut').click();
                           
                           console.log('User cancelled login or did not fully authorize.');
                           }
                       });
} else {
 $('#btnLogin').show();
 $('#spanNama').hide();
 // The user isn't logged in to Facebook. You can launch a
 // login dialog with a user gesture, but the user may have
 // to log in to Facebook before authorizing your application.
 //todo : alert dia belum login facebook
}

               }

   function loadAwal() {
     Cookie.showModalNewUser('modalCookie1');
     Cookie.showModalScoring('modalCookie1', 30);
     $.getJSON('konfirmlogin.ashx', function (data) {
       if (data.status=='200'  ){
         var nama;
         nama= data.row0.nama;
         $('.showNama').text( nama);
         document.getElementById('spanNama').innerText="Selamat Datang " + nama;
         $('#btnLogin').hide();
         Cookie.set('fbid', data.row0.fbid, { expires: 30 });
         Cookie.set('nama', data.row0.nama, { expires: 30 });
         Cookie.set('email', data.row0.email, { expires: 30 });
         Cookie.set('wa', data.row0.wa, { expires: 30 });
         Cookie.set('id', data.row0.id, { expires: 30 });
         Cookie.set('islogin', true, { expires: 30 });
       }else {
       document.getElementById('spanNama').innerText="Anda harus login untuk melanjutkan";
     }
     let basicInstance = mdb.Toast.getInstance(document.getElementById('basic-success-example'));
         basicInstance.show();      


      })


               }

$('#btnOut').click(function(){
 Cookie.remove('fbid');
 Cookie.remove('nama');
 Cookie.remove('email');
 Cookie.remove('wa');
 Cookie.remove('id');
 Cookie.remove('islogin');
 console.log("test 5")
location.reload();
return false;
  })
   $('#btnRegister').click(function () {
       const email = $("#daftarEmail").val();
       const nama = $("#daftarNama").val();
       const wa = $("#daftarWa").val();

       if (validateDaftar) {
           Cookie.remove('fbid');
           Cookie.set('email', email, { expires: 30 });
           Cookie.set('nama', nama, { expires: 30 });
           Cookie.set('wa', wa, { expires: 30 });
           Cookie.set('isconfirmed', false, { expires: 30 });
           $.getJSON('konfirmlogin.ashx', function (data) {
               if (data.status == '200') {
                   console.log("test 3")
                   var nama;
                   nama = data.row0.nama;
                   $('.showNama').text(nama);
                   document.getElementById('spanNama').innerText = "Selamat Datang " + nama;
                   $('#btnLogin').hide();
                   Cookie.set('fbid', data.row0.fbid, { expires: 30 });
                   Cookie.set('nama', data.row0.nama, { expires: 30 });
                   Cookie.set('email', data.row0.email, { expires: 30 });
                   Cookie.set('wa', data.row0.wa, { expires: 30 });
                   Cookie.set('id', data.row0.id, { expires: 30 });
                   Cookie.set('islogin', false, { expires: 30 });
                   Cookie.set('isconfirmed', false, { expires: 30 });
                   window.location.href = "http://kelasibw.com/konfirmasi.html";
               } else {
                   console.log("test 4")
                   document.getElementById('spanNama').innerText = "Pendaftaran Tidak Berhasil";
                   let basicInstance = mdb.Toast.getInstance(document.getElementById('basic-success-example'));
                   basicInstance.show();
                   const triggerEl = document.querySelector('#ex1 a[href="#pills-register"]');
                   mdb.Tab.getInstance(triggerEl).show(); // Select tab by name
               }

           })
       }
       return false;
   });
  $('#btnLoginEmailWa').click(function(){
    console.log("test 1")
   Cookie.remove('fbid');
   Cookie.remove('email');
   Cookie.remove('nama');
   Cookie.remove('wa');
   Cookie.remove('islogin');
   Cookie.remove('isconfirmed');
   const email = $("#loginUser").val();
   
if (validateEmail(email)){
Cookie.set('email', email, { expires: 30 });
Cookie.set('isconfirmed', false, { expires: 30 });
}else if (validateWa(email)) {
Cookie.set('wa', email, { expires: 30 });
Cookie.set('isconfirmed', false, { expires: 30 });
}
console.log("test 2")
$.getJSON('konfirmlogin.ashx', function (data) {
       if (data.status=='200'  ){
         
         var nama;
         nama= data.row0.nama;
         $('.showNama').text( nama);
         document.getElementById('spanNama').innerText="Selamat Datang " + nama;
         $('#btnLogin').hide();
         Cookie.set('fbid', data.row0.fbid, { expires: 30 });
         Cookie.set('nama', data.row0.nama, { expires: 30 });
         Cookie.set('email', data.row0.email, { expires: 30 });
         Cookie.set('wa', data.row0.wa, { expires: 30 });
         Cookie.set('id', data.row0.id, { expires: 30 });
        
         Cookie.set('isconfirmed', data.row0.isconfirmed, { expires: 30 });
         if (data.row0.isconfirmed =='true') {
            Cookie.set('islogin', true, { expires: 30 });
            location.reload();
          } else{
            Cookie.set('islogin', false, { expires: 30 });
            window.location.href = "http://kelasibw.com/konfirmasi.html";
          }

       }else {
           document.getElementById('spanNama').innerText = "Anda belum terdaftar";
           let basicInstance = mdb.Toast.getInstance(document.getElementById('basic-success-example'));
           basicInstance.show(); 
           const triggerEl = document.querySelector('#ex1 a[href="#pills-register"]');
           mdb.Tab.getInstance(triggerEl).show(); // Select tab by name
     }

  }) ; return false;
 })

function validateEmail(email) {
const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
return re.test(email);
}
  
function validate() {
const $result = $("#result");
const email = $("#loginUser").val();
$result.text("");
var hasilemail;
var hasilwa;
if (validateEmail(email)) {
 hasilemail=true;
} 
if (validateWa(email)) {
 hasilwa=true;
} 
if (hasilwa||hasilemail) {
 $result.text(email + " is valid ^_^)");
 $result.css("color", "white");
}
else {
 $result.text(email + " is not valid :( ");
 $result.css("color", "yellow");
}
return false;
}

//==
   function validateDaftar() {
       const $result = $("#result2");
       const email = $("#daftarEmail").val();
       const nama = $("#daftarNama").val();
       const wa = $("#daftarWa").val();
       $result.text("");
       var hasilemail;
       var hasilwa;
       if (validateEmail(email)) {
           hasilemail = true;
       }
       if (validateWa(wa)) {
           hasilwa = true;
       }
       if (hasilwa && hasilemail && allLetter(nama)) {
           $result.text( "Input is valid ^_^ ");
           $result.css("color", "white");
           return true;
       }
       else {
           $result.text( "input is not valid :( ")
           $result.css("color", "yellow");
           return false;
       }
      // return false;
   }
//==


function validateWa(wa)
{
   var hasil;
   var numbers = /^[0-9]+$/;
   if(wa.match(numbers))
   {
  console.log('Your Registration number has accepted....');
  // document.form1.text1.focus();
   hasil= true;
   }
   else
   {
     console.log('Please input numeric characters only');
 //  document.form1.text1.focus();
   hasil= false;
   }
   const regEx = /^0/;

   if(regEx.test(wa)){hasil=false;console.log('awali dengan kode negara');};
   return hasil;
} 

   function allLetter(inputtxt) {
       var letters = /^[A-Za-z]+$/;
       if (inputtxt.match(letters)) {
           console.log('ok');
           return true;
       }
       else {
           console.log('Hanya gunakan huruf ya');
           return false;
       }
   }

   $("#loginUser").on("input", validate);
   $("#daftarEmail,#daftarNama,#daftarWa").on("input", validateDaftar);
