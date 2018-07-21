using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Data.Repositories;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetails);
        IEnumerable<Order> GetAll(string keyword = "");
        List<Order> GetFullAll(string keyWord = "");
        void DeleteSingleOrder(int orderId);
        Order ChangeStatusOfOrder(OrderStatus destStatus, int orderId);
        IEnumerable<Product> GetProductBoughtByUser(string userId);
        IEnumerable<Order> GetAllByUserId(string userId);
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderId = order.Id;
                    _orderDetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Order> GetAll(string keyword = "")
        {
            if (!string.IsNullOrEmpty(keyword))
                return _orderRepository.GetAll().Where(p => p.CustomerName.Contains(keyword) || p.CustomerEmail.Contains(keyword)).ToList();
            return _orderRepository.GetAll().ToList();

        }

        public List<Order> GetFullAll(string keyWord = " ")
        {
            if(!string.IsNullOrEmpty(keyWord))
                return _orderRepository.GetFullAll().Where(p=>p.CustomerName.Contains(keyWord)).ToList();
            return _orderRepository.GetFullAll().ToList();
        }

        public Order ChangeStatusOfOrder(OrderStatus destStatus, int orderId)
        {
            var order = _orderRepository.GetSingleById(orderId);
            order.Status = destStatus;
            _unitOfWork.Commit();
            return order;
        }

        public void DeleteSingleOrder(int orderId)
        {
            _orderRepository.Delete(orderId);
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetProductBoughtByUser(string userId)
        {
            return _orderRepository.GetProductBoughtByUser(userId);
        }

        public IEnumerable<Order> GetAllByUserId(string userId)
        {
            return _orderRepository.GetByUserId(userId);
        }
    }
}
