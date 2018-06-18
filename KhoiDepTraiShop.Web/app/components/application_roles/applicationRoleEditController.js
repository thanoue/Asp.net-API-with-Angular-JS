(function (app) {
    'use strict';

    app.controller('applicationRoleEditController', applicationRoleEditController);

    applicationRoleEditController.$inject = ['$scope', 'apiService', 'notifyService', '$location', '$stateParams'];

    function applicationRoleEditController($scope, apiService, notifyService, $location, $stateParams) {
        $scope.role = {}


        $scope.updateApplicationRole = updateApplicationRole;

        function updateApplicationRole() {
            apiService.put('/api/applicationRole/update', $scope.role, addSuccessed, addFailed);
        }
        function loadDetail() {
            apiService.get('/api/applicationRole/detail/' + $stateParams.id, null,
            function (result) {
                $scope.role = result.data;
            },
            function (result) {
                notifyService.displayError(result.data);
            });
        }

        function addSuccessed() {
            notifyService.displaySuccess($scope.role.Name + ' đã được cập nhật thành công.');

            $location.url('application_roles');
        }
        function addFailed(response) {
            notifyService.displayError(response.data.Message);
        }
        loadDetail();
    }
})(angular.module('khoideptraishop.application_roles'));