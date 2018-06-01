using AutoMapper;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using KhoiDepTraiShop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhoiDepTraiShop.Web.Infrastructure.Extensions;
using System.Linq;
using System;
using System.Web.Script.Serialization;

namespace KhoiDepTraiShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategodyService _productCategoryService;
        public ProductCategoryController(IErrorService errorService,IProductCategodyService postCategoryService):base(errorService)
        {
            this._productCategoryService = postCategoryService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAllParents(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAllRootCategory();

                var responseData = model.ToModelList();
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getallsubs")]
        [HttpGet]
        public HttpResponseMessage GetAllsubs(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAllSubCategory();

                var responseData = model.ToModelList();
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }


        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategory)
        {
            return CreateHttpResponse(request,()=> {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                  response =  request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);                  
                }
                else
                {
                    ProductCategory pr = new ProductCategory();
                    pr.UpdateProductCategory(productCategory);
                    pr.CreatedBy = User.Identity.Name;
                    var category = _productCategoryService.Add(pr);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, category.ToModel());
                }
                return response;
            });
        }
        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productcategoryvm)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                }
                else
                {
                    ProductCategory pr = _productCategoryService.GetById(productcategoryvm.Id);
                    pr.UpdateProductCategory(productcategoryvm);
                    pr.UpdatedDate = DateTime.Now;
                    pr.UpdatedBy = User.Identity.Name;
                    _productCategoryService.Update(pr);
                    _productCategoryService.SaveChanges();

                    var vm = pr.ToModel();
                    response = request.CreateResponse(HttpStatusCode.Created,vm);
                }
                return response;
            });
        }
        public HttpResponseMessage DELETE(HttpRequestMessage request,int id)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {


                    var catagory = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, catagory);
                }
                return response;
            });
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize =2, string keyWord = null)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
               
               
                    int totalRow = 0;
                    var categoryList = _productCategoryService.GetAll(keyWord);
                   
                    totalRow = categoryList.Count();
                    var query = categoryList.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                    var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                    {
                        Items = query.ToModelList(),
                        Page = page,
                        TotalRow = totalRow,
                        TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                    };

                    response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
               
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);

                var responseData = model.ToModel();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldProductCategory = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    var vm =oldProductCategory.ToModel();
                  
                    response = request.CreateResponse(HttpStatusCode.Created, vm);
                }

                return response;
            });
        }

        [Route("multidelete")]
        [HttpDelete]

        public HttpResponseMessage MultiDelete(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var idList = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                    foreach(var item in idList)
                    {
                        _productCategoryService.Delete(item);
                    }
                   
                    _productCategoryService.SaveChanges();
    

                    response = request.CreateResponse(HttpStatusCode.OK, idList.Count);
                }

                return response;
            });
        }




    }

}