namespace DogsHouseService.Host.Models
{
    public class GetDogsQweryParametrs
    {
        public string Attribute { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
