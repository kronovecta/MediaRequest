//$(".dropdown").on('click', function (e) {
//    e.preventDefault();
//    let menu = $(this).children(".submenu");

//    if (menu.hasClass('opened')) {
//        menu.removeClass('opened')
//        $(menu).hide();
//    } else {
//        menu.addClass('opened')
//        $(menu).show();
//    }
//})

$(document).on('click', function (e) {
    if ($(e.target).parent().is('.nav-dropdown') || $(e.target).parent().is('.nav-dropdown')) {
        e.preventDefault()
        let menu = $(e.target).siblings('.submenu')

        if (menu.hasClass('opened')) {
            menu.removeClass('opened')
            $(menu).hide();
        } else {
            menu.addClass('opened')
            $(menu).show();
        }
    } else {
        $(menu).removeClass('opened')
        $(menu).hide();
    }
})

