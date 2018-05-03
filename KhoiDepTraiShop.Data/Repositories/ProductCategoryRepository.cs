using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
        IEnumerable<ProductCategory> GetAllCategory();
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory)
           : base(dbFactory)
        {
        }

        public IEnumerable<ProductCategory> GetAllCategory()
        {
            return DbContext.ProductCategories.ToList();
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}