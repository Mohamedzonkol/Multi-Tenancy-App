using MultiTenancy.API.Contracts;

namespace MultiTenancy.API
{
    public class Product : IMustHaveTenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Descreption { get; set; } = default!;
        public int Rate { get; set; }

        public string TenantId { get; set; } = default!;
    }

}
