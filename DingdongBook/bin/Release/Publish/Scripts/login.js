
$(() => {

    $('#signin-form').validate({
        submitHandler: function (form) {
            form.submit();
        }
    });
    $('#signup-form').validate({
        submitHandler: function (form) {
            form.submit();
        },
        rules: {
            re_password: {
                equalTo: '#password'
            },
            email: {
                email: true
            }
        }
    });
});
