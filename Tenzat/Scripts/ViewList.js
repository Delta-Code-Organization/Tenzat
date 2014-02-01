$(document).ready(function () {
    $.ajax({
        url: '/Lists/ListByID',
        type: 'post',
        data: {},
        success: function (data) {
            $('#MainList').append('<a href="/Lists/ViewList?_ID=' + data.ID + '">' + '<img alt="" src="' + data.image + '" style="height:350px;width:100%;">' + '</a>');
         },
        error: function (data) {
            alert(data.responseText);
        }
    });
});

