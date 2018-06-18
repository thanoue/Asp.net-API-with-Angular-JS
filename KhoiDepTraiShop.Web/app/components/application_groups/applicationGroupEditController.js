(function (app) {
    'use strict';

    app.controller('applicationGroupEditController', applicationGroupEditController);

    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notifyService', '$location', '$stateParams'];

    function applicationGroupEditController($scope, apiService, notifyService, $location, $stateParams) {
        $scope.group = {}


        $scope.updateApplicationGroup = updateApplicationGroup;

        function updateApplicationGroup() {
            apiService.put('/api/applicationGroup/update', $scope.group, addSuccessed, addFailed);
        }
        function loadDetail() {
            apiService.get('/api/applicationGroup/detail/' + $stateParams.id, null,
            function (result) {
                $scope.group = result.data;
            },
            function (result) {
                notifyService.displayError(result.data);
            });
        }

        function addSuccessed() {
            notifyService.displaySuccess($scope.group.Name + ' đã được cập nhật thành công.');

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
        loadDetail();
    }
})(angular.module('khoideptraishop.application_groups'));