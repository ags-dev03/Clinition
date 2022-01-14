$(document).ready(function () {
    document.oncontextmenu = function () { return false; };
    $(document).mousedown(function (e) {
        if (e.button == 2) {
            return false;
        }
        return true;
    });
});

$(document).ready(function () {
    $('#Ul_menu li').click(function (e) {
        if (e.ctrlKey == true) {
            return false;
        }
        return true;
    });
});

function disableF5(e) {
    if (e.ctrlKey && ((e.which || e.keyCode) == 82) || (e.which || e.keyCode) == 116) e.preventDefault();
}

$(document).ready(function () {
    $(document).on("keydown", disableF5);
});



function changeHashOnLoad() {
    //preloader();
    window.location.href += '#';
    setTimeout('changeHashAgain()', '50');
}

function changeHashAgain() {
    window.location.href += '1';
}

var storedHash = window.location.hash;
window.setInterval(function () {
    if (window.location.hash != storedHash) {
        window.location.hash = storedHash;
    }
}, 50);
window.onload = changeHashOnLoad;

