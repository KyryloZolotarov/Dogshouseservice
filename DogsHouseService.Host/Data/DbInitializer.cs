using DogsHouseService.Host.Data.Entities;

namespace DogsHouseService.Host.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.DogEntities.Any())
            {
                await context.DogEntities.AddRangeAsync(GetPreconfiguredDogs());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<DogEntity> GetPreconfiguredDogs()
        {
            return new List<DogEntity>
        {
            new () { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 },
            new () { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14 }
        };
        }
    }
}
