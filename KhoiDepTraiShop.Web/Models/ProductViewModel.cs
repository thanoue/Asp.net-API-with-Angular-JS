using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Service;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductTags = new List<ProductTagViewModel>();
        }
        public int Id { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public int CategoryId { set; get; }
        public string Image { set; get; }
        public string MoreImages { set; get; }
        [UIHint(TemplateConst.VNCurrencyDisPlay)]
        public decimal Price { set; get; }
        [UIHint(TemplateConst.VNCurrencyDisPlay)]
        public decimal? PromotionPrice { set; get; }
        public int? Warranty { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public bool NewFlag { get; set; }
        public int? ViewCount { set; get; }
        public IList<ProductTagViewModel> ProductTags { set; get; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
        public string Tags { get; set; }
        [UIHint(TemplateConst.RatingStarDisplay)]
        public int? RatingAverage { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrice { get; set; }
        

    }
    public static class ProductViewModelEmm
    {
        public static ProductViewModel ToModel(this Product productvm, int? ratingAverage =null, int? maxProductId = null)
        {
            var product = new ProductViewModel();
            product.Quantity = productvm.Quantity;
            product.Id = productvm.Id;
            product.Name = productvm.Name;
            product.Alias = productvm.Alias;
            product.CategoryId = productvm.CategoryId;
            product.Image = productvm.Image;
            product.MoreImages = productvm.MoreImages;
            product.Price = productvm.Price;
            product.PromotionPrice = productvm.PromotionPrice;
            product.Warranty = productvm.Warranty;
            product.Description = productvm.Description;
            product.Content = productvm.Content;
            product.HotFlag = productvm.HotFlag;
            product.ViewCount = productvm.ViewCount;
            product.HomeFlag = productvm.HomeFlag;
            product.CreatedDate = productvm.CreatedDate;
            product.CreatedBy = productvm.CreatedBy;
            product.UpdatedDate = productvm.UpdatedDate;
            product.UpdatedBy = productvm.UpdatedBy;
            product.MetaKeyword = productvm.MetaKeyword;
            product.MetaDescription = productvm.MetaDescription;
            product.Status = productvm.Status;
            product.ProductTags = productvm.ProductTags?.ToModelList();
            product.Tags = productvm.Tags;
            product.OriginalPrice = productvm.OriginalPrice;

            if (maxProductId != null)
                if (productvm.Id >= (maxProductId - 30))
                    product.NewFlag = true;
            product.RatingAverage = ratingAverage;
            return product;
        }
        public static Product ToEntity(this ProductViewModel productvm)
        {
            var product = new Product();
            product.Quantity = productvm.Quantity;
            product.Id = productvm.Id;
            product.Name = productvm.Name;
            product.Alias = productvm.Alias;
            product.CategoryId = productvm.CategoryId;
            product.Image = productvm.Image;
            product.MoreImages = productvm.MoreImages;
            product.Price = productvm.Price;
            product.PromotionPrice = productvm.PromotionPrice;
            product.Warranty = productvm.Warranty;
            product.Description = productvm.Description;
            product.Content = productvm.Content;
            product.HotFlag = productvm.HotFlag;
            product.ViewCount = productvm.ViewCount;
            product.HomeFlag = productvm.HomeFlag;
            product.CreatedDate = productvm.CreatedDate;
            product.CreatedBy = productvm.CreatedBy;
            product.UpdatedDate = productvm.UpdatedDate;
            product.UpdatedBy = productvm.UpdatedBy;
            product.MetaKeyword = productvm.MetaKeyword;
            product.MetaDescription = productvm.MetaDescription;
            product.Status = productvm.Status;
            product.Tags = productvm.Tags;
            product.OriginalPrice = productvm.OriginalPrice;
            return product;
        }
        public static List<ProductViewModel> ToModelList(this IEnumerable<Product> entities ,IProductRatingService productRatingService, int? maxProductId = null)
        {

            var vm = new List<ProductViewModel>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    if (productRatingService != null)
                    {
                        var ratingAvarege = productRatingService.GetRatingAverage(item.Id);
                        vm.Add(item.ToModel(ratingAvarege, maxProductId));
                    }
                    else
                        vm.Add(item.ToModel(null, maxProductId));
                }
            }
            return vm;
        }
    }
}