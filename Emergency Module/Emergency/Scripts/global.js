Object.keys = (function () {
    'use strict';
    var hasOwnProperty = Object.prototype.hasOwnProperty,
        hasDontEnumBug = !({
            toString: null
        }).propertyIsEnumerable('toString'),
        dontEnums = [
            'toString',
            'toLocaleString',
            'valueOf',
            'hasOwnProperty',
            'isPrototypeOf',
            'propertyIsEnumerable',
            'constructor'
        ],
        dontEnumsLength = dontEnums.length;
    return function (obj) {
        if (typeof obj !== 'object' && (typeof obj !== 'function' || obj === null)) {
            throw new TypeError('Object.keys called on non-object');
        }
        var result = [],
            prop, i;
        for (prop in obj) {
            if (hasOwnProperty.call(obj, prop)) {
                result.push(prop);
            }
        }
        if (hasDontEnumBug) {
            for (i = 0; i < dontEnumsLength; i++) {
                if (hasOwnProperty.call(obj, dontEnums[i])) {
                    result.push(dontEnums[i]);
                }
            }
        }
        return result;
    };
}());
if (!String.prototype.includes) {
    (function () {
        'use strict'; // needed to support `apply`/`call` with `undefined`/`null`
        var toString = {}.toString;
        var defineProperty = (function () {
            // IE 8 only supports `Object.defineProperty` on DOM elements
            try {
                var object = {};
                var $defineProperty = Object.defineProperty;
                var result = $defineProperty(object, object, object) && $defineProperty;
            } catch (error) {
            }
            return result;
        }());
        var indexOf = ''.indexOf;
        var includes = function (search) {
            if (this == null) {
                throw new TypeError();
            }
            var string = String(this);
            if (search && toString.call(search) == '[object RegExp]') {
                throw new TypeError();
            }
            var stringLength = string.length;
            var searchString = String(search);
            var searchLength = searchString.length;
            var position = arguments.length > 1 ? arguments[1] : undefined;
            // `ToInteger`
            var pos = position ? Number(position) : 0;
            if (pos != pos) { // better `isNaN`
                pos = 0;
            }
            var start = Math.min(Math.max(pos, 0), stringLength);
            // Avoid the `indexOf` call if no match is possible
            if (searchLength + start > stringLength) {
                return false;
            }
            return indexOf.call(string, searchString, pos) != -1;
        };
        if (defineProperty) {
            defineProperty(String.prototype, 'includes', {
                'value': includes,
                'configurable': true,
                'writable': true
            });
        } else {
            String.prototype.includes = includes;
        }
    }());
}
String.prototype.upperFirst = function () {
    return (this.replace(/^(.)/g, function (c) { return c.toUpperCase(); }));
};
String.prototype.upperFirstAll = function () {
    return (this.replace(/^(.)|(\s|\-)(.)/g, function (c) { return c.toUpperCase(); }));
};
String.prototype.allSmall = function () {
    return (this.replace(/(.)/g, function (c) { return c.toLowerCase(); }));
};
String.prototype.toTitleCase = function () { return (this.replace(/\w\S*/g, function (c) { return c.charAt(0).toUpperCase() + c.substr(1).toLowerCase();; })); };
String.prototype.endsWith = function (a) {
    return this.substr(this.length - a.length) === a
};
String.prototype.startsWith = function (a) {
    return this.substr(0, a.length) === a
};
String.prototype.contains = function (a) {
    return this.indexOf(a) > -1
};
String.prototype.equals = function (a) {
    return this.toLowerCase() === a.toLowerCase();
};
String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "")
};
String.prototype.trimEnd = function () {
    return this.replace(/\s+$/, "")
};
String.prototype.trimStart = function () {
    return this.replace(/^\s+/, "")
};
String.format = function () {
    return String._toFormattedString(false, arguments)
};
String._toFormattedString = function (l, j) {
    var c = "",
        e = j[0];
    for (var a = 0; true;) {
        var f = e.indexOf("{", a),
            d = e.indexOf("}", a);
        if (f < 0 && d < 0) {
            c += e.slice(a);
            break
        }
        if (d > 0 && (d < f || f < 0)) {
            c += e.slice(a, d + 1);
            a = d + 2;
            continue
        }
        c += e.slice(a, f);
        a = f + 1;
        if (e.charAt(a) === "{") {
            c += "{";
            a++;
            continue
        }
        if (d < 0) break;
        var h = e.substring(a, d),
            g = h.indexOf(":"),
            k = parseInt(g < 0 ? h : h.substring(0, g), 10) + 1,
            i = g < 0 ? "" : h.substring(g + 1),
            b = j[k];
        if (typeof b === "undefined" || b === null) b = "";
        if (b.toFormattedString) c += b.toFormattedString(i);
        else if (l && b.localeFormat) c += b.localeFormat(i);
        else if (b.format) c += b.format(i);
        else c += b.toString();
        a = d + 1
    }
    return c
};
String.prototype.phoneFormatUK = function () {
    return this.replace(/\s+/g, '').replace(/(.)(\d{4})(\d)/, '+44 ($1)$2 $3');
}
Array.prototype.filter = function (fn, context) {
    var i,
        value,
        result = [],
        length;
    if (!this || typeof fn !== 'function' || (fn instanceof RegExp)) {
        throw new TypeError();
    }
    length = this.length;
    for (i = 0; i < length; i++) {
        if (this.hasOwnProperty(i)) {
            value = this[i];
            if (fn.call(context, value, i, this)) {
                result.push(value);
            }
        }
    }
    return result;
};
Array.prototype.indexOf = function (searchElement, fromIndex) {
    var i,
        pivot = (fromIndex) ? fromIndex : 0,
        length;
    if (!this) {
        throw new TypeError();
    }
    length = this.length;
    if (length === 0 || pivot >= length) {
        return -1;
    }
    if (pivot < 0) {
        pivot = length - Math.abs(pivot);
    }
    for (i = pivot; i < length; i++) {
        if (this[i] === searchElement) {
            return i;
        }
    }
    return -1;
};
Array.prototype.clean = function () {
    for (var i = 0; i < this.length; i++) {
        if (!this[i]) {
            this.splice(i, 1);
            i--;
        }
    }
    return this;
};
Array.prototype.unique = function () {
    var r = new Array();
    o: for (var i = 0, n = this.length; i < n; i++) {
        for (var x = 0, y = r.length; x < y; x++) {
            if (r[x] === this[i]) {
                continue o;
            }
        }
        r[r.length] = this[i];
    }
    return r;
};
/*global window, $*/
var Global = {
    MessageType: {
        Success: 0,
        Error: 1,
        Warning: 2,
        Info: 3
    }
};
Global.ViewContactRecord = function (gridElement, options) {
    $(document).on('click', '#' + gridElement + ' tbody tr.dtrowclass td', function () {
        var enquiryTable = $('#' + gridElement).dataTable();
        var aPos = enquiryTable.fnGetPosition(this);
        var aData = enquiryTable.fnGetData(aPos);
        if (options.indexOf($(this).index()) == -1) {
            window.open(WebDomain.webUrl + "ContactRecord/Index/" + aData[8], '_self');
        }
    });
};
Global.ViewContactRecordByMergeContact = function (gridElement, options) {
    $(document).on('click', '#' + gridElement + ' tbody tr.dtrowclass td', function () {
        var enquiryTable = $('#' + gridElement).dataTable();
        var aPos = enquiryTable.fnGetPosition(this);
        var aData = enquiryTable.fnGetData(aPos);
        if (options.indexOf($(this).index()) == -1) {
            window.open(WebDomain.webUrl + "ContactRecord/Index/" + aData[0], '_blank');
        }
    });
};
Global.slaTimer = function ($clsName) {
    $.fn.countDownTimer('clearAll', $clsName);
    $("." + $clsName).each(function () {
        $(this).countDownTimer();
    });
}
Global.ViewEnquiryOnRowClick = function (gridElement, options, target) {
    var _target = Global.IsNullOrEmptyString(target) ? '_self' : target;
    $(document).off('click', '#' + gridElement + ' tbody tr.dtrowclass td').on('click', '#' + gridElement + ' tbody tr.dtrowclass td', function () {
        var enquiryTable = $('#' + gridElement).dataTable();
        var aPos = enquiryTable.fnGetPosition(this);
        var aData = enquiryTable.fnGetData(aPos);
        if (options.indexOf($(this).index()) == -1) {
            window.open(WebDomain.webUrl + "Enquiry/ViewEnquiry/" + aData[0], _target);
        }
    });
};
Global.EnquiryReportingOnRowClick = function (gridElement, options) {
    $(document).on('click', '#' + gridElement + ' tbody tr.dtrowclass td', function () {
        var enquiryTable = $('#' + gridElement).dataTable();
        var aPos = enquiryTable.fnGetPosition(this);
        var aData = enquiryTable.fnGetData(aPos);
        if (options.indexOf($(this).index()) == -1 && aData[25] == 'False' && aData[40] == 'False') {
            window.open(WebDomain.webUrl + "Enquiry/ViewEnquiry/" + aData[0], '_blank');
        }
    });
};
Boolean.parse = function (b) {
    var a = Global.IsNotNull(b) ? b.toString().trim().toLowerCase() : false;
    if (a === "false") return false;
    if (a === "true") return true;
    return false;
};
Global.FormHelper = function (formElement, options, onSucccess, onError, loadingElementId, onComplete) {
   // debugger
    "use strict";
    //debugger
    var settings = {};
    settings = $.extend({}, settings, options);
    $.validator.unobtrusive.parse(formElement);
    if (settings.validateSettings !== null && settings.validateSettings !== undefined) {
        formElement.validate(settings.validateSettings);
    }

    formElement.submit(function (e) {
        e.stopImmediatePropagation();

        if (options && options.beforeSubmit) {
            if (!options.beforeSubmit()) {
                return false;
            }
        }

        var formdata = new FormData();
        formElement.find('input[type="file"]:not(:disabled)').each(function (i, elem) {
            if (elem.files && elem.files.length) {
                for (var i = 0; i < elem.files.length; i++) {
                    var file = elem.files[i];
                    formdata.append(elem.getAttribute('name'), file);
                }
            }
        });

        $.each(formElement.serializeArray(), function (i, item) {
            formdata.append(item.name, item.value);
        });

        if (options && options.updateFormData) {
            var updateformdata = options.updateFormData(formdata)
            if (updateformdata != null && updateformdata != undefined) {
                formdata = updateformdata;
            }
        }

        var submitBtn = formElement.find('.btn-primary');
        if (formElement.validate().valid() && formElement.valid()) {
            var $buttonI = submitBtn.find('i');
            submitBtn.attr({ "data-visible-class": submitBtn.attr("class") }, { "data-text": submitBtn.find("span").text() });
            //submitBtn.removeClass(submitBtn.attr("class"));
            submitBtn.addClass("spinning");
            //submitBtn.prop('disabled', true);
            submitBtn.attr('disabled', 'disabled');
            submitBtn.find('span').html('Submiting..');

            $.ajax(formElement.attr("action"), {
                type: "POST",
                data: formdata,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    if (settings.loadingElementId != null || settings.loadingElementId != undefined) {
                        $("#" + settings.loadingElementId).show();
                        submitBtn.hide();
                    }
                },
                success: function (result) {
                    if (onSucccess === null || onSucccess === undefined) {
                        if (result.isSuccess) {
                            window.location.href = result.redirectUrl;
                        } else {
                            if (settings.updateTargetId) {
                                var datatresult = (result.message == null || result.message == undefined) ? ((result.data == null || result.data == undefined) ? result : result.data) : result.message;
                                $("#" + settings.updateTargetId).html(datatresult);
                            }
                        }
                    } else {
                        onSucccess(result);
                    }
                },
                error: function (jqXHR, status, error) {
                    if (onError !== null && onError !== undefined) {
                        onError(jqXHR, status, error);
                        $("#loadingElement").hide();
                    }
                },
                complete: function (result) {
                    if (onComplete === null || onComplete === undefined) {
                        if (settings.loadingElementId != null || settings.loadingElementId != undefined) {
                            $("#" + settings.loadingElementId).hide();
                        }
                        submitBtn.removeClass("spinning");
                        submitBtn.addClass(submitBtn.attr("data-visible-class"));
                        submitBtn.find('span').text(submitBtn.attr("data-text"));
                        //submitBtn.prop('disabled', false);
                        submitBtn.removeAttr('disabled');
                    } else {
                        onComplete(result);
                    }
                }
            });
        }

        e.preventDefault();
    });
    return formElement;
};

