
/* Refrence http://abhisanoujam.blogspot.com/2009/07/having-fun-with-jquery-numbers-to-words.html  */


function NumberToWords() {

    var units = ["", "One", "Two", "Three", "Four", "Five", "Six",
    "Seven", "Eight", "Nine", "Ten"];
    var teens = ["Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
    "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty"];
    var tens = ["", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty",
    "Seventy", "Eighty", "Ninety"];

    var othersIndian = ["Thousand", "Lakh", "Crore"];

    var INDIAN_MODE = "indian";
    var currentMode = INDIAN_MODE;

    var getBelowHundred = function (n) {
        if (n >= 100) {
            return "greater than or equal to 100";
        };
        if (n <= 10) {
            return units[n];
        };
        if (n <= 20) {
            return teens[n - 10 - 1];
        };
        var unit = Math.floor(n % 10);
        n /= 10;
        var ten = Math.floor(n % 10);
        var tenWord = (ten > 0 ? (tens[ten] + " ") : '');
        var unitWord = (unit > 0 ? units[unit] : '');
        return tenWord + unitWord;
    };

    var getBelowThousand = function (n) {
        if (n >= 1000) {
            return "greater than or equal to 1000";
        };
        var word = getBelowHundred(Math.floor(n % 100));

        n = Math.floor(n / 100);
        var hun = Math.floor(n % 10);
        word = (hun > 0 ? (units[hun] + " Hundred ") : '') + word;

        return word;
    };

    return {
        numberToWords: function (n) {
            if (isNaN(n)) {
                return "Not a number";
            };

            var word = '';
            var val;

            val = Math.floor(n % 1000);
            n = Math.floor(n / 1000);

            word = getBelowThousand(val);

            if (this.currentMode == INDIAN_MODE) {
                othersArr = othersIndian;
                divisor = 100;
                func = getBelowHundred;
            } else {
                throw "Invalid mode - '" + this.currentMode
       + "'. Supported modes: " + INDIAN_MODE;
            };

            var i = 0;
            while (n > 0) {
                if (i == othersArr.length - 1) {
                    word = this.numberToWords(n) + " " + othersArr[i] + " "
        + word;
                    break;
                };
                val = Math.floor(n % divisor);
                n = Math.floor(n / divisor);
                if (val != 0) {
                    word = func(val) + " " + othersArr[i] + " " + word;
                };
                i++;
            };
            return word;
        },
        setMode: function (mode) {
            if (mode != INDIAN_MODE) {
                throw "Invalid mode specified - '" + mode
       + "'. Supported modes: " + INDIAN_MODE;
            };
            this.currentMode = mode;
        }
    }
}

function clear() {
    $("#errSpan").hide();
    $("#resultDiv").hide();
}

var num2words = new NumberToWords();

function translate(inpt) {
    clear();
    var input = inpt;
    if (isNaN(input)) {
        $("#errSpan").html("This is not a number - " + input);
        $("#errSpan").show();
        $("#input").focus();
        return;
    };

    num2words.setMode("indian");
    var myArr = input.split('.');
    var indian_BeforeDot = num2words.numberToWords(myArr[0]);
    var retVal;
    if (myArr.length > 1) {
        if ((myArr[1] != null && parseInt(myArr[1]) != 0) && (myArr[0] == '' || parseInt(myArr[0]) == 0)) {
            var indian_AfterDot = num2words.numberToWords(myArr[1]);
            retVal = indian_AfterDot + ' Paise only.';

        }
        else if (myArr[1] != null && parseInt(myArr[1]) != 0) {
            var indian_AfterDot = num2words.numberToWords(myArr[1]);
            retVal = indian_BeforeDot + ' Rupees and ' + indian_AfterDot + ' Paise only.';
        }
        else {
            if (indian_BeforeDot.length > 0) // Add by Ravi Bansal 17/01/2014
                retVal = indian_BeforeDot + ' Rupees only';
            else
                retVal = '';
        }
    }
    else {
        if (indian_BeforeDot.length > 0) // Add by Ravi Bansal 17/01/2014
            retVal = indian_BeforeDot + ' Rupees only.';
        else
            retVal = '';
    }
    return retVal;
}

$(document).ready(function () {
    $("#resultDiv").hide();
    $("#input").focus();
    $(document).keypress(function (e) {
        if (e.keyCode == 27) {
            clear();
        };
        if (e.keyCode == 13) {
            translate();
        };
    });
});