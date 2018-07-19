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
    public interface IAddress_DistrictRepository : IRepository<AddressDistrict>
    {       
        List<AddressDistrict> GetAllByProvince(int provinceId);

    
    }
    public class Address_DistrictRepository : RepositoryBase<AddressDistrict>, IAddress_DistrictRepository
    {

        public Address_DistrictRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<AddressDistrict> GetAllByProvince(int provinceId)
        {
            return DbContext.AddressDistricts.Where(p=>p.ProvinceId == provinceId).ToList();
        }

    }
}
