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
    public class CategoryRepository : IBaseContract<Category>
    {
        private readonly DataContext _categoryContext;
        public CategoryRepository(DataContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            try
            {
                if (category != null)
                {
                    await _categoryContext.Categories.AddAsync(category);
                    await _categoryContext.SaveChangesAsync();
                    return category;
                }
                else
                {
                    throw new ArgumentException("The category cannot be null or empty.", nameof(category));
                }
            }

            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while adding your category.", ex);
            }
        }
        public async Task<IQueryable<Category>> GetAllAsync()
        {
            try
            {
                var getCategories = await _categoryContext.Categories.ToListAsync();
                return getCategories.AsQueryable();
            }

            catch
            {
                throw new Exception("An error occurred while retrieving the categories.");
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryContext.Categories.FirstOrDefaultAsync(r => r.CategoryId == id);
                if (category != null)
                {
                    return category;
                }
                else
                {
                    throw new Exception($"Category with ID {id} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while retrieving the category.");
            }
        }

        public async Task<IEnumerable<Category>> GetFiltered(Expression<Func<Category, bool>> filtered)
        {
            try
            {
                var filter = await _categoryContext.Categories.Where(filtered).ToListAsync();
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
                throw new Exception("An error occured while tryin to retrieve the Category");
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                var categoryCheck = await _categoryContext.Categories.FindAsync(category.CategoryId);
                if (categoryCheck != null)
                {
                    _categoryContext.Categories.Update(categoryCheck);
                    await _categoryContext.SaveChangesAsync();
                    return categoryCheck;
                }
                else
                {
                    throw new Exception($"");
                }

            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while updating your category.", ex);
            }
        }
        public async Task<Category> DeleteAsync(int id)
        {
            try
            {
                var delete = await _categoryContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (delete != null)
                {
                    _categoryContext.Categories.Remove(delete);
                    await _categoryContext.SaveChangesAsync();
                    return delete;

                }
                else
                {
                    throw new Exception($"Category with ID {id} was not found.");

                }
            }
            catch
            {
                throw new Exception("An error occurred while retrieving the Category.");
            }
        }
    }
}
