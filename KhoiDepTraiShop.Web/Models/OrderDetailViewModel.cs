using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderId { set; get; }


        public int ProductId { set; get; }

        public int Quantity { set; get; }

        public string Image { get; set; }

        public string ProductName { get; set; }


        public decimal Price { set; get; }

        public decimal SubTotal
        {
            get
            {
                return Quantity * Price;
            }
        }

        public OrderDetailViewModel()
        {

        }

    }

    public static class OrderDetailViewModelEmm
    {
        public static OrderDetailViewModel ToViewModel(this OrderDetail entity)
        {
            var vm = new OrderDetailViewModel()
            {
                Price = entity.Price,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity

            };
            using (var context = new KhoiDepTraiShop.Data.ShopDbContext())
            {
                var product = context.Products.Where(p => p.Id == entity.ProductId).FirstOrDefault();
                vm.ProductName = product.Name;
                vm.Image = product.Image;
            }
            return vm;
        }

        public static List<OrderDetailViewModel> ToViewModels(this IList<OrderDetail> entities)
        {
            var vm = new List<OrderDetailViewModel>();
            vm.AddRange(entities.Select(p => p.ToViewModel()));
            return vm;
        }

        public static OrderDetail ToEntity(this OrderDetailViewModel entity)
        {
            var vm = new OrderDetail()
            {
                Price = entity.Price,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity

            };
            return vm;
        }
    }
}