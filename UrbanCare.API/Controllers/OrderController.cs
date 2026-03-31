using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.OrderOperations.Commands;
using UrbanCare.Application.Features.OrderOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "ResidentPolicy")]
        [HttpPost("create_order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand cmd)
        {
            try
            {
                var response = await _mediator.Send(cmd);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Policy = "ResidentPolicy")]
        [HttpPost("update_order")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderFromResidentCommand cmd)
        {
            try
            {
                var response = await _mediator.Send(cmd);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_order_categories")]
        public async Task<IActionResult> GetOrderCategories()
        {
            var qry = new GetOrderCategoriesQuery();

            var response = await _mediator.Send(qry);       
            return Ok(response);
        }
     
        [HttpGet("get_order_statuses")]
        public async Task<IActionResult> GetOrderStatuses()
        {
            var qry = new GetOrderStatusesQuery();

            var response = await _mediator.Send(qry);       
            return Ok(response);
        }
     
        [HttpGet("get_priorities")]
        public async Task<IActionResult> GetPriorities()
        {
            var qry = new GetPrioritiesQuery();

            var response = await _mediator.Send(qry);       
            return Ok(response);
        }
    }
}