Global.AjaxPost = function (asyncRequest, options, onSucccess, onError, onComplete) {
    "use strict";
    asyncRequest = asyncRequest != null && asyncRequest != undefined && asyncRequest == true;
    if (options && options.beforeSubmit) {
        if (!options.beforeSubmit()) {
            return false;
        }
    }
    var formdata = new FormData();
    if (options && options.updateFormData) {
        var updateformdata = options.updateFormData(formdata)
        if (updateformdata != null && updateformdata != undefined) {
            formdata = updateformdata;
        }
    }
    $.ajax(options.url, {
        type: "POST",
        data: formdata,
        contentType: false,
        processData: false,
        async: asyncRequest,
        success: function (result) {
            if (onSucccess === null || onSucccess === undefined) {
                if (result.isSuccess) {
                    window.location.href = result.redirectUrl;
                } else {
                    if (settings.updateTargetId) {
                        var datatresult = (result.message == null || result.message == undefined) ? ((result.data == null || result.data == undefined) ? result : result.data) : result.message;
                        $("#" + settings.updateTargetId).html(datatresult);
                    }
                }
            } else {
                onSucccess(result);
            }
        },
        error: function (jqXHR, status, error) {
            if (onError !== null && onError !== undefined) {
                onError(jqXHR, status, error);
                $("#loadingElement").hide();
            }
        }
    });

};


