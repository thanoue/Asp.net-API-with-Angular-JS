using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public IList<ProductViewModel> HotProducts { get; set; }
        public IList<ProductViewModel> RandomProducts { get; set; }
        public IList<ProductViewModel> CheapVegetableProducts { get; set; }
        public IList<ProductViewModel> HighViewCountProducts { get; set; }
    }
}