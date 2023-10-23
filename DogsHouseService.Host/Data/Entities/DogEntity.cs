using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.Host.Data.Entities
{
    public class DogEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Color { get; set; }
        [Range(0, int.MaxValue)]
        public int TailLength { get; set; }
        [Range(0, int.MaxValue)]
        public int Weight { get; set; }
    }
}
