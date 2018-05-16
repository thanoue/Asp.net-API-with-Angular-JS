/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('khoideptraishop.products', ['khoideptraishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('products', {
            url: "/products",
            parent:"base",
            templateUrl: "/app/components/product/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            parent: "base",
            templateUrl: "/app/components/product/productAddView.html",
            controller: "productAddController"
        }).state('product_edit', {
            url: "/product_edit/:id",
            parent: "base",
            templateUrl: "/app/components/product/productEditView.html",
            controller: "productEditController"
        });
    }
})();