using KhoiDepTraiShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using KhoiDepTraiShop.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;
using KhoiDepTraiShop.Web.Models;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class OrderController : BaseController
    {
        ICommonService _commonService;
        IProductService _productService;
        IProductRatingService _productRatingService;
        private ApplicationUserManager _userManager;
        IProductCategodyService _productCategoryService;
        public OrderController(ICommonService commonService, IProductService productService, IProductRatingService productratingService, IProductCategodyService productCategodyService, ApplicationUserManager userManager) : base(commonService)
        {
            _commonService = commonService;
            _productRatingService = productratingService;
            _productService = productService;
            _productCategoryService = productCategodyService;
            _userManager = userManager;

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {

                _userManager = value;
            }
        }

        public JsonResult SetCartList(IList<CartItemViewModel> cartItemViewModels)
        {
            try
            {
                m_CartItemList = cartItemViewModels;
                return GetSuccessResult();
            }
            catch (Exception)
            {
                return GetFailResult();
            }
        }
        // GET: Order

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult >CheckOut()
        {
            var vm = new CheckOutViewModel();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                vm.CurrentUser = user.ToViewModel();
            }
            vm.CartItemViewModels = m_CartItemList;
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckOut(CheckOutViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            return View();
        }




    }
}