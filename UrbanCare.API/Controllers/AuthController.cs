using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrbanCare.Application.Features.UserOperations.Commands;

namespace UrbanCare.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("registrate")]
        public async Task<IActionResult> Registrate(RegistrationCommand cmd)
        {
            var response = await _mediator.Send(cmd);

            if (response.Errors is not null)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPost("log_in")]
        public async Task<IActionResult> LogIn(LogInCommand cmd)
        {
            var response = await _mediator.Send(cmd);

            if (response.Errors is not null)
                return BadRequest(response.Errors);

            return Ok(response);
        }


    }
}
