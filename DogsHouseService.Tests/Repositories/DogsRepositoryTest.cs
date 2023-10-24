using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Data;
using DogsHouseService.Host.Models;
using DogsHouseService.Host.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Repositories.Interfaces;
using DogsHouseService.Host.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using DogsHouseService.Host.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DogsHouseService.Tests.Repositories
{
    public class DogsRepositoryTest
    {
        [Fact]
        public async Task GetDogsAsync_Returns_IEnumerableDogEntity_Seccesfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContextMock = new ApplicationDbContext(options);
            var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
            dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

            var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

            var dogEntitySucces = new DogEntity()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };
            wrapper.DbContext.DogEntities.Add(dogEntitySucces);
            wrapper.DbContext.SaveChanges();

            var queryParameters = new GetDogsQueryParametrs() 
            {
                Attribute = "Name",
                Order = "desc"
            };
            var repository = new DogsRepository(wrapper);
            var result = await repository.GetDogsAsync(queryParameters);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddDogAsync_Seccesfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase1")
                .Options;
            var dbContextMock = new ApplicationDbContext(options);
            var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
            dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

            var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

            var dogEntitySucces = new DogEntity()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };

            var queryParameters = new GetDogsQueryParametrs();
            var repository = new DogsRepository(wrapper);
            await repository.AddDogAsync(dogEntitySucces);
            var result = wrapper.DbContext.DogEntities.FirstOrDefault(x => x.Name == dogEntitySucces.Name);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AddDogAsync_Throws_ArgumentException_Failed()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase2")
                .Options;
            var dbContextMock = new ApplicationDbContext(options);
            var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
            dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

            var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

            var dogEntitySucces = new DogEntity()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };
            wrapper.DbContext.DogEntities.Add(dogEntitySucces);
            wrapper.DbContext.SaveChanges();

            var queryParameters = new GetDogsQueryParametrs();
            var repository = new DogsRepository(wrapper);
            await Assert.ThrowsAsync<ArgumentException>(async () => {
                await repository.AddDogAsync(dogEntitySucces);
            });
        }
    }
}
