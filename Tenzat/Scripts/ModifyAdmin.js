$(document).ready(function () {
    $('#AddMod').click(function () {

        AddModeratorPopup();
    });
});

var AdminID;

function AddModeratorPopup(id) {
    var height = $(document).height();
    var poph = $('#newmoderator').height();
    var margin = (height - poph) / 4;
    $('#newmoderator').css({ "margin-top": margin });
    $('#newmoderator').slideToggle(0);
    $('#darkLayer').slideToggle(0);
    $('#darkLayer').css({ "height": height });
    AdminID = id;

}

function DeleteModerator(id)
{
    $.ajax({
        url: '/Moderator/DeleteModerator',
        type: 'Post',
        data: { '_ID': id },
        success: function (data) {
            $('.' + id).fadeOut(500);
        },
        error: function (data) {alert(data.responseText) }
    });
}

function EditAdmin()
{
    var Email = $('#email').val();
    var Password = $('#pass').val();
    var createMod = $('#ismod').is(":checked");
    var CreateList = $('#Cat').is(":checked");
    var sethotlist = $('#SRC').is(":checked");
    var approvelist = $('#Env').is(":checked");
    var data = { 'Email': Email, '_ID': AdminID, 'Password': Password, 'CreateAdmin': createMod, 'SethotList': sethotlist, 'CreateList': CreateList, 'ApproveList': approvelist };
    $.ajax({
        url: '/Moderator/UpdateAdmin',
        type: 'Post',
        data: data,
        success: function (data) {
            
        },
        error: function (data) { alert(data.responseText) }
    });
}

