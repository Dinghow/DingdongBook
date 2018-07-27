var nameArr = [];

$(document).ready(function () {
    

    $(".top-group #button-1").click(function () {
        $(".top-group #button-1").attr("class", "btn btn-danger");
        $(".top-group #button-2").attr("class", "btn btn-default");
        $("#omit").show();
        $("#detail").hide();
        select_cancle();
    });
    $(".top-group #button-2").click(function () {
        $(".top-group #button-1").attr("class", "btn btn-default");
        $(".top-group #button-2").attr("class", "btn btn-danger");
        $("#omit").hide();
        $("#detail").show();
        select_cancle();
    });
    $(".control-ol .control").click(function () {
        $(".control-ol").hide();
        $(".select-ol").show();
        $(".select-ol .select-all").click(function () {
            $("#display-scane-favorite #detail .card").each(function () {
                var name = $(this).attr("name");
                if (!$(this).hasClass("selected")) {
                    nameArr.push(name);
                    $(this).addClass("selected");
                }
            });
            $("#display-scane-favorite #omit .card").each(function () {
                var name = $(this).attr("name");
                if (!$(this).hasClass("selected")) {
                    $(this).addClass("selected");
                }
            });
        });
        $(".select-ol .delete").click(function () {
            var trans = '';
            for (var i = 0; i < nameArr.length; i++) {
                var name = nameArr[i];

                trans += name;
                trans += ",";
                $(".card").remove("[name=" + name + "]");
            }

            $("#trans-input").attr("value", trans);
            nameArr.splice(0,nameArr.length);
        });
        $("#display-scane-favorite .card").click(function () {
            var name = $(this).attr("name");
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
                for (var i = 0; i < nameArr.length; i++) {
                    if (nameArr[i] == name) {
                        nameArr.splice(i, 1);
                        break;
                    }
                }
            }
            else {
                nameArr.push(name);
                $(this).addClass("selected");
            }
        });

    });
    $(".select-ol .cancle").click(function () { select_cancle(); });
    $(".top-control .select").click(function () {

    });

    $('#delete-selected').on('click', function (e) {
        e.preventDefault();
        bookIds = [];
        $('#omit .card.selected').each((index, el) => {
            bookIds.push(Number($(el).attr('data-bookid')));
        });
        $.ajax({
            type: 'POST',
            url: '/favorite/DeleteFav',
            data: {
                bookIds: bookIds
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

function select_cancle() {
    $(".control-ol").show();
    $(".select-ol").hide();
    $(".selected").removeClass("selected");
    $("#display-scane-favorite .card").unbind();
    nameArr.splice(0, nameArr.length);
}
