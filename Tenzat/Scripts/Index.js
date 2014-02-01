//$(document).ready(function () {
//    $.ajax({
//        url: '/Home/GetHotList',
//        type: 'post',
//        data: {},
//        success: function (data) {
//            $('#BannerCont').append('<section id="banner">'
//									+ '<a href="/Lists/ViewList?_ID=' + data.ID + '">'
//										+ '<span class="image image-full"><img id="BannerIMG" style="height:500px;" src="' + data.image + '" alt="" /></span>'

//                                        + '<header style="height:auto;opacity:0.9;">'
//											+ '<h2 style="display: inline;">' + data.Title + '</h2>'
//											+ '<span class="byline">' + data.Tag.replace(/_/g, " ") + '</span>'
//										+ '</header>'
//									+ '</a>'
//								+ '</section>');
//        },
//        error: function (data) {
//            alert(data.responseText);
//        }
//    });
//});

var _throttleTimer = null;
var _throttleDelay = 100;
var counter = 0;

function convertDate(inputFormat) {
    var d = new Date(inputFormat);
    return [d.getMonth() + 1, d.getDate(), d.getFullYear()].join('/');
}

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
                        var milli = List.ListDate.replace(/\/Date\((-?\d+)\)\//, '$1');
                        var LD = new Date(parseInt(milli));
                        var $newItems = $('<div class="blog-post gallery-post">'
							+ '<div class="inner-post">'
								+ '<div class="flexslider">'
									+ '<ul class="slides" id="ImageSlider' + counter + '">'
									+ '<li>'
                                    + '<a href="/Lists/ViewList?_ID=' + List.ID + '" >'
										+ '<img alt="" style="height:200px;" src="' + List.image + '" /></a>'
										+ '</li>'
									+ '</ul>'
								+ '</div>'
								+ '<div class="post-content">'
									+ '<h2 style="text-align:right;">' + '<a href="/Lists/ViewList?_ID=' + List.ID + '">' + List.Title + '</a></h2>'
									+ '<a href="/Lists/ViewList?_ID=' + List.ID + '" class="button alt">اقرأ المزيد</a>'
								+ '</div>'
								+ '<ul class="post-tags">'
									+ '<li><a href="#"><i class="fa fa-comment"></i>' + List.Tag.replace(/_/g, " ") + '</a></li>'
									+ '<li><a href="#"><i class="fa fa-calendar"></i>' + convertDate(LD) + '</a></li>'
								+ '</ul>'
							+ '</div>'
						+ '</div>')
                        $('.blog-box').append($newItems).isotope('addItems', $newItems).isotope('appended', $newItems);
                        $.each(List.ListItems, function (index, LI) {
                            $('#ImageSlider' + counter).append('<li>'
                                + '<a href="/Lists/ViewList?_ID=' + List.ID + '" >'
										+ '<img alt="" style="height:200px;" src="' + LI.Drawable + '" /></a>'
										+ '</li>');
                        });
                        counter++;
                    });
                    $('.flexslider').flexslider();
                    setTimeout(function () {
                        $('.blog-box').isotope('reLayout');
                    }, 100);
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
    }, _throttleDelay);
}