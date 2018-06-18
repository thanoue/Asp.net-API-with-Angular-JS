using KhoiDepTraiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class PopupController : Controller
    {
        // GET: Popup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductRatingBoxPopup(int productId)
        {

            return PartialView();
        }

        public ActionResult CartViewPopup(IList<CartItemViewModel> cartItemViewModels)
        {
            if (cartItemViewModels == null || cartItemViewModels[0] == null)
                cartItemViewModels = new List<CartItemViewModel>();            
            return PartialView(cartItemViewModels);
        }
    }
}