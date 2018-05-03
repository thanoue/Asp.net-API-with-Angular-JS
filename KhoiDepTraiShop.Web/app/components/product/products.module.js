/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('khoideptraishop.products', ['khoideptraishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
      
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/product/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/app/components/product/productAddView.html",
            controller: "productAddController"
        });
    }
})();