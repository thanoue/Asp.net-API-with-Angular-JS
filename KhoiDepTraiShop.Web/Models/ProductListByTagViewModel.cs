using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductListByTagViewModel
    {
        public ProductListByTagViewModel()
        {
            PaginationSet = new PaginationSet<ProductViewModel>();
            RelativeProducts = new List<ProductViewModel>();
            HotProducts = new List<ProductViewModel>();
        }
        public string TagName { get; set; }
        public string TagId { get; set; }
        public PaginationSet<ProductViewModel> PaginationSet { get; set; }
        [UIHint(TemplateConst.SpecialProductListPartial)]
        public IList<ProductViewModel> HotProducts { get; set; }
        public IList<ProductViewModel> RelativeProducts { get; set; }
    }
}