using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System.Collections.Generic;
using System.Linq;


namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IProductRatingRepository : IRepository<ProductRating>
    {
       
    }

    public class ProductRatingRepository : RepositoryBase<ProductRating>, IProductRatingRepository
    {
        public ProductRatingRepository(IDbFactory dbFactory)
           : base(dbFactory)
        {
        }


    }
}
