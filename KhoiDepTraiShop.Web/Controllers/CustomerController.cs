using KhoiDepTraiShop.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Service;
using CaptchaMvc.HtmlHelpers;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class CustomerController : BaseController
    {
        IOrderService _orderService;
        IProductRatingService _productRatingService;
        public CustomerController(ICommonService commonService, IProductService productService,IProductRatingService productRatingService, IProductCategodyService productCategodyService, ApplicationUserManager userManager
            , IOrderService orderService, IAddressService addressService) : base(commonService, userManager)
        {
            _orderService = orderService;
            _productRatingService = productRatingService;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("User")]
        public async Task<ActionResult> UserProfileAsync(int page = 1)
        {
            var vm = new CustomerViewModel();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                vm.CurrentUser = user.ToViewModel();
                var boughtProducts = _orderService.GetProductBoughtByUser(User.Identity.GetUserId());
                vm.BoughtProducts = boughtProducts.ToList().ToModelList(_productRatingService);

                vm.OrderList = _orderService.GetAllByUserId(User.Identity.GetUserId()).ToList().ToViewModelList();

                return View(vm);
            }
            else
                return RedirectToAction("Index", "Home");           
          
           
        }
        [HttpPost]
        public async Task<ActionResult> UpdateUserProfile(UserViewModel vm)
        {
            if(this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {

                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                    if (user != null)
                    {
                        user.PhoneNumber = vm.PhoneNumber;
                        user.Address = vm.StreetAndNumber;
                        var key = new Random(); var cridential = key.Next(5000, 9000);
                        if (user.Email != vm.Email)
                        {
                            var emailUser = await UserManager.FindByEmailAsync(vm.Email);
                            if (emailUser != null)
                            {
                                ModelState.AddModelError("Email", "Email đã được sử dụng bở một tài khoản khác");
                                return PartialView(PartialConstCommon.UserProfile, vm);
                            }
                            else
                            {
                                user.EmailConfirmed = false;
                                user.Email = vm.Email;
                                user.CridentialCode = cridential.ToString();

                                string content = System.IO.File.ReadAllText(Server.MapPath(TemplateConst.UserValidationMail));

                                content = content.Replace("{{UserName}}", vm.FullName);
                                content = content.Replace("{{Code}}", cridential.ToString());
                                content = content.Replace("{{Link}}", ConfigUtility.GetByKey("MyDomain") + "Home/Index");

                                MailUtility.SendMail(vm.Email, "Thông báo xác thực tài khoản", content);
                            }                          
                        }
                        var update = await UserManager.UpdateAsync(user);
                        if (update != null)
                            return GetSuccessResult();
                    }
                }
                return PartialView(PartialConstCommon.UserProfile, vm);
            }
            ViewData["CatchaError"] = "Mã Captcha không đúng!";
            return PartialView(PartialConstCommon.UserProfile, vm);
        }

        [HttpPost]
        public async Task<ActionResult> PasswordChangingSave(PasswordChangingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    var  hashedOldPassword = await UserManager.CheckPasswordAsync(user,vm.OldPassword);                   
                    if (hashedOldPassword)
                    {
                        UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
                        string hashedNewPassword = UserManager.PasswordHasher.HashPassword(vm.NewPassword);
                        await store.SetPasswordHashAsync(user, hashedNewPassword);
                        var result = await UserManager.UpdateAsync(user);
                        if(result!=null)
                            return GetSuccessResult();                     

                    }
                    else
                    {
                        ViewData["Reload"] = "in";
                        ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng");
                        return PartialView(PartialConstCommon.PasswordChangingPartial, vm);
                    }
                }
                else
                {
                    ViewData["Reload"] = "in";
                    return PartialView(PartialConstCommon.PasswordChangingPartial, vm);
                }
            }
            ViewData["Reload"] = "in";
            return PartialView(PartialConstCommon.PasswordChangingPartial, vm);
        }
    }
}