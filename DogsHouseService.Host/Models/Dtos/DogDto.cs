using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.Host.Models.Dtos
{
    public class DogDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Color { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int TailLength { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Weight { get; set; }
    }
}
