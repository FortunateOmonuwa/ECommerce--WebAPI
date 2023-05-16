using AutoMapper;
using DataAccess.DTO.OrdersDTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OrderContract;

namespace E_Commerce_WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _order;
        private readonly IMapper _mapper;
        public OrderController(IOrderService order, IMapper mapper)
        {
            _mapper= mapper;
            _order = order;
        }

        [HttpPost]
        [Route("{customerId}/Order/{cartId}")]
        public async Task<IActionResult> PlaceOrder(OrdersCreateDTO order, int customerId, int cartId)
        {
            try
            {
                var newOrder = _mapper.Map<Order>(order);
                var placeOrder = await _order.PlaceOrder(newOrder, customerId, cartId);
                var mapOrder = _mapper.Map<OrdersGetDTO>(placeOrder);
                return CreatedAtAction(nameof(GetOrderById), new { customerId = customerId, orderId = mapOrder.OrderId }, mapOrder);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _order.GetAllOrders();
                var mappedOrders = _mapper.Map<IEnumerable<OrdersGetDTO>>(orders);
                return Ok(mappedOrders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{customerId}/Orders")]
        public async Task<IActionResult> GetAllCustomerOrders(int customerId)
        {
            try
            {
                var orders = await _order.GetAllCustomerOrders(customerId);
                if (orders == null)
                {
                    return NotFound();
                }
                var mappedOrders = _mapper.Map<IEnumerable<OrdersGetDTO>>(orders);
                return Ok(mappedOrders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{customerId}/Order/{orderId}")]
        public async Task<IActionResult> GetOrderById(int customerId, int orderId)
        {
            try
            {
                var order = await _order.GetOrderById(customerId, orderId);
                if (order == null)
                {
                    return NotFound();
                }
                var mappedOrder = _mapper.Map<OrdersGetDTO>(order);
                return Ok(mappedOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{customerId}/order/{orderId}/cart/{cartId}/price")]
        public async Task<IActionResult> GetTotalPrice(int customerId, int orderId, int cartId)
        {
            try
            {
                var totalPrice = await _order.GetTotalPrice(customerId, orderId, cartId);
                if (totalPrice == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(totalPrice);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{customerId}/order/{orderId}")]
        public async Task<IActionResult> UpdateOrder(OrdersCreateDTO orderUpdateDTO, int customerId, int orderId)
        {
            try
            {
                var order = _mapper.Map<Order>(orderUpdateDTO);
                var success = await _order.UpdateOrder(order, customerId, orderId);
                if (success)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{customerId}/order/{orderId}")]
        public async Task<IActionResult> CancelOrder(int customerId, int orderId)
        {
            try
            {
                var success = await _order.CancelOrder(customerId, orderId);
                if (success)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
