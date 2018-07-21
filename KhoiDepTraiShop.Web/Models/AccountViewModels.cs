using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KhoiDepTraiShop.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    } 
   
}
