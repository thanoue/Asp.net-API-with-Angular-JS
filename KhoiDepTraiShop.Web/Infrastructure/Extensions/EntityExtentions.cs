using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Infrastructure.Extensions
{
    public static class EntityExtentions
    {
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryvm)
        {
            productCategory.Id = productCategoryvm.Id;
            productCategory.Name = productCategoryvm.Name;
            productCategory.Description = productCategoryvm.Description;
            productCategory.Alias = productCategoryvm.Alias;
            productCategory.ParentId = productCategoryvm.ParentId;
            productCategory.DisplayOrder = productCategoryvm.DisplayOrder;
            productCategory.Image = productCategoryvm.Image;
            productCategory.HomeFlag = productCategoryvm.HomeFlag;
            productCategory.CreatedDate = productCategoryvm.CreatedDate;
            productCategory.CreatedBy = productCategoryvm.CreatedBy;
            productCategory.UpdatedDate = productCategoryvm.UpdatedDate;
            productCategory.UpdatedBy = productCategoryvm.UpdatedBy;
            productCategory.MetaKeyword = productCategoryvm.MetaKeyword;
            productCategory.MetaDescription = productCategoryvm.MetaDescription;
            productCategory.Status = productCategoryvm.Status;
        }
        public static void UpdateProduct(this Product product, ProductViewModel productvm)
        {
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
        }
           

    }
}