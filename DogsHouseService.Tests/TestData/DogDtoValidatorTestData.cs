using DogsHouseService.Host.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests.TestData
{
    public static class DogDtoValidatorTestData
    {
        public static IEnumerable<object[]> DogDtoTestData => new[] { new object[]
        {
            new DogDto()
            {
                Name = "name",
                Color = "color",
                TailLength = 5,
                Weight = 5,
            },
            true
        }, new object[]
        {
            new DogDto()
            {
                Name = "name",
                TailLength = 5,
                Weight = 5,
            },
            false
        }, new object[]
        {
            new DogDto()
            {
                Color = "color",
                TailLength = 5,
                Weight = 5,
            },
            false
        }, new object[]
        {
            new DogDto()
            {
                Name = "name",
                Color = "color",
                TailLength = -1,
                Weight = 5,
            },
            false
        }, new object[]
        {
            new DogDto()
            {
                Name = "name",
                Color = "color",
                TailLength = 5,
                Weight = -2,
            },
            false
        },
        };
    }
}
