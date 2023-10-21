using DogsHouseService.Host.Models.Dtos;

namespace DogsHouseService.Host.Services.Interfaces
{
    public interface IDogsService
    {
        Task<IEnumerable<DogDto>> GetDogs();
        Task AddDog(DogDto dog);
    }
}
