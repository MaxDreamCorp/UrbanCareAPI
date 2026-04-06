using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.ExecutorOperations.Commands;

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
    }
}