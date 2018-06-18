(function (app) {
    'use strict';

    app.controller('applicationUserAddController', applicationUserAddController);

    applicationUserAddController.$inject = ['$scope', 'apiService', 'notifyService', '$location', 'commonService'];

    function applicationUserAddController($scope, apiService, notifyService, $location, commonService) {
        $scope.account = {
            Groups: []
        }

        $scope.addAccount = addAccount;

        function addAccount() {
            apiService.post('/api/applicationUser/add', $scope.account, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notifyService.displaySuccess($scope.account.Name + ' đã được thêm mới.');

            $location.url('application_users');
        }
        function addFailed(response) {
            notifyService.displayError(response.data.Message);
        }

        function loadGroups() {
            apiService.get('/api/applicationGroup/getlistall',
                null,
                function (response) {
                    $scope.groups = response.data;
                }, function (response) {
                    notifyService.displayError('Không tải được danh sách nhóm.');
                });

        }

        loadGroups();

    }
})(angular.module('khoideptraishop.application_users'));