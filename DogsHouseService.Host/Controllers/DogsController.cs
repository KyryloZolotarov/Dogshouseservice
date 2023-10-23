using DogsHouseService.Host.Models;
using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DogsHouseService.Host.Controllers
{
    [Route("dogs")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogsService;

        public DogsController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }        

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DogDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDogsAsync([FromQuery] GetDogsQweryParametrs param, CancellationToken cancellationToken)
        {
            var result = await _dogsService.GetDogsAsync(param, cancellationToken);
            return Ok(result);
        }
    }
}
