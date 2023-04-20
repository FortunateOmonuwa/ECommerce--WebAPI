using AutoMapper;
using DataAccess.DTO;
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
    public class ProductController : ControllerBase
    {
        private readonly IBaseContract<Product> _productRepository;
        private readonly IBaseContract2<Review> _reviewRepository;
        private readonly IMapper _mapper;
        public ProductController(IBaseContract<Product> productRepository, IMapper mapper, IBaseContract2<Review> reviewRepository)
        {
            _productRepository= productRepository;
            _reviewRepository = reviewRepository;
            _mapper= mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDTO product)
        {
            try
            {
                if(product != null)
                {
                    var map = _mapper.Map<Product>(product);
                    await _productRepository.AddAsync(map);

                    var mappedProducts = _mapper.Map<ProductGetDTO>(map);
                    return CreatedAtAction(nameof(GetProductByID), new { id = map.ProductId }, mappedProducts);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
               
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            try
            {
                var check = await _productRepository.GetByIdAsync(id);
                if (check != null)
                {
                    var product = _mapper.Map<ProductGetDTO>(check);
                    return Ok(product);
                }
                else
                {
                    return BadRequest("An error has occured while trying to get the product");
                }
            }
            catch
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var query = await _productRepository.GetAllAsync();
                var product = _mapper.Map<List<ProductGetDTO>>(query);
                return Ok(product);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        //[HttpGet]
        //[Route("{filter}/Name-or-ProductId")]
        //public async Task<IActionResult> GetProductsByFiltering(string name, int productId)
        //{
        //    try
        //    {
        //        Expression<Func<Product, bool>> filter = r => r.ProductId == productId || r.Name == name;

        //        var filteredProducts = await _productRepository.GetFiltered(filter);
        //        if (filteredProducts != null)
        //        {
        //            var mapFilter = _mapper.Map<ReviewGetDTO>(filteredProducts);
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
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreateDTO product, int id)
        {
            try
            {

                var update = _mapper.Map<Product>(product);
                update.ProductId = id;
                if (update != null)
                {
                    await _productRepository.UpdateAsync(update);
                    var mappedProduct = _mapper.Map<ProductGetDTO>(update);
                    return CreatedAtAction(nameof(GetProductByID), new { id = update.ProductId }, mappedProduct);
                }
                else
                {
                    return BadRequest("Product doesn't exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productRepository.DeleteAsync(id);
                if (product != null)
                {
                    return Ok("Successfully Deleted");
                }
                else
                {
                    return NotFound("Doesn't exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("{productId}/reviews")]
        public async Task<IActionResult> AddReviewToProduct([FromBody] ReviewCreateDTO productReview, int productId)
        {
            try
            {
                if (productReview != null)
                {
                    var map = _mapper.Map<Review>(productReview);
                    //map.ProductId = productId;
                    await _reviewRepository.AddNestedAsync(map, productId);
                    var mapReview = _mapper.Map<ReviewGetDTO>(map);
                    return CreatedAtAction(nameof(GetProductReviewsById), new { productId = productId , reviewId = mapReview.ReviewId}, mapReview);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("{productId}/reviews")]
        public async Task<IActionResult> GetAllProductReviews(int productId)
        {
            try
            {
                var checkId = await _reviewRepository.GetAllNestedAsync(productId);
                if (checkId != null)
                {
                    var review = _mapper.Map<List<ReviewGetDTO>>(checkId);
                    return Ok(review);
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
        [Route("{productId}/reviews/{reviewId}")]
        public async Task<IActionResult> GetProductReviewsById(int productId, int reviewId)
        {
            try
            {
                var idCheck = await _reviewRepository.GetNestedByIdAsync(productId, reviewId);
                if (idCheck != null)
                {
                    var review = _mapper.Map<ReviewGetDTO>(idCheck);
                    return Ok(review);
                }
                else
                {
                    return NotFound($"Review with id {reviewId} doesn't exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{productId}/reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReviewFromProduct(int productId, int reviewId)
        {
            try
            {
                var review = await _reviewRepository.DeleteNestedAsync(productId, reviewId);
                if (review != null)
                {
                    return Ok("Successfully Deleted");
                }
                else
                {
                    return NotFound($"Review with Id {reviewId} does not exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{productId}/reviews/{reviewId}")]
        public async Task<IActionResult> UpdateProductReview(int productId, [FromBody] ReviewCreateDTO review)
        {
            try
            {
                var map = _mapper.Map<Review>(review);
                map.ProductId = productId;
                if(map != null)
                {
                    await _reviewRepository.UpdateNestedAsync(map, productId);
                    return Ok("Successful");
                }
                else
                {
                    return NoContent();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
