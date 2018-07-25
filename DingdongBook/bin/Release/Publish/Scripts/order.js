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
		$(".grade-num").text(num+"颗星");
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
});