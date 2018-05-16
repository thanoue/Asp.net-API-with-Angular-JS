
(function () {
    angular.module('khoideptraishop.product_categories', ['khoideptraishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('product_categories', {
            url: "/product_categories",
            templateUrl: "/app/components/product_category/productCategoryListView.html",
            controller: "productCategoryListController"
        }).state('add_product_category', {
            url: "/add_product_category",
            templateUrl: "/app/components/product_category/productCategoryAddView.html",
            controller: "productCategoryAddController"
        }).state('edit_product_category', {
            url: "/edit_product_category/:id",
            templateUrl: "/app/components/product_category/productCategoryEditView.html",
            controller: "productCategoryEditController"
        });
    }
})();