var hidWidth;
var scrollBarWidths = 40;

var widthOfList = function() {
    var itemsWidth = 0;
    $('.Navlist a').each(function() {
        var itemWidth = $(this).outerWidth();
        itemsWidth += itemWidth;
    });
    return itemsWidth;
};

var widthOfHidden = function() {
    return (($('.Navwrapper').outerWidth()) - widthOfList() - getLeftPosi()) - scrollBarWidths;
};

var getLeftPosi = function() {
    return $('.Navlist').position().left;
};

var reAdjust = function() {
    if (($('.Navwrapper').outerWidth()) < widthOfList()) {
        $('.Navscroller-right').show();
    } else {
        $('.Navscroller-right').hide();
    }

    if (getLeftPosi() < 0) {
        $('.Navscroller-left').show();
    } else {
        $('.Navitem').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow');
        $('.Navscroller-left').hide();
    }
}

reAdjust();

$(window).on('resize', function(e) {
    reAdjust();
});

$('.Navscroller-right').click(function() {

    $('.Navscroller-left').fadeIn('slow');
    $('.Navscroller-right').fadeOut('slow');

    $('.Navlist').animate({ left: "+=" + widthOfHidden() + "px" }, 'slow', function() {

    });
});

$('.Navscroller-left').click(function() {

    $('.Navscroller-right').fadeIn('slow');
    $('.Navscroller-left').fadeOut('slow');

    $('.Navlist').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow', function() {

    });
});