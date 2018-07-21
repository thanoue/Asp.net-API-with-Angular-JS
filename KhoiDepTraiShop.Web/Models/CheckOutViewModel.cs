using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models.RazorTemplateModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class CheckOutViewModel
    {
        public CheckOutViewModel()
        {
            CartItemViewModels = new List<CartItemViewModel>();
            CurrentUser = new UserViewModel();
            Address_District = new SelectViewItemViewModel();
         
            Address_Province = new SelectViewItemViewModel();
            Address_Ward = new SelectViewItemViewModel();
            Address_Province.DefaultOption = "Chọn tỉnh thành";
        }
        public IList<CartItemViewModel> CartItemViewModels { get; set; }

        public UserViewModel CurrentUser { get; set; }

        public string CustomerMessage {get;set;}

        [UIHint(TemplateConst.DropDownBox)]
        public SelectViewItemViewModel Address_Province { get; set; }
        [UIHint(TemplateConst.DropDownBox)]
        public SelectViewItemViewModel Address_District { set; get; }
        [UIHint(TemplateConst.DropDownBox)]
        public SelectViewItemViewModel Address_Ward { set; get; }
    }
}