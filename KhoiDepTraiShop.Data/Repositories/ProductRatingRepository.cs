using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IProductRatingRepository : IRepository<ProductRating>
    {
        ProductRating GetSingle(string UserId, int productId);
    }

    public class ProductRatingRepository : RepositoryBase<ProductRating>, IProductRatingRepository
    {

      
        public ProductRatingRepository(IDbFactory dbFactory)
           : base(dbFactory)
        {
        }

        public ProductRating GetSingle(string UserId, int productId)
        {
            var rating = DbContext.ProductRatings.Where(p => p.RatedProductId == productId && p.UserId == UserId).FirstOrDefault();
            return rating ??  null;
            
        }
    }
}
