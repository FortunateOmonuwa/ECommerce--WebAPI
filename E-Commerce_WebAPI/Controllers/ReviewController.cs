using AutoMapper;
using DataAccess.DTO;
using Domain.Contract;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace E_Commerce_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IBaseContract<Review> _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IBaseContract<Review> reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;

        }


        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDTO review)
        {
            try
            {
                var map = _mapper.Map<Review>(review);
                await _reviewRepository.AddAsync(map);

                var mappedReview = _mapper.Map<ReviewGetDTO>(map);
                return CreatedAtAction(nameof(GetReviewByID), new { id = map.ReviewId }, mappedReview);
            }
            catch
            {
                return Conflict();
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReviewByID(int id)
        {
            try
            {
                var check = await _reviewRepository.GetByIdAsync(id);
                if (check != null)
                {
                    var map = _mapper.Map<ReviewGetDTO>(check);
                    return Ok(map);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest("An error has occured");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var query = await _reviewRepository.GetAllAsync();
                var reviews = _mapper.Map<List<ReviewGetDTO>>(query);
                return Ok(reviews);
            }
            catch
            {
                return NotFound();
            }
        }

        //[HttpGet]
        //[Route("{condition}/filter-by-productId-or-customerId-or-reviewId")]
        //public async Task<IActionResult> GetFilteredReview(int productId, int customerId, int reviewId)
        //{
        //    try
        //    {
        //        Expression<Func<Review, bool>> filter = r => r.CustomerId == customerId || r.ReviewId == reviewId || r.ProductId == productId;

        //        var filteredReviews = await _reviewRepository.GetFiltered(filter);
        //        if (filteredReviews != null)
        //        {
        //            var mapFilter = _mapper.Map<ReviewGetDTO>(filteredReviews);
        //            return Ok(mapFilter);
        //        }
        //        else
        //        {
        //            return BadRequest("Please input a filter condition");
        //        }
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewCreateDTO review, int id)
        {
            try
            {
                var update = _mapper.Map<Review>(review);
                update.ReviewId = id;
                if(update != null)
                {
                    await _reviewRepository.UpdateAsync(update);
                    var map = _mapper.Map<ReviewGetDTO>(update);
                    return CreatedAtAction(nameof(GetReviewByID), new { id = update.ReviewId }, map);
                }
                else
                {
                    return BadRequest("Review doesn't exist");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var review = await _reviewRepository.DeleteAsync(id);
                if(review != null)
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
                return Conflict();
            }
            
        }

        //[HttpGet]
        //[Route("{reviewId}")]
        //public async Task<IActionResult> GetSortedReviews(int reviewId)
        //{
        //    try
        //    {
        //        Expression<Func<Review, bool>> reviewSort = r => r.ReviewId == reviewId;

        //        var sortedReviews = await _reviewRepository.GetSorted(reviewSort);
        //        if (sortedReviews != null)
        //        {
        //            return Ok(sortedReviews);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}



    }
}