Global.GridHelper = function (gridElement, options) {
    //debugger
    if ($(gridElement).find("thead tr th").length > 1 || options.serverSide === true) {
        var settings = {};
        settings = $.extend({}, settings, options);
        return $(gridElement).DataTable(settings);
    }
};

Global.FormValidationReset = function (formElement, validateOption) {
    if ($(formElement).data('validator')) {
        $(formElement).data('validator', null);
    }

    $(formElement).validate(validateOption);

    return $(formElement);
};
Global.DateProcess = function process(date) {
    var parts = date.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}
Global.TimeProcess = function process(time) {
    var parts = time.split(":");
    return parts;
}
Date.prototype.isSameDateAs = function (pDate) {
    return (
        this.getFullYear() === pDate.getFullYear() &&
        this.getMonth() === pDate.getMonth() &&
        this.getDate() === pDate.getDate()
    );
}
$(document).off("keypress", "input[patter!='']").on("keypress", "input[patter!='']", function (event) {
    var pattern = $(this).attr("pattern");
    var regxPattern = new RegExp(pattern);
    return regxPattern.test($(this).val() + event.originalEvent.key);
});


$(document).off("click", ".dropdown-menu a.dropdown-toggle").on("click", ".dropdown-menu a.dropdown-toggle", function (event) {
    event.preventDefault();
    event.stopImmediatePropagation();
    if (!$(this).next().hasClass('show')) {
        $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    }
    var $subMenu = $(this).next(".dropdown-menu");
    $subMenu.toggleClass('show');
});

