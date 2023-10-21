using DogsHouseService.Host.Data.Entities;

namespace DogsHouseService.Host.Repositories.Interfaces
{
    public interface IDogsRepository
    {
        Task<IEnumerable<DogEntity>> GetGogs();
        Task<bool> AddDog(DogEntity dog);
    }
}
