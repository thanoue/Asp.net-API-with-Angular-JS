using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class PopupController : BaseController
    {
        ICommonService _commonService;
        IProductService _productService;
        IProductRatingService _productRatingService;
        IProductCategodyService _productCategoryService;
        ITagService _tagService;
        public PopupController(ICommonService commonService, IProductService productService, IProductRatingService productratingService, IProductCategodyService productCategodyService, ITagService tagService) : base(commonService)
        {
            _commonService = commonService;
            _productRatingService = productratingService;
            _productService = productService;
            _productCategoryService = productCategodyService;
            _tagService = tagService;
        }

        // GET: Popup
        public ActionResult Index()
        {
            return View();
        }

     

        public ActionResult CartViewPopup(IList<CartItemViewModel> cartItemViewModels)
        {
            if (cartItemViewModels == null || cartItemViewModels[0] == null)
                cartItemViewModels = new List<CartItemViewModel>();            
            return PartialView(cartItemViewModels);
        }
    }
}