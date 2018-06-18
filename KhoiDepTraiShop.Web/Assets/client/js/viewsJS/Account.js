var common = {
    init: function () {
        common.registerEvents();

    },
    registerEvents: function () {

        $('#viewRegisterPopupbtn').click(function () {
            $('#smallLoading').show();

            $.ajax({
                type: 'GET',
                url: '/Account/Register',
                success: function (result) {
                    $('#signUpPopup').html(result);
                    $('#signUpModal').modal({
                        keyboard: false,
                        backdrop: 'static',
                    });
                    $('#smallLoading').hide();
                }
            });
        });
        $('body').delegate('#registerBtn', 'click', function () {

            var $form = $(this).closest("form");
            let formData = $form.serialize();

            $.ajax({
                type: 'POST',
                url: '/Account/Register',
                data: formData,
                success: function (result) {
                    if (result.isSuccess == undefined) {
                        $('#signUpPopup').html(result);
                    } else {
                        toastr.success("Bạn đã đăng ký thành công");
                        toastr.success("Kiểm tra hộp thư của bạn để nhận mã xác thực tài khoản!");
                       
                        $('#signUpModal .close').click();
                    }

                }
            });
        });

        $('#btnViewSigninPopup').click(function () {
            $('#smallLoading').show();

            $.ajax({
                type: 'GET',
                url: '/dang-nhap',
                success: function (result) {
                    $('#loginPopup').html(result);
                    $('#signInModal').modal({
                        keyboard: false,
                        backdrop: 'static',
                    });
                    $('#smallLoading').hide();
                }
            });
        });

        $('body').delegate('#loginBtn', 'click', function () {

            var $form = $(this).closest("form");
            let formData = $form.serialize();
            $.ajax({
                type: 'POST',
                url: '/Account/Login',
                data: formData,
                success: function (result) {

                    if (!result.isSuccess) {
                        toastr.info(result.obj.Message);
                        if (result.obj.Type == 'NeedCridentalCode')
                            $('#cridentalCodePanel').removeClass('hidden');
                        return;
                    }
                    toastr.success("Đăng nhập thành công");                    
                    $('#signInModal .close').click();
                    location.reload();
                    //Update user info here

                }
            });
        });

        $('#bntLogOut').click(function () {
            $.ajax({
                type: 'GET',
                url: '/Account/LogOut',
                success: function (result) {
                    if (!result.isSuccess)
                        toastr.error(result.obj.Message);
                    else
                        location.reload();
                }
            });
        });




    }
}
common.init();