$(document).ready(function () {

    //Stop first letter space in textbox.
    $(".form-control").keypress(function (evt) {
        var caretPos = $(this)[0].selectionStart
        if (evt.keyCode == 32 && caretPos == 0) {
            return false;
        }
    });

    $(document).ajaxStart(function () {
        $("#loading").css("display", "block");
    });

    $(document).ajaxComplete(function () {
        $("#loading").css("display", "none");
    });
});

$(document).on("click", ".dropdown-menu", function (e) {
    $(this).parent().is(".open") && e.stopPropagation();
});
$("div[role=dialog].modal").on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var url = button.attr("href");
    if (url !== "") {
        var modal = $(this);
        // note that this will replace the content of modal-content everytime the modal is opened
        modal.find('.modal-content').load(url);
    }
}).on('hidden.bs.modal', function (e) {
    $(this).removeData('bs.modal');
    $(this).find(".modal-content").empty();
});


$(document).on('keypress', '.number', function (event) {
    if ((event.which < 48 || event.which > 57) && event.which != 8 && event.which != 0) {
        event.preventDefault();
    }
});

$(document).on('keypress', '.decimal', function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57) && event.which != 8 && event.which != 0) {
        event.preventDefault();
    }
});
$(document).on('keypress', '.decimalNegative', function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which != 45 || $(this).val().indexOf('-') != -1) && (event.which < 48 || event.which > 57) && event.which != 8 && event.which != 0) {
        event.preventDefault();
    }
});
$(document).on('change', '.number, .decimal, .decimalNegative', function () {
    if ($(this).val() != "0" && $(this).val() != "" && !$.isNumeric($(this).val())) {
        $(this).val("");
        alert("Please enter a valid value");
        return false;
    }
});
$(document).on('focus', '.number, .decimal, .decimalNegative', function (event) {
    var default_value = 0;
    if ($(this).val() == default_value) $(this).val("");
});
$(document).on('blur', '.number, .decimal, .decimalNegative', function (event) {
    var default_value = 0;
    if ($(this).val().length == 0) $(this).val(default_value);
});
$(document).on('click', '.fa-calendar', function (event) {
    $(this).closest('div').find('input').focus();
});
$(document).on('change blur', 'input.allsmall', function () {
    $(this).val($(this).val().allSmall());
});
$(document).on('change keyup input blur', 'input.upperFirstAll', function () {
    var val = $(this).val();
    $(this).val(val.upperFirstAll());
});
Global.IsString = function (o) {
    return typeof o === "string";
};
Global.IsNull = function (o) { return typeof o === "undefined" || typeof o === "unknown" || o == null };
Global.IsNotNull = function (o) { return !Global.IsNull(o); };
Global.IsNullOrEmptyString = function (str) {
    return Global.IsNull(str) || typeof str === "string" && $.trim(str).length == 0
};
Global.IsNotNullOrEmptyString = function (str) { return !Global.IsNullOrEmptyString(str); };
Global.IsNotNullResult = function (results) { return Global.IsNotNull(results) && results.length > 0; };
Global.StringBuilder = function (a) {
    this._parts = typeof a !== "undefined" && a !== null && a !== "" ? [a.toString()] : []; this._value = {}; this._len = 0
};
Global.StringBuilder.prototype = {
    append: function (a) { this._parts[this._parts.length] = a },
    appendLine: function (a) { this._parts[this._parts.length] = typeof a === "undefined" || a === null || a === "" ? "\r\n" : a + "\r\n" },
    clear: function () { this._parts = []; this._value = {}; this._len = 0 },
    isEmpty: function () { if (this._parts.length === 0) return true; return this.toString() === "" },
    toString: function (a) {
        a = a || ""; var b = this._parts; if (this._len !== b.length) {
            this._value = {}; this._len = b.length
        } var d = this._value;
        if (typeof d[a] === "undefined") {
            if (a !== "")
                for (var c = 0; c < b.length;)
                    if (typeof b[c] === "undefined" || b[c] === "" || b[c] === null)
                        b.splice(c, 1); else c++; d[a] = this._parts.join(a)
        }
        return d[a];
    }
};
Global.padNumber = function (number, iLength) {
    var iNumber = number;
    var PAD_CHAR = "0", DEFAULT_LENGTH = 2;
    if (iLength == null)
        iLength = DEFAULT_LENGTH;
    for (var sNumber = new String(iNumber), oBuilder = new Global.StringBuilder, i = 0; i < iLength - sNumber.length; i++)
        oBuilder.append(PAD_CHAR);
    oBuilder.append(sNumber);
    return oBuilder.toString();
};

