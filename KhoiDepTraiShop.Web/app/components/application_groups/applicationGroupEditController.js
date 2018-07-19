(function (app) {
    'use strict';

    app.controller('applicationGroupEditController', applicationGroupEditController);

    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notifyService', '$location', '$stateParams'];

    function applicationGroupEditController($scope, apiService, notifyService, $location, $stateParams) {
        $scope.group = {}
        $scope.loading = true;


        $scope.updateApplicationGroup = updateApplicationGroup;

        function updateApplicationGroup() {
            $scope.loading = true;
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
            $scope.loading = false;
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
                    $scope.loading = false;
                }, function (response) {
                    notifyService.displayError('Không tải được danh sách quyền.');
                });

        }

        loadRoles();
        loadDetail();
    }
})(angular.module('khoideptraishop.application_groups'));