using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        ICommonService _commonService;

        public BaseController(ICommonService commonService)
        {
            _commonService = commonService;
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
            return Json(new { isSuccess = isSuccess,  obj = obj });
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