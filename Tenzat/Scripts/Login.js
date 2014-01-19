           (function ($, W, D) {
           var JQUERY4U = {};
JQUERY4U.UTIL =
{
    setupFormValidation: function () {
        $("#adminform").validate({
            rules: {
                AdminMail: {
                    email:true,
                    required: true        
                },
                Adminpass: {
                    minlength:5,
                    required: true     
                }
            },
            messages: {
                AdminMail: {
                    email:"Please enter a valid Email address",
                    required: "Please enter your Email "
                },
                Adminpass: {
                    minlength:"Your password must be at least 5 characters",
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
                            window.location.href = '/Moderator/ControlPanel';
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