jQuery.fn.preventDoubleSubmission = function () {
    $(this).on('submit', function (e) {
        var $form = $(this);
        if ($form.data('submitted') === true) {
            // Previously submitted - don't submit again
            e.preventDefault();
        } else {
            // Mark it so that the next submit can be ignored
            $form.data('submitted', true);
        }
    });
    // Keep chainability
    return this;
};
Global.GetRandomNumber = function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}
Global.StartSubmitProgress = function (submitBtn) {
    submitBtn.attr('disabled', 'disabled');
    submitBtn.find('i').html("<img src='../image/loader.gif'>");
}
Global.StopSubmitProgress = function (submitBtn) {
    submitBtn.removeAttr('disabled');
    submitBtn.find('i').html("");
}
Global.getURLParam = function (key, target) {
    var values = [];
    if (!target) {
        target = location.href;
    }
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var pattern = "[?&]" + key + '=([^&#]+)';
    var o_reg = new RegExp(pattern, 'ig');
    while (true) {
        var matches = o_reg.exec(target);
        if (matches && matches[1]) {
            values.push(matches[1]);
        }
        else {
            break;
        }
    }
    if (!values.length) {
        return null;
    }
    else {
        return values;
    }
}
Global.showloader = function () {
    $(".loader-outer").css('display', 'block');
}
Global.hideloader = function () {
    $(".loader-outer").css('display', 'none');
}

