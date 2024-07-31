namespace MultiTenancy.API.Services
{
    public interface ITenantServices
    {

        string GetDatabaseProvider();
        string GetConnectionString();
        Tenant GetTenant();
    }
}
