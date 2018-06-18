using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class CategoryDetailViewModel
    {
        public CategoryDetailViewModel()
        {
            PaginationSet = new PaginationSet<ProductViewModel>();
            RelativeProducts = new List<ProductViewModel>();
            HotProducts = new List<ProductViewModel>();
          

        }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public PaginationSet<ProductViewModel> PaginationSet  {get;set;}
        [UIHint(TemplateConst.SpecialProductListPartial)]
        public IList<ProductViewModel> HotProducts { get; set; }
        public IList<ProductViewModel> RelativeProducts { get; set; }
        
    }
}