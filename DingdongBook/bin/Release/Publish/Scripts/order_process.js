$(()=>{
	$('.address').on('click', changeSelectedAddress);
	$('.pay-method').on('click', changePayMethod);
	$('.create-addr').on('click', createAddr);
	$('#new-address').on('show.bs.modal', ()=>{

    });

    $('#submit-order').on('click', () => {
        let addrId = Number($('li.address.selected').attr('data-addrid'));
        $.ajax({
            type: 'POST',
            url: '/Orders/check',
            data: {
                addrId: addrId,
            },
            success: function (data) {
                window.location.href="Order_Complete"
            },
            error: function () {
                alert('提交订单失败！');
            }
        });
    })
});

function changeSelectedAddress(e){
	$('.address.selected').removeClass('selected');
	$(this).addClass('selected');
	let payInfo = $('.pay-info');
	payInfo.find('.province').text($(this).find('.province').text());
	payInfo.find('.city').text($(this).find('.city').text());
	payInfo.find('.district').text($(this).find('.district').text());
	payInfo.find('.detailed-addr').text($(this).find('.detailed-addr').text());
	payInfo.find('.zip').text($(this).find('.zip').text());
	payInfo.find('.name').text($(this).find('.name').text());
	payInfo.find('.phone').text($(this).find('.phone').text());
}

function changePayMethod(e){
	$('.pay-method.btn-danger').removeClass('btn-danger').addClass('btn-light border');
	$(this).removeClass('btn-light border').addClass('btn-danger');
}

function createAddr(e){
	$('.addresses').append($('<li class="address"><h6><span class="name">'+$('#inputLastName').val()+$('#inputFirstName').val()
		+'</span><span class="phone float-right">'+$('#inputPhone').val()
		+'</span></h6><p class="pcd"><span class="province tab">'+$('#inputProvince').val()
		+'</span><span class="city tab">'+$('#inputCity').val()
		+'</span><span class="district tab">'+$('#inputDistrict').val()
		+'</span><span class="detailed-addr tab">'+$('#inputAddress').val()
		+'</span></p><p class="postcode">邮编：<span class="zip tab">'+$('#inputZip').val()
		+'</span></p></li>').on('click', changeSelectedAddress));

	$('#new-address').modal('hide');
}