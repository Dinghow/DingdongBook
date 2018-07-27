$(function () {
    var bannerSlider = new Slider($('#banner_tabs'), {
        time: 5000,
        delay: 400,
        event: 'hover',
        auto: true,
        mode: 'fade',
        controller: $('#bannerCtrl'),
        activeControllerCls: 'active'
    });
    $('#banner_tabs .flex-prev').click(function () {
        bannerSlider.prev()
    });
    $('#banner_tabs .flex-next').click(function () {
        bannerSlider.next()
    });

    $('.add-to-fav').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Favorite/directAddFav',
            data: {
                bookId: Number($(this).parents('li').attr('data-addrid'))
            },
            success: function (feedback) {
                if (feedback === '-1')
                    window.location.href = "/Home/Login";
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    });
    $('.add-to-cart').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Books/directAddCart',
            data: {
                bookId: Number($(this).parents('li').attr('data-addrid'))
            },
            success: function (feedback) {
                if (feedback === '-1')
                    window.location.href = "/Home/Login";
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    });
})
