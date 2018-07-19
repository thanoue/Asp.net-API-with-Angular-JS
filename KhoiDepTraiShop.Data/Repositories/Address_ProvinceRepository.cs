using KhoiDepTraiShop.Common.ViewModels;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IAddress_ProvinceRepository : IRepository<AddressProvince>
    {       
        List<AddressProvince> GetAll();

    
    }
    public class Address_ProvinceRepository : RepositoryBase<AddressProvince>, IAddress_ProvinceRepository
    {

        public Address_ProvinceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<AddressProvince> GetAll()
        {
            return DbContext.AddressProvinces.ToList();
        }

    }
}
