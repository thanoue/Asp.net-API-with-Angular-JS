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
    public interface IAddress_WardRepository : IRepository<AddressWard>
    {
        List<AddressWard> GetAllByDistrict(int districtId);


    }
    public class Address_WardRepository : RepositoryBase<AddressWard>, IAddress_WardRepository
    {

        public Address_WardRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<AddressWard> GetAllByDistrict(int districtId) { 
            return DbContext.AddressWards.Where(p => p.DistrictId == districtId).ToList();
        }

    }
}
