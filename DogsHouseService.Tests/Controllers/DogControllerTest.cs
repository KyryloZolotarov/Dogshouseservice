using DogsHouseService.Host.Controllers;
using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Models;
using DogsHouseService.Host.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouseService.Tests.Controllers
{
    public class DogControllerTest
    {
        [Fact]
        public async Task GetDogsAsync_ReturnesStatusCodeOk_Succesfully()
        {
            var dogsServiceMock = new Mock<IDogsService>();

            var dogDtoSucces = new DogDto()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            dogsServiceMock.Setup(h => h.AddDogAsync(It.IsAny<DogDto>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var dogController = new DogController(
                dogsServiceMock.Object);

            var result = await dogController.AddDogAsync(dogDtoSucces, CancellationToken.None);
            Assert.Equal((int) HttpStatusCode.OK, ((OkResult)result).StatusCode);

            dogsServiceMock.Verify(x => x.AddDogAsync(It.IsAny<DogDto>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetDogsAsync_ThrowsException_Failed()
        {
            var dogsServiceMock = new Mock<IDogsService>();

            var dogDtoSucces = new DogDto()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            dogsServiceMock.Setup(h => h.AddDogAsync(It.IsAny<DogDto>(), CancellationToken.None)).Throws<Exception>();

            var dogController = new DogController(
                dogsServiceMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => {
                var result = await dogController.AddDogAsync(dogDtoSucces, CancellationToken.None);
            });
        }
    }
}
