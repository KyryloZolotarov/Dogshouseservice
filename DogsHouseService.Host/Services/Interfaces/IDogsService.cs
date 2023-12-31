﻿using DogsHouseService.Host.Models;
using DogsHouseService.Host.Models.Dtos;

namespace DogsHouseService.Host.Services.Interfaces
{
    public interface IDogsService
    {
        Task<IEnumerable<DogDto>> GetDogsAsync(GetDogsQueryParametrs param, CancellationToken cancellationToken);
        Task AddDogAsync(DogDto dog, CancellationToken cancellationToken);
    }
}
