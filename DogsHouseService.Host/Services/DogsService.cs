using AutoMapper;
using DogsHouseService.Host.Data;
using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Models;
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

        public async Task<IEnumerable<DogDto>> GetDogsAsync(GetDogsQweryParametrs param, CancellationToken cancellationToken)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _dogsRepository.GetGogsAsync(param);
                var mappedResult = result.Select(s => _mapper.Map<DogDto>(s)).ToList();
                return mappedResult;
            }, cancellationToken);
        }

        public async Task AddDogAsync(DogDto dog, CancellationToken cancellationToken)
        {
            await ExecuteSafeAsync(async () =>
            {
                var mappedDog = _mapper.Map<DogEntity>(dog);
                await _dogsRepository.AddDogAsync(mappedDog);
            }, cancellationToken);

        }
    }
}
