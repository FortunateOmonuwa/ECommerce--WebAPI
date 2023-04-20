using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.CustomerDTO;
using DataAccess.DTO.OrderDetailsDTO;
using DataAccess.DTO.OrdersDTO;
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
    public class CustomersController : ControllerBase
    {
        private readonly IBaseContract<Customer> _customer;
        private readonly IBaseContractNR<Review> _review;
        private readonly IMapper _mapper;

        public CustomersController(IBaseContract<Customer> customer, IBaseContractNR<Review> review, IMapper mapper)
        {
            _customer = customer;
            _review = review;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateDTO customer)
        {
            try
            {
                if (customer != null)
                {
                    var map = _mapper.Map<Customer>(customer);
                    await _customer.AddAsync(map);

                    var mappedCustomer = _mapper.Map<CustomerGetDTO>(map);
                    return CreatedAtAction(nameof(GetCustomerByID), new { customerId = map.CustomerId }, mappedCustomer);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> GetCustomerByID(int customerId)
        {
            try
            {
                var check = await _customer.GetByIdAsync(customerId);
                if (check != null)
                {
                    var customer = _mapper.Map<CustomerGetDTO>(check);
                    return Ok(customer);
                }
                else
                {
                    return BadRequest("Unsuccessful");
                }
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var query = await _customer.GetAllAsync();
                var customer = _mapper.Map<List<CustomerGetDTO>>(query);
                return Ok(customer);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //[HttpGet]
        //[Route("{customerId}/Filter")]
        //public async Task<IActionResult> GetCustomersByFiltering(int customerId)
        //{
        //    try
        //    {
        //        Expression<Func<Customer, bool>> filter = r => r.CustomerId == customerId;

        //        var customer = await _customer.GetFiltered(filter);
        //        if (customer != null)
        //        {
        //            var mapFilter = _mapper.Map<CustomerGetDTO>(customer);
        //            return Ok(mapFilter);
        //        }
        //        else
        //        {
        //            return BadRequest("");
        //        }


        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpPut]
        [Route("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerCreateDTO customer, int customerId)
        {
            try
            {

                var update = _mapper.Map<Customer>(customer);
                update.CustomerId = customerId;
                if (update != null)
                {
                    await _customer.UpdateAsync(update);
                    var mappedCustomer = _mapper.Map<CustomerGetDTO>(update);
                    return CreatedAtAction(nameof(GetCustomerByID), new { customerId = update.CustomerId }, mappedCustomer);
                }
                else
                {
                    return BadRequest("Customer doesn't exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            try
            {
                var customer = await _customer.DeleteAsync(customerId);
                if (customer != null)
                {
                    return Ok("Successfully Deleted");
                }
                else
                {
                    return NotFound("Customer doesn't exist");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //[HttpPost]
        //[Route("{customerId}/Orders")]
        //public async Task<IActionResult> AddCustomerOrder([FromBody] OrdersCreateDTO order, int customerId)
        //{
        //    try
        //    {
        //        if (order != null)
        //        {
        //            var map = _mapper.Map<Order>(order);
        //            map.CustomerId = customerId;
        //            await _order.AddNestedAsync(map, customerId);
        //            var mappedOrder = _mapper.Map<OrdersGetDTO>(map);
        //            return CreatedAtAction(nameof(GetCustomerOrderById), new { customerId = customerId, orderId = mappedOrder.OrderId }, mappedOrder);
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }
        //}
        //[HttpGet]
        //[Route("{customerId}/Order/{orderId}")]
        //public async Task<IActionResult> GetCustomerOrderById(int customerId, int orderId)
        //{
        //    try
        //    {
        //        var order = await _order.GetNestedByIdAsync(customerId, orderId);
        //        order.CustomerId = customerId;
        //        if (order != null)
        //        {
        //            var mappedOrder = _mapper.Map<OrdersGetDTO>(order);
        //            return Ok(mappedOrder);
        //        }
        //        else
        //        {
        //            return NotFound($"Order with id {orderId} doesn't exist");
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //[HttpGet]
        //[Route("{customerId}/Orders")]
        //public async Task<IActionResult> GetAllCustomerOrders(int customerId)
        //{
        //    try
        //    {
        //        var checkId = await _order.GetAllNestedAsync(customerId);
        //        if (checkId != null)
        //        {
        //            var order = _mapper.Map<List<OrdersGetDTO>>(checkId);
        //            return Ok(order);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //[HttpDelete]
        //[Route("{customerId}/Order/{orderId}")]
        //public async Task<IActionResult> DeleteCustomerOrder(int customerId, int orderId)
        //{
        //    try
        //    {
        //        var order = await _order.DeleteNestedAsync(customerId, orderId);
        //        if (order != null)
        //        {
        //            return Ok("Successfully Deleted");
        //        }
        //        else
        //        {
        //            return NotFound($"Order with Id {orderId} does not exist");
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpPut]
        //[Route("{customerId}/Orders/{orderId}")]
        //public async Task<IActionResult> UpdateCustomerOrder(int customerId, [FromBody] OrdersCreateDTO order)
        //{
        //    try
        //    {
        //        var map = _mapper.Map<Order>(order);
        //        map.CustomerId = customerId;
        //        if (map != null)
        //        {
        //            await _order.UpdateNestedAsync(map, customerId);
        //            return Ok("Successfully updated");
        //        }
        //        else
        //        {
        //            return NoContent();
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }
}
