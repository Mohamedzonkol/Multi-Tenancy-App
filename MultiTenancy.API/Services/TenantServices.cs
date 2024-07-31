using Microsoft.Extensions.Options;

namespace MultiTenancy.API.Services
{
    public class TenantServices : ITenantServices
    {
        private readonly TenantSettings _tenantSettings;
        private HttpContext? _httpContext;
        private Tenant? _currentTenant;

        public TenantServices(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _tenantSettings = tenantSettings.Value;

            if (_currentTenant is not null)
            {
                if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
                {
                    SetCurrentTenant(tenantId!);
                }
                else
                {
                    throw new Exception("Tenant not Provider");
                }
            }
        }

        public string? GetDatabaseProvider()
        {
            return _tenantSettings.Defaults.DbProvider;
        }

        public string? GetConnectionString()
        {
            var currentConnectionString = _currentTenant is null
                ? _tenantSettings.Defaults.ConnectionString
                : _currentTenant.ConnectionString;
            return currentConnectionString;
        }

        public Tenant GetTenant()
        {
            return _currentTenant;
        }
        private void SetCurrentTenant(string tenantId)
        {
            _currentTenant = _tenantSettings.Tenants.FirstOrDefault(x => x.TenantId == tenantId);
            if (_currentTenant is null)
            {
                throw new Exception("Tenant not found");
            }
            if (string.IsNullOrEmpty(_currentTenant.ConnectionString))
            {
                _currentTenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;

            }
        }
    }
}
