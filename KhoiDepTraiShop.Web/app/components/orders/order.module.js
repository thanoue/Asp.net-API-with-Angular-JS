(function () {
    angular.module('khoideptraishop.order', ['khoideptraishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('orders', {
            url: "/orders",
            templateUrl: "/app/components/orders/orderListView.html",
            parent: "base",
            controller: "orderListController"
        });
        //    .state('add_product_category', {
        //    url: "/add_product_category",
        //    templateUrl: "/app/components/product_category/productCategoryAddView.html",
        //    parent: "base",
        //    controller: "productCategoryAddController"
        //}).state('edit_product_category', {
        //    url: "/edit_product_category/:id",
        //    templateUrl: "/app/components/product_category/productCategoryEditView.html",
        //    parent: "base",
        //    controller: "productCategoryEditController"
        //});
    }
})();