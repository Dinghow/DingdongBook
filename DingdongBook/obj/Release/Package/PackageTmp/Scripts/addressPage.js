$(document).ready(function(){
	$(".control-ol .control").click(function(){
		$(".control-ol").hide();
		$(".select-ol").show();
		$(".select-ol .select-all").click(function(){
			$(".addresses .address").each(function(){
				var name=$(this).attr("name");
				if(!$(this).hasClass("selected")){
					
					$(this).addClass("selected");
				}
			});
		});
		$(".select-ol .delete").click(function(){
			$(".selected").remove();
		});
		$(".addresses .address").click(function(){
			var name=$(this).attr("name");
			if($(this).hasClass("selected")){
				$(this).removeClass("selected");
				
			}
			else{			
				
				$(this).addClass("selected");
			}
		});

	});
	$(".select-ol .cancle").click(function(){select_cancle();});
	$(".top-control .select").click(function(){

    });

    $('#delete-addr').on('click', function () {
        let addrIds = [];
        $('.address.selected').each((index, el) => {
            addrIds.push(Number($(el).attr('data-addrid')));
        });
        $.ajax({
            type: 'POST',
            url: '/Address/DeleteAddress',
            data: {
                addrIds: addrIds
            },
            success: function (userId) {
                window.location.reload();
            },
            error: function (e) {
                alert('Ã·Ωª ß∞‹');
            }
        });
    });
});

function select_cancle(){
	$(".control-ol").show();
	$(".select-ol").hide();
	$(".selected").removeClass("selected");
	$(".addresses .address").unbind();
}