(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notifyService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notifyService, $ngBootbox, $filter) {
        $scope.productCategories = [];

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCagories = getProductCagories;
        $scope.keyword = '';    

        $scope.search = search;

        $scope.deleteProductCategory = deleteProductCategory;

        $scope.multiDelete = multiDelete;

        $scope.selectAll = selectAll;

        function multiDelete() {

            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.Id);
                });
                var config = {
                    params: {
                        ids: JSON.stringify(listId)
                    }
                }
                apiService.del('api/productcategory/multidelete', config, function (result) {
                    notifyService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notifyService.displayError('Xóa không thành công');
                });
            });

          
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/productcategory/delete', config, function () {
                    notifyService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notifyService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getProductCagories();
        };

        function getProductCagories(page) {
            page = page || 0;

            var config = {
                params: {
                   
                    page: page,
                    pageSize: 20,
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