using Microsoft.Extensions.Options;

namespace MultiTenancy.API.Services
{
    public class TenantServices : ITenantServices
    {
        private readonly IOptions<TenantSettings> _tenantSettings;
        private Tenant? _currentTenant;
        private HttpContext? _httpContext;

        public TenantServices(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor httpContextAccessor)
        {
            _tenantSettings = tenantSettings;
            _httpContext = httpContextAccessor.HttpContext;
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

        public string GetDatabaseProvider()
        {
            return _tenantSettings.Value.Defaults.DbProvider;
        }

        public string GetConnectionString()
        {
            return _currentTenant.ConnectionString ?? _tenantSettings.Value.Defaults.ConnectionString;
        }

        public Tenant GetTenant()
        {
            return _currentTenant;
        }
        private void SetCurrentTenant(string tenantId)
        {
            _currentTenant = _tenantSettings.Value.Tenants.FirstOrDefault(x => x.TenantId == tenantId);
            if (_currentTenant is null)
            {
                throw new Exception("Tenant not found");
            }
            if (string.IsNullOrEmpty(_currentTenant.ConnectionString))
            {
                _currentTenant.ConnectionString = _tenantSettings.Value.Defaults.ConnectionString;

            }
        }
    }
}
