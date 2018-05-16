(function (app) {
    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', 'apiService', 'notifyService', '$ngBootbox', '$filter']
    function productListController($scope,apiService,notifyService,$ngBootbox,$filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteProduct = deleteProduct;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.Id);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.del('api/product/deletemulti', config, function (result) {
                notifyService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notifyService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/product/delete', config, function () {
                    notifyService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notifyService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getProducts();
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notifyService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalRow
            }, function () {
                console.log('Load product failed.');
            });
        }

        $scope.getProducts();

    }
})(angular.module('khoideptraishop.products'));