function getData(url, data) {
    
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (json) {
                var obj = eval("(" + json.d + ")");
                GS_DataCompiler(obj, false);
                $('.getemp').remove();
                $('#page').children("*").css('font-size', 'small');
                $('#header').children("*").css('font-size', 'small');
                $('#footer').children("*").css('font-size', 'small');
                $('#hdnfld_Page').val($('#page').html());
                
                fn_getEleobj('hdnfld_Header').val($('#header').html());
                fn_getEleobj('hdnfld_Footer').val($('#footer').html());
                fn_getEleobj('hdnfld_OPH').val($('#OPH').html());
                fn_getEleobj('hdnfld_LPF').val($('#LPF').html());
                fn_getEleobj('hdnfld_HeaderHeight').val($('#header').height() );
                fn_getEleobj('hdnfld_FooterHeight').val($('#footer').height()-15 );
                fn_getEleobj('hdnfld_OPHHeight').val($('#OPH').height());
                fn_getEleobj('hdnfld_LPFHeight').val($('#LPF').height() + 10);
                $('#btn_createPDf').click();
            },
            error: function () { }
        });
}

function GS_DataCompiler(obj, isRec) {
    var colIndex = 0;
    $.each(obj, function (tblName, rowObj) {
        //tblName will contain the name of Data Table..
        //arr will contain the Data Array..
        var $table = $('.' + tblName);
        var nextRow = '<tr>' + $table.find('tr:last').html() + '</tr>';
        var $childTbl;
        $childTbl = $('.c' + tblName).clone();
        
        $.each(rowObj, function (pos, colObj) {
            colIndex = 0;
            //To Modify the abnormal Table

            $.each(colObj, function (key, val) {
                colIndex++;
                $table.find('span.GS_Value').filter(function () {
                    if ($.trim($(this).text()).toLowerCase() == '[' + key.toLowerCase() + ']') {
                        if (colObj.IsChild != null && colObj.IsChild == 'True' && colIndex != 0 && $(this).closest('td').hasClass('GS_TestChild') == true) {
                            val = '&nbsp;&nbsp;&nbsp;&nbsp;' + val;
                            $(this).closest('span').removeClass('GS_Value');
                        }
                        else if (colObj.IsChild != null && colObj.IsChild == 'True' && colIndex != 0 && $(this).closest('td').hasClass('GS_NTestChild') == false) {
                            val = '&nbsp;&nbsp;&nbsp;&nbsp;' + val;
                            $(this).closest('span').removeClass('GS_Value');
                        }
                        if ($.trim($(this).text()) == '[GS_PaymentWords]') {
                            $(this).html(translate(val));
                            $(this).closest('span').removeClass('GS_Value');
                        }
                        else if ($table.hasClass('abnormal') == false || key.toLowerCase() != 'value') {
                            $(this).html(val);
                            $(this).closest('span').removeClass('GS_Value');
                        } else if ($table.hasClass('abnormal') && key.toLowerCase() == 'value' && colObj.marking != '') {
                            $(this).html('<strong>' + val + '</strong>');
                            $(this).closest('span').removeClass('GS_Value');
                        } else { $(this).html(val); $(this).closest('span').removeClass('GS_Value'); }

                    } else if ($(this).find('img').length > 0) {
                        var mySrc = $(this).find('img').attr('alt');
                        if (key.toLowerCase() == mySrc.toLowerCase()) {
                            $(this).find('img').attr('src', val);
                        }
                    }
                    //chage by Lal Singh for OPD Emergency Report in remarks work
                     if($(this).find('.PtmastEntryRemark').length > 0){
                        var td_value = $(this).html();                                                    
                        $(this).closest('tr').html('').append(td_value);
                    }

                });
                // Remove the Non Zero Elements..
                if ($table.hasClass('nonzero')) {
                    $table.find('td').filter(function () {
                        //chage by Lal Singh Convort parseInt to parseFloat
                        if (parseFloat($(this).text().trim()) == 0) { 
                            $(this).closest('tr').remove();
                        }
                    });
                }
            });
            // To Create One Depth Reporting..
            GS_ChildBinder($childTbl, $table, obj, colObj);
            $table.append(nextRow);
        });
        $table.find('tr:last').remove();
        //$('.' + tblName + ':last').remove();
    });
    
    $('img').each(function () {
        var $me = $(this);
        
        //alter By Lal Singh For Local Host ID find or not condition 02/01/2013
        if($me.attr('src') == "")
        {
            $me.remove();
        }
        else{
            var src = $me.attr('src');
            if(src.match("http://"))
                $me.attr('src',$me.attr('src'));
            else
                $me.attr('src','http://'+fnGetDomain(document.URL)+$me.attr('src'));
            var imgPath = $me.attr('src');
            //End Code by Lal Singh
            
            $me.remove();
            $.ajax({
                url: imgPath,
                //async: false,
                type: 'HEAD',
                error: function (err,param1,param2,param3,param4) {
                    //file does not exist remove this
                    if ($me.closest('td').length > 0) {
                          //$me.closest('table').remove();
                          //alter By Lal Singh For img tag src not found and other error generating 13/01/2013
                          $me.remove();
                    }
                    else{
                          //$me.parent().remove();
                          //alter By Lal Singh For img tag src not found and other error generating 13/01/2013
                          $me.remove();
                    }
                },
                success: function () {
                    //file exists do something here
                }
            });
        }
    });
    $('span.GS_Value').each(function () {
        if ($(this).html().match(/\[.*\]/)) {
            if ($(this).closest('p').length > 0)
                $(this).closest('p').remove();
            else
                $(this).remove();
        }
    });
    /* Created for Print PDF in Hindi 16/11/2014 by Lal Singh */
    $('span.Hindi').each(function () {
        if ($(this).html().match(/(<p[^>]*>.*?<\/p>)/)) {
           $(this).find("p").css("font-family","KrutiDev010");
        }
    });
/* End Code for Print PDF in Hindi 16/11/2014 by Lal Singh */
    
}

function fnGetDomain(url) {
    return (url.match(/:\/\/(.[^/]+)/)[1]).replace('www.','');
}

function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)')
                    .exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}

function gs_Linq_Sort(arr, id) {
    var sample = JSLINQ(arr).Where(function (item) {
        return item.IDC == id;
    });
    return sample.items;
}

function GS_ChildBinder($childTbl, $table, obj, colObj) {
    if ($childTbl.length > 0) {
        var rowParentID = colObj.IDP;
        var pColSpan = $table.find('tr:eq(0)').find('td').length;
        $.each($childTbl, function (idx) {
            var ID = makeid();
            var $me = $childTbl.eq(idx);
            var oldID = $me.attr('id');
            var childArr = gs_Linq_Sort(obj[oldID], rowParentID);
            if (childArr.length > 0) {
                var $element = $($($me.outerHTML()).filter('#' + oldID)[0]);
                $element.removeClass().attr('id', '').addClass(ID);
                $table.append('<tr><td colspan="' + pColSpan + '">' + $element.outerHTML() + '</td></tr>');
                var newarr = '{"' + ID + '":' + JSON.stringify(childArr) + '}';
                GS_DataCompiler(eval("(" + newarr + ")"), true);
            }
        });
    }
}

function GS_AbnormalBinding(obj, tblID, idx) {
    var abnormalArr = obj[tblID][idx];
    if (abnormalArr.marking == 'H') {

    }
}

function fn_getEleobj(id, context) {
    var el = $("#" + id, context);
    if (el.length < 1)
        el = $("[id$=_" + id + "]", context);
    return el;
}
// ptimg for Patient Image, bar for bar code, digi for digital sign