Global.IsWithin4weeks = function (paidOnDate, duedate) {
    if (paidOnDate != '' && duedate != '') {
        var arrPaidOnDate = paidOnDate.split(' ')[0].split('/');
        var arrDeptDate = duedate.split('/');

        var newdeptDate = new Date(arrDeptDate[1] + '/' + arrDeptDate[0] + '/' + arrDeptDate[2]);
        var newPaidOndate = new Date(arrPaidOnDate[1] + '/' + arrPaidOnDate[0] + '/' + arrPaidOnDate[2]);

        var diff = newdeptDate - newPaidOndate;

        var days = Math.round(diff / 1000 / 60 / 60 / 24);

        if (days <= 28) {
            return true;
        } else { return false; }

    }
}
Global.equalheight = function (container) {
    var currentTallest = 0,
        currentRowStart = 0,
        rowDivs = new Array(),
        $el,
        topPosition = 0;
    $(container).each(function () {
        $el = $(this);
        $($el).height('auto')
        topPostion = $el.position().top;
        if (currentRowStart != topPostion) {
            for (currentDiv = 0; currentDiv < rowDivs.length; currentDiv++) {
                rowDivs[currentDiv].height(currentTallest);
            }
            rowDivs.length = 0; // empty the array
            currentRowStart = topPostion;
            currentTallest = $el.height();
            rowDivs.push($el);
        } else {
            rowDivs.push($el);
            currentTallest = (currentTallest < $el.height()) ? ($el.height()) : (currentTallest);
        }
        for (currentDiv = 0; currentDiv < rowDivs.length; currentDiv++) {
            rowDivs[currentDiv].height(currentTallest);
        }
    });
}
Global.setFocusOnError = function () {
    $('.validationMessage,.field-validation-error:visible:eq(0)').prev(':eq(0)').focus();
}
Global.setFocusOnErrorForAlert = function (index, itm) {
    if (Global.IsNotNull(index) && !Global.IsNullOrEmptyString(itm)) {
        $('#' + String.format('{0}{1}', index, itm) + ', [name="[' + index + '].' + itm + '"]' + ', [name="' + itm + '"]').focus();
    }
}
Global.movePageOnControlForAlert = function (index, item) {
    if (!Global.IsNullOrEmptyString(item)) {
        $('html, body').animate({
            scrollTop: $('#' + String.format('{0}{1}', index, item) + ', [name="[' + index + '].' + item + '"]' + ', [name="' + item + '"]').offset().top - 65/*$('[name="' + itm + '"]')*/
        }, 500);
    }
}
Global.movePageOnControlForError = function () {
    $('html, body').animate({
        scrollTop: $('.validationMessage:visible:eq(0)').prev(':eq(0)').offset().top - 65
    }, 500);
}
Global.updateServerTimeIntervel = function () {
    if (Global.currentServerTimeIntervel != null) {
        clearInterval(Global.currentServerTimeIntervel);
    }
    Global.currentServerTimeIntervel = setInterval(function () {
        Global.currentServerTime = Global.currentServerTime.add(1, 'second');;
    }, 1000);
}
Global.getServerTime = function () {
    if (Global.serverTime == null) {
        $.ajax({
            type: "GET",
            url: WebDomain.webUrl + 'home/getservertime/',
            data: null,
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (result) {
                console.log(result);
                Global.serverTime = moment(result, "DD/MM/YYYY HH:mm:ss");
            },
            error: function (jqxhr, status, error) {
                if (jqxhr.responseText.trim().length > 0 || error.message.trim().length > 0) {
                    alertify.alert("Alert!", (jqxhr.responseText != "" ? jqxhr.responseText : error.message));
                }
            }
        });
        if (Global.serverTimeIntervel != null) {
            clearInterval(Global.serverTimeIntervel);
        }
        Global.serverTimeIntervel = setInterval(function () {
            Global.serverTime = (Global.serverTime.add(1, 'second'));
        }, 1000);
    }
    return Global.serverTime;
}
Global.updateServerTime = function (requestServerTime) {
    Global.currentServerTime = moment(requestServerTime, Global.dafaultDatetimeFormat);
}
function recursiveIteration(object) {
    for (var property in object) {
        console.log(property);
        if (object.hasOwnProperty(property)) {
            if (typeof object[property] == "children") {
                recursiveIteration(object[property]);
            } else {
                //found a property which is not an object, check for your conditions here
                //jQuery("#out").append("found prop: " + object[property] + "<br/>");
            }
        }
    }
}

