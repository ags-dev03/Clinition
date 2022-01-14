$(function () {
    var MenuObj = eval("(" + $('#hdn_Menus').val() + ")");
    var html = '';
    var dash = '<i class="fas fa-tachometer-alt"></i>';
    var other = '<i class="fas fa-rocket"></i>';
    var active = 'active-page';

    var module = find_in_object(MenuObj.Table1, { menutitle: 'Emergency' });
    var Menus = find_in_object(MenuObj.Table1, { parentmenuid: module[0].menuid });
  
    html += '<li class="nav-item dropdown"><a class="nav-link nav-url" data-toggle="tooltip" data-placement="top" title="Dashboard " href = "javascript:void(0);" url = "' + module[0].mnum_redirectto + '" id="dashboardsDropdown" role = "button" aria-haspopup="true" aria-expanded="false" ><i class="' + module[0].mnum_image + '"></i> Dashboard </a><label class="d-none">Home</label></li>';
    $.each(Menus, function (module, moduleVal) {
        var icon;
        var activeClass;
      
        html += '<li class="nav-item dropdown" ><a class="nav-link dropdown-toggle nav-url"  data-toggle="tooltip" data-placement="top" title="' + moduleVal.menutitle + '"  href = "javascript:void(0);" id="dashboardsDropdown" role = "button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ><i class="' + moduleVal.mnum_image + '"></i>' + moduleVal.menutitle + '</a><ul class="dropdown-menu" aria-labelledby="dashboardsDropdown">';

        var mainMenu = find_in_object(MenuObj.Table1, { parentmenuid: moduleVal.menuid });
        $.each(mainMenu, function (menu, menuVal) {
            if (menuVal.mnum_redirectto == '') {
                html += '<li><a class="dropdown-toggle sub-nav-link" href="javascript:void(0);" id="TravelDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' + menuVal.menutitle + '</a><ul class="dropdown-menu dropdown-menu-right show" aria-labelledby="TravelDropdown">';

                var Submenu = find_in_object(MenuObj.Table1, { parentmenuid: menuVal.menuid });
                
                $.each(Submenu, function (subMenu, subMenuVal) {
                    html += '<li><a class="dropdown-item nav-url" href = "javascript:void(0);"  url="' + subMenuVal.mnum_redirectto + '">' + subMenuVal.menutitle + '</a><label class="d-none">' + subMenuVal.parentmenuid + '</label></li>';
                });
                html += '</ul></li>';
            }

            else {
                html += '<li><a class="dropdown-item nav-url" href = "javascript:void(0);"  url="' + menuVal.mnum_redirectto + '">' + menuVal.menutitle + '</a><label class="d-none">' + menuVal.parentmenuid + '</label></li>';
            }
          
        });
        html += '</ul></li>';
    });
    $('#Ul_menu').html(html);
    $('.moduleName').text(module[0].menutitle);
    var currentMenu = find_in_object(MenuObj.Table1, { mnum_redirectto: $('#hdn_currenturl').val() });
    if (currentMenu.length > 0) {
        $('.pageHeadName').text(currentMenu[0].menutitle);
    }



    CreateTable($('#div_tbl'), eval("(" + $('#hdn_Menus').val() + ")"));

    UiAutoComplete($('#txt_search'), 'GetMenuBySearch', "'userRole':'" + $('#hdn_Type').val() + "'");

    $('#A_logOut').on('click', function () {
        window.location = "/Default.aspx";
    });
    $('#hdn_usrID').val(fn_getCookie('MedID'));

    $('.balpha').alphanumeric({ allow: '-_/' });
    $('.alphanumeric').alphanumeric({ allow: ' .-_&()@*/,' });
    $('.numeric').numeric({ allow: '.' });
    $('.int').numeric({});

    $('.decimal').on('input', function () {
        this.value = this.value.match(/^\d+\.?\d{0,2}/);
    });

    //----------------------------------------------------------------
    $('.decimalr').keypress(function (evt) {

        var maxlen = $('.decimalr').attr('maxlength');
        var len = $('.decimalr').val().length;
        var index = $('.decimalr').val().indexOf('.');
        var CharAfterdot = (len) - index;
        var CharBeforeDot = (len) + index;
        var unicode = evt.charCode ? evt.charCode : evt.keyCode;

        if ($('.decimalr').val().indexOf(".") != -1)

            if (unicode == 46)
                return false;
        if (unicode != 8) {

            if (unicode > 31 && (unicode < 48 || unicode > 57) && unicode != 46)
                return false;

            if (index > -1) {
                var CharAfterdot = (len) - index;
                if (CharAfterdot > 2) {
                    return false;
                }
            }

            if (index == -1 && CharAfterdot > (maxlen - 3) && CharBeforeDot > (maxlen - 5)) {
                if (unicode != 46) {

                    return false;
                }
            }
            if (($('.decimalr').val() == '0') && unicode != 46) {
                return false;
            }
        }
    });
    //----------------------------------------------------------------



  //  var urlCount = 0;
    //$('.nav-item a').each(function (row, data) {
    //    //console.log($(this).prop('href'), $('#hdn_currenturl').val());
    //    if ($(this).prop('href') == $('#hdn_currenturl').val()) {
    //        $(this).closest('.nav-item').find('a:eq(0)').addClass('active-page');
    //        $('#div_title').text($(this).next().text());
    //        urlCount++;
    //    }
    //});
    //if (urlCount == 0) {
    //     window.location = "/Default.aspx";
    //}
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
