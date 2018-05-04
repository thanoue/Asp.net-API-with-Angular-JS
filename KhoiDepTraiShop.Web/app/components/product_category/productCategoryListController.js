(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService','notifyService'];

    function productCategoryListController($scope, apiService, notifyService) {
        $scope.productCategories = [];

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCagories = getProductCagories;
        $scope.keyword = '';
        $scope.search = search;

        function search() {
            getProductCagories();
        };

        function getProductCagories(page) {
            page = page || 0;

            var config = {
                params: {
                   
                    page: page,
                    pageSize: 2,
                    keyword: $scope.keyword
                }
            }

            apiService.get('/api/productcategory/getall', config, function (result) {
                if ($scope.keyword != '') {
                    if (result.data.TotalRow <= 0) {
                        notifyService.displayWarning('Không tìm thấy loại sản phẩm nào!!');
                    }
                    else {
                        notifyService.displaySuccess('Tìm thấy ' + result.data.TotalRow + ' loại sản phẩm !!');
                    }
                }                
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalRow;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getProductCagories();
    }
})(angular.module('khoideptraishop.product_categories'));