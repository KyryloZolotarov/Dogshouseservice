using DogsHouseService.Host.Data;
using Microsoft.EntityFrameworkCore;
using DogsHouseService.Host.Services;
using DogsHouseService.Host.Services.Interfaces;
using DogsHouseService.Host.Repositories.Interfaces;
using DogsHouseService.Host.Repositories;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;

namespace DogsHouseService.Host;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddTransient<IDogsService, DogsService>();
        builder.Services.AddTransient<IDogsRepository, DogsRepository>();

        builder.Services.AddDbContextFactory<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration["ConnectionString"]));
        builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();        
        builder.Services.AddControllers()
            .AddFluentValidation(fluentValidtor => fluentValidtor.RegisterValidatorsFromAssemblyContaining<Program>().ImplicitlyValidateChildProperties = true);

        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        builder.Services.AddInMemoryRateLimiting();
        var app = builder.Build();

        app.UseRouting();
        app.UseIpRateLimiting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.MapGet("/ping", () => "Dogshouseservice.Version1.0.1");

        CreateDbIfNotExists(app);
        app.Run();

        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                DbInitializer.Initialize(context).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}
