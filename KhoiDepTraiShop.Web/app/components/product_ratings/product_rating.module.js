(function () {
    angular.module('khoideptraishop.product_rating', ['khoideptraishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('product_ratings', {
            url: "/product_ratings",
            templateUrl: "/app/components/product_ratings/product_ratingListView.html",
            parent: "base",
            controller: "product_ratingListController"
        });
       
    }
})();