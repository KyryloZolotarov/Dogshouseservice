using DogsHouseService.Host.Data.Entities;
using DogsHouseService.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DogsHouseService.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<DogEntity> DogEntities { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DogEntityTypeCongiguration());
        }
    }
}
