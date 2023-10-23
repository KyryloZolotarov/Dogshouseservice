using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DogsHouseService.Host.Models.Dtos
{
    public class DogDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        [JsonPropertyName("tail_length")]
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
