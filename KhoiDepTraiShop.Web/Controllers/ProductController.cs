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
using Newtonsoft.Json;

namespace KhoiDepTraiShop.Web.Controllers
{

    public class ProductController : BaseController
    {
        ICommonService _commonService;
        IProductService _productService;
        IProductRatingService _productRatingService;
        IProductCategodyService _productCategoryService;
        ITagService _tagService;
        public ProductController(ICommonService commonService,IProductService productService, IProductRatingService productratingService,IProductCategodyService productCategodyService,ITagService tagService):base(commonService)
        {
            _commonService = commonService;
            _productRatingService = productratingService;
            _productService = productService;
            _productCategoryService = productCategodyService;
            _tagService = tagService;
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

        public ActionResult ProductDetail(int productId )
        {
            var vm =new  ProductDetailViewModel();
            var Currentproduct = _productService.GetAll().Where(p => p.Id == productId).FirstOrDefault();
            var ratingAverage = _productRatingService.GetRatingAverage(Currentproduct.Id);
            vm.CurrentProduct = Currentproduct.ToModel(ratingAverage);
            vm.MoreImages = JsonConvert.DeserializeObject<List<string>>(vm.CurrentProduct.MoreImages);
            vm.RelativeProducts = _productService.GetRelativePrducts(Currentproduct).ToModelList(null);

            vm.TagViewModels = _productService.GetTagListByProductId(productId).ToList().ToModelList();
            _productService.IncreaseViewCount(productId);

            return View(vm);
        }

        [HttpGet]
        public ActionResult CategoryDetail(int categoryId)
        {
            var category = _productCategoryService.GetById(categoryId);
            var vm = new CategoryDetailViewModel();
            vm.CategoryName = category.Name;
            vm.CategoryId = category.Id;
            int pageSize = int.Parse(ConfigUtility.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetAllByCategoryPaging(categoryId, 1, pageSize, out totalRow).ToList();
            var productViewModel = productModel.ToModelList(_productRatingService, _productService.GetMaxProductId());
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigUtility.GetByKey("MaxPage")),
                Page = 1,
                TotalRow = totalRow,
                TotalPages = totalPage
            };
            vm.PaginationSet = paginationSet;

            //hot pr
            var hotProducts = _productService.GetAll().ToList().Where(p => p.HotFlag == true).ToList();
            vm.HotProducts = hotProducts.ToModelList(_productRatingService, _productService.GetMaxProductId());

            var relativeProducts = _productService.GetAll().Where(p=>p.ProductCategory.ParentId!= null && p.ProductCategory.ParentId == category.ParentId).OrderBy(r => Guid.NewGuid()).Take(9);
            vm.RelativeProducts = relativeProducts.ToModelList(_productRatingService, _productService.GetMaxProductId());

  

            return View(vm);
        }

        [HttpGet]
        public ActionResult GetCategoryProductListPaging(int categoryId,int page = 1)
        {
            int pageSize = int.Parse(ConfigUtility.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetAllByCategoryPaging(categoryId, page, pageSize, out totalRow).ToList();
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

        [HttpGet]
        public JsonResult AddProductToCart(int productId, int quantity =1)
        {
            var product = _productService.GetById(productId);
            var vm = new CartItemViewModel() {
                AddToCartTime = DateTime.Now.ToString("G"),
                ProductId = productId,
                Quantity = new Models.Commons.NumbericUpDown()
                {
                    Max = product.Quantity,
                    Min = 1,
                    Step =1,
                    Value = quantity
                }
            };
           
            vm.ProductName = product.Name;
            vm.Price = product.PromotionPrice ?? product.Price;
            vm.ProductImage = product.Image;
            return GetSuccessResult(vm);

        }

        [HttpGet]
        public ActionResult ProductListByTag(string tagId)
        {
            var totalRow = 0;
            int pageSize = Int32.Parse(ConfigUtility.GetByKey("PageSize"));
            var products = _productService.GetProductListByTag(tagId, 1, pageSize,out totalRow).ToList();
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paging = new PaginationSet<ProductViewModel>()
            {
                Items = products.ToModelList(_productRatingService, _productService.GetMaxProductId()),
                MaxPage = int.Parse(ConfigUtility.GetByKey("MaxPage")),
                Page = 1,
                TotalRow = totalRow,
                TotalPages = totalPage
            };
            var vm = new ProductListByTagViewModel();
            //hot pr
            var hotProducts = _productService.GetAll().ToList().Where(p => p.HotFlag == true).ToList();
            vm.HotProducts = hotProducts.ToModelList(_productRatingService, _productService.GetMaxProductId());

            var relativeProducts = _productService.GetAll().Where(p => p.ProductCategory.ParentId != null ).OrderBy(r => Guid.NewGuid()).Take(9);
            vm.RelativeProducts = relativeProducts.ToModelList(_productRatingService, _productService.GetMaxProductId());
            vm.PaginationSet = paging;

            vm.TagId = tagId;
            vm.TagName = _tagService.GetById(tagId).Name;
            


            return View(vm);
        }



    }
}