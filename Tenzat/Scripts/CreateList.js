(function ($, W, D) {
    var JQUERY4U = {};
    JQUERY4U.UTIL =
    {
        setupFormValidation: function () {
            $("#createlist").validate({
                rules: {
                    listtitl: {
                        required: true
                    },
                    tagdroplist: {
                        required: true
                    },
                    img1: {
                        required: true
                    },
                    il1: {
                        required: true
                    },
                    il2: {
                        required: true
                    },
                    il3: {
                        required: true
                    },
                    il4: {
                        required: true
                    },
                    il5: {
                        required: true
                    },
                    il6: {
                        required: true
                    },
                    il7: {
                        required: true
                    },
                    il8: {
                        required: true
                    },
                    il9: {
                        required: true
                    },
                    il10: {
                        required: true
                    },
                    dt1: {
                        required: true
                    },
                    dt2: {
                        required: true
                    },
                    dt3: {
                        required: true
                    },
                    dt4: {
                        required: true
                    },
                    dt5: {
                        required: true
                    },
                    dt6: {
                        required: true
                    },
                    dt7: {
                        required: true
                    },
                    dt8: {
                        required: true
                    },
                    dt9: {
                        required: true
                    },
                    dt10: {
                        required: true
                    }
                },
                messages: {
                    listtitl: {
                        required: "Please enter list title"

                    },
                    tagdroplist: {
                        required: "Please enter list tag"

                    },
                    il1: {
                        required: "Please enter ItemTitle "

                    },
                    il2: {
                        required: "Please enter ItemTitle "

                    },
                    il3: {
                        required: "Please enter ItemTitle "

                    },
                    il4: {
                        required: "Please enter ItemTitle "

                    },
                    il5: {
                        required: "Please enter ItemTitle "

                    },
                    il6: {
                        required: "Please enter ItemTitle "

                    },
                    il7: {
                        required: "Please enter ItemTitle "

                    },
                    il8: {
                        required: "Please enter ItemTitle "

                    },
                    il9: {
                        required: "Please enter ItemTitle "

                    },
                    il10: {
                        required: "Please enter ItemTitle "

                    },
                    dt1: {
                        required: "Please enter Item Details "

                    },
                    dt2: {
                        required: "Please enter Item Details "

                    },
                    dt3: {
                        required: "Please enter Item Details "

                    },
                    dt4: {
                        required: "Please enter Item Details "

                    },
                    dt5: {
                        required: "Please enter Item Details "

                    },
                    dt6: {
                        required: "Please enter Item Details "

                    },
                    dt7: {
                        required: "Please enter Item Details "

                    },

                    dt8: {
                        required: "Please enter Item Details "

                    },
                    dt9: {
                        required: "Please enter Item Details "

                    },
                    dt10: {
                        required: "Please enter Item Details "

                    }

                },
                submitHandler: function (form) {
                    $(function () {
                      //  alert('submit ok');
                        //VALIDATE IMAGE
                        //for(var i=1;i<=10;i++)
                        //{
                        //    if ($('#img' + i).attr('src') == "/content/imgs/file-upload.jpg")
                        //    {
                        //        alert('You must Upload Image to every List item !');
                        //        return;
                        //    }
                        //}

                        var ListTitle = $('#ListTitle').val();
                        var Image = 0;
                        var ImageUrl;
                        var videoUrl;
                        var VideoLink = 0;
                        var listType;
                        var VideoObject = "";
                        var ListOfVideos = "";
                        if ($('#RImg').is(':checked')) {
                            Image = $('#ImageList').val();
                            listType = 'Img';
                        }
                        if ($('#RVid').is(':checked')) {
                            VideoLink = $('#Vid').val();
                            listType = 'Vid';
                        }
                        var Tag = $('#droptag').val();
                        var ListItems = new Array();
                        for (var i = 1; i <= 10; i++) {
                            var Title = $('#itemtitle' + i).val();
                            var Details = $('#details' + i).val();
                            ListItems.push(Title);
                            ListItems.push(Details);
                        }
                        if (Image != 0) {
                            ImageUrl = Image;
                        }
                        if (VideoLink != 0) {
                            videoUrl = VideoLink.replace("watch?v=", "embed/");

                        }
                        for (var i = 1; i <= 10; i++) {
                            if ($('#LIVID' + i).val() != '') {
                                var LIVid = $('#LIVID' + i).val();
                                LIVid = LIVid.replace("watch?v=", "embed/");
                                VideoObject += "{Order : " + i + ",URL : \"" + LIVid + "\"},";
                            }
                            if (i == 10) {
                                var VideoLength = VideoObject.length;
                                ListOfVideos = VideoObject.substr(0, VideoLength - 1);
                            }
                        }
                        var data = { 'ListTitle': ListTitle, 'listtype': listType, 'ImageURL': ImageUrl, 'VideoURL': videoUrl, 'Tag': Tag, 'ListItems': ListItems, 'VideosUrls': '[' + ListOfVideos + ']' };
                        $.ajax({
                            url: '/Moderator/AddList',
                            type: 'post',
                            data: data,
                            traditional: true,
                            success: function (data) {
                                $("#MSG").text(data);
                            },
                            error: function (data) {
                                alert(data.responseText);
                            }
                        });
                    });
                }
            });
        }
}

    $(D).ready(function ($) {
        JQUERY4U.UTIL.setupFormValidation();
    });

})(jQuery, window, document);

