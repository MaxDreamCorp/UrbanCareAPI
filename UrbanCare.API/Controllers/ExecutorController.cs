using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.ExecutorOperations.Commands;
using UrbanCare.Application.Features.ExecutorOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/executor")]
    [Authorize(Policy = "ExecutorPolicy")]
    [ApiController]
    public class ExecutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExecutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get_executor_orders")]
        public async Task<IActionResult> GetExecutorOrders()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var query = new GetExecutorOrdersQuery(userId);

            try
            {
                var orders = await _mediator.Send(query);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update_status_to_available")]
        public async Task<IActionResult> UpdateStatusToAvailable()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var cmd = new UpdateStatusToAvailableCommand(userId);

            try
            {
                await _mediator.Send(cmd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update_status_to_on_order")]
        public async Task<IActionResult> UpdateStatusToOnOrder()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var cmd = new UpdateStatusToOnOrderCommand(userId);

            try
            {
                await _mediator.Send(cmd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("mark_as_completed/{orderId}")]
        public async Task<IActionResult> MarkAsCompleted(int orderId)
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var cmd = new MarkAsCompletedByExecutorCommand(userId, orderId);

            try
            {
                await _mediator.Send(cmd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("accept_order/{orderId}")]
        public async Task<IActionResult> AcceptOrder(int orderId)
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var cmd = new AcceptOrderCommand(userId, orderId);

            try
            {
                await _mediator.Send(cmd);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}