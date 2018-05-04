(function (app) {
    app.factory('notifyService', notifyService);
    function notifyService() {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "100",
            "hideDuration": "700",
            "timeOut": "5000",
            "extendedTimeOut": "1000",  
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
      
        function displaySuccess(message) {
            toastr.options.positionClass = 'toast-top-right';
            toastr.success(message);
        }
        function displayError(error) {
            toastr.options.positionClass = 'toast-top-full-width';
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                })
            }
            else
                toastr.error(error);
        }
        function displayWarning(message) {
            toastr.options.positionClass = 'toast-bottom-full-width';
            toastr.warning(message);
        }
        function displayInfo(message) {
            toastr.options.positionClass = 'toast-top-left';
            toastr.info(message);
        }

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }
})(angular.module('khoideptraishop.common'));