$(document).ready(function () {
    $('#AddMod').click(function () {
        AddModeratorPopup();
    });
});

var AdminID;

function ClosePopup()
{
    var height = $(document).height();
    var poph = $('#newmoderator').height();
    var margin = (height - poph) / 4;
    $('#newmoderator').css({ "margin-top": margin });
    $('#newmoderator').slideToggle(0);
    $('#darkLayer').slideToggle(0);
    $('#darkLayer').css({ "height": height });
}

function AddModeratorPopup(id) {
    AdminID = id;
    var data = { '_ID': AdminID };
    $.ajax({
        url: '/Moderator/GetModeratorData',
        type: 'Post',
        data: data,
        success: function (data) {
            $('#email').val(data.Email);
            $('#pass').val(data.password);
            $('#confirmPass').val(data.password);
            $('#ismod').attr('checked', data.CreateAdmin);
            $('#Cat').attr('checked', data.CreateList);
            $('#SRC').attr('checked', data.SethotList);
            $('#Env').attr('checked', data.ApproveList);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
    var height = $(document).height();
    var poph = $('#newmoderator').height();
    var margin = (height - poph) / 4;
    $('#newmoderator').css({ "margin-top": margin });
    $('#newmoderator').slideToggle(0);
    $('#darkLayer').slideToggle(0);
    $('#darkLayer').css({ "height": height });
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
        error: function (data) {
            alert(data.responseText)
        }
    });
}

function EditAdmin()
{
    if ($('#pass').val() != $('#confirmPass').val()) {
        alert("Your Password does't match");
        return;
    }
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
            $('#MSG1').text(data);
        },
        error: function (data) {
            alert(data.responseText)
        }
    });
}

