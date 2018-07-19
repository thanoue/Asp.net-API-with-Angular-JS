using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Web.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductRatingViewModel
    {
        public ProductRatingViewModel()
        {

        }

        public string UserId { get; set; }
        public int RatedProductId { get; set; }
        public DateTime? RatingTime { set; get; }
        public string RatingContent { set; get; }

        public string RatingTitle { set; get; }
        [UIHint(Web.Commons.TemplateConst.RatingStarDisplay)]
        public int? RatingScore { set; get; }

        public string UserName { get; set; }

        public ProductRatingStatus Status { get; set; }

    }

    public class ProductRatingShortModel
    {
        public ProductRatingShortModel()
        {

        }
        public int RatedProductId { get; set; }
        public string UserId { get; set; }

    }

    public static class ProductRatingViewModelEmm
    {
        public static async Task<ProductRatingViewModel> ToViewModel(this ProductRating entity, ApplicationUserManager userManager)
        {
            var vm = new ProductRatingViewModel()
            {
                UserId = entity.UserId,
                RatedProductId = entity.RatedProductId,
                RatingContent = entity.RatingContent,
                RatingScore = entity.RatingScore,
                RatingTime = entity.RatingTime,
                RatingTitle = entity.RatingTitle,
                Status = entity.Status
            };

            var user = await userManager.FindByIdAsync(entity.UserId);
            if (user != null)
            {
                vm.UserName = user.FullName;
                return vm;
            }
            else
                return null;

        }

        public static async Task<List<ProductRatingViewModel>> ToViewModelList(this IList<ProductRating> entities, ApplicationUserManager userManager)
        {

            List<ProductRatingViewModel> models =
                await Task<List<ProductRatingViewModel>>.Run(async () =>
                 {
                     var vm = new List<ProductRatingViewModel>();
                     int temp = 0;
                     foreach (var item in entities)
                     {
                         var rating = await item.ToViewModel(userManager);
                         if (rating != null)
                             vm.Add(rating);
                         temp++;

                     }
                     if (temp > 0)
                         return vm;
                     return null;
                 });
            return models;

        }

    }
}