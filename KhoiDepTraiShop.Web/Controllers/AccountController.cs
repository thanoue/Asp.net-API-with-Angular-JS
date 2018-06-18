using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using System.Threading.Tasks;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Model.Models;
using Microsoft.AspNet.Identity.Owin;
using KhoiDepTraiShop.Common;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ICommonService _commonService;

        public AccountController(ICommonService commonService, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(commonService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _commonService = commonService;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
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

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            var vm = new RegisterViewModel();
            return PartialView(PartialConstCommon.RegisterPopup, vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel vm)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {

                    var userByEmail = await _userManager.FindByEmailAsync(vm.Email);
                    if (userByEmail != null)
                    {
                        ViewData["EmailError"] = "Email đã tồn tại!";
                        return PartialView(PartialConstCommon.RegisterPopup, vm);
                    }

                    var userByname = await _userManager.FindByNameAsync(vm.UserName);
                    if (userByname != null)
                    {
                        ViewData["UserError"] = "Tên đăng nhập đã tồn tại!";
                        return PartialView(PartialConstCommon.RegisterPopup, vm);
                    }
                    var key = new Random(); var cridential = key.Next(5000, 9000);
                    var user = new ApplicationUser()
                    {
                        UserName = vm.UserName,
                        Email = vm.Email,
                        Address = vm.Address,
                        EmailConfirmed = false,
                        BirthDay = DateTime.Now,
                        FullName = vm.FullName,
                        CridentialCode = cridential.ToString(),
                        PhoneNumber = vm.PhoneNumber

                    };
                    await _userManager.CreateAsync(user, vm.Password);
                    var insertUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(insertUser.Id, "User");

                    string content = System.IO.File.ReadAllText(Server.MapPath(TemplateConst.UserValidationMail));

                    content = content.Replace("{{UserName}}", vm.UserName);
                    content = content.Replace("{{Code}}", cridential.ToString());
                    content = content.Replace("{{Link}}", ConfigUtility.GetByKey("MyDomain") + "dang-nhap");

                    MailUtility.SendMail(vm.Email, "Thông báo xác thực tài khoản", content);

                    return GetSuccessResult();

                }
                else
                    return PartialView(PartialConstCommon.RegisterPopup, vm);
            }
            ViewData["CatchaError"] = "Mã Captcha không đúng!";
            return PartialView(PartialConstCommon.RegisterPopup, vm);
        }

        [HttpGet]
        public ActionResult Login()
        {
            var vm = new LoginViewModel();
            return PartialView(PartialConstCommon.LoginPopup, vm);
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(vm.UserName, vm.Password);
                if (user != null)
                {
                    if (user.EmailConfirmed == false)
                    {
                        if (string.IsNullOrEmpty(vm.CridentialCode))
                            return GetFailResult(new { Message = "Ở lần đầu đăng nhập, bạn cần nhập mã xác thực" ,Type = "NeedCridentalCode"});
                        else
                        {
                            if(vm.CridentialCode == user.CridentialCode)
                            {
                                user.EmailConfirmed = true;

                                await UserManager.UpdateAsync(user);
                                // Success here
                                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                                ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                                AuthenticationProperties props = new AuthenticationProperties();
                                props.IsPersistent =(bool) vm.RememberMe;
                                authenticationManager.SignIn(props, identity);
                                return GetSuccessResult();
                            }
                            else
                            {
                                return GetFailResult(new { Message = "Mã xác thực không chính xác!" });
                            }

                        }
                    }
                    else
                    {
                        // Success here
                        IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                        ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationProperties props = new AuthenticationProperties();
                        props.IsPersistent = vm.RememberMe;
                        authenticationManager.SignIn(props, identity);
                        return GetSuccessResult();

                        //success here
                    }
                }
                else
                {

                    return GetFailResult(new { Message = "Thông tin đăng nhập không đúng" });
                }
            }
            else
            {
                return GetFailResult(new { Message = "Thông tin đăng nhập không đúng" });
            }

        }

        [HttpGet]
        [Authorize]
        public JsonResult LogOut()
        {
            try
            {
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut();
                return GetSuccessResult();
            }
            catch (Exception ex)
            {
                return GetFailResult(new { Message = ex.ToString() });
            }

        }
    }
}