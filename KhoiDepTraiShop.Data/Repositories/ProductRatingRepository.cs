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
        int GetRatingAverage(int productId);
    }

    public class ProductRatingRepository : RepositoryBase<ProductRating>, IProductRatingRepository
    {

      
        public ProductRatingRepository(IDbFactory dbFactory)
           : base(dbFactory)
        {
        }

        public int GetRatingAverage(int productId)
        {
            var ratingList = DbContext.ProductRatings.Where(p => p.RatedProductId == productId).ToList();
            int sum = 0;
            if (ratingList.Count() == 0)
                return 0;
            foreach (var rating in ratingList)
            {
                sum += rating.RatingScore ?? 0;
            }
            return sum / ratingList.Count();
        }

        public ProductRating GetSingle(string UserId, int productId)
        {
            var rating = DbContext.ProductRatings.Where(p => p.RatedProductId == productId && p.UserId == UserId).FirstOrDefault();
            return rating ??  null;
            
        }
    }
}
