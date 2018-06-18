using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public ContactDetailViewModel() { }
     

        public int Id { get; set; }

    
        public string Name { get; set; }
     
        public string Phone { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }
        public string Others { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
        public bool Status { get; set; }

    }
    public static class ContactDetailViewModelEmm
    {
        public static ContactDetailViewModel ToViewModel(this ContactDetail  entity)
        {
            var vm = new ContactDetailViewModel()
            {
                Address = entity.Address,
                Email = entity.Email,
                Id = entity.Id,
                Name = entity.Name,
                Lat = entity.Lat,
                Lng = entity.Lng,
                Others = entity.Others,
                Phone = entity.Phone,
                Status = entity.Status,
                Website = entity.Website
            };
            return vm;
        }

        public static List<ContactDetailViewModel> ToViewModelList(this IList<ContactDetail> entities)
        {
            var vm = new List<ContactDetailViewModel>();
            vm.AddRange(entities.Select(p => p.ToViewModel()));
            return vm;
        }

        public static ContactDetail ToEntity (this ContactDetailViewModel entity)
        {
            var vm = new ContactDetail()
            {
                Address = entity.Address,
                Email = entity.Email,
                Id = entity.Id,
                Name = entity.Name,
                Lat = entity.Lat,
                Lng = entity.Lng,
                Others = entity.Others,
                Phone = entity.Phone,
                Status = entity.Status,
                Website = entity.Website
            };
            return vm;
        }

        public static List<ContactDetail> ToViewModelList(this IList<ContactDetailViewModel> entities)
        {
            var vm = new List<ContactDetail>();
            vm.AddRange(entities.Select(p => p.ToEntity()));
            return vm;
        }
    }
}