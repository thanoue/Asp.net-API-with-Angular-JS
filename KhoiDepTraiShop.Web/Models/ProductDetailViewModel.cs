using KhoiDepTraiShop.Web.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductDetailViewModel
    {
        public ProductDetailViewModel() {
            RelativeProducts = new List<ProductViewModel>();
            CurrentProduct = new ProductViewModel();
            MoreImages = new List<string>();
            TagViewModels = new List<TagViewModel>();
        }
        public ProductViewModel CurrentProduct {get;set;}

        [UIHint(TemplateConst.SpecialProductListPartial)]
        public  IList<ProductViewModel> RelativeProducts { get; set; }
        public IList<string> MoreImages { get; set; }
        public IList<TagViewModel> TagViewModels { get; set; }
    }
}