namespace MultiTenancy.API.DTOS
{
    public class CreateProduct
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rate { get; set; }
    }
}
