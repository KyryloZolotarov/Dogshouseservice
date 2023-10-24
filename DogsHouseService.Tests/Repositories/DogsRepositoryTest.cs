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
            var dbContextMock = new Mock<ApplicationDbContext>(options);
            var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
            dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock.Object);

            var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);
            
            var queryParameters = new GetDogsQweryParametrs();
            var dogEntitySucces = new DogEntity()
            {
                Name = "Test",
                Color = "Test_Color",
                TailLength = 10,
                Weight = 3
            };
            wrapper.DbContext.DogEntities.Add(dogEntitySucces);

            var repository = new DogsRepository(wrapper);
            var result = repository.GetGogsAsync(queryParameters);

            Assert.NotNull(result);
        }
    }
}
