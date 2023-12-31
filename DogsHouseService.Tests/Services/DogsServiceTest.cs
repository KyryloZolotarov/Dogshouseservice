﻿using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DogsHouseService.Host.Repositories.Interfaces;
using DogsHouseService.Host.Services;
using Microsoft.Extensions.Logging;
using DogsHouseService.Host.Data;
using DogsHouseService.Host.Services.Interfaces;
using Moq;
using Microsoft.EntityFrameworkCore.Storage;
using DogsHouseService.Host.Data.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace DogsHouseService.Tests.Services
{
    public class DogsServiceTest
    {
        [Fact]
        public async Task GetDogsAsync_Returns_IEnumerableDogDto_Seccesfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<DogsService>>();
            var dogsRepositoryMock = new Mock<IDogsRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransactionMock.Object);

            var dogsRepositoryResultMock = new List<DogEntity>();
            var dogsServiceResultMock = new List<DogDto>();

            var dogEntitySucces = new DogEntity()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            var dogDtoSucces = new DogDto()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            var cancellationTokenMock = new CancellationToken();

            var paramMock = new GetDogsQueryParametrs() 
            {
                Attribute = "color",
                Order = "asc"
            };

            dogsRepositoryResultMock.Add(dogEntitySucces);
            dogsServiceResultMock.Add(dogDtoSucces);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<DogDto>(
                It.Is<DogEntity>(i => i.Equals(dogEntitySucces)))).Returns(dogDtoSucces);


            dogsRepositoryMock.Setup(h => h.GetDogsAsync(It.IsAny<GetDogsQueryParametrs>())).ReturnsAsync(dogsRepositoryResultMock);

            var dogsService = new DogsService(
                dogsRepositoryMock.Object,
                loggerMock.Object,
                dbContextWrapperMock.Object,                               
                mapperMock.Object);

            var result = await dogsService.GetDogsAsync(paramMock, cancellationTokenMock);
            Assert.NotNull(result);
            if (result == null)
            {
                return;
            }

            Assert.Equal(dogsServiceResultMock, result);
            dogsRepositoryMock.Verify(x => x.GetDogsAsync(It.IsAny<GetDogsQueryParametrs>()), Times.Once());
        }

        [Fact]
        public async Task GetDogsAsync_Throws_ArgumentNullException_Failed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<DogsService>>();
            var dogsRepositoryMock = new Mock<IDogsRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransactionMock.Object);

            var dogsRepositoryResultMock = new List<DogEntity>();
            var dogsServiceResultMock = new List<DogDto>();

            var cancellationTokenMock = new CancellationToken();

            var paramMock = new GetDogsQueryParametrs() 
            {
                Attribute = "tailLength",
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<List<DogDto>>(
                It.Is<List<DogEntity>>(i => i.Equals(dogsRepositoryMock)))).Returns(dogsServiceResultMock);


            dogsRepositoryMock.Setup(h => h.GetDogsAsync(It.IsAny<GetDogsQueryParametrs>())).ReturnsAsync((Func<List<DogEntity>>)null!);

            var dogsService = new DogsService(
                dogsRepositoryMock.Object,
                loggerMock.Object,
                dbContextWrapperMock.Object,
                mapperMock.Object);

            
            await Assert.ThrowsAsync<ArgumentNullException>(async () => { 
                var result = await dogsService.GetDogsAsync(paramMock, cancellationTokenMock);
            });
        }
        [Fact]
        public async Task AddDogAsync_Succesfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<DogsService>>();
            var dogsRepositoryMock = new Mock<IDogsRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransactionMock.Object);

            var dogEntitySucces = new DogEntity()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            var dogDtoSucces = new DogDto()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<DogEntity>(
                It.Is<DogDto>(i => i.Equals(dogDtoSucces)))).Returns(dogEntitySucces);

            dogsRepositoryMock.Setup(h => h.AddDogAsync(It.IsAny<DogEntity>())).Returns(Task.CompletedTask);

            var dogsService = new DogsService(
                dogsRepositoryMock.Object,
                loggerMock.Object,
                dbContextWrapperMock.Object,
                mapperMock.Object);

            await dogsService.AddDogAsync(dogDtoSucces, CancellationToken.None);
            mapperMock.Verify(m => m.Map<DogEntity>(dogDtoSucces), Times.Once);
            dogsRepositoryMock.Verify(r => r.AddDogAsync(dogEntitySucces), Times.Once);
        }
        [Fact]
        public async Task AddDogAsync_Failed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<DogsService>>();
            var dogsRepositoryMock = new Mock<IDogsRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransactionMock.Object);

            var dogEntitySucces = new DogEntity();

            var dogDtoSucces = new DogDto();

            var cancellationTokenMock = new CancellationToken();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<DogDto>(
                It.Is<DogEntity>(i => i.Equals(dogEntitySucces)))).Returns(dogDtoSucces);

            dogsRepositoryMock.Setup(h => h.AddDogAsync(It.IsAny<DogEntity>())).Returns(Task.CompletedTask);

            var dogsService = new DogsService(
                dogsRepositoryMock.Object,
                loggerMock.Object,
                dbContextWrapperMock.Object,
                mapperMock.Object);

            await dogsService.AddDogAsync(dogDtoSucces, cancellationTokenMock);
        }
    }
}