Global.select2Sorter = function (results) {
    var query = $('.select2-search__field').val().toLowerCase();
    return results.sort(function (a, b) {
        return a.text.toLowerCase().indexOf(query) -
            b.text.toLowerCase().indexOf(query);
    });
}

Global.select2OptionGroupSorter = function (results) {
    var query = $('.select2-search__field').val().toLowerCase();
    var newResult = [], newchildOptionsArray = [];

    $.each(results, function (indexInArray, objValue) {
        var first = [], others = [], newlist = [];
        if (objValue.children != undefined) {
            var childarray = objValue.children;
            $.each(childarray, function (childIndex, childValue) {
                if (childValue.text.toLowerCase().indexOf(query) == 0) {
                    first.push(childValue);
                } else {
                    others.push(childValue);
                }
            });
            first.sort(function (a, b) {
                return textsort(a, b);
            });
            others.sort(function (a, b) {
                return textsort(a, b);
            });
            newlist = $.merge(first, others)
            newchildOptionsArray = newlist;
        }

        objValue.children = newchildOptionsArray;
        newResult.push(objValue);
    });
    return newResult;
}

function textsort(a, b) {
    return ((a.text < b.text) ? -1 :
        ((a.text > b.text) ? 1 : 0));
}

Global.newtextsort = function (a, b) {
    return ((a.text < b.text) ? -1 :
        ((a.text > b.text) ? 1 : 0));
}

