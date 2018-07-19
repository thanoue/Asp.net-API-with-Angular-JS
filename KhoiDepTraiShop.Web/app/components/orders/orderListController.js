(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', 'apiService', 'notifyService', '$ngBootbox', '$filter'];

    function orderListController($scope, apiService, notifyService, $ngBootbox, $filter) {
        $scope.orders = [];
        $scope.sendingOrders = [];
        $scope.handlingOrders = [];
        $scope.deliveringOrders = [];
        $scope.receivedOrders = [];
        $scope.canceledOrders = [];

        $scope.totalPage = 0;
        $scope.totalPagesCount = 0;
        $scope.totalTotalCount = 0;

        $scope.sendingPage = 0;
        $scope.sendingPagesCount = 0;
        $scope.sendingTotalCount = 0;

        $scope.handlingPage = 0;
        $scope.handlingTotalCount = 0;
        $scope.handlingPagesCount = 0;

        $scope.deliveringPage = 0;
        $scope.deliveringTotalCount = 0;
        $scope.deliveringPagesCount = 0;

        $scope.receivedPage = 0;
        $scope.receivedTotalCount = 0;
        $scope.receivedPagesCount = 0;

        $scope.canceledPage = 0;
        $scope.canceledTotalCount = 0;
        $scope.canceledPagesCount = 0;

        $scope.deleteSingleOrder = deleteSingleOrder;
        $scope.getOrders = getOrders;
        $scope.getSendingOrder = getSendingOrder;
        $scope.getHandlingOrders = getHandlingOrders;
        $scope.getDeliveringOrders = getDeliveringOrders;
        $scope.getReceivedOrders = getReceivedOrders;
        $scope.getCanceledOrders = getCanceledOrders;
        $scope.updateOrderStatus = updateOrderStatus;
        $scope.cancelSingleOrder = cancelSingleOrder;


        
        function deleteSingleOrder(Id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        orderId: Id
                    }
                }
                apiService.del('api/order/singleDelete', config, function () {
                    notifyService.displaySuccess('Xóa thành công');
                    $scope.getOrders();
                }, function () {
                    notifyService.displayError('Xóa không thành công');
                })
            });
        }

        function getOrders(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 20,
                    keyword: ''
                }
            }

            apiService.get('/api/order/getall', config, function (result) {
              
                $scope.orders = result.data.Items;
                console.log('orders',$scope.orders);
                for (let i = 0; i < $scope.orders.length; i++) {
                    switch ($scope.orders[i].Status) {
                        case 0:
                            $scope.orders[i].badgeWidth = { "width": '30%' }
                            break;
                        case 1:
                            $scope.orders[i].badgeWidth = { "width": '50%' }
                            break;
                        case 2:
                            $scope.orders[i].badgeWidth = { "width": '70%' }
                            break;
                        case 3:
                            $scope.orders[i].badgeWidth = { "width": '100%' }
                            break;
                        case 4:
                            $scope.orders[i].badgeWidth = { "width": '0%' }
                            break;

                    }
                }
                $scope.totalPage = result.data.Page;
                $scope.totalPagesCount = result.data.TotalPages;
                $scope.totalTotalCount = result.data.TotalRow;

                $scope.getSendingOrder();
            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getSendingOrder(page) {
            page = page || 0;

            var config = {
                params: {
                    orderStatusType : 0,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/order/getByStatus', config, function (result) {
              
                $scope.sendingOrders = result.data.Items;
                console.log($scope.sendingOrders);
              
                $scope.sendingPage = result.data.Page;
                $scope.sendingPagesCount = result.data.TotalPages;
                $scope.sendingTotalCount = result.data.TotalRow;

                $scope.getHandlingOrders();
               
            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getHandlingOrders(page) {
            page = page || 0;

            var config = {
                params: {
                    orderStatusType: 1,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/order/getByStatus', config, function (result) {
             
                $scope.handlingOrders = result.data.Items;            

                $scope.handlingPage = result.data.Page;
                $scope.handlingPagesCount = result.data.TotalPages;
                $scope.handlingTotalCount = result.data.TotalRow;

                $scope.getDeliveringOrders();

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getDeliveringOrders(page) {
            page = page || 0;

            var config = {
                params: {
                    orderStatusType: 2,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/order/getByStatus', config, function (result) {
             
                $scope.deliveringOrders = result.data.Items;


                $scope.deliveringPage = result.data.Page;
                $scope.deliveringPagesCount = result.data.TotalPages;
                $scope.deliveringTotalCount = result.data.TotalRow;

                $scope.getReceivedOrders();

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getReceivedOrders(page) {
            page = page || 0;

            var config = {
                params: {
                    orderStatusType: 3,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/order/getByStatus', config, function (result) {
        
                $scope.receivedOrders = result.data.Items;


                $scope.receivedPage = result.data.Page;
                $scope.receivedPagesCount = result.data.TotalPages;
                $scope.receivedTotalCount = result.data.TotalRow;

                $scope.getCanceledOrders();

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function getCanceledOrders(page) {
            page = page || 0;

            var config = {
                params: {
                    orderStatusType: 4,
                    page: page,
                    pageSize: 20,
                    keyword: ' '
                }
            }

            apiService.get('/api/order/getByStatus', config, function (result) {
                
                $scope.canceledOrders = result.data.Items;


                $scope.canceledPage = result.data.Page;
                $scope.canceledPagesCount = result.data.TotalPages;
                $scope.canceledTotalCount = result.data.TotalRow;

            }, function () {
                console.log('Load Orders failed.');
            });
        }

        function updateOrderStatus(Id,Status) {
            console.log('id', Id);
            var config = {
                params: {
                    destStatus: Status +1,
                    orderId: Id
                }
            }
            apiService.get('api/order/changeStatusOfOrder', config,
              function (result) {
                  notifyService.displaySuccess('Cập nhật thành công.');
                  getOrders();

              }, function (error) {
                  notifyService.displayError('Cập nhật không thành công.');
              });
        }

        function cancelSingleOrder(Id) {
            console.log('id', Id);
            var config = {
                params: {
                    destStatus: 4,
                    orderId: Id
                }
            }
            apiService.get('api/order/changeStatusOfOrder', config,
              function (result) {
                  notifyService.displaySuccess('Hủy đơn hàng thành công!');
                  getOrders();

              }, function (error) {
                  notifyService.displayError('Cập nhật không thành công.');
              });
        }



     
  
        $scope.getOrders();
    }
})(angular.module('khoideptraishop.order'));