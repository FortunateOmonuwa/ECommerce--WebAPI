using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.CategoryDTO;
using DataAccess.DTO.ProductDTO;
using DataAccess.Repository;
using Domain.Contract;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace E_Commerce_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBaseContract<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(IBaseContract<Category> categoryRepository, IMapper mapper) 
        {
            _categoryRepository= categoryRepository;
            _mapper= mapper;
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDTO category)
        {
            try
            {
                if(category != null)
                {
                    var map = _mapper.Map<Category>(category);
                    await _categoryRepository.AddAsync(map);

                    var mappedCategory = _mapper.Map<CategoryGetDTO>(map);
                    return CreatedAtAction(nameof(GetCategoryByID), new { id = map.CategoryId }, mappedCategory);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryByID(int id)
        {
            try
            {
                var check = await _categoryRepository.GetByIdAsync(id);
                if (check != null)
                {
                    var category = _mapper.Map<CategoryGetDTO>(check);
                    return Ok(category);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var query = await _categoryRepository.GetAllAsync();
                var category = _mapper.Map<List<CategoryGetDTO>>(query);
                return Ok(category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpGet]
        //[Route("{filter}/Name-or-CategoryId")]
        //public async Task<IActionResult> GetCategoriesByFiltering(string name, int categoryId)
        //{
        //    try
        //    {
        //        Expression<Func<Category, bool>> filter = r => r.CategoryId == categoryId || r.Name == name;

        //        var filteredCategory = await _categoryRepository.GetFiltered(filter);
        //        if (filteredCategory != null)
        //        {
        //            var mapFilter = _mapper.Map<CategoryGetDTO>(filteredCategory);
        //            return Ok(mapFilter);
        //        }
        //        else
        //        {
        //            return BadRequest("Please input a filter condition");
        //        }


        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryCreateDTO category, int id)
        {
            try
            {
                var update = _mapper.Map<Category>(category);
                update.CategoryId = id;
                if (update != null)
                {
                    await _categoryRepository.UpdateAsync(update);
                    var mappedProduct = _mapper.Map<CategoryGetDTO>(update);
                    return CreatedAtAction(nameof(GetCategoryByID), new { id = update.CategoryId }, mappedProduct);
                }
                else
                {
                    return BadRequest("Category doesn't exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.DeleteAsync(id);
                if (category != null)
                {
                    return Ok("Successfully Deleted");
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
