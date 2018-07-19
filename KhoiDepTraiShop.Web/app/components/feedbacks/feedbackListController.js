(function (app) {
    app.controller('product_FeedbackListController', product_FeedbackListController);

    product_FeedbackListController.$inject = ['$scope', 'apiService', 'notifyService', '$ngBootbox', '$filter'];

    function product_FeedbackListController($scope, apiService, notifyService, $ngBootbox, $filter) {
        $scope.waitingFeedbacks = [];
        $scope.populatedFeedbacks = [];
        $scope.deletedFeedbacks = [];
        $scope.currentSelected = {};
        $scope.loading = true;



        $scope.waitingPage = 0;
        $scope.waitingPagesCount = 0;
        $scope.waitingTotalCount = 0;

        $scope.populatedPage = 0;
        $scope.populatedPagesCount = 0;
        $scope.populatedTotalCount = 0;

        $scope.deletedPage = 0;
        $scope.deletedPagesCount = 0;
        $scope.deletedTotalCount = 0;


        $scope.getWaitingFeedbacks = getWaitingFeedbacks;
        $scope.getPopulatedFeedbacks = getPopulatedFeedbacks;
        $scope.getDeletedFeedbacks = getDeletedFeedbacks;
        $scope.updateFeedbackStatus = updateFeedbackStatus;

        $scope.selectAllWaiting = selectAllWaiting;
        $scope.selectAllpopulated = selectAllpopulated;
        //$scope.selectAllDeleted = selectAllDeleted;

        $scope.multiDelete = multiDelete;

        function updateFeedbackStatus(feedBackId, status, response) {
            $scope.loading = true;
            var config = {
                params: {
                    status: status + 1,
                    feedBackId: feedBackId,
                    responseContent: response

                }
            };
            apiService.del('/api/feedback/changeStatusOfFeedback', config, function (result) {

                notifyService.displaySuccess('Cập nhật thành công');
                $scope.getWaitingFeedbacks();
                $scope.loading = false;
            }, function (error) {
                notifyService.displayError('Cập nhật không thành công');
            });
        }

        function getWaitingFeedbacks(page) {
            page = page || 0;

            var config = {
                params: {
                    feedBackStatus: 0,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/feedback/getByStatus', config, function (result) {

                $scope.waitingFeedbacks = result.data.Items;
                if ($scope.waitingFeedbacks != null) {

                    $scope.waitingPage = result.data.Page;
                    $scope.waitingPagesCount = result.data.TotalPages;
                    $scope.waitingTotalCount = result.data.TotalRow;
                }

                $scope.getPopulatedFeedbacks();
            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getPopulatedFeedbacks(page) {
            page = page || 0;

            var config = {
                params: {
                    feedBackStatus: 1,
                    page: page,
                    pageSize: 20,
                    keyword: ''
                }
            }

            apiService.get('/api/feedback/getByStatus', config, function (result) {

                $scope.populatedFeedbacks = result.data.Items;

                if ($scope.populatedFeedbacks != null) {

                    $scope.populatedPage = result.data.Page;
                    $scope.populatedPagesCount = result.data.TotalPages;
                    $scope.populatedTotalCount = result.data.TotalRow;
                }

                $scope.getDeletedFeedbacks();

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getDeletedFeedbacks(page) {
            page = page || 0;

            var config = {
                params: {
                    feedBackStatus: 2,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/Feedback/getByStatus', config, function (result) {

                $scope.deletedFeedbacks = result.data.Items;

                if ($scope.deletedFeedbacks != null) {

                    $scope.deletedPage = result.data.Page;
                    $scope.deletedPagesCount = result.data.TotalPages;
                    $scope.deletedTotalCount = result.data.TotalRow;
                }
                $scope.loading = false;

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function multiDelete(status) {
            let listId = [];
            switch (status) {
                case 0:
                    $scope.currentSelected = $scope.selectedWaiting;
                    break;
                case 1:
                    $scope.currentSelected = $scope.selectedPopulated;
                    break;
                default:
                    break;
            }

            $.each($scope.currentSelected, function (i, item) {
                listId.push(item.Id);
            });
            var config = {
                params: {
                    status:2,
                    ids: JSON.stringify(listId)
                }
            };
            apiService.del('/api/feedback/multichange', config, function (result) {

                notifyService.displaySuccess('Cập nhật thành công ' + result.data + ' bản ghi.');
                $scope.getWaitingFeedbacks();

            }, function (error) {
                notifyService.displayError('Cập nhật không thành công');
            });

        }

        $scope.isAllWaiting = false;
        function selectAllWaiting() {
            if ($scope.isAllWaiting === false) {
                angular.forEach($scope.waitingFeedbacks, function (item) {
                    item.checked = true;
                });
                $scope.isAllWaiting = true;
            } else {
                angular.forEach($scope.waitingFeedbacks, function (item) {
                    item.checked = false;
                });
                $scope.isAllWaiting = false;
            }
        }

        $scope.$watch("waitingFeedbacks", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });

            if (checked != null && checked.length) {
                $scope.selectedWaiting = checked;
                $('#btnDeleteWaiting').removeAttr('disabled');
                $('#btnChangeWaiting').removeAttr('disabled');

            } else {
                $('#btnDeleteWaiting').attr('disabled', 'disabled');
                $('#btnChangeWaiting').attr('disabled', 'disabled');
            }
        }, true);

        $scope.isAllPopulated = false;
        function selectAllpopulated() {
            if ($scope.isAllPopulated === false) {
                angular.forEach($scope.populatedFeedbacks, function (item) {
                    item.checked = true;
                });
                $scope.isAllPopulated = true;
            } else {
                angular.forEach($scope.populatedFeedbacks, function (item) {
                    item.checked = false;
                });
                $scope.isAllPopulated = false;
            }
        }

        $scope.$watch("populatedFeedbacks", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });

            if (checked != null && checked.length) {
                $scope.selectedPopulated = checked;
                $('#btnDeletePopulated').removeAttr('disabled');


            } else {
                $('#btnDeletePopulated').attr('disabled', 'disabled');
            }
        }, true);







        $scope.getWaitingFeedbacks();
    }
})(angular.module('khoideptraishop.feedbacks'));