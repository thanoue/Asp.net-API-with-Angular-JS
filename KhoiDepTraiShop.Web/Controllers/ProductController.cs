using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using KhoiDepTraiShop.Web.Models;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        ICommonService _commonService;
        IProductService _productService;
        IProductRatingService _productRatingService;
        public ProductController(ICommonService commonService,IProductService productService, IProductRatingService productratingService):base(commonService)
        {
            _commonService = commonService;
            _productRatingService = productratingService;
            _productService = productService;
        }

        [HttpGet]
        public PartialViewResult GetFilterByPriceRangeProduct(decimal min,decimal max,int? categoryId, int page = 1)
        {
            if (categoryId == -1)
                categoryId = null;
          

            int pageSize = int.Parse(ConfigUtility.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetPriceFilterByPriceRangeProductPaging(min,max,categoryId, page, pageSize, out totalRow).ToList();
            var productViewModel = productModel.ToModelList(_productRatingService,_productService.GetMaxProductId());
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigUtility.GetByKey("MaxPage")),
                Page = page,
                TotalRow = totalRow,
                TotalPages = totalPage
            };

            
            return PartialView(PartialConstCommon.ProductListPartial, paginationSet);
        }

        [HttpGet]
        public PartialViewResult GetFilterByDiscountRangeProduct(int minDiscount, int maxDiscount, int? categoryId,int page=1)
        {
            if (categoryId == -1)
                categoryId = null;
            int pageSize = int.Parse(ConfigUtility.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetPriceFilterByDiscountRangeProductPaging(minDiscount, maxDiscount, categoryId, page, pageSize, out totalRow).ToList();
            var productViewModel = productModel.ToModelList(_productRatingService, _productService.GetMaxProductId());
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigUtility.GetByKey("MaxPage")),
                Page = page,
                TotalRow = totalRow,
                TotalPages = totalPage
            };


            return PartialView(PartialConstCommon.ProductListPartial, paginationSet);

        }




    }
}