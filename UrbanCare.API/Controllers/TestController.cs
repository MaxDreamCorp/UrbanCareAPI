using Microsoft.AspNetCore.Mvc;
using UrbanCare.Domain.Interfaces.Repositories;

namespace UrbanCare.API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        public TestController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet("check_requesting")]
        public IActionResult CheckRequesting()
        {
            return Ok(new TestDTO("Succes!"));
        }

        [HttpGet("get_next_id")]
        public async Task<IActionResult> GetNextId()
        {
            var res = await _regionRepository.GetNextIdAsync();

            return Ok(res);
        }
    }

    public record TestDTO(string Response);
}
