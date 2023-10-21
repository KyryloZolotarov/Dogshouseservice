using AutoMapper;
using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Models.Dtos;

namespace DogsHouseService.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DogDto, DogEntity>();
        }
    }
}
