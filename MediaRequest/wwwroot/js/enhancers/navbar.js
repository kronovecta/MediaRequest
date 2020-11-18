

$(document).on('click', function (e) {
    let target = $(e.target);
    let navbar = $('.navbar');
    const targetClass = 'nav-dropdown';

    if ($(target).hasClass(targetClass) || $(target).parent('li').hasClass(targetClass)) {
        e.preventDefault();
        let menu = $(e.target).siblings('.submenu')

        $(menu).addClass('opened')
        $(menu).show();

    } else if (e.target != $('.submenu') && $('.nav-container').find('.submenu.opened').length > 0) {

        let openedMenu = $(navbar).find('.submenu.opened');
        openedMenu.hide();
        openedMenu.removeClass('opened')
    }
})

