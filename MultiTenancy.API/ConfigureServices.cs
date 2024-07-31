namespace MultiTenancy.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddMultiTenancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantServices, TenantServices>();

            services.Configure<TenantSettings>(configuration.GetSection(nameof(TenantSettings)));

            TenantSettings options = new();
            configuration.GetSection(nameof(TenantSettings)).Bind(options);

            var defaultDbProvider = options.Defaults.DbProvider;

            if (defaultDbProvider.ToLower() == "mssql")
            {
                services.AddDbContext<AppDbContext>(m => m.UseSqlServer());
            }
            foreach (var tenant in options.Tenants)
            {
                var connectionString = tenant.ConnectionString ?? options.Defaults.ConnectionString;

                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.SetConnectionString(connectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }

            return services;
        }
    }
}
