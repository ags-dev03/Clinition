function Fn_Save() {
    $('.multiple').each(function () {
        var value = "";
        $(this).find('option:selected').each(function () {
            value += $(this).val() + ",";
        });
        $(this).parents('span').next().val(value);
    });
    $('.BlankName').each(function () {
        if ($(this).parent().css('display') !== 'none' && $(this).val() == '') {
            var element = $(this).parent().prev().prev().find('label:eq(0)').text();
            MsgArray[MsgArray.length] = Msg["BlankText"] + element + '.';
            $(this).addClass('border-bottom-Danger');
        }
        else {
            $(this).removeClass('border-bottom-Danger');
        }
    });

    $('.BlankSelect').each(function () {
        if ($(this).parent().css('display') !== 'none' && ($(this).val() == null || $(this).val() == '0')) {
            var element = $(this).parent().prev().find('label:eq(0)').text();
            MsgArray[MsgArray.length] = Msg["BlankSelect"] + element + '.';
            $(this).addClass('border-bottom-Danger');
        }
        else {
            $(this).removeClass('border-bottom-Danger');
        }
    });

    $('.MultiBlankSelect').each(function () {
        if ($(this).parents('span').parent().css('display') !== 'none' && $(this).parents('span').next().val() == null || $(this).parents('span').next().val() == '') {
            var element = $(this).parents('span').parent().prev().find('label:eq(0)').text();
            MsgArray[MsgArray.length] = Msg["BlankSelect"] + element + '.';
            $(this).next().addClass('border-bottom-Danger');
        }
        else {
            $(this).next().removeClass('border-bottom-Danger');
        }
    });

    $('.BlankText').each(function () {
        if ($(this).parent().css('display') !== 'none' && $(this).val() == '') {
            var element = $(this).parent().prev().find('label:eq(0)').text();
            MsgArray[MsgArray.length] = Msg["BlankText"] + element + '.';
            $(this).addClass('border-bottom-Danger');

        }
        else {
            $(this).removeClass('border-bottom-Danger');
        }
    });

    $('.Blank').each(function () {
        if ($(this).parent().css('display') !== 'none' && $(this).val() == '') {
            var element = $(this).parent().prev().text();
            MsgArray[MsgArray.length] = Msg["BlankText"] + element + '.';
            $(this).addClass('border-bottom-Danger');
        }
        else {
            $(this).removeClass('border-bottom-Danger');
        }
    });
    

    if (MsgArray.length > 0) {
        MsgColor(true);
        MsgAlert();
        return false;
    }
    else {
        return true;

    }
}


$(document).on('keyup', '.border-bottom-Danger', function () {
    if ($(this).val() != "")
        $(this).removeClass('border-bottom-Danger');
});

$(document).on('blur', '.BlankText', function () {
    if ($(this).parent().css('display') !== 'none' && $(this).val() == '') {
        $(this).addClass('border-bottom-Danger');
    }
});

$(document).on('blur', '.MultiBlankSelect', function () {
    if ($(this).parent().css('display') !== 'none' && ($(this).next().next().val() == null || $(this).next().next().val() == '')) {
        $(this).next().addClass('border-bottom-Danger');
    }
    else {
        $(this).removeClass('border-bottom-Danger');
    }
});

$(document).on('blur', '.BlankSelect', function () {
    if ($(this).parent().css('display') !== 'none' && ($(this).val() == null || $(this).val() == '' || $(this).val() == '0')) {
        $(this).addClass('border-bottom-Danger');
    }
    else {
        $(this).removeClass('border-bottom-Danger');
    }
});
$(document).on('blur', '.BlankName', function () {
    if ($(this).parent().css('display') !== 'none' && $(this).val() == '') {
        $(this).addClass('border-bottom-Danger');
    }
});


$(document).on('change', '.img', function () {
    if (parseInt(this.files[0].size) > 500000) {  //500Kb File size 
        MsgArray[MsgArray.length] = 'Image size ' + Msg["Greater"] + '500 KB.';
        MsgColor(true);
        MsgAlert();
        $(this).val('');

    }
});


//Image select in logo and images
function img_pathUrl(input) {
    if (parseInt(input.files[0].size) >= 500000) {
        $('.imgsel')[0].src = '/View/Admin/Images/user.png';
    }
    else {
        $('.imgsel')[0].src = (window.URL ? URL : webkitURL).createObjectURL(input.files[0]);
    }
}


// Thumbnail View of Image in document upload

$(document).on('click', '.uploadimage', function () {
    $(this).next().click();
});

$(document).on('change', '.img_thumb', function () {
    var tmppath = URL.createObjectURL(event.target.files[0]);
    $(this).parent().next().find('img')[0].src = tmppath;
});


