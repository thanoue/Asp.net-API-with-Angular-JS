using KhoiDepTraiShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using KhoiDepTraiShop.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;
using KhoiDepTraiShop.Web.Models;
using CaptchaMvc.HtmlHelpers;
using KhoiDepTraiShop.Model.Models;
using System.Diagnostics;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Web.Models.RazorTemplateModel;

namespace KhoiDepTraiShop.Web.Controllers

{
    public class OrderController : BaseController
    {
        ICommonService _commonService;
        IProductService _productService;
        IProductRatingService _productRatingService;
       
        IProductCategodyService _productCategoryService;
        IOrderService _orderService;
        IAddressService _addressService;
        public OrderController(ICommonService commonService, IProductService productService, IProductRatingService productratingService, IProductCategodyService productCategodyService, ApplicationUserManager userManager
            ,IOrderService orderService,IAddressService addressService) : base(commonService,userManager)
        {
            _commonService = commonService;
            _productRatingService = productratingService;
            _productService = productService;
            _productCategoryService = productCategodyService;
            _orderService = orderService;
            _addressService = addressService;
        }
     

        public JsonResult SetCartList(IList<CartItemViewModel> cartItemViewModels)
        {
            try
            {
                m_CartItemList = cartItemViewModels;
                return GetSuccessResult();
            }
            catch (Exception)
            {
                return GetFailResult();
            }
        }
        // GET: Order

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.AllowAnonymous]
        public async Task<ActionResult >CheckOut()
        {
            var vm = new CheckOutViewModel();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                vm.CurrentUser = user.ToViewModel();
            }
            vm.CartItemViewModels = m_CartItemList;
            var provinces = _addressService.GetAllProvince();
            foreach(var item in provinces)
            {
                vm.Address_Province.ListData.Add(new Models.Commons.ViewItemModel(item.ProvinceId, item.Name));
            }          
            return View(vm);
        }

        [HttpPost]
        public  ActionResult CheckOut(CheckOutViewModel vm)
        {
            vm.CartItemViewModels = m_CartItemList;

            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (!ModelState.IsValid)    
                {
                    return View(vm);
                }

                var newOrder = new Order() {
                    CreatedDate = DateTime.Now,
                    Deleted = false,
                    Status = Common.OrderStatus.Sending,
                    PaymentMethod = Common.PaymentMethod.HandByHandPaying,
                    CustomerAddress = vm.CurrentUser.Address,
                    CustomerEmail = vm.CurrentUser.Email,
                    CustomerMobile = vm.CurrentUser.PhoneNumber,
                    CustomerMessage = vm.CustomerMessage,
                    CustomerName = vm.CurrentUser.FullName
                };               
                
                var orderDetails = new List<OrderDetail>();
                decimal total = 0;
                foreach(var cartItem in m_CartItemList)
                {
                    var orderDetail = new OrderDetail()
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity.Value,
                        Price = cartItem.Price
                    };
                    orderDetails.Add(orderDetail);
                    total +=cartItem.Quantity.Value * cartItem.Price;
                }
                if (Request.IsAuthenticated)
                {
                    newOrder.CustomerId = User.Identity.GetUserId();
                    newOrder.CreatedBy = User.Identity.GetUserName();
                }

                _orderService.Create(newOrder, orderDetails);

                string content = System.IO.File.ReadAllText(Server.MapPath(TemplateConst.OrderTemplate));

                content = content.Replace("{{UserName}}", vm.CurrentUser.FullName);
                content = content.Replace("{{OrderDate}}", DateTime.Now.ToString("G"));
                content = content.Replace("{{link}}", ConfigUtility.GetByKey("MyDomain") + "Home/index");
                content = content.Replace("{{Total}}",total.ToString());

                MailUtility.SendMail(vm.CurrentUser.Email, "Đơn hàng đã được gửi đi", content);
                return RedirectToAction("OrderSuccess", "Static");
            }
            else
            {
                ViewData["CatchaError"] = "Mã Captcha không đúng!";
                return View(vm);
            }

            
        }

        [HttpGet]
        public ActionResult ChangeProvince(int provinceId)
        {
            var districts = _addressService.GetDistrictByProvince(provinceId);
            var vm = new SelectViewItemViewModel();
            vm.DefaultOption = "Chọn Quận/Huyện/Thị xã";
            foreach (var item in districts)
            {
                vm.ListData.Add(new Models.Commons.ViewItemModel(item.DistrictId, item.Name));
            }
            ViewData["Id"] = "Address_District";
            return PartialView(PartialConstCommon.DropDownBox, vm);
        }

        [HttpGet]
        public ActionResult ChangeDistrict(int districtId)
        {
            var districts = _addressService.GetWardByDistrict(districtId);
            var vm = new SelectViewItemViewModel();
            vm.DefaultOption = "Chọn Xã/Phường/Phố";
            ViewData["Id"] = "Address_Ward";
            foreach (var item in districts)
            {
                vm.ListData.Add(new Models.Commons.ViewItemModel(item.WardId, item.Name));
            }
            return PartialView(PartialConstCommon.DropDownBox, vm);
        }



    }
}