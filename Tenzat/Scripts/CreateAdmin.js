(function ($, W, D) {
           var JQUERY4U = {};
           JQUERY4U.UTIL =
           {
               setupFormValidation: function () {
                   $("#admcreateform").validate({
                       rules: {
                           AdminMail: {
                               required: true
                           },
                           Adminpass: {
                               required: true
                           }
                       },
                       messages: {
                           AdminMail: {
                               required: "Email is required"
                           },
                               Adminpass: {
                               required: "Password is required"
                           },
                       },
                       submitHandler: function (form) {
                           $(function () {
                               var manage = $("#chbox").is(':checked') ? "True" : "False";
                               var create = $("#chbox1").is(':checked') ? "True" : "False";
                               var set = $("#chbox3").is(':checked') ? "True" : "False";
                               var hot = $("#chbox2").is(':checked') ? "True" : "False";
                               var Mail = $('#mailtxtbox').val();
                               var password = $('#passtxtbox').val();
                               var admindata = { '_email': Mail, '_paass': password, '_manage': manage, '_create': create,'_hot':hot,'_set':set };
                               $.ajax({
                                   url: '/Moderator/CreatModerator',
                                   type: 'post',
                                   data: admindata,
                                   success: function (data) {
                                       $("#MSG").text(data);
                                   },
                                   error: function (data) {
                                       alert(data.responseText);
                                   }
                               });
                           });
                       }
                   });
               }
           }
           $(D).ready(function ($) {
               JQUERY4U.UTIL.setupFormValidation();
           });

       })(jQuery, window, document);