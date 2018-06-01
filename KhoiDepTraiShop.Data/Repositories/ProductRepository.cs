using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Data.Infrastructure;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IProductRepository :IRepository<Product>
    {
        IEnumerable<Product> GetAllByTag(string tag,int pageindex, int pagesize, out int totalRow);

        IEnumerable<Product> GetFilterByPrice(decimal min, decimal max, int? categoryId);
    }
    public class ProductRepository : RepositoryBase<Product>,IProductRepository
    {

        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Product> GetAllByTag(string tag,int pageindex, int pagesize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.Id equals pt.ProductId
                        where pt.TagId == tag && p.Status
                        orderby p.Name
                        select p;
            totalRow = query.Count();
            query = query.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return query;
        }

        public IEnumerable<Product> GetFilterByPrice(decimal min, decimal max, int? categoryId)
        {
            var products = DbContext.Products.ToList();
            if (categoryId != null)
                products = products.Where(p => p.CategoryId == categoryId).ToList();
            return products.Where(p => p.Price >= min && p.Price <= max).ToList();
        }
    }
    
    
}
