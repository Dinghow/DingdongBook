$(() => {
    $('.thumb-up i').on('click', thumbUp);
    $('.thumb-down i').on('click', thumbDown);
    /*
    $('#add-to-cart').on('click', function () {
        $('#success-alert').fadeIn('slow', function () {
            $(this).delay(1000).fadeOut();
        });
    });
    */
});

function thumbUp() {
    let el = $(this).parent().next().children('i');
    //let cancelThumbDown = false;
    if (!el.hasClass('clicked')) {
        giveAttitude(this);
    }
    
    $.ajax({
        type: 'POST',
        url: '/Books/like',
        data: {
            cmtId: Number($(this).parents('li.comment').attr('data-cmtid')),
            //cancelThumbDown: cancelThumbDown
        },
        success: function (flag) {
            if (flag == -1)
                window.location.href = "/Home/Login";
        },
        error: function (e) {
            alert('提交失败');
        }
    });
}

function thumbDown() {
    let el = $(this).parent().prev().children('i');
    //let canelThumbUp = false
    if (!el.hasClass('clicked')) {
        giveAttitude(this);
    }
        
    
    $.ajax({
        type: 'POST',
        url: '/Books/dislike',
        data: {
            cmtId: Number($(this).parents('li.comment').attr('data-cmtid'))
        },
        success: function (flag) {
            if (flag == -1)
                window.location.href = "/Home/Login";
        },
        error: function (e) {
            alert('提交失败');
        }
    });
}


function giveAttitude(el) {
    if ($(el).hasClass('clicked')) {
        $(el).next().text(Number($(el).next().text()) - 1);
    }
    else {
        $(el).next().text(Number($(el).next().text()) + 1);
        $(el).addClass('clicked');
    }
}