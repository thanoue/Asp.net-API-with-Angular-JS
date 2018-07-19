using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Web.Commons;
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
    [RoutePrefix("api/feedback")]
    [Authorize]
    public class FeedbackController : ApiControllerBase
    {
        #region Initialize
        private IFeedBackService _feedBackService;

        public FeedbackController(IErrorService errorService, IFeedBackService feedBackService)
            : base(errorService)
        {
            _feedBackService = feedBackService;
        }

        #endregion


        [Route("getByStatus")]
        [HttpGet]
        [Authorize(Roles = "FeedbackView")]
        public HttpResponseMessage GetByStatus(HttpRequestMessage request, FeedBackStatus feedBackStatus, int page, int pageSize = 20, string keyword = "")
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                var res = _feedBackService.GetAllByType(feedBackStatus);
                var vm = res.ToList().ToViewModelList();
                var totalRow = vm.Count();
                var paginationSet = new PaginationSet<FeedBackViewModel>()
                {
                    Items = vm,
                    Page = page,
                    TotalRow = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.Created, paginationSet);

                return response;
            });
        }


        [Route("changeStatusOfFeedback")]
        [HttpDelete]
        [Authorize(Roles = "FeedbackModify")]
        public HttpResponseMessage ChangeStatusOfFeedback(HttpRequestMessage request, FeedBackStatus status, int feedBackId,string responseContent =" ")
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
                    var feedback = _feedBackService.UpdateStatus(status, feedBackId);
                    if(status == FeedBackStatus.Responsed)
                    {                                           

                        MailUtility.SendMail(feedback.Email, "Cảm ơn bạn đã gửi đánh giá phản hồi cho chúng tôi",responseContent);
                    }

                    response = request.CreateResponse(HttpStatusCode.Created);
                }

                return response;
            });
        }

        [Route("multichange")]
        [HttpDelete]
        [Authorize(Roles = "FeedbackModify")]
        public HttpResponseMessage MultiChange(HttpRequestMessage request, FeedBackStatus status, string ids)
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

                    var feedBackIdList = JsonConvert.DeserializeObject<List<int>>(ids);
                    _feedBackService.UpdateMultiFeedbacks(status, feedBackIdList);                 

                    response = request.CreateResponse(HttpStatusCode.Created, feedBackIdList.Count());
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
