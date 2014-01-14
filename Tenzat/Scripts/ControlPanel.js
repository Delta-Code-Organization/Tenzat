function myfunction(ID) {
    $("#" + ID).css({ "opacity": "1" });


}
function myfunction2(ID) {
    $("#" + ID).css({ "opacity": "0.9" });


}


function ModeratorManager() {
    window.location.href = "/Moderator/CreateAdmin";
}

function creatlist() {
    window.location.href = "/Moderator/CreateList";
}

function Hotlist() {
    window.location.href = "/Moderator/SetHotList";
}

function Serchlist() {
    window.location.href = "/Moderator/SearchList";
}