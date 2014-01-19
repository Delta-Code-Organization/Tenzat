var SetHotList = false;
var ApproveList = false;
var counter = 0;

$(document).ready(function () {
    $.ajax({
        url: '/Moderator/GetPermission',
        type: 'post',
        data: {},
        success: function (data) {
            ApproveList = data.ApproveLists;
            SetHotList = data.SetHotLists;
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
});

function searching() {
    var searchword = $('#tboxx').val();
    var searchtag = $('#droptag').val();
    var e = document.getElementById("droptag");
    var selectedtag = e.options[e.selectedIndex].value;
    $.ajax({
        url: '/Moderator/search',
        type: 'post',
        data: { '_title': searchword, '_tag': selectedtag },
        success: function (data) {
            $('.searchalllists').empty();
            $.each(data, function (Index, res) {
                $('.searchalllists').append('<div class="itemview1" id="listt'+ res.ID +'">'
+ '<div class="item">'
+ '<img src="' + res.image + '"/>'
+ '<h2>' + res.Title + '</h2>'
+ '<div class="hotset '+ counter + '">'
+ '</div>'
+ '</div>'
+ '</div>');
                if (SetHotList == true) {
                    $('.' + counter).append('<input type="submit" value="Set Hot" class="setbtn" onclick="Hot(' + res.ID + ')">');
                }
                if (ApproveList == true) {
                    $('.' + counter).append('<input type="submit" value="Confirm" class="setbtn" onclick="Listconfirm(' + res.ID + ')">'
                        + '<input type="submit" value="Remove" class="setbtn" onclick="Listremove(' + res.ID + ')">');
                }
                counter++;
            });
        }
    });
}


function Listremove(id) {
    $.ajax({
        url: '/Moderator/Remove',
        type: 'post',
        data: { '_id': id },
        success: function (data) {
            $('#listt' + id).fadeOut(500);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
    }
    
    function Listconfirm(id) {
    $.ajax({
        url: '/Moderator/confirm',
        type: 'post',
        data: { '_id': id },
        success: function (data) {
            $('#listt' + id).fadeOut(500);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function Hot(id) {
    $.ajax({
        url: '/Moderator/SetHot',
        type: 'post',
        data: { '_id': id },
        success: function (data) {
            $('#listt' + id).fadeOut(500);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}