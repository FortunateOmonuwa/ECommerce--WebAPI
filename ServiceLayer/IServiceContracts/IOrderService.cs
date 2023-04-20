using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OrderContract
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(Order order, int customerId, int cartId);
        Task<ICollection<Order>> GetAllCustomerOrders(int customerId);
        Task<ICollection<Order>> GetAllOrders();
        Task<Order> GetOrderById(int customerId, int orderId);
        Task<bool> CancelOrder(int customerId, int orderId);
        Task<int> GetTotalPrice(int customerId, int orderId, int cartId);
        Task<bool> UpdateOrder(Order order, int customerId, int orderId);
    }
}
