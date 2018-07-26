$(document).ready(function () {
    var input_commend = $('#input-commend');
    var grade_num = $('.total-grade');
    input_commend.change(function () {
        var hasCommend = input_commend.val();
        var hasGrade = $('.grade-num').text();
        if (hasCommend != '' && hasGrade != '') {
            $('.btn-submit').removeAttr('disabled');
        }
        else {
            $('.btn-submit').attr('disabled', 'disabled');
        }
    });
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
        $(".total-grade").mouseout(function () {
            for (var i = 0; i < 5; i++) {
                total_grade_star[i].innerText = "star_border";
            }
            $(".grade-num").text("");
            //55
            $('.btn-submit').attr('disabled', 'disabled');
        });
        $(".total-grade i").click(function () {
            //55
            var input_commend = $('#input-commend');
            var hasCommend = input_commend.val();
            if (hasCommend != '') $('.btn-submit').removeAttr('disabled');
            $(".total-grade").unbind();
        });

    });

    /*var hasCommend = $('#input-commend').val();
    var hasGrade = $('.grade-num').text();
    if (hasCommend == '' || hasGrade == '') {
        $('.submit-cmt').attr('disabled', 'disabled');
    } else {
        $('.submit-cmt').attr('disabled', 'false');
    }*/

    $('.comment-form').validate({
        submitHandler: function (form) {

            var hasContent = $(".grade-num").text();
            if (hasContent != '') {
                $('#new-commend').modal('hide');
                form.submit();
            }
            else {
                $(".grade-num").text("请评分");
            }
        }
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

    $('.submit-cmt').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/Orders/SetComment',
            data: {
                bookId: Number($('#new-commend form.comment-form').attr('data-bookid')),
                grade: Number($('#new-commend form.comment-form .grade-num>span').text()),
                content: $('#new-commend form.comment-form #input-commend').val()
            },
            success: function (userId) {
                $('#new-commend').modal('hide');
                //window.location.href = '/orders/process_cart?id=' + userId;
            },
            error: function (e) {
                alert('提交失败');
            }
        });
    });

   
    
});