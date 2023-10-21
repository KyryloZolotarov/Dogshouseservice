using DogsHouseService.Host.Data;
using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Repositories.Interfaces;
using DogsHouseService.Host.Services.Interfaces;

namespace DogsHouseService.Host.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DogsRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public Task<bool> AddDog(DogEntity dog)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DogEntity>> GetGogs()
        {
            throw new NotImplementedException();
        }
    }
}
