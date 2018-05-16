(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notifyService', '$state','commonService','$ngBootbox'];

    function productAddController(apiService, $scope, notifyService, $state, commonService,$ngBootbox) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
        }
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.moreImages = [];
        $scope.Addproduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.ChooseImage = ChooseImage;
        $scope.DeleteImage = DeleteImage;


        function DeleteImage(img) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                let index = $scope.moreImages.indexOf(img);
                if (index > -1) {
                    $scope.moreImages.splice(index, 1);
                    notifyService.displaySuccess("Mới xóa 1 hình");
                }


            });
        }

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                if ($scope.moreImages.indexOf(fileUrl) > -1) {
                    notifyService.displayInfo("Ảnh đã tồn tại");
                    return;
                }         
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });

            }
            finder.popup();
        }

        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });         
            }
            finder.popup();
        }
        
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('api/product/create', $scope.product,
                function (result) {
                    notifyService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('products');
                }, function (error) {
                    notifyService.displayError('Thêm mới không thành công.');
                });
        }
        function loadProductCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadProductCategory();
    }
})(angular.module('khoideptraishop.products'));