var common = {
    init: function () {
        common.registerEvents();

    },
    registerEvents: function () {

        $('.quantity-field').change(function () {
            $('.saveChange').removeClass('hidden');
            $('.cancelChange').removeClass('hidden');
            let total = $('.totalOfCart').text();
            total = total.substr(0, total.length - 1);

            let oldSubTotal = $(this).closest('tr').find('.subTotal').text();
            oldSubTotal = oldSubTotal.substr(0, oldSubTotal.length - 1);

            let quantity = $(this).val();

            let price = $(this).closest('tr').find('.price').text();
            price = price.substr(0, price.length - 1);

            let newValue = parseFloat(price) * quantity;

            $('.totalOfCart').text((parseFloat(total) - parseFloat(oldSubTotal) + parseFloat(newValue)).toFixed(3).toString() + '₫');
            $(this).closest('tr').find('.subTotal').text(newValue.toFixed(3).toString() + '₫');

        });

        $('.cancelChange').click(function () {
            console.log('clcked');
            location.reload();
        });

        $('.saveChange').click(function () {
            let cart = localStorage.getItem('ShoppingCart');
            let cartList = JSON.parse(cart);
            $('.timetable_sub').find('.btn-delete-cartItem').each(function () {
                for (let i = 0; i < cartList.length; i++) {
                    if (cartList[i].ProductId == parseInt($(this).data('item-id'))) {
                        if ($(this).hasClass('deleted')) {
                            console.log(cartList);
                            cartList.splice(i, 1);
                            
                        }                       
                        else {
                            let quantity = $(this).closest('tr').find('.quantity-field').val();
                            console.log(quantity);
                            cartList[i].Quantity.Value = parseInt(quantity);
                        }
                    }
                }
            });

            localStorage.setItem('ShoppingCart', JSON.stringify(cartList));
            toastr.success('Thông tin giỏ hàng đã được cập nhật!');

            $.ajax({
                type: 'POST',
                data: {
                    cartItemViewModels: cartList
                },
                url: '/Order/SetCartList',
                success: function (result) {
                    
                }
            });
        });

    }
}
common.init();