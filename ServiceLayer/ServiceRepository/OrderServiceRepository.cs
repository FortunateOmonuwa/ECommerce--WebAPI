using Data;
using DataAccess.DTO.OrdersDTO;
using DataAccess.Repository;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Service.IServiceContracts;
using Service.OrderContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceRepository
{
    public class OrderServiceRepository : IOrderService
    {   private readonly ICartService _cartServiceRepository;
        private readonly IBaseContract<Customer> _customer;
        private readonly IBaseContract<Product> _product;
        private readonly DataContext _context;
        public OrderServiceRepository(DataContext context, ICartService cartServiceRepository, IBaseContract<Customer> customer, IBaseContract<Product> product) 
        {
            _cartServiceRepository = cartServiceRepository;
            _customer = customer;
            _product = product;
            _context = context;
        }

        public async Task<Order> PlaceOrder(Order order, int customerId, int cartId)
        {
            try
            {
                //Check to see if customer exists
                var customer = await _customer.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return null;
                }

                //get cart
                var cart = await _cartServiceRepository.GetCartById(cartId, customerId);
                //check if cart is null
                if (cart == null)
                {
                    return null;
                }

                //loop through each product in the cart
                foreach (var item in cart.Products)
                {
                    //check if the product is null
                    var product = await _product.GetByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        return null;
                    }
                    //check to see if items in the cart is not more than the available product
                    else if (item.Quantity > product.Quantity)
                    {
                        return null;
                    }
                    else
                    {
                        //subtract the quantity of products in order from the quantity of products in stock
                        product.Quantity -= item.Quantity;

                        // create a new OrderDetail for each product in the cart
                        // check if order details collection is empty
                        if (order.OrderDetails.Count == 0)
                        {
                            // create a new OrderDetail with customer information
                            var orderDetail = new OrderDetails
                            {
                                DeliveryAddress = customer.Address,
                                CustomerName = customer.FirstName + " " + customer.LastName,
                                CustomerPhoneNumber = customer.PhoneNumber
                            };

                            // add the order detail to the order
                            order.OrderDetails.Add(orderDetail);
                        }
                        else
                        {
                            // update the existing order detail with customer information
                            var orderDetail = order.OrderDetails.First();
                            orderDetail.DeliveryAddress = customer.Address;
                            orderDetail.CustomerName = customer.FirstName + " " + customer.LastName;
                            orderDetail.CustomerPhoneNumber = customer.PhoneNumber;
                            // add the product details to the order details
                            order.OrderDetails.Add(orderDetail);
                        } 
                    }
                }
                //assign customerId in the customer to the customerId in the order class
                order.CustomerId = customer.CustomerId;
                order.CartId = cart.CartId;
                order.OrderDate = DateTime.Now;


                //add order into Orders table
                await _context.Orders.AddAsync(order);
                //save changes
                await _context.SaveChangesAsync();
                //delete cart
                await _cartServiceRepository.DeleteCart(cartId);
                return order;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> CancelOrder(int customerId, int orderId)
        {
            try
            {
                var customer = await _customer.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return false;
                }
                else
                {
                    var order = customer.Orders.FirstOrDefault(c => c.OrderId == orderId);
                    if(order == null)
                    {
                        return false;
                    }
                    else
                    {
                        customer.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ICollection<Order>> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                return orders;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ICollection<Order>> GetAllCustomerOrders(int customerId)
        {
            try
            {
                var customer = await _customer.GetByIdAsync(customerId);
                if(customer == null)
                {
                    return null;
                }
                else
                {
                    var order = customer.Orders.ToList();
                    return order;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> GetOrderById(int customerId, int orderId)
        {
            try
            {
                var customer = await _customer.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return null;
                }
                else
                {
                    var order = await _context.Orders
                        .Include(p => p.Products)
                        .FirstOrDefaultAsync(c => c.OrderId == orderId);
                    if(order == null)
                    {
                        return null;
                    }
                    else
                    {
                        return order;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<int> GetTotalPrice(int customerId, int orderId, int cartId)
        {
            try
            {
                var customer = await _customer.GetByIdAsync(customerId);
               if(customer == null)
                {
                    return 0;
                }
                else
                {
                    var order = customer.Orders.FirstOrDefault(o => o.OrderId==orderId);
                    if (order == null)
                    {
                        return 0;
                    }
                    else
                    {
                        var price = await _cartServiceRepository.GetTotalPrice(customerId, cartId);
                        return price;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateOrder(Order order, int customerId, int orderId)
        {
            try
            {
                var customer = await _customer.GetByIdAsync(customerId);
                if(customer == null)
                {
                    return false;
                }
                else
                {
                    var ordercheck = customer.Orders.FirstOrDefault(o => o.OrderId == orderId);
                    if(ordercheck == null)
                    {
                        return false;
                    }
                    else
                    {
                        order.CustomerId = customerId;
                        order.OrderId = orderId;
                        _context.Orders.Update(order);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
