using DogsHouseService.Host.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data.Common;
using System.Net;

namespace DogsHouseService.Host.Services;

public abstract class BaseDataService<T>
    where T : DbContext
{
    private readonly IDbContextWrapper<T> _dbContextWrapper;
    private readonly ILogger<BaseDataService<T>> _logger;

    protected BaseDataService(
        IDbContextWrapper<T> dbContextWrapper,
        ILogger<BaseDataService<T>> logger)
    {
        _dbContextWrapper = dbContextWrapper;
        _logger = logger;
    }

    protected Task ExecuteSafeAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        return ExecuteSafeAsync(token => action(), cancellationToken);
    }

    protected Task<TResult> ExecuteSafeAsync<TResult>(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        return ExecuteSafeAsync(token => action(), cancellationToken);
    }

    private async Task ExecuteSafeAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        when ((ex.InnerException as SqlException)?.Number == 2627)
        {
            throw new BadHttpRequestException("Dog with requested name allready exists", 400);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "transaction is rollbacked");
            throw;
        }
    }

    private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContextWrapper.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await action(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "transaction is rollbacked");
            throw;
        }

        return default!;
    }
}