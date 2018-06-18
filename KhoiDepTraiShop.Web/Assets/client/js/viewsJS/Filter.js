$(document).ready(function () {
    $('body').on('click', '#applySearchByCostArea', applyPriceFilter);
    $('body').on('click', '.rating-badge', applyRatingFilter);
    $('body').on('click', '.pageIndex', choosePage);
    $('body').on('change', '.discountFilterCheckbox', applyDiscountFilter);
    $('body').on('click', '.custom-option', productTypeChange);

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "8000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
});

const filterType = {
    priceRange: 'Filter by Price Range',
    discountRange: 'Filter by Discount Range',
    ratingRange: 'Filter by Rating Average',
    tagRange: 'Filter by Tag Area',
    none :'not chosen yet'
};

var currentFilterType = filterType.none; var minDiscountFilter = 0; var maxDiscountFilter = 0;

function productTypeChange() {
    let categporyId = $(this).data('value');
    window.location = '/danhsach-sanpham-' + categporyId;
}

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
        case filterType.none:
            $.ajax({
                type: 'GET',
                data: {categoryId: getCurrentCategoryId(), page: page },
                url: '/Product/GetCategoryProductListPaging',
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