Global.select2Focus = function () {
    var select2_open;
    // open select2 dropdown on focus
    $(document).on('focus', '.select2Focus .select2-selection--single', function (e) {
        select2_open = $(this).parent().parent().siblings('select');
        select2_open.select2('open');
    });
}

Global.bceMatchTitle = "INFO: This Contact&#39;s booking confirmation email matches with other contact&#39;s primary email or booking confirmation email.";
Global.PageRedirectWithTimeOut = function (url) {
    if (!Global.IsNullOrEmptyString(url)) {
        setTimeout(function () {
            window.location.href = url;
        }, 1000);
    }
}
Global.setCookie = function (key, value) {

    var expires = new Date();
    expires.setTime(expires.getTime() + (1 * 24 * 60 * 60 * 1000));
    document.cookie = key + '=' + value + ';expires=' + expires.toUTCString() + ';domain=' + WebDomain.CookieDomain + ';path=/';
}

Global.getCookie = function (key) {
    var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
    return keyValue ? keyValue[2] : null;
}

Global.confirmCustomerType = function (enquiryId) {
    var id = $(this).attr("data-enquiryId");
    var $linCustomerType = $(this);
    Global.Confirm("Confirmation!", "The customer type for this enquiry has not been added yet. Please add customer type first.", function () {

        $("<a/>", {
            href: WebDomain.webUrl + "EnquiryOperation/AddCustomerType?enquiryid=" + enquiryId,
            'data-toggle': "modal",
            'data-target': "#modal-customertype",
            "style": "display:none"
        }).appendTo("body").trigger("click").remove();

    }).set('labels', {
        ok: 'Yes', cancel: 'No'
    });
}
$(document).ready(function () {
    $(document).on("blur", "input.validate-url", function () {
        var pattern = /^(http|https)?:\/\/[a-zA-Z0-9-\.]+\.[a-z]{2,4}/;
        if ($(this).val() != '' && pattern.test($(this).val()) == false) {
            $(this).val("");
            $(this).focus();
            Global.ShowMessage("Please enter valid url", Global.MessageType.Error);
            return false;
        }
    });

});

$(document).keydown(function (evt) {
    if (evt.which == 17) { // ctrl
        Global.ctrlPressed = true;
    }
}).keyup(function (evt) {
    if (evt.which == 17) { // ctrl
        Global.ctrlPressed = false;
    }
});
$(document).mousedown(function (e) {
    //1 for left Click
    //3 for right click
    switch (e.which) {
        case 2:
            //middle Click
            Global.isMouseMiddleClicked = true;
            break;
    }
    return true;// to allow the browser to know that we handled it.
}).mouseup(function () {
    Global.isMouseMiddleClicked = false;
});
Global.bindRowClick = function (gridElement, redirectURL, options) {
    $(document).on('click', '#' + gridElement + ' tbody tr.dtrowclass td', function () {
        var enquiryTable = $('#' + gridElement).dataTable();
        var aPos = enquiryTable.fnGetPosition(this);
        var aData = enquiryTable.fnGetData(aPos);
        if (options.indexOf($(this).index()) == -1) {
            if (Global.ctrlPressed || Global.isMouseMiddleClicked) {
                window.open(redirectURL + aData[0], '_blank');
            }
            else {
                window.location = redirectURL + aData[0];
            }
        }
    });
};

Global.CorporateContactOnRowClick = function (gridElement, redirectURL, options) {
    $(document).on('click', '#' + gridElement + ' tbody tr.dtrowclass td', function () {
        var enquiryTable = $('#' + gridElement).dataTable();
        var aPos = enquiryTable.fnGetPosition(this);
        var aData = enquiryTable.fnGetData(aPos);
        if (options.indexOf($(this).index()) == -1) {
            window.open(redirectURL + aData[0], '_blank');
        }
    });
};
/* Get a cookie */
Global.GetCookie = function (c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) {
                c_end = document.cookie.length;
            }
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}