$(document).on('click', function (e) {
    if ($(e.target).parent().is('.dropdown-btn') || $(e.target).parent().is('.dropdown-menu')) {
        let menu = $(e.target).siblings('.dropdown-menu')
        
        if (menu.hasClass('opened')) {
            menu.removeClass('opened')
            $('.dropdown-menu').hide();
        } else {
            menu.addClass('opened')
            $('.dropdown-menu').show();
        }
    } else {
        $('.dropdown-menu').removeClass('opened')
        $('.dropdown-menu').hide();
    }
})

