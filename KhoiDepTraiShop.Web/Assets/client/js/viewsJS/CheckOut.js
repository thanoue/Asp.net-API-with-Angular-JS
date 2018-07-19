var common = {

    init: function () {
        common.registerEvents();

    },
    registerEvents: function () {

        let getAddress = function () {

        }

        let currentAddress = '';
        let currentProvince = '';
        let currentDistrict = '';
        let currentWard = '';

        $('.quantity-field').change(function () {

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


            $('.saveChange').removeClass('hidden');
            $('.cancelChange').removeClass('hidden');


        });

        $('.cancelChange').click(function () {
            location.reload();

        });

        $('.saveChange').click(function () {
            let cart = localStorage.getItem('ShoppingCart');
            let cartList = JSON.parse(cart);

            $('.timetable_sub').find('.btn-remove-cartItem').each(function () {
                for (let i = 0; i < cartList.length; i++) {

                    if (cartList[i].ProductId == parseInt($(this).data('item-id'))) {
                        if ($(this).hasClass('deleted')) {
                            cartList.splice(i, 1);
                        }
                        else {
                            let quantity = $(this).closest('tr').find('.quantity-field').val();

                            cartList[i].Quantity.Value = parseInt(quantity);
                        }
                    }
                }
            });

            localStorage.setItem('ShoppingCart', JSON.stringify(cartList));
            toastr.success('Thông tin giỏ hàng đã được cập nhật!');
            $('.submit').removeClass('hidden');
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

        $('#btnShowCheckoutbtn').click(function () {
            $('#captchaArea').removeClass('hidden');
            $('.saveChangeOrder').removeClass('hidden');
            $(this).addClass('hidden');
            return;
        });


        $('.btn-remove-cartItem').click(function () {
            $('.saveChange').removeClass('hidden');
            $('.cancelChange').removeClass('hidden');

            let $select = $(this).closest('tr');
            $(this).addClass('deleted');
            $select
                .children('td, th')
                .animate({
                    padding: 0
                })
                .wrapInner('<div />')
                .children()
                .slideUp(function () {
                });
        })

        $('#address_Province').change(function () {
            let provinceId = $(this).val();
            let province = $("#address_Province option:selected").text();
            $.ajax({
                type: 'GET',
                data: {
                    provinceId: provinceId
                },
                url: '/Order/ChangeProvince',
                success: function (result) {
                    $('#districtArea').html(result);
                    if (provinceId != undefined)
                        currentProvince = province;
                    else
                        currentProvince = '';

                }
            });
        });

        $('body').delegate('#Address_District', 'change', function () {
            let districtId = $(this).val();
            let district = $("#Address_District option:selected").text();
            $.ajax({
                type: 'GET',
                data: {
                    districtId: districtId
                },
                url: '/Order/ChangeDistrict',
                success: function (result) {
                    $('#wardArea').html(result);
                    if (districtId != undefined)
                        currentDistrict = district;
                    else
                        currentDistrict = '';
                }
            });
        });

        $('body').delegate('#Address_Ward', 'change', function () {
            let wardId = $(this).val();
            let ward = $("#Address_Ward option:selected").text();

            if (wardId != undefined) {
                currentWard = ward;
            }
            else
                currentWard = '';
            

        });

        $('body').on('change', '#StreetAndNumber', function (e) {
            if ($(this).val() == '' || currentWard.length <= 0 || currentProvince.length <= 0 || currentDistrict.length <= 0) {
                $('#btnShowCheckoutbtn').addClass('hidden');
                return;
            }



            $('#btnShowCheckoutbtn').removeClass('hidden');
            $('#address').val($(this).val() + ', ' + currentWard + ', ' + currentDistrict + ', ' + currentProvince);
            console.log($('#address').val());
        });
    }
}
common.init();