var First = true;
var ChangedID;

function OpenFileDialog(id) {
    $(".FileDialog").click();
    ChangedID = id;
}

function Show() {
    if (First == true) {
        $('#Vid').show();
        $('#ListImg').hide();
        $('.VideoItemElements').show();
        $('.ImageItemElements').hide();
        $('.rds').hide();
        First = false;
    }
    else {
        $('#Vid').hide();
        $('#ListImg').show();
        $('.VideoItemElements').hide();
        $('.ImageItemElements').show();
        $('.rds').show();
        First = true;
    }
}


$(document).ready(function () {
    // document.getElementById("RImg").checked = true
    var ImageInBase64;

    $("#ListImg").change(function (e) {
        readImagesFromUploader(e);
        var interval = setInterval(function () {
            if (ImageInBase64 != undefined && ImageInBase64 != null) {
                $('#ImageList').val(ImageInBase64);
                clearInterval(interval);
            }
        }, 1000);
    });


    $("#FileImage").change(function (e) {
        readImagesFromUploader(e);
        var interval = setInterval(function () {
            if (ImageInBase64 != undefined && ImageInBase64 != null) {
                $('#Image' + ChangedID).val(ImageInBase64);
                clearInterval(interval);
                UploadImage(ChangedID);
            }
        }, 1000);
    });

    function UploadImage(id) {
        var orderid = id;
        var imageinbase = $('#Image' + id).val();
        var data = { 'orderid': orderid, 'ImageBase64': imageinbase };
        $.ajax({
            url: '/Moderator/UploadImage',
            type: 'post',
            data: data,
            success: function (data) {
                $('#img' + id).attr('src', data);
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }

    function readImage(file, event) {
        ImageInBase64 = event.target.result.replace("data:image/png;base64,", "");
        ImageInBase64 = ImageInBase64.replace("data:image/jpeg;base64,", "");
        ImageInBase64 = ImageInBase64.replace("data:image/jpg;base64,", "");
        ImageInBase64 = ImageInBase64.replace("data:image/gif;base64,", "");
    }

    function readImages(event) {
        var files = event.originalEvent.dataTransfer.files;
        $.each(files, function (index, file) {
            var fileReader = new FileReader();
            fileReader.onload = (function (file) {
                return function (event) {
                    return readImage(file, event);
                }
            })(file);
            fileReader.readAsDataURL(file);
        });
    }

    function readImagesFromUploader(event) {
        var files = event.target.files;
        $.each(files, function (index, file) {
            var fileReader = new FileReader();
            fileReader.onload = (function (file) {
                return function (event) {
                    return readImage(file, event);
                }
            })(file);
            fileReader.readAsDataURL(file);
        });
    }

});

function ChangeImgVid(id) {
    var counter = id.substr(2, 1);
    if (id.indexOf('10') != -1) {
        counter = 10;
    }
    if (id.indexOf('vd') != -1) {
        $('#VideoItemElements' + counter).show();
        $('#ImageItemElements' + counter).hide();
    }
    else {
        $('#VideoItemElements' + counter).hide();
        $('#ImageItemElements' + counter).show();
    }
}

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

                        for(var i=1;i<=10;i++)
                        {
                            if ($('#imgItemR' + i).is(':checked'))
                            {
                                if ($('#img' + i).attr('src') == "/content/imgs/file-upload.jpg") {
                                    alert('You must Upload Image to List item !');
                                    return;
                                }
                            }
                            if ($('#vidItemR' + i).is(':checked'))
                            {
                                if ($('#LIVID' + i).val() == "") {
                                    alert('You must Upload Video to List item !');
                                }
                            }
                        }

                        var ListTitle = $('#ListTitle').val();
                        var Image = 0;
                        var ImageUrl;
                        var videoUrl;
                        var VideoLink = 0;
                        var listType;
                        var VideoObject = "";
                        var ListOfVideos = "";
                        //if ($('#RImg').is(':checked')) {
                        Image = $('#ImageList').val();
                        //    listType = 'Img';
                        //}
                        //if ($('#RVid').is(':checked')) {
                        //    VideoLink = $('#Vid').val();
                        //    listType = 'Vid';
                        //}
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
                        var data = { 'ListTitle': ListTitle, 'listtype': 'Img', 'ImageURL': ImageUrl, 'VideoUrl': videoUrl, 'Tag': Tag, 'ListItems': ListItems, 'VideosUrls': '[' + ListOfVideos + ']' };
                        $.ajax({
                            url: '/Moderator/AddList',
                            type: 'post',
                            data: data,
                            traditional: true,
                            success: function (data) {
                                $("#MSG").text(data);
                                var height = $(document).height();
                                window.scroll(0, height);
                                setInterval(function () {
                                    window.location.href = '/Moderator/CreateList';
                                }, 3000);
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

