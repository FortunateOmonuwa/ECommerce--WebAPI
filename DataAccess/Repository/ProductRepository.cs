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
    public class ProductRepository : IBaseContract<Product> , IBaseContract2<Review>
    {
        private readonly DataContext _productContext;
        public ProductRepository(DataContext productContext)
        {
            _productContext= productContext;
        }
        public async Task<Product> AddAsync(Product product)
        {
            try
            {
                if (product != null)
                {
                    await _productContext.Products.AddAsync(product);
                    await _productContext.SaveChangesAsync();
                    return product;
                }
                else
                {
                    throw new ArgumentException("The product cannot be null or empty.", nameof(product));
                }
            }

            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while adding this Product.", ex);
            }
        }
        public async Task<IQueryable<Product>> GetAllAsync()
        {
            try
            {
                var getProducts = await _productContext.Products.ToListAsync();
                return getProducts.AsQueryable();
            }

            catch
            {
                throw new Exception("An error occurred while retrieving the Products.");
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var product = await _productContext.Products.FirstOrDefaultAsync(r => r.ProductId == id);
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new Exception($"Product with ID {id} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while retrieving the Product.");
            }
        }

        public async Task<IEnumerable<Product>> GetFiltered(Expression<Func<Product, bool>> filtered)
        {
            try
            {
                var filter = await _productContext.Products.Where(filtered).ToListAsync();
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
                throw new Exception("An error occured while tryin to retrieve the filtered items");
            }
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            try
            {
                var productCheck = await _productContext.Products.FindAsync(product.ProductId);
                if (productCheck != null)
                {
                    _productContext.Products.Update(productCheck);
                    await _productContext.SaveChangesAsync();
                    return productCheck;
                }
                else
                {
                    throw new Exception($"");
                }

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while updating your product.", ex);
            }
        }
        public async Task<Product> DeleteAsync(int id)
        {
            try
            {
                var delete = await _productContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
                if (delete != null)
                {
                    _productContext.Products.Remove(delete);
                    await _productContext.SaveChangesAsync();
                    return delete;

                }
                else
                {
                    throw new Exception($"Product with ID {id} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while deleting this Product.");
            }
        }

        public async Task<Review> AddNestedAsync(Review review, int productId)
        {
            try
            {
                var productReview = await _productContext.Products.Include(r => r.Review).FirstOrDefaultAsync(x => x.ProductId == productId);
                if (productReview != null)
                {
                    productReview.Review.Add(review);
                    await _productContext.SaveChangesAsync();
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

        public async Task<Review> GetNestedByIdAsync(int productId, int reviewId)
        {
            try
            {
                var review = await _productContext.Reviews.FirstOrDefaultAsync(x => x.ProductId == productId &&  x.ReviewId == reviewId);
                if(review != null)
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

        public async Task<IQueryable<Review>> GetAllNestedAsync(int productId)
        {
            try
            {
                var check = _productContext.Reviews.FirstOrDefault(p => p.ProductId == productId);
                if(check != null)
                {
                    var productReview = await _productContext.Reviews.ToListAsync();
                    return productReview.AsQueryable();
                }
                else
                {
                    throw new Exception("not found");
                }
             
            }
            catch
            {
                throw new Exception("An error occured while trying to retrieve all reviews related to this product");
            }
        }

        public async Task<Review> UpdateNestedAsync(Review review, int productId)
        {
            try
            {
                var idCheck = await _productContext.Products.Include(r => r.Review).FirstOrDefaultAsync(x => x.ProductId == productId);
                if (idCheck != null)
                {
                    idCheck.Review.Add(review);
                    await _productContext.SaveChangesAsync();
                    return review;
                }
                else
                {
                    return null;
                }
            }
           
            catch
            {
                throw new DbUpdateException("An error occurred while updating your review.");
            }
        }

        public async Task<Review> DeleteNestedAsync(int productId, int reviewId)
        {
            try
            {
                var idCheck = await _productContext.Reviews.FirstOrDefaultAsync(x => x.ProductId == productId || x.ReviewId == reviewId);
                if(idCheck != null)
                {
                    _productContext.Remove(idCheck);
                    await _productContext.SaveChangesAsync();
                    return idCheck;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception("An error occured while deleting the review");
            }
        }
    }
}
