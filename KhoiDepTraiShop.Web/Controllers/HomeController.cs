using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        IProductCategodyService _productCategoryService;
        IProductRatingService _productRatingService;
        IProductService _productService;
        ICommonService _commonService;

        public HomeController(IProductCategodyService productCategodyService, ICommonService commonService,IProductService productService,IProductRatingService productRatingService):base(commonService)
        {
            _productCategoryService = productCategodyService;
            _commonService = commonService;
            _productService = productService;
            _productRatingService = productRatingService;
        }

        public ActionResult Index()
        {
            var vm = new HomeViewModel();
            
            //hot pr
            var hotProducts = _productService.GetAll().ToList().Where(p => p.HotFlag == true).ToList();
            vm.HotProducts = hotProducts.ToModelList(_productRatingService,_productService.GetMaxProductId());      

            //random pr
            var randomPr = _productService.GetAll().OrderBy(r => Guid.NewGuid()).Take(9);
            vm.RandomProducts = randomPr.ToModelList(_productRatingService,_productService.GetMaxProductId());

            var vegetableCategoryList = new List<int>();vegetableCategoryList.Add(1);vegetableCategoryList.Add(2);
            var vegetableProduct = _productService.GetAllByCategoryIds(vegetableCategoryList).OrderBy(r => Guid.NewGuid()).Take(6); 

            vm.CheapVegetableProducts = vegetableProduct.ToModelList(_productRatingService,_productService.GetMaxProductId());

            //high viewCount
            var highViewCountPr = _productService.GetHighViewCountProducts(14500).OrderBy(r => Guid.NewGuid()).Take(6); 
            vm.HighViewCountProducts = highViewCountPr.ToModelList(_productRatingService,_productService.GetMaxProductId());
            return View(vm);
        }

        public ActionResult Categories()
        {
            var model = _productCategoryService.GetAll().ToList();
            var vm = model.ToModelList();
            return PartialView(PartialConstCommon.CategoriesPartial,vm);
        }
    }
}