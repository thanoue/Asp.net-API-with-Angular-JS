using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Web.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class OrderViewModel
    {

        public OrderViewModel()
        {
            Orderdetails = new List<OrderDetailViewModel>();
        }
        public int Id { set; get; }

        public string CustomerId { get; set; }

        public string CustomerName { set; get; }


        public string CustomerAddress { set; get; }


        public string CustomerEmail { set; get; }

        public string CustomerMobile { set; get; }

        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public bool Deleted { set; get; }

        public OrderStatus Status { set; get; }

        [UIHint(TemplateConst.VNCurrencyDisPlay)]
        public decimal? TotalCost { get; set; }
        public IList<OrderDetailViewModel> Orderdetails { get; set; }
    }

    public static class OrderViewModelemm
    {
        public static OrderViewModel ToViewModel(this Order entity)
        {
            var vm = new OrderViewModel()
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                CustomerAddress = entity.CustomerAddress,
                CustomerEmail = entity.CustomerEmail,
                CustomerId = entity.CustomerId,
                CustomerMessage = entity.CustomerMessage,
                CustomerMobile = entity.CustomerMobile,
                CustomerName = entity.CustomerName,
                Deleted = entity.Deleted,
                Id = entity.Id,
                PaymentMethod = entity.PaymentMethod,
                Status = entity.Status,
                Orderdetails = entity.OrderDetails.ToList().ToViewModels()
                
            };
            vm.TotalCost = vm.Orderdetails.Sum(p => p.Quantity * p.Price);
            return vm;
        }

        public static List<OrderViewModel> ToViewModelList(this IList<Order> entities)
        {
            var vm = new List<OrderViewModel>();
            vm.AddRange(entities.Select(p => p.ToViewModel()));
            return vm;
        }

        public static Order ToEntity(this OrderViewModel entity)
        {
            var vm = new Order()
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                CustomerAddress = entity.CustomerAddress,
                CustomerEmail = entity.CustomerEmail,
                CustomerId = entity.CustomerId,
                CustomerMessage = entity.CustomerMessage,
                CustomerMobile = entity.CustomerMobile,
                CustomerName = entity.CustomerName,
                Deleted = entity.Deleted,
                Id = entity.Id,
                PaymentMethod = entity.PaymentMethod,
                Status = entity.Status
            };
            return vm;
        }
    }
}