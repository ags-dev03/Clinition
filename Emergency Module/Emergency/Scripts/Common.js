$(function () {
    $('.balpha').alphanumeric({ allow: '-_/' });
    $('.alphanumeric').alphanumeric({ allow: ' .-_&()@*/,' });
    $('.numeric').numeric({ allow: '.' });
    $('.int').numeric({});

    $('.decimal').on('input', function () {
        this.value = this.value.match(/^\d+\.?\d{0,2}/);
    });

    $(document).on('input', '.decimal', function () {
        this.value = this.value.match(/^\d+\.?\d{0,2}/);
    });


});




function maxDate(year) {
    var date = new Date();
    var dd = date.getDate();
    var mm = date.getMonth() + 1;
    var yyyy = date.getFullYear() - parseInt(year);
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    date = yyyy + '-' + mm + '-' + dd;
    // date = dd + '-' + mm + '-' + yyyy;

    return date;
}
//--------------------------------Multi Select -------------




function UiAutoComplete(Id, Function, Data) {
    var xhr2 = new XMLHttpRequest();
    var cache = {};
    Id.autocomplete({
        delay: 500,
        minLength: 1,
        source: function (req, response) {
            var term = req.term;
            if (term in cache) {
                response(cache[term]);
                return;
            }
            xhr2.abort();
            xhr2 = $.ajax({
                url: '/Classes/Services.asmx/' + Function,
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: "{'item': '" + term + "'," + Data + "}",
                success: function (data, status, xhr) {
                    var obj = eval("(" + data.d + ")");
                    if (isEmptyObj(obj) == false) {
                        cache[term] = obj.Table1;
                        if (term.substr(0, 1) != '%') {
                            var re = $.ui.autocomplete.escapeRegex(term);
                            var matcher = new RegExp("^" + re, "i");
                            response($.grep(obj.Table1, function (item) {
                                return matcher.test(item.label);
                            }));
                        }
                        else {
                            response(obj);
                        }
                    }
                    else {
                        Id.val('');
                    }
                    //$("#divProgress").hide();
                },
                error: function () {
                    // $("#divProgress").hide();
                }
            });
        },
        focus: function (event, ui) {
            // prevent autocomplete from updating the textbox
            event.preventDefault();
        },
        select: function (event, ui) {
            Id.val(ui.item.label);
            window.location = ui.item.value;
        }
    }).data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>" + item.label + "</li>").appendTo(ul);
    };
}




