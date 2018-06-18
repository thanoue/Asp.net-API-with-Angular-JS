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
    public interface IContactDetailService
    {
        ContactDetail GetById(int contactDetailId);
        IEnumerable<ContactDetail> GetContactDetails();
    }
    public class ContactDetailService : IContactDetailService
    {
        IUnitOfWork _unitOfWork;
        IContactDetailRepository _contactDetailRepository;
        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _contactDetailRepository = contactDetailRepository;
        }
        public ContactDetail GetById(int contactDetailId)
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Id == contactDetailId);
        }

        public IEnumerable<ContactDetail> GetContactDetails()
        {
            return _contactDetailRepository.GetMulti(x => x.Status == true).ToList();
        }
    }
}
