using AutoMapper;
using Data;
using DataAccess.DTO;
using Domain.Contract;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ReviewRepository : IBaseContract<Review>
    {
        private readonly DataContext _reviewContext;
        
        public ReviewRepository(DataContext reviewcontext)
        {
            _reviewContext = reviewcontext; 
        }

        public async Task<Review> AddAsync(Review review)
        {
            try
            {
                
                if( review != null)
                {
                    await _reviewContext.Reviews.AddAsync(review);
                    await _reviewContext.SaveChangesAsync();
                    return review;
                }
                else
                {
                    throw new ArgumentException("The review cannot be null or empty.", nameof(review));
                }
            }

            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while adding your review.", ex);
            }

        }

        

        public async Task<IQueryable<Review>> GetAllAsync()
        {
            try
            {
                var getReviews = await _reviewContext.Reviews.ToListAsync();
                return getReviews.AsQueryable();
            }

            catch 
            {
                throw new Exception("An error occurred while retrieving the reviews.");
            }
    
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            try
            {
                var review = await _reviewContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
                if(review != null)
                {
                    return review;
                }
                else
                {
                    throw new Exception($"Review with ID {id} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while retrieving the review.");
            }

        }

        public async Task<IEnumerable<Review>> GetFiltered(Expression<Func<Review, bool>> filtered)
        {
            try
            {
                var filter = await _reviewContext.Reviews.Where(filtered).ToListAsync();
                if(filter != null)
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
                throw new Exception("An error occured while tryin to retrieve the filtered items");
            }
            
        }

        //public async Task<IEnumerable<Review>> GetSorted<TKey>(Expression<Func<Review, TKey>> sorted)//bool descendingOrder = false)
        //{
        //    try
        //    {
        //        //var query = _reviewContext.Reviews.OrderBy(sorted);
        //        //if (descendingOrder)
        //        //{
        //        //    query = query.OrderByDescending(sorted);
        //        //}
        //        var sortedReview = await _reviewContext.Reviews.OrderBy(sorted).ToListAsync();
        //        if (sortedReview != null)
        //        {
        //            return sortedReview;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("An error occured while trying to retrieve the sorted reviews");
        //    }
        //}

        public async Task<Review> UpdateAsync(Review review)
        {
            try
            {
                var entityCheck = await _reviewContext.Reviews.FindAsync(review.ReviewId);
                if (entityCheck != null)
                {
                    _reviewContext.Reviews.Update(entityCheck);
                    await _reviewContext.SaveChangesAsync();
                    return entityCheck;
                }
                else
                {
                    throw new Exception($"");
                }

            }
            catch(DbUpdateException ex) 
            {
                throw new DbUpdateException("An error occurred while updating your review.", ex);
            }
        }

        public async Task<Review> DeleteAsync(int id)
        {
            try
            {
                var delete = await _reviewContext.Reviews.FirstOrDefaultAsync(x => x.ReviewId == id);
                if(delete != null)
                {
                    _reviewContext.Reviews.Remove(delete);
                    await _reviewContext.SaveChangesAsync();
                    return delete;
                    
                }
                else
                {
                    throw new Exception($"Review with ID {id} was not found.");
                   
                }
            }
            catch
            {
                throw new Exception("An error occurred while retrieving the review.");
            }
        }
    }
}
