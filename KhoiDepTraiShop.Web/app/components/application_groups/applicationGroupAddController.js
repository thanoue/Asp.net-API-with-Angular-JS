(function (app) {
    'use strict';

    app.controller('applicationGroupAddController', applicationGroupAddController);

    applicationGroupAddController.$inject = ['$scope', 'apiService', 'notifyService', '$location', 'commonService'];

    function applicationGroupAddController($scope, apiService, notifyService, $location, commonService) {
        $scope.group = {
            ID: 0,
            Roles: []
        }

        $scope.addAppGroup = addApplicationGroup;

        function addApplicationGroup() {
            apiService.post('/api/applicationGroup/add', $scope.group, addSuccessed, addFailed);
        }

        function addSuccessed() {
            notifyService.displaySuccess($scope.group.Name + ' đã được thêm mới.');

            $location.url('application_groups');
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
})(angular.module('khoideptraishop.application_groups'));