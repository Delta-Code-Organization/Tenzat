//function searching() {
//    var searchword = $('#tboxx').val();
//    var searchtag = $('#droptag').val();
//    var e = document.getElementById("droptag");
//    var selectedtag = e.options[e.selectedIndex].value;
//    $.ajax({
//        url: '/Moderator/search',
//        type: 'post',
//        data: { '_title': searchword, '_tag': selectedtag },
//        success: function (data) {
//            $('.searchalllists').empty();
//            $.each(data, function (Index, res) {
//                $('.searchalllists').append('<div class="itemview1" >'
//+ '<div class="item">'
//+ '<img src="' + res.image + '"/>'
//+ '<h2>' + res.Title + '</h2>'
//+ '<div class="hotset">'
//+ '<input type="submit" value="Confirm" class="setbtn">'
//   + '<input type="submit" value="Remove" class="setbtn">'
//+ '</div>'
//+ '</div>'
//+ '</div>');
//            });
//        }
//    });
//}







//function searching() {
//    var searchword = $('#tboxx').val();
//    var searchtag = $('#droptag').val();
//    var e = document.getElementById("droptag");
//    var selectedtag = e.options[e.selectedIndex].value;
//    $.ajax({
//        url: '/Moderator/search',
//        type: 'post',
//        data: { '_title': searchword, '_tag': selectedtag },
//        success: function (data) {
//            $('.searchalllists').empty();
//            $.each(data, function (Index, res) {
//                $('.searchalllists').append('<div class="itemview">'
//                    + '<div class="item">'
//                    + '<img src="' + res.image + '"/>'
//                    + '<h2>' + res.Title + '</h2>'
//                    + '<div class="hotset">'
//                    + '<input type="button" value="Confirm" class="setbtn" onclick="Listconfirm(' + res.ID + ')">'
//                    + '<input type="button" value="Remove" class="setbtn" onclick="Listremove(' + res.ID + ')">'
//                    + '</div>' + '</div>' + '</div>');
//            });
//        }
//    });
//} function Listremove(id) {
//    $.ajax({
//        url: '/Lists/Remove',
//        type: 'post',
//        data: { '_id': id },
//        success: function (data) {
//            alert(data);
//        }
//    });
//} function Listconfirm(id) {
//    $.ajax({
//        url: '/Lists/confirm',
//        type: 'post',
//        data: { '_id': id },
//        success: function (data) {
//            alert(data);
//        }
//    });
//}




function MakeHot() {
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
                $('.searchalllists').append('<div class="itemview1" >'
                    + '<div class="item">'
                    + '<img src="' + res.image + '"/>'
                    + '<h2>' + res.Title + '</h2>'
                    + '<div class="hotset">'
                    + '<input type="submit" value="HotList" class="setbtn" onclick="Hot(' + res.ID + ')">'
                    + '</div>'
                    + '</div>'
                    + '</div>');
            });
        }
    });
} function Hot(id) {
    $.ajax({
        url: '/Lists/SetHot',
        type: 'post',
        data: { '_id': id },
        success: function (data) {
            alert(data);
        }
    });
}