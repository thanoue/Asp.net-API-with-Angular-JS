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
    public interface IAddressService
    {
        List<AddressProvince> GetAllProvince();
        List<AddressDistrict> GetDistrictByProvince(int provinceId);
        List<AddressWard> GetWardByDistrict(int districtId);
    }
    public class AddressService : IAddressService
    {
        IAddress_DistrictRepository _address_DistrictRepository;
        IAddress_ProvinceRepository _address_ProvinceRepository;
        IAddress_WardRepository _address_WardRepository;
        IUnitOfWork _unitOfWork;

        public AddressService(IAddress_DistrictRepository address_DistrictRepository,
        IAddress_ProvinceRepository address_ProvinceRepository,
        IAddress_WardRepository address_WardRepository, IUnitOfWork unitOfWork)
        {
            _address_DistrictRepository = address_DistrictRepository;
            _address_ProvinceRepository = address_ProvinceRepository;
            _address_WardRepository = address_WardRepository;
            this._unitOfWork = unitOfWork;
        }

        public List<AddressProvince> GetAllProvince()
        {
            return _address_ProvinceRepository.GetAll();
        }

        public List<AddressDistrict> GetDistrictByProvince(int provinceId)
        {
            return _address_DistrictRepository.GetAllByProvince(provinceId);
        }

        public List<AddressWard> GetWardByDistrict(int districtId)
        {
            return _address_WardRepository.GetAllByDistrict(districtId);
        }
    }
}
