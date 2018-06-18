using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            ContactDetailViewModels = new List<ContactDetailViewModel>();
            FeedBack = new FeedBackViewModel();

        }
        public IList<ContactDetailViewModel> ContactDetailViewModels { get; set; }

        public FeedBackViewModel FeedBack { get; set; }
    }
}