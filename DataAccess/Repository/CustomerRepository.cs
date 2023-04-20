using Data;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : IBaseContract<Customer>, IBaseContractNR<Review>//, IBaseContract2<Order>
    {
        private readonly DataContext _customerContext;
        public CustomerRepository(DataContext customerContext) 
        {
            _customerContext= customerContext;
        }
        public async  Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    await _customerContext.Customers.AddAsync(customer);
                    await _customerContext.SaveChangesAsync();
                    return customer;
                }
                else
                {
                    throw new ArgumentException("The Customer cannot be null or empty.", nameof(customer));
                }
            }

            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while adding the new customer.", ex);
            }
        }

        public async Task<Customer> DeleteAsync(int customerId)
        {
            try
            {
                var customer = await _customerContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                if (customer != null)
                {
                    _customerContext.Customers.Remove(customer);
                    await _customerContext.SaveChangesAsync();
                    return customer;

                }
                else
                {
                    throw new Exception($"Customer with ID {customerId} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while deleting this Customer.");
            }
        }

        public async Task<IQueryable<Customer>> GetAllAsync()
        {
            try
            {
                var getCustomers = await _customerContext.Customers
                    .Include(o => o.Orders)
                    .ToListAsync();
                return getCustomers.AsQueryable();
            }

            catch
            {
                throw new Exception("An error occurred while retrieving the Customers.");
            }
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            try
            {
                var customer = await _customerContext.Customers
                    .Include(r => r.Orders)
                    .FirstOrDefaultAsync(r => r.CustomerId == customerId);
                if (customer != null)
                {
                    return customer;
                }
                else
                {
                    throw new Exception($"Customer with ID {customerId} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while retrieving this Customer.");
            }
        }
        public async Task<IEnumerable<Customer>> GetFiltered(Expression<Func<Customer, bool>> filtered)
        {
            try
            {
                var filter = await _customerContext.Customers.Where(filtered).OrderBy(c => c.FirstName).ToListAsync();
                if (filter != null)
                {
                    return filter;
                }
                else
                {
                    throw new Exception("You have not specified any filter condition");
                }
            }
            catch
            {
                throw new Exception("An error occured while tryin to retrieve the filtered customer");
            }
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            try
            {
                var customerCheck = await _customerContext.Customers.FindAsync(customer.CustomerId);
                if (customerCheck != null)
                {
                    _customerContext.Customers.Update(customerCheck);
                    await _customerContext.SaveChangesAsync();
                    return customerCheck;
                }
                else
                {
                    throw new Exception($"Customer doesn't exist");
                }

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while updating this Customer's details.", ex);
            }
        }
        //public async Task<Order> AddNestedAsync(Order newOrder, int customerId)
        //{
        //    try
        //    {
        //        var order = await _customerContext.Customers.Include(r => r.Orders).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        //        if (order != null)
        //        {
        //            order.Orders.Add(newOrder);
        //            await _customerContext.SaveChangesAsync();
        //            return newOrder;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("An error has occured");
        //    }
        //}
        //public async Task<IQueryable<Order>> GetAllNestedAsync(int customerId)
        //{
        //    try
        //    {
        //        var check = _customerContext.Orders.FirstOrDefault(p => p.CustomerId == customerId);
        //        if (check != null)
        //        {
        //            var orderDetails = await _customerContext.Orders.ToListAsync();
        //            return orderDetails.AsQueryable();
        //        }
        //        else
        //        {
        //            throw new Exception("not found");
        //        }

        //    }
        //    catch
        //    {
        //        throw new Exception("An error occured while trying to retrieve this order");
        //    }
        //}

        //public async Task<Order> GetNestedByIdAsync(int customerId, int orderId)
        //{
        //    try
        //    {
        //        var details = await _customerContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId && x.CustomerId == customerId);
        //        if (details != null)
        //        {
        //            return details;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }

        //    catch
        //    {
        //        throw new Exception("An error occurred while retrieving the order.");
        //    }
        //}

        //public async Task<Order> UpdateNestedAsync(Order order, int customerId)
        //{
        //    try
        //    {
        //        var idCheck = await _customerContext.Customers.Include(r => r.Orders).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        //        if (idCheck != null)
        //        {
        //            idCheck.Orders.Add(order);
        //            await _customerContext.SaveChangesAsync();
        //            return order;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }

        //    catch
        //    {
        //        throw new DbUpdateException("An error occurred while updating the Order.");
        //    }
        //}

        //public async Task<Order> DeleteNestedAsync(int customerId, int orderId)
        //{
        //    try
        //    {
        //        var order = await _customerContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        //        if (order != null)
        //        {
        //            _customerContext.Remove(order);
        //            await _customerContext.SaveChangesAsync();
        //            return order;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("An error occured while deleting this Order");
        //    }
        //}

        public async Task<Review> AddNRAsync(Review review, int customerId)
        {
            try
            {
                var customer = await _customerContext.Customers.Include(r => r.Reviews).FirstOrDefaultAsync(x => x.CustomerId == customerId);
                if (customer != null)
                {
                    customer.Reviews.Add(review);
                    await _customerContext.SaveChangesAsync();
                    return review;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception("An error has occured");
            }
        }

        public async Task<Review> GetNRByIdAsync(int customerId, int reviewId)
        {
            try
            {
                var review = await _customerContext.Reviews.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ReviewId == reviewId);
                if (review != null)
                {
                    return review;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                throw new Exception("An error occurred while retrieving the review.");
            }
        }

        public async Task<IQueryable<Review>> GetAllNRAsync(int customerId)
        {
            try
            {
                var check = _customerContext.Reviews.FirstOrDefault(p => p.CustomerId == customerId);
                if(check != null)
                {
                    var reviews = await _customerContext.Reviews.ToListAsync();
                    return reviews.AsQueryable();
                }
                else
                {
                    throw new Exception("not found");
                }

            }
            catch
            {
                throw new Exception("An error occured while trying to retrieve the reviews");
            }
        }

        public async Task<Review> UpdateNRAsync(Review review, int customerId)
        {
            try
            {
                var newReview = await _customerContext.Customers.Include(r => r.Reviews).FirstOrDefaultAsync(x => x.CustomerId == customerId);
                if (newReview != null)
                {
                    newReview.Reviews.Add(review);
                    await _customerContext.SaveChangesAsync();
                    return review;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                throw new DbUpdateException("An error occurred while updating the Review.");
            }
        }

        public async Task<Review> DeleteNRAsync(int customerId, int reviewId)
        {
            try
            {
                var review = await _customerContext.Reviews.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ReviewId == reviewId);
                if (review != null)
                {
                    _customerContext.Remove(review);
                    await _customerContext.SaveChangesAsync();
                    return review;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception("An error occured while deleting this Review");
            }
        }
    }
}
