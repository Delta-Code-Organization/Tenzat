$(document).ready(function () {
    $.ajax({
        url: '/Lists/ListByID',
        type: 'post',
        data: {},
        success: function (data) {
            var tag = data.Tag.replace(/_/g, " ");
            $('#MainList').append('<img alt="" src="' + data.image + '" style="height:350px;width:100%;">'
                + '<h1 style="text-align:center"><strong style="text-alight:center;">' + data.Title + '</strong></h1>'
                + '<div style="position:absolute;opacity:0.7;right:0;top:50px;width:auto;height:45px;background-color:#000;color:#fff;padding:2px; padding-right:10px; padding-left:10px;box-shadow:0px 5px 5px gray;"><h4 style=" text-align:center;line-height:20px;font-size:30px;">' + tag + '</h4></div>');
         },
        error: function (data) {
            alert(data.responseText);
        }
    });
});

