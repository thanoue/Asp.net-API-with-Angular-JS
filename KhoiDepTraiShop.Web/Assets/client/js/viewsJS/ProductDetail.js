var common = {
    init: function () {
        common.registerEvents();
        jQuery('<div class="quantity-nav"><div class="quantity-button quantity-up">+</div><div class="quantity-button quantity-down">-</div></div>').insertAfter('.quantity input');
        jQuery('.quantity').each(function () {
            var spinner = jQuery(this),
                input = spinner.find('input[type="number"]'),
                btnUp = spinner.find('.quantity-up'),
                btnDown = spinner.find('.quantity-down'),
                min = input.attr('min'),
                max = input.attr('max');

            btnUp.click(function () {
                var oldValue = parseFloat(input.val());
                if (oldValue >= max) {
                    var newVal = oldValue;
                } else {
                    var newVal = oldValue + 1;
                }
                spinner.find("input").val(newVal);
                spinner.find("input").trigger("change");
            });

            btnDown.click(function () {
                var oldValue = parseFloat(input.val());
                if (oldValue <= min) {
                    var newVal = oldValue;
                } else {
                    var newVal = oldValue - 1;
                }
                spinner.find("input").val(newVal);
                spinner.find("input").trigger("change");
            });

        });
    },
    registerEvents: function () {
        $('#showRatingBox').click(function () {
            //TODO Check if bought here
            toastr.info("Bạn chỉ được nhận xét sau khi đã mua hàng", "Thông báo");
            $('#loadingimage').show();
            let productId = $('#productId').val();
            $.ajax({
                type: 'GET',
                data: {
                    productId: parseInt(productId)
                },
                url: '/Popup/ProductRatingBoxPopup',
                success: function (result) {
                    $('#productRatingPopup').html(result);
                    $('#ratingModal').modal({
                        keyboard: false,
                        backdrop: 'static',
                    });
                    $('#loadingimage').hide();

                }
            });
        });

    
    }
}
common.init();