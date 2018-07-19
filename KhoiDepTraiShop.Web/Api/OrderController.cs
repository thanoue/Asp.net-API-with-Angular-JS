using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using KhoiDepTraiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KhoiDepTraiShop.Web.Api
{
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        #region Initialize
        private IOrderService _orderService;

        public OrderController(IErrorService errorService, IOrderService orderService)
            : base(errorService)
        {
            this._orderService = orderService;

        }

        #endregion
        
      
        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = "OrderView")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetFullAll(keyword).ToList();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = query.ToList().ToViewModelList();

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalRow = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getByStatus")]
        [HttpGet]
        [Authorize(Roles = "OrderView")]
        public HttpResponseMessage GetAllByStatus(HttpRequestMessage request,OrderStatus orderStatusType, int page, int pageSize = 20, string keyword=" ")
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetFullAll(keyword).Where(p=>p.Status == orderStatusType);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = query.ToList().ToViewModelList();

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalRow = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        [Route("changeStatusOfOrder")]
        [HttpGet]
        [Authorize(Roles = "OrderModify")]
        public HttpResponseMessage ChangeStatusOfOrder(HttpRequestMessage request,OrderStatus destStatus,int orderId )
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

                    var order = _orderService.ChangeStatusOfOrder(destStatus, orderId);

                    if(destStatus == OrderStatus.Delivering)
                    {

                       MailUtility.SendMail(order.CustomerEmail, "Thông báo giao đơn hàng","Đơn hàng của bạn đang được giao");
                    }
                    response = request.CreateResponse(HttpStatusCode.Created, order);
                }

                return response;
            });
        }

        [Route("singleDelete")]
        [HttpDelete]
        [Authorize(Roles = "OrderModify")]
        public HttpResponseMessage DeleteSingleOrder(HttpRequestMessage request, int orderId)
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
                    _orderService.DeleteSingleOrder(orderId);
                 
                    response = request.CreateResponse(HttpStatusCode.Created);
                }

                return response;
            });
        }

    }
}
