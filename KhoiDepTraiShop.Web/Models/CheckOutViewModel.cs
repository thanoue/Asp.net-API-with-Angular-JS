using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class CheckOutViewModel
    {
        public CheckOutViewModel()
        {
            CartItemViewModels = new List<CartItemViewModel>();
            CurrentUser = new UserVIewModel();
        }
        public IList<CartItemViewModel> CartItemViewModels { get; set; }

        public UserVIewModel CurrentUser { get; set; }
    }
}