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
            $('#loadingimage').show();
            let productId = $('#productId').val();
            $.ajax({
                type: 'GET',
                data: {
                    productId: parseInt(productId)
                },
                url: '/ProductRating/ProductRatingBoxPopup',
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

        $('#btnLoginToRate').click(function () {

            $('#closeReviewPopup').click();
            $.ajax({
                type: 'GET',
                url: '/dang-nhap',
                success: function (result) {
                    $('#loginPopup').html(result);
                    $('#signInModal').modal({
                        keyboard: false,
                        backdrop: 'static',
                    });
                }
            });
        })

        $("body").delegate("#btnLoginToRate", "click", function () {
            let productId = $('#productId').val();
            $.ajax({
                type: 'POST',
                data: { productId: productId },
                url: '/ProductRating/CheckRatingAbility',
                success: function (result) {
                    if (result.obj.logged == true) {
                        if (result.obj.rated == true)
                            toastr.warning("Bạn đã đánh giá sản phẩm này");
                        else
                            toastr.warning("Bạn chỉ có thể đánh giá những sản phẩm bạn đã mua trước đó");
                        return;
                    }
                    $('#closeReviewPopup').click();
                    $.ajax({
                        type: 'GET',
                        url: '/dang-nhap',
                        success: function (result) {
                            $('#loginPopup').html(result);
                            $('#signInModal').modal({
                                keyboard: false,
                                backdrop: 'static',
                            });
                        }
                    });
                }
            })

        });



        $('body').delegate('#btn-submitRating', 'click', function () {
            let title = $('#rating-title').val();
            let content = $('#rating-content').val();
            let ratingScore = $('input[name=rating]:checked').val();
            let productId = $('#productId').val();
            console.log('ratingscore', ratingScore);
            $.ajax({
                url: '/ProductRating/SubmitRating',
                type: 'POST',
                data: {
                    title: title,
                    content: content,
                    ratingScore: ratingScore,
                    productId: productId
                },
                success: function (result) {
                    if (!result.isSuccess) {
                        toastr.error(result.obj.Error);
                        return;
                    }
                    toastr.success("Cảm ơn đóng góp của bạn! Đánh giá của bạn sẽ được duyệt sớm");
                    $('#btnReadyToRate').click();
                    $('#btnReadyToRate').addClass('hidden');
                }
            })
        });
    }
}
common.init();