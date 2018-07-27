$(() => {
    $('#settlement .final-price span').text('0.00');
    $('#cart-table tbody tr>th>input').on('change', () => {
        updateSelectedItems();
        toggleDisable();
    });
    $('.cart input[name="select-all"]').on('click', selectAll);
    $('.remove, .move-to-favorite').on('click', removeItem);
    //$('.mass-delete').on('click', massDelete);
    $('.number-controller .increase, .number-controller .decrease').on('click', updateTotalPrice);
    
    $('#go-to-settle').on('click', () => {
        let bookIds = [];
        $('#cart-table>tbody>tr>th>input:checked').parents('tr.abook').each((index, el) => {
            bookIds.push(Number($(el).attr('data-bookid')));
        });
        console.log(bookIds);
        $.ajax({
            type: 'POST',
            url: '/Orders/GetOrder',
            data: {
                bookIds: bookIds,
            },
            success: function (userId) {
                window.location.href = '/orders/process_cart?id=' + userId;
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    })
    $('.remove').on('click', function() {     
        $.ajax({
            type: 'POST',
            url: '/Carts/RemoveBook',
            data: {
                bookId: Number($(this).parents('tr.abook').attr('data-bookid'))
            },
            success: function (userId) {
                window.location.reload();
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    })
    $('.move-to-favorite').on('click', function () {
        $.ajax({
            type: 'POST',
            url: '/Carts/AddFav',
            data: {
                bookId: Number($(this).parents('tr.abook').attr('data-bookid'))
            },
            success: function (userId) {
                //window.location.reload();
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    });


});

function updateSelectedItems(e){
	let cnt = $('#cart-table tbody tr>th>input:checked').length;
    $('#settlement .check-all span>strong').text(cnt);
    updateFinalPrice();
}

function selectAll(e) {
	let checked = $(this).is(':checked');
	$('.cart :checkbox').each((index, el) => {
		el.checked = checked;
	});
    updateSelectedItems();
    toggleDisable();
}

function removeItem(e) {
	$(this).parents('tr').remove();
}

function updateTotalPrice(e) {
	/* 更改 Total Price */
	$(this).parents('.amount').next().children('span').text(
		($(this).parents('.amount').prev().children('span').text() *
            $(this).siblings('input[type=number]').val()).toFixed(2));

    updateFinalPrice();
    /*
	// 更改 Final Price 
	let finalPrice = 0.00;
	$('#cart-table tbody tr .total-price span').each((index, el) => {
		finalPrice += Number($(el).text())
	});
    $('#settlement .final-price span').text(finalPrice.toFixed(2));
    */
    $.ajax({
        type: 'POST',
        url: '/Carts/EditAmount',
        data: {
            bookId: Number($(this).parents('tr.abook').attr('data-bookid')),
            bookAmount: Number($(this).siblings('input[type=number]').val())
        },
        success: function (userId) {
            //window.location.reload();
        },
        error: function (e) {
            alert('提交失败');
        }
    });
}

function updateFinalPrice() {
    /* 更改 Final Price */
    let finalPrice = 0.00;
    $('#cart-table tbody tr>th>input:checked').parents('tr').find('.total-price span').each((index, el) => {
        finalPrice += Number($(el).text())
    });
    $('#settlement .final-price span').text(finalPrice.toFixed(2));
}

function massDelete(e) {
	$('#cart-table tbody tr>th>input:checked').each((index, el) => {
		$(el).parents('tr').remove()
	});
}

function toggleDisable() {
    if ($('#cart-table tbody tr>th>input:checked').length > 0)
        $('#go-to-settle').attr('disabled', false);
    else
        $('#go-to-settle').attr('disabled', true);
}