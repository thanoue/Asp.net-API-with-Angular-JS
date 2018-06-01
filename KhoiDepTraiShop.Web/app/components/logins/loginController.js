(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notifyService', 'authenticationService','authData',
        function ($scope, loginService, $injector, notifyService, authenticationService,authData) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password, function (response) {
                    notifyService.displaySuccess("Đăng nhập thành công");
                    var stateService = $injector.get('$state');
                    stateService.go('home');
                }, function (error) {
                    notifyService.displayError("Đăng nhập không đúng.");
                });                
            }

            $scope.clearCache = clearCache;
            function clearCache() {
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
            };
            $scope.clearCache();

        }]);
})(angular.module('khoideptraishop'));