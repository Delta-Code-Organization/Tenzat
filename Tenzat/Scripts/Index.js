$(document).ready(function () {
    $.ajax({
        url: '/Home/GetHotList',
        type: 'post',
        data: {},
        success: function (data) {
            $('#BannerCont').append('<section id="banner">'
									+ '<a href="/Lists/ViewList?_ID' + data.ID + '">'
										+ '<span class="image image-full"><img id="BannerIMG" style="height:500px;" src="' + data.image + '" alt="" /></span>'
                                      
                                        + '<header>'
											+ '<h2>' + data.Title + '</h2>'
											+ '<span class="byline">' + data.Tag + '</span>'
										+ '</header>'
									+ '</a>'
								+ '</section>');
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
});

var _throttleTimer = null;
var _throttleDelay = 100;

$(document).ready(function () {
    $(window)
        .off('scroll', ScrollHandler)
        .on('scroll', ScrollHandler);
});

function isEmpty(obj) {
    for (var prop in obj) {
        if (obj.hasOwnProperty(prop))
            return false;
    }
    return true;
}

function ScrollHandler(e) {
    //throttle event:
    clearTimeout(_throttleTimer);
    _throttleTimer = setTimeout(function () {
        console.log('scroll');

        //do work
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {
            $.ajax({
                url: '/Home/GetLatest',
                type: 'post',
                data: {},
                beforeSend: function () {
                    //$('#Loader').html('<h3 style="background-color:#f8d03b; text-align:center; color:#fff; width:100%; margin-top:0px; margin-bottom:0px; height:50px; line-height:50px;">تحميل المنشورات</h3><br />');
                },
                complete: function () {
                    //$("#Loader").empty();
                },
                success: function (data) {
                    if (isEmpty(data) == true) {
                        return;
                    }
                    $.each(data, function (index, List) {
                        $('#ListCont').append('<div class="4u" style="margin-bottom: 50px;">'
                                    + '<section class="box">'
                                        + '<a href="/Lists/ViewList?_ID=' + List.ID + '" class="image image-full">'
                                            + '<img src="' + List.image + '" alt="" /></a>'
                                             + ' <div class="listtagcont">'
                                        + ' <div class="boxtag"> <span>' + List.Tag + '</span></div>'
                                       + ' </div>'
                                        + '<header>'
                                            + '<h3>' + List.Title + '</h3>'
                                        + '</header>'
                                        + '<footer>'
                                            + '<a href="/Lists/ViewList?_ID=' + List.ID + '"  class="button fa fa-file-text" >Continue Reading</a>'
                                        + '</footer>'
                                    + '</section>'
                                + '</div>');
                    });
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
    }, _throttleDelay);
}