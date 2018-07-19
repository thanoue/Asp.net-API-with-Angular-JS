using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class UserVIewModel
    {

        public UserVIewModel() { }

        public string Id { get; set; }
     

        public string FullName { get; set; }

     
        public string UserName { set; get; }

       
        public string Password { get; set; }

    
        public string RetypePassword { get; set; }

        [Email]
        public string Email { get; set; }

        public string Address { get; set; }

        public string StreetAndNumber { get; set; }
       
        public string PhoneNumber { get; set; }

    }
    public static class UserViewModelEmm
    {
        public static UserVIewModel ToViewModel(this ApplicationUser entity)
        {
            var vm = new UserVIewModel()
            {
                Id = entity.Id,
                Address = entity.Address,
                Email = entity.Email,
                FullName = entity.FullName,
                PhoneNumber = entity.PhoneNumber
            };
            return vm;
        }


    }
}