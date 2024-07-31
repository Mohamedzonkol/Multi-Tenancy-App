namespace MultiTenancy.API.Settings
{
    public class TenantSettings
    {
        public Configrations Defaults { get; set; } = default!;
        public List<Tenant> Tenants { get; set; } = [];
    }
}
