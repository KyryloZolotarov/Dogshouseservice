using DogsHouseService.Host.Services;
using DogsHouseService.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace DogsHouseService.Tests.Mocks;

public class MockService : BaseDataService<MockDbContext>
{
    public MockService(
        IDbContextWrapper<MockDbContext> dbContextWrapper,
        ILogger<MockService> logger)
        : base(dbContextWrapper, logger)
    {
    }

    public async Task RunWithException()
    {
        await ExecuteSafeAsync<bool>(() => throw new Exception());
    }

    public async Task RunWithoutException()
    {
        await ExecuteSafeAsync(() => Task.FromResult(true));
    }
}