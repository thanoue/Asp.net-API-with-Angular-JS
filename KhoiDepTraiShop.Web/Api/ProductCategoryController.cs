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

namespace KhoiDepTraiShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategodyService _productCategoryService;
        public ProductCategoryController(IErrorService errorService,IProductCategodyService postCategoryService):base(errorService)
        {
            this._productCategoryService = postCategoryService;
        }
        [Route("add")]
        public HttpResponseMessage POST(HttpRequestMessage request, ProductCategoryViewModel productcategoryvm)
        {
            return CreateHttpResponse(request,()=> {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {

                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                  
                }
                else
                {
                    ProductCategory pr = new ProductCategory();
                    pr.UpdateProductCategory(productcategoryvm);
                    var category = _productCategoryService.Add(pr);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }
        [Route("update")]
        public HttpResponseMessage PUT(HttpRequestMessage request, ProductCategoryViewModel productcategoryvm)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                }
                else
                {
                    ProductCategory pr = _productCategoryService.GetById(productcategoryvm.Id);
                    pr.UpdateProductCategory(productcategoryvm);
                    _productCategoryService.Update(pr);
                    _productCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
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
        public HttpResponseMessage GET(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {


                    var catagoryList = _productCategoryService.GetAll();
                    //    var listProductCategoryvm = Mapper.Map<List<ProductCategoryViewModel>>(catagoryList);
                    response = request.CreateResponse(HttpStatusCode.OK, catagoryList);
                }
                return response;
            });
        }

    }

}