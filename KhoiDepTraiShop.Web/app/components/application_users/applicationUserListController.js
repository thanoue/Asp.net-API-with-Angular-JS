(function (app) {
    'use strict';

    app.controller('applicationUserListController', applicationUserListController);

    applicationUserListController.$inject = ['$scope', 'apiService', 'notifyService', '$ngBootbox'];

    function applicationUserListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/applicationUser/delete', config, function () {
                        notifyService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                    function () {
                        notifyService.displayError('Xóa không thành công.');
                    });
                });
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.filterExpression
                }
            }

            apiService.get('api/applicationUser/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalRow;
            $scope.loading = false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                notifyService.displayInfo(result.data.Items.length + ' items found');
            }
        }
        function dataLoadFailed(response) {
            notifyService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }

        $scope.search();
    }
})(angular.module('khoideptraishop.application_users'));