using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Models;

namespace DogsHouseService.Host.Repositories.Interfaces
{
    public interface IDogsRepository
    {
        Task<IEnumerable<DogEntity>> GetDogsAsync(GetDogsQweryParametrs param);
        Task AddDogAsync(DogEntity dog);
    }
}
