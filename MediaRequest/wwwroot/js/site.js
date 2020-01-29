function scrollToAnchor(anchor) {
    let aTag = $("#" + anchor);
    let navHeight = 0;
    $('.navbar').outerHeight(false) == 0 ? navHeight = $('.mobile-top-nav').outerHeight(false) : navHeight = $('.navbar').outerHeight(false);
    $('html,body').animate({ scrollTop: (aTag.offset().top - navHeight) }, 'slow');
}

// INDEX
$(document).on('click', '.movie-list-container > .pagination > a', function () {
    scrollToAnchor('movies-partial-container');
    $('.loading').show();
})

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

// REQUESTS
let userRequestContainer = $('.avatar-user-container').not('.overflow');
let timerObject;

$(userRequestContainer).on('mouseenter', function (e) {
    $(this).animate({ 'width': ($(this).children('.avatar').outerWidth(true) + $(this).children('.username').outerWidth(true) + 8), 'margin-right': '1rem' }, {
        duration: 150,
        easing: 'swing',
        start: function () {
            $(this).children('.username').show();
        }
    });
})

$(userRequestContainer).on('mouseleave', function () {
    $(this).animate({ 'width': $(this).children('.avatar').outerWidth(true), 'margin-right': '0.5rem' }, {
        duration: 150,
        easing: 'swing',
        complete: function () {
            $(this).children('.username').hide();
        }
    })
})

$('.avatar-user-container.overflow').on('click', function (e) {
    $(this).parent().parent().children('.overflow-users').addClass('open')
    $(this).parent().parent().children('.overflow-users').css({
        'display': 'block',
        'left': e.pageX + 'px',
        'top': e.pageY + 'px'
    })
})

$(document).on('click', function (e) {
    console.log(e.target)
    let activeOverflow = $('.overflow-users.open');

    if (/*!$(e.target).is(activeOverflow) || */!$(e.target).is('.avatar-user-container.overflow')/* || !$(e.target).is('.overflow-amount')*/) {
        //$('.overflow-users').hide();
        console.log('closing')
    }  
})  