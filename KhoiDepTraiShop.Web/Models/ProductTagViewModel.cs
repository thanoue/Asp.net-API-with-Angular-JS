using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductTagViewModel
    {
        public ProductTagViewModel()
        {

        }
        public int ProductId { set; get; }
        public string TagId { set; get; }

    }
    public static class ProductTagViewModelEmm
    {
        public static ProductTagViewModel   ToModel(this ProductTag entity)
        {
            var model = new ProductTagViewModel
            {
                ProductId = entity.ProductId,
                TagId = entity.TagId
            };
            return model;
        }
        public static List<ProductTagViewModel> ToModelList(this IEnumerable<ProductTag> entities)
        {
            var vm = new List<ProductTagViewModel>();
             vm.AddRange(entities?.Select(p => p.ToModel()));
            return vm;
        }
    }
}