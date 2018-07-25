$(document).ready(function(){
	$(".card .btn-delete").click(function(){
		$(this).parents(".card").remove();
	});
	$(".btn-group #button-1").click(function(){
		$(".order-unreceived").show();
		$(".order-received").hide();
		$(".top-group #button-1").attr("class","btn btn-success");
		$(".top-group #button-2").attr("class","btn btn-default");
	});
	$(".btn-group #button-2").click(function(){
		$(".order-received").show();
		$(".order-unreceived").hide();
		$(".top-group #button-1").attr("class","btn btn-default");
		$(".top-group #button-2").attr("class","btn btn-danger");
	});
	$(".btn-commend").click(function(){
		var total_grade_star=$(".total-grade").children();
		for(var i=0;i<5;i++){
				total_grade_star[i].innerText="star_border";
			}
			$(".grade-num").text("");
	});
	$(".total-grade i").mouseover(function(){
		var num=$(this).attr("id");
		var total_grade_star=$(".total-grade").children();
		
		for(var i=0;i<5;i++){
			total_grade_star[i].innerText="star_border";
		}
		for(var i=0;i<num;i++){
			total_grade_star[i].innerText="star";
		}
		$(".grade-num").append("<span>"+num+"</span>颗星");
		$(".total-grade").mouseout(function(){
			for(var i=0;i<5;i++){
				total_grade_star[i].innerText="star_border";
			}
			$(".grade-num").text("");
		});
		$(".total-grade i").click(function(){
			$(".total-grade").unbind();
		});
    });


    $('.confirm-order').on('click', function () {
        let orderId = Number($(this).parents('.order-group').attr('data-orderid'));
        $.ajax({
            type: 'POST',
            url: '/Orders/StatusChange',
            data: {
                orderId: orderId
            },
            success: function (data) {
                window.location.reload();
            },
            error: function () {
                alert('订单确认失败！');
            }
        });
    });

    $('.delete-order').on('click', function () {
        let orderId = Number($(this).parents('.order-group').attr('data-orderid'));
        $.ajax({
            type: 'POST',
            url: '/Orders/DeleteOrder',
            data: {
                orderId: orderId
            },
            success: function (data) {
                window.location.reload();
            },
            error: function () {
                alert('订单删除失败！');
            }
        });
    });

    $('.btn-commend').on('click', function () {
        $('#new-commend form.comment-form').attr('data-bookid', $(this).parents('tr').attr('data-bookid'));
    });

    $('#new-commend form.comment-form').on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Orders/SetComment',
            data: {
                bookId: Number($(this).attr('data-bookid')),
                grade: Number($(this).children('.grade-num>span').text()),
                content: $(this).children('#input-commend').text()
            },
            success: function (userId) {
                //window.location.href = '/orders/process_cart?id=' + userId;
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    });
});