/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('khoideptraishop.product_categories', ['khoideptraishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('product_categories', {
            url: "/product_categories",
            templateUrl: "/app/components/product_category/productCategoryListView.html",
            controller: "productCategoryListController"
        });
    }
})();