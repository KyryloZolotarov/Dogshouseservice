using DogsHouseService.Host.Models.Dtos;
using FluentValidation;

namespace DogsHouseService.Host.Validators
{
    public class DogDtoValidator : AbstractValidator<DogDto>
    {
        public DogDtoValidator()
        {
            RuleFor(h =>  h.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Color).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TailLength).GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(x => x.Weight).GreaterThan(0).LessThan(int.MaxValue);
        }
    }
}
