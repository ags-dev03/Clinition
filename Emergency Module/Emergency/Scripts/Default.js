$(function () {
    function CorrectPass() {
        MsgArray[0] = Msg["PassMatch"];
        MsgColor(true);
        MsgAlert();
    }
    $('.Forget_mob').hide();
    $('.Forget_email').hide();
    $('.Forget_Pass').hide();
    $('.Forget_next').hide();
    
});
function LogIn() {
    if ($('#txtUserName').val() != "") {
        if ($('#txtUserPass').val() == "") 
            MsgArray[0] = Msg["UsrPass"];
    }
    else {
        MsgArray[0] = Msg["UsrCode"];
        if ($('#txtUserPass').val() == "")
            MsgArray[1] = Msg["UsrPass"];
    }

    if (MsgArray.length > 0) {
        MsgColor(true);
        MsgAlert();
        return false;
    }
    else
        return true;
}

