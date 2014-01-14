$(document).ready(function () {
    $('#AddMod').click(function () {

        AddModeratorPopup();
    });
});

function AddModeratorPopup(id) {
    var height = $(document).height();
    var poph = $('#newmoderator').height();
    var margin = (height - poph) / 4;
    $('#newmoderator').css({ "margin-top": margin });
    $('#newmoderator').slideToggle(0);
    $('#darkLayer').slideToggle(0);
    $('#darkLayer').css({ "height": height });
    $('body').tooltipster('hide');

}