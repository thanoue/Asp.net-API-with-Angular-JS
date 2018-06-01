$(document).ready(function () {
    $('body').on('click', '#applySearchByCostArea', applyPriceFilter);
    $('body').on('click', '.rating-badge', applyRatingFilter);
    $('body').on('click', '.pageIndex', choosePage);
    $('body').on('change', '.discountFilterCheckbox', applyDiscountFilter);
});
const filterType = {
    priceRange: 'Filter by Price Range',
    discountRange: 'Filter by Discount Range',
    ratingRange: 'Filter by Rating Average',
    tagRange: 'Filter by Tag Area'
};

var currentFilterType; var minDiscountFilter = 0; var maxDiscountFilter = 0;

function getCurrentCategoryId() {
    let categoryIdString = $('#categoryId').val();
    let categoryId = 0;
    if (categoryIdString == undefined)
        categoryId = -1;
    else
        categoryId = parseInt(categoryIdString);
    return categoryId;
}

function applyPriceFilter() {
    currentFilterType = filterType.priceRange;
    $('#mainPanel').empty();
    $('#loadingimage').show();

    let min = $("#slider-range").slider("values", 0);
    let max = $("#slider-range").slider("values", 1);
    $.ajax({
        type: 'GET',
        data: { min: min, max: max, categoryId: getCurrentCategoryId() },
        url: '/Product/GetFilterByPriceRangeProduct',
        success: function (result) {
            $('#loadingimage').hide();
            $('#mainPanel').html(result);

        }
    })

}

function applyRatingFilter() {
    currentFilterType = filterType.ratingRange;
    $('.rating-badge').removeClass('badge-danger');
    $('.rating-badge').addClass('badge-primary');

    $(this).removeClass('badge-primary');
    $(this).addClass('badge-danger');
}

function applyDiscountFilter() {
    currentFilterType = filterType.discountRange;

    if ($(this).prop('checked') == true) {
        $('.discountFilterCheckbox').prop('checked', false);
        $(this).prop('checked', true);
        minDiscountFilter = $(this).data('min');
        maxDiscountFilter = $(this).data('max');
        $('#mainPanel').empty();
        $('#loadingimage').show();
        $.ajax({
            type: 'GET',
            data: { minDiscount: minDiscountFilter, maxDiscount: maxDiscountFilter, categoryId: getCurrentCategoryId() },
            url: '/Product/GetFilterByDiscountRangeProduct',
            success: function (result) {
                $('#loadingimage').hide();
                $('#mainPanel').html(result);

            }
        });
    }

}

function choosePage() {

    $('#loadingimage').show();
    $('#mainPanel').empty();
    let page = $(this).data('page-index');

    switch (currentFilterType) {
        case filterType.discountRange:
            $.ajax({
                type: 'GET',
                data: { minDiscount: minDiscountFilter, maxDiscount: maxDiscountFilter, categoryId: getCurrentCategoryId(), page: page },
                url: '/Product/GetFilterByDiscountRangeProduct',
                success: function (result) {
                    $('#loadingimage').hide();
                    $('#mainPanel').html(result);
                }
            })
            break;
        case filterType.priceRange:
            let min = $("#slider-range").slider("values", 0);
            let max = $("#slider-range").slider("values", 1);
            $.ajax({
                type: 'GET',
                data: { min: min, max: max, categoryId: getCurrentCategoryId(), page: page },
                url: '/Product/GetFilterByPriceRangeProduct',
                success: function (result) {
                    $('#loadingimage').hide();
                    $('#mainPanel').html(result);
                }
            })
            break;
        default:
            break;
    }



}