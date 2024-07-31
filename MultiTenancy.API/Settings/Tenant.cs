namespace MultiTenancy.API.Settings
{
    public class Tenant
    {
        public string Name { get; set; } = default!;
        public string TenantId { get; set; } = default!;
        public string? ConnectionString { get; set; }
    }
}
