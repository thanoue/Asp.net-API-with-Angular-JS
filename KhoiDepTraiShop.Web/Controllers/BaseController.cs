using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models;
using Microsoft.AspNet.Identity.Owin;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        ICommonService _commonService;
        private ApplicationUserManager _userManager;
        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {

                _userManager = value;
            }
        }
        public BaseController(ICommonService commonService)
        {
            _commonService = commonService;            
        }

        public BaseController(ICommonService commonService, ApplicationUserManager applicationUserManager):this(commonService)
        {
            _userManager = applicationUserManager;
        }

        protected IList<CartItemViewModel> m_CartItemList
        {
            get
            {
                return HttpContext.Session[SessionConst.CART_ITEMS] as IList<CartItemViewModel>;
            }
            set
            {
                //if (value == null) return;
                HttpContext.Session[SessionConst.CART_ITEMS] = value;
            }
        }

        /// <summary>
        /// Return JSON result
        /// </summary>
        /// <param name="isSuccess">Flag to indicate that result is succeed or not</param>
        /// <param name="message">Message to return</param>
        /// <param name="obj">Additional data</param>
        /// <returns>JSON object</returns>
        public JsonResult GetResult(bool isSuccess, object obj = null)
        {
            return Json(new { isSuccess = isSuccess,  obj = obj },JsonRequestBehavior.AllowGet);
        }
        protected JsonResult GetSuccessResult()
        {
            return GetResult(true, null);
        }        

        protected JsonResult GetSuccessResult(object obj)
        {
            return GetResult(true, obj);
        }     

        protected JsonResult GetFailResult()
        {
            return GetResult(false, null);
        }

        protected JsonResult GetFailResult(object obj)
        {
            return GetResult(false, obj);
        }

       
    }
}