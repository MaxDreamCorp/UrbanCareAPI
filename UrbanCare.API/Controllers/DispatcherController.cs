using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.DispatcherOperations.Queries;

namespace UrbanCare.API.Controllers
{
    [Route("api/dispatcher")]
    [ApiController]
    [Authorize(Policy = "DispatcherPolicy")]
    public class DispatcherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DispatcherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get_company_executors/{managementCompanyId}")]
        public async Task<IActionResult> GetCompanyExecutors(int managementCompanyId)
        {
            try
            {
                var qry = new GetCompanyExecutorsQuery(managementCompanyId);
                var response = await _mediator.Send(qry);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_company_new_orders/{managementCompanyId}")]
        public async Task<IActionResult> GetCompanyNewOrders(int managementCompanyId)
        {
            try
            {
                var qry = new GetCompanyNewOrdersQuery(managementCompanyId);
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
