// MOBILE NAV
let mobilenav = $('.mobilenav');
let mobilemenu = $('.mobilenav > .nav-item-container');
let mobilebackdrop = $('.mobilenav > .backdrop');
let mobileMenuWidth = mobilemenu.outerWidth(true);
let mobileMenuIcon = $('.mobile-menu-icon');

$(mobilebackdrop).on('click', function () {
    mobileMenuHide();
})

$(mobileMenuIcon).on('click', function () {
    mobileMenuShow();
})

function mobileMenuShow() {
    $(mobilenav).show();
    $(mobilemenu).animate({ 'left': '0' }, 'fast');
    $(mobilebackdrop).show();
}

function mobileMenuHide() {
    $(mobilemenu).animate({ 'left': '-300px' }, 'fast', function () {
        $(mobilebackdrop).fadeOut('fast')
        $(mobilenav).hide();
    });
}