﻿(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService', '$scope', 'notifyService', '$state','commonService'];

    function productCategoryAddController(apiService, $scope, notifyService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true            
        }

        $scope.AddProductCategory = AddProductCategory;
        $scope.GetSeoTitle = GetSEOTitle;

        function GetSEOTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function AddProductCategory() {
            apiService.post('api/productcategory/create', $scope.productCategory,
                function (result) {
                    notifyService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('product_categories');
                }, function (error) {
                    notifyService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();
    }

})(angular.module('khoideptraishop.product_categories'));