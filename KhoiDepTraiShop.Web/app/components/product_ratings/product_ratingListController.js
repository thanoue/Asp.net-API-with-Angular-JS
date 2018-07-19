(function (app) {
    app.controller('product_ratingListController', product_ratingListController);

    product_ratingListController.$inject = ['$scope', 'apiService', 'notifyService', '$ngBootbox', '$filter'];

    function product_ratingListController($scope, apiService, notifyService, $ngBootbox, $filter) {
        $scope.waitingRates = [];
        $scope.populatedRates = [];
        $scope.reportedRates = [];
        $scope.deletedRates = [];
        $scope.currentSelected = {};


        $scope.waitingPage = 0;
        $scope.waitingPagesCount = 0;
        $scope.waitingTotalCount = 0;

        $scope.populatedPage = 0;
        $scope.populatedPagesCount = 0;
        $scope.populatedTotalCount = 0;

        $scope.reportedPage = 0;
        $scope.reportedPagesCount = 0;
        $scope.reportedTotalCount = 0;


        $scope.deletedPage = 0;
        $scope.deletedPagesCount = 0;
        $scope.deletedTotalCount = 0;


        $scope.getWaitingRates = getWaitingRates;
        $scope.getPopulatedRates = getPopulatedRates;
        $scope.getReportedRates = getReportedRates;
        $scope.getDeletedRates = getDeletedRates;
        $scope.updateRatingStatus = updateRatingStatus;
        $scope.selectAllWaiting = selectAllWaiting;
        $scope.selectAllpopulated = selectAllpopulated;
        $scope.selectAllReported = selectAllReported;
        $scope.multiChange = multiChange;


        $scope.isAllWaiting = false;
        function selectAllWaiting() {
            if ($scope.isAllWaiting === false) {
                angular.forEach($scope.waitingRates, function (item) {
                    item.checked = true;
                });
                $scope.isAllWaiting = true;
            } else {
                angular.forEach($scope.waitingRates, function (item) {
                    item.checked = false;
                });
                $scope.isAllWaiting = false;
            }
        }


        $scope.isAllPopulated = false;
        function selectAllpopulated() {
            if ($scope.isAllPopulated === false) {
                angular.forEach($scope.populatedRates, function (item) {
                    item.checked = true;
                });
                $scope.isAllPopulated = true;
            } else {
                angular.forEach($scope.populatedRates, function (item) {
                    item.checked = false;
                });
                $scope.isAllPopulated = false;
            }
        }

        $scope.isAllReported = false;
        function selectAllReported() {
            if ($scope.isAllReported === false) {
                angular.forEach($scope.reportedRates, function (item) {
                    item.checked = true;
                });
                $scope.isAllReported = true;
            } else {
                angular.forEach($scope.reportedRates, function (item) {
                    item.checked = false;
                });
                $scope.isAllReported = false;
            }
        }



        $scope.$watch("populatedRates", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });

            if (checked != null && checked.length) {
                $scope.selectedPopulated = checked;
                $('#btnChangePopulated').removeAttr('disabled');

            } else {
                $('#btnChangePopulated').attr('disabled', 'disabled');
            }
        }, true);




        $scope.$watch("reportedRates", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });

            if (checked != null && checked.length) {
                $scope.selectedReported = checked;
                $('#btnChangeReported').removeAttr('disabled');
            } else {
                $('#btnChangeReported').attr('disabled', 'disabled');
            }
        }, true);


        $scope.$watch("waitingRates", function (n, o) {
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

        function multiChange(status) {
            $ngBootbox.confirm('Bạn có chắc ?')
                .then(function () {
                    var listId = [];


                    switch (status) {
                        case 1:
                            $scope.currentSelected = $scope.selectedWaiting;
                            break;
                        case 2:
                            $scope.currentSelected = $scope.selectedPopulated;
                            break;
                        case 3:
                            $scope.currentSelected = $scope.selectedReported;
                            break;
                        default:
                            break;
                    }

                    $.each($scope.currentSelected, function (i, rate) {
                        let item = {
                            RatedProductId: rate.RatedProductId,
                            UserId: rate.UserId
                        };
                        listId.push(item);
                    });

                    var config = {
                        params: {
                            status: status,
                            ids: JSON.stringify(listId)
                        }
                    };
                    apiService.del('/api/rating/multichange', config, function (result) {

                        notifyService.displaySuccess('Cập nhật thành công ' + result.data + ' bản ghi.');
                        $scope.getWaitingRates();

                    }, function (error) {
                        notifyService.displayError('Cập nhật không thành công');
                    });
                });
        }

        function getWaitingRates(page) {
            page = page || 0;

            var config = {
                params: {
                    productRatingStatus: 0,
                    page: page,
                    pageSize: 20,
                    keyword: ''
                }
            }

            apiService.get('/api/rating/getByStatus', config, function (result) {

                $scope.waitingRates = result.data.Items;
                if ($scope.waitingRates != null) {
                    for (let i = 0; i < $scope.waitingRates.length; i++) {

                        switch ($scope.waitingRates[i].RatingScore) {
                            case 1:
                                $scope.waitingRates[i].progressWidth = { "width": '10%' }
                                break;
                            case 2:
                                $scope.waitingRates[i].progressWidth = { "width": '30%' }
                                break;
                            case 3:
                                $scope.waitingRates[i].progressWidth = { "width": '50%' }
                                break;
                            case 4:
                                $scope.waitingRates[i].progressWidth = { "width": '80%' }
                                break;
                            case 5:
                                $scope.waitingRates[i].progressWidth = { "width": '100%' }
                                break;

                        }
                    }

                    $scope.waitingPage = result.data.Page;
                    $scope.waitingPagesCount = result.data.TotalPages;
                    $scope.waitingTotalCount = result.data.TotalRow;
                }

                $scope.getPopulatedRates();
            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getPopulatedRates(page) {
            page = page || 0;

            var config = {
                params: {
                    productRatingStatus: 1,
                    page: page,
                    pageSize: 20,
                    keyword: ''
                }
            }

            apiService.get('/api/rating/getByStatus', config, function (result) {

                $scope.populatedRates = result.data.Items;

                if ($scope.populatedRates != null) {
                    for (let i = 0; i < $scope.populatedRates.length; i++) {

                        switch ($scope.populatedRates[i].RatingScore) {
                            case 1:
                                $scope.populatedRates[i].progressWidth = { "width": '10%' }
                                break;
                            case 2:
                                $scope.populatedRates[i].progressWidth = { "width": '30%' }
                                break;
                            case 3:
                                $scope.populatedRates[i].progressWidth = { "width": '50%' }
                                break;
                            case 4:
                                $scope.populatedRates[i].progressWidth = { "width": '80%' }
                                break;
                            case 5:
                                $scope.populatedRates[i].progressWidth = { "width": '100%' }
                                break;

                        }
                    }

                    $scope.populatedPage = result.data.Page;
                    $scope.populatedPagesCount = result.data.TotalPages;
                    $scope.populatedTotalCount = result.data.TotalRow;
                }

                $scope.getReportedRates();

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getReportedRates(page) {
            page = page || 0;

            var config = {
                params: {
                    productRatingStatus: 2,
                    page: page,
                    pageSize: 20,
                    keyword: ''
                }
            }

            apiService.get('/api/rating/getByStatus', config, function (result) {

                $scope.reportedRates = result.data.Items;

                if ($scope.reportedRates != null) {
                    for (let i = 0; i < $scope.reportedRates.length; i++) {

                        switch ($scope.reportedRates[i].RatingScore) {
                            case 1:
                                $scope.reportedRates[i].progressWidth = { "width": '10%' }
                                break;
                            case 2:
                                $scope.reportedRates[i].progressWidth = { "width": '30%' }
                                break;
                            case 3:
                                $scope.reportedRates[i].progressWidth = { "width": '50%' }
                                break;
                            case 4:
                                $scope.reportedRates[i].progressWidth = { "width": '80%' }
                                break;
                            case 5:
                                $scope.reportedRates[i].progressWidth = { "width": '100%' }
                                break;

                        }
                    }

                    $scope.reportedPage = result.data.Page;
                    $scope.reportedPagesCount = result.data.TotalPages;
                    $scope.reportedTotalCount = result.data.TotalRow;
                }

                $scope.getDeletedRates();

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getDeletedRates(page) {
            page = page || 0;

            var config = {
                params: {
                    productRatingStatus: 3,
                    page: page,
                    pageSize: 20,
                    keyword: ''
                }
            }
            apiService.get('/api/rating/getByStatus', config, function (result) {

                $scope.deletedRates = result.data.Items;

                if ($scope.deletedRates != null) {
                    for (let i = 0; i < $scope.deletedRates.length; i++) {

                        switch ($scope.deletedRates[i].RatingScore) {
                            case 1:
                                $scope.deletedRates[i].progressWidth = { "width": '10%' }
                                break;
                            case 2:
                                $scope.deletedRates[i].progressWidth = { "width": '30%' }
                                break;
                            case 3:
                                $scope.deletedRates[i].progressWidth = { "width": '50%' }
                                break;
                            case 4:
                                $scope.deletedRates[i].progressWidth = { "width": '80%' }
                                break;
                            case 5:
                                $scope.deletedRates[i].progressWidth = { "width": '100%' }
                                break;

                        }
                    }

                    $scope.deletedPage = result.data.Page;
                    $scope.deletedPagesCount = result.data.TotalPages;
                    $scope.deletedTotalCount = result.data.TotalRow;
                }




            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function updateRatingStatus(RatedProductId, Userid, Status) {

            var config = {
                params: {
                    ratingStatus: Status + 1,
                    productId: RatedProductId,
                    userid: Userid
                }
            }
            apiService.get('api/rating/changeStatusOfRating', config,
                function (result) {
                    notifyService.displaySuccess('Cập nhật thành công.');
                    $scope.getWaitingRates();

                }, function (error) {
                    notifyService.displayError('Cập nhật không thành công.');
                });
        }



        $scope.getWaitingRates();
    }
})(angular.module('khoideptraishop.product_rating'));