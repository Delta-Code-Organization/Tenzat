$(document).ready(function () {
    $.ajax({
        url: '/Lists/ListByID',
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

