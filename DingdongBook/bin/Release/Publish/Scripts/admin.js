$(() => {
    $('.select-category').multiselect();
    $('button.multiselect').addClass('border');

    /* 预览封面 */
    $('#input-cover').on('change', updateImageDisplay);

    /*切换操作*/
    $(".btn-user-control").click(function(){
        $(".user-control").show();
        $(".add-book").hide();
        $(".change-book").hide();
    });
    $(".btn-add-book").click(function(){
        $(".add-book").show();
        $(".user-control").hide();
        $(".change-book").hide();
    });
    $(".btn-change-book").click(function(){
        $(".change-book").show();
        $(".add-book").hide();
        $(".user-control").hide();
    });
    $(".user-control .search-result-user .btn-delete").click(function(){
        var thisCard=this;
        $(".user-control .modal-footer .btn-cancle").click(function(){
            thisCard='';
        });
        $(".user-control .modal-footer .btn-sure").click(function(){
            $(thisCard).parents(".card").remove();
        });

    });
    $(".change-book .search-result-book .btn-delete").click(function(){
        var thisCard=this;
        $(".change-book .modal-footer .btn-cancle").click(function(){
            thisCard='';
        });
        $(".change-book .modal-footer .btn-sure").click(function(){
            $(thisCard).parents(".card").remove();
        });

    });

});


function updateImageDisplay() {
    var preview = $('#preview');
    preview.empty();

    var curFiles = this.files;

    if (curFiles.length === 0) {
        preview.append('<p>没有文件</p>');
    } else {
        if (validFileType(curFiles[0]))
            preview.append('<img class="border rounded" src="' + window.URL.createObjectURL(curFiles[0]) + '">');
        else
        preview.append('<p>文件类型错误</p>');
    }
}

var fileTypes = [
    'image/jpeg',
    'image/pjpeg',
    'image/png'
]

function validFileType(file) {
    for (var i = 0; i < fileTypes.length; i++) {
        if (file.type === fileTypes[i]) {
            return true;
        }
    }

    return false;
}

function returnFileSize(number) {
    if (number < 1024) {
        return number + 'bytes';
    } else if (number > 1024 && number < 1048576) {
        return (number / 1024).toFixed(1) + 'KB';
    } else if (number > 1048576) {
        return (number / 1048576).toFixed(1) + 'MB';
    }
}