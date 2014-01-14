
function Add_Or_Remove() {
    window.location.href = "/Moderator/CreateAdmin";
}


//function searchbytitle() {
//    var titlesearch = $('#TXTBX').val();
//    var tagsearch = $('#droplist').val();
//    var searchdata = { '_title': titlesearch,'_tag':tagsearch };
//    $.ajax({
//        url: '/Moderator/Search',
//        type: 'post',
//        data: searchdata,
//        success: function (data) {
//            alert(data);
//        }
//    });
//};                 

function searching() {
    $('#tboxx').change(function () {
        var searchword = $('#tboxx').val();
    });
        $('#droptag').change(function () {
            var searchtag = $('#droptag').val();
        });
        if (searchword != '' && searchtag == '') {
            $.ajax({
                url: '/Moderator/serchtitle',
                type: 'post',
                data: { '_title': searchword },
                success: function (data) {
                    $.each(data, function (Index, res) {
                        $('.searchalllists').append('<div class="itemview1" >'
    + '<div class="item">'
    + '<img src=' + res.image + '/>'
     + '<h2>' + res.Title + '</h2>'
       + '<div class="hotset">'
    + '<input type="submit" value="Confirm" class="setbtn">'
           + '<input type="submit" value="Remove" class="setbtn">'
    + '</div>'
       + '</div>'
        + '</div>');
                    });
                }
            });
        }
        else if (searchword == '' && searchtag != '') {
            var e = document.getElementById("droptag");
            var selectedtag = e.options[e.selectedIndex].value;
            $.ajax({
                url: '/Moderator/serchtag',
                type: 'post',
                data: { '_tag': selectedtag },
                success: function (data) {
                    $.each(data, function (Index, res) {
                        $('.searchalllists').append('<div class="itemview1" >'
    + '<div class="item">'
    + '<img src='+res.image+'/>'
     + '<h2>' + res.Title + '</h2>'

       + '<div class="hotset">'
    + '<input type="submit" value="Confirm" class="setbtn">'
           + '<input type="submit" value="Remove" class="setbtn">'
    + '</div>'
       + '</div>'
        + '</div>');
                    });
                }
            });
                }
        else {
            var e = document.getElementById("droptag");
            var selectedtag = e.options[e.selectedIndex].value;
            $.ajax({
                url: 'Moderator/SearchTagTitle',
                type: 'post',
                data: { '_tag': selectedtag, '_title': searchword },
                success: function (data) {
                    $.each(data, function (Index, res) {
                        $('.searchalllists').append('<div class="itemview1" >'
    + '<div class="item">'
    + '<img src='+res.image+'/>'
     + '<h2>' + res.Title + '</h2>'
       + '<div class="hotset">'
    + '<input type="submit" value="Confirm" class="setbtn">'
           + '<input type="submit" value="Remove" class="setbtn">'
    + '</div>'
       + '</div>'
        + '</div>');
                    });
                }
            });
        }
    }