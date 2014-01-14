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
                $('.searchalllists').append('<div class="itemview1" >'
+ '<div class="item">'
+ '<img src="' + res.image + '"/>'
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