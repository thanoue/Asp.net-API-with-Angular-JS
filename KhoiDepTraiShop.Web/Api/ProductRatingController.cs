using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using KhoiDepTraiShop.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KhoiDepTraiShop.Web.Api
{
    [RoutePrefix("api/rating")]
    [Authorize]
    public class ProductRatingController : ApiControllerBase
    {
        #region Initialize
        private IProductRatingService _productRatingService;
        private ApplicationUserManager _userManager;
        public ProductRatingController(IErrorService errorService, IProductRatingService productRatingService, ApplicationUserManager applicationUserManager)
            : base(errorService)
        {
            this._productRatingService = productRatingService;
            _userManager = applicationUserManager;
        }

        #endregion


        [Route("getByStatus")]
        [HttpGet]
        [Authorize(Roles = "RatingView")]
        public async Task<HttpResponseMessage> GetAllByStatusAsync(HttpRequestMessage request, ProductRatingStatus productRatingStatus, int page, int pageSize = 20, string keyword = " ")
        {

            HttpResponseMessage response = null;
            try
            {
                HttpResponseMessage res = await Task<HttpResponseMessage>.Run(async () =>
               {
                   HttpResponseMessage resp = null;
                   int totalRow = 0;
                   var model = _productRatingService.GetAllByStatus(productRatingStatus);

                   totalRow = model.Count();
                   var query = model.OrderByDescending(x => x.RatingTime).Skip(page * pageSize).Take(pageSize).ToList();

                   var responseData = await query.ToViewModelList(_userManager);

                   var paginationSet = new PaginationSet<ProductRatingViewModel>()
                   {
                       Items = responseData,
                       Page = page,
                       TotalRow = totalRow,
                       TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                   };
                   resp = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                   return resp;
               });
                if (res != null)
                    return res;
                else
                    return response;

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {

                    Trace.WriteLine("Entity of type " + eve.Entry.Entity.GetType().Name.ToString() + " in state \"{eve.Entry.State}\" ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine("  - Propety: " + ve.PropertyName.ToString() + " ,Error: " + ve.ErrorMessage.ToString());
                    }
                }
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
                return response;
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = request.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
                return response;
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }


        }


        [Route("changeStatusOfRating")]
        [HttpGet]
        [Authorize(Roles = "RatingModify")]
        public HttpResponseMessage ChangeStatusOfOrder(HttpRequestMessage request, ProductRatingStatus ratingStatus, int productId,string userid)
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

                   var res=   _productRatingService.UpdateStatus(ratingStatus, productId,userid);
                  
                    response = request.CreateResponse(HttpStatusCode.Created, res);
                }

                return response;
            });
        }

        [Route("multichange")]
        [HttpDelete]
        [Authorize(Roles = "RatingModify")]
        public HttpResponseMessage MultiChange(HttpRequestMessage request,ProductRatingStatus status, string ids)
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

                    var ratingIDList = JsonConvert.DeserializeObject<List<ProductRatingShortModel>>(ids);
                    var ratingList = new List<ProductRating>();
                    foreach(var item in ratingIDList)
                    {
                        var rating = _productRatingService.GetSingle(item.UserId, item.RatedProductId);
                        ratingList.Add(rating);
                    }

                     _productRatingService.MultiUpdate(status,ratingList);

                    response = request.CreateResponse(HttpStatusCode.Created,ratingList.Count());
                }

                return response;
            });
        }



        //[Route("singleDelete")]
        //[HttpDelete]
        //[Authorize(Roles = "OrderModify")]
        //public HttpResponseMessage DeleteSingleOrder(HttpRequestMessage request, int orderId)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            _orderService.DeleteSingleOrder(orderId);

        //            response = request.CreateResponse(HttpStatusCode.Created);
        //        }

        //        return response;
        //    });
        //}


    }
}
