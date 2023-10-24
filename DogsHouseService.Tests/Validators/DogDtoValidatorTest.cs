using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Validators;
using DogsHouseService.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests.Validators
{
    public class DogDtoValidatorTest
    {
        [Theory]
        [MemberData(nameof(DogDtoValidatorTestData.DogDtoTestData),
            MemberType = typeof(DogDtoValidatorTestData))]
        public async Task DogDtoValidation_Correct(DogDto dog, bool expectedResult)
        {
            var validator = new DogDtoValidator();
            var result = await validator.ValidateAsync(dog);
            Assert.Equal(expectedResult, result.IsValid);
        } 
    }
}
