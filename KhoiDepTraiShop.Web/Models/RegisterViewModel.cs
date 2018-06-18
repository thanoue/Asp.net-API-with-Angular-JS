using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Tên là bắt buộc")]
        [MinLength(10, ErrorMessage = "Tên không được ít hơn 10 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [MinLength(6,ErrorMessage ="Tên đăng nhập không được ít hơn 6 ký tự")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập tối đa là 50 ký tự")]
        public string UserName { set; get; }

        [Required(ErrorMessage ="Mật khẩu là bắt buộc")]
        [MinLength(8, ErrorMessage = "Mật khẩu không được ít hơn 8 ký tự")]       
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không đúng")]
        public string RetypePassword { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [Email]
        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        public  string  Email { get; set; }
            
        public string  Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string PhoneNumber { get; set; }



    }
}