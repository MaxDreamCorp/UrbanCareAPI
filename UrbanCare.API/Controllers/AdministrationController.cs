using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.AdministrationOperations.Commands;
using UrbanCare.Application.Features.AdministrationOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [Route("api/administration")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("management_company/get_my_management_company")]
        public async Task<IActionResult> GetMyManagementCompany()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var query = new GetAdminManagementCompanyQuery(userId);
            var userData = await _mediator.Send(query);
            return Ok(userData);
        }

        [HttpGet("management_company/get_management_company_employees")]
        public async Task<IActionResult> GetManagementCompanyEmployees()
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Forbid();

            var query = new GetManagementCompanyEmployeesQuery(userId);
            var userData = await _mediator.Send(query);
            return Ok(userData);
        }

        [HttpPost("region/create_region")]
        public async Task<IActionResult> CreateRegion(CreateRegionCommand cmd)
        {
            try
            {
                var userData = await _mediator.Send(cmd);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        [HttpPut("region/update_region")]
        public async Task<IActionResult> UpdateRegion(UpdateRegionCommand cmd)
        {
            try
            {
                var userData = await _mediator.Send(cmd);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("region/delete_region/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var cmd = new DeleteRegionCommand(id);
            try
            {
                var isDeleted = await _mediator.Send(cmd);

                if (isDeleted)
                    return NoContent();
                else
                    return BadRequest("Не удалось удалить элемент");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("building/create_building")]
        public async Task<IActionResult> CreateBuilding(CreateBuildingCommand cmd)
        {
            try
            {
                var userData = await _mediator.Send(cmd);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("building/update_building")]
        public async Task<IActionResult> UpdateBuilding(UpdateBuildingCommand cmd)
        {
            try
            {
                var userData = await _mediator.Send(cmd);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("building/delete_building/{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var cmd = new DeleteBuildingCommand(id);
            try
            {
                var isDeleted = await _mediator.Send(cmd);

                if (isDeleted)
                    return NoContent();
                else
                    return BadRequest("Не удалось удалить элемент");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("apartment/create_apartment")]
        public async Task<IActionResult> CreateApartment(CreateApartmentCommand cmd)
        {
            try
            {
                var userData = await _mediator.Send(cmd);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("apartment/update_apartment")]
        public async Task<IActionResult> UpdateApartment(UpdateApartmentCommand cmd)
        {
            try
            {
                var userData = await _mediator.Send(cmd);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("apartment/delete_apartment/{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var cmd = new DeleteApartmentCommand(id);
            try
            {
                var isDeleted = await _mediator.Send(cmd);

                if (isDeleted)
                    return NoContent();
                else
                    return BadRequest("Не удалось удалить элемент");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }

}
