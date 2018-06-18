using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models;
using KhoiDepTraiShop.Web.Models.Commons;
using KhoiDepTraiShop.Web.Models.RazorTemplateModel;

namespace KhoiDepTraiShop.Web.Controllers
{
    public class StaticController : BaseController
    {
        IProductCategodyService _productCategoryService;
        //IProductRatingService _productRatingService;
        IProductService _productService;
        ICommonService _commonService;

        public StaticController(IProductCategodyService productCategodyService, ICommonService commonService, IProductService productService/*, IProductRatingService productRatingService*/ ) : base(commonService)
        {
            _productCategoryService = productCategodyService;
            _commonService = commonService;
            _productService = productService;
           // _productRatingService = productRatingService;
        }

        
        public ActionResult About()
        {
            return View();
        }

        public PartialViewResult GetCategorySelectView()
        {
            var vm = new SelectViewItemViewModel().SetDefaultOption("Chọn loại sản phẩm");
            var categoryList = _productCategoryService.GetAll().Where(p => p.ParentId != null).ToList();
            foreach(var item in categoryList)
            {
                vm.ListData.Add(new ViewItemModel(item.Id, item.Name));
            }
            return PartialView(PartialConstCommon.SelectViewItem,vm);
        }
    }
}