using KhoiDepTraiShop.Common.ViewModels;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
        List<Order> GetFullAll();

        IEnumerable<Order> GetOrderListByProductAndUser(int productId, string userId);

        IEnumerable<Product> GetProductBoughtByUser(string userId);

        IEnumerable<Order> GetByUserId(string userId);
    }
    public class OrderRepository : RepositoryBase<Order>,IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Order> GetByUserId(string userId)
        {
            var orders = DbContext.Orders.ToList().Where(p => p.CustomerId == userId);
            foreach (var order in orders)
            {
                order.OrderDetails = DbContext.OrderDetails.Where(p => p.OrderId == order.Id).ToList();
            }
            return orders;
        }

        public List<Order> GetFullAll()
        {
            var orders = DbContext.Orders.ToList();
            foreach(var order in orders)
            {
                order.OrderDetails = DbContext.OrderDetails.Where(p => p.OrderId == order.Id).ToList();
            }
            return orders;
        }

        public IEnumerable<Order> GetOrderListByProductAndUser(int productId, string userId)
        {
            var query = from od in DbContext.OrderDetails
                        join o in DbContext.Orders
                        on od.OrderId equals o.Id
                        where od.ProductId== productId && o.CustomerId == userId
                        select o;
            return query.ToList();

        }

        public IEnumerable<Product> GetProductBoughtByUser(string userId)
        {
            var productIds = from od in DbContext.OrderDetails
                             join o in DbContext.Orders
                             on od.OrderId equals o.Id
                             where o.CustomerId == userId
                             select od.Product;
            return productIds.ToList();
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var to = Convert.ToDateTime(toDate);
            var from = Convert.ToDateTime(fromDate);
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate",from.ToString()),
                new SqlParameter("@toDate",to.ToString())
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @toDate ,@fromDate", parameters);
        }
    }
}
