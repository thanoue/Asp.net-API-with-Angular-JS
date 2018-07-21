using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            CurrentUser = new UserViewModel();
            PasswordChangingViewModel = new PasswordChangingViewModel();
            BoughtProducts = new List<ProductViewModel>();
            OrderList = new List<OrderViewModel>();
        }

        public UserViewModel CurrentUser { get; set; }

        public IList<ProductViewModel> BoughtProducts { get; set; }

        public PasswordChangingViewModel PasswordChangingViewModel { get; set; }

        public IList<OrderViewModel> OrderList { get; set; } 
    }
}