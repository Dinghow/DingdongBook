$(() => {
	$('#cart-table tbody tr>th>input').on('change', updateSelectedItems);
	$('.cart input[name="select-all"]').on('click', selectAll);
	$('.remove, .move-to-favorite').on('click', removeItem);
	$('.number-controller .increase, .number-controller .decrease').on('click', updateFinalPrice);
	$('.mass-delete').on('click', massDelete);
});

function updateSelectedItems(e){
	let cnt = $('#cart-table tbody tr>th>input:checked').length;
	$('#settlement .check-all span>strong').text(cnt);
}

function selectAll(e) {
	let checked = $(this).is(':checked');
	$('.cart :checkbox').each((index, el) => {
		el.checked = checked;
	});
	updateSelectedItems();
}

function removeItem(e) {
	$(this).parents('tr').remove();
}

function updateFinalPrice(e) {
	/* 更改 Total Price */
	$(this).parents('.amount').next().children('span').text(
		($(this).parents('.amount').prev().children('span').text() *
			$(this).siblings('input[type=number]').val()).toFixed(2));

	/* 更改 Final Price */
	let finalPrice = 0.00;
	$('#cart-table tbody tr .total-price span').each((index, el) => {
		finalPrice += Number($(el).text())
	});
	$('#settlement .final-price span').text(finalPrice.toFixed(2));
}

function massDelete(e) {
	$('#cart-table tbody tr>th>input:checked').each((index, el) => {
		$(el).parents('tr').remove()
	});
}