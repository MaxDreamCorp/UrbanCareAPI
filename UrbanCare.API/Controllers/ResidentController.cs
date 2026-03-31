using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.OrderOperations.Queries;
using UrbanCare.Application.Features.ResidentOperations.Commands;
using UrbanCare.Application.Features.ResidentOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/resident")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResidentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("create_resident")]
        public async Task<IActionResult> CreateResident(CreateResidentCommand cmd)
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
        [HttpGet("get_my_resident_data")]
        public async Task<IActionResult> GetMyResidentData()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var qry = new GetResidentByUserIdQuery(userId);
            var response = await _mediator.Send(qry);
            return Ok(response);
        }

        [Authorize(Policy = "ResidentPolicy")]
        [HttpGet("get_my_orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();
            try
            {
                var qry = new GetResidentByUserIdQuery(userId);
                var resident = await _mediator.Send(qry);

                if (resident == null)
                    throw new Exception("Данного жителя не существует");

                var qry2 = new GetOrdersByResidentId(resident.Id);
                var response = await _mediator.Send(qry2);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

