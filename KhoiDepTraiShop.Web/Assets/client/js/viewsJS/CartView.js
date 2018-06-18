$(document).ready(function () {

    $('body').on('click', '#viewCartbn', openCart);
    $('body').on('click', '.btn-delete-cart-item', deleteCartItem);
    $('body').on('click', '#btnSaveChange', saveRemovingChange);
    $('body').on('hidden.bs.modal', '#cartModal', closeModal);
    $('body').on('click', '.btn-add-to-cart', addIemToCart);
    $('body').on('click', '#goToCheckOutBtn', checkOut);

});

var deleteCount = 0; var removeProductIdList = [];

let deleteCartItem = function () {
    if (deleteCount == 0) {
        $('#btnSaveChange').removeClass('hidden');
        $('.saveChange').removeClass('hidden');
        $('.cancelChange').removeClass('hidden');
    }
    deleteCount++;
    let $select = $(this).closest('tr');
    $(this).addClass('deleted');
    removeProductIdList.push(JSON.parse($(this).data('item-id')));
    $select
        .children('td, th')
        .animate({
            padding: 0
        })
        .wrapInner('<div />')
        .children()
        .slideUp(function () {
        });
}

let closeModal = function () {
    deleteCount = 0; removeProductIdList = [];
}

let saveRemovingChange = function () {
    let cart = localStorage.getItem('ShoppingCart');
    let cartList = JSON.parse(cart);
    removeProductIdList.forEach(function (item) {
        for (let i = 0; i < cartList.length; i++) {
            if (cartList[i].ProductId == item)
                cartList.splice(i, 1);
        }
    });
    localStorage.setItem('ShoppingCart', JSON.stringify(cartList));
    toastr.success('Xóa thành công ' + deleteCount.toString() + ' Sản phẩm trong giỏ');
    deleteCount = 0; removeProductIdList = [];
    $(this).addClass('hidden');
}



let openCart = function () {
    $('#smallLoading').show();
    let cart = localStorage.getItem('ShoppingCart');
    let cartList = JSON.parse(cart);
    console.log(cartList);

    $.ajax({
        type: 'POST',
        data: {
            cartItemViewModels: cartList
        },
        url: '/Popup/CartViewPopup',
        success: function (result) {
            $('#cartPopup').html(result);
            $('#cartModal').modal({
                keyboard: false,
                backdrop: 'static',
            });
            $('#smallLoading').hide();

        }
    });
}

let checkOut = function () {
    let cart = localStorage.getItem('ShoppingCart');
    let cartList = JSON.parse(cart);

    $.ajax({
        type: 'POST',
        data: {
            cartItemViewModels: cartList
        },
        url: '/Order/SetCartList',
        success: function (result) {
            if (result.isSuccess)
                window.location = '/chitiet-giohang';
        }
    });


}

let addIemToCart = function () {
    let quantity = $('#product-detail').find('.quantity-field:first').val();

    let productId = $(this).data('item-id');

    $.ajax({
        type: 'GET',
        data: {
            productId: productId,
            quantity: quantity
        },
        url: '/Product/AddProductToCart',
        success: function (result) {
            if (!result.isSuccess)
                return;
            let cart = localStorage.getItem('ShoppingCart');
            let currentCart = [];

            if (cart == null) {
                currentCart.push(result.obj);
            }
            else {
                let hasAready = false;
                currentCart = JSON.parse(cart);
                for (let i = 0; i < currentCart.length; i++) {
                    if (currentCart[i].ProductId == result.obj.ProductId) {
                        currentCart[i].Quantity += result.obj.Quantity;
                        hasAready = true;
                    }

                }
                if (!hasAready)
                    currentCart.push(result.obj);
            }
            localStorage.setItem('ShoppingCart', JSON.stringify(currentCart));
            console.log(JSON.parse(localStorage.getItem('ShoppingCart')));
            toastr.success('Bạn đã thêm một sản phẩm vào giỏ hàng', 'Thành công');

        }
    })
}