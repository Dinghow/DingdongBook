$(() => {
    $('.thumb-up i').on('click', thumbUp);
});

function thumbUp(e) {
    if ($(this).parents('li.comment').attr('data-thumbedup') == 'false') {
        $(this).css('color', '#007bff');
        $(this).parents('li.comment').attr('data-thumbedup', 'true');
        $(this).next().text(Number($(this).next().text()) + 1);
    } else{
    	$(this).css('color', '#6c757d');
        $(this).parents('li.comment').attr('data-thumbedup', 'false');
        $(this).next().text(Number($(this).next().text()) - 1);
    }
}