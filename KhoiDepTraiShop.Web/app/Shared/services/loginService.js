(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData',
        function ($http, $q, authenticationService, authData) {
            var userInfo;
            var deferred;
            var res;
            this.login = function (userName, password,sucessFunc,failFunction) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;

                var req = {
                    method: 'POST',
                    url: '/oauth/token',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    data: { data: data }
                }
                $http({
                    method: 'POST',
                    url: '/oauth/token',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    data: data
                }).then(function (response) {
                    console.log('response here');
                    userInfo = {
                        accessToken: response.data.access_token,
                        userName: userName
                    };
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;                 
                    sucessFunc(response);
                }, function (error) {
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";                   
                    failFunction(error);
                  
                });

                return res;
            }

            this.logOut = function () {
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
            }


        }]);
})(angular.module('khoideptraishop.common'));