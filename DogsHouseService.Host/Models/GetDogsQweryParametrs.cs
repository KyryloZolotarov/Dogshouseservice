namespace DogsHouseService.Host.Models
{
    public class GetDogsQweryParametrs
    {
        public string Attribute { get; set; } = "Id";
        public string Order { get; set; } = "Asc";
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
