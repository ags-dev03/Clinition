(function ($) {

    $.fn.alphanumeric = function (p) {

        p = $.extend({
            ichars: "!@#$%^&*()+=[]\\\';,/{}|\":<>?~`.- _",
            nchars: "",
            allow: "",
            dv: 0,
            onlyInt: 0
        }, p);

        return this.each
			(
				function () {
				    var $this = $(this);
				    if (p.nocaps) p.nchars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				    if (p.allcaps) p.nchars += "abcdefghijklmnopqrstuvwxyz";

				    $this.keypress
						(
							function (e) {

							    var rc = new RegExp('(\\\\){1}[^\\w]{1}(\\|){1}');
							    if (!(p.allow.toString() + '|').match(rc) && p.allow.trim() != '') {
							        s = p.allow.split('');
							        for (i = 0; i < s.length; i++) if (p.ichars.indexOf(s[i]) != -1) s[i] = "\\" + s[i];
							        p.allow = s.join('|');
							        console.log('i');
							    }

							    var reg = new RegExp(p.allow, 'gi');
							    var ch = p.ichars + p.nchars;
							    ch = ch.replace(reg, '');

							    if (!e.charCode) k = String.fromCharCode(e.which);
							    else k = String.fromCharCode(e.charCode);

							    if (ch.indexOf(k) != -1) e.preventDefault();
							    if (e.ctrlKey && k == 'v') e.preventDefault();

							}

						);

				    $this.bind('contextmenu', function () { return false });

				}
			);

    };


    $.fn.checkDecimal = function (p) {

        p = $.extend({
            ichars: "!@#$%^&*()+=[]\\\';,/{}|\":<>?~`- _abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
            nchars: "",
            allow: ""
        }, p);

        return this.each
			(
				function () {
				    var $this = $(this);

				    $this.keypress
						(
							function (e) {

							    var code = (e.keyCode ? e.keyCode : e.which);
							    console.log(code);
							    if (code != 9 && code!=8 && code!=46 && code!=37 && code != 116) {
							        console.log('inder');
							        var reg = new RegExp(p.allow, 'gi');
							        var ch = p.ichars + p.nchars;

							        ch = ch.replace(reg, '');
							        if (!e.charCode) k = String.fromCharCode(e.which);
							        else k = String.fromCharCode(e.charCode);

							        if (ch.indexOf(k) != -1) e.preventDefault();
							        if (e.ctrlKey && k == 'v') e.preventDefault();

							        var $me = $(this);
							        var rc = new RegExp("^[-+]?([0-9]*\.[0-9]+|[0-9]+)$");
							        var k;
							        if (!e.charCode) k = String.fromCharCode(e.which);
							        else k = String.fromCharCode(e.charCode);
							        console.log($me.val() + k + '0' + '::: ' + rc);
							        if (!($me.val() + k + '0').match(rc)) {
							            console.log('sdf');
							            e.preventDefault();
							        }
							    }
							}

						);

				    $this.bind('contextmenu', function () { return false });

				}
			);

    };


    $.fn.numeric = function (p) {

        var az = "abcdefghijklmnopqrstuvwxyz";
        az += az.toUpperCase();

        p = $.extend({
            nchars: az,
            dv: 0,
            onlyInt: 0
        }, p);

        return this.each(function () {
            if (p.onlyInt == 1) {
                if (isNaN($(this).val()) || $(this).val() == '') {
                    $(this).val(p.dv);
                }
                $(this).blur(
							function () {
							    console.log('inder');
							    if (isNaN($(this).val()) || $(this).val() == '') {
							        $(this).val(p.dv);
							    }
							});
            }
            $(this).alphanumeric(p);
        }
		);

    };



    $.fn.alpha = function (p) {

        var nm = "1234567890";

        p = $.extend({
            nchars: nm
        }, p);

        return this.each(function () {
            $(this).alphanumeric(p);
        }
		);

    };

})(jQuery);
