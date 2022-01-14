var Msg = {
    Insert: 'Record has been inserted successfully.',
    Update: 'Record has been updated successfully.',
    Delete: 'Record has been deleted successfully.',
    PasswordLength: 'Password should contain: <br> 1. At least 8 characters. <br>2. At least one lowercase character. <br>3. At least one uppercase character. <br>4.  At least one numeric value(0-9). <br>5. At least one special character(eg. @$!%*?&). <br>6. No spaces in between.',
    NoMenuAccess: 'You have no access to login software from this computer.',
    NoCompAccess: 'You have no access to login software from this computer.',
    backDate: 'Your System\'s Date & time is not matching with the server\'s date & time settings. Please make proper settings before login.',
    userBackDate: 'Your System\'s Date & time is not matching with the server\'s date & time settings. Please make proper settings before login.',
    DemoExpired: 'Your Software is current in Temporary Registration. Kindly contact our customer relations Dept On 9782511333 and Licence your software.',
    RgExpired: 'Your software is not yet registered.<br/>Kindly contact ASMI GLOBAL SOFTWARES PVT LTD Technical Assistance on :-<br/> 89528-54321<br/>97825-11333',
    Regnotregister: 'Your software is not yet registered.<br/>Kindly contact ASMI GLOBAL SOFTWARES PVT LTD Technical Assistance on :-<br/> 89528-54321<br/>97825-11333',
    staticAccess: 'You have no access for static IP to login software.',
    biometricThumb: 'You are not login by biometric thumb impression first login in biometric thumb then login software.',
    passwordExpired: 'Your password has been expired. Please change your password then login again.',
    loginName: 'The User Code or Company Name is incorrect. Please make correct entry.',
    dllMismatch: 'Your software and DB version are not match. Please contect to your IT department and update application',

    UsrCode: 'Please enter user code.',
    UsrPass: 'Please enter password.',
    UsrPassCreate: 'Minimum 8 characters are required in password.',
    PassMatch: 'Please enter correct password',
    Already: 'Record already exist.',
    Name: 'Please enter name.',
    Code: 'Please enter code.',
    LinkedRec: 'You can not delete linked record.',
    BlankSelect: 'Please select ',
    BlankText: 'Please enter ',
    InvalidFile: 'Please select valid file formats.',
    MaxFileLimit: 'You can not select more then 5 files.',
    ImgUpload: 'Imgaes have been uploaded successfully.',
    UserCode: 'User code already exist. Please try something else.',
    TableRow: 'There no row in ',
    SelectRow: 'Please select at least one row in ',
    Linked: 'Data has been linked successfully.',
    Unlinked: 'Data has been unlinked successfully.',
    Greater: 'can not be greater then ',
    Lesser: 'can not be less then ',
    Error: 'Some error occured.',
};

var MsgArray = [];

var MsgContain = $('.MsgPopUp');
MsgContain.hide();
function MsgColor(flag) {
    $(".MsgPopUp").removeClass("msgDanger msgSecure");
    if (flag)
        $(".MsgPopUp").addClass("msgDanger");
    else
        $(".MsgPopUp").addClass("msgSecure");
}
function MsgAlert() {
    $('.Msg').remove();
    $.each(MsgArray, function (i, val) {
        MsgContain.append('<div class="Msg">' + val + '</div>');
    });
    MsgArray = [];
    var width = parseInt(parseInt($(".MsgPopUp").width()) / 2);
    MsgContain.slideDown();
    window.setTimeout(function () {
        MsgContain.slideUp();
    }, 5000);
}


function find_in_object(my_object, my_criteria) {

    return my_object.filter(function (obj) {
        return Object.keys(my_criteria).every(function (c) {
            return obj[c] == my_criteria[c];
        });
    });

}

function CreateTable(id, Data) {
    //Build an array containing Customer records.
    var header = Object.keys(Data.Table1[0]);

    var customers = new Array();
    customers.push(["Customer Id", "Name", "Country"]);
    customers.push([1, "John Hammond", "United States"]);
    customers.push([2, "Mudassar Khan", "India"]);
    customers.push([3, "Suzanne Mathews", "France"]);
    customers.push([4, "Robert Schidner", "Russia"]);

    //Create a HTML Table element.
    var table = $("<table />");
    table[0].border = "1";

    //Get the count of columns.
    var columnCount = header[0].length;

    //Add the header row.
    var row = $(table[0].insertRow(-1));
    for (var i = 0; i < columnCount; i++) {
        var headerCell = $("<th />");
        headerCell.html(header[i]);
        row.append(headerCell);
    }

    //Add the data rows.
    for (var i = 1; i < customers.length; i++) {
        row = $(table[0].insertRow(-1));
        for (var j = 0; j < columnCount; j++) {
            var cell = $("<td />");
            cell.html(customers[i][j]);
            row.append(cell);
        }
    }

    var dvTable = id;
    dvTable.html("");
    dvTable.append(table);
}


function jqPost(url, data) {

    var retJson = [];
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        async: false,
        success: function (json) {

            retJson = json;

        },
        error: function () {

            retJson = [];
        }
    });
    if (retJson.length != 0) {
        var obj = eval("(" + retJson.d + ")");
        return obj;
    } else {
        return [];
    }
}
function isEmptyObj(obj) {
    for (var i in obj) {
        return false;
    }
    return true;
}
function fn_getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)')
        .exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}