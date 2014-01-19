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

function Modify() {
    window.location.href = "/Moderator/ModifyAdmin";
}

function Serchlist() {
    window.location.href = "/Moderator/SearchList";
}

function Logout() {
    window.location.href = "/Moderator/LogOut";
}