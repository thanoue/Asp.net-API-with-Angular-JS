using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class PasswordChangingViewModel
    {
        public PasswordChangingViewModel()
        {

        }
        [Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string RetypePassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
        [MinLength(8, ErrorMessage = "Mật khẩu không được ít hơn 8 ký tự")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Mật khẩu cũ là bắt buộc")]
        [MinLength(8, ErrorMessage = "Mật khẩu không được ít hơn 8 ký tự")]
        public string OldPassword { get; set; }
    }
    public class UserViewModel
    {

        public UserViewModel() { }

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
        public static UserViewModel ToViewModel(this ApplicationUser entity)
        {
            var vm = new UserViewModel()
            {
                Id = entity.Id,
                Address = entity.Address,
                Email = entity.Email,
                FullName = entity.FullName,
                PhoneNumber = entity.PhoneNumber,
                StreetAndNumber = entity.Address
            };
            return vm;
        }


    }
}