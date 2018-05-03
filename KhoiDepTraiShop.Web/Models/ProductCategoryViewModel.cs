using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel()
        {
            Products = new List<ProductViewModel>();
        }
        public int Id { set; get; }       
        public string Name { set; get; }
        public string Alias { set; get; }
        public string Description { set; get; }
        public int? ParentId { set; get; }
        public int? DisplayOrder { set; get; }
        public string Image { set; get; }
        public bool? HomeFlag { set; get; }
        public  IList<ProductViewModel> Products { set; get; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }        
        public string UpdatedBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
    }
    public static class ProductCategoryViewModelEmm
    {
        public static ProductCategoryViewModel ToModel(this ProductCategory entity )
        {
            var model = new ProductCategoryViewModel();
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Alias = entity.Alias;
            model.ParentId = entity.ParentId;
            model.DisplayOrder = entity.DisplayOrder;
            model.Image = entity.Image;
            model.HomeFlag = entity.HomeFlag;
            model.CreatedDate = entity.CreatedDate;
            model.CreatedBy = entity.CreatedBy;
            model.UpdatedDate = entity.UpdatedDate;
            model.UpdatedBy = entity.UpdatedBy;
            model.MetaKeyword = entity.MetaKeyword;
            model.MetaDescription = entity.MetaDescription;
            model.Status = entity.Status;
            model.Products = entity.Products.ToModelList();
            return model;
        }
        public static List<ProductCategoryViewModel> ToModelList (this IEnumerable<ProductCategory> entites)
        {
            var vm = new List<ProductCategoryViewModel>();
            vm.AddRange(entites?.Select(p => p.ToModel()));
            return vm;
        }
    }
}