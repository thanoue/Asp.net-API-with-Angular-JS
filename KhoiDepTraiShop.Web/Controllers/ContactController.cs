using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class ContactController : BaseController
    {

        ICommonService _commonService;
        IContactDetailService _contactDetailService;
        IFeedBackService _feedBackService;
        public ContactController(ICommonService commonService, IContactDetailService contactDetailService, IFeedBackService feedBackService) : base(commonService)
        {
            _commonService = commonService;
            _contactDetailService = contactDetailService;
            _feedBackService = feedBackService;
        }

        // GET: Contact
        public ActionResult ContactPage()
        {
            var vm = new ContactViewModel();
            vm.ContactDetailViewModels = _contactDetailService.GetContactDetails().ToList().ToViewModelList();
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]

        public ActionResult SendFeedBack(ContactViewModel vm)
        {


            //Validate Google recaptcha here
            var response = Request["g-recaptcha-response"];
            string secretKey = ConfigUtility.GetByKey("recaptchaPrivatekey");
            var client = new WebClient();
            string urlReQuest = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response);
            var result = client.DownloadString(urlReQuest);
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            bool valid = true;
            if (!status)
            {
                ViewData["FailMessage"] = "Bạn chưa xác thực ReCaptcha";
                valid = false;
            }
            if (!ModelState.IsValid)
            {
                valid = false;
            }
            if (!valid)
                return View("ContactPage", vm);

            var feedBack = vm.FeedBack.Toentity();
            _feedBackService.CreateFeedBack(feedBack);
            _feedBackService.Save();

            var adminEmail = ConfigUtility.GetByKey("AdminMail");
            TempData["Success"] = "Bạn sẽ nhận được câu trả lời qua email";
            bool mailSeding =  MailUtility.SendMail(adminEmail, "Thông báo có feedback từ khách hàng", "<h1>vào trang quản trị để phẩn hồi ý kiến của khách hàng!!!</h1>");
            
            return RedirectToAction("ContactPage");



        }




    }
}