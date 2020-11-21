$(document).on('click', function (e) {
    let target = $(e.target);
    let navbar = $('.navbar');
    const targetClass = 'nav-dropdown';

    if ($(target).hasClass(targetClass) || $(target).parent('li').hasClass(targetClass)) {
        e.preventDefault();
        $('.submenu').hide();

        let submenu = $(e.target).siblings('.submenu')

        let targetPosition = $(e.target).position().left;
        let targetWidth = $(e.target).outerWidth();
        let windowSize = $(window).width();

        let right = windowSize - targetPosition - targetWidth;
        submenu.css('right', right)

        $(submenu).addClass('opened')
        $(submenu).show();

    } else if (e.target != $('.submenu') && $('.nav-container').find('.submenu.opened').length > 0) {

        let openedMenu = $(navbar).find('.submenu.opened');
        openedMenu.hide();
        openedMenu.removeClass('opened')
    }
})