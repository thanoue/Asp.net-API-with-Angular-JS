(function (app) {
    app.filter('orderStatusFilter', function () {
        return function (input) {
            switch (input) {
                case 0:
                    return "Đang gửi đi"
                    break;
                case 1:
                    return "Đang xử lý"
                    break;
                case 2:
                    return "Đang chuyển hàng"
                    break;
                case 3:
                    return "Đã nhận hàng"
                    break;
                case 4:
                    return "Đã hủy"
                    break;
            }            
        }
    });
})(angular.module('khoideptraishop.common'));