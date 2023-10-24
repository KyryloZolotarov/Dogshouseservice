using DogsHouseService.Host.Data;
using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Extentions;
using DogsHouseService.Host.Models;
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

        public async Task AddDogAsync(DogEntity dog)
        {
            await _dbContext.DogEntities.AddAsync(dog);
            await _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<DogEntity>> GetDogsAsync(GetDogsQweryParametrs param)
        {
            IQueryable<DogEntity> query = _dbContext.DogEntities;
            var result = query.SortBy(param.Attribute, param.Order)
                .Skip(param.PageSize * param.PageNumber)
                .Take(param.PageSize == 0 ? int.MaxValue : param.PageSize)
                .ToList();
            return Task.FromResult(result as IEnumerable<DogEntity>);
        }
    }
}
