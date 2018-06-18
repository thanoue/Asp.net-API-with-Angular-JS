using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KhoiDepTraiShop.Web.Commons;

namespace KhoiDepTraiShop.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            HotProducts = new List<ProductViewModel>();
            HighViewCountProducts = new List<ProductViewModel>();
            RandomProducts = new List<ProductViewModel>();
        }
        [UIHint(TemplateConst.SpecialProductListPartial)]
        public IList<ProductViewModel> HotProducts { get; set; }
        public IList<ProductViewModel> RandomProducts { get; set; }
        public IList<ProductViewModel> CheapVegetableProducts { get; set; }
        public IList<ProductViewModel> HighViewCountProducts { get; set; }
    }
}