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
                        $('.blog-box').append('<div class="blog-post gallery-post">'
							+'<div class="inner-post">'
								+'<div class="flexslider">'
									+'<ul class="slides">'
									+'<li>'
										+'<img alt="" src="'+List.image+'" />'
										+'</li>'
										+'<li>'
										+	'<img alt="" src="'+List.image+'" />'
										+'</li>'
									+'</ul>'
								+'</div>'
								+'<div class="post-content">'
									+ '<h2 style="text-align:right;">' + '<a href="/Lists/ViewList?_ID=' + List.ID + '">' + List.Title + '</a></h2>'
									+'<a href="/Lists/ViewList?_ID='+List.ID+'" class="button alt">Read Now</a>'
								+'</div>'
								+'<ul class="post-tags">'
									+'<li><a href="#"><i class="fa fa-comment"></i>'+ List.Tag.replace(/_/g, " ")+'</a></li>'
									+'<li><a href="#"><i class="fa fa-calendar"></i>Dec 19, 2013</a></li>'
								+'</ul>'					
							+'</div>'
						+ '</div>');



                        //$('#ListCont').append('<div class="4u" style="margin-bottom: 50px;">'
                        //            + '<section class="box" style=" height:500px; position:relative;">'
                        //                + '<a href="/Lists/ViewList?_ID=' + List.ID + '" class="image image-full">'
                        //                    + '<img src="' + List.image + '" alt="" /></a>'
                        //                     + ' <div class="listtagcont">'
                        //                + ' <div class="boxtag"> <span>' + List.Tag.replace(/_/g, " ") + '</span></div>'
                        //               + ' </div>'
                        //                + '<header>'
                        //                    + '<h3 style="text-align:right;">' + List.Title + '</h3>'
                        //                + '</header>'
                        //                + '<footer>'
                        //                    + '<a href="/Lists/ViewList?_ID=' + List.ID + '"  class="button fa fa-file-text" style="background-color:#000;" >Read Now</a>'
                        //                + '</footer>'
                        //            + '</section>'
                        //        + '</div>');
                    });
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
        }
    }, _throttleDelay);
}