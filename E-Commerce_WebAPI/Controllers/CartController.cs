using AutoMapper;
using DataAccess.DTO.CartDTO;
using DataAccess.DTO.ProductDTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using Service.IServiceContracts;
using Service.OrderContract;

namespace E_Commerce_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cart;
        private IMapper _mapper;
        public CartController(ICartService cart, IMapper mapper)
        {
            _cart = cart;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("{customerId}")]
        public async Task<IActionResult> CreateCart([FromBody] CartCreateDTO cart, int customerId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (cart != null)
                    {
                        var map = _mapper.Map<Cart>(cart);
                        map.CustomerId = customerId;
                        await _cart.CreateCart(map, customerId);

                        var mappedCart = _mapper.Map<CartGetDTO>(map);
                        return CreatedAtAction(nameof(GetCartByIdd), new { cartId = map.CartId, customerId = customerId }, mappedCart);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status403Forbidden);
                    }
                }
                else
                {

                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{cartId}/cart/{customerId}")]
        public async Task<IActionResult> GetCartByIdd(int cartId, int customerId)
        {
            try
            {
                var cart = await _cart.GetCartById(cartId, customerId);
                if (cart == null)
                {
                    return Unauthorized();

                }
                else
                {
                    var mapCart = _mapper.Map<CartGetDTO>(cart);
                    return Ok(mapCart);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPut]
        //[Route("{cartId}/cart/{customerId}")]
        //public async Task<IActionResult> UpdateCart([FromBody] CartCreateDTO updateCart, int cartId, int customerId)
        //{
        //    try
        //    {
        //        if(ModelState.IsValid)
        //        {
        //            var cart = await _cart.GetCartById(cartId, customerId);
        //            if(cart!= null)
        //            {
        //                var map = _mapper.Map<Cart>(updateCart);
        //                map.CartId = cartId;
        //                map.CustomerId = customerId;
        //                var prod = await _cart.UpdateCart(map);
        //                var products = _mapper.Map<CartGetDTO>(prod);
        //                return Ok("Successfully updated");
        //            }
        //            else
        //            {
        //                return Unauthorized();
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("An error occured");
        //        }
        //    }
        //    catch(Exception ex )
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpDelete]
        [Route("{cartId}/cart/{customerId}")]
        public async Task<IActionResult> DeleteCart(int cartId, int customerId)
        {
            try
            {
                var cart = await _cart.GetCartById(cartId, customerId);
                if(cart!= null )
                {
                    await _cart.DeleteCart(cartId);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("{customerId}/Product/{cartId}")]
        public async Task<IActionResult> AddProductsToCart([FromBody] ProductCartAddDTO product, int customerId, int cartId)
        {
            try
            {
                var mapProduct = _mapper.Map<Product>(product);
                if(mapProduct.ProductId != null)
                {
                    var productAdd = await _cart.AddProductToCart(mapProduct, cartId, customerId);
                    var prod = _mapper.Map<ProductGetDTO>(productAdd);

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

      
        [HttpGet]
        [Route("{customerId}/Products/{cartId}")]
        public async Task<IActionResult> GetAllProductsInCart(int customerId, int cartId)
        {
            try
            {
                var products = await _cart.GetAllCartProducts(customerId, cartId);
                var mapProducts = _mapper.Map<List<ProductGetDTO>>(products);
                return Ok(mapProducts);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{cartId}/TotalProductPrice/{customerId}")]
        public async Task<IActionResult> GetTotalProductPrice(int cartId, int customerId)
        {
            try
            {
                var price = await _cart.GetTotalPrice(cartId, customerId);
                return Ok(price);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{cartId}/Product/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(int cartId, int customerId)
        {
            try
            {
                var product = await _cart.RemoveProductFromCart(cartId, customerId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{cartId}/Products/{customerId}")]
        public async Task<IActionResult> ClearCart(int customerId, int cartId)
        {
            var products = await _cart.RemoveAllProductsFromCart(customerId, cartId);
            return Ok();
        }
    }
}
