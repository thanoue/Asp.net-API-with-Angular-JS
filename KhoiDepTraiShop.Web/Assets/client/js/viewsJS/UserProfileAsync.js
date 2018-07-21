$(document).ready(function () {
    $('body').on('click', '#saveChangeProfile', userProfileChange);
    $('body').on('click', '#saveChangeNewPassword', changePassword);
    $(function () {
        $("[title]").tooltip({ 'placement': "bottom" });
    });
});

let userProfileChange = function () {
    var $form = $(this).closest("form");
   
    let formData = $form.serialize();

    $.ajax({
        type: 'POST',
        url:'/Customer/UpdateUserProfile' ,
        data: formData,
        success: function (result) {
            if (result.isSuccess == undefined) {
                $('#userProfileArea').html(result);
            } else {

                toastr.success("Bạn đã cập nhật thành công, xin mời đăng nhập lại");
                window.location ='/Account/LogOutToIndex'
            }

        }
    });
}

let changePassword = function () {
    var $form = $(this).closest("form");   
    let formData = $form.serialize();
    $.ajax({
        type: 'POST',
        url: '/Customer/PasswordChangingSave',
        data: formData,
        success: function (result) {
            if (result.isSuccess == undefined) {
                $('#passwordArea').html(result);
            } else {

                toastr.success("Bạn đã cập nhật thành công, xin mời đăng nhập lại");
                window.location = '/Account/LogOutToIndex'
            }

        }
    });
}