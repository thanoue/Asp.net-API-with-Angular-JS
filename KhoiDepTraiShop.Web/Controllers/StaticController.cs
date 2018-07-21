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
        IProductService _productService;
        ICommonService _commonService;

        public StaticController(IProductCategodyService productCategodyService, ICommonService commonService, IProductService productService) : base(commonService)
        {
            _productCategoryService = productCategodyService;
            _commonService = commonService;
            _productService = productService;
          
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

        public PartialViewResult GetVegetableCategory()
        {
            var categoryList = _productCategoryService.GetAllByParentId(15).ToList();
            var vm = new List<ViewItemModel>();
            foreach(var item in categoryList)
            {
                vm.Add(new ViewItemModel(item.Id, item.Name));
            }
            return PartialView(PartialConstCommon.CategoryByParentPartial, vm);
        }

        public PartialViewResult GetFoodCategory()
        {
            var categoryList = _productCategoryService.GetAllByParentId(16).ToList();
            var vm = new List<ViewItemModel>();
            foreach (var item in categoryList)
            {
                vm.Add(new ViewItemModel(item.Id, item.Name));
            }
            return PartialView(PartialConstCommon.CategoryByParentPartial, vm);
        }
        
        public PartialViewResult GetFruistCategory()
        {
            var categoryList = _productCategoryService.GetAllByParentId(17).ToList();
            var vm = new List<ViewItemModel>();
            foreach (var item in categoryList)
            {
                vm.Add(new ViewItemModel(item.Id, item.Name));
            }
            return PartialView(PartialConstCommon.CategoryByParentPartial, vm);
        }

        public PartialViewResult GetOthersCategory()
        {
            var categoryList = _productCategoryService.GetAllByParentId(18).ToList();
            var vm = new List<ViewItemModel>();
            foreach (var item in categoryList)
            {
                vm.Add(new ViewItemModel(item.Id, item.Name));
            }
            return PartialView(PartialConstCommon.CategoryByParentPartial, vm);
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}