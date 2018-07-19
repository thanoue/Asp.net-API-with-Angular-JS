using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using KhoiDepTraiShop.Web.App_Start;
namespace KhoiDepTraiShop.Web.Controllers
{
    public class ProductRatingController : BaseController
    {
        ICommonService _commonService;
        IProductService _productService;
        IProductRatingService _productRatingService;
        IProductCategodyService _productCategoryService;
        private ApplicationUserManager _userManager;

        public ProductRatingController(ICommonService commonService, IProductService productService, IProductRatingService productratingService, IProductCategodyService productCategodyService, ApplicationUserManager userManager) : base(commonService)
        {
            _commonService = commonService;
            _productRatingService = productratingService;
            _productService = productService;
            _productCategoryService = productCategodyService;
            _userManager = userManager;
        }

        public JsonResult CheckRatingAbility(int productId)
        {
            if (Request.IsAuthenticated)
            {
                if (_productRatingService.UserRatedOrNot(User.Identity.GetUserId(), productId))
                    return GetSuccessResult(new { logged = true, rated = true });
                return GetSuccessResult(new { logged = true, rated = false });
            }

            else
                return GetSuccessResult(new { logged = false });
        }

        public async Task<ActionResult> ProductRatingBoxPopup(int productId)
        {

            var vm = new ProductRatingPopupViewModel();

            if (Request.IsAuthenticated)
            {
                vm.RatingAbility = _productRatingService.RatingAblilityChecked(User.Identity.GetUserId(), productId);

            }
            else
                vm.RatingAbility = false;



            var ratings = _productRatingService.GetAllPopulatedByProduct(productId).ToList();

            var ratingsViewModels = await ratings.ToViewModelList(_userManager);
            if (ratingsViewModels != null)
            {
                vm.ProductRatingViewModel = ratingsViewModels;

                var total = 0; var notRatingTotalCount = 0;
                foreach (var item in ratingsViewModels)
                {
                    total += item.RatingScore ?? 0;

                    switch (item.RatingScore)
                    {
                        case 1:
                            vm.OneStarTotal += 1;
                            break;
                        case 2:
                            vm.TwoStarsTotal += 1;
                            break;
                        case 3:
                            vm.ThreeStarsTotal += 1;
                            break;
                        case 4:
                            vm.FourStarsTotal += 1;
                            break;
                        case 5:
                            vm.FiveStarsTotal += 1;
                            break;
                        default:
                            notRatingTotalCount += 1;
                            break;

                    }

                }
                vm.RatingTotal = ratingsViewModels.Count() - notRatingTotalCount;
                vm.RatingAverage = total / vm.RatingTotal;
                vm.OneStarTotal = (vm.OneStarTotal * 100) / vm.RatingTotal;
                vm.TwoStarsTotal = (vm.TwoStarsTotal * 100) / vm.RatingTotal;
                vm.ThreeStarsTotal = (vm.ThreeStarsTotal * 100) / vm.RatingTotal;
                vm.FourStarsTotal = (vm.FourStarsTotal * 100) / vm.RatingTotal;
                vm.FiveStarsTotal = (vm.FiveStarsTotal * 100) / vm.RatingTotal;

                return PartialView(vm);


            }
            else
                return PartialView(vm);




        }

        [HttpPost]
        public JsonResult SubmitRating(string title, string content, int ratingScore, int productId)
        {
            if (content.Length < 15)
                return GetFailResult(new { Error = "Nội dung không được ít hơn 15 ký tự" });
            if (title.Length > 30)
                return GetFailResult(new { Error = "Tiêu đề không được quá 30 ký tự" });
            var userId = User.Identity.GetUserId();
            _productRatingService.SubmitRating(productId, userId, title, content, ratingScore);
            return GetSuccessResult();

        }
        // GET: ProductRating

    }
}