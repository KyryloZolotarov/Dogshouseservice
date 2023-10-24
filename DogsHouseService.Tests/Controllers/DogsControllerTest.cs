using DogsHouseService.Host.Controllers;
using DogsHouseService.Host.Models;
using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Services;
using DogsHouseService.Host.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests.Controllers
{
    public class DogsControllerTest
    {
        [Fact]
        public async Task GetDogsAsync_ReturnesIEnumerableDogDto_Succesfully()
        {
            var dogsServiceMock = new Mock<IDogsService>();
            var paramMock = new GetDogsQweryParametrs();

            var dogDtoSucces = new DogDto()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            var dogsServiceResultMock = new List<DogDto>();

            dogsServiceResultMock.Add(dogDtoSucces);
            var dogsControllerResultMock = new List<DogDto>();
            dogsControllerResultMock.Add(dogDtoSucces);

            dogsServiceMock.Setup(h => h.GetDogsAsync(It.IsAny<GetDogsQweryParametrs>(), It.IsAny<CancellationToken>())).ReturnsAsync(dogsServiceResultMock);

            var dogsController = new DogsController(
                dogsServiceMock.Object);

            var result = await dogsController.GetDogsAsync(paramMock, CancellationToken.None);
            Assert.NotNull(result);

            dogsServiceMock.Verify(x => x.GetDogsAsync(It.IsAny<GetDogsQweryParametrs>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetDogsAsync_ThrowsException_Failed()
        {
            var dogsServiceMock = new Mock<IDogsService>();
            var paramMock = new GetDogsQweryParametrs();

            var dogDtoSucces = new DogDto()
            {};

            var dogsServiceResultMock = new List<DogDto>();

            dogsServiceResultMock.Add(dogDtoSucces);
            var dogsControllerResultMock = new List<DogDto>();
            dogsControllerResultMock.Add(dogDtoSucces);

            dogsServiceMock.Setup(h => h.GetDogsAsync(It.IsAny<GetDogsQweryParametrs>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var dogsController = new DogsController(
                dogsServiceMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => {
                var result = await dogsController.GetDogsAsync(paramMock, CancellationToken.None);
            });
        }
    }
}
