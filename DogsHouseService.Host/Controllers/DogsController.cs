using DogsHouseService.Host.Models;
using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DogsHouseService.Host.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogsService;

        public DogsController(IDogsService dogsService)
        {
            _dogsService = dogsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DogDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Dogs([FromQuery] GetDogsQweryParametrs param)
        {
            var result = await _dogsService.GetDogs();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Dog(string name, string color, int tailLength, int weight)
        {
            var dog = new DogDto() { Name = name, Color = color, TailLength = tailLength, Weight = weight };
            await _dogsService.AddDog(dog);
            return Ok();
        }
    }
}
