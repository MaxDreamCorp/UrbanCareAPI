using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.UserOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("get_my_user_data")]
        public async Task<IActionResult> GetMyUserData()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var qry = new GetUserDataQuery(userId);
            var userData = await _mediator.Send(qry);
            return Ok(userData);
        }

        [HttpGet("check_is_employee")]
        public async Task<IActionResult> CheckIsEmployee(
            [FromQuery] int userId)
        {
            var cmd = new CheckIsEmployeeQuery(userId);
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        [HttpGet("get_all_management_companies")]
        public async Task<IActionResult> GetAllManagementCompanies()
        {
            var managementCompanies = await _mediator.Send(new GetAllManagementCompaniesQuery());
            return Ok(managementCompanies);
        }
    }
}
