using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Data.Repositories;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Service
{
    public interface ICommonService
    {
      
    }
    public class CommonService : ICommonService
    {
        IUnitOfWork _unitOfWork;
        public CommonService( IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
      
    }
}
