using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Data.Infrastructure;

namespace KhoiDepTraiShop.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
    
    
}
