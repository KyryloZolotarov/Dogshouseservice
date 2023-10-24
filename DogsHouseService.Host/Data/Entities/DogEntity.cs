using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.Host.Data.Entities
{
    public class DogEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
