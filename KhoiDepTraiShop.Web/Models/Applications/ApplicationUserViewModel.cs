using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models.Applications
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }
        public string FullName { set; get; }
        public DateTime? BirthDay { set; get; } = DateTime.Now.Date;
        public string BirthDayString { get; set; }
        public string Bio { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }

        public string PhoneNumber { set; get; }
        public string Address { get; set; }

        public IEnumerable<ApplicationGroupViewModel> Groups { set; get; }
    }

    public static class ApplicationUserViewModelEmm
    {
        public static ApplicationUserViewModel ToModel(this ApplicationUser entity)
        {

            var vm = new ApplicationUserViewModel()
            {
                Id = entity.Id,               
                FullName = entity.FullName,
                BirthDay = entity.BirthDay,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                UserName = entity.UserName,
                Address = entity.Address,
                BirthDayString = (Convert.ToDateTime(entity.BirthDay)).ToString("yyyy-MM-dd")
            };
            return vm;
        }

        public static IList<ApplicationUserViewModel> ToViewModelList(this IList<ApplicationUser> entities)
        {
            var vm = new List<ApplicationUserViewModel>();
            vm.AddRange(entities.Select(p => p.ToModel()));
            return vm;
        }

        public static void ToEntity(this ApplicationUserViewModel model, ApplicationUser applicationUser)
        {

            applicationUser.Address = model.Address;
            applicationUser.BirthDay = model.BirthDay;
            applicationUser.Email = model.Email;
            applicationUser.UserName = model.UserName;
            applicationUser.FullName = model.FullName;
            applicationUser.PhoneNumber = model.PhoneNumber;

        }
    }


}