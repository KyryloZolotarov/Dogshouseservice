using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DogsHouseService.Host.Controllers
{
    [Route("dog")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IDogsService _dogsService;

        public DogController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddDogAsync([FromBody] DogDto dog, CancellationToken cancellationToken)
        {
            await _dogsService.AddDogAsync(dog, cancellationToken);
            return Ok();
        }
    }
}
