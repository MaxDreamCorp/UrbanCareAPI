using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.CompanyOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/company")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get_roles")]
        public async Task<IActionResult> GetRoles()
        {
            var qry = new GetRolesQuery();

            var response = await _mediator.Send(qry);
            return Ok(response);
        }

        [HttpGet("region/get_all_regions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var qry = new GetAllRegionsQuery();

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("region/get_regions_by_management_company")]
        public async Task<IActionResult> GetRegionsByManagementCompany(int managementCompanyId)
        {
            var qry = new GetRegionsByManagementCompanyQuery(managementCompanyId);

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("building/get_building_types")]
        public async Task<IActionResult> GetBuildingTypes()
        {
            var qry = new GetBuildingTypesQuery();

            var response = await _mediator.Send(qry);
            return Ok(response);
        }

        [HttpGet("building/get_floor_materials")]
        public async Task<IActionResult> GetFloorMaterials()
        {
            var qry = new GetFloorMaterialsQuery();

            var response = await _mediator.Send(qry);
            return Ok(response);
        }

        [HttpGet("building/get_wall_materials")]
        public async Task<IActionResult> GetWallMaterials()
        {
            var qry = new GetWallMaterialsQuery();

            var response = await _mediator.Send(qry);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("building/get_buildings_by_management_company")]
        public async Task<IActionResult> GetBuildingsByManagementCompany(int managementCompanyId)
        {
            var qry = new GetBuildingsByManagementCompanyQuery(managementCompanyId);

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("building/get_buildings_by_region")]
        public async Task<IActionResult> GetBuildingsByRegion(int regionId)
        {
            var qry = new GetBuildingsByRegionQuery(regionId);

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("apartment/get_apartments_by_management_company")]
        public async Task<IActionResult> GetApartmentsByManagmentCompany(int managementCompanyId)
        {
            var qry = new GetApartmentsByManagmentCompanyQuery(managementCompanyId);

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("apartment/get_apartments_by_building")]
        public async Task<IActionResult> GetApartmentsBybuilding(int buildingId)
        {
            var qry = new GetApartmentsByBuildingQuery(buildingId);

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpGet("material/get_company_materials/{managementCompanyId}")]
        public async Task<IActionResult> GetCompanyMaterials(int managementCompanyId)
        {
            var qry = new GetCompanyMaterialsQuery(managementCompanyId);

            try
            {
                var response = await _mediator.Send(qry);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
