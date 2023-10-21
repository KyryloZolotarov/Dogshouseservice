using AutoMapper;
using DogsHouseService.Host.Data;
using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Models.Dtos;
using DogsHouseService.Host.Repositories.Interfaces;
using DogsHouseService.Host.Services.Interfaces;

namespace DogsHouseService.Host.Services
{
    public class DogsService : BaseDataService<ApplicationDbContext>, IDogsService
    {
        private readonly IDogsRepository _dogsRepository;
        private readonly ILogger<DogsService> _logger;
        private readonly IMapper _mapper;

        public DogsService(IDogsRepository dogsRepository,
            ILogger<DogsService> logger,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _dogsRepository = dogsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DogDto>> GetDogs()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _dogsRepository.GetGogs();
                var mappedResult = result.Select(s => _mapper.Map<DogDto>(s)).ToList();
                return mappedResult;
            });
        }

        public async Task AddDog(DogDto dog)
        {
            await ExecuteSafeAsync(async () =>
            {
                var mappedDog = _mapper.Map<DogEntity>(dog);
                await _dogsRepository.AddDog(mappedDog);
            });

        }
    }
}
