using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.Employees.Commands;
using UrbanCare.Application.Features.Employees.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create_admin")]
        public async Task<IActionResult> CreateAdmin(CreateAdminCommand cmd)
        {

            var response = await _mediator.Send(cmd);

            if (response != null)
                return BadRequest(response);

            return Ok();
        }

        [HttpPost("create_dispatcher")]
        public async Task<IActionResult> CreateDispatcher(CreateDispatcherCommand cmd)
        {
            try
            {
                var result = await _mediator.Send(cmd);

                if (!result)
                    return BadRequest("Не удалось создать диспетчера");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create_executor")]
        public async Task<IActionResult> CreateExecutor(CreateExecutorCommand cmd)
        {
            try
            {
                var result = await _mediator.Send(cmd);

                if (!result)
                    return BadRequest("Не удалось создать исполнителя");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpGet("get_my_employee")]
        public async Task<IActionResult> GetMyEmployee()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var qry = new GetEmployeeByUserIdQuery(userId);

            var response = await _mediator.Send(qry);

            return Ok(response);
        }

        [HttpGet("get_all_employee_positions")]
        public async Task<IActionResult> GetAllEmployeePositions()
        {
            var qry = new GetAllEmployeePositionsQuery();

            var response = await _mediator.Send(qry);

            return Ok(response);
        }

        [HttpGet("get_all_employee_statuses")]
        public async Task<IActionResult> GetAllEmployeeStatuses()
        {
            var qry = new GetAllEmployeeStatusesQuery();

            var response = await _mediator.Send(qry);

            return Ok(response);
        }

        [HttpGet("get_all_qualification_categories_names")]
        public async Task<IActionResult> GetAllQualificationCategoriesNames()
        {
            var qry = new GetAllQualificationCategoriesNamesQuery();

            var response = await _mediator.Send(qry);

            return Ok(response);
        }
    }
}
