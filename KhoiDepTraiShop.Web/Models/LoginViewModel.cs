using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class LoginViewModel
    {
        public LoginViewModel() { }
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [MinLength(6, ErrorMessage = "Tên đăng nhập không được ít hơn 6 ký tự")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập tối đa là 50 ký tự")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [MinLength(8, ErrorMessage = "Mật khẩu không được ít hơn 8 ký tự")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }

        public string CridentialCode { get; set; } = "";
    }
}