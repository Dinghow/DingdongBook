$(function(){
	//定时更新头部广告，2秒一次
	var ad_index = 0;
	setInterval(function () {
		changeAd();
	}, 2000);
	
	function changeAd(){
		$(".topAd").css("background-image","url(img/ad_" + ad_index + ".png)");
		ad_index++;
		if(ad_index==4){
			ad_index=0;
		}
	}
	
	/////////送至某地////////////
	$(".dis_add").mouseenter(function(){
					$(".hide_add").css({display:"block"})
					}).mouseleave(function(){
						$(".hide_add").css({display:"none"})
						})
					$(".hide_add ul li a").click(function(){
					$(".hide_add").css({display:"none"})
					})
					$(".hide_add ul li").click(function(){
					$(".hide_add ul li").find("a").removeClass("selected");
					$(this).find("a").addClass("selected");})
					$(".hide_add ul li a").click(function(){
					$(".hide_add").css({display:"none"})
				})
	/////////////////////////
	$(".dao_list a").click(function(){
					$(this).addClass("color").siblings().removeClass("color")
					});
				/*控制显示或消失1	*/
				$(".cates").mouseenter(function(){
					var cat=$(this).attr("mt-ct");
					$(".b-"+cat+"").show().siblings().hide();
					})
				$(".important").mouseleave(function(){
					$(".import_list").hide()
					})
				/*侧边栏每块划上的时候添加颜色块*/
				$("aside ul li").mouseenter(function(){
					$(this).addClass("yanses").siblings().removeClass("yanses")
					})
				/*滚动之后固定定位*/
				$(window.document).scroll(function () {
					if($(window).scrollTop()>500){
						$(".dao_hang").addClass("fixed_dh");
						$("aside").css({display:"none"});
						/* $(".advertisement").css({display:"none"}); */
						/*侧边栏的出现和消失*/
					$(".important").mouseenter(function(){
						$("aside").css({display:"block",background:"#fff"})
						}).mouseleave(function(){
							$("aside").css({display:"none"})
							})
					};
					if($(window).scrollTop()<500){
						$(".dao_hang").removeClass("fixed_dh");
						$("aside").css({display:"block"});
						/* $(".advertisement").css({display:"block"}); */
						/*侧边栏的出现和消失*/
						$(".important").mouseleave(function(){
							$("aside").css({display:"block"})
							})
					};
					});
	//////////////////////////////
	//点击页面移动到对应位置
	$('.fixed_position_1 ul li').click(function(){
		$('html,body').animate({scrollTop:$('.as').eq($(this).index()).offset().top-100}, 500);}
	);
	//滚动条移动到对应的位置，对应的标签高亮显示
	$(window.document).scroll(function () {
		for(var i = 0;i<$(".as").length;i++){
			if($(window).scrollTop()>$(".as").eq(i).offset().top-300){
				$(".fixed_position_1 ul li").eq(i).addClass("addcss").siblings(".fixed_position_1 ul li").removeClass("addcss");
			}
		};
	});
	//左边楼层的出现或消失
	$(window.document).scroll(function () {
		if($(window).scrollTop()>500){
			$(".fixed_position_1").css({display:"block"})

		};
		if($(window).scrollTop()<500){
			$(".fixed_position_1").css({display:"none"})

		};
	});
	///////////////////////
	/*控制显示或消失2	*/
	$(".dog").mouseenter(function(){
	   var dogs=$(this).attr("dg-floor")
	   var dog=$(this).attr("dg-ct");
	   $(".f-"+dogs+"-"+dog+"").show().siblings().hide();
		})
	/*添加色块*/
	$(".detailed_navigation ul li").mouseenter(function(){
		$(this).addClass("yadi").siblings().removeClass("yadi")
		})
	///////////////////////////
	var i=0
	var clone=$(".lunbobanner .lunboimg li").first().clone();
	$(".lunbobanner .lunboimg").append(clone)
	var size=$(".lunbobanner .lunboimg li").size()
	/*自动轮播*/
	var t=setInterval(moveL,5000)
	/*定时器的关闭与开启*/
	$(".kongzhianniu").hover(function(){
		clearInterval(t);

		},function(){
		t=setInterval(moveL,5000)

		})

	$(".btnr").click(function(){
		moveL()
		})
	function moveL(){
		i++
		if(i==size){
			$(".lunboimg").css({left:0})
			i=1;
			}
		$(".lunboimg").stop().animate({left:-i*1200},500)

		}

	$(".btnl").click(function(){
		moveR()
		})
	function moveR(){
		i--
		if(i==-1){
			$(".lunboimg").css({left:-(size-1)*1200})
			i=size-2;
			}
		$(".lunboimg").stop().animate({left:-i*1200},500)
		}
		/////////////////////////////////
		var offset = $(".toolbar-item-weixin").offset();
    $(".add_scar").click(function(event){
        var addcar = $(this);
        var img = addcar.parent().parent().find('img').attr('src');
        var flyer = $('<img class="u-flyer" src="'+img+'">');
		var tops = $(window).scrollTop()

        flyer.fly({
            start: {
                left: event.pageX, //开始位置（必填）#fly元素会被设置成position: fixed
                top: event.pageY-tops, //开始位置（必填）
				width: 0, //结束时宽度
                height: 0 //结束时高度
            },
            end: {
                left: offset.left, //结束位置（必填）
                top: offset.top+40, //结束位置（必填）
                width: 0, //结束时宽度
                height: 0 //结束时高度
            },

        });
    });









































	})