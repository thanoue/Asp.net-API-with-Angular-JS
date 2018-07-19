    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductRatingPopupViewModel
    {
        public ProductRatingPopupViewModel() {
            ProductRatingViewModel = new List<ProductRatingViewModel>();
        }

        public bool RatingAbility { set; get; }
        public IList<ProductRatingViewModel> ProductRatingViewModel { get; set; }

        [UIHint(Web.Commons.TemplateConst.RatingStarDisplay)]
        public int RatingAverage { get; set; } = 0;
        public int RatingTotal { get; set; } = 0;
        public int OneStarTotal { get; set; } = 0;
        public int TwoStarsTotal { get; set; } = 0;
        public int ThreeStarsTotal { get; set; } = 0;
        public int FourStarsTotal { get; set; } = 0;
        public int FiveStarsTotal { get; set; } = 0;



    }
}