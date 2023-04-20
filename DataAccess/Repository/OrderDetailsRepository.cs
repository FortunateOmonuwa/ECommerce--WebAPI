using Data;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailsRepository : IBaseContract<OrderDetails>// IBaseContract2<Cart>
    {
        private readonly DataContext _detailsContext;

        public OrderDetailsRepository(DataContext detailsContext)
        {
            _detailsContext = detailsContext;
        }

        public async Task<OrderDetails> AddAsync(OrderDetails details)
        {
            try
            {
                if(details != null)
                {
                    await _detailsContext.AddAsync(details);
                    await _detailsContext.SaveChangesAsync();
                    return details;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(DbUpdateException ex)
            {
                throw new DbUpdateException("An error occured while creating this order detail", ex);
            }
        }

     
        public async Task<IQueryable<OrderDetails>> GetAllAsync()
        {
            try
            {
                var details = await _detailsContext.OrderDetails.ToListAsync();
                return details.AsQueryable();
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured while trying to retreive all OrderDetails", ex);
            }
           
        }

        public async Task<OrderDetails> GetByIdAsync(int id)
        {

            try
            {
                var idCheck = await _detailsContext.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailId == id);
                if(idCheck != null)
                {
                    return idCheck;
                }
                else
                {
                    throw new Exception("An error occured");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error trying to fetch the details of this order" , ex);
            }
        }

        public Task<IEnumerable<OrderDetails>> GetFiltered(Expression<Func<OrderDetails, bool>> filtered)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> UpdateAsync(OrderDetails entity)
        {
            throw new NotImplementedException();
        }
        public Task<OrderDetails> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
