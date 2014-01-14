           (function ($, W, D) {
           var JQUERY4U = {};
JQUERY4U.UTIL =
{
    setupFormValidation: function () {
        $("#adminform").validate({
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
                    required: "Please enter your Email "
                },
                Adminpass: {
                    required: "Please enter your password "
                },
            },
            submitHandler: function (form) {
                $(function () {
                    var Mail = $('#mailbox').val();
                    var password = $('#passbox').val();
                    var admindata = { '_mail': Mail, '_pass': password };
                    $.ajax({
                        url: '/Moderator/logadmin',
                        type: 'post',
                        data: admindata,
                        success: function (data) {
                            $('#logres').text(data);
                        },
                        error:function(data){
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

           function GoToPanel() {
               window.location.href="/Moderator/ControlPanel";
           }