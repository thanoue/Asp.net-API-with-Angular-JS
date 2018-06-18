(function (app) {
    'use strict';

    app.controller('applicationRoleAddController', applicationRoleAddController);

    applicationRoleAddController.$inject = ['$scope', 'apiService', 'notifyService', '$location', 'commonService'];

    function applicationRoleAddController($scope, apiService, notifyService, $location, commonService) {
        $scope.role = {
            Id: 0
        }

        $scope.addAppRole = addAppRole;

        function addAppRole() {
            apiService.post('/api/applicationRole/add', $scope.role, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notifyService.displaySuccess($scope.role.Name + ' đã được thêm mới.');

            $location.url('application_roles');
        }
        function addFailed(response) {
            notifyService.displayError(response.data.Message);
        }

        function loadRoles() {
            apiService.get('/api/applicationRole/getlistall',
                null,
                function (response) {
                    $scope.roles = response.data;
                }, function (response) {
                    notifyService.displayError('Không tải được danh sách quyền.');
                });

        }

        loadRoles();
    }
})(angular.module('khoideptraishop.application_roles'));