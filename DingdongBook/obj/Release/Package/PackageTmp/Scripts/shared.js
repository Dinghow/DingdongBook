$(()=>{
	$('.number-controller .decrease').on('click', decreaseAmount);
	$('.number-controller .increase').on('click', increaseAmount);
});

function decreaseAmount(e){
	e.preventDefault();
	let prevVal = $(this).next().val();
	if(prevVal > 1)
		$(this).next().val(--prevVal);
}

function increaseAmount(e){
	e.preventDefault();
	let prevVal = $(this).prev().val();
	$(this).prev().val(++prevVal);
}